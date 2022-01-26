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
public partial class Modules_Inventory_StatementOfAccount1 : basePage
{
    decimal TotalAmount = 0;
    decimal totalamount2 = 0;
    decimal totalamount3 = 0;
    decimal totalamount4 = 0;
    decimal totalamount5 = 0;
    decimal totalamount6 = 0;
    decimal totalamount7 = 0;
    decimal totalamount8 = 0;
    decimal totalamount9 = 0;
    decimal totalamount10 = 0;
    decimal totalamount11 = 0;



    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            lblOrderedValue.Text = "0";
            lblDCvalue.Text = "0";
            lblGoodsreturn.Text = "0";
            lblExtraDc.Text = "0";
            lblInvoicedAmt.Text = "0";
            lblBalanceAmount.Text = "0";
            lblBalanceInovieAmount.Text = "0"; 



            CustomerName_Fill();
            gvDeliveryChallanItems.DataBind();
            gvItemDetails.DataBind();
            gvItmDetails.DataBind();
          
            gvdeliveryChallanExtra.DataBind();
            gvSalesInvoice.DataBind();


            gvSlaesreturn.DataBind();
            gvsalesinvoiceondc.DataBind();
            gvDconCash.DataBind();


        }

    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "74");

        //btnSearchModelNo.Enabled = up.SearchModelNo;
        btnPrint.Enabled = up.Print;
        //btnvatcal.Enabled = up.vatcal;
    }

   

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

    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeviveryNo.SelectedValue = "0";
        ddlSalesOrderNo.SelectedValue = "0";
        SalesOrder_Fill();
        DeliveryonDc_fill();

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
        SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
        gvDeliveryChallanItems.DataBind();
        gvItemDetails.DataBind();

    }

    private void DeliveryonDc_fill()
    {
        try
        {
            Inventory.Delivery.DeliveryChallanApprovedOnDC_SelectSO(ddlDeliveryCallanOnsample, ddlCustomerName.SelectedValue);
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

    private void DeliveryonDcUnit_fill()
    {
        try
        {
            Inventory.Delivery.DeliveryChallanApprovedOnDCUnit_SelectSO(ddlDeliveryCallanOnsample, ddlUnitName.SelectedValue);
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

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlSalesOrderNo, ddlCustomerName.SelectedValue);
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

    #region Invoice Fill
    private void Invoice_Fill(string SoId)
    {
        try
        {
            Inventory.Delivery.DeliveryChallanApproved_SelectSI(ddlsalesinvoice, SoId);
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

    #region SalesOrder No Selected Index Changed
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
            try
            {
                SM.SalesOrder objSM = new SM.SalesOrder();
                if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
                {
                    Inventory.Delivery objDelivery = new Inventory.Delivery();
                    if (objDelivery.Delivery_SelectSO(ddlSalesOrderNo.SelectedItem.Value) > 0)
                    {
                        
                        Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
                        Invoice_Fill(ddlSalesOrderNo.SelectedItem.Value);
                                            
                    }
                    lblTerms.Text = objSM.SOOtherSpec;
                    lbltermsg.Text = objSM.SOOtherSpec;
                    txtSalesOrderDate.Text = objSM.SODate;
                    objSM.SalesOrderDetailsStatement_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                    objSM.SalesOrderDetailsBalanceQty_Select(ddlSalesOrderNo.SelectedItem.Value, gvPendingMaterial);

                    objDelivery.DeliveryDetailsPO_SelectInvoice(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
                    objDelivery.DeliveryDetails_SelectInvoiceExtra1SO(ddlSalesOrderNo.SelectedItem.Value, gvdeliveryChallanExtra);
                    objDelivery.DeliveryDetails_SelectInvoiceSI(ddlSalesOrderNo.SelectedItem.Value, gvSalesInvoice);
                    Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
                    objInventory.SalesReturnDetailsSO1_Select(ddlSalesOrderNo.SelectedItem.Value, gvItmDetails);
                   
                    if (ddlUnitName.SelectedValue != "0")
                    {
                        objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                        objSM.SalesOrderDetailsBalanceQty_Select(ddlSalesOrderNo.SelectedItem.Value, gvPendingMaterial);

                        objDelivery.DeliveryDetailsPOUnitName_SelectInvoice(ddlSalesOrderNo.SelectedItem.Value,ddlUnitName.SelectedItem.Value, gvDeliveryChallanItems);
                        objDelivery.DeliveryDetailsUnitName_SelectInvoiceExtra1SO(ddlSalesOrderNo.SelectedItem.Value, ddlUnitName.SelectedItem.Value, gvdeliveryChallanExtra);
                        //objDelivery.SoDetqty(ddlSalesOrderNo.SelectedItem.Value, gvpoqty);
                        //objDelivery.DCDetqtyUnitName(ddlSalesOrderNo.SelectedItem.Value,ddlUnitName.SelectedItem.Value,gvdcqty);
                      
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

                decimal tota = Convert.ToDecimal(lblOrderedValue.Text) - (((Convert.ToDecimal(lblDCvalue.Text) + Convert.ToDecimal(lblExtraDc.Text)) - Convert.ToDecimal(lblGoodsreturn.Text)));
                lblBalanceAmount.Text = tota.ToString();

                SM.Dispose();
            }
        

    }
    #endregion

    //protected void rbtnListStatement_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    lblSalesOrderNo.Text = "Sales Order No.";
    //    if (lblSalesOrderNo.Text == "Sales Order No.")
    //    {
    //        try
    //        {
    //            SM.SalesOrder objSM = new SM.SalesOrder();
    //            if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
    //            {
    //                Inventory.Delivery objDelivery = new Inventory.Delivery();
    //                if (objDelivery.Delivery_SelectSO(ddlSalesOrderNo.SelectedItem.Value) > 0)
    //                {
    //                    if (rbtnListStatement.SelectedValue == "DC")
    //                    {
    //                        lblDelivery.Text = "Delivery Challan No";
    //                        Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
    //                     //   lblDeliveredItemsHeading.Text = "Delivery Challan Items";
    //                        objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
    //                        rbtnListStatement.SelectedIndex = 0;
    //                    }
    //                    else if (rbtnListStatement.SelectedValue == "SI")
    //                    {
    //                        //lblDelivery.Text = "Sales Invoice No";
    //                        //lblDeliveredItemsHeading.Text = "Sales Invoice Items";
    //                        Invoice_Fill(ddlSalesOrderNo.SelectedItem.Value);
    //                        objDelivery.DeliveryDetails_SelectInvoiceSI(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
    //                        rbtnListStatement.SelectedIndex = 1;
    //                    }
    //                }
    //                txtSalesOrderDate.Text = objSM.SODate;
    //                objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
    //                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
    //                objInventory.SalesInvoice_SelectSO(ddlSalesOrderNo.SelectedItem.Value);
    //                //objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
    //              //  lblOrderedItemsHeading.Text = "Sales Ordered Items";
    //                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
    //                //if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
    //                //{
    //                //    ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
    //                //    //txtCustomerName.Text = objSMCustomer.CustName;
    //                //    txtAddress.Text = objSMCustomer.Address;
    //                //    txtEmail.Text = objSMCustomer.Email;
    //                //    txtRegion.Text = objSMCustomer.RegName;
    //                //    txtPhone.Text = objSMCustomer.Phone;
    //                //    txtMobile.Text = objSMCustomer.Mobile;
    //                //}
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            SM.Dispose();
    //        }
    //    }
    //}
    #region ddlDeviveryNo_SelectedIndexChanged
    protected void ddlDeviveryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Inventory.Delivery objDelivery = new Inventory.Delivery();
            if (objDelivery.Delivery_Select(ddlDeviveryNo.SelectedItem.Value) > 0)
            {
                txtChallanDate.Text = objDelivery.DCDate;
                if (objDelivery.DCFor == "Sales")
                {
                    //SalesOrder_Fill();
                    lblSalesOrderNo.Text = "Sales Order No.";
                    lblSalesOrderDate.Text = "Sales Order Date";
                  //  lblOrderedItemsHeading.Text = "Sales Ordered Items";
                    //ddlSalesOrderNo.SelectedValue = objDelivery.SOId;
                    //ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }
                else if (objDelivery.DCFor == "Spares")
                {
                    //SparesOrder_Fill();
                    //lblSalesOrderNo.Text = "Spares Order No.";
                    //lblSalesOrderDate.Text = "Spares Order Date";
                    //lblOrderedItemsHeading.Text = "Spares Ordered Items";
                    //ddlSalesOrderNo.SelectedValue = objDelivery.SPOId;
                    //ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }
                objDelivery.DeliveryDetails_SelectInvoice(ddlDeviveryNo.SelectedItem.Value, gvDeliveryChallanItems);
                objDelivery.DeliveryDetails_SelectInvoiceExtra1(ddlDeviveryNo.SelectedItem.Value, gvdeliveryChallanExtra);
                //objDelivery.DeliveryDetails_SelectInvoiceHighSeaSale(ddlDeviveryNo.SelectedItem.Value, gvDeliveryHighonsale);




                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                objInventory.SalesInvoice_SelectDelivery(ddlDeviveryNo.SelectedItem.Value);
                SM.DDLBindWithSelect(ddlSalesReturn, "select SR_ID,SR_NO from YANTRA_SALES_RETURN_MAST where DC_ID = " + ddlDeviveryNo.SelectedItem.Value);

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
    #endregion
   

    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[4].Text))).ToString("F");
            e.Row.Cells[7].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[6].Text)) * (Convert.ToDecimal(e.Row.Cells[4].Text))).ToString("F");
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[7].Text);
            lblOrderedValue.Text = TotalAmount.ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Text = "Total Amount:";
            e.Row.Cells[7].Text = TotalAmount.ToString();
        }

    }
    protected void ddlSalesReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
       // lblSalesReturnItems.Text = "Sales Return Items";
        Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
        objInventory.SalesReturnDetails_Select(ddlSalesReturn.SelectedItem.Value, gvItmDetails);
    }
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text));
            totalamount4 = totalamount4 + Convert.ToDecimal(e.Row.Cells[9].Text);
          //  lblSalesReturnAmount.Text = Convert.ToDecimal(TotalAmount).ToString();
            lblGoodsreturn.Text = totalamount4.ToString();





        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Total Amount:";
            e.Row.Cells[9].Text = totalamount4.ToString();
        }
    }
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[12].Text))).ToString("F");
            e.Row.Cells[11].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[10].Text)) * (Convert.ToDecimal(e.Row.Cells[5].Text))).ToString("F");
            //e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[4].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            totalamount2 = totalamount2 + Convert.ToDecimal(e.Row.Cells[11].Text);
            lblDCvalue.Text = totalamount2.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Text = "Total Amount:";
            e.Row.Cells[11].Text = totalamount2.ToString();
        }
    }
    protected void rdbPO_CheckedChanged(object sender, EventArgs e)
    {
        tblPo.Visible = true;
        tblwithDc.Visible = false;
    }
    protected void ddlsalesinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        Inventory.Delivery objDelivery = new Inventory.Delivery();
        objDelivery.DeliveryDetails_SelectInvoiceSI(ddlSalesOrderNo.SelectedItem.Value, gvSalesInvoice);
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        tblwithDc.Visible = true;
        tblPo.Visible = false;
    }
    protected void ddlDeliveryCallanOnsample_SelectedIndexChanged(object sender, EventArgs e)
    {
        Inventory.Delivery objDelivery = new Inventory.Delivery();
        SM.DDLBindWithSelect(ddlSalesReturnOnDc, "select SR_ID,SR_NO from YANTRA_SALES_RETURN_MAST where DC_ID = " + ddlDeliveryCallanOnsample.SelectedItem.Value);
        SM.DDLBindWithSelect(ddlSalesInvoiceOnDc, "select SI_ID,SI_NO from YANTRA_SALES_INVOICE_MAST where DC_ID = " + ddlDeliveryCallanOnsample.SelectedItem.Value);
        objDelivery.DeliveryDetails_SelectInvoiceExtraonDc(ddlDeliveryCallanOnsample.SelectedItem.Value, gvExtraItems);
        objDelivery.DeliveryDetails_SelectInvoiceExtraonCash(ddlDeliveryCallanOnsample.SelectedItem.Value, gvDconCash);

    }
    protected void ddlSalesInvoiceOnDc_SelectedIndexChanged(object sender, EventArgs e)
    {
        Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
        objInventory.SalesInvoiceDetailsSample_Select(ddlSalesInvoiceOnDc.SelectedItem.Value, gvsalesinvoiceondc);
    }
    protected void gvsalesinvoiceondc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));

        }
    }
    protected void ddlSalesReturnOnDc_SelectedIndexChanged(object sender, EventArgs e)
    {
        Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
        objInventory.SalesReturnDetailsSample_Select(ddlSalesReturnOnDc.SelectedItem.Value, gvSlaesreturn);

    }
    protected void gvSlaesreturn_RowDataBound(object sender, GridViewRowEventArgs e)
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



            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            //  txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            e.Row.Cells[10].Text = "Total Amount:";
            e.Row.Cells[11].Text = TotalAmount.ToString();
            
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //txtVAT.Text = VatCalc().ToString();
            //txtCST.Text = CstCalc().ToString();
        }
    }
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeviveryNo.SelectedValue = "0";
        ddlSalesOrderNo.SelectedValue = "0";
        SalesOrder_Fill();
        DeliveryonDc_fill();
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource1";
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
    protected void gvdeliveryChallanExtra_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[12].Text))).ToString("F");
            e.Row.Cells[11].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[10].Text)) * (Convert.ToDecimal(e.Row.Cells[5].Text))).ToString("F");
            //e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[4].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            totalamount11 = totalamount11 + Convert.ToDecimal(e.Row.Cells[11].Text);
            lblExtraDc.Text = totalamount11.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Text = "Total Amount:";
            e.Row.Cells[11].Text = totalamount11.ToString();
        }
    }
    protected void gvSalesInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[5].Text)) * (Convert.ToDecimal(e.Row.Cells[6].Text))).ToString("F");
            // totalamount10 = totalamount10 + Convert.ToDecimal(e.Row.Cells[7].Text);
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalamount5 = totalamount5 + Convert.ToDecimal(e.Row.Cells[9].Text);
            lblInvoicedAmt.Text = totalamount5.ToString();

          decimal kk = Convert.ToDecimal(lblInvoicedAmt.Text) - Convert.ToDecimal(lblOrderedValue.Text);
          lblBalanceInovieAmount.Text = kk.ToString();

        }

       

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Total Amount:";
            e.Row.Cells[9].Text = totalamount5.ToString();
        }
    }
    protected void gvdcqty_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Inventory/Statementprint.aspx");
    }
    protected void gvPendingMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[9].Text))).ToString("F");
            e.Row.Cells[7].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[6].Text)) * (Convert.ToDecimal(e.Row.Cells[4].Text))).ToString("F");
            totalamount10 = totalamount10 + Convert.ToDecimal(e.Row.Cells[7].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Text = "Total Amount:";
            e.Row.Cells[7].Text = totalamount10.ToString();
        }
    }



    protected void btnvatcal_Click(object sender, EventArgs e)
    {
        MessageBox.Show(this, "Hai");
        decimal totalAmount45 = Convert.ToDecimal(lblBalanceAmount.Text) + Convert.ToDecimal(lblBalanceInovieAmount);
        decimal vat = Convert.ToDecimal(txtVat.Text);
        decimal vatCalulated = (totalAmount45 / 100) * vat;
        decimal grandTotal = (totalAmount45 + vatCalulated);
        lblVatresult.Text = grandTotal.ToString(); 
    }
   
}


 
