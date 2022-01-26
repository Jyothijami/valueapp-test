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

public partial class dev_pages_DBoardAlertMessage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }

    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        addDBoardAlert(tbxSubject1.Text, tbxMessage1.Text, tbxStart1.Text, tbxEnd1.Text, true);
        GridView1.DataBind();
    }

    protected bool addDBoardAlert(string title, string desc, string starttime, string endtime, bool isEnabled)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            con.Close();
            string instr = "insert into dboardAlert_tbl(messagetitle, messagedesc, starttime, endtime, dt_modified, isEnabled) values(@messagetitle, @messagedesc, @starttime, @endtime, @dt_modified, @isEnabled)";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@messagetitle", SqlDbType.VarChar).Value = title;
            cmd.Parameters.Add("@messagedesc", SqlDbType.VarChar).Value = desc;
            cmd.Parameters.Add("@starttime", SqlDbType.DateTime).Value = Convert.ToDateTime(starttime);
            cmd.Parameters.Add("@endtime", SqlDbType.DateTime).Value = Convert.ToDateTime(endtime);
            cmd.Parameters.Add("@dt_modified", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@isEnabled", SqlDbType.Bit).Value = isEnabled;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        catch (Exception ex)
        {
            lblerr1.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
        return false;
    }


}
 
