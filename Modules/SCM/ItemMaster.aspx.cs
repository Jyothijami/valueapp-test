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
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.IO;
using vllib;
public partial class Modules_SCM_ItemMaster : basePage
{

    FileUpload fu1 = new FileUpload();


    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            setControlsVisibility();

            lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
            lblSearchValueHidden.Text = txtSearchText.Text;
            lblCPID.Text = (string)cp.getPresentCompanySessionValue();
            //lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
            //lblSearchValueHidden.Text = txtSearchText.Text;
            
            //lblCPID.Text = (string)cp.getPresentCompanySessionValue();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            gvItemsMasterDetails.DataBind();
            //GridView1.DataBind();

            FillPurchaseItemType();
            //FillItemType();
            FillUOM();
            FillItemCategory();
            FillBrand();
            CompanyName_Fill();
            FillGodownGen();
            //FillModelNo();

            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            if (user == "0")
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }

        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "67");
        btnNew.Enabled = up.add;
        btnPrint.Enabled = up.Print;
        //btnEdit.Enabled = up.Update;
        //btnDelete.Enabled = up.Delete;
        //btnEditQty.Enabled = up.Update;
        
        //btnClose1.Enabled=up.Close1;
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnStockSave.Enabled = up.StockSave;
        //btnStockRefresh.Enabled = up.StockRefresh;
        //btnCloseStock.Enabled = up.CloseStock;
        //btnSearchPrint.Enabled = up.SearchPrint;
        //btnSubmit.Enabled = up.Submit;
        //btnClosePrint.Enabled = up.ClosePrint;
        //btnUpload.Enabled = up.Upload;
        //Button1.Enabled = up.Upload1;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
    }
    
    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region FillItemUOM
    private void FillUOM()
    {
        try
        {
            Masters.UnitMaster.UnitMaster_Select(ddlItemUOM);
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
    #endregion

    #region Fill ModelNo
    private void FillModelNo()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_ModelNoGenSelect(ddlModelnoPrint);
            ddlModelnoPrint.Items.FindByText("--").Text = "All"; 
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
    #endregion

    #region Fill Item Type
    private void FillItemType()
    {
        try
        {
            Masters.ItemMaster.ItemMaster1_Select(ddlType, ddlItemCategory.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Fill Purchase Item Type
    private void FillPurchaseItemType()
    {
        try
        {
            Masters.PurchaseItemType.ItemType_Select(ddlPurchaseItemType);
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
    #endregion

    #region Fill Item Category
    private void FillItemCategory()
    {
        try
        {
            Masters.ItemCategory.ItemCategory_Select(ddlItemCategory);
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
    #endregion


    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);
            Masters.CompanyProfile.Company_Select(ddlCmpPrint);
            //ddlCmpPrint.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }

    }
    #endregion

    #region Fill FillGodownGen
    private void FillGodownGen()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_GoDown(ddlGodownPrint);
            //ddlGodownPrint.Items.FindByText("--").Text = "All";
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
    #endregion

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrandName);
            Masters.ProductCompany.ProductCompany_Select(ddlBrandStock);
            Masters.ProductCompany.ProductCompany_Select(ddlBrandPrint);
        //    ddlBrandPrint.Items.FindByText("--").Text = "All";
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
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        txtSpecification.Text = txtSpecification.Text.Replace("'", " ");
        txtPurchaseSpec.Text = txtPurchaseSpec.Text.Replace("'", " ");
        if (btnSave.Text == "Save")
        {
            ItemMasterSave();
            tblItDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            ItemMasterUpdate();
            tblItDetails.Visible = false;
        }
        gvItemsMasterDetails.SelectedIndex = -1;
    }
    #endregion

    #region ItemMasterSave
    private void ItemMasterSave()
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            objMaster.ItemName = txtItemName.Text;
            objMaster.ItemSpec = txtSpecification.Text;
            objMaster.ItemMinStockQty = txtMinimum.Text;
            objMaster.ItemMaterialType = txtMatirealType.Text;
            objMaster.ItemType = ddlType.SelectedItem.Value;
            objMaster.ItemQtyInHand = "0";
            objMaster.UOMId = ddlItemUOM.SelectedItem.Value;
            objMaster.ItemRate = txtRate.Text;
            objMaster.ItemPrincipalName = txtPrincipalName.Text;
            objMaster.ItemSeries = txtSeries.Text;
            objMaster.ItemPurchaseSpec = txtPurchaseSpec.Text;
            objMaster.ItemPurchaseTypeId = ddlPurchaseItemType.SelectedItem.Value;
            objMaster.ModelNo = txtModelNo.Text;
            objMaster.ItemCategoryId = ddlItemCategory.SelectedItem.Value;
            objMaster.BrandName = ddlBrandName.SelectedItem.Value;
            objMaster.Color = txtColor.Text;
            objMaster.IFY = txtFinacalYear.Text;
            if (FileUpload1.HasFile)
            {
                objMaster.ItemAttachments = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objMaster.ItemAttachments = "";
            }
            MessageBox.Show(this, objMaster.ItemMaster_Save());
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Masters/Items/" + objMaster.ItemCode + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvItemsMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ItemMasterUpdate
    private void ItemMasterUpdate()
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            objMaster.ItemCode = gvItemsMasterDetails.SelectedRow.Cells[1].Text;
            objMaster.ItemName = txtItemName.Text;
            objMaster.ItemSpec = txtSpecification.Text;
            objMaster.ItemMinStockQty = txtMinimum.Text;
            objMaster.ItemMaterialType = txtMatirealType.Text;
            objMaster.ItemType = ddlType.SelectedItem.Value;
            //  objMaster.ItemType = ddlType.SelectedItem.Text;
            objMaster.ItemQtyInHand = "0";
            objMaster.UOMId = ddlItemUOM.SelectedItem.Value;
            objMaster.ItemRate = txtRate.Text;
            objMaster.ItemPrincipalName = txtPrincipalName.Text;
            objMaster.ItemSeries = txtSeries.Text;
            objMaster.ItemPurchaseSpec = txtPurchaseSpec.Text;
            objMaster.ItemPurchaseTypeId = ddlPurchaseItemType.SelectedItem.Value;
            objMaster.ModelNo = txtModelNo.Text;
            objMaster.ItemCategoryId = ddlItemCategory.SelectedItem.Value;
            objMaster.BrandName = ddlBrandName.SelectedItem.Value;
            objMaster.Color = txtColor.Text;
            objMaster.IFY = txtFinacalYear.Text;
            if (FileUpload1.HasFile)
            {
                objMaster.ItemAttachments = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objMaster.ItemAttachments = lbtnAttachedFile.Text;
            }
            MessageBox.Show(this, objMaster.ItemMaster_Update());
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Masters/Items/" + objMaster.ItemCode + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvItemsMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvItemsMasterDetails_RowDataBound
    protected void gvItemsMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)        
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[13].Visible = false;
           e.Row.Cells[11].Visible = false;
           e.Row.Cells[12].Visible = false;
           e.Row.Cells[17].Visible = false;
           //e.Row.Cells[8].Visible = false;
        }

    }
    #endregion

    #region Link Button ItemMasterName_Click
    protected void lbtnItemMasterName_Click(object sender, EventArgs e)
    {
        tblItDetails.Visible = false;
        LinkButton lbtnItemMasterName;
        lbtnItemMasterName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnItemMasterName.Parent.Parent;
        gvItemsMasterDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
       // btnEdit_Click(sender, e);
        btnCloseStock_Click(sender, e);
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnStockSave.Text = "Update";
        if (gvItemsMasterDetails.SelectedIndex > -1)
        {
            tblStockEntry.Visible = true;
            ddlBrandStock.SelectedValue = gvItemsMasterDetails.SelectedRow.Cells[11].Text.ToString();
            ddlBrandStock_SelectedIndexChanged(sender, e);
            ddlCompany.SelectedValue = gvItemsMasterDetails.SelectedRow.Cells[10].Text.ToString();
            ddlCompany_SelectedIndexChanged(sender, e);
            ddlModelnoStock.SelectedValue=gvItemsMasterDetails.SelectedRow.Cells[1].Text.ToString();
            ddlModelnoStock_SelectedIndexChanged(sender, e);
            ddlGoDown.SelectedValue = gvItemsMasterDetails.SelectedRow.Cells[12].Text.ToString();
            //ddlColor.SelectedValue = gvItemsMasterDetails.SelectedRow.Cells[12].Text.ToString();
            if (gvItemsMasterDetails.SelectedRow.Cells[13].Text.ToString() == "&nbsp;" || gvItemsMasterDetails.SelectedRow.Cells[13].Text.ToString() == "")
            {
                ddlColor.SelectedValue = "0";
            }
            else
            {
                ddlColor.SelectedValue = gvItemsMasterDetails.SelectedRow.Cells[13].Text.ToString();
            }
            txtQty.Text = gvItemsMasterDetails.SelectedRow.Cells[8].Text.ToString();
            lblResqty.Text = gvItemsMasterDetails.SelectedRow.Cells[9].Text.ToString();
            

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvItemsMasterDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.ItemMaster objMaster = new Masters.ItemMaster();
                objMaster.ItemCode = gvItemsMasterDetails.SelectedRow.Cells[16].Text;
                MessageBox.Show(this, objMaster.stockItemMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblItDetails.Visible = false;

                gvItemsMasterDetails.DataBind();
                gvItemsMasterDetails.SelectedIndex = -1;

                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        btnStockSave.Text = "Save";
        tblStockEntry.Visible = true;
        tblPrint.Visible = false;
        btnStockRefresh_Click(sender, e);

        //tblNew.Visible = true;

        ////Masters.ItemMaster obj = new Masters.ItemMaster();
        ////try
        ////{
        ////    if (GridView1.HeaderRow.Cells[9].Visible == true)
        ////    {
        ////        if (btnNew.Text == "Insert")
        ////            btnNew.Text = "New";
               
        ////        GridView1.HeaderRow.Cells[9].Visible = false;
        ////        foreach (GridViewRow gvrow in GridView1.Rows)
        ////        {
        ////            if (gvrow.RowType != DataControlRowType.Pager)
        ////            {
        ////                gvrow.Cells[9].Visible = false;
        ////            }
        ////            if (gvrow.RowType == DataControlRowType.DataRow)
        ////            {
        ////                TextBox t1 = gvrow.FindControl("txtEditQty") as TextBox;
        ////                string resQty = "0";
        ////                obj.ItemMaster_Update(Convert.ToInt64(gvrow.Cells[1].Text), Convert.ToInt64(t1.Text), resQty);
        ////            }
        ////            MessageBox.Show(this, "Data Updated Successfully");
        ////        }
        ////        gvItemsMasterDetails.DataBind();
        ////        GridView1.DataBind();
        ////    }
        ////    else
        ////    {
        ////        if (btnNew.Text == "New")
        ////            btnNew.Text = "Insert";
        ////        GridView1.HeaderRow.Cells[9].Visible = true;
        ////        foreach (GridViewRow gvrow in GridView1.Rows)
        ////        {
        ////            if (gvrow.RowType != DataControlRowType.Pager)
        ////            {
        ////                gvrow.Cells[9].Visible = true;
        ////            }
        ////        }
        ////    }
        ////}
        ////catch (Exception  ex)
        ////{
        ////    MessageBox.Show(this,"There is no New Items");
        ////}

        //Masters.ClearControls(this);
        //ddlType.Items.Clear();
        //btnSave.Text = "Save";
        //tblItDetails.Visible = true;
        //Image1.ImageUrl = "~/Images/noimage400x300.gif";
        //Image2.ImageUrl = "~/Images/noimage400x300.gif";

    }
    #endregion

    #region  Button Refresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblItDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvItemsMasterDetails.DataBind();
    }
    #endregion

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster1_Select(ddlType, ddlItemCategory.SelectedValue);
        //FillItemType();
    }


    protected void btnEditQty_Click(object sender, EventArgs e)
    {
        Masters.ItemMaster obj = new Masters.ItemMaster();

        if (gvItemsMasterDetails.HeaderRow.Cells[9].Visible == true)
        {
            gvItemsMasterDetails.HeaderRow.Cells[9].Visible = false;
            foreach (GridViewRow gvrow in gvItemsMasterDetails.Rows)
            {
                if (gvrow.RowType != DataControlRowType.Pager)
                {
                    gvrow.Cells[9].Visible = false;
                }
                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    TextBox t1 = gvrow.FindControl("txtEditQty") as TextBox;
                    string resQty = gvrow.Cells[11].Text;
                  //  obj.ItemMaster_Update(Convert.ToInt64(gvrow.Cells[1].Text), Convert.ToInt64(t1.Text),resQty);
                }
                MessageBox.Show(this, "Data Updated Successfully");
               }
               lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
               lblSearchValueHidden.Text = txtSearchText.Text;
               lblCPID.Text = (string)cp.getPresentCompanySessionValue();
               gvItemsMasterDetails.DataBind();
     


         }
            
        else
        {
            gvItemsMasterDetails.HeaderRow.Cells[9].Visible = true;
            foreach (GridViewRow gvrow in gvItemsMasterDetails.Rows)
            {
                if (gvrow.RowType != DataControlRowType.Pager)
                {
                    gvrow.Cells[9].Visible = true;
                }
            }
        }
        
    }

    protected void btnClose1_Click1(object sender, EventArgs e)
    {
       // Page_Load(sender, e);
        gvItemsMasterDetails.DataBind();
       // gvItemsMasterDetails.HeaderRow.Cells[9].Visible = false;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        tblPrint.Visible = true;
        tblStockEntry.Visible = false;
        //Masters.ItemMaster.sample();
        //string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ItemQty&cpid=" + lblCPID.Text + "";
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);          

    }

    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
        if (GridView1.HeaderRow.Cells[9].Visible == true)
        {        
             GridView1.HeaderRow.Cells[9].Visible = true;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                if (gvrow.RowType != DataControlRowType.Pager)
                {
                    gvrow.Cells[9].Visible = true;
                }              
            }            
        }
        else
        {            
            GridView1.HeaderRow.Cells[9].Visible = false;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                if (gvrow.RowType != DataControlRowType.Pager)
                {
                    gvrow.Cells[9].Visible = false;
                }
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster_GoDownSelect(ddlGoDown, ddlCompany.SelectedValue);
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

    protected void btnStockSave_Click(object sender, EventArgs e)
    {
        if (btnStockSave.Text == "Save")
        {
             try
            {

                if (lblResqty.Text == "")
                {
                    lblResqty.Text = "0";
                }

            Masters.ItemMaster obj = new Masters.ItemMaster();
            Masters.BeginTransaction();
            obj.ItemMaster_UpdateStock(Convert.ToInt64(ddlModelnoStock.SelectedValue), Convert.ToInt64(txtQty.Text), Convert.ToInt32(lblResqty.Text), Convert.ToInt32(ddlGoDown.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlColor.SelectedValue));
            Masters.CommitTransaction();
            MessageBox.Show(this, "Stock Saved Sucessfully");
            gvItemsMasterDetails.DataBind();
            tblStockEntry.Visible = false;
            }
            catch (Exception ex)
            {
                Masters.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Masters.Dispose();
            }
        
        }
        else if (btnStockSave.Text == "Update")
        {
            try
            {
                Masters.ItemMaster obj = new Masters.ItemMaster();
                //Masters.BeginTransaction();
                //obj.qtyitemcode = ddlModelnoStock.SelectedItem.Value;
                //obj.qtycolorid = ddlColor.SelectedItem.Value;
                //obj.qtycompanyid = ddlCompany.SelectedItem.Value;
                //obj.qtyquantity = txtQty.Text;
                //obj.qtygdid = ddlGoDown.SelectedItem.Value;
                ////obj.ItemMaster_EditUpdateStock(gvItemsMasterDetails.SelectedRow.Cells[16].Text);

                obj.ItemMaster_EditUpdateStock(gvItemsMasterDetails.SelectedRow.Cells[17].Text.ToString(), Convert.ToInt64(ddlModelnoStock.SelectedValue), Convert.ToInt64(txtQty.Text), "0", Convert.ToInt32(ddlGoDown.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlColor.SelectedValue));
                //Masters.CommitTransaction();
                MessageBox.Show(this, "Stock Updated Sucessfully");
                //  MessageBox.Show(this, obj.ItemMaster_EditUpdateStock());
                gvItemsMasterDetails.DataBind();
                tblStockEntry.Visible = false;
            }
            catch (Exception ex)
            {
                //Masters.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Masters.Dispose();
            }

        }
    }
    protected void btnCloseStock_Click(object sender, EventArgs e)
    {
        tblStockEntry.Visible = false;
    }

    protected void btnStockRefresh_Click(object sender, EventArgs e)
    {
        ddlCompany.SelectedValue = "0";
        ddlModelnoStock.Items.Clear();
        ddlBrandStock.SelectedValue="0";
        txtSearchModel.Text = "";
        ddlGoDown.SelectedValue="0";
        txtQty.Text = "";
    }

    protected void btnClosePrint_Click(object sender, EventArgs e)
    {
        tblPrint.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ItemQty&cpid=" + ddlCmpPrint.SelectedValue + "&gdid="+ ddlGodownPrint.SelectedValue +"&brandid="+ ddlBrandPrint.SelectedValue + "&MdNo="+ ddlModelnoPrint.SelectedValue +"";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);          
    }

    protected void ddlCmpPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster_GoDownSelect(ddlGodownPrint, ddlCmpPrint.SelectedValue);
            //ddlGodownPrint.Items.FindByText("--").Text = "All"; 
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

    protected void ddlBrandStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelnoStock, ddlBrandStock.SelectedValue);
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

    protected void ddlModelnoStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMasterStock_Select(ddlModelnoStock.SelectedItem.Value) > 0)
            {
                ddlBrandStock.SelectedValue = objMaster.Brandid;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelnoStock.SelectedValue);
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

    protected void ddlBrandPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelnoPrint, ddlBrandPrint.SelectedValue);
            //ddlModelnoPrint.Items.FindByText("--").Text = "All"; 
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

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelnoPrint.DataSourceID = "SqlDataSource3";
        ddlModelnoPrint.DataTextField = "ITEM_MODEL_NO";
        ddlModelnoPrint.DataValueField = "ITEM_CODE";
        ddlModelnoPrint.DataBind();
    }

    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlModelnoStock.DataSourceID = "SqlDataSource2";
        ddlModelnoStock.DataTextField = "ITEM_MODEL_NO";
        ddlModelnoStock.DataValueField = "ITEM_CODE";
        ddlModelnoStock.DataBind();
        ddlModelnoStock_SelectedIndexChanged(sender, e);
    }


    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemsMasterDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvItemsMasterDetails.DataBind();
    }
}

 
