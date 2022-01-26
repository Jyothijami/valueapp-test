using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;
using YantraDAL;

public partial class dev_pages_ExcelPriceUodate : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    private static int _returnIntValue;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand1);
            BindPriceGrid();
            lblTotalTicketsRaised.Text = gvExcel.Rows.Count.ToString();
            Label1.Text = GridView1 .Rows.Count.ToString();
            Label2.Text = gvDiff.Rows.Count.ToString();
            gvExcel.DataBind();
            GridView1.DataBind();
            gvDiff.DataBind();
            //lblBrandID.Text = "54";
            //gvAppDiff.DataBind();
            lblTtl.Text = Label3.Text = GridView1.Rows.Count.ToString();
            //lblappcount.Text = gvExcelFile.Rows.Count.ToString();
            
            Masters.ProductCompany.ExcelFiles_Select(ddlExcel);

        }
    }
    protected void btnfileUpload_Click(object sender, EventArgs e)
    {
        General obj = new General();
        if (FileUpload1.HasFile)
        {
            string sql = @"select Filename  from  Excel_Price_File where Filename = '" + FileUpload1.FileName + "' ";
            DataTable dttemp3 = obj.ReturnDataTable(sql);

            if (dttemp3.Rows.Count >= 0)
            {
                string Sqls = @"insert into Excel_Price_File(Filename) values('" + FileUpload1.FileName + "')";
                string strreturn1 = obj.ReturnExecuteNoneQuery(Sqls);

                string filename = FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath(filename));




                SM.SalesOrder smobjj = new SM.SalesOrder();
                smobjj.MumbaiStockDetails_Delete();

                smobjj.SOPreparedBy  = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                //smobjj.MumbaiStockDetails_Update();
                ExportToGrid(Server.MapPath(filename));
                //GVBind();
            }
            else
            {
                MessageBox.Show(this, "File name Already Exist");
            }
        }
    }

    private void ExportToGrid(string path)
    {
        OleDbConnection MyConnection = null;
        DataSet DtSet = null;
        OleDbDataAdapter MyCommand = null;
        MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties='Excel 12.0;IMEX=1'");


        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
        DtSet = new System.Data.DataSet();
        MyCommand.Fill(DtSet, "[Sheet1$]");
        dt = DtSet.Tables[0];
        MyConnection.Close();

        General obj = new General();

        if (dt.Rows.Count > 0)
        {
            bool found = false;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.ItemArray[0].ToString() == "")
                {
                    found = true;
                    MessageBox.Show(this, "Please Check Excel Having Correct Values");
                    break;
                }

                else
                {

                    string ItemCode = "0";
                    string Price = "0";
                    string filename = FileUpload1.FileName;

                    if (dr.ItemArray[1].ToString() == "")
                    {
                        Price = "0";
                    }
                    else
                    {
                        Price = dr.ItemArray[1].ToString();
                    }

                    string sql = @"insert into excel_Price(Item_Model_No,Price,Brand,Filename)
                                values('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "','" + dr.ItemArray[2].ToString() + "', '"+filename+"') ";
                        obj.ReturnExecuteNoneQuery(sql);


                        string sql2 = @"insert into ExcelPrice_aud(Item_ModelNo,Price,Brand,updated_at,Filename)
                                values('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "','" + dr.ItemArray[2].ToString() + "',getdate(), '" + filename + "') ";
                        obj.ReturnExecuteNoneQuery(sql2);
                       


                }

            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
    protected void ddlNoOfRecord2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemPriceUpdate.PageSize = Convert.ToInt32(ddlNoOfRecord2.SelectedValue);
        BindPriceGrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Masters.ItemPurchase obj = new Masters.ItemPurchase();
        General obj1 = new General();
        foreach (GridViewRow gvrow in gvExcel.Rows)
        {
            if (gvrow .Cells [5].Text != gvrow .Cells [6].Text)
            {
                int itemcode = int.Parse(gvrow.Cells[0].Text);
                decimal  GrossAmt =decimal.Parse( gvrow.Cells[6].Text);
                //decimal Factor = decimal.Parse (gvrow.Cells[4].Text);
                decimal Factor = 1;
                decimal Price = GrossAmt * Factor;

                decimal OldPrice = decimal.Parse(gvrow.Cells[5].Text);
                string filename = gvrow.Cells[8].Text;

                obj.ExcelPriceUpdate(itemcode, GrossAmt, Price, OldPrice, Factor);
                string sql = @"insert into updatepriceCheking(Itemcode,filename)
                                values('" + itemcode + "','" + filename + "') ";
                obj1.ReturnExecuteNoneQuery(sql);
                MessageBox.Show(this, "Prices Updated Sucessfully");
                //Masters.ItemPurchase.ItemModel_RateCalc(gvrow.Cells[0].Text, txtPercentage.Text, Convert.ToDecimal(gvrow.Cells[4].Text));
                gvExcel.DataBind();
                lblTotalTicketsRaised.Text = gvExcel.Rows.Count.ToString();
                Label1.Text = GridView1.Rows.Count.ToString();
            }
            MessageBox.Show(this, "Item Rate's Updated Sucessfully");
        }
    }
    protected void lnkBalcheck_Click(object sender, EventArgs e)
    {
        pnlbal.Visible = true;
        pnlUpload.Visible = false;
        pnlDiff.Visible = false;
        pnlexcl.Visible = false;
    }
    protected void ddlBrand1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemCategory.ItemCategory_Select_WithBrand(ddlCategory1, ddlBrand1.SelectedItem.Value);

    }
    protected void ddlCategory1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemCategory.ItemCategory_SelectForPrint(ddlSubCategory1, ddlBrand1.SelectedItem.Value, ddlCategory1.SelectedItem.Value);

    }
    protected void ddlSubCategory1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo1, "select DISTINCT YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_ITEM_MAST,YANTRA_ITEM_PRICE where YANTRA_ITEM_PRICE.SubCat_Id = YANTRA_ITEM_MAST.IT_TYPE_ID and YANTRA_ITEM_PRICE.Brand_id=YANTRA_ITEM_MAST.BRAND_ID " +
"and YANTRA_ITEM_MAST.IC_ID=YANTRA_ITEM_PRICE.Cat_Id and YANTRA_ITEM_PRICE.SubCat_Id = '" + ddlSubCategory1.SelectedItem.Value + "' and YANTRA_ITEM_PRICE.Brand_id='" + ddlBrand1.SelectedItem.Value + "' and YANTRA_ITEM_PRICE.Cat_Id='" + ddlCategory1.SelectedItem.Value + "' ");


    }
    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlModelNo1.DataSourceID = "SqlDataSource2";
        ddlModelNo1.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo1.DataValueField = "ITEM_CODE";
        ddlModelNo1.DataBind();

        ddlSubCategory1.SelectedIndex = -1;
        ddlCategory1.SelectedIndex = -1;
        ddlBrand1.SelectedIndex = -1;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindPriceGrid();
    }
    private void BindPriceGrid()
    {
        if (txtSearchModel.Text != "")
        {
            SqlCommand cmd = new SqlCommand("SP_MASTER_ITEMPRICE_MODIFY_SELECT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlBrand1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Brand", ddlBrand1.SelectedItem.Value);
            }

            if (ddlCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Category", ddlCategory1.SelectedItem.Value);
            }

            if (ddlSubCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@SubCat", ddlSubCategory1.SelectedItem.Value);
            }

            if (ddlModelNo1.SelectedIndex > -1)
            {
                cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo1.SelectedItem.Value);
            }



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvItemPriceUpdate.DataSource = dt;
            gvItemPriceUpdate.DataBind();
        }
        else
        {
            //MessageBox.Show(this, "Please Search By selecting any one field atleast");
            SqlCommand cmd = new SqlCommand("SP_MASTER_ITEMPRICE_MODIFY_SELECT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlBrand1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Brand", ddlBrand1.SelectedItem.Value);
            }

            if (ddlCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@Category", ddlCategory1.SelectedItem.Value);
            }

            if (ddlSubCategory1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@SubCat", ddlSubCategory1.SelectedItem.Value);
            }

            if (ddlModelNo1.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo1.SelectedItem.Value);
            }



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvItemPriceUpdate.DataSource = dt;
            gvItemPriceUpdate.DataBind();
        }
    }
    protected void gvItemPriceUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemPriceUpdate.PageIndex = e.NewPageIndex;
        BindPriceGrid();

    }
    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        pnlDiff.Visible = false;
        pnlUpload.Visible = false;
        pnlbal.Visible = false;
        pnlexcl.Visible = true;
    }
    protected void lnkDifCheck_Click(object sender, EventArgs e)
    {
        pnlDiff.Visible = true;
        pnlUpload.Visible = false;
        pnlbal.Visible = false;
        pnlexcl.Visible = false ;

    }
    string BrandID;
    protected void gvExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header )
        {
            e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
             BrandID = e.Row.Cells[7].Text;
        }
        //lblBrandID.Text = BrandID;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BrandID = e.Row.Cells[7].Text;
        }
    }

    protected void ddlExcel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ProductCompany obj = new Masters.ProductCompany();
            if (obj.ExcelBrand_Select(ddlExcel.SelectedItem.Text) > 0)
            {
                lblBrandID.Text = obj.PdCompanyName;


                //General.GridBindwithCommand(gvExcelFile, "select YANTRA_ITEM_MAST.ITEM_CODE ,item_spec,ITEM_MODEL_NO as ModelNo ,PRODUCT_COMPANY_NAME as Brand ,Item_Price,GrossAmount,MulFactor,CONVERT (NVARCHAR(50),DATE,103)AS Updated_dt from YANTRA_ITEM_MAST inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID  left outer join YANTRA_ITEM_PRICE on YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code where PRODUCT_COMPANY_NAME  = '" + lblBrandID.Text + "'   and ITEM_MODEL_NO not in (select item_modelno  from excelprice_aud) and CONVERT (NVARCHAR(50),DATE,103) !=CONVERT (nvarchar(50),getdate(),103) and CONVERT (NVARCHAR(50),DATE,103)<'19/02/2020' and YANTRA_ITEM_MAST.ITEM_CODE not in (select Itemcode  from updatepriceCheking where filename != '"+ddlExcel.SelectedItem.Text+"') ");
                General.GridBindwithCommand(gvExcelFile, "select YANTRA_ITEM_MAST.ITEM_CODE ,item_spec,ITEM_MODEL_NO as ModelNo ,PRODUCT_COMPANY_NAME as Brand ,Item_Price,GrossAmount,MulFactor,CONVERT (NVARCHAR(50),DATE,103)AS Updated_dt,F2 from YANTRA_ITEM_MAST inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID  RIGHT outer join YANTRA_ITEM_PRICE on YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code where PRODUCT_COMPANY_NAME  = '" + lblBrandID.Text + "'   and YANTRA_ITEM_MAST.ITEM_CODE not in (select Itemcode  from updatepriceCheking where filename = '" + ddlExcel.SelectedItem.Text + "') ");

                General.GridBindwithCommand(gvExl, "select item_modelNo as ModelNo,price,CONVERT (nvarchar(50),Updated_at,103) as Uploaded_Date from excelprice_aud where filename ='" + ddlExcel.SelectedItem.Text + "' ");
                General.GridBindwithCommand(gvAppDiff, "select YANTRA_ITEM_MAST.ITEM_CODE ,item_spec,ITEM_MODEL_NO ,PRODUCT_COMPANY_NAME as Brand ,Item_Price,GrossAmount,MulFactor,CONVERT (NVARCHAR(50),DATE,103)AS Updated_dt from YANTRA_ITEM_MAST inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID  RIGHT outer join YANTRA_ITEM_PRICE on YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code where PRODUCT_COMPANY_NAME  = '" + lblBrandID.Text + "'   ");
                
                //gvExcelFile.DataBind();
                lblappcount.Text = gvExcelFile.Rows.Count.ToString();
                lblExcelCount.Text = gvExl.Rows.Count.ToString();
                lblAppttl.Text = gvAppDiff.Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnPriceUpdate_Click(object sender, EventArgs e)
    {
        Masters.ItemPurchase obj = new Masters.ItemPurchase();
        General obj1 = new General();
        foreach (GridViewRow gvr in gvExcelFile.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    TextBox t1 = gvr.FindControl("txtPrice") as TextBox;
                    if (t1.Text != "")
                    {
                        
                            int itemcode = int.Parse(gvr.Cells[0].Text);
                            decimal GrossAmt = decimal.Parse(t1.Text);
                            //decimal Factor = decimal.Parse(gvr.Cells[5].Text);
                            decimal Factor = 1;

                            decimal Price = decimal.Parse (t1.Text );
                            decimal OldPrice = decimal.Parse(gvr.Cells[4].Text);
                            obj.ExcelPriceUpdate(itemcode, GrossAmt, Price, OldPrice, Factor);

                            string filename = ddlExcel.SelectedItem.Text;

                            string sql = @"insert into updatepriceCheking(Itemcode,filename)
                                values('" + itemcode + "','" + filename + "') ";
                            obj1.ReturnExecuteNoneQuery(sql);
                            //Masters.ItemPurchase.ItemModel_RateCalc(gvrow.Cells[0].Text, txtPercentage.Text, Convert.ToDecimal(gvrow.Cells[4].Text));
                            //gvExcelFile.DataBind();
                            MessageBox.Show(this, "Prices Updated Sucessfully");

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        //General.GridBindwithCommand(gvExcelFile, "select YANTRA_ITEM_MAST.ITEM_CODE ,item_spec,ITEM_MODEL_NO as ModelNo ,PRODUCT_COMPANY_NAME as Brand ,Item_Price,GrossAmount,MulFactor,CONVERT (NVARCHAR(50),DATE,103)AS Updated_dt from YANTRA_ITEM_MAST inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID  left outer join YANTRA_ITEM_PRICE on YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code where PRODUCT_COMPANY_NAME  = '" + lblBrandID.Text + "'   and ITEM_MODEL_NO not in (select item_modelno  from excelprice_aud) and CONVERT (NVARCHAR(50),DATE,103) !=CONVERT (nvarchar(50),getdate(),103) and CONVERT (NVARCHAR(50),DATE,103)<'19/02/2020' and YANTRA_ITEM_MAST.ITEM_CODE not in (select Itemcode  from updatepriceCheking where filename != '" + ddlExcel.SelectedItem.Text + "') ");

        General.GridBindwithCommand(gvExcelFile, "select YANTRA_ITEM_MAST.ITEM_CODE ,item_spec,ITEM_MODEL_NO as ModelNo ,PRODUCT_COMPANY_NAME as Brand ,Item_Price,GrossAmount,MulFactor,CONVERT (NVARCHAR(50),DATE,103)AS Updated_dt from YANTRA_ITEM_MAST inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID  rIGHT outer join YANTRA_ITEM_PRICE on YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code where PRODUCT_COMPANY_NAME  = '" + lblBrandID.Text + "'   and  YANTRA_ITEM_MAST.ITEM_CODE not in (select Itemcode  from updatepriceCheking where filename = '" + ddlExcel.SelectedItem.Text + "') ");
        lblappcount.Text = gvExcelFile.Rows.Count.ToString();
        lblExcelCount.Text = gvExl.Rows.Count.ToString();

    }
    protected void gvExcelFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text == "Discontinued")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
    }
}