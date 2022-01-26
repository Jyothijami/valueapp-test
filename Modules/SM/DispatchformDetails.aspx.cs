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

public partial class Modules_SM_DispatchformDetails : basePage
{
    decimal TotalAmount = 0;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();
            txtItemQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtDiscount1.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtUnitprice.Attributes.Add("onfocus", "javascript:Unitamtcalc();");
            txtSpPrice.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");
            txtUnitprice.Attributes.Add("onkeyup", "javascript:amtcalcDisc1();");
            EmployeeMaster_Fill();
            CustomerName_Fill();
            Masters.CompanyProfile.Company_Select(ddlCompany);
            tblsub.Visible = false;
            gvDispatch.DataBind();
            //gvDispatchIns.DataBind();
            gvItemDetails.DataBind();

            if (Request.QueryString["DispatchId"] != null)
            {
                SM.Dispatch objSalesOrder = new SM.Dispatch();

                if (objSalesOrder.Dispatch_Select(Request.QueryString["DispatchId"].ToString()) > 0)
                {
                    txtSearchModel.Enabled = btnSearchModelNo.Enabled = false;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    txtSearchModel.Enabled = false;
                    tblsub.Visible = true;
                    ddlCustomerName.SelectedValue = objSalesOrder.CustId;
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                    lblStatus.Text = objSalesOrder.Status;
                    ddlUnitName.SelectedValue = objSalesOrder.UnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
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
                    objSalesOrder.DispatchDetails_Select(Request.QueryString["DispatchId"].ToString(), gvDispatch);


                    string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

                }
                
            }
            else
            {
                ddlCustomerName.Enabled = true;
                ddlUnitName.Enabled = true;
                //ddlUnitName_SelectedIndexChanged(sender, e);
                tblsub.Visible = true;
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //btnEdit.Enabled = false;
                //btnDelete.Enabled = false;
                btnSave.Text = "Save";
               // SM.ClearControls(this);
            }
        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "83");
        
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnAd.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        btnSave.Enabled = up.add;
        btnApprove.Enabled = up.Approve;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnExit.Enabled = up.Exit;
        btnDesPrint.Enabled = up.Print;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString["DispatchId"] != null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AppBy"].ToString()) && Request.QueryString["AppBy"].ToString() != "&nbsp;")
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnEdit.Visible = true;
                //btnDelete.Visible = false;
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
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;
                //btnSendWorkOrder.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            // btnApprove.Visible = false;
            //btnPrint.Visible = false;

            //btnEdit.Visible = true;
            //btnDelete.Visible = true;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

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
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlExecutive);
            //HR.EmployeeMaster.EmployeeMaster_SelectStores(ddlExecutive);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlExecutive);
            
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
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster obj = new SM.CustomerMaster();
        if (obj.CustomerUnits_Select(ddlUnitName.SelectedValue) > 0)
        {
            txtUnitAdd.Text = obj.CustUnitAddress;
        }
    }
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
                ddlCompany.SelectedValue = objSM.Cp_ID_Confirm;
                //ddlExecutive.SelectedValue = objSM.SORespId;
                objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                BindStock();

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

    private void BindStock()
    {
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            GridView gvStock = (GridView)(gvItemDetails.Rows[gvrow.RowIndex].Cells[5].FindControl("gvStock"));

            SqlCommand cmd = new SqlCommand("SELECT(isnull((select SUM(Quantity) from V_INWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)-isnull((select SUM(Quantity) from V_OUTWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)) as TOTAL_STOCK,isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.COLOUR_ID and locid=p.locid and  V_BLOCKNew.Customer_Id ='" + ddlCustomerName .SelectedItem .Value  + "'),0) " +
                                              "as Cust_BLOCK_Stock,isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.COLOUR_ID and locid=p.locid),0) " +
                                              "as TOTAL_BLOCK_Stock,(isnull((select SUM(Quantity) from V_INWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)- isnull((select SUM(Quantity) from V_OUTWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0) -isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)) " +
                                               " as TOTAL_AVALIABLE_STOCK,p.locname,p.locid from V_INWARDNew  p left join V_OUTWARDNew out on p.item_code=out.item_code left join V_BLOCKNew blo on p.item_code=blo.item_code  where p.ITEM_CODE = '" + gvrow.Cells[0].Text + "' and p.COLOUR_ID = '" + gvrow.Cells[16].Text + "' and p.locid = '" + gvrow.Cells[15].Text + "' group by p.item_code,p.colour_id,p.locid,p.locname", con);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStock.DataSource = dt;
            gvStock.DataBind();

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
            //ddlUnitName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer )
        {
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[4].Text))).ToString("F");
            e.Row.Cells[7].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[6].Text)) * (Convert.ToDecimal(e.Row.Cells[4].Text))).ToString("F");
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[7].Text);

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
            Response.Redirect("Dispatchform.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            DispatchUpdate();
            Response.Redirect("Dispatchform.aspx");
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
                objSM.DispatchId = Request.QueryString["DispatchId"].ToString();
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
                objSM.Status = lblStatus.Text;
                if (objSM.Dispatch_Update() == "Data Updated Successfully")
                {

                    objSM.DispatchDetails_Delete(objSM.DispatchId);
                    foreach (GridViewRow gvrow in gvDispatch.Rows)
                    {
                        objSM.ItemCode = gvrow.Cells[2].Text;
                        objSM.ModelNo = gvrow.Cells[3].Text;

                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        objSM.Qty = qty.Text;
                        objSM.Rate = gvrow.Cells[7].Text;
                        Label amount = (Label)gvrow.FindControl("lblAmount");
                        objSM.Price = amount.Text;
                        //objSM.Price = gvrow.Cells[9].Text;
                        //objSM.Qty = gvrow.Cells[4].Text;
                        objSM.Color = gvrow.Cells[11].Text;

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
                //btnDelete.Attributes.Clear();
                //gvDispatchIns.DataBind();

                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    private double GSTAmountCalc()
    {
        double _totalAmt1 = 0;
        foreach (GridViewRow gvrow in gvDispatch.Rows)
        {
            _totalAmt1 = _totalAmt1 + Convert.ToDouble(gvrow.Cells[12].Text);
        }
        return _totalAmt1;
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvDispatch.Rows)
        {
            Label TotalAmt = (Label)gvrow.FindControl("lblAmount");
            _totalAmt = _totalAmt + Convert.ToDouble(TotalAmt.Text);
        }
        return _totalAmt;
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvDispatch.Rows)
        {
            TextBox quantity = (TextBox)gvr.FindControl("txtQuantity");
            Label amount = (Label)gvr.FindControl("lblAmount");
            amount.Text = (Convert.ToDecimal(gvr.Cells[7].Text) * Convert.ToDecimal(quantity.Text)).ToString();
            if (lblTotalAmt.Text == "" || lblTotalAmt.Text == null) { lblTotalAmt.Text = "0"; }
            if (lblGSTAmt.Text == "" || lblGSTAmt.Text == null) { lblGSTAmt.Text = "0"; }

            lblTotalAmt.Text = txtPaymentsCollected.Text = GrossAmountCalc().ToString();
            lblGSTAmt.Text = txtOldDues.Text = GSTAmountCalc().ToString();
            txtPackingCharges.Text = txtGrossTotalAmtHidden.Value = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal(lblGSTAmt.Text));
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
                //if (ddlSalesOrderNo.SelectedIndex > 0)
                //{
                //    objSM.SoId = ddlSalesOrderNo.SelectedItem.Value;

                //}
                //else
                //{
                //    objSM.SoId = "0";

                //}
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
                objSM.Status = "New";
                if (objSM.Dispatch_Save() == "Data Saved Successfully")
                {

                    objSM.DispatchDetails_Delete(objSM.DispatchId);
                    foreach (GridViewRow gvrow in gvDispatch.Rows)
                    {
                        objSM.ItemCode = gvrow.Cells[2].Text;
                        objSM.ModelNo = gvrow.Cells[3].Text;

                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        objSM.Qty = qty.Text;
                        objSM.Rate = gvrow.Cells[7].Text;
                        Label amount = (Label)gvrow.FindControl("lblAmount");
                        objSM.Price = amount.Text;
                        //objSM.Price = gvrow.Cells[9].Text;
                        //objSM.Qty = gvrow.Cells[4].Text;
                        objSM.Color = gvrow.Cells[11].Text;

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
                //btnDelete.Attributes.Clear();
                //gvDispatchIns.DataBind();

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
        tblsub.Visible = true;
        //btnEdit.Enabled = false;
        //btnDelete.Enabled = false;
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
            objSMSOApprove.Status = "Open";

            SM.Dispatch.DispatchApprove_Update(objSMSOApprove.ApprovedBy, Request.QueryString["DispatchId"].ToString(), objSMSOApprove.Status);
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
            Response.Redirect("Dispatchform.aspx");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        if (Request.QueryString["DispatchId"] != null)
        {
            try
            {
                SM.Dispatch objSM = new SM.Dispatch();
                MessageBox.Show(this, objSM.Dispatch_Delete(Request.QueryString["DispatchId"].ToString()));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblsub.Visible = false;
                //btnDelete.Attributes.Clear();
                //gvDispatchIns.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
   
    
    
    
    protected void btnAd_Click(object sender, EventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Price");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("HSN_CODE");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GST_TAX");
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
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemName"] = txtItemName.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["Rate"] = txtUnitprice.Text;
                        dr["Price"] = txtSpPrice.Text;
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["HSN_CODE"] = txtHSN.Text;
                        dr["GST_TAX"] = txtGST.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["Price"] = gvrow.Cells[9].Text;
                        dr["Color"] = gvrow.Cells[11].Text;
                        dr["HSN_CODE"] = gvrow.Cells[12].Text;
                        dr["GST_TAX"] = gvrow.Cells[13].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();

                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Price"] = gvrow.Cells[9].Text;
                    dr["Color"] = gvrow.Cells[11].Text;
                    dr["HSN_CODE"] = gvrow.Cells[12].Text;
                    dr["GST_TAX"] = gvrow.Cells[13].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }



        if (gvDispatch.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();

            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemName"] = txtItemName.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;
            drnew["Rate"] = txtUnitprice.Text;
            drnew["Price"] = txtSpPrice.Text;
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["HSN_CODE"] = txtHSN.Text;
            drnew["GST_TAX"] = txtGST.Text;
            QuotationItems.Rows.Add(drnew);
        }
        gvDispatch.DataSource = QuotationItems;
        gvDispatch.DataBind();
        gvDispatch.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);

    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        //txtColor.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        ddlColor.SelectedValue = "0";
        //ddlCompany.SelectedValue = "0";
        txtRemarks.Text = "";
        txtHSN.Text = "";
        txtDescription.Text = "";
        txtGST.Text = "";
        txtRate.Text = "";
        txtDiscount1.Text = "";
        txtUnitprice.Text = "";
        txtSpPrice.Text = "";
        //ddlModelNo.SelectedValue = "0";
    }
    protected void gvDispatch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("HSN_CODE");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GST_TAX");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        //col = new DataColumn("Colorid");
        //QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);

        if (gvDispatch.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDispatch.Rows)
            {
                DataRow dr = QuotationItems.NewRow();

                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["HSN_CODE"] = gvrow.Cells[4].Text;
                dr["GST_TAX"] = gvrow.Cells[5].Text;
                dr["ItemName"] = gvrow.Cells[6].Text;
                dr["UOM"] = gvrow.Cells[7].Text;
                dr["Color"] = gvrow.Cells[8].Text;
                //dr["Colorid"] = gvrow.Cells[7].Text;
                dr["Quantity"] = gvrow.Cells[9].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvDispatch.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlModelNo.SelectedItem.Value = gvrow.Cells[2].Text;
                    ddlModelNo.SelectedItem.Text = gvrow.Cells[3].Text;
                    txtHSN.Text = gvrow.Cells[4].Text;
                    txtGST.Text = gvrow.Cells[5].Text;
                    txtItemName.Text = gvrow.Cells[6].Text;
                    txtItemUOM.Text = gvrow.Cells[7].Text;
                    ddlColor.SelectedItem.Text = gvrow.Cells[8].Text;
                    //ddlColor.SelectedItem.Value = gvrow.Cells[7].Text;
                    txtItemQuantity.Text = gvrow.Cells[9].Text;
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

        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("HSN_CODE");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GST_TAX");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Price");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);

        if (gvDispatch.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDispatch.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();

                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Price"] = gvrow.Cells[9].Text;
                    dr["Color"] = gvrow.Cells[11].Text;
                    dr["HSN_CODE"] = gvrow.Cells[12].Text;
                    dr["GST_TAX"] = gvrow.Cells[13].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvDispatch.DataSource = QuotationItems;
        gvDispatch.DataBind();
    }
    

    
    protected void btnDesPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DispatchId"] != null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Dispatch&DCId=" + Request.QueryString["DispatchId"].ToString() + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dispatchform.aspx");
        tblsub.Visible = false;
    }

    protected void gvDispatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
        GridViewRow gvrow = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            TextBox txtQuantity = (TextBox)gvrow.FindControl("txtQuantity");
            Label amount = (Label)gvrow.FindControl("lblAmount");
            amount.Text = (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(gvrow.Cells[7].Text)).ToString();
            e.Row.Cells[12].Text = Convert.ToString((Convert.ToDecimal(amount.Text)) * Convert.ToDecimal(e.Row.Cells[13].Text) / 100);
        }
        if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[8].Text = "Total KMs :";
            //e.Row.Cells[9].Text =  Total.ToString();
            if (lblTotalAmt.Text == "" || lblTotalAmt.Text == null) { lblTotalAmt.Text = "0"; }
            if (lblGSTAmt.Text == "" || lblGSTAmt.Text == null) { lblGSTAmt.Text = "0"; }

            lblTotalAmt.Text = txtPaymentsCollected.Text = GrossAmountCalc().ToString();
            lblGSTAmt.Text = txtOldDues.Text = GSTAmountCalc().ToString();
            txtPackingCharges.Text = txtGrossTotalAmtHidden.Value = Convert.ToString(Convert.ToDecimal(lblTotalAmt.Text) + Convert.ToDecimal(lblGSTAmt.Text));
        }
    }
    protected void chkhdr_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvItemDetails.HeaderRow.FindControl("chkhdr");
        foreach (GridViewRow row in gvItemDetails.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkItemSelect");
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
    protected void chkItemSelect_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow1.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {

                DataTable QuotationItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Price");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Color");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("HSN_CODE");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("GST_TAX");
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
                                dr["ItemCode"] = gvrow1.Cells[0].Text;
                                dr["ModelNo"] = gvrow1.Cells[1].Text;
                                dr["ItemName"] = gvrow1.Cells[2].Text;
                                dr["UOM"] = gvrow1.Cells[3].Text;
                                TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                                dr["Quantity"] = qty.Text;
                                dr["Rate"] = gvrow1.Cells[6].Text;
                                dr["Price"] = gvrow1.Cells[8].Text;
                                dr["Color"] = gvrow1.Cells[9].Text;
                                dr["HSN_CODE"] = gvrow1.Cells[11].Text;
                                dr["GST_TAX"] = gvrow1.Cells[12].Text;
                                //dr["Quantity"] = gvrow1.Cells[4].Text;

                                QuotationItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = QuotationItems.NewRow();
                               dr["ItemCode"] = gvrow.Cells[2].Text;
                                dr["ModelNo"] = gvrow.Cells[3].Text;
                                dr["ItemName"] = gvrow.Cells[4].Text;
                                dr["UOM"] = gvrow.Cells[5].Text;
                                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                                dr["Quantity"] = qty.Text;
                                dr["Rate"] = gvrow.Cells[7].Text;
                                dr["Price"] = gvrow.Cells[9].Text;
                                dr["Color"] = gvrow.Cells[11].Text;
                                dr["HSN_CODE"] = gvrow.Cells[12].Text;
                                dr["GST_TAX"] = gvrow.Cells[13].Text;
                                QuotationItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = QuotationItems.NewRow();

                            dr["ItemCode"] = gvrow.Cells[2].Text;
                            dr["ModelNo"] = gvrow.Cells[3].Text;
                            dr["ItemName"] = gvrow.Cells[4].Text;
                            dr["UOM"] = gvrow.Cells[5].Text;
                            TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                            dr["Quantity"] = qty.Text;
                            dr["Rate"] = gvrow.Cells[7].Text;
                            dr["Price"] = gvrow.Cells[9].Text;
                            dr["Color"] = gvrow.Cells[11].Text;
                            dr["HSN_CODE"] = gvrow.Cells[12].Text;
                            dr["GST_TAX"] = gvrow.Cells[13].Text;
                            QuotationItems.Rows.Add(dr);
                        }
                    }
                }



                if (gvDispatch.SelectedIndex == -1)
                {
                    DataRow drnew = QuotationItems.NewRow();

                    drnew["ItemCode"] = gvrow1.Cells[0].Text;
                    drnew["ModelNo"] = gvrow1.Cells[1].Text;
                    drnew["ItemName"] = gvrow1.Cells[2].Text;
                    drnew["UOM"] = gvrow1.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                    drnew["Quantity"] = qty.Text;
                    drnew["Rate"] = gvrow1.Cells[6].Text;
                    drnew["Price"] = gvrow1.Cells[8].Text;
                    drnew["Color"] = gvrow1.Cells[9].Text;
                    drnew["HSN_CODE"] = gvrow1.Cells[11].Text;
                    drnew["GST_TAX"] = gvrow1.Cells[12].Text;

                    QuotationItems.Rows.Add(drnew);
                }
                gvDispatch.DataSource = QuotationItems;
                gvDispatch.DataBind();
                gvDispatch.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }
    protected void btnSearchModelNo1_Click1(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource3";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //Godown_Fill();
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtDescription.Text = objMaster.ItemSpec;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtHSN.Text = objMaster.HSN_Code;
                txtGST.Text = objMaster.GST_Tax;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value + " order by  YANTRA_ITEM_MAST.ITEM_MODEL_NO ");
    }
    
}
 
