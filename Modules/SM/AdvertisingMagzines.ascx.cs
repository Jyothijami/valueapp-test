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

public partial class Modules_SM_AdvertisingMagzines : System.Web.UI.UserControl
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
            AdvertisingMagzineSave();

            tblMagzine.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            AdvertisingMagzineUpdate();
            tblMagzine.Visible = false;
        }
        gvMagzine.SelectedIndex = -1;
    }
    #endregion

    #region  AdvertisingMagzineSave
    private void AdvertisingMagzineSave()
    {
        try
        {
            SM.AdvertisingMagzines objam = new SM.AdvertisingMagzines();
            objam.AmName = txtMagzine.Text;
            objam.AmDesc = txtDescription.Text;

            MessageBox.Show(this, objam.AdvertisingMagzines_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvMagzine.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region AdvertisingMagzineUpdate
    private void AdvertisingMagzineUpdate()
    {
        try
        {
            SM.AdvertisingMagzines objam = new SM.AdvertisingMagzines();
            objam.AmId = gvMagzine.SelectedRow.Cells[1].Text;
            objam.AmName = txtMagzine.Text;
            objam.AmDesc = txtDescription.Text;
            MessageBox.Show(this, objam.AdvertisingMagzines_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvMagzine.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvMagzine.SelectedIndex > -1)
        {
            tblMagzine.Visible = true;

            txtMagzine.Text = gvMagzine.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvMagzine.SelectedRow.Cells[3].Text;

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



    protected void lbtnMagzineName_Click(object sender, EventArgs e)
    {
        tblMagzine.Visible = false;
        LinkButton lbtnMagzineName;
        lbtnMagzineName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnMagzineName.Parent.Parent;
        gvMagzine.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        if (gvMagzine.SelectedIndex > -1)
        {
            tblMagzine.Visible = true;
            btnSave.Text = "update";
            btnSave.Enabled = false;

            txtMagzine.Text = gvMagzine.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvMagzine.SelectedRow.Cells[3].Text;
        }

    }


    protected void gvMagzine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvMagzine.SelectedIndex > -1)
        {
            try
            {
                SM.AdvertisingMagzines objam = new SM.AdvertisingMagzines();
                objam.AmId = gvMagzine.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objam.AdvertisingMagzines_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvMagzine.DataBind();
                gvMagzine.SelectedIndex = -1;

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

        tblMagzine.Visible = true;

    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvMagzine.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblMagzine.Visible = false;
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
