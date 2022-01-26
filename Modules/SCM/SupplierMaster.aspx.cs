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


public partial class Modules_Scm_SupplierMaster : basePage
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            setControlsVisibility();

            gvSupplierDetails.DataBind();
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "20");
        // btnNew.Enabled = up.add;
        //btnEdit.Enabled=up.Update;

    }

    #region gvSuppliersMasterDetails_RowDataBound
    protected void gvSuppliersMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button SupplierName_Click
    protected void lbtnSupplierName_Click(object sender, EventArgs e)
    {
        tblSupDetails.Visible = false;
        LinkButton lbtnSupplierName;
        lbtnSupplierName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSupplierName.Parent.Parent;
        gvSupplierDetails.SelectedIndex = gvRow.RowIndex;
        Response.Redirect("SupplierMasterNew.aspx?supplierId=" + gvSupplierDetails.SelectedRow.Cells[1].Text);
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }
    #endregion

    

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvSupplierDetails.DataBind();
    }
    #endregion

    

    

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSupplierDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSupplierDetails.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierMasterNew.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if(gvSupplierDetails.SelectedIndex > -1)
        {
            Response.Redirect("SuppliersEnquiryNew.aspx?supplierId=" + gvSupplierDetails.SelectedRow.Cells[1].Text);
        }
    }

    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvSupplierDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
