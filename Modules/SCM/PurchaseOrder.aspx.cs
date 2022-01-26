using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;
using System.Data;
using vllibData;

public partial class Modules_SCM_PurchaseOrder : basePage
{
    string type;
    string poNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        type = Request.QueryString["type"];
        poNo = Request.QueryString["poNo"];
        if (!IsPostBack)
        {
            setControlsVisibility();

            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
            Masters.CompanyProfile.Company_Select(ddlCompanyName);
            Masters.DeliveryAddress.DeliveryAddress_Select(ddlBillingUnit);
            Masters.DeliveryAddress.DeliveryAddress_Select(ddlShippingUnit);
            txtPONo.Text = SCM.SupplierFixedPO.SuppliersFixedPO_AutoGenCode();
            txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCPID.Text = cp.getPresentCompanySessionValue();


            if (poNo != null)
            {
                try
                {
                    SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                    if (objSCM.SuppliersFixedPO_Select(poNo) > 0)
                    {
                        //rbByIndentApproval_CheckedChanged(sender, e);
                        //rbByQuotation_CheckedChanged(sender, e);
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblPurchaseOrder.Visible = true;
                        txtPONo.Text = objSCM.FPONo;
                        txtPODate.Text = objSCM.FPODate;
                        ddlSupplierName.SelectedValue = objSCM.SupId;
                        ddlSupplierName_SelectedIndexChanged(sender, e);
                        Table5.Visible = true;
                        if (objSCM.SupQuotId == "0")
                        {
                            //IndentApprovalNo_Fill();
                            rbnIndent.Checked = true;
                            // lblQuotNo.Text = "Indent Approval No.";
                            //lblQuotDate.Text = "Indent Approval Date";
                            rbnProformaIV.Checked = false;
                            rbnProformaIV.Enabled = false;
                            lblInvoiceNo.Visible = false;
                            ddlInvoiceNo.Visible = false;
                            //ddlQuotationNo.SelectedValue = objSCM.IndApprlId;
                        }
                        else
                        {
                            //QuotationMaster_Fill();
                            SCM.SuppliersQuotation.SuppliersQuotationPI_Select(ddlInvoiceNo, ddlSupplierName.SelectedItem.Value);
                            rbnProformaIV.Checked = true;
                            rbnIndent.Checked = false;
                            //lblQuotNo.Text = "Quotation No.";
                            //lblQuotDate.Text = "Quotation Date";
                            rbnIndent.Enabled = false;
                            ddlInvoiceNo.SelectedValue = objSCM.SupQuotId.Trim();
                           // ddlInvoiceNo_SelectedIndexChanged(sender, e);
                        }

                        txtItemDate.Text = objSCM.FPOFreight;
                        txtCustCode.Text = objSCM.FPOInsurance;
                        txtRefNo.Text = objSCM.FPODestination;
                        txtRemarks.Text = objSCM.FPOAmtWords;
                        txtTerms.Text = objSCM.FPOTermsConds;
                        txtPackingCharges.Text = objSCM.PackingCharges;
                        txtInsurance.Text = objSCM.Insurance;
                        txtCifCharges.Text = objSCM.CIFCharges;
                        txtFobCharges.Text = objSCM.FOBCharges;
                        txtTaxes.Text = objSCM.FPOTaxCST;
                        lblNetAmount.Text = objSCM.FPONetAmount;
                        txtPackingCharges.Text = objSCM.PackingCharges;
                        lblTtlAmt.Text = objSCM.FPOTotalAmt;
                        ddlBillingUnit.SelectedValue = objSCM.FPOFOBCharges;
                        ddlBillingUnit_SelectedIndexChanged(sender, e);
                        ddlCompanyName.SelectedValue = objSCM.FPOCIFCharges;
                        ddlShippingUnit.SelectedValue = objSCM.FPOSuppContactPerson;
                        ddlShippingUnit_SelectedIndexChanged(sender, e);


                        objSCM.SuppliersPODetails_Select2(poNo, gvProductDetails);
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

            if (type == "Indent")
            {
                tblPurchaseOrder.Visible = true;
                rbnIndent.Checked = true;
                rbnProformaIV.Enabled = false;
                lblInvoiceNo.Visible = false;
                ddlInvoiceNo.Visible = false;
                SCM.SupplierFixedPO obj = new SCM.SupplierFixedPO();
                obj.SupplierIndentPO_Select(gvProductDetails);
            }
            else
            {
                //tblPurchaseOrder.Visible = true;
                if (poNo == null)
                {
                    rbnProformaIV.Checked = true;                               
                    rbnIndent.Enabled = false;
                }
            }
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "23");
        btnSave.Enabled = up.add;
    }
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SCM.SuppliersMaster obj = new SCM.SuppliersMaster();
        if (obj.SuppliersMaster_Select(ddlSupplierName.SelectedValue) > 0)
        {
            txtSupplierAddress.Text = obj.SupAddress;
            SCM.SuppliersMaster.SuppliersUnits_Select(ddlSupUnitName, ddlSupplierName.SelectedItem.Value);

        }
        if (rbnProformaIV.Checked == true)
        {
            SCM.SuppliersQuotation.SuppliersQuotationPI_Select(ddlInvoiceNo, ddlSupplierName.SelectedItem.Value);
        }
    }
    protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SCM.SupplierFixedPO obj = new SCM.SupplierFixedPO();
        obj.SuppliersPODetails_Select(ddlInvoiceNo.SelectedItem.Value, gvProductDetails);
        Table5.Visible = true;

    }

    protected void gvProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal Amount = 0;
        if (gvProductDetails.Rows.Count > 0)
        {
            if (e.Row.Cells[10].Text != "")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Amount = Amount + Convert.ToDecimal(e.Row.Cells[10].Text);
                }

                //if (e.Row.RowType == DataControlRowType.Footer)
                //{
                //    // txtSubTotal.Text = NetAmountCalc().ToString();
                //    e.Row.Cells[9].Text = "TotalAmount:";
                //    e.Row.Cells[10].Text = Amount.ToString();

                //}
            }
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddlcurency = (DropDownList)e.Row.FindControl("ddlCurrency");
        //    SM.DDLBindWithSelect(ddlcurency, "SELECT CURRENCY_ID,CURRENCY_NAME FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null");
        //}
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (rbnProformaIV.Checked == true)
            {
                e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
                TextBox qty = (TextBox)row.FindControl("txtQuantity");
                TextBox price = (TextBox)row.FindControl("txtMRP");
                Label amount = (Label)row.FindControl("lblRate");
                amount.Text = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(price.Text)).ToString();
                Label ttlAmt = (Label)row.FindControl("lblAmount");
                ttlAmt.Text = amount.Text;
                if (poNo == null)
                {
                    lblTtlAmt.Text = (Convert.ToDecimal(lblTtlAmt.Text) + Convert.ToDecimal(amount.Text)).ToString();
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            if(rbnIndent.Checked==true)
            {
                e.Row.Cells[7].Visible = false;
            }
        }

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[1].Visible = false;
        //    e.Row.Cells[11].Visible = false;
        //    e.Row.Cells[12].Visible = false;
        //    e.Row.Cells[13].Visible = false;

        //    e.Row.Cells[10].Text = NetAmountCalc().ToString();
        //    e.Row.Cells[9].Text = "TotalAmount:";
        //   // e.Row.Cells[10].Text = Amount.ToString();
        //    //e.Row.Cells[8].Visible = false;//Tax is not applicable
        //    //e.Row.Cells[15].Visible = false;
        //}

    }
    private double NetAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvProductDetails.Rows)
        {
            Label amt = (Label)gvrow.FindControl("lblAmount");
            if (amt.Text != "")
            {
                _totalAmt = _totalAmt + Convert.ToDouble(amt.Text);
            }
            else
            {
                _totalAmt = _totalAmt + 0;

            }
        }
        return _totalAmt;
    }

    protected void ddlBillingUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.DeliveryAddress obj = new Masters.DeliveryAddress();
        if (obj.Deliveryadd_sele(ddlBillingUnit.SelectedItem.Value) > 0)
        {
            txtBillingAdd.Text = obj.Insaddress;
        }
    }
    protected void ddlShippingUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.DeliveryAddress obj = new Masters.DeliveryAddress();
        if (obj.Deliveryadd_sele(ddlShippingUnit.SelectedItem.Value) > 0)
        {
            txtShippingAdd.Text = obj.Insaddress;
        }
    }
    protected void txtMRP_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvProductDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtMRP");
            TextBox qty = (TextBox)gvr.FindControl("txtQuantity");
            Label amount = (Label)gvr.FindControl("lblRate");
            Label splAmt = (Label)gvr.FindControl("lblAmount");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            if (rate.Text != "" && qty.Text != "")
            {
                if (discount.Text != "")
                {
                    string disc;
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                    splAmt.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                    lblTtlAmt.Text = NetAmountCalc().ToString();
                }
                else
                {
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    splAmt.Text = amount.Text;
                    lblTtlAmt.Text = NetAmountCalc().ToString();

                }
            }
            else
            {
                amount.Text = "0";
                lblTtlAmt.Text = NetAmountCalc().ToString();

            }
        }
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvProductDetails.Rows)
        {
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            Label splAmt = (Label)gvr.FindControl("lblAmount");
            Label amount = (Label)gvr.FindControl("lblRate");
            string disc="0";
            if (amount.Text != "" && discount.Text != "")
            {
                disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                splAmt.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
            else
            {
                splAmt.Text = amount.Text;
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            FixedPOSave();
           // Response.Redirect("PurchaseOrderDetails.aspx");
            // tblPurchaseOrder.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            FixedPOUpdate();
           // Response.Redirect("PurchaseOrderDetails.aspx");

        }
    }


    private void FixedPOSave()
    {
        if (gvProductDetails.Rows.Count > 0)
        {
            try
            {
                SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                //SCM.BeginTransaction();
                objSCM.FPONo = txtPONo.Text;
                objSCM.FPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
                if (rbnIndent.Checked == true)
                {
                    objSCM.IndApprlId = "0";
                    objSCM.SupQuotId = "0";
                }
                else if (rbnProformaIV.Checked == true)
                {
                    objSCM.SupQuotId = ddlInvoiceNo.SelectedItem.Value;
                    objSCM.IndApprlId = "0";
                }

                objSCM.SupId = ddlSupplierName.SelectedItem.Value;
                objSCM.FPOSuppRef = "0";
                //SUPPLIER UNIT NAME
                objSCM.DespmId = ddlSupUnitName.SelectedItem.Value;
                objSCM.FPOPOStatus = "Ordered";
                objSCM.FPOTermsConds = txtTerms.Text;
                //REFERENCE NO
                objSCM.FPODestination = txtRefNo.Text;
                //CUSTOMER CODE
                objSCM.FPOInsurance = txtCustCode.Text;
                //ITEM DATE
                objSCM.FPOFreight = Yantra.Classes.General.toMMDDYYYY(txtItemDate.Text);

                objSCM.FPONetAmount = hfNetAmount.Value; // lblNetAmount.Text;
                //objSCM.FPONetAmount = "0";

                //REMARKS
                objSCM.FPOAmtWords = txtRemarks.Text;
                //COMPANY NAME
                objSCM.FPOCIFCharges = ddlCompanyName.SelectedItem.Value;
                //BILLING UNIT NAME
                objSCM.FPOFOBCharges = ddlBillingUnit.SelectedItem.Value;
                //SHIPPING UNIT NAME
                objSCM.FPOSuppContactPerson = ddlShippingUnit.SelectedItem.Value;
                objSCM.FPOCurrencyType = "0";


                objSCM.FPOPaymentTerms = "0";

                objSCM.CurrencyId = "0";
                objSCM.FPONetAmtInOtherCurrency = "0";
                objSCM.FPODiscount = "0";
                objSCM.FPOTaxCST = txtTaxes.Text;
                objSCM.FPOTermsOfDelivery = "0";
                objSCM.FPOContactPerson = "-";
                objSCM.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.CpId = lblCPID.Text;
                //Freight
                objSCM.Remitance = txtFreight.Text;
                objSCM.Insurance = txtInsurance.Text;
                objSCM.FOBCharges = txtFobCharges.Text;
                objSCM.CIFCharges = txtCifCharges.Text;
                objSCM.PackingCharges = txtPackingCharges.Text;
                objSCM.FPOTotalAmt = lblTtlAmt.Text;

                if (objSCM.SuppliersFixedPO_Save() == "Data Saved Successfully")
                {
                    objSCM.SuppliersFixedPODetails_Delete(objSCM.FPOId);
                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[1].Text;
                        TextBox quantity = (TextBox)gvrow.FindControl("txtQuantity");
                        objSCM.FPODetQty = quantity.Text;
                        Label rate = (Label)gvrow.FindControl("lblRate");
                        objSCM.FPODetRate = rate.Text;
                        //MRP
                        TextBox mrp = (TextBox)gvrow.FindControl("txtMRP");

                        objSCM.FPODetTax = mrp.Text;

                        objSCM.FPODetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtItemDate.Text);
                        //DESCRIPTION
                        TextBox desc = (TextBox)gvrow.FindControl("txtDesc");
                        objSCM.FPODetSpec = desc.Text;
                        //COLOR ID
                        objSCM.FPODetRemarks = gvrow.Cells[14].Text;
                        TextBox discount = (TextBox)gvrow.FindControl("txtDiscount");
                        objSCM.FPOSuppDisc = discount.Text;
                        Label splPrice = (Label)gvrow.FindControl("lblAmount");
                        objSCM.FPOSuppSpPrice = splPrice.Text;
                        objSCM.FPODetIndId = gvrow.Cells[12].Text;
                        objSCM.FPODetIndDetId = gvrow.Cells[13].Text;
                        objSCM.colorid = gvrow.Cells[14].Text;
                        objSCM.BalanceQty = quantity.Text;
                        //DropDownList currency = (DropDownList)gvrow.FindControl("ddlCurrency");
                        //objSCM.CurrencyType = currency.SelectedItem.Value;
                        objSCM.CurrencyType = gvrow.Cells[7].Text;

                        string ttlQty = quantity.Text;
                        string dId = gvrow.Cells[13].Text;

                        SCM.IndentApproval hai = new SCM.IndentApproval();
                        if (hai.IndentDetQty_Select(dId) > 0)
                        {
                            string qty = hai.Indentqty;
                            string ordQty = hai.IndOrdqty;
                            ordQty = (Convert.ToInt32(ordQty) + Convert.ToInt32(ttlQty)).ToString();
                            if (Convert.ToInt32(ordQty) >= Convert.ToInt32(qty))
                            {
                                hai.IndentStatus_Update(ordQty, "Close", dId);
                            }
                            else if (Convert.ToInt32(ordQty) < Convert.ToInt32(qty))
                            {
                                hai.IndentStatus_Update(ordQty, "New", dId);
                            }
                        }

                        objSCM.SuppliersFixedPODetails_Save();
                    }
                    //SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    // SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                // SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                // btnDelete.Attributes.Clear();
                //gvFixedPODetails.DataBind();

                //tblPurchaseOrder.Visible = false;
                SCM.ClearControls(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
       "alert(' Purchase Order Saved sucessfully');window.location ='PurchaseOrderDetails.aspx';", true);
                // SCM.Dispose();
            }
        }
    }
    private void FixedPOUpdate()
    {
        if (gvProductDetails.Rows.Count > 0)
        {
            try
            {
                SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                //SCM.BeginTransaction();
                objSCM.FPOId = poNo;
                objSCM.FPONo = txtPONo.Text;
                objSCM.FPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
                if (rbnIndent.Checked == true)
                {
                    objSCM.IndApprlId = "0";
                    objSCM.SupQuotId = "0";
                }
                else if (rbnProformaIV.Checked == true)
                {
                    objSCM.SupQuotId = ddlInvoiceNo.SelectedItem.Value;
                    objSCM.IndApprlId = "0";
                }

                objSCM.SupId = ddlSupplierName.SelectedItem.Value;
                objSCM.FPOSuppRef = "0";
                //SUPPLIER UNIT NAME
                objSCM.DespmId = ddlSupUnitName.SelectedItem.Value;
                objSCM.FPOPOStatus = "Ordered";
                objSCM.FPOTermsConds = txtTerms.Text;
                //REFERENCE NO
                objSCM.FPODestination = txtRefNo.Text;
                //CUSTOMER CODE
                objSCM.FPOInsurance = txtCustCode.Text;
                //ITEM DATE
                objSCM.FPOFreight = Yantra.Classes.General.toMMDDYYYY(txtItemDate.Text);

                //objSCM.FPONetAmount = lblNetAmount.Text;
                objSCM.FPONetAmount = hfNetAmount.Value; // lblNetAmount.Text;

                //REMARKS
                objSCM.FPOAmtWords = txtRemarks.Text;
                //COMPANY NAME
                objSCM.FPOCIFCharges = ddlCompanyName.SelectedItem.Value;
                //BILLING UNIT NAME
                objSCM.FPOFOBCharges = ddlBillingUnit.SelectedItem.Value;
                //SHIPPING UNIT NAME
                objSCM.FPOSuppContactPerson = ddlShippingUnit.SelectedItem.Value;
                objSCM.FPOCurrencyType = "0";


                objSCM.FPOPaymentTerms = "0";

                objSCM.CurrencyId = "0";
                objSCM.FPONetAmtInOtherCurrency = "0";
                objSCM.FPODiscount = "0";
                objSCM.FPOTaxCST = txtTaxes.Text;
                objSCM.FPOTermsOfDelivery = "0";
                objSCM.FPOContactPerson = "-";
                objSCM.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.CpId = lblCPID.Text;
                //Freight
                objSCM.Remitance = txtFreight.Text;
                objSCM.Insurance = txtInsurance.Text;
                objSCM.FOBCharges = txtFobCharges.Text;
                objSCM.CIFCharges = txtCifCharges.Text;
                objSCM.PackingCharges = txtPackingCharges.Text;
                objSCM.FPOTotalAmt = lblTtlAmt.Text;

                if (objSCM.SuppliersFixedPO_Update() == "Data Updated Successfully")
                {
                    objSCM.SuppliersFixedPODetails_Delete(objSCM.FPOId);
                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {
                        objSCM.ItemCode = gvrow.Cells[1].Text;
                        TextBox quantity = (TextBox)gvrow.FindControl("txtQuantity");
                        objSCM.FPODetQty = quantity.Text;
                        Label rate = (Label)gvrow.FindControl("lblRate");
                        objSCM.FPODetRate = rate.Text;
                        //MRP
                        TextBox mrp = (TextBox)gvrow.FindControl("txtMRP");

                        objSCM.FPODetTax = mrp.Text;

                        objSCM.FPODetDeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtItemDate.Text);
                        //DESCRIPTION
                        TextBox desc = (TextBox)gvrow.FindControl("txtDesc");
                        objSCM.FPODetSpec = desc.Text;
                        //COLOR ID
                        objSCM.FPODetRemarks = gvrow.Cells[14].Text;
                        TextBox discount = (TextBox)gvrow.FindControl("txtDiscount");
                        objSCM.FPOSuppDisc = discount.Text;
                        Label splPrice = (Label)gvrow.FindControl("lblAmount");
                        objSCM.FPOSuppSpPrice = splPrice.Text;
                        objSCM.FPODetIndId = gvrow.Cells[12].Text;
                        objSCM.FPODetIndDetId = gvrow.Cells[13].Text;
                        objSCM.colorid = gvrow.Cells[14].Text;
                        objSCM.BalanceQty = quantity.Text;
                        //DropDownList currency = (DropDownList)gvrow.FindControl("ddlCurrency");
                        //objSCM.CurrencyType = currency.SelectedItem.Value;
                        if (gvrow.Cells[7].Text == "" || gvrow.Cells[7].Text == null)
                        {
                            objSCM.CurrencyType = "Rs";
                        }
                        else
                        {
                            objSCM.CurrencyType = gvrow.Cells[7].Text;
                        }
                        string ttlQty = quantity.Text;
                        string dId = gvrow.Cells[13].Text;

                        objSCM.SuppliersFixedPODetails_Save();
                    }
                    //SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    // SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                // SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                SCM.ClearControls(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
     "alert(' Purchase Order Updated sucessfully');window.location ='PurchaseOrderDetails.aspx';", true);
                //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Fixed Purchase Order");
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseOrderDetails.aspx");
    }



    protected void gvProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvProductDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable SuppliersFixedPOItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Desc");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Color");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("IndId");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("IndDetId");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Currency");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("MRP");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Discount");
        SuppliersFixedPOItems.Columns.Add(col);
        col = new DataColumn("Amount");
        SuppliersFixedPOItems.Columns.Add(col);
        if (gvProductDetails.Rows.Count > 0)
        {

            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ModelNo"] = gvrow.Cells[2].Text;
                    TextBox desc = (TextBox)gvrow.FindControl("txtDesc");
                    dr["Desc"] = desc.Text;
                    dr["Color"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    TextBox quantity = (TextBox)gvrow.FindControl("txtQuantity");
                    dr["Quantity"] = quantity.Text;
                    dr["IndId"] = gvrow.Cells[4].Text;
                    dr["IndDetId"] = gvrow.Cells[5].Text;
                    dr["ColorId"] = gvrow.Cells[1].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    TextBox mrp = (TextBox)gvrow.FindControl("txtMRP");
                    dr["MRP"] = mrp.Text;
                    Label rate = (Label)gvrow.FindControl("lblRate");
                    dr["Rate"] = rate.Text;
                    TextBox discount = (TextBox)gvrow.FindControl("txtDiscount");
                    dr["Discount"] = discount.Text;
                    Label amount = (Label)gvrow.FindControl("lblAmount");
                    dr["Amount"] = amount.Text;
                    SuppliersFixedPOItems.Rows.Add(dr);
                }
            }
        }
        gvProductDetails.DataSource = SuppliersFixedPOItems;
        gvProductDetails.DataBind();
    }
   

    protected void btnPO1_Click(object sender, EventArgs e)
    {
        SupplierPO spo = new SupplierPO();
        DataTable dt = spo.pdt;

        //dt.Rows.Add("ItemCode", "Desc", "Color", "Qty");
        foreach (GridViewRow gvr in gvProductDetails.Rows)
        {
            string desc = ((TextBox)gvr.FindControl("txtDesc")).Text;
            string qty = ((TextBox)gvr.FindControl("txtQuantity")).Text;

            dt.Rows.Add(gvr.Cells[2].Text, desc, gvr.Cells[4].Text, qty);

        }

        spo.loadDataToExcel(ddlSupplierName.SelectedValue);

    }

    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvProductDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtMRP");
            TextBox qty = (TextBox)gvr.FindControl("txtQuantity");
            Label amount = (Label)gvr.FindControl("lblRate");
            Label splAmt = (Label)gvr.FindControl("lblAmount");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            if (rate.Text != "" && qty.Text != "")
            {
                if (discount.Text != "")
                {
                    string disc;
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                    splAmt.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                    lblTtlAmt.Text = NetAmountCalc().ToString();
                }
                else
                {
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    splAmt.Text = amount.Text;
                    lblTtlAmt.Text = NetAmountCalc().ToString();

                }
            }
            else
            {
                amount.Text = "0";
                lblTtlAmt.Text = NetAmountCalc().ToString();

            }
        }
    }
   
}
 
