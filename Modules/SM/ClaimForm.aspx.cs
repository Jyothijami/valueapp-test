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
using Yantra.MessageBox;
using YantraBLL.Modules;
using Yantra.Classes;
using vllib;
public partial class Modules_SM_ClaimForm : basePage
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            CustomerMaster_Fill();
            SupplierMaster_Fill();
           // ItemMaster_Fill();
            EmployeeMaster_Fill();
            //Customer_Fill();
            CurrencyType_Fill();
            //delivery_fill();
            City_Fill();
            Contact_Fill();
            insurancename_fill();
            SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");

            txtQty.Attributes.Add("onkeyup", "javascript:TotalPrice();");
            txtUnitPrice.Attributes.Add("onkeyup", "javascript:TotalPrice();");
            txtFob.Attributes.Add("onkeyup", "javascript:Total();");
            txtTotal.Attributes.Add("onkeyup", "javascript:Total();");
            txtCif.Attributes.Add("onkeyup", "javascript:Total();");
            txtPrice.Attributes.Add("onkeyup", "javascript:Total();");
            ddlConversion.Attributes.Add("onkeyup", "javascript:IRS();");
            txtDay.Attributes.Add("onkeyup", "javascript:IRS();");
            txtIrs.Attributes.Add("onkeyup", "javascript:IRS();");

            if (Request.QueryString["id"] != null)
            {
                string CFId = Request.QueryString["id"].ToString();
                foreach (GridViewRow gvRow in gvClaimForm.Rows)
                {
                    if (gvRow.Cells[0].Text == CFId)
                    {
                        gvClaimForm.SelectedIndex = gvRow.RowIndex;
                        tblClaimForm.Visible = true;
                        btnEdit_Click(sender, e);
                        return;
                    }
                }
            }
            #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (txtTotal.Text != "" && txtFob.Text != "" && txtCif.Text != "")
            {
                txtPrice.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + Convert.ToDecimal(txtFob.Text) + Convert.ToDecimal(txtCif.Text));
            }
            if (txtTota.Text != "" && txtDay.Text != "")
            {
                txtIrs.Text = Convert.ToString(Convert.ToDecimal(txtTota.Text) * Convert.ToDecimal(txtDay.Text));
            }
            #endregion

            tblClaimForm.Visible = false;
        }
    }

    private void insurancename_fill()
    {
        Masters.InsuranceMaster.Insurance_Select(ddlInsuranceCompany);
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "26");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnAdd.Enabled = up.add;
        //btnRefreshItems.Enabled = up.RefreshItems;
        //btnAdding.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnApprove.Enabled = up.Approve;
        //btnPrint.Enabled = up.Print;
        //btnClose.Enabled = up.Close;
    }

    #region   Contact Fill
    private void Contact_Fill()
    {
        try
        {

            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlPrincipal.SelectedItem.Value);



        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region City Fill
    private void City_Fill()
    {
        try
        {
            Masters.RegionalMaster.RegionalMaster_Select(ddlPort);
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

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomer);
            //SM.CustomerMaster.CustomerMasterDetails_Select(txtPhoneNo);
            //SM.CustomerMaster.CustomerMaster_Select(Value,ddlContactPerson);



        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region DropDownsReset
    private void DropDownsReset()
    {
        ddlUnitName.Items.Clear();
        ddlUnitName.Items.Add(new ListItem("--", "0"));
        ddlUnitName.Items.Add(new ListItem("-- Select Customer Name --", "0"));
    }
    #endregion

    #region Unit Name Fill
    private void UnitName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
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

    #region Currency Type Fill
    private void CurrencyType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlCurrency);
            Masters.CurrencyType.CurrencyType_Select(ddlConversion);
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

    #region ItemMaster Fill
    private void ItemMaster_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlDetails);
            //Masters.ItemMaster.ItemMaster_Select(ddlItem);
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

    #region SupplierMaster Fill
    private void SupplierMaster_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlPrincipal);
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

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        #region Do not delete or remove this region!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (txtTotal.Text != "" && txtFob.Text != "" && txtCif.Text != "")
        {
            txtPrice.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + Convert.ToDecimal(txtFob.Text) + Convert.ToDecimal(txtCif.Text));
        }
        if (txtTota.Text != "" && txtDay.Text != "")
        {
            txtIrs.Text = Convert.ToString(Convert.ToDecimal(txtTota.Text) * Convert.ToDecimal(txtDay.Text));
        }
        #endregion
    }
    #endregion

    #region LINK Button  ClaimForm Click
    protected void lbtnClaimFormNo_Click(object sender, EventArgs e)
    {

        tblClaimForm.Visible = false;
        LinkButton lbtnCliamFormNo;
        lbtnCliamFormNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCliamFormNo.Parent.Parent;
        gvClaimForm.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        //try
        //{

        //    SM.ClaimForm objcalimForm = new SM.ClaimForm();
        //    if (objcalimForm.ClaimForm_Select(gvClaimForm.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = false;
        //        tblClaimForm.Visible = true;
        //        txtReference.Text = objcalimForm.CfNo;
        //        txtDate.Text = objcalimForm.CfDate;
        //        ddlPrincipal.Text = objcalimForm.SupId;
        //        ddlCustomer.SelectedValue = objcalimForm.CustId;
        //        ddlCustomer_SelectedIndexChanged(sender, e);
        //        ddlUnitName.SelectedValue = objcalimForm.CustUnitId;
        //        ddlUnitName_SelectedIndexChanged(sender, e);
        //        ddlContactPerson.SelectedValue = objcalimForm.CustDetId;
        //        ddlContactPerson_SelectedIndexChanged(sender, e);
        //        txtRef.Text = objcalimForm.CfPoRefNo;
        //        txtFob.Text = objcalimForm.CfFOBDocCharges;
        //        txtTotal.Text = objcalimForm.CfTotalExworksFOB;
        //        ddlPort.SelectedValue = objcalimForm.CITYID;
        //        txtCif.Text = objcalimForm.CfCifCharges;
        //        txtTransfer.Text = objcalimForm.CfTranferCharges;
        //        txtPrice.Text = objcalimForm.CfTotalCifCharges;
        //        if (objcalimForm.CfTotalValue == "0")
        //        {
        //            txtTota.Text = string.Empty;
        //        }
        //        else
        //        {
        //            txtTota.Text = objcalimForm.CfTotalValue;
        //        }
        //        //txtTota.Text = objcalimForm.CfTotalValue;
        //        txtClaim.Text = objcalimForm.CfClaimValueUsd;
        //        ddlConversion.SelectedValue = objcalimForm.CURRENCYID;
        //        txtDay.Text = objcalimForm.CfCurValueAsPerDay;
        //        txtIrs.Text = objcalimForm.CfIrs;
        //        txtConsignee.Text = objcalimForm.CfConsigneeBillingAddress;
        //        txtPayment.Text = objcalimForm.CfPayment;
        //        txtAccount.Text = objcalimForm.CfAccountNo;
        //        txtDelivery.Text = objcalimForm.CfDelivery;
        //        txtSwift.Text = objcalimForm.CfSwift;
        //        txtInstructions.Text = objcalimForm.CfDeliveryInstructions;
        //        ddlPreparedBy.SelectedValue = objcalimForm.Preparedby;
        //        ddlApprovedBy.SelectedValue = objcalimForm.Approvedby;
        //        txtOrderDate.Text = objcalimForm.CfPoRefDate;
        //        ddlInsuranceCompany.SelectedValue = objcalimForm.InsCompany;
        //        txtAddress.Text = objcalimForm.InsAddress;
        //        txtInsContactperson.Text = objcalimForm.InsContactperson;
        //        txtTelephone.Text = objcalimForm.InsTelephone;

        //        //ddlApprovedBy.SelectedValue = objcalimForm.Approvedby;
        //        objcalimForm.ClaimFormDetails_Select(gvClaimForm.SelectedRow.Cells[0].Text, gvProductDetails);
        //        objcalimForm.ClaimTransferFormDetails_Select(gvClaimForm.SelectedRow.Cells[0].Text, gvItemDetails);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{

        //    SM.Dispose();
        //    //ddlDetails_SelectedIndexChanged(sender, e);
        //}

    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClaimFormNew.aspx");
        //SM.ClearControls(this);
        //DropDownsReset();
        //txtReference.Text = SM.ClaimForm.ClaimForm_AutoGenCode();
        //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //btnSave.Text = "Save";
        //btnSave.Enabled = true;
        //tblClaimForm.Visible = true;
        //gvItemDetails.DataBind();
        //gvProductDetails.DataBind();

    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ClaimFormSave();
        }
        else if (btnSave.Text == "Update")
        {
            ClaimFormUpdate();
        }
        DropDownsReset();
    }
    #endregion

    #region ClaimForm Save
    private void ClaimFormSave()
    {
        if (gvProductDetails.Rows.Count > 0)
        {
            if (gvItemDetails.Rows.Count > 0)
            {
                try
                {
                    SM.ClaimForm objclaimform = new SM.ClaimForm();
                    SM.BeginTransaction();
                    objclaimform.CfNo = txtReference.Text;
                    objclaimform.CfDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
                    objclaimform.SupId = ddlPrincipal.SelectedItem.Value;
                    objclaimform.CustId = ddlCustomer.SelectedItem.Value;
                    objclaimform.CustUnitId = ddlUnitName.SelectedItem.Value;
                    objclaimform.CustDetId = ddlContactPerson.SelectedItem.Value;
                    objclaimform.CfPoRefNo = txtRef.Text;
                    objclaimform.CfFOBDocCharges = txtFob.Text;
                    objclaimform.CfTotalExworksFOB = txtTotal.Text;
                    objclaimform.CfCifCharges = txtCif.Text;
                    objclaimform.CfTotalCifCharges = txtPrice.Text;
                    objclaimform.CfTranferCharges = "0";
                    if (txtTota.Text == string.Empty)
                    {
                        objclaimform.CfTotalValue = "0";
                    }
                    else
                    {
                        objclaimform.CfTotalValue = txtTota.Text;
                    }
                    //objclaimform.CfTotalValue = txtTota.Text;
                    objclaimform.CfClaimValueUsd = txtClaim.Text;
                    objclaimform.CURRENCYID = ddlConversion.Text;
                    objclaimform.CfCurValueAsPerDay = txtDay.Text;
                    objclaimform.CfIrs = txtIrs.Text;
                    objclaimform.CfConsigneeBillingAddress = txtConsignee.Text;
                    objclaimform.CfPayment = txtPayment.Text;
                    objclaimform.CfAccountNo = txtAccount.Text;
                    objclaimform.CfDelivery = txtDelivery.Text;
                    objclaimform.CfSwift = txtSwift.Text;
                    objclaimform.CfDeliveryInstructions = txtInstructions.Text;

                    objclaimform.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    objclaimform.Approvedby = ddlApprovedBy.SelectedItem.Value;
                    objclaimform.CITYID = ddlPort.Text;
                    objclaimform.CfPoRefDate = Yantra.Classes.General.toMMDDYYYY(txtOrderDate.Text);
                    objclaimform.InsTelephone = txtTelephone.Text;
                    objclaimform.InsContactperson = txtInsContactperson.Text;
                    objclaimform.InsAddress = txtAddress.Text;
                    objclaimform.InsCompany = ddlInsuranceCompany.SelectedItem.Value;

                    if (objclaimform.ClaimForm_Save() == "Data Saved Successfully")
                    {
                        objclaimform.ClaimFormDetails_Delete(objclaimform.CfId);
                        foreach (GridViewRow gvrow in gvProductDetails.Rows)
                        {
                            objclaimform.ItemCode = gvrow.Cells[1].Text;
                            objclaimform.CFProdDetQty = gvrow.Cells[3].Text;
                            objclaimform.CFProdDetUnitPrice = gvrow.Cells[4].Text;
                            objclaimform.CFProdDetCurrency = gvrow.Cells[7].Text;
                            //objclaimform.CFProdDetTotalPrice = gvrow.Cells[6].Text;
                            objclaimform.Detremarks = gvrow.Cells[8].Text;


                            objclaimform.ClaimFormDetails_Save();
                        }
                        objclaimform.ClaimTransferFormDetails_Delete(objclaimform.CfId);

                        foreach (GridViewRow gvrow in gvItemDetails.Rows)
                        {
                            objclaimform.CFItemCode = gvrow.Cells[1].Text;
                            objclaimform.CFTpDetValue = gvrow.Cells[3].Text;
                            objclaimform.CFTpDetClaimValue = gvrow.Cells[4].Text;

                            objclaimform.ClaimTransferFormDetails_Save();
                        }

                        SM.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
                }
                catch (Exception ex)
                {
                    SM.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    gvClaimForm.DataBind();
                    gvProductDetails.DataBind();
                    gvItemDetails.DataBind();
                    tblClaimForm.Visible = false;
                    SM.ClearControls(this);
                    SM.Dispose();
                }
            }
            else
            {
                MessageBox.Show(this, "Please Enter Transfer Details");
            }
        }
        else
        {
            MessageBox.Show(this, "Please Enter Product Details");
        }



    }
    #endregion

    #region ClaimForm Update
    private void ClaimFormUpdate()
    {

        try
        {
            SM.ClaimForm objclaimform = new SM.ClaimForm();

            SM.BeginTransaction();

            objclaimform.CfId = gvClaimForm.SelectedRow.Cells[0].Text;

            objclaimform.CfNo = txtReference.Text;
            objclaimform.CfDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
            objclaimform.SupId = ddlPrincipal.SelectedItem.Value;
            objclaimform.CustId = ddlCustomer.SelectedItem.Value;
            objclaimform.CustUnitId = ddlUnitName.SelectedItem.Value;
            objclaimform.CustDetId = ddlContactPerson.SelectedItem.Value;
            objclaimform.CfPoRefNo = txtRef.Text;
            objclaimform.CfFOBDocCharges = txtFob.Text;
            objclaimform.CfTotalExworksFOB = txtTotal.Text;
            objclaimform.CfCifCharges = txtCif.Text;
            objclaimform.CfTotalCifCharges = txtPrice.Text;
            objclaimform.CfTranferCharges = "0";
            if (txtTota.Text == string.Empty)
            {
                objclaimform.CfTotalValue = "0";
            }
            else
            {
                objclaimform.CfTotalValue = txtTota.Text;
            }
            //objclaimform.CfTotalValue = txtTota.Text;
            objclaimform.CfClaimValueUsd = txtClaim.Text;
            objclaimform.CURRENCYID = ddlConversion.SelectedItem.Value;
            objclaimform.CfCurValueAsPerDay = txtDay.Text;
            objclaimform.CfIrs = txtIrs.Text;
            objclaimform.CfConsigneeBillingAddress = txtConsignee.Text;
            objclaimform.CfPayment = txtPayment.Text;
            objclaimform.CfAccountNo = txtAccount.Text;
            objclaimform.CfDelivery = txtDelivery.Text;
            objclaimform.CfSwift = txtSwift.Text;
            objclaimform.CfDeliveryInstructions = txtInstructions.Text;
            objclaimform.CITYID = ddlPort.SelectedItem.Value;
            objclaimform.Approvedby = ddlApprovedBy.SelectedItem.Value;
            objclaimform.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objclaimform.CfPoRefDate = Yantra.Classes.General.toMMDDYYYY(txtOrderDate.Text);
            objclaimform.InsTelephone = txtTelephone.Text;
            objclaimform.InsContactperson = txtInsContactperson.Text;
            objclaimform.InsAddress = txtAddress.Text;
            objclaimform.InsCompany = ddlInsuranceCompany.SelectedItem.Value;

            if (objclaimform.ClaimForm_Update() == "Data Updated Successfully")
            {
                objclaimform.ClaimFormDetails_Delete(objclaimform.CfId);
                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    objclaimform.ItemCode = gvrow.Cells[1].Text;
                    objclaimform.CFProdDetQty = gvrow.Cells[3].Text;
                    objclaimform.CFProdDetCurrency = gvrow.Cells[7].Text;
                    objclaimform.CFProdDetUnitPrice = gvrow.Cells[4].Text;
                    objclaimform.Detremarks = gvrow.Cells[8].Text;
                    objclaimform.ClaimFormDetails_Save();
                }

                objclaimform.ClaimTransferFormDetails_Delete(objclaimform.CfId);

                foreach (GridViewRow gvrow in gvItemDetails.Rows)
                {
                    objclaimform.CFItemCode = gvrow.Cells[1].Text;
                    objclaimform.CFTpDetValue = gvrow.Cells[3].Text;
                    objclaimform.CFTpDetClaimValue = gvrow.Cells[4].Text;
                    objclaimform.ClaimTransferFormDetails_Save();
                }
            }

            SM.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");

        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            gvClaimForm.DataBind();
            tblClaimForm.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //tblClaimForm.Visible = true;

        if (gvClaimForm.SelectedIndex > -1)
        {
            Response.Redirect("ClaimFormNew.aspx?claimId=" + gvClaimForm.SelectedRow.Cells[0].Text);
            //try
            //{

            //    SM.ClaimForm objcalimForm = new SM.ClaimForm();
            //    if (objcalimForm.ClaimForm_Select(gvClaimForm.SelectedRow.Cells[0].Text) > 0)
            //    {
            //        btnSave.Text = "Update";
            //        btnSave.Enabled = true;
            //        tblClaimForm.Visible = true;
            //        txtReference.Text = objcalimForm.CfNo;
            //        txtDate.Text = objcalimForm.CfDate;
            //        ddlPrincipal.SelectedValue = objcalimForm.SupId;
            //        ddlCustomer.SelectedValue = objcalimForm.CustId;
            //        ddlCustomer_SelectedIndexChanged(sender, e);
            //        ddlUnitName.SelectedValue = objcalimForm.CustUnitId;
            //        ddlUnitName_SelectedIndexChanged(sender, e);
            //        ddlContactPerson.SelectedValue = objcalimForm.CustDetId;
            //        ddlContactPerson_SelectedIndexChanged(sender, e);
            //        txtRef.Text = objcalimForm.CfPoRefNo;

            //        txtFob.Text = objcalimForm.CfFOBDocCharges;
            //        txtTotal.Text = objcalimForm.CfTotalExworksFOB;
            //        ddlPort.SelectedValue = objcalimForm.CITYID;
            //        txtCif.Text = objcalimForm.CfCifCharges;
            //        txtTransfer.Text = objcalimForm.CfTranferCharges;
            //        txtPrice.Text = objcalimForm.CfTotalCifCharges;

            //        if (objcalimForm.CfTotalValue == "0")
            //        {
            //            txtTota.Text = string.Empty;

            //        }
            //        else
            //        {
            //            txtTota.Text = objcalimForm.CfTotalValue;
            //        }
            //        //txtTota.Text = objcalimForm.CfTotalValue;
            //        txtClaim.Text = objcalimForm.CfClaimValueUsd;
            //        ddlConversion.SelectedValue = objcalimForm.CURRENCYID;
            //        txtDay.Text = objcalimForm.CfCurValueAsPerDay;
            //        txtIrs.Text = objcalimForm.CfIrs;
            //        txtConsignee.Text = objcalimForm.CfConsigneeBillingAddress;
            //        txtPayment.Text = objcalimForm.CfPayment;
            //        txtAccount.Text = objcalimForm.CfAccountNo;
            //        txtDelivery.Text = objcalimForm.CfDelivery;
            //        txtSwift.Text = objcalimForm.CfSwift;
            //        txtInstructions.Text = objcalimForm.CfDeliveryInstructions;
            //        ddlPreparedBy.SelectedValue = objcalimForm.Preparedby;
            //        ddlApprovedBy.SelectedValue = objcalimForm.Approvedby;
            //        txtOrderDate.Text = objcalimForm.CfPoRefDate;
            //        ddlInsuranceCompany.SelectedValue = objcalimForm.InsCompany;
            //        txtAddress.Text = objcalimForm.InsAddress;
            //        txtInsContactperson.Text = objcalimForm.InsContactperson;
            //        txtTelephone.Text = objcalimForm.InsTelephone;

            //        //ddlApprovedBy.SelectedValue = objcalimForm.Approvedby;
            //        objcalimForm.ClaimFormDetails_Select(gvClaimForm.SelectedRow.Cells[0].Text, gvProductDetails);
            //        objcalimForm.ClaimTransferFormDetails_Select(gvClaimForm.SelectedRow.Cells[0].Text, gvItemDetails);
            //    }


            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message.ToString());
            //}
            //finally
            //{

            //    SM.Dispose();
            //   // ddlDetails_SelectedIndexChanged(sender, e);
            //}

        }
        else
        {
            MessageBox.Show(this, "Please select CF NO");
        }

    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvClaimForm.SelectedIndex > -1)
        {
            try
            {
                SM.ClaimForm objcalimForm = new SM.ClaimForm();

                MessageBox.Show(this, objcalimForm.ClaimForm_Delete(gvClaimForm.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvClaimForm.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
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
        SM.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblClaimForm.Visible = false;

    }
    #endregion

    #region gvClaimForm_RowDataBound
    protected void gvClaimForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "CF Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
    }
    #endregion

    #region DropDownList Symbols Select Index Changed
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            imgFromDate.Visible = true;
            ceSearchFrom.Enabled = true;
            MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {


        if (gvClaimForm.SelectedIndex <= -1)
        {
            gvClaimForm.SelectedIndex = -1;
            lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
            lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
            if (ceSearchFrom.Enabled == false)
            {
                lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
            }
            else if (ceSearchFrom.Enabled == true)
            {
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
            if (ceSearchValueToDate.Enabled == false)
            {
                lblSearchValueHidden.Text = txtSearchText.Text;
            }
            else if (ceSearchValueToDate.Enabled == true)
            {
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text);
                //lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text="00/00/0000");

                //MessageBox.Show(this, "Please Insert Valid Date");

            }

            gvClaimForm.DataBind();
        }

        else if (ddlSearchBy.SelectedItem.Text == "")
        {
            MessageBox.Show(this, "Please Select Search By ");
        }
        else if (txtSearchText.Text == "")
        {
            MessageBox.Show(this, "Please  Select Search Text Box");
        }


        else
        {
            MessageBox.Show(this, "Please Select a  record");
        }


    }
    #endregion

    //#region ddlCustomer_SelectedIndexChanged
    //protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    UnitName_Fill();
    //    Contact_Fill();

    //    try
    //    {

    //        SM.CustomerMaster objcust = new SM.CustomerMaster();
    //        if (objcust.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
    //        {
    //            ddlContact.SelectedItem.Value = objcust.ContactPerson;
    //            txtPhoneNo.Text = objcust.Phone;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        SM.Dispose();
    //    }
    //}
    //#endregion
    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                rfvContactPerson.Enabled = true;
                rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                rfvContactPerson.Enabled = false;
                rfvUnitName.Enabled = false;
                lblUnitAddress.Text = "Customer Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                    txtUnitAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtPhoneNo.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
            }
            SM.Dispose();

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }

    }
    #endregion

    #region ddlDetails_SelectedIndexChanged
    protected void ddlDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objitem = new Masters.ItemMaster();
            if (objitem.ItemMaster_Select(ddlDetails.SelectedItem.Value) > 0)
            {
                txtSpecification.Text = objitem.ItemSpec;
                //txtQty.Text = objitem.ItemQtyInHand;
               
            }
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


    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlDetails.SelectedValue = "0";
        ddlCurrency.SelectedValue = "0";
        txtSpecification.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtUnitPrice.Text = string.Empty;
        txtTotalPrice.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        gvProductDetails.SelectedIndex = -1;
    }
    #endregion

    #region btnRefresh
    protected void btnItem_Click(object sender, EventArgs e)
    {
        ddlItem.SelectedValue = "0";
        txtValue.Text = string.Empty;
        //txtFob.Text = string.Empty;
        //txtSpecify.Text = string.Empty;
        //txtTotal.Text = string.Empty;
        //txtCif.Text = string.Empty;
        //txtTransfer.Text = string.Empty;
        //txtPrice.Text = string.Empty;
        //ddlPort.SelectedValue = "0";
        gvItemDetails.SelectedIndex = -1;
    }

    #endregion

    #region gvProductDetails Row DataBound
    protected void gvProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[5].Text = (Convert.ToDecimal(e.Row.Cells[3].Text) * Convert.ToDecimal(e.Row.Cells[4].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtTotal.Text = TotalExWorks();
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            ddlItem.Items.Clear();
            ddlItem.Items.Add(new ListItem("--", "0"));
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ddlItem.Items.Add(new ListItem(e.Row.Cells[2].Text, e.Row.Cells[1].Text));
        }
    }

    private string TotalExWorks()
    {
        decimal totalValue = 0;
        foreach (GridViewRow gvRow in gvProductDetails.Rows)
        {
            totalValue = totalValue + decimal.Parse(gvRow.Cells[5].Text);
        }
        return totalValue.ToString();
    }
    #endregion

    #region gvItemDetails Row DataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = Convert.ToString(decimal.Parse(GetAmountValue(e.Row.Cells[1].Text)) - decimal.Parse(e.Row.Cells[3].Text));
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtTota.Text = TotalClaimValue();
        }
    }
    private string GetAmountValue(string ItemCode)
    {
        string AmountValue = "0";
        foreach (GridViewRow gvRow in gvProductDetails.Rows)
        {
            if (gvRow.Cells[1].Text == ItemCode)
            {
                AmountValue = gvRow.Cells[5].Text;
            }
        }
        return AmountValue;
    }

    private string TotalClaimValue()
    {
        string totalClaimValue = "0";
        foreach (GridViewRow gvRow in gvItemDetails.Rows)
        {
            totalClaimValue = Convert.ToString(decimal.Parse(totalClaimValue) + decimal.Parse(gvRow.Cells[4].Text));
        }
        return totalClaimValue;
    }

    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtSpecification.Text == "") { txtSpecification.Text = "-"; }

        DataTable ClaimFormProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Currency");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("CurrencyName");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        ClaimFormProducts.Columns.Add(col);

        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                if (gvProductDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvProductDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = ClaimFormProducts.NewRow();
                        dr["ItemCode"] = ddlDetails.SelectedItem.Value;
                        dr["ItemName"] = ddlDetails.SelectedItem.Text;
                        dr["Quantity"] = txtQty.Text;
                        dr["UnitPrice"] = txtUnitPrice.Text;
                        dr["CurrencyName"] = ddlCurrency.SelectedItem.Text;
                        dr["Currency"] = ddlCurrency.SelectedItem.Value;
                        dr["Remarks"] = txtRemarks.Text;
                        ClaimFormProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = ClaimFormProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[1].Text;
                        dr["ItemName"] = gvrow.Cells[2].Text;
                        dr["Quantity"] = gvrow.Cells[3].Text;
                        dr["UnitPrice"] = gvrow.Cells[4].Text;
                        dr["CurrencyName"] = gvrow.Cells[6].Text;
                        dr["Currency"] = gvrow.Cells[7].Text;
                        dr["Remarks"] = gvrow.Cells[8].Text;
                        ClaimFormProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = ClaimFormProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Quantity"] = gvrow.Cells[3].Text;
                    dr["UnitPrice"] = gvrow.Cells[4].Text;
                    dr["CurrencyName"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;
                    ClaimFormProducts.Rows.Add(dr);
                }
            }
        }

        if (gvProductDetails.Rows.Count > 0)
        {
            if (gvProductDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvProductDetails.Rows)
                {
                    if (gvrow.Cells[0].Text == ddlDetails.SelectedItem.Text)
                    {
                        gvProductDetails.DataSource = ClaimFormProducts;
                        gvProductDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvProductDetails.SelectedIndex == -1)
        {
            DataRow drnew = ClaimFormProducts.NewRow();
            drnew["ItemCode"] = ddlDetails.SelectedItem.Value;
            drnew["ItemName"] = ddlDetails.SelectedItem.Text;
            drnew["Quantity"] = txtQty.Text;
            drnew["UnitPrice"] = txtUnitPrice.Text;
            drnew["CurrencyName"] = ddlCurrency.SelectedItem.Text;
            drnew["Currency"] = ddlCurrency.SelectedItem.Value;
            drnew["Remarks"] = txtRemarks.Text;
            ClaimFormProducts.Rows.Add(drnew);
        }
        gvProductDetails.DataSource = ClaimFormProducts;
        gvProductDetails.DataBind();
        gvProductDetails.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region gvProductDetails Items Row Deleting
    protected void gvProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvProductDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable ClaimFormProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("UnitPrice");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Currency");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("CurrencyName");
        ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        ClaimFormProducts.Columns.Add(col);

        if (gvProductDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvProductDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = ClaimFormProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Quantity"] = gvrow.Cells[3].Text;
                    dr["UnitPrice"] = gvrow.Cells[4].Text;
                    dr["CurrencyName"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;
                    ClaimFormProducts.Rows.Add(dr);
                }
            }
        }
        gvProductDetails.DataSource = ClaimFormProducts;
        gvProductDetails.DataBind();
    }
    #endregion

    #region Button Add
    protected void btnAdding_Click(object sender, EventArgs e)
    {

        DataTable ClaimFormTransferProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("Value");
        ClaimFormTransferProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvItemDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = ClaimFormTransferProducts.NewRow();
                        dr["ItemCode"] = ddlItem.SelectedItem.Value;
                        dr["ItemName"] = ddlItem.SelectedItem.Text;
                        dr["Value"] = txtValue.Text;
                        ClaimFormTransferProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = ClaimFormTransferProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[1].Text;
                        dr["ItemName"] = gvrow.Cells[2].Text;
                        dr["Value"] = gvrow.Cells[3].Text;
                        ClaimFormTransferProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = ClaimFormTransferProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Value"] = gvrow.Cells[3].Text;
                    ClaimFormTransferProducts.Rows.Add(dr);
                }
            }
        }

        if (gvItemDetails.Rows.Count > 0)
        {
            if (gvItemDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItemDetails.Rows)
                {
                    if (gvrow.Cells[0].Text == ddlItem.SelectedItem.Text)
                    {
                        gvItemDetails.DataSource = ClaimFormTransferProducts;
                        gvItemDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvItemDetails.SelectedIndex == -1)
        {
            DataRow drnew = ClaimFormTransferProducts.NewRow();
            drnew["ItemCode"] = ddlItem.SelectedItem.Value;
            drnew["ItemName"] = ddlItem.SelectedItem.Text;
            drnew["Value"] = txtValue.Text;
            ClaimFormTransferProducts.Rows.Add(drnew);
        }
        gvItemDetails.DataSource = ClaimFormTransferProducts;
        gvItemDetails.DataBind();
        gvItemDetails.SelectedIndex = -1;
        btnItem_Click(sender, e);
    }
    #endregion

    #region gvItemDetails Items Row Deleting
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable ClaimFormTransferProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        ClaimFormTransferProducts.Columns.Add(col);
        //col = new DataColumn("ItemType");
        //ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        ClaimFormTransferProducts.Columns.Add(col);
        //col = new DataColumn("UOM");
        //ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("Value");
        ClaimFormTransferProducts.Columns.Add(col);

        col = new DataColumn("Specifications");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("FOBCharges");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("TotalFob");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("CIFCharges");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("TransferCharges");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("TotalCIFPrice");
        ClaimFormTransferProducts.Columns.Add(col);
        col = new DataColumn("Total");
        ClaimFormTransferProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = ClaimFormTransferProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    //dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    //dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Value"] = gvrow.Cells[3].Text;
                    dr["Specifications"] = gvrow.Cells[4].Text;
                    dr["FOBCharges"] = gvrow.Cells[5].Text;
                    dr["TotalFob"] = gvrow.Cells[6].Text;
                    dr["CIFCharges"] = gvrow.Cells[7].Text;
                    dr["TransferCharges"] = gvrow.Cells[8].Text;
                    dr["TotalCIFPri"] = gvrow.Cells[9].Text;
                    dr["Total"] = gvrow.Cells[10].Text;
                    ClaimFormTransferProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = ClaimFormTransferProducts;
        gvItemDetails.DataBind();
    }
    #endregion

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvClaimForm.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=claimform&cfid=" + gvClaimForm.SelectedRow.Cells[0].Text + "";
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

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtPhoneNo.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlContactPerson_SelectedIndexChanged
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtEmail.Text = objSMCustomer.CustCorpEmail;
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
                txtMobile.Text = objSMCustomer.CustCorpMobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    protected void ddlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SuppliersMaster objSupp = new SCM.SuppliersMaster();
            if (objSupp.SuppliersMaster_Select(ddlPrincipal.SelectedItem.Value) > 0)
            {
                txtPrincipalContact.Text = objSupp.SupContactPerson;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SM.ClaimForm objSMSOApprove = new SM.ClaimForm();
            SM.BeginTransaction();
            objSMSOApprove.CfId = gvClaimForm.SelectedRow.Cells[0].Text;
            objSMSOApprove.Approvedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMSOApprove.ClaimFormApprove_Update();
            SM.CommitTransaction();
            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvClaimForm.DataBind();
            SM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    protected void ddlBrandselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlDetails, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrandselect.SelectedItem.Value);

    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
       // ItemMaster_Fill();

        ddlDetails.DataSourceID = "SqlDataSource2";
        ddlDetails.DataTextField = "ITEM_MODEL_NO";
        ddlDetails.DataValueField = "ITEM_CODE";
        ddlDetails.DataBind();
        ddlDetails_SelectedIndexChanged(sender, e);
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvClaimForm.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvClaimForm.DataBind();
    }
}
 
