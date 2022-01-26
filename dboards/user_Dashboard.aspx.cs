using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using vllib;

public partial class dboards_user_Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string title = "Greetings";
        //string body = "Welcome to Valueline";
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);

        empinfo();
    }

    private void empinfo()
    {
        
    }









    protected void Page_Init(object sender, EventArgs e)
    {
        //DataTable dt = usre.getUserControls(Session["vl_userid"].ToString());
        HR.getDashBoards getDashBoards = new HR.getDashBoards();
        DataTable dt = getDashBoards.getUserControls(Session["vl_userid"].ToString());

        foreach(DataRow dr in dt.Rows)
        {
            Control myUserControl = (Control)Page.LoadControl("~/Modules/widgets/" + dr["DBoardName"].ToString());
            Panel pnl = new Panel();
            pnl.Controls.Add(myUserControl);

            pnl.CssClass = "flowlay";

            // Place web user control to place holder control
            ucPanel1.Controls.Add(pnl);
        }

    }
}

