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
public partial class Modules_SCM_OrderSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tblSub.Visible = false;
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        GridView1.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Po Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            meeSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    SCM.SupplierSelfPO obj = new SCM.SupplierSelfPO();

    //    Label lblId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblId");
    //    TextBox txtExpDate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtExpDate");
    //    TextBox txtArrivedDate = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtArrivedDate");
    //    DropDownList ddlStatus = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlStatus");

    //    obj.SuppliersSelfPODetStatus_Update(ddlStatus.SelectedValue, txtExpDate.Text, txtArrivedDate.Text, lblId.Text);
    //    GridView1.EditIndex = -1;
    //    GridView1.DataBind();
    //}
    //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView1.EditIndex = e.NewEditIndex;
    //    GridView1.DataBind();
    //}
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            imgFromDate.Visible = true;
            ceSearchFrom.Enabled = true;
            meeSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            meeSearchFromDate.Enabled = false;
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        tblSub.Visible = true;
        LinkButton lbtnEdit;
        lbtnEdit = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEdit.Parent.Parent;
        GridView1.SelectedIndex = gvRow.RowIndex;

        if (GridView1.SelectedIndex > -1)
        {
            if (GridView1.SelectedRow.Cells[9].Text == "")
            {
                txtArriveddate.Text = "";
            }
            else
            {
                txtArriveddate.Text = GridView1.SelectedRow.Cells[9].Text;
            }
            if (GridView1.SelectedRow.Cells[7].Text == "")
            {
                txtExpdateofarrival.Text = "";
            }
            else
            {
                txtExpdateofarrival.Text = GridView1.SelectedRow.Cells[7].Text;
            }
            if (GridView1.SelectedRow.Cells[10].Text == "&nbsp;")
            {
                ddlstatus.SelectedItem.Value = "Pending";
            }
            else
            {
                ddlstatus.SelectedValue = GridView1.SelectedRow.Cells[10].Text;
            }
        }

    }





    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.SupplierSelfPO obj = new SCM.SupplierSelfPO();
             MessageBox.Show(this,obj.SuppliersSelfPODetStatus_Update(ddlstatus.SelectedValue,Yantra.Classes.General.toMMDDYYYY(txtExpdateofarrival.Text), Yantra.Classes.General.toMMDDYYYY(txtArriveddate.Text), GridView1.SelectedRow.Cells[11].Text));
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            GridView1.DataBind();
            tblSub.Visible = false;
        }
    }
}

 
