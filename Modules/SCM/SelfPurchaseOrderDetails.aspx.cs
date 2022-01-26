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
public partial class Modules_SCM_SelfPurchaseOrderDetails : basePage
{
    decimal Amount = 0;
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            txtItemQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtItemRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtDisc.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtCIF.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtFOB.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtCSTTax.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtInsurance.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtFreight.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtDisc.Text = "0";
            SupplierMaster_Fill();
            DeliveryType_Fill();
            CurrencyType_Fill();
            EmployeeMaster_Fill();
            DeliveryAddress_Fill();
            invoice_Fill();
            Company_Fill();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);

            txtNetAmount.Text = NetAmountCalc().ToString();
            if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
            if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
            if (txtFOB.Text == "" || txtFOB.Text == string.Empty) { txtFOB.Text = "0"; }
            if (txtCIF.Text == "" || txtCIF.Text == string.Empty) { txtCIF.Text = "0"; }
            txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100) + double.Parse(txtCIF.Text) + double.Parse(txtFOB.Text));

            ItemTypesAll_Fill();
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
        }
    }
    #endregion

    #region Page Prerender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        txtNetAmount.Text = NetAmountCalc().ToString();
        if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
        if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
        if (txtFOB.Text == "" || txtFOB.Text == string.Empty) { txtFOB.Text = "0"; }
        if (txtCIF.Text == "" || txtCIF.Text == string.Empty) { txtCIF.Text = "0"; }
        txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100) + double.Parse(txtCIF.Text) + double.Parse(txtFOB.Text));

        if (gvFixedPODetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvFixedPODetails.SelectedRow.Cells[9].Text) && gvFixedPODetails.SelectedRow.Cells[9].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnEdit.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnEdit.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnEdit.Visible = true;
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
        
    }
    #endregion


    #region Delivery Address Fill
    private void DeliveryAddress_Fill()
    {
        try
        {
            Masters.DeliveryAddress.DeliveryAddress_Select(ddlDeliveryAddress);
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }

    }
    #endregion



    #region invoice Address Fill
    private void invoice_Fill()
    {
        try
        {
            Masters.DeliveryAddress.DeliveryAddress_Select(ddlInvoiceAddress);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }

    }
    #endregion

    #region Company Address Fill
    private void Company_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
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
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpecifications.Text = objMaster.ItemSpec;
                txtItemRate.Text = objMaster.ItemRate;
               // txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

            }
            //if (objMaster.supporate_select(ddlItemType.SelectedItem.Value) > 0)
            //{
            //    txtItemRate.Text = objMaster.supporate;
            //}
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlItemType.SelectedValue);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
       
    }
    #endregion

    #region Link Button FixedPONo_Click
    protected void lbtnFixedPONo_Click(object sender, EventArgs e)
    {
        tblFixedPODetails.Visible = false;
        LinkButton lbtnFixedPONo;
        lbtnFixedPONo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnFixedPONo.Parent.Parent;
        gvFixedPODetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            SCM.SupplierSelfPO objSCM = new SCM.SupplierSelfPO();
            if (objSCM.SuppliersSelfPO_Select(gvFixedPODetails.SelectedRow.Cells[0].Text) > 0)
            {
               
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                btnRefresh.Visible = true;
                tblFixedPODetails.Visible = true;

                txtPONo.Text = objSCM.FPONo;
                txtPODate.Text = objSCM.FPODate;
                
               
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
                ddlCompany.SelectedValue = objSCM.CpId;
                ddlDeliveryAddress.SelectedValue = objSCM.DeliveryAddrees;
                ddlInvoiceAddress.SelectedValue = objSCM.InvoiceAddress;
                txtCustomerCode.Text = objSCM.CustomerCode;
                objSCM.SuppliersSelfPODetails_Select(gvFixedPODetails.SelectedRow.Cells[0].Text, gvPOItems);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
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

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelfPurchaseOrderDetailsNew.aspx");
        //SCM.ClearControls(this);
        //gvFixedPODetails.SelectedIndex = -1;
        
        //ddlSupplierName.Enabled = true;
        //txtPONo.Text = SCM.SupplierSelfPO.SuppliersSelfPO_AutoGenCode();
        //txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //btnSave.Text = "Save";
        //btnSave.Enabled = true;
        //tblFixedPODetails.Visible = true;
        
        //gvPOItems.DataBind();
        //txtNetAmount.Text = "0";
        //txtTermsConditions.Text = "Delivery: " + Environment.NewLine + "Excise Duty: " + Environment.NewLine + "Warranty: ";
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            FixedPOSave();
        }
        else if (btnSave.Text == "Update")
        {
            FixedPOUpdate();
        }
        gvFixedPODetails.SelectedIndex = -1;
    }
    #endregion

    #region SalesOrderSave
    private void FixedPOSave()
    {
        if (gvPOItems.Rows.Count > 0)
        {
            try
            {
                SCM.SupplierSelfPO objSCM = new SCM.SupplierSelfPO();
                SCM.BeginTransaction();
                objSCM.FPONo = txtPONo.Text;
                objSCM.FPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
                
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
                objSCM.CpId = ddlCompany.SelectedItem.Value;
                objSCM.Remitance = txtRemitance.Text;
                objSCM.DeliveryAddrees = ddlDeliveryAddress.SelectedItem.Value;
                objSCM.InvoiceAddress = ddlInvoiceAddress.SelectedItem.Value;
                objSCM.CustomerCode = txtCustomerCode.Text;
                if (objSCM.SuppliersSelfPO_Save() == "Data Saved Successfully")
                {
                    objSCM.SuppliersSelfPODetails_Delete(objSCM.FPOSId);
                    foreach (GridViewRow gvrow in gvPOItems.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[2].Text;
                        objSCM.FPODetQty = gvrow.Cells[6].Text;
                        objSCM.FPODetRate = gvrow.Cells[7].Text;
                        objSCM.FPODetTax = gvrow.Cells[8].Text;
                        if (gvrow.Cells[12].Text == "")
                        {
                            objSCM.FPODetDeliveryDate = "01/01/2020";
                        }
                        else
                        {
                            objSCM.FPODetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                        }
                        objSCM.FPODetSpec = gvrow.Cells[13].Text;
                        objSCM.FPODetRemarks =  gvrow.Cells[14].Text;
                        objSCM.FPOSuppDisc = gvrow.Cells[10].Text;
                        objSCM.FPOSuppSpPrice = gvrow.Cells[11].Text;
                        objSCM.Customer = gvrow.Cells[16].Text;
                        objSCM.Color = gvrow.Cells[17].Text;
                        objSCM.ExpDateArrival = DateTime.Now.AddMonths(3).ToString();
                        objSCM.detStatus = "Pending";
                        objSCM.SuppliersSelfPODetails_Save();
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
                btnDelete.Attributes.Clear();
                gvFixedPODetails.DataBind();
                gvPOItems.DataBind();
                tblFixedPODetails.Visible = false;
                SCM.ClearControls(this);
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
                SCM.SupplierSelfPO objSCM = new SCM.SupplierSelfPO();
                SCM.BeginTransaction();
                objSCM.FPOSId = gvFixedPODetails.SelectedRow.Cells[0].Text;
                objSCM.FPONo = txtPONo.Text;
                objSCM.FPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
                
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
                objSCM.CpId = ddlCompany.SelectedItem.Value;
                objSCM.DeliveryAddrees = ddlDeliveryAddress.SelectedItem.Value;
                objSCM.Remitance = txtRemitance.Text;
                objSCM.InvoiceAddress = ddlInvoiceAddress.SelectedItem.Value;
                objSCM.CustomerCode = txtCustomerCode.Text;
                if (objSCM.SuppliersSelfPO_Update() == "Data Updated Successfully")
                {
                    objSCM.SuppliersSelfPODetails_Delete(objSCM.FPOSId);
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
                        objSCM.Color = gvrow.Cells[17].Text;
                        objSCM.Customer = gvrow.Cells[16].Text;
                        objSCM.ExpDateArrival = DateTime.Now.AddMonths(3).ToString();
                        objSCM.detStatus = "Pending";
                        objSCM.SuppliersSelfPODetails_Save();
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
                btnDelete.Attributes.Clear();
                gvFixedPODetails.DataBind();
              //  gvQuotationItems.DataBind();
                gvPOItems.DataBind();
                tblFixedPODetails.Visible = false;
                SCM.ClearControls(this);
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

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            Response.Redirect("SelfPurchaseOrderDetailsNew.aspx?poNo=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "&status=" + gvFixedPODetails.SelectedRow.Cells[9].Text);
            
            //try
            //{
                
            //    SCM.SupplierSelfPO objSCM = new SCM.SupplierSelfPO();
            //    if (objSCM.SuppliersSelfPO_Select(gvFixedPODetails.SelectedRow.Cells[0].Text) > 0)
            //    {
                   
            //        btnSave.Text = "Update";
            //        btnSave.Enabled = true;
            //        btnRefresh.Visible = true;
            //        tblFixedPODetails.Visible = true;

            //        txtPONo.Text = objSCM.FPONo;
            //        txtPODate.Text = objSCM.FPODate;
                    
            //       // ddlQuotationNo_SelectedIndexChanged(sender, e);
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
            //        ddlDeliveryAddress.SelectedValue = objSCM.DeliveryAddrees;
            //        ddlCompany.SelectedValue = objSCM.CpId;
            //        ddlInvoiceAddress.SelectedValue = objSCM.InvoiceAddress;
            //        txtCustomerCode.Text = objSCM.CustomerCode;
            //        objSCM.SuppliersSelfPODetails_Select(gvFixedPODetails.SelectedRow.Cells[0].Text, gvPOItems);
            //    } 

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message.ToString());
            //}
            //finally
            //{
            //    btnDelete.Attributes.Clear();
            //}
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                SCM.SupplierSelfPO objSCM = new SCM.SupplierSelfPO();
                MessageBox.Show(this, objSCM.SuppliersSelfPO_Delete(gvFixedPODetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvFixedPODetails.DataBind();
                SCM.ClearControls(this);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
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
                txtSupEmail.Text = objSupMast.SupEmail;
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
       
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        txtDiscount.Text = string.Empty;
        txtCSTTax.Text = string.Empty;
        gvPOItems.DataBind();
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblFixedPODetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtDisc.Text == "")
        {
            txtDisc.Text = "0";
        }

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
        col = new DataColumn("Customer");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
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
                        drnew["Customer"] = txtCustomerName.Text;
                        drnew["Color"] = ddlcolor.SelectedItem.Text;
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
                        dr["Customer"] = gvrow.Cells[16].Text;
                        dr["Color"] = gvrow.Cells[17].Text;


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
                    dr["Customer"] = gvrow.Cells[16].Text;
                    dr["Color"] = gvrow.Cells[17].Text;


                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }

        ////if (gvPOItems.Rows.Count > 0)
        ////{
        ////    if (gvPOItems.SelectedIndex == -1)
        ////    {
        ////        foreach (GridViewRow gvrow in gvPOItems.Rows)
        ////        {
        ////            if (gvrow.Cells[2].Text == ddlItemType.SelectedItem.Value)
        ////            {
        ////                gvPOItems.DataSource = SalesOrderItems;
        ////                gvPOItems.DataBind();
        ////                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
        ////                return;
        ////            }
        ////        }
        ////    }
        ////}

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
            drnew["Customer"] = txtCustomerName.Text;
            drnew["Color"] = ddlcolor.SelectedItem.Text;
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
       // ddlItemType.SelectedValue = "0";
       // ItemTypesAll_Fill();
        txtSearchModel.Text = "";
        txtModelName.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        txtDisc.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtTax.Text = string.Empty;
        txtItemSpecifications.Text = string.Empty;
        txtItemRemarks.Text = string.Empty;
        //txtDeliveryDate.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        ddlcolor.SelectedValue = "0";
        txtCustomerName.Text = "";
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
        col = new DataColumn("Customer");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
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
                dr["Customer"] = gvrow.Cells[16].Text;
                dr["Color"] = gvrow.Cells[17].Text;


                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvPOItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
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
                    txtCustomerName.Text = gvrow.Cells[16].Text;
                    ddlcolor.SelectedItem.Text = gvrow.Cells[17].Text;

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
                   }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;//Tax is not applicable
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[13].Visible = false;
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
            e.Row.Cells[13].Visible = false;
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
        col = new DataColumn("Customer");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
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
                    dr["Customer"] = gvrow.Cells[16].Text;
                    dr["Color"] = gvrow.Cells[17].Text;

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

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "PO Date")
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

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvFixedPODetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvFixedPODetails.DataBind();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Selfpurchaseorder&fpoid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    

    #region gvApprlItemDetails_RowDataBound
    protected void gvApprlItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[1].Visible = false;
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
            SCM.SupplierSelfPO objSCMPOApprove = new SCM.SupplierSelfPO();
            SCM.BeginTransaction();
            objSCMPOApprove.SuppliersSelfPOApprove_Update(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId), gvFixedPODetails.SelectedRow.Cells[0].Text);
            objSCMPOApprove.SuppliersSelfPOStatus_Update("Open", gvFixedPODetails.SelectedRow.Cells[0].Text);
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
            gvFixedPODetails.DataBind();
            btnEdit_Click(sender, e);
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

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemType.DataSourceID = "SqlDataSource2";
        ddlItemType.DataTextField = "ITEM_MODEL_NO";
        ddlItemType.DataValueField = "ITEM_CODE";
        ddlItemType.DataBind();
        ddlItemType_SelectedIndexChanged(sender, e);
    }



    #region Item TypesAll Fill
    private void ItemTypesAll_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMasterself_Select(ddlItemType);
            // SM.SalesEnquiry.SalesEnquiryItemTypes1_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();            
            SM.Dispose();
        }
    }
    #endregion



    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/Email.aspx?type=Selfpurchaseorder&fpoid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "" +
                "&qnono=" + ddlSupplierName.SelectedItem.Text + "" +
                "&empid=" + Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId) + "" +
                "&custemail=" + txtSupEmail.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SelfpurchaseorderImport&fpoid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnSendImEmail_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/Email.aspx?type=SelfpurchaseorderImport&fpoid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "" +
                "&qnono=" + ddlSupplierName.SelectedItem.Text + "" +
                "&empid=" + Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId) + "" +
                "&custemail=" + txtSupEmail.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster5_Select(ddlItemType, ddlBrand.SelectedItem.Value);
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFixedPODetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvFixedPODetails.DataBind();
    }
}

 
