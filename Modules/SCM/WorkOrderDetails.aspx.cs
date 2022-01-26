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

public partial class Modules_PurchasingManagement_WorkOrderDetails : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtItemQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtItemRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtCSTTax.Attributes.Add("onkeyup", "javascript:netamtcalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:netamtcalc();");

            DeliveryType_Fill();
            ItemTypes_Fill();
            CurrencyType_Fill();
            EmployeeMaster_Fill();
        }
        txtNetAmount.Text = NetAmountCalc().ToString();
        if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
        if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
        txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100));
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        txtNetAmount.Text = NetAmountCalc().ToString();
        if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
        if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
        txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100));

        if (gvFixedPODetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvFixedPODetails.SelectedRow.Cells[9].Text) && gvFixedPODetails.SelectedRow.Cells[9].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
        }
    }

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemType);
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

    //#region Item Name Fill
    //private void ItemName_Fill()
    //{
    //    try
    //    {
    //        Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion

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
            HR.Dispose();
        }
    }
    #endregion

    #region Currency Type Fill
    private void CurrencyType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlCurrencyType);
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
            Masters.Dispose();
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
                txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemRate.Text = objMaster.ItemRate;
                txtItemSpecifications.Text = objMaster.ItemSpec;
               

                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

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
            SCM.SupplierWorkOrder objSCM = new SCM.SupplierWorkOrder();
            if (objSCM.SupplierWorkOrder_Select(gvFixedPODetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblFixedPODetails.Visible = true;

                txtWONo.Text = objSCM.SupWoNo;
                if (objSCM.SupWoIndigenousForeign == rbIndigenous.Text)
                {
                    lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = true;
                    rbIndigenous.Checked = true;
                    rbForeign.Checked = false;
                    chklIndigenous.Visible = true;
                    chklForeign.Visible = false;
                }
                else if (objSCM.SupWoIndigenousForeign == rbForeign.Text)
                {
                    lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = false;
                    rbForeign.Checked = true;
                    rbIndigenous.Checked = false;
                    chklForeign.Visible = true;
                    chklIndigenous.Visible = false;
                }
                txtWODate.Text = objSCM.SupWoDate;
                txtSupplierName.Text = objSCM.SupWoSupplierName;
                txtContactPerson.Text = objSCM.SupWoSupplierContactPerson;
                txtAddress.Text = objSCM.SupWoSupplierAddress;
                txtContactNo1.Text = objSCM.SupWoSupplierPhone;
                txtContactNo2.Text = objSCM.SupWoSupplierMobile;
                txtEmail.Text = objSCM.SupWoSupplierEmail;
                txtFaxNo.Text = objSCM.SupWoSupplierFaxNo;

                ddlDespatchMode.SelectedValue = objSCM.DespmId;
                txtTermsConditions.Text = objSCM.SupWoTermsConditions;
                txtNetAmount.Text = "Rs." + objSCM.SupWoNetAmount;
                txtAmountInWords.Text = objSCM.SupWoAmtInWords;
                if (chklIndigenous.Visible == true)
                {
                    SelectCheckBoxFromString(objSCM.SupWoPaymentTerms, chklIndigenous);
                }
                else if (chklForeign.Visible == true)
                {
                    SelectCheckBoxFromString(objSCM.SupWoPaymentTerms, chklForeign);
                }
                ddlCurrencyType.SelectedValue = objSCM.CurrencyId;
                txtNetAmtInOtherCurrency.Text = objSCM.SupWoNetAmountInOtherCurrency;
                txtDestination.Text = objSCM.SupWoDestination;
                txtInsurance.Text = objSCM.SupWoInsurance;
                txtFreight.Text = objSCM.SupWoFreight;
                txtDiscount.Text = objSCM.SupWoDiscount;
                txtCSTTax.Text = objSCM.SupWoTaxCST;
                txtTermsOfDelivery.Text = objSCM.SupWoTermsOfDelivery;
                ddlContactPerson.SelectedValue = objSCM.SupWoContactPerson;
                txtSuppReference.Text = objSCM.SupWoReference;
                ddlPreparedBy.SelectedValue = objSCM.PreparedBy;
                ddlApprovedBy.SelectedValue = objSCM.ApprovedBy;
                objSCM.SupplierWorkOrderDetails_Select(gvFixedPODetails.SelectedRow.Cells[0].Text, gvPOItems);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            SCM.Dispose();
        }

    }
    #endregion

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

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        gvFixedPODetails.SelectedIndex = -1;
        rbForeign.Checked = rbIndigenous.Checked = false;
        chklForeign.ClearSelection();
        chklIndigenous.ClearSelection();
        chklIndigenous.Visible = chklForeign.Visible = false;
        txtWONo.Text = SCM.SupplierWorkOrder.Work_AutoGenCode();
            txtWODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblFixedPODetails.Visible = true;
        gvPOItems.DataBind();
        txtNetAmount.Text = "0";
       
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
    }
    #endregion

    #region SalesOrderSave
    private void FixedPOSave()
    {
        if (gvPOItems.Rows.Count > 0)
        {
            try
            {
                SCM.SupplierWorkOrder objSCM = new SCM.SupplierWorkOrder();
                SCM.BeginTransaction();

                objSCM.SupWoNo = txtWONo.Text;
                if (rbIndigenous.Checked == true)
                {
                    objSCM.SupWoIndigenousForeign = rbIndigenous.Text;
                }
                else if (rbForeign.Checked == true)
                {
                    objSCM.SupWoIndigenousForeign = rbForeign.Text;
                }
                objSCM.SupWoDate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
                objSCM.SupWoSupplierName = txtSupplierName.Text;
                objSCM.SupWoSupplierContactPerson = txtContactPerson.Text;
                objSCM.SupWoSupplierAddress = txtAddress.Text;
                objSCM.SupWoSupplierPhone = txtContactNo1.Text;
                objSCM.SupWoSupplierMobile = txtContactNo2.Text;
                objSCM.SupWoSupplierEmail = txtEmail.Text;
                objSCM.SupWoSupplierFaxNo = txtFaxNo.Text;

                objSCM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSCM.SupWoStatus = "New";
                objSCM.SupWoTermsConditions = txtTermsConditions.Text;
                objSCM.SupWoNetAmount = txtNetAmount.Text.Replace("Rs. ", "");
                objSCM.SupWoAmtInWords = txtAmountInWords.Text;
                if (chklIndigenous.Visible == true)
                {
                    objSCM.SupWoPaymentTerms = CheckBoxSelectedListInString(chklIndigenous);
                }
                else if (chklForeign.Visible == true)
                {
                    objSCM.SupWoPaymentTerms = CheckBoxSelectedListInString(chklForeign);
                }
                objSCM.CurrencyId = ddlCurrencyType.SelectedItem.Value;
                objSCM.SupWoNetAmountInOtherCurrency = txtNetAmtInOtherCurrency.Text;
                objSCM.SupWoDestination = txtDestination.Text;
                objSCM.SupWoInsurance = txtInsurance.Text;
                objSCM.SupWoFreight = txtFreight.Text;
                objSCM.SupWoDiscount = txtDiscount.Text;
                objSCM.SupWoTaxCST = txtCSTTax.Text;
                objSCM.SupWoTermsOfDelivery = txtTermsOfDelivery.Text;
                objSCM.SupWoContactPerson = ddlContactPerson.SelectedItem.Value;
                objSCM.SupWoReference = txtSuppReference.Text;
                objSCM.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.ApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objSCM.SupplierWorkOrder_Save() == "Data Saved Successfully")
                {
                    objSCM.SupplierWorkOrderDetails_Delete(objSCM.SupWoId);
                    foreach (GridViewRow gvrow in gvPOItems.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[2].Text;
                        objSCM.SupWoDetQty = gvrow.Cells[6].Text;
                        objSCM.SupWoDetRate = gvrow.Cells[7].Text;
                        objSCM.SupWoDetTax = gvrow.Cells[8].Text;
                        objSCM.SupWoDetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[10].Text);
                        objSCM.SupWoDetSpec = gvrow.Cells[11].Text;
                        objSCM.SupWoDetRemarks = gvrow.Cells[12].Text;

                        objSCM.SupplierWorkOrderDetails_Save();
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
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Fixed Purchase Order");
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
                SCM.SupplierWorkOrder objSCM = new SCM.SupplierWorkOrder();
                SCM.BeginTransaction();
                objSCM.SupWoId = gvFixedPODetails.SelectedRow.Cells[0].Text;
                objSCM.SupWoNo = txtWONo.Text;
                if (rbIndigenous.Checked == true)
                {
                    objSCM.SupWoIndigenousForeign = rbIndigenous.Text;
                }
                else if (rbForeign.Checked == true)
                {
                    objSCM.SupWoIndigenousForeign = rbForeign.Text;
                }
                objSCM.SupWoDate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
                objSCM.SupWoSupplierName = txtSupplierName.Text;
                objSCM.SupWoSupplierContactPerson = txtContactPerson.Text;
                objSCM.SupWoSupplierAddress = txtAddress.Text;
                objSCM.SupWoSupplierPhone = txtContactNo1.Text;
                objSCM.SupWoSupplierMobile = txtContactNo2.Text;
                objSCM.SupWoSupplierEmail = txtEmail.Text;
                objSCM.SupWoSupplierFaxNo = txtFaxNo.Text;
                objSCM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSCM.SupWoStatus = "New";
                objSCM.SupWoTermsConditions = txtTermsConditions.Text;
                objSCM.SupWoNetAmount = txtNetAmount.Text.Replace("Rs. ", "");
                objSCM.SupWoAmtInWords = txtAmountInWords.Text;
                if (chklIndigenous.Visible == true)
                {
                    objSCM.SupWoPaymentTerms = CheckBoxSelectedListInString(chklIndigenous);
                }
                else if (chklForeign.Visible == true)
                {
                    objSCM.SupWoPaymentTerms = CheckBoxSelectedListInString(chklForeign);
                }
                objSCM.CurrencyId = ddlCurrencyType.SelectedItem.Value;
                objSCM.SupWoNetAmountInOtherCurrency = txtNetAmtInOtherCurrency.Text;
                objSCM.SupWoDestination = txtDestination.Text;
                objSCM.SupWoInsurance = txtInsurance.Text;
                objSCM.SupWoFreight = txtFreight.Text;
                objSCM.SupWoDiscount = txtDiscount.Text;
                objSCM.SupWoTaxCST = txtCSTTax.Text;
                objSCM.SupWoTermsOfDelivery = txtTermsOfDelivery.Text;
                objSCM.SupWoContactPerson = ddlContactPerson.SelectedItem.Value;
                objSCM.SupWoReference = txtSuppReference.Text;
                objSCM.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.ApprovedBy = ddlApprovedBy.SelectedItem.Value;
                if (objSCM.SupplierWorkOrder_Update() == "Data Updated Successfully")
                {
                    objSCM.SupplierWorkOrderDetails_Delete(objSCM.SupWoId);
                    foreach (GridViewRow gvrow in gvPOItems.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[2].Text;
                        objSCM.SupWoDetQty = gvrow.Cells[6].Text;
                        objSCM.SupWoDetRate = gvrow.Cells[7].Text;
                        objSCM.SupWoDetTax = gvrow.Cells[8].Text;
                        objSCM.SupWoDetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[10].Text);
                        objSCM.SupWoDetSpec = gvrow.Cells[11].Text;
                        objSCM.SupWoDetRemarks = gvrow.Cells[12].Text;

                        objSCM.SupplierWorkOrderDetails_Save();
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
                gvPOItems.DataBind();
                tblFixedPODetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Fixed Purchase Order");
        }
    }
    #endregion

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



    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                SCM.SupplierWorkOrder objSCM = new SCM.SupplierWorkOrder();
                if (objSCM.SupplierWorkOrder_Select(gvFixedPODetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblFixedPODetails.Visible = true;

                    txtWONo.Text = objSCM.SupWoNo;
                    if (objSCM.SupWoIndigenousForeign == rbIndigenous.Text)
                    {
                        lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = true;

                        rbIndigenous.Checked = true;
                        rbForeign.Checked = false;
                        chklIndigenous.Visible = true;
                        chklForeign.Visible = false;
                    }
                    else if (objSCM.SupWoIndigenousForeign == rbForeign.Text)
                    {
                        lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = false;
                        rbForeign.Checked = true;
                        rbIndigenous.Checked = false;
                        chklForeign.Visible = true;
                        chklIndigenous.Visible = false;
                    }
                    txtWODate.Text = objSCM.SupWoDate;
                    txtSupplierName.Text = objSCM.SupWoSupplierName;
                    txtContactPerson.Text = objSCM.SupWoSupplierContactPerson;
                    txtAddress.Text = objSCM.SupWoSupplierAddress;
                    txtContactNo1.Text = objSCM.SupWoSupplierPhone;
                    txtContactNo2.Text = objSCM.SupWoSupplierMobile;
                    txtEmail.Text = objSCM.SupWoSupplierEmail;
                    txtFaxNo.Text = objSCM.SupWoSupplierFaxNo;

                    ddlDespatchMode.SelectedValue = objSCM.DespmId;
                    txtTermsConditions.Text = objSCM.SupWoTermsConditions;
                    txtNetAmount.Text = "Rs." + objSCM.SupWoNetAmount;
                    txtAmountInWords.Text = objSCM.SupWoAmtInWords;
                    if (chklIndigenous.Visible == true)
                    {
                        SelectCheckBoxFromString(objSCM.SupWoPaymentTerms, chklIndigenous);
                    }
                    else if (chklForeign.Visible == true)
                    {
                        SelectCheckBoxFromString(objSCM.SupWoPaymentTerms, chklForeign);
                    }
                    ddlCurrencyType.SelectedValue = objSCM.CurrencyId;
                    txtNetAmtInOtherCurrency.Text = objSCM.SupWoNetAmountInOtherCurrency;
                    txtDestination.Text = objSCM.SupWoDestination;
                    txtInsurance.Text = objSCM.SupWoInsurance;
                    txtFreight.Text = objSCM.SupWoFreight;
                    txtDiscount.Text = objSCM.SupWoDiscount;
                    txtCSTTax.Text = objSCM.SupWoTaxCST;
                    txtTermsOfDelivery.Text = objSCM.SupWoTermsOfDelivery;
                    ddlContactPerson.SelectedValue = objSCM.SupWoContactPerson;
                    txtSuppReference.Text = objSCM.SupWoReference;
                    ddlPreparedBy.SelectedValue = objSCM.PreparedBy;
                    ddlApprovedBy.SelectedValue = objSCM.ApprovedBy;

                    objSCM.SupplierWorkOrderDetails_Select(gvFixedPODetails.SelectedRow.Cells[0].Text, gvPOItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                SCM.Dispose();
            }
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
                SCM.SupplierWorkOrder objSCM = new SCM.SupplierWorkOrder();
                MessageBox.Show(this, objSCM.SupplierWorkOrder_Delete(gvFixedPODetails.SelectedRow.Cells[0].Text));
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
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion



    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
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
                        dr["DeliveryDate"] = gvrow.Cells[10].Text;
                        dr["Specifications"] = gvrow.Cells[11].Text;
                        dr["Remarks"] = gvrow.Cells[12].Text;
                        dr["ItemTypeId"] = gvrow.Cells[13].Text;

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
                    dr["DeliveryDate"] = gvrow.Cells[10].Text;
                    dr["Specifications"] = gvrow.Cells[11].Text;
                    dr["Remarks"] = gvrow.Cells[12].Text;
                    dr["ItemTypeId"] = gvrow.Cells[13].Text;

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
                    if (gvrow.Cells[1].Text == ddlItemType.SelectedItem.Value)
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
        txtModelName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtColor.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        txtTax.Text = string.Empty;
        txtItemSpecifications.Text = string.Empty;
        txtItemRemarks.Text = string.Empty;
        txtDeliveryDate.Text = string.Empty;
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
                dr["DeliveryDate"] = gvrow.Cells[10].Text;
                dr["Specifications"] = gvrow.Cells[11].Text;
                dr["Remarks"] = gvrow.Cells[12].Text;
                dr["ItemTypeId"] = gvrow.Cells[13].Text;

                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvPOItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[13].Text;
                   // ItemName_Fill();
                    ddlItemType_SelectedIndexChanged(sender, e);
                   txtModelName.Text = gvrow.Cells[2].Text;
                   txtTotal.Text = gvrow.Cells[9].Text;
                    //ddlItemName_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemRate.Text = gvrow.Cells[7].Text;
                    txtTax.Text = gvrow.Cells[8].Text;
                    txtDeliveryDate.Text = gvrow.Cells[10].Text;
                    txtItemSpecifications.Text = gvrow.Cells[11].Text;
                    txtItemRemarks.Text = gvrow.Cells[12].Text;
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
            e.Row.Cells[9].Text = (Convert.ToDouble(e.Row.Cells[7].Text) * Convert.ToDouble(e.Row.Cells[6].Text)) .ToString();
            // e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToShortDateString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;//Tax is not applicable
            e.Row.Cells[13].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtSubTotal.Text = NetAmountCalc().ToString();
        }
    }

    private double NetAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvPOItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[9].Text);
        }
        txtNetAmount.Text = _totalAmt.ToString();
        if (txtDiscount.Text == "" || txtDiscount.Text == string.Empty) { txtDiscount.Text = "0"; }
        if (txtCSTTax.Text == "" || txtCSTTax.Text == string.Empty) { txtCSTTax.Text = "0"; }
        txtNetAmount.Text = Convert.ToString(double.Parse(txtNetAmount.Text) - (double.Parse(txtDiscount.Text) * double.Parse(txtNetAmount.Text) / 100) + (double.Parse(txtCSTTax.Text) * double.Parse(txtNetAmount.Text) / 100));

        Yantra.Classes.General.NumberToEnglish convertToWords = new Yantra.Classes.General.NumberToEnglish();
        txtAmountInWords.Text = "Rs. " + convertToWords.changeCurrencyToWords(_totalAmt);
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
                    dr["DeliveryDate"] = gvrow.Cells[10].Text;
                    dr["Specifications"] = gvrow.Cells[11].Text;
                    dr["Remarks"] = gvrow.Cells[12].Text;
                    dr["ItemTypeId"] = gvrow.Cells[13].Text;
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
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=supplierworkorder&supwoid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "";
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
            e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToShortDateString();
        }

    }
    #endregion

    //#region ddlItemName_SelectedIndexChanged
    //protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
    //        {
    //            txtItemUOM.Text = objMaster.ItemUOMShort;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion

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

    protected void rbIndigenous_CheckedChanged(object sender, EventArgs e)
    {
        lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = true;
        chklIndigenous.Visible = true;
        chklForeign.Visible = false;
    }

    protected void rbForeign_CheckedChanged(object sender, EventArgs e)
    {
        lblTaxCST.Visible = lblTaxCSTPercent.Visible = txtCSTTax.Visible = false;
        chklIndigenous.Visible = false;
        chklForeign.Visible = true;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierWorkOrder objSCMPOApprove = new SCM.SupplierWorkOrder();
            SCM.BeginTransaction();
            objSCMPOApprove.SupplierWorkOrderApprove_Update(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId), gvFixedPODetails.SelectedRow.Cells[0].Text);
            objSCMPOApprove.SupplierWorkOrderStatus_Update("Open", gvFixedPODetails.SelectedRow.Cells[0].Text);
            SCM.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
            lblTaxCST.Visible = true;
            txtCSTTax.Visible = true;
            lblTaxCSTPercent.Visible = true;
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFixedPODetails.DataBind();
            SCM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{

    //}
}

 
