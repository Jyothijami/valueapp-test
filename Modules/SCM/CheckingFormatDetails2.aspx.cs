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
using Yantra.MessageBox;
using YantraBLL.Modules;
using Yantra.Classes;
using vllib;


public partial class Modules_SCM_CheckingFormatDetails2 : basePage
{
    static DataTable PurchaseInvoiceProducts = new DataTable();
    DataTable tb = new DataTable();
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GridView1.Columns[0].Visible = false;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            Session["ss"] = tb;
            GridView1.DataSource = tb;
            EmployeeMaster_Fill();
            PurchaseOrder_Fill();
            Godown_Fill();
            supliername_fill();
            CompanyName_Fill();

            setPurchaseInvoiceProducts();
            loadPurchaseInvoiceProducts();


            txtReceivedOn.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            if (Request.QueryString["ChkId"] != null)
            {
                //Link Btn Code
                rdbWithPo.Enabled = false;
                rdbWithoutPo.Enabled = false;
                txtSuplierInvoiceNowithoutpo.Visible = false;
                tblCheckDetails.Visible = false;
                ddlPONO.Visible = false;
                ddlSupInvNo.Visible = false;
                txtSlpl.Visible = true;
                txtSupplier.Visible = true;
                txtwithoutpono.Visible = false;
                //LinkButton lbtnCheckingNo;
                //lbtnCheckingNo = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)lbtnCheckingNo.Parent.Parent;
                //gvCheckForm.SelectedIndex = gvRow.RowIndex;
                //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

                try
                {
                    SCM.CheckingFormat objchkf = new SCM.CheckingFormat();
                    if (objchkf.CheckingFormat_Select(Request.QueryString["ChkId"].ToString()) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = false;
                        tblCheckDetails.Visible = true;
                        txtCkNo.Text = objchkf.CHKNo;
                        txtCheckingDate.Text = objchkf.CHKDate;
                        //ddlItemType.SelectedValue = objchkf.CHKEquipmentName;
                        //txtManufacturer.Text = objchkf.CHKManufacturerName;
                        txtReceivedOn.Text = objchkf.CHKReceivedOn;
                        //ddlItemName.SelectedValue = objchkf.CHKModel;
                        //txtSerialNo.Text = objchkf.CHKSerialNo;
                        ddlSupplierName.SelectedValue = objchkf.CHKManufacturerName;


                        if (objchkf.CHKPONo == "W/O-PO")
                        {
                            rdbWithoutPo.Checked = true;
                            rdbWithPo.Checked = false;
                        }
                        else
                        {
                            rdbWithoutPo.Checked = false;
                            rdbWithPo.Checked = true;
                        }
                        txtSlpl.Text = objchkf.CHKPONo;
                        txtPODate.Text = objchkf.CHKPODate;
                        txtSupplier.Text = objchkf.CHKInvoiceNo;
                        txtSupplierDate.Text = objchkf.CHKInvoiceDate;
                        txtRemarks.Text = objchkf.CHKRemarks;
                        ddlPreparedBy.SelectedItem.Text = objchkf.CHKPreparedBy;
                        ddlApprovedBy.SelectedItem.Text = objchkf.CHKApprovedBy;
                        ddlStoreIncharge.SelectedItem.Text = objchkf.CHKSTOREINCHARGE;
                        ddlAccounts.SelectedItem.Text = objchkf.CHKACCOUNTS;
                        ddlApayments.SelectedItem.Text = objchkf.CHKAUTHORISEDPAYMENTS;
                        txtlrno.Text = objchkf.CHKLrno;
                        txtGatepass.Text = objchkf.CHKGatepass;
                        ddlCompanyName.SelectedValue = objchkf.CHKCPID;

                        objchkf.ChekingFormatDetailsscm_Select(Request.QueryString["ChkId"].ToString(), GridView1);
                        gvItemDetails.DataBind();
                        GridView1.DataBind();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    SCM.Dispose();
                }


                //Edit Button Code
                rdbWithPo.Enabled = false;
                rdbWithoutPo.Enabled = false;
                txtSuplierInvoiceNowithoutpo.Visible = false;
                ddlPONO.Visible = false;
                ddlSupInvNo.Visible = false;
                txtSlpl.Visible = true;
                txtSupplier.Visible = true;
                txtwithoutpono.Visible = false;
                try
                {
                    SCM.CheckingFormat objchkf = new SCM.CheckingFormat();
                    if (objchkf.CheckingFormat_Select(Request.QueryString["ChkId"].ToString()) > 0)
                    {
                        btnSave.Text = "Update";
                        btnSave.Enabled = true;
                        tblCheckDetails.Visible = true;
                        txtCkNo.Text = objchkf.CHKNo;
                        txtCheckingDate.Text = objchkf.CHKDate;
                        //ddlItemType.SelectedValue = objchkf.CHKEquipmentName;
                        ddlSupplierName.SelectedValue = objchkf.CHKManufacturerName;
                        txtReceivedOn.Text = objchkf.CHKReceivedOn;
                        //ddlItemName.SelectedValue = objchkf.CHKModel;
                        // txtSerialNo.Text = objchkf.CHKSerialNo;
                        txtSlpl.Text = objchkf.CHKPONo;
                        txtPODate.Text = objchkf.CHKPODate;
                        txtSupplier.Text = objchkf.CHKInvoiceNo;
                        txtSupplierDate.Text = objchkf.CHKInvoiceDate;
                        ddlCompanyName.SelectedValue = objchkf.CHKCPID;
                        //txtRemarks.Text = objchkf.CHKRemarks;
                        ddlPreparedBy.SelectedItem.Text = objchkf.CHKPreparedBy;
                        ddlApprovedBy.SelectedItem.Text = objchkf.CHKApprovedBy;
                        if (objchkf.CHKPONo == "W/O-PO")
                        {
                            rdbWithoutPo.Checked = true;
                            rdbWithPo.Checked = false;
                        }
                        else
                        {
                            rdbWithoutPo.Checked = false;
                            rdbWithPo.Checked = true;
                        }
                        ddlStoreIncharge.SelectedItem.Text = objchkf.CHKSTOREINCHARGE;
                        ddlAccounts.SelectedItem.Text = objchkf.CHKACCOUNTS;
                        ddlApayments.SelectedItem.Text = objchkf.CHKAUTHORISEDPAYMENTS;
                        objchkf.ChekingFormatDetailsscm_Select(Request.QueryString["ChkId"].ToString(), GridView1);
                        GridView1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    SCM.Dispose();
                }

            }
            else
            {
                SCM.ClearControls(this);
                txtCkNo.Text = SCM.CheckingFormat.CheckingFormat_AutoGenCode();
                txtCheckingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                txtSupplier.Visible = false;
                txtSlpl.Visible = false;
                ddlSupInvNo.Visible = true;
                ddlPONO.Visible = true;
                gvItemDetails.DataBind();
                rdbWithPo.Checked = true;
                rdbWithPo_CheckedChanged(sender, e);
                rdbWithoutPo.Checked = false;
                gvItDetails.DataBind();
                GridView1.DataBind();
                tblCheckDetails.Visible = true;
                rdbWithPo.Enabled = true;
                rdbWithoutPo.Enabled = true;
            }

        }
    }
    #endregion

    protected void setPurchaseInvoiceProducts()
    {
        PurchaseInvoiceProducts = new DataTable();

        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemType");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("OQuantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("RQuantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("AQuantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("ReQuantity");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("NetQty");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Rate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Amount");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Godown");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Godownid");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Color");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Colorid");
        PurchaseInvoiceProducts.Columns.Add(col);
    }

    protected void loadPurchaseInvoiceProducts()
    {
        if (Session["vl_TempInward"] != null)
        {
            DataTable dtInward = new DataTable();
            dtInward = (DataTable)Session["vl_TempInward"];

            foreach (DataRow dr in dtInward.Rows)
            {
                DataRow drnew = PurchaseInvoiceProducts.NewRow();
                drnew["ItemCode"] = dr["ITEM_CODE"].ToString();
                drnew["ItemType"] = "0";
                drnew["ItemName"] = dr["ITEM_NAME"].ToString();
                drnew["OQuantity"] = dr["FPO_DET_QTY"].ToString();
                drnew["RQuantity"] = dr["NewQTY"].ToString();

                drnew["AQuantity"] = dr["NewQTY"].ToString();
                drnew["ReQuantity"] = "0";
                drnew["NetQty"] = dr["NewQty"].ToString();
                drnew["Rate"] = dr["FPO_DET_RATE"].ToString();
                drnew["Godown"] = "0";
                drnew["Godownid"] = "0";
                drnew["Remarks"] = dr["FPO_DET_REMARKS"].ToString();
                drnew["Color"] = dr["COLOUR_NAME"].ToString();
                drnew["Colorid"] = dr["Color_Id"].ToString();

                PurchaseInvoiceProducts.Rows.Add(drnew);
            }
        }

        //DataRow drnew = PurchaseInvoiceProducts.NewRow();
        //drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
        //drnew["ItemType"] = ddlItemName.SelectedItem.Text;
        //drnew["ItemName"] = txtItemName.Text;
        //drnew["OQuantity"] = txtQty.Text;
        //drnew["RQuantity"] = txtRecqty.Text;

        //drnew["AQuantity"] = txtAcceptedqty.Text;
        //drnew["ReQuantity"] = txtRejectedqty.Text;
        //drnew["NetQty"] = txtAcceptedqty.Text;
        //drnew["Rate"] = txtRate.Text;
        //drnew["Godown"] = ddlgodown.SelectedItem.Text;
        //drnew["Godownid"] = ddlgodown.SelectedItem.Value;
        //drnew["Remarks"] = txtRemarks.Text;
        //drnew["Color"] = ddlcolor.SelectedItem.Text;
        //drnew["Colorid"] = ddlcolor.SelectedItem.Value;

        //PurchaseInvoiceProducts.Rows.Add(drnew);

        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
    }

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompanyName);
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

    #region Suplier Name
    private void supliername_fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    #endregion

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //if (Request.QueryString["ChkId"] != null)
        //{
        //    if (!string.IsNullOrEmpty(gvCheckForm.SelectedRow.Cells[7].Text) && gvCheckForm.SelectedRow.Cells[7].Text != "&nbsp;")
        //    {
        //        btnApprove.Visible = false;
        //        btnSave.Visible = false;
        //        btnRefresh.Visible = false;
        //        //btnPrint.Visible = true;
        //    }
        //    else
        //    {
        //        btnApprove.Visible = true;
        //        btnSave.Visible = true;
        //        btnRefresh.Visible = false;
        //        //btnPrint.Visible = false;
        //    }
        //}
        //else
        //{
        //    btnSave.Visible = true;
        //    btnRefresh.Visible = true;
        //    btnApprove.Visible = false;
        //    //btnPrint.Visible = false;
        //}
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.DDLBindWithSelect(ddlStoreIncharge, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 10 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.DDLBindWithSelect(ddlApprovedBy, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 10 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlAccounts);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlStoreIncharge);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlApayments);
            HR.DDLBindWithSelect(ddlApayments, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 11 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            HR.DDLBindWithSelect(ddlAccounts, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 3 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");

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

    #region Purchase Order Fill
    private void PurchaseOrder_Fill()
    {
        try
        {
            SCM.SupplierFixedPO.SuppliersFixedPO_Select(ddlPONO);
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

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            // Masters.ItemType.ItemType_Select(ddlItemType);

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

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemName);

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

    #region Godown Name Fill
    private void Godown_Fill()
    {
        try
        {
            Masters.ItemMaster.Godown_select(ddlgodown);

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
        SCM.ClearControls(this);
        txtCkNo.Text = SCM.CheckingFormat.CheckingFormat_AutoGenCode();
        txtCheckingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        txtSupplier.Visible = false;
        txtSlpl.Visible = false;
        ddlSupInvNo.Visible = true;
        ddlPONO.Visible = true;
        gvItemDetails.DataBind();
        rdbWithPo.Checked = true;
        rdbWithPo_CheckedChanged(sender, e);
        rdbWithoutPo.Checked = false;
        gvItDetails.DataBind();
        GridView1.DataBind();
        tblCheckDetails.Visible = true;
        rdbWithPo.Enabled = true;
        rdbWithoutPo.Enabled = true;
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (btnSave.Text == "Save")
        {
            CheckFormatSave();
        }
        else if (btnSave.Text == "Update")
        {
            CheckingFormaUpdate();
        }
    }
    #endregion

    #region Checking Format Save

    private void CheckFormatSave()
    {
        try
        {
            SCM.CheckingFormat objchkf = new SCM.CheckingFormat();
            SCM.BeginTransaction();
            objchkf.CHKNo = txtCkNo.Text;
            objchkf.CHKDate = Yantra.Classes.General.toMMDDYYYY(txtCheckingDate.Text);
            objchkf.CHKEquipmentName = txtItemName.Text;
            objchkf.CHKManufacturerName = ddlSupplierName.SelectedItem.Value;
            if (rdbWithoutPo.Checked == true)
            {

                objchkf.CHKPONo = "W/O-PO";
                objchkf.CHKInvoiceNo = txtSuplierInvoiceNowithoutpo.Text;

            }
            if (rdbWithPo.Checked == true)
            {

                objchkf.CHKPONo = txtSlpl.Text;
                objchkf.CHKInvoiceNo = ddlSupInvNo.SelectedItem.Text;
            }
            objchkf.CHKReceivedOn = Yantra.Classes.General.toMMDDYYYY(txtReceivedOn.Text);
            objchkf.CHKModel = ddlItemName.SelectedItem.Value;
            objchkf.CHKSerialNo = txtSerialNo.Text;
            objchkf.CHKPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
            objchkf.CHKInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtSupplierDate.Text);
            objchkf.CHKPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
            if (ddlApprovedBy.SelectedItem.Value == "0")
            {
                objchkf.CHKApprovedBy = "";
            }
            else
            {
                objchkf.CHKApprovedBy = ddlApprovedBy.SelectedItem.Text;
            }
            objchkf.ItemTypeId = "0";
            objchkf.ItemCode = "0";
            objchkf.CHKQty = txtQty.Text;
            objchkf.CHKGatepass = txtGatepass.Text;
            objchkf.CHKLrno = txtlrno.Text;
            objchkf.CHKCPID = ddlCompanyName.SelectedItem.Value;
            if (ddlStoreIncharge.SelectedItem.Value == "0")
            {

                objchkf.CHKSTOREINCHARGE = "";
            }
            else
            {
                objchkf.CHKSTOREINCHARGE = ddlStoreIncharge.SelectedItem.Text;
            }


            if (ddlApayments.SelectedItem.Value == "0")
            {

                objchkf.CHKAUTHORISEDPAYMENTS = "";
            }
            else
            {
                objchkf.CHKAUTHORISEDPAYMENTS = ddlApayments.SelectedItem.Text;
            }

            if (ddlAccounts.SelectedItem.Value == "0")
            {

                objchkf.CHKACCOUNTS = "";
            }
            else
            {
                objchkf.CHKACCOUNTS = ddlAccounts.SelectedItem.Text;
            }

            if (objchkf.CheckingFormat_Save() == "Data Saved Successfully")
            {
                objchkf.ChekingFormatDetails_Delete(objchkf.CHKId);
                foreach (GridViewRow gvrow in gvItemDetails.Rows)
                {
                    objchkf.CHKDETITEMCODE = gvrow.Cells[2].Text;
                    objchkf.CHKDETGODOWNID = gvrow.Cells[13].Text;
                    objchkf.CHKDETORDEREDQTY = gvrow.Cells[6].Text;
                    objchkf.CHKDETACCEPTEDQTY = gvrow.Cells[8].Text;
                    objchkf.CHKDETRECEIVEDQTY = gvrow.Cells[7].Text;
                    objchkf.CHKDETREJECTEDQTY = gvrow.Cells[9].Text;
                    objchkf.CHKDETNETQTY = gvrow.Cells[10].Text;
                    objchkf.CHKDETREMARKS = gvrow.Cells[14].Text;
                    objchkf.CHKDETRATE = gvrow.Cells[11].Text;
                    objchkf.CHKDETCOLOR = gvrow.Cells[16].Text;
                   // objchkf.CheckingFormatIssueStock_Update(gvrow.Cells[2].Text, gvrow.Cells[10].Text, objchkf.CHKCPID, gvrow.Cells[13].Text, gvrow.Cells[16].Text);
                    objchkf.ChekingFormatDetails_Save();

                    Masters.ItemPurchase obj = new Masters.ItemPurchase();
                    int hai = int.Parse(gvrow.Cells[8].Text);
                    for (int i = 0; i < hai; i++)
                    {
                        obj.itemcode = gvrow.Cells[2].Text;
                        obj.ItemID = "I" + i + gvrow.Cells[2].Text;
                        obj.companyid = ddlCompanyName.SelectedItem.Value;
                        obj.Barcode = "I" + i + gvrow.Cells[2].Text;
                        obj.COLORID = gvrow.Cells[16].Text;
                        obj.locationid = gvrow.Cells[13].Text;

                        obj.ItemInward_Save();
                    }
                }
            }

            SCM.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //gvCheckForm.DataBind();
            tblCheckDetails.Visible = false;
            SCM.ClearControls(this);
            SCM.Dispose();
        }

    }
    #endregion

    #region CheckingFormat Update
    private void CheckingFormaUpdate()
    {

        try
        {
            SCM.CheckingFormat objchkf = new SCM.CheckingFormat();

            SCM.BeginTransaction();
            objchkf.CHKId = Request.QueryString["ChkId"].ToString();

            objchkf.CHKNo = txtCkNo.Text;
            objchkf.CHKDate = Yantra.Classes.General.toMMDDYYYY(txtCheckingDate.Text);
            objchkf.CHKEquipmentName = txtItemName.Text;
            objchkf.CHKManufacturerName = ddlSupplierName.SelectedItem.Value;
            if (rdbWithoutPo.Checked == true)
            {

                objchkf.CHKPONo = "W/O-PO";
                objchkf.CHKInvoiceNo = txtSupplier.Text;

            }
            if (rdbWithPo.Checked == true)
            {

                objchkf.CHKPONo = txtSlpl.Text;
                objchkf.CHKInvoiceNo = ddlSupInvNo.SelectedItem.Text;
            }
            objchkf.CHKReceivedOn = Yantra.Classes.General.toMMDDYYYY(txtReceivedOn.Text);
            objchkf.CHKModel = ddlItemName.SelectedItem.Value;
            objchkf.CHKSerialNo = txtSerialNo.Text;
            objchkf.CHKPODate = Yantra.Classes.General.toMMDDYYYY(txtPODate.Text);
            objchkf.CHKInvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtSupplierDate.Text);
            objchkf.CHKPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
            if (ddlApprovedBy.SelectedItem.Value == "0")
            {
                objchkf.CHKApprovedBy = "";
            }
            else
            {
                objchkf.CHKApprovedBy = ddlApprovedBy.SelectedItem.Text;
            }
            objchkf.ItemTypeId = "0";
            objchkf.ItemCode = "0";
            objchkf.CHKQty = txtQty.Text;
            objchkf.CHKGatepass = txtGatepass.Text;
            objchkf.CHKLrno = txtlrno.Text;
            objchkf.CHKCPID = ddlCompanyName.SelectedItem.Value;
            if (ddlStoreIncharge.SelectedItem.Value == "0")
            {

                objchkf.CHKSTOREINCHARGE = "";
            }
            else
            {
                objchkf.CHKSTOREINCHARGE = ddlStoreIncharge.SelectedItem.Text;
            }


            if (ddlApayments.SelectedItem.Value == "0")
            {

                objchkf.CHKAUTHORISEDPAYMENTS = "";
            }
            else
            {
                objchkf.CHKAUTHORISEDPAYMENTS = ddlApayments.SelectedItem.Text;
            }

            if (ddlAccounts.SelectedItem.Value == "0")
            {

                objchkf.CHKACCOUNTS = "";
            }
            else
            {
                objchkf.CHKACCOUNTS = ddlAccounts.SelectedItem.Text;
            }

            if (objchkf.CheckingFormat_Update() == "Data Updated Successfully")
            {
                // objchkf.ChekingFormatDetails_Delete(objchkf.CHKId);
                foreach (GridViewRow gvrow in gvItemDetails.Rows)
                {
                    objchkf.CHKDETITEMCODE = gvrow.Cells[2].Text;
                    objchkf.CHKDETGODOWNID = gvrow.Cells[13].Text;
                    objchkf.CHKDETORDEREDQTY = gvrow.Cells[6].Text;
                    objchkf.CHKDETACCEPTEDQTY = gvrow.Cells[8].Text;
                    objchkf.CHKDETRECEIVEDQTY = gvrow.Cells[7].Text;
                    objchkf.CHKDETREJECTEDQTY = gvrow.Cells[9].Text;
                    objchkf.CHKDETNETQTY = gvrow.Cells[10].Text;
                    objchkf.CHKDETREMARKS = gvrow.Cells[14].Text;
                    objchkf.CHKDETRATE = gvrow.Cells[11].Text;
                    objchkf.CHKDETCOLOR = gvrow.Cells[16].Text;
                    objchkf.CheckingFormatIssueStock_Update(gvrow.Cells[2].Text, gvrow.Cells[10].Text, objchkf.CHKCPID, gvrow.Cells[13].Text, gvrow.Cells[16].Text);
                    objchkf.ChekingFormatDetails_Save();

                }
            }




            SCM.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");

        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //gvCheckForm.DataBind();
            tblCheckDetails.Visible = false;
            SCM.ClearControls(this);
            SCM.Dispose();
        }


    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //tblCheckDetails.Visible = true;
        rdbWithPo.Enabled = false;
        rdbWithoutPo.Enabled = false;
        txtSuplierInvoiceNowithoutpo.Visible = false;
        ddlPONO.Visible = false;
        ddlSupInvNo.Visible = false;
        txtSlpl.Visible = true;
        txtSupplier.Visible = true;
        txtwithoutpono.Visible = false;
        if (Request.QueryString["ChkId"] != null)
        {
            try
            {
                SCM.CheckingFormat objchkf = new SCM.CheckingFormat();
                if (objchkf.CheckingFormat_Select(Request.QueryString["ChkId"].ToString()) > 0)
                {
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    tblCheckDetails.Visible = true;
                    txtCkNo.Text = objchkf.CHKNo;
                    txtCheckingDate.Text = objchkf.CHKDate;
                    //ddlItemType.SelectedValue = objchkf.CHKEquipmentName;
                    ddlSupplierName.SelectedValue = objchkf.CHKManufacturerName;
                    txtReceivedOn.Text = objchkf.CHKReceivedOn;
                    //ddlItemName.SelectedValue = objchkf.CHKModel;
                    // txtSerialNo.Text = objchkf.CHKSerialNo;
                    txtSlpl.Text = objchkf.CHKPONo;
                    txtPODate.Text = objchkf.CHKPODate;
                    txtSupplier.Text = objchkf.CHKInvoiceNo;
                    txtSupplierDate.Text = objchkf.CHKInvoiceDate;
                    ddlCompanyName.SelectedValue = objchkf.CHKCPID;
                    //txtRemarks.Text = objchkf.CHKRemarks;
                    ddlPreparedBy.SelectedItem.Text = objchkf.CHKPreparedBy;
                    ddlApprovedBy.SelectedItem.Text = objchkf.CHKApprovedBy;
                    if (objchkf.CHKPONo == "W/O-PO")
                    {
                        rdbWithoutPo.Checked = true;
                        rdbWithPo.Checked = false;
                    }
                    else
                    {
                        rdbWithoutPo.Checked = false;
                        rdbWithPo.Checked = true;
                    }
                    ddlStoreIncharge.SelectedItem.Text = objchkf.CHKSTOREINCHARGE;
                    ddlAccounts.SelectedItem.Text = objchkf.CHKACCOUNTS;
                    ddlApayments.SelectedItem.Text = objchkf.CHKAUTHORISEDPAYMENTS;
                    objchkf.ChekingFormatDetailsscm_Select(Request.QueryString["ChkId"].ToString(), GridView1);
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                SCM.Dispose();
            }
        }
    }
    #endregion


    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ChkId"] != null)
        {
            try
            {
                SCM.CheckingFormat objchkf = new SCM.CheckingFormat();

                MessageBox.Show(this, objchkf.CheckingFormat_Delete(Request.QueryString["ChkId"].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                //gvCheckForm.DataBind();
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




    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {

        tblCheckDetails.Visible = false;
        Response.Redirect("CheckingFormat.aspx");

    }
    #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }
    #endregion
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.CheckingFormat objCHK = new SCM.CheckingFormat(); ;
            SCM.BeginTransaction();
            objCHK.CHKId = Request.QueryString["ChkId"].ToString();
            objCHK.CHKApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objCHK.CheckingFormatApprove_Update();
            // objCHK.CheckingFormatIssueStock_Update(ddlItemName.SelectedValue, txtQty.Text);
            SCM.CommitTransaction();
            MessageBox.Show(this, "Record Approved Successfully");
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvCheckForm.DataBind();
            SCM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    protected void ddlSupInvNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtSupplier.Text = ddlSupInvNo.SelectedItem.Text;
            SCM.PurchaseInvoice objscm = new SCM.PurchaseInvoice();
            if (objscm.PurchaseInvoice_Select(ddlSupInvNo.SelectedItem.Value) > 0)
            {
                txtSupplierDate.Text = objscm.PIDate;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void ddlPONO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtSlpl.Text = ddlPONO.SelectedItem.Text;
            SCM.SupplierFixedPO objscm = new SCM.SupplierFixedPO();
            if (objscm.SuppliersFixedPO_Select(ddlPONO.SelectedItem.Value) > 0)
            {
                txtPODate.Text = objscm.FPODate;
                SCM.PurchaseInvoice.PurchaseInvoice_Select(ddlSupInvNo, ddlPONO.SelectedItem.Value);
                SCM.PurchaseInvoice.pochangeforsupname_Select(ddlSupplierName, ddlPONO.SelectedItem.Value);
                objscm.SuppliersFixedPODetails_Select(ddlPONO.SelectedItem.Value, gvItDetails);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ChkId"] != null)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=checkingformat&chkid=" + Request.QueryString["ChkId"].ToString() + "";
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



    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                //txtColor.Text = objMaster.Color;
                txtManufacturer.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                //txtRate.Text = objMaster.ItemRate;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlItemName.SelectedItem.Value);
            Masters.ItemPurchase obj = new Masters.ItemPurchase();
            if (obj.ItemPrice_Ddl(ddlItemName.SelectedItem.Value) > 0)
            {
                txtRate.Text = obj.rsp;
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



    protected void gvItDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ItemName_Fill();
        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("UOM");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Rate");
        SalesOrderItems.Columns.Add(col);

        col = new DataColumn("DeliveryDate");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Specifications");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        SalesOrderItems.Columns.Add(col);


        if (gvItDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItDetails.Rows)
            {
                DataRow dr = SalesOrderItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[1].Text;
                dr["ItemType"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;
                dr["UOM"] = gvrow.Cells[4].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Rate"] = gvrow.Cells[5].Text;

                dr["DeliveryDate"] = gvrow.Cells[8].Text;
                dr["Specifications"] = gvrow.Cells[9].Text;
                dr["Remarks"] = gvrow.Cells[10].Text;
                dr["ItemTypeId"] = gvrow.Cells[11].Text;



                SalesOrderItems.Rows.Add(dr);
                if (gvrow.RowIndex == gvItDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemName.SelectedValue = gvrow.Cells[1].Text;
                    ddlItemName_SelectedIndexChanged(sender, e);
                    //ItemName_Fill();
                    //txtModelName.Text = gvrow.Cells[3].Text;
                    // ddlItemName_SelectedIndexChanged(sender, e);
                    //txtUOM.Text = gvrow.Cells[4].Text;
                    txtQty.Text = gvrow.Cells[6].Text;
                    txtRate.Text = gvrow.Cells[5].Text;



                    //  txtDeliveryD.Text = gvrow.Cells[8].Text;
                    //txtItemSpecifications.Text = gvrow.Cells[9].Text;
                    //txtItemRemarks.Text = gvrow.Cells[10].Text;
                    gvItDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvItDetails.DataSource = SalesOrderItems;
        gvItDetails.DataBind();
    }
    protected void gvItDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //   e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[5].Text) * Convert.ToDecimal(e.Row.Cells[6].Text));
            //   e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToShortDateString();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text == "")
        {
            txtRemarks.Text = "0";
        }

        
        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                DataRow dr = PurchaseInvoiceProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["OQuantity"] = gvrow.Cells[6].Text;
                dr["RQuantity"] = gvrow.Cells[7].Text;
                dr["AQuantity"] = gvrow.Cells[8].Text;
                dr["ReQuantity"] = gvrow.Cells[9].Text;
                dr["NetQty"] = gvrow.Cells[10].Text;
                dr["Rate"] = gvrow.Cells[11].Text;
                dr["Amount"] = gvrow.Cells[12].Text;
                dr["Godown"] = gvrow.Cells[5].Text;
                dr["Godownid"] = gvrow.Cells[13].Text;
                dr["Remarks"] = gvrow.Cells[14].Text;
                dr["Color"] = gvrow.Cells[15].Text;
                dr["Colorid"] = gvrow.Cells[16].Text;
                PurchaseInvoiceProducts.Rows.Add(dr);
            }
        }

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.Cells[1].Text == ddlItemName.SelectedItem.Value)
                {
                    gvItemDetails.DataSource = PurchaseInvoiceProducts;
                    gvItemDetails.DataBind();
                    MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                    return;
                }

            }
        }

        DataRow drnew = PurchaseInvoiceProducts.NewRow();
        drnew["ItemCode"] = ddlItemName.SelectedItem.Value;
        drnew["ItemType"] = ddlItemName.SelectedItem.Text;
        drnew["ItemName"] = txtItemName.Text;
        drnew["OQuantity"] = txtQty.Text;
        drnew["RQuantity"] = txtRecqty.Text;

        drnew["AQuantity"] = txtAcceptedqty.Text;
        drnew["ReQuantity"] = txtRejectedqty.Text;
        drnew["NetQty"] = txtAcceptedqty.Text;
        drnew["Rate"] = txtRate.Text;
        drnew["Godown"] = ddlgodown.SelectedItem.Text;
        drnew["Godownid"] = ddlgodown.SelectedItem.Value;
        drnew["Remarks"] = txtRemarks.Text;
        drnew["Color"] = ddlcolor.SelectedItem.Text;
        drnew["Colorid"] = ddlcolor.SelectedItem.Value;

        PurchaseInvoiceProducts.Rows.Add(drnew);

        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtItemCategory.Text = string.Empty;
        txtItemName.Text = string.Empty;
        //  txtColor.Text = string.Empty;
        txtManufacturer.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtRate.Text = string.Empty;
        ddlItemName.SelectedItem.Value = "";
        txtSerialNo.Text = string.Empty;
        //txtReceivedOn.Text = string.Empty;
        txtAcceptedqty.Text = string.Empty;
        txtRecqty.Text = string.Empty;
        txtRejectedqty.Text = string.Empty;
        txtAcceptedqty.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtQty.Text = string.Empty;
        //  ddlcolor.SelectedItem.Value = "";

    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[0].Visible = false;
            // e.Row.Cells[1].Visible = false;
            e.Row.Cells[16].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[12].Text = ((Convert.ToDouble(e.Row.Cells[10].Text)) * (Convert.ToDouble(e.Row.Cells[11].Text))).ToString();
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;

        //DataTable PurchaseInvoiceProducts = new DataTable();
        //DataColumn col = new DataColumn();
        //col = new DataColumn("ItemCode");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("ItemType");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("ItemName");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("OQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("RQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("AQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("ReQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("NetQty");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Rate");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Amount");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Godown");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Godownid");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Remarks");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Color");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Colorid");
        //PurchaseInvoiceProducts.Columns.Add(col);

        if (gvItemDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItemDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["OQuantity"] = gvrow.Cells[6].Text;
                    dr["RQuantity"] = gvrow.Cells[7].Text;
                    dr["AQuantity"] = gvrow.Cells[8].Text;
                    dr["ReQuantity"] = gvrow.Cells[9].Text;
                    dr["NetQty"] = gvrow.Cells[10].Text;
                    dr["Rate"] = gvrow.Cells[11].Text;
                    dr["Amount"] = gvrow.Cells[12].Text;
                    dr["Godown"] = gvrow.Cells[5].Text;
                    dr["Godownid"] = gvrow.Cells[13].Text;
                    dr["Remarks"] = gvrow.Cells[14].Text;
                    dr["Color"] = gvrow.Cells[15].Text;
                    dr["Colorid"] = gvrow.Cells[16].Text;
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ItemName_Fill();
        //DataTable PurchaseInvoiceProducts = new DataTable();
        //DataColumn col = new DataColumn();
        //col = new DataColumn("ItemCode");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("ItemType");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("ItemName");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("OQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("RQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("AQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("ReQuantity");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("NetQty");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Rate");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Amount");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Godown");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Godownid");
        //PurchaseInvoiceProducts.Columns.Add(col);
        //col = new DataColumn("Remarks");
        //PurchaseInvoiceProducts.Columns.Add(col);


        foreach (GridViewRow gvrow in gvItemDetails.Rows)
        {

            DataRow dr = PurchaseInvoiceProducts.NewRow();
            dr["ItemCode"] = gvrow.Cells[2].Text;
            dr["ItemType"] = gvrow.Cells[3].Text;
            dr["ItemName"] = gvrow.Cells[4].Text;
            dr["OQuantity"] = gvrow.Cells[6].Text;
            dr["RQuantity"] = gvrow.Cells[7].Text;
            dr["AQuantity"] = gvrow.Cells[8].Text;
            dr["ReQuantity"] = gvrow.Cells[9].Text;
            dr["NetQty"] = gvrow.Cells[10].Text;
            dr["Rate"] = gvrow.Cells[11].Text;
            dr["Amount"] = gvrow.Cells[12].Text;
            dr["Godown"] = gvrow.Cells[5].Text;
            dr["Godownid"] = gvrow.Cells[13].Text;
            dr["Remarks"] = gvrow.Cells[14].Text;

            PurchaseInvoiceProducts.Rows.Add(dr);

            if (gvrow.RowIndex == gvItemDetails.Rows[e.NewEditIndex].RowIndex)
            {
                ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                ddlItemName_SelectedIndexChanged(sender, e);

                txtQty.Text = gvrow.Cells[6].Text;
                txtRecqty.Text = gvrow.Cells[7].Text;
                txtAcceptedqty.Text = gvrow.Cells[8].Text;
                txtRejectedqty.Text = gvrow.Cells[9].Text;
                ddlgodown.SelectedValue = gvrow.Cells[13].Text;
                txtRemarks.Text = gvrow.Cells[14].Text;
                gvItemDetails.SelectedIndex = gvrow.RowIndex;

            }
        }
        gvItemDetails.DataSource = PurchaseInvoiceProducts;
        gvItemDetails.DataBind();

    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster5_Select(ddlItemName, ddlBrand.SelectedItem.Value);
    }
    protected void rdbWithoutPo_CheckedChanged(object sender, EventArgs e)
    {
        txtwithoutpono.Visible = true;
        txtSuplierInvoiceNowithoutpo.Visible = true;
        ddlSupInvNo.Visible = false;
        txtwithoutpono.Text = "W/O-PO";
        txtPODate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlPONO.Visible = false;
        txtSupplierDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


    }
    protected void rdbWithPo_CheckedChanged(object sender, EventArgs e)
    {
        ddlPONO.Visible = true;
        txtwithoutpono.Visible = false;
        ddlSupInvNo.Visible = true;
        txtSuplierInvoiceNowithoutpo.Visible = false;

    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemName.DataSourceID = "SqlDataSource2";
        ddlItemName.DataTextField = "ITEM_MODEL_NO";
        ddlItemName.DataValueField = "ITEM_CODE";
        ddlItemName.DataBind();
        ddlItemName_SelectedIndexChanged(sender, e);
    }

    protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.Stockentry12(ddlgodown, ddlCompanyName.SelectedItem.Value);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = true;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[11].Text = ((Convert.ToDouble(e.Row.Cells[9].Text)) * (Convert.ToDouble(e.Row.Cells[10].Text))).ToString();
        }
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }


    }
    protected void lbtnDelete2_Click(object sender, EventArgs e)
    {

        LinkButton lbtnDelete2;
        lbtnDelete2 = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDelete2.Parent.Parent;
        GridView1.SelectedIndex = gvRow.RowIndex;
        lbtnDelete2.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        SCM.CheckingFormat hai = new SCM.CheckingFormat();
        hai.CheckingFormatDetails_Delete(GridView1.SelectedRow.Cells[16].Text);
        hai.ChekingFormatDetailsscm_Select(GridView1.SelectedRow.Cells[17].Text, GridView1);

    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvCheckForm.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvCheckForm.DataBind();
    }

}
 
