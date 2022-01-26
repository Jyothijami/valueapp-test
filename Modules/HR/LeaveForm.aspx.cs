using Yantra.Classes;
using Yantra.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_LeaveForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Employee_Fill();
            Department_Fill();
            Designation_Fill();
            LeaveFill();
          
        }
    }

    private void LeaveFill()
    {
        HR.EmployeeMaster objmas = new HR.EmployeeMaster();
        objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
        lblCasualLeaves.Text = objmas.CasualLeaves;
        lblLeavesEarned.Text = objmas.LeavesEarned;
        lblSickleaves.Text = objmas.SickLeaves;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvLeaveForm.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvLeaveForm.SelectedRow.Cells[6].Text) && gvLeaveForm.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnRefresh.Visible = false;
                btnDelete.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            btnApprove.Visible = false;
        }
    }
    #endregion

    #region Exit
    protected void btnExit_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        HR.ClearControls(this);
    }
    #endregion

    #region Refresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
    }
    #endregion

    #region Add New
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = true;
        ddlEmployee.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        ddlDepartment.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
        ddlDesignation.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.Desgid);

       // txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }
    #endregion

    #region Department Fill
    public void Department_Fill()
    {
        try
        {
            HR.Department_Select(ddlDepartment);
            HR.Department_Select(ddlChargeDept);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Designation Fill
    public void Designation_Fill()
    {
        try
        {
            HR.Designation_Select(ddlDesignation);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Employee Fill
    public void Employee_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployee);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedby);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Department Change
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeDept(ddlEmployee, ddlDepartment.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            LeaveFormSave();
        }
        else if (btnSave.Text == "Update")
        {
            LeaveFormUpdate();
        }
    }

    private void LeaveFormUpdate()
    {
        try
        {
            HR.EmpLeave objMaster = new HR.EmpLeave();
            objMaster.LeaveId = gvLeaveForm.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            objMaster.FromDate = General.toMMDDYYYY(txtFromDate.Text);
            objMaster.ToDate = General.toMMDDYYYY(txtTodate.Text);
            objMaster.ReasonforLeave = txtReasonforapply.Text;
            objMaster.HandedName = ddlChargeEmployee.SelectedItem.Value;
            objMaster.HandedDept = ddlChargeDept.SelectedItem.Value;
            objMaster.AddressofLeavePeriod = txtAddressinLeavePeriod.Text;
            objMaster.LeaveDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.Approvedby = ddlApprovedby.SelectedItem.Value;
            objMaster.LeaveType = ddlLeaveType.SelectedItem.Value;
            objMaster.DayofLeave = txtNoofdaysleave.Text;

           if(objMaster.EmpLeave_Update() == "Data Updated Successfully")
           {
               HR.EmployeeMaster obj = new HR.EmployeeMaster();
               obj.CasualLeaves = lblCasualLeaves.Text;
               obj.SickLeaves = lblSickleaves.Text;
               obj.LeavesEarned = lblLeavesEarned.Text;
               obj.EmployeeLeave_Update(ddlEmployee.SelectedItem.Value);
           }
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvLeaveForm.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    private void LeaveFormSave()
    {
        try
        {
            HR.EmpLeave objMaster = new HR.EmpLeave();
          
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            objMaster.FromDate = General.toMMDDYYYY(txtFromDate.Text);
            objMaster.ToDate = General.toMMDDYYYY(txtTodate.Text);
            objMaster.ReasonforLeave = txtReasonforapply.Text;
            objMaster.DayofLeave = txtNoofdaysleave.Text;
            objMaster.HandedName = ddlChargeEmployee.SelectedItem.Value;
            objMaster.HandedDept = ddlChargeDept.SelectedItem.Value;
            objMaster.AddressofLeavePeriod = txtAddressinLeavePeriod.Text;
            objMaster.LeaveDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.Approvedby = ddlApprovedby.SelectedItem.Value;
            objMaster.LeaveType = ddlLeaveType.SelectedItem.Value;
            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";
            objMaster.Status3 = "Pending";
            if(objMaster.EmpLeave_Save() == "Data Saved Successfully")
            {
                HR.EmployeeMaster obj = new HR.EmployeeMaster();
                obj.CasualLeaves = lblCasualLeaves.Text;
                obj.SickLeaves = lblSickleaves.Text;
                obj.LeavesEarned = lblLeavesEarned.Text;
                obj.EmployeeLeave_Update(ddlEmployee.SelectedItem.Value);
            }
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvLeaveForm.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    protected void ddlChargeDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeDept(ddlChargeEmployee, ddlChargeDept.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvLeaveForm.SelectedIndex > -1)
        {
            try
            {
                HR.EmpLeave objSM = new HR.EmpLeave();
                objSM.LeaveId = gvLeaveForm.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objSM.Leave_Delete());
            }
            catch (Exception ex)
            {
                HR.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblDetails.Visible = false;
                btnDelete.Attributes.Clear();
                gvLeaveForm.DataBind();
                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lbtnLeave_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        LinkButton lbtnovertime;
        lbtnovertime = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnovertime.Parent.Parent;
        gvLeaveForm.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvLeaveForm.SelectedIndex > -1)
        {
            tblDetails.Visible = true;

            HR.EmpLeave objmas = new HR.EmpLeave();
            objmas.LeaveDetails_Select(gvLeaveForm.SelectedRow.Cells[0].Text);

            ddlDepartment.SelectedValue = objmas.DeptId;
            ddlDepartment_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = objmas.EmpId;
            ddlDesignation.SelectedValue = objmas.DesgId;
            txtDate.Text = objmas.LeaveDate;

            txtFromDate.Text = objmas.FromDate;
            txtTodate.Text = objmas.ToDate;
            txtReasonforapply.Text = objmas.ReasonforLeave;
            ddlChargeDept.SelectedValue = objmas.HandedDept;
            ddlChargeDept_SelectedIndexChanged(sender, e);
            ddlChargeEmployee.SelectedValue = objmas.HandedName;
            txtAddressinLeavePeriod.Text = objmas.AddressofLeavePeriod;
            ddlApprovedby.SelectedItem.Value = objmas.Approvedby;
            ddlLeaveType.SelectedValue = objmas.LeaveType;
            txtNoofdaysleave.Text = objmas.DayofLeave;
            btnSave.Text = "Update";
        }

        else
        {
            MessageBox.Show(this, "Please select atleast one Record");
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            HR.EmpLeave obj = new HR.EmpLeave();
            HR.BeginTransaction();
            obj.LeaveId = gvLeaveForm.SelectedRow.Cells[0].Text;
            obj.Approvedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.LeaveDetailsApprove_Update();
            HR.CommitTransaction();
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvLeaveForm.DataBind();
            HR.Dispose();
            btnEdit_Click(sender, e);
        }
    }



    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal hai;
        if(ddlLeaveType.SelectedItem.Value == "SickLeaves")
        {
            hai = Math.Round(Convert.ToDecimal(lblSickleaves.Text), 1) - Math.Round(Convert.ToDecimal(txtNoofdaysleave.Text), 1);
            lblSickleaves.Text = hai.ToString();
        }
        else if(ddlLeaveType.SelectedItem.Value == "CasualLeaves")
        {
            hai = Math.Round(Convert.ToDecimal(lblCasualLeaves.Text), 1) - Math.Round(Convert.ToDecimal(txtNoofdaysleave.Text), 1);
            lblCasualLeaves.Text = hai.ToString();
        }
        else if(ddlLeaveType.SelectedItem.Value == "LeavesEarned")
        {
            hai = Math.Round(Convert.ToDecimal(lblLeavesEarned.Text), 1) - Math.Round(Convert.ToDecimal(txtNoofdaysleave.Text), 1);
            lblLeavesEarned.Text = hai.ToString();
        }
    }
}