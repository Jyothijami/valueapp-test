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

public partial class Modules_Masters_CountryMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;
    #region Page_load
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

    #region New_Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblCountryDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtCountry);
    }
    #endregion

    #region btnEdit_Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCountryDetails.SelectedIndex > -1)
        {
            tblCountryDetails.Visible = true;
            txtCountry.Text = gvCountryDetails.SelectedRow.Cells[0].Text;
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

        if (gvCountryDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.Country objMaster = new Masters.Country ();
                objMaster.CountryId = gvCountryDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Country_Delete ());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblCountryDetails.Visible = false;

                gvCountryDetails.DataBind();
                gvCountryDetails.SelectedIndex = -1;

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

    #region Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            CountrySave ();
            tblCountryDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            CountryUpdate ();
            tblCountryDetails.Visible = false;
        }
        gvCountryDetails.SelectedIndex = -1;
    }
    #endregion

    #region CountrySave
    private void CountrySave()
    {
        try
        {
            Masters.Country objMaster = new Masters.Country ();
            objMaster.CountryName = txtCountry.Text;
            MessageBox.Show(this, objMaster.Country_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvCountryDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region CountryUpdate
    private void CountryUpdate()
    {
        try
        {
            Masters.Country objMaster = new Masters.Country ();
            objMaster.CountryId = gvCountryDetails.SelectedRow.Cells[1].Text;
            objMaster.CountryName = txtCountry.Text;
            
            MessageBox.Show(this, objMaster.Country_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvCountryDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion   

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCountryDetails.Visible = false;
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
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion

    #region Go
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvCountryDetails.DataBind();
    }
    #endregion

    #region RowDataBound
    protected void gvCountryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region lbtnCountryName
    protected void lbtnCountryName_Click(object sender, EventArgs e)
    {
        tblCountryDetails.Visible = false;
        LinkButton lbtnCountryName;
        lbtnCountryName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCountryName.Parent.Parent;
        gvCountryDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion
}
