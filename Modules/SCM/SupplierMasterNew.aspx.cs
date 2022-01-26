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

public partial class Modules_SCM_SupplierMasterNew : basePage
{
    string supplierId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        supplierId = Request.QueryString["supplierId"];
        if (!IsPostBack)
        {
            setControlsVisibility();

            ItemTypes_Fill();
            FillBrand();
            FillCountry();
            if (supplierId == null)
            {
                SCM.ClearControls(this);
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                tblSupDetails.Visible = true;
                btnRefresh.Visible = true;
                btnSave.Visible = true;
                btnClose.Visible = true;
            }
            else
            {
                try
                {
                    SCM.SuppliersMaster objSCM = new SCM.SuppliersMaster();
                    if (objSCM.SuppliersMaster_Select(supplierId) > 0)
                    {
                        tblSupDetails.Visible = true;
                        btnRefresh.Visible = true;
                        btnSave.Visible = true;
                        btnClose.Visible = true;
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        txtSupplierName.Text = objSCM.SupName;
                        txtContactPerson.Text = objSCM.SupContactPerson; ;
                        txtAddress.Text = objSCM.SupAddress; ;
                        txtContactPersonDetails.Text = objSCM.SupContactPersonDetails;
                        txtContactNo1.Text = objSCM.SupPhone;
                        txtContactNo2.Text = objSCM.SupMobile;
                        txtEmail.Text = objSCM.SupEmail;
                        txtFaxNo.Text = objSCM.SupFaxNo;
                        txtPANNo.Text = objSCM.SupPanNo;
                        txtCSTNo.Text = objSCM.SupCstNo;
                        txtVATNo.Text = objSCM.SupVatNo;
                        txtECCNo.Text = objSCM.SupEccNo;
                        txtRanking.Text = objSCM.SupRanking;
                        txtSTNO.Text = objSCM.Stno;
                        ddlBrandName.SelectedValue = objSCM.SupBrand;
                        ddlCountry.SelectedValue = objSCM.CountryId;
                        ddlPOTemplate1.SelectedValue = objSCM.supplier_Template_ID;
                        objSCM.SuppliersApprovalDetails_Select(objSCM.SupId, chklBasisOfApproval);
                        objSCM.SuppliersUnit_Select(objSCM.SupId, gvUnitDetails);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
                finally
                {
                    SCM.Dispose();
                }
            }
           

        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "20");
        btnSave.Enabled = up.add;
        btnDelete.Enabled = up.Delete;

    }

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (supplierId != null)
        {
            try
            {
                SCM.SuppliersMaster objSCM = new SCM.SuppliersMaster();
                objSCM.SupId = supplierId;
                MessageBox.Show(this, objSCM.SuppliersMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                tblSupDetails.Visible = false;
                btnDelete.Attributes.Clear();
                Response.Redirect("SupplierMaster.aspx");
                //gvSupplierDetails.DataBind();
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }

    }
    #endregion

    #region BindSupplierData
    private void BindSupplierData()
    {
        //try
        //{
        //    SCM.SuppliersMaster objSCM = new SCM.SuppliersMaster();
        //    if (objSCM.SuppliersMaster_Select(supplierId) > 0)
        //    {
        //        //  Response.Redirect("SupplierMasterNew.aspx?supplId=" + gvSupplierDetails.SelectedRow.Cells[1].Text);
        //        tblSupDetails.Visible = true;
        //        btnRefresh.Visible = true;
        //        btnSave.Visible = true;
        //        btnClose.Visible = true;
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = true;
        //        txtSupplierName.Text = objSCM.SupName;
        //        txtContactPerson.Text = objSCM.SupContactPerson; ;
        //        txtAddress.Text = objSCM.SupAddress; ;
        //        txtContactPersonDetails.Text = objSCM.SupContactPersonDetails;
        //        txtContactNo1.Text = objSCM.SupPhone;
        //        txtContactNo2.Text = objSCM.SupMobile;
        //        txtEmail.Text = objSCM.SupEmail;
        //        txtFaxNo.Text = objSCM.SupFaxNo;
        //        txtPANNo.Text = objSCM.SupPanNo;
        //        txtCSTNo.Text = objSCM.SupCstNo;
        //        txtVATNo.Text = objSCM.SupVatNo;
        //        txtECCNo.Text = objSCM.SupEccNo;
        //        txtRanking.Text = objSCM.SupRanking;
        //        ddlBrandName.SelectedValue = objSCM.SupBrand;
        //        ddlCountry.SelectedValue = objSCM.CountryId;
        //        //if (objSCM.SupIndigenousForeign == rbIndigenous.Text)
        //        //{
        //        //    rbIndigenous.Checked = true;
        //        //}
        //        //else if (objSCM.SupIndigenousForeign == rbForeign.Text)
        //        //{
        //        //    rbForeign.Checked = true;
        //        //}
        //        objSCM.SuppliersApprovalDetails_Select(objSCM.SupId, chklBasisOfApproval);

        //        ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objSCM.ItemCode);
        //        ddlItemType_SelectedIndexChanged(sender, e);
        //        ddlItemName.SelectedValue = objSCM.ItemCode;
        //       ddlItemName_SelectedIndexChanged(sender, e);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{
        //    SCM.Dispose();
        //}
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemType.ItemType_Select(ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrandName);
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

    #region Fill Country master
    private void FillCountry()
    {
        try
        {
            Masters.Country.Country_Select(ddlCountry);
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

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region ddlItemName_SelectedIndexChanged
    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // Masters.Dispose();
        }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SuppliersMasterSave();
            Response.Redirect("SupplierMaster.aspx");
            tblSupDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            SuppliersMasterUpdate();
            Response.Redirect("SupplierMaster.aspx");
            tblSupDetails.Visible = true;
        }
        //chklBasisOfApproval.ClearSelection();

        // gvSupplierDetails.SelectedIndex = -1;
    }
    #endregion

    #region SuppliersMaster Save
    private void SuppliersMasterSave()
    {
        try
        {
           // SCM.BeginTransaction();
            SCM.SuppliersMaster objSCM = new SCM.SuppliersMaster();
            objSCM.SupName = txtSupplierName.Text;
            objSCM.SupContactPerson = txtContactPerson.Text;
            objSCM.SupAddress = txtAddress.Text;
            objSCM.SupContactPersonDetails = txtContactPersonDetails.Text;
            objSCM.SupPhone = txtContactNo1.Text;
            objSCM.SupMobile = txtContactNo2.Text;
            objSCM.SupEmail = txtEmail.Text;
            objSCM.SupFaxNo = txtFaxNo.Text;
            objSCM.SupPanNo = txtPANNo.Text;
            objSCM.SupCstNo = txtCSTNo.Text;
            objSCM.SupVatNo = txtVATNo.Text;
            objSCM.SupEccNo = txtECCNo.Text;
            objSCM.SupRanking = txtRanking.Text;
            objSCM.SupBrand = ddlBrandName.SelectedValue;
            objSCM.ItemCode = "0";
            objSCM.CountryId = ddlCountry.SelectedValue;
            objSCM.SupIndigenousForeign = "Indigenous";
            objSCM.Stno = txtSTNO.Text;
            objSCM.supplier_Template_ID = ddlPOTemplate1.SelectedValue;

            if (objSCM.SuppliersMaster_Save() == "Data Saved Successfully")
            {
                objSCM.SuppliersApprovalsDetails_Delete(objSCM.SupId);
                for (int i = 0; i < chklBasisOfApproval.Items.Count; i++)
                {
                    if (chklBasisOfApproval.Items[i].Selected == true)
                    {
                        objSCM.SupBasisOfApproval = chklBasisOfApproval.Items[i].Value;
                        objSCM.SuppliersApprovalDetails_Save();
                    }
                }

                objSCM.SuppliersUnits_Delete(objSCM.SupId);
                foreach(GridViewRow gvrow in gvUnitDetails.Rows)
                {
                    objSCM.unitname = gvrow.Cells[4].Text;
                    objSCM.unitaddress = gvrow.Cells[3].Text;
                    objSCM.SuppliersUnit_Save();
                }


                MessageBox.Show(this, "Data Saved Successfully");
            }
            //SCM.CommitTransaction();

        }
        catch (Exception ex)
        {
            //SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //btnDelete.Attributes.Clear();
            //gvSupplierDetails.DataBind();
            SCM.ClearControls(this);
            SCM.Dispose();
        }
    }
    #endregion

    #region SuppliersUpdate
    private void SuppliersMasterUpdate()
    {
        try
        {
            SCM.SuppliersMaster objSCM = new SCM.SuppliersMaster();
            objSCM.SupId = Request.QueryString["supplierId"];
            objSCM.SupName = txtSupplierName.Text;
            objSCM.SupContactPerson = txtContactPerson.Text;
            objSCM.SupAddress = txtAddress.Text;
            objSCM.SupContactPersonDetails = txtContactPersonDetails.Text;
            objSCM.SupPhone = txtContactNo1.Text;
            objSCM.SupMobile = txtContactNo2.Text;
            objSCM.SupEmail = txtEmail.Text;
            objSCM.SupFaxNo = txtFaxNo.Text;
            objSCM.SupPanNo = txtPANNo.Text;
            objSCM.SupCstNo = txtCSTNo.Text;
            objSCM.SupVatNo = txtVATNo.Text;
            objSCM.SupEccNo = txtECCNo.Text;
            objSCM.SupRanking = txtRanking.Text;
            objSCM.SupBrand = ddlBrandName.SelectedItem.Value;
            objSCM.ItemCode = "0";
            objSCM.CountryId = ddlCountry.SelectedItem.Value;
            objSCM.SupIndigenousForeign = "Indigenous";
            objSCM.Stno = txtSTNO.Text;
            objSCM.supplier_Template_ID = ddlPOTemplate1.SelectedValue;
           
            if (objSCM.SuppliersMaster_Update() == "Data Updated Successfully")
            {
                objSCM.SuppliersApprovalsDetails_Delete(objSCM.SupId);
                for (int i = 0; i < chklBasisOfApproval.Items.Count; i++)
                {
                    if (chklBasisOfApproval.Items[i].Selected == true)
                    {
                        objSCM.SupBasisOfApproval = chklBasisOfApproval.Items[i].Value;
                        objSCM.SuppliersApprovalDetails_Save();
                    }
                }

                objSCM.SuppliersUnits_Delete(objSCM.SupId);
                foreach (GridViewRow gvrow in gvUnitDetails.Rows)
                {
                    objSCM.unitname = gvrow.Cells[4].Text;
                    objSCM.unitaddress = gvrow.Cells[3].Text;
                    objSCM.SuppliersUnit_Save();
                }

            }
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            
        }
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        btnRefresh.Visible = true;
        btnSave.Visible = true;
        btnClose.Visible = true;
        Masters.ClearControls(this);      
        chklBasisOfApproval.ClearSelection();       
    }
    #endregion
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierMaster.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Label11.Text = ddlBoa1.SelectedValue;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitAddress");
        CustomerUnits.Columns.Add(col);

        if (gvUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvUnitDetails.Rows)
            {
                if (gvUnitDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvUnitDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["UnitName"] = txtUnitName.Text;
                        dr["UnitAddress"] = txtUnitAddress.Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["UnitName"] = gvrow.Cells[4].Text;
                        dr["UnitAddress"] = gvrow.Cells[3].Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["UnitName"] = gvrow.Cells[4].Text;
                    dr["UnitAddress"] = gvrow.Cells[3].Text;
                    CustomerUnits.Rows.Add(dr);
                }
            }
        }

        if (gvUnitDetails.Rows.Count > 0)
        {
            if (gvUnitDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvUnitDetails.Rows)
                {
                    if (gvrow.Cells[4].Text == txtUnitName.Text)
                    {
                        gvUnitDetails.DataSource = CustomerUnits;
                        gvUnitDetails.DataBind();
                        MessageBox.Show(this, "The Unit Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvUnitDetails.SelectedIndex == -1)
        {
            DataRow drnew = CustomerUnits.NewRow();
            drnew["UnitName"] = txtUnitName.Text;
            drnew["UnitAddress"] = txtUnitAddress.Text;
            CustomerUnits.Rows.Add(drnew);
        }
        gvUnitDetails.DataSource = CustomerUnits;
        gvUnitDetails.DataBind();
        gvUnitDetails.SelectedIndex = -1;
        btnunitrefresh_Click(sender, e);
    }
    protected void btnunitrefresh_Click(object sender, EventArgs e)
    {
        txtUnitAddress.Text = "";
        txtUnitName.Text = "";
    }

    protected void gvUnitDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvUnitDetails.Rows[e.RowIndex].Cells[4].Text;
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitAddress");
        CustomerUnits.Columns.Add(col);

        if (gvUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvUnitDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["UnitName"] = gvrow.Cells[4].Text;
                    dr["UnitAddress"] = gvrow.Cells[3].Text;
                    CustomerUnits.Rows.Add(dr);
                }
               
            }
        }
        gvUnitDetails.DataSource = CustomerUnits;
        gvUnitDetails.DataBind();
        btnunitrefresh_Click(sender, e);
    }
    protected void gvUnitDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitAddress");
        CustomerUnits.Columns.Add(col);

        if (gvUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvUnitDetails.Rows)
            {
                DataRow dr = CustomerUnits.NewRow();
                dr["UnitName"] = gvrow.Cells[4].Text;
                dr["UnitAddress"] = gvrow.Cells[3].Text;
                CustomerUnits.Rows.Add(dr);
                if (gvrow.RowIndex == gvUnitDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    txtUnitName.Text = gvrow.Cells[4].Text;
                    txtUnitAddress.Text = gvrow.Cells[3].Text;
                    gvUnitDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvUnitDetails.DataSource = CustomerUnits;
        gvUnitDetails.DataBind();
    }

    protected void gvUnitDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
           // e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }
}
 
