using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using vllib;
using System.IO;


public partial class Modules_Profiles_empProfile : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();
        objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
        lblEmpId.Text = objmas.EmpID;
        BindData();
    }

    private void BindData()
    {
        //SqlCommand cmd = new SqlCommand("select EMP_FIRST_NAME, EMP_MIDDLE_NAME, EMP_LAST_NAME, EMP_DOB, EMP_PHOTO, EMP_EMAIL, EMP_MOBILE from YANTRA_EMPLOYEE_MAST where EMP_ID='"+lblEmpId.Text+"'", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //lblFname.Text = dt.Rows[0][0].ToString();
        //lblMName.Text = dt.Rows[0][1].ToString();
        //lblLName.Text = dt.Rows[0][2].ToString();
        //lblDOB.Text = dt.Rows[0][3].ToString();
        //Image1.ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + lblEmpId.Text + "";
        //lblEmail.Text = dt.Rows[0][5].ToString();
        //lblMobileNo.Text = dt.Rows[0][6].ToString();
        SqlCommand cmd = new SqlCommand("USP_HR_EMPLOYEE_PERSONAL_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMP_ID", lblEmpId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        dlt.DataSource = dt;
        dlt.DataBind();
       // //##########
        SqlCommand cmd1 = new SqlCommand("select EMP_PHOTO from YANTRA_EMPLOYEE_MAST where EMP_ID='" + lblEmpId.Text + "'", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        Image1.ImageUrl = "~/Content/EmployeeImage/" + dt1.Rows[0][0] + "";

    }

    protected string getUserID()
    {
        return Request.QueryString["id"];
    }

    protected void btnProfileChange_Click(object sender, EventArgs e)
    {
        HR.EmployeeMaster obj = new HR.EmployeeMaster();

        #region Item Images
        if (fileProfilePic.HasFiles)
        {


            string itemimage = "";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeImage"))
            {
                foreach (HttpPostedFile uploadedFile in fileProfilePic.PostedFiles)
                {

                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 99999));
                    string path = Server.MapPath("~/Content/EmployeeImage/");
                    string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                    itemimage = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    obj.Emp_photo = itemimage;
                    obj.Emp_Img_Update(lblEmpId.Text);

                    //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;

                }
            }

            else
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeImage");
                foreach (HttpPostedFile uploadedFile in fileProfilePic.PostedFiles)
                {

                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 10000));
                    string path = Server.MapPath("~/Content/EmployeeImage/");
                    string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                    itemimage = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    obj.Emp_photo = itemimage;
                    //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;
                    obj.Emp_Img_Update(lblEmpId.Text);

                }

            }

        }
        //else
        //{
        //    MessageBox.Show(this, "Please Provide An Employee Image");
        //}
        #endregion

        BindData();
    }
}
 
