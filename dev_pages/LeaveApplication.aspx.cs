using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.Classes;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using vllib;


public partial class dev_pages_LeaveApplication : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    //string LeaveId = "";
    string pagenavigationstr, pagevar1, leaveidvar2;
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            Response.Redirect("~/MobileLogin.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
            
            objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
            lblEmpId.Text = objmas.EmpID;
            lblMobile.Text = objmas.AssignedMobileNo;
            txtEmpName.Text = objmas.EmpFirstName + " " +objmas.EmpLastName;
            lblEmpName.Text = objmas.EmpFirstName + " " +objmas.EmpLastName;
            lblHOD_Id.Text = objmas.Dept_Hod_Id;
            lblReportingID.Text = objmas.ReportingId;

            lblDeptId.Text = objmas.DeptID;
            txtDesignation.Text = objmas.DesgName12;
            txtDepartment.Text = objmas.DeptName12;
            DateTime now = DateTime.Now;
            txtDateOfApply.Text  = now.ToString("dd/MM/yyyy");
            GetAvailableLeaves();
            BindPendingLeaves();
            GetAvalibleCoff();
            rbtnFullLeave.SelectedValue = "1";
            rbnFrom.SelectedValue = "0";
            rbnTo.SelectedValue = "1";
            //gvPendingLeaves.DataBind();
            LoadGrid();
        }
        
    }

    private void GetAvalibleCoff()
    {
        SqlCommand cmd = new SqlCommand("Usp_AvlCOff", con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Month", DateTime.Now.Month);
        cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
        cmd.Parameters.AddWithValue("@EmpId", lblEmpId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count>0)
        {
            lblCoff.Text = dt.Rows[0][1].ToString();
        }
        else
        {
            lblCoff.Text = "0";
        }
    }

    private void BindPendingLeaves()
    {
        string stat1 = "Pending";
        SqlCommand cmd = new SqlCommand("SELECT Leave_Id, Emp_Id, CONVERT (VARCHAR(10), DateOfApplying, 103) AS DateOfApplying, CONVERT (VARCHAR(10), FromDate, 103) AS FromDate, CONVERT (VARCHAR(10), ToDate, 103) AS ToDate, AppliedNoOfLeaves, TypeOfLeave, Reason, AddressInLeavePeriod, Status1,FromSession,ToSession FROM EMP_Leave_tbl where Emp_Id = " +lblEmpId.Text + " and  Status1 = '" +stat1 + "' ", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvPendingLeaves.DataSource = dt;
        gvPendingLeaves.DataBind();
    }

    private void GetAvailableLeaves()
    {
        SqlCommand cmd = new SqlCommand("Usp_GetAvailableLeaves", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count >0)
        {
            txtAvailableCasualLeaves.Text = dt.Rows[0][0].ToString();
            txtAvailableEarnedLeaves.Text = dt.Rows[0][1].ToString();
            txtAvailableExtra.Text = dt.Rows[0][2].ToString();            
        }
      
    }

    protected void RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        lblLeaveId.Text = intRandomNumber.ToString();

    }

    protected void SaveLeaveApplication()
    {
        string leave_Id = "";


        SqlCommand cmd = new SqlCommand("USP_SaveUserLeaveApp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblId.Text == "")
        {
            leave_Id = lblLeaveId.Text;
            cmd.Parameters.AddWithValue("@Leave_Id", leave_Id);

        }
        else
        {
            leave_Id = lblId.Text;
            cmd.Parameters.AddWithValue("@Leave_Id", leave_Id);

        }
        cmd.Parameters.AddWithValue("@Emp_Id", lblEmpId.Text);
        cmd.Parameters.AddWithValue("@Casual_Leaves", txtAvailableCasualLeaves.Text);
        cmd.Parameters.AddWithValue("@Earned_Leaves", txtAvailableEarnedLeaves.Text);
        cmd.Parameters.AddWithValue("@Emp_Name", txtEmpName.Text);

        cmd.Parameters.AddWithValue("@Emp_Designation", txtDesignation.Text);
        cmd.Parameters.AddWithValue("@Emp_Department", lblDeptId.Text);
        cmd.Parameters.AddWithValue("@DateOfApplying", General.toMMDDYYYY(txtDateOfApply.Text));
        cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));

        cmd.Parameters.AddWithValue("@AppliedNoOfLeaves", txtTotalDaysOfLeave.Text);
        cmd.Parameters.AddWithValue("@TypeOfLeave", ddlTypeOfLeave.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@Reason", txtReason.Text);
        cmd.Parameters.AddWithValue("@AddressInLeavePeriod", txtAddress.Text);
        cmd.Parameters.AddWithValue("@Status1", "Pending");
        cmd.Parameters.AddWithValue("@Status2", "Pending");
        cmd.Parameters.AddWithValue("@Status3", "Pending");

        cmd.Parameters.AddWithValue("@Comment1", "HOD");
        cmd.Parameters.AddWithValue("@Comment2", lblMobile.Text);
        cmd.Parameters.AddWithValue("@Comment3", "");
        cmd.Parameters.AddWithValue("@Rejected_By", "");
        cmd.Parameters.AddWithValue("@FromSession", rbnFrom.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@ToSession", rbnTo.SelectedItem.Text);

        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        if(i>0)
        {

            HR.SendSMS objsms = new HR.SendSMS();
            //Send MSG to Employee
            //string msfEmp = "Your leave dated " +txtFromDate.Text + " to " +txtToDate.Text + " has been submitted to the management. Please contact the HR department in case of cancellation or any further quarries.";
            //string emp_MNo = lblMobile.Text;
            //objsms.Send_App_SMS(msfEmp, emp_MNo);

            HR.EmployeeMaster objmas = new HR.EmployeeMaster();

            //Send MSG To the HOD
            objmas.EmployeeMaster_Select(lblReportingID.Text);
            lblHod_Mbl.Text = objmas.AssignedMobileNo;
            //lblHod_Mbl.Text = "9492204836";
            pagenavigationstr = "http://183.82.108.55/HODLA.aspx?LID=" + lblLeaveId.Text + "";
            pagevar1 = "HODLA.aspx?LID=";
            leaveidvar2 = lblLeaveId.Text + " "+lblEmpName .Text ;;
            string msgHOD = "http://183.82.108.55/" + pagevar1 + leaveidvar2 + " has applied for a " + ddlTypeOfLeave.SelectedItem.Text + " from " + txtFromDate.Text + " to " + txtToDate.Text + ".  VALUELINE";
                
            
            //string msgHOD = ""+ pagenavigationstr +" " + lblEmpName.Text + " has applied for a " + ddlTypeOfLeave.SelectedItem.Text + " from " + txtFromDate.Text + " to " + txtToDate.Text + ". Please use the link to approve/Reject the leave.";
            objsms.Send_LeaveApprovalSMS(msgHOD, lblHod_Mbl.Text);
        }
    }

    protected void UpdateLeavetbl()
    {
        if (ddlTypeOfLeave.SelectedValue != "3")
        {
            SqlCommand cmd = new SqlCommand("Usp_UpdateLeavetbl", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Emp_Id", lblEmpId.Text);

            if (ddlTypeOfLeave.SelectedValue == "1")
            {
                decimal casual = (Convert.ToDecimal(txtAvailableCasualLeaves.Text.ToString()) - Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString()));
                cmd.Parameters.AddWithValue("@Casual_Leaves", casual);
                cmd.Parameters.AddWithValue("@Earned_Leaves", txtAvailableEarnedLeaves.Text);
            }
            else if (ddlTypeOfLeave.SelectedValue == "2")
            {
                decimal earned = (Convert.ToDecimal(txtAvailableEarnedLeaves.Text.ToString()) - Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString()));
                cmd.Parameters.AddWithValue("@Casual_Leaves", txtAvailableCasualLeaves.Text);
                cmd.Parameters.AddWithValue("@Earned_Leaves", earned);
            }

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        else if (ddlTypeOfLeave.SelectedValue == "3")
        {
            SqlCommand cmd1 = new SqlCommand("Usp_UpdateLeavetbl_Extra", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Emp_Id", lblEmpId.Text);
            decimal extra = (Convert.ToDecimal(txtAvailableExtra.Text.ToString()) - Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString()));
            cmd1.Parameters.AddWithValue("@Extra_Leaves", extra);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }

    }

   protected void btnApplyLeave_Click(object sender, EventArgs e)
    {
        btnApplyLeave.Enabled = false;
        if (ddlTypeOfLeave.SelectedItem.Value == "2")
        {

            DateTime fdate = Convert.ToDateTime(General.toMMDDYYYY(txtFromDate.Text)); 
            int fyear=fdate.Year;
            int cyear=DateTime.Now.Year;
            if(fyear==cyear)
            {
                RandomGenerator();
                if (lblId.Text != "")
                {
                    UpdateLeaveTblOnDelete();
                }
                GetAvailableLeaves();
                SaveLeaveApplication();
                UpdateLeavetbl();
                ClearFields();
                LoadGrid();
                GetAvailableLeaves();
                MessageBox.Show(this, "Leave Application Submitted Successfully");
            }
            else
            {
               if(Convert.ToDecimal(txtTotalDaysOfLeave.Text)<=5)
               {
                   RandomGenerator();
                   if (lblId.Text != "")
                   {
                       UpdateLeaveTblOnDelete();
                   }
                   GetAvailableLeaves();
                   SaveLeaveApplication();
                   UpdateLeavetbl();
                   ClearFields();
                   LoadGrid();
                   GetAvailableLeaves();
                   MessageBox.Show(this, "Leave Application Submitted Successfully");
               }
                else
               {
                   MessageBox.Show(this, "Leaves count should be less than 5");

               }
            }
           
        }
        else
        {
            DateTime fdate = Convert.ToDateTime(General.toMMDDYYYY(txtFromDate.Text)); 
            int fyear = fdate.Year;
            int cyear = DateTime.Now.Year;
            if (fyear == cyear)
            {
                RandomGenerator();
                if (lblId.Text != "")
                {
                    UpdateLeaveTblOnDelete();
                }
                GetAvailableLeaves();
                SaveLeaveApplication();
                UpdateLeavetbl();
                ClearFields();
                LoadGrid();
                GetAvailableLeaves();
                MessageBox.Show(this, "Leave Application Submitted Successfully");
            }
            else
            {
                MessageBox.Show(this, "Cannot forward Casual/Special leaves for next year");

            }
        }
        BindPendingLeaves();
    }

   private void LoadGrid()
   {
       ////Load Grid View
       //SqlCommand cmd = new SqlCommand("USP_LeaveHistoryGrid", con);
       //cmd.CommandType = CommandType.StoredProcedure;
       //cmd.Parameters.AddWithValue("@Emp_Id", lblEmpId.Text);

       //SqlDataAdapter da = new SqlDataAdapter(cmd);
       //DataTable dt = new DataTable();
       //da.Fill(dt);
       //gvLeaveHistory.DataSource = dt;
       //gvLeaveHistory.DataBind();
   }

   private void ClearFields()
   {
       txtFromDate.Text = txtToDate.Text = txtTotalDaysOfLeave.Text = txtReason.Text = txtAddress.Text = "";
       ddlTypeOfLeave.SelectedIndex = 0;
       btnApplyLeave.Visible = false;
   }

   protected void btnCalculate_Click(object sender, EventArgs e)
   {
       if (txtAvailableCasualLeaves.Text != "" && txtAvailableEarnedLeaves.Text != "" && txtFromDate.Text != "" && txtToDate.Text != "")
       {
           
           string fDate = General.toMMDDYYYY(txtFromDate.Text).ToString();
           string toDate = General.toMMDDYYYY(txtToDate.Text).ToString();

           DateTime d1 = Convert.ToDateTime(fDate);
           DateTime d2 = Convert.ToDateTime(toDate);
           TimeSpan t = d2 - d1;
           double days = t.TotalDays + 1;
           txtTotalDaysOfLeave.Text = days.ToString();
           if(rbtnFullLeave.SelectedValue =="0")
           {
               days = days - 0.5;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           if (rbnFrom.SelectedValue == "0" && rbnTo.SelectedValue == "0")
           {
               days = days - 0.5;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           else if (rbnFrom.SelectedValue == "1" && rbnTo.SelectedValue == "1")
           {
               days = days - 0.5;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           else if (rbnFrom.SelectedValue == "1" && rbnTo.SelectedValue == "0")
           {
               days = days - 1;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           
           decimal totalLeaves = Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString());
           decimal casual = Convert.ToDecimal(txtAvailableCasualLeaves.Text.ToString());
           decimal earned = Convert.ToDecimal(txtAvailableEarnedLeaves.Text.ToString());
           decimal extra = Convert.ToDecimal(txtAvailableExtra.Text.ToString());
           if (ddlTypeOfLeave.SelectedValue == "1")
           {
               if (totalLeaves <= casual)
               {
                   btnApplyLeave.Visible = true;
               }
               else
               {
                   btnApplyLeave.Visible = false;
                   MessageBox.Show(this, "Please Make sure your Leave Count not to exceed your available Leaves");
                   txtToDate.Text = "";
                   txtTotalDaysOfLeave.Text = "";
                   ddlTypeOfLeave.SelectedIndex = 0;
               }
           }
           else if (ddlTypeOfLeave.SelectedValue == "2")
           {
               if (totalLeaves <= earned)
               {
                   btnApplyLeave.Visible = true;
               }
               else
               {
                   btnApplyLeave.Visible = false;
                   MessageBox.Show(this, "Please Make sure your Leave Count not to exceed your available Leaves");
                   txtToDate.Text = "";

               }
           }
           else if (ddlTypeOfLeave.SelectedValue == "3")
           {
               if (totalLeaves <= extra)
               {
                   btnApplyLeave.Visible = true;
               }
               else
               {
                   btnApplyLeave.Visible = false;
                   MessageBox.Show(this, "This Leave Cannot be submitted. Please Contact the HR");
                   txtToDate.Text = "";

               }
           }
           else
           {
               MessageBox.Show(this, "Please Select Any Type Of Leave");
               btnApplyLeave.Visible = false;
           }
       }
       else
       {
           MessageBox.Show(this, "Please Enter Valid Details");
       }
   }
   //protected void btnLeaveHistory_Click(object sender, EventArgs e)
   //{
   //    tblRecords.Visible = true;
   //    BindPendingLeaves();
   //    LoadGrid();
   //}
   protected void lbtnLeaveId_Click(object sender, EventArgs e)
   {

       //(GridViewRow gvr in gvPendingLeaves.Rows)
       {
           
           try
           {
               LinkButton lbtnLeaveId = (LinkButton)sender;
               lblLeaveIdTemp.Text = lbtnLeaveId.Text;
               GridViewRow gvr = (GridViewRow)lbtnLeaveId.Parent.Parent;
               gvPendingLeaves.SelectedIndex = gvr.RowIndex;

               Label lblAppliedDate = (Label)gvr.FindControl("lblAppliedDate");
               Label lblFromDate = (Label)gvr.FindControl("lblFromDate");
               Label lblToDate = (Label)gvr.FindControl("lblToDate");
               Label lblDaysofLeave = (Label)gvr.FindControl("lblDaysofLeave");
               lblNoOfLeaves.Text = lblDaysofLeave.Text;
               Label lblLeaveType = (Label)gvr.FindControl("lblLeaveType");
               lblTypeOfLeave.Text = lblLeaveType.Text; 
               Label lblReason = (Label)gvr.FindControl("lblReason");
               Label lblStatus = (Label)gvr.FindControl("lblStatus");
               Label lblAddressInLeave = (Label)gvr.FindControl("lblAddressInLeave");
               txtFromDate.Text = lblFromDate.Text;
               txtToDate.Text = lblToDate.Text;
               txtTotalDaysOfLeave.Text = lblDaysofLeave.Text;
               if (lblLeaveType.Text=="Casual Leave")
               {
                   ddlTypeOfLeave.SelectedIndex = 1;

               }
               else
               {
                   ddlTypeOfLeave.SelectedIndex = 2;
               }
               txtReason.Text = lblReason.Text;
               txtAddress.Text = lblAddressInLeave.Text;
               lblId.Text = lbtnLeaveId.Text.ToString();
               Label lblFromSess = (Label)gvr.FindControl("lblFromSession");
               Label lblToSess = (Label)gvr.FindControl("lblToSession");
               if (lblFromSess.Text == "Session1")
               {
                   rbnFrom.SelectedValue = "0";
               }
               else if (lblFromSess.Text == "Session2")
               {
                   rbnFrom.SelectedValue = "1";
               }
               if (lblToSess.Text == "Session1")
               {
                   rbnTo.SelectedValue = "0";
               }

               else if (lblToSess.Text == "Session2")
               {
                   rbnTo.SelectedValue = "1";
               }
               btnApplyLeave.Text = "Update";
               btnDelete.Visible = true;

           }
           catch (Exception ex)
           {
               MessageBox.Show(this, ex.Message.ToString());
           }

       }
   }

   protected void UpdateLeaveTblOnDelete()
   {

       try
       {


           if (lblTypeOfLeave.Text == "Casual Leave")
           {
               SqlCommand cmd = new SqlCommand("USP_UpdateEmpFinalLeaves", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@NoOfdays", lblNoOfLeaves.Text);
               cmd.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);

               con.Open();
               cmd.ExecuteNonQuery();
               con.Close();
           }
           else if (lblTypeOfLeave.Text == "Earned Leave")
           {
               SqlCommand cmd1 = new SqlCommand("USP_UpdateEmpFinalLeaves2", con);
               cmd1.CommandType = CommandType.StoredProcedure;
               cmd1.Parameters.AddWithValue("@NoOfdays", lblNoOfLeaves.Text);
               cmd1.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);

               con.Open();
               cmd1.ExecuteNonQuery();
               con.Close();
           }
           else if (lblTypeOfLeave.Text == "Extra Leave")
           {
               SqlCommand cmd1 = new SqlCommand("USP_UpdateEmpFinalLeaves3", con);
               cmd1.CommandType = CommandType.StoredProcedure;
               cmd1.Parameters.AddWithValue("@NoOfdays", lblNoOfLeaves.Text);
               cmd1.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);

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
   protected void btnDelete_Click(object sender, EventArgs e)
   {
       UpdateLeaveTblOnDelete();
       SqlCommand cmd2 = new SqlCommand("Delete from EMP_Leave_tbl where Leave_Id=@Leave_Id", con);
       cmd2.CommandType = CommandType.Text;
       cmd2.Parameters.AddWithValue("@Leave_Id", lblLeaveIdTemp.Text);
       con.Open();
       int i = cmd2.ExecuteNonQuery();
       con.Close();
       if(i>0)
       {
           MessageBox.Show(this, "Leave Application Deleted Successfully");
       }
       GetAvailableLeaves();
       ClearFields();
       BindPendingLeaves();
       btnDelete.Visible = false;
       btnApplyLeave.Text = "Apply Leave";
       lblId.Text = "";
   }

   protected void rbtnFullLeave_SelectedIndexChanged(object sender, EventArgs e)
   {
              
       if(txtTotalDaysOfLeave.Text != "")
       {
           if(rbtnFullLeave.SelectedValue == "0")
           {
               double ttldays = Convert.ToDouble(txtTotalDaysOfLeave.Text);
               ttldays = ttldays - 0.5;
               txtTotalDaysOfLeave.Text = ttldays.ToString();

           }
           else
           {
               double ttldays = Convert.ToDouble(txtTotalDaysOfLeave.Text);
               ttldays = ttldays + 0.5;
               txtTotalDaysOfLeave.Text = ttldays.ToString();
           }

       }
       
   }
   protected void rbnFrom_SelectedIndexChanged(object sender, EventArgs e)
   {

       btnApplyLeave.Visible = false;
   }
   protected void rbnTo_SelectedIndexChanged(object sender, EventArgs e)
   {
       btnApplyLeave.Visible = false;
   }
   protected void txtTotalDaysOfLeave_TextChanged(object sender, EventArgs e)
   {

   }
   protected void ddlTypeOfLeave_SelectedIndexChanged(object sender, EventArgs e)
   {
       if (txtAvailableCasualLeaves.Text != "" && txtAvailableEarnedLeaves.Text != "" && txtFromDate.Text != "" && txtToDate.Text != "")
       {

           string fDate = General.toMMDDYYYY(txtFromDate.Text).ToString();
           string toDate = General.toMMDDYYYY(txtToDate.Text).ToString();

           DateTime d1 = Convert.ToDateTime(fDate);
           DateTime d2 = Convert.ToDateTime(toDate);
           TimeSpan t = d2 - d1;
           double days = t.TotalDays + 1;
           txtTotalDaysOfLeave.Text = days.ToString();
           if (rbtnFullLeave.SelectedValue == "0")
           {
               days = days - 0.5;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           if (rbnFrom.SelectedValue == "0" && rbnTo.SelectedValue == "0")
           {
               days = days - 0.5;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           else if (rbnFrom.SelectedValue == "1" && rbnTo.SelectedValue == "1")
           {
               days = days - 0.5;
               txtTotalDaysOfLeave.Text = days.ToString();

           }
           else if (rbnFrom.SelectedValue == "1" && rbnTo.SelectedValue == "0")
           {
               days = days - 1;
               txtTotalDaysOfLeave.Text = days.ToString();

           }

           decimal totalLeaves = Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString());
           decimal casual = Convert.ToDecimal(txtAvailableCasualLeaves.Text.ToString());
           decimal earned = Convert.ToDecimal(txtAvailableEarnedLeaves.Text.ToString());
           decimal extra = Convert.ToDecimal(txtAvailableExtra.Text.ToString());
           if (ddlTypeOfLeave.SelectedValue == "1")
           {
               if (totalLeaves <= casual)
               {
                   btnCalculate.Visible = true;
               }
               else
               {
                   btnCalculate.Visible = false;
                   btnApplyLeave.Visible = false;
                   MessageBox.Show(this, "Please Make sure your Leave Count not to exceed your available Leaves");
                   txtToDate.Text = "";
                   txtTotalDaysOfLeave.Text = "";
                   ddlTypeOfLeave.SelectedIndex = 0;
               }
           }
           else if (ddlTypeOfLeave.SelectedValue == "2")
           {
               if (casual == 0)
               {
                   if (totalLeaves <= earned)
                   {
                       btnCalculate.Visible = true;
                   }
                   else
                   {
                       btnApplyLeave.Visible = false;
                       MessageBox.Show(this, "Please Make sure your Leave Count not to exceed your available Leaves");
                       txtToDate.Text = "";
                       txtTotalDaysOfLeave.Text = "";
                       ddlTypeOfLeave.SelectedIndex = 0;
                       btnCalculate.Visible = false;
                       btnApplyLeave.Visible = false;
                   }
               }
               else
               {
                   ddlTypeOfLeave.SelectedIndex = 0;
                   MessageBox.Show(this, "Please Attend your availble casual leaves First");
                   ddlTypeOfLeave.SelectedIndex = 0;

                   btnCalculate.Visible = false;
                   btnApplyLeave.Visible = false;
               }
           }
           else if (ddlTypeOfLeave.SelectedValue == "3")
           {
               if (casual == 0 && earned == 0)
               {
                   if (totalLeaves <= extra)
                   {
                       btnCalculate.Visible = true;
                   }
                   else
                   {
                       btnApplyLeave.Visible = false;
                       MessageBox.Show(this, "This Leave Cannot be submitted. Please Contact the HR");
                       ddlTypeOfLeave.SelectedIndex = 0;
                       txtTotalDaysOfLeave.Text = "";
                       txtToDate.Text = "";
                       btnCalculate.Visible = false;
                       btnApplyLeave.Visible = false;
                   }
               }
               else
               {
                   ddlTypeOfLeave.SelectedIndex = 0;
                   MessageBox.Show(this, "Please Attend your availble casual leaves & Earned Leaves First");
                   ddlTypeOfLeave.SelectedIndex = 0;

                   btnCalculate.Visible = false;
                   btnApplyLeave.Visible = false;
               }
           }
       }
   }
}