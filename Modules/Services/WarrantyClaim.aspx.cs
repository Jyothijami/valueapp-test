//Date Written      Written By

//29/Apr/2009        L.Hima Kishore    
//16/Mar/2009      A.Vishnu Prasad


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
public partial class Modules_Services_WarrantyClaim : basePage
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerMaster_Fill();
            ItemTypes_Fill();
            EmployeeMaster_Fill();
            tblWarrantyDetails.Visible = false;
            setControlsVisibility();

        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "40");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;
        //btnApprove.Enabled = up.add;
        //btnSend.Enabled = up.Email;
        btnPrint.Enabled = up.Print;
        //btnClose.Enabled = up.add;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvWarrantyClaim.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvWarrantyClaim.SelectedRow.Cells[6].Text) && gvWarrantyClaim.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                // btnSave.Visible = false;
                btnRefresh.Visible = false;

            }
            else
            {
                btnSave.Visible = true;
                btnRefresh.Visible = false;

            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;

        }
    }
    #endregion

    #region Customer Master Fill
    private void CustomerMaster_Fill()
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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlModel, ddlItemType.SelectedValue);

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
            // HR.EmployeeMaster.EmployeeMaster_Select(ddlResponsiblePerson);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlCheckedBy);
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

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarrantyClaimNew.aspx");
        //try
        //{
        //    gvWarrantyClaim.SelectedIndex = -1;
        //    Services.ClearControls(this);
        //    txtSupplierName.Text = "Value Line Trade Pvt Ltd.";
        //    txtWCNo.Text = Services.WarrantyClaim.WarrantyClaim_AutoGenCode();
        //    txtWCDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //    //btnSave.Text = "Save";
        //    tblWarrantyDetails.Visible = true;
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            WarrantyClaimSave();
        }
        else if (btnSave.Text == "Update")
        {
            WarrantyClaimUpdate();
        }
    }
    #endregion

    #region Warranty Claim Save
    private void WarrantyClaimSave()
    {
        try
        {
            Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();


            objWarrantyClaim.WCNo = txtWCNo.Text;
            objWarrantyClaim.WCDate = Yantra.Classes.General.toMMDDYYYY(txtWCDate.Text);
            objWarrantyClaim.WCInvoiceNo = txtSupInvoiceNo.Text;
            objWarrantyClaim.WCInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtSupplierDate.Text);
            objWarrantyClaim.WCInstDate = Yantra.Classes.General.toMMDDYYYY(txtInstallationDate.Text);
            objWarrantyClaim.CustId = ddlCustomerName.SelectedItem.Value;
            objWarrantyClaim.CustUnitId = ddlUnitName.SelectedItem.Value;
            objWarrantyClaim.CustDetId = ddlContactPerson.SelectedItem.Value;

            objWarrantyClaim.ItemCode = ddlModel.SelectedItem.Value;
            objWarrantyClaim.WCItemsSlNo = txtSLNo.Text;
            objWarrantyClaim.WCQty = txtQuantity.Text;

            objWarrantyClaim.WCWarrantyExpOn = Yantra.Classes.General.toMMDDYYYY(txtExpires.Text);
            objWarrantyClaim.WCWarrantyClaimsOn = Yantra.Classes.General.toMMDDYYYY(txtClaimed.Text);

            objWarrantyClaim.WCProbNature = txtProblemNature.Text;
            objWarrantyClaim.WCSerEngRemarks = txtServiceRemarks.Text;
            objWarrantyClaim.WCSMPLRemarks = txtSmplRemarks.Text;
            objWarrantyClaim.WCRemarksNeeds = txtRemarksNeeds.Text;
            objWarrantyClaim.WCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWarrantyClaim.WCApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWarrantyClaim.WCSupplierName = txtSupplierName.Text;
            objWarrantyClaim.WCItemDesc = txtDescription.Text;
            objWarrantyClaim.WCSupplierAddressForCommunication = txtSupplierToAddress.Text;
            objWarrantyClaim.WCAttentionTo = txtAttentionTo.Text;
            objWarrantyClaim.WCSupplierCompanyName = txtSupplierCompanyName.Text;

            MessageBox.Show(this, objWarrantyClaim.WarrantyClaim_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
            tblWarrantyDetails.Visible = false;
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvWarrantyClaim.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion

    #region Warranty Claim Update
    private void WarrantyClaimUpdate()
    {
        try
        {

            Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();

            objWarrantyClaim.WCId = gvWarrantyClaim.SelectedRow.Cells[0].Text;

            objWarrantyClaim.WCNo = txtWCNo.Text;
            objWarrantyClaim.WCDate = Yantra.Classes.General.toMMDDYYYY(txtWCDate.Text);
            objWarrantyClaim.WCInvoiceNo = txtSupInvoiceNo.Text;
            objWarrantyClaim.WCInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtSupplierDate.Text);
            objWarrantyClaim.WCInstDate = Yantra.Classes.General.toMMDDYYYY(txtInstallationDate.Text);
            objWarrantyClaim.CustId = ddlCustomerName.SelectedItem.Value;
            objWarrantyClaim.CustUnitId = ddlUnitName.SelectedItem.Value;
            objWarrantyClaim.CustDetId = ddlContactPerson.SelectedItem.Value;

            objWarrantyClaim.ItemCode = ddlModel.SelectedItem.Value;
            objWarrantyClaim.WCItemsSlNo = txtSLNo.Text;
            objWarrantyClaim.WCQty = txtQuantity.Text;
            objWarrantyClaim.WCWarrantyExpOn = Yantra.Classes.General.toMMDDYYYY(txtExpires.Text);
            objWarrantyClaim.WCWarrantyClaimsOn = Yantra.Classes.General.toMMDDYYYY(txtClaimed.Text);

            objWarrantyClaim.WCProbNature = txtProblemNature.Text;
            objWarrantyClaim.WCSerEngRemarks = txtServiceRemarks.Text;
            objWarrantyClaim.WCSMPLRemarks = txtSmplRemarks.Text;
            objWarrantyClaim.WCRemarksNeeds = txtRemarksNeeds.Text;
            objWarrantyClaim.WCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWarrantyClaim.WCApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objWarrantyClaim.WCSupplierName = txtSupplierName.Text;
            objWarrantyClaim.WCItemDesc = txtDescription.Text;
            objWarrantyClaim.WCSupplierAddressForCommunication = txtSupplierToAddress.Text;
            objWarrantyClaim.WCAttentionTo = txtAttentionTo.Text;
            objWarrantyClaim.WCSupplierCompanyName = txtSupplierCompanyName.Text;

            MessageBox.Show(this, objWarrantyClaim.WarrantyClaim_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
            tblWarrantyDetails.Visible = false;
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvWarrantyClaim.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
        }
    }
    #endregion



    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvWarrantyClaim.SelectedIndex > -1)
        {
            Response.Redirect("WarrantyClaimNew.aspx?wcNo=" + gvWarrantyClaim.SelectedRow.Cells[0].Text + "&preparedBy=" + gvWarrantyClaim.SelectedRow.Cells[6].Text);
            //try
            //{
            //    Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();

            //    if (objWarrantyClaim.WarrantyClaim_Select(gvWarrantyClaim.SelectedRow.Cells[0].Text) > 0)
            //    {
            //        tblWarrantyDetails.Visible = true;
            //        btnRefresh.Visible = true;
            //        btnSave.Visible = true;
            //        btnClose.Visible = true;
            //        btnSave.Text = "Update";
            //        btnSave.Enabled = true;



            //        txtWCNo.Text = objWarrantyClaim.WCNo;
            //        txtWCDate.Text = objWarrantyClaim.WCDate;
            //        txtSupInvoiceNo.Text = objWarrantyClaim.WCInvoiceNo;
            //        txtSupplierDate.Text = objWarrantyClaim.WCInvoiceDate;
            //        txtInstallationDate.Text = objWarrantyClaim.WCInstDate;
            //        ddlCustomerName.SelectedValue = objWarrantyClaim.CustId;
            //        ddlCustomerName_SelectedIndexChanged(sender, e);
            //        ddlUnitName.SelectedValue = objWarrantyClaim.CustUnitId;
            //        ddlUnitName_SelectedIndexChanged(sender, e);
            //        ddlContactPerson.SelectedValue = objWarrantyClaim.CustDetId;
            //        ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objWarrantyClaim.ItemCode);
            //        ddlItemType_SelectedIndexChanged(sender, e);
            //        ddlModel.SelectedValue = objWarrantyClaim.ItemCode;
            //        txtSLNo.Text = objWarrantyClaim.WCItemsSlNo;
            //        txtQuantity.Text = objWarrantyClaim.WCQty;
            //        txtExpires.Text = objWarrantyClaim.WCWarrantyExpOn;
            //        txtClaimed.Text = objWarrantyClaim.WCWarrantyClaimsOn;
            //        txtProblemNature.Text = objWarrantyClaim.WCProbNature;
            //        txtServiceRemarks.Text = objWarrantyClaim.WCSerEngRemarks;
            //        txtSmplRemarks.Text = objWarrantyClaim.WCSMPLRemarks;
            //        txtRemarksNeeds.Text = objWarrantyClaim.WCRemarksNeeds;
            //        ddlPreparedBy.SelectedValue = objWarrantyClaim.WCPreparedBy;
            //        // ddlApprovedBy.SelectedItem.Value = objWarrantyClaim.WCApprovedBy;
            //        txtSupplierName.Text = objWarrantyClaim.WCSupplierName;
            //        txtDescription.Text = objWarrantyClaim.WCItemDesc;

            //        txtSupplierToAddress.Text = objWarrantyClaim.WCSupplierAddressForCommunication;
            //        txtAttentionTo.Text = objWarrantyClaim.WCAttentionTo;
            //        txtSupplierCompanyName.Text = objWarrantyClaim.WCSupplierCompanyName;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message);
            //}
            //finally
            //{
            //    Services.Dispose();
            //}
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
        if (gvWarrantyClaim.SelectedIndex > -1)
        {
            try
            {
                Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();
                objWarrantyClaim.WCId = gvWarrantyClaim.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objWarrantyClaim.WarrantyClaim_Delete());
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
                tblWarrantyDetails.Visible = false;
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvWarrantyClaim.DataBind();
                Services.ClearControls(this);
                Services.Dispose();
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
        ItemName_Fill();
    }
    #endregion


    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
        btnNew_Click(sender, e);
    }
    #endregion


    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblWarrantyDetails.Visible = false;

    }
    #endregion


    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "WF DATE" || ddlSearchBy.SelectedItem.Text == "Supplier Invoice Date" || ddlSearchBy.SelectedItem.Text == "Install Date")
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
    protected void ddlSymbols_SelectedIndexChanged1(object sender, EventArgs e)
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
    protected void btnSearchGo_Click1(object sender, EventArgs e)
    {
        gvWarrantyClaim.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvWarrantyClaim.DataBind();
    }
    #endregion


    #region Link Button lbtnWCNo_Click
    protected void lbtnWCNo_Click(object sender, EventArgs e)
    {
        tblWarrantyDetails.Visible = false;
        LinkButton lbtnWCNo;
        lbtnWCNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnWCNo.Parent.Parent;
        gvWarrantyClaim.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();

        if (objWarrantyClaim.WarrantyClaim_Select(gvWarrantyClaim.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;
            btnSave.Text = "Update";

            tblWarrantyDetails.Visible = true;

            txtWCNo.Text = objWarrantyClaim.WCNo;
            txtWCDate.Text = objWarrantyClaim.WCDate;
            txtSupInvoiceNo.Text = objWarrantyClaim.WCInvoiceNo;
            txtSupplierDate.Text = objWarrantyClaim.WCInvoiceDate;
            txtInstallationDate.Text = objWarrantyClaim.WCInstDate;
            ddlCustomerName.SelectedValue = objWarrantyClaim.CustId;
            ddlCustomerName_SelectedIndexChanged(sender, e);
            ddlUnitName.SelectedValue = objWarrantyClaim.CustUnitId;
            ddlUnitName_SelectedIndexChanged(sender, e);
            ddlContactPerson.SelectedValue = objWarrantyClaim.CustDetId;
            //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objWarrantyClaim.ItemCode);
          //  ddlItemType_SelectedIndexChanged(sender, e);
          //  ddlModel.SelectedValue = objWarrantyClaim.ItemCode;
            txtSLNo.Text = objWarrantyClaim.WCItemsSlNo;
            txtQuantity.Text = objWarrantyClaim.WCQty;
            txtExpires.Text = objWarrantyClaim.WCWarrantyExpOn;
            txtClaimed.Text = objWarrantyClaim.WCWarrantyClaimsOn;
            txtProblemNature.Text = objWarrantyClaim.WCProbNature;
            txtServiceRemarks.Text = objWarrantyClaim.WCSerEngRemarks;
            txtSmplRemarks.Text = objWarrantyClaim.WCSMPLRemarks;
            txtRemarksNeeds.Text = objWarrantyClaim.WCRemarksNeeds;
            ddlPreparedBy.SelectedValue = objWarrantyClaim.WCPreparedBy;
            // ddlApprovedBy.SelectedItem.Value = objWarrantyClaim.WCApprovedBy;
            txtSupplierName.Text = objWarrantyClaim.WCSupplierName;
            txtDescription.Text = objWarrantyClaim.WCItemDesc;
            txtSupplierToAddress.Text = objWarrantyClaim.WCSupplierAddressForCommunication;
            txtAttentionTo.Text = objWarrantyClaim.WCAttentionTo;
            txtSupplierCompanyName.Text = objWarrantyClaim.WCSupplierCompanyName;
        }

    }
    #endregion

    #region ddlModel_SelectedIndexChanged
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModel.SelectedItem.Value) > 0)
            {

                txtDescription.Text = objMaster.ItemSpec;
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

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
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
                //txtContactPerson.Text  = objSMCustomer.ContactPerson;
                txtCustUnitAddress.Text = objSMCustomer.CustUnitAddress;
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





    protected void gvWarrantyClaim_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvWarrantyClaim.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=wc&wcid=" + gvWarrantyClaim.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWarrantyClaim.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvWarrantyClaim.DataBind();
    }
}


 
