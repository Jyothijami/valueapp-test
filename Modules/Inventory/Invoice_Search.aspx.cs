using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.Classes;
using Yantra.MessageBox;

public partial class Modules_Inventory_Invoice_Search : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGo2_Click(object sender, EventArgs e)
    {
        gvDCDetails.PageIndex = Convert.ToInt32(txtGo2.Text) - 1;

    }
    int OutwardQty = 0;
    protected void btnSearch2_Click(object sender, EventArgs e)
    {
        if (rbBranchTransfer.SelectedItem.Text == "Exclude BranchTransfer")
        {
            BindGrid_All2Cond();
        }
        else
        {
            BindGrid_All2();
        }
        lblOutwardQty.Text = OutwardQty.ToString();
    }
    private void BindGrid_All2Cond()
    {
        SqlCommand cmd = new SqlCommand("[USP_Inventory_Serach_Invoice_Cond]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtMRN2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Chk_No", txtMRN2.Text);
        }
        if (txtFromDate2.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate2.Text));
        }
        if (txtToDate2.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate2.Text));
        }
        if (txtModel2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Model_No", txtModel2.Text);
        }
        if (txtCust.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtCust.Text);
        }

        if (txtColor2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtColor2.Text);
        }

        if (txtBrand2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtBrand2.Text);
        }

        if (txtCat2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtCat2.Text);
        }
        if (txtSubCat2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtSubCat2.Text);
        }
        if (txtRemark2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Remarks", txtRemark2.Text);
        }
        if (ddlCompany2.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany2.SelectedItem.Value);
        }


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDCDetails.DataSource = dt;
        gvDCDetails.DataBind();
    }
    private void BindGrid_All2()
    {
        SqlCommand cmd = new SqlCommand("[USP_Inventory_Serach_Invoice]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtMRN2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Chk_No", txtMRN2.Text);
        }
        if (txtFromDate2.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate2.Text));
        }
        if (txtToDate2.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate2.Text));
        }
        if (txtModel2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Model_No", txtModel2.Text);
        }
        if (txtCust.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtCust.Text);
        }

        if (txtColor2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtColor2.Text);
        }

        if (txtBrand2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtBrand2.Text);
        }

        if (txtCat2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtCat2.Text);
        }
        if (txtSubCat2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtSubCat2.Text);
        }
        if (txtRemark2.Text != "")
        {
            cmd.Parameters.AddWithValue("@Remarks", txtRemark2.Text);
        }
        if (ddlCompany2.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany2.SelectedItem.Value);
        }


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDCDetails.DataSource = dt;
        gvDCDetails.DataBind();
    }
    protected void gvDCDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDCDetails.PageIndex = e.NewPageIndex;
        if (rbBranchTransfer.SelectedItem.Text == "Exclude BranchTransfer")
        {
            BindGrid_All2Cond();
        }
        else
        {
            BindGrid_All2();
        }
    }
    protected void gvDCDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TotalQty = TotalQty + int.Parse (e.Row.Cells[5].Text);
            OutwardQty = OutwardQty + int.Parse(e.Row.Cells[9].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = lblOutwardQty.Text = OutwardQty.ToString();
        }
    }
    protected void btnDCExport_Click(object sender, EventArgs e)
    {
        if (gvDCDetails.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventoryDCReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvDCDetails.AllowPaging = false;
                BindGrid_All2();
                //gvDCDetails.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvDCDetails.HeaderRow.Cells)
                {
                    cell.BackColor = gvDCDetails.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvDCDetails.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvDCDetails.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvDCDetails.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvDCDetails.RenderControl(hw);

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
            MessageBox.Show(this, "There is No Data To Export To Excel");
        }
    }
    protected void ddlNoOfRecord2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDCDetails.PageSize = Convert.ToInt32(ddlNoOfRecord2.SelectedValue);
        gvDCDetails.DataBind();
    }
}