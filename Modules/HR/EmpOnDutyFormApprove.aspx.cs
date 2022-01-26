using System;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using Yantra.Classes;
using vllib;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Modules_HR_EmpOnDutyFormApprove : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int i,j,k,l,m,n;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OnDutyForm.Visible = true;
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
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
            LoadOnDutyHistory();
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "117");
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
    protected void btnOnDutyReject_Click(object sender, EventArgs e)
    {
        i = 0;
        OnDutyApprove();
        gvOndutyPend.DataBind();
        //gvOnDutyHist.DataBind();
        LoadOnDutyHistory();
    }
    protected void btnOnDutyAppr_Click(object sender, EventArgs e)
    {
        i = 1;
        OnDutyApprove();
        gvOndutyPend.DataBind();
        //gvOnDutyHist.DataBind();
        LoadOnDutyHistory();

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
                    Label onDutyId = (Label)gvr.FindControl("lblId");
                    TextBox CoffDays = (TextBox)gvr.FindControl("txtCompOff");

                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval_OnDuty", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OnDuty_ID", onDutyId.Text);
                    if (i == 1)
                    {
                        cmd.Parameters.AddWithValue("@C_Off", CoffDays.Text);
                        cmd.Parameters.AddWithValue("@Status2", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (i == 0)
                    {
                        cmd.Parameters.AddWithValue("@C_Off", CoffDays.Text);
                        cmd.Parameters.AddWithValue("@Status2", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "HR");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmd1 = new SqlCommand("Usp_COff", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@EmpId", gvr.Cells[11].Text);
                    cmd1.Parameters.AddWithValue("@COffDays", CoffDays.Text);
                    con.Open();
                    cmd1.ExecuteNonQuery();
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
    protected void lnkOnDuty_Click(object sender, EventArgs e)
    {
        OnDutyForm.Visible = true;
        OneHourPerm.Visible = false;
        OverTime.Visible = false;
        ShiftChange.Visible = false;
        TicketDetails.Visible = false;
        gvOndutyPend.DataBind();
        //gvOnDutyHist.DataBind();
        LoadOnDutyHistory();

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
        LoadOverTimeHistory();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadOnDutyHistory();
    }

    private void LoadOnDutyHistory()
    {
        SqlCommand cmd = new SqlCommand("USP_HROnDutyHistoryGrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Status2", "Pending");

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        if (txtSearch.Text != "")
        {
            cmd.Parameters.AddWithValue("@Emp_Name", txtSearch.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvOnDutyHist.DataSource = dt;
        gvOnDutyHist.DataBind();
    }
    protected void gvOnDutyHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOnDutyHist.PageIndex = e.NewPageIndex;
        LoadOnDutyHistory();

    }
    protected void btnSearch_Overtime_Click(object sender, EventArgs e)
    {
        LoadOverTimeHistory();
    }

    private void LoadOverTimeHistory()
    {
        SqlCommand cmd = new SqlCommand("USP_HROverTimeHistoryGrid", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Status2", "Pending");

        if (txtFromDate_Overtime.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate_Overtime.Text));
        }
        if (txtToDate_Overtime.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate_Overtime.Text));
        }
        if (txtEmpName_Overtime.Text != "")
        {
            cmd.Parameters.AddWithValue("@Emp_Name", txtEmpName_Overtime.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvOvertimeHist.DataSource = dt;
        gvOvertimeHist.DataBind();
    }
    protected void gvOvertimeHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOvertimeHist.PageIndex = e.NewPageIndex;
        LoadOverTimeHistory();
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
    protected void btnOneHrAppr_Click(object sender, EventArgs e)
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
                    Label onrHourID = (Label)gvr.FindControl("lblOnehour");
                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval_OneHour", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@One_Hour_ID", onrHourID.Text);
                    if (j == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (j == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "HR");

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
    protected void btnOneHrReject_Click(object sender, EventArgs e)
    {
        j = 1;
        OneHourApprove();
        gvOneHrPend.DataBind();
        gvOneHrhist.DataBind();
    }
    protected void btnOverTimeReject_Click(object sender, EventArgs e)
    {
        k = 0;
        OverTimeApprove();
        gvOvertime.DataBind();
        //gvOvertimeHist.DataBind();
        LoadOnDutyHistory();
    }
    protected void btnOverTimeAppr_Click(object sender, EventArgs e)
    {
        k = 1;
        OverTimeApprove();
        gvOvertime.DataBind();
        //gvOvertimeHist.DataBind();
        LoadOnDutyHistory();
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
                    TextBox CoffDays = (TextBox)gvr.FindControl("txtCompOff");

                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval_OverTime", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Overtime_ID", lblOverTime.Text);
                    if (k == 1)
                    {
                        cmd.Parameters.AddWithValue("@C_Off", CoffDays.Text); 
                        cmd.Parameters.AddWithValue("@Status2", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (k == 0)
                    {
                        cmd.Parameters.AddWithValue("@C_Off", CoffDays.Text);
                        cmd.Parameters.AddWithValue("@Status2", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "HR");

                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmd1 = new SqlCommand("Usp_COff", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@EmpId", gvr.Cells[11].Text);
                    cmd1.Parameters.AddWithValue("@COffDays", CoffDays.Text);
                    con.Open();
                    cmd1.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval_ShiftChange", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Shift_Change_ID", lblShiftChange.Text);
                    if (l == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (l == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "HR");

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
                    SqlCommand cmd = new SqlCommand("USP_UpdateHRApproval_TicketDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TicketDetails_Id", lblTicketDetails.Text);
                    if (m == 1)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Approved");
                        cmd.Parameters.AddWithValue("@Status3", "Pending");
                        cmd.Parameters.AddWithValue("@Rejected_By", "-");

                    }
                    else if (m == 0)
                    {
                        cmd.Parameters.AddWithValue("@Status2", "Rejected");
                        cmd.Parameters.AddWithValue("@Status3", "Rejected");
                        cmd.Parameters.AddWithValue("@Rejected_By", "HR");

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

    protected void gvOvertime_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;          

        }
    }
    protected void gvOndutyPend_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Visible = false;

        }
    }
}