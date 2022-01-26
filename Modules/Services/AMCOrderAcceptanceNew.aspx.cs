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
public partial class Modules_Services_AMCOrderAcceptanceNew : basePage
{
    string scheduleNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        scheduleNo = Request.QueryString["scheduleNo"];
        if(!IsPostBack)
        {
            if (scheduleNo == "")
            {
                Services.ClearControls(this);
                // gvOrderAcceptanceDetails.SelectedIndex = -1;
                txtOANo.Text = Services.AMCOrderAcceptance.AMCOrderAcceptance_AutoGenCode();
                txtOADate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                ddlSONo.Enabled = true;
                tblOrderAcceptanceDetails.Visible = true;
                gvSalesOrderItems.DataBind();
                gvQuotationItems.DataBind();
            }
            else
            {
                try
                {
                    Services.AMCOrderAcceptance objOrderAcceptance = new Services.AMCOrderAcceptance();
                    if (objOrderAcceptance.AMCOrderAcceptance_Select(scheduleNo) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        ddlSONo.Enabled = false;

                        tblOrderAcceptanceDetails.Visible = true;
                        txtOANo.Text = objOrderAcceptance.OANo;
                        txtOADate.Text = objOrderAcceptance.OADate;
                        ddlSONo.SelectedValue = objOrderAcceptance.SOId;
                        ddlPreparedBy.SelectedValue = objOrderAcceptance.OAPreparedBy;
                        ddlCheckedBy.SelectedValue = objOrderAcceptance.OACheckedBy;
                        ddlApprovedBy.SelectedValue = objOrderAcceptance.OAApprovedBy;
                        txtConsignee.Text = objOrderAcceptance.OAConsignee;
                        txtPmVisits.Text = objOrderAcceptance.OAPMVisits;
                        txtPMSchedule.Text = objOrderAcceptance.OAPMSchedule;
                        txtPayment.Text = objOrderAcceptance.OAPayment;
                        txtPaymentStatus.Text = objOrderAcceptance.OAPaymentStatus;
                        txtInvoiceStatus.Text = objOrderAcceptance.OAInvoiceStatus;
                        txtInvoiceDetails.Text = objOrderAcceptance.OAInvoiceDetails;
                        txtAmcAmt.Text = objOrderAcceptance.OAAMCAmt;
                        txtCST.Text = objOrderAcceptance.OAServiceTax;
                        txtResponsiblePerson.Text = objOrderAcceptance.OARespPerson;
                        txtFollowupEmail.Text = objOrderAcceptance.OARespPersonEmail;
                        objOrderAcceptance.AMCOrderAcceptancePMCallDetails_Select(scheduleNo, gvQuotationItems);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    //btnDelete.Attributes.Clear();
                    Services.Dispose();
                    ddlSONo_SelectedIndexChanged(sender, e);
                    ddlResponsiblePerson_SelectedIndexChanged(sender, e);
                }
            }


            DeliveryType_Fill();
            CurrencyType_Fill();
            SalesOrder_Fill();
            EmployeeMaster_Fill();
            Customer_Fill();
        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (txtAmcAmt.Text == "" || txtAmcAmt.Text == string.Empty) { txtAmcAmt.Text = "0"; }
        if (txtCST.Text == "" || txtCST.Text == string.Empty) { txtCST.Text = "0"; }
        txtAmcTotAmt.Text = Convert.ToString(double.Parse(txtAmcAmt.Text) + (double.Parse(txtCST.Text) * double.Parse(txtCST.Text) / 100));

        if (scheduleNo!=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]) && Request.QueryString["status"] != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnPrint.Visible = true;
                btnSend.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = true;
                btnPrint.Visible = false;
                btnSend.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnPrint.Visible = false;
            btnSend.Visible = false;
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

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            Services.AMCWorkOrder.AMCWorkOrder_Select(ddlSONo);
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


    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            OrderAcceptanceSave();
        }
        else if (btnSave.Text == "Update")
        {
            OrderAcceptanceUpdate();
        }
    }
      #endregion

      #region OrderAcceptanceUpdate
private void OrderAcceptanceUpdate()
{
 	 try
        {
            Services.AMCOrderAcceptance objOrderAcceptance = new Services.AMCOrderAcceptance();

            Services.BeginTransaction();

            objOrderAcceptance.OAId = scheduleNo;
            objOrderAcceptance.OANo = txtOANo.Text;
            objOrderAcceptance.OADate = Yantra.Classes.General.toMMDDYYYY(txtOADate.Text);
            objOrderAcceptance.SOId = ddlSONo.SelectedItem.Value;
            objOrderAcceptance.OARespId = "0";
            objOrderAcceptance.OASalespId = ddlSalesPerson.SelectedItem.Value;
            objOrderAcceptance.OAPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objOrderAcceptance.OACheckedBy = ddlCheckedBy.SelectedItem.Value;
            objOrderAcceptance.OAApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objOrderAcceptance.OAConsignee = txtConsignee.Text;
            objOrderAcceptance.OAPMVisits = txtPmVisits.Text;
            objOrderAcceptance.OAPMSchedule = txtPMSchedule.Text;
            objOrderAcceptance.OAPayment = txtPayment.Text;
            objOrderAcceptance.OAPaymentStatus = txtPaymentStatus.Text;
            objOrderAcceptance.OAInvoiceStatus = txtInvoiceStatus.Text;
            objOrderAcceptance.OAInvoiceDetails = txtInvoiceDetails.Text;
            objOrderAcceptance.OAAMCAmt = txtAmcAmt.Text;
            objOrderAcceptance.OAServiceTax = txtCST.Text;
            objOrderAcceptance.OARespPerson = txtResponsiblePerson.Text;
            objOrderAcceptance.OARespPersonEmail = txtFollowupEmail.Text;
            objOrderAcceptance.OAFlag = Services.ServicesStatus.New.ToString();

            if (objOrderAcceptance.AMCOrderAcceptance_Update() == "Data Updated Successfully")
            {
                objOrderAcceptance.AMCOrderAcceptanceDetails_Delete(objOrderAcceptance.OAId);
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {
                    objOrderAcceptance.OAItemCode = gvrow.Cells[0].Text;
                    objOrderAcceptance.OADetQty = gvrow.Cells[3].Text;
                    objOrderAcceptance.OARate = gvrow.Cells[4].Text;
                    objOrderAcceptance.OADetSpec = gvrow.Cells[6].Text;
                    objOrderAcceptance.OADetRemarks = gvrow.Cells[7].Text;
                    //objOrderAcceptance.OADetPriority = gvrow.Cells[8].Text;
                    objOrderAcceptance.AMCOrderAcceptanceDetails_Save();
                }
                objOrderAcceptance.AMCOrderAcceptancePMCallDetails_Delete(objOrderAcceptance.OAId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objOrderAcceptance.OACallName = gvrow.Cells[2].Text;
                    objOrderAcceptance.OACallDate = gvrow.Cells[3].Text;
                    objOrderAcceptance.AMCOrderAcceptancePMCallDetails_Save();
                }
                Services.AMCOrder.AMCOrderStatus_Update(Services.ServicesStatus.Closed, objOrderAcceptance.SOId);
                Services.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
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
            //btnDelete.Attributes.Clear();
            //gvOrderAcceptanceDetails.DataBind();
            gvQuotationItems.DataBind();
            tblOrderAcceptanceDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }

}
    #endregion

    #region OrderAcceptanceSave
    private void OrderAcceptanceSave()
    {
        Services.AMCOrderAcceptance objsr = new Services.AMCOrderAcceptance();
        if (objsr.AMCOrderAcceptance_isrecordexists(ddlSONo.SelectedItem.Value) > 0)
        {
            MessageBox.Show(this, "order acceptance for " + ddlSONo.SelectedItem.Text + " already prepared");
            Yantra.Classes.General.ClearControls(this);
            return;
        }
        try
        {
            Services.AMCOrderAcceptance objOrderAcceptance = new Services.AMCOrderAcceptance();
            Services.BeginTransaction();
            objOrderAcceptance.OANo = txtOANo.Text;
            objOrderAcceptance.OADate = Yantra.Classes.General.toMMDDYYYY(txtOADate.Text);
            objOrderAcceptance.SOId = ddlSONo.SelectedItem.Value;

            objOrderAcceptance.OARespId = "0";
            objOrderAcceptance.OASalespId = ddlSalesPerson.SelectedItem.Value;
            objOrderAcceptance.OAPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objOrderAcceptance.OACheckedBy = ddlCheckedBy.SelectedItem.Value;
            objOrderAcceptance.OAApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objOrderAcceptance.OAConsignee = txtConsignee.Text;
            objOrderAcceptance.OAPMVisits = txtPmVisits.Text;
            objOrderAcceptance.OAPMSchedule = txtPMSchedule.Text;
            objOrderAcceptance.OAPayment = txtPayment.Text;
            objOrderAcceptance.OAPaymentStatus = txtPaymentStatus.Text;
            objOrderAcceptance.OAInvoiceStatus = txtInvoiceStatus.Text;
            objOrderAcceptance.OAInvoiceDetails = txtInvoiceDetails.Text;
            objOrderAcceptance.OAAMCAmt = txtAmcAmt.Text;
            objOrderAcceptance.OAServiceTax = txtCST.Text;
            objOrderAcceptance.OAFlag = Services.ServicesStatus.New.ToString();
            objOrderAcceptance.OARespPerson = txtResponsiblePerson.Text;
            objOrderAcceptance.OARespPersonEmail = txtFollowupEmail.Text;

            if (objOrderAcceptance.AMCOrderAcceptance_Save() == "Data Saved Successfully")
            {
                objOrderAcceptance.AMCOrderAcceptanceDetails_Delete(objOrderAcceptance.OAId);
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {

                    objOrderAcceptance.OAItemCode = gvrow.Cells[0].Text;
                    objOrderAcceptance.OADetQty = gvrow.Cells[3].Text;
                    objOrderAcceptance.OARate = gvrow.Cells[4].Text;
                    objOrderAcceptance.OADetSpec = gvrow.Cells[6].Text;
                    objOrderAcceptance.OADetRemarks = gvrow.Cells[7].Text;
                    //objOrderAcceptance.OADetPriority = gvrow.Cells[8].Text;
                    objOrderAcceptance.AMCOrderAcceptanceDetails_Save();
                }
                objOrderAcceptance.AMCOrderAcceptancePMCallDetails_Delete(objOrderAcceptance.OAId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objOrderAcceptance.OACallName = gvrow.Cells[2].Text;
                    objOrderAcceptance.OACallDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                    objOrderAcceptance.AMCOrderAcceptancePMCallDetails_Save();
                }
                Services.AMCOrder.AMCOrderStatus_Update(Services.ServicesStatus.Closed, objOrderAcceptance.SOId);
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
        //    btnDelete.Attributes.Clear();
        //    gvOrderAcceptanceDetails.DataBind();
            gvQuotationItems.DataBind();
            tblOrderAcceptanceDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion

    #region SO No Selected Index Changed
    protected void ddlSONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.AMCWorkOrder objSalesOrder = new Services.AMCWorkOrder();
            if (objSalesOrder.AMCWorkOrder_Select(ddlSONo.SelectedItem.Value) > 0)
            {
                txtSODate.Text = objSalesOrder.WODate;
                objSalesOrder.AMCWorkOrderDetails_Select(ddlSONo.SelectedItem.Value, gvSalesOrderItems);
                if (btnSave.Text == "Save")
                {
                    txtCST.Text = objSalesOrder.WOServiceTax;
                    txtPmVisits.Text = objSalesOrder.WOPMCalls;
                    txtBDCalls.Text = objSalesOrder.WOBDCalls;
                    objSalesOrder.AMCWorkOrderPMCallDetails_Select(ddlSONo.SelectedItem.Value, gvQuotationItems);
                }
                Services.AMCOrder objAMCO = new Services.AMCOrder();
                if (objAMCO.AMCOrder_Select(objSalesOrder.AMCOId) > 0)
                {
                    lblAMCOIdHidden.Text = objAMCO.AMCOId;
                    if (btnSave.Text == "Save")
                    {
                        txtConsignee.Text = objAMCO.AMCOConsignee;
                        txtResponsiblePerson.Text = objAMCO.AMCOResponsiblePerson;
                        txtFollowupEmail.Text = objAMCO.AMCOResponsiblePersonEmail;
                    }
                    ddlCustomerName.SelectedValue = objAMCO.CustId;
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                    ddlUnitName.SelectedValue = objAMCO.CustUnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
                    ddlContactPerson.SelectedValue = objAMCO.CustDetId;
                    ddlContactPerson_SelectedIndexChanged(sender, e);
                }

                //Services.CustomerMaster objSMCustomer = new Services.CustomerMaster();
                //if (objSMCustomer.CustomerMaster_Select(objSalesOrder.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //}
                txtAmcAmt.Text = "0";
                foreach (GridViewRow gvRow in gvSalesOrderItems.Rows)
                {
                    txtAmcAmt.Text = Convert.ToString(decimal.Parse(txtAmcAmt.Text) + decimal.Parse(gvRow.Cells[5].Text));
                }
            }
            else
            {
                MessageBox.Show(this, "The order profile " + ddlSONo.SelectedItem.Value + " is Deleted so u cannot prepare orderacceptance");
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
           // btnDelete.Attributes.Clear();
            Services.Dispose();
            if (txtOANo.Text == String.Empty)
            {
                txtOANo.Text = Services.AMCOrderAcceptance.AMCOrderAcceptance_AutoGenCode();
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

    #region GridView SalesOrder Products Row Databound
    protected void gvSalesOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = (Convert.ToInt32(e.Row.Cells[4].Text) * Convert.ToInt32(e.Row.Cells[3].Text)).ToString();
        }
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
        gvSalesOrderItems.DataBind();
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("AMCOrderAcceptance.aspx");
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

    #region GridView OrderAcceptance Details Row DataBound
    protected void gvOrderAcceptanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (scheduleNo!=null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=amcoa&oano=" + scheduleNo + "";
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

    #region Button APPROVE Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCOrderAcceptance objSMOAApprove = new Services.AMCOrderAcceptance();
            Services.BeginTransaction();
            objSMOAApprove.OAId = scheduleNo;
            objSMOAApprove.OAApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            Services.AMCAssignment objassn = new Services.AMCAssignment();
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                objassn.AMCADate = DateTime.Now.ToShortDateString();
                objassn.AMCAScheduleDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                objassn.AMCOId = lblAMCOIdHidden.Text;
                objassn.AMCOAId = scheduleNo;
                objassn.AMCAssignment_Save();
            }


            MessageBox.Show(this, objSMOAApprove.AMCOrderAcceptanceApprove_Update());
            Services.AMCOrder.AMCOrderStatus_Update(Services.ServicesStatus.Open, scheduleNo);
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
          //  gvOrderAcceptanceDetails.DataBind();
            Services.Dispose();
           // btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Button SEND Click
    protected void btnSend_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("callname");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("calldate");
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
                        dr["callname"] = txtCallName.Text;
                        dr["calldate"] = txtCallDate.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["callname"] = gvrow.Cells[2].Text;
                        dr["calldate"] = gvrow.Cells[3].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["callname"] = gvrow.Cells[2].Text;
                    dr["calldate"] = gvrow.Cells[3].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }


        if (gvQuotationItems.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();
            drnew["callname"] = txtCallName.Text;
            drnew["calldate"] = txtCallDate.Text;
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
        txtCallName.Text = string.Empty;
        txtCallDate.Text = string.Empty;
    }
    #endregion

    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    #region GridView Quotation Items Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("callname");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("calldate");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["callname"] = gvrow.Cells[2].Text;
                    dr["calldate"] = gvrow.Cells[3].Text;
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
        col = new DataColumn("callname");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("calldate");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["callname"] = gvrow.Cells[2].Text;
                dr["calldate"] = gvrow.Cells[3].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {
                    txtCallName.Text = gvrow.Cells[2].Text;
                    txtCallDate.Text = gvrow.Cells[3].Text;
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }

    #endregion
}
 
