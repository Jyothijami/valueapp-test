using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraDAL;
using Yantra.MessageBox;
using System.IO;

public partial class wastelogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager1.SetFocus(txtUserName);
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //#region Check Browser Integration
        //if (Request.Browser.Type.Contains("Firefox"))
        //{
        Session["UserName"] = txtUserName.Text;
        Yantra.Authentication Login = new Yantra.Authentication();
        MessageBox.Show(this, Login.LoginCheck1(this.Page, txtUserName.Text.Trim(), txtPassword.Text));
        ScriptManager1.SetFocus(txtPassword);
        //}
        //else
        //{
        //    MessageBox.Show(this, "For the best User Interface Options we suggest You to open the App in Firefox");
        //}
        //#endregion
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        ScriptManager1.SetFocus(txtUserName);
    }
}