using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using DatumDAL;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using vllib;
using Yantra.Classes;
using System.IO;
using System.Drawing;

public partial class Modules_HR_LeaveHistory : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            DdlFill();
            BindLeaveHistory();
            BindOneHourHistory();
            BindTicketHistory();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "115");
        //btnNew.Enabled = up.add;
        //btnEdit.Enabled = up.Update;
        btnCancel.Enabled = up.Delete;
        
    }
    private void DdlFill()
    {
        try
        {
            HR.EmployeeMaster.LeaveEmpMaster_Select(ddlOneHourEmpName);
            HR.EmployeeMaster.LeaveEmpMaster_Select(ddlEmpName);
            HR.EmployeeMaster.LeaveEmpMaster_Select(ddlTicketEmpName);
            HR.EmpLeave.LeaveType_Select(ddlType);
            HR.EmpLeave.LeaveStatus_Select(ddlStatus);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void btnOneHrSearch_Click(object sender, EventArgs e)
    {
        BindOneHourHistory();
    }
    private void BindOneHourHistory()
    {
        SqlCommand cmd = new SqlCommand("USP_ONE_HOUR_HISTORY", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlOneHourEmpName.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlOneHourEmpName.SelectedItem.Value);
        }
        //if (ddlType.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
        //}
        if (ddlOneHourStatus.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Status", ddlOneHourStatus.SelectedItem.Text);
        }
        if (txtOneHrFrmDt.Text != "")
        {
            //cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtOneHrFrmDt.Text));
        }
        if (txtOneHrToDt.Text != "")
        {
            //cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtOneHrToDt.Text));
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvOneHourHistory.DataSource = dt;
        gvOneHourHistory.DataBind();
    }
    private void BindTicketHistory()
    {
        SqlCommand cmd = new SqlCommand("[USP_TicketHistory]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlTicketEmpName.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlTicketEmpName.SelectedItem.Value);
        }
        //if (ddlType.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
        //}
        if (ddlTicketStatus.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Status", ddlTicketStatus.SelectedItem.Text);
        }
        if (txtTicketFromDt.Text != "")
        {
            //cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtTicketFromDt.Text));
        }
        if (txtTicketToDt.Text != "")
        {
            //cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtTicketToDt.Text));
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvTicketHistory.DataSource = dt;
        gvTicketHistory.DataBind();
    }
    private void BindLeaveHistory()
    {
        SqlCommand cmd = new SqlCommand("USP_LeaveHistory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlEmpName.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlEmpName.SelectedItem.Value);
        }
        if (ddlType.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedItem.Text);
        }
        if (txtFromDate.Text != "")
        {
            //cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            //cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvLeaveHistory.DataSource = dt;
        gvLeaveHistory.DataBind();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLeaveHistory.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvLeaveHistory.DataBind();
    }
    protected void ddlTicket_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvTicketHistory.PageSize = Convert.ToInt32(ddlTicket.SelectedValue);
        gvTicketHistory.DataBind();
    }
    protected void ddlHistoryNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvOneHourHistory.PageSize = Convert.ToInt32(ddlHistoryNoOfRecords.SelectedValue);
        gvOneHourHistory.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    { 
        
        foreach (GridViewRow gvr in gvLeaveHistory.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    lblLeaveIdTemp.Text = gvr.Cells[1].Text;
                    lblEmpId.Text = gvr.Cells[2].Text;
                    lblNoOfLeaves.Text = gvr.Cells[11].Text;
                    lblTypeOfLeave.Text = gvr.Cells[12].Text;
                    UpdateLeaveTblOnDelete();
                    SqlCommand cmd2 = new SqlCommand("Delete from EMP_Leave_tbl where Leave_Id=@Leave_Id", con);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@Leave_Id", lblLeaveIdTemp.Text);
                    con.Open();
                    int i = cmd2.ExecuteNonQuery();
                    con.Close();

                    SqlCommand cmd = new SqlCommand("Delete from [OB_CB_tbl] where Leave_Id=@Leave_Id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Leave_Id", lblLeaveIdTemp.Text);
                    con.Open();
                     cmd.ExecuteNonQuery();
                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    BindLeaveHistory();
                }
            }
        }
    }

    protected void UpdateLeaveTblOnDelete()
    {

        try
        {
            if (lblTypeOfLeave.Text == "Casual Leave")
            {
                SqlCommand cmd = new SqlCommand("USP_UpdateEmpFinalLeaves", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NoOfdays", lblNoOfLeaves.Text);
                cmd.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (lblTypeOfLeave.Text == "Earned Leave")
            {
                SqlCommand cmd1 = new SqlCommand("USP_UpdateEmpFinalLeaves2", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@NoOfdays", lblNoOfLeaves.Text);
                cmd1.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);

                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            else if (lblTypeOfLeave.Text == "Extra Leave")
            {
                SqlCommand cmd1 = new SqlCommand("USP_UpdateEmpFinalLeaves3", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@NoOfdays", lblNoOfLeaves.Text);
                cmd1.Parameters.AddWithValue("@EMP_Id", lblEmpId.Text);

                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaveHistory();
    }
    protected void gvOneHourHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOneHourHistory.PageIndex = e.NewPageIndex;
        BindOneHourHistory();
    }
    protected void gvTicketHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketHistory.PageIndex = e.NewPageIndex;
        BindTicketHistory();
    }
    protected void gvLeaveHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gvLeaveHistory.PageIndex=  e.NewPageIndex;
      BindLeaveHistory();
    }
    decimal LeaveCount = 0;
    protected void gvLeaveHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
           
            //e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            if(e.Row.Cells[15].Text =="Rejected")
            {
                e.Row.Cells[0].Text = "";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LeaveCount += Convert.ToDecimal(e.Row.Cells[11].Text);
                //InwardQty = InwardQty + int.Parse(e.Row.Cells[8].Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[11].Text = lblTotalLeaveCount.Text = LeaveCount.ToString();
            }
            lblTotalLeaveCount.Text = LeaveCount.ToString();
        }
    }
    protected void btnExprot_Click(object sender, EventArgs e)
    {
        if (gvLeaveHistory.Rows.Count > 0)
        {
            #region Seleted data export to excel
            bool isselected = false;
            foreach (GridViewRow gvrow in gvLeaveHistory.Rows)
            {
                CheckBox chck = gvrow.FindControl("Chk") as CheckBox;
                if (chck != null && chck.Checked)
                {
                    isselected = true;
                    break;
                }
            }
            if (isselected)
            {
                GridView grdxport = gvLeaveHistory;
                grdxport.AllowPaging = false;

                grdxport.Columns[0].Visible = false;
                foreach (GridViewRow gvrow in gvLeaveHistory.Rows)
                {
                    grdxport.Rows[gvrow.RowIndex].Visible = false;
                    CheckBox chck = gvrow.FindControl("Chk") as CheckBox;
                    if (chck != null && chck.Checked)
                    {
                        grdxport.Rows[gvrow.RowIndex].Visible = true;
                    }
                }

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-excel";


                StringWriter swr = new StringWriter();

                HtmlTextWriter htmlwtr = new HtmlTextWriter(swr);
                grdxport.RenderControl(htmlwtr);
                Response.Output.Write(swr.ToString());
                //    Response.Flush();
                Response.End();

            }
            #endregion
            #region Old Exporting Code
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=IndentRecordsReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvLeaveHistory.AllowPaging = false;
                //BindGrid_All();
                // BindGridview();
                gvLeaveHistory.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvLeaveHistory.HeaderRow.Cells)
                {
                    cell.BackColor = gvLeaveHistory.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvLeaveHistory.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvLeaveHistory.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvLeaveHistory.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvLeaveHistory.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            #endregion
        }
    }
    protected void lnkOneHour_Click(object sender, EventArgs e)
    {
        pnlOneHour.Visible = true;
        pnlLeaveHistory.Visible = false;
        pnlTicket.Visible = false;
    }
    protected void lnkTickets_Click(object sender, EventArgs e)
    {
        pnlTicket.Visible = true;
        pnlOneHour.Visible = false;
        pnlLeaveHistory.Visible = false;

    }

    protected void btnTicketSearch_Click(object sender, EventArgs e)
    {
        BindTicketHistory();
    }


    string pagenavigationstr;
    protected void btnRunReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=Leaves&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDate.Text) + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
  
    }

    protected void btnRunReportEmp_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=LeavesEmp&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDate.Text) + "&EmpId="+ddlEmpName .SelectedItem .Value +"";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
}