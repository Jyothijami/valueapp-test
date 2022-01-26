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

public partial class Modules_SM_StatementOfSecurityDepositReceipt_ : System.Web.UI.Page
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerMaster_Fill();

            EmployeeMaster_Fill();
            TenderNo_Fill();
            salesOrder_Fill();
            SdbgNo_Fill();

            tblReceiptsDetails.Visible = false;

        }

    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustName);




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

    
    #region TenderNo Fill
    private void TenderNo_Fill()
    {
        try
        {

            SM.SalesEnquiry.SalesEnquiryTenderNo_Select(ddlTenderNo);


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


    #region  salesOrder_Fill
    private void salesOrder_Fill()
    {
        try
        {

            SM.SalesOrder.SalesOrder_Select(ddlSOPNo);


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

    #region    SdbgNo_Fill()
    private void SdbgNo_Fill()
    {
        try
        {
            SM.SDBG.SDBG_Select(ddlSDNo);

            //SM.SDBGReceipts objrec = new SM.SDBGReceipts();
            //objrec.SDBGDetId = SM.SDBG.SDBGDetails_AutoGenCode();

            
           



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


    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);

        txtReceipts.Text = SM.SDBGReceipts.SDBGReceipts_AutoGenCode();

        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblReceiptsDetails.Visible = true;
       


    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SDBGReceiptsSave();
        }
        else if (btnSave.Text == "Update")
        {
            SDBGReceiptsUpdate();
        }
    }
    #endregion

    #region SDBGReceiptsSave
    private void SDBGReceiptsSave()
    {
        try
        {

            SM.SDBGReceipts objrec = new SM.SDBGReceipts();
            
           
            SM.BeginTransaction();
            objrec.SDBGReceiptsNo = txtReceipts.Text;

            objrec.SDBGReceiptsDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);

            objrec.CustId = ddlCustName.SelectedItem.Value;
            objrec.SDBGReceiptsStatementOf = ddlStatementOf.SelectedItem.Value;
            objrec.SDBGDetId = ddlSDNo.SelectedItem.Value;
            objrec.SDBGReceiptsPayMode = ddlPaymentMode.SelectedItem.Value;
            objrec.SDBGReceiptsDueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objrec.SDBGReceiptsDdChequeNo = txtChequeNo.Text;
            objrec.SDBGReceiptsDDChequeDate = Yantra.Classes.General.toMMDDYYYY(txtChequeDate.Text);
            objrec.SDBGReceiptsBankDetails = txtDetailsOfBank.Text;
            objrec.SDBGReceiptsRemarks = txtRemarks.Text;
            objrec.SDBGReceipts_Save();
         
            SM.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvDepositReceiptsDetails.DataBind();
            tblReceiptsDetails.Visible = false;
            
            SM.ClearControls(this);
            SM.Dispose();
        }

    }
    #endregion


    #region   SDBGReceiptsUpdate
    private void SDBGReceiptsUpdate()
    {

        try
        {
            SM.SDBGReceipts objrec = new SM.SDBGReceipts();


            SM.BeginTransaction();
            objrec.SDBGReceiptsId = gvDepositReceiptsDetails.SelectedRow.Cells[0].Text;



            objrec.SDBGReceiptsNo = txtReceipts.Text;

            objrec.SDBGReceiptsDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);

            objrec.CustId = ddlCustName.SelectedItem.Value;
            objrec.SDBGReceiptsStatementOf = ddlStatementOf.SelectedItem.Value;
            objrec.SDBGDetId = ddlSDNo.SelectedItem.Value;
            objrec.SDBGReceiptsPayMode = ddlPaymentMode.SelectedItem.Value;
            objrec.SDBGReceiptsDueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objrec.SDBGReceiptsDdChequeNo = txtChequeNo.Text;
            objrec.SDBGReceiptsDDChequeDate = Yantra.Classes.General.toMMDDYYYY(txtChequeDate.Text);
            objrec.SDBGReceiptsBankDetails = txtDetailsOfBank.Text;
            objrec.SDBGReceiptsRemarks = txtRemarks.Text;
            objrec.SDBGReceipts_Update();

          

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
            gvDepositReceiptsDetails.DataBind();
            tblReceiptsDetails.Visible = false;

            SM.ClearControls(this);
            SM.Dispose();
        }


    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblReceiptsDetails.Visible = true;

        if (gvDepositReceiptsDetails.SelectedIndex > -1)
        {
            try
            {

                SM.SDBGReceipts objrec = new SM.SDBGReceipts();
                if (objrec.SDBGReceipts_Select(gvDepositReceiptsDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    //btnSave.Enabled = false;
                    tblReceiptsDetails.Visible = true;
                    txtReceipts.Text = objrec.SDBGReceiptsNo;

                    txtDate.Text = objrec.SDBGReceiptsDate;

                    ddlCustName.SelectedValue = objrec.CustId;
                    ddlStatementOf.SelectedValue = objrec.SDBGReceiptsStatementOf;
                    ddlSDNo.SelectedValue = objrec.SDBGDetId;
                    ddlPaymentMode.SelectedValue = objrec.SDBGReceiptsPayMode;
                    txtDueDate.Text = objrec.SDBGReceiptsDueDate;
                    txtChequeNo.Text = objrec.SDBGReceiptsDdChequeNo;
                    txtChequeDate.Text = objrec.SDBGReceiptsDDChequeDate;
                    txtDetailsOfBank.Text = objrec.SDBGReceiptsBankDetails;
                    txtRemarks.Text = objrec.SDBGReceiptsRemarks;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

                SM.Dispose();
                ddlPaymentMode_SelectedIndexChanged1(sender, e);
                ddlSOPNo_SelectedIndexChanged1(sender, e);
                ddlTenderNo_SelectedIndexChanged1(sender, e);
            }

        }
    }
    #endregion

    #region LlbtnSDReceiptsNo Click
    protected void lbtnSDReceiptsNo_Click(object sender, EventArgs e)
    {

        tblReceiptsDetails.Visible = false;
        LinkButton lbtnSDReceiptsNo;
        lbtnSDReceiptsNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSDReceiptsNo.Parent.Parent;
        gvDepositReceiptsDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {

            SM.SDBGReceipts objrec = new SM.SDBGReceipts();
            if (objrec.SDBGReceipts_Select(gvDepositReceiptsDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                //btnSave.Enabled = false;
                tblReceiptsDetails.Visible = true;
                txtReceipts.Text = objrec.SDBGReceiptsNo;

                txtDate.Text = objrec.SDBGReceiptsDate;

                ddlCustName.SelectedValue = objrec.CustId;
                ddlStatementOf.SelectedValue = objrec.SDBGReceiptsStatementOf;
                ddlSDNo.SelectedValue = objrec.SDBGDetId;
                ddlPaymentMode.SelectedValue = objrec.SDBGReceiptsPayMode;
                txtDueDate.Text = objrec.SDBGReceiptsDueDate;
                txtChequeNo.Text = objrec.SDBGReceiptsDdChequeNo;
                txtChequeDate.Text = objrec.SDBGReceiptsDDChequeDate;
                txtDetailsOfBank.Text = objrec.SDBGReceiptsBankDetails;
                txtRemarks.Text = objrec.SDBGReceiptsRemarks;

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            SM.Dispose();
            ddlPaymentMode_SelectedIndexChanged1(sender, e);
            ddlSOPNo_SelectedIndexChanged1(sender, e);
            ddlTenderNo_SelectedIndexChanged1(sender, e);
        }

    }
    #endregion


    #region Button DELETE Click
    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        if (gvDepositReceiptsDetails.SelectedIndex > -1)
        {
            try
            {
                SM.SDBGReceipts objrec = new SM.SDBGReceipts();

                MessageBox.Show(this, objrec.SDBGReceipts_Delete(gvDepositReceiptsDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvDepositReceiptsDetails.DataBind();
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

    #region Button REFRESH Click
    protected void btnRefresh_Click1(object sender, EventArgs e)
    {
        SM.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click1(object sender, EventArgs e)
    {
        tblReceiptsDetails.Visible = false;

    }
     #endregion

    protected void gvDepositReceiptsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "SDBG Receipts Date")
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
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void btnSearchGo_Click1(object sender, EventArgs e)
    {
        gvDepositReceiptsDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvDepositReceiptsDetails.DataBind();
    }
    #endregion

    #region ddlPaymentMode_SelectedIndexChanged
    protected void ddlPaymentMode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedItem.Text == "cheque" || ddlPaymentMode.SelectedItem.Text == "DD")
        {
            tblCheque.Visible = true;
        }
        
        else
        {
            tblCheque.Visible = false;

        }

    }
      #endregion

    #region ddlSOPNo_SelectedIndexChanged
    protected void ddlSOPNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {


            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrder_Select(ddlSOPNo.SelectedItem.Value) > 0)
            {
                txtSOPDate.Text = objSO.SODate;


                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objSO.CustId) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;

                    txtUnitName.Text = objSMCustomer.CustUnitName;
                    txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                }
                else if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;

                    txtUnitName.Text = "--";
                    txtUnitAddress.Text = "--";
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

       

    #region ddlTenderNo_SelectedIndexChanged
    protected void ddlTenderNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            SM.SalesEnquiry objsalesqto = new SM.SalesEnquiry();
            objsalesqto.SalesEnquiry_Select(ddlTenderNo.SelectedItem.Value);
           
                txtTenderDate.Text = objsalesqto.EnqDate;

            

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

    protected void ddlSDNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            SM.SDBG objsd = new SM.SDBG();

            objsd.SDBG_Select(ddlSDNo.SelectedItem.Value);
            //objsd.SDBGDetails_Select(ddlSDNo.SelectedItem.Value, gvDepositReceiptsDetails);
            txtSDDate.Text = objsd.SDBGDate;

            //txtTenderDate.Text = objsd.SDBGDetDated;

           

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


























  

    
}

 
