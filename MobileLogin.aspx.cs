using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;

public partial class MobileLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsignin_Click(object sender, EventArgs e)
    {


        string ReturnUrl = Convert.ToString(Request.QueryString["url"]);

        if (!string.IsNullOrEmpty(ReturnUrl))
        {
            Yantra.Authentication Login = new Yantra.Authentication();
            MessageBox.Show(this, Login.MobileLoginCheck2(this.Page, txtUserName.Text.Trim(), txtPassword.Text,ReturnUrl));

        }
        else
        {
            Yantra.Authentication Login = new Yantra.Authentication();
            MessageBox.Show(this, Login.MobileLoginCheck(this.Page, txtUserName.Text.Trim(), txtPassword.Text));
        }





       
    }
}