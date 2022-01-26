using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
//using System.Net.Http;
//using System.Net;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager1.SetFocus(txtUserName);




            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //using (var httpClient = new HttpClient())
            //{
            //    var ip_task = httpClient.GetStringAsync("https://api.ipify.org");
            //    ip_task.Wait();
            //    lblip.Text = ip_task.Result;
            //}
                                
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //#region Check Browser Integration
        //if (Request.Browser.Type.Contains("Firefox"))
        //{
        Session["UserName"] = txtUserName.Text;
        Yantra.Authentication Login = new Yantra.Authentication();
        MessageBox.Show(this, Login.LoginCheck(this.Page, txtUserName.Text.Trim(), txtPassword.Text));
        ScriptManager1.SetFocus(txtPassword);





        //}
        //else
        //{
        //    MessageBox.Show(this, "For the best User Interface Options we suggest You to open the App in Firefox");
        //}
        //#endregion
        //if (this.IpAddress_Check(IPAddress) > 0)
        //{

        //}

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        ScriptManager1.SetFocus(txtUserName);
    }
}