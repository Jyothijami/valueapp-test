using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraDAL;

public partial class Mst : System.Web.UI.Page
{

    private DataTable dt = new DataTable();
    private static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        General obj = new General();
        if (FileUpload1.HasFile)
        {
            string filename = FileUpload1.FileName;
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
        MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0;");

        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
        DtSet = new System.Data.DataSet();
        MyCommand.Fill(DtSet, "[Sheet1$]");
        dt = DtSet.Tables[0];
        MyConnection.Close();

        General obj = new General();

        if (dt.Rows.Count > 0)
        {
            // bool found = false;

            foreach (DataRow dr in dt.Rows)
            {
                // Getting Color Id
                //string sql = @"select COLOR_ID from YANTRA_ITEM_COLOR_DETAILS where ITEM_CODE = '" + Convert.ToString(dr.ItemArray[0]).Trim().ToString() + "'  ";
                //DataTable dttemp = obj.ReturnDataTable(sql);
                //if (dttemp.Rows.Count > 0)
                //{
                    //string ItemCode = "0";
                    //ItemCode = dttemp.Rows[0]["Material_Id"].ToString();
                    //gETTING cOLOR iD

                    //string sql1 = @"select COLOR_ID  from  YANTRA_ITEM_COLOR_DETAILS where ITEM_CODE = '" + Convert.ToString(dr.ItemArray[0]).Trim().ToString() + "'  ";
                    //DataTable dttemp1 = obj.ReturnDataTable(sql1);
                    //string ColorCode = "0";
                    //ColorCode = dttemp1.Rows[0]["COLOR_ID"].ToString();

                    

                    //Getting Length from Stock
                    //string sql2 = @"select Length  from  Stock where Length = '" + Convert.ToString(dr.ItemArray[5]).Trim().ToString() + "'  ";
                    //DataTable dttemp2 = obj.ReturnDataTable(sql2);
                 
                    //string sql2 = @"select CP_ID  from  YANTRA_COMP_PROFILE where CP_SHORT_NAME = '" + Convert.ToString(dr.ItemArray[4]).Trim().ToString() + "'  ";
                    //DataTable dttemp2 = obj.ReturnDataTable(sql2);
                    string CompanyId = "1";
                    //CompanyId = dttemp2.Rows[0]["CP_ID"].ToString();


                    string sql4 = @"select wh_id , whname1= locname +','+whname from warehouse_tbl inner join location_tbl on warehouse_tbl .locid =location_tbl .locid   where locname +','+whname = '" + Convert.ToString(dr.ItemArray[2]).Trim().ToString() + "'  ";
                    DataTable dttemp4 = obj.ReturnDataTable(sql4);
                    string Whlocid = "0";
                    Whlocid = dttemp4.Rows[0]["wh_id"].ToString();


                    string sql = @"insert into Audit_InventoryMasters(InventoryID,ItemCode,ColorId, CpId,WhLocId,RemainingQty,InQty,OutQty,ResultQty,AuditDate,AuditType)
                                values('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[0].ToString() + "', '" + dr.ItemArray[3].ToString() + "', '" + CompanyId + "','" + Whlocid + "',    '" + dr.ItemArray[1].ToString() + "', '0','0', '" + dr.ItemArray[1].ToString() + "',getdate(), 'I') ";
                    obj.ReturnExecuteNoneQuery(sql);


                  


             

            }
        }
    }


}