using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
public partial class Modules_widgets_Warehouse_Stock : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string getData()
    {
        string data = "";

        int Inward = 0;
        int Block = 0;
        int Outward = 0;
        int Available = 0;
        TextBox txtModelNo = (TextBox)this.Parent.FindControl("txtModelNo");

        string[] st = Warehouse_Admin_Report.getAvailableStock_ModelNo(txtModelNo.Text).Split(new char[] { ',' });

        Inward = Convert.ToInt32(st[0]);
        Block = Convert.ToInt32(st[1]);
        Outward = Convert.ToInt32(st[2]);
        Available = Convert.ToInt32(st[3]);

        decimal total = Inward + Block + Outward + Available;

        if (total != 0)
        {

            decimal i = Math.Round((Inward / total) * 100, 2);
            decimal b = Math.Round((Block / total) * 100, 2);
            decimal o = Math.Round((Outward / total) * 100, 2);
            decimal a = Math.Round((Available / total) * 100, 2);

            data = "['Inward', " + i.ToString() + " ," + Inward.ToString() + "],['Block', " + b.ToString() + "," + Block.ToString() + "],['Outward', " + o.ToString() + "," + Outward.ToString() + "],['Available', " + a.ToString() + "," + Available.ToString() + "]";
        }

        return data;
    }

}