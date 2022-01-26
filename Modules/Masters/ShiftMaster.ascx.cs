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

public partial class Modules_Masters_ShiftMaster : System.Web.UI.UserControl
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
            ShiftSave();
        }
        else if (btnSave.Text == "Update")
        {
            ShiftUpdate();

        }
    }
    #endregion

    #region ShiftSave
    private void ShiftSave()
    {
        try
        {
            Masters.ShiftMaster objMaster = new Masters.ShiftMaster();
            objMaster.ShiftId = txtShiftCode.Text;
            objMaster.ShiftName = txtShiftName.Text;
            objMaster.ShiftStartTime = txtStartTime.Text;
            objMaster.ShiftEndTime = txtEndTime.Text;
            objMaster.ShiftBreakDur = txtBreakDuration.Text;
            objMaster.ShiftAvailableDur = txtAvailableTime.Text;
            MessageBox.Show(this, objMaster.ShiftMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            tblShiftDetails.Visible = false;
            gvShiftMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ShiftUpdate
    private void ShiftUpdate()
    {
        try
        {
            Masters.ShiftMaster objMaster = new Masters.ShiftMaster();
            objMaster.ShiftId = gvShiftMasterDetails.SelectedRow.Cells[1].Text;
            objMaster.ShiftName = txtShiftName.Text;
            objMaster.ShiftStartTime = txtStartTime.Text;
            objMaster.ShiftEndTime = txtEndTime.Text;
            objMaster.ShiftBreakDur = txtBreakDuration.Text;
            objMaster.ShiftAvailableDur = txtAvailableTime.Text;
            MessageBox.Show(this, objMaster.ShiftMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            tblShiftDetails.Visible = false;
            gvShiftMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvShiftMasterDetails_RowDataBound
    protected void gvShiftMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion


    #region Link Button ShiftName_Click
    protected void lbtnShiftName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnShiftName;
        lbtnShiftName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnShiftName.Parent.Parent;
        gvShiftMasterDetails.SelectedIndex = gvRow.RowIndex;
        tblShiftDetails.Visible = false;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvShiftMasterDetails.SelectedIndex > -1)
        {
            tblShiftDetails.Visible = true;
            txtShiftCode.Text = gvShiftMasterDetails.SelectedRow.Cells[1].Text;
            txtShiftName.Text = gvShiftMasterDetails.SelectedRow.Cells[0].Text;
            txtStartTime.Text = gvShiftMasterDetails.SelectedRow.Cells[3].Text;
            txtEndTime.Text = gvShiftMasterDetails.SelectedRow.Cells[4].Text;
            txtBreakDuration.Text = gvShiftMasterDetails.SelectedRow.Cells[5].Text;
            txtAvailableTime.Text = gvShiftMasterDetails.SelectedRow.Cells[6].Text;
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
        try
        {
            if (gvShiftMasterDetails.SelectedIndex > -1)
            {
                Masters.ShiftMaster objMaster = new Masters.ShiftMaster();
                objMaster.ShiftId = gvShiftMasterDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.ShiftMaster_Delete());
            }
            else
            {
                MessageBox.Show(this, "Please select atleast a Record");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblShiftDetails.Visible = false;
           
            gvShiftMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblShiftDetails.Visible = true;
        txtShiftCode.Text = Masters.ShiftMaster.ShiftMaster_AutoGenCode();
        ScriptManagerLocal.SetFocus(txtShiftName);
    }
    #endregion

    //#region Button SEARCH GO Click
    //protected void btnSearchGo_Click(object sender, EventArgs e)
    //{
    //    lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
    //    lblSearchValueHidden.Text = txtSearchText.Text;
    //    gvShiftMasterDetails.DataBind();
    //}
    //#endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblShiftDetails.Visible = false;

    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Start Time" || ddlSearchBy.SelectedItem.Text == "End Time")
        {
            ddlSymbols.Visible = true;
            MaskedEditSearchToTime.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            MaskedEditSearchToTime.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            MaskedEditSearchFromTime.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion

    #region DropDownList Symbols Select Index Changed
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            MaskedEditSearchFromTime.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            MaskedEditSearchFromTime.Enabled = false;
        }
    }
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvShiftMasterDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvShiftMasterDetails.DataBind();
    }
    #endregion


}
