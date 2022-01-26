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


public partial class Modules_Inventory_MRIR : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            SupplierMaster_Fill();
            PONo_Fill();
            ItemType_Fill();
            ItemNames_Fill();
            EmployeeMaster_Fill();

            tblMRIRDetails.Visible = false;
            tblInspectionDetails.Visible = true;

        }
    }
    #endregion

    #region Supplier Master Fill
    private void SupplierMaster_Fill()
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

    #region Item Type Fill
    private void ItemType_Fill()
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

    #region PO No Names Fill
    private void PONo_Fill()
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlInspectedBy);
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


    #region Link Button lbtnMRIRId_Click
    protected void lbtnMRIRId_Click(object sender, EventArgs e)
    {
        tblMRIRDetails.Visible = false;
        LinkButton lbtnMRIRId;
        lbtnMRIRId = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnMRIRId.Parent.Parent;
        gvMRIRDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {

            Inventory.MRIR objMRIR = new Inventory.MRIR();

            if (objMRIR.MRIR_Select(gvMRIRDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblMRIRDetails.Visible = true;

                txtMRIRNo.Text = objMRIR.MRIRNo;
                txtMRIRDate.Text = objMRIR.MRIRDate;
                ddlPONo.SelectedItem.Value = objMRIR.FPOId;
                txtPODate.Text = objMRIR.FPODate;
                txtPDCNo.Text = objMRIR.MRIRPDCNo;
                txtPDCDate.Text = objMRIR.MRIRPDCDate;
                ddlSupplierName.SelectedValue = objMRIR.SupId;
                //ddlItemType.SelectedValue = objMRIR.ItemType;
                //ddlItemName.SelectedValue = objMRIR.ItemCode;
                //txtReceivedQty.Text = objMRIR.MRIRDetReceivedQty;
                ddlPreparedBy.SelectedValue = objMRIR.MRIRPreparedBy;
                ddlApprovedBy.SelectedValue = objMRIR.MRIRApprovedBy;
                txtInvoiceNo.Text = objMRIR.MRIRInvoiceNo;
                txtInvoiceDate.Text = objMRIR.MRIRInvoiceDate;
                txtLRNo.Text = objMRIR.MRIRLRNo;
                txtVehicleNo.Text = objMRIR.MRIRVehicleNo;
                txtFromStation.Text = objMRIR.MRIRFromStation;
                txtTransportName.Text = objMRIR.MRIRTransportName;
                txtChallanNo.Text = objMRIR.MRIRChallanNo;
                txtChallanDate.Text = objMRIR.MRIRChallanDate;
                txtGatePassNo.Text = objMRIR.MRIRGatePassNo;
                txtGatePassDate.Text = objMRIR.MRIRGatePassDate;
                if (chkNotInStock.Checked == true)
                {
                    objMRIR.MRIRNotInStock = "True";
                }
                if (chkInExcisble.Checked == true)
                {
                    objMRIR.MRIRIsExcisble = "True";
                }

                objMRIR.MRIRDetails_Select(gvMRIRDetails.SelectedRow.Cells[0].Text, gvItemsDetails);

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
            ddlSupplierName_SelectedIndexChanged(sender, e);
            ddlPONo_SelectedIndexChanged(sender, e);
        }

    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMRIRDetails.SelectedIndex = -1;
        tblMRIRDetails.Visible = true;
        tblInspectionDetails.Visible = false;

        Inventory.ClearControls(this);

        txtMRIRNo.Text = Inventory.MRIR.MRIR_AutoGenCode();
        txtMRIRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //btnSave.Text = "Save";

        gvItemsDetails.DataBind();

    }
    #endregion

    #region Button  SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            MRIRSave();
        }
        else if (btnSave.Text == "Update")
        {
            MRIRUpdate();
        }
    }
    #endregion


    #region MRIRSave
    private void MRIRSave()
    {
        if (gvItemsDetails.Rows.Count > 0)
        {
            try
            {

                Inventory.MRIR objMRIR = new Inventory.MRIR();

                Inventory.BeginTransaction();

                objMRIR.MRIRNo = txtMRIRNo.Text;
                objMRIR.MRIRDate = Yantra.Classes.General.toMMDDYYYY(txtMRIRDate.Text);
                objMRIR.FPOId = ddlPONo.SelectedItem.Value;
                //objMRIR.FPODate = txtPODate.Text;
                objMRIR.MRIRPDCNo = txtPDCNo.Text;
                objMRIR.MRIRPDCDate = Yantra.Classes.General.toMMDDYYYY(txtPDCDate.Text);
                objMRIR.SupId = ddlSupplierName.SelectedItem.Value;
                //objMRIR.ItemType = ddlItemType.SelectedItem.Value;
                //objMRIR.ItemName = ddlItemName.SelectedItem.Value;
                //objMRIR.MRIRDetReceivedQty = txtReceivedQty.Text;            
                objMRIR.MRIRInvoiceNo = txtInvoiceNo.Text;
                objMRIR.MRIRInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objMRIR.MRIRLRNo = txtLRNo.Text;
                objMRIR.MRIRVehicleNo = txtVehicleNo.Text;
                objMRIR.MRIRFromStation = txtFromStation.Text;
                objMRIR.MRIRTransportName = txtTransportName.Text;
                objMRIR.MRIRChallanNo = txtChallanNo.Text;
                objMRIR.MRIRChallanDate = Yantra.Classes.General.toMMDDYYYY(txtChallanDate.Text);
                objMRIR.MRIRGatePassNo = txtGatePassNo.Text;
                objMRIR.MRIRGatePassDate = Yantra.Classes.General.toMMDDYYYY(txtGatePassDate.Text);
                objMRIR.MRIRPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objMRIR.MRIRApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (chkNotInStock.Checked == true)
                {
                    objMRIR.MRIRNotInStock = "True";
                }
                if (chkInExcisble.Checked == true)
                {
                    objMRIR.MRIRIsExcisble = "True";
                }



                if (objMRIR.MRIR_Save() == "Data Saved Successfully")
                {
                    objMRIR.MRIRDetails_Delete(objMRIR.MRIRId);
                    foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                    {
                        objMRIR.ItemCode = gvrow.Cells[1].Text;
                        objMRIR.ItemName = gvrow.Cells[3].Text;
                        objMRIR.UOM = gvrow.Cells[4].Text;
                        objMRIR.MRIRDetReceivedQty = gvrow.Cells[5].Text;
                        objMRIR.MRIRDetOrderedQty = gvrow.Cells[6].Text;
                        objMRIR.MRIRDetails_Save();
                    }

                    if (tblInspectionDetails.Visible == true)
                    {
                        objMRIR.MRIRDetAccpQty = txtAcceptedQty.Text;
                        objMRIR.MRIRDetRejtQty = txtRejectQty.Text;
                        objMRIR.MRIRINSPDetVisual = txtVisual.Text;
                        objMRIR.MRIRINSPDetHardness = txtHardness.Text;
                        objMRIR.MRIRINSPDetSurfFinish = txtSurfaceFinish.Text;
                        objMRIR.MRIRINSPDetOthers = txtOthers.Text;
                        objMRIR.MRIRINSPDetSTC = fuSTC.FileName;
                        objMRIR.MRIRINSPDetInspStatus = ddlInspStatus.SelectedItem.Value;
                        objMRIR.MRIRINSPDetInspBy = ddlInspectedBy.SelectedItem.Value;
                        objMRIR.MRIRINSPDetRemarks = txtPDCNo.Text;


                        if (objMRIR.MRIR_Save() == "Data Saved Successfully")
                        {
                            objMRIR.MRIRInspectionDetails_Delete(objMRIR.MRIRId);
                            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                            {
                                objMRIR.ItemCode = gvrow.Cells[1].Text;
                                objMRIR.ItemType = gvrow.Cells[2].Text;
                                objMRIR.ItemName = gvrow.Cells[3].Text;
                                objMRIR.UOM = gvrow.Cells[4].Text;
                                objMRIR.MRIRDetReceivedQty = gvrow.Cells[5].Text;
                                objMRIR.MRIRDetOrderedQty = gvrow.Cells[6].Text;
                                objMRIR.MRIRDetAccpQty = gvrow.Cells[7].Text;
                                objMRIR.MRIRDetRejtQty = gvrow.Cells[8].Text;
                                objMRIR.MRIRINSPDetVisual = gvrow.Cells[9].Text;
                                objMRIR.MRIRINSPDetHardness = gvrow.Cells[10].Text;
                                objMRIR.MRIRINSPDetSurfFinish = gvrow.Cells[11].Text;
                                objMRIR.MRIRINSPDetOthers = gvrow.Cells[12].Text;
                                objMRIR.MRIRINSPDetSTC = gvrow.Cells[13].Text;
                                objMRIR.MRIRINSPDetInspStatus = gvrow.Cells[14].Text;
                                objMRIR.MRIRINSPDetInspBy = gvrow.Cells[15].Text;
                                objMRIR.MRIRINSPDetRemarks = gvrow.Cells[16].Text;

                                objMRIR.MRIRInspectionDetails_Save();
                            }
                        }
                        Inventory.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
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
                gvMRIRDetails.DataBind();
                gvItemsDetails.DataBind();
                tblInspectionDetails.Visible = false;
                tblMRIRDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for MRIR ");
        }
    }
    #endregion

    #region MRIRUpdate
    private void MRIRUpdate()
    {
        if (gvItemsDetails.Rows.Count > 0)
        {
            try
            {
                Inventory.MRIR objMRIR = new Inventory.MRIR();

                Inventory.BeginTransaction();
                objMRIR.MRIRId = gvMRIRDetails.SelectedRow.Cells[0].Text;
                objMRIR.MRIRNo = txtMRIRNo.Text;
                objMRIR.MRIRDate = Yantra.Classes.General.toMMDDYYYY(txtMRIRDate.Text);
                objMRIR.FPOId = ddlPONo.SelectedItem.Value;
                //  objMRIR.FPODate = txtPODate.Text;
                objMRIR.MRIRPDCNo = txtPDCNo.Text;
                objMRIR.MRIRPDCDate = Yantra.Classes.General.toMMDDYYYY(txtPDCDate.Text);
                objMRIR.SupId = ddlSupplierName.SelectedItem.Value;
                //objMRIR.ItemType = ddlItemType.SelectedItem.Value;
                //objMRIR.ItemName = ddlItemName.SelectedItem.Value;
                //objMRIR.MRIRDetReceivedQty = txtReceivedQty.Text;
                objMRIR.MRIRInvoiceNo = txtInvoiceNo.Text;
                objMRIR.MRIRInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objMRIR.MRIRLRNo = txtLRNo.Text;
                objMRIR.MRIRVehicleNo = txtVehicleNo.Text;
                objMRIR.MRIRFromStation = txtFromStation.Text;
                objMRIR.MRIRTransportName = txtTransportName.Text;
                objMRIR.MRIRChallanNo = txtChallanNo.Text;
                objMRIR.MRIRChallanDate = Yantra.Classes.General.toMMDDYYYY(txtChallanDate.Text);
                objMRIR.MRIRGatePassNo = txtGatePassNo.Text;
                objMRIR.MRIRGatePassDate = Yantra.Classes.General.toMMDDYYYY(txtGatePassDate.Text);

                objMRIR.MRIRPreparedBy = ddlPreparedBy.SelectedItem.Value;
                objMRIR.MRIRApprovedBy = ddlApprovedBy.SelectedItem.Value;
                if (chkNotInStock.Checked == true)
                {
                    objMRIR.MRIRNotInStock = "True";
                }
                if (chkInExcisble.Checked == true)
                {
                    objMRIR.MRIRIsExcisble = "True";
                }

                if (objMRIR.MRIR_Update() == "Data Updated Successfully")
                {
                    objMRIR.MRIRDetails_Delete(objMRIR.MRIRId);
                    foreach (GridViewRow gvrow in gvItemsDetails.Rows)
                    {
                        objMRIR.ItemCode = gvrow.Cells[1].Text;
                        objMRIR.ItemType = gvrow.Cells[2].Text;
                        objMRIR.ItemName = gvrow.Cells[3].Text;
                        objMRIR.UOM = gvrow.Cells[4].Text;
                        objMRIR.MRIRDetReceivedQty = gvrow.Cells[5].Text;
                        objMRIR.MRIRDetOrderedQty = gvrow.Cells[6].Text;
                        objMRIR.MRIRDetAccpQty = gvrow.Cells[7].Text;
                        objMRIR.MRIRDetRejtQty = gvrow.Cells[8].Text;
                        objMRIR.MRIRINSPDetVisual = gvrow.Cells[9].Text;
                        objMRIR.MRIRINSPDetHardness = gvrow.Cells[10].Text;
                        objMRIR.MRIRINSPDetSurfFinish = gvrow.Cells[11].Text;
                        objMRIR.MRIRINSPDetOthers = gvrow.Cells[12].Text;
                        objMRIR.MRIRINSPDetSTC = gvrow.Cells[13].Text;
                        objMRIR.MRIRINSPDetInspStatus = gvrow.Cells[14].Text;
                        objMRIR.MRIRINSPDetInspBy = gvrow.Cells[15].Text;
                        objMRIR.MRIRINSPDetRemarks = gvrow.Cells[16].Text;


                        objMRIR.MRIRInspectionDetails_Save();

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
                gvMRIRDetails.DataBind();
                // gvQuotationItems.DataBind();
                gvItemsDetails.DataBind();
                tblInspectionDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item MRIR");
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        if (gvMRIRDetails.SelectedIndex > -1)
        {
            try
            {

                Inventory.MRIR objMRIR = new Inventory.MRIR();

                if (objMRIR.MRIR_Select(gvMRIRDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblMRIRDetails.Visible = true;

                    txtMRIRNo.Text = objMRIR.MRIRNo;
                    txtMRIRDate.Text = objMRIR.MRIRDate;
                    ddlPONo.SelectedValue = objMRIR.FPOId;
                    txtPODate.Text = objMRIR.FPODate;
                    txtPDCNo.Text = objMRIR.MRIRPDCNo;
                    txtPDCDate.Text = objMRIR.MRIRPDCDate;
                    ddlSupplierName.SelectedValue = objMRIR.SupId;
                    txtInvoiceNo.Text = objMRIR.MRIRInvoiceNo;
                    txtInvoiceDate.Text = objMRIR.MRIRInvoiceDate;
                    txtLRNo.Text = objMRIR.MRIRLRNo;
                    txtVehicleNo.Text = objMRIR.MRIRVehicleNo;
                    txtFromStation.Text = objMRIR.MRIRFromStation;
                    txtTransportName.Text = objMRIR.MRIRTransportName;
                    txtChallanNo.Text = objMRIR.MRIRChallanNo;
                    txtChallanDate.Text = objMRIR.MRIRChallanDate;
                    txtGatePassNo.Text = objMRIR.MRIRGatePassNo;
                    txtGatePassDate.Text = objMRIR.MRIRGatePassDate;
                    ddlPreparedBy.SelectedValue = objMRIR.MRIRPreparedBy;
                    ddlApprovedBy.SelectedValue = objMRIR.MRIRApprovedBy;
                    if (chkNotInStock.Checked == true)
                    {
                        objMRIR.MRIRNotInStock = "True";
                    }
                    if (chkInExcisble.Checked == true)
                    {
                        objMRIR.MRIRIsExcisble = "True";
                    }

                    objMRIR.MRIRDetails_Select(gvMRIRDetails.SelectedRow.Cells[0].Text, gvItemsDetails);
                    objMRIR.MRIRInspectionDetails_Select(gvMRIRDetails.SelectedRow.Cells[0].Text, gvItemsDetails);
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
                ddlSupplierName_SelectedIndexChanged(sender, e);
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
        if (gvMRIRDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.MRIR objMRIR = new Inventory.MRIR();

                MessageBox.Show(this, objMRIR.MRIR_Delete(gvMRIRDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvMRIRDetails.DataBind();
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
        // gvQuotationItems.DataBind();
        gvItemsDetails.DataBind();
    }
    #endregion

    #region Button INSPECTION Click
    protected void btnInspect_Click(object sender, EventArgs e)
    {
        tblInspectionDetails.Visible = true;
        tblMRIRDetails.Visible = true;
        lblAcceptedQty.Visible = true;
        txtAcceptedQty.Visible = true;
        lblRejectQty.Visible = true;
        txtRejectQty.Visible = true;


        txtMRIRNo.Text = Inventory.MRIR.MRIR_AutoGenCode();
        txtMRIRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";

        gvItemsDetails.DataBind();



        //try
        //{
        //    if (btnSave.Text == "Save")
        //    {
        //        MRIRInspSave();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    //Inventory.RollBackTransaction();
        //    MessageBox.Show(this, ex.Message.ToString());
        //}

    }
    #endregion

    #region MRIRInspSave
    //private void MRIRInspSave()
    //{
    //    if (gvItemsDetails.Rows.Count > 0)
    //    {
    //        try
    //        {

    //            Inventory.MRIR objMRIR = new Inventory.MRIR();

    //            Inventory.BeginTransaction();

    //            objMRIR.MRIRNo = txtMRIRNo.Text;
    //            objMRIR.MRIRDate = txtMRIRDate.Text;
    //            objMRIR.FPOId = ddlPONo.SelectedItem.Value;
    //            objMRIR.MRIRPDCNo = txtPDCNo.Text;
    //            objMRIR.MRIRPDCDate = txtPDCDate.Text;
    //            objMRIR.SupId = ddlSupplierName.SelectedItem.Value;
    //            objMRIR.ItemType = ddlItemType.SelectedItem.Value;
    //            objMRIR.ItemName = ddlItemName.SelectedItem.Value;
    //            objMRIR.MRIRDetReceivedQty = txtReceivedQty.Text;


    //            if (objMRIR.MRIR_Save() == "Data Saved Successfully")
    //            {
    //                objMRIR.MRIRInspectionDetails_Delete(objMRIR.MRIRId);
    //                foreach (GridViewRow gvrow in gvItemsDetails.Rows)
    //                {
    //                    objMRIR.ItemType = gvrow.Cells[1].Text;
    //                    objMRIR.ItemName = gvrow.Cells[2].Text;
    //                    objMRIR.UOM = gvrow.Cells[3].Text;
    //                    objMRIR.MRIRDetReceivedQty = gvrow.Cells[4].Text;
    //                    objMRIR.MRIRDetAccpQty = gvrow.Cells[5].Text;
    //                    objMRIR.MRIRDetRejtQty = gvrow.Cells[6].Text;
    //                    objMRIR.MRIRINSPDetVisual = gvrow.Cells[7].Text;
    //                    objMRIR.MRIRINSPDetHardness = gvrow.Cells[8].Text;
    //                    objMRIR.MRIRINSPDetSurfFinish = gvrow.Cells[9].Text;
    //                    objMRIR.MRIRINSPDetOthers = gvrow.Cells[10].Text;
    //                    objMRIR.MRIRINSPDetSTC = gvrow.Cells[11].Text;
    //                    objMRIR.MRIRINSPDetInspStatus = gvrow.Cells[12].Text;
    //                    objMRIR.MRIRINSPDetInspBy = gvrow.Cells[13].Text;
    //                    objMRIR.MRIRINSPDetRemarks = gvrow.Cells[14].Text;


    //                    objMRIR.MRIRInspectionDetails_Save();

    //                }
    //                Inventory.CommitTransaction();
    //                MessageBox.Show(this, "Data Saved Successfully");
    //            }
    //            else
    //            {
    //                Inventory.RollBackTransaction();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Inventory.RollBackTransaction();
    //            MessageBox.Show(this, ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            btnDelete.Attributes.Clear();
    //            gvMRIRDetails.DataBind();
    //            // gvQuotationItems.DataBind();
    //            gvItemsDetails.DataBind();
    //            tblInspectionDetails.Visible = false;
    //            tblMRIRDetails.Visible = false;
    //            Inventory.ClearControls(this);
    //            Inventory.Dispose();
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please add atleast one Item for MRIR ");
    //    }
    //}
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblMRIRDetails.Visible = false;
    }
    #endregion

    #region Button ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable MRIRItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("UOM");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("ReceivedQty");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("OrderedQty");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("AcceptedQty");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("RejectedQty");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("Visual");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("Hardness");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("SurfaceFinish");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("Others");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("STC");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("InspectedStatus");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("InspectedBy");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        MRIRItems.Columns.Add(col);

        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                DataRow dr = MRIRItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemType"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["UOM"] = gvrow.Cells[4].Text;
                dr["ReceivedQty"] = gvrow.Cells[5].Text;
                dr["OrderedQty"] = gvrow.Cells[6].Text;
                dr["AcceptedQty"] = gvrow.Cells[7].Text;
                dr["RejectedQty"] = gvrow.Cells[8].Text;
                dr["Visual"] = gvrow.Cells[9].Text;
                dr["Hardness"] = gvrow.Cells[10].Text;
                dr["SurfaceFinish"] = gvrow.Cells[11].Text;
                dr["Others"] = gvrow.Cells[12].Text;
                dr["STC"] = gvrow.Cells[13].Text;
                dr["InspectedStatus"] = gvrow.Cells[14].Text;
                dr["InspectedBy"] = gvrow.Cells[15].Text;
                dr["Remarks"] = gvrow.Cells[16].Text;



                MRIRItems.Rows.Add(dr);
            }
        }

        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Value)
                {
                    gvItemsDetails.DataSource = MRIRItems;
                    gvItemsDetails.DataBind();
                    MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = MRIRItems.NewRow();
        drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
        drnew["ItemType"] = ddlItemType.SelectedItem.Value;
        drnew["ItemName"] = ddlItemName.SelectedItem.Value;
        drnew["UOM"] = txtUOM.Text;
        drnew["ReceivedQty"] = txtReceivedQty.Text;
        drnew["OrderedQty"] = txtOrderedQty.Text;

        drnew["AcceptedQty"] = txtAcceptedQty.Text;
        drnew["RejectedQty"] = txtRejectQty.Text;
        drnew["Visual"] = txtVisual.Text;
        drnew["Hardness"] = txtHardness.Text;
        drnew["SurfaceFinish"] = txtSurfaceFinish.Text;
        drnew["Others"] = txtOthers.Text;
        drnew["STC"] = fuSTC.FileName;
        drnew["InspectedStatus"] = ddlInspStatus.SelectedItem.Value;
        drnew["InspectedBy"] = ddlInspectedBy.SelectedItem.Value;
        drnew["Remarks"] = txtRemarks.Text;


        MRIRItems.Rows.Add(drnew);

        gvItemsDetails.DataSource = MRIRItems;
        gvItemsDetails.DataBind();
        btnItemsRefresh_Click(sender, e);

    }
    #endregion


    #region Button ITEMS REFRESH Click
    protected void btnItemsRefresh_Click(object sender, EventArgs e)
    {
        ddlItemType.SelectedValue = "0";
        ddlItemName.SelectedValue = "0";
        txtOrderedQty.Text = string.Empty;
        txtUOM.Text = string.Empty;
        txtReceivedQty.Text = string.Empty;

    }
    #endregion

    #region gvMRIRDetails_RowDataBound
    protected void gvMRIRDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
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
            }
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

    #region ddlItemName_SelectedIndexChanged
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                // txtItemName.Text = objMaster.ItemName;
                txtUOM.Text = objMaster.ItemUOMShort;
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

    #region ddlSupplierName_SelectedIndexChanged
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.SuppliersMaster objSupplierMaster = new SCM.SuppliersMaster();

            if (objSupplierMaster.SuppliersMaster_Select(ddlSupplierName.SelectedItem.Value) > 0)
            {
                txtContactPerson.Text = objSupplierMaster.SupContactPerson;
                txtPhoneNo.Text = objSupplierMaster.SupPhone;
                txtEmail.Text = objSupplierMaster.SupEmail;
                txtAddress.Text = objSupplierMaster.SupAddress;
            }
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

    #region gvItemsDetails_RowDeleting
    protected void gvItemsDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemsDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable MRIRItems = new DataTable();

        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("UOM");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("ReceivedQty");
        MRIRItems.Columns.Add(col);
        col = new DataColumn("OrderedQty");
        MRIRItems.Columns.Add(col);


        if (gvItemsDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemsDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = MRIRItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["ItemType"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["OrderedQty"] = gvrow.Cells[6].Text;
                    dr["ReceivedQty"] = gvrow.Cells[5].Text;
                    MRIRItems.Rows.Add(dr);

                }
            }
        }
        gvItemsDetails.DataSource = MRIRItems;
        gvItemsDetails.DataBind();
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
        gvMRIRDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvMRIRDetails.DataBind();
    }
    #endregion






    protected void gvItemsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[7].Visible = false;
        }


    }
}

 
