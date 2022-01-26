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

public partial class Modules_Masters_InsuranceMaster : System.Web.UI.UserControl
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
        tblDespatchDetails.Visible = true;
        btnSave.Text = "Save";
        Masters.ClearControls(this);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvDespatchDetails.SelectedIndex > -1)
        {
            tblDespatchDetails.Visible = true;
            txtInsurancecompany.Text = gvDespatchDetails.SelectedRow.Cells[0].Text;
            txtAddress.Text = gvDespatchDetails.SelectedRow.Cells[3].Text;
           
            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvDespatchDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.InsuranceMaster objMaster = new Masters.InsuranceMaster();
                objMaster.Insid = gvDespatchDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.RegisterMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvDespatchDetails.DataBind();
                gvDespatchDetails.SelectedIndex = -1;

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
            InsSave();
            tblDespatchDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            InsUpdate();
            tblDespatchDetails.Visible = false;
        }
        gvDespatchDetails.SelectedIndex = -1;
    }

    private void InsUpdate()
    {
        try
        {
            Masters.InsuranceMaster objMaster = new Masters.InsuranceMaster();
            objMaster.InsName = txtInsurancecompany.Text;
            objMaster.Insaddress = txtAddress.Text;
           
            objMaster.Insid = gvDespatchDetails.SelectedRow.Cells[1].Text;
            MessageBox.Show(this, objMaster.InsuranceMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvDespatchDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }

    private void InsSave()
    {
        try
        {
            Masters.InsuranceMaster objMaster = new Masters.InsuranceMaster();
            objMaster.InsName = txtInsurancecompany.Text;
            objMaster.Insaddress = txtAddress.Text;
           

            MessageBox.Show(this, objMaster.InsuranceMaster_Save());
        }

        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvDespatchDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
       
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblDespatchDetails.Visible = false;
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvDespatchDetails.DataBind();
    }

    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    protected void lbtnDespatchName_Click(object sender, EventArgs e)
    {
        tblDespatchDetails.Visible = false;
        LinkButton lbtnCountryName;
        lbtnCountryName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCountryName.Parent.Parent;
        gvDespatchDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    protected void gvDespatchDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
}
