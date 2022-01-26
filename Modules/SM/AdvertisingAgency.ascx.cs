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


public partial class Modules_SM_AdvertisingAgency : System.Web.UI.UserControl
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
            AgencySave();

            tblAgency.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            AgencyUpdate();
            tblAgency.Visible = false;
        }

        gvAgency.SelectedIndex = -1;
    }
    #endregion

    #region AgencySave
    private void AgencySave()
    {
        try
        {
            SM.AdvertisingAgency objam = new SM.AdvertisingAgency();
            objam.AaName = txtAgency.Text;
            objam.AaDesc = txtDescription.Text;

            MessageBox.Show(this, objam.AdvertisingAgency_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvAgency.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region AgencyUpdate
    private void AgencyUpdate()
    {
        try
        {
            SM.AdvertisingAgency objam = new SM.AdvertisingAgency();
            objam.AaId = gvAgency.SelectedRow.Cells[1].Text;
            objam.AaName = txtAgency.Text;
            objam.AaDesc = txtDescription.Text;
            MessageBox.Show(this, objam.AdvertisingAgency_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvAgency.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvAgency.SelectedIndex > -1)
        {
            tblAgency.Visible = true;
            btnRefresh.Visible = true;
            btnSave.Text = "Update";
            btnSave.Enabled = true;
            txtAgency.Text = gvAgency.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvAgency.SelectedRow.Cells[3].Text;

            //btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion


    protected void lbtnAdvertisingAgencyName_Click(object sender, EventArgs e)
    {
        tblAgency.Visible = false;
        LinkButton lbtnAdvertisingAgencyName;
        lbtnAdvertisingAgencyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnAdvertisingAgencyName.Parent.Parent;
        gvAgency.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        if (gvAgency.SelectedIndex > -1)
        {
            tblAgency.Visible = true;
            btnSave.Text = "update";
            btnSave.Enabled = false;
            txtAgency.Text = gvAgency.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvAgency.SelectedRow.Cells[3].Text;
        }
    }



    protected void gvAgency_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }

    }

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvAgency.SelectedIndex > -1)
        {
            try
            {
                SM.AdvertisingAgency objam = new SM.AdvertisingAgency();
                objam.AaId = gvAgency.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objam.AdvertisingAgency_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvAgency.DataBind();
                gvAgency.SelectedIndex = -1;

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

        tblAgency.Visible = true;

    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvAgency.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblAgency.Visible = false;
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
