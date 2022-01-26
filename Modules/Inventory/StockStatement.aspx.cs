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
public partial class Modules_Inventory_StockStatement : basePage
{


    int TotalAmount=0;
    int resqty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // ItemTypes_Fill();
            FillBrand();
            FillItemCategory();
            gvItemsMasterDetails.Visible = false;
        }
    }


    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Fill Model No
    private void FillModelNo()
    {
        
    }
    #endregion

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo, "select distinct(YANTRA_ITEM_MAST.item_code),YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_ITEM_MAST,YANTRA_ITEM_QTY,YANTRA_LKUP_PRODUCT_COMPANY where YANTRA_ITEM_MAST.ITEM_CODE = YANTRA_ITEM_QTY.ITEM_CODE and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID = YANTRA_ITEM_MAST.BRAND_ID and  YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID = " + ddlBrand.SelectedItem.Value);

    }
    protected void gvItemsMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
           // e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[16].Visible = false;
            //e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToInt16(e.Row.Cells[7].Text);
            resqty = resqty + Convert.ToInt16(e.Row.Cells[8].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total Quantity:";
            e.Row.Cells[7].Text = TotalAmount.ToString();
            e.Row.Cells[8].Text = resqty.ToString();
        }

    }



    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster12_Select(ddlSubcategory);
            // Services.SalesQuotation.SalesQuotationItemTypes_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            //  Services.Dispose();
        }
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        gvItemsMasterDetails.DataSourceID = "sdsItemMasterDetails";
            lblSearchItemHidden.Text = "ITEM_MODEL_NO";
            lblSearchValueHidden.Text = ddlModelNo.SelectedItem.Text;
            lblColorid.Text = ddlColor.SelectedItem.Value;
        lblCPID.Text = "0";
            gvItemsMasterDetails.DataBind();
            gvItemsMasterDetails.Visible = true;
            GridView1.Visible = false;
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.DDLBindWithSelect(ddlColor, "select distinct(YANTRA_LKUP_COLOR_MAST.COLOUR_ID),( YANTRA_LKUP_COLOR_MAST.COLOUR_NAME) from YANTRA_LKUP_COLOR_MAST,YANTRA_ITEM_QTY where  YANTRA_ITEM_QTY.COLOUR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and YANTRA_ITEM_QTY.ITEM_CODE = " + ddlModelNo.SelectedItem.Value);
        }
        catch (Exception ex)
        {

        }
    }

    #region Fill Item Category
    private void FillItemCategory()
    {
        try
        {
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion


    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster1_Select(ddlSubcategory, ddlCategory.SelectedValue);

    }
    protected void ddlSubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       // gvItemsMasterDetails.DataSourceID = "sdsItemMasterDetails";
        lblSearchItemHidden.Text = "IT_TYPE";
        lblCategory.Text = ddlCategory.SelectedItem.Text;
        lblSubCategory.Text = ddlSubcategory.SelectedItem.Text;
        lblBrand.Text = ddlBrand.SelectedItem.Text;

        SM.GridBindwithCommand(GridView1, "select CP_FULL_NAME,GODOWN_NAME,COLOUR_NAME,YANTRA_ITEM_QTY.ITEM_QTY_IN_HAND,dbo.YANTRA_ITEM_MAST.ITEM_NAME, dbo.YANTRA_ITEM_MAST.ITEM_MODEL_NO, dbo.YANTRA_ITEM_MAST.BRAND_ID, dbo.YANTRA_ITEM_QTY.*,dbo.YANTRA_LKUP_ITEM_TYPE.IT_TYPE,dbo.YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_NAME,dbo.YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_NAME  from  YANTRA_COMP_PROFILE,YANTRA_LKUP_COLOR_MAST,YANTRA_LKUP_GODOWN,[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_ITEM_CATEGORY,YANTRA_LKUP_PRODUCT_COMPANY,YANTRA_ITEM_QTY    WHERE  YANTRA_ITEM_QTY.ITEM_QTY_IN_HAND != '0' and   YANTRA_ITEM_QTY.COLOUR_ID=YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND YANTRA_ITEM_QTY.GODOWN_ID=YANTRA_LKUP_GODOWN.GODOWN_ID AND YANTRA_COMP_PROFILE.CP_ID=YANTRA_ITEM_QTY.CP_ID AND YANTRA_ITEM_QTY.ITEM_CODE = [YANTRA_ITEM_MAST].ITEM_CODE and [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND  YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID and YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE = '" + lblSubCategory.Text + "' and dbo.YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_NAME = '" + lblBrand.Text + "'  and dbo.YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_NAME = '" + lblCategory.Text + "'  ORDER BY [YANTRA_ITEM_MAST].ITEM_MODEL_NO DESC");

       // gvItemsMasterDetails.DataBind();
        GridView1.Visible = true;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            // e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[16].Visible = false;
            //e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToInt16(e.Row.Cells[7].Text);
            resqty = resqty + Convert.ToInt16(e.Row.Cells[8].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total Quantity:";
            e.Row.Cells[7].Text = TotalAmount.ToString();
            e.Row.Cells[8].Text = resqty.ToString();
        }
    }
}

 
