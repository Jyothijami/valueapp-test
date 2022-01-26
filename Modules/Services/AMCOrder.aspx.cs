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
using YantraDAL;
using vllib;
public partial class Modules_Services_AMCOrder : basePage
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (FileUploaderAJAX1.IsPosting)
        //{
        //    this.managePost();
        //}

        if (!IsPostBack)
        {
            //lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            DeliveryType_Fill();
            CurrencyType_Fill();
            QuotationMaster_Fill();
            //ItemTypes_Fill();
            EmployeeMaster_Fill();
            Customer_Fill();
            txtItemQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtItemRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtCST.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtAmount.Attributes.Add("onkeyup", "javascript:grosscalc();");
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (txtSubTotal.Text == "" || txtSubTotal.Text == string.Empty) { txtSubTotal.Text = "0"; }
        if (txtCST.Text == "" || txtCST.Text == string.Empty) { txtCST.Text = "0"; }
        txtTotal.Text = Convert.ToString(double.Parse(txtSubTotal.Text) + (double.Parse(txtCST.Text) * double.Parse(txtSubTotal.Text) / 100));

        if (gvOrderDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvOrderDetails.SelectedRow.Cells[7].Text) && gvOrderDetails.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnPrint.Visible = true;
                //btnSend.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = true;
                //btnPrint.Visible = false;
                //btnSend.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            //btnPrint.Visible = false;
            //btnSend.Visible = false;
        }
    }
    #endregion

    #region Customer Name Fill
    private void Customer_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomerName);
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

    #region Quotation Master Fill
    private void QuotationMaster_Fill()
    {
        try
        {
            Services.AMCQuotation.AMCQuotation_Select(ddlQuotationNo);
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
            Services.AMCQuotation.AMCQuotationItemTypes_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
            //Masters.ItemType.ItemType_Select(ddlItemType);
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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Services.AMCQuotation.AMCQuotationItemNames_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType.SelectedItem.Value, ddlItemName);
            //Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
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
        gvOrderDetails.SelectedIndex = gvRow.RowIndex;
        ddlQuotationNo.Enabled = false;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        Services.ClearControls(this);
        try
        {
            Services.AMCOrder objSalesOrder = new Services.AMCOrder();
            if (objSalesOrder.AMCOrder_Select(gvOrderDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblSalesOrderDetails.Visible = true;
                txtSalesOrderNo.Text = objSalesOrder.AMCONo;
                txtSalesOrderDate.Text = objSalesOrder.AMCODate;
                ddlQuotationNo.SelectedValue = objSalesOrder.AMCQTId;
                ddlResponsiblePerson.SelectedValue = objSalesOrder.AMCORespId;
                ddlSalesPerson.SelectedValue = objSalesOrder.AMCOSalespId;
                ddlPreparedBy.SelectedValue = objSalesOrder.AMCOPreparedBy;
                ddlCheckedBy.SelectedValue = objSalesOrder.AMCOCheckedBy;
                ddlApprovedBy.SelectedValue = objSalesOrder.AMCOApprovedBy;

                txtDelivery.Text = objSalesOrder.AMCODelivery;
                ddlCurrencyType.SelectedValue = objSalesOrder.AMCOCurrencyTypeId;
                txtPaymentTerms.Text = objSalesOrder.AMCOPaymentTerms;
                txtPackingCharges.Text = objSalesOrder.AMCOPackageCharges;
                txtExciseDuty.Text = objSalesOrder.AMCOExciseDuty;
                txtCST.Text = objSalesOrder.AMCOCSTax;
                ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                txtGuarantee.Text = objSalesOrder.AMCOGuarantee;
                txtTransCharges.Text = objSalesOrder.AMCOTransportCharges;
                txtInsurance.Text = objSalesOrder.AMCOInsurance;
                txtErrection.Text = objSalesOrder.AMCOErection;
                txtJurisdiction.Text = objSalesOrder.AMCOJurisdiction;
                txtValidity.Text = objSalesOrder.AMCOValidity;
                //txtInspection.Text = objSalesOrder.AMCOInspection;
                txtOtherSpecs.Text = objSalesOrder.AMCOOtherSpec;
                txtAmcDate.Text = objSalesOrder.AMCOTillDate;
                txtCustPONo.Text = objSalesOrder.AMCOCustPONo;
                txtCustPODate.Text = objSalesOrder.AMCOCustPODate;
                lblAMCOIdHidden.Text = objSalesOrder.AMCOId;
                txtConsignee.Text = objSalesOrder.AMCOConsignee;
                txtResponsiblePerson.Text = objSalesOrder.AMCOResponsiblePerson;
                txtFollowupEmail.Text = objSalesOrder.AMCOResponsiblePersonEmail;
                txtPMCalls.Text = objSalesOrder.AMCOPMCalls;
                txtBDCalls.Text = objSalesOrder.AMCOBDCalls;
                objSalesOrder.AMCOrderDetails_Select(gvOrderDetails.SelectedRow.Cells[0].Text, gvOrderItems);

                //SM.SalesQuotation objSalesQuot = new SM.SalesQuotation();
                //if (objSalesQuot.SalesQuotation_Select(objSalesOrder.QuotId) > 0)
                //{
                //    txtDelivery.Text = objSalesQuot.QuotDelivery;
                //    txtPaymentTerms.Text = objSalesQuot.QuotPayTerms;
                //    txtPackingCharges.Text = objSalesQuot.QuotPackCharges;
                //    txtExciseDuty.Text = objSalesQuot.QuotExcise;
                //    txtCST.Text = objSalesQuot.QuotCST;
                //    ddlDespatchMode.SelectedValue = objSalesQuot.DespmId;
                //    txtGuarantee.Text = objSalesQuot.QuotGuarantee;
                //    txtTransCharges.Text = objSalesQuot.QuotTransCharges;
                //    txtInsurance.Text = objSalesQuot.QuotInsurance;
                //    txtErrection.Text = objSalesQuot.QuotErrec;
                //    txtJurisdiction.Text = objSalesQuot.QuotJurisdiction;
                //    txtValidity.Text = objSalesQuot.QuotValidity;
                //    txtInspection.Text = objSalesQuot.QuotInspection;
                //    txtOtherSpecs.Text = objSalesQuot.QuotOtherSpecs;
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
            ddlQuotationNo_SelectedIndexChanged(sender, e);
            ddlResponsiblePerson_SelectedIndexChanged(sender, e);
        }
    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
     
        gvQuotationProducts.DataBind();
        gvOrderItems.DataBind();
        txtSalesOrderNo.Text = Services.AMCOrder.AMCOrder_AutoGenCode();
        txtSalesOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblSalesOrderDetails.Visible = true;
        gvOrderDetails.SelectedIndex = -1;
        ddlQuotationNo.Enabled = true;
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SalesOrderSave();
        }
        else if (btnSave.Text == "Update")
        {
            SalesOrderUpdate();
        }
    }
    #endregion

    #region SalesOrderSave
    private void SalesOrderSave()
    {
        Services.AMCQuotation objsr = new Services.AMCQuotation();
        if (objsr.AMCQuotation_isrecordexists(ddlQuotationNo.SelectedItem.Value) > 0)
        {
            MessageBox.Show(this, "Qutation order for "+ddlQuotationNo.SelectedItem.Text+" already prepared");
            Yantra.Classes.General.ClearControls(this);
             return;
            
        }

        if (gvOrderItems.Rows.Count > 0)
        {
            try
            {
                Services.AMCOrder objServices = new Services.AMCOrder();
               

                Services.BeginTransaction();
                objServices.AMCONo = txtSalesOrderNo.Text;
                objServices.AMCODate = Yantra.Classes.General.toMMDDYYYY(txtSalesOrderDate.Text);
                objServices.AMCQTId = ddlQuotationNo.SelectedItem.Value;
               
               

                objServices.AMCORespId = "0";
                objServices.AMCOSalespId = ddlSalesPerson.SelectedItem.Value;
                objServices.AMCOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objServices.AMCOCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objServices.AMCOApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objServices.AMCOAcceptanceFlag = Services.ServicesStatus.New.ToString();

                objServices.AMCODelivery = txtDelivery.Text;
                objServices.AMCOCurrencyTypeId = ddlCurrencyType.SelectedItem.Value;
                objServices.AMCOPaymentTerms = txtPaymentTerms.Text;
                objServices.AMCOPackageCharges = txtPackingCharges.Text;
                objServices.AMCOExciseDuty = txtExciseDuty.Text;
                objServices.AMCOCSTax = txtCST.Text;
                objServices.DespmId = ddlDespatchMode.SelectedItem.Value;
                objServices.AMCOGuarantee = txtGuarantee.Text;
                objServices.AMCOTransportCharges = txtTransCharges.Text;
                objServices.AMCOInsurance = txtInsurance.Text;
                objServices.AMCOErection = txtErrection.Text;
                objServices.AMCOJurisdiction = txtJurisdiction.Text;
                objServices.AMCOValidity = txtValidity.Text;
                //objServices.AMCOInspection = txtInspection.Text;
                objServices.AMCOOtherSpec = txtOtherSpecs.Text;
                objServices.AMCOTillDate = Yantra.Classes.General.toMMDDYYYY(txtAmcDate.Text);
                objServices.AMCOCustPONo = txtCustPONo.Text;
                objServices.AMCOCustPODate = Yantra.Classes.General.toMMDDYYYY(txtCustPODate.Text);
                objServices.AMCOConsignee = txtConsignee.Text;
                objServices.AMCOResponsiblePerson = txtResponsiblePerson.Text;
                objServices.AMCOResponsiblePersonEmail = txtFollowupEmail.Text;
                objServices.AMCOPMCalls = txtPMCalls.Text;
                objServices.AMCOBDCalls = txtBDCalls.Text;
               
            

                if (objServices.AMCOrder_Save() == "Data Saved Successfully")
                {
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text))
                    {
                        string[] fileEntries = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text);
                        foreach (string fileName in fileEntries)
                        {
                            string filenameofpath = System.IO.Path.GetFileName(fileName);
                            string[] filepart = filenameofpath.Split('-');
                            if (filepart[0] == "AMCO")
                            {
                                //FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + FileUpload1.PostedFile.FileName.ToString());
                                objServices.AMCOUploadFileName = filepart[2];
                                objServices.AMCOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblAMCOIdHidden.Text + "-" + filepart[2];
                                objServices.AMCOUploadDate = DateTime.Now.ToShortDateString();
                                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/AMCOFiles"))
                                { }
                                else
                                { Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/AMCOFiles"); }
                                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "/" + filenameofpath, AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/AMCOFiles/" + objServices.AMCOFileContentType);
                                objServices.AMCOrderUploads_Save();
                            }
                        }
                    }


                    objServices.AMCOrderDetails_Delete(objServices.AMCOId);
                    foreach (GridViewRow gvrow in gvOrderItems.Rows)
                    {
                        objServices.AMCOItemCode = gvrow.Cells[1].Text;
                        objServices.AMCODetQty = gvrow.Cells[5].Text;
                        objServices.AMCORate = gvrow.Cells[6].Text;
                        objServices.AMCODetSpec = gvrow.Cells[8].Text;
                        objServices.AMCODetRemarks = gvrow.Cells[9].Text;
                        objServices.AMCODetPriority = gvrow.Cells[10].Text;
                        objServices.AMCOrderDetails_Save();
                    }
                    if (objServices.Get_Ids_Select(objServices.AMCOId) > 0)
                    {
                        Services.AMCQuotation.AMCQuotationStatus_Update(Services.ServicesStatus.Closed, objServices.CrId);
                        Services.AMCAssignments.AMCAssignmentsStatus_Update(Services.ServicesStatus.Closed, objServices.AssignTaskId);
                        Services.AMCQuotation.AMCQuotationStatus_Update(Services.ServicesStatus.Closed, objServices.AMCQTId);
                    }
                    Services.CommitTransaction();
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + ""))
                    { Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "", true); }
                    //FileUploaderAJAX1.Reset();
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
                gvOrderDetails.DataBind();
                gvQuotationProducts.DataBind();
                gvOrderItems.DataBind();
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

    #region SalesOrderUpdate
    private void SalesOrderUpdate()
    {
        if (gvOrderItems.Rows.Count > 0)
        {
            try
            {
                Services.AMCOrder objServices = new Services.AMCOrder();
                Services.BeginTransaction();
                objServices.AMCOId = gvOrderDetails.SelectedRow.Cells[0].Text;
                objServices.AMCONo = txtSalesOrderNo.Text;
                objServices.AMCODate = Yantra.Classes.General.toMMDDYYYY(txtSalesOrderDate.Text);
                objServices.AMCQTId = ddlQuotationNo.SelectedItem.Value;
                objServices.AMCORespId = "0";

                objServices.AMCOSalespId = ddlSalesPerson.SelectedItem.Value;
                objServices.AMCOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objServices.AMCOCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objServices.AMCOApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objServices.AMCOAcceptanceFlag = Services.ServicesStatus.New.ToString();

                objServices.AMCODelivery = txtDelivery.Text;
                objServices.AMCOCurrencyTypeId = ddlCurrencyType.SelectedItem.Value;
                objServices.AMCOPaymentTerms = txtPaymentTerms.Text;
                objServices.AMCOPackageCharges = txtPackingCharges.Text;
                objServices.AMCOExciseDuty = txtExciseDuty.Text;
                objServices.AMCOCSTax = txtCST.Text;
                objServices.DespmId = ddlDespatchMode.SelectedItem.Value;
                objServices.AMCOGuarantee = txtGuarantee.Text;
                objServices.AMCOTransportCharges = txtTransCharges.Text;
                objServices.AMCOInsurance = txtInsurance.Text;
                objServices.AMCOErection = txtErrection.Text;
                objServices.AMCOJurisdiction = txtJurisdiction.Text;
                objServices.AMCOValidity = txtValidity.Text;
                //objServices.AMCOInspection = txtInspection.Text;
                objServices.AMCOOtherSpec = txtOtherSpecs.Text;
                objServices.AMCOTillDate = Yantra.Classes.General.toMMDDYYYY(txtAmcDate.Text);
                objServices.AMCOCustPONo = txtCustPONo.Text;
                objServices.AMCOCustPODate = Yantra.Classes.General.toMMDDYYYY(txtCustPODate.Text);
                objServices.AMCOConsignee = txtConsignee.Text;
                objServices.AMCOResponsiblePerson = txtResponsiblePerson.Text;
                objServices.AMCOResponsiblePersonEmail = txtFollowupEmail.Text;
                objServices.AMCOPMCalls = txtPMCalls.Text;
                objServices.AMCOBDCalls = txtBDCalls.Text;

                if (objServices.AMCOrder_Update() == "Data Updated Successfully")
                {
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text))
                    {
                        string[] fileEntries = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text);
                        foreach (string fileName in fileEntries)
                        {
                            string filenameofpath = System.IO.Path.GetFileName(fileName);
                            string[] filepart = filenameofpath.Split('-');
                            if (filepart[0] == "AMCO")
                            {
                                //FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + FileUpload1.PostedFile.FileName.ToString());
                                objServices.AMCOUploadFileName = filepart[2];
                                objServices.AMCOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblAMCOIdHidden.Text + "-" + filepart[2];
                                objServices.AMCOUploadDate = DateTime.Now.ToShortDateString();
                                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/AMCOFiles"))
                                { }
                                else
                                { Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/AMCOFiles"); }
                                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "/" + filenameofpath, AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/AMCOFiles/" + objServices.AMCOFileContentType);
                                objServices.AMCOrderUploads_Save();
                            }
                        }
                    }


                    objServices.AMCOrderDetails_Delete(objServices.AMCOId);
                    foreach (GridViewRow gvrow in gvOrderItems.Rows)
                    {
                        objServices.AMCOItemCode = gvrow.Cells[1].Text;
                        objServices.AMCODetQty = gvrow.Cells[5].Text;
                        objServices.AMCORate = gvrow.Cells[6].Text;
                        objServices.AMCODetSpec = gvrow.Cells[8].Text;
                        objServices.AMCODetRemarks = gvrow.Cells[9].Text;
                        objServices.AMCODetPriority = gvrow.Cells[10].Text;

                        objServices.AMCOrderDetails_Save();
                    }
                    if (objServices.Get_Ids_Select(objServices.AMCOId) > 0)
                    {
                        Services.AMCEnquiry.AMCEnquiryStatus_Update(Services.ServicesStatus.Closed, objServices.EnqId);
                        Services.AMCAssignments.AMCAssignmentsStatus_Update(Services.ServicesStatus.Closed, objServices.AssignTaskId);
                        Services.AMCQuotation.AMCQuotationStatus_Update(Services.ServicesStatus.Closed, objServices.AMCQTId);
                    }
                    Services.CommitTransaction();
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + ""))
                    { Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "", true); }
                    //FileUploaderAJAX1.Reset();
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
                gvOrderDetails.DataBind();
                gvQuotationProducts.DataBind();
                gvOrderItems.DataBind();
                tblSalesOrderDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Sales Order");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvOrderDetails.SelectedIndex > -1)
        {
            Services.ClearControls(this);
            try
            {
                Services.AMCOrder objSalesOrder = new Services.AMCOrder();

                if (objSalesOrder.AMCOrder_Select(gvOrderDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblSalesOrderDetails.Visible = true;
                    txtSalesOrderNo.Text = objSalesOrder.AMCONo;
                    ddlQuotationNo.Enabled = false;
                    txtSalesOrderDate.Text = objSalesOrder.AMCODate;
                    ddlQuotationNo.SelectedValue = objSalesOrder.AMCQTId;
                    ddlResponsiblePerson.SelectedValue = objSalesOrder.AMCORespId;
                    ddlSalesPerson.SelectedValue = objSalesOrder.AMCOSalespId;
                    ddlPreparedBy.SelectedValue = objSalesOrder.AMCOPreparedBy;
                    ddlCheckedBy.SelectedValue = objSalesOrder.AMCOCheckedBy;
                    ddlApprovedBy.SelectedValue = objSalesOrder.AMCOApprovedBy;

                    txtDelivery.Text = objSalesOrder.AMCODelivery;
                    ddlCurrencyType.SelectedValue = objSalesOrder.AMCOCurrencyTypeId;
                    txtPaymentTerms.Text = objSalesOrder.AMCOPaymentTerms;
                    txtPackingCharges.Text = objSalesOrder.AMCOPackageCharges;
                    txtExciseDuty.Text = objSalesOrder.AMCOExciseDuty;
                    txtCST.Text = objSalesOrder.AMCOCSTax;
                    ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                    txtGuarantee.Text = objSalesOrder.AMCOGuarantee;
                    txtTransCharges.Text = objSalesOrder.AMCOTransportCharges;
                    txtInsurance.Text = objSalesOrder.AMCOInsurance;
                    txtErrection.Text = objSalesOrder.AMCOErection;
                    txtJurisdiction.Text = objSalesOrder.AMCOJurisdiction;
                    txtValidity.Text = objSalesOrder.AMCOValidity;
                    //txtInspection.Text = objSalesOrder.AMCOInspection;
                    txtOtherSpecs.Text = objSalesOrder.AMCOOtherSpec;
                    txtAmcDate.Text = objSalesOrder.AMCOTillDate;
                    txtCustPONo.Text = objSalesOrder.AMCOCustPONo;
                    txtCustPODate.Text = objSalesOrder.AMCOCustPODate;
                    lblAMCOIdHidden.Text = objSalesOrder.AMCOId;
                    txtConsignee.Text = objSalesOrder.AMCOConsignee;
                    txtResponsiblePerson.Text = objSalesOrder.AMCOResponsiblePerson;
                    txtFollowupEmail.Text = objSalesOrder.AMCOResponsiblePersonEmail;
                    txtPMCalls.Text = objSalesOrder.AMCOPMCalls;
                    txtBDCalls.Text = objSalesOrder.AMCOBDCalls;
                    objSalesOrder.AMCOrderDetails_Select(gvOrderDetails.SelectedRow.Cells[0].Text, gvOrderItems);

                    //SM.SalesQuotation objSalesQuot = new SM.SalesQuotation();
                    //if (objSalesQuot.SalesQuotation_Select(objSalesOrder.QuotId) > 0)
                    //{
                    //    txtDelivery.Text = objSalesQuot.QuotDelivery;
                    //    txtPaymentTerms.Text = objSalesQuot.QuotPayTerms;
                    //    txtPackingCharges.Text = objSalesQuot.QuotPackCharges;
                    //    txtExciseDuty.Text = objSalesQuot.QuotExcise;
                    //    txtCST.Text = objSalesQuot.QuotCST;
                    //    ddlDespatchMode.SelectedValue = objSalesQuot.DespmId;
                    //    txtGuarantee.Text = objSalesQuot.QuotGuarantee;
                    //    txtTransCharges.Text = objSalesQuot.QuotTransCharges;
                    //    txtInsurance.Text = objSalesQuot.QuotInsurance;
                    //    txtErrection.Text = objSalesQuot.QuotErrec;
                    //    txtJurisdiction.Text = objSalesQuot.QuotJurisdiction;
                    //    txtValidity.Text = objSalesQuot.QuotValidity;
                    //    txtInspection.Text = objSalesQuot.QuotInspection;
                    //    txtOtherSpecs.Text = objSalesQuot.QuotOtherSpecs;
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
        if (gvOrderDetails.SelectedIndex > -1)
        {
            try
            {
                Services.AMCOrder objServices = new Services.AMCOrder();
                Services.BeginTransaction();
                MessageBox.Show(this, objServices.AMCOrder_Delete(gvOrderDetails.SelectedRow.Cells[0].Text));
                Services.CommitTransaction();
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvOrderDetails.DataBind();
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
            Services.AMCQuotation objServices = new Services.AMCQuotation();
            
             
           
            if (objServices.AMCQuotation_Select(ddlQuotationNo.SelectedItem.Value) > 0)
            {
                if (objServices.AMCQTApprovedBy == "0")
                {
                    MessageBox.Show(this, "Qutation "+ddlQuotationNo.SelectedItem.Text+" is not approved so order cannot be prepared");
                    Yantra.Classes.General.ClearControls(this);
                    return;

                }
                
                 ItemTypes_Fill();
                txtQuotationDate.Text = objServices.AMCQTDate;
                ddlCustomerName.SelectedValue = objServices.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objServices.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objServices.CustDetId;
                ddlContactPerson_SelectedIndexChanged(sender, e);

                if (btnSave.Text == "Save")
                {
                    txtPaymentTerms.Text = objServices.AMCQTPaymentTerms;
                    txtCST.Text = objServices.AMCQTServiceTax;
                    txtPMCalls.Text = objServices.AMCQTPMCalls;
                    txtBDCalls.Text = objServices.AMCQTBreakDownCalls;
                }

                objServices.AMCQuotationDetails_Select(ddlQuotationNo.SelectedItem.Value, gvQuotationProducts);

                //Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                //if (objServicesCustomer.CustomerMaster_Select(objServices.CustId) > 0)
                //{
                //    txtCustName.Text = objServicesCustomer.CustName;
                //    txtAddress.Text = objServicesCustomer.Address;
                //    txtEmail.Text = objServicesCustomer.Email;
                //    txtRegion.Text = objServicesCustomer.RegName;
                //    txtPhone.Text = objServicesCustomer.Phone;
                //    txtMobile.Text = objServicesCustomer.Mobile;
                //}
            }
            else
            {
                MessageBox.Show(this, "The Qutation "+ddlQuotationNo.SelectedItem.Text+" is Deleted so u cannot prepare order");
                Yantra.Classes.General.ClearControls(this);
                
                return;
               
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
            if (txtSalesOrderNo.Text == String.Empty)
            {
                txtSalesOrderNo.Text = Services.AMCOrder.AMCOrder_AutoGenCode();
            }
           
       
        }

    }
    #endregion

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {

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


        //try
        //{
        //    SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    SM.Dispose();
        //}
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0 && ddlUnitName.SelectedValue != "0")
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

    //#region GridView Quotation Products Row Databound
    //protected void gvOrderItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Cells[0].Visible = false;

    //    }

    //}
    //#endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
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

        if (gvOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOrderItems.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemType"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["UOM"] = gvrow.Cells[4].Text;
                dr["Quantity"] = gvrow.Cells[5].Text;
                dr["Rate"] = gvrow.Cells[6].Text;
                dr["Specifications"] = gvrow.Cells[8].Text;
                dr["Remarks"] = gvrow.Cells[9].Text;
                dr["Priority"] = gvrow.Cells[10].Text;
                dr["ItemTypeId"] = gvrow.Cells[11].Text;

                SalesOrderItems.Rows.Add(dr);
            }
        }

        if (gvOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOrderItems.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Text)
                {
                    gvOrderItems.DataSource = SalesOrderItems;
                    gvOrderItems.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

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

        gvOrderItems.DataSource = SalesOrderItems;
        gvOrderItems.DataBind();
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


    }
    #endregion

    #region GridView Sales Order Items Row DataBound
    protected void gvOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[7].Text = (Convert.ToInt32(e.Row.Cells[5].Text) * Convert.ToInt32(e.Row.Cells[6].Text)).ToString();
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtSubTotal.Text = GrossAmountCalc().ToString();
        }
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvOrderItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[7].Text);
        }
        return _totalAmt;
    }
    #endregion

    #region GridView Sales Order Items Items Row Deleting
    protected void gvOrderItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvOrderItems.Rows[e.RowIndex].Cells[1].Text;
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

        if (gvOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOrderItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemType"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["Quantity"] = gvrow.Cells[5].Text;
                    dr["Rate"] = gvrow.Cells[6].Text;
                    dr["Specifications"] = gvrow.Cells[8].Text;
                    dr["Remarks"] = gvrow.Cells[9].Text;
                    dr["Priority"] = gvrow.Cells[10].Text;
                    dr["ItemTypeId"] = gvrow.Cells[11].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvOrderItems.DataSource = SalesOrderItems;
        gvOrderItems.DataBind();
    }
    #endregion

    #region Responsible Person Select Index Changed
    protected void ddlResponsiblePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    HR.EmployeeMaster objHR = new HR.EmployeeMaster();
        //    if (objHR.EmployeeMaster_Select(ddlResponsiblePerson.SelectedItem.Value) > 0)
        //    {
        //        txtFollowupEmail.Text = objHR.EmpEMail;
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

    #region GridView Quotation Details Row DataBound
    protected void gvOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (ddlSearchBy.SelectedItem.Text == "Sales Order Date")
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
        gvOrderDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvOrderDetails.DataBind();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //if (gvOrderDetails.SelectedIndex > -1)
        //{
        //    try
        //    {
        //        string pagenavigationstr = "../Reports/ServicesReportViewer.aspx?type=salesorder&amcono=" + gvOrderDetails.SelectedRow.Cells[0].Text + "";
        //        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message);
        //    }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please select atleast a Record");
        //}
    }
    #endregion

    #region gvQuotationItems_RowDataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = (Convert.ToDecimal(e.Row.Cells[3].Text) * Convert.ToDecimal(e.Row.Cells[2].Text)).ToString();
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

    #region Button SEND Click
    protected void btnSend_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Button APPROVE Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCOrder objServicesSOApprove = new Services.AMCOrder();
            Services.BeginTransaction();
            objServicesSOApprove.AMCOId = gvOrderDetails.SelectedRow.Cells[0].Text;
            objServicesSOApprove.AMCOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objServicesSOApprove.AMCOrderApprove_Update();
            Services.AMCOrder.AMCOrderStatus_Update(Services.ServicesStatus.Open, gvOrderDetails.SelectedRow.Cells[0].Text);
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvOrderDetails.DataBind();
            Services.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    //protected void managePost()
    //{
    //    HttpPostedFileAJAX pf = FileUploaderAJAX1.PostedFile;
    //    FileUploaderAJAX1.SaveAs("~/temp/" + lblEmpIdHidden.Text + "/", "AMCO-" + DateTime.Now.Day + "-" + pf.FileName);
    //}

    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        LinkButton lbtnFileOpener;
        lbtnFileOpener = (LinkButton)sender;
        Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
        DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
        string command = "SELECT AMCO_CONTENT_TYPE FROM [YANTRA_AMCO_UPLOADS] WHERE AMCO_ID=" + gvOrderDetails.SelectedRow.Cells[0].Text + " AND AMCO_UPLOAD_FILENAME='" + lbtnFileOpener.Text + "'";
        dbcon.Open();
        string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
        string path = "../../YANTRA_DOCUMENTS/AMCOFiles/" + filename;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
    }

    protected void lbtnAttachedFiles_Click(object sender, EventArgs e)
    {

    }









    
}

 
