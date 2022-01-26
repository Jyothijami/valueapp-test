using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Modules_Masters_ItemPriceUpdate : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();

            //gvItemPriceUpdate.DataBind();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand1);
            //Masters.ItemCategory.ItemCategory_Select(ddlCategory);
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "109");
        btnUpdate.Enabled = up.Update;
    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        //lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        //lblSearchValueHidden.Text = txtSearchText.Text;
        gvItemPriceUpdate.DataBind();
    }





    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Masters.ItemPurchase obj = new Masters.ItemPurchase();
        foreach (GridViewRow gvrow in gvItemPriceUpdate.Rows)
        {
            TextBox t1 = gvrow.FindControl("txtRSP") as TextBox;
            TextBox t2 = gvrow.FindControl("txtMRP") as TextBox;
            TextBox t3 = gvrow.FindControl("txtGross") as TextBox;
            TextBox t4 = gvrow.FindControl("txtCoefficient") as TextBox;
            TextBox t5 = gvrow.FindControl("txtMulFactor") as TextBox;
            DropDownList ddlCurrency = gvrow.FindControl("ddlCurrency") as DropDownList;
            string ip = "0";
            if (t1.Text != "" & t2.Text != "" & t3.Text != "" & t4.Text != "" & t5.Text != "")
            {

                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    obj.itemcode = gvrow.Cells[0].Text;

                    obj.ItemPriceUpdate(int.Parse(gvrow.Cells[0].Text), decimal.Parse(t1.Text), decimal.Parse(t2.Text), decimal.Parse(ip), int.Parse(gvrow.Cells[2].Text), int.Parse(gvrow.Cells[1].Text), int.Parse(gvrow.Cells[11].Text),int.Parse(ddlCurrency.SelectedItem.Value), decimal.Parse(t3.Text),decimal.Parse(t4.Text),decimal.Parse(t5.Text));
                    MessageBox.Show(this, "Prices Updated Sucessfully");
                }
            }
        }
        //gvItemPriceUpdate.DataBind();
        BindPriceGrid();

    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtSearchText.Text = "";
    }

    protected void gvItemPriceUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hf = (HiddenField)e.Row.FindControl("cthf1");

            DropDownList ddlcurency = (DropDownList)e.Row.FindControl("ddlCurrency");
            SM.DDLBindWithSelect(ddlcurency, "SELECT CURRENCY_ID,CURRENCY_NAME FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null", hf.Value);
        }
    }

    //protected void btnBrandUpdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && int.Parse(ddlModelNo.SelectedItem.Value) > 0 && txtPercentage.Text != "") 
    //        {
    //            Masters.ItemPurchase.ItemBrand_RateCalc5(ddlBrand.SelectedValue,ddlCategory.SelectedValue,ddlSubCategory.SelectedValue,ddlModelNo.SelectedValue, txtPercentage.Text);
    //        }
    //        else if(int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0 && int.Parse(ddlSubCategory.SelectedValue) > 0 && txtPercentage.Text != "")
    //        {
    //            Masters.ItemPurchase.ItemBrand_RateCalc4(ddlBrand.SelectedValue, ddlCategory.SelectedValue, ddlSubCategory.SelectedValue, txtPercentage.Text);
    //        }
    //        else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && int.Parse(ddlCategory.SelectedItem.Value) > 0  && txtPercentage.Text != "")
    //        {
    //            //Masters.ItemPurchase.ItemBrand_RateCalc3(ddlBrand.SelectedValue, ddlCategory.SelectedValue, txtPercentage.Text);
    //        }
    //        else if (int.Parse(ddlBrand.SelectedItem.Value) > 0 && txtPercentage.Text != "")
    //        {
    //           // Masters.ItemPurchase.ItemBrand_RateCalc(ddlBrand.SelectedValue,txtPercentage.Text);
    //        }
            
    //        MessageBox.Show(this, "Item Rate's Updated Sucessfully");
          
    //    }
    //    catch (Exception ex)
    //    {
    //        //Masters.ItemPurchase.ItemBrand_RateCalc(ddlBrand.SelectedValue, txtPercentage.Text);
    //        MessageBox.Show(this, ex.Message);

    //    }
    //    finally
    //    {   
    //        Masters.Dispose();
    //        Masters.ClearControls(this);
    //    }
    //}

    //protected void btnUpdateCategory_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.BeginTransaction();
    //        Masters.ItemPurchase.ItemCategory_RateCalc(ddlCategory.SelectedValue, txtPercentage.Text);
    //        MessageBox.Show(this, "Item Rate's Updated Sucessfully");
    //        Masters.CommitTransaction();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //        Masters.RollBackTransaction();

    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //        Masters.ClearControls(this);
    //    }
    //}


    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemCategory.ItemCategory_SelectForPrint(ddlSubCategory, ddlBrand.SelectedItem.Value, ddlCategory.SelectedItem.Value);
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo, "select YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_ITEM_MAST,YANTRA_ITEM_PRICE  where YANTRA_ITEM_PRICE.SubCat_Id = YANTRA_ITEM_MAST.IT_TYPE_ID and YANTRA_ITEM_PRICE.SubCat_Id = '" + ddlSubCategory.SelectedItem.Value + "'  ");
    }

    protected void ddlBrand1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemCategory.ItemCategory_Select_WithBrand(ddlCategory1, ddlBrand1.SelectedItem.Value);

    }
    protected void ddlCategory1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemCategory.ItemCategory_SelectForPrint(ddlSubCategory1, ddlBrand1.SelectedItem.Value, ddlCategory1.SelectedItem.Value);

    }
    protected void ddlSubCategory1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo1, "select DISTINCT YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_ITEM_MAST,YANTRA_ITEM_PRICE where YANTRA_ITEM_PRICE.SubCat_Id = YANTRA_ITEM_MAST.IT_TYPE_ID and YANTRA_ITEM_PRICE.Brand_id=YANTRA_ITEM_MAST.BRAND_ID " +
"and YANTRA_ITEM_MAST.IC_ID=YANTRA_ITEM_PRICE.Cat_Id and YANTRA_ITEM_PRICE.SubCat_Id = '" + ddlSubCategory1.SelectedItem.Value + "' and YANTRA_ITEM_PRICE.Brand_id='" + ddlBrand1.SelectedItem.Value + "' and YANTRA_ITEM_PRICE.Cat_Id='" + ddlCategory1.SelectedItem.Value + "' ");


    }
    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlModelNo1.DataSourceID = "SqlDataSource2";
        ddlModelNo1.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo1.DataValueField = "ITEM_CODE";
        ddlModelNo1.DataBind();

        ddlSubCategory1.SelectedIndex = -1;
        ddlCategory1.SelectedIndex = -1;
        ddlBrand1.SelectedIndex = -1;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindPriceGrid();
    }

    private void BindPriceGrid()
    {
        if (txtSearchModel.Text !="")
        {
            SqlCommand cmd = new SqlCommand("SP_MASTER_ITEMPRICE_MODIFY_SELECT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlBrand1.SelectedIndex > 0 )
            {
                cmd.Parameters.AddWithValue("@Brand", ddlBrand1.SelectedItem.Value);
            }

            if (ddlCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Category", ddlCategory1.SelectedItem.Value);
            }

            if (ddlSubCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@SubCat", ddlSubCategory1.SelectedItem.Value);
            }

            if (ddlModelNo1.SelectedIndex > -1 )
            {
                cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo1.SelectedItem.Value);
            }



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvItemPriceUpdate.DataSource = dt;
            gvItemPriceUpdate.DataBind();
        }
        else
        {
            //MessageBox.Show(this, "Please Search By selecting any one field atleast");
            SqlCommand cmd = new SqlCommand("SP_MASTER_ITEMPRICE_MODIFY_SELECT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlBrand1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Brand", ddlBrand1.SelectedItem.Value);
            }

            if (ddlCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Category", ddlCategory1.SelectedItem.Value);
            }

            if (ddlSubCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@SubCat", ddlSubCategory1.SelectedItem.Value);
            }

            if (ddlModelNo1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo1.SelectedItem.Value);
            }



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvItemPriceUpdate.DataSource = dt;
            gvItemPriceUpdate.DataBind();
        }
    }
    protected void gvItemPriceUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemPriceUpdate.PageIndex = e.NewPageIndex;
        BindPriceGrid();

    }
} 
 
