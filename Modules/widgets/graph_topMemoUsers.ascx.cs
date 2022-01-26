using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_graph_topMemoUsers : System.Web.UI.UserControl
{
    DataTable memodt;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        memodt = memo.getTopMemos();
    }

    protected string getEMPNames()
    {
        string st = "";

        foreach(DataRow dr in memodt.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            st = st + "'" + dr["EMP_Name"].ToString() + "'";
        }

        return st;
    }

    protected string getData()
    {
        string st = "";
        string str = "";
        foreach (DataRow dr in memodt.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            st = st + dr["Memo_Cnt"].ToString();
        }


        str = "{name: 'Memo', data: [" + st + "]}";


        return str;
    }
}