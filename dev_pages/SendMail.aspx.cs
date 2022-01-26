using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vllib;
using Yantra.MessageBox;

public partial class dev_pages_SendMail : System.Web.UI.Page
{
    static string msgid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            string fromuid = Session["vl_userid"].ToString();
            hffromuid1.Value = fromuid;
            msgid = dbc.get_rnum("msgid", "msgs_tbl");

        }
    }
    private void BindData()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString()))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT USER_NAME  ,USER_ID FROM YANTRA_USER_DETAILS  where EXPIRY_DATE ='2019-12-31 00:00:00.000' Order by USER_NAME";
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                // BIND THE SELECT DROP-DOWN LIST WITH A DATABASE TABLE.
                Books.DataSource = dt;
                Books.DataTextField = "USER_NAME";
                Books.DataValueField = "USER_ID";
                Books1.DataSource = dt;
                Books1.DataTextField = "USER_NAME";
                Books1.DataValueField = "USER_ID";
                Books.DataBind();
                Books1.DataBind();

                // FOR MULTIPLE SELECTION. SET THE VALUE AS FALSE, AND SEE WHAT HAPPENS.
                Books.Multiple = true;
                Books1.Multiple = true;

            }
        }


        // SM.CustomerMaster.CustomerMaster_Select(Books);


    }
    protected void btnsend_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in Books.Items)
            {
                if (item.Selected)
                {

                    String EmpID = item.Value.ToString();
                    msgs.send_message(EmpID, txtsub.Text, txtmsg.Text, hffromuid1.Value, msgid);
                   

                }
            }
         MessageBox.Show(this, "Message Send Successfully");
      
    }
}