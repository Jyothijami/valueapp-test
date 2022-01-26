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
using System.Data.SqlClient;

public partial class Modules_SM_WorkOrderDetails : basePage
{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (FileUpload1.HasFile) { }
        if (!IsPostBack)
        {
            //txtWODate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtInspectionDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            //txtDeliveryDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            SalesOrderMaster_Fill();
            DeliveryType_Fill();
            EmployeeMaster_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            //gvWorkOrderDetails.DataBind();

            if (Request.QueryString["SOId"] != null)
            {
                btnNew_Click(sender, e);
                ddlOrderAcceptance.SelectedValue = Request.QueryString["SOId"].ToString();
                ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                tblWorkOrderDetails.Visible = true;
            }
            tblPopUp1.Visible = false;

            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
            SM.WorkOrder objWorkOrder = new SM.WorkOrder();

            if (objWorkOrder.WorkOrder_Select(Request.QueryString["IoId"].ToString()) > 0)
            {
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
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString["IoNo"] !=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Status"].ToString()) && Request.QueryString["Status"].ToString() != "&nbsp;")
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
                btnEdit.Visible = false;
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

       // gvWorkOrderDetails.SelectedIndex = -1;
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

    

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            WorkOrderSave();
           // Response.Redirect("WorkOrder.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            WorkOrderUpdate();
           // Response.Redirect("WorkOrder.aspx");
        }
    }
    #endregion

    #region Work Order Save
    private void WorkOrderSave()
    {
        try
        {
            btnSave.Enabled = false;
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
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    //objWorkOrder.WODetQty = gvrow.Cells[5].Text;
                    objWorkOrder.WODetQty = qty.Text;
                    objWorkOrder.WODetSpec = gvrow.Cells[8].Text;
                    objWorkOrder.WODetRemarks = gvrow.Cells[9].Text;
                    objWorkOrder.ColorId = gvrow.Cells[15].Text;
                    //objWorkOrder.WorkOrderDetails_Save() == "Data Saved Successfully";


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
            btnSave.Enabled = true;
            //gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
         "alert(' Internal Order Saved sucessfully');window.location ='WorkOrder.aspx';", true);
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
            objWorkOrder.WOId = Request.QueryString["IoId"].ToString();
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
                    objWorkOrder.WOItemCode = gvrow.Cells[1].Text;
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    //objWorkOrder.WODetQty = gvrow.Cells[5].Text;
                    objWorkOrder.WODetQty = qty.Text;
                    objWorkOrder.WODetSpec = gvrow.Cells[8].Text;

                    objWorkOrder.WODetRemarks = gvrow.Cells[9].Text;
                    objWorkOrder.ColorId = gvrow.Cells[15].Text;
                    objWorkOrder.AnnexureQty = "0";
                    objWorkOrder.WorkOrderDetailsEdit_Save();
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
                //MessageBox.Show(this, "Data Updated Successfully");
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
            //gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
         "alert(' Internal Order Updated sucessfully');window.location ='WorkOrder.aspx';", true);
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IoNo"] !=null)
        {
            try
            {
                SM.WorkOrder objWorkOrder = new SM.WorkOrder();

                if (objWorkOrder.WorkOrder_Select(Request.QueryString["IoId"].ToString()) > 0)
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
        if (Request.QueryString["IoNo"] !=null)
        {
            try
            {
                SM.WorkOrder objWorkOrder = new SM.WorkOrder();
                MessageBox.Show(this, objWorkOrder.WorkOrder_Delete(Request.QueryString["IoId"].ToString()));
                tblWorkOrderDetails.Visible = false;
                gvOrderAcceptanceItems.DataBind();
                Response.Redirect("WorkOrder.aspx");
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                //gvWorkOrderDetails.DataBind();
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
            //////////        txtCustName.Text = objSMCustomer.CustNa   me;
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
                if (btnSave.Text == "Save")
                {
                    //txtDeliveryDate.Text = objSO.SODelivery;
                    ddlDeliveryMode.SelectedValue = objSO.DespmId;
                    txtAdvanceAmt.Text = objSO.SOAdvanceAmt;
                    lbtnAttachedFiles.Text = objSO.SOFiles;
                    txtAccessories.Text = objSO.SOAccessories;
                    txtExtraSpace.Text = objSO.SOExtraSpares;
                    lblRespId.Text = objSO.SORespId;

                    if (objSO.SOCSTax == "")
                    {
                        lblVATCSTNo.Text = "VAT";
                        txtCST.Text = objSO.SOVAT;
                    }
                    else if (objSO.SOVAT == "")
                    {
                        lblVATCSTNo.Text = "CS Tax";
                        txtCST.Text = objSO.SOCSTax;
                    }
                }
                objSO.SalesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);
                if (gvOrderAcceptanceItems.Rows.Count > 0)
                {
                    btnCheck.Visible = true;
                }
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    lblCust_Id.Text = objSO.CustId;
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
                    lblCust_Id.Text = objSO.CustId;

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
        Response.Redirect("WorkOrder.aspx");
        tblWorkOrderDetails.Visible = false;
    }
    #endregion

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IoNo"] !=null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=workorder&wono=" + Request.QueryString["IoId"].ToString() + "";
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
            objSMWOApprove.WOId = Request.QueryString["IoId"].ToString();
            objSMWOApprove.WOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            MessageBox.Show(this, objSMWOApprove.WorkOrderApprove_Update());
            SM.SalesOrder.SalesOrderStatus_Update(SM.SMStatus.Open, Request.QueryString["IoId"].ToString());
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvWorkOrderDetails.DataBind();
            SM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Send Cilck
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IoNo"] !=null)
        {
            string pagenavigationstr = "../Reports/Email.aspx?type=workorder&wono=" + Request.QueryString["IoId"].ToString() + "" +
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
            TextBox qty = (TextBox)e.Row.FindControl("txtQuantity");   
            //e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) * (Convert.ToDouble(qty.Text))).ToString();

            if (e.Row.Cells[16].Text == "True")
            {
                CheckBox ch;
                ch = (CheckBox)e.Row.FindControl("chk");
                ch.Checked = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (btnSave.Text == "Save")
            //{
            //    e.Row.Cells[0].Visible = false;
            //    btnReserve.Visible = false;
            //}
            //else { e.Row.Cells[0].Visible = false; btnReserve.Visible = false; }

            e.Row.Cells[8].Visible = e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
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

           // SM.WorkOrder objWorkOrder = new SM.WorkOrder();

            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                CheckBox chk;
                chk = (CheckBox)gvrow.FindControl("chk");
                if (chk.Checked == true)
                {                   
                        string Itemcode = gvrow.Cells[1].Text;
                        string ColorId = gvrow.Cells[15].Text;

                        //SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,Cp_Id from dbo.INWARD where ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " and Item_ID not in(select Item_ID from BlOCK where ITEM_CODE=" + Itemcode + ") and Item_ID not in(select Item_ID from OUTWARD where ITEM_CODE=" + Itemcode + ")", con);
                        SqlCommand cmd = new SqlCommand(" select isnull((SUM(isnull((Quantity),0))),0) from [V_AvaliableInwardItems] where ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " ", con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Masters.ItemPurchase obj = new Masters.ItemPurchase();
                        TextBox qty1 = (TextBox)gvrow.FindControl("txtQuantity");
                        int qty = int.Parse(qty1.Text);
                        int avlQty =Convert.ToInt32(dt.Rows[0][0].ToString());
                        if (avlQty < qty)
                        {
                            MessageBox.Show(this, "Required Quantity is not Avaliable in Stock for Item  " + gvrow.Cells[2].Text + "");
                            return;
                        }
                       
                    }           
            }

            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                CheckBox chk;
                chk = (CheckBox)gvrow.FindControl("chk");
                if (chk.Checked == true)
                {
                    string Itemcode = gvrow.Cells[1].Text;
                    string ColorId = gvrow.Cells[15].Text;

                  //  SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,cp_id from dbo.INWARD where ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " and Item_ID not in(select Item_ID from BlOCK where ITEM_CODE=" + Itemcode + ") and Item_ID not in(select Item_ID from OUTWARD where ITEM_CODE=" + Itemcode + ")", con);
                    SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,cp_id,Quantity from [V_AvaliableInwardItems] where Quantity>0 and ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " ", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Masters.ItemPurchase obj = new Masters.ItemPurchase();
                    TextBox qty1 = (TextBox)gvrow.FindControl("txtQuantity");
                    int qty = int.Parse(qty1.Text);
                    int qty2 = int.Parse(qty1.Text);

                    //objWorkOrder.WODetQty = gvrow.Cells[5].Text;

                    if (dt.Rows.Count > 0)
                    {                                                    
                            for (int i = 0; i < qty2; i++)
                            {

                                if (qty >= Convert.ToInt32(dt.Rows[i][3]))
                                {
                                    obj.Quantity = dt.Rows[i][3].ToString();
                                }
                                else if (qty < Convert.ToInt32(dt.Rows[i][3]))
                                {
                                    obj.Quantity = qty.ToString();
                                }
                                obj.itemcode = gvrow.Cells[1].Text;
                                obj.ItemID = dt.Rows[i][0].ToString();
                                obj.whLocId = dt.Rows[i][1].ToString();
                                obj.Barcode = dt.Rows[i][0].ToString();

                                // obj.companyid = lblCPID.Text;
                                obj.companyid = dt.Rows[i][2].ToString();
                                obj.POID = txtWONo.Text;
                                obj.COLORID = gvrow.Cells[15].Text;
                                obj.status = "Blocked";
                                obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[11].Text);
                                obj.CustomerId = lblCust_Id.Text;
                                obj.BlockNew_Save2();
                                qty = qty - Convert.ToInt32(dt.Rows[i][3]);
                                if (qty <= 0)
                                {
                                    break;
                                }
                            }
                        
                        
                    }                  
                }               
            }
            MessageBox.Show(this, "Stock Reserved Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
           // tblWorkOrderDetails.Visible = false;
            SM.SalesOrder objSO = new SM.SalesOrder();
            objSO.SalesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);
            lblData.Text = "";
           // Response.Redirect("WorkOrder.aspx");
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
        //gvWorkOrderDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvWorkOrderDetails.DataBind();
    }

    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvOrderAcceptanceItems.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvOrderAcceptanceItems.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;

            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }    
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        gvAvailQty.DataSource = null;
        gvAvailQty.DataBind();
        foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
        {

            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {

                DataTable IndentApprovalProducts = new DataTable();

                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("TtlQty");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("BlockQty");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("AvailQty");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("CustQty");
                IndentApprovalProducts.Columns.Add(col);
                if (gvAvailQty.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvAvailQty.Rows)
                    {
                        DataRow dr = IndentApprovalProducts.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[0].Text;
                        dr["ModelNo"] = gvrow1.Cells[1].Text;
                        dr["Color"] = gvrow1.Cells[2].Text;
                        dr["Quantity"] = gvrow1.Cells[3].Text;
                        dr["TtlQty"] = gvrow1.Cells[4].Text;
                        dr["BlockQty"] = gvrow1.Cells[5].Text;
                        dr["AvailQty"] = gvrow1.Cells[6].Text;
                        dr["CustQty"] = gvrow1.Cells[7].Text;

                        IndentApprovalProducts.Rows.Add(dr);
                    }
                }
                lblItemCode.Text = gvrow.Cells[1].Text;
                lblColor.Text = gvrow.Cells[15].Text;

                Masters.ItemMaster objMaster = new Masters.ItemMaster();
                if (objMaster.TtlStockAvailNew_select(lblItemCode.Text, lblColor.Text) > 0)
                {
                    lblTtlQty.Text = objMaster.TtlQuantity;
                    lblBlockQty.Text = objMaster.BlockQuantity;
                    lblAvaliableQty.Text = objMaster.AvaliableQuantity;
                    //lblCustQty.Text = objMaster.CustQty;
                }
                else
                {
                    lblTtlQty.Text = "0";
                    lblBlockQty.Text = "0";
                    lblAvaliableQty.Text = "0";
                   // lblCustQty.Text = "0";
                }
                if (objMaster.CustStockAvailNew_select(lblItemCode.Text, lblColor.Text, txtWONo.Text, lblCust_Id.Text) > 0)
                {                   
                    lblCustQty.Text = objMaster.CustQty;
                }
                else
                {
                    lblCustQty.Text = "0";
                }
                DataRow drnew = IndentApprovalProducts.NewRow();
                drnew["ItemCode"] = gvrow.Cells[1].Text;
                drnew["ModelNo"] = gvrow.Cells[2].Text;
                drnew["Color"] = gvrow.Cells[14].Text;
                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                drnew["Quantity"] = qty.Text;
                if(lblTtlQty.Text=="")
                {
                    drnew["TtlQty"] = "0";
                }
                else
                {
                    drnew["TtlQty"] = lblTtlQty.Text;
                }
                if (lblBlockQty.Text == "")
                {
                    drnew["BlockQty"] = "0";
                }
                else
                {
                    drnew["BlockQty"] = lblBlockQty.Text;
                }
                if (lblAvaliableQty.Text == "")
                {
                    drnew["AvailQty"] = "0";
                }
                else
                {
                    drnew["AvailQty"] = lblAvaliableQty.Text;
                }
                if (lblCustQty.Text == "")
                {
                    drnew["CustQty"] = "0";
                }
                else
                {
                    drnew["CustQty"] = lblCustQty.Text;
                }
                IndentApprovalProducts.Rows.Add(drnew);
                gvAvailQty.DataSource = IndentApprovalProducts;
                gvAvailQty.DataBind();
                ch.Checked = false;
            }
        }
    }


    protected void btnIndent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/SCM/ChangedIndent.aspx");
    }
}
 
