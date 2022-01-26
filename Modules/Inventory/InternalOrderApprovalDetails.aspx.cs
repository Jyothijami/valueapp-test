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
using vllib;
public partial class Modules_Inventory_InternalOrderApprovalDetails : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            SalesOrderMaster_Fill();
            CompanyName_Fill();
            // Godown_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            //gvWorkOrderDetails.DataBind();
            SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
            tblWorkOrderDetails.Visible = false;

            SM.WorkOrder objWorkOrder = new SM.WorkOrder();

            if (objWorkOrder.WorkOrder_Select(Request.QueryString["WoNo"].ToString()) > 0)
            {

                tblWorkOrderDetails.Visible = true;
                ddlOrderAcceptance.SelectedValue = objWorkOrder.SOId;
                SM.SalesEnquiry.SalesEnquiryItemTypes123_Select(ddlOrderAcceptance.SelectedItem.Value, ddlItemType);
                
                ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                
            }
        }

    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "69");
        btnReserve.Enabled = up.add;
    }

    //#region Godown Name Fill
    //private void Godown_Fill()
    //{
    //    try
    //    {
    //        Masters.ItemMaster.Godown_select(ddlGodown);

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();

    //    }
    //}
    //#endregion

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);
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

    #region Sales Order Master Fill
    private void SalesOrderMaster_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_Select(ddlOrderAcceptance);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // SM.Dispose();
        }
    }
    #endregion

    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrder_Select(ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                txtOADate.Text = objSO.SODate;
                txtSpecification.Text = objSO.SODetId;
                //txtCST.Text = objSO.SOCSTax;

                //    //txtDeliveryDate.Text = objSO.SODelivery;
                //    ddlDeliveryMode.SelectedValue = objSO.DespmId;
                //    txtAdvanceAmt.Text = objSO.SOAdvanceAmt;
                //    lbtnAttachedFiles.Text = objSO.SOFiles;
                //    txtAccessories.Text = objSO.SOAccessories;
                //    txtExtraSpace.Text = objSO.SOExtraSpares;
                //    if (objSO.SOCSTax == "")
                //    {
                //        lblVATCSTNo.Text = "VAT";
                //        txtCST.Text = objSO.SOVAT;
                //    }
                //    else if (objSO.SOVAT == "")
                //    {
                //        lblVATCSTNo.Text = "CS Tax";
                //        txtCST.Text = objSO.SOCSTax;
                //    }
                gvItemDetails.DataBind();

                objSO.SalesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text = objSMCustomer.CustUnitName;
                }
                else if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text = "--";
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

            // SM.Dispose();
        }

    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemType.DataSourceID = "SqlDataSource2";
        ddlItemType.DataTextField = "ITEM_MODEL_NO";
        ddlItemType.DataValueField = "ITEM_CODE";
        ddlItemType.DataBind();
        ddlItemType_SelectedIndexChanged(sender, e);
    }
    protected void ddlBrandselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemType, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrandselect.SelectedItem.Value);

    }
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //btnItemRefresh_Click(sender, e);
            Godown_Fill();
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            // Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {

                // txtQuantityInHand.Text = objMaster.ItemQtyInHand;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                // txtSpecification.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                //txtSpecification.Text = objMaster.ItemSpec;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

            }


            if (objMaster.QTY_select(ddlItemType.SelectedItem.Value, ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                txtQuantity.Text = objMaster.quantity;
            }


            if (objMaster.QTYInHand_select(ddlItemType.SelectedItem.Value) > 0)
            {
                if (objMaster.itemquantity == "")
                {
                    txtQuantityInHand.Text = "0";
                }
                else
                {
                    txtQuantityInHand.Text = objMaster.itemquantity;
                }
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedItem.Value);


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // Masters.Dispose();
        }
    }


    #region Godown Name Fill
    private void Godown_Fill()
    {
        try
        {
            Masters.ItemMaster.Stockentry(ddlGodown, ddlItemType.SelectedItem.Value);
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

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("InternalOrderApproval.aspx");
        tblWorkOrderDetails.Visible = false;
    }
    protected void btnReserve_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.CheckingFormat objchkf = new SCM.CheckingFormat();
            SM.WorkOrder objWorkOrder = new SM.WorkOrder();

            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {


                objWorkOrder.ItemMasterReserveQty3_Update(Convert.ToInt32(gvrow.Cells[2].Text), Convert.ToInt16(gvrow.Cells[6].Text), Convert.ToInt16(gvrow.Cells[15].Text), Convert.ToInt32(ddlOrderAcceptance.SelectedItem.Value), Convert.ToInt16(gvrow.Cells[16].Text), Convert.ToInt16(gvrow.Cells[17].Text));
                objchkf.ReserveStock_Update(gvrow.Cells[2].Text, gvrow.Cells[6].Text, gvrow.Cells[16].Text, gvrow.Cells[17].Text, gvrow.Cells[15].Text);



            }

            MessageBox.Show(this, "Stock Reserved Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //SM.Dispose();
            tblWorkOrderDetails.Visible = false;
            //lblData.Text = "";
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable IndentProducts = new DataTable();

        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Color");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("ColorId");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("CPID");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("GDID");
        IndentProducts.Columns.Add(col);
        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvItemDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
                    {
                        DataRow drnew = IndentProducts.NewRow();
                        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
                        drnew["ModelNo"] = ddlItemType.SelectedItem.Text;
                        drnew["ItemName"] = txtModelName.Text;
                        drnew["UOM"] = txtItemUOM.Text;
                        drnew["Quantity"] = txtQuantity.Text;

                        drnew["Color"] = ddlColor.SelectedItem.Text;
                        drnew["ColorId"] = ddlColor.SelectedItem.Value;
                        drnew["CPID"] = ddlCompany.SelectedItem.Value;
                        drnew["GDID"] = ddlGodown.SelectedItem.Value;
                        if (ddlItemPriority.SelectedValue == "0")
                        {
                            drnew["Priority"] = "--";
                        }
                        else
                        {
                            drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
                        }
                        drnew["Brand"] = txtBrand.Text;

                        drnew["SuggestedParty"] = "---";



                        drnew["ReqFor"] = "---";

                        drnew["ReqDate"] = txtReqByDate.Text;
                        drnew["Specification"] = txtSpecification.Text;
                        if (txtBrand.Text == string.Empty)
                        {
                            drnew["Brand"] = "--";
                        }
                        else
                        {
                        }

                        if (txtSpecification.Text == string.Empty)
                        {
                            drnew["Specification"] = "--";
                        }
                        else
                        {
                            drnew["Specification"] = txtSpecification.Text;
                        }
                        IndentProducts.Rows.Add(drnew);

                    }
                    else
                    {
                        DataRow dr = IndentProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Priority"] = gvrow.Cells[7].Text;
                        dr["Brand"] = gvrow.Cells[8].Text;

                        dr["SuggestedParty"] = gvrow.Cells[10].Text;
                        dr["ReqFor"] = gvrow.Cells[9].Text;
                        dr["ReqDate"] = gvrow.Cells[11].Text;
                        dr["Specification"] = gvrow.Cells[12].Text;
                        dr["Color"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;
                        dr["CPID"] = gvrow.Cells[16].Text;
                        dr["GDID"] = gvrow.Cells[17].Text;

                        IndentProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    dr["SuggestedParty"] = gvrow.Cells[10].Text;
                    dr["ReqFor"] = gvrow.Cells[9].Text;
                    dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["CPID"] = gvrow.Cells[16].Text;
                    dr["GDID"] = gvrow.Cells[17].Text;

                    IndentProducts.Rows.Add(dr);
                }
            }
        }
        if (gvItemDetails.Rows.Count > 0)
        {
            if (gvItemDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItemDetails.Rows)
                {

                    if (gvrow.Cells[2].Text == ddlItemType.SelectedItem.Value)
                    {
                        gvItemDetails.DataSource = IndentProducts;
                        gvItemDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }

                }
            }

        }
        if (gvItemDetails.SelectedIndex == -1)
        {

            DataRow drnew = IndentProducts.NewRow();
            drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
            drnew["ModelNo"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = txtModelName.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQuantity.Text;
            if (ddlItemPriority.SelectedValue == "0")
            {
                drnew["Priority"] = "--";
            }
            else
            {
                drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
            }
            drnew["Brand"] = txtBrand.Text;

            drnew["SuggestedParty"] = "---";


            drnew["ReqFor"] = "---";

            //drnew["SuggestedParty"] = ddlSupplierName.SelectedItem.Text;
            //drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
            drnew["ReqDate"] = txtReqByDate.Text;
            drnew["Specification"] = txtSpecification.Text;

            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["CPID"] = ddlCompany.SelectedItem.Value;
            drnew["GDID"] = ddlGodown.SelectedItem.Value;

            if (txtBrand.Text == string.Empty)
            {
                drnew["Brand"] = "--";
            }
            else
            {
                drnew["Brand"] = txtBrand.Text;


            }
            if (txtSpecification.Text == string.Empty)
            {
                drnew["Specification"] = "--";
            }
            else
            {
                drnew["Specification"] = txtSpecification.Text;
            }

            IndentProducts.Rows.Add(drnew);
        }
        gvItemDetails.DataSource = IndentProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
        gvItemDetails.SelectedIndex = -1;
    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        // ddlItemType.SelectedValue = "0";
        txtModelName.Text = string.Empty;
        //ddlItemType.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtQuantityInHand.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        //txtBalanceQty.Text = string.Empty;
        ddlItemPriority.SelectedValue = "0";
        txtBrand.Text = string.Empty;
        ddlBrandselect.SelectedValue = "0";
        //txtRequiredFor.Text = string.Empty;
        //txtReqByDate.Text = string.Empty;
        //txtSpecification.Text = string.Empty;
        //txtColor.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        //ddlSupplierName.SelectedValue = "0";
        ddlColor.SelectedValue = "0";
        ddlCompany.SelectedValue = "0";
        ddlGodown.SelectedValue = "0";
    }
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Itemcodefill();

        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Color");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        IndentProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = IndentProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Priority"] = gvrow.Cells[7].Text;
                dr["Brand"] = gvrow.Cells[8].Text;
                dr["SuggestedParty"] = gvrow.Cells[9].Text;
                dr["ReqFor"] = gvrow.Cells[10].Text;
                dr["ReqDate"] = gvrow.Cells[11].Text;
                dr["Specification"] = gvrow.Cells[12].Text;

                dr["Color"] = gvrow.Cells[14].Text;
                dr["ColorId"] = gvrow.Cells[15].Text;


                IndentProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
                {

                    ddlItemType.SelectedItem.Value = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    ddlItemType.SelectedItem.Text = gvrow.Cells[3].Text;
                    ddlItemPriority.SelectedValue = gvrow.Cells[7].Text;
                    txtReqByDate.Text = gvrow.Cells[11].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    //txtItemRate.Text = gvrow.Cells[7].Text;
                    //txtRequiredFor.Text = gvrow.Cells[10].Text;
                    // ddlSupplierName.SelectedItem.Text = gvrow.Cells[9].Text;
                    txtSpecification.Text = gvrow.Cells[12].Text;
                    //ddlColor.SelectedValue = gvrow.Cells[14].Text;
                    ddlColor.SelectedItem.Text = gvrow.Cells[14].Text;

                    gvItemDetails.SelectedIndex = gvrow.RowIndex;

                }
            }
        }
    }

    private void Itemcodefill()
    {
        throw new Exception("The method or operation is not implemented.");
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Color");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("CPID");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("GDID");
        IndentProducts.Columns.Add(col);
        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    dr["SuggestedParty"] = gvrow.Cells[9].Text;
                    dr["ReqFor"] = gvrow.Cells[10].Text;
                    dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["CPID"] = gvrow.Cells[16].Text;
                    dr["GDID"] = gvrow.Cells[17].Text;

                    IndentProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = IndentProducts;
        gvItemDetails.DataBind();
    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
    }
    protected void gvWorkOrderDetails_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    protected void gvOrderAcceptanceItems_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            if (e.Row.Cells[16].Text == "True")
            {
                CheckBox ch;
                ch = (CheckBox)e.Row.FindControl("chk");
                ch.Checked = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (btnSave.Text == "Save")
            //{
            e.Row.Cells[0].Visible = false;
            //    btnReserve.Visible = false;
            //}
            //  else { e.Row.Cells[0].Visible = true; btnReserve.Visible = true; }

            e.Row.Cells[8].Visible = e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
    }
    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster objMaster = new Masters.ItemMaster();
        if (objMaster.StockEntrychange_select(ddlItemType.SelectedItem.Value, ddlGodown.SelectedItem.Value, ddlColor.SelectedValue) > 0)
        {
            txtQuantityInHand.Text = objMaster.itemquantity;
        }
        else
        {
            txtQuantityInHand.Text = "0";
        }
        //Masters.ItemMaster objMaster = new Masters.ItemMaster();
        //if (objMaster.QTYInHand_select(ddlItemType.SelectedItem.Value, ddlColor.SelectedItem.Value) > 0)
        //{
        //    if (objMaster.itemquantity == "")
        //    {
        //        txtQuantityInHand.Text = "0";
        //    }
        //    else
        //    {
        //        txtQuantityInHand.Text = objMaster.itemquantity;
        //    }
        //}
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.Stockentry1(ddlGodown, ddlItemType.SelectedItem.Value, ddlCompany.SelectedItem.Value);
        ddlGodown_SelectedIndexChanged2(sender, e);
        //Masters.ItemMaster.Stockentry12(ddlGodown, ddlCompany.SelectedItem.Value);
    }


    protected void ddlGodown_SelectedIndexChanged2(object sender, EventArgs e)
    {
        Masters.ItemMaster objMaster = new Masters.ItemMaster();
        if (objMaster.StockEntrychange_select(ddlItemType.SelectedItem.Value, ddlGodown.SelectedItem.Value, ddlColor.SelectedValue) > 0)
        {
            txtQuantityInHand.Text = objMaster.itemquantity;
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvWorkOrderDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvWorkOrderDetails.DataBind();
    }
}
 
