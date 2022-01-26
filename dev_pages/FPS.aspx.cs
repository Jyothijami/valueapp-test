using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;
using YantraDAL;
using System.Web.UI;


public partial class dev_pages_FPS : System.Web.UI.Page
{
    decimal TotalAmount1 = 0;

    decimal TotalAmount = 0;
    decimal TotalAmount2 = 0;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QuotationMaster_Fill();
            CustomerMaster_Fill();
        }
    }

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomer);
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

    #region Quotation Master Fill
    private void QuotationMaster_Fill()
    {
        try
        {
            SM.SalesQuotation.SalesQuotation_Select(ddlQuotationNo);
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

    #region Quotation No Selected Index Changed
    protected void ddlQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.SalesQuotation objSM = new SM.SalesQuotation();
            if (objSM.SalesQuotation_Select(ddlQuotationNo.SelectedItem.Value) > 0)
            {
                txtQuotationDate.Text = objSM.QuotDate;

                lblQuotRespId.Text = objSM.QuotRespoId;

                //}

                //objSM.SalesQuotationDetails_SelectForPO(ddlQuotationNo.SelectedItem.Value, gvQuotationProducts);

                ddlCustomer.SelectedValue = objSM.CustId;
                ddlCustomer_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objSM.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objSM.CustDetId;
                ddlContactPerson_SelectedIndexChanged(sender, e);


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

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                //rfvContactPerson.Enabled = true;
                //rfvUnitName.Enabled = true;
                //lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                //rfvContactPerson.Enabled = false;
                //rfvUnitName.Enabled = false;
                //lblUnitAddress.Text = "Customer Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtUnitAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtPhoneNo.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                    //txtCSTNo.Text = objSMCustomer.CSTNo;
                }
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

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtPhoneNo.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
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

    #region ddlContactPerson_SelectedIndexChanged
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtEmail.Text = objSMCustomer.CustCorpEmail;
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
                txtMobile.Text = objSMCustomer.CustCorpMobile;
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

    protected void gvDonepo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = e.Row.Cells[18].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = e.Row.Cells[18].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = e.Row.Cells[18].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = e.Row.Cells[18].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[15].Visible = false;
            //e.Row.Cells[13].Visible = false;
        }
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string disc, Amt;
            TextBox qty = (TextBox)row.FindControl("txtQuantity");
            TextBox price = (TextBox)row.FindControl("txtMRP");
            Label UnitPrice = (Label)row.FindControl("lblUnitPrice");
            Label SplPrice = (Label)row.FindControl("lblSplPrice");
            TextBox Disount = (TextBox)row.FindControl("txtgvDiscount");
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Products list?');");

            UnitPrice.Text = Math.Round((Convert.ToDecimal(SplPrice.Text) / Convert.ToDecimal(qty.Text)), 0).ToString();
            TotalAmount2 = TotalAmount2 + Convert.ToDecimal(SplPrice.Text);
            lblTotalAmt1.Text = TotalAmount2.ToString();

        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Products list?');");
        //    e.Row.Cells[9].Text = ((Convert.ToDouble(e.Row.Cells[10].Text)) / (Convert.ToDouble(e.Row.Cells[6].Text))).ToString("F");
        //    TotalAmount2 = TotalAmount2 + Convert.ToDecimal(e.Row.Cells[10].Text);
        //    lblTotalAmt1.Text = TotalAmount2.ToString();
        //}

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total Amount:";
            e.Row.Cells[10].Text = TotalAmount2.ToString();
            e.Row.Cells[15].Visible = false;
            // e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = false;
        }

    }

    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvDonepo.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtMRP");
            TextBox qty = (TextBox)gvr.FindControl("txtQuantity");
            Label UnitPrice = (Label)gvr.FindControl("lblUnitPrice");
            Label splPrice = (Label)gvr.FindControl("lblSplPrice");
            //TextBox Disount = (TextBox)gvr.FindControl("txtgvDiscount");

            if (rate.Text != "" && qty.Text != "")
            {
                splPrice.Text = ((Convert.ToDecimal(rate.Text)) * Convert.ToDecimal((qty.Text))).ToString();
                UnitPrice.Text = (Convert.ToDecimal(splPrice.Text) / Convert.ToDecimal(qty.Text)).ToString();
                //splPrice.Text = (Convert.ToDecimal(UnitPrice.Text) * Convert.ToDecimal(qty.Text)).ToString();
                TotalAmount2 = TotalAmount2 + Convert.ToDecimal(splPrice.Text);
                lblTotalAmt1.Text = TotalAmount2.ToString();
            }
            else
            {
                rate.Text = "0";
            }
        }
    }
    protected void txtMRP_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvDonepo.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtMRP");
            TextBox qty = (TextBox)gvr.FindControl("txtQuantity");
            Label UnitPrice = (Label)gvr.FindControl("lblUnitPrice");
            Label splPrice = (Label)gvr.FindControl("lblSplPrice");
            //TextBox Disount = (TextBox)gvr.FindControl("txtgvDiscount");
            if (rate.Text != "" && qty.Text != "")
            {
                splPrice.Text = ((Convert.ToDecimal(rate.Text)) * Convert.ToDecimal((qty.Text))).ToString();
                UnitPrice.Text = (Convert.ToDecimal(splPrice.Text) / Convert.ToDecimal(qty.Text)).ToString();
                TotalAmount2 = TotalAmount2 + Convert.ToDecimal(splPrice.Text);
                lblTotalAmt1.Text = TotalAmount2.ToString();
            }
            else
            {
                rate.Text = "0";
            }
        }
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvDonepo.Rows)
        {
            TextBox rate = (TextBox)gvr.FindControl("txtMRP");
            TextBox qty = (TextBox)gvr.FindControl("txtQuantity");
            Label UnitPrice = (Label)gvr.FindControl("lblUnitPrice");
            Label splPrice = (Label)gvr.FindControl("lblSplPrice");
            TextBox Disount = (TextBox)gvr.FindControl("txtgvDiscount");
            if (rate.Text != "" && qty.Text != "")
            {
                string disc, Amt;
                if (Disount.Text != "")
                {
                    Amt = ((Convert.ToDecimal(rate.Text)) * Convert.ToDecimal((qty.Text))).ToString();
                    disc = ((Convert.ToDecimal(Amt) * Convert.ToDecimal(Disount.Text)) / 100).ToString();
                    splPrice.Text = (Convert.ToDecimal(Amt) - Convert.ToDecimal(disc)).ToString();
                    UnitPrice.Text = (Convert.ToDecimal(splPrice.Text) / Convert.ToDecimal(qty.Text)).ToString();
                }
                UnitPrice.Text = ((Convert.ToDouble(splPrice.Text)) / (Convert.ToDouble(qty.Text))).ToString();
                Disount.Text = Math.Round((100 - ((Convert.ToDecimal(UnitPrice.Text)) / (Convert.ToDecimal(rate.Text))) * 100), 0).ToString();
            }
        }
    }
}