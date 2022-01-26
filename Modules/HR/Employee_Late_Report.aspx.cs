using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_HR_Employee_Late_Report : basePage
{
    //DataTable dt = new DataTable();
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //BindEmpAttendanceGrid();
    }
    //private void BindEmpAttendanceGrid()
    //{
    //    SqlCommand cmd = new SqlCommand("USP_AttendanceManagement", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    gvEmpLate.DataSource = dt;
    //    gvEmpLate.DataBind();
    //}
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMonthName.Text = ddlMonth.SelectedItem.Text;
        gvEmpLate.DataBind();
    }
}