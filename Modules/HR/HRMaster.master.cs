using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_HR_HRMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnApplications_Click(object sender, EventArgs e)
    {
        if (PanelApps.Visible == false)
        {
            PanelApps.Visible = true;
        }
        else
        {
            PanelApps.Visible = false;
        }
    }
    protected void btnOfferLetter_Click(object sender, EventArgs e)
    {
        Response.Redirect("Emp_OfferLetter.aspx");
    }
}
