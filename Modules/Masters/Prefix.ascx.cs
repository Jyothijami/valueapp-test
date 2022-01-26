using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using System;
using vllib;

public partial class Modules_Masters_Prefix : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            PrefixSelect();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnSave.Enabled = up.add;

    }
   

    #region Prefix Select
    private void PrefixSelect()
    {
        try
        {
            Masters.Prefix objMaster = new Masters.Prefix();
            if (objMaster.Prefix_Select() > 0)
            {
                btnSave.Text = "Update";


                txtCustomerInformation.Text = objMaster.PF_CUSTOMERINFO;
                txtSalesLead.Text = objMaster.PF_SALESLEAD;
                txtSalesAssignments.Text = objMaster.PF_SALESASSIGNMENTS;
                txtSalesQuotation.Text = objMaster.PF_SALESQUOTATION;
                txtSalesOrder.Text = objMaster.PF_SALESORDER;
                txtOrderProfile.Text = objMaster.PF_ORDERPROFILE;
                txtSalesOrderAcceptance.Text = objMaster.PF_SALESORDERACCEPTANCE;
                txtDelliveryChallan.Text = objMaster.PF_DELIVERYCHALLAN;
                txtSalesInvoice.Text = objMaster.PF_SALESINVOICE;
                txtCheckingFormat.Text = objMaster.PF_CHECKINGFORMAT;
                txtPurchaseOrderDetails.Text = objMaster.PF_PURCHASEORDERDETAILS;
                txtWorkOrderDetails.Text = objMaster.PF_WORKORDERDETAILS;
                txtPurchaseInvoice.Text = objMaster.PF_PURCHASEINVOICE;
                txtSupplierMaster.Text = objMaster.PF_SUPPLIERMASTER;
                txtClaimForm.Text = objMaster.PF_CLAIMFORM;
                txtAgentMaster.Text = objMaster.PF_AGENTMASTER;
                txtSDBG.Text = objMaster.PF_SD_BG;
                txtFEOrderprofile.Text = objMaster.PF_FE_ORDERPROFILE;
                txtSalesPaymentsReceived.Text = objMaster.PF_SALESPAYMENTSRECEIVED;
                txtSDBGReceipts.Text = objMaster.PF_SD_BG_RECEIPTS;
                txtEmdsReceived.Text = objMaster.PF_EMDSRECEIVED;
                txtComplaintRegister.Text = objMaster.PF_COMPLAINTREGISTER;
                txtServiceAssignments.Text = objMaster.PF_SERVICEASSIGNMENTS;
                txtServiceReport.Text = objMaster.PF_SERVICEREPORT;
                txtAmcQuotation.Text = objMaster.PF_AMC_QUOTATION;
                txtAmcOrder.Text = objMaster.PF_AMC_ORDER;
                txtAmcOrderAcceptance.Text = objMaster.PF_AMC_ORDERACCEPTANCE;
                txtAmcOrderProfile.Text = objMaster.PF_AMC_ORDERPROFILE;
                txtWarrantyClaim.Text = objMaster.PF_WARRANTYCLAIM;
                txtSparesQuotation.Text = objMaster.PF_SPARESQUOTATION;
                txtSparesOrder.Text = objMaster.PF_SPARESORDER;
                txtSparesOrderProfile.Text = objMaster.PF_SPARESORDERPROFILE;
                txtSparesOrderACCEPTANCE.Text = objMaster.PF_SPARESORDERACCEPTANCE;
                txtAmcInvoice.Text = objMaster.PF_AMC_INVOICE;
                txtAmcPaymentsReceived.Text = objMaster.PF_AMC_PAYMETSRECEIVED;
                txtEmployeeMaster.Text = objMaster.PF_EMPLOYEEMASTER;
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
    #endregion

    #region Prefix Update
    private void PrefixUpdate()
    {
        try
        {
            Masters.Prefix objMaster = new Masters.Prefix();

            objMaster.PF_CUSTOMERINFO = txtCustomerInformation.Text;
            objMaster.PF_SALESLEAD = txtSalesLead.Text;
            objMaster.PF_SALESASSIGNMENTS = txtSalesAssignments.Text;
            objMaster.PF_SALESQUOTATION = txtSalesQuotation.Text;
            objMaster.PF_SALESORDER = txtSalesOrder.Text;
            objMaster.PF_ORDERPROFILE = txtOrderProfile.Text;
            objMaster.PF_SALESORDERACCEPTANCE = txtSalesOrderAcceptance.Text;
            objMaster.PF_DELIVERYCHALLAN = txtDelliveryChallan.Text;
            objMaster.PF_SALESINVOICE = txtSalesInvoice.Text;
            objMaster.PF_CHECKINGFORMAT = txtCheckingFormat.Text;
            objMaster.PF_PURCHASEORDERDETAILS = txtPurchaseOrderDetails.Text;
            objMaster.PF_WORKORDERDETAILS = txtWorkOrderDetails.Text;
            objMaster.PF_PURCHASEINVOICE = txtPurchaseInvoice.Text;
            objMaster.PF_SUPPLIERMASTER = txtSupplierMaster.Text;
            objMaster.PF_CLAIMFORM = txtClaimForm.Text;
            objMaster.PF_AGENTMASTER = txtAgentMaster.Text;
            objMaster.PF_SD_BG = txtSDBG.Text;
            objMaster.PF_FE_ORDERPROFILE = txtOrderProfile.Text;
            objMaster.PF_SALESPAYMENTSRECEIVED = txtSalesPaymentsReceived.Text;
            objMaster.PF_SD_BG_RECEIPTS = txtSDBGReceipts.Text;
            objMaster.PF_EMDSRECEIVED = txtEmdsReceived.Text;
            objMaster.PF_COMPLAINTREGISTER = txtComplaintRegister.Text;
            objMaster.PF_SERVICEASSIGNMENTS = txtServiceAssignments.Text;
            objMaster.PF_SERVICEREPORT = txtServiceReport.Text;
            objMaster.PF_AMC_QUOTATION = txtAmcQuotation.Text;
            objMaster.PF_AMC_ORDER = txtAmcOrder.Text;
            objMaster.PF_AMC_ORDERACCEPTANCE = txtAmcOrderAcceptance.Text;
            objMaster.PF_AMC_ORDERPROFILE = txtOrderProfile.Text;
            objMaster.PF_WARRANTYCLAIM = txtWarrantyClaim.Text;
            objMaster.PF_SPARESQUOTATION = txtSparesQuotation.Text;
            objMaster.PF_SPARESORDER = txtSparesOrder.Text;
            objMaster.PF_SPARESORDERPROFILE = txtSparesOrderProfile.Text;
            objMaster.PF_SPARESORDERACCEPTANCE = txtSparesOrderACCEPTANCE.Text;
            objMaster.PF_AMC_INVOICE = txtAmcInvoice.Text;
            objMaster.PF_AMC_PAYMETSRECEIVED = txtAmcPaymentsReceived.Text;
            objMaster.PF_EMPLOYEEMASTER = txtEmployeeMaster.Text;
            MessageBox.Show(this, objMaster.Prefix_Update());
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

    #region Prefix Save
    private void PrefixSave()
    {
        try
        {
            Masters.Prefix objMaster = new Masters.Prefix();

            objMaster.PF_CUSTOMERINFO = txtCustomerInformation.Text;
            objMaster.PF_SALESLEAD = txtSalesLead.Text;
            objMaster.PF_SALESASSIGNMENTS = txtSalesAssignments.Text;
            objMaster.PF_SALESQUOTATION = txtSalesQuotation.Text;
            objMaster.PF_SALESORDER = txtSalesOrder.Text;
            objMaster.PF_ORDERPROFILE = txtOrderProfile.Text;
            objMaster.PF_SALESORDERACCEPTANCE = txtSalesOrderAcceptance.Text;
            objMaster.PF_DELIVERYCHALLAN = txtDelliveryChallan.Text;
            objMaster.PF_SALESINVOICE = txtSalesInvoice.Text;
            objMaster.PF_CHECKINGFORMAT = txtCheckingFormat.Text;
            objMaster.PF_PURCHASEORDERDETAILS = txtPurchaseOrderDetails.Text;
            objMaster.PF_WORKORDERDETAILS = txtWorkOrderDetails.Text;
            objMaster.PF_PURCHASEINVOICE = txtPurchaseInvoice.Text;
            objMaster.PF_SUPPLIERMASTER = txtSupplierMaster.Text;
            objMaster.PF_CLAIMFORM = txtClaimForm.Text;
            objMaster.PF_AGENTMASTER = txtAgentMaster.Text;
            objMaster.PF_SD_BG = txtSDBG.Text;
            objMaster.PF_FE_ORDERPROFILE = txtFEOrderprofile.Text;
            objMaster.PF_SALESPAYMENTSRECEIVED = txtSalesPaymentsReceived.Text;
            objMaster.PF_SD_BG_RECEIPTS = txtSDBGReceipts.Text;
            objMaster.PF_EMDSRECEIVED = txtEmdsReceived.Text;
            objMaster.PF_COMPLAINTREGISTER = txtComplaintRegister.Text;
            objMaster.PF_SERVICEASSIGNMENTS = txtServiceAssignments.Text;
            objMaster.PF_SERVICEREPORT = txtServiceReport.Text;
            objMaster.PF_AMC_QUOTATION = txtAmcQuotation.Text;
            objMaster.PF_AMC_ORDER = txtAmcOrder.Text;
            objMaster.PF_AMC_ORDERACCEPTANCE = txtAmcOrderAcceptance.Text;
            objMaster.PF_AMC_ORDERPROFILE = txtAmcOrderProfile.Text;
            objMaster.PF_WARRANTYCLAIM = txtWarrantyClaim.Text;
            objMaster.PF_SPARESQUOTATION = txtSparesQuotation.Text;
            objMaster.PF_SPARESORDER = txtSparesOrder.Text;
            objMaster.PF_SPARESORDERPROFILE = txtSparesOrderProfile.Text;
            objMaster.PF_SPARESORDERACCEPTANCE = txtSparesOrderACCEPTANCE.Text;
            objMaster.PF_AMC_INVOICE = txtAmcInvoice.Text;
            objMaster.PF_AMC_PAYMETSRECEIVED = txtAmcPaymentsReceived.Text;
            objMaster.PF_EMPLOYEEMASTER = txtEmployeeMaster.Text;

            string ReturnMessage = objMaster.Prefix_Save();
            if (ReturnMessage == "Data Saved Successfully")
            {
                btnSave.Text = "Update";
            }
            MessageBox.Show(this, ReturnMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region Save Click
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            PrefixSave();
        }
        else if (btnSave.Text == "Update")
        {
            PrefixUpdate();
        }
    }
    #endregion
}
