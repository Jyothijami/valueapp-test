using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using System.Data;
using vllib;
using System.IO;
using Yantra.MessageBox;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using Yantra.Classes;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.IO.Compression;  
public partial class Modules_Warehouse_Warehouse_Report_WithImages : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int flag;
    DataTable dt2;
    protected void Page_Load(object sender, EventArgs e)
    {
        PopulateCheckBoxArray();
        if (!IsPostBack)
        {
            setControlsVisibility();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand_WH);
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
            Masters.ColorMaster.Color_Select(ddlcolor);
        }
    }
    private void PopulateCheckBoxArray()
    {
        if (gvWarehouseReportImages.Rows.Count > 0)
        {
            ArrayList CheckBoxArray;
            if (ViewState["CheckBoxArray"] != null)
            {
                CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            }
            else
            {
                CheckBoxArray = new ArrayList();
            }

            int CheckBoxIndex;
            bool CheckAllWasChecked = false;
            CheckBox chkAll = (CheckBox)gvWarehouseReportImages.HeaderRow.Cells[0].FindControl("chkAll");
            string checkAllIndex = "chkAll-" + gvWarehouseReportImages.PageIndex;
            if (chkAll.Checked)
            {
                if (CheckBoxArray.IndexOf(checkAllIndex) == -1)
                {
                    CheckBoxArray.Add(checkAllIndex);
                }
            }
            else
            {
                if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                {
                    CheckBoxArray.Remove(checkAllIndex);
                    CheckAllWasChecked = true;
                }
            }
            for (int i = 0; i < gvWarehouseReportImages.Rows.Count; i++)
            {
                if (gvWarehouseReportImages.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)gvWarehouseReportImages.Rows[i].Cells[0].FindControl("CheckBox1");
                    CheckBoxIndex = gvWarehouseReportImages.PageSize * gvWarehouseReportImages.PageIndex + (i + 1);
                    if (chk.Checked)
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1 && !CheckAllWasChecked)
                        {
                            CheckBoxArray.Add(CheckBoxIndex);
                        }
                    }
                    else
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1 || CheckAllWasChecked)
                        {
                            CheckBoxArray.Remove(CheckBoxIndex);
                        }
                    }
                }
            }

            ViewState["CheckBoxArray"] = CheckBoxArray;
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "152");
        //btnExportExcel.Enabled = up.Email;
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWarehouseReportImages.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvWarehouseReportImages.DataBind();
    }
    protected void ddlBrand_WH_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelNo, ddlBrand_WH.SelectedItem.Value);

        if (ddlBrand_WH.SelectedItem.Value != "0" )
        {
            Masters.ColorMaster.ColorBrand_Select(ddlcolor,ddlBrand_WH.SelectedItem.Value);
        }


    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);

            if(ddlBrand_WH.SelectedItem.Value != "0" && ddlCategory.SelectedItem.Value != "0")
            {
                Masters.ColorMaster.Color_Select(ddlcolor,ddlBrand_WH.SelectedItem.Value,ddlCategory.SelectedItem.Value);
            }

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

    protected void lnk1_Click(object sender, EventArgs e)
    {
        try
        {
            lbl1.Text = "1000";
            BindGrid_WH();
            btnPrintAll.Visible = true;
            btnExportExcel.Visible = true;
            btnExportAll.Visible = true;

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        lbl2.Text = "5000";
        BindGrid_WH();
        btnPrintAll.Visible = true;
        btnExportExcel.Visible = true;
        btnExportAll.Visible = true;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        lbl3.Text = "10000";
        BindGrid_WH();
        btnPrintAll.Visible = true;
        btnExportExcel.Visible = true;
        btnExportAll.Visible = true;
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        lbl4.Text = "20000";
        BindGrid_WH();
        btnPrintAll.Visible = true;
        btnExportExcel.Visible = true;
        btnExportAll.Visible = true;
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        lbl5.Text = "30000";
        BindGrid_WH();
        btnPrintAll.Visible = true;
        btnExportExcel.Visible = true;
        btnExportAll.Visible = true;
    }

    protected void btnSearch_WH_Click(object sender, EventArgs e)
    {
        try
        {

            BindGrid_WH();
            btnPrintAll.Visible = true;
            btnExportExcel.Visible = true;
            btnExportAll.Visible = true;
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }

    private void BindGrid_WH()
    {
        SqlCommand cmd = new SqlCommand("USP_Warehouse_WithImages_Serach_2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlBrand_WH.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand_WH.SelectedItem.Value);
        }

        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyId", ddlCompany.SelectedItem.Value);
        }
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
        }
        if (ddlModelNo.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        }
        if (ddlCategory.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CatId", ddlCategory.SelectedItem.Value);
        }
        if (ddlSubCat.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@SubCatId", ddlSubCat.SelectedItem.Value);
        }

        if (ddlcolor.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@colorid", ddlcolor.SelectedItem.Value);
        }

        if (txtDisci.Text != "")
        {
            cmd.Parameters.AddWithValue("@DiscItems", txtDisci.Text);
        }

        if (ddlLocation.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Wh_Loc", ddlLocation.SelectedItem.Text);
        }
        if (ddlSymbols.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Symbols", ddlSymbols.SelectedItem.Text);
        }
        if (txtQtyCheck.Text != "")
        {
            cmd.Parameters.AddWithValue("@Qty_Check", txtQtyCheck.Text);
        }
        if (ddlPrice.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Price", ddlPrice.SelectedItem.Value);
        }
        if (lbl1.Text != "" || lbl1.Text=="1000")
        {
            cmd.Parameters.AddWithValue("@lbl1", lbl1.Text);
        }
        if (lbl2.Text != "" || lbl2.Text == "5000")
        {
            cmd.Parameters.AddWithValue("@lbl2", lbl2.Text);
        }
        if (lbl3.Text != "" || lbl3.Text == "10000")
        {
            cmd.Parameters.AddWithValue("@lbl3", lbl3.Text);
        }
        if (lbl4.Text != "" || lbl4.Text == "20000")
        {
            cmd.Parameters.AddWithValue("@lbl4", lbl4.Text);
        }
        if (lbl5.Text != "" || lbl5.Text == "30000")
        {
            cmd.Parameters.AddWithValue("@lbl5", lbl5.Text);
        }
        if (txtMin.Text != "")
        {
            cmd.Parameters.AddWithValue("@MinAmt", txtMin.Text);
        }
        if (txtMax.Text != "")
        {
            cmd.Parameters.AddWithValue("@MaxAmt", txtMax.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        gvWarehouseReportImages.DataSource = dt1;
        gvWarehouseReportImages.DataBind();
        lbl1.Text = string.Empty;
        lbl2.Text = string.Empty;
        lbl3.Text = string.Empty;
        lbl4.Text = string.Empty;
        lbl5.Text = string.Empty;
    }
    protected void btnPrintAll_Click1(object sender, EventArgs e)
    {
        //Excel_Export();
        PrintAllPages(gvWarehouseReportImages); //Pass the original gridview as parameter
    }
    public void PrintAllPages(GridView gvWarehouseReportImages)
    {
        GridView gv = gvWarehouseReportImages;

        gv.AllowPaging = false;
        // gv.DataBind();
        BindGrid_WH();

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
    protected void gvWarehouseReportImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWarehouseReportImages.PageIndex = e.NewPageIndex;
        //gvWarehouseReportImages.DataBind();
        BindGrid_WH();
        ResetCheckBoxes();
    }

    private void ResetCheckBoxes()
    {
        if (ViewState["CheckBoxArray"] != null)
        {
            ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            string checkAllIndex = "chkAll-" + gvWarehouseReportImages.PageIndex;

            if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
            {
                CheckBox chkAll = (CheckBox)gvWarehouseReportImages.HeaderRow.Cells[0].FindControl("chkAll");
                chkAll.Checked = true;
            }
            for (int i = 0; i < gvWarehouseReportImages.Rows.Count; i++)
            {

                if (gvWarehouseReportImages.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                    {
                        CheckBox chk = (CheckBox)gvWarehouseReportImages.Rows[i].Cells[0].FindControl("CheckBox1");
                        chk.Checked = true;
                    }
                    else
                    {
                        int CheckBoxIndex = gvWarehouseReportImages.PageSize * (gvWarehouseReportImages.PageIndex) + (i + 1);
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1)
                        {
                            CheckBox chk = (CheckBox)gvWarehouseReportImages.Rows[i].Cells[0].FindControl("CheckBox1");
                            chk.Checked = true;
                        }
                    }
                }
            }
        }
    }
    protected void gvWarehouseReportImages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Page " + (gvWarehouseReportImages.PageIndex + 1) + " of " + gvWarehouseReportImages.PageCount;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

            string imageName = "~/Content/Images/" + (e.Row.FindControl("lblPath") as Label).Text;
            string[] filename = imageName.Split('/');

            // 70 is define image size.
            GenerateThumbNail("~/Content/ItemImage/" + filename[3], imageName, 100);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[12].Text == "Discontinued")
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text + ' '+ " (Discontinued)";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    public void GenerateThumbNail(string sourcefile, string destinationfile, int width)
    {
        if (File.Exists(Server.MapPath(sourcefile)))
        {

            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(sourcefile));
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            int thumbWidth = width;
            int thumbHeight;
            Bitmap bmp;
            if (srcHeight > srcWidth)
            {
                thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }
            else
            {
                thumbHeight = thumbWidth;
                thumbWidth = (srcWidth / srcHeight) * thumbHeight;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

            bmp.Save(Server.MapPath(destinationfile));
            bmp.Dispose();
            image.Dispose();
            //DeleteTempImage(sourcefile, destinationfile);

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (gvWarehouseReportImages.Rows.Count > 0)
        {
        if (ViewState["CheckBoxArray"] != null)
        {
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvWarehouseReportImages.AllowPaging = false;
            BindGrid_WH();
            gvWarehouseReportImages.HeaderRow.Cells[0].Visible = false;
            if (ViewState["CheckBoxArray"] != null)
            {
                ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                string checkAllIndex = "chkAll-" + gvWarehouseReportImages.PageIndex;
                int rowIdx = 0;
                for (int i = 0; i < gvWarehouseReportImages.Rows.Count; i++)
                {
                    GridViewRow row = gvWarehouseReportImages.Rows[i];
                    row.Visible = false;

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (CheckBoxArray.IndexOf(i + 1) != -1)
                        {
                            row.Visible = true;
                            row.BackColor = System.Drawing.Color.White;
                            row.Cells[0].Visible = false;
                            row.Attributes.Add("class", "textmode");
                            if (rowIdx % 2 != 0)
                            {
                               
                            }
                            rowIdx++;
                            row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                            row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                            string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                            System.Web.UI.WebControls.Image img1 = row.Cells[11].Controls[1] as System.Web.UI.WebControls.Image;
                            row.Cells[11].Controls.Add(img1);
                            img1.Height = Unit.Pixel(150);
                            img1.Width = Unit.Pixel(150);
                        }
                    }
                }
            }
            gvWarehouseReportImages.RenderControl(hw);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.End();
            
        }
           
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //To Export all pages

                gvWarehouseReportImages.AllowPaging = false;
                //gvBlockedItems.DataBind();
                BindGrid_WH();
                gvWarehouseReportImages.HeaderRow.Cells[0].Visible = false;
                //BindSearchGrid();
                //gvterms.AllowPaging = false;
                //gvterms.DataBind();
                //gvitemsgrid.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvWarehouseReportImages.HeaderRow.Cells)
                {
                    cell.BackColor = gvWarehouseReportImages.HeaderStyle.BackColor;
                    //cell.BackColor = gvterms.HeaderStyle.BackColor;

                }
                foreach (GridViewRow row in gvWarehouseReportImages.Rows)
                {
                    //row.BackColor = Color.White;
                    row.Cells[0].Visible = false;
                    row.HorizontalAlign = HorizontalAlign.Center;
                    gvWarehouseReportImages.HorizontalAlign = HorizontalAlign.Center;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvWarehouseReportImages.AlternatingRowStyle.BackColor;
                            //cell.BackColor = gvterms.AlternatingRowStyle.BackColor;

                            cell.Wrap = true;
                        }
                        else
                        {
                            cell.BackColor = gvWarehouseReportImages.RowStyle.BackColor;
                            //cell.BackColor = gvterms.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }

                    gvWarehouseReportImages.Style["font-family"] = "Book Antiqua, Helvetica, sans-serif;";
                    row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                    row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                    string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                    System.Web.UI.WebControls.Image img1 = row.Cells[11].Controls[1] as System.Web.UI.WebControls.Image;
                    row.Cells[11].Controls.Add(img1);
                    img1.Height = Unit.Pixel(150);
                    img1.Width = Unit.Pixel(150);
                }

                gvWarehouseReportImages.RenderControl(hw);
                //gvterms.RenderControl(hw);
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Quotation.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }


    
}
 
