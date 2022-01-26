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

public partial class Modules_SM_SalesAssignments : basePage
{
    
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            Yantra.Authentication.Privilege_Check(this);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            //lblCPID.Text = cp.getPresentCompanySessionValue();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            gvEnqAssignDetails.DataBind();
            EnquiryMode_Fill();
            CustomerMaster_Fill();
            EmployeeMaster_Fill();
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "7");
        btnDelete.Enabled = up.Delete;
        //btnFollowUp.Enabled = up.FollowUp;
        btnSendQuotation.Enabled = up.add;
        //btnViewEnquiry.Enabled = up.ViewEnquiry;
        btnFollowUpSave.Enabled = up.add;
        btnViewEnquiry.Enabled = up.Approve;
        //btnFollowUpRefresh.Enabled = up.FollowUpRefresh;
        //btnFollowUpHistory.Enabled = up.FollowUpHistory;
        //btnFollowUpClose.Enabled = up.FollowUpClose;

    }

    #region lbtnEnquiryNo Click
    protected void lbtnEnquiryNo_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
        LinkButton lbtnEnquiryNo;
        lbtnEnquiryNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnquiryNo.Parent.Parent;
        gvEnqAssignDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to Delete this Record?');");
        lblAssignTaskIdHiddenForFollowUp.Text = gvRow.Cells[0].Text;
        ExistingRecordFill(sender, e);

        btnFollowUp.Visible = true;
        btnSendQuotation.Visible = true;
        btnViewEnquiry.Visible = true;

        SM.SalesAssignments objSM = new SM.SalesAssignments();

        if (objSM.SalesAssignments_Select(gvEnqAssignDetails.SelectedRow.Cells[1].Text) > 0)
        {

            tblAssignmentDetails.Visible = true;

            txtEnquiryNo.Text = objSM.EnqNo;
            txtEnquiryDate.Text = objSM.EnqDate;
            txtCustomerName.Text = gvRow.Cells[4].Text;
            //txtAssignedTo.Text = gvRow.Cells[7].Text;
            txtAssignedTo.Text = "EDP Department";
            txtAssignedDate.Text = objSM.AssingDate;
            txtDueDate.Text = objSM.DueDate;
            txtStatus.Text = gvRow.Cells[8].Text;
            txtRemarks.Text = objSM.AssignRemarks;
            //txtEnquiryNo.Text = gvRow.Cells[2].Text;
            //txtEnquiryDate.Text = gvRow.Cells[3].Text;
            //txtCustomerName.Text = gvRow.Cells[4].Text;
            //txtAssignedDate.Text = gvRow.Cells[5].Text;
            //txtDueDate.Text = gvRow.Cells[6].Text;
            //txtAssignedTo.Text = gvRow.Cells[7].Text;
            //txtStatus.Text = gvRow.Cells[8].Text;



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
        if (ddlSearchBy.SelectedItem.Text == "Enquiry Date" || ddlSearchBy.SelectedItem.Text == "Assign Date" || ddlSearchBy.SelectedItem.Text == "Due Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;

        }
        else
        {
            txtSearchText.Visible = true;
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;

    }
    #endregion

    #region ddlSymbols_SelectedIndexChanged
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvEnqAssignDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy.SelectedItem.Text == "Enquiry Date" || ddlSearchBy.SelectedItem.Text == "Assign Date" || ddlSearchBy.SelectedItem.Text == "Due Date")
        {

            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }

        gvEnqAssignDetails.DataBind();
    }
    #endregion

    #region Enquiry Assignments FollowUp

    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            SM.SalesAssignments objSMAssign = new SM.SalesAssignments();
            SM.BeginTransaction();
            objSMAssign.AssignTaskId = gvEnqAssignDetails.SelectedRow.Cells[0].Text;
        //    objSMAssign.FollowUpEmpId = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            objSMAssign.FollowUpEmpId = ddlFollowEmpName.SelectedItem.Value;

            objSMAssign.FollowUpDesc = txtFollowUpDesc.Text;
            objSMAssign.FollowUpDate = DateTime.Now.ToString("MM/dd/yyyy");
            MessageBox.Show(this, objSMAssign.SalesAssignmentsFollowUp_Save());
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFollowUp.DataBind();
            txtFollowUpDesc.Text = string.Empty;
            SM.Dispose();
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
            //txtFollowUpName.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpName];
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
            Response.Redirect("SalesQuotationDetails1.aspx?enqid=" + gvEnqAssignDetails.SelectedRow.Cells[1].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    //////////////////////////////////////////////////////////////////////

    #region EnqiryMode Fill
    private void EnquiryMode_Fill()
    {
        try
        {
            Masters.EnquiryMode.EnquiryMode_Select(ddlEnquirySource);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomer);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlOriginatedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlFollowEmpName);
            // HR.EmployeeMaster.EmployeeMaster_Select(ddlFollowEmpName);
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

    #region ddlEnquirySource_SelectedIndexChanged
    protected void ddlEnquirySource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  SM.SalesEnquiry objSMSalesEnquiry = new SM.SalesEnquiry();

            if (ddlEnquirySource.SelectedItem.Text == "Tender")
            {
                //tblTenderDetails.Visible = true;
                TenderDetailsShowHide(true);
                lblReferenceCode.Text = "Tender No";
                lblEnquiryDueDate.Text = "Tender Due Date";
            }
            else
            {
                //tblTenderDetails.Visible = false;
                TenderDetailsShowHide(false);
                lblReferenceCode.Text = "Reference Code";
                lblEnquiryDueDate.Text = "Enquiry Due Date";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
        }
    }
    #endregion

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
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
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
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

    #region TenderDetailsShowHide
    private void TenderDetailsShowHide(bool showhide)
    {
        tdSubmissionTime1.Visible = tdSubmissionTime2.Visible = showhide;
        tdOpeningDate1.Visible = tdOpeningDate2.Visible = showhide;
        tdOpeningTime1.Visible = tdOpeningTime2.Visible = showhide;
        tdTenderDate1.Visible = tdTenderDate2.Visible = showhide;
    }
    #endregion

    #region gvInterestedProducts_RowDataBound
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlEnquirySource.SelectedItem.Text != "Tender")
        {
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = false;
        }
    }
    #endregion

    private void ExistingRecordFill(object sender, EventArgs e)
    {
        if (gvEnqAssignDetails.SelectedIndex > -1)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                if (objSM.SalesEnquiry_Select(gvEnqAssignDetails.SelectedRow.Cells[1].Text) > 0)
                {
                    txtSalesEnqNo.Text = objSM.EnqNo;
                    txtSalesEnqDate.Text = objSM.EnqDate;
                    txtTenderDate.Text = objSM.EnqTenderDate;
                    ddlCustomer.SelectedValue = objSM.CustId;
                    ddlEnquirySource.SelectedValue = objSM.EnqModeId;
                    ddlEnquirySource_SelectedIndexChanged(sender, e);
                    //ddlOriginatedBy.SelectedIndex = ddlOriginatedBy.Items.IndexOf(ddlOriginatedBy.Items.FindByText(objSM.EnqOrigName));
                    ddlOriginatedBy.SelectedValue = objSM.EnqOrigName;
                    txtReferenceCode.Text = objSM.EnqRef;
                    txtFollowUpCriteria.Text = objSM.EnqFollowUp;
                    txtPromotionType.Text = objSM.PromotionType;
                    txtPromotionActivity.Text = objSM.PromotionActivity;
                    txtEnquiryDueDate.Text = objSM.EnqDueDate;
                    txtDescription.Text = objSM.EnqDesc;
                    txtSubmissionTime.Text = objSM.EnqSubTime;
                    txtOpeningDate.Text = objSM.EnqOpeningDate;
                    txtOpeningTime.Text = objSM.EnqOpeningTime;

                    objSM.SalesEnquiryDetails_Select(gvEnqAssignDetails.SelectedRow.Cells[1].Text, gvInterestedProducts);

                    ddlCustomer_SelectedIndexChanged(sender, e);
                    if (ddlUnitName.Items.Count > 1)
                    {
                        ddlUnitName.SelectedValue = objSM.CustUnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                        ddlContactPerson.SelectedValue = objSM.CustDetId;
                        ddlContactPerson_SelectedIndexChanged(sender, e);
                    }
                    ////if (objSM.CustUnitId == "0")
                    ////{
                    ////    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    ////    if ((objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value)) > 0)
                    ////    {
                    ////        txtContactPerson.Visible = true;
                    ////        ddlContactPerson.Visible = false;
                    ////        rfvContactPerson.Enabled = false;
                    ////        rfvUnitName.Enabled = false;
                    ////        lblUnitAddress.Text = "Customer Address";

                    ////        txtRegion.Text = objSMCustomer.RegName;
                    ////        txtIndustryType.Text = objSMCustomer.IndType;
                    ////        txtContactPerson.Text = objSMCustomer.ContactPerson;
                    ////        txtRegion.Text = objSMCustomer.RegName;
                    ////        txtIndustryType.Text = objSMCustomer.IndType;
                    ////        txtUnitAddress.Text = objSMCustomer.Address;
                    ////        txtEmail.Text = objSMCustomer.Email;
                    ////        txtPhoneNo.Text = objSMCustomer.Phone;
                    ////        txtMobile.Text = objSMCustomer.Mobile;
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);
                    ////    txtContactPerson.Visible = false;
                    ////    ddlContactPerson.Visible = true;
                    ////    rfvContactPerson.Enabled = true;
                    ////    rfvUnitName.Enabled = true;

                    ////    txtRegion.Text = objSMCustomer.RegName;
                    ////    txtIndustryType.Text = objSMCustomer.IndType;
                    ////}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

    protected void btnViewEnquiry_Click(object sender, EventArgs e)
    {
        if (tblSalesEnquiry.Visible == true)
        {
            tblSalesEnquiry.Visible = false;
        }
        else if (tblSalesEnquiry.Visible == false)
        {
            tblSalesEnquiry.Visible = true;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvEnqAssignDetails.SelectedIndex > -1)
        {
            if (gvEnqAssignDetails.SelectedRow.Cells[8].Text != "Closed")
            {
                try
                {
                    SM.SalesAssignments objSM = new SM.SalesAssignments();
                    SM.BeginTransaction();
                    MessageBox.Show(this, objSM.SalesAssignments_Delete(gvEnqAssignDetails.SelectedRow.Cells[0].Text));
                    SM.CommitTransaction();
                }
                catch (Exception ex)
                {
                    SM.RollBackTransaction();
                    MessageBox.Show(this, ex.Message);
                }
                finally
                {
                    tblAssignmentDetails.Visible = false;
                    btnDelete.Attributes.Clear();
                    gvEnqAssignDetails.DataBind();
                    SM.ClearControls(this);
                    SM.Dispose();
                }
            }
            else { MessageBox.Show(this, "This Record is Closed... Closed will not be Deleted"); }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvEnqAssignDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvEnqAssignDetails.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gvEnqAssignDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
        }
        catch(Exception ex)
        { }

    }
}

 
