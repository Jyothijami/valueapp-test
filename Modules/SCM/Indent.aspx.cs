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

public partial class Modules_SCM_Indent : System.Web.UI.Page
{


    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack && !IsCallback)
        {
           
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            gvIndentDetails.DataBind();
          /// txtQuantity.Attributes.Add("onkeyup", "javascript:balqtycalc();"); 
             SalesOrder_Fill();
            CustomerName_Fill();
            ItemTypes_Fill();
            //tblPoDetails.Visible = false;
          
        }
        

    }
    #endregion

    #region Sales Order Fill
    private void SalesOrder_Fill()
    {
        try
        {

            SM.SalesOrder.SalesOrder_Select(ddlOrderAcceptance);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Sales Order Master Fill
    private void SalesOrderMaster_Fill()
    {
        try
        {
            SM.SalesOrder.CusID_Select(ddlOrderAcceptance, ddlSupplierName.SelectedItem.Value);
            //SM.SalesOrder.SalesOrder_Select(ddlOrderAcceptance);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Department Fill
    private void Department_Fill()
    {
        try
        {
            Masters.Department.Department_Select(ddlDepartment);
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

    
    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemType);
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

    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerNameIndent(ddlSupplierName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlFollowUp);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    #region Link Button IndentNo_Click
    protected void lbtnIndentNo_Click(object sender, EventArgs e)
    {
        tblIndentDetails.Visible = false;
        LinkButton lbtnIndentNo;
        lbtnIndentNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIndentNo.Parent.Parent;
        gvIndentDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        try
        {
            SCM.Indent objIndent = new SCM.Indent();

            if (objIndent.Indent_Select(gvIndentDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = false;
                tblIndentDetails.Visible = true;
                txtIndentNo.Text = objIndent.INDNo;
                txtIndentDate.Text = objIndent.INDDate;
                ddlDepartment.SelectedValue = objIndent.DeptId;
                ddlFollowUp.SelectedValue = objIndent.FollowUp;
                ddlPreparedBy.SelectedValue = objIndent.INDPreparedBy;
                ddlApprovedBy.SelectedValue = objIndent.INDApprovedBy;

                objIndent.IndentDetails_Select(gvIndentDetails.SelectedRow.Cells[0].Text, gvItemDetails);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            SCM.Dispose();
        }

    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        tblIndentDetails.Visible = true;
        SCM.ClearControls(this);
        Department_Fill();
        EmployeeMaster_Fill();
       
      //  SalesOrder_Fill();
        txtIndentNo.Text = SCM.Indent.Indent_AutoGenCode();
        txtIndentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        // ddlDepartment.SelectedItem = SCM.Indent.Indent_Select();
        // ddlFollowUp.SelectedItem = SCM.Indent.Indent_Select();
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        gvItemDetails.DataBind();
        gvOrderAcceptanceItems.DataBind();
        txtReqByDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            txtIndentNo.Text = SCM.Indent.Indent_AutoGenCode();
            IndentSave();
        }
        else if (btnSave.Text == "Update")
        {
            IndentUpdate();
        }
    }
    #endregion

    #region IndentSave
    private void IndentSave()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.Indent objSCM = new SCM.Indent();
                SCM.BeginTransaction();

                objSCM.INDDate = Yantra.Classes.General.toMMDDYYYY(txtIndentDate.Text);
                objSCM.DeptId = ddlDepartment.SelectedItem.Value;

                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                objSCM.Status = "New";

                objSCM.CP_ID = cp.getPresentCompanySessionValue();
                 objSCM.INDPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                 objSCM.INDApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                //objSCM.INDApprovedBy = ddlApprovedBy.SelectedItem.Value;
                if (objSCM.Indent_Save() == "Data Saved Successfully")
                {
                    objSCM.IndentDetails_Delete(objSCM.INDId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.INDItemCode = gvrow.Cells[2].Text;
                        objSCM.INDDetQty = gvrow.Cells[6].Text;
                        objSCM.INDDetPriority = gvrow.Cells[7].Text;
                        objSCM.INDDetBrand = gvrow.Cells[8].Text;
                        objSCM.INDDetSuggParty = gvrow.Cells[10].Text;
                        objSCM.INDDetReqFor = gvrow.Cells[9].Text;
                        objSCM.INDDetReqByDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[11].Text);
                        objSCM.INDDetSpecs = gvrow.Cells[12].Text;
                        objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                        objSCM.IndColor = gvrow.Cells[15].Text;
                        objSCM.INDDetRemQty = gvrow.Cells[6].Text;
                        objSCM.IndentDetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvIndentDetails.DataBind();

                gvItemDetails.DataBind();
                gvOrderAcceptanceItems.DataBind();
                tblIndentDetails.Visible = false;
                SCM.Dispose();
                SCM.ClearControls(this);
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Indent");
        }
    }
    #endregion

    #region IndentUpdate
    private void IndentUpdate()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.Indent objSCM = new SCM.Indent();

                SCM.BeginTransaction();

                objSCM.INDId = gvIndentDetails.SelectedRow.Cells[0].Text;
                objSCM.INDNo = txtIndentNo.Text;
                objSCM.INDDate = Yantra.Classes.General.toMMDDYYYY(txtIndentDate.Text);
                objSCM.DeptId = ddlDepartment.SelectedItem.Value;

                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                //objSCM.INDApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objSCM.Indent_Update() == "Data Updated Successfully")
                {
                    objSCM.IndentDetails_Delete(objSCM.INDId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.INDItemCode = gvrow.Cells[2].Text;
                        objSCM.INDDetQty = gvrow.Cells[6].Text;
                        objSCM.INDDetPriority = gvrow.Cells[7].Text;
                        objSCM.INDDetBrand = gvrow.Cells[8].Text;
                        objSCM.INDDetSuggParty = gvrow.Cells[9].Text;
                        objSCM.INDDetReqFor = gvrow.Cells[10].Text;
                        objSCM.INDDetReqByDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[11].Text);
                        objSCM.INDDetSpecs = gvrow.Cells[12].Text;
                        objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                        objSCM.IndColor = gvrow.Cells[15].Text;

                        objSCM.IndentDetails_Save();
                    }
                    SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                btnDelete.Attributes.Clear();
                gvIndentDetails.DataBind();

                gvItemDetails.DataBind();
                tblIndentDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Indent");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.Indent objIndent = new SCM.Indent();

                if (objIndent.Indent_Select(gvIndentDetails.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblIndentDetails.Visible = true;
                    txtIndentNo.Text = objIndent.INDNo;
                    txtIndentDate.Text = objIndent.INDDate;
                    ddlDepartment.SelectedValue = objIndent.DeptId;
                    ddlFollowUp.SelectedValue = objIndent.FollowUp;
                    ddlPreparedBy.SelectedValue = objIndent.INDPreparedBy;
                    ddlApprovedBy.SelectedValue = objIndent.INDApprovedBy;

                    objIndent.IndentDetails_Select(gvIndentDetails.SelectedRow.Cells[0].Text, gvItemDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.Indent objSCM = new SCM.Indent();
                MessageBox.Show(this, objSCM.Indent_Delete(gvIndentDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvIndentDetails.DataBind();
                gvItemDetails.DataBind();
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

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        gvItemDetails.DataBind();

        txtIndentNo.Text = SCM.Indent.Indent_AutoGenCode();
        txtIndentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblIndentDetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        DataTable IndentProducts = new DataTable();

        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Color");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("ColorId");
        IndentProducts.Columns.Add(col);


        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvItemDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
                    {
                        DataRow drnew = IndentProducts.NewRow();
                        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
                        drnew["ItemName"] = ddlItemType.SelectedItem.Text;
                        drnew["ItemType"] = txtModelName.Text;
                        drnew["UOM"] = txtItemUOM.Text;
                        drnew["Quantity"] = txtQuantity.Text;

                        drnew["Color"] = ddlColor.SelectedItem.Text;
                        drnew["ColorId"] = ddlColor.SelectedItem.Value;

                        if (ddlItemPriority.SelectedValue == "0")
                        {
                            drnew["Priority"] = "--";
                        }
                        else
                        {
                            drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
                        }
                        drnew["Brand"] = txtBrand.Text;
                        drnew["SuggestedParty"] = ddlSupplierName.SelectedItem.Text;
                        drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                        drnew["ReqDate"] = txtReqByDate.Text;
                        drnew["Specification"] = txtSpecification.Text;
                        if (txtBrand.Text == string.Empty)
                        {
                            drnew["Brand"] = "--";
                        }
                        else
                        {
                        }
                       
                        if (txtSpecification.Text == string.Empty)
                        {
                            drnew["Specification"] = "--";
                        }
                        else
                        {
                            drnew["Specification"] = txtSpecification.Text;
                        }
                        IndentProducts.Rows.Add(drnew);

                    }
                    else
                    {
                        DataRow dr = IndentProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemName"] = gvrow.Cells[3].Text;
                        dr["ItemType"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Priority"] = gvrow.Cells[7].Text;
                        dr["Brand"] = gvrow.Cells[8].Text;
                        dr["SuggestedParty"] = gvrow.Cells[10].Text;
                        dr["ReqFor"] = gvrow.Cells[9].Text;
                        dr["ReqDate"] = gvrow.Cells[11].Text;
                        dr["Specification"] = gvrow.Cells[12].Text;
                        dr["Color"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;


                        IndentProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["ItemType"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    dr["SuggestedParty"] = gvrow.Cells[10].Text;
                    dr["ReqFor"] = gvrow.Cells[9].Text;
                    dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;


                    IndentProducts.Rows.Add(dr);
                }
            }
        }
        if (gvItemDetails.Rows.Count > 0)
        {
            if (gvItemDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItemDetails.Rows)
                {

                    if (gvrow.Cells[2].Text == ddlItemType.SelectedItem.Value)
                    {
                        gvItemDetails.DataSource = IndentProducts;
                        gvItemDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }

                }
            }
            
        }
        if (gvItemDetails.SelectedIndex == -1)
        {

            DataRow drnew = IndentProducts.NewRow();
            drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
            drnew["ItemName"] = ddlItemType.SelectedItem.Text;
            drnew["ItemType"] = txtModelName.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtQuantity.Text;
            if (ddlItemPriority.SelectedValue == "0")
            {
                drnew["Priority"] = "--";
            }
            else
            {
                drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
            }
            drnew["Brand"] = txtBrand.Text;
            drnew["SuggestedParty"] = ddlSupplierName.SelectedItem.Text;
            drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
            drnew["ReqDate"] = txtReqByDate.Text;
            drnew["Specification"] = txtSpecification.Text;

            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;

            if (txtBrand.Text == string.Empty)
            {
                drnew["Brand"] = "--";
            }
            else
            {
                drnew["Brand"] = txtBrand.Text;


            }           
            if (txtSpecification.Text == string.Empty)
            {
                drnew["Specification"] = "--";
            }
            else
            {
                drnew["Specification"] = txtSpecification.Text;
            }

            IndentProducts.Rows.Add(drnew);
        }
            gvItemDetails.DataSource = IndentProducts;
            gvItemDetails.DataBind();
            btnItemRefresh_Click(sender, e);
            gvItemDetails.SelectedIndex = -1;
        
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        //ddlItemType.SelectedValue = "0";
        txtModelName.Text = string.Empty;
        //ddlItemType.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtQuantityInHand.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtBalanceQty.Text = string.Empty;
        ddlItemPriority.SelectedValue = "0";
        txtBrand.Text = string.Empty;
 
        //txtRequiredFor.Text = string.Empty;
        //txtReqByDate.Text = string.Empty;
        txtSpecification.Text = string.Empty;
        //txtColor.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        //ddlSupplierName.SelectedValue = "0";
        //ddlColor.SelectedValue = "0";
        
        
    }
    #endregion

    //#region ItemCodes Select Index Changed
    //protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemCode.SelectedItem.Value) > 0)
    //        {
    //            txtItemUOM.Text = objMaster.ItemUOMShort;
    //            txtQuantityInHand.Text = objMaster.ItemQtyInHand;

    //            SCM.SuppliersMaster objSCMSM = new SCM.SuppliersMaster();
    //            if (objSCMSM.SuppliersMaster_SelectByItemCode(ddlItemCode.SelectedItem.Value) > 0)
    //            {
    //                txtSupplierName.Text = objSCMSM.SupName;
    //            }
    //            else
    //            {
    //                txtSupplierName.Text = string.Empty;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion

    #region GridView Indent Details Row DataBound
    protected void gvIndentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region GridView Indent Items Row Deleting
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Color");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        IndentProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["ItemType"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    dr["SuggestedParty"] = gvrow.Cells[9].Text;
                    dr["ReqFor"] = gvrow.Cells[10].Text;
                    dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;

                    IndentProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = IndentProducts;
        gvItemDetails.DataBind();
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Indent Date")
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
        txtSearchText.Text = string.Empty;
    }
    #endregion

    #region DropDownList Symbols Select Index Changed
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
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        //if (lblSearchValueHidden.Text == "")
        //{
        //    MessageBox.Show(this, "No Data Exist");
        //}
        //else
        //{
            gvIndentDetails.SelectedIndex = -1;
            lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
            lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
            if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
            else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
            if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
            else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
            gvIndentDetails.DataBind();


        //}
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Indent&indno=" + gvIndentDetails.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    //#region ddlItemType_SelectedIndexChanged
    //protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster.ItemMaster_Select(ddlItemCode, ddlItemType.SelectedItem.Value);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion

    #region gvItemDetails_RowDataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = false;
            //e.Row.Cells[9].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = false;
            //e.Row.Cells[9].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
    }
    #endregion



    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
           
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                
                txtQuantityInHand.Text = objMaster.ItemQtyInHand;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtSpecification.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtSpecification.Text = objMaster.ItemSpec;
               // txtColor.Text = objMaster.Color;
              //  txtSupplierName.Text = objMaster.ItemPrincipalName;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";
                
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedItem.Value);

            
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
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("UOM");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("Brand");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("SuggestedParty");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqDate");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("Color");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        IndentProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = IndentProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["ItemType"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Priority"] = gvrow.Cells[7].Text;
                dr["Brand"] = gvrow.Cells[8].Text;
                dr["SuggestedParty"] = gvrow.Cells[9].Text;
                dr["ReqFor"] = gvrow.Cells[10].Text;
                dr["ReqDate"] = gvrow.Cells[11].Text;
                dr["Specification"] = gvrow.Cells[12].Text;

                dr["Color"] = gvrow.Cells[14].Text;
                dr["ColorId"] = gvrow.Cells[15].Text;


                IndentProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    ddlItemPriority.SelectedValue = gvrow.Cells[7].Text;
                    txtReqByDate.Text = gvrow.Cells[11].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    //txtItemRate.Text = gvrow.Cells[7].Text;
                    //txtRequiredFor.Text = gvrow.Cells[10].Text;
                    ddlSupplierName.SelectedItem.Text = gvrow.Cells[9].Text;
                    txtSpecification.Text = gvrow.Cells[12].Text;
                    ddlColor.SelectedValue = gvrow.Cells[13].Text;
                    ddlColor.SelectedItem.Value = gvrow.Cells[15].Text;

                    gvItemDetails.SelectedIndex = gvrow.RowIndex;

                }
            }
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            Response.Redirect("IndentApprovedList.aspx?indid=" + gvIndentDetails.SelectedRow.Cells[0].Text + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
        //try
        //{

        //    SCM.Indent objSCM = new SCM.Indent();
        //    SCM.BeginTransaction();
        //    objSCM.INDId = gvIndentDetails.SelectedRow.Cells[0].Text;
        //    objSCM.INDApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);


        //    if (objSCM.IndentApprove_Update() == "Status Updated Successfully")
        //        MessageBox.Show(this, "Indent Approved Successfully");                     
        //    SCM.CommitTransaction();
        //}
        //catch (Exception ex)
        //{
        //    SCM.Dispose();
        //    SCM.RollBackTransaction();
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{

        //    SCM.Dispose();
        //    gvIndentDetails.DataBind();
        //    btnClose_Click (sender, e);
        //}
    }
    protected void gvOrderAcceptanceItems_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[6].Text))).ToString();
           
            e.Row.Cells[16].Text = SM.SalesOrder.OrderedQuantity(e.Row.Cells[2].Text, ddlOrderAcceptance.SelectedItem.Value);
            if (e.Row.Cells[16].Text == "")
            {
                e.Row.Cells[16].Text = "0";
            }
            else
            {
                e.Row.Cells[17].Text = Convert.ToString((Convert.ToInt64(e.Row.Cells[6].Text)) - (Convert.ToInt64(e.Row.Cells[16].Text)));
            }
            if (e.Row.Cells[17].Text == "")
            {
                e.Row.Cells[17].Text = e.Row.Cells[6].Text;
            }

            if (e.Row.Cells[16].Text == e.Row.Cells[6].Text)
            {
                e.Row.Visible = false;
               // gvOrderAcceptanceItems.Enabled = false;
            }
            else
            {
                e.Row.Visible = true;
             //   gvOrderAcceptanceItems.Enabled = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = e.Row.Cells[9].Visible = e.Row.Cells[19].Visible = false;
            
        }

       
       
    }
    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.SalesOrder objSO = new SM.SalesOrder();
        objSO.SalesOrderDetails_Select1(ddlOrderAcceptance.SelectedItem.Value, gvOrderAcceptanceItems);

      //  Ordered_Quantity();

    }

    private void Ordered_Quantity()
    {
        foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
        {
            if (gvrow.RowType == DataControlRowType.DataRow)
            {
                gvrow.Cells[16].Text =  SM.SalesOrder.OrderedQuantity(gvrow.Cells[2].Text, ddlOrderAcceptance.SelectedItem.Value );

                
            }

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("ChkItemSelect");
            if (ch.Checked == true)
            {

                DataTable SalesOrderItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Brand");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ReqFor");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Specification");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Priority");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ReqDate");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderItems.Columns.Add(col);


                col = new DataColumn("Color");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderItems.Columns.Add(col);


                if (gvItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
                    {
                        if (gvItemDetails.SelectedIndex > -1)
                        {
                            if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow.Cells[2].Text;
                                dr["ModelNo"] = gvrow.Cells[3].Text;
                                dr["ItemName"] = gvrow.Cells[4].Text;
                                dr["UOM"] = gvrow.Cells[5].Text;
                                dr["Quantity"] = gvrow.Cells[6].Text;
                                dr["Priority"] = "---";
                                dr["Brand"] = gvrow.Cells[15].Text;

                                dr["Specification"] = "---";
                                dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                                dr["ReqDate"] = txtReqByDate.Text;
                                dr["Room"] = "--";
                                //dr["Priority"] = "---";
                                dr["Color"] = gvrow.Cells[18].Text;
                                dr["ColorId"] = gvrow.Cells[19].Text;


                                SalesOrderItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ItemType"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                dr["Quantity"] = gvrow1.Cells[6].Text;
                                //dr["Currency"] = gvrow1.Cells[7].Text;
                                //dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                                dr["Rate"] = gvrow1.Cells[7].Text;
                                dr["Specification"] = "---";
                                //dr["Remarks"] = gvrow1.Cells[10].Text;
                                dr["Brand"] = gvrow1.Cells[8].Text;
                                dr["ReqDate"] = txtReqByDate.Text;
                                dr["Room"] = gvrow1.Cells[13].Text;
                                dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                                dr["Priority"] = "---";

                                dr["Color"] = gvrow1.Cells[14].Text;
                                dr["ColorId"] = gvrow1.Cells[15].Text;


                                SalesOrderItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = SalesOrderItems.NewRow();
                            dr["ItemCode"] = gvrow1.Cells[2].Text;
                            dr["ItemType"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;
                            //dr["Currency"] = gvrow1.Cells[7].Text;
                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                            dr["Rate"] = gvrow1.Cells[7].Text;
                            dr["Specification"] = "---";
                            // dr["Remarks"] = gvrow1.Cells[10].Text;
                            dr["Brand"] = gvrow1.Cells[8].Text;
                            dr["ReqDate"] = txtReqByDate.Text;
                            dr["Room"] = gvrow1.Cells[13].Text;
                            dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                            dr["Priority"] = "---";

                            dr["Color"] = gvrow1.Cells[14].Text;
                            dr["ColorId"] = gvrow1.Cells[15].Text;

                            SalesOrderItems.Rows.Add(dr);
                        }
                        if (gvItemDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[3].Text == gvrow1.Cells[3].Text)
                            {
                                gvItemDetails.DataSource = SalesOrderItems;
                                gvItemDetails.DataBind();
                                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                                btnItemRefresh_Click(sender, e);
                                ch.Checked = false;
                                return;
                            }
                        }

                    }
                }
                if (gvItemDetails.SelectedIndex == -1)
                {
                    DataRow drnew = SalesOrderItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[2].Text;
                    drnew["ItemName"] = gvrow.Cells[3].Text;
                    drnew["ItemType"] = gvrow.Cells[4].Text;
                    drnew["UOM"] = gvrow.Cells[5].Text;
                    drnew["Quantity"] = gvrow.Cells[6].Text;
                    drnew["Brand"] = gvrow.Cells[15].Text;
                    drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                    //drnew["Rate"] = gvrow.Cells[8].Text;
                    drnew["Specification"] = "---";
                    //drnew["Remarks"] = "--";
                    drnew["Priority"] = "--";
                    drnew["ReqDate"] = txtReqByDate.Text;
                    drnew["Room"] = gvrow.Cells[10].Text;

                    drnew["Color"] = gvrow.Cells[18].Text;
                    drnew["ColorId"] = gvrow.Cells[19].Text;

                    //drnew["Price"] = gvrow.Cells[10].Text;

                    SalesOrderItems.Rows.Add(drnew);
                }
                gvItemDetails.DataSource = SalesOrderItems;
                gvItemDetails.DataBind();
                gvItemDetails.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }


        }

    }

    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.SalesOrder.CusID_Select(ddlOrderAcceptance, ddlSupplierName.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally { SM.Dispose(); }
       //SalesOrderMaster_Fill();
       // SM.DDLBindWithSelect(ddlOrderAcceptance,"SELECT YANTRA_SO_MAST.SO_ID, YANTRA_SO_MAST.SO_NO FROM YANTRA_SO_MAST INNER JOIN YANTRA_QUOT_MAST ON YANTRA_SO_MAST.QUOT_ID = YANTRA_QUOT_MAST.QUOT_ID INNER JOIN YANTRA_ENQ_MAST ON YANTRA_QUOT_MAST.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID INNER JOIN  YANTRA_CUSTOMER_MAST ON YANTRA_ENQ_MAST.CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID where YANTRA_CUSTOMER_MAST.CUST_ID =" +ddlSupplierName.SelectedItem.Value +""); 
        //SM.DDLBindWithSelect(ddlOrderAcceptance, "select YANTRA_SO_MAST.SO_NO, YANTRA_SO_MAST.SO_ID from  YANTRA_SO_MAST INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_SO_MAST.SO_CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID where YANTRA_CUSTOMER_MAST.CUST_ID = " + ddlSupplierName.SelectedItem.Value + " ");
    }

    protected void gvOrderAcceptanceItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
     
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Priority");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Room");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Price");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Brand");
        SalesOrderItems.Columns.Add(col);

        if (gvOrderAcceptanceItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOrderAcceptanceItems.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
             
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["Specifications"] = gvrow.Cells[9].Text;
                //dr["Remarks"] = gvrow.Cells[10].Text;
                //dr["Priority"] = gvrow.Cells[11].Text;
                dr["DeliveryDate"] = gvrow.Cells[12].Text;
                dr["Brand"] = gvrow.Cells[15].Text;
              

                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvOrderAcceptanceItems.Rows[e.NewEditIndex].RowIndex)
                {

                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);

                    txtItemUOM.Text = gvrow.Cells[5].Text;
                   txtQuantity.Text = gvrow.Cells[6].Text;
               
                    txtSpecification.Text = gvrow.Cells[9].Text;
                    // txtItemRemarks.Text = gvrow.Cells[10].Text;
                    // ddlItemPriority.SelectedValue = gvrow.Cells[11].Text;
                    
                  
                    gvOrderAcceptanceItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvOrderAcceptanceItems.DataSource = SalesOrderItems;
        gvOrderAcceptanceItems.DataBind();
    }
    protected void gvOrderAcceptanceItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemType.DataSourceID = "SqlDataSource2";
        ddlItemType.DataTextField = "ITEM_MODEL_NO";
        ddlItemType.DataValueField = "ITEM_CODE";
        ddlItemType.DataBind();
        ddlItemType_SelectedIndexChanged(sender, e);
    }

    protected void rdbCustomer_CheckedChanged(object sender, EventArgs e)
    {
        tblPoDetails.Visible = true;
        
    }
    protected void rdbself_CheckedChanged(object sender, EventArgs e)
    {
        tblPoDetails.Visible = false;
       
    }
  
}

 
