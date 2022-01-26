using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using vllib;
public partial class Modules_HR_BirthdayWishes : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        gvwishes.DataBind();
        setControlsVisibility();
        bindnotify();
        
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
    private void setControlsVisibility()
    {
        //User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "68");
        //btnwishme.Enabled = up.add;
    }
    protected void btnwishme_Click(object sender, EventArgs e)
    {
        imgty.Visible = true;
        string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        string wishs = txtwishes.Text;
        HttpContext htc = System.Web.HttpContext.Current;
        con.Open();
        log.add_Insert(wishs + " Greetings " + " - Wished by:  " + htc.Session["vl_username"].ToString(), "140");
        txtwishes.Text = "";
    }
    protected void imgbtnnotify_Click(object sender, ImageClickEventArgs e)
    {
        gvwishes.Visible = true;
        imgty.Visible = true;
        bindnotify();
    }
}