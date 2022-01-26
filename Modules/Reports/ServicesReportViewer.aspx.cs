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
using YantraDAL;
using Yantra.MessageBox;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;


public partial class Modules_Reports_ServicesReportViewer : System.Web.UI.Page
{
    string reportType;
    ReportDocument myRep = new ReportDocument();


    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            reportType = Request.QueryString["type"].ToString();
        }

        RunReport();
    }
    #endregion


    protected void Page_Unload(object sender, EventArgs e)
    {
        if (myRep != null)
        {
            myRep.Close();
            myRep.Dispose();
            CrystalReportViewer1.ReportSource = null;
            CrystalReportViewer1.Dispose();
            CrystalReportViewer1 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }

    #region Run Selected Type Report
    private void RunReport()
    {
        switch (reportType)
        {
            case "amcquot":
                {
                    AMCQuotationReport("Services_AMCQuotation", Request.QueryString["amcqno"].ToString());
                    break;
                }
            //case "salesenq":
            //    {
            //        SalesEnquiryReport("SM_SalesEnquiry", Request.QueryString["enqno"].ToString());
            //        break;
            //    }
            //case "salesorder":
            //    {
            //        SalesOrderReport("SM_SalesOrder", Request.QueryString["sono"].ToString());
            //        break;
            //    }
            //case "orderacc":
            //    {
            //        SalesOrderAcceptanceReport("SM_SalesOrderAcceptance", Request.QueryString["oano"].ToString());
            //        break;
            //    }
            //case "workorder":
            //    {
            //        WorkOrderReport("SM_WorkOrder", Request.QueryString["wono"].ToString());
            //        break;
            //    }
            //case "salesinvoice":
            //    {
            //        SalesInvoiceReport("Inv_Invoice", Request.QueryString["siid"].ToString());
            //        break;
            //    }
            //case "deliverychallan":
            //    {
            //        DeliveryChallanReport("Inv_DeliveryChallan", Request.QueryString["dcid"].ToString());
            //        break;
            //    }
            //case "claimform":
            //    {
            //        ClaimFormReport("ClaimForm", Request.QueryString["cfid"].ToString());
            //        break;
            //    }
            //case "checkingformat":
            //    {
            //        CheckingFormatReport("CheckingFormat", Request.QueryString["chkid"].ToString());
            //        break;
            //    }
            //case "purchaseorder":
            //    {
            //        PurchaseOrderReport("PurchaseOrder", Request.QueryString["fpoid"].ToString());
            //        break;
            //    }
            //case "supplierworkorder":
            //    {
            //        SupplierWorkOrderReport("SuppWorkOrderBody", Request.QueryString["supwoid"].ToString());
            //        break;
            //    }
            default:
                {
                    MessageBox.Show(this, "Under Construction");
                    break;
                }
        }
    }
    #endregion

    #region AMC Quotation Report
    private void AMCQuotationReport(string ReportName, string AMCQuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_AMC_QUOTATION_DET where AMCQT_ID ="+AMCQuotId+") ", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_DET", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMP_PROFILE");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da6.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da7.Fill(rds, "YANTRA_AMC_QUOTATION_DET");
            da8.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da9.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da10.Fill(rds, "YANTRA_EMPLOYEE_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_AMC_QUOTATION_MAST.AMCQT_ID}=" + AMCQuotId + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Sales Enquiry Report
    private void SalesEnquiryReport(string ReportName, string EnqId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_ENQ_DET", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da6.Fill(rds, "YANTRA_ENQ_DET");
            da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_ID}=" + EnqId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Sales Order Report
    private void SalesOrderReport(string ReportName, string SOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMP_PROFILE");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da6.Fill(rds, "YANTRA_QUOT_MAST");
            da7.Fill(rds, "YANTRA_SO_MAST");
            da8.Fill(rds, "YANTRA_SO_DET");
            da9.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da10.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_ID}=" + SOId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Sales Order Acceptance Report
    private void SalesOrderAcceptanceReport(string ReportName, string OAId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_OA_MAST", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_OA_DET", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_TRANS_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMP_PROFILE");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da6.Fill(rds, "YANTRA_QUOT_MAST");
            da7.Fill(rds, "YANTRA_SO_MAST");
            da8.Fill(rds, "YANTRA_OA_MAST");
            da9.Fill(rds, "YANTRA_OA_DET");
            da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da11.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da12.Fill(rds, "YANTRA_LKUP_TRANS_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_OA_MAST.OA_ID}=" + OAId + "";
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Work Order Report
    private void WorkOrderReport(string ReportName, string WOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_OA_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_WO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_WO_DET", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_OA_DET", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_OA_MAST");
            da5.Fill(rds, "YANTRA_WO_MAST");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_WO_DET");
            da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da11.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da12.Fill(rds, "YANTRA_OA_DET");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_WO_MAST.WO_ID}=" + WOId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Claim Form Report
    private void ClaimFormReport(string ReportName, string CFId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CLAIM_FORM", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CLAIM_FORM_PROD_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CLAIM_FORM");
            da2.Fill(rds, "YANTRA_CLAIM_FORM_PROD_DET");
            da3.Fill(rds, "YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da8.Fill(rds, "YANTRA_CUSTOMER_DET");
            da9.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da10.Fill(rds, "YANTRA_COMP_PROFILE");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_CLAIM_FORM.CF_ID}=" + CFId + "";
            CrystalReportViewer1.ReportSource = myRep;
            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Claim Form");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    ///////////////////////////SCM

    #region Purchase Order Report
    private void PurchaseOrderReport(string ReportName, string FPOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_ITEM_UOM_DET", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_FIXED_PO_MAST");
            da2.Fill(rds, "YANTRA_FIXED_PO_DET");
            da3.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da4.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da5.Fill(rds, "YANTRA_LKUP_UOM");
            da6.Fill(rds, "YANTRA_ITEM_UOM_DET");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_MAST.FPO_ID}=" + FPOId + "";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Supplier Work Order Report
    private void SupplierWorkOrderReport(string ReportName, string SupWoId)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SUP_WO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SUP_WO_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SUP_WO_MAST");
            da2.Fill(rds, "YANTRA_SUP_WO_DET");
            da3.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da4.Fill(rds, "YANTRA_LKUP_UOM");
            da5.Fill(rds, "YANTRA_ITEM_MAST");
            da6.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SUP_WO_MAST.SUP_WO_ID}=" + SupWoId + "";
            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Purchase Order");

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Checking Format Report
    private void CheckingFormatReport(string ReportName, string CHKId)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CHECKING_FORMAT", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CHECKING_FORMAT");
            da2.Fill(rds, "YANTRA_ITEM_MAST");
            da3.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_CHECKING_FORMAT.CHK_ID}=" + CHKId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    ///////////////////////////Inventory

    #region Sales Invoice Report
    private void SalesInvoiceReport(string ReportName, string SIId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da5.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_ID}=" + SIId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Delivery Challan Report
    private void DeliveryChallanReport(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from  YANTRA_DELIVERY_CHALLAN_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_DELIVERY_CHALLAN_DET where DC_ID = "+DCId+") ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da5.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_DELIVERY_CHALLAN_MAST.DC_ID}=" + DCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

}

 
