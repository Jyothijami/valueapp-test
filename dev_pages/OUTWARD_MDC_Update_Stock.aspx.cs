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

public partial class dev_pages_OUTWARD_DC_Update_Stock : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        gvDCDet.DataBind();
        gvOutwardBind.DataBind();
        gvStkMvmtDet.DataBind();
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
        gvDCDet.DataSource = null;
        gvDCDet.DataBind();
        Bind_All_Grids(gvDCDet, "[USP_MDC_ITEM_DETAILS_ALL]");
        Bind_All_Grids(gvOutwardBind, "USP_OUTWARD_MDC_DET");
        Bind_All_Grids(gvStkMvmtDet, "USP_StockMovment_DET");
    }
    private void Bind_All_Grids(GridView gv, string Procedure)
    {
        try
        {
            SqlCommand cmd = new SqlCommand(Procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            
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
    protected void gvDCDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDCDet.PageIndex = e.NewPageIndex;
        //Bind_All_Grids(gvDCDet, "USP_DC_ITEM_DETAILS_ALL");
        Bind_All_Grids();


    }
    protected void gvOutwardBind_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOutwardBind.PageIndex = e.NewPageIndex;
        //Bind_All_Grids(gvOutwardBind, "USP_DC_ITEM_DETAILS_ALL");
        Bind_All_Grids();
    }
}