using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using System.Data;
using vllib;
using System.Drawing;
using System.IO;
using Yantra.MessageBox;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using Yantra.Classes;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
public partial class Modules_Warehouse_ItemsReport : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int flag;
    DataTable dt2;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            //Masters.ProductCompany.ProductCompany_Select(ddlBrand2);
            Masters.ProductCompany.ProductCompany_Select(ddlBrand2);
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
            //Masters.ColorMaster.Color_Select(ddlColor);

            btnAll.Visible = false;
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "9");
        //btnExport.Enabled = up.Email;
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //flag = 1;

            ddlBrand2.SelectedIndex = ddlBrand.SelectedIndex;
            BindGrid();

            btnPrintAll.Visible = true;
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemMaster.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //BindGrid();
    }
    private void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_GetItemsReport", con);
        
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@BrandId", ddlBrand2.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@BrandId", ddlBrand.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);

        //dt2 = BindSearchGrid();

        //DataTable dt3 = dt1.Clone();
        //foreach (DataRow row1 in dt2.Rows)
        //{
        //    foreach (DataRow row2 in dt1.Rows)
        //    {
        //        if (row1["Item Code"].ToString() == row2["ITEM_CODE"].ToString())
        //        {
        //            dt3.ImportRow(row2);
        //        }
        //    }
        //}

        //var UniqueRows = dt3.AsEnumerable().Distinct(DataRowComparer.Default);
        //DataTable dt4 = UniqueRows.CopyToDataTable();

        gvItemMaster.DataSource = dt1;
        gvItemMaster.DataBind();
    }
    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_GetItemsReport_2", con);
        //SqlCommand cmd = new SqlCommand("[USP_StockReportNew_Serach]", con);
        cmd.CommandType = CommandType.StoredProcedure;



        if (txtModelNo.Text != "" && txtModelNo.Text != null)
        {
            cmd.Parameters.AddWithValue("@Model", txtModelNo.Text);
        }
        if (txtseries.Text != "" && txtseries.Text != null)
        {
            cmd.Parameters.AddWithValue("@Series", txtseries.Text);
        }
        if (ddlBrand2.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand2.SelectedItem.Value);
        }

        if (ddlCategory.SelectedIndex != 0 && ddlCategory.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@Category", ddlCategory.SelectedItem.Value);
        }

        if (ddlSubCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", ddlSubCat.SelectedItem.Value);
        }
        if (ddlModelNo.SelectedIndex > 0 )
        {
            cmd.Parameters.AddWithValue("@Item_Code", ddlModelNo.SelectedItem.Value);
        }
       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvItemMaster.DataSource = dt;
        gvItemMaster.DataBind();
       // gvItemMaster.PageIndex = 0;
        
    }
    protected void gvItemMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemMaster.PageIndex = e.NewPageIndex;
        //BindGrid();
        BindSearchGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvItemMaster.Rows.Count > 0)
        {
            Excel_Export();
           //PDF_Export();
            //Word_Export();
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=ItemsDetailsReport.xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    gvItemMaster.AllowPaging = false;
            //    this.BindGrid();

            //    gvItemMaster.HeaderRow.BackColor = Color.Yellow;
            //    foreach (TableCell cell in gvItemMaster.HeaderRow.Cells)
            //    {
            //        cell.BackColor = gvItemMaster.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in gvItemMaster.Rows)
            //    {
            //        row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = gvItemMaster.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = gvItemMaster.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    gvItemMaster.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //}
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }




    private void Word_Export()
    {

        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader("content-disposition",

          "attachment;filename=GridViewExport.doc");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-word ";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvItemMaster.AllowPaging = false;

        //BindGrid();
        BindSearchGrid();

        gvItemMaster.RenderControl(hw);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();

    }

    private void Excel_Export()
    {

        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader("content-disposition",

         "attachment;filename=GridViewExport.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvItemMaster.AllowPaging = false;
        //BindGrid();
        BindSearchGrid();
       // gvItemMaster.DataBind();

        for (int i = 0; i < gvItemMaster.Rows.Count; i++)
        {

            GridViewRow row = gvItemMaster.Rows[i];

            //Apply text style to each Row

            row.Attributes.Add("class", "textmode");

        }

        gvItemMaster.RenderControl(hw);



        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();

    }


    private void PDF_Export()
    {

        Response.ContentType = "application/pdf";

        Response.AddHeader("content-disposition",

            "attachment;filename=GridViewExport.pdf");

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvItemMaster.AllowPaging = false;

        //BindGrid();
        BindSearchGrid();
        gvItemMaster.RenderControl(hw);

        StringReader sr = new StringReader(sw.ToString());

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        pdfDoc.Open();

        htmlparser.Parse(sr);

        pdfDoc.Close();

        Response.Write(pdfDoc);

        Response.End();

    }
    public void PrintAllPages(GridView gvItemMaster)
    {
        GridView gv = gvItemMaster;

        gv.AllowPaging = false;
       // gv.DataBind();
        //BindGrid();
        BindSearchGrid();
        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gv.RenderControl(hw);

        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("window.onload = new function(){");

        sb.Append("var printWin = window.open('', '', 'left=0");

        sb.Append(",top=0,width=1000,height=600,scrollbars=1,status=0');");

        sb.Append("printWin.document.write(\"");

        sb.Append(gridHTML);

        sb.Append("\");");

        sb.Append("printWin.document.close();");

        sb.Append("printWin.focus();");

        sb.Append("printWin.print();};");

        //sb.Append("printWin.close();};");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

    }
      

    
    protected void btnPrintAll_Click1(object sender, EventArgs e)
    {
        PrintAllPages(gvItemMaster); //Pass the original gridview as parameter
    }
    protected void gvItemMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[13].Text == "Discontinued")
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text + ' ' + " (Discontinued)";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Visible = false;

            e.Row.Cells[13].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[0].Text = "Page " + (gvItemMaster.PageIndex + 1) + " of " + gvItemMaster.PageCount;
        }
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        
        gvItemMaster.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
        //BindGrid();
        BindSearchGrid();

    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        flag = 0;
        //BindGrid();
        BindSearchGrid();
        btnPrintAll.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //BindGrid();
            BindSearchGrid();
            btnPrintAll.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void ddlBrand2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBrand.SelectedIndex = ddlBrand2.SelectedIndex;
            Masters.ItemCategory.ItemCategory_Select_WithBrand(ddlCategory, ddlBrand2.SelectedItem.Value);
           
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }

     
    }
    //protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
    //}
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ModelNoSelect_Brand_Cat_SubCat(ddlModelNo, ddlBrand2.SelectedItem.Value,ddlCategory.SelectedItem.Value,ddlSubCat.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    protected void btnModelSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //BindGrid();
            BindSearchGrid();
            btnPrintAll.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    private void BindEssSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_GetEssItemReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //if (txtEssModelNo.Text != "" && txtEssModelNo.Text != null)
        //{
        //    cmd.Parameters.AddWithValue("@Model", txtEssModelNo.Text);
        //}
        if (lblItemCode.Text != "" && lblItemCode.Text != null)
        {
            cmd.Parameters.AddWithValue("@Model", ddlModelNo1.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvItemMaster.DataSource = dt;
        gvItemMaster.DataBind();
        gvItemMaster.PageIndex = 0;

    }
    protected void ddlModelNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster obj = new Masters.ItemMaster();
            obj.ItemMaster_ModelNoSelect1(ddlModelNo1.SelectedValue);
            lblItemCode.Text = obj.ItemCode;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void btnEssModelSearch_Click(object sender, EventArgs e)
    {
        ddlModelNo1.DataSourceID = "SqlDataSource2";
        ddlModelNo1.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo1.DataValueField = "ITEM_CODE";
        ddlModelNo1.DataBind();
        ddlModelNo1_SelectedIndexChanged(sender, e);
        try
        {
            BindEssSearchGrid();
            btnPrintAll.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void btnSeries_Click(object sender, EventArgs e)
    {
        try
        {
            //BindGrid();
            BindSearchGrid();
            btnPrintAll.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }

    
    
    protected void gvItemMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton bts = e.CommandSource as ImageButton;

        if (e.CommandName.Equals("Save"))
        {

            int rowindex = int.Parse(e.CommandArgument.ToString().Trim());
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            FileUpload FileUpload1 = bts.Parent.Parent.FindControl("fileupload1") as FileUpload;
            if (FileUpload1.HasFile)
            {
                string itemimage = "";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage"))
                {

                    foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                    {

                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 99999));
                        string path = Server.MapPath("~/Content/ItemImage/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                        //string Itemcode = gvItemMaster.DataKeys[rowindex].Values["ITEM_CODE"].ToString().Trim();
                        string Itemcode = rowindex.ToString();
                        itemimage = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        obj.Emp_photo = itemimage;
                        obj.Item_Path = "http://183.82.108.55/Content/ItemImage/" + itemimage;
                        obj.Item_img_update(Itemcode);

                        //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;


                    }
                }

            }
            gvItemMaster.DataBind();
        }
        else
        {
            if (e.CommandName.Equals("TechSave"))
            {

                int rowindex = int.Parse(e.CommandArgument.ToString().Trim());
                HR.EmployeeMaster obj = new HR.EmployeeMaster();
                FileUpload FileUpload1 = bts.Parent.Parent.FindControl("fileupload2") as FileUpload;
                if (FileUpload1.HasFile)
                {
                    string itemimage = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings"))
                    {

                        foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                        {

                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 99999));
                            string path = Server.MapPath("~/Content/ItemDrawings/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                            //string Itemcode = gvItemMaster.DataKeys[rowindex].Values["ITEM_CODE"].ToString().Trim();
                            string Itemcode = rowindex.ToString();
                            itemimage = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            obj.Emp_photo = itemimage;
                            obj.Item_Path = "http://183.82.108.55/Content/ItemDrawings/" + itemimage;
                            obj.Item_techimg_update(Itemcode);

                            //obj.tEmpPhoto = "http://valuelineapp.com/Content/EmployeeImage/" + itemimage;


                        }
                    }

                }
                gvItemMaster.DataBind();
            }
        }
        BindSearchGrid();
    }
}
 
