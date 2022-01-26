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

public partial class Modules_Masters_IdleCodes : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }
    #endregion

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
            IdleCodeSave();
            tblIdleCodeDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            IdleCodeUpdate();
            tblIdleCodeDetails.Visible = false;
        }
        gvIdleCodeDetails.SelectedIndex = -1;
    }
    #endregion

    #region IdleCode Save
    private void IdleCodeSave()
    {
        try
        {
            Masters.IdleCode objMaster = new Masters.IdleCode();
            objMaster.IdleCodeName = txtIdleCode.Text;
            objMaster.IdleCodeDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.IdleCode_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvIdleCodeDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region IdleCode Update
    private void IdleCodeUpdate()
    {
        try
        {
            Masters.IdleCode objMaster = new Masters.IdleCode();
            objMaster.IdleCodeId = gvIdleCodeDetails.SelectedRow.Cells[1].Text;
            objMaster.IdleCodeName = txtIdleCode.Text;
            objMaster.IdleCodeDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.IdleCode_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvIdleCodeDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvIdleCodeDetails_RowDataBound
    protected void gvIdleCodeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button IdleCodeName_Click
    protected void lbtnIdleCodeName_Click(object sender, EventArgs e)
    {
        tblIdleCodeDetails.Visible = false;
        LinkButton lbtnIdleCodeName;
        lbtnIdleCodeName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIdleCodeName.Parent.Parent;
        gvIdleCodeDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvIdleCodeDetails.SelectedIndex > -1)
        {
            tblIdleCodeDetails.Visible = true;
            txtIdleCode.Text = gvIdleCodeDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvIdleCodeDetails.SelectedRow.Cells[3].Text;
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
        if (gvIdleCodeDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.IdleCode objMaster = new Masters.IdleCode();
                objMaster.IdleCodeId = gvIdleCodeDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.IdleCode_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblIdleCodeDetails.Visible = false;
               
                gvIdleCodeDetails.DataBind();
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
        tblIdleCodeDetails.Visible = true;
        ScriptManagerLocal.SetFocus(txtIdleCode);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvIdleCodeDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblIdleCodeDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
