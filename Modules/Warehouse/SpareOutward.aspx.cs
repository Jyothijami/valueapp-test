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
using vllib;
using System.Data.SqlClient;
using Yantra.Classes;
using YantraBLL.Modules;
using System.Collections.Generic;
using Yantra.MessageBox;

public partial class Modules_Warehouse_SpareOutward : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        { 
            BindReservedStockGrid();
            CustomerMaster_Fill();
            gvSpares.Columns[8].Visible = false;
            setControlsVisibility();
            txtDCNo.Text = Masters.ItemPurchase.SpareDC_AutoGenCode();

        }   
        
    }

    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "95");
        //btnSaveWarehouse.Enabled = up.add;

    }
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomerName);
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
    private void BindReservedStockGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_BindInwardSpares]", con);
        cmd.CommandType = CommandType.StoredProcedure;

        //if (txtSearchText.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@ModelNo", txtSearchText.Text);
        //}
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSpares.DataSource = dt;
        gvSpares.DataBind();
    }
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
        {
            txtUnitAddress.Text = objSMCustomer.Address;
        }
    }
    protected void btnSaveWarehouse_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvSpares.Rows)
            {
                //for(int i=0;i<gvSpares.Rows.Count ;i++)
                //{
                    CheckBox ch = new CheckBox();
                    ch = (CheckBox)gvrow.FindControl("chk");

                    if (ch.Checked == true)
                    {
                    
                        Masters.ItemPurchase obj = new Masters.ItemPurchase();
                        obj.ItemID = gvrow.Cells[0].Text;
                        obj.InvoiceNo = gvrow.Cells[1].Text;
                        obj.Barcode = gvrow.Cells[2].Text;
                        obj.ModelNo = gvrow.Cells[2].Text;
                        obj.SpareModelNo = gvrow.Cells[3].Text;
                        obj.subcatid = gvrow.Cells[4].Text;
                        obj.brandid = gvrow.Cells[5].Text;
                        obj.color = gvrow.Cells[6].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQty");
                        obj.Quantity = qty.Text;
                        obj.Remarks = gvrow.Cells[8].Text;
                        obj.whLocId = gvrow.Cells[9].Text;
                        obj.MRN_No = gvrow.Cells[2].Text;
                        obj.Cust_id = ddlCustomerName.SelectedItem.Value;
                        obj.Cust_Name = ddlCustomerName.SelectedItem.Text;
                        obj.CpId = cp.getPresentCompanySessionValue();
                        obj.DcNo = txtDCNo.Text;
                        obj.Spare_Outward_Save();

                        int Quantity =Convert.ToInt32(gvrow.Cells[12].Text)- Convert.ToInt32(qty.Text) ;

                        obj.SpareQty_Update(gvrow.Cells[2].Text, gvrow.Cells[0].Text, Quantity);
                    }
                //}
            }
        } 
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            BindReservedStockGrid();
            CustomerMaster_Fill();
            txtDCNo.Text = Masters.ItemPurchase.SpareDC_AutoGenCode();
            txtUnitAddress.Text = "";
        }
    }
    protected void gvSpares_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow||e.Row.RowType==DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSparesOutwardGrid();
    }
    private void BindSparesOutwardGrid()
    {
        SqlCommand cmd = new SqlCommand("Usp_SpareOutwardDtls", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
        }
        if (txtSpareModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SpareModelNo", txtSpareModelNo.Text);

        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));

        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));

        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@CustName", txtCustName.Text);

        }
        if (txtSpDCNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@DCNo", txtSpDCNo.Text);

        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvSpareDtls.DataSource = dt;
        gvSpareDtls.DataBind();
    }
    protected void lnkPnl1_Click(object sender, EventArgs e)
    {
        pnl1.Visible = true;
        pnl2.Visible = false;

    }
    protected void lnkPnl2_Click(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = true;
    }
}
 
