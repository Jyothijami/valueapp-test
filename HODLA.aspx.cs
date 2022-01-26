using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data;
using Yantra.Classes;
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
public partial class LeaveApprove : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    int i;
    string pagenavigationstr, pagevar1, leaveidvar2;
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            //Response.Redirect("~/MobileLogin.aspx");
            Response.Redirect("~/MobileLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));  

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblLeaveId.Text = Request.QueryString["LId"];
        if (!IsPostBack)
        {
            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
            objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
            lblDeptId.Text = objmas.DeptID;
            lblEmpIdHidden.Text = objmas.EmpID;
            lblHOD_Id.Text = objmas.Dept_Hod_Id;

            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            //setControlsVisibility();

            HR.EmpLeave obj = new HR.EmpLeave();
            if (obj.EmpLeave_Select(Request.QueryString["LId"]) > 0)
            {
                lblEMpName.Text = obj.EmpName;
                lblFrmDt.Text = obj.FromDate;
                lblToDt.Text = obj.ToDate;
                lblLeaveType.Text  = obj.LeaveType;
                lblReason.Text = obj.ReasonforLeave;
                lblAdd.Text = obj.AddressofLeavePeriod;
                lblMobile.Text = obj.MobileNo;

                if (obj.Status1 !="Pending")
                {
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                }
                
            }
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "116");
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "68");
        //btnDelete.Enabled = up1.Delete;
        //btnSearch.Enabled = up.Search;
        btnApprove.Enabled = up.Approve;
        btnReject.Enabled = up.Approve;
    }
    protected void btnHODApprove_Click(object sender, EventArgs e)
    {
        i = 1;
        LeaveApprove1();
    }
    private void LeaveApprove1()
    {
        if (Request.QueryString["LId"] != "")
            {
                try
                {
                    

                    SqlCommand cmd = new SqlCommand("USP_UpdateHODApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", lblLeaveId .Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status1", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Comment1", "HR");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");
                        
                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status1", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Comment1", "-");

                        cmd.Parameters.AddWithValue("@Rejected_By", "HOD");
                        
                    }
                    //Label lblMobile = (Label)gvr.FindControl("lblMobileNo");
                    //Label lblEmpName = (Label)gvr.FindControl("lblName");
                    //Label lbltypeOfLeave = (Label)gvr.FindControl("lblTypeOfLeave");
                    //Label lblFromDate = (Label)gvr.FindControl("lblFromDate");
                    //Label lblToDate = (Label)gvr.FindControl("lblToDate");

                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i == 1)
                    {

                        if (j > 0)
                        {
                            HR.SendSMS objsms = new HR.SendSMS();
                            pagenavigationstr = "http://183.82.108.55/HRLA.aspx?LID=" + lblLeaveId.Text + "";
                            pagevar1 = "HRLA.aspx?LID=";
                            leaveidvar2 = lblLeaveId.Text + " " + lblEMpName.Text; ;
                            string msgHR = "http://183.82.108.55/" + pagevar1 + leaveidvar2 + " has applied for a " + lblLeaveType.Text + " from " + lblFrmDt.Text + " to " + lblToDt.Text + ".  VALUELINE";
                           
                            //string msgHR = "" + pagenavigationstr + " "+ lblEMpName.Text + " has applied for a " + lblLeaveType.Text + " from " + lblFrmDt.Text + " to " + lblToDt.Text + ". Please use the link to approve /reject the leave.";
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            //HR Emp_Id=90
                            objmas.EmployeeMaster_Select("90");
                            string HR_MNo = objmas.AssignedMobileNo;
                            //string HR_MNo = "9059638293";
                            objsms.Send_LeaveApprovalSMS(msgHR, HR_MNo);
                            MessageBox.Show(this, "Leave got Approved and forwarded to HR");
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                        }
                    }
                    else if (i == 0)
                    {
                        if (j > 0)
                        {
                            HR.SendSMS objsms = new HR.SendSMS();
                            ////Send MSG to Employee
                            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                            objmas.EmployeeMaster_Select(lblEmpIdHidden.Text);
                            lblHod_Name.Text = objmas.EmpFirstName + " " + objmas.EmpLastName;

                            string msfEmp = "Your leave dated " + lblFrmDt .Text + " to " + lblToDt.Text + " has been rejected by " + lblHod_Name.Text + ". Please contact the HR department for any quarries. VALUELINE";
                            //string emp_MNo = "9059638293";
                            string emp_MNo = lblMobile.Text;
                            objsms.Send_RejectedLeaveSMS(msfEmp, emp_MNo);
                            MessageBox.Show(this, "Leave got Rejected");
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        
    }
    protected void btnHODReject_Click(object sender, EventArgs e)
    {
        i = 0;
        LeaveApprove1();
    }
}