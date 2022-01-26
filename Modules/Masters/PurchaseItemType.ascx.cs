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

public partial class Modules_Masters_PItemType : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }

     #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ItemSave();
        }
        else if (btnSave.Text == "Update")
        {
            ItemUpdate();
        }
    }
    #endregion

    #region ItemSave
    private void ItemSave()
    {
        try
        {
            Masters.PurchaseItemType objMaster = new Masters.PurchaseItemType();
            objMaster.PurchaseItemTypeName = txtItemTypeName.Text;
            objMaster.PurchaseItemDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.ItemType_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblItemTypeDetails.Visible = false;
           
            gvItemTypeDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ItemUpdate
    private void ItemUpdate()
    {
        try
        {
            Masters.PurchaseItemType objMaster = new Masters.PurchaseItemType();
            objMaster.PurchaseItemTypeId = gvItemTypeDetails.SelectedRow.Cells[1].Text;
            objMaster.PurchaseItemTypeName = txtItemTypeName.Text;
            objMaster.PurchaseItemDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.ItemType_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblItemTypeDetails.Visible = false;
           
            gvItemTypeDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvItemTypeDetails_RowDataBound
    protected void gvItemTypeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button ItemTypeName_Click
    protected void lbtnItemTypeName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnItemTypeName;
        lbtnItemTypeName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnItemTypeName.Parent.Parent;
        gvItemTypeDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        tblItemTypeDetails.Visible = false;
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvItemTypeDetails.SelectedIndex > -1)
        {
            tblItemTypeDetails.Visible = true;
            txtItemTypeName.Text = gvItemTypeDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvItemTypeDetails.SelectedRow.Cells[3].Text;
            btnSave.Text = "Update";
           
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvItemTypeDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.PurchaseItemType objMaster = new Masters.PurchaseItemType();
                objMaster.PurchaseItemTypeId = gvItemTypeDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.ItemType_Delete());

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblItemTypeDetails.Visible = false;
               
                gvItemTypeDetails.DataBind();
                gvItemTypeDetails.SelectedIndex = -1;
                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblItemTypeDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtItemTypeName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvItemTypeDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblItemTypeDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion 

}
