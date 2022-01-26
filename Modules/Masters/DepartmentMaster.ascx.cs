using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;

public partial class Modules_Masters_DepartmentMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            setControlsVisibility();
            FillDepartmentHeader();
            
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }


    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            DepartmentSave();
            tblDeptDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            DepartmentUpdate();
            tblDeptDetails.Visible = false;
        }
        gvDepartmentDetails.SelectedIndex = -1;
    }
    #endregion

    #region DepartmentSave
    private void DepartmentSave()
    {
        try
        {
            Masters.Department objMaster = new Masters.Department();
            objMaster.DeptName = txtDepartmentName.Text;
            objMaster.DeptHead = ddlDeptHead.SelectedItem.Value;
            objMaster.DeptDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Department_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvDepartmentDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region DepartmentUpdate
    private void DepartmentUpdate()
    {
        try
        {
            Masters.Department objMaster = new Masters.Department();
            objMaster.DeptId = gvDepartmentDetails.SelectedRow.Cells[1].Text;
            objMaster.DeptName = txtDepartmentName.Text;
            objMaster.DeptHead = ddlDeptHead.SelectedValue;
            objMaster.DeptDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Department_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {        
            gvDepartmentDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvDepartmentDetails_RowDataBound
    protected void gvDepartmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button DepartmentName_Click
    protected void lbtnDepartmentName_Click(object sender, EventArgs e)
    {
        tblDeptDetails.Visible = false;
        LinkButton lbtnDepartmentName;
        lbtnDepartmentName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDepartmentName.Parent.Parent;
        gvDepartmentDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvDepartmentDetails.SelectedIndex > -1)
        {
            tblDeptDetails.Visible = true;
            txtDepartmentName.Text = gvDepartmentDetails.SelectedRow.Cells[0].Text;
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            obj.EmployeeMaster_SelectDepartment(gvDepartmentDetails.SelectedRow.Cells[3].Text);
            ddlDeptHead.SelectedValue = obj.EmpID;            
            txtDescription.Text = gvDepartmentDetails.SelectedRow.Cells[4].Text;

            btnSave.Text = "Update";           
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvDepartmentDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.Department objMaster = new Masters.Department();
                objMaster.DeptId = gvDepartmentDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Department_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
               
                gvDepartmentDetails.DataBind();
                gvDepartmentDetails.SelectedIndex = -1;

                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblDeptDetails.Visible = true;
        
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvDepartmentDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblDeptDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Dropdown list select index change
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
    }
    #endregion

    #region Fill DepartmentHeader
    private void FillDepartmentHeader()
    {
        try
        {
            //Masters.Department.Department_Select(ddlDeptHead);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlDeptHead);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            HR.Dispose();
            //Masters.Dispose();
        }
    }
    #endregion
}
