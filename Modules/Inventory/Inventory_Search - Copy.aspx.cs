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

public partial class Modules_Inventory_Inventory_Search : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string reference;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            pnlMRN.Visible = true;
            BindGrid_AllCond();
            BindGrid_All2Cond();
            BindOtherStockGrid_All();
            BinddcGrid_All2();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "9");
        btnExprot.Enabled = up.Email;
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvInventoryInward.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (rdbBT.SelectedItem.Text == "Exclude BranchTransfer")
        {
            BindGrid_AllCond();
        }
        else
        {
            BindGrid_All();
        }
        lblInwardQty.Text = InwardQty.ToString();
    }
    private void BindGrid_AllCond()
    {
        SqlCommand cmd = new SqlCommand("USP_Inventory_Serach_Cond", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtMrnNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@Chk_No", txtMrnNo.Text);
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
            cmd.Parameters.AddWithValue("@Model_No", txtModelNo.Text);
        }
        if (txtSupplier.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtSupplier.Text);
        }
        if (txtColor.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtColor.Text);
        }

        if (txtBrand.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtBrand.Text);
        }

        if (txtCategory.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
        }
        if (txtSubCat.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtSubCat.Text);
        }
        if (txtRemarks.Text != "")
        {
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInventoryInward.DataSource = dt;
        gvInventoryInward.DataBind();
    }
    protected void btnExprot_Click(object sender, EventArgs e)
    {
        if (gvInventoryInward.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventorMRNyReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvInventoryInward.AllowPaging = false;
                BindGrid_All();
                gvInventoryInward.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvInventoryInward.HeaderRow.Cells)
                {
                    cell.BackColor = gvInventoryInward.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvInventoryInward.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvInventoryInward.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvInventoryInward.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvInventoryInward.RenderControl(hw);

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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_Inventory_Serach", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtMrnNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@Chk_No", txtMrnNo.Text);
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
            cmd.Parameters.AddWithValue("@Model_No", txtModelNo.Text);
        }
        if (txtSupplier.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtSupplier.Text);
        }
        if (txtColor.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtColor.Text);
        }

        if (txtBrand.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtBrand.Text);
        }
        
        if (txtCategory.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
        }
        if (txtSubCat.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtSubCat.Text);
        }
        if (txtRemarks.Text != "")
        {
            cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }

        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInventoryInward.DataSource = dt;
        gvInventoryInward.DataBind();
    }

    private void BindStatGrid()
    {
        SM.SalesOrder obj = new SM.SalesOrder();

        for (int i = 0; i < chkPONo.Items.Count; i++)
        {
            if (chkPONo.Items[i].Selected == true)
            {
                lblchkpoid.Text = chkPONo.Items[i].Value;
                SqlCommand cmd = new SqlCommand("USP_Statement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (ddlCustomerName.SelectedIndex != 0)
                {
                    cmd.Parameters.AddWithValue("@CustId", ddlCustomerName.SelectedItem.Value);
                }
                if (lblchkpoid.Text != "")
                {
                    cmd.Parameters.AddWithValue("@SO_NO", lblchkpoid.Text);

                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvStat.DataSource = dt;
                gvStat.DataBind();
                foreach (GridViewRow gvrow in gvStat.Rows)
                {
                    GridView gvDC = (GridView)(gvStat.Rows[gvrow.RowIndex].Cells[2].FindControl("gvDC"));
                    gvDC.DataSource = dt;
                    gvDC.DataBind();
                }
            }
        }
    }
    
    private void BindOtherStockGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_Sales_Sample_OS_Stock_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtReference3.Text != "")
        {
            cmd.Parameters.AddWithValue("@RefNo", txtReference3.Text);
        }
        if (txtFromDate3.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate3.Text));
        }
        if (txtToDate3.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate3.Text));
        }
        if (txtModelNo3.Text != "")
        {
            cmd.Parameters.AddWithValue("@Model_No", txtModelNo3.Text);
        }

        if (txtColor3.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtColor3.Text);
        }

        if (txtBrand3.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtBrand3.Text);
        }

        if (txtCat3.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtCat3.Text);
        }
        if (txtSubCat3.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtSubCat3.Text);
        }

        if (ddlCompName3.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompName3.SelectedItem.Value);
        }
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvOtherStock.DataSource = dt;
        gvOtherStock.DataBind();
    }
    protected void gvInventoryInward_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvInventoryInward.PageIndex = e.NewPageIndex;
        BindGrid_All();
    }
    protected void lnkMRN_Click(object sender, EventArgs e)
    {
        reference = "MRN";
        pnlDC.Visible = false;
        pnlMRN.Visible = true;
        pnlOtherStock.Visible = false;
        pnlStatment.Visible = false;
        pnlDCStat.Visible = false;
    }
    protected void lnkDC_Click(object sender, EventArgs e)
    {
        reference = "DC";
        pnlStatment.Visible = false;
        pnlDC.Visible = true;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlDCStat.Visible = false;


    }
    protected void btnGo2_Click(object sender, EventArgs e)
    {
        gvDCDetails.PageIndex = Convert.ToInt32(txtGo2.Text) - 1;

    }
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
    protected void btnSearch4_Click(object sender, EventArgs e)
    {
        //if (rbBranchTransfer.SelectedItem.Text == "Exclude BranchTransfer")
        //{
        //    BindGrid_All2Cond();
        //}
        //else
        //{
        //    BindGrid_All2();
        //}
        //lblOutwardQty.Text = OutwardQty.ToString();
        BinddcGrid_All2();

    }
    private void BinddcGrid_All2()
    {
        SqlCommand cmd = new SqlCommand("[USP_Inventory_Serach_DCStat]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (TxtDcNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@Chk_No", TxtDcNo.Text);
        }
        if (txtDCFrom.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtDCFrom.Text));
        }
        if (txtDCTo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtDCTo.Text));
        }
        if (txtDCModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@Model_No", txtDCModelNo.Text);
        }
        if (txtDCCust.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtDCCust.Text);
        }

        if (txtDCColor.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtDCColor .Text);
        }

        if (txtDCBrand.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtDCBrand.Text);
        }

        if (txtDcCate.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtDcCate.Text);
        }
        if (txtDCSub.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtDCSub.Text);
        }
        if (txtDCRemarks.Text != "")
        {
            cmd.Parameters.AddWithValue("@Remarks", txtDCRemarks.Text);
        }
        if (ddlDCComp.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlDCComp.SelectedItem.Value);
        }


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDCStat.DataSource = dt;
        gvDCStat.DataBind();
    }
    private void BindGrid_All2Cond()
    {
        SqlCommand cmd = new SqlCommand("USP_Inventory_Serach_DC_Cond", con);
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
        SqlCommand cmd = new SqlCommand("USP_Inventory_Serach_DC", con);
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
                gvDCDetails.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvDCDetails.HeaderRow.Cells)
                {
                    cell.BackColor = gvDCDetails.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvDCDetails.Rows)
                {
                    row.BackColor = Color.White;
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
	protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvInventoryInward.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvInventoryInward.DataBind();
    }
    protected void ddlNoOfRecord2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDCDetails.PageSize = Convert.ToInt32(ddlNoOfRecord2.SelectedValue);
        gvDCDetails.DataBind();
    }
    protected void ddlNoOfRecord4_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDCStat.PageSize = Convert.ToInt32(ddlNoOfRecord4.SelectedValue);
        gvDCStat.DataBind();
    }
    protected void lnkOtherStock_Click(object sender, EventArgs e)
    {
        reference = "Other";
        pnlStatment.Visible = false;
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = true;
    }
    protected void ddlNoOfRecords3_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvOtherStock.PageSize = Convert.ToInt32(ddlNoOfRecords3.SelectedValue);
        gvOtherStock.DataBind();
    }
    protected void btnGOPage3_Click(object sender, EventArgs e)
    {
        gvOtherStock.PageIndex = Convert.ToInt32(txtGo3.Text) - 1;
    }

    protected void btnExport3_Click(object sender, EventArgs e)
    {
        if (gvOtherStock.Rows.Count > 0)
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
                gvOtherStock.AllowPaging = false;
                BindOtherStockGrid_All();
                gvOtherStock.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvOtherStock.HeaderRow.Cells)
                {
                    cell.BackColor = gvOtherStock.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvOtherStock.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvOtherStock.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvOtherStock.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvOtherStock.RenderControl(hw);

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
    protected void btnSearch3_Click(object sender, EventArgs e)
    {
        BindOtherStockGrid_All();
    }
    protected void gvOtherStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOtherStock.PageIndex = e.NewPageIndex;
        BindOtherStockGrid_All();
    }
    int InwardQty = 0, OutwardQty = 0, OtherQty = 0;
    protected void gvInventoryInward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InwardQty += Convert.ToInt32(e.Row.Cells[8].Text);
            //InwardQty = InwardQty + int.Parse(e.Row.Cells[8].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = lblInwardQty.Text = InwardQty.ToString();
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
    protected void gvOtherStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TotalQty = TotalQty + int.Parse (e.Row.Cells[5].Text);
            OtherQty = OtherQty + int.Parse(e.Row.Cells[9].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = lblOtherQty.Text = OtherQty.ToString();
        }
    }
    protected void lnkInvoice_Click(object sender, EventArgs e)
    {
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false ;
        pnlStatment.Visible = true;
        pnlDCStat.Visible = false;
    }
    protected void lnkDCStat_Click(object sender, EventArgs e)
    {
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlStatment.Visible = false ;
        pnlDCStat.Visible = true;
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource3";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.InvoiceCustomerMaster_SelectForCustomer(ddlCustomerName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
        {
            txtUnitAdd.Text = objSMCustomer.Address;
        }
        lblpono.Visible = true;
        Masters.CheckboxListWithStatement(chkPONo, "select * from YANTRA_SO_MAST where SO_CUST_ID = "+ddlCustomerName .SelectedItem .Value );
        
           
        
    }

    protected void btnSearchStat_Click(object sender, EventArgs e)
    {
        BindStatGrid();
    }
    protected void gvStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStat.PageIndex = e.NewPageIndex;
        BindStatGrid();
    }
    protected void gvStat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string msg ="Client Name : "+ ddlCustomerName.SelectedItem.Text + "<br>" + txtUnitAdd.Text + ""
               + "<br>" + "<br>" + "PO NO : " +chkPONo .SelectedItem .Text + " Date : "+lblpodate.Text+"<br>";
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = msg;
            HeaderCell.ColumnSpan = 10;
            
            HeaderGridRow.Cells.Add(HeaderCell);
            gvStat.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void btnExportStat_Click(object sender, EventArgs e)
    {
        if (gvStat.Rows.Count > 0)
        {
             using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages

                    gvStat.AllowPaging = false;
                    BindStatGrid();
                    //gvterms.AllowPaging = false;
                    //gvterms.DataBind();
                    //gvitemsgrid.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvStat.HeaderRow.Cells)
                    {
                        cell.BackColor = gvStat.HeaderStyle.BackColor;
                        cell.BackColor = gvStat.HeaderStyle.BackColor;

                    }
                    foreach (GridViewRow row in gvStat.Rows)
                    {
                        //row.BackColor = Color.White;
                        row.HorizontalAlign = HorizontalAlign.Center;
                        gvStat.HorizontalAlign = HorizontalAlign.Center;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvStat.AlternatingRowStyle.BackColor;
                                cell.BackColor = gvStat.AlternatingRowStyle.BackColor;

                                cell.Wrap = true;
                            }
                            else
                            {
                                cell.BackColor = gvStat.RowStyle.BackColor;
                                cell.BackColor = gvStat.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                        
                        gvStat.Style["font-family"] = "Book Antiqua, Helvetica, sans-serif;";
                       
                    }

                    gvStat.RenderControl(hw);
                    //gvterms.RenderControl(hw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Quotation.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    
                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        
    }
    protected void chkPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.SalesOrder obj = new SM.SalesOrder();
        if (obj.SalesOrder_Select(chkPONo.SelectedItem.Value) > 0)
        {
            lblpodate.Text = obj.SODate;
        }
    }
    protected void gvDC_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
 
