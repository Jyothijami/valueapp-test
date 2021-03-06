using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_graph_BrandDCMonthly : System.Web.UI.UserControl
{
    DataTable smdt;
    protected void Page_Load(object sender, EventArgs e)
    {
        smdt = po.getBrandDCCount_Monthly();
    }

    protected string getMDataCount(string MonthName)
    {
        int x = 0;
        try
        {
            DataTable selectedTable = smdt.AsEnumerable()
                                .Where(r => r.Field<string>("Month_name") == MonthName)
                                .CopyToDataTable();


            foreach (DataRow dr in selectedTable.Rows)
            {
                x = x + Convert.ToInt32(dr["DC_Count"]);
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
        try
        {
            DataTable selectedTable = smdt.AsEnumerable()
                                .Where(r => r.Field<string>("Month_name") == MonthName)
                                .CopyToDataTable();

            foreach (DataRow dr in selectedTable.Rows)
            {
                if (!st.Equals(""))
                {
                    st = st + ",";
                }

                st = st + "['" + dr["Brand"].ToString() + "'," + dr["DC_Count"].ToString() + "]";
            }
        }
        catch (Exception) { }
        return st;
    }

}