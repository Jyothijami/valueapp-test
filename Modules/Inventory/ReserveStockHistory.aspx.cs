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
using vllib;
public partial class Modules_Inventory_ReserveStockHistory : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvStockHistory.DataBind();
    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvStockHistory.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
    }
    protected void gvStockHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[4].Text.Contains(""))
            //{
            //    e.Row.Cells[4].Text = "0";
            //}
            //if (e.Row.Cells[3].Text.Contains(""))
            //{
            //    e.Row.Cells[3].Text = "0";
            //}
           // e.Row.Cells[7].Text = ((Convert.ToInt16(e.Row.Cells[5].Text)) - (Convert.ToInt16(e.Row.Cells[6].Text))).ToString();
        }
    }


    protected void SqlDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStockHistory.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvStockHistory.DataBind();
    }
    protected void btnSearchGo_Click1(object sender, EventArgs e)
    {

    }
}

 
