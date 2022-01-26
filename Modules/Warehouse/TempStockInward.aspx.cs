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
using Yantra.Classes;
using vllib;
public partial class Modules_Warehouse_TempStockInward : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            //Masters.ItemMaster.Stockentry12(ddlgodown, lblCPID.Text);
        }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster5_Select(ddlItemName, ddlBrand.SelectedItem.Value);

    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemName.DataSourceID = "SqlDataSource2";
        ddlItemName.DataTextField = "ITEM_MODEL_NO";
        ddlItemName.DataValueField = "ITEM_CODE";
        ddlItemName.DataBind();
        ddlItemName_SelectedIndexChanged(sender, e);
    }
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                //txtColor.Text = objMaster.Color;
                txtManufacturer.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                //txtRate.Text = objMaster.ItemRate;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlItemName.SelectedItem.Value);
            Masters.ItemPurchase obj = new Masters.ItemPurchase();
            if (obj.ItemPrice_Ddl(ddlItemName.SelectedItem.Value) > 0)
            {
                txtRate.Text = obj.rsp;
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text == "")
        {
            txtRemarks.Text = "0";
        }

        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Godown");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("NetQty");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ReQuantity");
        PurchaseInvoiceProducts.Columns.Add(col);
         col = new DataColumn("Godownid");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Color");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Colorid");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("RefNo");
        PurchaseInvoiceProducts.Columns.Add(col);
    
        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = PurchaseInvoiceProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["Godown"] = gvrow.Cells[5].Text;
                dr["NetQty"] = gvrow.Cells[6].Text;
                dr["ReQuantity"] = gvrow.Cells[7].Text;
                dr["Godownid"] = gvrow.Cells[8].Text;
                dr["Remarks"] = gvrow.Cells[9].Text;
                dr["Color"] = gvrow.Cells[10].Text;
                dr["Colorid"] = gvrow.Cells[11].Text;
                dr["RefNo"] = gvrow.Cells[12].Text;
                PurchaseInvoiceProducts.Rows.Add(dr);
            }
        }

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.Cells[2].Text == ddlItemName.SelectedItem.Value)
                {
                    gvItemDetails.DataSource = PurchaseInvoiceProducts;
                    gvItemDetails.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = PurchaseInvoiceProducts.NewRow();
        drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
        drnew["ItemType"] = ddlItemName.SelectedItem.Text;
        drnew["ItemName"] = txtItemName.Text;
        drnew["Godown"] = ddlgodown.SelectedItem.Text;
        if (txtAcceptedqty.Text == "")
        {
            drnew["NetQty"] = 0;
        }
        else
        {
            drnew["NetQty"] = txtAcceptedqty.Text;
        }
        if (txtRejectedqty.Text=="")
        {
            drnew["ReQuantity"] = 0;
        }
        else
        {
            drnew["ReQuantity"] = txtRejectedqty.Text;
        }
        drnew["Godownid"] = ddlgodown.SelectedItem.Value;
        drnew["Remarks"] = txtRemarks.Text;
        drnew["Color"] = ddlcolor.SelectedItem.Text;
        drnew["Colorid"] = ddlcolor.SelectedItem.Value;
        drnew["RefNo"] = txtReferenceNo.Text;

        PurchaseInvoiceProducts.Rows.Add(drnew);

        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);

    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemCategory.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtManufacturer.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtRate.Text = string.Empty;
        ddlItemName.SelectedItem.Value = "";
        txtSearchModel.Text = string.Empty;
        txtAcceptedqty.Text = string.Empty;
        //txtRejectedqty.Text = string.Empty;
        //txtRemarks.Text = string.Empty;
       // txtReferenceNo.Text = string.Empty;
        ddlItemName.DataSource=null;
        ddlItemName.DataBind();
        ddlcolor.SelectedIndex = 0;
        ddlItemName.Items.Clear();
    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            //e.Row.Cells[8].Visible = false;
            e.Row.Cells[11].Visible = false; 
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[2].Text;
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Godown");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("NetQty");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ReQuantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Godownid");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Color");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Colorid");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("RefNo");
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
                    dr["Godown"] = gvrow.Cells[5].Text;
                    dr["NetQty"] = gvrow.Cells[6].Text;
                    dr["ReQuantity"] = gvrow.Cells[7].Text;
                    dr["Godownid"] = gvrow.Cells[8].Text;
                    dr["Remarks"] = gvrow.Cells[9].Text;
                    dr["Color"] = gvrow.Cells[10].Text;
                    dr["Colorid"] = gvrow.Cells[11].Text;
                    dr["RefNo"] = gvrow.Cells[12].Text;
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
    }
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        try
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                string ItemCat = "";
                string ItemSubCat = "";
                Masters.ItemMaster objMaster = new Masters.ItemMaster();
                if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                {
                    ItemCat = objMaster.ItemCategoryName;
                    ItemSubCat = objMaster.ItemType;
                }

                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                obj.RefNo = gvrow.Cells[12].Text;
                obj.ItemCode = gvrow.Cells[2].Text;
                obj.ItemCategory = ItemCat;
                obj.ItemSubCategory = ItemSubCat;
                obj.ColorId = gvrow.Cells[11].Text;
                obj.qty = gvrow.Cells[6].Text;
                obj.BalanceQty = gvrow.Cells[6].Text;
                obj.DamageQty = gvrow.Cells[7].Text;
                obj.CpId = cp.getPresentCompanySessionValue();
                obj.InwardType = "OS";
                obj.DateAdded = DateTime.Now.ToString();
                obj.ItemLoc = gvrow.Cells[8].Text;
                obj.Cust_id = "0";
                obj.DeliveryDate = DateTime.Now.ToString();
                obj.Remarks = gvrow.Cells[9].Text;
                obj.InwardTemp_Save();
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
           // SCM.ClearControls(this);
            gvItemDetails.DataSource = null;
            gvItemDetails.DataBind();
            btnSave.Enabled = true;
        }
    }
}
 
