using Yantra.MessageBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using vllib;
using System.Data.SqlClient;
using System.Configuration;

public partial class MASTERS_ItemMaster : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string CD = DateTime.Now.ToString("MM/dd/yyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BrandFill();
            ItemCategoryFill();
            UOMFill();
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
        }
    }

    private void UOMFill()
    {
        Masters.UnitMaster.UnitMaster_Select(ddlUom);
        ddlUom.SelectedIndex = 5;
    }

   

    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlItemCategory);
    }

    private void BrandFill()
    {
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
       // Masters.ProductCompany.ProductCompany_Select(ddlPriceBrand);
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try { Masters.ItemType.ItemTypeCategory_Select(ddlItemSubCategory, ddlItemCategory.SelectedValue); }
        catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
        finally { Masters.Dispose(); }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.CheckboxListWithStatement(chkItemColor, "select * from YANTRA_LKUP_COLOR_MAST where IC_ID = " + ddlItemCategory.SelectedItem.Value + " and   BRAND_ID = " + ddlBrand.SelectedItem.Value);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        chkItemColor.ClearSelection();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/MASTERS/ItemMasterDetails.aspx");
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ItemMasterSave();
        }
        
    }

    //private void ItemMasterUpdate()
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        objMaster.ItemCode = gvItemMaster.SelectedRow.Cells[0].Text;
    //        objMaster.ItemName = txtItemName.Text;
    //        objMaster.ItemSpec = txtItemSpecification.Text;
    //        objMaster.Materialtype = txtMaterialtype.Text;
    //        objMaster.ItemtypeId = ddlItemSubCategory.SelectedItem.Value;
    //        objMaster.Uomid = ddlUom.SelectedItem.Value;
    //        objMaster.Principalname = txtPrincipalName.Text;
    //        objMaster.Itemseries = txtitemseries.Text;
    //        objMaster.Purchasespec = txtPurchaseSpecification.Text;
    //        objMaster.ModelNo = txtItemModelNo.Text;
    //        objMaster.IcId = ddlItemCategory.SelectedItem.Value;
    //        objMaster.Brandid = ddlBrand.SelectedItem.Value;
            

    //        if (objMaster.ItemMaster_Update() == "Data Updated Successfully")
    //        {
    //            objMaster.ItemColorDetails_Delete(gvItemMaster.SelectedRow.Cells[0].Text);
    //            for (int i = 0; i < chkItemColor.Items.Count; i++)
    //            {
    //                if (chkItemColor.Items[i].Selected == true)
    //                {

    //                    objMaster.detailscolorid = chkItemColor.Items[i].Value;
    //                    objMaster.ItemColorDetails_save();
    //                }
    //                else if (chkItemColor.Items[i].Selected != true)
    //                {
    //                    objMaster.detailscolorid = "0";
    //                    objMaster.ItemColorDetails_save();
    //                    return;
    //                }

    //            }
    //        }
    //        Masters.CommitTransaction();
    //        MessageBox.Show(this, "Data Updated Successfully");
    //        chkItemColor.ClearSelection();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        tblDetails.Visible = false;
    //        gvItemMaster.DataBind();
    //        Masters.ClearControls(this);
    //        Masters.Dispose();
    //    }
    //}

    private void ItemMasterSave()
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            objMaster.ItemName = txtItemName.Text;
            objMaster.ItemSpec = txtItemSpecification.Text;
            objMaster.Materialtype = txtMaterialtype.Text;
            objMaster.ItemtypeId = ddlItemSubCategory.SelectedItem.Value;
            objMaster.Uomid = ddlUom.SelectedItem.Value;
            objMaster.Principalname = txtPrincipalName.Text;
            objMaster.Itemseries = txtitemseries.Text;
            objMaster.Purchasespec = txtPurchaseSpecification.Text;
            objMaster.ModelNo = txtItemModelNo.Text;
            objMaster.IcId = ddlItemCategory.SelectedItem.Value;
            objMaster.Brandid = ddlBrand.SelectedItem.Value;

            objMaster.HSN_Code = txtHSNCode.Text;
            objMaster.Remarks = txtRemarks.Text;
            objMaster.GST_Tax = txtItemTAX.Text;
            objMaster.Prepared_By = lblEmpIdHidden.Text;

            if (objMaster.ItemMaster_Save() == "Data Saved Successfully")
            {
                for (int i = 0; i < chkItemColor.Items.Count; i++)
                {
                    if (chkItemColor.Items[i].Selected == true)
                    {

                        objMaster.detailscolorid = chkItemColor.Items[i].Value;
                        objMaster.ItemColorDetails_save();
                    }
                    else if (chkItemColor.Items[i].Selected != true)
                    {
                        objMaster.detailscolorid = "0";
                        objMaster.ItemColorDetails_save();
                       // return;
                    }

                }

                if (Uploadattach.HasFiles)
                {
                    #region Item Attachment
                    string Attachment = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemAttachments"))
                    {

                        foreach(HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ItemAttachments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            objMaster.Itemattachment = Attachment;
                            objMaster.attachmentdate = CD;
                            objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + Attachment;
                            objMaster.ItemAttachment_Save();

                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                        }
                        
                        // HttpFileCollection files = Request.Files;
                        //for (int i = 0; i < files.Count; i++)
                        //{
                        //    HttpPostedFile file = files[i];
                        //    if (file.ContentLength > 0)
                        //    {
                        //        Random rand = new Random();
                        //        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        //        string path = Server.MapPath("~/Content/ItemAttachments/");
                        //        string fileName = System.IO.Path.GetFileName(file.FileName);

                        //        Attachment = randNumber + "_" + fileName;
                        //        // now save the file to the disk
                        //        file.SaveAs(path + randNumber + "_" + fileName);

                        //        objMaster.Itemattachment = Attachment;
                        //        objMaster.attachmentdate = CD;
                        //        objMaster.ItemAttachment_Save();
                        //    }
                        //}
                    }
                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemAttachments");
                        foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ItemAttachments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            //uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            objMaster.Itemattachment = Attachment;
                            objMaster.attachmentdate = CD;
                            objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + Attachment;

                            objMaster.ItemAttachment_Save();

                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                        }

                    }

                    #endregion
                }


                if (itemimages.HasFiles)
                {
                    try
                    {
                        #region Item Images

                        string itemimage = "";
                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage"))
                        {
                            foreach (HttpPostedFile uploadedFile in itemimages.PostedFiles)
                            {

                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/ItemImage/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                itemimage = randNumber + "_" + fileName;

                                objMaster.ItemImage = itemimage;
                                objMaster.ItemDate = CD;
                                objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemimage;
                                objMaster.ItemImage_Save();

                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }
                        }

                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage");
                            foreach (HttpPostedFile uploadedFile in itemimages.PostedFiles)
                            {

                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/ItemImage/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                itemimage = randNumber + "_" + fileName;
                                objMaster.ItemImage = itemimage;
                                objMaster.ItemDate = CD;
                                objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemimage;

                                objMaster.ItemImage_Save();

                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }

                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }


                if (ItemDrawings.HasFiles)
                {
                    try
                    {
                        #region Item Drawings
                        string itemdrawing = "";
                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings"))
                        {
                            foreach (HttpPostedFile uploadedFile in ItemDrawings.PostedFiles)
                            {

                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/ItemDrawings/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                itemdrawing = randNumber + "_" + fileName;
                                objMaster.ItemSpec = itemdrawing;
                                objMaster.Specdate = CD;
                                objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemdrawing;

                                objMaster.SpecImage_Save();
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }
                        }

                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings");
                            foreach (HttpPostedFile uploadedFile in ItemDrawings.PostedFiles)
                            {

                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/ItemDrawings/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                itemdrawing = randNumber + "_" + fileName;
                                objMaster.ItemSpec = itemdrawing;
                                objMaster.Specdate = CD;
                                objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemdrawing;

                                objMaster.SpecImage_Save();
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }

                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }

                if (gvSp.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow in gvSp.Rows)
                    {
                        objMaster.ModelNo = gvrow.Cells[0].Text;
                        objMaster.SpModelNo = gvrow.Cells[1].Text;
                        objMaster.SpDisc = gvrow.Cells[2].Text;
                        objMaster.spImageId = "";
                        objMaster.spImage = "";
                        objMaster.Item_Path = "";
                        objMaster.Item_Spare_Save();
                    }
                }


            }
            
            Masters.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");
            chkItemColor.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {            
            Masters.ClearControls(this);
            Masters.Dispose();
            Response.Redirect("~/Modules/MASTERS/ItemMasterDetails.aspx");
        }
    }

    //protected void lbtnModelNo_Click(object sender, EventArgs e)
    //{
    //    tblDetails.Visible = false;
    //    LinkButton lbtnModelNo;
    //    lbtnModelNo = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lbtnModelNo.Parent.Parent;
    //    gvItemMaster.SelectedIndex = gvRow.RowIndex;
    //    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    //    if (gvItemMaster.SelectedIndex > -1)
    //    {
    //        tblDetails.Visible = true;
    //        Masters.ItemMaster objmaster = new Masters.ItemMaster();
    //        if (objmaster.ItemMaster_Select(gvItemMaster.SelectedRow.Cells[0].Text) > 0)
    //        {
    //            txtItemName.Text = objmaster.ItemName;
    //            ddlUom.SelectedValue = objmaster.Uomid;
    //            txtItemSpecification.Text = objmaster.ItemSpec;
    //            txtMaterialtype.Text = objmaster.Materialtype;
    //            txtPrincipalName.Text = objmaster.Principalname;
    //            txtitemseries.Text = objmaster.Itemseries;
    //            txtPurchaseSpecification.Text = objmaster.Purchasespec;
    //            txtItemModelNo.Text = objmaster.ModelNo;
               
    //            ddlItemCategory.SelectedValue = objmaster.IcId;
    //            ddlItemCategory_SelectedIndexChanged(sender, e);
    //            ddlBrand.SelectedValue = objmaster.Brandid;
    //            ddlBrand_SelectedIndexChanged(sender, e);
               
    //            ddlItemSubCategory.SelectedValue = objmaster.ItemtypeId;
    //            txtFinancialYear.Text = objmaster.financialyear;
    //            txtRoundPrice.Text = objmaster.rsp;
    //            txtRsp.Text = objmaster.rsp;
               
    //            DataTable dt = objmaster.ItemColor_Select(int.Parse(gvItemMaster.SelectedRow.Cells[0].Text));
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                ListItem currentCheckBox = chkItemColor.Items.FindByValue(dt.Rows[i][0].ToString());
    //                if (currentCheckBox != null)
    //                {
    //                    currentCheckBox.Selected = true;
    //                }
    //            }
    //        }
    //        btnSave.Text = "Update";
    //        btnSave.Enabled = false;
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    if (gvItemMaster.SelectedIndex > -1)
    //    {
    //        tblDetails.Visible = true;
    //        Masters.ItemMaster objmaster = new Masters.ItemMaster();
    //        if (objmaster.ItemMaster_Select(gvItemMaster.SelectedRow.Cells[0].Text) > 0)
    //        {
    //            txtItemName.Text = objmaster.ItemName;
    //            ddlUom.SelectedValue = objmaster.Uomid;
    //            txtItemSpecification.Text = objmaster.ItemSpec;
    //            txtMaterialtype.Text = objmaster.Materialtype;
    //            txtPrincipalName.Text = objmaster.Principalname;
    //            txtitemseries.Text = objmaster.Itemseries;
    //            txtPurchaseSpecification.Text = objmaster.Purchasespec;
    //            txtItemModelNo.Text = objmaster.ModelNo;

    //            ddlItemCategory.SelectedValue = objmaster.IcId;
    //            ddlItemCategory_SelectedIndexChanged(sender, e);
    //            ddlBrand.SelectedValue = objmaster.Brandid;
    //            ddlBrand_SelectedIndexChanged(sender, e);

    //            ddlItemSubCategory.SelectedValue = objmaster.ItemtypeId;
    //            txtFinancialYear.Text = objmaster.financialyear;
    //            txtRoundPrice.Text = objmaster.rsp;
    //            txtRsp.Text = objmaster.rsp;

    //            DataTable dt = objmaster.ItemColor_Select(int.Parse(gvItemMaster.SelectedRow.Cells[0].Text));
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                ListItem currentCheckBox = chkItemColor.Items.FindByValue(dt.Rows[i][0].ToString());
    //                if (currentCheckBox != null)
    //                {
    //                    currentCheckBox.Selected = true;
    //                }
    //            }
    //        }
    //    }

    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast one Record");
    //    }
    //}

    protected void txtItemModelNo_TextChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM YANTRA_ITEM_MAST where ITEM_MODEL_NO ='" + txtItemModelNo.Text + "' and STATUS =1 ", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        int count = Convert.ToInt32(dt.Rows[0][0].ToString());
        if (count > 0)
        {
            txtItemModelNo.Text = "";
            MessageBox.Show(this,"This Model No Already Exists");
        }
        else
        {
            Masters.ItemMaster objmaster = new Masters.ItemMaster();
            DataTable dt1 = objmaster.ItemSpare_Select(txtItemModelNo .Text );
            //txtItemSpecification.Text = dt1.Rows[2].ToString();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                lblChkArtical.Visible = true;
                chkArticlNo.Visible = true;
                Masters.CheckboxListWithStatement(chkArticlNo, "select item_spareDisc,item_model_no from YANTRA_item_spare_mast where item_SpareModelNo = '" + txtItemModelNo.Text + "' ");
            }
        }
    }
    protected void lnkSpareItem_Click(object sender, EventArgs e)
    {
        pnlSP .Visible =true;
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Item_Model_No");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Item_SpareModelNo");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Item_SpareDisc");
        SalesOrderItems.Columns.Add(col);
        if (gvSp.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSp.Rows)
            {
                if (gvSp.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvSp.SelectedRow.RowIndex)
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["Item_Model_No"] = txtItemModelNo.Text;
                        dr["Item_SpareModelNo"] = txtsp.Text;
                        dr["Item_SpareDisc"] = txtspDisc .Text ;
                        SalesOrderItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["Item_Model_No"] = gvrow.Cells[0].Text;
                        dr["Item_SpareModelNo"] = gvrow.Cells[1].Text;
                        dr["Item_SpareDisc"] = gvrow.Cells[2].Text;
                        SalesOrderItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["Item_Model_No"] = gvrow.Cells[0].Text;
                    dr["Item_SpareModelNo"] = gvrow.Cells[1].Text;
                    dr["Item_SpareDisc"] = gvrow.Cells[2].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        if (gvSp.SelectedIndex == -1)
        {
            DataRow drnew = SalesOrderItems.NewRow();
            drnew["Item_Model_No"] = txtItemModelNo.Text;
            drnew["Item_SpareModelNo"] = txtsp.Text;
            drnew["Item_SpareDisc"] = txtspDisc.Text;
            SalesOrderItems.Rows.Add(drnew);
        }
        gvSp.DataSource = SalesOrderItems;
        gvSp.DataBind();
        gvSp.SelectedIndex = -1;
        btnRefreshItems_Click(sender, e);
    }
    protected void btnRefreshItems_Click(object sender, EventArgs e)
    {
        txtsp.Text = string.Empty;
        txtspDisc.Text = string.Empty;
    }
    protected void chkArticlNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Message = "Artical Reference Codes are : ";
        foreach (ListItem item in chkArticlNo.Items)
        {
            if (item.Selected)
            {
                Message += ""+item.Text+", ";
                txtPurchaseSpecification.Text = Message;
                txtItemSpecification.Text = item.Value;
            }
        }
    }

    protected void gvSp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvSp.Rows[e.RowIndex].Cells[1].Text;
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Item_Model_No");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Item_SpareModelNo");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Item_SpareDisc");
        SalesOrderItems.Columns.Add(col);
        if (gvSp.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSp.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["Item_Model_No"] = gvrow.Cells[0].Text;
                    dr["Item_SpareModelNo"] = gvrow.Cells[1].Text;
                    dr["Item_SpareDisc"] = gvrow.Cells[2].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }
        gvSp.DataSource = SalesOrderItems;
        gvSp.DataBind();
    }
    protected void gvSp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton bts = e.CommandSource as ImageButton;
        if (e.CommandName.Equals("Save"))
        {
            int rowindex = int.Parse(e.CommandArgument.ToString().Trim());
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            FileUpload FileUpload1 = bts.Parent.Parent.FindControl("fileupload1") as FileUpload;
            if (FileUpload1.HasFile)
            {
                string itemimage = "";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemAttachments"))
                {

                    foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                    {

                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 99999));
                        string path = Server.MapPath("~/Content/ItemAttachments/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                        //string Itemcode = gvItemMaster.DataKeys[rowindex].Values["ITEM_CODE"].ToString().Trim();
                        string Itemcode = rowindex.ToString();
                        itemimage = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        obj.Emp_photo = itemimage;
                        obj.Item_Path = "http://183.82.108.55/Content/ItemImage/" + itemimage;
                        obj.SpareItem_img_update(Itemcode);

                    }
                }
            }
            gvSp.DataBind();
        }
    }
}
 
