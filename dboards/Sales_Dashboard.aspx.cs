using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class dboards_Sales_Dashboard : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblCustStatus1");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            
        }
    }

    protected string getUrl(String CUST_ID)
    {
        // CustomerInformation_Details.aspx? CustId = 3966 & CUST_NAME = CI - 20 / 15 - 16

        string custinfo = dbc.get_Multiple_column_data("Select CUST_ID, CUST_CODE FROM YANTRA_CUSTOMER_MAST where CUST_ID = " + CUST_ID, 2);
        string[] st = custinfo.Split(new char[] { ',' });

        return "/Modules/SM/CustomerInformation_Details.aspx?CustId=" + st[0] + "&CUST_NAME=" + st[1];
        //return Page.ClientScript.RegisterStartupScript(
        //   this.GetType(), "OpenWindow",
        //   "window.open('second.aspx','_newtab');", true);
    }
   
    protected string getUrl1(String QUOT_ID)
    {
        //SalesQuotationDetails.aspx?QuoNo=SQtn-73/15-16 &QuoId=189&AppBy=&nbsp;&Status=New&lbtn=lbtn

        string Quot = dbc.get_Multiple_column_data("select QUOT_ID, QUOT_NO, QUOT_APPROVED_BY, QUOT_PO_FLAG from YANTRA_QUOT_MAST where QUOT_ID= " + QUOT_ID, 4);
        string[] st = Quot.Split(new char[] { ',' });

        return "/Modules/SM/SalesQuotationDetails.aspx?QuoNo=" + st[1] + "&QuoId=" + st[0] + "&AppBy=" + st[2] + "&Status=" + st[3] + "&lbtn=lbtn";
    }
    protected string getUrl2(String WO_ID)
    {
        // WorkOrderDetails.aspx?IoNo=IO-17/15-16&IoId=85&AppBy=Auron &Status=&nbsp;

        string custinfo = dbc.get_Multiple_column_data("select WO_ID, WO_NO, WO_APPROVED_BY, WO_FLAG  from YANTRA_WO_MAST where WO_ID= " + WO_ID, 4);
        string[] st = custinfo.Split(new char[] { ',' });

        return "/Modules/SM/WorkOrderDetails.aspx?IoNo=" + st[1] + "&IoId=" + st[0] + "&AppBy=" + st[2] + "&Status=" + st[3];
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblEnqStatus1");

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
            Label lbl = (Label)e.Row.FindControl("lblAssignStatus1");

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
            Label lbl = (Label)e.Row.FindControl("lblQuotStatus1");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPOStatus");

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
            Label lbl = (Label)e.Row.FindControl("lblIOStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}
 
