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

public partial class Modules_Inventory_StatementOfAccount : System.Web.UI.Page
{
    decimal TotalAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerName_Fill();
            gvDeliveryChallanItems.DataBind();
            gvItemDetails.DataBind();
            gvItmDetails.DataBind();
        }

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
        gvDeliveryChallanItems.DataBind();
        gvItemDetails.DataBind();
       
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
            Inventory.Delivery.DeliveryChallanApproved_SelectSI(ddlDeviveryNo, SoId);
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
        rbtnListStatement.Visible = true;
        lblSalesOrderNo.Text = "Sales Order No.";
        if (lblSalesOrderNo.Text == "Sales Order No.")
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
                        objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
                    }
                    txtSalesOrderDate.Text = objSM.SODate;
                    objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                    Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                    objInventory.SalesInvoice_SelectSO(ddlSalesOrderNo.SelectedItem.Value);
                    //objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
                    lblOrderedItemsHeading.Text = "Sales Ordered Items";
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
                rbtnListStatement.SelectedIndex = 0;
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
        else if (lblSalesOrderNo.Text == "Spares Order No.")
        {
            try
            {
                Services.SparesOrder objSM = new Services.SparesOrder();
                if (objSM.SparesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
                {
                    txtSalesOrderDate.Text = objSM.SPODate;
                    //objSM.SparesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
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

    protected void rbtnListStatement_SelectedIndexChanged(object sender, EventArgs e)
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
                    if (objDelivery.Delivery_SelectSO(ddlSalesOrderNo.SelectedItem.Value) > 0)
                    {
                        if (rbtnListStatement.SelectedValue == "DC")
                        {
                            lblDelivery.Text = "Delivery Challan No";
                            Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
                            lblDeliveredItemsHeading.Text = "Delivery Challan Items";
                            objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
                            rbtnListStatement.SelectedIndex = 0;
                        }
                        else if (rbtnListStatement.SelectedValue == "SI")
                        {
                            lblDelivery.Text = "Sales Invoice No";
                            lblDeliveredItemsHeading.Text = "Sales Invoice Items";
                            Invoice_Fill(ddlSalesOrderNo.SelectedItem.Value);
                            objDelivery.DeliveryDetails_SelectInvoiceSI(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
                            rbtnListStatement.SelectedIndex = 1;
                        }
                    }
                    txtSalesOrderDate.Text = objSM.SODate;
                    objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                    Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                    objInventory.SalesInvoice_SelectSO(ddlSalesOrderNo.SelectedItem.Value);
                    //objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
                    lblOrderedItemsHeading.Text = "Sales Ordered Items";
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
                SM.Dispose();
            }
        }
    }
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
                    lblOrderedItemsHeading.Text = "Sales Ordered Items";
                    //ddlSalesOrderNo.SelectedValue = objDelivery.SOId;
                    //ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }
                else if (objDelivery.DCFor == "Spares")
                {
                    //SparesOrder_Fill();
                    lblSalesOrderNo.Text = "Spares Order No.";
                    lblSalesOrderDate.Text = "Spares Order Date";
                    lblOrderedItemsHeading.Text = "Spares Ordered Items";
                    //ddlSalesOrderNo.SelectedValue = objDelivery.SPOId;
                    //ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }
                objDelivery.DeliveryDetails_SelectInvoice(ddlDeviveryNo.SelectedItem.Value, gvDeliveryChallanItems);
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (rbtnListStatement.SelectedValue == "DC")
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoicestatement&siid=" + ddlSalesOrderNo.SelectedValue + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else if (rbtnListStatement.SelectedValue == "SI")
        {
            //Inventory.SalesInvoice obj = new Inventory.SalesInvoice();
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=StatementOfAcc&siid=" + ddlSalesOrderNo.SelectedValue + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }

    }

    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[4].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[6].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = "Total Amount:";
            e.Row.Cells[6].Text = TotalAmount.ToString();
        }

    }
    protected void ddlSalesReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSalesReturnItems.Text = "Sales Return Items";
        Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
     
        objInventory.SalesReturnDetails_Select(ddlSalesReturn.SelectedItem.Value, gvItmDetails);
    }
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         
            
            e.Row.Cells[9].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text));
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[9].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Total Amount:";
            e.Row.Cells[9].Text = TotalAmount.ToString();
        }
    }
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
                       TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[7].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total Amount:";
            e.Row.Cells[7].Text = TotalAmount.ToString();
        }
    }
}

 
