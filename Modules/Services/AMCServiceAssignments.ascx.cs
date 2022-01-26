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
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Services_AMCServiceAssignments : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            txtSearchValueFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtSearchText.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            btnSearchGo_Click(sender, e);
            gvAMCAssignDetails.DataBind();
        }
    }
    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvAMCAssignDetails.SelectedIndex = -1;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvAMCAssignDetails.DataBind();
    }
    #endregion

    protected void lbtnAssignNo_Click(object sender, EventArgs e)
    {
        lbtnCustName_Click(sender, e);
    }
    protected void lbtnCustName_Click(object sender, EventArgs e)
    {
        tblAssignTasks.Visible = false;
        LinkButton lbtnCustName;
        lbtnCustName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCustName.Parent.Parent;
        gvAMCAssignDetails.SelectedIndex = gvRow.RowIndex;
        lblAssignTaskIdHiddenForFollowUp.Text = gvAMCAssignDetails.SelectedRow.Cells[0].Text;

        try
        {
            ClearControls();
            tblAssignTasks.Visible = true;
            Services.AMCAssignment OBJASSN = new Services.AMCAssignment();
            if (OBJASSN.AMCAssignment_Select(gvAMCAssignDetails.SelectedRow.Cells[0].Text) > 0)
            {
                txtAssignedDate.Text = OBJASSN.AMCAAssignDate;
                txtDueDate.Text = OBJASSN.AMCADueDate;
                txtStatus.Text = OBJASSN.AMCAStatus;
                Services.AMCOrderAcceptance objamcoa = new Services.AMCOrderAcceptance();
                if (objamcoa.AMCOrderAcceptance_Select(OBJASSN.AMCOAId) > 0)
                {
                    txtEnquiryNo.Text = objamcoa.OANo;
                    txtEnquiryDate.Text = objamcoa.OADate;
                    SM.CustomerMaster objcust = new SM.CustomerMaster();
                    if (objcust.CustomerMaster_Select(objamcoa.CustId) > 0)
                    {
                        txtCustomerName.Text = objcust.CustName;
                    }
                }
                HR.EmployeeMaster objemp = new HR.EmployeeMaster();
                if (objemp.EmployeeMaster_Select(OBJASSN.EmpId) > 0)
                {
                    txtAssignedTo.Text = objemp.EmpFirstName + " " + objemp.EmpMiddleName + " " + objemp.EmpLastName;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }

    #region Button Assign Task
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    Services.AMCAssignment objServicesAssign = new Services.AMCAssignment();
        //    Services.BeginTransaction();
        //    objServicesAssign.AMCANo = txtAssignTaskNo.Text;
        //    objServicesAssign.AMCAId = gvAMCAssignDetails.SelectedRow.Cells[0].Text;
        //    objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
        //    objServicesAssign.AMCAAssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
        //    objServicesAssign.AMCADueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
        //    objServicesAssign.AMCADate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
        //    objServicesAssign.AMCARemarks = txtRemarksForAssingn.Text;
        //    objServicesAssign.AMCAStatus = "New";
        //    MessageBox.Show(this, objServicesAssign.AMCAssignment_Update());
        //    Services.CommitTransaction();
        //}
        //catch (Exception ex)
        //{
        //    Services.RollBackTransaction();
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    tblAssignTasks.Visible = false;
        //    gvAMCAssignDetails.DataBind();
        //    ClearControls();
        //    Services.Dispose();
        //}
    }
    #endregion

    #region Button Cancel Task
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {

        tblAssignTasks.Visible = false;
    }
    #endregion

    private void ClearControls()
    {
        txtAssignedDate.Text = "";
        txtCustomerName.Text = "";
        txtDueDate.Text = "";
        txtEnquiryDate.Text = "";
        txtEnquiryNo.Text = "";
    }
    protected void gvAMCAssignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #region Enquiry Assignments FollowUp

    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCAssignment objSMAssign = new Services.AMCAssignment();
            Services.BeginTransaction();
            objSMAssign.AMCAssignTaskId = gvAMCAssignDetails.SelectedRow.Cells[0].Text;
            objSMAssign.FollowUpEmpId = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            objSMAssign.FollowUpDesc = txtFollowUpDesc.Text;
            objSMAssign.FollowUpDate = DateTime.Now.ToString();
            MessageBox.Show(this, objSMAssign.AMCAssignmentsFollowUp_Save());
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
        if (gvAMCAssignDetails.SelectedIndex > -1)
        {
            tblUpdateStatus.Visible = false;
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

    protected void btnUpdateSetStatus_Click(object sender, EventArgs e)
    {
        Services.AMCAssignment.AMCAssignmentStatus_Update(ddlStatus.SelectedItem.Text, gvAMCAssignDetails.SelectedRow.Cells[0].Text);
    }
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        ddlStatus.SelectedValue = gvAMCAssignDetails.SelectedRow.Cells[7].Text;
        if (tblUpdateStatus.Visible == true)
        {
            tblUpdateStatus.Visible = false;
        }
        else
        {
            tblUpdateStatus.Visible = true;
        }
    }
    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}
