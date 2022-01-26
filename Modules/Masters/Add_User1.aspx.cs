using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using Yantra;

public partial class Modules_Masters_Add_User1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Add New CLick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Authentication.ClearControls(this);
        tblUserDetails.Visible = true;
        ddlUserName.Enabled = true;
        RadioButtonList1.SelectedValue = "Yes";
        RadioButtonList1_SelectedIndexChanged(sender, e);
        RadioButtonList1.Enabled = false;
        rfvddlUserName.Enabled = true;
        rfvPassword.Enabled = true;
        UserNameFill();
        btnSave.Text = "Save";
        chklMaster.ClearSelection();
        chklSM.ClearSelection();
        chklSCM.ClearSelection();
        chklInventory.ClearSelection();
        chklServices.ClearSelection();
        chklHR.ClearSelection();
        chklPermissions.ClearSelection();
        chklFullControl.ClearSelection();

    }
    #endregion

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompanySearch);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }

    }
    #endregion

    #region User Name Fill
    private void UserNameFill()
    {
        try
        {
            Authentication.UserDetails.UserDetails_Select(ddlUserName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Authentication.Dispose();
        }
    }
    #endregion

    #region Privileges_FillFill
    private void PrivilegesFill()
    {
        try
        {
            Authentication.UserDetails.Privileges_Fill(ddlPriveleges);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Authentication.Dispose();
        }
    }
    #endregion

    #region User Name Selected Index
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster objEmp = new HR.EmployeeMaster();
        if (objEmp.UserEmployeeMaster_Select(ddlUserName.SelectedItem.Value) > 0)
        {
            txtUserId.Text = objEmp.EMPUserName;
            txtCompanyName.Text = objEmp.ComapanyName;
            txtDepartment.Text = objEmp.DeptName12;
            txtDesignation.Text = objEmp.DesgName12;
        }
    }
    #endregion

    #region Save Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            UserSave();
            tblUserDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            UserUpdate();
            tblUserDetails.Visible = false;
        }
        gvAddUserDetails.SelectedIndex = -1;
        gvAddUserDetails.DataBind();
    }
    #endregion
    //protected int slnoforMaingrid = 1;
    #region GridView Data Bound
    protected void gvAddUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = Convert.ToString(gvAddUserDetails.PageIndex * gvAddUserDetails.PageSize + slnoforMaingrid);
            //slnoforMaingrid++;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
    }
    #endregion

    #region Lnk Button Click
    protected void lbtnUserName_Click(object sender, EventArgs e)
    {
        tblUserDetails.Visible = false;
        LinkButton lbtnUserName;
        lbtnUserName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnUserName.Parent.Parent;
        gvAddUserDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Edit Button Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ddlUserName.Enabled = false;
        rfvddlUserName.Enabled = false;
        rfvPassword.Enabled = false;

        if (gvAddUserDetails.SelectedIndex > -1)
        {
            tblUserDetails.Visible = true;
            txtUserId.Text = gvAddUserDetails.SelectedRow.Cells[1].Text;
            ddlPriveleges.SelectedItem.Value = gvAddUserDetails.SelectedRow.Cells[6].Text;
            txtAssignDate.Text = gvAddUserDetails.SelectedRow.Cells[7].Text;
            txtExpiryDate.Text = gvAddUserDetails.SelectedRow.Cells[8].Text;
            txtCompanyName.Text = gvAddUserDetails.SelectedRow.Cells[4].Text;
            txtDesignation.Text = gvAddUserDetails.SelectedRow.Cells[3].Text;
            txtDepartment.Text = gvAddUserDetails.SelectedRow.Cells[5].Text;
            ddlUsertype.SelectedValue = gvAddUserDetails.SelectedRow.Cells[10].Text;
            btnSave.Text = "Update";
            UserPrivilegesFill();
            chklFullControl_SelectedIndexChanged(sender, e);
            RadioButtonList1.SelectedValue = "No";
            RadioButtonList1_SelectedIndexChanged(sender, e);
            RadioButtonList1.Enabled = true;
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Save Function
    private void UserSave()
    {
        try
        {
            Authentication.BeginTransaction();
            Authentication.UserDetails objMaster = new Authentication.UserDetails();
            objMaster.UserName = txtUserId.Text;
            objMaster.Password = txtPassword.Text;
            objMaster.AssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objMaster.ExpiryDate = Yantra.Classes.General.toMMDDYYYY(txtExpiryDate.Text);
            objMaster.PrivelegeId = ddlPriveleges.SelectedItem.Value;
            objMaster.UserType = ddlUsertype.SelectedValue;
            if (objMaster.UserDetails_Save() == "Data Saved Successfully")
            {
                for (int i = 0; i < chklMaster.Items.Count; i++)
                {
                    if (chklMaster.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklMaster.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklSM.Items.Count; i++)
                {
                    if (chklSM.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklSM.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklSCM.Items.Count; i++)
                {
                    if (chklSCM.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklSCM.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }

                for (int i = 0; i < chklServices.Items.Count; i++)
                {
                    if (chklServices.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklServices.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklHR.Items.Count; i++)
                {
                    if (chklHR.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklHR.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklInventory.Items.Count; i++)
                {
                    if (chklInventory.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklInventory.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklInventorys.Items.Count; i++)
                {
                    if (chklInventorys.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklInventorys.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklFinance.Items.Count; i++)
                {
                    if (chklFinance.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chklFinance.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                for (int i = 0; i < chkWarehouse.Items.Count; i++)
                {
                    if (chkWarehouse.Items[i].Selected == true)
                    {
                        objMaster.PrivelegeId = chkWarehouse.Items[i].Value;
                        objMaster.Privelges_Save();
                    }
                }
                Authentication.CommitTransaction();
                MessageBox.Show(this, "Data Saved Successfully");
                chklMaster.ClearSelection();
                chklSM.ClearSelection();
                chklSCM.ClearSelection();
                chklInventorys.ClearSelection();
                chklFinance.ClearSelection();
                chklInventory.ClearSelection();
                chklServices.ClearSelection();
                chklHR.ClearSelection();
                chkWarehouse.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            Authentication.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvAddUserDetails.DataBind();
            Authentication.ClearControls(this);
            Authentication.Dispose();
        }
    }
    #endregion

    #region Update Function
    private void UserUpdate()
    {
        try
        {
            Authentication.BeginTransaction();
            Authentication.UserDetails objAuthentication = new Authentication.UserDetails();
            objAuthentication.UserId = gvAddUserDetails.SelectedRow.Cells[0].Text;
            objAuthentication.UserName = txtUserId.Text;
            objAuthentication.Password = txtPassword.Text;
            objAuthentication.AssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objAuthentication.ExpiryDate = Yantra.Classes.General.toMMDDYYYY(txtExpiryDate.Text);
            objAuthentication.UserType = ddlUsertype.SelectedValue;
            if (objAuthentication.UserDetails_Update() == "Data Updated Successfully")
            {
                objAuthentication.Privelges_Delete(gvAddUserDetails.SelectedRow.Cells[0].Text);

                for (int i = 0; i < chklMaster.Items.Count; i++)
                {
                    if (chklMaster.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklMaster.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklSM.Items.Count; i++)
                {
                    if (chklSM.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklSM.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklSCM.Items.Count; i++)
                {
                    if (chklSCM.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklSCM.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklInventory.Items.Count; i++)
                {
                    if (chklInventory.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklInventory.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklServices.Items.Count; i++)
                {
                    if (chklServices.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklServices.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklHR.Items.Count; i++)
                {
                    if (chklHR.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklHR.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chkWarehouse.Items.Count; i++)
                {
                    if (chkWarehouse.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chkWarehouse.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }
                for (int i = 0; i < chklPermissions.Items.Count; i++)
                {
                    if (chklPermissions.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklPermissions.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }

                for (int i = 0; i < chklInventorys.Items.Count; i++)
                {
                    if (chklInventorys.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklInventorys.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }

                for (int i = 0; i < chklFinance.Items.Count; i++)
                {
                    if (chklFinance.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklFinance.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }

                for (int i = 0; i < chklFullControl.Items.Count; i++)
                {
                    if (chklFullControl.Items[i].Selected == true)
                    {
                        objAuthentication.PrivelegeId = chklFullControl.Items[i].Value;
                        objAuthentication.Privelges_Save();
                    }
                }

                Authentication.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
                chklMaster.ClearSelection();
                chklSM.ClearSelection();
                chklSCM.ClearSelection();
                chklInventory.ClearSelection();
                chklServices.ClearSelection();
                chklHR.ClearSelection();
                chklFinance.ClearSelection();
                chklInventorys.ClearSelection();
                chkWarehouse.ClearSelection();
            }
            else
            {
                MessageBox.Show(this, "User Name Already Exists.");
            }
        }
        catch (Exception ex)
        {
            Authentication.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvAddUserDetails.DataBind();
            Authentication.ClearControls(this);
            Authentication.Dispose();
        }
    }
    #endregion

    private void SelectedPrivilegesSave(CheckBoxList chkl)
    {

    }

    private void UserPrivilegesFill()
    {
        Authentication.UserDetails.UserPrivilegesFill(gvAddUserDetails.SelectedRow.Cells[0].Text, hfPrivilegesList);
        hfPrivilegesList.Value = hfPrivilegesList.Value.Replace("#", "");
        string[] PrivilegesList = hfPrivilegesList.Value.Split('|');

        chklMaster.ClearSelection();
        chklSM.ClearSelection();
        chklSCM.ClearSelection();
        chklInventory.ClearSelection();
        chklServices.ClearSelection();
        chklHR.ClearSelection();
        chklPermissions.ClearSelection();
        chklFullControl.ClearSelection();
        chklInventorys.ClearSelection();
        chklFinance.ClearSelection();
        chkWarehouse.ClearSelection();

        chklPermissions.Enabled = true;

        for (int i = 0; i < PrivilegesList.Length; i++)
        {
            string[] MenuName = PrivilegesList[i].Split('_');
            if (MenuName[0] == "Master")
            {
                SelectClear(PrivilegesList[i], chklMaster, true);
            }
            else if (MenuName[0] == "SM")
            {
                SelectClear(PrivilegesList[i], chklSM, true);
            }
            else if (MenuName[0] == "SCM")
            {
                SelectClear(PrivilegesList[i], chklSCM, true);
            }
            else if (MenuName[0] == "Inventory")
            {
                SelectClear(PrivilegesList[i], chklInventorys, true);
            }
            else if (MenuName[0] == "Support")
            {
                SelectClear(PrivilegesList[i], chklServices, true);
            }
            else if (MenuName[0] == "HR")
            {
                SelectClear(PrivilegesList[i], chklHR, true);
            }
            else if (MenuName[0] == "Reports")
            {
                SelectClear(PrivilegesList[i], chklInventory, true);
            }
            else if (MenuName[0] == "Finance")
            {
                SelectClear(PrivilegesList[i], chklFinance, true);
            }
            else if (MenuName[0] == "Warehouse")
            {
                SelectClear(PrivilegesList[i], chkWarehouse, true);
            }
            else if (MenuName[0] == "Permission")
            {
                SelectClear(PrivilegesList[i], chklPermissions, true);
                SelectClear(PrivilegesList[i], chklFullControl, true);
            }
        }
    }

    protected void btnMasterSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklMaster, true);
    }
    protected void btnMasterCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklMaster, false);
    }
    protected void btnSMSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklSM, true);
    }
    protected void btnSMCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklSM, false);
    }
    protected void btnSCMSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklSCM, true);
    }
    protected void btnSCMCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklSCM, false);
    }
    protected void btnInvSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklInventory, true);
    }
    protected void btnInvCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklInventory, false);
    }
    protected void btnSupSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklServices, true);
    }
    protected void btnSupCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklServices, false);
    }
    protected void btnHRSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklHR, true);
    }
    protected void btnHRCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklHR, false);
    }
    private void SelectAllClearAll(CheckBoxList chkl, bool TrueOrFalse)
    {
        for (int i = 0; i < chkl.Items.Count; i++)
        {
            chkl.Items[i].Selected = TrueOrFalse;
        }
    }

    private void SelectClear(string PrivilegeName, CheckBoxList chkl, bool TrueOrFalse)
    {
        for (int i = 0; i < chkl.Items.Count; i++)
        {
            if (PrivilegeName == chkl.Items[i].Value)
            {
                chkl.Items[i].Selected = TrueOrFalse;
            }
        }
    }

    private void PrivilegesCountCheck()
    {
        PrivilegesCountCheck("Master", chklMaster);
        PrivilegesCountCheck("SM", chklSM);
        PrivilegesCountCheck("SCM", chklSCM);
        PrivilegesCountCheck("Support", chklServices);
        PrivilegesCountCheck("HR", chklHR);
        PrivilegesCountCheck("Reports", chklInventory);
        PrivilegesCountCheck("Warehouse", chkWarehouse);
        PrivilegesCountCheck("Permissions", chklPermissions);
        PrivilegesCountCheck("Permissions", chklFullControl);
    }

    private void PrivilegesCountCheck(string MenuGroupName, CheckBoxList chkl)
    {
        int privCountInDB = Authentication.UserDetails.CheckPrivilegesCountInList(MenuGroupName + "_");
        if (privCountInDB != chkl.Items.Count)
        {
            for (int i = 0; i < chkl.Items.Count; i++)
            {
                if (Authentication.UserDetails.CheckPrivilegesInList(chkl.Items[i].Value) == 0)
                {
                    //Authentication.UserDetails.AddPrivilegesInList(chkl.Items[i].Value);
                }
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Authentication.UserDetails objauth = new Authentication.UserDetails();
            if (objauth.UserDetails_Delete(gvAddUserDetails.SelectedRow.Cells[9].Text) > 0)
            {
                MessageBox.Show(this, "User Deleted Successfully");
            }
            else
            {
                MessageBox.Show(this, "User Cannot be Deleted");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvAddUserDetails.DataBind();
            gvAddUserDetails.SelectedIndex = -1;
        }
    }


    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "Yes")
        {
            txtPassword.Visible = true;
            lblPasswordValidator.Visible = rfvPassword.Enabled = true;
        }
        else
        {
            txtPassword.Visible = false;
            txtPassword.Text = "";
            lblPasswordValidator.Visible = rfvPassword.Enabled = false;
        }
    }


    protected void chklFullControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chklFullControl.SelectedItem != null)
        {
            if (chklFullControl.SelectedItem.Text == "Full Control")
            {
                chklPermissions.ClearSelection();
                chklPermissions.Enabled = false;
            }
        }
        else
        {
            chklPermissions.Enabled = true;
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblUserDetails.Visible = false;
    }
    protected void txtUserId_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtAssignDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnCurrentDayTasksGo_Click(object sender, EventArgs e)
    {
        if (ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Company Name")
        {
            lblSearchItemHidden.Text = ddlCurrentDayTaskSearchBy.SelectedItem.Value;
            lblSearchValueHidden.Text = ddlCompanySearch.SelectedItem.Text;
            //lblSearchValueHidden1.Text = "0";

        }
        else if (ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Assign Date" || ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Expiry Date")
        {
            lblSearchItemHidden.Text = ddlCurrentDayTaskSearchBy.SelectedItem.Value;
            lblSearchValueHidden1.Text = txtDateAssign.Text;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        else
        {

            lblSearchItemHidden.Text = ddlCurrentDayTaskSearchBy.SelectedItem.Value;
            lblSearchValueHidden.Text = txtSearchText.Text;
            //lblSearchValueHidden1.Text = "0";
        }
        gvAddUserDetails.DataBind();
        HR.ClearControls(this);
    }
    protected void ddlPriveleges_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCurrentDayTaskSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrentDayTaskSearchBy.SelectedItem.Value == "User Name" || ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Designation" || ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Department")
        {
            txtDateAssign.Visible = false;
            txtSearchText.Visible = true;
            imgAssignFrom.Visible = false;
            ddlCompanySearch.Visible = false;
            imgAssignTo.Visible = false;

        }
        else if (ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Company Name")
        {
            CompanyName_Fill();
            ddlCompanySearch.Visible = true;
            txtSearchText.Visible = false;
            txtDateAssign.Visible = false;
            imgAssignFrom.Visible = false;
            imgAssignTo.Visible = false;
        }

        else if (ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Assign Date" || ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Expiry Date")
        {
            txtDateAssign.Visible = true;
            txtSearchText.Visible = true;
            ddlCompanySearch.Visible = false;
            imgAssignFrom.Visible = true;
            CalendarExtender2.TargetControlID = "txtSearchText";
            imgAssignTo.Visible = true;
        }


    }
    protected void sdsUserDetailsFill_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void btnInventorySAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklInventorys, true);
    }
    protected void btnInventoryCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklInventorys, false);
    }


    protected void btnFinanceSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklFinance, true);
    }
    protected void btnFinanceCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chklFinance, false);
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAddUserDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvAddUserDetails.DataBind();
    }

    protected void btnWarSAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chkWarehouse, true);

    }
    protected void btnWarCAll_Click(object sender, EventArgs e)
    {
        SelectAllClearAll(chkWarehouse, false);

    }
}
 
