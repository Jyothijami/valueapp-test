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



public partial class Modules_SM_PaymentsReceived : basePage
{
    decimal TotalAmount=0;
    decimal TotalAmount1 = 0;
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            CustomerMaster_Fill();
            SalesOrder_Fill();
            EmployeeMaster_Fill();
            tblPaymentsReceived.Visible = false;
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "73");
        btnNew.Enabled = up.add;
        //btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnSave.Enabled = up.Save;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;

    }


    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomer);
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
            //SM.SalesOrder.SalesOrderForPayments_Select(ddlPONo, ddlCustomer.SelectedItem.Value, ddlUnitName.SelectedItem.Value, btnSave.Text);
            SM.SalesOrder.SalesOrder_Select(ddlPONo);
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
    
    #region Spares Order Fill
    private void SparesOrder_Fill()
    {
        try
        {
            Services.SparesOrder.SparesOrderForPayments_Select(ddlPONo, ddlCustomer.SelectedItem.Value, ddlUnitName.SelectedItem.Value, btnSave.Text);
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
            rbSpares.Enabled = rbSales.Enabled = true;
            btnRefresh.Visible = true;
        }
        else if (btnSave.Text == "Update")
        {
            rbSpares.Enabled = rbSales.Enabled = false;
            btnRefresh.Visible = false;
        }
    }
    #endregion

    #region LINK Button  PaymentsReceivedNo Click
    protected void lbtnPaymentsReceivedNo_Click(object sender, EventArgs e)
    {
        //SM.ClearControls(this);
        //gvPreviousPayments.DataBind();     
        tblPaymentsReceived.Visible = false;
        LinkButton lbtnPaymentsReceivedNo;
        lbtnPaymentsReceivedNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPaymentsReceivedNo.Parent.Parent;
        gvPaymentsReceived.SelectedIndex = gvRow.RowIndex;

        Response.Redirect("PaymentsReceivedNew.aspx?Prid=" + gvPaymentsReceived.SelectedRow.Cells[0].Text
            + "&PrStatus=" + gvPaymentsReceived.SelectedRow.Cells[5].Text);


        //Old Code
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {

            SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
            if (objPaymentsReceived.PaymentsReceived_Select(gvPaymentsReceived.SelectedRow.Cells[0].Text) > 0)
            {
                
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblPaymentsReceived.Visible = true;
              
                lblPRIdHidden.Text = objPaymentsReceived.PRId;
                txtPRNo.Text = objPaymentsReceived.PRNo;
                txtPRDate.Text = objPaymentsReceived.PRDate;
                ddlCustomer.SelectedValue = objPaymentsReceived.CustId;
                ddlCustomer_SelectedIndexChanged(sender, e);
                //ddlUnitName.SelectedValue = objPaymentsReceived.UnitId;
                //ddlUnitName_SelectedIndexChanged(sender, e);
                if (objPaymentsReceived.SPOId == "0")
                {
                    rbSales.Checked = true;
                    rbSpares.Checked = false;
                    SalesOrder_Fill();
                    ddlPONo.SelectedValue = objPaymentsReceived.SO_Id;
                }
                else if (objPaymentsReceived.SO_Id == "0")
                {
                    rbSpares.Checked = true;
                    rbSales.Checked = false;
                    SparesOrder_Fill();
                    ddlPONo.SelectedValue = objPaymentsReceived.SPOId;
                }
                //SM.SalesOrder objSM = new SM.SalesOrder();
                //objSM.SalesOrderDetails_Select(ddlPONo.SelectedItem.Value, gvItemDetails);
                ddlPONo_SelectedIndexChanged(sender, e);
                //ddlInvoiceNo.SelectedValue = objPaymentsReceived.SIId;
                //ddlInvoiceNo_SelectedIndexChanged(sender, e);
                 txtTotalAmount.Text = objPaymentsReceived.Totalamt;
                // txtAmtReceived.Text = objPaymentsReceived.PRReceivedAmt;
                 txtAmtReceived.Text = string.Empty;

                ddlPaymentMode.SelectedValue = objPaymentsReceived.PRPaymodeType;
                ddlPaymentMode_SelectedIndexChanged(sender, e);
                txtDDChequeNo.Text = objPaymentsReceived.PRChequeNo;
                txtDDChequeDate.Text = objPaymentsReceived.PRChequeDate;
                txtCashReceivedOn.Text = objPaymentsReceived.PRCahReceivedDate;
                txtBankDetails.Text = objPaymentsReceived.PRBankDetails;
                ddlPreparedBy.SelectedValue = objPaymentsReceived.PRPreparedBy;
                ddlApprovedBy.SelectedValue = objPaymentsReceived.PRApprovedBy;

            }

            txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtTotalPaid.Text));

            if (gvPaymentsReceived.SelectedRow.Cells[5].Text == "Cleared")
            {
                txtBalanceAmount.Text = "0";
            }
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //gvPreviousPayments.SelectedIndex = -1;
            //gvPaymentsReceived.SelectedIndex = -1;
            SM.Dispose();
          //   ddlDeviveryNo_SelectedIndexChanged(sender, e);
        }

    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentsReceivedNew.aspx");
        //Old Code
        SM.ClearControls(this);
        DropDownsReset();
        txtPRNo.Text = SM.PaymentsReceived.PaymentsReceived_AutoGenCode();
        txtPRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblPaymentsReceived.Visible = true;
        gvPreviousPayments.DataBind();
        rbSales.Checked = false;
        rbSpares.Checked = false;
    }
    #endregion

    #region Button SAVE/UPDATE Click
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtAmtReceived.Text == string.Empty)
        {
            txtAmtReceived.Text = "0";
        }
     
        string amt = txtTotalAmount.Text;
        foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
        {
            if (btnSave.Text == "Update")
            {
                if (gvrow.Cells[0].Text != lblPRIdHidden.Text)
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
                PaymentsReceivedSave();
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
        //ddlInvoiceNo.Items.Add(new ListItem("--", "0"));
        //ddlInvoiceNo.Items.Add(new ListItem("-- Select PO No. --", "0"));
        ddlUnitName.Items.Add(new ListItem("--", "0"));
        ddlUnitName.Items.Add(new ListItem("-- Select Customer. --", "0"));
        //ddlInvoiceNo.Enabled = ddlPONo.Enabled = true;
    }
    #endregion

    #region PaymentsReceived Save
    private void PaymentsReceivedSave()
    {
        try
        {
            SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
            SM.BeginTransaction();

            objPaymentsReceived.PRNo = txtPRNo.Text;
            objPaymentsReceived.PRDate = Yantra.Classes.General.toMMDDYYYY(txtPRDate.Text);
            objPaymentsReceived.CustId = ddlCustomer.SelectedItem.Value;
            objPaymentsReceived.UnitId = ddlUnitName.SelectedItem.Value;
            if (rbSales.Checked)
            {
                objPaymentsReceived.SO_Id = ddlPONo.SelectedItem.Value;
                objPaymentsReceived.SPOId = "0";
            }
            else if (rbSpares.Checked)
            {
                objPaymentsReceived.SPOId = ddlPONo.SelectedItem.Value;
                objPaymentsReceived.SO_Id = "0";
            }
            objPaymentsReceived.SIId = "0";
            objPaymentsReceived.SIAmt = txtTotalAmount.Text;
            objPaymentsReceived.PRReceivedAmt = txtAmtReceived.Text;
            objPaymentsReceived.PRPaymodeType = ddlPaymentMode.SelectedItem.Value;
            objPaymentsReceived.PRChequeNo = txtDDChequeNo.Text;
            objPaymentsReceived.PRChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objPaymentsReceived.PRCahReceivedDate = Yantra.Classes.General.toMMDDYYYY(txtCashReceivedOn.Text);
            objPaymentsReceived.PRBankDetails = txtBankDetails.Text;
            objPaymentsReceived.PRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objPaymentsReceived.PRApprovedBy = ddlApprovedBy.SelectedItem.Value;

            if (objPaymentsReceived.PaymentsReceived_Save() == "Data Saved Successfully")
            {

                string amt = txtTotalAmount.Text;
              
               // amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAdvanceAmount.Text));

                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                }
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
               
                if ( Convert.ToDecimal(amt) > 0 )
                {
                    objPaymentsReceived.PaymentsStatus_Update("Pending", objPaymentsReceived.SO_Id);
                }
                else 
                {
                    objPaymentsReceived.PaymentsStatus_Update("Cleared", objPaymentsReceived.SO_Id);
                }
                if (Convert.ToDecimal(amt) <= 100)
                {
                    objPaymentsReceived.PaymentsStatus_Update("Cleared", objPaymentsReceived.SO_Id);
                }

                //if (Convert.ToDecimal(hi) <= (Convert.ToDecimal(amt) - Convert.ToDecimal(bal)))
                //{
                //    objPaymentsReceived.PaymentsStatus_Update("Cleared", objPaymentsReceived.SO_Id);
                //}


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
            gvPaymentsReceived.DataBind();
            tblPaymentsReceived.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }

    }
    #endregion

    #region PaymentsReceived Update
    private void PaymentsReceivedUpdate()
    {
       // txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(txtAmtReceived.Text));


        try
        {
            SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
            SM.BeginTransaction();

            objPaymentsReceived.PRId = gvPaymentsReceived.SelectedRow.Cells[0].Text;

            objPaymentsReceived.PRNo = txtPRNo.Text;
            objPaymentsReceived.PRDate = Yantra.Classes.General.toMMDDYYYY(txtPRDate.Text);
            objPaymentsReceived.CustId = ddlCustomer.SelectedItem.Value;
            objPaymentsReceived.UnitId = ddlUnitName.SelectedItem.Value;
            if (rbSales.Checked)
            {
                objPaymentsReceived.SO_Id = ddlPONo.SelectedItem.Value;
                objPaymentsReceived.SPOId = "0";
            }
            else if (rbSpares.Checked)
            {
                objPaymentsReceived.SPOId = ddlPONo.SelectedItem.Value;
                objPaymentsReceived.SO_Id = "0";
            }
            objPaymentsReceived.SIId = "0";
            objPaymentsReceived.SIAmt = txtTotalAmount.Text;
            objPaymentsReceived.PRReceivedAmt = txtAmtReceived.Text;
            objPaymentsReceived.PRPaymodeType = ddlPaymentMode.SelectedItem.Value;
            objPaymentsReceived.PRChequeNo = txtDDChequeNo.Text;
            objPaymentsReceived.PRChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDDChequeDate.Text);
            objPaymentsReceived.PRCahReceivedDate = Yantra.Classes.General.toMMDDYYYY(txtCashReceivedOn.Text);
            objPaymentsReceived.PRBankDetails = txtBankDetails.Text;
            objPaymentsReceived.PRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objPaymentsReceived.PRApprovedBy = ddlApprovedBy.SelectedItem.Value;

            if (objPaymentsReceived.PaymentsReceived_Update() == "Data Updated Successfully")
            {
                string amt = txtBalanceAmount.Text;
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    if (gvrow.Cells[0].Text != lblPRIdHidden.Text)
                    {
                        amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(gvrow.Cells[5].Text));
                    }
                }
                amt = Convert.ToString(Convert.ToDecimal(amt) - Convert.ToDecimal(txtAmtReceived.Text));
                if (Convert.ToDecimal(amt) > 0)
                {
                    objPaymentsReceived.PaymentsStatus_Update("Pending", objPaymentsReceived.SO_Id);
                }
                else
                {
                    objPaymentsReceived.PaymentsStatus_Update("Cleared", objPaymentsReceived.SO_Id);
                }
                SM.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
            }
            else
            {
                SM.RollBackTransaction();
            }

            SM.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvPaymentsReceived.DataBind();
            tblPaymentsReceived.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
       
        //tblPaymentsReceived.Visible = true;

        if (gvPaymentsReceived.SelectedIndex > -1)
        {
            Response.Redirect("PaymentsReceivedNew.aspx?Prid=" + gvPaymentsReceived.SelectedRow.Cells[0].Text);
            try
            {
                SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();

                if (objPaymentsReceived.PaymentsReceived_Select(gvPaymentsReceived.SelectedRow.Cells[0].Text) > 0)
                {

                    btnSave.Text = "Update";
                    btnSave.Enabled = false;
                    tblPaymentsReceived.Visible = true;

                    lblPRIdHidden.Text = objPaymentsReceived.PRId;
                    txtPRNo.Text = objPaymentsReceived.PRNo;
                    txtPRDate.Text = objPaymentsReceived.PRDate;
                    ddlCustomer.SelectedValue = objPaymentsReceived.CustId;
                    ddlCustomer_SelectedIndexChanged(sender, e);
                    //ddlUnitName.SelectedValue = objPaymentsReceived.UnitId;
                    //ddlUnitName_SelectedIndexChanged(sender, e);
                    ddlPONo.SelectedValue = objPaymentsReceived.SO_Id;
                    ddlPONo_SelectedIndexChanged(sender, e);
                    //ddlInvoiceNo.SelectedValue = objPaymentsReceived.SIId;
                    //ddlInvoiceNo_SelectedIndexChanged(sender, e);
                    txtTotalAmount.Text = objPaymentsReceived.Totalamt;
                    // txtAmtReceived.Text = objPaymentsReceived.PRReceivedAmt;
                    txtAmtReceived.Text = string.Empty;
                    ddlPaymentMode.SelectedValue = objPaymentsReceived.PRPaymodeType;
                    ddlPaymentMode_SelectedIndexChanged(sender, e);
                    txtDDChequeNo.Text = objPaymentsReceived.PRChequeNo;
                    txtDDChequeDate.Text = objPaymentsReceived.PRChequeDate;
                    txtCashReceivedOn.Text = objPaymentsReceived.PRCahReceivedDate;
                    txtBankDetails.Text = objPaymentsReceived.PRBankDetails;
                    ddlPreparedBy.SelectedValue = objPaymentsReceived.PRPreparedBy;
                    ddlApprovedBy.SelectedValue = objPaymentsReceived.PRApprovedBy;
                    //txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtBalanceAmount.Text) - Convert.ToDecimal(txtAmtReceived.Text));


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
        else
        {
            MessageBox.Show(this, "Please select  a Record");
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
                SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();

                MessageBox.Show(this, objPaymentsReceived.PaymentsReceived_Delete(gvPaymentsReceived.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                tblPaymentsReceived.Visible = false;
            }
            finally
            {

                gvPaymentsReceived.DataBind();
                gvPreviousPayments.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select  a Record");
        }
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblPaymentsReceived.Visible = false;
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
        SM.CustomerMaster objcustomer = new SM.CustomerMaster();
        if (objcustomer.unit_Select(ddlCustomer.SelectedValue) > 0)
        {

            ddlUnitName.SelectedItem.Value = objcustomer.CustUnitId;
            ddlUnitName.SelectedItem.Text = objcustomer.CustUnitName;
            txtUnitAddress.Text = objcustomer.CustUnitAddress;
        }
        else
        {
            ddlUnitName.SelectedItem.Text = "--";
            txtUnitAddress.Text = string.Empty;
        }
    }
    #endregion

    #region ddlPONo_SelectedIndexChanged
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlUnitName.SelectedItem.Text = "---";
        //txtUnitAddress.Text = "---";
        if (rbSales.Checked)
        {
            try
            {
                SM.SalesOrder objSalesOrder = new SM.SalesOrder();
                if (objSalesOrder.SalesOrder_Select(ddlPONo.SelectedItem.Value) > 0)
                {
                    txtAdvanceAmount.Text = objSalesOrder.SOAdvanceAmt;
                    txtPODate.Text = objSalesOrder.SODate;
                   
                    ddlCustomer.SelectedValue = objSalesOrder.CustId;
                    ddlCustomer_SelectedIndexChanged(sender, e);
                    txtTotalPaid.Text = string.Empty;
                    txtTotalAmount.Text = objSalesOrder.Sototalamt;
                
                    SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
                    objPaymentsReceived.ExistingPaymentsReceived_Select(gvPreviousPayments, ddlPONo.SelectedItem.Value);

                
                }
                //SM.SalesOrder objSM = new SM.SalesOrder();
                //objSM.SalesOrderDetails_Select(ddlPONo.SelectedItem.Value, gvItemDetails);
             
                txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) );
               
                if (txtBalanceAmount.Text == "")
                {
                    txtBalanceAmount.Text = "0";
                }
                if (txtTotalPaid.Text != "")
                {
                    txtBalanceAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(txtTotalPaid.Text));
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                SM.Dispose();
               // gvPreviousPayments.DataBind();
            }
        }
        else if (rbSpares.Checked)
        {
            try
            {
                Services.SparesOrder objSalesOrder = new Services.SparesOrder();
                if (objSalesOrder.SparesOrder_Select(ddlPONo.SelectedItem.Value) > 0)
                {
                    txtPODate.Text = objSalesOrder.SPODate;
                    //SalesInvoiceNo_Fill();
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
    }
    #endregion

    //#region ddlInvoiceNo_SelectedIndexChanged
    //protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Inventory.SalesInvoice objSalesInvoice = new Inventory.SalesInvoice();

    //        if (objSalesInvoice.SalesInvoice_Select(ddlInvoiceNo.SelectedItem.Value) > 0)
    //        {
    //            txtInvoiceDate.Text = objSalesInvoice.SIDate;
    //            txtInvoiceAmt.Text = objSalesInvoice.SIGrossAmt;
               
    //            txtInvoiceBlance.Text =Convert.ToString(Convert.ToDecimal (txtInvoiceAmt.Text) - Convert.ToDecimal(txtAdvanceAmount.Text));

    //            if (txtBalanceAmount.Text == "")
    //            {
    //                txtBalanceAmount.Text = "0";
    //            }
    //            txtBalanceAmount.Text = txtInvoiceBlance.Text;

    //            SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
    //            objPaymentsReceived.ExistingPaymentsReceived_Select(gvPreviousPayments, ddlInvoiceNo.SelectedItem.Value);
    //        }
           
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        SM.Dispose();
    //        Inventory.Dispose();
    //    }
    //}
    //#endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objCustUnits = new SM.CustomerMaster();
            if (objCustUnits.CustomerUnits_Select(ddlUnitName.SelectedItem.Value) > 0)
            {
                txtUnitAddress.Text = objCustUnits.CustUnitAddress;
                if (rbSales.Checked)
                {
                   // SalesOrder_Fill();
                }
                else if (rbSpares.Checked)
                {
                  //  SparesOrder_Fill();
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
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Receipt  Date")
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
        gvPaymentsReceived.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy.SelectedItem.Text == "Receipt  Date")
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
        gvPaymentsReceived.DataBind();
    }
    #endregion

    #region gvPreviousPayments_RowDataBound
    protected void gvPreviousPayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[5].Text);
            //txtTotalPaid.Text =Convert.ToString(Convert.ToDecimal(txtAdvanceAmount.Text) + TotalAmount);
            txtTotalPaid.Text = Convert.ToString(TotalAmount);
          
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Paid Amount:";
            e.Row.Cells[5].Text = TotalAmount.ToString();
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
    }
    #endregion

    #region rbSales_CheckedChanged
    protected void rbSales_CheckedChanged(object sender, EventArgs e)
    {
        lblPONo.Text = "Purchase Order No.";
        lblPODate.Text = "P O Date";
        SalesOrder_Fill();
    }
    #endregion

    #region rbSpares_CheckedChanged
    protected void rbSpares_CheckedChanged(object sender, EventArgs e)
    {
        lblPONo.Text = "Spares Order No";
        lblPODate.Text = "Spares Order Date";
        SparesOrder_Fill();
    }
    #endregion

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPaymentsReceived.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvPaymentsReceived.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvPaymentsReceived.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
