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

public partial class Modules_SM_FEOrderProfile : System.Web.UI.Page
{
    decimal exworksvalue, excisedutyvalue, educessvalue, seceducessvalue, cstvalue, totalvalue;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerMaster_Fill();
            ItemName_Fill();
            //SalesOrder_Fill();
            EmployeeMaster_Fill();
            //SalesInvoiceNo_Fill();
            tblOrderProfile.Visible = false;


        }
        txtExWorksValue.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtExciseDuty.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtPacking.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtCST.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtEduCess.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtSecEduCess.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtStampingCharges.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtDiscountedValue.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtFOBCharges.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtCIFCharges.Attributes.Add("onkeyup", "javascript:totalvaluecalc();");
        txtTotalPriceTotalHidden.Style.Add("visibility", "hidden");

        TotalItemValue();
        if (txtDiscountedValue.Text == "" || txtDiscountedValue.Text == string.Empty) { txtDiscountedValue.Text = "0"; }
        if (txtTotalPriceTotalHidden.Text == "" || txtTotalPriceTotalHidden.Text == string.Empty) { txtTotalPriceTotalHidden.Text = "0"; }
        if (txtExciseDuty.Text == "" || txtExciseDuty.Text == string.Empty) { txtExciseDuty.Text = "0"; }
        if (txtPacking.Text == "" || txtPacking.Text == string.Empty) { txtPacking.Text = "0"; }
        if (txtCST.Text == "" || txtCST.Text == string.Empty) { txtCST.Text = "0"; }
        if (txtEduCess.Text == "" || txtEduCess.Text == string.Empty) { txtEduCess.Text = "0"; }
        if (txtSecEduCess.Text == "" || txtSecEduCess.Text == string.Empty) { txtSecEduCess.Text = "0"; }
        if (txtStampingCharges.Text == "" || txtStampingCharges.Text == string.Empty) { txtStampingCharges.Text = "0"; }
        if (txtFOBCharges.Text == "" || txtFOBCharges.Text == string.Empty) { txtFOBCharges.Text = "0"; }
        if (txtCIFCharges.Text == "" || txtCIFCharges.Text == string.Empty) { txtCIFCharges.Text = "0"; }

        exworksvalue = (decimal.Parse(txtTotalPriceTotalHidden.Text) - decimal.Parse(txtDiscountedValue.Text));
        txtExWorksValue.Text = exworksvalue.ToString();
        excisedutyvalue = (exworksvalue * decimal.Parse(txtExciseDuty.Text) / 100);
        educessvalue = (excisedutyvalue * decimal.Parse(txtEduCess.Text) / 100);
        seceducessvalue = (excisedutyvalue * decimal.Parse(txtSecEduCess.Text) / 100);
        totalvalue = (exworksvalue + decimal.Parse(txtPacking.Text) + excisedutyvalue + educessvalue + seceducessvalue);
        cstvalue = (totalvalue * decimal.Parse(txtCST.Text) / 100);
        txtTotalValue.Text = Convert.ToString(totalvalue + cstvalue + decimal.Parse(txtPacking.Text) + decimal.Parse(txtFOBCharges.Text) + decimal.Parse(txtCIFCharges.Text));
        txtTotalValue.Text = Convert.ToString(decimal.Round(decimal.Parse(txtTotalValue.Text), 0));
    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomer);
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
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);
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

    #region Contact Fill
    private void Contact_Fill()
    {
        try
        {
            //if (ddlCustomer.SelectedIndex != 0)
            //{
            //SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlCustomer.SelectedItem.Value);
            //}
            //else
            //{
            SM.CustomerMaster.CustomerMasterAllDetails_Select(ddlBuyerContactPerson, ddlCustomer.SelectedItem.Value);
            //}
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
            HR.EmployeeMaster.EmployeeMaster_Select(ddlOrderBookedBy);
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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlProductName);
            // SM.SalesQuotation.SalesQuotationItemNames_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType.SelectedItem.Value, ddlProductName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            // SM.Dispose();
        }
    }
    #endregion

    #region DropDownsReset
    private void DropDownsReset()
    {
        // ddlInvoiceNo.Items.Clear();
        // ddlPONo.Items.Clear();
        ddlUnitName.Items.Clear();
        ddlContactPerson.Items.Add(new ListItem("--", "0"));
        ddlContactPerson.Items.Add(new ListItem("-- Select Unit Name --", "0"));
        //   ddlInvoiceNo.Items.Add(new ListItem("--", "0"));
        //   ddlInvoiceNo.Items.Add(new ListItem("-- Select PO No. --", "0"));
        ddlUnitName.Items.Add(new ListItem("--", "0"));
        ddlUnitName.Items.Add(new ListItem("-- Select Customer. --", "0"));
        //  ddlInvoiceNo.Enabled = ddlPONo.Enabled = true;
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {

        TotalItemValue();
        if (txtDiscountedValue.Text == "" || txtDiscountedValue.Text == string.Empty) { txtDiscountedValue.Text = "0"; }
        if (txtTotalPriceTotalHidden.Text == "" || txtTotalPriceTotalHidden.Text == string.Empty) { txtTotalPriceTotalHidden.Text = "0"; }
        if (txtExciseDuty.Text == "" || txtExciseDuty.Text == string.Empty) { txtExciseDuty.Text = "0"; }
        if (txtPacking.Text == "" || txtPacking.Text == string.Empty) { txtPacking.Text = "0"; }
        if (txtCST.Text == "" || txtCST.Text == string.Empty) { txtCST.Text = "0"; }
        if (txtEduCess.Text == "" || txtEduCess.Text == string.Empty) { txtEduCess.Text = "0"; }
        if (txtSecEduCess.Text == "" || txtSecEduCess.Text == string.Empty) { txtSecEduCess.Text = "0"; }
        if (txtStampingCharges.Text == "" || txtStampingCharges.Text == string.Empty) { txtStampingCharges.Text = "0"; }
        if (txtFOBCharges.Text == "" || txtFOBCharges.Text == string.Empty) { txtFOBCharges.Text = "0"; }
        if (txtCIFCharges.Text == "" || txtCIFCharges.Text == string.Empty) { txtCIFCharges.Text = "0"; }

        exworksvalue = (decimal.Parse(txtTotalPriceTotalHidden.Text) - decimal.Parse(txtDiscountedValue.Text));
        txtExWorksValue.Text = exworksvalue.ToString();
        excisedutyvalue = (exworksvalue * decimal.Parse(txtExciseDuty.Text) / 100);
        educessvalue = (excisedutyvalue * decimal.Parse(txtEduCess.Text) / 100);
        seceducessvalue = (excisedutyvalue * decimal.Parse(txtSecEduCess.Text) / 100);
        totalvalue = (exworksvalue + decimal.Parse(txtPacking.Text) + excisedutyvalue + educessvalue + seceducessvalue);
        cstvalue = (totalvalue * decimal.Parse(txtCST.Text) / 100);
        txtTotalValue.Text = Convert.ToString(totalvalue + cstvalue + decimal.Parse(txtPacking.Text) + decimal.Parse(txtFOBCharges.Text) + decimal.Parse(txtCIFCharges.Text));
        txtTotalValue.Text = Convert.ToString(decimal.Round(decimal.Parse(txtTotalValue.Text), 0));

        if (gvFEOrderProfile.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvFEOrderProfile.SelectedRow.Cells[6].Text) && gvFEOrderProfile.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                //btnPrint.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            //btnPrint.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
        }
    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvFEOrderProfile.SelectedIndex = -1;
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        SM.ClearControls(this);
        txtProfileRefNo.Text = SM.ClaimForm.ClaimForm_AutoGenCode();
        txtProfileRefDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        gvProductDetails.DataBind();
        gvBuyerDetails.DataBind();

        tblOrderProfile.Visible = true;
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtFreightCharges.Text == "") { txtFreightCharges.Text = "0"; }
        if (txtFOBCharges.Text == "") { txtFOBCharges.Text = "0"; }
        if (txtCIFCharges.Text == "") { txtCIFCharges.Text = "0"; }
        if (txtTotalItemsValue.Text == "") { txtTotalItemsValue.Text = "0"; }
        if (txtDiscountedValue.Text == "") { txtDiscountedValue.Text = "0"; }
        if (txtExWorksValue.Text == "") { txtExWorksValue.Text = "0"; }
        if (txtPacking.Text == "") { txtPacking.Text = "0"; }
        if (txtExciseDuty.Text == "") { txtExciseDuty.Text = "0"; }
        if (txtEduCess.Text == "") { txtEduCess.Text = "0"; }
        if (txtSecEduCess.Text == "") { txtSecEduCess.Text = "0"; }
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtStampingCharges.Text == "") { txtStampingCharges.Text = "0"; }
        if (txtTotalValue.Text == "") { txtTotalValue.Text = "0"; }
        if (txtDiscountPrice.Text == "") { txtDiscountPrice.Text = "0"; }

        if (btnSave.Text == "Save")
        {
            FEOrderProfileSave();
        }
        else if (btnSave.Text == "Update")
        {
            FEOrderProfileUpdate();
        }
    }
    #endregion

    #region FE Order Profile Save
    private void FEOrderProfileSave()
    {
        try
        {
            SM.FEOrderProfile objFEOrderProfile = new SM.FEOrderProfile();
            SM.BeginTransaction();

            objFEOrderProfile.FEOPNo = txtProfileRefNo.Text;
            objFEOrderProfile.FEOPDate = Yantra.Classes.General.toMMDDYYYY(txtProfileRefDate.Text);
            objFEOrderProfile.CustId = ddlCustomer.SelectedItem.Value;
            objFEOrderProfile.CustUnitId = ddlUnitName.SelectedItem.Value;
            objFEOrderProfile.CustDetId = ddlContactPerson.SelectedItem.Value;
            objFEOrderProfile.FEOPPONo = txtPORefNo.Text;
            objFEOrderProfile.FEOPPODate = Yantra.Classes.General.toMMDDYYYY(txtPORefDate.Text);
            objFEOrderProfile.FEOPMarketing = rbMarketing.Text;
            objFEOrderProfile.FEOPRegion = txtRegion.Text;
            objFEOrderProfile.FEOPTerritory = txtTerritory.Text;
            objFEOrderProfile.FEOPDivision = txtDivison.Text;
            objFEOrderProfile.FEOPMarketSegment = txtMarketSegment.Text;
            objFEOrderProfile.FEOPOrders = rbOrders.Text;
            objFEOrderProfile.FEOPForwarder = txtForwarder.Text;
            objFEOrderProfile.FEOPPortofLanding = txtPort.Text;
            objFEOrderProfile.FEOPFOBCharges = txtFOBCharges.Text;
            objFEOrderProfile.FEOPCIFCharges = txtCIFCharges.Text;
            objFEOrderProfile.FEOPDespatchMode = txtDespatchMode.Text;
            objFEOrderProfile.FEOPECCNo = txtECCNo.Text;
            objFEOrderProfile.FEOPCSTNo = txtCSTNo.Text;
            objFEOrderProfile.FEOPLSTNo = txtLSTNo.Text;
            objFEOrderProfile.FEOPTINNo = txtTINNo.Text;
            objFEOrderProfile.FEOPFrieghtCharges = txtFreightCharges.Text;
            objFEOrderProfile.FEOPOctroi = txtOctroi.Text;
            objFEOrderProfile.FEOPInsurance = txtInsurance.Text;
            objFEOrderProfile.FEOPPatrshipment = rbPartshipment.Text;
            objFEOrderProfile.FEOPRoadPermitReq = rbRoadPermitReq.Text;
            objFEOrderProfile.FEOPARE1Trans = rbARE1Transaction.Text;
            objFEOrderProfile.FEOPEPCGTrans = rbEPCGTransaction.Text;
            objFEOrderProfile.FEOPDocEnclosed = rbDocEnclosed.Text;
            objFEOrderProfile.FEOPDocNo = txtDocumentNo.Text;
            objFEOrderProfile.FEOPTotItemsValue = txtTotalItemsValue.Text;
            objFEOrderProfile.FEOPTotDisValue = txtDiscountedValue.Text;
            objFEOrderProfile.FEOPTotExWorksValue = txtExWorksValue.Text;
            objFEOrderProfile.FEOPPacking = txtPacking.Text;
            objFEOrderProfile.FEOPExciseDuty = txtExciseDuty.Text;
            objFEOrderProfile.FEOPEduCess = txtEduCess.Text;
            objFEOrderProfile.FEOPSecEduCess = txtSecEduCess.Text;
            objFEOrderProfile.FEOPCST = txtCST.Text;
            objFEOrderProfile.FEOPStampingCharges = txtStampingCharges.Text;
            objFEOrderProfile.FEOPTotalValue = txtTotalValue.Text;
            objFEOrderProfile.FEOPDeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objFEOrderProfile.FEOPWarrantyPeriod = txtWarrantyPeriod.Text;
            objFEOrderProfile.FEOPPaymentTerms = txtOtherPayTerms.Text;
            objFEOrderProfile.FEOPAdvRecdDetails = txtAdvRecdDetails.Text;
            objFEOrderProfile.FEOPChequeDD = txtChequePayment.Text;
            objFEOrderProfile.FEOPPostalAddress = txtFullPostalAddress.Text;
            objFEOrderProfile.FEOPContactPerson = txtContactPerson.Text;
            objFEOrderProfile.FEOPTelNo = txtPhoneNo.Text;
            objFEOrderProfile.FEOPMobileNo = txtMobileNo.Text;
            objFEOrderProfile.FEOPSplInstr = txtSplInstr.Text;
            objFEOrderProfile.FEOPOrderBookedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objFEOrderProfile.FEOPApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objFEOrderProfile.FEOPName = txtName.Text;
            objFEOrderProfile.FEOPSignature = txtBillingAddress.Text;
            objFEOrderProfile.FEOPDespatchConsignee = txtDespatchAddress.Text;
            objFEOrderProfile.FEOPPerDisc = txtDiscountPrice.Text;
            objFEOrderProfile.FEOPDocRefNo = txtDocRefNo.Text;
            objFEOrderProfile.FEPortOfDestination = txtPortOfDestination.Text;

            if (objFEOrderProfile.FEOrderProfile_Save() == "Data Saved Successfully")
            {
                objFEOrderProfile.FEOrderProfileProductDetails_Delete(objFEOrderProfile.FEOPId);
                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    objFEOrderProfile.ItemCode = gvrow.Cells[7].Text;
                    objFEOrderProfile.ProdDetQty = gvrow.Cells[3].Text;
                    objFEOrderProfile.ProdDetCurrency = gvrow.Cells[4].Text;
                    objFEOrderProfile.ProdDetUnitPrice = gvrow.Cells[5].Text;
                    objFEOrderProfile.ProdDetTotalPrice = gvrow.Cells[6].Text;

                    objFEOrderProfile.FEOrderProfileProductDetails_Save();
                }

                objFEOrderProfile.FEOrderProfileBuyerDetails_Delete(objFEOrderProfile.FEOPId);

                foreach (GridViewRow gvrow in gvBuyerDetails.Rows)
                {
                    objFEOrderProfile.BuyerDetBuyerType = gvrow.Cells[2].Text;
                    objFEOrderProfile.BuyerDetContactPerson = gvrow.Cells[3].Text;
                    objFEOrderProfile.BuyerDetDesig = gvrow.Cells[4].Text;
                    objFEOrderProfile.BuyerDetTelNo = gvrow.Cells[5].Text;
                    objFEOrderProfile.BuyerDetMobileNo = gvrow.Cells[6].Text;

                    objFEOrderProfile.FEOrderProfileBuyerDetails_Save();
                }

                //   SM.SalesOrder.SalesOrderStatus_Update(SM.SMStatus.Closed, objFEOrderProfile.SOId);

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
            btnDelete.Attributes.Clear();
            gvFEOrderProfile.DataBind();
            gvProductDetails.DataBind();
            gvBuyerDetails.DataBind();

            //tblOrderProfile.Visible = false;
            //SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region FE Order Profile Update
    private void FEOrderProfileUpdate()
    {
        try
        {

            SM.FEOrderProfile objFEOrderProfile = new SM.FEOrderProfile();

            SM.BeginTransaction();

            objFEOrderProfile.FEOPId = gvFEOrderProfile.SelectedRow.Cells[0].Text;

            objFEOrderProfile.FEOPNo = txtProfileRefNo.Text;
            objFEOrderProfile.FEOPDate = Yantra.Classes.General.toMMDDYYYY(txtProfileRefDate.Text);
            objFEOrderProfile.CustId = ddlCustomer.SelectedItem.Value;
            objFEOrderProfile.CustUnitId = ddlUnitName.SelectedItem.Value;
            objFEOrderProfile.CustDetId = ddlContactPerson.SelectedItem.Value;
            objFEOrderProfile.FEOPPONo = txtPORefNo.Text;
            objFEOrderProfile.FEOPPODate = Yantra.Classes.General.toMMDDYYYY(txtPORefDate.Text);
            objFEOrderProfile.FEOPMarketing = rbMarketing.Text;
            objFEOrderProfile.FEOPRegion = txtRegion.Text;
            objFEOrderProfile.FEOPTerritory = txtTerritory.Text;
            objFEOrderProfile.FEOPDivision = txtDivison.Text;
            objFEOrderProfile.FEOPMarketSegment = txtMarketSegment.Text;
            objFEOrderProfile.FEOPOrders = rbOrders.Text;
            objFEOrderProfile.FEOPForwarder = txtForwarder.Text;
            objFEOrderProfile.FEOPPortofLanding = txtPort.Text;
            objFEOrderProfile.FEOPFOBCharges = txtFOBCharges.Text;
            objFEOrderProfile.FEOPCIFCharges = txtCIFCharges.Text;
            objFEOrderProfile.FEOPDespatchMode = txtDespatchMode.Text;
            objFEOrderProfile.FEOPECCNo = txtECCNo.Text;
            objFEOrderProfile.FEOPCSTNo = txtCSTNo.Text;
            objFEOrderProfile.FEOPLSTNo = txtLSTNo.Text;
            objFEOrderProfile.FEOPTINNo = txtTINNo.Text;
            objFEOrderProfile.FEOPFrieghtCharges = txtFreightCharges.Text;
            objFEOrderProfile.FEOPOctroi = txtOctroi.Text;
            objFEOrderProfile.FEOPInsurance = txtInsurance.Text;
            objFEOrderProfile.FEOPPatrshipment = rbPartshipment.Text;
            objFEOrderProfile.FEOPRoadPermitReq = rbRoadPermitReq.Text;
            objFEOrderProfile.FEOPARE1Trans = rbARE1Transaction.Text;
            objFEOrderProfile.FEOPEPCGTrans = rbEPCGTransaction.Text;
            objFEOrderProfile.FEOPDocEnclosed = rbDocEnclosed.Text;
            objFEOrderProfile.FEOPDocNo = txtDocumentNo.Text;
            objFEOrderProfile.FEOPTotItemsValue = txtTotalItemsValue.Text;
            objFEOrderProfile.FEOPTotDisValue = txtDiscountedValue.Text;
            objFEOrderProfile.FEOPTotExWorksValue = txtExWorksValue.Text;
            objFEOrderProfile.FEOPPacking = txtPacking.Text;
            objFEOrderProfile.FEOPExciseDuty = txtExciseDuty.Text;
            objFEOrderProfile.FEOPEduCess = txtEduCess.Text;
            objFEOrderProfile.FEOPSecEduCess = txtSecEduCess.Text;
            objFEOrderProfile.FEOPCST = txtCST.Text;
            objFEOrderProfile.FEOPStampingCharges = txtStampingCharges.Text;
            objFEOrderProfile.FEOPTotalValue = txtTotalValue.Text;
            objFEOrderProfile.FEOPDeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objFEOrderProfile.FEOPWarrantyPeriod = txtWarrantyPeriod.Text;
            objFEOrderProfile.FEOPPaymentTerms = txtOtherPayTerms.Text;
            objFEOrderProfile.FEOPAdvRecdDetails = txtAdvRecdDetails.Text;
            objFEOrderProfile.FEOPChequeDD = txtChequePayment.Text;
            objFEOrderProfile.FEOPPostalAddress = txtFullPostalAddress.Text;
            objFEOrderProfile.FEOPContactPerson = txtContactPerson.Text;
            objFEOrderProfile.FEOPTelNo = txtPhoneNo.Text;
            objFEOrderProfile.FEOPMobileNo = txtMobileNo.Text;
            objFEOrderProfile.FEOPSplInstr = txtSplInstr.Text;
            objFEOrderProfile.FEOPOrderBookedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objFEOrderProfile.FEOPApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objFEOrderProfile.FEOPName = txtName.Text;
            objFEOrderProfile.FEOPSignature = txtBillingAddress.Text;
            objFEOrderProfile.FEOPDespatchConsignee = txtDespatchAddress.Text;
            objFEOrderProfile.FEOPPerDisc = txtDiscountPrice.Text;
            objFEOrderProfile.FEOPDocRefNo = txtDocRefNo.Text;
            objFEOrderProfile.FEPortOfDestination = txtPortOfDestination.Text;

            if (objFEOrderProfile.FEOrderProfile_Update() == "Data Updated Successfully")
            {
                objFEOrderProfile.FEOrderProfileProductDetails_Delete(objFEOrderProfile.FEOPId);

                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    objFEOrderProfile.ItemCode = gvrow.Cells[7].Text;
                    objFEOrderProfile.ProdDetQty = gvrow.Cells[3].Text;
                    objFEOrderProfile.ProdDetCurrency = gvrow.Cells[4].Text;
                    objFEOrderProfile.ProdDetUnitPrice = gvrow.Cells[5].Text;
                    objFEOrderProfile.ProdDetTotalPrice = gvrow.Cells[6].Text;

                    objFEOrderProfile.FEOrderProfileProductDetails_Save();
                }

                objFEOrderProfile.FEOrderProfileBuyerDetails_Delete(objFEOrderProfile.FEOPId);

                foreach (GridViewRow gvrow in gvBuyerDetails.Rows)
                {
                    objFEOrderProfile.BuyerDetBuyerType = gvrow.Cells[2].Text;
                    objFEOrderProfile.BuyerDetContactPerson = gvrow.Cells[3].Text;
                    objFEOrderProfile.BuyerDetDesig = gvrow.Cells[4].Text;
                    objFEOrderProfile.BuyerDetTelNo = gvrow.Cells[5].Text;
                    objFEOrderProfile.BuyerDetMobileNo = gvrow.Cells[6].Text;

                    objFEOrderProfile.FEOrderProfileBuyerDetails_Save();
                }

                SM.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
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
            btnSave.Text = "Save";
            btnDelete.Attributes.Clear();
            gvFEOrderProfile.DataBind();
            gvProductDetails.DataBind();
            gvBuyerDetails.DataBind();

            tblOrderProfile.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvFEOrderProfile.SelectedIndex > -1)
        {
            try
            {
                SM.FEOrderProfile objFEOrderProfile = new SM.FEOrderProfile();

                if (objFEOrderProfile.FEOrderProfile_Select(gvFEOrderProfile.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblOrderProfile.Visible = true;


                    txtProfileRefNo.Text = objFEOrderProfile.FEOPNo;
                    txtProfileRefDate.Text = objFEOrderProfile.FEOPDate;
                    ddlCustomer.SelectedValue = objFEOrderProfile.CustId;
                    ddlCustomer_SelectedIndexChanged(sender, e);
                    ddlUnitName.SelectedValue = objFEOrderProfile.CustUnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
                    ddlContactPerson.SelectedValue = objFEOrderProfile.CustDetId;
                    ddlContactPerson_SelectedIndexChanged(sender, e);
                    txtPORefNo.Text = objFEOrderProfile.FEOPPONo;
                    txtPORefDate.Text = objFEOrderProfile.FEOPPODate;
                    rbMarketing.Text = objFEOrderProfile.FEOPMarketing;
                    txtRegion.Text = objFEOrderProfile.FEOPRegion;
                    txtTerritory.Text = objFEOrderProfile.FEOPTerritory;
                    txtDivison.Text = objFEOrderProfile.FEOPDivision;
                    txtMarketSegment.Text = objFEOrderProfile.FEOPMarketSegment;
                    rbOrders.Text = objFEOrderProfile.FEOPOrders;
                    txtForwarder.Text = objFEOrderProfile.FEOPForwarder;
                    txtPort.Text = objFEOrderProfile.FEOPPortofLanding;
                    txtFOBCharges.Text = objFEOrderProfile.FEOPFOBCharges;
                    txtCIFCharges.Text = objFEOrderProfile.FEOPCIFCharges;
                    txtDespatchMode.Text = objFEOrderProfile.FEOPDespatchMode;
                    txtECCNo.Text = objFEOrderProfile.FEOPECCNo;
                    txtCSTNo.Text = objFEOrderProfile.FEOPCSTNo;
                    txtLSTNo.Text = objFEOrderProfile.FEOPLSTNo;
                    txtTINNo.Text = objFEOrderProfile.FEOPTINNo;
                    txtFreightCharges.Text = objFEOrderProfile.FEOPFrieghtCharges;
                    txtOctroi.Text = objFEOrderProfile.FEOPOctroi;
                    txtInsurance.Text = objFEOrderProfile.FEOPInsurance;
                    rbPartshipment.Text = objFEOrderProfile.FEOPPatrshipment;
                    rbRoadPermitReq.Text = objFEOrderProfile.FEOPRoadPermitReq;
                    rbARE1Transaction.Text = objFEOrderProfile.FEOPARE1Trans;
                    rbEPCGTransaction.Text = objFEOrderProfile.FEOPEPCGTrans;
                    rbDocEnclosed.Text = objFEOrderProfile.FEOPDocEnclosed;
                    txtDocumentNo.Text = objFEOrderProfile.FEOPDocNo;
                    txtTotalItemsValue.Text = objFEOrderProfile.FEOPTotItemsValue;
                    txtDiscountedValue.Text = objFEOrderProfile.FEOPTotDisValue;
                    txtExWorksValue.Text = objFEOrderProfile.FEOPTotExWorksValue;
                    txtPacking.Text = objFEOrderProfile.FEOPPacking;
                    txtExciseDuty.Text = objFEOrderProfile.FEOPExciseDuty;
                    txtEduCess.Text = objFEOrderProfile.FEOPEduCess;
                    txtSecEduCess.Text = objFEOrderProfile.FEOPSecEduCess;
                    txtCST.Text = objFEOrderProfile.FEOPCST;
                    txtStampingCharges.Text = objFEOrderProfile.FEOPStampingCharges;
                    txtTotalValue.Text = objFEOrderProfile.FEOPTotalValue;
                    txtDeliveryDate.Text = objFEOrderProfile.FEOPDeliveryDate;
                    txtWarrantyPeriod.Text = objFEOrderProfile.FEOPWarrantyPeriod;
                    txtOtherPayTerms.Text = objFEOrderProfile.FEOPPaymentTerms;
                    if (txtOtherPayTerms.Text == "CAD" || txtOtherPayTerms.Text == "TT" || txtOtherPayTerms.Text == "LC" || txtOtherPayTerms.Text == "FDD" || txtOtherPayTerms.Text == "WT")
                    {
                        rbPaymentTerms.SelectedValue = txtOtherPayTerms.Text;
                    }
                    else
                    {
                        rbPaymentTerms.SelectedValue = "Others";
                    }
                    rbPaymentTerms_SelectedIndexChanged(sender, e);
                    txtAdvRecdDetails.Text = objFEOrderProfile.FEOPAdvRecdDetails;
                    txtChequePayment.Text = objFEOrderProfile.FEOPChequeDD;
                    txtFullPostalAddress.Text = objFEOrderProfile.FEOPPostalAddress;
                    txtContactPerson.Text = objFEOrderProfile.FEOPContactPerson;
                    txtPhoneNo.Text = objFEOrderProfile.FEOPTelNo;
                    txtMobileNo.Text = objFEOrderProfile.FEOPMobileNo;
                    txtSplInstr.Text = objFEOrderProfile.FEOPSplInstr;
                    ddlOrderBookedBy.SelectedValue = objFEOrderProfile.FEOPOrderBookedBy;
                    ddlApprovedBy.SelectedValue = objFEOrderProfile.FEOPApprovedBy;
                    txtName.Text = objFEOrderProfile.FEOPName;
                    txtBillingAddress.Text = objFEOrderProfile.FEOPSignature;
                    txtDespatchAddress.Text = objFEOrderProfile.FEOPDespatchConsignee;
                    txtDiscountPrice.Text = objFEOrderProfile.FEOPPerDisc;
                    txtDocRefNo.Text = objFEOrderProfile.FEOPDocRefNo;
                    txtPortOfDestination.Text = objFEOrderProfile.FEPortOfDestination;
                    objFEOrderProfile.FEOrderProfileProductDetails_Select(gvFEOrderProfile.SelectedRow.Cells[0].Text, gvProductDetails);
                    objFEOrderProfile.FEOrderProfileBuyerDetails_Select(gvFEOrderProfile.SelectedRow.Cells[0].Text, gvBuyerDetails);

                    //ddlOrderAcceptance_SelectedIndexChanged(sender, e);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                SM.Dispose();
                // ddlOrderAcceptance_SelectedIndexChanged(sender, e);
            }
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
        if (gvFEOrderProfile.SelectedIndex > -1)
        {
            try
            {
                SM.FEOrderProfile objFEOrderProfile = new SM.FEOrderProfile();

                MessageBox.Show(this, objFEOrderProfile.FEOrderProfile_Delete(gvFEOrderProfile.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvFEOrderProfile.DataBind();
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

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
        //if (Request.QueryString["SOId"] != null)
        //{
        //    btnNew_Click(sender, e);
        //    ddlOrderAcceptance.SelectedValue = Request.QueryString["SOId"].ToString();
        //    ddlOrderAcceptance_SelectedIndexChanged(sender, e);
        //    tblOrderProfile.Visible = true;
        //}
    }
    #endregion

    #region Button CLAIM FORM Click
    protected void btnClaim_Click(object sender, EventArgs e)
    {
        if (gvFEOrderProfile.SelectedIndex > -1)
        {
            string pagenavigationstr = "ClaimForm.aspx?id=" + SM.ClaimForm.GetClaimFormId(txtProfileRefNo.Text).ToString() + "";
            Response.Redirect(pagenavigationstr);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblOrderProfile.Visible = false;

    }
    #endregion

    #region Link Button lbtnFEOrderProfile_Click
    protected void lbtnFEOrderProfileNo_Click(object sender, EventArgs e)
    {
        tblOrderProfile.Visible = false;
        LinkButton lbtnFEOrderProfileNo;
        lbtnFEOrderProfileNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnFEOrderProfileNo.Parent.Parent;
        gvFEOrderProfile.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        SM.FEOrderProfile objFEOrderProfile = new SM.FEOrderProfile();


        if (objFEOrderProfile.FEOrderProfile_Select(gvFEOrderProfile.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;

            btnSave.Text = "Update";

            tblOrderProfile.Visible = true;

            txtProfileRefNo.Text = objFEOrderProfile.FEOPNo;
            txtProfileRefDate.Text = objFEOrderProfile.FEOPDate;
            ddlCustomer.SelectedValue = objFEOrderProfile.CustId;
            ddlCustomer_SelectedIndexChanged(sender, e);
            ddlUnitName.SelectedValue = objFEOrderProfile.CustUnitId;
            ddlUnitName_SelectedIndexChanged(sender, e);
            ddlContactPerson.SelectedValue = objFEOrderProfile.CustDetId;
            ddlContactPerson_SelectedIndexChanged(sender, e);
            txtPORefNo.Text = objFEOrderProfile.FEOPPONo;
            txtPORefDate.Text = objFEOrderProfile.FEOPPODate;
            rbMarketing.Text = objFEOrderProfile.FEOPMarketing;
            txtRegion.Text = objFEOrderProfile.FEOPRegion;
            txtTerritory.Text = objFEOrderProfile.FEOPTerritory;
            txtDivison.Text = objFEOrderProfile.FEOPDivision;
            txtMarketSegment.Text = objFEOrderProfile.FEOPMarketSegment;
            rbOrders.Text = objFEOrderProfile.FEOPOrders;
            rbOrders_SelectedIndexChanged(sender, e);
            txtForwarder.Text = objFEOrderProfile.FEOPForwarder;
            txtPort.Text = objFEOrderProfile.FEOPPortofLanding;
            txtFOBCharges.Text = objFEOrderProfile.FEOPFOBCharges;
            txtCIFCharges.Text = objFEOrderProfile.FEOPCIFCharges;
            txtDespatchMode.Text = objFEOrderProfile.FEOPDespatchMode;
            txtECCNo.Text = objFEOrderProfile.FEOPECCNo;
            txtCSTNo.Text = objFEOrderProfile.FEOPCSTNo;
            txtLSTNo.Text = objFEOrderProfile.FEOPLSTNo;
            txtTINNo.Text = objFEOrderProfile.FEOPTINNo;
            txtFreightCharges.Text = objFEOrderProfile.FEOPFrieghtCharges;
            txtOctroi.Text = objFEOrderProfile.FEOPOctroi;
            txtInsurance.Text = objFEOrderProfile.FEOPInsurance;
            rbPartshipment.Text = objFEOrderProfile.FEOPPatrshipment;
            rbRoadPermitReq.Text = objFEOrderProfile.FEOPRoadPermitReq;
            rbARE1Transaction.Text = objFEOrderProfile.FEOPARE1Trans;
            rbEPCGTransaction.Text = objFEOrderProfile.FEOPEPCGTrans;
            rbDocEnclosed.Text = objFEOrderProfile.FEOPDocEnclosed;
            txtDocumentNo.Text = objFEOrderProfile.FEOPDocNo;
            txtTotalItemsValue.Text = objFEOrderProfile.FEOPTotItemsValue;
            txtDiscountedValue.Text = objFEOrderProfile.FEOPTotDisValue;
            txtExWorksValue.Text = objFEOrderProfile.FEOPTotExWorksValue;
            txtPacking.Text = objFEOrderProfile.FEOPPacking;
            txtExciseDuty.Text = objFEOrderProfile.FEOPExciseDuty;
            txtEduCess.Text = objFEOrderProfile.FEOPEduCess;
            txtSecEduCess.Text = objFEOrderProfile.FEOPSecEduCess;
            txtCST.Text = objFEOrderProfile.FEOPCST;
            txtStampingCharges.Text = objFEOrderProfile.FEOPStampingCharges;
            txtTotalValue.Text = objFEOrderProfile.FEOPTotalValue;
            txtDeliveryDate.Text = objFEOrderProfile.FEOPDeliveryDate;
            txtWarrantyPeriod.Text = objFEOrderProfile.FEOPWarrantyPeriod;
            txtOtherPayTerms.Text = objFEOrderProfile.FEOPPaymentTerms;
            if (txtOtherPayTerms.Text == "CAD" || txtOtherPayTerms.Text == "TT" || txtOtherPayTerms.Text == "LC" || txtOtherPayTerms.Text == "FDD" || txtOtherPayTerms.Text == "WT")
            {
                rbPaymentTerms.SelectedValue = txtOtherPayTerms.Text;
            }
            else
            {
                rbPaymentTerms.SelectedValue = "Others";
            }
            rbPaymentTerms_SelectedIndexChanged(sender, e);
            txtAdvRecdDetails.Text = objFEOrderProfile.FEOPAdvRecdDetails;
            txtChequePayment.Text = objFEOrderProfile.FEOPChequeDD;
            txtFullPostalAddress.Text = objFEOrderProfile.FEOPPostalAddress;
            txtContactPerson.Text = objFEOrderProfile.FEOPContactPerson;
            txtPhoneNo.Text = objFEOrderProfile.FEOPTelNo;
            txtMobileNo.Text = objFEOrderProfile.FEOPMobileNo;
            txtSplInstr.Text = objFEOrderProfile.FEOPSplInstr;
            ddlOrderBookedBy.SelectedValue = objFEOrderProfile.FEOPOrderBookedBy;
            ddlApprovedBy.SelectedValue = objFEOrderProfile.FEOPApprovedBy;
            txtName.Text = objFEOrderProfile.FEOPName;
            txtBillingAddress.Text = objFEOrderProfile.FEOPSignature;
            txtDespatchAddress.Text = objFEOrderProfile.FEOPDespatchConsignee;
            txtDiscountPrice.Text = objFEOrderProfile.FEOPPerDisc;
            txtDocRefNo.Text = objFEOrderProfile.FEOPDocRefNo;
            txtPortOfDestination.Text = objFEOrderProfile.FEPortOfDestination;
            objFEOrderProfile.FEOrderProfileProductDetails_Select(gvFEOrderProfile.SelectedRow.Cells[0].Text, gvProductDetails);
            objFEOrderProfile.FEOrderProfileBuyerDetails_Select(gvFEOrderProfile.SelectedRow.Cells[0].Text, gvBuyerDetails);

        }
    }
    #endregion

    #region rbOrders_SelectedIndexChanged
    protected void rbOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbOrders.SelectedItem.Text == "For Indent Orders (FE)")
        {
            tblFE.Visible = true;
            FEShowHide(true);
            tblINR.Visible = false;
            INRShowHide(false);
        }
        else if (rbOrders.SelectedItem.Text == "For Inhouse Orders (INR)")
        {
            tblINR.Visible = true;
            INRShowHide(true);
            tblFE.Visible = false;
            FEShowHide(false);
        }
    }

    private void INRShowHide(bool TrueFalse)
    {
        tdexcise1.Visible = tdexcise2.Visible = TrueFalse;
        tdpack1.Visible = tdpack2.Visible = TrueFalse;
        tdcst1.Visible = tdcst2.Visible = TrueFalse;
        tdeducess1.Visible = tdeducess2.Visible = TrueFalse;
        tdseceducess1.Visible = tdseceducess2.Visible = TrueFalse;
        tdstamp1.Visible = tdstamp2.Visible = TrueFalse;
    }
    private void FEShowHide(bool TrueFalse)
    {
        tdfob1.Visible = tdfob2.Visible = TrueFalse;
        tdcif1.Visible = tdcif2.Visible = TrueFalse;
    }

    #endregion

    #region Button PRODUCT ADD Click
    protected void btnProductsAdd_Click(object sender, EventArgs e)
    {
        DataTable ProductDetails = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("Qty");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("Currency");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("TotalPrice");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("ItemCode");
        ProductDetails.Columns.Add(col);


        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                if (gvProductDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvProductDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = ProductDetails.NewRow();

                        dr["ItemName"] = ddlProductName.SelectedItem.Text;
                        dr["Qty"] = txtProductQty.Text; ;
                        dr["Currency"] = ddlCurrency.SelectedItem.Text;
                        dr["UnitPrice"] = txtProductUnitPrice.Text;
                        dr["TotalPrice"] = txtProductTotalPrice.Text;
                        dr["ItemCode"] = ddlProductName.SelectedItem.Value;
                        ProductDetails.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = ProductDetails.NewRow();
                        dr["ItemName"] = gvrow.Cells[2].Text;
                        dr["Qty"] = gvrow.Cells[3].Text;
                        dr["Currency"] = gvrow.Cells[4].Text;
                        dr["UnitPrice"] = gvrow.Cells[5].Text;
                        dr["TotalPrice"] = gvrow.Cells[6].Text;
                        dr["ItemCode"] = gvrow.Cells[7].Text;

                        ProductDetails.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = ProductDetails.NewRow();
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Qty"] = gvrow.Cells[3].Text;
                    dr["Currency"] = gvrow.Cells[4].Text;
                    dr["UnitPrice"] = gvrow.Cells[5].Text;
                    dr["TotalPrice"] = gvrow.Cells[6].Text;
                    dr["ItemCode"] = gvrow.Cells[7].Text;
                    ProductDetails.Rows.Add(dr);
                }
            }
        }

        if (gvProductDetails.Rows.Count > 0)
        {
            if (gvProductDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlProductName.SelectedItem.Value)
                    {
                        gvProductDetails.DataSource = ProductDetails;
                        gvProductDetails.DataBind();
                        MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvProductDetails.SelectedIndex == -1)
        {
            DataRow drnew = ProductDetails.NewRow();

            drnew["ItemName"] = ddlProductName.SelectedItem.Text;
            drnew["Qty"] = txtProductQty.Text;
            drnew["Currency"] = ddlCurrency.SelectedItem.Text;
            drnew["UnitPrice"] = txtProductUnitPrice.Text;
            drnew["TotalPrice"] = txtProductTotalPrice.Text;
            drnew["ItemCode"] = ddlProductName.SelectedItem.Value;

            ProductDetails.Rows.Add(drnew);
        }
        gvProductDetails.DataSource = ProductDetails;
        gvProductDetails.DataBind();
        gvProductDetails.SelectedIndex = -1;
        btnProductsRefresh_Click(sender, e);
    }
    #endregion

    #region Button PRODUCT REFRESH Click
    protected void btnProductsRefresh_Click(object sender, EventArgs e)
    {
        ddlProductName.SelectedValue = "0";
        txtProductQty.Text = string.Empty;
        ddlCurrency.SelectedValue = "0";
        txtProductUnitPrice.Text = string.Empty; ;
        txtProductTotalPrice.Text = string.Empty; ;
    }
    #endregion

    #region Button BUYER ADD Click
    protected void btnBuyerAdd_Click(object sender, EventArgs e)
    {
        DataTable BuyerDetails = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("BuyerType");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("Designation");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("TelNo");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("MobileNo");
        BuyerDetails.Columns.Add(col);

        if (gvBuyerDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvBuyerDetails.Rows)
            {
                if (gvBuyerDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvBuyerDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = BuyerDetails.NewRow();

                        dr["BuyerType"] = rbBuyerType.SelectedItem.Text;
                        dr["ContactPerson"] = ddlBuyerContactPerson.SelectedItem.Text;
                        dr["Designation"] = txtBuyerDesig.Text;
                        dr["TelNo"] = txtBuyerTeleNo.Text;
                        dr["MobileNo"] = txtBuyerMobileNo.Text;

                        BuyerDetails.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = BuyerDetails.NewRow();
                        dr["BuyerType"] = gvrow.Cells[2].Text;
                        dr["ContactPerson"] = gvrow.Cells[3].Text;
                        dr["Designation"] = gvrow.Cells[4].Text;
                        dr["TelNo"] = gvrow.Cells[5].Text;
                        dr["MobileNo"] = gvrow.Cells[6].Text;


                        BuyerDetails.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = BuyerDetails.NewRow();
                    dr["BuyerType"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["Designation"] = gvrow.Cells[4].Text;
                    dr["TelNo"] = gvrow.Cells[5].Text;
                    dr["MobileNo"] = gvrow.Cells[6].Text;

                    BuyerDetails.Rows.Add(dr);
                }
            }
        }

        if (gvBuyerDetails.Rows.Count > 0)
        {
            if (gvBuyerDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvBuyerDetails.Rows)
                {
                    if (gvrow.Cells[2].Text == rbBuyerType.SelectedItem.Value)
                    {
                        gvBuyerDetails.DataSource = BuyerDetails;
                        gvBuyerDetails.DataBind();
                        MessageBox.Show(this, "The Buyer Type you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvBuyerDetails.SelectedIndex == -1)
        {
            DataRow drnew = BuyerDetails.NewRow();

            drnew["BuyerType"] = rbBuyerType.SelectedItem.Text;
            drnew["ContactPerson"] = ddlBuyerContactPerson.SelectedItem.Text;
            drnew["Designation"] = txtBuyerDesig.Text;
            drnew["TelNo"] = txtBuyerTeleNo.Text;
            drnew["MobileNo"] = txtBuyerMobileNo.Text;

            BuyerDetails.Rows.Add(drnew);
        }
        gvBuyerDetails.DataSource = BuyerDetails;
        gvBuyerDetails.DataBind();
        gvBuyerDetails.SelectedIndex = -1;
        btnBuyerRefresh_Click(sender, e);
    }
    #endregion

    #region Button BUYER REFRESH Click
    protected void btnBuyerRefresh_Click(object sender, EventArgs e)
    {
        ddlBuyerContactPerson.SelectedValue = "0";
        txtBuyerDesig.Text = string.Empty;
        txtBuyerTeleNo.Text = string.Empty; ;
        txtBuyerMobileNo.Text = string.Empty; ;
    }
    #endregion

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UnitName_Fill();
        Contact_Fill();

        SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);
        SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

        if (ddlUnitName.Items.Count > 1)
        {
            txtCustContactPerson.Visible = false;
            ddlContactPerson.Visible = true;

            if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
            {
                txtRegion.Text = objSMCustomer.RegName;
            }
        }
        else
        {
            txtCustContactPerson.Visible = true;
            ddlContactPerson.Visible = false;

            if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
            {
                txtCustContactPerson.Text = objSMCustomer.ContactPerson;
                txtRegion.Text = objSMCustomer.RegName;
                txtPhoneNo.Text = objSMCustomer.Phone;
                txtBillingAddress.Text = objSMCustomer.Address;
            }
        }
        SM.Dispose();
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Contact_Fill();
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);
            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtBillingAddress.Text = objSMCustomer.CustUnitAddress;
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

    #region ddlBuyerContactPerson_SelectedIndexChanged
    protected void ddlBuyerContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region gvProductDetails_RowDataBound
    protected void gvProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                e.Row.Cells[7].Visible = e.Row.Cells[7].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from  Products list?');");
            e.Row.Cells[6].Text = (Convert.ToDecimal(e.Row.Cells[3].Text) * Convert.ToDecimal(e.Row.Cells[5].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TotalItemValue();
        }
    }

    private void TotalItemValue()
    {
        txtExWorksValue.Text = "0";
        txtTotalPriceTotalHidden.Text = "0";
        foreach (GridViewRow gvRow in gvProductDetails.Rows)
        {
            txtTotalPriceTotalHidden.Text = Convert.ToString(decimal.Parse(txtTotalPriceTotalHidden.Text) + decimal.Parse(gvRow.Cells[6].Text));
            if (txtDiscountedValue.Text != "0" || txtDiscountedValue.Text != "")
            {
                txtExWorksValue.Text = Convert.ToString(decimal.Parse(txtTotalPriceTotalHidden.Text) - decimal.Parse(txtDiscountedValue.Text));
            }
        }
    }
    #endregion

    #region gvProductDetails_RowDeleting
    protected void gvProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvProductDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable ProductDetails = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("Qty");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("Currency");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("TotalPrice");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("ItemCode");
        ProductDetails.Columns.Add(col);
        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = ProductDetails.NewRow();
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Qty"] = gvrow.Cells[3].Text;
                    dr["Currency"] = gvrow.Cells[4].Text;
                    dr["UnitPrice"] = gvrow.Cells[5].Text;
                    dr["TotalPrice"] = gvrow.Cells[6].Text;
                    dr["ItemCode"] = gvrow.Cells[7].Text;

                    ProductDetails.Rows.Add(dr);
                }
            }
        }
        gvProductDetails.DataSource = ProductDetails;
        gvProductDetails.DataBind();
    }
    #endregion

    #region gvProductDetails_RowEditing
    protected void gvProductDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable ProductDetails = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("Qty");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("Currency");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("TotalPrice");
        ProductDetails.Columns.Add(col);
        col = new DataColumn("ItemCode");
        ProductDetails.Columns.Add(col);

        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                DataRow dr = ProductDetails.NewRow();
                dr["ItemName"] = gvrow.Cells[2].Text;
                dr["Qty"] = gvrow.Cells[3].Text;
                dr["Currency"] = gvrow.Cells[4].Text;
                dr["UnitPrice"] = gvrow.Cells[5].Text;
                dr["TotalPrice"] = gvrow.Cells[6].Text;
                dr["ItemCode"] = gvrow.Cells[7].Text;


                ProductDetails.Rows.Add(dr);
                if (gvrow.RowIndex == gvProductDetails.Rows[e.NewEditIndex].RowIndex)
                {

                    ddlProductName.SelectedValue = gvrow.Cells[7].Text;
                    txtProductQty.Text = gvrow.Cells[3].Text;
                    ddlCurrency.SelectedValue = gvrow.Cells[4].Text;
                    txtProductUnitPrice.Text = gvrow.Cells[5].Text;
                    txtProductTotalPrice.Text = gvrow.Cells[6].Text;

                    gvProductDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvProductDetails.DataSource = ProductDetails;
        gvProductDetails.DataBind();
    }
    #endregion

    #region gvBuyerDetails_RowDataBound
    protected void gvBuyerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                // e.Row.Cells[7].Visible = e.Row.Cells[7].Visible = false;

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Buyer Type from  Buyer Details list?');");
            //  e.Row.Cells[6].Text = (Convert.ToInt32(e.Row.Cells[3].Text) * Convert.ToInt32(e.Row.Cells[5].Text)).ToString();
        }
    }
    #endregion

    #region gvBuyerDetails_RowDeleting
    protected void gvBuyerDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable BuyerDetails = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("BuyerType");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("Designation");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("TelNo");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("MobileNo");
        BuyerDetails.Columns.Add(col);

        if (gvBuyerDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvBuyerDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = BuyerDetails.NewRow();
                    dr["BuyerType"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["Designation"] = gvrow.Cells[4].Text;
                    dr["TelNo"] = gvrow.Cells[5].Text;
                    dr["MobileNo"] = gvrow.Cells[6].Text;


                    BuyerDetails.Rows.Add(dr);


                }
            }
        }
        gvBuyerDetails.DataSource = BuyerDetails;
        gvBuyerDetails.DataBind();
    }
    #endregion

    #region gvBuyerDetails_RowEditing
    protected void gvBuyerDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable BuyerDetails = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("BuyerType");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("Designation");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("TelNo");
        BuyerDetails.Columns.Add(col);
        col = new DataColumn("MobileNo");
        BuyerDetails.Columns.Add(col);


        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvBuyerDetails.Rows)
            {
                DataRow dr = BuyerDetails.NewRow();
                dr["BuyerType"] = gvrow.Cells[2].Text;
                dr["ContactPerson"] = gvrow.Cells[3].Text;
                dr["Designation"] = gvrow.Cells[4].Text;
                dr["TelNo"] = gvrow.Cells[5].Text;
                dr["MobileNo"] = gvrow.Cells[6].Text;


                BuyerDetails.Rows.Add(dr);

                if (gvrow.RowIndex == gvBuyerDetails.Rows[e.NewEditIndex].RowIndex)
                {

                    rbBuyerType.SelectedValue = gvrow.Cells[2].Text;
                    ddlBuyerContactPerson.SelectedValue = gvrow.Cells[3].Text;
                    txtBuyerDesig.Text = gvrow.Cells[4].Text;
                    txtBuyerTeleNo.Text = gvrow.Cells[5].Text;
                    txtBuyerMobileNo.Text = gvrow.Cells[6].Text;

                    gvBuyerDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvBuyerDetails.DataSource = BuyerDetails;
        gvBuyerDetails.DataBind();
    }
    #endregion

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvFEOrderProfile.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=feorderprofile&feid=" + gvFEOrderProfile.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SM.FEOrderProfile objSMSOApprove = new SM.FEOrderProfile();
            SM.BeginTransaction();
            objSMSOApprove.FEOPId = gvFEOrderProfile.SelectedRow.Cells[0].Text;
            objSMSOApprove.FEOPApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMSOApprove.FEOrderProfileApprove_Update();
            SM.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFEOrderProfile.DataBind();
            SM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    protected void gvFEOrderProfile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    protected void rbPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbPaymentTerms.SelectedItem.Text == "Others")
        {
            txtOtherPayTerms.Visible = true;
        }
        else
        {
            txtOtherPayTerms.Visible = false;
            txtOtherPayTerms.Text = rbPaymentTerms.SelectedItem.Text;
        }
    }
}

 
