using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Yantra.Classes;
using YantraBLL.Modules;
using System.IO;
using System.Drawing;
using Yantra.MessageBox;
using Yantra.Classes;
public partial class Modules_Warehouse_Stock_Report : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            Masters.ItemMaster.ItemMaster_Select(ddlModelNo);
            Masters.ColorMaster.Color_Select(ddlColor);
            //BindStockReportGrid();
            setControlsVisibility();
            ItemCategoryFill();
        }
    }

    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlCategory);
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "91");
        //btnSearch.Enabled = up.Search;

    }

    private void BindStockReportGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_StockReport_Serach", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();
        
    }

    
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelNo, ddlBrand.SelectedItem.Value);
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearchGrid();
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_StockReport_Day_Serach", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedItem.Value);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value);
        }
        if (ddlModelNo.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo.SelectedItem.Value);
        }

        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        }

        if (ddlCategory.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CatId", ddlCategory.SelectedItem.Value);
        }

        if (ddlSubCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@SubCatId", ddlSubCat.SelectedItem.Value);
        }

        if (ddlColor.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Color", ddlColor.SelectedItem.Value);
        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStockReport.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvStockReport.DataBind();   
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (txtModelNo.Text != "")
        {
            SqlCommand cmd = new SqlCommand("USP_StockReport_Day_ModelNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStockReport.DataSource = dt;
            gvStockReport.DataBind();
        }
        else
        {
            MessageBox.Show(this, "Please Enter ModelNo");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvStockReport.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=PurchaseOrderSearch_Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvStockReport.AllowPaging = false;
                this.BindSearchGrid();

                gvStockReport.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvStockReport.HeaderRow.Cells)
                {
                    cell.BackColor = gvStockReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvStockReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvStockReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvStockReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvStockReport.RenderControl(hw);

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
    protected void gvStockReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[2].ForeColor = Color.Black;
            //e.Row.Cells[3].ForeColor = Color.Black;
            
            e.Row.Cells[1].Visible = false;
            
            e.Row.Cells[5].ForeColor = Color.Green;
            e.Row.Cells[6].ForeColor = Color.Red;
            e.Row.Cells[7].ForeColor = Color.DarkRed;


        }
    }
    protected void gvStockReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStockReport.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }
}
 
