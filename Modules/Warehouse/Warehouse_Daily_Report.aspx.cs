﻿using System;
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

public partial class Modules_Warehouse_Warehouse_Daily_Report : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //gvDCDet.DataBind();

            pnlInward.Visible = true;

            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            Masters.ItemMaster.ItemMaster_Select(ddlModelNo);
            Masters.ColorMaster.Color_Select(ddlColor);
            //Masters.ItemMaster.ItemMasterModelNo_Select(ddlMdlNo);
            Masters.CompanyProfile.Company_Select(ddlComp);
            ItemCategoryFill();

            BindInward_Report();
        }
    }
    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlCategory);
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
        //ItemCategoryFill();
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlColor.Items.Clear();
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
    }
    protected void lnkDayBasisStk_Click(object sender, EventArgs e)
    {
        pnlDailyBasisStk.Visible = true;
        pnlInward.Visible = false;
        pnlOutward.Visible = false;
        pnlMovingDc.Visible = false;
        pnlstk.Visible = false;
        pnlyrstk.Visible = false ;
    }
    protected void lnkYeardata_Click(object sender, EventArgs e)
    {
        pnlInward.Visible = false;
        pnlOutward.Visible = false;
        pnlMovingDc.Visible = false;
        pnlDailyBasisStk.Visible = false;
        pnlstk.Visible = false;
        pnlyrstk.Visible = true;
        BindInward_Report();
    }
    protected void btnGoMdl_Click(object sender, EventArgs e)
    {

        gvStockReportyear.Visible = true;
        BindGridOnModelSearchForYear();
        

    }
    protected void lnkInward_Click(object sender, EventArgs e)
    {
        pnlInward.Visible = true;
        pnlOutward.Visible = false;
        pnlMovingDc.Visible = false;
        pnlDailyBasisStk.Visible = false;
        pnlstk.Visible = false;
        pnlyrstk.Visible = false;
        BindInward_Report();
    }
    protected void lnkOutward_Click(object sender, EventArgs e)
    {
        pnlInward.Visible = false;
        pnlOutward.Visible = true;
        pnlMovingDc.Visible = false;
        pnlDailyBasisStk.Visible = false;
        pnlstk.Visible = false;
        pnlyrstk.Visible = false;
        BindOutward_Report();
    }
    protected void gvInwardReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInwardReport.PageIndex = e.NewPageIndex;
        BindInward_Report();
    }

    private void BindInward_Report()
    {
        SqlCommand cmd = new SqlCommand("USP_Warehouse_Inward_Daily_Report", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (ddlBranch1.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Wh_Loc_Id", ddlBranch1.SelectedItem.Value);
        }

        if (txtFromDate1.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate1.Text));
        }
        if (txtToDate1.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate1.Text));
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInwardReport.DataSource = dt;
        gvInwardReport.DataBind();
    }
    protected void btnExprot_Click(object sender, EventArgs e)
    {
        #region Export to Excel
        if (gvInwardReport.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=WarehouseInwardReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvInwardReport.AllowPaging = false;
                BindInward_Report();
                gvInwardReport.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvInwardReport.HeaderRow.Cells)
                {
                    cell.BackColor = gvInwardReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvInwardReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvInwardReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvInwardReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvInwardReport.RenderControl(hw);

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
        #endregion
    }
    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        BindInward_Report();
    }
    protected void ddlLocations1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch1.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvInwardReport.PageIndex = Convert.ToInt32(txtPageNo1.Text) - 1;

    }
    protected void btnGosearch_Click(object sender, EventArgs e)
    {
        gvStockReportyear.PageIndex = Convert.ToInt32(txtgo.Text) - 1;

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvInwardReport.PageSize = Convert.ToInt32(ddlNoOfRecords1.SelectedValue);
        gvInwardReport.DataBind();
    }
    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        BindOutward_Report();

    }

    private void BindOutward_Report()
    {
        SqlCommand cmd = new SqlCommand("USP_Warehouse_Outward_Daily_Report", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (ddlBranch2.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Wh_Loc_Id", ddlBranch2.SelectedItem.Value);
        }
        if (txtFromDate2.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate2.Text));
        }
        if (txtToDate2.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate2.Text));
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvOutwardReport.DataSource = dt;
        gvOutwardReport.DataBind();
    }

    protected void btnExprot2_Click(object sender, EventArgs e)
    {
        #region Export to Excel
        if (gvOutwardReport.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=WarehouseOutwardReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvOutwardReport.AllowPaging = false;
                BindOutward_Report();
                gvOutwardReport.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvOutwardReport.HeaderRow.Cells)
                {
                    cell.BackColor = gvOutwardReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvOutwardReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvOutwardReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvOutwardReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvOutwardReport.RenderControl(hw);

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
        #endregion
    }
    protected void gvOutwardReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOutwardReport.PageIndex = e.NewPageIndex;
        BindOutward_Report();
    }
    protected void ddlNoOfRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvOutwardReport.PageSize = Convert.ToInt32(ddlNoOfRecords2.SelectedValue);
        gvOutwardReport.DataBind();
    }
    protected void btnPageNoSearch2_Click(object sender, EventArgs e)
    {
        gvOutwardReport.PageIndex = Convert.ToInt32(txtGo2.Text) - 1;
    }

    protected void ddlLocation2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch2.DataBind();

    }

    #region MovingDC
    protected void lnkMovingDc_Click(object sender, EventArgs e)
    {
        pnlInward.Visible = false;
        pnlOutward.Visible = false;
        pnlMovingDc.Visible = true;
        pnlstk.Visible = false;
        pnlDailyBasisStk.Visible = false;
        pnlyrstk.Visible = false;
        BindMovingDc_Report();
    }
    protected void lnkstock_Click(object sender, EventArgs e)
    {
        pnlInward.Visible = false;
        pnlOutward.Visible = false;
        pnlMovingDc.Visible = false;
        pnlDailyBasisStk.Visible = false;
        pnlyrstk.Visible = false;
        pnlstk.Visible = true;
    }
    private void BindMovingDc_Report()
    {
        SqlCommand cmd = new SqlCommand("USP_Warehouse_MovingDc_Daily_Report", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (ddlBranch3.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Wh_Loc_Id", ddlBranch3.SelectedItem.Value);
        }

        if (ddlBranch4.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Wh_Loc_Id_To", ddlBranch4.SelectedItem.Value);
        }

        if (txtFromDate3.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate3.Text));
        }
        if (txtToDate3.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate3.Text));
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvMovingDc.DataSource = dt;
        gvMovingDc.DataBind();   
    }
    protected void gvMovingDc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMovingDc.PageIndex = e.NewPageIndex;
        BindMovingDc_Report();
    }
    protected void btnExprot3_Click(object sender, EventArgs e)
    {
        #region Export to Excel
        if (gvMovingDc.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=WarehouseMovingDcReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvMovingDc.AllowPaging = false;
                BindMovingDc_Report();
                gvMovingDc.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvMovingDc.HeaderRow.Cells)
                {
                    cell.BackColor = gvMovingDc.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvMovingDc.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvMovingDc.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvMovingDc.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvMovingDc.RenderControl(hw);

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
        #endregion
    }
    protected void btnSearch3_Click(object sender, EventArgs e)
    {
        BindMovingDc_Report();
    }
    protected void ddlNoOfRecords3_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvMovingDc.PageSize = Convert.ToInt32(ddlNoOfRecords3.SelectedValue);
        gvMovingDc.DataBind();
    }
    protected void btnPageNoSearch3_Click(object sender, EventArgs e)
    {
        gvMovingDc.PageIndex = Convert.ToInt32(txtPageNo3.Text) - 1;

    }
    protected void btnPageNoSearch4_Click(object sender, EventArgs e)
    {
        gvStockReport.PageIndex = Convert.ToInt32(txtPageNo3.Text) - 1;


    }
    protected void ddlLocation3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch3.DataBind();
        ddlBranch4.DataBind();

    }
    #endregion
    //protected void ddlLocation4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlBranch4.DataBind();
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        btnExport.Visible = true;
        btnExport2.Visible = false;
        gvStockReport.Visible = true;
        gvModelNoSearch.Visible = false;
        BindSearchGrid();
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_AuditStockReportNew_Serach]", con);
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

        if (ddlCategory.SelectedIndex != 0 && ddlCategory.SelectedIndex != -1)
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
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ItemCode", txtModelNo.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataRow dr = dt.Rows[i];

        //    if (dt.Rows[i]["Closing Stock"].ToString() == "0")
        //        dr.Delete();

        //    //if (dr["Total Available Stock"] == "0")
        //    //    dr.Delete();
        //}

        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();
    }

    protected void gvStockReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Cells[8].Visible = false;

            // Total Inward -Green
            e.Row.Cells[4].ForeColor = Color.Green;
            // Total Outward -Red
            e.Row.Cells[5].ForeColor = Color.Red;
            // Total Block -Dark Red
            e.Row.Cells[6].ForeColor = Color.DarkRed;

            //if (e.Row.Cells[4].Text == e.Row.Cells[5].Text)
            //{
            //    e.Row.Visible = false;
            //}
        }
    }

    protected void ddlNoOfRecords4_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStockReport.PageSize = Convert.ToInt32(ddlNoOfRecords4.SelectedValue);
        gvStockReport.DataBind();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        btnExport.Visible = false;
        btnExport2.Visible = true;
        gvStockReport.Visible = false;
        gvModelNoSearch.Visible = true;
        BindGridOnModelSearch();
    }

    private void BindGridOnModelSearch()
    {
        if (txtModelNo.Text != "")
        {
            SqlCommand cmd = new SqlCommand("[USP_AuditStockReportNew_ModelNo]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvModelNoSearch.DataSource = dt;
            gvModelNoSearch.DataBind();
        }
        else
        {
            MessageBox.Show(this, "Please Enter ModelNo");
        }
    }
    private void BindGridOnModelSearchForYear()
    {
        
            SqlCommand cmd = new SqlCommand("[USP_YearStockReport_ModelNo]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlMdlNo.SelectedIndex > -1)
            {
                cmd.Parameters.AddWithValue("@ModelNo", ddlMdlNo.SelectedItem .Value );
            }
            if (ddlComp.SelectedItem.Value != "0")
            {
                cmd.Parameters.AddWithValue("@CP_ID", ddlComp.SelectedItem.Value);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            lblInwardQty.Text = InwardQty.ToString();
            lblOutwardQty.Text = OutwardQty.ToString();
            gvStockReportyear.DataSource = dt;
            gvStockReportyear.DataBind();
        
       
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Export To Excel
        if (gvStockReport.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StockReport.xls");
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
        #endregion

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);
            // ddlColor.Items.Clear();
            Masters.ColorMaster.Color_Select(ddlColor, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlBrand.SelectedValue));

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

    protected void gvStockReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStockReport.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }

    protected void gvModelNoSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[2].ForeColor = Color.Black;
            //e.Row.Cells[3].ForeColor = Color.Black;

            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Visible = false;
            e.Row.Cells[8].Visible = false;            

            // Total Inward -Green
            e.Row.Cells[4].ForeColor = Color.Green;
            // Total Outward -Red
            e.Row.Cells[5].ForeColor = Color.Red;
            // Total Block -Dark Red
            e.Row.Cells[6].ForeColor = Color.DarkRed;

            //if (e.Row.Cells[4].Text == e.Row.Cells[5].Text)
            //{
            //    e.Row.Visible = false;
            //}
        }
    }
    protected void gvModelNoSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvModelNoSearch.PageIndex = e.NewPageIndex;
        BindGridOnModelSearch();

    }
    protected void btnExport2_Click(object sender, EventArgs e)
    {
        #region Export To Excel
        if (gvModelNoSearch.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StockReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvModelNoSearch.AllowPaging = false;
                this.BindSearchGrid();


                gvModelNoSearch.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvModelNoSearch.HeaderRow.Cells)
                {
                    cell.BackColor = gvModelNoSearch.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvModelNoSearch.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvModelNoSearch.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvModelNoSearch.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvModelNoSearch.RenderControl(hw);

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
        #endregion
    }
   
    private void Bind_All_Grids()
    {
        gvDCDet.DataSource = null;
        gvDCDet.DataBind();
        Bind_All_Grids(gvDCDet, "[USP_DC_Stock_ITEM_DETAILS_ALL]");
        //Bind_All_Grids(gvOutwardBind, "USP_OUTWARD_DC_DET");
        //Bind_All_Grids(gvStkMvmtDet, "USP_StockMovment_DET");
    }
    protected void btnDCSearch_Click(object sender, EventArgs e)
    {
        Bind_All_Grids();
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

            if (txtItmModelNo.Text != "")
            {
                cmd.Parameters.AddWithValue("@Model_No", txtItmModelNo.Text);
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


    protected void gvStockReportyear_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStockReportyear.PageIndex = e.NewPageIndex;
        BindGridOnModelSearchForYear();
    }
    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlMdlNo.DataSourceID = "SqlDataSource21";
        ddlMdlNo.DataTextField = "ITEM_MODEL_NO";
        ddlMdlNo.DataValueField = "ITEM_CODE";
        ddlMdlNo.DataBind();
        //ddlModelNo_SelectedIndexChanged(sender, e);


    }
    int InwardQty = 0, OutwardQty = 0;
    protected void gvStockReportyear_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InwardQty += Convert.ToInt32(e.Row.Cells[5].Text);
            OutwardQty += Convert.ToInt32(e.Row.Cells[6].Text);

            //e.Row.Cells[7].Text = (Convert.ToInt64(e.Row.Cells[5].Text) - Convert.ToInt64(e.Row.Cells[6].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = lblInwardQty.Text = InwardQty.ToString();
            e.Row.Cells[6].Text = lblOutwardQty.Text = OutwardQty.ToString();

        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStockReportyear.PageSize = Convert.ToInt32(DropDownList1.SelectedValue);
        gvStockReportyear.DataBind();
    }
}
 
