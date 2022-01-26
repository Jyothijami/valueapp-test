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


public partial class Modules_Masters_CurrencyType : System.Web.UI.UserControl
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
            CurrencyTypeSave();
            tblCurrencyDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            CurrencyTypeUpdate();
            tblCurrencyDetails.Visible = false;
        }
        gvCurrencyDetails.SelectedIndex = -1;
    }
    #endregion
    
    #region CurrencyTypeSave
    private void CurrencyTypeSave()
    {
        try
        {
            Masters.CurrencyType objMaster = new Masters.CurrencyType();
            objMaster.CurrencyName = txtCurrencyName.Text;
            objMaster.CurrencyFullName = txtCurrencyFullName.Text;
            objMaster.CurrencyDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.CurrencyType_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvCurrencyDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region CurrencyTypeUpdate
    private void CurrencyTypeUpdate()
    {
        try
        {
            Masters.CurrencyType objMaster = new Masters.CurrencyType();
            objMaster.CurrencyId = gvCurrencyDetails.SelectedRow.Cells[1].Text;
            objMaster.CurrencyName = txtCurrencyName.Text;
            objMaster.CurrencyFullName = txtCurrencyFullName.Text;
            objMaster.CurrencyDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.CurrencyType_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvCurrencyDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvDepartmentDetails_RowDataBound
    protected void gvCurrencyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button CurrencyName_Click
    protected void lbtnCurrencyName_Click(object sender, EventArgs e)
    {
        tblCurrencyDetails.Visible = false;
        LinkButton lbtnCurrencyName;
        lbtnCurrencyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCurrencyName.Parent.Parent;
        gvCurrencyDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion


    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCurrencyDetails.SelectedIndex > -1)
        {
            tblCurrencyDetails.Visible = true;
            txtCurrencyName.Text = gvCurrencyDetails.SelectedRow.Cells[0].Text;
            txtCurrencyFullName.Text = gvCurrencyDetails.SelectedRow.Cells[3].Text;
            txtDescription.Text = gvCurrencyDetails.SelectedRow.Cells[4].Text;
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
        if (gvCurrencyDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.CurrencyType objMaster = new Masters.CurrencyType();
                objMaster.CurrencyId = gvCurrencyDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.CurrencyType_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
               
                gvCurrencyDetails.DataBind();
                gvCurrencyDetails.SelectedIndex = -1;

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
        tblCurrencyDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtCurrencyName);
    }
    #endregion


    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvCurrencyDetails.DataBind();
    }
    #endregion


    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCurrencyDetails.Visible = false;
    }
    #endregion



    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion
    #region Dropdown list select index change
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        txtSearchText.Text = "";
        
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
   #endregion
}
