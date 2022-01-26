using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class MASTERS_ItemEdit : basePage
{   
    string CD = DateTime.Now.ToString("MM/dd/yyyy");
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        //string Qid = Request.QueryString["Cid"].ToString();
        if (!IsPostBack)
        {
            BrandFill();
            ItemCategoryFill();
            UOMFill();
            string Qid = Request.QueryString["Cid"].ToString();
            lblItemCode.Text = Request.QueryString["Cid"].ToString();
            Itemfill();
            DataList1.DataBind();
            DataList2.DataBind();
            setControlsVisibility();

            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);

        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "3");
        btnSave.Enabled = up.add;
        

    }

    private void Itemfill()
    {
        Masters.ItemMaster objmaster = new Masters.ItemMaster();
        if (objmaster.ItemMaster_Select(Request.QueryString["Cid"].ToString()) > 0)
                {
                    txtItemName.Text = objmaster.ItemName;
                    ddlUom.SelectedValue = objmaster.Uomid;
                    txtItemSpecification.Text = objmaster.ItemSpec;
                    txtMaterialtype.Text = objmaster.Materialtype;
                    txtPrincipalName.Text = objmaster.Principalname;
                    txtitemseries.Text = objmaster.Itemseries;
                    txtPurchaseSpecification.Text = objmaster.Purchasespec;
                    txtItemModelNo.Text = objmaster.ModelNo;

                    ddlItemCategory.SelectedValue = objmaster.IcId;
                    ddlItemCategory_SelectedIndexChanged(new object(),new System.EventArgs());
                    ddlBrand.SelectedValue = objmaster.Brandid;
                    ddlBrand_SelectedIndexChanged(new object(), new System.EventArgs());

                    ddlItemSubCategory.SelectedValue = objmaster.ItemtypeId;

                    txtItemTAX.Text = objmaster.GST_Tax;
                    txtRemarks.Text = objmaster.Remarks;
                    txtHSNCode.Text = objmaster.HSN_Code;

                    if (objmaster.F2 == "Discontinued") 
                    {
                        chkForDisc.Checked = true;
                        lblforChk.Text = "Discontinued";
                    }
                    else
                    {
                        chkForDisc.Checked = false;
                        lblforChk.Text = "";

                    }

                    DataTable dt = objmaster.ItemColor_Select(int.Parse(Request.QueryString["Cid"].ToString()));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListItem currentCheckBox = chkItemColor.Items.FindByValue(dt.Rows[i][0].ToString());
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.Selected = true;
                        }
                    }
                    objmaster.SpareDetails_Select(Request.QueryString["Cid"].ToString(), gvSp);
                }
               
            }
          
   

    private void UOMFill()
    {
        Masters.UnitMaster.UnitMaster_Select(ddlUom);
    }



    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlItemCategory);
    }

    private void BrandFill()
    {
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            Masters.ItemType.ItemTypeCategory_Select(ddlItemSubCategory, ddlItemCategory.SelectedValue);
        }
        catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
        finally { Masters.Dispose(); }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.CheckboxListWithStatement(chkItemColor, "select * from YANTRA_LKUP_COLOR_MAST where IC_ID = " + ddlItemCategory.SelectedItem.Value + " and   BRAND_ID = " + ddlBrand.SelectedItem.Value);

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        chkItemColor.ClearSelection();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            objMaster.ItemCode = Request.QueryString["Cid"].ToString();
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
            objMaster.F2 = lblforChk.Text;
            if (objMaster.ItemMaster_Update() == "Data Updated Successfully")
            {
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
                                objMaster.Item_Path = "http://183.82.108.55/Content/ItemImage/" + itemimage;
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
                                objMaster.Item_Path = "http://183.82.108.55/Content/ItemImage/" + itemimage;
                                objMaster.ItemImage_Save();
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }

                        }
                        #endregion
                    }
                    catch (Exception)
                    {
                        #region Catch

                        MessageBox.Show(this, "Due to Slow Connection No-Image was uploaded to this Model No, Please Check it Again");

                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage"))
                        {
                            objMaster.ItemImage = "7026_No Image.jpg";
                            objMaster.ItemDate = CD;
                            objMaster.Item_Path = "http://183.82.108.55/Content/ItemImage/7026_No Image.jpg";
                            objMaster.ItemImage_Save();
                        }

                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage");

                            objMaster.ItemImage = "7026_No Image.jpg";
                            objMaster.ItemDate = CD;
                            objMaster.Item_Path = "http://183.82.108.55/Content/ItemImage/7026_No Image.jpg";
                            objMaster.ItemImage_Save();
                        }
                        #endregion
                    }
                }
                //else
                //{
                //    #region No Image Selected

                //    MessageBox.Show(this, "Due to Technical reason No-Image was uploaded to this Model No, Please Check it Again");

                //    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage"))
                //    {
                //        objMaster.ItemImage = "7026_No Image.jpg";
                //        objMaster.ItemDate = CD;
                //        objMaster.Item_Path = "http://183.82.108.55/Content/ItemImage/7026_No Image.jpg";
                //        objMaster.ItemImage_Save();
                //    }

                //    else
                //    {
                //        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage");

                //        objMaster.ItemImage = "7026_No Image.jpg";
                //        objMaster.ItemDate = CD;
                //        objMaster.Item_Path = "http://183.82.108.55/Content/ItemImage/7026_No Image.jpg";
                //        objMaster.ItemImage_Save();
                //    }
                //    #endregion
                //}


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
                                objMaster.Item_Path = "http://183.82.108.55/Content/ItemDrawings/" + itemdrawing;
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
                                objMaster.Item_Path = "http://183.82.108.55/Content/ItemDrawings/" + itemdrawing;
                                objMaster.SpecImage_Save();
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }

                        }
                        #endregion
                    }
                    catch (Exception)
                    {
                        #region Catch
                        MessageBox.Show(this, "Due to Slow Connection No-Image was uploaded to this Model No, Please Check it Again");

                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings"))
                        {
                            objMaster.ItemSpec = "7649_No Image.png";
                            objMaster.Specdate = CD;
                            objMaster.Item_Path = "http://183.82.108.55/Content/ItemDrawings/7649_No Image.png";
                            objMaster.SpecImage_Save();

                        }

                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings");

                            objMaster.ItemSpec = "7649_No Image.png";
                            objMaster.Specdate = CD;
                            objMaster.Item_Path = "http://183.82.108.55/Content/ItemDrawings/7649_No Image.png";
                            objMaster.SpecImage_Save();
                        }
                        #endregion
                    }
                }
                if (SpareImages.HasFiles)
                {
                    try
                    {
                        #region Spare Images
                        string itemdrawing = "";
                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemAttachments"))
                        {
                            foreach (HttpPostedFile uploadedFile in SpareImages.PostedFiles)
                            {

                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/ItemAttachments/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                itemdrawing = randNumber + "_" + fileName;
                                objMaster.spImage = itemdrawing;
                                objMaster.Specdate = CD;
                                objMaster.Item_Path = "http://183.82.108.55/Content/ItemAttachments/" + itemdrawing;
                                objMaster.SpareImage_Save();
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }
                        }

                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemAttachments");
                            foreach (HttpPostedFile uploadedFile in SpareImages.PostedFiles)
                            {

                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/ItemAttachments/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                itemdrawing = randNumber + "_" + fileName;
                                objMaster.spImage = itemdrawing;
                                objMaster.Specdate = CD;
                                objMaster.Item_Path = "http://183.82.108.55/Content/ItemAttachments/" + itemdrawing;
                                objMaster.SpareImage_Save();
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);

                            }

                        }
                        #endregion
                    }
                    catch (Exception)
                    {
                        #region Catch
                        MessageBox.Show(this, "Due to Slow Connection No-Image was uploaded to this Model No, Please Check it Again");

                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings"))
                        {
                            objMaster.ItemSpec = "7649_No Image.png";
                            objMaster.Specdate = CD;
                            objMaster.Item_Path = "http://183.82.108.55/Content/ItemAttachments/7649_No Image.png";
                            objMaster.SpecImage_Save();

                        }

                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings");

                            objMaster.ItemSpec = "7649_No Image.png";
                            objMaster.Specdate = CD;
                            objMaster.Item_Path = "http://183.82.108.55/Content/ItemAttachments/7649_No Image.png";
                            objMaster.SpecImage_Save();
                        }
                        #endregion
                    }
                }
                //else
                //{
                //    #region No Image Selected

                //    MessageBox.Show(this, "Due to Technical reason No-Image was uploaded to this Model No, Please Check it Again");

                //    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings"))
                //    {
                //        objMaster.ItemSpec = "7649_No Image.png";
                //        objMaster.Specdate = CD;
                //        objMaster.Item_Path = "http://valuelineapp.com/Content/ItemDrawings/7649_No Image.png";
                //        objMaster.SpecImage_Save();

                //    }

                //    else
                //    {
                //        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings");
                //        objMaster.ItemSpec = "7649_No Image.png";
                //        objMaster.Specdate = CD;
                //        objMaster.Item_Path = "http://valuelineapp.com/Content/ItemDrawings/7649_No Image.png";
                //        objMaster.SpecImage_Save();
                //    }
                //    #endregion
                //}

                objMaster.ItemColorDetails_Delete(Request.QueryString["Cid"].ToString());
                for (int i = 0; i < chkItemColor.Items.Count; i++)
                {
                    if (chkItemColor.Items[i].Selected == true)
                    {
                        objMaster.titemcode = Convert.ToInt32(Request.QueryString["Cid"]);
                        objMaster.detailscolorid = chkItemColor.Items[i].Value;
                        objMaster.ItemColorDetails_Update();
                    }
                    else if (chkItemColor.Items[i].Selected != true)
                    {
                        objMaster.titemcode = Convert.ToInt32(Request.QueryString["Cid"]);
                        objMaster.detailscolorid = "0";
                        objMaster.ItemColorDetails_Update();

                    }
                }
                if (gvSp.Rows.Count > 0)
                {
                    objMaster.ItemCode = Convert.ToInt32(Request.QueryString["Cid"]).ToString ();
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
                //if (itemimages.HasFiles)
                //{
                //    #region Item Images

                //    string itemimage = "";
                //    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage"))
                //    {
                //        foreach (HttpPostedFile uploadedFile in itemimages.PostedFiles)
                //        {

                //            Random rand = new Random();
                //            string randNumber = Convert.ToString(rand.Next(0, 10000));
                //            string path = Server.MapPath("~/Content/ItemImage/");
                //            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                //            itemimage = randNumber + "_" + fileName;
                //            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                //            objMaster.ItemImage = itemimage;
                //            objMaster.ItemDate = CD;
                //            objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemimage;
                //            objMaster.ItemImage_Save();
                //        }
                //    }

                //    else
                //    {
                //        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemImage");
                //        foreach (HttpPostedFile uploadedFile in itemimages.PostedFiles)
                //        {

                //            Random rand = new Random();
                //            string randNumber = Convert.ToString(rand.Next(0, 10000));
                //            string path = Server.MapPath("~/Content/ItemImage/");
                //            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                //            itemimage = randNumber + "_" + fileName;
                //            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                //            objMaster.ItemImage = itemimage;
                //            objMaster.ItemDate = CD;
                //            objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemimage;
                //            objMaster.ItemImage_Save();
                //        }

                //    }
                //    #endregion
                //}


                //if (ItemDrawings.HasFiles)
                //{
                //    #region Item Drawings
                //    string itemdrawing = "";
                //    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings"))
                //    {
                //        foreach (HttpPostedFile uploadedFile in ItemDrawings.PostedFiles)
                //        {

                //            Random rand = new Random();
                //            string randNumber = Convert.ToString(rand.Next(0, 10000));
                //            string path = Server.MapPath("~/Content/ItemDrawings/");
                //            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                //            itemdrawing = randNumber + "_" + fileName;
                //            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                //            objMaster.ItemSpec = itemdrawing;
                //            objMaster.Specdate = CD;
                //            objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemdrawing;
                //            objMaster.SpecImage_Save();
                //        }
                //    }

                //    else
                //    {
                //        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ItemDrawings");
                //        foreach (HttpPostedFile uploadedFile in ItemDrawings.PostedFiles)
                //        {

                //            Random rand = new Random();
                //            string randNumber = Convert.ToString(rand.Next(0, 10000));
                //            string path = Server.MapPath("~/Content/ItemDrawings/");
                //            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                //            itemdrawing = randNumber + "_" + fileName;
                //            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                //            objMaster.ItemSpec = itemdrawing;
                //            objMaster.Specdate = CD;
                //            objMaster.Item_Path = "http://valuelineapp.com/Content/ItemImage/" + itemdrawing;
                //            objMaster.SpecImage_Save();
                //        }

                //    }
                //    #endregion
                //}
            }                
                Masters.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
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
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/MASTERS/ItemMasterDetails.aspx");
    }


    protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        
        string ItemImgID = DataList1.DataKeys[e.Item.ItemIndex].ToString();
        SqlCommand cmd = new SqlCommand("Delete from YANTRA_ITEM_IMAGE where Item_Image_Id=" + ItemImgID +" ", con);
        cmd.CommandType = CommandType.Text;

        con.Open();

        cmd.ExecuteNonQuery();

        con.Close();

        DataList1.EditItemIndex = -1;
        DataList1.DataBind();

    }
    protected void DataList2_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        string ItemImgID = DataList2.DataKeys[e.Item.ItemIndex].ToString();

        SqlCommand cmd = new SqlCommand("Delete from YANTRA_ITEM_SPECIFICATION_IMAGE where Item_Specification_Id=" + ItemImgID + " ", con);
        cmd.CommandType = CommandType.Text;

        con.Open();

        cmd.ExecuteNonQuery();

        con.Close();

        DataList2.EditItemIndex = -1;
        DataList2.DataBind();
    }
    protected void DataList3_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        string ItemImgID = DataList3.DataKeys[e.Item.ItemIndex].ToString();

        SqlCommand cmd = new SqlCommand("Delete from YANTRA_ITEM_ATTACHMENTS where Item_attachmentId=" + ItemImgID + " ", con);
        cmd.CommandType = CommandType.Text;

        con.Open();

        cmd.ExecuteNonQuery();

        con.Close();

        DataList3.EditItemIndex = -1;
        DataList3.DataBind();
    }
    protected void lnkSpareItem_Click(object sender, EventArgs e)
    {
        pnlSP.Visible = true;
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
                        dr["Item_SpareDisc"] = txtspDisc.Text;
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
    protected void chkForDisc_CheckedChanged(object sender, EventArgs e)
    {
        if (chkForDisc.Checked == true)
        {
            lblforChk.Text = "Discontinued";
        }
        else
        {
            lblforChk.Text = "";

        }
    }
}
 
