using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using YantraBLL.Modules;

public partial class Modules_Reports_ShipmentStat : basePage
{
    string pagenavigationstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.CompanyProfile.Company_Select(ddlCompanyNameShipment);
            ddlCompanyNameShipment.Items.FindByText("--").Text = "All";
            Masters.ProductCompany.ProductCompany_Select(ddlBrand1);
        }
    }
    protected void btnShipment_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ShipmentDet&f=" + Yantra.Classes.General.toMMDDYYYY(txtShipmentFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtShipmentTo.Text) + "&c=" + ddlCompanyNameShipment.SelectedValue + "&b=" + ddlBrand1.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
}