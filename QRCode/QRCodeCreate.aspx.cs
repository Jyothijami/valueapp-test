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
using System.Net;
using WhatsAppApi;


public partial class QRCode_QRCodeCreate : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int QRID;
    string QRCode;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            RandomGenerator();
            string Attachment = "";

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            #region Item Attachment

            if (Uploadattach.HasFiles)
            {
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServiceRequest"))
                {

                    foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                    {
                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/Content/ServiceRequest/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        Attachment=lblAtt .Text = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        //objMaster.Itemattachment = Attachment;
                    }

                }
                else
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServiceRequest");
                    foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                    {
                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/Content/ServiceRequest/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        Attachment = lblAtt.Text = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        //objMaster.Itemattachment = Attachment;
                    }

                }

            }
            #endregion
            //txtDate.Text = DateTime.Now.ToString();
            int i = SaveServiceRequest(QRID, DateTime.Now, txtName.Text, txtMobile.Text, Attachment);
            if (i > 0)
            {
                GenerateQRCode();
            }
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Unable to Generate QR Code, please try again or contact Admin.");
        }
    }
    private void GenerateQRCode()
    {
        string Name = txtName.Text;
        string Mobile=txtMobile .Text ;
        string Attach = lblAtt.Text;
        string hai = "Name :" + Name + " " + "Mobile No :" + Mobile + ", " + "Image :" + Attach;


        //string hai = "hai";
        var url = string.Format("https://api.qrserver.com/v1/create-qr-code/?size=150x150&data={0}", hai);

        WebResponse response = default(WebResponse);
        Stream remoteStream = default(Stream);
        StreamReader readStream = default(StreamReader);
        WebRequest request = WebRequest.Create(url);
        response = request.GetResponse();
        remoteStream = response.GetResponseStream();
        readStream = new StreamReader(remoteStream);
        System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
        //  img.Save("D:/QRCode/" + txtaadharno.Text + ".png");
        img.Save(Server.MapPath("QRCodeImages/") + QRID+ ".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
        //img.Save("D:\\valueapp\\QRCode\\QRCodeImages\\" + img + ".png");
        //QRCode = "D:\\valueapp\\QRCode\\QRCodeImages\\" + txtName.Text + ".png";
        
        //img.Save("~/QRCode/" + txtaadharno.Text + ".png", ImageFormat.Png);
        response.Close();
        remoteStream.Close();
        readStream.Close();
        imgQRCode.Visible = true;
        imgQRCode.ImageUrl = "http://183.82.108.55/QRCode/QRCodeImages/" + QRID + ".JPEG";
    }
    protected void RandomGenerator()
    {
        
        Random RandomGenerator = null;
        RandomGenerator = new Random();
        QRID = RandomGenerator.Next(0001, 99999999);
        
        
    }
    private int SaveServiceRequest(int QRID, DateTime Date, string Name, string Mobile, string File)
    {
        SqlCommand cmd = new SqlCommand();
        int i = 0;
        try
        {
            con.Close();
            string instr = "Insert Into QR_Code_Tbl Values(" + "" + QRID + "," + "'" + Date + "'," + "'" + Name + "'," + "'" + Mobile + "'," + "'" + File + "')";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }
        catch (Exception ex) 
        { 
            i = 0; 
        }
        return i;
    }
   public string SendMessage(string To, string Message)
    {
        string from = "919177700473";
        string status="";
        string password;
        var res = WhatsAppApi.Register.WhatsRegisterV2.RequestCode(from, out password);
        WhatsApp wa = new WhatsApp(from, "4sgLq1p5sV6", "valuelineapp.com", false, false);
        //WhatsApp wa = new WhatsApp("919177700473", "4sgLq1p5sV6", "Jami", false, false);
        wa.OnConnectSuccess += () =>
        {
            wa.OnLoginSuccess += (phoneNumber, data) =>
            {
                status = "Connection Success";
                wa.SendMessage(To, Message);
                status = "Message Sent Success";
            };
            wa.OnLoginFailed += (data) =>
            {
                status = "Login Failed" + data;
            };
            wa.Login();
        };
        wa.OnConnectFailed+=(ex)=>
        {
            status="Connection Failed"+ex.StackTrace;
        };
        wa.Connect();
        return status;

   }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        //string From = "919177700473";
        //string To = txtToNo.Text;
        //string Msg = txtMessage.Text;
        //string Img = "http://183.82.108.55/QRCode/QRCodeImages/" + QRID + ".JPEG";
        //WhatsApp wa = new WhatsApp(From, "4sgLq1p5sV6", "Jami", false, false);
        //wa.OnConnectSuccess += () =>
        //{
        //    MessageBox.Show(this, "Connected to WhatsApp");

        //    wa.OnLoginSuccess += (PhoneNumber, data) => 
        //    {
        //        wa.SendMessage(To, Msg);
        //        MessageBox.Show(this, "Message Sent ....");

        //    };
        //    wa.OnLoginFailed += (data) => {
        //        MessageBox.Show(this, "Sending Failed ....");
        //    };
        //    wa.Login();
        //};
        //wa.OnConnectFailed += (ex) => 
        //{
        //    MessageBox.Show(this, "Connection Failed ....");

        //};
        //wa.Connect();
        SendMessage(txtToNo.Text, txtMessage.Text);
    }
}