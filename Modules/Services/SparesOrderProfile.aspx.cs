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

public partial class Modules_Services_SparesOrderProfile : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile) { }
        if (!IsPostBack)
        {
            //txtWODate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtInspectionDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtDeliveryDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            SparesOrderMaster_Fill();
            DeliveryType_Fill();
            EmployeeMaster_Fill();

            if (Request.QueryString["SPOId"] != null)
            {
                btnNew_Click(sender, e);
                ddlOrderAcceptance.SelectedValue = Request.QueryString["SPOId"].ToString();
                ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                tblWorkOrderDetails.Visible = true;
            }
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvWorkOrderDetails.SelectedRow.Cells[8].Text) && gvWorkOrderDetails.SelectedRow.Cells[8].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                //btnPrint.Visible = true;
                btnSend.Visible = true;
            }
            else
            {
                if (btnSave.Enabled == true) { btnApprove.Visible = false; } else if (btnSave.Enabled == false) { btnApprove.Visible = true; };
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                //btnPrint.Visible = false;
                btnSend.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            //btnPrint.Visible = false;
            btnSend.Visible = false;
        }
    }
    #endregion

    #region New Button Click
    protected void btnNew_Click(object sender, EventArgs e)
    {

        gvWorkOrderDetails.SelectedIndex = -1;
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        Services.ClearControls(this);
        txtWONo.Text = Services.OrderProfile.OrderProfile_AutoGenCode();
        txtWODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        gvOrderAcceptanceItems.DataBind();
        tblWorkOrderDetails.Visible = true;
    }
    #endregion


    #region Spares Order Master Fill
    private void SparesOrderMaster_Fill()
    {
        try
        {
            Services.SparesOrder.SparesOrder_Select(ddlOrderAcceptance);
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
            Masters.DespatchMode.DespatchMode_Select(ddlDeliveryMode);
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

    #region Link Button WorkOrderNo_Click
    protected void lbtnWorkOrderNo_Click(object sender, EventArgs e)
    {

        tblWorkOrderDetails.Visible = false;
        LinkButton lbtnWorkOrderNo;
        lbtnWorkOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnWorkOrderNo.Parent.Parent;
        gvWorkOrderDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        Services.OrderProfile objWorkOrder = new Services.OrderProfile();

        if (objWorkOrder.OrderProfile_Select(gvWorkOrderDetails.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;

            btnSave.Text = "Update";
            

            tblWorkOrderDetails.Visible = true;
            ddlOrderAcceptance.SelectedValue = objWorkOrder.SPOId;
            txtWONo.Text = objWorkOrder.SWONo;
            txtWODate.Text = objWorkOrder.SWODate;
            ddlDeliveryMode.SelectedValue = objWorkOrder.DespId;
            txtInspectionDate.Text = objWorkOrder.SWOInspDate;
            txtPackingInstrs.Text = objWorkOrder.SWOPackForwInst;
            txtDeliveryDate.Text = objWorkOrder.SWODeliveryDate;
            txtAccessories.Text = objWorkOrder.SWOAccessories;
            txtExtraSpace.Text = objWorkOrder.SWOExtraSpares;
            txtAdvanceAmt.Text = objWorkOrder.SPOAdvanceAmt;
            ddlPreparedBy.SelectedValue = objWorkOrder.SWOPreparedBy;
            ddlCheckedBy.SelectedValue = objWorkOrder.SWOCheckedBy;
            ddlApprovedBy.SelectedValue = objWorkOrder.SWOApprovedBy;
            txtFrieght.Text = objWorkOrder.SWOFrieght;
            txtRoadPermit.Text = objWorkOrder.SWORoadPermit;
            ddlOrderAcceptance_SelectedIndexChanged(sender, e);
            txtCST.Text = objWorkOrder.SWOCSTax;
            lbtnAttachedFiles.Text = objWorkOrder.SWOFiles;

            if (lbtnAttachedFiles.Text != "")
            {
                string[] ext = lbtnAttachedFiles.Text.Split('.');
                if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/WOFiles/" + objWorkOrder.SWOId + "." + ext[1]))
                {
                    lbtnAttachedFiles.Attributes.Add("onclick", "window.open('WOFiles/" + objWorkOrder.SWOId + "." + ext[1] + "','WOFiles','resizable=yes,width=800,height=600,status=yes,toolbar=no,menubar=no');");
                }
                else
                {
                    lbtnAttachedFiles.Text = "";
                    lbtnAttachedFiles.Attributes.Clear();
                }
            }
        }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            OrderProfileSave();
        }
        else if (btnSave.Text == "Update")
        {
            OrderProfileUpdate();
        }
    }
    #endregion


    #region  OrderProfileSave
    private void OrderProfileSave()
    {
        try
        {
            Services.SparesOrder objsr = new Services.SparesOrder();
            if (objsr.SparesOrder_isrecordexists(ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                MessageBox.Show(this, "order profile for " + ddlOrderAcceptance.SelectedItem.Text + " already prepared");
                Yantra.Classes.General.ClearControls(this);
                return;
            }
            Services.OrderProfile objWorkOrder = new Services.OrderProfile();
            Services.BeginTransaction();
            objWorkOrder.SWONo = txtWONo.Text;
            objWorkOrder.SWODate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
            objWorkOrder.SPOId = ddlOrderAcceptance.SelectedItem.Value;
            objWorkOrder.DespId = ddlDeliveryMode.SelectedItem.Value;
            objWorkOrder.SWOInspDate = Yantra.Classes.General.toMMDDYYYY(txtInspectionDate.Text);
            objWorkOrder.SWOPackForwInst = txtPackingInstrs.Text;
            objWorkOrder.SWODeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objWorkOrder.SWOAccessories = txtAccessories.Text;
            objWorkOrder.SWOExtraSpares = txtExtraSpace.Text;
            objWorkOrder.SWOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWorkOrder.SWOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objWorkOrder.SWOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objWorkOrder.SWOFrieght = txtFrieght.Text;
            objWorkOrder.SWORoadPermit = txtRoadPermit.Text;
            objWorkOrder.SWOCSTax = txtCST.Text;
           
            objWorkOrder.SWOFLag = Services.ServicesStatus.New.ToString();

            if (objWorkOrder.OrderProfile_Save() == "Data Saved Successfully")
            {
                if (FileUpload1.HasFile)
                {
                    objWorkOrder.SWOFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                }
                else
                {
                    objWorkOrder.SWOFiles = "";
                }
                objWorkOrder.OrderProfileDetails_Delete(objWorkOrder.SWOId);
                foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
                {
                    objWorkOrder.SWOItemCode = gvrow.Cells[0].Text;
                    objWorkOrder.SWODetQty = gvrow.Cells[3].Text;
                    objWorkOrder.SWODetSpec = gvrow.Cells[6].Text;
                    objWorkOrder.SWODetRemarks = gvrow.Cells[7].Text;

                    objWorkOrder.OrderProfileDetails_Save();
                }
                Services.SparesOrder.SparesOrderStatus_Update(Services.ServicesStatus.Closed, objWorkOrder.SPOId);
                if (FileUpload1.HasFile)
                {
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + "." + ext[1]))
                        { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + "." + ext[1]); }
                    }
                    FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
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
            gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion

    #region  OrderProfileUpdate
    private void OrderProfileUpdate()
    {
        try
        {
            if (FileUpload1.HasFile) { }
            Services.OrderProfile objWorkOrder = new Services.OrderProfile();
            Services.BeginTransaction();
            objWorkOrder.SWOId = gvWorkOrderDetails.SelectedRow.Cells[0].Text;
            objWorkOrder.SWONo = txtWONo.Text;
            objWorkOrder.SWODate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
            objWorkOrder.SPOId = ddlOrderAcceptance.SelectedItem.Value;
            objWorkOrder.DespId = ddlDeliveryMode.SelectedItem.Value;
            objWorkOrder.SWOInspDate = Yantra.Classes.General.toMMDDYYYY(txtInspectionDate.Text);
            objWorkOrder.SWOPackForwInst = txtPackingInstrs.Text;
            objWorkOrder.SWODeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objWorkOrder.SWOAccessories = txtAccessories.Text;
            objWorkOrder.SWOExtraSpares = txtExtraSpace.Text;
            objWorkOrder.SWOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWorkOrder.SWOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objWorkOrder.SWOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objWorkOrder.SWOFrieght = txtFrieght.Text;
            objWorkOrder.SWORoadPermit = txtRoadPermit.Text;
            objWorkOrder.SWOCSTax = txtCST.Text;
           
            objWorkOrder.SWOFLag = Services.ServicesStatus.New.ToString();

            if (objWorkOrder.OrderProfile_Update() == "Data Updated Successfully")
            {
                if (FileUpload1.HasFile)
                {
                    objWorkOrder.SWOFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                }
                else
                {
                    objWorkOrder.SWOFiles = lbtnAttachedFiles.Text;
                }
                objWorkOrder.OrderProfileDetails_Delete(objWorkOrder.SWOId);
                foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
                {
                    objWorkOrder.SWOItemCode = gvrow.Cells[0].Text;
                    objWorkOrder.SWODetQty = gvrow.Cells[3].Text;
                    objWorkOrder.SWODetSpec = gvrow.Cells[6].Text;
                    objWorkOrder.SWODetRemarks = gvrow.Cells[7].Text;

                    objWorkOrder.OrderProfileDetails_Save();
                }
                Services.SparesOrder.SparesOrderStatus_Update(Services.ServicesStatus.Closed, objWorkOrder.SPOId);
                if (FileUpload1.HasFile)
                {
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + "." + ext[1]))
                        { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + "." + ext[1]); }
                    }
                    FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
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
            gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            try
            {
                Services.OrderProfile objWorkOrder = new Services.OrderProfile();

                if (objWorkOrder.OrderProfile_Select(gvWorkOrderDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblWorkOrderDetails.Visible = true;
                    ddlOrderAcceptance.SelectedValue = objWorkOrder.SPOId;
                    txtWONo.Text = objWorkOrder.SWONo;
                    txtWODate.Text = objWorkOrder.SWODate;
                    ddlDeliveryMode.SelectedValue = objWorkOrder.DespId;
                    txtInspectionDate.Text = objWorkOrder.SWOInspDate;
                    txtPackingInstrs.Text = objWorkOrder.SWOPackForwInst;
                    txtDeliveryDate.Text = objWorkOrder.SWODeliveryDate;
                    txtAccessories.Text = objWorkOrder.SWOAccessories;
                    txtExtraSpace.Text = objWorkOrder.SWOExtraSpares;
                    txtAdvanceAmt.Text = objWorkOrder.SPOAdvanceAmt;
                    ddlPreparedBy.SelectedValue = objWorkOrder.SWOPreparedBy;
                    ddlCheckedBy.SelectedValue = objWorkOrder.SWOCheckedBy;
                    ddlApprovedBy.SelectedValue = objWorkOrder.SWOApprovedBy;
                    txtFrieght.Text = objWorkOrder.SWOFrieght;
                    txtRoadPermit.Text = objWorkOrder.SWORoadPermit;
                    ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                    txtCST.Text = objWorkOrder.SWOCSTax;
                    lbtnAttachedFiles.Text = objWorkOrder.SWOFiles;
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SWOFiles/" + objWorkOrder.SWOId + "." + ext[1]))
                        {
                            lbtnAttachedFiles.Attributes.Add("onclick", "window.open('SWOFiles/" + objWorkOrder.SWOId + "." + ext[1] + "','WOFiles','resizable=yes,width=800,height=600,status=yes,toolbar=no,menubar=no');");
                        }
                        else
                        {
                            lbtnAttachedFiles.Text = "";
                            lbtnAttachedFiles.Attributes.Clear();
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
                ddlOrderAcceptance_SelectedIndexChanged(sender, e);
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
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            try
            {
                Services.OrderProfile objWorkOrder = new Services.OrderProfile();
                MessageBox.Show(this, objWorkOrder.OrderProfile_Delete(gvWorkOrderDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvWorkOrderDetails.DataBind();
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

    #region Order Acceptance No Selected Index Changed
    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Services.SparesOrder objSO = new Services.SparesOrder();
            if (objSO.SparesOrder_Select(ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                txtOADate.Text = objSO.SPODate;
                txtCST.Text = objSO.SPOCSTax;
                if (btnSave.Text == "Save")
                {
                    //txtDeliveryDate.Text = objSO.SODelivery;
                    ddlDeliveryMode.SelectedValue = objSO.DespmId;
                    txtAdvanceAmt.Text = objSO.SPOAdvanceAmt;
                    lbtnAttachedFiles.Text = objSO.SPOFiles;
                    txtAccessories.Text = objSO.SPOAccessories;
                    txtExtraSpace.Text = objSO.SPOExtraSpares;
                }
                objSO.SparesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);

                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                //if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objSO.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = objSMCustomer.CustUnitName;
                //}
                //else if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
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
                if (objSM1.CompalintRegister_Select(objSO.CrId) > 0)
                {
                    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
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

    #region GridView Work Order Details Row Databound
    protected void gvWorkOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        if (Request.QueryString["SOId"] != null)
        {
            btnNew_Click(sender, e);
            ddlOrderAcceptance.SelectedValue = Request.QueryString["SPOId"].ToString();
            ddlOrderAcceptance_SelectedIndexChanged(sender, e);
            tblWorkOrderDetails.Visible = true;
        }
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblWorkOrderDetails.Visible = false;
    }
    #endregion


    #region GridView Order Acceptance Items Row DataBound
    protected void gvOrderAcceptanceItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = (Convert.ToInt32(e.Row.Cells[4].Text) * Convert.ToInt32(e.Row.Cells[3].Text)).ToString();
        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == " Order Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date")
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
        gvWorkOrderDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvWorkOrderDetails.DataBind();
    }
    #endregion

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/ServicesReportViewer.aspx?type=orderProfile&swono=" + gvWorkOrderDetails.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
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
            Services.OrderProfile objSMWOApprove = new Services.OrderProfile();
            Services.BeginTransaction();
            objSMWOApprove.SWOId = gvWorkOrderDetails.SelectedRow.Cells[0].Text;
            objSMWOApprove.SWOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            MessageBox.Show(this, objSMWOApprove.OrderProfileApprove_Update());
            Services.SparesOrder.SparesOrderStatus_Update(Services.ServicesStatus.Open, gvWorkOrderDetails.SelectedRow.Cells[0].Text);
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvWorkOrderDetails.DataBind();
            Services.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            Response.Redirect("SparesOrderAcceptence.aspx?SWOId=" + gvWorkOrderDetails.SelectedRow.Cells[0].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }


}

 
