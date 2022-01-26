using phaniDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;

public partial class Modules_Warehouse_StockVerification : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    private static int _returnIntValue;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvPhyVerif.DataBind();
        }
    }
   
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnfileUpload_Click(object sender, EventArgs e)
    {
        General obj = new General();
        if (FileUpload1.HasFile)
        {
            string filename = FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath(filename));

            string PrepaedBy = Yantra.Authentication.GetEmployeeInSession(Yantra .Authentication.Logged_EMP_Details.EmpId);
            ExportToGrid(Server.MapPath(filename));
        }
        else
        {
            MessageBox.Show(this, "File name Already Exist");
        }
    }
    private void ExportToGrid(string path)
    {
        OleDbConnection MyConnection = null;
        DataSet DtSet = null;
        OleDbDataAdapter MyCommand = null;
        MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties='Excel 12.0;IMEX=1'");


        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
        DtSet = new System.Data.DataSet();
        MyCommand.Fill(DtSet, "[Sheet1$]");
        dt = DtSet.Tables[0];
        MyConnection.Close();

        General obj = new General();

        if (dt.Rows.Count > 0)
        {
            bool found = false;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.ItemArray[0].ToString() == "")
                {
                    found = true;
                    MessageBox.Show(this, "Please Check Excel Having Correct Values");
                    break;
                }
                else
                {
                    string ItemCode = "0";
                    string ColorId = "0";
                    string BrandId = "0";
                    string IcId = "0";
                    string GodownId = "0";
                    string sql = @"select ITEM_CODE,BRAND_ID,IC_ID  from  YANTRA_ITEM_MAST where ITEM_MODEL_NO = '" + Convert.ToString(dr.ItemArray[0]).Trim().ToString() + "' ";
                    DataTable dttemp = obj.ReturnDataTable(sql);
                    if (dttemp.Rows.Count > 0)
                    {
                        ItemCode = dttemp.Rows[0]["ITEM_CODE"].ToString();
                        BrandId = dttemp.Rows[0]["BRAND_ID"].ToString();
                        IcId = dttemp.Rows[0]["IC_ID"].ToString();

                        //get colour id
                        string sql1 = @"select Colour_Id  from  YANTRA_LKUP_COLOR_MAST where COLOUR_NAME = '" + Convert.ToString(dr.ItemArray[1]).Trim().ToString() + "'  and BRAND_ID = '" + BrandId  + "' and IC_ID = '" + IcId  + "'  ";
                        DataTable dttemp1 = obj.ReturnDataTable(sql1);
                        if (dttemp1.Rows.Count > 0)
                        {
                            ColorId = dttemp1.Rows[0]["Colour_Id"].ToString();
                        }
                        else
                        {
                            ColorId = "0";
                        }
                        GodownId = ddlLocation.SelectedItem.Value;
                        string AppOnDate = DateTime.Now.ToString();
                        string ExcelOnDate = DateTime.Now.ToString();
                        //get app stock 
                        SqlCommand cmd = new SqlCommand("[USP_PhyStockVerfication]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@ModelNo", ItemCode);
                        cmd.Parameters.AddWithValue("@Color", ColorId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt1 = new DataTable();
                        da.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            sql = @"Insert into PhyStockVerification_tbl (Item_Code,Brand_Id,Color_Id,Wh_Id,AppOS,AppInward,AppOutward,AppBlocked,AppAvailStock,AppCS,AppOnDate,ExcelOS,ExcelInward,ExcelOutward,ExcelBlocked,ExcelAvailStock,ExcelCS,ExcelOnDate,ExcelLocation)  
                                values ('" + ItemCode + "','" + BrandId + "','" + ColorId + "','" + GodownId + "','" + dt1.Rows[0]["Opening Stock"] + "','" + dt1.Rows[0]["Total Inward Stock"] + "','" + dt1.Rows[0]["Total Outward Stock"] + "','" + dt1.Rows[0]["Total Block Stock"] +
                          "','" + dt1.Rows[0]["Total Available Stock"] + "','" + dt1.Rows[0]["Closing Stock"] + "','" + AppOnDate + "','" + dr.ItemArray[2].ToString() + "','" + dr.ItemArray[3].ToString() + "','" + dr.ItemArray[4].ToString() + "','" + dr.ItemArray[5].ToString() +
                          "','" + dr.ItemArray[6].ToString() + "','" + dr.ItemArray[7].ToString() + "','" + ExcelOnDate + "','" + dr.ItemArray[8].ToString() + "')";
                            obj.ReturnExecuteNoneQuery(sql);
                        }
                    }
                    else
                    {
                        string Code = Convert.ToString(dr.ItemArray[0]).Trim().ToString();
                        MessageBox.Show(this, "Item Code Missing " + Code + "");



                        break;
                    }
                }
            }
        }
    }
}