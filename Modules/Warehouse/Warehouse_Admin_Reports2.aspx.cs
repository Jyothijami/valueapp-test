using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using vllib;
using Yantra.MessageBox;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class Modules_Warehouse_Warehouse_Admin_Reports : basePage
{
    string reference;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            pnlMRN.Visible = true;
            ItemCategoryFill();
            CustomerName_Fill();

            BrandFill();
            BindGrid_All();
            BindSession();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "66");
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "78");

        btnExprot.Enabled = up.Email;
        btnBlockExport.Enabled = up.Email;
        btnReleaseBlock.Enabled = up1.Email;
    }
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster .BlockedCustBind(ddlCust );
            SM.CustomerMaster.BlockedCustBind(ddlReleaseCust);

        }
        catch (Exception ex) { }
    }
    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlCat);
        //HR.EmployeeMaster.EmployeeMaster_SelectSales12(ddlExecutive);
    }

    private void BrandFill()
    {
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
    }

    protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCat.SelectedValue);
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
    protected void btnExprot_Click(object sender, EventArgs e)
    {
        if (gvBlockedItems.Rows.Count > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages

                    gvBlockedItems.AllowPaging = false;
                    //gvBlockedItems.DataBind();
                    BindGrid_All();
                    //BindSearchGrid();
                    //gvterms.AllowPaging = false;
                    //gvterms.DataBind();
                    //gvitemsgrid.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvBlockedItems.HeaderRow.Cells)
                    {
                        cell.BackColor = gvBlockedItems.HeaderStyle.BackColor;
                        //cell.BackColor = gvterms.HeaderStyle.BackColor;

                    }
                    foreach (GridViewRow row in gvBlockedItems.Rows)
                    {
                        //row.BackColor = Color.White;
                        row.HorizontalAlign = HorizontalAlign.Center;
                        gvBlockedItems.HorizontalAlign = HorizontalAlign.Center;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvBlockedItems.AlternatingRowStyle.BackColor;
                                //cell.BackColor = gvterms.AlternatingRowStyle.BackColor;

                                cell.Wrap = true;
                            }
                            else
                            {
                                cell.BackColor = gvBlockedItems.RowStyle.BackColor;
                                //cell.BackColor = gvterms.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }

                        gvBlockedItems.Style["font-family"] = "Book Antiqua, Helvetica, sans-serif;";
                        row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                        row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                        string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                        System.Web.UI.WebControls.Image img1 = row.Cells[10].Controls[1] as System.Web.UI.WebControls.Image;
                        row.Cells[10].Controls.Add(img1);
                        img1.Height = Unit.Pixel(150);
                        img1.Width = Unit.Pixel(150);
                    }

                    gvBlockedItems.RenderControl(hw);
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
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(txtModelNo.Text != "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
        {
            // Only Model No
            BindGrid_ModelNo();
            tblBlockedItems.Visible = false;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
        {
            // Only Brand
            BindGrid_Brand();
            tblBlockedItems.Visible = false;
            
        }
        else if(txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Only Category
            BindGrid_Category();
            tblBlockedItems.Visible = false;
        }
        else if(txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Category & Sub-Category
            BindGrid_Cat_Sub();
            tblBlockedItems.Visible = false;
        }
        else if(txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Brand & Category
            BindGrid_Brand_Cat();
            tblBlockedItems.Visible = false;
        }
        else if(txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Brand , Category & Sub-Category
            BindGrid_Brand_Cat_Sub();
            tblBlockedItems.Visible = false;
        }
        //else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1 && ddlExecutive.SelectedIndex != 0)
        //{
        //    BindGrid_Executive();
        //    tblBlockedItems.Visible = false;
        //}
        else
        {
            // All Items
            BindGrid_All();
            tblBlockedItems.Visible = false;
        }
        BindSession();
    }

    private void BindSession()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new System.Data.DataColumn("Location", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Inward Stock", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Blocked Stock", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Outward Stock", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Available Stock", typeof(String)));

        foreach (GridViewRow row in gvWarehouseReport.Rows)
        {
            dr = dt.NewRow();
            dr[0] = row.Cells[0].Text;
            dr[1] = row.Cells[1].Text;
            LinkButton lnk = (LinkButton)row.FindControl("lbtnBlockStock");
            dr[2] = lnk.Text;
            dr[3] = row.Cells[3].Text;
            dr[4] = row.Cells[4].Text;

            dt.Rows.Add(dr);
        }
        Session["StockReport"] = dt;
    }    

    private void BindGrid_ModelNo()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReportNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", txtModelNo.Text);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Brand()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_BrandNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Executive()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Executive]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Executive", ddlExecutive.SelectedItem.Text );

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Category()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_CatNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Cat_Sub()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Cat_SubNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubCategory", ddlSubCat.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Brand_Cat()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Brand_CatNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Brand_Cat_Sub()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Brand_Cat_SubNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubCategory", ddlSubCat.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_AllNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    protected void btnCompSearch_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[USP_CompWiseBlockedItemsNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CP_ID", ddlCompany.SelectedValue);
        cmd.Parameters.AddWithValue("@Locid", ddlLoc.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvCpBlocked.DataSource = dt;
        gvCpBlocked.DataBind();
    }
    protected void btnBlockExport_Click(object sender, EventArgs e)
    {
        if (gvCpBlocked.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventorMRNyReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvCpBlocked.AllowPaging = false;
                //gvCpBlocked.DataBind();
                gvCpBlocked.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvCpBlocked.HeaderRow.Cells)
                {
                    cell.BackColor = gvCpBlocked.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvCpBlocked.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvCpBlocked.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvCpBlocked.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvCpBlocked.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }

    protected void lbtnBlockStock_Click(object sender, EventArgs e)
    {
        LinkButton lbtnBlocked;
        lbtnBlocked = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnBlocked.Parent.Parent;
        gvWarehouseReport.SelectedIndex = gvRow.RowIndex;
        lblLocId.Text = gvWarehouseReport.SelectedRow.Cells[5].Text;

        if (txtModelNo.Text != "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
        {
            // Only Model No
            BindBlockedGrid_ModelNo();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
        {
            // Only Brand
            BindBlockedGrid_Brand();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Only Category
            BindBlockedGrid_Category();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Category & Sub-Category
            BindBlockedGrid_Cat_Sub();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Brand & Category
            BindBlockedGrid_Brand_Cat();
            tblBlockedItems.Visible = true;
        }
            else if(txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        //else if (txtModelNo.Text != "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Brand , Category & Sub-Category
            BindBlockedGrid_Brand_Cat_Sub();
            tblBlockedItems.Visible = true;
        }
        else
        {
            // All Items
            BindBlockedGrid_All();
            tblBlockedItems.Visible = true;
        }
   
    }

    private void BindBlockedGrid_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_AllNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }

    private void BindBlockedGrid_ModelNo()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_ModelNoNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", txtModelNo.Text);
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }

    private void BindBlockedGrid_Brand()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_BrandNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BrandId", ddlBrand.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }

    private void BindBlockedGrid_Category()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_CatNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CatId", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }

    private void BindBlockedGrid_Cat_Sub()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Cat_SubNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CatId", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubCatId", ddlSubCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }

    private void BindBlockedGrid_Brand_Cat()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Brand_CatNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BrandId", ddlBrand.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@CatId", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }

    private void BindBlockedGrid_Brand_Cat_Sub()
    {
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Brand_Cat_SubNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BrandId", ddlBrand.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@CatId", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubCatId", ddlSubCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@locid", lblLocId.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvBlockedItems.DataSource = dt;
        gvBlockedItems.DataBind();
    }
    protected void gvWarehouseReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.Header || e.Row.RowType==DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Visible=false;
        }
    }
    protected void gvBlockedItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string imageName = "~/Content/Images/" + (e.Row.FindControl("lblPath") as Label).Text;
            string[] filename = imageName.Split('/');

            // 70 is define image size.
            GenerateThumbNail("~/Content/ItemImage/" + filename[3], imageName, 100);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Visible = false;
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
    protected void btnCustSearch_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[USP_CustWiseBlockedItemsNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Customer",txtCustomer.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvCustItems.DataSource = dt;
        gvCustItems.DataBind();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtModelNo.Text = "";
        ddlBrand.SelectedIndex =ddlCat.SelectedIndex=ddlSubCat.SelectedIndex = -1;
    }

    protected void gvBlockedItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBlockedItems.PageIndex = e.NewPageIndex;
        if (txtModelNo.Text != "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
        {
            // Only Model No
            BindBlockedGrid_ModelNo();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
        {
            // Only Brand
            BindBlockedGrid_Brand();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Only Category
            BindBlockedGrid_Category();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Category & Sub-Category
            BindBlockedGrid_Cat_Sub();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Brand & Category
            BindBlockedGrid_Brand_Cat();
            tblBlockedItems.Visible = true;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        //else if (txtModelNo.Text != "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Brand , Category & Sub-Category
            BindBlockedGrid_Brand_Cat_Sub();
            tblBlockedItems.Visible = true;
        }
        else
        {
            // All Items
            BindBlockedGrid_All();
            tblBlockedItems.Visible = true;
        }
    }
    protected void lnkMRN_Click(object sender, EventArgs e)
    {
        reference = "Stock";
        pnlDC.Visible = false;
        pnlMRN.Visible = true;
        pnlOtherStock.Visible = false;
    }
    protected void lnkDC_Click(object sender, EventArgs e)
    {
        reference = "Customer";
        pnlDC.Visible = true;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlReleaseStock.Visible = false;

    }
    protected void lnkOtherStock_Click(object sender, EventArgs e)
    {
        reference = "Branch";
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = true;
        pnlReleaseStock.Visible = false ;

    }
    protected void ddlCust_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[USP_CustWiseBlockedItemsNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Customer", ddlCust .SelectedItem .Text );
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvCustItems.DataSource = dt;
        gvCustItems.DataBind();
    }
    protected void lnkReleaseBlockedStk_Click(object sender, EventArgs e)
    {
        reference = "Release";
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlReleaseStock.Visible = true;
    }
    private void BindBlockedItems()
    {
        SqlCommand cmd = new SqlCommand("[USP_CustBlockedItemsNew_ByCust]", con);
        //SqlCommand cmd = new SqlCommand("[USP_CustBlockedItemsNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CustId", ddlReleaseCust.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvReservedStock.DataSource = dt;
        gvReservedStock.DataBind();
    }
    protected void ddlReleaseCust_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlockedItems();
    }
    protected void btnReleaseBlock_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {
                    string blockStock = gvrow.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int b = Convert.ToInt32(blockStock);
                    int q = Convert.ToInt32(qty.Text);


                    if (b >= q)
                    {
                        string Itemcode = gvrow.Cells[2].Text;
                        SqlCommand cmd = new SqlCommand("[Usp_Blocked_Items_New]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[0].Text);
                        cmd.Parameters.AddWithValue("@ColorId", gvrow.Cells[9].Text);
                        cmd.Parameters.AddWithValue("@DeliveryDate", Yantra.Classes.General.toMMDDYYYY( gvrow.Cells[5].Text));
                        cmd.Parameters.AddWithValue("@CustId", gvrow.Cells[10].Text);
                        cmd.Parameters.AddWithValue("@LocId", gvrow.Cells[11].Text);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        int remQty = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < q; i++)
                            {
                                SqlCommand cmd1 = new SqlCommand("Usp_Blocked_Items_Release", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                if (q >= Convert.ToInt32(dt.Rows[i][1]))
                                {
                                    cmd1.Parameters.AddWithValue("@Flag", "1");
                                }
                                else if (q < Convert.ToInt32(dt.Rows[i][1]))
                                {
                                    cmd1.Parameters.AddWithValue("@Flag", "0");
                                    remQty = Convert.ToInt32(dt.Rows[i][1]) - q;
                                }
                                cmd1.Parameters.AddWithValue("@ItemId", dt.Rows[i][0]);
                                cmd1.Parameters.AddWithValue("@Quantity", remQty);
                                cmd1.Parameters.AddWithValue("@CustId", dt.Rows[i][2]);
                                cmd1.Parameters.AddWithValue("@LocId", dt.Rows[i][3]);
                                cmd1.Parameters.AddWithValue("@DeliveryDate", dt.Rows[i][4]);
                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();

                                q = q - Convert.ToInt32(dt.Rows[i][1]);
                                if (q <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "This Operation Cannot Be Performed.");
                    }

                }

            }


            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {

                    DataTable ReleasedItems = new DataTable();

                    DataColumn col = new DataColumn();

                    col = new DataColumn("Item_Code");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("ITEM_MODEL_NO");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("COLOUR_NAME");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("Quantity");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("CUST_NAME");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("Delivery_Date");
                    ReleasedItems.Columns.Add(col);

                    if (gvReleasedItems.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvrow1 in gvReleasedItems.Rows)
                        {
                            if (gvReleasedItems.SelectedIndex > -1)
                            {
                                if (gvrow.RowIndex == gvReleasedItems.SelectedRow.RowIndex)
                                {


                                    DataRow dr = ReleasedItems.NewRow();
                                    dr["Item_Code"] = gvrow.Cells[0].Text;
                                    dr["ITEM_MODEL_NO"] = gvrow.Cells[1].Text;
                                    dr["COLOUR_NAME"] = gvrow.Cells[2].Text;
                                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                                    dr["Quantity"] = qty.Text;
                                    dr["CUST_NAME"] = gvrow.Cells[4].Text;
                                    dr["Delivery_Date"] = gvrow.Cells[5].Text;
                                    ReleasedItems.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = ReleasedItems.NewRow();
                                    dr["Item_Code"] = gvrow1.Cells[0].Text;
                                    dr["ITEM_MODEL_NO"] = gvrow1.Cells[1].Text;
                                    dr["COLOUR_NAME"] = gvrow1.Cells[2].Text;
                                    dr["Quantity"] = gvrow1.Cells[3].Text;
                                    dr["CUST_NAME"] = gvrow1.Cells[4].Text;
                                    dr["Delivery_Date"] = gvrow1.Cells[5].Text;
                                    ReleasedItems.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = ReleasedItems.NewRow();
                                dr["Item_Code"] = gvrow1.Cells[0].Text;
                                dr["ITEM_MODEL_NO"] = gvrow1.Cells[1].Text;
                                dr["COLOUR_NAME"] = gvrow1.Cells[2].Text;
                                dr["Quantity"] = gvrow1.Cells[3].Text;
                                dr["CUST_NAME"] = gvrow1.Cells[4].Text;
                                dr["Delivery_Date"] = gvrow1.Cells[5].Text;
                                ReleasedItems.Rows.Add(dr);
                            }
                        }
                    }

                    if (gvReleasedItems.SelectedIndex == -1)
                    {
                        DataRow drnew = ReleasedItems.NewRow();
                        drnew["Item_Code"] = gvrow.Cells[0].Text;
                        drnew["ITEM_MODEL_NO"] = gvrow.Cells[1].Text;
                        drnew["COLOUR_NAME"] = gvrow.Cells[2].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        drnew["Quantity"] = qty.Text;
                        drnew["CUST_NAME"] = gvrow.Cells[4].Text;
                        drnew["Delivery_Date"] = gvrow.Cells[5].Text;

                        ReleasedItems.Rows.Add(drnew);
                    }
                    gvReleasedItems.DataSource = ReleasedItems;
                    gvReleasedItems.DataBind();
                    gvReleasedItems.SelectedIndex = -1;
                    ch.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            BindBlockedItems();
            btnReleaseBlock.Visible = false;
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvReservedStock.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                btnReleaseBlock.Visible = true;
            }
        }
    }

    protected void gvReservedStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
            //e.Row.Cells[14].Visible = false;
        }
    }
}
 
