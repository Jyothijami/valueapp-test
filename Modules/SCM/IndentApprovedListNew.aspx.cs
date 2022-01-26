using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;

public partial class Modules_SCM_IndentApprovedListNew : basePage
{

    #region PAGE LOAD

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Department_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            gvApprlItemDetails.DataBind();
            gvSupplierDetails.DataBind();
            
            EmployeeMaster_Fill();
            CustomerName_Fill();
            SuppName_Fill();
            ItemName_Fill();
           
            if(Request.QueryString["IndentId"] == "New")
            {
                tblIndentApprovalDetails.Visible = true;
                gvApprlItemDetails.DataBind();
               // SCM.ClearControls(this);
                txtApprovalNo.Text = SCM.IndentApproval.IndentApproval_AutoGenCode();
                txtIndentApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
            }
            else if (Request.QueryString["IndentId"] != "New")
            {
                EditIndentApprovedList();
            }
        }
    }

    private void EditIndentApprovedList()
    {
        try
            {
                SCM.IndentApproval objIndentApproval = new SCM.IndentApproval();

                if (objIndentApproval.IndentApproval_Select(Request.QueryString["IndentId"]) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblIndentApprovalDetails.Visible = true;
                    // ddlIndentNo.SelectedValue = objIndentApproval.IndId;
                    //txtIndentDate.Text = objIndentApproval.INDDate;
                    // ddlDepart.SelectedValue=objIndentApproval.INDDepartment
                    txtApprovalNo.Text = objIndentApproval.INDAPPRLNo;
                    txtIndentApprovalDate.Text = objIndentApproval.INDAPPRLDate;
                    ddlDepart.SelectedValue = objIndentApproval.DeptId;
                    ddlFollowUp.SelectedValue = objIndentApproval.FollowUp;
                    ddlPreparedBy.SelectedValue = objIndentApproval.INDAPPRLPreparedBy;
                    ddlApprovedBy.SelectedValue = objIndentApproval.INDAPPRLApprovedBy;

                    objIndentApproval.IndentApprovalDetails_Select(Request.QueryString["IndentId"], gvApprlItemDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //btnDelete.Attributes.Clear();
                // ddlIndentNo_SelectedIndexChanged(sender, e);
            }
    }

    #endregion PAGE LOAD

    private void SuppName_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
            //SCM.SuppliersMaster.SuppliersMaster_SelectForBrand(ddlSupplierName,ddlBrand.SelectedValue );
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

    #region CustomerName Fill

    private void CustomerName_Fill()
    {
    }

    #endregion CustomerName Fill

    #region Department Fill

    private void Department_Fill()
    {
        try
        {
            //Masters.Department.Department_Select(ddlDepartment);
            Masters.Department.Department_Select(ddlDepart);
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

    #endregion Department Fill

    #region Employee Master Fill

    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            // HR.EmployeeMaster.EmployeeMaster_Select(ddlFollow);
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

    #endregion Employee Master Fill

    #region Item Name Fill

    private void ItemName_Fill()
    {
        //try
        //{
        //    //SCM.Indent.IndentItemNames_Select(ddlIndentNo.SelectedItem.Value, ddlItemType.SelectedItem.Value, ddlItemName);
        //    Masters.ItemMaster.ItemMaster_Select(ddlItemType, ddlItemType.SelectedValue);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{
        //    //SCM.Dispose();
        //    Masters.Dispose();
        //}
    }

    #endregion Item Name Fill

    //#region Link Button IndentApprovalNo_Click

    //protected void lbtnIndentApprovalNo_Click(object sender, EventArgs e)
    //{
    //    tblIndentApprovalDetails.Visible = false;
    //    LinkButton lbtnIndentApprovalNo;
    //    lbtnIndentApprovalNo = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lbtnIndentApprovalNo.Parent.Parent;
    //    gvIndentApprlDetails.SelectedIndex = gvRow.RowIndex;
    //    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    //    try
    //    {
    //        SCM.IndentApproval objIndentApproval = new SCM.IndentApproval();

    //        if (objIndentApproval.IndentApproval_Select(Request.QueryString["IndentId"]) > 0)
    //        {
    //            btnSave.Text = "Update";
    //            btnSave.Enabled = false;
    //            tblIndentApprovalDetails.Visible = true;
    //            // ddlIndentNo.SelectedValue = objIndentApproval.IndId;
    //            //txtIndentDate.Text = objIndentApproval.INDDate;
    //            // ddlDepart.SelectedValue=objIndentApproval.INDDepartment
    //            txtApprovalNo.Text = objIndentApproval.INDAPPRLNo;
    //            txtIndentApprovalDate.Text = objIndentApproval.INDAPPRLDate;
    //            ddlDepart.SelectedValue = objIndentApproval.DeptId;
    //            ddlFollowUp.SelectedValue = objIndentApproval.FollowUp;
    //            ddlPreparedBy.SelectedValue = objIndentApproval.INDAPPRLPreparedBy;
    //            ddlApprovedBy.SelectedValue = objIndentApproval.INDAPPRLApprovedBy;

    //            objIndentApproval.IndentApprovalDetails_Select(Request.QueryString["IndentId"], gvApprlItemDetails);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        btnDelete.Attributes.Clear();
    //        // SCM.Dispose();
    //        // ddlIndentNo_SelectedIndexChanged(sender, e);
    //    }
    //}

    //#endregion Link Button IndentApprovalNo_Click

    #region Button NEW  Click

    protected void btnNew_Click(object sender, EventArgs e)
    {
        tblIndentApprovalDetails.Visible = true;
        gvIndentDetails.DataBind();
        gvApprlItemDetails.DataBind();
        SCM.ClearControls(this);
        txtApprovalNo.Text = SCM.IndentApproval.IndentApproval_AutoGenCode();
        txtIndentApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
    }

    #endregion Button NEW  Click

    #region Button SAVE/UPDATE  Click

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            txtApprovalNo.Text = SCM.IndentApproval.IndentApproval_AutoGenCode();
            IndentApprovalSave();
            Response.Redirect("IndentHistory.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            IndentApprovalUpdate();
            Response.Redirect("IndentHistory.aspx");
        }
    }

    #endregion Button SAVE/UPDATE  Click

    #region gvIndentDetails_RowDataBound

    private void IndentApprovalSave()
    {
        if (gvApprlItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.IndentApproval objSCM = new SCM.IndentApproval();
                objSCM.INDAPPRLNo = txtApprovalNo.Text;
                objSCM.INDAPPRLDate = Yantra.Classes.General.toMMDDYYYY(txtIndentApprovalDate.Text);
                objSCM.DeptId = ddlDepart.SelectedItem.Value;
                objSCM.IndId = "0";
                objSCM.FollowUp = "0";
                objSCM.INDAPPRLPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLFlag = "New";
                objSCM.CpId = lblCPID.Text;

                if (objSCM.IndentApproval_Save() == "Data Saved Successfully")
                {
                    objSCM.IndentApprovalDetails_Delete(objSCM.INDAPPRLId);
                    foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
                    {
                        objSCM.INDAPPRLItemCode = gvrow.Cells[2].Text;
                        objSCM.INDAPPRLDetQty = gvrow.Cells[7].Text;
                        objSCM.INDAPPRLDetPriority = "0";
                        objSCM.INDAPPRLDetBrand = gvrow.Cells[8].Text;
                        objSCM.INDAPPRLDetSuggParty = gvrow.Cells[9].Text;
                        objSCM.INDAPPRLDetReqFor = gvrow.Cells[9].Text;
                        objSCM.INDAPPRLDetReqByDate = DateTime.Now.ToString("MM/dd/yyyy");
                        objSCM.INDAPPRLDetSpecs = "-";
                        objSCM.INDAPPRLDetStatus = "New";
                        objSCM.IndColor = gvrow.Cells[11].Text;
                        objSCM.IND_DET_ID = gvrow.Cells[12].Text;
                        objSCM.IndId = gvrow.Cells[5].Text;

                        objSCM.IndentApprovalDetails_Save();

                        //int remainQty = int.Parse((gvrow.Cells[13].Text) - (gvrow.Cells[7].Text));
                        string indDetId=gvrow.Cells[12].Text;
                        string qty = (Convert.ToInt32(gvrow.Cells[13].Text) - Convert.ToInt32(gvrow.Cells[7].Text)).ToString();
                        objSCM.IndentDetEnqQuantity_Update(qty, indDetId);                        
                    }

                    objSCM.IndentSupplierDetails_Delete(objSCM.INDAPPRLId);
                    foreach (GridViewRow gvr in gvSupplierDetails.Rows)
                    {
                        objSCM.Supplier_Id = gvr.Cells[1].Text;
                        objSCM.IndentSupplierDetails_Save();
                    }

                    //if (objSCM.Get_Ids_Select(objSCM.INDAPPRLId) > 0)
                    //{
                    //    SCM.IndentApproval.IndentStatus_Update(SCM.SCMStatus.Closed, objSCM.IndId);

                    //}

                    //SCM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    //SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                // SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //btnDelete.Attributes.Clear();
                //gvIndentApprlDetails.DataBind();
                gvApprlItemDetails.DataBind();
                gvIndentDetails.DataBind();
                tblIndentApprovalDetails.Visible = false;
                SCM.Dispose();
                SCM.ClearControls(this);
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for IndentApproval");
        }
    }

    #endregion gvIndentDetails_RowDataBound

    #region IndentApprovalUpdate

    private void IndentApprovalUpdate()
    {
        if (gvApprlItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.IndentApproval objSCM = new SCM.IndentApproval();

                //  SCM.BeginTransaction();
                objSCM.INDAPPRLId = Request.QueryString["IndentId"];
                objSCM.INDAPPRLNo = txtApprovalNo.Text;
                objSCM.INDAPPRLDate = Yantra.Classes.General.toMMDDYYYY(txtIndentApprovalDate.Text);
                //objSCM.IndNo = ddlIndentNo.SelectedItem.Value;
                objSCM.DeptId = ddlDepart.SelectedItem.Value;
                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDAPPRLPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLFlag = "New";
                objSCM.CpId = lblCPID.Text;

                if (objSCM.IndentApproval_Update() == "Data Updated Successfully")
                {
                    objSCM.IndentApprovalDetails_Delete(objSCM.INDAPPRLId);
                    foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
                    {
                        objSCM.INDAPPRLItemCode = gvrow.Cells[2].Text;
                        objSCM.INDAPPRLDetQty = gvrow.Cells[7].Text;
                        objSCM.INDAPPRLDetPriority = "0";
                        objSCM.INDAPPRLDetBrand = gvrow.Cells[8].Text;
                        objSCM.INDAPPRLDetSuggParty = gvrow.Cells[9].Text;
                        objSCM.INDAPPRLDetReqFor = gvrow.Cells[9].Text;
                        objSCM.INDAPPRLDetReqByDate = DateTime.UtcNow.ToString();
                        objSCM.INDAPPRLDetSpecs = "-";
                        objSCM.INDAPPRLDetStatus = "New";
                        objSCM.IndColor = gvrow.Cells[11].Text;
                        objSCM.IND_DET_ID = gvrow.Cells[12].Text;
                        objSCM.IndId = gvrow.Cells[5].Text;

                        objSCM.IndentApprovalDetails_Save();
                    }

                    objSCM.IndentSupplierDetails_Delete(objSCM.INDAPPRLId);
                    foreach (GridViewRow gvr in gvSupplierDetails.Rows)
                    {
                        objSCM.Supplier_Id = gvr.Cells[1].Text;
                        objSCM.IndentSupplierDetails_Save();
                    }
                    MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    //SCM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                //  SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                //btnDelete.Attributes.Clear();
                //gvIndentApprlDetails.DataBind();
                gvApprlItemDetails.DataBind();
                gvIndentDetails.DataBind();
                tblIndentApprovalDetails.Visible = false;
                SCM.ClearControls(this);
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for IndentApproval");
        }
    }

    #endregion IndentApprovalUpdate

    //#region Button EDIT  Click

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    if (gvIndentApprlDetails.SelectedIndex > -1)
    //    {
    //        try
    //        {
    //            SCM.IndentApproval objIndentApproval = new SCM.IndentApproval();

    //            if (objIndentApproval.IndentApproval_Select(Request.QueryString["IndentId"]) > 0)
    //            {
    //                btnSave.Text = "Update";
    //                btnSave.Enabled = true;
    //                tblIndentApprovalDetails.Visible = true;
    //                // ddlIndentNo.SelectedValue = objIndentApproval.IndId;
    //                //txtIndentDate.Text = objIndentApproval.INDDate;
    //                // ddlDepart.SelectedValue=objIndentApproval.INDDepartment
    //                txtApprovalNo.Text = objIndentApproval.INDAPPRLNo;
    //                txtIndentApprovalDate.Text = objIndentApproval.INDAPPRLDate;
    //                ddlDepart.SelectedValue = objIndentApproval.DeptId;
    //                ddlFollowUp.SelectedValue = objIndentApproval.FollowUp;
    //                ddlPreparedBy.SelectedValue = objIndentApproval.INDAPPRLPreparedBy;
    //                ddlApprovedBy.SelectedValue = objIndentApproval.INDAPPRLApprovedBy;

    //                objIndentApproval.IndentApprovalDetails_Select(Request.QueryString["IndentId"], gvApprlItemDetails);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            btnDelete.Attributes.Clear();
    //            // ddlIndentNo_SelectedIndexChanged(sender, e);
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}

    //#endregion Button EDIT  Click

    //#region Button DELETE  Click

    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    if (gvIndentApprlDetails.SelectedIndex > -1)
    //    {
    //        try
    //        {
    //            SCM.IndentApproval objSCM = new SCM.IndentApproval();
    //            MessageBox.Show(this, objSCM.IndentApproval_Delete(Request.QueryString["IndentId"]));
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message);
    //        }
    //        finally
    //        {
    //            btnDelete.Attributes.Clear();
    //            gvIndentApprlDetails.DataBind();
    //            SCM.ClearControls(this);
    //            SCM.Dispose();
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}

    //#endregion Button DELETE  Click

    #region Button REFRESH  Click

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }

    #endregion Button REFRESH  Click

    #region Button CLOSE  Click

    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblIndentApprovalDetails.Visible = false;
    }

    #endregion Button CLOSE  Click

    //#region Button Add
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    DataTable IndentApprovalProducts = new DataTable();

    //    DataColumn col = new DataColumn();

    //    col = new DataColumn("ItemCode");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("ItemName");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("ItemType");
    //    IndentApprovalProducts.Columns.Add(col);
    //    //col = new DataColumn("ItemGroup");
    //    //IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("UOM");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("Quantity");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("Priority");
    //    IndentApprovalProducts.Columns.Add(col);
    //    //col = new DataColumn("BalQty");
    //    //IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("Brand");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("SuggestedParty");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("ReqFor");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("ReqDate");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("Specification");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("Color");
    //    IndentApprovalProducts.Columns.Add(col);
    //    col = new DataColumn("ColorId");
    //    IndentApprovalProducts.Columns.Add(col);

    //    if (gvApprlItemDetails.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
    //        {
    //            if (gvApprlItemDetails.SelectedIndex > -1)
    //            {
    //                if (gvrow.RowIndex == gvApprlItemDetails.SelectedRow.RowIndex)
    //                {
    //                    DataRow drnew = IndentApprovalProducts.NewRow();
    //                    drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
    //                    drnew["ItemName"] = ddlItemType.SelectedItem.Text;
    //                    drnew["ItemType"] = txtModelName.Text;
    //                    //drnew["ItemGroup"] = ddlItemGroup.SelectedItem.Value;
    //                    drnew["UOM"] = txtItemUOM.Text;
    //                    drnew["Quantity"] = txtQuantity.Text;
    //                    drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
    //                    //drnew["BalQty"] = txtBalanceQty.Text;
    //                    drnew["Brand"] = txtBrand.Text;
    //                    drnew["SuggestedParty"] = ddlSuppliers.SelectedItem.Text;
    //                    drnew["ReqFor"] = txtRequiredFor.Text;
    //                    drnew["ReqDate"] = txtReqByDate.Text;
    //                    drnew["Specification"] = txtSpecification.Text;
    //                    drnew["Color"] = ddlColor.SelectedItem.Text;
    //                    drnew["ColorId"] = ddlColor.SelectedItem.Value;
    //                    if (txtBrand.Text == string.Empty)
    //                    {
    //                        drnew["Brand"] = "--";
    //                    }
    //                    else
    //                    {
    //                        drnew["Brand"] = txtBrand.Text;

    //                    }
    //                    if (txtRequiredFor.Text == string.Empty)
    //                    {
    //                        drnew["ReqFor"] = "--";
    //                    }
    //                    else
    //                    {
    //                        drnew["ReqFor"] = txtRequiredFor.Text;

    //                    }
    //                    if (txtSpecification.Text == string.Empty)
    //                    {
    //                        drnew["Specification"] = "--";
    //                    }
    //                    else
    //                    {
    //                        drnew["Specification"] = txtSpecification.Text;

    //                    }
    //                    IndentApprovalProducts.Rows.Add(drnew);
    //                }
    //                else
    //                {
    //                    DataRow dr = IndentApprovalProducts.NewRow();
    //                    dr["ItemCode"] = gvrow.Cells[2].Text;
    //                    dr["ItemName"] = gvrow.Cells[3].Text;
    //                    dr["ItemType"] = gvrow.Cells[4].Text;
    //                    //dr["ItemGroup"] = gvrow.Cells[4].Text;
    //                    dr["UOM"] = gvrow.Cells[5].Text;
    //                    dr["Quantity"] = gvrow.Cells[6].Text;
    //                    dr["Priority"] = gvrow.Cells[7].Text;
    //                    //dr["BalQty"] = gvrow.Cells[8].Text;
    //                    dr["Brand"] = gvrow.Cells[8].Text;
    //                    dr["SuggestedParty"] = gvrow.Cells[9].Text;
    //                    dr["ReqFor"] = gvrow.Cells[10].Text;
    //                    dr["ReqDate"] = gvrow.Cells[11].Text;
    //                    dr["Specification"] = gvrow.Cells[12].Text;
    //                    dr["Color"] = gvrow.Cells[14].Text;
    //                    dr["ColorId"] = gvrow.Cells[15].Text;

    //                    IndentApprovalProducts.Rows.Add(dr);
    //                }
    //            }
    //            else
    //            {
    //                DataRow dr = IndentApprovalProducts.NewRow();
    //                dr["ItemCode"] = gvrow.Cells[2].Text;
    //                dr["ItemName"] = gvrow.Cells[3].Text;
    //                dr["ItemType"] = gvrow.Cells[4].Text;
    //                //dr["ItemGroup"] = gvrow.Cells[4].Text;
    //                dr["UOM"] = gvrow.Cells[5].Text;
    //                dr["Quantity"] = gvrow.Cells[6].Text;
    //                dr["Priority"] = gvrow.Cells[7].Text;
    //                //dr["BalQty"] = gvrow.Cells[8].Text;
    //                dr["Brand"] = gvrow.Cells[8].Text;
    //                dr["SuggestedParty"] = gvrow.Cells[9].Text;
    //                dr["ReqFor"] = gvrow.Cells[10].Text;
    //                dr["ReqDate"] = gvrow.Cells[11].Text;
    //                dr["Specification"] = gvrow.Cells[12].Text;
    //                dr["Color"] = gvrow.Cells[14].Text;
    //                dr["ColorId"] = gvrow.Cells[15].Text;

    //                IndentApprovalProducts.Rows.Add(dr);
    //            }
    //        }
    //    }
    //    if (gvApprlItemDetails.Rows.Count > 0)
    //    {
    //        if (gvApprlItemDetails.SelectedIndex == -1)
    //        {
    //            foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
    //            {
    //                if (gvrow.Cells[2].Text == ddlItemType.SelectedItem.Value)
    //                {
    //                    gvApprlItemDetails.DataSource = IndentApprovalProducts;
    //                    gvApprlItemDetails.DataBind();
    //                    MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
    //                    return;
    //                }

    //            }
    //        }
    //    }
    //    if (gvApprlItemDetails.SelectedIndex == -1)
    //    {
    //        DataRow drnew = IndentApprovalProducts.NewRow();
    //        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
    //        drnew["ItemName"] = ddlItemType.SelectedItem.Text;
    //        drnew["ItemType"] = txtModelName.Text;
    //        //drnew["ItemGroup"] = ddlItemGroup.SelectedItem.Value;
    //        drnew["UOM"] = txtItemUOM.Text;
    //        drnew["Quantity"] = txtQuantity.Text;
    //        drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
    //        //drnew["BalQty"] = txtBalanceQty.Text;
    //        drnew["Brand"] = txtBrand.Text;
    //        drnew["SuggestedParty"] = ddlSuppliers.SelectedItem.Text;
    //        drnew["ReqFor"] = txtRequiredFor.Text;
    //        drnew["ReqDate"] = txtReqByDate.Text;
    //        drnew["Specification"] = txtSpecification.Text;
    //        drnew["Color"] = ddlColor.SelectedItem.Text;
    //        drnew["ColorId"] = ddlColor.SelectedItem.Value;
    //        if (txtBrand.Text == string.Empty)
    //        {
    //            drnew["Brand"] = "--";
    //        }
    //        else
    //        {
    //            drnew["Brand"] = txtBrand.Text;

    //        }
    //        if (txtRequiredFor.Text == string.Empty)
    //        {
    //            drnew["ReqFor"] = "--";
    //        }
    //        else
    //        {
    //            drnew["ReqFor"] = txtRequiredFor.Text;

    //        }
    //        if (txtSpecification.Text == string.Empty)
    //        {
    //            drnew["Specification"] = "--";
    //        }
    //        else
    //        {
    //            drnew["Specification"] = txtSpecification.Text;

    //        }

    //        IndentApprovalProducts.Rows.Add(drnew);
    //    }
    //    gvApprlItemDetails.DataSource = IndentApprovalProducts;
    //    gvApprlItemDetails.DataBind();
    //    btnItemRefresh_Click(sender, e);
    //    gvApprlItemDetails.SelectedIndex = -1;
    //}
    //#endregion

    //#region Button Items Refresh
    //protected void btnItemRefresh_Click(object sender, EventArgs e)
    //{
    //    txtItemCategory.Text = string.Empty;
    //    txtItemSubCategory.Text = string.Empty;
    //    txtColor.Text = string.Empty;
    //    ddlItemType.SelectedValue = "0";
    //    txtModelName.Text = string.Empty;
    //    txtItemUOM.Text = string.Empty;
    //    txtQuantityInHand.Text = string.Empty;
    //    txtQuantity.Text = string.Empty;
    //    txtBalanceQty.Text = string.Empty;
    //    ddlItemPriority.SelectedValue = "0";
    //    txtBrand.Text = string.Empty;
    //    txtSupplierName.Text = string.Empty;
    //    txtRequiredFor.Text = string.Empty;
    //    txtReqByDate.Text = string.Empty;
    //    txtSpecification.Text = string.Empty;
    //    Image1.ImageUrl = "~/Images/noimage400x300.gif";
    //    ddlSuppliers.SelectedValue = "0";
    //    ddlColor.SelectedValue = "0";
    //}
    //#endregion

    //#region ddlItemName_SelectedIndexChanged
    //protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
    //        {
    //            txtItemUOM.Text = objMaster.ItemUOMShort;
    //            txtQuantityInHand.Text = objMaster.ItemQtyInHand;
    //            txtBalanceQty.Text = objMaster.ItemMinStockQty;

    //            SCM.SuppliersMaster objSCMSM = new SCM.SuppliersMaster();
    //            if (objSCMSM.SuppliersMaster_SelectByItemCode(ddlItemType.SelectedItem.Value) > 0)
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

    //#region DropDownList Search By Select Index Changed

    //protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSearchBy.SelectedItem.Text == "Indent Approval Date" || ddlSearchBy.SelectedItem.Text == "Indent Date")
    //    {
    //        ddlSymbols.Visible = true;
    //        imgToDate.Visible = true;
    //        ceSearchValueToDate.Enabled = true;
    //        //  MaskedEditSearchToDate.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlSymbols.Visible = false;
    //        imgToDate.Visible = false;
    //        ceSearchValueToDate.Enabled = false;
    //        //  MaskedEditSearchToDate.Enabled = false;
    //        txtSearchValueFromDate.Visible = false;
    //        lblCurrentFromDate.Visible = false;
    //        lblCurrentToDate.Visible = false;
    //        imgFromDate.Visible = false;
    //        ceSearchFrom.Enabled = false;
    //        //  MaskedEditSearchFromDate.Enabled = false;
    //        ddlSymbols.SelectedIndex = 0;
    //    }
    //    txtSearchText.Text = string.Empty;
    //}

    //#endregion DropDownList Search By Select Index Changed

    //#region DropDownList Symbols Select Index Changed

    //protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSymbols.SelectedItem.Text == "R")
    //    {
    //        txtSearchValueFromDate.Visible = true;
    //        lblCurrentFromDate.Visible = true;
    //        lblCurrentToDate.Visible = true;
    //        imgFromDate.Visible = true;
    //        ceSearchFrom.Enabled = true;
    //        // MaskedEditSearchFromDate.Enabled = true;
    //    }
    //    else
    //    {
    //        txtSearchValueFromDate.Visible = false;
    //        lblCurrentFromDate.Visible = false;
    //        lblCurrentToDate.Visible = false;
    //        imgFromDate.Visible = false;
    //        ceSearchFrom.Enabled = false;
    //        // MaskedEditSearchFromDate.Enabled = false;
    //    }
    //}

    //#endregion DropDownList Symbols Select Index Changed

    //#region Search Go Click

    //protected void btnSearchGo_Click(object sender, EventArgs e)
    //{
    //    gvIndentApprlDetails.SelectedIndex = -1;
    //    lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
    //    lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
    //    if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
    //    else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
    //    if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
    //    else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
    //    gvIndentApprlDetails.DataBind();
    //}

    //#endregion Search Go Click

    #region Print Button Click

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IndentId"] != null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=IndentApproval&indapprlno=" + Request.QueryString["IndentId"] + "";
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

    #endregion Print Button Click

    //#region itemtype selected index changed
    //protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedItem.Value);
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

    #region ddlIndent NO Selected Index Changed

    //protected void ddlIndentNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SCM.Indent objIndent = new SCM.Indent();
    //        if (objIndent.Indent_Select(ddlIndentNo.SelectedItem.Value) > 0)
    //        {
    //            ItemTypes_Fill();
    //           // txtIndentDate.Text = objIndent.INDDate;
    //            //ddlDepartment.SelectedValue = objIndent.DeptId;
    //           // ddlFollow.SelectedValue = objIndent.FollowUp;

    //            objIndent.IndentDetails_Select(ddlIndentNo.SelectedItem.Value, gvIndentDetails);
    //            gvApprlItemDetails.DataBind();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        btnDelete.Attributes.Clear();
    //        SCM.Dispose();
    //    }

    //}

    #endregion ddlIndent NO Selected Index Changed

    #region GridView IndentApproval Details Row DataBound

    protected void gvIndentApprlDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    #endregion GridView IndentApproval Details Row DataBound

    #region gvApprlItemDetails_RowDataBound

    protected void gvApprlItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
    }

    #endregion gvApprlItemDetails_RowDataBound

    #region GridView IndentApprl Items Row Deleting

    protected void gvApprlItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvApprlItemDetails.Rows[e.RowIndex].Cells[2].Text;
        DataTable IndentApprovalProducts = new DataTable();

        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("UOM");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IndentId");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        IndentApprovalProducts.Columns.Add(col);
        //col = new DataColumn("SupplierName");
        //IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IndentdetId");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("RemQty");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("OrdQty");
        IndentApprovalProducts.Columns.Add(col);
        
        //col = new DataColumn("SuggestedParty");
        //IndentApprovalProducts.Columns.Add(col);
        //col = new DataColumn("ReqFor");
        //IndentApprovalProducts.Columns.Add(col);
        //col = new DataColumn("ReqDate");
        //IndentApprovalProducts.Columns.Add(col);
        //col = new DataColumn("Specification");
        //IndentApprovalProducts.Columns.Add(col);

        col = new DataColumn("Color");
        IndentApprovalProducts.Columns.Add(col);

        col = new DataColumn("ColorId");
        IndentApprovalProducts.Columns.Add(col);

        if (gvApprlItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["ItemType"] = gvrow.Cells[4].Text;
                    //dr["ItemGroup"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    // dr["SupplierName"] = gvrow.Cells[8].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    //dr["SuggestedParty"] = gvrow.Cells[9].Text;
                    //dr["ReqFor"] = gvrow.Cells[9].Text;
                    //dr["ReqDate"] = gvrow.Cells[11].Text;
                    //dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[9].Text;
                    dr["ColorId"] = gvrow.Cells[10].Text;
                    dr["IndentdetId"] = gvrow.Cells[12].Text;
                    dr["IndentId"] = gvrow.Cells[5].Text;
                    dr["RemQty"] = gvrow.Cells[13].Text;
                    dr["OrdQty"] = gvrow.Cells[14].Text;

                    IndentApprovalProducts.Rows.Add(dr);
                }
            }
        }
        gvApprlItemDetails.DataSource = IndentApprovalProducts;
        gvApprlItemDetails.DataBind();
        MessageBox.Show(this, "Deleted Successfully");
    }

    #endregion GridView IndentApprl Items Row Deleting

    #region GridView gvIndentDetails Row Databound

    protected void gvIndentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
    }

    #endregion GridView gvIndentDetails Row Databound

    //protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
    //        {
    //            txtBalanceQty.Text = objMaster.ItemMinStockQty;
    //            txtQuantityInHand.Text = objMaster.ItemQtyInHand;
    //            txtItemUOM.Text = objMaster.ItemUOMShort;
    //            txtSpecification.Text = objMaster.ItemSpec;
    //            txtItemCategory.Text = objMaster.ItemCategoryName;
    //            txtModelName.Text = objMaster.ItemName;
    //            txtSpecification.Text = objMaster.ItemSpec;
    //           //txtColor.Text = objMaster.Color;
    //            txtSupplierName.Text = objMaster.ItemPrincipalName;
    //            txtBrand.Text = objMaster.BrandProductName;
    //            txtItemSubCategory.Text = objMaster.ItemType;
    //            Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

    //        }
    //        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedValue);
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
    //protected void gvApprlItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    DataTable IndentProducts = new DataTable();
    //    DataColumn col = new DataColumn();
    //    col = new DataColumn("ItemCode");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemName");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemType");
    //    IndentProducts.Columns.Add(col);

    //    col = new DataColumn("UOM");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Quantity");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Priority");
    //    IndentProducts.Columns.Add(col);

    //    col = new DataColumn("Brand");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("SuggestedParty");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ReqFor");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ReqDate");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Specification");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Color");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ColorId");
    //    IndentProducts.Columns.Add(col);

    //    if (gvApprlItemDetails.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
    //        {
    //            DataRow dr = IndentProducts.NewRow();
    //            dr["ItemCode"] = gvrow.Cells[2].Text;
    //            dr["ItemName"] = gvrow.Cells[3].Text;
    //            dr["ItemType"] = gvrow.Cells[4].Text;
    //            dr["UOM"] = gvrow.Cells[5].Text;
    //            dr["Quantity"] = gvrow.Cells[6].Text;
    //            dr["Priority"] = gvrow.Cells[7].Text;
    //            dr["Brand"] = gvrow.Cells[8].Text;
    //            dr["SuggestedParty"] = gvrow.Cells[9].Text;
    //            dr["ReqFor"] = gvrow.Cells[10].Text;
    //            dr["ReqDate"] = gvrow.Cells[11].Text;
    //            dr["Specification"] = gvrow.Cells[12].Text;

    //            dr["Color"] = gvrow.Cells[14].Text;
    //            dr["ColorId"] = gvrow.Cells[15].Text;

    //            IndentProducts.Rows.Add(dr);
    //            if (gvrow.RowIndex == gvApprlItemDetails.Rows[e.NewEditIndex].RowIndex)
    //            {
    //                ddlItemType.SelectedValue = gvrow.Cells[2].Text;
    //                ddlItemType_SelectedIndexChanged(sender, e);
    //                //ddlItemPriority.SelectedValue = gvrow.Cells[7].Text;
    //                //txtReqByDate.Text = gvrow.Cells[11].Text;
    //                txtQuantity.Text = gvrow.Cells[6].Text;
    //                //txtItemRate.Text = gvrow.Cells[7].Text;
    //                txtRequiredFor.Text = gvrow.Cells[10].Text;
    //                //ddlSuppliers.SelectedItem.Text = gvrow.Cells[9].Text;
    //                ddlColor.SelectedItem.Value = gvrow.Cells[15].Text;
    //                gvApprlItemDetails.SelectedIndex = gvrow.RowIndex;
    //            }
    //        }
    //    }
    //}
    //protected void gvIndentDetails_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    DataTable IndentProducts = new DataTable();
    //    DataColumn col = new DataColumn();
    //    col = new DataColumn("ItemCode");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemName");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ItemType");
    //    IndentProducts.Columns.Add(col);

    //    col = new DataColumn("UOM");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Quantity");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Priority");
    //    IndentProducts.Columns.Add(col);

    //    col = new DataColumn("Brand");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("SuggestedParty");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ReqFor");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("ReqDate");
    //    IndentProducts.Columns.Add(col);
    //    col = new DataColumn("Specification");
    //    IndentProducts.Columns.Add(col);

    //    if (gvIndentDetails.Rows.Count > 0)
    //    {
    //        foreach (GridViewRow gvrow in gvIndentDetails.Rows)
    //        {
    //            DataRow dr = IndentProducts.NewRow();
    //            dr["ItemCode"] = gvrow.Cells[2].Text;
    //            dr["ItemName"] = gvrow.Cells[3].Text;
    //            dr["ItemType"] = gvrow.Cells[4].Text;
    //            dr["UOM"] = gvrow.Cells[5].Text;
    //            dr["Quantity"] = gvrow.Cells[6].Text;
    //            dr["Priority"] = gvrow.Cells[12].Text;
    //            dr["Brand"] = gvrow.Cells[7].Text;
    //             dr["SuggestedParty"] = gvrow.Cells[8].Text;
    //            //dr["ReqFor"] = gvrow.Cells[10].Text;
    //            dr["ReqDate"] = gvrow.Cells[10].Text;
    //            dr["Specification"] = gvrow.Cells[11].Text;

    //            IndentProducts.Rows.Add(dr);
    //            if (gvrow.RowIndex == gvIndentDetails.Rows[e.NewEditIndex].RowIndex)
    //            {
    //                ddlItemType.SelectedValue = gvrow.Cells[2].Text;
    //                ddlItemType_SelectedIndexChanged(sender, e);
    //                //ddlItemPriority.SelectedValue = gvrow.Cells[12].Text;
    //                //txtReqByDate.Text = gvrow.Cells[10].Text;
    //                txtQuantity.Text = gvrow.Cells[6].Text;
    //                //txtItemRate.Text = gvrow.Cells[7].Text;
    //                //txtRequiredFor.Text = gvrow.Cells[10].Text;
    //                //ddlSuppliers.SelectedItem.Value = gvrow.Cells[8].Text;
    //                gvIndentDetails.SelectedIndex = gvrow.RowIndex;
    //            }
    //        }
    //    }
    //}
    protected void gvIndentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvIndentDetails.Rows[e.RowIndex].Cells[1].Text;
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

        if (gvIndentDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvIndentDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["ItemType"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[12].Text;
                    dr["Brand"] = gvrow.Cells[7].Text;
                    //dr["SuggestedParty"] = gvrow.Cells[9].Text;
                    //dr["ReqFor"] = gvrow.Cells[10].Text;
                    dr["ReqDate"] = gvrow.Cells[10].Text;
                    dr["Specification"] = gvrow.Cells[11].Text;

                    IndentProducts.Rows.Add(dr);
                }
            }
        }
        gvIndentDetails.DataSource = IndentProducts;
        gvIndentDetails.DataBind();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvIndentDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                DataTable IndentApprovalProducts = new DataTable();

                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Itemname");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Itemtype");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Indentid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Requiredfor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IndentdetId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("RemQty");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("OrdQty");
                IndentApprovalProducts.Columns.Add(col);

                if (gvApprlItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvApprlItemDetails.Rows)
                    {
                        DataRow dr = IndentApprovalProducts.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ItemName"] = gvrow1.Cells[3].Text;
                        dr["ItemType"] = gvrow1.Cells[4].Text;
                        dr["Indentid"] = gvrow1.Cells[5].Text;
                        dr["UOM"] = gvrow1.Cells[6].Text;
                       // TextBox qty = (TextBox)gvrow.FindControl("txtqty");
                        dr["Quantity"] = gvrow1.Cells[7].Text;
                        dr["Brand"] = gvrow1.Cells[8].Text;
                        dr["Requiredfor"] = gvrow1.Cells[9].Text;
                        dr["Color"] = gvrow1.Cells[10].Text;
                        dr["ColorId"] = gvrow1.Cells[11].Text;
                        dr["IndentdetId"] = gvrow1.Cells[12].Text;
                        dr["RemQty"] = gvrow1.Cells[13].Text;
                        dr["OrdQty"] = gvrow1.Cells[14].Text;
                        IndentApprovalProducts.Rows.Add(dr);
                    }
                }

                if (gvApprlItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvApprlItemDetails.Rows)
                    {
                        if (gvrow1.Cells[2].Text == gvrow.Cells[2].Text)
                        {
                            gvApprlItemDetails.DataSource = IndentApprovalProducts;
                            gvApprlItemDetails.DataBind();
                            MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            ch.Checked = false;
                            return;
                        }
                    }
                }

                DataRow drnew = IndentApprovalProducts.NewRow();
                drnew["ItemCode"] = gvrow.Cells[2].Text;
                drnew["ItemName"] = gvrow.Cells[3].Text;
                drnew["ItemType"] = gvrow.Cells[4].Text;
                drnew["Indentid"] = gvrow.Cells[5].Text;
                drnew["UOM"] = gvrow.Cells[6].Text;
                TextBox qty1 = (TextBox)gvrow.FindControl("txtqty");
                drnew["Quantity"] = qty1.Text;
                drnew["Brand"] = gvrow.Cells[8].Text;
                drnew["Requiredfor"] = gvrow.Cells[9].Text;
                drnew["Color"] = gvrow.Cells[10].Text;
                drnew["ColorId"] = gvrow.Cells[11].Text;
                drnew["IndentdetId"] = gvrow.Cells[13].Text;
                drnew["RemQty"] = gvrow.Cells[14].Text;
                drnew["OrdQty"] = gvrow.Cells[15].Text;
                IndentApprovalProducts.Rows.Add(drnew);
                gvApprlItemDetails.DataSource = IndentApprovalProducts;
                gvApprlItemDetails.DataBind();
                ch.Checked = false;
            }
        }
    }

    //#region Btnsend

    //protected void btnsuppliersEnq_Click(object sender, EventArgs e)
    //{
    //    if (gvIndentApprlDetails.SelectedIndex > -1)
    //    {
    //        Response.Redirect("SuppliersEnquiry.aspx?SeId=" + Request.QueryString["IndentId"] + "");
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please Select Atleast a Record From List");
    //    }
    //}

    //#endregion Btnsend

    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SCM.SuppliersMaster objSCM = new SCM.SuppliersMaster();
        if (objSCM.SuppliersMaster_Select(ddlSupplierName.SelectedItem.Value) > 0)
        {
            txtContactPerson.Text = objSCM.SupContactPerson;
            txtAddress.Text = objSCM.SupAddress;
            txtPhoneNo.Text = objSCM.SupPhone;
            txtMobile.Text = objSCM.SupMobile;
            txtEmail.Text = objSCM.SupEmail;
        }
    }

    protected void btnSuppDetails_Click(object sender, EventArgs e)
    {
        DataTable SupplierDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("SuppId");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("Name");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("PhoneNo");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("Email");
        SupplierDetails.Columns.Add(col);

        if (gvSupplierDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSupplierDetails.Rows)
            {
                DataRow dr = SupplierDetails.NewRow();
                dr["SuppId"] = gvrow.Cells[1].Text;
                dr["Name"] = gvrow.Cells[2].Text;
                dr["ContactPerson"] = gvrow.Cells[3].Text;
                dr["PhoneNo"] = gvrow.Cells[4].Text;
                dr["Email"] = gvrow.Cells[5].Text;

                SupplierDetails.Rows.Add(dr);
            }
        }

        if (gvSupplierDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSupplierDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlSupplierName.SelectedItem.Value)
                {
                    gvSupplierDetails.DataSource = SupplierDetails;
                    gvSupplierDetails.DataBind();
                    MessageBox.Show(this, "The Supplier Name you have selected is already exists in list");
                    return;
                }
            }
        }

        DataRow drnew = SupplierDetails.NewRow();
        drnew["SuppId"] = ddlSupplierName.SelectedItem.Value;
        drnew["Name"] = ddlSupplierName.SelectedItem.Text;
        drnew["ContactPerson"] = txtContactPerson.Text;
        drnew["PhoneNo"] = txtPhoneNo.Text;
        drnew["Email"] = txtEmail.Text;

        SupplierDetails.Rows.Add(drnew);
        gvSupplierDetails.DataSource = SupplierDetails;
        gvSupplierDetails.DataBind();
        btnSuppRefresh_Click(sender, e);
    }

    protected void btnSuppRefresh_Click(object sender, EventArgs e)
    {
        ddlSupplierName.SelectedValue = "0";
        txtPhoneNo.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtAddress.Text = string.Empty;
    }

    protected void gvSupplierDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gvSupplierDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvSupplierDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable SupplierDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("SuppId");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("Name");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("PhoneNo");
        SupplierDetails.Columns.Add(col);
        col = new DataColumn("Email");
        SupplierDetails.Columns.Add(col);
        if (gvSupplierDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSupplierDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = SupplierDetails.NewRow();
                    dr["SuppId"] = gvrow.Cells[1].Text;
                    dr["Name"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["PhoneNo"] = gvrow.Cells[4].Text;
                    dr["Email"] = gvrow.Cells[5].Text;
                    SupplierDetails.Rows.Add(dr);
                }
            }
        }
        gvSupplierDetails.DataSource = SupplierDetails;
        gvSupplierDetails.DataBind();
    }

    protected void gvIndentDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvIndentDetails.EditIndex = e.NewEditIndex;
        //gvIndentDetails.DataBind();
    }

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        gvIndentDetails.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Required By Date" || ddlSearchBy.SelectedItem.Text == "Indent Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            //  MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            //  MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            //  MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
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
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
        }
    }
}
 
