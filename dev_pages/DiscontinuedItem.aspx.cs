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
using YantraBLL.Modules;
using YantraDAL;

public partial class dev_pages_DiscontinuedItem : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    private static int _returnIntValue;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTotalTicketsRaised.Text = gvMatchedItems.Rows.Count.ToString();
        Label1.Text = gvNotMatched.Rows.Count.ToString();
        Label2.Text = GridView1.Rows.Count.ToString();
    }
    protected void btnfileUpload_Click(object sender, EventArgs e)
    {
        General obj = new General();
        if (FileUpload1.HasFile)
        {
            string sql = @"select Filename  from  Excel_Price_File where Filename = '" + FileUpload1.FileName + "' ";
            DataTable dttemp3 = obj.ReturnDataTable(sql);

            if (dttemp3.Rows.Count >= 0)
            {
                string Sqls = @"insert into Excel_Price_File(Filename) values('" + FileUpload1.FileName + "')";
                string strreturn1 = obj.ReturnExecuteNoneQuery(Sqls);

                string filename = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath(filename));




                SM.SalesOrder smobjj = new SM.SalesOrder();
                smobjj.DiscExcel_Delete();

                smobjj.SOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                //smobjj.MumbaiStockDetails_Update();
                ExportToGrid(Server.MapPath(filename));
                //GVBind();
            }
            else
            {
                MessageBox.Show(this, "File name Already Exist");
            }
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
                    string Price = "0";
                    string filename = FileUpload1.FileName;

                    if (dr.ItemArray[1].ToString() == "")
                    {
                        Price = "0";
                    }
                    else
                    {
                        Price = dr.ItemArray[1].ToString();
                    }
                    string EmpName = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName );
                
                    string sql = @"insert into Discontinued_Items(Item_Model_No,Brand,Filename)
                                values('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "', '" + filename + "') ";
                    obj.ReturnExecuteNoneQuery(sql);
                    string sql2 = @"insert into discontinued_items_aud(Item_Model_No,Brand,Filename,updated_dt,Updated_By)
                                values('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "','" + filename + "', getdate(), '" + EmpName + "') ";
                    obj.ReturnExecuteNoneQuery(sql2);
                    MessageBox.Show(this, "Data Uploaded Successfully");
                    gvMatchedItems.DataBind();
                }

            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
         Masters.ItemPurchase obj = new Masters.ItemPurchase();
        General obj1 = new General();
        foreach (GridViewRow gvrow in gvMatchedItems.Rows)
        {
            int itemcode = int.Parse(gvrow.Cells[0].Text);
            string EmpName = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
            obj.DisconItem_Update(itemcode, EmpName);
            MessageBox.Show(this, "Data Updated Successfully");
        }
    }
    protected void lnkDisc_Click(object sender, EventArgs e)
    {
        pnlUpload.Visible = false;
        pnlDiscon.Visible = true;
    }
}