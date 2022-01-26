using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using vllib;
public partial class Modules_HR_Staff_Tour_Advance : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployee);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }
    protected void setControlsVisibility()
    {
        //User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "43");
        //btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDetails();
        clearFields();
        gvStaffTour.DataBind();
    }

    private void clearFields()
    {
        ddlEmployee.SelectedIndex = 0;
        txtDate.Text = txtAmount.Text = txtDateOfArrival.Text = txtDestination.Text = txtNoOfDays.Text = txtComment.Text = "";
    }

    private void SaveDetails()
    {
        Services.ServiceCourierInfo obj = new Services.ServiceCourierInfo();

        obj.Emp_Id = Convert.ToInt32(ddlEmployee.SelectedItem.Value);
        obj.Emp_Name = ddlEmployee.SelectedItem.Text;
        obj.Date = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
        obj.Amount = txtAmount.Text;
        obj.Date_Of_Travel = Yantra.Classes.General.toMMDDYYYY(txtDateOfArrival.Text);
        obj.Destination = txtDestination.Text;
        obj.Tour_Tenure = txtNoOfDays.Text;
        if (txtComment.Text == "")
        {
            txtComment.Text = "-";
        }
        obj.Comment = txtComment.Text;
        MessageBox.Show(this, obj.Staff_Tour_Save());
    }
    protected void lbtnId_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvStaffTour.SelectedIndex = gvRow.RowIndex;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvStaffTour.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=TourAdvance&siid=" + gvStaffTour.SelectedRow.Cells[9].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void gvStaffTour_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Visible = false;
        }
    }
}