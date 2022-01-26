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
public partial class Modules_Warehouse_Warehouse_Admin_Reports : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            ItemCategoryFill();
            BrandFill();
            BindGrid_All();
            BindSession();

        }
    }

    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlCat);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport", con);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport_Brand", con);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport_Cat", con);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport_Cat_Sub", con);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport_Brand_Cat", con);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport_Brand_Cat_Sub", con);
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
        SqlCommand cmd = new SqlCommand("USP_StackReport_All", con);
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
        else if (txtModelNo.Text != "" && ddlBrand.SelectedIndex != 0 && ddlCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != 0)
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
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_All]", con);
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
        SqlCommand cmd = new SqlCommand("USP_BlockedItems_ModelNo", con);
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
        SqlCommand cmd = new SqlCommand("USP_BlockedItems_Brand", con);
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
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Cat]", con);
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
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Cat_Sub]", con);
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
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Brand_Cat]", con);
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
        SqlCommand cmd = new SqlCommand("[USP_BlockedItems_Brand_Cat_Sub]", con);
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
    protected void btnCustSearch_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("USP_CustWiseBlockedItems", con);
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
}
 
