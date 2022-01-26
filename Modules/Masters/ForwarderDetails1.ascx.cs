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

public partial class Modules_Masters_ForwarderDetails : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        tblCurrencyDetails.Visible = true;
        btnSave.Text = "Save";
        Masters.ClearControls(this);

    }


    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvForwarderDetails.SelectedIndex > -1)
        {
            tblCurrencyDetails.Visible = true;
            txtAddress.Text = gvForwarderDetails.SelectedRow.Cells[4].Text;
            txtEmail.Text = gvForwarderDetails.SelectedRow.Cells[5].Text;
            txtForwarderName.Text = gvForwarderDetails.SelectedRow.Cells[0].Text;
            txtPhone.Text = gvForwarderDetails.SelectedRow.Cells[3].Text;

            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvForwarderDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.ForwardedDetails objMaster = new Masters.ForwardedDetails();
                objMaster.Forid = gvForwarderDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Forwarder_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvForwarderDetails.DataBind();
                gvForwarderDetails.SelectedIndex = -1;

                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ForwardDetailsSave();
            tblCurrencyDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            //ForwardDetailsUpdate();
            tblCurrencyDetails.Visible = false;
        }
        gvForwarderDetails.SelectedIndex = -1;
    }

    private void ForwardDetailsUpdate()
    {
        try
        {
            Masters.ForwardedDetails objMaster = new Masters.ForwardedDetails();
            objMaster.Forname = txtForwarderName.Text;
            objMaster.Forphone = txtPhone.Text;
            objMaster.Foraddress = txtAddress.Text;
            objMaster.ForEmail = txtEmail.Text;
            objMaster.Forphone = txtPhone.Text;
            objMaster.Forid = gvForwarderDetails.SelectedRow.Cells[1].Text;
            MessageBox.Show(this, objMaster.Forwarddetails_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvForwarderDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }

    private void ForwardDetailsSave()
    {
        try
        {
            Masters.ForwardedDetails objMaster = new Masters.ForwardedDetails();
            objMaster.Forname = txtForwarderName.Text;
            objMaster.Forphone = txtPhone.Text;
            objMaster.Foraddress = txtAddress.Text;
            objMaster.ForEmail = txtEmail.Text;
            objMaster.Forphone = txtPhone.Text;

            MessageBox.Show(this, objMaster.ForwardDetails_Save());
        }

        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvForwarderDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCurrencyDetails.Visible = false;
        Masters.ClearControls(this);
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvForwarderDetails.DataBind();
    }
    protected void lbtnCurrencyName_Click(object sender, EventArgs e)
    {
        tblCurrencyDetails.Visible = false;
        LinkButton lbtnCurrencyName;
        lbtnCurrencyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCurrencyName.Parent.Parent;
        gvForwarderDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }

    protected void gvForwarderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}
