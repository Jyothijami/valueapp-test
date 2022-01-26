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

public partial class Modules_SM_WorkOrder : basePage
{

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile) { }
        if (!IsPostBack)
        {
            setControlsVisibility();

            //txtWODate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtInspectionDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtDeliveryDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            SalesOrderMaster_Fill();
            DeliveryType_Fill();
            EmployeeMaster_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            gvWorkOrderDetails.DataBind();

            if (Request.QueryString["SOId"] != null)
            {
                btnNew_Click(sender, e);
                ddlOrderAcceptance.SelectedValue = Request.QueryString["SOId"].ToString();
                ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                tblWorkOrderDetails.Visible = true;
            }
            tblPopUp1.Visible = false;

        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "10");
        //btnGo.Enabled = up.Go;
        //btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnSave.Enabled = up.Save;
        btnApprove.Enabled = up.Approve;
        //btnRefresh.Enabled = up.Refresh;
        btnSend.Enabled = up.Email;
        btnPrint.Enabled = up.Print;
        //btnClose.Enabled = up.Close;
        //btnReserve.Enabled = up.Reserve;
    }

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
                btnSend.Visible = false;
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
        SM.ClearControls(this);
        txtWONo.Text = SM.WorkOrder.WorkOrder_AutoGenCode();
        txtWODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        gvOrderAcceptanceItems.DataBind();
        tblWorkOrderDetails.Visible = true;
    }
    #endregion

    #region Sales Order Master Fill
    private void SalesOrderMaster_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_Select(ddlOrderAcceptance);
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
        LinkButton lbtnQuoNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnQuoNo.Parent.Parent;
        gvWorkOrderDetails.SelectedIndex = Row.RowIndex;

        Response.Redirect("WorkOrderDetails2.aspx?IoNo=" + lbtnQuoNo.Text +
            "&IoId=" + gvWorkOrderDetails.SelectedRow.Cells[0].Text +
            "&AppBy=" + gvWorkOrderDetails.SelectedRow.Cells[7].Text +
            "&Status=" + gvWorkOrderDetails.SelectedRow.Cells[8].Text);

        //Old Code
        tblWorkOrderDetails.Visible = false;
        LinkButton lbtnWorkOrderNo;
        lbtnWorkOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnWorkOrderNo.Parent.Parent;
        gvWorkOrderDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        SM.WorkOrder objWorkOrder = new SM.WorkOrder();

        if (objWorkOrder.WorkOrder_Select(gvWorkOrderDetails.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;

            btnSave.Text = "Update";

            tblWorkOrderDetails.Visible = true;
            ddlOrderAcceptance.SelectedValue = objWorkOrder.SOId;
            txtWONo.Text = objWorkOrder.WONo;
            txtWODate.Text = objWorkOrder.WODate;
            ddlDeliveryMode.SelectedValue = objWorkOrder.DespId;
            txtInspectionDate.Text = objWorkOrder.WOInspDate;
            txtPackingInstrs.Text = objWorkOrder.WOPackForwInst;
            txtDeliveryDate.Text = objWorkOrder.WODeliveryDate;
            txtAccessories.Text = objWorkOrder.WOAccessories;
            txtExtraSpace.Text = objWorkOrder.WOExtraSpares;
            txtAdvanceAmt.Text = objWorkOrder.SOAdvanceAmt;
            ddlPreparedBy.SelectedValue = objWorkOrder.WOPreparedBy;
            ddlCheckedBy.SelectedValue = objWorkOrder.WOCheckedBy;
            ddlApprovedBy.SelectedValue = objWorkOrder.WOApprovedBy;
            txtFrieght.Text = objWorkOrder.WOFrieght;
            txtRoadPermit.Text = objWorkOrder.WORoadPermit;
            ddlOrderAcceptance_SelectedIndexChanged(sender, e);
            txtCST.Text = objWorkOrder.WOCSTax;
            lblVATCSTNo.Text = objWorkOrder.WOTaxLabel;

            lbtnAttachedFiles.Text = objWorkOrder.WOFiles;

            if (lbtnAttachedFiles.Text != "")
            {
                string[] ext = lbtnAttachedFiles.Text.Split('.');
                if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]))
                {
                    lbtnAttachedFiles.Attributes.Add("onclick", "window.open('WOFiles/" + objWorkOrder.WOId + "." + ext[1] + "','WOFiles','resizable=yes,width=800,height=600,status=yes,toolbar=no,menubar=no');");
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
            WorkOrderSave();
        }
        else if (btnSave.Text == "Update")
        {
            WorkOrderUpdate();
        }
    }
    #endregion

    #region Work Order Save
    private void WorkOrderSave()
    {
        try
        {
            SM.WorkOrder objWorkOrder = new SM.WorkOrder();
            SM.BeginTransaction();
            objWorkOrder.WONo = txtWONo.Text;
            objWorkOrder.WODate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
            objWorkOrder.SOId = ddlOrderAcceptance.SelectedItem.Value;
            objWorkOrder.DespId = ddlDeliveryMode.SelectedItem.Value;
            objWorkOrder.WOInspDate = Yantra.Classes.General.toMMDDYYYY(txtInspectionDate.Text);
            objWorkOrder.WOPackForwInst = txtPackingInstrs.Text;
            objWorkOrder.WODeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objWorkOrder.WOAccessories = txtAccessories.Text;
            objWorkOrder.WOExtraSpares = txtExtraSpace.Text;
            objWorkOrder.WOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWorkOrder.WOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objWorkOrder.WOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objWorkOrder.WOFrieght = txtFrieght.Text;
            objWorkOrder.WORoadPermit = txtRoadPermit.Text;
            objWorkOrder.WOCSTax = txtCST.Text;
            objWorkOrder.WOTaxLabel = lblVATCSTNo.Text;
            if (FileUpload1.HasFile)
            {
                objWorkOrder.WOFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objWorkOrder.WOFiles = "";
            }
            objWorkOrder.WOFLag = SM.SMStatus.New.ToString();
            objWorkOrder.CpId = lblCPID.Text;
            objWorkOrder.RespId = lblRespId.Text;


            if (objWorkOrder.WorkOrder_Save() == "Data Saved Successfully")
            {
                objWorkOrder.WorkOrderDetails_Delete(objWorkOrder.WOId);
                foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
                {
                    objWorkOrder.WOItemCode = gvrow.Cells[1].Text;
                    objWorkOrder.WODetQty = gvrow.Cells[5].Text;
                    objWorkOrder.WODetSpec = gvrow.Cells[9].Text;
                    objWorkOrder.WODetRemarks = gvrow.Cells[10].Text;
                    objWorkOrder.ColorId = gvrow.Cells[16].Text;
                    TextBox txtAnnexureQty = (TextBox)gvrow.FindControl("txtAnnexQty");
                    //objWorkOrder.WorkOrderDetails_Save() == "Data Saved Successfully";
                    decimal anqty = Convert.ToDecimal(txtAnnexureQty.Text);
                    int aqty = Convert.ToInt32(anqty);
                    objWorkOrder.AnnexureQty = aqty.ToString();
                    objWorkOrder.SoDetId = gvrow.Cells[18].Text;
                    // To Block Items To the PO No

                    //Masters.ItemPurchase obj = new Masters.ItemPurchase();
                    //int hai = int.Parse(gvrow.Cells[5].Text);
                    //for (int i = 0; i < hai; i++)
                    //{
                    //    obj.itemcode = gvrow.Cells[1].Text;
                    //    obj.ItemID = "I" + i + gvrow.Cells[1].Text;
                    //    obj.companyid = lblCPID.Text;
                    //    obj.Barcode = "I" + i + gvrow.Cells[1].Text;dec
                    //    obj.POID = ddlOrderAcceptance.SelectedItem.Value;
                    //    obj.COLORID = gvrow.Cells[15].Text;
                    //    obj.Block_Save();
                    //}


                    if (objWorkOrder.WorkOrderDetails_Save() == "Data Saved Successfully")
                    {
                        //objWorkOrder.ItemMasterReserveQty_Update(Convert.ToString(gvrow.Cells[0].Text), Convert.ToInt16(gvrow.Cells[4].Text));
                    }
                }
                //  SM.SalesOrder.SalesOrderStatus_Update(SM.SMStatus.Closed, objWorkOrder.SOId);
                if (FileUpload1.HasFile)
                {
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]))
                        { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]); }
                    }
                    FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
                }
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
            gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Work Order Update
    private void WorkOrderUpdate()
    {
        try
        {
            if (FileUpload1.HasFile) { }
            SM.WorkOrder objWorkOrder = new SM.WorkOrder();
            SM.BeginTransaction();
            objWorkOrder.WOId = gvWorkOrderDetails.SelectedRow.Cells[0].Text;
            objWorkOrder.WONo = txtWONo.Text;
            objWorkOrder.WODate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
            objWorkOrder.SOId = ddlOrderAcceptance.SelectedItem.Value;
            objWorkOrder.DespId = ddlDeliveryMode.SelectedItem.Value;
            objWorkOrder.WOInspDate = Yantra.Classes.General.toMMDDYYYY(txtInspectionDate.Text);
            objWorkOrder.WOPackForwInst = txtPackingInstrs.Text;
            objWorkOrder.WODeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objWorkOrder.WOAccessories = txtAccessories.Text;
            objWorkOrder.WOExtraSpares = txtExtraSpace.Text;
            objWorkOrder.WOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWorkOrder.WOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objWorkOrder.WOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objWorkOrder.WOFrieght = txtFrieght.Text;
            objWorkOrder.WORoadPermit = txtRoadPermit.Text;
            objWorkOrder.WOCSTax = txtCST.Text;
            objWorkOrder.CpId = lblCPID.Text;
            if (FileUpload1.HasFile)
            {
                objWorkOrder.WOFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objWorkOrder.WOFiles = lbtnAttachedFiles.Text;
            }
            objWorkOrder.WOFLag = SM.SMStatus.New.ToString();

            if (objWorkOrder.WorkOrder_Update() == "Data Updated Successfully")
            {
                objWorkOrder.WorkOrderDetails_Delete(objWorkOrder.WOId);
                foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
                {
                    objWorkOrder.WOItemCode = gvrow.Cells[0].Text;
                    objWorkOrder.WODetQty = gvrow.Cells[3].Text;
                    objWorkOrder.WODetSpec = gvrow.Cells[6].Text;
                    objWorkOrder.WODetRemarks = gvrow.Cells[7].Text;

                    objWorkOrder.WorkOrderDetails_Save();
                }
                SM.SalesOrder.SalesOrderStatus_Update(SM.SMStatus.Closed, objWorkOrder.SOId);
                if (FileUpload1.HasFile)
                {
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]))
                        { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]); }
                    }
                    FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
                }
                SM.CommitTransaction();
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
            gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
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
                SM.WorkOrder objWorkOrder = new SM.WorkOrder();

                if (objWorkOrder.WorkOrder_Select(gvWorkOrderDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblWorkOrderDetails.Visible = true;
                    ddlOrderAcceptance.SelectedValue = objWorkOrder.SOId;
                    txtWONo.Text = objWorkOrder.WONo;
                    txtWODate.Text = objWorkOrder.WODate;
                    ddlDeliveryMode.SelectedValue = objWorkOrder.DespId;
                    txtInspectionDate.Text = objWorkOrder.WOInspDate;
                    txtPackingInstrs.Text = objWorkOrder.WOPackForwInst;
                    txtDeliveryDate.Text = objWorkOrder.WODeliveryDate;
                    txtAccessories.Text = objWorkOrder.WOAccessories;
                    txtExtraSpace.Text = objWorkOrder.WOExtraSpares;
                    txtAdvanceAmt.Text = objWorkOrder.SOAdvanceAmt;
                    ddlPreparedBy.SelectedValue = objWorkOrder.WOPreparedBy;
                    ddlCheckedBy.SelectedValue = objWorkOrder.WOCheckedBy;
                    ddlApprovedBy.SelectedValue = objWorkOrder.WOApprovedBy;
                    txtFrieght.Text = objWorkOrder.WOFrieght;
                    txtRoadPermit.Text = objWorkOrder.WORoadPermit;
                    ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                    txtCST.Text = objWorkOrder.WOCSTax;
                    lblVATCSTNo.Text = objWorkOrder.WOTaxLabel;

                    lbtnAttachedFiles.Text = objWorkOrder.WOFiles;
                    if (lbtnAttachedFiles.Text != "")
                    {
                        string[] ext = lbtnAttachedFiles.Text.Split('.');
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]))
                        {
                            lbtnAttachedFiles.Attributes.Add("onclick", "window.open('WOFiles/" + objWorkOrder.WOId + "." + ext[1] + "','WOFiles','resizable=yes,width=800,height=600,status=yes,toolbar=no,menubar=no');");
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
                SM.Dispose();
                //ddlOrderAcceptance_SelectedIndexChanged(sender, e);
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
                SM.WorkOrder objWorkOrder = new SM.WorkOrder();
                MessageBox.Show(this, objWorkOrder.WorkOrder_Delete(gvWorkOrderDetails.SelectedRow.Cells[0].Text));
                tblWorkOrderDetails.Visible = false;
                gvOrderAcceptanceItems.DataBind();
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvWorkOrderDetails.DataBind();
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

    #region Order Acceptance No Selected Index Changed
    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //////////SM.OrderAcceptance objOA = new SM.OrderAcceptance();
            //////////if (objOA.OrderAcceptance_Select(ddlOrderAcceptance.SelectedItem.Value) > 0)
            //////////{
            //////////    txtOADate.Text = objOA.OADate;
            //////////    txtCST.Text = objOA.SOCSTax;
            //////////    if (btnSave.Text == "Save")
            //////////    {
            //////////        txtDeliveryDate.Text = objOA.OADeliveryDate;
            //////////        ddlDeliveryMode.SelectedValue = objOA.DespmId;
            //////////    }
            //////////    objOA.OrderAcceptanceDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);

            //////////    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            //////////    if (objSMCustomer.CustomerMaster_Select(objOA.CustId) > 0)
            //////////    {
            //////////        txtCustName.Text = objSMCustomer.CustName;
            //////////        txtAddress.Text = objSMCustomer.Address;
            //////////        txtEmail.Text = objSMCustomer.Email;
            //////////        txtRegion.Text = objSMCustomer.RegName;
            //////////        txtPhone.Text = objSMCustomer.Phone;
            //////////        txtMobile.Text = objSMCustomer.Mobile;
            //////////    }
            //////////}

            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrder_Select(ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                txtOADate.Text = objSO.SODate;
                txtCST.Text = objSO.SOCSTax;
                string vatTax = objSO.SOVAT;

                if (btnSave.Text == "Save")
                {
                    //txtDeliveryDate.Text = objSO.SODelivery;
                    ddlDeliveryMode.SelectedValue = objSO.DespmId;
                    txtAdvanceAmt.Text = objSO.SOAdvanceAmt;
                    lbtnAttachedFiles.Text = objSO.SOFiles;
                    txtAccessories.Text = objSO.SOAccessories;
                    txtExtraSpace.Text = objSO.SOExtraSpares;
                    lblRespId.Text = objSO.SORespId;

                    if (objSO.SOCSTax == "" && objSO.SOInspection == "")
                    //if (objSO.SOCSTax == "")
                    {
                        lblVATCSTNo.Text = "VAT";
                        txtCST.Text = objSO.SOVAT;
                    }
                    //else if (objSO.SOVAT == "")
                    else if (objSO.SOVAT == "" && objSO.SOInspection == "")
                    {
                        lblVATCSTNo.Text = "CS Tax";
                        txtCST.Text = objSO.SOCSTax;
                    }
                    else if (objSO.SOVAT == "" && objSO.SOCSTax == "")
                    {
                        lblVATCSTNo.Text = "Including Tax";
                        txtCST.Text = objSO.SOInspection;
                    }
                }
                //objSO.SalesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);
                objSO.SalesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvSo );

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text = objSMCustomer.CustUnitName;
                }
                else if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text = "--";
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
        SM.ClearControls(this);
        if (Request.QueryString["SOId"] != null)
        {
            btnNew_Click(sender, e);
            ddlOrderAcceptance.SelectedValue = Request.QueryString["SOId"].ToString();
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

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "IO Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
    #endregion


    #region DropDownList Symbols Select Index Changed
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvWorkOrderDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "IO Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvWorkOrderDetails.DataBind();
    }
    #endregion

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=workorder&wono=" + gvWorkOrderDetails.SelectedRow.Cells[0].Text + "";
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
            SM.WorkOrder objSMWOApprove = new SM.WorkOrder();
            SM.BeginTransaction();
            objSMWOApprove.WOId = gvWorkOrderDetails.SelectedRow.Cells[0].Text;
            objSMWOApprove.WOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            MessageBox.Show(this, objSMWOApprove.WorkOrderApprove_Update());
            SM.SalesOrder.SalesOrderStatus_Update(SM.SMStatus.Open, gvWorkOrderDetails.SelectedRow.Cells[0].Text);
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvWorkOrderDetails.DataBind();
            SM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Send Cilck
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (gvWorkOrderDetails.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/Email.aspx?type=workorder&wono=" + gvWorkOrderDetails.SelectedRow.Cells[0].Text + "" +
                "&wonos=" + txtCustName.Text + "" +
                "&empid=" + Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId) + "" +
                "&custemail=" + txtEmail.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region LinkBtnAttached Files
    protected void lbtnAttachedFiles_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region GvOrder Acceptance RowDataBound
    protected void gvOrderAcceptanceItems_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[5].Text = (Convert.ToInt32(e.Row.Cells[5].Text)).ToString();
            e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            if (e.Row.Cells[17].Text == "True")
            {
                CheckBox ch;
                ch = (CheckBox)e.Row.FindControl("chk");
                ch.Checked = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            if (btnSave.Text == "Save")
            {
                e.Row.Cells[0].Visible = false;
                btnReserve.Visible = false;
            }
            else { e.Row.Cells[0].Visible = true; btnReserve.Visible = true; }

            e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
        }
    }
    #endregion

    #region Gvworkorderdetails Row DataBound
    protected void gvWorkOrderDetails_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Reserve
    protected void btnReserve_Click(object sender, EventArgs e)
    {
        try
        {

            SM.WorkOrder objWorkOrder = new SM.WorkOrder();

            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                CheckBox chk;
                chk = (CheckBox)gvrow.FindControl("chk");
                if (chk.Checked == true)
                {
                    string TempStr = "";
                    TempStr = objWorkOrder.ItemMasterReserveQty_Update(Convert.ToString(gvrow.Cells[1].Text), Convert.ToInt16(gvrow.Cells[15].Text), Convert.ToInt16(gvrow.Cells[5].Text), Convert.ToString(gvrow.Cells[2].Text), Convert.ToInt16(gvrow.Cells[17].Text));
                    //MessageBox.Show(this, objWorkOrder.ItemMasterReserveQty_Update(Convert.ToString(gvrow.Cells[1].Text), Convert.ToInt16(gvrow.Cells[15].Text), Convert.ToInt16(gvrow.Cells[5].Text), Convert.ToString(gvrow.Cells[2].Text), Convert.ToInt16(gvrow.Cells[17].Text)));
                    //  MessageBox.Show(this,objWorkOrder.ItemMasterReserveQty_Update(Convert.ToString(gvrow.Cells[1].Text), Convert.ToInt16(gvrow.Cells[5].Text), Convert.ToInt16(gvrow.Cells[17].Text), Convert.ToInt16(gvrow.Cells[15].Text)));                 
                    lblData.Text = lblData.Text + TempStr;
                }

            }
            if (lblData.Text != string.Empty)
            {
                //tblPopUp1.Visible = true;
                //ModalPopupExtender.Show();
            }
            else
            {
                foreach (GridViewRow gvrow1 in gvOrderAcceptanceItems.Rows)
                {
                    CheckBox chk;
                    chk = (CheckBox)gvrow1.FindControl("chk");
                    if (chk.Checked == true)
                    {
                        //objWorkOrder.ItemMasterReserveQty_Update(Convert.ToString(gvrow1.Cells[1].Text), Convert.ToInt16(gvrow1.Cells[5].Text), Convert.ToInt16(gvrow1.Cells[17].Text), Convert.ToInt16(gvrow1.Cells[15].Text));
                    }
                }

            }
            //MessageBox.Show(this, "Stock Reserved Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
            tblWorkOrderDetails.Visible = false;
            lblData.Text = "";
        }
    }
    #endregion

    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        SM.WorkOrder objWorkOrder = new SM.WorkOrder();
        try
        {
            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                CheckBox chk;
                chk = (CheckBox)gvrow.FindControl("chk");
                if (chk.Checked == true)
                {
                    // objWorkOrder.ItemMasterReserveQty_Update(Convert.ToString(gvrow.Cells[1].Text), Convert.ToInt16(gvrow.Cells[5].Text), Convert.ToInt16(gvrow.Cells[17].Text), Convert.ToInt16(gvrow.Cells[15].Text));
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
            tblWorkOrderDetails.Visible = false;
        }

    }
    protected void btnConfirmNo_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Hide();
        tblPopUp1.Visible = false;
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWorkOrderDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvWorkOrderDetails.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvWorkOrderDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }

    protected void gvSo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            if (e.Row.Cells[17].Text == "True")
            {
                CheckBox ch;
                ch = (CheckBox)e.Row.FindControl("chk");
                ch.Checked = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            if (btnSave.Text == "Save")
            {
                //e.Row.Cells[0].Visible = false;
                btnReserve.Visible = false;
            }
            else { e.Row.Cells[0].Visible = true; btnReserve.Visible = true; }

            e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
        }
    }

    protected void CheckBox_CheckChanged(object sender, EventArgs e)
    {
        GetData();
        SetData();
        BindSecondaryGrid();
    }

    private void GetData()
    {
        DataTable dt;
        if (ViewState["SelectedRecords"] != null)
            dt = (DataTable)ViewState["SelectedRecords"];
        else
            dt = CreateDataTable();
        CheckBox chkAll = (CheckBox)gvSo.HeaderRow
                            .Cells[0].FindControl("chkAll");
        for (int i = 0; i < gvSo.Rows.Count; i++)
        {
            if (chkAll.Checked)
            {
                dt = AddRow(gvSo.Rows[i], dt);
            }
            else
            {
                CheckBox chk = (CheckBox)gvSo.Rows[i]
                                .Cells[0].FindControl("chk");
                if (chk.Checked)
                {
                    dt = AddRow(gvSo.Rows[i], dt);
                }
                else
                {
                    dt = RemoveRow(gvSo.Rows[i], dt);
                }
            }
        }
        ViewState["SelectedRecords"] = dt;
    }

    private DataTable RemoveRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ItemCode = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable AddRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ItemCode = '" + gvRow.Cells[1].Text + "'");
        if (dr.Length <= 0)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["ItemCode"] = gvRow.Cells[1].Text;
            dt.Rows[dt.Rows.Count - 1]["ModelNo"] = gvRow.Cells[2].Text;
            dt.Rows[dt.Rows.Count - 1]["ItemName"] = gvRow.Cells[3].Text;
            dt.Rows[dt.Rows.Count - 1]["UOM"] = gvRow.Cells[4].Text;
            dt.Rows[dt.Rows.Count - 1]["Quantity"] = gvRow.Cells[5].Text;
            dt.Rows[dt.Rows.Count - 1]["Annexure_Qty"] = gvRow.Cells[6].Text;

            dt.Rows[dt.Rows.Count - 1]["Rate"] = gvRow.Cells[7].Text;
            dt.Rows[dt.Rows.Count - 1]["Amount"] = gvRow.Cells[8].Text;
            dt.Rows[dt.Rows.Count - 1]["Specifications"] = gvRow.Cells[9].Text;
            dt.Rows[dt.Rows.Count - 1]["Remarks"] = gvRow.Cells[10].Text;
            dt.Rows[dt.Rows.Count - 1]["Priority"] = gvRow.Cells[11].Text;
            dt.Rows[dt.Rows.Count - 1]["DeliveryDate"] = gvRow.Cells[12].Text;
            dt.Rows[dt.Rows.Count - 1]["Room"] = gvRow.Cells[13].Text;
            dt.Rows[dt.Rows.Count - 1]["Price"] = gvRow.Cells[14].Text;
            dt.Rows[dt.Rows.Count - 1]["Color"] = gvRow.Cells[15].Text;
            dt.Rows[dt.Rows.Count - 1]["ColorId"] = gvRow.Cells[16].Text;
            dt.Rows[dt.Rows.Count - 1]["SO_RES_STATUS"] = gvRow.Cells[17].Text;
            dt.Rows[dt.Rows.Count - 1]["SODetId"] = gvRow.Cells[18].Text;


            dt.AcceptChanges();
        }
        return dt;
    }

    private void SetData()
    {
        CheckBox chkAll = (CheckBox)gvSo.HeaderRow.Cells[0].FindControl("chkAll");
        chkAll.Checked = true;
        if (ViewState["SelectedRecords"] != null)
        {
            DataTable dt = (DataTable)ViewState["SelectedRecords"];
            for (int i = 0; i < gvSo.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvSo.Rows[i].Cells[0].FindControl("chk");
                if (chk != null)
                {
                    DataRow[] dr = dt.Select("ItemCode = '" + gvSo.Rows[i].Cells[1].Text + "'");
                    chk.Checked = dr.Length > 0;
                    if (!chk.Checked)
                    {
                        chkAll.Checked = false;
                    }
                }
            }
        }
    }

    private void BindSecondaryGrid()
    {
        DataTable dt = (DataTable)ViewState["SelectedRecords"];
        gvOrderAcceptanceItems.DataSource = dt;
        gvOrderAcceptanceItems.DataBind();
    }

    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ItemCode");
        dt.Columns.Add("ModelNo");
        dt.Columns.Add("ItemName");
        dt.Columns.Add("UOM");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Annexure_Qty");

        dt.Columns.Add("Rate");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Specifications");
        dt.Columns.Add("Remarks");
        dt.Columns.Add("Priority");
        dt.Columns.Add("DeliveryDate");
        dt.Columns.Add("Room");
        dt.Columns.Add("Price");
        dt.Columns.Add("Color");
        dt.Columns.Add("ColorId");
        dt.Columns.Add("SO_RES_STATUS");
        dt.Columns.Add("SODetId");
        dt.AcceptChanges();
        return dt;
    }
}


