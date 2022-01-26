using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;

public partial class Modules_Masters_Item_Price_History : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemCategory.ItemCategory_SelectForPrint(ddlSubCategory, ddlBrand.SelectedItem.Value, ddlCategory.SelectedItem.Value);
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo, "select DISTINCT YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_ITEM_MAST,YANTRA_ITEM_PRICE  where YANTRA_ITEM_PRICE.SubCat_Id = YANTRA_ITEM_MAST.IT_TYPE_ID and YANTRA_ITEM_PRICE.SubCat_Id = '" + ddlSubCategory.SelectedItem.Value + "'  ");
    }
    protected void btnBrandUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            if(txtAmount.Text != "" && txtPercentage.Text == "")
            {
                if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc5(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlModelNo.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc4(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc3(ddlBrand.SelectedValue, ddlCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                MessageBox.Show(this, "Item Rate's Updated Sucessfully");

            }
            else if (txtAmount.Text == "" && txtPercentage.Text != "")
            {
                string str ="0";

                if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc5(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlModelNo.SelectedValue, txtPercentage.Text, Convert.ToDecimal(str));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc4(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc3(ddlBrand.SelectedValue, ddlCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemBrand_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                MessageBox.Show(this, "Item Rate's Updated Sucessfully");

            }

            else
            {
                MessageBox.Show(this, "Please Provide Either % or Amount ");
            }

            gvItemPriceHistory.DataBind();

        }
        catch (Exception ex)
        {
            //Masters.ItemPurchase.ItemBrand_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text);
            MessageBox.Show(this, ex.Message);

        }
        finally
        {
            Masters.Dispose();
            Masters.ClearControls(this);
        }
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemPriceHistory.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvItemPriceHistory.DataBind();
    }
    
}
 
