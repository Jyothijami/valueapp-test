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

public partial class Modules_HR_HRApproval : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    string pagenavigationstr, pagevar1, leaveidvar2;
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
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "68");
        btnDelete.Enabled = up1.Delete;
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "117");
        btnApprove.Enabled = up.Approve;
        btnReject.Enabled = up.Approve;
    }
    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvHRPendingApps.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvHRPendingApps.Rows)
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

    private void UpdateEmpNoOfLeave()
    {
        foreach (GridViewRow gvr in gvHRPendingApps.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label EmpId = (Label)gvr.FindControl("lblId");
                    Label LeaveType = (Label)gvr.FindControl("lblTypeOfLeave");
                    Label NoOfdays = (Label)gvr.FindControl("lblNoOfDays");

                    // Only In the case of Rejection Leaves
                    if (LeaveType.Text == "Casual Leave")
                    {
                        SqlCommand cmd = new SqlCommand("USP_UpdateEmpFinalLeaves", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoOfdays", NoOfdays.Text);
                        cmd.Parameters.AddWithValue("@EMP_Id", EmpId.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (LeaveType.Text == "Earned Leave")
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        i = 1;
        LeaveApprove();
        gvHRPendingApps.DataBind();
        LoadGrid();
    }

    private void LeaveApprove()
    {
        foreach (GridViewRow gvr in gvHRPendingApps.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");
                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", leaveId.Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Comment1", "MD");

                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Comment1", "-");

                        cmd.Parameters.AddWithValue("@Rejected_By", "HR");

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
                            pagenavigationstr = "http://183.82.108.55/MDLA.aspx?LID=" + leaveId.Text + "";
                            pagevar1 = "MDLA.aspx?LID=";
                            leaveidvar2 = leaveId.Text +" "+ lblEmpName .Text ;
                            string msgMD = "http://183.82.108.55/" + pagevar1 + leaveidvar2 + " has applied for a " + lbltypeOfLeave.Text + " from " + lblFromDate.Text + " to " + lblToDate.Text + ".  VALUELINE";
                           
                            //string msgMD = "" + pagenavigationstr + " " + lblEmpName.Text + " has applied for a " + lbltypeOfLeave.Text + " from " + lblFromDate.Text + " to " + lblToDate.Text + ". ";
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            //MD Emp_Id=50
                            objmas.EmployeeMaster_Select("50");
                            //string MD_MNo = "9059638293";
                            string MD_MNo = objmas.AssignedMobileNo;
                            objsms.Send_LeaveApprovalSMS(msgMD, MD_MNo);
                        }
                    }
                    else if (i == 0)
                    {
                        if (j > 0)
                        {
                            HR.SendSMS objsms = new HR.SendSMS();
                            ////Send MSG to Employee
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            objmas.EmployeeMaster_Select("90");
                            lblHR_Name.Text = objmas.EmpFirstName + " " + objmas.EmpLastName;

                            string msgEmp = "Your leave dated " + lblFromDate.Text + " to " + lblToDate.Text + " has been rejected by " + lblHR_Name.Text + ". Please contact the HR department for any quarries. VALUELINE";
                            //string emp_MNo = "9059638293";
                            string emp_MNo = lblMobile.Text;
                            objsms.Send_RejectedLeaveSMS(msgEmp, emp_MNo);
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
        gvHRPendingApps.DataBind();
        LoadGrid();
    }
    
    protected void btnLeaveHistory_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvHRPendingApps.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");
                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", leaveId.Text);
                    cmd.Parameters.AddWithValue("@Status2", "Obsoleted");
                    cmd.Parameters.AddWithValue("@Status3", "Obsoleted");
                    cmd.Parameters.AddWithValue("@Comment1", "-");

                    cmd.Parameters.AddWithValue("@Rejected_By", "HR");

                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        UpdateEmpNoOfLeave();
        gvHRPendingApps.DataBind();
        LoadGrid();
    }
    private void LoadGrid()
    {
        //Load Grid View
        SqlCommand cmd = new SqlCommand("USP_HRLeaveHistoryGrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Status2", "Approved");
        cmd.Parameters.AddWithValue("@Rejected", "Rejected");
        if(txtSearch.Text != "")             
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