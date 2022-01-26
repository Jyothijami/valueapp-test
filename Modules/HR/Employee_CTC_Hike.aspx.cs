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
using Yantra.MessageBox;
using YantraBLL.Modules;
using Yantra.Classes;
using vllib;
using System.IO;
using System.Drawing;

using System.Data.SqlClient;

public partial class Modules_HR_Employee_CTC_Hike : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCTCgrid();
            FillCompany();
        }

    }

    #region FillCompany
    private void FillCompany()
    {
        try
        {
            HR.Company_Select(ddlCompany);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindCTCgrid();
        RefreshSearchFields();
    }

    private void RefreshSearchFields()
    {
        ddlCompany.SelectedIndex = 0;
        ddlDepartment.DataSource = string.Empty;
        ddlDepartment.DataBind();
        ddlEmployee.DataSource = string.Empty;
        ddlEmployee.DataBind();

        txtLocation.Text = "";
    }
    protected void gvEmpCTC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpCTC.PageIndex = e.NewPageIndex;
        BindCTCgrid();
    }

    private void BindCTCgrid()
    {
        var tbl = GetTable1();
        tbl.Merge(GetTable2());
        gvEmpCTC.DataSource = tbl;
        gvEmpCTC.DataBind();
    }

    public DataTable GetTable1()
    {
        SqlCommand cmd = new SqlCommand("USP_Emp_Master_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlCompany.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedItem.Value);
        }

        if (txtLocation.Text != "")
        {
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);

        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Department", ddlDepartment.SelectedItem.Value);

        }
        if (ddlEmployee.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@EmployeeId", ddlEmployee.SelectedItem.Value);

        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        var colm = dt.Columns.Add("Emp_Id", typeof(int));
        dt.PrimaryKey = new DataColumn[] { colm };
        da.Fill(dt);
        return dt;
    }
    public DataTable GetTable2()
    {
        SqlCommand cmd1 = new SqlCommand("Usp_salHistory", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        var colm = dt1.Columns.Add("Emp_Id", typeof(int));
        dt1.PrimaryKey = new DataColumn[] { colm };
        da1.Fill(dt1);
        return dt1;
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDept();
        HR.EmployeeMaster.EmployeeMaster_SelectFromCompany(ddlEmployee, ddlCompany.SelectedItem.Value);

    }
    private void FillDept()
    {
        try
        {
            HR.Dept_Select(ddlDepartment);

        }
        catch
        {
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster.EmployeeMaster_SelectDept_Comp(ddlEmployee, ddlDepartment.SelectedItem.Value,ddlCompany.SelectedItem.Value);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        UpdateCTC();
        BindCTCgrid();
    }
    private void UpdateCTC()
    {
        foreach (GridViewRow gvr in gvEmpCTC.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvr.FindControl("CheckBox1");
            if (ch.Checked == true)
            {
                try
                {
                    HR.EmployeeMaster obj = new HR.EmployeeMaster();
                    TextBox ctc = (TextBox)gvr.FindControl("txtGross");
                    obj.GrossSal = ctc.Text;
                    obj.EmpFirstName = gvr.Cells[1].Text;
                    obj.EmployeeCTC_Update(gvr.Cells[0].Text);

                    obj.CurrentCTC = gvr.Cells[12].Text;
                    obj.Employee_Sal_His_Update(gvr.Cells[0].Text);


                    #region Employee Age
                    int age = obj.GetAge(Convert.ToInt32(gvr.Cells[0].Text));
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
                    decimal salary = Convert.ToDecimal(ctc.Text) / 12;
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
                        cmd1.Parameters.AddWithValue("@Emp_ID", gvr.Cells[0].Text);
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
                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    ch.Checked = false;
                }
            }
        }

    }
    protected void gvEmpCTC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = new Label();
            lbl.Text = e.Row.Cells[2].Text;
            if (lbl.Text == "" || lbl.Text == "&nbsp;")
            {
                e.Row.Visible = false;
            }
            if (e.Row.Cells[12].Text != "&nbsp;")
            {
                e.Row.Cells[11].Text = Math.Round(Convert.ToDecimal(e.Row.Cells[12].Text) / 12).ToString();
            }
            if (e.Row.Cells[12].Text != "&nbsp;")
            {
                SqlCommand cmd = new SqlCommand("Salary_Breakup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fsal", Convert.ToDecimal(e.Row.Cells[12].Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Label lblStatus = new Label();
                    lblStatus.Text = dt.Rows[0][15].ToString();
                    if (lblStatus.Text == "ESI_is_Applicable")
                    {
                        e.Row.Cells[10].Text = Math.Round(Convert.ToDecimal(dt.Rows[0][9]) / 12).ToString();
                    }
                    else
                    {
                        e.Row.Cells[10].Text = Math.Round(Convert.ToDecimal(dt.Rows[0][8]) / 12).ToString();
                    }
                }
            }
        }

    }
}