using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_widgets_userLinks : System.Web.UI.UserControl
{
    SqlConnection con = dbc.con;
    protected void Page_Load(object sender, EventArgs e)
    {
        string links = "";

        if (Session["vl_links"] == null)
        {
            links = getLinks();
            Session["vl_links"] = links;
        }
        else
        {
            string st = "";
            st = Session["vl_links"].ToString();

            if (st == null || st.Equals(""))
            {
                links = getLinks();
                Session["vl_links"] = links;
            }
            else
            {
                links = st;
            }
        }

        //HttpCookie cookie = default(HttpCookie);
        //if (Request.Cookies["udetails"] == null)
        //{
        //    cookie = new HttpCookie("udetails");

        //    links = loadLinks();
        //    cookie.Values.Add("vl_links", links);
        //}
        //else
        //{
        //    cookie = Request.Cookies["udetails"];

        //    links = cookie.Values["vl_links"];

        //    if (links == null || links.Equals(""))
        //    {
        //        links = loadLinks();
        //        cookie.Values.Add("vl_links", links);
        //    }
        //}

        //Response.AppendCookie(cookie);

        litlinks1.Text = links;
    }

    protected string getLinks()
    {
        string st = "";

        foreach (DataRow dr in getPrivilegesCategory().Rows)
        {
            st = st + "<li>";
            st = st + "<a>" + dr["catename"].ToString() + "</a>" + Environment.NewLine;

            st = st + "<ul style='top: 34px; visibility: visible; left: 0px; width: 160px; display: none;'>" + Environment.NewLine;
            foreach (DataRow ddr in getPrivileges(dr["catename"].ToString()).Rows)
            {
                st = st + "<li><a href='/Modules/" + ddr["pageurl"].ToString() + "'>" + ddr["pagename"].ToString() + "</a></li>" + Environment.NewLine;
            }
            st = st + "</ul>" + Environment.NewLine;
            st = st + "</li>";
        }

        return st;
    }

    protected DataTable getPrivilegesCategory()
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            conn.Close();

            string squery = "";
            //squery = "select distinct catename from YANTRA_LKUP_PRIVILEGES order by catename asc";
            //squery = "SELECT DISTINCT YANTRA_LKUP_PRIVILEGES.catename FROM YANTRA_LKUP_PRIVILEGES INNER JOIN YANTRA_USER_PRIVILEGES ON YANTRA_LKUP_PRIVILEGES.PRIVILEGE_ID = YANTRA_USER_PRIVILEGES.PRIVILEGE_ID WHERE (YANTRA_USER_PRIVILEGES.USER_Id = @USER_Id) ORDER BY YANTRA_LKUP_PRIVILEGES.catename";

            //        squery = @"SELECT DISTINCT YANTRA_LKUP_PRIVILEGES.catename, YANTRA_LKUP_PRIVILEGES_CATE.cateid
            //FROM         YANTRA_LKUP_PRIVILEGES INNER JOIN
            //                      YANTRA_USER_PRIVILEGES ON YANTRA_LKUP_PRIVILEGES.PRIVILEGE_ID = YANTRA_USER_PRIVILEGES.PRIVILEGE_ID INNER JOIN
            //                      YANTRA_LKUP_PRIVILEGES_CATE ON YANTRA_LKUP_PRIVILEGES.catename = YANTRA_LKUP_PRIVILEGES_CATE.catename
            //WHERE     (YANTRA_USER_PRIVILEGES.USER_Id = @USER_Id)
            //ORDER BY YANTRA_LKUP_PRIVILEGES_CATE.cateid";

            squery = @"SELECT DISTINCT YANTRA_LKUP_PRIVILEGES.catename, YANTRA_LKUP_PRIVILEGES_CATE.cateid
FROM         YANTRA_LKUP_PRIVILEGES INNER JOIN
                      YANTRA_USER_PERMISSIONS ON YANTRA_LKUP_PRIVILEGES.PRIVILEGE_ID = YANTRA_USER_PERMISSIONS.PRIVILEGE_ID INNER JOIN
                      YANTRA_LKUP_PRIVILEGES_CATE ON YANTRA_LKUP_PRIVILEGES.catename = YANTRA_LKUP_PRIVILEGES_CATE.catename
WHERE     (YANTRA_USER_PERMISSIONS.UserId = @USER_Id) and (YANTRA_USER_PERMISSIONS.permission = 1)
ORDER BY YANTRA_LKUP_PRIVILEGES_CATE.cateid";


            cmd = new SqlCommand(squery, conn);
            cmd.Parameters.Add("@USER_Id", SqlDbType.BigInt).Value = Convert.ToInt32(Session["vl_userid"].ToString());
            conn.Open();

            da.SelectCommand = cmd;
            da.Fill(dt);
        }

        return dt;
    }

    protected DataTable getPrivileges(string catename)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            conn.Close();
            string squery = "";
            //squery = "select PRIVILEGE_NAME, pagename, pageurl from YANTRA_LKUP_PRIVILEGES where catename = @catename order by catename asc";
            squery = "SELECT distinct YANTRA_LKUP_PRIVILEGES.seqno, YANTRA_LKUP_PRIVILEGES.PRIVILEGE_NAME, YANTRA_LKUP_PRIVILEGES.pagename, YANTRA_LKUP_PRIVILEGES.pageurl FROM YANTRA_LKUP_PRIVILEGES INNER JOIN YANTRA_USER_PERMISSIONS ON YANTRA_LKUP_PRIVILEGES.PRIVILEGE_ID = YANTRA_USER_PERMISSIONS.PRIVILEGE_ID  WHERE (YANTRA_LKUP_PRIVILEGES.catename = @catename) AND (YANTRA_USER_PERMISSIONS.UserId = @USER_Id) and (YANTRA_USER_PERMISSIONS.permission = @permission) ORDER BY YANTRA_LKUP_PRIVILEGES.seqno";
            cmd = new SqlCommand(squery, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@catename", SqlDbType.VarChar).Value = catename;
            cmd.Parameters.Add("@USER_Id", SqlDbType.BigInt).Value = Convert.ToInt32(Session["vl_userid"].ToString());
            cmd.Parameters.Add("@permission", SqlDbType.Int).Value = 1;
            conn.Open();

            da.SelectCommand = cmd;
            da.Fill(dt);
        }
        return dt;
    }
}