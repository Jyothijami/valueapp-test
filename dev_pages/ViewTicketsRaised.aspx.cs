using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using YantraBLL.Modules;

public partial class dev_pages_ViewTicketsRaised : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void gvRaisedTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox status = (TextBox)e.Row.FindControl("txtStatus");

            if (status.Text == "Open")
            {
                e.Row.BackColor = System.Drawing.Color.Bisque;
            }
            if (status.Text == "Closed")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (status.Text == "Working")
            {
                e.Row.BackColor = System.Drawing.Color.LightCyan;
            }

        }
    }
    private void PostAdminComment()
    {
        try
        {
            string dt_Wrkd = txtDateWorked.Text;
            string dt_Clsd = txtClosed.Text;
            int i = 0;
            #region Post Admin Commnet
            foreach (GridViewRow gvr in gvRaisedTickets.Rows)
            {
                if (((CheckBox)gvr.FindControl("Chk")).Checked)
                {
                    try
                    {
                        con.Close();

                        Label ticket_Id = (Label)gvr.FindControl("lbtnTicketID");

                        TextBox comment = (TextBox)gvr.FindControl("txtComment");
                        TextBox status = (TextBox)gvr.FindControl("txtStatus");

                        string _command = "update Service_Requests_tbl set Comments='" + comment.Text + "', Date_Worked ='" + Convert.ToDateTime(dt_Wrkd) + "', Date_Closed ='" + Convert.ToDateTime(dt_Clsd) + "', Status='" + status.Text + "' where Ticket_Id='" + ticket_Id.Text + "'";
                        SqlCommand cmd = new SqlCommand(_command, con);
                        cmd.CommandType = CommandType.Text;

                        con.Open();
                        i = i + cmd.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            SendMsgToAdmin(ticket_Id.Text, status.Text);
                            MessageBox.Show(this, "Comments Updated Successfully.");
                            txtClosed.Text = txtDateWorked.Text = "";
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(this, "Error Occured in Updating Comments. Please Try Again");
                    }
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }

    private void SendMsgToAdmin(string ticket_ID, string status)
    {
        HR.SendSMS objsms = new HR.SendSMS();
        string msfEmp = "";
        if (status == "Working")
        {
            msfEmp = "Admin Started Working on ticket ID ," + ticket_ID + ". And will be Resolved Soon.";

        }
        else if (status == "Closed")
        {
            msfEmp = "Ticket " + ticket_ID + ", has been closed successfully.";
        }
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();

        objmas.EmployeeMaster_Select("50");
        string MD_MNo = objmas.AssignedMobileNo;

        objsms.Send_App_SMS(msfEmp, MD_MNo);
    }
    protected void btnPostComments_Click(object sender, EventArgs e)
    {
        PostAdminComment();

        gvRaisedTickets.DataBind();
    }

}