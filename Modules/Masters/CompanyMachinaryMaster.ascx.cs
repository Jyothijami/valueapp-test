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

public partial class Modules_Masters_CompanyMachinaryMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;
    
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }
    #endregion

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
            CompMachinarySave();
            tblMachinaryDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            CompMachinaryUpdate();
            tblMachinaryDetails.Visible = false;
        }
        gvCompMachinary.SelectedIndex = -1;
    }
    #endregion

    #region CompMachinarySave
    private void CompMachinarySave()
    {
        try
        {
            Masters.CompMachinaryMaster objMaster = new Masters.CompMachinaryMaster();
            objMaster.CMId = txtMachineCode.Text;
            objMaster.CMMachineName = txtMachineName.Text;
            objMaster.CMManufactName = txtManufacturerName.Text;
            objMaster.CMInvoiceNo = txtInvoiceNo.Text;
            objMaster.CMWarrenty = txtWarrantyDetails.Text;
            objMaster.CMInstDate = txtInstallationDate.Text;
            objMaster.CMDesc = txtDescription.Text;
            objMaster.CMManufactDate = txtManufacturingDate.Text;
            MessageBox.Show(this, objMaster.CompMachinaryMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvCompMachinary.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region CompMachinaryUpdate
    private void CompMachinaryUpdate()
    {
        try
        {
            Masters.CompMachinaryMaster objMaster = new Masters.CompMachinaryMaster();
            objMaster.CMId = gvCompMachinary.SelectedRow.Cells[1].Text;
            objMaster.CMMachineName = txtMachineName.Text;
            objMaster.CMManufactName = txtManufacturerName.Text;
            objMaster.CMInvoiceNo = txtInvoiceNo.Text;
            objMaster.CMWarrenty = txtWarrantyDetails.Text;
            objMaster.CMInstDate = txtInstallationDate.Text;
            objMaster.CMDesc = txtDescription.Text;
            objMaster.CMManufactDate = txtManufacturingDate.Text;

            MessageBox.Show(this, objMaster.CompMachinaryMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvCompMachinary.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvCompMachinary_RowDataBound
    protected void gvCompMachinary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
    }
    #endregion

    #region Link Button CompMachinary_Click
    protected void lbtnCompMachinary_Click(object sender, EventArgs e)
    {
        tblMachinaryDetails.Visible = false;
        LinkButton lbtnCompMachinary;
        lbtnCompMachinary = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompMachinary.Parent.Parent;
        gvCompMachinary.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCompMachinary.SelectedIndex > -1)
        {
            txtMachineCode.Text = gvCompMachinary.SelectedRow.Cells[1].Text;
            txtMachineName.Text = gvCompMachinary.SelectedRow.Cells[0].Text;
            txtManufacturerName.Text = gvCompMachinary.SelectedRow.Cells[3].Text;
            txtInvoiceNo.Text = gvCompMachinary.SelectedRow.Cells[8].Text;
            txtWarrantyDetails.Text = gvCompMachinary.SelectedRow.Cells[5].Text;
            txtInstallationDate.Text = gvCompMachinary.SelectedRow.Cells[6].Text;
            txtDescription.Text = gvCompMachinary.SelectedRow.Cells[4].Text;
            txtManufacturingDate.Text = gvCompMachinary.SelectedRow.Cells[7].Text;

            tblMachinaryDetails.Visible = true;
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
        if (gvCompMachinary.SelectedIndex > -1)
        {
            try
            {
                Masters.CompMachinaryMaster objMaster = new Masters.CompMachinaryMaster();
                objMaster.CMId = gvCompMachinary.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.CompMachinaryMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblMachinaryDetails.Visible = false;
               
                gvCompMachinary.DataBind();
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
        try
        {
            Masters.ClearControls(this);
            btnSave.Text = "Save";
            txtMachineCode.Text = Masters.CompMachinaryMaster.CompMachinaryMaster_AutoGenCode();
            tblMachinaryDetails.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            Masters.Dispose();
            ScriptManagerLocal.SetFocus(txtMachineName);
        }
    }
    #endregion

    #region  Button Refresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblMachinaryDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvCompMachinary.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        txtSearchValueFromDate.Text = "";
        if (ddlSearchBy.SelectedItem.Text == "Installation Date" || ddlSearchBy.SelectedItem.Text == "Manufacture Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            imgFromDate.Visible = true;
            ceSearchFrom.Enabled = true;
            MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
        }
    }
}
