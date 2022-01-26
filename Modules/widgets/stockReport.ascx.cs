using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_stockReport : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            
        }
    }

    protected string getLocation()
    {
        
        string st1 = "";
        DataTable dt = Session["StockReport"] as DataTable;

        int i = 0;

        foreach (DataRow dr in dt.Rows)
        {
            if (st1 == "")
            {
                st1 = dr["Location"].ToString();           
                

            }
            else
            {
                st1 = st1 + "," + dr["Location"].ToString();           
            }
             
            i++;
        }

        return st1;
    }

    protected string getItemQty()
    {
        string st1 = "";

        DataTable dt = Session["StockReport"] as DataTable;

        string w, x, y, z;
        w = x = y = z = "";

        string xaxis = getLocation();
        int i = 0;

        foreach (DataRow dr in dt.Rows)
        {
            if (!w.Equals(""))
            {
                w = w + ",";
                x = x + ",";
                y = y + ",";
                z = z + ",";

            }

            w = w + dr["Inward Stock"].ToString();
            x = x + dr["Blocked Stock"].ToString();
            y = y + dr["Outward Stock"].ToString();
            z = z + dr["Available Stock"].ToString();

            i++;
        }


        st1 = "{name: 'Inward', data: [" + w + "]},{name: 'Block', data: [" + x + "]},{name: 'Outward', data: [" + y + "]},{name: 'Available', data: [" + z + "]}";

        return st1;

    }
}