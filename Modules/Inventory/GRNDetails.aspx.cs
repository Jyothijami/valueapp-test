
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


public partial class Modules_Inventory_GRNDetails : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MRIRNo_Fill();
            ItemNames_Fill();
            EmployeeMaster_Fill();

        }
    }
    #endregion

    #region MRIR No Fill
    private void MRIRNo_Fill()
    {
        try
        {
            Inventory.MRIR.MRIR_Select(ddlMRIRNo);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Inventory.Dispose();
        }
    }
    #endregion

    #region Item Names Fill
    private void ItemNames_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlItemName);
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

    #region Link Button lbtnGRNNo_Click
    protected void lbtnGRNNo_Click(object sender, EventArgs e)
    {
        tblGRNDetails.Visible = false;
        LinkButton lbtnGRNNo;
        lbtnGRNNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnGRNNo.Parent.Parent;
        gvGRNDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            Inventory.GRNDetails objGRN = new Inventory.GRNDetails();

            if (objGRN.GRN_Select(gvGRNDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblGRNDetails.Visible = true;


                txtGRNNo.Text = objGRN.GRNNo;
                txtGRNDate.Text = objGRN.GRNDate;
                ddlMRIRNo.SelectedValue = objGRN.MRIRId;


                if (objGRN.GRNType == "PurchaseOrder")
                {
                    rbPurchaseOrder.Checked = true;
                    rbMRIR.Checked = false;
                }
                else
                {
                    rbMRIR.Checked = true;
                    rbPurchaseOrder.Checked = false;
                }

                txtRemarks.Text = objGRN.GRNRemarks;
                ddlPreparedBy.SelectedValue = objGRN.GRNPreparedBy;
                ddlApprovedBy.SelectedValue = objGRN.GRNApprovedBy;


                objGRN.GRNDetails_Select(gvGRNDetails.SelectedRow.Cells[0].Text, gvItemsDetails);
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
            ddlMRIRNo_SelectedIndexChanged(sender, e);

        }

    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);

        txtGRNNo.Text = Inventory.GRNDetails.GRN_AutoGenCode();
        txtGRNDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        tblGRNDetails.Visible = true;

        gvItemsDetails.DataBind();
        // txtNetAmount.Text = "0";

    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            GRNSave();
        }
        else if (btnSave.Text == "Update")
        {
            GRNUpdate();
        }
    }
    #endregion

    #region GRNSave
    private void GRNSave()
    {
        if (gvItemsDetails.Rows.Count > 0)
        {
            try
            {
                Inventory.GRNDetails objGRN = new Inventory.GRNDetails();
                Inventory.BeginTransaction();

                objGRN.GRNNo = txtGRNNo.Text;
                objGRN.GRNDate = Yantra.Classes.General.toMMDDYYYY(txtGRNDate.Text);
                objGRN.MRIRId = ddlMRIRNo.SelectedItem.Value;

                if (rbPurchaseOrder.Checked == true)
                {
                    objGRN.GRNType = rbPurchaseOrder.Text;
                }
                else if (rbMRIR.Checked == true)
                {
                    objGRN.GRNType = rbMRIR.Text;
                }

                objGRN.GRNRemarks = txtRemarks.Text;
                objGRN.GRNPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objGRN.GRNApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objGRN.GRN_Save() == "Data Saved Successfully")
                {
                    objGRN.GRNDetails_Delete(objGRN.GRNId);
                    foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                    {


                        objGRN.ItemName = gvrow.Cells[1].Text;
                        objGRN.UOM = gvrow.Cells[2].Text;
                        objGRN.MRIRDetOrderedQty = gvrow.Cells[3].Text;
                        objGRN.GRNDetReceivedQty = gvrow.Cells[4].Text;


                        objGRN.GRNDetails_Save();
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
                gvGRNDetails.DataBind();
                gvItemsDetails.DataBind();
                tblGRNDetails.Visible = false;
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

    #region GRNUpdate
    private void GRNUpdate()
    {
        if (gvItemsDetails.Rows.Count > 0)
        {
            try
            {
                Inventory.GRNDetails objGRN = new Inventory.GRNDetails();
                Inventory.BeginTransaction();
                objGRN.GRNId = gvGRNDetails.SelectedRow.Cells[0].Text;

                objGRN.GRNNo = txtGRNNo.Text;
                objGRN.GRNDate = Yantra.Classes.General.toMMDDYYYY(txtGRNDate.Text);
                objGRN.MRIRId = ddlMRIRNo.SelectedItem.Value; ;
                /////// // objGRN.GRNType = 

                objGRN.GRNRemarks = txtRemarks.Text;
                objGRN.GRNPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objGRN.GRNApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objGRN.GRN_Update() == "Data Updated Successfully")
                {
                    objGRN.GRNDetails_Delete(objGRN.GRNId);
                    foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                    {

                        objGRN.ItemName = gvrow.Cells[1].Text;
                        objGRN.UOM = gvrow.Cells[2].Text;
                        objGRN.MRIRDetOrderedQty = gvrow.Cells[3].Text;
                        objGRN.GRNDetReceivedQty = gvrow.Cells[4].Text;

                        objGRN.GRNDetails_Save();
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
                gvGRNDetails.DataBind();

                gvItemsDetails.DataBind();
                tblGRNDetails.Visible = false;
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

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvGRNDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.GRNDetails objGRN = new Inventory.GRNDetails();

                if (objGRN.GRN_Select(gvGRNDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblGRNDetails.Visible = true;


                    txtGRNNo.Text = objGRN.GRNNo;
                    txtGRNDate.Text = objGRN.GRNDate;
                    ddlMRIRNo.SelectedValue = objGRN.MRIRId;


                    //if (objGRN.GRNType == rbPurchaseOrder.Text)
                    //{
                    //    rbPurchaseOrder.Checked = true;
                    //}
                    //else if (objGRN.GRNType == rbMRIR.Text)
                    //{
                    //    rbMRIR.Checked = true;
                    //}

                    if (objGRN.GRNType == "PurchaseOrder")
                    {
                        rbPurchaseOrder.Checked = true;
                        rbMRIR.Checked = false;
                    }
                    else
                    {
                        rbMRIR.Checked = true;
                        rbPurchaseOrder.Checked = false;
                    }

                    txtRemarks.Text = objGRN.GRNRemarks;
                    ddlPreparedBy.SelectedValue = objGRN.GRNPreparedBy;
                    ddlApprovedBy.SelectedValue = objGRN.GRNApprovedBy;


                    objGRN.GRNDetails_Select(gvGRNDetails.SelectedRow.Cells[0].Text, gvItemsDetails);
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
                ddlMRIRNo_SelectedIndexChanged(sender, e);

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
        if (gvGRNDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.GRNDetails objGRN = new Inventory.GRNDetails();
                MessageBox.Show(this, objGRN.GRN_Delete(gvGRNDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvGRNDetails.DataBind();
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
        DataTable GRNItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        GRNItems.Columns.Add(col);
        col = new DataColumn("UOM");
        GRNItems.Columns.Add(col);
        col = new DataColumn("OrderedQty");
        GRNItems.Columns.Add(col);
        col = new DataColumn("ReceivedQty");
        GRNItems.Columns.Add(col);


        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                DataRow dr = GRNItems.NewRow();

                dr["ItemName"] = gvrow.Cells[1].Text;
                dr["UOM"] = gvrow.Cells[2].Text;
                dr["OrderedQty"] = gvrow.Cells[3].Text;
                dr["ReceivedQty"] = gvrow.Cells[4].Text;

                GRNItems.Rows.Add(dr);
            }
        }

        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Value)
                {
                    gvItemsDetails.DataSource = GRNItems;
                    gvItemsDetails.DataBind();
                    MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = GRNItems.NewRow();

        drnew["ItemName"] = ddlItemName.SelectedItem.Value;
        drnew["UOM"] = txtUOM.Text;
        drnew["OrderedQty"] = txtOrderedQty.Text;
        drnew["ReceivedQty"] = txtReceivedQty.Text;

        GRNItems.Rows.Add(drnew);

        gvItemsDetails.DataSource = GRNItems;
        gvItemsDetails.DataBind();
        btnItemsRefresh_Click(sender, e);
    }
    #endregion

    #region Button ITEMS REFRESH Click
    protected void btnItemsRefresh_Click(object sender, EventArgs e)
    {
        ddlItemName.SelectedValue = "0";
        txtUOM.Text = string.Empty;
        txtOrderedQty.Text = string.Empty;
        txtReceivedQty.Text = string.Empty;

    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblGRNDetails.Visible = false;
    }
    #endregion


    #region ddlMRIRNo_SelectedIndexChanged
    protected void ddlMRIRNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Inventory.MRIR objMRIR = new Inventory.MRIR();

            if (objMRIR.MRIR_Select(ddlMRIRNo.SelectedItem.Value) > 0)
            {
                txtSupplierName.Text = objMRIR.SupName;
                txtSupplierAddress.Text = objMRIR.SupAddress;
                txtPONo.Text = objMRIR.FPOId;
                txtPODate.Text = objMRIR.FPODate;
                txtScheduleNo.Text = objMRIR.MRIRPDCNo;
                txtScheduleDate.Text = objMRIR.MRIRPDCDate;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Inventory.Dispose();
        }
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

                txtUOM.Text = objMaster.ItemUOMShort;
            }

            Inventory.GRNDetails objGRN = new Inventory.GRNDetails();
            if (objGRN.GRNItemCode_Select(ddlItemName.SelectedItem.Value, ddlMRIRNo.SelectedItem.Value) > 0)
            {

                txtOrderedQty.Text = objGRN.MRIRDetOrderedQty;
                //txtReceivedQty.Text = objGRN.GRNDetReceivedQty;

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            Inventory.Dispose();
        }
    }
    #endregion

    #region gvGRNDetails_RowDataBound
    protected void gvGRNDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region gvItemsDetails_RowDeleting
    protected void gvItemsDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemsDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable GRNItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        GRNItems.Columns.Add(col);
        col = new DataColumn("UOM");
        GRNItems.Columns.Add(col);
        col = new DataColumn("OrderedQty");
        GRNItems.Columns.Add(col);
        col = new DataColumn("ReceivedQty");
        GRNItems.Columns.Add(col);


        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = GRNItems.NewRow();
                    dr["ItemName"] = gvrow.Cells[1].Text;
                    dr["UOM"] = gvrow.Cells[2].Text;
                    dr["OrderedQty"] = gvrow.Cells[3].Text;
                    dr["ReceivedQty"] = gvrow.Cells[4].Text;


                    GRNItems.Rows.Add(dr);
                }
            }
        }
        gvItemsDetails.DataSource = GRNItems;
        gvItemsDetails.DataBind();
    }
    #endregion

    #region gvItemsDetails_RowDataBound
    protected void gvItemsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");

        }

    }
    #endregion


    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "PO Date")
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
        gvGRNDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvGRNDetails.DataBind();
    }
    #endregion






}

 
