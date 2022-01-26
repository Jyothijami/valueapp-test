using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data;

[PartialCaching(240)]
public partial class Modules_widgets_graph_SalesQuotationsMonthly : System.Web.UI.UserControl
{
    DataTable smdt;

    protected void Page_Init(object sender, EventArgs e)
    {
        smdt = SQ.getSalesQuotationCount_Monthly();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //cLiteral1.Text = getData();
    }

    protected string getData()
    {
        string st = "";

        DataTable dt = SQ.getSalesQuotationCount_Monthly();
        foreach (DataRow dr in dt.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + Environment.NewLine;
            }

            st = st + dr["Month"].ToString() + "|" + dr["Exe_Name"].ToString() + "|" + dr["quot_no"].ToString();
        }

        return st;
    }

    protected string getMDataCount(string MonthName)
    {
        int x = 0;
        try {
            DataTable selectedTable = smdt.AsEnumerable()
                                .Where(r => r.Field<string>("Month") == MonthName)
                                .CopyToDataTable();


            foreach (DataRow dr in selectedTable.Rows)
            {
                x = x + Convert.ToInt32(dr["quot_no"]);
            }
        }
        catch (Exception)
        {

        }
        return x.ToString();
    }

    protected string getMData(string MonthName)
    {
        string st = "";
        try {
            DataTable selectedTable = smdt.AsEnumerable()
                                .Where(r => r.Field<string>("Month") == MonthName)
                                .CopyToDataTable();

            foreach (DataRow dr in selectedTable.Rows)
            {
                if (!st.Equals(""))
                {
                    st = st + ",";
                }

                st = st + "['" + dr["Exe_Name"].ToString() + "'," + dr["quot_no"].ToString() + "]";
            }
        }
        catch (Exception) { }
        return st;
    }
}