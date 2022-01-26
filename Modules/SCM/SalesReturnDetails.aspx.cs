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

public partial class Modules_SCM_SalesReturnDetails : basePage
{
    decimal TotalAmount = 0;

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
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
        if (!IsPostBack)
        {

            setControlsVisibility();
            lblCompany.Text = cp.getPresentCompanySessionValue();

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
           // txtIncludeVat.Attributes.Add("onkeyup", "javascript:Grossamtcalc();");
           // txtIncludeVat.Attributes.Add("onkeyup", "javascript:Include();");
            lblOrderedItemsHeading.Text = "Sales Ordered Items";

            EmployeeMaster_Fill();
            CustomerName_Fill();
            tblsr.Visible = false;

            if (Request.QueryString["SrNo"] != null)
            {
                try
                {
                    Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

                    if (objInventory.SalesReturn_SelectOri(Request.QueryString["SrNo"].ToString()) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblsr.Visible = true;
                        txtSalesReturnNo.Text = objInventory.SRNo;
                        txtSalesReturndate.Text = objInventory.SRDate;
                        ddlCustomerName.SelectedValue = objInventory.CustId;
                        ddlCustomerName_SelectedIndexChanged(sender, e);
                        ddlSalesOrderNo.SelectedValue = objInventory.SOId;
                        ddlSalesOrderNo_SelectedIndexChanged(sender, e);                
                        ddlDeviveryNo.SelectedValue = objInventory.DCId;               
                        ddlDeviveryNo_SelectedIndexChanged(sender, e);
                        ddlPreparedBy.SelectedValue = objInventory.SRPreparedBy;
                        ddlApprovedBy.SelectedValue = objInventory.SRApprovedBy;
                        //ddlInvoiceType.SelectedValue = objInventory.SIType;

                        //ddlDeliveryType.SelectedValue = objInventory.DespmId;
                        txtMiscelleneous.Text = objInventory.SRMissChrgs;
                        txtDiscount.Text = objInventory.SRDiscount;
                        txtGrossAmount.Text = objInventory.SRGrossAmt;
                        txtGrossTotalAmtHidden.Value = objInventory.SRGrossAmt;
                        lblGrossAmt.Text = objInventory.SRGrossAmt;

                        txtRemarks.Text = objInventory.SRRemarks;
                        ddlSalesInvoiceNo.SelectedValue = objInventory.SIINVOICEID;
                        ddlSalesInvoiceNo_SelectedIndexChanged(sender, e);
            
                            txtCST.Text = objInventory.SRCSTax;
                   
    
                            txtVAT.Text = objInventory.SRVAT;
                 
                        gvDeliveryChallanItems.DataBind();
                        gvItemDetails.DataBind();
               

                        objInventory.SalesReturnDetails_Select(Request.QueryString["SrNo"].ToString(),gvItmDetails);
                        gvItmDetails.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                    Inventory.Dispose();
                }
            }
            else
            {
                //Inventory.ClearControls(this);
                txtSalesReturnNo.Text = Inventory.SalesReturn.SalesReturn_AutoGenCode();
                txtSalesReturndate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                btnSave.Visible = true;
                tblsr.Visible = true;
                gvDeliveryChallanItems.DataBind();
                gvSalesInvoice.DataBind();
                gvItmDetails.DataBind();
                gvsales.DataBind();
            }

        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "71");
        
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        btnSave.Enabled = up.add;
        btnApprove.Enabled = up.Approve;
        //btnRefresh.Enabled = up.Refresh;
        btnPrint.Enabled = up.Print;
        btnDelete.Enabled = up.Delete;
        


    }

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            SM.DDLBindWithSelect(ddlModelNo, "select YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_SALES_INVOICE_DET,YANTRA_ITEM_MAST where YANTRA_SALES_INVOICE_DET.ITEM_CODE = YANTRA_ITEM_MAST.ITEM_CODE and YANTRA_SALES_INVOICE_DET.SI_ID = " + ddlSalesInvoiceNo.SelectedItem.Value);
            // Inventory.Delivery.DeliveryDetailsItemTypes1_Select(ddlDeviveryNo.SelectedItem.Value, ddlModelNo);
            //Masters.ItemType.ItemType_Select(ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Inventory.Dispose();
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

    #region CustomerName Fill
    private void CustomerName_Fill()
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

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlSalesOrderNo, ddlCustomerName.SelectedItem.Value);
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

    #region SparesOrder Fill
    private void SparesOrder_Fill()
    {
        try
        {
            Services.SparesOrder.SparesOrder_Select(ddlSalesOrderNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Services.Dispose();
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
            Inventory.Dispose();
        }
    }
    #endregion

    #region DdlCustomerName Change
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeviveryNo.SelectedValue = "0";
        ddlSalesOrderNo.SelectedValue = "0";
        SalesOrder_Fill();
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
        {
            //ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
            //txtCustomerName.Text = objSMCustomer.CustName;
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
        }
        //gvDeliveryChallanItems.DataBind();
        //gvItemDetails.DataBind();
        //gvItmDetails.DataBind();
    }
    #endregion

    #region DDlsales Order No Change
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSalesOrderNo.Text = "Sales Order No.";
        if (lblSalesOrderNo.Text == "Sales Order No.")
        {
            try
            {
                SM.SalesOrder objSM = new SM.SalesOrder();
                if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
                {
                    Inventory.Delivery objDelivery = new Inventory.Delivery();
                    //if (objDelivery.Delivery_SelectSO(ddlSalesOrderNo.SelectedItem.Value) > 0)
                    //{

                    //   // objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
                    //}
                    Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
                    txtSalesOrderDate.Text = objSM.SODate;
                    objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);

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
        else if (lblSalesOrderNo.Text == "Spares Order No.")
        {
            try
            {
                Services.SparesOrder objSM = new Services.SparesOrder();
                if (objSM.SparesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
                {
                    txtSalesOrderDate.Text = objSM.SPODate;
                    objSM.SparesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                    //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    //if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                    //{
                    //    ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
                    //    //txtCustomerName.Text = objSMCustomer.CustName;
                    //    txtAddress.Text = objSMCustomer.Address;
                    //    txtEmail.Text = objSMCustomer.Email;
                    //    txtRegion.Text = objSMCustomer.RegName;
                    //    txtPhone.Text = objSMCustomer.Phone;
                    //    txtMobile.Text = objSMCustomer.Mobile;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Services.Dispose();
            }
        }
    }
    #endregion

    #region ddl DeliveryNo Select Change
    protected void ddlDeviveryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ItemTypesDc_Fill();
            Inventory.Delivery objDelivery = new Inventory.Delivery();
            if (objDelivery.Delivery_Select(ddlDeviveryNo.SelectedItem.Value) > 0)
            {
                txtChallanDate.Text = objDelivery.DCDate;
                if (objDelivery.DCFor == "Sales")
                {
                    //SalesOrder_Fill();
                    lblSalesOrderNo.Text = "Sales Order No.";
                    lblSalesOrderDate.Text = "Sales Order Date";
                    lblOrderedItemsHeading.Text = "Sales Ordered Items";
                    //ddlSalesOrderNo.SelectedValue = objDelivery.SOId;
                    //ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }
                else if (objDelivery.DCFor == "Spares")
                {

                    lblSalesOrderNo.Text = "Spares Order No.";
                    lblSalesOrderDate.Text = "Spares Order Date";
                    lblOrderedItemsHeading.Text = "Spares Ordered Items";
                    //ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }
                objDelivery.DeliveryDetails_SelectInvoice1(ddlDeviveryNo.SelectedItem.Value, gvDeliveryChallanItems);
                SM.DDLBindWithSelect(ddlSalesInvoiceNo, "select SI_ID,SI_NO from YANTRA_SALES_INVOICE_MAST, YANTRA_DELIVERY_CHALLAN_MAST where YANTRA_SALES_INVOICE_MAST.DC_ID = YANTRA_DELIVERY_CHALLAN_MAST.DC_ID and YANTRA_SALES_INVOICE_MAST.DC_ID = " + ddlDeviveryNo.SelectedItem.Value);
                //Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                //objInventory.SalesInvoice_SelectDelivery(ddlDeviveryNo.SelectedItem.Value);
                // objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
                gvItmDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Inventory.Dispose();
            //ddlSalesOrderNo_SelectedIndexChanged(sender, e);
        }
    }

    private void ItemTypesDc_Fill()
    {
        SM.DDLBindWithSelect(ddlModelNo, "select YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_DELIVERY_CHALLAN_DET,YANTRA_ITEM_MAST where YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE = YANTRA_ITEM_MAST.ITEM_CODE and YANTRA_DELIVERY_CHALLAN_DET.DC_ID = " + ddlDeviveryNo.SelectedItem.Value);
    }
    #endregion

    #region Gvitem Details RowdataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = (Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text)).ToString();
        }
    }
    #endregion

    #region go Cilck
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {
                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Vat");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Cst");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Excise");
                DeliveryItems.Columns.Add(col);
                if (gvItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvItmDetails.Rows)
                    {
                        if (gvItmDetails.SelectedIndex > -1)
                        {
                            if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                            {
                                DataRow dr = DeliveryItems.NewRow();
                                dr["ItemCode"] = gvrow.Cells[1].Text;
                                dr["ModelNo"] = gvrow.Cells[2].Text;
                                dr["ItemName"] = gvrow.Cells[3].Text;
                                dr["UOM"] = gvrow.Cells[4].Text;
                                dr["Quantity"] = gvrow.Cells[5].Text;
                                dr["Rate"] = gvrow.Cells[6].Text;
                                dr["Vat"] = gvrow.Cells[8].Text;
                                dr["Cst"] = gvrow.Cells[9].Text;

                                dr["Excise"] = "0";
                                //dr["DeliveryDate"] = gvrow.Cells[10].Text;
                                ////dr["Room"] = gvrow.Cells[10].Text;
                                dr["SPPrice"] = gvrow.Cells[7].Text;
                                DeliveryItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = DeliveryItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ModelNo"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                dr["Quantity"] = gvrow1.Cells[6].Text;
                                dr["Rate"] = gvrow1.Cells[7].Text;
                                dr["Vat"] = gvrow1.Cells[8].Text;
                                dr["Cst"] = gvrow1.Cells[9].Text;
                                dr["Excise"] = "0";
                                //dr["DeliveryDate"] = gvrow.Cells[13].Text;
                                dr["SPPrice"] = gvrow1.Cells[12].Text;
                                DeliveryItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = DeliveryItems.NewRow();
                            dr["ItemCode"] = gvrow1.Cells[2].Text;
                            dr["ModelNo"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;
                            dr["Rate"] = gvrow1.Cells[7].Text;
                            dr["Vat"] = gvrow1.Cells[8].Text;
                            dr["Cst"] = gvrow1.Cells[9].Text;
                            dr["Excise"] = "0";

                            //dr["DeliveryDate"] = gvrow1.Cells[13].Text;

                            dr["SPPrice"] = gvrow1.Cells[12].Text;
                            DeliveryItems.Rows.Add(dr);
                        }
                        if (gvItmDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[2].Text == gvrow1.Cells[3].Text)
                            {
                                gvItmDetails.DataSource = DeliveryItems;
                                gvItmDetails.DataBind();
                                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                                btnItemRefresh_Click(sender, e);
                                ch.Checked = false;
                                return;
                            }
                        }

                    }
                }
                if (gvItmDetails.SelectedIndex == -1)
                {
                    DataRow drnew = DeliveryItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[1].Text;
                    drnew["ModelNo"] = gvrow.Cells[2].Text;
                    drnew["ItemName"] = gvrow.Cells[3].Text;
                    drnew["UOM"] = gvrow.Cells[4].Text;
                    drnew["Quantity"] = gvrow.Cells[5].Text;
                    drnew["Rate"] = gvrow.Cells[6].Text;
                    drnew["Vat"] = gvrow.Cells[8].Text;
                    if (gvrow.Cells[8].Text == "")
                    {
                        drnew["Vat"] = "0";
                    }
                    drnew["Cst"] = gvrow.Cells[9].Text;
                    if (gvrow.Cells[9].Text == "&nbsp;")
                    {
                        drnew["Cst"] = "0";
                    }
                    drnew["Excise"] = "0";
                    //drnew["DeliveryDate"] = gvrow.Cells[10].Text;
                    drnew["SPPrice"] = gvrow.Cells[7].Text;
                    DeliveryItems.Rows.Add(drnew);
                }
                gvItmDetails.DataSource = DeliveryItems;
                gvItmDetails.DataBind();
                gvItmDetails.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }
    #endregion

    #region DDlModel No Change
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;

                txtItemname.Text = objMaster.ItemName;
                foreach (GridViewRow gvRow in gvItemDetails.Rows)
                {
                    if (gvRow.Cells[0].Text == ddlModelNo.SelectedItem.Value)
                    {
                        txtRate.Text = gvRow.Cells[5].Text;
                        return;
                    }
                }

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
            e.Row.Cells[12].Visible = false;

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
            lblTtlAmt.Text = txtTotalAmt.Text;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[12].Visible = false;
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
            Response.Redirect("SalesReturn.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            SalesReturnUpdate();
            Response.Redirect("SalesReturn.aspx");

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
                btnSave.Enabled = false;

                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }

                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
                Inventory.Delivery obj = new Inventory.Delivery();
                //Inventory.BeginTransaction();
                objInventory.SRNo = txtSalesReturnNo.Text;
                objInventory.SRDate = Yantra.Classes.General.toMMDDYYYY(txtSalesReturndate.Text);
                objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
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

                if (objInventory.SalesReturn_Save() == "Data Saved Successfully")
                {
                   // objInventory.SalesReturnDetails_Delete(objInventory.SRId);
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SRDetQty = gvrow.Cells[6].Text;
                        objInventory.SRDetRate = gvrow.Cells[7].Text;
                        objInventory.SRDetVat = gvrow.Cells[8].Text;
                        objInventory.SRDetCst = gvrow.Cells[9].Text;
                        objInventory.SRDetExcise = gvrow.Cells[10].Text;
                        objInventory.SalesReturnDetails_Save();


                    }
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
                        Masters.ItemMaster obj1 = new Masters.ItemMaster();
                        
                        obj1.ItemMaster_UpdateStoc(Convert.ToInt64(gvrow.Cells[2].Text), Convert.ToInt64(gvrow.Cells[6].Text), Convert.ToInt32(gvrow.Cells[15].Text), Convert.ToInt32(gvrow.Cells[16].Text), Convert.ToInt32(gvrow.Cells[14].Text));

                    }
                    //Inventory.CommitTransaction();
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
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
                        objsales.InwardType = "SalesReturn";
                        objsales.DateAdded = DateTime.Now.ToString();
                        objsales.ItemLoc = gvrow.Cells[15].Text;
                        objsales.Cust_id = "0";
                        objsales.DeliveryDate = DateTime.Now.ToString();
                        objsales.InwardTemp_Save();
                    }
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    MessageBox.Show(this, "Please Check the data and try again by deleting old unsuccessfull data(if any).");

                    //Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                //Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvSalesReturnDetails.DataBind();
                gvsales.DataBind();
                gvsales.DataBind();
                gvDeliveryChallanItems.DataBind();
                tblsr.Visible = false;
                btnSave.Enabled = true;

                Inventory.ClearControls(this);
                //Inventory.Dispose();
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
        //if (gvsales.Rows.Count > 0)
        //{
            try
            {
                btnSave.Enabled = false;
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

                //Inventory.BeginTransaction();

                objInventory.SRId = Request.QueryString["SrNo"].ToString();
                objInventory.SRNo = txtSalesReturnNo.Text;
                objInventory.SRDate = txtSalesReturndate.Text;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;

                objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;


                objInventory.SRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SRApprovedBy = ddlApprovedBy.SelectedItem.Value;
                //objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                //objInventory.SIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                //objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.SRMissChrgs = txtMiscelleneous.Text;
                objInventory.SRDiscount = txtDiscount.Text;
                objInventory.SRGrossAmt = txtGrossAmount.Text;
                if (txtVAT.Text == "" || txtVAT.Text == "0")
                {
                    objInventory.SRGrossAmt = txtTotalAmt.Text;
                }
                objInventory.SRRemarks = txtRemarks.Text;
                objInventory.SRVAT = txtVAT.Text;
                objInventory.SRCSTax = txtCST.Text;
                objInventory.CPid = cp.getPresentCompanySessionValue();
                objInventory.SRaftermonth = txtTotalAmt.Text;
                objInventory.SIINVOICEID = ddlSalesInvoiceNo.SelectedItem.Value;

                if (objInventory.SalesReturn_Update() == "Data Updated Successfully")
                {
                   // objInventory.SalesReturnDetails_Delete(objInventory.SRId);
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SRDetQty = gvrow.Cells[6].Text;
                        objInventory.SRDetRate = gvrow.Cells[7].Text;
                        objInventory.SRDetVat = gvrow.Cells[8].Text;
                        objInventory.SRDetCst = gvrow.Cells[9].Text;
                        objInventory.SRDetExcise = gvrow.Cells[10].Text;

                        objInventory.SalesReturnDetails_Save();

                    }


                    //Inventory.CommitTransaction();
                    foreach (GridViewRow gvrow in gvsales.Rows)
                    {
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
                        objsales.InwardType = "SalesReturn";
                        objsales.DateAdded = DateTime.Now.ToString();
                        objsales.ItemLoc = gvrow.Cells[15].Text;
                        objsales.Cust_id = "0";
                        objsales.DeliveryDate = DateTime.Now.ToString();
                        objsales.InwardTemp_Save();
                    }
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    MessageBox.Show(this, "Please Check the data and try again by deleting old unsuccessfull data(if any).");

                    //Inventory.RollBackTransaction();
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
                //Inventory.Dispose();
                btnSave.Enabled = true;
            }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please add atleast one Item for Sales Return");
        //}
    }
    #endregion

    #region Approve Cilck
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory.SalesReturn objSI = new Inventory.SalesReturn();
            Inventory.BeginTransaction();
            objSI.SRId = Request.QueryString["SrNo"].ToString();
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
           // gvSalesReturnDetails.DataBind();
            btnEdit_Click(sender, e);
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
        if (Request.QueryString["SrNo"]!= null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SalesReturn&Srno=" + Request.QueryString["SrNo"].ToString() + "";
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

    #region New
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
        txtSalesReturnNo.Text = Inventory.SalesReturn.SalesReturn_AutoGenCode();
        txtSalesReturndate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        btnSave.Visible = true;
        tblsr.Visible = true;
        gvDeliveryChallanItems.DataBind();
        gvSalesInvoice.DataBind();
        gvItmDetails.DataBind();
        gvsales.DataBind();
    }
    #endregion

    #region Edit Cilck
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblsr.Visible = true;

        if (Request.QueryString["SrNo"]!= null)
        {
            try
            {
                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

                if (objInventory.SalesReturn_Select(Request.QueryString["SrNo"].ToString()) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblsr.Visible = true;
                    txtSalesReturnNo.Text = objInventory.SRNo;
                    txtSalesReturndate.Text = objInventory.SRDate;
                    ddlSalesOrderNo.SelectedValue = objInventory.SOId;
                    ddlSalesOrderNo_SelectedIndexChanged(sender, e);
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
                    objInventory.SalesReturnDetails_Select(Request.QueryString["SrNo"].ToString(), gvItmDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Delete Cilck
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["SrNo"]!= null)
        {
            try
            {
                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
                MessageBox.Show(this, objInventory.SalesReturn_Delete(Request.QueryString["SrNo"].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                //gvSalesReturnDetails.SelectedIndex = -1;
                //gvSalesReturnDetails.DataBind();
                Inventory.ClearControls(this);
                Inventory.Dispose();
                Response.Redirect("SalesReturn.aspx");
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

    #region MainLinkButtons Cilck
    protected void lbtnDCNo_Click(object sender, EventArgs e)
    {
        lbtnSalesReturnNo_Click(sender, e);
    }

    protected void lbtnSalesReturnNo_Click(object sender, EventArgs e)
    {
        tblsr.Visible = false;
        LinkButton lbtnSalesReturnNo;
        lbtnSalesReturnNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesReturnNo.Parent.Parent;
        //gvSalesReturnDetails.SelectedIndex = gvRow.RowIndex;
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

            if (objInventory.SalesReturn_SelectOri(Request.QueryString["SrNo"].ToString()) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblsr.Visible = true;
                txtSalesReturnNo.Text = objInventory.SRNo;
                txtSalesReturndate.Text = objInventory.SRDate;
                ddlCustomerName.SelectedValue = objInventory.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                ddlSalesOrderNo.SelectedValue = objInventory.SOId;
                ddlSalesOrderNo_SelectedIndexChanged(sender, e);

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
                ddlSalesInvoiceNo.SelectedValue = objInventory.SIINVOICEID;
                ddlSalesInvoiceNo_SelectedIndexChanged(sender, e);

                txtCST.Text = objInventory.SRCSTax;


                txtVAT.Text = objInventory.SRVAT;

                gvDeliveryChallanItems.DataBind();
                gvItemDetails.DataBind();


                objInventory.SalesReturnDetails_Select(Request.QueryString["SrNo"].ToString(), gvItmDetails);
                gvItmDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            Inventory.Dispose();
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
            obj.SalesInvoiceDetailsReturn_Select(ddlSalesInvoiceNo.SelectedItem.Value, gvSalesInvoice);
        }
    }
    protected void gvSalesInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[12].Visible = false;


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
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;

            e.Row.Cells[10].Visible = false;


        }
    }
    protected void btngo1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvSalesInvoice.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {
                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Vat");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Cst");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Excise");
                DeliveryItems.Columns.Add(col);
                if (gvItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvItmDetails.Rows)
                    {
                        if (gvItmDetails.SelectedIndex > -1)
                        {
                            if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                            {
                                DataRow dr = DeliveryItems.NewRow();
                                dr["ItemCode"] = gvrow.Cells[0].Text;
                                dr["ModelNo"] = gvrow.Cells[1].Text;
                                dr["ItemName"] = gvrow.Cells[2].Text;
                                dr["UOM"] = gvrow.Cells[3].Text;
                                dr["Quantity"] = gvrow.Cells[4].Text;
                                dr["Rate"] = gvrow.Cells[5].Text;
                                dr["Vat"] = gvrow.Cells[6].Text;
                                dr["Cst"] = gvrow.Cells[7].Text;

                                dr["Excise"] = "0";
                                //dr["DeliveryDate"] = gvrow.Cells[10].Text;
                                ////dr["Room"] = gvrow.Cells[10].Text;
                                dr["SPPrice"] = gvrow.Cells[10].Text;
                                DeliveryItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = DeliveryItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ModelNo"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                dr["Quantity"] = gvrow1.Cells[6].Text;
                                dr["Rate"] = gvrow1.Cells[7].Text;
                                dr["Vat"] = gvrow1.Cells[8].Text;
                                dr["Cst"] = gvrow1.Cells[9].Text;
                                dr["Excise"] = "0";
                                //dr["DeliveryDate"] = gvrow.Cells[13].Text;
                                dr["SPPrice"] = gvrow1.Cells[12].Text;
                                DeliveryItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = DeliveryItems.NewRow();
                            dr["ItemCode"] = gvrow1.Cells[2].Text;
                            dr["ModelNo"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;
                            dr["Rate"] = gvrow1.Cells[7].Text;
                            dr["Vat"] = gvrow1.Cells[8].Text;
                            dr["Cst"] = gvrow1.Cells[9].Text;
                            dr["Excise"] = "0";

                            //dr["DeliveryDate"] = gvrow1.Cells[13].Text;

                            dr["SPPrice"] = gvrow1.Cells[12].Text;
                            DeliveryItems.Rows.Add(dr);
                        }
                        if (gvItmDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[2].Text == gvrow1.Cells[3].Text)
                            {
                                gvItmDetails.DataSource = DeliveryItems;
                                gvItmDetails.DataBind();
                                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                                btnItemRefresh_Click(sender, e);
                                ch.Checked = false;
                                return;
                            }
                        }

                    }
                }
                if (gvItmDetails.SelectedIndex == -1)
                {
                    DataRow drnew = DeliveryItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[0].Text;
                    drnew["ModelNo"] = gvrow.Cells[1].Text;
                    drnew["ItemName"] = gvrow.Cells[2].Text;
                    drnew["UOM"] = gvrow.Cells[3].Text;
                    drnew["Quantity"] = gvrow.Cells[4].Text;
                    drnew["Rate"] = gvrow.Cells[5].Text;
                    drnew["Vat"] = gvrow.Cells[6].Text;
                    if (gvrow.Cells[6].Text == "")
                    {
                        drnew["Vat"] = "0";
                    }
                    drnew["Cst"] = gvrow.Cells[7].Text;
                    if (gvrow.Cells[7].Text == "&nbsp;")
                    {
                        drnew["Cst"] = "0";
                    }
                    drnew["Excise"] = "0";
                    //drnew["DeliveryDate"] = gvrow.Cells[10].Text;
                    drnew["SPPrice"] = gvrow.Cells[10].Text;
                    DeliveryItems.Rows.Add(drnew);
                }
                gvItmDetails.DataSource = DeliveryItems;
                gvItmDetails.DataBind();
                gvItmDetails.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged1(object sender, EventArgs e)
    {
       // Masters.ItemMaster.Stockentry143(ddllocation, ddlCompany.SelectedItem.Value);
       // Godown_Fill();
    }
    private void Godown_Fill()
    {
        try
        {
            // Masters.ItemMaster.Godown_select(ddlgodown);
            Inventory.Internalindent.Location_Select(ddllocation);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

        }
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
            if (lblGrossAmt.Text == "") { lblGrossAmt.Text = "0"; }
            txtAfteronemonthHidden.Value = txtGrossTotalAmtHidden.Value = txtGrossAmount.Text = (Convert.ToDecimal(txtGrossAmount.Text) + Convert.ToDecimal(lblGrossAmt.Text)).ToString();
            if (lblTtlAmt.Text == "") { lblTtlAmt.Text = "0"; }
            txtTotalAmt.Text = (TotalAmount + Convert.ToDecimal(lblTtlAmt.Text)).ToString();
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

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource2";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesReturn.aspx");

    }
}
 
