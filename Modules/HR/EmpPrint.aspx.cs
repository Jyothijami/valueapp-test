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
using vllib;
public partial class Modules_HR_EmpPrint : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Department_Fill();
            Designation_Fill();
            EmployeeType_Fill();
            BranchName_Fill();
            CompanyName_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            gvEmployeeMaster.DataBind();
        }
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

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
     //       Masters.EmployeeType.EmployeeType_Select(ddlEmployeeType);
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
//            Masters.CompanyProfile.Company_Select(ddlCompany);
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
                lblchkllist.Visible = false;
                EmployeeMasterSave();
                HR.ClearControls(this);
                tblEmployeeDetails.Visible = false;
            }




        }
        else if (btnSave.Text == "Update")
        {
            int index = 0;

            foreach (ListItem a in chklDocumentsSubmitted.Items)
            {
                if (a.Selected == true)
                    index++;
            }


            if (index < 3)
            {
                lblchkllist.Visible = true;
            }

            else
            {
                lblchkllist.Visible = false;
                EmployeeMasterUpdate();
                HR.ClearControls(this);
                tblEmployeeDetails.Visible = false;
            }
        }
    }
    #endregion

    #region EmployeeMasterSave
    private void EmployeeMasterSave()
    {
        try
        {
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            HR.BeginTransaction();
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
            obj.tEmpPhoto = "xxx.jpeg";
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
            obj.Cp_Id = cp.getPresentCompanySessionValue();

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
                HR.CommitTransaction();
                MessageBox.Show(this, "Data Saved Successfully");
                chklDocumentsSubmitted.ClearSelection();
            }
            else
            {
                HR.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblEmployeeDetails.Visible = false;
            gvEmployeeMaster.DataBind();
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
            HR.BeginTransaction();

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
            obj.Cp_Id = cp.getPresentCompanySessionValue();

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
                for (int i = 0; i < chklDocumentsSubmitted.Items.Count; i++)
                {
                    if (chklDocumentsSubmitted.Items[i].Selected == true)
                    {
                        obj.DocSubmit = chklDocumentsSubmitted.Items[i].Value;
                        obj.Empdoc_Update(gvEmployeeMaster.SelectedRow.Cells[1].Text);
                    }
                }


                HR.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
            }
            else
            {
                HR.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblEmployeeDetails.Visible = false;
            gvEmployeeMaster.DataBind();
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

                Image1.ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + gvEmployeeMaster.SelectedRow.Cells[1].Text + "";
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
                    txtAssignedEmpId.Text = objEM.AssignedEmpId;
                    txtAssignedAccNo.Text = objEM.AssignedAccNo;
                    txtInsurance.Text = objEM.InsuraceInfo;
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
                    Image1.ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + gvEmployeeMaster.SelectedRow.Cells[1].Text + "";

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

    #region New Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
        chklDocumentsSubmitted.ClearSelection();
        btnSave.Enabled = true;
        btnSave.Text = "Save";
        gvEmployeeMaster.SelectedIndex = -1;
        tblEmployeeDetails.Visible = true;
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
                HR.BeginTransaction();
                MessageBox.Show(this, objEMP.Employee_Delete(gvEmployeeMaster.SelectedRow.Cells[1].Text));
                HR.CommitTransaction();
                tblEmployeeDetails.Visible = false;
            }
            catch (Exception ex)
            {
                HR.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvEmployeeMaster.SelectedIndex = -1;
                gvEmployeeMaster.DataBind();
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
            e.Row.Cells[10].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString(gvEmployeeMaster.PageIndex * gvEmployeeMaster.PageSize + slnoforMaingrid);
            slnoforMaingrid++;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[10].Visible = false;
        }





    }
    #endregion

    protected void btnCurrentDayTasksGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtCurrentDayTaskSearchText.Text;
        gvEmployeeMaster.DataBind();
        HR.ClearControls(this);
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../HR/Print.aspx','PrintMe','height=600,width=900,scrollbars=yes');</script>");

    }
}

