using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Reports_DailyReport : basePage
{
    string pagenavigationstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.CompanyProfile.Company_Select(ddlDepDaily);
        }
       

    }

   

   
   
    protected void runDailyReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=StockTransfer&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDailyRep.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDailyRep.Text) + "&cmpids=" + ddlDepDaily.SelectedValue + "";

        //pagenavigationstr = "../Reports/EODReportViewer.aspx?type=StockTransfer&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDailyRep.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDailyRep.Text) + "&empid=" + ddlEmpDaily.SelectedValue + "&dep=" + ddlDepDaily.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
   
}