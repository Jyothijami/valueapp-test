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
using Yantra.MessageBox;
using YantraBLL.Modules;
using System.Collections.Generic;
using System.Data.SqlClient;
using vllib;

public partial class Modules_SM_SalesEnquiryDetails : basePage
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            txtEnquiryDueDate.Text = Convert.ToDateTime(DateTime.Now).ToString();
            lblCPID.Text = cp.getPresentCompanySessionValue();
            //lblCPID.Text = cp.getPresentCompanySessionValue();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            txtCurrentDateHidden.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDocCharges.Attributes.Add("onkeyup", "javascript:DocEmdEnableDisable();");
            txtEMDCharges.Attributes.Add("onkeyup", "javascript:DocEmdEnableDisable();");
            txtCurrentDateHidden.Style.Add("display", "none");
            btnRegret.Attributes.Add("onclick", "return confirm('Are you sure you want to Absolute this Enquiry?');");
            btnForApproveHidden.Style.Add("display", "none");
            EnquiryMode_Fill();
            DeliveryType_Fill();
            CustomerMaster_Fill();
            FillBrand();
            EmployeeMaster_Fill();
            gvInterestedProducts.EditIndex = -1;
            //gvInterestedProducts.DataBind();
            tblSalesEnquiry.Visible = true;
            tblAssignTasks.Visible = false;
            btnRefresh.Visible = false;
            TenderDetailsShowHide(false);

            #region Edit
            if (Request.QueryString["Eid"] != null && Request.QueryString["Edit"] == "edit")
            {

                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                if (objSM.SalesEnquiry_Select(Request.QueryString["Eid"].ToString()) > 0)
                {
                    if (objSM.EnqApprovedBy == "0")
                    {
                        btnApprove.Visible = true;
                        btnRegret.Visible = false;
                        btnSave.Visible = true;
                        btnRefresh.Visible = true;
                        btnAssign.Visible = false;
                        if (objSM.EnqStatus == "Closed")
                        {
                            btnAssign.Visible = false;
                        }
                        else
                        {
                            btnAssign.Visible = true;
                        }
                        if (objSM.EnqStatus == "Absolute")
                        {
                            btnAssign.Visible = false;
                            btnSave.Visible = false;
                            btnRefresh.Visible = false;
                            btnRegret.Visible = false;
                            btnApprove.Visible = false;
                        }

                    }
                    else
                    {
                        btnApprove.Visible = false;
                        btnRegret.Visible = false;
                        btnSave.Enabled = false;
                        btnRefresh.Visible = false;
                        btnAssign.Visible = true;
                    }
                }
                btnEdit_Click(sender, e);
            }
            #endregion

            #region CustomerLead
            if (Request.QueryString["Cid"] != null)
            {
                string Cid = Request.QueryString["Cid"].ToString();
                btnDelete.Attributes.Clear();
                //SM.ClearControls(this);
                txtEnquiryNo.Text = SM.SalesEnquiry.SalesEnquiry_AutoGenCode();
                txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tblSalesEnquiry.Visible = true;
                tblAssignTasks.Visible = false;
                btnAssign.Visible = false;
                btnRegret.Visible = false;
                btnRefresh.Visible = true;
                btnSave.Enabled = true;
                btnSave.Text = "Save";
                Image1.ImageUrl = "~/Images/noimage400x300.gif";
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if ((objSMCustomer.CustomerMaster_Select(Cid)) > 0)
                {
                    ddlCustomer.SelectedValue = Cid;
                    ddlCustomer_SelectedIndexChanged(sender, e);
                }
            }
            #endregion

            #region New
            if (Request.QueryString["new"] == "new")
            {
                btnNew_Click(sender, e);
            }
            #endregion

        }
      
    }
    
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "6");
        btnSave.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnApprove.Enabled = up.Approve;
        btnAssignTask.Enabled = up.Update;
    }

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrandStock);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region EnqiryMode Fill
    private void EnquiryMode_Fill()
    {
        try
        {
            Masters.EnquiryMode.EnquiryMode_Select(ddlEnquirySource);
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

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDeliveryType);
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


    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            //SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlCustomer);
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomer);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
           //HR.EmployeeMaster.EmployeeMaster_SelectForEDP(ddlEmpNameForAssign);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlOriginatedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //gvSalesEnquiry.SelectedIndex = -1;
        gvInterestedProducts.DataBind();
        //btnDelete.Attributes.Clear();
       // SM.ClearControls(this);
        txtEnquiryNo.Text = SM.SalesEnquiry.SalesEnquiry_AutoGenCode();
        //txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        tblSalesEnquiry.Visible = true;
        tblAssignTasks.Visible = false;
        btnAssign.Visible = false;
        btnRegret.Visible = false;
        btnRefresh.Visible = true;
        btnApprove.Visible = false;
        btnSave.Enabled = true;
        btnSave.Text = "Save";
        Image1.ImageUrl = "~/Images/noimage400x300.gif";

    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesEnquiry.aspx");
        tblSalesEnquiry.Visible = false;
    }
    #endregion

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                rfvContactPerson.Enabled = true;
                //rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                rfvContactPerson.Enabled = false;
                //rfvUnitName.Enabled = false;
                lblUnitAddress.Text = "Customer Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                    txtUnitAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtPhoneNo.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
            }
            // SM.Dispose();

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            // SM.Dispose();
        }
        finally
        {
        }

    }
    #endregion

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtItemSpecifications.Text == "") { txtItemSpecifications.Text = "-"; }
        if (txtRemarks.Text == "") { txtRemarks.Text = "-"; }
        if (txtBrand.Text == "") { txtBrand.Text = "-"; }
        if (txtItemName.Text == "") { txtItemName.Text = "-"; }
        if (txtRoom.Text == "") { txtRoom.Text = "-"; }
        if (txtItemUOM.Text == "") { txtItemUOM.Text = "-"; }
        if (txtcolorName.Text == "" && ddlcolor.SelectedItem.Value == "")
        {
            ddlcolor.SelectedItem.Value = "0";
            ddlcolor.SelectedItem.Text = "-";
            txtcolorName.Text = "-";
        }
        //if (ddlcolor.SelectedItem.Value == "") { ddlcolor.SelectedItem.Value = "0"; ddlcolor.SelectedItem.Text = "-"; }
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("DocCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Room");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvInterestedProducts.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvInterestedProducts.SelectedRow.RowIndex)
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        if (ddlModelNo.SelectedIndex>=1)
                        {
                            dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        }
                        else if (txtModelNo.Text != null)
                        {
                            dr["ModelNo"] =txtModelNo.Text;

                        }
                        //dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["Brand"] = txtBrand.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["Specifications"] = txtItemSpec.Text;
                        dr["Remarks"] = txtRemarks.Text; ;
                        dr["Priority"] = ddlPriority.SelectedItem.Text;
                        //dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        dr["DocCharges"] = txtDocCharges.Text;
                        dr["DocInFavourOf"] = txtInFavourofDoc.Text;
                        dr["EMDCharges"] = txtEMDCharges.Text;
                        dr["EMDInFavourOf"] = txtInFavourofEMD.Text;
                        dr["Room"] = txtRoom.Text;
                        if (ddlcolor.SelectedIndex >= 1)
                        {
                            dr["color"] = txtcolorName.Text;
                            dr["ColorId"] = 0;

                        }
                        else if (txtcolorName.Text == "")
                        {
                            dr["Color"] = ddlcolor.SelectedItem.Text;
                            dr["ColorId"] = ddlcolor.SelectedItem.Value;
                        }
                        dr["ItemName"] = txtItemName.Text;


                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["Brand"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Specifications"] = gvrow.Cells[7].Text;
                        dr["Remarks"] = gvrow.Cells[8].Text;
                        dr["Priority"] = gvrow.Cells[9].Text;
                        // dr["ItemTypeId"] = gvrow.Cells[10].Text;
                        dr["DocCharges"] = gvrow.Cells[10].Text;
                        dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                        dr["EMDCharges"] = gvrow.Cells[12].Text;
                        dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                        dr["Room"] = gvrow.Cells[14].Text;

                        dr["Color"] = gvrow.Cells[15].Text;
                        dr["ColorId"] = gvrow.Cells[16].Text;
                        dr["ItemName"] = gvrow.Cells[17].Text;

                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["Brand"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Specifications"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;
                    dr["Priority"] = gvrow.Cells[9].Text;
                    // dr["ItemTypeId"] = gvrow.Cells[10].Text;
                    dr["DocCharges"] = gvrow.Cells[10].Text;
                    dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                    dr["EMDCharges"] = gvrow.Cells[12].Text;
                    dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                    dr["Room"] = gvrow.Cells[14].Text;
                    dr["Color"] = gvrow.Cells[15].Text;
                    dr["ColorId"] = gvrow.Cells[16].Text;
                    dr["ItemName"] = gvrow.Cells[17].Text;

                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gvInterestedProducts.SelectedIndex == -1)
        {
            DataRow drnew = InterestedProducts.NewRow();
            if (ddlModelNo.SelectedIndex != -1)
            {
                drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
                drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            }
            else
                if (txtModelNo.Text != "")
                {
                    drnew["ItemCode"] = "0";
                    drnew["ModelNo"] = txtModelNo.Text;
                }
            drnew["Brand"] = txtBrand.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;
            drnew["Specifications"] = txtItemSpec.Text;
            drnew["Remarks"] = txtRemarks.Text; ;
            drnew["Priority"] = ddlPriority.SelectedItem.Text;
            //drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            drnew["DocCharges"] = txtDocCharges.Text;
            drnew["DocInFavourOf"] = txtInFavourofDoc.Text;
            drnew["EMDCharges"] = txtEMDCharges.Text;
            drnew["EMDInFavourOf"] = txtInFavourofEMD.Text;
            drnew["Room"] = txtRoom.Text;
            if (ddlcolor.SelectedIndex != -1)
            {
                drnew["Color"] = ddlcolor.SelectedItem.Text;
                drnew["ColorId"] = ddlcolor.SelectedItem.Value;
            }
            else
                if (txtcolorName.Text != "")
                {
                    drnew["Color"] = txtcolorName.Text;
                    drnew["ColorId"] = "0";
                }
            drnew["ItemName"] = txtItemName.Text;

            InterestedProducts.Rows.Add(drnew);
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
        gvInterestedProducts.SelectedIndex = -1;
        btnRefreshItems_Click(sender, e);
    }
    #endregion

    #region gvInterestedProducts_RowEditing
    protected void gvInterestedProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);
        //col = new DataColumn("ItemTypeId");
        //InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Room");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                DataRow dr = InterestedProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["Brand"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Specifications"] = gvrow.Cells[7].Text;
                dr["Remarks"] = gvrow.Cells[8].Text;
                dr["Priority"] = gvrow.Cells[9].Text;

                dr["DocCharges"] = gvrow.Cells[10].Text;
                dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                dr["EMDCharges"] = gvrow.Cells[12].Text;
                dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                dr["Room"] = gvrow.Cells[14].Text;
                dr["Color"] = gvrow.Cells[15].Text;
                dr["ColorId"] = gvrow.Cells[16].Text;
                dr["ItemName"] = gvrow.Cells[17].Text;

                InterestedProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvInterestedProducts.Rows[e.NewEditIndex].RowIndex)
                {
                    //ddlItemType.SelectedValue = gvrow.Cells[10].Text;
                    //ItemName_Fill();
                    ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlModelNo_SelectedIndexChanged(sender, e);
                    //ddlItemName_SelectedIndexChanged(sender, e);
                    if (gvrow.Cells[5].Text != "")
                    {
                        txtItemUOM.Text = gvrow.Cells[5].Text;
                    }
                    else
                    {
                        txtItemUOM.Text = null;
                    }
                    // ItemTypes_Fill();
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemSpecifications.Text = gvrow.Cells[7].Text;
                    txtRemarks.Text = gvrow.Cells[8].Text;
                    //ddlPriority.SelectedValue = gvrow.Cells[9].Text;
                    txtDocCharges.Text = gvrow.Cells[10].Text;
                    txtInFavourofDoc.Text = gvrow.Cells[11].Text;
                    txtEMDCharges.Text = gvrow.Cells[12].Text;
                    txtInFavourofEMD.Text = gvrow.Cells[13].Text;
                    txtRoom.Text = gvrow.Cells[14].Text;
                    ddlcolor.SelectedValue = gvrow.Cells[16].Text;
                    txtItemName.Text = gvrow.Cells[17].Text;
                    gvInterestedProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region gvInterestedProducts_RowDeleting
    protected void gvInterestedProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvInterestedProducts.Rows[e.RowIndex].Cells[2].Text;
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("DocCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Room");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["Brand"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Specifications"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;
                    dr["Priority"] = gvrow.Cells[9].Text;

                    dr["DocCharges"] = gvrow.Cells[10].Text;
                    dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                    dr["EMDCharges"] = gvrow.Cells[12].Text;
                    dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                    dr["Room"] = gvrow.Cells[14].Text;
                    dr["Color"] = gvrow.Cells[15].Text;
                    dr["ColorId"] = gvrow.Cells[16].Text;
                    dr["ItemName"] = gvrow.Cells[17].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region gvInterestedProducts_RowDataBound
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (ddlEnquirySource.SelectedItem.Text != "Tender")
            //{
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[17].Visible = false;
            //}
            
        }
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[9].Visible = e.Row.Cells[5].Visible = e.Row.Cells[16].Visible = e.Row.Cells[17].Visible = false;
            }
        }


    }
    #endregion

    #region Button Rfresh Items Click
    protected void btnRefreshItems_Click(object sender, EventArgs e)
    {
        //####
        txtBrand.Text = string.Empty;
        txtModelNo.Text = string.Empty;
        txtcolorName.Text = string.Empty;
        txtBrandName.Text = string.Empty;
        //#####
        //if (ddlBrandStock.SelectedValue != "0")
        //{
        //    ddlModelNo.SelectedValue = "0";
        //}
        txtItemUOM.Text = string.Empty;//
        txtBrand.Text = string.Empty;//
        //txtItemQuantity.Text = string.Empty;
        txtItemCategory.Text = string.Empty;//
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        ddlBrandStock.SelectedValue = "0";
        txtItemName.Text = string.Empty;//
        txtItemQuantity.Text = string.Empty;//
        txtRoom.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;//
        txtColor.Text = string.Empty;//
        txtItemSpec.Text = string.Empty;
        ddlcolor.SelectedValue = "0";
        txtItemSpecifications.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlPriority.SelectedValue = "3";
        txtDocCharges.Text = string.Empty;
        txtInFavourofDoc.Text = string.Empty;
        txtEMDCharges.Text = string.Empty;
        txtInFavourofEMD.Text = string.Empty;
        ddlModelNo.Items.Clear();
        ddlcolor.Items.Clear();
        txtSearchModel.Text = "--";
        // gvInterestedProducts.SelectedIndex = -1;
    }
    #endregion

    #region Button SAVE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (btnSave.Text == "Save")
        {
            SalesEnquirySave();
            //Response.Redirect("SalesEnquiry.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            SalesEnquiryUpdate();
           // Response.Redirect("SalesEnquiry.aspx");
        }
    }
    #endregion

    #region SalesEnquirySave
    private void SalesEnquirySave()
    {
        //if (gvInterestedProducts.Rows.Count > 0)
        //{
            try
            {
                //txtEnquiryDueDate.Text = Convert.ToDateTime(DateTime.Now).ToString();

                //txtEnquiryDueDate.Text=Convert.ToDateTime(txtEnquiryDueDate.Text).ToShortDateString();
                btnSave.Enabled = false;
                var dateTimeNow = DateTime.Now; 
                var dateOnlyString = dateTimeNow.ToShortDateString();
                txtEnquiryDueDate.Text = dateOnlyString;

                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                SM.BeginTransaction();
                objSM.EnqNo = txtEnquiryNo.Text;
                
                txtEnquiryDate.Text = Convert.ToDateTime(DateTime.Now).ToString();
                txtEnquiryDate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
                //objSM.EnqDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDate.Text);

                objSM.EnqDate = txtEnquiryDueDate.Text;
                //Yantra.Classes.General.toMMDDYYYY(dateOnlyString);

                objSM.CustId = ddlCustomer.SelectedItem.Value;
                objSM.EnqModeId = ddlEnquirySource.SelectedItem.Value;
                if (rbEmployee.Checked == true)
                { objSM.EnqOrigBy = rbEmployee.Text; }
                else if (rbAgent.Checked == true)
                { objSM.EnqOrigBy = rbAgent.Text; }

                objSM.EnqOrigName = ddlOriginatedBy.SelectedItem.Value;

                objSM.EnqRef = txtReferenceCode.Text;
                objSM.EnqFollowUp = txtFollowUpCriteria.Text;
                objSM.EnqDeliveryDate = txtEnquiryDueDate.Text;
                    //Yantra.Classes.General.toMMDDYYYY(dateOnlyString);
                objSM.DespModeId = ddlDeliveryType.SelectedItem.Value;
                objSM.PromotionType = txtPromotionType.Text;
                objSM.PromotionActivity = txtPromotionActivity.Text;
                objSM.EnqStatus = "New";
                objSM.EnqDueDate = txtEnquiryDueDate.Text;
                    //Yantra.Classes.General.toMMDDYYYY(dateOnlyString);
                objSM.EnqDesc = txtDescription.Text;
                objSM.CustUnitId = ddlUnitName.SelectedItem.Value;
                objSM.CustDetId = ddlContactPerson.SelectedItem.Value;
                objSM.EnqSubTime = txtSubmissionTime.Text;
                objSM.EnqOpeningDate = txtEnquiryDueDate.Text;
                    //Yantra.Classes.General.toMMDDYYYY(dateOnlyString);
                objSM.EnqOpeningTime = txtOpeningTime.Text;
                objSM.EnqDocCharges = txtDocCharges.Text;
                objSM.EnqDocFavourof = txtInFavourofDoc.Text;
                objSM.EnqEMDCharges = txtEMDCharges.Text;
                objSM.EnqEMDFavourof = txtInFavourofEMD.Text;
                objSM.EnqTenderDate = txtEnquiryDueDate.Text;
                    //Yantra.Classes.General.toMMDDYYYY(dateOnlyString);

                objSM.EnqContact = txtContactNo.Text;
                objSM.EnqPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.EnqApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.CpId = lblCPID.Text;
                objSM.ENQPRIORITY = ddlenqpriority.SelectedItem.Value;
                if (objSM.SalesEnquiry_Save() == "Data Saved Successfully")
                {
                    SM.CustomerMaster obj = new SM.CustomerMaster();
                    obj.UpdateCustomerStatus(objSM.CustId);
                    objSM.SalesEnquiryDetails_Delete(objSM.EnqId);
                    foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                    {
                        objSM.EnqDetItemCode = gvrow.Cells[2].Text;
                        objSM.modelno = gvrow.Cells[3].Text;
                        objSM.brand = gvrow.Cells[4].Text;
                        objSM.itemname = gvrow.Cells[17].Text;

                        objSM.EnqDetQty = gvrow.Cells[6].Text;
                        objSM.EnqDetSpec = gvrow.Cells[7].Text;
                        objSM.EnqDetRemarks = gvrow.Cells[8].Text;
                        objSM.EnqDetPriority = gvrow.Cells[9].Text;
                        objSM.EnqDocCharges = gvrow.Cells[10].Text;
                        objSM.EnqDocFavourof = gvrow.Cells[11].Text;
                        objSM.EnqEMDCharges = gvrow.Cells[12].Text;
                        objSM.EnqEMDFavourof = gvrow.Cells[13].Text;
                        objSM.EnqDetRoom = gvrow.Cells[14].Text;
                        objSM.EnqColor = gvrow.Cells[16].Text;
                        objSM.uom = gvrow.Cells[5].Text;
                        objSM.colorname = gvrow.Cells[15].Text;
                        
                        objSM.SalesEnquiryDetails_Save();
                    }
                    SM.CommitTransaction();
                   // MessageBox.Show(this, "Data Saved Successfully");
                    tblSalesEnquiry.Visible = false;
                }
                else
                {
                    SM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //tblSalesEnquiry.Visible = false;
                btnSave.Enabled = true;
                gvInterestedProducts.DataBind();
                
                //btnDelete.Attributes.Clear();
                //gvSalesEnquiry.DataBind();
                //gvCustMasterDetails.DataSourceID = "sdsCustMaster";
                //gvCustMasterDetails.DataBind();
                ////SM.ClearControls(this);
                //lblCountUnLeads.Text = SM.SalesEnquiry.AutoCountUnleads(lblCPID.Text.ToString());
                //int unleads = int.Parse(lblCountUnLeads.Text.ToString());
                //if (unleads > 0)
                //{ btnUnLeadsGo.Visible = true; }
                //else { btnUnLeadsGo.Visible = false; }
                //SM.Dispose();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
          "alert(' Enquiry Saved sucessfully');window.location ='SalesEnquiry.aspx';", true);
            }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please Add atleast one Item");
        //}
    }
    #endregion

    #region SalesEnquiryUpdate
    private void SalesEnquiryUpdate()
    {
        if (gvInterestedProducts.Rows.Count > 0)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                SM.BeginTransaction();
                objSM.EnqId = Request.QueryString["Eid"].ToString();
                objSM.EnqDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDate.Text);
                objSM.CustId = ddlCustomer.SelectedItem.Value;
                objSM.EnqModeId = ddlEnquirySource.SelectedItem.Value;
                if (rbEmployee.Checked == true)
                { objSM.EnqOrigBy = rbEmployee.Text; }
                else if (rbAgent.Checked == true)
                { objSM.EnqOrigBy = rbAgent.Text; }

                objSM.EnqOrigName = ddlOriginatedBy.SelectedItem.Value;

                objSM.EnqRef = txtReferenceCode.Text;
                objSM.EnqFollowUp = txtFollowUpCriteria.Text;
                objSM.EnqDeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
                objSM.DespModeId = ddlDeliveryType.SelectedItem.Value;
                objSM.PromotionType = txtPromotionType.Text;
                objSM.PromotionActivity = txtPromotionActivity.Text;
                objSM.EnqStatus = "New";
                objSM.EnqDueDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDueDate.Text);
                objSM.EnqDesc = txtDescription.Text;
                objSM.CustUnitId = ddlUnitName.SelectedItem.Value;
                objSM.CustDetId = ddlContactPerson.SelectedItem.Value;
                objSM.EnqSubTime = txtSubmissionTime.Text;
                objSM.EnqOpeningDate = Yantra.Classes.General.toMMDDYYYY(txtOpeningDate.Text);

                objSM.EnqOpeningTime = txtOpeningTime.Text;
                objSM.EnqDocCharges = txtDocCharges.Text;
                objSM.EnqDocFavourof = txtInFavourofDoc.Text;
                objSM.EnqEMDCharges = txtEMDCharges.Text;
                objSM.EnqEMDFavourof = txtInFavourofEMD.Text;
                objSM.EnqTenderDate = Yantra.Classes.General.toMMDDYYYY(txtTenderDate.Text);

                objSM.EnqContact = txtContactNo.Text;
                objSM.EnqPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.EnqApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.CpId = lblCPID.Text;
                objSM.ENQPRIORITY = ddlenqpriority.SelectedItem.Value;
                if (objSM.SalesEnquiry_Update() == "Data Updated Successfully")
                {
                    objSM.SalesEnquiryDetails_Delete(objSM.EnqId);
                    foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                    {
                        objSM.EnqDetItemCode = gvrow.Cells[2].Text;
                        objSM.modelno = gvrow.Cells[3].Text;
                        objSM.brand = gvrow.Cells[4].Text;
                        objSM.itemname = gvrow.Cells[17].Text;

                        objSM.EnqDetQty = gvrow.Cells[6].Text;
                        objSM.EnqDetSpec = gvrow.Cells[7].Text;
                        objSM.EnqDetRemarks = gvrow.Cells[8].Text;
                        objSM.EnqDetPriority = gvrow.Cells[9].Text;
                        objSM.EnqDocCharges = gvrow.Cells[10].Text;
                        objSM.EnqDocFavourof = gvrow.Cells[11].Text;
                        objSM.EnqEMDCharges = gvrow.Cells[12].Text;
                        objSM.EnqEMDFavourof = gvrow.Cells[13].Text;
                        objSM.EnqDetRoom = gvrow.Cells[14].Text;
                        objSM.EnqColor = gvrow.Cells[16].Text;
                        objSM.uom = gvrow.Cells[5].Text;
                        objSM.colorname = gvrow.Cells[15].Text;

                        
                        objSM.SalesEnquiryDetails_Save();
                    }
                    SM.CommitTransaction();
                   // MessageBox.Show(this, "Data Updated Successfully");
                    tblSalesEnquiry.Visible = false;
                }
                else
                {
                    SM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblSalesEnquiry.Visible = false;
                gvInterestedProducts.DataBind();
                //btnDelete.Attributes.Clear();
                //gvSalesEnquiry.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
        "alert(' Enquiry Updated sucessfully');window.location ='SalesEnquiry.aspx';", true);
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    #endregion
    
    #region gvSalesEnquiry_RowDataBound
    protected void gvSalesEnquiry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        if (Request.QueryString["Eid"] !=null)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                MessageBox.Show(this, objSM.SalesEnquiry_Delete(Request.QueryString["Eid"].ToString()));
                Response.Redirect("SalesEnquiry.aspx");
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblSalesEnquiry.Visible = false;
                //btnDelete.Attributes.Clear();
                //gvSalesEnquiry.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        btnSave.Text = "Update";
        ExistingRecordFill(sender, e);
    }

    private void ExistingRecordFill(object sender, EventArgs e)
    {
        if (Request.QueryString["Eid"] != null)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                if (objSM.SalesEnquiry_Select(Request.QueryString["Eid"].ToString()) > 0)
                {
                    btnSave.Visible = true;
                    btnAssign.Visible = false;
                    btnSave.Text = "Update";
                    btnRefresh.Visible = false;
                    btnRegret.Visible = true;

                    tblSalesEnquiry.Visible = true;
                    txtEnquiryNo.Text = objSM.EnqNo;
                    txtEnquiryDate.Text = objSM.EnqDate;
                    txtTenderDate.Text = objSM.EnqTenderDate;
                    ddlCustomer.SelectedValue = objSM.CustId;
                    ddlEnquirySource.SelectedValue = objSM.EnqModeId;
                    ddlEnquirySource_SelectedIndexChanged(sender, e);
                    if (objSM.EnqOrigBy == rbEmployee.Text)
                    {
                        rbEmployee.Checked = true;
                        rbAgent.Checked = false;
                        rbEmployeeAgent_CheckedChanged(sender, e);
                    }
                    else if (objSM.EnqOrigBy == rbAgent.Text)
                    {
                        rbAgent.Checked = true;
                        rbEmployee.Checked = false;
                        rbEmployeeAgent_CheckedChanged(sender, e);
                    }
                    //ddlOriginatedBy.SelectedIndex = ddlOriginatedBy.Items.IndexOf(ddlOriginatedBy.Items.FindByText(objSM.EnqOrigName));
                    ddlOriginatedBy.SelectedValue = objSM.EnqOrigName;
                    txtReferenceCode.Text = objSM.EnqRef;
                    txtFollowUpCriteria.Text = objSM.EnqFollowUp;
                    txtDeliveryDate.Text = objSM.EnqDeliveryDate;
                    ddlDeliveryType.SelectedValue = objSM.DespModeId;
                    txtPromotionType.Text = objSM.PromotionType;
                    txtPromotionActivity.Text = objSM.PromotionActivity;
                    txtEnquiryDueDate.Text = objSM.EnqDueDate;
                    txtDescription.Text = objSM.EnqDesc;
                    txtSubmissionTime.Text = objSM.EnqSubTime;
                    txtOpeningDate.Text = objSM.EnqOpeningDate;
                    txtOpeningTime.Text = objSM.EnqOpeningTime;
                    txtDocCharges.Text = objSM.EnqDocCharges;
                    txtInFavourofDoc.Text = objSM.EnqDocFavourof;
                    txtEMDCharges.Text = objSM.EnqEMDCharges;
                    txtInFavourofEMD.Text = objSM.EnqEMDFavourof;
                    txtContactNo.Text = objSM.EnqContact;
                    ddlApprovedBy.SelectedValue = objSM.EnqApprovedBy;
                    if(objSM.EnqApprovedBy != "0")
                    {
                        btnApprove.Visible = false;
                        btnSave.Visible = true;
                        btnAssign.Visible = true; 
                    }

                    ddlPreparedBy.SelectedValue = objSM.EnqPreparedBy;
                    ddlenqpriority.SelectedValue = objSM.ENQPRIORITY;
                    objSM.SalesEnquiryDetails_Select(Request.QueryString["Eid"].ToString(), gvInterestedProducts);

                    ddlCustomer_SelectedIndexChanged(sender, e);
                    if (ddlUnitName.Items.Count > 1)
                    {
                        ddlUnitName.SelectedValue = objSM.CustUnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                        ddlContactPerson.SelectedValue = objSM.CustDetId;
                        ddlContactPerson_SelectedIndexChanged(sender, e);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
        gvInterestedProducts.DataBind();
        btnNew_Click(sender, e);
    }
    #endregion

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Eid"] != null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesenq&enqno=" + Request.QueryString["Eid"].ToString() + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Employee/Agent Radio Button Change
    protected void rbEmployeeAgent_CheckedChanged(object sender, EventArgs e)
    {
        lblOrginatedList.Text = "Employee Name";
        HR.EmployeeMaster.EmployeeMaster_Select(ddlOriginatedBy);
    }
    #endregion

    #region Assign Button Click
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        tblSalesEnquiry.Visible = false;
        if (Request.QueryString["Eid"] != null)
        {
            try
            {
                txtAssignTaskNo.Text = SM.SalesAssignments.SalesAssignments_AutoGenCode();
                SM.SalesAssignments objSMAssign = new SM.SalesAssignments();
                if (objSMAssign.SalesAssignments_Select(Request.QueryString["Eid"].ToString()) > 0)
                {
                    tblAssignTasks.Visible = true;
                    btnAssignTask.Text = "Re-Assign";

                    txtEnquiryNoForAssign.Text = string.Empty;
                    txtEnquiryDateForAssign.Text = string.Empty;
                    txtCustomerNameForAssingn.Text = string.Empty;
                    txtCustomerEmailForAssingn.Text = string.Empty;
                    ddlEmpNameForAssign.SelectedValue = "0";
                    txtEmpEmailId.Text = string.Empty;
                    txtRemarksForAssingn.Text = string.Empty;
                    txtAssignDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    txtEnquiryNoForAssign.Text = objSMAssign.EnqNo;
                    txtEnquiryDateForAssign.Text = objSMAssign.EnqDate;
                    ddlEmpNameForAssign.SelectedValue = objSMAssign.EmpId;
                    txtAssignDate.Text = objSMAssign.AssingDate;
                    txtDueDate.Text = objSMAssign.DueDate;
                    txtRemarksForAssingn.Text = objSMAssign.AssignRemarks;
                    ddlEmpNameForAssign_SelectedIndexChanged(sender, e);
                    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    if ((objSMCustomer.CustomerMaster_Select(objSMAssign.CustId)) > 0)
                    {
                        txtCustomerNameForAssingn.Text = objSMCustomer.CustName;
                        txtCustomerEmailForAssingn.Text = objSMCustomer.Email;
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Re-Assign this Enquiry?');");
                }
                else
                {
                    SM.SalesEnquiry objSMEnq = new SM.SalesEnquiry();
                    if (objSMEnq.SalesEnquiry_Select(Request.QueryString["Eid"].ToString()) > 0)
                    {
                        tblAssignTasks.Visible = true;
                        btnAssignTask.Text = "Assign";

                        txtEnquiryNoForAssign.Text = string.Empty;
                        txtEnquiryDateForAssign.Text = string.Empty;
                        txtCustomerNameForAssingn.Text = string.Empty;
                        txtCustomerEmailForAssingn.Text = string.Empty;
                        ddlEmpNameForAssign.SelectedValue = "0";
                        txtEmpEmailId.Text = string.Empty;
                        txtRemarksForAssingn.Text = string.Empty;
                        txtAssignDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        txtEnquiryNoForAssign.Text = objSMEnq.EnqNo;
                        txtEnquiryDateForAssign.Text = objSMEnq.EnqDate;

                        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                        if ((objSMCustomer.CustomerMaster_Select(objSMEnq.CustId)) > 0)
                        {
                            txtCustomerNameForAssingn.Text = objSMCustomer.CustName;
                            txtCustomerEmailForAssingn.Text = objSMCustomer.Email;
                        }
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Assign this Enquiry?');");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //btnDelete.Attributes.Clear();
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region ddlEmpNameForAssign_SelectedIndexChanged
    protected void ddlEmpNameForAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    HR.EmployeeMaster objHR = new HR.EmployeeMaster();
        //    if (objHR.EmployeeMaster_Select(ddlEmpNameForAssign.SelectedItem.Value) > 0)
        //    {
        //        txtEmpEmailId.Text = objHR.EmpEMail;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{
        //    HR.Dispose();
        //}
    }
    #endregion

    #region btnAssignTask_Click
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {

        //if (Convert.ToDateTime(Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text)) > Convert.ToDateTime(Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text)))
        //{
        //    MessageBox.Show(this, "Due Date should not be less than Assign Date");
        //    return;
        //}


        try
        {
            SM.SalesAssignments objSMAssign = new SM.SalesAssignments();
            SM.BeginTransaction();
            objSMAssign.AssignTaskNo = txtAssignTaskNo.Text;
            objSMAssign.EnqId = Request.QueryString["Eid"].ToString();
            objSMAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
            objSMAssign.AssingDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);

            objSMAssign.DueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);

            objSMAssign.AssignRemarks = txtRemarksForAssingn.Text;
            objSMAssign.AssignStatus = "New";
            objSMAssign.Cpid = lblCPID.Text;
            SM.SalesEnquiry.SalesEnquiryStatus_Update(SM.SMStatus.Open, Request.QueryString["Eid"].ToString());

            if (btnAssignTask.Text == "Assign")
            {
                MessageBox.Show(this, objSMAssign.SalesAssignments_Save());
                SM.CommitTransaction();
            }
            else if (btnAssignTask.Text == "Re-Assign")
            {
                MessageBox.Show(this, objSMAssign.SalesAssignments_Update());
                SM.CommitTransaction();
            }
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblSalesEnquiry.Visible = false;
            tblAssignTasks.Visible = false;
            gvInterestedProducts.DataBind();
            //btnDelete.Attributes.Clear();
            btnAssignTask.Attributes.Clear();
            //gvSalesEnquiry.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
            Response.Redirect("~/Modules/SM/SalesAssignments.aspx");
        }
       
    }
    #endregion

    #region btnCancelTask_Click
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {
        tblAssignTasks.Visible = false;
        Response.Redirect("~/Modules/SM/SalesEnquiry.aspx");
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtPhoneNo.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlContactPerson_SelectedIndexChanged
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtEmail.Text = objSMCustomer.CustCorpEmail;
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
                txtMobile.Text = objSMCustomer.CustCorpMobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlEnquirySource_SelectedIndexChanged
    protected void ddlEnquirySource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  SM.SalesEnquiry objSMSalesEnquiry = new SM.SalesEnquiry();

            if (ddlEnquirySource.SelectedItem.Text == "Tender")
            {
                //tblTenderDetails.Visible = true;
                //TenderDetailsShowHide(true);
                //lblReferenceCode.Text = "Tender No";
                //lblEnquiryDueDate.Text = "Tender Due Date";
                TenderDetailsShowHide(false);
                lblReferenceCode.Text = "Reference";
                lblEnquiryDueDate.Text = "Enquiry Due Date";
            }
            else
            {
                //tblTenderDetails.Visible = false;
                TenderDetailsShowHide(false);
                lblReferenceCode.Text = "Reference";
                lblEnquiryDueDate.Text = "Enquiry Due Date";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
        }
    }
    #endregion


    #region TenderDetailsShowHide
    private void TenderDetailsShowHide(bool showhide)
    {
        tdSubmissionTime1.Visible = tdSubmissionTime2.Visible = showhide;
        tdOpeningDate1.Visible = tdOpeningDate2.Visible = showhide;
        tdOpeningTime1.Visible = tdOpeningTime2.Visible = showhide;
        tdDocCharges1.Visible = tdDocCharges2.Visible = showhide;
        tdInfavourOfDoc1.Visible = tdInfavourOfDoc2.Visible = showhide;
        tdEMDCharges1.Visible = tdEMDCharges2.Visible = showhide;
        tdInfavourOfEMD1.Visible = tdInfavourOfEMD2.Visible = showhide;
        tdTenderDate1.Visible = tdTenderDate2.Visible = showhide;
    }
    #endregion

    #region Button REGRET Click
    protected void btnRegret_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Eid"] != null)
        {
            try
            {
                MessageBox.Show(this, SM.SalesEnquiry.SalesEnquiryStatus_Update(SM.SMStatus.Obsolete, Request.QueryString["Eid"].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                //gvSalesEnquiry.DataBind();
                SM.Dispose();
                Response.Redirect("SalesEnquiry.aspx");
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Model nO Select Change
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtModelNo.Text = ddlModelNo.SelectedItem.Text;
            ddlPriority.SelectedIndex = 3;

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select345(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Content/ItemImage/"+objMaster.ItemImage;
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlModelNo.SelectedValue);
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

    #region lbtnCustMaster
    protected void lbtnCustMaster_Click(object sender, EventArgs e)
    {
        //gvSalesEnquiry.SelectedIndex = -1;
        // gvCustMasterDetails.SelectedIndex = -1; 
        gvInterestedProducts.DataBind();
        //btnDelete.Attributes.Clear();
        SM.ClearControls(this);
        txtEnquiryNo.Text = SM.SalesEnquiry.SalesEnquiry_AutoGenCode();
        txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        LinkButton lbtnCustMaster;
        lbtnCustMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCustMaster.Parent.Parent;
        
        tblSalesEnquiry.Visible = true;
        tblAssignTasks.Visible = false;
        btnAssign.Visible = false;
        btnRegret.Visible = false;
        btnRefresh.Visible = true;
        btnSave.Enabled = true;
        btnSave.Text = "Save";
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
    }
    #endregion

    #region gvCustMasterDetails_RowDataBound
    protected void gvCustMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;

        }
    }
    #endregion

    protected void gvInterestedProducts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        try
        {
            SM.SalesEnquiry objSMEnqApprove = new SM.SalesEnquiry();
            SM.BeginTransaction();
            objSMEnqApprove.EnqId = Request.QueryString["Eid"].ToString();
            objSMEnqApprove.EnqApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMEnqApprove.EnqApprove_Update();
            MessageBox.Show(this, "Data Approved Sucessfully");
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvSalesEnquiry.DataBind();
            SM.Dispose();
            btnEdit_Click(sender, e);
        }

    }
    
    protected void ddlBrandStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrandName.Text = "";
            txtBrandName.Text = ddlBrandStock.SelectedItem.Text;
            //ddlModelNo.DataSource = null;
            //ddlModelNo.DataBind();
            Masters.ItemMaster.ItemMaster5_Select(ddlModelNo, ddlBrandStock.SelectedItem.Value);
            //SM.DDLBindWithSelect(ddlModelNo, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrandStock.SelectedItem.Value);
        }
        catch (Exception ex)
        {
        
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            // Masters.Dispose();
        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
        ddlBrandStock.SelectedIndex = 0;
        txtBrandName.Text = "";
    }
    


    #region Item TypesAll Fill
    private void ItemTypesAll_Fill()
    {
        try
        {
            Masters.ItemMaster  .ItemMasterself_Select(ddlModelNo);
            // SM.SalesEnquiry.SalesEnquiryItemTypes1_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }       
        finally
        {
            //Masters.Dispose();            
            //  SM.Dispose();
        }
    }
    #endregion
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvSalesEnquiry.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvSalesEnquiry.DataBind();
    }


    protected void ddlcolor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtcolorName.Text = ddlcolor.SelectedItem.Text;
    }

    protected void txtBrandName_TextChanged(object sender, EventArgs e)
    {
        if (ddlModelNo.SelectedIndex < 0)
        {
            txtSearchModel.Text = "--";
            //ddlModelNo.SelectedIndex = -1;
            txtBrand.Text = txtBrandName.Text;
            ddlModelNo.Items.Clear();
        }
    }
    protected void txtModelNo_TextChanged(object sender, EventArgs e)
    {
        ddlPriority.SelectedValue = "3";
    }
    protected void btngo2_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text != "")
        {
            ddlCustomer.DataSourceID = "SqlDataSource1";
            ddlCustomer.DataTextField = "CUST_NAME";
            ddlCustomer.DataValueField = "CUST_ID";
            ddlCustomer.DataBind();
            ddlCustomer_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
}
 
