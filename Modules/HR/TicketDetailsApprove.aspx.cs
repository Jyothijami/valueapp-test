using Yantra.Classes;
using Yantra.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
public partial class Modules_HR_TicketDetailsApprove : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Employee_Fill();
            Department_Fill();
            Designation_Fill();
            //Location_Fill();
        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvTicketdetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvTicketdetails.SelectedRow.Cells[6].Text) && gvTicketdetails.SelectedRow.Cells[6].Text != "&nbsp;")
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

    //#region Location Fill
    //public void Location_Fill()
    //{
    //    try
    //    {
    //        HR.RegionalMaster_Select(ddlLocation);
    //    }
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
            TicketSave();
        }
        else if (btnSave.Text == "Update")
        {
            TicketUpdate();
        }
    }

    private void TicketUpdate()
    {
        try
        {
            HR.TicketDetails objMaster = new HR.TicketDetails();
            objMaster.TicketId = gvTicketdetails.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            //objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.LocationId = txtLocation.Text;

            objMaster.MovingDate = General.toMMDDYYYY(txtMovingdate.Text);
            objMaster.ModeofTravel = txtModeoftravel.Text;
            objMaster.Destination = txtDestination.Text;
            objMaster.Eligibility = txtEligibility.Text;
            objMaster.IdProofNo = txtIdproofno.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.TicketDate = General.toMMDDYYYY(txtDate.Text);

            objMaster.TicketDetails_Update();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvTicketdetails.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }

    private void TicketSave()
    {
        try
        {
            HR.TicketDetails objMaster = new HR.TicketDetails();
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.DeptId = ddlDepartment.SelectedItem.Value;
            objMaster.DesgId = ddlDesignation.SelectedItem.Value;
            //objMaster.LocationId = ddlLocation.SelectedItem.Value;
            objMaster.LocationId = txtLocation.Text;

            objMaster.MovingDate = General.toMMDDYYYY(txtMovingdate.Text);
            objMaster.ModeofTravel = txtModeoftravel.Text;
            objMaster.Destination = txtDestination.Text;
            objMaster.Eligibility = txtEligibility.Text;
            objMaster.IdProofNo = txtIdproofno.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.TicketDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.TicketDetails_Save();
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvTicketdetails.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvTicketdetails.SelectedIndex > -1)
        {
            try
            {
                HR.TicketDetails objSM = new HR.TicketDetails();
                objSM.TicketId = gvTicketdetails.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objSM.TicketDetails_Delete());
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
                gvTicketdetails.DataBind();
                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

    protected void lbtnTicketDetails_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        LinkButton lbtnTicketdetails;
        lbtnTicketdetails = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnTicketdetails.Parent.Parent;
        gvTicketdetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //if (gvTicketdetails.SelectedIndex > -1)
        //{
        //    tblDetails.Visible = true;

        //    HR.TicketDetails objmas = new HR.TicketDetails();
        //    objmas.TicketDetails_Select(gvTicketdetails.SelectedRow.Cells[0].Text);

        //    ddlDepartment.SelectedValue = objmas.DeptId;
        //    ddlDepartment_SelectedIndexChanged(sender, e);
        //    ddlEmployee.SelectedValue = objmas.EmpId;
        //    ddlDesignation.SelectedValue = objmas.DesgId;
        //    ddlLocation.SelectedValue = objmas.LocationId;
        //    txtDate.Text = objmas.TicketDate;

        //    txtMovingdate.Text = objmas.MovingDate;
        //    txtModeoftravel.Text = objmas.ModeofTravel;
        //    txtDestination.Text = objmas.Destination;
        //    txtEligibility.Text = objmas.Eligibility;
        //    txtIdproofno.Text = objmas.IdProofNo;
        //    ddlApprovedby.SelectedValue = objmas.ApprovedBy;

        //    btnSave.Text = "Update";
        //    btnSave.Enabled = false;
        //}

        //else
        //{
        //    MessageBox.Show(this, "Please select atleast one Record");
        //}

        //New Code 

        if (gvTicketdetails.SelectedIndex > -1)
        {
            tblDetails.Visible = true;

            HR.TicketDetails objmas = new HR.TicketDetails();
            objmas.TicketDetails_Select(gvTicketdetails.SelectedRow.Cells[0].Text);

            ddlDepartment.SelectedValue = objmas.DeptId;
            ddlDepartment_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = objmas.EmpId;
            ddlDesignation.SelectedValue = objmas.DesgId;
            //ddlLocation.SelectedValue = objmas.LocationId;
            txtLocation.Text = objmas.LocationId;

            txtDate.Text = objmas.TicketDate;

            txtMovingdate.Text = objmas.MovingDate;
            txtModeoftravel.Text = objmas.ModeofTravel;
            txtDestination.Text = objmas.Destination;

            ddlEligibility.SelectedValue = objmas.Eligibility;
            txtIdproofno.Text = objmas.IdProofNo;
            ddlApprovedby.SelectedValue = objmas.ApprovedBy;
            ddlIdProof.SelectedValue = objmas.Idproof;
            txtAge.Text = objmas.Age;
            txtMobileNo.Text = objmas.Mobile;

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
            HR.TicketDetails obj = new HR.TicketDetails();
            HR.BeginTransaction();
            obj.TicketId = gvTicketdetails.SelectedRow.Cells[0].Text;
            obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.TicketDetailsApprove_Update();
            HR.CommitTransaction();
        }
        catch (Exception ex)
        {
            HR.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblDetails.Visible = false;
            gvTicketdetails.DataBind();
            HR.Dispose();
            btnEdit_Click(sender, e);
        }
    }
}