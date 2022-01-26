using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class waste10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtval1.Text = sdate.getDateTime().ToString();

        createTree();
    }

    protected void createTree()
    {
        TextBox1.Text = getLocations();
        
    }

    protected string getLocations()
    {
        string st = "";

        DataTable dt = WH.getWarehouseLocations();

        foreach (DataRow dr in dt.Rows)
        {
            if (!st.Equals(""))
            {
                st += ",";
            }

            st += "{" + Environment.NewLine;

            st += "\"id\":\"L" + dr["locid"].ToString() + "\"," + Environment.NewLine;
            st += "\"text\":\"" + dr["locname"].ToString() + "\"" + Environment.NewLine;

            st += getWarehouses(dr["locid"].ToString());

            st += "}" + Environment.NewLine;
        }

        return "[" + st + "]";
    }

    protected string getWarehouses(string locid)
    {
        string st = "";

        DataTable dt = WH.getWarehouses(locid);

        foreach (DataRow dr in dt.Rows)
        {
            if (!st.Equals(""))
            {
                st += ",";
            }

            st += "{" + Environment.NewLine;

            st += "\"id\":\"W" + dr["wh_id"].ToString() + "\"," + Environment.NewLine;
            st += "\"text\":\"" + dr["whname"].ToString() + "\"" + Environment.NewLine;

            st += getSubLocations(dr["wh_id"].ToString());

            st += "}" + Environment.NewLine;
        }

        if (!st.Equals(""))
        {
            st = ",\"children\":[" + st + "]";
        }

        return st;
    }

    protected string getSubLocations(string whid, string whLocId = "0")
    {
        string st = "";

        DataTable dt = WH_Locations.getLocations2(whid, whLocId);

        foreach (DataRow dr in dt.Rows)
        {
            if (!st.Equals(""))
            {
                st += ",";
            }

            st += "{" + Environment.NewLine;

            st += "\"id\":\"S" + dr["whLocId"].ToString() + "\"," + Environment.NewLine;
            st += "\"text\":\"" + dr["whLocName"].ToString() + "\"" + Environment.NewLine;

            st += getSubLocations(whid, dr["whLocId"].ToString());

            st += "}" + Environment.NewLine;
        }

        if (!st.Equals(""))
        {
            st = ",\"children\":[" + st + "]";
        }

        return st;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        //txtval1.Text = Request.Form["textbox-value"];
        //txtval1.Text = ddltxtval.Value;


        
    }
}