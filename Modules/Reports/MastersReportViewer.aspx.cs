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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using Yantra.MessageBox;
using YantraDAL;

public partial class Modules_Reports_MastersReportViewer : System.Web.UI.Page
{
    string reportType;
    ReportDocument myRep = new ReportDocument();
    DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] != null)
        {
            reportType = Request.QueryString["type"].ToString();
        }

        RunReport();
    }



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
            case "pm":
                {
                    AMCQuotationReport("ProductMasterReport", Request.QueryString["pno"].ToString());
                    break;
                }
            case "Memo":
                {
                    MemoReport("Memo", Request.QueryString["Mid"].ToString());
                    break;
                }
            case "Circular":
                {
                    CircularReport("Circular", Request.QueryString["Cid"].ToString());
                    break;
                }
            case "CircularDept":
                {
                    CircularDeptReport("CircularDept", Request.QueryString["Cid"].ToString(), Request.QueryString["Did"].ToString());
                    break;
                }
            case "CircularForAll":
                {
                    CircularForAllReport("CircularForAll", Request.QueryString["Cid"].ToString());
                    break;
                }
            case "ItemDetils":
                {
                   
                    ItemTypeReport("ItemType");
                    break;
                }
            case "ItemMaster":
                {
                    ItemMasterReport("ItemMaster",Request.QueryString["b"].ToString(),Request.QueryString["c"].ToString(),Request.QueryString["s"].ToString(),Request.QueryString["m"].ToString());
                    break;
                }
            case "ItemMasterWithoutDrawing":
                {
                    ItemMasterReportWithoutDrawings("ItemMasterWithoutDrawing", Request.QueryString["b"].ToString(), Request.QueryString["c"].ToString(), Request.QueryString["s"].ToString(), Request.QueryString["m"].ToString());
                    break;
                }
            case "ItemPrice":
                {
                    ItemPriceReport("ItemPrice", Request.QueryString["b"].ToString(), Request.QueryString["f"].ToString());
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

    #region Memo
    private void MemoReport(string ReportName, string mid)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_EMP_MEMO", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString()); //

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString()); //
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_COMP_PROFILE", DBConString.ConnectionString()); //

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_DESG_MAST");
            da1.Fill(rds, "YANTRA_DEPT_MAST");
            da2.Fill(rds, "YANTRA_EMP_MEMO");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");//
            da4.Fill(rds, "YANTRA_EMPLOYEE_DET");//
            da5.Fill(rds, "YANTRA_COMP_PROFILE");
            myRep.SetDataSource(rds);
            myRep.RecordSelectionFormula = "{YANTRA_EMP_MEMO.Memo_Id}=" + mid + "";

            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
     #endregion

    #region Circular
    private void CircularReport(string ReportName, string Cid)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_HR_CIRCULAR", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_DESG_MAST");
            da1.Fill(rds, "YANTRA_DEPT_MAST");
            da2.Fill(rds, "YANTRA_HR_CIRCULAR");
            da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            da4.Fill(rds, "YANTRA_EMPLOYEE_DET");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_ITEM_MAST.ITEM_CODE}= {YANTRA_ITEM_QTY.ITEM_CODE}";
            myRep.RecordSelectionFormula = "{YANTRA_HR_CIRCULAR.CIR_ID}=" + Cid + "";

            CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion


    #region CircularForAllReport
    private void CircularForAllReport(string ReportName, string Cid)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            //SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            //SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_HR_CIRCULAR", DBConString.ConnectionString());
            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            //da.Fill(rds, "YANTRA_DESG_MAST");
            //da1.Fill(rds, "YANTRA_DEPT_MAST");
            da2.Fill(rds, "YANTRA_HR_CIRCULAR");
            //da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da4.Fill(rds, "YANTRA_EMPLOYEE_DET");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_ITEM_MAST.ITEM_CODE}= {YANTRA_ITEM_QTY.ITEM_CODE}";
            myRep.RecordSelectionFormula = "{YANTRA_HR_CIRCULAR.CIR_ID}=" + Cid + "";

            CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion
    #region CircularDept
    private void CircularDeptReport(string ReportName, string Cid, string Dpid)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            //SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_DESG_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_DEPT_MAST", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_HR_CIRCULAR", DBConString.ConnectionString());
            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_MAST", DBConString.ConnectionString());

            //SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_EMPLOYEE_DET", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            //da.Fill(rds, "YANTRA_DESG_MAST");
            da1.Fill(rds, "YANTRA_DEPT_MAST");
            da2.Fill(rds, "YANTRA_HR_CIRCULAR");
            //da3.Fill(rds, "YANTRA_EMPLOYEE_MAST");
            //da4.Fill(rds, "YANTRA_EMPLOYEE_DET");

            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_ITEM_MAST.ITEM_CODE}= {YANTRA_ITEM_QTY.ITEM_CODE}";
            myRep.RecordSelectionFormula = "{YANTRA_HR_CIRCULAR.CIR_ID}=" + Cid + "";

            CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Item Type
    private void ItemTypeReport(string ReportName)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());
            
            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            da1.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
           
            myRep.SetDataSource(rds);
            //myRep.RecordSelectionFormula = "{YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID}=" + Iid + "";
            //myRep.RecordSelectionFormula = "{YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID}=" + Iid + "";

            CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion

    #region Item Master
    private void ItemMasterReport(string ReportName,string brand,string category,string subcategory,string modelno)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_ITEM_MAST");
            da1.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
            da2.Fill(rds, "YANTRA_LKUP_UOM");
            da3.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            da4.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "tonumber({YANTRA_ITEM_MAST.IC_ID})={YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID}";
            if (brand != "0" && category != "0" && subcategory != "0" && modelno!= "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            if (brand != "0" && category != "0" && subcategory != "0" )
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            if (brand != "0" && category != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            if (brand != "0" && subcategory != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula +  " AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            if (category != "0" && subcategory != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory +  " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            else if (brand != "0" && category != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (brand != "0" && subcategory != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (brand != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (category != "0" && subcategory != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + "";
            }
            else if (category != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "'";
            }
            else if (brand != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            else if (subcategory != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + "";
            }
            else 
            {
                 myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "";
            }


            CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion  
    
    #region Item Master
    private void ItemMasterReportWithoutDrawings(string ReportName, string brand, string category, string subcategory, string modelno)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_TYPE", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());
            SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());
            SqlDataAdapter da5 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_RATE_MASTER", DBConString.ConnectionString());

            YANTRADataSet rds = new YANTRADataSet();

            da.Fill(rds, "YANTRA_ITEM_MAST");
            da1.Fill(rds, "YANTRA_LKUP_ITEM_TYPE");
            da2.Fill(rds, "YANTRA_LKUP_UOM");
            da3.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            da4.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            da5.Fill(rds, "YANTRA_LKUP_ITEM_RATE_MASTER");
            myRep.SetDataSource(rds);

            myRep.RecordSelectionFormula = "tonumber({YANTRA_ITEM_MAST.IC_ID})={YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID}";
            if (brand != "0" && category != "0" && subcategory != "0" && modelno!= "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            if (brand != "0" && category != "0" && subcategory != "0" )
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            if (brand != "0" && category != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            if (brand != "0" && subcategory != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula +  " AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            if (category != "0" && subcategory != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory +  " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            else if (brand != "0" && category != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (brand != "0" && subcategory != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (brand != "0" && modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (category != "0" && subcategory != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "' AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + "";
            }
            else if (category != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IC_ID}='" + category + "'";
            }
            else if (brand != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            }
            else if (modelno != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.ITEM_CODE}=" + modelno + "";
            }
            else if (subcategory != "0")
            {
                myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.IT_TYPE_ID}=" + subcategory + "";
            }
            else 
            {
                 myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "";
            }


            CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion  

    #region ItemPriceReport
    private void ItemPriceReport1(string ReportName, string brand, string finan)
    {
        try
        {
            //myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

           ReportDocument reportDocument = new ReportDocument();
           ParameterField paramField = new ParameterField();
           ParameterFields paramFields = new ParameterFields();
           ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

          //Set instances for input parameter 1 -  @vDepartment
            paramField.Name = "@brand";
         //Below variable can be set to any data 
         //present in SalseData table, Department column
            paramDiscreteValue.Value = brand; 
            paramField.CurrentValues.Add(paramDiscreteValue);
          //Add the paramField to paramFields
            paramFields.Add(paramField); 

	    //Set instances for input parameter 2 -  @iSalseYear
	    //*Remember to reconstruct the paramDiscreteValue and paramField objects
	    paramField = new ParameterField();
        paramField.Name = "@finan";
	    paramDiscreteValue = new ParameterDiscreteValue();
        paramDiscreteValue.Value = finan;
	    paramField.CurrentValues.Add(paramDiscreteValue);
	
	    //Add the paramField to paramFields
	    paramFields.Add(paramField);

        CrystalReportViewer1.ParameterFieldInfo = paramFields;
       
             reportDocument.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));
             ParameterFieldDefinitions pfd = reportDocument.DataDefinition.ParameterFields;
             
        //reportDocument.Load(@"..\..\..\Reports\SalseReport.rpt");
       
	
	    //set the database loggon information. 
	    //**Note that the third parameter is the DSN name 
	    //  and not the Database or System name
	    reportDocument.SetDatabaseLogon("sa", "root1234",
                                   "TestDB_DSN", "Yantra-Vlt", false);
	  //192.168.1.4
       


	    //Load the report by setting the report source
        CrystalReportViewer1.ReportSource = reportDocument;
        MessageBox.Show(this, "Report Loading...");



            ///SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            ///SqlDataAdapter da1 = new SqlDataAdapter("Select * from [YANTRA_LKUP_ITEM_RATE_MASTER]", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());
            ///SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());

            ///YANTRADataSet rds = new YANTRADataSet();

            ///da.Fill(rds, "YANTRA_ITEM_MAST");
            ///da1.Fill(rds, "YANTRA_LKUP_ITEM_RATE_MASTER");
            //da2.Fill(rds, "YANTRA_LKUP_UOM");
            //da3.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            ///da4.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
           /// myRep.SetDataSource(rds);

            ///myRep.RecordSelectionFormula = "{YANTRA_LKUP_ITEM_RATE_MASTER.FINANCIAL_YEAR}='" + finan + "'";
            
            ///if (brand != "0")
            ///{
               /// myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            ///}
           
      ///else
            ///{
               /// myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "";
            ///}


            ///CrystalReportViewer1.ReportSource = myRep;


       }
       catch (Exception ex)
      {
           MessageBox.Show(this, ex.Message.ToString());
       }
    }
    #endregion  

    #region ItemPriceReport
    private void ItemPriceReport(string ReportName, string brand, string finan)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));
            //dbManager.Open();
            //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ToString());          
            //con.Open();
            //CrystalReportViewer1.ReportSource = myRep;

            ConnectionInfo mycon = new ConnectionInfo();
            mycon.ServerName = "MADHU";
            mycon.DatabaseName ="Yantra-Vlt" ;

              
            mycon.UserID = "sa";
            mycon.Password = "root1234";

            ParameterField paramField = new ParameterField();
            ParameterFields paramFields = new ParameterFields();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            //Set instances for input parameter 1 -  @vDepartment
            paramField.Name = "@brand";
            //Below variable can be set to any data 
            //present in SalseData table, Department column
            paramDiscreteValue.Value = brand;
            paramField.CurrentValues.Add(paramDiscreteValue);
            //Add the paramField to paramFields
            paramFields.Add(paramField);

            //Set instances for input parameter 2 -  @iSalseYear
            //*Remember to reconstruct the paramDiscreteValue and paramField objects
            paramField = new ParameterField();
            paramField.Name = "@finan";
            paramDiscreteValue = new ParameterDiscreteValue();
            paramDiscreteValue.Value = finan;
            paramField.CurrentValues.Add(paramDiscreteValue);

            //Add the paramField to paramFields
            paramFields.Add(paramField);

           
            TableLogOnInfos mylogs = CrystalReportViewer1.LogOnInfo;
            for (int i = 0; i < mylogs.Count; i++)
            {
                TableLogOnInfo mylog = mylogs[i];
                mylog.ConnectionInfo = mycon ;
            }
            CrystalReportViewer1.ParameterFieldInfo = paramFields;


            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection =con;
            //SqlDataAdapter da = new SqlDataAdapter();
            //DataSet ds = new DataSet();
            //cmd.CommandText = "SP_REPORTS_PRICE_LIST_SEARCH_SELECT";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@brand", brand);
            //cmd.Parameters.AddWithValue("@finan", finan);
            //da.SelectCommand = cmd;
            //da.Fill(ds);
            //myRep.SetDataSource(ds);0

            
            //dbManager.CreateParameters(2);
            //dbManager.AddParameters(0, "@brand", brand);
            //dbManager.AddParameters(1, "@finan", finan);
            //dbManager.ExecuteReader(CommandType.StoredProcedure, "SP_REPORTS_PRICE_LIST_SEARCH_SELECT");
            //myRep.SetDataSource(dbManager.DataReader);
            CrystalReportViewer1.ReportSource = myRep;
            
            //SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            ///SqlDataAdapter da1 = new SqlDataAdapter("Select * from [YANTRA_LKUP_ITEM_RATE_MASTER]", DBConString.ConnectionString());
            //SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_LKUP_UOM", DBConString.ConnectionString());
            //SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_LKUP_ITEM_CATEGORY", DBConString.ConnectionString());
            ///SqlDataAdapter da4 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_COMPANY", DBConString.ConnectionString());

            ///YANTRADataSet rds = new YANTRADataSet();

            ///da.Fill(rds, "YANTRA_ITEM_MAST");
            ///da1.Fill(rds, "YANTRA_LKUP_ITEM_RATE_MASTER");
            //da2.Fill(rds, "YANTRA_LKUP_UOM");
            //da3.Fill(rds, "YANTRA_LKUP_ITEM_CATEGORY");
            ///da4.Fill(rds, "YANTRA_LKUP_PRODUCT_COMPANY");
            /// myRep.SetDataSource(rds);

            ///myRep.RecordSelectionFormula = "{YANTRA_LKUP_ITEM_RATE_MASTER.FINANCIAL_YEAR}='" + finan + "'";

            ///if (brand != "0")
            ///{
            /// myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + " AND {YANTRA_ITEM_MAST.BRAND_ID}=" + brand + "";
            ///}

            ///else
            ///{
            /// myRep.RecordSelectionFormula = myRep.RecordSelectionFormula + "";
            ///}


            ///CrystalReportViewer1.ReportSource = myRep;


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally 
        {
            

        }
    }
    #endregion  

    #region AMC Quotation Report
    private void AMCQuotationReport(string ReportName, string ProductId)
    {
        try
        {
            myRep.Load(Server.MapPath("Masters/" + ReportName + ".rpt"));

            SqlDataAdapter da = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_DETAILS", DBConString.ConnectionString());
            SqlDataAdapter da2 = new SqlDataAdapter("Select * from YANTRA_LKUP_PRODUCT_MASTER", DBConString.ConnectionString());
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from YANTRA_ITEM_MAST", DBConString.ConnectionString());
            
            YANTRADataSet rds = new YANTRADataSet();
            da.Fill(rds, "YANTRA_LKUP_PRODUCT_DETAILS");
            da2.Fill(rds, "YANTRA_LKUP_PRODUCT_MASTER");
            da3.Fill(rds, "YANTRA_ITEM_MAST");
            

            myRep.SetDataSource(rds);
           // myRep.RecordSelectionFormula = "YANTRA_LKUP_PRODUCT_DETAILS,YANTRA_LKUP_PRODUCT_MASTER,YANTRA_ITEM_MAST where YANTRA_LKUP_PRODUCT_DETAILS.Product_Id = YANTRA_LKUP_PRODUCT_MASTER.Product_Id and YANTRA_LKUP_PRODUCT_DETAILS.ItemCode = YANTRA_ITEM_MAST.ITEM_CODE and YANTRA_LKUP_PRODUCT_MASTER.Product_Id=" + ProductId + "";
            myRep.RecordSelectionFormula = "{YANTRA_LKUP_PRODUCT_MASTER.Product_Id}=" + ProductId + "";
            CrystalReportViewer1.ReportSource = myRep;

            //myRep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "Sales Quotation");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    #endregion
}

 
