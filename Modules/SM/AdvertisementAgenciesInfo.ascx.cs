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


public partial class Modules_SM_AdvertisementAgenciesInfo : System.Web.UI.UserControl
{
    #region pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EmployeeMaster_Fill();
            AdvertisingMode_Fill();
            AdvertisingAgency_Fill();
            AdvertisinfMagzines_Fill();
            SIzeOfAdvertising_Fill();


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

    #region  AdvertisingMode Fill
    private void AdvertisingMode_Fill()
    {
        try
        {

            SM.AdvertisingMode.AdvertisingMode_Select(ddlAdvertisingMode);
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

    #region  AdvertisingAgency Fill
    private void AdvertisingAgency_Fill()
    {
        try
        {

            SM.AdvertisingAgency.AdvertisingAgency_Select(ddlAdvertisingAgency);
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

    #region AdvertisinfMagzines Fill
    private void AdvertisinfMagzines_Fill()
    {
        try
        {

            SM.AdvertisingMagzines.AdvertisingMagzines_Select(ddlMagzines);
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

    #region  SIzeOfAdvertising Fill
    private void SIzeOfAdvertising_Fill()
    {
        try
        {

            SM.SizeOfAdvertising.SizeOfAdvertising_Select(ddlSizeOfAdvertisement);
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

        txtAdvertisingNo.Text = SM.AdvertisingAgencyInformation.AdvertisingAgencyInformation_AutoGenCode();
        txtAdvertisingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblAdvertiseInfo.Visible = true;
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            AdvertisingInformationSave();
        }
        else if (btnSave.Text == "Update")
        {
            AdvertisingInformationUpdate();
        }
    }
    #endregion

    #region AdvertisingInformation Save
    private void AdvertisingInformationSave()
    {
        try
        {
            SM.AdvertisingAgencyInformation objadinfo = new SM.AdvertisingAgencyInformation();
            SM.BeginTransaction();

            objadinfo.AAINo = txtAdvertisingNo.Text;
            objadinfo.AAIDate = Yantra.Classes.General.toMMDDYYYY(txtAdvertisingDate.Text);
            objadinfo.AdvmId = ddlAdvertisingMode.SelectedItem.Value;
            objadinfo.AaId = ddlAdvertisingAgency.SelectedItem.Value;
            objadinfo.AmId = ddlMagzines.SelectedItem.Value;
            objadinfo.AAIOrgName = txtOrganization.Text;
            objadinfo.AAISubscriptionDate = Yantra.Classes.General.toMMDDYYYY(txtSubscriptionDate.Text);
            objadinfo.AAIEventName = txtEventName.Text;
            objadinfo.AAIEventOnDate = Yantra.Classes.General.toMMDDYYYY(txtOnDate.Text);
            objadinfo.AAIEventTillDate = Yantra.Classes.General.toMMDDYYYY(txtTillDate.Text);
            objadinfo.AAISponsorshipMode = txtSponsorship.Text;
            objadinfo.AAISponsorshipDate = Yantra.Classes.General.toMMDDYYYY(txtSponsorDate.Text);
            if (rbApproved.Checked == true)
            {
                objadinfo.AAIAdvertisement = rbApproved.Text;
            }
            else
            {
                objadinfo.AAIAdvertisement = rbNotApproved.Text;



            }
            objadinfo.AAIAdvtApprovedDate = Yantra.Classes.General.toMMDDYYYY(txtApprovedDate.Text);
            objadinfo.SaId = ddlSizeOfAdvertisement.SelectedItem.Value;
            objadinfo.AAIAdvtPublishingDate = Yantra.Classes.General.toMMDDYYYY(txtPublishDate.Text);
            objadinfo.AAIInvoiceNo = txtInvoiceNo.Text;
            objadinfo.AAIInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
            //objadinfo.AAIInvoiceAmount = txtInvoiceAmount.Text;

            if (txtInvoiceAmount.Text == string.Empty)
            {
                objadinfo.AAIInvoiceAmount = "0";
            }
            else
            {
                objadinfo.AAIInvoiceAmount = txtInvoiceAmount.Text;
            }
            objadinfo.AAIPayMode = ddlPaymentMode.SelectedItem.Value;
            //objadinfo.AAIAdvanceGiven = txtAdvance.Text;
            if (txtAdvance.Text == string.Empty)
            {
                objadinfo.AAIAdvanceGiven = "0";
            }
            else
            {
                objadinfo.AAIAdvanceGiven = txtAdvance.Text;
            }
            objadinfo.AAIPaymentDate = Yantra.Classes.General.toMMDDYYYY(txtPaymentDate.Text);
            objadinfo.AAIChequeNo = txtDDChequeNo.Text;
            objadinfo.AAIChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objadinfo.AAIBankDetails = txtBankDetails.Text;
            if (txtBalance.Text == string.Empty)
            {
                objadinfo.AAIBalnceAmount = "0";
            }
            else
            {
                objadinfo.AAIBalnceAmount = txtBalance.Text;
            }

            //objadinfo.AAIBalnceAmount = txtBalance.Text;
            objadinfo.AAIFullPayMode = ddlModeOfPayment.SelectedItem.Value;
            objadinfo.AAIFullPaymentDate = Yantra.Classes.General.toMMDDYYYY(txtDateOfPayment.Text);
            //objadinfo.AAIFullAmountPaid = txtAmountPaid.Text;
            if (txtAmountPaid.Text ==string.Empty)
            {
                objadinfo.AAIFullAmountPaid = "0";
            }
            else
            {
                objadinfo.AAIFullAmountPaid = txtAmountPaid.Text;
            }
          
            objadinfo.AAIFullChequeNo = txtDDNO.Text;
            objadinfo.AAIFullChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
            objadinfo.AAIFullBankDetails = txtDetailsOfBank.Text;
            //objadinfo.aaipr
            objadinfo.AdvertisingAgencyInformation_Save();

            SM.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");

        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvAdvertisingInfo.DataBind();
            tblAdvertiseInfo.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }

    }
    #endregion

    #region AdvertisingInformation Update
    private void AdvertisingInformationUpdate()
    {

        try
        {
            SM.AdvertisingAgencyInformation objadinfo = new SM.AdvertisingAgencyInformation();
            SM.BeginTransaction();
            objadinfo.AAIId = gvAdvertisingInfo.SelectedRow.Cells[0].Text;
            objadinfo.AAINo = txtAdvertisingNo.Text;
            objadinfo.AAIDate = Yantra.Classes.General.toMMDDYYYY(txtAdvertisingDate.Text);
            objadinfo.AdvmId = ddlAdvertisingMode.SelectedItem.Value;
            objadinfo.AaId = ddlAdvertisingAgency.SelectedItem.Value;
            objadinfo.AmId = ddlMagzines.SelectedItem.Value;
            objadinfo.AAIOrgName = txtOrganization.Text;
            objadinfo.AAISubscriptionDate = Yantra.Classes.General.toMMDDYYYY(txtSubscriptionDate.Text);
            objadinfo.AAIEventName = txtEventName.Text;
            objadinfo.AAIEventOnDate = Yantra.Classes.General.toMMDDYYYY(txtOnDate.Text);
            objadinfo.AAIEventTillDate = Yantra.Classes.General.toMMDDYYYY(txtTillDate.Text);
            objadinfo.AAISponsorshipMode = txtSponsorship.Text;
            objadinfo.AAISponsorshipDate = Yantra.Classes.General.toMMDDYYYY(txtSponsorDate.Text);
            if (rbApproved.Checked == true)
            {
                objadinfo.AAIAdvertisement = rbApproved.Text;
            }
            else
            {
                objadinfo.AAIAdvertisement = rbNotApproved.Text;



            }
            objadinfo.AAIAdvtApprovedDate = Yantra.Classes.General.toMMDDYYYY(txtApprovedDate.Text);
            objadinfo.SaId = ddlSizeOfAdvertisement.SelectedItem.Value;
            objadinfo.AAIAdvtPublishingDate = Yantra.Classes.General.toMMDDYYYY(txtPublishDate.Text);
            objadinfo.AAIInvoiceNo = txtInvoiceNo.Text;
            objadinfo.AAIInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
            //objadinfo.AAIInvoiceAmount = txtInvoiceAmount.Text;

            if (txtInvoiceAmount.Text == string.Empty)
            {
                objadinfo.AAIInvoiceAmount = "0";
            }
            else
            {
                objadinfo.AAIInvoiceAmount = txtInvoiceAmount.Text;
            }
            objadinfo.AAIPayMode = ddlPaymentMode.SelectedItem.Value;
            //objadinfo.AAIAdvanceGiven = txtAdvance.Text;
            if (txtAdvance.Text == string.Empty)
            {
                objadinfo.AAIAdvanceGiven = "0";
            }
            else
            {
                objadinfo.AAIAdvanceGiven = txtAdvance.Text;
            }
            objadinfo.AAIPaymentDate = Yantra.Classes.General.toMMDDYYYY(txtPaymentDate.Text);
            objadinfo.AAIChequeNo = txtDDChequeNo.Text;
            objadinfo.AAIChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objadinfo.AAIBankDetails = txtBankDetails.Text;
            if (txtBalance.Text == string.Empty)
            {
                objadinfo.AAIBalnceAmount = "0";
            }
            else
            {
                objadinfo.AAIBalnceAmount = txtBalance.Text;
            }

            //objadinfo.AAIBalnceAmount = txtBalance.Text;
            objadinfo.AAIFullPayMode = ddlModeOfPayment.SelectedItem.Value;
            objadinfo.AAIFullPaymentDate = Yantra.Classes.General.toMMDDYYYY(txtDateOfPayment.Text);
            //objadinfo.AAIFullAmountPaid = txtAmountPaid.Text;
            if (txtAmountPaid.Text == string.Empty)
            {
                objadinfo.AAIFullAmountPaid = "0";
            }
            else
            {
                objadinfo.AAIFullAmountPaid = txtAmountPaid.Text;
            }

            objadinfo.AAIFullChequeNo = txtDDNO.Text;
            objadinfo.AAIFullChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
            objadinfo.AAIFullBankDetails = txtDetailsOfBank.Text;
            //objadinfo.aaipr
            objadinfo.AdvertisingAgencyInformation_Update();

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
            gvAdvertisingInfo.DataBind();
            tblAdvertiseInfo.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }


    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblAdvertiseInfo.Visible = true;

        if (gvAdvertisingInfo.SelectedIndex > -1)
        {
            try
            {
                SM.AdvertisingAgencyInformation objadinfo = new SM.AdvertisingAgencyInformation();
                SM.BeginTransaction();


                if (objadinfo.AdvertisingAgencyInformation_Select(gvAdvertisingInfo.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    btnInvoice.Enabled = true;
                    tblAdvertiseInfo.Visible = true;
                    tblApproved.Visible = true;
                    tblInvoiceDetails.Visible = true;
                    tblAdvanceDetails.Visible = true;
                    tblPaymentDetails.Visible = true;
                    

                    txtAdvertisingNo.Text = objadinfo.AAINo;
                    txtAdvertisingDate.Text = objadinfo.AAIDate;
                    ddlAdvertisingMode.SelectedValue = objadinfo.AdvmId;
                    ddlAdvertisingAgency.SelectedValue = objadinfo.AaId;
                    ddlMagzines.SelectedValue = objadinfo.AmId;
                    txtOrganization.Text = objadinfo.AAIOrgName;
                    txtSubscriptionDate.Text = objadinfo.AAISubscriptionDate;
                    txtEventName.Text = objadinfo.AAIEventName;
                    txtOnDate.Text = objadinfo.AAIEventOnDate;
                    txtTillDate.Text = objadinfo.AAIEventTillDate;
                    txtSponsorship.Text = objadinfo.AAISponsorshipMode;
                    txtSponsorDate.Text = objadinfo.AAISponsorshipDate;
                    if (objadinfo.AAIAdvertisement == "Approved")
                    {
                        rbApproved.Checked = true;
                        rbNotApproved.Checked = false;
                    }
                    else
                    {
                        rbNotApproved.Checked = true;
                        rbApproved.Checked = false;
                    }
                    if (objadinfo.AAIInvoiceAmount == "0")
                    {
                        txtInvoiceAmount.Text = string.Empty;

                    }
                    else
                    {

                        txtInvoiceAmount.Text = objadinfo.AAIInvoiceAmount;
                    }
                    txtApprovedDate.Text = objadinfo.AAIAdvtApprovedDate;
                    ddlSizeOfAdvertisement.SelectedValue = objadinfo.SaId;
                    txtPublishDate.Text = objadinfo.AAIAdvtPublishingDate;
                    txtInvoiceNo.Text = objadinfo.AAIInvoiceNo;
                    txtInvoiceDate.Text = objadinfo.AAIInvoiceDate;
                    //txtInvoiceAmount.Text = objadinfo.AAIInvoiceAmount;
                   
                    ddlPaymentMode.SelectedValue = objadinfo.AAIPayMode;
                    txtAdvance.Text = objadinfo.AAIAdvanceGiven;
                    txtPaymentDate.Text = objadinfo.AAIPaymentDate;
                    txtDDChequeNo.Text = objadinfo.AAIChequeNo;
                    txtDDChequeDate.Text = objadinfo.AAIChequeDate;
                    txtBankDetails.Text = objadinfo.AAIBankDetails;
                    ddlModeOfPayment.SelectedValue = objadinfo.AAIFullPayMode;
                    txtDateOfPayment.Text = objadinfo.AAIFullPaymentDate;
                    txtAmountPaid.Text = objadinfo.AAIFullAmountPaid;
                    txtBalance.Text = objadinfo.AAIBalnceAmount;
                    txtDDNO.Text = objadinfo.AAIFullChequeNo;
                    txtDDDate.Text = objadinfo.AAIFullChequeDate;
                    txtDetailsOfBank.Text = objadinfo.AAIFullBankDetails;
                    if (ddlPaymentMode.SelectedItem.Text == "Cheque" || ddlPaymentMode.SelectedItem.Text == "DD" || ddlPaymentMode.SelectedItem.Text == "Cash")
                    {
                        tblChekDetails.Visible = true;
                    }
                    else
                    {
                        tblChekDetails.Visible = false;
                    }
                    if (ddlModeOfPayment.SelectedItem.Text == "Cheque" || ddlModeOfPayment.SelectedItem.Text == "DD" || ddlModeOfPayment.SelectedItem.Text == "Cash")
                    {

                        tblCheque.Visible = true;
                    }
                    else
                    {
                        tblCheque.Visible = false;

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

                SM.Dispose();

                ddlPaymentMode_SelectedIndexChanged(sender, e);
                ddlModeOfPayment_SelectedIndexChanged(sender, e);
                ddlAdvertisingMode_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion


    #region LINK Button  SizeOfAdvertising Click
    protected void lbtnSizeOfAdvertising_Click(object sender, EventArgs e)
    {
        tblAdvertiseInfo.Visible = false;
        LinkButton lbtnSizeOfAdvertising;
        lbtnSizeOfAdvertising = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSizeOfAdvertising.Parent.Parent;
        gvAdvertisingInfo.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            SM.AdvertisingAgencyInformation objadinfo = new SM.AdvertisingAgencyInformation();
            SM.BeginTransaction();


            if (objadinfo.AdvertisingAgencyInformation_Select(gvAdvertisingInfo.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                btnInvoice.Enabled = false;

                tblAdvertiseInfo.Visible = true;
                tblApproved.Visible = true;
                tblInvoiceDetails.Visible = true;
                tblAdvanceDetails.Visible = true;
                tblPaymentDetails.Visible = true;
                txtAdvertisingNo.Text = objadinfo.AAINo;
                txtAdvertisingDate.Text = objadinfo.AAIDate;
                ddlAdvertisingMode.SelectedValue = objadinfo.AdvmId;
                ddlAdvertisingAgency.SelectedValue = objadinfo.AaId;
                ddlMagzines.SelectedValue = objadinfo.AmId;
                txtOrganization.Text = objadinfo.AAIOrgName;
                txtSubscriptionDate.Text = objadinfo.AAISubscriptionDate;
                txtEventName.Text = objadinfo.AAIEventName;
                txtOnDate.Text = objadinfo.AAIEventOnDate;
                txtTillDate.Text = objadinfo.AAIEventTillDate;
                txtSponsorship.Text = objadinfo.AAISponsorshipMode;
                txtSponsorDate.Text = objadinfo.AAISponsorshipDate;
                if (objadinfo.AAIAdvertisement == "Approved")
                {
                    rbApproved.Checked = true;
                    rbNotApproved.Checked = false;
                }
                else
                {
                    rbNotApproved.Checked = true;
                    rbApproved.Checked = false;
                }
                if (objadinfo.AAIInvoiceAmount == "0")
                {
                    txtInvoiceAmount.Text = string.Empty;

                }
                else
                {

                    txtInvoiceAmount.Text = objadinfo.AAIInvoiceAmount;
                }
                txtApprovedDate.Text = objadinfo.AAIAdvtApprovedDate;
                ddlSizeOfAdvertisement.SelectedValue = objadinfo.SaId;
                txtPublishDate.Text = objadinfo.AAIAdvtPublishingDate;
                txtInvoiceNo.Text = objadinfo.AAIInvoiceNo;
                txtInvoiceDate.Text = objadinfo.AAIInvoiceDate;
                //txtInvoiceAmount.Text = objadinfo.AAIInvoiceAmount;

                ddlPaymentMode.SelectedValue = objadinfo.AAIPayMode;
                txtAdvance.Text = objadinfo.AAIAdvanceGiven;
                txtPaymentDate.Text = objadinfo.AAIPaymentDate;
                txtDDChequeNo.Text = objadinfo.AAIChequeNo;
                txtDDChequeDate.Text = objadinfo.AAIChequeDate;
                txtBankDetails.Text = objadinfo.AAIBankDetails;
                ddlModeOfPayment.SelectedValue = objadinfo.AAIFullPayMode;
                txtDateOfPayment.Text = objadinfo.AAIFullPaymentDate;
                txtAmountPaid.Text = objadinfo.AAIFullAmountPaid;
                txtBalance.Text = objadinfo.AAIBalnceAmount;
                txtDDNO.Text = objadinfo.AAIFullChequeNo;
                txtDDDate.Text = objadinfo.AAIFullChequeDate;
                txtDetailsOfBank.Text = objadinfo.AAIFullBankDetails;
                if (ddlPaymentMode.SelectedItem.Text == "Cheque" || ddlPaymentMode.SelectedItem.Text == "DD" || ddlPaymentMode.SelectedItem.Text == "Cash")
                {
                    tblChekDetails.Visible = true;
                }
                else
                {
                    tblChekDetails.Visible = false;
                }
                if (ddlModeOfPayment.SelectedItem.Text == "Cheque" || ddlModeOfPayment.SelectedItem.Text == "DD" || ddlModeOfPayment.SelectedItem.Text == "Cash")
                {

                    tblCheque.Visible = true;
                }
                else
                {
                    tblCheque.Visible = false;

                }


            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            SM.Dispose();

            ddlPaymentMode_SelectedIndexChanged(sender, e);
            ddlModeOfPayment_SelectedIndexChanged(sender, e);
            ddlAdvertisingMode_SelectedIndexChanged(sender, e);
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvAdvertisingInfo.SelectedIndex > -1)
        {
            try
            {
                SM.AdvertisingAgencyInformation objadinfo = new SM.AdvertisingAgencyInformation();

                MessageBox.Show(this, objadinfo.AdvertisingAgencyInformation_Delete(gvAdvertisingInfo.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvAdvertisingInfo.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region gvAdvertisingInfo
    protected void gvAdvertisingInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region ddlPaymentMode_SelectedIndexChanged
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedItem.Text == "Cheque" || ddlPaymentMode.SelectedItem.Text == "DD")
        {
            tblChekDetails.Visible = true;
            txtAdvance.Visible = false;
            txtPaymentDate.Visible = false;
            lblAdvance.Visible = false;
            lblPayment.Visible = false;
            imgPaymentDate.Visible = false;
        }
        else if (ddlPaymentMode.SelectedItem.Text == "Cash")
        {
            txtAdvance.Visible = true;
            txtPaymentDate.Visible = true;
            tblChekDetails.Visible = false;
            lblAdvance.Visible = true;
            lblPayment.Visible = true;
            imgPaymentDate.Visible = true;

        }
        else
        {
            txtAdvance.Visible = false;
            txtPaymentDate.Visible = false;
            tblChekDetails.Visible = false;
            lblAdvance.Visible = false;
            lblPayment.Visible = false;
            imgPaymentDate.Visible = false;

        }

    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Advertising Date")
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
        gvAdvertisingInfo.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvAdvertisingInfo.DataBind();
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
        tblAdvertiseInfo.Visible = false;

    }
    #endregion

    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        tblInvoiceDetails.Visible = true;
        lblInvoiceNo.Visible = true;
        txtInvoiceNo.Visible = true;
        txtInvoiceDate.Visible = true;
        lblInvoiceDate.Visible = true;
        imgInvoiceDate.Visible = true;
        lblInvoiceAmount.Visible = true;
        txtInvoiceAmount.Visible = true;
        btnAdvance.Visible = true;
        btnFullPayment.Visible = true;

        btnSave.Visible = true;
        btnRefresh.Visible = true;
        btnClose.Visible = true;
    }

    protected void btnAdvance_Click(object sender, EventArgs e)
    {
        tblAdvanceDetails.Visible = true;
        btnSave.Visible = true;
        btnRefresh.Visible = true;
        btnClose.Visible = true;
    }
    protected void btnFullPayment_Click(object sender, EventArgs e)
    {
        tblAdvanceDetails.Visible = false;
        tblPaymentDetails.Visible = true;
        btnSave.Visible = true;
        btnRefresh.Visible = true;
        btnClose.Visible = true;
    }

    protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlModeOfPayment.SelectedItem.Text == "Cheque" || ddlModeOfPayment.SelectedItem.Text == "DD")
        {
            tblCheque.Visible = true;
            txtAmountPaid.Visible = false;
            txtDateOfPayment.Visible = false;
            lblPaid.Visible = false;
            lblDateOfPayment.Visible = false;
            imgDateOfPayment.Visible = false;
            lblBalance.Visible = false;
            txtBalance.Visible = false;
           
        }
        else if (ddlModeOfPayment.SelectedItem.Text == "Cash")
        {
            txtAmountPaid.Visible = true;
            txtDateOfPayment.Visible = true;
            lblPaid.Visible = true;
            lblDateOfPayment.Visible = true;
            imgDateOfPayment.Visible = true;
            lblBalance.Visible = true;
            txtBalance.Visible = true;
            tblCheque.Visible = false;

        }
        else
        {
            txtAmountPaid.Visible = false;
            txtDateOfPayment.Visible = false;
            tblCheque.Visible = false;
            lblPaid.Visible = false;
            lblDateOfPayment.Visible = false;
            imgDateOfPayment.Visible = false;
            lblBalance.Visible = false;
            txtBalance.Visible = false;
        }
    }



    protected void rbApproved_CheckedChanged(object sender, EventArgs e)
    {
        tblApproved.Visible = true;
    }
    protected void rbNotApproved_CheckedChanged(object sender, EventArgs e)
    {
        tblApproved.Visible = false;
    }
    protected void ddlAdvertisingMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdvertisingMode.SelectedItem.Text == "Subscription")
        {
            txtSubscriptionDate.Visible = true;
            lblSubscription.Visible = true;
            imgSubscriptionDate.Visible = true;
            lblEventName.Visible = false;
            txtEventName.Visible = false;
            lblOnDate.Visible = false;
            txtOnDate.Visible = false;
            imgOnDate.Visible = false;
            lblTillDate.Visible = false;
            txtTillDate.Visible = false;
            imgTillDate.Visible = false;
            tblApproved.Visible = false;
            lblName.Visible = false;
            txtOrganization.Visible = false;
            lblSponsor.Visible = false;
            txtSponsorship.Visible = false;
            lblSponsorshipDate.Visible = false;
            txtSponsorDate.Visible = false;
            imgSponsorDate.Visible = false;
        }
        else if (ddlAdvertisingMode.SelectedItem.Text == "Publication")
        {
            tblApproved.Visible = true;

            lblName.Visible = false;
            txtOrganization.Visible = false;
            lblSponsor.Visible = false;
            txtSponsorship.Visible = false;
            lblSponsorshipDate.Visible = false;
            txtSponsorDate.Visible = false;
            imgSponsorDate.Visible = false;
            txtSubscriptionDate.Visible = false;
            lblSubscription.Visible = false;
            imgSubscriptionDate.Visible = false;
            lblEventName.Visible = false;
            txtEventName.Visible = false;
            lblOnDate.Visible = false;
            txtOnDate.Visible = false;
            imgOnDate.Visible = false;
            lblTillDate.Visible = false;
            txtTillDate.Visible = false;
            imgTillDate.Visible = false;
        }
        else if (ddlAdvertisingMode.SelectedItem.Text == "Exhibition")
        {

            lblEventName.Visible = true;
            txtEventName.Visible = true;
            lblOnDate.Visible = true;
            txtOnDate.Visible = true;
            imgOnDate.Visible = true;
            lblTillDate.Visible = true;
            txtTillDate.Visible = true;
            imgTillDate.Visible = true;
            tblApproved.Visible = false;
            lblName.Visible = false;
            txtOrganization.Visible = false;
            lblSponsor.Visible = false;
            txtSponsorship.Visible = false;
            lblSponsorshipDate.Visible = false;
            txtSponsorDate.Visible = false;
            imgSponsorDate.Visible = false;
            txtSubscriptionDate.Visible = false;
            lblSubscription.Visible = false;
            imgSubscriptionDate.Visible = false;



        }

        else if (ddlAdvertisingMode.SelectedItem.Text == "Sponsorship")
        {

            txtOrganization.Visible = true;
            lblEventName.Visible = true;
            txtEventName.Visible = true;
            lblOnDate.Visible = true;
            txtOnDate.Visible = true;
            imgOnDate.Visible = true;
            lblTillDate.Visible = true;
            txtTillDate.Visible = true;
            imgTillDate.Visible = true;
            lblSponsor.Visible = true;
            txtSponsorship.Visible = true;
            lblSponsorshipDate.Visible = true;
            txtSponsorDate.Visible = true;
            imgSponsorDate.Visible = true;
            txtSubscriptionDate.Visible = false;
            lblSubscription.Visible = false;
            imgSubscriptionDate.Visible = false;
            tblApproved.Visible = false;
            ddlAdvertisingAgency.Visible = false;
            lblAgency.Visible = false;

        }
    }



   

    
}







