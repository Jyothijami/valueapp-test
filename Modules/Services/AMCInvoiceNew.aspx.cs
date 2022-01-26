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
public partial class Modules_Services_AMCInvoiceNew : basePage
{
    //#region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //    if (txtMiscelleneous.Text != "" && txtDiscount.Text != "")
    //    {
    //        if (txtGrossTotalAmtHidden.Value == "")
    //        {
    //            txtGrossTotalAmtHidden.Value = "0";
    //        }

    //        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text))) / 100));
    //    }
    //    if (rbVAT.Checked == true)
    //    {
    //        txtVAT.Style.Add("display", "");
    //        lblVAT.Style.Add("display", "");
    //        txtCST.Style.Add("display", "none");
    //        lblCSTax.Style.Add("display", "none");
    //    }
    //    else if (rbCST.Checked == true)
    //    {
    //        txtVAT.Style.Add("display", "none");
    //        lblVAT.Style.Add("display", "none");
    //        txtCST.Style.Add("display", "");
    //        lblCSTax.Style.Add("display", "");
    //    }
    //    #endregion
        string invoiceNo="";
    protected void Page_Load(object sender, EventArgs e)
    {
        invoiceNo=Request.QueryString["invoiceNo"];
        if(!IsPostBack)
        {
if(invoiceNo==null)
{
            Services.ClearControls(this);
           // gvSalesInvoiceDetails.SelectedIndex = -1;
            txtInvoiceNo.Text = Services.AmcInvoice.AmcInvoice_AutoGenCode();
            txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Text = "Save";
            btnSave.Enabled = true;
            tblSIDetails.Visible = true;
            gvItemDetails.DataBind();
            gvItmDetails.DataBind();
}
else
{
 try
            {
                Services.AmcInvoice objInventory = new Services.AmcInvoice();

                if (objInventory.AmcInvoice_Select(invoiceNo) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblSIDetails.Visible = true;
                    txtInvoiceNo.Text = objInventory.AMCINo;
                    txtInvoiceDate.Text = objInventory.AMCIDate;
                    ddlSalesOrderNo.SelectedValue = objInventory.AMCOId;
                    ddlPreparedBy.SelectedValue = objInventory.AMCIPreparedBy;
                    ddlApprovedBy.SelectedValue = objInventory.AMCIApprovedBy;
                    ddlInvoiceType.SelectedValue = objInventory.AMCIType;
                    ddlDeviveryNo.SelectedValue = objInventory.DCId;
                    ddlDeliveryType.SelectedValue = objInventory.DespmId;
                    txtMiscelleneous.Text = objInventory.AMCIMissChrgs;
                    txtDiscount.Text = objInventory.AMCIDiscount;
                    txtGrossAmount.Text = objInventory.AMCIGrossAmt;
                    txtRemarks.Text = objInventory.AMCIRemarks;

                    objInventory.AmcInvoiceDetails_Select(invoiceNo, gvItmDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Services.Dispose();
                ddlDeviveryNo_SelectedIndexChanged(sender, e);
            }
}


   rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtVAT.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtCST.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtExcise.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");

            //ItemTypes_Fill();
            EmployeeMaster_Fill();
            DeliveryType_Fill();
            SalesOrder_Fill();
            Delivery_Fill();
            //tblSIDetails.Visible = false;
        }
    }
   #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtMiscelleneous.Text != "" && txtDiscount.Text != "")
        {
            if (txtGrossTotalAmtHidden.Value == "")
            {
                txtGrossTotalAmtHidden.Value = "0";
            }
            txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text))) / 100));
            txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();
        }
        if (rbVAT.Checked == true)
        {
            txtVAT.Style.Add("display", "");
            lblVAT.Style.Add("display", "");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "");
            lblCSTax.Style.Add("display", "");
        }
        #endregion
        if (btnSave.Text == "Save")
        {
            btnRefresh.Visible = true;
        }
        else if (btnSave.Text == "Update")
        {
            btnRefresh.Visible = false;
        }
        if (invoiceNo!=null)
        {
            if (!string.IsNullOrEmpty(invoiceNo) && invoiceNo != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnDelete.Visible = false;
                //btnEdit.Visible = false;
                btnPrint.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnDelete.Visible = true;
                //btnEdit.Visible = true;
                btnPrint.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            //btnDelete.Visible = true;
            //btnEdit.Visible = true;
            btnPrint.Visible = false;
        }
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Services.AMCOrder.AMCOrderItemTypes_Select(ddlSalesOrderNo.SelectedValue, ddlItemType);
            //Masters.ItemType.ItemType_Select(ddlItemType);
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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Services.AMCOrder.AMCOrderItemNames_Select(ddlSalesOrderNo.SelectedValue, ddlItemType.SelectedValue, ddlItemName);
            //Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
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

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDeliveryType);

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

    #region AmcOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            Services.AMCOrder.AMCOrder_Select(ddlSalesOrderNo);
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
    private void Delivery_Fill()
    {
        try
        {
            Inventory.Delivery.Delivery_Select(ddlDeviveryNo);
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

 #region Button  SAVE/UPDATE Click

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            AmcInvoiceSave();
        }
        else if (btnSave.Text == "Update")
        {
            AmcInvoiceUpdate();
        }
    }

    #endregion

    #region AmcInvoiceSave
    private void AmcInvoiceSave()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                Services.AMCWorkOrder objsr = new Services.AMCWorkOrder();

                //if (objsr.AMCOrder_isrecordexists(ddlSalesOrderNo.SelectedItem.Value) > 0)
                //{
                //    MessageBox.Show(this, "invoice for " + ddlSalesOrderNo.SelectedItem.Text + " already prepared");
                //    Yantra.Classes.General.ClearControls(this);
                //    return;
                //}
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                Services.AmcInvoice objInventory = new Services.AmcInvoice();
                Services.BeginTransaction();

                objInventory.AMCINo = txtInvoiceNo.Text;
                objInventory.AMCIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objInventory.AMCOId = ddlSalesOrderNo.SelectedItem.Value;
                objInventory.AMCIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.AMCIApprovedBy = ddlApprovedBy.SelectedItem.Value;

                objInventory.AMCIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.AMCIMissChrgs = txtMiscelleneous.Text;
                objInventory.AMCIDiscount = txtDiscount.Text;
                objInventory.AMCIGrossAmt = txtGrossAmount.Text;
                objInventory.AMCIRemarks = txtRemarks.Text;


                if (objInventory.AmcInvoice_Save() == "Data Saved Successfully")
                {
                    objInventory.AmcInvoiceDetails_Delete(objInventory.AMCIId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.AMCIDetQty = gvrow.Cells[6].Text;
                        objInventory.AMCIDetRate = gvrow.Cells[7].Text;
                        objInventory.AMCIDetVat = gvrow.Cells[8].Text;
                        objInventory.AMCIDetCst = gvrow.Cells[9].Text;
                        objInventory.AMCIDetExcise = gvrow.Cells[10].Text;
                        objInventory.AmcInvoiceDetails_Save();
                    }
                    Services.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    Services.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

               // gvSalesInvoiceDetails.DataBind();
                gvItemDetails.DataBind();
                gvItmDetails.DataBind();
                tblSIDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region AmcInvoiceUpdate
    private void AmcInvoiceUpdate()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                Services.AmcInvoice objInventory = new Services.AmcInvoice();
                Services.BeginTransaction();
                objInventory.AMCIId = invoiceNo;
                objInventory.AMCINo = txtInvoiceNo.Text;
                objInventory.AMCIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objInventory.AMCOId = ddlSalesOrderNo.SelectedItem.Value;
                objInventory.AMCIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.AMCIApprovedBy = ddlApprovedBy.SelectedItem.Value;
                //objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                objInventory.AMCIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.AMCIMissChrgs = txtMiscelleneous.Text;
                objInventory.AMCIDiscount = txtDiscount.Text;
                objInventory.AMCIGrossAmt = txtGrossAmount.Text;
                objInventory.AMCIRemarks = txtRemarks.Text;


                if (objInventory.AmcInvoice_Update() == "Data Updated Successfully")
                {
                    objInventory.AmcInvoiceDetails_Delete(objInventory.AMCIId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.AMCIDetQty = gvrow.Cells[6].Text;
                        objInventory.AMCIDetRate = gvrow.Cells[7].Text;
                        objInventory.AMCIDetVat = gvrow.Cells[8].Text;
                        objInventory.AMCIDetCst = gvrow.Cells[9].Text;
                        objInventory.AMCIDetExcise = gvrow.Cells[10].Text;

                        objInventory.AmcInvoiceDetails_Save();
                    }
                    Services.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    Services.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";

               // gvSalesInvoiceDetails.DataBind();
                gvItemDetails.DataBind();
                gvItmDetails.DataBind();
                tblSIDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Sales Order");
        }
    }
    #endregion

  #region AMC ORDER No Selected Index Changed
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ItemTypes_Fill();
            Services.AMCOrder objSM = new Services.AMCOrder();
            if (objSM.AMCOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
            {
                txtSalesOrderDate.Text = objSM.AMCODate;

                objSM.AMCOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;

                }
            }
            else
            {
                MessageBox.Show(this, "SalesOrder " + ddlSalesOrderNo.SelectedItem.Text + " is deleted so invoice cannot be done");
                Yantra.Classes.General.ClearControls(this);
                return;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            Services.Dispose();
            if (txtInvoiceNo.Text == String.Empty)
            {
                txtInvoiceNo.Text = Services.AmcInvoice.AmcInvoice_AutoGenCode();
            }
          
        }

    }
    #endregion

    #region GridView Item Details Row Databound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = (Convert.ToInt32(e.Row.Cells[4].Text) * Convert.ToInt32(e.Row.Cells[5].Text)).ToString();
        }

        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[12].Visible = false;
        //}




    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("AMCInvoice.aspx");
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtExcise.Text == "") { txtExcise.Text = "0"; }
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
        col = new DataColumn("ItemTypeId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
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
                        dr["ItemCode"] = ddlItemName.SelectedItem.Value;
                        dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["ItemName"] = ddlItemName.SelectedItem.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtQuantity.Text;
                        dr["Rate"] = txtRate.Text;
                        if (rbVAT.Checked == true)
                        {
                            dr["VAT"] = txtVAT.Text;
                            dr["Cst"] = "0";
                        }
                        else if (rbCST.Checked == true)
                        {
                            dr["VAT"] = "0";
                            dr["Cst"] = txtCST.Text;
                        }
                        dr["Excise"] = txtExcise.Text;
                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["VAT"] = gvrow.Cells[8].Text;
                        dr["Cst"] = gvrow.Cells[9].Text;
                        dr["Excise"] = gvrow.Cells[10].Text;
                        dr["ItemTypeId"] = gvrow.Cells[12].Text;
                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["ItemTypeId"] = gvrow.Cells[12].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }

        if (gvItmDetails.Rows.Count > 0)
        {
            if (gvItmDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItmDetails.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlItemName.SelectedItem.Value)
                    {
                        gvItmDetails.DataSource = SalesInvoiceProducts;
                        gvItmDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }
        if (gvItmDetails.SelectedIndex == -1)
        {
            DataRow drnew = SalesInvoiceProducts.NewRow();
            drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = ddlItemName.SelectedItem.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQuantity.Text;
            drnew["Rate"] = txtRate.Text;
            if (rbVAT.Checked == true)
            {
                drnew["VAT"] = txtVAT.Text;
                drnew["Cst"] = "0";
            }
            else if (rbCST.Checked == true)
            {
                drnew["VAT"] = "0";
                drnew["Cst"] = txtCST.Text;
            }
            drnew["Excise"] = txtExcise.Text;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            SalesInvoiceProducts.Rows.Add(drnew);
        }

        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
        gvItmDetails.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        ddlItemName.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtVAT.Text = string.Empty;
        txtCST.Text = string.Empty;
        txtExcise.Text = string.Empty;
        txtAmount.Text = string.Empty;
        gvItmDetails.SelectedIndex = -1;
    }
    #endregion

    #region ItemName Select Index Changed
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemSpec.Text = objMaster.ItemSpec;
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

    #region GridView ItmDetails Items Row DataBound
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
            e.Row.Cells[12].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text) + ((Convert.ToDecimal(e.Row.Cells[8].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100) + ((Convert.ToDecimal(e.Row.Cells[9].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100) + ((Convert.ToDecimal(e.Row.Cells[10].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtGrossTotalAmtHidden.Value = txtGrossAmount.Text = GrossAmountCalc().ToString();
        }
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
    #endregion

    #region GridView gItm Items Row Deleting
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
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        SalesInvoiceProducts.Columns.Add(col);

        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["ItemTyeId"] = gvrow.Cells[12].Text;

                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    #endregion

  #region GridView sales Details Row DataBound
    protected void gvSalesInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

#region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (invoiceNo!=null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=amcinvoice&amciid=" + invoiceNo + "";
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

    #region ddlDeviveryNo_SelectedIndexChanged
    protected void ddlDeviveryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Inventory.Delivery objDelivery = new Inventory.Delivery();
            if (objDelivery.Delivery_Select(ddlDeviveryNo.SelectedItem.Value) > 0)
            {
                txtChallanDate.Text = objDelivery.DCDate;
                ddlSalesOrderNo.SelectedValue = objDelivery.SOId;
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

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

#region gvItmDetails_RowEditing
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
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
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
            dr["Excise"] = gvrow.Cells[10].Text;
            dr["ItemTypeId"] = gvrow.Cells[12].Text;
            dr["ItemType"] = gvrow.Cells[3].Text;
            SalesInvoiceProducts.Rows.Add(dr);
            if (gvrow.RowIndex == gvItmDetails.Rows[e.NewEditIndex].RowIndex)
            {
                ddlItemType.SelectedValue = gvrow.Cells[12].Text;
                ItemName_Fill();
                ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                ddlItemName_SelectedIndexChanged(sender, e);
                txtItemUOM.Text = gvrow.Cells[5].Text;
                txtQuantity.Text = gvrow.Cells[6].Text;
                txtRate.Text = gvrow.Cells[7].Text;
                txtVAT.Text = gvrow.Cells[8].Text;
                txtCST.Text = gvrow.Cells[9].Text;
                txtExcise.Text = gvrow.Cells[10].Text;
                if (txtVAT.Text != "0")
                {
                    rbVAT.Checked = true;
                    rbCST.Checked = false;
                }
                else if (txtCST.Text != "0")
                {
                    rbVAT.Checked = false;
                    rbCST.Checked = true;
                }
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text) + ((Convert.ToDecimal(txtVAT.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100) + ((Convert.ToDecimal(txtCST.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100) + ((Convert.ToDecimal(txtExcise.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100));
                gvItmDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    #endregion

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AmcInvoice objSI = new Services.AmcInvoice();
            Services.BeginTransaction();
            objSI.AMCIId = invoiceNo;
            objSI.AMCIApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSI.AMCInvoiceApprove_Update();
            Services.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvSalesInvoiceDetails.DataBind();
            //btnEdit_Click(sender, e);
        }
    }
}
 
