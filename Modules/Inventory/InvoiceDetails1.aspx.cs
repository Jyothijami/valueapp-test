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

public partial class Modules_Inventory_InvoiceDetails : basePage
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
        else if (rbBranchTransfer.Checked == true)
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
            //if (rbVAT.Checked == true)
            //{
            //    txtVAT.Text = "14.5";
            //}
            //gvSalesInvoiceDetails.DataBind();
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
            txtDiscount1.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtUnitprice.Attributes.Add("onfocus", "javascript:Unitamtcalc();");
            txtSpPrice.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");
            txtUnitprice.Attributes.Add("onkeyup", "javascript:amtcalcDisc1();");
            lblOrderedItemsHeading.Text = "Sales Ordered Items";
            txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //ItemTypes_Fill();
            EmployeeMaster_Fill();
            DeliveryType_Fill();
            // Delivery_Fill();
            //SalesOrder_Fill();
            CustomerName_Fill();
            tblSIDetails.Visible = false;

            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (Request.QueryString["SI_ID"] != null)
            {

                try
                {
                    Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

                    if (objInventory.SalesInvoice_Select(Request.QueryString["SI_ID"].ToString()) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblSIDetails.Visible = true;
                        txtInvoiceNo.Text = objInventory.SINo;
                        txtInvoiceDate.Text = Convert.ToDateTime(objInventory.SIDate).ToString("dd/MM/yyyy");
                        //txtInvoiceDate.Text = objInventory.SIDate;
                        ddlCustomerName.SelectedValue = objInventory.CustId;
                        ddlCustomerName_SelectedIndexChanged(sender, e);
                        ddlunitname.SelectedValue = objInventory.Unitname;
                        ddlunitname_SelectedIndexChanged(sender, e);
                        ddlSalesOrderNo.SelectedValue = objInventory.SOId;

                        ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                        ddlDeviveryNo.SelectedValue = objInventory.DCId;
                        ddlDeviveryNo_SelectedIndexChanged(sender, e);
                        ddlPreparedBy.SelectedValue = objInventory.SIPreparedBy;
                        ddlApprovedBy.SelectedValue = objInventory.SIApprovedBy;
                        ddlInvoiceType.SelectedValue = objInventory.SIType;
                        ddlDeliveryType.SelectedValue = objInventory.DespmId;
                        txtMiscelleneous.Text = objInventory.SIMissChrgs;
                        txtDiscount.Text = objInventory.SIDiscount;
                        txtGrossAmount.Text = objInventory.SIGrossAmt;
                        txtRemarks1.Text = objInventory.SIRemarks;
                        txtInno.Text = objInventory.InvoiceNo;
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
                        if (objInventory.SIVAT == "0" && objInventory.SICSTax == "0" && objInventory.SIBranchTransfer=="0")
                        {
                            rbCST.Checked = false;
                            rbVAT.Checked = false;
                            rbBranchTransfer.Checked = false;
                        }

                        if(gvItmDetails.Rows.Count==0)
                        {
                            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = lblTtlAmt.Text;
                        }
                        gvDeliveryChallanItems.DataBind();
                        gvItemDetails.DataBind();
                        gvInvoicedItems.DataBind();
                        // gvItmDetails.DataBind();

                        // objInventory.SalesInvoiceDetails_Select(Request.QueryString["SI_ID"].ToString(),gvItmDetails);
                        // objInventory.SalesInvoiceDetails_Select(objInventory.SOId, gvInvoicedItems);
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
            else
            {
                //Inventory.ClearControls(this);
                txtInvoiceNo.Text = Inventory.SalesInvoice.SalesInvoice_AutoGenCode();
                txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                btnSave.Visible = true;

                tblSIDetails.Visible = true;
                gvItmDetails.DataBind();
                gvItemDetails.DataBind();
                gvDeliveryChallanItems.DataBind();
                gvInvoicedItems.DataBind();
                ddlInvoiceType.SelectedIndex = 1;
                ddlDeliveryType.SelectedIndex = 1;
            }


        }

    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "70");
        btnSave.Enabled = up.Update;
        btnApprove.Enabled = up.Approve;
        btnPrint.Enabled = up.Print;
        btnDelete.Enabled = up.Delete;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //#region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        //if (txtCST.Text == "") { txtCST.Text = "0"; }
        //if (txtDiscount1.Text == "") { txtDiscount1.Text = "0"; }
        //if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        //if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        //if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }

        //txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + ((Convert.ToDecimal(txtVAT.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) + ((Convert.ToDecimal(txtCST.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        //txtGrossAmount.Text = decimal.Round(decimal.Parse(txtGrossAmount.Text), 2).ToString();

        //if (rbVAT.Checked == true)
        //{
        //    txtVAT.Style.Add("display", "");
        //    lblVAT.Style.Add("display", "");
        //    txtCST.Style.Add("display", "none");
        //    lblCSTax.Style.Add("display", "none");
        //}
        //else if (rbCST.Checked == true)
        //{
        //    txtVAT.Style.Add("display", "none");
        //    lblVAT.Style.Add("display", "none");
        //    txtCST.Style.Add("display", "");
        //    lblCSTax.Style.Add("display", "");
        //}
        //#endregion

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
        if (Request.QueryString["SI_ID"] != null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AppBy"].ToString()) && Request.QueryString["AppBy"].ToString() != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnDelete.Visible = false;
                //btnEdit.Visible = false;
                //btnPrint.Visible = true;
            }
            else
            {
                //btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                btnDelete.Visible = true;
                //btnEdit.Visible = true;
                btnPrint.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnDelete.Visible = true;
            //btnEdit.Visible = true;
            btnPrint.Visible = false;
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

    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.InvoiceCustomerMaster_SelectForCustomer(ddlCustomerName);
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
            SM.Dispose();
        }
    }
    #endregion

    #region SparesOrder Fill
    private void SparesOrder_Fill()
    {
        try
        {
            Services.SparesOrder.SparesOrder_Select(ddlSalesOrderNo);
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

    #region Delivery Fill
    private void Delivery_Fill(string SoId)
    {
        try
        {
            Inventory.Delivery.DeliveryChallanApproved_SelectSO(ddlDeviveryNo, SoId);
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

    

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
        txtInvoiceNo.Text = Inventory.SalesInvoice.SalesInvoice_AutoGenCode();
        txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        btnSave.Visible = true;

        tblSIDetails.Visible = true;
        gvItmDetails.DataBind();
        gvItemDetails.DataBind();
        gvDeliveryChallanItems.DataBind();
        gvInvoicedItems.DataBind();

    }

    #endregion

    #region Button  SAVE/UPDATE Click

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SalesInvoiceSave();
            Response.Redirect("Invoice.aspx");            
        }
        else if (btnSave.Text == "Update")
        {
            SalesInvoiceUpdate();
            Response.Redirect("Invoice.aspx");
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
                btnSave.Enabled = false;
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtBranchTransfer.Text == "") { txtBranchTransfer.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }             

                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                //Inventory.BeginTransaction();
                objInventory.SINo = txtInvoiceNo.Text;
                objInventory.SIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                if (lblSalesOrderNo.Text == "Sales Order No.")
                {
                    objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
                    objInventory.SPOId = "0";
                }
                else if (lblSalesOrderNo.Text == "Spares Order No.")
                {
                    objInventory.SPOId = ddlSalesOrderNo.SelectedItem.Value;
                    objInventory.SOId = "0";
                }
                objInventory.SIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SIApprovedBy = ddlApprovedBy.SelectedItem.Value;

                objInventory.SIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.SIMissChrgs = txtMiscelleneous.Text;
                objInventory.SIDiscount = txtDiscount.Text;
                objInventory.SIGrossAmt = txtGrossAmount.Text;
                objInventory.SIRemarks = txtRemarks1.Text;
                objInventory.SIVAT = txtVAT.Text;
                objInventory.SICSTax = txtCST.Text;
                objInventory.CpId = lblCPID.Text;
                objInventory.SIStatus = "Open";
                objInventory.InvoiceNo = txtInno.Text;
                objInventory.Unitname = ddlunitname.SelectedItem.Value;
                objInventory.SIBranchTransfer = txtBranchTransfer.Text;
                int one = 500;
                int two = 500;
                decimal amt1 = (Convert.ToDecimal(txtGrossAmount.Text) + Convert.ToDecimal(one));
                decimal amt2 = (Convert.ToDecimal(txtGrossAmount.Text) - Convert.ToDecimal(two));
                string Totalrec = lblPaymentreceived.Text;
                if (Convert.ToDecimal(Totalrec) >= Convert.ToDecimal(txtGrossAmount.Text))
                {
                    objInventory.SIStatus = "Closed";

                }
                else
                {
                    objInventory.SIStatus = "Open";
                }

                if (objInventory.SalesInvoice_Save() == "Data Saved Successfully")
                {
                    // objInventory.SalesInvoiceDetails_Delete(objInventory.SIId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SIDetQty = gvrow.Cells[6].Text;
                        objInventory.SIDetRate = gvrow.Cells[12].Text;
                        objInventory.SIDetVat = gvrow.Cells[8].Text;
                        objInventory.SIDetCst = gvrow.Cells[9].Text;
                        objInventory.SIDetExcise = gvrow.Cells[10].Text;
                        objInventory.SIDcid = gvrow.Cells[14].Text;
                        objInventory.sicoLORID = gvrow.Cells[15].Text;
                        objInventory.Remarks = gvrow.Cells[16].Text;
                        objInventory.SalesInvoiceDetails_Save();

                    }
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {

                        objInventory.SIDcid = gvrow.Cells[14].Text;

                        objInventory.SalesInvoiceDC_Update(objInventory.SIDcid);
                    }
                     foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {

                        objInventory.SINo = txtInvoiceNo.Text;

                        objInventory.DCInvoiceDtls_Update(gvrow.Cells[17].Text);
                    }
                    
                  //  Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                  //  Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
              //  Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvSalesInvoiceDetails.DataBind();
                btnSave.Enabled = true;
                gvItemDetails.DataBind();
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
        //if (gvItmDetails.Rows.Count > 0)
        //{
            try
            {
                if (txtVAT.Text == "") { txtVAT.Text = "0"; }
                if (txtCST.Text == "") { txtCST.Text = "0"; }
                if (txtBranchTransfer.Text == "") { txtBranchTransfer.Text = "0"; }
                if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
                if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

               // Inventory.BeginTransaction();

                objInventory.SIId = Request.QueryString["SI_ID"].ToString();
                objInventory.SINo = txtInvoiceNo.Text;
                objInventory.SIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                if (lblSalesOrderNo.Text == "Sales Order No.")
                {
                    objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
                    objInventory.SPOId = "0";
                }
                else if (lblSalesOrderNo.Text == "Spares Order No.")
                {
                    objInventory.SPOId = ddlSalesOrderNo.SelectedItem.Value;
                    objInventory.SOId = "0";
                }
                objInventory.SIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SIApprovedBy = ddlApprovedBy.SelectedItem.Value;
                //objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                objInventory.SIType = ddlInvoiceType.SelectedItem.Value;
                objInventory.DCId = ddlDeviveryNo.SelectedItem.Value;
                objInventory.DespmId = ddlDeliveryType.SelectedItem.Value;
                objInventory.SIMissChrgs = txtMiscelleneous.Text;
                objInventory.SIDiscount = txtDiscount.Text;
                objInventory.SIGrossAmt = txtGrossAmount.Text;
                objInventory.SIRemarks = txtRemarks1.Text;
                objInventory.SIVAT = txtVAT.Text;
                objInventory.SICSTax = txtCST.Text;
                objInventory.CpId = lblCPID.Text;
                objInventory.SIStatus = "Open";
                objInventory.InvoiceNo = txtInno.Text;
                objInventory.SIBranchTransfer = txtBranchTransfer.Text;
                objInventory.Unitname = ddlunitname.SelectedItem.Value;
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
                    // objInventory.SalesInvoiceDetails_Delete(objInventory.SIId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.SIDetQty = gvrow.Cells[6].Text;
                        objInventory.SIDetRate = gvrow.Cells[12].Text;
                        objInventory.SIDetVat = gvrow.Cells[8].Text;
                        objInventory.SIDetCst = gvrow.Cells[9].Text;
                        objInventory.SIDetExcise = gvrow.Cells[10].Text;
                        objInventory.SIDcid = gvrow.Cells[14].Text;
                        objInventory.sicoLORID = gvrow.Cells[15].Text;
                        objInventory.Remarks = gvrow.Cells[16].Text;
                        objInventory.SalesInvoiceDetails_Save();
                    }

                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {

                        objInventory.SINo = txtInvoiceNo.Text;

                        objInventory.DCInvoiceDtls_Update(gvrow.Cells[17].Text);
                    }
                   // Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                  //  Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
               // Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";

                //gvSalesInvoiceDetails.DataBind();
                gvItemDetails.DataBind();
                gvItmDetails.DataBind();
                tblSIDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please add atleast one Item for Sales Order");
        //}
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        tblSIDetails.Visible = true;

        if (Request.QueryString["SI_ID"] != null)
        {


            try
            {
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();

                if (objInventory.SalesInvoice_Select(Request.QueryString["SI_ID"].ToString()) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblSIDetails.Visible = true;
                    txtInvoiceNo.Text = objInventory.SINo;
                    txtInvoiceDate.Text = Convert.ToDateTime(objInventory.SIDate).ToString("dd/MM/yyyy");
                    ddlSalesOrderNo.SelectedValue = objInventory.SOId;
                    ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                    ddlDeviveryNo.SelectedValue = objInventory.DCId;
                    ddlDeviveryNo_SelectedIndexChanged(sender, e);
                    ddlPreparedBy.SelectedValue = objInventory.SIPreparedBy;
                    ddlApprovedBy.SelectedValue = objInventory.SIApprovedBy;
                    ddlInvoiceType.SelectedValue = objInventory.SIType;
                    ddlDeliveryType.SelectedValue = objInventory.DespmId;
                    txtMiscelleneous.Text = objInventory.SIMissChrgs;
                    txtDiscount.Text = objInventory.SIDiscount;
                    txtGrossAmount.Text = objInventory.SIGrossAmt;
                    txtRemarks1.Text = objInventory.SIRemarks;
                    txtInno.Text = objInventory.InvoiceNo;
                    ddlunitname.SelectedValue = objInventory.Unitname;
                    ddlunitname_SelectedIndexChanged(sender, e);
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
                    objInventory.SalesInvoiceDetails_Select(Request.QueryString["SI_ID"].ToString(), gvItmDetails);
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
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["SI_ID"] != null)
        {
            try
            {
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                MessageBox.Show(this, objInventory.SalesInvoice_Delete(Request.QueryString["SI_ID"].ToString()));
                Response.Redirect("Invoice.aspx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                //gvSalesInvoiceDetails.SelectedIndex = -1;
                //gvSalesInvoiceDetails.DataBind();
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region SalesOrder No Selected Index Changed
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSalesOrderNo.Text = "Sales Order No.";
        if (lblSalesOrderNo.Text == "Sales Order No.")
        {
            try
            {
                SM.SalesOrder objSM = new SM.SalesOrder();
                if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
                {
                    if (objSM.PaymentRecivedSum(ddlSalesOrderNo.SelectedItem.Value) > 0)
                    {
                        if (objSM.Paymentrec == "")
                        {
                            lblPaymentreceived.Text = "0.00";
                        }
                        else
                        {
                            lblPaymentreceived.Text = objSM.Paymentrec;
                        }
                    }
                    lblTerms.Text = objSM.SOOtherSpec;
                    Inventory.Delivery objDelivery = new Inventory.Delivery();
                    Delivery_Fill(ddlSalesOrderNo.SelectedItem.Value);
                    objDelivery.DeliveryDetails_SelectInvoiceSO(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);

                    txtSalesOrderDate.Text = objSM.SODate;
                    objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                    Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                    objInventory.SalesInvoice_SelectSO(ddlSalesOrderNo.SelectedItem.Value);
                    //objInventory.SalesInvoiceDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvInvoicedItems);

                    lblOrderedItemsHeading.Text = "Sales Ordered Items";

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
        else if (lblSalesOrderNo.Text == "Spares Order No.")
        {
            try
            {
                Services.SparesOrder objSM = new Services.SparesOrder();
                if (objSM.SparesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
                {
                    txtSalesOrderDate.Text = objSM.SPODate;
                    objSM.SparesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
                    //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    //if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                    //{
                    //    ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
                    //    //txtCustomerName.Text = objSMCustomer.CustName;
                    //    txtAddress.Text = objSMCustomer.Address;
                    //    txtEmail.Text = objSMCustomer.Email;
                    //    txtRegion.Text = objSMCustomer.RegName;
                    //    txtPhone.Text = objSMCustomer.Phone;
                    //    txtMobile.Text = objSMCustomer.Mobile;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Services.Dispose();
            }
        }
    }
    #endregion

    #region GridView Item Details Row Databound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = (Convert.ToDecimal(e.Row.Cells[7].Text) / Convert.ToInt32(e.Row.Cells[4].Text)).ToString("F");
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
        Response.Redirect("Invoice.aspx");
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
        col = new DataColumn("ModelNo");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SPPrice");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemStatus");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DCId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DetId");
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
                        dr["DeliveryDate"] = txtDeliveryDate.Text;
                        //dr["Room"] = gvrow.Cells[10].Text;
                        dr["SPPrice"] = txtUnitprice.Text;
                        dr["DCId"] = ddlDeviveryNo.SelectedItem.Value;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["DetId"] = "-";
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
                        dr["DeliveryDate"] = gvrow.Cells[13].Text;
                        dr["SPPrice"] = gvrow.Cells[12].Text;
                        dr["DCId"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;
                        dr["Remarks"] = gvrow.Cells[16].Text;
                        dr["DetId"] = gvrow.Cells[17].Text;
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
                    dr["DeliveryDate"] = gvrow.Cells[13].Text;
                    dr["SPPrice"] = gvrow.Cells[12].Text;
                    dr["DCId"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Remarks"] = gvrow.Cells[16].Text;
                    dr["DetId"] = gvrow.Cells[17].Text;

                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }

        //if (gvItmDetails.Rows.Count > 0)
        //{
        //    if (gvItmDetails.SelectedIndex == -1)
        //    {
        //        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        //        {
        //            if (gvrow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
        //            {
        //                gvItmDetails.DataSource = SalesInvoiceProducts;
        //                gvItmDetails.DataBind();
        //                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
        //                return;
        //            }
        //        }
        //    }
        //}
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
            drnew["DeliveryDate"] = txtDeliveryDate.Text;
            drnew["SPPrice"] = txtUnitprice.Text;
            drnew["DCId"] = ddlDeviveryNo.SelectedItem.Value;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["DetId"] = "-";
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
        txtDiscount1.Text = "";
        txtSpPrice.Text = "";
        txtUnitprice.Text = "";
        gvItmDetails.SelectedIndex = -1;
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
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[12].Text));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (lblTtlAmt.Text == "" || lblTtlAmt.Text == null) { lblTtlAmt.Text = "0"; }
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text =(Convert.ToDecimal(txtTotalAmt.Text) +Convert.ToDecimal(lblTtlAmt.Text)).ToString();
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
        col = new DataColumn("DCId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DetId");
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
                    dr["DCId"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Remarks"] = gvrow.Cells[16].Text;
                    dr["DetId"] = gvrow.Cells[17].Text;
                    SalesInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = SalesInvoiceProducts;
        gvItmDetails.DataBind();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt.Text)).ToString();
  

    }
    #endregion

    #region GridView sales Details Row DataBound
    protected void gvSalesInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[12].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text == "Sales")
            {
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[9].Visible = false;
            }
            else if (e.Row.Cells[5].Text == "Spares")
            {
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
        }
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["SI_ID"] != null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoice&siid=" + Request.QueryString["SI_ID"].ToString() + "&dcfor=" + Request.QueryString["DcFor"].ToString() + "";
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
                if (objDelivery.DCFor == "Sales")
                {
                    //SalesOrder_Fill();
                    lblSalesOrderNo.Text = "Sales Order No.";
                    lblSalesOrderDate.Text = "Sales Order Date";
                    lblOrderedItemsHeading.Text = "Sales Ordered Items";
                    //ddlSalesOrderNo.SelectedValue = objDelivery.SOId;
                    ddlDeliveryType.SelectedValue = objDelivery.DespmId;
                }

                objDelivery.DeliveryDetails_SelectInvoice(ddlDeviveryNo.SelectedItem.Value, gvDeliveryChallanItems);
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                objInventory.SalesInvoice_SelectDelivery(ddlDeviveryNo.SelectedItem.Value);
                objInventory.DCInvoiceDetails_Select(ddlDeviveryNo.SelectedItem.Value, gvInvoicedItems);


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
            objSI.SIId = Request.QueryString["SI_ID"].ToString();
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
            btnEdit_Click(sender, e);
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
                foreach (GridViewRow gvRow in gvItemDetails.Rows)
                {
                    if (gvRow.Cells[0].Text == ddlModelNo.SelectedItem.Value)
                    {
                        txtRate.Text = gvRow.Cells[6].Text;                        
                    }
                }
                foreach (GridViewRow gvRow in gvDeliveryChallanItems.Rows)
                {
                    if (gvRow.Cells[1].Text == ddlModelNo.SelectedItem.Value)
                    {
                        txtRemarks.Text = gvRow.Cells[14].Text;
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
    protected void btnGo_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
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
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("VAT");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Cst");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Excise");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DetId");
                DeliveryItems.Columns.Add(col);



                if (gvItemDetails.Rows.Count > 0)
                {
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
                                dr["Rate"] = gvrow.Cells[6].Text;
                                dr["VAT"] = "0";
                                dr["Cst"] = "0";
                                dr["Excise"] = "0";
                                dr["DeliveryDate"] = gvrow.Cells[9].Text;
                                dr["SPPrice"] = gvrow.Cells[7].Text;
                                dr["DCId"] = gvrow.Cells[0].Text;
                                dr["ColorId"] = gvrow.Cells[13].Text;
                                dr["Remarks"] = gvrow.Cells[14].Text;
                                dr["DetId"] = gvrow.Cells[15].Text;
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
                                dr["VAT"] = "0";
                                dr["Cst"] = "0";
                                dr["Excise"] = "0";
                                //dr["Specifications"] = gvrow1.Cells[9].Text;
                                //dr["Remarks"] = gvrow1.Cells[10].Text;
                                //dr["Priority"] = gvrow1.Cells[11].Text;
                                dr["DeliveryDate"] = gvrow.Cells[13].Text;
                                //dr["Room"] = gvrow1.Cells[13].Text;
                                dr["SPPrice"] = gvrow1.Cells[12].Text;
                                dr["DCId"] = gvrow1.Cells[14].Text;
                                dr["ColorId"] = gvrow1.Cells[15].Text;
                                dr["Remarks"] = gvrow1.Cells[16].Text;
                                dr["DetId"] = gvrow1.Cells[17].Text;

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
                            dr["VAT"] = "0";
                            dr["Cst"] = "0";
                            dr["Excise"] = "0";
                            //dr["Priority"] = gvrow1.Cells[11].Text;
                            dr["DeliveryDate"] = gvrow1.Cells[13].Text;
                            //dr["Room"] = gvrow1.Cells[13].Text;
                            dr["SPPrice"] = gvrow1.Cells[12].Text;
                            dr["DCId"] = gvrow1.Cells[14].Text;
                            dr["ColorId"] = gvrow1.Cells[15].Text;
                            dr["Remarks"] = gvrow1.Cells[16].Text;
                            dr["DetId"] = gvrow1.Cells[17].Text;
                            DeliveryItems.Rows.Add(dr);
                        }
                        if (gvItmDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[2].Text == gvrow1.Cells[3].Text && gvrow.Cells[8].Text == gvrow1.Cells[13].Text)
                            {
                                gvItmDetails.DataSource = DeliveryItems;
                                gvItmDetails.DataBind();
                                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                                btnItemRefresh_Click(sender, e);
                                ch.Checked = false;
                                return;
                            }
                        }

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
                    drnew["Rate"] = gvrow.Cells[6].Text;
                    drnew["VAT"] = "0";
                    drnew["Cst"] = "0";
                    drnew["Excise"] = "0";
                    //drnew["Specifications"] = "--";
                    // drnew["Remarks"] = "--";
                    // drnew["Priority"] = "Low";
                    drnew["DeliveryDate"] = gvrow.Cells[9].Text;
                    //drnew["Room"] = gvrow.Cells[10].Text;
                    drnew["SPPrice"] = gvrow.Cells[7].Text;
                    drnew["DCId"] = gvrow.Cells[0].Text;
                    drnew["ColorId"] = gvrow.Cells[13].Text;
                    drnew["Remarks"] = gvrow.Cells[14].Text;
                    drnew["DetId"] = gvrow.Cells[15].Text;

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



    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeviveryNo.SelectedValue = "0";
        ddlSalesOrderNo.SelectedValue = "0";
        SalesOrder_Fill();
        SM.CustomerMaster.CustomerUnits_Select(ddlunitname, ddlCustomerName.SelectedItem.Value);

        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
        {
            //ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
            //txtCustomerName.Text = objSMCustomer.CustName;
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
        }
        gvDeliveryChallanItems.DataBind();
        gvItemDetails.DataBind();
        gvItmDetails.DataBind();
    }
    protected void btnStatement_Click(object sender, EventArgs e)
    {
        //if (Request.QueryString["SI_ID"] != null)
        //{
        try
        {

            rbtnListStatement.Visible = true;

            //string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoicestatement&siid=" + ddlSalesOrderNo.SelectedValue + "";
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please select atleast a Record");
        //}
    }
    protected void rbtnListStatement_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rbtnListStatement.SelectedItem.Value == "Delevered")
        {

            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoicestatement&siid=" + ddlSalesOrderNo.SelectedValue + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

        }
        else if (rbtnListStatement.SelectedItem.Value == "Yet to Deliver")
        {
            Inventory.SalesInvoice obj = new Inventory.SalesInvoice();




            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=salesinvoicestatement_notdelivered&siid=" + ddlSalesOrderNo.SelectedValue + "&det_id=" + obj.Get_Yet_To_Deliver(Convert.ToInt16(ddlSalesOrderNo.SelectedValue)) + "";

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);




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
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = (Convert.ToDecimal(e.Row.Cells[11].Text) / Convert.ToInt32(e.Row.Cells[12].Text)).ToString("F");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = (Convert.ToDecimal(e.Row.Cells[7].Text) * Convert.ToInt32(e.Row.Cells[5].Text)).ToString("F");
        }


        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Visible = e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = e.Row.Cells[14].Visible = e.Row.Cells[15].Visible = false;
        }


    }
    protected void gvInvoicedItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[15].Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[16].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Products list?');");
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            lblTtlAmt.Text =  GrossAmount().ToString();
        }
    }
   
   
    private double GrossAmount()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvInvoicedItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }

    protected void ddlunitname_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if ((objSMCustomer.CustomerUnits_Select(ddlunitname.SelectedItem.Value)) > 0)
        {
            txtUnitaddress.Text = objSMCustomer.CustUnitAddress;
        }
    }
   
  
   
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvInvoicedItems.SelectedIndex = gvRow.RowIndex;
        Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
        objInventory.SalesInvoiceItems_Delete(gvInvoicedItems.SelectedRow.Cells[15].Text);
        MessageBox.Show(this, "Data Deleted");
        objInventory.DCInvoiceDetails_Select(ddlDeviveryNo.SelectedItem.Value, gvInvoicedItems);
        //objInventory.SalesInvoiceDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvInvoicedItems);
        lblTtlAmt.Text = GrossAmount().ToString();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt.Text)).ToString();
  
    }
    protected void rbVAT_CheckedChanged(object sender, EventArgs e)
    {
        if(rbVAT.Checked==true)
        {
            txtVAT.Text = "14.5";
        }
        else
        {
            txtVAT.Text = "0";
        }
    }
    protected void rbCST_CheckedChanged(object sender, EventArgs e)
    {
        txtVAT.Text = "0";
    }
    protected void rbBranchTransfer_CheckedChanged(object sender, EventArgs e)
    {
        txtVAT.Text = "0";
    }
}
 
