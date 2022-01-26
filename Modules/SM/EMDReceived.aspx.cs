
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

public partial class Modules_SM_EMDReceived : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerMaster_Fill();
            //TenderNo_Fill();
            EmployeeMaster_Fill();
            ///    SalesInvoiceNo_Fill();
            tblEmdReceived.Visible = false;
        }
    }
    #endregion

    #region DropDownsReset
    private void DropDownsReset()
    {

        ddlTenderNo.Items.Clear();
        ddlUnitName.Items.Clear();
        ddlTenderNo.Items.Add(new ListItem("--", "0"));
        ddlTenderNo.Items.Add(new ListItem("-- Select Unit Name --", "0"));
        ddlUnitName.Items.Add(new ListItem("--", "0"));
        ddlUnitName.Items.Add(new ListItem("-- Select Customer. --", "0"));

    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.TenderCustomerMaster_Select(ddlCustomer);
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
            SM.CustomerMaster.TenderCustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);
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
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);

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

    #region TenderNo Fill
    private void TenderNo_Fill()
    {
        try
        {
            //  SM.SalesEnquiry.SalesEnquiry_Select(ddlTenderNo);
            SM.SalesEnquiry.SalesEnquiryTenderNo_Select(ddlTenderNo, ddlCustomer.SelectedItem.Value, ddlUnitName.SelectedItem.Value, btnSave.Text);
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

    #region LINK Button  EMDSReceivedNo Click
    protected void lbtnEMDSReceivedNo_Click(object sender, EventArgs e)
    {
        tblEmdReceived.Visible = false;
        LinkButton lbtnEMDSReceivedNo;
        lbtnEMDSReceivedNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEMDSReceivedNo.Parent.Parent;
        gvEMDSReceived.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            SM.EMDSReceived objEMDSReceived = new SM.EMDSReceived();
            if (objEMDSReceived.EMDSReceived_Select(gvEMDSReceived.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblEmdReceived.Visible = true;

                lblEMRDIDHidden.Text = objEMDSReceived.EMDRId;
                txtEMDRNo.Text = objEMDSReceived.EMDRNo;
                txtEMDRDate.Text = objEMDSReceived.EMDRDate;
                ddlCustomer.SelectedValue = objEMDSReceived.CustId;
                ddlCustomer_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objEMDSReceived.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlTenderNo.SelectedValue = objEMDSReceived.EnqId;
                ddlTenderNo_SelectedIndexChanged(sender, e);
                txtEmdCharges.Text = objEMDSReceived.EnqEMDCharges;
                txtAmtReceived.Text = objEMDSReceived.EMDRAmtReceived;
                ddlPaymentMode.SelectedValue = objEMDSReceived.EMDRPaymodeType;
                ddlPaymentMode_SelectedIndexChanged(sender, e);
                txtDDChequeNo.Text = objEMDSReceived.EMDRChequeNo;
                txtDDChequeDate.Text = objEMDSReceived.EMDRChequeDate;
                txtCashReceivedOn.Text = objEMDSReceived.EMDRCahReceivedDate;
                txtBankDetails.Text = objEMDSReceived.EMDRBankDetails;
                ddlPreparedBy.SelectedValue = objEMDSReceived.EMDRPreparedBy;
                ddlApprovedBy.SelectedValue = objEMDSReceived.EMDRApprovedBy;
                txtRemarks.Text = objEMDSReceived.EMDRRemarks;
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
    #endregion

    #region PAGE EMDRERENDER
    protected void Page_EMDReRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            btnRefresh.Visible = true;
        }
        else if (btnSave.Text == "Update")
        {
            btnRefresh.Visible = false;
        }
    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
        DropDownsReset();
        txtEMDRNo.Text = SM.EMDSReceived.EMDSReceived_AutoGenCode();
        txtEMDRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblEmdReceived.Visible = true;
        gvPreviousPayments.DataBind();
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string amt = txtEmdCharges.Text;
        foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
        {
            if (btnSave.Text == "Update")
            {
                if (gvrow.Cells[0].Text != lblEMRDIDHidden.Text)
                {
                    amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                }
            }
            else
            {
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
            }
        }
        amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
        if (Convert.ToDecimal(amt) < 0)
        {
            MessageBox.Show(this, "Amount Received should not exceed more than Balance Amount");
            return;
        }
        else
        {
            if (btnSave.Text == "Save")
            {
                EMDSReceivedSave();
            }
            else if (btnSave.Text == "Update")
            {
                EMDSReceivedUpdate();
            }
        }
        DropDownsReset();
    }
    #endregion

    #region EMDS Received Save
    private void EMDSReceivedSave()
    {
        try
        {
            SM.EMDSReceived objEMDSReceived = new SM.EMDSReceived();
            SM.BeginTransaction();

            objEMDSReceived.EMDRNo = txtEMDRNo.Text;
            objEMDSReceived.EMDRDate = Yantra.Classes.General.toMMDDYYYY(txtEMDRDate.Text);
            objEMDSReceived.EnqRef = ddlTenderNo.SelectedItem.Text;
            objEMDSReceived.EnqId = ddlTenderNo.SelectedItem.Value;
            objEMDSReceived.EmdrStatus = "";
            objEMDSReceived.EnqEMDCharges = txtEmdCharges.Text;
            if (txtAmtReceived.Text == string.Empty)
            {
                objEMDSReceived.EMDRAmtReceived = "0";
            }
            else
            {
                objEMDSReceived.EMDRAmtReceived = txtAmtReceived.Text;
            }
            //objEMDSReceived.EMDRAmtReceived = txtAmtReceived.Text;
            objEMDSReceived.EMDRPaymodeType = ddlPaymentMode.SelectedItem.Value;
            objEMDSReceived.EMDRChequeNo = txtDDChequeNo.Text;
            objEMDSReceived.EMDRChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objEMDSReceived.EMDRCahReceivedDate = Yantra.Classes.General.toMMDDYYYY(txtCashReceivedOn.Text);
            objEMDSReceived.EMDRBankDetails = txtBankDetails.Text;
            objEMDSReceived.EMDRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objEMDSReceived.EMDRApprovedBy = "0";
            objEMDSReceived.EMDRRemarks = txtRemarks.Text;


            if (objEMDSReceived.EMDSReceived_Save() == "Data Saved Successfully")
            {
                string amt = txtEmdCharges.Text;
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                }
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
                if (Convert.ToDecimal(amt) > 0)
                {
                    objEMDSReceived.EMDSStatus_Update("Pending", objEMDSReceived.EnqId);
                }
                else
                {
                    objEMDSReceived.EMDSStatus_Update("Cleared", objEMDSReceived.EnqId);
                }
                SM.CommitTransaction();
                MessageBox.Show(this, "Data Saved Successfully");
            }
            else
            {
                SM.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvEMDSReceived.DataBind();
            tblEmdReceived.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }

    }
    #endregion

    #region EMDS Received Update
    private void EMDSReceivedUpdate()
    {

        try
        {
            SM.EMDSReceived objEMDSReceived = new SM.EMDSReceived();
            SM.BeginTransaction();

            objEMDSReceived.EMDRId = gvEMDSReceived.SelectedRow.Cells[0].Text;

            objEMDSReceived.EMDRNo = txtEMDRNo.Text;
            objEMDSReceived.EMDRDate = Yantra.Classes.General.toMMDDYYYY(txtEMDRDate.Text);
            objEMDSReceived.EnqRef = ddlTenderNo.SelectedItem.Text;
            objEMDSReceived.EnqId = ddlTenderNo.SelectedItem.Value;
            objEMDSReceived.EnqEMDCharges = txtEmdCharges.Text;
            objEMDSReceived.EMDRAmtReceived = txtAmtReceived.Text;
            objEMDSReceived.EMDRPaymodeType = ddlPaymentMode.SelectedItem.Value;
            objEMDSReceived.EMDRChequeNo = txtDDChequeNo.Text;
            objEMDSReceived.EMDRChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objEMDSReceived.EMDRCahReceivedDate = Yantra.Classes.General.toMMDDYYYY(txtCashReceivedOn.Text);
            objEMDSReceived.EMDRBankDetails = txtBankDetails.Text;
            objEMDSReceived.EMDRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objEMDSReceived.EMDRApprovedBy = "0";
            objEMDSReceived.EMDRRemarks = txtRemarks.Text;


            if (objEMDSReceived.EMDSReceived_Update() == "Data Updated Successfully")
            {
                string amt = txtEmdCharges.Text;
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    if (gvrow.Cells[0].Text != lblEMRDIDHidden.Text)
                    {
                        amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                    }
                }
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
                if (Convert.ToDecimal(amt) > 0)
                {
                    objEMDSReceived.EMDSStatus_Update("Pending", objEMDSReceived.EnqId);
                }
                else
                {
                    objEMDSReceived.EMDSStatus_Update("Cleared", objEMDSReceived.EnqId);
                }
                SM.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
            }
            else
            {
                SM.RollBackTransaction();
            }
            SM.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvEMDSReceived.DataBind();
            tblEmdReceived.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }


    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblEmdReceived.Visible = true;

        if (gvEMDSReceived.SelectedIndex > -1)
        {
            try
            {
                SM.EMDSReceived objEMDSReceived = new SM.EMDSReceived();
                if (objEMDSReceived.EMDSReceived_Select(gvEMDSReceived.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblEmdReceived.Visible = true;

                    lblEMRDIDHidden.Text = objEMDSReceived.EMDRId;
                    txtEMDRNo.Text = objEMDSReceived.EMDRNo;
                    txtEMDRDate.Text = objEMDSReceived.EMDRDate;
                    ddlCustomer.SelectedValue = objEMDSReceived.CustId;
                    ddlCustomer_SelectedIndexChanged(sender, e);
                    ddlUnitName.SelectedValue = objEMDSReceived.CustUnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
                    ddlTenderNo.SelectedValue = objEMDSReceived.EnqId;
                    ddlTenderNo_SelectedIndexChanged(sender, e);
                    txtEmdCharges.Text = objEMDSReceived.EnqEMDCharges;
                    txtAmtReceived.Text = objEMDSReceived.EMDRAmtReceived;
                    ddlPaymentMode.SelectedValue = objEMDSReceived.EMDRPaymodeType;
                    ddlPaymentMode_SelectedIndexChanged(sender, e);
                    txtDDChequeNo.Text = objEMDSReceived.EMDRChequeNo;
                    txtDDChequeDate.Text = objEMDSReceived.EMDRChequeDate;
                    txtCashReceivedOn.Text = objEMDSReceived.EMDRCahReceivedDate;
                    txtBankDetails.Text = objEMDSReceived.EMDRBankDetails;
                    ddlPreparedBy.SelectedValue = objEMDSReceived.EMDRPreparedBy;
                    ddlApprovedBy.SelectedValue = objEMDSReceived.EMDRApprovedBy;
                    txtRemarks.Text = objEMDSReceived.EMDRRemarks;
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
            MessageBox.Show(this, "Please select  a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvEMDSReceived.SelectedIndex > -1)
        {
            try
            {
                SM.EMDSReceived objEMDSReceived = new SM.EMDSReceived();

                MessageBox.Show(this, objEMDSReceived.EMDSReceived_Delete(gvEMDSReceived.SelectedRow.Cells[0].Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvEMDSReceived.DataBind();
                gvPreviousPayments.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select a Record");
        }
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);

    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblEmdReceived.Visible = false;

    }
    #endregion

    #region ddlPaymentMode_SelectedIndexChanged
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedItem.Text == "Cheque" || ddlPaymentMode.SelectedItem.Text == "DD")
        {
            txtDDChequeNo.Visible = true;
            txtDDChequeDate.Visible = true;
            imgDDChequeDate.Visible = true;
            lblDDChequeNo.Visible = true;
            lblDDChequeDate.Visible = true;
            lblBankDetails.Visible = true;
            txtBankDetails.Visible = true;

            txtCashReceivedOn.Visible = false;
            imgCashReceivedOn.Visible = false;
            lblCashReceivedOn.Visible = false;

        }
        else if (ddlPaymentMode.SelectedItem.Text == "Cash")
        {
            txtCashReceivedOn.Visible = true;
            imgCashReceivedOn.Visible = true;
            lblCashReceivedOn.Visible = true;

            txtDDChequeNo.Visible = false;
            txtDDChequeDate.Visible = false;
            imgDDChequeDate.Visible = false;
            lblDDChequeNo.Visible = false;
            lblDDChequeDate.Visible = false;
            lblBankDetails.Visible = false;
            txtBankDetails.Visible = false;

        }
        else
        {
            txtDDChequeNo.Visible = false;
            txtDDChequeDate.Visible = false;
            imgDDChequeDate.Visible = false;
            lblDDChequeNo.Visible = false;
            lblDDChequeDate.Visible = false;
            lblBankDetails.Visible = false;
            txtBankDetails.Visible = false;


            txtCashReceivedOn.Visible = false;
            imgCashReceivedOn.Visible = false;
            lblCashReceivedOn.Visible = false;

        }

    }
    #endregion

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitName_Fill();
    }
    #endregion

    #region ddlTenderNo_SelectedIndexChanged
    protected void ddlTenderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //////////// THIS APPROACH IS TOTALLY WRONG... QUOTATION RECORD SHOULD GET ONLY WITH QUOTATION ID(QUOT_ID) AND NOT WITH ENQUIRY ID . HERE TENDER SELECTED VALUE IS ENQUIRY ID
            ////////////SM.SalesQuotation objsalesqto = new SM.SalesQuotation();
            ////////////if (objsalesqto.SalesQuotation_Select(ddlTenderNo.SelectedItem.Value) > 0)
            ////////////{
            ////////////    txtTenderDate.Text = objsalesqto.QuotDate;
            ////////////    //txtEmdCharges.Text=objsalesqto.
            ////////////    txtDD.Text = objsalesqto.QuotDDNo;
            ////////////    txtDDDate.Text = objsalesqto.QuotDDDate;
            ////////////    txtTenderBankDetails.Text = objsalesqto.QuotBankName;
            ////////////    SM.SalesEnquiry objSalesEnquiry = new SM.SalesEnquiry();
            ////////////    objSalesEnquiry.SalesEnquiryDetails_Select(ddlTenderNo.SelectedItem.Value, gvPreviousPayments);
            ////////////    txtEmdCharges.Text = objSalesEnquiry.EnqEMDCharges;
            ////////////    txtBalanceAmount.Text = txtEmdCharges.Text;
            ////////////    SM.EMDSReceived OBJEMDREC = new SM.EMDSReceived();
            ////////////    OBJEMDREC.ExistingEMDSReceived_Select(gvPreviousPayments, ddlTenderNo.SelectedItem.Value);
            ////////////}
            //////////// THIS APPROACH IS TOTALLY WRONG... QUOTATION RECORD SHOULD GET ONLY WITH QUOTATION ID(QUOT_ID) AND NOT WITH ENQUIRY ID . HERE TENDER SELECTED VALUE IS ENQUIRY ID

            SM.SalesQuotation objTender = new SM.SalesQuotation();
            if (objTender.TenderWithQuotationRaised_Select(ddlTenderNo.SelectedItem.Value) > 0)
            {
                txtTenderDate.Text = objTender.EnqDate;
                txtEmdCharges.Text = objTender.QuotTotalEMDCharges;
                txtDD.Text = objTender.QuotDDNo;
                txtDDDate.Text = objTender.QuotDDDate;
                txtTenderBankDetails.Text = objTender.QuotBankName;
                txtBalanceAmount.Text = txtEmdCharges.Text;
                SM.EMDSReceived OBJEMDREC = new SM.EMDSReceived();
                OBJEMDREC.ExistingEMDSReceived_Select(gvPreviousPayments, ddlTenderNo.SelectedItem.Value);
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

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objCustUnits = new SM.CustomerMaster();
            if (objCustUnits.CustomerUnits_Select(ddlUnitName.SelectedItem.Value) > 0)
            {
                txtUnitAddress.Text = objCustUnits.CustUnitAddress;
                TenderNo_Fill();
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

    #region gvEMDSReceived_RowDataBound
    protected void gvEMDSReceived_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Emdr  Date")
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

    #region DropDownList Symbols Select Index Changed
    protected void ddlSymbols_SelectedIndexChanged1(object sender, EventArgs e)
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

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvEMDSReceived.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvEMDSReceived.DataBind();
    }
    #endregion

    #region gvPreviousPayments_RowDataBound
    protected void gvPreviousPayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(e.Row.Cells[5].Text));
        }
    }
    #endregion




   

   
}

 
