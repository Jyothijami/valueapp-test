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
            setControlsVisibility();

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
                lblEmpId.Text = obj.EmpId;
                if (obj.Status2 != "Pending")
                {
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                }
                
            }
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "68");
        //btnDelete.Enabled = up1.Delete;
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "117");
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


                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Leave_Id", lblLeaveId.Text);
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
                    //Label lblMobile = (Label)gvr.FindControl("lblMobileNo");
                    //Label lblEmpName = (Label)gvr.FindControl("lblName");
                    //Label lbltypeOfLeave = (Label)gvr.FindControl("lblTypeOfLeave");
                    //Label lblFromDate = (Label)gvr.FindControl("lblFromDate");
                    //Label lblToDate = (Label)gvr.FindControl("lblToDate");
                    General OBJ = new General();

                    int i1 = General.CountofRecordsWithQuery("select COUNT(*) from YANTRA_DEPT_MAST  where DEPT_HEAD =" + lblEmpId.Text);

                    if (i1 > 0)
                    {
                        con.Open();
                        int j = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i == 1)
                        {

                            if (j > 0)
                            {
                                HR.SendSMS objsms = new HR.SendSMS();
                                pagenavigationstr = "http://183.82.108.55/MDLA.aspx?LID=" + lblLeaveId.Text + "";
                                pagevar1 = "MDLA.aspx?LID=";
                                leaveidvar2 = lblLeaveId.Text + " " + lblEMpName.Text;
                                string msgMD = "http://183.82.108.55/" + pagevar1 + leaveidvar2 + " has applied for a " + lblLeaveType.Text + " from " + lblFrmDt.Text + " to " + lblToDt.Text + ".  VALUELINE";
                           
                                //string msgMD = "" + pagenavigationstr + " " + lblEMpName.Text + " has applied for a " + lblLeaveType.Text + " from " + lblFrmDt.Text + " to " + lblToDt.Text + ". Please use link to approve /reject the leave.";
                                HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                                //MD Emp_Id=50
                                objmas.EmployeeMaster_Select("89");
                                //string MD_MNo = "9059638293";
                                string MD_MNo = objmas.AssignedMobileNo;
                                objsms.Send_LeaveApprovalSMS(msgMD, MD_MNo);
                                MessageBox.Show(this, "Leave got Approved and forwarded to MD");
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
                                objmas.EmployeeMaster_Select("90");
                                lblHR_Name.Text = objmas.EmpFirstName + " " + objmas.EmpLastName;

                                string msgEmp = "Your leave dated " + lblFrmDt.Text + " to " + lblToDt.Text + " has been rejected by " + lblHR_Name.Text + ". Please contact the HR department for any quarries regarding the same.";
                                //string emp_MNo = "9059638293";
                                string emp_MNo = lblMobile.Text;
                                objsms.Send_App_SMS(msgEmp, emp_MNo);
                                MessageBox.Show(this, "Leave got Rejected");
                                btnApprove.Enabled = false;
                                btnReject.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        con.Open();
                        int j = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i == 1)
                        {

                            if (j > 0)
                            {
                                HR.SendSMS objsms = new HR.SendSMS();
                                pagenavigationstr = "http://183.82.108.55/MDLA.aspx?LID=" + lblLeaveId.Text + "";

                                string msgMD = "" + pagenavigationstr + " " + lblEMpName.Text + " has applied for a " + lblLeaveType.Text + " from " + lblFrmDt.Text + " to " + lblToDt.Text + ". Please use link to approve /reject the leave.";
                                HR.EmployeeMaster objmas = new HR.EmployeeMaster();
                                //MD Emp_Id=50
                                objmas.EmployeeMaster_Select("50");
                                //string MD_MNo = "9059638293";
                                string MD_MNo = objmas.AssignedMobileNo;
                                objsms.Send_App_SMS(msgMD, MD_MNo);
                                MessageBox.Show(this, "Leave got Approved and forwarded to MD");
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
                                objmas.EmployeeMaster_Select("90");
                                lblHR_Name.Text = objmas.EmpFirstName + " " + objmas.EmpLastName;

                                string msgEmp = "Your leave dated " + lblFrmDt.Text + " to " + lblToDt.Text + " has been rejected by " + lblHR_Name.Text + ". Please contact the HR department for any quarries regarding the same.";
                                //string emp_MNo = "9059638293";
                                string emp_MNo = lblMobile.Text;
                                objsms.Send_App_SMS(msgEmp, emp_MNo);
                                MessageBox.Show(this, "Leave got Rejected");
                                btnApprove.Enabled = false;
                                btnReject.Enabled = false;
                            }
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