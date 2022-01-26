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
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.Data.SqlClient;
using vllib;
using System.IO;
using YantraDAL;
public partial class Modules_HRManagement_EmployeeMaster : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    string EnrollmentId = "";
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

        EnrollmentId = Request.QueryString["EnrollId"];
      //  CalendarExtender3.StartDate = DateTime.Now.AddDays(2);
        if (!IsPostBack)
        {
            Department_Fill();
            Designation_Fill();
            EmployeeType_Fill();
            BranchName_Fill();
            //CompanyName_Fill();

            if(EnrollmentId!=null)
            {
                tblEmployeeDetails.Visible = true;
                //SqlCommand cmd = new SqlCommand("USP_BindEnrollmentMaster", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@EnrollmentId", EnrollmentId);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    txtFirstName.Text = dt.Rows[0][0].ToString();
                //    txtMiddleName.Text = dt.Rows[0][1].ToString();
                //    txtLastName.Text = dt.Rows[0][2].ToString();
                //    txtMobileNo.Text = dt.Rows[0][3].ToString();
                //    txtEmail.Text = dt.Rows[0][4].ToString();
                //    txtAddress.Text = dt.Rows[0][5].ToString();
                //    txtDateOfBirth.Text = dt.Rows[0][6].ToString();
                //    txtGrosssalary.Text = dt.Rows[0][7].ToString();
                //    ddlCompany.SelectedItem.Text = dt.Rows[0][8].ToString();
                //    ddlDepartment.SelectedItem.Text = dt.Rows[0][9].ToString();
                //    ddlDesignation.SelectedItem.Text = dt.Rows[0][10].ToString();
                //    txtDateOfAppointment.Text = dt.Rows[0][11].ToString();
                //}
                try
                {
                    HR.EmpLeave.Enrollment obj = new HR.EmpLeave.Enrollment();
                    if (obj.EnrollmentMaster_Select(EnrollmentId) > 0)
                    {
                        txtFirstName.Text = obj.FName;
                        txtMiddleName.Text = obj.MName;
                        txtLastName.Text = obj.LName;
                        txtMobileNo.Text = obj.MobileNo;
                        txtEmail.Text = obj.EmailId;
                        txtAddress.Text = obj.Address;
                        txtDateOfBirth.Text = obj.DateOfBirth;
                        txtGrosssalary.Text = obj.GrossSalary;
                        txtDateOfAppointment.Text = obj.Doj;
                        ddlCompany.SelectedValue = obj.CompanyId;
                        ddlDepartment.SelectedValue = obj.DeptId;
                        ddlDesignation.SelectedValue = obj.DesignId;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    HR.Dispose();
                }
            }

          
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            BindSearchGrid(); 
            //gvEmployeeMaster.DataBind();
        }
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "47");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnImage.Enabled = up.AddImage;
        btnPrint.Enabled = up.Print;
        //Button1.Enabled = up.EmpData;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        //btnLeaveMaster.Enabled = up.LeaveMaster;


    }

    #region Department Fill
    public void Department_Fill()
    {
        try
        {
            Masters.Department.Department_Select(ddlDepartment);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Designation Fill
    public void Designation_Fill()
    {
        try
        {
            Masters.Designation.Designation_Select(ddlDesignation);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region EmployeeType Fill
    public void EmployeeType_Fill()
    {
        try
        {
            Masters.EmployeeType.EmployeeType_Select(ddlEmployeeType);
            HR.EmployeeMaster.EmployeeMaster_Select (ddlReportingTo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region BranchName Fill
    public void BranchName_Fill()
    {
        try
        {
            //Masters.Branch.Branch_Select(ddlBranch);
            Masters.RegionalMaster.RegionalMaster_Select(ddlBranch);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }

    }
    #endregion

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }

    }
    #endregion

    #region Save Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Save
        if (btnSave.Text == "Save")
        {
            int index = 0;

            foreach (ListItem a in chklDocumentsSubmitted.Items)
            {
                 if (a.Selected == true)
                    index++;
            }


            if (index < 1)
            {
                lblchkllist.Visible = true;
            }
            else
            {
                 HR.EmployeeMaster objEM = new HR.EmployeeMaster();
                 if (Convert.ToInt32(objEM.EmployeeUserName_Select(txtUserName.Text)) > 0)
                 {
                     MessageBox.Show(this, "UserName not Valid");
                 }
                 
                 else
                 {
                     lblchkllist.Visible = false;
                     EmployeeMasterSave();
                     HR.ClearControls(this);
                     tblEmployeeDetails.Visible = false;
                 }
            }


        }
        #endregion

        #region Update 
        else if (btnSave.Text == "Update")
        {
            int index = 0;

            foreach (ListItem a in chklDocumentsSubmitted.Items)
            {
                if (a.Selected == true)
                    index++;
            }


            if (index < 1)
            {
                lblchkllist.Visible = true;
            }

            else
            {
                HR.EmployeeMaster objEM = new HR.EmployeeMaster();
                //if (Convert.ToInt32(objEM.EmployeeUserName_Select(txtUserName.Text)) > 0)
                //{
                //    MessageBox.Show(this, "UserName not Valid");
                //}

                //else
                //{
                lblchkllist.Visible = false;
                EmployeeMasterUpdate();
                HR.ClearControls(this);
                tblEmployeeDetails.Visible = false;
                //}
            }
        }
        #endregion
               

        if (EnrollmentId != null)
        {
            AcceptApproval();
        }
    }

    private void AcceptApproval()
    {
        SqlCommand cmd = new SqlCommand("USP_UpdateOfferStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentId", EnrollmentId);
        cmd.Parameters.AddWithValue("@OfferStatus", "Accepted");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    #endregion

    #region EmployeeMasterSave
    private void EmployeeMasterSave()
    {
        
        try
        {
           
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            //HR.BeginTransaction();
            obj.EmpFirstName = txtFirstName.Text;
            obj.EmpMiddleName = txtMiddleName.Text;
            obj.EmpLastName = txtLastName.Text;
            if (rbtMale.Checked == true)
            {
                obj.EmpGender = rbtMale.Text;
            }
            else
            {
                obj.EmpGender = rbtFemale.Text;
            }
            obj.EmpMobile = txtMobileNo.Text;
            obj.EmpDOB = Yantra.Classes.General.toMMDDYYYY(txtDateOfBirth.Text);
            obj.EmpEMail = txtEmail.Text;
            obj.EmpAddress = txtAddress.Text;
            obj.EmpCity = txtCity.Text;
            obj.EmpPhone = txtPhoneNo.Text;
            obj.DeptID = ddlDepartment.SelectedItem.Value;
            obj.DesgID = ddlDesignation.SelectedItem.Value;
            obj.BranchId = ddlBranch.SelectedItem.Value;
            obj.EmpDetDOJ = Yantra.Classes.General.toMMDDYYYY(txtDateOfAppointment.Text);
            obj.EmpDetDOT = Yantra.Classes.General.toMMDDYYYY(txtDateOfTermination.Text);
            obj.EmpTypeID = ddlEmployeeType.SelectedItem.Value;
            //obj.tEmpPhoto = "xxx.jpeg";
            #region Emp Images
            if (EmpImage.HasFiles)
            {


                string itemimage = "";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeImage"))
                {
                    foreach (HttpPostedFile uploadedFile in EmpImage.PostedFiles)
                    {

                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 99999));
                        string path = Server.MapPath("~/Content/EmployeeImage/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        itemimage = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        obj.tEmpPhoto = itemimage;
                        //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;

                    }
                }

                else
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeImage");
                    foreach (HttpPostedFile uploadedFile in EmpImage.PostedFiles)
                    {

                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/Content/EmployeeImage/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        itemimage = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        obj.tEmpPhoto = itemimage;

                        //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;
                    }

                }

            }
            else
            {
                MessageBox.Show(this, "Please Provide An Employee Image");
            }
            #endregion
            obj.EMPUserName = txtUserName.Text;
            obj.EmpCompany = ddlCompany.SelectedItem.Value;
            obj.ReportingId = ddlReportingTo.SelectedItem.Value;
            obj.servicebond = txtYears.Text;
            obj.PerPhoneno = txtPerPhoneNo.Text;  ///PF Number
            obj.permobileno = txtPerMobileNo.Text;
            obj.emrname = txtemrName.Text;
            obj.emrrelation = txtemrRelationship.Text;
            obj.emgphone = txtemrPhone.Text;
            obj.emgaddress = txtemrAddress.Text;
            obj.Others = txtOthers.Text;
            obj.FATHERNAME = txtfathername.Text;
            obj.AssignedEmailId = txtAssignedEmail.Text;
            obj.AssignedMobileNo = txtAssignedMobile.Text;
            obj.AssignedEmpId = txtAssignedEmpId.Text;
            obj.AssignedAccNo = txtAssignedAccNo.Text;
            obj.InsuraceInfo = txtInsurance.Text;
            obj.GrossSal = txtGrosssalary.Text;
            obj.AssignedBankName = txtBankName.Text;
            obj.InsuranceCompanyName = txtInsuranceCompany.Text;
            obj.Cp_Id = cp.getPresentCompanySessionValue();
            obj.Asset1 = txtAsset1.Text;
            obj.Asset2 = txtAsset2.Text;
            obj.Asset3 = txtAsset3.Text;
            obj.Asset4 = txtAsset4.Text;
            obj.Asset5 = txtAsset5.Text;
            obj.Asset6 = txtAsset6.Text;
            ////obj.CasualLeaves = txtNoofCasualLeaves.Text;
            ////obj.SickLeaves = txtNoofsickleaves.Text;
            ////obj.LeavesEarned = txtNoofLeavesEarned.Text;
            if (txtAsset2.Checked == true)
            {
                obj.Asset2 = "NO-PF";
            }
            if (txtAsset5.Checked == true)
            {
                obj.Asset5 = "NO-Bonus";
            }
            if (txtAsset4.Checked == true)
            {
                obj.Asset4 = "Exclude Paysheet";
            }
            if (rdbActive.Checked == true)
            {
                obj.Status = "1";
            }
            else
            {
                obj.Status = "0";
            }
            

            if (obj.Empoyee_Save() == "Data Saved Successfully")
            {
                for (int i = 0; i < chklDocumentsSubmitted.Items.Count; i++)
                {
                    if (chklDocumentsSubmitted.Items[i].Selected == true)
                    {
                        obj.DocSubmit = chklDocumentsSubmitted.Items[i].Value;
                        obj.Empdoc_save();
                    }
                }

                // Saving Uploaded Documents
                string CD = DateTime.Now.ToString("MM/dd/yyyy");

                if (docSubmitted.HasFiles)
                {
                    #region Employee Documents

                    string empDoc = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments"))
                    {
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.EmpDocDetails_Save();
                        }
                    }

                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments");
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.DocSubmit1 = chklDocumentsSubmitted.SelectedItem.Value;
                            obj.EmpDocDetails_Save();
                        }

                    }
                    #endregion
                }

                #region Employee Age
                string emp_Id = obj.NewEmp_Id();
                int age = obj.GetAge(Convert.ToInt32(emp_Id));
                #endregion
                #region Salary BreakUp

                #region Calculation
                decimal Basic = 0;
                decimal HRA = 0;
                decimal CA = 0;
                decimal Other_Allowance = 0;
                decimal PF = 0;
                decimal MedicalAllowance = 0;
                decimal PT = 0;
                decimal salary = Convert.ToDecimal(txtGrosssalary.Text) / 12;
                SqlCommand cmd = new SqlCommand("[Usp_GetCTCAge]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@salary_fix", salary);
                cmd.Parameters.AddWithValue("@Age", age);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Basic = Math.Round(Convert.ToDecimal(dt.Rows[0][0]));
                    HRA = Math.Round(Convert.ToDecimal(dt.Rows[0][1]));
                    CA = Math.Round(Convert.ToDecimal(dt.Rows[0][2]));
                    MedicalAllowance = Math.Round(Convert.ToDecimal(dt.Rows[0][3]));
                    Other_Allowance = Math.Round(Convert.ToDecimal(dt.Rows[0][4]));
                    PF = Math.Round(Convert.ToDecimal(dt.Rows[0][6]));
                    PT = Math.Round(Convert.ToDecimal(dt.Rows[0][8]));

                #endregion
                    // string emp_Id = obj.NewEmp_Id();
                    SqlCommand cmd1 = new SqlCommand("[USP_SalaryDetailsNew]", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Emp_ID", emp_Id);
                    cmd1.Parameters.AddWithValue("@Basic", Basic);
                    cmd1.Parameters.AddWithValue("@HRA", HRA);
                    cmd1.Parameters.AddWithValue("@CA", CA);
                    cmd1.Parameters.AddWithValue("@Other_Allowance", Other_Allowance);
                    cmd1.Parameters.AddWithValue("@PF", PF);
                    cmd1.Parameters.AddWithValue("@MedicalAllowance", MedicalAllowance);
                    cmd1.Parameters.AddWithValue("@PT", PT);

                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
                #endregion
                //HR.CommitTransaction();
                MessageBox.Show(this, "Data Saved Successfully");
                chklDocumentsSubmitted.ClearSelection();
            }
            else
            {
                //HR.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            //HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblEmployeeDetails.Visible = false;
            BindSearchGrid(); 
            //gvEmployeeMaster.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }
    #endregion

    #region EmployeeMasterUpdate
    private void EmployeeMasterUpdate()
    {
        btnSave.Enabled = true;
        try
        {
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            //HR.BeginTransaction();

            obj.EmpFirstName = txtFirstName.Text;
            obj.EmpMiddleName = txtMiddleName.Text;
            obj.EmpLastName = txtLastName.Text;
            if (rbtMale.Checked == true)
            {
                obj.EmpGender = rbtMale.Text;
            }
            else
            {
                obj.EmpGender = rbtFemale.Text;
            }
            obj.EmpMobile = txtMobileNo.Text;
            obj.EmpDOB = Yantra.Classes.General.toMMDDYYYY(txtDateOfBirth.Text);
            obj.EmpEMail = txtEmail.Text;
            obj.EmpAddress = txtAddress.Text;
            obj.EmpCity = txtCity.Text;
            obj.EmpPhone = txtPhoneNo.Text;
            obj.DeptID = ddlDepartment.SelectedItem.Value;
            obj.DesgID = ddlDesignation.SelectedItem.Value;
            obj.BranchId = ddlBranch.SelectedItem.Value;
            obj.EmpDetDOJ = Yantra.Classes.General.toMMDDYYYY(txtDateOfAppointment.Text);
            obj.EmpDetDOT = Yantra.Classes.General.toMMDDYYYY(txtDateOfTermination.Text);
            obj.EmpTypeID = ddlEmployeeType.SelectedItem.Value;
           // obj.tEmpPhoto = "xxx.jpeg";
            obj.EMPUserName = txtUserName.Text;
            obj.EmpCompany = ddlCompany.SelectedItem.Value;
            obj.servicebond = txtYears.Text;
            obj.PerPhoneno = txtPerPhoneNo.Text;
            obj.permobileno = txtPerMobileNo.Text;
            obj.emrname = txtemrName.Text;
            obj.emrrelation = txtemrRelationship.Text;
            obj.emgphone = txtemrPhone.Text;
            obj.emgaddress = txtemrAddress.Text;
            obj.Others = txtOthers.Text;
            obj.FATHERNAME = txtfathername.Text;
            obj.AssignedEmailId = txtAssignedEmail.Text;
            obj.AssignedMobileNo = txtAssignedMobile.Text;
            obj.AssignedEmpId = txtAssignedEmpId.Text;
            obj.AssignedAccNo = txtAssignedAccNo.Text;
            obj.InsuraceInfo = txtInsurance.Text;
            obj.GrossSal = txtGrosssalary.Text;
            obj.AssignedBankName = txtBankName.Text;
            obj.InsuranceCompanyName = txtInsuranceCompany.Text;
            obj.Cp_Id = cp.getPresentCompanySessionValue();
            obj.Asset1 = txtAsset1.Text;
            obj.Asset2 = txtAsset2.Text;
            obj.Asset3 = txtAsset3.Text;
            obj.Asset4 = txtAsset4.Text;
            obj.Asset5 = txtAsset5.Text;
            obj.Asset6 = txtAsset6.Text;
            //obj.LeavesEarned = txtNoofLeavesEarned.Text;
            //obj.SickLeaves = txtNoofsickleaves.Text;
            //obj.CasualLeaves = txtNoofCasualLeaves.Text;
            obj.ReportingId = ddlReportingTo.SelectedItem.Value;
            if (txtAsset2.Checked == true)
            {
                obj.Asset2 = "NO-PF";
            }
            if (txtAsset5.Checked == true)
            {
                obj.Asset5 = "NO-Bonus";
            }
            if (txtAsset4.Checked == true)
            {
                obj.Asset4  = "Exclude Paysheet";
            }
            if (rdbActive.Checked == true)
            {
                obj.Status = "1";
            }
            else
            {
                obj.Status = "0";
            }


            if (obj.Employee_Update(gvEmployeeMaster.SelectedRow.Cells[1].Text) == "Data Updated Successfully")
            {

                obj.Empdoc_Delete(gvEmployeeMaster.SelectedRow.Cells[1].Text);

                #region EMP Images
                if (EmpImage.HasFiles)
                {


                    string itemimage = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeImage"))
                    {
                        foreach (HttpPostedFile uploadedFile in EmpImage.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeImage/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            itemimage = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.Emp_photo = itemimage;
                            obj.Emp_Img_Update(gvEmployeeMaster.SelectedRow.Cells[1].Text);

                            //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;

                        }
                    }

                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeImage");
                        foreach (HttpPostedFile uploadedFile in EmpImage.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/EmployeeImage/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            itemimage = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.Emp_photo = itemimage;
                            //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;
                            obj.Emp_Img_Update(gvEmployeeMaster.SelectedRow.Cells[1].Text);

                        }

                    }

                }
                
                #endregion

                for (int i = 0; i < chklDocumentsSubmitted.Items.Count; i++)
                {
                    if (chklDocumentsSubmitted.Items[i].Selected == true)
                    {
                        obj.DocSubmit = chklDocumentsSubmitted.Items[i].Value;
                        obj.Empdoc_Update(gvEmployeeMaster.SelectedRow.Cells[1].Text);
                    }
                }

                // Saving Uploaded Documents
                string CD = DateTime.Now.ToString("MM/dd/yyyy");

                if (docSubmitted.HasFiles)
                {
                    #region Employee Documents

                    string empDoc = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments"))
                    {
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.tMaxEmpId = Convert.ToInt32(gvEmployeeMaster.SelectedRow.Cells[1].Text);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.DocSubmit1 = chklDocumentsSubmitted.SelectedItem.Value;
                            obj.EmpDocDetails_Update();
                        }
                    }

                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments");
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.tMaxEmpId = Convert.ToInt32(gvEmployeeMaster.SelectedRow.Cells[1].Text);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.EmpDocDetails_Update();
                        }

                    }
                    #endregion
                }


                #region Employee Age
                int age = obj.GetAge(Convert.ToInt32(gvEmployeeMaster.SelectedRow.Cells[1].Text));
                #endregion
                #region Salary BreakUp

                #region Calculation
                decimal Basic = 0;
                decimal HRA = 0;
                decimal CA = 0;
                decimal Other_Allowance = 0;
                decimal PF = 0;
                decimal MedicalAllowance = 0;
                decimal PT = 0;
                decimal salary = Convert.ToDecimal(txtGrosssalary.Text) / 12;
                SqlCommand cmd = new SqlCommand("[Usp_GetCTCAge]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@salary_fix", salary);
                cmd.Parameters.AddWithValue("@Age", age);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Basic = Math.Round(Convert.ToDecimal(dt.Rows[0][0]));
                    HRA = Math.Round(Convert.ToDecimal(dt.Rows[0][1]));
                    CA = Math.Round(Convert.ToDecimal(dt.Rows[0][2]));
                    MedicalAllowance = Math.Round(Convert.ToDecimal(dt.Rows[0][3]));
                    Other_Allowance = Math.Round(Convert.ToDecimal(dt.Rows[0][4]));
                    PF = Math.Round(Convert.ToDecimal(dt.Rows[0][6]));
                    PT = Math.Round(Convert.ToDecimal(dt.Rows[0][8]));

                #endregion
                    // string emp_Id = obj.NewEmp_Id();
                    SqlCommand cmd1 = new SqlCommand("[USP_SalaryDetailsNew]", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Emp_ID", gvEmployeeMaster.SelectedRow.Cells[1].Text);
                    cmd1.Parameters.AddWithValue("@Basic", Basic);
                    cmd1.Parameters.AddWithValue("@HRA", HRA);
                    cmd1.Parameters.AddWithValue("@CA", CA);
                    cmd1.Parameters.AddWithValue("@Other_Allowance", Other_Allowance);
                    cmd1.Parameters.AddWithValue("@PF", PF);
                    cmd1.Parameters.AddWithValue("@MedicalAllowance", MedicalAllowance);
                    cmd1.Parameters.AddWithValue("@PT", PT);

                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();

                }
                #endregion
                //HR.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
            }
            else
            {
                //HR.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            //HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblEmployeeDetails.Visible = false;
            BindSearchGrid(); 
            //gvEmployeeMaster.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }
    #endregion

    #region Link Button Click
    protected void lbtnEmpFirstName_Click(object sender, EventArgs e)
    {
      
        LinkButton lbtnEnqNo;
        lbtnEnqNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnqNo.Parent.Parent;
        gvEmployeeMaster.SelectedIndex = gvRow.RowIndex;
        tblEmployeeDetails.Visible = false;

        

        try
        {
            HR.EmployeeMaster objEM = new HR.EmployeeMaster();
            if (objEM.EmployeeMaster_Select(gvEmployeeMaster.SelectedRow.Cells[1].Text) > 0)
            {
                tblEmployeeDetails.Visible = true;
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                txtFirstName.Text = objEM.EmpFirstName;
                txtMiddleName.Text = objEM.EmpMiddleName;
                txtLastName.Text = objEM.EmpLastName;
                if (objEM.EmpGender == "Male")
                {
                    rbtMale.Checked = true;
                    rbtFemale.Checked = false;
                }
                else
                {
                    rbtFemale.Checked = true;
                    rbtMale.Checked = false;
                }
                if (objEM.servicebond != "")
                {
                    txtYears.Visible = true;
                    lblYears.Visible = true;
                    txtYears.Text = objEM.servicebond;
                    rdbYes.Checked = true;
                }
                else
                {
                    txtYears.Visible = false;
                    lblYears.Visible = false;
                    rdbno.Checked = true;
                }
                txtAddress.Text = objEM.EmpAddress;
                txtCity.Text = objEM.EmpCity;
                txtDateOfBirth.Text = objEM.EmpDOB;
                txtEmail.Text = objEM.EmpEMail;
                txtMobileNo.Text = objEM.EmpMobile;
                txtPhoneNo.Text = objEM.EmpPhone;
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(objEM.DeptID));
                ddlDesignation.SelectedIndex = ddlDesignation.Items.IndexOf(ddlDesignation.Items.FindByValue(objEM.DesgID));
                ddlEmployeeType.SelectedIndex = ddlEmployeeType.Items.IndexOf(ddlEmployeeType.Items.FindByValue(objEM.EmpTypeID));
                ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(objEM.EmpBranchID));
                txtDateOfAppointment.Text = objEM.EmpDetDOJ;
                txtDateOfTermination.Text = objEM.EmpDetDOT;
                txtUserName.Text = objEM.EMPUserName;
                ddlCompany.SelectedValue = objEM.EmpCompany;
                //ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(objEM.EmpCompany));
                ddlReportingTo.SelectedIndex = ddlReportingTo.Items.IndexOf(ddlReportingTo.Items.FindByValue(objEM.ReportingId));

                txtYears.Text = objEM.servicebond;
                txtPerPhoneNo.Text = objEM.PerPhoneno;
                txtPerMobileNo.Text = objEM.permobileno;
                txtemrName.Text = objEM.emrname;
                txtemrRelationship.Text = objEM.emrrelation;
                txtemrPhone.Text = objEM.emgphone;
                txtemrAddress.Text = objEM.emgaddress;
                txtfathername.Text = objEM.FATHERNAME;
                txtAssignedMobile.Text = objEM.AssignedMobileNo;
                txtAssignedEmail.Text = objEM.AssignedEmailId;
                txtAssignedEmpId.Text = objEM.AssignedEmpId;
                txtAssignedAccNo.Text = objEM.AssignedAccNo;
                txtInsurance.Text = objEM.InsuraceInfo;
                txtGrosssalary.Text = objEM.GrossSal;
                txtBankName.Text = objEM.AssignedBankName;
                txtInsuranceCompany.Text = objEM.InsuranceCompanyName;
                
                //txtNoofCasualLeaves.Text = objEM.CasualLeaves;
                //txtNoofLeavesEarned.Text = objEM.LeavesEarned;
                //txtNoofsickleaves.Text = objEM.SickLeaves;
                lblSOIdHidden.Text = objEM.EmpID;

                if (objEM.Others != "")
                {
                    chkothers.Checked = true;
                    chkothers_CheckedChanged(sender, e);
                    txtOthers.Text = objEM.Others;
                }
                else
                {
                    chkothers.Checked = false;
                    txtOthers.Visible = false;
                }

                if (objEM.Status == "1")
                {
                    rdbActive.Checked = true;
                    rdbInactive.Checked = false;

                }
                else
                {
                    rdbInactive.Checked = true;
                    rdbActive.Checked = false;
                }

                //Image1.ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + gvEmployeeMaster.SelectedRow.Cells[1].Text + "";
                Image1.ImageUrl = "~/Content/EmployeeImage/" + objEM.EmpPhoto + "";
                

                chklDocumentsSubmitted.ClearSelection();
                DataTable dt = objEM.EmpDoc_Select(int.Parse(gvEmployeeMaster.SelectedRow.Cells[1].Text));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem currentCheckBox = chklDocumentsSubmitted.Items.FindByValue(dt.Rows[i][0].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }

                }

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            HR.Dispose();
        }

    }
    #endregion

    #region Edit Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvEmployeeMaster.SelectedIndex > -1)
        {
            tblEmployeeDetails.Visible = true;
            try
            {
                HR.EmployeeMaster objEM = new HR.EmployeeMaster();
                if (objEM.EmployeeMaster_Select(gvEmployeeMaster.SelectedRow.Cells[1].Text) > 0)
                {
                   
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    //txtUserName.Enabled = false;
                    txtFirstName.Text = objEM.EmpFirstName;
                    txtMiddleName.Text = objEM.EmpMiddleName;
                    txtLastName.Text = objEM.EmpLastName;
                    if (objEM.EmpGender == "Male")
                    {
                        rbtMale.Checked = true;
                        rbtFemale.Checked = false;
                    }
                    else
                    {
                        rbtFemale.Checked = true;
                        rbtMale.Checked = false;
                    }
                    if (objEM.servicebond != "")
                    {
                        txtYears.Visible = true;
                        lblYears.Visible = true;
                        txtYears.Text = objEM.servicebond;
                        rdbYes.Checked = true;
                    }
                    else
                    {
                        rdbno.Checked = true;
                    }
                    if (objEM.AssignedEmpId == "")
                    {
                        txtAssignedEmpId.Text = objEM.EmpID  ;
                    }
                    else
                    {
                        txtAssignedEmpId.Text = objEM.AssignedEmpId;
                    }
                    txtAddress.Text = objEM.EmpAddress;
                    txtCity.Text = objEM.EmpCity;
                    txtDateOfBirth.Text = objEM.EmpDOB;
                    txtEmail.Text = objEM.EmpEMail;
                    txtMobileNo.Text = objEM.EmpMobile;
                    txtPhoneNo.Text = objEM.EmpPhone;
                    ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(objEM.DeptID));
                    ddlDesignation.SelectedIndex = ddlDesignation.Items.IndexOf(ddlDesignation.Items.FindByValue(objEM.DesgID));
                    ddlEmployeeType.SelectedIndex = ddlEmployeeType.Items.IndexOf(ddlEmployeeType.Items.FindByValue(objEM.EmpTypeID));
                    ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(objEM.EmpBranchID));
                    //ddlBranch.SelectedValue = objEM.EmpBranchID;
                    ddlReportingTo.SelectedIndex = ddlReportingTo.Items.IndexOf(ddlReportingTo.Items.FindByValue(objEM.ReportingId));
                    txtDateOfAppointment.Text = objEM.EmpDetDOJ;
                    txtDateOfTermination.Text = objEM.EmpDetDOT;
                    txtUserName.Text = objEM.EMPUserName;
                    //ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(objEM.EmpCompany));
                    ddlCompany.SelectedValue = objEM.EmpCompany;
                    txtYears.Text = objEM.servicebond;
                    txtPerPhoneNo.Text = objEM.PerPhoneno;
                    txtPerMobileNo.Text = objEM.permobileno;
                    txtemrName.Text = objEM.emrname;
                    txtemrRelationship.Text = objEM.emrrelation;
                    txtemrPhone.Text = objEM.emgphone;
                    txtemrAddress.Text = objEM.emgaddress;
                    txtOthers.Text = objEM.Others;
                    txtfathername.Text = objEM.FATHERNAME;
                    txtAssignedMobile.Text = objEM.AssignedMobileNo;
                    txtAssignedEmail.Text = objEM.AssignedEmailId;
                    
                    txtAssignedAccNo.Text = objEM.AssignedAccNo;
                    txtInsurance.Text = objEM.InsuraceInfo;
                    txtGrosssalary.Text = objEM.GrossSal;
                    txtBankName.Text = objEM.AssignedBankName;
                    txtInsuranceCompany.Text = objEM.InsuranceCompanyName;
                    txtAsset1.Text = objEM.Asset1;
                    txtAsset2.Text = objEM.Asset2;
                    txtAsset3.Text = objEM.Asset3;
                    txtAsset4.Text = objEM.Asset4;
                    txtAsset5.Text = objEM.Asset5;
                    txtAsset6.Text = objEM.Asset6;
                    //txtNoofCasualLeaves.Text = objEM.CasualLeaves;
                    //txtNoofLeavesEarned.Text = objEM.LeavesEarned;
                    //txtNoofsickleaves.Text = objEM.SickLeaves;
                    lblSOIdHidden.Text = objEM.EmpID;
                    if (objEM.Asset2 == "NO-PF")
                    {
                        txtAsset2.Checked = true;
                    }
                    else
                    {
                        txtAsset2.Checked = false ;

                    }
                    if (objEM.Asset4 == "Exclude Paysheet")
                    {
                        txtAsset4.Checked = true;
                    }
                    else
                    {
                        txtAsset4.Checked = false;

                    }
                    if (objEM.Asset5 == "NO-Bonus")
                    {
                        txtAsset5.Checked = true;
                    }
                    else
                    {
                        txtAsset5.Checked = false;

                    }
                    if (objEM.Status == "1")
                    {
                        rdbActive.Checked = true;
                        rdbInactive.Checked = false;
                    }
                    else
                    {
                        rdbInactive.Checked = true;
                        rdbActive.Checked = false;
                    }
                    //Image1.ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + gvEmployeeMaster.SelectedRow.Cells[1].Text + "";
                    Image1.ImageUrl = "~/Content/EmployeeImage/" + objEM.EmpPhoto + "";


                    chklDocumentsSubmitted.ClearSelection();
                    DataTable dt = objEM.EmpDoc_Select(int.Parse(gvEmployeeMaster.SelectedRow.Cells[1].Text));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListItem currentCheckBox = chklDocumentsSubmitted.Items.FindByValue(dt.Rows[i][0].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                HR.Dispose();


            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //Find the reference of the Repeater Item.
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            int so_upload_id = int.Parse((item.FindControl("lbl_So_Upload_Id") as Label).Text);

            SqlCommand cmd = new SqlCommand("Delete from [Emp_Documents_Submitted] where Doc_ID=" + so_upload_id + " ", con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            Repeater1.DataBind();
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Unable to delete the record, Please try again");
        }

    }

    #region Link Button File Opener Click
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbtnFileOpener;
            lbtnFileOpener = (LinkButton)sender;
            Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
            DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
            string command = "SELECT Document_Submitted FROM [Emp_Documents_Submitted] WHERE EMP_ID=" + gvEmployeeMaster.SelectedRow.Cells[1].Text + " AND Document_Submitted='" + lbtnFileOpener.Text + "'";
            dbcon.Open();
            string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
            string path = "/Content/EmployeeDocuments/" + filename;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Problem In Loading the File");
        }

    }
    protected void lbtnFileOpener2_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbtnFileOpener2;
            lbtnFileOpener2 = (LinkButton)sender;
            Repeater gvRow = (Repeater)lbtnFileOpener2.Parent.Parent;
            DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
            string command = "SELECT Document_Submitted FROM [Emp_Documents_Submitted] WHERE EMP_ID=" + gvEmployeeMaster.SelectedRow.Cells[1].Text + " AND Document_Submitted='" + lbtnFileOpener2.Text + "'";
            dbcon.Open();
            string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
            string path = "/Content/EmployeeDocuments/" + filename;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Problem In Loading the File");
        }

    }
    #endregion
    #region Link Button Attached Files Click
    protected void lbtnAttachedFiles_Click(object sender, EventArgs e)
    {
        HR.EmployeeMaster objEM = new HR.EmployeeMaster();
        //objEM.DocSubmit  = lbtnAttachedFiles.Text;
    }
    #endregion
    #region New Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        HR.EmployeeMaster obj = new HR.EmployeeMaster();

        HR.ClearControls(this);
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        chklDocumentsSubmitted.ClearSelection();
        btnSave.Enabled = true;
        btnSave.Text = "Save";
        gvEmployeeMaster.SelectedIndex = -1;
        tblEmployeeDetails.Visible = true;
        txtAssignedEmpId.Text = obj.NewEmp_Id();
        txtUserName.Enabled = true;
    }
    #endregion

    #region Delete CLick
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvEmployeeMaster.SelectedIndex > -1)
        {
            try
            {
                HR.EmployeeMaster objEMP = new HR.EmployeeMaster();
                //HR.BeginTransaction();
                MessageBox.Show(this, objEMP.Employee_Delete(gvEmployeeMaster.SelectedRow.Cells[1].Text));
                //HR.CommitTransaction();
                tblEmployeeDetails.Visible = false;
            }
            catch (Exception ex)
            {
                //HR.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvEmployeeMaster.SelectedIndex = -1;
                BindSearchGrid(); 
                //gvEmployeeMaster.DataBind();
                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Close Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblEmployeeDetails.Visible = false;
        HR.ClearControls(this);
    }
    #endregion

    protected int slnoforMaingrid = 1;
    #region Grid View Row Databound
    protected void gvEmployeeMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[1].Visible = false;

        //}
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            //e.Row.Cells[10].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(gvEmployeeMaster.PageIndex * gvEmployeeMaster.PageSize + slnoforMaingrid);
            slnoforMaingrid++;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[10].Visible = false;
            if (e.Row.Cells[15].Text == "0")
            {
                e.Row.Cells[15].Text = "In-Active";
            }
            else
            {
                e.Row.Cells[15].Text = "Active";

            }
        }

       



    }
    #endregion
    
    protected void btnCurrentDayTasksGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtCurrentDayTaskSearchText.Text;
        BindSearchGrid(); 
        //gvEmployeeMaster.DataBind();
        //HR.ClearControls(this);
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_EmployeeMaster_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (ddlSearchBy.SelectedItem.Value == "EMP_ID")
        {
            cmd.Parameters.AddWithValue("@EMP_ID", txtCurrentDayTaskSearchText.Text);
        }
        if (ddlSearchBy.SelectedItem.Value == "EMP_FIRST_NAME")
        {
            cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtCurrentDayTaskSearchText.Text);

        }
        if (ddlSearchBy.SelectedItem.Value == "EMP_LAST_NAME")
        {
            cmd.Parameters.AddWithValue("@EMP_LAST_NAME", txtCurrentDayTaskSearchText.Text);

        }
        if (ddlSearchBy.SelectedItem.Value == "EMP_PHONE")
        {
            cmd.Parameters.AddWithValue("@EMP_PHONE", txtCurrentDayTaskSearchText.Text);

        }
        if (ddlSearchBy.SelectedItem.Value == "EMP_MOBILE")
        {
            cmd.Parameters.AddWithValue("@EMP_MOBILE", txtCurrentDayTaskSearchText.Text);

        }
        if (ddlSearchBy.SelectedItem.Value == "EMP_EMAIL")
        {
            cmd.Parameters.AddWithValue("@EMP_EMAIL", txtCurrentDayTaskSearchText.Text);

        }
        if (ddlSearchBy.SelectedItem.Value == "CP_FULL_NAME")
        {
            cmd.Parameters.AddWithValue("@CP_FULL_NAME", txtCurrentDayTaskSearchText.Text);

        }
        if (ddlSearchBy.SelectedItem.Value == "STATUS")
        {
            cmd.Parameters.AddWithValue("@STATUS", txtCurrentDayTaskSearchText.Text);

        }

        if (ddlSearchBy.SelectedItem.Value == "DEPT_NAME")
        {
            cmd.Parameters.AddWithValue("@DEPT_NAME", txtCurrentDayTaskSearchText.Text);

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvEmployeeMaster.DataSource = dt;
        gvEmployeeMaster.DataBind();
    }

    protected void gvEmployeeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeMaster.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        chklDocumentsSubmitted.ClearSelection();
        btnNew_Click(sender, e);
        chkothers.Checked = false;
        txtOthers.Visible = false;
        
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbYes.Checked == true)
        {
            txtYears.Visible = true;
            lblYears.Visible = true;
        }
        else
        {
            txtYears.Visible = false;
            lblYears.Visible = false;
        }
    }
    protected void rdbno_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbno.Checked == true)
        {
            txtYears.Visible = false;
            lblYears.Visible = false;
        }
    }
    protected void chkothers_CheckedChanged(object sender, EventArgs e)
    {
        if (chkothers.Checked == true)
        {
            txtOthers.Visible = true;
        }
        else
        {
            txtOthers.Visible = false;
        }
    }
    protected void gvEmployeeMaster_DataBound(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
        
        foreach (GridViewRow gvr in gvEmployeeMaster.Rows)
        {
            if (gvr.Cells[10].Text == dt.ToString())
            {

                gvr.BackColor = System.Drawing.Color.Red;
            }

        }   
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=HR";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/HR/EmpData.aspx");
    }

    protected void Upload_DataBinding(object sender, EventArgs e)
    {
        //string path=Upload.PostedFiles.
    }
    protected void btnLeaveMaster_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default2.aspx");
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvEmployeeMaster.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        BindSearchGrid(); 
        //gvEmployeeMaster.DataBind();
    }


    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvEmployeeMaster.PageIndex = Convert.ToInt32(txtPageNo.Text)-1;

    }

    protected void chklDocumentsSubmitted_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in chklDocumentsSubmitted.Items)
        {
            if (li.Value == "VoterId Card")
            {
                txtVoterId.Visible = true;
                //txtDL.Visible = false;
                //txtRation.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtAdhar.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "Driving License")
            {
                txtDL.Visible = true;
                //txtVoterId.Visible = false;
                //txtRation.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtAdhar.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "Ration Card")
            {
                txtRation.Visible = true;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtAdhar.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "Ration Card")
            {
                txtRation.Visible = true;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtAdhar.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "PassPort")
            {
                txtPassBook.Visible = true;
                //txtRation.Visible = false;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                ////txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtAdhar.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "Pan")
            {
                txtPan.Visible = true;
                //txtRation.Visible = false;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                ////txtPassBook.Visible = false;
                //txtPassBook.Visible = false;
                //txtAdhar.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;

            }
            else if (li.Value == "Adhar Card")
            {
                txtAdhar.Visible = true;
                //txtRation.Visible = false;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                ////txtPassBook.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "Telephone Bill")
            {
                txtTelBill.Visible = true;
                //txtAdhar.Visible = false;
                //txtRation.Visible = false;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                ////txtPassBook.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                ////txtTelBill.Visible = false;
                //txtBPB.Visible = false;
            }
            else if (li.Value == "Bank Pass Book")
            {
                txtBPB.Visible = true;
                //txtAdhar.Visible = false;
                //txtRation.Visible = false;
                //txtVoterId.Visible = false;
                //txtDL.Visible = false;
                ////txtPassBook.Visible = false;
                //txtPassBook.Visible = false;
                //txtPan.Visible = false;
                //txtTelBill.Visible = false;
                ////txtBPB.Visible = false;
            }
        }
    }
}

