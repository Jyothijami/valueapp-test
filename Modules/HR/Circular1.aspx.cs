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



public partial class Modules_HR_Circular1 : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();
            CheckBoxList1.DataBind();

            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();

            txtDate.Text = System.DateTime.Now.Date.ToString();
            FillCompany();
            FillDept();
        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "62");
        //btnNew.Enabled = up.add;
        //btnEdit.Enabled = up.Update;
        //btnDelete.Enabled = up.Delete;
        //btnSave.Enabled = up.add;

        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;


    }



    #region New
    protected void btnNew_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
        txtcirNo.Text = HR.Circular.Cir_AutoGenCode();
        //btnSave.Text = "Save";
        tblemp.Visible = true;
        tblcircular.Visible = true;

        ListBox1.Items.Clear();
        CheckBoxList1.ClearSelection();

    }
    #endregion

    #region Edit
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCircular.SelectedIndex > -1)
        {
            tblemp.Visible = true;
            tblcircular.Visible = true;
            ddlCompanyid.SelectedValue = gvCircular.SelectedRow.Cells[4].Text;
            ddlCompanyid_SelectedIndexChanged(sender, e);
            ddlDept.SelectedValue = gvCircular.SelectedRow.Cells[2].Text;
            ddlEmployee_SelectedIndexChanged(sender, e);
            txtcirNo.Text = gvCircular.SelectedRow.Cells[8].Text;
            txtDate.Text = gvCircular.SelectedRow.Cells[6].Text;
            txtdescription.Text = gvCircular.SelectedRow.Cells[7].Text;

            //btnSave.Text = "Update";
            //btnSave.Visible = true;
        }

        else
        {
            MessageBox.Show(this, "Please select atleast one Record");
        }
    }
    #endregion

    #region Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (gvCircular.SelectedIndex > -1)
        {
            try
            {
                HR.Circular objMaster = new HR.Circular();
                objMaster.CirId = gvCircular.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objMaster.Circular_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblemp.Visible = false;
                tblcircular.Visible = false;
                gvCircular.DataBind();
                gvCircular.SelectedIndex = -1;

                HR.ClearControls(this);

            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (btnSave.Text == "Save")
        //{
        //    CircularSave();
        //    tblemp.Visible = false;
        //    tblcircular.Visible = false;
        //}
        //else if (btnSave.Text == "Update")
        //{
        //    CircularUpdate();
        //    tblemp.Visible = false;
        //    tblcircular.Visible = false;
        //}


    }
    #endregion

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
                    objMaster.CmpId = em.Company_ID;
                    objMaster.DeptId = em.DeptID;
                    objMaster.empid = empid;
                    objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
                    objMaster.descrption = txtdescription.Text;
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

            gvCircular.DataBind();
            HR.ClearControls(this);
        }
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

    #region CircularUpdate
    private void CircularUpdate()
    {
        try
        {
            HR.Circular objMaster = new HR.Circular();
            if (ddlDept.SelectedValue == "0")
            {
                objMaster.CirId = gvCircular.SelectedRow.Cells[0].Text;
                objMaster.circular = txtcirNo.Text;
                objMaster.CmpId = ddlCompanyid.SelectedValue;
                objMaster.DeptId = ddlDept.SelectedValue;
                objMaster.empid = "0";
                objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
                objMaster.descrption = txtdescription.Text;
                MessageBox.Show(this, objMaster.Circular_Update());
            }
            else
            {
                objMaster.CirId = gvCircular.SelectedRow.Cells[0].Text;
                objMaster.circular = txtcirNo.Text;
                objMaster.CmpId = ddlCompanyid.SelectedValue;
                objMaster.DeptId = ddlDept.SelectedValue;
                objMaster.empid = gvCircular.SelectedRow.Cells[9].Text;
                objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
                objMaster.descrption = txtdescription.Text;
                MessageBox.Show(this, objMaster.Circular_Update());

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvCircular.DataBind();
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

    #region Close
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblemp.Visible = false;
        tblcircular.Visible = false;
    }
    #endregion



    #region search
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtSearchText.Text = "";
    }
    #endregion

    #region Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        //lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        //lblSearchValueHidden.Text = txtSearchText.Text;
        gvCircular.DataBind();

    }
    #endregion



    #region RowBound
    protected void gvCircular_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
    }
    #endregion

    #region DDLCompany
    protected void ddlCompanyid_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeCompany(ddlEmployee, ddlCompanyid.SelectedItem.Value);
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

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        HR.EmployeeDept(ddlEmployee, ddlDept.SelectedItem.Value);
    }
    protected void lbtnCirNo_Click(object sender, EventArgs e)
    {
        LinkButton lbtnCirNo;
        lbtnCirNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCirNo.Parent.Parent;
        gvCircular.SelectedIndex = gvRow.RowIndex;
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        if (gvCircular.SelectedIndex > -1)
        {
            tblemp.Visible = true;
            tblcircular.Visible = true;
            ddlCompanyid.SelectedValue = gvCircular.SelectedRow.Cells[4].Text;
            ddlCompanyid_SelectedIndexChanged(sender, e);
            ddlDept.SelectedValue = gvCircular.SelectedRow.Cells[2].Text;
            CheckBoxList1.SelectedValue = gvCircular.SelectedRow.Cells[2].Text;
            CheckBoxList1_SelectedIndexChanged(sender, e);
            ddlEmployee_SelectedIndexChanged(sender, e);
            ListBox1.SelectedValue = gvCircular.SelectedRow.Cells[9].Text;
            txtcirNo.Text = gvCircular.SelectedRow.Cells[8].Text;
            txtDate.Text = General.toDDMMYYYY(gvCircular.SelectedRow.Cells[6].Text);
            txtdescription.Text = gvCircular.SelectedRow.Cells[7].Text;

            //btnSave.Text = "Update";
            //btnSave.Visible = false;
        }
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
where DEPT_ID in (" + deptids + ")", con);

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
    
}
 
