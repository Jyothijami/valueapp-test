using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modules_Warehouse_Temp_Outward : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblCompany.Text = cp.getPresentCompanySessionValue();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomer);

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable TempOutward = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        TempOutward.Columns.Add(col);
        col = new DataColumn("ModelNo");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Brand");
        TempOutward.Columns.Add(col);
        col = new DataColumn("BrandID");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Color");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Colorid");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Reference");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Customer");
        TempOutward.Columns.Add(col);
        col = new DataColumn("CustomerID");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Company");
        TempOutward.Columns.Add(col);
        col = new DataColumn("CompanyId");
        TempOutward.Columns.Add(col);

        col = new DataColumn("LocationId");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Quantity");
        TempOutward.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = TempOutward.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["Brand"] = gvrow.Cells[4].Text;
                dr["BrandID"] = gvrow.Cells[5].Text;
                dr["Color"] = gvrow.Cells[6].Text;
                dr["Colorid"] = gvrow.Cells[7].Text;
                dr["Reference"] = gvrow.Cells[8].Text;
                dr["Customer"] = gvrow.Cells[9].Text;
                dr["CustomerID"] = gvrow.Cells[10].Text;
                dr["Company"] = gvrow.Cells[11].Text;
                dr["CompanyId"] = gvrow.Cells[12].Text;
                dr["LocationId"] = gvrow.Cells[13].Text;
                dr["Quantity"] = gvrow.Cells[14].Text;
                TempOutward.Rows.Add(dr);
            }
        }
        DataRow drnew = TempOutward.NewRow();
        drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
        drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
        drnew["Brand"] = ddlBrand.SelectedItem.Text;
        drnew["BrandID"] = ddlBrand.SelectedItem.Value;
        drnew["Color"] = ddlColor.SelectedItem.Text;

        drnew["Colorid"] = ddlColor.SelectedItem.Value;
        drnew["Reference"] = txtDCNo.Text;
        drnew["Customer"] = ddlCustomer.SelectedItem.Text;
        drnew["CustomerID"] = ddlCustomer.SelectedItem.Value;
        drnew["Company"] = ddlCompanyProfile1.SelectedItem.Text;
        drnew["CompanyId"] = ddlCompanyProfile1.SelectedItem.Value;
        drnew["LocationId"] = ddlLocation.SelectedItem.Value;
        // drnew["LocationId"] = WH_Locations.getLocationID(TextBox2_value.Value);
        drnew["Quantity"] = txtQty.Text;

        TempOutward.Rows.Add(drnew);

        gvItemDetails.DataSource = TempOutward;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);

    }

   
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlBrand.SelectedIndex = 0;
        ddlModelNo.SelectedIndex = 0;
        ddlModelNo.DataSource = null;
        ddlModelNo.DataBind();
        ddlColor.SelectedIndex = 0;
        txtDCNo.Text = string.Empty;
        ddlCustomer.SelectedIndex = 0;
        ddlCompanyProfile1.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        txtQty.Text = string.Empty;
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster5_Select(ddlModelNo, ddlBrand.SelectedItem.Value);
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedItem.Value);
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[2].Text;
        DataTable TempOutward = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        TempOutward.Columns.Add(col);
        col = new DataColumn("ModelNo");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Brand");
        TempOutward.Columns.Add(col);
        col = new DataColumn("BrandID");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Color");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Colorid");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Reference");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Customer");
        TempOutward.Columns.Add(col);
        col = new DataColumn("CustomerID");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Company");
        TempOutward.Columns.Add(col);
        col = new DataColumn("CompanyId");
        TempOutward.Columns.Add(col);

        col = new DataColumn("LocationId");
        TempOutward.Columns.Add(col);
        col = new DataColumn("Quantity");
        TempOutward.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {

                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = TempOutward.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["Brand"] = gvrow.Cells[4].Text;
                    dr["BrandID"] = gvrow.Cells[5].Text;
                    dr["Color"] = gvrow.Cells[6].Text;
                    dr["Colorid"] = gvrow.Cells[7].Text;
                    dr["Reference"] = gvrow.Cells[8].Text;
                    dr["Customer"] = gvrow.Cells[9].Text;
                    dr["CustomerID"] = gvrow.Cells[10].Text;
                    dr["Company"] = gvrow.Cells[11].Text;
                    dr["CompanyId"] = gvrow.Cells[12].Text;
                    dr["LocationId"] = gvrow.Cells[13].Text;
                    dr["Quantity"] = gvrow.Cells[14].Text;
                    TempOutward.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = TempOutward;
        gvItemDetails.DataBind();
        //btnItemRefresh_Click(sender, e);

    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
        }
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            SqlCommand cmd = new SqlCommand("Usp_Get_Top_Selected_Items_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
            cmd.Parameters.AddWithValue("@ColourId", gvrow.Cells[7].Text);
            cmd.Parameters.AddWithValue("@LocationId", gvrow.Cells[13].Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Masters.ItemPurchase objout = new Masters.ItemPurchase();
            int qty1 = int.Parse(gvrow.Cells[14].Text);
            int qty = int.Parse(gvrow.Cells[14].Text);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < qty1; i++)
                {

                    if (qty >= Convert.ToInt32(dt.Rows[i][4]))
                    {
                        objout.qty = dt.Rows[i][4].ToString();
                    }
                    else if (qty < Convert.ToInt32(dt.Rows[i][4]))
                    {
                        objout.qty = qty.ToString();
                    }
                    objout.itemcode = gvrow.Cells[2].Text;
                    objout.ItemID = dt.Rows[i][0].ToString();
                    objout.locationid = dt.Rows[i][1].ToString();
                    objout.Barcode = dt.Rows[i][0].ToString();
                    objout.companyid = dt.Rows[i][3].ToString();
                    objout.DCID = gvrow.Cells[8].Text;
                    objout.COLORID = dt.Rows[i][2].ToString();
                    objout.CustId = gvrow.Cells[10].Text;
                    objout.OutwardNew_Save();
                    qty = qty - Convert.ToInt32(dt.Rows[i][4]);
                    if (qty <= 0)
                    {
                        break;
                    }
                }
            }
            #region Old outward save
            //string Itemcode = gvrow.Cells[2].Text;
            //SqlCommand cmd = new SqlCommand("Usp_Get_Top_Selected_Items", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
            //cmd.Parameters.AddWithValue("@ColourId", gvrow.Cells[7].Text);
            //cmd.Parameters.AddWithValue("@LocationId", gvrow.Cells[13].Text);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //Masters.ItemPurchase objout = new Masters.ItemPurchase();
            //int qty = int.Parse(gvrow.Cells[14].Text);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < qty; i++)
            //    {
            //        objout.itemcode = gvrow.Cells[2].Text;
            //        objout.ItemID = dt.Rows[i][0].ToString();
            //        objout.locationid = dt.Rows[i][1].ToString();
            //        objout.Barcode = dt.Rows[i][0].ToString();
            //        //objout.companyid = gvrow.Cells[12].Text;
            //        objout.companyid = dt.Rows[i][3].ToString();
            //        objout.DCID = gvrow.Cells[8].Text;
            //        //objout.COLORID = gvrow.Cells[7].Text;
            //        objout.COLORID = dt.Rows[i][2].ToString();
            //        objout.CustId = gvrow.Cells[10].Text;
            //        objout.Outward_Save();
            //    }
            //}
            #endregion

        }
        btnItemRefresh_Click(sender, e);
        gvItemDetails.DataSource = null;
        gvItemDetails.DataBind();
    }
}
 
