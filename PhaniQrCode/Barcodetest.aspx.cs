using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Data;


public partial class Barcodetest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // grd.DataSource = GetTableWithInitialData(); // get first initial data
            grd.DataBind();
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        //string code = "Item Code :" + txtCode.Text + "\t" + "Model No:" + txt2.Text + "\t" + txt3.Text + "\t" + txt4.Text;

        string code = txtCode.Text + "\t" +  txt2.Text + "\t" + txt3.Text  ;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            plBarCode.Controls.Add(imgBarCode);
        }
    }




    public DataTable GetTableWithInitialData() // this might be your sp for select
    {
        DataTable table = new DataTable();
        table.Columns.Add("PayScale", typeof(string));
        table.Columns.Add("IncrementAmount", typeof(string));
        table.Columns.Add("Period", typeof(string));

        table.Rows.Add(1, "David", "1");
        table.Rows.Add(2, "Sam", "2");
        table.Rows.Add(3, "Christoff", "1.5");
        return table;
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        DataTable dt = GetTableWithNoData(); // get select column header only records not required
        DataRow dr;

        foreach (GridViewRow gvr in grd.Rows)
        {
            dr = dt.NewRow();

            TextBox txtPayScale = gvr.FindControl("txtPayScale") as TextBox;
            TextBox txtIncrementAmount = gvr.FindControl("txtIncrementAmount") as TextBox;
            TextBox txtPeriod = gvr.FindControl("txtPeriod") as TextBox;

            dr[0] = txtPayScale.Text;
            dr[1] = txtIncrementAmount.Text;
            dr[2] = txtPeriod.Text;

            dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
        }

        for (int i = 0; i < 2; i++)
        {
            dr = dt.NewRow(); // add last empty row
            dt.Rows.Add(dr);
        }

      

        grd.DataSource = dt; // bind new datatable to grid
        grd.DataBind();
    }

    public DataTable GetTableWithNoData() // returns only structure if the select columns
    {
        DataTable table = new DataTable();
        table.Columns.Add("PayScale", typeof(string));
        table.Columns.Add("IncrementAmount", typeof(string));
        table.Columns.Add("Period", typeof(string));
        return table;
    }


































    //private void SetInitialRow()
    //{
    //    DataTable dt = new DataTable();
    //    DataRow dr = null;
    //    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Column1", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Column2", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Column3", typeof(string)));
    //    dr = dt.NewRow();
    //    dr["RowNumber"] = 1;
    //    dr["Column1"] = string.Empty;
    //    dr["Column2"] = string.Empty;
    //    dr["Column3"] = string.Empty;
    //    dt.Rows.Add(dr);

    //    //Store the DataTable in ViewState
    //    ViewState["CurrentTable"] = dt;

    //    Gridview1.DataSource = dt;
    //    Gridview1.DataBind();
    //    AddNewRowToGrid();
    //}
    //private void AddNewRowToGrid()
    //{
    //    int rowIndex = 0;

    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        DataRow drCurrentRow = null;
    //        if (dtCurrentTable.Rows.Count > 0)
    //        {
    //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
    //            {
    //                //extract the TextBox values
    //                TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
    //                TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
    //                TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

    //                drCurrentRow = dtCurrentTable.NewRow();
    //                drCurrentRow["RowNumber"] = i + 1;

    //                dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
    //                dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
    //                dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;

    //                rowIndex++;
    //            }
    //            dtCurrentTable.Rows.Add(drCurrentRow);
    //            ViewState["CurrentTable"] = dtCurrentTable;

    //            Gridview1.DataSource = dtCurrentTable;
    //            Gridview1.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("ViewState is null");
    //    }

    //    //Set Previous Data on Postbacks
    //    SetPreviousData();
    //}
    //private void SetPreviousData()
    //{
    //    int rowIndex = 0;
    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
    //                TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
    //                TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

    //                box1.Text = dt.Rows[i]["Column1"].ToString();
    //                box2.Text = dt.Rows[i]["Column2"].ToString();
    //                box3.Text = dt.Rows[i]["Column3"].ToString();

    //                rowIndex++;
    //            }
    //        }
    //    }
    //}
    //protected void ButtonAdd_Click(object sender, EventArgs e)
    //{
    //    AddNewRowToGrid();
    //}












    protected void txtPeriod_TextChanged(object sender, EventArgs e)
    {
        btnAddRow_Click(sender, e);
    }
}