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
public partial class Modules_SCM_QuotationNew : basePage
{
    decimal TotalAmount = 0;
    string invoiceId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

        invoiceId = Request.QueryString["invoiceId"];

        lblCPID.Text = cp.getPresentCompanySessionValue();
        lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

        txtQunatity.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtDiscount.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtSpPrice.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");
        rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        if(!IsPostBack)
        {
            if (invoiceId == null)
            {
                // gvSupQuotationDetails.SelectedIndex = -1;
                //btnDelete.Attributes.Clear();
                SCM.ClearControls(this);
                txtQuotationNo.Text = SCM.SuppliersQuotation.SuppliersQuotation_AutoGenCode();
                txtQuotationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tblSupQuotationDetails.Visible = true;
                gvEnquiryProducts.DataBind();
                gvProductDetails.DataBind();
                btnSave.Text = "Save";
                btnSave.Enabled = true;
            }
            else
            {
                try
                {
                    SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

                    if (objSupQuot.SuppliersQuotation_Select(invoiceId) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblSupQuotationDetails.Visible = true;
                        txtQuotationNo.Text = objSupQuot.SupQuotNo;
                        txtQuotationDate.Text = objSupQuot.SupQuotDate;
                        //txtEnquiryDate.Text = objSupQuot.SuppEnqDate;
                        ddlSupplierName.SelectedValue = objSupQuot.SupId;
                        ddlSupplierName_SelectedIndexChanged(sender, e);
                        ddlEnquiryNo.SelectedValue = objSupQuot.SupEnqId;
                        ddlPOType.SelectedValue = objSupQuot.SupQuotPOType;
                        ddlTransport.SelectedValue = objSupQuot.TransId;
                        txtDelivery.Text = objSupQuot.SupDelivery;
                        txtPackingCharges.Text = objSupQuot.SupPackingCharges;
                        txtPaymentTerms.Text = objSupQuot.SupPaymentTerms;
                        txtGuarantee.Text = objSupQuot.SupGuarantee;
                        txtValidity.Text = objSupQuot.SupValidity;
                        txtInsurance.Text = objSupQuot.SupInsurance;
                        txtTransCharges.Text = objSupQuot.SupTransportationCharges;
                        txtFOB.Text = objSupQuot.SupFOB;
                        txtCIF.Text = objSupQuot.SupCIF;
                        ddlDespatchMode.SelectedValue = objSupQuot.SupDepatchMode;
                        txtOtherSpecs.Text = objSupQuot.SupOtherSpec;
                        txtDisc.Text = objSupQuot.SupDiscount;
                        txtSubTotal.Text = objSupQuot.SupExworks;

                        if (objSupQuot.SupVat != "")
                        {
                            txtVAT.Text = objSupQuot.SupVat;
                            lblVATCST.Text = "VAT";
                            rbVAT.Checked = true;
                            rbCST.Checked = false;
                            rbincluding.Checked = false;

                        }
                        else if (objSupQuot.SupCST != "")
                        {
                            txtVAT.Text = objSupQuot.SupCST;
                            lblVATCST.Text = "C.S. Tax";
                            rbCST.Checked = true;
                            rbVAT.Checked = false;
                            rbincluding.Checked = false;

                        }
                        else if (objSupQuot.SupIncluding != "")
                        {
                            txtVAT.Text = objSupQuot.SupIncluding;
                            rbincluding.Checked = true;
                            rbCST.Checked = false;
                            rbVAT.Checked = false;

                            lblVATCST.Text = "Including VAT";

                        }

                        // cblStatus.SelectedValue = objSupQuot.SupQuotSTC;

                        objSupQuot.SuppliersQuotationDetails_Select(invoiceId, gvProductDetails);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    // btnDelete.Attributes.Clear();
                    SCM.Dispose();
                }
            }

            SM.DDLBindWithSelect(ddlRate, "SELECT CURRENCY_ID,CURRENCY_NAME FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null");
            SM.DDLBindWithSelect(ddlDespatchMode, "SELECT DESPM_ID ,DESPM_NAME FROM [YANTRA_LKUP_DESP_MODE] ORDER BY DESPM_NAME");
            SM.DDLBindWithSelect(ddlTransport, "SELECT TRANS_LONG_NAME,TRANS_ID FROM [YANTRA_LKUP_TRANS_MAST] ORDER BY TRANS_LONG_NAME");
            SM.DDLBindWithSelect(ddlSupplierName, "SELECT SUP_ID,SUP_NAME FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_NAME");
            SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "22");
        
        //btnGo.Enabled = up.Go;   
        //btnAdd.Enabled = up.add;
        //btnItemsRefresh.Enabled = up.ItemRefresh;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        //btnPrint.Enabled = up.Print;
        //btnPurchase.Enabled = up.Purchase;
    }

    private void LoadInvoiceData()
    {
        //try
        //{
        //    SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

        //    if (objSupQuot.SuppliersQuotation_Select(invoiceId) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = true;
        //        tblSupQuotationDetails.Visible = true;
        //        txtQuotationNo.Text = objSupQuot.SupQuotNo;
        //        txtQuotationDate.Text = objSupQuot.SupQuotDate;
        //        txtEnquiryDate.Text = objSupQuot.SuppEnqDate;
        //        ddlSupplierName.SelectedValue = objSupQuot.SupId;
        //        ddlSupplierName_SelectedIndexChanged(sender, e);
        //        ddlEnquiryNo.SelectedValue = objSupQuot.SupEnqId;
        //        ddlPOType.SelectedValue = objSupQuot.SupQuotPOType;
        //        ddlTransport.SelectedValue = objSupQuot.TransId;
        //        txtDelivery.Text = objSupQuot.SupDelivery;
        //        txtPackingCharges.Text = objSupQuot.SupPackingCharges;
        //        txtPaymentTerms.Text = objSupQuot.SupPaymentTerms;
        //        txtGuarantee.Text = objSupQuot.SupGuarantee;
        //        txtValidity.Text = objSupQuot.SupValidity;
        //        txtInsurance.Text = objSupQuot.SupInsurance;
        //        txtTransCharges.Text = objSupQuot.SupTransportationCharges;
        //        txtFOB.Text = objSupQuot.SupFOB;
        //        txtCIF.Text = objSupQuot.SupCIF;
        //        ddlDespatchMode.SelectedValue = objSupQuot.SupDepatchMode;
        //        txtOtherSpecs.Text = objSupQuot.SupOtherSpec;
        //        txtDisc.Text = objSupQuot.SupDiscount;
        //        txtSubTotal.Text = objSupQuot.SupExworks;

        //        if (objSupQuot.SupVat != "")
        //        {
        //            txtVAT.Text = objSupQuot.SupVat;
        //            lblVATCST.Text = "VAT";
        //            rbVAT.Checked = true;
        //            rbCST.Checked = false;
        //            rbincluding.Checked = false;

        //        }
        //        else if (objSupQuot.SupCST != "")
        //        {
        //            txtVAT.Text = objSupQuot.SupCST;
        //            lblVATCST.Text = "C.S. Tax";
        //            rbCST.Checked = true;
        //            rbVAT.Checked = false;
        //            rbincluding.Checked = false;

        //        }
        //        else if (objSupQuot.SupIncluding != "")
        //        {
        //            txtVAT.Text = objSupQuot.SupIncluding;
        //            rbincluding.Checked = true;
        //            rbCST.Checked = false;
        //            rbVAT.Checked = false;

        //            lblVATCST.Text = "Including VAT";

        //        }

        //        // cblStatus.SelectedValue = objSupQuot.SupQuotSTC;

        //        objSupQuot.SuppliersQuotationDetails_Select(invoiceId, gvProductDetails);

               
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //   // btnDelete.Attributes.Clear();
        //    SCM.Dispose();
        //}
    }

    #region Supplier Fill
    private void Supplier_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    #endregion

    #region ItemCode Fill
    private void ItemCodes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemCode);
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
    #endregion

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDespatchMode);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();
        }
    }
    #endregion

    #region  Transport Fill
    private void Transport_Fill()
    {
        try
        {
            Masters.TrasnporterMaster.TransporterMaster_Select(ddlTransport);
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
    #endregion

    #region Enquiry Master Fill
    private void EnquiryMaster_Fill()
    {
        try
        {
            SCM.SuppliersEnquiry.SuppliersEnquiryMaster_Select(ddlEnquiryNo, ddlSupplierName.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //  SCM.Dispose();
        }
    }
    #endregion

    #region ddlSupplierName_SelectedIndexChanged
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SCM.SuppliersMaster objSCMSuppliers = new SCM.SuppliersMaster();
            if (objSCMSuppliers.SuppliersMaster_Select(ddlSupplierName.SelectedItem.Value) > 0)
            {
                txtAddress.Text = objSCMSuppliers.SupAddress;
                txtEmail.Text = objSCMSuppliers.SupEmail;
                txtPhone.Text = objSCMSuppliers.SupPhone;
                txtMobile.Text = objSCMSuppliers.SupMobile;

            }
            EnquiryMaster_Fill();

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //btnDelete.Attributes.Clear();
            //SCM.Dispose();
        }
    }
    #endregion

    #region Enquiry No. Select Index Changed
    protected void ddlEnquiryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SuppliersEnquiry objSupEnq = new SCM.SuppliersEnquiry();

            if (objSupEnq.SuppliersEnquiryMaster_Select(ddlEnquiryNo.SelectedItem.Value) > 0)
            {
                txtEnquiryDate.Text = objSupEnq.SuppEnqDate;

                objSupEnq.SuppliersEnquiryDetails_Select(ddlEnquiryNo.SelectedItem.Value, gvEnquiryProducts);
                Masters.ItemMaster.PurchaseQuotationItemTypesPerformaInvoice_Select(ddlEnquiryNo.SelectedItem.Value, ddlItemCode);
                //  Supplier_Fill(objSupEnq.SuppEnqId);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
           // btnDelete.Attributes.Clear();
            // SCM.Dispose();
        }

    }
    #endregion
    protected void gvEnquiryProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);

        if (gvEnquiryProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvEnquiryProducts.Rows)
            {
                DataRow dr = IndentProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["ItemType"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[7].Text;
                dr["Priority"] = gvrow.Cells[9].Text;
                dr["Brand"] = gvrow.Cells[6].Text;
                dr["Specification"] = gvrow.Cells[8].Text;


                IndentProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvEnquiryProducts.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemCode.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemCode_SelectedIndexChanged(sender, e);
                    txtQunatity.Text = gvrow.Cells[7].Text;
                    txtSpecifications.Text = gvrow.Cells[8].Text;
                    gvEnquiryProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
    }
    protected void gvEnquiryProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvEnquiryProducts.Rows[e.RowIndex].Cells[1].Text;
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);

        if (gvEnquiryProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvEnquiryProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["ItemType"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[7].Text;
                    dr["Priority"] = gvrow.Cells[9].Text;
                    dr["Brand"] = gvrow.Cells[6].Text;
                    dr["Specification"] = gvrow.Cells[8].Text;

                    IndentProducts.Rows.Add(dr);
                }
            }
        }
        gvEnquiryProducts.DataSource = IndentProducts;
        gvEnquiryProducts.DataBind();
        MessageBox.Show(this, "Deleted Successfully");
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemCode, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value);

    }

    #region ddlItemCode_SelectedIndexChanged
    protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemCode.SelectedItem.Value) > 0)
            {
                txtUOM.Text = objMaster.ItemUOMShort;
                txtSpecifications.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;

                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
               // Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemCode.SelectedItem.Value + "";
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(ddlItemCode.SelectedItem.Value) > 0)
                {
                    txtOldRate.Text = objrate.rsp;
                }
                txtRate.Text = string.Empty;
                txtQunatity.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtSpPrice.Text = string.Empty;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemCode.SelectedValue);

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

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable SuppliersQuotationItems = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("ItemCode");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("ItemName");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("ModelName");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("UOM");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("Quantity");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("Brand");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("Specification");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("Rate");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("SpRate");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("Curency");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("CurencyId");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("Disc");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("oldrate");
            SuppliersQuotationItems.Columns.Add(col);
            col = new DataColumn("EnqNo");
            SuppliersQuotationItems.Columns.Add(col);

            if (gvProductDetails.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    DataRow dr = SuppliersQuotationItems.NewRow();

                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["ModelName"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["Quantity"] = gvrow.Cells[5].Text;
                    dr["Brand"] = gvrow.Cells[6].Text;
                    dr["Curency"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["Disc"] = gvrow.Cells[10].Text;
                    dr["SpRate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["CurencyId"] = gvrow.Cells[13].Text;
                    dr["oldrate"] = gvrow.Cells[14].Text;
                    dr["EnqNo"] = gvrow.Cells[15].Text;
                    SuppliersQuotationItems.Rows.Add(dr);
                }
            }

            if (gvProductDetails.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    if (gvrow.Cells[1].Text == ddlItemCode.SelectedItem.Value)
                    {
                        if (gvrow.Cells[15].Text == ddlEnquiryNo.SelectedItem.Value)
                        {
                            //  gvrow.Cells[5].Text = Convert.ToString((Convert.ToInt16(txtQunatity.Text) + Convert.ToInt16(gvrow.Cells[5].Text)));
                            gvProductDetails.DataSource = SuppliersQuotationItems;
                            gvProductDetails.DataBind();
                            MessageBox.Show(this, "The Item Code With Enq No you have selected is already exists in list");
                            return;
                        }
                        //else
                        //{
                        //    gvrow.Cells[5].Text = Convert.ToString((Convert.ToInt16(txtQunatity.Text) + Convert.ToInt16(gvrow.Cells[5].Text)));
                        //}


                    }
                }
            }

            DataRow drnew = SuppliersQuotationItems.NewRow();
            drnew["ItemCode"] = ddlItemCode.SelectedItem.Value;
            drnew["ItemName"] = ddlItemCode.SelectedItem.Text;
            drnew["ModelName"] = txtItemName.Text;
            drnew["UOM"] = txtUOM.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["Quantity"] = txtQunatity.Text;
            drnew["Disc"] = txtDiscount.Text;
            drnew["Brand"] = txtBrand.Text;
            drnew["SpRate"] = txtSpPrice.Text;
            drnew["Curency"] = ddlRate.SelectedItem.Text;
            drnew["CurencyId"] = ddlRate.SelectedItem.Value;
            drnew["Specification"] = txtSpecifications.Text;
            drnew["oldrate"] = txtOldRate.Text;
            drnew["EnqNo"] = ddlEnquiryNo.SelectedItem.Value;
            SuppliersQuotationItems.Rows.Add(drnew);

            gvProductDetails.DataSource = SuppliersQuotationItems;
            gvProductDetails.DataBind();
            btnItemsRefresh_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    #endregion

    #region Button ITEM REFRESH Click
    protected void btnItemsRefresh_Click(object sender, EventArgs e)
    {
        ddlItemCode.SelectedValue = "0";
        txtUOM.Text = string.Empty;
        txtRate.Text = string.Empty;
        // ddlItemCode.SelectedItem.Text = "0";
        txtQunatity.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtSpPrice.Text = string.Empty;
        txtDiscount.Text = string.Empty;
        ddlColor.SelectedValue = "0";
        txtSpecifications.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        txtItemCategory.Text = string.Empty;
        ddlRate.SelectedValue = "0";
        txtBrand.Text = string.Empty;
        txtOldRate.Text = string.Empty;
    }
    #endregion
    
    #region gvProductDetails_RowDataBound
    protected void gvProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[9].Text = (Convert.ToDecimal(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[5].Text)).ToString();
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = "TotalAmount:";
            e.Row.Cells[11].Text = TotalAmount.ToString();

            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
    }
    #endregion
    
    #region gvProductDetails_RowDeleting
    protected void gvProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvProductDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable SuppliersQuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("ModelName");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Brand");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Specification");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("SpRate");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Curency");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("CurencyId");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("Disc");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("EnqNo");
        SuppliersQuotationItems.Columns.Add(col);
        col = new DataColumn("oldrate");
        SuppliersQuotationItems.Columns.Add(col);


        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SuppliersQuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["ModelName"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["Quantity"] = gvrow.Cells[5].Text;
                    dr["Brand"] = gvrow.Cells[6].Text;
                    dr["Curency"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["SpRate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["CurencyId"] = gvrow.Cells[13].Text;
                    dr["Disc"] = gvrow.Cells[10].Text;
                    dr["EnqNo"] = gvrow.Cells[15].Text;
                    dr["oldrate"] = gvrow.Cells[15].Text;
                    SuppliersQuotationItems.Rows.Add(dr);
                }
            }
        }
        gvProductDetails.DataSource = SuppliersQuotationItems;
        gvProductDetails.DataBind();

    }
    #endregion
    #region Button SAVE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SuppliersQuotationSave();
        }
        else if (btnSave.Text == "Update")
        {
            SuppliersQuotationUpdate();
        }
    }
    #endregion

    #region SuppliersQuotationSave
    private void SuppliersQuotationSave()
    {
        if (gvProductDetails.Rows.Count > 0)
        {
            try
            {
                SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

                SCM.BeginTransaction();

                objSupQuot.SupQuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objSupQuot.SupId = ddlSupplierName.SelectedItem.Value;
                objSupQuot.SupQuotPOType = ddlPOType.SelectedItem.Value;
                objSupQuot.TransId = "1";
                //objSupQuot.SupQuotSTC = cblStatus.SelectedItem.Value;
                objSupQuot.SupEnqId = ddlEnquiryNo.SelectedItem.Value;
                objSupQuot.SupCIF = txtCIF.Text;
                objSupQuot.SupDelivery = txtDelivery.Text;
                objSupQuot.SupDepatchMode = ddlDespatchMode.SelectedValue;
                objSupQuot.SupFOB = txtFOB.Text;
                objSupQuot.SupGuarantee = txtGuarantee.Text;
                objSupQuot.SupInsurance = txtInsurance.Text;
                objSupQuot.SupPackingCharges = txtPackingCharges.Text;
                objSupQuot.SupPaymentTerms = txtPaymentTerms.Text;
                objSupQuot.SupTransportationCharges = txtTransCharges.Text;
                objSupQuot.SupValidity = txtValidity.Text;
                objSupQuot.SupOtherSpec = txtOtherSpecs.Text;
                objSupQuot.SupExworks = txtSubTotal.Text;
                objSupQuot.SupDiscount = txtDisc.Text;
                objSupQuot.CpId = lblCPID.Text;
                if (rbVAT.Checked == true)
                { objSupQuot.SupVat = txtVAT.Text; }
                else if (rbCST.Checked == true)
                { objSupQuot.SupCST = txtVAT.Text; }
                else if (rbincluding.Checked == true)
                { objSupQuot.SupIncluding = txtVAT.Text; }



                if (objSupQuot.SuppliersQuotation_Save() == "Data Saved Successfully")
                {
                    objSupQuot.SuppliersQuotationDetails_Delete(objSupQuot.SupQuotId);
                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {

                        objSupQuot.ItemCode = gvrow.Cells[1].Text;
                        //objSupQuot.UOM = gvrow.Cells[3].Text;
                        objSupQuot.SupQuotDetRate = gvrow.Cells[8].Text;
                        objSupQuot.SupQuotDetCurrency = gvrow.Cells[13].Text;
                        objSupQuot.SupSpPrice = gvrow.Cells[11].Text;
                        objSupQuot.SupQty = gvrow.Cells[5].Text;
                        objSupQuot.SupQuotDetDisRate = gvrow.Cells[10].Text;
                        objSupQuot.SupQuotDetSpecs = gvrow.Cells[12].Text;
                        objSupQuot.SupQuotDetTax = "1";
                        objSupQuot.SupQuotDetPFRate = "1";
                        objSupQuot.SupQuotDetExciseRate = "1";
                        objSupQuot.SupQuotDetLandedCost = "1";
                        objSupQuot.SupQuotDetOldRate = gvrow.Cells[14].Text;
                        objSupQuot.SupQuotDetEnqid = gvrow.Cells[15].Text;
                        SCM.SuppliersQuotation.SupEnqStatus_Update(SCM.SCMStatus.Closed, gvrow.Cells[15].Text);
                        //objSupQuot.SupQuotDetPFRate = "1";
                        objSupQuot.SuppliersQuotationDetails_Save();

                    }



                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    //SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblSupQuotationDetails.Visible = false;
                gvProductDetails.DataBind();
               // btnDelete.Attributes.Clear();
                //gvSupQuotationDetails.DataBind();
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    #endregion

    #region SuppliersQuotationUpdate
    private void SuppliersQuotationUpdate()
    {
        if (gvProductDetails.Rows.Count > 0)
        {
            try
            {

                SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

                SCM.BeginTransaction();

                objSupQuot.SupQuotId =invoiceId;

                objSupQuot.SupQuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objSupQuot.SupId = ddlSupplierName.SelectedItem.Value;
                objSupQuot.SupQuotPOType = ddlPOType.SelectedItem.Value;
                objSupQuot.TransId = "1";
                //  objSupQuot.SupQuotSTC = cblStatus.SelectedItem.Value;
                objSupQuot.SupEnqId = ddlEnquiryNo.SelectedItem.Value;
                objSupQuot.SupCIF = txtCIF.Text;
                objSupQuot.SupDelivery = txtDelivery.Text;
                objSupQuot.SupDepatchMode = ddlDespatchMode.SelectedValue;
                objSupQuot.SupFOB = txtFOB.Text;
                objSupQuot.SupGuarantee = txtGuarantee.Text;
                objSupQuot.SupInsurance = txtInsurance.Text;
                objSupQuot.SupPackingCharges = txtPackingCharges.Text;
                objSupQuot.SupPaymentTerms = txtPaymentTerms.Text;
                objSupQuot.SupTransportationCharges = txtTransCharges.Text;
                objSupQuot.SupValidity = txtValidity.Text;
                objSupQuot.SupOtherSpec = txtOtherSpecs.Text;
                if (rbVAT.Checked == true)
                { objSupQuot.SupVat = txtVAT.Text; }
                else if (rbCST.Checked == true)
                { objSupQuot.SupCST = txtVAT.Text; }
                else if (rbincluding.Checked == true)
                { objSupQuot.SupIncluding = txtVAT.Text; }
                objSupQuot.SupExworks = txtSubTotal.Text;
                objSupQuot.SupDiscount = txtDisc.Text;
                objSupQuot.CpId = lblCPID.Text;

                if (objSupQuot.SuppliersQuotation_Update() == "Data Updated Successfully")
                {
                    objSupQuot.SuppliersQuotationDetails_Delete(objSupQuot.SupQuotId);
                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {
                        objSupQuot.ItemCode = gvrow.Cells[1].Text;
                        //objSupQuot.UOM = gvrow.Cells[3].Text;
                        objSupQuot.SupQuotDetRate = gvrow.Cells[8].Text;
                        objSupQuot.SupQuotDetCurrency = gvrow.Cells[13].Text;
                        objSupQuot.SupSpPrice = gvrow.Cells[11].Text;
                        objSupQuot.SupQty = gvrow.Cells[5].Text;
                        objSupQuot.SupQuotDetDisRate = gvrow.Cells[10].Text;
                        objSupQuot.SupQuotDetSpecs = gvrow.Cells[12].Text;
                        objSupQuot.SupQuotDetTax = "1";
                        objSupQuot.SupQuotDetPFRate = "1";
                        objSupQuot.SupQuotDetExciseRate = "1";
                        objSupQuot.SupQuotDetLandedCost = "1";
                        objSupQuot.SupQuotDetOldRate = gvrow.Cells[14].Text;



                        objSupQuot.SuppliersQuotationDetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    // SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblSupQuotationDetails.Visible = false;
                gvProductDetails.DataBind();
               // btnDelete.Attributes.Clear();
                //gvSupQuotationDetails.DataBind();
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        gvProductDetails.DataBind();
    }
    #endregion
  
    protected void btnPurchase_Click(object sender, EventArgs e)
    {
        if (invoiceId!=null)
        {
            Response.Redirect("FixedPurchaseOrderDetails.aspx?QId=" + invoiceId + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quotation.aspx");
    }
}
 
