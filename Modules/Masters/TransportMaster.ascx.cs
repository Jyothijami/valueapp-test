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


public partial class Modules_Masters_TransportMaster : System.Web.UI.UserControl
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
            TransporterSave();
            tblTransporterDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            TransporterUpdate();
            tblTransporterDetails.Visible = false;
        }
        gvTransporterDetails.SelectedIndex = -1;
    }
    #endregion

    #region TransporterSave
    private void TransporterSave()
    {
        try
        {
            Masters.TrasnporterMaster objMaster = new Masters.TrasnporterMaster();
            objMaster.TransContactPerson = txtContactPersonName.Text;
            objMaster.TransAddr = txtAddress.Text;
            objMaster.TransLongName = txtTransportLongName.Text;
            objMaster.TransContactNo = txtContactNo.Text;
            objMaster.TransMobileNo = txtMobileNo.Text;

            MessageBox.Show(this, objMaster.TransporterMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvTransporterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region TransporterUpdate
    private void TransporterUpdate()
    {
        try
        {
            Masters.TrasnporterMaster objMaster = new Masters.TrasnporterMaster();
            objMaster.TransId = gvTransporterDetails.SelectedRow.Cells[1].Text;
            objMaster.TransContactPerson = txtContactPersonName.Text;
            objMaster.TransAddr = txtAddress.Text;
            objMaster.TransLongName = txtTransportLongName.Text;
            objMaster.TransContactNo = txtContactNo.Text;
            objMaster.TransMobileNo = txtMobileNo.Text;
            MessageBox.Show(this, objMaster.TransporterMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvTransporterDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvTransporterDetails_RowDataBound
    protected void gvTransporterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button TransportName_Click
    protected void lbtnTranportName_Click(object sender, EventArgs e)
    {
        tblTransporterDetails.Visible = false;
        LinkButton lbtnTranportName;
        lbtnTranportName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnTranportName.Parent.Parent;
        gvTransporterDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        if (gvTransporterDetails.SelectedIndex > -1)
        {
            tblTransporterDetails.Visible = true;
            txtContactPersonName.Text = gvTransporterDetails.SelectedRow.Cells[3].Text;
            txtAddress.Text = gvTransporterDetails.SelectedRow.Cells[4].Text;
            txtTransportLongName.Text = gvTransporterDetails.SelectedRow.Cells[0].Text;
            txtContactNo.Text = gvTransporterDetails.SelectedRow.Cells[5].Text;
            txtMobileNo.Text = gvTransporterDetails.SelectedRow.Cells[6].Text;
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
        if (gvTransporterDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.TrasnporterMaster objMaster = new Masters.TrasnporterMaster();
                objMaster.TransId = gvTransporterDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.TransporterMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblTransporterDetails.Visible = false;
               
                gvTransporterDetails.DataBind();
                gvTransporterDetails.SelectedIndex = -1;

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
        tblTransporterDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtTransportLongName);
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
        tblTransporterDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvTransporterDetails.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
