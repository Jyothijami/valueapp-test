using QRCoder;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Warehouse_Inward_Management_New : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string ReferenceType;
    DataTable dt;
    static DataTable dtSpares = new DataTable();
    public string constr;

    public string status;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lblUserName.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
            lblUserId.Text = usre.getUserID(lblUserName.Text);
            DataTable dt = Yantra.Authentication.Execute_Command("SELECT [CP_ID]  FROM [user_Company_Access_tbl] where permission=1 and USER_ID='" + lblUserId.Text + "' order by [CP_ID]  ", "Select");
            lblCp_Ids.Text = Yantra.Authentication.GetCompIds(dt);

            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblCPID.Text = cp.getPresentCompanySessionValue();

            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
            BrandFill();
            BindGrid();
            ClearRows();
            PrepareGrid();
            AddNewRow();
            setControlsVisibility();
            AddDefaultFirstRecord();
            AddDefaultReserveItems();

            btnDelete.Visible = true;
            btnDeleteMoved.Visible = false;
        }

        //SaveGridValues();
    }
    private void BrandFill()
    {
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
    }
    public void connection()
    {
        //Stoting connection string   
        constr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        con = new SqlConnection(constr);
        con.Open();

    }
    private void AddDefaultFirstRecord()
    {
        //creating DataTable  
        DataTable dt = new DataTable();
        DataRow dr;
        dt.TableName = "ProductsSold";
        //creating columns for DataTable  
        dt.Columns.Add(new DataColumn("Item_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Barcode", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEM_CODE", typeof(int)));
        dt.Columns.Add(new DataColumn("Cp_Id", typeof(int)));
        dt.Columns.Add(new DataColumn("whLocId", typeof(int)));
        dt.Columns.Add(new DataColumn("dt_added", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("COLOUR_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("MRN_NO", typeof(string)));
        dr = dt.NewRow();
        dt.Rows.Add(dr);

        ViewState["ProductsSold"] = dt;
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }

    private void AddDefaultReserveItems()
    {
        //creating DataTable  
        DataTable dt1 = new DataTable();
        DataRow dr;
        dt1.TableName = "ReservedItems";
        //creating columns for DataTable  
        dt1.Columns.Add(new DataColumn("Item_ID", typeof(string)));
        dt1.Columns.Add(new DataColumn("Barcode", typeof(string)));
        dt1.Columns.Add(new DataColumn("ITEM_CODE", typeof(int)));
        dt1.Columns.Add(new DataColumn("Cp_Id", typeof(int)));
        dt1.Columns.Add(new DataColumn("whLocId", typeof(int)));
        dt1.Columns.Add(new DataColumn("dt_added", typeof(DateTime)));
        dt1.Columns.Add(new DataColumn("COLOUR_ID", typeof(int)));
        dt1.Columns.Add(new DataColumn("SO_Id", typeof(string)));
        dt1.Columns.Add(new DataColumn("Status", typeof(string)));
        dt1.Columns.Add(new DataColumn("Customer_Id", typeof(int)));
        dt1.Columns.Add(new DataColumn("Delivery_Date", typeof(DateTime)));
        dr = dt1.NewRow();
        dt1.Rows.Add(dr);

        ViewState["ReservedItems"] = dt1;
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
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

        cmd.Parameters.AddWithValue("@UserType", lblUserType.Text);
        cmd.Parameters.AddWithValue("@CPID", lblCp_Ids.Text);

        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        }
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
        btnDelete.Visible = true;
        btnDeleteMoved.Visible = false;
    }
    protected void lnkSalesReturn_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        ReferenceType = "SalesReturn";
        BindReferenceGrid();

        btnDelete.Visible = true;
        btnDeleteMoved.Visible = false;

    }
    protected void lnkSampleReturn_Click(object sender, EventArgs e)
    {
        gvInwardItems.Visible = true;
        gvMovingItems.Visible = false;
        btnSave.Visible = true;
        btnSavems.Visible = false;
        ReferenceType = "SampleReturn";
        BindReferenceGrid();

        btnDelete.Visible = true;
        btnDeleteMoved.Visible = false;
    }
    private void BindReferenceGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_InwardManagement2", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Inward_Type", ReferenceType);
        cmd.Parameters.AddWithValue("@UserType", lblUserType.Text);
        cmd.Parameters.AddWithValue("@CPID", lblCp_Ids.Text);

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

        btnDelete.Visible = true;
        btnDeleteMoved.Visible = false;

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

        cmd1.Parameters.AddWithValue("@UserType", lblUserType.Text);
        cmd1.Parameters.AddWithValue("@CPID", lblCp_Ids.Text);

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

    private void Inward_Save_Test()
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
                        int quantity = int.Parse(qty.Text);
                        int custId = 0;
                        if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;")
                        {
                            custId = int.Parse(gvrow.Cells[14].Text);
                        }
                        for (int i = 0; i < quantity; i++)
                        {
                            string code = "";

                            #region Loading Individually into another Grid

                            if (ViewState["ProductsSold"] != null)
                            {
                                DataTable dtCurrentTable = (DataTable)ViewState["ProductsSold"];
                                DataRow drCurrentRow = null;

                                if (dtCurrentTable.Rows.Count > 0)
                                {

                                    for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
                                    {

                                        //Creating new row and assigning values  
                                        drCurrentRow = dtCurrentTable.NewRow();
                                        code = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                        drCurrentRow["Item_ID"] = code;
                                        drCurrentRow["Barcode"] = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                        drCurrentRow["ITEM_CODE"] = gvrow.Cells[2].Text;
                                        drCurrentRow["Cp_Id"] = cp.getPresentCompanySessionValue();
                                        drCurrentRow["whLocId"] = WH_Locations.getLocationID(TextBox2_value.Value);
                                        drCurrentRow["dt_added"] = DateTime.Now.ToString();
                                        drCurrentRow["COLOUR_ID"] = gvrow.Cells[9].Text;
                                        drCurrentRow["MRN_NO"] = gvrow.Cells[1].Text;

                                    }
                                    //Removing initial blank row  
                                    if (dtCurrentTable.Rows[0][0].ToString() == "")
                                    {
                                        dtCurrentTable.Rows[0].Delete();
                                        dtCurrentTable.AcceptChanges();

                                    }

                                    //Added New Record to the DataTable  
                                    dtCurrentTable.Rows.Add(drCurrentRow);
                                    //storing DataTable to ViewState  
                                    ViewState["ProductsSold"] = dtCurrentTable;
                                    //binding Gridview with New Row  
                                    //GridView1.DataSource = dtCurrentTable;
                                    //GridView1.DataBind();
                                }
                            }
                            #endregion

                            #region Loading Reserved Items Individually into another Grid

                            if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;" && gvrow.Cells[14].Text != "0")
                            {
                                if (ViewState["ReservedItems"] != null)
                                {
                                    DataTable dtReserveItems = (DataTable)ViewState["ReservedItems"];
                                    DataRow drCurrentRow = null;

                                    if (dtReserveItems.Rows.Count > 0)
                                    {

                                        for (int j = 1; j <= dtReserveItems.Rows.Count; j++)
                                        {

                                            //Creating new row and assigning values  
                                            drCurrentRow = dtReserveItems.NewRow();

                                            //  drCurrentRow["Item_ID"] = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                            drCurrentRow["Item_ID"] = code;
                                            drCurrentRow["Barcode"] = "I" + i + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                            drCurrentRow["ITEM_CODE"] = gvrow.Cells[2].Text;
                                            drCurrentRow["Cp_Id"] = cp.getPresentCompanySessionValue();
                                            drCurrentRow["whLocId"] = WH_Locations.getLocationID(TextBox2_value.Value);
                                            drCurrentRow["dt_added"] = DateTime.Now.ToString();
                                            drCurrentRow["COLOUR_ID"] = gvrow.Cells[9].Text;
                                            drCurrentRow["Delivery_Date"] = gvrow.Cells[15].Text;
                                            drCurrentRow["Status"] = "Blocked";
                                            drCurrentRow["Customer_Id"] = custId.ToString();
                                            drCurrentRow["SO_Id"] = gvrow.Cells[1].Text;

                                        }
                                        //Removing initial blank row  
                                        if (dtReserveItems.Rows[0][0].ToString() == "")
                                        {
                                            dtReserveItems.Rows[0].Delete();
                                            dtReserveItems.AcceptChanges();

                                        }

                                        //Added New Record to the DataTable  
                                        dtReserveItems.Rows.Add(drCurrentRow);
                                        //storing DataTable to ViewState  
                                        ViewState["ReservedItems"] = dtReserveItems;
                                        //binding Gridview with New Row  
                                        //GridView1.DataSource = dtReserveItems;
                                        //GridView1.DataBind();
                                    }
                                }
                            }
                            #endregion

                            #region Block For Customer Registered

                            //if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;" && gvrow.Cells[14].Text != "0")
                            //{
                            //    objsales.itemcode = gvrow.Cells[2].Text;
                            //    //objsales.ItemID = dt.Rows[i][0].ToString();
                            //    objsales.whLocId = WH_Locations.getLocationID(TextBox2_value.Value);
                            //    // objsales.Barcode = dt.Rows[i][0].ToString();
                            //    // objsales.companyid = lblCPID.Text;
                            //    objsales.POID = gvrow.Cells[1].Text;
                            //    objsales.COLORID = gvrow.Cells[9].Text;
                            //    objsales.status = "Blocked";
                            //    objsales.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[15].Text);
                            //    //objsales.DeliveryDate = gvrow.Cells[15].Text;
                            //    objsales.CustomerId = custId.ToString();
                            //    //obj.CustomerId="0";

                            //    objsales.Block_Save2();
                            //}
                            #endregion
                        }
                    }

                }
                BulkInsertToDataBase();
                DataTable dtReservedItems = (DataTable)ViewState["ReservedItems"];
                if (dtReservedItems.Rows.Count > 1)
                {
                    BulkReservedItemsInsertToDataBase();
                }
                RecQty_Update();
                DamageQty_Update();
            }
            catch (Exception ex)
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
    private void BulkInsertToDataBase()
    {
        DataTable dtProductSold = (DataTable)ViewState["ProductsSold"];
        connection();
        //creating object of SqlBulkCopy  
        SqlBulkCopy objbulk = new SqlBulkCopy(con);
        //assigning Destination table name  
        objbulk.DestinationTableName = "INWARD";
        //Mapping Table column  
        objbulk.ColumnMappings.Add("Item_ID", "Item_ID");
        objbulk.ColumnMappings.Add("Barcode", "Barcode");
        objbulk.ColumnMappings.Add("ITEM_CODE", "ITEM_CODE");
        objbulk.ColumnMappings.Add("Cp_Id", "Cp_Id");
        objbulk.ColumnMappings.Add("whLocId", "whLocId");
        objbulk.ColumnMappings.Add("dt_added", "dt_added");
        objbulk.ColumnMappings.Add("COLOUR_ID", "COLOUR_ID");
        objbulk.ColumnMappings.Add("MRN_NO", "MRN_NO");

        // Create SqlBulkCopy
        SqlBulkCopy bulkData = new SqlBulkCopy(constr);
        bulkData.BulkCopyTimeout = 36000;
        bulkData.BatchSize = 10000;

        //inserting bulk Records into DataBase   
        objbulk.WriteToServer(dtProductSold);

        GridView1.DataSource = null;
        GridView1.DataBind();
        ViewState["ProductsSold"] = null;
        AddDefaultFirstRecord();

    }

    private void BulkReservedItemsInsertToDataBase()
    {
        DataTable dtReservedItems = (DataTable)ViewState["ReservedItems"];
        connection();
        //creating object of SqlBulkCopy  
        SqlBulkCopy objbulk = new SqlBulkCopy(con);
        //assigning Destination table name  
        objbulk.DestinationTableName = "BlOCK";
        //Mapping Table column  
        objbulk.ColumnMappings.Add("Item_ID", "Item_ID");
        objbulk.ColumnMappings.Add("Barcode", "Barcode");
        objbulk.ColumnMappings.Add("ITEM_CODE", "ITEM_CODE");
        objbulk.ColumnMappings.Add("Cp_Id", "Cp_Id");
        objbulk.ColumnMappings.Add("whLocId", "whLocId");
        objbulk.ColumnMappings.Add("dt_added", "dt_added");
        objbulk.ColumnMappings.Add("COLOUR_ID", "COLOUR_ID");
        objbulk.ColumnMappings.Add("SO_Id", "SO_Id");
        objbulk.ColumnMappings.Add("Status", "Status");
        objbulk.ColumnMappings.Add("Customer_Id", "Customer_Id");
        objbulk.ColumnMappings.Add("Delivery_Date", "Delivery_Date");

        //inserting bulk Records into DataBase   
        objbulk.WriteToServer(dtReservedItems);


        //GridView1.DataSource = null;
        //GridView1.DataBind();
        ViewState["ReservedItems"] = null;
        AddDefaultReserveItems();

    }
    private void Inward_Save()
    {
        btnSave.Enabled = false;

        if (TextBox2_value.Value != "")
        {
            try
            {
                int rowcount = 0;
                foreach (GridViewRow gvrow in gvInwardItems.Rows)
                {
                    CheckBox ch = new CheckBox();
                    ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                    if (ch.Checked == true)
                    {
                        TextBox qty = (TextBox)gvrow.FindControl("txtReceivedQty");
                        Masters.ItemPurchase objsales = new Masters.ItemPurchase();
                        int quantity = int.Parse(qty.Text);
                        int custId = 0;
                        if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;")
                        {
                            custId = int.Parse(gvrow.Cells[14].Text);
                        }
                        //for (int i = 0; i < quantity; i++)
                        //{
                        objsales.itemcode = gvrow.Cells[2].Text;

                        objsales.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                        objsales.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                        //objsales.companyid = cp.getPresentCompanySessionValue();
                        objsales.companyid = gvrow.Cells[16].Text;
                        objsales.MRNID = gvrow.Cells[1].Text;
                        objsales.COLORID = gvrow.Cells[9].Text;
                        objsales.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                        objsales.Quantity = qty.Text;
                        if (objsales.ItemInward_Save_New() == "Data Saved Successfully")
                        {
                            string code = "MRN No :" + gvrow.Cells[17].Text + "\n" + "Dt :" + gvrow.Cells[18].Text + "\n" + "Model No :" + gvrow.Cells[3].Text + "\n" + "Color :" + gvrow.Cells[6].Text + "\n" + " Brand=" + gvrow.Cells[19].Text + "\n" + " Description=" + gvrow.Cells[21].Text + "\n" + " ID=" + objsales.ItemID + "";
                            lblCode.Text = code;

                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(lblCode.Text, QRCodeGenerator.ECCLevel.Q);
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

                            using (Bitmap bitMap = qrCode.GetGraphic(20))
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    string itemimage = "";
                                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    byte[] byteImage = ms.ToArray();
                                    File.WriteAllBytes(Server.MapPath("~/Content/QRCodes/" + objsales.ItemID + ".png"), byteImage);
                                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

                                    Inventory.QRCode obj = new Inventory.QRCode();
                                    itemimage = objsales.ItemID + ".png";
                                    obj.MRN_Det_Id = gvrow.Cells[1].Text;
                                    obj.Item_Code = gvrow.Cells[2].Text;
                                    obj.Image = itemimage;
                                    obj.Image_Path = "http://183.82.108.55/Content/QRCodes/" + objsales.ItemID + ".png";
                                    obj.Item_Id = objsales.ItemID;
                                    obj.CHK_DET_Color = gvrow.Cells[9].Text;
                                    obj.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                                    TextBox txtqty = (TextBox)gvrow.FindControl("txtReceivedQty");
                                    obj.PrintQty = txtqty.Text;

                                    TextBox txtPrintqty = (TextBox)gvrow.FindControl("txtReceivedQty");
                                    obj.Qty = "1";
                                    obj.ISPrint = "0";
                                    obj.LocName = gvrow.Cells[12].Text;
                                    obj.Updateddt = DateTime.Now.ToString();
                                    obj.QRImage_Save1();

                                }
                            }
                        }
                        

                        if (gvrow.Cells[14].Text != "" && gvrow.Cells[14].Text != "&nbsp;" && gvrow.Cells[14].Text != "0")
                        {
                            objsales.itemcode = gvrow.Cells[2].Text;
                            objsales.whLocId = WH_Locations.getLocationID(TextBox2_value.Value);
                            objsales.POID = gvrow.Cells[1].Text;
                            objsales.COLORID = gvrow.Cells[9].Text;
                            objsales.status = "Blocked";
                            objsales.DeliveryDate = Yantra.Classes.General.toDDMMYYYY (gvrow.Cells[15].Text);
                            objsales.CustomerId = custId.ToString();
                            objsales.Quantity = qty.Text;

                            objsales.Block_Save_New();
                        }

                        //}
                    }
                    rowcount++;
                }
                RecQty_Update();
                DamageQty_Update();
            }
            catch (Exception ex)
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
            e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[21].Visible = false;

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
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "111");
        btnSave.Enabled = up.add;
        btnSavems.Enabled = up.add;
        btnDelete.Enabled = up.Delete;
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
        status = "Moving";
        btnDelete.Visible = false;
        btnDeleteMoved.Visible = true;
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

        // UpdateItemQty();
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

    //private void SaveMovingStock()
    //{
    //    foreach (GridViewRow gvrow in gvMovingItems.Rows)
    //    {
    //        CheckBox ch = (CheckBox)gvrow.FindControl("ChkBox_row");
    //        if (ch.Checked == true)
    //        {
    //            string Itemcode = gvrow.Cells[1].Text;

    //            SqlCommand cmd = new SqlCommand("select Item_ID,whLocId from V_StockInward where [ITEM_CODE]=" + Itemcode + " and [wh_id]=" + gvrow.Cells[11].Text + " ", con);
    //            cmd.CommandType = CommandType.Text;
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            da.Fill(dt);
    //            Masters.ItemPurchase objout = new Masters.ItemPurchase();
    //            TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
    //            int Quantity = int.Parse(qty.Text);
    //            // int locId = int.Parse(gvrow.Cells[11].Text);
    //            int whLocId = Convert.ToInt32(WH_Locations.getLocationID(TextBox2_value.Value));

    //            // int whLocId = int.Parse(ddlMovingto.SelectedItem.Value);

    //            if (dt.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < Quantity; i++)
    //                {
    //                    int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
    //                    string itemId = dt.Rows[i][0].ToString();
    //                    objout.InwardLoc_Update(locId, itemId, whLocId);
    //                }
    //            }
    //        }
    //    }

    //}
    private void SaveMovingStock()
    {
        try
        {


            foreach (GridViewRow gvrow in gvMovingItems.Rows)
            {
                CheckBox ch = (CheckBox)gvrow.FindControl("ChkBox_row");
                if (ch.Checked == true)
                {
                    string Itemcode = gvrow.Cells[1].Text;

                    SqlCommand cmd = new SqlCommand("SELECT isnull(sum(Avail_Qty),0) FROM [V_MovingStock] where avail_qty>0 and [ITEM_CODE]=" + Itemcode + " and [COLOUR_ID]=" + gvrow.Cells[8].Text + " and [wh_id]=" + gvrow.Cells[11].Text + " ", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Masters.ItemPurchase objout = new Masters.ItemPurchase();
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int Quantity = int.Parse(qty.Text);
                    if (Convert.ToInt32(dt.Rows[0][0]) < Quantity)
                    {
                        MessageBox.Show(this, "Required Quantity is not avaliable for item " + gvrow.Cells[2].Text + "");
                        //return(this,"Required Quantity is not avaliable for item '" + gvrow.Cells[2].Text + "'");
                        return;
                    }
                }
            }

            foreach (GridViewRow gvrow in gvMovingItems.Rows)
            {
                CheckBox ch = (CheckBox)gvrow.FindControl("ChkBox_row");
                if (ch.Checked == true)
                {
                    string Itemcode = gvrow.Cells[1].Text;

                    SqlCommand cmd = new SqlCommand("SELECT * FROM [V_MovingStock] where avail_qty>0 and [ITEM_CODE]=" + Itemcode + " and [COLOUR_ID]=" + gvrow.Cells[8].Text + " and [wh_id]=" + gvrow.Cells[11].Text + " ", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Masters.ItemPurchase objout = new Masters.ItemPurchase();
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int Quantity = int.Parse(qty.Text);
                    int Quantity1 = int.Parse(qty.Text);
                    // int locId = int.Parse(gvrow.Cells[11].Text);
                    int whLocId = Convert.ToInt32(WH_Locations.getLocationID(TextBox2_value.Value));

                    // int whLocId = int.Parse(ddlMovingto.SelectedItem.Value);

                    int rowcount = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Quantity1; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i][7].ToString()) > Convert.ToInt32(dt.Rows[i][4].ToString()))
                            {
                                if (Quantity >= Convert.ToInt32(dt.Rows[i][4]))
                                {
                                    int x = Convert.ToInt32(dt.Rows[i][7].ToString()) - Convert.ToInt32(dt.Rows[i][4].ToString());
                                    int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    objout.InwardLocNew_Update(itemId, locId, x);

                                    objout.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.companyid = dt.Rows[i][3].ToString();
                                    objout.MRNID = gvrow.Cells[0].Text;
                                    objout.COLORID = dt.Rows[i][2].ToString();
                                    objout.itemcode = Itemcode;
                                    objout.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                                    objout.Quantity = dt.Rows[i][4].ToString();
                                    objout.ItemInward_Save_New();
                                }
                                else
                                {
                                    int x = Convert.ToInt32(dt.Rows[i][7].ToString()) - Quantity;
                                    int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    objout.InwardLocNew_Update(itemId, locId, x);

                                    objout.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.companyid = dt.Rows[i][3].ToString();
                                    objout.MRNID = gvrow.Cells[0].Text;
                                    objout.COLORID = dt.Rows[i][2].ToString();
                                    objout.itemcode = Itemcode;
                                    objout.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                                    objout.Quantity = Quantity.ToString();
                                    objout.ItemInward_Save_New();
                                }
                            }
                            else
                            {
                                if (Quantity >= Convert.ToInt32(dt.Rows[i][4]))
                                {
                                    int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    objout.InwardLocNew_Update(locId, itemId, whLocId);
                                    //objout.qty = dt.Rows[i][4].ToString();
                                }
                                else if (Quantity < Convert.ToInt32(dt.Rows[i][4]))
                                {
                                    int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    int qty1 = Convert.ToInt32(dt.Rows[i][4]) - Quantity;
                                    objout.InwardLocNew_Update(itemId, locId, qty1);

                                    objout.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.companyid = dt.Rows[i][3].ToString();
                                    objout.MRNID = gvrow.Cells[0].Text;
                                    objout.COLORID = dt.Rows[i][2].ToString();
                                    objout.itemcode = Itemcode;
                                    objout.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                                    objout.Quantity = Quantity.ToString();
                                    objout.ItemInward_Save_New();
                                }

                            }
                            rowcount++;
                            Quantity = Quantity - Convert.ToInt32(dt.Rows[i][4]);
                            if (Quantity <= 0)
                            {
                                TextBox mvQty = (TextBox)gvrow.FindControl("txtQuantity");
                                int ttlQty = Convert.ToInt32(gvrow.Cells[9].Text);
                                string qty1 = (ttlQty - Convert.ToInt32(mvQty.Text)).ToString();
                                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                                obj.MovingItemQty_Update(qty1, gvrow.Cells[0].Text);
                                break;
                            }
                            //int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                            //string itemId = dt.Rows[i][0].ToString();
                            //objout.InwardLoc_Update(locId, itemId, whLocId);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
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
    protected void btnDeleteMoved_Click(object sender, EventArgs e)
    {
        #region Delete Stock
        try
        {
            int i = 0;
            foreach (GridViewRow gvrow in gvMovingItems.Rows)
            {
                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("ChkBox_row");
                if (ch.Checked == true)
                {
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");

                    string actualQty = gvrow.Cells[9].Text;

                    int updateQty = (Convert.ToInt32(actualQty.ToString()) - Convert.ToInt32(qty.Text));

                    if (updateQty > 0)
                    {
                        string itemId = gvrow.Cells[0].Text;
                        SqlCommand cmd = new SqlCommand("Update STOCKMOVEMENT_DETAILS set QUANTITY = " + updateQty + " where SM_DCDET_ID = '" + itemId + "'  ", con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        i += cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (updateQty == 0)
                    {
                        string itemId = gvrow.Cells[0].Text;
                        SqlCommand cmd = new SqlCommand("Delete from STOCKMOVEMENT_DETAILS where SM_DCDET_ID = '" + itemId + "'  ", con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        i += cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "You Cannot delete more Items than the existing Quantity");
                    }
                }
            }
            if (i > 0)
            {
                MessageBox.Show(this, "Items Deleted Successfully");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            LoadMovingStock();
        }
        #endregion
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        #region Delete Stock
        try
        {
            int i = 0;
            foreach (GridViewRow gvrow in gvInwardItems.Rows)
            {
                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                if (ch.Checked == true)
                {
                    TextBox qty = (TextBox)gvrow.FindControl("txtReceivedQty");
                    string actualQty = gvrow.Cells[10].Text;

                    int updateQty = (Convert.ToInt32(actualQty.ToString()) - Convert.ToInt32(qty.Text));

                    if (updateQty > 0)
                    {
                        string itemId = gvrow.Cells[0].Text;
                        SqlCommand cmd = new SqlCommand("Update Temp_Inward set Balance_Qty = " + updateQty + " where Id = '" + itemId + "'  ", con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        i += cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (updateQty == 0)
                    {
                        string itemId = gvrow.Cells[0].Text;
                        SqlCommand cmd = new SqlCommand("Delete from Temp_Inward where Id = '" + itemId + "'  ", con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        i += cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "You Cannot delete more Items than the existing Quantity");
                    }
                }
            }
            if (i > 0)
            {
                MessageBox.Show(this, "Items Deleted Successfully");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            BindGrid();
        }
        #endregion
    }

}

