
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatumDAL;
using Yantra.MessageBox;
using Yantra.Classes;
using vllib;

public partial class Modules_HR_EmpOverTimeFormApprove : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Employee_Fill();
            Department_Fill();
            Designation_Fill();
            Location_Fill();
        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvOvertime.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvOvertime.SelectedRow.Cells[7].Text) && gvOvertime.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                //btnRefresh.Visible = false;
                //btnDelete.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
                //btnSave.Visible = true;
                //btnRefresh.Visible = false;
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;
            }
        }
        else
        {
            //btnSave.Visible = true;
            //btnRefresh.Visible = true;
            //btnEdit.Visible = true;
            //btnDelete.Visible = true;
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
        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

    }
    #endregion

    #region Department Fill
    public void Department_Fill()
    {
        try
        {
            HR.Department_Select(ddlDepartment);
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

    #region Location Fill
    public void Location_Fill()
    {
        try
        {
            HR.RegionalMaster_Select(ddlLocation);
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
            EmpOvertimeSave();
        }
        else if (btnSave.Text == "Update")
        {
            EmpOvertimeUpdate();
        }
    }

    private void EmpOvertimeUpdate()
    {
        try
        {
            HR.OverTime objMaster = new HR.OverTime();
            objMaster.OvertimeId = gvOvertime.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.FromTime = txtfromtime.Text;
            objMaster.ToTime = txttotime.Text;
            objMaster.CoffDays = txtCoffdays.Text;
            objMaster.Natureofwork = txtnatureofwork.Text;
            objMaster.AddPlaceWorked = txtAddofplaceworked.Text;
            objMaster.Remarks = txtRemarks.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.OvertimeDate = General.toMMDDYYYY(txtDate.Text);

            objMaster.OverTime_Update();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvOvertime.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    private void EmpOvertimeSave()
    {
        try
        {
            HR.OverTime objMaster = new HR.OverTime();
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.FromTime = txtfromtime.Text;
            objMaster.ToTime = txttotime.Text;
            objMaster.CoffDays = txtCoffdays.Text;
            objMaster.Natureofwork = txtnatureofwork.Text;
            objMaster.AddPlaceWorked = txtAddofplaceworked.Text;
            objMaster.Remarks = txtRemarks.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.OvertimeDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.OverTime_Save();
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvOvertime.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvOvertime.SelectedIndex > -1)
        {
            try
            {
                HR.OverTime objSM = new HR.OverTime();
                objSM.OvertimeId = gvOvertime.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objSM.OverTime_Delete());
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
                gvOvertime.DataBind();
                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lbtnOvertime_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        LinkButton lbtnovertime;
        lbtnovertime = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnovertime.Parent.Parent;
        gvOvertime.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }




    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvOvertime.SelectedIndex > -1)
        {
            tblDetails.Visible = true;

            HR.OverTime objmas = new HR.OverTime();
            objmas.OverTime_Select(gvOvertime.SelectedRow.Cells[0].Text);

            ddlDepartment.SelectedValue = objmas.DeptId;
            ddlDepartment_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = objmas.EmpId;
            ddlDesignation.SelectedValue = objmas.DesgId;
            ddlLocation.SelectedValue = objmas.LocationId;
            txtDate.Text = objmas.OvertimeDate;

            txtfromtime.Text = objmas.FromTime;
            txttotime.Text = objmas.ToTime;
            txtCoffdays.Text = objmas.CoffDays;
            txtnatureofwork.Text = objmas.Natureofwork;
            txtAddofplaceworked.Text = objmas.AddPlaceWorked;
            txtRemarks.Text = objmas.Remarks;
            ddlApprovedby.SelectedValue = objmas.ApprovedBy;

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
            HR.OverTime obj = new HR.OverTime();
            HR.BeginTransaction();
            obj.OvertimeId = gvOvertime.SelectedRow.Cells[0].Text;
            obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.OverTimeApprove_Update();
            HR.CommitTransaction();
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvOvertime.DataBind();
            HR.Dispose();
            btnEdit_Click(sender, e);
        }
    }
}