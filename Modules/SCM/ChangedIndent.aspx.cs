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
public partial class Modules_SCM_ChangedIndent : basePage
{
    decimal TotalAmount1 = 0;

    decimal TotalAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            setControlsVisibility();

            //SM.DDLBindWithSelect(ddlDepartment, "Select DEPT_ID,DEPT_NAME from YANTRA_DEPT_MAST where dept_name is not null");
            //SM.DDLBindWithSelect(ddlFollowUp, "select EMP_ID,EMP_FIRST_NAME from YANTRA_EMPLOYEE_MAST where emp_first_name is not null");
            //SM.DDLBindWithSelect(ddlPreparedBy, "select EMP_ID,EMP_FIRST_NAME from YANTRA_EMPLOYEE_MAST where emp_first_name is not null");
            //SM.DDLBindWithSelect(ddlApprovedBy, "select EMP_ID,EMP_FIRST_NAME from YANTRA_EMPLOYEE_MAST where emp_first_name is not null");
            //SM.DDLBindWithSelect(ddlSupplierName, "select CUST_ID,CUST_NAME from YANTRA_CUSTOMER_MAST where CUST_NAME is not null");
            //SM.DDLBindWithSelect(ddlItemType, "select ITEM_CODE,ITEM_MODEL_NO from YANTRA_ITEM_MAST where ITEM_MODEL_NO is not null");
            SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
            ////SM.DDLBindWithSelect(ddlOrderAcceptance,"select 
            SalesOrder_Fill();
            Department_Fill();
            EmployeeMaster_Fill();
            CustomerName_Fill();
            gvItemDetails.DataBind();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "64");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btngo2.Enabled = up.Go2;
        //btnGo.Enabled = up.Go;
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnSave.Enabled = up.Save;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        //btnPrint.Enabled = up.Print;
        //btnApprove.Enabled = up.Approve;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvIndentDetails.SelectedRow.Cells[5].Text) && gvIndentDetails.SelectedRow.Cells[5].Text != "&nbsp;")
            {
                btnApprove.Visible = false;
                // btnSave.Visible = false;
                btnRefresh.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                
            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
                //btnSend.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                //btnSendWorkOrder.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            // btnApprove.Visible = false;
            //btnPrint.Visible = false;
           // btnSend.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
                //rdbAll.Visible = true;
                //lblSelectModel.Visible = true;
            }
            else
            {
                btnDelete.Visible = true;
                btnEdit.Visible = false;
                //rdbAll.Visible = false;
                //lblSelectModel.Visible = false;
            }
            //btnSendWorkOrder.Visible = false;
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
           // SM.Dispose();
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
           // SM.Dispose();
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
           // Masters.Dispose();
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
            //HR.Dispose();
        }
    }
    #endregion


    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangedIndentDetails.aspx");
        tblIndentDetails.Visible = true;
        SCM.ClearControls(this);
        txtIndentNo.Text = SCM.Indent.Indent_AutoGenCode();
        txtIndentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        rdblIndentfor.SelectedValue = "Self";
        rdblIndentfor_SelectedIndexChanged(sender, e);
        txtReqByDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        gvSalesOrderItems.DataBind();
        gvItemDetails.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.SelectedIndex > -1)
        {
            Response.Redirect("ChangedIndentDetails.aspx?IndentId=" + gvIndentDetails.SelectedRow.Cells[0].Text +
                "&AppBy=" + gvIndentDetails.SelectedRow.Cells[5].Text+"&Cust="+gvIndentDetails.SelectedRow.Cells[3].Text);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
        
        //Old Code
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
                    if (objIndent.INDENTFOR == "Self")
                    {
                        rdblIndentfor.SelectedValue = "Self";
                    }
                    if (objIndent.INDENTFOR == "Customer")
                    {
                        rdblIndentfor.SelectedValue = "Customer";
                        rdblIndentfor_SelectedIndexChanged(sender, e);
                        ddlSupplierName.SelectedIndex = ddlSupplierName.Items.IndexOf(ddlSupplierName.Items.FindByValue(objIndent.INDDetReqFor));
  
                        //ddlSupplierName.SelectedItem.Text = objIndent.INDDetReqFor;
                        ddlSupplierName_SelectedIndexChanged(sender, e);
                        ddlOrderAcceptance.SelectedItem.Value = objIndent.INDSoId;
                        ddlOrderAcceptance_SelectedIndexChanged(sender, e);
                        
                       
                    }


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
              //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
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
              //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlOrderAcceptance, "select YANTRA_SO_MAST.SO_ID,YANTRA_SO_MAST.SO_NO  from  YANTRA_SO_MAST INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_SO_MAST.SO_CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID where YANTRA_CUSTOMER_MAST.CUST_ID = " + ddlSupplierName.SelectedItem.Value);
    }
    protected void ddlOrderAcceptance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.SalesOrder objSO = new SM.SalesOrder();
            objSO.SalesOrderDetails_Select1(ddlOrderAcceptance.SelectedItem.Value, gvSalesOrderItems);
            ItemcodefillPO(ddlOrderAcceptance.SelectedItem.Value);
        
        
        }
        catch
        {

        }
        finally
        {

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvSalesOrderItems.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("ChkItemSelect");
            if (ch.Checked == true)
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
                                dr["ItemCode"] = gvrow.Cells[0].Text;
                                dr["ModelNo"] = gvrow.Cells[1].Text;
                                dr["ItemName"] = gvrow.Cells[2].Text;
                                dr["UOM"] = gvrow.Cells[3].Text;
                                dr["Quantity"] = gvrow.Cells[4].Text;
                                dr["Priority"] = "---";
                                dr["Brand"] = gvrow.Cells[14].Text;

                                dr["Specification"] = "---";
                                dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                                dr["ReqDate"] = gvrow.Cells[10].Text;
                                dr["Room"] = "--";
                                //dr["Priority"] = "---";
                                dr["Color"] = gvrow.Cells[12].Text;
                                dr["ColorId"] = gvrow.Cells[13].Text;


                                SalesOrderItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ModelNo"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                dr["Quantity"] = gvrow1.Cells[6].Text;
                                //dr["Currency"] = gvrow1.Cells[7].Text;
                                //dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                               // dr["Rate"] = gvrow1.Cells[5].Text;
                                dr["Specification"] = "---";
                                //dr["Remarks"] = gvrow1.Cells[10].Text;
                                dr["Brand"] = gvrow1.Cells[8].Text;
                                dr["ReqDate"] = gvrow1.Cells[11].Text;
                                dr["Room"] = gvrow1.Cells[10].Text;
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
                            dr["ModelNo"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;
                            //dr["Currency"] = gvrow1.Cells[7].Text;
                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                            //dr["Rate"] = gvrow1.Cells[5].Text;
                            dr["Specification"] = "---";
                            // dr["Remarks"] = gvrow1.Cells[10].Text;
                            dr["Brand"] = gvrow1.Cells[8].Text;
                            dr["ReqDate"] = gvrow1.Cells[11].Text;
                            dr["Room"] = gvrow1.Cells[10].Text;
                            dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                            dr["Priority"] = "---";

                            dr["Color"] = gvrow1.Cells[14].Text;
                            dr["ColorId"] = gvrow1.Cells[15].Text;

                            SalesOrderItems.Rows.Add(dr);
                        }
                        if (gvItemDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[1].Text == gvrow1.Cells[1].Text)
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
                    drnew["ItemCode"] = gvrow.Cells[0].Text;
                    drnew["ModelNo"] = gvrow.Cells[1].Text;
                    drnew["ItemName"] = gvrow.Cells[2].Text;
                    drnew["UOM"] = gvrow.Cells[3].Text;
                    drnew["Quantity"] = gvrow.Cells[4].Text;
                    drnew["Brand"] = gvrow.Cells[14].Text;
                    drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                    //drnew["Rate"] = gvrow.Cells[8].Text;
                    drnew["Specification"] = "---";
                    //drnew["Remarks"] = "--";
                    drnew["Priority"] = "--";
                    drnew["ReqDate"] = gvrow.Cells[10].Text;
                    drnew["Room"] = gvrow.Cells[8].Text;

                    drnew["Color"] = gvrow.Cells[12].Text;
                    drnew["ColorId"] = gvrow.Cells[13].Text;

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
    
    //protected void gvSalesOrderItems_RowDataBound1(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = e.Row.Cells[9].Visible = e.Row.Cells[17].Visible = false;
    //    }

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    { e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[6].Text))).ToString(); }

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[8].Text);
    //    }

    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[7].Text = "Total Amount:";
    //        e.Row.Cells[8].Text = TotalAmount.ToString();
    //        e.Row.Cells[10].Visible = false;
    //        e.Row.Cells[11].Visible = false;
    //        e.Row.Cells[9].Visible = false;
    //        e.Row.Cells[17].Visible = false; 
    //        e.Row.Cells[0].Visible = false;
    //        e.Row.Cells[1].Visible = false;
    //    }



    //}
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemType.DataSourceID = "SqlDataSource2";
        ddlItemType.DataTextField = "ITEM_MODEL_NO";
        ddlItemType.DataValueField = "ITEM_CODE";
        ddlItemType.DataBind();
        ddlItemType_SelectedIndexChanged(sender, e);
    }
    protected void ddlBrandselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemType,"SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrandselect.SelectedItem.Value);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable IndentProducts = new DataTable();

        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
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
                        drnew["ModelNo"] = ddlItemType.SelectedItem.Text;
                        drnew["ItemName"] = txtModelName.Text;
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
                        if (rdblIndentfor.SelectedItem.Text == "Customer")
                        {
                            drnew["SuggestedParty"] = ddlSupplierName.SelectedItem.Text;
                        }
                        else
                        {
                            drnew["SuggestedParty"] = "Self";
                        }
                        if (rdblIndentfor.SelectedItem.Text == "Customer")
                        {

                            drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                        }
                        else
                        {
                            drnew["ReqFor"] = "Self";
                        }
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
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
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
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
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
            drnew["ModelNo"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = txtModelName.Text;
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
            if (rdblIndentfor.SelectedItem.Text == "Customer")
            {
                drnew["SuggestedParty"] = ddlSupplierName.SelectedItem.Text;
            }
            else
            {
                drnew["SuggestedParty"] = "Self";
            }
            if (rdblIndentfor.SelectedItem.Text == "Customer")
            {
                drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
            }
            else
            {
                drnew["ReqFor"] = "Self";
            }
            //drnew["SuggestedParty"] = ddlSupplierName.SelectedItem.Text;
            //drnew["ReqFor"] = ddlSupplierName.SelectedItem.Text;
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
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
       // ddlItemType.SelectedValue = "0";
        txtModelName.Text = string.Empty;
        //ddlItemType.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtQuantityInHand.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtBalanceQty.Text = string.Empty;
        ddlItemPriority.SelectedValue = "0";
        txtBrand.Text = string.Empty;
        ddlBrandselect.SelectedValue = "0";
        //txtRequiredFor.Text = string.Empty;
        //txtReqByDate.Text = string.Empty;
        txtSpecification.Text = string.Empty;
        //txtColor.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        //ddlSupplierName.SelectedValue = "0";
         ddlColor.SelectedValue = "0";
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
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
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
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
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        
        DataTable IndentProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
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
               // Itemcodefill1(gvrow.Cells[2].Text);
                DataRow dr = IndentProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
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
                   // Itemcodefill1(gvrow.Cells[2].Text);
                    ddlItemType.SelectedItem.Value = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    ddlItemType.SelectedItem.Text = gvrow.Cells[3].Text;
                    ddlItemPriority.SelectedValue = gvrow.Cells[7].Text;
                    txtReqByDate.Text = gvrow.Cells[11].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    //txtItemRate.Text = gvrow.Cells[7].Text;
                    //txtRequiredFor.Text = gvrow.Cells[10].Text;
                   // ddlSupplierName.SelectedItem.Text = gvrow.Cells[9].Text;
                    txtSpecification.Text = gvrow.Cells[12].Text;
                    //ddlColor.SelectedValue = gvrow.Cells[14].Text;
                    ddlColor.SelectedItem.Text= gvrow.Cells[14].Text;

                    gvItemDetails.SelectedIndex = gvrow.RowIndex;

                }
            }
        }
    }

    private void Itemcodefill()
    {
        try
        {
            SM.DDLBindWithSelect(ddlItemType, "select ITEM_CODE,ITEM_MODEL_NO from YANTRA_ITEM_MAST where ITEM_MODEL_NO is not null");
        }
        catch (Exception ex)
        {
           
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally 
        {
           // SM.Dispose();
        }
    }



    private void Itemcodefill1(string itemcode)
    {
        try
        {
            SM.DDLBindWithSelect(ddlItemType, "select ITEM_CODE,ITEM_MODEL_NO from YANTRA_ITEM_MAST where   YANTRA_ITEM_MAST.Item_code = '"+itemcode+"'   and  ITEM_MODEL_NO is not null");
        }
        catch (Exception ex)
        {

            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            
        }
    }


    private void ItemcodefillPO(string PO)
    {
        try
        {
            SM.DDLBindWithSelect(ddlItemType, "select YANTRA_ITEM_MAST.ITEM_CODE,YANTRA_ITEM_MAST.ITEM_MODEL_NO from YANTRA_ITEM_MAST,YANTRA_SO_MAST,YANTRA_SO_DET where YANTRA_SO_MAST.SO_Id = YANTRA_SO_DET.SO_ID and YANTRA_SO_DET.Item_code = YANTRA_ITEM_MAST.item_code and YANTRA_SO_DET.SO_ID = '" + PO + "'   ");
        }
        catch (Exception ex)
        {

            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {

        }
    }


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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            IndentSave();
        }
        else if (btnSave.Text == "Update")
        {
            IndentUpdate();
        }
    }

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
                if (rdblIndentfor.SelectedItem.Text == "Customer")
                {

                    objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                    objSCM.INDENTFOR = "Customer";
                    objSCM.Custid = ddlSupplierName.SelectedItem.Value;
                }
                else
                {
                    objSCM.INDSoId = "0";
                    objSCM.INDENTFOR = "Self";
                    objSCM.Custid = "0";
                }
                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.CP_ID = cp.getPresentCompanySessionValue();
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
                        if (rdblIndentfor.SelectedItem.Text == "Customer")
                        {
                            objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                        }
                        else
                        {
                            objSCM.INDSoId = "0";
                        }
                       
                        objSCM.IndColor = gvrow.Cells[15].Text;
                        objSCM.INDDetStatus = "New";

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
              //  SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Indent");
        }
    }

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
                if (rdblIndentfor.SelectedItem.Text == "Customer")
                {

                    objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                    objSCM.INDENTFOR = "Customer";
                    objSCM.Custid = ddlSupplierName.SelectedItem.Value;
                }
                else
                {
                    objSCM.INDSoId = "0";
                    objSCM.INDENTFOR = "Self";
                    objSCM.Custid = "0";
                }
                objSCM.Status = "New";
                

                objSCM.CP_ID = cp.getPresentCompanySessionValue();
                objSCM.INDPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDApprovedBy = "0";// Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
               
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
                        if (rdblIndentfor.SelectedItem.Text == "Customer")
                        {
                            objSCM.INDSoId = ddlOrderAcceptance.SelectedItem.Value;
                        }
                        else
                        {
                            objSCM.INDSoId = "0";
                        }
                        objSCM.IndColor = gvrow.Cells[15].Text;
                        objSCM.INDDetStatus = "New";
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
                gvSalesOrderItems.DataBind();
                tblIndentDetails.Visible = false;
               // SCM.Dispose();
                
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Indent");
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblIndentDetails.Visible = false;
        SCM.ClearControls(this);
    }
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
  
    protected void gvIndentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvIndentDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy.SelectedItem.Text == "Indent Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvIndentDetails.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSearchBy.SelectedItem.Text == "Indent Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchText.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;

    }
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    protected void lbtnIndentNo_Click(object sender, EventArgs e)
    {
        LinkButton lbnIndentNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbnIndentNo.Parent.Parent;
        gvIndentDetails.SelectedIndex = Row.RowIndex;
        //Response.Redirect("ChangedIndentDetails.aspx?IndentId=" + gvIndentDetails.SelectedRow.Cells[0].Text +
        //        "&AppBy=" + gvIndentDetails.SelectedRow.Cells[5].Text);

        

        //Old Code
        tblIndentDetails.Visible = false;
        LinkButton lbtnIndentNo;
        lbtnIndentNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIndentNo.Parent.Parent;
        gvIndentDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

      
    }
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();

            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {

               // txtQuantityInHand.Text = objMaster.ItemQtyInHand;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtSpecification.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtSpecification.Text = objMaster.ItemSpec;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + ddlItemType.SelectedItem.Value + "";

            }
            if (objMaster.QTYInHand_select(ddlItemType.SelectedItem.Value) > 0)
            {
                if (objMaster.itemquantity == "")
                {
                    txtQuantityInHand.Text = "0";
                }
                else
                {
                    txtQuantityInHand.Text = objMaster.itemquantity;
                }
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedItem.Value);


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
    

    protected void rdblIndentfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rdblIndentfor.SelectedItem.Text == "Customer")
        {
            tblPoDetails.Visible = true;
            gvSalesOrderItems.DataBind();
        }
        else
        {
            tblPoDetails.Visible = false;
        }
    }




    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
         Masters.ItemMaster objMaster = new Masters.ItemMaster();

        if (objMaster.QTYInHandCOlor_select(ddlItemType.SelectedItem.Value,ddlColor.SelectedItem.Value) > 0)
        {
            if (objMaster.itemquantity == "")
            {
                txtQuantityInHand.Text = "0";
            }
            else
            {
                txtQuantityInHand.Text = objMaster.itemquantity;
            }
        }
    }
    protected void gvSalesOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
               e.Row.Cells[13].Visible = false;
            }
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) * (Convert.ToDouble(e.Row.Cells[4].Text))).ToString();
         //   TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[9].Text);
            //lblTotalamount.Text = TotalAmount.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // TotalAmount1 = TotalAmount1 + Convert.ToDecimal(e.Row.Cells[10].Text);
           // lblTotalamount.Text = TotalAmount1.ToString();
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[8].Text = "Total Amount:";
        //    e.Row.Cells[9].Text = TotalAmount1.ToString();
        //    e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[15].Visible = false;
        //}

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster.EmployeeMaster_SelectFromDepartment(ddlFollowUp, ddlDepartment.SelectedItem.Value);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {

            SCM.Indent objSCM = new SCM.Indent();
           // SCM.BeginTransaction();
            objSCM.INDId = gvIndentDetails.SelectedRow.Cells[0].Text;
            objSCM.INDApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSCM.Status = "Open";

            if (objSCM.IndentApprove_Update() == "Status Updated Successfully")
                MessageBox.Show(this, "Indent Approved Successfully");
           // SCM.CommitTransaction();
        }
        catch (Exception ex)
        {
           // SCM.Dispose();
          //  SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

           // SCM.Dispose();
            gvIndentDetails.DataBind();
            btnClose_Click(sender, e);
        }
    }
    protected void btngo2_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text != "")
        {
            ddlSupplierName.DataSourceID = "SqlDataSource1";
            ddlSupplierName.DataTextField = "CUST_NAME";
            ddlSupplierName.DataValueField = "CUST_ID";
            ddlSupplierName.DataBind();
            ddlSupplierName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvIndentDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvIndentDetails.DataBind();
    }
}

 

