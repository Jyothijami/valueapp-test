//Date Written: 08/Jan/2009      Written By: L.Hima Kishore



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
using System.Data.SqlClient;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Services_AMCAssignments : System.Web.UI.Page
{


    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Yantra.Authentication.Privilege_Check(this);
            //lblEmpIdHidden.Text = Yantra.LoginAuthentication.EmpDetails[(int)Yantra.LoginAuthentication.Logged_EMP_Details.EmpId];
        }
    }
    #endregion

    #region lbtn Assign Task Click
    protected void lbtnAssignTaskNo_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
        LinkButton lbtnEnqNo;
        lbtnEnqNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnqNo.Parent.Parent;
        gvAssignDetails.SelectedIndex = gvRow.RowIndex;
        lblAssignTaskIdHiddenForFollowUp.Text = gvRow.Cells[0].Text;
    }
    #endregion

    #region gvEnquiry Assigned Tasks_RowDataBound
    protected void gvAssignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
    #endregion

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Enquiry Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date" || ddlSearchBy.SelectedItem.Text == "Assign Date" || ddlSearchBy.SelectedItem.Text == "Due Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
    }
    #endregion

    #region ddlSymbols_SelectedIndexChanged
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            imgFromDate.Visible = true;
            ceSearchFrom.Enabled = true;
            MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvAssignDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvAssignDetails.DataBind();
    }
    #endregion

    #region Enquiry Assignments FollowUp

    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCAssignments objServicesAssign = new Services.AMCAssignments();
            Services.BeginTransaction();
            objServicesAssign.AssignTaskId = gvAssignDetails.SelectedRow.Cells[0].Text;
            objServicesAssign.FollowUpEmpId = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            objServicesAssign.FollowUpDesc = txtFollowUpDesc.Text;
            objServicesAssign.FollowUpDate = DateTime.Now.ToString();
            MessageBox.Show(this, objServicesAssign.AMCAssignmentsFollowUp_Save());
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFollowUp.DataBind();
            txtFollowUpDesc.Text = string.Empty;
            Services.Dispose();
        }
    }

    protected void btnFollowUpRefresh_Click(object sender, EventArgs e)
    {
        txtFollowUpDesc.Text = string.Empty;
    }

    protected void btnFollowUpHistory_Click(object sender, EventArgs e)
    {
        if (tblFollowUpHistory.Visible == false)
        {
            tblFollowUpHistory.Visible = true;
        }
        else if (tblFollowUpHistory.Visible == true)
        {
            tblFollowUpHistory.Visible = false;
        }
    }

    protected void btnFollowUpClose_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
    }

    protected void btnFollowUp_Click(object sender, EventArgs e)
    {
        if (gvAssignDetails.SelectedIndex > -1)
        {
            gvFollowUp.DataBind();
            txtFollowUpName.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpName];
            if (tblFollowUp.Visible == false)
            {
                tblFollowUp.Visible = true;
            }
            else if (tblFollowUp.Visible == true)
            {
                tblFollowUp.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }

    #endregion

    #region Button SENDQUOTATION Click
    protected void btnSendQuotation_Click(object sender, EventArgs e)
    {
        if (gvAssignDetails.SelectedIndex > -1)
        {
            Response.Redirect("AMCQuotation.aspx?enqid=" + gvAssignDetails.SelectedRow.Cells[1].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion



}

 
