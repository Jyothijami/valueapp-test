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

public partial class Modules_Services_SparesQuotation : System.Web.UI.Page
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            //txtQuotationDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtDDDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            Yantra.Authentication.Privilege_Check(this);
            //lblEmpIdHidden.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            btnRegret.Attributes.Add("onclick", "return confirm('Are you sure you want to Regret this Quotation !');");
            btnForApproveHidden.Style.Add("display", "none");
            EnquiryMaster_Fill();
            DeliveryType_Fill();
            //ItemTypes_Fill();
            CurrencyType_Fill();
            EmployeeMaster_Fill();
          

            if (Request.QueryString["crid"] != null)
            {
                btnNew_Click(sender, e);

                ddlEnquiryNo.SelectedValue = Request.QueryString["crid"].ToString();
                ddlEnquiryNo_SelectedIndexChanged(sender, e);
                tblQuotationDetails.Visible = true;
            }
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvQuotationDetails.SelectedRow.Cells[7].Text) && gvQuotationDetails.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnRegret.Visible = false;
                btnSave.Visible = false;
                btnEdit.Visible = false;
                btnRefresh.Visible = false;
                //btnPrint.Visible = true;
                btnSend.Visible = true;
                btnRevise.Visible = true;

            }
            else
            {
                btnApprove.Visible = true;
                btnRegret.Visible = true;
                btnSave.Visible = true;
                btnEdit.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
                btnSend.Visible = false;
                btnRevise.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnEdit.Visible = false;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnRegret.Visible = false;
            //btnPrint.Visible = false;
            btnSend.Visible = false;
            btnRevise.Visible = false;
        }

        //if (btnApprove.Visible == false)
        //{
        //Panel1.Visible = false;
        //}
        //else
        //{
        //    Panel1.Visible = true;
        //}
    }
    #endregion

    #region Enquiry Master Fill
    private void EnquiryMaster_Fill()
    {
        try
        {
            Services.ComplaintRegister.ComplaintRegister_Select(ddlEnquiryNo);
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
    #endregion

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDespatchMode);
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

    #region Currency Type Fill
    private void CurrencyType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlCurrencyType);
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

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemType.ItemType_Select(ddlItemType);
            //Services.SparesQuotation.SparesQuotationItemTypes_Select(ddlEnquiryNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();            
            //SM.Dispose();
        }
    }
    #endregion

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
            //Services.SparesQuotation.SparesQuotationItemNames_Select(ddlEnquiryNo.SelectedItem.Value, ddlItemType.SelectedItem.Value, ddlItemName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            //SM.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlResponsiblePerson);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlCheckedBy);
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

    #region Link Button QuotationNo_Click
    protected void lbtnQuotationNo_Click(object sender, EventArgs e)
    {
        btnRevise.Text = "Modify";
        tblQuotationDetails.Visible = false;
        tblFollowUp.Visible = false;
        btnFollowUp.Visible = btnEdit.Visible = btnSlaesOrder.Visible = true;
       

        LinkButton lbtnQuotationNo;
        lbtnQuotationNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnQuotationNo.Parent.Parent;
        gvQuotationDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        lblQuotIdHiddenForFollowUp.Text = gvRow.Cells[0].Text;
        Services.SparesQuotation objSM = new Services.SparesQuotation();
        if (objSM.SparesQuotation_Select(gvQuotationDetails.SelectedRow.Cells[0].Text) > 0)
        {

           

            btnSave.Enabled = false;
            btnSave.Text = "Update";
            tblQuotationDetails.Visible = true;

            txtQuotationNo.Text = objSM.SparesQuotNo;
            txtQuotationDate.Text = objSM.SparesQuotDate;
            ddlEnquiryNo.SelectedValue = objSM.CrId;
            txtDelivery.Text = objSM.SparesQuotDelivery;
            txtPaymentTerms.Text = objSM.SparesQuotPayTerms;
            txtPackingCharges.Text = objSM.SparesQuotPackCharges;
            txtExciseDuty.Text = objSM.SparesQuotExcise;
            txtCST.Text = objSM.SparesQuotCST;
            txtVAT.Text = objSM.SparesQuotVAT;
            txtPrice.Text = objSM.Price;
            ddlDespatchMode.SelectedValue = objSM.DespmId;
            txtGuarantee.Text = objSM.SparesQuotGuarantee;
            txtTransCharges.Text = objSM.SparesQuotTransCharges;
            txtInsurance.Text = objSM.SparesQuotInsurance;
            txtErrection.Text = objSM.SparesQuotErrec;
            txtJurisdiction.Text = objSM.SparesQuotJurisdiction;
            txtValidity.Text = objSM.SparesQuotValidity;
            txtInspection.Text = objSM.SparesQuotInspection;
            txtOtherSpecs.Text = objSM.SparesQuotOtherSpecs;
            //   objSM.QuotPOLog = txtpo.Text;
            ddlResponsiblePerson.SelectedValue = objSM.SparesQuotRespId;
            ddlSalesPerson.SelectedValue = objSM.SparesQuotSalespId;
            ddlPreparedBy.SelectedValue = objSM.SparesQuotPreparedBy;
            ddlCheckedBy.SelectedValue = objSM.SparesQuotCheckedBy;
            ddlApprovedBy.SelectedValue = objSM.SparesQuotApprovedBy;
            ddlCurrencyType.SelectedValue = objSM.SparesCurrencyId;
            chkIsExpectedOrder.Checked = objSM.IsExpectedOrder;
            if (gvQuotationDetails.SelectedRow.Cells[7].Text == "&nbsp;")
            {
                btnSlaesOrder.Enabled = false;
            }
            else
            {
                btnSlaesOrder.Enabled = true;
            }
            if (objSM.SparesQuotStatus == Services.ServicesStatus.Regret.ToString())
            {
                lblApprovedBy.Text = "Regreted By";
            }
            else
            {
                lblApprovedBy.Text = "Approved By";
            }
            objSM.SparesQuotationDetails_Select(gvQuotationDetails.SelectedRow.Cells[0].Text, gvQuotationItems);
            ddlEnquiryNo_SelectedIndexChanged(sender, e);
            ddlResponsiblePerson_SelectedIndexChanged(sender, e);

        }
    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //btnApprove.Visible = false;
        //btnSave.Visible = true;
        //btnRefresh.Visible = true;
        //btnPrint.Visible = false;
        //btnSend.Visible = false;

        btnFollowUp.Visible = btnEdit.Visible = btnSlaesOrder.Visible = false;
        gvQuotationDetails.SelectedIndex = -1;
        Services.ClearControls(this);
        gvQuotationItems.DataBind();
        gvEnquiryProducts.DataBind();
        txtQuotationNo.Text = Services.SparesQuotation.SparesQuotation_AutoGenCode();
        txtQuotationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        tblQuotationDetails.Visible = true;
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SparesQuotationSave();
        }
        else if (btnSave.Text == "Update")
        {
            SparesQuotationUpdate();
        }
    }
    #endregion

    #region SparesQuotationSave
    private void SparesQuotationSave()
    {
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                Services.SparesQuotation objsr = new Services.SparesQuotation();
                if (objsr.complaintrecord_isrecordexists(ddlEnquiryNo.SelectedItem.Value) > 0)
                {
                    MessageBox.Show(this, "Qutation  " + ddlEnquiryNo.SelectedItem.Text + " already prepared");
                    Yantra.Classes.General.ClearControls(this);
                    return;
                }
                Services.SparesQuotation objSM = new Services.SparesQuotation();
                Services.BeginTransaction();
               
                objSM.SparesQuotNo = txtQuotationNo.Text;
                objSM.SparesQuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objSM.CrId = ddlEnquiryNo.SelectedItem.Value;
                objSM.SparesQuotDelivery = txtDelivery.Text;
                objSM.SparesQuotPayTerms = txtPaymentTerms.Text;
                objSM.SparesQuotPackCharges = txtPackingCharges.Text;
                objSM.SparesQuotExcise = txtExciseDuty.Text;
                objSM.SparesQuotCST = txtCST.Text;
                objSM.SparesQuotVAT = txtVAT.Text;
                objSM.Price = txtPrice.Text;
                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.SparesQuotGuarantee = "0";
                objSM.SparesQuotTransCharges = txtTransCharges.Text;
                objSM.SparesQuotInsurance = "0";
                objSM.SparesQuotErrec = txtErrection.Text;
                objSM.SparesQuotJurisdiction = txtJurisdiction.Text;
                objSM.SparesQuotValidity = txtValidity.Text;
                objSM.SparesQuotInspection = txtInspection.Text;
                objSM.SparesQuotOtherSpecs = txtOtherSpecs.Text;
                //   objSM.QuotPOLog = txtpo.Text;
                objSM.SparesQuotRespId = ddlResponsiblePerson.SelectedItem.Value;
                objSM.SparesQuotSalespId = ddlSalesPerson.SelectedItem.Value;
                objSM.SparesQuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.SparesQuotCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.SparesQuotApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.SparesCurrencyId = ddlCurrencyType.SelectedItem.Value;
                objSM.IsExpectedOrder = chkIsExpectedOrder.Checked;


                if (objSM.SparesQuotation_Save() == "Data Saved Successfully")
                {
                    objSM.SparesQuotationDetails_Delete(objSM.SparesQuotId);
                    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                    {
                        objSM.SparesQuotDetItemCode = gvrow.Cells[2].Text;
                        objSM.SparesQuotDetQty = gvrow.Cells[6].Text;
                        objSM.SparesQuotRate = gvrow.Cells[7].Text;

                        objSM.SparesQuotationDetails_Save();
                    }



                    Services.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                     Services.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                 Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvQuotationDetails.DataBind();
                gvEnquiryProducts.DataBind();
                gvQuotationItems.DataBind();
                tblQuotationDetails.Visible = false;
                 Services.ClearControls(this);
                 Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region  SparesQuotationUpdate
    private void SparesQuotationUpdate()
    {
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                Services.SparesQuotation objSM = new Services.SparesQuotation();
                Services.BeginTransaction();


                objSM.SparesQuotId = gvQuotationDetails.SelectedRow.Cells[0].Text;
                objSM.SparesQuotNo = txtQuotationNo.Text;
                objSM.SparesQuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objSM.SparesQuotDelivery = txtDelivery.Text;
                objSM.SparesQuotPayTerms = txtPaymentTerms.Text;
                objSM.SparesQuotPackCharges = txtPackingCharges.Text;
                objSM.SparesQuotExcise = txtExciseDuty.Text;
                objSM.SparesQuotCST = txtCST.Text;
                objSM.SparesQuotVAT = txtVAT.Text;
                objSM.Price = txtPrice.Text;
                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.SparesQuotGuarantee ="0";
                objSM.SparesQuotTransCharges = txtTransCharges.Text;
                objSM.SparesQuotInsurance = "0";
                objSM.SparesQuotErrec = txtErrection.Text;
                objSM.SparesQuotJurisdiction = txtJurisdiction.Text;
                objSM.SparesQuotValidity = txtValidity.Text;
                objSM.SparesQuotInspection = txtInspection.Text;
                objSM.SparesQuotOtherSpecs = txtOtherSpecs.Text;
                //   objSM.QuotPOLog = txtpo.Text;
                objSM.SparesQuotRespId = ddlResponsiblePerson.SelectedItem.Value;
                objSM.SparesQuotSalespId = ddlSalesPerson.SelectedItem.Value;
                objSM.SparesQuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.SparesQuotCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.SparesQuotApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.SparesCurrencyId = ddlCurrencyType.SelectedItem.Value;
                objSM.IsExpectedOrder = chkIsExpectedOrder.Checked;
                              

                if (objSM.SparesQuotation_Update() == "Data Updated Successfully")
                {
                    objSM.SparesQuotationDetails_Delete(objSM.SparesQuotId);
                    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                    {
                        objSM.SparesQuotDetItemCode = gvrow.Cells[2].Text;
                        objSM.SparesQuotDetQty = gvrow.Cells[6].Text;
                        objSM.SparesQuotRate = gvrow.Cells[7].Text;
                        objSM.SparesQuotationDetails_Save();
                    }
                    Services.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    Services.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                btnDelete.Attributes.Clear();
                gvQuotationDetails.DataBind();
                gvEnquiryProducts.DataBind();
                gvQuotationItems.DataBind();
                tblQuotationDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            try
            {
                Services.SparesQuotation objSM = new Services.SparesQuotation();
                if (objSM.SparesQuotation_Select(gvQuotationDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblQuotationDetails.Visible = true;

                    txtQuotationNo.Text = objSM.SparesQuotNo;
                    txtQuotationDate.Text = objSM.SparesQuotDate;
                    ddlEnquiryNo.SelectedValue = objSM.CrId;
                    txtDelivery.Text = objSM.SparesQuotDelivery;
                    txtPaymentTerms.Text = objSM.SparesQuotPayTerms;
                    txtPackingCharges.Text = objSM.SparesQuotPackCharges;
                    txtExciseDuty.Text = objSM.SparesQuotExcise;
                    txtCST.Text = objSM.SparesQuotCST;
                    txtVAT.Text = objSM.SparesQuotVAT;
                    txtPrice.Text = objSM.Price;
                    ddlDespatchMode.SelectedValue = objSM.DespmId;
                    txtGuarantee.Text = objSM.SparesQuotGuarantee;
                    txtTransCharges.Text = objSM.SparesQuotTransCharges;
                    txtInsurance.Text = objSM.SparesQuotInsurance;
                    txtErrection.Text = objSM.SparesQuotErrec;
                    txtJurisdiction.Text = objSM.SparesQuotJurisdiction;
                    txtValidity.Text = objSM.SparesQuotValidity;
                    txtInspection.Text = objSM.SparesQuotInspection;
                    txtOtherSpecs.Text = objSM.SparesQuotOtherSpecs;
                    //   objSM.QuotPOLog = txtpo.Text;
                    ddlResponsiblePerson.SelectedValue = objSM.SparesQuotRespId;
                    ddlSalesPerson.SelectedValue = objSM.SparesQuotSalespId;
                    ddlPreparedBy.SelectedValue = objSM.SparesQuotPreparedBy;
                    ddlCheckedBy.SelectedValue = objSM.SparesQuotCheckedBy;
                    ddlApprovedBy.SelectedValue = objSM.SparesQuotApprovedBy;
                    ddlCurrencyType.SelectedValue = objSM.SparesCurrencyId;
                    chkIsExpectedOrder.Checked = objSM.IsExpectedOrder;
                    if (objSM.SparesQuotStatus == Services.ServicesStatus.Regret.ToString())
                    {
                        lblApprovedBy.Text = "Regreted By";
                    }
                    else
                    {
                        lblApprovedBy.Text = "Approved By";
                    }
                   


                    objSM.SparesQuotationDetails_Select(gvQuotationDetails.SelectedRow.Cells[0].Text, gvQuotationItems);

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
                ddlEnquiryNo_SelectedIndexChanged(sender, e);
                ddlResponsiblePerson_SelectedIndexChanged(sender, e);
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
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            try
            {
                Services.SparesQuotation objSM = new Services.SparesQuotation();
                MessageBox.Show(this, objSM.SparesQuotation_Delete(gvQuotationDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvQuotationDetails.DataBind();
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

    #region Enquiry No. Select Index Changed
    protected void ddlEnquiryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.SparesQuotation objSM = new Services.SparesQuotation();
            if (objSM.CompalintRegister_Select(ddlEnquiryNo.SelectedItem.Value) > 0)
            {
                txtEnquiryDate.Text = objSM.CrDate;

                             
                ItemTypes_Fill();
                
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                //if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objSM.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = objSMCustomer.CustUnitName;
                //}
                if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSM.cust_unit_add;
                    
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;

                    txtUnitName.Text = objSM.cust_unit;
                    if (txtAddress.Text == "")
                    {
                        txtAddress.Text = objSMCustomer.Address;
                        txtUnitName.Text = "--";
                    }
                }
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
    #endregion

    #region GridView Enquiry Products Row Databound
    protected void gvEnquiryProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

            //if (tdEMDHeader.Visible == false)
            //{
            //    e.Row.Cells[7].Visible = false;
            //    e.Row.Cells[8].Visible = false;
            //    e.Row.Cells[9].Visible = false;
            //    e.Row.Cells[10].Visible = false;
            //}
        }
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
        gvQuotationItems.DataBind();
        gvEnquiryProducts.DataBind();
        if (Request.QueryString["crid"] != null)
        {
            btnNew_Click(sender, e);
            ddlEnquiryNo.SelectedValue = Request.QueryString["crid"].ToString();
            ddlEnquiryNo_SelectedIndexChanged(sender, e);
            tblQuotationDetails.Visible = true;
        }
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblQuotationDetails.Visible = false;
    } 
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
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
                        dr["ItemCode"] = ddlItemName.SelectedItem.Value;
                        dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["ItemName"] = ddlItemName.SelectedItem.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtQunatity.Text;
                        dr["Rate"] = txtRate.Text;
                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["ItemTypeId"] = gvrow.Cells[9].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {

                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["ItemTypeId"] = gvrow.Cells[9].Text;

                    QuotationItems.Rows.Add(dr);
                }
            }
        }

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvQuotationItems.SelectedIndex == -1)
                {
                    if (gvrow.Cells[2].Text == ddlItemName.SelectedItem.Value)
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
            drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = ddlItemName.SelectedItem.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQunatity.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
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
        ddlItemName.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtQunatity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        gvQuotationItems.SelectedIndex = -1;
    }
    #endregion

    #region ItemNames Select Index Changed
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
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

    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
              //  e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Item Details list?');");
            e.Row.Cells[8].Text = (Convert.ToInt32(e.Row.Cells[7].Text) * Convert.ToInt32(e.Row.Cells[6].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Visible = false;
        }
       
        
    }
    #endregion

    #region Grid View Quotation Row Editing
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
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["ItemTypeId"] = gvrow.Cells[9].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvQuotationDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[9].Text;
                    ItemName_Fill();
                    ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtQunatity.Text = gvrow.Cells[6].Text;
                    txtRate.Text = gvrow.Cells[7].Text;
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
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
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
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
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["ItemTypeId"] = gvrow.Cells[9].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region Responsible Person Select Index Changed
    protected void ddlResponsiblePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlResponsiblePerson.SelectedItem.Value) > 0)
            {
                txtFollowupEmail.Text = objHR.EmpEMail;
                txtFollowupPhoneNo.Text = objHR.EmpPhone;
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

    #region GridView Quotation Details Row DataBound
    protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[9].Visible = false;
            //if (!string.IsNullOrEmpty(e.Row.Cells[9].Text) && e.Row.Cells[9].Text != "&nbsp;")
            //{             }
        }
      
   
    }
 #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Quotation Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            meeSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            meeSearchFromDate.Enabled = false;
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
            meeSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            meeSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvQuotationDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvQuotationDetails.DataBind();
    }
      #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=sparesquot&sparesqno=" + gvQuotationDetails.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Send Button Click
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/ServicesReportViewer.aspx?type=Sparesquot&qno=" + gvQuotationDetails.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Follow Up Button Click
    protected void btnFollowUp_Click(object sender, EventArgs e)
    {
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            gvFollowUp.DataBind();
            tblFollowUpHistory.Visible = false;
            txtFollowUpName.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpName];
            if (tblFollowUp.Visible == false)
            {
                tblFollowUp.Visible = true;
            }
            else if (tblFollowUp.Visible == true)
            {
                tblFollowUp.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    #region Button FOLLOW UP CLOSE Click
    protected void btnFollowUpClose_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
    }
    #endregion

    #region Button FOLLOW UP SAVE Click
    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            Services.SparesQuotation objSMQuotAssign = new Services.SparesQuotation();
            Services.BeginTransaction();
            objSMQuotAssign.SparesQuotId = gvQuotationDetails.SelectedRow.Cells[0].Text;
            objSMQuotAssign.FollowUpEmpId = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMQuotAssign.FollowUpDesc = txtFollowUpDesc.Text;
            objSMQuotAssign.FollowUpDate = DateTime.Now.ToString();

            objSMQuotAssign.FollowUpTechDiss = ddlTechnicalDiscussions.SelectedValue;
            objSMQuotAssign.FollowUpCommNegos = ddlCommercialNegociations.SelectedValue;
            objSMQuotAssign.FollowUpCompExistance = ddlCompetatorsExistance.SelectedValue;
            objSMQuotAssign.FollowUpRemarks = txtRemarks.Text;
            objSMQuotAssign.FollowUpExpDate = Yantra.Classes.General.toMMDDYYYY(txtExpectedFwpDate.Text);

            MessageBox.Show(this, objSMQuotAssign.SparesQuotationFollowUp_Save());
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFollowUp.DataBind();
            txtFollowUpDesc.Text = string.Empty;
            ddlTechnicalDiscussions.SelectedValue = "--";
            ddlCommercialNegociations.SelectedValue = "--";
            ddlCompetatorsExistance.SelectedValue = "--";
            txtRemarks.Text = string.Empty;
            txtExpectedFwpDate.Text = string.Empty;
            Services.Dispose();
        }
    }
    #endregion

    #region Button FOLLOW UP REFRESH Click
    protected void btnFollowUpRefresh_Click(object sender, EventArgs e)
    {
        txtFollowUpDesc.Text = string.Empty;
    }
    #endregion

    #region Button FOLLOW UP HISTORY Click
    protected void btnFollowUpHistory_Click(object sender, EventArgs e)
    {
        if (tblFollowUpHistory.Visible == false)
        {
            tblFollowUpHistory.Visible = true;
        }
        else if (tblFollowUpHistory.Visible == true)
        {
            tblFollowUpHistory.Visible = false;
        }
    }
    #endregion

    #region Button CONFIRM YES Click
    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        try
        {
            
            Services.SparesQuotation objSMQuotApprove = new Services.SparesQuotation();
            Services.BeginTransaction();
            objSMQuotApprove.SparesQuotId = gvQuotationDetails.SelectedRow.Cells[0].Text;
            objSMQuotApprove.SparesQuotApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMQuotApprove.SparesQuotationApprove_Update1();
            if (objSMQuotApprove.Get_Ids_Select1(gvQuotationDetails.SelectedRow.Cells[0].Text) > 0)
            {
                Services.SparesQuotation.CompalintRegisterStatus_Update1(Services.ServicesStatus.Open, objSMQuotApprove.CrId);
                Services.ServicesAssignments.ServicesAssignmentsStatus_Update1(Services.ServicesStatus.Open, objSMQuotApprove.AssignTaskId);
            }
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvQuotationDetails.DataBind();
            Services.Dispose();
            btnEdit_Click(sender, e);
        }

        if (((Button)sender).Text == "Yes")
        {
            btnSend_Click(sender, e);
        }
    }
     #endregion

    #region Button REVISE Click
    protected void btnRevise_Click(object sender, EventArgs e)
    {
        if (btnRevise.Text == "Modify")
        {
            btnRevise.Text = "Revise";
        }
        else if (btnRevise.Text == "Revise")
        {
            if (gvQuotationItems.Rows.Count > 0)
            {
                try
                {
                    Services.SparesQuotation objSM = new Services.SparesQuotation();
                    Services.BeginTransaction();
                    objSM.SparesQuotId = gvQuotationDetails.SelectedRow.Cells[0].Text;
                    objSM.SparesQuotNo = txtQuotationNo.Text;
                    objSM.SparesQuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                    objSM.CrId = ddlEnquiryNo.SelectedItem.Value;
                    objSM.SparesQuotDelivery = txtDelivery.Text;
                    objSM.SparesQuotPayTerms = txtPaymentTerms.Text;
                    objSM.SparesQuotPackCharges = txtPackingCharges.Text;
                    objSM.SparesQuotExcise = txtExciseDuty.Text;
                    objSM.SparesQuotCST = txtCST.Text;
                    objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                    objSM.SparesQuotGuarantee ="0";
                    objSM.SparesQuotTransCharges = txtTransCharges.Text;
                    objSM.SparesQuotInsurance = "0";
                    objSM.SparesQuotErrec = txtErrection.Text;
                    objSM.SparesQuotJurisdiction = txtJurisdiction.Text;
                    objSM.SparesQuotValidity = txtValidity.Text;
                    objSM.SparesQuotInspection = txtInspection.Text;
                    objSM.SparesQuotOtherSpecs = txtOtherSpecs.Text;
                    //   objSM.QuotPOLog = txtpo.Text;
                    objSM.SparesQuotRespId = ddlResponsiblePerson.SelectedItem.Value;
                    objSM.SparesQuotSalespId = ddlSalesPerson.SelectedItem.Value;
                    objSM.SparesQuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    objSM.SparesQuotCheckedBy = "0";
                    objSM.SparesQuotApprovedBy = "0";
                    objSM.SparesCurrencyId = ddlCurrencyType.SelectedItem.Value;

                    if (objSM.SparesQuotationRevise_Save() == "Data Saved Successfully")
                    {
                        objSM.SparesQuotationDetails_Delete(objSM.SparesQuotId);
                        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                        {
                            objSM.SparesQuotDetItemCode = gvrow.Cells[2].Text;
                            objSM.SparesQuotDetQty = gvrow.Cells[6].Text;
                            objSM.SparesQuotRate = gvrow.Cells[7].Text;

                            objSM.SparesQuotationDetails_Save();
                        }

                        Services.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
                    else
                    {
                        Services.RollBackTransaction();
                    }
                }
                catch (Exception ex)
                {
                    Services.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    btnDelete.Attributes.Clear();
                    gvQuotationDetails.DataBind();
                    gvEnquiryProducts.DataBind();
                    gvQuotationItems.DataBind();
                    tblQuotationDetails.Visible = false;
                    Services.ClearControls(this);
                    Services.Dispose();
                }
                gvQuotationDetails.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(this, "Please add atleast one Item for Quotation");
            }
        }
    }
    #endregion

    #region ddlCompetatorsExistance_SelectedIndexChanged
    protected void ddlCompetatorsExistance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCompetatorsExistance.SelectedItem.Text == "Exist")
            {
                tdRemarksField.Visible = true;
                tdRemarkslbl.Visible = true;

                tdOrderExpDateField.Visible = false;
                tdOrderExpDatelbl.Visible = false;
            }
            else if (ddlCompetatorsExistance.SelectedItem.Text == "Does Not Exist")
            {
                tdRemarksField.Visible = false;
                tdRemarkslbl.Visible = false;

                tdOrderExpDateField.Visible = true;
                tdOrderExpDatelbl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    #endregion

    #region gvFollowUp_RowDataBound
    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "01/01/1900")
            {
                e.Row.Cells[7].Text = "";
            }


        }
    }
  #endregion

    #region Send To Sales Order
    protected void btnSalesOrder_Click(object sender, EventArgs e)
    {
      
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            Response.Redirect("SparesOrder.aspx?qId=" + gvQuotationDetails.SelectedRow.Cells[0].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    #region Regret Click
    protected void btnRegret_Click(object sender, EventArgs e)
    {
        try
        {
            Services.SparesQuotation objSMQuotApprove = new Services.SparesQuotation();
            Services.BeginTransaction();
            objSMQuotApprove.SparesQuotId = gvQuotationDetails.SelectedRow.Cells[0].Text;
            objSMQuotApprove.SparesQuotApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMQuotApprove.SparesQuotationRegret_Update();
            if (objSMQuotApprove.Get_Ids_Select(gvQuotationDetails.SelectedRow.Cells[0].Text) > 0)
            {
                Services.SparesQuotation.CompalintRegisterStatus_Update(Services.ServicesStatus.Regret, objSMQuotApprove.CrId);
                Services.ServicesAssignments.ServicesAssignmentsStatus_Update(Services.ServicesStatus.Regret, objSMQuotApprove.AssignTaskId);
            }
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvQuotationDetails.DataBind();
            Services.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Approve Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
    }
    #endregion

}

 
