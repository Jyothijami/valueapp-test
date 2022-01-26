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
using vllib;

public partial class Modules_SCM_SampleReturnNew : basePage
{
    decimal TotalAmount = 0;
    string dcNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        dcNo = Request.QueryString["dcNo"];
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtIncludeVat.Text == "")
        {
            txtIncludeVat.Text = "0";
        }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtVAT.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();
        if (rbVAT.Checked == true)
        {
            //txtVAT.Style.Add("display", "");
            //lblVAT.Style.Add("display", "");
            //txtCST.Style.Add("display", "none");
            //lblCSTax.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            //txtVAT.Style.Add("display", "none");
            //lblVAT.Style.Add("display", "none");
            //txtCST.Style.Add("display", "");
            //lblCSTax.Style.Add("display", "");
        }
        #endregion
        if(!IsPostBack)
        {
            lblCompany.Text = cp.getPresentCompanySessionValue();
            Delivery_Fill();
            if (dcNo == null)
            {
               // Inventory.ClearControls(this);
                txtSalesReturnNo.Text = Inventory.SalesReturn.SalesReturn_AutoGenCode();
                txtSalesReturndate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                btnSave.Visible = true;
                tblsr.Visible = true;
                //Delivery_Fill();
                gvDeliveryChallanItems.DataBind();
                gvSalesInvoice.DataBind();
                gvItmDetails.DataBind();
                gvsales.DataBind();
            }

            else
            {
                try
                {
                    Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

                    if (objInventory.SalesReturn_Select(dcNo) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblsr.Visible = true;
                        txtSalesReturnNo.Text = objInventory.SRNo;
                        txtSalesReturndate.Text = objInventory.SRDate;
                        //ddlSalesOrderNo.SelectedValue = objInventory.SOId;
                        //ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                        ddlDeviveryNo.SelectedValue = objInventory.DCId;
                        ddlDeviveryNo_SelectedIndexChanged(sender, e);
                        ddlPreparedBy.SelectedValue = objInventory.SRPreparedBy;
                        ddlApprovedBy.SelectedValue = objInventory.SRApprovedBy;
                        //ddlInvoiceType.SelectedValue = objInventory.SIType;
                        //ddlDeliveryType.SelectedValue = objInventory.DespmId;
                        txtMiscelleneous.Text = objInventory.SRMissChrgs;
                        txtDiscount.Text = objInventory.SRDiscount;
                        txtGrossAmount.Text = objInventory.SRGrossAmt;
                        txtRemarks.Text = objInventory.SRRemarks;
                        txtCST.Text = objInventory.SRCSTax;
                        txtVAT.Text = objInventory.SRVAT;
                        ddlSalesInvoiceNo.SelectedItem.Value = objInventory.SIINVOICEID;
                        ddlSalesInvoiceNo_SelectedIndexChanged(sender, e);
                        objInventory.SalesReturnDetailsSample_Select(dcNo, gvItmDetails);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    //Inventory.Dispose();
                }
            }


            txtSalesReturndate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            //lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            //gvSalesReturnDetails.DataBind();
            rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtVAT.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtCST.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtIncludeVat.Attributes.Add("onkeyup", "javascript:Include();");
            //lblOrderedItemsHeading.Text = "Sales Ordered Items";

            EmployeeMaster_Fill();
            //CustomerName_Fill();
            Masters.CompanyProfile.Company_Select(ddlCompany);
           // tblsr.Visible = false;
           
        }
    }

    #region Delivery Fill
    private void Delivery_Fill()
    {
        try
        {
            Inventory.Delivery.DeliveryChallanSample_SelectSO(ddlDeviveryNo,cp.getPresentCompanySessionValue());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Inventory.Dispose();
        }
    }
    #endregion


    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            //  SM.DDLBindWithSelect(ddlModelNo, "select YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_SALES_INVOICE_DET,YANTRA_ITEM_MAST where YANTRA_SALES_INVOICE_DET.ITEM_CODE = YANTRA_ITEM_MAST.ITEM_CODE and YANTRA_SALES_INVOICE_DET.SI_ID = " + ddlSalesInvoiceNo.SelectedItem.Value);
            Inventory.Delivery.DeliveryDetailsItemTypes1_Select(ddlDeviveryNo.SelectedItem.Value, ddlModelNo);
            //Masters.ItemType.ItemType_Select(ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //  Inventory.Dispose();
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
            //  HR.Dispose();
        }
    }
    #endregion

    #region Delivery Fill
    private void Delivery_Fill(string SoId)
    {
        try
        {
            Inventory.Delivery.DeliveryChallanApproved_SelectSO(ddlDeviveryNo, SoId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // Inventory.Dispose();
        }
    }
    #endregion

    #region ddl DeliveryNo Select Change
    protected void ddlDeviveryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Inventory.Delivery objDelivery = new Inventory.Delivery();
            if (objDelivery.Delivery_Select(ddlDeviveryNo.SelectedItem.Value) > 0)
            {
                txtChallanDate.Text = objDelivery.DCDate;
                lblDCType.Text = objDelivery.DCType;
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objDelivery.DcCustomerid) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;
                    //txtCustomerName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }

                ItemTypesDc_Fill();

                objDelivery.DeliveryDetails_SelectInvoiceExtra2(ddlDeviveryNo.SelectedItem.Value, gvDeliveryChallanItems);
                SM.DDLBindWithSelect(ddlSalesInvoiceNo, "select SI_ID,SI_NO from YANTRA_SALES_INVOICE_MAST, YANTRA_DELIVERY_CHALLAN_MAST where YANTRA_SALES_INVOICE_MAST.DC_ID = YANTRA_DELIVERY_CHALLAN_MAST.DC_ID and YANTRA_SALES_INVOICE_MAST.DC_ID = " + ddlDeviveryNo.SelectedItem.Value);
                //Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                //objInventory.SalesInvoice_SelectDelivery(ddlDeviveryNo.SelectedItem.Value);
                // objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
                // gvItmDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            // Inventory.Dispose();
            /// SM.Dispose();
            //ddlSalesOrderNo_SelectedIndexChanged(sender, e);
        }
    }

    private void ItemTypesDc_Fill()
    {

        try
        {

            SM.DDLBindWithSelect(ddlModelNo, "select YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_DELIVERY_CHALLAN_DET,YANTRA_ITEM_MAST where YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE = YANTRA_ITEM_MAST.ITEM_CODE and YANTRA_DELIVERY_CHALLAN_DET.DC_ID = " + ddlDeviveryNo.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            // SM.Dispose();

        }

    }
    #endregion

    #region Gvitem Details RowdataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = (Convert.ToInt32(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text)).ToString();
        }
    }
    #endregion

    #region DDlModel No Change
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedItem.Value);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
               // txtRate.Text = objMaster.Rate;
                txtItemname.Text = objMaster.ItemName;

            }
            Masters.ItemPurchase objrate = new Masters.ItemPurchase();
            if (objrate.ItemPrice_Ddl(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtRate.Text = objrate.rsp;
            }
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

    #region Add Cilck
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SPPrice");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("Color");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("GodownId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("CompanyId");
        SalesInvoiceProducts.Columns.Add(col);






        if (gvsales.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvsales.Rows)
            {
                if (gvsales.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvsales.SelectedRow.RowIndex)
                    {

                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemName"] = txtItemname.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtQuantity.Text;
                        dr["Rate"] = txtRate.Text;
                        dr["VAT"] = "0";
                        dr["Cst"] = "0";
                        dr["Excise"] = "0";
                        dr["SPPrice"] = "0";
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["GodownId"] = ddllocation.SelectedItem.Value;
                        dr["CompanyId"] = ddlCompany.SelectedItem.Value;
                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["VAT"] = "0";
                        dr["Cst"] = "0";
                        dr["Excise"] = "0";
                        dr["SPPrice"] = gvrow.Cells[12].Text;
                        dr["Color"] = gvrow.Cells[13].Text;
                        dr["ColorId"] = gvrow.Cells[14].Text;
                        dr["GodownId"] = gvrow.Cells[15].Text;
                        dr["CompanyId"] = gvrow.Cells[16].Text;
                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = "0";
                    dr["Cst"] = "0";
                    dr["Excise"] = "0";
                    dr["SPPrice"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[13].Text;
                    dr["ColorId"] = gvrow.Cells[14].Text;
                    dr["GodownId"] = gvrow.Cells[15].Text;
                    dr["CompanyId"] = gvrow.Cells[16].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }

        if (gvsales.Rows.Count > 0)
        {
            if (gvsales.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvsales.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
                    {
                        gvsales.DataSource = SalesInvoiceProducts;
                        gvsales.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }
        if (gvsales.SelectedIndex == -1)
        {
            DataRow drnew = SalesInvoiceProducts.NewRow();
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemName"] = txtItemname.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQuantity.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["VAT"] = "0";
            drnew["Cst"] = "0";
            drnew["Excise"] = "0";
            drnew["SPPrice"] = "0";
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["GodownId"] = ddllocation.SelectedItem.Value;
            drnew["CompanyId"] = ddlCompany.SelectedItem.Value;
            SalesInvoiceProducts.Rows.Add(drnew);
        }

        gvsales.DataSource = SalesInvoiceProducts;
        gvsales.DataBind();
        gvsales.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlModelNo.SelectedValue = "0";
        txtItemname.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        //txtVAT.Text = string.Empty;
        //txtCST.Text = string.Empty;
        //txtExcise.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtSpprice.Text = string.Empty;
        txtDeliDate.Text = string.Empty;
        gvItmDetails.SelectedIndex = -1;
        ddlCompany.SelectedValue = "0";
        ddlColor.SelectedValue = "0";
        ddllocation.SelectedValue = "0";

    }
    #endregion

    #region GvItemdetails Row DataBound
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[12].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));


            //if (e.Row.Cells[8].Text != "" && e.Row.Cells[9].Text == "0")
            //{

            //    //if (e.Row.Cells[6].Text != "&nbsp;")
            //    //{
            //    decimal am = 0;
            //    decimal VatCalc = 0;
            //    am = Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));
            //    VatCalc = Convert.ToDecimal(Convert.ToDecimal((am / 100) * Convert.ToDecimal(e.Row.Cells[8].Text)));
            //    e.Row.Cells[11].Text = Convert.ToString(am + VatCalc);
            //}
            ////}
            //if (e.Row.Cells[9].Text != "" && e.Row.Cells[8].Text == "0")
            //{
            //    decimal am = 0;
            //    decimal VatCalc = 0;
            //    am = Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));
            //    VatCalc = Convert.ToDecimal(Convert.ToDecimal((am / 100) * Convert.ToDecimal(e.Row.Cells[9].Text)));
            //    e.Row.Cells[11].Text = Convert.ToString(am + VatCalc);
            //}

            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            //  txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            e.Row.Cells[10].Text = "Total Amount:";
            e.Row.Cells[11].Text = TotalAmount.ToString();
            txtTotalAmt.Text = GrossAmountCalc1().ToString();
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //txtVAT.Text = VatCalc().ToString();
            //txtCST.Text = CstCalc().ToString();
        }
    }

    private object GrossAmountCalc1()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }


    private double VatCalc()
    {
        double _totalVat = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalVat = _totalVat + Convert.ToDouble(gvrow.Cells[8].Text);
        }
        return _totalVat;
    }
    private double CstCalc()
    {
        double _totalCst = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalCst = _totalCst + Convert.ToDouble(gvrow.Cells[9].Text);
        }
        return _totalCst;
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvsales.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }

    #endregion

    #region Item Row Delete
    protected void gvItmDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItmDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Vat");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SPPrice");
        SalesInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("DeliveryDate");
        //SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);

        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["SPPrice"] = gvrow.Cells[12].Text;
                    //dr["DeliveryDate"] = gvrow.Cells[13].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    #endregion

    #region Item Row Editing
    protected void gvItmDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Vat");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SPPrice");
        SalesInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("DeliveryDate");
        //SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);

        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            DataRow dr = SalesInvoiceProducts.NewRow();

            dr["ItemCode"] = gvrow.Cells[2].Text;
            dr["ItemName"] = gvrow.Cells[4].Text;
            dr["UOM"] = gvrow.Cells[5].Text;
            dr["Quantity"] = gvrow.Cells[6].Text;
            dr["Rate"] = gvrow.Cells[7].Text;
            dr["VAT"] = gvrow.Cells[8].Text;
            dr["Cst"] = gvrow.Cells[9].Text;
            dr["Excise"] = "0";
            dr["SPPrice"] = gvrow.Cells[12].Text;
            //dr["DeliveryDate"] = gvrow.Cells[13].Text;
            dr["ModelNo"] = gvrow.Cells[3].Text;
            SalesInvoiceProducts.Rows.Add(dr);
            if (gvrow.RowIndex == gvItmDetails.Rows[e.NewEditIndex].RowIndex)
            {

                ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                ddlModelNo_SelectedIndexChanged(sender, e);
                txtItemUOM.Text = gvrow.Cells[5].Text;
                txtQuantity.Text = gvrow.Cells[6].Text;
                txtRate.Text = gvrow.Cells[7].Text;
                txtSpprice.Text = gvrow.Cells[12].Text;
                // txtDeliDate.Text = gvrow.Cells[13].Text;
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text));
                gvItmDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    #endregion

    #region Save & Update
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SalesReturnSave();
        }
        else if (btnSave.Text == "Update")
        {
            SalesReturnUpdate();
        }
    }
    #endregion

    #region SalesReturnSave
    private void SalesReturnSave()
    {
        if (gvsales.Rows.Count > 0)
        {
            try
            {
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }

                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
                Inventory.Delivery obj = new Inventory.Delivery();
                Inventory.BeginTransaction();
                objInventory.SRNo = txtSalesReturnNo.Text;
                objInventory.SRDate = Yantra.Classes.General.toMMDDYYYY(txtSalesReturndate.Text);
                objInventory.SOId = "0";
                objInventory.SRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SRApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;

                objInventory.SRMissChrgs = txtMiscelleneous.Text;
                objInventory.SRDiscount = txtDiscount.Text;
                objInventory.SRGrossAmt = txtGrossAmount.Text;
                objInventory.SRRemarks = txtRemarks.Text;
                objInventory.SRVAT = txtVAT.Text;
                objInventory.SRCSTax = txtCST.Text;
                objInventory.CPid = cp.getPresentCompanySessionValue();
                objInventory.SRaftermonth = txtTotalAmt.Text;
                objInventory.SIINVOICEID = ddlSalesInvoiceNo.SelectedItem.Value;
                //  objInventory.SIINVOICEID = "0";

                if (objInventory.SalesReturn_Save() == "Data Saved Successfully")
                {
                    objInventory.SalesReturnDetails_Delete(objInventory.SRId);
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SRDetQty = gvrow.Cells[6].Text;
                        objInventory.SRDetRate = gvrow.Cells[7].Text;
                        objInventory.SRDetVat = gvrow.Cells[8].Text;
                        objInventory.SRDetCst = gvrow.Cells[9].Text;
                        objInventory.SRDetExcise = gvrow.Cells[10].Text;
                        objInventory.SalesReturnDetails_Save();


                        string ItemCat = "";
                        string ItemSubCat = "";
                        Masters.ItemMaster objMaster = new Masters.ItemMaster();
                        if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                        {
                            ItemCat = objMaster.ItemCategoryName;
                            ItemSubCat = objMaster.ItemType;
                        }

                        Masters.ItemPurchase objsales = new Masters.ItemPurchase();
                        objsales.RefNo = txtSalesReturnNo.Text;
                        objsales.ItemCode = gvrow.Cells[2].Text;
                        objsales.ItemCategory = ItemCat;
                        objsales.ItemSubCategory = ItemSubCat;
                        objsales.ColorId = gvrow.Cells[14].Text;
                        objsales.qty = gvrow.Cells[6].Text;
                        objsales.BalanceQty = gvrow.Cells[6].Text;
                        objsales.DamageQty = "0";
                        objsales.CpId = cp.getPresentCompanySessionValue();
                        objsales.InwardType = "SampleReturn";
                        objsales.DateAdded = DateTime.Now.ToString();
                        objsales.ItemLoc = gvrow.Cells[15].Text;
                        objsales.Cust_id = "0";
                        objsales.DeliveryDate = DateTime.Now.ToString();

                        objsales.InwardTemp_Save();

                        //Masters.ItemPurchase objsales = new Masters.ItemPurchase();
                        //int hai = int.Parse(gvrow.Cells[6].Text);
                        //for (int i = 0; i < hai; i++)
                        //{
                        //    objsales.itemcode = gvrow.Cells[2].Text;
                        //    objsales.ItemID = "I" + i + gvrow.Cells[2].Text;
                        //    objsales.companyid = ddlCompany.SelectedItem.Value;
                        //    objsales.Barcode = "I" + i + gvrow.Cells[2].Text;
                        //    objsales.MRNID = "0";
                        //    objsales.COLORID = gvrow.Cells[14].Text;
                        //    // obj.locid = gvrow.Cells[13].Text;
                        //    objsales.ItemInward_Save();
                        //}

                    }
                    if (lblDCType.Text == "Returnable")
                    {
                        Inventory.SalesInvoice objDC = new Inventory.SalesInvoice();
                        objDC.DCId = ddlDeviveryNo.SelectedItem.Value;
                        objDC.DCStatus_Update();
                    }
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
                        //obj.iqitemcode = gvrow.Cells[2].Text;
                        //obj.iqcpid = gvrow.Cells[16].Text;
                        //obj.iqgodownid = gvrow.Cells[15].Text;
                        //obj.iqcolorid = gvrow.Cells[14].Text;
                        //obj.iqitemqtyinhand = gvrow.Cells[6].Text;
                        //obj.iqresqty = "0";
                        //obj.Return_Update();


                        Masters.ItemMaster obj1 = new Masters.ItemMaster();

                        obj1.ItemMaster_UpdateStoc(Convert.ToInt64(gvrow.Cells[2].Text), Convert.ToInt64(gvrow.Cells[6].Text), Convert.ToInt32(gvrow.Cells[15].Text), Convert.ToInt32(gvrow.Cells[16].Text), Convert.ToInt32(gvrow.Cells[14].Text));


                    }
                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvSalesReturnDetails.DataBind();
                gvsales.DataBind();
                gvsales.DataBind();
                gvDeliveryChallanItems.DataBind();
                tblsr.Visible = false;
                Inventory.ClearControls(this);
                // Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for SalesReturn");
        }
    }
    #endregion

    #region SalesReturnUpdate
    private void SalesReturnUpdate()
    {
        if (gvsales.Rows.Count > 0)
        {
            try
            {
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

                Inventory.BeginTransaction();

                objInventory.SRId = dcNo;
                objInventory.SRNo = txtSalesReturnNo.Text;
                objInventory.SRDate = txtSalesReturndate.Text;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;

                objInventory.SOId = "0";

                objInventory.SRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SRApprovedBy = ddlApprovedBy.SelectedItem.Value;
                //objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                //objInventory.SIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                //objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.SRMissChrgs = txtMiscelleneous.Text;
                objInventory.SRDiscount = txtDiscount.Text;
                objInventory.SRGrossAmt = txtGrossAmount.Text;
                objInventory.SRRemarks = txtRemarks.Text;
                objInventory.SRVAT = txtVAT.Text;
                objInventory.SRCSTax = txtCST.Text;
                objInventory.CPid = cp.getPresentCompanySessionValue();
                objInventory.SRaftermonth = txtTotalAmt.Text;
                objInventory.SIINVOICEID = ddlSalesInvoiceNo.SelectedItem.Value;

                if (objInventory.SalesReturn_Update() == "Data Updated Successfully")
                {
                    //objInventory.SalesReturnDetails_Delete(objInventory.SRId);
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SRDetQty = gvrow.Cells[6].Text;
                        objInventory.SRDetRate = gvrow.Cells[7].Text;
                        objInventory.SRDetVat = gvrow.Cells[8].Text;
                        objInventory.SRDetCst = gvrow.Cells[9].Text;
                        objInventory.SRDetExcise = gvrow.Cells[10].Text;

                        objInventory.SalesReturnDetails_Save();

                        string ItemCat = "";
                        string ItemSubCat = "";
                        Masters.ItemMaster objMaster = new Masters.ItemMaster();
                        if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                        {
                            ItemCat = objMaster.ItemCategoryName;
                            ItemSubCat = objMaster.ItemType;
                        }

                        Masters.ItemPurchase objsales = new Masters.ItemPurchase();
                        objsales.RefNo = txtSalesReturnNo.Text;
                        objsales.ItemCode = gvrow.Cells[2].Text;
                        objsales.ItemCategory = ItemCat;
                        objsales.ItemSubCategory = ItemSubCat;
                        objsales.ColorId = gvrow.Cells[14].Text;
                        objsales.qty = gvrow.Cells[6].Text;
                        objsales.BalanceQty = gvrow.Cells[6].Text;
                        objsales.DamageQty = "0";
                        objsales.CpId = cp.getPresentCompanySessionValue();
                        objsales.InwardType = "SampleReturn";
                        objsales.DateAdded = DateTime.Now.ToString();
                        objsales.ItemLoc = gvrow.Cells[15].Text;
                        objsales.Cust_id = "0";
                        objsales.DeliveryDate = DateTime.Now.ToString();

                        objsales.InwardTemp_Save();

                       
                        //Masters.ItemPurchase objsales = new Masters.ItemPurchase();
                        //int hai = int.Parse(gvrow.Cells[6].Text);
                        //for (int i = 0; i < hai; i++)
                        //{
                        //    objsales.itemcode = gvrow.Cells[2].Text;
                        //    objsales.ItemID = "I" + i + gvrow.Cells[2].Text;
                        //    objsales.companyid = ddlCompany.SelectedItem.Value;
                        //    objsales.Barcode = "I" + i + gvrow.Cells[2].Text;
                        //    objsales.MRNID = "0";
                        //    objsales.COLORID = gvrow.Cells[14].Text;
                        //    // obj.locid = gvrow.Cells[13].Text;
                        //    objsales.ItemInward_Save();
                        //}
                    }




                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";

                gvsales.DataBind();
                gvsales.DataBind();
                gvsales.DataBind();
                tblsr.Visible = false;
                Inventory.ClearControls(this);
                // Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Sales Return");
        }
    }
    #endregion

    #region Approve Cilck
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory.SalesReturn objSI = new Inventory.SalesReturn();
            Inventory.BeginTransaction();
            objSI.SRId = dcNo;
            objSI.SRApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSI.SalesReturnApprove_Update();
            Inventory.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            Inventory.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
         //   gvSalesReturnDetails.DataBind();
           // btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region MainRefresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
        gvItmDetails.DataBind();
    }
    #endregion

    #region Print
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (dcNo!=null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SalesReturn&Srno=" + dcNo + "";
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

    #region Sales Return Details Databound
    protected void gvSalesReturnDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[4].Visible = false;

        }
    }
    #endregion

    protected void ddlSalesInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        Inventory.SalesInvoice obj = new Inventory.SalesInvoice();
        if (obj.SalesInvoice_Select(ddlSalesInvoiceNo.SelectedItem.Value) > 0)
        {
            ItemTypes_Fill();
            txtSalesInvoiceDate.Text = obj.SIDate;
            obj.SalesInvoiceDetailsSampleReturn_Select(ddlSalesInvoiceNo.SelectedItem.Value, gvSalesInvoice);
        }
    }
    protected void gvSalesInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
           // e.Row.Cells[11].Visible = false;
            e.Row.Cells[10].Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[6].Text == "&nbsp;" && e.Row.Cells[7].Text == "&nbsp;")
            //{
            //    e.Row.Cells[9].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text));
            //}
            if (e.Row.Cells[6].Text == "&nbsp;")
            {
                e.Row.Cells[6].Text = "0";
            }

            if (e.Row.Cells[7].Text == "&nbsp;")
            {
                e.Row.Cells[7].Text = "0";
            }
            if (e.Row.Cells[8].Text == "&nbsp;")
            {
                e.Row.Cells[8].Text = "0";
            }

            if (e.Row.Cells[6].Text != "" && e.Row.Cells[7].Text == "0")
            {

                //if (e.Row.Cells[6].Text != "&nbsp;")
                //{
                decimal am = 0;
                decimal VatCalc = 0;

                am = Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text));

                VatCalc = Convert.ToDecimal(Convert.ToDecimal((am / 100) * Convert.ToDecimal(e.Row.Cells[6].Text)));

                e.Row.Cells[9].Text = Convert.ToString(am + VatCalc);
            }
            //}
            if (e.Row.Cells[7].Text != "" && e.Row.Cells[6].Text == "0")
            {
                decimal am = 0;
                decimal VatCalc = 0;

                am = Convert.ToDecimal(Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text));

                VatCalc = Convert.ToDecimal(Convert.ToDecimal((am / 100) * Convert.ToDecimal(e.Row.Cells[7].Text)));

                e.Row.Cells[9].Text = Convert.ToString(am + VatCalc);
            }

        }
    }



    protected void ddlCompany_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //Masters.ItemMaster.Stockentry143(ddllocation, ddlCompany.SelectedItem.Value);
    }
    protected void gvsales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[6].Text))).ToString();
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtAfteronemonthHidden.Value = txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = txtGrossAmount.Text = GrossAmountCalc().ToString();
            e.Row.Cells[10].Text = "Total:";
            e.Row.Cells[11].Text = TotalAmount.ToString();
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[12].Visible = false;

        }

    }
    protected void gvsales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvsales.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Vat");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SPPrice");
        SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("Color");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("GodownId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("CompanyId");
        SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);

        if (gvsales.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvsales.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["SPPrice"] = gvrow.Cells[12].Text;

                    dr["Color"] = gvrow.Cells[13].Text;
                    dr["ColorId"] = gvrow.Cells[14].Text;
                    dr["GodownId"] = gvrow.Cells[15].Text;
                    dr["CompanyId"] = gvrow.Cells[16].Text;


                    //dr["DeliveryDate"] = gvrow.Cells[13].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvsales.DataSource = SalesInvoiceProducts;
        gvsales.DataBind();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleReturn.aspx");
    }
}
 
