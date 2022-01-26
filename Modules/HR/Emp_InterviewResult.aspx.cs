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
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using vllib;

public partial class Modules_HR_Emp_InterviewResult : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int i,j;
    string email = "";
    string age = "34";
    double Basic1 = 0;
    double Basic2 = 0;
    double Hra1 = 0;
    double Hra2 = 0;

    double ConAll1 = 0;
    double ConAll2 = 0;

    double Medexp1 = 0;
    double Medexp2 = 0;
    

    double Other1 = 0;
    double Other2 = 0;
    double GrossTotal1 = 0;
    double GrossTotal2 = 0;
    double Pf1 = 0;
    double Pf2 = 0;
    double Medi1 = 0;
    double Medi2 = 0;

    double esi1 = 0;
    double esi2 = 0;
    double pt1 = 0;
    double pt2 = 0;
    double ttlB1 = 0;
    double ttlB2 = 0;
    double net1 = 0;
    double net2 = 0;

    double Epf1 = 0;
    double Epf2 = 0;
    double Eesi1 = 0;
    double Eesi2 = 0;
    double EMed1 = 0;
    double EMed2 = 0;
    double Bouns1 = 0;
    double Bouns2 = 0;
    double EcTotal1 = 0;
    double EcTotal2 = 0;

    double a = 0;

    double Ptax = 0;
    double EdTotal = 0;

    double CtcPm = 0;
    double CtcPa = 0;

    double NetPay = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            //HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpName);
            BindAge();

        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "86");

        btnAccept.Enabled = up.Approve;
        btnReject.Enabled = up.Delete;
        btnSubmit.Enabled = up.Email;


    }
    private void BindAge()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                    Label Id = (Label)gvr.FindControl("lblNo");
                Label a=(Label)gvr.FindControl("lblAge");
                    //SqlCommand cmd = new SqlCommand("USP_BindAge", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@EnrollmentId", Id.Text);
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //age = a.Text = dt.ToString();
                age = a.Text;
                age = "34";
                }
           
        }
}


    
    public long DateDiff(System.DateTime StartDate, System.DateTime EndDate)
    {
        long lngDateDiffValue = 0;
        System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
        lngDateDiffValue = (long)(TS.Days / 365);
        return (lngDateDiffValue);
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
                Label mail=(Label)row.FindControl("lblEmail");
                email = mail.Text;
                btnAccept.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }

    
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        Details.Visible = true;
        tblNet.Visible = true;

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
         i = 0;
        InterviewReject();
        gvHistory.DataBind();
        gvEnrollmentDtls.DataBind();
    }
    private void InterviewReject()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label EnrollId = (Label)gvr.FindControl("lblNo");
                    SqlCommand cmd = new SqlCommand("USP_UpdateInterviewStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EnrollmentId", EnrollId.Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@InterviewStatus", "Accepted");
                        cmd.Parameters.AddWithValue("@GrossSalary", txtGrossAmount.Text);


                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@InterviewStatus", "Reject");
                        cmd.Parameters.AddWithValue("@GrossSalary", "");


                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //HR.EmpLeave.InterviewSchedule obj = new HR.EmpLeave.InterviewSchedule();
                    //obj.InterviewStatus = "Rejected";
                    //obj.GrossSalary = "";
                    ////obj.CompanyName = "";
                    ////obj.DepartmentName = "";
                    ////obj.DesignationName = "";
                    ////obj.DateOfJoining = "";
                    //obj.EnrollmentId = EnrollId.Text;
                    //MessageBox.Show(this, obj.InterviewApprove_Update());
                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                }
            }

        }
        
    }

    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        i = 1;
        InterviewReject();
       SendSalaryBreakUp();
        //InterviewAccept();
        gvEnrollmentDtls.DataBind();
        HR.ClearControls(this);
        Details.Visible = false;
        gvHistory.DataBind();
    }

    private void InterviewAccept()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label Id = (Label)gvr.FindControl("lblNo");
                     HR.EmpLeave.InterviewSchedule obj = new HR.EmpLeave.InterviewSchedule();
                    obj.InterviewStatus = "Accepted";
                    obj.GrossSalary=txtGrossAmount.Text;
                    obj.OfferStatus = "Pending";
                    //obj.CompanyName = "";
                    //obj.DepartmentName = "";
                    //obj.DesignationName = "";
                    //obj.DateOfJoining = "";
                    obj.EnrollmentId = Id.Text;
                    MessageBox.Show(this, obj.InterviewApprove_Update());
                    gvHistory.DataBind();
                    gvEnrollmentDtls.DataBind();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                }
            }

        }
    }

    private void SendSalaryBreakUp()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                Label email = (Label)gvr.FindControl("lblEmail");

                StreamReader reader = new StreamReader(Server.MapPath("~/Modules/HR/SalaryBreakuptemplate.html"));
                string readFile = reader.ReadToEnd();
                string myString = "";
                myString = readFile;
                myString = myString.Replace("$$Basic$$", txtBasic1.Text);
                myString = myString.Replace("$$YBasic$$", txtBasic2.Text);
                myString = myString.Replace("$$HRA$$", txtHRA1.Text);
                myString = myString.Replace("$$YHRA$$", txtHRA2.Text);
                myString = myString.Replace("$$TransportAllowance$$", txtOther1.Text);
                myString = myString.Replace("$$YTransportAllowance$$", txtOther2.Text);
                myString = myString.Replace("$$SpecialAllowance$$", txtSpl1.Text);
                myString = myString.Replace("$$YSpecialAllowance$$", txtSpl2.Text);
                myString = myString.Replace("$$Gross$$", txtGrossTotal1.Text);
                myString = myString.Replace("$$YGross$$", txtGrossTotal2.Text);


                myString = myString.Replace("$$PF$$", txtPF1.Text);
                myString = myString.Replace("$$YPF$$", txtPF2.Text);
                myString = myString.Replace("$$ESI$$", txtMedi1.Text);
                myString = myString.Replace("$$YESI$$", txtMedi2.Text);
                myString = myString.Replace("$$PerformanceIncentives$$", txtBonus1.Text);
                myString = myString.Replace("$$YPerformanceIncentives$$", txtBonus2.Text);
                myString = myString.Replace("$$Total$$", txtECTotal1.Text);
                myString = myString.Replace("$$YTotal$$", txtECTotal2.Text);

                myString = myString.Replace("$$OtherBenefits$$", txtoth1.Text);
                myString = myString.Replace("$$YOtherBenefits$$", txtoth2.Text);

                //myString = myString.Replace("$$Ptax$$", txtEDptax.Text);
                //myString = myString.Replace("$$Total1$$", txtECTotal1.Text);

                myString = myString.Replace("$$CTCPM$$", txtCTCPM.Text);
                myString = myString.Replace("$$CTCPA$$", txtCTCPA.Text);
                //myString = myString.Replace("$$NET$$", txtNetSal.Text);

                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    PdfWriter.GetInstance(myString, memoryStream);
                //    byte [] bytes=new memoryStream.ToArray();
                //    memoryStream.Close();
                    System.Net.Mail.MailMessage mymailmessage = new System.Net.Mail.MailMessage();
                    mymailmessage.Subject = "Salary Details from ValueLine Trade Pvt Ltd.";
                    mymailmessage.Body = myString.ToString();
                    //mymailmessage.Body = "Hi";
                    //mymailmessage.Attachments.Add(new Attachment(myString.ToString()));
                    mymailmessage.IsBodyHtml = true;

                    mymailmessage.From = new MailAddress("valuelineinfo@gmail.com");
                    mymailmessage.To.Add(email.Text);
                    System.Net.NetworkCredential mymailauthentications = new System.Net.NetworkCredential("valuelineinfo@gmail.com", "Valuelinehyd");

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

   
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden2.Text = ddlSearchBy.SelectedItem.Text;
        lblSearchValueHidden2.Text = txtSearchText.Text;
        gvHistory.DataBind();
    }

   
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvEnrollmentDtls.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxRows.Checked == true)
            {
                btnAccept.Visible = true;
                btnReject.Visible = true;
            }
        }
    }
    protected void btnCal_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[Salary_Breakup]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fsal", txtGrossAmount.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblStatus.Text = dt.Rows[0][15].ToString();
            txtBasic2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][0])).ToString();
            txtHRA2.Text = dt.Rows[0][1].ToString();
            txtOther2.Text = dt.Rows[0][2].ToString();
            txtBonus2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][4])).ToString();
            txtPF2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][3])).ToString();
            txtCTCPA.Text = dt.Rows[0][14].ToString();
            txtoth2.Text = "0";


            txtBasic1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][0]) / 12).ToString();
            txtHRA1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][1]) / 12).ToString();
            txtOther1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][2]) / 12).ToString();
            txtBonus1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][4]) / 12).ToString();
            txtPF1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][3]) / 12).ToString();
            txtCTCPM.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][14]) / 12).ToString();
            txtoth1.Text = "0";

            if (lblStatus.Text == "ESI_is_Applicable")
            {
                txtSpl2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][7])).ToString();
                txtGrossTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][9])).ToString();
                txtECTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][11])).ToString();
                txtMedi2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][10])).ToString();

                txtSpl1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][7]) / 12).ToString();
                txtGrossTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][9]) / 12).ToString();
                txtECTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][11]) / 12).ToString();
                txtMedi1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][10]) / 12).ToString();

            }
            else
            {
                txtSpl2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][6])).ToString();
                txtGrossTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][8])).ToString();
                txtECTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][12])).ToString();
                txtMedi2.Text = "0";

                txtSpl1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][6]) / 12).ToString();
                txtGrossTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][8]) / 12).ToString();
                txtECTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][12]) / 12).ToString();
                txtMedi1.Text = "0";
            }
        }
    }

    private void InterviewStatus1()
    {
        foreach (GridViewRow gvr in gv1st.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk1")).Checked)
            {
                try
                {
                    Label EnrollId = (Label)gvr.FindControl("lblNo");
                    SqlCommand cmd = new SqlCommand("USP_UpdateInterviewStatus1", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EnrollmentId", EnrollId.Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@InterviewStatus", "Accepted");
                        cmd.Parameters.AddWithValue("@InterviewType", "0");

                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@InterviewStatus", "Reject");
                        cmd.Parameters.AddWithValue("@InterviewType", "0");


                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                }
            }

        }

    }
    protected void btnAccept1_Click(object sender, EventArgs e)
    {
        j = 1;
        InterviewStatus1();
        gvEnrollmentDtls.DataBind();
        gv1st.DataBind();
    }
    protected void btnReject2_Click(object sender, EventArgs e)
    {
        j = 0;
        InterviewStatus1();
        gv1st.DataBind();
        gvEnrollmentDtls.DataBind();
    }
    protected void chkhdr1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvEnrollmentDtls.HeaderRow.FindControl("chkhdr1");
        foreach (GridViewRow row in gvEnrollmentDtls.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk1");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
                Label mail = (Label)row.FindControl("lblEmail");
                email = mail.Text;
                btnAccept1.Visible = true;
                btnReject1.Visible = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
                btnAccept1.Visible = false;
                btnReject1.Visible = false;
            }
        }
    }
    protected void Chk1_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv1st.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk1");
            if (ChkBoxRows.Checked == true)
            {
                btnAccept1.Visible = true;
                btnReject1.Visible = true;
            }
        }
    }
}