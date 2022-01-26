using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;

public partial class Modules_SM_RemarksView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        string Qid = Request.QueryString["Cid"].ToString();
        lblId.Text = Request.QueryString["Cid"].ToString();
        if(!IsPostBack)
        {
            SM.DailyReport obj = new SM.DailyReport();
            Masters.EnquiryMode.EnquiryMode_Select(ddlreference);
            ddlreference.Items.FindByText("--").Text = "Not Selected";
            //txtDateTime.Text = DateTime.Now.ToString();
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlAttendedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlBackup);



            if (obj.DailyReport_Select(Request.QueryString["Cid"].ToString()) > 0)
            {
                txtPurpose.Text = obj.Purpose;
                txtRemarks.Text = obj.Remarks;
                txtClientsName.Text = obj.CustName;
                txtPhoneNo.Text = obj.Phone;
                txtArchitect.Text = obj.Architect;
                ddlreference.SelectedItem.Text = obj.Reference;
                ddlDRType.SelectedItem .Text  = obj.DRType;
                ddlAttendedBy.SelectedValue = obj.DRAttendedBy;
                txtAddress.Text = obj.Address;
                txtDateTime.Text = obj.DRDate;
                txtEmail.Text = obj.email;
            }


        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SM.DailyReport obj = new SM.DailyReport();
            
            obj.DRId = Request.QueryString["Cid"].ToString();
            obj.DetCustName = txtClientsName.Text;
                    obj.DetReference = txtPhoneNo.Text;
                    obj.DetPurpose = txtPurpose.Text;
                    obj.DetRemarks = txtRemarks.Text;
                    obj.DetComments = txtComments.Text;
                    obj.DRDetDate = DateTime.Now.ToString();
                    obj.CommentedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    //MessageBox.Show(this, obj.DailyReportDet_Save());
                    obj.Time = "01/01/1900 " + ddlHour.SelectedItem.Value + ":" + ddlMin.SelectedItem.Value + " " + ddlAMPM.SelectedItem.Value;
                    obj.outTime = "01/01/1900 " + ddlOutHour.SelectedItem.Value + ":" + ddlOutMin.SelectedItem.Value + " " + ddlOutAMPM.SelectedItem.Value;
                   
            if (obj.DailyReportDet_Save() == "Data Saved Successfully")
                    {

                        obj.DRId = Request.QueryString["Cid"].ToString();
                        obj.DailyReportComm_Update();
                        obj.DRDate = Yantra.Classes.General.toMMDDYYYY(txtDateTime.Text);

                        //obj.DRDate = DateTime.Now.ToString();
                        obj.CustName = txtClientsName.Text;
                        obj.Purpose = txtRemarks.Text;
                        obj.Remarks = txtComments.Text;
                        obj.DRAttendedBy = ddlAttendedBy.SelectedValue;
                        obj.DRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                        obj.DRAssistedBy = ddlBackup.SelectedValue;
                        obj.Time = "01/01/1900 " + ddlHour.SelectedItem.Value + ":" + ddlMin.SelectedItem.Value + " " + ddlAMPM.SelectedItem.Value;
                        obj.Address = txtAddress.Text;
                        obj.Phone = txtPhoneNo .Text ;
                        obj.Reference = ddlreference.SelectedItem.Text;
                        obj.Architect = txtArchitect.Text;
                        obj.outTime = "01/01/1900 " + ddlOutHour.SelectedItem.Value + ":" + ddlOutMin.SelectedItem.Value + " " + ddlOutAMPM.SelectedItem.Value;
                        if (obj.DRAttendedBy != obj.DRPreparedBy)
                        {
                            obj.Comment = "Comment";
                            obj.DRFollowup = "Comment";
                        }
                        else
                        {
                            obj.Comment = "Follow Up";
                            obj.DRFollowup = "Follow Up";
                        }
                        //ddlHour.SelectedValue + ":" + ddlMin.SelectedValue + " " + ddlAMPM.SelectedValue;
                        obj.FileName = "";
                        obj.DRType = ddlDRType.SelectedItem.Text;
                        
                        obj.DRStatus = "Open";
                        obj.email = txtEmail.Text;
                        obj.DailyReports_Save();

                    }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            gvFollowUp.DataBind();
            txtClientsName.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtPurpose.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtComments.Text = string.Empty;

        }
    }

    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Visible = false;
            if (e.Row.Cells[8].Text == "0")
            {
                e.Row.BackColor = System.Drawing.Color.Pink;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.SkyBlue;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}