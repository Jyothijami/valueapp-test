using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatumDAL;
using Yantra.MessageBox;
using Yantra.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using vllib;

public partial class Modules_Home_TimeSheet : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.SetActiveView(View1);
            //txtcirNo.Text = HR.Circular.Cir_AutoGenCode();
            CheckBoxList1.DataBind();
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            txtDate.Text = DateTime.Now.ToString();
            FillCompany();
            FillDept();
        }
        
    }
    #region DDLCompany
    protected void ddlCompanyid_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeCompany(ddlEmployee, ddlCompanyid.SelectedItem.Value);
    }
    #endregion
    #region FillCompany
    private void FillCompany()
    {
        try
        {

            HR.Company_Select(ddlCompanyid);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    #endregion
    #region DDL Employee
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        HR.EmployeeMaster obj = new HR.EmployeeMaster();
        obj.EmployeeMaster_Select(ddlEmployee.SelectedItem.Value);
        txtMobileno.Text = obj.EmpMobile;
        txtDesignation.Text = obj.DesgName12;
    }
    #endregion
    private void FillDept()
    {
        try
        {
            HR.Dept_Select(ddlDept);

        }
        catch
        {
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        HR.EmployeeDept(ddlEmployee, ddlDept.SelectedItem.Value);
    }
    

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string st = "";
        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected)
            {
                if (st != "")
                {
                    st += ",";
                }

                st = st + li.Value;
            }
        }

        set_EmployeesList(st);
        foreach (ListItem li in ListBox1.Items)
        {

            li.Selected = true;
        }
    }
    protected void set_EmployeesList(string deptids)
    {

        ListBox1.Items.Clear();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = default(SqlDataReader);


        con.Close();
        cmd = new SqlCommand(@"SELECT     YANTRA_EMPLOYEE_MAST.EMP_ID, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME + ' ' + YANTRA_EMPLOYEE_MAST.EMP_MIDDLE_NAME as Fullname
FROM         YANTRA_EMPLOYEE_MAST INNER JOIN
                      YANTRA_EMPLOYEE_DET ON YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID 
where YANTRA_EMPLOYEE_MAST.STATUS = 1 and DEPT_ID in (" + deptids + ")", con);

        con.Open();

        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (dr.Read())
        {
            ListItem li1 = new ListItem();
            li1.Text = dr["Fullname"].ToString();
            li1.Value = dr["EMP_ID"].ToString();

            ListBox1.Items.Add(li1);

        }

        con.Close();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            CircularSave();
        }
    }
    #region CircularSave
    private void CircularSave()
    {
        try
        {

            foreach (ListItem li in ListBox1.Items)
            {
                string empid = li.Value;

                if (li.Selected)
                {
                    HR.EmployeeMaster em = new HR.EmployeeMaster(empid);


                    HR.Circular objMaster = new HR.Circular();
                    objMaster.circular = txtcirNo.Text;
                    objMaster.CmpId = ddlActivity.SelectedItem.Value;
                    //objMaster.DeptId = em.DeptID;
                    objMaster.DeptId = lblEmpIdHidden.Text;
                    objMaster.empid = empid;
                    objMaster.issuedate = txtDate.Text;
                    objMaster.descrption = txtdescription.Text;
                    objMaster.readmsg = "0";
                    MessageBox.Show(this, objMaster.Circular_Save());


                }
            }

            //HR.Circular objMaster = new HR.Circular();
            //if (ddlDept.SelectedValue == "0")
            //{
            //    objMaster.circular = txtcirNo.Text;
            //    objMaster.CmpId = ddlCompanyid.SelectedValue;
            //    objMaster.DeptId = ddlDept.SelectedValue;
            //    objMaster.empid = "0";
            //    objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
            //    objMaster.descrption = txtdescription.Text;

            //    MessageBox.Show(this, objMaster.Circular_Save());
            //}
            //else
            //{

            //    objMaster.circular = txtcirNo.Text;
            //    objMaster.CmpId = ddlCompanyid.SelectedValue;
            //    objMaster.DeptId = ddlDept.SelectedValue;
            //    objMaster.empid = ddlEmployee.SelectedValue;
            //    objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
            //    objMaster.descrption = txtdescription.Text;
            //    MessageBox.Show(this, objMaster.Circular_Save());

            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            //gvCircular.DataBind();
            HR.ClearControls(this);
        }
    }
    #endregion

    #region Refresh

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
    }
    #endregion
}