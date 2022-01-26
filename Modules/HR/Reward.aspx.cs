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
using System.IO;

public partial class Modules_HR_Reward : basePage
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
        //txtReward.Text = HR.Reward.Reward_AutoGenCode();
        btnSave.Text = "Save";
        tblRewardDetails.Visible = true;
        tblemp.Visible = true;


    }
    #endregion

    #region Edit
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvRewardDetails.SelectedIndex > -1)
        {
            tblemp.Visible = true;
            tblRewardDetails.Visible = true;
            ddlCompanyid.SelectedValue = gvRewardDetails.SelectedRow.Cells[10].Text;
            ddlCompanyid_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = gvRewardDetails.SelectedRow.Cells[2].Text;
            txtReward.Text = gvRewardDetails.SelectedRow.Cells[1].Text;
            txtDate.Text = gvRewardDetails.SelectedRow.Cells[8].Text;
            txtreason.Text = gvRewardDetails.SelectedRow.Cells[9].Text;

            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast one Record");
        }
    }
    #endregion

    #region RewardUpdate
    private void RewardUpdate()
    {
        try
        {
            HR.Reward objMaster = new HR.Reward();
            objMaster.RewardId = gvRewardDetails.SelectedRow.Cells[0].Text;
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.RewardName = txtReward.Text;
            objMaster.RewardDate = General.toMMDDYYYY(txtDate.Text);
            objMaster.RewardReason = txtreason.Text;
            if (objMaster.Reward_Update() == "Data Updated Successfully")
            {
                HR.EmployeeMaster obj = new HR.EmployeeMaster();

                // Saving Uploaded Documents
                string CD = DateTime.Now.ToString("MM/dd/yyyy");

                if (docSubmitted.HasFiles)
                {
                    #region Employee Documents

                    string empDoc = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments"))
                    {
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.DocSubmit1 = "Reward";
                            obj.EmpID = ddlEmployee .SelectedItem .Value ;
                            obj.DOcRewardId = objMaster.RewardId;

                            obj.EmpDocDetails_Save();
                        }
                    }

                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments");
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.DocSubmit1 = "Reward";
                            obj.EmpID = ddlEmployee.SelectedItem.Value;
                            obj.DOcRewardId = objMaster.RewardId;

                            obj.EmpDocDetails_Save();
                        }

                    }
                    #endregion
                }

                MessageBox.Show(this, "Updated Sucessfully");

            }
            tblemp.Visible = false;
            tblRewardDetails.Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvRewardDetails.DataBind();

            HR.Dispose();
        }
    }
    #endregion

    #region Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvRewardDetails.SelectedIndex > -1)
        {
            try
            {
                HR.Reward objMaster = new HR.Reward();
                objMaster.RewardId = gvRewardDetails.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objMaster.Reward_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblemp.Visible = false;
                tblRewardDetails.Visible = false;
                gvRewardDetails.DataBind();
                gvRewardDetails.SelectedIndex = -1;

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
            RewardSave();
            tblemp.Visible = false;
            tblRewardDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            RewardUpdate();
            tblemp.Visible = false;
            tblRewardDetails.Visible = false;
        }
    }
    #endregion

    #region RewardSave
    private void RewardSave()
    {
        try
        {
            HR.Reward objMaster = new HR.Reward();
            objMaster.EmpId = ddlEmployee.SelectedItem.Value;
            objMaster.RewardName = txtReward.Text;
            objMaster.RewardDate = General.toMMDDYYYY(txtDate.Text);
            //objMaster.RewardDate = txtDate.Text;
            objMaster.RewardReason = Server.UrlDecode(txtreason.Text);
            if (objMaster.Reward_Save() == "Data Saved Successfully")
            {
                HR.EmployeeMaster obj = new HR.EmployeeMaster();

                // Saving Uploaded Documents
                string CD = DateTime.Now.ToString("MM/dd/yyyy");

                if (docSubmitted.HasFiles)
                {
                    #region Employee Documents

                    string empDoc = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments"))
                    {
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.DocSubmit1 = "Reward";
                            obj.EmpID  = lblEmpIdHidden.Text;
                            obj.DOcRewardId = objMaster.RewardId;
                            obj.EmpDocDetails_Save();
                        }
                    }

                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments");
                        foreach (HttpPostedFile uploadedFile in docSubmitted.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/EmployeeDocuments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            empDoc = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.DocSubmit = empDoc;
                            obj.DateSubmitted = CD;
                            obj.DocSubmit1 ="Reward";
                            obj.EmpID  = lblEmpIdHidden.Text;
                            obj.DOcRewardId = objMaster.RewardId;

                            obj.EmpDocDetails_Save();
                        }

                    }
                    #endregion
                }
            }
            
            MessageBox.Show(this, "Data Saved Sucessfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvRewardDetails.DataBind();
            HR.ClearControls(this);
            HR.Dispose();
        }
    }
    #endregion
    protected void Upload_DataBinding(object sender, EventArgs e)
    {
        //string path=Upload.PostedFiles.
    }
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
        tblRewardDetails.Visible = false;
    }
    #endregion

    #region LinkButtion
    protected void lbtnEmpName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnEmpName;
        lbtnEmpName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEmpName.Parent.Parent;
        gvRewardDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");



        if (gvRewardDetails.SelectedIndex > -1)
        {
            tblemp.Visible = true;
            tblRewardDetails.Visible = true;
            ddlCompanyid.SelectedValue = gvRewardDetails.SelectedRow.Cells[10].Text;
            ddlCompanyid_SelectedIndexChanged(sender, e);
            ddlEmployee.SelectedValue = gvRewardDetails.SelectedRow.Cells[2].Text;
            ddlEmployee_SelectedIndexChanged(sender, e);
            txtReward.Text = gvRewardDetails.SelectedRow.Cells[1].Text;
            txtDate.Text = gvRewardDetails.SelectedRow.Cells[8].Text;
            txtreason.Text = gvRewardDetails.SelectedRow.Cells[9].Text;

            btnSave.Text = "Update";

        }


    }
    #endregion

    #region Rowdatabound
    protected void gvRewardDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        txtReward.Text = ddlEmployee.SelectedItem.Text + DateTime.Now.ToString("dd/MM/yyyy");


    }
    #endregion

    #region Search
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {

        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvRewardDetails.DataBind();

    }
    #endregion


    #region
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvRewardDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/MastersReportViewer.aspx?type=Reward&Mid=" + gvRewardDetails.SelectedRow.Cells[0].Text + "";
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