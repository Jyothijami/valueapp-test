using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_SM_SalesLead : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        if (!IsPostBack)
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand2);

        }
    }

    

    protected void ddlBrand2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlBrand.SelectedIndex = ddlBrand2.SelectedIndex;
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

            BindSearchGrid();


            //Masters.ItemMaster.ModelNoSelect_Brand_Cat_SubCat_fortelerik(RadAutoCompleteBox1, ddlBrand2.SelectedItem.Value, ddlCategory.SelectedItem.Value, ddlSubCat.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //Masters.Dispose();
        }
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
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

            e.Row.Cells[1].Visible = false;

            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[0].Text = "Page " + (gvItemMaster.PageIndex + 1) + " of " + gvItemMaster.PageCount;
        }
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_GetItemsReport_2", con);
        //SqlCommand cmd = new SqlCommand("[USP_StockReportNew_Serach]", con);
        cmd.CommandType = CommandType.StoredProcedure;



        //if (txtModelNo.Text != "" && txtModelNo.Text != null)
        //{
        //    cmd.Parameters.AddWithValue("@Model", txtModelNo.Text);
        //}
        //if (txtseries.Text != "" && txtseries.Text != null)
        //{
        //    cmd.Parameters.AddWithValue("@Series", txtseries.Text);
        //}
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
        //if (ddlModelNo.SelectedIndex > 0)
        //{
        //    cmd.Parameters.AddWithValue("@Item_Code", ddlModelNo.SelectedItem.Value);
        //}

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvItemMaster.DataSource = dt;
        gvItemMaster.DataBind();
        // gvItemMaster.PageIndex = 0;

    }
}