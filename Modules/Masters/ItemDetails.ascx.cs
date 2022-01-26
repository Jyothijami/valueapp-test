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

public partial class Modules_Masters_ItemDetails : System.Web.UI.UserControl
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

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
            ItemDetailsSave();
            tblItemDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            ItemDetailsUpdate();
            tblItemDetails.Visible = false;
        }
        gvItemDetails.SelectedIndex = -1;
    }
    #endregion

    #region ItemDetailsSave
    private void ItemDetailsSave()
    {
        try
        {
            Masters.ItemDetails objMaster = new Masters.ItemDetails();
            objMaster.ItemCode = ddlItemCode.SelectedItem.Value;
            objMaster.ItemDetManufacturer = txtManufacturing.Text;
            objMaster.ItemDetMfgDate = txtManufacturingDate.Text;
            objMaster.ItemDetExpDate = txtExpireDate.Text;
            objMaster.ItemDetBatchNo = txtBatchNo.Text;
           
            MessageBox.Show(this, objMaster.ItemDetails_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvItemDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ItemDetails Update
    private void ItemDetailsUpdate()
    {
        try
        {
            Masters.ItemDetails objMaster = new Masters.ItemDetails();
            objMaster.ItemDetId = gvItemDetails.SelectedRow.Cells[1].Text;
            objMaster.ItemCode = ddlItemCode.SelectedItem.Value;
            objMaster.ItemDetManufacturer = txtManufacturing.Text;
            objMaster.ItemDetMfgDate = txtManufacturingDate.Text;
            objMaster.ItemDetExpDate = txtExpireDate.Text;
            objMaster.ItemDetBatchNo = txtBatchNo.Text;
           

            MessageBox.Show(this, objMaster.ItemDetails_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvItemDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvItemDetails_RowDataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button ItemCode_Click
    protected void lbtnItemCode_Click(object sender, EventArgs e)
    {
        tblItemDetails.Visible = false;
        LinkButton lbtnItemCode;
        lbtnItemCode = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnItemCode.Parent.Parent;
        gvItemDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvItemDetails.SelectedIndex > -1)
        {
            tblItemDetails.Visible = true;
            ddlItemCode.SelectedValue = gvItemDetails.SelectedRow.Cells[2].Text;
            txtManufacturing.Text = gvItemDetails.SelectedRow.Cells[3].Text;
            txtManufacturingDate.Text = gvItemDetails.SelectedRow.Cells[4].Text;
            txtExpireDate.Text = gvItemDetails.SelectedRow.Cells[5].Text;
            txtBatchNo.Text = gvItemDetails.SelectedRow.Cells[6].Text;

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
        if (gvItemDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.ItemDetails objMaster = new Masters.ItemDetails();
                objMaster.ItemDetId = gvItemDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.ItemDetails_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblItemDetails.Visible = false;
               
                gvItemDetails.DataBind();
                gvItemDetails.SelectedIndex = -1;

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
        tblItemDetails.Visible = true;
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
        tblItemDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvItemDetails.DataBind();
    }
    #endregion





    
} 
