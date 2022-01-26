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
using vllib;

public partial class Modules_SM_SalesQuotation : basePage
{
    ScriptManager ScriptManagerLocal;
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            setControlsVisibility();
            //Yantra.Authentication.Privilege_Check(this);
            gridbind();
        }
    }

   

    private void gridbind()
    {        
        lblSearchItemHidden.Text = "0";
        lblSearchTypeHidden.Text = "0";
        lblSearchValueFromHidden.Text = "0";
        lblSearchValueHidden.Text = "0";
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        lblUserId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        lblCPID.Text = cp.getPresentCompanySessionValue();
        gvQuotationDetails.DataBind();
    }
    #endregion


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "8");
        btnNewSalesQuotation.Enabled = up.add;

    }

    #region Link Button QuotationNo_Click
    protected void lbtnQuotationNo_Click(object sender, EventArgs e)
    {
        LinkButton lbtnQuoNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnQuoNo.Parent.Parent;
        gvQuotationDetails.SelectedIndex = Row.RowIndex;
        if (gvQuotationDetails.SelectedRow.Cells[0].Text != null)
        {
            try
            {
                SM.SalesQuotation objSM = new SM.SalesQuotation();
                if (objSM.SalesQuotation_Select1(gvQuotationDetails.SelectedRow.Cells[0].Text.ToString()) > 0)
                {
                    if (objSM.QuotVAT == "14.5" || objSM.QuotCST == "2" || objSM.QuotIncluding == "14.5" || objSM.QuotIncluding == "2")
                    {
                        Response.Redirect("SalesQuotationDetails.aspx?QuoNo=" + lbtnQuoNo.Text +
            "&QuoId=" + gvQuotationDetails.SelectedRow.Cells[0].Text +
            "&AppBy=" + gvQuotationDetails.SelectedRow.Cells[7].Text +
            "&Status=" + gvQuotationDetails.SelectedRow.Cells[8].Text + "&lbtn=lbtn");
                    }
                    else
                    {
                        Response.Redirect("SalesQuotationDetails1.aspx?QuoNo=" + lbtnQuoNo.Text +
            "&QuoId=" + gvQuotationDetails.SelectedRow.Cells[0].Text +
            "&AppBy=" + gvQuotationDetails.SelectedRow.Cells[7].Text +
            "&Status=" + gvQuotationDetails.SelectedRow.Cells[8].Text + "&lbtn=lbtn");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        //Response.Redirect("SalesQuotationDetails.aspx?QuoNo=" + lbtnQuoNo.Text +
        //    "&QuoId=" + gvQuotationDetails.SelectedRow.Cells[0].Text +
        //    "&AppBy=" + gvQuotationDetails.SelectedRow.Cells[7].Text +
        //    "&Status=" + gvQuotationDetails.SelectedRow.Cells[8].Text + "&lbtn=lbtn");

    }
    #endregion

    protected void btnNewSalesQuotation_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesQuotationDetails1.aspx?QuoNo=" + "New");
    }

    #region GridView Quotation Details Row DataBound
    protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[9].Visible = false;
            //if (!string.IsNullOrEmpty(e.Row.Cells[9].Text) && e.Row.Cells[9].Text != "&nbsp;")
            //{             }
        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Quotation Date")
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
        gvQuotationDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "Quotation Date")
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
        gvQuotationDetails.DataBind();

    }
    #endregion

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvQuotationDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);

        gvQuotationDetails.DataBind();
    }

    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvQuotationDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
