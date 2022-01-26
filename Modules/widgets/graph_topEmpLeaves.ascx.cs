using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_graph_topEmpLeaves : System.Web.UI.UserControl
{
    DataTable leavedt;

    protected void Page_Load(object sender, EventArgs e)
    {
        leavedt = leave.getTopLeaves_WRT_Employees();
    }

    protected string getEMPNames()
    {
        string st = "";

        foreach (DataRow dr in leavedt.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            st = st + "'" + dr["emp_name"].ToString() + "'";
        }

        return st;
    }

    protected string getData()
    {
        string st = "";
        string str = "";
        foreach (DataRow dr in leavedt.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            st = st + dr["TotalLeaves"].ToString();
        }


        str = "{name: 'Leaves', data: [" + st + "]}";


        return str;
    }

}