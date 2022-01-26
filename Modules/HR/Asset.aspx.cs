using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_HR_Asset : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlReportingTo);
            CategoryType();

            txtAssetNo.Text = HR.Asset.Asset_AutoGenCode();

        }
    }

    #region Fill CategoryType
    private void CategoryType()
    {
        try
        {
            Masters.ItemCategory.AssetCategory_Select(ddlAssetCategory);
            //Masters.ItemType.AssetType_Select(ddlProductType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvAsset.DataBind();
    }
    #endregion
    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion 

    #region Link Button ItemTypeName_Click
    protected void lbtnAssetName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnAssetName;
        lbtnAssetName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnAssetName.Parent.Parent;
        gvAsset.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        pnlAssetDet.Visible = false;
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            AssetSave();
        }
        else if (btnSave.Text == "Update")
        {
            AssetUpdate();
        }
    }

    private void AssetUpdate()
    {
        try
        {
            if (gvAsset.SelectedIndex > -1)
            {


                HR.Asset obj = new HR.Asset();
                obj.Asset_No = txtAssetNo.Text;
                obj.Category_Id = ddlAssetCategory.SelectedItem.Value;
                obj.IT_Type_Id = ddlProductType.SelectedItem.Value;
                obj.Product_Name = txtProduct.Text;
                obj.Description = txtDesc.Text;
                obj.Manufacturer = txtManfacturer.Text;
                obj.Vendor = txtVendor.Text;
                obj.PONo = txtPONo.Text;
                obj.PO_Dt = General.toMMDDYYYY(txtPoDt.Text);
                obj.Warrenty = txtWarrenty.Text;
                obj.Expiry_Dt = General.toMMDDYYYY(txtExpiryDt.Text);
                obj.Cost = txtCost.Text;
                obj.Discount = txtDisc.Text;
                obj.TotalCost = txtTotalCost.Text;
                obj.Barcode = txtBarcode.Text;
                obj.Asset_ManagedBy = ddlReportingTo.SelectedItem.Value;
                obj.Remarks = txtRemarks.Text;
                obj.Updated_Dt = DateTime.Now.ToString();
                obj.Status = ddlStatus.Text;
                obj.location = ddlLocation.SelectedItem.Text;
                obj.Asset_Id = gvAsset.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, obj.Asset_Update());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            pnlAssetDet.Visible = false;

            gvAsset.DataBind();

        }
    }

    #region AssetSave
    private void AssetSave()
    {
        try
        {
            HR.Asset obj = new HR.Asset();
            obj.Asset_No = txtAssetNo.Text;
            obj.Category_Id = ddlAssetCategory.SelectedItem.Value;
            obj.IT_Type_Id = ddlProductType.SelectedItem.Value;
            obj.Product_Name = txtProduct.Text;
            obj.Description = txtDesc.Text;
            obj.Manufacturer = txtManfacturer.Text;
            obj.Vendor = txtVendor.Text;
            obj.PONo = txtPONo.Text;
            obj.PO_Dt = General.toMMDDYYYY(txtPoDt.Text);
            obj.Warrenty = txtWarrenty.Text;
            obj.Expiry_Dt = General.toMMDDYYYY(txtExpiryDt.Text);
            obj.Cost = txtCost.Text;
            obj.Discount = txtDisc.Text;
            obj.TotalCost = txtTotalCost.Text;
            obj.Barcode = txtBarcode.Text;
            obj.Asset_ManagedBy = ddlReportingTo.SelectedItem.Value;
            obj.Remarks = txtRemarks.Text;
            obj.Updated_Dt = DateTime.Now.ToString();
            obj.Status = ddlStatus.Text;
            obj.location = ddlLocation.SelectedItem.Text;
            MessageBox.Show(this, obj.Asset_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            pnlAssetDet.Visible = false;

            gvAsset.DataBind();
            
        }
    }
    #endregion
    protected void ddlAssetCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemType.AssetTypeCategory_Select(ddlProductType , ddlAssetCategory.SelectedValue);

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlAssetDet.Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvAsset.SelectedIndex > -1)
        {
            HR.Asset obj = new HR.Asset();
            pnlAssetDet.Visible = true;
            
            btnSave.Text = "Update";

            if (obj.Asset_Select(gvAsset .SelectedRow .Cells [0].Text ) > 0)
            {
                txtAssetNo.Text = obj.Asset_No;
                txtProduct.Text = obj.Product_Name;
                ddlStatus.SelectedValue = obj.Status;
                txtManfacturer.Text = obj.Manufacturer;
                txtVendor.Text = obj.Vendor;
                ddlAssetCategory.SelectedValue = obj.Category_Id;
                ddlAssetCategory_SelectedIndexChanged(sender, e);
                ddlProductType.SelectedValue = obj.IT_Type_Id;
                txtPONo.Text = obj.PONo;
                txtPoDt.Text = obj.PO_Dt;
                txtWarrenty.Text = obj.Warrenty;
                txtExpiryDt.Text = obj.Expiry_Dt;
                txtCost.Text = obj.Cost;
                txtDisc.Text = obj.Discount;
                txtDesc.Text = obj.Description;
                txtTotalCost.Text = obj.TotalCost;
                txtBarcode.Text = obj.Barcode;
                txtRemarks.Text = obj.Remarks;
                ddlLocation.SelectedItem.Text = obj.location;
                ddlReportingTo.SelectedValue = obj.Asset_ManagedBy;
            }

           
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvAsset.SelectedIndex > -1)
        {

            try
            {
                HR.Asset objMaster = new HR.Asset();
                objMaster.Asset_Id  = gvAsset.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objMaster.Asset_Delete(objMaster .Asset_Id ));

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                pnlAssetDet.Visible = false;

                gvAsset.DataBind();
                gvAsset.SelectedIndex = -1;
                
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
}