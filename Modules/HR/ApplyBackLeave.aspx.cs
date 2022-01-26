using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.Classes;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using vllib;


public partial class Modules_HR_ApplyBackLeave : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDept();
            DateTime now = DateTime.Now;
            txtDateOfApply.Text = now.ToString("dd/MM/yyyy");
            rbnFrom.SelectedValue = "0";
            rbnTo.SelectedValue = "1";
        }
    }

    private void GetAvailableLeaves()
    {
        SqlCommand cmd = new SqlCommand("Usp_GetAvailableLeaves", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMP_Id", ddlEmployee.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtAvailableCasualLeaves.Text = dt.Rows[0][0].ToString();
            txtAvailableEarnedLeaves.Text = dt.Rows[0][1].ToString();
            txtAvailableExtra.Text = dt.Rows[0][2].ToString();            
        }

    }

    private void FillDept()
    {
        try
        {
            HR.Dept_Select(ddlDept);

        }
        catch
        {
        }
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        
        if (ddlTypeOfLeave.SelectedValue == "0")
        {
            MessageBox.Show(this, "Please Select Any Type Of Leave");

            btnApplyLeave.Visible = false;
        }
        else
        {

            DateTime d1 = Convert.ToDateTime(General.toMMDDYYYY(txtFromDate.Text));
            DateTime d2 = Convert.ToDateTime(General.toMMDDYYYY(txtToDate.Text));
            TimeSpan t = d2 - d1;
            double days = t.TotalDays + 1;
            txtTotalDaysOfLeave.Text = days.ToString();

            decimal totalLeaves = Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString());
            decimal casual = Convert.ToDecimal(txtAvailableCasualLeaves.Text.ToString());
            decimal earned = Convert.ToDecimal(txtAvailableEarnedLeaves.Text.ToString());
            decimal extra = Convert.ToDecimal(txtAvailableExtra.Text.ToString());
            if (rbnFrom.SelectedValue == "0" && rbnTo.SelectedValue == "0")
            {
                days = days - 0.5;
                txtTotalDaysOfLeave.Text = days.ToString();

            }
            else if (rbnFrom.SelectedValue == "1" && rbnTo.SelectedValue == "1")
            {
                days = days - 0.5;
                txtTotalDaysOfLeave.Text = days.ToString();

            }
            else if (rbnFrom.SelectedValue == "1" && rbnTo.SelectedValue == "0")
            {
                days = days - 1;
                txtTotalDaysOfLeave.Text = days.ToString();

            }
            if (ddlTypeOfLeave.SelectedValue == "1")
            {
                if (totalLeaves <= casual)
                {
                    btnApplyLeave.Visible = true;
                }
                else
                {
                    btnApplyLeave.Visible = false;
                    MessageBox.Show(this, "Please Make sure your Leave Count not to exceed your available Leaves");
                    //lblMsg.ForeColor = System.Drawing.Color.Red; 
                    //lblMsg.Text = "Please Make sure your Leave Count not to exceed your available Leaves";
                    txtToDate.Text = "";

                }
            }
            else if (ddlTypeOfLeave.SelectedValue == "2")
            {
                if (totalLeaves <= earned)
                {
                    btnApplyLeave.Visible = true;
                }
                else
                {
                    btnApplyLeave.Visible = false;
                    MessageBox.Show(this, "Please Make sure your Leave Count not to exceed your available Leaves");
                    //lblMsg.ForeColor = System.Drawing.Color.Red; 
                    //lblMsg.Text = "Please Make sure your Leave Count not to exceed your available Leaves";
                    txtToDate.Text = "";

                }
            }
            else if (ddlTypeOfLeave.SelectedValue == "3")
            {
                if (totalLeaves <= extra)
                {
                    btnApplyLeave.Visible = true;
                }
                else
                {
                    btnApplyLeave.Visible = false;
                    MessageBox.Show(this, "This Leave Cannot be submitted. Please Contact the HR");
                    txtToDate.Text = "";

                }
            }
            else if (ddlTypeOfLeave.SelectedValue == "3")
            {
                btnApplyLeave.Visible = true;
            }
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmployee, ddlDept.SelectedItem.Value);

    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster obj = new HR.EmployeeMaster();
        obj.EmployeeMaster_Select(ddlEmployee.SelectedItem.Value);
        txtDesignation.Text = obj.DesgName12;
        GetAvailableLeaves();

    }
    protected void btnApplyLeave_Click(object sender, EventArgs e)
    {
        RandomGenerator();
        SaveLeaveApplication();
        UpdateLeavetbl();
        ClearFields();
        GetAvailableLeaves();
        gvPendingLeaves.DataBind();
        MessageBox.Show(this, "Back Leave Application Saved Successfully");

    }

    protected void RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        lblLeaveId.Text = intRandomNumber.ToString();

    }

    protected void SaveLeaveApplication()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("USP_SaveUserLeaveApp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Leave_Id", lblLeaveId.Text);

            cmd.Parameters.AddWithValue("@Emp_Id", ddlEmployee.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Casual_Leaves", txtAvailableCasualLeaves.Text);
            cmd.Parameters.AddWithValue("@Earned_Leaves", txtAvailableEarnedLeaves.Text);
            cmd.Parameters.AddWithValue("@Emp_Name", ddlEmployee.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@Emp_Designation", txtDesignation.Text);

            cmd.Parameters.AddWithValue("@Emp_Department", ddlDept.SelectedItem.Value);
            string dOA = General.toMMDDYYYY(txtDateOfApply.Text);
            cmd.Parameters.AddWithValue("@DateOfApplying", dOA);
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));

            cmd.Parameters.AddWithValue("@AppliedNoOfLeaves", txtTotalDaysOfLeave.Text);
            cmd.Parameters.AddWithValue("@TypeOfLeave", ddlTypeOfLeave.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Reason", txtReason.Text);
            cmd.Parameters.AddWithValue("@AddressInLeavePeriod", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Status1", "Approved");

            cmd.Parameters.AddWithValue("@Status2", "Approved");
            cmd.Parameters.AddWithValue("@Status3", "Approved");
            cmd.Parameters.AddWithValue("@Comment1", "Back Leave");
            cmd.Parameters.AddWithValue("@FromSession", rbnFrom.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@ToSession", rbnTo.SelectedItem.Text);

            string path = "";
            string fileName = "";
            string fileUploaded = "";
            if (FileUpload1.HasFile)
            {
                path = Server.MapPath("~/Content/Leave_Forms/");
                fileName = FileUpload1.PostedFile.FileName;
                FileUpload1.SaveAs(path + fileName);
                fileUploaded = FileUpload1.PostedFile.FileName;                
            }
          
            cmd.Parameters.AddWithValue("@Comment2", fileUploaded);
            cmd.Parameters.AddWithValue("@Comment3", "");
            cmd.Parameters.AddWithValue("@Rejected_By", "");
            
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            con.Close();
        }
       
    }

    protected void UpdateLeavetbl()
    {
        if (ddlTypeOfLeave.SelectedValue != "3")
        {
            SqlCommand cmd = new SqlCommand("Usp_UpdateLeavetbl", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Emp_Id", ddlEmployee.SelectedItem.Value);

            if (ddlTypeOfLeave.SelectedValue == "1")
            {
                decimal casual = (Convert.ToDecimal(txtAvailableCasualLeaves.Text.ToString()) - Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString()));
                cmd.Parameters.AddWithValue("@Casual_Leaves", casual);
                cmd.Parameters.AddWithValue("@Earned_Leaves", txtAvailableEarnedLeaves.Text);
            }
            else if (ddlTypeOfLeave.SelectedValue == "2")
            {
                decimal earned = (Convert.ToDecimal(txtAvailableEarnedLeaves.Text.ToString()) - Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString()));
                cmd.Parameters.AddWithValue("@Casual_Leaves", txtAvailableCasualLeaves.Text);
                cmd.Parameters.AddWithValue("@Earned_Leaves", earned);
            }
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else if (ddlTypeOfLeave.SelectedValue == "3")
        {
            SqlCommand cmd1 = new SqlCommand("Usp_UpdateLeavetbl_Extra", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Emp_Id", ddlEmployee.SelectedItem.Value);
            decimal extra = (Convert.ToDecimal(txtAvailableExtra.Text.ToString()) - Convert.ToDecimal(txtTotalDaysOfLeave.Text.ToString()));
            cmd1.Parameters.AddWithValue("@Extra_Leaves", extra);
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
        }
    }

    private void ClearFields()
    {
        txtFromDate.Text = txtToDate.Text = txtTotalDaysOfLeave.Text = txtReason.Text = txtAddress.Text = txtDesignation.Text = txtAvailableCasualLeaves.Text=txtAvailableEarnedLeaves.Text=  "";
        ddlTypeOfLeave.SelectedIndex = ddlEmployee.SelectedIndex = ddlDept.SelectedIndex = 0;
        btnApplyLeave.Visible = false;
    }
    protected void rbnFrom_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnApplyLeave.Visible = false;
    }
    protected void rbnTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnApplyLeave.Visible = false;
    }
}
 
