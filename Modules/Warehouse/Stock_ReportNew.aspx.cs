using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Warehouse_Stock_ReportNew : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid_All();
            ItemCategoryFill();
            BrandFill();

        }
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
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_StockReport_AllNew_Locations]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    protected void lbtnBlockStock_Click(object sender, EventArgs e)
    {
        LinkButton lbtnBlocked;
        lbtnBlocked = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnBlocked.Parent.Parent;
        gvWarehouseReport.SelectedIndex = gvRow.RowIndex;
        lblLocId.Text = gvWarehouseReport.SelectedRow.Cells[6].Text;
        BindSearchGrid();
        tblBlockedItems.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (txtModelNo.Text != "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex == 0 && ddlSubCat.SelectedIndex == -1)
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
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Only Category
            BindGrid_Category();
            tblBlockedItems.Visible = false;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex == 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Category & Sub-Category
            BindGrid_Cat_Sub();
            tblBlockedItems.Visible = false;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex == 0)
        {
            // Brand & Category
            BindGrid_Brand_Cat();
            tblBlockedItems.Visible = false;
        }
        else if (txtModelNo.Text == "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
        {
            // Brand , Category & Sub-Category
            BindGrid_Brand_Cat_Sub();
            tblBlockedItems.Visible = false;
        }
    }
    private void BindGrid_Brand_Cat_Sub()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Brand_Cat_SubNew_QRCode]", con);
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
    private void BindGrid_Brand_Cat()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Brand_CatNew_QRCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_ModelNo()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReportNew_QRCode]", con);
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
        SqlCommand cmd = new SqlCommand("[USP_StackReport_BrandNew_QRCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }
    private void BindGrid_Category()
    {
        SqlCommand cmd = new SqlCommand("[USP_StackReport_CatNew_QRCode]", con);
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
        SqlCommand cmd = new SqlCommand("[USP_StackReport_Cat_SubNew_QRCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@SubCategory", ddlSubCat.SelectedItem.Value);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvWarehouseReport.DataSource = dt;
        gvWarehouseReport.DataBind();
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_StockReportNew_Serach_QRCODE1]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //if (ddlCompany.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedItem.Value);
        //}
        //if (ddlLocation.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value);
        //}
        //if (ddlModelNo.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo.SelectedItem.Value);
        //}

        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        }

        if (ddlCat.SelectedIndex != 0 && ddlCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@CatId", ddlCat.SelectedItem.Value);
        }

        if (ddlSubCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@SubCatId", ddlSubCat.SelectedItem.Value);
        }
        //if (ddlMrn.SelectedIndex != 0 && ddlMrn.SelectedIndex != -1)
        //{
        //    cmd.Parameters.AddWithValue("@CHK_ID", ddlMrn.SelectedItem.Value);
        //}
        //if (ddlColor.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@Color", ddlColor.SelectedItem.Value);
        //}
        //if (txtFromDate.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        //}
        //if (txtToDate.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        //}
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ItemCode", txtModelNo.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            if (dt.Rows[i]["ClosingStock"].ToString() == "0")
                dr.Delete();

            //if (dr["Total Available Stock"] == "0")
            //    dr.Delete();
        }

        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();
    }
    decimal ClosingStock = 0, printQty = 0;
    protected void gvStockReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            //e.Row.Cells[1].Visible = false;

            // Total Inward -Green
            e.Row.Cells[5].ForeColor = Color.DarkRed;

            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            //e.Row.Cells[19].Visible = false;







            if (e.Row.Cells[18].Text == "1")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[19].Text == string.Empty || e.Row.Cells[19].Text == "&nbsp;")
                {
                    e.Row.Cells[19].Text = "0";
                }
                ClosingStock += Convert.ToDecimal(e.Row.Cells[5].Text);
                printQty += Convert.ToDecimal(e.Row.Cells[19].Text);
                //InwardQty = InwardQty + int.Parse(e.Row.Cells[8].Text);
                //e.Row.Cells[5].Text = lblCS.Text = ClosingStock.ToString();
                //lblCS.Text = ClosingStock.ToString();
                //lblPrintQty.Text = printQty.ToString();

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[5].Text = lblCS.Text = ClosingStock.ToString();
            }
        }
    }
    protected void gvStockReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStockReport.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }
}