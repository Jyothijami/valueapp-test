using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DatumDAL;
using Yantra.MessageBox;
using System.IO;
using System.Data.SqlClient;
using vllib;
public partial class Modules_HR_EmployeeEnrollment : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();

            gvEnrollmentDtls.DataBind();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "86");

        btnNew.Enabled = up.add;
        btnAccept.Enabled = up.Approve;
        btnReject.Enabled = up.Delete;
        btnSave.Enabled = up.add;
        //btnClose.Enabled = up.Close;
        

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EmployeeEnrollmentSave();
        tblEmployeeDetails.Visible = false;
        gvEnrollmentDtls.DataBind();
        gvHistory.DataBind();
    }

    private void EmployeeEnrollmentSave()
    {
        try
        {
            if (fuResume.HasFile)
            {
                HR.EmpLeave.Enrollment obj    = new HR.EmpLeave.Enrollment();
                
                obj.EnrollmentDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text.Trim());
                //obj.EnrollmentDate = txtDate.Text;
                obj.FName = txtFname.Text;
                obj.MName = txtMiddleName.Text;
                obj.LName = txtLastName.Text;
                obj.MobileNo = txtMobileNo.Text;
                obj.EmailId = txtEmail.Text;
                obj.Address = txtAddress.Text;
                string path = Server.MapPath("~/Content/Resumes/");
                string fileName = fuResume.PostedFile.FileName;
                fuResume.SaveAs(path + fileName);
                obj.Resume = fuResume.PostedFile.FileName;
                obj.Education = txtEducation.Text;
                //obj.DateOfBirth = txtDOB.Text;
                obj.DateOfBirth= Yantra.Classes.General.toMMDDYYYY(txtDOB.Text);
                GetAge();
                obj.Age = lblAge.Text;
                obj.Status = "Pending";
                MessageBox.Show(this, obj.Enrollment_Save());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            HR.ClearControls(this);
        }
    }

    private void GetAge()
    {
        string s = Yantra.Classes.General.toMMDDYYYY(txtDOB.Text);
        //string s = txtDOB.Text;
        DateTime dob = Convert.ToDateTime(s);
        DateTime currentdate = Convert.ToDateTime(DateTime.Now);
        TimeSpan time = currentdate.Subtract(dob);
        int total = (time.Days) / 365;
        lblAge.Text = total.ToString();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        tblEmployeeDetails.Visible = true;
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        EnrollmentApproval();
        gvEnrollmentDtls.DataBind();
        gvHistory.DataBind();
    }

    private void EnrollmentApproval()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label Id = (Label)gvr.FindControl("lblNo");
                    HR.EmpLeave.Enrollment obj = new HR.EmpLeave.Enrollment();
                    obj.Status = "Approved";
                    obj.EnrollmentId = Id.Text;
                    MessageBox.Show(this, obj.EnrollmentApprove_Update());
                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                }
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label Id = (Label)gvr.FindControl("lblNo");
                    HR.EmpLeave.Enrollment obj = new HR.EmpLeave.Enrollment();
                    obj.Status = "Rejected";
                    obj.EnrollmentId = Id.Text;
                    MessageBox.Show(this, obj.EnrollmentApprove_Update());
                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                }
                gvEnrollmentDtls.DataBind();
                gvHistory.DataBind();
            }
        }
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
 
   
   
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
       lblSearchItemHidden2.Text = ddlSearchBy.SelectedValue;
       lblSearchValueHidden2.Text = txtSearchText.Text;
        gvHistory.DataBind();
      
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (fuResume.HasFile)
    //    {

    //        try
    //        {

    //            string filename = path.GetFileName(fuResume.FileName);

    //            fuResume.SaveAs(Server.MapPath("~/") + filename);

    //        }

    //        catch (Exception ex)
    //        {

    //            //write error handling code

    //        }

    //    }

    //}
    //protected void btnUpload_Click1(object sender, EventArgs e)
    //{
    //    //if (fuResume.HasFile)
    //    //{
    //    //    string filename = path.GetFileName(fuResume.FileName);

    //    //    fuResume.SaveAs(Server.MapPath("~/Modules/HR/Resumes/") + filename);
    //    //}
    //    //if (fuResume.HasFile)
    //    //{

    //    //    fuResume.SaveAs(@"/Modules/HR/Resumes" + fuResume.FileName);
    //    //    lblUpload.Text = "File Uploaded: " + fuResume.FileName;
    //    //}
    //    //else
    //    //{
    //    //    lblUpload.Text = "No File Uploaded.";
    //    //}
    //    //if (!System.IO.Directory.Exists(Server.MapPath("Resumes")))
    //    //{
    //    //    System.IO.Directory.CreateDirectory(Server.MapPath("Resumes"));
    //    //}

    //    //if (fuResume.HasFile)
    //    //{
    //    //    fuResume.SaveAs(Server.MapPath("Resumes\\") + fuResume.FileName);
    //    //    lblUpload.Text = "File Uploaded:" + fuResume.FileName;
    //    //}
    //    //HttpPostedFile file = Request.Files["myFile"];

    //    ////check file was submitted
    //    //if (file != null && file.ContentLength > 0)
    //    //{
    //    //    string fname = Path.GetFileName(file.FileName);
    //    //    file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
    //    //}
    //    if (fuResume.PostedFile.FileName == "")
    //    {
    //        lblUpload.Text = "No file specified.";
    //    }
    //    else
    //    {
    //        try
    //        {
    //            string serverFileName = Path.GetFileName(fuResume.PostedFile.FileName);
    //            //FileInput.PostedFile.SaveAs(@"c:\" + serverFileName);
    //            fuResume.PostedFile.SaveAs(MapPath(".") + serverFileName);
    //            lblUpload.Text = "File " + serverFileName;
    //            lblUpload.Text += " uploaded successfully.";
    //        }
    //        catch (Exception err)
    //        {
    //            lblUpload.Text = err.Message;
    //        }
    //    }
    //}
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvHistory.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvHistory.DataBind();
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblEmployeeDetails.Visible = false;
    }
}