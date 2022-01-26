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
public partial class Modules_SCM_FixedPurchaseOrderDetailsNew : basePage
{
    decimal Amount = 0;
    string poNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        poNo = Request.QueryString["poNo"];
        if(!IsPostBack)
    {
        if (poNo == "")
        {
            SCM.ClearControls(this);
            // gvFixedPODetails.SelectedIndex = -1;
            // gvQuotationItems.SelectedIndex = -1;
            rbByIndentApproval.Checked = false;
            rbByQuotation.Checked = false;
            //IndentApprovalNo_Fill();
            ddlSupplierName.Enabled = true;
            txtPONo.Text = SCM.SupplierFixedPO.SuppliersFixedPO_AutoGenCode();
            txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Text = "Save";
            btnSave.Enabled = true;
            tblFixedPODetails.Visible = true;
            gvQuotationItems.DataBind();
            gvPOItems.DataBind();
            txtNetAmount.Text = "0";
            txtTermsConditions.Text = "Delivery: " + Environment.NewLine + "Excise Duty: " + Environment.NewLine + "Warranty: ";
        }
        else
        {

            try
            {
                SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                if (objSCM.SuppliersFixedPO_Select(poNo) > 0)
                {
                    rbByIndentApproval_CheckedChanged(sender, e);
                    rbByQuotation_CheckedChanged(sender, e);
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    btnRefresh.Visible = true;
                    tblFixedPODetails.Visible = true;

                    txtPONo.Text = objSCM.FPONo;
                    txtPODate.Text = objSCM.FPODate;
                    if (objSCM.SupQuotId == "0")
                    {
                        IndentApprovalNo_Fill();
                        rbByIndentApproval.Checked = true;
                        lblQuotNo.Text = "Indent Approval No.";
                        lblQuotDate.Text = "Indent Approval Date";
                        rbByQuotation.Checked = false;
                        ddlQuotationNo.SelectedValue = objSCM.IndApprlId;
                    }
                    else
                    {
                        QuotationMaster_Fill();
                        rbByQuotation.Checked = true;
                        lblQuotNo.Text = "Quotation No.";
                        lblQuotDate.Text = "Quotation Date";
                        rbByIndentApproval.Checked = false;
                        ddlQuotationNo.SelectedValue = objSCM.SupQuotId;
                    }
                    ddlQuotationNo_SelectedIndexChanged(sender, e);
                    ddlSupplierName.SelectedValue = objSCM.SupId;
                    ddlSupplierName_SelectedIndexChanged(sender, e);
                    txtSuppReference.Text = objSCM.FPOSuppRef;
                    ddlDespatchMode.SelectedValue = objSCM.DespmId;
                    txtTermsConditions.Text = objSCM.FPOTermsConds;
                    txtDestination.Text = objSCM.FPODestination;
                    txtInsurance.Text = objSCM.FPOInsurance;
                    txtFreight.Text = objSCM.FPOFreight;
                    txtNetAmount.Text = objSCM.FPONetAmount;
                    txtAmountInWords.Text = objSCM.FPOAmtWords;
                    txtCIF.Text = objSCM.FPOCIFCharges;
                    txtFOB.Text = objSCM.FPOFOBCharges;
                    txtSuppliersContactPerson.Text = objSCM.FPOSuppContactPerson;
                    ddlCurrencyTypeForOrder.SelectedValue = objSCM.FPOCurrencyType;

                    if (chklIndigenous.Visible == true)
                    {
                        SelectCheckBoxFromString(objSCM.FPOPaymentTerms, chklIndigenous);
                    }
                    else if (chklForeign.Visible == true)
                    {
                        SelectCheckBoxFromString(objSCM.FPOPaymentTerms, chklForeign);
                    }
                    ddlCurrencyType.SelectedValue = objSCM.CurrencyId;
                    txtNetAmtInOtherCurrency.Text = objSCM.FPONetAmtInOtherCurrency;
                    txtDiscount.Text = objSCM.FPODiscount;
                    txtCSTTax.Text = objSCM.FPOTaxCST;
                    txtTermsOfDelivery.Text = objSCM.FPOTermsOfDelivery;
                    ddlContactPerson.SelectedValue = objSCM.FPOContactPerson;
                    ddlPreparedBy.SelectedValue = objSCM.PreparedBy;
                    ddlApprovedBy.SelectedValue = objSCM.ApprovedBy;
                    txtRemitance.Text = objSCM.Remitance;


                    objSCM.SuppliersFixedPODetails_Select(poNo, gvPOItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                // btnDelete.Attributes.Clear();
                // SCM.Dispose();
            }
        }



        lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        lblCPID.Text = cp.getPresentCompanySessionValue();
        txtItemQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtItemRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtDisc.Attributes.Add("onkeyup", "javascript:amtcalc();");
        //txtTax.Attributes.Add("onkeyup", "javascript:amtcalc();");

        txtCIF.Attributes.Add("onkeyup", "javascript:netamtcalc();");
        txtFOB.Attributes.Add("onkeyup", "javascript:netamtcalc();");
        txtCSTTax.Attributes.Add("onkeyup", "javascript:netamtcalc();");
        txtDiscount.Attributes.Add("onkeyup", "javascript:netamtcalc();");
        txtInsurance.Attributes.Add("onkeyup", "javascript:netamtcalc();");
        txtFreight.Attributes.Add("onkeyup", "javascript:netamtcalc();");

        //  IndentApprovalNo_Fill();
        SupplierMaster_Fill();
        DeliveryType_Fill();
        // ItemTypes_Fill();
        CurrencyType_Fill();
        EmployeeMaster_Fill();
        if (Request.QueryString["Qid"] != null)
        {
            // btnNew_Click(sender, e);
            rbByQuotation.Checked = true;
            rbByQuotation_CheckedChanged(sender, e);
            ddlQuotationNo.SelectedValue = Request.QueryString["Qid"].ToString();
            ddlQuotationNo_SelectedIndexChanged(sender, e);
            tblFixedPODetails.Visible = true;
            //txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
        {
            txtNetAmount.Text = NetAmountCalc().ToString();
            if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
            if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
            if (txtFOB.Text == "" || txtFOB.Text == string.Empty) { txtFOB.Text = "0"; }
            if (txtCIF.Text == "" || txtCIF.Text == string.Empty) { txtCIF.Text = "0"; }
            txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100) + double.Parse(txtCIF.Text) + double.Parse(txtFOB.Text));
        }

    }

    private void LoadPurchaseOrderDetails()
    {
        //try
        //{
        //    SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
        //    if (objSCM.SuppliersFixedPO_Select(poNo) > 0)
        //    {
        //        rbByIndentApproval_CheckedChanged(sender, e);
        //        rbByQuotation_CheckedChanged(sender, e);
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = true;
        //        btnRefresh.Visible = true;
        //        tblFixedPODetails.Visible = true;

        //        txtPONo.Text = objSCM.FPONo;
        //        txtPODate.Text = objSCM.FPODate;
        //        if (objSCM.SupQuotId == "0")
        //        {
        //            IndentApprovalNo_Fill();
        //            rbByIndentApproval.Checked = true;
        //            lblQuotNo.Text = "Indent Approval No.";
        //            lblQuotDate.Text = "Indent Approval Date";
        //            rbByQuotation.Checked = false;
        //            ddlQuotationNo.SelectedValue = objSCM.IndApprlId;
        //        }
        //        else
        //        {
        //            QuotationMaster_Fill();
        //            rbByQuotation.Checked = true;
        //            lblQuotNo.Text = "Quotation No.";
        //            lblQuotDate.Text = "Quotation Date";
        //            rbByIndentApproval.Checked = false;
        //            ddlQuotationNo.SelectedValue = objSCM.SupQuotId;
        //        }
        //        ddlQuotationNo_SelectedIndexChanged(sender, e);
        //        ddlSupplierName.SelectedValue = objSCM.SupId;
        //        ddlSupplierName_SelectedIndexChanged(sender, e);
        //        txtSuppReference.Text = objSCM.FPOSuppRef;
        //        ddlDespatchMode.SelectedValue = objSCM.DespmId;
        //        txtTermsConditions.Text = objSCM.FPOTermsConds;
        //        txtDestination.Text = objSCM.FPODestination;
        //        txtInsurance.Text = objSCM.FPOInsurance;
        //        txtFreight.Text = objSCM.FPOFreight;
        //        txtNetAmount.Text = objSCM.FPONetAmount;
        //        txtAmountInWords.Text = objSCM.FPOAmtWords;
        //        txtCIF.Text = objSCM.FPOCIFCharges;
        //        txtFOB.Text = objSCM.FPOFOBCharges;
        //        txtSuppliersContactPerson.Text = objSCM.FPOSuppContactPerson;
        //        ddlCurrencyTypeForOrder.SelectedValue = objSCM.FPOCurrencyType;

        //        if (chklIndigenous.Visible == true)
        //        {
        //            SelectCheckBoxFromString(objSCM.FPOPaymentTerms, chklIndigenous);
        //        }
        //        else if (chklForeign.Visible == true)
        //        {
        //            SelectCheckBoxFromString(objSCM.FPOPaymentTerms, chklForeign);
        //        }
        //        ddlCurrencyType.SelectedValue = objSCM.CurrencyId;
        //        txtNetAmtInOtherCurrency.Text = objSCM.FPONetAmtInOtherCurrency;
        //        txtDiscount.Text = objSCM.FPODiscount;
        //        txtCSTTax.Text = objSCM.FPOTaxCST;
        //        txtTermsOfDelivery.Text = objSCM.FPOTermsOfDelivery;
        //        ddlContactPerson.SelectedValue = objSCM.FPOContactPerson;
        //        ddlPreparedBy.SelectedValue = objSCM.PreparedBy;
        //        ddlApprovedBy.SelectedValue = objSCM.ApprovedBy;
        //        txtRemitance.Text = objSCM.Remitance;


        //        objSCM.SuppliersFixedPODetails_Select(poNo, gvPOItems);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //   // btnDelete.Attributes.Clear();
        //    // SCM.Dispose();
        //}
    }

    #region Page Prerender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        txtNetAmount.Text = NetAmountCalc().ToString();
        if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
        if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
        if (txtFOB.Text == "" || txtFOB.Text == string.Empty) { txtFOB.Text = "0"; }
        if (txtCIF.Text == "" || txtCIF.Text == string.Empty) { txtCIF.Text = "0"; }
        txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100) + double.Parse(txtCIF.Text) + double.Parse(txtFOB.Text));

        if (poNo!=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]) && Request.QueryString["status"] != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnEdit.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                // btnEdit.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            // btnEdit.Visible = true;
        }
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.PerformaInvoiceItemTypesIndent_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();
        }
    }
    #endregion

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.PurchaseQuotationItemTypesIndent_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();
        }
    }
    #endregion

    #region Supplier Master Fill
    private void SupplierMaster_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // SCM.Dispose();
        }
    }
    #endregion

    #region Quotation Master Fill
    private void QuotationMaster_Fill()
    {
        try
        {
            SCM.SuppliersQuotation.SuppliersQuotation_Select(ddlQuotationNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // SCM.Dispose();
        }
    }
    #endregion

    #region Indent Approval Fill
    private void IndentApprovalNo_Fill()
    {
        try
        {
            SCM.IndentApproval.IndentApproval_Select(ddlQuotationNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // SCM.Dispose();
        }
    }
    #endregion

    #region Currency Type Fill
    private void CurrencyType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlCurrencyType);
            Masters.CurrencyType.CurrencyType_Select(ddlCurrencyTypeForOrder);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // Masters.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlContactPerson);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // HR.Dispose();
        }
    }
    #endregion

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDespatchMode);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //  Masters.Dispose();
        }
    }
    #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                // txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                // txtItemRate.Text = objMaster.ItemSeries;
                txtItemSpecifications.Text = objMaster.ItemSpec;
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

            }
            if (objMaster.supporate_select(ddlItemType.SelectedItem.Value) > 0)
            {
                txtItemRate.Text = objMaster.supporate;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlItemType.SelectedValue);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // Masters.Dispose();
        }
    }
    #endregion

    #region Select CheckBox Change
    private void SelectCheckBoxFromString(string SelectedString, CheckBoxList chkl)
    {
        chkl.ClearSelection();
        if (SelectedString == null) return;
        string[] SelectedStringSplit = SelectedString.Split(',');
        for (int i = 0; i < chkl.Items.Count; i++)
        {
            for (int j = 0; j < SelectedStringSplit.Length; j++)
            {
                if (SelectedStringSplit[j].ToString() == chkl.Items[i].Text)
                {
                    chkl.Items[i].Selected = true;
                }
            }
        }
    }
    #endregion

    #region Quotation No Selected Index Changed
    protected void ddlQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gvApprlItemDetails.DataBind();
            gvQuotationItems.DataBind();
            if (rbByIndentApproval.Checked == true)
            {
                ItemName_Fill();

                SCM.Indent obj = new SCM.Indent();
                if (obj.Indent_Select(ddlQuotationNo.SelectedItem.Value) > 0)
                {
                    txtQuotationDate.Text = obj.INDDate;
                    obj.IndentDetails_Select(ddlQuotationNo.SelectedItem.Value, gvApprlItemDetails);
                    gvPOItems.DataBind();
                    btnGo.Visible = false;
                    if (gvApprlItemDetails.Rows.Count > 0)
                    {
                        btnGoIndent.Visible = true;
                    }
                    else
                    {
                        btnGoIndent.Visible = false;
                    }
                    
                }


            }
            else if (rbByQuotation.Checked == true)
            {
                ItemTypes_Fill();
                SCM.SuppliersQuotation objSCM = new SCM.SuppliersQuotation();
                if (objSCM.SuppliersQuotation_Select(ddlQuotationNo.SelectedItem.Value) > 0)
                {
                    txtQuotationDate.Text = objSCM.SupQuotDate;
                    ddlSupplierName.SelectedValue = objSCM.SupId;
                    ddlDespatchMode.SelectedValue = objSCM.SupDepatchMode;
                    //txtFreight.Text = objSCM.SupPackingCharges;
                    //txtInsurance.Text = objSCM.SupInsurance;
                    ddlSupplierName_SelectedIndexChanged(sender, e);
                    objSCM.SuppliersQuotationDetails_Select(ddlQuotationNo.SelectedItem.Value, gvQuotationItems);
                    gvPOItems.DataBind();
                    btnGoIndent.Visible = false;
                    if (gvQuotationItems.Rows.Count > 0)
                    {
                        btnGo.Visible = true;
                    }
                    else
                    {
                        btnGo.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
           // btnDelete.Attributes.Clear();
            // SCM.Dispose();
        }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            FixedPOSave();
            Response.Redirect("FixedPurchaseOrderDetails.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            FixedPOUpdate();
        }
       // gvFixedPODetails.SelectedIndex = -1;
    }
    #endregion

    #region SalesOrderSave
    private void FixedPOSave()
    {
        if (gvPOItems.Rows.Count > 0)
        {
            try
            {
                SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                SCM.BeginTransaction();
                objSCM.FPONo = txtPONo.Text;
                objSCM.FPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
                if (rbByIndentApproval.Checked == true)
                {
                    objSCM.IndApprlId = ddlQuotationNo.SelectedItem.Value;
                    objSCM.SupQuotId = "0";
                }
                else if (rbByQuotation.Checked == true)
                {
                    objSCM.SupQuotId = ddlQuotationNo.SelectedItem.Value;
                    objSCM.IndApprlId = "0";
                }
                objSCM.SupId = ddlSupplierName.SelectedItem.Value;
                objSCM.FPOSuppRef = txtSuppReference.Text;
                objSCM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSCM.FPOPOStatus = "New";
                objSCM.FPOTermsConds = txtTermsConditions.Text;
                objSCM.FPODestination = txtDestination.Text;
                objSCM.FPOInsurance = txtInsurance.Text;
                objSCM.FPOFreight = txtFreight.Text;
                objSCM.FPONetAmount = txtNetAmount.Text.Replace("Rs. ", "");
                objSCM.FPOAmtWords = txtAmountInWords.Text;
                objSCM.FPOCIFCharges = txtCIF.Text;
                objSCM.FPOFOBCharges = txtFOB.Text;
                objSCM.FPOSuppContactPerson = txtSuppliersContactPerson.Text;
                objSCM.FPOCurrencyType = ddlCurrencyTypeForOrder.SelectedItem.Value;

                if (chklIndigenous.Visible == true)
                {
                    objSCM.FPOPaymentTerms = CheckBoxSelectedListInString(chklIndigenous);
                }
                else if (chklForeign.Visible == true)
                {
                    objSCM.FPOPaymentTerms = CheckBoxSelectedListInString(chklForeign);
                }
                objSCM.CurrencyId = ddlCurrencyType.SelectedItem.Value;
                objSCM.FPONetAmtInOtherCurrency = txtNetAmtInOtherCurrency.Text;
                objSCM.FPODiscount = txtDiscount.Text;
                objSCM.FPOTaxCST = txtCSTTax.Text;
                objSCM.FPOTermsOfDelivery = txtTermsOfDelivery.Text;
                objSCM.FPOContactPerson = ddlContactPerson.SelectedItem.Value;
                objSCM.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.ApprovedBy = "0";
                objSCM.CpId = lblCPID.Text;
                objSCM.Remitance = txtRemitance.Text;

                if (objSCM.SuppliersFixedPO_Save() == "Data Saved Successfully")
                {
                    objSCM.SuppliersFixedPODetails_Delete(objSCM.FPOId);
                    foreach (GridViewRow gvrow in gvPOItems.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[2].Text;
                        objSCM.FPODetQty = gvrow.Cells[6].Text;
                        objSCM.FPODetRate = gvrow.Cells[7].Text;
                        objSCM.FPODetTax = gvrow.Cells[8].Text;
                        if (gvrow.Cells[12].Text == "")
                        {
                            objSCM.FPODetDeliveryDate = "01/01/1900";
                        }
                        else
                        {
                           // objSCM.FPODetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                            objSCM.FPODetDeliveryDate = (gvrow.Cells[12].Text);

                        }
                        objSCM.FPODetSpec = gvrow.Cells[13].Text;
                        objSCM.FPODetRemarks = gvrow.Cells[14].Text;
                        objSCM.FPOSuppDisc = gvrow.Cells[10].Text;
                        objSCM.FPOSuppSpPrice = gvrow.Cells[11].Text;
                        objSCM.FPODetIndId = gvrow.Cells[16].Text;
                        objSCM.FPODetIndDetId = gvrow.Cells[17].Text;
                        string ttlQty = gvrow.Cells[6].Text;
                        string dId = gvrow.Cells[17].Text;
                       
                        SCM.IndentApproval hai = new SCM.IndentApproval();
                        if(hai.IndentDetQty_Select(dId) > 0)
                        {
                            string qty = hai.Indentqty;
                            string ordQty = hai.IndOrdqty;
                            ordQty =(Convert.ToInt32(ordQty) + Convert.ToInt32(ttlQty)).ToString();
                            if (Convert.ToInt32(ordQty) >= Convert.ToInt32(qty))
                            {
                                hai.IndentStatus_Update(ordQty, "Close", dId);
                            }
                            else  if (Convert.ToInt32(ordQty) < Convert.ToInt32(qty))
                            {
                                hai.IndentStatus_Update(ordQty, "Ordered", dId);
                            }
                        }  

                        objSCM.SuppliersFixedPODetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
               // btnDelete.Attributes.Clear();
                //gvFixedPODetails.DataBind();
                gvQuotationItems.DataBind();
                gvPOItems.DataBind();
                tblFixedPODetails.Visible = false;
                SCM.ClearControls(this);
                // SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add Mandatory Feilds");
        }
    }
    #endregion

    #region SalesOrderUpdate
    private void FixedPOUpdate()
    {
        if (gvPOItems.Rows.Count > 0)
        {
            try
            {
                SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                SCM.BeginTransaction();
                objSCM.FPOId = poNo;
                objSCM.FPONo = txtPONo.Text;
                objSCM.FPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
                if (rbByIndentApproval.Checked == true)
                {
                    objSCM.IndApprlId = ddlQuotationNo.SelectedItem.Value;
                    objSCM.SupQuotId = "0";
                }
                else if (rbByQuotation.Checked == true)
                {
                    objSCM.SupQuotId = ddlQuotationNo.SelectedItem.Value;
                    objSCM.IndApprlId = "0";
                }
                objSCM.SupId = ddlSupplierName.SelectedItem.Value;
                objSCM.FPOSuppRef = txtSuppReference.Text;
                objSCM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSCM.FPOPOStatus = "New";
                objSCM.FPOTermsConds = txtTermsConditions.Text;
                objSCM.FPODestination = txtDestination.Text;
                objSCM.FPOInsurance = txtInsurance.Text;
                objSCM.FPOFreight = txtFreight.Text;
                objSCM.FPONetAmount = txtNetAmount.Text.Replace("Rs. ", "");
                objSCM.FPOAmtWords = txtAmountInWords.Text;
                objSCM.FPOCIFCharges = txtCIF.Text;
                objSCM.FPOFOBCharges = txtFOB.Text;
                objSCM.FPOSuppContactPerson = txtSuppliersContactPerson.Text;
                objSCM.FPOCurrencyType = ddlCurrencyTypeForOrder.SelectedItem.Value;

                if (chklIndigenous.Visible == true)
                {
                    objSCM.FPOPaymentTerms = CheckBoxSelectedListInString(chklIndigenous);
                }
                else if (chklForeign.Visible == true)
                {
                    objSCM.FPOPaymentTerms = CheckBoxSelectedListInString(chklForeign);
                }
                objSCM.CurrencyId = ddlCurrencyType.SelectedItem.Value;
                objSCM.FPONetAmtInOtherCurrency = txtNetAmtInOtherCurrency.Text;
                objSCM.FPODiscount = txtDiscount.Text;
                objSCM.FPOTaxCST = txtCSTTax.Text;
                objSCM.FPOTermsOfDelivery = txtTermsOfDelivery.Text;
                objSCM.FPOContactPerson = ddlContactPerson.SelectedItem.Value;
                objSCM.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.ApprovedBy = "0";
                objSCM.CpId = lblCPID.Text;
                objSCM.Remitance = txtRemitance.Text;

                if (objSCM.SuppliersFixedPO_Update() == "Data Updated Successfully")
                {
                    objSCM.SuppliersFixedPODetails_Delete(objSCM.FPOId);
                    foreach (GridViewRow gvrow in gvPOItems.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[2].Text;
                        objSCM.FPODetQty = gvrow.Cells[6].Text;
                        objSCM.FPODetRate = gvrow.Cells[7].Text;
                        objSCM.FPODetTax = gvrow.Cells[8].Text;
                        if (gvrow.Cells[12].Text == "")
                        {
                            objSCM.FPODetDeliveryDate = "01/01/1900";
                        }
                        else
                        {
                            objSCM.FPODetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                        }
                        objSCM.FPODetSpec = gvrow.Cells[13].Text;
                        objSCM.FPODetRemarks = gvrow.Cells[14].Text;
                        objSCM.FPOSuppDisc = gvrow.Cells[10].Text;
                        objSCM.FPOSuppSpPrice = gvrow.Cells[11].Text;

                        objSCM.SuppliersFixedPODetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
               // btnDelete.Attributes.Clear();
                //gvFixedPODetails.DataBind();
                gvQuotationItems.DataBind();
                gvPOItems.DataBind();
                tblFixedPODetails.Visible = false;
                SCM.ClearControls(this);
                Response.Redirect("FixedPurchaseOrderDetails.aspx");
                //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Fixed Purchase Order");
        }
    }
    #endregion

    #region CheckBox Select change
    private string CheckBoxSelectedListInString(CheckBoxList chkl)
    {
        string chklSelectedListString = "";
        for (int i = 0; i < chkl.Items.Count; i++)
        {
            if (chkl.Items[i].Selected == true)
            {
                if (chklSelectedListString == "")
                {
                    chklSelectedListString = chkl.Items[i].Text;
                }
                else
                {
                    chklSelectedListString = chklSelectedListString + "," + chkl.Items[i].Text;
                }
            }
        }
        return chklSelectedListString;
    }
    #endregion

    #region DDlSupplier Name change
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SuppliersMaster objSupMast = new SCM.SuppliersMaster();
            if (objSupMast.SuppliersMaster_Select(ddlSupplierName.SelectedItem.Value) > 0)
            {
                txtSuppliersContactPerson.Text = objSupMast.SupContactPerson;
                if (objSupMast.SupIndigenousForeign.ToLower() == "indigenous")
                {
                    lblFOB.Visible = txtFOB.Visible = lblCIF.Visible = txtCIF.Visible = false;
                    lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = true;
                    chklIndigenous.Visible = true;
                    chklForeign.Visible = false;
                }
                else if (objSupMast.SupIndigenousForeign.ToLower() == "foreign")
                {
                    lblFOB.Visible = txtFOB.Visible = lblCIF.Visible = txtCIF.Visible = true;
                    lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = false;
                    chklIndigenous.Visible = false;
                    chklForeign.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            // SCM.Dispose();
        }
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        txtDiscount.Text = string.Empty;
        txtCSTTax.Text = string.Empty;
        gvQuotationItems.DataBind();
        gvPOItems.DataBind();
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Tax");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Disc");
        SalesOrderItems.Columns.Add(col);


        if (gvPOItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvPOItems.Rows)
            {
                if (gvPOItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvPOItems.SelectedRow.RowIndex)
                    {
                        DataRow drnew = SalesOrderItems.NewRow();
                        drnew["ItemType"] = ddlItemType.SelectedItem.Text;
                        drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
                        drnew["ItemName"] = txtModelName.Text;
                        drnew["UOM"] = txtItemUOM.Text;
                        drnew["Quantity"] = txtItemQuantity.Text;
                        drnew["Rate"] = txtItemRate.Text;
                        drnew["Tax"] = txtTax.Text;
                        drnew["DeliveryDate"] = txtDeliveryDate.Text;
                        drnew["Specifications"] = txtItemSpecifications.Text;
                        drnew["Remarks"] = txtItemRemarks.Text;
                        drnew["SpPrice"] = txtTotal.Text;
                        drnew["Disc"] = txtDisc.Text;

                        SalesOrderItems.Rows.Add(drnew);
                    }
                    else
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["Tax"] = gvrow.Cells[8].Text;
                        dr["DeliveryDate"] = gvrow.Cells[12].Text;
                        dr["Specifications"] = gvrow.Cells[13].Text;
                        dr["Remarks"] = gvrow.Cells[14].Text;
                        dr["ItemTypeId"] = gvrow.Cells[15].Text;
                        dr["SpPrice"] = gvrow.Cells[11].Text;
                        dr["Disc"] = gvrow.Cells[10].Text;


                        SalesOrderItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Tax"] = gvrow.Cells[8].Text;
                    dr["DeliveryDate"] = gvrow.Cells[12].Text;
                    dr["Specifications"] = gvrow.Cells[13].Text;
                    dr["Remarks"] = gvrow.Cells[14].Text;
                    dr["ItemTypeId"] = gvrow.Cells[15].Text;
                    dr["SpPrice"] = gvrow.Cells[11].Text;
                    dr["Disc"] = gvrow.Cells[10].Text;


                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }

        if (gvPOItems.Rows.Count > 0)
        {
            if (gvPOItems.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvPOItems.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlItemType.SelectedItem.Value)
                    {
                        gvPOItems.DataSource = SalesOrderItems;
                        gvPOItems.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvPOItems.SelectedIndex == -1)
        {
            DataRow drnew = SalesOrderItems.NewRow();
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
            drnew["ItemName"] = txtModelName.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;
            drnew["Rate"] = txtItemRate.Text;
            drnew["Tax"] = txtTax.Text;
            drnew["DeliveryDate"] = txtDeliveryDate.Text;
            drnew["Specifications"] = txtItemSpecifications.Text;
            drnew["Remarks"] = txtItemRemarks.Text;
            drnew["SpPrice"] = txtTotal.Text;
            drnew["Disc"] = txtDisc.Text;

            SalesOrderItems.Rows.Add(drnew);
        }
        gvPOItems.DataSource = SalesOrderItems;
        gvPOItems.DataBind();
        gvPOItems.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        //ddlItemType.SelectedValue = "0";
        txtModelName.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        txtDisc.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtTax.Text = string.Empty;
        txtItemSpecifications.Text = string.Empty;
        txtItemRemarks.Text = string.Empty;
        txtDeliveryDate.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        // txtColor.Text = string.Empty;
        ddlcolor.SelectedValue = "0";

        txtBrand.Text = string.Empty;
    }
    #endregion

    #region gvPOItems_RowEditing
    protected void gvPOItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Tax");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Disc");
        SalesOrderItems.Columns.Add(col);


        if (gvPOItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvPOItems.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["Tax"] = gvrow.Cells[8].Text;
                dr["DeliveryDate"] = gvrow.Cells[12].Text;
                dr["Specifications"] = gvrow.Cells[13].Text;
                dr["Remarks"] = gvrow.Cells[14].Text;
                dr["ItemTypeId"] = gvrow.Cells[15].Text;
                dr["SpPrice"] = gvrow.Cells[11].Text;
                dr["Disc"] = gvrow.Cells[10].Text;


                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvPOItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    //ItemName_Fill();
                    txtModelName.Text = gvrow.Cells[4].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemRate.Text = gvrow.Cells[7].Text;
                    txtTax.Text = gvrow.Cells[8].Text;
                    txtTotal.Text = gvrow.Cells[11].Text;
                    txtDisc.Text = gvrow.Cells[10].Text;
                    txtDeliveryDate.Text = gvrow.Cells[12].Text;
                    txtItemSpecifications.Text = gvrow.Cells[13].Text;
                    txtItemRemarks.Text = gvrow.Cells[14].Text;
                    gvPOItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvPOItems.DataSource = SalesOrderItems;
        gvPOItems.DataBind();
    }
    #endregion

    #region GridView PO Items Row DataBound
    protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            if (e.Row.Cells[8].Text == "&nbsp;" || e.Row.Cells[8].Text == "") { e.Row.Cells[8].Text = "0"; }
            e.Row.Cells[9].Text = (Convert.ToDouble(e.Row.Cells[7].Text) * Convert.ToDouble(e.Row.Cells[6].Text)).ToString();
            //e.Row.Cells[9].Text = ((Convert.ToDouble(e.Row.Cells[7].Text) * Convert.ToDouble(e.Row.Cells[6].Text)) + (Convert.ToDouble(e.Row.Cells[8].Text) * (Convert.ToDouble(e.Row.Cells[7].Text) * Convert.ToDouble(e.Row.Cells[6].Text))) / 100).ToString();
            // e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToShortDateString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;//Tax is not applicable
            e.Row.Cells[15].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Amount = Amount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtSubTotal.Text = NetAmountCalc().ToString();
            e.Row.Cells[10].Text = "TotalAmount:";
            e.Row.Cells[11].Text = Amount.ToString();
            e.Row.Cells[8].Visible = false;//Tax is not applicable
            e.Row.Cells[15].Visible = false;
        }
    }

    private double NetAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvPOItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        txtNetAmount.Text = _totalAmt.ToString();
        if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
        if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
        txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100));

        //Yantra.Classes.General.NumberToEnglish convertToWords = new Yantra.Classes.General.NumberToEnglish();
        //txtAmountInWords.Text = "Rs. " + convertToWords.changeCurrencyToWords(_totalAmt);
        return _totalAmt;
    }
    #endregion


    #region GridView PO Items Row Deleting
    protected void gvPOItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvPOItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Tax");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Disc");
        SalesOrderItems.Columns.Add(col);


        if (gvPOItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvPOItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Tax"] = gvrow.Cells[8].Text;
                    dr["DeliveryDate"] = gvrow.Cells[12].Text;
                    dr["Specifications"] = gvrow.Cells[13].Text;
                    dr["Remarks"] = gvrow.Cells[14].Text;
                    dr["ItemTypeId"] = gvrow.Cells[15].Text;
                    dr["SpPrice"] = gvrow.Cells[10].Text;
                    dr["Disc"] = gvrow.Cells[11].Text;

                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvPOItems.DataSource = SalesOrderItems;
        gvPOItems.DataBind();
        if (gvPOItems.Rows.Count == 0)
        {
            txtAmountInWords.Text = "";
            txtNetAmount.Text = "";
        }
    }
    #endregion

    #region GridView Fixed PO Details Row DataBound
    protected void gvFixedPODetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
    #endregion

    #region rbByIndentApproval_CheckedChanged
    protected void rbByIndentApproval_CheckedChanged(object sender, EventArgs e)
    {

        SCM.ClearControls(this);
        gvApprlItemDetails.DataBind();
        gvQuotationItems.DataBind();
        SM.DDLBindWithSelect(ddlQuotationNo, "select IND_ID,IND_NO from YANTRA_INDENT_MAST");
        //IndentApprovalNo_Fill();
        ddlSupplierName.Enabled = true;
        txtPONo.Text = SCM.SupplierFixedPO.SuppliersFixedPO_AutoGenCode();
        txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        // rbByIndentApproval.Checked = true;
        lblQuotNo.Text = "Indent No.";
        lblQuotDate.Text = "Indent Date";
        // IndentApprovalNo_Fill();
        ItemName_Fill();
        gvPOItems.DataBind();
        btnGo.Visible = false;

        //else if (rbByQuotation.Checked == true)
        //{
        //    SCM.ClearControls(this);
        //    gvApprlItemDetails.DataBind();
        //    gvQuotationItems.DataBind();
        //    IndentApprovalNo_Fill();
        //    ddlSupplierName.Enabled = false;
        //    txtPONo.Text = SCM.SupplierFixedPO.SuppliersFixedPO_AutoGenCode();
        //    txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    rbByQuotation.Checked = true;
        //    lblQuotNo.Text = "Quotation No.";
        //    lblQuotDate.Text = "Quotation Date";
        //    QuotationMaster_Fill();
        //}
    }
    #endregion

    #region gvApprlItemDetails_RowDataBound
    protected void gvApprlItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[14].Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[14].Visible = false;

        }

    }
    #endregion

    #region ddlItemName_SelectedIndexChanged
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
            }
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

    #region DDlCurrnecy Change
    protected void ddlCurrencyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrencyType.SelectedItem.Value != "0")
        {
            lblOtherCurrencyValue.Text = "Net Amount in " + ddlCurrencyType.SelectedItem.Text;
        }
        else
        {
            lblOtherCurrencyValue.Text = "Net Amount in Other Currency";
        }
    }
    #endregion

    #region BtnApprove Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierFixedPO objSCMPOApprove = new SCM.SupplierFixedPO();
            SCM.BeginTransaction();

           objSCMPOApprove.SuppliersFixedPOApprove_Update(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId), poNo);
           
           objSCMPOApprove.SuppliersFixedPOStatus_Update("Open", poNo);
            
            SCM.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           // gvFixedPODetails.DataBind();
            // SCM.Dispose();
            //btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region ddlcurrency Type Change
    protected void ddlCurrencyTypeForOrder_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlCurrencyTypeForOrder.SelectedItem.Value == "0")
        {
            lblCurrenctAlert.Text = "";
        }
        else
        {
            lblCurrenctAlert.Text = " Please Select the Order Details as per the Currency - <b>" + ddlCurrencyTypeForOrder.SelectedItem.Text + "</b>";
        }
        ddlCurrencyType.SelectedValue = ddlCurrencyTypeForOrder.SelectedItem.Value;
    }
    #endregion

    #region RadioButton quotation Check Change
    protected void rbByQuotation_CheckedChanged(object sender, EventArgs e)
    {

        SCM.ClearControls(this);
        gvApprlItemDetails.DataBind();
        gvQuotationItems.DataBind();
        IndentApprovalNo_Fill();
        ddlSupplierName.Enabled = false;
        txtPONo.Text = SCM.SupplierFixedPO.SuppliersFixedPO_AutoGenCode();
        txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //rbByQuotation.Checked = true;
        lblQuotNo.Text = "PI No.";
        lblQuotDate.Text = "PI Date";
        QuotationMaster_Fill();
        gvPOItems.DataBind();
        if (gvQuotationItems.Rows.Count > 0)
        {
            btnGo.Visible = true;
        }
        else
        {
            btnGo.Visible = false;
        }

    }
    #endregion 

    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[10].Text = (Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(e.Row.Cells[6].Text)).ToString();
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
    }
    protected void gvQuotationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnGo.Visible = true;
        DataTable SuppliersQuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("ModelName");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Brand");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Specification");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("SpRate");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Curency");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("CurencyId");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Disc");
        SuppliersQuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = SuppliersQuotationItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["ModelName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Brand"] = gvrow.Cells[7].Text;
                dr["Curency"] = gvrow.Cells[8].Text;
                dr["Rate"] = gvrow.Cells[9].Text;
                dr["SpRate"] = gvrow.Cells[12].Text;
                dr["Specification"] = gvrow.Cells[13].Text;
                dr["CurencyId"] = gvrow.Cells[14].Text;
                dr["Disc"] = gvrow.Cells[11].Text;

                SuppliersQuotationItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    txtItemRate.Text = gvrow.Cells[9].Text;
                    txtDisc.Text = gvrow.Cells[11].Text;
                    txtTotal.Text = gvrow.Cells[12].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemSpecifications.Text = gvrow.Cells[13].Text;

                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
    }
    protected void gvApprlItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);

        if (gvApprlItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
            {
                DataRow dr = IndentProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["ItemType"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Priority"] = gvrow.Cells[7].Text;
                dr["Brand"] = gvrow.Cells[8].Text;
                dr["SuggestedParty"] = gvrow.Cells[9].Text;
                dr["ReqFor"] = gvrow.Cells[10].Text;
                dr["ReqDate"] = gvrow.Cells[11].Text;
                dr["Specification"] = gvrow.Cells[12].Text;

                IndentProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvApprlItemDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemSpecifications.Text = gvrow.Cells[12].Text;
                    txtDeliveryDate.Text = gvrow.Cells[11].Text;

                    gvApprlItemDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkQuot");
            if (ch.Checked == true)
            {

                DataTable SuppliersQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelName");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Brand");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Curency");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("CurencyId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("IndentId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("IndentDetId");
                SuppliersQuotationItems.Columns.Add(col);

                if (gvPOItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvPOItems.Rows)
                    {
                        DataRow dr = SuppliersQuotationItems.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ItemType"] = gvrow1.Cells[3].Text;
                        dr["ItemName"] = gvrow1.Cells[4].Text;
                        dr["UOM"] = gvrow1.Cells[5].Text;
                        dr["Quantity"] = gvrow1.Cells[6].Text;
                        dr["Rate"] = gvrow1.Cells[7].Text;
                        dr["Tax"] = gvrow1.Cells[8].Text;
                        dr["DeliveryDate"] = gvrow1.Cells[12].Text;
                        dr["Specifications"] = gvrow1.Cells[13].Text;
                        dr["Remarks"] = gvrow1.Cells[14].Text;
                        dr["ItemTypeId"] = gvrow1.Cells[15].Text;
                        dr["SpPrice"] = gvrow1.Cells[11].Text;
                        dr["Disc"] = gvrow1.Cells[10].Text;
                        dr["IndentId"] = gvrow1.Cells[16].Text;
                        dr["IndentDetId"] = gvrow1.Cells[17].Text;



                        SuppliersQuotationItems.Rows.Add(dr);
                    }
                }

                if (gvPOItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvPOItems.Rows)
                    {
                        if (gvrow1.Cells[2].Text == gvrow.Cells[2].Text)
                        {
                            gvPOItems.DataSource = SuppliersQuotationItems;
                            //  gvPOItems.DataBind();
                            MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            ch.Checked = false;
                            return;
                        }

                    }
                }

                DataRow drnew = SuppliersQuotationItems.NewRow();
                drnew["ItemCode"] = gvrow.Cells[2].Text;
                drnew["ItemName"] = gvrow.Cells[3].Text;
                drnew["ItemType"] = gvrow.Cells[4].Text;
                drnew["UOM"] = gvrow.Cells[5].Text;
                drnew["Quantity"] = gvrow.Cells[6].Text;
                drnew["Rate"] = gvrow.Cells[9].Text;
                drnew["Tax"] = gvrow.Cells[8].Text;
                drnew["DeliveryDate"] = gvrow.Cells[15].Text;
                drnew["Specifications"] = gvrow.Cells[13].Text;
                drnew["Remarks"] = "--";
                drnew["ItemTypeId"] = gvrow.Cells[15].Text;
                drnew["SpPrice"] = gvrow.Cells[12].Text;
                drnew["Disc"] = gvrow.Cells[11].Text;
                drnew["IndentId"] = gvrow.Cells[16].Text;
                drnew["IndentDetId"] = gvrow.Cells[17].Text;

                SuppliersQuotationItems.Rows.Add(drnew);
                gvPOItems.DataSource = SuppliersQuotationItems;
                gvPOItems.DataBind();
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("FixedPurchaseOrderDetails.aspx");
    }
    protected void btnGoIndent_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkQuot");
            if (ch.Checked == true)
            {

                DataTable SuppliersQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelName");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Brand");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Curency");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("CurencyId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("IndentId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("IndentDetId");
                SuppliersQuotationItems.Columns.Add(col);

                if (gvPOItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvPOItems.Rows)
                    {
                        DataRow dr = SuppliersQuotationItems.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ItemType"] = gvrow1.Cells[3].Text;
                        dr["ItemName"] = gvrow1.Cells[4].Text;
                        dr["UOM"] = gvrow1.Cells[5].Text;
                        dr["Quantity"] = gvrow1.Cells[6].Text;
                        dr["Rate"] = gvrow1.Cells[7].Text;
                        dr["Tax"] = gvrow1.Cells[8].Text;
                        dr["DeliveryDate"] = gvrow1.Cells[12].Text;
                        dr["Specifications"] = gvrow1.Cells[13].Text;
                        dr["Remarks"] = gvrow1.Cells[14].Text;
                        dr["ItemTypeId"] = gvrow1.Cells[15].Text;
                        dr["SpPrice"] = gvrow1.Cells[11].Text;
                        dr["Disc"] = gvrow1.Cells[10].Text;
                        dr["IndentId"] = gvrow1.Cells[16].Text;
                        dr["IndentDetId"] = gvrow1.Cells[17].Text;



                        SuppliersQuotationItems.Rows.Add(dr);
                    }
                }

                if (gvPOItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvPOItems.Rows)
                    {
                        if (gvrow1.Cells[2].Text == gvrow.Cells[2].Text)
                        {
                            gvPOItems.DataSource = SuppliersQuotationItems;
                            //  gvPOItems.DataBind();
                            MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            ch.Checked = false;
                            return;
                        }

                    }
                }

                DataRow drnew = SuppliersQuotationItems.NewRow();
                drnew["ItemCode"] = gvrow.Cells[2].Text;
                drnew["ItemName"] = gvrow.Cells[3].Text;
                drnew["ItemType"] = gvrow.Cells[4].Text;
                drnew["UOM"] = gvrow.Cells[5].Text;
                Label qty = (Label)gvrow.FindControl("lblQuantity");
               // drnew["Quantity"] = gvrow.Cells[6].Text;
                drnew["Quantity"] = qty.Text;

                TextBox rate = (TextBox)gvrow.FindControl("txtRate");
                drnew["Rate"] = rate.Text;
                drnew["Tax"] = gvrow.Cells[8].Text;
                drnew["DeliveryDate"] = gvrow.Cells[15].Text;
                drnew["Specifications"] = gvrow.Cells[16].Text;
                drnew["Remarks"] = "--";
                drnew["ItemTypeId"] = gvrow.Cells[15].Text;
                //TextBox splPrice = (TextBox)gvrow.FindControl("txtSplAmt");
                Label splPrice = (Label)gvrow.FindControl("lblSplAmt");
                drnew["SpPrice"] = splPrice.Text;
                TextBox discount = (TextBox)gvrow.FindControl("txtDiscount");
                drnew["Disc"] = discount.Text;
                drnew["IndentId"] = gvrow.Cells[18].Text;
                drnew["IndentDetId"] = gvrow.Cells[19].Text;

                SuppliersQuotationItems.Rows.Add(drnew);
                gvPOItems.DataSource = SuppliersQuotationItems;
                gvPOItems.DataBind();
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }

    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvApprlItemDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtRate");
            Label qty = (Label)gvr.FindControl("lblQuantity");
            Label amount = (Label)gvr.FindControl("lblAmount");
            Label splAmt = (Label)gvr.FindControl("lblSplAmt");

            if (rate.Text != "")
            {
                amount.Text = (Convert.ToInt32(rate.Text) * Convert.ToInt32(qty.Text)).ToString();
                splAmt.Text = amount.Text;
            }
            else
            {
                amount.Text = "0";
            }
        }
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvApprlItemDetails.Rows)
        {
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            Label splAmt = (Label)gvr.FindControl("lblSplAmt");
            Label amount = (Label)gvr.FindControl("lblAmount");
            string disc;
            if(amount.Text!="" && discount.Text!="")
            {
                disc = ((Convert.ToInt32(amount.Text) * Convert.ToInt32(discount.Text)) / 100).ToString();
                splAmt.Text = (Convert.ToInt32(amount.Text) - Convert.ToInt32(disc)).ToString();
            }
            else
            {
                splAmt.Text = amount.Text;
            }
        }
    }
}
 
