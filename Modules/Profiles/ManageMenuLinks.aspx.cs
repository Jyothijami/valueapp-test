using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class dev_pages_ManageMenuLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblIP.Text = GetVisitorIPAddress();
            lblMAC.Text = GetMAC();
            //VerifyFileExistance();
        }
    }
    private void VerifyFileExistance()
    {
        if (System.IO.File.Exists(@"C:\Program Files (x86)\DatumInfoSystems\eQuotation\TermsConditionsPnl.png"))
        {
            //True statements comes here
            lblFileExist.Text = "File Exists";
        }
        else
        {
            lblFileExist.Text = "File Doesn't Exists";

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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string PrivilegeID = GridView1.Rows[e.RowIndex].Cells[1].Text;

        uPriv.DeletePermissions(PrivilegeID);
    }
    protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        GridView1.DataBind();
    }
}