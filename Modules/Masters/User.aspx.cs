using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;

public partial class Modules_Masters_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserNameFill();
           // SM.DDLBindWithSelect(ddlUsersType, "select userTypeId,userTypeName from usertype_tbl");
            Authentication.UserDetails.UserTypesFill(ddlUsersType);
        }
    }


    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAddUserDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvAddUserDetails.DataBind();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        General.ClearControls(this);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblUserDetails.Visible = false;
        General.ClearControls(this);
    }

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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        tblUserDetails.Visible = true;
        UserNameFill();
        ddlUserName.Enabled = true;
        General.ClearControls(this);
        btnSave.Text = "Save";

    }
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

    #region Save Function
    private void UserSave()
    {
        try
        {
           
            Authentication.UserDetails objMaster = new Authentication.UserDetails();
            objMaster.UserName = txtUserId.Text;
            objMaster.Password = txtPassword.Text;
            objMaster.AssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objMaster.ExpiryDate = Yantra.Classes.General.toMMDDYYYY(txtExpiryDate.Text);
            objMaster.UserType = ddlUsertype.SelectedValue;
            objMaster.EmpId = ddlUserName.SelectedItem.Value;
            objMaster.UserTypes = ddlUsersType.SelectedItem.Value;
            objMaster.UserDetails_Save();
            
                MessageBox.Show(this, "Data Saved Successfully");
               
            }
        catch (Exception ex)
        {
            
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
            
            Authentication.UserDetails objAuthentication = new Authentication.UserDetails();
            objAuthentication.UserId = gvAddUserDetails.SelectedRow.Cells[0].Text;
            objAuthentication.UserName = txtUserId.Text;
            objAuthentication.Password = txtPassword.Text;
            objAuthentication.AssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objAuthentication.ExpiryDate = Yantra.Classes.General.toMMDDYYYY(txtExpiryDate.Text);
            objAuthentication.UserType = ddlUsertype.SelectedValue;
            objAuthentication.EmpId = ddlUserName.SelectedItem.Value;
            objAuthentication.UserTypes = ddlUsersType.SelectedItem.Value;
            objAuthentication.UserDetails_Update();
                MessageBox.Show(this, "Data Updated Successfully");
               
          
        }
        catch (Exception ex)
        {
           
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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ddlUserName.Enabled = false;

        if (gvAddUserDetails.SelectedIndex > -1)
        {
            tblUserDetails.Visible = true;
            txtUserId.Text = gvAddUserDetails.SelectedRow.Cells[1].Text;
            txtAssignDate.Text = gvAddUserDetails.SelectedRow.Cells[6].Text;
            txtExpiryDate.Text = gvAddUserDetails.SelectedRow.Cells[7].Text;
            txtCompanyName.Text = gvAddUserDetails.SelectedRow.Cells[4].Text;
            txtDesignation.Text = gvAddUserDetails.SelectedRow.Cells[3].Text;
            txtDepartment.Text = gvAddUserDetails.SelectedRow.Cells[5].Text;
            ddlUsertype.SelectedValue = gvAddUserDetails.SelectedRow.Cells[9].Text;
            //ddlUserName.SelectedValue = gvAddUserDetails.SelectedRow.Cells[8].Text;
            //ddlUserName_SelectedIndexChanged(sender, e);
            Authentication.UserDetails obj = new Authentication.UserDetails();
           if(obj.UserDetails_Select(gvAddUserDetails.SelectedRow.Cells[8].Text) > 0)
           {
               ddlUsersType.SelectedValue = obj.usertypeid;
           }


            btnSave.Text = "Update"; 
            
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvAddUserDetails.SelectedIndex > -1)
            {
                Authentication.UserDetails obj = new Authentication.UserDetails();
                MessageBox.Show(this, obj.UserName_UserPermissions_Delete(int.Parse(gvAddUserDetails.SelectedRow.Cells[0].Text)));
                gvAddUserDetails.DataBind();
            }
            else
            {
                MessageBox.Show(this, "You Must Select Any Record From List");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
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



    protected void gvAddUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = Convert.ToString(gvAddUserDetails.PageIndex * gvAddUserDetails.PageSize + slnoforMaingrid);
            //slnoforMaingrid++;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
    }
    protected void btnCurrentDayTasksGo_Click(object sender, EventArgs e)
    {
        if (ddlCurrentDayTaskSearchBy.SelectedItem.Value == "Company Name")
        {
            lblSearchItemHidden.Text = ddlCurrentDayTaskSearchBy.SelectedItem.Value;
            lblSearchValueHidden.Text = ddlCompanySearch.SelectedItem.Text;

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
        }
        gvAddUserDetails.DataBind();
        HR.ClearControls(this);
    }
}
 
