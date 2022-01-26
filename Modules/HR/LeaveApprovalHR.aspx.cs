using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatumDAL;
using Yantra.MessageBox;
public partial class Modules_HR_LeaveApprovalHR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvEnrollmentDtls.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvEnrollmentDtls.Rows)
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
        LeaveApprove();
        gvEnrollmentDtls.DataBind();
    }

    private void LeaveApprove()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");
                    HR.EmpLeave obj = new HR.EmpLeave();
                    obj.Status1 = "Approved";
                    obj.Status2 = "Approved";
                    obj.Status3 = "Pending";
                    obj.LeaveId = leaveId.Text;
                    MessageBox.Show(this, obj.LeaveDetailsApprove_Update());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
    }
    protected void btnreject_Click(object sender, EventArgs e)
    {
        LeaveReject();
        gvEnrollmentDtls.DataBind();

    }

    private void LeaveReject()
    {
        foreach (GridViewRow gvr in gvEnrollmentDtls.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label leaveId = (Label)gvr.FindControl("lblLeaveId");
                    HR.EmpLeave obj = new HR.EmpLeave();
                    obj.Status1 = "Rejected";
                    obj.Status2 = "Rejected";
                    obj.Status3 = "Rejected";
                    obj.LeaveId = leaveId.Text;
                    MessageBox.Show(this, obj.LeaveDetailsApprove_Update());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
    }
}