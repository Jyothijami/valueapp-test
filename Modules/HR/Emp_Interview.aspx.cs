using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DatumDAL;
using Yantra.MessageBox;
using System.Net.NetworkInformation;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.IO;
using vllib;

using System.Data.SqlClient;
public partial class Modules_HR_Emp_Interview : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            gvEnrollmentDtls.DataBind();
            lblcpid.Text = cp.getPresentCompanySessionValue();
            location_fill();
        }
    }

    private void location_fill()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        SqlCommand cmd = new SqlCommand("select CP_ADDRESS, CP_EMAIL from YANTRA_COMP_PROFILE where CP_ID=@CP_ID ORDER BY CP_FULL_NAME ", con);
        cmd.Parameters.AddWithValue("@CP_ID", lblcpid.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        txtLocation.Text = dt.Rows[0][0].ToString();
        TextBox4.Text = txtLocation.Text;
        //lblEmail.Text = dt.Rows[0][1].ToString();
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "86");

        btnSubmit.Enabled = up.Email;


    }
    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox ChkBoxHeader = (CheckBox)gvEnrollmentDtls.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvEnrollmentDtls.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                tblEmployeeDetails.Visible = true;

            }
            else
            {
                ChkBoxRows.Checked = false;
                tblEmployeeDetails.Visible = false;
            }
        }
    }

    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                tblEmployeeDetails.Visible = true;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        EmployeeInterviewSchedule_Save();
        SendMailMsg();
        gvEnrollmentDtls.DataBind();
        gvIntRes.DataBind();
        tblEmployeeDetails.Visible = false;
        HR.ClearControls(this);
        GridView1.DataBind();
    }

    private void SendMailMsg()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                Label email = (Label)gvr.FindControl("lblEmail");
                StreamReader reader = new StreamReader(Server.MapPath("~/Modules/HR/MailTemplate.html"));
                string readFile = reader.ReadToEnd();
                string myString = "";
                myString = readFile;
                myString = myString.Replace("$$Location$$", txtLocation.Text);
                myString = myString.Replace("$$InterviewDate$$", txtDate.Text);
                myString = myString.Replace("$$Time$$", txtTime.Text);
                myString = myString.Replace("$$Name$$", txtInterviewerName.Text);
                myString = myString.Replace("$$Remarks$$", txtRemarsks.Text);

                System.Net.Mail.MailMessage mymailmessage = new System.Net.Mail.MailMessage();
                mymailmessage.Subject = "Interview call letter from ValueLine Trade Pvt Ltd.";
                mymailmessage.Body = myString.ToString();
                mymailmessage.IsBodyHtml = true;
                mymailmessage.From = new MailAddress("jyothi0231anu@gmail.com");
                mymailmessage.To.Add(email.Text);
                System.Net.NetworkCredential mymailauthentications = new System.Net.NetworkCredential("jyothi0231anu@gmail.com", "9M78S101");

                System.Net.Mail.SmtpClient mailclient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

                mailclient.EnableSsl = true;
                mailclient.UseDefaultCredentials = true;
                mailclient.Credentials = mymailauthentications;
                mymailmessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mymailmessage.Headers.Add("Disposition-Notification-To", email.Text);
                mailclient.Send(mymailmessage);
                reader.Dispose();
            }
        }

    }

    private void SendMailMsg2()
    {
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk2")).Checked)
            {
                Label email = (Label)gvr.FindControl("lblEmail2");
                StreamReader reader = new StreamReader(Server.MapPath("~/Modules/HR/MailTemplate.html"));
                string readFile = reader.ReadToEnd();
                string myString = "";
                myString = readFile;
                myString = myString.Replace("$$Location$$", TextBox4.Text);
                myString = myString.Replace("$$InterviewDate$$", TextBox1.Text);
                myString = myString.Replace("$$Time$$", TextBox2.Text);
                myString = myString.Replace("$$Name$$", TextBox3.Text);
                myString = myString.Replace("$$Remarks$$", TextBox5.Text);


                System.Net.Mail.MailMessage mymailmessage = new System.Net.Mail.MailMessage();
                mymailmessage.Subject = "Interview call letter from ValueLine Trade Pvt Ltd.";
                mymailmessage.Body = myString.ToString();
                mymailmessage.IsBodyHtml = true;
                mymailmessage.From = new MailAddress("jyothi0231anu@gmail.com");
                mymailmessage.To.Add(email.Text);
                System.Net.NetworkCredential mymailauthentications = new System.Net.NetworkCredential("jyothi0231anu@gmail.com", "9M78S101");

                System.Net.Mail.SmtpClient mailclient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

                mailclient.EnableSsl = true;
                mailclient.UseDefaultCredentials = true;
                mailclient.Credentials = mymailauthentications;
                mymailmessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mymailmessage.Headers.Add("Disposition-Notification-To", email.Text);
                mailclient.Send(mymailmessage);
                reader.Dispose();
            }
        }

    }

    private void EmployeeInterviewSchedule_Save()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {

                try
                {
                    Label Id = (Label)gvr.FindControl("lblNo");
                    HR.EmpLeave.InterviewSchedule obj = new HR.EmpLeave.InterviewSchedule();
                    obj.EnrollmentId = Id.Text;
                    obj.InterviewerName = txtInterviewerName.Text;
                    obj.DateOfInterview = Yantra.Classes.General.toMMDDYYYY(txtDate.Text.Trim());
                    obj.TimeOfInterview = txtTime.Text;
                    obj.Location = txtLocation.Text;
                    obj.InterviewStatus = "Pending";
                    obj.GrossSalary = "0.00";
                    obj.OfferStatus = "";
                    obj.InterviewType = "1";
                    obj.Remarks = txtRemarsks.Text;
                    obj.InterviewStatus1 = "Pending";

                    //obj.CompanyName = "";
                    //obj.DepartmentName = "";
                    //obj.DesignationName = "";
                    //obj.DateOfJoining = "";
                    MessageBox.Show(this, obj.InterviewSchedule_Save());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                    HR.Dispose();
                }
            }
        }
    }

    private void EmployeeInterviewSchedule_Save2()
    {
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk2")).Checked)
            {

                try
                {
                    Label Id = (Label)gvr.FindControl("lblNo2");
                    HR.EmpLeave.InterviewSchedule obj = new HR.EmpLeave.InterviewSchedule();
                    obj.EnrollmentId = Id.Text;
                    obj.DateOfInterview = Yantra.Classes.General.toMMDDYYYY(TextBox1.Text.Trim());
                    obj.TimeOfInterview = TextBox2.Text;
                    obj.InterviewerName = TextBox3.Text;
                    obj.Location = TextBox4.Text;
                    obj.InterviewStatus = "Pending";
                    obj.GrossSalary = "0.00";
                    obj.OfferStatus = "";
                    obj.InterviewType = "2";
                    obj.Remarks = TextBox5.Text;
                    obj.InterviewStatus1 = "Approved";

                    MessageBox.Show(this, obj.InterviewSchedule_Update());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                    HR.Dispose();
                }
            }
        }
    }


    //protected void ddlStatusSearch_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void btnSearchGo_Click(object sender, EventArgs e)
    //{
    //    lblSearchItemHidden.Text = ddlStatusSearch.SelectedValue;
    //    lblSearchValueHidden.Text = txtSearchText.Text;
    //    gvIntRes.DataBind();
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        EmployeeInterviewSchedule_Save2();
        SendMailMsg2();
        GridView1.DataBind();
        GridView1.DataBind();
        Table2.Visible = false;
        HR.ClearControls(this);
        gvEnrollmentDtls.DataBind();
        gvEnrollmentDtls.DataBind();
    }

    protected void Chk2_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk2")).Checked)
            {
                Table2.Visible = true;
            }
        }
    }

    protected void chkhdr2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader2 = (CheckBox)GridView1.HeaderRow.FindControl("chkhdr2");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows2 = (CheckBox)row.FindControl("Chk2");
            if (ChkBoxHeader2.Checked == true)
            {
                ChkBoxRows2.Checked = true;
                Table2.Visible = true;

            }
            else
            {
                ChkBoxRows2.Checked = false;
                Table2.Visible = false;
            }
        }
    }
}