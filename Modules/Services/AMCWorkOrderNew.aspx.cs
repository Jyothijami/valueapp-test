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
public partial class Modules_Services_AMCWorkOrderNew : basePage
{
    string woNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        woNo = Request.QueryString["woNo"];
        if(!IsPostBack)
        {
            if (woNo == "")
            {
                Services.ClearControls(this);
                // gvWorkOrderDetails.SelectedIndex = -1;
                txtWONo.Text = Services.AMCWorkOrder.AMCWorkOrder_AutoGenCode();
                txtWODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                ddlOrderAcceptance.Enabled = true;
                gvOrderAcceptanceItems.DataBind();
                gvQuotationItems.DataBind();
                tblWorkOrderDetails.Visible = true;
            }
            else

            {
                try
                {
                    Services.AMCWorkOrder objWorkOrder = new Services.AMCWorkOrder();

                    if (objWorkOrder.AMCWorkOrder_Select(woNo) > 0)
                    {
                        btnSave.Text = "Update";
                        ddlOrderAcceptance.Enabled = false;
                        btnSave.Enabled = true;
                        tblWorkOrderDetails.Visible = true;
                        ddlOrderAcceptance.SelectedValue = objWorkOrder.AMCOId;
                        txtWONo.Text = objWorkOrder.WONo;
                        txtWODate.Text = objWorkOrder.WODate;
                        ddlDeliveryMode.SelectedValue = objWorkOrder.DespId;
                        txtInspectionDate.Text = objWorkOrder.WOInspDate;
                        txtPackingInstrs.Text = objWorkOrder.WOPackForwInst;
                        txtDeliveryDate.Text = objWorkOrder.WODeliveryDate;
                        txtAccessories.Text = objWorkOrder.WOAccessories;
                        txtExtraSpace.Text = objWorkOrder.WOExtraSpares;
                        txtPMCalls.Text = objWorkOrder.WOPMCalls;
                        txtBDCalls.Text = objWorkOrder.WOBDCalls;
                        ddlPreparedBy.SelectedValue = objWorkOrder.WOPreparedBy;
                        ddlCheckedBy.SelectedValue = objWorkOrder.WOCheckedBy;
                        ddlApprovedBy.SelectedValue = objWorkOrder.WOApprovedBy;
                        objWorkOrder.AMCWorkOrderPMCallDetails_Select(woNo, gvQuotationItems);
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
                    ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                }
            }




            OrderAcceptanceMaster_Fill();
            DeliveryType_Fill();
            EmployeeMaster_Fill();
            Customer_Fill();
        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (woNo!=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]) && Request.QueryString["status"] != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnPrint.Visible = true;
                btnSend.Visible = false;
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

    #region Order Acceptance Master Fill
    private void OrderAcceptanceMaster_Fill()
    {
        try
        {
            Services.AMCOrder.AMCOrder_Select(ddlOrderAcceptance);
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


    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvQuotationItems.Rows.Count == int.Parse(txtPMCalls.Text))
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
        else
        {
            MessageBox.Show(this, "Please Add PM Calls Schedule as per the PM Calls count. The PM Calls count should match with schedule records");
        }
    }
    #endregion

    #region Work Order Save
    private void WorkOrderSave()
    {
        Services.AMCWorkOrder objsr = new Services.AMCWorkOrder();

        if (objsr.AMCOrder_isrecordexists(ddlOrderAcceptance.SelectedItem.Value) > 0)
        {
            MessageBox.Show(this, "order for " + ddlOrderAcceptance.SelectedItem.Text + " already prepared");
            Yantra.Classes.General.ClearControls(this);
            return;
        }
        try
        {
            Services.AMCWorkOrder objWorkOrder = new Services.AMCWorkOrder();
            Services.BeginTransaction();
            objWorkOrder.WONo = txtWONo.Text;
            objWorkOrder.WODate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
            objWorkOrder.AMCOId = ddlOrderAcceptance.SelectedItem.Value;
            objWorkOrder.DespId = ddlDeliveryMode.SelectedItem.Value;
            objWorkOrder.WOInspDate = Yantra.Classes.General.toMMDDYYYY(txtInspectionDate.Text);
            objWorkOrder.WOPackForwInst = txtPackingInstrs.Text;
            objWorkOrder.WODeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objWorkOrder.WOAccessories = txtAccessories.Text;
            objWorkOrder.WOExtraSpares = txtExtraSpace.Text;
            objWorkOrder.WOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWorkOrder.WOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objWorkOrder.WOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objWorkOrder.WOFLag = Services.ServicesStatus.New.ToString();
            objWorkOrder.WOPMCalls = txtPMCalls.Text;
            objWorkOrder.WOBDCalls = txtBDCalls.Text;
            objWorkOrder.WOServiceTax = txtCST.Text;

            if (objWorkOrder.AMCWorkOrder_Save() == "Data Saved Successfully")
            {
                objWorkOrder.AMCWorkOrderDetails_Delete(objWorkOrder.WOId);
                foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
                {
                    objWorkOrder.WOItemCode = gvrow.Cells[0].Text;
                    objWorkOrder.WODetQty = gvrow.Cells[3].Text;
                    objWorkOrder.WODetSpec = gvrow.Cells[6].Text;
                    objWorkOrder.WODetRemarks = gvrow.Cells[7].Text;
                    objWorkOrder.WODetRate = gvrow.Cells[4].Text;

                    objWorkOrder.AMCWorkOrderDetails_Save();
                }
                objWorkOrder.AMCWorkOrderPMCallDetails_Delete(objWorkOrder.WOId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objWorkOrder.WOCallName = gvrow.Cells[2].Text;
                    objWorkOrder.WOCallDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                    objWorkOrder.AMCWorkOrderPMCallDetails_Save();
                }

                Services.AMCOrderAcceptance.AMCOrderAcceptanceStatus_Update(Services.ServicesStatus.Closed, objWorkOrder.AMCOId);
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
            //btnDelete.Attributes.Clear();
            //gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }

    }
    #endregion

    #region Work Order Update
    private void WorkOrderUpdate()
    {
        try
        {
            Services.AMCWorkOrder objWorkOrder = new Services.AMCWorkOrder();
            Services.BeginTransaction();
            objWorkOrder.WOId = woNo;
            objWorkOrder.WONo = txtWONo.Text;
            objWorkOrder.WODate = Yantra.Classes.General.toMMDDYYYY(txtWODate.Text);
            objWorkOrder.AMCOId = ddlOrderAcceptance.SelectedItem.Value;
            objWorkOrder.DespId = ddlDeliveryMode.SelectedItem.Value;
            objWorkOrder.WOInspDate = Yantra.Classes.General.toMMDDYYYY(txtInspectionDate.Text);
            objWorkOrder.WOPackForwInst = txtPackingInstrs.Text;
            objWorkOrder.WODeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
            objWorkOrder.WOAccessories = txtAccessories.Text;
            objWorkOrder.WOExtraSpares = txtExtraSpace.Text;
            objWorkOrder.WOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWorkOrder.WOCheckedBy = ddlCheckedBy.SelectedItem.Value;
            objWorkOrder.WOApprovedBy = ddlApprovedBy.SelectedItem.Value;
            objWorkOrder.WOFLag = Services.ServicesStatus.New.ToString();
            objWorkOrder.WOPMCalls = txtPMCalls.Text;
            objWorkOrder.WOBDCalls = txtBDCalls.Text;
            objWorkOrder.WOServiceTax = txtCST.Text;

            if (objWorkOrder.AMCWorkOrder_Update() == "Data Updated Successfully")
            {
                objWorkOrder.AMCWorkOrderDetails_Delete(objWorkOrder.WOId);
                foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
                {
                    objWorkOrder.WOItemCode = gvrow.Cells[0].Text;
                    objWorkOrder.WODetQty = gvrow.Cells[3].Text;
                    objWorkOrder.WODetSpec = gvrow.Cells[6].Text;
                    objWorkOrder.WODetRemarks = gvrow.Cells[7].Text;

                    objWorkOrder.AMCWorkOrderDetails_Save();
                }
                objWorkOrder.AMCWorkOrderPMCallDetails_Delete(objWorkOrder.WOId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objWorkOrder.WOCallName = gvrow.Cells[2].Text;
                    objWorkOrder.WOCallDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                    objWorkOrder.AMCWorkOrderPMCallDetails_Save();
                }
                Services.AMCOrderAcceptance.AMCOrderAcceptanceStatus_Update(Services.ServicesStatus.Closed, objWorkOrder.AMCOId);
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
            //btnDelete.Attributes.Clear();
            //gvWorkOrderDetails.DataBind();
            gvOrderAcceptanceItems.DataBind();
            tblWorkOrderDetails.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }

    }
    #endregion

    #region Order Acceptance No Selected Index Changed
    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.AMCOrder objOA = new Services.AMCOrder();
            if (objOA.AMCOrder_Select(ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                txtOADate.Text = objOA.AMCODate;
                txtCST.Text = objOA.AMCOCSTax;
                ddlDeliveryMode.SelectedValue = objOA.DespmId;
                ddlCustomerName.SelectedValue = objOA.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objOA.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objOA.CustDetId;
                ddlContactPerson_SelectedIndexChanged(sender, e);
                if (btnSave.Text == "Save")
                {
                    txtPMCalls.Text = objOA.AMCOPMCalls;
                    txtBDCalls.Text = objOA.AMCOBDCalls;
                }

                objOA.AMCOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);

                //Services.CustomerMaster objSMCustomer = new Services.CustomerMaster();
                //if (objSMCustomer.CustomerMaster_Select(objOA.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //}
            }
            else
            {
                MessageBox.Show(this, "The AMC Order " + ddlOrderAcceptance.SelectedItem.Text + " Has Been Deleted So U cannot Prepare Profile");
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
            if (txtWONo.Text == String.Empty)
            {
                txtWONo.Text = Services.AMCWorkOrder.AMCWorkOrder_AutoGenCode();
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

    #region GridView Work Order Details Row Databound
    protected void gvWorkOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("AMCWorkOrder.aspx");
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

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (woNo!=null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=workorder&wono=" + woNo + "";
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
            Services.AMCWorkOrder objSMWOApprove = new Services.AMCWorkOrder();
            Services.BeginTransaction();
            objSMWOApprove.WOId = woNo;
            objSMWOApprove.WOApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            MessageBox.Show(this, objSMWOApprove.AMCWorkOrderApprove_Update());
            Services.AMCWorkOrder.AMCWorkOrderStatus_Update(Services.ServicesStatus.Open, woNo);
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvWorkOrderDetails.DataBind();
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
 
