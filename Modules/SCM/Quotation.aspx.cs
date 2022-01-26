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
using System.IO;
using YantraDAL;

public partial class Modules_PurchasingManagement_Quotation : basePage
{
    decimal TotalAmount = 0;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        lblCPID.Text = cp.getPresentCompanySessionValue();
        lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
 
        txtQunatity.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtDiscount.Attributes.Add("onkeyup", "javascript:amtcalc();");
        txtSpPrice.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");
        rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
        if (!IsPostBack)
        {
            setControlsVisibility();
                    
          //  Supplier_Fill();
            //EnquiryMaster_Fill();
           // ItemCodes_Fill();
          //  Transport_Fill();
           // RateType_Fill();
            //DeliveryType_Fill();
           // Supplier_Fill();


            SM.DDLBindWithSelect(ddlRate, "SELECT CURRENCY_ID,CURRENCY_NAME FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null");
            SM.DDLBindWithSelect(ddlDespatchMode, "SELECT DESPM_ID ,DESPM_NAME FROM [YANTRA_LKUP_DESP_MODE] ORDER BY DESPM_NAME");
            SM.DDLBindWithSelect(ddlTransport, "SELECT TRANS_LONG_NAME,TRANS_ID FROM [YANTRA_LKUP_TRANS_MAST] ORDER BY TRANS_LONG_NAME");
            SM.DDLBindWithSelect(ddlSupplierName, "SELECT SUP_ID,SUP_NAME FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_NAME");
            SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "22");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
       btnDelete.Enabled = up.Delete;
       //btnGo.Enabled = up.Go;   
       //btnAdd.Enabled = up.add;
       //btnItemsRefresh.Enabled = up.ItemRefresh;
       //btnSave.Enabled = up.Save;
       //btnRefresh.Enabled = up.Refresh;
       //btnClose.Enabled = up.Close;
       //btnPrint.Enabled = up.Print;
       //btnPurchase.Enabled = up.Purchase;
    }

    #region Enquiry Master Fill
    private void EnquiryMaster_Fill()
    {
        try
        {
         SCM.SuppliersEnquiry.SuppliersEnquiryMaster_Select2(ddlEnquiryNo,ddlSupplierName.SelectedItem.Value);
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

    //private void Currency_Fill()
    //{
    //    try
    //    {
    //        Masters.CurrencyType.CurrencyType_Select(ddlCurrencyType);
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

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {

        //Response.Redirect("QuotationNew.aspx");
        gvSupQuotationDetails.SelectedIndex = -1;
        btnDelete.Attributes.Clear();
        //SCM.ClearControls(this);
        txtQuotationNo.Text = SCM.SuppliersQuotation.SuppliersQuotation_AutoGenCode();
        txtQuotationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        tblSupQuotationDetails.Visible = true;
        tblButtons.Visible = true;
        gvApprlItemDetails.DataBind();
        gvProductDetails.DataBind();
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        clearfields();
      
    }

    private void clearfields()
    {
        lblTtlAmt.Text = string.Empty;
        lblsum.Text = string.Empty;
        UploadsRepeater.DataSourceID =null;
        UploadsRepeater.DataSource=null;
        UploadsRepeater.DataBind();
       
    }
    #endregion


    #region Link Button File Opener Click
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        LinkButton lbtnFileOpener;
        lbtnFileOpener = (LinkButton)sender;
        Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
        DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
        string command = "SELECT QUOT_ATTACHMENT FROM [YANTRA_SUP_QUOT_ATTACHMENTS] WHERE SUP_QUOT_ID=" + gvSupQuotationDetails.SelectedRow.Cells[0].Text + " AND QUOT_ATTACHMENT='" + lbtnFileOpener.Text + "'";
        dbcon.Open();
        string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
        string path = "../../Content/SupQuotAttachments/" + filename;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
    }
    #endregion

    #region  Link Button lbtnSupQuotNo_Click
    protected void lbtnSupQuotNo_Click(object sender, EventArgs e)
    {
        tblSupQuotationDetails.Visible = false;
        tblButtons.Visible = false;
        LinkButton lbtnSupQuotNo;
        lbtnSupQuotNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSupQuotNo.Parent.Parent;
        gvSupQuotationDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

            if (objSupQuot.SuppliersQuotation_Select(gvSupQuotationDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblSupQuotationDetails.Visible = true;
                tblButtons.Visible = true;
                //
                ddlSupplierName.SelectedValue = objSupQuot.SupId;
                ddlSupplierName_SelectedIndexChanged(sender, e);
                ddlEnquiryNo.SelectedValue = objSupQuot.SupEnqId;
                ddlEnquiryNo_SelectedIndexChanged(sender, e);
               // txtEnquiryDate.Text = objSupQuot.SupEnqDate;
                txtQuotationNo.Text = objSupQuot.SupQuotNo;
                txtQuotationDate.Text = objSupQuot.SupQuotDate;
                //ddlSupplierName.SelectedValue = objSupQuot.SupId;
               //ddlSupplierName_SelectedIndexChanged(sender, e);
                ddlPOType.SelectedItem.Text = objSupQuot.SupQuotPOType;
                ddlTransport.SelectedItem.Value = objSupQuot.TransId;
              //  cblStatus.SelectedValue = objSupQuot.SupQuotSTC;
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
                txtSubTotal.Text = objSupQuot.SupExworks;
                txtDisc.Text = objSupQuot.SupDiscount;
                lblTtlAmt.Text = objSupQuot.SupNetAmount;
                lblsum.Text = objSupQuot.SupTtlAmount;
                lblSOIdHidden.Text = objSupQuot.SupQuotId;

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

                SCM.IndentApproval objSupEnq = new SCM.IndentApproval();
                objSupEnq.SupQuationDtls_Select3(gvSupQuotationDetails.SelectedRow.Cells[0].Text, gvProductDetails);


                //objSupQuot.SuppliersQuotationDetails_Select(gvSupQuotationDetails.SelectedRow.Cells[0].Text, gvProductDetails);

              //  txtRate.Text = objSupQuot.SupQuotDetRate;
              //ddlItemCode.SelectedItem.Text = objSupQuot.SupQuotDetCurrency;
              //  txtTax.Text = objSupQuot.SupQuotDetTax;
              //  ddlTaxType.SelectedValue = objSupQuot.SupQuotDetTaxType;
              //  txtPFRate.Text = objSupQuot.SupQuotDetPFRate;
              //  ddlPFType.SelectedValue = objSupQuot.SupQuotDetPFType;
              //  txtExciseRate.Text = objSupQuot.SupQuotDetExciseRate;
              //  ddlExciseType.SelectedValue = objSupQuot.SupQuotDetExciseType;
              //  txtDisRate.Text = objSupQuot.SupQuotDetDisRate;
              //  ddlDisType.SelectedValue = objSupQuot.SupQuotDetDisType;
              //  txtMinDelPeriod.Text = objSupQuot.SupQuotDetMinDelPer;
              //  txtMaxDelPeriod.Text = objSupQuot.SupQuotDetMaxDelPer;
              //  ddlDelPeriod.SelectedValue = objSupQuot.SupQuotDetDelPer;
              //  txtSpecifications.Text = objSupQuot.SupQuotDetSpecs;
              //  txtPayTerms.Text = objSupQuot.SupQuotDetPayTerms;
                //txtLandedCost.Text = objSupQuot.SupQuotDetLandedCost;


            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            SCM.Dispose();
        }


    }
    #endregion

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {

                DataTable IndentApprovalProducts = new DataTable();

                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Itemname");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Itemtype");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Indentid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Requiredfor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IndentdetId");
                IndentApprovalProducts.Columns.Add(col);
                if (gvProductDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvProductDetails.Rows)
                    {
                        DataRow dr = IndentApprovalProducts.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ItemName"] = gvrow1.Cells[3].Text;
                        dr["ItemType"] = gvrow1.Cells[4].Text;
                        dr["Indentid"] = gvrow1.Cells[5].Text;
                        dr["UOM"] = gvrow1.Cells[6].Text;
                        dr["Quantity"] = gvrow1.Cells[7].Text;
                        dr["Brand"] = gvrow1.Cells[8].Text;
                        dr["Requiredfor"] = gvrow1.Cells[9].Text;
                        dr["Color"] = gvrow1.Cells[10].Text;
                        dr["ColorId"] = gvrow1.Cells[11].Text;


                        IndentApprovalProducts.Rows.Add(dr);
                    }
                }

                if (gvProductDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvProductDetails.Rows)
                    {
                        if (gvrow1.Cells[2].Text == gvrow.Cells[2].Text)
                        {
                            gvProductDetails.DataSource = IndentApprovalProducts;
                            gvProductDetails.DataBind();
                            MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            ch.Checked = false;
                            return;
                        }

                    }
                }

                DataRow drnew = IndentApprovalProducts.NewRow();
                drnew["ItemCode"] = gvrow.Cells[2].Text;
                drnew["ItemName"] = gvrow.Cells[3].Text;
                drnew["ItemType"] = gvrow.Cells[4].Text;
                drnew["Indentid"] = gvrow.Cells[5].Text;
                drnew["UOM"] = gvrow.Cells[6].Text;
                Label qty = (Label)gvrow.FindControl("lblQuantity");
                drnew["Quantity"] = qty.Text;
                drnew["Brand"] = gvrow.Cells[8].Text;
                drnew["Requiredfor"] = gvrow.Cells[9].Text;
                drnew["Color"] = gvrow.Cells[10].Text;
                drnew["ColorId"] = gvrow.Cells[11].Text;
                IndentApprovalProducts.Rows.Add(drnew);
                gvProductDetails.DataSource = IndentApprovalProducts;
                gvProductDetails.DataBind();
                ch.Checked = false;
            }
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

              //  SCM.BeginTransaction();

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
                objSupQuot.SupNetAmount = lblTtlAmt.Text;
               // objSupQuot.SupTtlAmount = lblsum.Text;
                objSupQuot.SupTtlAmount = hfSum.Value;
                //objSupQuot.SupTtlAmount = "0";

               

                if (objSupQuot.SuppliersQuotation_Save() == "Data Saved Successfully")
                {
                    objSupQuot.SuppliersQuotationDetails_Delete(objSupQuot.SupQuotId);
                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {

                        objSupQuot.ItemCode = gvrow.Cells[2].Text;
                        TextBox txtPrice = (TextBox)gvrow.FindControl("txtPrice");
                        objSupQuot.SupQuotDetRate = txtPrice.Text;
                        DropDownList currencyType = (DropDownList)gvrow.FindControl("ddlCurrency");
                        objSupQuot.SupQuotDetCurrency = currencyType.SelectedItem.Value;
                        TextBox txtDiscount = (TextBox)gvrow.FindControl("txtDiscount");
                        objSupQuot.SupQuotdetDiscount = txtDiscount.Text;
                       // TextBox txtSpeciallPrice = (TextBox)gvrow.FindControl("txtSpecialPrice");
                        Label lblSpeciallPrice = (Label)gvrow.FindControl("lblSpecialPrice");
                        objSupQuot.SupQuotDetSpPrice = lblSpeciallPrice.Text;
                        TextBox txtArrivalDate = (TextBox)gvrow.FindControl("txtArrivalDate");
                        objSupQuot.SupQuotDetArriDate = Yantra.Classes.General.toMMDDYYYY(txtArrivalDate.Text);
                        TextBox txtInvoiceNo = (TextBox)gvrow.FindControl("txtInvoiceNo");
                        objSupQuot.SupQuotDetInvoiceNo = txtInvoiceNo.Text;
                        objSupQuot.SupQuotDetColorId = gvrow.Cells[11].Text;
                        objSupQuot.SupQuotDetReqFor = gvrow.Cells[9].Text;
                        Label qty = (Label)gvrow.FindControl("lblQuantity");
                        objSupQuot.SupQuotDetQty = qty.Text;
                        //objSupQuot.SupQuotDetQty = gvrow.Cells[7].Text;
                        objSupQuot.SupQuotDetIndId = gvrow.Cells[5].Text;
                        objSupQuot.SupQuotDetIndDetId = gvrow.Cells[12].Text;
                        
                        objSupQuot.SuppliersQuotationDetails_Save();

                    }

                    if (Uploadattach.HasFiles)
                    {

                        #region Item Attachment
                        string Attachment = "";
                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/SupQuotAttachments"))
                        {

                            foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                            {
                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/SupQuotAttachments/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                Attachment = randNumber + "_" + fileName;
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                                objSupQuot.QuotAttchment = Attachment;
                                objSupQuot.SupplierQuotAttachment_Save();
                            }

                        }
                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/SupQuotAttachments");
                            foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                            {
                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/SupQuotAttachments/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                Attachment = randNumber + "_" + fileName;
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                                objSupQuot.QuotAttchment = Attachment;
                                objSupQuot.SupplierQuotAttachment_Save();
                            }

                        }

                        #endregion

                    }




                  //  SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    //SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
              //  SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblSupQuotationDetails.Visible = false;
                tblButtons.Visible = false;
                gvProductDetails.DataBind();
                btnDelete.Attributes.Clear();
                gvSupQuotationDetails.DataBind();
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

             //   SCM.BeginTransaction();

                objSupQuot.SupQuotId = gvSupQuotationDetails.SelectedRow.Cells[0].Text;
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
                objSupQuot.SupNetAmount = lblTtlAmt.Text;
                 objSupQuot.SupTtlAmount = lblsum.Text;
               // objSupQuot.SupTtlAmount = hfSum.Value;
                //objSupQuot.SupTtlAmount = "0";

               
                if (objSupQuot.SuppliersQuotation_Update() == "Data Updated Successfully")
                {
                    objSupQuot.SuppliersQuotationDetails_Delete(objSupQuot.SupQuotId);
                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {
                        //objSupQuot.ItemCode = gvrow.Cells[1].Text;
                        ////objSupQuot.UOM = gvrow.Cells[3].Text;

                        //objSupQuot.SupQuotDetRate = gvrow.Cells[8].Text;
                        //objSupQuot.SupQuotDetCurrency = gvrow.Cells[13].Text;
                        //objSupQuot.SupSpPrice = gvrow.Cells[11].Text;
                        //objSupQuot.SupQty = gvrow.Cells[5].Text;
                        //objSupQuot.SupQuotDetDisRate = gvrow.Cells[10].Text;
                        //objSupQuot.SupQuotDetSpecs = gvrow.Cells[12].Text;
                        //objSupQuot.SupQuotDetTax = "1";
                        //objSupQuot.SupQuotDetPFRate = "1";
                        //objSupQuot.SupQuotDetExciseRate = "1";
                        //objSupQuot.SupQuotDetLandedCost = "1";
                        //objSupQuot.SupQuotDetOldRate = gvrow.Cells[14].Text;


                        objSupQuot.ItemCode = gvrow.Cells[2].Text;
                        TextBox txtPrice = (TextBox)gvrow.FindControl("txtPrice");
                        objSupQuot.SupQuotDetRate = txtPrice.Text;
                        DropDownList currencyType = (DropDownList)gvrow.FindControl("ddlCurrency");
                        objSupQuot.SupQuotDetCurrency = currencyType.SelectedItem.Value;
                        TextBox txtDiscount = (TextBox)gvrow.FindControl("txtDiscount");
                        objSupQuot.SupQuotdetDiscount = txtDiscount.Text;
                        // TextBox txtSpeciallPrice = (TextBox)gvrow.FindControl("txtSpecialPrice");
                        Label lblSpeciallPrice = (Label)gvrow.FindControl("lblSpecialPrice");
                        objSupQuot.SupQuotDetSpPrice = lblSpeciallPrice.Text;
                        TextBox txtArrivalDate = (TextBox)gvrow.FindControl("txtArrivalDate");
                        objSupQuot.SupQuotDetArriDate =Yantra.Classes.General.toMMDDYYYY(txtArrivalDate.Text);
                        TextBox txtInvoiceNo = (TextBox)gvrow.FindControl("txtInvoiceNo");
                        objSupQuot.SupQuotDetInvoiceNo = txtInvoiceNo.Text;
                        objSupQuot.SupQuotDetColorId = gvrow.Cells[11].Text;
                        objSupQuot.SupQuotDetReqFor = gvrow.Cells[9].Text;
                        Label qty = (Label)gvrow.FindControl("lblQuantity");
                        objSupQuot.SupQuotDetQty = qty.Text;
                        //objSupQuot.SupQuotDetQty = gvrow.Cells[7].Text;
                        objSupQuot.SupQuotDetIndId = gvrow.Cells[5].Text;
                        objSupQuot.SupQuotDetIndDetId = gvrow.Cells[12].Text;
                       
                        objSupQuot.SuppliersQuotationDetails_Save();
                    }
                    if (Uploadattach.HasFiles)
                    {

                        #region Item Attachment
                        string Attachment = "";
                        if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/SupQuotAttachments"))
                        {

                            foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                            {
                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/SupQuotAttachments/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                Attachment = randNumber + "_" + fileName;
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                                objSupQuot.QuotAttchment = Attachment;
                                objSupQuot.SupplierQuotAttachment_Save();
                            }

                        }
                        else
                        {
                            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/SupQuotAttachments");
                            foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                            {
                                Random rand = new Random();
                                string randNumber = Convert.ToString(rand.Next(0, 10000));
                                string path = Server.MapPath("~/Content/SupQuotAttachments/");
                                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                                Attachment = randNumber + "_" + fileName;
                                uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                                objSupQuot.QuotAttchment = Attachment;
                                objSupQuot.SupplierQuotAttachment_Save();
                            }

                        }

                        #endregion

                    }

                   // SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                   // SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
              //  SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                tblSupQuotationDetails.Visible = false;
                tblButtons.Visible = false;
                gvProductDetails.DataBind();
                btnDelete.Attributes.Clear();
                gvSupQuotationDetails.DataBind();
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

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvSupQuotationDetails.SelectedIndex > -1)
        {
            //Response.Redirect("QuotationNew.aspx?invoiceId=" + gvSupQuotationDetails.SelectedRow.Cells[0].Text);
            try
            {
                SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

                if (objSupQuot.SuppliersQuotation_Select(gvSupQuotationDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblSupQuotationDetails.Visible = true;
                    tblButtons.Visible = true;
                    txtQuotationNo.Text = objSupQuot.SupQuotNo;
                    txtQuotationDate.Text = objSupQuot.SupQuotDate;
                    //txtQuotationDate.Text = objSupQuot.SupQuotDate;
                    //txtEnquiryDate.Text = objSupQuot.SupEnqDate;
                    ddlSupplierName.SelectedValue = objSupQuot.SupId;
                    ddlSupplierName_SelectedIndexChanged(sender, e);
                    ddlEnquiryNo.SelectedValue = objSupQuot.SupEnqId;
                    ddlEnquiryNo_SelectedIndexChanged(sender, e);              
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
                    lblSOIdHidden.Text = objSupQuot.SupQuotId;

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
                    SCM.IndentApproval objSupEnq = new SCM.IndentApproval();
                    objSupEnq.SupQuationDtls_Select3(gvSupQuotationDetails.SelectedRow.Cells[0].Text, gvProductDetails);

                    //   txtRate.Text = objSupQuot.SupQuotDetRate;
                    //   ddlItemCode.SelectedItem.Text = objSupQuot.SupQuotDetCurrency;
                    //   txtTax.Text = objSupQuot.SupQuotDetTax;
                    //   ddlTaxType.SelectedValue = objSupQuot.SupQuotDetTaxType;
                    //   txtPFRate.Text = objSupQuot.SupQuotDetPFRate;
                    //   ddlPFType.SelectedValue = objSupQuot.SupQuotDetPFType;
                    //   txtExciseRate.Text = objSupQuot.SupQuotDetExciseRate;
                    //   ddlExciseType.SelectedValue = objSupQuot.SupQuotDetExciseType;
                    //   txtDisRate.Text = objSupQuot.SupQuotDetDisRate;
                    //   ddlDisType.SelectedValue = objSupQuot.SupQuotDetDisType;
                    //   txtMinDelPeriod.Text = objSupQuot.SupQuotDetMinDelPer;
                    //   txtMaxDelPeriod.Text = objSupQuot.SupQuotDetMaxDelPer;
                    //   ddlDelPeriod.SelectedValue = objSupQuot.SupQuotDetDelPer;
                    //   txtSpecifications.Text = objSupQuot.SupQuotDetSpecs;
                    //   txtPayTerms.Text = objSupQuot.SupQuotDetPayTerms;
                    //   txtLandedCost.Text = objSupQuot.SupQuotDetLandedCost;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
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

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSupQuotationDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.SuppliersQuotation objSupQuot = new SCM.SuppliersQuotation();

                MessageBox.Show(this, objSupQuot.SuppliersQuotation_Delete(gvSupQuotationDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvSupQuotationDetails.DataBind();
                tblSupQuotationDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblSupQuotationDetails.Visible = false;
        tblButtons.Visible = false;
    }
    #endregion

    #region Rate Type Fill
    private void RateType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlRate);
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
                //Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemCode.SelectedItem.Value + "";
                txtRate.Text = objMaster.Rate;
               // txtOldRate.Text = objMaster.Rate;
                txtQunatity.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtSpPrice.Text = string.Empty;
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(ddlItemCode.SelectedItem.Value) > 0)
                {
                    txtOldRate.Text = objrate.rsp;
                }
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

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Sup Quotation Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
    #endregion

    #region ddlSymbols_SelectedIndexChanged
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvSupQuotationDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "Sup Quotation Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvSupQuotationDetails.DataBind();
    }
    #endregion

    #region gvSupQuotationDetails_RowDataBound
    protected void gvSupQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Enquiry No. Select Index Changed
    protected void ddlEnquiryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.IndentApproval objSupEnq = new SCM.IndentApproval();

            if (objSupEnq.IndentApproval_Select(ddlEnquiryNo.SelectedItem.Value) > 0)
            {
                txtEnquiryDate.Text = objSupEnq.INDAPPRLDate;

                objSupEnq.IndentApprovalDetails_Select2(ddlEnquiryNo.SelectedItem.Value, gvApprlItemDetails);
               // Masters.ItemMaster.PurchaseQuotationItemTypesPerformaInvoice_Select(ddlEnquiryNo.SelectedItem.Value, ddlItemCode);
              //  Supplier_Fill(objSupEnq.SuppEnqId);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
           // SCM.Dispose();
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
    protected void ddlTaxType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvSupQuotationDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PInovice&PIid=" + gvSupQuotationDetails.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

    //protected void gvEnquiryProducts_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    DataTable IndentProducts = new DataTable();
    //    DataColumn col = new DataColumn();
    //    col = new DataColumn("ItemCode");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemName");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemType");
    //    IndentProducts.Columns.Add(col);

    //    col = new DataColumn("UOM");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Quantity");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Priority");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Brand");
    //    IndentProducts.Columns.Add(col);    
    //   col = new DataColumn("Specification");
    //    IndentProducts.Columns.Add(col);

    //    if (gvEnquiryProducts.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow gvrow in gvEnquiryProducts.Rows)
    //        {
    //            DataRow dr = IndentProducts.NewRow();
    //            dr["ItemCode"] = gvrow.Cells[2].Text;
    //            dr["ItemName"] = gvrow.Cells[3].Text;
    //            dr["ItemType"] = gvrow.Cells[4].Text;
    //            dr["UOM"] = gvrow.Cells[5].Text;
    //            dr["Quantity"] = gvrow.Cells[7].Text;
    //            dr["Priority"] = gvrow.Cells[9].Text;
    //            dr["Brand"] = gvrow.Cells[6].Text;               
    //            dr["Specification"] = gvrow.Cells[8].Text;


    //            IndentProducts.Rows.Add(dr);
    //            if (gvrow.RowIndex == gvEnquiryProducts.Rows[e.NewEditIndex].RowIndex)
    //            {
    //                ddlItemCode.SelectedValue = gvrow.Cells[2].Text;
    //                ddlItemCode_SelectedIndexChanged(sender, e);
    //                 txtQunatity.Text = gvrow.Cells[7].Text;
    //                 txtSpecifications.Text = gvrow.Cells[8].Text;
    //                 gvEnquiryProducts.SelectedIndex = gvrow.RowIndex;
    //            }
    //        }
    //    }
    //}
    //protected void gvEnquiryProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string x1 = gvEnquiryProducts.Rows[e.RowIndex].Cells[1].Text;
    //    DataTable IndentProducts = new DataTable();
    //    DataColumn col = new DataColumn();
    //    col = new DataColumn("ItemCode");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemName");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemType");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("UOM");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Quantity");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Priority");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Brand");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Specification");
    //    IndentProducts.Columns.Add(col);

    //    if (gvEnquiryProducts.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow gvrow in gvEnquiryProducts.Rows)
    //        {
    //            if (gvrow.RowIndex != e.RowIndex)
    //            {
    //                DataRow dr = IndentProducts.NewRow();
    //                dr["ItemCode"] = gvrow.Cells[2].Text;
    //                dr["ItemName"] = gvrow.Cells[3].Text;
    //                dr["ItemType"] = gvrow.Cells[4].Text;
    //                dr["UOM"] = gvrow.Cells[5].Text;
    //                dr["Quantity"] = gvrow.Cells[7].Text;
    //                dr["Priority"] = gvrow.Cells[9].Text;
    //                dr["Brand"] = gvrow.Cells[6].Text;                                                   
    //                dr["Specification"] = gvrow.Cells[8].Text;

    //                IndentProducts.Rows.Add(dr);
    //            }
    //        }
    //    }
    //    gvEnquiryProducts.DataSource = IndentProducts;
    //    gvEnquiryProducts.DataBind();
    //    MessageBox.Show(this, "Deleted Successfully");
    //}
    protected void btnPurchase_Click(object sender, EventArgs e)
    {
        if (gvSupQuotationDetails.SelectedIndex > -1)
        {
            Response.Redirect("FixedPurchaseOrderDetails.aspx?QId=" + gvSupQuotationDetails.SelectedRow.Cells[0].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemCode, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value);

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSupQuotationDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSupQuotationDetails.DataBind();
    }
    protected void gvApprlItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
        {
           
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {

                DataTable IndentApprovalProducts = new DataTable();

                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Itemname");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Itemtype");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Indentid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Requiredfor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IndentdetId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Price");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("SplAmt");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ArrivalDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("InvoiceNo");
                IndentApprovalProducts.Columns.Add(col);
              
                if (gvProductDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvProductDetails.Rows)
                    {
                        DataRow dr = IndentApprovalProducts.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ItemName"] = gvrow1.Cells[3].Text;
                        dr["ItemType"] = gvrow1.Cells[4].Text;
                        dr["Indentid"] = gvrow1.Cells[5].Text;
                        dr["UOM"] = gvrow1.Cells[6].Text;
                        Label qty = (Label)gvrow1.FindControl("lblQuantity");
                        //dr["Quantity"] = gvrow1.Cells[7].Text;
                        dr["Quantity"] = qty.Text;
                        dr["Brand"] = gvrow1.Cells[8].Text;
                        dr["Requiredfor"] = gvrow1.Cells[9].Text;
                        dr["Color"] = gvrow1.Cells[10].Text;
                        dr["ColorId"] = gvrow1.Cells[11].Text;
                        dr["IndentdetId"] = gvrow1.Cells[12].Text;
                        DropDownList curncy = (DropDownList)gvrow1.FindControl("ddlCurrency");
                        dr["Currency"] = curncy.SelectedItem.Text;
                        TextBox price = (TextBox)gvrow1.FindControl("txtPrice");
                        dr["Price"] = price.Text;
                        TextBox disc = (TextBox)gvrow1.FindControl("txtDiscount");
                        dr["Discount"] = disc.Text;
                        Label splAmt = (Label)gvrow1.FindControl("lblSpecialPrice");
                        dr["SplAmt"] = splAmt.Text;
                        TextBox ariDate = (TextBox)gvrow1.FindControl("txtArrivalDate");
                        dr["ArrivalDate"] = ariDate.Text;
                        TextBox invoiceNo = (TextBox)gvrow1.FindControl("txtInvoiceNo");
                        dr["InvoiceNo"] = invoiceNo.Text;



                        IndentApprovalProducts.Rows.Add(dr);
                    }
                }

                if (gvProductDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvProductDetails.Rows)
                    {
                        if (gvrow1.Cells[2].Text == gvrow.Cells[2].Text)
                        {
                            gvProductDetails.DataSource = IndentApprovalProducts;
                            gvProductDetails.DataBind();
                            MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            ch.Checked = false;
                            return;
                        }

                    }
                }

                DataRow drnew = IndentApprovalProducts.NewRow();
                drnew["ItemCode"] = gvrow.Cells[2].Text;
                drnew["ItemName"] = gvrow.Cells[3].Text;
                drnew["ItemType"] = gvrow.Cells[4].Text;
                drnew["Indentid"] = gvrow.Cells[5].Text;
                drnew["UOM"] = gvrow.Cells[6].Text;
                //Label qty = (Label)gvrow.FindControl("lblQuantity");
                drnew["Quantity"] = gvrow.Cells[7].Text;
                drnew["Brand"] = gvrow.Cells[8].Text;
                drnew["Requiredfor"] = gvrow.Cells[9].Text;
                drnew["Color"] = gvrow.Cells[10].Text;
                drnew["ColorId"] = gvrow.Cells[11].Text;
                drnew["IndentdetId"] = gvrow.Cells[12].Text;

                IndentApprovalProducts.Rows.Add(drnew);
                gvProductDetails.DataSource = IndentApprovalProducts;
                gvProductDetails.DataBind();
                ch.Checked = false;
            }
        }

    }
    protected void gvProductDetails_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if(e.Row.RowType == DataControlRowType.DataRow )
        {
            HiddenField hf = (HiddenField)e.Row.FindControl("cthf1");
            DropDownList ddlcurency = (DropDownList)e.Row.FindControl("ddlCurrency");
            SM.DDLBindWithSelect(ddlcurency, "SELECT CURRENCY_ID,CURRENCY_NAME FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null", hf.Value);

            //SCM.IndentApproval objSupEnq = new SCM.IndentApproval();
           
            //if (objSupEnq.SupQuationDtls_Select4(gvSupQuotationDetails.SelectedRow.Cells[0].Text) > 0)
            //{
            //    if (objSupEnq.disc != null)
            //    {
            //        ddlcurency.SelectedValue = objSupEnq.disc;
            //    }
            //}

        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[11].Visible = false;

            //DropDownList ddlcurency = (DropDownList)e.Row.FindControl("ddlCurrency");
            //SM.DDLBindWithSelect(ddlcurency, "SELECT CURRENCY_ID,CURRENCY_NAME FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null");

            //SCM.IndentApproval objSupEnq = new SCM.IndentApproval();
            //if (objSupEnq.SupQuationDtls_Select4(gvSupQuotationDetails.SelectedRow.Cells[0].Text) > 0)
            //{
            //    if (objSupEnq.disc != null)
            //    {
            //        ddlcurency.SelectedItem.Value = objSupEnq.disc;
            //    }
            //}
        }
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvProductDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtPrice");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            Label splAmt = (Label)gvr.FindControl("lblSpecialPrice");
            Label qty = (Label)gvr.FindControl("lblQuantity");
            if (rate.Text == "") { rate.Text = "0"; }
            string disc;
            if (rate.Text != "" && discount.Text != "")
            {
                disc = ((Convert.ToDecimal(rate.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                splAmt.Text = ((Convert.ToDecimal(rate.Text) - Convert.ToDecimal(disc)) * (Convert.ToDecimal(qty.Text))).ToString();
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
            else
            {
                splAmt.Text = ((Convert.ToDecimal(rate.Text)) * (Convert.ToDecimal(qty.Text))).ToString();
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
        }
    }
    protected void txtPrice_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvProductDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtPrice");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            Label splAmt = (Label)gvr.FindControl("lblSpecialPrice");
            Label qty = (Label)gvr.FindControl("lblQuantity");
            string disc;
            if (rate.Text != "" && discount.Text != "")
            {
                disc = ((Convert.ToDecimal(rate.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                splAmt.Text = ((Convert.ToDecimal(rate.Text) - Convert.ToDecimal(disc)) * (Convert.ToDecimal(qty.Text))).ToString();
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
            else if (rate.Text != "" && discount.Text == "")
            {
                splAmt.Text = ((Convert.ToDecimal(rate.Text)) * (Convert.ToDecimal(qty.Text))).ToString();
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
            else
            {
                splAmt.Text = "0";
                lblTtlAmt.Text = NetAmountCalc().ToString();
            }
        }
    }

    private double NetAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvProductDetails.Rows)
        {
            Label amt = (Label)gvrow.FindControl("lblSpecialPrice");
            if (amt.Text != "")
            {
                _totalAmt = _totalAmt + Convert.ToDouble(amt.Text);
            }
            else
            {
                _totalAmt = _totalAmt + 0;

            }
        }
        return _totalAmt;
    }

    protected void gvProductDetails_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvProductDetails.Rows[e.RowIndex].Cells[2].Text;
        DataTable IndentApprovalProducts = new DataTable();

        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Itemname");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Itemtype");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Indentid");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Requiredfor");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Color");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IndentdetId");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Currency");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Price");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Discount");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("SplAmt");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("ArrivalDate");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("InvoiceNo");
        IndentApprovalProducts.Columns.Add(col);

        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow1 in gvProductDetails.Rows)
            {
                if (gvrow1.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = gvrow1.Cells[2].Text;
                    dr["ItemName"] = gvrow1.Cells[3].Text;
                    dr["ItemType"] = gvrow1.Cells[4].Text;
                    dr["Indentid"] = gvrow1.Cells[5].Text;
                    dr["UOM"] = gvrow1.Cells[6].Text;
                    Label qty = (Label)gvrow1.FindControl("lblQuantity");
                    dr["Quantity"] = qty.Text;
                    dr["Brand"] = gvrow1.Cells[8].Text;
                    dr["Requiredfor"] = gvrow1.Cells[9].Text;
                    dr["Color"] = gvrow1.Cells[10].Text;
                    dr["ColorId"] = gvrow1.Cells[11].Text;
                    dr["IndentdetId"] = gvrow1.Cells[12].Text;
                    DropDownList curncy = (DropDownList)gvrow1.FindControl("ddlCurrency");
                    dr["Currency"] = curncy.SelectedItem.Text;
                    TextBox price = (TextBox)gvrow1.FindControl("txtPrice");
                    dr["Price"] = price.Text;
                    TextBox disc = (TextBox)gvrow1.FindControl("txtDiscount");
                    dr["Discount"] = disc.Text;
                    Label splAmt = (Label)gvrow1.FindControl("lblSpecialPrice");
                    dr["SplAmt"] = splAmt.Text;
                    TextBox ariDate = (TextBox)gvrow1.FindControl("txtArrivalDate");
                    dr["ArrivalDate"] = ariDate.Text;
                    TextBox invoiceNo = (TextBox)gvrow1.FindControl("txtInvoiceNo");
                    dr["InvoiceNo"] = invoiceNo.Text;

                    IndentApprovalProducts.Rows.Add(dr);
                }
            }
        }

        gvProductDetails.DataSource = IndentApprovalProducts;
        gvProductDetails.DataBind();
    }

    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvSupQuotationDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
