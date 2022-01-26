using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DatumDAL;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using vllib;

public partial class Modules_HR_LeaveAllocation : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvUpdateLeaves.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvUpdateLeaves.Rows)
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateLeaveTbl();
        gvUpdateLeaves.DataBind();
        ClearFields();
    }

    private void ClearFields()
    {
        if (i > 0)
        {
            txtEarnedLeaves.Text = "";
            txtCasualLeaves.Text = "";
            MessageBox.Show(this, "Leaves Updated Successfully");
            //lblMsg.ForeColor = System.Drawing.Color.Green;
            //lblMsg.Text = "Leaves Updated Successfully";
        }
        else
        {
            MessageBox.Show(this,"Leave Updating Failed Please Contact Admin :");
            //lblMsg.ForeColor = System.Drawing.Color.Red;
            //lblMsg.Text = "Leave Updating Failed";
        }
    }

    protected void UpdateLeaveTbl()
    {
        
        foreach (GridViewRow gvr in gvUpdateLeaves.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    decimal eLeaves;
                    decimal cLeaves;
                    Label EmpId = (Label)gvr.FindControl("lblEmpId");
                    Label DesgId = (Label)gvr.FindControl("lblDesgId");
                    Label DeptId = (Label)gvr.FindControl("lblDepartmentId");
                    Label EarnedLeaves = (Label)gvr.FindControl("lblEarnedLeaves");
                    Label CasualLeaves = (Label)gvr.FindControl("lblCasualLeaves");

                    if (EarnedLeaves.Text == "")
                    {
                        EarnedLeaves.Text = "0";
                        eLeaves = Convert.ToDecimal(EarnedLeaves.Text);

                    }
                    else
                    {
                        eLeaves = Convert.ToDecimal(EarnedLeaves.Text);

                    }
                    if (CasualLeaves.Text == "")
                    {
                        CasualLeaves.Text = "0";
                        cLeaves = Convert.ToDecimal(CasualLeaves.Text);

                    }
                    else
                    {
                        cLeaves = Convert.ToDecimal(CasualLeaves.Text);

                    }
                                       
                    //Label CasualLeaves = (Label)gvr.FindControl("lblCasualLeaves");
                    //int eLeaves = Convert.ToInt32(EarnedLeaves.Text.ToString());
                    //decimal el = Convert.ToDecimal(txtEarnedLeaves.Text);
                    SqlCommand cmd = new SqlCommand("Usp_UpdateNoOfLeave", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Emp_Id", EmpId.Text);
                    cmd.Parameters.AddWithValue("@DESG_ID", DesgId.Text);
                    cmd.Parameters.AddWithValue("@DEPT_ID", DeptId.Text);


                    if (txtCasualLeaves.Text == "")
                    {
                        txtCasualLeaves.Text = "0";
                        cLeaves = cLeaves + Convert.ToDecimal(txtCasualLeaves.Text);
                        cmd.Parameters.AddWithValue("@Casual_Leaves", cLeaves);
                    }
                    else
                    {
                        cLeaves = cLeaves + Convert.ToDecimal(txtCasualLeaves.Text);
                        cmd.Parameters.AddWithValue("@Casual_Leaves", cLeaves);

                    }
                    if (txtEarnedLeaves.Text == "")
                    {
                        txtEarnedLeaves.Text = "0";
                        eLeaves = eLeaves + Convert.ToDecimal(txtEarnedLeaves.Text);
                        cmd.Parameters.AddWithValue("@Earned_Leaves", eLeaves);
                    }
                    else
                    {
                        eLeaves = eLeaves + Convert.ToDecimal(txtEarnedLeaves.Text);
                        cmd.Parameters.AddWithValue("@Earned_Leaves", eLeaves);

                    }
                    //if (eLeaves >= 5)
                    //{
                    //    decimal l = el + 5;
                    //    cmd.Parameters.AddWithValue("@Earned_Leaves", l);

                    //}
                    //else
                    //{
                    //    decimal j = el + eLeaves;
                    //    cmd.Parameters.AddWithValue("@Earned_Leaves", j);

                    //}
                    
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();                    

                }


                
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
            
        }    
    }
}