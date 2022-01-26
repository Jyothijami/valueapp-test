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

public partial class Modules_Inventory_Reserved_Stock : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindReservedStockGrid();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            CustomerMaster_Fill();
            lblCPID.Text = cp.getPresentCompanySessionValue();

        }
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
        SqlCommand cmd = new SqlCommand("USP_Blocked_Items_Serach", con);
        cmd.CommandType = CommandType.StoredProcedure;
        
        if (txtSearchText.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtSearchText.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvReservedStock.DataSource = dt;
        gvReservedStock.DataBind();
    }
    //private void BindManualBlockGv()
    //{
    //    SqlCommand cmd = new SqlCommand("USP_Blocked_Items_Serach", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    if (txtSearchText.Text != "")
    //    {
    //        cmd.Parameters.AddWithValue("@ModelNo", txtSearchText.Text);
    //    }
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    gvBlockItem.DataSource = dt;
    //    gvBlockItem.DataBind();
    //}
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(pnlHistory.Visible == true)
        {
            gvReservedStock.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
            gvReservedStock.DataBind();
        }
        else
        {
            pnlHistory.Visible = true;
            gvReservedStock.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
            gvReservedStock.DataBind();
        }
        
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        if(pnlBlockItem.Visible == true)
        {
            //BindManualBlockGv();

        }
        else if (pnlHistory.Visible == true)
        {
            BindReservedStockGrid();


        }
    }
    protected void btnBlockHistory_Click(object sender, EventArgs e)
    {
        pnlBlockItem.Visible = false;
        pnlHistory.Visible = true;        
        BindReservedStockGrid();
        
    }
    protected void btnItemBlock_Click(object sender, EventArgs e)
    {
        pnlHistory.Visible = false;
        pnlBlockItem.Visible = true;
        //BindManualBlockGv();        
    }
    protected void btnReleaseBlock_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {
                    Label blockStock = (Label)gvrow.FindControl("lblQty");
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int b = Convert.ToInt32(blockStock.Text);
                    int q = Convert.ToInt32(qty.Text);

                    if (b >= q)
                    {
                        SqlCommand cmd = new SqlCommand("USP_ReleaseBlock", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@qty", q);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        MessageBox.Show(this, "This Operation Cannot Be Performed.");
                    }

                }

            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);            
        }
        finally
        {
            BindReservedStockGrid();
            btnReleaseBlock.Visible = false;
        }

    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelNo, ddlBrand.SelectedItem.Value);
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
        BindCompany();
        BindAvailableStock();
    }

    private void BindAvailableStock()
    {        
        string itemcode = ddlModelNo.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("SELECT((select COUNT(*) from inward where item_code=p.item_code )-((select COUNT(*) from outward where item_code=p.item_code)+(select COUNT(*)from BLOCK where item_code=p.item_code))) as TOTAL_AVAILABLE_STOCK,(select COUNT(*)from BLOCK where item_code=p.item_code) as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + itemcode + " group by p.item_code", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count>0)
        {
            lblAvailableStock.Text = dt.Rows[0][0].ToString();
            lblBlockedStock.Text = dt.Rows[0][1].ToString();
        }
        else
        {
            lblAvailableStock.Text = lblBlockedStock.Text = "0";
        }
        
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCompany.SelectedIndex > 0)
        {
            BindWHCompStock();
        }
        else
        {
            BindAvailableStock();
        }
    }
    private void BindCompany()
    {
        SqlCommand cmd = new SqlCommand("Usp_BindCompany", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@itemcode", ddlModelNo.SelectedValue);       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlCompany.DataTextField = ds.Tables[0].Columns["COMP_NAME"].ToString();
        ddlCompany.DataValueField = ds.Tables[0].Columns["CP_ID"].ToString();
        ddlCompany.DataSource = ds.Tables[0];
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
        {
            txtUnitAddress.Text = objSMCustomer.Address;
        }
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        if(lblAvailableStock.Text != "")
        {
            if(Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(lblAvailableStock.Text))
            {
                txtQuantity.Text = "";
                MessageBox.Show(this, "Please select Quantity Less or Equal to Available Stock");
            }
            else
            {
                btnBlock.Visible = true;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Provide Proper Data To Reserve Items");
        }
    }
    protected void btnBlock_Click(object sender, EventArgs e)
    {
        string Itemcode = ddlModelNo.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("select  Item_ID,whLocId from dbo.INWARD where ITEM_CODE=" + Itemcode + " and Item_ID not in(select Item_ID from dbo.BlOCK where ITEM_CODE=" + Itemcode + ")", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        try
        {         
            Masters.ItemPurchase obj = new Masters.ItemPurchase();
            int qty = Convert.ToInt32(txtQuantity.Text);
            for (int i = 0; i < qty; i++)
            {
                obj.itemcode = ddlModelNo.SelectedItem.Value;
                obj.ItemID = dt.Rows[i][0].ToString();
                obj.whLocId = dt.Rows[i][1].ToString();
                obj.Barcode = dt.Rows[i][0].ToString();

                obj.companyid = lblCPID.Text;
                obj.POID = ddlCustomerName.SelectedItem.Value;
                obj.COLORID = ddlColor.SelectedItem.Value;
                obj.status = txtComment.Text;
                obj.Block_Save2();
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            pnlHistory.Visible = true;
            pnlBlockItem.Visible = false;
            btnBlock.Visible = false;
            ClearFields();
            BindReservedStockGrid();
        }
    }

    private void ClearFields()
    {
        ddlCustomerName.SelectedIndex = ddlBrand.SelectedIndex = ddlModelNo.SelectedIndex = ddlCompany.SelectedIndex = ddlWarehouseLocation.SelectedIndex = ddlColor.SelectedIndex = 0;
        txtComment.Text = txtQuantity.Text = txtUnitAddress.Text = "";
        btnBlock.Visible = false;
        lblAvailableStock.Text = lblBlockedStock.Text = "";

    }
    protected void ddlWarehouseLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlWarehouseLocation.SelectedItem.Value) != 0)
        //{
        //    BindWHStock();
        //}
        //else
        //{
        //    BindAvailableStock();
        //}
    }

    private void BindWHCompStock()
    {
        
        string itemcode = ddlModelNo.SelectedItem.Value;
        string Cp_Id = ddlCompany.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("SELECT((select COUNT(*) from inward where item_code=p.item_code and Cp_Id= " + Cp_Id + ")-((select COUNT(*) from outward where item_code=p.item_code and Cp_Id=" + Cp_Id + ")+(select COUNT(*)from BLOCK where item_code=p.item_code and Cp_Id=" + Cp_Id + "))) as TOTAL_AVAILABLE_STOCK,(select COUNT(*)from BLOCK where item_code=p.item_code and Cp_Id=" + Cp_Id + ") as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + itemcode + " group by p.item_code", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count>0)
        {
            lblAvailableStock.Text = dt.Rows[0][0].ToString();
            lblBlockedStock.Text = dt.Rows[0][1].ToString();
        }
        else
        {
            lblAvailableStock.Text = lblBlockedStock.Text = "0";
        }
        
    }
    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(Convert.ToInt32(ddlColor.SelectedItem.Value) !=0)
        {
            BindStockWithColor();
        }
        else if(Convert.ToInt32(ddlColor.SelectedItem.Value) ==0 && Convert.ToInt32(ddlCompany.SelectedItem.Value) != 0 )
        {
            BindWHCompStock();
        }
        else
        {
            BindAvailableStock();
        }
    }

    private void BindStockWithColor()
    {
        
        string itemcode = ddlModelNo.SelectedItem.Value;
        //string whLocId = ddlWarehouseLocation.SelectedItem.Value;
        string Cp_Id = ddlCompany.SelectedItem.Value;
        string colorid = ddlColor.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("SELECT((select COUNT(*) from inward where item_code=p.item_code and Cp_Id= " + Cp_Id + " and COLOUR_ID=" + colorid + ")-((select COUNT(*) from outward where item_code=p.item_code and Cp_Id=" + Cp_Id + " and COLOUR_ID=" + colorid + ")+(select COUNT(*)from BLOCK where item_code=p.item_code and Cp_Id=" + Cp_Id + " and COLOUR_ID=" + colorid + "))) as TOTAL_AVAILABLE_STOCK,(select COUNT(*)from BLOCK where item_code=p.item_code and Cp_Id=" + Cp_Id + " and COLOUR_ID=" + colorid + ") as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + itemcode + " group by p.item_code", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count>0)
        {
            lblAvailableStock.Text = dt.Rows[0][0].ToString();
            lblBlockedStock.Text = dt.Rows[0][1].ToString();
        }
        else
        {
            lblAvailableStock.Text = lblBlockedStock.Text = "0";
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvReservedStock.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if(ch.Checked == true)
            {
                btnReleaseBlock.Visible = true;
            }            
        }
        
    }
    
}
 
