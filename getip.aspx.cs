using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class getip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //if (string.IsNullOrEmpty(ip))
        //{
        //    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //}
        //Label1.Text = ip;


        GetVisitorIPAddress();


        WebClient webClient = new WebClient();
        string publicIp = webClient.DownloadString("http://api.ipify.org");
      //  Console.WriteLine("My public IP Address is: {0}", publicIp);
        Label1.Text = publicIp;

    }


    public  string GetVisitorIPAddress()
    {
        bool GetLan = false;
        Label1.Text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (String.IsNullOrEmpty(Label1.Text))
            Label1.Text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (string.IsNullOrEmpty(Label1.Text))
            Label1.Text = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(Label1.Text) || Label1.Text.Trim() == "::1")
        {
            GetLan = true;
            Label1.Text = string.Empty;
        }

        if (GetLan && string.IsNullOrEmpty(Label1.Text))
        {
            //This is for Local(LAN) Connected ID Address
            string stringHostName = Dns.GetHostName();
            //Get Ip Host Entry
            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);

            //Get Ip Address From The Ip Host Entry Address List
            IPAddress[] arrIpAddress = ipHostEntries.AddressList;

            try
            {
                //Get Ip Address of InterNetwork From The Ip Host Entry Address List
                foreach (IPAddress ip in ipHostEntries.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        Label1.Text = ip.ToString();
                    }
                }
                //Label1.Text = arrIpAddress[arrIpAddress.Length - 2].ToString();
            }
            catch
            {
                try
                {
                    Label1.Text = arrIpAddress[0].ToString();
                }
                catch
                {
                    try
                    {
                        arrIpAddress = Dns.GetHostAddresses(stringHostName);
                        Label1.Text = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        Label1.Text = "127.0.0.1";
                    }
                }
            }

        }


        return Label1.Text;
    }
}