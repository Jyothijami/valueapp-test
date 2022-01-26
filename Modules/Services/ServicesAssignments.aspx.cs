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
using vllib;
public partial class Modules_Services_ServicesAssignments : basePage
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Yantra.Authentication.Privilege_Check(this);
            lblEmpIdHidden.Text = lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            
            EmployeeMaster_Fill();
            CustomerMaster_Fill();

        }
    }
    #endregion

    #region lbtnEnquiryNo Click
    protected void lbtnEnquiryNo_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
        LinkButton lbtnEnquiryNo;
        lbtnEnquiryNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnquiryNo.Parent.Parent;
        gvEnqAssignDetails.SelectedIndex = gvRow.RowIndex;
        lblAssignTaskIdHiddenForFollowUp.Text = gvRow.Cells[0].Text;

        btnFollowUp.Visible = false;
        btnSendQuotation.Visible = true;
       // btnSparesQuotation.Visible = true;
        //btnSparesQuotation.Visible = true;
        btnViewComplaint.Visible = true;
        btnServiceRe.Visible = true;

        Services.ServicesAssignments objSM = new Services.ServicesAssignments();

        if (objSM.ServicesAssignments_Select(gvEnqAssignDetails.SelectedRow.Cells[1].Text) > 0)
        {
            tblAssignmentDetails.Visible = true;

            txtEnquiryNo.Text = objSM.CrNo;
            txtEnquiryDate.Text = objSM.CrDate;
            txtCustomerName.Text = gvRow.Cells[5].Text;
            txtAssignedTo.Text = gvRow.Cells[8].Text;
            txtAssignedDate.Text = objSM.AssingDate;
            txtDueDate.Text = objSM.DueDate;
            txtStatus.Text = gvRow.Cells[9].Text;
            txtCallType.Text = gvRow.Cells[4].Text;


            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

            if (objComplaintRegister.ComplaintRegister_Select(gvEnqAssignDetails.SelectedRow.Cells[1].Text) > 0)
            {
                txtCRNo.Text = objComplaintRegister.CRNo;
                txtCRDate.Text = objComplaintRegister.CRDate;
                ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
                ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
                txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
                txtRootCause.Text = objComplaintRegister.CRRootCause;
                txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
                ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;

                objComplaintRegister.ComplaintRegisterDetails_Select(gvEnqAssignDetails.SelectedRow.Cells[1].Text, gvQuotationItems);
            }

        }
    }
    #endregion

    #region gvEnquiry Assigned Tasks_RowDataBound
    protected void gvEnqAssignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

            e.Row.Cells[1].Visible = false;
        }
    }
    #endregion

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "CR Date" || ddlSearchBy.SelectedItem.Text == "Assign Date" || ddlSearchBy.SelectedItem.Text == "Due Date")
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
        gvEnqAssignDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvEnqAssignDetails.DataBind();
    }
    #endregion

    #region Enquiry Assignments FollowUp

    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            Services.ServicesAssignments objSMAssign = new Services.ServicesAssignments();
            Services.BeginTransaction();
            objSMAssign.AssignTaskId = gvEnqAssignDetails.SelectedRow.Cells[0].Text;
            objSMAssign.FollowUpEmpId = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            objSMAssign.FollowUpDesc = txtFollowUpDesc.Text;
            objSMAssign.FollowUpDate = DateTime.Now.ToString();
            MessageBox.Show(this, objSMAssign.ServicesAssignmentsFollowUp_Save());
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
        if (gvEnqAssignDetails.SelectedIndex > -1)
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


    #region Button SEND QUOTATION Click
    protected void btnSendQuotation_Click(object sender, EventArgs e)
    {
        if (gvEnqAssignDetails.SelectedIndex > -1)
        {
            if (txtCallType.Text == "Non Warranty")
            {
                //MessageBox.Show(this, "U Can Not Set a AMC Quotation For Call Type Non-Warranty ");
                Response.Redirect("AMCQuotation.aspx?crid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
            }
            else if (txtCallType.Text == "Warranty" || txtCallType.Text == "Installation")
            {
                //MessageBox.Show(this, "U Can Not Set a Quotation For Call Type Warranty and Installation");
                Response.Redirect("AMCQuotation.aspx?crid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
            }
            else if (txtCallType.Text == "AMC")
            {
                Response.Redirect("AMCQuotation.aspx?crid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
            }

            else
            {
                MessageBox.Show(this, "Please Select atleast a Record ");
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlCustomerName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region   Contact Fill
    private void Contact_Fill()
    {
        try
        {
            if (ddlCustomerName.SelectedIndex != -1)
            {
                SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlCustomerName.SelectedItem.Value);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Unit Name Fill
    private void UnitName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Visible = false;
        }
    }
    #endregion

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                lblUnitAddress.Text = "Customer Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
                {
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                    txtUnitAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtPhoneNo.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
            }
            SM.Dispose();

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtPhoneNo.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }

        //try
        //{
        //    SM.CustomerMaster objCustUnits = new SM.CustomerMaster();
        //    if (objCustUnits.CustomerMasterUnitsDetailsEnquiry_Select(ddlUnitName.SelectedItem.Value) > 0)
        //    {
        //        txtUnitAddress.Text = objCustUnits.Address;

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{
        //    SM.Dispose();
        //}
    }
    #endregion

    #region ddlContactPerson_SelectedIndexChanged
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtEmail.Text = objSMCustomer.CustCorpEmail;
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
                txtMobile.Text = objSMCustomer.CustCorpMobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion


    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
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

    protected void btnViewComplaint_Click(object sender, EventArgs e)
    {
        if (tblComplaintRegister.Visible == true)
        {
            tblComplaintRegister.Visible = false;
        }
        else if (tblComplaintRegister.Visible == false)
        {
            tblComplaintRegister.Visible = true;
        }
    }

    protected void btnSparesQuotation_Click(object sender, EventArgs e)
    {
        if (gvEnqAssignDetails.SelectedIndex > -1)
        {
            if (txtCallType.Text == "Non Warranty")
            {
                Response.Redirect("SparesQuotation.aspx?crid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
            }
            else if (txtCallType.Text == "Warranty" || txtCallType.Text == "Installation")
            {
                //MessageBox.Show(this, "U Can Not Set a Quotation For Call Type Warranty and Installation");
                Response.Redirect("SparesQuotation.aspx?crid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
            }
            else if (txtCallType.Text == "AMC")
            {
                //MessageBox.Show(this, "U Can Not Set a Spares Quotation For Call Type AMC");
                Response.Redirect("SparesQuotation.aspx?crid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
            }

            else
            {
                MessageBox.Show(this, "Please Select atleast a Record ");
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }

    }





    protected void btnWarantytab_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = "CR_CALL_TYPE";
        lblSearchValueHidden.Text = "Warranty";
        gvEnqAssignDetails.DataBind();
    }
    protected void btnInstaltab_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = "CR_CALL_TYPE";
        lblSearchValueHidden.Text = "Installation";
        gvEnqAssignDetails.DataBind();
    }
    protected void BtnAmc_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = "CR_CALL_TYPE";
        lblSearchValueHidden.Text = "AMC";
        gvEnqAssignDetails.DataBind();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        if (gvEnqAssignDetails.SelectedIndex > -1)
        {
            Response.Redirect("ServiceReport.aspx?SOId=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvEnqAssignDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvEnqAssignDetails.DataBind();
    }
}

 
