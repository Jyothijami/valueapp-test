using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DatumDAL;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using vllib;
using Yantra.Classes;

public partial class Modules_HR_MDApproval : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            LoadGrid();
            setControlsVisibility();
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "118");
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "68");
        btnDelete.Enabled = up1.Delete;
        btnApprove.Enabled = up.Approve;
        btnReject.Enabled = up.Approve;
    }

    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvEnrollmentDtls.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvEnrollmentDtls.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;

            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        i = 1;
        LeaveApprove();
        TotalLeaveUpdates();
        gvEnrollmentDtls.DataBind();
        LoadGrid();
    }

    private void TotalLeaveUpdates()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    #region SaveLeavesMonthwise

                    Label LeaveId = (Label)gvr.FindControl("lblLeaveId");
                    Label EmpId = (Label)gvr.FindControl("lblId");
                    Label LeaveType = (Label)gvr.FindControl("lblTypeOfLeave");
                    Label NoOfdays = (Label)gvr.FindControl("lblNoOfDays");
                    Label FromDate = (Label)gvr.FindControl("lblFromDate");
                    Label Todate = (Label)gvr.FindControl("lblToDate");
                    Label FromSession = (Label)gvr.FindControl("lblFromsession");
                    Label Tosession = (Label)gvr.FindControl("lblToSession");

                    string dt1 = General.toyymmdd(FromDate.Text);
                    string dt2 = General.toyymmdd(Todate.Text);

                    DateTime Date1 = Convert.ToDateTime(dt1);
                    DateTime Date2 = Convert.ToDateTime(dt2);
                    int months = ((Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month);

                    if (months == 0)
                    {
                        SqlCommand cmd = new SqlCommand("USP_SaveLeavesMonthwise", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Leave_Id", LeaveId.Text);
                        cmd.Parameters.AddWithValue("@Emp_Id", EmpId.Text);
                        cmd.Parameters.AddWithValue("@Month", Date1.Month);
                        cmd.Parameters.AddWithValue("@Year", Date1.Year);
                        cmd.Parameters.AddWithValue("@TypeOfLeave", LeaveType.Text);
                        cmd.Parameters.AddWithValue("@NoOfLeaves", NoOfdays.Text);

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (months == 1)
                    {
                        DateTime lastDate = new DateTime(Date1.Year, Date1.Month, 1).AddMonths(1).AddDays(-1);
                        TimeSpan t = lastDate - Date1;
                        double month1 = t.TotalDays + 1;

                        //DateTime FirstDate = new DateTime(Date2.Year, Date2.Month, 1).AddMonths(1).AddDays(-1);
                        DateTime FirstDate = new DateTime(Date2.Year, Date2.Month, 1);
                        TimeSpan t2 = Date2 - FirstDate;
                        double month2 = t2.TotalDays + 1;

                        if (FromSession.Text == "Session2")
                        {
                            month1 = month1 - 0.5;
                        }
                        if (Tosession.Text == "Session1")
                        {
                            month2 = month2 - 0.5;
                        }
                        SqlCommand cmd = new SqlCommand("USP_SaveLeavesMonthwise", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Leave_Id", LeaveId.Text);
                        cmd.Parameters.AddWithValue("@Emp_Id", EmpId.Text);
                        cmd.Parameters.AddWithValue("@Month", Date1.Month);
                        cmd.Parameters.AddWithValue("@Year", Date1.Year);
                        cmd.Parameters.AddWithValue("@TypeOfLeave", LeaveType.Text);
                        cmd.Parameters.AddWithValue("@NoOfLeaves", month1);

                        con.Open();
                        int j = cmd.ExecuteNonQuery();
                        con.Close();

                        SqlCommand cmd1 = new SqlCommand("USP_SaveLeavesMonthwise", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Leave_Id", LeaveId.Text);
                        cmd1.Parameters.AddWithValue("@Emp_Id", EmpId.Text);
                        cmd1.Parameters.AddWithValue("@Month", Date2.Month);
                        cmd1.Parameters.AddWithValue("@Year", Date2.Year);
                        cmd1.Parameters.AddWithValue("@TypeOfLeave", LeaveType.Text);
                        cmd1.Parameters.AddWithValue("@NoOfLeaves", month2);

                        con.Open();
                        int k = cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
    }

    private void UpdateEmpNoOfLeave()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label EmpId = (Label)gvr.FindControl("lblId");
                    Label LeaveType = (Label)gvr.FindControl("lblTypeOfLeave");
                    Label NoOfdays = (Label)gvr.FindControl("lblNoOfDays");

                    // Only In the case of Rejection Leaves
                    if(LeaveType.Text == "Casual Leave")
                    {
                        SqlCommand cmd = new SqlCommand("USP_UpdateEmpFinalLeaves", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoOfdays", NoOfdays.Text);
                        cmd.Parameters.AddWithValue("@EMP_Id", EmpId.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else if(LeaveType.Text == "Earned Leave")
                    {
                        SqlCommand cmd1 = new SqlCommand("USP_UpdateEmpFinalLeaves2", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@NoOfdays", NoOfdays.Text);
                        cmd1.Parameters.AddWithValue("@EMP_Id", EmpId.Text);

                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (LeaveType.Text == "Extra Leave")
                    {
                        SqlCommand cmd1 = new SqlCommand("USP_UpdateEmpFinalLeaves3", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@NoOfdays", NoOfdays.Text);
                        cmd1.Parameters.AddWithValue("@EMP_Id", EmpId.Text);

                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                  
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
    }

    private void LeaveApprove()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", leaveId.Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Approved");
                        cmd.Parameters.AddWithValue("@Comment1", "-");

                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Comment1", "-");

                        cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    }
                    Label lblMobile = (Label)gvr.FindControl("lblMobileNo");
                    Label lblEmpName = (Label)gvr.FindControl("lblName");
                    Label lbltypeOfLeave = (Label)gvr.FindControl("lblTypeOfLeave");
                    Label lblFromDate = (Label)gvr.FindControl("lblFromDate");
                    Label lblToDate = (Label)gvr.FindControl("lblToDate");
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i == 1)
                    {
                        if (j > 0)
                        {
                            HR.SendSMS objsms = new HR.SendSMS();
                            string msfEmp = "Your leave dated " + lblFromDate.Text + " to " + lblToDate.Text + " has been approved by the Management. VALUELINE";
                            //string emp_MNo = "9059638293";
                            string emp_MNo = lblMobile.Text;
                            objsms.Send_ApprovedLeaveSMS(msfEmp, emp_MNo);
                        }
                    }
                    else if (i == 0)
                    {
                        if (j > 0)
                        {
                            HR.SendSMS objsms = new HR.SendSMS();
                            ////Send MSG to Employee
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            //MD Emp_Id=50
                            objmas.EmployeeMaster_Select("50");
                            lblMD_Name.Text = objmas.EmpFirstName + " " + objmas.EmpLastName;

                            string msfEmp = "Your leave dated " + lblFromDate.Text + " to " + lblToDate.Text + " has been rejected by " + lblMD_Name.Text + ". Please contact the HR department for any quarries. VALUELINE";
                            //string emp_MNo = "9059638293";
                            string emp_MNo = lblMobile.Text;
                            objsms.Send_RejectedLeaveSMS(msfEmp, emp_MNo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        i = 0;
        LeaveApprove();
        UpdateEmpNoOfLeave();
        gvEnrollmentDtls.DataBind();
        LoadGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", leaveId.Text);
                    cmd.Parameters.AddWithValue("@Status3", "Obsoleted");
                    cmd.Parameters.AddWithValue("@Comment1", "-");
                    cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }
        UpdateEmpNoOfLeave();
        gvEnrollmentDtls.DataBind();
        LoadGrid();
    }
    protected void btnLeaveHistory_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }

    private void LoadGrid()
    {
        //Load Grid View
        SqlCommand cmd = new SqlCommand("USP_MDLeaveHistoryGrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Status3", "Approved");
        cmd.Parameters.AddWithValue("@Rejected", "Rejected");
        if (txtSearch.Text != "")
        {
            cmd.Parameters.AddWithValue("@Emp_Name", txtSearch.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvLeaveHistory.DataSource = dt;
        gvLeaveHistory.DataBind();
    }
    protected void gvLeaveHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeaveHistory.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
}