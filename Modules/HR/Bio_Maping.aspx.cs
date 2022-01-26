using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using YantraBLL.Modules;
using YantraDAL;
using Yantra.MessageBox;
using vllib;
public partial class Modules_HR_Bio_Maping : basePage
{
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filename = FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath(filename));
            ExportToGrid(Server.MapPath(filename));
            GridView1.DataBind();
        }
    }


    void ExportToGrid(String path)
    {
        OleDbConnection MyConnection = null;
        DataSet DtSet = null;
        OleDbDataAdapter MyCommand = null;
        //MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties=Excel 8.0;");
        //use below connection string if your excel file .xslx 2007 format
        MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0;");


        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
        DtSet = new DataSet();
        MyCommand.Fill(DtSet, "[Sheet1$]");
        dt = DtSet.Tables[0];
        MyConnection.Close();
        General obj = new General();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.ItemArray[0].ToString() != "" )
                {
                    HR.EmployeeMaster hai = new HR.EmployeeMaster();
                    if(hai.IsRecordExists(dr.ItemArray[0].ToString()) == "0")
                    //if (IsRecordExists("[EMP_BIO_MAP]", "emp_code", dr.ItemArray[0].ToString()) == false)
                    {
                        string sql;
                        sql = @"insert into EMP_BIO_MAP (emp_code,emp_name,app_emp_id,app_emp_name)  values 
                        ('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "','0','0' ) ";
                        obj.ReturnExecuteNoneQuery(sql);
                    }
                }
               
            }

        }
        
    }




    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlemp = (DropDownList)e.Row.FindControl("ddlemp");
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlemp);
            HR.EmployeeMaster.EmployeeMaster_Select_BioMap(ddlemp);

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        HR.EmployeeMaster obj = new HR.EmployeeMaster();
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            DropDownList d1 = gvrow.FindControl("ddlemp") as DropDownList;
            if (d1.SelectedItem.Value != "0")
            {
                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    obj.appempid = d1.SelectedItem.Value;
                    obj.appempname = d1.SelectedItem.Text;
                    obj.empcode = gvrow.Cells[0].Text;
                    obj.empname = gvrow.Cells[1].Text;
                    obj.biomapping_update();
                }
            }
        }
        MessageBox.Show(this, "Data Updated Successfully");
        GridView1.DataBind();
    }


    private static int _returnIntValue;
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
   
    //private static bool IsRecordExists(string paraTableName, string paraFirstFieldName, string paraFirstFieldValue, string paraSecondFieldName, string paraSecondFieldValue)
    //{
    //    bool check = false;
    //    _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFirstFieldName + "='" + paraFirstFieldValue + "' and " + paraSecondFieldName + "<>'" + paraSecondFieldValue + "'").ToString());
    //    if (_returnIntValue > 0)
    //    {
    //        check = true;
    //    }
    //    else
    //    {
    //        check = false;
    //    }
    //    return check;
    //}
    protected void btnGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = "emp_name";       
        lblSearchValueHidden.Text = txtEmpName.Text;
        GridView1.DataBind();
        txtEmpName.Text = "";
    }
}
 
