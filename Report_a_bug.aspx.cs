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
using Yantra.MessageBox;
public partial class Report_a_bug : System.Web.UI.Page
{
    SqlConnection con = dbc.con;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string url = HttpContext.Current.Request.RawUrl;

        string rptid = dbc.get_rnum("rptid", "Bug_tbl");

        SqlCommand cmd = new SqlCommand("insert into Bug_tbl (rptid, rpttitle, rptdesc, page_name , page_url, User_ID, dt_added , comments, status ) values(@rptid, @rpttitle, @rptdesc, @page_name, @page_url, @User_ID, @dt_added, @comments, @status)", con);
        cmd.Parameters.AddWithValue("@rptid",rptid);
        cmd.Parameters.AddWithValue("@rpttitle",txtTitle.Text);
        cmd.Parameters.AddWithValue("@rptdesc", txtDescription.Text);
        cmd.Parameters.AddWithValue("@page_name", txtPageName.Text);
        cmd.Parameters.AddWithValue("@page_url",url);
        cmd.Parameters.AddWithValue("@User_ID", Session["vl_userid"].ToString());
        cmd.Parameters.AddWithValue("@dt_added",sdate.getDateTime());
        cmd.Parameters.AddWithValue("@comments","");
        cmd.Parameters.AddWithValue("@status",0);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        MessageBox.Show(this, "Bug Reported Successfully");
        clearFields();
    }

    private void clearFields()
    {
        txtDescription.Text = txtPageName.Text = txtTitle.Text = "";
    }
}