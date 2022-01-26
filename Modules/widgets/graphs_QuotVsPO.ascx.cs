using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_graphs_QuotVsPO : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string getQuotData()
    {
        string st = "";

        DataTable dt = SQ.getQuotCount_Monthly();

        for (int i = 0; i< 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception) {
                st = st + "0";
            }
        }

        return st;
    }

    protected string getPOData()
    {
        string st = "";

        DataTable dt = po.getPOCount_Monthly();

        for (int i = 0; i < 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception)
            {
                st = st + "0";
            }
        }

        return st;
    }
}