using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraDAL;
using vllib;
using Yantra.MessageBox;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Yantra.Classes;

public partial class IPCheck : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    string UserName,sql;
    protected void Page_Load(object sender, EventArgs e)
    {
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();
        UserName = Request.QueryString["UserName"].ToString();
        //objmas.UserName_Select(UserName);
        //string UserId = objmas.UserId;

        if (!IsPostBack)
        {
            lblOtp.Text = RandomGenerator().ToString();
            lblMessage.Text = "Your OTP is : " + lblOtp.Text;

            SaveOtp();
        //    lblUserName.Text = Request.QueryString["UserName"].ToString();

            HR.SendSMS objsms = new HR.SendSMS();
            //Send MSG to Employee
            string msfEmp = "The OTP generated as according to the login request by " + UserName + " is - " + lblOtp.Text + ". VALUELINE";

            if (UserName  == "venu")
            {
                objmas.EmployeeMaster_Select("184");
                string Venu_No = objmas.AssignedMobileNo;
                objsms.Send_OTPSMS(msfEmp, Venu_No);
            }
            else if (UserName == "lakshmikanth")
            {
                objmas.EmployeeMaster_Select("11");
                string Kanth_No = objmas.AssignedMobileNo;
                objsms.Send_OTPSMS(msfEmp, Kanth_No);
            }
            else if (UserName == "bvenkat")
            {
                objmas.EmployeeMaster_Select("314");
                string Venkat_No = objmas.AssignedMobileNo;
                objsms.Send_OTPSMS(msfEmp, Venkat_No);
            }
            else if (UserName == "jami")
            {
                objmas.EmployeeMaster_Select("314");
                string Jami_No = objmas.AssignedMobileNo;
                objsms.Send_OTPSMS(msfEmp, Jami_No);
            }
            else if (UserName == "saikumar")
            {
                objmas.EmployeeMaster_Select("25");
                string Saino = objmas.AssignedMobileNo;
                objsms.Send_OTPSMS(msfEmp, Saino);
            }
            else if (UserName == "naveenp")
            {
                objmas.EmployeeMaster_Select("301");
                string naveen = objmas.AssignedMobileNo;
                objsms.Send_OTPSMS(msfEmp, naveen);
            }
            else
            {
                //HR Seema Mam Emp_Id=50
                objmas.EmployeeMaster_Select("50");
                string MD_MNo = objmas.AssignedMobileNo;
                //string MD_MNo = "9177700473";

                objsms.Send_OTPSMS(msfEmp, MD_MNo);
            }
            //string MD_MNo = "9177700473";
            //string MD_MNo = "9949066678";
            //string MD_MNo = "9059638293";
            
            MessageBox.Show(this, "Your IP is not Registerd, Please enter OTP to Continue...");
        }
    }

    private void SaveOtp()
    {
        SqlCommand cmd = new SqlCommand("USP_InsertOtp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserName", UserName);
        cmd.Parameters.AddWithValue("@Otp", lblOtp.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("select Otp from OTP_Tbl where UserName='" + UserName + "'", con);
        cmd.Parameters.AddWithValue("@UserName", UserName);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblUserOtp.Text = dt.Rows[0][0].ToString();

            if (txtOtp.Text == lblUserOtp.Text)
            {
                Yantra.Authentication Login = new Yantra.Authentication();
                Login.IPCheck(this, UserName);
            }
            else
            {
                MessageBox.Show(this, "Please enter valid OTP");
            }
        }
    }
    protected int RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        return intRandomNumber;
    }
}