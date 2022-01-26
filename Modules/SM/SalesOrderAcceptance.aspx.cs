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

public partial class Modules_SM_SalesOrderAcceptance : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // txtOADate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //    txtDeliveryDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            DeliveryType_Fill();
            CurrencyType_Fill();
            WorkOrder_Fill();
            EmployeeMaster_Fill();
            TransporterName_Fill();
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvOrderAcceptanceDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvOrderAcceptanceDetails.SelectedRow.Cells[9].Text) && gvOrderAcceptanceDetails.SelectedRow.Cells[9].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
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
            if (gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text == "&nbsp;")
            {
                btnSave.Visible = true;
                btnRefresh.Visible = true;
                btnApprove.Visible = false;
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

    #region WorkOrder Fill
    private void WorkOrder_Fill()
    {
        try
        {
            SM.WorkOrder.WorkOrder_Select(ddlSONo);
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
    #region WorkOrder Fill
    private void WorkOrderALL_Fill()
    {
        try
        {
            SM.WorkOrder.WorkOrder_SelectAll(ddlSONo);
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

    #region Transporter Name Fill
    private void TransporterName_Fill()
    {
        try
        {
            Masters.TrasnporterMaster.TransporterMaster_Select(ddlTransporter);
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

    protected void lbtnWorkOrderNo_Click(object sender, EventArgs e)
    {
        lbtnOrderAcceptanceNo_Click(sender, e);
    }

    #region Link Button OrderAcceptanceNo_Click
    protected void lbtnOrderAcceptanceNo_Click(object sender, EventArgs e)
    {
        tblOrderAcceptanceDetails.Visible = false;
        LinkButton lbtnOrderAcceptanceNo;
        lbtnOrderAcceptanceNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnOrderAcceptanceNo.Parent.Parent;
        gvOrderAcceptanceDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        if (gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text == "&nbsp;")
        {
            tblOrderAcceptanceDetails.Visible = true;
            WorkOrder_Fill();
            btnNew_Click(sender, e);
            ddlSONo.SelectedValue = gvOrderAcceptanceDetails.SelectedRow.Cells[1].Text;
            ddlSONo_SelectedIndexChanged(sender, e);
            return;
        }
        else
        {
            btnEdit_Click(sender, e);
        }
        btnSave.Enabled = false;
    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //gvOrderAcceptanceDetails.SelectedIndex = -1;
        SM.ClearControls(this);
        txtOANo.Text = SM.OrderAcceptance.OrderAcceptance_AutoGenCode();
        txtOADate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //btnSave.Text = "Save";
        tblOrderAcceptanceDetails.Visible = true;
        gvSalesOrderItems.DataBind();
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

    #region OrderAcceptanceSave
    private void OrderAcceptanceSave()
    {
        try
        {
            SM.OrderAcceptance objOrderAcceptance = new SM.OrderAcceptance();

            SM.BeginTransaction();
            objOrderAcceptance.OANo = txtOANo.Text;
            objOrderAcceptance.OADate = Yantra.Classes.General.toMMDDYYYY(txtOADate.Text);
            objOrderAcceptance.WOId = ddlSONo.SelectedItem.Value;

            objOrderAcceptance.OARespId = ddlResponsiblePerson.SelectedItem.Value;
            objOrderAcceptance.OASalespId = ddlSalesPerson.SelectedItem.Value;
            objOrderAcceptance.OAPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objOrderAcceptance.OACheckedBy = ddlCheckedBy.SelectedItem.Value;
            objOrderAcceptance.OAApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objOrderAcceptance.OAConsignee = txtConsignee.Text;
            objOrderAcceptance.DespmId = ddlDespatchMode.SelectedItem.Value;
            objOrderAcceptance.TransId = ddlTransporter.SelectedItem.Value;
            objOrderAcceptance.OADeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objOrderAcceptance.OACSTax = txtCST.Text;
            objOrderAcceptance.OAInspection = txtInspection.Text;
            objOrderAcceptance.OAInvoiceTo = txtInvoiceTo.Text;
            objOrderAcceptance.OAFlag = SM.SMStatus.New.ToString();

            if (objOrderAcceptance.OrderAcceptance_Save() == "Data Saved Successfully")
            {
                objOrderAcceptance.OrderAcceptanceDetails_Delete(objOrderAcceptance.OAId);
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {

                    objOrderAcceptance.OAItemCode = gvrow.Cells[0].Text;
                    objOrderAcceptance.OADetQty = gvrow.Cells[3].Text;
                    objOrderAcceptance.OARate = gvrow.Cells[4].Text;
                    objOrderAcceptance.OADetSpec = gvrow.Cells[6].Text;
                    objOrderAcceptance.OADetRemarks = gvrow.Cells[7].Text;
                    objOrderAcceptance.OADetPriority = gvrow.Cells[8].Text;


                    objOrderAcceptance.OrderAcceptanceDetails_Save();
                }
                SM.WorkOrder.WorkOrderStatus_Update(SM.SMStatus.Closed, objOrderAcceptance.SOId);
                SM.CommitTransaction();
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
            gvOrderAcceptanceDetails.DataBind();
            tblOrderAcceptanceDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region OrderAcceptanceUpdate
    private void OrderAcceptanceUpdate()
    {
        try
        {
            SM.OrderAcceptance objOrderAcceptance = new SM.OrderAcceptance();

            SM.BeginTransaction();

            objOrderAcceptance.OAId = gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text;
            objOrderAcceptance.OANo = txtOANo.Text;
            objOrderAcceptance.OADate = Yantra.Classes.General.toMMDDYYYY(txtOADate.Text);
            objOrderAcceptance.WOId = ddlSONo.SelectedItem.Value;
            objOrderAcceptance.OARespId = ddlResponsiblePerson.SelectedItem.Value;
            objOrderAcceptance.OASalespId = ddlSalesPerson.SelectedItem.Value;
            objOrderAcceptance.OAPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objOrderAcceptance.OACheckedBy = ddlCheckedBy.SelectedItem.Value;
            objOrderAcceptance.OAApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objOrderAcceptance.OAConsignee = txtConsignee.Text;
            objOrderAcceptance.DespmId = ddlDespatchMode.SelectedItem.Value;
            objOrderAcceptance.TransId = ddlTransporter.SelectedItem.Value;
            objOrderAcceptance.OADeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objOrderAcceptance.OACSTax = txtCST.Text;
            objOrderAcceptance.OAInspection = txtInspection.Text;
            objOrderAcceptance.OAInvoiceTo = txtInvoiceTo.Text;
            objOrderAcceptance.OAFlag = SM.SMStatus.New.ToString();

            if (objOrderAcceptance.OrderAcceptance_Update() == "Data Updated Successfully")
            {
                objOrderAcceptance.OrderAcceptanceDetails_Delete(objOrderAcceptance.OAId);
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {
                    objOrderAcceptance.OAItemCode = gvrow.Cells[0].Text;
                    objOrderAcceptance.OADetQty = gvrow.Cells[3].Text;
                    objOrderAcceptance.OARate = gvrow.Cells[4].Text;
                    objOrderAcceptance.OADetSpec = gvrow.Cells[6].Text;
                    objOrderAcceptance.OADetRemarks = gvrow.Cells[7].Text;
                    objOrderAcceptance.OADetPriority = gvrow.Cells[8].Text;

                    objOrderAcceptance.OrderAcceptanceDetails_Save();
                }
                SM.WorkOrder.WorkOrderStatus_Update(SM.SMStatus.Closed, objOrderAcceptance.SOId);
                SM.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
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
            gvOrderAcceptanceDetails.DataBind();
            tblOrderAcceptanceDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }

    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvOrderAcceptanceDetails.SelectedIndex > -1)
        {
            try
            {
                if (gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text == "&nbsp;")
                {
                    return;
                }
                WorkOrderALL_Fill();
                SM.OrderAcceptance ObjOrderAcceptance = new SM.OrderAcceptance();
                if (ObjOrderAcceptance.OrderAcceptance_Select(gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblOrderAcceptanceDetails.Visible = true;
                    txtOANo.Text = ObjOrderAcceptance.OANo;
                    txtOADate.Text = ObjOrderAcceptance.OADate;
                    ddlSONo.SelectedValue = ObjOrderAcceptance.WOId;
                    ddlResponsiblePerson.SelectedValue = ObjOrderAcceptance.OARespId;
                    ddlPreparedBy.SelectedValue = ObjOrderAcceptance.OAPreparedBy;
                    ddlCheckedBy.SelectedValue = ObjOrderAcceptance.OACheckedBy;
                    ddlApprovedBy.SelectedValue = ObjOrderAcceptance.OAApprovedBy;
                    txtConsignee.Text = ObjOrderAcceptance.OAConsignee;
                    ddlDespatchMode.SelectedValue = ObjOrderAcceptance.DespmId;
                    ddlTransporter.SelectedValue = ObjOrderAcceptance.TransId;
                    txtDeliveryDate.Text = ObjOrderAcceptance.OADeliveryDate;
                    txtCST.Text = ObjOrderAcceptance.OACSTax;
                    txtInspection.Text = ObjOrderAcceptance.OAInspection;
                    txtInvoiceTo.Text = ObjOrderAcceptance.OAInvoiceTo;
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
                ddlSONo_SelectedIndexChanged(sender, e);
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
        if (gvOrderAcceptanceDetails.SelectedIndex > -1)
        {
            try
            {
                SM.OrderAcceptance ObjOrderAcceptance = new SM.OrderAcceptance();
                MessageBox.Show(this, ObjOrderAcceptance.OrderAcceptance_Delete(gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvOrderAcceptanceDetails.DataBind();
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

    #region SO No Selected Index Changed
    protected void ddlSONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////////SM.SalesOrder objSalesOrder = new SM.SalesOrder();
            ////////if (objSalesOrder.SalesOrder_Select(ddlSONo.SelectedItem.Value) > 0)
            ////////{
            ////////    txtSODate.Text = objSalesOrder.SODate;
            ////////    txtDelivery.Text = objSalesOrder.SODelivery;
            ////////    ddlCurrencyType.SelectedValue = objSalesOrder.SOCurrencyTypeId;
            ////////    txtPaymentTerms.Text = objSalesOrder.SOPaymentTerms;
            ////////    txtPackingCharges.Text = objSalesOrder.SOPackageCharges;
            ////////    txtExciseDuty.Text = objSalesOrder.SOExciseDuty;
            ////////    txtCST.Text = objSalesOrder.SOCSTax;
            ////////    txtGuarantee.Text = objSalesOrder.SOGuarantee;
            ////////    txtInspection.Text = objSalesOrder.SOInspection;
            ////////    txtOtherSpecs.Text = objSalesOrder.SOOtherSpec;
            ////////    if (btnSave.Text == "Save")
            ////////    {
            ////////        txtConsignee.Text = objSalesOrder.ConsignmentTo;
            ////////        ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
            ////////    }
            ////////    objSalesOrder.SalesOrderDetails_Select(ddlSONo.SelectedItem.Value, gvSalesOrderItems);

            ////////    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            ////////    if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objSM.CustId) > 0)
            ////////    {
            ////////        txtCustName.Text = objSMCustomer.CustName;
            ////////        txtAddress.Text = objSMCustomer.Address;
            ////////        txtEmail.Text = objSMCustomer.Email;
            ////////        txtRegion.Text = objSMCustomer.RegName;
            ////////        txtPhone.Text = objSMCustomer.Phone;
            ////////        txtMobile.Text = objSMCustomer.Mobile;
            ////////        txtUnitName.Text = objSMCustomer.CustUnitName;
            ////////    }
            ////////    else if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
            ////////    {
            ////////        txtCustName.Text = objSMCustomer.CustName;
            ////////        txtAddress.Text = objSMCustomer.Address;
            ////////        txtEmail.Text = objSMCustomer.Email;
            ////////        txtRegion.Text = objSMCustomer.RegName;
            ////////        txtPhone.Text = objSMCustomer.Phone;
            ////////        txtMobile.Text = objSMCustomer.Mobile;
            ////////        txtUnitName.Text = "--";
            ////////    }
            ////////}

            SM.WorkOrder objWO = new SM.WorkOrder();
            if (objWO.WorkOrder_Select(ddlSONo.SelectedItem.Value) > 0)
            {
                txtSODate.Text = objWO.WODate;
                txtCST.Text = objWO.WOCSTax;

                SM.SalesOrder objSalesOrder = new SM.SalesOrder();
                if (objSalesOrder.SalesOrder_Select(objWO.SOId) > 0)
                {
                    txtSODate.Text = objSalesOrder.SODate;
                    txtDelivery.Text = objSalesOrder.SODelivery;
                    ddlCurrencyType.SelectedValue = objSalesOrder.SOCurrencyTypeId;
                    txtPaymentTerms.Text = objSalesOrder.SOPaymentTerms;
                    txtPackingCharges.Text = objSalesOrder.SOPackageCharges;
                    txtExciseDuty.Text = objSalesOrder.SOExciseDuty;
                    txtGuarantee.Text = objSalesOrder.SOGuarantee;
                    txtOtherSpecs.Text = objSalesOrder.SOOtherSpec;
                    if (btnSave.Text == "Save")
                    {
                        txtInspection.Text = objSalesOrder.SOInspection;
                        txtConsignee.Text = objSalesOrder.ConsignmentTo;
                        txtInvoiceTo.Text = objSalesOrder.InvoiceTo;
                        ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                    }
                    SM.SalesAssignments objSA = new SM.SalesAssignments();
                    if (objSA.SalesAssignments_Select(objSalesOrder.EnqId) > 0)
                    {
                        ddlSalesPerson.SelectedValue = objSA.EmpId;
                        ddlSalesPerson_SelectedIndexChanged(sender, e);
                    }
                    objSalesOrder.SalesOrderDetails_Select(objSalesOrder.SOId, gvSalesOrderItems);
                }

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objWO.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text =  "--";
                }
                
                else if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objWO.CustId) > 0)    
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    
                    txtUnitName.Text = objSMCustomer.CustUnitName;
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
            SM.Dispose();
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
        SM.ClearControls(this);
        gvSalesOrderItems.DataBind();
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblOrderAcceptanceDetails.Visible = false;
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
                txtResponsiblePersonPhNo.Text = objHR.EmpMobile;
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

    #region GridView OrderAcceptance Details Row DataBound
    protected void gvOrderAcceptanceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[10].Text.ToLower() == "closed")
        //    {
        //        e.Row.Visible = false;
        //    }
        //}
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Order Acceptance Date")
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
        gvOrderAcceptanceDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvOrderAcceptanceDetails.DataBind();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvOrderAcceptanceDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=orderacc&oano=" + gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text + "";
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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SM.OrderAcceptance objSMOAApprove = new SM.OrderAcceptance();
            SM.BeginTransaction();
            objSMOAApprove.OAId = gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text;
            objSMOAApprove.OAApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            MessageBox.Show(this, objSMOAApprove.OrderAcceptanceApprove_Update());
            SM.WorkOrder.WorkOrderStatus_Update(SM.SMStatus.Closed, gvOrderAcceptanceDetails.SelectedRow.Cells[1].Text);
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvOrderAcceptanceDetails.DataBind();
            gvOrderAcceptanceDetails.SelectedIndex = -1;
            tblOrderAcceptanceDetails.Visible = false;
            SM.Dispose();
            //btnEdit_Click(sender, e);
            //if (ddlApprovedBy.SelectedValue != "0")
            //{
            //    btnRefresh_Click(sender, e);
            //    btnClose_Click(sender, e);
            //}
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {

    }

    protected void ddlSalesPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlSalesPerson.SelectedItem.Value) > 0)
            {
                txtSalesPersonPhNo.Text = objHR.EmpMobile;
                txtSalesPersonEMail.Text = objHR.EmpEMail;
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
}

 
