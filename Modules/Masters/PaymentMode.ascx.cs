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

public partial class Modules_Masters_PaymentMode : System.Web.UI.UserControl
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
            PayModeSave();
            tblPayModeDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            PayModeUpdate();
            tblPayModeDetails.Visible = false;
        }
        gvPayModetDetails.SelectedIndex = -1;
    }
    #endregion

    #region PayModeSave
    private void PayModeSave()
    {
        try
        {
            Masters.PayMode objMaster = new Masters.PayMode();
            objMaster.PMName = txtPaymentMode.Text;
            objMaster.PMDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.PayMode_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvPayModetDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region PayModeUpdate
    private void PayModeUpdate()
    {
        try
        {
            Masters.PayMode objMaster = new Masters.PayMode();
            objMaster.PMId = gvPayModetDetails.SelectedRow.Cells[1].Text;
            objMaster.PMName = txtPaymentMode.Text;
            objMaster.PMDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.PayMode_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvPayModetDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvPayModetDetails_RowDataBound
    protected void gvPayModetDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button PayMode_Click
    protected void lbtnPayMode_Click(object sender, EventArgs e)
    {
        tblPayModeDetails.Visible = false;
        LinkButton lbtnPayMode;
        lbtnPayMode = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPayMode.Parent.Parent;
        gvPayModetDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvPayModetDetails.SelectedIndex > -1)
        {
            tblPayModeDetails.Visible = true;
            txtPaymentMode.Text = gvPayModetDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvPayModetDetails.SelectedRow.Cells[3].Text;
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
        if (gvPayModetDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.PayMode objMaster = new Masters.PayMode();
                objMaster.PMId = gvPayModetDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.PayMode_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblPayModeDetails.Visible = false;
               
                gvPayModetDetails.DataBind();
                gvPayModetDetails.SelectedIndex = -1;

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
        tblPayModeDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtPaymentMode);
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
        tblPayModeDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvPayModetDetails.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
