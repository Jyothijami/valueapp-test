using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class dboards_Purchase_Dashboard : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string getUrl(String PI_ID)
    {

        // Modules/SCM/PurchaseInvoiceNew.aspx?invoiceNo=2&status=&nbsp;

        string custinfo = dbc.get_Multiple_column_data("select PI_ID, PI_NO, PI_STATUS  from YANTRA_PURCHASE_INVOICE_MAST where PI_ID=" + PI_ID, 3);
        string[] st = custinfo.Split(new char[] { ',' });

        return "/Modules/SCM/PurchaseInvoiceNew.aspx?invoiceNo=" + st[0] + "&status=" + st[2];
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblQuotStatus1");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblQuotStatus2");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
    protected void GridView6_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPurchaseStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPIStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}
 
