using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Ionic.Zip;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Yantra.Classes;
using System.Configuration;
using YantraBLL.Modules;
using YantraDAL;

public partial class ReadingBarcode : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtQR.Focus();
        }
    }


    //protected void btnAddRow_Click(object sender, EventArgs e)
    //{
    //    //DataTable dt = GetTableWithNoData(); // get select column header only records not required
    //    DataRow dr;

    //    foreach (GridViewRow gvr in grd.Rows)
    //    {
    //        dr = dt.NewRow();

    //        Label lblItemCode = gvr.FindControl("lblItemCode") as Label;
    //        Label lblModelNo = gvr.FindControl("lblModelNo") as Label;
    //        Label lblMRNNo = gvr.FindControl("lblMRNNo") as Label;
    //        Label lblMRNDt = gvr.FindControl("lblMRNDt") as Label;
    //        Label lblColorId = gvr.FindControl("lblColorId") as Label;
    //        Label lblColour = gvr.FindControl("lblColour") as Label;
    //        TextBox txtQty = gvr.FindControl("txtQty") as TextBox;
    //        //Label lblMRNDt = gvr.FindControl("lblMRNDt") as Label;
    //        dr[0] = lblItemCode.Text;
    //        dr[1] = lblModelNo.Text;
    //        dr[2] = lblMRNNo.Text;
    //        dr[3] = lblMRNDt.Text;
    //        dr[4] = lblColorId.Text;
    //        dr[5] = lblColour.Text;
    //        dr[6] = txtQty.Text;
    //        //dr[7] = lblMRNDt.Text;
    //        dt.Rows.Add(dr); // add grid values in to row and add row to the blank table
    //    }

    //    for (int i = 0; i < 1; i++)
    //    {
    //        dr = dt.NewRow(); // add last empty row
    //        dt.Rows.Add(dr);
    //    }

       

    //    grd.DataSource = dt; // bind new datatable to grid
    //    grd.DataBind();
    //}

    //public DataTable GetTableWithNoData() // returns only structure if the select columns
    //{
    //    DataTable table = new DataTable();
    //    table.Columns.Add("Item_Code", typeof(string));
    //    table.Columns.Add("ITEM_Model_No", typeof(string));
    //    table.Columns.Add("CHK_NO", typeof(string));
    //    table.Columns.Add("CHK_DATE", typeof(string));
    //    table.Columns.Add("CHK_DET_Color", typeof(string));
    //    table.Columns.Add("COlour_NAme", typeof(string));
    //    table.Columns.Add("Qty", typeof(string));
    //    //table.Columns.Add("CHK_DATE", typeof(string));
    //    return table;
    //}







    //protected void createzip_Click(object sender, EventArgs e)
    //{
    //    //if (fileupload.HasFile)
    //    //{
    //    //    string zipfilepath = Server.MapPath("~/ZipFolder/" + Path.GetFileName(fileupload.PostedFile.FileName));
    //    //    fileupload.SaveAs(zipfilepath);
    //    //    string secondPath = Server.MapPath("~/ZipFolder/");
    //    //    string[] x = Directory.GetFiles(secondPath);
    //    //    using (Ionic.Zip.ZipFile compress = new Ionic.Zip.ZipFile())
    //    //    {
    //    //        //string dateofcreation = DateTime.Now.ToString("y");
    //    //        //dateofcreation = dateofcreation.Replace("/", "");
    //    //        string dateofcreation = fileupload.PostedFile.FileName;

    //    //        compress.AddFiles(x, dateofcreation);
    //    //        compress.Save(Server.MapPath(dateofcreation + ".zip"));
    //    //        Label1.Text = "ZIP Created Successfully";
    //    //    }
    //    //    if (Label1.Text == "ZIP Created Successfully")
    //    //    {
    //    //        Array.ForEach(Directory.GetFiles(secondPath),
    //    //          delegate(string deleteFile) { File.Delete(deleteFile); });
    //    //    }
    //    //}  




    //    HttpFileCollection filecollect = Request.Files;
    //    for (int i = 0; i < filecollect.Count; i++)
    //    {
    //        HttpPostedFile hpf = filecollect[i];
    //        if (hpf.ContentLength > 0)
    //        {
    //            hpf.SaveAs(Server.MapPath("ZipFolder") + "\\" +
    //              System.IO.Path.GetFileName(hpf.FileName));
    //            Label2.Text += " <br/> <b>File: </b>" + hpf.FileName +
    //              " <b>Size:</b> " + hpf.ContentLength + " <b>Type:</b> " +
    //              hpf.ContentType + "File Uploaded!";
    //        }
    //    }
    //    string zipfilepath = Server.MapPath("~/ZipFolder/");
    //    string[] x = Directory.GetFiles(zipfilepath);
    //    //using (Ionic.Zip.ZipFile compress = new Ionic.Zip.ZipFile())
    //    //{
    //    //    string dateofcreation = DateTime.Now.ToString("y");
    //    //    dateofcreation = dateofcreation.Replace("/", "");
    //    //    compress.AddFiles(x, dateofcreation);
    //    //    compress.Save(Server.MapPath(dateofcreation + ".zip"));
    //    //    Label1.Text = "ZIP Created Successfully";
    //    //}
    //    if (Label1.Text == "ZIP Created Successfully")
    //    {
    //        Array.ForEach(Directory.GetFiles(zipfilepath),
    //          delegate(string deleteFile) { File.Delete(deleteFile); });
    //    }  







    //}
    protected void txtQR_TextChanged(object sender, EventArgs e)
    {


       // Match match = Regex.Match(txtQR.Text, @"ID", RegexOptions.IgnoreCase);
      //  Regex regex = new Regex(@"(?<=ID=)\d+");

        Regex regex = new Regex(@"ID=([A-Za-z0-9\-]+)");
        Match match = regex.Match(txtQR.Text);


        //Match match = Regex.Match(txtQR.Text, @"ID=/([A-Za-z0-9\-]+)\$",
        //   RegexOptions.IgnoreCase);

     
       // Regex.Matches(txtQR.Text, @"=(.*?)");

      //  Label3.Text = match.Groups[1].Value;

        var s = match.Value; 
        var match1 = Regex.Match(s, "=([^-]+)$");
        if (match1.Success)
        {
            Label3.Text = match1.Groups[1].Value;

        }
        
        SelectQrItems();
        
        txtQR.Text = "";
        txtQR.Focus();

    }


    private void SelectQrItems()
    {
        Inventory.QRCode obj = new Inventory.QRCode();
        if (obj.QRItems_Select(Label3.Text) > 0)
        {
            txtItemCode.Text = obj.Item_Code;
            txtModelNo.Text = obj.ITEM_Model_No;
            txtChkNo.Text = obj.CHK_NO;
            txtChKDetColor.Text = obj.CHK_DET_Color;
            txtColorName.Text = obj.COlour_NAme;
            txtChkDt.Text = obj.CHK_DATE;
            txtQty.Text = obj.Qty ;
            txtPrintQty.Text = obj.PrintQty;
            btnAdd_Click();
        }
    }
    
    protected void btnAdd_Click()
    {
        DataTable QRItems = new DataTable();

        DataColumn col = new DataColumn();
        col = new DataColumn("Item_Code");
        QRItems.Columns.Add(col);
        col = new DataColumn("ITEM_Model_No");
        QRItems.Columns.Add(col);
        col = new DataColumn("CHK_NO");
        QRItems.Columns.Add(col);
        col = new DataColumn("CHK_DATE");
        QRItems.Columns.Add(col);
        col = new DataColumn("CHK_DET_Color");
        QRItems.Columns.Add(col);
        col = new DataColumn("COlour_NAme");
        QRItems.Columns.Add(col);
        col = new DataColumn("Qty");
        QRItems.Columns.Add(col);
        col = new DataColumn("PrintQty");
        QRItems.Columns.Add(col);
        //col = new DataColumn("Specifications");
        //QRItems.Columns.Add(col);

        if (grd.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in grd.Rows)
            {
                if (grd.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == grd.SelectedRow.RowIndex)
                    {


                        DataRow dr = QRItems.NewRow();
                        dr["Item_Code"] = txtItemCode.Text;

                        dr["ITEM_Model_No"] = txtModelNo.Text;
                        dr["CHK_NO"] = txtChkNo.Text;
                        dr["CHK_DATE"] = txtChkDt.Text;

                        dr["CHK_DET_Color"] = txtChKDetColor.Text;
                        dr["COlour_NAme"] = txtColorName.Text;
                        dr["Qty"] = txtQty.Text;
                        dr["PrintQty"] = txtPrintQty.Text;
                        QRItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QRItems.NewRow();
                        dr["Item_Code"] = gvrow.Cells[0].Text;
                        dr["ITEM_Model_No"] = gvrow.Cells[1].Text;
                        dr["CHK_NO"] = gvrow.Cells[2].Text;
                        dr["CHK_DATE"] = gvrow.Cells[3].Text;
                        dr["CHK_DET_Color"] = gvrow.Cells[4].Text;
                        dr["COlour_NAme"] = gvrow.Cells[5].Text;
                        dr["Qty"] = gvrow.Cells[6].Text;
                        dr["PrintQty"] = gvrow.Cells[7].Text;
                        QRItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QRItems.NewRow();
                    dr["Item_Code"] = gvrow.Cells[0].Text;
                    dr["ITEM_Model_No"] = gvrow.Cells[1].Text;
                    dr["CHK_NO"] = gvrow.Cells[2].Text;
                    dr["CHK_DATE"] = gvrow.Cells[3].Text;
                    dr["CHK_DET_Color"] = gvrow.Cells[4].Text;
                    dr["COlour_NAme"] = gvrow.Cells[5].Text;
                    dr["Qty"] = gvrow.Cells[6].Text;
                    dr["PrintQty"] = gvrow.Cells[7].Text;
                    QRItems.Rows.Add(dr);
                }
            }
        }
        if (grd.SelectedIndex == -1)
        {

            DataRow drnew = QRItems.NewRow();
            drnew["Item_Code"] = txtItemCode.Text;

            drnew["ITEM_Model_No"] = txtModelNo.Text;
            drnew["CHK_NO"] = txtChkNo.Text;
            drnew["CHK_DATE"] = txtChkDt.Text;

            drnew["CHK_DET_Color"] = txtChKDetColor.Text;
            drnew["COlour_NAme"] = txtColorName.Text;
            drnew["Qty"] = txtQty.Text;
            drnew["PrintQty"] = txtPrintQty.Text;

            QRItems.Rows.Add(drnew);
        }
        grd.DataSource = QRItems;
        grd.DataBind();
        grd.SelectedIndex = -1;
        Label3.Text = string.Empty;
    }
}