using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;

public partial class SendMail : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    protected string toEmail, EmailSubj, EmailMsg;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRead_Click(object sender, EventArgs e)
    {
        if (fileupload1.HasFile)
        {
            string Savepath = Server.MapPath("~/Uploaded/" + fileupload1.FileName);
            fileupload1.SaveAs(Savepath);

            string path = Savepath;
            if (!string.IsNullOrEmpty(path))
            {
                 OleDbConnection MyConnection = null;
                DataSet DtSet = null;
                OleDbDataAdapter MyCommand = null;
                MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties='Excel 12.0;IMEX=1'");


                MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
                DtSet = new System.Data.DataSet();
                MyCommand.Fill(DtSet, "[Sheet1$]");
                dt = DtSet.Tables[0];
             MyConnection.Close();


            if (dt.Rows.Count > 0)
            {
                bool found = false;

                foreach (DataRow dr in dt.Rows)
                {
                   toEmail=  dr.ItemArray[0].ToString();
                    EmailSubj = Convert.ToString(txtsub.Text);
                    EmailMsg = Convert.ToString(txtmsg.Text);
                    Email_Without_Attachment(toEmail, EmailSubj, EmailMsg);
                }
                MessageBox.Show(this, "Mail sent Successfully");
             }
               
            }
            
        }
        else
        {
            toEmail = txttomail.Text;
            EmailSubj = Convert.ToString(txtsub.Text);
            EmailMsg = Convert.ToString(txtmsg.Text);
            Email_Without_Attachment(toEmail, EmailSubj, EmailMsg);
            MessageBox.Show(this, "Mail sent Successfully");

        }

    }

    public static string Pass, FromEmailid, HostAdd;

    public static void Email_Without_Attachment(String ToEmail, String Subj, string Message)
    {
        //Reading sender Email credential from web.config file
        //HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
        FromEmailid = "acc@alumil.in";
        Pass = "alumil@2020";

        //creating the object of MailMessage
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(FromEmailid); //From Email Id
        mailMessage.Subject = Subj; //Subject of Email
        mailMessage.Body = Message; //body or message of Email
        mailMessage.IsBodyHtml = true;
        //Adding Multiple recipient email id logic
        string[] Multi = ToEmail.Split(','); //spiliting input Email id string with comma(,)
        foreach (string Multiemailid in Multi)
        {
            mailMessage.To.Add(new MailAddress(Multiemailid)); //adding multi reciver's Email Id
        }
        SmtpClient smtp = new SmtpClient(); // creating object of smptpclient
        //smtp.Host = HostAdd; //host of emailaddress for example smtp.gmail.com etc

        //network and security related credentials
        //smtp.EnableSsl = true;
        NetworkCredential NetworkCred = new NetworkCredential();
        NetworkCred.UserName = mailMessage.From.Address;
        NetworkCred.Password = Pass;
        smtp.UseDefaultCredentials = true;
        smtp.EnableSsl = true;
        smtp.Credentials = NetworkCred;
        smtp.Port = 587;
        smtp.Send(mailMessage); //sending Email
        
    }
}