using Yantra.Classes;
using Yantra.MessageBox;
using System;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class HR_TicketDetails : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
        {
            Employee_Fill();
            Department_Fill();
            Designation_Fill();
            //Location_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            BindGrid_All();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid_All();
    }
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("SP_HR_TICKETDETAILS_SEARCH_SELECT_3", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblUserType.Text != "")
        {
            cmd.Parameters.AddWithValue("@userType", lblUserType.Text);

        }
        if (lblEmpIdHidden.Text != "")
        {
            cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);

        }

        //cmd.Parameters.AddWithValue("@Remarks", remarks);

        if (txtEmpNameSearch.Text != "")
        {
            cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmpNameSearch.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvTicketdetailsHist.DataSource = dt;
        gvTicketdetailsHist.DataBind();
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvTicketdetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvTicketdetails.SelectedRow.Cells[6].Text) && gvTicketdetails.SelectedRow.Cells[6].Text != "&nbsp;")
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
                btnDelete.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnEdit.Visible = true;
            btnDelete.Visible = false;
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
            //objMaster.Eligibility = txtEligibility.Text;
            objMaster.Eligibility = ddlEligibility.SelectedItem.Text;
            objMaster.Age = txtAge.Text;
            objMaster.Idproof = ddlIdProof.SelectedValue;
            objMaster.IdProofNo = txtIdproofno.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.TicketDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.Mobile = txtMobileNo.Text;

            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";
            objMaster.Status3 = "Pending";
            objMaster.Comment1 = txtPurpose.Text;
            objMaster.Comment2 = "-";
            objMaster.Comment3 = "-";
            objMaster.Rejected_By = "";

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
            objMaster.Eligibility = ddlEligibility.SelectedItem.Text;
            objMaster.IdProofNo = txtIdproofno.Text;
            objMaster.ApprovedBy = ddlApprovedby.SelectedItem.Value;
            objMaster.TicketDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.Idproof = ddlIdProof.SelectedValue;
            objMaster.Age = txtAge.Text;
            objMaster.Mobile = txtMobileNo.Text;

            objMaster.Status1 = "Pending";
            objMaster.Status2 = "Pending";
            objMaster.Status3 = "Pending";
            objMaster.Comment1 = txtPurpose.Text;
            objMaster.Comment2 = "-";
            objMaster.Comment3 = "-";
            objMaster.Rejected_By = "";

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
                //MessageBox.Show(this, "Record Deleted Successfully!");
            }
            catch (Exception ex)
            {
                //HR.RollBackTransaction();
                //MessageBox.Show(this, ex.Message);
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
            txtPurpose.Text = objmas.Comment1;

            ddlEligibility.SelectedItem.Text = objmas.Eligibility;
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
            gvTicketdetails.DataBind();
            HR.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvTicketdetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=TicketDetails&siid=" + gvTicketdetails.SelectedRow.Cells[0].Text + "";
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