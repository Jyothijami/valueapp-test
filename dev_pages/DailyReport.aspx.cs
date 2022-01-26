using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
public partial class dev_pages_DailyReport : System.Web.UI.Page
{
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            Response.Redirect("~/MobileLogin.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        if (!IsPostBack)
        {
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlAttendedBy);
            txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlAttendedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        try
        {
            objdr.DRDate = Yantra.Classes.General.toMMDDYYYY(txtDateTime.Text);
            objdr.CustName = txtClientsName.Text;
            objdr.Purpose = txtPurpose.Text;
            objdr.Remarks = txtRemarks.Text;
            objdr.DRAttendedBy = ddlAttendedBy.SelectedItem.Value;
            objdr.DRPreparedBy = ddlAttendedBy.SelectedItem.Value;
            objdr.Time = "01/01/1900 " + ddlHour.SelectedItem.Value + ":" + ddlMin.SelectedItem.Value + " " + ddlAMPM.SelectedItem.Value;
            objdr.Address = txtAddress.Text;
            objdr.Phone = txtPhoneNo.Text;
            objdr.Reference = txtReference.SelectedItem .Value ;
            objdr.Architect = txtArchitect.Text;
            objdr.outTime = "01/01/1900 " + ddlOutHour.SelectedItem.Value + ":" + ddlOutMin.SelectedItem.Value + " " + ddlOutAMPM.SelectedItem.Value;
            objdr.Comment = "";
            //ddlHour.SelectedValue + ":" + ddlMin.SelectedValue + " " + ddlAMPM.SelectedValue;
            objdr.FileName = "";
            objdr.DailyReports_Save();
            MessageBox.Show(this, "Data Saved Successfully");
            btnRefresh_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Unable to raise the request, please try again or contact Admin.");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtClientsName.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        //txtDateTime.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        //ddlAttendedBy.SelectedValue = "0";
        txtArchitect.Text = string.Empty;
    }
}