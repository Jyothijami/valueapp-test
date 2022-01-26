using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_eQuotation_eQuotation_App : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownload1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DownloadFile.ashx?dt=eQuot");
    }
}
 
