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

public partial class Modules_Inventory_DisplayIndentRequest : basePage
{
    decimal TotalAmount1 = 0;

    decimal TotalAmount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //setControlsVisibility();

            SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
            SalesOrder_Fill();
            Department_Fill();
            EmployeeMaster_Fill();
            CustomerName_Fill();
            gvItemDetails.DataBind();
            if (Request.QueryString["IndentId"] != null)
            {
                try
                {
                    SCM.Indent objIndent = new SCM.Indent();

                    if (objIndent.Indent_Select1(Request.QueryString["IndentId"].ToString()) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblIndentDetails.Visible = true;
                        txtIndentNo.Text = objIndent.INDNo;
                        txtIndentDate.Text = objIndent.INDDate;
                        ddlDepartment.SelectedValue = objIndent.DeptId;
                        ddlDepartment_SelectedIndexChanged(sender, e);
                        ddlFollowUp.SelectedValue = objIndent.FollowUp;
                        //ddlFollowUp.SelectedItem.Value = objIndent.FollowUp;

                        ddlPreparedBy.SelectedValue = objIndent.INDPreparedBy;
                        ddlApprovedBy.SelectedValue = objIndent.INDApprovedBy;
                        if (objIndent.INDENTFOR == "Display")
                        {
                            rdblIndentfor.SelectedValue = "Display";
                        }
                        if (objIndent.INDENTFOR == "Customer")
                        {
                            rdblIndentfor.SelectedValue = "Customer";
                            rdblIndentfor_SelectedIndexChanged(sender, e);
                            //ddlSupplierName.SelectedIndex = ddlSupplierName.Items.IndexOf(ddlSupplierName.Items.FindByValue(objIndent.INDDetReqFor));

                            //ddlSupplierName.SelectedValue = objIndent.Custid;
                            ddlSupplierName.SelectedValue = objIndent.Custid;
                            ddlSupplierName_SelectedIndexChanged(sender, e);
                            ddlOrderAcceptance.SelectedValue = objIndent.INDSoId;
                            ddlOrderAcceptance_SelectedIndexChanged(sender, e);


                        }


                        objIndent.IndentDetails_Select1(Request.QueryString["IndentId"].ToString(), gvItemDetails);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    //btnDelete.Attributes.Clear();
                    //  SCM.Dispose();
                }
            }
            else
            {
                if (Session["GridData"] != null)
                {
                    //DataTable tb = new DataTable();
                    //Session["vl_Indent"] = tb;
                    tblReleasedItems.Visible = true;
                    gvReleasedItems.DataSource = Session["GridData"];
                    gvReleasedItems.DataBind();


                }
                SM.DDLBindWithSelect(ddlBrandselect, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");
                SalesOrder_Fill();
                Department_Fill();
                EmployeeMaster_Fill();
                CustomerName_Fill();
                gvItemDetails.DataBind();

                tblIndentDetails.Visible = true;
                //SCM.ClearControls(this);
                txtIndentNo.Text = SCM.Indent.Indent_AutoGenCode1();
                txtIndentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                rdblIndentfor.SelectedValue = "Display";
                rdblIndentfor_SelectedIndexChanged(sender, e);
                txtReqByDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                gvSalesOrderItems.DataBind();
                gvItemDetails.DataBind();
            }



        }
    }


    //private void setControlsVisibility()
    //{
    //    User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "9");


    //    btnSave.Enabled = up.add;
    //    //btnRefresh.Enabled = up.Refresh;
    //    //btnClose.Enabled = up.Close;
    //    btnPrint.Enabled = up.Print;
    //    btnApprove.Enabled = up.Approve;
    //}

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString["IndentId"] != null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AppBy"].ToString()) && Request.QueryString["AppBy"].ToString() != "&nbsp;")
            {
                btnApprove.Visible = false;
                // btnSave.Visible = false;
                btnRefresh.Visible = false;
                //btnEdit.Visible = false;
                //btnDelete.Visible = false;

            }
            else
            {
                btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = false;
                //btnPrint.Visible = false;
                //btnSend.Visible = false;
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;
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
            //btnEdit.Visible = true;
            //btnDelete.Visible = true;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                //btnDelete.Visible = true;
                //btnEdit.Visible = true;
                //rdbAll.Visible = true;
                //lblSelectModel.Visible = true;
            }
            else
            {
                //btnDelete.Visible = false;
                //btnEdit.Visible = false;
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
            // HR.EmployeeMaster.EmployeeMaster_Select(ddlFollowUp);
            HR.EmployeeMaster.EmployeeMaster_SelectStores(ddlFollowUp);
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
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlOrderAcceptance, "select YANTRA_SO_MAST.SO_ID,YANTRA_SO_MAST.SO_NO  from  YANTRA_SO_MAST INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_SO_MAST.SO_CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID where YANTRA_CUSTOMER_MAST.CUST_ID = " + ddlSupplierName.SelectedItem.Value);
        txtRemarks.Text = ddlSupplierName.SelectedItem.Text;
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
                //col = new DataColumn("Remarks1");
                //SalesOrderItems.Columns.Add(col);
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
                col = new DataColumn("Remarks");
                SalesOrderItems.Columns.Add(col);

                col = new DataColumn("So_Det_Id");
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
                                //TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                                //dr["Quantity"] = qty.Text;                              

                                dr["Priority"] = "High";
                                dr["Brand"] = gvrow.Cells[14].Text;

                                dr["Specification"] = gvrow.Cells[9].Text;
                                dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                                if (txtReqByDate.Text != "")
                                {
                                    dr["ReqDate"] = txtReqByDate.Text;
                                }
                                else
                                {
                                    dr["ReqDate"] = gvrow.Cells[10].Text;
                                }
                                dr["Room"] = "--";
                                //dr["Priority"] = "---";
                                dr["Color"] = gvrow.Cells[12].Text;
                                dr["ColorId"] = gvrow.Cells[13].Text;
                                dr["Remarks"] = ddlSupplierName.SelectedItem.Value;
                                dr["So_Det_Id"] = gvrow.Cells[17].Text;

                                SalesOrderItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = SalesOrderItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ModelNo"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                                dr["Quantity"] = qty.Text;
                                //dr["Quantity"] = gvrow1.Cells[6].Text;
                                //dr["Currency"] = gvrow1.Cells[7].Text;
                                //dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                                // dr["Rate"] = gvrow1.Cells[5].Text;
                                dr["Specification"] = gvrow1.Cells[12].Text;
                                //dr["Remarks"] = gvrow1.Cells[10].Text;
                                dr["Brand"] = gvrow1.Cells[8].Text;
                                dr["ReqDate"] = gvrow1.Cells[11].Text;
                                dr["Room"] = gvrow1.Cells[10].Text;
                                dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                                dr["Priority"] = gvrow1.Cells[13].Text;

                                dr["Color"] = gvrow1.Cells[14].Text;
                                dr["ColorId"] = gvrow1.Cells[15].Text;
                                dr["Remarks"] = gvrow1.Cells[16].Text;
                                dr["So_Det_Id"] = gvrow1.Cells[17].Text;

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
                            TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                            dr["Quantity"] = qty.Text;
                            //dr["Quantity"] = gvrow1.Cells[6].Text;
                            //dr["Currency"] = gvrow1.Cells[7].Text;
                            // dr["Currency"] = ddlCurrencyType.SelectedItem.Text;
                            //dr["Rate"] = gvrow1.Cells[5].Text;
                            dr["Specification"] = gvrow1.Cells[12].Text;
                            // dr["Remarks"] = gvrow1.Cells[10].Text;
                            dr["Brand"] = gvrow1.Cells[8].Text;
                            dr["ReqDate"] = gvrow1.Cells[11].Text;
                            dr["Room"] = gvrow1.Cells[10].Text;
                            dr["ReqFor"] = ddlSupplierName.SelectedItem.Text;
                            dr["Priority"] = gvrow1.Cells[13].Text;
                            dr["Color"] = gvrow1.Cells[14].Text;
                            dr["ColorId"] = gvrow1.Cells[15].Text;
                            dr["Remarks"] = gvrow1.Cells[16].Text;
                            dr["So_Det_Id"] = gvrow1.Cells[17].Text;
                            SalesOrderItems.Rows.Add(dr);
                        }
                        if (gvItemDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[0].Text == gvrow1.Cells[2].Text && gvrow.Cells[13].Text == gvrow1.Cells[15].Text)
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
                    drnew["Specification"] = gvrow.Cells[9].Text;
                    //drnew["Remarks"] = "--";
                    drnew["Priority"] = "High";

                    if (txtReqByDate.Text != "")
                    {
                        drnew["ReqDate"] = txtReqByDate.Text;
                    }
                    else
                    {
                        drnew["ReqDate"] = gvrow.Cells[10].Text;
                    }

                    //drnew["ReqDate"] = gvrow.Cells[10].Text;
                    drnew["Room"] = gvrow.Cells[8].Text;

                    drnew["Color"] = gvrow.Cells[12].Text;
                    drnew["ColorId"] = gvrow.Cells[13].Text;
                    drnew["Remarks"] = ddlSupplierName.SelectedItem.Text;
                    drnew["So_Det_Id"] = gvrow.Cells[17].Text;
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
        ddlItemPriority.SelectedIndex = 3;
        ddlItemType.DataSourceID = "SqlDataSource2";
        ddlItemType.DataTextField = "ITEM_MODEL_NO";
        ddlItemType.DataValueField = "ITEM_CODE";
        ddlItemType.DataBind();
        ddlItemType_SelectedIndexChanged(sender, e);
    }
    protected void ddlBrandselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemType, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrandselect.SelectedItem.Value);
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
        col = new DataColumn("Remarks");
        IndentProducts.Columns.Add(col);
        col = new DataColumn("So_Det_Id");
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
                        if (txtItemUOM.Text == "")
                        {
                            drnew["UOM"] = "-";
                        }
                        else
                        { drnew["UOM"] = txtItemUOM.Text.Trim(); }
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
                        if (txtRemarks.Text == string.Empty)
                        {
                            drnew["Remarks"] = "--";
                        }
                        else
                        {
                            drnew["Remarks"] = txtRemarks.Text;
                        }

                        drnew["So_Det_Id"] = "0";
                        IndentProducts.Rows.Add(drnew);

                    }
                    else
                    {
                        DataRow dr = IndentProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        dr["Quantity"] = qty.Text;
                        //dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Priority"] = gvrow.Cells[7].Text;
                        dr["Brand"] = gvrow.Cells[8].Text;

                        dr["SuggestedParty"] = gvrow.Cells[10].Text;
                        dr["ReqFor"] = gvrow.Cells[9].Text;
                        dr["ReqDate"] = gvrow.Cells[11].Text;
                        dr["Specification"] = gvrow.Cells[12].Text;
                        dr["Color"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;
                        dr["Remarks"] = gvrow.Cells[16].Text;
                        dr["So_Det_Id"] = gvrow.Cells[17].Text;

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
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    dr["Quantity"] = qty.Text;
                    //dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    dr["SuggestedParty"] = gvrow.Cells[10].Text;
                    dr["ReqFor"] = gvrow.Cells[9].Text;
                    dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Remarks"] = gvrow.Cells[16].Text;
                    dr["So_Det_Id"] = gvrow.Cells[17].Text;

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
            if (txtItemUOM.Text == "")
            {
                drnew["UOM"] = "-";
            }
            else
            {
                drnew["UOM"] = txtItemUOM.Text.Trim();
            }
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
            if (txtRemarks.Text == string.Empty)
            {
                drnew["Remarks"] = "--";
            }
            else
            {
                drnew["Remarks"] = txtRemarks.Text;
            }
            drnew["So_Det_Id"] = "0";

            IndentProducts.Rows.Add(drnew);
        }
        gvItemDetails.DataSource = IndentProducts;
        gvItemDetails.SelectedIndex = -1;
        gvItemDetails.EditIndex = -1;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        // ddlItemType.SelectedValue = "0";
        txtModelName.Text = string.Empty;
        ddlItemType.SelectedIndex = 0;
        txtItemUOM.Text = string.Empty;
        txtQuantityInHand.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtBalanceQty.Text = string.Empty;
        ddlItemPriority.SelectedIndex = 0;
        txtBrand.Text = string.Empty;
        ddlBrandselect.SelectedIndex = 0;
        //txtRequiredFor.Text = string.Empty;
        // txtReqByDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        txtSpecification.Text = string.Empty;
        //txtColor.Text = string.Empty;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        //ddlSupplierName.SelectedValue = "0";
        ddlColor.SelectedValue = "0";
        txtRemarks.Text = string.Empty;
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
        col = new DataColumn("Remarks");
        IndentProducts.Columns.Add(col);

        col = new DataColumn("So_Det_Id");
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
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    dr["Quantity"] = qty.Text;
                    // dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Priority"] = gvrow.Cells[7].Text;
                    dr["Brand"] = gvrow.Cells[8].Text;
                    dr["SuggestedParty"] = gvrow.Cells[9].Text;
                    dr["ReqFor"] = gvrow.Cells[10].Text;
                    dr["ReqDate"] = gvrow.Cells[11].Text;
                    dr["Specification"] = gvrow.Cells[12].Text;
                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["Remarks"] = gvrow.Cells[16].Text;
                    dr["So_Det_Id"] = gvrow.Cells[17].Text;
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
        col = new DataColumn("So_Det_Id");
        IndentProducts.Columns.Add(col);
        if (gvItemDetails.Rows.Count > 0)
        {
            gvItemDetails.SelectedIndex = -1;
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
                dr["So_Det_Id"] = gvrow.Cells[17].Text;

                IndentProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    txtModelName.Text = gvrow.Cells[4].Text;
                    txtItemUOM.Text = gvrow.Cells[5].Text.Trim();
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    if (gvrow.Cells[8].Text != "--")
                        txtBrand.Text = gvrow.Cells[8].Text;
                    txtReqByDate.Text = gvrow.Cells[11].Text;
                    txtSpecification.Text = gvrow.Cells[12].Text;
                    ddlItemPriority.SelectedValue = gvrow.Cells[7].Text;
                    if (ddlColor.Items.FindByText(gvrow.Cells[14].Text) != null)
                        ddlColor.Items.FindByText(gvrow.Cells[14].Text).Selected = true;
                    gvItemDetails.SelectedIndex = gvrow.RowIndex;

                }
            }
        }
        gvItemDetails.DataSource = IndentProducts;
        gvItemDetails.EditIndex = -1;
        gvItemDetails.DataBind();
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
            SM.DDLBindWithSelect(ddlItemType, "select ITEM_CODE,ITEM_MODEL_NO from YANTRA_ITEM_MAST where   YANTRA_ITEM_MAST.Item_code = '" + itemcode + "'   and  ITEM_MODEL_NO is not null");
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
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            IndentSave();
            Session.Remove("GridData");
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

                objSCM.INDId = Request.QueryString["IndentId"].ToString();
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
                    if (Request.QueryString["Cust"].ToString() == "From DC")
                    {
                        objSCM.INDENTFOR = "From DC";
                        objSCM.Custid = "-1";

                    }
                    else
                    {
                        objSCM.INDENTFOR = "Self";
                        objSCM.Custid = "0";

                    }
                }
                objSCM.FollowUp = ddlFollowUp.SelectedItem.Value;
                objSCM.INDPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                //objSCM.INDApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.CP_ID = cp.getPresentCompanySessionValue();
                objSCM.INDApprovedBy = ddlApprovedBy.SelectedItem.Value;

                if (objSCM.Indent_Update1() == "Data Updated Successfully")
                {
                    objSCM.IndentDetails_Delete1(objSCM.INDId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.INDItemCode = gvrow.Cells[2].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        objSCM.INDDetQty = qty.Text;
                        //objSCM.INDDetQty = gvrow.Cells[6].Text;
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


                        objSCM.INDDetRemQty = "0";
                        objSCM.INDDETORDQTY = "0";
                        objSCM.Remarks = gvrow.Cells[16].Text;
                        if (gvrow.Cells[17].Text == "&nbsp;" || gvrow.Cells[17].Text == string.Empty) { objSCM.IndSoDetId = "0"; }
                        else { objSCM.IndSoDetId = gvrow.Cells[17].Text; }

                        objSCM.IndentDetails_Save1();
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
                //btnDelete.Attributes.Clear();
                //gvIndentDetails.DataBind();

                gvItemDetails.DataBind();
                tblIndentDetails.Visible = false;
                //SCM.ClearControls(this);
                //  SCM.Dispose();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
"alert(' Indent Updated sucessfully');window.location ='Dispatchform.aspx';", true);
                // Response.Redirect("ChangedIndent.aspx");
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
                    if (gvReleasedItems.Rows.Count > 0)
                    {
                        objSCM.INDENTFOR = "From DC";
                        objSCM.Custid = "-1";
                    }
                    else
                    {
                        objSCM.INDENTFOR = "Self";
                        objSCM.Custid = "0";
                    }
                    objSCM.INDSoId = "0";


                }
                objSCM.Status = "New";


                objSCM.CP_ID = cp.getPresentCompanySessionValue();
                objSCM.INDPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSCM.INDApprovedBy = "0";// Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

                if (objSCM.Indent_Save1() == "Data Saved Successfully")
                {
                    objSCM.IndentDetails_Delete1(objSCM.INDId);
                    foreach (GridViewRow gvrow in gvItemDetails.Rows)
                    {
                        objSCM.INDItemCode = gvrow.Cells[2].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        objSCM.INDDetQty = qty.Text;

                        //objSCM.INDDetQty = gvrow.Cells[6].Text;
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
                        objSCM.INDDetRemQty = "0";
                        objSCM.INDDETORDQTY = "0";
                        objSCM.Remarks = gvrow.Cells[16].Text;
                        if (gvrow.Cells[17].Text == "&nbsp;" || gvrow.Cells[17].Text == string.Empty) { objSCM.IndSoDetId = "0"; }
                        else { objSCM.IndSoDetId = gvrow.Cells[17].Text; }
                        //objSCM.IndSoDetId = gvrow.Cells[17].Text;
                        objSCM.IndentDetails_Save1();
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
                //btnDelete.Attributes.Clear();
                //gvIndentDetails.DataBind();

                gvItemDetails.DataBind();
                gvSalesOrderItems.DataBind();
                tblIndentDetails.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
  "alert(' Indent Saved sucessfully');window.location ='Dispatchform.aspx';", true);

                // Response.Redirect("ChangedIndent.aspx");
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
        Response.Redirect("Dispatchform.aspx");

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IndentId"] != null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=IndentReq&indno=" + Request.QueryString["IndentId"].ToString() + "";
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
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            ddlItemPriority.SelectedIndex = 3;
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtSpecification.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
                if (rdblIndentfor.SelectedItem.Text == "Display")
                {
                    txtRemarks.Text = "Display";
                }
                else
                {
                    txtRemarks.Text = ddlSupplierName.SelectedItem.Text;
                }
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedValue);
            ddlItemPriority.SelectedIndex = 3;

            // Item Quantity In Hand By Model No
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

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

        }
    }
    protected void rdblIndentfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblIndentfor.SelectedItem.Text == "Customer")
        {
            tblPoDetails.Visible = true;
            gvSalesOrderItems.DataBind();

        }
        else
        {
            tblPoDetails.Visible = false;
            txtRemarks.Text = "Display";
        }
    }
    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster objMaster = new Masters.ItemMaster();

        if (objMaster.QTYInHandCOlor_select(ddlItemType.SelectedItem.Value, ddlColor.SelectedItem.Value) > 0)
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
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) * (Convert.ToDouble(e.Row.Cells[4].Text))).ToString();
            //   TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[9].Text);
            //lblTotalamount.Text = TotalAmount.ToString();
            if (Convert.ToInt32(e.Row.Cells[16].Text) >= Convert.ToInt32(e.Row.Cells[4].Text))
            {
                e.Row.BackColor = System.Drawing.Color.YellowGreen;
            }
            else if ((Convert.ToInt32(e.Row.Cells[16].Text) < Convert.ToInt32(e.Row.Cells[4].Text)) && (e.Row.Cells[16].Text != "0"))
            {
                e.Row.BackColor = System.Drawing.Color.Wheat;
            }
        }
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
            objSCM.INDId = Request.QueryString["IndentId"].ToString();
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
            //gvIndentDetails.DataBind();
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
        //gvIndentDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvIndentDetails.DataBind();
    }
}