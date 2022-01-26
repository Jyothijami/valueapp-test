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
public partial class Testmaster : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        HttpCookie cookie = default(HttpCookie);
        if (Request.Cookies["udetails"] == null)
        {
            //cookie = new HttpCookie("udetails");

            Response.Redirect("~/default.aspx?rurl=" + Request.RawUrl);
        }
        else
        {
            cookie = Request.Cookies["udetails"];

            string[] u = new string[10];

            u[0] = cookie.Values["vl_empid"];
            u[1] = cookie.Values["vl_userfullname"];
            u[2] = cookie.Values["vl_useremail"];
            u[3] = cookie.Values["vl_username"];
            u[4] = cookie.Values["vl_cmpid"];
            u[5] = cookie.Values["vl_cmpname"];
            u[6] = cookie.Values["vl_usertype"];
            u[7] = cookie.Values["vl_deptid"];
            u[8] = cookie.Values["vl_desgid"];
            u[9] = cookie.Values["vl_userid"];

            Session["vl_empid"] = u[0];
            Session["vl_userfullname"] = u[1];
            Session["vl_useremail"] = u[2];
            Session["vl_username"] = u[3];
            Session["vl_cmpid"] = u[4];
            Session["vl_cmpname"] = u[5];
            Session["vl_usertype"] = u[6];
            Session["vl_deptid"] = u[7];
            Session["vl_desgid"] = u[8];
            Session["vl_userid"] = u[9];

            Response.AppendCookie(cookie);

            System.Web.HttpContext.Current.Session["YantraSession"] = (object)u;
        }

        if (Session["vl_userid"] == null)
        {
            Response.Redirect("~/default.aspx?rurl=" + Request.RawUrl);
        }
        else if (Session["vl_userid"].ToString().Equals(""))
        {
            Response.Redirect("~/default.aspx?rurl=" + Request.RawUrl);
        }
        else
        {
            string userid = Session["vl_userid"].ToString();

            litusername1.Text = Session["vl_username"].ToString();
            litprofilelnk1.Text = "<a href='/Modules/profiles/empProfile.aspx?id=" + userid + "'>Profile</a>";

            int unreadMessageCount = 0;
            unreadMessageCount = msgs.get_unread_count(userid);

            if (unreadMessageCount > 0)
            {
                litInboxUnreadCnt1.Text = "<span class='badge badge-info'>" + unreadMessageCount.ToString() + "</span>";
            }

            int noticount = 0;
            noticount = readRecords.getCircularMemoCount(userid);
            if (noticount > 0)
            {
                litnoticount1.Text = "<span class='badge badge-important'>" + noticount.ToString() + "</span>";
            }

            string companyid = Session["vl_cmpid"].ToString();
            string cplogo = cp.getCompanyLogo(companyid);

            cpImage1.ImageUrl = "/Content/CompanyProfileImgs/" + cplogo;
            cpImage1.AlternateText = companyid;

            loadddlCompanyProfile();
            ddlCompanyProfile1.SelectedValue = Session["vl_cmpid"].ToString();
        }


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();

        //string hai = Session["vl_userid"].ToString();
        string hai = Session["vl_empid"].ToString();
        objmas.EmployeeMaster_Select(hai);
        //lblEmpId.Text = objmas.EmpID;
        lblEmpId.Text = hai.ToString();

        showImg();
    }

    private void showImg()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        SqlCommand cmd = new SqlCommand("select EMP_PHOTO from YANTRA_EMPLOYEE_MAST where EMP_ID='" + lblEmpId.Text + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //Image1.ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + lblEmpId.Text + "";
        Image1.ImageUrl = "~/Content/EmployeeImage/" + dt.Rows[0][0] + "";


    }

    protected string getPageRawUrl()
    {
        return Request.RawUrl;
    }

    public string SiteMap()
    {
        string st = "";

        st = "<ul id='breadcrumbs'>";
        st += "<li><a href='javascript:void(0)'><i class='icon-home'></i></a></li>";
        st += ListChildNodes(System.Web.SiteMap.RootNode);

        st += "</ul>";
        return st;

    }

    private string ListChildNodes(System.Web.SiteMapNode node)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //sb.Append("<ul>");
        foreach (SiteMapNode item in node.ChildNodes)
        {
            sb.Append(string.Concat("<li><a href=\"", item.Url, "\">", item.Title, "</a></li>"));
            if (item.HasChildNodes)
                sb.Append(ListChildNodes(item));
        }
        //sb.Append("</ul>");

        return sb.ToString();
    }
    protected void lkbtLogout1_Click(object sender, EventArgs e)
    {
        Session["YantraSession"] = null;

        Session["vl_empid"] = null;
        Session["vl_userfullname"] = null;
        Session["vl_useremail"] = null;
        Session["vl_username"] = null;
        Session["vl_cmpid"] = null;
        Session["vl_cmpname"] = null;
        Session["vl_usertype"] = null;
        Session["vl_deptid"] = null;
        Session["vl_desgid"] = null;
        Session["vl_userid"] = null;
        Session["vl_links"] = null;

        HttpCookie cookie = default(HttpCookie);
        cookie = new HttpCookie("udetails");

        cookie.Values.Remove("vl_empid");
        cookie.Values.Remove("vl_userfullname");
        cookie.Values.Remove("vl_useremail");
        cookie.Values.Remove("vl_username");
        cookie.Values.Remove("vl_cmpid");
        cookie.Values.Remove("vl_cmpname");
        cookie.Values.Remove("vl_usertype");
        cookie.Values.Remove("vl_deptid");
        cookie.Values.Remove("vl_desgid");
        cookie.Values.Remove("vl_userid");


        Response.AppendCookie(cookie);

        Response.Cookies.Clear();

        Response.Redirect("~/default.aspx");
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rptSubMenu = e.Item.FindControl("Repeater2") as Repeater;
            rptSubMenu.DataSource = GetData("SELECT SubMenu_Id,SubMenu FROM [YANTRA_SUBMENU] where Menu_Id=" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0]);
            rptSubMenu.DataBind();
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        // string constr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        string constr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    protected void ddlCompanyProfile1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string compid = ddlCompanyProfile1.SelectedValue;
        string compname = ddlCompanyProfile1.SelectedItem.Text;

        Session["vl_cmpid"] = compid;
        Session["vl_cmpname"] = compname;

        HttpCookie cookie = default(HttpCookie);

        cookie = Request.Cookies["udetails"];

        cookie.Values.Remove("vl_cmpid");
        cookie.Values.Remove("vl_cmpname");

        cookie.Values.Add("vl_cmpid", compid);
        cookie.Values.Add("vl_cmpname", compname);

        Response.AppendCookie(cookie);

        Yantra.Authentication.UpdateYantraSessionToCompany(compid, compname);

        Response.Redirect(Request.RawUrl);
    }

    protected void loadddlCompanyProfile()
    {
        DataTable dt = cpAccess.getCompanies(Session["vl_userid"].ToString());

        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr["CP_ID"].ToString();
            li.Text = dr["CP_Name"].ToString();

            if (dr["CP_ID"].ToString().Equals(Session["vl_cmpid"].ToString()))
            {
                li.Selected = true;
            }

            ddlCompanyProfile1.Items.Add(li);
        }
    }
}
