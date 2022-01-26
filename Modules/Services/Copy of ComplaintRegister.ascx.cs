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
using vllib;

public partial class Modules_Services_ascxComplaintRegister : System.Web.UI.UserControl
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDontDelete.Text = "0";
            txtSerialNo.Attributes.Add("onkeyup", "javascript:Serialno();");
            CustomerMaster_Fill();
           // ItemTypes_Fill();
            FillBrand();
            FillItemCategory();
            EmployeeMaster_Fill();
            Region_Fill();
            IndustryType_Fill();
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblEmpIdHidden.Text = lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            tblComplaintRegister.Visible = false;
           
            if (Request.QueryString["srid"] != null)
            {
                btnNew_Click(sender, e);
               
               // SM.SalesOrder objSM = new SM.SalesOrder();
                Inventory.Delivery objDC = new Inventory.Delivery();
                objDC.DeliveryDetailsBySalesOrderId_Select2(Request.QueryString["srid"].ToString(), gvQuotationItems);
                SM.SalesOrder objSM = new SM.SalesOrder();
                objSM.SalesOrder_Select(Request.QueryString["srid"].ToString());
  
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                {
                    //    //txtCustomerName.Text = objSMCustomer.CustName;
                    //   // txtAddress.Text = objSMCustomer.Address;
                    //    //txtEmail.Text = objSMCustomer.Email;
                    //    //txtRegion.Text = objSMCustomer.RegName;
                    //    //txtPhone.Text = objSMCustomer.Phone;
                    //    //txtMobile.Text = objSMCustomer.Mobile;
                    ddlCallType.SelectedValue="Installation";
                    ddlCustomerName.SelectedValue = objSM.CustId;
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                } 
            }
                     
            if (Request.QueryString["crid"] != null)
            {
                btnNew_Click(sender, e);

                //ddlEnquiryNo.SelectedValue = Request.QueryString["crid"].ToString();
                Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

                if(objComplaintRegister.ComplaintRegister_Select(Request.QueryString["crid"].ToString())>0)
                {
             txtCRDate.Text = objComplaintRegister.CRDate;
            ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
            ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
            ddlCustomerName_SelectedIndexChanged(sender, e);
            ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
            ddlUnitName_SelectedIndexChanged(sender, e);
           ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
          // ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
          // ddlItemType_SelectedIndexChanged(sender, e);
           //ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
           txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
           txtRootCause.Text = objComplaintRegister.CRRootCause;
           txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
           ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;
          //objComplaintRegister.ComplaintRegisterDetails_Select(Request.QueryString["crid"].ToString(), gvQuotationItems);
           //objComplaintRegister.ComplaintRegisterDetails_Select1(Request.QueryString["crid"].ToString(), gvQuotationItems);
           objComplaintRegister.ComplaintRegisterDetails_Select3(Request.QueryString["crid"].ToString(), gvQuotationItems);


         

                }
               

                //ddlEnquiryNo.SelectedValue = Request.QueryString["crid"].ToString();
               

               
                
             
        //    ddlCallType.SelectedItem.Text = "installation";
          //  ddlCustomerName.SelectedValue = objSMCustomer.CustId;
            //ddlCustomerName_SelectedIndexChanged(sender, e);
            //ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
            //ddlUnitName_SelectedIndexChanged(sender, e);
           //ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
           //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
           //ddlItemType_SelectedIndexChanged(sender, e);
           //ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
           //txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
           //txtRootCause.Text = objComplaintRegister.CRRootCause;
           //txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
           //ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;
          //objComplaintRegister.ComplaintRegisterDetails_Select(Request.QueryString["crid"].ToString(), gvQuotationItems);
           //objComplaintRegister.ComplaintRegisterDetails_Select1(Request.QueryString["crid"].ToString(), gvQuotationItems);
          // objComplaintRegister.ComplaintRegisterDetails_Select3(Request.QueryString["crid"].ToString(), gvQuotationItems);

                
               // ddlEnquiryNo_SelectedIndexChanged(sender, e);
                // tblQuotationDetails.Visible = true;
            } 
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvComplaintRegister.SelectedRow.Cells[6].Text) && gvComplaintRegister.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                // btnSave.Visible = false;
                btnRefresh.Visible = false;

            }
            else
            {
                btnSave.Visible = true;
                btnRefresh.Visible = false;

            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;

        }
    }
    #endregion

    #region Region_Fill
    private void Region_Fill()
    {
        try
        {
            Masters.RegionalMaster.RegionalMaster_Select(ddlRegion);
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

    #region IndustryType_Fill
    private void IndustryType_Fill()
    {
        try
        {
            Masters.IndustryType.IndustryType_Select(ddlIndustryType);
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

    #region Fill Item Category
    private void FillItemCategory()
    {
        try
        {
            Masters.ItemCategory.ItemCategory_Select(ddlItemCategory);
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

    #region   Contact Fill
    private void Contact_Fill()
    {
        try
        {
            if (ddlCustomerName.SelectedIndex != -1)
            {
                SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlCustomerName.SelectedItem.Value);

            }

            //  SM.CustomerMaster.CustomerMasterDetails_Select(ddlBuyerContactPerson, "0");

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

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrandName);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            // HR.EmployeeMaster.EmployeeMaster_Select(ddlResponsiblePerson);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlCheckedBy);\
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpNameForAssign);
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

    #region Link Button lbtnCRNo_Click
    protected void lbtnCRNo_Click(object sender, EventArgs e)
    {
        CustomerMaster_Fill();
        tblComplaintRegister.Visible = false;
        LinkButton lbtnCRNo;
        lbtnCRNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCRNo.Parent.Parent;
        gvComplaintRegister.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

        if (objComplaintRegister.ComplaintRegister_Select(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;
            btnSave.Text = "Update";
            tblComplaintRegister.Visible = true;
            txtCRNo.Text = objComplaintRegister.CRNo;
            txtCRDate.Text = objComplaintRegister.CRDate;
            ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
            ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
            ddlCustomerName_SelectedIndexChanged(sender, e);
            ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
            ddlUnitName_SelectedIndexChanged(sender, e);
            ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
            ddlContactPerson_SelectedIndexChanged(sender, e);
            //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
            //ddlItemType_SelectedIndexChanged(sender, e);
       //     ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
            txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
            txtRootCause.Text = objComplaintRegister.CRRootCause;
            txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
            ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;
            objComplaintRegister.ComplaintRegisterDetails_Select(gvComplaintRegister.SelectedRow.Cells[0].Text, gvQuotationItems);
        }

    }
    #endregion

    #region ddlItemCategory_SelectedIndexChanged
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       // Masters.ItemMaster.ItemMaster_SelectForComplaint(ddlType,ddlBrandName.SelectedValue , ddlItemCategory.SelectedValue);
        Masters.ItemMaster.ItemMaster1_Select(ddlType, ddlItemCategory.SelectedValue);
        //FillItemType();
    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            gvComplaintRegister.SelectedIndex = -1;
            Services.ClearControls(this);

            txtCRNo.Text = Services.ComplaintRegister.ComplaintRegister_AutoGenCode();
            txtCRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Text = "Save";
            btnSave.Enabled = true;
            tblComplaintRegister.Visible = true;
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
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
        
            #region Customer_Save
            if (rbCustomerType.SelectedValue == "New") 
            {
                try
                {
                    Services.ComplaintRegister objMaster = new Services.ComplaintRegister();
                    Services.BeginTransaction();
                    objMaster.RegId = ddlRegion.SelectedItem.Value;
                    objMaster.CustName = txtCustomer.Text;

                    objMaster.CompName = string.Empty;

                    objMaster.ContactPerson = txtContactPerson.Text;
                    objMaster.Address = txtUnitAddress.Text;
                    objMaster.Phone = txtPhoneNo.Text;
                    objMaster.Mobile = txtMobile.Text;
                    objMaster.Fax = string.Empty;
                    objMaster.Email = txtEmail.Text;
                    objMaster.IndTypeId = ddlIndustryType.SelectedItem.Value;
                    objMaster.Website = string.Empty;
                    objMaster.PANNo = string.Empty;
                    objMaster.ECCNo = string.Empty;
                    objMaster.CSTNo = string.Empty;
                    objMaster.LocalSTNo = string.Empty;
                    objMaster.SplInsrs = string.Empty;
                    objMaster.DesgId = "0";
                    objMaster.IsNewOrExisting = string.Empty;
                    objMaster.CorpDesgId = "0";

                    if (objMaster.CustomerMaster_Save() == "Data Saved Successfully")
                    {
                        objMaster.CRNo = txtCRNo.Text;
                        objMaster.CRDate = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
                        objMaster.CRCallType = ddlCallType.SelectedItem.Text;
                        //objMaster.CustId = ddlCustomerName.SelectedItem.Value;
                        objMaster.CustUnitId = ddlUnitName.SelectedItem.Value;
                        objMaster.CustDetId = ddlContactPerson.SelectedItem.Value;
                        //objComplaintRegister.CustDetId = ddlContactPerson.SelectedItem.Value;
                        objMaster.ItemCode = ddlItemType.SelectedItem.Value;
                        objMaster.CRComplaintNature = txtNatureofComplaint.Text;
                        objMaster.CRRootCause = txtRootCause.Text;
                        objMaster.CRCorrectiveAction = txtCorrectiveAction.Text;
                        objMaster.CRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                        objMaster.Cp_Id = lblCPID.Text;

                        if (objMaster.ComplaintRegister_Save() == "Data Saved Successfully")
                        {
                            objMaster.ComplaintRegisterDetails_Delete(objMaster.CRId);
                            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                            {
                                objMaster.ItemCode = gvrow.Cells[2].Text;
                                objMaster.CRDetQty = gvrow.Cells[6].Text;
                                objMaster.CRDetSerialNo = gvrow.Cells[5].Text;
                                objMaster.CRComplaintNature = gvrow.Cells[8].Text;
                                objMaster.CRRootCause = gvrow.Cells[9].Text;
                                objMaster.CRCorrectiveAction = gvrow.Cells[10].Text;
                                objMaster.ComplaintRegisterDetails_Save();
                            }
                        }
                        Services.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                        tblComplaintRegister.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Services.RollBackTransaction();
                    MessageBox.Show(this, ex.Message);
                }
                finally
                {
                    btnDelete.Attributes.Clear();
                    gvComplaintRegister.DataBind();
                    Services.ClearControls(this);
                    Services.Dispose();
                    gvQuotationItems.DataSource = null;
                    gvQuotationItems.DataBind();
                    gvComplaintRegister.DataBind();
                }
               
            }

            #endregion
            else
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
            tblComplaintRegister.Visible = false;


        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
            gvComplaintRegister.DataBind();
        }
    }
    }
    #endregion

    #region Complaint Register Update
    private void ComplaintRegisterUpdate()
    {
        try
        {

            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

            objComplaintRegister.CRId = gvComplaintRegister.SelectedRow.Cells[0].Text;

            objComplaintRegister.CRNo = txtCRNo.Text;
            objComplaintRegister.CRDate = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
            objComplaintRegister.CRCallType = ddlCallType.SelectedItem.Text;
            objComplaintRegister.CustId = ddlCustomerName.SelectedItem.Value;
            objComplaintRegister.CustUnitId = ddlUnitName.SelectedItem.Value;
            objComplaintRegister.CustDetId = ddlContactPerson.SelectedItem.Value;
            objComplaintRegister.ItemCode = "0";
            objComplaintRegister.CRComplaintNature = txtNatureofComplaint.Text;
            objComplaintRegister.CRRootCause = txtRootCause.Text;
            objComplaintRegister.CRCorrectiveAction = txtCorrectiveAction.Text;
            objComplaintRegister.CRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objComplaintRegister.Cp_Id = lblCPID.Text;
            //MessageBox.Show(this, objComplaintRegister.ComplaintRegister_Update());


            if (objComplaintRegister.ComplaintRegister_Update() == "Data Updated Successfully")
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
            MessageBox.Show(this, "Data Updated Successfully");
            tblComplaintRegister.Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            try
            {
                Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

                if (objComplaintRegister.ComplaintRegister_Select(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
                {
                    tblComplaintRegister.Visible = true;
                    btnRefresh.Visible = true;
                    btnSave.Visible = true;
                    btnClose.Visible = true;
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;

                    txtCRNo.Text = objComplaintRegister.CRNo;
                    txtCRDate.Text = objComplaintRegister.CRDate;
                    ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
                    ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                    ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
                    ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
                    ddlContactPerson_SelectedIndexChanged(sender, e);
                    //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
                    //ddlItemType_SelectedIndexChanged(sender, e);
              //      ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
                    txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
                    txtRootCause.Text = objComplaintRegister.CRRootCause;
                    txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
                    ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;

                    objComplaintRegister.ComplaintRegisterDetails_Select(gvComplaintRegister.SelectedRow.Cells[0].Text, gvQuotationItems);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            try
            {
                Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
                objComplaintRegister.CRId = gvComplaintRegister.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objComplaintRegister.ComplaintRegister_Delete());
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvComplaintRegister.DataBind();
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region GridView Quotation Products Row Databound
    protected void gvQuotationProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

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

    #region GridView gvComplaintRegister_RowDataBound
    protected void gvComplaintRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
           
        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "CR Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
    }
    #endregion

    #region DropDownList Symbols Select Index Changed
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            imgFromDate.Visible = true;
            ceSearchFrom.Enabled = true;
            MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvComplaintRegister.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvComplaintRegister.DataBind();
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
               
                txtItemName.Text = objMaster.ItemName;
              //  txtColor.Text = objMaster.Color;
                if (lblDontDelete.Text == "1") 
                {
                    ddlBrandName.SelectedValue = objMaster.BrandName;
                    ddlItemCategory.SelectedValue = objMaster.ItemCategoryId;
                    ddlItemCategory_SelectedIndexChanged(sender, e);
                    ddlType.SelectedValue = objMaster.ItemTypeId;
                   
                }
                
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlItemType.SelectedValue);

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

    #region Assign Button Click
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        tblComplaintRegister.Visible = false;
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            try
            {
                txtAssignTaskNo.Text = Services.ServicesAssignments.ServicesAssignments_AutoGenCode();
                Services.ServicesAssignments objServicesAssign = new Services.ServicesAssignments();
                if (objServicesAssign.ServicesAssignments_Select(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
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
                    txtAssignDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());
                    txtDueDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());

                    txtEnquiryNoForAssign.Text = objServicesAssign.CrNo;
                    txtEnquiryDateForAssign.Text = objServicesAssign.CrDate;
                    ddlEmpNameForAssign.SelectedValue = objServicesAssign.EmpId;
                    txtAssignDate.Text = objServicesAssign.AssingDate;
                    txtDueDate.Text = objServicesAssign.DueDate;
                    txtRemarksForAssingn.Text = objServicesAssign.AssignRemarks;
                    ddlEmpNameForAssign_SelectedIndexChanged(sender, e);
                    //SM.CustomerMaster objCustomerMast = new SM.CustomerMaster();
                    //if ((objCustomerMast.CustomerMaster_Select(objCustomerMast.CustId)) > 0)
                    //{
                    //    txtCustomerNameForAssingn.Text = objCustomerMast.CustName;
                    //    txtCustomerEmailForAssingn.Text = objCustomerMast.CustCorpEmail;
                    //}
                    Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                    if ((objServicesCustomer.CustomerMaster_Select(objServicesAssign.CustId)) > 0)
                    {
                        txtCustomerNameForAssingn.Text = objServicesCustomer.CompName;
                        txtCustomerEmailForAssingn.Text = objServicesCustomer.Email;
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Re-Assign this Enquiry?');");
                }
                else
                {
                    Services.ComplaintRegister objServices = new Services.ComplaintRegister();
                    if (objServices.ComplaintRegister_Select(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
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
                        txtAssignDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());
                        txtDueDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());

                        txtEnquiryNoForAssign.Text = objServices.CRNo;
                        txtEnquiryDateForAssign.Text = objServices.CRDate;

                        //SM.CustomerMaster objCustomerMast = new SM.CustomerMaster();
                        //if ((objCustomerMast.CustomerMaster_Select(objCustomerMast.CustId)) > 0)
                        //{
                        //    txtCustomerNameForAssingn.Text = objCustomerMast.CustName;
                        //    txtCustomerEmailForAssingn.Text = objCustomerMast.CustCorpEmail;
                        //}

                       Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                       if ((objServicesCustomer.CustomerMaster_Select(objServices.CustId)) > 0)
                       {
                           txtCustomerNameForAssingn.Text = objServicesCustomer.CustName;
                           txtCustomerEmailForAssingn.Text = objServicesCustomer.Email;
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
                btnDelete.Attributes.Clear();
                Services.Dispose();
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
        try
        {

            Services.ServicesAssignments objServicesAssign = new Services.ServicesAssignments();
            Services.BeginTransaction();
            objServicesAssign.AssignTaskNo = txtAssignTaskNo.Text;
            objServicesAssign.CrId = gvComplaintRegister.SelectedRow.Cells[0].Text;
            objServicesAssign.CrNo = txtEnquiryNoForAssign.Text;
            objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
            objServicesAssign.AssingDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objServicesAssign.DueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objServicesAssign.AssignRemarks = txtRemarksForAssingn.Text;
            objServicesAssign.AssignStatus = "New";
            Services.ComplaintRegister.CompalintRegisterStatus_Update(Services.ServicesStatus.Open, gvComplaintRegister.SelectedRow.Cells[0].Text);
            objServicesAssign.Cp_Id =lblCPID.Text;

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
            btnDelete.Attributes.Clear();
            btnAssignTask.Attributes.Clear();
            gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
        }
        tblAssignTasks.Visible = false;
    }
    #endregion

    #region Button Cancel Task
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {

        tblAssignTasks.Visible = false;
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

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtNatureofComplaint.Text == string.Empty)
        {
            txtNatureofComplaint.Text= "-";
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
                        dr["ItemType"] = ddlType.SelectedItem.Text;
                        dr["ItemCode"] = ddlItemType.SelectedItem.Value;
                        dr["ItemName"] = ddlItemType.SelectedItem.Text;
                        dr["ItemTypeId"] = ddlType.SelectedItem.Value;
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

        if (gvQuotationItems.Rows.Count > 0)
        {
            if (gvQuotationItems.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlItemType.SelectedItem.Value)
                    {
                        gvQuotationItems.DataSource = QuotationItems;
                        gvQuotationItems.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

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
        //txtItemCategory.Text = string.Empty;//
        //txtItemSubCategory.Text = string.Empty;//
        txtColor.Text = string.Empty;//
        //txtBrand.Text = string.Empty;//
        txtSerialNo.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtCorrectiveAction.Text = string.Empty;
        txtNatureofComplaint.Text = string.Empty;
        txtRootCause.Text = string.Empty;
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
                    lblDontDelete.Text = "1";
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

    #region RbCustomerType_Changed
    protected void rbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCustomerType.SelectedValue == "New")
        {
            txtCustomer.Visible = true;
            ddlCustomerName.Visible = false;
            ddlUnitName.Visible = false;
            //RequiredFieldValidator3.ControlToValidate = "txtCustomer";
            txtUnitName.Visible = true;
            txtContactPerson.Visible = true;
            txtContactPerson.ReadOnly = false;
            txtRegion.ReadOnly = false;
            ddlContactPerson.Visible = false;
            txtIndustryType.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtMobile.ReadOnly = false;
            txtPhoneNo.ReadOnly = false;
            txtUnitAddress.ReadOnly = false;
            lblSelect.Visible = false;
            ddlSelect.Visible = false;
            txtIndustryType.Visible = false;
            txtRegion.Visible = false;
            ddlRegion.Visible = true;
            ddlIndustryType.Visible = true;
            Label30.Visible = true;         //Region
            Label32.Visible = true;         //Industry Type
            RequiredFieldValidator11.Visible = true;
            RequiredFieldValidator12.Visible = true;
            lblUnitAddress.Text = "Address";
            lblInitName.Visible = false;
            txtUnitName.Visible = false;
            lblSearch.Enabled = false;
            txtSearchModel.Enabled = false;
            btnSearchGo.Enabled = false;
        }
        else if (rbCustomerType.SelectedValue == "Existing")
        {
            txtCustomer.Visible = false;
            ddlCustomerName.Visible = true;
            ddlUnitName.Visible = true;
            //RequiredFieldValidator3.ControlToValidate = "txtCustomer";
            txtUnitName.Visible = false;
            txtContactPerson.Visible = false;
            ddlContactPerson.Visible = true ;
            txtContactPerson.ReadOnly = true;
            txtRegion.ReadOnly = true;
            txtIndustryType.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtMobile.ReadOnly = true;
            txtPhoneNo.ReadOnly = true;
            txtUnitAddress.ReadOnly = true;
            lblSelect.Visible = false;
            ddlSelect.Visible = false;
            txtIndustryType.Visible = true;
            txtRegion.Visible = true;
            ddlRegion.Visible = false;
            ddlIndustryType.Visible = false;
            Label30.Visible = true;         //Region
            Label32.Visible = true;         //Industry Type
            RequiredFieldValidator11.Visible = true;
            RequiredFieldValidator12.Visible = true;
            lblUnitAddress.Text = "Unit Address";
            lblInitName.Visible = true;


            lblSearch.Enabled = true;
            txtSearchModel.Enabled = true;
            btnSearchGo.Enabled = true;

        }
        else if (rbCustomerType.SelectedValue == "PO")
        {
            lblSelect.Text = "PO No";
            lblSelect.Visible = true;
            try
            {
                SM.SalesOrder.SalesOrder_Select(ddlSelect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                SM.Dispose();
            }
            ddlSelect.Visible = true;
            txtCustomer.Visible = false;
            ddlCustomerName.Visible = true;
            ddlUnitName.Visible = true;
            //RequiredFieldValidator3.ControlToValidate = "txtCustomer";
            txtUnitName.Visible = false;
            txtContactPerson.Visible = false;
            txtContactPerson.ReadOnly = true;
            txtRegion.ReadOnly = true;
            txtIndustryType.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtMobile.ReadOnly = true;
            txtPhoneNo.ReadOnly = true;
            txtUnitAddress.ReadOnly = true;
            ddlContactPerson.Visible = true;
            txtIndustryType.Visible = true;
            txtRegion.Visible = true;
            ddlRegion.Visible = false;
            ddlIndustryType.Visible = false;
            Label30.Visible = true;         //Region
            Label32.Visible = true;         //Industry Type
            RequiredFieldValidator11.Visible = true;
            RequiredFieldValidator12.Visible = true;
            lblUnitAddress.Text = "Unit Address";
            lblInitName.Visible = true;
            lblSearch.Enabled = false;
            txtSearchModel.Enabled = false;
            btnSearchGo.Enabled = false;

        }

        else if (rbCustomerType.SelectedValue == "DC")
        {
            lblSelect.Text = "DC No";
            lblSelect.Visible = true;
            try
            {
                Inventory.Delivery.Delivery_Select(ddlSelect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                Inventory.Dispose();
            }
            ddlSelect.Visible = true;
            txtCustomer.Visible = false;
            ddlCustomerName.Visible = true;
            ddlUnitName.Visible = true;
            //RequiredFieldValidator3.ControlToValidate = "txtCustomer";
            txtUnitName.Visible = false;
            txtContactPerson.Visible = false;
            txtContactPerson.ReadOnly = true;
            txtRegion.ReadOnly = true;
            txtIndustryType.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtMobile.ReadOnly = true;
            txtPhoneNo.ReadOnly = true;
            txtUnitAddress.ReadOnly = true;
            ddlContactPerson.Visible = true;
            txtIndustryType.Visible = true;
            txtRegion.Visible = true;
            ddlRegion.Visible = false;
            ddlIndustryType.Visible = false;
            Label30.Visible = true;         //Region
            Label32.Visible = true;         //Industry Type
            RequiredFieldValidator11.Visible = true;
            RequiredFieldValidator12.Visible = true;
            lblUnitAddress.Text = "Unit Address";
            lblInitName.Visible = true;
            lblSearch.Enabled = false;
            txtSearchModel.Enabled = false;
            btnSearchGo.Enabled = false;
        }

        else if (rbCustomerType.SelectedValue == "Invoice")
        {
            lblSelect.Text = "Invoice No";
            lblSelect.Visible = true;
            try
            {
                SCM.Indent.Indent_Select(ddlSelect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                SCM.Dispose();
            }
            ddlSelect.Visible = true;
            txtCustomer.Visible = false;
            ddlCustomerName.Visible = true;
            ddlUnitName.Visible = true;
            //RequiredFieldValidator3.ControlToValidate = "txtCustomer";
            txtUnitName.Visible = false;
            txtContactPerson.Visible = false;
            txtContactPerson.ReadOnly = true;
            txtRegion.ReadOnly = true;
            txtIndustryType.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtMobile.ReadOnly = true;
            txtPhoneNo.ReadOnly = true;
            txtUnitAddress.ReadOnly = true;
            ddlContactPerson.Visible = true;
            txtIndustryType.Visible = true;
            txtRegion.Visible = true;
            ddlRegion.Visible = false;
            ddlIndustryType.Visible = false;
            Label30.Visible = true;         //Region
            Label32.Visible = true;         //Industry Type
            RequiredFieldValidator11.Visible = true;  //Region
            RequiredFieldValidator12.Visible = true;   //Industry Type
            lblUnitAddress.Text = "Unit Address";
            lblInitName.Visible = true;
            lblSearch.Enabled = false;
            txtSearchModel.Enabled = false;
            btnSearchGo.Enabled = false;

        }
    }

    #endregion

    #region Item SubCategory_selected changed 
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_SelectForComplaint(ddlItemType, ddlBrandName.SelectedValue, ddlType.SelectedValue);
    }
    #endregion 

    # region DdlSelectedIndexChanged 
    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCustomerType.SelectedValue == "PO")
        { 
            try
            {
            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrder_Select(ddlSelect.SelectedItem.Value) > 0)
            {          
                ddlCustomerName.SelectedValue = objSO.CustId;
                ddlCustomerName_SelectedIndexChanged(sender,e);                              
                objSO.SalesOrderDetails_Select(ddlSelect.SelectedItem.Value, gvOrderAcceptanceItems);
                gvItmDetails.DataBind();
                gvDeliveryChallanItems.DataBind();
                gvOrderAcceptanceItems.DataBind();
                ddlCustomerName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            SM.Dispose();
        }
        }
        else if (rbCustomerType.SelectedValue == "DC") 
        {
            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrder_SelectForComplaint(ddlSelect.SelectedItem.Value) > 0)
            {
                ddlCustomerName.SelectedValue = objSO.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                Inventory.Delivery objDelivery = new Inventory.Delivery();
                if (objDelivery.Delivery_Select(ddlSelect.SelectedItem.Value) > 0)
                {
                    objDelivery.DeliveryDetails_SelectInvoice(ddlSelect.SelectedItem.Value, gvDeliveryChallanItems);
                    gvItmDetails.DataBind();
                    gvDeliveryChallanItems.DataBind();
                    gvOrderAcceptanceItems.DataBind();
                }
                //objSO.SalesOrderDetails_Select(ddlSelect.SelectedItem.Value, gvOrderAcceptanceItems);
                ddlCustomerName.Enabled = false;
            }
            
        }
        else if (rbCustomerType.SelectedValue == "Invoice")
        {
            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrderInvoice_SelectForComplaint(ddlSelect.SelectedItem.Value) > 0)
            {
                ddlCustomerName.SelectedValue = objSO.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                //if (objDelivery.Delivery_Select(ddlSelect.SelectedItem.Value) > 0)
                //{
                //    objDelivery.DeliveryDetails_SelectInvoice(ddlSelect.SelectedItem.Value, gvDeliveryChallanItems);
                //}               
                objInventory.SalesInvoiceDetails_Select(ddlSelect.SelectedItem.Value, gvItmDetails);
                gvItmDetails.DataBind();
                gvDeliveryChallanItems.DataBind();
                gvOrderAcceptanceItems.DataBind();
                //objSO.SalesOrderDetails_Select(ddlSelect.SelectedItem.Value, gvOrderAcceptanceItems);
                ddlCustomerName.Enabled = false;
            }

        }
    }
    #endregion

    #region GvOrder Acceptance RowDataBound
    protected void gvOrderAcceptanceItems_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[6].Text))).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = false;
            //  e.Row.Cells[12].Visible = false;
        }
    }
    #endregion

    #region GVItmDetails_DataBound
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        //}
    }
    #endregion

    #region gvOrderAcceptanceItems_RowEditing
    protected void gvOrderAcceptanceItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Currency");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Priority");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);


        if (gvOrderAcceptanceItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Currency"] = gvrow.Cells[8].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["Specifications"] = gvrow.Cells[9].Text;
                dr["Remarks"] = gvrow.Cells[10].Text;
                dr["Priority"] = gvrow.Cells[11].Text;
                dr["DeliveryDate"] = gvrow.Cells[12].Text;
                dr["Room"] = gvrow.Cells[13].Text;
                dr["Price"] = gvrow.Cells[14].Text;

                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvOrderAcceptanceItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ItemTypes_Fill();
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    lblDontDelete.Text = "1";
                    ddlItemType_SelectedIndexChanged(sender, e);                    
                    gvOrderAcceptanceItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvOrderAcceptanceItems.DataSource = SalesOrderItems;
        gvOrderAcceptanceItems.DataBind();
    }
    #endregion

    #region gvOrderAcceptanceItems_RowDeleting
    protected void gvOrderAcceptanceItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvOrderAcceptanceItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Currency");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Priority");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);

        if (gvOrderAcceptanceItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[8].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Specifications"] = gvrow.Cells[9].Text;
                    dr["Remarks"] = gvrow.Cells[10].Text;
                    dr["Priority"] = gvrow.Cells[11].Text;
                    dr["DeliveryDate"] = gvrow.Cells[12].Text;
                    dr["Room"] = gvrow.Cells[13].Text;
                    dr["Price"] = gvrow.Cells[14].Text;

                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvOrderAcceptanceItems.DataSource = SalesOrderItems;
        gvOrderAcceptanceItems.DataBind();
    }
    #endregion

    #region gvDeliveryChallanItems_RowEditing
    protected void gvDeliveryChallanItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("DC No");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SPPrice");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Rate");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Priority");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Room");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Price");
        DeliveryItems.Columns.Add(col);


        if (gvDeliveryChallanItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
            {
                DataRow dr = DeliveryItems.NewRow();
                dr["DC No"] = gvrow.Cells[2].Text;
                dr["ItemCode"] = gvrow.Cells[3].Text;
                dr["ModelNo"] = gvrow.Cells[4].Text;
                dr["ItemName"] = gvrow.Cells[5].Text;
                dr["UOM"] = gvrow.Cells[6].Text;
                dr["Quantity"] = gvrow.Cells[7].Text;
                dr["Rate"] = gvrow.Cells[8].Text;
                dr["SPPrice"] = gvrow.Cells[9].Text;
                dr["DeliveryDate"] = gvrow.Cells[10].Text;
                
                DeliveryItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvDeliveryChallanItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ItemTypes_Fill();
                    ddlItemType.SelectedValue = gvrow.Cells[3].Text;
                    txtQuantity.Text = gvrow.Cells[7].Text;
                    lblDontDelete.Text = "1";
                    ddlItemType_SelectedIndexChanged(sender, e);
                    gvDeliveryChallanItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvDeliveryChallanItems.DataSource = DeliveryItems;
        gvDeliveryChallanItems.DataBind();

    }
    #endregion

    #region gvItmDetails_RowEditing
    protected void gvItmDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable InvoiceItems = new DataTable();
        DataColumn col = new DataColumn();
        
        col = new DataColumn("ItemCode");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("UOM");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("SPPrice");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Rate");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Vat");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("CST");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Excise");
        InvoiceItems.Columns.Add(col);   
       col = new DataColumn("DeliveryDate");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Room");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Price");
        InvoiceItems.Columns.Add(col);


        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                DataRow dr = InvoiceItems.NewRow();
                
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Vat"] = gvrow.Cells[8].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["SPPrice"] = gvrow.Cells[12].Text;
                dr["CST"] = gvrow.Cells[9].Text;
                dr["Excise"] = gvrow.Cells[10].Text;
                dr["DeliveryDate"] = gvrow.Cells[13].Text;

                InvoiceItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvItmDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ItemTypes_Fill();
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    lblDontDelete.Text = "1";
                    ddlItemType_SelectedIndexChanged(sender, e);
                    gvItmDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvItmDetails.DataSource = InvoiceItems;
        gvItmDetails.DataBind();
    }
    #endregion

    #region gvDeliveryChallanItems_RowDeleting
    protected void gvDeliveryChallanItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvDeliveryChallanItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("DC No");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SPPrice");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Rate");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Priority");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Room");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Price");
        DeliveryItems.Columns.Add(col);

        if (gvDeliveryChallanItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = gvrow.Cells[2].Text;
                    dr["ItemCode"] = gvrow.Cells[3].Text;
                    dr["ModelNo"] = gvrow.Cells[4].Text;
                    dr["ItemName"] = gvrow.Cells[5].Text;
                    dr["UOM"] = gvrow.Cells[6].Text;
                    dr["Quantity"] = gvrow.Cells[7].Text;                    
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["SPPrice"] = gvrow.Cells[9].Text;                    
                    dr["DeliveryDate"] = gvrow.Cells[10].Text;
                    
                    DeliveryItems.Rows.Add(dr);
                }
            }
        }
        gvDeliveryChallanItems.DataSource = DeliveryItems;
        gvDeliveryChallanItems.DataBind();
    }
#endregion

   #region gvItmDetails_RowDeleting
    protected void gvItmDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItmDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable InvoiceItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("UOM");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("SPPrice");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Rate");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Vat");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("CST");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Excise");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Room");
        InvoiceItems.Columns.Add(col);
        col = new DataColumn("Price");
        InvoiceItems.Columns.Add(col);


        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = InvoiceItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Vat"] = gvrow.Cells[8].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["SPPrice"] = gvrow.Cells[12].Text;
                    dr["CST"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["DeliveryDate"] = gvrow.Cells[13].Text;
                    InvoiceItems.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = InvoiceItems;
        gvItmDetails.DataBind();
    }
   #endregion

    #region gvDeliveryChallanItems_RowDataBound
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
    #endregion
    protected void ddlBrandName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {

        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource1";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
}