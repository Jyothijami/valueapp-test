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

public partial class Modules_HR_Emp_Attnd_Report : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int yy, mn, dy;
    int sundays ;
    int ttlDays, holidays;
    string Loc_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            FillCompany();
            GetYearValue();

        }
    }

    private void GetYearValue()
    {
        String sDate = DateTime.Now.ToString();
        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

         dy = datevalue.Day;
         mn = datevalue.Month;
         yy = datevalue.Year;

        txtYear.Text = yy.ToString();
    }

    static int CountSundays(int year, int month)
    {
        var firstDay = new DateTime(year, month, 1);

        var day29 = firstDay.AddDays(28);
        var day30 = firstDay.AddDays(29);
        var day31 = firstDay.AddDays(30);

        if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
        || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
        || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
        {
            return 5;

        }
        else
        {
            return 4;
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
        if (ddlMonth.SelectedIndex > 0)
       {
           var tbl = GetTable1();
           tbl.Merge(GetTable2());

           gvEmpCTC.DataSource = tbl;
           gvEmpCTC.DataBind();
           GetTable3();
           TextChangeMethod();

       }
        else
       {
           MessageBox.Show(this, "Please Select A Month to get Details");
       }
    }

    private void GetTable3()
    {
        foreach (GridViewRow r in gvEmpCTC.Rows)
        {
            Label l1 = (Label)r.FindControl("lblEmpId");

            SqlCommand cmd = new SqlCommand("USP_FillAbsentDays", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", l1.Text);
            cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Year", txtYear.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int a = Convert.ToInt32(dt.Rows[0][0]);

                TextBox t = (TextBox)r.FindControl("txtAbsentDays");
                t.Text = a.ToString();

                TextBox tds = (TextBox)r.FindControl("txtTDS");
                tds.Text = dt.Rows[0][1].ToString();

                TextBox other = (TextBox)r.FindControl("txtOther");
                other.Text = dt.Rows[0][2].ToString();

            }
        }
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
        var colm = dt.Columns.Add("EMP_ID", typeof(int));
        dt.PrimaryKey = new DataColumn[] { colm };

        da.Fill(dt);
        return dt;
      
    }
    public DataTable GetTable2()
    {
        SqlCommand cmd1 = new SqlCommand("USp_getLeavesCount", con);

        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);

        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        var colm = dt1.Columns.Add("EMP_ID", typeof(int));
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
        HR.EmployeeMaster.EmployeeMaster_SelectDept_Comp(ddlEmployee, ddlDepartment.SelectedItem.Value, ddlCompany.SelectedItem.Value);
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get Total Days In A Month
        
        int month = Convert.ToInt32(ddlMonth.SelectedItem.Value);
        yy = Convert.ToInt32(txtYear.Text);
        ttlDays= System.DateTime.DaysInMonth(yy, month);

        txtNOD.Text = ttlDays.ToString();

        //Get Total Holidays (Sundays+Holidays)

        sundays = CountSundays(Convert.ToInt32(txtYear.Text), Convert.ToInt32(ddlMonth.SelectedItem.Value));
        
        lblWoff.Text = sundays.ToString();

        HR.EmployeeMaster objmas = new HR.EmployeeMaster();
        objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
        Loc_Id = objmas.locid;
        
        SqlCommand cmd = new SqlCommand("SELECT SUM(total_days) FROM [HolidayCalender_tbl] where from_no=" + ddlMonth.SelectedItem.Value + " and from_year=" + txtYear.Text + " and Location_Id='" + Loc_Id + "'  ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            string lbl = dt.Rows[0][0].ToString();
            if (lbl != "")
            {
                holidays = Convert.ToInt32(dt.Rows[0][0]);
                lblHoli.Text = holidays.ToString();

            }
            else
            {
                holidays = 0;
                lblHoli.Text = holidays.ToString();

            }
        }
        else
        {
            MessageBox.Show(this, "Please Select Proper Month");
        }

        int totalHolidays = sundays + holidays;
        txtHolidays.Text = totalHolidays.ToString();
        BindCTCgrid();

    }
    
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        #region Save
        foreach (GridViewRow gvr in gvEmpCTC.Rows)
        {
            if (((CheckBox)gvr.FindControl("CheckBox1")).Checked)
            {
                try
                {
                    Label EmpId = (Label)gvr.FindControl("lblEmpId");
                    Label CL = (Label)gvr.FindControl("lblCL");
                    Label EL = (Label)gvr.FindControl("lblEL");
                    Label PD = (Label)gvr.FindControl("lblPD");
                    Label presentDays = (Label)gvr.FindControl("lblPresentDays");

                    TextBox absent = (TextBox)gvr.FindControl("txtAbsentDays");
                    TextBox tds = (TextBox)gvr.FindControl("txtTDS");
                    TextBox other = (TextBox)gvr.FindControl("txtOther");


                    if (tds.Text == "" || tds.Text == null) { tds.Text = "0"; }
                    if (other.Text == "" || other.Text == null) { other.Text = "0"; }

                    SqlCommand cmd = new SqlCommand("USP_AttendanceReportSave", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Emp_Id", EmpId.Text);
                    cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                    cmd.Parameters.AddWithValue("@Absent_Days", absent.Text);
                    cmd.Parameters.AddWithValue("@CL", CL.Text);
                    cmd.Parameters.AddWithValue("@EL", EL.Text);

                    cmd.Parameters.AddWithValue("@TotalNOD", txtNOD.Text);
                    cmd.Parameters.AddWithValue("@Holidays", Convert.ToDecimal(lblHoli.Text));
                    cmd.Parameters.AddWithValue("@WOff", Convert.ToDecimal(lblWoff.Text));
                    cmd.Parameters.AddWithValue("@PresentDays", presentDays.Text);
                    cmd.Parameters.AddWithValue("@Paid", PD.Text);
                    cmd.Parameters.AddWithValue("@TDS", tds.Text);  
                    cmd.Parameters.AddWithValue("@Other_Deductions", other.Text);
                    cmd.Parameters.AddWithValue("@Sal_Advance","0" );

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }

            }
            BindCTCgrid();

        }
        #endregion
    }

    protected void gvEmpCTC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region rowBound
        GridViewRow gvr = e.Row;
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox absent = (TextBox)gvr.FindControl("txtAbsentDays");
            Label CL = (Label)gvr.FindControl("lblCL");
            Label EL = (Label)gvr.FindControl("lblEL");

            Label present = (Label)gvr.FindControl("lblPresentDays");
            Label pd = (Label)gvr.FindControl("lblPD");
            Label lop = (Label)gvr.FindControl("lblLOP");

            if (CL.Text == "")
            {
                CL.Text = "0";
            }
            if (EL.Text == "")
            {
                EL.Text = "0";
            }
            if(absent.Text == "" )
            {
                absent.Text = "0";
            }
            
            present.Text = ((Convert.ToDecimal(txtNOD.Text)) - (Convert.ToDecimal(txtHolidays.Text)) - (Convert.ToDecimal(absent.Text))).ToString();

            pd.Text = ((Convert.ToDecimal(present.Text)) + (Convert.ToDecimal(txtHolidays.Text)) + (Convert.ToDecimal(CL.Text)) + (Convert.ToDecimal(EL.Text))).ToString();

            lop.Text = ((Convert.ToDecimal(txtNOD.Text)) - (Convert.ToDecimal(pd.Text))).ToString();


        }
        #endregion

        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            Label EmpName = (Label)gvr.FindControl("lblEmpName");

            if(EmpName.Text == "")
            {
                e.Row.Visible = false;
            }

        }
         if(e.Row.RowType == DataControlRowType.DataRow||e.Row.RowType == DataControlRowType.Header)
         {
             e.Row.Cells[4].Visible=false;
         }
    }
    protected void txtAbsentDays_TextChanged(object sender, EventArgs e)
    {
        TextChangeMethod();
    }

    private void TextChangeMethod()
    {
        #region txtChange
        foreach (GridViewRow gvr in gvEmpCTC.Rows)
        {
            TextBox absent = (TextBox)gvr.FindControl("txtAbsentDays");
            Label CL = (Label)gvr.FindControl("lblCL");
            Label EL = (Label)gvr.FindControl("lblEL");
            Label Extra = (Label)gvr.FindControl("lblExtra");

            Label present = (Label)gvr.FindControl("lblPresentDays");
            Label pd = (Label)gvr.FindControl("lblPD");
            Label lop = (Label)gvr.FindControl("lblLOP");
            
            if (absent.Text != "")
            {
                if (CL.Text == "")
                {
                    CL.Text = "0";
                }
                if (EL.Text == "")
                {
                    EL.Text = "0";
                }
                if (Extra.Text == "")
                {
                    Extra.Text = "0";
                }
                present.Text = ((Convert.ToDecimal(txtNOD.Text)) - (Convert.ToDecimal(txtHolidays.Text)) - (Convert.ToDecimal(absent.Text))).ToString();

                pd.Text = ((Convert.ToDecimal(present.Text)) + (Convert.ToDecimal(txtHolidays.Text)) + (Convert.ToDecimal(CL.Text)) + (Convert.ToDecimal(EL.Text))).ToString();

                lop.Text = ((Convert.ToDecimal(txtNOD.Text)) - (Convert.ToDecimal(pd.Text))).ToString();
            }
        }
        #endregion
    }
}