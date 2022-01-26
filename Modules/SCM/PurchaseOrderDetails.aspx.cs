using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;

public partial class Modules_SCM_PurchaseOrderDetails : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCPID.Text = cp.getPresentCompanySessionValue();
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        gvFixedPODetails.DataBind();
        setControlsVisibility();

    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "23");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnPrint.Enabled = up.Print;
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFixedPODetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvFixedPODetails.DataBind();
    }
    protected void lbtnFixedPONo_Click(object sender, EventArgs e)
    {
       // tblPurchaseOrder.Visible = false;
        LinkButton lbtnFixedPONo;
        lbtnFixedPONo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnFixedPONo.Parent.Parent;
        gvFixedPODetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");  
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PurchaseOrder1.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            Response.Redirect("PurchaseOrder1.aspx?poNo=" + gvFixedPODetails.SelectedRow.Cells[0].Text);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                SCM.SupplierFixedPO objSCM = new SCM.SupplierFixedPO();
                MessageBox.Show(this, objSCM.SuppliersFixedPO_Delete(gvFixedPODetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvFixedPODetails.DataBind();
                SCM.ClearControls(this);
                //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void gvFixedPODetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;
        }
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvFixedPODetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) 
        //{
        //    lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; 
        //}
        //else if (ceSearchFrom.Enabled == true) 
        //{
        //    lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); 
        //}
        //if (ceSearchValueToDate.Enabled == false) 
        //{
        //    lblSearchValueHidden.Text = txtSearchText.Text; 
        //}
        //else if (ceSearchValueToDate.Enabled == true) 
        //{
        //    lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text);
        //}

        if (ddlSearchBy.SelectedItem.Text == "PO Date")
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
        gvFixedPODetails.DataBind();
        ////=====
        //lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        //lblSearchValueHidden.Text = txtSearchText.Text;
        //gvFixedPODetails.DataBind();
    }

    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvFixedPODetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

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
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "PO Date")
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=purchaseorder&fpoid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "";
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
    protected void btnp2_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=P2&SupDetid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "";
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (gvFixedPODetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PT&SupDetid=" + gvFixedPODetails.SelectedRow.Cells[0].Text + "";
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
}
 
