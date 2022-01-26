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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modules_Masters_Item_Purchase_Price_History : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
            setControlsVisibility();
            BindHistoryGrid();
        }
    }

    private void BindHistoryGrid()
    {
        if(txtModelNo.Text !="")
        {
            SqlCommand cmd = new SqlCommand("USP_ItemPurchasePriceHistory2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvItemPriceHistory.DataSource = dt;
            gvItemPriceHistory.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("USP_ItemPurchasePriceHistory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvItemPriceHistory.DataSource = dt;
            gvItemPriceHistory.DataBind();
        }        
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "108");
        btnBrandUpdate.Enabled = up.Update;
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

            if (txtAmount.Text != "" && txtPercentage.Text == "")
            {
                if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc5(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlModelNo.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc4(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc3(ddlBrand.SelectedValue, ddlCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }

                else if (int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtAmount.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchaseModel_RateCalc(ddlModelNo.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                MessageBox.Show(this, "Item Rate's Updated Sucessfully");

            }
            else if (txtAmount.Text == "" && txtPercentage.Text != "")
            {
                txtAmount.Text = "0";

                if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc5(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, ddlModelNo.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc4(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc3(ddlBrand.SelectedValue, ddlCategory.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchase_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }

                else if (int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtPercentage.Text != "")
                {
                    Masters.ItemPurchase.ItemPurchaseModel_RateCalc(ddlModelNo.SelectedValue, txtPercentage.Text, Convert.ToDecimal(txtAmount.Text));
                }
                MessageBox.Show(this, "Item Rate's Updated Sucessfully");

            }

            else
            {
                MessageBox.Show(this, "Please Provide Valid Data");
            }

            BindHistoryGrid();

        }
        catch (Exception ex)
        {
            //Masters.ItemPurchase.ItemPurchase_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text);
            MessageBox.Show(this, ex.Message);

        }
        finally
        {
            Masters.Dispose();
            Masters.ClearControls(this);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindHistoryGrid();
    }
    protected void gvItemPriceHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindHistoryGrid();
        gvItemPriceHistory.PageIndex = e.NewPageIndex;
        gvItemPriceHistory.DataBind();

    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvItemPriceHistory.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
        BindHistoryGrid();
    }

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
    }
    protected void gvItemPriceHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          
            e.Row.Cells[0].Text = "Page " + (gvItemPriceHistory.PageIndex + 1) + " of " + gvItemPriceHistory.PageCount;
        }
    }
}   
 
