using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;

public partial class FPSLogin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnsignin_Click(object sender, EventArgs e)
    {
        btnsignin.Visible = false;
        btnSubmit.Visible = true;
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();
        lblotpmsg.Visible = true;
        lblOtp1.Visible = true;
        txtPassword.Visible = true;

        lblOtp.Text = RandomGenerator().ToString();
        lblMessage.Text = "Your OTP is : " + lblOtp.Text;

        SaveOtp();

        HR.SendSMS objsms = new HR.SendSMS();
        //Send MSG to Employee
        string msfEmp = "The OTP generated as according to the Login request by " + txtUserName .Text + " is - " + lblOtp.Text + ". VALUELINE";

          //HR Seema Mam Emp_Id=50
                objmas.EmployeeMaster_Select("50");
                string MD_MNo = objmas.AssignedMobileNo;
                //string MD_MNo = "9177700473";

                objsms.Send_OTPSMS(msfEmp, MD_MNo);
           
            
            MessageBox.Show(this, "Your FPS Page is not Registerd, Please enter OTP to Continue...");
        

        string ReturnUrl = Convert.ToString(Request.QueryString["url"]);

       

    }

    private void SaveOtp()
    {
        SqlCommand cmd = new SqlCommand("USP_InsertOtp", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserName", txtUserName.Text );
        cmd.Parameters.AddWithValue("@Otp", lblOtp.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected int RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        return intRandomNumber;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("select Otp from OTP_Tbl where UserName='" + txtUserName .Text + "'", con);
        cmd.Parameters.AddWithValue("@UserName", txtUserName .Text );
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblUserOtp.Text = dt.Rows[0][0].ToString();

            if (txtPassword.Text == lblUserOtp.Text)
            {
                Yantra.Authentication Login = new Yantra.Authentication();
                Login.FPSCheck(this, txtUserName.Text);
            }
            else
            {
                MessageBox.Show(this, "Please enter valid OTP");
            }
        }
    }
}