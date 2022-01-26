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

public partial class Modules_SCM_PurchaseInvoice : basePage
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - Convert.ToDecimal(txtDiscount.Text));

        #endregion
        if (!IsPostBack)
        {
            setControlsVisibility();

            txtPackingCharges.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtTranportationCharges.Attributes.Add("onkeyup", "javascript:grosscalc();");
            rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtVAT.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtCST.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtExcise.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();

            SupplierFixedPO_Fill();
            SupplierName_Fill();
            DespatchMode_Fill();
            //ItemTypes_Fill();
            EmployeeMaster_Fill();
            btnPrint.Visible = false;
            SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");

        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "24");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnApprove.Enabled = up.Approve;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        //btnPrint.Enabled = up.Print;
    }


    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - Convert.ToDecimal(txtDiscount.Text));


        if (rbVAT.Checked == true)
        {
            txtVAT.Style.Add("display", "");
            lblVAT.Style.Add("display", "");
            txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            txtCST.Style.Add("display", "");
            lblCSTax.Style.Add("display", "");
        }

        #endregion
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvInvoiceDetails.SelectedRow.Cells[9].Text) && gvInvoiceDetails.SelectedRow.Cells[9].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnPrint.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            //btnPrint.Visible = false;
        }



    }
    #endregion

    #region Supplier Name
    private void SupplierName_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SCM.Dispose();
        }
    }
    #endregion

    #region DespatchMode
    private void DespatchMode_Fill()
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
            Masters.Dispose();
        }
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemType);
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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.PurchaseQuotationItemTypes1_Select(ddlPONo.SelectedItem.Value,ddlItemType);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Purchase Order Fill
    private void SupplierFixedPO_Fill()
    {
        try
        {
            SCM.SupplierFixedPO.SuppliersFixedPO_Select(ddlPONo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SCM.Dispose();
        }
    }
    #endregion

    #region Link Button InvoiceNo_Click
    protected void lbtnInvoiceNo_Click(object sender, EventArgs e)
    {
        tblPIDetails.Visible = false;
        LinkButton lbtnInvoiceNo;
        lbtnInvoiceNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnInvoiceNo.Parent.Parent;
        gvInvoiceDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        //try
        //{
        //    SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();

        //    if (objSCM.PurchaseInvoice_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = false;
        //        tblPIDetails.Visible = true;
        //        txtInvoiceNo.Text = objSCM.PINo;
        //        txtInvoiceDate.Text = objSCM.PIDate;
        //        ddlPONo.SelectedValue = objSCM.FPOId;
        //        ddlInvoiceType.SelectedValue = objSCM.PIInvType;
        //        ddlSupplierName.SelectedValue = objSCM.SUPId;

        //        txtPackingCharges.Text = objSCM.PIPackChrgs;
        //        txtTranportationCharges.Text = objSCM.PITransChrgs;
        //        txtInsurance.Text = objSCM.PIInsuranceChrgs;
        //        txtMiscelleneous.Text = objSCM.PIMiscChrgs;
        //        txtDiscount.Text = objSCM.PIDiscount;
        //        txtGrossAmount.Text = objSCM.PIGrossAmt;
        //        txtTermsOfDelivery.Text = objSCM.PITermsofDelivery;
        //        ddlDespatchMode.SelectedValue = objSCM.DESPMId;
        //        txtRemarks.Text = objSCM.PIRemarks;
        //        ddlPreparedBy.SelectedValue = objSCM.PIPreparedBy;
        //        ddlApprovedBy.SelectedValue = objSCM.PIApprovedBy;
        //        txtBank.Text = objSCM.PIBank;
        //        txtChequeNo.Text = objSCM.PIChequeNo;
        //        txtDate.Text = objSCM.PIChequeDate;
        //        txtCustInvNo.Text = objSCM.PICustInvNo;
        //        txtCustInvDate.Text = objSCM.PICustInvDate;
        //        txtpaybylc.Text = objSCM.PAYBYLC;
        //        txtPaybytt.Text = objSCM.PAYBYTT;
        //        txtLCdate.Text = objSCM.LCDATE;
        //        txtlcexpdate.Text = objSCM.LCEXPDATE;
        //        txtttdate.Text = objSCM.TTDATE;

        //        objSCM.PurchaseInvoiceDetails_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text, gvItemDetails);



        //        //SCM.SuppliersFixedPO objSupplierFixedPO = new SCM.SuppliersFixedPO();
        //        //if (objSalesFPO.SuppliersFixedPO_Select(objPurchaseInvoice.FPONo) > 0)
        //        //{
        //        //    txtPackingCharges.Text = objSupplierFixedPO.PIPackChrgs;
        //        //    txtTranportationCharges.Text = objSupplierFixedPO.PITransChrgs;
        //        //    txtInsurance.Text = objSupplierFixedPO.PIInsuranceChrgs;
        //        //    txtMiscelleneous.Text = objSupplierFixedPO.PIMiscChrgs;
        //        //    txtDiscount.Text = objSupplierFixedPO.PIDiscount;
        //        //    txtGrassAmount.Tex = objSupplierFixedPO.FPONetAmount;


        //        //}
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    btnDelete.Attributes.Clear();
        //    SCM.Dispose();
        //    ddlPONo_SelectedIndexChanged(sender, e);

        //}
    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseInvoiceNew1.aspx");
        //SCM.ClearControls(this);
        //txtInvoiceNo.Text = SCM.PurchaseInvoice.PurchaseInvoice_AutoGenCode();
        //txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //btnSave.Text = "Save";
        //btnSave.Enabled = true; ;
        //tblPIDetails.Visible = true;
        //gvItemDetails.DataBind();
        //gvItDetails.DataBind();
        //gvInvoiceDetails.SelectedIndex = -1;
      
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            PurchaseInvoiceSave();
        }
        else if (btnSave.Text == "Update")
        {
            PurchaseInvoiceUpdate();
        }
    }
    #endregion

    #region  Purchase Invoice Save
    private void PurchaseInvoiceSave()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
                SCM.BeginTransaction();
                if (txtPackingCharges.Text == "") { txtPackingCharges.Text = "0"; }
                if (txtTranportationCharges.Text == "") { txtTranportationCharges.Text = "0"; }
                if (txtInsurance.Text == "") { txtInsurance.Text = "0"; }
                objSCM.PINo = txtInvoiceNo.Text;
                objSCM.PIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objSCM.FPOId = ddlPONo.SelectedItem.Value;
                objSCM.PIInvType = ddlInvoiceType.SelectedItem.Value;
                objSCM.SUPId = ddlSupplierName.SelectedItem.Value;

                objSCM.PIPackChrgs = txtPackingCharges.Text;
                objSCM.PITransChrgs = txtTranportationCharges.Text;
                objSCM.PIInsuranceChrgs = txtInsurance.Text;
                objSCM.PIMiscChrgs = txtMiscelleneous.Text;
                objSCM.PIDiscount = txtDiscount.Text;
                objSCM.PIGrossAmt = txtGrossAmount.Text;
                objSCM.PITermsofDelivery = txtTermsOfDelivery.Text;
                objSCM.DESPMId = ddlDespatchMode.SelectedItem.Value;
                objSCM.PIRemarks = txtRemarks.Text;
                objSCM.PIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.PIApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSCM.PIBank = txtBank.Text;
                objSCM.PIChequeNo = txtChequeNo.Text;
                objSCM.PIChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
                objSCM.PICustInvNo = txtCustInvNo.Text;
                objSCM.PICustInvDate = Yantra.Classes.General.toMMDDYYYY(txtCustInvDate.Text);
                objSCM.PIStatus = "New";
                objSCM.CpId = lblCPID.Text;
                objSCM.LCEXPDATE = Yantra.Classes.General.toMMDDYYYY(txtlcexpdate.Text);
                objSCM.LCDATE = Yantra.Classes.General.toMMDDYYYY(txtLCdate.Text);
                objSCM.PAYBYLC = txtLCdate.Text;
                objSCM.LCDATE = Yantra.Classes.General.toMMDDYYYY(txtLCdate.Text);
                objSCM.PAYBYLC = txtpaybylc.Text;


                if (objSCM.PurchaseInvoice_Save() == "Data Saved Successfully")
                {
                    objSCM.PurchaseInvoiceDetails_Delete(objSCM.PIId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.PIItemCode = gvrow.Cells[2].Text;
                        objSCM.PIDetQty = gvrow.Cells[6].Text;
                        objSCM.PIDetRate = gvrow.Cells[7].Text;
                        objSCM.PIDetCst = gvrow.Cells[9].Text;
                        objSCM.PIDetVat = gvrow.Cells[8].Text;
                        objSCM.PIDetExcise = gvrow.Cells[10].Text;
                        objSCM.PIDetAmount = gvrow.Cells[11].Text;

                        objSCM.PurchaseInvoiceDetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvInvoiceDetails.DataBind();
                gvItDetails.DataBind();
                gvItemDetails.DataBind();
                tblPIDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region PurchaseInvoiceUpdate
    private void PurchaseInvoiceUpdate()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
                SCM.BeginTransaction();
                objSCM.PIId = gvInvoiceDetails.SelectedRow.Cells[0].Text;
                objSCM.PINo = txtInvoiceNo.Text;
                objSCM.PIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objSCM.FPOId = ddlPONo.SelectedItem.Value;
                objSCM.PIInvType = ddlInvoiceType.SelectedItem.Text;
                objSCM.SUPId = ddlSupplierName.SelectedItem.Value;

                objSCM.PIPackChrgs = txtPackingCharges.Text;
                objSCM.PITransChrgs = txtTranportationCharges.Text;
                objSCM.PIInsuranceChrgs = txtInsurance.Text;
                objSCM.PIMiscChrgs = txtMiscelleneous.Text;
                objSCM.PIDiscount = txtDiscount.Text;
                objSCM.PIGrossAmt = txtGrossAmount.Text;
                objSCM.PITermsofDelivery = txtTermsOfDelivery.Text;
                objSCM.DESPMId = ddlDespatchMode.SelectedItem.Value;
                objSCM.PIRemarks = txtRemarks.Text;
                objSCM.PIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.PIApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSCM.PIBank = txtBank.Text;
                objSCM.PIChequeNo = txtChequeNo.Text;
                objSCM.PIChequeDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
                objSCM.PICustInvNo = txtCustInvNo.Text;
                objSCM.PICustInvDate = Yantra.Classes.General.toMMDDYYYY(txtCustInvDate.Text);
                objSCM.PIStatus = "New";
                objSCM.CpId = lblCPID.Text;
                objSCM.LCEXPDATE = Yantra.Classes.General.toMMDDYYYY(txtlcexpdate.Text);
                objSCM.LCDATE = Yantra.Classes.General.toMMDDYYYY(txtLCdate.Text);
                objSCM.PAYBYLC = txtLCdate.Text;
                objSCM.LCDATE = Yantra.Classes.General.toMMDDYYYY(txtLCdate.Text);
                objSCM.PAYBYLC = txtpaybylc.Text;


                if (objSCM.PurchaseInvoice_Update() == "Data Updated Successfully")
                {
                    objSCM.PurchaseInvoiceDetails_Delete(objSCM.PIId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.PIItemCode = gvrow.Cells[2].Text;
                        objSCM.PIDetQty = gvrow.Cells[6].Text;
                        objSCM.PIDetRate = gvrow.Cells[7].Text;
                        objSCM.PIDetCst = gvrow.Cells[9].Text;
                        objSCM.PIDetVat = gvrow.Cells[8].Text;
                        objSCM.PIDetExcise = gvrow.Cells[10].Text;
                        objSCM.PIDetAmount = gvrow.Cells[11].Text;

                        objSCM.PurchaseInvoiceDetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                btnDelete.Attributes.Clear();
                gvInvoiceDetails.DataBind();
                gvItDetails.DataBind();
                gvItemDetails.DataBind();
                tblPIDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Purchase Order");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            Response.Redirect("PurchaseInvoiceNew1.aspx?invoiceNo=" + gvInvoiceDetails.SelectedRow.Cells[0].Text + "&status=" + gvInvoiceDetails.SelectedRow.Cells[9].Text);
            //try
            //{
            //    SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();

            //    if (objSCM.PurchaseInvoice_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text) > 0)
            //    {
            //        btnSave.Text = "Update";
            //        btnSave.Enabled = true;
            //        tblPIDetails.Visible = true;
            //        txtInvoiceNo.Text = objSCM.PINo;
            //        txtInvoiceDate.Text = objSCM.PIDate;
            //        ddlPONo.SelectedValue = objSCM.FPOId;
            //        ddlInvoiceType.SelectedValue = objSCM.PIInvType;
            //        ddlSupplierName.SelectedValue = objSCM.SUPId;

            //        txtPackingCharges.Text = objSCM.PIPackChrgs;
            //        txtTranportationCharges.Text = objSCM.PITransChrgs;
            //        txtInsurance.Text = objSCM.PIInsuranceChrgs;
            //        txtMiscelleneous.Text = objSCM.PIMiscChrgs;
            //        txtDiscount.Text = objSCM.PIDiscount;
            //        txtGrossAmount.Text = objSCM.PIGrossAmt;
            //        txtTermsOfDelivery.Text = objSCM.PITermsofDelivery;
            //        ddlDespatchMode.SelectedValue = objSCM.DESPMId;
            //        txtRemarks.Text = objSCM.PIRemarks;
            //        ddlPreparedBy.SelectedValue = objSCM.PIPreparedBy;
            //        ddlApprovedBy.SelectedValue = objSCM.PIApprovedBy;
            //        txtBank.Text = objSCM.PIBank;
            //        txtChequeNo.Text = objSCM.PIChequeNo;
            //        txtDate.Text = objSCM.PIChequeDate;
            //        txtCustInvNo.Text = objSCM.PICustInvNo;
            //        txtCustInvDate.Text = objSCM.PICustInvDate;
            //        objSCM.PurchaseInvoiceDetails_Select(gvInvoiceDetails.SelectedRow.Cells[0].Text, gvItemDetails);
            //        txtpaybylc.Text = objSCM.PAYBYLC;
            //        txtPaybytt.Text = objSCM.PAYBYTT;
            //        txtLCdate.Text = objSCM.LCDATE;
            //        txtlcexpdate.Text = objSCM.LCEXPDATE;
            //        txtttdate.Text = objSCM.TTDATE;


            //        //SCM.SuppliersFixedPO objSupplierFixedPO = new SCM.SuppliersFixedPO();
            //        //if (objSalesFPO.SuppliersFixedPO_Select(objPurchaseInvoice.FPONo) > 0)
            //        //{
            //        //    txtPackingCharges.Text = objSupplierFixedPO.PIPackChrgs;
            //        //    txtTranportationCharges.Text = objSupplierFixedPO.PITransChrgs;
            //        //    txtInsurance.Text = objSupplierFixedPO.PIInsuranceChrgs;
            //        //    txtMiscelleneous.Text = objSupplierFixedPO.PIMiscChrgs;
            //        //    txtDiscount.Text = objSupplierFixedPO.PIDiscount;
            //        //    txtGrassAmount.Tex = objSupplierFixedPO.FPONetAmount;


            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message.ToString());
            //}
            //finally
            //{
            //    btnDelete.Attributes.Clear();
            //    SCM.Dispose();
            //    ddlPONo_SelectedIndexChanged(sender, e);

            //}
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
                MessageBox.Show(this, objSCM.PurchaseInvoice_Delete(gvInvoiceDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvInvoiceDetails.DataBind();
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

    #region Purchase Order Selected Index Changed
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierFixedPO objSCMPO = new SCM.SupplierFixedPO();
            if (objSCMPO.SuppliersFixedPO_Select(ddlPONo.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objSCMPO.FPODate;

                objSCMPO.SuppliersFixedPODetails_Select(objSCMPO.FPOId, gvItDetails);

                Masters.ItemMaster.PurchaseQuotationItemTypes1_Select(ddlPONo.SelectedItem.Value, ddlItemType);
                SCM.SuppliersMaster objSCMSM = new SCM.SuppliersMaster();
                if (objSCMSM.SuppliersMaster_Select(objSCMPO.SupId) > 0)
                {
                    ddlSupplierName.SelectedValue = objSCMSM.SupId;
                    txtSupplierName.Text = objSCMSM.SupName;
                    txtAddress.Text = objSCMSM.SupAddress;
                    txtEmail.Text = objSCMSM.SupEmail;
                    txtContactPerson.Text = objSCMSM.SupContactPerson;
                    txtPhoneNo.Text = objSCMSM.SupPhone;
                    txtMobileNo.Text = objSCMSM.SupMobile;
                }
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

    #region GridView gvItDetails Row Databound
    protected void gvItDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
         //   e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[5].Text) * Convert.ToDecimal(e.Row.Cells[6].Text));
         //   e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToShortDateString();
        }

    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblPIDetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtCST.Text == "") { txtCST.Text = "0"; }
        if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtExcise.Text == "") { txtExcise.Text = "0"; }
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);


        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = PurchaseInvoiceProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["VAT"] = gvrow.Cells[8].Text;
                dr["Excise"] = gvrow.Cells[10].Text;
                dr["Cst"] = gvrow.Cells[9].Text;
                dr["Amount"] = gvrow.Cells[11].Text;
                dr["ItemTypeId"] = gvrow.Cells[12].Text;

                PurchaseInvoiceProducts.Rows.Add(dr);
            }
        }

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemType.SelectedItem.Value)
                {
                    gvItemDetails.DataSource = PurchaseInvoiceProducts;
                    gvItemDetails.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = PurchaseInvoiceProducts.NewRow();
        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
        drnew["ItemType"] = ddlItemType.SelectedItem.Text;
        drnew["ItemName"] = txtModelName.Text;
        drnew["UOM"] = txtUOM.Text;
        drnew["Quantity"] = txtQuantity.Text;
        drnew["Rate"] = txtRate.Text;
        if (rbVAT.Checked == true)
        {
            drnew["VAT"] = txtVAT.Text;
            drnew["Cst"] = "0";
        }
        else if (rbCST.Checked == true)
        {
            drnew["VAT"] = "0";
            drnew["Cst"] = txtCST.Text;
        }
        drnew["Excise"] = txtExcise.Text;
        drnew["Amount"] = txtAmount.Text;

        PurchaseInvoiceProducts.Rows.Add(drnew);

        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
       // ddlItemName.SelectedValue = "0";
        txtUOM.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtVAT.Text = string.Empty;
        txtCST.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtExcise.Text = string.Empty;
        txtModelName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtColor.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        txtOrderedQuantity.Text = string.Empty;
    }
    #endregion

    //#region ddlItemName_SelectedIndexChanged
    //protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
    //        {
    //            txtUOM.Text = objMaster.ItemUOMShort;
    //            txtItemSpec.Text = objMaster.ItemSpec;
    //        }
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

    #region GridView  Items Row DataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                e.Row.Cells[10].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text) + ((Convert.ToDecimal(e.Row.Cells[8].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100) + ((Convert.ToDecimal(e.Row.Cells[9].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100) + ((Convert.ToDecimal(e.Row.Cells[10].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtGrossTotalAmtHidden.Value = txtGrossAmount.Text = GrossAmountCalc().ToString();
        }

    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }
    #endregion

    #region GridView  Items Row Deleting
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["VAT"] = gvrow.Cells[8].Text;
                    dr["Cst"] = gvrow.Cells[9].Text;
                    dr["Excise"] = gvrow.Cells[10].Text;
                    dr["Amount"] = gvrow.Cells[11].Text;
                    dr["ItemTypeId"] = gvrow.Cells[12].Text;

                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region GridView Invoice Details Row DataBound
    protected void gvInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Invoice Date")
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

    #region DropDownList Symbols Select Index Changed
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

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvInvoiceDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "Invoice Date")
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
        gvInvoiceDetails.DataBind();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvInvoiceDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PurchaseInvoice&pino=" + gvInvoiceDetails.SelectedRow.Cells[0].Text + "";
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
    #endregion



    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtUOM.Text = objMaster.ItemUOMShort;
                txtRate.Text = objMaster.ItemSeries;
                txtItemSpec.Text = objMaster.ItemSpec;

          Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }//ItemName_Fill();
    }
    #endregion

    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable PurchaseInvoiceProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("VAT");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Cst");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Excise");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);


        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {

            DataRow dr = PurchaseInvoiceProducts.NewRow();
            dr["ItemCode"] = gvrow.Cells[2].Text;
            dr["ItemType"] = gvrow.Cells[3].Text;
            dr["ItemName"] = gvrow.Cells[4].Text;
            dr["UOM"] = gvrow.Cells[5].Text;
            dr["Quantity"] = gvrow.Cells[6].Text;
            dr["Rate"] = gvrow.Cells[7].Text;
            dr["VAT"] = gvrow.Cells[8].Text;
            dr["Cst"] = gvrow.Cells[9].Text;
            dr["Excise"] = gvrow.Cells[10].Text;
            dr["Amount"] = gvrow.Cells[11].Text;
            dr["ItemTypeId"] = gvrow.Cells[12].Text;

            PurchaseInvoiceProducts.Rows.Add(dr);

            if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
            {
                ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                ddlItemType_SelectedIndexChanged(sender, e);
                //ItemName_Fill();
                //ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                //ddlItemName_SelectedIndexChanged(sender, e);
                txtModelName.Text = gvrow.Cells[2].Text;
                txtUOM.Text = gvrow.Cells[5].Text;
                txtQuantity.Text = gvrow.Cells[6].Text;
                txtRate.Text = gvrow.Cells[7].Text;
                txtVAT.Text = gvrow.Cells[8].Text;
                txtCST.Text = gvrow.Cells[9].Text;
                txtExcise.Text = gvrow.Cells[10].Text;
                if (txtVAT.Text != "0")
                {
                    rbVAT.Checked = true;
                    rbCST.Checked = false;
                }
                else if (txtCST.Text != "0")
                {
                    rbVAT.Checked = false;
                    rbCST.Checked = true;
                }




                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text) + ((Convert.ToDecimal(txtVAT.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100) + ((Convert.ToDecimal(txtCST.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100) + ((Convert.ToDecimal(txtExcise.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100));
                gvItemDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.PurchaseInvoice objSMSOApprove = new SCM.PurchaseInvoice();
            SCM.BeginTransaction();
            objSMSOApprove.PIId = gvInvoiceDetails.SelectedRow.Cells[0].Text;
            objSMSOApprove.PIApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMSOApprove.PurchaseInvoiceApprove_Update();
            SCM.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvInvoiceDetails.DataBind();
            SCM.Dispose();
            btnEdit_Click(sender, e);
        }
    }


    //protected void BtnGo_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow gvrow in gvItDetails.Rows)
    //    {
    //        CheckBox ch = new CheckBox();
    //        ch = (CheckBox)gvrow.FindControl("chkitemselect");
    //        if (ch.Checked == true)
    //        {
    //            DataTable PurchaseInvoiceProducts = new DataTable();
    //            DataColumn col = new DataColumn();
    //            col = new DataColumn("ItemCode");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("ItemType");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("ItemName");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("UOM");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Quantity");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Rate");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("VAT");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Cst");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Excise");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Amount");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("ItemTypeId");
    //            PurchaseInvoiceProducts.Columns.Add(col);


    //            if (gvItemDetails.Rows.Count > 0)
    //            {
    //                foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
    //                {
    //                    if (gvItemDetails.SelectedIndex > -1)
    //                    {
    //                        if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
    //                        {
    //                            DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                            dr["ItemCode"] = gvrow.Cells[2].Text;
    //                            dr["ItemType"] = gvrow.Cells[3].Text;
    //                            dr["ItemName"] = gvrow.Cells[4].Text;
    //                            dr["UOM"] = gvrow.Cells[5].Text;
    //                            dr["Quantity"] = gvrow.Cells[6].Text;
    //                          //  dr["Currency"] = gvrow.Cells[7].Text;
    //                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                            dr["Rate"] = gvrow.Cells[7].Text;
    //                            dr["VAT"] = "0";
    //                            dr["Cst"] = "0";
    //                            dr["Excise"] = "0";
    //                            dr["Amount"] = gvrow.Cells[11].Text;
    //                            dr["ItemTypeId"] = gvrow.Cells[12].Text;
    //                            PurchaseInvoiceProducts.Rows.Add(dr);
    //                        }
    //                        else
    //                        {
    //                            DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                            dr["ItemCode"] = gvrow1.Cells[2].Text;
    //                            dr["ItemType"] = gvrow1.Cells[3].Text;
    //                            dr["ItemName"] = gvrow1.Cells[4].Text;
    //                            dr["UOM"] = gvrow1.Cells[5].Text;
    //                            dr["Quantity"] = gvrow1.Cells[6].Text;
    //                            //  dr["Currency"] = gvrow.Cells[7].Text;
    //                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                            dr["Rate"] = gvrow1.Cells[7].Text;
    //                            dr["VAT"] = "0";
    //                            dr["Cst"] = "0";
    //                            dr["Excise"] = "0";
    //                            dr["Amount"] = gvrow1.Cells[11].Text;
    //                            dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
    //                            PurchaseInvoiceProducts.Rows.Add(dr);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                        dr["ItemCode"] = gvrow1.Cells[2].Text;
    //                        dr["ItemType"] = gvrow1.Cells[3].Text;
    //                        dr["ItemName"] = gvrow1.Cells[4].Text;
    //                        dr["UOM"] = gvrow1.Cells[5].Text;
    //                        dr["Quantity"] = gvrow1.Cells[6].Text;
    //                        //  dr["Currency"] = gvrow.Cells[7].Text;
    //                        // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                        dr["Rate"] = gvrow1.Cells[7].Text;
    //                        dr["VAT"] = "0";
    //                        dr["Cst"] = "0";
    //                        dr["Excise"] = "0";
    //                        dr["Amount"] = gvrow1.Cells[11].Text;
    //                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
    //                        PurchaseInvoiceProducts.Rows.Add(dr);
    //                    }
    //                    if (gvItemDetails.SelectedIndex == -1)
    //                    {
    //                        if (gvrow.Cells[2].Text == gvrow1.Cells[2].Text)
    //                        {
    //                            gvItemDetails.DataSource = PurchaseInvoiceProducts;
    //                            gvItemDetails.DataBind();
    //                            MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
    //                            btnItemRefresh_Click(sender, e);
    //                            ch.Checked = false;
    //                            return;
    //                        }
    //                    }

    //                }
    //            }
    //            if (gvItemDetails.SelectedIndex == -1)
    //            {
    //                DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                dr["ItemCode"] = gvrow.Cells[2].Text;
    //                dr["ItemType"] = gvrow.Cells[3].Text;
    //                dr["ItemName"] = gvrow.Cells[4].Text;
    //                dr["UOM"] = gvrow.Cells[5].Text;
    //                dr["Quantity"] = gvrow.Cells[6].Text;
    //                //  dr["Currency"] = gvrow.Cells[7].Text;
    //                // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                dr["Rate"] = gvrow.Cells[8].Text;
    //                dr["VAT"] = "0";
    //                dr["Cst"] = "0";
    //                dr["Excise"] = "0";
    //                dr["Amount"] = gvrow.Cells[11].Text;
    //                dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
    //                PurchaseInvoiceProducts.Rows.Add(dr);
    //            }
    //            gvItemDetails.DataSource = PurchaseInvoiceProducts;
    //            gvItemDetails.DataBind();
    //            gvItemDetails.SelectedIndex = -1;
    //            btnItemRefresh_Click(sender, e);
    //            ch.Checked = false;
    //        }


    //    }
    //}
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow gvrow in gvItDetails.Rows)
    //    {
    //        CheckBox ch = new CheckBox();
    //        ch = (CheckBox)gvrow.FindControl("chkItemSelect");
    //        if (ch.Checked == true)
    //        {
    //            DataTable PurchaseInvoiceProducts = new DataTable();
    //            DataColumn col = new DataColumn();
    //            col = new DataColumn("ItemCode");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("ItemType");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("ItemName");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("UOM");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Quantity");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Rate");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("VAT");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Cst");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Excise");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("Amount");
    //            PurchaseInvoiceProducts.Columns.Add(col);
    //            col = new DataColumn("ItemTypeId");
    //            PurchaseInvoiceProducts.Columns.Add(col);


    //            if (gvItemDetails.Rows.Count > 0)
    //            {
    //                foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
    //                {
    //                    if (gvItemDetails.SelectedIndex > -1)
    //                    {
    //                        if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
    //                        {
    //                            DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                            dr["ItemCode"] = gvrow.Cells[2].Text;
    //                            dr["ItemType"] = gvrow.Cells[3].Text;
    //                            dr["ItemName"] = gvrow.Cells[4].Text;
    //                            dr["UOM"] = gvrow.Cells[5].Text;
    //                            dr["Quantity"] = gvrow.Cells[6].Text;
    //                            //  dr["Currency"] = gvrow.Cells[7].Text;
    //                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                            dr["Rate"] = gvrow.Cells[7].Text;
    //                            dr["VAT"] = "0";
    //                            dr["Cst"] = "0";
    //                            dr["Excise"] = "0";
                              
    //                            //dr["ItemTypeId"] = gvrow.Cells[12].Text;
    //                            PurchaseInvoiceProducts.Rows.Add(dr);
    //                        }
    //                        else
    //                        {
    //                            DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                            dr["ItemCode"] = gvrow1.Cells[2].Text;
    //                            dr["ItemType"] = gvrow1.Cells[3].Text;
    //                            dr["ItemName"] = gvrow1.Cells[4].Text;
    //                            dr["UOM"] = gvrow1.Cells[5].Text;
    //                            dr["Quantity"] = gvrow1.Cells[6].Text;
    //                            //  dr["Currency"] = gvrow.Cells[7].Text;
    //                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                            dr["Rate"] = gvrow1.Cells[7].Text;
    //                            dr["VAT"] = "0";
    //                            dr["Cst"] = "0";
    //                            dr["Excise"] = "0";
                               
    //                            //dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
    //                            PurchaseInvoiceProducts.Rows.Add(dr);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                        dr["ItemCode"] = gvrow1.Cells[2].Text;
    //                        dr["ItemType"] = gvrow1.Cells[3].Text;
    //                        dr["ItemName"] = gvrow1.Cells[4].Text;
    //                        dr["UOM"] = gvrow1.Cells[5].Text;
    //                        dr["Quantity"] = gvrow1.Cells[6].Text;
    //                        //  dr["Currency"] = gvrow.Cells[7].Text;
    //                        // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                        dr["Rate"] = gvrow1.Cells[7].Text;
    //                        dr["VAT"] = "0";
    //                        dr["Cst"] = "0";
    //                        dr["Excise"] = "0";
                            
    //                        //dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
    //                        PurchaseInvoiceProducts.Rows.Add(dr);
    //                    }
    //                    if (gvItemDetails.SelectedIndex == -1)
    //                    {
    //                        if (gvrow.Cells[2].Text == gvrow1.Cells[2].Text)
    //                        {
    //                            gvItemDetails.DataSource = PurchaseInvoiceProducts;
    //                            gvItemDetails.DataBind();
    //                            MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
    //                            btnItemRefresh_Click(sender, e);
    //                            ch.Checked = false;
    //                            return;
    //                        }
    //                    }

    //                }
    //            }
    //            if (gvItemDetails.SelectedIndex == -1)
    //            {
    //                DataRow dr = PurchaseInvoiceProducts.NewRow();
    //                dr["ItemCode"] = gvrow.Cells[2].Text;
    //                dr["ItemType"] = gvrow.Cells[3].Text;
    //                dr["ItemName"] = gvrow.Cells[4].Text;
    //                dr["UOM"] = gvrow.Cells[5].Text;
    //                dr["Quantity"] = gvrow.Cells[6].Text;
    //                //  dr["Currency"] = gvrow.Cells[7].Text;
    //                // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
    //                dr["Rate"] = gvrow.Cells[8].Text;
    //                dr["VAT"] = "0";
    //                dr["Cst"] = "0";
    //                dr["Excise"] = "0";
                    
    //              //  dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
    //                PurchaseInvoiceProducts.Rows.Add(dr);
    //            }
    //            gvItemDetails.DataSource = PurchaseInvoiceProducts;
    //            gvItemDetails.DataBind();
    //            gvItemDetails.SelectedIndex = -1;
    //            btnItemRefresh_Click(sender, e);
    //            ch.Checked = false;
    //        }


    //    }
    //}
    protected void gvItDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
       
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);
       

        if (gvItDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItDetails.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemType"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["UOM"] = gvrow.Cells[4].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[5].Text;
             
                dr["DeliveryDate"] = gvrow.Cells[8].Text;
                dr["Specifications"] = gvrow.Cells[9].Text;
                dr["Remarks"] = gvrow.Cells[10].Text;
                dr["ItemTypeId"] = gvrow.Cells[11].Text;
               


                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvItDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[1].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    //ItemName_Fill();
                    txtModelName.Text = gvrow.Cells[3].Text;
                   // ddlItemName_SelectedIndexChanged(sender, e);
                    //txtUOM.Text = gvrow.Cells[4].Text;
                    txtOrderedQuantity.Text = gvrow.Cells[6].Text;
                    txtRate.Text = gvrow.Cells[5].Text;
                  
                 
                 
                  //  txtDeliveryD.Text = gvrow.Cells[8].Text;
                    //txtItemSpecifications.Text = gvrow.Cells[9].Text;
                    //txtItemRemarks.Text = gvrow.Cells[10].Text;
                    gvItDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvItDetails.DataSource = SalesOrderItems;
        gvItDetails.DataBind();
    }
    protected void ddlBrandselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemType, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrandselect.SelectedItem.Value);
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvInvoiceDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvInvoiceDetails.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvInvoiceDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
