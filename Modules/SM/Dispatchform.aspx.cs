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
public partial class Modules_SM_Dispatchform : basePage
{
    decimal TotalAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            EmployeeMaster_Fill();
            CustomerName_Fill();
            Masters.CompanyProfile.Company_Select(ddlCompany);
            tblsub.Visible = false;
            gvDispatch.DataBind();
            gvDispatchIns.DataBind();
            gvItemDetails.DataBind();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "83");
        //btnAdd.Enabled = up.add;
        //btnEdit.Enabled = up.Update;
        //btnDelete.Enabled = up.Delete;
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnAd.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnSave.Enabled = up.Save;
        //btnApprove.Enabled = up.Approve;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnExit.Enabled = up.Exit;
        //btnDesPrint.Enabled = up.Print;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvDispatchIns.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvDispatchIns.SelectedRow.Cells[5].Text) && gvDispatchIns.SelectedRow.Cells[5].Text != "&nbsp;")
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                //btnPrint.Visible = true;
               
                // btnSendWorkOrder.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
               // btnSend.Visible = false;
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
           
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
               
               
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
               
            }
            //btnSendWorkOrder.Visible = false;
        }
    }
    #endregion

    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlCustomerName);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedby);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlExecutive);
            HR.EmployeeMaster.EmployeeMaster_SelectAccounts(ddlAccId);
            HR.EmployeeMaster.EmployeeMaster_SelectCMD(ddlCMD);
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

    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSalesOrderNo.SelectedValue = "0";
        
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
        {
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
        }
        SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedValue);
        SalesOrder_Fill();
    }

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlSalesOrderNo, ddlCustomerName.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           // SM.Dispose();
        }
    }
    #endregion

    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.SalesOrder objSM = new SM.SalesOrder();
            if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
            {
                txtTransportCharges.Text = objSM.SOTransportCharges;
                txtPackingCharges.Text = objSM.SOPackageCharges;
                objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
               

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            SM.Dispose();
        }
        
    }


    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource1";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
          //ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[4].Text))).ToString("F");
            e.Row.Cells[7].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[6].Text)) * (Convert.ToDecimal(e.Row.Cells[4].Text))).ToString("F");
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[6].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total Amount:";
            e.Row.Cells[7].Text = TotalAmount.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            DispatchSave();
        }
        else if (btnSave.Text == "Update")
        {
            DispatchUpdate();
        }
    }

    private void DispatchUpdate()
    {
        if (gvDispatch.Rows.Count > 0)
        {
            try
            {
                SM.Dispatch objSM = new SM.Dispatch();
               // SM.BeginTransaction();
                objSM.DispatchId = gvDispatchIns.SelectedRow.Cells[0].Text;
                objSM.CustId = ddlCustomerName.SelectedItem.Value;
                objSM.UnitId = ddlUnitName.SelectedItem.Value;
                objSM.CompanyId = ddlCompany.SelectedItem.Value;
                objSM.Remarks = txtRemarks.Text;
                objSM.SoId = ddlSalesOrderNo.SelectedItem.Value;
                objSM.PaymentsCollected = txtPaymentsCollected.Text;
                objSM.OldDues = txtOldDues.Text;
                objSM.TransportCharges = txtTransportCharges.Text;
                objSM.PackingCharges = txtPackingCharges.Text;
                objSM.Exective = ddlExecutive.SelectedItem.Value;
                objSM.Preparedby = ddlPreparedBy.SelectedItem.Value;
                objSM.ApprovedBy = ddlApprovedby.SelectedItem.Value;
                objSM.CreatedOn = DateTime.Now.ToString();
                objSM.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
                objSM.Time = txtDeliveryTime.Text;
                objSM.Status = "Open";
                if (objSM.Dispatch_Update() == "Data Updated Successfully")
                {

                    objSM.DispatchDetails_Delete(objSM.DispatchId);
                    foreach (GridViewRow gvrow in gvDispatch.Rows)
                    {
                        objSM.ItemCode = gvrow.Cells[2].Text;
                        objSM.ModelNo = gvrow.Cells[3].Text;
                        objSM.Qty = gvrow.Cells[4].Text;
                        objSM.Color = gvrow.Cells[5].Text;

                        objSM.DispatchDetails_Save();
                    }
                    //SM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                    tblsub.Visible = false;
                }
                else
                {
                   /// SM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                //SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblsub.Visible = false;
                gvDispatch.DataBind();
                btnDelete.Attributes.Clear();
                gvDispatchIns.DataBind();

                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }

    private void DispatchSave()
    {
        if (gvDispatch.Rows.Count > 0)
        {
            try
            {
                SM.Dispatch objSM = new SM.Dispatch();
                //SM.BeginTransaction();
                objSM.CustId = ddlCustomerName.SelectedItem.Value;
                objSM.UnitId = ddlUnitName.SelectedItem.Value;
                    objSM.CompanyId = ddlCompany.SelectedItem.Value;
                objSM.Remarks = txtRemarks.Text;
                objSM.SoId = ddlSalesOrderNo.SelectedItem.Value;
                objSM.PaymentsCollected = txtPaymentsCollected.Text;
                objSM.OldDues = txtOldDues.Text;
                objSM.TransportCharges = txtTransportCharges.Text;
                objSM.PackingCharges = txtPackingCharges.Text;
                objSM.Exective = ddlExecutive.SelectedItem.Value;
                objSM.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

                objSM.ApprovedBy = ddlApprovedby.SelectedItem.Value;
                objSM.CreatedOn = DateTime.Now.ToString();
                objSM.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryDate.Text);
                objSM.Time = txtDeliveryTime.Text;
                objSM.Status = "Open";
                if (objSM.Dispatch_Save() == "Data Saved Successfully")
                {
                   
                    objSM.DispatchDetails_Delete(objSM.DispatchId);
                    foreach (GridViewRow gvrow in gvDispatch.Rows)
                    {
                        objSM.ItemCode = gvrow.Cells[2].Text;
                        objSM.ModelNo = gvrow.Cells[3].Text;
                        objSM.Qty = gvrow.Cells[4].Text;
                        objSM.Color = gvrow.Cells[5].Text;
                    
                        objSM.DispatchDetails_Save();
                    }
                   // SM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                    tblsub.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                //SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblsub.Visible = false;
                gvDispatch.DataBind();
                btnDelete.Attributes.Clear();
                gvDispatchIns.DataBind();
             
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("DispatchformDetails.aspx");
        tblsub.Visible = true;
        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
        btnSave.Text = "Save";
        SM.ClearControls(this);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SM.Dispatch objSMSOApprove = new SM.Dispatch();
           // SM.BeginTransaction();
           // objSMSOApprove.SOId = gvSalesOrderDetails.SelectedRow.Cells[0].Text;
            objSMSOApprove.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            //objSMSOApprove.SalesOrderApprove_Update();
            objSMSOApprove.Status = "Closed";
                
            SM.Dispatch.DispatchApprove_Update(objSMSOApprove.ApprovedBy, gvDispatchIns.SelectedRow.Cells[0].Text,objSMSOApprove.Status);
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            //SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
            //MessageBox.Show("Approved Successfully", ex.Message);
        }
        finally
        {
            tblsub.Visible = false;
            //gvDispatchIns.DataBind();
            SM.Dispose();
            //btnEdit_Click(sender, e);
            //txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        if (gvDispatchIns.SelectedIndex > -1)
        {
            try
            {
                SM.Dispatch objSM = new SM.Dispatch();
                MessageBox.Show(this, objSM.Dispatch_Delete(gvDispatchIns.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblsub.Visible = false;
                btnDelete.Attributes.Clear();
                gvDispatchIns.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvDispatchIns.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "DeliveryDate")
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
        gvDispatchIns.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSearchBy.SelectedItem.Text == "DeliveryDate")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchText.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
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
        gvDispatchIns.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        LinkButton lbtnCustName;
        lbtnCustName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCustName.Parent.Parent;
        gvDispatchIns.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        #region Only Link Button Click
        //LinkButton lbtnWONo;
        //lbtnWONo = (LinkButton)sender;
        //GridViewRow Row = (GridViewRow)lbtnWONo.Parent.Parent;
        //gvDispatchIns.SelectedIndex = Row.RowIndex;
        //Response.Redirect("DispatchformDetails.aspx?DispatchId=" + gvDispatchIns.SelectedRow.Cells[0].Text
        //    + "&AppBy=" + gvDispatchIns.SelectedRow.Cells[5].Text);
        
        ////Old Code
        //tblsub.Visible = false;
        //LinkButton lbtnSalesOrderNo;
        //lbtnSalesOrderNo = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        //gvDispatchIns.SelectedIndex = gvRow.RowIndex;
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        //SM.Dispatch objSalesOrder = new SM.Dispatch();

        //if (objSalesOrder.Dispatch_Select(gvDispatchIns.SelectedRow.Cells[0].Text) > 0)
        //{
        //    btnSave.Enabled = false;
        //    btnSave.Text = "Update";

        //    tblsub.Visible = true;
        //    ddlCustomerName.SelectedValue = objSalesOrder.CustId;
        //    ddlCustomerName_SelectedIndexChanged(sender, e);

        //    ddlUnitName.SelectedValue = objSalesOrder.UnitId;
        //    ddlSalesOrderNo.SelectedValue = objSalesOrder.SoId;
        //    ddlSalesOrderNo_SelectedIndexChanged(sender, e);
        //    txtDeliveryDate.Text = objSalesOrder.DeliveryDate;
        //    txtDeliveryTime.Text = objSalesOrder.Time;
        //    ddlCompany.SelectedValue = objSalesOrder.CompanyId;
        //    txtRemarks.Text = objSalesOrder.Remarks;
        //    txtOldDues.Text = objSalesOrder.OldDues;
        //    txtPaymentsCollected.Text = objSalesOrder.PaymentsCollected;
        //    txtTransportCharges.Text = objSalesOrder.TransportCharges;
        //    txtPackingCharges.Text = objSalesOrder.PackingCharges;
        //    ddlExecutive.SelectedValue = objSalesOrder.Exective;
        //    ddlPreparedBy.SelectedValue = objSalesOrder.Preparedby;
        //    ddlApprovedby.SelectedValue = objSalesOrder.ApprovedBy;
        //    objSalesOrder.DispatchDetails_Select(gvDispatchIns.SelectedRow.Cells[0].Text, gvDispatch);
            
           
        //    string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

        //    if (user == "0")
        //    {
        //        btnDelete.Visible = true;
        //        btnEdit.Visible = true;
        //    }
        //    else
        //    {
        //        btnDelete.Visible = false;
        //        btnEdit.Visible = false;
        //    }


        //}
        //// btnItemRefresh_Click(sender, e);

        #endregion

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvDispatchIns.SelectedIndex > -1)
        {
            Response.Redirect("DispatchformDetails.aspx?DispatchId=" + gvDispatchIns.SelectedRow.Cells[0].Text
           + "&AppBy=" + gvDispatchIns.SelectedRow.Cells[5].Text);
        }
        else
        {
            MessageBox.Show(this, "Please Select Atleast One Record");

        }

        #region Old Code
        LinkButton lbtnWONo;
        lbtnWONo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnWONo.Parent.Parent;
        gvDispatchIns.SelectedIndex = Row.RowIndex;
        Response.Redirect("DispatchformDetails.aspx?DispatchId=" + gvDispatchIns.SelectedRow.Cells[0].Text
            + "&AppBy=" + gvDispatchIns.SelectedRow.Cells[5].Text);
        
        //Old Code
        SM.Dispatch objSalesOrder = new SM.Dispatch();

        if (objSalesOrder.Dispatch_Select(gvDispatchIns.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = true;
            btnSave.Text = "Update";

            tblsub.Visible = true;
            ddlCustomerName.SelectedValue = objSalesOrder.CustId;
            ddlCustomerName_SelectedIndexChanged(sender, e);

            ddlUnitName.SelectedValue = objSalesOrder.UnitId;
            ddlSalesOrderNo.SelectedValue = objSalesOrder.SoId;
            ddlSalesOrderNo_SelectedIndexChanged(sender, e);
            txtDeliveryDate.Text = objSalesOrder.DeliveryDate;
            txtDeliveryTime.Text = objSalesOrder.Time;
            ddlCompany.SelectedValue = objSalesOrder.CompanyId;
            txtRemarks.Text = objSalesOrder.Remarks;
            txtOldDues.Text = objSalesOrder.OldDues;
            txtPaymentsCollected.Text = objSalesOrder.PaymentsCollected;
            txtTransportCharges.Text = objSalesOrder.TransportCharges;
            txtPackingCharges.Text = objSalesOrder.PackingCharges;
            ddlExecutive.SelectedValue = objSalesOrder.Exective;
            ddlPreparedBy.SelectedValue = objSalesOrder.Preparedby;
            ddlApprovedby.SelectedValue = objSalesOrder.ApprovedBy;
            objSalesOrder.DispatchDetails_Select(gvDispatchIns.SelectedRow.Cells[0].Text, gvDispatch);

        }
        #endregion

    }
    
    protected void btnAd_Click(object sender, EventArgs e)
    {
        if (txtModelNo.Text == string.Empty)
        {
            txtModelNo.Text = "-";
        }
        if (txtItemName.Text == string.Empty)
        { txtItemName.Text = "-"; }
        if (txtColor.Text == string.Empty)
        { txtColor.Text = "-"; }
        if (txtQty.Text == string.Empty)
        {
            txtQty.Text = "-";
        }

        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Colour");
        QuotationItems.Columns.Add(col);
      
        if (gvDispatch.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDispatch.Rows)
            {
                if (gvDispatch.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvDispatch.SelectedRow.RowIndex)
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemName"] = txtItemName.Text;
                        dr["ModelNo"] = txtModelNo.Text;
                        dr["Quantity"] = txtQty.Text;
                        dr["Colour"] = txtColor.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemName"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["Quantity"] = gvrow.Cells[4].Text;
                        dr["Colour"] = gvrow.Cells[5].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();

                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["Quantity"] = gvrow.Cells[4].Text;
                    dr["Colour"] = gvrow.Cells[5].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }



        if (gvDispatch.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();

            drnew["ItemName"] = txtItemName.Text;
            drnew["ModelNo"] = txtModelNo.Text;
            drnew["Quantity"] = txtQty.Text;
            drnew["Colour"] = txtColor.Text;

            QuotationItems.Rows.Add(drnew);
        }
        gvDispatch.DataSource = QuotationItems;
        gvDispatch.DataBind();
        gvDispatch.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtModelNo.Text = "";
        txtItemName.Text = "";
        txtQty.Text = "";
        txtColor.Text = "";
    }
    protected void gvDispatch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Colour");
        QuotationItems.Columns.Add(col);
        
        if (gvDispatch.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDispatch.Rows)
            {
                DataRow dr = QuotationItems.NewRow();

                dr["ItemName"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["Quantity"] = gvrow.Cells[4].Text;
                dr["Colour"] = gvrow.Cells[5].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvDispatch.Rows[e.NewEditIndex].RowIndex)
                {
                    txtItemName.Text = gvrow.Cells[2].Text;
                    txtModelNo.Text = gvrow.Cells[3].Text;
                    txtQty.Text = gvrow.Cells[4].Text;
                    txtColor.Text = gvrow.Cells[5].Text;
                    gvDispatch.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvDispatch.DataSource = QuotationItems;
        gvDispatch.DataBind();
    }
  
    
    protected void gvDispatch_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvDispatch.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Colour");
        QuotationItems.Columns.Add(col);
       
        if (gvDispatch.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDispatch.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();

                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["Quantity"] = gvrow.Cells[4].Text;
                    dr["Colour"] = gvrow.Cells[5].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvDispatch.DataSource = QuotationItems;
        gvDispatch.DataBind();
    }
    

    protected void gvDispatchIns_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnDesPrint_Click(object sender, EventArgs e)
    {
        if (gvDispatchIns.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Dispatch&DCId=" + gvDispatchIns.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        tblsub.Visible = false;
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDispatchIns.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvDispatchIns.DataBind();
    }
    protected void gvDispatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    protected void btnCreditApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvDispatchIns.SelectedRow.Cells[0].Text != null)
            {
               
                try
                {
                    SM.Dispatch obj = new SM.Dispatch();
                    if (obj.Dispatch_SelectForCredit(gvDispatchIns.SelectedRow.Cells[0].Text) > 0)
                    {
                        txtCustName.Text = obj.CustUnitname;
                        txtcustAddress.Text = obj.UnitAddress;
                        txtcustomerEmail.Text = obj.CustomerEmailid;
                        txtCustomerMobile.Text = obj.CustomerPhoneno;
                        txtPOValue.Text = obj.POValue;
                        txtDispatchValue.Text = obj.DispatchValue;
                        lblPaymentTerms.Text = obj.PackingCharges;
                        tblpopup3.Visible = true;
                        
                        if (obj.Status == "Credit Approval Raised")
                        {
                            if (obj.CreditApp_Select(gvDispatchIns.SelectedRow.Cells[0].Text) > 0)
                            {
                                string id = obj.Credit_Id;
                                btnSavepopup.Text = "Update";
                                txtDispatchValue.Text = obj.DispatchValue;
                                txtDays.Text = obj.PaymentsCollected;
                                txtCreditAmt.Text = obj.CrAppValue;
                                txtCR.Text = obj.crValue;
                                txtUDC.Text = obj.UDCValue;
                                txtOther.Text = obj.OtherVaue;
                                ddlAccId.SelectedValue = obj.AccountsId;
                                ddlCMD.SelectedValue = obj.CMDID;
                                txtCRANo.Text = obj.CRANo;
                                txtRmks.Text = obj.Time;
                                if (obj.NoOfDays == "DR")
                                {
                                    rdbWithPo.Checked = true;
                                    rdbWithoutPo.Checked = false;
                                }
                                else
                                {
                                    rdbWithoutPo.Checked = true;
                                    rdbWithPo.Checked = false;
                                }

                            }
                        }
                        else
                        {
                            txtCRANo.Text = SM.Dispatch.CRA_AutoGenCode();
                            btnSave.Text = "Save";
                            RefreshSave();
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void RefreshSave()
    {
        txtCreditAmt.Text = "";
        txtPaymentsCollected.Text = null;
        txtCR.Text = "";
        txtDays.Text = "";
        txtUDC.Text = "";
        txtOther.Text = "";
        ddlAccId.SelectedValue = "0";
        ddlCMD.SelectedValue = "0";
    }
    protected void btnSavepopup_Click(object sender, EventArgs e)
    {
        if (btnSavepopup.Text == "Save")
        {
            CreditApproveSave();
        }
        else
        {
            CreditApproveUpdate();
        }
    }
    private void SendSmsToAccounts()
    {
        MessageBox.Show(this, "Your Credit Approval raised and forwarded to Accounts Team.");
        HR.SendSMS objsms = new HR.SendSMS();
        string msfEmp = "Dear Team, New Credit Approval is raised for " + gvDispatchIns.SelectedRow.Cells[2].Text + " By " + gvDispatchIns.SelectedRow.Cells[5].Text + ". VALUELINE";
        string MD_MNo = "8008103074";
        objsms.CreditAppSMS(msfEmp, MD_MNo);
    }
    private void CreditApproveSave()
    {
        SM.Dispatch obj = new SM.Dispatch();
        obj.DispatchId = gvDispatchIns.SelectedRow.Cells[0].Text;
        obj.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.CreatedOn = DateTime.Now.ToString();
        obj.Exective = gvDispatchIns.SelectedRow.Cells[11].Text;
        //obj.PaymentsCollected = txtDays.Text;
        obj.PaymentsCollected = Yantra.Classes.General.toMMDDYYYY(txtDays.Text);
        obj.POValue = txtPOValue.Text;
        obj.DispatchValue = txtDispatchValue.Text;
        obj.CP_ID = gvDispatch.SelectedRow.Cells[12].Text;
        if (rdbWithPo.Checked == true)
        {
            obj.NoOfDays = "DR";
        }
        else
        {
            obj.NoOfDays = "CR";
        }
        if (txtCreditAmt.Text == "" || txtCreditAmt.Text == null)
        {
            obj.CrAppValue = "0";
        }
        else { obj.CrAppValue = txtCreditAmt.Text; }
        if (txtCR.Text == "" || txtCR.Text == null)
        {
            obj.crValue = "0";
        }
        else { obj.crValue = txtCR.Text; }
        if (txtUDC.Text == "" || txtUDC.Text == null)
        {
            obj.UDCValue = "0";
        }
        else { obj.UDCValue = txtUDC.Text; }
        if (txtOther.Text == "" || txtOther.Text == null)
        {
            obj.OtherVaue = "0";
        }
        else { obj.OtherVaue = txtOther.Text; }
        
        obj.AccountsId = ddlAccId.SelectedItem.Value;
        obj.CMDID = ddlCMD.SelectedItem.Value;
        obj.CustId = gvDispatchIns.SelectedRow.Cells[9].Text;
        obj.SoId = gvDispatchIns.SelectedRow.Cells[10].Text;
        obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.Status = "Credit Approval Raised";
        obj.Time = txtRmks.Text;
        obj.CRANo = txtCRANo.Text;
        if (obj.CreditApproval_Save() == "Data Saved Successfully")
        {
            SM.Dispatch.DispatchStatus_Update(SM.SMStatus.Closed, obj.DispatchId);
            SendSmsToAccounts();

        }
        //MessageBox.Show(this, "Data Saved Successfully");
        tblpopup3.Visible = false;
    }
    private void CreditApproveUpdate()
    {
        SM.Dispatch obj = new SM.Dispatch();
        obj.DispatchId = gvDispatchIns.SelectedRow.Cells[0].Text;
        obj.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.CreatedOn = DateTime.Now.ToString();
        obj.Exective = gvDispatchIns.SelectedRow.Cells[11].Text;
        obj.PaymentsCollected = Yantra.Classes.General.toMMDDYYYY(txtDays.Text);
        obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDays.Text);
        obj.POValue = txtPOValue.Text;
        obj.DispatchValue = txtDispatchValue.Text;
        if (rdbWithPo.Checked == true)
        {
            obj.NoOfDays = "DR";
        }
        else
        {
            obj.NoOfDays = "CR";
        }
        if (txtCreditAmt.Text == "" || txtCreditAmt.Text == null)
        {
            obj.CrAppValue = "0";
        }
        else { obj.CrAppValue = txtCreditAmt.Text; }
        if (txtCR.Text == "" || txtCR.Text == null)
        {
            obj.crValue = "0";
        }
        else { obj.crValue = txtCR.Text; }
        if (txtUDC.Text == "" || txtUDC.Text == null)
        {
            obj.UDCValue = "0";
        }
        else { obj.UDCValue = txtUDC.Text; }
        if (txtOther.Text == "" || txtOther.Text == null)
        {
            obj.OtherVaue = "0";
        }
        else { obj.OtherVaue = txtOther.Text; }
        //obj.CrAppValue = txtCreditAmt.Text;
        //obj.UDCValue = txtUDC.Text;
        //obj.OtherVaue = txtOther.Text;
        obj.AccountsId = ddlAccId.SelectedItem.Value;
        obj.CMDID = ddlCMD.SelectedItem.Value;
        obj.CustId = gvDispatchIns.SelectedRow.Cells[9].Text;
        obj.SoId = gvDispatchIns.SelectedRow.Cells[10].Text;
        obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.Status = "Credit Approval Raised";
        obj.CP_ID = gvDispatch.SelectedRow.Cells[12].Text;

        obj.Time = txtRmks.Text;
        obj.CRANo = txtCRANo.Text;
        //obj.NoOfDays = "";
        
        if (obj.CreditApproval_Update() == "Data Updated Successfully")
        {
            SM.Dispatch.DispatchStatus_Update(SM.SMStatus.Closed, obj.DispatchId);
        }
        MessageBox.Show(this, "Data Updated Successfully");
        tblpopup3.Visible = false;
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Hide();
        tblpopup3.Visible = false;
    }
    protected void gvDispatchIns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvDispatchIns.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=CreditApproval&DCId=" + gvDispatchIns.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lnkDispatch_Click(object sender, EventArgs e)
    {
        pnlDispatch.Visible = true;
        pnlIndent.Visible = false;
    }
    protected void lnkIndent_Click(object sender, EventArgs e)
    {
        pnlDispatch.Visible = false;
        pnlIndent.Visible = true;
    }
    protected void gvIndentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    protected void lbtnIndentNo_Click(object sender, EventArgs e)
    {
        LinkButton lbnIndentNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbnIndentNo.Parent.Parent;
        gvIndentDetails.SelectedIndex = Row.RowIndex;
        //Response.Redirect("ChangedIndentDetails.aspx?IndentId=" + gvIndentDetails.SelectedRow.Cells[0].Text +
        //        "&AppBy=" + gvIndentDetails.SelectedRow.Cells[5].Text);



        //Old Code
        //tblIndentDetails.Visible = false;
        LinkButton lbtnIndentNo;
        lbtnIndentNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIndentNo.Parent.Parent;
        gvIndentDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("DisplayIndentRequest.aspx");
        //tblIndentDetails.Visible = true;
        SCM.ClearControls(this);
       

    }
    protected void ddlNoOfRecords1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvIndentDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvIndentDetails.DataBind();
    }
    protected void ddlSearchBy1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSearchBy1.SelectedItem.Text == "Indent Date")
        {
            ddlSymbols1.Visible = true;
            txtSearchText1.Visible = false;
            txtSearchValueToDate1.Visible = false;
            txtSearchValueFromDate1.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols1.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate1.Visible = false;
            txtSearchValueFromDate1.Visible = false;
            txtSearchText1.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols1.SelectedIndex = 0;
        }
        txtSearchText1.Text = string.Empty;
        txtSearchValueFromDate1.Text = string.Empty;
        txtSearchValueToDate1.Text = string.Empty;

    }
    protected void ddlSymbols1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols1.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate1.Visible = true;
            txtSearchValueToDate1.Visible = true;
            txtSearchText1.Visible = false;
            lblCurrentFromDate1.Visible = true;
            lblCurrentToDate1.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate1.Visible = true;
            txtSearchValueToDate1.Visible = false;
            lblCurrentFromDate1.Visible = false;
            lblCurrentToDate1.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    protected void btnSearchGo1_Click(object sender, EventArgs e)
    {
        gvIndentDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy1.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols1.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy1.SelectedItem.Text == "Indent Date")
        {
            if (ddlSymbols1.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate1.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate1.Text);
                txtSearchValueToDate1.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate1.Text);

            }
            else
            {
                txtSearchText1.Visible = false;
                txtSearchValueFromDate1.Visible = true;
                txtSearchValueToDate1.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate1.Text);
            }
        }
        else
        {
            txtSearchValueFromDate1.Visible = false;
            txtSearchValueToDate1.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvIndentDetails.DataBind();
    }
    protected void btnEdit1_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            Response.Redirect("DisplayIndentRequest.aspx?IndentId=" + gvIndentDetails.SelectedRow.Cells[0].Text +
                "&AppBy=" + gvIndentDetails.SelectedRow.Cells[5].Text + "&Cust=" + gvIndentDetails.SelectedRow.Cells[3].Text);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.Indent objSCM = new SCM.Indent();
                MessageBox.Show(this, objSCM.Indent_Delete(gvIndentDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete1.Attributes.Clear();
                gvIndentDetails.DataBind();
                gvItemDetails.DataBind();
                SCM.ClearControls(this);
                //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnPrint1_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=IndentReq&indno=" + gvIndentDetails.SelectedRow.Cells[0].Text + "";
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
}

 
