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

public partial class Modules_Inventory_CashInvoiceNew : basePage
{
    string invoiceNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        invoiceNo = Request.QueryString["invoiceNo"];
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtBranchTransfer.Text == "") { txtBranchTransfer.Text = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtVAT.Text) + Convert.ToDecimal(txtCST.Text) + Convert.ToDecimal(txtBranchTransfer.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtVAT.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();
        if (rbVAT.Checked == true)
        {
            //txtVAT.Style.Add("display", "");
            lblVAT.Style.Add("display", "");
            txtVAT.Text = GSTAmountCalc().ToString();
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
            txtBranchTransfer.Style.Add("display", "none");
            lblBranchTransfer.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            //txtCST.Style.Add("display", "");
            txtCST.Text = GSTAmountCalc().ToString();
            lblCSTax.Style.Add("display", "");
            txtBranchTransfer.Style.Add("display", "none");
            lblBranchTransfer.Style.Add("display", "none");
        }
        else if (rbBranchTransfer.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
            //txtBranchTransfer.Style.Add("display", "");
            txtBranchTransfer.Text = GSTAmountCalc().ToString();
            lblBranchTransfer.Style.Add("display", "");
        }
        #endregion
        if(!IsPostBack)
        {
            setControlsVisibility();

            if (invoiceNo == null)
            {
               // Inventory.ClearControls(this);
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
            else
            {
                try
                {
                    Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

                    if (objInventory.SalesInvoice_Select(invoiceNo) > 0)
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
                        if (objInventory.SIVAT == "0")
                        {
                            rbCST.Checked = true;
                            rbVAT.Checked = false;
                            txtCST.Text = objInventory.SICSTax;
                            txtVAT.Text = "0";
                        }
                        else if (objInventory.SICSTax == "0")
                        {
                            rbVAT.Checked = true;
                            rbCST.Checked = false;
                            txtVAT.Text = objInventory.SIVAT;
                            txtCST.Text = "0";
                        }
                        //gvDeliveryChallanItems.DataBind();
                        //gvItemDetail_RowDataBound.DataBind();

                        objInventory.SalesInvoiceDetailsSample_Select(invoiceNo, gvItmDetails);
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



            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            txtDetGST.Attributes.Add("onkeyup", "javascript:GST_amtcalc();");
            txtDetGSTAmt.Attributes.Add("onkeyup", "javascript:Gst_Disc_calc();");
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
            //tblSIDetails.Visible = false;
          //  string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
                 


        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "75");

        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        btnApprove.Enabled = up.Approve;
        //btnClose.Enabled = up.Close;
        btnPrint.Enabled = up.Print;
        
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
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtVAT.Text) + Convert.ToDecimal(txtCST.Text) + Convert.ToDecimal(txtBranchTransfer.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtVAT.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();

        if (rbVAT.Checked == true)
        {
            //txtVAT.Style.Add("display", "");
            txtVAT.Text = GSTAmountCalc().ToString();
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
            //txtCST.Style.Add("display", "");
            txtCST.Text = GSTAmountCalc().ToString();
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
            //txtBranchTransfer.Style.Add("display", "");
            txtBranchTransfer.Text = GSTAmountCalc().ToString();
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
        if (invoiceNo!=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]) && Request.QueryString["status"] != "&nbsp;")
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
                //btnDelete.Visible = true;
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectSalesIn12(ddlApprovedBy);

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


    #region Button  SAVE/UPDATE Click

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SalesInvoiceSave();
            Response.Redirect("CashInvoice.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            SalesInvoiceUpdate();
            Response.Redirect("CashInvoice.aspx");

        }
    }

    #endregion

    #region SalesInvoiceSave
    private void SalesInvoiceSave()
    {
        if (Inventory.SalesInvoice.ISInvoiceRaised(ddlDeviveryNo .SelectedItem .Value ) > 0)
        {
            MessageBox.Show(this, "Invoice has already prepared for this DC");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ISQuot()", true);

        }
        else
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
                    objInventory.SIBranchTransfer = txtBranchTransfer.Text;

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
                            objInventory.SIDcid = gvrow.Cells[15].Text;
                            objInventory.sicoLORID = gvrow.Cells[13].Text;
                            objInventory.Remarks = gvrow.Cells[14].Text;
                            objInventory.SalesInvoiceDetails_Save();
                        }
                        objInventory.DCStatus_Update();
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
                    //gvSalesInvoiceDetails.DataBind();
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
                        objInventory.SIDcid = gvrow.Cells[15].Text;
                        objInventory.sicoLORID = gvrow.Cells[13].Text;
                        objInventory.Remarks = gvrow.Cells[14].Text;
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
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Sales Order");
        }
    }
    #endregion

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
        Response.Redirect("CashInvoice.aspx");
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
                        dr["VAT"] = txtDetGST .Text;
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
                        dr["VAT"] = gvrow.Cells[8].Text;
                        dr["Cst"] = "0";
                        dr["Excise"] = "0";
                        dr["ColorId"] = gvrow.Cells[13].Text;
                        dr["Remarks"] = gvrow.Cells[14].Text;
                        dr["DCId"] = gvrow.Cells[15].Text;
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
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = "0";
                    dr["Excise"] = "0";
                    dr["ColorId"] = gvrow.Cells[13].Text;

                    dr["Remarks"] = gvrow.Cells[14].Text;
                    dr["DCId"] = gvrow.Cells[15].Text;

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
            drnew["VAT"] = txtDetGST.Text;
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
        txtDetGST.Text = string.Empty;
        txtDetGSTAmt.Text = string.Empty;
    }
    #endregion

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
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));
            e.Row.Cells[12].Text = (Convert.ToDecimal(e.Row.Cells[11].Text) * Convert.ToDecimal(e.Row.Cells[8].Text) / 100).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            if (rbVAT.Checked == true)
            {
                txtVAT.Text = GSTAmountCalc().ToString();

            }
            else if (rbCST.Checked == true)
            {
                txtCST.Text = GSTAmountCalc().ToString();
            }
            else if (rbBranchTransfer.Checked == true)
            {
                txtBranchTransfer.Text = GSTAmountCalc().ToString();
            }
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
    private double GSTAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[12].Text);
        }
        return _totalAmt;
    }
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
            //e.Row.Cells[4].Visible = false;
            //e.Row.Cells[5].Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[7].Visible = false;
        //    e.Row.Cells[9].Visible = false;
        //}
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[5].Text == "Sales")
        //    {
        //        e.Row.Cells[7].Visible = false;
        //        e.Row.Cells[9].Visible = false;
        //    }
        //    else if (e.Row.Cells[5].Text == "Spares")
        //    {
        //        e.Row.Cells[6].Visible = false;
        //        e.Row.Cells[8].Visible = false;
        //    }
        //}
    }
    #endregion

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
            dr["VAT"] = gvrow.Cells[8].Text;
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
            objSI.SIId = invoiceNo;
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

           // gvSalesInvoiceDetails.DataBind();
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
                txtDetGST.Text = objMaster.GST_Tax;
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (invoiceNo!=null)
        {
            try
            {
                string ai = "hai";
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoice&siid=" + invoiceNo + "&dcfor=" + ai + "";
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
    protected void gvExtraItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow gvr = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRate = (TextBox)gvr.FindControl("txtrategv");
            //e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[5].Text) * (Convert.ToDecimal(txtRate.Text)) / 100);
        }
    }
    protected void txtrategv_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvExtraItems .Rows)
        {
            TextBox Rate = (TextBox)gvr.FindControl("txtrategv");
            if (Rate.Text == "") { Rate.Text = "0"; }
            gvr.Cells[7].Text =Math.Round (Convert.ToDecimal(gvr.Cells[5].Text) * (Convert.ToDecimal(Rate.Text))).ToString ();
            gvr.Cells[9].Text = Math.Round((Convert.ToDecimal(gvr.Cells[7].Text) * Convert.ToDecimal(gvr.Cells[8].Text)) / 100).ToString();
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvExtraItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {
                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Vat");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("CST");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Excise");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Amount");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                
                col = new DataColumn("DCId");
                DeliveryItems.Columns.Add(col);
                foreach (GridViewRow gvrow1 in gvItmDetails.Rows)
                {
                    if (gvItmDetails.SelectedIndex > -1)
                    {
                        if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                        {
                            DataRow dr = DeliveryItems.NewRow();
                            dr["ItemCode"] = gvrow.Cells[1].Text;
                            dr["ModelNo"] = gvrow.Cells[2].Text;
                            dr["ItemName"] = gvrow.Cells[3].Text;
                            dr["UOM"] = gvrow.Cells[4].Text;
                            dr["Quantity"] = gvrow.Cells[5].Text;
                            TextBox Rate = (TextBox)gvrow.FindControl("txtrategv");
                            dr["Rate"] = Rate.Text;
                            dr["VAT"] = gvrow.Cells[8].Text;
                            dr["Cst"] = "0";
                            dr["Excise"] = "0";
                            dr["Amount"] = gvrow.Cells[7].Text;
                            //dr["SPPrice"] = gvrow.Cells[7].Text;
                            dr["DCId"] = gvrow.Cells[0].Text;
                            dr["ColorId"] = gvrow.Cells[11].Text;
                            dr["Remarks"] = gvrow.Cells[10].Text;
                            dr["DCId"] = gvrow.Cells[0].Text;
                            //dr["GST_TAX"] = gvrow.Cells[16].Text;
                            DeliveryItems.Rows.Add(dr);
                        }
                         else
                        {
                            DataRow dr = DeliveryItems.NewRow();
                            dr["ItemCode"] = gvrow1.Cells[2].Text;
                            dr["ModelNo"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;
                            dr["Rate"] = gvrow1.Cells[7].Text;
                            dr["VAT"] = gvrow1.Cells[8].Text;
                            dr["Cst"] = "0";
                            dr["Excise"] = "0";
                            //dr["Specifications"] = gvrow1.Cells[9].Text;
                            //dr["Remarks"] = gvrow1.Cells[10].Text;
                            //dr["Priority"] = gvrow1.Cells[11].Text;
                            //dr["DeliveryDate"] = gvrow.Cells[14].Text;
                            //dr["Room"] = gvrow1.Cells[13].Text;
                            //dr["SPPrice"] = gvrow1.Cells[].Text;
                            dr["DCId"] = gvrow1.Cells[15].Text;
                            dr["ColorId"] = gvrow1.Cells[13].Text;
                            dr["Remarks"] = gvrow1.Cells[14].Text;
                            //dr["DetId"] = gvrow1.Cells[18].Text;
                            //dr["GST_TAX"] = gvrow1.Cells[13].Text;
                            DeliveryItems.Rows.Add(dr);
                        }
                       
                    }
                    else
                    {
                        DataRow dr = DeliveryItems.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ModelNo"] = gvrow1.Cells[3].Text;
                        dr["ItemName"] = gvrow1.Cells[4].Text;
                        dr["UOM"] = gvrow1.Cells[5].Text;
                        dr["Quantity"] = gvrow1.Cells[6].Text;
                        dr["Rate"] = gvrow1.Cells[7].Text;
                        dr["VAT"] = gvrow1.Cells[8].Text;
                        dr["Cst"] = "0";
                        dr["Excise"] = "0";
                        //dr["Specifications"] = gvrow1.Cells[9].Text;
                        //dr["Remarks"] = gvrow1.Cells[10].Text;
                        //dr["Priority"] = gvrow1.Cells[11].Text;
                        //dr["DeliveryDate"] = gvrow.Cells[14].Text;
                        //dr["Room"] = gvrow1.Cells[13].Text;
                        //dr["SPPrice"] = gvrow1.Cells[].Text;
                        dr["DCId"] = gvrow1.Cells[15].Text;
                        dr["ColorId"] = gvrow1.Cells[13].Text;
                        dr["Remarks"] = gvrow1.Cells[14].Text;
                        //dr["DetId"] = gvrow1.Cells[18].Text;
                        //dr["GST_TAX"] = gvrow1.Cells[13].Text;
                        DeliveryItems.Rows.Add(dr);
                    }
                }
                if (gvItmDetails.SelectedIndex == -1)
                {
                    DataRow drnew = DeliveryItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[1].Text;
                    drnew["ModelNo"] = gvrow.Cells[2].Text;
                    drnew["ItemName"] = gvrow.Cells[3].Text;
                    drnew["UOM"] = gvrow.Cells[4].Text;
                    drnew["Quantity"] = gvrow.Cells[5].Text;
                    TextBox Rate = (TextBox)gvrow.FindControl("txtrategv");
                    drnew["Rate"] = Rate.Text;
                    drnew["VAT"] = gvrow.Cells[8].Text;
                    drnew["Cst"] = "0";
                    drnew["Excise"] = "0";
                    drnew["Amount"] = gvrow.Cells[7].Text;
                    //drnew["SPPrice"] = gvrow.Cells[7].Text;
                    drnew["DCId"] = gvrow.Cells[0].Text;
                    drnew["ColorId"] = gvrow.Cells[11].Text;
                    drnew["Remarks"] = gvrow.Cells[10].Text;
                    drnew["DCId"] = gvrow.Cells[0].Text;

                    DeliveryItems.Rows.Add(drnew);
                }
                gvItmDetails.DataSource = DeliveryItems;
                gvItmDetails.DataBind();
                gvItmDetails.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlDeviveryNo .DataSourceID = "SqlDataSource1";
            ddlDeviveryNo.DataTextField = "DC_NO";
            ddlDeviveryNo.DataValueField = "DC_ID";
            ddlDeviveryNo.DataBind();
            ddlDeviveryNo_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
}
 
