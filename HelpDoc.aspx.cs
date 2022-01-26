using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HelpDoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("~/Content/valuline.pdf");
    }
    protected void btnHelpDoc_Click(object sender, EventArgs e)
    {
        Response.ContentType = "Application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=help.pdf");
        Response.TransmitFile(Server.MapPath("~/Content/valuline.pdf"));
        Response.End();
    }
}