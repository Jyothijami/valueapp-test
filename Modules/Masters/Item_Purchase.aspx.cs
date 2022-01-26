using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;
public partial class Modules_Masters_Item_Purchase : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "107");
        btnSave.Enabled = up.add;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                foreach (GridViewRow gvrow in gvItemPriceUpdate.Rows)
                {
                    TextBox t1 = gvrow.FindControl("txtIP") as TextBox;
                    TextBox t2 = gvrow.FindControl("txtRpp") as TextBox;
                    DropDownList d1 = gvrow.FindControl("ddlCurrency") as DropDownList;

                    if (t1.Text != "" && t2.Text != "")
                    {
                        if (gvrow.RowType == DataControlRowType.DataRow)
                        {
                            //TextBox t1 = gvrow.FindControl("txtIP") as TextBox;
                            //TextBox t2 = gvrow.FindControl("txtRpp") as TextBox;
                            //DropDownList d1 = gvrow.FindControl("ddlCurrency") as DropDownList;
                            obj.itemcode = gvrow.Cells[0].Text;
                            obj.brandid = gvrow.Cells[2].Text;
                            obj.catid = gvrow.Cells[1].Text;
                            obj.subcatid = gvrow.Cells[9].Text;
                            obj.Currency = d1.SelectedItem.Value;
                            obj.internationalprice = t1.Text;
                            obj.rpp = t2.Text;
                            obj.Barcode = gvrow.Cells[4].Text;
                            obj.mrp = "0";
                            obj.rsp = "0";
                            obj.currencyType = "0"; obj.gross = "0"; obj.coefficient = "0"; obj.mulFactor = "0";
                            obj.ItemPurchase_Save();
                             //obj.ItemPriceUpdate(int.Parse(gvrow.Cells[0].Text), "0", 0, int.Parse(gvrow.Cells[2].Text), int.Parse(gvrow.Cells[1].Text));
                            MessageBox.Show(this, "Prices Saved Sucessfully");
                        }
                    }
                }
                gvItemPriceUpdate.DataBind();
        }
        catch
        {

        }
        finally
        {
            gvItemPriceUpdate.DataBind();
        }
    }

    protected void gvItemPriceUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCurrency = (DropDownList)e.Row.FindControl("ddlCurrency");
            Masters.CurrencyType.CurrencyType_Select(ddlCurrency);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;

            e.Row.Cells[0].Text = "Page " + (gvItemPriceUpdate.PageIndex + 1) + " of " + gvItemPriceUpdate.PageCount;
        }
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvItemPriceUpdate.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvItemPriceUpdate.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}
 
