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

public partial class Modules_HR_HODApproval : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    string pagenavigationstr,pagevar1,leaveidvar2;
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
            objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
            lblDeptId.Text = objmas.DeptID;
            lblEmpIdHidden.Text = objmas.EmpID;
            lblHOD_Id.Text = objmas.Dept_Hod_Id;

            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            BindPendingLeaves();
            LoadGrid();
            setControlsVisibility();

        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "116");
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "68");
        btnDelete.Enabled = up1.Delete;
        //btnSearch.Enabled = up.Search;
        btnApprove.Enabled = up.Approve;
        btnReject.Enabled = up.Approve;
    }
    private void BindPendingLeaves()
    {
        string stat1 = "Pending";

        ///GET hOD ID FROM DEPT
        ///

        General OBJ = new General();

        string HODID = OBJ.GetColumnVal("select * from YANTRA_DEPT_MAST where DEPT_ID='"+lblDeptId.Text+"'", "DEPT_HEAD");

        string sql4 = @" select Dept_ID  from YANTRA_DEPT_MAST where DEPT_HEAD ='" + HODID + "'  ";
        DataTable dttemp4 = OBJ.ReturnDataTable(sql4);
        foreach (DataRow dr in dttemp4.Rows)
        {
            foreach (DataColumn col in dttemp4.Columns)
            {
                lblDeptIds.Text += dr[col] + ",";
            }
        }

        if (!string.IsNullOrEmpty(lblDeptIds.Text))
        {
            lblDeptIds.Text = string.Format(lblDeptIds.Text.Substring(0, lblDeptIds.Text.Length - 1));
        }

      
         string mamdam = "50";

        //if(HODID == lblHOD_Id.Text)
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "' and EMP_Leave_tbl.Emp_Department in ( " + lblDeptIds.Text + ") order by EMP_Leave_tbl.DateOfApplying desc", con);
        //    //SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "' order by EMP_Leave_tbl.DateOfApplying desc", con);

        //    cmd.CommandType = CommandType.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    gvHodPendingAppl.DataSource = dt;
        //    gvHodPendingAppl.DataBind();
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "' and EMP_Leave_tbl.Emp_Department in (select [YANTRA_DEPT_MAST].DEPT_ID from [YANTRA_DEPT_MAST]  where [YANTRA_DEPT_MAST].DEPT_HEAD='" + lblEmpIdHidden.Text + "') order by EMP_Leave_tbl.DateOfApplying desc", con);

        //    cmd.CommandType = CommandType.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    gvHodPendingAppl.DataSource = dt;
        //    gvHodPendingAppl.DataBind();
        //}


        if(mamdam == lblEmpIdHidden.Text)
        {
            SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "'  order by EMP_Leave_tbl.DateOfApplying desc", con);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvHodPendingAppl.DataSource = dt;
            gvHodPendingAppl.DataBind();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl inner join YANTRA_EMPLOYEE_DET  on YANTRA_EMPLOYEE_DET .Emp_Id =EMP_Leave_tbl .Emp_Id  WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "' and YANTRA_EMPLOYEE_DET .Reporting_ID =  '" + lblEmpIdHidden .Text + "'  order by EMP_Leave_tbl.DateOfApplying desc", con);

            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvHodPendingAppl.DataSource = dt;
            gvHodPendingAppl.DataBind();
        }










        
        //if (lblUserType.Text == "1")
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" +  stat1 + "' and EMP_Leave_tbl.Emp_Department in (select [YANTRA_DEPT_MAST].DEPT_ID from [YANTRA_DEPT_MAST]  where [YANTRA_DEPT_MAST].DEPT_HEAD='" +  lblEmpIdHidden.Text + "') order by EMP_Leave_tbl.DateOfApplying desc", con);
           
        //    cmd.CommandType = CommandType.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    gvHodPendingAppl.DataSource = dt;
        //    gvHodPendingAppl.DataBind();
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2  FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "'  order by EMP_Leave_tbl.DateOfApplying desc", con);


        //    //SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2  FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "' and Emp_Department = '"+lblDeptId+"'   order by EMP_Leave_tbl.DateOfApplying desc", con);
        //    //SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" + stat1 + "' and EMP_Leave_tbl.Emp_Department in (select [YANTRA_DEPT_MAST].DEPT_ID from [YANTRA_DEPT_MAST]  where [YANTRA_DEPT_MAST].DEPT_HEAD='" + lblEmpIdHidden.Text + "') order by EMP_Leave_tbl.DateOfApplying desc", con);



        //    cmd.CommandType = CommandType.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    gvHodPendingAppl.DataSource = dt;
        //    gvHodPendingAppl.DataBind();
        //}


        //SqlCommand cmd = new SqlCommand("SELECT Leave_Id, Emp_Id, Emp_Name, Emp_Designation, Emp_Department, CONVERT (VARCHAR(10), DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), ToDate, 103) AS [To Date], AppliedNoOfLeaves, TypeOfLeave, Reason, AddressInLeavePeriod,FromSession,ToSession FROM EMP_Leave_tbl WHERE Emp_Department = '" +  lblDeptId.Text + "' and Status1 = '" +  stat1 + "' ", con);

    }
    //private void BindPendingLeaves()
    //{
    //    string stat1 = "Pending";
    //    //SqlCommand cmd = new SqlCommand("SELECT Leave_Id, Emp_Id, Emp_Name, Emp_Designation, Emp_Department, CONVERT (VARCHAR(10), DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), ToDate, 103) AS [To Date], AppliedNoOfLeaves, TypeOfLeave, Reason, AddressInLeavePeriod,FromSession,ToSession FROM EMP_Leave_tbl WHERE Emp_Department = '" +  lblDeptId.Text + "' and Status1 = '" +  stat1 + "' ", con);
    //    SqlCommand cmd = new SqlCommand("SELECT EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, EMP_Leave_tbl.Emp_Name, EMP_Leave_tbl.Emp_Designation, EMP_Leave_tbl.Emp_Department, CONVERT (VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS [From Date], CONVERT (VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS [To Date], EMP_Leave_tbl.AppliedNoOfLeaves, EMP_Leave_tbl.TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod,EMP_Leave_tbl.FromSession,EMP_Leave_tbl.ToSession FROM EMP_Leave_tbl WHERE EMP_Leave_tbl.Status1 = '" +  stat1 + "' and EMP_Leave_tbl.Emp_Department in (select [YANTRA_DEPT_MAST].DEPT_ID from [YANTRA_DEPT_MAST]  where [YANTRA_DEPT_MAST].DEPT_HEAD='" +  lblEmpIdHidden.Text + "') order by EMP_Leave_tbl.DateOfApplying desc", con);
        
    //    cmd.CommandType = CommandType.Text;
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    gvHodPendingAppl.DataSource = dt;
    //    gvHodPendingAppl.DataBind();
    //}

    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvHodPendingAppl.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvHodPendingAppl.Rows)
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
        BindPendingLeaves();
        //gvHodPendingAppl.DataBind();
        LoadGrid();
    }

    private void LeaveApprove()
    {
        foreach (GridViewRow gvr in gvHodPendingAppl.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");                                      
                    
                    SqlCommand cmd = new SqlCommand("USP_UpdateHODApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", leaveId.Text);
                    if(i==1)
                    {
                        cmd.Parameters.AddWithValue("@Status1", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Comment1", "HR");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if( i==0 )
                    {
                        cmd.Parameters.AddWithValue("@Status1", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Comment1", "-");

                        cmd.Parameters.AddWithValue("@Rejected_By", "HOD");

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
                            pagenavigationstr = "http://183.82.108.55/HRLA.aspx?LID=" + leaveId.Text + "";
                            pagevar1 = "HRLA.aspx?LID=";
                            leaveidvar2 = leaveId.Text + " "+ lblEmpName .Text ;
                            string msgHR = "http://183.82.108.55/" + pagevar1 + leaveidvar2 + " has applied for a " + lbltypeOfLeave.Text + " from " + lblFromDate.Text + " to " + lblToDate.Text + ".  VALUELINE";
                           
                            
                            //string msgHR = "" + pagenavigationstr + " " + lblEmpName.Text + " has applied for a " + lbltypeOfLeave.Text + " from " + lblFromDate.Text + " to " + lblToDate.Text + ". ";
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            //HR Emp_Id=90
                            objmas.EmployeeMaster_Select("90");
                            string HR_MNo = objmas.AssignedMobileNo;
                            //string HR_MNo = "9059638293";
                            objsms.Send_LeaveApprovalSMS(msgHR, HR_MNo);
                        }
                    }
                    else if(i==0)
                    {
                        if (j > 0)
                        {
                            HR.SendSMS objsms = new HR.SendSMS();
                            ////Send MSG to Employee
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            objmas.EmployeeMaster_Select(lblEmpIdHidden.Text);
                            lblHod_Name.Text = objmas.EmpFirstName + " " +  objmas.EmpLastName;

                            string msfEmp = "Your leave dated " +  lblFromDate.Text + " to " +  lblToDate.Text + " has been rejected by " +  lblHod_Name.Text + ". Please contact the HR department for any quarries. VALUELINE";
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
        BindPendingLeaves();
        //gvHodPendingAppl.DataBind();
        LoadGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvHodPendingAppl.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");

                    SqlCommand cmd = new SqlCommand("USP_UpdateHODApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", leaveId.Text);
                        cmd.Parameters.AddWithValue("@Status1", "Obsoleted");
                        cmd.Parameters.AddWithValue("@Status3", "Obsoleted");
                        cmd.Parameters.AddWithValue("@Comment1", "-");

                        cmd.Parameters.AddWithValue("@Rejected_By", "HOD");
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
        gvHodPendingAppl.DataBind();
        LoadGrid();
    }

    private void UpdateEmpNoOfLeave()
    {
        foreach (GridViewRow gvr in gvHodPendingAppl.Rows)
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
    protected void btnLeaveHistory_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    private void LoadGrid()
    {
        //Load Grid View
        SqlCommand cmd = new SqlCommand("USP_HODLeaveHistoryGrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if(lblUserType.Text == "1")
        {
            cmd.Parameters.AddWithValue("@DeptId", lblDeptId.Text);
        }

        cmd.Parameters.AddWithValue("@Status1", "Approved");
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