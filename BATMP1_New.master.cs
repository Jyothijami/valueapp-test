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
using Yantra.MessageBox;
public partial class BATMP1 : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
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

            string[] u = new string[11];

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
            u[10] = cookie.Values["vl_userstype"];

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
            Session["vl_userstype"] = u[10];

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

        Page.LoadComplete += new EventHandler(Page_LoadComplete);
        //Page.LoadComplete += new EventHandler(Page_LoadComplete1);

    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "128");
        btnlog.Enabled = up.add;
        btnlog.Enabled = up.Update;
        btnlog.Enabled = up.Approve;
        btnlog.Enabled = up.Delete;
        btnlog.Enabled = up.Full_ReadOnly ;

        if (up.add || up.Update || up.Approve || up.Delete || up.Email || up.Print || up.Full_ReadOnly)
        {
            btnlog.Visible = true;
            PnlSiteAlert1.Visible = true;
        }
        else { btnlog.Visible = false;
        PnlSiteAlert1.Visible = false;
        }
        
    }
    void Page_LoadComplete(object sender, EventArgs e)
    {
        if (AlertDataList1.Items.Count == 0)
        {
            setControlsVisibility();
            PnlSiteAlert1.Visible = false;
            showlogbtn();
        }
    }
    //void Page_LoadComplete1(object sender, EventArgs e)
    //{
    //    if (BDYDataList1.Items.Count == 0)
    //    {

    //        //PnlSiteAlert1.Visible = false;
    //        pnlbdy.Visible = false;
    //    }
    //}
    static string msgid;
    protected void Page_Load(object sender, EventArgs e)
    {
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();

        //string hai = Session["vl_userid"].ToString();
        string hai = Session["vl_empid"].ToString();
        objmas.EmployeeMaster_Select(hai);
        //lblEmpId.Text = objmas.EmpID;
        lblEmpId.Text = hai.ToString();
        setControlsVisibility();
        showImg();
        showbtn();
        showlogbtn();
        //if (!IsPostBack)
        //{
        //    BindData();
        //    string fromuid = Session["vl_userid"].ToString();
        //    hffromuid1.Value = fromuid;
        //    msgid = dbc.get_rnum("msgid", "msgs_tbl");
        //}
    }
    //private void BindData()
    //{
    //    DataTable dt = new DataTable();

    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString()))
    //    {
    //        using (SqlCommand cmd = con.CreateCommand())
    //        {
    //            cmd.CommandText = "SELECT USER_NAME  ,USER_ID FROM YANTRA_USER_DETAILS  where EXPIRY_DATE ='2019-12-31 00:00:00.000' Order by USER_NAME";
    //            con.Open();

    //            SqlDataAdapter sda = new SqlDataAdapter();
    //            sda.SelectCommand = cmd;
    //            sda.Fill(dt);

    //            // BIND THE SELECT DROP-DOWN LIST WITH A DATABASE TABLE.
    //            Books.DataSource = dt;
    //            Books.DataTextField = "USER_NAME";
    //            Books.DataValueField = "USER_ID";
    //            Books1.DataSource = dt;
    //            Books1.DataTextField = "USER_NAME";
    //            Books1.DataValueField = "USER_ID";
    //            Books.DataBind();
    //            Books1.DataBind();

    //            // FOR MULTIPLE SELECTION. SET THE VALUE AS FALSE, AND SEE WHAT HAPPENS.
    //            Books.Multiple = true;
    //            Books1.Multiple = true;

    //        }
    //    }


    //    // SM.CustomerMaster.CustomerMaster_Select(Books);


    //}
    private void showlogbtn()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from log_details_tbl1 where logcateid =141 and logtypeid ='Open'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            PnlSiteAlert1.Visible = true;
            btnlog.Visible = true;
            //btnBdyClick.Visible = false;

        }
        else
        {
            btnlog.Visible = false;
        }
        con.Close();
    }
    //protected void btnsend_Click(object sender, EventArgs e)
    //{
    //    foreach (ListItem item in Books.Items)
    //    {
    //        if (item.Selected)
    //        {

    //            String EmpID = item.Value.ToString();
    //            msgs.send_message(EmpID, txtsub.Text, txtmsg.Text, hffromuid1.Value, msgid);


    //        }
    //    }
    //    MessageBox.Show(this, "Message Send Successfully");

    //}
    private void showbtn()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select EMP_FIRST_NAME from YANTRA_EMPLOYEE_MAST where DATEPART(M,EMP_DOB)=DATEPART(m,getdate()) and DATEPART(d,EMP_DOB)=DATEPART(d,getdate()) and status=1", con);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            btnBdyClick.Visible = true;
            //btnBdyClick.Visible = false;

        }
        else
        {
            btnBdyClick.Visible = false;
        }
        con.Close();
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
        Session["vl_userstype"] = null;

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
        cookie.Values.Remove("vl_userstype");


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
        try
        {
            DataTable dt = cpAccess.getCompanies(Session["vl_userid"].ToString());

            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                li.Value = dr["CP_ID"].ToString();
                li.Text = dr["CP_SHORT_NAME"].ToString();
                //li.Text = dr["CP_Name"].ToString();
            

                if (dr["CP_ID"].ToString().Equals(Session["vl_cmpid"].ToString()))
                {
                    li.Selected = true;
                }

                ddlCompanyProfile1.Items.Add(li);
            }
        }
        
        catch(Exception)
        {
            MessageBox.Show(this, "Try reloading the page once");
        }
    }

    protected void lkbtDBoard1_Click(object sender, EventArgs e)
    {
        string userstype = Session["vl_userstype"].ToString();

        string dboard = uPriv.getUserDashboard(userstype);

        Response.Redirect("~/dboards/" + dboard);
    }
    protected void btnlog_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://183.82.108.55/Modules/SM/Customer_Info_Log_Activity - Copy.aspx");
    }
}
