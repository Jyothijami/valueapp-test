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
using vllib;
using System.Data.SqlClient;
using System.Configuration;

public partial class Interno_Interno : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filename = FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath(filename));
            ExportToGrid(Server.MapPath(filename));
            //GridView1.DataBind();
        }
        else
        {
            MessageBox.Show(this, "Please Select A Location & File To Upload");
        }
    }

    void ExportToGrid(String path)
    {
        OleDbConnection MyConnection = null;
        DataSet DtSet = null;
        OleDbDataAdapter MyCommand = null;
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
                string sql;

                HR.EmployeeMaster hai = new HR.EmployeeMaster();
                sql = @"Insert into Interno_Det (DetId,Id,AA,Package,OrderNo,CustOrder,Code,Descr,MU1,Qty,UP,Total,Length,Bars,MET,Weight,KGR) values
                    ('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "','" + dr.ItemArray[2].ToString() + "','" + dr.ItemArray[3].ToString() + "','" + dr.ItemArray[4].ToString() + "','" + dr.ItemArray[5].ToString() + "','" + dr.ItemArray[6].ToString() + "','" + dr.ItemArray[7].ToString() + "','" + dr.ItemArray[8].ToString()
                      + "','" + dr.ItemArray[9].ToString() + "','" + dr.ItemArray[10].ToString() + "','" + dr.ItemArray[11].ToString() + "','" + dr.ItemArray[12].ToString() + "','" + dr.ItemArray[13].ToString() + "','" + dr.ItemArray[14].ToString() + "'," + dr.ItemArray[15].ToString() + "," + dr.ItemArray[16].ToString() + ")" ;
                obj.ReturnExecuteNoneQuery(sql);


            }

        }
        MessageBox.Show(this, "Data Inserted Successfully");
    }
    protected void lbtnIndentNo_Click(object sender, EventArgs e)
    {
        LinkButton lbnIndentNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbnIndentNo.Parent.Parent;
        gvInterno.SelectedIndex = Row.RowIndex;
        //Response.Redirect("ChangedIndentDetails.aspx?IndentId=" + gvIndentDetails.SelectedRow.Cells[0].Text +
        //        "&AppBy=" + gvIndentDetails.SelectedRow.Cells[5].Text);



        //Old Code
        //tblIndentDetails.Visible = false;
        LinkButton lbtnIndentNo;
        lbtnIndentNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIndentNo.Parent.Parent;
        gvInterno.SelectedIndex = gvRow.RowIndex;


    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        tblpRint.Visible = true;
    }

    protected void chkIn_CheckedChanged(object sender, EventArgs e)
    {
        if (gvInterno.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Modules/Reports/SMReportViewer.aspx?type=InternoIn&DCId=" + gvInterno.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void chkPL_CheckedChanged(object sender, EventArgs e)
    {
        if (gvInterno.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Modules/Reports/SMReportViewer.aspx?type=InternoPL&DCId=" + gvInterno.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
}