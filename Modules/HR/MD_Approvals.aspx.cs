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

public partial class Modules_HR_MD_Approvals : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int i,j,k,l,m;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            OnDutyForm.Visible = true;
            # region set color for link buttons
            if (gvOndutyPend.Rows.Count > 0)
            {
                lnkOnDuty.ForeColor = System.Drawing.Color.Red;
            }
            if (gvOneHrPend.Rows.Count > 0)
            {
                lnkOneHour.ForeColor = System.Drawing.Color.Red;

            }
            if (gvOvertime.Rows.Count > 0)
            {
                lnkOverTime.ForeColor = System.Drawing.Color.Red;

            }
            if (gvShiftchange.Rows.Count > 0)
            {
                lnkShiftChange.ForeColor = System.Drawing.Color.Red;

            }
            if (gvTicketdetails.Rows.Count > 0)
            {
                lnkTickets.ForeColor = System.Drawing.Color.Red;

            }
            #endregion
            setControlsVisibility();
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "118");
        btnOnDutyAppr.Enabled = up.Approve;
        btnOnDutyReject.Enabled = up.Approve;
        btnOneHrAppr.Enabled = up.Approve;
        btnOneHrReject.Enabled = up.Approve;
        btnOverTimeAppr.Enabled = up.Approve;
        btnOverTimeReject.Enabled = up.Approve;
        btnShiftAppr.Enabled = up.Approve;
        btnShiftReject.Enabled = up.Approve;
        btnTicketAppr.Enabled = up.Approve;
        btnTicketReject.Enabled = up.Approve;
    }
    protected void lnkOnDuty_Click(object sender, EventArgs e)
    {
        OnDutyForm.Visible = true;
        OneHourPerm.Visible = false;
        OverTime.Visible = false;
        ShiftChange.Visible = false;
        TicketDetails.Visible = false;
        gvOndutyPend.DataBind();
        gvOnDutyHist.DataBind();

    }
    protected void lnkOneHour_Click(object sender, EventArgs e)
    {
        OnDutyForm.Visible = false;
        OneHourPerm.Visible = true;
        OverTime.Visible = false;
        ShiftChange.Visible = false;
        TicketDetails.Visible = false;
        gvOneHrPend.DataBind();
        gvOneHrhist.DataBind();
    }
    protected void lnkOverTime_Click(object sender, EventArgs e)
    {
        OnDutyForm.Visible = false;
        OneHourPerm.Visible = false;
        OverTime.Visible = true;
        ShiftChange.Visible = false;
        TicketDetails.Visible = false;
    }
    protected void lnkShiftChange_Click(object sender, EventArgs e)
    {
        OnDutyForm.Visible = false;
        OneHourPerm.Visible = false;
        OverTime.Visible = false;
        ShiftChange.Visible = true;
        TicketDetails.Visible = false;
    }
    protected void lnkTickets_Click(object sender, EventArgs e)
    {
        OnDutyForm.Visible = false;
        OneHourPerm.Visible = false;
        OverTime.Visible = false;
        ShiftChange.Visible = false;
        TicketDetails.Visible = true;
    }
    protected void btnOnDutyReject_Click(object sender, EventArgs e)
    {
        i = 0;
        OnDutyApprove();
        gvOndutyPend.DataBind();
        gvOnDutyHist.DataBind();
    }
    protected void btnOnDutyAppr_Click(object sender, EventArgs e)
    {
        i = 1;
        OnDutyApprove();
        gvOndutyPend.DataBind();
        gvOnDutyHist.DataBind();
    }

    private void OnDutyApprove()
    {
        #region Approve/Reject Application
        foreach (GridViewRow gvr in gvOndutyPend.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblId");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval_OnDuty", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OnDuty_ID", leaveId.Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Approved");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    protected void btnOneHrAppr_Click(object sender, EventArgs e)
    {
        j = 1;
        OneHourApprove();
        gvOneHrPend.DataBind();
        gvOneHrhist.DataBind();
    }
    protected void btnOneHrReject_Click(object sender, EventArgs e)
    {
        j = 0;
        OneHourApprove();
        gvOneHrPend.DataBind();
        gvOneHrhist.DataBind();
    }

    private void OneHourApprove()
    {
        #region Approve/Reject Application
        foreach (GridViewRow gvr in gvOneHrPend.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label oneHourId = (Label)gvr.FindControl("lblOnehour");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval_OneHour", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@One_Hour_ID", oneHourId.Text);
                    if (j == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Approved");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (j == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    protected void btnOverTimeAppr_Click(object sender, EventArgs e)
    {
        k = 1;
        OverTimeApprove();
        gvOvertime.DataBind();
        gvOvertimeHist.DataBind();
    }
    protected void btnOverTimeReject_Click(object sender, EventArgs e)
    {
        k = 0;
        OverTimeApprove();
        gvOvertime.DataBind();
        gvOvertimeHist.DataBind();
    }

    private void OverTimeApprove()
    {
        #region Approve/Reject Application
        foreach (GridViewRow gvr in gvOvertime.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label lblOverTime = (Label)gvr.FindControl("lblOverTime");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval_OverTime", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Overtime_ID", lblOverTime.Text);
                    if (k == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Approved");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (k == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    protected void btnShiftAppr_Click(object sender, EventArgs e)
    {
        l = 1;
        ShiftChangeApprove();
        gvShiftchange.DataBind();
        gvShiftchangeHist.DataBind();
    }
    protected void btnShiftReject_Click(object sender, EventArgs e)
    {
        l = 0;
        ShiftChangeApprove();
        gvShiftchange.DataBind();
        gvShiftchangeHist.DataBind();
    }
    private void ShiftChangeApprove()
    {
        #region Approve/Reject Application
        foreach (GridViewRow gvr in gvShiftchange.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label lblShiftChange = (Label)gvr.FindControl("lblShiftChange");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval_ShiftChange", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Shift_Change_ID", lblShiftChange.Text);
                    if (l == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Approved");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (l == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    protected void btnTicketAppr_Click(object sender, EventArgs e)
    {
        m = 1;
        TicketDetailsApprove();
        gvTicketdetails.DataBind();
        gvTicketdetailsHist.DataBind();
    }
    protected void btnTicketReject_Click(object sender, EventArgs e)
    {
        m = 0;
        TicketDetailsApprove();
        gvTicketdetails.DataBind();
        gvTicketdetailsHist.DataBind();
    }
    private void TicketDetailsApprove()
    {
        #region Approve/Reject Application
        foreach (GridViewRow gvr in gvTicketdetails.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label lblTicketDetails = (Label)gvr.FindControl("lblTicketDetails");
                    SqlCommand cmd = new SqlCommand("USP_UpdateMDApproval_TicketDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TicketDetails_Id", lblTicketDetails.Text);
                    if (m == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Approved");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (m == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "MD");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
}