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


public partial class Modules_Reports_SMReportViewer : System.Web.UI.Page
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
            case "SupEnquiry":
                {
                    SuppliersEnqStmt("SupEnquiry", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString());
                    Page.Title = "Suppliers Enquiry Statement";
                    break;
                }
            case "ProformaInvoice":
                {
                    ProformaInvoiceStmt("CrystalReport2", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString(), Request.QueryString["Reg"].ToString());
                    Page.Title = "Customer Data Analysis";
                    break;
                }
            case "CustBrand":
                {
                    CustBrand("CustBrandAnalysis", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString(), Request.QueryString["Reg"].ToString());
                    Page.Title = "Customer Data Analysis";
                    break;
                }
            case "ProformaInvoiceBar":
                {
                    ProformaInvoiceStmtBar("BarGraphAnalysis", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString(), Request.QueryString["Reg"].ToString ());
                    Page.Title = "Customer Data Analysis";
                    break;
                }
            case "PurOrderStmt":
                {
                    PurchaseOrderStmt("PurOrderStmt", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString());
                    Page.Title = "Puchase Order Statement";
                    break;
                }
            case "ShipmentDet":
                {
                    ShipmentDetStmt("Shipmentdet", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["b"].ToString());
                    Page.Title = "Shipment Details Statement";
                    break;
                }
            case "PurInvStmt":
                {
                    PurInvoiceStmt("PurInvStmt", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString());
                    Page.Title = "Suppliers Enquiry Statement";
                    break;
                }
            case "goodsreceiptoftheday":
                {
                    GoodsReceiptOfTheDay("GoodsReceiptOfTheDay1", Request.QueryString["F"].ToString(), Request.QueryString["T"].ToString(), Request.QueryString["MRN"].ToString(), Request.QueryString["cmp"].ToString(), Request.QueryString["Brand"].ToString(), Request.QueryString["InvNo"].ToString());

                    //GoodsReceiptOfTheDay("GoodsReceiptOfTheDay1", Request.QueryString["date"].ToString());
                    Page.Title = "Goods Receipt Of The Day";
                    break;
                }
            case "despatchdetailsfortheday":
                {
                    DespatchDetailsForTheDay("DespatchDetailsForTheDay1", Request.QueryString["date"].ToString());
                    Page.Title = "Despatch Details For The Day";
                    break;
                }
            case "approvedindigenousforeignsuppliers":
                {
                    ApprovedIndigenousForeignSuppliers("ApprovedSuppliers", Request.QueryString["iorf"].ToString(), Request.QueryString["boa"].ToString());
                    Page.Title = "Approved " + Request.QueryString["iorf"].ToString() + " Suppliers";
                    break;
                }
            case "closingstock":
                {
                    ClosingStockForTheMonth("ClosingStock", Request.QueryString["it"].ToString());
                    Page.Title = "Closing Stock For The Month Of " + DateTime.Now.Month + ", " + DateTime.Now.Year + "";
                    break;
                }
            case "mids":
                {
                    MIDS("MIDS", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["cmp"].ToString(), Request.QueryString["d"].ToString(), Convert.ToBoolean(Request.QueryString["expec"].ToString()));

                    //Page.Title = "MIDS - Expected Orders  -  " + (Request.QueryString["m"].ToString() == "0" ? "" : Convert.ToDateTime(Request.QueryString["m"].ToString() + "/01/1900").ToString("MMMM") + ",") + " " + Request.QueryString["y"].ToString();
                    Page.Title = "Sales Quotation Statement  From-  " + Request.QueryString["y"].ToString() + " To " + Request.QueryString["m"].ToString();
                    break;
                }
            case "Salesrpt":
                {
                    Salesrpt("SalesReport", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["cmp"], Request.QueryString["Dept"].ToString(), Request.QueryString["Emp"].ToString(), Request .QueryString ["ST"].ToString (), Request .QueryString ["Cust"].ToString ());
                    Page.Title = "Sales Report Statement  From-  " + Request.QueryString["y"].ToString() + " To " + Request.QueryString["m"].ToString();
                    break;
                }
            case "POTech":
                {
                    POTech("POTechDraw", Request.QueryString["SoId"].ToString());

                    break;
                }

            case "mids2":
                {
                    MIDS2("MIDS2", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString());

                    //Page.Title = "MIDS - Expected Orders  -  " + (Request.QueryString["m"].ToString() == "0" ? "" : Convert.ToDateTime(Request.QueryString["m"].ToString() + "/01/1900").ToString("MMMM") + ",") + " " + Request.QueryString["y"].ToString();
                    Page.Title = "Sales Quotation Statement  From-  " + Request.QueryString["y"].ToString() + " To " + Request.QueryString["m"].ToString();
                    break;
                }


            case "mpds":
                {
                    MPDS("MPDS", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["cs"].ToString());
                    Page.Title = "MPDS  -  " + (Request.QueryString["m"].ToString() == "0" ? "" : Convert.ToDateTime(Request.QueryString["m"].ToString() + "/01/1900").ToString("MMMM") + ",") + " " + Request.QueryString["y"].ToString();
                    break;
                }
            case "emdreceived":
                {
                    EMDReceived("EMDReceived", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    Page.Title = "EMD Received";
                    break;
                }
            case "emdlist":
                {
                    EMDList("EMDList", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    Page.Title = "EMD List";
                    break;
                }
            case "pendingpayments":
                {
                    if (Request.QueryString["d"].ToString() == "Sales")
                    {
                        SalesPendingPayments("SalesOutStanding", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["cs"].ToString());
                    }
                    else if (Request.QueryString["d"].ToString() == "Services")
                    {
                        ServicesPendingPayments("ServicesOutStanding", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["cs"].ToString());
                    }
                    else if (Request.QueryString["d"].ToString() == "amc")
                    {
                        AMCPendingPayments("AMCOutStanding", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["cs"].ToString());
                    }
                    Page.Title = "Pending Payments  -  " + (Request.QueryString["m"].ToString() == "0" ? "" : Convert.ToDateTime(Request.QueryString["m"].ToString() + "/01/1900").ToString("MMMM") + ",") + " " + Request.QueryString["y"].ToString();
                    break;
                }
            case "sos":
                {
                    Page.Title = "Sales Out Standing  Report";
                    SalesOutStandingPayments("SalesOutStanding1", Request.QueryString["cid"].ToString(), Request.QueryString["nod1"].ToString(), Request.QueryString["nod2"].ToString());
                    break;
                }
            case "aos":
                {
                    Page.Title = "AMC Out Standing  Report";
                    AMCPendingPayments("AMCOutStanding", Request.QueryString["y"].ToString(), Request.QueryString["m"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["cs"].ToString());
                    //AMCOutStandingPayments("AMCOutStanding", Request.QueryString["cid"].ToString(), Request.QueryString["nod1"].ToString(), Request.QueryString["nod2"].ToString());
                    break;
                }
            case "pol":
                {
                    Page.Title = "Purchase Orders";
                    PurchaseOrdersList("PurchaseOrders", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    break;
                }
            case "sop":
                {
                    Page.Title = "Spares Order Profile";
                    SparesOrderProfile("SparesOrderProfile", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    break;
                }
            case "aop":
                {
                    Page.Title = "AMC Order Profile";
                    AMCOrderProfile("AMCOrderProfile", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    break;
                }
            case "srl":
                {
                    Page.Title = "Service Report";
                    ServiceReportList("Compregi", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request .QueryString ["comp"].ToString ());
                    break;
                }
            case "sal":
                {
                    Page.Title = "Service Assignment Report";
                    ServiceAssignmentList("ServiceAssignmentList", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["st"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["e"].ToString(), Request.QueryString["d"].ToString());
                    break;
                }
            case "crl":
                {
                    Page.Title = "Complaint Record List";
                    ServiceReportList("Compregi", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request .QueryString ["comp"].ToString ());
                    break;
                }
            case "crd":
                {
                    Page.Title = "Service Register";
                    //ServiceReportDetails("CompRegByClientName", Request.QueryString["CrD"].ToString(), Request.QueryString["PO"].ToString(), Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    ServiceReportDetails("CompRegByClientName", Request.QueryString["CrD"].ToString(), Request.QueryString["PONO"].ToString(), Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["Status"].ToString(), Request .QueryString ["Comp"].ToString ());
                    break;
                }
            case "crdet":
                {
                    Page.Title = "Service Register";
                    //ServiceReportDetails("CompRegByClientName", Request.QueryString["CrD"].ToString(), Request.QueryString["PO"].ToString(), Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    ServiceReportDet("compregi2", Request.QueryString["CrD"].ToString(), Request.QueryString["PONO"].ToString(), Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString(), Request.QueryString["Status"].ToString(), Request.QueryString["Comp"].ToString());
                    break;
                }

            case "Cou":
                {
                    Page.Title = "Courier Record List";
                    COurier("Courier", Request.QueryString["f"].ToString(), Request.QueryString["t"].ToString());
                    break;
                }
            case "adsoutstanding":
                {
                    Page.Title = "Advertising Outstanding Statement";
                    AdsOutStanding(Request.QueryString["mode"].ToString(), Request.QueryString["agcy"].ToString(), Request.QueryString["nod1"].ToString(), Request.QueryString["nod2"].ToString());
                    break;
                }
            case "sdbg":
                {
                    Page.Title = "Statement Of Security Deposits & Bank  Guarantees";
                    SDBGStatement("SDBG", Request.QueryString["c"].ToString(), Request.QueryString["s"].ToString());
                    break;
                }
            case "ser":
                {
                    Page.Title = "Supplier Evaluation Report";
                    SuppliersEvaluationReport("SuppliersEvaluation", Request.QueryString["fy"].ToString(), Request.QueryString["sid"].ToString(), Request.QueryString["ic"].ToString());
                    break;
                }
            case "SalesLeadStatement":
                {
                    SalesLeadStmt("SalesLeadStatement", Request.QueryString["Comapanyid"].ToString(), Request.QueryString["ut"].ToString(), Request.QueryString["FromSales"].ToString(), Request.QueryString["ToSales"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());
                    Page.Title = "Sales Lead List";
                    break;
                }
            case "Architect":
                {
                    Architect("Architect", Request.QueryString["cat"].ToString(),Request .QueryString ["City"].ToString (),Request .QueryString ["Pincode"].ToString ());
                    //Page.Title = Request.QueryString["c"].ToString() + " Data";
                    break;
                }
            case "DailyReport":
                {
                    //DailyReportStmt("InternoIn", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());

                    DailyReportStmt("DailyReport", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());
                    Page.Title = "Daily Report";
                    break;
                }
            case "ToDoList":
                {
                    ToDoListStmt("ToDoList", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());
                    Page.Title = "Daily Report";
                    break;
                }
            case "Leaves":
                {
                    //DailyReportStmt("InternoIn", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());

                    LeavesReport("LeavesReport", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString());
                    Page.Title = "Leaves History";
                    break;
                }
            case "StockTransfer":
                {
                    StockTransfer("StockTransfer", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(), Request.QueryString ["cmpids"].ToString ());
                    Page.Title = "DC & Stock Out based on locations wise Report";
                    break;
                }
            case "LeavesEmp":
                {
                    //DailyReportStmt("InternoIn", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());

                    LeavesReportEmp("LeaveReportEmp", Request.QueryString["From"].ToString(), Request.QueryString["To"].ToString(),Request.QueryString ["EmpId"].ToString ());
                    Page.Title = "Leaves History";
                    break;
                }
            case "SalesAssignmentStatement":
                {
                    SalesAssignmentStmt("SalesAssignmentStatement", Request.QueryString["Comapanyid"].ToString(), Request.QueryString["Status"].ToString(), Request.QueryString["FromSalesAssign"].ToString(), Request.QueryString["ToSalesAssign"].ToString(), Request.QueryString["dep"].ToString(), Request.QueryString["emp"].ToString());
                    Page.Title = "Sales Assignment List";
                    break;
                }
            case "POStatement":
                {
                    PurchaseStmt("POStatement", Request.QueryString["Comapanyid"].ToString(), Request.QueryString["FromPO"].ToString(), Request.QueryString["ToPO"].ToString(), Request.QueryString["dep"].ToString(), Request.QueryString["emp"].ToString());
                    Page.Title = "Purchase Order List";
                    break;


                }
            case "PurchaseStmt_Executive":
                {
                    PurchaseStmt_Executive("PurchaseStmt_Executive", Request.QueryString["Comapanyid"].ToString(), Request.QueryString["FromPO"].ToString(), Request.QueryString["ToPO"].ToString(), Request.QueryString["dep"].ToString(), Request.QueryString["emp"].ToString());
                    Page.Title = "Purchase Order List Executive wise";
                    break;
                }

            case "InternalOrder":
                {
                    InternalOrderStmt("InternalOrder", Request.QueryString["Comapanyid"].ToString(), Request.QueryString["FromInternal"].ToString(), Request.QueryString["ToInternal"].ToString(), Request.QueryString["empid"].ToString(), Request.QueryString["dep"].ToString());
                    Page.Title = " Internal Order Statement ";
                    break;
                }
            case "ReserveStockHistory":
                {
                    ReserveStockHistory("ReserveStockHistory");
                    Page.Title = "Reserve Stock History Report ";
                    break;
                }

            default:
                {
                    MessageBox.Show(this, "Under Construction");
                    Page.Title = "";
                    break;
                }
        }
    }
    #endregion

    //#region Goods Receipt Of The Day Report
    //private void GoodsReceiptOfTheDay(string ReportName, string Date)
    //{
    //    try
    //    {
    //        myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

    //        SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_PURCHASE_INVOICE_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_PURCHASE_INVOICE_DET", DBConString.ConnectionString());
    //        SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_DET", DBConString.ConnectionString());
    //        SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST  where item_code in (select item_code from YANTRA_PURCHASE_INVOICE_DET)", DBConString.ConnectionString());

    //        YANTRADataSet rds = new YANTRADataSet();
    //        da.Fill(rds, "YANTRA_PURCHASE_INVOICE_MAST");
    //        da1.Fill(rds, "YANTRA_PURCHASE_INVOICE_DET");
    //        da2.Fill(rds, "YANTRA_FIXED_PO_MAST");
    //        da3.Fill(rds, "YANTRA_FIXED_PO_DET");
    //        da4.Fill(rds, "YANTRA_SUPPLIER_MAST");
    //        da5.Fill(rds, "YANTRA_ITEM_MAST");

    //        myRep.SetDataSource(rds);
    //        myRep.RecordSelectionFormula = "{YANTRA_PURCHASE_INVOICE_MAST.PI_DATE}=Date(" + Convert.ToDateTime(Date).ToString("yyyy,MM,dd") + ")";

    //        CrystalReportViewer1.ReportSource = myRep;

    //        //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }
    //}
    //#endregion



    #region Goods Receipt Of The Day Report
    private void GoodsReceiptOfTheDay(string ReportName, string from, string to, string MRN, string cmpmids, string Brand, string InvNo)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CHECKING_FORMAT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CHECKING_FORMAT", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CHECKING_FORMAT_DETAILS");
            da1.Fill(rds, "YANTRA_CHECKING_FORMAT");
            da2.Fill(rds, "YANTRA_COMP_PROFILE");
            da3.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da4.Fill(rds, "YANTRA_ITEM_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_CHECKING_FORMAT.CHK_DATE}>=Date(" + Convert.ToDateTime(from).ToString("yyyy,MM,dd") + ")  AND {YANTRA_CHECKING_FORMAT.CHK_DATE}<=Date(" + Convert.ToDateTime(to).ToString("yyyy,MM,dd") + ") ";
            if (cmpmids == "0" && Brand != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + Brand + "";
            }
            else if (cmpmids == "0" && Brand == "0" && MRN =="0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (cmpmids != "0" && Brand == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_CHECKING_FORMAT.CP_ID}=" + cmpmids + " ";
            }
            else if (cmpmids != "0" && Brand != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + Brand + " AND {YANTRA_CHECKING_FORMAT.CP_ID}=" + cmpmids;

            }
            else if (MRN != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_CHECKING_FORMAT.CHK_ID}=" + MRN + "";
            }
            else if (InvNo != "") 
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_CHECKING_FORMAT.CHK_INVOICE_NO}=" + InvNo + "";

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


    #region Despatch Details For The Day
    private void DespatchDetailsForTheDay(string ReportName, string Date)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_DET", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_LKUP_COLOR_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da2.Fill(rds, "YANTRA_ITEM_MAST");
            da3.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da6.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da7.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_DELIVERY_CHALLAN_MAST.DC_DATE}=Date(" + Convert.ToDateTime(Date).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion



    //#region Despatch Details For The Day
    //private void DespatchDetailsForTheDay(string ReportName, string Date)
    //{
    //    try
    //    {
    //        myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

    //        SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_WO_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
    //        SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_DELIVERY_CHALLAN_DET)", DBConString.ConnectionString());
    //        SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_DET", DBConString.ConnectionString());

    //        YANTRADataSet rds = new YANTRADataSet();
    //        da.Fill(rds, "YANTRA_CUSTOMER_MAST");
    //        da2.Fill(rds, "YANTRA_ENQ_MAST");
    //        da3.Fill(rds, "YANTRA_QUOT_MAST");
    //        da4.Fill(rds, "YANTRA_SO_MAST");
    //        da5.Fill(rds, "YANTRA_WO_MAST");
    //        da6.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
    //        da7.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
    //        da8.Fill(rds, "YANTRA_SALES_INVOICE_DET");
    //        da9.Fill(rds, "YANTRA_ITEM_MAST");
    //        da10.Fill(rds, "YANTRA_DELIVERY_CHALLAN_DET");

    //        myRep.SetDataSource(rds);
    //        myRep.RecordSelectionFormula = "{YANTRA_DELIVERY_CHALLAN_MAST.DC_DATE}=Date(" + Convert.ToDateTime(Date).ToString("yyyy,MM,dd") + ")";

    //        CrystalReportViewer1.ReportSource = myRep;

    //        //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }
    //}
    //#endregion

    #region Approved Indigenous Foreign Suppliers
    private void ApprovedIndigenousForeignSuppliers(string ReportName, string IndigenousForeign, string BasisOfApproval)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2;
            if (BasisOfApproval == "All")
            {
                da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_APPROVALS", DBConString.ConnectionString());
            }
            else
            {
                da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_APPROVALS WHERE BASIS_OF_APPROVAL='" + BasisOfApproval + "'", DBConString.ConnectionString());
            }
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_MAST", DBConString.ConnectionString());
            // SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SUPPLIER_MAST)", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da2.Fill(rds, "YANTRA_SUPPLIER_APPROVALS");
            da3.Fill(rds, "YANTRA_FIXED_PO_MAST");
            // da4.Fill(rds, "YANTRA_ITEM_MAST");
            da5.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            myRep.SetDataSource(rds);
            if (BasisOfApproval == "All")
            {
                myRep.RecordSelectionFormula = "{YANTRA_SUPPLIER_MAST.INDIGENOUS_FOREIGN}='" + IndigenousForeign + "'";
            }
            else
            {
                myRep.RecordSelectionFormula = "{YANTRA_SUPPLIER_MAST.INDIGENOUS_FOREIGN}='" + IndigenousForeign + "' AND {YANTRA_SUPPLIER_APPROVALS.BASIS_OF_APPROVAL} IN ('" + BasisOfApproval + "')";
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

    #region Closing Stock For The Month
    private void ClosingStockForTheMonth(string ReportName, string ItemTypeId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_ITEM_MAST where IT_TYPE_ID= " + ItemTypeId + ")", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ITEM_QTY where ITEM_QTY_IN_HAND != '0' ", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ITEM_MAST");
            da1.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
            da2.Fill(rds, "YANTRA_ITEM_QTY");
            myRep.SetDataSource(rds);
            if (ItemTypeId != "0")
            {
                myRep.RecordSelectionFormula = "{YANTRA_ITEM_MAST.IT_TYPE_ID}=" + ItemTypeId + " ";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion
    private void POTech(string ReportName, string SoId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            //myRep.Load(filename);
            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SO_DET Order by SO_DET_ID", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_IMAGE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_ITEM_SPECIFICATION_IMAGE", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_ITEM_SPECIFICATION_IMAGE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            
            da.Fill(rds, "YANTRA_SO_DET");
            da1.Fill(rds, "YANTRA_SO_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            da4.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da5.Fill(rds, "YANTRA_ITEM_IMAGE");
            da6.Fill(rds, "YANTRA_ITEM_SPECIFICATION_IMAGE");
            da7.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_ID}=" + SoId + " ";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }

    private void Salesrpt(string ReportName, string from, string To, string cmpids, string Dept,string Emp,string ST ,string cust)
    {
        try
        {
            string filename = Server.MapPath("EOD/") + ReportName + ".rpt";
            myRep.Load(filename);
            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_MAST Order by SI_ID desc", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST Where CUST_ID not in (2724,3040,3041,3211,4345,4869,3212,3887,1167,2209,2210,4263,6116,6290)", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from Location_tbl", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_SO_Det", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da1.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da2.Fill(rds, "YANTRA_SO_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da4.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            da6.Fill(rds, "Location_tbl");
            da7.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da8.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da9.Fill(rds, "YANTRA_SO_Det");
            myRep.SetDataSource(rds);

            string Soid = "0";

            myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(from).ToString("yyyy,MM,dd") + ")  AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(To).ToString("yyyy,MM,dd") + ") ";

            if (cmpids == "0" && Dept == "0" && Emp == "0" || Emp == "" && ST == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula;
            }
            else if (cmpids == "0" && Dept == "0" && Emp == "0" || Emp == "" && ST == "0" && cust !="0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND YANTRA_CUSTOMER_UNITS.CUST_ID='" + cust + "'";
            }
            else if (cmpids != "0" && Dept == "0" && Emp =="0" && ST == "0" && cust == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "'";
            }
            else if (cmpids != "0" && Dept == "0" && Emp == "0" && ST == "0" && cust != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' AND {YANTRA_CUSTOMER_MAST.CUST_ID} =" + cust + "";
            }
            else if (cmpids != "0" && Dept != "0" && Emp == "0" && ST == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' And {YANTRA_EMPLOYEE_DET.dept_id} =" + Dept + "";
            }
            else if (cmpids != "0" && Dept != "0" && Emp != "0" && ST == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' And {YANTRA_EMPLOYEE_DET.dept_id} =" + Dept + " And {Yantra_Employee_Mast.EMP_ID}=" + Emp + "";
            }
            else if (cmpids != "0" && Dept != "0" && Emp == "0"  && ST == "1")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' And {YANTRA_EMPLOYEE_DET.dept_id} =" + Dept + "And {YANTRA_SALES_INVOICE_MAST.SO_ID} >" + Soid + "";
            }
            else if (cmpids != "0" && Dept != "0" && Emp == "0" && ST == "2")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' And {YANTRA_EMPLOYEE_DET.dept_id} =" + Dept + "And {YANTRA_SALES_INVOICE_MAST.SO_ID} =" + Soid + "";
            }
            else if (cmpids != "0" && Dept != "0" && Emp != "0" && ST == "1")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' And {YANTRA_EMPLOYEE_DET.dept_id} =" + Dept + " And {Yantra_Employee_Mast.EMP_ID}=" + Emp + " And {YANTRA_SALES_INVOICE_MAST.SO_ID} >" + Soid + "";
            }
            else if (cmpids != "0" && Dept != "0" && Emp != "0" && ST == "2")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "' And {YANTRA_EMPLOYEE_DET.dept_id} =" + Dept + " And {Yantra_Employee_Mast.EMP_ID}=" + Emp + " And {YANTRA_SALES_INVOICE_MAST.SO_ID} =" + Soid + "";
            }
            
            else if (cmpids != "0" && Dept == "0" && Emp == "0" || Emp == "" && ST == "1")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "'  And {YANTRA_SALES_INVOICE_MAST.SO_ID} >" + Soid + "";
            }
            else if (cmpids != "0" && Dept == "0" && Emp == "0" || Emp =="" && ST == "2")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {Location_tbl.Locid}='" + cmpids + "'  And {YANTRA_SALES_INVOICE_MAST.SO_ID}=" + Soid + "";
            }
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }

    #region MIDS - Expected Orders
    //  private void MIDS(string ReportName, string Year, string Month, string EmployeeId, string CustomerStatus)
    private void MIDS(string ReportName, string from, string to, string EmployeeId, string cmpmids, string dep, bool expec)
    {
        try
        {
            string filename = Server.MapPath("EOD/") + ReportName + ".rpt";
            myRep.Load(filename);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST ", DBConString.ConnectionString());
            // SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST A WHERE A.QUOT_REVISED_KEY =(SELECT MAX(B.QUOT_REVISED_KEY) FROM YANTRA_QUOT_MAST B WHERE A.QUOT_NO=B.QUOT_NO) AND A.QUOT_PO_FLAG<>'CLOSED' ORDER BY A.QUOT_ID", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_DET", DBConString.ConnectionString());
            // SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_FOLLOWUP_DET", DBConString.ConnectionString()); 
            //SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_FOLLOWUP_DET A WHERE A.QUOT_FOLLOWUP_DET_ID =(SELECT MAX(B.QUOT_FOLLOWUP_DET_ID) FROM YANTRA_QUOT_FOLLOWUP_DET B WHERE A.QUOT_ID=B.QUOT_ID)", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_QUOT_DET)", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE ", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("SELECT * FROM YANTRA_SALES_INVOICE_MAST ", DBConString.ConnectionString());
            
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_SO_MAST");
            da5.Fill(rds, "YANTRA_QUOT_MAST");
            da6.Fill(rds, "YANTRA_QUOT_DET");
            da11.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da8.Fill(rds, "YANTRA_ITEM_MAST");

            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da10.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Convert.ToDateTime(from).ToString("yyyy,MM,dd") + ")  AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Convert.ToDateTime(to).ToString("yyyy,MM,dd") + ") ";

            if (cmpmids == "0" && EmployeeId != "0")
            {
                if (expec == true)
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + " AND {YANTRA_QUOT_MAST.IS_EXPECTED_ORDER}=" + expec + "";
                }
                else
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + "";
                }
            }
            else if (cmpmids == "0" && EmployeeId == "0")
            {
                if (expec == true)
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_QUOT_MAST.IS_EXPECTED_ORDER}=" + expec + " ";
                }
                else
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                }

            }
            else if (cmpmids != "0" && EmployeeId == "0")
            {
                if (expec == true)
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_QUOT_MAST.CP_ID}=" + cmpmids + " AND {YANTRA_QUOT_MAST.IS_EXPECTED_ORDER}=" + expec + " ";
                }
                else
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_QUOT_MAST.CP_ID}=" + cmpmids + " ";
                }

            }
            else if (cmpmids != "0" && EmployeeId != "0")
            {
                if (expec == true)
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + " AND {YANTRA_QUOT_MAST.CP_ID}=" + cmpmids + " AND {YANTRA_QUOT_MAST.IS_EXPECTED_ORDER}=" + expec + "";
                }
                else
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + " AND {YANTRA_QUOT_MAST.CP_ID}=" + cmpmids;
                }

            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region MIDS2 - Expected Orders
    //  private void MIDS(string ReportName, string Year, string Month, string EmployeeId, string CustomerStatus)
    private void MIDS2(string ReportName, string from, string to, string EmployeeId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST A WHERE A.QUOT_REVISED_KEY =(SELECT MAX(B.QUOT_REVISED_KEY) FROM YANTRA_QUOT_MAST B WHERE A.QUOT_NO=B.QUOT_NO)  ORDER BY A.QUOT_ID", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_DET", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_FOLLOWUP_DET A WHERE A.QUOT_FOLLOWUP_DET_ID =(SELECT MAX(B.QUOT_FOLLOWUP_DET_ID) FROM YANTRA_QUOT_FOLLOWUP_DET B WHERE A.QUOT_ID=B.QUOT_ID)", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_QUOT_DET)", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
            //SqlDataAdapter da11 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST_1  ", DBConString.ConnectionString());
            
            SqlDataAdapter da10 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE ", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
            da5.Fill(rds, "YANTRA_QUOT_MAST");
            da6.Fill(rds, "YANTRA_QUOT_DET");
            da7.Fill(rds, "YANTRA_QUOT_FOLLOWUP_DET");
            da8.Fill(rds, "YANTRA_ITEM_MAST");

            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da11.Fill(rds, "YANTRA_EMPLOYEE_MAST_1");

            da10.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Convert.ToDateTime(from).ToString("yyyy,MM,dd") + ")  AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Convert.ToDateTime(to).ToString("yyyy,MM,dd") + ") ";

            if (EmployeeId != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_PREPARED_BY}=" + EmployeeId + "";
            }
            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion





    #region MPDS
    private void MPDS(string ReportName, string Year, string Month, string EmployeeId, string CustomerStatus)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST A WHERE A.QUOT_REVISED_KEY =(SELECT MAX(B.QUOT_REVISED_KEY) FROM YANTRA_QUOT_MAST B WHERE A.QUOT_NO=B.QUOT_NO) AND A.QUOT_PO_FLAG<>'CLOSED' AND A.IS_EXPECTED_ORDER='TRUE' ORDER BY A.QUOT_ID", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_DET", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_FOLLOWUP_DET ", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_FOLLOWUP_DET A WHERE A.QUOT_FOLLOWUP_DET_ID =(SELECT MAX(B.QUOT_FOLLOWUP_DET_ID) FROM YANTRA_QUOT_FOLLOWUP_DET B WHERE A.QUOT_ID=B.QUOT_ID)", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_QUOT_DET)", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da3.Fill(rds, "YANTRA_ENQ_MAST");
            da4.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
            da5.Fill(rds, "YANTRA_QUOT_MAST");
            da6.Fill(rds, "YANTRA_QUOT_DET");
            //da7.Fill(rds, "YANTRA_QUOT_FOLLOWUP_DET");
            da8.Fill(rds, "YANTRA_ITEM_MAST");
            da9.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da10.Fill(rds, "YANTRA_LKUP_ENQ_MODE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "";

            if (Month == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + ",01,01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + ",12," + DateTime.DaysInMonth(int.Parse(Year), int.Parse("12")) + ")";
            }
            else if (Month != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            if (EmployeeId != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + "";
            }
            if (CustomerStatus != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_CUSTOMER_MAST.ISNEWOREXISTING}=" + CustomerStatus + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region EMD Received Report
    private void EMDReceived(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_EMDS_RECEIVED", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_EMDS_RECEIVED");
            da3.Fill(rds, "YANTRA_QUOT_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_EMDS_RECEIVED.EMDR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_EMDS_RECEIVED.EMDR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region EMD List Report
    private void EMDList(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_ENQ_DET");
            da3.Fill(rds, "YANTRA_QUOT_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Pending Payments From Invoice Report
    private void SalesPendingPayments(string ReportName, string Year, string Month, string EmployeeId, string CustomerStatus)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("select * from YANTRA_SALES_INVOICE_MAST where si_id in(select si_id  from YANTRA_PAYMENTS_RECEIVED where PR_PAYMENT_STATUS<>'Cleared') or si_id not in(select si_id  from YANTRA_PAYMENTS_RECEIVED )", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("select * from YANTRA_SALES_INVOICE_MAST", DBConString.ConnectionString());

            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SALES_INVOICE_DET) ", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from VIEW_SALES_PENDING_PAYMENTS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_QUOT_MAST");
            da4.Fill(rds, "YANTRA_SO_MAST");
            da5.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
            da6.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da7.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da8.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da9.Fill(rds, "YANTRA_ITEM_MAST");
            da10.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da11.Fill(rds, "VIEW_SALES_PENDING_PAYMENTS");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            if (Month == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Year + ",01,01) AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Year + ",12," + DateTime.DaysInMonth(int.Parse(Year), int.Parse("12")) + ")";
            }
            else if (Month != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            if (EmployeeId != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + "";
            }
            if (CustomerStatus != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_CUSTOMER_MAST.ISNEWOREXISTING}=" + CustomerStatus + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    private void ServicesPendingPayments(string ReportName, string Year, string Month, string EmployeeId, string CustomerStatus)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SPARES_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SPARES_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SERVICES_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("select * from YANTRA_SALES_INVOICE_MAST where si_id in(select si_id  from YANTRA_PAYMENTS_RECEIVED where PR_PAYMENT_STATUS<>'Cleared') or si_id not in(select si_id  from YANTRA_PAYMENTS_RECEIVED )", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SALES_INVOICE_DET)", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from VIEW_SALES_PENDING_PAYMENTS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_SPARES_QUOT_MAST");
            da4.Fill(rds, "YANTRA_SPARES_ORDER_MAST");
            da5.Fill(rds, "YANTRA_SERVICES_ASSIGN_TASKS");
            da6.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da7.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da8.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da9.Fill(rds, "YANTRA_ITEM_MAST");
            da10.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da11.Fill(rds, "VIEW_SALES_PENDING_PAYMENTS");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            if (Month == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Year + ",01,01) AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Year + ",12," + DateTime.DaysInMonth(int.Parse(Year), int.Parse("12")) + ")";
            }
            else if (Month != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            if (EmployeeId != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + "";
            }
            if (CustomerStatus != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_CUSTOMER_MAST.ISNEWOREXISTING}=" + CustomerStatus + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    private void AMCPendingPayments(string ReportName, string Year, string Month, string EmployeeId, string CustomerStatus)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_SERVICES_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_AMC_INVOICE_MAST where amci_id in(select amci_id  from YANTRA_AMC_PAYMENT_RECEIVED where AMCPR_PAYMENT_STATUS<>'Cleared') or amci_id not in(select amci_id  from YANTRA_AMC_PAYMENT_RECEIVED )", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from VIEW_AMC_PENDING_PAYMENTS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da4.Fill(rds, "YANTRA_AMC_ORDER_MAST");
            da5.Fill(rds, "YANTRA_SERVICES_ASSIGN_TASKS");
            da6.Fill(rds, "YANTRA_AMC_INVOICE_MAST");
            da7.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da8.Fill(rds, "VIEW_AMC_PENDING_PAYMENTS");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            if (Month == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_AMC_INVOICE_MAST.AMCI_DATE}>=Date(" + Year + ",01,01) AND {YANTRA_AMC_INVOICE_MAST.AMCI_DATE}<=Date(" + Year + ",12," + DateTime.DaysInMonth(int.Parse(Year), int.Parse("12")) + ")";
            }
            else if (Month != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_AMC_INVOICE_MAST.AMCI_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_AMC_INVOICE_MAST.AMCI_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            if (EmployeeId != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_EMPLOYEE_MAST.EMP_ID}=" + EmployeeId + "";
            }
            if (CustomerStatus != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_CUSTOMER_MAST.ISNEWOREXISTING}=" + CustomerStatus + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Sales Out Standing Payments
    private void SalesOutStandingPayments(string ReportName, string CustomerId, string NoOfDays1, string NoOfDays2)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SO_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("select * from YANTRA_SALES_INVOICE_MAST where si_id in(select si_id  from YANTRA_PAYMENTS_RECEIVED where PR_PAYMENT_STATUS<>'Cleared') or si_id not in(select si_id  from YANTRA_PAYMENTS_RECEIVED )", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_SALES_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SALES_INVOICE_DET)", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_PAYMENTS_RECEIVED", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from VIEW_SALES_PENDING_PAYMENTS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_QUOT_MAST");
            da4.Fill(rds, "YANTRA_SO_MAST");
            da6.Fill(rds, "YANTRA_DELIVERY_CHALLAN_MAST");
            da7.Fill(rds, "YANTRA_SALES_INVOICE_MAST");
            da8.Fill(rds, "YANTRA_SALES_INVOICE_DET");
            da9.Fill(rds, "YANTRA_ITEM_MAST");
            da10.Fill(rds, "YANTRA_PAYMENTS_RECEIVED");
            da11.Fill(rds, "VIEW_SALES_PENDING_PAYMENTS");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CustomerId != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_CUSTOMER_MAST.CUST_ID}=" + CustomerId + "";
            }
            if (NoOfDays1 != "All" && NoOfDays2 != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "DateDiff ('d',{YANTRA_SALES_INVOICE_MAST.SI_DATE} ,CurrentDate )" + NoOfDays1 + "and DateDiff ('d',{YANTRA_SALES_INVOICE_MAST.SI_DATE} ,CurrentDate )" + NoOfDays2 + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region AMC Out Standing Payments
    private void AMCOutStandingPayments(string ReportName, string CustomerId, string NoOfDays1, string NoOfDays2)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_AMC_INVOICE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_AMC_INVOICE_DET", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_AMC_INVOICE_DET)", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_AMC_PAYMENT_RECEIVED", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da4.Fill(rds, "YANTRA_AMC_ORDER_MAST");
            da7.Fill(rds, "YANTRA_AMC_INVOICE_MAST");
            da8.Fill(rds, "YANTRA_AMC_INVOICE_DET");
            da9.Fill(rds, "YANTRA_ITEM_MAST");
            da10.Fill(rds, "YANTRA_AMC_PAYMENT_RECEIVED");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CustomerId != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_CUSTOMER_MAST.CUST_ID}=" + CustomerId + "";
            }
            if (NoOfDays1 != "All" && NoOfDays2 != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "DateDiff ('d',{YANTRA_AMC_INVOICE_MAST.AMCI_DATE} ,CurrentDate )" + NoOfDays1 + "and DateDiff ('d',{YANTRA_AMC_INVOICE_MAST.AMCI_DATE} ,CurrentDate )" + NoOfDays2 + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Purchase Order List
    private void PurchaseOrdersList(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_FIXED_PO_DET", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_FIXED_PO_DET)", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_FIXED_PO_MAST");
            da1.Fill(rds, "YANTRA_FIXED_PO_DET");
            da2.Fill(rds, "YANTRA_ITEM_MAST");
            da3.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
            da4.Fill(rds, "YANTRA_LKUP_DESP_MODE");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SALES_INVOICE_MAST.SI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_MAST.FPO_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_FIXED_PO_MAST.FPO_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Spares Order Profile
    private void SparesOrderProfile(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_SPARES_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SPARES_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SPARES_OP_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SPARES_ORDER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from VIEW_SPARES_SERVICES_PENDING_PAYMENTS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da1.Fill(rds, "YANTRA_SPARES_QUOT_MAST");
            da2.Fill(rds, "YANTRA_SPARES_ORDER_MAST");
            da3.Fill(rds, "YANTRA_SPARES_OP_MAST");
            da4.Fill(rds, "YANTRA_SPARES_ORDER_DET");
            da5.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da6.Fill(rds, "YANTRA_ITEM_MAST");
            da7.Fill(rds, "VIEW_SPARES_SERVICES_PENDING_PAYMENTS");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_SPARES_OP_MAST.SPOP_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SPARES_OP_MAST.SPOP_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region AMC Order Profile
    private void AMCOrderProfile(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_AMC_QUOTATION_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_AMC_WO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_AMC_ORDER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_AMC_WO_DET)", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from VIEW_AMC_SERVICES_PENDING_PAYMENTS", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_AMC_OA_MAST", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_AMC_WO_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da1.Fill(rds, "YANTRA_AMC_QUOTATION_MAST");
            da2.Fill(rds, "YANTRA_AMC_ORDER_MAST");
            da3.Fill(rds, "YANTRA_AMC_WO_MAST");
            da4.Fill(rds, "YANTRA_AMC_ORDER_DET");
            da5.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da6.Fill(rds, "YANTRA_ITEM_MAST");
            da7.Fill(rds, "VIEW_AMC_SERVICES_PENDING_PAYMENTS");
            da8.Fill(rds, "YANTRA_AMC_OA_MAST");
            da9.Fill(rds, "YANTRA_AMC_WO_DET");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_AMC_WO_MAST.WO_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_AMC_WO_MAST.WO_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region COureir Report List
    private void COurier(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from CourierMaster", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "CourierMaster");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{CourierMaster.Date}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {CourierMaster.Date}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Service Report List
    private void ServiceReportList(string ReportName, string FromDate, string ToDate , string comp)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from Service_Customer_Information", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Service_Customer_Unit_Details", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Service_Customer_Information");
            da1.Fill(rds, "Service_Customer_Unit_Details");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMPLAINT_REGISTER_DET");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            myRep.SetDataSource(rds);

            if (comp =="0")
            {
                myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            }
            else
            {
                myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.Cp_id}=" + comp + "";
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Complaint Reg With Custmer Details
    private void ServiceReportDet(string ReportName, string CustId, string PONO, string FromDate, string ToDate, string Status, string comp)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from Service_Customer_Information", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Service_Customer_Unit_Details", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Service_Customer_Information");
            da1.Fill(rds, "Service_Customer_Unit_Details");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMPLAINT_REGISTER_DET");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            //da6.Fill(rds, "YANTRA_SO_MAST");
            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = " {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_STATUS}='"+Status +"'";
            if (CustId == "0")
            {
                if (PONO == "0" && Status == "ALL" && comp == "0")
                {
                    myRep.RecordSelectionFormula = " {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                }
                else if (PONO == "0" && Status == "ALL" && comp != "0")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.Cp_id}=" + comp + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                }
                else
                {
                    myRep.RecordSelectionFormula = " {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_STATUS}='" + Status + "'";
                }
            }
            else if (CustId != "0")
            {
                if (PONO == "0" && Status == "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                }
                else if (PONO != "0" && Status != "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "And {YANTRA_COMPLAINT_REGISTER.CR_STATUS}= '" + Status + "'";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST .SO_ID}= " + PONO + "";
                }
                else if (PONO != "0" && Status == "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST .SO_ID}= " + PONO + "";

                }
                else if (PONO == "0" && Status != "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "And {YANTRA_COMPLAINT_REGISTER.CR_STATUS}= '" + Status + "'";
                }
            }


            //else if(CustId =="0")
            //{
            //    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
            //}
            //myRep.RecordSelectionFormula =myRep. RecordSelectionFormula+ " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Complaint Reg With Custmer Details
    private void ServiceReportDetails(string ReportName, string CustId, string PONO, string FromDate, string ToDate, string Status, string comp)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("Select * from Service_Customer_Information", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("Select * from Service_Customer_Unit_Details", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "Service_Customer_Information");
            //da1.Fill(rds, "Service_Customer_Unit_Details");
            da2.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMPLAINT_REGISTER_DET");
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            //da6.Fill(rds, "YANTRA_SO_MAST");
            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = " {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_STATUS}='"+Status +"'";
            if (CustId == "0")
            {
                if (PONO == "0" && Status == "ALL" && comp =="0")
                {
                    myRep.RecordSelectionFormula = " {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                }
                else if (PONO == "0" && Status == "ALL" && comp != "0")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMP_PROFILE.Cp_id}=" + comp + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                }
                else
                {
                    myRep.RecordSelectionFormula = " {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_STATUS}='" + Status + "'";
                }
            }
            else if (CustId != "0")
            {
                if (PONO == "0" && Status == "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                }
                else if (PONO != "0" && Status != "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "And {YANTRA_COMPLAINT_REGISTER.CR_STATUS}= '" + Status + "'";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST .SO_ID}= " + PONO + "";
                }
                else if (PONO != "0" && Status == "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST .SO_ID}= " + PONO + "";

                }
                else if (PONO == "0" && Status != "ALL")
                {
                    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "And {YANTRA_COMPLAINT_REGISTER.CR_STATUS}= '" + Status + "'";
                }
            }


            //else if(CustId =="0")
            //{
            //    myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CUST_ID}=" + CustId + "";
            //}
            //myRep.RecordSelectionFormula =myRep. RecordSelectionFormula+ " AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region Complaint Record List
    private void ComplaintRecordList(string ReportName, string FromDate, string ToDate, string CmpId, string empid, string dept)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SERVICE_REPORT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_COMPLAINT_REGISTER_DET)", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER_DET", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            //da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            //da2.Fill(rds, "YANTRA_CUSTOMER_DET");
            da3.Fill(rds, "YANTRA_SERVICE_REPORT_MAST");
            da4.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da5.Fill(rds, "YANTRA_ITEM_MAST");
            da6.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da7.Fill(rds, "YANTRA_COMPLAINT_REGISTER_DET");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_COMPLAINT_REGISTER.CR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_COMPLAINT_REGISTER.CR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND tonumber({YANTRA_COMPLAINT_REGISTER.CR_PREPARED_BY})={YANTRA_EMPLOYEE_MAST.EMP_ID}";

            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_PREPARED_BY}='" + empid + "'";
            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CP_ID}=" + CmpId;
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_PREPARED_BY}='" + empid + "' AND {YANTRA_COMPLAINT_REGISTER.CP_ID}=" + CmpId;
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region AMC RecordList
    private void AMCRecordList(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_SERVICE_REPORT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da2.Fill(rds, "YANTRA_CUSTOMER_DET");
            da3.Fill(rds, "YANTRA_SERVICE_REPORT_MAST");
            da4.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da5.Fill(rds, "YANTRA_ITEM_MAST");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_SERVICE_REPORT_MAST.SR_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SERVICE_REPORT_MAST.SR_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Ads Out Standing
    private void AdsOutStanding(string Mode, string Agency, string NoOfDays1, string NoOfDays2)
    {
        try
        {
            if (Mode == "Subscription")
            {
                myRep.Load(Server.MapPath("EOD/AdsOutStandingSubscription.rpt"));
            }
            else if (Mode == "Publication")
            {
                myRep.Load(Server.MapPath("EOD/AdsOutStandingPublication.rpt"));
            }
            else if (Mode == "Exhibition")
            {
                myRep.Load(Server.MapPath("EOD/AdsOutStandingExhibition.rpt"));
            }
            else if (Mode == "Sponsorship")
            {
                myRep.Load(Server.MapPath("EOD/AdsOutStandingSponsorship.rpt"));
            }

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ADVERTISING_AGENCY", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ADVERTISING_AGENSIES_INFO", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ADVERTISING_MAGZINES", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ADVERTISING_MODE", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SIZE_OF_ADVERTISING", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ADVERTISING_AGENCY");
            da1.Fill(rds, "YANTRA_ADVERTISING_AGENSIES_INFO");
            da2.Fill(rds, "YANTRA_ADVERTISING_MAGZINES");
            da3.Fill(rds, "YANTRA_ADVERTISING_MODE");
            da4.Fill(rds, "YANTRA_SIZE_OF_ADVERTISING");

            myRep.SetDataSource(rds);


            if (Mode != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_ADVERTISING_MODE.ADVM_NAME}='" + Mode + "'";
            }

            if (Agency != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " {YANTRA_ADVERTISING_AGENSIES_INFO.AA_ID}=" + Agency + "";
            }

            if (NoOfDays1 != "All" && NoOfDays2 != "All")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "DateDiff ('d',{YANTRA_ADVERTISING_AGENSIES_INFO.AAI_INVOICE_DATE} ,CurrentDate )" + NoOfDays1 + "and DateDiff ('d',{YANTRA_ADVERTISING_AGENSIES_INFO.AAI_INVOICE_DATE} ,CurrentDate )" + NoOfDays2 + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region SD/BG Statement
    private void SDBGStatement(string ReportName, string CustId, string SDorBG)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SDBG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_SDBG_DET", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SDBG_MAST");
            da1.Fill(rds, "YANTRA_SDBG_DET");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_QUOT_MAST");
            da4.Fill(rds, "YANTRA_SO_MAST");
            da5.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da6.Fill(rds, "YANTRA_CUSTOMER_UNITS");

            myRep.SetDataSource(rds);
            if (CustId != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = "{YANTRA_CUSTOMER_MAST.CUST_ID}=" + CustId + "";
            }
            if (SDorBG != "0")
            {
                if (myRep.RecordSelectionFormula != "")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND ";
                }
                myRep.RecordSelectionFormula = " {YANTRA_SDBG_MAST.SDBG_STATEMENT_OF}='" + SDorBG + "'";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Suppliers Evaluation Report
    private void SuppliersEvaluationReport(string ReportName, string FinancialYear, string SupplierId, string ItemCode)
    {
        try
        {
            //////string[] Years = FinancialYear.Split('-');
            //////string Year1 = Year1[0].ToString();
            //////string Year2 = Year1.Substring(1, 2).ToString() + Years[1].ToString();

            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_EVALUATION_REPORT", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SUPPLIER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SUPPLIER_EVALUATION_REPORT");
            da1.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SUPPLIER_EVALUATION_REPORT.SER_FINANCIAL_YEAR}='" + FinancialYear + "' AND {YANTRA_SUPPLIER_EVALUATION_REPORT.SUP_ID}=" + SupplierId + " AND {YANTRA_SUPPLIER_EVALUATION_REPORT.ITEM_CODE}=" + ItemCode + "";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    private void Architect(string ReportName,string Category,string City, string Pincode)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_ARCHITECT", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_LKUP_ARCHITECT");


            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "";

            if (City == "All" && Category == "All" && Pincode == "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (City != "All" && Category == "All" && Pincode == "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "  {YANTRA_LKUP_ARCHITECT.City}='" + City + "' ";
            }
            else if (City != "All" && Category != "All" && Pincode == "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "  {YANTRA_LKUP_ARCHITECT.City}='" + City + "' AND {YANTRA_LKUP_ARCHITECT.Category}= '"+ Category +"'";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_LKUP_ARCHITECT.City}='" + City + "' AND {YANTRA_LKUP_ARCHITECT.Category}= '"+ Category +"'";
            }
            else if (City != "All" && Category == "All" && Pincode != "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "  {YANTRA_LKUP_ARCHITECT.City}='" + City + "' AND {YANTRA_LKUP_ARCHITECT.Pincode}= " + Pincode + "";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_LKUP_ARCHITECT.City}='" + City + "' AND {YANTRA_LKUP_ARCHITECT.Category}= '"+ Category +"'";
            }
            else if (City == "All" && Category != "All" && Pincode == "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "  {YANTRA_LKUP_ARCHITECT.Category}='" + Category + "'";
            }
            else if (City == "All" && Category == "All" && Pincode != "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "  {YANTRA_LKUP_ARCHITECT.Pincode}='" + Pincode  + "'";
            }
            else if (City != "All" && Category != "All" && Pincode != "All")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "  {YANTRA_LKUP_ARCHITECT.City}='" + City + "' AND {YANTRA_LKUP_ARCHITECT.Category}=' " + Category + "' And {YANTRA_LKUP_ARCHITECT.Pincode}=" + Pincode +"";

            }
            

            

            
    
            CrystalReportViewer1.ReportSource = myRep;

           
        }
        catch (Exception ex)
        {

        }
    }

    #region  SalesLeadStmt List Report
    private void SalesLeadStmt(string ReportName, string CmpId, string UserType, string FromDate, string ToDate, string depid, string empid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST order by ENQ_DATE desc", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_COMP_PROFILE");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            //myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId + "";
            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.ENQ_PREPARED_BY}=" + empid + "";
            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId;
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.ENQ_PREPARED_BY}=" + empid + " AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId;
            }


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    private void LeavesReportEmp(string ReportName, string FromDate, string ToDate, string EmpId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EMP_LEAVE_tbl where Status3 ='Approved'  ", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM OB_CB_tbl", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "EMP_LEAVE_tbl");
            da1.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da2.Fill(rds, "OB_CB_tbl");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{EMP_LEAVE_tbl.Emp_Id}=" + EmpId + "";
            myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {EMP_LEAVE_tbl.DateOfApplying}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {EMP_LEAVE_tbl.DateOfApplying}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {

        }
    }
    private void StockTransfer(string ReportName, string FromDate, string ToDate, string cmpids)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM V_StockTransfer order by PO_NO Desc ", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "V_StockTransfer");
            //da1.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{Interno_Mast.ID}=1";
            myRep.RecordSelectionFormula = "{V_StockTransfer.DC_Date}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {V_StockTransfer.DC_Date}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (cmpids == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula;
            }
            else if (cmpids != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {V_StockTransfer.Cp_ID}=" + cmpids + " ";
            }
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {

        }
    }
    private void LeavesReport(string ReportName, string FromDate, string ToDate)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EMP_LEAVE_tbl where Status3 ='Approved'  ", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "EMP_LEAVE_tbl");
            da1.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{Interno_Mast.ID}=1";
            myRep.RecordSelectionFormula = "{EMP_LEAVE_tbl.DateOfApplying}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {EMP_LEAVE_tbl.DateOfApplying}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            CrystalReportViewer1.ReportSource = myRep;

        }
        catch (Exception ex)
        {

        }
    }
    #region  ToDoListStmt List Report
    private void ToDoListStmt(string ReportName, string FromDate, string ToDate, string empid, string depid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM To_Do_List", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST order by ENQ_DATE desc", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_DEPT_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM Interno_Mast", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM Interno_Det", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "To_Do_List");
            //da1.Fill(rds, "YANTRA_ENQ_MAST");
            //da2.Fill(rds, "YANTRA_COMP_PROFILE");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da5.Fill(rds, "YANTRA_DEPT_MAST");
            //da5.Fill(rds, "Interno_Mast");
            //da5.Fill(rds, "Interno_Det");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{Interno_Mast.ID}=1";
            myRep.RecordSelectionFormula = "{To_Do_List.Date}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {To_Do_List.Date}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            //myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId + "";
            if (depid != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_DEPT_MAST.DEPT_ID}=" + depid + "";
            }

            else if (empid != "0" && depid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {To_Do_List.Preparedby}=" + empid + "";
            }
            else if (empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "";
            }

            else if (depid == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            //else if (CmpId != "0" && empid == "0")
            //{
            //    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId;
            //}
            //else if (CmpId != "0" && empid != "0")
            //{
            //    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.ENQ_PREPARED_BY}=" + empid + " AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId;
            //}


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion
    #region  DailyReportStmt List Report
    private void DailyReportStmt(string ReportName, string FromDate, string ToDate, string empid, string depid)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_DAILY_REPORT", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST order by ENQ_DATE desc", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_DEPT_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM Interno_Mast", DBConString.ConnectionString());
            //SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM Interno_Det", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            rds.EnforceConstraints = false;
            da.Fill(rds, "YANTRA_DAILY_REPORT");
            //da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_COMP_PROFILE");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_EMPLOYEE_DET");
            da5.Fill(rds, "YANTRA_DEPT_MAST");
            //da5.Fill(rds, "Interno_Mast");
            //da5.Fill(rds, "Interno_Det");

            myRep.SetDataSource(rds);
            
            //myRep.RecordSelectionFormula = "{Interno_Mast.ID}=1";
            myRep.RecordSelectionFormula = "{YANTRA_DAILY_REPORT.DATETIME}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_DAILY_REPORT.DATETIME}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            //myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId + "";
            if (depid != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_DEPT_MAST.DEPT_ID}=" + depid + "";
            }

            else if (empid != "0" && depid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_DAILY_REPORT.EMPLOYEENAME}=" + empid + "";
            }
            else if (empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "";
            }

            else if (depid == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            //else if (CmpId != "0" && empid == "0")
            //{
            //    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId;
            //}
            //else if (CmpId != "0" && empid != "0")
            //{
            //    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_MAST.ENQ_PREPARED_BY}=" + empid + " AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId;
            //}


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  SalesAssignmentStmt List Report
    private void SalesAssignmentStmt(string ReportName, string CmpId, string Status, string FromDate, string ToDate, string dep, string empid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            //DateTime dt1, dt2;
            //dt1 = Convert.ToDateTime(FromDate);
            //dt2 = Convert.ToDateTime(ToDate);

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_ENQ_ASSIGN_TASKS.ASSIGN_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_ASSIGN_TASKS.ASSIGN_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CmpId == "0" && empid != "0")
            {
                if (Status == "ALL")
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.EMP_ID}=" + empid + "";
                else
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.EMP_ID}=" + empid + "AND {YANTRA_ENQ_ASSIGN_TASKS.ASSIGN_STATUS}='" + Status + "'";

            }
            else if (CmpId == "0" && empid == "0")
            {
                if (Status == "ALL")
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                else
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.ASSIGN_STATUS}='" + Status + "'";

            }
            else if (CmpId != "0" && empid == "0")
            {
                if (Status == "ALL")
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.CP_ID}=" + CmpId + "";
                else
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.CP_ID}=" + CmpId + "AND {YANTRA_ENQ_ASSIGN_TASKS.ASSIGN_STATUS}='" + Status + "'";
            }
            else if (CmpId != "0" && empid != "0")
            {
                if (Status == "ALL")
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.EMP_ID}=" + empid + "AND {YANTRA_ENQ_ASSIGN_TASKS.CP_ID}=" + CmpId + "";
                else
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ENQ_ASSIGN_TASKS.EMP_ID}=" + empid + "AND {YANTRA_ENQ_ASSIGN_TASKS.CP_ID}=" + CmpId + "AND {YANTRA_ENQ_ASSIGN_TASKS.ASSIGN_STATUS}='" + Status + "'";

            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  PurchaseStmt List Report
    private void PurchaseStmt(string ReportName, string CmpId, string FromDate, string ToDate, string dep, string empid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST order by SO_DATE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_SO_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SO_MAST.SO_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST.SO_PREPARED_BY}=" + empid + "";

            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (CmpId != "0" && empid == "0")
            {

                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST.CP_ID}=" + CmpId + "";

            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST.SO_PREPARED_BY}=" + empid + " AND {YANTRA_SO_MAST.CP_ID}=" + CmpId + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  PurchaseStmt List Report Executivewise
    private void PurchaseStmt_Executive(string ReportName, string CmpId, string FromDate, string ToDate, string dep, string empid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST order by SO_DATE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_SO_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SO_MAST.SO_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SO_MAST.SO_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST.SO_RESP_ID}=" + empid + "";

            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (CmpId != "0" && empid == "0")
            {

                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST.CP_ID}=" + CmpId + "";

            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SO_MAST.SO_RESP_ID}=" + empid + " AND {YANTRA_SO_MAST.CP_ID}=" + CmpId + "";
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  Internal Order Report
    private void InternalOrderStmt(string ReportName, string CmpId, string FromDate, string ToDate, string empid, string depid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_WO_MAST order by WO_DATE desc", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_WO_MAST");
            da1.Fill(rds, "YANTRA_QUOT_MAST");
            da2.Fill(rds, "YANTRA_ENQ_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_SO_MAST");
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");



            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_WO_MAST.WO_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_WO_MAST.WO_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            //myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId + "";
            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_WO_MAST.WO_PREPARED_BY}=" + empid + "";
            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_WO_MAST.CP_ID}=" + CmpId;
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_WO_MAST.WO_PREPARED_BY}=" + empid + " AND {YANTRA_WO_MAST.CP_ID}=" + CmpId;
            }


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  Suppliers Enq Report
    private void SuppliersEnqStmt(string ReportName, string FromDate, string ToDate, string CmpId, string empid, string depid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_SUP_ENQ_MAST order by SUP_ENQ_DATE desc", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_SUPPLIERS ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SUPPLIER_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SUP_ENQ_MAST");
            da1.Fill(rds, "YANTRA_ENQ_SUPPLIERS");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_SUP_ENQ_MAST.SUP_ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SUP_ENQ_MAST.SUP_ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND tonumber({YANTRA_SUP_ENQ_MAST.SUP_ENQ_PREPARED_BY})={YANTRA_EMPLOYEE_MAST.EMP_ID}";

            //myRep.RecordSelectionFormula = "{YANTRA_ENQ_MAST.ENQ_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.ENQ_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_ENQ_MAST.CP_ID}=" + CmpId + "";
            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SUP_ENQ_MAST.SUP_ENQ_PREPARED_BY}='" + empid + "'";
            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                //myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "{YANTRA_QUOT_MAST.QUOT_DATE}>=Date(" + Year + "," + Month + "," + "01) AND {YANTRA_QUOT_MAST.QUOT_DATE}<=Date(" + Year + "," + Month + "," + DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month)) + ")";
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SUP_ENQ_MAST.CP_ID}=" + CmpId;
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SUP_ENQ_MAST.SUP_ENQ_PREPARED_BY}='" + empid + "' AND {YANTRA_SUP_ENQ_MAST.CP_ID}=" + CmpId;
            }


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  Proforma Invoice Report
    private void ProformaInvoiceStmtBar(string ReportName, string FromDate, string ToDate, string ReqId, string CateId, string BrandId, string RegId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Cust_Brand_Analysis_tbl ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM Cust_Cate_Analysis_tbl ", DBConString.ConnectionString());
            ////SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST order by SUP_QUOT_DATE desc", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "Cust_Brand_Analysis_tbl");
            da2.Fill(rds, "Cust_Cate_Analysis_tbl");
            //da4.Fill(rds, "YANTRA_COMP_PROFILE");
            //da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");


            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "";
            //if (ReqId != "")
            //{
            //    if (ReqId == "All" && RegId =="All")
            //    {
            //        myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "YANTRA_CUSTOMER_MAST.CUST_FAX !='' ";
            //    }
            //    else
            //    {
            //        myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "YANTRA_CUSTOMER_MAST.CUST_FAX like '%" + ReqId + "%' and Reg_ID ="+RegId+" ";
            //    }
            //}
            myRep.RecordSelectionFormula = " {Cust_Cate_Analysis_tbl.Date}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {Cust_Cate_Analysis_tbl.Date}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            if (RegId == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (RegId != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_CUSTOMER_MAST.REG_ID}=" + RegId;
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  Proforma Invoice Report
    private void CustBrand(string ReportName, string FromDate, string ToDate, string ReqId, string CateId, string BrandId, string RegId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Cust_Brand_Analysis_tbl ", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM Cust_Cate_Analysis_tbl ", DBConString.ConnectionString());
            ////SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST order by SUP_QUOT_DATE desc", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "Cust_Brand_Analysis_tbl");
            //da2.Fill(rds, "Cust_Cate_Analysis_tbl");
            //da4.Fill(rds, "YANTRA_COMP_PROFILE");
            //da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = " {Cust_Brand_Analysis_tbl.Date}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {Cust_Brand_Analysis_tbl.Date}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            if (RegId == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (RegId != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_CUSTOMER_MAST.REG_ID}=" + RegId;
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region  Proforma Invoice Report
    private void ProformaInvoiceStmt(string ReportName, string FromDate, string ToDate, string ReqId, string CateId, string BrandId, string RegId)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM Cust_Brand_Analysis_tbl ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM Cust_Cate_Analysis_tbl ", DBConString.ConnectionString());
            ////SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST order by SUP_QUOT_DATE desc", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            //da1.Fill(rds, "Cust_Brand_Analysis_tbl");
            da2.Fill(rds, "Cust_Cate_Analysis_tbl");
            //da4.Fill(rds, "YANTRA_COMP_PROFILE");
            //da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");


            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = " {Cust_Cate_Analysis_tbl.Date}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {Cust_Cate_Analysis_tbl.Date}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";
            if (RegId == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (RegId != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_CUSTOMER_MAST.REG_ID}=" + RegId;
            }
            

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    //#region  Proforma Invoice Report
    //private void ProformaInvoiceStmt(string ReportName, string FromDate, string ToDate, string CmpId)
    //{
    //    try
    //    {
    //        myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

    //        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_SUP_ENQ_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_SUPPLIERS ", DBConString.ConnectionString());
    //        SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SUPPLIER_MAST ", DBConString.ConnectionString());
    //        //SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
    //        SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
    //        SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST order by SUP_QUOT_DATE desc", DBConString.ConnectionString());


    //        YANTRADataSet rds = new YANTRADataSet();
    //        da.Fill(rds, "YANTRA_SUP_ENQ_MAST");
    //        da1.Fill(rds, "YANTRA_ENQ_SUPPLIERS");
    //        da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
    //        da4.Fill(rds, "YANTRA_COMP_PROFILE");
    //        da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");


    //        myRep.SetDataSource(rds);

    //        myRep.RecordSelectionFormula = "{YANTRA_SUP_QUOT_MAST.SUP_QUOT_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SUP_QUOT_MAST.SUP_QUOT_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

    //        if (CmpId == "0")
    //        {
    //            myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
    //        }
    //        else if (CmpId != "0")
    //        {
    //            myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SUP_QUOT_MAST.CP_ID}=" + CmpId;
    //        }

    //        CrystalReportViewer1.ReportSource = myRep;
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }
    //}
    //#endregion

    #region  PurchaseOrderStmt Report
    private void PurchaseOrderStmt(string ReportName, string FromDate, string ToDate, string CmpId, string empid, string depid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_SUP_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_SUPPLIERS ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SUPPLIER_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_FIXED_PO_MAST order by FPO_DATE desc", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SUP_ENQ_MAST");
            da1.Fill(rds, "YANTRA_ENQ_SUPPLIERS");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");
            da6.Fill(rds, "YANTRA_FIXED_PO_MAST");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_MAST.FPO_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_FIXED_PO_MAST.FPO_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_FIXED_PO_MAST.PREPAREDBY}=" + empid + "";
            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_FIXED_PO_MAST.CP_ID}=" + CmpId;
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_FIXED_PO_MAST.PREPAREDBY}=" + empid + " AND {YANTRA_FIXED_PO_MAST.CP_ID}=" + CmpId;
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  Shipment Details Stmt Report
    private void ShipmentDetStmt(string ReportName, string FromDate, string ToDate, string CmpId, string Brand)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_SUP_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_SUPPLIERS ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SUPPLIER_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_SHIPPING_DETAILS_MASTER order by SD_DATE desc", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_FIXED_PO_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_SHIPPING_DETAILS ", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_IMAGE ", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("SELECT * FROM Forwarder_Details ", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_PRODUCT_COMPANY ", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_SUP_ENQ_MAST");
            da1.Fill(rds, "YANTRA_ENQ_SUPPLIERS");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_SHIPPING_DETAILS_MASTER");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");
            da6.Fill(rds, "YANTRA_FIXED_PO_MAST");
            da7.Fill(rds, "YANTRA_SHIPPING_DETAILS");
            da8.Fill(rds, "YANTRA_ITEM_MAST");
            da9.Fill(rds, "YANTRA_ITEM_IMAGE");
            da10.Fill(rds, "Forwarder_Details");
            da11.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_SHIPPING_DETAILS_MASTER.SD_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SHIPPING_DETAILS_MASTER.SD_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CmpId == "0" && Brand == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (CmpId != "0" && Brand=="0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SHIPPING_DETAILS_MASTER.CP_ID}=" + CmpId;
            }
            else if (CmpId == "0" && Brand != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SHIPPING_DETAILS.Brand_Id}=" + Brand ;
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  PurInvoiceStmt Report
    private void PurInvoiceStmt(string ReportName, string FromDate, string ToDate, string CmpId, string empid, string depid)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.YANTRA_PURCHASE_INVOICE_MAST order by PI_DATE desc", DBConString.ConnectionString());
            // SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_SUPPLIERS ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_SUPPLIER_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            //SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_SUP_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_FIXED_PO_MAST ", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_PURCHASE_INVOICE_MAST");
            // da1.Fill(rds, "YANTRA_ENQ_SUPPLIERS");
            da2.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_COMP_PROFILE");
            //da5.Fill(rds, "YANTRA_SUP_QUOT_MAST");
            da6.Fill(rds, "YANTRA_FIXED_PO_MAST");

            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_PURCHASE_INVOICE_MAST.PI_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_PURCHASE_INVOICE_MAST.PI_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ") AND tonumber({YANTRA_PURCHASE_INVOICE_MAST.PI_PREPARED_BY}) = {YANTRA_EMPLOYEE_MAST.EMP_ID}";

            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_PURCHASE_INVOICE_MAST.PI_PREPARED_BY}='" + empid + "'";
            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_PURCHASE_INVOICE_MAST.CP_ID}=" + CmpId;
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_PURCHASE_INVOICE_MAST.PI_PREPARED_BY}='" + empid + "' AND {YANTRA_PURCHASE_INVOICE_MAST.CP_ID}=" + CmpId;
            }

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region ServiceAssignmentList
    private void ServiceAssignmentList(string ReportName, string FromDate, string ToDate, string ServiceType, string CmpId, string empid, string dept)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));
            //  myRep.Load(Server.MapPath("ServiceAssignmentList.rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_COMPLAINT_REGISTER", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_SERVICES_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_COMP_PROFILE", DBConString.ConnectionString());


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da1.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da2.Fill(rds, "YANTRA_SERVICES_ASSIGN_TASKS");
            da4.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da6.Fill(rds, "YANTRA_COMP_PROFILE");


            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "{YANTRA_SERVICES_ASSIGN_TASKS.SERVICE_ASSIGN_DATE}>=Date(" + Convert.ToDateTime(FromDate).ToString("yyyy,MM,dd") + ") AND {YANTRA_SERVICES_ASSIGN_TASKS.SERVICE_ASSIGN_DATE}<=Date(" + Convert.ToDateTime(ToDate).ToString("yyyy,MM,dd") + ")";

            if (CmpId == "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SERVICES_ASSIGN_TASKS.EMP_ID}='" + empid + "'";
                if (ServiceType == "i")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Installation'";
                }
                else if (ServiceType == "w")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Warranty'";
                }
                else if (ServiceType == "a")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='AMC'";
                }
                else if (ServiceType == "n")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Non Warranty'";
                }

            }
            else if (CmpId == "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " ";
                if (ServiceType == "i")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Installation'";
                }
                else if (ServiceType == "w")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Warranty'";
                }
                else if (ServiceType == "a")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='AMC'";
                }
                else if (ServiceType == "n")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Non Warranty'";
                }
            }
            else if (CmpId != "0" && empid == "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SERVICES_ASSIGN_TASKS.CP_ID}=" + CmpId;
                if (ServiceType == "i")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Installation'";
                }
                else if (ServiceType == "w")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Warranty'";
                }
                else if (ServiceType == "a")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='AMC'";
                }
                else if (ServiceType == "n")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Non Warranty'";
                }
            }
            else if (CmpId != "0" && empid != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_SERVICES_ASSIGN_TASKS.EMP_ID}='" + empid + "' AND {YANTRA_SERVICES_ASSIGN_TASKS.CP_ID}=" + CmpId;
                if (ServiceType == "i")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Installation'";
                }
                else if (ServiceType == "w")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Warranty'";
                }
                else if (ServiceType == "a")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='AMC'";
                }
                else if (ServiceType == "n")
                {
                    myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_COMPLAINT_REGISTER.CR_CALL_TYPE}='Non Warranty'";
                }
            }


            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region  Reserve History Stmt Report
    private void ReserveStockHistory(string ReportName)
    {
        try
        {
            myRep.Load(Server.MapPath("EOD/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM YANTRA_SO_DET where SO_RES_STATUS='True'", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM YANTRA_ENQ_MAST ", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM YANTRA_QUOT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM YANTRA_SO_MAST", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_MAST where item_code in (select item_code from YANTRA_SO_DET)", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("SELECT * FROM YANTRA_ITEM_QTY ", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_COLOR_MAST ", DBConString.ConnectionString());
            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_SO_DET");
            da1.Fill(rds, "YANTRA_ENQ_MAST");
            da2.Fill(rds, "YANTRA_QUOT_MAST");
            da3.Fill(rds, "YANTRA_SO_MAST");
            da4.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da5.Fill(rds, "YANTRA_ITEM_MAST");
            da6.Fill(rds, "YANTRA_ITEM_QTY");
            da7.Fill(rds, "YANTRA_LKUP_COLOR_MAST");


            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "";

            CrystalReportViewer1.ReportSource = myRep;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    //#region Send
    //protected void btnSend_Click(object sender, EventArgs e)
    //{
    //    myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("report.pdf"));


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
    //#endregion
}


