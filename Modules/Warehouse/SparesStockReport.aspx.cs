using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;
public partial class Modules_Warehouse_SparesStockReport : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindSparesStock();
        }
    }

    private void BindSparesStock()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("USP_SparesReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (txtModelNo.Text != "")
            {
                cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
            }
            if (txtSpareModelNo.Text != "")
            {
                cmd.Parameters.AddWithValue("@SpareModelNo", txtSpareModelNo.Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvSparesStock.DataSource = dt;
            gvSparesStock.DataBind();
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSparesStock();
        
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSparesStock.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        BindSparesStock();

    }
    protected void gvSparesStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //InwardQty += Convert.ToInt32(e.Row.Cells[8].Text);
            ////InwardQty = InwardQty + int.Parse(e.Row.Cells[8].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
        }
    }
    protected void gvSparesStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSparesStock.PageIndex = e.NewPageIndex;
        BindSparesStock();
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDetId;
        lbtnDetId = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDetId.Parent.Parent;
        gvSparesStock.SelectedIndex = gvRow.RowIndex;
        Inventory.Delivery objInventory = new Inventory.Delivery();
        objInventory.SpareItem_Delete(gvSparesStock.SelectedRow.Cells[1].Text);
        BindSparesStock();
        //objInventory.DeliveryDetailsByCustIdSample_Select(ddlCustomerName.SelectedItem.Value, gvDeliveryChallanItems);

    }
}
 
