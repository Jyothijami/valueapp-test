using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;
public partial class Modules_SCM_SupplierQuationDetails : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            gvSupEnquiryDetails.DataBind();
            setControlsVisibility();

        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "21");
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnPrint.Enabled = up.Print;

    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSupEnquiryDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSupEnquiryDetails.DataBind();
    }
    protected void gvSupEnquiryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            
        }
    }
    protected void lbtnSupEnqNo_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSuppEnqNo;
        lbtnSuppEnqNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSuppEnqNo.Parent.Parent;
        gvSupEnquiryDetails.SelectedIndex = gvRow.RowIndex;
       // Response.Redirect("SuppliersEnquiryNew.aspx?supEnqNo=" + gvSupEnquiryDetails.SelectedRow.Cells[0].Text);

        //Old code
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        //try
        //{
        //    SCM.SuppliersEnquiry objSCM = new SCM.SuppliersEnquiry();
        //    if (objSCM.SuppliersEnquiryMaster_Select(gvSupEnquiryDetails.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = false;
        //        tblSupEnqDetails.Visible = true;
        //        txtEnquiryNo.Text = objSCM.SuppEnqNo;
        //        txtEnquiryDate.Text = objSCM.SuppEnqDate;
        //        txtEnquiryStatus.Text = objSCM.SuppEnqStatus;
        //        ddlCriteria.SelectedValue = objSCM.SuppEnqFollwUp;
        //        txtEnquiryDueDate.Text = objSCM.SuppEnqDueDate;
        //        ddlDeliveryType.SelectedValue = objSCM.SuppEnqDespId;
        //        ddlPreparedBy.SelectedValue = objSCM.SuppEnqPreparedBy;
        //        ddlApprovedBy.SelectedValue = objSCM.SuppEnqApprovedBy;
        //        objSCM.SuppliersEnquiryDetails_Select(gvSupEnqDetails.SelectedRow.Cells[0].Text, gvItemDetails);
        //        objSCM.EnquirySuppliersDetails_Select(gvSupEnqDetails.SelectedRow.Cells[0].Text, gvSupplierDetails);
        //        gvItem.DataBind();
        //        // ddlIndentApprovel_SelectedIndexChanged(sender, e);

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    btnDelete.Attributes.Clear();
        //    SCM.Dispose();
        //}

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvSupEnquiryDetails.SelectedIndex > -1)
        {
            Response.Redirect("SupplierQuation.aspx?enqNo=" + gvSupEnquiryDetails.SelectedRow.Cells[0].Text);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvSupEnquiryDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvSupEnquiryDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SupEnq&SupDetid=" + gvSupEnquiryDetails.SelectedRow.Cells[0].Text + "";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSupEnquiryDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.IndentApproval objIndAppr = new SCM.IndentApproval();

                MessageBox.Show(this, objIndAppr.IndentApproval_Delete(gvSupEnquiryDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvSupEnquiryDetails.DataBind();
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvSupEnquiryDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        
        if (ddlSearchBy.SelectedItem.Text == "Sup Enq Date")
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
        gvSupEnquiryDetails.DataBind();
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Sup Enq Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            
        }
        else
        {
            ddlSymbols.Visible = false;
            
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
           
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

        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
           
        }
    }
    #endregion


}
 
