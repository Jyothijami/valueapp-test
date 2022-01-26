using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_SM_DailyReportEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Qid = Request.QueryString["Cid"].ToString();
        lblId.Text = Request.QueryString["Cid"].ToString();
        if (!IsPostBack)
        {
            SM.DailyReport obj = new SM.DailyReport();
            Masters.EnquiryMode.EnquiryMode_Select(ddlreference);
            ddlreference.Items.FindByText("--").Text = "Not Selected";
            //txtDateTime.Text = DateTime.Now.ToString();
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlAttendedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlBackup);

            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);


            if (obj.DailyReport_Select(Request.QueryString["Cid"].ToString()) > 0)
            {
                txtPurpose.Text = obj.Purpose;
                txtRemarks.Text = obj.Remarks;
                txtClientsName.Text = obj.CustName;
                txtPhoneNo.Text = obj.Phone;
                txtArchitect.Text = obj.Architect;
                ddlreference.SelectedItem.Text = obj.Reference;
                ddlDRType.SelectedItem.Text = obj.DRType;
                ddlAttendedBy.SelectedValue = obj.DRAttendedBy;
                txtAddress.Text = obj.Address;
                lbldt.Text=txtDateTime.Text = obj.DRDate;
                ddlBackup.SelectedValue = obj.DRAssistedBy;
                ddlHour.SelectedValue = obj.InHours;
                ddlMin.SelectedValue = obj.InMin;
                ddlAMPM.SelectedValue = obj.InAM;
                ddlOutHour.SelectedValue = obj.OutHours;
                ddlOutMin.SelectedValue = obj.OutMin;
                ddlOutAMPM.SelectedValue = obj.OutAM;
                txtEmail.Text = obj.email;
            }
            //BindTasks();

        }
    }

    private void BindTasks()
    {
        try
        {
            SM.DailyReport obj = new SM.DailyReport();
            if (obj.DailyReport_Taskswithdt(ddlAttendedBy.SelectedValue, lbldt.Text) > 0)
            {
                //txtAchiveYesterday.Text = obj.AchiveYesterday;
                //txtachiveToday.Text = obj.AchiveToday;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SM.DailyReport obj = new SM.DailyReport();

            obj.DRId = Request.QueryString["Cid"].ToString();
            obj.DRDate = Yantra.Classes.General.toMMDDYYYY(txtDateTime.Text);
            obj.CustName = txtClientsName.Text;
            obj.Purpose = txtPurpose .Text;
            obj.Remarks = txtRemarks.Text;
            obj.DRAttendedBy = ddlAttendedBy.SelectedValue;
            obj.DRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.DRAssistedBy = ddlBackup.SelectedValue;
            obj.Time = "01/01/1900 " + ddlHour.SelectedItem.Value + ":" + ddlMin.SelectedItem.Value + " " + ddlAMPM.SelectedItem.Value;
            obj.Address = txtAddress.Text;
            obj.Phone = txtPhoneNo.Text;
            obj.Reference = ddlreference.SelectedItem.Text;
            obj.Architect = txtArchitect.Text;
            obj.outTime = "01/01/1900 " + ddlOutHour.SelectedItem.Value + ":" + ddlOutMin.SelectedItem.Value + " " + ddlOutAMPM.SelectedItem.Value;
            //obj.Comment = "Follow Up";
            //ddlHour.SelectedValue + ":" + ddlMin.SelectedValue + " " + ddlAMPM.SelectedValue;
            obj.FileName = "";
            obj.DRType = ddlDRType.SelectedItem.Text;
            obj.DRFollowup = "Follow Up";
            obj.DRStatus = "Open";
            obj.email = txtEmail.Text;
            obj.DailyReport_Update1();
            

            //obj.DailyReport_Update();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            MessageBox.Show(this, "Data Saved Successfully");

        }
    }
}