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

public partial class Modules_Masters_RegisterMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }
    #endregion

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
            RegisterMasterSave();
        }
        else if (btnSave.Text == "Update")
        {
            RegisterMasterUpdate();
        }
        gvRegistertMaster.SelectedIndex = -1;
    }
    #endregion

    #region RegisterMasterSave
    private void RegisterMasterSave()
    {
        try
        {
            Masters.RegisterMaster objMaster = new Masters.RegisterMaster();
            objMaster.RegisterName = txtRegisterName.Text;
            objMaster.RegisterDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.RegisterMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblRegisterDetails.Visible = false;
           
            gvRegistertMaster.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }

    }
    #endregion

    #region RegisterMasterUpdate
    private void RegisterMasterUpdate()
    {
        try
        {
            Masters.RegisterMaster objMaster = new Masters.RegisterMaster();
            objMaster.RegisterId = gvRegistertMaster.SelectedRow.Cells[1].Text;
            objMaster.RegisterName = txtRegisterName.Text;
            objMaster.RegisterDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.RegisterMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblRegisterDetails.Visible = false;
           
            gvRegistertMaster.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvRegistertMaster_RowDataBound
    protected void gvRegistertMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button RegisterName_Click
    protected void lbtnRegisterName_Click(object sender, EventArgs e)
    {
        tblRegisterDetails.Visible = false;
        LinkButton lbtnRegisterName;
        lbtnRegisterName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnRegisterName.Parent.Parent;
        gvRegistertMaster.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvRegistertMaster.SelectedIndex > -1)
        {
            tblRegisterDetails.Visible = true;
            txtRegisterName.Text = gvRegistertMaster.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvRegistertMaster.SelectedRow.Cells[3].Text;
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
        if (gvRegistertMaster.SelectedIndex > -1)
        {
            try
            {
                Masters.RegisterMaster objMaster = new Masters.RegisterMaster();
                objMaster.RegisterId = gvRegistertMaster.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.RegisterMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblRegisterDetails.Visible = false;
               
                gvRegistertMaster.DataBind();
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
        tblRegisterDetails.Visible = true;
        ScriptManagerLocal.SetFocus(txtRegisterName);
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblRegisterDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvRegistertMaster.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
