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
using System.Data.SqlClient;
using Yantra.MessageBox;
using YantraDAL;


public partial class Custom_Controls_ChangePassword : System.Web.UI.UserControl
{

    SqlConnection Con = new SqlConnection(DBConString.ConnectionString());
    SqlCommand Cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
        }

        //Ocrm.Classes.User.OcrmUserId = (string[])Ocrm.Classes.User.OCRMSession;
        //if (Ocrm.Classes.User.OcrmUserId == null)
        //{
        //    Response.Redirect("../Default.aspx");
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtNewPass.Text = "";
        txtOldPassword.Text = "";
        txtRetypeNewpassword.Text = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Con.Open();
            Cmd.Connection = Con;
            Cmd.Parameters.Clear();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@User_Name", Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName));
            Cmd.Parameters.AddWithValue("@Old_Password", txtOldPassword.Text);
            Cmd.Parameters.AddWithValue("@New_Password", txtRetypeNewpassword.Text);
            Cmd.Parameters.AddWithValue("@Current_Password", "");

            Cmd.CommandText = "sp_ChangePassword";

            SqlParameter RetValue = Cmd.Parameters.Add("@Ret", SqlDbType.Int);
            RetValue.Direction = ParameterDirection.ReturnValue;
            Cmd.ExecuteScalar();

            if (RetValue.SqlValue.ToString() == "1")
            {
                MessageBox.Show(this, "Password has been Changed Successfully!");

            }
            else if (RetValue.SqlValue.ToString() == "0")
            {
                MessageBox.Show(this, " Please enter Correct Password ");

            }
            Con.Close();

            //Mail_Send();
            txtNewPass.Text = "";
            txtOldPassword.Text = "";
            txtRetypeNewpassword.Text = "";


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());

        }
    }


    //private void Mail_Send()
    //{

    //    Yantra.Classes.Email mail = new Ocrm.Classes.Email(Ocrm.Classes.User.OcrmUserId[2].ToString());
    //    //for defining  the format
    //    mail.BodyFormat = System.Web.Mail.MailFormat.Html;
    //    // reading  the formated htmx  format file :
    //    System.IO.StreamReader sr = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Mail_Formats\\PasswordReminder.htmx");
    //    mail.Body = sr.ReadToEnd();

    //    sr.Close();
    //    // to send to htmx file
    //    //mail.Body = mail.Body.Replace("_UserTitle_", u.FullTitle);
    //    mail.Body = mail.Body.Replace("_UserTitle_", Ocrm.Classes.User.OcrmUserId[1].ToString());
    //    mail.Body = mail.Body.Replace("_Email_", Ocrm.Classes.User.OcrmUserId[2].ToString());
    //    mail.Body = mail.Body.Replace("_pass_", txtRetypeNewpassword.Text);
    //    mail.Subject = "Your password changed Successfully";
    //    mail.From = "Ocrm@Dsssolutions.com";


    //    try
    //    {
    //        mail.Send();
    //        MessageBox.Show(this, "Your password changed Successfully");
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, "Could not send your password. please try again");
    //    }
    //}

}
