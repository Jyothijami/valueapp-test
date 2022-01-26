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
using System.IO;
public partial class Modules_Services_SparesOrder : System.Web.UI.Page
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        if (FileUpload1.HasFile) { }
        //txtSalesOrderDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
        rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        txtOtherSpecs.Attributes.Add("onblur", "javascript:othernextfocus();");
        txtEmail2.Attributes.Add("onblur", "javascript:email2nextfocus();");
        if (!IsPostBack)
        {
            //lblEmpIdHidden.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            DeliveryType_Fill();
            CurrencyType_Fill();
            QuotationMaster_Fill();
            ItemTypes_Fill();
            EmployeeMaster_Fill();
            Designation_Fill();

            if (Request.QueryString["qid"] != null)
            {
                btnNew_Click(sender, e);
                ddlQuotationNo.SelectedValue = Request.QueryString["qid"].ToString();
                ddlQuotationNo_SelectedIndexChanged(sender, e);
                tblSalesOrderDetails.Visible = true;
            }
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvSalesOrderDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvSalesOrderDetails.SelectedRow.Cells[7].Text) && gvSalesOrderDetails.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnPrint.Visible = true;
                btnSend.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
                btnSend.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            //btnPrint.Visible = false;
            btnSend.Visible = false;
        }
    }
    #endregion

    #region Designation Fill
    public void Designation_Fill()
    {
        try
        {
            Masters.Designation.Designation_Select(ddlDesignation1);
            Masters.Designation.Designation_Select(ddlDesignation2);

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

    #region Quotation Master Fill
    private void QuotationMaster_Fill()
    {
        try
        {
            Services.SparesQuotation.SparesQuotation_Select(ddlQuotationNo);
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
            //Services.SparesOrder.SparesOrderItemTypes_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            //Services.Dispose();
        }
    }
    #endregion

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
            //Services.SparesOrder.SparesOrderItemNames_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType.SelectedItem.Value, ddlItemName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            //Services.Dispose();
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

    #region Link Button SalesOrderNo_Click
    protected void lbtnSalesOrderNo_Click(object sender, EventArgs e)
    {
        tblSalesOrderDetails.Visible = false;
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvSalesOrderDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        Services.SparesOrder objSalesOrder = new Services.SparesOrder();

        if (objSalesOrder.SparesOrder_Select(gvSalesOrderDetails.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;
            btnSave.Text = "Update";

            tblSalesOrderDetails.Visible = true;
            txtSalesOrderNo.Text = objSalesOrder.SPONo;
            txtSalesOrderDate.Text = objSalesOrder.SPODate;
            ddlQuotationNo.SelectedValue = objSalesOrder.SparesQuotId;
            ddlResponsiblePerson.SelectedValue = objSalesOrder.SPORespId;
            ddlSalesPerson.SelectedValue = objSalesOrder.SPOSalespId;
            ddlPreparedBy.SelectedValue = objSalesOrder.SPOPreparedBy;
            ddlCheckedBy.SelectedValue = objSalesOrder.SPOCheckedBy;
            ddlApprovedBy.SelectedValue = objSalesOrder.SPOApprovedBy;

            txtDelivery.Text = objSalesOrder.SPODelivery;
            ddlCurrencyType.SelectedValue = objSalesOrder.SPOCurrencyTypeId;
            txtPaymentTerms.Text = objSalesOrder.SPOPaymentTerms;
            txtPackingCharges.Text = objSalesOrder.SPOPackageCharges;
            txtExciseDuty.Text = objSalesOrder.SPOExciseDuty;
            if (objSalesOrder.SPOVAT != "")
            {
                txtVAT.Text = objSalesOrder.SPOVAT;
                lblVATCST.Text = "VAT";
                rbVAT.Checked = true;
                rbCST.Checked = false;
            }
            else if (objSalesOrder.SPOCSTax != "")
            {
                txtVAT.Text = objSalesOrder.SPOCSTax;
                lblVATCST.Text = "C.S. Tax";
                rbCST.Checked = true;
                rbVAT.Checked = false;
            }
            ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
            txtGuarantee.Text = objSalesOrder.SPOGuarantee;
            txtTransCharges.Text = objSalesOrder.SPOTransportCharges;
            txtInsurance.Text = objSalesOrder.SPOInsurance;
            txtErrection.Text = objSalesOrder.SPOErection;
            txtJurisdiction.Text = objSalesOrder.SPOJurisdiction;
            txtValidity.Text = objSalesOrder.SPOValidity;
            txtInspection.Text = objSalesOrder.SPOInspection;
            txtOtherSpecs.Text = objSalesOrder.SPOOtherSpec;

            txtContactName1.Text = objSalesOrder.ContactName1;
            txtContactName2.Text = objSalesOrder.ContactName2;
            txtPhone1.Text = objSalesOrder.ContactPhone1;
            txtPhone2.Text = objSalesOrder.ContactPhone2;
            txtEmail1.Text = objSalesOrder.ContactEmail1;
            txtEmail2.Text = objSalesOrder.ContactEmail2;
            txtConsinment.Text = objSalesOrder.ConsignmentTo;
            txtInvoiceTo.Text = objSalesOrder.InvoiceTo;
            ddlDesignation1.SelectedValue = objSalesOrder.ContactDesig1;
            ddlDesignation2.SelectedValue = objSalesOrder.ContactDesig2;
            txtAdvanceAmt.Text = objSalesOrder.SPOAdvanceAmt;
            txtAccessories.Text = objSalesOrder.SPOAccessories;
            txtExtraSpares.Text = objSalesOrder.SPOExtraSpares;

            lblSPOIdHidden.Text = objSalesOrder.SPOId;

            objSalesOrder.SparesOrderDetails_Select(gvSalesOrderDetails.SelectedRow.Cells[0].Text, gvSalesOrderItems);
            ddlQuotationNo_SelectedIndexChanged(sender, e);
            ddlResponsiblePerson_SelectedIndexChanged(sender, e);

        }

    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            gvSalesOrderDetails.SelectedIndex = -1;
            Services.ClearControls(this);
            gvQuotationProducts.DataBind();
            gvSalesOrderItems.DataBind();
            txtSalesOrderNo.Text = Services.SparesOrder.SparesOrder_AutoGenCode();
            txtSalesOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //btnSave.Text = "Save";
            tblSalesOrderDetails.Visible = true;
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
            SparesOrderSave();
        }
        else if (btnSave.Text == "Update")
        {
            SparesOrderUpdate();
        }
    }
    #endregion

    #region SparesOrderSave
    private void SparesOrderSave()
    {
        Services.SparesOrder objsr = new Services.SparesOrder();
        if (objsr.sparesQuotation_isrecordexists(ddlQuotationNo.SelectedItem.Value) > 0)
        {
            MessageBox.Show(this, "Qutation order for " + ddlQuotationNo.SelectedItem.Text + " already prepared");
            Yantra.Classes.General.ClearControls(this);
            return;
        }
        if (ddlCurrencyType.SelectedValue == "0")
        {
            MessageBox.Show(this, "Please select valid currency");
            return;
        }
        if (gvSalesOrderItems.Rows.Count > 0)
        {
            try
            {
                if (txtAdvanceAmt.Text == "") { txtAdvanceAmt.Text = "0"; }
               
                

                Services.SparesOrder objSM = new Services.SparesOrder();
                Services.BeginTransaction();
                objSM.SPONo = txtSalesOrderNo.Text;
                objSM.SPODate = Yantra.Classes.General.toMMDDYYYY(txtSalesOrderDate.Text);
                objSM.SparesQuotId = ddlQuotationNo.SelectedItem.Value;

                objSM.SPORespId = ddlResponsiblePerson.SelectedItem.Value;
                objSM.SPOSalespId = ddlSalesPerson.SelectedItem.Value;
                objSM.SPOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.SPOCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.SPOApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.SPOAcceptanceFlag = Services.ServicesStatus.New.ToString();

                objSM.SPODelivery = txtDelivery.Text;
                objSM.SPOCurrencyTypeId = ddlCurrencyType.SelectedItem.Value;
                objSM.SPOPaymentTerms = txtPaymentTerms.Text;
                objSM.SPOPackageCharges = txtPackingCharges.Text;
                objSM.SPOExciseDuty = txtExciseDuty.Text;
                if (rbVAT.Checked == true) { objSM.SPOVAT = txtVAT.Text; } else if (rbCST.Checked == true) { objSM.SPOCSTax = txtVAT.Text; }
                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.SPOGuarantee = txtGuarantee.Text;
                objSM.SPOTransportCharges = txtTransCharges.Text;
                objSM.SPOInsurance = txtInsurance.Text;
                objSM.SPOErection = txtErrection.Text;
                objSM.SPOJurisdiction = txtJurisdiction.Text;
                objSM.SPOValidity = txtValidity.Text;
                objSM.SPOInspection = txtInspection.Text;
                objSM.SPOOtherSpec = txtOtherSpecs.Text;

                objSM.ContactName1 = txtContactName1.Text;
                objSM.ContactName2 = txtContactName2.Text;
                objSM.ContactPhone1 = txtPhone1.Text;
                objSM.ContactPhone2 = txtPhone2.Text;
                objSM.ContactEmail1 = txtEmail1.Text;
                objSM.ContactEmail2 = txtEmail2.Text;
                objSM.ConsignmentTo = txtConsinment.Text;
                objSM.InvoiceTo = txtInvoiceTo.Text;

                objSM.ContactDesig1 = ddlDesignation1.SelectedItem.Value;
                objSM.ContactDesig2 = ddlDesignation2.SelectedItem.Value;
                objSM.SPOAdvanceAmt = txtAdvanceAmt.Text;
                objSM.SPOAccessories = txtAccessories.Text;
                objSM.SPOExtraSpares = txtExtraSpares.Text;
                

                objSM.SPOFLag = Services.ServicesStatus.New.ToString();


                if (objSM.SparesOrder_Save() == "Data Saved Successfully")
                {
                    if (FileUpload1.HasFile)
                    {
                        objSM.SOUploadFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        objSM.SparesOrderUploads_Save();
                    }
                    else
                    {
                        objSM.SOFiles = "";
                    }
                   
                   
                    objSM.SparesOrderDetails_Delete(objSM.SPOId);
                    foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                    {
                        objSM.SPOItemCode = gvrow.Cells[2].Text;
                        objSM.SPODetQty = gvrow.Cells[6].Text;
                        objSM.SPORate = gvrow.Cells[7].Text;
                        objSM.SPODetSpec = gvrow.Cells[9].Text;
                        objSM.SPODetRemarks = gvrow.Cells[10].Text;
                        objSM.SPODetPriority = gvrow.Cells[11].Text;

                        objSM.SparesOrderDetails_Save();
                    }
                    if (objSM.Get_Ids_Select(objSM.SPOId) > 0)
                    {
                        Services.AMCEnquiry.AMCEnquiryStatus_Update(Services.ServicesStatus.Closed, objSM.EnqId);
                        Services.AMCAssignments.AMCAssignmentsStatus_Update(Services.ServicesStatus.Closed, objSM.AssignTaskId);
                        Services.SparesQuotation.SparesQuotationStatus_Update(Services.ServicesStatus.Closed, objSM.SparesQuotId);
                    }

                    //if (FileUpload1.HasFile)
                    //{
                    //    if (lbtnAttachedFiles.Text != "")
                    //    {
                    //        string[] ext = lbtnAttachedFiles.Text.Split('.');
                    //        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSM.SPOId + "." + ext[1]))
                    //        { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSM.SPOId + "." + ext[1]); }
                    //    }
                    //  //  FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSM.SPOId + FileUpload1.PostedFile.FileName.ToString());
                    //}

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
                gvSalesOrderDetails.DataBind();
                gvQuotationProducts.DataBind();
                gvSalesOrderItems.DataBind();
                tblSalesOrderDetails.Visible = false;
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

    #region SPARESOrderUpdate
    private void SparesOrderUpdate()
    {
        if (gvSalesOrderItems.Rows.Count > 0)
        {
            try
            {
                Services.SparesOrder objSM = new Services.SparesOrder();

                //Services.BeginTransaction();

                objSM.SPOId = gvSalesOrderDetails.SelectedRow.Cells[0].Text;
                objSM.SPONo = txtSalesOrderNo.Text;
                objSM.SPODate = Yantra.Classes.General.toMMDDYYYY(txtSalesOrderDate.Text);
                objSM.SparesQuotId = ddlQuotationNo.SelectedItem.Value;

                objSM.SPORespId = ddlResponsiblePerson.SelectedItem.Value;
                objSM.SPOSalespId = ddlSalesPerson.SelectedItem.Value;
                objSM.SPOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.SPOCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.SPOApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.SPOAcceptanceFlag = Services.ServicesStatus.New.ToString();

                objSM.SPODelivery = txtDelivery.Text;
                objSM.SPOCurrencyTypeId = ddlCurrencyType.SelectedItem.Value;
                objSM.SPOPaymentTerms = txtPaymentTerms.Text;
                objSM.SPOPackageCharges = txtPackingCharges.Text;
                objSM.SPOExciseDuty = txtExciseDuty.Text;
                if (rbVAT.Checked == true) { objSM.SPOVAT = txtVAT.Text; } else if (rbCST.Checked == true) { objSM.SPOCSTax = txtVAT.Text; }
                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.SPOGuarantee = txtGuarantee.Text;
                objSM.SPOTransportCharges = txtTransCharges.Text;
                objSM.SPOInsurance = txtInsurance.Text;
                objSM.SPOErection = txtErrection.Text;
                objSM.SPOJurisdiction = txtJurisdiction.Text;
                objSM.SPOValidity = txtValidity.Text;
                objSM.SPOInspection = txtInspection.Text;
                objSM.SPOOtherSpec = txtOtherSpecs.Text;

                objSM.ContactName1 = txtContactName1.Text;
                objSM.ContactName2 = txtContactName2.Text;
                objSM.ContactPhone1 = txtPhone1.Text;
                objSM.ContactPhone2 = txtPhone2.Text;
                objSM.ContactEmail1 = txtEmail1.Text;
                objSM.ContactEmail2 = txtEmail2.Text;
                objSM.ConsignmentTo = txtConsinment.Text;
                objSM.InvoiceTo = txtInvoiceTo.Text;


                objSM.ContactDesig1 = ddlDesignation1.SelectedItem.Value;
                objSM.ContactDesig2 = ddlDesignation2.SelectedItem.Value;
                objSM.SPOAdvanceAmt = txtAdvanceAmt.Text;
                objSM.SPOAccessories = txtAccessories.Text;
                objSM.SPOExtraSpares = txtExtraSpares.Text;
               


                objSM.SPOFLag = Services.ServicesStatus.New.ToString();


                if (objSM.SparesOrder_Update() == "Data Updated Successfully")
                {
                    if (FileUpload1.HasFile)
                    {
                        objSM.SOUploadFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        objSM.SparesOrderUploads_Save();
                    }
                    else
                    {
                        objSM.SOFiles = "";
                    }

                    objSM.SparesOrderDetails_Delete(objSM.SPOId);
                    foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                    {
                        objSM.SPOItemCode = gvrow.Cells[2].Text;
                        objSM.SPODetQty = gvrow.Cells[6].Text;
                        objSM.SPORate = gvrow.Cells[7].Text;
                        objSM.SPODetSpec = gvrow.Cells[9].Text;
                        objSM.SPODetRemarks = gvrow.Cells[10].Text;
                        objSM.SPODetPriority = gvrow.Cells[11].Text;

                        objSM.SparesOrderDetails_Save();
                    }
                    if (objSM.Get_Ids_Select(objSM.SPOId) > 0)
                    {
                        Services.SparesQuotation.CompalintRegisterStatus_Update(Services.ServicesStatus.Closed, objSM.CrId);
                        Services.ServicesAssignments.ServicesAssignmentsStatus_Update(Services.ServicesStatus.Closed, objSM.AssignTaskId);
                        Services.SparesQuotation.SparesQuotationStatus_Update(Services.ServicesStatus.Closed, objSM.SparesQuotId);
                    }

                    if (FileUpload1.HasFile)
                    {
                        if (lbtnAttachedFiles.Text != "")
                        {
                            string[] ext = lbtnAttachedFiles.Text.Split('.');
                            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSM.SPOId + "." + ext[1]))
                            { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSM.SPOId + "." + ext[1]); }
                        }
                        FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSM.SPOId + FileUpload1.PostedFile.FileName.ToString());
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
                gvSalesOrderDetails.DataBind();
                gvQuotationProducts.DataBind();
                gvSalesOrderItems.DataBind();
                tblSalesOrderDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Spares Order");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvSalesOrderDetails.SelectedIndex > -1)
        {
            Services.ClearControls(this);
            try
            {
                Services.SparesOrder objSalesOrder = new Services.SparesOrder();

                if (objSalesOrder.SparesOrder_Select(gvSalesOrderDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblSalesOrderDetails.Visible = true;
                    txtSalesOrderNo.Text = objSalesOrder.SPONo;
                    txtSalesOrderDate.Text = objSalesOrder.SPODate;
                    ddlQuotationNo.SelectedValue = objSalesOrder.SparesQuotId;
                    ddlResponsiblePerson.SelectedValue = objSalesOrder.SPORespId;
                    ddlSalesPerson.SelectedValue = objSalesOrder.SPOSalespId;
                    ddlPreparedBy.SelectedValue = objSalesOrder.SPOPreparedBy;
                    ddlCheckedBy.SelectedValue = objSalesOrder.SPOCheckedBy;
                    ddlApprovedBy.SelectedValue = objSalesOrder.SPOApprovedBy;

                    txtDelivery.Text = objSalesOrder.SPODelivery;
                    ddlCurrencyType.SelectedValue = objSalesOrder.SPOCurrencyTypeId;
                    txtPaymentTerms.Text = objSalesOrder.SPOPaymentTerms;
                    txtPackingCharges.Text = objSalesOrder.SPOPackageCharges;
                    txtExciseDuty.Text = objSalesOrder.SPOExciseDuty;
                    if (objSalesOrder.SPOVAT != "")
                    {
                        txtVAT.Text = objSalesOrder.SPOVAT;
                        lblVATCST.Text = "VAT";
                        rbVAT.Checked = true;
                        rbCST.Checked = false;
                    }
                    else if (objSalesOrder.SPOCSTax != "")
                    {
                        txtVAT.Text = objSalesOrder.SPOCSTax;
                        lblVATCST.Text = "C.S. Tax";
                        rbCST.Checked = true;
                        rbVAT.Checked = false;
                    }
                    ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                    txtGuarantee.Text = objSalesOrder.SPOGuarantee;
                    txtTransCharges.Text = objSalesOrder.SPOTransportCharges;
                    txtInsurance.Text = objSalesOrder.SPOInsurance;
                    txtErrection.Text = objSalesOrder.SPOErection;
                    txtJurisdiction.Text = objSalesOrder.SPOJurisdiction;
                    txtValidity.Text = objSalesOrder.SPOValidity;
                    txtInspection.Text = objSalesOrder.SPOInspection;
                    txtOtherSpecs.Text = objSalesOrder.SPOOtherSpec;

                    txtContactName1.Text = objSalesOrder.ContactName1;
                    txtContactName2.Text = objSalesOrder.ContactName2;
                    txtPhone1.Text = objSalesOrder.ContactPhone1;
                    txtPhone2.Text = objSalesOrder.ContactPhone2;
                    txtEmail1.Text = objSalesOrder.ContactEmail1;
                    txtEmail2.Text = objSalesOrder.ContactEmail2;
                    txtConsinment.Text = objSalesOrder.ConsignmentTo;
                    txtInvoiceTo.Text = objSalesOrder.InvoiceTo;


                    ddlDesignation1.SelectedValue = objSalesOrder.ContactDesig1;
                    ddlDesignation2.SelectedValue = objSalesOrder.ContactDesig2;
                    txtAdvanceAmt.Text = objSalesOrder.SPOAdvanceAmt;
                    txtAccessories.Text = objSalesOrder.SPOAccessories;
                    txtExtraSpares.Text = objSalesOrder.SPOExtraSpares;
                    lblSPOIdHidden.Text = objSalesOrder.SPOId;

                    lbtnAttachedFiles.Text = objSalesOrder.SOFiles;
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SOFiles/" + objSalesOrder.SOFiles + "." + ext[1]))
                        {
                            lbtnAttachedFiles.Attributes.Add("onclick", "window.open('SOFiles/" + objSalesOrder.SPOId + "." + ext[1] + "','SOFiles','resizable=yes,width=800,height=600,status=yes,toolbar=no,menubar=no');");
                        }
                        else
                        {
                            lbtnAttachedFiles.Text = "";
                            lbtnAttachedFiles.Attributes.Clear();
                        }
                    }
                    ddlQuotationNo_SelectedIndexChanged(sender, e);
                    ddlResponsiblePerson_SelectedIndexChanged(sender, e);
                    objSalesOrder.SparesOrderDetails_Select(gvSalesOrderDetails.SelectedRow.Cells[0].Text, gvSalesOrderItems);
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
                ddlQuotationNo_SelectedIndexChanged(sender, e);
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
        if (gvSalesOrderDetails.SelectedIndex > -1)
        {
            try
            {
                Services.SparesOrder objSM = new Services.SparesOrder();
                MessageBox.Show(this, objSM.SparesOrder_Delete(gvSalesOrderDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvSalesOrderDetails.DataBind();
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

    #region Quotation No Selected Index Changed
    protected void ddlQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Services.SparesQuotation objSM = new Services.SparesQuotation();
            if (objSM.SparesQuotation_Select(ddlQuotationNo.SelectedItem.Value) > 0)
            {
                txtQuotationDate.Text = objSM.SparesQuotDate;
                ItemTypes_Fill();
                if (btnSave.Text == "Save")
                {
                    txtDelivery.Text = objSM.SparesQuotDelivery;
                    txtPaymentTerms.Text = objSM.SparesQuotPayTerms;
                    txtPackingCharges.Text = objSM.SparesQuotPackCharges;
                    txtExciseDuty.Text = objSM.SparesQuotExcise;
                    txtCST.Text = objSM.SparesQuotCST;
                    ddlDespatchMode.SelectedValue = objSM.DespmId;
                    txtGuarantee.Text = objSM.SparesQuotGuarantee;
                    txtTransCharges.Text = objSM.SparesQuotTransCharges;
                    txtInsurance.Text = objSM.SparesQuotInsurance;
                    txtErrection.Text = objSM.SparesQuotErrec;
                    txtJurisdiction.Text = objSM.SparesQuotJurisdiction;
                    txtValidity.Text = objSM.SparesQuotValidity;
                    txtInspection.Text = objSM.SparesQuotInspection;
                    txtOtherSpecs.Text = objSM.SparesQuotOtherSpecs;
                }

                objSM.SparesQuotationDetails_Select(ddlQuotationNo.SelectedItem.Value, gvQuotationProducts);

                
                //if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select1(objSM.CustId,objSM.cust_unit) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = objSMCustomer.CustUnitName;
                //}
                Services.SparesQuotation objSM1 = new Services.SparesQuotation();
                if (objSM1.CompalintRegister_Select(objSM.CrId) > 0)
                {
                    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                    {
                        txtCustName.Text = objSMCustomer.CustName;
                        txtAddress.Text = objSM1.cust_unit_add;

                        txtEmail.Text = objSMCustomer.Email;
                        txtRegion.Text = objSMCustomer.RegName;
                        txtPhone.Text = objSMCustomer.Phone;
                        txtMobile.Text = objSMCustomer.Mobile;

                        txtUnitName.Text = objSM1.cust_unit;
                        if (txtAddress.Text == "")
                        {
                            txtAddress.Text = objSMCustomer.Address;
                            txtUnitName.Text = "--";
                        }
                    }
                }
                // if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = "--";
                //}
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
        if (Request.QueryString["qid"] != null)
        {
            btnNew_Click(sender, e);
            ddlQuotationNo.SelectedValue = Request.QueryString["qid"].ToString();
            ddlQuotationNo_SelectedIndexChanged(sender, e);
            tblSalesOrderDetails.Visible = true;
        }
    }
    #endregion
    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblSalesOrderDetails.Visible = false;
    }
      #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtItemSpecifications.Text == "") { txtItemSpecifications.Text = "-"; }
        if (txtItemRemarks.Text == "") { txtItemRemarks.Text = "-"; }
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Priority");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);

        if (gvSalesOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
            {
                if (gvSalesOrderItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvSalesOrderItems.SelectedRow.RowIndex)
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["ItemCode"] = ddlItemName.SelectedItem.Value;
                        dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["ItemName"] = ddlItemName.SelectedItem.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["Rate"] = txtItemRate.Text;
                        dr["Specifications"] = txtItemSpecifications.Text;
                        dr["Remarks"] = txtItemRemarks.Text;
                        dr["Priority"] = ddlItemPriority.SelectedItem.Value;
                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        SalesOrderItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["Specifications"] = gvrow.Cells[9].Text;
                        dr["Remarks"] = gvrow.Cells[10].Text;
                        dr["Priority"] = gvrow.Cells[11].Text;
                        dr["ItemTypeId"] = gvrow.Cells[12].Text;
                        SalesOrderItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Specifications"] = gvrow.Cells[9].Text;
                    dr["Remarks"] = gvrow.Cells[10].Text;
                    dr["Priority"] = gvrow.Cells[11].Text;
                    dr["ItemTypeId"] = gvrow.Cells[12].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }

        if (gvSalesOrderItems.Rows.Count > 0)
        {
            if (gvSalesOrderItems.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {
                    if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Text)
                    {
                        gvSalesOrderItems.DataSource = SalesOrderItems;
                        gvSalesOrderItems.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvSalesOrderItems.SelectedIndex == -1)
        {
            DataRow drnew = SalesOrderItems.NewRow();
            drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = ddlItemName.SelectedItem.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;
            drnew["Rate"] = txtItemRate.Text;
            drnew["Specifications"] = txtItemSpecifications.Text;
            drnew["Remarks"] = txtItemRemarks.Text;
            drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            SalesOrderItems.Rows.Add(drnew);
        }
        gvSalesOrderItems.DataSource = SalesOrderItems;
        gvSalesOrderItems.DataBind();
        gvSalesOrderItems.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlItemName.SelectedValue = "0";
        ddlItemType.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        txtItemSpecifications.Text = string.Empty;
        txtItemRemarks.Text = string.Empty;
        ddlItemPriority.SelectedValue = "0";
        gvSalesOrderItems.SelectedIndex = -1;
    }
    #endregion

    #region GridView Sales Order Items Row DataBound
    protected void gvSalesOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[8].Text = (Convert.ToInt32(e.Row.Cells[7].Text) * Convert.ToInt32(e.Row.Cells[6].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[12].Visible = false;
        }

    }
    #endregion

    #region GridView Sales Order Items Items Row Deleting
    protected void gvSalesOrderItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvSalesOrderItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Priority");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);

        if (gvSalesOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Specifications"] = gvrow.Cells[9].Text;
                    dr["Remarks"] = gvrow.Cells[10].Text;
                    dr["Priority"] = gvrow.Cells[11].Text;
                    dr["ItemTypeId"] = gvrow.Cells[12].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvSalesOrderItems.DataSource = SalesOrderItems;
        gvSalesOrderItems.DataBind();
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
    protected void gvSalesOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (ddlSearchBy.SelectedItem.Text == "Spares Order Date")
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
        gvSalesOrderDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvSalesOrderDetails.DataBind();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvSalesOrderDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/ServicesReportViewer.aspx?type=sparesorder&spono=" + gvSalesOrderDetails.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region gvQuotationItems_RowDataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = (Convert.ToInt32(e.Row.Cells[4].Text) * Convert.ToInt32(e.Row.Cells[3].Text)).ToString();
        }
    }
    #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

    #region ddlItemName_SelectedIndexChanged
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

    protected void btnSend_Click(object sender, EventArgs e)
    {
    }

    #region Button APPROVE
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Services.SparesOrder objSMSOApprove = new  Services.SparesOrder();
            Services.BeginTransaction();
            objSMSOApprove.SPOId = gvSalesOrderDetails.SelectedRow.Cells[0].Text;
            objSMSOApprove.SPOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMSOApprove.SparesOrderApprove_Update();
            Services.SparesOrder.SparesOrderStatus_Update(Services.ServicesStatus.Open, gvSalesOrderDetails.SelectedRow.Cells[0].Text);
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvSalesOrderDetails.DataBind();
            Services.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Button SEND WORK ORDER
    protected void btnSendWorkOrder_Click(object sender, EventArgs e)
    {
        if (gvSalesOrderDetails.SelectedIndex > -1)
        {
            Response.Redirect("SparesOrderProfile.aspx?SPOId=" + gvSalesOrderDetails.SelectedRow.Cells[0].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    #region gvSalesOrderItems_RowEditing
    protected void gvSalesOrderItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Priority");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);

        if (gvSalesOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["Specifications"] = gvrow.Cells[9].Text;
                dr["Remarks"] = gvrow.Cells[10].Text;
                dr["Priority"] = gvrow.Cells[11].Text;
                dr["ItemTypeId"] = gvrow.Cells[12].Text;

                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvSalesOrderItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[12].Text;
                    ItemName_Fill();
                    ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemRate.Text = gvrow.Cells[7].Text;
                    txtItemSpecifications.Text = gvrow.Cells[9].Text;
                    txtItemRemarks.Text = gvrow.Cells[10].Text;
                    ddlItemPriority.SelectedValue = gvrow.Cells[11].Text;
                    gvSalesOrderItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvSalesOrderItems.DataSource = SalesOrderItems;
        gvSalesOrderItems.DataBind();
    }
    #endregion

    #region File Opener Click
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        LinkButton lbtnFileOpener;
        lbtnFileOpener = (LinkButton)sender;
        Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
        string filepath = "SOFiles/" + lbtnFileOpener.Text;
        if (File.Exists(Request.PhysicalApplicationPath + "\\Modules/Services\\" + filepath))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + filepath + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        }
        else
        {
            MessageBox.Show(this, "File Not Found");
        }
    }
    #endregion


}




 
