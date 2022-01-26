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

public partial class Modules_Masters_DespatchMode : System.Web.UI.UserControl
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
            DespatchSave();
        }
        else if (btnSave.Text == "Update")
        {
            DespatchUpdate();
        }
        gvDespatchDetails.SelectedIndex = -1;
        tblDespatchDetails.Visible = false;
    }
    #endregion

    #region DespatchSave
    private void DespatchSave()
    {
        try
        {
            Masters.DespatchMode objMaster = new Masters.DespatchMode();
            objMaster.DespmName = txtDespatchName.Text;
            objMaster.DespmDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.DespatchMode_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvDespatchDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region DespatchUpdate
    private void DespatchUpdate()
    {
        try
        {
            Masters.DespatchMode objMaster = new Masters.DespatchMode();
            objMaster.DespmId = gvDespatchDetails.SelectedRow.Cells[1].Text;
            objMaster.DespmName = txtDespatchName.Text;
            objMaster.DespmDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.DespatchMode_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvDespatchDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvDespatchDetails_RowDataBound
    protected void gvDespatchDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button DespatchName_Click
    protected void lbtnDespatchName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDespatchName;
        lbtnDespatchName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDespatchName.Parent.Parent;
        gvDespatchDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        tblDespatchDetails.Visible = false;
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvDespatchDetails.SelectedIndex > -1)
        {
            tblDespatchDetails.Visible = true;            
            txtDespatchName.Text = gvDespatchDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvDespatchDetails.SelectedRow.Cells[3].Text;
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
        if (gvDespatchDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.DespatchMode objMaster = new Masters.DespatchMode();
                objMaster.DespmId = gvDespatchDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.DespatchMode_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
               
                gvDespatchDetails.DataBind();
                gvDespatchDetails.SelectedIndex = -1;

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
        tblDespatchDetails.Visible = true;   
        //ScriptManagerLocal.SetFocus(txtDespatchName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvDespatchDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblDespatchDetails.Visible = false;      
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
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
