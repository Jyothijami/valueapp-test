using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_SM_Quot_Preview : System.Web.UI.Page
{
    decimal TotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            txtspldiscount.Text = "0";
            txtspldiscount.Attributes.Add("onkeyup", "javascript:summarycalc();");

            string Qid = Request.QueryString["Cid"].ToString();
            CustomerMaster_Fill();
            EmployeeMaster_Fill();
            if (Qid != "0")
            {

                SM.SalesQuotation objSM = new SM.SalesQuotation();

                if (objSM.SalesQuotation_Select(Qid) > 0)
                {
                    txtQuotationNo.Text = objSM.QuotNo;
                    txtQuotationDate.Text = objSM.QuotDate;
                    ddlCustomer.SelectedValue = objSM.CustId;
                    ddlUnitName.SelectedValue = objSM.CustUnitId;
                    ddlResponsiblePerson.SelectedValue = objSM.QuotRespId;
                    ddlSalesPerson.SelectedValue = objSM.QuotSalespId;
                    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
                    {
                        //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                        //txtRegion.Text = objSMCustomer.RegName;
                        //txtIndustryType.Text = objSMCustomer.IndType;
                        txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                        //    txtEmail.Text = objSMCustomer.Email;
                        //    txtPhoneNo.Text = objSMCustomer.Phone;
                        txtContactPerson.Text = objSMCustomer.Phone;
                    }
                }
                objSM.SalesQuotationDetails_SelectPreview(Qid, gvQuotationItems);

            }
        }
    }
    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName2(ddlCustomer);
            SM.CustomerMaster.CustomerNickName_SelectCredit(ddlUnitName);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //SM.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            //HR.EmployeeMaster.EmployeeMasterStatus_Select(ddlResponsiblePerson);
            //HR.EmployeeMaster.EmployeeMasterStatus_Select(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlResponsiblePerson);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesPerson);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlCheckedBy);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //HR.Dispose();
        }
    }
    #endregion
    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (btnSave.Enabled == false)
        //{
            if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
            //if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible  = e.Row.Cells[18].Visible = e.Row.Cells[19].Visible = false;

            }
        //}
        GridViewRow gvr = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Item Details list?');");
            TextBox rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            e.Row.Cells[9].Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(Qty.Text)).ToString();
            //e.Row.Cells[9].Text = (Convert.ToDecimal(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[6].Text)).ToString();
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            e.Row.Cells[13].Text = ((Convert.ToDecimal(e.Row.Cells[11].Text) * Convert.ToDecimal(e.Row.Cells[12].Text)) / 100).ToString();
            disc.Text = e.Row.Cells[10].Text;
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total Amount:";
            e.Row.Cells[11].Text = TotalAmount.ToString();
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            txttotal.Text = txtsubtotal.Text = GrossAmountCalc().ToString();
            if (txtspldiscount.Text == "") { txtspldiscount.Text = "0"; }
            if (txtspldiscount.Text != "0")
            { txtsubtotal.Text = (Convert.ToDecimal(txttotal.Text) - (Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(txtspldiscount.Text)) / 100).ToString(); }
            txtsummaryvat.Text = ((Convert.ToDecimal(txtsubtotal.Text) * Convert.ToDecimal(18)) / 100).ToString();
            //txtsummaryCst.Text = ((Convert.ToDecimal(txtsubtotal.Text) * Convert.ToDecimal(2)) / 100).ToString();
            txtsummaryGtoalvat.Text = (Convert.ToDecimal(txtsubtotal.Text) + Convert.ToDecimal(txtsummaryvat.Text)).ToString();
            //txtsummaryGtotalcst.Text = (Convert.ToDecimal(txtsubtotal.Text) + Convert.ToDecimal(txtsummaryCst.Text)).ToString();
        }
    }
    #endregion

    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }
    protected void txtDisc_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvQuotationItems.Rows)
        {

            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[11].Text = ((Convert.ToDecimal(Rate.Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(Rate.Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[13].Text = ((Convert.ToDecimal(gvr.Cells[11].Text) * Convert.ToDecimal(gvr.Cells[12].Text)) / 100).ToString();

        }

    }

    protected void txtDetQty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvQuotationItems.Rows)
        {
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            //decimal discu = Convert.ToDecimal(disc.Text);
            TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[9].Text = (Convert.ToDecimal(Rate.Text) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[11].Text = ((Convert.ToDecimal(Rate.Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(Rate.Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[13].Text = ((Convert.ToDecimal(gvr.Cells[11].Text) * Convert.ToDecimal(gvr.Cells[12].Text)) / 100).ToString();

        }
    }
    protected void txtDetRate_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvQuotationItems.Rows)
        {
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[9].Text = (Convert.ToDecimal(Rate.Text) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[11].Text = ((Convert.ToDecimal(Rate.Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(Rate.Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[13].Text = ((Convert.ToDecimal(gvr.Cells[11].Text) * Convert.ToDecimal(gvr.Cells[12].Text)) / 100).ToString();

        }
    }

    #region GridView Quotation Items Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        string x2 = gvQuotationItems.Rows[e.RowIndex].Cells[2].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Currency");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Discount");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GSTperc");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GSTRate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Room");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CurrencyId");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("OptId");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Floor");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SrlOrder");
        QuotationItems.Columns.Add(col);

        //col = new DataColumn("Item_Image");
        //QuotationItems.Columns.Add(col);

        //if (gvSubItems.Rows.Count > 0)
        //{
        //    DataTable SubItems = new DataTable();
        //    DataColumn dc;
        //    dc = new DataColumn("ITEM_CODE");
        //    SubItems.Columns.Add(dc);
        //    dc = new DataColumn("SUBITEM_CODE");
        //    SubItems.Columns.Add(dc);
        //    foreach (GridViewRow gvrow in SubItems.Rows)
        //    {
        //        if (x2 != gvrow.Cells[0].Text)
        //        {

        //            DataRow dr = SubItems.NewRow();
        //            dr["ITEM_CODE"] = gvrow.Cells[1].Text;
        //            dr["SUBITEM_CODE"] = gvrow.Cells[2].Text;
        //            SubItems.Rows.Add(dr);

        //        }
        //    }
        //    gvSubItems.DataSource = SubItems;
        //    gvSubItems.DataBind();

        //}

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    //dr["Quantity"] = gvrow.Cells[6].Text;
                    TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                    dr["Quantity"] = Quantity.Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    //dr["Rate"] = gvrow.Cells[8].Text;
                    TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                    dr["Rate"] = Rate.Text;
                    dr["Discount"] = gvrow.Cells[10].Text;
                    dr["SpPrice"] = gvrow.Cells[11].Text;
                    dr["GSTperc"] = gvrow.Cells[12].Text;
                    dr["GSTRate"] = gvrow.Cells[13].Text;
                    //dr["Room"] = gvrow.Cells[14].Text;
                    TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                    dr["Room"] = Room.Text;
                    dr["CurrencyId"] = gvrow.Cells[15].Text;
                    dr["Color"] = gvrow.Cells[16].Text;
                    dr["ColorId"] = gvrow.Cells[17].Text;
                    dr["OptId"] = gvrow.Cells[18].Text;
                    dr["ItemType"] = gvrow.Cells[19].Text;
                    dr["Remarks"] = gvrow.Cells[20].Text;
                    //dr["Floor"] = gvrow.Cells[21].Text;
                    TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                    dr["Floor"] = Floor.Text;
                    //  dr["SrlOrder"] = gvrow.Cells[21].Text;
                    TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                    dr["SrlOrder"] = srl.Text;

                    //Image img = (Image)gvrow.FindControl("Image");
                    //dr["Item_Image"] = img;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        btnApprove.Enabled = false;
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                SM.SalesQuotation objSM = new SM.SalesQuotation();
                objSM.QuotId = Request.QueryString["Cid"].ToString();
                objSM.SalesQuotationDetails_Delete(objSM.QuotId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                    //objSM.QuotDetQty = gvrow.Cells[6].Text;
                    TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                    objSM.QuotDetQty = Quantity.Text;
                    TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                    objSM.QuotRate = Rate.Text;
                    //objSM.QuotRate = gvrow.Cells[8].Text;
                    objSM.QuotDetDisc = gvrow.Cells[10].Text;
                    objSM.QuotDetSpPrice = gvrow.Cells[11].Text;
                    objSM.QuotGSTperc = gvrow.Cells[12].Text;
                    objSM.QuotGSTRate = gvrow.Cells[13].Text;
                    //objSM.QuotRoom = gvrow.Cells[14].Text;
                    TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                    objSM.QuotRoom = Room.Text;
                    objSM.QuotCurrency = gvrow.Cells[15].Text;
                    objSM.ColorId = gvrow.Cells[17].Text;
                    objSM.OptionalId = gvrow.Cells[18].Text;
                    objSM.Remarks = gvrow.Cells[20].Text;
                    objSM.SrlOrder = gvrow.Cells[22].Text;
                    // objSM.SrlOrder = gvrow.Cells[21].Text;
                    TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                    objSM.SrlOrder = srl.Text;
                    objSM.SalesQuotationDetails_Save();
                }

                SM.QuotApprove obj = new SM.QuotApprove();
                obj.quotid = Request.QueryString["Cid"].ToString();
                obj.flag = "Open";
                obj.approved = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                MessageBox.Show(this, obj.quotapprove());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());

            }
            finally
            {
                //gvQuotationItems.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
               "alert(' Quotation Updated & Approved sucessfully');window.location ='SalesQuotation.aspx';", true);
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                //Response.Write("<script language=javascript>window.close();</script>");
            }
        }


       
        //btnEdit_Click(sender, e);
        // gvQuotationDetails.DataBind();
        
    }
}