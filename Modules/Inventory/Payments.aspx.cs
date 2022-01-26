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
public partial class Modules_Inventory_Payments : basePage
{
    decimal TotalAmount = 0;
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerMaster_Fill();

            // SalesOrder_Fill();
            EmployeeMaster_Fill();
            //   SalesInvoiceNo_Fill();
            invoicefill();
            tblServiceReceived.Visible = false;
        }
    }

    private void invoicefill()
    {
        try
        {
            Services.AmcInvoice.AmcInvoice_Select(ddlInvoiceNo);
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

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomer);
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

    #region Unit Name Fill
    private void UnitName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);
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

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            Services.AMCOrder.AMCOrderForPayments_Select(ddlPONo, ddlCustomer.SelectedItem.Value, ddlUnitName.SelectedItem.Value, btnSave.Text);
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

    #region SalesInvoiceNo Fill
    private void SalesInvoiceNo_Fill()
    {
        try
        {

            Services.AmcInvoice.AmcInvoiceForPayments_Select(ddlInvoiceNo, ddlPONo.SelectedItem.Value, btnSave.Text);
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

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            btnRefresh.Visible = true;
        }
        else if (btnSave.Text == "Update")
        {
            btnRefresh.Visible = false;
        }
    }
    #endregion

    #region LINK Button  PaymentsReceivedNo Click
    protected void lbtnPaymentsReceivedNo_Click(object sender, EventArgs e)
    {
        tblServiceReceived.Visible = true;
        LinkButton lbtnPaymentsReceivedNo;
        lbtnPaymentsReceivedNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPaymentsReceivedNo.Parent.Parent;
        gvPaymentsReceived.SelectedIndex = gvRow.RowIndex;
        Response.Redirect("PaymentDetails.aspx?AMCPR_ID=" + gvPaymentsReceived.SelectedRow.Cells[0].Text);
        //+ "&SoId=" + gvWorkOrderDetails.SelectedRow.Cells[1].Text
        //+ "&DcType=" + gvWorkOrderDetails.SelectedRow.Cells[9].Text
        //+ "&DcFor=" + gvWorkOrderDetails.SelectedRow.Cells[13].Text
        //);

        //Old Code
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            Services.AMCPaymentsReceived objPaymentsReceived = new Services.AMCPaymentsReceived();
            if (objPaymentsReceived.AMCPaymentsReceived_Select(gvPaymentsReceived.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblServiceReceived.Visible = true;
                ddlInvoiceNo.Enabled = false;
                ddlCustomer.Enabled = false;
                ddlUnitName.Enabled = false;

                lblAMCPRIdHidden.Text = objPaymentsReceived.AMCPRId;
                txtAMCPRNo.Text = objPaymentsReceived.AMCPRNo;
                txtAMCPRDate.Text = objPaymentsReceived.AMCPRDate;
                ddlCustomer.SelectedValue = objPaymentsReceived.CustId;
                ddlCustomer_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objPaymentsReceived.UnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                //ddlPONo.SelectedValue = objPaymentsReceived.AMCOId;
                // ddlPONo_SelectedIndexChanged(sender, e);
                ddlInvoiceNo.SelectedValue = objPaymentsReceived.AMCIId;
                ddlInvoiceNo_SelectedIndexChanged(sender, e);
                txtInvoiceAmt.Text = objPaymentsReceived.AMCIAmt;
                txtAmtReceived.Text = objPaymentsReceived.AMCPRReceivedAmt;
                txtType.Text = objPaymentsReceived.AMCServiceType;
                txtAMCORDERNO.Text = objPaymentsReceived.AMCOId;
                txtEquipment.Text = objPaymentsReceived.AMCPREquipmentModel;
                txtSerialNo.Text = objPaymentsReceived.AMCPRSLNo;
                txtPayment.Text = objPaymentsReceived.AMCPRPaymentTerms;
                ddlPaymentMode.SelectedValue = objPaymentsReceived.AMCPRPaymodeType;
                ddlPaymentMode_SelectedIndexChanged(sender, e);
                txtDDChequeNo.Text = objPaymentsReceived.AMCPRChequeNo;
                txtDDChequeDate.Text = objPaymentsReceived.AMCPRChequeDate;
                txtCashReceivedOn.Text = objPaymentsReceived.AMCPRCahReceivedDate;
                txtBankDetails.Text = objPaymentsReceived.AMCPRBankDetails;
                ddlPreparedBy.SelectedValue = objPaymentsReceived.AMCPRPreparedBy;
                ddlApprovedBy.SelectedValue = objPaymentsReceived.AMCPRApprovedBy;

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            Services.Dispose();
            //  ddlDeviveryNo_SelectedIndexChanged(sender, e);
        }

    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentDetails.aspx");
        
        //Old Code
        Services.ClearControls(this);
        DropDownsReset();
        txtAMCPRNo.Text = SM.PaymentsReceived.PaymentsReceived_AutoGenCode();
        txtAMCPRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        gvPaymentsReceived.SelectedIndex = -1;
        ddlInvoiceNo.Enabled = true;
        ddlCustomer.Enabled = true;
        ddlUnitName.Enabled = true;
        tblServiceReceived.Visible = true;
        gvPreviousPayments.DataBind();
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string amt = txtInvoiceAmt.Text;
        foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
        {
            if (btnSave.Text == "Update")
            {
                if (gvrow.Cells[0].Text != lblAMCPRIdHidden.Text)
                {
                    amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                }
            }
            else
            {
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
            }
        }
        amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
        if (Convert.ToDecimal(amt) < 0)
        {
            MessageBox.Show(this, "Amount Received should not exceed more than Balance Amount");
            return;
        }
        else
        {
            if (btnSave.Text == "Save")
            {
                PaymentsReceivedSave();
            }
            else if (btnSave.Text == "Update")
            {
                PaymentsReceivedUpdate();
            }
        }
        DropDownsReset();
    }
    #endregion


    #region DropDownsReset
    private void DropDownsReset()
    {
        //ddlInvoiceNo.Items.Clear();
        ddlPONo.Items.Clear();
        ddlUnitName.Items.Clear();
        ddlPONo.Items.Add(new ListItem("--", "0"));
        ddlPONo.Items.Add(new ListItem("-- Select Unit Name --", "0"));
        // ddlInvoiceNo.Items.Add(new ListItem("--", "0"));
        //ddlInvoiceNo.Items.Add(new ListItem("-- Select PO No. --", "0"));
        // ddlUnitName.Items.Add(new ListItem("--", "0"));
        //ddlUnitName.Items.Add(new ListItem("-- Select Customer. --", "0"));
        //ddlInvoiceNo.Enabled = ddlPONo.Enabled = true;
    }
    #endregion


    #region PaymentsReceived Save
    private void PaymentsReceivedSave()
    {
        try
        {
            Services.AMCPaymentsReceived objsr = new Services.AMCPaymentsReceived();
            //if (objsr.AMCinvoice_isrecordexists(ddlInvoiceNo.SelectedItem.Value) > 0)
            //{
            //    MessageBox.Show(this, "Payment for Invoice " + ddlInvoiceNo.SelectedItem.Text + " already prepared");
            //    Yantra.Classes.General.ClearControls(this);
            //    return;
            //}
            Services.AMCPaymentsReceived objPaymentsReceived = new Services.AMCPaymentsReceived();

            Services.BeginTransaction();

            objPaymentsReceived.AMCPRNo = txtAMCPRNo.Text;
            objPaymentsReceived.AMCPRDate = Yantra.Classes.General.toMMDDYYYY(txtAMCPRDate.Text);
            objPaymentsReceived.CustId = ddlCustomer.SelectedItem.Value;
            objPaymentsReceived.UnitId = ddlUnitName.SelectedItem.Value;
            objPaymentsReceived.AMCOId = txtAMCORDERNO.Text;

            objPaymentsReceived.AMCIId = ddlInvoiceNo.SelectedItem.Value;
            objPaymentsReceived.AMCIAmt = txtInvoiceAmt.Text;
            objPaymentsReceived.AMCPRReceivedAmt = txtAmtReceived.Text;
            objPaymentsReceived.AMCServiceType = txtType.Text;
            objPaymentsReceived.AMCPREquipmentModel = txtEquipment.Text;
            objPaymentsReceived.AMCPRSLNo = txtSerialNo.Text;
            objPaymentsReceived.AMCPRPaymentTerms = txtPayment.Text;
            objPaymentsReceived.AMCPRPaymodeType = ddlPaymentMode.SelectedItem.Value;
            objPaymentsReceived.AMCPRChequeNo = txtDDChequeNo.Text;
            objPaymentsReceived.AMCPRChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objPaymentsReceived.AMCPRCahReceivedDate = Yantra.Classes.General.toMMDDYYYY(txtCashReceivedOn.Text);
            objPaymentsReceived.AMCPRBankDetails = txtBankDetails.Text;
            objPaymentsReceived.AMCPRPreparedBy = ddlPreparedBy.SelectedItem.Value;
            objPaymentsReceived.AMCPRApprovedBy = ddlApprovedBy.SelectedItem.Value;
            //objPaymentsReceived.AMCPRPaymentStatus = "";

            if (objPaymentsReceived.AMCPaymentsReceived_Save() == "Data Saved Successfully")
            {
                string amt = txtInvoiceAmt.Text;
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                }
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
                if (Convert.ToDecimal(amt) > 0)
                {
                    objPaymentsReceived.AMCPaymentsStatus_Update("Pending", objPaymentsReceived.AMCIId);
                }
                else
                {
                    objPaymentsReceived.AMCPaymentsStatus_Update("Cleared", objPaymentsReceived.AMCIId);
                }

                if (Convert.ToDecimal(amt) <= 100)
                {
                    objPaymentsReceived.AMCPaymentsStatus_Update("Cleared", objPaymentsReceived.AMCIId);
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
            gvPaymentsReceived.DataBind();
            tblServiceReceived.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }

    }
    #endregion

    #region PaymentsReceived Update
    private void PaymentsReceivedUpdate()
    {

        try
        {
            Services.AMCPaymentsReceived objPaymentsReceived = new Services.AMCPaymentsReceived();

            Services.BeginTransaction();

            objPaymentsReceived.AMCPRId = gvPaymentsReceived.SelectedRow.Cells[0].Text;

            objPaymentsReceived.AMCPRNo = txtAMCPRNo.Text;
            objPaymentsReceived.AMCPRDate = Yantra.Classes.General.toMMDDYYYY(txtAMCPRDate.Text);
            objPaymentsReceived.CustId = ddlCustomer.SelectedItem.Value;
            objPaymentsReceived.UnitId = ddlUnitName.SelectedItem.Value;
            objPaymentsReceived.AMCOId = txtAMCORDERNO.Text;
            objPaymentsReceived.AMCIId = ddlInvoiceNo.SelectedItem.Value;
            objPaymentsReceived.AMCIAmt = txtInvoiceAmt.Text;
            objPaymentsReceived.AMCPRReceivedAmt = txtAmtReceived.Text;
            objPaymentsReceived.AMCServiceType = txtType.Text;
            objPaymentsReceived.AMCPREquipmentModel = txtEquipment.Text;
            objPaymentsReceived.AMCPRSLNo = txtSerialNo.Text;
            objPaymentsReceived.AMCPRPaymentTerms = txtPayment.Text;
            objPaymentsReceived.AMCPRPaymodeType = ddlPaymentMode.SelectedItem.Value;
            objPaymentsReceived.AMCPRChequeNo = txtDDChequeNo.Text;
            objPaymentsReceived.AMCPRChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objPaymentsReceived.AMCPRCahReceivedDate = Yantra.Classes.General.toMMDDYYYY(txtCashReceivedOn.Text);
            objPaymentsReceived.AMCPRBankDetails = txtBankDetails.Text;
            objPaymentsReceived.AMCPRPreparedBy = ddlPreparedBy.SelectedItem.Value;
            objPaymentsReceived.AMCPRApprovedBy = ddlApprovedBy.SelectedItem.Value;
            //objPaymentsReceived.AMCPRPaymentStatus = "";

            if (objPaymentsReceived.AMCPaymentsReceived_Update() == "Data Updated Successfully")
            {
                string amt = txtInvoiceAmt.Text;
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    if (gvrow.Cells[0].Text != lblAMCPRIdHidden.Text)
                    {
                        amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                    }
                }
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
                if (Convert.ToDecimal(amt) > 0)
                {
                    objPaymentsReceived.AMCPaymentsStatus_Update("Pending", objPaymentsReceived.AMCIId);
                }
                else
                {
                    objPaymentsReceived.AMCPaymentsStatus_Update("Cleared", objPaymentsReceived.AMCIId);
                }
                if (Convert.ToDecimal(amt) <= 100)
                {
                    objPaymentsReceived.AMCPaymentsStatus_Update("Cleared", objPaymentsReceived.AMCIId);
                }
                Services.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
            }
            else
            {
                Services.RollBackTransaction();
            }

            Services.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvPaymentsReceived.DataBind();
            tblServiceReceived.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblServiceReceived.Visible = true;
        LinkButton lbtnPaymentsReceivedNo;
        lbtnPaymentsReceivedNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPaymentsReceivedNo.Parent.Parent;
        gvPaymentsReceived.SelectedIndex = gvRow.RowIndex;
        Response.Redirect("PaymentDetails.aspx?AMCPR_ID=" + gvPaymentsReceived.SelectedRow.Cells[0].Text);
        //+ "&SoId=" + gvWorkOrderDetails.SelectedRow.Cells[1].Text
        //+ "&DcType=" + gvWorkOrderDetails.SelectedRow.Cells[9].Text
        //+ "&DcFor=" + gvWorkOrderDetails.SelectedRow.Cells[13].Text
        //);


        //Old Code
        if (gvPaymentsReceived.SelectedIndex > -1)
        {
            Response.Redirect("PaymentsNew.aspx?amcprNo=" + gvPaymentsReceived.SelectedRow.Cells[0].Text);
            //try
            //{
            //    Services.AMCPaymentsReceived objPaymentsReceived = new Services.AMCPaymentsReceived();

            //    if (objPaymentsReceived.AMCPaymentsReceived_Select(gvPaymentsReceived.SelectedRow.Cells[0].Text) > 0)
            //    {
            //        btnSave.Text = "Update";
            //        btnSave.Enabled = true;

            //        tblServiceReceived.Visible = true;
            //        ddlInvoiceNo.Enabled = false;
            //        ddlCustomer.Enabled = false;
            //        ddlUnitName.Enabled = false;

            //        lblAMCPRIdHidden.Text = objPaymentsReceived.AMCPRId;
            //        txtAMCPRNo.Text = objPaymentsReceived.AMCPRNo;
            //        txtAMCPRDate.Text = objPaymentsReceived.AMCPRDate;
            //        ddlCustomer.SelectedValue = objPaymentsReceived.CustId;
            //        ddlCustomer_SelectedIndexChanged(sender, e);
            //        ddlUnitName.SelectedValue = objPaymentsReceived.UnitId;
            //        ddlUnitName_SelectedIndexChanged(sender, e);

            //        // ddlPONo.SelectedValue = objPaymentsReceived.AMCOId;
            //        //  ddlPONo_SelectedIndexChanged(sender, e);
            //        ddlInvoiceNo.SelectedValue = objPaymentsReceived.AMCIId;
            //        ddlInvoiceNo_SelectedIndexChanged(sender, e);
            //        txtInvoiceAmt.Text = objPaymentsReceived.AMCIAmt;
            //        txtAmtReceived.Text = objPaymentsReceived.AMCPRReceivedAmt;
            //        txtType.Text = objPaymentsReceived.AMCServiceType;
            //        txtAMCORDERNO.Text = objPaymentsReceived.AMCOId;
            //        txtEquipment.Text = objPaymentsReceived.AMCPREquipmentModel;
            //        txtSerialNo.Text = objPaymentsReceived.AMCPRSLNo;
            //        txtPayment.Text = objPaymentsReceived.AMCPRPaymentTerms;
            //        ddlPaymentMode.SelectedValue = objPaymentsReceived.AMCPRPaymodeType;
            //        ddlPaymentMode_SelectedIndexChanged(sender, e);
            //        txtDDChequeNo.Text = objPaymentsReceived.AMCPRChequeNo;
            //        txtDDChequeDate.Text = objPaymentsReceived.AMCPRChequeDate;
            //        txtCashReceivedOn.Text = objPaymentsReceived.AMCPRCahReceivedDate;
            //        txtBankDetails.Text = objPaymentsReceived.AMCPRBankDetails;
            //        ddlPreparedBy.SelectedValue = objPaymentsReceived.AMCPRPreparedBy;
            //        ddlApprovedBy.SelectedValue = objPaymentsReceived.AMCPRApprovedBy;

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message.ToString());
            //}
            //finally
            //{

            //    Services.Dispose();
            //}
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvPaymentsReceived.SelectedIndex > -1)
        {
            try
            {
                Services.AMCPaymentsReceived objPaymentsReceived = new Services.AMCPaymentsReceived();

                MessageBox.Show(this, objPaymentsReceived.AMCPaymentsReceived_Delete(gvPaymentsReceived.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvPaymentsReceived.DataBind();
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

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblServiceReceived.Visible = false;
    }
    #endregion

    #region ddlPaymentMode_SelectedIndexChanged
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedItem.Text == "Cheque")
        {
            txtDDChequeNo.Visible = true;
            txtDDChequeDate.Visible = true;
            imgDDChequeDate.Visible = true;
            lblDDChequeNo.Visible = true;
            lblDDChequeDate.Visible = true;
            lblDDChequeNo.Text = "Cheque No.";
            lblDDChequeDate.Text = "Cheque Date";
            lblBankDetails.Visible = true;
            txtBankDetails.Visible = true;

            txtCashReceivedOn.Visible = false;
            imgCashReceivedOn.Visible = false;
            lblCashReceivedOn.Visible = false;
        }
        else if (ddlPaymentMode.SelectedItem.Text == "DD")
        {
            txtDDChequeNo.Visible = true;
            txtDDChequeDate.Visible = true;
            imgDDChequeDate.Visible = true;
            lblDDChequeNo.Visible = true;
            lblDDChequeDate.Visible = true;
            lblDDChequeNo.Text = "DD No.";
            lblDDChequeDate.Text = "DD Date";
            lblBankDetails.Visible = true;
            txtBankDetails.Visible = true;

            txtCashReceivedOn.Visible = false;
            imgCashReceivedOn.Visible = false;
            lblCashReceivedOn.Visible = false;
        }
        else if (ddlPaymentMode.SelectedItem.Text == "Cash")
        {
            txtCashReceivedOn.Visible = true;
            imgCashReceivedOn.Visible = true;
            lblCashReceivedOn.Visible = true;

            txtDDChequeNo.Visible = false;
            txtDDChequeDate.Visible = false;
            imgDDChequeDate.Visible = false;
            lblDDChequeNo.Visible = false;
            lblDDChequeDate.Visible = false;
            lblBankDetails.Visible = false;
            txtBankDetails.Visible = false;

        }
        else
        {
            txtDDChequeNo.Visible = false;
            txtDDChequeDate.Visible = false;
            imgDDChequeDate.Visible = false;
            lblDDChequeNo.Visible = false;
            lblDDChequeDate.Visible = false;
            lblBankDetails.Visible = false;
            txtBankDetails.Visible = false;


            txtCashReceivedOn.Visible = false;
            imgCashReceivedOn.Visible = false;
            lblCashReceivedOn.Visible = false;

        }

    }
    #endregion

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        UnitName_Fill();
    }
    #endregion

    #region ddlPONo_SelectedIndexChanged
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Services.AMCOrder objOrder = new Services.AMCOrder();
            if (objOrder.AMCOrder_Select(ddlPONo.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objOrder.AMCODate;
                SalesInvoiceNo_Fill();
                gvPreviousPayments.DataBind();
            }
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

    #region ddlInvoiceNo_SelectedIndexChanged
    protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Inventory.SalesInvoice objSalesInvoice = new Inventory.SalesInvoice();


            Services.AmcInvoice objInvoice = new Services.AmcInvoice();
            if (objInvoice.AmcInvoice_Select(ddlInvoiceNo.SelectedItem.Value) > 0)
            {
                txtInvoiceDate.Text = objInvoice.AMCIDate;
                txtInvoiceAmt.Text = objInvoice.AMCIGrossAmt;
                txtBalanceAmount.Text = txtInvoiceAmt.Text;
                //txtType.Text = objInvoice.AMCIType;
                txtPayment.Text = objInvoice.amcpayterms;
                if (objInvoice.AmcInvoice_Select1(objInvoice.AMCIId, objInvoice.AMCOId) > 0)
                {
                    ddlCustomer.SelectedValue = objInvoice.CustId;
                    UnitName_Fill();

                    ddlUnitName.SelectedValue = objInvoice.CustUnitId;
                    SM.CustomerMaster objcust = new SM.CustomerMaster();
                    objcust.CustomerUnits_Select(objInvoice.CustUnitId);
                    txtAMCORDERNO.Text = objInvoice.AMCOId;

                    txtUnitAddress.Text = objcust.CustUnitAddress;
                    txtPONO.Text = objInvoice.AMCOCustPONo;
                    txtPODate.Text = objInvoice.AMCOCustPODate;

                    Services.AMCPaymentsReceived objPaymentsReceived = new Services.AMCPaymentsReceived();
                    objPaymentsReceived.ExistingAMCPaymentsReceived_Select(gvPreviousPayments, ddlInvoiceNo.SelectedItem.Value);
                }
                else
                {
                    MessageBox.Show(this, "Amc Invoice " + ddlInvoiceNo.SelectedItem.Text + " has been deleted so payment cannot be prepared");
                    Yantra.Classes.General.ClearControls(this);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Services.Dispose();
            if (txtAMCPRNo.Text == String.Empty)
            {
                txtAMCPRNo.Text = SM.PaymentsReceived.PaymentsReceived_AutoGenCode();
            }

        }
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objCustUnits = new SM.CustomerMaster();
            if (objCustUnits.CustomerMasterUnitsDetailsEnquiry_Select(ddlUnitName.SelectedItem.Value) > 0)
            {
                txtUnitAddress.Text = objCustUnits.Address;
                SalesOrder_Fill();
            }
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

    #region gvPaymentsReceived_RowDataBound
    protected void gvPaymentsReceived_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "AMCPR Date")
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
    protected void ddlSymbols_SelectedIndexChanged1(object sender, EventArgs e)
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
        gvPaymentsReceived.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvPaymentsReceived.DataBind();
    }
    #endregion

    #region gvPreviousPayments_RowDataBound
    protected void gvPreviousPayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(e.Row.Cells[5].Text));
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[5].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[4].Text = "Total Amount:";
            //e.Row.Cells[5].Text = TotalAmount.ToString();
        }
    }
    #endregion




    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            gvPaymentsReceived.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvPaymentsReceived.DataBind();
    }
}

 
