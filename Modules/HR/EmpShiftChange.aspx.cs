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

public partial class HR_EmpShiftChange : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Employee_Fill();
            Department_Fill();
            Designation_Fill();
            Location_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }


    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvShiftchange.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvShiftchange.SelectedRow.Cells[6].Text) && gvShiftchange.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnRefresh.Visible = false;
                btnDelete.Visible = false;
            }
            else
            {
                btnApprove.Visible = false;
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
            ShiftChangeSave();
        }
        else if (btnSave.Text == "Update")
        {
            ShiftChangeUpdate();
        }
    }

    private void ShiftChangeUpdate()
    {
        try
        {
            HR.ShiftChange objMaster = new HR.ShiftChange();
            objMaster.ShiftId = gvShiftchange.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.Reason = txtReasonforshiftchange.Text;
            objMaster.PresentShift = txtPresentShift.Text;
            objMaster.RequiredShift = txtRequiredshift.Text;
            objMaster.ShiftChangeBetween = txtshiftchangebetween.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.ShiftDate = General.toMMDDYYYY(txtDate.Text);

            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";
            objMaster.Status3 = "Pending";
            objMaster.Comment1 = "-";
            objMaster.Comment2 = "-";
            objMaster.Comment3 = "-";
            objMaster.Rejected_By = "";

            objMaster.ShiftChange_Update();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvShiftchange.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    private void ShiftChangeSave()
    {
        try
        {
            HR.ShiftChange objMaster = new HR.ShiftChange();
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.Reason = txtReasonforshiftchange.Text;
            objMaster.PresentShift = txtPresentShift.Text;
            objMaster.RequiredShift = txtRequiredshift.Text;
            objMaster.ShiftChangeBetween = txtshiftchangebetween.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.ShiftDate = General.toMMDDYYYY(txtDate.Text);

            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";
            objMaster.Status3 = "Pending";
            objMaster.Comment1 = "-";
            objMaster.Comment2 = "-";
            objMaster.Comment3 = "-";
            objMaster.Rejected_By = "";


            objMaster.ShiftChange_Save();
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvShiftchange.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvShiftchange.SelectedIndex > -1)
        {
            try
            {
                HR.ShiftChange objSM = new HR.ShiftChange();
                objSM.ShiftId = gvShiftchange.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objSM.ShiftChange_Delete());
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
                gvShiftchange.DataBind();
                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lbtnShiftChange_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        LinkButton lbtnovertime;
        lbtnovertime = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnovertime.Parent.Parent;
        gvShiftchange.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvShiftchange.SelectedIndex > -1)
        {
            tblDetails.Visible = true;

            HR.ShiftChange objmas = new HR.ShiftChange();
            objmas.ShiftChange_Select(gvShiftchange.SelectedRow.Cells[0].Text);

            ddlDepartment.SelectedValue = objmas.DeptId;
            ddlDepartment_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = objmas.EmpId;
            ddlDesignation.SelectedValue = objmas.DesgId;
            ddlLocation.SelectedValue = objmas.LocationId;
            txtDate.Text = objmas.ShiftDate;

            txtPresentShift.Text = objmas.PresentShift;
            txtRequiredshift.Text = objmas.RequiredShift;
            txtshiftchangebetween.Text = objmas.ShiftChangeBetween;
            txtReasonforshiftchange.Text = objmas.Reason;
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
            HR.ShiftChange obj = new HR.ShiftChange();
            HR.BeginTransaction();
            obj.ShiftId = gvShiftchange.SelectedRow.Cells[0].Text;
            obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.ShiftChangeApprove_Update();
            HR.CommitTransaction();
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvShiftchange.DataBind();
            HR.Dispose();
            btnEdit_Click(sender, e);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        if (gvShiftchange.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ShiftChange&siid=" + gvShiftchange.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
}