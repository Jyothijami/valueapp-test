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
public partial class Modules_Services_AMCQuotationNew : basePage
{
    string quationId = "";
    ScriptManager ScriptManagerLocal;
    protected void Page_Load(object sender, EventArgs e)
    {
        quationId = Request.QueryString["quationId"];
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if(!IsPostBack)
        {
            if (quationId == "")
            {
                Services.ClearControls(this);
                gvQuotationItems.DataBind();
                gvpm.DataBind();
                // gvEnquiryProducts.DataBind();
                txtQuotationNo.Text = Services.AMCQuotation.AMCQuotation_AutoGenCode();
                txtQuotationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                tblQuotationDetails.Visible = true;
            }
            else
            {
                try
                {
                    Services.AMCQuotation objAMCQuotation = new Services.AMCQuotation();
                    if (objAMCQuotation.AMCQuotation_Select(quationId) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblQuotationDetails.Visible = true;

                        txtQuotationNo.Text = objAMCQuotation.AMCQTNo;
                        txtQuotationDate.Text = objAMCQuotation.AMCQTDate;
                        txtAMCPeriod.Text = objAMCQuotation.AMCQTPeriod;
                        ddlCRNo.SelectedValue = objAMCQuotation.CRId;
                        txtPMCalls.Text = objAMCQuotation.AMCQTPMCalls;
                        txtBreakDownCalls.Text = objAMCQuotation.AMCQTBreakDownCalls;
                        txtPaymentTerms.Text = objAMCQuotation.AMCQTPaymentTerms;
                        ddlCustomerName.SelectedValue = objAMCQuotation.CustId;
                        ddlCustomerName_SelectedIndexChanged(sender, e);
                        ddlUnitName.SelectedValue = objAMCQuotation.CustUnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                        ddlContactPerson.SelectedValue = objAMCQuotation.CustDetId;
                        txtCustPONo.Text = objAMCQuotation.AMCQTCustPONo;
                        txtCustPODate.Text = objAMCQuotation.AMCQTCustPODate;
                        txtServiceTax.Text = objAMCQuotation.AMCQTServiceTax;

                        ddlPreparedBy.SelectedValue = objAMCQuotation.AMCQTPreparedBy;
                        ddlApprovedBy.SelectedValue = objAMCQuotation.AMCQTApprovedBy;
                        txtValidity.Text = objAMCQuotation.AMCQTValidity;

                        objAMCQuotation.AMCQuotationDetails_Select(quationId, gvQuotationItems);
                        objAMCQuotation.AMCWorkOrderPMCallDetails_Select(quationId, gvpm);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    Services.Dispose();
                }
            }




            txtServiceTax.Attributes.Add("onkeyup", "javascript:grosscalc();");
            txtSerialNo.Attributes.Add("onkeyup", "javascript:Serialno();");
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

            ItemTypes_Fill();
            EmployeeMaster_Fill();
            CRNo_Fill();
            Customer_Fill();
            btnRegret.Attributes.Add("onclick", "return confirm('Are you sure you want to Regret this Quotation !');");
            SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");

            if (Request.QueryString["crid"] != null)
            {
               // btnNew_Click(sender, e);
                ////ddlCustomerName.SelectedValue = Request.QueryString["crid"].ToString();
                ddlCRNo.SelectedValue = Request.QueryString["crid"].ToString();
                Services.ComplaintRegister objCR = new Services.ComplaintRegister();
                if (objCR.ComplaintRegister_Select(Request.QueryString["crid"].ToString()) > 0)
                {
                    ddlCustomerName.SelectedValue = objCR.CustId;
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                    ddlUnitName.SelectedValue = objCR.CustUnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
                    ddlContactPerson.SelectedValue = objCR.CustDetId;
                }
                tblQuotationDetails.Visible = true;
            }
        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (txtSubTotal.Text == "" || txtSubTotal.Text == string.Empty) { txtSubTotal.Text = "0"; }
        if (txtServiceTax.Text == "" || txtServiceTax.Text == string.Empty) { txtServiceTax.Text = "0"; }
        txtTotalAmt.Text = Convert.ToString(double.Parse(txtSubTotal.Text) + (double.Parse(txtServiceTax.Text) * double.Parse(txtSubTotal.Text) / 100));

        if (quationId!=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["status"].ToString()) && (Request.QueryString["status"].ToString()) != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnSave.Visible = false;
                btnRegret.Visible = false;
                btnRefresh.Visible = false;
                //btnPrint.Visible = true;
                btnSend.Visible = true;
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRegret.Visible = true;
                btnRefresh.Visible = true;
                //btnPrint.Visible = false;
                btnSend.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnRegret.Visible = false;
            //btnPrint.Visible = false;
            btnSend.Visible = false;
        }

        if (btnApprove.Visible == false)
        {
            Panel1.Visible = false;
        }
        else
        {
            Panel1.Visible = true;
        }
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemType.ItemType_Select(ddlItemType);
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

    #region Customer Name Fill
    private void Customer_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomerName);
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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
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

    #region Ref No / CR No Fill
    private void CRNo_Fill()
    {
        try
        {
            Services.ComplaintRegister.ComplaintRegister_Select(ddlCRNo);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Services.Dispose();
        }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvpm.Rows.Count == int.Parse(txtPMCalls.Text))
        {
            if (btnSave.Text == "Save")
            {
                SalesQuotationSave();
            }
            else if (btnSave.Text == "Update")
            {
                SalesQuotationUpdate();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add PM Calls Schedule as per the PM Calls count. The PM Calls count should match with schedule records");
        }
    }
    #endregion

    #region SalesQuotationSave
    private void SalesQuotationSave()
    {
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                Services.AMCQuotation objsr = new Services.AMCQuotation();
                if (objsr.complaintrecord_isrecordexists(ddlCRNo.SelectedItem.Value) > 0)
                {
                    MessageBox.Show(this, "Qutation  " + ddlCRNo.SelectedItem.Text + " already prepared");
                    Yantra.Classes.General.ClearControls(this);
                    return;
                }
                Services.AMCQuotation objAMCQuotation = new Services.AMCQuotation();
                Services.BeginTransaction();

                objAMCQuotation.AMCQTNo = txtQuotationNo.Text;
                objAMCQuotation.AMCQTDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objAMCQuotation.AMCQTPeriod = txtAMCPeriod.Text;
                objAMCQuotation.CRId = ddlCRNo.SelectedItem.Value;
                objAMCQuotation.AMCQTPMCalls = txtPMCalls.Text;
                objAMCQuotation.AMCQTBreakDownCalls = txtBreakDownCalls.Text;
                objAMCQuotation.AMCQTPaymentTerms = txtPaymentTerms.Text;
                objAMCQuotation.CustId = ddlCustomerName.SelectedItem.Value;
                objAMCQuotation.CustUnitId = ddlUnitName.SelectedItem.Value;
                objAMCQuotation.CustDetId = ddlContactPerson.SelectedItem.Value;
                objAMCQuotation.AMCQTCustPONo = txtCustPONo.Text;
                objAMCQuotation.AMCQTCustPODate = Yantra.Classes.General.toMMDDYYYY(txtCustPODate.Text);
                objAMCQuotation.AMCQTServiceTax = txtServiceTax.Text;

                objAMCQuotation.AMCQTPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objAMCQuotation.AMCQTApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objAMCQuotation.AMCQTValidity = txtValidity.Text;

                if (objAMCQuotation.AMCQuotation_Save() == "Data Saved Successfully")
                {
                    objAMCQuotation.AMCQuotationDetails_Delete(objAMCQuotation.AMCQTId);
                    //foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                    //{

                    //    objAMCQuotation.ItemCode = gvrow.Cells[2].Text;
                    //    objAMCQuotation.AMCQTDetQty = gvrow.Cells[6].Text;
                    //    objAMCQuotation.AMCQTDetUnitPrice = gvrow.Cells[7].Text;
                    //    objAMCQuotation.AMCQTDetSerialNo = gvrow.Cells[5].Text;

                    //    objAMCQuotation.AMCQuotationDetails_Save();
                    //}
                    objAMCQuotation.AMCWorkOrderPMCallDetails_Delete(objAMCQuotation.AMCQTId);
                    foreach (GridViewRow gvrow in gvpm.Rows)
                    {
                        objAMCQuotation.WOCallName = gvrow.Cells[2].Text;
                        objAMCQuotation.WOCallDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                        objAMCQuotation.AMCWorkOrderPMCallDetails_Save();
                    }

                    Services.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    Services.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvQuotationDetails.DataBind();
                //   gvEnquiryProducts.DataBind();
               // gvQuotationItems.DataBind();
                tblQuotationDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region SalesQuotationUpdate
    private void SalesQuotationUpdate()
    {
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                Services.AMCQuotation objAMCQuotation = new Services.AMCQuotation();
                Services.BeginTransaction();

                objAMCQuotation.AMCQTId = quationId;
                objAMCQuotation.AMCQTNo = txtQuotationNo.Text;
                objAMCQuotation.AMCQTDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objAMCQuotation.AMCQTPeriod = txtAMCPeriod.Text;
                objAMCQuotation.CRId = ddlCRNo.SelectedItem.Value;
                objAMCQuotation.AMCQTPMCalls = txtPMCalls.Text;
                objAMCQuotation.AMCQTBreakDownCalls = txtBreakDownCalls.Text;
                objAMCQuotation.AMCQTPaymentTerms = txtPaymentTerms.Text;
                objAMCQuotation.CustId = ddlCustomerName.SelectedItem.Value;
                objAMCQuotation.CustUnitId = ddlUnitName.SelectedItem.Value;
                objAMCQuotation.CustDetId = ddlContactPerson.SelectedItem.Value;
                objAMCQuotation.AMCQTCustPONo = txtCustPONo.Text;
                objAMCQuotation.AMCQTCustPODate = Yantra.Classes.General.toMMDDYYYY(txtCustPODate.Text);
                objAMCQuotation.AMCQTServiceTax = txtServiceTax.Text;

                objAMCQuotation.AMCQTPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objAMCQuotation.AMCQTApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objAMCQuotation.AMCQTValidity = txtValidity.Text;

                if (objAMCQuotation.AMCQuotation_Update() == "Data Updated Successfully")
                {
                    objAMCQuotation.AMCQuotationDetails_Delete(objAMCQuotation.AMCQTId);
                    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                    {
                        objAMCQuotation.ItemCode = gvrow.Cells[2].Text;
                        objAMCQuotation.AMCQTDetQty = gvrow.Cells[6].Text;
                        objAMCQuotation.AMCQTDetUnitPrice = gvrow.Cells[7].Text;
                        objAMCQuotation.AMCQTDetSerialNo = gvrow.Cells[5].Text;

                        objAMCQuotation.AMCQuotationDetails_Save();
                    }

                    objAMCQuotation.AMCWorkOrderPMCallDetails_Delete(objAMCQuotation.AMCQTId);
                    foreach (GridViewRow gvrow in gvpm.Rows)
                    {
                        objAMCQuotation.WOCallName = gvrow.Cells[2].Text;
                        objAMCQuotation.WOCallDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[3].Text);
                        objAMCQuotation.AMCWorkOrderPMCallDetails_Save();
                    }

                    Services.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    Services.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
               // gvQuotationDetails.DataBind();
                //gvEnquiryProducts.DataBind();
              //  gvQuotationItems.DataBind();
                tblQuotationDetails.Visible = false;
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                rfvContactPerson.Enabled = true;
                rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
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
                if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
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


        //try
        //{
        //    SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    SM.Dispose();
        //}
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0 && ddlUnitName.SelectedValue != "0")
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

    #region GridView Enquiry Products Row Databound
    protected void gvEnquiryProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("AMCQuotation.aspx");
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvQuotationItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvQuotationItems.SelectedRow.RowIndex)
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemType"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ItemName"] = txtItemName.Text;
                        dr["ItemTypeId"] = ddlModelNo.SelectedItem.Value;
                        dr["Quantity"] = txtQunatity.Text;
                        dr["Rate"] = txtRate.Text;
                        dr["SerialNo"] = txtSerialNo.Text;

                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["SerialNo"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Rate"] = gvrow.Cells[7].Text;
                        dr["ItemTypeId"] = gvrow.Cells[9].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["SerialNo"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["ItemTypeId"] = gvrow.Cells[9].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }

        //if (gvQuotationItems.Rows.Count > 0)
        //{
        //    if (gvQuotationItems.SelectedIndex == -1)
        //    {
        //        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
        //        {
        //            if (gvrow.Cells[2].Text == ddlItemName.SelectedItem.Value)
        //            {
        //                gvQuotationItems.DataSource = QuotationItems;
        //                gvQuotationItems.DataBind();
        //                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
        //                return;
        //            }
        //        }
        //    }
        //}

        if (gvQuotationItems.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();
            drnew["ItemType"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ItemName"] = txtItemName.Text;
            drnew["Quantity"] = txtQunatity.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["ItemTypeId"] = ddlModelNo.SelectedItem.Value;
            drnew["SerialNo"] = txtSerialNo.Text;

            QuotationItems.Rows.Add(drnew);
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
        gvQuotationItems.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        //ddlModelNo.SelectedValue = "0";
        //   ddlItemName.SelectedValue = "0";
        txtItemName.Text = string.Empty;
        txtSerialNo.Text = string.Empty;
        txtQunatity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtItemSpec.Text = string.Empty;

    }
    #endregion

    #region ItemNames Select Index Changed
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemSpec.Text = objMaster.ItemSpec;
            }
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

    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[8].Text = (decimal.Parse(e.Row.Cells[6].Text) * decimal.Parse(e.Row.Cells[7].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            txtSubTotal.Text = GrossAmountCalc().ToString();
        }
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[8].Text);
        }
        return _totalAmt;
    }
    #endregion

    #region GridView Quotation Items Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["SerialNo"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["ItemTypeId"] = gvrow.Cells[9].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region GridView Quotation Items Row Editing
    protected void gvQuotationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["SerialNo"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["ItemTypeId"] = gvrow.Cells[9].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[9].Text;
                    ItemName_Fill();
                    ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    txtQunatity.Text = gvrow.Cells[6].Text;
                    txtRate.Text = gvrow.Cells[7].Text;
                    if (gvrow.Cells[5].Text != "-")
                    { txtSerialNo.Text = gvrow.Cells[5].Text; }
                    else
                    { txtSerialNo.Text = ""; }
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }

    #endregion

    #region GridView Quotation Details Row DataBound
    protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (quationId!=null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=amcquot&amcqno=" + quationId + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Send Button Click
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (quationId!=null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=amcquot&amcqno=" + quationId + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Follow Up Button Click
    protected void btnFollowUp_Click(object sender, EventArgs e)
    {
        if (quationId!=null)
        {
            gvFollowUp.DataBind();
            txtFollowUpName.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpName];
            if (tblFollowUp.Visible == false)
            {
                tblFollowUp.Visible = true;
            }
            else if (tblFollowUp.Visible == true)
            {
                tblFollowUp.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    #region Button FOLLOW UP CLOSE Click
    protected void btnFollowUpClose_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
    }
    #endregion

    #region Button FOLLOW UP SAVE Click
    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCQuotation objAMCQuotationQuotAssign = new Services.AMCQuotation();
            Services.BeginTransaction();
            objAMCQuotationQuotAssign.AMCQTId = quationId;
            objAMCQuotationQuotAssign.EmpId = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            objAMCQuotationQuotAssign.AMCQTFUDetDesc = txtFollowUpDesc.Text;
            objAMCQuotationQuotAssign.AMCQTFUDetDate = DateTime.Now.ToString();
            MessageBox.Show(this, objAMCQuotationQuotAssign.AMCQuotationFollowUp_Save());
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFollowUp.DataBind();
            txtFollowUpDesc.Text = string.Empty;
            Services.Dispose();
        }
    }
    #endregion

    #region Button FOLLOW UP REFRESH Click
    protected void btnFollowUpRefresh_Click(object sender, EventArgs e)
    {
        txtFollowUpDesc.Text = string.Empty;
    }
    #endregion

    #region Button FOLLOW UP HISTORY Click
    protected void btnFollowUpHistory_Click(object sender, EventArgs e)
    {
        if (tblFollowUpHistory.Visible == false)
        {
            tblFollowUpHistory.Visible = true;
        }
        else if (tblFollowUpHistory.Visible == true)
        {
            tblFollowUpHistory.Visible = false;
        }
    }
    #endregion

    #region Button CONFIRM YES Click
    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCQuotation objAMCQuotationQuotApprove = new Services.AMCQuotation();
            Services.BeginTransaction();
            objAMCQuotationQuotApprove.AMCQTId = quationId;
            objAMCQuotationQuotApprove.AMCQTApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objAMCQuotationQuotApprove.AMCQuotationApprove_Update();
            if (objAMCQuotationQuotApprove.Get_Ids_Select(quationId) > 0)
            {
                Services.AMCEnquiry.AMCEnquiryStatus_Update(Services.ServicesStatus.Open, objAMCQuotationQuotApprove.CRId);
                Services.AMCAssignments.AMCAssignmentsStatus_Update(Services.ServicesStatus.Open, objAMCQuotationQuotApprove.AssignTaskId);
            }
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           // gvQuotationDetails.DataBind();
            Services.Dispose();
            //btnEdit_Click(sender, e);
        }

        if (((Button)sender).Text == "Yes")
        {
            btnSend_Click(sender, e);
        }
    }
    #endregion

    #region Button APPROVE Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();

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

    protected void btnRegret_Click(object sender, EventArgs e)
    {
        try
        {
            Services.AMCQuotation objSMQuotApprove = new Services.AMCQuotation();
            Services.BeginTransaction();
            objSMQuotApprove.AMCQTId = quationId;
            objSMQuotApprove.AMCQTApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMQuotApprove.AMCQuotationRegret_Update();

            if (objSMQuotApprove.Get_Ids_Select1(quationId) > 0)
            {
                Services.SparesQuotation.CompalintRegisterStatus_Update(Services.ServicesStatus.Regret, objSMQuotApprove.CRId);
                Services.ServicesAssignments.ServicesAssignmentsStatus_Update(Services.ServicesStatus.Regret, objSMQuotApprove.AssignTaskId);
            }
            Services.CommitTransaction();
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           // gvQuotationDetails.DataBind();
            Services.Dispose();
            //btnEdit_Click(sender, e);
        }
    }
    protected void btnpmadd_Click(object sender, EventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("callname");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("calldate");
        QuotationItems.Columns.Add(col);

        if (gvpm.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvpm.Rows)
            {
                if (gvpm.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvpm.SelectedRow.RowIndex)
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["callname"] = txtCallName.Text;
                        dr["calldate"] = txtCallDate.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["callname"] = gvrow.Cells[2].Text;
                        dr["calldate"] = gvrow.Cells[3].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["callname"] = gvrow.Cells[2].Text;
                    dr["calldate"] = gvrow.Cells[3].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }


        if (gvpm.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();
            drnew["callname"] = txtCallName.Text;
            drnew["calldate"] = txtCallDate.Text;
            QuotationItems.Rows.Add(drnew);
        }
        gvpm.DataSource = QuotationItems;
        gvpm.DataBind();
        gvpm.SelectedIndex = -1;
        btnpmrefresh_Click(sender, e);
    }
    protected void btnpmrefresh_Click(object sender, EventArgs e)
    {
        txtCallName.Text = string.Empty;
        txtCallDate.Text = string.Empty;
    }
    protected void gvpm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gvpm_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvpm.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("callname");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("calldate");
        QuotationItems.Columns.Add(col);

        if (gvpm.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvpm.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["callname"] = gvrow.Cells[2].Text;
                    dr["calldate"] = gvrow.Cells[3].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvpm.DataSource = QuotationItems;
        gvpm.DataBind();
    }
    protected void gvpm_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("callname");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("calldate");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvpm.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["callname"] = gvrow.Cells[2].Text;
                dr["calldate"] = gvrow.Cells[3].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvpm.Rows[e.NewEditIndex].RowIndex)
                {
                    txtCallName.Text = gvrow.Cells[2].Text;
                    txtCallDate.Text = gvrow.Cells[3].Text;
                    gvpm.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvpm.DataSource = QuotationItems;
        gvpm.DataBind();
    }

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }

    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                // txtColor.Text = objMaster.Color;
                //  txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
            }
            // Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedValue);
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
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemType, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value);

    }
}
 
