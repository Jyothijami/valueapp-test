using System;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_widgets_CustomerInformation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }
    protected void gvCustInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblCustStatus1");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Orange;
            }

        }
    }
}