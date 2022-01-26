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

public partial class Modules_SCM_SuppliersEnquiryNew : basePage
{
    string supEnqNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        supEnqNo = Request.QueryString["supEnqNo"];

        if(!IsPostBack)
        {
            if (supEnqNo == null)
            {
                // gvSupEnqDetails.SelectedIndex = -1;
                //btnDelete.Attributes.Clear();
                SCM.ClearControls(this);
                txtEnquiryNo.Text = SCM.SuppliersEnquiry.SuppliersEnquiry_AutoGenCode();
                txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                tblSupEnqDetails.Visible = true;
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                gvItem.DataBind();
                gvItemDetails.DataBind();
                gvSupplierDetails.DataBind();
            }
            else
            {
                LoadSuppliersEnquiryDtls();
                try
                {
                    SCM.SuppliersEnquiry objSCM = new SCM.SuppliersEnquiry();
                    if (objSCM.SuppliersEnquiryMaster_Select(supEnqNo) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblSupEnqDetails.Visible = true;
                        txtEnquiryNo.Text = objSCM.SuppEnqNo;
                        txtEnquiryDate.Text = objSCM.SuppEnqDate;
                        txtEnquiryStatus.Text = objSCM.SuppEnqStatus;
                        ddlCriteria.SelectedValue = objSCM.SuppEnqFollwUp;
                        txtEnquiryDueDate.Text = objSCM.SuppEnqDueDate;
                        ddlDeliveryType.SelectedValue = objSCM.SuppEnqDespId;
                        ddlPreparedBy.SelectedValue = objSCM.SuppEnqPreparedBy;
                        ddlApprovedBy.SelectedValue = objSCM.SuppEnqApprovedBy;
                        objSCM.SuppliersEnquiryDetails_Select(supEnqNo, gvItemDetails);
                        objSCM.EnquirySuppliersDetails_Select(supEnqNo, gvSupplierDetails);
                        gvItem.DataBind();
                        // ddlIndentApprovel_SelectedIndexChanged(sender, e);

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

            //gvSupEnqDetails.DataBind();
            DeliveryType_Fill();
            ItemTypes_Fill();
            EmployeeMaster_Fill();
            SuppName_Fill();
            IndentApproval_Fill();
            FillBrand();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            if (Request.QueryString["SeId"] != null)
            {
                //btnNew_Click(sender, e);
                ddlIndentApprovel.SelectedValue = Request.QueryString["SeId"].ToString();
                ddlIndentApprovel_SelectedIndexChanged(sender, e);
            }
        }

    }

    #region LoadSuppliersEnquiryDtls
    private void LoadSuppliersEnquiryDtls()
    {
        try
        {
            SCM.SuppliersEnquiry objSCM = new SCM.SuppliersEnquiry();
            if (objSCM.SuppliersEnquiryMaster_Select(supEnqNo) > 0)
            {
                btnSave.Text = "Update";
                btnSave.Enabled = true;
                tblSupEnqDetails.Visible = true;
                txtEnquiryNo.Text = objSCM.SuppEnqNo;
                txtEnquiryDate.Text = objSCM.SuppEnqDate;
                txtEnquiryStatus.Text = objSCM.SuppEnqStatus;
                ddlCriteria.SelectedValue = objSCM.SuppEnqFollwUp;
                txtEnquiryDueDate.Text = objSCM.SuppEnqDueDate;
                ddlDeliveryType.SelectedValue = objSCM.SuppEnqDespId;
                ddlPreparedBy.SelectedValue = objSCM.SuppEnqPreparedBy;
                ddlApprovedBy.SelectedValue = objSCM.SuppEnqApprovedBy;

                objSCM.SuppliersEnquiryDetails_Select(supEnqNo, gvItemDetails);
                objSCM.EnquirySuppliersDetails_Select(supEnqNo, gvSupplierDetails);
                //ddlIndentApprovel_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
           // btnDelete.Attributes.Clear();
            SCM.Dispose();
        }
    }
    #endregion

    #region Print
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (supEnqNo != null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SupEnq&SupDetid=" + supEnqNo + "";
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

    #region Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (supEnqNo !=null)
        {
            try
            {
                SCM.SuppliersEnquiry objSCM = new SCM.SuppliersEnquiry();
                MessageBox.Show(this, objSCM.SuppliersEnquiryMaster_Delete(supEnqNo));

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                //gvSupEnqDetails.DataBind();
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

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
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

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDeliveryType);

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

    #region Indentapproval Fill
    private void IndentApproval_Fill()
    {
        try
        {
            SCM.IndentApproval.IndentApproval_Select1(ddlIndentApprovel);
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
    #endregion

   
    #region SuppName Fill
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
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectForPurchases(ddlCriteria);

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
   
    #region DDl brand select change
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
            SCM.SuppliersMaster.SuppliersMaster_SelectForBrand(ddlSupplierName, ddlBrand.SelectedValue);
            SCM.Indent objIndentApproval = new SCM.Indent();
            objIndentApproval.IndentDetailsBrand_Select(ddlBrand.SelectedItem.Text, gvItem);
            gvItemDetails.DataBind();
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
    #endregion

    #region ddlIndentApprovel select Change
    protected void ddlIndentApprovel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SCM.IndentApproval objIndentApproval = new SCM.IndentApproval();
        //objIndentApproval.IndentApprovalDetails_Select(ddlIndentApprovel.SelectedItem.Value, gvApprlItemDetails);
    }
    #endregion

    #region DDlsupplier name change
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
    #endregion

    #region Button SupplierDetails
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
    #endregion

    #region SuppDetails Refresh
    protected void btnSuppRefresh_Click(object sender, EventArgs e)
    {
        ddlSupplierName.SelectedValue = "0";
        txtPhoneNo.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        txtAddress.Text = string.Empty;
    }
    #endregion

    #region Go click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvItem.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {

                DataTable IndentApprovalProducts = new DataTable();

                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentApprovalProducts.Columns.Add(col);
                //col = new DataColumn("ItemGroup");
                //IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                IndentApprovalProducts.Columns.Add(col);
                //col = new DataColumn("BalQty");
                //IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                //col = new DataColumn("SupplierName");
                //IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ReqFor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ReqDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                IndentApprovalProducts.Columns.Add(col);

                if (gvItemDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
                    {
                        DataRow dr = IndentApprovalProducts.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[2].Text;
                        dr["ItemName"] = gvrow1.Cells[3].Text;
                        dr["ItemType"] = gvrow1.Cells[4].Text;
                        dr["UOM"] = gvrow1.Cells[5].Text;
                        dr["Quantity"] = gvrow1.Cells[6].Text;
                        dr["Priority"] = gvrow1.Cells[7].Text;
                        dr["Brand"] = gvrow1.Cells[8].Text;
                        dr["ReqFor"] = gvrow1.Cells[9].Text;
                        // dr["ReqDate"] = gvrow1.Cells[11].Text;
                        dr["Specification"] = gvrow1.Cells[10].Text;

                        IndentApprovalProducts.Rows.Add(dr);
                    }
                }

                if (gvItem.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
                    {
                        if (gvrow1.Cells[2].Text == gvrow.Cells[2].Text)
                        {
                            gvrow1.Cells[6].Text = Convert.ToString((Convert.ToInt16(gvrow1.Cells[6].Text) + Convert.ToInt16(gvrow.Cells[6].Text)));


                            //gvItemDetails.DataSource = IndentApprovalProducts;
                            //gvItemDetails.DataBind();
                            //MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            //  ch.Checked = false;
                            //return ;
                            goto come;
                        }

                    }
                }

                DataRow drnew = IndentApprovalProducts.NewRow();
                drnew["ItemCode"] = gvrow.Cells[2].Text;
                drnew["ItemName"] = gvrow.Cells[3].Text;
                drnew["ItemType"] = gvrow.Cells[4].Text;
                drnew["UOM"] = gvrow.Cells[5].Text;
                drnew["Quantity"] = gvrow.Cells[6].Text;
                drnew["Priority"] = gvrow.Cells[7].Text;
                drnew["Brand"] = gvrow.Cells[8].Text;
                drnew["ReqDate"] = gvrow.Cells[11].Text;
                drnew["Specification"] = gvrow.Cells[12].Text;

                IndentApprovalProducts.Rows.Add(drnew);
                gvItemDetails.DataSource = IndentApprovalProducts;
                gvItemDetails.DataBind();
            //btnRefresh_Click(sender, e);
            // ch.Checked = false;

            come:
                {
                }


            }
        }
    }
    #endregion

    #region ddlItem type
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                // txtQuantity.Text = objMaster.ItemQtyInHand;
                txtUOM.Text = objMaster.ItemUOMShort;
                txtSpecifications.Text = objMaster.ItemSpec;
                txtCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                //txtSpecification.Text = objMaster.ItemSpec;
                txtColor.Text = objMaster.Color;
                // txtSupplierName.Text = objMaster.ItemPrincipalName;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";


            }
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

    #region Button Intrested Product
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable ItemDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("ItemName");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("ItemType");
        ItemDetails.Columns.Add(col);

        col = new DataColumn("UOM");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("Quantity");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("Priority");
        ItemDetails.Columns.Add(col);

        col = new DataColumn("Brand");
        ItemDetails.Columns.Add(col);
        //col = new DataColumn("SuggestedParty");
        //ItemDetails.Columns.Add(col);
        col = new DataColumn("ReqFor");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("ReqDate");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("Specification");
        ItemDetails.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = ItemDetails.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["ItemType"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Priority"] = gvrow.Cells[7].Text;
                dr["Brand"] = gvrow.Cells[8].Text;
                // dr["SuggestedParty"] = gvrow.Cells[9].Text;
                dr["ReqFor"] = gvrow.Cells[9].Text;
                dr["ReqDate"] = gvrow.Cells[10].Text;
                dr["Specification"] = gvrow.Cells[11].Text;
                ItemDetails.Rows.Add(dr);
            }
        }

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.Cells[3].Text == ddlItemType.SelectedItem.Value)
                {
                    gvItemDetails.DataSource = ItemDetails;
                    gvItemDetails.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = ItemDetails.NewRow();
        drnew["ItemCode"] = ddlItemType.SelectedItem.Value;
        drnew["ItemName"] = ddlItemType.SelectedItem.Text;
        drnew["ItemType"] = txtModelName.Text;
        drnew["UOM"] = txtUOM.Text;
        drnew["Quantity"] = txtQuantity.Text;
        drnew["Priority"] = ddlItemPriority.SelectedItem.Value;
        drnew["Brand"] = txtBrand.Text;
        //drnew["SuggestedParty"] = txtSupplierName.Text;
        // drnew["ReqFor"] = txtRoom.Text;
        // drnew["ReqDate"] = txtReqByDate.Text;
        drnew["Specification"] = txtSpecifications.Text;
        if (txtBrand.Text == string.Empty)
        {
            drnew["Brand"] = "--";
        }
        else
        {
            drnew["Brand"] = txtBrand.Text;


        }

        if (txtSpecifications.Text == string.Empty)
        {
            drnew["Specification"] = "--";
        }
        else
        {
            drnew["Specification"] = txtSpecifications.Text;


        }
        ItemDetails.Rows.Add(drnew);
        gvItemDetails.DataSource = ItemDetails;
        gvItemDetails.DataBind();
        btnIntrstProduct_Click(sender, e);

    }
    #endregion

    #region Intrest Product Details Refresh
    protected void btnIntrstProduct_Click(object sender, EventArgs e)
    {
        txtCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        ddlItemType.SelectedValue = "0";
        txtModelName.Text = string.Empty;
        //ddlItemType.SelectedValue = "0";
        txtUOM.Text = string.Empty;
        //txtQuantityInHand.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        //txtBalanceQty.Text = string.Empty;
        ddlItemPriority.SelectedValue = "0";
        txtBrand.Text = string.Empty;
        // txtSupplierName.Text = string.Empty;
        // txtRoom.Text = string.Empty;
        //txtReqByDate.Text = string.Empty;
        txtSpecifications.Text = string.Empty;
        txtColor.Text = string.Empty;
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SuppliersEnquiryMasterSave();

        }
        else if (btnSave.Text == "Update")
        {
            SuppliersEnquiryMasterUpdate();
        }

    }
    #endregion


    #region SuppliersEnquiryMasterSave
    private void SuppliersEnquiryMasterSave()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            if (gvSupplierDetails.Rows.Count > 0)
            {
                try
                {
                    SCM.SuppliersEnquiry objSCM = new SCM.SuppliersEnquiry();
                    SCM.BeginTransaction();
                    objSCM.SuppEnqNo = txtEnquiryNo.Text;
                    objSCM.SuppEnqDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDate.Text);
                    if (rdoDirectSupp.Checked == true)
                    {
                        objSCM.SuppEnqOrgBy = rdoDirectSupp.Text;
                    }
                    else if (rdoConsult.Checked == true)
                    {
                        objSCM.SuppEnqOrgBy = rdoConsult.Text;
                    }
                    objSCM.SuppEnqStatus = "New";
                    objSCM.SuppEnqFollwUp = ddlCriteria.SelectedItem.Value;
                    objSCM.SuppEnqDueDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDueDate.Text);
                    objSCM.SuppEnqDespId = ddlDeliveryType.SelectedItem.Value; ;
                    objSCM.SuppEnqPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    objSCM.SuppEnqApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    objSCM.CpId = lblCPID.Text;
                    if (objSCM.SuppliersEnquiryMater_Save() == "Data Saved Successfully")
                    {
                        objSCM.SuppliersEnquiryDetails_Delete(objSCM.SuppEnqId);
                        foreach (GridViewRow gvrow in gvItemDetails.Rows)
                        {
                            objSCM.SuppEnqItemCode = gvrow.Cells[2].Text;
                            objSCM.SuppEnqDetQty = gvrow.Cells[6].Text;
                            objSCM.SuppEnqDetPriority = gvrow.Cells[7].Text;
                            objSCM.SuppEnqDetSpec = gvrow.Cells[10].Text;
                            objSCM.SuppEnqDetReqFor = gvrow.Cells[9].Text;
                            objSCM.SuppEnqDetBrand = gvrow.Cells[8].Text;
                            objSCM.SuppEnqDetStatus = "New";
                            objSCM.SuppliersEnquiryDetails_Save();
                        }

                        objSCM.EnqSuppliersDetails_Delete(objSCM.SuppEnqId);
                        foreach (GridViewRow gvrow in gvSupplierDetails.Rows)
                        {
                            objSCM.SupId = gvrow.Cells[1].Text;

                            objSCM.EnqSuppliersDetails_Save();
                        }




                        SCM.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
                    else
                    {
                        SCM.RollBackTransaction();
                    }
                    foreach (GridViewRow gvrow in gvItem.Rows)
                    {
                        CheckBox ch = new CheckBox();
                        ch = (CheckBox)gvrow.FindControl("chk");
                        if (ch.Checked == true)
                        {
                            objSCM.CustId = gvrow.Cells[16].Text;
                            objSCM.UpdateIndApprovalDetails(objSCM.CustId);
                        }
                    }

                }
                catch (Exception ex)
                {
                    SCM.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    tblSupEnqDetails.Visible = false;
                    gvItemDetails.DataBind();
                    gvSupplierDetails.DataBind();
                   // btnDelete.Attributes.Clear();
                   // gvSupEnqDetails.DataBind();
                    SCM.ClearControls(this);
                    SCM.Dispose();
                }
            }

            else
            {
                MessageBox.Show(this, "Please Add Atleast one Supplier");
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }

    }
    #endregion

    #region SuppliersEnquiryMasterUpdate
    private void SuppliersEnquiryMasterUpdate()
    {
        if (gvItemDetails.Rows.Count > 0)
        {
            try
            {
                SCM.SuppliersEnquiry objSCM = new SCM.SuppliersEnquiry();
                SCM.BeginTransaction();

                objSCM.SuppEnqId = supEnqNo;

                objSCM.SuppEnqNo = txtEnquiryNo.Text;
                objSCM.SuppEnqDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDate.Text);
                if (rdoDirectSupp.Checked == true)
                {
                    objSCM.SuppEnqOrgBy = rdoDirectSupp.Text;
                }
                else if (rdoConsult.Checked == true)
                {
                    objSCM.SuppEnqOrgBy = rdoConsult.Text;
                }
                objSCM.SuppEnqStatus = txtEnquiryStatus.Text;
                objSCM.SuppEnqFollwUp = ddlCriteria.SelectedItem.Value;
                objSCM.SuppEnqDueDate = Yantra.Classes.General.toMMDDYYYY(txtEnquiryDueDate.Text);
                objSCM.SuppEnqDespId = ddlDeliveryType.SelectedItem.Value; ;
                objSCM.SuppEnqPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.SuppEnqApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.CpId = lblCPID.Text;

                if (objSCM.SuppliersEnquiryMaster_Update() == "Data Updated Successfully")
                {
                    objSCM.SuppliersEnquiryDetails_Delete(objSCM.SuppEnqId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.SuppEnqItemCode = gvrow.Cells[2].Text;
                        objSCM.SuppEnqDetQty = gvrow.Cells[6].Text;
                        objSCM.SuppEnqDetPriority = gvrow.Cells[7].Text;
                        objSCM.SuppEnqDetSpec = gvrow.Cells[10].Text;
                        objSCM.SuppEnqDetReqFor = gvrow.Cells[9].Text;
                        objSCM.SuppEnqDetBrand = gvrow.Cells[8].Text;
                        objSCM.SuppliersEnquiryDetails_Save();
                    }
                    objSCM.EnqSuppliersDetails_Delete(objSCM.SuppEnqId);
                    foreach (GridViewRow gvrow in gvSupplierDetails.Rows)
                    {
                        objSCM.SupId = gvrow.Cells[1].Text;
                        objSCM.EnqSuppliersDetails_Save();
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
                tblSupEnqDetails.Visible = false;
                gvItemDetails.DataBind();
                gvSupplierDetails.DataBind();
                //btnDelete.Attributes.Clear();
                //gvSupEnqDetails.DataBind();
                SCM.ClearControls(this);
                SCM.Dispose();
                Response.Redirect("SuppliersEnquiry.aspx");

            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }

    }
    #endregion

    #region btnRefresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
        gvItemDetails.DataBind();
        gvSupplierDetails.DataBind();
        gvItem.DataBind();
    }
    #endregion
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SuppliersEnquiry.aspx");
    }

    #region GridView  Supplier Details Row DataBound
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
    #endregion

    #region GridView Supplier Details Row Deleting
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
        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
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
    #endregion

    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[15].Visible = false;
        }

    }

    #region GridView  Items Details Row DataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");

        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }

    #endregion

    #region Gvitemdetails deleted
    protected void gvItemDetails_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    #endregion

    #region GridView  Items Details Row Deleting
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable ItemDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("ItemName");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("ItemType");
        ItemDetails.Columns.Add(col);

        col = new DataColumn("UOM");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("Quantity");
        ItemDetails.Columns.Add(col);
        col = new DataColumn("Priority");
        ItemDetails.Columns.Add(col);

        col = new DataColumn("Brand");
        ItemDetails.Columns.Add(col);
        //col = new DataColumn("SuggestedParty");
        //ItemDetails.Columns.Add(col);
        col = new DataColumn("ReqFor");
        ItemDetails.Columns.Add(col);
        //col = new DataColumn("ReqDate");
        //ItemDetails.Columns.Add(col);
        col = new DataColumn("Specification");
        ItemDetails.Columns.Add(col);
        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = ItemDetails.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["ItemType"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    // dr["SuggestedParty"] = gvrow.Cells[9].Text;
                    dr["ReqFor"] = gvrow.Cells[9].Text;
                    // dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[10].Text;
                    ItemDetails.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = ItemDetails;
        gvItemDetails.DataBind();
    }
    #endregion

    #region gvItem Details
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
        //col = new DataColumn("SuggestedParty");
        //IndentProducts.Columns.Add(col);
        col = new DataColumn("ReqFor");
        IndentProducts.Columns.Add(col);
        //col = new DataColumn("ReqDate");
        //IndentProducts.Columns.Add(col);
        col = new DataColumn("Specification");
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
                //dr["SuggestedParty"] = gvrow.Cells[9].Text;
                dr["ReqFor"] = gvrow.Cells[9].Text;
                //dr["ReqDate"] = gvrow.Cells[11].Text;
                dr["Specification"] = gvrow.Cells[10].Text;


                IndentProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    ddlItemPriority.SelectedValue = gvrow.Cells[7].Text;
                    //txtReqByDate.Text = gvrow.Cells[11].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    //txtItemRate.Text = gvrow.Cells[7].Text;
                    // txtRoom.Text = gvrow.Cells[9].Text;
                    gvItemDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
    }
    #endregion
}
 
