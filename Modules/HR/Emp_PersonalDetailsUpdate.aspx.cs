using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using vllib;

public partial class Modules_HR_Emp_PersonalDetailsUpdate : basePage
{
    SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();

        }

    }

    private void setControlsVisibility()
    {

        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "87");

        btnApprove.Enabled = up.Approve;
        btnReject.Enabled = up.Delete;
        
    }


    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
       CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxRows.Checked == true)
            {
                Label EmpId = (Label)row.FindControl("lblEmpId");
                Label FName = (Label)row.FindControl("lblFirstName");
                Label LName = (Label)row.FindControl("lblLastName");
                Label FatName = (Label)row.FindControl("lblFatherName");
                Label DOB = (Label)row.FindControl("lblDOB");
                Label Mobile = (Label)row.FindControl("lblMobile");
                Label Address = (Label)row.FindControl("lblAddress");
                Label Email = (Label)row.FindControl("lblEmail");

                SqlCommand cmd = new SqlCommand("USP_UpdateEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_ID", EmpId.Text);
                cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", FName.Text);
                cmd.Parameters.AddWithValue("@EMP_LAST_NAME", LName.Text);
                cmd.Parameters.AddWithValue("@FATHER_NAME", FatName.Text);
                cmd.Parameters.AddWithValue("@EMP_DOB", DOB.Text);
                cmd.Parameters.AddWithValue("@EMP_MOBILE", Mobile.Text);
                cmd.Parameters.AddWithValue("@EMP_ADDRESS", Address.Text);
                cmd.Parameters.AddWithValue("@EMP_EMAIL", Email.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //###########
                SqlCommand cmd1 = new SqlCommand("update Emp_PersonalDetailsUpdate set Status='Approved' where EMP_ID=@EMP_ID ", con);
                cmd1.Parameters.AddWithValue("@EMP_ID", EmpId.Text);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
                GridView1.DataBind();
            }
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
         CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkhdr");
         foreach (GridViewRow row in GridView1.Rows)
         {
             CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
             if (ChkBoxRows.Checked == true)
             {
                 Label EmpId = (Label)row.FindControl("lblEmpId");
                 SqlCommand cmd = new SqlCommand("update Emp_PersonalDetailsUpdate set Status='Reject' where EMP_ID=@EMP_ID  ", con);
                 cmd.Parameters.AddWithValue("@EMP_ID", EmpId.Text);
                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();
                 GridView1.DataBind();
             }
         }
    }
}