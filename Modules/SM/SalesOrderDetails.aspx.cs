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
using Subgurim.Controles;
using YantraDAL;
using System.Data.SqlClient;
using vllib;

public partial class Modules_SM_SalesOrderDetails : basePage
{
    decimal TotalAmount1 = 0;

    decimal TotalAmount = 0;
    decimal TotalAmount2 = 0;
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {



        //txtSalesOrderDate.Attributes.Add("onblur", "javascript:isValidDate(this);");

        txtItemQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtItemRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtDiscount.Attributes.Add("onkeyup", "javascript:amtcalc();");


        rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        txtOtherSpecs.Attributes.Add("onblur", "javascript:othernextfocus();");
        txtEmail2.Attributes.Add("onblur", "javascript:email2nextfocus();");
        if (!IsPostBack)
        {

            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            //if (FileUploaderAJAX1.IsPosting)
            //{
            //    this.managePost();
            //}
            //gvSalesOrderDetails.DataBind();
            DeliveryType_Fill();
            CurrencyType_Fill();
            QuotationMaster_Fill();
            CustomerMaster_Fill();
            EmployeeMaster_Fill();
            Designation_Fill();
            txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlCurrencyType.SelectedValue = "1";
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
                rdbAll.Visible = true;
                lblSelectModel.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                rdbAll.Visible = false;
                lblSelectModel.Visible = false;
            }
            if (Request.QueryString["qid"] != null)
            {
                btnNew_Click(sender, e);
                ddlQuotationNo.SelectedValue = Request.QueryString["qid"].ToString();
                ddlQuotationNo_SelectedIndexChanged(sender, e);
                tblSalesOrderDetails.Visible = true;
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
            SM.SalesOrder objSalesOrder = new SM.SalesOrder();

            if (objSalesOrder.SalesOrder_Select(Request.QueryString["SalesId"].ToString()) > 0)
            {
                btnSave.Enabled = true;
                btnSave.Text = "Update";

                tblSalesOrderDetails.Visible = true;
                txtSalesOrderNo.Text = objSalesOrder.SONo;
                txtSalesOrderDate.Text = objSalesOrder.SODate;
                ddlQuotationNo.SelectedValue = objSalesOrder.QuotId;

                ddlPreparedBy.SelectedValue = objSalesOrder.SOPreparedBy;
                ddlCheckedBy.SelectedValue = objSalesOrder.SOCheckedBy;
                ddlApprovedBy.SelectedValue = objSalesOrder.SOApprovedBy;
                //   txtTransCharges.Text = objSalesOrder.SOTransportCharges;
                txtDelivery.Text = (objSalesOrder.SODelivery);
                ddlCurrencyType.SelectedValue = objSalesOrder.SOCurrencyTypeId;
                txtPaymentTerms.Text = objSalesOrder.SOPaymentTerms;
                txtPackingCharges.Text = objSalesOrder.SOPackageCharges;
                txtExciseDuty.Text = objSalesOrder.SOExciseDuty;
                if (objSalesOrder.SOVAT != "")
                {
                    txtVAT.Text = objSalesOrder.SOVAT;
                    lblVATCST.Text = "VAT";
                    rbVAT.Checked = true;
                    rbCST.Checked = false;
                }
                else if (objSalesOrder.SOCSTax != "")
                {
                    txtVAT.Text = objSalesOrder.SOCSTax;
                    lblVATCST.Text = "C.S. Tax";
                    rbCST.Checked = true;
                    rbVAT.Checked = false;
                }
                ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                txtGuarantee.Text = objSalesOrder.SOGuarantee;
                txtTransCharges.Text = objSalesOrder.SOTransportCharges;
                txtInsurance.Text = objSalesOrder.SOInsurance;
                txtErrection.Text = objSalesOrder.SOErection;
                txtJurisdiction.Text = objSalesOrder.SOJurisdiction;
                txtValidity.Text = objSalesOrder.SOValidity;
                txtInspection.Text = objSalesOrder.SOInspection;
                txtOtherSpecs.Text = objSalesOrder.SOOtherSpec;

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
                txtAdvanceAmt.Text = objSalesOrder.SOAdvanceAmt;
                txtAccessories.Text = objSalesOrder.SOAccessories;
                txtExtraSpares.Text = objSalesOrder.SOExtraSpares;
                txtCustPONo.Text = objSalesOrder.SOCustPONo;
                txtCustPODated.Text = objSalesOrder.SOCustPODated;
                txtCSTNo.Text = objSalesOrder.SOCSTNo;
                txtTINNo.Text = objSalesOrder.SOTINNo;
                lblSOIdHidden.Text = objSalesOrder.SOId;
                ddlsalestatus.SelectedValue = objSalesOrder.sosalestatus;
                objSalesOrder.SalesOrderDetails_Select(Request.QueryString["SalesId"].ToString(), gvDonepo);
                ddlQuotationNo_SelectedIndexChanged(sender, e);
                string userId = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

                if (userId == "0")
                {
                    btnDelete.Visible = true;
                    btnEdit.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                    btnEdit.Visible = false;
                }


            }
            btnItemRefresh_Click(sender, e);


        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString["PoNo"] != null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AppBy"].ToString()) && Request.QueryString["AppBy"].ToString() != "&nbsp;")
            {
                btnApprove.Visible = false;
                // btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnEdit.Visible = false;
                btnDelete.Visible = false;
                //btnPrint.Visible = true;
                btnSend.Visible = true;
                btnAcceptence.Visible = true;
                // btnSendWorkOrder.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
                btnSend.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                //btnSendWorkOrder.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            // btnApprove.Visible = false;
            //btnPrint.Visible = false;
            btnSend.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
                rdbAll.Visible = true;
                lblSelectModel.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
                rdbAll.Visible = false;
                lblSelectModel.Visible = false;
            }
            //btnSendWorkOrder.Visible = false;
        }
    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
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
            SM.SalesQuotation.SalesQuotation_Select(ddlQuotationNo);
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
            //Masters.ItemType.ItemType_Select(ddlItemType);
            SM.SalesQuotation.SalesQuotationItemTypes1_Select(ddlQuotationNo.SelectedItem.Value, ddlModelNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();
            SM.Dispose();
        }
    }
    #endregion


    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {

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

    

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            //gvSalesOrderDetails.SelectedIndex = -1;
            SM.ClearControls(this);
            gvQuotationProducts.DataBind();
            gvSalesOrderItems.DataBind();
            txtSalesOrderNo.Text = SM.SalesOrder.SalesOrder_AutoGenCode();
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

    private string AutoFillInvoiceAddress()
    {
        string InvoiceAddress;
        InvoiceAddress = ddlCustomer.SelectedItem.Text;
        if (ddlUnitName.SelectedValue != "0")
        {
            InvoiceAddress = InvoiceAddress + Environment.NewLine + ddlUnitName.SelectedItem.Text;
        }
        InvoiceAddress = InvoiceAddress + Environment.NewLine + txtUnitAddress.Text;

        return InvoiceAddress;
    }
    private string AutoFillConsigneeAddress()
    {
        string ConsigneeAddress;
        ConsigneeAddress = ddlCustomer.SelectedItem.Text;
        if (ddlUnitName.SelectedValue != "0")
        {
            ConsigneeAddress = ConsigneeAddress + Environment.NewLine + ddlUnitName.SelectedItem.Text;
        }
        ConsigneeAddress = ConsigneeAddress + Environment.NewLine + txtUnitAddress.Text;

        return ConsigneeAddress;
    }

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlsalestatus.SelectedItem.Value == "0")
        {
            ddlsalestatus.SelectedItem.Value = "1";
        }
        if (txtConsinment.Text == "" || txtConsinment.Text.Trim() == "")
        {
            txtConsinment.Text = AutoFillConsigneeAddress();
        }
        if (txtInvoiceTo.Text == "" || txtInvoiceTo.Text.Trim() == "")
        {
            txtInvoiceTo.Text = AutoFillInvoiceAddress();
        }
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
        if (gvSalesOrderItems.Rows.Count > 0)
        {
            try
            {
                if (txtAdvanceAmt.Text == "") { txtAdvanceAmt.Text = "0"; }
                SM.SalesOrder objSM = new SM.SalesOrder();
                SM.BeginTransaction();
                objSM.SONo = txtSalesOrderNo.Text;
                objSM.SODate = Yantra.Classes.General.toMMDDYYYY(txtSalesOrderDate.Text);
                objSM.QuotId = ddlQuotationNo.SelectedItem.Value;

                objSM.SORespId = "0";
                objSM.SOSalespId = "0";
                objSM.SOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.SOCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.SOApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.SOAcceptanceFlag = SM.SMStatus.New.ToString();

                objSM.SODelivery = txtDelivery.Text;
                objSM.SOPaymentTerms = txtPaymentTerms.Text;
                objSM.SOPackageCharges = txtPackingCharges.Text;
                objSM.SOExciseDuty = txtExciseDuty.Text;
                if (rbVAT.Checked == true) { objSM.SOVAT = txtVAT.Text; } else if (rbCST.Checked == true) { objSM.SOCSTax = txtVAT.Text; }
                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.SOGuarantee = txtGuarantee.Text;
                objSM.SOTransportCharges = txtTransCharges.Text;
                objSM.SOInsurance = txtInsurance.Text;
                objSM.SOErection = txtErrection.Text;
                objSM.SOJurisdiction = txtJurisdiction.Text;
                objSM.SOValidity = txtValidity.Text;
                objSM.SOInspection = txtInspection.Text;
                objSM.SOOtherSpec = txtOtherSpecs.Text;
                objSM.SOCurrencyTypeId = ddlCurrencyType.SelectedItem.Value;
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
                objSM.SOAdvanceAmt = txtAdvanceAmt.Text;
                objSM.SOAccessories = txtAccessories.Text;
                objSM.SOExtraSpares = txtExtraSpares.Text;
                objSM.SOCustPONo = txtCustPONo.Text;
                objSM.SOCustPODated = Yantra.Classes.General.toMMDDYYYY(txtCustPODated.Text);
                objSM.SOCSTNo = txtCSTNo.Text;
                objSM.SOTINNo = txtTINNo.Text;
                objSM.CpId = lblCPID.Text;
                objSM.Sototalamt = lblTotalamount.Text;
                objSM.SOCUSTID = ddlCustomer.SelectedItem.Value;
                objSM.SOFLag = SM.SMStatus.New.ToString();
                objSM.sosalestatus = ddlsalestatus.SelectedItem.Value;

                if (objSM.SalesOrder_Save() == "Data Saved Successfully")
                {
                    //if (FileUpload1.HasFile)
                    //{
                    //    objSM.SOUploadFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    //    objSM.SOFileContentType = Yantra.Classes.General.GetContentType(System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName));
                    //    //Stream fs = FileUpload1.PostedFile.InputStream;
                    //    //BinaryReader br = new BinaryReader(fs);
                    //    //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    //    objSM.SOfileBytes = localFileBytes;
                    //    objSM.SalesOrderUploads_Save();
                    //}
                    //else
                    //{
                    //    objSM.SOFiles = "";
                    //}
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text))
                    {
                        string[] fileEntries = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text);
                        foreach (string fileName in fileEntries)
                        {
                            string filenameofpath = System.IO.Path.GetFileName(fileName);
                            string[] filepart = filenameofpath.Split('-');
                            if (filepart[0] == "SO")
                            {
                                //FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + FileUpload1.PostedFile.FileName.ToString());
                                objSM.SOUploadFileName = filepart[2];
                                objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text + "-" + filepart[2];
                                objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles"))
                                { }
                                else
                                { Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles"); }
                                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "/" + filenameofpath, AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles/" + objSM.SOFileContentType);
                                objSM.SalesOrderUploads_Save();
                            }
                        }
                    }


                    //objSM.SalesOrderDetails_Delete(objSM.SOId);
                    foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                    {
                        objSM.SOItemCode = gvrow.Cells[2].Text;
                        objSM.SODetQty = gvrow.Cells[6].Text;
                        objSM.SORate = gvrow.Cells[8].Text;
                        objSM.SODetSpec = gvrow.Cells[11].Text;
                        //objSM.SODetRemarks = gvrow.Cells[10].Text;
                        // objSM.SODetPriority = gvrow.Cells[11].Text;
                        objSM.SODETDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                        objSM.SODetRoom = gvrow.Cells[13].Text;
                        objSM.SODetPrice = gvrow.Cells[10].Text;
                        objSM.ColorId = gvrow.Cells[15].Text;
                        objSM.BalanceQty = gvrow.Cells[6].Text;
                        objSM.Sales = gvrow.Cells[16].Text;
                        objSM.SalesOrderDetails_Save();
                    }
                    if (objSM.Get_Ids_Select(objSM.SOId) > 0)
                    {
                        //SM.SalesEnquiry.SalesEnquiryStatus_Update(SM.SMStatus.Closed, objSM.EnqId);
                        //SM.SalesAssignments.SalesAssignmentsStatus_Update(SM.SMStatus.Closed, objSM.AssignTaskId);
                        //SM.SalesQuotation.SalesQuotationStatus_Update(SM.SMStatus.Closed, objSM.QuotId);
                    }

                    //if (FileUpload1.HasFile)
                    //{
                    //    if (lbtnAttachedFiles.Text != "")
                    //    {
                    //        string[] ext = lbtnAttachedFiles.Text.Split('.');
                    //        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + "." + ext[1]))
                    //        { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + "." + ext[1]); }
                    //    }
                    //    FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + FileUpload1.PostedFile.FileName.ToString());
                    //}

                    SM.CommitTransaction();
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + ""))
                    { Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "", true); }
                    FileUploaderAJAX1.Reset();
                    MessageBox.Show(this, "Data Saved Successfully");
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
                btnDelete.Attributes.Clear();
                //gvSalesOrderDetails.DataBind();
                gvQuotationProducts.DataBind();
                gvSalesOrderItems.DataBind();
                tblSalesOrderDetails.Visible = false;
                SM.ClearControls(this);
                SM.Dispose();
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

        try
        {
            SM.SalesOrder objSM = new SM.SalesOrder();

            SM.BeginTransaction();

            objSM.SOId = Request.QueryString["SalesId"].ToString();
            objSM.SONo = txtSalesOrderNo.Text;
            objSM.SODate = Yantra.Classes.General.toMMDDYYYY(txtSalesOrderDate.Text);
            objSM.QuotId = ddlQuotationNo.SelectedItem.Value;

            objSM.SORespId = "0";
            objSM.SOSalespId = "0";
            objSM.SOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSM.SOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objSM.SOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objSM.SOAcceptanceFlag = SM.SMStatus.New.ToString();

            objSM.SODelivery = txtDelivery.Text;
            objSM.SOCurrencyTypeId = ddlCurrencyType.SelectedItem.Value;
            objSM.SOPaymentTerms = txtPaymentTerms.Text;
            objSM.SOPackageCharges = txtPackingCharges.Text;
            objSM.SOExciseDuty = txtExciseDuty.Text;
            if (rbVAT.Checked == true) { objSM.SOVAT = txtVAT.Text; } else if (rbCST.Checked == true) { objSM.SOCSTax = txtVAT.Text; }
            objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
            objSM.SOGuarantee = txtGuarantee.Text;
            objSM.SOTransportCharges = txtTransCharges.Text;
            objSM.SOInsurance = txtInsurance.Text;
            objSM.SOErection = txtErrection.Text;
            objSM.SOJurisdiction = txtJurisdiction.Text;
            objSM.SOValidity = txtValidity.Text;
            objSM.SOInspection = txtInspection.Text;
            objSM.SOOtherSpec = txtOtherSpecs.Text;

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
            objSM.SOAdvanceAmt = txtAdvanceAmt.Text;
            objSM.SOAccessories = txtAccessories.Text;
            objSM.SOExtraSpares = txtExtraSpares.Text;
            objSM.SOCustPONo = txtCustPONo.Text;
            objSM.SOCustPODated = Yantra.Classes.General.toMMDDYYYY(txtCustPODated.Text);
            objSM.SOCSTNo = txtCSTNo.Text;
            objSM.SOTINNo = txtTINNo.Text;
            objSM.CpId = lblCPID.Text;
            objSM.Sototalamt = lblTotalamount.Text;
            objSM.SOCUSTID = ddlCustomer.SelectedItem.Value;
            objSM.SOFLag = SM.SMStatus.New.ToString();
            objSM.sosalestatus = ddlsalestatus.SelectedItem.Value;


            if (objSM.SalesOrder_Update() == "Data Updated Successfully")
            {


                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text))
                {
                    string[] fileEntries = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text);
                    foreach (string fileName in fileEntries)
                    {
                        string filenameofpath = System.IO.Path.GetFileName(fileName);
                        string[] filepart = filenameofpath.Split('-');
                        if (filepart[0] == "SO")
                        {
                            //FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSM.SOId + FileUpload1.PostedFile.FileName.ToString());
                            objSM.SOUploadFileName = filepart[2];
                            objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text + "-" + filepart[2];
                            objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles"))
                            { }
                            else
                            {
                                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles");
                            }
                            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "/" + filenameofpath, AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles/" + objSM.SOFileContentType);
                            objSM.SalesOrderUploads_Save();
                        }
                    }
                }
                if (gvSalesOrderItems.Rows.Count > 0)
                {
                    // objSM.SalesOrderDetails_Delete(objSM.SOId);
                    foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                    {
                        objSM.SOItemCode = gvrow.Cells[2].Text;
                        objSM.SODetQty = gvrow.Cells[6].Text;
                        objSM.SORate = gvrow.Cells[8].Text;
                        objSM.SODetSpec = gvrow.Cells[11].Text;
                        //objSM.SODetRemarks = gvrow.Cells[10].Text;
                        // objSM.SODetPriority = gvrow.Cells[11].Text;
                        objSM.SODETDeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                        objSM.SODetRoom = gvrow.Cells[13].Text;
                        objSM.SODetPrice = gvrow.Cells[10].Text;
                        objSM.ColorId = gvrow.Cells[15].Text;
                        objSM.BalanceQty = gvrow.Cells[6].Text;
                        objSM.Sales = gvrow.Cells[16].Text;
                        objSM.SalesOrderDetails_Save();
                    }
                }
                if (objSM.Get_Ids_Select(objSM.SOId) > 0)
                {
                    SM.SalesEnquiry.SalesEnquiryStatus_Update(SM.SMStatus.Closed, objSM.EnqId);
                    SM.SalesAssignments.SalesAssignmentsStatus_Update(SM.SMStatus.Closed, objSM.AssignTaskId);
                    SM.SalesQuotation.SalesQuotationStatus_Update(SM.SMStatus.Closed, objSM.QuotId);
                }


                SM.CommitTransaction();

                //string[] fileEntriesDel = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "");
                //foreach (string fileName in fileEntriesDel)
                //{
                //    //string filenameofpath = System.IO.Path.GetFileName(fileName);
                //    //string[] filepart = filenameofpath.Split('-');
                //    //if (filepart[0] == "SO")
                //    //{
                //    //File.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "/" + filenameofpath);
                //    ;
                //    //}
                //}

                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + ""))
                { Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "/temp/" + lblEmpIdHidden.Text + "", true); }
                FileUploaderAJAX1.Reset();

                MessageBox.Show(this, "Data Updated Successfully");
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
            btnSave.Text = "Save";
            btnDelete.Attributes.Clear();
            //gvSalesOrderDetails.DataBind();
            gvQuotationProducts.DataBind();
            gvSalesOrderItems.DataBind();
            tblSalesOrderDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }


    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PoNo"] != null)
        {
            SM.ClearControls(this);
            try
            {
                SM.SalesOrder objSalesOrder = new SM.SalesOrder();

                if (objSalesOrder.SalesOrder_Select(Request.QueryString["SalesId"].ToString()) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblSalesOrderDetails.Visible = true;
                    txtSalesOrderNo.Text = objSalesOrder.SONo;
                    txtSalesOrderDate.Text = objSalesOrder.SODate;
                    ddlQuotationNo.SelectedValue = objSalesOrder.QuotId;

                    ddlPreparedBy.SelectedValue = objSalesOrder.SOPreparedBy;
                    ddlCheckedBy.SelectedValue = objSalesOrder.SOCheckedBy;
                    ddlApprovedBy.SelectedValue = objSalesOrder.SOApprovedBy;

                    txtDelivery.Text = objSalesOrder.SODelivery;
                    ddlCurrencyType.SelectedValue = objSalesOrder.SOCurrencyTypeId;
                    txtPaymentTerms.Text = objSalesOrder.SOPaymentTerms;
                    txtPackingCharges.Text = objSalesOrder.SOPackageCharges;
                    txtExciseDuty.Text = objSalesOrder.SOExciseDuty;
                    if (objSalesOrder.SOVAT != "")
                    {
                        txtVAT.Text = objSalesOrder.SOVAT;
                        lblVATCST.Text = "VAT";
                        rbVAT.Checked = true;
                        rbCST.Checked = false;
                    }
                    else if (objSalesOrder.SOCSTax != "")
                    {
                        txtVAT.Text = objSalesOrder.SOCSTax;
                        lblVATCST.Text = "C.S. Tax";
                        rbCST.Checked = true;
                        rbVAT.Checked = false;
                    }
                    ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                    txtGuarantee.Text = objSalesOrder.SOGuarantee;
                    txtTransCharges.Text = objSalesOrder.SOTransportCharges;
                    txtInsurance.Text = objSalesOrder.SOInsurance;
                    txtErrection.Text = objSalesOrder.SOErection;
                    txtJurisdiction.Text = objSalesOrder.SOJurisdiction;
                    txtValidity.Text = objSalesOrder.SOValidity;
                    txtInspection.Text = objSalesOrder.SOInspection;
                    txtOtherSpecs.Text = objSalesOrder.SOOtherSpec;

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
                    txtAdvanceAmt.Text = objSalesOrder.SOAdvanceAmt;
                    txtAccessories.Text = objSalesOrder.SOAccessories;
                    txtExtraSpares.Text = objSalesOrder.SOExtraSpares;
                    txtCustPONo.Text = objSalesOrder.SOCustPONo;
                    txtCustPODated.Text = objSalesOrder.SOCustPODated;
                    txtCSTNo.Text = objSalesOrder.SOCSTNo;
                    txtTINNo.Text = objSalesOrder.SOTINNo;
                    ddlsalestatus.SelectedItem.Value = objSalesOrder.sosalestatus;
                    lbtnAttachedFiles.Text = objSalesOrder.SOFiles;
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/SOFiles/" + objSalesOrder.SOFiles + "." + ext[1]))
                        {
                            lbtnAttachedFiles.Attributes.Add("onclick", "window.open('SOFiles/" + objSalesOrder.SOId + "." + ext[1] + "','SOFiles','resizable=yes,width=800,height=600,status=yes,toolbar=no,menubar=no');");
                        }
                        else
                        {
                            lbtnAttachedFiles.Text = "";
                            lbtnAttachedFiles.Attributes.Clear();
                        }
                    }
                    ddlQuotationNo_SelectedIndexChanged(sender, e);

                    objSalesOrder.SalesOrderDetails_Select(Request.QueryString["SalesId"].ToString(), gvDonepo);
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
                ddlQuotationNo_SelectedIndexChanged(sender, e);

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
        if (Request.QueryString["PoNo"]!=null)
        {
            try
            {
                SM.SalesOrder objSM = new SM.SalesOrder();
                MessageBox.Show(this, objSM.SalesOrder_Delete(Request.QueryString["SalesId"].ToString()));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                //gvSalesOrderDetails.DataBind();
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

    #region Quotation No Selected Index Changed
    protected void ddlQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.SalesQuotation objSM = new SM.SalesQuotation();
            if (objSM.SalesQuotation_Select(ddlQuotationNo.SelectedItem.Value) > 0)
            {
                txtQuotationDate.Text = objSM.QuotDate;
                ItemTypes_Fill();
                //if (btnSave.Text == "Save")
                //{
                if (objSM.QuotVAT != "")
                {
                    txtVAT.Text = objSM.QuotVAT;
                    lblVATCST.Text = "VAT";
                    rbVAT.Checked = true;
                    rbCST.Checked = false;

                }
                else if (objSM.QuotCST != "")
                {
                    txtVAT.Text = objSM.QuotCST;
                    lblVATCST.Text = "C.S. Tax";
                    rbCST.Checked = true;
                    rbVAT.Checked = false;

                }


                txtDelivery.Text = objSM.QuotDelivery;
                txtPaymentTerms.Text = objSM.QuotPayTerms;
                txtPackingCharges.Text = objSM.QuotPackCharges;
                txtExciseDuty.Text = objSM.QuotExcise;

                ddlDespatchMode.SelectedValue = objSM.DespmId;
                txtGuarantee.Text = objSM.QuotGuarantee;
                txtTransCharges.Text = objSM.QuotTransCharges;
                txtInsurance.Text = objSM.QuotInsurance;
                txtErrection.Text = objSM.QuotErrec;
                txtJurisdiction.Text = objSM.QuotJurisdiction;
                txtValidity.Text = objSM.QuotValidity;
                txtInspection.Text = objSM.QuotInspection;
                txtOtherSpecs.Text = objSM.QuotOtherSpecs;
                ddlCurrencyType.SelectedValue = "1";
                ddlsalestatus.SelectedValue = "1";

                //}

                objSM.SalesQuotationDetails_Select(ddlQuotationNo.SelectedItem.Value, gvQuotationProducts);

                ddlCustomer.SelectedValue = objSM.CustId;
                ddlCustomer_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objSM.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objSM.CustDetId;
                ddlContactPerson_SelectedIndexChanged(sender, e);


                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
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
                //else if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
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
            SM.Dispose();
        }
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
                rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                    txtCSTNo.Text = objSMCustomer.CSTNo;
                    txtTINNo.Text = objSMCustomer.LocalSTNo;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                rfvContactPerson.Enabled = false;
                rfvUnitName.Enabled = false;
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
                    txtCSTNo.Text = objSMCustomer.CSTNo;
                    txtTINNo.Text = objSMCustomer.LocalSTNo;
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
        SM.ClearControls(this);
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
        Response.Redirect("SalesOrder.aspx");
        tblSalesOrderDetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlColor.SelectedItem.Value == "") { ddlColor.SelectedItem.Value = "-"; }
        if (txtItemSpecifications.Text == "") { txtItemSpecifications.Text = "-"; }
        if (txtItemRemarks.Text == "") { txtItemRemarks.Text = "-"; }
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
        //col = new DataColumn("Remarks");
        //SalesOrderItems.Columns.Add(col);
        //col = new DataColumn("Priority");
        //SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Sales");
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
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemName"] = txtItemname.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["Rate"] = txtItemRate.Text;
                        dr["Specifications"] = txtItemSpecifications.Text;
                        //dr["Remarks"] = txtItemRemarks.Text;
                        //dr["Priority"] = ddlItemPriority.SelectedItem.Value;
                        dr["DeliveryDate"] = txtDeliveryDate.Text;
                        dr["Room"] = txtRoom.Text;
                        dr["Price"] = txtSpecialPrice.Text;
                        dr["Currency"] = ddlCurrencyType.SelectedItem.Text;

                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["Sales"] = ddlSales.SelectedItem.Value;

                        SalesOrderItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Currency"] = gvrow.Cells[7].Text;
                        dr["Rate"] = gvrow.Cells[8].Text;
                        dr["Specifications"] = gvrow.Cells[11].Text;
                        //dr["Remarks"] = gvrow.Cells[10].Text;
                        // dr["Priority"] = gvrow.Cells[11].Text;
                        dr["DeliveryDate"] = gvrow.Cells[12].Text;
                        dr["Room"] = gvrow.Cells[13].Text;
                        dr["Price"] = gvrow.Cells[10].Text;
                        dr["Color"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;
                        dr["Sales"] = gvrow.Cells[16].Text;

                        SalesOrderItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["Specifications"] = gvrow.Cells[11].Text;
                    //dr["Remarks"] = gvrow.Cells[10].Text;
                    // dr["Priority"] = gvrow.Cells[11].Text;
                    dr["DeliveryDate"] = gvrow.Cells[12].Text;
                    dr["Room"] = gvrow.Cells[13].Text;
                    dr["Price"] = gvrow.Cells[10].Text;

                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Sales"] = gvrow.Cells[16].Text;

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
                    if (gvrow.Cells[3].Text == ddlModelNo.SelectedItem.Text && gvrow.Cells[14].Text == ddlColor.SelectedItem.Text)
                    {
                        gvSalesOrderItems.DataSource = SalesOrderItems;
                        gvSalesOrderItems.DataBind();
                        MessageBox.Show(this, "The Item Code and Color you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvSalesOrderItems.SelectedIndex == -1)
        {
            DataRow drnew = SalesOrderItems.NewRow();
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemName"] = txtItemname.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;
            drnew["Rate"] = txtItemRate.Text;
            drnew["Specifications"] = txtItemSpecifications.Text;
            //  drnew["Remarks"] = txtItemRemarks.Text;
            // drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
            drnew["DeliveryDate"] = txtDeliveryDate.Text;
            drnew["Room"] = txtRoom.Text;
            drnew["Price"] = txtSpecialPrice.Text;
            drnew["Currency"] = ddlCurrencyType.SelectedItem.Text;

            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["Sales"] = ddlSales.SelectedItem.Value;

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
        ItemTypes_Fill();
        //ddlItemType.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemRate.Text = string.Empty;
        txtItemSpecifications.Text = string.Empty;
        txtItemRemarks.Text = string.Empty;
        ddlItemPriority.SelectedValue = "0";
        gvSalesOrderItems.SelectedIndex = -1;
        txtRoom.Text = string.Empty;
        txtSpecialPrice.Text = string.Empty;
        txtDiscount.Text = string.Empty;
        txtItemname.Text = string.Empty;
        ddlColor.SelectedValue = "0";


    }
    #endregion

    #region GridView Sales Order Items Row DataBound
    protected void gvSalesOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[9].Text = ((Convert.ToDouble(e.Row.Cells[10].Text)) / (Convert.ToDouble(e.Row.Cells[6].Text))).ToString("F");
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[9].Text);
            lblTotalamount.Text = TotalAmount.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlSales.SelectedItem.Text == "Sales")
            {
                TotalAmount1 = TotalAmount1 + Convert.ToDecimal(e.Row.Cells[10].Text);
                lblTotalamount.Text = TotalAmount1.ToString();
            }
        }



        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total Amount:";
            e.Row.Cells[10].Text = TotalAmount1.ToString();
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = false;
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
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);
        //col = new DataColumn("Priority");
        //SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Sales");
        SalesOrderItems.Columns.Add(col);

        if (gvSalesOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["Specifications"] = gvrow.Cells[11].Text;
                    dr["Price"] = gvrow.Cells[10].Text;
                    //dr["Priority"] = gvrow.Cells[11].Text;
                    dr["DeliveryDate"] = gvrow.Cells[12].Text;
                    dr["Room"] = gvrow.Cells[13].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Sales"] = gvrow.Cells[16].Text;

                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvSalesOrderItems.DataSource = SalesOrderItems;
        gvSalesOrderItems.DataBind();
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
    
    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PoNo"] !=null)
        {
            try
            {
               // string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=StatementPO&sono=" + Request.QueryString["SalesId"].ToString() + "";

                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesorder&sono=" + Request.QueryString["SalesId"].ToString() + "";
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

    protected void btnSend_Click(object sender, EventArgs e)
    {
    }

    #region Button APPROVE
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SM.SalesOrder objSMSOApprove = new SM.SalesOrder();
            SM.BeginTransaction();
            objSMSOApprove.SOId = Request.QueryString["SalesId"].ToString();
            objSMSOApprove.SOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMSOApprove.SalesOrderApprove_Update();
            SM.SalesOrder.SalesOrderStatus_Update(SM.SMStatus.Open, Request.QueryString["SalesId"].ToString());
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvSalesOrderDetails.DataBind();
            SM.Dispose();
            btnEdit_Click(sender, e);
            txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    #endregion

    #region Button SEND WORK ORDER
    protected void btnSendWorkOrder_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PoNo"]!=null)
        {
            Response.Redirect("WorkOrder.aspx?SOId=" + Request.QueryString["SalesId"].ToString() + "");
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
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);
        //col = new DataColumn("Priority");
        //SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Sales");
        SalesOrderItems.Columns.Add(col);

        if (gvSalesOrderItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Currency"] = gvrow.Cells[7].Text;
                dr["Rate"] = gvrow.Cells[8].Text;
                dr["Specifications"] = gvrow.Cells[11].Text;
                dr["Price"] = gvrow.Cells[10].Text;
                //dr["Priority"] = gvrow.Cells[11].Text;
                dr["DeliveryDate"] = gvrow.Cells[12].Text;
                dr["Room"] = gvrow.Cells[13].Text;
                dr["Color"] = gvrow.Cells[14].Text;
                dr["ColorId"] = gvrow.Cells[15].Text;
                dr["Sales"] = gvrow.Cells[16].Text;


                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvSalesOrderItems.Rows[e.NewEditIndex].RowIndex)
                {

                    ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlModelNo_SelectedIndexChanged(sender, e);

                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemRate.Text = gvrow.Cells[8].Text;
                    txtItemSpecifications.Text = gvrow.Cells[11].Text;
                    txtSpecialPrice.Text = gvrow.Cells[10].Text;
                    // ddlItemPriority.SelectedValue = gvrow.Cells[11].Text;
                    txtDeliveryDate.Text = gvrow.Cells[12].Text;
                    txtRoom.Text = gvrow.Cells[13].Text;
                    ddlColor.SelectedValue = gvrow.Cells[15].Text;
                    ddlSales.SelectedItem.Value = gvrow.Cells[16].Text;
                    gvSalesOrderItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvSalesOrderItems.DataSource = SalesOrderItems;
        gvSalesOrderItems.DataBind();
    }
    #endregion

    #region Link Button File Opener Click
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        LinkButton lbtnFileOpener;
        lbtnFileOpener = (LinkButton)sender;
        Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
        DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
        string command = "SELECT SO_CONTENT_TYPE FROM [YANTRA_SO_UPLOADS] WHERE SO_ID=" + Request.QueryString["SalesId"].ToString() + " AND SO_UPLOAD_FILENAME='" + lbtnFileOpener.Text + "'";
        dbcon.Open();
        string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
        string path = "../../YANTRA_DOCUMENTS/SOFiles/" + filename;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
    }
    #endregion

    #region Link Button Attached Files Click
    protected void lbtnAttachedFiles_Click(object sender, EventArgs e)
    {
        SM.SalesOrder objsm = new SM.SalesOrder();
        objsm.SOFiles = lbtnAttachedFiles.Text;
    }
    #endregion

    #region ManagePost
    protected void managePost()
    {
        HttpPostedFileAJAX pf = FileUploaderAJAX1.PostedFile;

        //if (pf.ContentType.Equals("image/gif") && pf.ContentLength <= 5 * 1024)
        FileUploaderAJAX1.SaveAs("~/temp/" + lblEmpIdHidden.Text + "/", "SO-" + DateTime.Now.Day + "-" + pf.FileName);
    }
    #endregion

    #region Ddl Model No Selected Index Changed
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
         try
        {

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtItemname.Text = objMaster.ItemName;
                SM.SalesQuotation obj = new SM.SalesQuotation();
                if (obj.SalesQuotationDetails1_Select(ddlModelNo.SelectedItem.Value, ddlQuotationNo.SelectedItem.Value) > 0)
                {
                    txtItemRate.Text = obj.QuotRate;
                    txtItemQuantity.Text = obj.QuotDetQty;
                    txtSpecialPrice.Text = obj.QuotDetSpPrice;
                    txtDiscount.Text = obj.QuotDetDisc;
                    txtRoom.Text = obj.QuotRoom;
                    // ddlColor.SelectedItem.Value = obj.ColorId;

                }
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
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

    protected void gvQuotationProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationProducts.Rows[e.RowIndex].Cells[1].Text;
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
        col = new DataColumn("Discount");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesOrderItems.Columns.Add(col);


        if (gvQuotationProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {

                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["Discount"] = gvrow.Cells[9].Text;
                    dr["SpPrice"] = gvrow.Cells[10].Text;
                    dr["Room"] = gvrow.Cells[11].Text;
                    dr["Color"] = gvrow.Cells[12].Text;
                    dr["ColorId"] = gvrow.Cells[13].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationProducts.DataSource = SalesOrderItems;
        gvQuotationProducts.DataBind();
    }

    #region GvQuoatation Products Row Editing
    protected void gvQuotationProducts_RowEditing(object sender, GridViewEditEventArgs e)
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
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesOrderItems.Columns.Add(col);

        if (gvQuotationProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationProducts.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Currency"] = gvrow.Cells[7].Text;
                dr["Rate"] = gvrow.Cells[8].Text;
                dr["Room"] = gvrow.Cells[11].Text;
                dr["ColorId"] = gvrow.Cells[13].Text;

                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvQuotationProducts.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlModelNo_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemRate.Text = gvrow.Cells[8].Text;
                    txtRoom.Text = gvrow.Cells[11].Text;
                    ddlColor.SelectedValue = gvrow.Cells[13].Text;
                    gvQuotationProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
    }
    #endregion

    protected void chkItemSelect_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvQuotationProducts.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
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
                col = new DataColumn("Price");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ReqDate");
                SalesOrderItems.Columns.Add(col);
                //col = new DataColumn("Priority");
                //SalesOrderItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Color");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Sales");
                SalesOrderItems.Columns.Add(col);



                if (gvSalesOrderItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvSalesOrderItems.Rows)
                    {
                        if (gvSalesOrderItems.SelectedIndex > -1)
                        {
                            if (gvrow.RowIndex == gvSalesOrderItems.SelectedRow.RowIndex)
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow.Cells[2].Text;
                                dr["ModelNo"] = gvrow.Cells[3].Text;
                                dr["ItemName"] = gvrow.Cells[4].Text;
                                dr["UOM"] = gvrow.Cells[5].Text;
                                dr["Quantity"] = gvrow.Cells[6].Text;
                                dr["Currency"] = gvrow.Cells[7].Text;
                                dr["Discount"] = gvrow.Cells[9].Text;
                                dr["Rate"] = gvrow.Cells[8].Text;
                                dr["SpPrice"] = gvrow.Cells[10].Text;
                                //dr["Remarks"] = "--";
                                // dr["DeliveryDate"] = txtDeliveryDate.Text;
                                dr["Room"] = gvrow.Cells[11].Text;
                                //dr["Price"] = gvrow.Cells[10].Text;
                                dr["Color"] = gvrow.Cells[12].Text;
                                dr["ColorId"] = gvrow.Cells[13].Text;
                                // dr["Sales"] = "Sales";
                                SalesOrderItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ModelNo"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                dr["Quantity"] = gvrow1.Cells[6].Text;
                                dr["Currency"] = gvrow1.Cells[7].Text;
                                //dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                                dr["Rate"] = gvrow1.Cells[8].Text;
                                dr["Specifications"] = gvrow1.Cells[11].Text;
                                //dr["Remarks"] = gvrow1.Cells[10].Text;
                                // dr["Priority"] = gvrow1.Cells[11].Text;
                                dr["DeliveryDate"] = txtDeliveryDate.Text;
                                dr["Room"] = gvrow1.Cells[13].Text;
                                dr["Price"] = gvrow1.Cells[10].Text;
                                dr["Color"] = gvrow1.Cells[14].Text;
                                dr["ColorId"] = gvrow1.Cells[15].Text;
                                dr["Sales"] = "Sales";
                                SalesOrderItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = SalesOrderItems.NewRow();
                            dr["ItemCode"] = gvrow1.Cells[2].Text;
                            dr["ModelNo"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;
                            dr["Currency"] = gvrow1.Cells[7].Text;
                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                            dr["Rate"] = gvrow1.Cells[8].Text;
                            dr["Specifications"] = gvrow1.Cells[11].Text;
                            // dr["Remarks"] = gvrow1.Cells[10].Text;
                            // dr["Priority"] = gvrow1.Cells[11].Text;
                            dr["DeliveryDate"] = txtDeliveryDate.Text;
                            dr["Room"] = gvrow1.Cells[13].Text;
                            dr["Price"] = gvrow1.Cells[10].Text;
                            dr["Color"] = gvrow1.Cells[14].Text;
                            dr["ColorId"] = gvrow1.Cells[15].Text;
                            dr["Sales"] = "Sales";
                            SalesOrderItems.Rows.Add(dr);
                        }
                        if (gvSalesOrderItems.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[3].Text == gvrow1.Cells[3].Text && gvrow.Cells[13].Text == gvrow1.Cells[15].Text)
                            {
                                gvSalesOrderItems.DataSource = SalesOrderItems;
                                gvSalesOrderItems.DataBind();
                                MessageBox.Show(this, "The Item Code and Color you have selected is already exists in list");
                                btnItemRefresh_Click(sender, e);
                                ch.Checked = false;
                                return;
                            }
                        }

                    }
                }
                if (gvSalesOrderItems.SelectedIndex == -1)
                {
                    DataRow drnew = SalesOrderItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[2].Text;
                    drnew["ModelNo"] = gvrow.Cells[3].Text;
                    drnew["ItemName"] = gvrow.Cells[4].Text;
                    drnew["UOM"] = gvrow.Cells[5].Text;
                    drnew["Quantity"] = gvrow.Cells[6].Text;
                    drnew["Currency"] = gvrow.Cells[7].Text;
                    //drnew["Currency"] = ddlCurrencyType.SelectedItem.Text;
                    drnew["Rate"] = gvrow.Cells[8].Text;
                    drnew["Specifications"] = "--";
                    //drnew["Remarks"] = "--";
                    //drnew["Priority"] = "Low";
                    drnew["DeliveryDate"] = txtDeliveryDate.Text;
                    drnew["Room"] = gvrow.Cells[11].Text;
                    drnew["Price"] = gvrow.Cells[10].Text;
                    drnew["Color"] = gvrow.Cells[12].Text;
                    drnew["ColorId"] = gvrow.Cells[13].Text;
                    drnew["Sales"] = "Sales";

                    SalesOrderItems.Rows.Add(drnew);
                }
                gvSalesOrderItems.DataSource = SalesOrderItems;
                gvSalesOrderItems.DataBind();
                gvSalesOrderItems.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }


        }

    }
    protected void btnAcceptence_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PoNo"]!=null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=orderacc&sono=" + Request.QueryString["SalesId"].ToString() + "";
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
    protected void gvQuotationProducts_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[13].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

            // e.Row.Cells[14].Visible = false;

        }
    }
    #region Rdb All change
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        lblSearch.Visible = true;
        txtSearchModel.Visible = true;
        btnSearchModelNo.Visible = true;
        lblSearchBrand.Visible = true;
        ddlBrand.Visible = true;
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
    }
    #endregion
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster5_Select(ddlModelNo, ddlBrand.SelectedItem.Value);
    }

    protected void gvDonepo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[9].Text = ((Convert.ToDouble(e.Row.Cells[10].Text)) / (Convert.ToDouble(e.Row.Cells[6].Text))).ToString("F");
            TotalAmount2 = TotalAmount2 + Convert.ToDecimal(e.Row.Cells[10].Text);
            //lblTotalamount.Text = TotalAmount.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (ddlSales.SelectedItem.Text == "Sales")
            //{
            //    TotalAmount1 = TotalAmount1 + Convert.ToDecimal(e.Row.Cells[10].Text);
            //    lblTotalamount.Text = TotalAmount1.ToString();
            //}
        }



        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total Amount:";
            e.Row.Cells[10].Text = TotalAmount2.ToString();
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = false;
        }

    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvDonepo.SelectedIndex = gvRow.RowIndex;
        SM.SalesOrder objSalesOrder = new SM.SalesOrder();
        objSalesOrder.SalesOrderDetailsDone_Delete(gvDonepo.SelectedRow.Cells[1].Text);
        MessageBox.Show(this, "Data Deleted");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=StatementPO&sono=" + Request.QueryString["SalesId"].ToString() + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
}
 
