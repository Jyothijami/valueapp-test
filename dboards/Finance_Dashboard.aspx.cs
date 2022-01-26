using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class dboards_Finance_Dashboard : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string getUrl(String SI_ID)
    {

        // Modules/Inventory/InvoiceDetails.aspx?SI_ID=43&AppBy=&nbsp;&DcFor=Sales

        string custinfo = dbc.get_Multiple_column_data("select a.SI_ID, a.SI_APPROVED_BY, b.DC_FOR from YANTRA_SALES_INVOICE_MAST a inner join YANTRA_DELIVERY_CHALLAN_MAST b on a.DC_ID=b.DC_ID where a.SI_ID=" + SI_ID, 3);
        string[] st = custinfo.Split(new char[] { ',' });

        return "/Modules/Inventory/InvoiceDetails.aspx?SI_ID=" + st[0] + "&AppBy=" + st[1] + "&DcFor=" + st[2];
    }
    protected string getUrl1(String PR_ID)
    {

        // Modules/SM/PaymentsReceivedNew.aspx?Prid=29&PrStatus=Pending

        string custinfo = dbc.get_Multiple_column_data("select PR_ID, PR_PAYMENT_STATUS from YANTRA_PAYMENTS_RECEIVED where PR_ID=" + PR_ID, 2);
        string[] st = custinfo.Split(new char[] { ',' });

        return "/Modules/SM/PaymentsReceivedNew.aspx?Prid=" + st[0] + "&PrStatus=" + st[1];
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblSIStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPRStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblSIStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}
 
