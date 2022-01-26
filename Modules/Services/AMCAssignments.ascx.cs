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

public partial class Modules_Services_SAMCAssignments : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSearchValueFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtSearchText.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            btnSearchGo_Click(sender, e);
            gvAMCAssignDetails.DataBind();
            EmployeeMaster_Fill();
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

        try
        {
            ClearControls();
            tblAssignTasks.Visible = true;
            Services.AMCAssignment OBJASSN = new Services.AMCAssignment();
            if (OBJASSN.AMCAssignment_Select(gvAMCAssignDetails.SelectedRow.Cells[0].Text) > 0)
            {
                if (OBJASSN.EmpId == "0")
                {
                    btnAssignTask.Text = "Assign";
                    txtAssignTaskNo.Text = Services.AMCAssignment.AMCAssignment_AutoGenCode();
                }
                else if (OBJASSN.EmpId != "0")
                {
                    btnAssignTask.Text = "Re-Assign";
                    txtAssignTaskNo.Text = OBJASSN.AMCANo;
                }
                ddlEmpNameForAssign.SelectedValue = OBJASSN.EmpId;
                ddlEmpNameForAssign_SelectedIndexChanged(sender, e);
                txtRemarksForAssingn.Text = OBJASSN.AMCARemarks;
                txtAssignDate.Text = OBJASSN.AMCAAssignDate;
                txtDueDate.Text = OBJASSN.AMCADueDate;
                Services.AMCOrderAcceptance objamcoa = new Services.AMCOrderAcceptance();
                if (objamcoa.AMCOrderAcceptance_Select(OBJASSN.AMCOAId) > 0)
                {
                    txtEnquiryNoForAssign.Text = objamcoa.OANo;
                    txtEnquiryDateForAssign.Text = objamcoa.OADate;
                    SM.CustomerMaster objcust = new SM.CustomerMaster();
                    if (objcust.CustomerMaster_Select(objamcoa.CustId) > 0)
                    {
                        txtCustomerNameForAssingn.Text = objcust.CustName;
                        txtCustomerEmailForAssingn.Text = objcust.Email;
                    }
                }
            }
        }
        catch
        {
        }
    }

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpNameForAssign);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region ddlEmpNameForAssign_SelectedIndexChanged
    protected void ddlEmpNameForAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlEmpNameForAssign.SelectedItem.Value) > 0)
            {
                txtEmpEmailId.Text = objHR.EmpEMail;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Button Assign Task
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCAssignment objServicesAssign = new Services.AMCAssignment();
            Services.BeginTransaction();
            objServicesAssign.AMCANo = txtAssignTaskNo.Text;
            objServicesAssign.AMCAId = gvAMCAssignDetails.SelectedRow.Cells[0].Text;
            objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
            objServicesAssign.AMCAAssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objServicesAssign.AMCADueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objServicesAssign.AMCADate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objServicesAssign.AMCARemarks = txtRemarksForAssingn.Text;
            objServicesAssign.AMCAStatus = "New";
            MessageBox.Show(this, objServicesAssign.AMCAssignment_Update());
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblAssignTasks.Visible = false;
            gvAMCAssignDetails.DataBind();
            ClearControls();
            Services.Dispose();
        }
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
        txtAssignTaskNo.Text = "";
        txtAssignDate.Text = "";
        txtCustomerEmailForAssingn.Text = "";
        txtCustomerNameForAssingn.Text = "";
        txtDueDate.Text = "";
        txtEmpEmailId.Text = "";
        txtEnquiryDateForAssign.Text = "";
        txtEnquiryNoForAssign.Text = "";
        txtRemarksForAssingn.Text = "";
        ddlEmpNameForAssign.SelectedValue = "0";
    }
    protected void gvAMCAssignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}
