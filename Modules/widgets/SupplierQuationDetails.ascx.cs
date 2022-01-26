using System;
using System.Web.UI.WebControls;
using vllib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modules_widgets_SupplierQuationDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }
    
}