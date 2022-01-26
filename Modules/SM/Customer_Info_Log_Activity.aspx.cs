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
using YantraDAL;
using System.Data.SqlClient;
using vllib;

public partial class Modules_SM_Customer_Info_Log_Activity : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
            txtInvoiceNo.Text = SM.SalesOrder.Amendment_AutoGenCode();
            txtReturnNo.Text = Inventory.SalesReturn.SalesReturnNote_AutoGenCode();
            txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtReturnDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCPID.Text = cp.getPresentCompanySessionValue();
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomerName);
            gvProformaInvoice.DataBind();
            EmployeeMaster_Fill();
            lblReturnCPID.Text = cp.getPresentCompanySessionValue();
            CustomerName_Fill();
        }
    }
    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlReturnCustName );
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
            HR.EmployeeMaster.EmployeeMaster_SelectSales12(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlRecivedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlAuthorisedBy);
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
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
    }
    protected void btnReturnCustSearch_Click(object sender, EventArgs e)
    {
        
        if (txtCusSearch.Text != "")
        {
            ddlReturnCustName.DataSourceID = "SqlDataSource3";
            ddlReturnCustName.DataTextField = "CUST_NAME";
            ddlReturnCustName.DataValueField = "CUST_ID";
            ddlReturnCustName.DataBind();
            ddlReturnCustName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource1";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void ddlReturnCustName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DCNo_Fill();
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlReturnCustName.SelectedValue) > 0)
        {
            //ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
            //txtCustomerName.Text = objSMCustomer.CustName;
            txtCustAdd.Text = objSMCustomer.Address;
            txtCustEmail.Text = objSMCustomer.Email;
            txtRegn.Text = objSMCustomer.RegName;
            txtCustPhn.Text = objSMCustomer.Phone;
            txtCustMbl.Text = objSMCustomer.Mobile;
        }
    }
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesOrder_Fill();
        //SM.CustomerMaster.CustomerUnits_Select(ddlunitname, ddlCustomerName.SelectedItem.Value);

        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
        {
            //ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
            //txtCustomerName.Text = objSMCustomer.CustName;
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
        }
    }
    #region DC Fill
    private void DCNo_Fill()
    {
        try
        {
            Inventory.Delivery.DeliveryChallanApprovedOnDC_SelectSO(ddlDCNo, ddlReturnCustName.SelectedValue);
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
    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlSalesOrderNo, ddlCustomerName.SelectedValue);
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
    protected void ddlDCNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Inventory.Delivery objdc = new Inventory.Delivery();
        if (objdc.DeliveryCust_Select(ddlDCNo.SelectedItem.Value) > 0)
        {
            txtDelAdd.Text = objdc.DelAdd;
            txtBillingAdd.Text = objdc.BillAdd;
            txtDCDt.Text = objdc.DCDate;
            objdc.DeliveryDetails_SelectInvoiceExtraItems(ddlDCNo.SelectedItem.Value, gvExtraItems);
        }
    }
    #region SalesOrder No Selected Index Changed
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.SalesOrder objSM = new SM.SalesOrder();
        if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
        {
            txtDeliveryAdd.Text = objSM.ConsignmentTo;
            txtBillAdd.Text = objSM.InvoiceTo;
            txtSalesOrderDate.Text = objSM.SODate;
            objSM.SalesOrderDetails_Amendment_select(ddlSalesOrderNo.SelectedItem.Value, gvItemPriceUpdate);
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            AmendmentSave();
        }
        else if (btnSave.Text == "Update")
        {
            AmendmentUpdate();
        }
       
        
        
    }

    private void AmendmentUpdate()
    {
        try
        {
            SM.SalesOrder objSM = new SM.SalesOrder();
            objSM.AmeId = gvProformaInvoice.SelectedRow.Cells[0].Text;
            objSM.AmeDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
            objSM.AmeNo = txtInvoiceNo.Text;
            objSM.SOId = ddlSalesOrderNo.SelectedItem.Value;
            objSM.SOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSM.CustId = ddlCustomerName.SelectedItem.Value;
            objSM.CpId = lblCPID.Text;
            if (objSM.Amendment_Update() == "Data Updated Successfully")
            {
                objSM.Amendment_DetDelete(objSM.AmeId);
                foreach (GridViewRow gvrow in gvItemPriceUpdate.Rows)
                {
                    TextBox RepModelNo = gvrow.FindControl("txtNewModelNo") as TextBox;
                    TextBox Reason = gvrow.FindControl("txtReason") as TextBox;
                    TextBox RepQty = gvrow.FindControl("txtNewQty") as TextBox;
                    TextBox RepPrice = gvrow.FindControl("txtNewPrice") as TextBox;
                    Label RepAmt = gvrow.FindControl("lblNewAmt") as Label;
                    Label RepGST = gvrow.FindControl("lblNewGst") as Label;
                    if (RepModelNo.Text != "" && RepQty.Text != "" && RepPrice.Text != "")
                    {
                        objSM.SODetId = gvrow.Cells[0].Text;
                        objSM.SOItemCode = gvrow.Cells[1].Text;
                        objSM.emodelno = gvrow.Cells[2].Text;
                        objSM.eqty = gvrow.Cells[4].Text;
                        objSM.erate = gvrow.Cells[5].Text;
                        objSM.eamt = gvrow.Cells[6].Text;
                        objSM.SOModelNo = RepModelNo.Text;
                        objSM.SODetSpec = Reason.Text;
                        objSM.SODetQty = RepQty.Text;
                        objSM.SODetPrice = RepPrice.Text;
                        objSM.SoDetAmt = RepAmt.Text;
                        objSM.SoDetGST = RepGST.Text;
                        objSM.OrderPayed = objSM.OrderArrived = objSM.Blocked = objSM.Blocked = objSM.IndentQty = objSM.Orderdqty = objSM.OrderShipped = objSM.BalanceQty = "";
                        //if (gvrow.Cells[14].Text == " " || gvrow.Cells[14].Text == "&nbsp;") { objSM.Blocked = "0"; } else { objSM.Blocked = gvrow.Cells[14].Text; }
                        //if (gvrow.Cells[15].Text == " " || gvrow.Cells[15].Text == "&nbsp;") { objSM.IndentQty = "0"; } else { objSM.IndentQty = gvrow.Cells[15].Text; }
                        //if (gvrow.Cells[16].Text == " " || gvrow.Cells[16].Text == "&nbsp;") { objSM.Orderdqty = "0"; } else { objSM.Orderdqty = gvrow.Cells[16].Text; }
                        //if (gvrow.Cells[17].Text == " " || gvrow.Cells[17].Text == "&nbsp;") { objSM.OrderShipped = "0"; } else { objSM.OrderShipped = gvrow.Cells[17].Text; }
                        //if (gvrow.Cells[18].Text == " " || gvrow.Cells[18].Text == "&nbsp;") { objSM.BalanceQty = "0"; } else { objSM.BalanceQty = gvrow.Cells[18].Text; }

                        objSM.Amendment_Det_Save();
                    }
                    else
                    {
                        //MessageBox.Show(this, "Please do enter Amendment Details Properly");
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        tbldet.Visible = false;
        gvItemPriceUpdate.DataBind();
    }
    private void AmendmentSave()
    {
        if (gvItemPriceUpdate.Rows.Count > 0)
        {
            try
            {
                SM.SalesOrder objSM = new SM.SalesOrder();
                objSM.SONo = txtInvoiceNo.Text;
                objSM.SODate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objSM.SOPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.SODetId = ddlSalesOrderNo.SelectedItem.Value;
                objSM.SOCUSTID = ddlCustomerName.SelectedItem.Value;
                objSM.SOApprovedBy = objSM.POInchaarge = objSM.StoreIncharge = "0";
                objSM.CpId = lblCPID.Text;
                

                if (objSM.Amendment_Save() == "Data Saved Successfully")
                {
                    foreach (GridViewRow gvrow in gvItemPriceUpdate.Rows)
                    {
                        TextBox RepModelNo = gvrow.FindControl("txtNewModelNo") as TextBox;
                        TextBox Reason = gvrow.FindControl("txtReason") as TextBox;
                        TextBox RepQty = gvrow.FindControl("txtNewQty") as TextBox;
                        TextBox RepPrice = gvrow.FindControl("txtNewPrice") as TextBox;
                        Label RepAmt = gvrow.FindControl("lblNewAmt") as Label;
                        Label RepGST = gvrow.FindControl("lblNewGst") as Label;
                        if (RepModelNo.Text != "" && RepQty.Text != "" && RepPrice.Text != "")
                        {
                            objSM.SODetId = gvrow.Cells[0].Text;
                            objSM.SOItemCode = gvrow.Cells[1].Text;
                            objSM.emodelno = gvrow.Cells[2].Text;
                            objSM.eqty  = gvrow.Cells[4].Text;
                            objSM.erate  = gvrow.Cells[5].Text;
                            objSM.eamt  = gvrow.Cells[6].Text;
                            objSM.SOModelNo = RepModelNo.Text;
                            objSM.SODetSpec = Reason.Text;
                            objSM.SODetQty = RepQty.Text;
                            objSM.SODetPrice = RepPrice.Text;
                            objSM.SoDetAmt = RepAmt.Text;
                            objSM.SoDetGST = RepGST.Text;
                            objSM.OrderPayed = objSM.OrderArrived = objSM.Blocked = objSM.Blocked = objSM.IndentQty = objSM.Orderdqty = objSM.OrderShipped = objSM.BalanceQty = "";
                           //if (gvrow.Cells[14].Text == " " || gvrow.Cells[14].Text == "&nbsp;") { objSM.Blocked = "0"; } else { objSM.Blocked = gvrow.Cells[14].Text; }
                           //if (gvrow.Cells[15].Text == " " || gvrow.Cells[15].Text == "&nbsp;") { objSM.IndentQty = "0"; } else { objSM.IndentQty = gvrow.Cells[15].Text; }
                           //if (gvrow.Cells[16].Text == " " || gvrow.Cells[16].Text == "&nbsp;") { objSM.Orderdqty = "0"; } else { objSM.Orderdqty = gvrow.Cells[16].Text; }
                           //if (gvrow.Cells[17].Text == " " || gvrow.Cells[17].Text == "&nbsp;") { objSM.OrderShipped = "0"; } else { objSM.OrderShipped = gvrow.Cells[17].Text; }
                           //if (gvrow.Cells[18].Text == " " || gvrow.Cells[18].Text == "&nbsp;") { objSM.BalanceQty = "0"; } else { objSM.BalanceQty = gvrow.Cells[18].Text; }

                            objSM.Amendment_Det_Save();
                        }
                        else
                        {
                            //MessageBox.Show(this, "Please do enter Amendment Details Properly");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        tbldet.Visible = false;
        gvItemPriceUpdate.DataBind();

    }
    protected void gvItemPriceUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox qty = (TextBox)row.FindControl("txtNewQty");
            TextBox price = (TextBox)row.FindControl("txtNewPrice");
            Label lblAmt = (Label)row.FindControl("lblNewAmt");
            Label lblGST = (Label)row.FindControl("lblNewGst");
            if (qty.Text == "") { qty.Text = "0"; }
            if (price.Text == "") { price.Text = "0"; }
            lblAmt.Text = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(price.Text)).ToString();
            lblGST.Text = (Convert.ToDecimal(lblAmt.Text) * Convert.ToDecimal(18) / 100).ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[7].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
        }
    }
    protected void txtNewQty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvItemPriceUpdate.Rows)
        {
            TextBox qty = (TextBox)row.FindControl("txtNewQty");
            TextBox price = (TextBox)row.FindControl("txtNewPrice");
            Label lblAmt = (Label)row.FindControl("lblNewAmt");
            Label lblGST = (Label)row.FindControl("lblNewGst");
            if (price.Text != "" && qty.Text != "")
            {
                lblAmt.Text = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(price.Text)).ToString();
                lblGST.Text = (Convert.ToDecimal(lblAmt.Text) * Convert.ToDecimal(18) / 100).ToString();
            }
        }
    }
    protected void txtNewPrice_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvItemPriceUpdate.Rows)
        {
            TextBox qty = (TextBox)row.FindControl("txtNewQty");
            TextBox price = (TextBox)row.FindControl("txtNewPrice");
            Label lblAmt = (Label)row.FindControl("lblNewAmt");
            Label lblGST = (Label)row.FindControl("lblNewGst");
            if (price.Text != "" && qty.Text != "")
            {
                lblAmt.Text = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(price.Text)).ToString();
                lblGST.Text = (Convert.ToDecimal(lblAmt.Text) * Convert.ToDecimal(18) / 100).ToString();
            }
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemPriceUpdate.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvItemPriceUpdate.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvItemPriceUpdate.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "PI Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
    #endregion
    #region ddlSymbols_SelectedIndexChanged
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion
    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvItemPriceUpdate.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "PI Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvItemPriceUpdate.DataBind();
    }
    #endregion

    protected void lbtnPINO_Click(object sender, EventArgs e)
    {
        
        LinkButton lbtnPINO;
        lbtnPINO = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPINO.Parent.Parent;
        gvProformaInvoice.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        try
        {
            SM.SalesOrder objSM = new SM.SalesOrder();
            if (objSM.AmendmentDetails_Select(gvProformaInvoice.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                tbldet.Visible = true;
                txtInvoiceNo.Text = objSM.AmeNo;
                txtInvoiceDate.Text = objSM.AmeDate;
                ddlCustomerName.SelectedValue = objSM.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                ddlSalesOrderNo.SelectedValue = objSM.SOId;
                ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gvProformaInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemPriceUpdate.PageIndex = e.NewPageIndex;

    }
    protected void gvProformaInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        tbldet.Visible = true;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvProformaInvoice.SelectedIndex > -1)
        {
            try
            {
                SM.SalesOrder objSM = new SM.SalesOrder();
                MessageBox.Show(this, objSM.Amendment_Delete(gvProformaInvoice.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvProformaInvoice.DataBind();
                tbldet.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvProformaInvoice.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Amendmnet&Ameid=" + gvProformaInvoice.SelectedRow.Cells[0].Text + " ";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select a Record");
        }
    }
    protected void lnkPOAmen_Click(object sender, EventArgs e)
    {
        POAmendment.Visible = true;
        ReturnNote.Visible = false;
    }
    protected void lnkSalesReturn_Click(object sender, EventArgs e)
    {
        POAmendment.Visible = false;
        ReturnNote.Visible = true;
    }
    protected void ddlReturnNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlReturnSearch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnReturnSearchGo_Click(object sender, EventArgs e)
    {

    }
    protected void btnReturnNew_Click(object sender, EventArgs e)
    {
        tblreturnDet.Visible = true;
    }
    protected void btnReturnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnReturnPrint_Click(object sender, EventArgs e)
    {
        if (gvSalesReturnDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ReturnNote&Srno=" + gvSalesReturnDetails.SelectedRow.Cells[0].Text + "";
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
    private DataTable AddRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ItemCode = '" + gvRow.Cells[2].Text + "'");
        if (dr.Length <= 0)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["DC No"] = gvRow.Cells[1].Text;
            dt.Rows[dt.Rows.Count - 1]["ItemCode"] = gvRow.Cells[2].Text;
            dt.Rows[dt.Rows.Count - 1]["ModelNo"] = gvRow.Cells[3].Text;
            dt.Rows[dt.Rows.Count - 1]["ItemName"] = gvRow.Cells[4].Text;
            dt.Rows[dt.Rows.Count - 1]["UOM"] = gvRow.Cells[5].Text;
            dt.Rows[dt.Rows.Count - 1]["Quantity"] = gvRow.Cells[6].Text;
            dt.Rows[dt.Rows.Count - 1]["Remarks"] = gvRow.Cells[7].Text;
            //dt.Rows[dt.Rows.Count - 1]["Areasq"] = gvRow.Cells[8].Text;
            //dt.Rows[dt.Rows.Count - 1]["Amount"] = gvRow.Cells[9].Text;
            //dt.Rows[dt.Rows.Count - 1]["SeriesID"] = gvRow.Cells[10].Text;
            //dt.Rows[dt.Rows.Count - 1]["Id"] = gvRow.Cells[10].Text;
            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable RemoveRow(GridViewRow gvRow, DataTable dt)
    {
        DataRow[] dr = dt.Select("ItemCode = '" + gvRow.Cells[2].Text + "'");
        if (dr.Length > 0)
        {
            dt.Rows.Remove(dr[0]);
            dt.AcceptChanges();
        }
        return dt;
    }
    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DC No");
        dt.Columns.Add("ItemCode");
        dt.Columns.Add("ModelNo");
        dt.Columns.Add("ItemName");
        dt.Columns.Add("UOM");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Remarks");
        //dt.Columns.Add("Areasq");
        //dt.Columns.Add("Amount");
        //dt.Columns.Add("SeriesID");
        //dt.Columns.Add("Id");
        dt.AcceptChanges();
        return dt;
    }
    private void GetData()
    {
        DataTable dt;
        if (ViewState["SelectedRecords"] != null)
            dt = (DataTable)ViewState["SelectedRecords"];
        else
            dt = CreateDataTable();
        CheckBox chkAll = (CheckBox)gvExtraItems.HeaderRow
                            .Cells[0].FindControl("chkAll");
        for (int i = 0; i < gvExtraItems.Rows.Count; i++)
        {
            if (chkAll.Checked)
            {
                dt = AddRow(gvExtraItems.Rows[i], dt);
            }
            else
            {
                CheckBox chk = (CheckBox)gvExtraItems.Rows[i]
                                .Cells[0].FindControl("chk");
                if (chk.Checked)
                {
                    dt = AddRow(gvExtraItems.Rows[i], dt);
                }
                else
                {
                    dt = RemoveRow(gvExtraItems.Rows[i], dt);
                }
            }
        }
        ViewState["SelectedRecords"] = dt;
    }
    
    private void SetData()
    {
        CheckBox chkAll = (CheckBox)gvExtraItems.HeaderRow.Cells[0].FindControl("chkAll");
        chkAll.Checked = true;
        if (ViewState["SelectedRecords"] != null)
        {
            DataTable dt = (DataTable)ViewState["SelectedRecords"];
            for (int i = 0; i < gvExtraItems.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvExtraItems.Rows[i].Cells[0].FindControl("chk");
                if (chk != null)
                {
                    DataRow[] dr = dt.Select("ItemCode = '" + gvExtraItems.Rows[i].Cells[2].Text + "'");
                    chk.Checked = dr.Length > 0;
                    if (!chk.Checked)
                    {
                        chkAll.Checked = false;
                    }
                }
            }
        }
    }
    private void BindSecondaryGrid()
    {
        DataTable dt = (DataTable)ViewState["SelectedRecords"];
        gvitems.DataSource = dt;
        gvitems.DataBind();
    }
    protected void CheckBox_CheckChanged(object sender, EventArgs e)
    {
        GetData();
        SetData();
        BindSecondaryGrid();
    }
    protected void btnReturnSave_Click(object sender, EventArgs e)
    {
        if (btnReturnSave.Text == "Save")
        {
            Return_Save();
            tblreturnDet.Visible = false;
        }
        else if (btnReturnSave.Text == "Update")
        {
            Return_Update();
            tblreturnDet.Visible = false;
        }
    }

    private void Return_Update()
    {

    }
    private void Return_Save()
    {
        if (gvitems.Rows.Count > 0)
        {
            try 
            {
                btnReturnSave.Enabled = false;
                Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
                objInventory.SRNo  = txtReturnNo.Text;
                objInventory.SRDate = Yantra.Classes.General.toMMDDYYYY(txtReturnDt.Text);
                objInventory.DCId = ddlDCNo.SelectedItem.Value;
                objInventory.CustId = ddlReturnCustName.SelectedItem.Value;
                objInventory.SRPreparedBy  = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SRApprovedBy  = ddlApprovedBy.SelectedItem.Value;
                objInventory.RecivedBy = ddlRecivedBy.SelectedItem.Value;
                objInventory.AuthorisedBy = ddlAuthorisedBy.SelectedItem.Value;
                objInventory.CPid  = lblReturnCPID.Text;
                objInventory.SRRemarks  = txtReason.Text;
                objInventory.SOId = "0";
                objInventory.SRGrossAmt = "0";
                objInventory.SRaftermonth = "0";
                if (objInventory.Return_Save() == "Data Saved Successfully")
                {
                    foreach (GridViewRow gvrow in gvitems.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[1].Text;
                        //objInventory.SRDetQty = gvrow.Cells[5].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                        objInventory.SRDetQty = Quantity.Text;
                        objInventory.SRDetRate = "0";
                        objInventory.SRDetVat = "0";
                        //objInventory.SRDetCst = gvrow.Cells[9].Text;
                        //objInventory.SRDetExcise = gvrow.Cells[10].Text;
                        objInventory.ReturnDet_Save();
                    }
                }
                
            }
            catch (Exception ex) { }
            finally
            {
                gvSalesReturnDetails.DataBind();

                lblReturnCPID.Visible = false;
                Inventory.ClearControls(this);
                // Inventory.Dispose();
            }
        }
    }
    protected void gvSalesReturnDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvSalesReturnDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false;
            //e.Row.Cells[12].Visible = false;

        }
    }
    protected void lbtnDCNo_Click(object sender, EventArgs e)
    {
        lbtnSalesReturnNo_Click(sender, e);
    }
    protected void lbtnSalesReturnNo_Click(object sender, EventArgs e)
    {
        tblreturnDet.Visible = false;
        LinkButton lbtnSalesInvoiceNo;
        lbtnSalesInvoiceNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesInvoiceNo.Parent.Parent;
        gvSalesReturnDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        try
        {
            Inventory.SalesReturn objInventory = new Inventory.SalesReturn();

            if (objInventory.ReturnNote_Select(gvSalesReturnDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblreturnDet.Visible = true;
                txtReturnNo.Text = objInventory.SRNo;
                txtReturnDt.Text = objInventory.SRDate;
                ddlReturnCustName.SelectedValue = objInventory.CustId;
                ddlReturnCustName_SelectedIndexChanged (sender,  e);
                ddlDCNo.SelectedValue = objInventory.DCId;
                ddlDCNo_SelectedIndexChanged(sender, e);
                ddlPreparedBy.SelectedValue = objInventory.SRPreparedBy;
                ddlApprovedBy.SelectedValue = objInventory.SRApprovedBy;
                ddlRecivedBy.SelectedValue = objInventory.RecivedBy;
                ddlAuthorisedBy.SelectedValue = objInventory.AuthorisedBy;
                txtReason.Text = objInventory.SRRemarks;
                objInventory.ReturnNoteDet_Select(gvSalesReturnDetails.SelectedRow.Cells[0].Text, gvitems);
                gvitems.DataBind();
            }
        }
        catch (Exception ex) { }
    }
   
}
 
