using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatumDAL;
using Yantra.MessageBox;
using Yantra.Classes;
using vllib;


public partial class HR_EmpMemo : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

            txtDate.Text = System.DateTime.Now.Date.ToString();
            FillCompany();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "61");
        btnNew.Enabled = up.add;
        //btnEdit.Enabled = up.Update;
        //btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        btnPrint.Enabled = up.Print;
        //btnClose.Enabled = up.Close;


    }


    #region New
    protected void btnNew_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
        txtMemo.Text = HR.Memo.Memo_AutoGenCode();
        btnSave.Text = "Save";
        tblMemoDetails.Visible = true;
        tblemp.Visible = true;


    }
    #endregion

    #region Edit
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvMemoDetails.SelectedIndex > -1)
        {
            tblemp.Visible = true;
            tblMemoDetails.Visible = true;
            ddlCompanyid.SelectedValue = gvMemoDetails.SelectedRow.Cells[10].Text;
            ddlCompanyid_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = gvMemoDetails.SelectedRow.Cells[2].Text;
            txtMemo.Text = gvMemoDetails.SelectedRow.Cells[1].Text;
            txtDate.Text = gvMemoDetails.SelectedRow.Cells[8].Text;
            txtreason.Text = gvMemoDetails.SelectedRow.Cells[9].Text;

            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast one Record");
        }
    }
    #endregion

    #region MemoUpdate
    private void MemoUpdate()
    {
        try
        {
            HR.Memo objMaster = new HR.Memo();
            objMaster.MemoId = gvMemoDetails.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.MemoName = txtMemo.Text;
            objMaster.MemoDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.MemoReason = txtreason.Text;
            objMaster.Memo_Update();
            MessageBox.Show(this, "Updated Sucessfully");
            tblemp.Visible = false;
            tblMemoDetails.Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvMemoDetails.DataBind();

            HR.Dispose();
        }
    }
    #endregion

    #region Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvMemoDetails.SelectedIndex > -1)
        {
            try
            {
                HR.Memo objMaster = new HR.Memo();
                objMaster.MemoId = gvMemoDetails.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objMaster.Memo_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblemp.Visible = false;
                tblMemoDetails.Visible = false;
                gvMemoDetails.DataBind();
                gvMemoDetails.SelectedIndex = -1;

                HR.ClearControls(this);
                HR.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }

    }
    #endregion

    #region Save Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            MemoSave();
            tblemp.Visible = false;
            tblMemoDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            MemoUpdate();
            tblemp.Visible = false;
            tblMemoDetails.Visible = false;
        }
    }
    #endregion

    #region MemoSave
    private void MemoSave()
    {
        try
        {
            HR.Memo objMaster = new HR.Memo();
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.MemoName = txtMemo.Text;
            objMaster.MemoDate = General.toMMDDYYYY(txtDate.Text);
            //objMaster.MemoDate = txtDate.Text;
            objMaster.MemoReason = Server.UrlDecode(txtreason.Text);
            objMaster.Memo_Save();
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvMemoDetails.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
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
        tblMemoDetails.Visible = false;
    }
    #endregion

    #region LinkButtion
    protected void lbtnEmpName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnEmpName;
        lbtnEmpName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEmpName.Parent.Parent;
        gvMemoDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");



        if (gvMemoDetails.SelectedIndex > -1)
        {
            tblemp.Visible = true;
            tblMemoDetails.Visible = true;
            ddlCompanyid.SelectedValue = gvMemoDetails.SelectedRow.Cells[10].Text;
            ddlCompanyid_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = gvMemoDetails.SelectedRow.Cells[2].Text;
            ddlEmployee_SelectedIndexChanged(sender, e);
            txtMemo.Text = gvMemoDetails.SelectedRow.Cells[1].Text;
            txtDate.Text = gvMemoDetails.SelectedRow.Cells[8].Text;
            txtreason.Text = gvMemoDetails.SelectedRow.Cells[9].Text;

            btnSave.Text = "Update";

        }


    }
    #endregion

    #region Rowdatabound
    protected void gvMemoDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
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

    #region DDLCompany
    protected void ddlCompanyid_SelectedIndexChanged(object sender, EventArgs e)
    {
        //HR.EmployeeMaster.EmployeeCompany(ddlEmployee, ddlCompanyid.SelectedItem.Value);
        //HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployee);
        HR.EmployeeMaster.Employee_Company(ddlEmployee, ddlCompanyid.SelectedItem.Value);

    }
    #endregion

    #region ddlEmployee
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        HR.EmployeeMaster obj = new HR.EmployeeMaster();
        obj.EmployeeMaster_Select(ddlEmployee.SelectedItem.Value);
        txtMobileno.Text = obj.EmpMobile;
        txtDesignation.Text = obj.DesgName12;
        txtDepartment.Text = obj.DeptName12;


    }
    #endregion

    #region Search
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {

        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvMemoDetails.DataBind();

    }
    #endregion


    #region
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvMemoDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/MastersReportViewer.aspx?type=Memo&Mid=" + gvMemoDetails.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {

    }


    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
       
    }
}