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
using YantraDAL;
using vllib;

public partial class Modules_Masters_Godown : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;

        if (!IsPostBack)
        {
            setControlsVisibility();

            FillCompany();
        }
            
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }
   
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblGodownDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtgodown);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvGodownDetails.SelectedIndex > -1)
        {
            tblGodownDetails.Visible = true;
            txtgodown.Text = gvGodownDetails.SelectedRow.Cells[1].Text;
            ddlCompanyId.SelectedValue = gvGodownDetails.SelectedRow.Cells[3].Text;
            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast one Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (gvGodownDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.Godown objMaster = new Masters.Godown();
                objMaster.GodownId = gvGodownDetails.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objMaster.Godown_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblGodownDetails.Visible = false;

                gvGodownDetails.DataBind();
                gvGodownDetails.SelectedIndex = -1;

                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {

            GodownSave();
            tblGodownDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            GodownUpdate();
            tblGodownDetails.Visible = false;
        }
        gvGodownDetails.SelectedIndex = -1;
    }

    #region GodownSave
    private void GodownSave()
    {
        try
        {
            Masters.Godown objMaster = new Masters.Godown();
            //objMaster.GodownName = txtgodown.Text;
            objMaster.GodownName = txtgodown.Text;
            objMaster.Cpid =ddlCompanyId.SelectedValue;
             objMaster.Godown_Save();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvGodownDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region GodownUpdate
    private void GodownUpdate()
    {
        try
        {
            Masters.Godown objMaster = new Masters.Godown();
            objMaster.GodownId = gvGodownDetails.SelectedRow.Cells[0].Text;
            objMaster.GodownName = txtgodown.Text;
            objMaster.Cpid = ddlCompanyId.SelectedValue;


            MessageBox.Show(this, objMaster.Godown_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvGodownDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion   

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }

    #region FillCompany
    private void FillCompany()
    {
        try
        {
            Masters.Godown.Company_Select(ddlCompanyId);
           
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        
    }
    #endregion




    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblGodownDetails.Visible = false;
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvGodownDetails.DataBind();
    }
    
    protected void gvGodownDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
    protected void lbtnCountryName_Click(object sender, EventArgs e)
    {
        tblGodownDetails.Visible = false;
        LinkButton lbtnGodownName;
        lbtnGodownName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnGodownName.Parent.Parent;
        gvGodownDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }



  
}
