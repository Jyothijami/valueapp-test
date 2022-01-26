using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Modules_Inventory_ReserveItems : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            SalesOrderMaster_Fill();
            btnReserve.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnReserve, null) + ";");

        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvWorkOrderDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvWorkOrderDetails.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvWorkOrderDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "IO Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date")
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


    #region DropDownList Symbols Select Index Changed
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

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvWorkOrderDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
      
        if (ddlSearchBy.SelectedItem.Text == "IO Date" || ddlSearchBy.SelectedItem.Text == "Delivery Date")
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
        gvWorkOrderDetails.DataBind();
    }
    #endregion
    #region Sales Order Master Fill
    private void SalesOrderMaster_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_Select(ddlOrderAcceptance);
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
    
    protected void lbtnWorkOrderNo_Click(object sender, EventArgs e)
    {
        tblWorkOrderDetails.Visible = true;
        LinkButton lbtnQuoNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnQuoNo.Parent.Parent;
        gvWorkOrderDetails.SelectedIndex = Row.RowIndex;
        lblIOId.Text = gvWorkOrderDetails.SelectedRow.Cells[0].Text;
        SM.WorkOrder objWorkOrder = new SM.WorkOrder();
        if (objWorkOrder.WorkOrder_Select(lblIOId.Text) > 0)
        {
            ddlOrderAcceptance.SelectedValue = objWorkOrder.SOId;
            ddlOrderAcceptance_SelectedIndexChanged(sender, e);
        }       
        gvAvailQty.DataSource = null;
        gvAvailQty.DataBind();
        ddlLocation.SelectedIndex = 0;
    }
    protected void gvWorkOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #region Order Acceptance No Selected Index Changed
    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.SalesOrder objSO = new SM.SalesOrder();
            if (objSO.SalesOrder_Select(ddlOrderAcceptance.SelectedItem.Value) > 0)
            {
                txtOADate.Text = objSO.SODate;               
               
                    lblRespId.Text = objSO.SORespId;

                    
                objSO.SalesOrderDetails_Select(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);
               
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    lblCust_Id.Text = objSO.CustId;
                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text = objSMCustomer.CustUnitName;
                }
                else if (objSMCustomer.CustomerMaster_Select(objSO.CustId) > 0)
                {
                    lblCust_Id.Text = objSO.CustId;

                    txtCustName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    txtUnitName.Text = "--";

                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            SM.Dispose();
        }

    }
    #endregion
    protected void gvOrderAcceptanceItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAvailQty.DataSource = null;
        gvAvailQty.DataBind();
        foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
        {
            DataTable IndentApprovalProducts = new DataTable();

            DataColumn col = new DataColumn();

            col = new DataColumn("ItemCode");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("ModelNo");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("Color");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("Quantity");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("TtlQty");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("BlockQty");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("AvailQty");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("CustQty");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("Location");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("locId");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("colorId");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("qty");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("DeliveryDate");
            IndentApprovalProducts.Columns.Add(col);
            col = new DataColumn("DetId");
            IndentApprovalProducts.Columns.Add(col);
            if (gvAvailQty.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow1 in gvAvailQty.Rows)
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = gvrow1.Cells[0].Text;
                    dr["ModelNo"] = gvrow1.Cells[1].Text;
                    dr["Color"] = gvrow1.Cells[2].Text;
                    //dr["Quantity"] = gvrow1.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                    dr["Quantity"] = qty.Text;
                    dr["TtlQty"] = gvrow1.Cells[4].Text;
                    dr["BlockQty"] = gvrow1.Cells[5].Text;
                    //dr["AvailQty"] = qty.Text;
                    dr["AvailQty"] = gvrow1.Cells[6].Text;
                    dr["CustQty"] = gvrow1.Cells[7].Text;
                    dr["Location"] = gvrow1.Cells[8].Text;
                    dr["locId"] = gvrow1.Cells[9].Text;
                    dr["colorId"] = gvrow1.Cells[10].Text;
                    dr["qty"] = gvrow1.Cells[11].Text;
                    dr["DeliveryDate"] = gvrow1.Cells[12].Text;
                    dr["DetId"] = gvrow1.Cells[14].Text;

                    IndentApprovalProducts.Rows.Add(dr);
                }
            }
            lblItemCode.Text = gvrow.Cells[0].Text;
            lblColor.Text = gvrow.Cells[14].Text;

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.TtlStockAvailNew_select(lblItemCode.Text, lblColor.Text,ddlLocation.SelectedItem.Value) > 0)
            {
                lblTtlQty.Text = objMaster.TtlQuantity;
                lblBlockQty.Text = objMaster.BlockQuantity;
                lblAvaliableQty.Text = objMaster.AvaliableQuantity;
                lblLocation.Text = objMaster.locName;
                lbllocID.Text = objMaster.locId;
            }
            else
            {
                lblTtlQty.Text = "0";
                lblBlockQty.Text = "0";
                lblAvaliableQty.Text = "0";
                lblLocation.Text = ddlLocation.SelectedItem.Text;
                lbllocID.Text = ddlLocation.SelectedItem.Value;
            }
            if (objMaster.CustStockAvailNew_select(lblItemCode.Text, lblColor.Text, lblIOId.Text, lblCust_Id.Text) > 0)
            {
                lblCustQty.Text = objMaster.CustQty;
            }
            else
            {
                lblCustQty.Text = "0";
            }
            DataRow drnew = IndentApprovalProducts.NewRow();
            drnew["ItemCode"] = gvrow.Cells[0].Text;
            drnew["ModelNo"] = gvrow.Cells[1].Text;
            drnew["Color"] = gvrow.Cells[13].Text;
            //TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
           // drnew["Quantity"] = qty.Text;
            drnew["Quantity"] = gvrow.Cells[4].Text;

            if (lblTtlQty.Text == "")
            {
                drnew["TtlQty"] = "0";
            }
            else
            {
                drnew["TtlQty"] = lblTtlQty.Text;
            }
            if (lblBlockQty.Text == "")
            {
                drnew["BlockQty"] = "0";
            }
            else
            {
                drnew["BlockQty"] = lblBlockQty.Text;
            }
            if (lblAvaliableQty.Text == "")
            {
                drnew["AvailQty"] = "0";
            }
            else
            {
                drnew["AvailQty"] = lblAvaliableQty.Text;
            }
            if (lblCustQty.Text == "")
            {
                drnew["CustQty"] = "0";
            }
            else
            {
                drnew["CustQty"] = lblCustQty.Text;
            }
            drnew["Location"] = lblLocation.Text;
            drnew["locId"] = lbllocID.Text;
            drnew["colorId"] = gvrow.Cells[14].Text;
            drnew["qty"] = lblAvaliableQty.Text;
            drnew["DeliveryDate"] = gvrow.Cells[10].Text;
            drnew["DetId"] = gvrow.Cells[16].Text;

            IndentApprovalProducts.Rows.Add(drnew);
            gvAvailQty.DataSource = IndentApprovalProducts;
            gvAvailQty.DataBind();
        }
    }
    protected void btnReserve2_Click(object sender, EventArgs e)
    {
        try
        {
            int qty_Reserved = 0;
            int qty_Actual = 0;

            foreach (GridViewRow gvrow in gvAvailQty.Rows)
            {
                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {
                    TextBox reqQty = (TextBox)gvrow.FindControl("txtQuantity");
                    int qty = int.Parse(reqQty.Text);

                    qty_Actual = qty_Actual + qty;

                    int avlQty = Convert.ToInt32(gvrow.Cells[11].Text);
                    if (avlQty < qty)
                    {
                        MessageBox.Show(this, "Required Quantity is not Avaliable in Stock for Item  " + gvrow.Cells[1].Text + "");
                        return;
                    }
                    else
                    {
                        string Itemcode = gvrow.Cells[0].Text;
                        string ColorId = gvrow.Cells[10].Text;
                        string locId = gvrow.Cells[9].Text;

                        SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,cp_id,Quantity from [V_LocAvaliableItems] where Quantity>0 and ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " and [locid]=" + locId + "  order by whname  ", con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);


                        qty = int.Parse(reqQty.Text);
                        int qty2 = int.Parse(reqQty.Text);

                        string confirm = "";
                        
                        if (dt.Rows.Count > 0)
                        {
                            Masters.ItemPurchase obj = new Masters.ItemPurchase();

                            for (int i = 0; i < qty2; i++)
                            {

                                if (qty >= Convert.ToInt32(dt.Rows[i][3]))
                                {
                                    obj.Quantity = dt.Rows[i][3].ToString();
                                }

                                else if (qty < Convert.ToInt32(dt.Rows[i][3]))
                                {
                                    obj.Quantity = qty.ToString();
                                }

                                obj.itemcode = gvrow.Cells[0].Text;
                                
                                obj.ItemID = dt.Rows[i][0].ToString();
                                obj.whLocId = dt.Rows[i][1].ToString();
                                obj.Barcode = dt.Rows[i][0].ToString();
                                obj.companyid = dt.Rows[i][2].ToString();

                                // obj.companyid = lblCPID.Text;
                                obj.POID = lblIOId.Text;
                                obj.COLORID = gvrow.Cells[10].Text;
                                obj.status = "Blocked";
                                obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                                obj.CustomerId = lblCust_Id.Text;
                                
                                string msg =  obj.BlockNew_Save2();

                                if (msg == "Data Saved Successfully")
                                {
                                    qty = qty - Convert.ToInt32(dt.Rows[i][3]);

                                    qty_Reserved = qty_Reserved + Convert.ToInt32(obj.Quantity);
                                }
                                
                                if (qty <= 0)
                                {
                                    confirm = "Successful";
                                    break;
                                }
                            }
                        }

                        if (confirm == "Successful")
                        {
                            SM.SalesOrder objSM = new SM.SalesOrder();
                            objSM.SODetId = gvrow.Cells[14].Text;
                            // objSM.BalanceQty = qty1.Text;                   
                            objSM.BalanceQty = (qty2 + Convert.ToInt32(gvrow.Cells[7].Text)).ToString();
                            objSM.SalesOrderStatus_Update();
                        }

                    }
                }
            }
            if(qty_Reserved == qty_Actual)
            {
                MessageBox.Show(this, "Stock Reserved Successfully");
                tblWorkOrderDetails.Visible = false;
            }
            else
            {
                MessageBox.Show(this, qty_Reserved.ToString() + ", Stock got reserved instead of " + qty_Actual.ToString() + ", Please Re-check and confirm."); 

            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        
    }
    protected void btnReserve_Click(object sender, EventArgs e)
    {
        try
        {
            //foreach (GridViewRow gvrow in gvAvailQty.Rows)
            //{
            //    CheckBox chk;
            //    chk = (CheckBox)gvrow.FindControl("chk");
            //    if (chk.Checked == true)
            //    {                   
                        //TextBox qty1 = (TextBox)gvrow.FindControl("txtQuantity");
                        //int qty = int.Parse(qty1.Text);

                        //int avlQty = Convert.ToInt32(gvrow.Cells[11].Text);
                        //if (avlQty < qty)
                        //{
                        //    MessageBox.Show(this, "Required Quantity is not Avaliable in Stock for Item  " + gvrow.Cells[1].Text + "");
                        //    return;
                        //}
                        
            //    }
            //}

            foreach (GridViewRow gvrow in gvAvailQty.Rows)
            {
                CheckBox chk;
                chk = (CheckBox)gvrow.FindControl("chk");
                if (chk.Checked == true)
                {
                    TextBox qty1 = (TextBox)gvrow.FindControl("txtQuantity");
                    int qty = int.Parse(qty1.Text);

                    int avlQty = Convert.ToInt32(gvrow.Cells[11].Text);
                    if (avlQty < qty)
                    {
                        MessageBox.Show(this, "Required Quantity is not Avaliable in Stock for Item  " + gvrow.Cells[1].Text + "");
                        return;
                    }
                    else
                    {
                        #region Stock Reserve

                        string Itemcode = gvrow.Cells[0].Text;
                        string ColorId = gvrow.Cells[10].Text;
                        string locId = gvrow.Cells[9].Text;

                        //  SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,cp_id from dbo.INWARD where ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " and Item_ID not in(select Item_ID from BlOCK where ITEM_CODE=" + Itemcode + ") and Item_ID not in(select Item_ID from OUTWARD where ITEM_CODE=" + Itemcode + ")", con);
                        SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,cp_id,Quantity from [V_LocAvaliableItems] where Quantity>0 and ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " and [locid]=" + locId + "  order by whname  ", con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        Masters.ItemPurchase obj = new Masters.ItemPurchase();
                        qty1 = (TextBox)gvrow.FindControl("txtQuantity");
                        qty = int.Parse(qty1.Text);
                        int qty2 = int.Parse(qty1.Text);

                        //objWorkOrder.WODetQty = gvrow.Cells[5].Text;

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < qty2; i++)
                            {

                                if (qty >= Convert.ToInt32(dt.Rows[i][3]))
                                {
                                    obj.Quantity = dt.Rows[i][3].ToString();
                                }

                                else if (qty < Convert.ToInt32(dt.Rows[i][3]))
                                {
                                    obj.Quantity = qty.ToString();
                                }
                                obj.itemcode = gvrow.Cells[0].Text;
                                obj.ItemID = dt.Rows[i][0].ToString();
                                obj.whLocId = dt.Rows[i][1].ToString();
                                obj.Barcode = dt.Rows[i][0].ToString();

                                // obj.companyid = lblCPID.Text;
                                obj.companyid = dt.Rows[i][2].ToString();
                                obj.POID = lblIOId.Text;
                                obj.COLORID = gvrow.Cells[10].Text;
                                obj.status = "Blocked";
                                obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[12].Text);
                                obj.CustomerId = lblCust_Id.Text;
                                obj.BlockNew_Save2();
                                qty = qty - Convert.ToInt32(dt.Rows[i][3]);
                                if (qty <= 0)
                                {
                                    break;
                                }
                            }
                        }
                        SM.SalesOrder objSM = new SM.SalesOrder();
                        objSM.SODetId = gvrow.Cells[14].Text;
                        // objSM.BalanceQty = qty1.Text;                   
                        objSM.BalanceQty = (qty2 + Convert.ToInt32(gvrow.Cells[7].Text)).ToString();
                        objSM.SalesOrderStatus_Update();

                        #endregion
                    }
                    
                }
            }
            MessageBox.Show(this, "Stock Reserved Successfully");
            tblWorkOrderDetails.Visible = false;
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           // tblWorkOrderDetails.Visible = false;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblWorkOrderDetails.Visible = false;
    }
    protected void gvAvailQty_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[14].Visible = false;
        }
    }
    
}
 
