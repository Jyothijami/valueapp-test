using System;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.Data.SqlClient;

using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using vllib;
public partial class Modules_HR_Emp_OfferLetter : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            setControlsVisibility();

            //FillCompany();
            FillDept();
            Masters.Designation.Designation_Select(ddlDesignation);
        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "86");

        gvEnrollmentDtls.Columns[10].Visible = up.Approve;
        gvEnrollmentDtls.Columns[11].Visible = up.Delete;
        btnSubmit.Enabled = up.add;
        btnOfferLetter.Enabled = up.Email;

    }

    #region FillCompany
    private void FillCompany()
    {
        try
        {

            Masters.Circular.Company_Select(ddlCompanyName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    #endregion

    private void FillDept()
    {
        try
        {
            Masters.Circular.Dept_Select(ddlDepartment);

        }
        catch
        {

        }
    }
    //protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox ChkBoxHeader = (CheckBox)gvEnrollmentDtls.HeaderRow.FindControl("chkhdr");
    //    foreach (GridViewRow row in gvEnrollmentDtls.Rows)
    //    {
    //        CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
    //        if (ChkBoxHeader.Checked == true)
    //        {
    //            ChkBoxRows.Checked = true;

    //        }
    //        else
    //        {
    //            ChkBoxRows.Checked = false;
    //        }
    //    }

    //}
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        tblEmployeeDetails.Visible = true;
    }

    private void OfferLetterApprove()
    {
        //foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        //{
        //    if (((CheckBox)gvr.FindControl("Chk")).Checked)
        //    {

        try
        {
            //string Name = (gvEnrollmentDtls.SelectedRow.FindControl("lblFirstName") as Label).Text;
            //string Salary = (gvEnrollmentDtls.SelectedRow.FindControl("lblGrossSalary") as Label).Text;
            //string Id = (gvEnrollmentDtls.SelectedRow.FindControl("lblNo") as Label).Text;

            //Label Name = (Label)gvr.FindControl("lblFirstName");
            //Label Mobile = (Label)gvr.FindControl("lblMobileNo");
            //Label Id = (Label)gvr.FindControl("lblNo");
            //Label Salary = (Label)gvr.FindControl("lblGrossSalary");
            HR.EmpLeave.InterviewSchedule obj = new HR.EmpLeave.InterviewSchedule();
            obj.InterviewStatus = "Approved";
            obj.GrossSalary = lblGrossSalary.Text;
            obj.OfferStatus = "Approved";
            obj.EnrollmentId = lblEnrollmentId.Text;
            MessageBox.Show(this, obj.InterviewApprove_Update());
        }

        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

        }
        //    }
        //}
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        OfferLetterReject();
        gvEnrollmentDtls.DataBind();
    }

    private void OfferLetterReject()
    {
        //foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        //{
        //    if (((CheckBox)gvr.FindControl("Chk")).Checked)
        //    {
        try
        {
            //string Name = (gvEnrollmentDtls.SelectedRow.FindControl("lblFirstName") as Label).Text;
            //string Salary = (gvEnrollmentDtls.SelectedRow.FindControl("lblGrossSalary") as Label).Text;
            //string Id = (gvEnrollmentDtls.SelectedRow.FindControl("lblNo") as Label).Text;

            //Label Id = (Label)gvr.FindControl("lblNo");
            //Label Salary = (Label)gvr.FindControl("lblGrossSalary");
            HR.EmpLeave.InterviewSchedule obj = new HR.EmpLeave.InterviewSchedule();
            obj.InterviewStatus = "Approved";
            obj.GrossSalary = lblGrossSalary.Text;
            obj.OfferStatus = "Rejected";
            obj.EnrollmentId = lblEnrollmentId.Text;
            MessageBox.Show(this, "Offer Rejected");
            gvEnrollmentDtls.DataBind();
            MessageBox.Show(this, obj.InterviewApprove_Update());
        }

        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

        }
        //    }

        //}

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetOfferLetter();
        //SendPDFEmail();
        // OfferLetterApprove();
        //SaveOfferDetails();
        gvEnrollmentDtls.DataBind();
        // tblEmployeeDetails.Visible = false;
        // SaveOfferLetter();
        //  OfferLetterSave();
        // txtcirNo.Text = Masters.Circular.Offer_AutoGenCode();

    }

    private void GetOfferLetter()
    {

        tblEmployeeDetails.Visible = true;
        //Label salary = (Label)gvr.FindControl("lblGrossSalary");
        //Label eName = (Label)gvr.FindControl("lblFirstName");
        //Label Email = (Label)gvr.FindControl("lblEmail");
        // string Name = (gvEnrollmentDtls.SelectedRow.FindControl("lblFirstName") as Label).Text;
        string companyName = ddlCompanyName.SelectedItem.Text;
        string designation = ddlDesignation.SelectedItem.Text;
        string department = ddlDepartment.SelectedItem.Text;
        // string grossSalary = (gvEnrollmentDtls.SelectedRow.FindControl("lblGrossSalary") as Label).Text;
        string dateOfJoin = Yantra.Classes.General.toMMDDYYYY(txtDOJ.Text.Trim());
        StringBuilder sb = new StringBuilder();


        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        sb.Append("<tr><td align='center' colspan = '3'><img height='40px' width='130px' src='" + Server.MapPath("../../Content/CompanyProfileImgs/1.jpg") + "'/></td></tr>");
        sb.Append("<tr><td colspan = '3' align='left'><b>Date: </b>");
        sb.Append(DateTime.Now);
        sb.Append(" </td></tr>");
        sb.Append("<tr><td colspan = '3' align='left'>");
        sb.Append(lblName.Text);
        sb.Append("</td></tr>");
        //sb.Append("<tr><td colspan = '3' align='left'>390-1/7, Friends Colony </td></tr>");
        //sb.Append("<tr><td colspan = '3' align='left'>Chanda Nagar</td></tr>");
        //sb.Append("<tr><td colspan = '3' align='left'>Hyderabad 500018 </td></tr>");
        sb.Append("<tr><td colspan = '3' align='left'>");
        sb.Append(lblAddress1.Text);
        sb.Append("</td></tr>");
        sb.Append("<br />");

        sb.Append("<tr><td colspan = '3' align='left'>Dear ");
        sb.Append(lblName.Text);
        sb.Append(" </td></tr>");
        sb.Append("<tr><td colspan = '3' align='left'><p style='text-indent: 5em;'>With reference to the discussions that we had with you, we are pleased to appoint you as <b>");
        sb.Append(designation);
        sb.Append("</b> in <b>");
        sb.Append(companyName);
        sb.Append("</b>. Your Place of Posting will be <b> Hyderabad.</b><p> </td></tr>");
        //sb.Append("<br />");

        sb.Append("<tr><td colspan = '3' align='left'><p style=' text-indent: 5em;'>Your Annual Total Compensation will be Rs. <b>");
        sb.Append(lblGrossSalary.Text);
        sb.Append("</b>.This amount may vary, depending on ValueLine’s performance and your performance. The break-up is presented in Annexure A. ");
        sb.Append("We would like to inform you that Value Line has considered your experience as relevant, that would be updated in our records.<p> </td></tr>");
        sb.Append("<br />");

        sb.Append("<tr><td colspan = '3' align='left'><p>We request you to join us on or before  <b>");
        sb.Append(dateOfJoin);
        sb.Append("</b>. At the time of joining, please submit the following documents:");
        sb.Append("<br />");
        sb.Append("1. Photocopy of your passport, certificates and mark sheets in support of your educational qualifications.");
        sb.Append("<br />");
        sb.Append("2. Relieving letter from all your previous employer and last drawn pay slip, if applicable.");
        sb.Append("<br />");
        sb.Append("3. Ten passport size color photographs.</p></td></tr>");
        sb.Append("<br />");

        sb.Append("<tr><td colspan = '3' align='left'><p>We look forward to you joining us. Please do not hesitate to call us for any information you may need.");
        sb.Append("Also, sign the duplicate of this offer as your acceptance and forward the same to us. </p> </td></tr>");
        sb.Append("<br />");
        sb.Append("<tr><td colspan = '3' align='left'><p>Yours sincerely,</p></td></tr>");
        sb.Append("<br />");


        sb.Append("<tr><td colspan = '3' align='left'><p>for <b>");
        sb.Append(companyName);
        sb.Append(".</b></p></td></tr>");
        sb.Append("<br />");



        //sb.Append("<tr><td colspan = '3'  style='padding:50px' align='left'><p>  </p></td></tr>");
        sb.Append("<tr><td colspan = '3' align='left'><p>VP- Human Resources</p></td></tr>");
        sb.Append("<tr><td colspan = '3' align='left'><p>I accept the offer on the terms and conditions and shall report to work on ........</p></td></tr>");
        sb.Append("<tr><td>Signature :</td><td></td><td>Date :</td></tr>");


        sb.Append("</table>");
        StringReader sr = new StringReader(sb.ToString());
        Editor1.Content = sb.ToString();
    }

    private void SaveOfferDetails()
    {
        //foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        //{
        //    if (((CheckBox)gvr.FindControl("Chk")).Checked)
        //    {
        //        Label EnrollId = (Label)gvr.FindControl("lblNo");
        try
        {
            //string EnrollId = (gvEnrollmentDtls.SelectedRow.FindControl("lblNo") as Label).Text;

            SqlCommand cmd = new SqlCommand("USP_SaveOfferDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnrollmentId", lblEnrollmentId.Text);
            cmd.Parameters.AddWithValue("@CompanyName", ddlCompanyName.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Designation", ddlDesignation.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@DOJ", Yantra.Classes.General.toMMDDYYYY(txtDOJ.Text));
            cmd.Parameters.AddWithValue("@OfferStatus", "Pending");
            cmd.Parameters.AddWithValue("@CompanyId", ddlCompanyName.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DeptId", ddlDepartment.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@DesignId", ddlDesignation.SelectedItem.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        //    }
        //}
    }

    private void SendPDFEmail()
    {

        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        //        {
        //            if (((CheckBox)gvr.FindControl("Chk")).Checked)
        //            {
        //                Label salary = (Label)gvr.FindControl("lblGrossSalary");
        //                Label eName = (Label)gvr.FindControl("lblFirstName");
        //                Label Email=(Label)gvr.FindControl("lblEmail");
        //                string Name = eName.Text;
        //                string companyName = ddlCompanyName.SelectedItem.Text;
        //                string designation = txtDesignation.Text;
        //                string department = ddlDepartment.SelectedItem.Text;
        //                string grossSalary = salary.Text;
        //                string dateOfJoin =Yantra.Classes.General.toMMDDYYYY(txtDOJ.Text.Trim());
        //                StringBuilder sb = new StringBuilder();


        //                sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        //                sb.Append("<tr><td align='center' colspan = '3'><img src='" + Server.MapPath("../../BAT_files/ValueLine.gif") + "'/></td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'><b>Date: </b>");
        //                sb.Append(DateTime.Now);
        //                sb.Append(" </td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'>");
        //                sb.Append(Name);
        //                sb.Append("</td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'>390-1/7, Friends Colony </td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'>Chanda Nagar</td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'>Hyderabad 500018 </td></tr>");
        //                sb.Append("<br />");

        //                sb.Append("<tr><td colspan = '3' align='left'>Dear ");
        //                sb.Append(Name);
        //                sb.Append(" </td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'><p>With reference to the discussions that we had with you, we are pleased to appoint you as <b>");
        //                sb.Append(designation);
        //                sb.Append("</b> in <b>");
        //                sb.Append(companyName);
        //                sb.Append("</b>. Your Place of Posting will be <b> Hyderabad.</b><p> </td></tr>");
        //                sb.Append("<br />");

        //                sb.Append("<tr><td colspan = '3' align='left'><p>Your Annual Total Compensation will be Rs. <b>");
        //                sb.Append(grossSalary);
        //                sb.Append("</b>.This amount may vary, depending on ValueLine’s performance and your performance. The break-up is presented in Annexure A. ");
        //                sb.Append("We would like to inform you that Value Line has considered your experience as relevant, that would be updated in our records.<p> </td></tr>");
        //                sb.Append("<br />");

        //                sb.Append("<tr><td colspan = '3' align='left'><p>We request you to join us on or before  <b>");
        //                sb.Append(dateOfJoin);
        //                sb.Append("</b>. At the time of joining, please submit the following documents:");
        //                sb.Append("<br />");
        //                sb.Append("1. Photocopy of your passport, certificates and mark sheets in support of your educational qualifications.");
        //                sb.Append("<br />");
        //                sb.Append("2. Relieving letter from all your previous employer and last drawn pay slip, if applicable.");
        //                sb.Append("<br />");
        //                sb.Append("3. Ten passport size color photographs.</p></td></tr>");
        //                sb.Append("<br />");

        //                sb.Append("<tr><td colspan = '3' align='left'><p>We look forward to you joining us. Please do not hesitate to call us for any information you may need.");
        //                sb.Append("Also, sign the duplicate of this offer as your acceptance and forward the same to us. </p> </td></tr>");
        //                sb.Append("<br />");
        //                sb.Append("<tr><td colspan = '3' align='left'><p>Yours sincerely,</p></td></tr>");
        //                sb.Append("<br />");


        //                sb.Append("<tr><td colspan = '3' align='left'><p>for <b>");
        //                sb.Append(companyName);
        //                sb.Append(".</b></p></td></tr>");
        //                sb.Append("<br />");



        //                sb.Append("<tr><td colspan = '3'  style='padding:50px' align='left'><p>Prd</p></td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'><p>VP- Human Resources</p></td></tr>");
        //                sb.Append("<tr><td colspan = '3' align='left'><p>I accept the offer on the terms and conditions and shall report to work on ........</p></td></tr>");
        //                sb.Append("<tr><td>Signature :</td><td></td><td>Date :</td></tr>");


        //                sb.Append("</table>");
        //                StringReader sr = new StringReader(sb.ToString());
        //                Editor1.Content = sb.ToString();
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();
        //        MailMessage mm = new MailMessage("pramodbmk@gmail.com", Email.Text);
        //        //MailMessage mm = new MailMessage("pramodbmk@gmail.com", "codegear1046@gmail.com");
        //        mm.Subject = "Offer Letter";
        //        mm.Body = "Value Line Trade Offer Letter";
        //        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "OfferLetter.pdf"));
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential();
        //        NetworkCred.UserName = "pramodbmk@gmail.com";
        //        NetworkCred.Password = "bommakal";
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mm);
        //    }
        //            }
        //        }
        //    }
        //}
    }
    protected void gvEnrollmentDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        //  GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        GridViewRow row = gvEnrollmentDtls.Rows[index];
        //
        lblEnrollmentId.Text = ((Label)row.FindControl("lblNo")).Text;
        lblGrossSalary.Text = ((Label)row.FindControl("lblGrossSalary")).Text;
        lblName.Text = ((Label)row.FindControl("lblFirstName")).Text;
        lblEmailId.Text = ((Label)row.FindControl("lblEmail")).Text;

        lblAddress1.Text = ((Label)row.FindControl("lblAddress")).Text;

        //lblEnrollmentId.Text = gvEnrollmentDtls.Rows[row].Cells[0].Text;
        //lblEnrollmentId.Text = e.Row.Cells[0].Text;
        //lblGrossSalary.Text = gvEnrollmentDtls.Rows[row].Cells[9].Text;
        //lblName.Text = gvEnrollmentDtls.Rows[row].Cells[1].Text;
        //lblEmailId.Text = gvEnrollmentDtls.Rows[row].Cells[4].Text;


        //lblName.Text = (gvEnrollmentDtls.SelectedRow.FindControl("lblFirstName") as Label).Text;
        //lblGrossSalary.Text = (gvEnrollmentDtls.SelectedRow.FindControl("lblGrossSalary") as Label).Text;
        //lblEnrollmentId.Text = (gvEnrollmentDtls.SelectedRow.FindControl("lblNo") as Label).Text;
        //lblEmailId.Text = (gvEnrollmentDtls.SelectedRow.FindControl("lblEmail") as Label).Text;


        if (e.CommandName == "Approve")
        {
            tblEmployeeDetails.Visible = true;
        }
        else if (e.CommandName == "Reject")
        {
            OfferLetterReject();
            gvEnrollmentDtls.DataBind();
        }

    }
    protected void btnOfferLetter_Click(object sender, EventArgs e)
    {
        SendEmail();
        OfferLetterApprove();
        SaveOfferDetails();
        gvEnrollmentDtls.DataBind();
        tblEmployeeDetails.Visible = false;
        gvHistory.DataBind();
    }

    private void SendEmail()
    {
        // string EmailId = (gvEnrollmentDtls.SelectedRow.FindControl("lblEmail") as Label).Text;

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();
            StringReader sr = new StringReader(Editor1.Content.ToString());
            htmlparser.Parse(sr);
            pdfDoc.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            MailMessage mm = new MailMessage("valuelineinfo@gmail.com", lblEmailId.Text);
            //MailMessage mm = new MailMessage("pramodbmk@gmail.com", "codegear1046@gmail.com");
            mm.Subject = "Offer Letter";
            mm.Body = "Value Line Trade Offer Letter";
            mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "OfferLetter.pdf"));
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "valuelineinfo@gmail.com";
            NetworkCred.Password = "Valuelinehyd";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }

    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden2.Text = ddlSearchBy.SelectedItem.Text;
        lblSearchValueHidden2.Text = txtSearchText.Text;
        gvHistory.DataBind();
    }
}