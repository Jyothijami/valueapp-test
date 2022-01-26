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


public partial class Modules_Masters_UnitMaster : System.Web.UI.UserControl
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
            UnitMasterSave();
            tblUnitMasterDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            UnitMasterUpdate();
            tblUnitMasterDetails.Visible = false;
        }
        gvUnitMasterDetails.SelectedIndex = -1;
    }
    #endregion


    #region UnitMasterSave
    private void UnitMasterSave()
    {
        try
        {
            Masters.UnitMaster objMaster = new Masters.UnitMaster();
            objMaster.UOMName = txtUnitShort.Text;
            objMaster.UOMDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.UnitMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvUnitMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region UnitMasterUpdate
    private void UnitMasterUpdate()
    {
        try
        {
            Masters.UnitMaster objMaster = new Masters.UnitMaster();
            objMaster.UOMId = gvUnitMasterDetails.SelectedRow.Cells[1].Text;
            objMaster.UOMName = txtUnitShort.Text;
            objMaster.UOMDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.UnitMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvUnitMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvUnitMasterDetails_RowDataBound
    protected void gvUnitMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button UnitMaster_Click
    protected void lbtnUnitMaster_Click(object sender, EventArgs e)
    {
        tblUnitMasterDetails.Visible = false;
        LinkButton lbtnUnitMaster;
        lbtnUnitMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnUnitMaster.Parent.Parent;
        gvUnitMasterDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvUnitMasterDetails.SelectedIndex > -1)
        {
            tblUnitMasterDetails.Visible = true;
            txtUnitShort.Text = gvUnitMasterDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvUnitMasterDetails.SelectedRow.Cells[3].Text;
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
        if (gvUnitMasterDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.UnitMaster objMaster = new Masters.UnitMaster();
                objMaster.UOMId = gvUnitMasterDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.UnitMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblUnitMasterDetails.Visible = false;
               
                gvUnitMasterDetails.DataBind();
                gvUnitMasterDetails.SelectedIndex = -1;

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
        tblUnitMasterDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtUnitShort);
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblUnitMasterDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvUnitMasterDetails.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);

    }
}
