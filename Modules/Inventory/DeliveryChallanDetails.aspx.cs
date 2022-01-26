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
using System.Data.SqlClient;
public partial class Modules_Inventory_DeliveryChallanDetails : basePage
{
    decimal TotalAmount = 0;
    decimal Total = 0;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());


    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //btnEdit.Enabled = false;
        if (!IsPostBack)
        {
            setControlsVisibility();

            // txtInhand.Style.Add("display", "none");
            txtBalanceQtyHidden.Style.Add("display", "none");
            // txtItemQuantity.Attributes.Add("onkeyup", "javascript:qtyinhandItemsCheck();");
            txtItemQuantity.Attributes.Add("onkeyup", "javascript:DeliveryItemsCheck();");
            txtSerialNo.Attributes.Add("onkeyup", "javascript:Serialno();");
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblCPID.Text = cp.getPresentCompanySessionValue();
            //  gvDeliveryChallanDetails.DataBind();
            DespatchMode_Fill();
            Trans_Fill();
            EmployeeMaster_Fill();
            tblDCDetails.Visible = false;
            SalesOrder_Fill();
            Masters.CompanyProfile.Company_Select(ddlCompany);
            btnForApproveHidden.Style.Add("display", "none");
            Masters.CompanyProfile.Company_Select(ddlCompany1);
            SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY order by PRODUCT_COMPANY_NAME asc ");
            //lblCompany.Text = cp.getPresentCompanySessionValue();
            ddlCompany1.SelectedValue = cp.getPresentCompanySessionValue();

            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if(Request.QueryString["DcId"] != null)
            {
                try
                {
                    txtSearchModel.Enabled = btnSearchModelNo.Enabled = ddlSalesOrderNo.Enabled = false;
                    Inventory.Delivery objDelivery = new Inventory.Delivery();
                    if (objDelivery.Delivery_Select(Request.QueryString["DcId"].ToString()) > 0)
                    {
                        btnSave.Enabled = false;
                        btnSave.Text = "Update";
                        rbSales.Enabled = rbSpares.Enabled = false;
                        if (objDelivery.DCFor == "Sales")
                        {
                            rbSales.Checked = true;
                            rbSales_CheckedChanged(sender, e);
                            rbSpares.Checked = false;
                            SalesOrder_Fill();
                            ddlSalesOrderNo.SelectedValue = objDelivery.SOId;
                        }
                        else if (objDelivery.DCFor == "Spares")
                        {
                            rbSpares.Checked = true;
                            rbSpares_CheckedChanged(sender, e);
                            rbSales.Checked = false;
                            SparesOrder_Fill();
                            ddlSalesOrderNo.SelectedValue = objDelivery.SPOId;
                        }
                        ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                        tblDCDetails.Visible = true;
                        txtDeliveryChallanNo.Text = objDelivery.DCNo;
                        txtDeliveryChallanDate.Text = objDelivery.DCDate;
                        ddlTransPorterName.SelectedValue = objDelivery.TransId;
                        txtLRNo.Text = objDelivery.DCLrNo;
                        txtLRDate.Text = objDelivery.DCLrDate;
                        ddlDCType.SelectedValue = objDelivery.DCType;
                        ddlDCType_SelectedIndexChanged(sender, e);
                        // txtCSTNo.Text = objDelivery.DCCSTNo;
                        // txtTINNo.Text = objDelivery.DCTINNo;
                        ddlPreparedBy.SelectedValue = objDelivery.DCPreparedBy;
                        ddlApprovedBy.SelectedValue = objDelivery.DCApprovedBy;
                        txtInwardDate.Text = objDelivery.DCInwardDate;
                        ddlDespatchMode.SelectedValue = objDelivery.DespmId;
                        ddlCompany1.SelectedValue = objDelivery.DetCompany;
                       // objDelivery.DeliveryDetails_Select(Request.QueryString["DcId"].ToString(), gvDeliveryChallanItems);
                        txtRevisedFrom.Text = objDelivery.RevisedKey;
                        if (txtRevisedFrom.Text != "")
                        {
                            lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
                        }
                        else
                        {
                            lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                        }
                        if (objDelivery.UnitId != "0")
                        {
                            ddlUnitName.SelectedValue = objDelivery.UnitId;
                            ddlUnitName_SelectedIndexChanged(sender, e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Inventory.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    Inventory.Dispose();
                }
                try
                {
                    Inventory.Delivery objDelivery = new Inventory.Delivery();

                    if (objDelivery.Delivery_Select(Request.QueryString["DcId"].ToString()) > 0)
                    {

                        btnSave.Enabled = true;
                        btnSave.Text = "Update";
                        //  rbSales.Enabled = rbSpares.Enabled = false;
                        if (objDelivery.DCFor == "Sales")
                        {
                            rbSales.Checked = true;
                            rbSales_CheckedChanged(sender, e);
                            rbSpares.Checked = false;
                            SalesOrder_Fill();
                            ddlSalesOrderNo.SelectedValue = objDelivery.SOId;

                            ddlUnitName.SelectedValue = objDelivery.UnitId;
                            ddlUnitName_SelectedIndexChanged(sender, e);
                        }
                        else if (objDelivery.DCFor == "Spares")
                        {
                            rbSpares.Checked = true;
                            rbSpares_CheckedChanged(sender, e);
                            rbSales.Checked = false;
                            SparesOrder_Fill();
                            ddlSalesOrderNo.SelectedValue = objDelivery.SPOId;

                            ddlUnitName.SelectedValue = objDelivery.UnitId;
                            ddlUnitName_SelectedIndexChanged(sender, e);
                        }
                        tblDCDetails.Visible = true;
                        txtDeliveryChallanNo.Text = objDelivery.DCNo;
                        txtDeliveryChallanDate.Text = objDelivery.DCDate;
                        ddlTransPorterName.SelectedValue = objDelivery.TransId;
                        txtLRNo.Text = objDelivery.DCLrNo;
                        txtLRDate.Text = objDelivery.DCLrDate;
                        ddlPreparedBy.SelectedValue = objDelivery.DCPreparedBy;
                        ddlApprovedBy.SelectedValue = objDelivery.DCApprovedBy;

                        ddlDCType.SelectedValue = objDelivery.DCType;
                        ddlDCType_SelectedIndexChanged(sender, e);
                        txtInwardDate.Text = objDelivery.DCInwardDate;
                        //txtCSTNo.Text = objDelivery.DCCSTNo;
                        //txtTINNo.Text = objDelivery.DCTINNo;
                        ddlDespatchMode.SelectedValue = objDelivery.DespmId;
                        ddlCompany1.SelectedValue = objDelivery.Company;

                        //  objDelivery.DeliveryDetails_Select(Request.QueryString["DcId"].ToString(), gvItmDetails);
                        txtRevisedFrom.Text = objDelivery.RevisedKey;
                        if (txtRevisedFrom.Text != "")
                        {
                            lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
                        }
                        else
                        {
                            lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    Inventory.Dispose();
                    ddlSalesOrderNo_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                btnRevise.Attributes.Clear();
                //Inventory.ClearControls(this);
                txtDeliveryChallanNo.Text = Inventory.Delivery.Delivery_AutoGenCode();
                txtDeliveryChallanDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                txtLRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ddlDespatchMode.SelectedValue = "1";
                ddlDCType.SelectedIndex = 2;
                ddlTransPorterName.SelectedValue = "1";
                btnRevise.Text = "Modify";
                btnSave.Text = "Save";
                //SalesOrder_Fill();
                rbSales.Enabled = rbSpares.Enabled = true;
                rbSales.Checked = true;
                btnSave.Enabled = true;
                tblDCDetails.Visible = true;
               // gvDeliveryChallanDetails.SelectedIndex = -1;
                gvItemDetails.DataBind();
                gvItmDetails.DataBind();
                gvDeliveryChallanItems.DataBind();
            }
        }
    }


    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "68");
        btnSave.Enabled = up.add;
        btnPrint.Enabled = up.Print;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Update")
        {
            btnRefresh.Visible = false;
            btnDelete.Visible = true;
        }
        else if (btnSave.Text == "Save")
        {
            btnRefresh.Visible = true;
        }

        if (Request.QueryString["DcId"] != null)
        {
            if (Request.QueryString["DcType"].ToString() == "Returnable")
            {
                btnRevise.Visible = true;
            }
            else if (Request.QueryString["DcType"].ToString() == "Non Returnable")
            {
                btnRevise.Visible = false;
            }
        }
        //#region Approve

        //if (Request.QueryString["DcId"] != null)
        //{
        //    if (!string.IsNullOrEmpty(gvDeliveryChallanDetails.SelectedRow.Cells[12].Text) && gvDeliveryChallanDetails.SelectedRow.Cells[12].Text != "&nbsp;")
        //    {
        //        btnApprove.Visible = false;
        //        btnSave.Visible = false;
        //        btnRefresh.Visible = false;
        //        btnDelete.Visible = false;
        //        btnEdit.Visible = false;
        //        btnPrint.Visible = true;
        //    }
        //    else
        //    {
        //        btnApprove.Visible = true;
        //        btnSave.Visible = true;
        //        btnRefresh.Visible = false;
        //        btnDelete.Visible = true;
        //        btnEdit.Visible = true;
        //        btnPrint.Visible = false;
        //    }
        //}
        //else
        //{
        //    btnSave.Visible = true;
        //    btnRefresh.Visible = true;
        //    btnApprove.Visible = false;
        //    btnDelete.Visible = true;
        //    btnEdit.Visible = true;
        //    btnPrint.Visible = false;
        //}
        //#endregion


        string DeliveryChallanId, DeliveredQtyCount = "0";
        if (Request.QueryString["DcId"] != null)
        {
            DeliveryChallanId = Request.QueryString["DcId"].ToString();
        }
        else
        {
            DeliveryChallanId = "0";
        }

        foreach (GridViewRow DCItemRow in gvItmDetails.Rows)
        {
            DeliveredQtyCount = "0";
            foreach (GridViewRow DCDeliveredRow in gvDeliveryChallanItems.Rows)
            {
                if (DCDeliveredRow.Cells[7].Text != DeliveryChallanId)
                {
                    if (DCItemRow.Cells[2].Text == DCDeliveredRow.Cells[2].Text)
                    {
                        DeliveredQtyCount = Convert.ToString(int.Parse(DeliveredQtyCount) + int.Parse(DCDeliveredRow.Cells[6].Text));
                    }
                }
                if (DCDeliveredRow.Cells[7].Text == DeliveryChallanId)
                {
                    if (DCItemRow.Cells[2].Text == DCDeliveredRow.Cells[2].Text)
                    {
                        foreach (GridViewRow SOItemsRow in gvItemDetails.Rows)
                        {
                            if (SOItemsRow.Cells[0].Text == DCItemRow.Cells[2].Text)
                            {
                                if (int.Parse(SOItemsRow.Cells[4].Text) - int.Parse(DCItemRow.Cells[6].Text) > 0)
                                {
                                    DCItemRow.Cells[8].Text = "pd";
                                }
                                else if (int.Parse(SOItemsRow.Cells[4].Text) - int.Parse(DCItemRow.Cells[6].Text) == 0)
                                {
                                    DCItemRow.Cells[8].Text = "d";
                                }
                            }
                        }
                    }
                }
            }
            if (DeliveredQtyCount != "0")
            {
                foreach (GridViewRow SOItemsRow in gvItemDetails.Rows)
                {
                    if (SOItemsRow.Cells[0].Text == DCItemRow.Cells[2].Text)
                    {
                        if (int.Parse(SOItemsRow.Cells[4].Text) - int.Parse(DeliveredQtyCount) - int.Parse(DCItemRow.Cells[6].Text) > 0)
                        {
                            DCItemRow.Cells[8].Text = "pd";
                        }
                        else if (int.Parse(SOItemsRow.Cells[4].Text) - int.Parse(DeliveredQtyCount) - int.Parse(DCItemRow.Cells[6].Text) == 0)
                        {
                            DCItemRow.Cells[8].Text = "d";
                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow SOItemsRow in gvItemDetails.Rows)
                {
                    if (SOItemsRow.Cells[0].Text == DCItemRow.Cells[2].Text)
                    {
                        if (int.Parse(SOItemsRow.Cells[4].Text) - int.Parse(DCItemRow.Cells[6].Text) > 0)
                        {
                            DCItemRow.Cells[8].Text = "pd";
                        }
                        else if (int.Parse(SOItemsRow.Cells[4].Text) - int.Parse(DCItemRow.Cells[6].Text) == 0)
                        {
                            DCItemRow.Cells[8].Text = "d";
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                SM.SalesOrder.SalesOrderForDelivery_Select(ddlSalesOrderNo);
                ddlSalesOrderNo.Enabled = true;
            }
            else if (btnSave.Text == "Update")
            {
                SM.SalesOrder.SalesOrderForDelivery_Select(ddlSalesOrderNo);
                ddlSalesOrderNo.Enabled = true;
            }
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

    #region SparesOrder Fill
    private void SparesOrder_Fill()
    {
        try
        {
            if (btnSave.Text == "Save")
            {
                Services.SparesOrder.SparesOrderForDelivery_Select(ddlSalesOrderNo);
                ddlSalesOrderNo.Enabled = true;
            }
            else if (btnSave.Text == "Update")
            {
                Services.SparesOrder.SparesOrder_Select(ddlSalesOrderNo);
                ddlSalesOrderNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Services.Dispose();
        }
    }
    #endregion

    #region Despatch Mode Fill
    private void DespatchMode_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDespatchMode);
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

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {

            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpNameForAssign);
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

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        if (rbSales.Checked)
        {
            try
            {
                //Masters.ItemType.ItemType_Select(ddlItemType);
                SM.SalesOrder.SalesOrderItemTypes2_Select(ddlSalesOrderNo.SelectedItem.Value, ddlModelNo);
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
        else if (rbSpares.Checked)
        {
            try
            {
                //Masters.ItemType.ItemType_Select(ddlItemType);
                Services.SparesOrder.SparesOrderItemTypes_Select(ddlSalesOrderNo.SelectedItem.Value, ddlModelNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                Services.Dispose();
            }
        }
    }
    #endregion

    #region Godown Name Fill
    //private void Godown_Fill()
    //{
    //    try
    //    {
    //        Masters.ItemMaster.ModelLocation(ddllocation, ddlModelNo.SelectedItem.Value);
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
    #endregion

    #region Trans Fill
    private void Trans_Fill()
    {
        try
        {
            Masters.TrasnporterMaster.TransporterMaster_Select(ddlTransPorterName);
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

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
        btnRevise.Attributes.Clear();
        Inventory.ClearControls(this);
        txtDeliveryChallanNo.Text = Inventory.Delivery.Delivery_AutoGenCode();
        txtDeliveryChallanDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        txtLRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


        btnRevise.Text = "Modify";
        btnSave.Text = "Save";
        //SalesOrder_Fill();
        rbSales.Enabled = rbSpares.Enabled = true;
        rbSales.Checked = true;
        btnSave.Enabled = true;
        tblDCDetails.Visible = true;
        //gvDeliveryChallanDetails.SelectedIndex = -1;
        gvItemDetails.DataBind();
        gvItmDetails.DataBind();
        gvDeliveryChallanItems.DataBind();


    }

    #endregion

    #region Button  SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (btnSave.Text == "Save")
        {
            
            DeliverySave();
            //UpdateSoItemsQty();
            
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (gvReleasedItems.Rows.Count > 0)
                {
                    RaiseIndent();
                }
                
            }
            else
            {
                Response.Redirect("DeliveryChallan.aspx");
            }
            Response.Redirect("DeliveryChallan.aspx");


        }
        else if (btnSave.Text == "Update")
        {
            DeliveryUpdate();
           // UpdateSoItemsQty();
            
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (gvReleasedItems.Rows.Count > 0)
                    {
                        RaiseIndent();
                    }
                    else
                    {
                        MessageBox.Show(this, "No Items Released to Raise Indent");
                    }

                }
                else
                {
                    Response.Redirect("DeliveryChallan.aspx");
                }
            Response.Redirect("DeliveryChallan.aspx");
        }
    }

    private void UpdateSoItemsQty()
    {
        foreach (GridViewRow gvrow in gvItmDetails.Rows)
        {
            Inventory.Delivery objInventory = new Inventory.Delivery();
            objInventory.iqitemcode = gvrow.Cells[2].Text;
            objInventory.iqcolorid = gvrow.Cells[11].Text;
            objInventory.DCDetQty = gvrow.Cells[6].Text;
            objInventory.SOId = ddlSalesOrderNo.SelectedValue;
           // objInventory.SoDetId = gvrow.Cells[16].Text;
            objInventory.BalanceQty_Update();

        }
    }

    private void RaiseIndent()
    {
        
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new System.Data.DataColumn("Item Code", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Item Model No", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Colour", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Quantity", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Customer Name", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Delivery Date", typeof(String)));

            foreach (GridViewRow row in gvReleasedItems.Rows)
            {
                dr = dt.NewRow();
                dr[0] = row.Cells[0].Text;
                dr[1] = row.Cells[1].Text;
                dr[2] = row.Cells[2].Text;
                dr[3] = row.Cells[3].Text;
                dr[4] = row.Cells[4].Text;
                dr[5] = row.Cells[5].Text;

                dt.Rows.Add(dr);
            }
            Session["GridData"] = dt;
            Response.Redirect("~/Modules/SCM/ChangedIndentDetails.aspx");
                
    }

    #endregion

    #region DeliverySave
    private void DeliverySave()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                Inventory.Delivery objInventory = new Inventory.Delivery();
               // Inventory.BeginTransaction();
                objInventory.DCNo = txtDeliveryChallanNo.Text;
                objInventory.DCDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryChallanDate.Text);
                if (rbSales.Checked)
                {
                    objInventory.DCFor = rbSales.Text;
                    objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
                    objInventory.SPOId = "0";
                }

                objInventory.DCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.DCApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                objInventory.DCLrNo = txtLRNo.Text;
                objInventory.DCLrDate = Yantra.Classes.General.toMMDDYYYY(txtLRDate.Text);
                objInventory.DCType = ddlDCType.SelectedItem.Value;
                objInventory.DCCSTNo = "0";
                objInventory.DCTINNo = "0";
                objInventory.DCInwardDate = Yantra.Classes.General.toMMDDYYYY(txtInwardDate.Text);
                objInventory.DespmId = ddlDespatchMode.SelectedItem.Value;
                objInventory.Cp_Id = lblCPID.Text;
                objInventory.STATUS = "OPEN";
                objInventory.Company = ddlCompany1.SelectedItem.Value;
                objInventory.DcCustomerid = lblCustId.Text;
                objInventory.UnitId = ddlUnitName.SelectedItem.Value;

                if (objInventory.Delivery_Save() == "Data Saved Successfully")
                {

                    string id = objInventory.DCId;
                    objInventory.DeliveryDetails_Delete(objInventory.DCId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.DCDetQty = gvrow.Cells[6].Text;
                        objInventory.DCDetSerialNo = gvrow.Cells[8].Text;
                        objInventory.COLORID = gvrow.Cells[11].Text;
                        objInventory.GODOWNID = gvrow.Cells[12].Text;
                        objInventory.DETSTATUS = gvrow.Cells[15].Text;
                        objInventory.DetCompany = gvrow.Cells[17].Text;
                        objInventory.ITemremarks = gvrow.Cells[18].Text;
                        objInventory.DCfor = gvrow.Cells[19].Text;
                        objInventory.Remarks2 = gvrow.Cells[20].Text;
                        objInventory.invoiceNo = "0";

                        objInventory.DeliveryDetails_Save();

                        int qty = int.Parse(gvrow.Cells[6].Text);

                        //**Block Release **//
                        SqlCommand cmd2 = new SqlCommand("Usp_Release_Block_DC", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@Qty", qty);
                        cmd2.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
                        cmd2.Parameters.AddWithValue("@ColourId", gvrow.Cells[11].Text);
                        cmd2.Parameters.AddWithValue("@Customer_Id", Convert.ToInt32(lblCustId.Text));
                        con.Open();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        //** End Block Release **//

                        //** Save To Outward ***//

                        #region Outward
                        string Itemcode = gvrow.Cells[2].Text;
                        SqlCommand cmd = new SqlCommand("Usp_Get_Top_Selected_Items", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
                        cmd.Parameters.AddWithValue("@ColourId", gvrow.Cells[11].Text);
                        cmd.Parameters.AddWithValue("@LocationId", gvrow.Cells[12].Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Masters.ItemPurchase objout = new Masters.ItemPurchase();

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                objout.itemcode = gvrow.Cells[2].Text;
                                objout.ItemID = dt.Rows[i][0].ToString();
                                objout.locationid = dt.Rows[i][1].ToString();
                                objout.Barcode = dt.Rows[i][0].ToString();
                                //objout.companyid = lblCPID.Text;
                                objout.companyid = dt.Rows[i][3].ToString();
                                objout.DCID = ddlSalesOrderNo.SelectedItem.Value;
                                //objout.COLORID = gvrow.Cells[11].Text;
                                objout.COLORID = dt.Rows[i][2].ToString();
                                objout.CustId = lblCustId.Text;
                                objout.Outward_Save();
                            }
                        }
                        #endregion

                        //** End Save To Outward ***//


                        //Masters.ItemPurchase obj = new Masters.ItemPurchase();
                        //int hai = int.Parse(gvrow.Cells[6].Text);
                        //for (int i = 0; i < hai; i++)
                        //{
                        //    obj.itemcode = gvrow.Cells[2].Text;
                        //    obj.ItemID = "I" + i + gvrow.Cells[2].Text;
                        //    obj.companyid = lblCPID.Text;
                        //    obj.Barcode = "I" + i + gvrow.Cells[2].Text;
                        //    obj.locationid = gvrow.Cells[12].Text;
                        //    //obj.MRNID = objchkf.CHKID;
                        //    obj.DCID = id;
                        //    obj.COLORID = gvrow.Cells[11].Text;
                        //    obj.Outward_Save();
                        //}

                        //if (gvrow.Cells[7].Text == "pd" || gvrow.Cells[7].Text == "d")
                        //{
                        //    foreach (GridViewRow gvSORow in gvItemDetails.Rows)
                        //    {
                        //        if (gvrow.Cells[2].Text == gvSORow.Cells[2].Text)
                        //        {
                        //            if (gvrow.Cells[7].Text == "pd")
                        //            {
                        //                if (rbSales.Checked) { SM.SalesOrder.SalesOrderDetailsItemStatus_Update(SM.SalesOrder.SOItemStatus.PartiallyDelivered, gvSORow.Cells[9].Text); }
                        //                else if (rbSpares.Checked) { Services.SparesOrder.SparesOrderDetailsItemStatus_Update(Services.SparesOrder.SOItemStatus.PartiallyDelivered, gvSORow.Cells[8].Text); }
                        //            }
                        //            else if (gvrow.Cells[7].Text == "d")
                        //            {
                        //                if (rbSales.Checked) { SM.SalesOrder.SalesOrderDetailsItemStatus_Update(SM.SalesOrder.SOItemStatus.Delivered, gvSORow.Cells[9].Text); }
                        //                else if (rbSpares.Checked) { Services.SparesOrder.SparesOrderDetailsItemStatus_Update(Services.SparesOrder.SOItemStatus.Delivered, gvSORow.Cells[8].Text); }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    UpdateSoItemsQty();
                    //foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    //{
                    //    objInventory.iqitemcode = gvrow.Cells[2].Text;
                    //    objInventory.iqcpid = gvrow.Cells[17].Text;
                    //    objInventory.iqgodownid = gvrow.Cells[12].Text;
                    //    objInventory.iqcolorid = gvrow.Cells[11].Text;
                    //    objInventory.iqitemqtyinhand = gvrow.Cells[6].Text;
                    //    objInventory.iqresqty = gvrow.Cells[14].Text;
                    //    objInventory.SoDetId = ddlSalesOrderNo.SelectedValue;
                    //    objInventory.Itemqty_Update();

                    //}
                    //foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    //{
                    //    objInventory.iqitemcode = gvrow.Cells[2].Text;
                    //    objInventory.iqcolorid = gvrow.Cells[11].Text;
                    //    objInventory.DCDetQty = gvrow.Cells[6].Text;
                    //    objInventory.SOId = ddlSalesOrderNo.SelectedValue;
                    //    objInventory.BalanceQty_Update();

                    //}
                   // Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");


                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }

            catch (Exception ex)
            {
               // Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

                //gvDeliveryChallanDetails.DataBind();
                gvItmDetails.DataBind();
                //gvDeliveryChallanDetails.SelectedIndex = -1;
                tblDCDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }

    }
    #endregion

    #region DeliveryRevise
    protected void btnRevise_Click(object sender, EventArgs e)
    {
        if (btnRevise.Text == "Modify")
        {
            lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
            txtRevisedFrom.Text = txtDeliveryChallanNo.Text;
            txtDeliveryChallanNo.Text = Inventory.Delivery.Delivery_AutoGenCode();
            txtDeliveryChallanDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnRevise.Attributes.Add("onclick", "return confirm('Are you sure you want to Revise this Record !');");
            btnRevise.Text = "Revise";
        }
        else if (btnRevise.Text == "Revise")
        {
            if (gvItmDetails.Rows.Count > 0)
            {
                try
                {
                    btnRevise.Attributes.Clear();
                    Inventory.Delivery objInventory = new Inventory.Delivery();
                    Inventory.BeginTransaction();
                    objInventory.RevisedKey = txtDeliveryChallanNo.Text;
                    objInventory.DCNo = txtDeliveryChallanNo.Text;
                    objInventory.DCDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryChallanDate.Text);
                    if (rbSales.Checked)
                    {
                        objInventory.DCFor = rbSales.Text;
                        objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
                        objInventory.SPOId = "0";
                    }
                    else if (rbSpares.Checked)
                    {
                        objInventory.DCFor = rbSpares.Text;
                        objInventory.SPOId = ddlSalesOrderNo.SelectedItem.Value;
                        objInventory.SOId = "0";
                    }
                    objInventory.DCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    objInventory.DCApprovedBy = "0";
                    objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                    objInventory.DCLrNo = txtLRNo.Text;
                    objInventory.DCLrDate = Yantra.Classes.General.toMMDDYYYY(txtLRDate.Text);
                    objInventory.DCType = ddlDCType.SelectedItem.Value;
                    objInventory.DCCSTNo = "0";
                    objInventory.DCTINNo = "0";
                    objInventory.DCInwardDate = Yantra.Classes.General.toMMDDYYYY(txtInwardDate.Text);
                    objInventory.DespmId = ddlDespatchMode.SelectedItem.Value;
                    objInventory.Cp_Id = lblCPID.Text;
                    objInventory.STATUS = "OPEN";
                    objInventory.Company = ddlCompany1.SelectedItem.Value;
                    objInventory.DcCustomerid = lblCustId.Text;
                    objInventory.UnitId = ddlUnitName.SelectedItem.Value;

                    if (objInventory.DeliveryRevise_Save() == "Data Saved Successfully")
                    {
                        objInventory.DeliveryDetails_Delete(objInventory.DCId);
                        foreach (GridViewRow gvrow in gvItmDetails.Rows)
                        {
                            objInventory.ItemCode = gvrow.Cells[2].Text;
                            objInventory.DCDetQty = gvrow.Cells[6].Text;
                            objInventory.DCDetSerialNo = gvrow.Cells[9].Text;
                            objInventory.COLORID = gvrow.Cells[11].Text;
                            objInventory.GODOWNID = gvrow.Cells[12].Text;
                            objInventory.DETSTATUS = gvrow.Cells[15].Text;
                            objInventory.DeliveryDetails_Save();
                            if (gvrow.Cells[7].Text == "pd" || gvrow.Cells[7].Text == "d")
                            {
                                foreach (GridViewRow gvSORow in gvItemDetails.Rows)
                                {
                                    if (gvrow.Cells[2].Text == gvSORow.Cells[2].Text)
                                    {
                                        if (gvrow.Cells[7].Text == "pd")
                                        {
                                            if (rbSales.Checked) { SM.SalesOrder.SalesOrderDetailsItemStatus_Update(SM.SalesOrder.SOItemStatus.PartiallyDelivered, gvSORow.Cells[7].Text); }
                                            else if (rbSpares.Checked) { Services.SparesOrder.SparesOrderDetailsItemStatus_Update(Services.SparesOrder.SOItemStatus.PartiallyDelivered, gvSORow.Cells[8].Text); }
                                        }
                                        else if (gvrow.Cells[7].Text == "d")
                                        {
                                            if (rbSales.Checked) { SM.SalesOrder.SalesOrderDetailsItemStatus_Update(SM.SalesOrder.SOItemStatus.Delivered, gvSORow.Cells[7].Text); }
                                            else if (rbSpares.Checked) { Services.SparesOrder.SparesOrderDetailsItemStatus_Update(Services.SparesOrder.SOItemStatus.Delivered, gvSORow.Cells[8].Text); }
                                        }
                                    }
                                }
                            }
                        }

                        Inventory.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
                    else
                    {
                        Inventory.RollBackTransaction();
                    }
                }
                catch (Exception ex)
                {
                    Inventory.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {

                    //gvDeliveryChallanDetails.DataBind();
                    gvItmDetails.DataBind();

                    tblDCDetails.Visible = false;
                    Inventory.ClearControls(this);
                    Inventory.Dispose();
                }
            }
            else
            {
                MessageBox.Show(this, "Please add atleast one Item for Delivery Challan");
            }
        }
    }
    #endregion

    #region DeliveryUpdate
    private void DeliveryUpdate()
    {

        Inventory.Delivery objInventory = new Inventory.Delivery();

        //*****

        //this.DCDate, this.DCLrNo, this.DCLrDate, this.TransId, this.DCType, 
        //this.DCCSTNo, this.DCTINNo, this.DCInwardDate, this.DespmId, this.Cp_Id, this.Company,this.UnitId, this.DCI

        //******
        // Inventory.BeginTransaction();
        objInventory.DCId = Request.QueryString["DcId"].ToString();
        objInventory.DCNo = txtDeliveryChallanNo.Text;
        objInventory.DCDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryChallanDate.Text);
        //if (rbSales.Checked)
        //{
        //    objInventory.DCFor = rbSales.Text;
        //    objInventory.SOId = ddlSalesOrderNo.SelectedItem.Value;
        //    objInventory.SPOId = "0";
        //}

        objInventory.DCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        //objInventory.DCApprovedBy = ddlApprovedBy.SelectedItem.Value;
        objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
        objInventory.DCLrNo = txtLRNo.Text;
        objInventory.DCLrDate = Yantra.Classes.General.toMMDDYYYY(txtLRDate.Text);
        objInventory.DCType = ddlDCType.SelectedItem.Value;
        objInventory.DCCSTNo = "0";
        objInventory.DCTINNo = "0";
        objInventory.DCInwardDate = Yantra.Classes.General.toMMDDYYYY(txtInwardDate.Text);
        objInventory.DespmId = ddlDespatchMode.SelectedItem.Value;
        objInventory.Cp_Id = lblCPID.Text;
        //objInventory.STATUS = "OPEN";
        objInventory.Company = ddlCompany1.SelectedItem.Value;
        objInventory.DcCustomerid = lblCustId.Text;
        objInventory.UnitId = ddlUnitName.SelectedItem.Value;

        if (objInventory.Delivery_Update() == "Data Updated Successfully")
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                //Inventory.Delivery objInventory = new Inventory.Delivery();

               // Inventory.BeginTransaction();
                
                objInventory.DCId = Request.QueryString["DcId"].ToString();

                foreach (GridViewRow gvrow in gvItmDetails.Rows)
                {
                    objInventory.ItemCode = gvrow.Cells[2].Text;
                    objInventory.DCDetQty = gvrow.Cells[6].Text;
                    objInventory.DCDetSerialNo = gvrow.Cells[8].Text;
                    objInventory.COLORID = gvrow.Cells[11].Text;
                    objInventory.GODOWNID = gvrow.Cells[12].Text;
                    objInventory.DETSTATUS = gvrow.Cells[15].Text;
                    objInventory.DetCompany = gvrow.Cells[17].Text;
                    objInventory.ITemremarks = gvrow.Cells[18].Text;
                    objInventory.DCfor = gvrow.Cells[19].Text;
                    objInventory.Remarks2 = gvrow.Cells[20].Text;
                    objInventory.invoiceNo = "0";
                    objInventory.DeliveryDetails_Save();
                    int qty = int.Parse(gvrow.Cells[6].Text);

                    //**Block Release **//
                    SqlCommand cmd2 = new SqlCommand("Usp_Release_Block_DC", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@Qty", qty);
                    cmd2.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
                    cmd2.Parameters.AddWithValue("@ColourId", gvrow.Cells[11].Text);
                    cmd2.Parameters.AddWithValue("@Customer_Id", Convert.ToInt32(lblCustId.Text));
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    //** End Block Release **//


                    //string Itemcode = gvrow.Cells[2].Text;
                    //SqlCommand cmd = new SqlCommand("select  Item_ID,whLocId,COLOUR_ID from dbo.INWARD where ITEM_CODE=" + Itemcode + " and Item_ID not in(select Item_ID from dbo.OUTWARD where ITEM_CODE=" + Itemcode + ")", con);
                    //cmd.CommandType = CommandType.Text;
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //Masters.ItemPurchase objout = new Masters.ItemPurchase();

                    string Itemcode = gvrow.Cells[2].Text;
                    SqlCommand cmd = new SqlCommand("Usp_Get_Top_Selected_Items", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
                    cmd.Parameters.AddWithValue("@ColourId", gvrow.Cells[11].Text);
                    cmd.Parameters.AddWithValue("@LocationId", gvrow.Cells[12].Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Masters.ItemPurchase objout = new Masters.ItemPurchase();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < qty; i++)
                        {
                            objout.itemcode = gvrow.Cells[2].Text;
                            objout.ItemID = dt.Rows[i][0].ToString();
                            objout.locationid = dt.Rows[i][1].ToString();
                            objout.Barcode = dt.Rows[i][0].ToString();
                           // objout.companyid = lblCPID.Text;
                            objout.companyid = dt.Rows[i][3].ToString();
                            objout.DCID = ddlSalesOrderNo.SelectedItem.Value;
                            //objout.COLORID = gvrow.Cells[11].Text;
                            objout.COLORID = dt.Rows[i][2].ToString();
                            objout.CustId = lblCustId.Text;

                            objout.Outward_Save();
                        }
                    }

                  

                    //Masters.ItemPurchase obj = new Masters.ItemPurchase();
                    //int hai = int.Parse(gvrow.Cells[6].Text);
                    //if(dt.Rows.Count>0)
                    //for (int i = 0; i < hai; i++)
                    //{
                    //    obj.itemcode = gvrow.Cells[2].Text;
                    //    obj.ItemID = "I" + i + gvrow.Cells[2].Text;
                    //    obj.companyid = lblCPID.Text;
                    //    obj.Barcode = "I" + i + gvrow.Cells[2].Text;
                    //    obj.locationid = gvrow.Cells[12].Text;
                    //    //obj.MRNID = objchkf.CHKID;
                    //    obj.DCID = objInventory.DCId;
                    //    obj.COLORID = gvrow.Cells[11].Text;
                    //    obj.Outward_Save();
                    //}

                }
                UpdateSoItemsQty();
                //foreach (GridViewRow gvrow in gvItmDetails.Rows)
                //{
                //    objInventory.iqitemcode = gvrow.Cells[2].Text;
                //    objInventory.iqcpid = gvrow.Cells[17].Text;
                //    objInventory.iqgodownid = gvrow.Cells[12].Text;
                //    objInventory.iqcolorid = gvrow.Cells[11].Text;
                //    objInventory.iqitemqtyinhand = gvrow.Cells[6].Text;
                //    objInventory.iqresqty = gvrow.Cells[14].Text;
                //    objInventory.SampleItemqty_Update();

                //}
               // Inventory.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
                //}
                //else
                //{
                //    Inventory.RollBackTransaction();
                //}
            }
            catch (Exception ex)
            {
               // Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                // btnSave.Text = "Save";

                //gvDeliveryChallanDetails.DataBind();
                gvItmDetails.DataBind();
                tblDCDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Sales Order");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DcId"] != null)
        {
            try
            {
                Inventory.Delivery objDelivery = new Inventory.Delivery();

                if (objDelivery.Delivery_Select(Request.QueryString["DcId"].ToString()) > 0)
                {

                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    //  rbSales.Enabled = rbSpares.Enabled = false;
                    if (objDelivery.DCFor == "Sales")
                    {
                        rbSales.Checked = true;
                        rbSales_CheckedChanged(sender, e);
                        rbSpares.Checked = false;
                        SalesOrder_Fill();
                        ddlSalesOrderNo.SelectedValue = objDelivery.SOId;

                        ddlUnitName.SelectedValue = objDelivery.UnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                    }
                    else if (objDelivery.DCFor == "Spares")
                    {
                        rbSpares.Checked = true;
                        rbSpares_CheckedChanged(sender, e);
                        rbSales.Checked = false;
                        SparesOrder_Fill();
                        ddlSalesOrderNo.SelectedValue = objDelivery.SPOId;

                        ddlUnitName.SelectedValue = objDelivery.UnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                    }
                    tblDCDetails.Visible = true;
                    txtDeliveryChallanNo.Text = objDelivery.DCNo;
                    txtDeliveryChallanDate.Text = objDelivery.DCDate;
                    ddlTransPorterName.SelectedValue = objDelivery.TransId;
                    txtLRNo.Text = objDelivery.DCLrNo;
                    txtLRDate.Text = objDelivery.DCLrDate;
                    ddlPreparedBy.SelectedValue = objDelivery.DCPreparedBy;
                    ddlApprovedBy.SelectedValue = objDelivery.DCApprovedBy;

                    ddlDCType.SelectedValue = objDelivery.DCType;
                    ddlDCType_SelectedIndexChanged(sender, e);
                    txtInwardDate.Text = objDelivery.DCInwardDate;
                    //txtCSTNo.Text = objDelivery.DCCSTNo;
                    //txtTINNo.Text = objDelivery.DCTINNo;
                    ddlDespatchMode.SelectedValue = objDelivery.DespmId;
                    ddlCompany1.SelectedValue = objDelivery.Company;

                    //  objDelivery.DeliveryDetails_Select(Request.QueryString["DcId"].ToString(), gvItmDetails);
                    txtRevisedFrom.Text = objDelivery.RevisedKey;
                    if (txtRevisedFrom.Text != "")
                    {
                        lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
                    }
                    else
                    {
                        lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Inventory.Dispose();
                ddlSalesOrderNo_SelectedIndexChanged(sender, e);
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
        if (Request.QueryString["DcId"] != null)
        {
            try
            {
                Inventory.Delivery objInventory = new Inventory.Delivery();
                MessageBox.Show(this, objInventory.Delivery_Delete(Request.QueryString["DcId"].ToString(), Request.QueryString["SoId"].ToString()));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                
            }
            finally
            {

                //gvDeliveryChallanDetails.DataBind();
                Inventory.ClearControls(this);
                Inventory.Dispose();
                tblDCDetails.Visible = false;
                //gvDeliveryChallanDetails.SelectedIndex = -1;
                
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region SalesOrder No Selected Index Changed
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.SalesOrder objSM = new SM.SalesOrder();
            if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
            {
                ItemTypes_Fill();
                txtSalesOrderDate.Text = objSM.SODate;
                txtAdvanceAmount.Text = objSM.SOAdvanceAmt;
                if (txtAdvanceAmount.Text == "0")
                {
                    txtAdvanceAmount.Text = "0";
                }

                objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);

                Inventory.Delivery objDC = new Inventory.Delivery();
                objDC.DeliveryDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvDeliveryChallanItems);

               // objDC.DeliveryDetailsBySalesOrderId_Select(objSM.SOId, gvDeliveryChallanItems);
                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, objSM.CustId);
                Inventory.Delivery objDelivery = new Inventory.Delivery();
                if (Request.QueryString["DcId"] != null)
                {

                    if (objDelivery.Delivery_Select(Request.QueryString["DcId"].ToString()) > 0)
                    {
                        ddlUnitName.SelectedValue = objDelivery.UnitId;
                    }
                }

                lblCustId.Text = objSM.CustId;
                if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                {
                    txtCustomerName.Text = objSMCustomer.CustName;
                    txtAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtPhone.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
                SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
                objPaymentsReceived.ExistingPaymentsReceived_Select(gvPreviousPayments, ddlSalesOrderNo.SelectedItem.Value);
                txtTotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtAdvanceAmount.Text) + TotalAmount);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            SM.Dispose();
        }

        //if (rdbHighsale.Checked)
        // {
        //     try
        //     {
        //         SM.SalesOrder objSM = new SM.SalesOrder();
        //         if (objSM.SalesOrder_Select(ddlSalesOrderNo.SelectedItem.Value) > 0)
        //         {
        //             ItemTypes_Fill();
        //             txtSalesOrderDate.Text = objSM.SODate;
        //             txtAdvanceAmount.Text = objSM.SOAdvanceAmt;
        //             if (txtAdvanceAmount.Text == "0")
        //             {
        //                 txtAdvanceAmount.Text = "0";
        //             }

        //             objSM.SalesOrderDetails_Select(ddlSalesOrderNo.SelectedItem.Value, gvItemDetails);
        //             Inventory.Delivery objDC = new Inventory.Delivery();
        //             objDC.DeliveryDetailsBySalesOrderId_Select(objSM.SOId, gvDeliveryChallanItems);
        //             SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        //             if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
        //             {
        //                 txtCustomerName.Text = objSMCustomer.CustName;
        //                 txtAddress.Text = objSMCustomer.Address;
        //                 txtEmail.Text = objSMCustomer.Email;
        //                 txtRegion.Text = objSMCustomer.RegName;
        //                 txtPhone.Text = objSMCustomer.Phone;
        //                 txtMobile.Text = objSMCustomer.Mobile;
        //             }
        //             SM.PaymentsReceived objPaymentsReceived = new SM.PaymentsReceived();
        //             objPaymentsReceived.ExistingPaymentsReceived_Select(gvPreviousPayments, ddlSalesOrderNo.SelectedItem.Value);
        //             txtTotalAmount.Text = Convert.ToString(Convert.ToDecimal(txtAdvanceAmount.Text) + TotalAmount);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show(this, ex.Message.ToString());
        //     }
        //     finally
        //     {
        //         SM.Dispose();
        //     }
        // }

    }
    #endregion

    #region GridView Item Details Row Databound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[14].Text)) / (Convert.ToDouble(e.Row.Cells[6].Text))).ToString("F");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[14].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[9].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) * (Convert.ToDouble(e.Row.Cells[8].Text))).ToString("F");
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[16].Visible = false;
        }


    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Response.Redirect("DeliveryChallanDetails.aspx");
        Response.Redirect("DeliveryChallan.aspx");
        //gvDeliveryChallanDetails.SelectedIndex = -1;
        tblDCDetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtremarks2.Text == "")
        {
            txtremarks2.Text = "-";
        }

        if (btnSave.Visible == false && btnRevise.Text == "Revise")
        {
            MessageBox.Show(this, "No More Items Can Be Added In this Revise Delivery Challan. Issue Another Delivery Challan For Other Items");
            return;
        }
        //if (txtQtyInHand.Text == "")
        //{
        //    MessageBox.Show(this, "We are not Having the Item in Stock");
        //    return;
        //}

        //if (txtItemQuantity.Text == "0")
        //{
        //    MessageBox.Show(this, "Item Quantity Should Be More Than Zero");
        //    return;
        //}

        //foreach (GridViewRow gvRow in gvItemDetails.Rows)
        //{
        //    if (gvRow.Cells[0].Text == ddlModelNo.SelectedItem.Value)
        //    {
        //        if (int.Parse(txtOrderedQty.Text) > int.Parse(gvRow.Cells[4].Text))
        //        {
        //            MessageBox.Show(this, "Item Quantity Should Not exceed the Ordered Quantity");
        //            return;
        //        }
        //    }
        //}
        if (txtItemQuantity.Text == "0")
        {
            MessageBox.Show(this, "Item Quantity Should Be More Than Zero");
            return;
        }
        if (Convert.ToInt32(txtQtyInHand.Text) < Convert.ToInt32(txtItemQuantity.Text))
        {
            MessageBox.Show(this, "Required Quantity is not avaliable in Stock");
            return;
        }

       


        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("ItemStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Location");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Color");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("GodownId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Inhand");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("resqty");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Status");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("StatusId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Companyid");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("DCfor");
        DeliveryItems.Columns.Add(col);


        col = new DataColumn("Remarks2");
        DeliveryItems.Columns.Add(col);


        string str = "";
        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvItmDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = DeliveryItems.NewRow();
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemName"] = txtItemName.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["SerialNo"] = txtSerialNo.Text;
                        //dr["Location"] = ddllocation.SelectedItem.Text;
                        dr["Location"] = ddlLocation.SelectedItem.Text;
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                       // dr["GodownId"] = ddllocation.SelectedItem.Value;
                        dr["GodownId"] = ddlLocation.SelectedItem.Value;

                        if (txtInhand.Text != str)
                            dr["Inhand"] = txtInhand.Text;
                        else
                            dr["Inhand"] = "0";
                        if (txtresqty.Text != str)
                            dr["resqty"] = txtresqty.Text;
                        else
                            dr["Inhand"] = "0";
                        dr["Status"] = ddlStatus.SelectedItem.Text;
                        dr["StatusID"] = ddlStatus.SelectedItem.Value;
                        dr["Companyid"] = ddlCompany.SelectedItem.Value;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["DCfor"] = ddlDcFor.SelectedItem.Value;
                        dr["Remarks2"] = txtremarks2.Text;
                        DeliveryItems.Rows.Add(dr);
                    }
                    else
                    {

                        DataRow dr = DeliveryItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ItemName"] = gvrow.Cells[3].Text;
                        dr["UOM"] = gvrow.Cells[4].Text;
                        dr["Quantity"] = gvrow.Cells[5].Text;

                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;

                        dr["ItemStatus"] = gvrow.Cells[7].Text;
                        dr["SerialNo"] = gvrow.Cells[8].Text;
                        dr["Location"] = gvrow.Cells[9].Text;

                        dr["Color"] = gvrow.Cells[10].Text;
                        dr["ColorId"] = gvrow.Cells[11].Text;

                        dr["GodownId"] = gvrow.Cells[12].Text;
                        dr["Inhand"] = gvrow.Cells[13].Text;
                        dr["resqty"] = gvrow.Cells[14].Text;
                        dr["Status"] = gvrow.Cells[15].Text;
                        dr["StatusId"] = gvrow.Cells[16].Text;
                        dr["Companyid"] = gvrow.Cells[17].Text;
                        dr["Remarks"] = gvrow.Cells[18].Text;
                        dr["DCfor"] = gvrow.Cells[19].Text;
                        dr["Remarks2"] = gvrow.Cells[20].Text;

                        DeliveryItems.Rows.Add(dr);

                    }
                }
                else
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;

                    dr["ItemStatus"] = gvrow.Cells[7].Text;
                    dr["SerialNo"] = gvrow.Cells[8].Text;
                    dr["Location"] = gvrow.Cells[9].Text;
                    dr["Color"] = gvrow.Cells[10].Text;
                    dr["ColorId"] = gvrow.Cells[11].Text;
                    dr["GodownId"] = gvrow.Cells[12].Text;
                    dr["Inhand"] = gvrow.Cells[13].Text;
                    dr["resqty"] = gvrow.Cells[14].Text;
                    dr["Status"] = gvrow.Cells[15].Text;
                    dr["StatusId"] = gvrow.Cells[16].Text;
                    dr["Companyid"] = gvrow.Cells[17].Text;
                    dr["Remarks"] = gvrow.Cells[18].Text;
                    dr["DCfor"] = gvrow.Cells[19].Text;
                    dr["Remarks2"] = gvrow.Cells[20].Text;

                    DeliveryItems.Rows.Add(dr);

                }

            }
        }

        if (gvItmDetails.Rows.Count > 0)
        {
            if (gvItmDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItmDetails.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlModelNo.SelectedItem.Value && gvrow.Cells[11].Text == ddlColor.SelectedItem.Value)
                    {
                        gvItmDetails.DataSource = DeliveryItems;
                        gvItmDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvItmDetails.SelectedIndex == -1)
        {
            DataRow drnew = DeliveryItems.NewRow();
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemName"] = txtItemName.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;

            drnew["SerialNo"] = txtSerialNo.Text;
           // drnew["Location"] = ddllocation.SelectedItem.Text;
            drnew["Location"] =  ddlLocation.SelectedItem.Text;
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
           // drnew["GodownId"] = ddllocation.SelectedItem.Value;
            drnew["GodownId"] =  ddlLocation.SelectedItem.Value;

            if (txtInhand.Text != str)
                drnew["Inhand"] = txtInhand.Text;
            else
                drnew["Inhand"] = "0";
            if (txtresqty.Text != str)
                drnew["resqty"] = txtresqty.Text;
            else
                drnew["resqty"] = "0";
            drnew["Status"] = ddlStatus.SelectedItem.Text;
            drnew["StatusId"] = ddlStatus.SelectedItem.Value;
            drnew["Companyid"] = ddlCompany.SelectedItem.Value;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["DCfor"] = ddlDcFor.SelectedItem.Value;
            drnew["Remarks2"] = txtremarks2.Text;

            DeliveryItems.Rows.Add(drnew);
        }
        gvItmDetails.DataSource = DeliveryItems;
        gvItmDetails.DataBind();

        if (rbSales.Checked)
        {

            foreach (GridViewRow gvRow in gvItmDetails.Rows)
            {
                if (gvRow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
                {
                    gvRow.Cells[7].Text = "d";

                }
            }
        }
        gvItmDetails.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlBrand.SelectedValue = "0";
        ddlModelNo.SelectedValue = "0";
        SM.SalesOrder.SalesOrderItemTypes2_Select(ddlSalesOrderNo.SelectedItem.Value, ddlModelNo);
        txtItemName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        //txtColor.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtOrderedQty.Text = string.Empty;
        txtSerialNo.Text = string.Empty;
        txtBalanceQty.Text = txtBalanceQtyHidden.Text = string.Empty;
        //ddllocation.SelectedValue = "0";
        gvItmDetails.SelectedIndex = -1;
        ddlColor.SelectedValue = "0";
        txtInhand.Text = string.Empty;
        txtresqty.Text = string.Empty;
        // ddlessential.SelectedValue = "0";
        txtQtyInHand.Text = string.Empty;
        ddlCompany.SelectedValue = "0";
        ddlDcFor.SelectedValue = "0";
        txtRemarks.Text = "";
        txtDescription.Text = "";
        txtremarks2.Text = "";

    }
    #endregion

    #region GridView  Items Row Deleting
    protected void gvItmDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItmDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Location");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("Color");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("GodownId");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("Inhand");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("resqty");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Status");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("StatusId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Companyid");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("DCfor");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks2");
        DeliveryItems.Columns.Add(col);

        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    //dr["ItemTypeId"] = gvrow.Cells[7].Text;
                    dr["ItemStatus"] = gvrow.Cells[7].Text;
                    dr["SerialNo"] = gvrow.Cells[8].Text;
                    dr["Location"] = gvrow.Cells[9].Text;

                    dr["Color"] = gvrow.Cells[10].Text;
                    dr["ColorId"] = gvrow.Cells[11].Text;
                    dr["GodownId"] = gvrow.Cells[12].Text;
                    dr["Inhand"] = gvrow.Cells[13].Text;
                    dr["resqty"] = gvrow.Cells[14].Text;
                    dr["Status"] = gvrow.Cells[15].Text;
                    dr["StatusId"] = gvrow.Cells[16].Text;
                    dr["Companyid"] = gvrow.Cells[17].Text;
                    dr["Remarks"] = gvrow.Cells[18].Text;
                    dr["DCfor"] = gvrow.Cells[19].Text;
                    dr["Remarks2"] = gvrow.Cells[20].Text;


                    DeliveryItems.Rows.Add(dr);
                }
            }
        }
        gvItmDetails.DataSource = DeliveryItems;
        gvItmDetails.DataBind();
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DcId"] != null)
        {

            tblpRint.Visible = true;
            //try
            //{
            //    string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallan&dcid=" + Request.QueryString["DcId"].ToString() + "&dcfor=" + Request.QueryString["DcFor"].ToString() + "";
            //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message);
            //}
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region  btnCancelTask
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {

        tblAssignTasks.Visible = false;
    }
    #endregion

    #region   ddlEmpNameForAssign

    protected void ddlEmpNameForAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlEmpNameForAssign.SelectedItem.Value) > 0)
            {
                txtEmpEmailId.Text = objHR.EmpEMail;
            }
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

    #region gvItmDetails_RowEditing
    protected void gvItmDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("ItemStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Location");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("GodownId");
        DeliveryItems.Columns.Add(col);


        col = new DataColumn("Color");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Inhand");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("resqty");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Status");
        DeliveryItems.Columns.Add(col);

        col = new DataColumn("StatusId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Companyid");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);

        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                DataRow dr = DeliveryItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;

                dr["ItemStatus"] = gvrow.Cells[7].Text;
                dr["SerialNo"] = gvrow.Cells[8].Text;
                dr["Location"] = gvrow.Cells[9].Text;

                dr["Color"] = gvrow.Cells[10].Text;
                dr["ColorId"] = gvrow.Cells[11].Text;
                dr["GodownId"] = gvrow.Cells[12].Text;
                dr["Inhand"] = gvrow.Cells[13].Text;
                dr["resqty"] = gvrow.Cells[14].Text;
                dr["Status"] = gvrow.Cells[15].Text;
                dr["StatusId"] = gvrow.Cells[16].Text;
                dr["Companyid"] = gvrow.Cells[17].Text;
                dr["Remarks"] = gvrow.Cells[18].Text;
                DeliveryItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvItmDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    // ddlItemType.SelectedValue = gvrow.Cells[7].Text;
                    //ItemName_Fill();
                    ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlModelNo_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtSerialNo.Text = gvrow.Cells[8].Text;
                    ddlColor.SelectedItem.Value = gvrow.Cells[11].Text;
                    //ddllocation.SelectedItem.Value = gvrow.Cells[12].Text;
                    txtInhand.Text = gvrow.Cells[13].Text;
                    ddlStatus.SelectedValue = gvrow.Cells[16].Text;
                    ddlCompany.SelectedValue = gvrow.Cells[17].Text;
                    txtRemarks.Text = gvrow.Cells[18].Text;
                    txtresqty.Text = gvrow.Cells[14].Text;



                    gvItmDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvItmDetails.DataSource = DeliveryItems;
        gvItmDetails.DataBind();
    }
    #endregion

    #region gvItmDetails_RowDataBound
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[17].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[14].Visible = false; e.Row.Cells[0].Visible = false;
            e.Row.Cells[17].Visible = false;

        }
    }
    #endregion

    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[13].Visible = false;

        }
    }

    #region  btnAssignTask
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        //tblDCDetails.Visible = false;
        //tblAssignTasks.Visible = true;
        //try
        //{
        //    Services.ServicesAssignments objServicesAssign = new Services.ServicesAssignments();
        //    Services.BeginTransaction();
        //    objServicesAssign.AssignTaskNo = txtAssignTaskNo.Text;
        //    objServicesAssign.DcId = Request.QueryString["DcId"].ToString();
        //    objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
        //    objServicesAssign.AssingDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
        //    objServicesAssign.DueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
        //    objServicesAssign.AssignRemarks = txtRemarksForAssingn.Text;
        //    objServicesAssign.AssignStatus = "New";
        //    // Inventory.Delivery.SalesEnquiryStatus_Update(SM.SMStatus.Open, gvSalesEnquiry.SelectedRow.Cells[0].Text);

        //    if (btnAssignTask.Text == "Assign")
        //    {
        //        MessageBox.Show(this, objServicesAssign.ServiceAssignments_Save());
        //        Services.CommitTransaction();
        //    }
        //    else if (btnAssignTask.Text == "Re-Assign")
        //    {
        //        MessageBox.Show(this, objServicesAssign.ServiceAssignments_Update());
        //        Services.CommitTransaction();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Services.RollBackTransaction();
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    tblDCDetails.Visible = false;
        //    tblAssignTasks.Visible = false;
        //    gvDeliveryChallanDetails.DataBind();

        //    btnAssignTask.Attributes.Clear();
        //    gvItmDetails.DataBind();
        //    gvItemDetails.DataBind();
        //    Services.ClearControls(this);
        //    Services.Dispose();
        //}
        //tblAssignTasks.Visible = false;
    }
    #endregion

    #region Assign Button Click
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        //tblDCDetails.Visible = false;
        //tblAssignTasks.Visible = true;
        //if (Request.QueryString["DcId"] != null)
        //{

        //    try
        //    {
        //        txtAssignTaskNo.Text = Services.ServicesAssignments.ServiceAssignments_AutoGenCode();
        //        Services.ServicesAssignments objServiceAssign = new Services.ServiceAssignments();
        //        if (objServiceAssign.ServiceAssignments_Select(Request.QueryString["DcId"].ToString()) > 0)
        //        {

        //            tblAssignTasks.Visible = true;

        //            btnAssignTask.Text = "Re-Assign";

        //            txtDeliveryNoForAssign.Text = string.Empty;
        //            txtDeliveryDateForAssign.Text = string.Empty;
        //            txtCustomerNameForAssingn.Text = string.Empty;
        //            txtCustomerEmailForAssingn.Text = string.Empty;
        //            ddlEmpNameForAssign.SelectedValue = "0";
        //            txtEmpEmailId.Text = string.Empty;
        //            txtRemarksForAssingn.Text = string.Empty;
        //            txtAssignDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //            txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        //            txtDeliveryNoForAssign.Text = objServiceAssign.DcNo;
        //            txtDeliveryDateForAssign.Text = objServiceAssign.DcDate;
        //            ddlEmpNameForAssign.SelectedValue = objServiceAssign.EmpId;
        //            txtAssignDate.Text = objServiceAssign.AssingDate;
        //            txtDueDate.Text = objServiceAssign.DueDate;
        //            txtRemarksForAssingn.Text = objServiceAssign.AssignRemarks;
        //            ddlEmpNameForAssign_SelectedIndexChanged(sender, e);
        //            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        //            if ((objSMCustomer.CustomerMaster_Select(objServiceAssign.CustId)) > 0)
        //            {
        //                txtCustomerNameForAssingn.Text = objSMCustomer.CompName;
        //                txtCustomerEmailForAssingn.Text = objSMCustomer.Email;
        //            }
        //            btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Re-Assign thisdeliver?');");
        //        }
        //        else
        //        {
        //            Inventory.Delivery objInventoryDe = new Inventory.Delivery();
        //            if (objInventoryDe.Delivery_Select(Request.QueryString["DcId"].ToString()) > 0)
        //            {
        //                tblAssignTasks.Visible = true;
        //                btnAssignTask.Text = "Assign";

        //                txtDeliveryNoForAssign.Text = string.Empty;
        //                txtDeliveryDateForAssign.Text = string.Empty;
        //                txtCustomerNameForAssingn.Text = string.Empty;
        //                txtCustomerEmailForAssingn.Text = string.Empty;
        //                ddlEmpNameForAssign.SelectedValue = "0";
        //                txtEmpEmailId.Text = string.Empty;
        //                txtRemarksForAssingn.Text = string.Empty;
        //                txtAssignDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //                txtDueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

        //                txtDeliveryNoForAssign.Text = objInventoryDe.DCNo;
        //                txtDeliveryDateForAssign.Text = objInventoryDe.DCDate;

        //                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        //                if ((objSMCustomer.CustomerMaster_Select(objInventoryDe.CustId)) > 0)
        //                {
        //                    txtCustomerNameForAssingn.Text = objSMCustomer.CompName;
        //                    txtCustomerEmailForAssingn.Text = objSMCustomer.Email;
        //                }
        //            }
        //            btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Assign this Delivery?');");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        Inventory.Dispose();   // NEED TO DISCUSS WHETHER SERVICE CLASS IS NEEDED OR NOT IN THIS METHOD   OR SHIFT ASSIGNMENTS IN INVENTORY CLASS
        //    }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please select atleast a Record");
        //}
    }
    #endregion

    protected void ddlDCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDCType.SelectedValue == "Returnable")
        {
            lblInwardDate.Visible = lblInwardDateValInd.Visible = txtInwardDate.Visible = true;
            rfvInwardDate.Enabled = custValInwardDate.Enabled = true;
        }
        else if (ddlDCType.SelectedValue == "Non Returnable")
        {
            lblInwardDate.Visible = lblInwardDateValInd.Visible =  txtInwardDate.Visible = false;
            rfvInwardDate.Enabled = custValInwardDate.EnableClientScript = false;
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory.Delivery objDeliveryApprove = new Inventory.Delivery();
            Inventory.BeginTransaction();
            objDeliveryApprove.DCId = Request.QueryString["DcId"].ToString();
            objDeliveryApprove.DCApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objDeliveryApprove.DeliveryDetailsApprove_Update();

            //if (txtRevisedFrom.Visible == false)
            //{
            //    foreach (GridViewRow gvrow in gvItmDetails.Rows)
            //    {
            //        if (gvrow.Cells[7].Text == "pd" || gvrow.Cells[7].Text == "d")
            //        {
            //            foreach (GridViewRow gvSORow in gvItemDetails.Rows)
            //            {
            //                if (gvrow.Cells[2].Text == gvSORow.Cells[2].Text)
            //                {
            //                    if (gvrow.Cells[7].Text == "pd")
            //                    {
            //                        if (rbSales.Checked) { SM.SalesOrder.SalesOrderDetailsItemStatus_Update(SM.SalesOrder.SOItemStatus.PartiallyDelivered, gvSORow.Cells[8].Text); }
            //                        else if (rbSpares.Checked) { Services.SparesOrder.SparesOrderDetailsItemStatus_Update(Services.SparesOrder.SOItemStatus.PartiallyDelivered, gvSORow.Cells[8].Text); }
            //                    }
            //                    else if (gvrow.Cells[7].Text == "d")
            //                    {
            //                        if (rbSales.Checked) { SM.SalesOrder.SalesOrderDetailsItemStatus_Update(SM.SalesOrder.SOItemStatus.Delivered, gvSORow.Cells[8].Text); }
            //                        else if (rbSpares.Checked) { Services.SparesOrder.SparesOrderDetailsItemStatus_Update(Services.SparesOrder.SOItemStatus.Delivered, gvSORow.Cells[8].Text); }
            //                    }
            //                }
            //            }
            //        }
            //        //////objInventory.ItemCode = gvrow.Cells[2].Text;//////objInventory.DCDetQty = gvrow.Cells[6].Text;
            //        //objDeliveryApprove.DeliveryDetailsIssueStock_Update(gvrow.Cells[2].Text, gvrow.Cells[6].Text);
            //    }
            //}
            Inventory.CommitTransaction();

            Services.InstallAssignment objassn = new Services.InstallAssignment();
            objassn.IADate = DateTime.Now.ToShortDateString();
            objassn.IAScheduleDate = DateTime.Now.AddDays(2).ToShortDateString();
            objassn.SOId = ddlSalesOrderNo.SelectedItem.Value;
            objassn.DCId = Request.QueryString["DcId"].ToString();
            objassn.InstallAssignment_Save();


            MessageBox.Show(this, "Data Approved Successfully");
        }
        catch (Exception ex)
        {
            Inventory.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvDeliveryChallanDetails.DataBind();
            Inventory.Dispose();
            btnEdit_Click(sender, e);

        }
    }
    protected void rbSales_CheckedChanged(object sender, EventArgs e)
    {
        ddlSalesOrderNo.Visible = true;

        lblSalesOrderNo.Visible = true;
        lblSalesOrderNo.Text = "Purchase Order No.";
        lblSalesOrderDate.Text = "P O Date";
        lblOrderedItemsHeading.Text = "Purchase Ordered Items";
        SalesOrder_Fill();
    }
    protected void rbSpares_CheckedChanged(object sender, EventArgs e)
    {
        lblSalesOrderNo.Text = "Spares Order No";
        lblSalesOrderDate.Text = "Spares Order Date";
        lblOrderedItemsHeading.Text = "Spares Ordered Items";
        SparesOrder_Fill();
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //Godown_Fill();
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtDescription.Text = objMaster.ItemSpec;
                txtRemarks.Text = ddlModelNo.SelectedItem.Text + ' ' + txtDescription.Text;
            }
            if (objMaster.ItemStockAvail_select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtQtyInHand.Text = objMaster.itemquantity;
            }
            tblBlocked.Visible = true;
            BindBlockedItems();
            BindStock_ModelNo();


            foreach (GridViewRow DCgvRow in gvDeliveryChallanItems.Rows)
            {
                if (Request.QueryString["DcId"] != null)
                {
                    if (DCgvRow.Cells[7].Text == Request.QueryString["DcId"].ToString())
                    {
                        if (DCgvRow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
                        {
                            //txtBalanceQty.Text = Convert.ToString(int.Parse(txtBalanceQty.Text) - int.Parse(DCgvRow.Cells[6].Text));
                            //txtBalanceQtyHidden.Text = txtBalanceQty.Text;
                        }
                    }

                    else
                    {
                        if (DCgvRow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
                        {
                            //txtBalanceQty.Text = Convert.ToString(int.Parse(txtBalanceQty.Text) - int.Parse(DCgvRow.Cells[6].Text));
                            //txtBalanceQtyHidden.Text = txtBalanceQty.Text;
                        }
                    }
                }
            }
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

    private void BindStock_ModelNo()
    {
        SqlCommand cmd = new SqlCommand("USP_Get_ModelNo_Stock", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if(dt.Rows.Count > 0)
        {
            txtQtyInHand.Text =dt.Rows[0][0].ToString();

        }
        else
        {
            txtQtyInHand.Text = "0";

        }
    }

    private void BindBlockedItems()
    {
        SqlCommand cmd = new SqlCommand("USP_CustBlockedItems", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);       
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvReservedStock.DataSource = dt;
        gvReservedStock.DataBind();
    }
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Rate");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Amount");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("DeliveryStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SODetId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Price");
        DeliveryItems.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = DeliveryItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[7].Text;
                dr["Amount"] = gvrow.Cells[8].Text;
                dr["DeliveryStatus"] = gvrow.Cells[9].Text;
                dr["SODetId"] = gvrow.Cells[10].Text;
                dr["Price"] = gvrow.Cells[11].Text;


                DeliveryItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlModelNo_SelectedIndexChanged(sender, e);
                    txtItemName.Text = gvrow.Cells[4].Text;
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    //txtItemRate.Text = gvrow.Cells[7].Text;
                    // txtRoom.Text = gvrow.Cells[10].Text;
                    gvItemDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  btnGo.Visible = false;
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Rate");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Amount");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("DeliveryStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SODetId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Price");
        DeliveryItems.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Rate"] = gvrow.Cells[7].Text;
                    dr["Amount"] = gvrow.Cells[8].Text;
                    dr["DeliveryStatus"] = gvrow.Cells[9].Text;
                    dr["SODetId"] = gvrow.Cells[10].Text;
                    dr["Price"] = gvrow.Cells[11].Text;

                    DeliveryItems.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = DeliveryItems;
        gvItemDetails.DataBind();
    }
    protected void gvPreviousPayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[5].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Paid Amount:";
            e.Row.Cells[5].Text = TotalAmount.ToString();
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
    }
    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);


                if (gvItmDetails.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvItmDetails.Rows)
                    {
                        if (gvItmDetails.SelectedIndex > -1)
                        {
                            if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                            {
                                DataRow dr = DeliveryItems.NewRow();
                                dr["ItemCode"] = gvrow.Cells[2].Text;
                                dr["ModelNo"] = gvrow.Cells[3].Text;
                                dr["ItemName"] = gvrow.Cells[4].Text;
                                dr["UOM"] = gvrow.Cells[5].Text;
                                dr["Quantity"] = gvrow.Cells[6].Text;
                                dr["ItemStatus"] = gvrow.Cells[7].Text;
                                dr["SerialNo"] = gvrow.Cells[8].Text;

                                DeliveryItems.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = DeliveryItems.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ModelNo"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["UOM"] = gvrow1.Cells[5].Text;
                                dr["Quantity"] = gvrow1.Cells[6].Text;
                                dr["ItemStatus"] = gvrow1.Cells[7].Text;
                                dr["SerialNo"] = gvrow1.Cells[8].Text;

                                DeliveryItems.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            DataRow dr = DeliveryItems.NewRow();
                            dr["ItemCode"] = gvrow1.Cells[2].Text;
                            dr["ModelNo"] = gvrow1.Cells[3].Text;
                            dr["ItemName"] = gvrow1.Cells[4].Text;
                            dr["UOM"] = gvrow1.Cells[5].Text;
                            dr["Quantity"] = gvrow1.Cells[6].Text;

                            dr["ItemStatus"] = gvrow1.Cells[7].Text;
                            dr["SerialNo"] = gvrow1.Cells[8].Text;

                            DeliveryItems.Rows.Add(dr);
                        }
                        if (gvItmDetails.SelectedIndex == -1)
                        {
                            if (gvrow.Cells[3].Text == gvrow1.Cells[3].Text)
                            {
                                gvItmDetails.DataSource = DeliveryItems;
                                gvItmDetails.DataBind();
                                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                                btnItemRefresh_Click(sender, e);
                                ch.Checked = false;
                                return;
                            }
                        }

                    }
                }
                if (gvItmDetails.SelectedIndex == -1)
                {
                    DataRow drnew = DeliveryItems.NewRow();
                    drnew["ItemCode"] = gvrow.Cells[2].Text;
                    drnew["ModelNo"] = gvrow.Cells[3].Text;
                    drnew["ItemName"] = gvrow.Cells[4].Text;
                    drnew["UOM"] = gvrow.Cells[5].Text;
                    drnew["Quantity"] = gvrow.Cells[6].Text;
                    drnew["ItemStatus"] = gvrow.Cells[7].Text;
                    drnew["SerialNo"] = gvrow.Cells[8].Text;


                    DeliveryItems.Rows.Add(drnew);
                }
                gvItmDetails.DataSource = DeliveryItems;
                gvItmDetails.DataBind();
                gvItmDetails.SelectedIndex = -1;
                btnItemRefresh_Click(sender, e);
                ch.Checked = false;
            }
        }
    }
    //protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //    if (objMaster.ItemColorcompanylocation_select(ddlModelNo.SelectedItem.Value,ddlColor.SelectedItem.Value, ddlCompany.SelectedItem.Value, ddllocation.SelectedValue) > 0)
    //    {
    //        txtQtyInHand.Text = objMaster.itemquantity;
    //    }
    //}
    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvItemDetails.Rows)
        {
            if (gvRow.Cells[2].Text == ddlModelNo.SelectedItem.Value && gvRow.Cells[13].Text == ddlColor.SelectedItem.Value)
            {
                txtOrderedQty.Text = gvRow.Cells[6].Text;
                txtBalanceQty.Text = txtOrderedQty.Text;
                txtBalanceQtyHidden.Text = txtBalanceQty.Text;
            }
        }
        //Masters.ItemMaster objMaster = new Masters.ItemMaster();
        //if (objMaster.ItemColorStockAvail_select(ddlModelNo.SelectedItem.Value,ddlColor.SelectedValue) > 0)
        //{
        //    txtQtyInHand.Text = objMaster.itemquantity;
        //}
        BindStock_Color();
        //ddlLocation.DataSource = null;
        //ddlLocation.DataBind();

        //lblCompany.Text = cp.getPresentCompanySessionValue();
        //ddlLocation.DataBind();
    }

    private void BindStock_Color()
    {
        SqlCommand cmd = new SqlCommand("USP_Get_ModelNo_Color_Stock", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ColourId", ddlColor.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtQtyInHand.Text =dt.Rows[0][0].ToString();

        }
        else
        {
            txtQtyInHand.Text = "0";

        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtQtyInHand.Text = "";

        //Masters.ItemMaster objMaster = new Masters.ItemMaster();
        //if (objMaster.ItemColorCompanyStockAvail_select(ddlModelNo.SelectedItem.Value, ddlColor.SelectedValue,ddlCompany.SelectedItem.Value) > 0)
        //{
        //    txtQtyInHand.Text = objMaster.itemquantity;
        //}
       // ddllocation_SelectedIndexChanged(sender, e);
        //TextBox2_TextChanged(sender, e);
        ddlLocation.Items.Clear();
        lblCompany.Text = ddlCompany.SelectedItem.Value;
        ddlLocation.DataBind();
        ddlLocation_SelectedIndexChanged(sender, e);

    }
    protected void rdbHighsale_CheckedChanged(object sender, EventArgs e)
    {
        ddlSalesOrderNo.Visible = true;
        lblSalesOrderNo.Visible = true;
        lblSalesOrderNo.Text = "Purchase Order No.";
        lblSalesOrderDate.Text = "P O Date";
        lblOrderedItemsHeading.Text = "Purchase Ordered Items";
        SM.SalesOrder.SalesOrderForDeliveryHighsale_Select(ddlSalesOrderNo);
    }
    protected void rdbCashaccount_CheckedChanged(object sender, EventArgs e)
    {
        ddlSalesOrderNo.Visible = true;
        lblSalesOrderNo.Visible = true;
        lblSalesOrderNo.Text = "Purchase Order No.";
        lblSalesOrderDate.Text = "P O Date";
        lblOrderedItemsHeading.Text = "Purchase Ordered Items";
        SalesOrder_Fill();
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SM.DDLBindWithSelect(ddlModelNo, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID ='" + ddlBrand.SelectedItem.Value + "' order by  YANTRA_ITEM_MAST.ITEM_MODEL_NO asc ");
        //Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlModelNo, ddlBrand.SelectedItem.Value);
        Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelNo, ddlBrand.SelectedItem.Value);
        //Masters.ItemMaster.ItemMaster5_Select(ddlModelNo, ddlBrand.SelectedItem.Value);
    
    }
    protected void chkOriginal_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkDuplicate.Checked = false;
            chktriplicate.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanOri&dcid=" + Request.QueryString["DcId"].ToString() + "&dcfor=" + Request.QueryString["DcFor"].ToString() + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chkDuplicate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkOriginal.Checked = false;
            chktriplicate.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanDup&dcid=" + Request.QueryString["DcId"].ToString() + "&dcfor=" + Request.QueryString["DcFor"].ToString() + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chktriplicate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkDuplicate.Checked = false;
            chkOriginal.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanTri&dcid=" + Request.QueryString["DcId"].ToString() + "&dcfor=" + Request.QueryString["DcFor"].ToString() + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlSalesOrderNo.DataSourceID = "SqlDataSource1";
        ddlSalesOrderNo.DataTextField = "SO_NO";
        ddlSalesOrderNo.DataValueField = "SO_ID";
        ddlSalesOrderNo.DataBind();
        ddlSalesOrderNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
        {
            txtunitaddress.Text = objSMCustomer.CustUnitAddress;
        }

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvDeliveryChallanDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvDeliveryChallanDetails.DataBind();
    }

    

    protected string getLocations()
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        if (lid.Equals(""))
        {
            m = "<a href='locid='>All</a>";
        }
        else
        {
            m = "<a href='?locid=" + lid + "'>" + locations.getLocationsName(lid) + "</a>";
        }

        DataTable dt = WH.getWarehouseLocations();

        string s = "";

        foreach (DataRow dr in dt.Rows)
        {
            s += "<li><a href='?locid=" + dr["locid"].ToString() + "'>" + dr["locname"].ToString() + "</a></li>";
        }

        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }

    protected string getWarehouses()
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        string s = "";

        if (lid.Equals(""))
        {
            m = "";
        }
        else
        {
            string whid = (Request.QueryString["wh_id"] == null) ? "" : Request.QueryString["wh_id"];

            if (whid.Equals(""))
            {
                m = "<a href='?locid=" + lid + "'>ALL</a>";
            }
            else
            {
                m = "<a href='?locid=" + lid + "&wh_id=" + whid + "'>" + WH.getWarehouseName(whid) + "</a>";
            }



            DataTable dt = WH.getWarehouses(lid);

            s = "";

            foreach (DataRow dr in dt.Rows)
            {
                s += "<li><a href='?locid=" + lid + "&wh_id=" + dr["wh_id"].ToString() + "'>" + dr["whname"].ToString() + "</a></li>";
            }
        }


        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }

    protected string getSubSections(string whLocId)
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        string s = "";

        if (lid.Equals(""))
        {
            m = "";
        }
        else
        {
            string whid = (Request.QueryString["wh_id"] == null) ? "" : Request.QueryString["wh_id"];


            if (whid.Equals(""))
            {
                m = "";
            }
            else
            {
                DataTable dt = new DataTable();

                switch (whLocId)
                {
                    case "":
                    case "0":
                        m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";

                        dt = WH_Locations.getLocations2(whid, whLocId);

                        s = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                        }

                        break;

                    default:
                        m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + whLocId + "'>" + WH_Locations.getLocationName(whLocId) + "</a>";

                        string parentid = WH_Locations.getParentId(whLocId);

                        dt = WH_Locations.getLocations2(whid, parentid);

                        s = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                        }
                        break;
                }

                //string whLocId = "";

                //whLocId = (Request.QueryString["whLocId"] == null) ? "" : Request.QueryString["whLocId"];

                //if (whLocId.Equals(""))
                //{
                //    m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";
                //}
                //else if (whLocId.Equals("0"))
                //{
                //    m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";


                //}
                //else
                //{
                //    //m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";
                //    m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + whLocId + "'>" + WH_Locations.getLocationName(whLocId) + "</a>";

                //    string parentid = WH_Locations.getParentId(whLocId);

                //    DataTable dt = WH_Locations.getLocations2(whid, parentid);

                //    s = "";

                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                //    }
                //}

            }
        }


        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }

    protected string getSubSections_sub(string whLocId)
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        string s = "";

        if (lid.Equals(""))
        {
            m = "";
        }
        else
        {
            string whid = (Request.QueryString["wh_id"] == null) ? "" : Request.QueryString["wh_id"];


            if (whid.Equals(""))
            {
                m = "";
            }
            else
            {
                DataTable dt = new DataTable();

                m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + whLocId + "'>All</a>";

                dt = WH_Locations.getLocations2(whid, whLocId);

                s = "";

                foreach (DataRow dr in dt.Rows)
                {
                    s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                }


            }
        }


        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }
    #region Treeview

    

    protected void setWHLocations(TreeNode tnode)
    {
        string locid = tnode.Value;
        DataTable dt = WH.getWarehouses(locid);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["wh_id"].ToString();
            tn.Text = dr["whname"].ToString();

            setWHLocations_Sections(tn, dr["wh_id"].ToString());

            tnode.ChildNodes.Add(tn);
        }
    }

    protected void setWHLocations_Sections(TreeNode tnode, string wh_id, string parentid = "0")
    {
        //string wh_id = tnode.Value;
        DataTable dt = WH_Locations.getLocations2(wh_id, parentid);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["whLocId"].ToString();
            tn.Text = dr["whLocName"].ToString();

            parentid = dr["whLocId"].ToString();

            setWHLocations_Sections(tn, wh_id, parentid);

            tnode.ChildNodes.Add(tn);
        }
    }
    #endregion
   
    protected void lbtnDCNo_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDCNo;
        lbtnDCNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDCNo.Parent.Parent;
        gvDeliveryChallanItems.SelectedIndex = gvRow.RowIndex;
        btnDeleteItem.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        lblDCItemCode.Text= gvDeliveryChallanItems.SelectedRow.Cells[2].Text;
        lblQty.Text = gvDeliveryChallanItems.SelectedRow.Cells[6].Text;
        lblLoc.Text = gvDeliveryChallanItems.SelectedRow.Cells[10].Text;
        lblDCDetId.Text = gvDeliveryChallanItems.SelectedRow.Cells[13].Text;
    }
    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
       // Inward_Save();
       Outward_Delete();
       PO_Delete();
        Inventory.Delivery objInventory = new Inventory.Delivery();
        objInventory.DeliveryItem_Delete(lblDCDetId.Text);
        Inventory.Delivery objDelivery = new Inventory.Delivery();
        objDelivery.DeliveryDetails_Select(Request.QueryString["DcId"].ToString(), gvDeliveryChallanItems);
         //DeliveryItem_Delete
    }

    private void PO_Delete()
    {
        Inventory.Delivery objInventory = new Inventory.Delivery();
        objInventory.iqitemcode = lblDCItemCode.Text;
        objInventory.iqcolorid = gvDeliveryChallanItems.SelectedRow.Cells[14].Text;
        objInventory.DCDetQty = lblQty.Text;
        objInventory.SOId = ddlSalesOrderNo.SelectedValue;
        // objInventory.SoDetId = gvrow.Cells[16].Text;
        objInventory.POBalanceQty_Update();
    }

    private void Outward_Delete()
    {
        for(int i=0;i<Convert.ToInt32(lblQty.Text);i++)
        {
            SqlCommand cmd = new SqlCommand("delete top (1) from [OUTWARD] where [ITEM_CODE]=" + lblDCItemCode.Text + " and CUSTOMERID=" + lblCustId.Text + "", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    private void Inward_Save()
    {
            string ItemCat = "";
            string ItemSubCat = "";
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(lblDCItemCode.Text) > 0)
            {
                ItemCat = objMaster.ItemCategoryName;
                ItemSubCat = objMaster.ItemType;
            }
            Masters.ItemPurchase obj = new Masters.ItemPurchase();
            obj.RefNo = txtDeliveryChallanNo.Text;
            obj.ItemCode = lblDCItemCode.Text;
            obj.ItemCategory = ItemCat;
            obj.ItemSubCategory = ItemSubCat;
            obj.ColorId = "18";
            obj.qty = lblQty.Text;
            obj.BalanceQty = lblQty.Text;
            obj.DamageQty = "0";
            obj.CpId = cp.getPresentCompanySessionValue();
            obj.InwardType = "SR";
            obj.DateAdded = DateTime.Now.ToString();
            obj.ItemLoc = lblLoc.Text;

            obj.InwardTemp_Save();
        

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster objMaster = new Masters.ItemMaster();
        if (objMaster.ItemColorcompanylocation_select(ddlModelNo.SelectedItem.Value, ddlColor.SelectedItem.Value, ddlCompany.SelectedItem.Value, WH_Locations.getLocationID(TextBox2_value.Value)) > 0)
        {
            txtQtyInHand.Text = objMaster.itemquantity;
        }
    }
    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvReservedStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
        }
        
    }
   // DataTable ReleasedItems = new DataTable();
    protected void btnReleaseBlock_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {
                    string blockStock = gvrow.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int b = Convert.ToInt32(blockStock);
                    int q = Convert.ToInt32(qty.Text);


                    if (b >= q)
                    {
                        SqlCommand cmd = new SqlCommand("USP_ReleaseBlockItems", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@qty", q);
                        cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[0].Text);
                        cmd.Parameters.AddWithValue("@ColorId", gvrow.Cells[8].Text);
                        cmd.Parameters.AddWithValue("@DeliveryDate", gvrow.Cells[5].Text);
                        cmd.Parameters.AddWithValue("@CustId", gvrow.Cells[9].Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    else
                    {
                        MessageBox.Show(this, "This Operation Cannot Be Performed.");
                    }

                }

            }
          

            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {

                     DataTable ReleasedItems = new DataTable();

                    DataColumn col = new DataColumn();

                    col = new DataColumn("Item_Code");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("ITEM_MODEL_NO");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("COLOUR_NAME");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("Quantity");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("CUST_NAME");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("Delivery_Date");
                    ReleasedItems.Columns.Add(col);

                    if (gvReleasedItems.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvrow1 in gvReleasedItems.Rows)
                        {
                            if(gvReleasedItems.SelectedIndex>-1)
                            { 
                                if(gvrow.RowIndex==gvReleasedItems.SelectedRow.RowIndex)
                            {

                           
                            DataRow dr = ReleasedItems.NewRow();
                            dr["Item_Code"] = gvrow.Cells[0].Text;
                            dr["ITEM_MODEL_NO"] = gvrow.Cells[1].Text;
                            dr["COLOUR_NAME"] = gvrow.Cells[2].Text;
                            TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                            dr["Quantity"] = qty.Text;
                            dr["CUST_NAME"] = gvrow.Cells[4].Text;
                            dr["Delivery_Date"] = gvrow.Cells[5].Text;
                            ReleasedItems.Rows.Add(dr);
                            }
                                else
                                {
                                    DataRow dr = ReleasedItems.NewRow();
                                    dr["Item_Code"] = gvrow1.Cells[0].Text;
                                    dr["ITEM_MODEL_NO"] = gvrow1.Cells[1].Text;
                                    dr["COLOUR_NAME"] = gvrow1.Cells[2].Text;
                                    dr["Quantity"] = gvrow1.Cells[3].Text;
                                    dr["CUST_NAME"] = gvrow1.Cells[4].Text;
                                    dr["Delivery_Date"] = gvrow1.Cells[5].Text;
                                    ReleasedItems.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = ReleasedItems.NewRow();
                                dr["Item_Code"] = gvrow1.Cells[0].Text;
                                dr["ITEM_MODEL_NO"] = gvrow1.Cells[1].Text;
                                dr["COLOUR_NAME"] = gvrow1.Cells[2].Text;
                                dr["Quantity"] = gvrow1.Cells[3].Text;
                                dr["CUST_NAME"] = gvrow1.Cells[4].Text;
                                dr["Delivery_Date"] = gvrow1.Cells[5].Text;
                                ReleasedItems.Rows.Add(dr);
                            }
                        }
                    }

                    if (gvReleasedItems.SelectedIndex == -1)
                    {
                        DataRow drnew = ReleasedItems.NewRow();
                        drnew["Item_Code"] = gvrow.Cells[0].Text;
                        drnew["ITEM_MODEL_NO"] = gvrow.Cells[1].Text;
                        drnew["COLOUR_NAME"] = gvrow.Cells[2].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        drnew["Quantity"] = qty.Text;
                        drnew["CUST_NAME"] = gvrow.Cells[4].Text;
                        drnew["Delivery_Date"] = gvrow.Cells[5].Text;

                        ReleasedItems.Rows.Add(drnew);
                    }
                    gvReleasedItems.DataSource = ReleasedItems;
                    gvReleasedItems.DataBind();
                    gvReleasedItems.SelectedIndex = -1;
                    ch.Checked = false;
                }
            }


            

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            BindBlockedItems();
            btnReleaseBlock.Visible = false;
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvReservedStock.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                btnReleaseBlock.Visible = true;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        DataRow dr; 
        dt.Columns.Add(new System.Data.DataColumn("Item Code", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Item Model No", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Colour", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Quantity", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Customer Name", typeof(String)));
        dt.Columns.Add(new System.Data.DataColumn("Delivery Date", typeof(String)));
  
        foreach (GridViewRow row in gvReleasedItems.Rows)
        {
               dr = dt.NewRow();
               dr[0] = row.Cells[0].Text;
               dr[1] = row.Cells[1].Text;
               dr[2] = row.Cells[2].Text;
               dr[3] = row.Cells[3].Text;
               dr[4] = row.Cells[4].Text;
               dr[5] = row.Cells[5].Text;
       
            dt.Rows.Add(dr);
        }
        Session["GridData"] = dt;
        Response.Redirect("~/Modules/SCM/ChangedIndentDetails.aspx");


        //Session["vl_Indent"] = dt;
        ////dt2 = (DataTable)Session["ss"];
        //Response.Redirect("~/Modules/SCM/ChangedIndentDetails.aspx?Indent=" + "Release");
    }
    protected void gvReleasedItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStock_Location();
    }

    private void BindStock_Location()
    {
        SqlCommand cmd = new SqlCommand("USP_Get_ModelNo_Color_Loc_Stock", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ColourId", ddlColor.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@LocationId", ddlLocation.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtQtyInHand.Text = dt.Rows[0][0].ToString();

        }
        else
        {
            txtQtyInHand.Text = "0";

        }
    }
    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
        {           
            lblQty.Text = gvrow.Cells[6].Text;
            lblDCItemCode.Text = gvrow.Cells[2].Text;
            Outward_Delete();
            Inventory.Delivery objInventory = new Inventory.Delivery();
            objInventory.DeliveryItem_Delete(gvrow.Cells[2].Text);
            objInventory.DeliveryChallen_Delete(Request.QueryString["DcId"]);

        }
    }
}

 
