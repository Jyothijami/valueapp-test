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

public partial class Modules_Masters_OperationsMaster : System.Web.UI.UserControl
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
            OperationSave();
            tblOprDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            OperationUpdate();
            tblOprDetails.Visible = false;
        }
        gvOperationDetails.SelectedIndex = -1;
    }
    #endregion

    #region OperationSave
    private void OperationSave()
    {
        try
        {
            Masters.Operation objMaster = new Masters.Operation();
            objMaster.OprName = txtOperationCode.Text;
            objMaster.OprDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Operation_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvOperationDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region OperationUpdate
    private void OperationUpdate()
    {
        try
        {
            Masters.Operation objMaster = new Masters.Operation();
            objMaster.OprId = gvOperationDetails.SelectedRow.Cells[1].Text;
            objMaster.OprName = txtOperationCode.Text;
            objMaster.OprDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Operation_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvOperationDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvOperationDetails_RowDataBound
    protected void gvOperationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button OperationName_Click
    protected void lbtnOperationName_Click(object sender, EventArgs e)
    {
        tblOprDetails.Visible = false;
        LinkButton lbtnOperationName;
        lbtnOperationName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnOperationName.Parent.Parent;
        gvOperationDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvOperationDetails.SelectedIndex > -1)
        {
            tblOprDetails.Visible = true;
            txtOperationCode.Text = gvOperationDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvOperationDetails.SelectedRow.Cells[3].Text;
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
        if (gvOperationDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.Operation objMaster = new Masters.Operation();
                objMaster.OprId = gvOperationDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Operation_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblOprDetails.Visible = false;
               
                gvOperationDetails.DataBind();
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
        tblOprDetails.Visible = true;
        ScriptManagerLocal.SetFocus(txtOperationCode);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvOperationDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblOprDetails.Visible = false;
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
