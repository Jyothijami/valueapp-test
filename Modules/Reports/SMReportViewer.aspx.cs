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
using System.Net.Mail;
using CrystalDecisions.Shared;
using System.Text;
using System.Net;


public partial class Modules_Reports_SMReportViewer : System.Web.UI.Page
{

    string reportType; string mailAttachPath;
    private ReportDocument myRep = new ReportDocument();

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        mailAttachPath = AppDomain.CurrentDomain.BaseDirectory + "mailattach\\";
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



    /////////////////////////////////////// Report Selector indapprlno

    #region Run Selected Type Report
    private void RunReport()
    {
        string returnFileName = "";
        switch (reportType)
        {
            case "SalesReturn":
                {
                    SalesReturn("SM_SalesReturn", Request.QueryString["Srno"].ToString());
                    break;
                }
            case "ReturnNote":
                {
                    ReturnNote("ReturnNote", Request.QueryString["Srno"].ToString());
                    break;
                }

            case "PurchaseReturn":
                {
                    PurchaseReturn("PurchaseReturn", Request.QueryString["Prno"].ToString());
                    break;
                }

            case "PInovice":
                {
                    PInvoice("ProformaInvoice", Request.QueryString["PIid"].ToString());
                    break;
                }
            case "PInv":
                {
                    PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());
                    break;
                }

            case "PInvoice1":
                {
                    PIReport("Inv_PInvoice", Request.QueryString["PIid"].ToString());
                    //PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());
                    break;
                }
            case "QRCode":
                {
                    QRReport("QRCode", Request.QueryString["QRid"].ToString());
                    //PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());
                    break;
                }
            case "QRCodeProc":
                {
                    QRReport_Proc("TestQrCode", Request.QueryString["ItemCode"].ToString(), Request .QueryString ["t1"].ToString (),Request .QueryString ["t2"].ToString (),Request.QueryString ["ID"].ToString ());
                    //PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());
                    break;
                }
            case "QRCodeProc2":
                {
                    QRReport_ProcRoller("TestQrCode", Request.QueryString["t1"].ToString(), Request.QueryString["t2"].ToString(), Request.QueryString["ID"].ToString());
                    //PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());
                    returnFileName = mailAttachPath + "QR.pdf";
                    myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, returnFileName);


                    ClientScript.RegisterStartupScript(this.Page.GetType(), "popupOpener", "var popup=window.open('mailattach/'" + returnFileName + "'');popup.focus();", true);

                    string FilePath = returnFileName;

                    WebClient User = new WebClient();

                    Byte[] FileBuffer = User.DownloadData(FilePath);

                    if (FileBuffer != null)
                    {

                        Response.ContentType = "application/pdf";

                        Response.AddHeader("content-length", FileBuffer.Length.ToString());

                        Response.BinaryWrite(FileBuffer);

                    }
                    break;
                }
            case "QRCodeProc1":
                {
                    QRReport_Proc1("TestQrCode_A4 - Copy", Request.QueryString["t1"].ToString(), Request.QueryString["t2"].ToString(), Request.QueryString["ID"].ToString(), Request.QueryString["QRId"].ToString());

                    //QRReport_Proc1("TestQrCode_A4",  Request.QueryString["t1"].ToString(), Request.QueryString["t2"].ToString(), Request.QueryString["ID"].ToString(),Request .QueryString ["QRId"].ToString ());
                    //PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());

                    //returnFileName = mailAttachPath + "QR.pdf";
                    //myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, returnFileName);


                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "popupOpener", "var popup=window.open('mailattach/'" + returnFileName + "'');popup.focus();", true);

                    //string FilePath = returnFileName;

                    //WebClient User = new WebClient();

                    //Byte[] FileBuffer = User.DownloadData(FilePath);

                    //if (FileBuffer != null)
                    //{

                    //    Response.ContentType = "application/pdf";

                    //    Response.AddHeader("content-length", FileBuffer.Length.ToString());

                    //    Response.BinaryWrite(FileBuffer);

                    //}
                  



                    break;
                }
            case "QRCodeLabel":
                {
                    QRReport_Label("OldStk_QRCode", Request.QueryString["Qty"].ToString());
                    //PIReport("Inv_PInvoice - Copy", Request.QueryString["PIid"].ToString());
                    break;
                }
            case "Amendmnet":
                {
                    AmendmnetReport("AmendmentPO", Request.QueryString["Ameid"].ToString());
                    break;
                }

            case "SupEnq":
                {
                    Supenqdet("SupplierEnquiry", Request.QueryString["SupDetid"].ToString());
                    break;
                }
            case "P2":
                {
                    p2("P2", Request.QueryString["SupDetid"].ToString());
                    break;
                }
            case "PT":
                {
                    p2("PT", Request.QueryString["SupDetid"].ToString());
                    break;
                }
            case "ItemQty":
                {
                    ItemMasterQty("ItemQty", Request.QueryString["cpid"].ToString(), Request.QueryString["gdid"].ToString(), Request.QueryString["brandid"].ToString(), Request.QueryString["MdNo"].ToString());
                    break;
                }

            case "Indent":
                {
                    IndentReport("Indent_New", Request.QueryString["indno"].ToString());
                    break;
                }
            case "IndentReq":
                {
                    IndentReqReport("Indent_Request", Request.QueryString["indno"].ToString());
                    break;
                }
            //case "Indent":
            //    {
            //        IndentReport("Indent", Request.QueryString["indno"].ToString());
            //        break;
            //    }
            case "StatementPO":
                {
                    StatementPo23("State1", Request.QueryString["sono"].ToString());
                    break;
                }
            case "InternalIndent":
                {
                    InternalIndentReport("Indent - Copy", Request.QueryString["indno"].ToString());
                    break;
                }
            case "IndentApproval":
                {
                    IndentApprovalReport("IndentApproval", Request.QueryString["indapprlno"].ToString());
                    break;
                }
            case "quot":
                {
                    SalesQuotationReport("SM_SalesQuotation", Request.QueryString["qno"].ToString());
                    break;
                }

            case "quotwinp":
                {
                    //NewSalesQuotationReport("RMSU", Request.QueryString["qno"].ToString());
                    NewSalesQuotationReport("RMSU_Copy", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwinp_NoGST":
                {
                    //NewSalesQuotationReport("RMSU", Request.QueryString["qno"].ToString());
                    NewSalesQuotationReport("RMSU_Inclusive", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwinp_WoMRP":
                {
                    //NewSalesQuotationReport("RMSU", Request.QueryString["qno"].ToString());
                    NewSalesQuotationReport("WoPrices", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwinp_WoMRPcodes":
                {
                    //NewSalesQuotationReport("RMSU", Request.QueryString["qno"].ToString());
                    NewSalesQuotationReport("WoPrices_WoCode", Request.QueryString["qno"].ToString());
                    break;
                }
            case "PertQuot":
                {
                    //NewSalesQuotationReport("RMSU", Request.QueryString["qno"].ToString());
                    NewSalesQuotationReport_Pert("RMSU_Pert", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwithprice":
                {
                    NewSalesQuotationReport("WithPrice", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotWPSP":
                {
                    NewSalesQuotationReport("WPSP", Request.QueryString["qno"].ToString());
                    break;
                }
            case "T2":
                {
                    NewSalesQuotationReport("RMWSU", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("Test2", Request.QueryString["qno"].ToString());
                    break;
                }

            case "T3":
                {

                    NewSalesQuotationReport("RMSWU", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("Test3", Request.QueryString["qno"].ToString());
                    break;
                }

            case "Tech":
                {
                    NewTechSalesQuotationReport("TDraw", Request.QueryString["qno"].ToString());
                    break;
                }

            case "C1":
                {

                    CompareSalesQuotationReport("R-Comapre", Request.QueryString["qno"].ToString());
                    //CompareSalesQuotationReport("CompareQuot", Request.QueryString["qno"].ToString());
                    break;
                }



            case "quotwinpnor":
                {
                    NewSalesQuotationReport("winpTesting", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwimrp":
                {
                    NewSalesQuotationReport("WRCM", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("QT2", Request.QueryString["qno"].ToString());
                    break;
                }
            case "Q3":
                {
                    NewSalesQuotationReport("WRWCM", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("QT3", Request.QueryString["qno"].ToString());
                    break;
                }
            case "Q4":
                {
                    NewSalesQuotationReport("WRWCMS", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("QT4", Request.QueryString["qno"].ToString());
                    break;
                }
            case "Q5":
                {
                    NewSalesQuotationReport("RMSU_code", Request.QueryString["qno"].ToString());

                    //NewSalesQuotationReport("WRCSM", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("QT5", Request.QueryString["qno"].ToString());
                    break;
                }
            case "QCt2":
                {
                    NewSalesQuotationReport("WRWCWUS", Request.QueryString["qno"].ToString());
                    //NewSalesQuotationReport("QC2", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwiwithoutnorpricie":
                {
                    NewSalesQuotationReport("wiWithoutNormalPrice", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwiwithsprpricie":
                {
                    NewSalesQuotationReport("wiWithSpprice", Request.QueryString["qno"].ToString());
                    break;
                }

            case "Copyquotwinp":
                {
                    NewSalesQuotationReport("winp - Copy (2)", Request.QueryString["qno"].ToString());
                    break;
                }
            case "Copyquotwiwithoutnorpricie":
                {
                    NewSalesQuotationReport("wiWithoutNormalPrice - Copy", Request.QueryString["qno"].ToString());
                    break;
                }
            case "Copyquotwiwithsprpricie":
                {
                    NewSalesQuotationReport("wiWithSpprice - Copy", Request.QueryString["qno"].ToString());
                    break;
                }


            case "quotwinpitemcode":
                {
                    SalesQuotationReport("wiWithSpprice", Request.QueryString["qno"].ToString());
                    break;
                }


            case "quottb":
                {
                    SalesQuotationReport("SM_SalesQuotationProjet", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quottbmn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationProjetMN", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwithoutMN":
                {
                    SalesQuotationReportWithoutModelNo("SM_SalesQuotationWithoutModelNo", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotcb":
                {
                    SalesQuotationReport("SM_SalesQuotationSpecial", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotcbmn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationSpecialMN", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwd":
                {
                    SalesQuotationReport("SM_SalesQuotationWithoutDraw", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwdimg":
                {
                    SalesQuotationReport("SM_SalesQuotationWithoutImages", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwdimgmn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationWithoutImagesWithoutMN", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwdmn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationWithoutDrawWithoutMN", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwr":
                {
                    SalesQuotationReport("SM_SalesQuotationWithDrawingsNoRates", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotwrMn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationWithDrawingsNoRatesMN", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotspwd":
                {
                    SalesQuotationReport("SM_SalesQuotationSpecialWithDrawings", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotspwdmn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationSpecialWithDrawingsmn", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotppwd":
                {
                    SalesQuotationReport("SM_SalesQuotationProjetWithDrawings", Request.QueryString["qno"].ToString());
                    break;
                }
            case "quotppwdmn":
                {
                    SalesQuotationReport("Copy of SM_SalesQuotationProjetWithDrawingsMN", Request.QueryString["qno"].ToString());
                    break;
                }
            case "salesenq":
                {
                    SalesEnquiryReport("SM_SalesEnquiry", Request.QueryString["enqno"].ToString());
                    break;
                }
            case "salesorder":
                {
                    SalesOrderReport("SM_SalesOrder", Request.QueryString["sono"].ToString());
                    break;
                }
            case "StatementPO1":
                {
                    StatementPo2("State1", Request.QueryString["sono"].ToString());
                    break;
                }
            case "orderacc":
                {
                    SalesOrderAcceptanceReport("SM_SalesOrderAcceptance", Request.QueryString["sono"].ToString());
                    break;
                }
            case "workorder":
                {
                    WorkOrderReport("SM_WorkOrder - Copy", Request.QueryString["wono"].ToString());
                    break;
                }

            case "Dispatch":
                {
                    DispatchDetailsReport("Inv_Dispatchdetails", Request.QueryString["DCId"].ToString());
                    break;
                }
            case "InternoPL":
                {
                    InternoPL("InternoIn", Request.QueryString["DCId"].ToString());
                    break;
                }
            case "InternoIn":
                {
                    InternoIn("InternoInvoice", Request.QueryString["DCId"].ToString());
                    break;
                }
            case "CreditApproval":
                {
                    CreditApprovalReport("Inv_CreditApproval", Request.QueryString["DCId"].ToString());
                    break;
                }
            case "salesinvoice":
                {
                    if (Request.QueryString["dcfor"].ToString() == "Sales")
                    {
                        SalesInvoiceNewReport("InvoiceNew", Request.QueryString["siid"].ToString());

                        //SalesInvoiceReport("Inv_Invoice", Request.QueryString["siid"].ToString());
                    }
                    if (Request.QueryString["dcfor"].ToString() == "hai")
                    {
                        CashInvoiceReport("Sample_Cash_Inv", Request.QueryString["siid"].ToString());
                        //CashInvoiceReport("Copy of Inv_Invoice", Request.QueryString["siid"].ToString());
                    }
                    else if (Request.QueryString["dcfor"].ToString() == "Spares")
                    {
                        SparesInvoiceReport("Inv_SparesInvoice", Request.QueryString["siid"].ToString());
                    }
                    break;
                }
            case "deliverychallan":
                {
                    if (Request.QueryString["dcfor"].ToString() == "Sales")
                    {
                        DeliveryChallanReport("Inv_DeliveryChallan", Request.QueryString["dcid"].ToString());
                    }
                    else if (Request.QueryString["dcfor"].ToString() == "Spares")
                    {
                        SparesDeliveryChallanReport("Inv_SparesDeliveryChallan", Request.QueryString["dcid"].ToString());
                    }
                    break;
                }

            case "deliverychallanOri":
                {
                    if (Request.QueryString["dcfor"].ToString() == "Sales")
                    {
                        DeliveryChallanReport("Inv_DeliveryChallan_Original", Request.QueryString["dcid"].ToString());
                        //DeliveryChallanReport("Inv_DeliveryChallan_Original_2", Request.QueryString["dcid"].ToString());

                    }
                    else if (Request.QueryString["dcfor"].ToString() == "Spares")
                    {
                        SparesDeliveryChallanReport("Inv_SparesDeliveryChallan", Request.QueryString["dcid"].ToString());
                    }
                    break;
                }

            case "deliverychallanDup":
                {
                    if (Request.QueryString["dcfor"].ToString() == "Sales")
                    {
                        DeliveryChallanReport("Inv_DeliveryChallan_Duplicate", Request.QueryString["dcid"].ToString());
                    }
                    else if (Request.QueryString["dcfor"].ToString() == "Spares")
                    {
                        SparesDeliveryChallanReport("Inv_SparesDeliveryChallan", Request.QueryString["dcid"].ToString());
                    }
                    break;
                }

            case "deliverychallanTri":
                {
                    if (Request.QueryString["dcfor"].ToString() == "Sales")
                    {
                        DeliveryChallanReport("Inv_DeliveryChallan_Triplicate2", Request.QueryString["dcid"].ToString());
                    }
                    else if (Request.QueryString["dcfor"].ToString() == "Spares")
                    {
                        SparesDeliveryChallanReport("Inv_SparesDeliveryChallan", Request.QueryString["dcid"].ToString());
                    }
                    break;
                }

            case "deliverychallanSample":
                {
                    DeliveryChallanReportForsample("Inv_DeliveryChallanSample", Request.QueryString["dcid"].ToString());
                    break;
                }
            case "deliverychallanSampleDuplicate":
                {
                    DeliveryChallanReportForsample("Inv_DeliveryChallanSample_Duplicate", Request.QueryString["dcid"].ToString());
                    break;
                }
            case "deliverychallanSample_Triplicate":
                {
                    DeliveryChallanReportForsample("Inv_DeliveryChallanSample_Tri", Request.QueryString["dcid"].ToString());
                    break;
                }

            case "MovingDc":
                {

                    MovingDeliveryChallan("MovingDC1", Request.QueryString["dcid"].ToString());
                    //MovingDeliveryChallan("move2", Request.QueryString["dcid"].ToString());
                    break;
                }

            case "Insurance":
                {
                    Insurance("Insurance", Request.QueryString["dcid"].ToString());
                    break;
                }
            case "HR":
                {
                    HR("HR");
                    break;
                }

            case "POAIR":
                {

                    POAIR("POAIR", Request.QueryString["siid"].ToString());
                    break;
                }

            case "POSEA":
                {

                    POSEA("POSea", Request.QueryString["siid"].ToString());
                    break;
                }


            case "OnDuty":
                {

                    OnDutyForm("EmpOnDutyForm", Request.QueryString["siid"].ToString());
                    break;
                }


            case "OneHour":
                {

                    OneHOurPermission("EmpOneHour", Request.QueryString["siid"].ToString());
                    break;
                }

            case "OverTime":
                {

                    OverTime("OverTimeApplication", Request.QueryString["siid"].ToString());
                    break;
                }

            case "ShiftChange":
                {

                    ShiftChange("ShiftChangeForm", Request.QueryString["siid"].ToString());
                    break;
                }

            case "TicketDetails":
                {

                    TicketDetails("TicketDetails", Request.QueryString["siid"].ToString());
                    break;
                }

            case "BankStatement":
                {

                    BankStatement("Bank_Statement", Request.QueryString["CPid"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["year"].ToString());
                    break;
                }

            case "Payslip":
                {

                    PaySlip("PaySlip_New", Request.QueryString["siid"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["year"].ToString());
                    break;
                }
            case "Payshet":
                {
                    Payshet("PayrollSheet", Request.QueryString["CPid"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["year"].ToString());
                    break;
                }

            case "PFSheet":
                {
                    PFSheet("PF_Sheet", Request.QueryString["CPid"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["year"].ToString());
                    break;
                }
            case "ESISheet":
                {
                    ESISheet("ESI_Sheet", Request.QueryString["CPid"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["year"].ToString());
                    break;
                }
            case "Payslip_Old":
                {

                    PaySlip_Old("Pay", Request.QueryString["siid"].ToString(), Request.QueryString["e"].ToString());
                    break;
                }




            case "MobileAdvance":
                {

                    MobileAdvance("ApplicationMobileAdvance", Request.QueryString["siid"].ToString());
                    break;
                }

            case "SalaryAdvance":
                {

                    SalaryAdvance("AdvanceSalary", Request.QueryString["siid"].ToString());
                    break;
                }

            case "TourAdvance":
                {

                    TourAdvance("StaffTour", Request.QueryString["siid"].ToString());
                    break;
                }
            case "salesinvoicestatement":
                {

                    SalesInvoiceStatementReport("Statement", Request.QueryString["siid"].ToString());
                    break;
                }

            case "StatementOfAcc":
                {

                    SalesStatementOfACCReport("InvioceStatementStmtOfAcc", Request.QueryString["siid"].ToString());
                    break;
                }

            case "salesinvoicestatement_notdelivered":
                {
                    SalesInvoiceStatementReport_NotDelivered("Yet_To_deliver_Statement", Request.QueryString["siid"].ToString(), Request.QueryString["det_id"].ToString());
                    break;
                }

            case "claimform":
                {
                    ClaimFormReport("ClaimForm", Request.QueryString["cfid"].ToString());
                    break;
                }
            case "checkingformat":
                {
                    CheckingFormatReport("CheckingFormat2", Request.QueryString["chkid"].ToString());
                    break;
                }

            case "MRN":
                {
                    CheckingFormatReport("MRN", Request.QueryString["chkid"].ToString());
                    break;
                }
            case "purchaseorder":
                {
                    PurchaseOrderReport("PurchaseOrder", Request.QueryString["fpoid"].ToString());
                    break;
                }

            case "Selfpurchaseorder":
                {
                    SelfPurchaseOrder("SelfPurchaseOrder", Request.QueryString["fpoid"].ToString());
                    break;
                }

            case "SelfpurchaseorderImport":
                {
                    SelfPurchaseOrder("LocalSelfPurchaseOrder", Request.QueryString["fpoid"].ToString());
                    break;
                }
            case "supplierworkorder":
                {
                    SupplierWorkOrderReport("SuppWorkOrderBody", Request.QueryString["supwoid"].ToString());
                    break;
                }
            case "servicerpt":
                {
                    ServiceReport("ServiceRpt", Request.QueryString["srid"].ToString(), Request.QueryString["srid2"].ToString());
                    break;
                }
            case "wc":
                {
                    WarrantyClaimReport("WarrantyClaim", Request.QueryString["wcid"].ToString());
                    break;
                }
            case "amcinvoice":
                {
                    AMCInvoiceReport("AMCInvoice", Request.QueryString["amciid"].ToString());
                    break;
                }
            case "amcquot":
                {
                    AMCQuotationReport("Services_AMCQuotation", Request.QueryString["amcqno"].ToString());
                    break;
                }
            case "sparesquot":
                {
                    SparesQuotationReport("Services_SparesQuotationBody", Request.QueryString["sparesqno"].ToString());
                    break;
                }
            case "amcoa":
                {
                    AMCAcceptanceReport("AMCOrderAcceptance", Request.QueryString["oano"].ToString());
                    break;
                }
            case "feorderprofile":
                {
                    FEOrderProfile("FEOrderProfile", Request.QueryString["feid"].ToString());
                    break;
                }

            case "ShipmentFallowUp":
                {
                    ShipmentFallowUp("ShipmentFallowUp", Request.QueryString["sdid"].ToString());
                    // ShipmentFallowUp("ShipmentFallowUp");
                    break;
                }

            case "CV1":
                {
                    CV("Convince", Request.QueryString["sdid"].ToString());
                    // ShipmentFallowUp("ShipmentFallowUp");
                    break;
                }
            case "Tour":
                {
                    TourClaim("TourClaim", Request.QueryString["sdid"].ToString());
                    // ShipmentFallowUp("ShipmentFallowUp");
                    break;
                }
            case "SiteInspection":
                {
                    Inspection("Inspection", Request.QueryString["ClientID"].ToString());
                    break;
                }
            default:
                {
                    MessageBox.Show(this, "Under Construction");
                    break;
                }
        }
    }
    #endregion


    ////////////////////////////////////Sales & Marketing
    #region Statement2 Po Report
    private void StatementPo23(string ReportName, string SOId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            // SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SOId + ")", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_SALES_RETURN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_SALES_RETURN_DET", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from V_Dc_ExtraMaterial", DBConString.ConnectionString());



            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da1.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_SO_MAST");
            da3.Fill(rds, "YANTRA_SO_DET");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da8.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da9.Fill(rds, "YANTRA_SALES_RETURN_MAST");
            da10.Fill(rds, "YANTRA_SALES_RETURN_DET");
            da11.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da12.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da13.Fill(rds, "V_Dc_ExtraMaterial");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_DET.SO_ID}=" + SOId + "";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    # region ShipmentFallowUp

    private void ShipmentFallowUp(string ReportName, string sdid)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SHIPPING_DETAILS_FOLLOWUP", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_SHIPPING_DETAILS_MASTER", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_PURCHASE_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString()); //


            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_SHIPPING_DETAILS_FOLLOWUP");
            da1.Fill(rds, "YANTRA_SHIPPING_DETAILS_MASTER");
            da2.Fill(rds, "YANTRA_PURCHASE_INVOICE_MAST");
            da3.Fill(rds, "YANTRA_SUPPLIER_MAST");//


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SHIPPING_DETAILS_FOLLOWUP.SD_ID}=" + sdid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }



    #endregion

    #region Sales Return Report
    private void ReturnNote(string ReportName, string Srno)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from ReturnNote_Det_tbl", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from ReturnNote_tbl", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM ReturnNote_Det_tbl WHERE SR_ID=" + Srno + ")", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString()); //
            //SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "ReturnNote_Det_tbl");
            da1.Fill(rds, "ReturnNote_tbl");
            da2.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_MAST");//
            da4.Fill(rds, "YANTRA_CUSTOMER_UNITS");//
            da5.Fill(rds, "YANTRA_ITEM_MAST");//
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");//
            //da7.Fill(rds, "YANTRA_SO_MAST");//


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{ReturnNote_tbl.SR_ID}=" + Srno + "";

            CrystalReportViewer1.ReportSource = myRep;


            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Sales Return Report
    private void SalesReturn(string ReportName, string Srno)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SALES_RETURN_DET", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_SALES_RETURN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_SALES_RETURN_DET WHERE SR_ID=" + Srno + ")", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_SALES_RETURN_DET");
            da1.Fill(rds, "YANTRA_SALES_RETURN_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_MAST");//
            da4.Fill(rds, "YANTRA_COMP_PROFILE");//
            da5.Fill(rds, "YANTRA_ITEM_MAST");//
            da6.Fill(rds, "YANTRA_QUOT_MAST");//
            da7.Fill(rds, "YANTRA_SO_MAST");//


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SALES_RETURN_MAST.SR_ID}=" + Srno + "";

            CrystalReportViewer1.ReportSource = myRep;


            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    #region Purchase Return Report
    private void PurchaseReturn(string ReportName, string Prno)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_PURCHASE_RETURN_DET", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_PURCHASE_RETURN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_PURCHASE_RETURN_DET");
            da1.Fill(rds, "YANTRA_PURCHASE_RETURN_MAST");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_PURCHASE_RETURN_DET.PR_DET_ID}=" + Prno + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    private void AmendmnetReport(string ReportName, string AmeId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from Amendment_tbl", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Amendment_Det_tbl Order by Ame_Det_ID asc", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Amendment_tbl");
            da1.Fill(rds, "Amendment_DET_tbl");
            da2.Fill(rds, "YANTRA_SO_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da4.Fill(rds, "YANTRA_SO_DET");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_ITEM_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da9.Fill(rds, "Terms_Conditions");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{Amendment_tbl.Amendment_Id}=" + AmeId + "";
            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    private void QRReport_Proc(string ReportName, string ItemCode, string t1,string t2, string ID)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlCommand cmd = new SqlCommand("USP_StockReportNew_Serach_QRCODE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModelNo", ItemCode);
            cmd.Parameters.AddWithValue("@Qty", t2);
            cmd.Parameters.AddWithValue("@Qty1", t1);
            cmd.Parameters.AddWithValue("@ID", ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "USP_StockReportNew_Serach_QRCODE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = " " ;
            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {

        }
    }
    private void QRReport_ProcRoller(string ReportName, string t1, string t2, string ID)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlCommand cmd = new SqlCommand("USP_StockReportNew_Serach_QRCODE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ModelNo", ItemCode);
            cmd.Parameters.AddWithValue("@Qty", t2);
            cmd.Parameters.AddWithValue("@Qty1", t1);
            cmd.Parameters.AddWithValue("@ID", ID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "USP_StockReportNew_Serach_QRCODE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = " ";
            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {

        }
    }
    private void QRReport_Proc1(string ReportName, string t1, string t2, string ID, string QRId)
    {
       // ID = ID.Replace("'", "''");

        //string hai = ID;

        //hai.Replace

        //StringBuilder sb = new StringBuilder(ID);
        //sb.Replace("pha", "'");
        //sb.Replace("jam", "'");

        //string hai = sb.ToString();

        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlCommand cmd = new SqlCommand("USP_StockReportNew_Serach_QRCODE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ModelNo", ItemCode);
            cmd.Parameters.AddWithValue("@Qty", t2);
            cmd.Parameters.AddWithValue("@Qty1", t1);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@QRID", QRId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "USP_StockReportNew_Serach_QRCODE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = " ";
            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {

        }
    }

    #region QR Report
    private void QRReport(string ReportName, string QRid)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from QRCODE_IMage", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_CHECKING_FORMAT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_CHECKING_FORMAT", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_COLOR_mAST", DBConString.ConnectionString());
             YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "QRCODE_IMage");
            da1.Fill(rds, "YANTRA_CHECKING_FORMAT_DETAILS");
            da2.Fill(rds, "YANTRA_CHECKING_FORMAT");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_LKUP_COLOR_mAST");
            
            //da9.Fill(rds, "Terms_Conditions");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_CHECKING_FORMAT_DETAILS.CHK_DET_id}=" + QRid + "";
            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region QR Report Label
    private void QRReport_Label(string ReportName, string Qty)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from QRCODE_IMage Order by Id", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_CHECKING_FORMAT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_CHECKING_FORMAT", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_COLOR_mAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM Inward_New", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Exec RepeatLabel "+Qty+"", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("select * from NumbersTable  (1,"+Qty+",1)", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "QRCODE_IMage");
            da1.Fill(rds, "YANTRA_CHECKING_FORMAT_DETAILS");
            da2.Fill(rds, "YANTRA_CHECKING_FORMAT");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_LKUP_COLOR_mAST");
            da5.Fill(rds, "Inward_New");

            da6.Fill(rds, "RepeatLabel");
            da7.Fill(rds, "NumbersTable");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "  cross table dbo.NumbersTable(1, "+Qty+", 1) ";
            myRep.RecordSelectionFormula = " ";

            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region PI Report
    private void PIReport(string ReportName, string PIid)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_PI_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_PI_DET Order by PI_DET_ID asc", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * from Terms_Conditions_Selected", DBConString.ConnectionString());
            //SqlDataAdapter da9 = new SqlDataAdapter("SELECT * FROM Terms_Conditions", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_PI_MAST");
            da1.Fill(rds, "YANTRA_PI_DET");
            da2.Fill(rds, "YANTRA_SO_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da4.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_ITEM_MAST");
            da7.Fill(rds, "YANTRA_LKUP_UOM");
            //da8.Fill(rds, "Terms_Conditions_Selected");
            //da9.Fill(rds, "Terms_Conditions");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_PI_MAST.PI_ID}=" + PIid + "";
            CrystalReportViewer1.ReportSource = myRep;
            //CrystalReportViewer1.ReportSource = myRep;
            CrystalReportViewer1.RefreshReport();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region ProformaInvoice Report
    private void PInvoice(string ReportName, string PIid)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SUP_QUOT_DET", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_SUP_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_SUP_QUOT_DET WHERE SUP_QUOT_ID=" + PIid + ")", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SUP_ENQ_MAST", DBConString.ConnectionString()); //
            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_SUP_QUOT_DET");
            da1.Fill(rds, "YANTRA_SUP_QUOT_MAST");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//

            da4.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da5.Fill(rds, "YANTRA_SUP_ENQ_MAST");//
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SUP_QUOT_DET.SUP_QUOT_ID}=" + PIid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region SupenqDet Report
    private void Supenqdet(string ReportName, string SupDetid)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_INDENT_APPROVAL_DET", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_INDENT_APPROVAL_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_INDENT_APPROVAL_DET WHERE IND_APPRL_ID=" + SupDetid + ")", DBConString.ConnectionString()); //
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString()); //
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_INDENT_APPROVAL_DET");
            da1.Fill(rds, "YANTRA_INDENT_APPROVAL_MAST");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_LKUP_COLOR_MAST");//
            da5.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da6.Fill(rds, "YANTRA_COMP_PROFILE");//

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_INDENT_APPROVAL_DET.IND_APPRL_ID}=" + SupDetid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region p2 Report
    private void p2(string ReportName, string SupDetid)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_DET Order by FPO_DET_ID ", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_FIXED_PO_DET WHERE FPO_ID=" + SupDetid + ")", DBConString.ConnectionString()); //
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString()); //
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_FIXED_PO_DET");
            da1.Fill(rds, "YANTRA_FIXED_PO_MAST");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_LKUP_COLOR_MAST");//
            da5.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da6.Fill(rds, "YANTRA_COMP_PROFILE");//

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_DET.FPO_ID}=" + SupDetid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region ItemMasterQty Report
    private void ItemMasterQty(string ReportName, string cpid, string gdid, string brandid, string MdNo)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_QTY", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_GODOWN", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString()); //
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_ITEM_QTY");
            da1.Fill(rds, "YANTRA_COMP_PROFILE");
            da2.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_LKUP_GODOWN");
            da5.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");//
            da6.Fill(rds, "YANTRA_LKUP_COLOR_MAST");//


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_ITEM_MAST.ITEM_CODE}= {YANTRA_ITEM_QTY.ITEM_CODE}";

            if (gdid != "0" && brandid != "0" && cpid != "0" && MdNo != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + "";
            }
            else if (gdid != "0" && brandid != "0" && cpid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + " ";
            }
            else if (gdid != "0" && brandid != "0" && MdNo != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + "";
            }
            else if (cpid != "0" && brandid != "0" && MdNo != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + "";
            }
            else if (brandid != "0" && MdNo != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + "";
            }
            else if (brandid != "0" && cpid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + "";
            }
            else if (brandid != "0" && gdid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + " AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + "";
            }
            else if (MdNo != "0" && cpid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + "";
            }
            else if (gdid != "0" && cpid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + " ";
            }
            else if (gdid != "0" && MdNo != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + " ";
            }

            else if (cpid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + " AND {YANTRA_ITEM_QTY.CP_ID}=" + cpid + "";
            }

            else if (gdid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "AND {YANTRA_ITEM_QTY.GODOWN_ID}=" + gdid + "";
            }
            else if (brandid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brandid + "";
            }
            else if (MdNo != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_QTY.ITEM_CODE}=" + MdNo + "";
            }
            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Indent Report
    private void IndentReport(string ReportName, string Indno)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_INDENT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_INDENT_DET", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString()); //

            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());

            SqlDataAdapter da15 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_INDENT_MAST");
            da1.Fill(rds, "YANTRA_INDENT_DET");
            da2.Fill(rds, "YANTRA_DEPT_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            //da4.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            //da5.Fill(rds, "YANTRA_QUOT_MAST");//
            //da6.Fill(rds, "YANTRA_QUOT_DET");
            //da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            //da8.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da10.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            //da11.Fill(rds, "YANTRA_CUSTOMER_DET");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da13.Fill(rds, "YANTRA_COMP_PROFILE");
            da14.Fill(rds, "YANTRA_LKUP_UOM");

            da15.Fill(rds, "YANTRA_LKUP_COLOR_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_INDENT_DET.IND_ID}=" + Indno + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Indent Report
    private void IndentReqReport(string ReportName, string Indno)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_INDENT_REQUEST_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_INDENT_REQUEST_DET", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString()); //

            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST_1", DBConString.ConnectionString());
            //SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());

            SqlDataAdapter da15 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_INDENT_REQUEST_MAST");
            da1.Fill(rds, "YANTRA_INDENT_REQUEST_DET");
            da2.Fill(rds, "YANTRA_DEPT_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            //da4.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            //da5.Fill(rds, "YANTRA_QUOT_MAST");//
            //da6.Fill(rds, "YANTRA_QUOT_DET");
            //da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            //da8.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da10.Fill(rds, "YANTRA_EMPLOYEE_MAST_1");
            //da11.Fill(rds, "YANTRA_CUSTOMER_DET");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da13.Fill(rds, "YANTRA_COMP_PROFILE");
            //da14.Fill(rds, "YANTRA_LKUP_UOM");

            da15.Fill(rds, "YANTRA_LKUP_COLOR_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_INDENT_REQUEST_DET.IND_ID}=" + Indno + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion
    #region Indent Report
    private void IndentApprovalReport(string ReportName, string Indno)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_INDENT_APPROVAL_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_INDENT_APPROVAL_DET", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString()); //
            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_INDENT_APPROVAL_MAST");
            da1.Fill(rds, "YANTRA_INDENT_APPROVAL_DET");
            da2.Fill(rds, "YANTRA_DEPT_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            //da4.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            //da5.Fill(rds, "YANTRA_QUOT_MAST");//
            //da6.Fill(rds, "YANTRA_QUOT_DET");
            //da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            //da8.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da10.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            //da11.Fill(rds, "YANTRA_CUSTOMER_DET");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_INDENT_APPROVAL_DET.IND_APPRL_ID}=" + Indno + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region TourClaim Report
    private void TourClaim(string ReportName, string SupDetid)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from Tour_Expanses", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tour_DailAllowances", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Tour_LocalConveyance", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from Tour_LodgingExpanses", DBConString.ConnectionString()); //
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from Tour_TravelExpanses", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString()); //
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString()); //
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString()); //
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString()); //




            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "Tour_Expanses");
            da1.Fill(rds, "Tour_DailAllowances");
            da2.Fill(rds, "Tour_LocalConveyance");
            da3.Fill(rds, "Tour_LodgingExpanses");
            da4.Fill(rds, "Tour_TravelExpanses");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_DEPT_MAST");
            da7.Fill(rds, "YANTRA_DESG_MAST");
            da8.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{Tour_Expanses.TourId}=" + SupDetid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region CV Report
    private void CV(string ReportName, string SupDetid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from Convenience_Voucher_tbl", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Convenience_Voucher_Det_tbl order by On_Date ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString()); //




            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "Convenience_Voucher_tbl");
            da1.Fill(rds, "Convenience_Voucher_Det_tbl");
            da2.Fill(rds, "YANTRA_COMP_PROFILE");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");//




            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{Convenience_Voucher_Det_tbl.Id}=" + SupDetid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region SiteInspection Report

    private void Inspection(string ReportName, string ClientId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from Site_Inspection_Report_tbl", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Site_Inspection_Details_tbl", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Site_Inspection_Report_tbl");
            da1.Fill(rds, "Site_Inspection_Details_tbl");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{Site_Inspection_Details_tbl.Client_Id}=" + ClientId + "";
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Compare Sales Quotation Report
    private void CompareSalesQuotationReport(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET where ITEM_TYPE ='Original' order by Quot_OrderNo", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_QUOT_OPT_DET", DBConString.ConnectionString());

            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from location_tbl", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_QUOT_MAST");
            da5.Fill(rds, "YANTRA_QUOT_DET");//
            da6.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da7.Fill(rds, "YANTRA_ITEM_IMAGE");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_QUOT_OPT_DET");

            da10.Fill(rds, "YANTRA_COMP_PROFILE");
            da11.Fill(rds, "location_tbl");


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_DET.QUOT_ID}=" + QuotId + "   ";
                // myRep.SummaryInfo.ReportTitle = "Quotation -" + QuotId+  ""  ;
                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Internal Indent Report
    private void InternalIndentReport(string ReportName, string Indno)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from INTERNAL_INDENT", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from INTERNAL_INDENT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from warehouse_tbl", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM INTERNAL_INDENT_DETAILS WHERE INT_INDID=" + Indno + ")", DBConString.ConnectionString()); //

            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from yantra_Lkup_Color_Mast", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "INTERNAL_INDENT");
            da1.Fill(rds, "INTERNAL_INDENT_DETAILS");
            da2.Fill(rds, "warehouse_tbl");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            //da4.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            //da5.Fill(rds, "YANTRA_QUOT_MAST");//
            //da6.Fill(rds, "YANTRA_QUOT_DET");
            //da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            //da8.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da9.Fill(rds, "YANTRA_COMP_PROFILE");
            //da10.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da11.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da13.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{INTERNAL_INDENT_DETAILS.INT_INDID}=" + Indno + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion




    #region Sales QuotationWithoutModelNo Report
    private void SalesQuotationReportWithoutModelNo(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));


            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());

            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());

            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//

            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by Quot_OrderNo", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());

            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());//
            SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());//

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da5.Fill(rds, "YANTRA_QUOT_MAST");//
            da6.Fill(rds, "YANTRA_QUOT_DET");
            da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da8.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da10.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da11.Fill(rds, "YANTRA_CUSTOMER_DET");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da13.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");//
            da14.Fill(rds, "YANTRA_LKUP_COLOR_MAST");//


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_ID}=" + QuotId + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Sales Quotation Report
    private void SalesQuotationReport(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));


            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());

            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());

            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//

            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by Quot_OrderNo", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());

            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());//
            SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da15 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE", DBConString.ConnectionString());//

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da5.Fill(rds, "YANTRA_QUOT_MAST");//
            da6.Fill(rds, "YANTRA_QUOT_DET");
            da7.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da8.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da10.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da11.Fill(rds, "YANTRA_CUSTOMER_DET");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");//
            da13.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");//
            da14.Fill(rds, "YANTRA_LKUP_COLOR_MAST");//
            da15.Fill(rds, "YANTRA_ITEM_IMAGE");//

            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_ID}=" + QuotId + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region New Sales Quotation Report
    private void NewSalesQuotationReport_Pert(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST with(nolock) where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST with(nolock) where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")   ", DBConString.ConnectionString());

            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST  where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")   ", DBConString.ConnectionString());


            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by  Quot_OrderNo asc ", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());//
            //SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE with(nolock) where Item_Code IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + "   )", DBConString.ConnectionString());

            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE  where Item_Code IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + "   )", DBConString.ConnectionString());

            // SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());//
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());//
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());//

            SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());//
            SqlDataAdapter da15 = new SqlDataAdapter("Select * from location_tbl", DBConString.ConnectionString());//
            SqlDataAdapter da16 = new SqlDataAdapter("select * from Terms_Conditions", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_QUOT_MAST");
            da5.Fill(rds, "YANTRA_QUOT_DET");//
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da9.Fill(rds, "YANTRA_ITEM_IMAGE");
            da10.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            da11.Fill(rds, "YANTRA_DEPT_MAST");
            da12.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da13.Fill(rds, "YANTRA_DESG_MAST");

            da14.Fill(rds, "YANTRA_COMP_PROFILE");
            da15.Fill(rds, "location_tbl");
            da16.Fill(rds, "Terms_Conditions");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_DET.QUOT_ID}=" + QuotId + "";
                // myRep.SummaryInfo.ReportTitle = "Quotation -" + QuotId+  ""  ;
                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region New Sales Quotation Report
    private void NewSalesQuotationReport(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST with(nolock) where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST with(nolock) where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")   ", DBConString.ConnectionString());

            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST  where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")   ", DBConString.ConnectionString());


            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by  Quot_OrderNo asc ", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());//
            //SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE with(nolock) where Item_Code IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + "   )", DBConString.ConnectionString());

            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE  where Item_Code IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + "   )", DBConString.ConnectionString());
           
            // SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());//
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());//
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());//

            SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());//
            SqlDataAdapter da15 = new SqlDataAdapter("Select * from location_tbl", DBConString.ConnectionString());//
            SqlDataAdapter da16 = new SqlDataAdapter("select * from Terms_Conditions", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_QUOT_MAST");
            da5.Fill(rds, "YANTRA_QUOT_DET");//
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da9.Fill(rds, "YANTRA_ITEM_IMAGE");
            da10.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            da11.Fill(rds, "YANTRA_DEPT_MAST");
            da12.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da13.Fill(rds, "YANTRA_DESG_MAST");

            da14.Fill(rds, "YANTRA_COMP_PROFILE");
            da15.Fill(rds, "location_tbl");
            da16.Fill(rds, "Terms_Conditions");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_DET.QUOT_ID}=" + QuotId + "";
                // myRep.SummaryInfo.ReportTitle = "Quotation -" + QuotId+  ""  ;
                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    #region New Tech Sales Quotation Report
    private void NewTechSalesQuotationReport(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by Quot_OrderNo ", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE where Item_Code IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_ITEM_SPECIFICATION_IMAGE", DBConString.ConnectionString());//

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_QUOT_MAST");
            da5.Fill(rds, "YANTRA_QUOT_DET");//
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da9.Fill(rds, "YANTRA_ITEM_IMAGE");
            da10.Fill(rds, "YANTRA_ITEM_SPECIFICATION_IMAGE");


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_DET.QUOT_ID}=" + QuotId + "   ";
                // myRep.SummaryInfo.ReportTitle = "Quotation -" + QuotId+  ""  ;
                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion








    #region 2New Sales Quotation Report
    private void NewSalesQuotationReport2(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by Quot_OrderNo ", DBConString.ConnectionString());
            //  SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());//
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE where Item_Code IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());//

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");//
            da4.Fill(rds, "YANTRA_QUOT_MAST");
            da5.Fill(rds, "YANTRA_QUOT_DET");//
            //  da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da9.Fill(rds, "YANTRA_ITEM_IMAGE");
            da10.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_ID}=" + QuotId + "   ";
                // myRep.SummaryInfo.ReportTitle = "Quotation -" + QuotId+  ""  ;
                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region POAIR
    private void POAIR(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from PObyAir", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "PObyAir");
            da2.Fill(rds, "YANTRA_COMP_PROFILE");



            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{PObyAir.Id}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region POSEA
    private void POSEA(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from PObySea", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "PObySea");
            da2.Fill(rds, "YANTRA_COMP_PROFILE");



            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{PObySea.Id}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region
    private void ESISheet(string ReportName, string CPid, string e, string year)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from V_ToGenerate_PaySlip", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  where STATUS !=0 order by EMP_ID asc", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select  * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from Location_tbl", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "V_ToGenerate_PaySlip");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_COMP_PROFILE");
            da4.Fill(rds, "Location_tbl");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.CP_ID}=" + CPid + "  and {V_ToGenerate_PaySlip.Month} = " + e + " and {V_ToGenerate_PaySlip.Year} = " + year + " ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region
    private void PFSheet(string ReportName, string CPid, string e, string year)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from V_ToGenerate_PaySlip", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  where STATUS !=0 order by EMP_ID asc", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select  * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from Location_tbl", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "V_ToGenerate_PaySlip");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_COMP_PROFILE");
            da4.Fill(rds, "Location_tbl");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.CP_ID}=" + CPid + "  and {V_ToGenerate_PaySlip.Month} = " + e + " and {V_ToGenerate_PaySlip.Year} = " + year + " ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region
    private void BankStatement(string ReportName, string CPid, string e, string year)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from V_ToGenerate_PaySlip", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  where STATUS !=0 order by EMP_ID asc", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select  * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from Location_tbl", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "V_ToGenerate_PaySlip");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_COMP_PROFILE");
            da4.Fill(rds, "Location_tbl");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.CP_ID}=" + CPid + "  and {V_ToGenerate_PaySlip.Month} = " + e + " and {V_ToGenerate_PaySlip.Year} = " + year + " ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    private void Payshet(string ReportName, string CPid, string e, string year)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from V_ToGenerate_PaySlip", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST where STATUS !=0 and Asset4!='Exclude Paysheet' ", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET Order by EMP_DET_DOJ asc", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from Location_tbl", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "V_ToGenerate_PaySlip");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_COMP_PROFILE");
            da4.Fill(rds, "Location_tbl");
            da5.Fill(rds,"YANTRA_EMPLOYEE_DET");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.CP_ID}=" + CPid + "  and {V_ToGenerate_PaySlip.Month} = " + e + " and {V_ToGenerate_PaySlip.Year} = " + year + " ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #region PaySlip
    private void PaySlip(string ReportName, string siid, string e, string year)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from V_ToGenerate_PaySlip", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET where EMP_DET_DOT >=GETDATE ()", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "V_ToGenerate_PaySlip");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");
            da6.Fill(rds, "YANTRA_COMP_PROFILE");

            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{V_ToGenerate_PaySlip.EMP_ID}=" + siid + "  and {V_ToGenerate_PaySlip.Month} = " + e + " and {V_ToGenerate_PaySlip.Year} = " + year + " ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region PaySlip_Old
    private void PaySlip_Old(string ReportName, string siid, string e)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from V_PaySlip", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "V_PaySlip");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");
            da6.Fill(rds, "YANTRA_COMP_PROFILE");

            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{V_PaySlip.EMP_ID}=" + siid + "  and {V_PaySlip.Month} = " + e + " ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion




    #region oNdUTyform
    private void OnDutyForm(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ONDUTY_FORM", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_ONDUTY_FORM");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");

            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_ONDUTY_FORM.OnDuty_ID}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region 1-Hour
    private void OneHOurPermission(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ONE_HOUR_PERMISSION", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_REGION", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_ONE_HOUR_PERMISSION");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");
            da6.Fill(rds, "YANTRA_LKUP_REGION");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_ONE_HOUR_PERMISSION.One_Hour_ID}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region OVerTime App
    private void OverTime(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_OVER_TIME_FORM", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_REGION", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_OVER_TIME_FORM");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");
            da6.Fill(rds, "YANTRA_LKUP_REGION");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_OVER_TIME_FORM.Overtime_ID}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Shift Change App
    private void ShiftChange(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_EMP_SHIFT_CHANGE_FORM", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_REGION", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_EMP_SHIFT_CHANGE_FORM");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");
            da6.Fill(rds, "YANTRA_LKUP_REGION");
            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_EMP_SHIFT_CHANGE_FORM.Shift_Change_ID}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region TicketDetails
    private void TicketDetails(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_TICKET_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_TICKET_DETAILS");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da4.Fill(rds, "YANTRA_DEPT_MAST");
            da5.Fill(rds, "YANTRA_DESG_MAST");

            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_TICKET_DETAILS.TicketDetails_Id}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion








    #region Mobile Advance
    private void MobileAdvance(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Staff_Mobile_Advance_tbl", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "Staff_Mobile_Advance_tbl");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");//
            da4.Fill(rds, "YANTRA_DESG_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");//


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{Staff_Mobile_Advance_tbl.Id}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Salary Advance
    private void SalaryAdvance(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Staff_Salary_Advance_Request_tbl", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "Staff_Salary_Advance_Request_tbl");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");//
            da4.Fill(rds, "YANTRA_DESG_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");//


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{Staff_Salary_Advance_Request_tbl.Sal_Adv_Id}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region TOur Advance
    private void TourAdvance(string ReportName, string siid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Staff_Tour_Advance_tbl", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "Staff_Tour_Advance_tbl");
            da2.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_DET");//
            da4.Fill(rds, "YANTRA_DESG_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");//


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{Staff_Tour_Advance_tbl.ID}=" + siid + "   ";

                CrystalReportViewer1.ReportSource = myRep;
                CrystalReportViewer1.RefreshReport();
            }
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
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ENQ_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
            da4.Fill(rds, "YANTRA_ENQ_DET");



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

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SOId + ")", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da6.Fill(rds, "YANTRA_QUOT_MAST");
            da7.Fill(rds, "YANTRA_SO_MAST");
            da8.Fill(rds, "YANTRA_SO_DET");
            da9.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da10.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da11.Fill(rds, "YANTRA_COMP_PROFILE");
            da12.Fill(rds, "YANTRA_EMPLOYEE_MAST");
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



    #region Statement Po Report
    private void StatementPo(string ReportName, string SOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SOId + ")", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_PAYMENTS_RECEIVED", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da1.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_SO_MAST");
            da3.Fill(rds, "YANTRA_SO_DET");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_PAYMENTS_RECEIVED");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_DET.SO_ID}=" + SOId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    #region Sales Order Acceptance Report
    private void SalesOrderAcceptanceReport(string ReportName, string SOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SOId + ")", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMP_PROFILE");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da6.Fill(rds, "YANTRA_QUOT_MAST");
            da7.Fill(rds, "YANTRA_SO_MAST");
            da8.Fill(rds, "YANTRA_SO_DET");
            da9.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da10.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");


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

    #region Work Order Report
    private void WorkOrderReport(string ReportName, string WOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());

            // SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_OA_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_WO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_WO_DET where WO_ID =" + WOId + ") ", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select top 1 * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //  SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //  SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_OA_DET", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_WO_DET", DBConString.ConnectionString());
            SqlDataAdapter da14 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da15 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da16 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            // da4.Fill(rds, "YANTRA_OA_MAST");
            da5.Fill(rds, "YANTRA_WO_MAST");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            //da8.Fill(rds, "YANTRA_COMP_PROFILE");
            //  da8.Fill(rds, "YANTRA_SO_DET");
            da9.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da10.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            // da11.Fill(rds, "YANTRA_OA_DET");
            da12.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da13.Fill(rds, "YANTRA_WO_DET");
            da14.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da15.Fill(rds, "YANTRA_LKUP_UOM");
            da16.Fill(rds, "YANTRA_COMP_PROFILE");
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
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_CLAIM_FORM_PROD_DET  where CF_ID =" + CFId + ") ", DBConString.ConnectionString());
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

    #region FE Order Profile Report
    private void FEOrderProfile(string ReportName, string FEOPID)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_FE_ORDER_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_FE_ORDER_PROFILE_PRODUCT_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_FE_ORDER_PROFILE_BUYER_DET", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            //SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_FE_ORDER_PROFILE");
            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da3.Fill(rds, "YANTRA_CUSTOMER_DET");
            da4.Fill(rds, "YANTRA_FE_ORDER_PROFILE_PRODUCT_DET");
            da5.Fill(rds, "YANTRA_ITEM_MAST");
            da6.Fill(rds, "YANTRA_FE_ORDER_PROFILE_BUYER_DET");
            da7.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da10.Fill(rds, "YANTRA_COMP_PROFILE");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_FE_ORDER_PROFILE.FEOP_ID}=" + FEOPID + "";
            CrystalReportViewer1.ReportSource = myRep;
            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Claim Form");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    /////////////////////////// HR

    #region Statement2 Po Report
    private void StatementPo2(string ReportName, string SOId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            // SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SOId + ")", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_SALES_RETURN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_SALES_RETURN_DET", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());



            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da1.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_SO_MAST");
            da3.Fill(rds, "YANTRA_SO_DET");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da8.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da9.Fill(rds, "YANTRA_SALES_RETURN_MAST");
            da10.Fill(rds, "YANTRA_SALES_RETURN_DET");
            da11.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da12.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_DET.SO_ID}=" + SOId + "";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    /////////////////////////////////////SCM

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
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_FIXED_PO_DET where FPO_ID = " + FPOId + ") ", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_FIXED_PO_MAST");
            da2.Fill(rds, "YANTRA_FIXED_PO_DET");
            da3.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da4.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            //  da5.Fill(rds, "YANTRA_LKUP_UOM");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            // da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da10.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_MAST.SUP_ID} = {YANTRA_SUPPLIER_MAST.SUP_ID} and {YANTRA_LKUP_DESP_MODE.DESPM_ID} = {YANTRA_FIXED_PO_MAST.DESPM_ID} and {YANTRA_FIXED_PO_DET.FPO_ID}={YANTRA_FIXED_PO_MAST.FPO_ID} AND {YANTRA_FIXED_PO_DET.ITEM_CODE} = {YANTRA_ITEM_MAST.ITEM_CODE} AND {YANTRA_FIXED_PO_DET.FPO_ID} =" + FPOId + "";
            myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_mast.FPO_ID} =" + FPOId + "";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region SelfPurchase Order Report
    private void SelfPurchaseOrder(string ReportName, string FPOId)
    {
        try
        {
            myRep.Load(Server.MapPath("SCM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from SELF_PO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from SELF_PO_MASTER_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from SELF_PO_MASTER_DET where FPOS_ID = " + FPOId + ") ", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from DeliveryAddress_Master", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "SELF_PO_MAST");
            da2.Fill(rds, "SELF_PO_MASTER_DET");
            da3.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da4.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            //  da5.Fill(rds, "YANTRA_LKUP_UOM");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            // da8.Fill(rds, "YANTRA_COMP_PROFILE");
            // da9.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da10.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da11.Fill(rds, "YANTRA_COMP_PROFILE");
            da12.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
            da13.Fill(rds, "DeliveryAddress_Master");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_MAST.SUP_ID} = {YANTRA_SUPPLIER_MAST.SUP_ID} and {YANTRA_LKUP_DESP_MODE.DESPM_ID} = {YANTRA_FIXED_PO_MAST.DESPM_ID} and {YANTRA_FIXED_PO_DET.FPO_ID}={YANTRA_FIXED_PO_MAST.FPO_ID} AND {YANTRA_FIXED_PO_DET.ITEM_CODE} = {YANTRA_ITEM_MAST.ITEM_CODE} AND {YANTRA_FIXED_PO_DET.FPO_ID} =" + FPOId + "";
            myRep.RecordSelectionFormula = "{SELF_PO_MAST.FPOS_ID} =" + FPOId + "";

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
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SUP_WO_DET where SUP_WO_ID = " + SupWoId + ")", DBConString.ConnectionString());
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
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_CHECKING_FORMAT_DETAILS where CHK_ID =" + CHKId + ") ", DBConString.ConnectionString());

            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CHECKING_FORMAT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_GODOWN", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CHECKING_FORMAT");
            da1.Fill(rds, "YANTRA_ITEM_MAST");

            //da2.Fill(rds, "YANTRA_COMP_PROFILE");
            da2.Fill(rds, "YANTRA_CHECKING_FORMAT_DETAILS");
            da3.Fill(rds, "YANTRA_LKUP_GODOWN");
            da4.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_LKUP_UOM");
            da7.Fill(rds, "YANTRA_LKUP_COLOR_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_CHECKING_FORMAT_DETAILS.CHK_ID}=" + CHKId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    ////////////////////////////////////Inventory

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
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SALES_INVOICE_DET where SI_ID =" + SIId + ") ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());

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
            da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da12.Fill(rds, "YANTRA_CUSTOMER_DET");
            da13.Fill(rds, "YANTRA_SO_DET");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_DET.SI_ID}=" + SIId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Sales Invoice Report
    private void SalesInvoiceNewReport(string ReportName, string SIId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            //SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SALES_INVOICE_DET where SI_ID =" + SIId + ") ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from V_INVOICE_HSN_TAX", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            //da.Fill(rds, "YANTRA_ENQ_MAST");
            //da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da5.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            //da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da12.Fill(rds, "YANTRA_LKUP_UOM");
            da13.Fill(rds, "V_INVOICE_HSN_TAX");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_DET.SI_ID}=" + SIId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Cash Invoice Report
    private void CashInvoiceReport(string ReportName, string SIId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            //  SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SALES_INVOICE_DET where SI_ID =" + SIId + ") ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            // SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            // da.Fill(rds, "YANTRA_ENQ_MAST");
            //da2.Fill(rds, "YANTRA_QUOT_MAST");
            // da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da5.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da12.Fill(rds, "YANTRA_CUSTOMER_DET");
            // da13.Fill(rds, "YANTRA_SO_DET");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_DET.SI_ID}=" + SIId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    #region Spares Invoice Report
    private void SparesInvoiceReport(string ReportName, string SIId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SPARES_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SPARES_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da2.Fill(rds, "YANTRA_SPARES_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SPARES_ORDER_MAST");
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
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from  YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_DELIVERY_CHALLAN_DET where DC_ID=" + DCId + ")", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da5.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da10.Fill(rds, "YANTRA_CUSTOMER_DET");
            da11.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da12.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da13.Fill(rds, "YANTRA_LKUP_UOM");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_DELIVERY_CHALLAN_DET.DC_ID}=" + DCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    private void InternoIn(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from Interno_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Interno_DET Order by DetId asc", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Interno_MAST");
            da2.Fill(rds, "Interno_DET");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{Interno_MAST.ID}=" + DCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {

        }
    }
    private void InternoPL(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from Interno_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Interno_DET Order by DetId asc", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Interno_MAST");
            da2.Fill(rds, "Interno_DET");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{Interno_MAST.ID}=" + DCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {

        }
    }

    #region Dispatch Details Report
    private void DispatchDetailsReport(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            //SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from Dispatch", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from  Dispatch_Details order by Dispatch_Details.Dispatch_DetId  asc", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_DELIVERY_CHALLAN_DET where DC_ID=" + DCId + ")", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //// SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            // SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            // SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da13 = new SqlDataAdapter("select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where Status=1 ", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            //da.Fill(rds, "YANTRA_ENQ_MAST");
            //da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "Dispatch");
            da5.Fill(rds, "Dispatch_Details");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            //da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da10.Fill(rds, "YANTRA_ITEM_MAST");
            // da10.Fill(rds, "YANTRA_CUSTOMER_DET");
            // da11.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            // da12.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            // da13.Fill(rds, "YANTRA_LKUP_UOM");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{Dispatch_Details.DispatchId}=" + DCId + "  ";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion
    #region Credit Approval Report
    private void CreditApprovalReport(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Credit_Approval_tbl", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Dispatch", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from Yantra_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da1.Fill(rds, "Credit_Approval_tbl");
            da2.Fill(rds, "Dispatch");
            da3.Fill(rds, "Yantra_SO_MAST");
            da4.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da5.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_COMP_PROFILE");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{Credit_Approval_tbl.Dispatch_Id}=" + DCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion




    #region Delivery Challan Sample Report
    private void DeliveryChallanReportForsample(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from  YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_DELIVERY_CHALLAN_DET where DC_ID=" + DCId + " )", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();


            da1.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da2.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da3.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da7.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da8.Fill(rds, "YANTRA_LKUP_UOM");
            da9.Fill(rds, "YANTRA_CUSTOMER_UNITS");

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



    #region  Moving Delivery Challan Report
    private void MovingDeliveryChallan(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da1 = new SqlDataAdapter("Select * from STOCKMOVEMENT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from  StockMovement_Master", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from warehouse_tbl", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from STOCKMOVEMENT_DETAILS where SM_DC_ID=" + DCId + ")", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from INTERNAL_INDENT", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from V_Warehouse_Tbl", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "STOCKMOVEMENT_DETAILS");
            da2.Fill(rds, "StockMovement_Master");
            da3.Fill(rds, "warehouse_tbl");
            da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "INTERNAL_INDENT");
            da9.Fill(rds, "V_Warehouse_Tbl");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{STOCKMOVEMENT_DETAILS.SM_DC_ID}=" + DCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region  Insurance Report
    private void Insurance(string ReportName, string Id)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Insurance_Form_tbl", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "Insurance_Form_tbl");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{Insurance_Form_tbl.Ins_Id}=" + Id + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  HR Report
    private void HR(string ReportName)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "YANTRA_EMPLOYEE_MAST");

            myRep.SetDataSource(rds);
            // myRep.RecordSelectionFormula = "{Insurance_Form_tbl.Ins_Id}=" + Id + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    #region Sales Invoice Statement Report
    private void SalesInvoiceStatementReport(string ReportName, string SIId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_PAYMENTS_RECEIVED", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SIId + ")", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //  SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //  SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_PAYMENTS_RECEIVED");
            // da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            //da4.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            //da5.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da9.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            // da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            // da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            //  da12.Fill(rds, "YANTRA_CUSTOMER_DET");
            da13.Fill(rds, "YANTRA_SO_DET");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_ID}=" + SIId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region SalesStatement Of ACC Report
    private void SalesStatementOfACCReport(string ReportName, string SIId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_PAYMENTS_RECEIVED", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID = " + SIId + ")", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            // SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //  SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //  SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_SO_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_PAYMENTS_RECEIVED");
            // da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            //da4.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            //da5.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da9.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            // da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            // da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            //  da12.Fill(rds, "YANTRA_CUSTOMER_DET");
            da13.Fill(rds, "YANTRA_SO_DET");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_ID}=" + SIId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Spares Delivery Challan Report
    private void SparesDeliveryChallanReport(string ReportName, string DCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SPARES_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SPARES_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from  YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da2.Fill(rds, "YANTRA_SPARES_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SPARES_ORDER_MAST");
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


    /////////////////////////////////////Services

    #region Services Report
    private void ServiceReport(string ReportName, string SRId, string SRId2)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_SERVICE_REPORT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SERVICE_REPORT_DET", DBConString.ConnectionString());




            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da4.Fill(rds, "YANTRA_CUSTOMER_DET");
            da5.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da6.Fill(rds, "YANTRA_SERVICE_REPORT_MAST");
            da7.Fill(rds, "YANTRA_SERVICE_REPORT_DET");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SERVICE_REPORT_MAST.SR_ID}=" + SRId + "" + "and {YANTRA_SERVICE_REPORT_DET.SRDET_DET_ID}=" + SRId2 + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Warranty Claim Report
    private void WarrantyClaimReport(string ReportName, string WCId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_WARRANTY_CLAIM", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da4.Fill(rds, "YANTRA_WARRANTY_CLAIM");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_WARRANTY_CLAIM.WC_ID}=" + WCId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region AMC Invoice Report
    private void AMCInvoiceReport(string ReportName, string AMCInId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_AMC_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_AMC_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_AMC_INVOICE_DET where AMCI_ID = " + AMCInId + " )", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_AMC_INVOICE_MAST");
            da2.Fill(rds, "YANTRA_AMC_INVOICE_DET");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da6.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da7.Fill(rds, "YANTRA_AMC_ORDER_MAST");
            da8.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da9.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da10.Fill(rds, "YANTRA_CUSTOMER_DET");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_AMC_INVOICE_MAST.AMCI_ID}=" + AMCInId + "";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region AMC Quotation Report
    private void AMCQuotationReport(string ReportName, string AMCQuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));


            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_AMC_QUOTATION_DET where AMCQT_ID= " + AMCQuotId + ") ", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da5.Fill(rds, "YANTRA_AMC_QUOTATION_DET");
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da8.Fill(rds, "YANTRA_CUSTOMER_DET");
            da9.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_AMC_QUOTATION_MAST.AMCQT_ID}=" + int.Parse(AMCQuotId) + "";


            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Spares Quotation Report
    private void SparesQuotationReport(string ReportName, string SparesQuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));


            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SPARES_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SPARES_QUOT_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());

            //SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
            // SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_SPARES_QUOT_MAST");
            da5.Fill(rds, "YANTRA_SPARES_QUOT_DET");
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da8.Fill(rds, "YANTRA_CUSTOMER_DET");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SPARES_QUOT_MAST.SPARES_QUOT_ID}=" + int.Parse(SparesQuotId) + "";


            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region AMC Order Acceptance Report
    private void AMCAcceptanceReport(string ReportName, string AMCOrderId)
    {
        try
        {
            myRep.Load(Server.MapPath("Services/" + ReportName + ".rpt"));

            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_AMC_OA_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da1.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da2.Fill(rds, "YANTRA_AMC_ORDER_MAST");
            da3.Fill(rds, "YANTRA_AMC_OA_MAST");
            da4.Fill(rds, "YANTRA_AMC_ORDER_DET");
            da5.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da6.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
            da7.Fill(rds, "YANTRA_ITEM_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_AMC_OA_MAST.OA_ID}=" + AMCOrderId + "";


            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Sales Invoice Statement Report
    private void SalesInvoiceStatementReport_NotDelivered(string ReportName, string SIId, string det_id)
    {
        try
        {
            myRep.Load(Server.MapPath("Inventory/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_PAYMENTS_RECEIVED", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SO_MAST ", DBConString.ConnectionString());
            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET where SO_ID= " + SIId + ") ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_DET order by DC_DET_ID   asc", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            //  SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //  SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da13 = new SqlDataAdapter("Select * from YANTRA_SO_DET where item_code NOT IN (" + det_id + ")", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_PAYMENTS_RECEIVED");
            // da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            //da4.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            //da5.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da6.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da9.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            // da10.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            // da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            //  da12.Fill(rds, "YANTRA_CUSTOMER_DET");
            da13.Fill(rds, "YANTRA_SO_DET");
            myRep.SetDataSource(rds);




            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_ID}=" + SIId + "  ";
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    //protected void btnSend_Click(object sender, EventArgs e)
    //{

    //    //string returnFileName;

    //    if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Exported_Reports"))
    //    {
    //        //mailAttachPath = AppDomain.CurrentDomain.BaseDirectory + "mailattach\\" + lblEmpIdHidden.Text + "\\";
    //        //returnFileName = mailAttachPath + "Quotation-" + Request.QueryString["qnono"].ToString() + ".pdf";
    //     //   myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(AppDomain.CurrentDomain.BaseDirectory + "Exported_Reports\\report.pdf"));

    //        myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Exported_Reports/report.pdf"));



    //    }
    //    else
    //    {
    //        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Exported_Reports");
    //        myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(AppDomain.CurrentDomain.BaseDirectory + "\\Exported_Reports\\report.pdf"));

    //    }



    //  // myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("report.pdf"));


    //    //Ocrm.Classes.Email mail = new Ocrm.Classes.Email("srinivasgajula.mca@gmail.com");
    //    Yantra.Classes.Email mail = new Yantra.Classes.Email(txtTo.Text);

    //    mail.BodyFormat = System.Web.Mail.MailFormat.Html;

    //    System.IO.StreamReader sr = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Mail_Formats\\Appointments.htmx");
    //    mail.Body = sr.ReadToEnd();
    //    sr.Close();
    //    mail.Body = mail.Body.Replace("_Description_", " Report ");
    //    mail.Body = mail.Body.Replace("Fromname_", "N-Infosoft Pvt Ltd......");
    //    mail.Subject = "Export";
    //    System.Web.Mail.MailAttachment attachfiles = new System.Web.Mail.MailAttachment(Server.MapPath("../Reports/report.pdf"));
    //    mail.Attachments.Add(attachfiles);
    //    mail.From = "vltpvtltd@gmail.com";

    //    mail.Send();


    //}
}


