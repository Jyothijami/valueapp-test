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

public partial class Modules_Masters_EnquiryMode : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }
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
            EnquiryModeSave();
            tblEquiryModeDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            EnquiryModeUpdate();
            tblEquiryModeDetails.Visible = false;
        }
        gvEnquiryMode.SelectedIndex = -1;
    }
    #endregion

    #region EnquiryModeSave
    private void EnquiryModeSave()
    {
        try
        {
            Masters.EnquiryMode objMaster = new Masters.EnquiryMode();
            objMaster.EnqmName = txtEnquiryMode.Text;
            objMaster.EnqmDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.EnquiryMode_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvEnquiryMode.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region EnquiryModeUpdate
    private void EnquiryModeUpdate()
    {
        try
        {
            Masters.EnquiryMode objMaster = new Masters.EnquiryMode();
            objMaster.EnqmId = gvEnquiryMode.SelectedRow.Cells[1].Text;
            objMaster.EnqmName = txtEnquiryMode.Text;
            objMaster.EnqmDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.EnquiryMode_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvEnquiryMode.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvEnquiryMode_RowDataBound
    protected void gvEnquiryMode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button EnquiryMode_Click
    protected void lbtnEnquiryMode_Click(object sender, EventArgs e)
    {
        tblEquiryModeDetails.Visible = false;
        LinkButton lbtnEnquiryMode;
        lbtnEnquiryMode = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnquiryMode.Parent.Parent;
        gvEnquiryMode.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvEnquiryMode.SelectedIndex > -1)
        {
            tblEquiryModeDetails.Visible = true;
            txtEnquiryMode.Text = gvEnquiryMode.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvEnquiryMode.SelectedRow.Cells[3].Text;
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
        if (gvEnquiryMode.SelectedIndex > -1)
        {
            try
            {
                Masters.EnquiryMode objMaster = new Masters.EnquiryMode();
                objMaster.EnqmId = gvEnquiryMode.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.EnquiryMode_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblEquiryModeDetails.Visible = false;
               
                gvEnquiryMode.DataBind();
                gvEnquiryMode.SelectedIndex = -1;

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
        tblEquiryModeDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtEnquiryMode);
    }
    #endregion



    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvEnquiryMode.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblEquiryModeDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion


    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
