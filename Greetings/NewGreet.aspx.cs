using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class NewGreet : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvwishes.DataBind();
            //bindnotify();
        }

    }

    public void bindnotify()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select count(*) from  log_details_tbl1 where logcateid =140 and DATEPART(M,dt_added )=DATEPART(m,getdate()) and DATEPART(d,dt_added )=DATEPART(d,getdate())", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            lblnotify.Text = dr[0].ToString();
        }
        con.Close();


    }
    protected void btnwishme_Click(object sender, EventArgs e)
    {
        string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        string Username = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);

        string wishs = txtwishes.Text;
        HttpContext htc = System.Web.HttpContext.Current;
        con.Open();
        //log.add_Insert(wishs + " Greetings " + " - Wished by:  " + htc.Session["vl_username"].ToString(), "140");
        log.add_Insert(wishs + " Greetings " + " - Wished by:  " + Username, "140");

        txtwishes.Text = "";
    }
    protected void imgbtnnotify_Click(object sender, ImageClickEventArgs e)
    {
        gvwishes.Visible = true;
        //bindnotify();
    }
}