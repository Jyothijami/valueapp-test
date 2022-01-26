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

public partial class Modules_Services_InstallAssignments : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSearchValueFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtSearchText.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            btnSearchGo_Click(sender, e);
            gvInstallAssignDetails.DataBind();
            EmployeeMaster_Fill();
            CustomerMaster_Fill();
            ItemTypes_Fill();
            
        }
    }

    #region Unit Name Fill
    private void UnitName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
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

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomerName);
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

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemType);
            // Services.SalesQuotation.SalesQuotationItemTypes_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            //  Services.Dispose();
        }
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvInstallAssignDetails.SelectedIndex = -1;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvInstallAssignDetails.DataBind();
    }
    #endregion
    
    protected void lbtnAssignNo_Click(object sender, EventArgs e)
    {
        lbtnCustName_Click(sender, e);
        //txtCRNo.Text = Services.ComplaintRegister.ComplaintRegister_AutoGenCode();
        //txtCRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //tblComplaintRegister.Visible = true;
        //Inventory.Delivery objDC = new Inventory.Delivery();
        //objDC.DeliveryDetailsBySalesOrderId_Select2(lblSONO.Text.ToString(), gvQuotationItems);
        //SM.SalesOrder objSM = new SM.SalesOrder();
        //objSM.SalesOrder_Select(lblSONO.Text.ToString());

        //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        //if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
        //{
        //    //    //txtCustomerName.Text = objSMCustomer.CustName;
        //    //   // txtAddress.Text = objSMCustomer.Address;
        //    //    //txtEmail.Text = objSMCustomer.Email;
        //    //    //txtRegion.Text = objSMCustomer.RegName;
        //    //    //txtPhone.Text = objSMCustomer.Phone;
        //    //    //txtMobile.Text = objSMCustomer.Mobile;
        //    ddlCallType.SelectedValue = "Installation";
        //    ddlCustomerName.SelectedValue = objSM.CustId;
        //    ddlUnitName.SelectedValue = objSM.CustUnitId;
        //    ddlCustomerName_SelectedIndexChanged(sender, e);
        //} 

    }

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //  ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                // txtRegion.Text = objSMCustomer.RegName;
                // txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //   txtEmail.Text = objSMCustomer.Email;
                //  txtPhoneNo.Text = objSMCustomer.Phone;
                //  txtMobile.Text = objSMCustomer.Mobile;
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

        //try
        //{
        //    SM.CustomerMaster objCustUnits = new SM.CustomerMaster();
        //    if (objCustUnits.CustomerMasterUnitsDetailsEnquiry_Select(ddlUnitName.SelectedItem.Value) > 0)
        //    {
        //        txtUnitAddress.Text = objCustUnits.Address;

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{
        //    SM.Dispose();
        //}
    }
    #endregion

    #region ddlItemType_SelectedIndexChanged
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
            }
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

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UnitName_Fill();
        //Contact_Fill();

        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                rfvContactPerson.Enabled = true;
                rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
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
                rfvUnitName.Enabled = false;
                lblUnitAddress.Text = "Customer Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
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

    protected void lbtnCustName_Click(object sender, EventArgs e)
    {
        tblAssignTasks.Visible = false;
        LinkButton lbtnCustName;
        lbtnCustName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCustName.Parent.Parent;
        gvInstallAssignDetails.SelectedIndex = gvRow.RowIndex;

        try
        {
            ClearControls();
            tblAssignTasks.Visible = true;
            Services.InstallAssignment OBJASSN = new Services.InstallAssignment();
            if (OBJASSN.InstallAssignment_Select(gvInstallAssignDetails.SelectedRow.Cells[0].Text) > 0)
            {
                if (OBJASSN.EmpId == "0")
                {
                    btnAssignTask.Text = "Assign";
                    txtAssignTaskNo.Text = Services.InstallAssignment.InstallAssignment_AutoGenCode();
                }
                else if (OBJASSN.EmpId != "0")
                {
                    btnAssignTask.Text = "Re-Assign";
                    txtAssignTaskNo.Text = OBJASSN.IANo;
                }
                ddlEmpNameForAssign.SelectedValue = OBJASSN.EmpId;
                ddlEmpNameForAssign_SelectedIndexChanged(sender, e);
                txtRemarksForAssingn.Text = OBJASSN.IARemarks;
                txtAssignDate.Text = OBJASSN.IAAssignDate;
                txtDueDate.Text = OBJASSN.IADueDate;
                SM.SalesOrder objso = new SM.SalesOrder();
                if (objso.SalesOrder_Select(OBJASSN.SOId) > 0)
                {
                    lblSONO.Text = OBJASSN.SOId;
                    txtEnquiryNoForAssign.Text = objso.SONo;
                    txtEnquiryDateForAssign.Text = objso.SODate;
                    SM.CustomerMaster objcust = new SM.CustomerMaster();
                    if (objcust.CustomerMaster_Select(objso.CustId) > 0)
                    {
                        txtCustomerNameForAssingn.Text = objcust.CustName;
                        txtCustomerEmailForAssingn.Text = objcust.Email;
                    }
                }

                txtCRNo.Text = Services.ComplaintRegister.ComplaintRegister_AutoGenCode();
                txtCRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tblComplaintRegister.Visible = true;
                Inventory.Delivery objDC = new Inventory.Delivery();
                objDC.DeliveryDetailsBySalesOrderId_Select2(lblSONO.Text.ToString(), gvQuotationItems);
                SM.SalesOrder objSM = new SM.SalesOrder();
                objSM.SalesOrder_Select(lblSONO.Text.ToString());

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                {
                    //    //txtCustomerName.Text = objSMCustomer.CustName;
                    //   // txtAddress.Text = objSMCustomer.Address;
                    //    //txtEmail.Text = objSMCustomer.Email;
                    //    //txtRegion.Text = objSMCustomer.RegName;
                    //    //txtPhone.Text = objSMCustomer.Phone;
                    //    //txtMobile.Text = objSMCustomer.Mobile;
                    ddlCallType.SelectedValue = "Installation";
                    ddlCustomerName.SelectedValue = objSM.CustId;                   
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                    //ddlUnitName.SelectedValue = objSM.CustUnitId;
                    //ddlUnitName_SelectedIndexChanged(sender, e);
                    //ddlContactPerson.SelectedValue = objso.CustDetId;
                    
                }
                //if (objDC.unitid != "0")
                //{
                //    ddlUnitName.SelectedValue = objDC.unitid;
                //    ddlUnitName_SelectedIndexChanged(sender, e);
                //}
                //else
                //{
                //    txtUnitAddress.Text = objDC.custaddress;


                //}


                if (OBJASSN.IAStatus == "CLOSED")
                {
                    MessageBox.Show(this, "Service Report has been done");
                    return;
                }
                else
                {
                    Services.ServiceReport objsr = new Services.ServiceReport();
                    if (objsr.servicereportsalesorder_isrecordexists(OBJASSN.SOId) > 0)
                    {
                        objsr.InstallAssignmentStatussalesorder_Update1(OBJASSN.SOId);
                        // return;
                        gvInstallAssignDetails.DataBind();
                        MessageBox.Show(this, "Service Report has been done");
                        return;
                    }
                    
                }
            }
        }
        catch
        {
        }
    }

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpNameForAssign);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
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
  

    #region ddlEmpNameForAssign_SelectedIndexChanged
    protected void ddlEmpNameForAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlEmpNameForAssign.SelectedItem.Value) > 0)
            {
                txtEmpEmailId.Text = objHR.EmpEMail;
            }
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

    #region Button Assign Task
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        if (DateTime.Parse(Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text)) > DateTime.Parse(Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text)))
        {
            MessageBox.Show(this, "Due Date should not be less than Assign Date");
            return;
        }
        try
        {
            Services.InstallAssignment objServicesAssign = new Services.InstallAssignment();
            Services.BeginTransaction();
            objServicesAssign.IANo = txtAssignTaskNo.Text;
            objServicesAssign.IAId = gvInstallAssignDetails.SelectedRow.Cells[0].Text;
            objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
            objServicesAssign.IAAssignDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objServicesAssign.IADueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objServicesAssign.IADate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objServicesAssign.IARemarks = txtRemarksForAssingn.Text;
            objServicesAssign.IAStatus = "New";
            objServicesAssign.Cp_Id = lblCPID.Text;
            MessageBox.Show(this, objServicesAssign.InstallAssignment_Update());
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblAssignTasks.Visible = false;
            gvInstallAssignDetails.DataBind();
            ClearControls();
            Services.Dispose();
        }
    }
    #endregion

    #region Button Cancel Task
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {
        tblAssignTasks.Visible = false;
    }
    #endregion

    private void ClearControls()
    {
        txtAssignTaskNo.Text = "";
        txtAssignDate.Text = "";
        txtCustomerEmailForAssingn.Text = "";
        txtCustomerNameForAssingn.Text = "";
        txtDueDate.Text = "";
        txtEmpEmailId.Text = "";
        txtEnquiryDateForAssign.Text = "";
        txtEnquiryNoForAssign.Text = "";
        txtRemarksForAssingn.Text = "";
        ddlEmpNameForAssign.SelectedValue = "0";
    }

    protected void gvInstallAssignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
    }
    protected void btnCr_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ComplaintRegister.aspx?srid=" + lblSONO.Text + "");
        tblComplaintRegister.Visible = true;
        txtCRNo.Text = Services.ComplaintRegister.ComplaintRegister_AutoGenCode();
        txtCRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //tblComplaintRegister.Visible = true;
        Inventory.Delivery objDC = new Inventory.Delivery();
        objDC.DeliveryDetailsBySalesOrderId_Select2(lblSONO.Text.ToString(), gvQuotationItems);
        SM.SalesOrder objSM = new SM.SalesOrder();
        objSM.SalesOrder_Select(lblSONO.Text.ToString());

        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
        {
            //    //txtCustomerName.Text = objSMCustomer.CustName;
            //   // txtAddress.Text = objSMCustomer.Address;
            //    //txtEmail.Text = objSMCustomer.Email;
            //    //txtRegion.Text = objSMCustomer.RegName;
            //    //txtPhone.Text = objSMCustomer.Phone;
            //    //txtMobile.Text = objSMCustomer.Mobile;
            ddlCallType.SelectedValue = "Installation";
            ddlCustomerName.SelectedValue = objSM.CustId;
            ddlCustomerName_SelectedIndexChanged(sender, e);
        } 
    }

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtNatureofComplaint.Text == string.Empty)
        {
            txtNatureofComplaint.Text = "-";
        }
        if (txtRootCause.Text == string.Empty)
        { txtRootCause.Text = "-"; }
        if (txtCorrectiveAction.Text == string.Empty)
        { txtCorrectiveAction.Text = "-"; }
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvQuotationItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvQuotationItems.SelectedRow.RowIndex)
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["ItemCode"] = ddlItemType.SelectedItem.Value;
                        dr["ItemName"] = ddlItemType.SelectedItem.Text;
                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        dr["Quantity"] = txtQuantity.Text;
                        dr["SerialNo"] = txtSerialNo.Text;
                        dr["NatureofComplaint"] = txtNatureofComplaint.Text;
                        dr["RootCausedNotice"] = txtRootCause.Text;
                        dr["CorrectiveActionTaken"] = txtCorrectiveAction.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["SerialNo"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["ItemTypeId"] = gvrow.Cells[7].Text;
                        dr["NatureofComplaint"] = gvrow.Cells[8].Text;
                        dr["RootCausedNotice"] = gvrow.Cells[9].Text;
                        dr["CorrectiveActionTaken"] = gvrow.Cells[10].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["SerialNo"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["ItemTypeId"] = gvrow.Cells[7].Text;
                    dr["NatureofComplaint"] = gvrow.Cells[8].Text;
                    dr["RootCausedNotice"] = gvrow.Cells[9].Text;
                    dr["CorrectiveActionTaken"] = gvrow.Cells[10].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }

        //if (gvQuotationItems.Rows.Count > 0)
        //{
        //    if (gvQuotationItems.SelectedIndex == -1)
        //    {
        //        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
        //        {
        //            if (gvrow.Cells[2].Text == ddlItemName.SelectedItem.Value)
        //            {
        //                gvQuotationItems.DataSource = QuotationItems;
        //                gvQuotationItems.DataBind();
        //                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
        //                return;
        //            }
        //        }
        //    }
        //}

        if (gvQuotationItems.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
            drnew["ItemName"] = ddlItemType.SelectedItem.Text;
            drnew["Quantity"] = txtQuantity.Text;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            drnew["SerialNo"] = txtSerialNo.Text;
            drnew["NatureofComplaint"] = txtNatureofComplaint.Text;
            drnew["RootCausedNotice"] = txtRootCause.Text;
            drnew["CorrectiveActionTaken"] = txtCorrectiveAction.Text;

            QuotationItems.Rows.Add(drnew);
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
        gvQuotationItems.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";


        txtItemName.Text = string.Empty;//
        txtColor.Text = string.Empty;//
        txtItemCategory.Text = string.Empty;//
        txtItemSubCategory.Text = string.Empty;//
        txtColor.Text = string.Empty;//
        txtBrand.Text = string.Empty;//
        txtSerialNo.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtCorrectiveAction.Text = string.Empty;
        txtNatureofComplaint.Text = string.Empty;
        txtRootCause.Text = string.Empty;
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save & Assign")
        {
            ComplaintRegisterSave();
        }
        else if (btnSave.Text == "Update")
        {
            ComplaintRegisterUpdate();
        }
    }
    #endregion

    #region Complaint Register Save
    private void ComplaintRegisterSave()
    {
        try
        {
            Services.BeginTransaction();
            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

            //objComplaintRegister.CRId = gvComplaintRegister.SelectedRow.Cells[1].Text;

            objComplaintRegister.CRNo = txtCRNo.Text;
            objComplaintRegister.CRDate = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
            objComplaintRegister.CRCallType = ddlCallType.SelectedItem.Text;
            objComplaintRegister.CustId = ddlCustomerName.SelectedItem.Value;
            objComplaintRegister.CustUnitId = ddlUnitName.SelectedItem.Value;
            objComplaintRegister.CustDetId = ddlContactPerson.SelectedItem.Value;
            //objComplaintRegister.CustDetId = ddlContactPerson.SelectedItem.Value;
            objComplaintRegister.ItemCode = ddlItemType.SelectedItem.Value;
            objComplaintRegister.CRComplaintNature = txtNatureofComplaint.Text;
            objComplaintRegister.CRRootCause = txtRootCause.Text;
            objComplaintRegister.CRCorrectiveAction = txtCorrectiveAction.Text;
            objComplaintRegister.CRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objComplaintRegister.Cp_Id = lblCPID.Text;

            if (objComplaintRegister.ComplaintRegister_Save() == "Data Saved Successfully")
            {
                objComplaintRegister.ComplaintRegisterDetails_Delete(objComplaintRegister.CRId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objComplaintRegister.ItemCode = gvrow.Cells[2].Text;
                    objComplaintRegister.CRDetQty = gvrow.Cells[6].Text;
                    objComplaintRegister.CRDetSerialNo = gvrow.Cells[5].Text;
                    objComplaintRegister.CRComplaintNature = gvrow.Cells[8].Text;
                    objComplaintRegister.CRRootCause = gvrow.Cells[9].Text;
                    objComplaintRegister.CRCorrectiveAction = gvrow.Cells[10].Text;
                    objComplaintRegister.ComplaintRegisterDetails_Save();
                }
            }
            Services.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");
            #region For Assigning 
            try
            {

                Services.ServicesAssignments objServicesAssign = new Services.ServicesAssignments();
                Services.BeginTransaction();
                objServicesAssign.AssignTaskNo = txtAssignTaskNo.Text;
                objServicesAssign.CrId = objComplaintRegister.CRId;
                objServicesAssign.CrNo = txtEnquiryNoForAssign.Text;
                objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
                objServicesAssign.AssingDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
                objServicesAssign.DueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
                objServicesAssign.AssignRemarks = txtRemarksForAssingn.Text;
                objServicesAssign.AssignStatus = "New";
                Services.ComplaintRegister.CompalintRegisterStatus_Update(Services.ServicesStatus.Open, objComplaintRegister.CRId);

                if (btnAssignTask.Text == "Assign")
                {
                    MessageBox.Show(this, objServicesAssign.ServicesAssignments_Save());
                    Services.CommitTransaction();
                }
                else if (btnAssignTask.Text == "Re-Assign")
                {
                    MessageBox.Show(this, objServicesAssign.ServicesAssignments_Update());
                    Services.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblComplaintRegister.Visible = false;
                tblAssignTasks.Visible = false;
                //gvInterestedProducts.DataBind();
                //btnDelete.Attributes.Clear();
                btnAssignTask.Attributes.Clear();
                //gvComplaintRegister.DataBind();
                Services.ClearControls(this);
                Services.Dispose();
            }
            tblAssignTasks.Visible = false;

            #endregion


        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //btnDelete.Attributes.Clear();
            //gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
        }
    }
    #endregion

    #region Complaint Register Update
    private void ComplaintRegisterUpdate()
    {
        try
        {

            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

            //objComplaintRegister.CRId = gvComplaintRegister.SelectedRow.Cells[0].Text;

            objComplaintRegister.CRNo = txtCRNo.Text;
            objComplaintRegister.CRDate = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
            objComplaintRegister.CRCallType = ddlCallType.SelectedItem.Text;
            objComplaintRegister.CustId = ddlCustomerName.SelectedItem.Value;
            objComplaintRegister.CustUnitId = ddlUnitName.SelectedItem.Value;
            //objComplaintRegister.CustDetId = ddlContactPerson.SelectedItem.Value;
            objComplaintRegister.ItemCode = ddlItemType.SelectedItem.Value;
            objComplaintRegister.CRComplaintNature = txtNatureofComplaint.Text;
            objComplaintRegister.CRRootCause = txtRootCause.Text;
            objComplaintRegister.CRCorrectiveAction = txtCorrectiveAction.Text;
            objComplaintRegister.CRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objComplaintRegister.Cp_Id = lblCPID.Text;

            MessageBox.Show(this, objComplaintRegister.ComplaintRegister_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //btnDelete.Attributes.Clear();
            //gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
        }
    }
    #endregion

    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
        }
    }
    #endregion

    #region GridView Quotation Items Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["SerialNo"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["ItemTypeId"] = gvrow.Cells[7].Text;
                    dr["NatureofComplaint"] = gvrow.Cells[8].Text;
                    dr["RootCausedNotice"] = gvrow.Cells[9].Text;
                    dr["CorrectiveActionTaken"] = gvrow.Cells[10].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region GridView Quotation Items Row Editing
    protected void gvQuotationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["SerialNo"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["ItemTypeId"] = gvrow.Cells[7].Text;
                dr["NatureofComplaint"] = gvrow.Cells[8].Text;
                dr["RootCausedNotice"] = gvrow.Cells[9].Text;
                dr["CorrectiveActionTaken"] = gvrow.Cells[10].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {

                    ItemTypes_Fill();
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);

                    txtQuantity.Text = gvrow.Cells[6].Text;
                    txtNatureofComplaint.Text = gvrow.Cells[8].Text;
                    txtRootCause.Text = gvrow.Cells[9].Text;
                    txtCorrectiveAction.Text = gvrow.Cells[10].Text;
                    if (gvrow.Cells[5].Text != "-")
                    { txtSerialNo.Text = gvrow.Cells[5].Text; }
                    else
                    { txtSerialNo.Text = ""; }
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }

    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);

    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblComplaintRegister.Visible = false;
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
}
