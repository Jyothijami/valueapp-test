using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;
public partial class Modules_SCM_PurchaseInvoiceNew : basePage
{
    string invoiceNo = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        invoiceNo = Request.QueryString["invoiceNo"];
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGrossAmount.Text == "") { txtGrossAmount.Text = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtTotalAmt.Text == "" || txtTotalAmt.Text == null) { txtTotalAmt.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        if (txtGST.Text == "") { txtGST.Text = "0"; }
        if (lblCurrent_ttl.Text == "" || lblCurrent_ttl.Text == null) { lblCurrent_ttl.Text = "0"; }
        if (lblCurrent_GST.Text == "" || lblCurrent_GST.Text == null) { lblCurrent_GST.Text = "0"; }
        if (lblTtlAmt.Text == "" || lblTtlAmt.Text == null) { lblTtlAmt.Text = "0"; }
        if (lblTtlGSTAmt.Text == "" || lblTtlGSTAmt.Text == null) { lblTtlGSTAmt.Text = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(txtMiscelleneous.Text)
                  - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtTotalAmt.Text))) / 100));

        #endregion

        if (!IsPostBack)
        {
            setControlsVisibility();
            //SupplierFixedPO_Fill();
            //SupplierItems_Fill();
            SupplierName_Fill();
            DespatchMode_Fill();
            //ItemTypes_Fill();
            EmployeeMaster_Fill();
            // btnPrint.Visible = false;
            SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
            //gvItemDetails.RowDataBound += new GridViewRowEventHandler(gvItemDetails_RowDataBound);


            if (invoiceNo == null)
            {
                SCM.ClearControls(this);
                txtInvoiceNo.Text = SCM.PurchaseInvoice.PurchaseInvoice_AutoGenCode();
                txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true; ;
                tblPIDetails.Visible = true;
                gvItemDetails.DataBind();
                gvItDetails.DataBind();
            }
            else
            {
                try
                {
                    SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();

                    if (objSCM.PurchaseInvoice_Select(invoiceNo) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblPIDetails.Visible = true;
                        txtInvoiceNo.Text = objSCM.PINo;
                        txtInvoiceDate.Text = objSCM.PIDate;
                        btnSupSearch.Enabled = false;
                        ddlSupplierName.Enabled = false;
                        // txtPOOrders.Text = objSCM.FPOId;
                        ddlSupplierName.SelectedValue = objSCM.SUPId;
                        ddlSupplierName_SelectedIndexChanged(sender, e);
                        ddlPONo.SelectedValue = objSCM.FPOId;
                        //ddlPONo_SelectedIndexChanged(sender, e);
                        ddlInvoiceType.SelectedValue = objSCM.PIInvType;
                        txtGST.Text = objSCM.PIPackChrgs;
                        // txtPackingCharges.Text = objSCM.PIPackChrgs;
                        txtTranportationCharges.Text = objSCM.PITransChrgs;
                        txtTotalAmt.Text = objSCM.PIInsuranceChrgs;
                        txtMiscelleneous.Text = objSCM.PIMiscChrgs;
                        txtDiscount.Text = objSCM.PIDiscount;
                        txtGrossAmount.Text = objSCM.PIGrossAmt;
                        txtGrossTotalAmtHidden.Value = objSCM.PIGrossAmt;
                        txtTermsOfDelivery.Text = objSCM.PITermsofDelivery;
                        ddlDespatchMode.SelectedValue = objSCM.DESPMId;
                        txtRemarks.Text = objSCM.PIRemarks;
                        ddlPreparedBy.SelectedValue = objSCM.PIPreparedBy;
                        ddlApprovedBy.SelectedValue = objSCM.PIApprovedBy;
                        txtBank.Text = objSCM.PIBank;
                        txtChequeNo.Text = objSCM.PIChequeNo;
                        txtDate.Text = objSCM.PIChequeDate;
                        txtCustInvNo.Text = objSCM.PICustInvNo;
                        txtCustInvDate.Text = objSCM.PICustInvDate;
                        //objSCM.PurchaseInvoiceDetails_Select(invoiceNo, gvItemDetails);
                        txtpaybylc.Text = objSCM.PAYBYLC;
                        txtPaybytt.Text = objSCM.PAYBYTT;
                        txtLCdate.Text = objSCM.LCDATE;
                        txtlcexpdate.Text = objSCM.LCEXPDATE;
                        txtttdate.Text = objSCM.TTDATE;
                        txtVehicleNo.Text = objSCM.PIVehicleNo;
                        txtVehicalArrDt.Text = objSCM.PIVehicleArrDt;
                        objSCM.PurchaseInvoiceDetails1_Select(invoiceNo, gvPIDetails);
                        //BindMultiplePOS();
                        //SCM.SuppliersFixedPO objSupplierFixedPO = new SCM.SuppliersFixedPO();
                        //if (objSalesFPO.SuppliersFixedPO_Select(objPurchaseInvoice.FPONo) > 0)
                        //{
                        //    txtPackingCharges.Text = objSupplierFixedPO.PIPackChrgs;
                        //    txtTranportationCharges.Text = objSupplierFixedPO.PITransChrgs;
                        //    txtInsurance.Text = objSupplierFixedPO.PIInsuranceChrgs;
                        //    txtMiscelleneous.Text = objSupplierFixedPO.PIMiscChrgs;
                        //    txtDiscount.Text = objSupplierFixedPO.PIDiscount;
                        //    txtGrassAmount.Tex = objSupplierFixedPO.FPONetAmount;


                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    //btnDelete.Attributes.Clear();
                    SCM.Dispose();
                    //ddlPONo_SelectedIndexChanged(sender, e);

                }
            }


            txtPackingCharges.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtTranportationCharges.Attributes.Add("onkeyup", "javascript:grosscalc();");
            //rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            //rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            txtMiscelleneous.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtGST.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtGrossAmount.Attributes.Add("onkeyup", "javascript:grosscalc();");
            //txtVAT.Attributes.Add("onkeyup", "javascript:amtcalc();");
            //txtCST.Attributes.Add("onkeyup", "javascript:amtcalc();");
            //txtExcise.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtQuantity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();


        }
    }

    private void BindMultiplePOS()
    {
        int PI_ID = Convert.ToInt32(invoiceNo);
        SqlCommand cmd = new SqlCommand("select distinct(YANTRA_PURCHASE_INVOICE_DET.PI_PONo),[YANTRA_FIXED_PO_MAST].[FPO_NO] FROM YANTRA_PURCHASE_INVOICE_DET inner join [YANTRA_FIXED_PO_MAST] on [YANTRA_FIXED_PO_MAST].FPO_ID=YANTRA_PURCHASE_INVOICE_DET.PI_PONo where [PI_ID] = " + PI_ID + " ", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtPOOrders.Text = txtPOOrders.Text + dt.Rows[i][1].ToString() + " , ";

            }
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "24");

        btnApprove.Enabled = up.Approve;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        //btnPrint.Enabled = up.Print;
    }

    private void LoadInvoiceDetails()
    {
        try
        {
            SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();

            if (objSCM.PurchaseInvoice_Select(invoiceNo) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = true;
                tblPIDetails.Visible = true;
                txtInvoiceNo.Text = objSCM.PINo;
                txtInvoiceDate.Text = objSCM.PIDate;
                //ddlPONo.SelectedValue = objSCM.FPOId;
                ddlInvoiceType.SelectedValue = objSCM.PIInvType;
                ddlSupplierName.SelectedValue = objSCM.SUPId;

                txtPackingCharges.Text = objSCM.PIPackChrgs;
                txtTranportationCharges.Text = objSCM.PITransChrgs;
                txtInsurance.Text = objSCM.PIInsuranceChrgs;
                txtMiscelleneous.Text = objSCM.PIMiscChrgs;
                txtDiscount.Text = objSCM.PIDiscount;
                txtGrossAmount.Text = objSCM.PIGrossAmt;
                txtTermsOfDelivery.Text = objSCM.PITermsofDelivery;
                ddlDespatchMode.SelectedValue = objSCM.DESPMId;
                txtRemarks.Text = objSCM.PIRemarks;
                ddlPreparedBy.SelectedValue = objSCM.PIPreparedBy;
                ddlApprovedBy.SelectedValue = objSCM.PIApprovedBy;
                txtBank.Text = objSCM.PIBank;
                txtChequeNo.Text = objSCM.PIChequeNo;
                txtDate.Text = objSCM.PIChequeDate;
                txtCustInvNo.Text = objSCM.PICustInvNo;
                txtCustInvDate.Text = objSCM.PICustInvDate;
                objSCM.PurchaseInvoiceDetails1_Select(invoiceNo, gvPIDetails);
                txtpaybylc.Text = objSCM.PAYBYLC;
                txtPaybytt.Text = objSCM.PAYBYTT;
                txtLCdate.Text = objSCM.LCDATE;
                txtlcexpdate.Text = objSCM.LCEXPDATE;
                txtttdate.Text = objSCM.TTDATE;


                //SCM.SuppliersFixedPO objSupplierFixedPO = new SCM.SuppliersFixedPO();
                //if (objSalesFPO.SuppliersFixedPO_Select(objPurchaseInvoice.FPONo) > 0)
                //{
                //    txtPackingCharges.Text = objSupplierFixedPO.PIPackChrgs;
                //    txtTranportationCharges.Text = objSupplierFixedPO.PITransChrgs;
                //    txtInsurance.Text = objSupplierFixedPO.PIInsuranceChrgs;
                //    txtMiscelleneous.Text = objSupplierFixedPO.PIMiscChrgs;
                //    txtDiscount.Text = objSupplierFixedPO.PIDiscount;
                //    txtGrassAmount.Tex = objSupplierFixedPO.FPONetAmount;


                //}
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //btnDelete.Attributes.Clear();
            SCM.Dispose();
            //ddlPONo_SelectedIndexChanged(sender, e);

        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtGrossTotalAmtHidden.Value == "") { txtGrossTotalAmtHidden.Value = "0"; }
        if (txtMiscelleneous.Text == "") { txtMiscelleneous.Text = "0"; }
        if (txtDiscount.Text == "") { txtDiscount.Text = "0"; }
        txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) - (Convert.ToDecimal(txtDiscount.Text) * Convert.ToDecimal(txtGrossTotalAmtHidden.Value) / 100));


        if (rbVAT.Checked == true)
        {
            //txtVAT.Style.Add("display", "");
            lblVAT.Style.Add("display", "");
            //txtCST.Style.Add("display", "none");
            lblCSTax.Style.Add("display", "none");
        }
        else if (rbCST.Checked == true)
        {
            //txtVAT.Style.Add("display", "none");
            lblVAT.Style.Add("display", "none");
            //txtCST.Style.Add("display", "");
            lblCSTax.Style.Add("display", "");
        }

        #endregion
        if (invoiceNo != null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"]) && Request.QueryString["status"] != "&nbsp;")
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
            SCM.SuppliersMaster.SuppliersMast_Select(ddlSupplierName);
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
            Masters.ItemMaster.PurchaseQuotationItemTypes1_Select(ddlPONo.SelectedItem.Value, ddlItemType);
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
    #region PurchaseItems Fill
    private void SupplierItems_Fill()
    {
        try
        {
            //SCM.SupplierFixedPO.SuppliersFixedPOItems_Select(ddlSupItems);
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
    #region Purchase Order Fill
    private void SupplierFixedPO_Fill()
    {
        try
        {
            //SCM.SupplierFixedPO.SuppliersFixedPO_Select(ddlPONo);
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
    #region PurchaseInvoiceUpdate
    private void PurchaseInvoiceUpdate()
    {
        int Exists = 0;
        //if (gvItemDetails.Rows.Count > 0)
        //{
            try
            {
                SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
                SCM.BeginTransaction();
                objSCM.PIId = invoiceNo;
                objSCM.PINo = txtInvoiceNo.Text;
                objSCM.PIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
              
                
                //objSCM.FPOId = ddlPONo.SelectedItem.Value;
                
                
                //objSCM.FPOId = "0";
                // objSCM.FPOId = txtPOOrders.Text;

                objSCM.PIInvType = ddlInvoiceType.SelectedItem.Text;
                objSCM.SUPId = ddlSupplierName.SelectedItem.Value;
                //VAT Or CST
                //objSCM.PIGST=txtGST
                objSCM.PIPackChrgs = txtGST.Text;
                objSCM.PITransChrgs = txtTranportationCharges.Text;
                objSCM.PIInsuranceChrgs = txtTotalAmt.Text;
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
                // objSCM.PAYBYLC = txtLCdate.Text;
                objSCM.LCDATE = Yantra.Classes.General.toMMDDYYYY(txtLCdate.Text);
                objSCM.PAYBYLC = txtpaybylc.Text;
                objSCM.PAYBYTT = txtPaybytt.Text;
                objSCM.TTDATE = Yantra.Classes.General.toMMDDYYYY(txtttdate.Text);
                objSCM.PIVehicleNo = txtVehicleNo.Text;
                objSCM.PIVehicleArrDt = Yantra.Classes.General.toMMDDYYYY(txtVehicalArrDt.Text);


                if (objSCM.PurchaseInvoice_Update() == "Data Updated Successfully")
                {
                    //objSCM.PurchaseInvoiceDetails_Delete(objSCM.PIId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        //int count = CheckItemExistence(Convert.ToInt32(objSCM.PIId), Convert.ToInt32(gvrow.Cells[2].Text), Convert.ToInt32(gvrow.Cells[16].Text));
                        //if (count <= 0)
                        //{
                            objSCM.PIItemCode = gvrow.Cells[2].Text;
                            TextBox gst = (TextBox)gvrow.FindControl("txtDetGST");
                            objSCM.PIGST = gst.Text;
                            TextBox qty = (TextBox)gvrow.FindControl("txtQty");
                            objSCM.PIDetQty = qty.Text;
                            TextBox rate = (TextBox)gvrow.FindControl("txtRate");
                            objSCM.PIDetRate = rate.Text;
                            TextBox disc = (TextBox)gvrow.FindControl("txtDiscount");
                            objSCM.PIDetDisc = disc.Text;
                            objSCM.PIDetCustomer = gvrow.Cells[13].Text;
                            //objSCM.PIDetCst = gvrow.Cells[9].Text;
                            //objSCM.PIDetVat = gvrow.Cells[8].Text;
                            //objSCM.PIDetExcise = gvrow.Cells[10].Text;
                            Label amount = (Label)gvrow.FindControl("lblAmount");
                            objSCM.PIDetAmount = amount.Text;
                            objSCM.PIPONo = gvrow.Cells[17].Text;
                            //objSCM.PIPONo = gvrow.Cells[15].Text;
                            objSCM.PIColorId = gvrow.Cells[16].Text;
                            DropDownList ddlStatus = (DropDownList)gvrow.FindControl("ddlStatus");
                            objSCM.PIDetStatus = ddlStatus.SelectedItem.Value;
                            TextBox AppDeliveryDt = (TextBox)gvrow.FindControl("txtAppDeliveryDt");
                            objSCM.PIDetDeliveryDt = Yantra.Classes.General.toMMDDYYYY(AppDeliveryDt.Text);
                            objSCM.PurchaseInvoiceDetails_Save();
                        //}
                        //else
                        //{
                        //    lblErrorItemCode.Text = gvrow.Cells[3].Text;
                        //    Exists = 1;
                        //    break;
                        //}
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
                if (Exists != 1)
                {
                    UpdatePOItemsQty();
                    btnSave.Text = "Save";
                    gvItDetails.DataBind();
                    gvItemDetails.DataBind();
                    tblPIDetails.Visible = false;
                    SCM.ClearControls(this);
                    SCM.Dispose();
                }
                else if (Exists == 1)
                {
                    string error = " Model No." + lblErrorItemCode.Text + " Already Exists in This PI. Please delete the existing item and try again.";
                    MessageBox.Show(this, error);
                }
                else
                {
                    string error = "Unable to Save PI Details" + " Check Model No." + lblErrorItemCode.Text + " & try again";
                    MessageBox.Show(this, error);
                }
                //btnDelete.Attributes.Clear();
                //gvInvoiceDetails.DataBind();

                // Response.Redirect("PurchaseInvoice.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
    "alert(' Purchase Invoice Updated sucessfully');window.location ='PurchaseInvoice.aspx';", true);
            }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please add atleast one Item for Purchase Order");
        //}
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
                if (txtTotalAmt.Text == "") { txtTotalAmt.Text = "0"; }
                objSCM.PINo = txtInvoiceNo.Text;
                objSCM.PIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objSCM.FPOId = ddlPONo.SelectedItem.Value;
                //objSCM.FPOId = "0";
                // objSCM.FPOId = txtPOOrders.Text;

                objSCM.PIInvType = ddlInvoiceType.SelectedItem.Value;
                objSCM.SUPId = ddlSupplierName.SelectedItem.Value;
                //VAT Or CST
                objSCM.PIPackChrgs = txtGST.Text;
                //objSCM.PIPackChrgs = txtPackingCharges.Text;
                objSCM.PITransChrgs = txtTranportationCharges.Text;
                objSCM.PIInsuranceChrgs = txtTotalAmt.Text;
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
                // objSCM.LCEXPDATE = txtLCdate.Text;
                objSCM.LCDATE = Yantra.Classes.General.toMMDDYYYY(txtLCdate.Text);
                objSCM.PAYBYLC = txtpaybylc.Text;
                objSCM.PAYBYTT = txtPaybytt.Text;
                objSCM.TTDATE = Yantra.Classes.General.toMMDDYYYY(txtttdate.Text);
                objSCM.PIVehicleNo = txtVehicleNo.Text;
                objSCM.PIVehicleArrDt = Yantra.Classes.General.toMMDDYYYY(txtVehicalArrDt.Text);

                if (objSCM.PurchaseInvoice_Save() == "Data Saved Successfully")
                {
                    objSCM.PurchaseInvoiceDetails_Delete(objSCM.PIId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.PIItemCode = gvrow.Cells[2].Text;
                        TextBox gst = (TextBox)gvrow.FindControl("txtDetGST");
                        objSCM.PIGST = gst.Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQty");
                        objSCM.PIDetQty = qty.Text;
                        TextBox rate = (TextBox)gvrow.FindControl("txtRate");
                        objSCM.PIDetRate = rate.Text;
                        TextBox disc = (TextBox)gvrow.FindControl("txtDiscount");
                        objSCM.PIDetDisc = disc.Text;
                        objSCM.PIDetCustomer = gvrow.Cells[13].Text;
                        //objSCM.PIDetCst = gvrow.Cells[9].Text;
                        //objSCM.PIDetVat = gvrow.Cells[8].Text;
                        //objSCM.PIDetExcise = gvrow.Cells[10].Text;
                        Label amount = (Label)gvrow.FindControl("lblAmount");
                        objSCM.PIDetAmount = amount.Text;
                        objSCM.PIPONo = gvrow.Cells[17].Text;
                        //objSCM.PIPONo = gvrow.Cells[15].Text;
                        objSCM.PIColorId = gvrow.Cells[16].Text;
                        DropDownList ddlStatus = (DropDownList)gvrow.FindControl("ddlStatus");
                        objSCM.PIDetStatus = ddlStatus.SelectedItem.Value;
                        TextBox AppDeliveryDt = (TextBox)gvrow.FindControl("txtAppDeliveryDt");
                        objSCM.PIDetDeliveryDt =Yantra.Classes.General.toMMDDYYYY( AppDeliveryDt.Text);
                        objSCM.PurchaseInvoiceDetails_Save();

                        //if (objSCM.PurchaseInvoiceDetails_Save() == "Data Saved Successfully")
                        //{
                        //    UpdatePOItemsQty();
                        //}
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
                //btnDelete.Attributes.Clear();
                //gvInvoiceDetails.DataBind();
                UpdatePOItemsQty();
                gvItDetails.DataBind();
                gvItemDetails.DataBind();
                tblPIDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
                //  Response.Redirect("PurchaseInvoice.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
     "alert(' Purchase Invoice Saved sucessfully');window.location ='PurchaseInvoice.aspx';", true);
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    private void UpdatePOItemsQty()
    {
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            SCM.PurchaseInvoice objPI = new SCM.PurchaseInvoice();
            objPI.PIItemCode = gvrow.Cells[2].Text;
            objPI.PIColorId = gvrow.Cells[16].Text;
            TextBox qty = (TextBox)gvrow.FindControl("txtQty");
            objPI.PIDetQty = qty.Text;
            //objPI.PIDetQty = gvrow.Cells[6].Text;
            objPI.PIPOID = gvrow.Cells[18].Text;
            objPI.PIDetId = gvrow.Cells[17].Text;
            objPI.BalanceQty_Update();
        }
    }
    protected void ddlSupItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierFixedPO objSCMPO = new SCM.SupplierFixedPO();
            if (objSCMPO.SupItemsFixedPO_Select(ddlSupItems.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objSCMPO.FPODate;
                ddlPONo.SelectedValue = objSCMPO.FPOId;
                objSCMPO.SuppliersFixedPODetails_Select(objSCMPO.ItemCode, gvItDetails);
                Masters.ItemMaster.PurchaseQuotationItemTypes1_Select(ddlPONo.SelectedItem.Value, ddlItemType);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }



    #region Purchase Order Selected Index Changed
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierFixedPO objSCMPO = new SCM.SupplierFixedPO();
            if (objSCMPO.SuppliersFixedPO_Select(ddlPONo.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objSCMPO.FPODate;
                txtSPONo.Text = objSCMPO.FPONo;
                objSCMPO.SuppliersFixedPODetails_Select1(objSCMPO.FPOId, gvItDetails);

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
            // btnDelete.Attributes.Clear();
            SCM.Dispose();
        }

    }
    #endregion

    #region GridView gvItDetails Row Databound
    protected void gvItDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[19].Visible = false;
            //e.Row.Cells[15].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //decimal discount=(Convert.ToDecimal(qty.Text) * Convert.ToDecimal(rate.Text)*Convert.ToDecimal(disc.Text))/100;
            decimal discount = (Convert.ToDecimal(e.Row.Cells[7].Text) * Convert.ToDecimal(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[16].Text)) / 100;
            e.Row.Cells[9].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[7].Text) * Convert.ToDecimal(e.Row.Cells[8].Text) - discount);
            e.Row.Cells[10].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(e.Row.Cells[6].Text) / 100);
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
    private int CheckItemExistence(int PIId, int ItemCode, int ColorId)
    {
        SqlCommand cmd = new SqlCommand("select count(*) from YANTRA_PURCHASE_INVOICE_DET where PI_ID=" + PIId + " and ITEM_CODE=" + ItemCode + " and PI_ColorId=" + ColorId + " ", con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da2.Fill(dt);
        int IsExist = 0;
        return IsExist = Convert.ToInt32(dt.Rows[0][0].ToString());
    }

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPOOrders.Text == "")
        {
            txtPOOrders.Text = ddlPONo.SelectedItem.Text;
        }
        else
        {
            txtPOOrders.Text = txtPOOrders.Text + "," + ddlPONo.SelectedItem.Text;
        }
        if (txtGST.Text == "") { txtGST.Text = "0"; }
        //if (txtVAT.Text == "") { txtVAT.Text = "0"; }
        if (txtExcise.Text == "") { txtExcise.Text = "0"; }
        foreach (GridViewRow row in gvItDetails.Rows)
        {

            CheckBox ch = new CheckBox();
            ch = (CheckBox)row.FindControl("chk");
            if (ch.Checked == true)
            {
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
                col = new DataColumn("Discount");
                PurchaseInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("Cst");
                //PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SugParty");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PONo");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("HSN_Code");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("GST");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("GST_Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("FPO_DET_ID");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("FPO_ID");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Status");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("AppDeliveryDt");
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
                        dr["HSN_Code"] = gvrow.Cells[6].Text;
                        TextBox txtQty = (TextBox)gvrow.FindControl("txtQty");
                        dr["Quantity"] = txtQty.Text;
                        //dr["Quantity"] = gvrow.Cells[6].Text;
                        //dr["Rate"] = gvrow.Cells[7].Text;
                        TextBox txtRate = (TextBox)gvrow.FindControl("txtRate");
                        dr["Rate"] = txtRate.Text;
                        //dr["VAT"] = gvrow.Cells[8].Text;
                        //dr["Excise"] = gvrow.Cells[10].Text;
                        //dr["Cst"] = gvrow.Cells[9].Text;
                        TextBox txtDisc = (TextBox)gvrow.FindControl("txtDiscount");
                        dr["Discount"] = txtDisc.Text;
                        TextBox txtDetGST = (TextBox)gvrow.FindControl("txtDetGST");
                        dr["GST"] = txtDetGST.Text;
                        //dr["Amount"] = gvrow.Cells[11].Text;
                        Label lblAmt = (Label)gvrow.FindControl("lblAmount");
                        dr["Amount"] = lblAmt.Text;
                        Label lblGSTAmt = (Label)gvrow.FindControl("lblGSTAmount");
                        dr["GST_Amount"] = lblGSTAmt.Text;
                        dr["SugParty"] = gvrow.Cells[13].Text;
                        dr["ItemTypeId"] = gvrow.Cells[14].Text;
                        dr["PONo"] = gvrow.Cells[15].Text;
                        dr["ColorId"] = gvrow.Cells[16].Text;
                        dr["FPO_DET_ID"] = gvrow.Cells[17].Text;
                        dr["FPO_ID"] = gvrow.Cells[18].Text;
                        dr["Status"] = gvrow.Cells[19].Text;
                        dr["AppDeliveryDt"] = gvrow.Cells[20].Text;

                        PurchaseInvoiceProducts.Rows.Add(dr);
                    }
                }

                //if (gvItemDetails.Rows.Count > 0)
                //{
                //    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                //    {
                //        if (gvrow.Cells[2].Text == row.Cells[1].Text)
                //        {
                //            gvItemDetails.DataSource = PurchaseInvoiceProducts;
                //            gvItemDetails.DataBind();
                //            MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                //            return;
                //        }

                //    }
                //}

                DataRow drnew = PurchaseInvoiceProducts.NewRow();
                drnew["ItemCode"] = row.Cells[1].Text;
                drnew["ItemType"] = row.Cells[2].Text;
                drnew["ItemName"] = row.Cells[3].Text;
                drnew["UOM"] = row.Cells[4].Text;
                drnew["HSN_Code"] = row.Cells[5].Text;
                drnew["GST"] = row.Cells[6].Text;
                drnew["Quantity"] = row.Cells[7].Text;
                drnew["Rate"] = row.Cells[8].Text;
                //if (rbVAT.Checked == true)
                //{
                //    drnew["VAT"] = txtVAT.Text;
                //    drnew["Cst"] = "0";
                //}
                //else if (rbCST.Checked == true)
                //{
                //    drnew["VAT"] = "0";
                //    drnew["Cst"] = txtCST.Text;
                //}
                drnew["Discount"] = row.Cells[16].Text;
                drnew["Amount"] = row.Cells[9].Text;
                drnew["GST_Amount"] = row.Cells[10].Text;
                drnew["SugParty"] = row.Cells[15].Text;
                drnew["PONo"] = ddlPONo.SelectedItem.Value;
                drnew["ColorId"] = row.Cells[19].Text;
                drnew["FPO_DET_ID"] = row.Cells[20].Text;
                drnew["FPO_ID"] = row.Cells[21].Text;
                
                
                PurchaseInvoiceProducts.Rows.Add(drnew);

                gvItemDetails.DataSource = PurchaseInvoiceProducts;
                gvItemDetails.DataBind();
                ch.Checked = false;
                btnItemRefresh_Click(sender, e);
            }
        }
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
        txtGST.Text = string.Empty;
        //txtCST.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtExcise.Text = string.Empty;
        txtModelName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtBrand.Text = string.Empty;
        //txtColor.Text = string.Empty;
        ddlColor.SelectedValue = "0";
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        txtOrderedQuantity.Text = string.Empty;
    }
    #endregion
    double TotalAmt = 0, GSTTotalAmt = 0;
    protected void gvPIDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        GridViewRow gvrow = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[18].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Products list?');");
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = (Convert.ToDecimal(e.Row.Cells[10].Text) * Convert.ToDecimal(e.Row.Cells[6].Text) / 100).ToString();
            //TotalAmt += Convert.ToDouble(e.Row.Cells[10].Text);
            //GSTTotalAmt += Convert.ToDouble(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[10].Text = TotalAmt.ToString();
            //e.Row.Cells[11].Text = GSTTotalAmt.ToString();
            lblTtlAmt.Text = PIGrossAmount().ToString();
            lblTtlGSTAmt.Text = PIGSTAmount().ToString();
        }
    }

    #region GridView  Items Row DataBound
    public void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;

                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
                e.Row.Cells[15].Visible = false;

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {


            //e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;

            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;

            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;

        }
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            TextBox qty = (TextBox)row.FindControl("txtQty");
            TextBox rate = (TextBox)row.FindControl("txtRate");
            TextBox disc = (TextBox)row.FindControl("txtDiscount");
            TextBox gst = (TextBox)row.FindControl("txtDetGST");
            Label amount = (Label)row.FindControl("lblAmount");
            decimal discount = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(rate.Text) * Convert.ToDecimal(disc.Text)) / 100;
            amount.Text = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(rate.Text) - (discount)).ToString();
            Label gstamount = (Label)row.FindControl("lblGSTAmount");
            gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
        {
            if (lblTtlAmt.Text == "" || lblTtlAmt.Text == null) { lblTtlAmt.Text = "0"; }
            if (lblTtlGSTAmt.Text == "" || lblTtlGSTAmt.Text == null) { lblTtlGSTAmt.Text = "0"; }
            txtGrossTotalAmtHidden.Value = GrossAmountCalc().ToString();
            txtGST.Text = GSTTotalCalc().ToString();
            lblCurrent_ttl.Text = GrossAmountCalc().ToString();
            lblCurrent_GST.Text = GSTTotalCalc().ToString();
            txtTotalAmt.Text = (Convert.ToDecimal(lblTtlAmt.Text) + Convert.ToDecimal(lblCurrent_ttl.Text)).ToString();
            txtGST.Text = (Convert.ToDecimal(lblTtlGSTAmt.Text) + Convert.ToDecimal(lblCurrent_GST.Text)).ToString();
            lbltempGross_woDisc.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(txtMiscelleneous.Text));
            txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(lbltempGross_woDisc.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(lbltempGross_woDisc.Text))) / 100));

        }

    }

    private void loadSum(GridViewRow gridViewRow, GridViewRowEventArgs e)
    {
        e.Row.Cells[8].Text = "Total Amount:";
        e.Row.Cells[9].Text = GrossAmountCalc().ToString();
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            Label amount = (Label)gvrow.FindControl("lblAmount");
            if (amount.Text != "")
            {
                _totalAmt = _totalAmt + Convert.ToDouble(amount.Text);
            }
            else
            {
                _totalAmt = _totalAmt + 0;
            }
        }
        return _totalAmt;
    }
    #endregion
    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        LinkButton lbtnUpdate;
        lbtnUpdate = (LinkButton)sender;
        GridViewRow gvrow = (GridViewRow)lbtnUpdate.Parent.Parent;
        gvPIDetails.SelectedIndex = gvrow.RowIndex;
        //lbtnUpdate.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        lblPIItemCode.Text = gvPIDetails.SelectedRow.Cells[2].Text;
        lblPIColorId.Text = gvPIDetails.SelectedRow.Cells[15].Text;
        lblPIQty.Text = gvPIDetails.SelectedRow.Cells[7].Text;
        lblPIDetId.Text = gvPIDetails.SelectedRow.Cells[14].Text;
        lblPIId.Text = invoiceNo;

        SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
        DropDownList ddlStatus = (DropDownList)gvrow.FindControl("ddlStatus");
        objSCM.PIDetStatus = ddlStatus.SelectedItem.Value;
        TextBox AppDeliveryDt = (TextBox)gvrow.FindControl("txtAppDeliveryDt");
        objSCM.PIDetDeliveryDt = Yantra.Classes.General.toMMDDYYYY(AppDeliveryDt.Text);
        objSCM.PurchaseInvoiceStatus_Update(gvPIDetails.SelectedRow.Cells[14].Text);
        MessageBox.Show(this, "Data Updated");
        objSCM.PurchaseInvoiceDetails1_Select(invoiceNo, gvPIDetails);
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDelete;
        lbtnDelete = (LinkButton)sender;
        GridViewRow gvrow = (GridViewRow)lbtnDelete.Parent.Parent;
        gvPIDetails.SelectedIndex = gvrow.RowIndex;
        lbtnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        lblPIItemCode.Text = gvPIDetails.SelectedRow.Cells[2].Text;
        lblPIColorId.Text = gvPIDetails.SelectedRow.Cells[15].Text;
        lblPIQty.Text = gvPIDetails.SelectedRow.Cells[7].Text;
        lblPIDetId.Text = gvPIDetails.SelectedRow.Cells[14].Text;
        lblFPODetID.Text = gvPIDetails.SelectedRow.Cells[13].Text;
        lblPIId.Text = invoiceNo;
        
        SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
        int _true = objSCM.PurchaseInvoiceDetails_Delete1(gvPIDetails.SelectedRow.Cells[14].Text);
        if (_true == 1)
        {
            objSCM.PurchaseInvoiceDetails1_Select(invoiceNo, gvPIDetails);
            PO_Delete();
            lblTtlAmt.Text = PIGrossAmount().ToString();
            lblTtlGSTAmt.Text = PIGSTAmount().ToString();
            txtTotalAmt.Text = GrossAmountCalc().ToString();
            txtGST.Text = GSTTotalCalc().ToString();
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlGSTAmt.Text)).ToString();
            txtGST.Text = (Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(lblTtlGSTAmt.Text)).ToString();
            txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGST.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
            MessageBox.Show(this, "Data Deleted");
            objSCM.PurchaseInvoiceDetails1_Select(invoiceNo, gvPIDetails);
        }
        else
        {
            MessageBox.Show(this, "Unable to delete the data, Please contact admin");
        }
    }
    private void PO_Delete()
    {
        SCM.PurchaseInvoice objSCM = new SCM.PurchaseInvoice();
        objSCM.PIItemCode = lblPIItemCode.Text;
        objSCM.PIColorId = lblPIColorId.Text;
        objSCM.PIDetQty = lblPIQty.Text;
        objSCM.PIPOID = lblFPODetID.Text;
        objSCM.FPOBalanceQty_Update();
    }
    private double PIGrossAmount()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvPIDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[10].Text);
        }
        return _totalAmt;
    }
    private double PIGSTAmount()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvPIDetails.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }
    protected void gvPIDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvPIDetails.Rows[e.RowIndex].Cells[1].Text;
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
        col = new DataColumn("GST");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Discount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SugParty");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("PONo");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("PIID");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Status");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("AppDeliveryDt");
        PurchaseInvoiceProducts.Columns.Add(col);
        if (gvPIDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvPIDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["GST"] = gvrow.Cells[6].Text;
                    dr["Quantity"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;
                    dr["Discount"] = gvrow.Cells[9].Text;
                    dr["Amount "] = gvrow.Cells[10].Text;
                    dr["SugParty"] = gvrow.Cells[12].Text;
                    dr["PONo"] = gvrow.Cells[13].Text;
                    dr["PIID"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Status"] = gvrow.Cells[16].Text;
                    dr["AppDeliveryDt"] = gvrow.Cells[17].Text;
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        //btnItemRefresh_Click(sender, e);
    }

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
        col = new DataColumn("HSN_Code");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("GST");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Discount");
        PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Cst");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Amount");
        //PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("SugParty");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("PONo");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Status");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("AppDeliveryDt");
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
                    dr["HSN_Code"] = gvrow.Cells[6].Text;
                    dr["GST"] = gvrow.Cells[7].Text;
                    TextBox txtQty = (TextBox)gvrow.FindControl("txtQty");
                    dr["Quantity"] = txtQty.Text;
                    //dr["Quantity"] = gvrow.Cells[6].Text;
                    //dr["Rate"] = gvrow.Cells[7].Text;
                    TextBox txtRate = (TextBox)gvrow.FindControl("txtRate");
                    dr["Rate"] = txtRate.Text;
                    //dr["VAT"] = gvrow.Cells[8].Text;
                    //dr["Excise"] = gvrow.Cells[10].Text;
                    //dr["Cst"] = gvrow.Cells[9].Text;
                    TextBox txtDisc = (TextBox)gvrow.FindControl("txtDiscount");
                    dr["Discount"] = txtDisc.Text;
                    //dr["Amount"] = gvrow.Cells[11].Text;
                    //Label lblAmt = (Label)gvrow.FindControl("lblAmount");
                    //dr["Amount"] = lblAmt.Text;
                    dr["SugParty"] = gvrow.Cells[13].Text;
                    dr["ItemTypeId"] = gvrow.Cells[14].Text;
                    dr["PONo"] = gvrow.Cells[15].Text;
                    dr["ColorId"] = gvrow.Cells[16].Text;
                    dr["Status"] = gvrow.Cells[19].Text;
                    dr["AppDeliveryDt"] = gvrow.Cells[20].Text;
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        txtGST.Text = GSTTotalCalc().ToString();
        txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt.Text)).ToString();
        txtGST.Text = (Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(lblTtlGSTAmt.Text)).ToString();
        //btnItemRefresh_Click(sender, e);
    }
    #endregion
    private double GSTTotalCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            Label amount = (Label)gvrow.FindControl("lblGSTAmount");
            if (amount.Text != "")
            {
                _totalAmt = _totalAmt + Convert.ToDouble(amount.Text);
            }
            else
            {
                _totalAmt = _totalAmt + 0;
            }
        }
        return _totalAmt;
    }
    #region GridView Invoice Details Row DataBound
    protected void gvInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false;
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

                txtItemSpec.Text = objMaster.ItemSpec;

                Image1.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(ddlItemType.SelectedItem.Value) > 0)
                {
                    txtRate.Text = objrate.rsp;
                }
                Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedValue);

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
                //txtVAT.Text = gvrow.Cells[8].Text;
                //txtCST.Text = gvrow.Cells[9].Text;
                //txtExcise.Text = gvrow.Cells[10].Text;
                //if (txtVAT.Text != "0")
                //{
                //    rbVAT.Checked = true;
                //    rbCST.Checked = false;
                //}
                //else if (txtCST.Text != "0")
                //{
                //    rbVAT.Checked = false;
                //    rbCST.Checked = true;
                //}




                //txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text) + ((Convert.ToDecimal(txtVAT.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100) + ((Convert.ToDecimal(txtCST.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100) + ((Convert.ToDecimal(txtExcise.Text) / (Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text))) * 100));
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
            objSMSOApprove.PIId = invoiceNo;
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
            // gvInvoiceDetails.DataBind();
            SCM.Dispose();
            // btnEdit_Click(sender, e);
        }
    }

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




    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseInvoice.aspx");
    }
    decimal _ttlGSTAmt = 0, _ttlAmt = 0;
    protected void txtDetGST_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvItemDetails.Rows)
        {
            TextBox gst = (TextBox)gvr.FindControl("txtDetGST");
            TextBox qty = (TextBox)gvr.FindControl("txtQty");
            Label amount = (Label)gvr.FindControl("lblAmount");
            Label gstamount = (Label)gvr.FindControl("lblGSTAmount");
            TextBox rate = (TextBox)gvr.FindControl("txtRate");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            if (rate.Text != "" && qty.Text != "")
            {
                if (discount.Text != "")
                {
                    string disc, gst1;
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                    amount.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                    gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
                }
                else
                {
                    gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
                }
            }
            else
            {
                amount.Text = "0";
                gstamount.Text = "0";
            }
            lblTtlAmt.Text = PIGrossAmount().ToString();
            lblTtlGSTAmt.Text = PIGSTAmount().ToString();
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            txtGST.Text = GSTTotalCalc().ToString();
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt.Text)).ToString();
            txtGST.Text = (Convert.ToDecimal(txtGST.Text) + Convert.ToDecimal(lblTtlGSTAmt.Text)).ToString();
            txtGrossAmount.Text = Convert.ToString(Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtMiscelleneous.Text) + Convert.ToDecimal(txtGST.Text) - ((Convert.ToDecimal(txtDiscount.Text) * (Convert.ToDecimal(txtGrossTotalAmtHidden.Value))) / 100));
        }
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvItemDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtRate");
            TextBox qty = (TextBox)gvr.FindControl("txtQty");
            Label amount = (Label)gvr.FindControl("lblAmount");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            TextBox gst = (TextBox)gvr.FindControl("txtDetGST");
            Label gstamount = (Label)gvr.FindControl("lblGSTAmount");
            if (rate.Text != "" && qty.Text != "")
            {
                if (discount.Text != "")
                {
                    string disc;
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                    amount.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                    gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
                }
                else
                {
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
                }
            }
            else
            {
                amount.Text = "0";
            }
            _ttlAmt = (Convert.ToDecimal(GrossAmountCalc()) + (Convert.ToDecimal(lblTtlAmt.Text)));
            txtTotalAmt.Text = _ttlAmt.ToString();
            _ttlGSTAmt = (Convert.ToDecimal(GSTTotalCalc()) + (Convert.ToDecimal(lblTtlGSTAmt.Text)));
            txtGST.Text = _ttlGSTAmt.ToString();
        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvItemDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtRate");
            TextBox qty = (TextBox)gvr.FindControl("txtQty");
            Label amount = (Label)gvr.FindControl("lblAmount");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            TextBox gst = (TextBox)gvr.FindControl("txtDetGST");
            Label gstamount = (Label)gvr.FindControl("lblGSTAmount");
            if (rate.Text != "" && qty.Text != "")
            {
                if (discount.Text != "")
                {
                    string disc;
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                    amount.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                    gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
                }
                else
                {
                    amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                    gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
                }
            }
            else
            {
                amount.Text = "0";
            }
            _ttlAmt = (Convert.ToDecimal(GrossAmountCalc()) + (Convert.ToDecimal(lblTtlAmt.Text)));
            txtTotalAmt.Text = _ttlAmt.ToString();
            _ttlGSTAmt = (Convert.ToDecimal(GSTTotalCalc()) + (Convert.ToDecimal(lblTtlGSTAmt.Text)));
            txtGST.Text = _ttlGSTAmt.ToString();
        }
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvItemDetails.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtRate");
            TextBox qty = (TextBox)gvr.FindControl("txtQty");
            TextBox discount = (TextBox)gvr.FindControl("txtDiscount");
            Label amount = (Label)gvr.FindControl("lblAmount");
            TextBox gst = (TextBox)gvr.FindControl("txtDetGST");
            Label gstamount = (Label)gvr.FindControl("lblGSTAmount");
            string disc = "0";
            if (discount.Text != "")
            {
                amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                disc = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(discount.Text)) / 100).ToString();
                amount.Text = (Convert.ToDecimal(amount.Text) - Convert.ToDecimal(disc)).ToString();
                gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
            }
            else
            {
                amount.Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(qty.Text)).ToString();
                gstamount.Text = ((Convert.ToDecimal(amount.Text) * Convert.ToDecimal(gst.Text)) / 100).ToString();
            }
            GridViewRowEventArgs f = new GridViewRowEventArgs(gvr);
            //loadSum(gvr,f);
            _ttlAmt = (Convert.ToDecimal(GrossAmountCalc()) + (Convert.ToDecimal(lblTtlAmt.Text)));
            txtTotalAmt.Text = _ttlAmt.ToString();
            _ttlGSTAmt = (Convert.ToDecimal(GSTTotalCalc()) + (Convert.ToDecimal(lblTtlGSTAmt.Text)));
            txtGST.Text = _ttlGSTAmt.ToString();
        }
        //gvItemDetails_RowDataBound(null, null);


    }

    protected void btnSupSearch_Click(object sender, EventArgs e)
    {
        if (txtSearchSupp.Text != "")
        {
            ddlSupplierName.DataSourceID = "SqlDataSource";
            ddlSupplierName.DataTextField = "SUP_NAME";
            ddlSupplierName.DataValueField = "SUP_ID";
            ddlSupplierName.DataBind();
            if (ddlSupplierName.SelectedIndex > -1)
            {
                ddlSupplierName_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show(this, "Supplier Searched Does not Exists");
            }
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }

    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SCM.SuppliersMaster objSCMSM = new SCM.SuppliersMaster();
        if (objSCMSM.SuppliersMaster_Select(ddlSupplierName.SelectedItem.Value) > 0)
        {
            ddlSupplierName.SelectedValue = objSCMSM.SupId;
            txtSupplierName.Text = objSCMSM.SupName;
            txtAddress.Text = objSCMSM.SupAddress;
            txtEmail.Text = objSCMSM.SupEmail;
            txtContactPerson.Text = objSCMSM.SupContactPerson;
            txtPhoneNo.Text = objSCMSM.SupPhone;
            txtMobileNo.Text = objSCMSM.SupMobile;
            SCM.PurchaseInvoice objPI = new SCM.PurchaseInvoice();
            //objPI.PurchaseOrderDetailsByCustName_Select(ddlPONo, ddlSupplierName.SelectedItem.Value);
            
            objPI.POItemsDetailsBySupName_Select(ddlSupItems, ddlSupplierName.SelectedItem.Value);
        }
    }




    protected void btnSearchPO_Click(object sender, EventArgs e)
    {
        if (txtSearchPo.Text != "")
        {
            ddlPONo.DataSourceID = "SqlDS1";
            //ddlPONo.DataTextField = "IND_DET_SUGG_PARTY";
            ddlPONo.DataTextField = "fposugg";
            ddlPONo.DataValueField = "FPO_ID";
            ddlPONo.DataBind();
            if (ddlPONo.SelectedIndex > -1)
            {
                ddlPONo_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show(this, "PO Searched Does not Exists");
            }
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }

   
}

