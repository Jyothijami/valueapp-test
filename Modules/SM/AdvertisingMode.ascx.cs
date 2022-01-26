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


public partial class Modules_SM_AdvertisingMode : System.Web.UI.UserControl
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
            AdvertisingSave();

            tblAdvertise.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            AdvertisingUpdate();
            tblAdvertise.Visible = false;
        }
        gvAdvertising.SelectedIndex = -1;
    }
    #endregion

    #region Advertising Save
    private void AdvertisingSave()
    {
        try
        {
            SM.AdvertisingMode objam = new SM.AdvertisingMode();
            objam.AdvmName = txtMode.Text;
            objam.AdvmDesc = txtDescription.Text;

            MessageBox.Show(this, objam.AdvertisingMode_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvAdvertising.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region AdvertisingUpdate
    private void AdvertisingUpdate()
    {
        try
        {
            SM.AdvertisingMode objam = new SM.AdvertisingMode();
            objam.AdvmId = gvAdvertising.SelectedRow.Cells[1].Text;
            objam.AdvmName = txtMode.Text;
            objam.AdvmDesc = txtDescription.Text;
            MessageBox.Show(this, objam.AdvertisingMode_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvAdvertising.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvAdvertising.SelectedIndex > -1)
        {
            tblAdvertise.Visible = true;
            txtMode.Text = gvAdvertising.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvAdvertising.SelectedRow.Cells[3].Text;

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

    protected void lbtnAdvertisingName_Click(object sender, EventArgs e)
    {
        tblAdvertise.Visible = false;
        LinkButton lbtnAdvertisingName;
        lbtnAdvertisingName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnAdvertisingName.Parent.Parent;
        gvAdvertising.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        if (gvAdvertising.SelectedIndex > -1)
        {
            tblAdvertise.Visible = true;
            btnSave.Text = "update";
            btnSave.Enabled = false;
            txtMode.Text = gvAdvertising.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvAdvertising.SelectedRow.Cells[3].Text;
        }


    }
    protected void gvAdvertising_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvAdvertising.SelectedIndex > -1)
        {
            try
            {
                SM.AdvertisingMode objam = new SM.AdvertisingMode();
                objam.AdvmId = gvAdvertising.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objam.AdvertisingMode_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvAdvertising.DataBind();
                gvAdvertising.SelectedIndex = -1;

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

        tblAdvertise.Visible = true;

    }
    #endregion


    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvAdvertising.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblAdvertise.Visible = false;
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
