//Date Written: 08/Jan/2009      Written By: L.Hima Kishore



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

public partial class Modules_Services_AMCEnquiry : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EnquiryMode_Fill();
            DeliveryType_Fill();
            ItemTypes_Fill();
            CustomerMaster_Fill();
            rbEmployee.Checked = true;
            rbEmployeeAgent_CheckedChanged(sender, e);
        }
    }
    #endregion

    #region EnqiryMode Fill
    private void EnquiryMode_Fill()
    {
        try
        {
            Masters.EnquiryMode.EnquiryMode_Select(ddlEnquirySource);
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

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDeliveryType);
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

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            Services.CustomerMaster.CustomerMaster_Select(ddlCustomer);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlOriginatedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpNameForAssign);
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
        gvSalesEnquiry.SelectedIndex = -1;
        btnDelete.Attributes.Clear();
        Services.ClearControls(this);
        txtEnquiryNo.Text = Services.AMCEnquiry.AMCEnquiry_AutoGenCode();
        txtEnquiryDate.Text = DateTime.Now.ToShortDateString();
        tblSalesEnquiry.Visible = true;
        tblAssignTasks.Visible = false;
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblSalesEnquiry.Visible = false;
    }
    #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

    #region ddlItemName_SelectedIndexChanged
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
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

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
            if ((objServicesCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value)) > 0)
            {
                txtContactPerson.Text = objServicesCustomer.ContactPerson;
                txtRegion.Text = objServicesCustomer.RegName;
                txtIndustryType.Text = objServicesCustomer.IndType;
                txtAddress.Text = objServicesCustomer.Address;
                txtEmail.Text = objServicesCustomer.Email;
                txtPhoneNo.Text = objServicesCustomer.Phone;
                txtMobile.Text = objServicesCustomer.Mobile;
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
    #endregion

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                DataRow dr = InterestedProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemName"] = gvrow.Cells[2].Text;
                dr["UOM"] = gvrow.Cells[3].Text;
                dr["Quantity"] = gvrow.Cells[4].Text;
                dr["Specifications"] = gvrow.Cells[5].Text;
                dr["Remarks"] = gvrow.Cells[6].Text;
                dr["Priority"] = gvrow.Cells[7].Text;
                InterestedProducts.Rows.Add(dr);
            }
        }

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Value)
                {
                    gvInterestedProducts.DataSource = InterestedProducts;
                    gvInterestedProducts.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }
            }
        }

        DataRow drnew = InterestedProducts.NewRow();
        drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
        drnew["ItemName"] = ddlItemName.SelectedItem.Text;
        drnew["UOM"] = txtItemUOM.Text;
        drnew["Quantity"] = txtItemQuantity.Text;
        drnew["Specifications"] = txtItemSpecifications.Text;
        drnew["Remarks"] = txtRemarks.Text; ;
        drnew["Priority"] = ddlPriority.SelectedItem.Text;
        InterestedProducts.Rows.Add(drnew);

        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
        btnRefreshItems_Click(sender, e);
    }
    #endregion

    #region gvInterestedProducts_RowDeleting
    protected void gvInterestedProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvInterestedProducts.Rows[e.RowIndex].Cells[1].Text;
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["UOM"] = gvrow.Cells[3].Text;
                    dr["Quantity"] = gvrow.Cells[4].Text;
                    dr["Specifications"] = gvrow.Cells[5].Text;
                    dr["Remarks"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();

    }
    #endregion

    #region gvInterestedProducts_RowDataBound
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
    }
    #endregion

    #region Button Rfresh Items Click
    protected void btnRefreshItems_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        ddlItemName.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtItemSpecifications.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlPriority.SelectedValue = "0";
    }
    #endregion

    #region Button SAVE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SalesEnquirySave();
        }
        else if (btnSave.Text == "Update")
        {
            SalesEnquiryUpdate();
        }
    }
    #endregion

    #region SalesEnquirySave
    private void SalesEnquirySave()
    {
        if (gvInterestedProducts.Rows.Count > 0)
        {
            try
            {
                Services.AMCEnquiry objServices = new Services.AMCEnquiry();
                Services.BeginTransaction();
                objServices.EnqNo = txtEnquiryNo.Text;
                objServices.EnqDate = txtEnquiryDate.Text;
                objServices.CustId = ddlCustomer.SelectedItem.Value;
                objServices.EnqModeId = ddlEnquirySource.SelectedItem.Value;
                if (rbEmployee.Checked == true)
                { objServices.EnqOrigBy = rbEmployee.Text; }
                else if (rbAgent.Checked == true)
                { objServices.EnqOrigBy = rbAgent.Text; }
                objServices.EnqOrigName = ddlOriginatedBy.SelectedItem.Text;
                objServices.EnqRef = txtReferenceCode.Text;
                objServices.EnqFollowUp = txtFollowUpCriteria.Text;
                objServices.EnqDeliveryDate = txtDeliveryDate.Text;
                objServices.DespModeId = ddlDeliveryType.SelectedItem.Value;
                objServices.PromotionType = txtPromotionType.Text;
                objServices.PromotionActivity = txtPromotionActivity.Text;
                objServices.EnqStatus = "New";
                objServices.EnqDueDate = txtEnquiryDueDate.Text;
                objServices.EnqDesc = txtDescription.Text;
                if (objServices.AMCEnquiry_Save() == "Data Saved Successfully")
                {
                    objServices.AMCEnquiryDetails_Delete(objServices.EnqId);
                    foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                    {
                        objServices.EnqDetItemCode = gvrow.Cells[1].Text;
                        objServices.EnqDetQty = gvrow.Cells[4].Text;
                        objServices.EnqDetSpec = gvrow.Cells[5].Text;
                        objServices.EnqDetRemarks = gvrow.Cells[6].Text;
                        objServices.EnqDetPriority = gvrow.Cells[7].Text;
                        objServices.AMCEnquiryDetails_Save();
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
                tblSalesEnquiry.Visible = false;
                gvInterestedProducts.DataBind();
                btnDelete.Attributes.Clear();
                gvSalesEnquiry.DataBind();
                 Services.ClearControls(this);
                 Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    #endregion

    #region SalesEnquiryUpdate
    private void SalesEnquiryUpdate()
    {
        if (gvInterestedProducts.Rows.Count > 0)
        {
            try
            {
                Services.AMCEnquiry objServices = new Services.AMCEnquiry();
                  Services.BeginTransaction();
                objServices.EnqId = gvSalesEnquiry.SelectedRow.Cells[0].Text;
                objServices.EnqDate = txtEnquiryDate.Text;
                objServices.CustId = ddlCustomer.SelectedItem.Value;
                objServices.EnqModeId = ddlEnquirySource.SelectedItem.Value;
                if (rbEmployee.Checked == true)
                { objServices.EnqOrigBy = rbEmployee.Text; }
                else if (rbAgent.Checked == true)
                { objServices.EnqOrigBy = rbAgent.Text; }
                objServices.EnqOrigName = ddlOriginatedBy.SelectedItem.Text;
                objServices.EnqRef = txtReferenceCode.Text;
                objServices.EnqFollowUp = txtFollowUpCriteria.Text;
                objServices.EnqDeliveryDate = txtDeliveryDate.Text;
                objServices.DespModeId = ddlDeliveryType.SelectedItem.Value;
                objServices.PromotionType = txtPromotionType.Text;
                objServices.PromotionActivity = txtPromotionActivity.Text;
                objServices.EnqStatus = "New";
                objServices.EnqDueDate = txtEnquiryDueDate.Text;
                objServices.EnqDesc = txtDescription.Text;
                if (objServices.AMCEnquiry_Update() == "Data Updated Successfully")
                {
                    objServices.AMCEnquiryDetails_Delete(objServices.EnqId);
                    foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                    {
                        objServices.EnqDetItemCode = gvrow.Cells[1].Text;
                        objServices.EnqDetQty = gvrow.Cells[4].Text;
                        objServices.EnqDetSpec = gvrow.Cells[5].Text;
                        objServices.EnqDetRemarks = gvrow.Cells[6].Text;
                        objServices.EnqDetPriority = gvrow.Cells[7].Text;
                        objServices.AMCEnquiryDetails_Save();
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
                tblSalesEnquiry.Visible = false;
                gvInterestedProducts.DataBind();
                btnDelete.Attributes.Clear();
                gvSalesEnquiry.DataBind();
                 Services.ClearControls(this);
               Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    #endregion

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Enquiry Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date")
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

    #region ddlSymbols_SelectedIndexChanged
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

    #region gvSalesEnquiry_RowDataBound
    protected void gvSalesEnquiry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region lbtnEnqNo_Click
    protected void lbtnEnqNo_Click(object sender, EventArgs e)
    {
        tblSalesEnquiry.Visible = false;
        tblAssignTasks.Visible = false;
        LinkButton lbtnEnqNo;
        lbtnEnqNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnqNo.Parent.Parent;
        gvSalesEnquiry.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        try
        {
            Services.AMCEnquiry objServices = new Services.AMCEnquiry();
            if (objServices.AMCEnquiry_Select(gvSalesEnquiry.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblSalesEnquiry.Visible = true;
                txtEnquiryNo.Text = objServices.EnqNo;
                txtEnquiryDate.Text = objServices.EnqDate;
                ddlCustomer.SelectedValue = objServices.CustId;
                ddlEnquirySource.SelectedValue = objServices.EnqModeId;
                if (objServices.EnqOrigBy == rbEmployee.Text)
                {
                    rbEmployee.Checked = true;
                    rbAgent.Checked = false;
                    rbEmployeeAgent_CheckedChanged(sender, e);
                }
                else if (objServices.EnqOrigBy == rbAgent.Text)
                {
                    rbAgent.Checked = true;
                    rbEmployee.Checked = false;
                    rbEmployeeAgent_CheckedChanged(sender, e);
                }
                ddlOriginatedBy.SelectedIndex = ddlOriginatedBy.Items.IndexOf(ddlOriginatedBy.Items.FindByText(objServices.EnqOrigName));
                txtReferenceCode.Text = objServices.EnqRef;
                txtFollowUpCriteria.Text = objServices.EnqFollowUp;
                txtDeliveryDate.Text = objServices.EnqDeliveryDate;
                ddlDeliveryType.SelectedValue = objServices.DespModeId;
                txtPromotionType.Text = objServices.PromotionType;
                txtPromotionActivity.Text = objServices.PromotionActivity;
                txtEnquiryDueDate.Text = objServices.EnqDueDate;
                txtDescription.Text = objServices.EnqDesc;
                objServices.AMCEnquiryDetails_Select(gvSalesEnquiry.SelectedRow.Cells[0].Text, gvInterestedProducts);

                Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                if ((objServicesCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value)) > 0)
                {
                    txtContactPerson.Text = objServicesCustomer.ContactPerson;
                    txtRegion.Text = objServicesCustomer.RegName;
                    txtIndustryType.Text = objServicesCustomer.IndType;
                    txtAddress.Text = objServicesCustomer.Address;
                    txtEmail.Text = objServicesCustomer.Email;
                    txtPhoneNo.Text = objServicesCustomer.Phone;
                    txtMobile.Text = objServicesCustomer.Mobile;
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
            Services.Dispose();
        }


    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSalesEnquiry.SelectedIndex > -1)
        {
            try
            {
                Services.AMCEnquiry objServices = new Services.AMCEnquiry();
                  Services.BeginTransaction();
                MessageBox.Show(this, objServices.AMCEnquiry_Delete(gvSalesEnquiry.SelectedRow.Cells[0].Text));
               Services.CommitTransaction();
            }
            catch (Exception ex)
            {
               Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvSalesEnquiry.DataBind();
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

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvSalesEnquiry.SelectedIndex > -1)
        {
            try
            {
                Services.AMCEnquiry objServices = new Services.AMCEnquiry();
                if (objServices.AMCEnquiry_Select(gvSalesEnquiry.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblSalesEnquiry.Visible = true;
                    txtEnquiryNo.Text = objServices.EnqNo;
                    txtEnquiryDate.Text = objServices.EnqDate;
                    ddlCustomer.SelectedValue = objServices.CustId;
                    ddlEnquirySource.SelectedValue = objServices.EnqModeId;
                    if (objServices.EnqOrigBy == rbEmployee.Text)
                    {
                        rbEmployee.Checked = true;
                        rbAgent.Checked = false;
                        rbEmployeeAgent_CheckedChanged(sender, e);
                    }
                    else if (objServices.EnqOrigBy == rbAgent.Text)
                    {
                        rbAgent.Checked = true;
                        rbEmployee.Checked = false;
                        rbEmployeeAgent_CheckedChanged(sender, e);
                    }
                    ddlOriginatedBy.SelectedIndex = ddlOriginatedBy.Items.IndexOf(ddlOriginatedBy.Items.FindByText(objServices.EnqOrigName));
                    txtReferenceCode.Text = objServices.EnqRef;
                    txtFollowUpCriteria.Text = objServices.EnqFollowUp;
                    txtDeliveryDate.Text = objServices.EnqDeliveryDate;
                    ddlDeliveryType.SelectedValue = objServices.DespModeId;
                    txtPromotionType.Text = objServices.PromotionType;
                    txtPromotionActivity.Text = objServices.PromotionActivity;
                    txtEnquiryDueDate.Text = objServices.EnqDueDate;
                    txtDescription.Text = objServices.EnqDesc;
                    objServices.AMCEnquiryDetails_Select(gvSalesEnquiry.SelectedRow.Cells[0].Text, gvInterestedProducts);

                    Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                    if ((objServicesCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value)) > 0)
                    {
                        txtContactPerson.Text = objServicesCustomer.ContactPerson;
                        txtRegion.Text = objServicesCustomer.RegName;
                        txtIndustryType.Text = objServicesCustomer.IndType;
                        txtAddress.Text = objServicesCustomer.Address;
                        txtEmail.Text = objServicesCustomer.Email;
                        txtPhoneNo.Text = objServicesCustomer.Phone;
                        txtMobile.Text = objServicesCustomer.Mobile;
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
               Services.Dispose();
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
         Services.ClearControls(this);
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvSalesEnquiry.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvSalesEnquiry.DataBind();
    }
    #endregion

    #region Button PRINT Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvSalesEnquiry.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/ServicesReportViewer.aspx?type=salesenq&enqno=" + gvSalesEnquiry.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Employee/Agent Radio Button Change
    protected void rbEmployeeAgent_CheckedChanged(object sender, EventArgs e)
    {
        if (rbEmployee.Checked == true && rbAgent.Checked == false)
        {
            lblOrginatedList.Text = "Employee Name";
            EmployeeMaster_Fill();
        }
        else if (rbAgent.Checked == true && rbEmployee.Checked == false)
        {
            lblOrginatedList.Text = "Agent Name";
            ddlOriginatedBy.Items.Clear();
            ddlOriginatedBy.Items.Add(new ListItem("--", "0"));
        }
    }
    #endregion

    #region Assign Button Click
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        tblSalesEnquiry.Visible = false;
        if (gvSalesEnquiry.SelectedIndex > -1)
        {
            try
            {
                txtAssignTaskNo.Text = Services.AMCAssignments.AMCAssignments_AutoGenCode();
                Services.AMCAssignments objServicesAssign = new Services.AMCAssignments();
                if (objServicesAssign.AMCAssignments_Select(gvSalesEnquiry.SelectedRow.Cells[0].Text) > 0)
                {
                    tblAssignTasks.Visible = true;
                    btnAssignTask.Text = "Re-Assign";

                    txtEnquiryNoForAssign.Text = string.Empty;
                    txtEnquiryDateForAssign.Text = string.Empty;
                    txtCustomerNameForAssingn.Text = string.Empty;
                    txtCustomerEmailForAssingn.Text = string.Empty;
                    ddlEmpNameForAssign.SelectedValue = "0";
                    txtEmpEmailId.Text = string.Empty;
                    txtRemarksForAssingn.Text = string.Empty;
                    txtAssignDate.Text = DateTime.Now.ToString();
                    txtDueDate.Text = DateTime.Now.ToString();

                    txtEnquiryNoForAssign.Text = objServicesAssign.EnqNo;
                    txtEnquiryDateForAssign.Text = objServicesAssign.EnqDate;
                    ddlEmpNameForAssign.SelectedValue = objServicesAssign.EmpId;
                    txtAssignDate.Text = objServicesAssign.AssingDate;
                    txtDueDate.Text = objServicesAssign.DueDate;
                    txtRemarksForAssingn.Text = objServicesAssign.AssignRemarks;
                    ddlEmpNameForAssign_SelectedIndexChanged(sender, e);
                    Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                    if ((objServicesCustomer.CustomerMaster_Select(objServicesAssign.CustId)) > 0)
                    {
                        txtCustomerNameForAssingn.Text = objServicesCustomer.CompName;
                        txtCustomerEmailForAssingn.Text = objServicesCustomer.Email;
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Re-Assign this Enquiry?');");
                }
                else
                {
                    Services.AMCEnquiry objServicesEnq = new Services.AMCEnquiry();
                    if (objServicesEnq.AMCEnquiry_Select(gvSalesEnquiry.SelectedRow.Cells[0].Text) > 0)
                    {
                        tblAssignTasks.Visible = true;
                        btnAssignTask.Text = "Assign";

                        txtEnquiryNoForAssign.Text = string.Empty;
                        txtEnquiryDateForAssign.Text = string.Empty;
                        txtCustomerNameForAssingn.Text = string.Empty;
                        txtCustomerEmailForAssingn.Text = string.Empty;
                        ddlEmpNameForAssign.SelectedValue = "0";
                        txtEmpEmailId.Text = string.Empty;
                        txtRemarksForAssingn.Text = string.Empty;
                        txtAssignDate.Text = DateTime.Now.ToString();
                        txtDueDate.Text = DateTime.Now.ToString();

                        txtEnquiryNoForAssign.Text = objServicesEnq.EnqNo;
                        txtEnquiryDateForAssign.Text = objServicesEnq.EnqDate;

                        Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                        if ((objServicesCustomer.CustomerMaster_Select(objServicesEnq.CustId)) > 0)
                        {
                            txtCustomerNameForAssingn.Text = objServicesCustomer.CompName;
                            txtCustomerEmailForAssingn.Text = objServicesCustomer.Email;
                        }
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Assign this Enquiry?');");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
               Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion


    #region ddlEmpNameForAssign_SelectedIndexChanged
    protected void ddlEmpNameForAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlEmpNameForAssign.SelectedItem.Value) > 0)
            {
                txtEmpEmailId.Text = objHR.EmpEMail;
            }
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

    #region Button Assign Task 
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        try
        {
            
            Services.AMCAssignments objServicesAssign = new Services.AMCAssignments();
            Services.BeginTransaction();
            objServicesAssign.AssignTaskNo = txtAssignTaskNo.Text;
            objServicesAssign.EnqId = gvSalesEnquiry.SelectedRow.Cells[0].Text;
            objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
            objServicesAssign.AssingDate = txtAssignDate.Text;
            objServicesAssign.DueDate = txtDueDate.Text;
            objServicesAssign.AssignRemarks = txtRemarksForAssingn.Text;
            objServicesAssign.AssignStatus = "New";
            Services.AMCEnquiry.AMCEnquiryStatus_Update(Services.ServicesStatus.Open, gvSalesEnquiry.SelectedRow.Cells[0].Text);

            if (btnAssignTask.Text == "Assign")
            {
                MessageBox.Show(this, objServicesAssign.AMCAssignments_Save());
                Services.CommitTransaction();
            }
            else if (btnAssignTask.Text == "Re-Assign")
            {
                MessageBox.Show(this, objServicesAssign.AMCAssignments_Update());
                Services.CommitTransaction();
            }
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblSalesEnquiry.Visible = false;
            tblAssignTasks.Visible = false;
            gvInterestedProducts.DataBind();
            btnDelete.Attributes.Clear();
            btnAssignTask.Attributes.Clear();
            gvSalesEnquiry.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
        }
        tblAssignTasks.Visible = false;
    }
    #endregion

    #region Button Cancel Task
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {

        tblAssignTasks.Visible = false;
    }
    #endregion



}


 
