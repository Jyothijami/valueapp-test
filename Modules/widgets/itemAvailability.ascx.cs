using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_itemAvailability : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoadData()
    {
        //DataTable dt = itemsAvailability.getItemAvailability_3Months();

        //foreach (DataRow dr in dt.Rows)
        //{

        //}

    }

    protected string getMonths()
        {
        return itemsAvailability.getItemAvailability_3Months();
    }

    protected string getData()
    {
        string st1 = "";

        DataTable dt = itemsAvailability.getItemAvailability_3Months_data("1234");


        string w, x, y, z;
        w = x = y = z = "";

        string mnths = getMonths();

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

            //if (dr["MONTH_YEAR"].ToString().Equals(mnths.Split(new char[] { ',' })[i]))
            //{
            //    w = w + dr["TOTAL_OUTWARD_STOCK"].ToString();
            //    x = x + dr["TOTAL_AVAILABLE_STOCK"].ToString();
            //    y = y + dr["TOTAL_BLOCK_STOCK"].ToString();
            //    z = z + dr["ORDERED_STOCK"].ToString();
            //}
            //else
            //{
            //    w = w + "0";
            //    x = x + "0";
            //    y = y + "0";
            //    z = z + "0";
            //}

            w = w + dr["TOTAL_OUTWARD_STOCK"].ToString();
            x = x + dr["TOTAL_AVAILABLE_STOCK"].ToString();
            y = y + dr["TOTAL_BLOCK_STOCK"].ToString();
            z = z + dr["ORDERED_STOCK"].ToString();
            
            i++;
        }


        st1 = "{name: 'OutWard', data: [" + w + "]},{name: 'Available', data: [" + x + "]},{name: 'Block', data: [" + y + "]},{name: 'Ordered', data: [" + z + "]}";

        return st1;
    }
}