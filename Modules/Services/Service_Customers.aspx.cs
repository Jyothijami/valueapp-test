using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Services_Service_Customers : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            gvCustMasterDetails.DataBind();
            setControlsVisibility();
        }
    }


    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "44");
        btnNew.Enabled = up.add;

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCustMasterDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvCustMasterDetails.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        string str ="New";
        Response.Redirect("ServiceCustomerInformation.aspx?CustCode=" +str);
    }
    protected void lbtnCustMaster_Click(object sender, EventArgs e)
    {
        LinkButton lbtnCustMaster = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnCustMaster.Parent.Parent;
        gvCustMasterDetails.SelectedIndex = Row.RowIndex;
        Label lblcustcode = (Label)Row.FindControl("lblCustId");
        //string Cust_Code = gvCustMasterDetails.SelectedRow.Cells[0].Text;
        Response.Redirect("ServiceCustomerInformation.aspx?CustCode=" + lblcustcode.Text);

    }
    protected void lbtnCustCompName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnCompMaster = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnCompMaster.Parent.Parent;
        gvCustMasterDetails.SelectedIndex = Row.RowIndex;
        Label lblcustcode = (Label)Row.FindControl("lblCustId");
        //string Cust_Code = gvCustMasterDetails.SelectedRow.Cells[0].Text;
        Response.Redirect("ServiceCustomerInformation.aspx?CustCode=" + lblcustcode.Text);
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvCustMasterDetails.DataBind();
    }
    #endregion
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvCustMasterDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
}
 
