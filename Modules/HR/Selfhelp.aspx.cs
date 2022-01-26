using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using vllib;
using YantraDAL;
using Yantra.MessageBox;

public partial class Modules_HR_Selfhelp : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

        HR.EmployeeMaster objmas = new HR.EmployeeMaster();
        objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
        lblEmpId.Text = lblSOIdHidden.Text = objmas.EmpID;
        string firstName = (string)(Session["UserName"]);
        lblUserName.Text = firstName;
        Binddlt();

    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "87");

        btnEdit.Enabled = up.Update;
        btnSave.Enabled = up.add;

    }

    private void Binddlt()
    {
        SqlCommand cmd = new SqlCommand("USP_HR_EMPLOYEE_PERSONAL_DETAILS", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMP_ID", lblEmpId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        dlt.DataSource = dt;
        dlt.DataBind();
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = true;
        Label a = dlt.FindControl("lblFName") as Label;
        txtFirstName.Text = a.Text;
        Label b = dlt.FindControl("lblLName") as Label;
        txtLastName.Text = b.Text;
        Label c = dlt.FindControl("lblFatherName") as Label;
        txtFatherName.Text = c.Text;
        Label d = dlt.FindControl("lblDOB") as Label;
        txtDOB.Text = d.Text;
        Label E = dlt.FindControl("lblMobileNo") as Label;
        txtMobileNo.Text = E.Text;
        Label f = dlt.FindControl("lblAddress") as Label;
        txtAddress.Text = f.Text;
        Label g = dlt.FindControl("lblEmail") as Label;
        txtEmail.Text = g.Text;
    
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("USP_Emp_PersonalDetailsSave", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EMP_ID", lblEmpId.Text);
        cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtFirstName.Text);
        cmd.Parameters.AddWithValue("@EMP_LAST_NAME", txtLastName.Text);
        cmd.Parameters.AddWithValue("@FATHER_NAME", txtFatherName.Text);
        cmd.Parameters.AddWithValue("@EMP_DOB", txtDOB.Text);
        cmd.Parameters.AddWithValue("@EMP_MOBILE", txtMobileNo.Text);
        cmd.Parameters.AddWithValue("@EMP_ADDRESS", txtAddress.Text);
        cmd.Parameters.AddWithValue("@EMP_EMAIL", txtEmail.Text);
        cmd.Parameters.AddWithValue("@Status", "Pending");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        tblDetails.Visible = false;

    }
    protected void lbtnAttachedFiles_Click(object sender, EventArgs e)
    {
        HR.EmployeeMaster objEM = new HR.EmployeeMaster();
        //objEM.DocSubmit  = lbtnAttachedFiles.Text;
    }
    #region Link Button File Opener Click
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbtnFileOpener;
            lbtnFileOpener = (LinkButton)sender;
            Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
            DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
            string command = "SELECT Document_Submitted FROM [Emp_Documents_Submitted] WHERE EMP_ID=" + lblEmpId.Text + " AND Document_Submitted='" + lbtnFileOpener.Text + "'";
            dbcon.Open();
            string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
            string path = "/Content/EmployeeDocuments/" + filename;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Problem In Loading the File");
        }

    }
    protected void lbtnFileOpener2_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbtnFileOpener2;
            lbtnFileOpener2 = (LinkButton)sender;
            Repeater gvRow = (Repeater)lbtnFileOpener2.Parent.Parent;
            DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
            string command = "SELECT Document_Submitted FROM [Emp_Documents_Submitted] WHERE EMP_ID=" + lblEmpId.Text + " AND Document_Submitted='" + lbtnFileOpener2.Text + "'";
            dbcon.Open();
            string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
            string path = "/Content/EmployeeDocuments/" + filename;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Problem In Loading the File");
        }

    }
    #endregion
    protected void btnApplications_Click(object sender, EventArgs e)
    {
        if (PanelApps.Visible == false)
        {
            PanelApps.Visible = true;
        }
        else
        {
            PanelApps.Visible = false;
        }
    }
}