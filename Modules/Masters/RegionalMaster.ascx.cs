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

public partial class Modules_Masters_RegionalMaster : System.Web.UI.UserControl
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

    protected void RandomGenerator()
    {
        Random RandomGenerator = null;
        RandomGenerator = new Random();
        int rand = RandomGenerator.Next(0001, 99999999);
        lblRegId.Text = rand.ToString();
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
            RandomGenerator();
            RegionalMasterSave();
            tblRegionalMasterDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            RegionalMasterUpdate();
            tblRegionalMasterDetails.Visible = false;
        }
        gvRegionalMasterDetails.SelectedIndex = -1;
    }
    #endregion

    #region RegionalMasterSave
    private void RegionalMasterSave()
    {
        try
        {
            Masters.RegionalMaster objMaster = new Masters.RegionalMaster();
            objMaster.RegId = lblRegId.Text;
            objMaster.RegName = txtRegionalCode.Text;
            objMaster.RegDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.RegionalMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvRegionalMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region RegionalMasterUpdate
    private void RegionalMasterUpdate()
    {
        try
        {
            Masters.RegionalMaster objMaster = new Masters.RegionalMaster();
            objMaster.RegId = gvRegionalMasterDetails.SelectedRow.Cells[1].Text;
            objMaster.RegName = txtRegionalCode.Text;
            objMaster.RegDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.RegionalMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvRegionalMasterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvRegionalMasterDetails_RowDataBound
    protected void gvRegionalMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button RegionalMaster_Click
    protected void lbtnRegionalMaster_Click(object sender, EventArgs e)
    {
        tblRegionalMasterDetails.Visible = false;
        LinkButton lbtnRegionalMaster;
        lbtnRegionalMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnRegionalMaster.Parent.Parent;
        gvRegionalMasterDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion


    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvRegionalMasterDetails.SelectedIndex > -1)
        {
            tblRegionalMasterDetails.Visible = true;
            lblRegId.Text = gvRegionalMasterDetails.SelectedRow.Cells[1].Text;
            txtRegionalCode.Text = gvRegionalMasterDetails.SelectedRow.Cells[0].Text;
            txtDescription.Text = gvRegionalMasterDetails.SelectedRow.Cells[3].Text;
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
        if (gvRegionalMasterDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.RegionalMaster objMaster = new Masters.RegionalMaster();
                objMaster.RegId = gvRegionalMasterDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.RegionalMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblRegionalMasterDetails.Visible = false;
               
                gvRegionalMasterDetails.DataBind();
                gvRegionalMasterDetails.SelectedIndex = -1;

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
        tblRegionalMasterDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtRegionalCode);
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
        tblRegionalMasterDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvRegionalMasterDetails.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
