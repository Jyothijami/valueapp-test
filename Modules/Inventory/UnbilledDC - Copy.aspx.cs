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
using System.IO;
using vllib;
using YantraBLL.Modules;
public partial class Modules_Inventory_UnbilledDC : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.CompanyProfile.Company_Select(ddlCpId);

            lblCp_Ids .Text = "1,3,5";


        }
    }
    protected void gvDeliveryChallanDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[4].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[10].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
           // e.Row.Cells[8].Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[13].Text == "Spares")
        //    {
        //        e.Row.Cells[7].Visible = false;
        //    }
        //    else if (e.Row.Cells[13].Text == "Sales")
        //    {
        //        e.Row.Cells[8].Visible = false;
        //    }
        //}
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvDeliveryChallanDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy.SelectedItem.Text == "Company Search") 
        {
            lblSearchValueHidden.Text = ddlCpId.SelectedValue;
        }

         else if (ddlSearchBy.SelectedItem.Text == "DC Date")
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
        gvDeliveryChallanDetails.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "DC Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
            ddlCpId.Visible = false;
            ddlCpId .SelectedIndex = 0;

        }
        else if (ddlSearchBy.SelectedItem.Text == "Company Search")
        {
            ddlSymbols.Visible = false;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            ddlCpId.Visible = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchText.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
            ddlCpId.Visible = false;
            ddlCpId .SelectedIndex = 0;

        }
        
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
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

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDeliveryChallanDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvDeliveryChallanDetails.DataBind();
    }
    protected void lnkSales_Click(object sender, EventArgs e)
    {
        pnlSample.Visible = false;
        pnlSales.Visible = true;
    }
    protected void lnkCash_Click(object sender, EventArgs e)
    {
        pnlSales.Visible = false;
        pnlSample.Visible = true;

    }
    protected void ddlSearchBy1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy1.SelectedItem.Text == "DC Date")
        {
            ddlSymbol1.Visible = true;
            txtSearchText1.Visible = false;
            txtSearchValueToDate1.Visible = false;
            txtSearchValueFromDate1.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbol1.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate1.Visible = false;
            txtSearchValueFromDate1.Visible = false;
            txtSearchText1.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbol1.SelectedIndex = 0;
        }
        txtSearchText1.Text = string.Empty;
        txtSearchValueFromDate1.Text = string.Empty;
        txtSearchValueToDate1.Text = string.Empty;
    }
    protected void ddlSymbol1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbol1.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate1.Visible = true;
            txtSearchValueToDate1.Visible = true;
            txtSearchText1.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate1.Visible = true;
            txtSearchValueToDate1.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    protected void btnSearchGo1_Click(object sender, EventArgs e)
    {
        gvSampleDC.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy1.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbol1.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy1.SelectedItem.Text == "DC Date")
        {
            if (ddlSymbol1.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate1.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate1.Text);
                txtSearchValueToDate1.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate1.Text);

            }
            else
            {
                txtSearchText1.Visible = false;
                txtSearchValueFromDate1.Visible = true;
                txtSearchValueToDate1.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate1.Text);
            }
        }
        else
        {
            txtSearchValueFromDate1.Visible = false;
            txtSearchValueToDate1.Visible = false;
            lblSearchValueHidden.Text = txtSearchText1.Text;
        }
        gvSampleDC.DataBind();
    }
    protected void gvSampleDC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}

 
