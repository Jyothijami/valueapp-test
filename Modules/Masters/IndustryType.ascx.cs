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

public partial class Modules_Masters_IndustryType : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
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
            IndustrySave();
        }
        else if (btnSave.Text == "Update")
        {
            IndustryUpdate();
        }
    }
    #endregion

    #region IndustrySave
    private void IndustrySave()
    {
        try
        {
            Masters.IndustryType objMaster = new Masters.IndustryType();
            objMaster.IndType = txtIndustryTypeName.Text;
            objMaster.IndDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.IndustryType_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblIndTypeDetails.Visible = false;
           
            gvIndustryTypeDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region IndustryUpdate
    private void IndustryUpdate()
    {
        try
        {
            Masters.IndustryType objMaster = new Masters.IndustryType();
            objMaster.IndTypeId = gvIndustryTypeDetails.SelectedRow.Cells[1].Text;
            objMaster.IndType = txtIndustryTypeName.Text;
            objMaster.IndDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.IndustryType_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblIndTypeDetails.Visible = false;
           
            gvIndustryTypeDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvIndustryTypeDetails_RowDataBound
    protected void gvIndustryTypeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion


    #region Link Button IndustryTypeName_Click
    protected void lbtnIndustryTypeName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnIndustryTypeName;
        lbtnIndustryTypeName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIndustryTypeName.Parent.Parent;
        gvIndustryTypeDetails.SelectedIndex = gvRow.RowIndex;
        tblIndTypeDetails.Visible = false;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvIndustryTypeDetails.SelectedIndex > -1)
        {
            tblIndTypeDetails.Visible = true;
            txtIndustryTypeName.Text = gvIndustryTypeDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvIndustryTypeDetails.SelectedRow.Cells[3].Text;
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
        if (gvIndustryTypeDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.IndustryType objMaster = new Masters.IndustryType();
                objMaster.IndTypeId = gvIndustryTypeDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.IndustryType_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblIndTypeDetails.Visible = false;
               
                gvIndustryTypeDetails.DataBind();
                gvIndustryTypeDetails.SelectedIndex = -1;
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
        tblIndTypeDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtIndustryTypeName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvIndustryTypeDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblIndTypeDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion

}
