using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using Yantra.Classes;
using vllib;
using System.IO;
using System.Drawing;

using System.Data.SqlClient;
public partial class Modules_Reports_PurchaseOrder_Report : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            BrandFill();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "9");
        btnExportGrid.Enabled = up.Email;
        btnPIExport.Enabled = up.Email;
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPOSearch.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        BindSearchGrid();

    }
    private void BrandFill()
    {
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
        Masters.ProductCompany.ProductCompany_Select(ddlPIBrand);

    }


    //protected void btnPageNoSearch_Click(object sender, EventArgs e)
    //{
    //    gvPOSearch.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearchGrid();
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_PurchaseReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if(txtModelNo.Text !="")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
        }
        if(ddlBrand.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);

        }
        if(txtFrom.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFrom.Text));

        }
        if(txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));

        }
        if(txtCustomer.Text != "")
        {
            cmd.Parameters.AddWithValue("@Customer", txtCustomer.Text);

        }
        if(txtSupplierName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Supplier", txtSupplierName.Text);

        }
        if(txtPONo.Text != "")
        {
            cmd.Parameters.AddWithValue("@PONo", txtPONo.Text);

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvPOSearch.DataSource = dt;
        gvPOSearch.DataBind();
    }
    protected void gvPOSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPOSearch.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnExportGrid_Click(object sender, EventArgs e)
    {
        if (gvPOSearch.Rows.Count > 0)
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
                gvPOSearch.AllowPaging = false;
                this.BindSearchGrid();

                gvPOSearch.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvPOSearch.HeaderRow.Cells)
                {
                    cell.BackColor = gvPOSearch.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvPOSearch.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvPOSearch.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvPOSearch.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvPOSearch.RenderControl(hw);

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
    protected void gvPIReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPIReport.PageIndex = e.NewPageIndex;
        BindPISearchGrid();
    }
    protected void btnPISearch_Click(object sender, EventArgs e)
    {
        BindPISearchGrid();
    }
    protected void btnPIExport_Click(object sender, EventArgs e)
    {
        if (gvPOSearch.Rows.Count > 0)
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
                gvPIReport.AllowPaging = false;
                this.BindPISearchGrid();

                gvPIReport.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvPIReport.HeaderRow.Cells)
                {
                    cell.BackColor = gvPIReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvPIReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvPIReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvPIReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvPIReport.RenderControl(hw);

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
    private void BindPISearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_PurchaseInvoiceReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (txtPIModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtPIModelNo.Text);
        }
        if (ddlPIBrand.SelectedIndex > 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlPIBrand.SelectedItem.Value);

        }
        if (txtPIFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtPIFromDate.Text));

        }
        if (txtPIToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtPIToDate.Text));

        }
        if (txtPICustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Customer", txtPICustName.Text);

        }
        if (txtPISupName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Supplier", txtPISupName.Text);

        }
        if (txtPIInvoiceNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SupInvoiceNo", txtPIInvoiceNo.Text);

        }
        if (txtPIVehicleNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@PIVehicleNo", txtPIVehicleNo.Text);

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvPIReport.DataSource = dt;
        gvPIReport.DataBind();
    }
    protected void btnPO_Click(object sender, EventArgs e)
    {
        pnlPO.Visible = true;
        pnlPI.Visible = false;
    }
    protected void btnPI_Click(object sender, EventArgs e)
    {
        pnlPO.Visible = false;
        pnlPI.Visible = true;
    }
    protected void ddlPIgv_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPIReport.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        BindPISearchGrid();

    }
}
 
