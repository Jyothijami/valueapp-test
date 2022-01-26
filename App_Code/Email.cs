using System;
using System.Threading;
using System.Net.Mail;
using System.Web;

namespace Yantra.Classes
{
    /// <summary>
    /// Summary description for Email.
    /// </summary>
    public class Email : System.Web.Mail.MailMessage
    {
        /*
        public Email(string To, bool SendHr)
        {
            this.Cc = "DssSolutions<hr@dsssolutions.com>";
            Email(To);
        }*/

        public Email(string To)
        {
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com");
            //this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "marri9494@gmail.com");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "9494785464");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");

            this.From = "Phani";
            this.To = To;
        }

        public Email(string To, string Cc, string From)
        {
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com");
          //  this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "webmail.ninfosoft.com");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25");
            //this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "ocrm.dssinfosys@gmail.com");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "valuelineinfo@gmail.com");
            //this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword","dssocrm123");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "Valuelinehyd");
            //this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
            this.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");

            this.From = From;
            this.To = To;
            this.Cc = Cc;
        }

        public void Send()
        {
            // open new thread and send the mail using that.
            Thread t = new Thread(new ThreadStart(this.SendEmail));
            t.Start();
        }

        private void SendEmail()
        {
            try
            {
                System.Web.Mail.SmtpMail.Send(this);
            }
            catch (Exception)
            {
            } // error
        }

        private string getPassword(string Email)
        {
            return HR.EmployeeMaster.GetEmailPass(Email);
        }
        
        /*
           public bool SendTestEmail(string ToEmailId, string FromEmail, string CcEmailIds, string MailBody, string BodyFormat, string Subject)
          {
              try
              {
                  if( ToEmailId.Length > 0)
                  {
                      MailMessage objMail = new MailMessage();

                      //Assigning the From E-Mail address
                      objMail.From = FromEmail;
                      //Assigning the to E-Mail addres
                      objMail.To = ToEmailId;
                      //Assigning the CC address for the Mail
                      objMail.Cc = CcEmailIds;
                      //Assigning the Subject for the Mail
                      objMail.Subject = Subject;
                      //Assigning the body text for the Mail
                      objMail.Body = ( BodyFormat.Trim().ToUpper() == "HTML" ? MailBody.Replace("\r\n","<br>") : MailBody );
                      //Setting the Body format for the Mail body
                      objMail.BodyFormat = ( BodyFormat.Trim().ToUpper() == "HTML" ? MailFormat.Html : MailFormat.Text );
				
                      //set SMTP Server
                      SmtpMail.SmtpServer = "98.131.2.251";

                      //Sending the E-mail to the assigned Addresses
                      SmtpMail.Send(objMail);

                      return true;
                  }
                  else
                  {
                      return false;
                  }				
              }
              catch( Exception ex )
              {
                  if( ex != null ){}
                  return false;
              }
          }
          */
    }
}
