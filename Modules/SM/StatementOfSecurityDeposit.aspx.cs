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

public partial class Modules_SM_StatementOfSecurityDeposit : System.Web.UI.Page
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //tblsdDetils.Visible = true;



        if (!IsPostBack)
        {
            //ddlStatementOf.Attributes.Add("onclick", "javascript:SDBGEnableDisable();");
            //ddlStatementOf.Attributes.Add("onclick", "javascript:SDBGEnableDisable();");
            CustomerMaster_Fill();

            EmployeeMaster_Fill();
            TenderNo_Fill();
            salesOrder_Fill();

            tblDepositDetails.Visible = false;

        }

    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustName);




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

    #region TenderNo Fill
    private void TenderNo_Fill()
    {
        try
        {

            SM.SalesEnquiry.SalesEnquiryTenderNo_Select(ddlTenderNo);


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

    #region  salesOrder_Fill
    private void salesOrder_Fill()
    {
        try
        {
            SM.SalesEnquiry.SalesOrderByTender_Select(ddlTenderNo.SelectedItem.Value, ddlSOPNo);
            //SM.SalesOrder.SalesOrder_Select(ddlSOPNo);
            //SM.SalesOrder.SalesOrder_Select(txtSOPDate);
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

    #region Unit Name Fill
    //private void UnitName_Fill()
    //{
    //    try
    //    {
    //        SM.CustomerMaster.CustomerUnits_Select(txtUnitName, txtCustomerName.text);
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
    #endregion

    #region LINK Button  lbtnSDNo Click
    protected void lbtnSDNo_Click(object sender, EventArgs e)
    {

        tblDepositDetails.Visible = false;
        LinkButton lbtnSDNo;
        lbtnSDNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSDNo.Parent.Parent;
        gvSdDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        String Temp_SOID = String.Empty;
        tblDepositDetails.Visible = true;

        if (gvSdDetails.SelectedIndex > -1)
        {
            try
            {




                SM.SDBG objsd = new SM.SDBG();
                SM.BeginTransaction();
                if (objsd.SDBG_Select(gvSdDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblDepositDetails.Visible = true;
                    tblsdDetils.Visible = true;
                    lblCUSTIdHidden.Text = objsd.CustId;
                    txtSDNo.Text = objsd.SDBGNo;
                    txtDate.Text = objsd.SDBGDate;
                    //txtCustomerName.Text = objsd.CustId;

                    ddlTenderNo.SelectedValue = objsd.EnqId;
                    Temp_SOID = objsd.SoId;

                    salesOrder_Fill();

                    //txtDDNo.Text = objsd.SDBGDdNO;
                    //txtDDDate.Text = objsd.SDBGDdDate;
                    //txtDDNo.Text = objsd.SDBGDdNO;
                    //txtAmount.Text = objsd.SDBGAmount;
                    //txtBank.Text = objsd.SDBGBank;
                    //txtDueDate.Text= objsd.SDBGDueDate;
                    //txtRemarks.Text = objsd.SDBGRemarks;
                    ddlStatementOf.SelectedValue = objsd.SDBGStatementOf;


                    objsd.SDBGDetails_Select(gvSdDetails.SelectedRow.Cells[0].Text, gvPreviousPayments);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {


                ddlTenderNo_SelectedIndexChanged(sender, e);
                ddlSOPNo.SelectedValue = Temp_SOID;
                ddlSOPNo_SelectedIndexChanged1(sender, e);
                SM.Dispose();

            }

        }
    }

    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);

        txtSDNo.Text = SM.SDBG.SDBG_AutoGenCode();

        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        tblDepositDetails.Visible = true;
        tblsdDetils.Visible = false;


    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SDBGSave();
        }
        else if (btnSave.Text == "Update")
        {
            SDBGUpdate();
        }
    }
    #endregion

    //#region Add
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    tblsdDetils.Visible = true;
    //    txtSecurityNo.Text = SM.SDBG.SDBGDetails_AutoGenCode();
    //}
    //#endregion

    #region SDBGSave
    private void SDBGSave()
    {
        //if (gvSdDetails.SelectedIndex > -1)
        //{
        try
        {
            SM.SDBG objsd = new SM.SDBG();
            SM.BeginTransaction();
            objsd.SDBGNo = txtSDNo.Text;
            objsd.CustId = lblCUSTIdHidden.Text;

            objsd.SDBGDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
            //objsd.CustId = txtCustomerName.Text;
            objsd.SDBGStatementOf = ddlStatementOf.SelectedItem.Value;
            objsd.EnqId = ddlTenderNo.SelectedItem.Value;
            objsd.SoId = ddlSOPNo.SelectedItem.Value;
            //objsd.SDBGDdNO = txtDDNo.Text;
            //objsd.SDBGDdDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
            //objsd.SDBGDdNO = txtDDNo.Text;
            ////objsd.SDBGAmount = txtAmount.Text;
            //objsd.SDBGBank = txtBank.Text;
            //objsd.SDBGDueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            //objsd.SDBGRemarks = txtRemarks.Text;

            //if (txtAmount.Text == string.Empty)
            //{
            //    objsd.SDBGAmount = "0";
            //}
            //else
            //{
            //    objsd.SDBGAmount = txtAmount.Text;
            //}


            if (objsd.SDBG_Save() == "Data Saved Successfully")
            {
                objsd.SDBGDetails_Delete(objsd.SDBGId);
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    objsd.SDBGDetNo = gvrow.Cells[1].Text;
                    objsd.SDBGDetDDNo = gvrow.Cells[2].Text;
                    objsd.SDBGDetDated = gvrow.Cells[3].Text;
                    objsd.SDBGDetAmount = gvrow.Cells[4].Text;
                    objsd.SDBGDetBank = gvrow.Cells[5].Text;
                    objsd.SDBGDetDueDate = gvrow.Cells[6].Text;
                    objsd.SDBGDetRemarks = gvrow.Cells[7].Text;

                    objsd.SDBGDetails_Save();
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
            gvPreviousPayments.DataBind();

            gvSdDetails.DataBind();
            tblDepositDetails.Visible = false;

            SM.ClearControls(this);
            SM.Dispose();
        }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please Enter Feilds");
        //}

    }
    #endregion

    #region SDBG Update
    private void SDBGUpdate()
    {

        try
        {
            SM.SDBG objsd = new SM.SDBG();
            SM.BeginTransaction();
            objsd.CustId = lblCUSTIdHidden.Text;
            objsd.SDBGId = gvSdDetails.SelectedRow.Cells[0].Text;
            objsd.SDBGNo = txtSDNo.Text;
            objsd.SDBGDate = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
            //objsd.CustId = txtCustomerName.Text;
            objsd.EnqId = ddlTenderNo.SelectedItem.Value;
            objsd.SDBGStatementOf = ddlStatementOf.SelectedItem.Value;
            objsd.SoId = ddlSOPNo.SelectedItem.Value;
            //objsd.SDBGDdNO = txtDDNo.Text;
            //objsd.SDBGDdDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
            //objsd.SDBGDdNO = txtDDNo.Text;
            ////objsd.SDBGAmount = txtAmount.Text;
            //objsd.SDBGBank = txtBank.Text;
            //objsd.SDBGDueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            //objsd.SDBGRemarks = txtRemarks.Text;

            //if (txtAmount.Text == string.Empty)
            //{
            //    objsd.SDBGAmount = "0";
            //}
            //else
            //{
            //    objsd.SDBGAmount = txtAmount.Text;
            //}


            if (objsd.SDBG_Update() == "Data Updated Successfully")
            {

                objsd.SDBGDetails_Delete(objsd.SDBGId);
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    objsd.SDBGDetNo = gvrow.Cells[1].Text;
                    objsd.SDBGDetDDNo = gvrow.Cells[2].Text;
                    objsd.SDBGDetDated = gvrow.Cells[3].Text;
                    objsd.SDBGDetAmount = gvrow.Cells[4].Text;
                    objsd.SDBGDetBank = gvrow.Cells[5].Text;
                    objsd.SDBGDetDueDate = gvrow.Cells[6].Text;
                    objsd.SDBGDetRemarks = gvrow.Cells[7].Text;

                    objsd.SDBGDetails_Save();
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
            gvPreviousPayments.DataBind();
            gvSdDetails.DataBind();
            tblDepositDetails.Visible = false;

            SM.ClearControls(this);
            SM.Dispose();
        }


    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        String Temp_SOID = String.Empty;
        tblDepositDetails.Visible = true;

        if (gvSdDetails.SelectedIndex > -1)
        {
            try
            {




                SM.SDBG objsd = new SM.SDBG();
                SM.BeginTransaction();
                if (objsd.SDBG_Select(gvSdDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblDepositDetails.Visible = true;
                    tblsdDetils.Visible = true;
                    lblCUSTIdHidden.Text = objsd.CustId;
                    txtSDNo.Text = objsd.SDBGNo;
                    txtDate.Text = objsd.SDBGDate;
                    //txtCustomerName.Text = objsd.CustId;

                    ddlTenderNo.SelectedValue = objsd.EnqId;
                    Temp_SOID = objsd.SoId;

                    salesOrder_Fill();

                    //txtDDNo.Text = objsd.SDBGDdNO;
                    //txtDDDate.Text = objsd.SDBGDdDate;
                    //txtDDNo.Text = objsd.SDBGDdNO;
                    //txtAmount.Text = objsd.SDBGAmount;
                    //txtBank.Text = objsd.SDBGBank;
                    //txtDueDate.Text= objsd.SDBGDueDate;
                    //txtRemarks.Text = objsd.SDBGRemarks;
                    ddlStatementOf.SelectedValue = objsd.SDBGStatementOf;


                    objsd.SDBGDetails_Select(gvSdDetails.SelectedRow.Cells[0].Text, gvPreviousPayments);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {


                ddlTenderNo_SelectedIndexChanged(sender, e);
                ddlSOPNo.SelectedValue = Temp_SOID;
                ddlSOPNo_SelectedIndexChanged1(sender, e);
                SM.Dispose();

            }

        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSdDetails.SelectedIndex > -1)
        {
            try
            {
                SM.SDBG objsd = new SM.SDBG();

                MessageBox.Show(this, objsd.SDBG_Delete(gvSdDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvSdDetails.DataBind();
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
        gvPreviousPayments.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblDepositDetails.Visible = false;

    }
    #endregion

    #region gvClaimForm_RowDataBound
    protected void gvSdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "SDBG DATE")
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
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvSdDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvSdDetails.DataBind();
    }
    #endregion

    #region txtCustomerName_TextChanged
    //protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    //{
    //    UnitName_Fill();


    //    try
    //    {

    //        SM.CustomerMaster objcust = new SM.CustomerMaster();
    //        if (objcust.CustomerMaster_Select(txtUnitName.Text) > 0)
    //        {
    //            txtUnitName.Text = objcust.ContactPerson;

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
    #endregion

    #region txtUnitName_TextChanged
    //protected void txtUnitName_TextChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        SM.CustomerMaster objCustUnits = new SM.CustomerMaster();
    //        if (objCustUnits.CustomerMasterUnitsDetailsEnquiry_Select(ddlUnitName.SelectedItem.Value) > 0)
    //        {

    //            txtUnitAddress.Text = objCustUnits.Address;
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
    #endregion

    #region ddlTenderNo_SelectedIndexChanged
    protected void ddlTenderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.SalesEnquiry objsalesenq = new SM.SalesEnquiry();
            if (objsalesenq.SalesEnquiry_Select(ddlTenderNo.SelectedItem.Value) > 0)
            {
                txtTenderDate.Text = objsalesenq.EnqDate;
                salesOrder_Fill();
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

    #region ddlSOPNo_SelectedIndexChanged
    protected void ddlSOPNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {


            SM.SalesOrder objSO = new SM.SalesOrder();
            //if (objSO.SalesOrderItem_Select(ddlSOPNo.SelectedItem.Value) > 0)
            if (objSO.SalesOrderItem_Select(ddlTenderNo.SelectedItem.Text) > 0)
            {
                txtSOPDate.Text = objSO.SODate;



                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objSO.CustId) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;
                    lblCUSTIdHidden.Text = objSMCustomer.CustId;
                    txtUnitName.Text = objSMCustomer.CustUnitName;
                    txtUnitAddress.Text = objSMCustomer.Address;
                }
                else if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;

                    txtUnitName.Text = "--";
                    txtUnitAddress.Text = "--";
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
            SM.Dispose();
        }

    }
    #endregion

    #region gvPreviousPayments_RowDataBound
    protected void gvPreviousPayments_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");

        }


    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtSecurityNo.Text = string.Empty;
        txtDd.Text = string.Empty;
        txtDDate.Text = string.Empty;
        txtAmt.Text = string.Empty;
        txtBankDetails.Text = string.Empty;
        txtDue.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtSecurityNo.Text = string.Empty;


        gvPreviousPayments.SelectedIndex = -1;
    }
    #endregion

    #region Button Add
    protected void btnAdding_Click(object sender, EventArgs e)
    {


        DataTable SdbgProducts = new DataTable();
        DataColumn col = new DataColumn();
        //col = new DataColumn("StatementOf");
        //SdbgProducts.Columns.Add(col);
        col = new DataColumn("SDNumber");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("DDNo");
        SdbgProducts.Columns.Add(col);
        //col = new DataColumn("UOM");
        //ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("DDDate");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("Bank");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("DueDate");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SdbgProducts.Columns.Add(col);

        if (gvPreviousPayments.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
            {
                if (gvPreviousPayments.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvPreviousPayments.SelectedRow.RowIndex)
                    {
                        DataRow dr = SdbgProducts.NewRow();
                        //dr["StatementOf"] = ddlDetails.SelectedItem.Value;
                        dr["SDNumber"] = txtSecurityNo.Text;
                        dr["DDNo"] = txtDd.Text;
                        dr["DDDate"] = txtDDate.Text;
                        dr["Amount"] = txtAmt.Text;
                        dr["Bank"] = txtBankDetails.Text;
                        dr["DueDate"] = txtDue.Text;

                        dr["Remarks"] = txtRemark.Text;

                        SdbgProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SdbgProducts.NewRow();
                        dr["SDNumber"] = gvrow.Cells[1].Text;
                        //dr["ItemType"] = gvrow.Cells[3].Text;
                        dr["DDNo"] = gvrow.Cells[2].Text;
                        //dr["UOM"] = gvrow.Cells[5].Text;
                        dr["DDDate"] = gvrow.Cells[3].Text;
                        dr["Amount"] = gvrow.Cells[4].Text;
                        dr["Bank"] = gvrow.Cells[5].Text;
                        dr["DueDate"] = gvrow.Cells[6].Text;
                        dr["Remarks"] = gvrow.Cells[7].Text;
                        //dr["ItemTypeId"] = gvrow.Cells[12].Text;
                        SdbgProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SdbgProducts.NewRow();
                    dr["SDNumber"] = gvrow.Cells[1].Text;
                    //dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["DDNo"] = gvrow.Cells[2].Text;
                    //dr["UOM"] = gvrow.Cells[5].Text;
                    dr["DDDate"] = gvrow.Cells[3].Text;
                    dr["Amount"] = gvrow.Cells[4].Text;
                    dr["Bank"] = gvrow.Cells[5].Text;
                    dr["DueDate"] = gvrow.Cells[6].Text;
                    dr["Remarks"] = gvrow.Cells[7].Text;
                    //dr["ItemTypeId"] = gvrow.Cells[12].Text;
                    SdbgProducts.Rows.Add(dr);
                }
            }
        }

        if (gvPreviousPayments.Rows.Count > 0)
        {
            if (gvPreviousPayments.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
                {
                    if (gvrow.Cells[0].Text == txtSecurityNo.Text)
                    {
                        gvPreviousPayments.DataSource = SdbgProducts;
                        gvPreviousPayments.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvPreviousPayments.SelectedIndex == -1)
        {
            DataRow drnew = SdbgProducts.NewRow();
            drnew["SDNumber"] = txtSecurityNo.Text;
            drnew["DDNo"] = txtDd.Text;
            drnew["DDDate"] = txtDDate.Text;
            drnew["Amount"] = txtAmt.Text;
            drnew["Bank"] = txtBankDetails.Text;
            drnew["DueDate"] = txtDue.Text;

            drnew["Remarks"] = txtRemark.Text;
            SdbgProducts.Rows.Add(drnew);
        }
        gvPreviousPayments.DataSource = SdbgProducts;
        gvPreviousPayments.DataBind();
        gvPreviousPayments.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);



    }
    #endregion

    #region gvPreviousPayments Items Row Deleting
    protected void gvPreviousPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string x1 = gvPreviousPayments.Rows[e.RowIndex].Cells[1].Text;
        DataTable SdbgProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("SDNumber");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("DDNo");
        SdbgProducts.Columns.Add(col);
        //col = new DataColumn("UOM");
        //ClaimFormProducts.Columns.Add(col);
        col = new DataColumn("DDDate");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("Bank");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("DueDate");
        SdbgProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        SdbgProducts.Columns.Add(col);
        if (gvPreviousPayments.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvPreviousPayments.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SdbgProducts.NewRow();
                    dr["SDNumber"] = gvrow.Cells[1].Text;
                    //dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["DDNo"] = gvrow.Cells[2].Text;
                    //dr["UOM"] = gvrow.Cells[5].Text;
                    dr["DDDate"] = gvrow.Cells[3].Text;
                    dr["Amount"] = gvrow.Cells[4].Text;
                    dr["Bank"] = gvrow.Cells[5].Text;
                    dr["DueDate"] = gvrow.Cells[6].Text;
                    dr["Remarks"] = gvrow.Cells[7].Text;
                    SdbgProducts.Rows.Add(dr);
                }
            }
        }
        gvPreviousPayments.DataSource = SdbgProducts;
        gvPreviousPayments.DataBind();
    }
    #endregion

    protected void ddlStatementOf_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSecurityNo.Text = SM.SDBG.SDBGDetails_AutoGenCode();

        if (ddlStatementOf.SelectedItem.Text == "Security Deposit")
        {
            tblsdDetils.Visible = true;
            lblSDBgNo.Visible = true;
            txtSecurityNo.Visible = true;
            lblBGNo.Visible = false;
        }
        else if (ddlStatementOf.SelectedItem.Text == "Bank Guarantee")
        {
            tblsdDetils.Visible = true;
            lblSDBgNo.Visible = false;
            txtSecurityNo.Visible = true;
            lblBGNo.Visible = true;
        }
        else
        {
            tblsdDetils.Visible = false;
        }
    }
}
 
