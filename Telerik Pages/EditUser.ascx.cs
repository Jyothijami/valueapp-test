using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Telerik_Pages_EditUser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RateType_Fill();

        }
    }
    #region Rate Type Fill
    private void RateType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlRate);
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

    #region Rdb All change
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        //for clearing the fields when click on this radio button
        ddlModelNo.ClearSelection();
        
        txtSearchModel.Enabled  = true;
        btnSearchModelNo.Enabled  = true;
        ddlBrand.Enabled  = false;
        ddlModelNo.DataSource = null;
        ddlModelNo.DataBind();
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
    }
    #endregion

    #region RDB OnlyFrom Lead
    protected void rdbOnlyfromLead_CheckedChanged(object sender, EventArgs e)
    {
       
        if (rdbOnlyfromLead.Checked == true)
        {
            ItemTypes_Fill();
            txtSearchModel.Enabled  = false;
            btnSearchModelNo.Enabled  = false;
            //lblSearchBrand.Visible = false;
            //ddlBrand.Visible = false;
            ddlBrand.Enabled  = true;
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);



        }
    }

    private void ItemTypes_Fill()
    {
        try
        {
            //SM.SalesOrder.SalesQuatation_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo);
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
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }

    #region DDL model No change

    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            string itemcode = "";
            if (rdbOnlyfromLead.Checked == true)
            {
                itemcode = getItemCode(ddlModelNo.SelectedItem.Value);
                lblItemCode.Text = itemcode;
            }
            else if (rdbAll.Checked == true)
            {
                itemcode = ddlModelNo.SelectedValue;
                lblItemCode.Text = itemcode;
            }

            if (objMaster.ItemMaster_Select(itemcode) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
                Image2.ImageUrl = "~/Content/ItemDrawings/" + objMaster.itemSpecifcation;
                txtRoom.Text = string.Empty;
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(itemcode) > 0)
                {
                    txtRate.Text = objrate.rsp;
                }
                

                txtGST_Perc.Text = objMaster.GST_Tax;
                if (txtGST_Perc.Text == "" || txtGST_Perc.Text == null)
                {
                    txtGST_Perc.Text = "0";
                }

                if (txtSpPrice.Text == "" || txtSpPrice.Text == null)
                {
                    txtSpPrice.Text = "0";
                }

                txtGST_Amt.Text = ((Convert.ToDecimal(txtGST_Perc.Text) * Convert.ToDecimal(txtSpPrice.Text)) / 100).ToString();
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, itemcode);

            Types_Fill();
            ddlEssentials.Enabled = true;
            Masters.ItemMaster.CheckboxListWithStatement(chklitemcolor, "SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),[YANTRA_ITEM_MAST].ITEM_MODEL_NO FROM [YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST]  WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE  = [YANTRA_ITEM_MAST].ITEM_CODE  and [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=" + itemcode + "");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //  Masters.Dispose();
        }
    }
    #region  Types Fill
    private void Types_Fill()
    {
        try
        {
            //Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
            SM.SalesEnquiry.SalesEnquiryItemTypes2_Select(ddlModelNo.SelectedItem.Value, ddlEssentials);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();            
            //  SM.Dispose();
        }
    }
    #endregion
    private string getItemCode(string ENQ_DET_ID)
    {
        string itemcode;

        itemcode = dbc.get_column_data("ITEM_CODE", "ENQ_DET_ID", Convert.ToInt32(ENQ_DET_ID), "YANTRA_ENQ_DET");

        return itemcode;
    }
    #endregion
}