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
using System.Data.SqlClient;
using Yantra.MessageBox;
using YantraDAL;
using Yantra.Classes;
//using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net.Mail;
using System.Net;



public partial class Modules_Home_Email : System.Web.UI.Page
{

    ReportDocument myRep = new ReportDocument();
    string mailAttachPath, reportType;
    Yantra.Classes.Email mail;
    protected void Page_Load(object sender, EventArgs e)
    {
        Button2.Attributes.Add("onclick", "javascript:window.close();");
        reportType = "";
        if (Request.QueryString["type"] != null)
        {
            reportType = Request.QueryString["type"].ToString();
        }

        if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "mailattach"))
        {
            if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "mailattach\\" + lblEmpIdHidden.Text))
            { }
            else
            {
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "mailattach\\" + lblEmpIdHidden.Text);
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "mailattach");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "mailattach\\" + lblEmpIdHidden.Text);
        }
        mailAttachPath = AppDomain.CurrentDomain.BaseDirectory + "mailattach\\" + lblEmpIdHidden.Text + "\\";
        if (!IsPostBack)
        {
            if (Request.QueryString["empid"] != null)
            {
                lblEmpIdHidden.Text = Request.QueryString["empid"].ToString();
            }
            if (Request.QueryString["custemail"] != null)
            {
                txtTo.Text = Request.QueryString["custemail"].ToString();
            }
        }
    }

    private void Mail_Send()
    {
       
        //mail = new Yantra.Classes.Email(txtTo.Text, txtCc.Text, HR.EmployeeMaster.GetEmployeeEmail(lblEmpIdHidden.Text));
        //mail.BodyFormat = MailFormat.Html;
        //mail.Body = txtBody.Text;
        //mail.Subject = txtSubject.Text;
        
        //if (reportType != "")
        //{
        //    MailAttachment objattach = new MailAttachment(RunReport());
        //    mail.Attachments.Add(objattach);
        //}
        //try
        //{
        //    mail.Send();
        //    MessageBox.Show(this, "Mail Sent Successfully");
        //}
        //catch (Exception) 
        //{
        //    MessageBox.Show(this, "Could not send mail. please try again");
        //}


        MailMessage mm = new MailMessage("valuelineinfo@gmail.com", txtTo.Text);
        //MailMessage mm = new MailMessage("pramodbmk@gmail.com", "codegear1046@gmail.com");
        mm.Subject = txtSubject.Text;
        mm.Body = txtBody.Text;
       // System.Net.Mail.MailAttachment objattach = new System.Web.Mail.MailAttachment(RunReport());
      
        mm.Attachments.Add(new Attachment(RunReport()));
        mm.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        NetworkCredential NetworkCred = new NetworkCredential();
        NetworkCred.UserName = "valuelineinfo@gmail.com";
        NetworkCred.Password = "Valuelinehyd";
        smtp.UseDefaultCredentials = true;
        smtp.Credentials = NetworkCred;
        smtp.Port = 587;
        smtp.Send(mm);

        MessageBox.Show(this, "Mail sent sucessfully");





    }




   












    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Mail_Send();
        //if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "mailattach/" + lblEmpIdHidden.Text + ""))
        //{ Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "mailattach/" + lblEmpIdHidden.Text + "", true); }
    }


    /////////////////////////////////////// Report Selector

    #region Run Selected Type Report
    private string RunReport()
    {
        string returnFileName = "";
        switch (reportType)
        {
            case "quot":
                {
                    NewSalesQuotationReport("Test1", Request.QueryString["qno"].ToString());
                    returnFileName = mailAttachPath + "Quotation-" + Request.QueryString["qnono"].ToString() + ".pdf";
                    myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, returnFileName);
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
            case "orderacc":
                {
                    SalesOrderAcceptanceReport("SM_SalesOrderAcceptance", Request.QueryString["oano"].ToString());
                    break;
                }
            case "workorder":
                {
                    WorkOrderReport("SM_WorkOrder", Request.QueryString["wono"].ToString());
                    returnFileName = mailAttachPath + "InteralOrder-" + Request.QueryString["wonos"].ToString() + ".pdf";
                    myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, returnFileName);
                    break;
                    
                }
            case "salesinvoice":
                {
                    if (Request.QueryString["dcfor"].ToString() == "Sales")
                    {
                        SalesInvoiceReport("Inv_Invoice", Request.QueryString["siid"].ToString());
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
            case "claimform":
                {
                    ClaimFormReport("ClaimForm", Request.QueryString["cfid"].ToString());
                    break;
                }
            case "checkingformat":
                {
                    CheckingFormatReport("CheckingFormat", Request.QueryString["chkid"].ToString());
                    break;
                }
            case "purchaseorder":
                {
                    PurchaseOrderReport("PurchaseOrder", Request.QueryString["fpoid"].ToString());
                    break;
                }
            case "supplierworkorder":
                {
                    SupplierWorkOrderReport("SuppWorkOrderBody", Request.QueryString["supwoid"].ToString());
                    break;
                }
            case "servicerpt":
                {
                    ServiceReport("ServiceRpt", Request.QueryString["srid"].ToString());
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

            case "Selfpurchaseorder":
                {
                    SelfPurchaseOrder("SelfPurchaseOrder", Request.QueryString["fpoid"].ToString());
                    returnFileName = mailAttachPath + "PurchaseOrder-" + Request.QueryString["fpoid"].ToString() + ".pdf";
                    myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, returnFileName);
                    break;
                    
                }

            case "SelfpurchaseorderImport":
                {
                    SelfPurchaseOrder("LocalSelfPurchaseOrder", Request.QueryString["fpoid"].ToString());
                    returnFileName = mailAttachPath + "PurchaseOrder-" + Request.QueryString["fpoid"].ToString() + ".pdf";
                    myRep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, returnFileName);
                    break;

                }

            default:
                {
                    MessageBox.Show(this, "Under Construction");
                    break;
                }
        }
        return returnFileName;
    }
    #endregion


    ////////////////////////////////////Sales & Marketing

    #region New Sales Quotation Report
    private void NewSalesQuotationReport(string ReportName, string QuotId)
    {
        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));



            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());
            // SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET", DBConString.ConnectionString());
            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST  ", DBConString.ConnectionString());
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
            da6.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da7.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da8.Fill(rds, "YANTRA_LKUP_COLOR_MAST");
            da9.Fill(rds, "YANTRA_ITEM_IMAGE");
            da10.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");


            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_DET.QUOT_ID}=" + QuotId + "   ";
                // myRep.SummaryInfo.ReportTitle = "Quotation -" + QuotId+  ""  ;
               
            }
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
        //try
        //{
        //    myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));

        //    SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
        //    SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
        //    SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
        //    SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());
        //    SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
        //    SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());
        //    SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());
        //    SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET", DBConString.ConnectionString());
        //    SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
        //    SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());
        //    SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());
        //    SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
        //    SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());

        //    YANTRADataSet rds = new YANTRADataSet();
        //    da.Fill(rds, "YANTRA_COMP_PROFILE");
        //    da1.Fill(rds, "YANTRA_ENQ_ASSIGN_TASKS");
        //    da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
        //    da3.Fill(rds, "YANTRA_ENQ_MAST");
        //    da4.Fill(rds, "YANTRA_ITEM_MAST");
        //    da5.Fill(rds, "YANTRA_LKUP_ENQ_MODE");
        //    da6.Fill(rds, "YANTRA_QUOT_MAST");
        //    da7.Fill(rds, "YANTRA_QUOT_DET");
        //    da8.Fill(rds, "YANTRA_LKUP_DESP_MODE");
        //    da9.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");
        //    da10.Fill(rds, "YANTRA_EMPLOYEE_MAST");
        //    da11.Fill(rds, "YANTRA_CUSTOMER_UNITS");
        //    da12.Fill(rds, "YANTRA_CUSTOMER_DET");

        //    myRep.SetDataSource(rds);
        //    myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_ID}=" + QuotId + "";

        //    //
        //    //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");

        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}

        try
        {
            myRep.Load(Server.MapPath("SM/" + ReportName + ".rpt"));


            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ENQ_ASSIGN_TASKS", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_ENQ_MAST", DBConString.ConnectionString());

            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST where ITEM_CODE IN (SELECT ITEM_CODE FROM YANTRA_QUOT_DET WHERE QUOT_ID=" + QuotId + ")", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_ENQ_MODE", DBConString.ConnectionString());

            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_QUOT_MAST", DBConString.ConnectionString());//

            SqlDataAdapter da6 = new SqlDataAdapter("Select * from YANTRA_QUOT_DET order by QUOT_DET_ID", DBConString.ConnectionString());
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

            if (rds != null)
            {
                myRep.SetDataSource(rds);
                myRep.RecordSelectionFormula = "{YANTRA_QUOT_MAST.QUOT_ID}=" + QuotId + "";

                //CrystalReportViewer1.ReportSource = myRep;
                //CrystalReportViewer1.RefreshReport();
            }
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
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_WO_MAST.WO_ID}=" + WOId + "";
           // CrystalReportViewer1.ReportSource = myRep;
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

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Claim Form");
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
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_LKUP_CURRENCY_TYPE", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_FIXED_PO_MAST");
            da2.Fill(rds, "YANTRA_FIXED_PO_DET");
            da3.Fill(rds, "YANTRA_SUPPLIER_MAST");
            da4.Fill(rds, "YANTRA_LKUP_DESP_MODE");
            da5.Fill(rds, "YANTRA_LKUP_UOM");
            da7.Fill(rds, "YANTRA_ITEM_MAST");
            da8.Fill(rds, "YANTRA_COMP_PROFILE");
            da9.Fill(rds, "YANTRA_LKUP_CURRENCY_TYPE");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_FIXED_PO_MAST.FPO_ID}=" + FPOId + "";


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

           // CrystalReportViewer1.ReportSource = myRep;
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
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_DELIVERY_CHALLAN_MAST", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_LKUP_DESP_MODE", DBConString.ConnectionString());
            SqlDataAdapter da11 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da12 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());

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

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SALES_INVOICE_MAST.SI_ID}=" + SIId + "";

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
            SqlDataAdapter da7 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da8 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString());
            SqlDataAdapter da9 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_UNITS", DBConString.ConnectionString());
            SqlDataAdapter da10 = new SqlDataAdapter("Select * from YANTRA_CUSTOMER_DET", DBConString.ConnectionString());


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


            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_DELIVERY_CHALLAN_MAST.DC_ID}=" + DCId + "";

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
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from  YANTRA_DELIVERY_CHALLAN_DET", DBConString.ConnectionString());
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

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    /////////////////////////////////////Services

    #region Services Report
    private void ServiceReport(string ReportName, string SRId)
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


            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_ITEM_MAST");
            da2.Fill(rds, "YANTRA_CUSTOMER_MAST");
            da3.Fill(rds, "YANTRA_CUSTOMER_UNITS");
            da4.Fill(rds, "YANTRA_CUSTOMER_DET");
            da5.Fill(rds, "YANTRA_COMPLAINT_REGISTER");
            da6.Fill(rds, "YANTRA_SERVICE_REPORT_MAST");

            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_SERVICE_REPORT_MAST.SR_ID}=" + SRId + "";

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
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
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
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
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




            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

}

 
