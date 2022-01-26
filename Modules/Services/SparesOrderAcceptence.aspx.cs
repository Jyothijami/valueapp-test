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

public partial class Modules_Services_SparesOrderAcceptence : System.Web.UI.Page
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // txtOADate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //    txtDeliveryDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            DeliveryType_Fill();
            CurrencyType_Fill();
            WorkOrder_Fill();
           EmployeeMaster_Fill();
            TransporterName_Fill();
        }
//EmployeeMaster_Fill();
        if (Request.QueryString["SWOId"] != null)
        {
            btnNew_Click(sender, e);
            ddlSONo.SelectedValue = Request.QueryString["SWOId"].ToString();
            ddlSONo_SelectedIndexChanged(sender, e);
            tblOrderAcceptanceDetails.Visible = true;
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
            Services.OrderProfile.OrderProfile_SelectAll(ddlSONo);
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
        //Services.ClearControls(this);
        txtOANo.Text = Services.SparesOrderAcceptance.SparesOrderAcceptance_AutoGenCode();
        txtOADate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
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
            Services.SparesOrderAcceptance objsr = new Services.SparesOrderAcceptance();
            if (objsr.SparesOrderAcceptance_isrecordexists(ddlSONo.SelectedItem.Value) > 0)
            {
                MessageBox.Show(this, "order acceptance for " + ddlSONo.SelectedItem.Text + " already prepared");
                Yantra.Classes.General.ClearControls(this);
                return;
            }
            Services.SparesOrderAcceptance objOrderAcceptance = new Services.SparesOrderAcceptance();

            Services.BeginTransaction();

            objOrderAcceptance.SOANo = txtOANo.Text;
            objOrderAcceptance.SOADate = Yantra.Classes.General.toMMDDYYYY(txtOADate.Text);
            objOrderAcceptance.SWOId = ddlSONo.SelectedItem.Value;

            objOrderAcceptance.SOARespId = ddlResponsiblePerson.SelectedItem.Value;
            objOrderAcceptance.SOASalespId = ddlSalesPerson.SelectedItem.Value;
            objOrderAcceptance.SOAPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objOrderAcceptance.SOACheckedBy = ddlCheckedBy.SelectedItem.Value;
            objOrderAcceptance.SOAApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objOrderAcceptance.SOAConsignee = txtConsignee.Text;
            objOrderAcceptance.DespmId = ddlDespatchMode.SelectedItem.Value;
            objOrderAcceptance.TransId = ddlTransporter.SelectedItem.Value;
            objOrderAcceptance.SOADeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objOrderAcceptance.SOACSTax = txtCST.Text;
            objOrderAcceptance.SOAInspection = txtInspection.Text;
            objOrderAcceptance.SOAInvoiceTo = txtInvoiceTo.Text;
            objOrderAcceptance.SOAFlag = Services.ServicesStatus.New.ToString();

            if (objOrderAcceptance.SparesOrderAcceptance_Save() == "Data Saved Successfully")
            {
                objOrderAcceptance.SparesOrderAcceptanceDetails_Delete(objOrderAcceptance.SOAId);
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {

                    objOrderAcceptance.SOAItemCode = gvrow.Cells[0].Text;
                    objOrderAcceptance.SOADetQty = gvrow.Cells[3].Text;
                    objOrderAcceptance.SOARate = gvrow.Cells[4].Text;
                    objOrderAcceptance.SOADetSpec = gvrow.Cells[6].Text;
                    objOrderAcceptance.SOADetRemarks = gvrow.Cells[7].Text;
                    objOrderAcceptance.SOADetPriority = gvrow.Cells[8].Text;


                    objOrderAcceptance.SparesOrderAcceptanceDetails_Save();
                }
                Services.OrderProfile.OrderProfileStatus_Update(Services.ServicesStatus.Closed, objOrderAcceptance.SWOId);
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
            gvOrderAcceptanceDetails.DataBind();
            tblOrderAcceptanceDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion

    #region OrderAcceptanceUpdate
    private void OrderAcceptanceUpdate()
    {
        try
        {
            Services.SparesOrderAcceptance objOrderAcceptance = new Services.SparesOrderAcceptance();

            Services.BeginTransaction();

            objOrderAcceptance.SOAId = gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text;
            objOrderAcceptance.SOANo = txtOANo.Text;
            objOrderAcceptance.SOADate = Yantra.Classes.General.toMMDDYYYY(txtOADate.Text);
            objOrderAcceptance.SWOId = ddlSONo.SelectedItem.Value;
            objOrderAcceptance.SOARespId = ddlResponsiblePerson.SelectedItem.Value;
            objOrderAcceptance.SOASalespId = ddlSalesPerson.SelectedItem.Value;
            objOrderAcceptance.SOAPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objOrderAcceptance.SOACheckedBy = ddlCheckedBy.SelectedItem.Value;
            objOrderAcceptance.SOAApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objOrderAcceptance.SOAConsignee = txtConsignee.Text;
            objOrderAcceptance.DespmId = ddlDespatchMode.SelectedItem.Value;
            objOrderAcceptance.TransId = ddlTransporter.SelectedItem.Value;
            objOrderAcceptance.SOADeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objOrderAcceptance.SOACSTax = txtCST.Text;
            objOrderAcceptance.SOAInspection = txtInspection.Text;
            objOrderAcceptance.SOAInvoiceTo = txtInvoiceTo.Text;
            objOrderAcceptance.SOAFlag = Services.ServicesStatus.New.ToString();

            if (objOrderAcceptance.SparesOrderAcceptance_Update() == "Data Updated Successfully")
            {
                objOrderAcceptance.SparesOrderAcceptanceDetails_Delete(objOrderAcceptance.SOAId);
                foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
                {
                    objOrderAcceptance.SOAItemCode = gvrow.Cells[0].Text;
                    objOrderAcceptance.SOADetQty = gvrow.Cells[3].Text;
                    objOrderAcceptance.SOARate = gvrow.Cells[4].Text;
                    objOrderAcceptance.SOADetSpec = gvrow.Cells[6].Text;
                    objOrderAcceptance.SOADetRemarks = gvrow.Cells[7].Text;
                    objOrderAcceptance.SOADetPriority = gvrow.Cells[8].Text;

                    objOrderAcceptance.SparesOrderAcceptanceDetails_Save();
                }
                Services.OrderProfile.OrderProfileStatus_Update(Services.ServicesStatus.Closed, objOrderAcceptance.SWOId);
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
            btnDelete.Attributes.Clear();
            gvOrderAcceptanceDetails.DataBind();
            tblOrderAcceptanceDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
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
                Services.SparesOrderAcceptance ObjOrderAcceptance = new Services.SparesOrderAcceptance();
                if (ObjOrderAcceptance.SparesOrderAcceptance_Select(gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblOrderAcceptanceDetails.Visible = true;
                    txtOANo.Text = ObjOrderAcceptance.SOANo;
                    txtOADate.Text = ObjOrderAcceptance.SOADate;
                    ddlSONo.SelectedValue = ObjOrderAcceptance.SWOId;
                    ddlResponsiblePerson.SelectedValue = ObjOrderAcceptance.SOARespId;
                    ddlPreparedBy.SelectedValue = ObjOrderAcceptance.SOAPreparedBy;
                    ddlCheckedBy.SelectedValue = ObjOrderAcceptance.SOACheckedBy;
                    ddlApprovedBy.SelectedValue = ObjOrderAcceptance.SOAApprovedBy;
                    txtConsignee.Text = ObjOrderAcceptance.SOAConsignee;
                    ddlDespatchMode.SelectedValue = ObjOrderAcceptance.DespmId;
                    ddlTransporter.SelectedValue = ObjOrderAcceptance.TransId;
                    txtDeliveryDate.Text = ObjOrderAcceptance.SOADeliveryDate;
                    txtCST.Text = ObjOrderAcceptance.SOACSTax;
                    txtInspection.Text = ObjOrderAcceptance.SOAInspection;
                    txtInvoiceTo.Text = ObjOrderAcceptance.SOAInvoiceTo;
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
                Services.SparesOrderAcceptance ObjOrderAcceptance = new Services.SparesOrderAcceptance();
                MessageBox.Show(this, ObjOrderAcceptance.SparesOrderAcceptance_Delete(gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvOrderAcceptanceDetails.DataBind();
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

    #region SO No Selected Index Changed
    protected void ddlSONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {



            Services.OrderProfile objWO = new Services.OrderProfile();
            if (objWO.OrderProfile_Select(ddlSONo.SelectedItem.Value) > 0)
            {
                txtSODate.Text = objWO.SWODate;
                txtCST.Text = objWO.SWOCSTax;
                //txtConsignee.Text=objWO.SWOC
                Services.SparesOrder objSalesOrder = new Services.SparesOrder();
                if (objSalesOrder.SparesOrder_Select(objWO.SPOId) > 0)
                {
                    txtSODate.Text = objSalesOrder.SPODate;
                    txtDelivery.Text = objSalesOrder.SPODelivery;
                    ddlCurrencyType.SelectedValue = objSalesOrder.SPOCurrencyTypeId;
                    txtPaymentTerms.Text = objSalesOrder.SPOPaymentTerms;
                    txtPackingCharges.Text = objSalesOrder.SPOPackageCharges;
                    txtExciseDuty.Text = objSalesOrder.SPOExciseDuty;
                    txtGuarantee.Text = objSalesOrder.SPOGuarantee;
                    txtOtherSpecs.Text = objSalesOrder.SPOOtherSpec;
                    if (btnSave.Text == "Save")
                    {
                        txtInspection.Text = objSalesOrder.SPOInspection;
                        txtConsignee.Text = objSalesOrder.ConsignmentTo;
                        txtInvoiceTo.Text = objSalesOrder.InvoiceTo;
                        ddlDespatchMode.SelectedValue = objSalesOrder.DespmId;
                    }
                    
                    objSalesOrder.SparesOrderDetails_Select(objSalesOrder.SPOId, gvSalesOrderItems);
                }

                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                //if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objWO.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = objSMCustomer.CustUnitName;
                //}
                //else if (objSMCustomer.CustomerMaster_Select(objWO.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = "--";
                //}
                Services.SparesQuotation objSM1 = new Services.SparesQuotation();
                if (objSM1.CompalintRegister_Select(objWO.cridnew) > 0)
                {
                    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    if (objSMCustomer.CustomerMaster_Select(objWO.CustId) > 0)
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
        tblOrderAcceptanceDetails.Visible = false;
    }
    #endregion

    #region Responsible Person Select Index Changed
    protected void ddlResponsiblePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          
           
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();

            if (objHR.EmployeeMaster_Select(ddlResponsiblePerson.SelectedValue) > 0)
            {
                txtResponsiblePersonPhNo.Text = objHR.EmpPhone;
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
            Services.SparesOrderAcceptance objSMOAApprove = new Services.SparesOrderAcceptance();
            Services.BeginTransaction();
            objSMOAApprove.SOAId = gvOrderAcceptanceDetails.SelectedRow.Cells[0].Text;
            objSMOAApprove.SOAApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            MessageBox.Show(this, objSMOAApprove.SparesOrderAcceptanceApprove_Update());
            Services.OrderProfile.OrderProfileStatus_Update(Services.ServicesStatus.Closed, gvOrderAcceptanceDetails.SelectedRow.Cells[1].Text);
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvOrderAcceptanceDetails.DataBind();
            Services.Dispose();
            btnEdit_Click(sender, e);
            if (ddlApprovedBy.SelectedValue != "0")
            {
                btnRefresh_Click(sender, e);
                btnClose_Click(sender, e);
            }
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
                txtSalesPersonPhNo.Text = objHR.EmpPhone;
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

 
