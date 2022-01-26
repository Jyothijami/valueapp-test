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

public partial class Modules_SCM_PurchaseReturn : System.Web.UI.Page
{

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - Convert.ToDecimal(txtDiscount.Text));

        #endregion
        if (!IsPostBack)
        {
            txtPackingCharges.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtTranportationCharges.Attributes.Add("onkeyup", "javascript:grosscalc();");
            rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtVAT.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtCST.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtExcise.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
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

            SupplierFixedPO_Fill();
            SupplierName_Fill();

            ItemTypes_Fill();
            EmployeeMaster_Fill();
            btnPrint.Visible = false;
        }
    }
    #endregion

    #region New Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        txtPurchaseNo.Text = SCM.PurchaseReturn.PurchaseReturn_AutoGenCode();
        txtPurchaseReturnDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        tblPIDetails.Visible = true;
        gvItemDetails.DataBind();
        gvItDetails.DataBind();
        gvInvoiceDetails.SelectedIndex = -1;
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - Convert.ToDecimal(txtDiscount.Text));


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
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvInvoiceDetails.SelectedRow.Cells[8].Text) && gvInvoiceDetails.SelectedRow.Cells[8].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnPrint.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnPrint.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnPrint.Visible = false;
        }

    }
    #endregion

    #region Edit
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.PurchaseReturn objSCM = new SCM.PurchaseReturn();

                if (objSCM.PurchaseReturn_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblPIDetails.Visible = true;
                    txtPurchaseNo.Text = objSCM.PRNo;
                    txtPurchaseReturnDate.Text = objSCM.PRDate;
                    ddlPONo.SelectedValue = objSCM.FPOId;
                 
                    ddlSupplierName.SelectedValue = objSCM.SUPId;

                    txtPackingCharges.Text = objSCM.PRPackChrgs;
                    txtTranportationCharges.Text = objSCM.PRTransChrgs;
                    txtInsurance.Text = objSCM.PRInsuranceChrgs;
                    txtMiscelleneous.Text = objSCM.PRMiscChrgs;
                    txtDiscount.Text = objSCM.PRDiscount;
                    txtGrossAmount.Text = objSCM.PRGrossAmt;
                    
                    
                    txtRemarks.Text = objSCM.PRRemarks;
                    ddlPreparedBy.SelectedValue = objSCM.PRPreparedBy;
                    ddlApprovedBy.SelectedValue = objSCM.PRApprovedBy;
                    
                    
                    
                  
                  
                    objSCM.PurchaseReturnDetails_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text, gvItemDetails);
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
                ddlPONo_SelectedIndexChanged(sender, e);

            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.PurchaseReturn objSCM = new SCM.PurchaseReturn();
                MessageBox.Show(this, objSCM.PurchaseReturn_Delete(gvInvoiceDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvInvoiceDetails.DataBind();
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

    #region DDlPoNo Select Change
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierFixedPO objSCMPO = new SCM.SupplierFixedPO();
            if (objSCMPO.SuppliersFixedPO_Select(ddlPONo.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objSCMPO.FPODate;

                objSCMPO.SuppliersFixedPODetails_Select(objSCMPO.FPOId, gvItDetails);

                SCM.SuppliersMaster objSCMSM = new SCM.SuppliersMaster();
                if (objSCMSM.SuppliersMaster_Select(objSCMPO.SupId) > 0)
                {
                    ddlSupplierName.SelectedValue = objSCMSM.SupId;
                    txtSupplierName.Text = objSCMSM.SupName;
                    txtAddress.Text = objSCMSM.SupAddress;
                    txtEmail.Text = objSCMSM.SupEmail;
                    txtContactPerson.Text = objSCMSM.SupContactPerson;
                    txtPhoneNo.Text = objSCMSM.SupPhone;
                    txtMobileNo.Text = objSCMSM.SupMobile;
                }
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

    #region Gvitem Details Row DataBound
    protected void gvItDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
           
            e.Row.Cells[11].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[5].Text) * Convert.ToDecimal(e.Row.Cells[6].Text));
            //   e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToShortDateString();
        }
    }
    #endregion

    #region Gvitem Details Row Editing    
    protected void gvItDetails_RowEditing(object sender, GridViewEditEventArgs e)
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

        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);


        if (gvItDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItDetails.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemType"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["UOM"] = gvrow.Cells[4].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[5].Text;

                dr["DeliveryDate"] = gvrow.Cells[8].Text;
                dr["Specifications"] = gvrow.Cells[9].Text;
                dr["Remarks"] = gvrow.Cells[10].Text;
                dr["ItemTypeId"] = gvrow.Cells[11].Text;



                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvItDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[1].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    //ItemName_Fill();
                    txtModelName.Text = gvrow.Cells[3].Text;
                    // ddlItemName_SelectedIndexChanged(sender, e);
                    //txtUOM.Text = gvrow.Cells[4].Text;
                    txtOrderedQuantity.Text = gvrow.Cells[6].Text;
                    txtRate.Text = gvrow.Cells[5].Text;



                    //  txtDeliveryD.Text = gvrow.Cells[8].Text;
                    //txtItemSpecifications.Text = gvrow.Cells[9].Text;
                    //txtItemRemarks.Text = gvrow.Cells[10].Text;
                    gvItDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvItDetails.DataSource = SalesOrderItems;
        gvItDetails.DataBind();
    }
    #endregion

    #region Ddl Item type Change
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
                txtUOM.Text = objMaster.ItemUOMShort;
                txtRate.Text = objMaster.ItemSeries;
                txtItemSpec.Text = objMaster.ItemSpec;
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

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtExcise.Text == "") { txtExcise.Text = "0"; }
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);


        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = PurchaseInvoiceProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["VAT"] = gvrow.Cells[8].Text;
                dr["Excise"] = gvrow.Cells[10].Text;
                dr["Cst"] = gvrow.Cells[9].Text;
                dr["Amount"] = gvrow.Cells[11].Text;
                dr["ItemTypeId"] = gvrow.Cells[12].Text;

                PurchaseInvoiceProducts.Rows.Add(dr);
            }
        }

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemType.SelectedItem.Value)
                {
                    gvItemDetails.DataSource = PurchaseInvoiceProducts;
                    gvItemDetails.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = PurchaseInvoiceProducts.NewRow();
        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
        drnew["ItemType"] = ddlItemType.SelectedItem.Text;
        drnew["ItemName"] = txtModelName.Text;
        drnew["UOM"] = txtUOM.Text;
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
        drnew["Amount"] = txtAmount.Text;

        PurchaseInvoiceProducts.Rows.Add(drnew);

        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Btn Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        txtUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtVAT.Text = string.Empty;
        txtCST.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtExcise.Text = string.Empty;
        txtModelName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtColor.Text = string.Empty;
        txtOrderedQuantity.Text = string.Empty;
    }
    #endregion

    #region Gvitem details Row dataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                e.Row.Cells[10].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
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
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }

    #endregion

    #region Gvitems Details Row Deleting
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["Amount"] = gvrow.Cells[11].Text;
                    dr["ItemTypeId"] = gvrow.Cells[12].Text;

                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Row Editing
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);


        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {

            DataRow dr = PurchaseInvoiceProducts.NewRow();
            dr["ItemCode"] = gvrow.Cells[2].Text;
            dr["ItemType"] = gvrow.Cells[3].Text;
            dr["ItemName"] = gvrow.Cells[4].Text;
            dr["UOM"] = gvrow.Cells[5].Text;
            dr["Quantity"] = gvrow.Cells[6].Text;
            dr["Rate"] = gvrow.Cells[7].Text;
            dr["VAT"] = gvrow.Cells[8].Text;
            dr["Cst"] = gvrow.Cells[9].Text;
            dr["Excise"] = gvrow.Cells[10].Text;
            dr["Amount"] = gvrow.Cells[11].Text;
            dr["ItemTypeId"] = gvrow.Cells[12].Text;

            PurchaseInvoiceProducts.Rows.Add(dr);

            if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
            {
                ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                ddlItemType_SelectedIndexChanged(sender, e);
            
                txtModelName.Text = gvrow.Cells[2].Text;
                txtUOM.Text = gvrow.Cells[5].Text;
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
                gvItemDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
    }
    #endregion

    #region Save & Update
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            PurchaseReturnSave();
        }
        else if (btnSave.Text == "Update")
        {
            PurchaseReturnUpdate();
        }
    }
    #endregion

    #region Approve Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.PurchaseReturn objSMSOApprove = new SCM.PurchaseReturn();
            SCM.BeginTransaction();
            objSMSOApprove.PRId = gvInvoiceDetails.SelectedRow.Cells[0].Text;
            objSMSOApprove.PRApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMSOApprove.PurchaseReturnApprove_Update();
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
            gvInvoiceDetails.DataBind();
            SCM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Refresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }
    #endregion

    #region Close
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblPIDetails.Visible = false;
    }
    #endregion

    #region Print Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PurchaseReturn&prno=" + gvInvoiceDetails.SelectedRow.Cells[0].Text + "";
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

    #region Supplier Name
    private void SupplierName_Fill()
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
            SCM.Dispose();
        }
    }
    #endregion
   
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

    #region Purchase Order Fill
    private void SupplierFixedPO_Fill()
    {
        try
        {
            SCM.SupplierFixedPO.SuppliersFixedPO_Select(ddlPONo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SCM.Dispose();
        }
    }
    #endregion

    #region  Purchase Return Save
    private void PurchaseReturnSave()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.PurchaseReturn objSCM = new SCM.PurchaseReturn();
                SCM.BeginTransaction();
                if (txtPackingCharges.Text == "") { txtPackingCharges.Text = "0"; }
                if (txtTranportationCharges.Text == "") { txtTranportationCharges.Text = "0"; }
                if (txtInsurance.Text == "") { txtInsurance.Text = "0"; }
                objSCM.PRNo = txtPurchaseNo.Text;
                objSCM.PRDate = Yantra.Classes.General.toMMDDYYYY(txtPurchaseReturnDate.Text);
                objSCM.FPOId = ddlPONo.SelectedItem.Value;
              
                objSCM.SUPId = ddlSupplierName.SelectedItem.Value;

                objSCM.PRPackChrgs = txtPackingCharges.Text;
                objSCM.PRTransChrgs = txtTranportationCharges.Text;
                objSCM.PRInsuranceChrgs = txtInsurance.Text;
                objSCM.PRMiscChrgs = txtMiscelleneous.Text;
                objSCM.PRDiscount = txtDiscount.Text;
                objSCM.PRGrossAmt = txtGrossAmount.Text;
                objSCM.PRRemarks = txtRemarks.Text;
                objSCM.PRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.PRApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSCM.CpId = cp.getPresentCompanySessionValue();
                if (objSCM.PurchaseReturn_Save() == "Data Saved Successfully")
                {
                    objSCM.PurchaseReturnDetails_Delete(objSCM.PRId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.PRItemCode = gvrow.Cells[2].Text;
                        objSCM.PRDetQty = gvrow.Cells[6].Text;
                        objSCM.PRDetRate = gvrow.Cells[7].Text;
                        objSCM.PRDetCst = gvrow.Cells[9].Text;
                        objSCM.PRDetVat = gvrow.Cells[8].Text;
                        objSCM.PRDetExcise = gvrow.Cells[10].Text;
                        objSCM.PRDetAmount = gvrow.Cells[11].Text;

                        objSCM.PurchaseReturnDetails_Save();
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
                gvInvoiceDetails.DataBind();
                gvItDetails.DataBind();
                gvItemDetails.DataBind();
                tblPIDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region PurchaseReturnUpdate
    private void PurchaseReturnUpdate()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.PurchaseReturn objSCM = new SCM.PurchaseReturn();
                SCM.BeginTransaction();
                objSCM.PRId = gvInvoiceDetails.SelectedRow.Cells[0].Text;
                objSCM.PRNo = txtPurchaseNo.Text;
                objSCM.PRDate = Yantra.Classes.General.toMMDDYYYY(txtPurchaseReturnDate.Text);
                objSCM.FPOId = ddlPONo.SelectedItem.Value;
                objSCM.SUPId = ddlSupplierName.SelectedItem.Value;
                objSCM.PRPackChrgs = txtPackingCharges.Text;
                objSCM.PRTransChrgs = txtTranportationCharges.Text;
                objSCM.PRInsuranceChrgs = txtInsurance.Text;
                objSCM.PRMiscChrgs = txtMiscelleneous.Text;
                objSCM.PRDiscount = txtDiscount.Text;
                objSCM.PRGrossAmt = txtGrossAmount.Text;
                objSCM.PRRemarks = txtRemarks.Text;
                objSCM.PRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.PRApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSCM.CpId = cp.getPresentCompanySessionValue();
                if (objSCM.PurchaseReturn_Update() == "Data Updated Successfully")
                {
                    objSCM.PurchaseReturnDetails_Delete(objSCM.PRId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.PRItemCode = gvrow.Cells[2].Text;
                        objSCM.PRDetQty = gvrow.Cells[6].Text;
                        objSCM.PRDetRate = gvrow.Cells[7].Text;
                        objSCM.PRDetCst = gvrow.Cells[9].Text;
                        objSCM.PRDetVat = gvrow.Cells[8].Text;
                        objSCM.PRDetExcise = gvrow.Cells[10].Text;
                        objSCM.PRDetAmount = gvrow.Cells[11].Text;

                        objSCM.PurchaseReturnDetails_Save();
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
                gvInvoiceDetails.DataBind();
                gvItDetails.DataBind();
                gvItemDetails.DataBind();
                tblPIDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Purchase Order");
        }
    }
    #endregion

    #region Row DataBound
    protected void gvInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
    }
    #endregion


    #region Linkbutton Main gv
    protected void lbtnPrNo_Click(object sender, EventArgs e)
    {
        tblPIDetails.Visible = false;
        LinkButton lbtnPrNo;
        lbtnPrNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPrNo.Parent.Parent;
        gvInvoiceDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        try
        {
            SCM.PurchaseReturn objSCM = new SCM.PurchaseReturn();

            if (objSCM.PurchaseReturn_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblPIDetails.Visible = true;
                txtPurchaseNo.Text = objSCM.PRNo;
                txtPurchaseReturnDate.Text = objSCM.PRDate;
                ddlPONo.SelectedValue = objSCM.FPOId;
              
                ddlSupplierName.SelectedValue = objSCM.SUPId;

                txtPackingCharges.Text = objSCM.PRPackChrgs;
                txtTranportationCharges.Text = objSCM.PRTransChrgs;
                txtInsurance.Text = objSCM.PRInsuranceChrgs;
                txtMiscelleneous.Text = objSCM.PRMiscChrgs;
                txtDiscount.Text = objSCM.PRDiscount;
                txtGrossAmount.Text = objSCM.PRGrossAmt;
              
                txtRemarks.Text = objSCM.PRRemarks;
                ddlPreparedBy.SelectedValue = objSCM.PRPreparedBy;
                ddlApprovedBy.SelectedValue = objSCM.PRApprovedBy;
                objSCM.PurchaseReturnDetails_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text, gvItemDetails);
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
            ddlPONo_SelectedIndexChanged(sender, e);

        }
    }
    #endregion
}

 
