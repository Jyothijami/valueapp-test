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
using System.IO;
using Yantra.Classes;

public partial class Modules_Masters_ProductMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;  
    public string  ProductId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ItemTypes_Fill();
            ProductCompany_Fill();
        }
    }

    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region Link Button Click
    protected void lbtnItemMasterName_Click(object sender, EventArgs e)
    {
        tblPMDetails.Visible = false;
        LinkButton lbtnProductMaster;
        lbtnProductMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnProductMaster.Parent.Parent;
        gvProductMasterDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvProductMasterDetails.DataBind();
    }
    #endregion

    #region New Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblPMDetails.Visible = true;
        gvInterestedProducts.DataBind();
        gvProductMasterDetails.SelectedIndex = -1;
        //ScriptManagerLocal.SetFocus(txtProductName);
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
    }
    #endregion

    #region Save Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ProductMasterSave();
            tblPMDetails.Visible = false;
            Masters.ClearControls(this);
        }
        else if (btnSave.Text == "Update")
        {
            ProductMasterUpdate();
            tblPMDetails.Visible = false;
            Masters.ClearControls(this);
        }
        gvProductMasterDetails.SelectedIndex = -1;
    }
    #endregion

    #region ProductMasterSave
    private void ProductMasterSave()
    {
        try
        {
            Masters.ProductMasterDetails objMaster = new Masters.ProductMasterDetails();
            objMaster.Product_Code = txtProductCode.Text;
            objMaster.Product_Name = txtProductName.Text;
            objMaster.ReorderLevel = txtMinimum.Text;
            objMaster.Rate = txtRate.Text;
            objMaster.Image = "";
            objMaster.Product_Specification = txtProductSpecification.Text;
            objMaster.Product_Company = DropDownList1.SelectedItem.Value;
                       
            objMaster.ProductMasterDetails_Save();

           ProductId = objMaster.Product_Id;

            Masters.ProductDetails obj = new Masters.ProductDetails();
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                {
                    obj.Product_Id = ProductId;
                    obj.ItemCode = gvrow.Cells[2].Text;
                    obj.ProductDetails_Save();
                }
                
                MessageBox.Show(this, "Data Saved Successfully");

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvProductMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ProductMasterUpdate
    private void ProductMasterUpdate()
    {
        try
        {
            Masters.ProductMasterDetails objMaster = new Masters.ProductMasterDetails();
            objMaster.Product_Id = gvProductMasterDetails.SelectedRow.Cells[1].Text;
            objMaster.Product_Code = txtProductCode.Text;
            objMaster.Product_Name = txtProductName.Text;
            objMaster.ReorderLevel = txtMinimum.Text;
            objMaster.Rate = txtRate.Text;
            objMaster.Image = "";
            objMaster.Product_Specification = txtProductSpecification.Text;
            objMaster.Product_Company = DropDownList1.SelectedItem.Value;

            ProductId = gvProductMasterDetails.SelectedRow.Cells[1].Text;

            Masters.ProductDetails obj = new Masters.ProductDetails();
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                obj.Product_Id = ProductId;
                obj.ItemCode = gvrow.Cells[2].Text;
                obj.ProductDetails_Update();
            }
            
            MessageBox.Show(this, objMaster.ProductMasterDetails_Update());
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvProductMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region Grid View Row Databound
    protected void gvProductMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int count = General.CountofRecordsWithQuery("select count(*) from YANTRA_LKUP_PRODUCT_MASTER where PRODUCT_ID = " + Convert.ToInt16(e.Row.Cells[1].Text) + "");
            if (count > 0)
                (e.Row.FindControl("Image") as Image).ImageUrl = "~/Modules/Masters/ProductImage.ashx?id=" + e.Row.Cells[1].Text + "";
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
    }
    #endregion

    #region Edit Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvProductMasterDetails.SelectedIndex > -1)
        {
            tblPMDetails.Visible = true;

                txtProductCode.Text = gvProductMasterDetails.SelectedRow.Cells[2].Text;
                txtProductName.Text = gvProductMasterDetails.SelectedRow.Cells[0].Text;
                txtMinimum.Text = gvProductMasterDetails.SelectedRow.Cells[4].Text;
                txtRate.Text = gvProductMasterDetails.SelectedRow.Cells[5].Text;
                Image1.ImageUrl = "~/Modules/Masters/ProductImage.ashx?id=" + gvProductMasterDetails.SelectedRow.Cells[1].Text + "";
                txtProductSpecification.Text = gvProductMasterDetails.SelectedRow.Cells[7].Text;
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(gvProductMasterDetails.SelectedRow.Cells[9].Text));
              
               

                Masters.ProductDetails obj = new Masters.ProductDetails();
                obj.SalesEnquiryDetails_Select(gvProductMasterDetails.SelectedRow.Cells[1].Text, gvInterestedProducts);
            
            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Delete Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvProductMasterDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.ProductMasterDetails objMaster = new Masters.ProductMasterDetails();
                objMaster.Product_Id = gvProductMasterDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.ProductMasterDetails_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvProductMasterDetails.DataBind();
                gvProductMasterDetails.SelectedIndex = -1;

                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Close Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblPMDetails.Visible = false;
    }
    #endregion

    #region Refresh Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        gvInterestedProducts.DataBind();
        btnNew_Click(sender, e);
    }
    #endregion

    #region Ddl Searchby Selected Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ScriptManagerLocal.SetFocus(txtSearchText);
        
    }
    #endregion
    
    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
           Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
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

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemType.ItemType_Select(ddlItemType);
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

    #region Item Type Selected Index
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

    #region Item Name Selected Index
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
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

    #region Add Button Click
    protected void btnAddProductDetails_Click(object sender, EventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        InterestedProducts.Columns.Add(col);
        
        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvInterestedProducts.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvInterestedProducts.SelectedRow.RowIndex)
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["ItemCode"] = ddlItemName.SelectedItem.Value;
                        dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["ItemName"] = ddlItemName.SelectedItem.Text;
                        dr["Specifications"] = txtItemSpec.Text;
                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["Specifications"] = gvrow.Cells[5].Text;
                        dr["ItemTypeId"] = gvrow.Cells[6].Text;
                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["Specifications"] = gvrow.Cells[5].Text;
                    dr["ItemTypeId"] = gvrow.Cells[6].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gvInterestedProducts.Rows.Count > 0)
        {
            if (gvInterestedProducts.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlItemName.SelectedItem.Value)
                    {
                        gvInterestedProducts.DataSource = InterestedProducts;
                        gvInterestedProducts.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvInterestedProducts.SelectedIndex == -1)
        {
            DataRow drnew = InterestedProducts.NewRow();
            drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = ddlItemName.SelectedItem.Text;
            drnew["Specifications"] = txtItemSpec.Text;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            InterestedProducts.Rows.Add(drnew);
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
        gvInterestedProducts.SelectedIndex = -1;
        btnAddProductDetailsRefresh_Click(sender, e);
    }
    #endregion

    #region Add Products Refresh Button Click
    protected void btnAddProductDetailsRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        ddlItemName.SelectedValue = "0";
        txtItemSpec.Text = string.Empty;
        
        gvInterestedProducts.SelectedIndex = -1;
    }
    #endregion

    #region Temp Grid Row Databound
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
    }
    #endregion

    #region Temp Gird Row Deleting
    protected void gvInterestedProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvInterestedProducts.Rows[e.RowIndex].Cells[2].Text;
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        InterestedProducts.Columns.Add(col);
        
        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["Specifications"] = gvrow.Cells[5].Text;
                    dr["ItemTypeId"] = gvrow.Cells[6].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region Temp Grid Row Editing
    protected void gvInterestedProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        InterestedProducts.Columns.Add(col);

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                DataRow dr = InterestedProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["Specifications"] = gvrow.Cells[5].Text;
                dr["ItemTypeId"] = gvrow.Cells[6].Text;
                
                InterestedProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvInterestedProducts.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[6].Text;
                    ItemName_Fill();
                    ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    txtItemSpec.Text = gvrow.Cells[5].Text;
                    gvInterestedProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region ProductCompany Fill
    private void ProductCompany_Fill()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(DropDownList1);
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
   
}
