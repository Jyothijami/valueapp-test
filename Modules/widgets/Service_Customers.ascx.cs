﻿using System;
using System.Web.UI.WebControls;
using vllib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class Modules_widgets_Service_Customers : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblEmpIdHidden.Text = lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        }
    }
}