using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using YantraBLL.Modules;
using Yantra.MessageBox;
using Yantra.Classes;


public partial class Modules_Warehouse_Inward_Management : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string ReferenceType;
    DataTable dt;
    static DataTable dtSpares = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if(!IsPostBack)
        {           
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
            BindGrid();
            ClearRows();
            PrepareGrid();
            AddNewRow();
            setControlsVisibility();
        }

        SaveGridValues();
        

    }

    private void SaveGridValues()
    {

        for (int i = 0; gvSpares.Rows.Count > i; i++)
        {
            for (int j = 1; j < gvSpares.Rows[i].Cells.Count; j++)
            {
                if (dtSpares.Rows.Count - 1 >= i)
                {
                    dtSpares.Rows[i][j] = ((TextBox)gvSpares.Rows[i].Cells[j].Controls[1]).Text;
                }
                else
                {
                    DataRow row = dtSpares.NewRow();
                    dtSpares.Rows.Add(row);
                    dtSpares.Rows[i][j] = ((TextBox)gvSpares.Rows[i].Cells[j].Controls[1]).Text;
                }
            }
        }
    }
    public void ClearRows()
    {
        dtSpares.Rows.Clear();
    }

    public void PrepareGrid()
    {
        dtSpares.PrimaryKey = null;
        dtSpares.Columns.Clear();
        DataColumn col = new DataColumn("SNo", typeof(int)); col.AutoIncrement = true; col.AutoIncrementSeed = 1;
        dtSpares.Columns.Add(col);
        dtSpares.PrimaryKey = new DataColumn[] { dtSpares.Columns["SNo"] };
        //dtDamage.PrimaryKey = col;
        dtSpares.Columns.Add("Invoice No", typeof(string));
        dtSpares.Columns.Add("Item Model No", typeof(string));
        dtSpares.Columns.Add("Spare Model No", typeof(string));
        dtSpares.Columns.Add("Sub Category", typeof(string));
        dtSpares.Columns.Add("Brand", typeof(string));
        dtSpares.Columns.Add("Color", typeof(string));
        dtSpares.Columns.Add("Quantity", typeof(string));
        dtSpares.Columns.Add("Remarks", typeof(string));
    }

    public void AddNewRow()
    {
        DataRow row = dtSpares.NewRow();

        dtSpares.Rows.Add(row);
        gvSpares.DataSource = dtSpares; gvSpares.DataBind();
    }
    private void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_InwardManagement", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlCategory.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Category", ddlCategory.SelectedItem.Text);
        }
        if (ddlSubCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@SubCategory", ddlSubCat.SelectedItem.Text);
        }
        if (ddlModelNo.SelectedIndex != 0 && ddlModelNo.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInwardItems.DataSource = dt;
        gvInwardItems.DataBind();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvInwardItems.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvInwardItems.DataBind();
        //BindGrid();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedItem.Value);
    }
    protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemType.Item_ModelNo_Select(ddlModelNo, ddlSubCat.SelectedItem.Value);
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkMRN_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        ReferenceType = "MRN";
        BindReferenceGrid();
    }
    protected void lnkSalesReturn_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        ReferenceType = "SalesReturn";
        BindReferenceGrid();

    }
    protected void lnkSampleReturn_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        ReferenceType = "SampleReturn";
        BindReferenceGrid();
    }
    private void BindReferenceGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_InwardManagement2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Inward_Type", ReferenceType);        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInwardItems.DataSource = dt;
        gvInwardItems.DataBind();
    }

    protected void lnkAll_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        BindGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        BindGrid();
    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtModelNo.Text != "" || txtLocation.Text != "")
            {

                ModelNoSearch();
              
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }

    private void ModelNoSearch()
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        SqlCommand cmd1 = new SqlCommand("USP_InwardManagementModelNo", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.AddWithValue("@ItemCode", txtModelNo.Text);
        cmd1.Parameters.AddWithValue("@whname", txtLocation.Text);
        //cmd1.Parameters.AddWithValue("@Category", cat);
        //cmd1.Parameters.AddWithValue("@SubCategory",subcat);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        gvInwardItems.DataSource = dt1;
        gvInwardItems.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Inward_Save();
        BindGrid();
    }
     

    private void Inward_Save()
    {
        if (TextBox2_value.Value != "")
        {
            try
            {
                btnSave.Enabled = false;
                foreach (GridViewRow gvrow in gvInwardItems.Rows)
                {
                    CheckBox ch = new CheckBox();
                    ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                    if (ch.Checked == true)
                    {
                        TextBox qty = (TextBox)gvrow.FindControl("txtReceivedQty");
                        Masters.ItemPurchase objsales = new Masters.ItemPurchase();
                        int hai = int.Parse(qty.Text);
                        int custId = 0;
                        if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;")
                        {
                            custId = int.Parse(gvrow.Cells[14].Text);
                        }
                        for (int i = 0; i < hai; i++)
                        {
                            objsales.itemcode = gvrow.Cells[2].Text;

                            objsales.ItemID = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                            objsales.companyid = cp.getPresentCompanySessionValue();
                            objsales.Barcode = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                            objsales.MRNID = gvrow.Cells[1].Text;
                            objsales.COLORID = gvrow.Cells[9].Text;
                            objsales.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                            objsales.ItemInward_Save();

                            if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;" && gvrow.Cells[14].Text != "0")
                            {
                                objsales.itemcode = gvrow.Cells[2].Text;
                                //objsales.ItemID = dt.Rows[i][0].ToString();
                                objsales.whLocId = WH_Locations.getLocationID(TextBox2_value.Value);
                                // objsales.Barcode = dt.Rows[i][0].ToString();
                                // objsales.companyid = lblCPID.Text;
                                objsales.POID = gvrow.Cells[1].Text;
                                objsales.COLORID = gvrow.Cells[9].Text;
                                objsales.status = "Blocked";
                                objsales.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[15].Text);
                                //objsales.DeliveryDate = gvrow.Cells[15].Text;
                                objsales.CustomerId = custId.ToString();
                                //obj.CustomerId="0";

                                objsales.Block_Save2();
                            }

                        }
                    }
                }
                RecQty_Update();
                DamageQty_Update();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnSave.Enabled = true;
            }

        }
        else
        {
            MessageBox.Show(this, "Please Select a Warehouse Location");
        }

    }
    private void RecQty_Update()
    {
        foreach (GridViewRow gvrow in gvInwardItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("CheckBox_row");
            if (ch.Checked == true)
            {   
                    TextBox ordQty = (TextBox)gvrow.FindControl("txtReceivedQty");
                int ttlQty = Convert.ToInt32(gvrow.Cells[10].Text);
                string qty = (ttlQty - Convert.ToInt32(ordQty.Text)).ToString();
                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                obj.RecItemQty_Update(qty, gvrow.Cells[0].Text);
            }
        }
    }

    private void DamageQty_Update()
    {
        foreach (GridViewRow gvrow in gvInwardItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("CheckBox_row");
            if (ch.Checked == true)
            {
                TextBox dmgQty = (TextBox)gvrow.FindControl("txtDamageQty");    
                int ttlQty = Convert.ToInt32(gvrow.Cells[11].Text);
                string qty = (ttlQty - Convert.ToInt32(dmgQty.Text)).ToString();
                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                obj.DamageItemQty_Update(qty, gvrow.Cells[0].Text);
            }
        }
    }

    protected void gvInwardItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;

        }
        
    }
    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        AddNewRow();

    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)gvSpares.HeaderRow.FindControl("chkAll");
        if (chkAll.Checked)
        {
            dtSpares.Rows.Clear();
        }
        else
        {
            for (int i = 0; i < gvSpares.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvSpares.Rows[i].FindControl("chk");
                if (chk.Checked)
                {                                                                   
                    dtSpares.Rows.Find(chk.Text).Delete();
                }
            }
        }
        if (dtSpares.Rows.Count == 0)
        {
            AddNewRow();
        }
        gvSpares.DataSource = dtSpares; gvSpares.DataBind();
    }

    protected void setControlsVisibility()
    {
        //User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "94");
        //btnAddNewRow.Enabled = up.add;
        //btnDeleteRow.Enabled = up.Delete;
    }
    public void JavaScriptAlert(string val)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "Popup", "<script>alert('" + val + "');</script>");
    }
    protected void btnSaveWH_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvSpares.Rows)
        {
            CheckBox ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                TextBox InvoiceNo = (TextBox)gvrow.FindControl("txtInvoiceNo");
                TextBox ModelNo = (TextBox)gvrow.FindControl("txtItemModelNo");
                TextBox SpareNo = (TextBox)gvrow.FindControl("txtSpareModelNo");
                TextBox SubCat = (TextBox)gvrow.FindControl("txtSubCategory");
                TextBox Brand = (TextBox)gvrow.FindControl("txtBrand");
                TextBox Color = (TextBox)gvrow.FindControl("txtColor");
                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                TextBox Remarks = (TextBox)gvrow.FindControl("txtRemarks");

                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                int quantity = Convert.ToInt32(qty.Text);
                for (int i = 0; i < quantity; i++)
                {
                    obj.ItemID = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
                    obj.InvoiceNo = InvoiceNo.Text;
                    obj.Barcode = ModelNo.Text;
                    obj.ModelNo = ModelNo.Text;
                    obj.SpareModelNo = SpareNo.Text;
                    obj.subcatid = SubCat.Text;
                    obj.brandid = Brand.Text;
                    obj.color = Color.Text;
                    obj.Quantity = "1";
                    obj.Remarks = Remarks.Text;
                    //obj.whLocId = ddlLocation.SelectedItem.Value;
                    obj.whLocId = "146";
                                        
                    obj.MRN_No = ModelNo.Text;
                    obj.Spare_Inward_Save();

                }
            }
        }

        ClearRows();
        PrepareGrid();
        AddNewRow();
    }
    protected void lnkStockMove_Click(object sender, EventArgs e)
    {
        LoadMovingStock();       
    }

    private void LoadMovingStock()
    {
        gvInwardItems.Visible = false;
        gvMovingItems.Visible = true;
        btnSave.Visible = false;
        btnSavems.Visible = true;
        SqlCommand cmd = new SqlCommand("Usp_StockMovement", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvMovingItems.DataSource = dt;
        gvMovingItems.DataBind();
    }
    protected void gvMovingItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;

        }
    }
    protected void btnSavems_Click(object sender, EventArgs e)
    {
        SaveMovingStock();        
        UpdateItemQty();
        LoadMovingStock();
    }

    private void UpdateItemQty()
    {
        foreach (GridViewRow gvrow in gvMovingItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("ChkBox_row");
            if (ch.Checked == true)
            {
                TextBox mvQty = (TextBox)gvrow.FindControl("txtQuantity");
                int ttlQty = Convert.ToInt32(gvrow.Cells[9].Text);
                string qty = (ttlQty - Convert.ToInt32(mvQty.Text)).ToString();
                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                obj.MovingItemQty_Update(qty, gvrow.Cells[0].Text);
            }
        }
    }

    private void SaveMovingStock()
    {
        foreach (GridViewRow gvrow in gvMovingItems.Rows)
        {
            CheckBox ch = (CheckBox)gvrow.FindControl("ChkBox_row");
            if (ch.Checked == true)
            {
                string Itemcode = gvrow.Cells[1].Text;

                SqlCommand cmd = new SqlCommand("select Item_ID,whLocId from V_StockInward where [ITEM_CODE]=" + Itemcode + " and [wh_id]=" + gvrow.Cells[11].Text + " ", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Masters.ItemPurchase objout = new Masters.ItemPurchase();
                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                int Quantity = int.Parse(qty.Text);
               // int locId = int.Parse(gvrow.Cells[11].Text);
                int whLocId =Convert.ToInt32(WH_Locations.getLocationID(TextBox2_value.Value)); 

                // int whLocId = int.Parse(ddlMovingto.SelectedItem.Value);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Quantity; i++)
                    {
                        int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                        string itemId = dt.Rows[i][0].ToString();
                        objout.InwardLoc_Update(locId, itemId, whLocId);
                    }
                }
            }
        }

    }

    protected void txtModelNo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvSpares_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void lbtnPrint_Click(object sender, EventArgs e)
    //{
    //    LinkButton lbtnPrint;
    //    lbtnPrint = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lbtnPrint.Parent.Parent;
    //    gvMovingItems.SelectedIndex = gvRow.RowIndex;

    //    string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=MovingDc&dcid=" + gvMovingItems.SelectedRow.Cells[13].Text + "";
    //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);


    //}
    protected void gvInwardItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInwardItems.PageIndex = e.NewPageIndex;
        if (txtModelNo.Text != "" || txtLocation.Text != "")
        {

            ModelNoSearch();

        }
        else
        {
            BindGrid();

        }
    }
}
 
