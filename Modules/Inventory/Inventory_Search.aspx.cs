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
            BindCashDCs();
            BindStatGrid();
            BinddcGrid_All2();
            BindMDCGrid();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "9");
        //btnExprot.Enabled = up.Email;
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
    //private void BindReturnGrid()
    //{
    //    SM.SalesOrder obj = new SM.SalesOrder();

    //    for (int i = 0; i < chkPONo.Items.Count; i++)
    //    {
    //        if (chkPONo.Items[i].Selected == true)
    //        {
    //            lblchkpoid.Text = chkPONo.Items[i].Value;
    //            SqlCommand cmd = new SqlCommand("USP_ReturnStatment", con);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            if (ddlCustomerName.SelectedItem.Value != "")
    //            {
    //                cmd.Parameters.AddWithValue("@Cust_Name", ddlCustomerName.SelectedItem.Value);
    //            }
    //            if (lblchkpoid.Text != "")
    //            {
    //                cmd.Parameters.AddWithValue("@SO_NO", lblchkpoid.Text);

    //            }
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            da.Fill(dt);
    //            gvReturnItems.DataSource = dt;
    //            gvReturnItems.DataBind();
    //            //BindChileDCGV();
    //            //BindInvoiceRate();
    //            //BindChildSI();
    //        }
    //    }
    //}
    private void BindCashDCs()
    {
        SM.SalesOrder obj = new SM.SalesOrder();
        string condition = string.Empty;
        foreach (ListItem item in chkDcNo.Items)
        {
            condition += item.Selected ? string.Format("'{0}',", item.Value) : string.Empty;
        }
        if (!string.IsNullOrEmpty(condition))
        {
            SqlCommand cmd = new SqlCommand("USP_CashDCStatement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlCustomerName.SelectedItem.Value != "")
            {
                cmd.Parameters.AddWithValue("@Cust_Name", ddlCustomerName.SelectedItem.Value);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                condition = string.Format(condition.Substring(0, condition.Length - 1));
                cmd.Parameters.AddWithValue("@SO_NO", condition);

            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCashDcs.DataSource = dt;
            gvCashDcs.DataBind();
            //BindChileDCGV();
            BindCashInvoiceRate();
            BindCashReturn();
        }
    }
    private void BindStatGrid()
    {

        SM.SalesOrder obj = new SM.SalesOrder();
        string condition = string.Empty;
        foreach (ListItem item in chkPONo.Items)
        {
            condition += item.Selected ? string.Format("'{0}',", item.Value) : string.Empty;
        }
        if (!string.IsNullOrEmpty(condition))
        {
            SqlCommand cmd = new SqlCommand("USP_Statement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlCustomerName.SelectedItem.Value != "")
            {
                cmd.Parameters.AddWithValue("@Cust_Name", ddlCustomerName.SelectedItem.Value);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                condition = string.Format(condition.Substring(0, condition.Length - 1));
                cmd.Parameters.AddWithValue("@SO_NO", condition);

            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStat.DataSource = dt;
            gvStat.DataBind();
            BindChileDCGV();
        }
        //for (int i = 0; i < chkPONo.Items.Count; i++)
        //{
        //    if (chkPONo.Items[i].Selected == true)
        //    {
        //        lblchkpoid.Text = chkPONo.Items[i].Value;
        //        SqlCommand cmd = new SqlCommand("USP_Statement", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        if (ddlCustomerName.SelectedItem.Value !="")
        //        {
        //            cmd.Parameters.AddWithValue("@Cust_Name", ddlCustomerName.SelectedItem.Value);
        //        }
        //        if (lblchkpoid.Text != "")
        //        {
        //            cmd.Parameters.AddWithValue("@SO_NO", lblchkpoid.Text);

        //        }
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        gvStat.DataSource = dt;
        //        gvStat.DataBind();
        //        BindChileDCGV();
        //        //BindInvoiceRate();
        //        //BindChildSI();
        //    }
        //}
    }
    private void BindCashReturn()
    {
        foreach (GridViewRow gvrow in gvCashDcs.Rows)
        {
            GridView gvSIChild = (GridView)(gvCashDcs.Rows[gvrow.RowIndex].Cells[8].FindControl("gvReturn"));

            SqlCommand cmd = new SqlCommand("[USP_ReturnStatment]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[6].Text == "&nbsp;")
            {
                gvrow.Cells[6].Text = "0";
            }
            cmd.Parameters.AddWithValue("@SOID", 0);
            if (gvrow.Cells[6].Text != "")
            {
                cmd.Parameters.AddWithValue("@DC_ID", gvrow.Cells[8].Text);
            }
            if (gvrow.Cells[6].Text != "")
            {
                cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[9].Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvSIChild.DataSource = dt;
            gvSIChild.DataBind();
        }
    }
    private void BindCashInvoiceRate()
    {
        foreach (GridViewRow gvrow in gvCashDcs.Rows)
        {
            GridView gvSIChild = (GridView)(gvCashDcs.Rows[gvrow.RowIndex].Cells[8].FindControl("gvSIChild"));

            SqlCommand cmd = new SqlCommand("[USP_SIStatement]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[7].Text == "&nbsp;")
            {
                gvrow.Cells[7].Text = "0";
            }
            cmd.Parameters.AddWithValue("@SOID", 0);
            if (gvrow.Cells[7].Text != "")
            {
                cmd.Parameters.AddWithValue("@SI_ID", gvrow.Cells[7].Text);
            }
            if (gvrow.Cells[9].Text != "")
            {
                cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[9].Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvSIChild.DataSource = dt;
            gvSIChild.DataBind();
        }
    }
   
    private void BindInvoiceRate()
    {
        foreach (GridViewRow gvrow in gvStat.Rows)
        {
            GridView gvDC = (GridView)(gvStat.Rows[gvrow.RowIndex].Cells[5].FindControl("gvDC"));
            foreach (GridViewRow gvrow1 in gvDC.Rows)
            {
                GridView gvSIChild = (GridView)(gvDC.Rows[gvrow1.RowIndex].Cells[5].FindControl("gvSIChild"));

                SqlCommand cmd = new SqlCommand("[USP_SIStatement]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (gvrow1.Cells[5].Text == "&nbsp;")
                {
                    gvrow1.Cells[5].Text ="0";
                }
                cmd.Parameters.AddWithValue("@SOID", 99);
                if (gvrow1.Cells[5].Text != "")
                {
                    cmd.Parameters.AddWithValue("@SI_ID", gvrow1.Cells[5].Text);
                }
                if (gvrow.Cells[6].Text != "")
                {
                    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSIChild.DataSource = dt;
                gvSIChild.DataBind();


                //lblDelTotal.Text += Convert.ToDecimal(dt.Rows[0]["SI_DET_Rate"].ToString()); 
            }
        }
    }
    private void BindChildSI()
    {
        foreach (GridViewRow gvrow in gvStat.Rows)
        {
            GridView gvDC = (GridView)(gvStat.Rows[gvrow.RowIndex].Cells[5].FindControl("gvSI"));
            
            SqlCommand cmd = new SqlCommand("[USP_SIStatement]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[5].Text != "")
            {
                cmd.Parameters.AddWithValue("@SOID", gvrow.Cells[5].Text);
            }
            if (gvrow.Cells[6].Text != "")
            {
                cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();
        }
    }

    private void BindChildReturn()
    {
        foreach (GridViewRow gvrow in gvStat.Rows)
        {
            GridView gvDC = (GridView)(gvStat.Rows[gvrow.RowIndex].Cells[5].FindControl("gvDC"));
            foreach (GridViewRow gvrow1 in gvDC.Rows)
            {
                GridView gvSIChild = (GridView)(gvDC.Rows[gvrow1.RowIndex].Cells[5].FindControl("gvReturn"));

                SqlCommand cmd = new SqlCommand("[USP_ReturnStatment]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (gvrow1.Cells[6].Text == "&nbsp;")
                {
                    gvrow1.Cells[6].Text = "0";
                }
                cmd.Parameters.AddWithValue("@SOID", 99);
                if (gvrow1.Cells[6].Text != "")
                {
                    cmd.Parameters.AddWithValue("@DC_ID", gvrow1.Cells[6].Text);
                }
                if (gvrow.Cells[6].Text != "")
                {
                    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSIChild.DataSource = dt;
                gvSIChild.DataBind();


                //lblDelTotal.Text += Convert.ToDecimal(dt.Rows[0]["SI_DET_Rate"].ToString()); 
            }
        }
    }
    private void BindInvoiceStatus()
    {
        foreach (GridViewRow gvrow in gvDCStat.Rows)
        {
            //GridView gvDC = (GridView)(gvDCStat.Rows[gvrow.RowIndex].Cells[3].FindControl("gvSubInvoice"));
            //SqlCommand cmd = new SqlCommand("USP_DCStatement", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //if (gvrow.Cells[3].Text != "")
            //{
            //    cmd.Parameters.AddWithValue("@DCID", gvrow.Cells[3].Text);
            //}
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //gvDC.DataSource = dt;
            //gvDC.DataBind();
        }
    }
    private void BindChileDCGV()
    {
        foreach (GridViewRow gvrow in gvStat.Rows)
        {
            GridView gvDC = (GridView)(gvStat.Rows[gvrow.RowIndex].Cells[5].FindControl("gvDC"));
            SqlCommand cmd = new SqlCommand("USP_DCStatement", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[5].Text != "")
            {
                cmd.Parameters.AddWithValue("@SOID", gvrow.Cells[5].Text);
            }
            if (gvrow.Cells[6].Text != "")
            {
                cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();
            BindInvoiceRate();
            BindChildReturn();

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
        pnlMovingDC.Visible = false;

    }
    protected void lnkDC_Click(object sender, EventArgs e)
    {
        reference = "DC";
        pnlStatment.Visible = false;
        pnlDC.Visible = true;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlDCStat.Visible = false;
        pnlMovingDC.Visible = false ;

    }
    protected void lnkMDC_Click(object sender, EventArgs e)
    {
        reference = "MDC";
        pnlStatment.Visible = false;
        pnlDC.Visible = false ;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlDCStat.Visible = false;
        pnlMovingDC.Visible = true;
    }
    protected void btnGo2_Click(object sender, EventArgs e)
    {
        gvDCDetails.PageIndex = Convert.ToInt32(txtGo2.Text) - 1;

    }
    protected void btnMDCGo2_Click(object sender, EventArgs e)
    {
        gvMDCDetails.PageIndex = Convert.ToInt32(txtMDCGo2.Text) - 1;

    }
    protected void btnMDCSearch_Click(object sender, EventArgs e)
    {
        BindMDCGrid();
        lblMDCQty.Text = MDCQty.ToString();

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
        if (txtDCCust.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtDCCust.Text);
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
        //BindInvoiceStatus();
        BindChileDCInvoiceGV();
        BindChileDCSalesGV();
        
    }

    private void BindMDCGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_Inventory_Serach_MDC]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtMDCNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@MDC_No", txtMDCNo.Text);
        }
        if (txtMDCFromdt.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtMDCFromdt.Text));
        }
        if (txtMDCToDt.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtMDCToDt.Text));
        }
        if (txtMDCModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@Model_No", txtMDCModelNo.Text);
        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@cust_Name", txtCustName.Text);
        }

        if (txtMDCColor.Text != "")
        {
            cmd.Parameters.AddWithValue("@Color", txtMDCColor.Text);
        }

        if (txtMDCBrand.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtMDCBrand.Text);
        }

        if (txtMDCCate.Text != "")
        {
            cmd.Parameters.AddWithValue("@Category", txtMDCCate.Text);
        }
        if (txtMDCSubCate.Text != "")
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", txtMDCSubCate.Text);
        }
        if (txtMDCRemarks.Text != "")
        {
            cmd.Parameters.AddWithValue("@Remarks", txtMDCRemarks.Text);
        }
        if (ddlMDCComp.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlMDCComp.SelectedItem.Value);
        }


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvMDCDetails.DataSource = dt;
        gvMDCDetails.DataBind();
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
    protected void lnkDCStat_Click(object sender, EventArgs e)
    {
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = false;
        pnlStatment.Visible = false;
        pnlDCStat.Visible = true;
        pnlMovingDC.Visible = false;

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
    protected void gvMDCDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMDCDetails.PageIndex = e.NewPageIndex;
        BindMDCGrid();

    }
   
    protected void gvDCStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDCStat.PageIndex = e.NewPageIndex;
        BinddcGrid_All2();
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
    protected void ddlMDCNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvMDCDetails.PageSize = Convert.ToInt32(ddlMDCNoOfRecords.SelectedValue);
        gvMDCDetails.DataBind();
    }
    protected void lnkOtherStock_Click(object sender, EventArgs e)
    {
        reference = "Other";
        pnlStatment.Visible = false;
        pnlDC.Visible = false;
        pnlMRN.Visible = false;
        pnlOtherStock.Visible = true;
        pnlDCStat.Visible = false;
        pnlMovingDC.Visible = false;
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
    Decimal InwardQty = 0, OutwardQty = 0, OtherQty = 0, MDCQty = 0;
    protected void gvInventoryInward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[14].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InwardQty += Convert.ToDecimal (e.Row.Cells[9].Text);
            //InwardQty = InwardQty + int.Parse(e.Row.Cells[8].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = lblInwardQty.Text = InwardQty.ToString();
        }
    }
    protected void gvMDCDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TotalQty = TotalQty + int.Parse (e.Row.Cells[5].Text);
            MDCQty = MDCQty + int.Parse(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[11].Text = lblMDCQty.Text = MDCQty.ToString();
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
        //lblpono.Visible = true;
        Masters.CheckboxListWithStatement(chkPONo, "select * from YANTRA_SO_MAST where SO_CUST_ID = "+ddlCustomerName .SelectedItem .Value );
        Masters.CheckboxListWithStatement(chkDcNo, "Select * from Yantra_delivery_Challan_mast where dc_For='Cash' and DC_CUSTOMER_ID =" + ddlCustomerName.SelectedItem.Value);
           
        
    }

    protected void btnSearchStat_Click(object sender, EventArgs e)
    {
        BindStatGrid();
        //BindReturnGrid();
        BindCashDCs();
    }
    protected void gvStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStat.PageIndex = e.NewPageIndex;
        BindStatGrid();
    }
    decimal TotalAmount = 0,PenTotal=0,GSTamt=0,GTotal=0,ReturnTotal=0,BlockedTotal=0,GSTPenAmt=0,GstGttl=0,BlcGSt=0,BlcGTtl=0, DelTtl=0,DelGST=0,DelGTtl=0;
    protected void gvStat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string msg ="Client Name : "+ ddlCustomerName.SelectedItem.Text + "<br>" + txtUnitAdd.Text + ""
               + "<br>" + "<br>" + "PO NO : " + lblpono.Text + " Date : " + lblpodate.Text + "<br>"   +"Status : "+lblpostatus.Text ;
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = msg;
            HeaderCell.ColumnSpan = 11;
            
            HeaderGridRow.Cells.Add(HeaderCell);

            gvStat.Controls[0].Controls.AddAt(0, HeaderGridRow);
            
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    string msg = "GST : "+lblPOGST .Text ;
        //    GridView FooterGrid = (GridView)sender;
        //    GridViewRow FooterGridRow = new GridViewRow(0, 0, DataControlRowType.Footer , DataControlRowState.Insert);
        //    TableCell FooterCell = new TableCell();
        //    FooterCell.Text = msg;
        //    FooterCell.ColumnSpan = 10;
        //    FooterGridRow.Cells.Add(FooterCell);
        //    gvStat.Controls[0].Controls.AddAt(0, FooterGridRow);
        //}
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow || e.Row .RowType ==DataControlRowType .Footer )
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            //e.Row.Cells[8].Visible = false;
            e.Row.Cells[13].Visible = false;
        }
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = Math.Round((Convert.ToDecimal(e.Row.Cells[4].Text) / Convert.ToDecimal(e.Row.Cells[2].Text)), 0).ToString();
            e.Row.Cells[10].Text = (Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(e.Row.Cells[3].Text)).ToString();
            e.Row.Cells[12].Text = (Convert.ToDecimal(e.Row.Cells[11].Text) * Convert.ToDecimal(e.Row.Cells[3].Text)).ToString();
            e.Row.Cells[13].Text = (Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[9].Text)).ToString();
            e.Row.Cells[8].Text = (Convert.ToDecimal(e.Row.Cells[13].Text) * Convert.ToDecimal(e.Row.Cells[3].Text)).ToString();
            //find the nested grid in the current row with findcontrol
            GridView gridView1 = e.Row.FindControl("gvDC") as GridView;
            //BindInvoiceRate();
            //e.Row.Cells[8].Text = lblDelTotal.Text;
            //check if it is the first nested gridview and show/hide the header
            if (e.Row.RowIndex == 0)
            {
                gridView1.ShowHeader = true;
            }
            else
            {
                gridView1.ShowHeader = false;
            }

            
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[4].Text);
            PenTotal = PenTotal + Convert.ToDecimal(e.Row.Cells[10].Text);
            BlockedTotal = BlockedTotal + Convert.ToDecimal(e.Row.Cells[12].Text);
            DelTtl = DelTtl + Convert.ToDecimal(e.Row.Cells[8].Text);
            DelGST = (DelTtl * 18) / 100;
            GSTamt = (TotalAmount * 18) / 100;
            GSTPenAmt = (PenTotal * 18) / 100;
            BlcGSt = (BlockedTotal * 18) / 100;
            DelGTtl = DelTtl + DelGST;
            BlcGTtl = BlockedTotal + BlcGSt;
            GstGttl = PenTotal + GSTPenAmt;
            GTotal = TotalAmount + GSTamt;

           
           
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[3].Text = "Total :";
            //e.Row.Cells[4].Text = TotalAmount.ToString();
            //e.Row .Cells [9].Text =PenTotal .ToString ();

            TableRow tableRow = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text =  TotalAmount.ToString() ;
            cell1.ColumnSpan = 7; // You can change this
            cell1.Visible = false;
            tableRow.Controls.AddAt(tableRow.Controls.Count, cell1);
            e.Row.NamingContainer.Controls.Add(tableRow);

            TableRow tableRowGst = new TableRow();
            TableCell cellGSt = new TableCell();
            cellGSt.Text =  GSTamt.ToString();
            cellGSt.ColumnSpan = 7; // You can change this
            cellGSt.Visible = false;
            tableRowGst.Controls.AddAt(tableRowGst.Controls.Count, cellGSt);
            e.Row.NamingContainer.Controls.Add(tableRowGst);

            TableRow tableRowGTtl = new TableRow();
            TableCell cellGTtl = new TableCell();
            cellGTtl.Text = GTotal.ToString();
            cellGTtl.ColumnSpan = 7; // You can change this
            cellGTtl.Visible = false;
            tableRowGTtl.Controls.AddAt(tableRowGTtl.Controls.Count, cellGTtl);
            e.Row.NamingContainer.Controls.Add(tableRowGTtl);

            TableRow tableRowPenTtl = new TableRow();
            TableCell cellPenTtl = new TableCell();
            cellPenTtl.Text = PenTotal.ToString();
            cellPenTtl.ColumnSpan = 7; // You can change this
            cellPenTtl.Visible = false;
            tableRowPenTtl.Controls.AddAt(tableRowPenTtl.Controls.Count, cellPenTtl);
            e.Row.NamingContainer.Controls.Add(tableRowPenTtl);

            TableRow tableRowPenGst = new TableRow();
            TableCell cellPenGst = new TableCell();
            cellPenGst.Text = GSTPenAmt.ToString();
            cellPenGst.ColumnSpan = 7; // You can change this
            cellPenGst.Visible = false;
            tableRowPenGst.Controls.AddAt(tableRowPenGst.Controls.Count, cellPenGst);
            e.Row.NamingContainer.Controls.Add(tableRowPenGst);

            TableRow tableRowPenGTtl = new TableRow();
            TableCell cellPenGTtl = new TableCell();
            cellPenGTtl.Text = GstGttl.ToString();
            cellPenGTtl.ColumnSpan = 7; // You can change this
            cellPenGTtl.Visible = false;
            tableRowPenGTtl.Controls.AddAt(tableRowPenGTtl.Controls.Count, cellPenGTtl);
            e.Row.NamingContainer.Controls.Add(tableRowPenGTtl);

            TableRow tblRowBlockTtl = new TableRow();
            TableCell cellBlockTtl = new TableCell();
            cellBlockTtl.Text = BlockedTotal.ToString();
            cellBlockTtl.ColumnSpan = 7; // You can change this
            cellBlockTtl.Visible = false;
            tblRowBlockTtl.Controls.AddAt(tblRowBlockTtl.Controls.Count, cellBlockTtl);
            e.Row.NamingContainer.Controls.Add(tblRowBlockTtl);

            TableRow tblRowBlockGst = new TableRow();
            TableCell cellBlockGst = new TableCell();
            cellBlockGst.Text = BlcGSt.ToString();
            cellBlockGst.ColumnSpan = 7; // You can change this
            cellBlockGst.Visible = false;
            tblRowBlockGst.Controls.AddAt(tblRowBlockGst.Controls.Count, cellBlockGst);
            e.Row.NamingContainer.Controls.Add(tblRowBlockGst);

            TableRow tblRowBlockGTtl = new TableRow();
            TableCell cellBlockGTtl = new TableCell();
            cellBlockGTtl.Text = BlcGTtl.ToString();
            cellBlockGTtl.ColumnSpan = 7; // You can change this
            cellBlockGTtl.Visible = false;
            tblRowBlockGTtl.Controls.AddAt(tblRowBlockGTtl.Controls.Count, cellBlockGTtl);
            e.Row.NamingContainer.Controls.Add(tblRowBlockGTtl);

            TableRow tblRowDelTtl = new TableRow();
            TableCell cellDelTtl = new TableCell();
            cellDelTtl.Text = DelTtl.ToString();
            cellDelTtl.ColumnSpan = 7; // You can change this
            cellDelTtl.Visible = false;
            tblRowDelTtl.Controls.AddAt(tblRowDelTtl.Controls.Count, cellDelTtl);
            e.Row.NamingContainer.Controls.Add(tblRowDelTtl);

            TableRow tblRowDelGst = new TableRow();
            TableCell cellDelGst = new TableCell();
            cellDelGst.Text = DelGST.ToString();
            cellDelGst.ColumnSpan = 7; // You can change this
            cellDelGst.Visible = false;
            tblRowDelGst.Controls.AddAt(tblRowDelGst.Controls.Count, cellDelGst);
            e.Row.NamingContainer.Controls.Add(tblRowDelGst);

            TableRow tblRowDelGTtl = new TableRow();
            TableCell cellDelGTtl = new TableCell();
            cellDelGTtl.Text = DelGTtl.ToString();
            cellDelGTtl.ColumnSpan = 7; // You can change this
            cellDelGTtl.Visible = false;
            tblRowDelGTtl.Controls.AddAt(tblRowDelGTtl.Controls.Count, cellDelGTtl);
            e.Row.NamingContainer.Controls.Add(tblRowDelGTtl);

            e.Row.Cells[3].Text = "Total :"+"<br>"+"GST :" +"<br>"+"G Total :";
            e.Row.Cells[4].Text = cell1.Text + "<br>" + cellGSt.Text+"<br>"+cellGTtl .Text ;

            e.Row.Cells[9].Text = "Total :" + "<br>" + "GST :" + "<br>" + "G Total :";
            e.Row.Cells[10].Text = cellPenTtl.Text + "<br>" + cellPenGst.Text+"<br>"+cellPenGTtl.Text ;

            e.Row.Cells[11].Text = "Total :" + "<br>" + "GST :" + "<br>" + "G Total :";
            e.Row.Cells[12].Text = cellBlockTtl.Text + "<br>" + cellBlockGst.Text + "<br>" + cellBlockGTtl.Text ;

            e.Row.Cells[7].Text = "Total :" + "<br>" + "GST :" + "<br>" + "G Total :";
            e.Row.Cells[8].Text = cellDelTtl.Text + "<br>" + cellDelGst.Text + "<br>" + cellDelGTtl.Text;
        }
    }
    protected void gvDCStat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gridView1 = e.Row.FindControl("gvDCStat") as GridView;
        }
    }
    private void PopulateProductsGrid(GridView gv, string SOId, string ItemCode)
    {
        string constring = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("select SI_DET_RATE from YANTRA_SALES_INVOICE_MAST inner join YANTRA_SALES_INVOICE_DET on YANTRA_SALES_INVOICE_MAST .SI_ID =YANTRA_SALES_INVOICE_DET .SI_ID inner join YANTRA_SO_MAST on YANTRA_SALES_INVOICE_MAST .SO_ID =YANTRA_SO_MAST .SO_ID"
                + " where YANTRA_SO_MAST .SO_ID ='" + SOId + "' and YANTRA_SALES_INVOICE_DET.ITEM_CODE ='" + ItemCode + "'"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Parameters.AddWithValue("@SOID", SOId);
                    cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
                    
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //DataSet ds = new DataSet();
                    //sda.Fill(ds);
                    //gv.DataSource = ds;
                    //gv.DataBind();

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gv.DataSource = dt;
                    gv.DataBind();

                }
            }
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
                    BindCashDCs();
                    //BindReturnGrid();
                    //gvReturnItems.AllowPaging = false;
                    //gvterms.AllowPaging = false;
                    //gvterms.DataBind();
                    //gvitemsgrid.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvStat.HeaderRow.Cells)
                    {
                        cell.BackColor = gvStat.HeaderStyle.BackColor;
                        cell.BackColor = gvStat.HeaderStyle.BackColor;
                        cell.Wrap = true;
                    }
                    foreach (TableCell cell in gvStat.FooterRow.Cells)
                    {
                        cell.BackColor = gvStat.HeaderStyle.BackColor;
                        cell.BackColor = gvStat.HeaderStyle.BackColor;
                        cell.Wrap = true;
                    }
                    foreach (GridViewRow row in gvStat.Rows)
                    {
                        //row.BackColor = Color.White;
                        
                        row.Cells [0].HorizontalAlign = HorizontalAlign.Left ;
                        row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                        row.Cells[2].HorizontalAlign = HorizontalAlign.Center ;
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
                        gvCashDcs.Style["font-family"] = "Book Antiqua, Helvetica, sans-serif;";
                    }

                    gvStat.RenderControl(hw);
                    gvCashDcs.RenderControl(hw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Statement.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    
                    //style to format numbers to string
                    //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    //Response.Write(style);
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
            //lblPOGST.Text = obj.SOSalespId;
            lblpono.Text = obj.SOCustPONo;
            lblpostatus.Text = obj.SOAcceptanceFlag;
        }
    }

    //protected void gvSI_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
       
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //e.Row.Cells[4].Text = (Convert.ToInt32(e.Row.Cells[2].Text) * Convert.ToDecimal(e.Row.Cells[3].Text)).ToString();
            
    //    }
       
    //    if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Cells[0].Visible = false;
    //        e.Row.Cells[1].Visible = false;
    //        e.Row.Cells[2].Visible = false;
    //        e.Row.Cells[3].Visible = false;

    //    }
    //}
    protected void gvDC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvSIChild = e.Row.FindControl("gvSIChild") as GridView;

            if (e.Row.RowIndex == 0)
            {
                gvSIChild.ShowHeader = false;
                
            }
            else
            {
                gvSIChild.ShowHeader = false;
            }

            GridView gvReturn = e.Row.FindControl("gvReturn") as GridView;

            if (e.Row.RowIndex == 0)
            {
                gvReturn.ShowHeader = false;

            }
            else
            {
                gvReturn.ShowHeader = false;
            }

        }
    }
    protected void gvSIChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = (Convert.ToInt32(e.Row.Cells[1].Text) * Convert.ToDecimal(e.Row.Cells[2].Text)).ToString();

        }
         if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
         {
             e.Row.Cells[0].Visible = false;
             e.Row.Cells[1].Visible = false;
             e.Row.Cells[2].Visible = false;
             //e.Row.Cells[3].Visible = false;

         }
    }
    protected void gvReturnItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string msg = "Returned Items" + "<br>" +"";
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = msg;
            HeaderCell.ColumnSpan = 9;

            HeaderGridRow.Cells.Add(HeaderCell);
            //gvReturnItems.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = (Convert.ToInt32(e.Row.Cells[2].Text) * Convert.ToDecimal(e.Row.Cells[3].Text)).ToString();
            ReturnTotal = ReturnTotal + Convert.ToDecimal(e.Row.Cells[4].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableRow tableRow = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = "Total : " + ReturnTotal.ToString();
            cell1.ColumnSpan = 7; // You can change this
            cell1.Visible = false;
            tableRow.Controls.AddAt(tableRow.Controls.Count, cell1);
            e.Row.NamingContainer.Controls.Add(tableRow);
            e.Row.Cells[4].Text = cell1.Text;
        }
    }
    protected void gvReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvCashDcs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string msg = ""+"<br>"+"Sample and Cash DC/Inv Details for Mr. " + ddlCustomerName.SelectedItem.Text+"<br>"+"" ;
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = msg;
            HeaderCell.ColumnSpan = 9;

            HeaderGridRow.Cells.Add(HeaderCell);

            gvCashDcs.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void ddlNoOfRecord4_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDCStat.PageSize = Convert.ToInt32(ddlNoOfRecord4.SelectedValue);
        gvDCStat.DataBind();
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
    protected void gvSubInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "All Invoiced";
                e.Row.ForeColor = System.Drawing.Color.Red;


            }
            else if (e.Row.Cells[2].Text != e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "Partially Invoiced";
                e.Row.ForeColor = System.Drawing.Color.ForestGreen;

            }
            else
            {
                e.Row.Cells[4].Text = "No Invoices";
            }
        }
    }
    protected void gvDCSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "All Returned";
                e.Row.ForeColor = System.Drawing.Color.Red;


            }
            else if (e.Row.Cells[2].Text != e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "Partially Returned";
                e.Row.ForeColor = System.Drawing.Color.ForestGreen;

            }
            else
            {
                e.Row.Cells[4].Text = "No Returns";
            }
        }
    }
    private void BindChileDCSalesGV()
    {
        foreach (GridViewRow gvrow in gvDCStat .Rows)
        {
            GridView gvDC = (GridView)(gvDCStat.Rows[gvrow.RowIndex].Cells[3].FindControl("gvDCSales"));
            SqlCommand cmd = new SqlCommand("[USP_DC_SRStatementAll]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[0].Text != "")
            {
                cmd.Parameters.AddWithValue("@DCID", gvrow.Cells[3].Text);
            }
            //if (gvrow.Cells[6].Text != "")
            //{
            //    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();


        }
    }
    private void BindChileDCInvoiceGV()
    {
        foreach (GridViewRow gvrow in gvDCStat.Rows)
        {
            GridView gvDC = (GridView)(gvDCStat.Rows[gvrow.RowIndex].Cells[3].FindControl("gvSubInvoice"));
            SqlCommand cmd = new SqlCommand("[USP_DC_SIStatementAll]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[0].Text != "")
            {
                cmd.Parameters.AddWithValue("@DCID", gvrow.Cells[3].Text);
            }
            //if (gvrow.Cells[6].Text != "")
            //{
            //    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();
        }
    }


    
}
 
