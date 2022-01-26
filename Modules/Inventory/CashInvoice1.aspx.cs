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
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
public partial class Modules_Inventory_CashInvoice : basePage
{

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtBranchTransfer.Text == "") { txtBranchTransfer.Text = "0"; }

        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }

        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtVAT.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();
        if (rbVAT.Checked == true)
        {
            txtVAT.Style.Add("display", "");
            lblVAT.Style.Add("display", "");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
            txtBranchTransfer.Style.Add("display", "none");
            lblBranchTransfer.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "");
            lblCSTax.Style.Add("display", "");
            txtBranchTransfer.Style.Add("display", "none");
            lblBranchTransfer.Style.Add("display", "none");
        }
        else if (rbBranchTransfer.Checked==true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
            txtBranchTransfer.Style.Add("display", "");
            lblBranchTransfer.Style.Add("display", "");
        }
        #endregion
        if (!IsPostBack)
        {
            setControlsVisibility();

            // txtInvoiceDate.Attributes.Add("onblur", "javascript:isValidDate(this);");
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
          
            rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbBranchTransfer.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");

            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtVAT.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtCST.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtBranchTransfer.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
         
           //ItemTypes_Fill();
            EmployeeMaster_Fill();
            DeliveryType_Fill();
            Delivery_Fill();
            //SalesOrder_Fill();
            //CustomerName_Fill();
            tblSIDetails.Visible = false;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = false;
                
            }
            else
            {
                btnDelete.Visible = true;
              
            }


        }

    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "75");
        btnNew.Enabled = up.add;
        btnDelete.Enabled = up.Delete;
        btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnSave.Enabled = up.Save;
        btnApprove.Enabled = up.Approve;
        //btnRefresh.Enabled = up.Refresh;
        btnPrint.Enabled = up.Print;
        //btnClose.Enabled = up.Close;

    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }

        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtVAT.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();

        if (rbVAT.Checked == true)
        {
            txtVAT.Style.Add("display", "");
            lblVAT.Style.Add("display", "");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
            txtBranchTransfer.Style.Add("display", "none");
            lblBranchTransfer.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "");
            lblCSTax.Style.Add("display", "");
            txtBranchTransfer.Style.Add("display", "none");
            lblBranchTransfer.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
            txtBranchTransfer.Style.Add("display", "");
            lblBranchTransfer.Style.Add("display", "");
        }
        #endregion
        if (btnSave.Text == "Save")
        {
            btnRefresh.Visible = true;
        }
        else if (btnSave.Text == "Update")
        {
            btnRefresh.Visible = false;
        }
        if (gvSalesInvoiceDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvSalesInvoiceDetails.SelectedRow.Cells[5].Text) && gvSalesInvoiceDetails.SelectedRow.Cells[5].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnDelete.Visible = false;
                //btnEdit.Visible = false;
                //btnPrint.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnDelete.Visible = true;
                //btnEdit.Visible = true;
                //btnPrint.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            //btnDelete.Visible = true;
            //btnEdit.Visible = true;
            //btnPrint.Visible = false;
        }
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Inventory.Delivery.DeliveryDetailsItemTypes1_Select(ddlDeviveryNo.SelectedItem.Value, ddlModelNo);
            //Masters.ItemType.ItemType_Select(ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Inventory.Dispose();
        }
    }
    #endregion

    //#region Item Name Fill
    //private void ItemName_Fill()
    //{
    //    try
    //    {
    //        Inventory.Delivery.DeliveryDetailsItemNames_Select(ddlDeviveryNo.SelectedItem.Value, ddlItemType.SelectedItem.Value, ddlItemName);
    //        //Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Inventory.Dispose();
    //    }
    //}
    //#endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);

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

    //#region CustomerName Fill
    //private void CustomerName_Fill()
    //{
    //    try
    //    {
    //        SM.CustomerMaster.InvoiceCustomerMaster_SelectForCustomer(ddlCustomerName);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        SM.Dispose();
    //    }
    //}
    //#endregion

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDeliveryType);

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

    //#region SalesOrder Fill
    //private void SalesOrder_Fill()
    //{
    //    try
    //    {
    //        SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlSalesOrderNo, ddlCustomerName.SelectedValue);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        SM.Dispose();
    //    }
    //}
    //#endregion

    //#region SparesOrder Fill
    //private void SparesOrder_Fill()
    //{
    //    try
    //    {
    //        Services.SparesOrder.SparesOrder_Select(ddlSalesOrderNo);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Services.Dispose();
    //    }
    //}
    //#endregion

    #region Delivery Fill
    private void Delivery_Fill()
    {
        try
        {
            Inventory.Delivery.DeliveryChallanSample_SelectSO(ddlDeviveryNo, cp.getPresentCompanySessionValue());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Inventory.Dispose();
        }
    }
    #endregion

    #region link button lbtnSalesInvoiceNo_Click
    protected void lbtnSalesInvoiceNo_Click(object sender, EventArgs e)
    {
        tblSIDetails.Visible = false;
        LinkButton lbtnSalesInvoiceNo;
        lbtnSalesInvoiceNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesInvoiceNo.Parent.Parent;
        gvSalesInvoiceDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        //Response.Redirect("CashInvoiceNew.aspx?invoiceNo=" + gvSalesInvoiceDetails.SelectedRow.Cells[0].Text+"&status="+gvSalesInvoiceDetails.SelectedRow.Cells[5].Text);

        try
        {
            Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

            if (objInventory.SalesInvoice_Select(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblSIDetails.Visible = true;
                txtInvoiceNo.Text = objInventory.SINo;
                txtInvoiceDate.Text = Convert.ToDateTime(objInventory.SIDate).ToString("dd/MM/yyyy");
                //txtInvoiceDate.Text = objInventory.SIDate;
                //ddlCustomerName.SelectedValue = objInventory.CustId;
                //ddlCustomerName_SelectedIndexChanged(sender, e);
                //ddlSalesOrderNo.SelectedValue = objInventory.SOId;

                //ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                ddlDeviveryNo.SelectedValue = objInventory.DCId;

                ddlDeviveryNo_SelectedIndexChanged(sender, e);
                ddlPreparedBy.SelectedValue = objInventory.SIPreparedBy;
                ddlApprovedBy.SelectedValue = objInventory.SIApprovedBy;
                ddlInvoiceType.SelectedValue = objInventory.SIType;
                txtInvo.Text = objInventory.InvoiceNo;
                ddlunitname.SelectedValue = objInventory.Unitname;
                ddlunitname_SelectedIndexChanged(sender, e);
                ddlDeliveryType.SelectedValue = objInventory.DespmId;
                txtMiscelleneous.Text = objInventory.SIMissChrgs;
                txtDiscount.Text = objInventory.SIDiscount;
                txtGrossAmount.Text = objInventory.SIGrossAmt;
                txtRemarks.Text = objInventory.SIRemarks;
                if (objInventory.SIVAT == "0" && objInventory.SIBranchTransfer == "0")
                {
                    rbBranchTransfer.Checked = false;
                    rbCST.Checked = true;
                    rbVAT.Checked = false;
                    txtCST.Text = objInventory.SICSTax;
                    txtVAT.Text = "0";
                    txtBranchTransfer.Text = "0";
                }
                else if (objInventory.SICSTax == "0" && objInventory.SIBranchTransfer == "0")
                {
                    rbVAT.Checked = true;
                    rbCST.Checked = false;
                    rbBranchTransfer.Checked = false;
                    txtVAT.Text = objInventory.SIVAT;
                    txtCST.Text = "0";
                    txtBranchTransfer.Text = "0";
                }

                else if (objInventory.SICSTax == "0" && objInventory.SIVAT == "0")
                {
                    rbVAT.Checked = false;
                    rbCST.Checked = false;
                    rbBranchTransfer.Checked = true;
                    txtVAT.Text = "0";
                    txtCST.Text = "0";
                    txtBranchTransfer.Text = objInventory.SIBranchTransfer;
                }
                if (objInventory.SIVAT == "0" && objInventory.SICSTax == "0" && objInventory.SIBranchTransfer == "0")
                {
                    rbCST.Checked = false;
                    rbVAT.Checked = false;
                    rbBranchTransfer.Checked = false;
                }
                //gvDeliveryChallanItems.DataBind();
                //gvItemDetail_RowDataBound.DataBind();

                objInventory.SalesInvoiceDetailsSample_Select(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text, gvItmDetails);
                gvItmDetails.DataBind();

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            Inventory.Dispose();
        }

    }

    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("CashInvoiceNew.aspx");
        Inventory.ClearControls(this);
        txtInvoiceNo.Text = Inventory.SalesInvoice.SalesInvoice_AutoGenCode();
        txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        btnSave.Visible = true;

        tblSIDetails.Visible = true;


        gvItmDetails.DataBind();
        //gvItemDetails.DataBind();
        //gvDeliveryChallanItems.DataBind();
        gvExtraItems.DataBind();

    }

    #endregion

    #region Button  SAVE/UPDATE Click

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SalesInvoiceSave();
        }
        else if (btnSave.Text == "Update")
        {
            SalesInvoiceUpdate();
        }
    }

    #endregion

    #region SalesInvoiceSave
    private void SalesInvoiceSave()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }

                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                Inventory.BeginTransaction();
                objInventory.SINo = txtInvoiceNo.Text;
                objInventory.SIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objInventory.SOId = "0";
                objInventory.SPOId = "0";
                objInventory.SIStatus = "Closed";
                //if (lblSalesOrderNo.Text == "Sales Order No.")
                //{
                //    objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
                //    objInventory.SPOId = "0";
                //}
                //else if (lblSalesOrderNo.Text == "Spares Order No.")
                //{
                //    objInventory.SPOId = ddlSalesOrderNo.SelectedItem.Value;
                //    objInventory.SOId = "0";
                //}
                objInventory.SIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SIApprovedBy = ddlApprovedBy.SelectedItem.Value;

                objInventory.SIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.SIMissChrgs = txtMiscelleneous.Text;
                objInventory.SIDiscount = txtDiscount.Text;
                objInventory.SIGrossAmt = txtGrossAmount.Text;
                objInventory.SIRemarks = txtRemarks.Text;
                objInventory.SIVAT = txtVAT.Text;
                objInventory.SICSTax = txtCST.Text;
                objInventory.CpId = lblCPID.Text;
                objInventory.InvoiceNo = txtInvo.Text;
                objInventory.Unitname = ddlunitname.SelectedItem.Value;
                //int one = 500;
                //int two = 500;
                //decimal amt1 = (Convert.ToDecimal(txtGrossAmount.Text) + Convert.ToDecimal(one));
                //decimal amt2 = (Convert.ToDecimal(txtGrossAmount.Text) - Convert.ToDecimal(two));
                //string Totalrec = lblPaymentreceived.Text;
                //if (Convert.ToDecimal(Totalrec) <= amt2 || Convert.ToDecimal(Totalrec) >= amt1)
                //{
                //    objInventory.SIStatus = "Closed";

                //}
                //else
                //{
                //    objInventory.SIStatus = "Open";
                //}

                if (objInventory.SalesInvoice_Save() == "Data Saved Successfully")
                {
                    objInventory.SalesInvoiceDetails_Delete(objInventory.SIId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SIDetQty = gvrow.Cells[6].Text;
                        objInventory.SIDetRate = gvrow.Cells[7].Text;
                        objInventory.SIDetVat = gvrow.Cells[8].Text;
                        objInventory.SIDetCst = gvrow.Cells[9].Text;
                        objInventory.SIDetExcise = gvrow.Cells[10].Text;
                        objInventory.SIDcid = gvrow.Cells[14].Text;
                        objInventory.sicoLORID = gvrow.Cells[12].Text;
                        objInventory.Remarks = gvrow.Cells[13].Text;
                        objInventory.SalesInvoiceDetails_Save();
                    }
                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvSalesInvoiceDetails.DataBind();
                //gvItemDetails.DataBind();
                gvSalesInvoiceDetails.DataBind();
                gvItmDetails.DataBind();
                tblSIDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region SalesInvoiceUpdate
    private void SalesInvoiceUpdate()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtBranchTransfer.Text == "") { txtBranchTransfer.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

                Inventory.BeginTransaction();

                //objInventory.SIId = gvSalesInvoiceDetails.SelectedRow.Cells[0].Text;
                objInventory.SINo = txtInvoiceNo.Text;
                objInventory.SIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.SPOId = "0";
                objInventory.SOId = "0";
                objInventory.SIId = gvSalesInvoiceDetails.SelectedRow.Cells[0].Text;

                //if (lblSalesOrderNo.Text == "Sales Order No.")
                //{
                //    objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
                //    objInventory.SPOId = "0";
                //}
                //else if (lblSalesOrderNo.Text == "Spares Order No.")
                //{
                //    objInventory.SPOId = ddlSalesOrderNo.SelectedItem.Value;
                //    objInventory.SOId = "0";
                //}
                objInventory.SIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SIApprovedBy = ddlApprovedBy.SelectedItem.Value;
                //objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                objInventory.SIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.SIMissChrgs = txtMiscelleneous.Text;
                objInventory.SIDiscount = txtDiscount.Text;
                objInventory.SIGrossAmt = txtGrossAmount.Text;
                objInventory.SIRemarks = txtRemarks.Text;
                objInventory.SIVAT = txtVAT.Text;
                objInventory.SICSTax = txtCST.Text;
                objInventory.CpId = lblCPID.Text;
                objInventory.InvoiceNo = txtInvo.Text;
                objInventory.SIBranchTransfer = txtBranchTransfer.Text;
                objInventory.Unitname = ddlunitname.SelectedItem.Value;
                //objInventory.SIStatus = "Open";
                //string amt = txtGrossAmount.Text;
                //string Totalrec = lblPaymentreceived.Text;
                //if (Convert.ToDecimal(Totalrec) <= Convert.ToDecimal(amt) + 500 || Convert.ToDecimal(Totalrec) >= Convert.ToDecimal(amt) - 500 || Convert.ToDecimal(Totalrec) == Convert.ToDecimal(amt))
                //{
                //    objInventory.SIStatus = "Closed";
                //}
                //else
                //{
                //    objInventory.SIStatus = "Open";
                //}

                if (objInventory.SalesInvoice_Update() == "Data Updated Successfully")
                {
                    objInventory.SalesInvoiceDetails_Delete(objInventory.SIId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SIDetQty = gvrow.Cells[6].Text;
                        objInventory.SIDetRate = gvrow.Cells[7].Text;
                        objInventory.SIDetVat = gvrow.Cells[8].Text;
                        objInventory.SIDetCst = gvrow.Cells[9].Text;
                        objInventory.SIDetExcise = gvrow.Cells[10].Text;
                        objInventory.SIDcid = gvrow.Cells[14].Text;
                        objInventory.sicoLORID = gvrow.Cells[12].Text;
                        objInventory.Remarks = gvrow.Cells[13].Text;
                        objInventory.SalesInvoiceDetails_Save();
                    }
                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";

                //gvSalesInvoiceDetails.DataBind();
                //gvItemDetails.DataBind();
                gvItmDetails.DataBind();
                tblSIDetails.Visible = false;
               // Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Sales Order");
        }
    }
    #endregion

    //#region Button EDIT  Click
    //protected void btnEdit_Click(object sender, EventArgs e)
    //{

    //    tblSIDetails.Visible = true;

    //    if (gvSalesInvoiceDetails.SelectedIndex > -1)
    //    {


    //        try
    //        {
    //            Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

    //            if (objInventory.SalesInvoice_Select(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
    //            {
    //                btnSave.Text = "Update";
    //                btnSave.Enabled = true;
    //                tblSIDetails.Visible = true;
    //                txtInvoiceNo.Text = objInventory.SINo;
    //                txtInvoiceDate.Text = Convert.ToDateTime(objInventory.SIDate).ToString("dd/MM/yyyy");
    //                ddlSalesOrderNo.SelectedValue = objInventory.SOId;
    //                ddlSalesOrderNo_SelectedIndexChanged(sender, e);
    //                ddlDeviveryNo.SelectedValue = objInventory.DCId;
    //                ddlDeviveryNo_SelectedIndexChanged(sender, e);
    //                ddlPreparedBy.SelectedValue = objInventory.SIPreparedBy;
    //                ddlApprovedBy.SelectedValue = objInventory.SIApprovedBy;
    //                ddlInvoiceType.SelectedValue = objInventory.SIType;
    //                ddlDeliveryType.SelectedValue = objInventory.DespmId;
    //                txtMiscelleneous.Text = objInventory.SIMissChrgs;
    //                txtDiscount.Text = objInventory.SIDiscount;
    //                txtGrossAmount.Text = objInventory.SIGrossAmt;
    //                txtRemarks.Text = objInventory.SIRemarks;
    //                if (objInventory.SIVAT == "0")
    //                {
    //                    rbCST.Checked = true;
    //                    rbVAT.Checked = false;
    //                    txtCST.Text = objInventory.SICSTax;
    //                    txtVAT.Text = "0";
    //                }
    //                else if (objInventory.SICSTax == "0")
    //                {
    //                    rbVAT.Checked = true;
    //                    rbCST.Checked = false;
    //                    txtVAT.Text = objInventory.SIVAT;
    //                    txtCST.Text = "0";
    //                }
    //                objInventory.SalesInvoiceDetails_Select(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text, gvItmDetails);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            Inventory.Dispose();
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}
    //#endregion

    //#region Button DELETE  Click
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    if (gvSalesInvoiceDetails.SelectedIndex > -1)
    //    {
    //        try
    //        {
    //            Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
    //            MessageBox.Show(this, objInventory.SalesInvoice_Delete(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text));
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message);
    //        }
    //        finally
    //        {
    //            gvSalesInvoiceDetails.SelectedIndex = -1;
    //            gvSalesInvoiceDetails.DataBind();
    //            Inventory.ClearControls(this);
    //            Inventory.Dispose();
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}
    //#endregion

    //#region SalesOrder No Selected Index Changed
    //protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblSalesOrderNo.Text = "Sales Order No.";
    //    if (lblSalesOrderNo.Text == "Sales Order No.")
    //    {
    //        try
    //        {
    //            SM.SalesOrder objSM = new SM.SalesOrder();
    //            if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
    //            {
    //                if (objSM.PaymentRecivedSum(ddlSalesOrderNo.SelectedItem.Value) > 0)
    //                {
    //                    if (objSM.Paymentrec == "")
    //                    {
    //                        lblPaymentreceived.Text = "0.00";
    //                    }
    //                    else
    //                    {
    //                        lblPaymentreceived.Text = objSM.Paymentrec;
    //                    }
    //                }

    //                Inventory.Delivery objDelivery = new Inventory.Delivery();
    //                Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
    //                objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
    //                //if (objDelivery.Delivery_SelectSO(ddlSalesOrderNo.SelectedItem.Value) > 0)
    //                //{
    //                //    Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
    //                //    objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);
    //                //}
    //                txtSalesOrderDate.Text = objSM.SODate;
    //                objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
    //                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
    //                objInventory.SalesInvoice_SelectSO(ddlSalesOrderNo.SelectedItem.Value);
    //                objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
    //                lblOrderedItemsHeading.Text = "Sales Ordered Items";
    //                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
    //                //if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
    //                //{
    //                //    ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
    //                //    //txtCustomerName.Text = objSMCustomer.CustName;
    //                //    txtAddress.Text = objSMCustomer.Address;
    //                //    txtEmail.Text = objSMCustomer.Email;
    //                //    txtRegion.Text = objSMCustomer.RegName;
    //                //    txtPhone.Text = objSMCustomer.Phone;
    //                //    txtMobile.Text = objSMCustomer.Mobile;
    //                //}
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            SM.Dispose();
    //        }
    //    }
    //    else if (lblSalesOrderNo.Text == "Spares Order No.")
    //    {
    //        try
    //        {
    //            Services.SparesOrder objSM = new Services.SparesOrder();
    //            if (objSM.SparesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
    //            {
    //                txtSalesOrderDate.Text = objSM.SPODate;
    //                objSM.SparesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
    //                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
    //                //if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
    //                //{
    //                //    ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
    //                //    //txtCustomerName.Text = objSMCustomer.CustName;
    //                //    txtAddress.Text = objSMCustomer.Address;
    //                //    txtEmail.Text = objSMCustomer.Email;
    //                //    txtRegion.Text = objSMCustomer.RegName;
    //                //    txtPhone.Text = objSMCustomer.Phone;
    //                //    txtMobile.Text = objSMCustomer.Mobile;
    //                //}
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            Services.Dispose();
    //        }
    //    }
    //}
    //#endregion

    #region GridView Item Details Row Databound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = (Convert.ToInt32(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text)).ToString();
        }
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblSIDetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (txtCST.Text == "") { txtCST.Text = "0"; }
        //if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        //if (txtExcise.Text == "") { txtExcise.Text = "0"; }
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DCId");
        SalesInvoiceProducts.Columns.Add(col);
        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvItmDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                    {

                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemName"] = txtItemname.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtQuantity.Text;
                        dr["Rate"] = txtRate.Text;
                        dr["VAT"] = "0";
                        dr["Cst"] = "0";
                        dr["Excise"] = "0";
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["Remarks"] = txtRemarks1.Text;
                        dr["DCId"] = ddlDeviveryNo.SelectedItem.Value;

                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesInvoiceProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["VAT"] = "0";
                        dr["Cst"] = "0";
                        dr["Excise"] = "0";
                        dr["ColorId"] = gvrow.Cells[12].Text;
                        dr["Remarks"] = gvrow.Cells[13].Text;
                        dr["DCId"] = gvrow.Cells[14].Text;
                        SalesInvoiceProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = "0";
                    dr["Cst"] = "0";
                    dr["Excise"] = "0";
                    dr["ColorId"] = gvrow.Cells[12].Text;

                    dr["Remarks"] = gvrow.Cells[13].Text;
                    dr["DCId"] = gvrow.Cells[14].Text;

                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }

        if (gvItmDetails.Rows.Count > 0)
        {
            if (gvItmDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItmDetails.Rows)
                {
                    //if (gvrow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
                    //{
                    //    gvItmDetails.DataSource = SalesInvoiceProducts;
                    //    gvItmDetails.DataBind();
                    //    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    //    return;
                    //}
                }
            }
        }
        if (gvItmDetails.SelectedIndex == -1)
        {
            DataRow drnew = SalesInvoiceProducts.NewRow();
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemName"] = txtItemname.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQuantity.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["VAT"] = "0";
            drnew["Cst"] = "0";
            drnew["Excise"] = "0";
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["Remarks"] = txtRemarks1.Text;
            drnew["DCId"] = ddlDeviveryNo.SelectedItem.Value;

            SalesInvoiceProducts.Rows.Add(drnew);
        }

        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
        gvItmDetails.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlModelNo.SelectedValue = "0";
        txtItemname.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtVAT.Text = string.Empty;
        txtCST.Text = string.Empty;
        txtExcise.Text = string.Empty;
        txtAmount.Text = string.Empty;
        gvItmDetails.SelectedIndex = -1;
        txtRemarks1.Text = "";
    }
    #endregion

    //#region ItemName Select Index Changed
    //protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
    //        {
    //            txtItemSpec.Text = objMaster.ItemSpec;
    //            txtItemUOM.Text = objMaster.ItemUOMShort;
    //            foreach (GridViewRow gvRow in gvItemDetails.Rows)
    //            {
    //                if (gvRow.Cells[0].Text == ddlItemName.SelectedItem.Value)
    //                {
    //                    txtRate.Text = gvRow.Cells[5].Text;
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion

    #region GridView ItmDetails Items Row DataBound
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = true;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));





        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        }
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }
    #endregion

    #region GridView gItm Items Row Deleting
    protected void gvItmDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItmDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SPPrice");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DCId");
        SalesInvoiceProducts.Columns.Add(col);

        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = "0";
                    dr["Cst"] = "0";
                    dr["Excise"] = "0";
                    dr["SPPrice"] = gvrow.Cells[12].Text;
                    dr["DeliveryDate"] = gvrow.Cells[13].Text;
                    dr["ColorId"] = gvrow.Cells[12].Text;
                    dr["Remarks"] = gvrow.Cells[13].Text;
                    dr["DCId"] = gvrow.Cells[14].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    #endregion

    #region GridView sales Details Row DataBound
    protected void gvSalesInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    //#region DropDownList Search By Select Index Changed
    //protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSearchBy.SelectedItem.Text == "Sales Order Date")
    //    {
    //        ddlSymbols.Visible = true;
    //        imgToDate.Visible = true;
    //        ceSearchValueToDate.Enabled = true;
    //        MaskedEditSearchToDate.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlSymbols.Visible = false;
    //        imgToDate.Visible = false;
    //        ceSearchValueToDate.Enabled = false;
    //        MaskedEditSearchToDate.Enabled = false;
    //        txtSearchValueFromDate.Visible = false;
    //        lblCurrentFromDate.Visible = false;
    //        lblCurrentToDate.Visible = false;
    //        imgFromDate.Visible = false;
    //        ceSearchFrom.Enabled = false;
    //        MaskedEditSearchFromDate.Enabled = false;
    //        ddlSymbols.SelectedIndex = 0;
    //    }
    //    txtSearchText.Text = string.Empty;
    //}
    //#endregion

    //#region DropDownList Symbols Select Index Changed
    //protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSymbols.SelectedItem.Text == "R")
    //    {
    //        txtSearchValueFromDate.Visible = true;
    //        lblCurrentFromDate.Visible = true;
    //        lblCurrentToDate.Visible = true;
    //        imgFromDate.Visible = true;
    //        ceSearchFrom.Enabled = true;
    //        MaskedEditSearchFromDate.Enabled = true;
    //    }
    //    else
    //    {
    //        txtSearchValueFromDate.Visible = false;
    //        lblCurrentFromDate.Visible = false;
    //        lblCurrentToDate.Visible = false;
    //        imgFromDate.Visible = false;
    //        ceSearchFrom.Enabled = false;
    //        MaskedEditSearchFromDate.Enabled = false;
    //    }
    //}
    //#endregion

    //#region Search Go Click
    //protected void btnSearchGo_Click(object sender, EventArgs e)
    //{
    //    gvSalesInvoiceDetails.SelectedIndex = -1;
    //    lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
    //    lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
    //    if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
    //    else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
    //    if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
    //    else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
    //    gvSalesInvoiceDetails.DataBind();
    //}
    //#endregion

    //#region Print Button Click
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    if (gvSalesInvoiceDetails.SelectedIndex > -1)
    //    {
    //        try
    //        {
    //            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoice&siid=" + gvSalesInvoiceDetails.SelectedRow.Cells[0].Text + "&dcfor=" + gvSalesInvoiceDetails.SelectedRow.Cells[5].Text + "";
    //            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message);
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}
    //#endregion

    #region ddlDeviveryNo_SelectedIndexChanged
    protected void ddlDeviveryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ItemTypes_Fill();
            Inventory.Delivery objDelivery = new Inventory.Delivery();
            if (objDelivery.Delivery_Select(ddlDeviveryNo.SelectedItem.Value) > 0)
            {
                txtChallanDate.Text = objDelivery.DCDate;
                ddlDeliveryType.SelectedValue = objDelivery.DespmId;

                SM.CustomerMaster.CustomerUnits_Select(ddlunitname, objDelivery.DcCustomerid);

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objDelivery.DcCustomerid) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;
                    //txtCustomerName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }

                
               // objDelivery.DeliveryDetails_SelectInvoice(ddlDeviveryNo.SelectedItem.Value, gvDeliveryChallanItems);
                objDelivery.DeliveryDetails_SelectInvoiceExtraItems(ddlDeviveryNo.SelectedItem.Value, gvExtraItems);

                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                objInventory.SalesInvoice_SelectDelivery(ddlDeviveryNo.SelectedItem.Value);
                objInventory.SalesInvoiceDetails_Select(objInventory.SIId, gvItmDetails);
                gvItmDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Inventory.Dispose();
            //ddlSalesOrderNo_SelectedIndexChanged(sender, e);
        }
    }
    #endregion

    //#region Item Type Select Index Changed
    //protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ItemName_Fill();
    //}
    //#endregion

    #region LINK BUTTON DC NO Click
    protected void lbtnDCNo_Click(object sender, EventArgs e)
    {

        //lbtnSalesInvoiceNo_Click(sender, e);
        //tblSIDetails.Visible = false;
        //LinkButton lbtnDCNo;
        //lbtnDCNo = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lbtnDCNo.Parent.Parent;
        //gvSalesInvoiceDetails.SelectedIndex = gvRow.RowIndex;
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        //try
        //{
        //    Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

        //    if (objInventory.SalesInvoice_Select(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = false;
        //        tblSIDetails.Visible = true;
        //        txtInvoiceNo.Text = objInventory.SINo;
        //        txtInvoiceDate.Text = objInventory.SIDate;
        //        ddlSalesOrderNo.SelectedValue = objInventory.SOId;
        //        ddlPreparedBy.SelectedValue = objInventory.SIPreparedBy;
        //        ddlApprovedBy.SelectedValue = objInventory.SIApprovedBy;
        //        ddlInvoiceType.SelectedValue = objInventory.SIType;
        //        ddlDeviveryNo.SelectedValue = objInventory.DCId;
        //        ddlDeliveryType.SelectedValue = objInventory.DespmId;
        //        txtMiscelleneous.Text = objInventory.SIMissChrgs;
        //        txtDiscount.Text = objInventory.SIDiscount;
        //        txtGrossAmount.Text = objInventory.SIGrossAmt;
        //        txtRemarks.Text = objInventory.SIRemarks;

        //        objInventory.SalesInvoiceDetails_Select(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text, gvItmDetails);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{

        //    Inventory.Dispose();
        //    ddlDeviveryNo_SelectedIndexChanged(sender, e);
        //}

    }
    #endregion

    #region gvItmDetails_RowEditing
    protected void gvItmDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);

        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);

        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            DataRow dr = SalesInvoiceProducts.NewRow();

            dr["ItemCode"] = gvrow.Cells[2].Text;
            dr["ItemName"] = gvrow.Cells[4].Text;
            dr["UOM"] = gvrow.Cells[5].Text;
            dr["Quantity"] = gvrow.Cells[6].Text;
            dr["Rate"] = gvrow.Cells[7].Text;
            dr["VAT"] = "0";
            dr["Cst"] = "0";
            dr["Excise"] = "0";

            dr["ModelNo"] = gvrow.Cells[3].Text;
            SalesInvoiceProducts.Rows.Add(dr);
            if (gvrow.RowIndex == gvItmDetails.Rows[e.NewEditIndex].RowIndex)
            {

                ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                ddlModelNo_SelectedIndexChanged(sender, e);
                txtItemUOM.Text = gvrow.Cells[5].Text;
                txtQuantity.Text = gvrow.Cells[6].Text;
                txtRate.Text = gvrow.Cells[7].Text;

                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text));
                gvItmDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
    }
    #endregion

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory.SalesInvoice objSI = new Inventory.SalesInvoice();
            Inventory.BeginTransaction();
            objSI.SIId = gvSalesInvoiceDetails.SelectedRow.Cells[0].Text;
            objSI.SIApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSI.SalesInvoiceApprove_Update();
            Inventory.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            Inventory.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            
            gvSalesInvoiceDetails.DataBind();
            //btnEdit_Click(sender, e);
        }
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;

                txtItemname.Text = objMaster.ItemName;
                foreach (GridViewRow gvRow in gvExtraItems.Rows)
                {
                    if (gvRow.Cells[1].Text == ddlModelNo.SelectedItem.Value)
                    {
                        txtQuantity.Text = gvRow.Cells[5].Text;
                        txtRemarks1.Text = gvRow.Cells[6].Text;

                        return;
                    }
                }

            }



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
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    //CheckBox chkItemSelect;
    //    //lbtnDCNo = (LinkButton)sender;
    //    //GridViewRow gvRow = (GridViewRow)lbtnDCNo.Parent.Parent;
    //    //gvSalesInvoiceDetails.SelectedIndex = gvRow.RowIndex;
    //    foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
    //    {
    //        CheckBox ch = new CheckBox();
    //        ch = (CheckBox)gvrow.FindControl("chkItemSelect");
    //        if (ch.Checked == true)
    //        {

    //            DataTable DeliveryItems = new DataTable();
    //            DataColumn col = new DataColumn();

    //            col = new DataColumn("ItemCode");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("ModelNo");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("ItemName");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("UOM");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("Quantity");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("Rate");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("SPPrice");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("DeliveryDate");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("ItemStatus");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("SerialNo");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("VAT");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("Cst");
    //            DeliveryItems.Columns.Add(col);
    //            col = new DataColumn("Excise");
    //            DeliveryItems.Columns.Add(col);


    //            if (gvItemDetails.Rows.Count > 0)
    //            {
    //                foreach (GridViewRow gvrow1 in gvItmDetails.Rows)
    //                {
    //                    if (gvItmDetails.SelectedIndex > -1)
    //                    {
    //                        if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
    //                        {
    //                            DataRow dr = DeliveryItems.NewRow();
    //                            dr["ItemCode"] = gvrow.Cells[1].Text;
    //                            dr["ModelNo"] = gvrow.Cells[2].Text;
    //                            dr["ItemName"] = gvrow.Cells[3].Text;
    //                            dr["UOM"] = gvrow.Cells[4].Text;
    //                            dr["Quantity"] = gvrow.Cells[5].Text;
    //                            dr["Rate"] = gvrow.Cells[6].Text;
    //                            dr["VAT"] = "0";
    //                            dr["Cst"] = "0";
    //                            dr["Excise"] = "0";
    //                            dr["DeliveryDate"] = gvrow.Cells[8].Text;
    //                            //dr["Room"] = gvrow.Cells[10].Text;
    //                            dr["SPPrice"] = gvrow.Cells[7].Text;
    //                            DeliveryItems.Rows.Add(dr);
    //                        }
    //                        else
    //                        {
    //                            DataRow dr = DeliveryItems.NewRow();
    //                            dr["ItemCode"] = gvrow1.Cells[2].Text;
    //                            dr["ModelNo"] = gvrow1.Cells[3].Text;
    //                            dr["ItemName"] = gvrow1.Cells[4].Text;
    //                            dr["UOM"] = gvrow1.Cells[5].Text;
    //                            dr["Quantity"] = gvrow1.Cells[6].Text;
    //                            dr["Rate"] = gvrow1.Cells[7].Text;
    //                            dr["VAT"] = "0";
    //                            dr["Cst"] = "0";
    //                            dr["Excise"] = "0";
    //                            //dr["Specifications"] = gvrow1.Cells[9].Text;
    //                            //dr["Remarks"] = gvrow1.Cells[10].Text;
    //                            //dr["Priority"] = gvrow1.Cells[11].Text;
    //                            dr["DeliveryDate"] = gvrow.Cells[13].Text;
    //                            //dr["Room"] = gvrow1.Cells[13].Text;
    //                            dr["SPPrice"] = gvrow1.Cells[12].Text;
    //                            DeliveryItems.Rows.Add(dr);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        DataRow dr = DeliveryItems.NewRow();
    //                        dr["ItemCode"] = gvrow1.Cells[2].Text;
    //                        dr["ModelNo"] = gvrow1.Cells[3].Text;
    //                        dr["ItemName"] = gvrow1.Cells[4].Text;
    //                        dr["UOM"] = gvrow1.Cells[5].Text;
    //                        dr["Quantity"] = gvrow1.Cells[6].Text;
    //                        dr["Rate"] = gvrow1.Cells[7].Text;
    //                        dr["VAT"] = "0";
    //                        dr["Cst"] = "0";
    //                        dr["Excise"] = "0";
    //                        //dr["Priority"] = gvrow1.Cells[11].Text;
    //                        dr["DeliveryDate"] = gvrow1.Cells[13].Text;
    //                        //dr["Room"] = gvrow1.Cells[13].Text;
    //                        dr["SPPrice"] = gvrow1.Cells[12].Text;
    //                        DeliveryItems.Rows.Add(dr);
    //                    }
    //                    if (gvItmDetails.SelectedIndex == -1)
    //                    {
    //                        if (gvrow.Cells[2].Text == gvrow1.Cells[3].Text)
    //                        {
    //                            gvItmDetails.DataSource = DeliveryItems;
    //                            gvItmDetails.DataBind();
    //                            MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
    //                            btnItemRefresh_Click(sender, e);
    //                            ch.Checked = false;
    //                            return;
    //                        }
    //                    }

    //                }
    //            }
    //            if (gvItmDetails.SelectedIndex == -1)
    //            {
    //                DataRow drnew = DeliveryItems.NewRow();
    //                drnew["ItemCode"] = gvrow.Cells[1].Text;
    //                drnew["ModelNo"] = gvrow.Cells[2].Text;
    //                drnew["ItemName"] = gvrow.Cells[3].Text;
    //                drnew["UOM"] = gvrow.Cells[4].Text;
    //                drnew["Quantity"] = gvrow.Cells[5].Text;
    //                drnew["Rate"] = gvrow.Cells[6].Text;
    //                drnew["VAT"] = "0";
    //                drnew["Cst"] = "0";
    //                drnew["Excise"] = "0";
    //                //drnew["Specifications"] = "--";
    //                // drnew["Remarks"] = "--";
    //                // drnew["Priority"] = "Low";
    //                drnew["DeliveryDate"] = gvrow.Cells[8].Text;
    //                //drnew["Room"] = gvrow.Cells[10].Text;
    //                drnew["SPPrice"] = gvrow.Cells[7].Text;

    //                DeliveryItems.Rows.Add(drnew);
    //            }
    //            gvItmDetails.DataSource = DeliveryItems;
    //            gvItmDetails.DataBind();
    //            gvItmDetails.SelectedIndex = -1;
    //            btnItemRefresh_Click(sender, e);
    //            ch.Checked = false;
    //        }


    //    }
    //}



    //protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlDeviveryNo.SelectedValue = "0";
    //    ddlSalesOrderNo.SelectedValue = "0";
    //    SalesOrder_Fill();
    //    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
    //    if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
    //    {
    //        //ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
    //        //txtCustomerName.Text = objSMCustomer.CustName;
    //        txtAddress.Text = objSMCustomer.Address;
    //        txtEmail.Text = objSMCustomer.Email;
    //        txtRegion.Text = objSMCustomer.RegName;
    //        txtPhone.Text = objSMCustomer.Phone;
    //        txtMobile.Text = objSMCustomer.Mobile;
    //    }
    //    //gvDeliveryChallanItems.DataBind();
    //    //gvItemDetails.DataBind();
    //    gvItmDetails.DataBind();
    //}
    
    //protected void rbtnListStatement_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (rbtnListStatement.SelectedItem.Value == "Delevered")
    //    {

    //        string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoicestatement&siid=" + ddlSalesOrderNo.SelectedValue + "";
    //        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    //    }
    //    else if (rbtnListStatement.SelectedItem.Value == "Yet to Deliver")
    //    {
    //        Inventory.SalesInvoice obj = new Inventory.SalesInvoice();




    //        string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoicestatement_notdelivered&siid=" + ddlSalesOrderNo.SelectedValue + "&det_id=" + obj.Get_Yet_To_Deliver(Convert.ToInt16(ddlSalesOrderNo.SelectedValue)) + "";

    //        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);




    //    }

    //}






    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSalesInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                MessageBox.Show(this, objInventory.SalesInvoice_Delete(gvSalesInvoiceDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvSalesInvoiceDetails.SelectedIndex = -1;
                gvSalesInvoiceDetails.DataBind();
                Inventory.ClearControls(this);
                //Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvSalesInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                string ai = "hai";
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoice&siid=" + gvSalesInvoiceDetails.SelectedRow.Cells[0].Text + "&dcfor=" + ai + "";
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
    protected void ddlunitname_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if ((objSMCustomer.CustomerUnits_Select(ddlunitname.SelectedItem.Value)) > 0)
        {
            txtUnitaddress.Text = objSMCustomer.CustUnitAddress;
        }
    }
    //protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    gvSalesInvoiceDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
    //    gvSalesInvoiceDetails.DataBind();
    //}
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        //lbtnSalesInvoiceNo_Click(sender, e);
        //ddlDeviveryNo_SelectedIndexChanged(sender, e);
        //Inventory.Delivery objDelivery = new Inventory.Delivery();
        //if (objDelivery.Delivery_Select(ddlDeviveryNo.SelectedItem.Value) > 0)
        //{
        //    objDelivery.DeliveryDetails_SelectInvoiceExtraItems(ddlDeviveryNo.SelectedItem.Value, gvExtraItems);
        //}
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvSalesInvoiceDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ddlSearchBy.SelectedItem.Text == "Invoice Date" )
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
        gvSalesInvoiceDetails.DataBind();
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
            
        }
        else
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
           
        }
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Invoice Date")
        {
            ddlSymbols.Visible = true;
            //imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true ;

        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
            txtSearchText.Visible = true;

        }
        txtSearchText.Text = string.Empty;
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSalesInvoiceDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSalesInvoiceDetails.DataBind();
    }
    
}

 
