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
public partial class Modules_Services_WarrantyClaimNew : basePage
{
    string wcNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        wcNo = Request.QueryString["wcNo"];
        if(!IsPostBack)
        {
            if (wcNo == null)
            {
                // gvWarrantyClaim.SelectedIndex = -1;
                Services.ClearControls(this);
                txtSupplierName.Text = "Value Line Trade Pvt Ltd.";
                txtWCNo.Text = Services.WarrantyClaim.WarrantyClaim_AutoGenCode();
                txtWCDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //btnSave.Text = "Save";
                tblWarrantyDetails.Visible = true;
            }
            else
            {
                try
                {
                    Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();

                    if (objWarrantyClaim.WarrantyClaim_Select(wcNo) > 0)
                    {
                        tblWarrantyDetails.Visible = true;
                        btnRefresh.Visible = true;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;



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
                        ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objWarrantyClaim.ItemCode);
                        ddlItemType_SelectedIndexChanged(sender, e);
                        ddlModel.SelectedValue = objWarrantyClaim.ItemCode;
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
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
                finally
                {
                    Services.Dispose();
                }
            }



            CustomerMaster_Fill();
            ItemTypes_Fill();
            EmployeeMaster_Fill();
           // tblWarrantyDetails.Visible = false;
        }
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (wcNo!=null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["preparedBy"]) && Request.QueryString["preparedBy"] != "&nbsp;")
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
            //btnDelete.Attributes.Clear();
            //gvWarrantyClaim.DataBind();
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

            objWarrantyClaim.WCId = wcNo;

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
            //btnDelete.Attributes.Clear();
            //gvWarrantyClaim.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
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
       // btnNew_Click(sender, e);
    }
    #endregion


    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarrantyClaim.aspx");

    }
    #endregion

    #region Link Button lbtnWCNo_Click
    protected void lbtnWCNo_Click(object sender, EventArgs e)
    {
        tblWarrantyDetails.Visible = false;
        LinkButton lbtnWCNo;
        lbtnWCNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnWCNo.Parent.Parent;
        //gvWarrantyClaim.SelectedIndex = gvRow.RowIndex;
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        Services.WarrantyClaim objWarrantyClaim = new Services.WarrantyClaim();

        if (objWarrantyClaim.WarrantyClaim_Select(wcNo) > 0)
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
        if (wcNo!=null)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=wc&wcid=" +wcNo+ "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

}
 
