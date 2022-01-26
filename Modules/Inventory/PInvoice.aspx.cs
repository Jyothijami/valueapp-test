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
using System.Data.SqlClient;

public partial class Modules_Inventory_PInvoice : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGSTTotal.Text == "") { txtGSTTotal.Text = "0"; }
        //if (txtCST.Text == "") { txtCST.Text = "0"; }
        //if (txtInclude.Text == "") { txtInclude.Text = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }

        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtTotalAmt.Text == "" || txtTotalAmt.Text == null) { txtTotalAmt .Text = "0"; }

        if (lblTtlAmt_Previous.Text == "" || lblTtlAmt_Previous.Text == null) { lblTtlAmt_Previous.Text = "0"; }
        if (lblTtlGSTAmt_Previous.Text == "" || lblTtlGSTAmt_Previous.Text == null) { lblTtlGSTAmt_Previous.Text = "0"; }

        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(txtMiscelleneous.Text)
                  - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtTotalAmt.Text))) / 100));


        //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) +
        //Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGSTTotal.Text) -
        //((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));

        //if (txtGrossAmount.Text != "" || txtGrossAmount.Text != "0")
        //{
        //    txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossAmount.Text) +
        //    Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGSTTotal.Text) -
        //    ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossAmount.Text))) / 100));
        //}
        //else
        //{
        //    txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) +
        //    Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGSTTotal.Text) -
        //    ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        //}


        //txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();
        //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtGSTTAX.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGSTTotal.Text));

        #endregion
        
        if (!IsPostBack)
        {
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();

            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtGSTTotal.Attributes.Add("onkeyup", "javascript:grosscalc();");

            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtDiscount1.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtUnitprice.Attributes.Add("onfocus", "javascript:Unitamtcalc();");
            txtSpPrice.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");
            txtUnitprice.Attributes.Add("onkeyup", "javascript:amtcalcDisc1();");
            lblOrderedItemsHeading.Text = "Sales Ordered Items";
            txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //ItemTypes_Fill();
            Terms_Fill();
            EmployeeMaster_Fill();
            CustomerName_Fill();
            tblSIDetails.Visible = false;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        }
        gvProformaInvoice.DataBind();
    }
    private void Terms_Fill()
    {
        Masters.CheckboxListWithStatement(chkTerms, "Select * from Terms_Conditions");
    }
    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);

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
            SM.CustomerMaster.InvoiceCustomerMaster_SelectForCustomer(ddlCustomerName);
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
    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvProformaInvoice.SelectedIndex = -1;
        btnDelete.Attributes.Clear();
        txtInvoiceNo.Text = Inventory.ProformaInvoice.PI_AutoGenCode();
        txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        if (rdbPO.Checked == true)
        {
            rdbPO_CheckedChanged(sender, e);
        }
        else if (rdbSelf.Checked == true)
        {
            rdbSelf_CheckedChanged(sender, e);
        }
        tblSIDetails.Visible = true;
        gvItmDetails.DataBind();
        gvItemDetails_Invoiced.DataBind();
        gvInvoicedItems.DataBind();
    }
    protected void rdbPO_CheckedChanged(object sender, EventArgs e)
    {
        lblSalesOrderNo.Visible = true;
        ddlSalesOrderNo.Visible = true;
        txtSalesOrderDate.Visible = true;
        lblSalesOrderDate.Visible = true;
    }
    protected void rdbSelf_CheckedChanged(object sender, EventArgs e)
    {
        lblSalesOrderNo.Visible = false;
        ddlSalesOrderNo.Visible = false;
        txtSalesOrderDate.Visible = false;
        lblSalesOrderDate.Visible = false;
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
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesOrder_Fill();
        SM.CustomerMaster.CustomerUnits_Select(ddlunitname, ddlCustomerName.SelectedItem.Value);
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
        {
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
            if (rdbPO.Checked == true)
            {
                lblSalesOrderNo.Visible = true;
                ddlSalesOrderNo.Visible = true;
                txtSalesOrderDate.Visible = true;
                lblSalesOrderDate.Visible = true;
            }
            else if (rdbSelf.Checked == true)
            {
                lblSalesOrderNo.Visible = false;
                ddlSalesOrderNo.Visible = false;
                txtSalesOrderDate.Visible = false;
                lblSalesOrderDate.Visible = false;
            }
            Inventory.ProformaInvoice obj = new Inventory.ProformaInvoice();
            //obj.SalesPIDetails_Select(ddlCustomerName.SelectedItem.Value, gvInvoicedItems);
        }
        gvItemDetails_Invoiced.DataBind();
        gvInvoicedItems.DataBind();
        gvItmDetails.DataBind();
    }
    protected void ddlunitname_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if ((objSMCustomer.CustomerUnits_Select(ddlunitname.SelectedItem.Value)) > 0)
        {
            txtUnitaddress.Text = objSMCustomer.CustUnitAddress;
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
                    ItemTypes_Fill();
                    ddlSalesOrderNo.SelectedItem.Text = objSM.SONo;
                    lblTerms.Text = objSM.SOOtherSpec;
                    txtSalesOrderDate.Text = objSM.SODate;
                    objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails_Invoiced);
                    if (objSM.SOAdvanceAmt != "")
                    {
                        txtAdvanceAmt.Text = objSM.SOAdvanceAmt;
                    }
                    else if (txtAdvanceAmt.Text == "")
                    {
                        txtAdvanceAmt.Text = "0";
                    }
                    if (objSM.SOVAT != "")
                    {
                        //txtGSTTAX.Text = objSM.SOVAT;
                        //rbVAT.Checked = true;
                        //rbCST.Checked = false;

                    }
                    else if (objSM.SOCSTax != "")
                    {
                        //txtGSTTAX.Text = objSM.SOCSTax;
                        //lblVATCST.Text = "TAX";
                        //rbCST.Checked = true;
                        //rbVAT.Checked = false;
                    }
                    else if (objSM.SOInspection != "" && objSM.SOInspection != null)
                    {
                        //txtGSTTAX.Text = objSM.SOInspection;
                        //rbInclude.Checked = true;
                        //rbVAT.Checked = false;
                        //rbCST.Checked = false;
                    }
                    txtPaymentTerms.Text = objSM.SOPaymentTerms;
                    txtPackingCharges.Text = objSM.SOPackageCharges;
                    //txtExciseDuty.Text = objSM.SOExciseDuty;
                    Inventory.ProformaInvoice obj = new Inventory.ProformaInvoice();
                    obj.ProformaInvoice_SelectSO(ddlSalesOrderNo.SelectedItem.Value);
                    lblOrderedItemsHeading.Text = "Sales Ordered Items";
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
    #region GridView Item Details Row Databound
    protected void gvItemDetails_Invoiced_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text =  Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) / Convert.ToDecimal(e.Row.Cells[5].Text));
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[8].Text = (Convert.ToDecimal(e.Row.Cells[9].Text) / Convert.ToInt32(e.Row.Cells[5].Text)).ToString("F");
        //}

        //e.Row.Cells[10].Visible =false ;
    }
    #endregion
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtHSN.Text = objMaster.HSN_Code;
                txtGST.Text = objMaster.GST_Tax;
                txtItemname.Text = objMaster.ItemName;
                txtRemarks.Text = "-";
                foreach (GridViewRow gvRow in gvItemDetails_Invoiced.Rows)
                {
                    if (gvRow.Cells[0].Text == ddlModelNo.SelectedItem.Value)
                    {
                        txtRate.Text = gvRow.Cells[8].Text;
                        txtQuantity.Text = gvRow.Cells[5].Text;
                        //txtRate.Text = gvRow.Cells[6].Text;
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
    private void ItemTypes_Fill()
    {
        try
        {
            //Masters.ItemType.ItemType_Select(ddlItemType);
            SM.SalesOrder.SalesOrderItemTypes2_Select(ddlSalesOrderNo.SelectedItem.Value, ddlModelNo);
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

    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvItemDetails_Invoiced.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                DataTable SalesOrderItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("HSN_CODE");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("GST_RATE");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("UnitPrice");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Spl_Price");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Color");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderItems.Columns.Add(col);
                //col = new DataColumn("DetId");
                //SalesOrderItems.Columns.Add(col);
                if (gvItemDetails_Invoiced.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1_Items in gvItmDetails.Rows)
                    {
                        if (gvItmDetails.SelectedIndex > -1)
                        {
                            if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow.Cells[0].Text;
                                dr["ModelNo"] = gvrow.Cells[1].Text;
                                dr["HSN_CODE"] = gvrow.Cells[2].Text;
                                dr["ItemName"] = gvrow.Cells[3].Text;
                                dr["UOM"] = gvrow.Cells[4].Text;
                                dr["Quantity"] = gvrow.Cells[5].Text;

                                dr["Rate"] = gvrow.Cells[6].Text;
                                dr["GST_RATE"] = gvrow.Cells[7].Text;
                                dr["UnitPrice"] = gvrow.Cells[8].Text;
                                dr["Spl_Price"] = gvrow.Cells[9].Text;
                                dr["Color"] = gvrow.Cells[10].Text;
                                dr["ColorId"] = gvrow.Cells[11].Text;
                                dr["Remarks"] = "-";
                                SalesOrderItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow1_Items.Cells[2].Text;
                                dr["ModelNo"] = gvrow1_Items.Cells[3].Text;
                                dr["HSN_CODE"] = gvrow1_Items.Cells[4].Text;
                                dr["ItemName"] = gvrow1_Items.Cells[5].Text;
                                dr["UOM"] = gvrow1_Items.Cells[6].Text;
                                dr["Quantity"] = gvrow1_Items.Cells[7].Text;

                                dr["Rate"] = gvrow1_Items.Cells[8].Text;
                                dr["GST_RATE"] = gvrow1_Items.Cells[9].Text;
                                dr["UnitPrice"] = gvrow1_Items.Cells[10].Text;

                                //dr["Spl_Price"] = gvrow1_Items.Cells[11].Text;

                                dr["Color"] = gvrow1_Items.Cells[13].Text;
                                dr["ColorId"] = gvrow1_Items.Cells[14].Text;
                                dr["Remarks"] = gvrow1_Items.Cells[15].Text;
                                //dr["DetId"] = gvrow1_Items.Cells[15].Text;
                                SalesOrderItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = SalesOrderItems.NewRow();
                            dr["ItemCode"] = gvrow1_Items.Cells[2].Text;
                            dr["ModelNo"] = gvrow1_Items.Cells[3].Text;
                            dr["HSN_CODE"] = gvrow1_Items.Cells[4].Text;
                            dr["ItemName"] = gvrow1_Items.Cells[5].Text;
                            dr["UOM"] = gvrow1_Items.Cells[6].Text;
                            dr["Quantity"] = gvrow1_Items.Cells[7].Text;
                            dr["Rate"] = gvrow1_Items.Cells[8].Text;
                            dr["GST_RATE"] = gvrow1_Items.Cells[9].Text;

                            dr["UnitPrice"] = gvrow1_Items.Cells[10].Text;
                            //dr["Spl_Price"] = gvrow1_Items.Cells[11].Text;

                            dr["Color"] = gvrow1_Items.Cells[13].Text;
                            dr["ColorId"] = gvrow1_Items.Cells[14].Text;
                            dr["Remarks"] = gvrow1_Items.Cells[15].Text;
                            //dr["DetId"] = gvrow1_Items.Cells[15].Text;
                            SalesOrderItems.Rows.Add(dr);
                        }
                        if (gvItmDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[0].Text == gvrow1_Items.Cells[2].Text)
                            {
                                gvItmDetails.DataSource = SalesOrderItems;
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
                    DataRow drnew = SalesOrderItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[0].Text;
                    drnew["ModelNo"] = gvrow.Cells[1].Text;
                    drnew["HSN_CODE"] = gvrow.Cells[2].Text;
                    drnew["ItemName"] = gvrow.Cells[3].Text;
                    drnew["UOM"] = gvrow.Cells[4].Text;
                    drnew["Quantity"] = gvrow.Cells[5].Text;

                    drnew["Rate"] = gvrow.Cells[6].Text;
                    drnew["GST_RATE"] = gvrow.Cells[7].Text;
                    drnew["UnitPrice"] = gvrow.Cells[8].Text;
                    drnew["Spl_Price"] = gvrow.Cells[9].Text;
                    drnew["Color"] = gvrow.Cells[10].Text;
                    drnew["ColorId"] = gvrow.Cells[11].Text;
                    drnew["Remarks"] = "-";
                    SalesOrderItems.Rows.Add(drnew);
                }
                gvItmDetails.DataSource = SalesOrderItems;
                gvItmDetails.DataBind();
                gvItmDetails.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ddlModelNo.SelectedValue = "0";
        }
        catch (Exception)
        {
            ddlModelNo.Items.Clear();

            //ddlModelNo.SelectedValue = "-1";
        }
        txtItemname.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        //txt.Text = string.Empty;
        //txtCST.Text = string.Empty;
        txtExcise.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtDiscount1.Text = "";
        txtSpPrice.Text = "";
        txtUnitprice.Text = "";
        gvItmDetails.SelectedIndex = -1;
        txtHSN.Text = string.Empty;
        txtGST.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlColor.SelectedValue = "0";
    }
    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("HSN_CODE");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("GST_RATE");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Color");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesInvoiceProducts.Columns.Add(col);
        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvItmDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["HSN_CODE"] = txtHSN.Text;
                        dr["ItemName"] = txtItemname.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtQuantity.Text;
                        dr["Rate"] = txtRate.Text;
                        dr["GST_RATE"] = txtGST.Text;
                        dr["UnitPrice"] = txtUnitprice.Text;
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["Remarks"] = txtRemarks.Text;

                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["HSN_CODE"] = gvrow.Cells[4].Text;
                        dr["ItemName"] = gvrow.Cells[5].Text;
                        dr["UOM"] = gvrow.Cells[6].Text;
                        dr["Quantity"] = gvrow.Cells[7].Text;
                        dr["Rate"] = gvrow.Cells[8].Text;
                        dr["GST_RATE"] = gvrow.Cells[9].Text;


                        dr["UnitPrice"] = gvrow.Cells[10].Text;
                        dr["Color"] = gvrow.Cells[13].Text;
                        dr["ColorId"] = gvrow.Cells[14].Text;
                        dr["Remarks"] = gvrow.Cells[15].Text;

                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["HSN_CODE"] = gvrow.Cells[4].Text;
                    dr["ItemName"] = gvrow.Cells[5].Text;
                    dr["UOM"] = gvrow.Cells[6].Text;
                    dr["Quantity"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["GST_RATE"] = gvrow.Cells[9].Text;
                    dr["UnitPrice"] = gvrow.Cells[10].Text;
                    dr["Color"] = gvrow.Cells[13].Text;
                    dr["ColorId"] = gvrow.Cells[14].Text;
                    dr["Remarks"] = gvrow.Cells[15].Text;

                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        if (gvItmDetails.SelectedIndex == -1)
        {
            DataRow drnew = SalesInvoiceProducts.NewRow();
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["HSN_CODE"] = txtHSN.Text;
            drnew["ItemName"] = txtItemname.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQuantity.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["GST_RATE"] = txtGST.Text;
            drnew["UnitPrice"] = txtUnitprice.Text;
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["Remarks"] = txtRemarks.Text;

            SalesInvoiceProducts.Rows.Add(drnew);
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
        gvItmDetails.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            int index = 0;
            foreach (ListItem a in chkTerms.Items)
            {
                if (a.Selected == true)
                    index++;
            }
            if (index < 1)
            {
                lblchkllist.Visible = true;
            }
            else
            {
                lblchkllist.Visible = false;

                SalesInvoiceSave();

            }
        }
        else if (btnSave.Text == "Update")
        {
            int index = 0;
            foreach (ListItem a in chkTerms.Items)
            {
                if (a.Selected == true)
                    index++;
            }
            if (index < 1)
            {
                lblchkllist.Visible = true;
            }
            else
            {
                lblchkllist.Visible = false;

                SalesInvoiceUpdate();

            }
        }
    }
    private void SalesInvoiceSave()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                btnSave.Enabled = false;
              
                if (txtGSTTotal.Text == "") { txtGSTTotal.Text = "0"; }
                //if (txtCST.Text == "") { txtCST.Text = "0"; }
                //if (txtInclude.Text == "") { txtInclude.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
                if (txtFright.Text == "") { txtFright.Text = "0"; }
                if (txtAdvanceAmt.Text == "") { txtAdvanceAmt.Text = "0"; }
                if (txtPackingCharges.Text == "") { txtPackingCharges.Text = "0"; }
                Inventory.ProformaInvoice objInventory = new Inventory.ProformaInvoice();
                objInventory.PINO = txtInvoiceNo.Text;
                objInventory.PIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
               
                    objInventory.SOID = ddlSalesOrderNo.SelectedItem.Value;
                    objInventory.PIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    //objInventory.PIApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    if (rdbPO.Checked == true)
                    {
                        objInventory.PIType = "With PO";
                    }
                    else if (rdbSelf.Checked == true)
                    {
                        objInventory.PIType = "WithOut PO/Quot";
                    }
                    objInventory.SplDisc = txtDiscount.Text;

                    objInventory.PIMissCharges = txtMiscelleneous.Text;
                    objInventory.Fright = txtFright.Text;
                    objInventory.PI_Total = txtTotalAmt.Text;
                    objInventory.PIGross = txtGrossAmount.Text;

                    objInventory.PIRemarks = txtRemarks1.Text;
                    objInventory.OtherSpec = txtTerms.Text;
                    objInventory.PaymentTerms = txtPaymentTerms.Text;
                    objInventory.TransportCharges = txtTransportcharges.Text;
                    objInventory.PackingCharges = txtPackingCharges.Text;
                    objInventory.AdvanceAmount = txtAdvanceAmt.Text;
                    objInventory.GSTType = ddlGSTType.SelectedItem.Value;
                    //objInventory.Fright = txtFright.Text;
                    objInventory.PIGST = txtGSTTotal.Text;
                    //objInventory.PICST = txtCST.Text;
                    objInventory.CPID = lblCPID.Text;
                    //objInventory.InvoiceNo = txtInno.Text;
                    objInventory.CustUnitId = ddlunitname.SelectedItem.Value;
                    objInventory.CUSTID = ddlCustomerName.SelectedItem.Value;
                    //objInventory.PIInclude = txtInclude.Text;

                    if (objInventory.PI_Save() == "Data Saved Successfully")
                    {
                        foreach (GridViewRow gvrow in gvItmDetails.Rows)
                        {
                            objInventory.ItemCode = gvrow.Cells[2].Text;
                            objInventory.PIDetQty = gvrow.Cells[7].Text;
                            objInventory.PIDetRate = gvrow.Cells[8].Text;
                            //TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                            //objInventory.PIDetQty = qty.Text;
                            //TextBox txtPrice = (TextBox)gvrow.FindControl("txtPrice");
                            //objInventory.PIDetRate = txtPrice.Text;
                            objInventory.GSTDetRate = gvrow.Cells[9].Text;
                            objInventory.PIDetUnitPrice = gvrow.Cells[10].Text;
                            objInventory.PIDetSplAmt = gvrow.Cells[11].Text;

                            objInventory.PIColiId = gvrow.Cells[14].Text;
                            objInventory.PIDescrption = gvrow.Cells[15].Text;
                            objInventory.PIDetails_Save();
                        }
                        for (int i = 0; i < chkTerms.Items.Count; i++)
                        {
                            if (chkTerms.Items[i].Selected == true)
                            {
                                objInventory.Term_id = chkTerms.Items[i].Value;
                                objInventory.TermTitle = chkTerms.Items[i].Text;
                                objInventory.Others = "0";
                                objInventory.Terms_Save();
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
                btnSave.Enabled = true;
                gvItemDetails_Invoiced.DataBind();
                gvItmDetails.DataBind();
                tblSIDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
       "alert(' Data Saved sucessfully');window.location ='PInvoice.aspx';", true);
            }
        }
    }
    private void SalesInvoiceUpdate()
    {
        try
        {
            if (txtGSTTotal.Text == "") { txtGSTTotal.Text = "0"; }
            //if (txtCST.Text == "") { txtCST.Text = "0"; }
            //if (txtInclude.Text == "") { txtInclude.Text = "0"; }
            if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
            if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
            if (txtFright.Text == "") { txtFright.Text = "0"; }
            if (txtAdvanceAmt.Text == "") { txtAdvanceAmt.Text = "0"; }
            if (txtPackingCharges.Text == "") { txtPackingCharges.Text = "0"; }
            Inventory.ProformaInvoice objInventory = new Inventory.ProformaInvoice();
            objInventory.PIID = gvProformaInvoice.SelectedRow.Cells[0].Text;
            objInventory.PIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
            objInventory.PINO = txtInvoiceNo.Text;
            objInventory.SOID = ddlSalesOrderNo.SelectedItem.Value;
            objInventory.PIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            //objInventory.PIApprovedBy = ddlApprovedBy.SelectedItem.Value;
            if (rdbPO.Checked == true)
            {
                objInventory.PIType = "With PO";
            }
            else if (rdbSelf.Checked == true)
            {
                objInventory.PIType = "WithOut PO/Quot";
            }
            objInventory.SplDisc = txtDiscount.Text;

            objInventory.PIMissCharges = txtMiscelleneous.Text;
            objInventory.Fright = txtFright.Text;
            objInventory.PI_Total = txtTotalAmt.Text;
            objInventory.PIGross = txtGrossAmount.Text;

            objInventory.PIRemarks = txtRemarks1.Text;
            objInventory.OtherSpec = txtTerms.Text;
            objInventory.PaymentTerms = txtPaymentTerms.Text;
            objInventory.TransportCharges = txtTransportcharges.Text;
            objInventory.PackingCharges = txtPackingCharges.Text;
            objInventory.AdvanceAmount = txtAdvanceAmt.Text;
            objInventory.GSTType = ddlGSTType.SelectedItem.Value;
            //objInventory.Fright = txtFright.Text;
            objInventory.PIGST = txtGSTTotal.Text;
            //objInventory.PICST = txtCST.Text;
            objInventory.CPID = lblCPID.Text;
            //objInventory.InvoiceNo = txtInno.Text;
            objInventory.CustUnitId = ddlunitname.SelectedItem.Value;
            objInventory.CUSTID = ddlCustomerName.SelectedItem.Value;
            //objInventory.PIInclude = txtInclude.Text;
            if (objInventory.PI_Update() == "Data Updated Successfully")
            {
                foreach (GridViewRow gvrow in gvItmDetails.Rows)
                {
                    objInventory.ItemCode = gvrow.Cells[2].Text;
                    objInventory.PIDetQty = gvrow.Cells[7].Text;
                    objInventory.PIDetRate = gvrow.Cells[8].Text;
                    objInventory.GSTDetRate = gvrow.Cells[9].Text;

                    objInventory.PIDetUnitPrice = gvrow.Cells[10].Text;
                    objInventory.PIDetSplAmt = gvrow.Cells[11].Text;
                    objInventory.PIColiId = gvrow.Cells[14].Text;
                    objInventory.PIDescrption = gvrow.Cells[15].Text;
                    objInventory.PIDetails_Save();
                }
                objInventory .Others =gvProformaInvoice .SelectedRow .Cells [0].Text ;
                for (int i = 0; i < chkTerms.Items.Count; i++)
                {
                    int count = ChkTermExistence(Convert.ToInt32(objInventory.Others), Convert.ToInt32(chkTerms.Items[i].Value));

                    if (count <= 0)
                    {
                        if (chkTerms.Items[i].Selected == true)
                        {
                            objInventory.Term_id = chkTerms.Items[i].Value;
                            objInventory.TermTitle = chkTerms.Items[i].Text;
                            objInventory.Others = "0";
                            objInventory.Terms_Save();
                        }
                    }
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnSave.Text = "Save";
            gvItemDetails_Invoiced.DataBind();
            gvItemDetails_Invoiced.SelectedIndex = -1;
            gvItmDetails.DataBind();
            tblSIDetails.Visible = false;
            Inventory.ClearControls(this);
            Inventory.Dispose();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
       "alert(' Data Updated sucessfully');window.location ='PInvoice.aspx';", true);
        }
    }

    private int ChkTermExistence(int OthersId, int TermId)
    {
        SqlCommand cmd = new SqlCommand("select COUNT (*) from Terms_Conditions_Selected where Others =" + OthersId + " and Term_Id =" + TermId + " ", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da2.Fill(dt);
        int IsExist = 0;
        return IsExist = Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    protected void lbtnPINO_Click(object sender, EventArgs e)
    {
        tblSIDetails.Visible = false;

        LinkButton lbtnPINO;
        lbtnPINO = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPINO.Parent.Parent;
        gvProformaInvoice.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        try
        {
            Inventory.ProformaInvoice objInventory = new Inventory.ProformaInvoice();
            if (objInventory.ProformaInvoice_Select(gvProformaInvoice.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblSIDetails.Visible = true;
                txtInvoiceNo.Text = objInventory.PINO;
                txtInvoiceDate.Text = objInventory.PIDate;

                ddlPreparedBy.SelectedValue = objInventory.PIPreparedBy;
                //ddlApprovedBy.SelectedValue = objInventory.PIApprovedBy;
                ddlCustomerName.SelectedValue = objInventory.CUSTID;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                ddlunitname.SelectedValue = objInventory.UnitId;
                ddlunitname_SelectedIndexChanged(sender, e);
                if (objInventory.PIType == "With PO")
                {
                    rdbPO.Checked = true;
                    rdbPO_CheckedChanged(sender, e);
                    ddlSalesOrderNo.SelectedValue = objInventory.SOID;
                    ddlSalesOrderNo.SelectedItem.Text = objInventory.SoNo;
                    ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                }
                else if (objInventory.PIType == "WithOut PO/Quot")
                {
                    rdbSelf.Checked = true;
                    rdbSelf_CheckedChanged(sender, e);
                    rdbPO.Checked = false;
                }


                txtRemarks1.Text = objInventory.PIRemarks;
                txtTerms.Text = objInventory.OtherSpec;
                txtPaymentTerms.Text = objInventory.PaymentTerms;
                txtTransportcharges.Text = objInventory.TransportCharges;
                txtPackingCharges.Text = objInventory.PackingCharges;

                txtGSTTotal.Text = objInventory.PIGST;
                txtTotalAmt.Text = objInventory.PI_Total;
                txtDiscount.Text = objInventory.PIDisc;
                txtGrossAmount.Text = objInventory.PIGross;
                txtAdvanceAmt.Text = objInventory.AdvanceAmount;
                txtFright.Text = objInventory.Fright;

                //ddlGSTType.SelectedItem .Value  = objInventory.GSTType;
                ddlGSTType.SelectedIndex = ddlGSTType.Items.IndexOf(ddlGSTType.Items.FindByValue(objInventory.GSTType));
                //txtInno.Text = objInventory.InvoiceNo;

                //txtFright.Text = obj.Fright;
                objInventory.ProformaInvoiceDetails_Select(gvProformaInvoice.SelectedRow.Cells[0].Text, gvInvoicedItems);

                #region Invoiced Items values
                lblTtlAmt_Previous.Text = GrossAmount().ToString();
                lblTtlGSTAmt_Previous.Text = GSTTotal().ToString();
                #endregion

                //gvItemDetails_Invoiced.DataBind();
                //gvInvoicedItems.DataBind();
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblSIDetails.Visible = true;
        try
        {

            if (gvProformaInvoice.SelectedRow.Cells[0].Text != null)
            {
                try
                {
                    Inventory.ProformaInvoice objInventory = new Inventory.ProformaInvoice();
                    if (objInventory.ProformaInvoice_Select(gvProformaInvoice.SelectedRow.Cells[0].Text) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblSIDetails.Visible = true;
                        ddlCustomerName.SelectedValue = objInventory.CUSTID;
                        ddlCustomerName_SelectedIndexChanged(sender, e);
                        ddlunitname.SelectedValue = objInventory.UnitId;
                        ddlunitname_SelectedIndexChanged(sender, e);

                        if (objInventory.PIType == "With PO")
                        {
                            rdbPO.Checked = true;
                            rdbPO_CheckedChanged(sender, e);
                            ddlSalesOrderNo.SelectedValue = objInventory.SOID;
                            ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                        }
                        else if (objInventory.PIType == "WithOut PO/Quot")
                        {
                            rdbSelf.Checked = true;
                            rdbSelf_CheckedChanged(sender, e);
                            rdbPO.Checked = false;
                        }
                        txtInvoiceNo.Text = objInventory.PINO;
                        txtInvoiceDate.Text = objInventory.PIDate;
                        ddlPreparedBy.SelectedValue = objInventory.PIPreparedBy;
                        //ddlApprovedBy.SelectedValue = objInventory.PIApprovedBy;

                        //ddlGSTType.SelectedValue = objInventory.GSTType;
                        ddlGSTType.SelectedIndex = ddlGSTType.Items.IndexOf(ddlGSTType.Items.FindByValue(objInventory.GSTType));

                        txtRemarks1.Text = objInventory.PIRemarks;
                        txtTerms.Text = objInventory.OtherSpec;
                        txtPaymentTerms.Text = objInventory.PaymentTerms;
                        txtTransportcharges.Text = objInventory.TransportCharges;
                        txtPackingCharges.Text = objInventory.PackingCharges;


                        txtGSTTotal.Text = objInventory.PIGST;
                        txtTotalAmt.Text = objInventory.PI_Total;
                        txtDiscount.Text = objInventory.PIDisc;
                        txtGrossAmount.Text = objInventory.PIGross;
                        txtAdvanceAmt.Text = objInventory.AdvanceAmount;
                        txtFright.Text = objInventory.Fright;

                        objInventory.ProformaInvoiceDetails_Select(gvProformaInvoice.SelectedRow.Cells[0].Text, gvInvoicedItems);

                        #region Invoiced Items values

                        lblTtlAmt_Previous.Text = GrossAmount().ToString();
                        lblTtlGSTAmt_Previous.Text = GSTTotal().ToString();
                        #endregion
                        DataTable dt = objInventory.PITerms_Select(int.Parse(gvProformaInvoice.SelectedRow.Cells[0].Text));
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ListItem currentCheckBox = chkTerms.Items.FindByValue(dt.Rows[i][0].ToString());
                            if (currentCheckBox != null)
                            {
                                currentCheckBox.Selected = true;
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
                    Inventory.Dispose();
                }
            }
            else
            {
                MessageBox.Show(this, "Please select atleast a Record");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
    }
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {


            //e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[14].Visible = false;

            //e.Row.Cells[15].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[7].Text) * Convert.ToDecimal(e.Row.Cells[10].Text));
            e.Row.Cells[12].Text = Convert.ToString((Convert.ToDecimal(e.Row.Cells[11].Text)) * Convert.ToDecimal(e.Row.Cells[9].Text) / 100);

        }
        if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
        {

            if (lblTtlAmt_Previous.Text == "" || lblTtlAmt_Previous.Text == null) { lblTtlAmt_Previous.Text = "0"; }
            if (lblTtlGSTAmt_Previous.Text == "" || lblTtlGSTAmt_Previous.Text == null) { lblTtlGSTAmt_Previous.Text = "0"; }

            txtGrossTotalAmtHidden.Value = GrossAmountCalc().ToString();
            txtGSTTotal.Text = GSTTotalCal().ToString();

            lbl_Current_Total.Text = GrossAmountCalc().ToString();
            lbl_Current_Gst.Text = GSTTotalCal().ToString();


            txtTotalAmt.Text = (Convert.ToDecimal(lblTtlAmt_Previous.Text) + Convert.ToDecimal(lbl_Current_Total.Text)).ToString();
            txtGSTTotal.Text = (Convert.ToDecimal(lblTtlGSTAmt_Previous.Text) + Convert.ToDecimal(lbl_Current_Gst.Text)).ToString();

            lblTemp_Gross_WithoutDisc.Text = Convert.ToString( Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(txtMiscelleneous.Text) ) ;

            txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(lblTemp_Gross_WithoutDisc.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(lblTemp_Gross_WithoutDisc.Text))) / 100));

            //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(txtMiscelleneous.Text)
            //       - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtTotalAmt.Text))) / 100));

            //txtTotalAmt.Text = (Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(lblTtlAmt_Previous.Text)).ToString();
            //txtGSTTotal.Text = (Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(lblTtlGSTAmt_Previous.Text)).ToString();

            //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) +
            //                            Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGSTTotal.Text)
            //       - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));


            //txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt_Previous.Text)).ToString();
            //txtGrossTotalAmtHidden.Value = txtGSTTotal.Text = (Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(lblGSTtotal.Text)).ToString();
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvProformaInvoice.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvProformaInvoice.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvProformaInvoice.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "PI Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
    #endregion
    #region ddlSymbols_SelectedIndexChanged
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion
    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvProformaInvoice.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "PI Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvProformaInvoice.DataBind();
    }
    #endregion
    protected void gvProformaInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvProformaInvoice.SelectedIndex > -1)
        {
            try
            {
                Inventory.ProformaInvoice obj = new Inventory.ProformaInvoice();
                MessageBox.Show(this, obj.PI_Delete(gvProformaInvoice.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvProformaInvoice.DataBind();
                tblSIDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvInvoicedItems.SelectedIndex = gvRow.RowIndex;

        Inventory.ProformaInvoice objInventory = new Inventory.ProformaInvoice();
        int _true = objInventory.PIDetails_Delete(gvInvoicedItems.SelectedRow.Cells[14].Text);

        if (_true == 1)
        {
            objInventory.ProformaInvoiceDetails_Select(gvProformaInvoice.SelectedRow.Cells[0].Text, gvInvoicedItems);

            lblTtlAmt_Previous.Text = GrossAmount().ToString();
            lblTtlGSTAmt_Previous.Text = GSTTotal().ToString();

            //txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            //txtGSTTotal.Text = GSTTotalCal().ToString();
            //txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt_Previous.Text)).ToString();
            //txtGSTTotal.Text = (Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(lblTtlGSTAmt_Previous.Text)).ToString();


            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            txtGSTTotal.Text = GSTTotalCal().ToString();
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt_Previous.Text)).ToString();
            txtGSTTotal.Text = (Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(lblTtlGSTAmt_Previous.Text)).ToString();

            txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGSTTotal.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));

            //gvProformaInvoice.DataBind();

            MessageBox.Show(this, "Data Deleted");
        }
        else
        {
            MessageBox.Show(this, "Unable to delete the data, Please contact admin");
        }


    }
    private double GrossAmount()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvInvoicedItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[10].Text);
        }
        return _totalAmt;
    }
    private double GSTTotal()
    {
        double _totalAmt1 = 0;
        foreach (GridViewRow gvrow in gvInvoicedItems.Rows)
        {
            _totalAmt1 = _totalAmt1 + Convert.ToDouble(gvrow.Cells[13].Text);
        }
        return _totalAmt1;
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }
    private double GSTTotalCal()
    {
        double _totalAmt1 = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalAmt1 = _totalAmt1 + Convert.ToDouble(gvrow.Cells[12].Text);
        }
        return _totalAmt1;
    }
    protected void gvItmDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItmDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("HSN_CODE");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("VAT");
        //SalesInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Cst");
        //SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("GST_RATE");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Color");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("DetId");
        //SalesInvoiceProducts.Columns.Add(col);
        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["HSN_CODE"] = gvrow.Cells[4].Text;
                    dr["ItemName"] = gvrow.Cells[5].Text;
                    dr["UOM"] = gvrow.Cells[6].Text;
                    dr["Quantity"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["GST_RATE"] = gvrow.Cells[9].Text;
                    dr["UnitPrice"] = gvrow.Cells[10].Text;

                    dr["Color"] = gvrow.Cells[13].Text;
                    dr["ColorId"] = gvrow.Cells[14].Text;
                    dr["Remarks"] = gvrow.Cells[15].Text;
                    //dr["DetId"] = gvrow.Cells[15].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        txtGSTTotal.Text = GSTTotalCal().ToString();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt_Previous.Text)).ToString();
        txtGSTTotal.Text = (Convert.ToDecimal(txtGSTTotal.Text) + Convert.ToDecimal(lblTtlGSTAmt_Previous.Text)).ToString();
    }
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
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);

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
            dr["VAT"] = "0";
            dr["Cst"] = "0";
            dr["Excise"] = "0";

            dr["ModelNo"] = gvrow.Cells[3].Text;
            SalesInvoiceProducts.Rows.Add(dr);
            if (gvrow.RowIndex == gvItmDetails.Rows[e.NewEditIndex].RowIndex)
            {

                ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                ddlModelNo_SelectedIndexChanged(sender, e);
                txtItemUOM.Text = gvrow.Cells[5].Text;
                txtQuantity.Text = gvrow.Cells[6].Text;
                txtRate.Text = gvrow.Cells[7].Text;

                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text));
                gvItmDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    protected void gvInvoicedItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
            e.Row.Cells[14].Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[0].Visible = false;
        //    //e.Row.Cells[15].Visible = false;
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[15].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Products list?');");
            //e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[10].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[7].Text) * Convert.ToDecimal(e.Row.Cells[11].Text));
            e.Row.Cells[13].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[10].Text) * Convert.ToDecimal(e.Row.Cells[9].Text) / 100);

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            lblTtlAmt_Previous.Text = GrossAmount().ToString();
            lblTtlGSTAmt_Previous.Text = GSTTotal().ToString();
        }
    }


    //protected void btnApprove_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Inventory.ProformaInvoice objSI = new Inventory.ProformaInvoice();
    //        Inventory.BeginTransaction();
    //        objSI.PIID = Request.QueryString["SI_ID"].ToString();
    //        objSI.PIApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
    //        objSI.SalesInvoiceApprove_Update();
    //        Inventory.CommitTransaction();
    //        MessageBox.Show(this, "Data Approved Successfully");
    //    }
    //    catch (Exception ex)
    //    {
    //        Inventory.RollBackTransaction();
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        // gvSalesInvoiceDetails.DataBind();
    //        btnEdit_Click(sender, e);
    //    }
    //}
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvProformaInvoice.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PInvoice1&PIid=" + gvProformaInvoice.SelectedRow.Cells[0].Text + " ";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select a Record");
        }
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSearch_ItemModel_No_Click(object sender, EventArgs e)
    {
        if (txtSearchModel1.Text != "")
        {
            ddlModelNo.DataSourceID = "SqlDataSource2";
            ddlModelNo.DataTextField = "ITEM_MODEL_NO";
            ddlModelNo.DataValueField = "ITEM_CODE";
            ddlModelNo.DataBind();
            ddlModelNo_SelectedIndexChanged(sender, e);

            //ddlModelNo.Items.Clear();
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void gvProformaInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProformaInvoice. PageIndex = e.NewPageIndex;
       
    }
}