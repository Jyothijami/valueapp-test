using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class waste_ip2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblIP.Text = GetVisitorIPAddress();
            lblPATH_INFO.Text = HttpContext.Current.Request.ServerVariables["UserHostName"];
            string stringHostName = Dns.GetHostName();
            lblHostName.Text = stringHostName;
            lblMAC.Text = GetMAC();

        }
    }
    private string GetMAC()
    {
        string macAddresses = "";

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }
    public static string GetVisitorIPAddress()
    {
        bool GetLan = false;
        string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (String.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (string.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
        {
            GetLan = true;
            visitorIPAddress = string.Empty;
        }

        if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
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
                        visitorIPAddress = ip.ToString();
                    }
                }
                //visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
            }
            catch
            {
                try
                {
                    visitorIPAddress = arrIpAddress[0].ToString();
                }
                catch
                {
                    try
                    {
                        arrIpAddress = Dns.GetHostAddresses(stringHostName);
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        visitorIPAddress = "127.0.0.1";
                    }
                }
            }

        }


        return visitorIPAddress;
    }
}

