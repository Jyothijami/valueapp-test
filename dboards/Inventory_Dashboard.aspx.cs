using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class dboards_Inventory_Dashboard : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string getUrl1(String IND_ID)
    {
        //Modules/SCM/ChangedIndentDetails.aspx?IndentId=573&AppBy=&nbsp;&Cust=Self

        string Quot = dbc.get_Multiple_column_data("select IND_ID, IND_APPROVED_BY, INDENT_FOR  from YANTRA_INDENT_MAST where IND_ID=" + IND_ID, 3);
        string[] st = Quot.Split(new char[] { ',' });

        return "/Modules/SCM/ChangedIndentDetails.aspx?IndentId=" + st[0] + "&AppBy=" + st[1] + "&Cust=" + st[2];
    }
    protected string getUrl2(String Dispatch_id)
    {
        //Modules/SM/DispatchformDetails.aspx?DispatchId=322&AppBy=&nbsp;
        string Quot = dbc.get_Multiple_column_data("select Dispatch_id, ApprovedBy  from Dispatch where Dispatch_id=" + Dispatch_id, 2);
        string[] st = Quot.Split(new char[] { ',' });

        return "/Modules/SM/DispatchformDetails.aspx?DispatchId=" + st[0] + "&AppBy=" + st[1];
    }
    protected string getUrl3(String DC_ID)
    {
        //Modules/Inventory/DeliveryChallanDetails.aspx?DcId=132&SoId=109&DcType=Non Returnable&DcFor=Sales
        string Quot = dbc.get_Multiple_column_data("select DC_ID, SO_ID, DC_TYPE, DC_FOR from YANTRA_DELIVERY_CHALLAN_MAST where DC_ID=" + DC_ID, 4);
        string[] st = Quot.Split(new char[] { ',' });

        return "/Modules/Inventory/DeliveryChallanDetails.aspx?DcId=" + st[0] + "&SoId=" + st[1] + "&DcType=" + st[2] + "&DcFor=" + st[3];
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblIndentStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblDispatchStatus");

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
            Label lbl = (Label)e.Row.FindControl("lblDCStatus");

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
            Label lbl = (Label)e.Row.FindControl("lblIOStatus");

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
            Label lbl = (Label)e.Row.FindControl("lblDCCashStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}
 
