using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;
using vllib;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modules_Masters_Attendance_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindAttendence();
        }
    }

    private void BindAttendence()
    {
        if (txtEmpName.Text == "")
        {
            if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex != 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence_Location", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence_TotalTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtInTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence_InTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtOutTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence_OutTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text != "" && txtInTime.Text != "" || txtTotalTime.Text != "" && txtOutTime.Text != "" || txtOutTime.Text != "" && txtInTime.Text != "")
            {
                MessageBox.Show(this, "Please Provide Only One Search Criteria at a time");
            }

        }
        else
        {
            if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex != 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_Location", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }

            else if (txtTotalTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_TotalTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtInTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_InTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtOutTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_OutTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text != "" && txtInTime.Text != "" || txtTotalTime.Text != "" && txtOutTime.Text != "" || txtOutTime.Text != "" && txtInTime.Text != "")
            {
                MessageBox.Show(this, "Please Provide Only One Search Criteria at a time");
            }

        }
    }
    protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAttendence();
        
    }
    protected void btnTotalGo_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtTotalTime.Text = "";
        txtInTime.Text = "";
        txtOutTime.Text = "";
    }
    protected void btnInGo_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtTotalTime.Text = "";
        txtInTime.Text = "";
        txtOutTime.Text = "";
    }
    protected void btnOutGo_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtTotalTime.Text = "";
        txtInTime.Text = "";
        txtOutTime.Text = "";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtEmpName.Text = "";
    }
}
 
