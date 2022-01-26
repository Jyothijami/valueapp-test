using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data;
using Yantra.Classes;
using YantraBLL.Modules;
using Yantra.MessageBox;

public partial class dev_pages_SO_Bal_Qty_Update : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_All_Grids(gvSOBalQty, "USP_SO_Item_Details");
            Bind_All_Grids(gvDCItemQty, "USP_DC_Item_Details");
            Bind_All_Grids(gvDcItemSummaryGrid, "USP_STOCKMOVMENT");
        }
    }

    private void Bind_All_Grids(GridView gv, string Procedure)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(Procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (txtSONo.Text != "")
            {
                cmd.Parameters.AddWithValue("@SO_No", txtSONo.Text);
            }
            if (txtSODate.Text != "")
            {
                cmd.Parameters.AddWithValue("@SO_Date", General.toMMDDYYYY(txtSODate.Text));
            }
            if (txtDcNo.Text != "")
            {
                cmd.Parameters.AddWithValue("@DC_No", txtDcNo.Text);
            }
            if (txtDcDate.Text != "")
            {
                cmd.Parameters.AddWithValue("@DC_Date", General.toMMDDYYYY(txtDcDate.Text));
            }

            if (txtModelNo.Text != "")
            {
                cmd.Parameters.AddWithValue("@Model_No", txtModelNo.Text);
            }
            if (txtItemCode.Text != "")
            {
                cmd.Parameters.AddWithValue("@Item_Code", txtItemCode.Text);
            }

            if (txtCustname.Text != "")
            {
                cmd.Parameters.AddWithValue("@Cust_Name", txtCustname.Text);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gv.DataSource = dt;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Bind_All_Grids();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }

    }

    private void Bind_All_Grids()
    {
        gvSOBalQty.DataSource = null;
        gvSOBalQty.DataBind();
        gvDCItemQty.DataSource = null;
        gvDCItemQty.DataBind();
        gvDcItemSummaryGrid.DataSource = null;
        gvDcItemSummaryGrid.DataBind();

        Bind_All_Grids(gvSOBalQty, "USP_SO_Item_Details");
        Bind_All_Grids(gvDCItemQty, "USP_DC_Item_Details");
        Bind_All_Grids(gvDcItemSummaryGrid, "USP_DC_Summary_Item_Details");
    }
    protected void btnUpdateSOQty_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;
            foreach (GridViewRow gvr in gvSOBalQty.Rows)
            {
                if (((CheckBox)gvr.FindControl("Chk")).Checked)
                {
                    try
                    {
                        con.Close();

                        Label so_Det_Id = (Label)gvr.FindControl("lbtnSO_DET_ID");

                        TextBox balQty = (TextBox)gvr.FindControl("txtBalanceQty");

                        string _command = "update YANTRA_SO_DET set BalanceQty='" + Convert.ToInt32(balQty.Text) + "' where SO_DET_ID='" + so_Det_Id.Text + "'";
                        SqlCommand cmd = new SqlCommand(_command, con);
                        cmd.CommandType = CommandType.Text;

                        con.Open();
                        i = i + cmd.ExecuteNonQuery();
                        con.Close();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
            }

            if (i > 0)
            {
                MessageBox.Show(this, i+", Record/s Updated Successfully..!");
            }

            Bind_All_Grids();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void gvSOBalQty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSOBalQty.PageIndex = e.NewPageIndex;

        Bind_All_Grids(gvSOBalQty, "USP_SO_Item_Details");
    }
    protected void gvDCItemQty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDCItemQty.PageIndex = e.NewPageIndex;

        Bind_All_Grids(gvDCItemQty, "USP_DC_Item_Details");
    }
    protected void gvDcItemSummaryGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDcItemSummaryGrid.PageIndex = e.NewPageIndex;
        Bind_All_Grids(gvDcItemSummaryGrid, "USP_DC_Summary_Item_Details");
    }
}