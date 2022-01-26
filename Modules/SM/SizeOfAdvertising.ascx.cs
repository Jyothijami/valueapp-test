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
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_SM_SizeOfAdvertising : System.Web.UI.UserControl
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }

    }
    #endregion

    #region Page PreRender
    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    //}
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SizeSave();
            tblSize.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            SizeUpdate();
            tblSize.Visible = false;
        }
       gvSize.SelectedIndex = -1;
    }
    #endregion

    #region SizeSave
    private void SizeSave()
    {
        try
        {
            SM.SizeOfAdvertising objam = new SM.SizeOfAdvertising();
            objam.SaSize = txtSize.Text;
           
            objam.SaDesc = txtDescription.Text;

            MessageBox.Show(this, objam.SizeOfAdvertising_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvSize.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion


    #region SizeUpdate
    private void SizeUpdate()
    {
        try
        {
            SM.SizeOfAdvertising objam = new SM.SizeOfAdvertising();
            objam.SaId = gvSize.SelectedRow.Cells[1].Text;
            objam.SaSize = txtSize.Text;
            //objam.SaName = txtName.Text;
            objam.SaDesc = txtDescription.Text;
            MessageBox.Show(this, objam.SizeOfAdvertising_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvSize.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvSize.SelectedIndex > -1)
        {
            tblSize.Visible = true;
            txtSize.Text = gvSize.SelectedRow.Cells[0].Text;
            //txtName.Text = gvSize.SelectedRow.Cells[1].Text;
            txtDescription.Text = gvSize.SelectedRow.Cells[3].Text;

            btnRefresh.Visible = true;
            btnSave.Text = "Update";
            btnSave.Enabled = true;

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    protected void lbtnSizeOfAdvertising_Click(object sender, EventArgs e)
    {
        tblSize.Visible = false;
        LinkButton lbtnSizeOfAdvertising;
        lbtnSizeOfAdvertising = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSizeOfAdvertising.Parent.Parent;
        gvSize.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        if (gvSize.SelectedIndex > -1)
        {
            tblSize.Visible = true;
            btnSave.Text = "update";
            btnSave.Enabled = false;

            txtSize.Text = gvSize.SelectedRow.Cells[0].Text;
            //txtName.Text = gvSize.SelectedRow.Cells[1].Text;
            txtDescription.Text = gvSize.SelectedRow.Cells[3].Text;

        }
    }
    protected void gvSize_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }

    }
    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSize.SelectedIndex > -1)
        {
            try
            {
                SM.SizeOfAdvertising objam = new SM.SizeOfAdvertising();
                objam.SaId = gvSize.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objam.SizeOfAdvertising_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvSize.DataBind();
                gvSize.SelectedIndex = -1;

                SM.ClearControls(this);
                SM.Dispose();
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
        SM.ClearControls(this);
        btnSave.Text = "Save";

        tblSize.Visible = true;

    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvSize.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblSize.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
    }
    #endregion

    #region Dropdown list select index change
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";

    }
    #endregion


}
