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

public partial class Modules_Masters_DesignationMaster : System.Web.UI.UserControl
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
            DesignationSave();
            tblDesgDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            DesignationUpdate();
            tblDesgDetails.Visible = false;
        }
        gvDesignationDetails.SelectedIndex = -1;
    }
    #endregion

    #region DesignationSave
    private void DesignationSave()
    {
        try
        {
            Masters.Designation objMaster = new Masters.Designation();
            objMaster.DesgName = txtDesignationName.Text;
            objMaster.DesgDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Designation_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvDesignationDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region DesignationUpdate
    private void DesignationUpdate()
    {
        try
        {
            Masters.Designation objMaster = new Masters.Designation();
            objMaster.DesgId = gvDesignationDetails.SelectedRow.Cells[1].Text;
            objMaster.DesgName = txtDesignationName.Text;
            objMaster.DesgDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Designation_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvDesignationDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvDesignationDetails_RowDataBound
    protected void gvDesignationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button DesignationName_Click
    protected void lbtnDesignationName_Click(object sender, EventArgs e)
    {
        tblDesgDetails.Visible = false;
        LinkButton lbtnDesignationName;
        lbtnDesignationName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDesignationName.Parent.Parent;
        gvDesignationDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvDesignationDetails.SelectedIndex > -1)
        {
            tblDesgDetails.Visible = true;
            txtDesignationName.Text = gvDesignationDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvDesignationDetails.SelectedRow.Cells[3].Text;
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

        if (gvDesignationDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.Designation objMaster = new Masters.Designation();
                objMaster.DesgId = gvDesignationDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Designation_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblDesgDetails.Visible = false;
               
                gvDesignationDetails.DataBind();
                gvDesignationDetails.SelectedIndex = -1;

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
        tblDesgDetails.Visible = true;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvDesignationDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblDesgDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Search By DropdownList Select Index Change Event
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
    } 
    #endregion
}

