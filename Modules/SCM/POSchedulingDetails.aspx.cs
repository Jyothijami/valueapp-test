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

public partial class Modules_PurchasingManagement_POSchedulingDetails : System.Web.UI.Page
{


    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PONO_Fill();

            Supplier_Fill();
            ItemTypes_Fill();
            EmployeeMaster_Fill();


            txtSchQty.Attributes.Add("onkeyup", "javascript:Grandamtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:Grandamtcalc();");

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
        finally
        {
            SCM.Dispose();
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

    #region PONO Fill
    private void PONO_Fill()
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

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //gvPOSchedulingDetails.SelectedIndex = -1;
        btnDelete.Attributes.Clear();
        SCM.ClearControls(this);
        txtPOScheduleNo.Text = SCM.POScheduling.POScheduling_AutoGenCode();
        txtPOScheduleDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        tblPOSchedulingDetails.Visible = true;

    }
    #endregion

    #region Button SAVE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            POSchedulingSave();
        }
        else if (btnSave.Text == "Update")
        {
            POSchedulingUpdate();
        }
    }
    #endregion

    #region POSchedulingSave
    private void POSchedulingSave()
    {
        if (gvScheduledItems.Rows.Count > 0)
        {
            try
            {
                SCM.POScheduling objPOSchedule = new SCM.POScheduling();

                SCM.BeginTransaction();

                objPOSchedule.POSDate = Yantra.Classes.General.toMMDDYYYY(txtPOScheduleDate.Text);
                objPOSchedule.FPOId = ddlPONo.SelectedItem.Value;
                //objPOSchedule.FPOPOType = ddlPOType.SelectedItem.Value;
                objPOSchedule.POSStatus = "New";
                objPOSchedule.POSPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objPOSchedule.POSApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objPOSchedule.POScheduling_Save() == "Data Saved Successfully")
                {
                    objPOSchedule.POSchedulingDetails_Delete(objPOSchedule.POSId);
                    foreach (GridViewRow gvrow in gvScheduledItems.Rows)
                    {
                        objPOSchedule.ItemCode = gvrow.Cells[1].Text;
                        //objPOSchedule.ItemName = gvrow.Cells[2].Text;
                        objPOSchedule.UOM = gvrow.Cells[4].Text;
                        objPOSchedule.POSDetRate = gvrow.Cells[5].Text;
                        objPOSchedule.POSDetQty = gvrow.Cells[6].Text;
                        objPOSchedule.POSDetSchQty = gvrow.Cells[7].Text;

                        objPOSchedule.POSDetSchDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[9].Text);

                        objPOSchedule.POSchedulingDetails_Save();
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
                tblPOSchedulingDetails.Visible = false;
                gvScheduledItems.DataBind();
                gvPOItemsDetails.DataBind();
                btnDelete.Attributes.Clear();
                gvPOSchedulingDetails.DataBind();
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

    #region POSchedulingUpdate
    private void POSchedulingUpdate()
    {
        if (gvScheduledItems.Rows.Count > 0)
        {
            try
            {
                SCM.POScheduling objPOSchedule = new SCM.POScheduling();

                SCM.BeginTransaction();
                objPOSchedule.POSId = gvPOSchedulingDetails.SelectedRow.Cells[0].Text;

                objPOSchedule.POSNo = txtPOScheduleNo.Text;
                objPOSchedule.POSDate = Yantra.Classes.General.toMMDDYYYY(txtPOScheduleDate.Text);
                objPOSchedule.FPOId = ddlPONo.SelectedItem.Value;
                objPOSchedule.POSPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objPOSchedule.POSApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objPOSchedule.POSStatus = "New";

                if (objPOSchedule.POScheduling_Update() == "Data Updated Successfully")
                {
                    objPOSchedule.POSchedulingDetails_Delete(objPOSchedule.POSId);
                    foreach (GridViewRow gvrow in gvScheduledItems.Rows)
                    {
                        objPOSchedule.ItemCode = gvrow.Cells[1].Text;
                        //objPOSchedule.ItemName = gvrow.Cells[2].Text;
                        objPOSchedule.UOM = gvrow.Cells[4].Text;
                        objPOSchedule.POSDetRate = gvrow.Cells[5].Text;
                        objPOSchedule.POSDetQty = gvrow.Cells[6].Text;
                        objPOSchedule.POSDetSchQty = gvrow.Cells[7].Text;
                        objPOSchedule.POSDetSchDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[9].Text);

                        objPOSchedule.POSchedulingDetails_Save();
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
                tblPOSchedulingDetails.Visible = false;
                gvScheduledItems.DataBind();
                btnDelete.Attributes.Clear();
                gvPOItemsDetails.DataBind();
                gvPOSchedulingDetails.DataBind();
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
        if (gvPOSchedulingDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.POScheduling objPOSchedule = new SCM.POScheduling();

                if (objPOSchedule.POScheduling_Select(gvPOSchedulingDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblPOSchedulingDetails.Visible = true;


                    txtPOScheduleNo.Text = objPOSchedule.POSNo;
                    txtPOScheduleDate.Text = objPOSchedule.POSDate;
                    ddlPONo.SelectedValue = objPOSchedule.FPOId;
                    txtPODate.Text = objPOSchedule.PODate;
                    //ddlPOType.SelectedValue = objPOSchedule.FPOPOType;
                    ddlSupplierName.SelectedValue = objPOSchedule.SupId;
                    ddlPreparedBy.SelectedValue = objPOSchedule.POSPreparedBy;
                    ddlApprovedBy.SelectedValue = objPOSchedule.POSApprovedBy;

                    objPOSchedule.POSchedulingDetails_Select(gvPOSchedulingDetails.SelectedRow.Cells[0].Text, gvScheduledItems);

                    //ddlItemCode.SelectedValue = objPOSchedule.ItemCode;
                    //txtRate.Text = objPOSchedule.POSDetRate;
                    //txtPOQty.Text = objPOSchedule.POSDetQty;
                    //txtSchQty.Text = objPOSchedule.POSDetSchQty;
                    //txtScheduledDate.Text = objPOSchedule.POSDetSchDate.ToString();

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
                ddlPONo_SelectedIndexChanged(sender, e);
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
        if (gvPOSchedulingDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.POScheduling objPOSchedule = new SCM.POScheduling();
                SCM.BeginTransaction();
                MessageBox.Show(this, objPOSchedule.POScheduling_Delete(gvPOSchedulingDetails.SelectedRow.Cells[0].Text));
                SCM.CommitTransaction();
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvPOSchedulingDetails.DataBind();
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

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        gvScheduledItems.DataBind();
    }
    #endregion

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable POSchedulingItems = new DataTable();

            DataColumn col = new DataColumn();
            col = new DataColumn("ItemCode");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("ItemName");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("ItemType");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("UOM");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("Rate");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("POQuantity");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("SchQuantity");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("GrandAmt");
            POSchedulingItems.Columns.Add(col);
            col = new DataColumn("SchDate");
            POSchedulingItems.Columns.Add(col);

            if (gvScheduledItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvScheduledItems.Rows)
                {
                    DataRow dr = POSchedulingItems.NewRow();

                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["Rate"] = gvrow.Cells[5].Text;
                    dr["POQuantity"] = gvrow.Cells[6].Text;
                    dr["SchQuantity"] = gvrow.Cells[7].Text;
                    dr["GrandAmt"] = gvrow.Cells[8].Text;
                    dr["SchDate"] = gvrow.Cells[9].Text;


                    POSchedulingItems.Rows.Add(dr);
                }
            }

            if (gvScheduledItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvScheduledItems.Rows)
                {
                    if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Text)
                    {
                        gvScheduledItems.DataSource = POSchedulingItems;
                        gvScheduledItems.DataBind();
                        MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }

            DataRow drnew = POSchedulingItems.NewRow();

            drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
            drnew["ItemName"] = ddlItemName.SelectedItem.Text;
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["UOM"] = txtUOM.Text;
            drnew["Rate"] = txtRate.Text;
            drnew["POQuantity"] = txtPOQty.Text;
            drnew["SchQuantity"] = txtSchQty.Text;
            drnew["GrandAmt"] = txtGrandAmt.Text;
            drnew["SchDate"] = txtScheduledDate.Text;


            POSchedulingItems.Rows.Add(drnew);

            gvScheduledItems.DataSource = POSchedulingItems;
            gvScheduledItems.DataBind();
            btnItemsRefresh_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    #endregion

    #region Button ITEMS REFRESH Click
    protected void btnItemsRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        ddlItemName.SelectedValue = "0";

        txtUOM.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtPOQty.Text = string.Empty;
        txtSchQty.Text = string.Empty;
        txtGrandAmt.Text = string.Empty;
        txtScheduledDate.Text = string.Empty;

    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblPOSchedulingDetails.Visible = false;

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
                txtUOM.Text = objMaster.ItemUOMShort;

            }
            foreach (GridViewRow rv in gvPOItemsDetails.Rows)
            {
                if (rv.Cells[0].Text.ToString() == ddlItemName.SelectedItem.Value)
                {
                    txtPOQty.Text = rv.Cells[3].Text;
                    txtRate.Text = Convert.ToString (Convert.ToDouble (rv.Cells[4].Text));
                }
                            
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

    #region  gvPOSchedulingDetails_RowDataBound
    protected void gvPOSchedulingDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region gvScheduledItems_RowDeleting
    protected void gvScheduledItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvScheduledItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable POSchedulingItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("UOM");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("Rate");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("POQuantity");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("SchQuantity");
        //POSchedulingItems.Columns.Add(col);
        //col = new DataColumn("GrandAmt");
        POSchedulingItems.Columns.Add(col);
        col = new DataColumn("SchDate");
        POSchedulingItems.Columns.Add(col);

        if (gvScheduledItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvScheduledItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = POSchedulingItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["Rate"] = gvrow.Cells[5].Text;
                    dr["POQuantity"] = gvrow.Cells[6].Text;
                    dr["SchQuantity"] = gvrow.Cells[7].Text;
                    //dr["GrandAmt"] = gvrow.Cells[8].Text;
                    dr["SchDate"] = gvrow.Cells[9].Text;


                    POSchedulingItems.Rows.Add(dr);

                    //if (gvScheduledItems.Rows.Count == 0)
                    //{
                    //    txtGrandAmt.Text = "";

                    //}
                }
            }
        }
        gvScheduledItems.DataSource = POSchedulingItems;
        gvScheduledItems.DataBind();

    }
    #endregion

    #region gvScheduledItems_RowDataBound
    protected void gvScheduledItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            //e.Row.Cells[9].Text = Convert.ToDateTime(e.Row.Cells[9].Text).ToShortDateString();
            e.Row.Cells[8].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[5].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));
            //e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[1].Visible = false;
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

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvPOSchedulingDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvPOSchedulingDetails.DataBind();
    }
    #endregion

    #region Link Button lbtnPOScheduleNo_Click
    protected void lbtnPOScheduleNo_Click(object sender, EventArgs e)
    {

        tblPOSchedulingDetails.Visible = false;
        LinkButton lbtnPOScheduleNo;
        lbtnPOScheduleNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPOScheduleNo.Parent.Parent;
        gvPOSchedulingDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        try
        {
            SCM.POScheduling objPOSchedule = new SCM.POScheduling();

            if (objPOSchedule.POScheduling_Select(gvPOSchedulingDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblPOSchedulingDetails.Visible = true;


                txtPOScheduleNo.Text = objPOSchedule.POSNo;
                txtPOScheduleDate.Text = objPOSchedule.POSDate;
                ddlPONo.SelectedValue = objPOSchedule.FPOId;
                txtPODate.Text = objPOSchedule.PODate;
                //ddlPOType.SelectedValue = objPOSchedule.FPOPOType;
                ddlSupplierName.SelectedValue = objPOSchedule.SupId;
                ddlPreparedBy.SelectedValue = objPOSchedule.POSPreparedBy;
                ddlApprovedBy.SelectedValue = objPOSchedule.POSApprovedBy;

                objPOSchedule.POSchedulingDetails_Select(gvPOSchedulingDetails.SelectedRow.Cells[0].Text, gvScheduledItems);

                //ddlItemCode.SelectedValue = objPOSchedule.ItemCode;
                //txtRate.Text = objPOSchedule.POSDetRate;
                //txtPOQty.Text = objPOSchedule.POSDetQty;
                //txtSchQty.Text = objPOSchedule.POSDetSchQty;
                //txtScheduledDate.Text = objPOSchedule.POSDetSchDate.ToString();

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
            ddlPONo_SelectedIndexChanged(sender, e);
        }

    }
    #endregion

    #region ddlPONo_SelectedIndexChanged
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierFixedPO objFixedPO = new SCM.SupplierFixedPO();

            if (objFixedPO.SuppliersFixedPO_Select(ddlPONo.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objFixedPO.FPODate;
            //    ddlPOType.SelectedValue = objFixedPO.FPOPOType;
                ddlSupplierName.SelectedValue = objFixedPO.SupId;

                objFixedPO.SuppliersFixedPODetails_Select(ddlPONo.SelectedItem.Value, gvPOItemsDetails);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            SCM.Dispose();
        }
    }
    #endregion

    #region Grand Amount Calculation txtPOQty_TextChanged
    protected void txtSchQty_TextChanged(object sender, EventArgs e)
    {
        if (txtRate.Text != "")
            txtGrandAmt.Text = Convert.ToString((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtSchQty.Text)));


    }
    #endregion

    #region Grand Amount Calculation txtRate_TextChanged
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        if (txtSchQty.Text != "")
            txtGrandAmt.Text = Convert.ToString((Convert.ToDouble(txtRate.Text)) * (Convert.ToDouble(txtSchQty.Text)));

    }
    #endregion

    #region gvPOItemsDetails_RowDataBound
    protected void gvPOItemsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }

    }
    #endregion 

}

 
