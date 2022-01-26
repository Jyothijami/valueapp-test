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
using System.Data.SqlClient;
using System.Configuration;
public partial class Modules_SCM_SupplierQuation : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string enqNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        enqNo = Request.QueryString["enqNo"];
        if (!IsPostBack)
        {
            setControlsVisibility();

            Department_Fill();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
           // gvApprlItemDetails.DataBind();
             SCM.IndentApproval objs = new SCM.IndentApproval();
            //gvSupplierDetails.DataBind();
            EmployeeMaster_Fill();
            CustomerName_Fill();
            SuppName_Fill();
            ItemName_Fill();
            txtApprovalNo.Text = SCM.IndentApproval.IndentApproval_AutoGenCode();
            txtIndentApprovalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (enqNo != null)
            {
                try
                {
                    SCM.IndentApproval objSCM = new SCM.IndentApproval();
                    if (objSCM.SuppliersEnquiryMaster_Select2(enqNo) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        txtApprovalNo.Text = objSCM.INDAPPRLNo;
                        txtIndentApprovalDate.Text = objSCM.INDAPPRLDate;
                        ddlDepart.SelectedValue = objSCM.DeptId;
                        ddlFollowUp.SelectedValue = objSCM.FollowUp;
                        ddlPreparedBy.SelectedValue = objSCM.INDAPPRLPreparedBy;
                        ddlSupplierName.SelectedValue = objSCM.Supplier_Id;
                        ddlSupplierName_SelectedIndexChanged(sender, e);

                        objSCM.IndentDetails_Select2(objSCM.IndId, gvApprlItemDetails);
                        //objSCM.SuppliersPODetails_Select2(poNo, gvProductDetails);
                       // objSCM.IndentSuppliersPODetails_Select(objSCM.IndId, gvSupplierDetails);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    // btnDelete.Attributes.Clear();
                    // SCM.Dispose();
                }

            }
            else
            {

                if (Request.QueryString["IndentId"] == "New")
                {
                    tblIndentApprovalDetails.Visible = true;
                    objs.IndentDetails_Select3(gvApprlItemDetails);

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
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "21");
        btnSave.Enabled = up.add;
        //btnPrint.Enabled = up.Print;

    }

         private void EditIndentApprovedList()
    {
        try
            {
                SCM.IndentApproval objIndentApproval = new SCM.IndentApproval();

                if (objIndentApproval.IndentApproval_Select(Request.QueryString["IndentId"]) > 0)
                {


                    objIndentApproval.IndentDetails_Select3(gvApprlItemDetails);

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
            HR.EmployeeMaster.EmployeeMaster_SelectPurchase(ddlFollowUp);
           // HR.EmployeeMaster.EmployeeMaster_Select(ddlFollowUp);
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

    #region Button NEW  Click

    protected void btnNew_Click(object sender, EventArgs e)
    {
        tblIndentApprovalDetails.Visible = true;
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
            DeleteRecords();
            Response.Redirect("SupplierQuationDetails.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            IndentApprovalUpdate();
            Response.Redirect("SupplierQuationDetails.aspx");
        }
    }

    #endregion Button SAVE/UPDATE  Click

    private void DeleteRecords()
    {
        SqlCommand cmd = new SqlCommand("Delete From IND_DET_ITEMS", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

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
                //Employee Id
                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDAPPRLPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLFlag = "New";
                objSCM.CpId = lblCPID.Text;
                objSCM.Supplier_Id = ddlSupplierName.SelectedItem.Value;

                if (objSCM.IndentApproval_Save() == "Data Saved Successfully")
                {
                    objSCM.IndentApprovalDetails_Delete(objSCM.INDAPPRLId);
                    foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
                    {
                        objSCM.INDAPPRLItemCode = gvrow.Cells[2].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtQuantity");
                        // objSCM.INDAPPRLDetQty = gvrow.Cells[7].Text;
                        objSCM.INDAPPRLDetQty = Quantity.Text;

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
                        string indDetId = gvrow.Cells[12].Text;
                        string qty = (Convert.ToInt32(Quantity.Text) + Convert.ToInt32(gvrow.Cells[13].Text)).ToString();
                        objSCM.IndentDetEnqQuantity_Update(qty, indDetId);
                        //SCM.IndentApproval obj = new SCM.IndentApproval();
                        //if (obj.IndentDetQty_Select(indDetId) > 0)
                        //{
                        //    string ordQty = obj.IndOrdqty;
                        //    string ttlQty = obj.Indentqty;
                        //    if (Convert.ToInt32(ordQty) >= Convert.ToInt32(qty))
                        //    {
                        //        obj.IndentRecordsStatus_Update("New", indDetId);
                        //    }
                        //}

                        //objSCM.IndentSupplierDetails_Delete(objSCM.INDAPPRLId);
                        //foreach (GridViewRow gvr in gvSupplierDetails.Rows)
                        //{
                        //    objSCM.Supplier_Id = gvr.Cells[1].Text;
                        //    objSCM.IndentSupplierDetails_Save();
                        //}

                        //if (objSCM.Get_Ids_Select(objSCM.INDAPPRLId) > 0)
                        //{
                        //    SCM.IndentApproval.IndentStatus_Update(SCM.SCMStatus.Closed, objSCM.IndId);

                        //}

                        //SCM.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
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
                objSCM.INDAPPRLId = enqNo;
                objSCM.INDAPPRLNo = txtApprovalNo.Text;
                objSCM.INDAPPRLDate = Yantra.Classes.General.toMMDDYYYY(txtIndentApprovalDate.Text);
                //objSCM.IndNo = ddlIndentNo.SelectedItem.Value;
                objSCM.DeptId = ddlDepart.SelectedItem.Value;
                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDAPPRLPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDAPPRLFlag = "New";
                objSCM.CpId = lblCPID.Text;
                objSCM.Supplier_Id = ddlSupplierName.SelectedItem.Value;


                if (objSCM.IndentApproval_Update() == "Data Updated Successfully")
                {
                    objSCM.IndentApprovalDetails_Delete(objSCM.INDAPPRLId);
                    foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
                    {
                        objSCM.INDAPPRLItemCode = gvrow.Cells[2].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtQuantity");
                        objSCM.INDAPPRLDetQty = Quantity.Text;
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

                    //objSCM.IndentSupplierDetails_Delete(objSCM.INDAPPRLId);
                    //foreach (GridViewRow gvr in gvSupplierDetails.Rows)
                    //{
                    //    objSCM.Supplier_Id = gvr.Cells[1].Text;
                    //    objSCM.IndentSupplierDetails_Save();
                    //}
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

    #region Button REFRESH  Click

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }

    #endregion Button REFRESH  Click

    #region Button CLOSE  Click

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //tblIndentApprovalDetails.Visible = false;
        Response.Redirect("SupplierQuationDetails.aspx");
    }

    #endregion Button CLOSE  Click

    

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
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
           // e.Row.Cells[12].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
         
        }
    }

    #endregion gvApprlItemDetails_RowDataBound

    
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

    protected void gvApprlItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvApprlItemDetails.Rows[e.RowIndex].Cells[2].Text;

        DataTable IndentApprovalProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ITEM_CODE");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("ITEM_MODEL_NO");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IT_TYPE");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_ID");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("UOM_SHORT_DESC");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_DET_QTY");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_DET_BRAND");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_DET_REQ_FOR");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("COLOUR_NAME");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("COLOR_ID");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_DET_ID");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_DET_REM_QTY");
        IndentApprovalProducts.Columns.Add(col);
        col = new DataColumn("IND_DET_ORD_QTY");
        IndentApprovalProducts.Columns.Add(col);

        if (gvApprlItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvApprlItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ITEM_CODE"] = gvrow.Cells[2].Text;
                    dr["ITEM_MODEL_NO"] = gvrow.Cells[3].Text;
                    dr["IT_TYPE"] = gvrow.Cells[4].Text;
                    dr["IND_ID"] = gvrow.Cells[5].Text;
                    dr["UOM_SHORT_DESC"] = gvrow.Cells[6].Text;
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    dr["IND_DET_QTY"] = qty.Text;
                    dr["IND_DET_BRAND"] = gvrow.Cells[8].Text;
                    dr["IND_DET_REQ_FOR"] = gvrow.Cells[9].Text;
                    dr["COLOUR_NAME"] = gvrow.Cells[10].Text;
                    dr["COLOR_ID"] = gvrow.Cells[11].Text;
                    dr["IND_DET_ID"] = gvrow.Cells[12].Text;
                    dr["IND_DET_REM_QTY"] = gvrow.Cells[13].Text;
                    dr["IND_DET_ORD_QTY"] = gvrow.Cells[14].Text;

                    IndentApprovalProducts.Rows.Add(dr);
                }
            }
        }
        gvApprlItemDetails.DataSource = IndentApprovalProducts;
        gvApprlItemDetails.DataBind();
    }
}


 
