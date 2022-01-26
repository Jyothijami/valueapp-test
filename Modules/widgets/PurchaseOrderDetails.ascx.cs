﻿using System;
using System.Web.UI.WebControls;
using vllib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modules_widgets_PurchaseOrderDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }
    protected void GridView6_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblPurchaseStatus");

            if (lbl.Text == "New")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }
    }
}