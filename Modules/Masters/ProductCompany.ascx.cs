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

public partial class Modules_Masters_ProductCompany : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
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
            CompanySave();
            tblCompanyDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            CompanyUpdate();
            tblCompanyDetails.Visible = false;
        }
        gvCompanyDetails.SelectedIndex = -1;
    }
    #endregion

    #region CompanySave
    private void CompanySave()
    {
        try
        {
            Masters.ProductCompany objMaster = new Masters.ProductCompany ();
            objMaster.PdCompanyName = txtCompanyName.Text;
            objMaster.PdCompanyDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.ProductCompany_Save ());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvCompanyDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region CompanyUpdate
    private void CompanyUpdate()
    {
        try
        {
            Masters.ProductCompany objMaster = new Masters.ProductCompany ();
            objMaster.PdCompanyId = gvCompanyDetails.SelectedRow.Cells[1].Text;
            objMaster.PdCompanyName = txtCompanyName.Text;
            objMaster.PdCompanyDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.ProductCompany_Update ());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvCompanyDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvCompanyDetails_RowDataBound
    protected void gvCompanyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button lbtnCompanyName_Click
    protected void lbtnCompanyName_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
        LinkButton lbtnCompanyName;
        lbtnCompanyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvCompanyDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCompanyDetails.SelectedIndex > -1)
        {
            tblCompanyDetails.Visible = true;
            txtCompanyName.Text = gvCompanyDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvCompanyDetails.SelectedRow.Cells[3].Text;

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
        if (gvCompanyDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.ProductCompany objMaster = new Masters.ProductCompany();
                objMaster.PdCompanyId = gvCompanyDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.ProductCompany_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvCompanyDetails.DataBind();
                gvCompanyDetails.SelectedIndex = -1;

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
        tblCompanyDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtCompanyName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvCompanyDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
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
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion
    
    protected void txtDepartmentName_TextChanged(object sender, EventArgs e)
    {

    }

   
}
