using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;

public partial class MASTERS_ItemMasterDetails : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvItemMaster.DataBind();
        setControlsVisibility();
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "3");
        ImageButton1.Enabled = up.add;
        //imagebutton3.Enabled = up.Update;
        //imagebutton2.Enabled = up.Delete;
        foreach (GridViewRow gvr in gvItemMaster.Rows)
        {
            ImageButton btnEdit = (ImageButton)gvr.FindControl("ibtmImage");
            ImageButton btnDelete = (ImageButton)gvr.FindControl("ImageButton2");
            btnEdit.Enabled = up.Update;
            btnDelete.Enabled = up.Delete;
        }
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemMaster.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvItemMaster.DataBind();

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        Response.Redirect("~/Modules/MASTERS/ItemMaster.aspx");
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

        Response.Redirect("~/dev_pages/DiscontinuedItem.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgdelete;
        imgdelete = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)imgdelete.Parent.Parent;
        gvItemMaster.SelectedIndex = gvRow.RowIndex;

            try
            {
                Masters.ItemMaster objSM = new Masters.ItemMaster();
                objSM.ItemCode = gvItemMaster.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objSM.ItemMasterStatus_Update());
            }
            catch (Exception ex)
            {
                Masters.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvItemMaster.DataBind();
                Masters.Dispose();
            }
        }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText1.Text;
        gvItemMaster.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText1.Text = "";
    }
    protected void gvItemMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        GridViewRow gvr = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = (ImageButton)gvr.FindControl("ImageButton2");

           // e.Row.Cells[8].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
           
            e.Row.Cells[0].Text = "Page " + (gvItemMaster.PageIndex + 1) + " of " + gvItemMaster.PageCount;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[15].Text == "Discontinued")
            {
                e.Row.Cells[2].Text = e.Row.Cells[2].Text + ' ' + " (Discontinued)";
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void ibtmImage_Command(object sender, CommandEventArgs e)
    {
        string strJS = ("<script type='text/javascript'>window.open('ItemEdit.aspx?Cid=" + e.CommandArgument.ToString() + "','_blank');</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "strJSAlert", strJS);
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvItemMaster.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}
 
