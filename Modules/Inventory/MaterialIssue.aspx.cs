
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


public partial class Modules_Inventory_MaterialIssue : System.Web.UI.Page
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WorkOrder_Fill();
            ItemTypes_Fill();
            EmployeeMaster_Fill();
            txtIssueQty.Attributes.Add("onkeyup", "javascript:balqtycalc();");
        }
    }
    #endregion

    #region WO No Fill
    private void WorkOrder_Fill()
    {
        try
        {
            SM.WorkOrder.WorkOrder_Select(ddlWONo);

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

    #region Link Button lbtnMIVNo_Click
    protected void lbtnMIVNo_Click(object sender, EventArgs e)
    {
        tblMaterialIssueDetails.Visible = false;
        LinkButton lbtnMIVNo;
        lbtnMIVNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnMIVNo.Parent.Parent;
        gvMaterialIssueDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            Inventory.MaterialIssue objMaterialIssue = new Inventory.MaterialIssue();

            if (objMaterialIssue.MaterialIssue_Select(gvMaterialIssueDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblMaterialIssueDetails.Visible = true;

                txtMIVNo.Text = objMaterialIssue.MIVNo;
                txtMIVDate.Text = objMaterialIssue.MIVDate;
                ddlWONo.SelectedValue = objMaterialIssue.WOId;
                txtRemarks.Text = objMaterialIssue.MIVRemarks;
                ddlPreparedBy.SelectedValue = objMaterialIssue.MIVPreparedBy;
                ddlApprovedBy.SelectedValue = objMaterialIssue.MIVApprovedBy;

                objMaterialIssue.MaterialIssueDetails_Select(gvMaterialIssueDetails.SelectedRow.Cells[0].Text, gvItemsDetails);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            Inventory.Dispose();
            ddlWONo_SelectedIndexChanged(sender, e);
        }

    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);

        txtMIVNo.Text = Inventory.MaterialIssue.MaterialIssue_AutoGenCode();
        txtMIVDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        tblMaterialIssueDetails.Visible = true;

        gvItemsDetails.DataBind();
        // txtNetAmount.Text = "0";
    }
    #endregion

    #region Button SAVE / UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            MaterialIssueSave();
        }
        else if (btnSave.Text == "Update")
        {
            MaterialIssueUpdate();
        }
    }
    #endregion

    #region MaterialIssueSave
    private void MaterialIssueSave()
    {
        if (gvItemsDetails.Rows.Count > 0)
        {
            try
            {
                Inventory.MaterialIssue objMaterialIssue = new Inventory.MaterialIssue();
                Inventory.BeginTransaction();

                objMaterialIssue.MIVNo = txtMIVNo.Text;
                objMaterialIssue.MIVDate = Yantra.Classes.General.toMMDDYYYY(txtMIVDate.Text);
                objMaterialIssue.WOId = ddlWONo.SelectedItem.Value;
                objMaterialIssue.MIVRemarks = txtRemarks.Text;
                objMaterialIssue.MIVPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objMaterialIssue.MIVApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objMaterialIssue.MaterialIssue_Save() == "Data Saved Successfully")
                {
                    objMaterialIssue.MaterialIssueDetails_Delete(objMaterialIssue.MIVId);
                    foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                    {

                        objMaterialIssue.ItemCode = gvrow.Cells[1].Text;
                        objMaterialIssue.ItemBalanceQuantity = gvrow.Cells[4].Text;
                        objMaterialIssue.MIVDetIssueQty = gvrow.Cells[5].Text;

                        objMaterialIssue.MaterialIssueDetails_Save();
                        objMaterialIssue.MaterialIssueStock_Update();
                    }

                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvMaterialIssueDetails.DataBind();
                gvItemsDetails.DataBind();
                tblMaterialIssueDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for GRN Details ");
        }
    }
    #endregion

    #region MaterialIssueUpdate
    private void MaterialIssueUpdate()
    {
        if (gvItemsDetails.Rows.Count > 0)
        {
            try
            {

                Inventory.MaterialIssue objMaterialIssue = new Inventory.MaterialIssue();
                Inventory.BeginTransaction();

                objMaterialIssue.MIVId = gvMaterialIssueDetails.SelectedRow.Cells[0].Text;
                objMaterialIssue.MIVNo = txtMIVNo.Text;
                objMaterialIssue.MIVDate = Yantra.Classes.General.toMMDDYYYY(txtMIVDate.Text);
                objMaterialIssue.WOId = ddlWONo.SelectedItem.Value;
                objMaterialIssue.MIVRemarks = txtRemarks.Text;
                objMaterialIssue.MIVPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objMaterialIssue.MIVApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objMaterialIssue.MaterialIssue_Update() == "Data Updated Successfully")
                {
                    objMaterialIssue.MaterialIssueDetails_Delete(objMaterialIssue.MIVId);
                    foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                    {

                        objMaterialIssue.ItemCode = gvrow.Cells[1].Text;
                        objMaterialIssue.ItemBalanceQuantity = gvrow.Cells[4].Text;
                        objMaterialIssue.MIVDetIssueQty = gvrow.Cells[5].Text;

                        objMaterialIssue.MaterialIssueDetails_Save();
                        objMaterialIssue.MaterialIssueStock_Update();
                    }
                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                btnDelete.Attributes.Clear();
                gvMaterialIssueDetails.DataBind();

                gvItemsDetails.DataBind();
                tblMaterialIssueDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Material Issue Details ");
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvMaterialIssueDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.MaterialIssue objMaterialIssue = new Inventory.MaterialIssue();

                if (objMaterialIssue.MaterialIssue_Select(gvMaterialIssueDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblMaterialIssueDetails.Visible = true;

                    txtMIVNo.Text = objMaterialIssue.MIVNo;
                    txtMIVDate.Text = objMaterialIssue.MIVDate;
                    ddlWONo.SelectedValue = objMaterialIssue.WOId;
                    txtRemarks.Text = objMaterialIssue.MIVRemarks;
                    ddlPreparedBy.SelectedValue = objMaterialIssue.MIVPreparedBy;
                    ddlApprovedBy.SelectedValue = objMaterialIssue.MIVApprovedBy;

                    objMaterialIssue.MaterialIssueDetails_Select(gvMaterialIssueDetails.SelectedRow.Cells[0].Text, gvItemsDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                Inventory.Dispose();
                ddlWONo_SelectedIndexChanged(sender, e);
            }
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
        if (gvMaterialIssueDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.MaterialIssue objMaterialIssue = new Inventory.MaterialIssue();
                MessageBox.Show(this, objMaterialIssue.MaterialIssue_Delete(gvMaterialIssueDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvMaterialIssueDetails.DataBind();
                Inventory.ClearControls(this);
                Inventory.Dispose();
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
        Inventory.ClearControls(this);

        gvItemsDetails.DataBind();
    }
    #endregion

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtQtyInHand.Text == "")
        {
            MessageBox.Show(this, "Please Select Item Name");
            return;
        }

        if (txtQtyInHand.Text == "0")
        {
            MessageBox.Show(this, "No Stock Found for the Selected Item");
            return;
        }
        else
        {
            if ((Convert.ToInt32(txtQtyInHand.Text) - Convert.ToInt32(txtIssueQty.Text)) < 0)
            {
                MessageBox.Show(this, "Issue Qty Should be less or equal to the Quantity In Hand");
                return;
            }
        }

        DataTable MIVItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        MIVItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        MIVItems.Columns.Add(col);
        col = new DataColumn("UOM");
        MIVItems.Columns.Add(col);
        col = new DataColumn("BalanceQty");
        MIVItems.Columns.Add(col);
        col = new DataColumn("IssueQty");
        MIVItems.Columns.Add(col);


        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                DataRow dr = MIVItems.NewRow();

                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemName"] = gvrow.Cells[2].Text;
                dr["UOM"] = gvrow.Cells[3].Text;
                dr["BalanceQty"] = gvrow.Cells[4].Text;
                dr["IssueQty"] = gvrow.Cells[5].Text;

                MIVItems.Rows.Add(dr);
            }
        }

        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Value)
                {
                    gvItemsDetails.DataSource = MIVItems;
                    gvItemsDetails.DataBind();
                    MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = MIVItems.NewRow();

        drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
        drnew["ItemName"] = ddlItemName.SelectedItem.Text;
        drnew["UOM"] = txtItemUOM.Text;
        drnew["BalanceQty"] = txtBalanceQty.Text;
        drnew["IssueQty"] = txtIssueQty.Text;

        MIVItems.Rows.Add(drnew);

        gvItemsDetails.DataSource = MIVItems;
        gvItemsDetails.DataBind();
        btnItemsRefresh_Click(sender, e);
    }
    #endregion

    #region Button ITEMS RERFRESH Click
    protected void btnItemsRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        ddlItemName.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtBalanceQty.Text = string.Empty;
        txtIssueQty.Text = string.Empty;
        txtQtyInHand.Text = string.Empty;
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblMaterialIssueDetails.Visible = false;

    }
    #endregion

    #region ddlWONo_SelectedIndexChanged
    protected void ddlWONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.WorkOrder objWorkOrder = new SM.WorkOrder();

            if (objWorkOrder.WorkOrder_Select(ddlWONo.SelectedItem.Value) > 0)
            {
                txtWODate.Text = objWorkOrder.WODate;

                objWorkOrder.WorkOrderDetails_Select(ddlWONo.SelectedItem.Value, gvWorkOrderDetails);

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objWorkOrder.CustId) > 0)
                {
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
            }
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

    #region ItemName Select Index Changed
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtBalanceQty.Text = objMaster.ItemQtyInHand;
                txtQtyInHand.Text = objMaster.ItemQtyInHand;
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

    #region gvMaterialIssueDetails_RowDataBound
    protected void gvMaterialIssueDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region gvItemsDetails_RowDataBound
    protected void gvItemsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Item Details list?');");

        }
    }
    #endregion

    #region gvItemsDetails_RowDeleting
    protected void gvItemsDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemsDetails.Rows[e.RowIndex].Cells[1].Text;

        DataTable MIVItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        MIVItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        MIVItems.Columns.Add(col);
        col = new DataColumn("UOM");
        MIVItems.Columns.Add(col);
        col = new DataColumn("BalanceQty");
        MIVItems.Columns.Add(col);
        col = new DataColumn("IssueQty");
        MIVItems.Columns.Add(col);


        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = MIVItems.NewRow();

                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["UOM"] = gvrow.Cells[3].Text;
                    dr["BalanceQty"] = gvrow.Cells[4].Text;
                    dr["IssueQty"] = gvrow.Cells[5].Text;


                    MIVItems.Rows.Add(dr);
                }
            }
        }
        gvItemsDetails.DataSource = MIVItems;
        gvItemsDetails.DataBind();
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "WO Date" || ddlSearchBy.SelectedItem.Text == "MIV Date")
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
        gvMaterialIssueDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvMaterialIssueDetails.DataBind();
    }
    #endregion

    #region ddlItemType_SelectedIndexChanged
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
       ItemName_Fill();
    }
    #endregion

}

 
