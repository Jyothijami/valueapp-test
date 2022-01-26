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

public partial class HR_EmpOnDutyForm : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
       {
           Employee_Fill();
           Department_Fill();
           Designation_Fill();
           
           lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
           lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
       }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvOnDutyForm.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvOnDutyForm.SelectedRow.Cells[6].Text) && gvOnDutyForm.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnRefresh.Visible = false;
                btnDelete.Visible = true;
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

    //#region Location Fill
    //public void Location_Fill()
    //{
    //    try
    //    {
    //        HR.RegionalMaster_Select(ddlLocation);
    //        }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        HR.Dispose();
    //    }
    //}
    //#endregion

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
            HR.EmployeeDept(ddlEmployee,ddlDepartment.SelectedItem.Value);
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

    #region Approvedby Fill
    public void ApprovedBy_Fill()
    {
        try
        {
            HR.EmployeeDeptNameSelect(ddlEmployee);
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
        if(btnSave.Text == "Save")
        {
            OnDutySave();
        }
        else if(btnSave.Text == "Update")
        {
            OnDutyUpdate();
        }
    }

    private void OnDutyUpdate()
    {
        try
        {
            HR.OnDuty objMaster = new HR.OnDuty();
            objMaster.OndutyId = gvOnDutyForm.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            //objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.LocationId = txtLocation.Text;

            objMaster.MovingDate = General.toMMDDYYYY(txtMovingDate.Text);
            objMaster.OnDutyDateFrom = General.toMMDDYYYY(txtOndutyFrom.Text);
            objMaster.OnDutyDateTo = General.toMMDDYYYY(txtondutyto.Text);
            objMaster.ReturnDate = General.toMMDDYYYY(txtReturndate.Text);
            objMaster.RefExecutive = txtRefexecutive.Text;
            objMaster.NatureofWork = txtNatureofwork.Text;
            objMaster.CoffDays = txtCoffDays.Text;
            objMaster.PlaceVisited = txtPlaceVisited.Text;
            objMaster.FromTime = txtMovingTime.Text;
            objMaster.ToTime = txtReturnTime.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.OndutyDate = General.toMMDDYYYY(txtDate.Text);

            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";
            objMaster.Status3 = "Pending";
            objMaster.Comment1 = "-";
            objMaster.Comment2 = "-";
            objMaster.Comment3 = "-";
            objMaster.Rejected_By = "";

            objMaster.OnDuty_Update();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvOnDutyForm.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    private void OnDutySave()
    {
        try
        {
            HR.OnDuty objMaster = new HR.OnDuty();
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            //objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.LocationId = txtLocation.Text;

            objMaster.MovingDate = General.toMMDDYYYY(txtMovingDate.Text);
            objMaster.OnDutyDateFrom = General.toMMDDYYYY(txtOndutyFrom.Text);
            objMaster.OnDutyDateTo = General.toMMDDYYYY(txtondutyto.Text);
            objMaster.ReturnDate = General.toMMDDYYYY(txtReturndate.Text);
            objMaster.RefExecutive = txtRefexecutive.Text;
            objMaster.NatureofWork = txtNatureofwork.Text;
            objMaster.CoffDays = txtCoffDays.Text;
            objMaster.PlaceVisited = txtPlaceVisited.Text;
            objMaster.FromTime = txtMovingTime.Text;
            objMaster.ToTime = txtReturnTime.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.OndutyDate = General.toMMDDYYYY(txtDate.Text);

            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";       
            objMaster.Status3 = "Pending";
            objMaster.Comment1 = "-";
            objMaster.Comment2 = "-";
            objMaster.Comment3 = "-";
            objMaster.Rejected_By = "";


            objMaster.OnDuty_Save();
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvOnDutyForm.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvOnDutyForm.SelectedIndex > -1)
        {
            try
            {
                HR.OnDuty objSM = new HR.OnDuty();
                objSM.OndutyId = gvOnDutyForm.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objSM.OnDuty_Delete());
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
                gvOnDutyForm.DataBind();
                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lbtnOnDutyform_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        LinkButton lbtnOnDuty;
        lbtnOnDuty = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnOnDuty.Parent.Parent;
        gvOnDutyForm.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvOnDutyForm.SelectedIndex > -1)
        {
            tblDetails.Visible = true;

            HR.OnDuty objmas = new HR.OnDuty();
            objmas.OnDuty_Select(gvOnDutyForm.SelectedRow.Cells[0].Text);

            ddlDepartment.SelectedValue = objmas.DeptId;
            ddlDepartment_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = objmas.EmpId;
            ddlDesignation.SelectedValue = objmas.DesgId;
            //ddlLocation.SelectedValue = objmas.LocationId;
            txtLocation.Text = objmas.LocationId;

            txtDate.Text = objmas.OndutyDate;

            txtMovingDate.Text = objmas.MovingDate;
            txtOndutyFrom.Text = objmas.OnDutyDateFrom;
            txtondutyto.Text = objmas.OnDutyDateTo;
            txtReturndate.Text = objmas.ReturnDate;
            txtRefexecutive.Text = objmas.RefExecutive;
            txtNatureofwork.Text = objmas.NatureofWork;
            txtCoffDays.Text = objmas.CoffDays;
            txtPlaceVisited.Text = objmas.PlaceVisited;
            ddlApprovedby.SelectedValue = objmas.ApprovedBy;
            txtMovingTime.Text = objmas.FromTime;
            txtReturnTime.Text = objmas.ToTime;
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
            HR.OnDuty obj = new HR.OnDuty();
            HR.BeginTransaction();
            obj.OndutyId = gvOnDutyForm.SelectedRow.Cells[0].Text;
            obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.OnDutyApprove_Update();
            HR.CommitTransaction();
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvOnDutyForm.DataBind();
            HR.Dispose();
            btnEdit_Click(sender, e);
        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvOnDutyForm.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=OnDuty&siid=" + gvOnDutyForm.SelectedRow.Cells[0].Text + "";
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
    protected void gvOnDutyForm_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}