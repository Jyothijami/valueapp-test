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


public partial class Modules_SCM_CheckingFormatDetails : basePage
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            setControlsVisibility();

            GridView1.Columns[0].Visible = false;
            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            
            EmployeeMaster_Fill();
            PurchaseOrder_Fill();
            SuppInvoiceNo_fill();
            Godown_Fill();
            supliername_fill();
            CompanyName_Fill();
            ddlCompanyName.SelectedValue = cp.getPresentCompanySessionValue();
            txtReceivedOn.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            if(Request.QueryString["ChkId"] !=null)
            {
                //Link Btn Code
                rdbWithPo.Enabled = false;
                rdbWithoutPo.Enabled = false;
                txtSuplierInvoiceNowithoutpo.Visible = false;
                tblCheckDetails.Visible = false;
                //ddlPONO.Visible = false;
                //ddlSupInvNo.Visible = false;
                //txtSlpl.Visible = true;
                //txtSupplier.Visible = true;
                //txtwithoutpono.Visible = false;
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
                            txtSlpl.Visible = true;
                            txtSupplier.Visible = true;
                            txtwithoutpono.Visible = false;
                        }
                        else
                        {
                            rdbWithoutPo.Checked = false;
                            rdbWithPo.Checked = true;
                            ddlSupInvNo.Visible = true;
                            ddlPONO.Visible = true;
                            ddlSupInvNo.SelectedItem.Value = objchkf.ChkInvId;
                            ItemType_Fill();

                        }
                        if (objchkf.CHKSerialNo == "1")
                        {
                            rdbChk.Checked = true;
                        }
                        else
                        {
                            rdbChk.Checked = false;
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
                //txtSuplierInvoiceNowithoutpo.Visible = false;
                //ddlPONO.Visible = false;
                //ddlSupInvNo.Visible = false;
                //txtSlpl.Visible = true;
                //txtSupplier.Visible = true;
                //txtwithoutpono.Visible = false;
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
                            ddlSupInvNo.SelectedItem.Value = objchkf.ChkInvId;
                            ddlSupInvNo.SelectedItem.Text = objchkf.CHKInvoiceNo;
                            ddlFPO.SelectedValue  = objchkf.ChkPOId;
                            //ddlFPO.SelectedItem.Text = objchkf.CHKPacking;
                            if (objchkf.CHKPONo == "W/O-PO")
                            {
                                //rdbWithoutPo.Checked = true;
                                //rdbWithPo.Checked = false;
                                rdbWithoutPo.Checked = true;
                                rdbWithPo.Checked = false;
                                ddlSupInvNo.Visible = false;
                                ddlPONO.Visible = false;
                                txtSlpl.Visible = true;
                                txtSupplier.Visible = true;
                                txtwithoutpono.Visible = false;
                            }
                            else
                            {
                                rdbWithoutPo.Checked = false;
                                rdbWithPo.Checked = true;
                                ddlSupInvNo.Visible = true;
                                ddlPONO.Visible = false;
                                lbl4.Visible = false;
                                ddlFPO.Visible = false;
                                txtSlpl.Visible = true;
                                //SCM.PurchaseInvoice.CHKItemsBind(ddlItemName, ddlSupInvNo.SelectedItem.Value);
                                SCM.SupplierFixedPO objscm = new SCM.SupplierFixedPO();
                                objscm.SuppliersFixedPIDetails_Select(ddlSupInvNo.SelectedItem.Value, gvItDetails);
                                ItemType_Fill();

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
               // SCM.ClearControls(this);
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

      private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "65");
       
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        btnSave.Enabled = up.add;
        btnApprove.Enabled = up.Approve;
        //btnRefresh.Enabled = up.Refresh;
        btnPrint.Enabled = up.Print;
        //btnClose.Enabled = up.Close;
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
           // HR.DDLBindWithSelect(ddlStoreIncharge, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 10 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            //HR.DDLBindWithSelect(ddlApprovedBy, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 10 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlAccounts);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlStoreIncharge);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlApayments);
           // HR.DDLBindWithSelect(ddlApayments, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 11 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            //HR.DDLBindWithSelect(ddlAccounts, "SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 3 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
            HR.EmployeeMaster.EmployeeMaster_SelectStores(ddlStoreIncharge);
            HR.EmployeeMaster.EmployeeMaster_SelectStores(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectAccounts(ddlAccounts);
            HR.EmployeeMaster.EmployeeMaster_SelectCMD(ddlApayments);
            

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
    #region Invoice Order Fill
    private void SuppInvoiceNo_fill()
    {
        try
        {
            SCM.SupplierFixedPO.SuppliersPI_Select_MRN(ddlSupInvNo );
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

    #region Purchase Order Fill
    private void PurchaseOrder_Fill()
    {
        try
        {
            SCM.SupplierFixedPO.SuppliersFixedPO_Select_MRN(ddlPONO);
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
           // Masters.ItemMaster.Godown_select(ddlgodown);
            Inventory.Internalindent.Location_Select(ddlgodown);

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
        rdbChk.Checked = false;
        rdbChk.Enabled = true;
    }
    #endregion

    #region Button SAVE/UPDATE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (btnSave.Text == "Save")
        {
            CheckFormatSave();
           // Response.Redirect("CheckingFormat.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            CheckingFormaUpdate();
           // Response.Redirect("CheckingFormat.aspx");
        }
    }
    #endregion

    #region Checking Format Save

    private void CheckFormatSave()
    {
        try
        {
            btnSave.Enabled = false;
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
                objchkf.ItemTypeId = "0";
                objchkf.ChkPOId = "0";
                objchkf.ChkInvId = "0";
            }
            else if (rdbWithPo.Checked == true)
            {

                objchkf.CHKPONo = txtSlpl.Text;
                objchkf.CHKInvoiceNo = ddlSupInvNo.SelectedItem.Text;
                objchkf.CHKInvoiceNo = txtSupplier.Text;
                objchkf.ItemTypeId = lblSupInvId.Text;
                objchkf.ChkInvId = ddlSupInvNo.SelectedItem.Value;
                objchkf.CHKPacking = ddlFPO.SelectedItem.Text;
                objchkf.ChkPOId = ddlFPO.SelectedItem.Value;
            }
            if (rdbChk.Checked == true)
            {
                objchkf.CHKSerialNo = "1";
            }
            else
            {
                objchkf.CHKSerialNo = "0";
            }
            objchkf.CHKReceivedOn = Yantra.Classes.General.toMMDDYYYY(txtReceivedOn.Text);
            objchkf.CHKModel = ddlItemName.SelectedItem.Value;
            //objchkf.CHKSerialNo = txtSerialNo.Text;
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
            //objchkf.ItemTypeId = "0";
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
                string id= objchkf.CHKId ;
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
                 //   objchkf.CheckingFormatIssueStock_Update(gvrow.Cells[2].Text, gvrow.Cells[10].Text, objchkf.CHKCPID, gvrow.Cells[13].Text, gvrow.Cells[16].Text);
                    if (objchkf.ChekingFormatDetails_Save() == "Data Saved Successfully")
                    {
                        string ItemCat = "";
                        string ItemSubCat = "";
                        Masters.ItemMaster objMaster = new Masters.ItemMaster();
                        if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                        {
                            ItemCat = objMaster.ItemCategoryName;
                            ItemSubCat = objMaster.ItemType;
                        }

                        Masters.ItemPurchase obj = new Masters.ItemPurchase();
                        obj.RefNo = objchkf.ChkDetId;
                        obj.ItemCode = gvrow.Cells[2].Text;
                        obj.ItemCategory = ItemCat;
                        obj.ItemSubCategory = ItemSubCat;
                        obj.ColorId = gvrow.Cells[16].Text;
                        obj.qty = gvrow.Cells[8].Text;
                        obj.BalanceQty = gvrow.Cells[8].Text;
                        obj.DamageQty = gvrow.Cells[9].Text;
                        obj.CpId = cp.getPresentCompanySessionValue();
                        obj.InwardType = "MRN";
                        obj.DateAdded = DateTime.Now.ToString();
                        obj.ItemLoc = gvrow.Cells[13].Text;
                        if (((CheckBox)gvrow.FindControl("Chk")).Checked)
                        {
                            obj.Cust_id = gvrow.Cells[17].Text;
                        }
                        else
                        {
                            obj.Cust_id = "0";
                        }
                        obj.DeliveryDate = DateTime.Now.ToString();
                        //obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[19].Text);
                        obj.InwardTemp_Save();

                        if (gvrow.Cells[21].Text != "0")
                        {
                            SCM.PurchaseInvoice objpi = new SCM.PurchaseInvoice();
                            objpi.PIStatus = "Arrived";
                            objpi.PIDetDeliveryDt = Yantra.Classes.General.toMMDDYYYY(txtCheckingDate.Text);
                            objpi.PIDetId = gvrow.Cells[21].Text;
                            objpi.PIStatusUpdate();
                        }
                    }

                    
                }

                if (rdbWithPo.Checked == true)
                {
                    objchkf.CHKPONo = ddlPONO.SelectedItem.Value;
                    objchkf.POStatus_Update();
                }
            }

            SCM.CommitTransaction();
            //foreach (GridViewRow gvrow in gvItemDetails.Rows)
            //{
                //string ItemCat = "";
                //string ItemSubCat = "";
                //Masters.ItemMaster objMaster = new Masters.ItemMaster();
                //if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                //{
                //    ItemCat = objMaster.ItemCategoryName;
                //    ItemSubCat = objMaster.ItemType;
                //}

                //Masters.ItemPurchase obj = new Masters.ItemPurchase();
                //obj.RefNo = objchkf.ChkDetId;
                //obj.ItemCode = gvrow.Cells[2].Text;
                //obj.ItemCategory = ItemCat;
                //obj.ItemSubCategory = ItemSubCat;
                //obj.ColorId = gvrow.Cells[16].Text;
                //obj.qty = gvrow.Cells[8].Text;
                //obj.BalanceQty = gvrow.Cells[8].Text;
                //obj.DamageQty = gvrow.Cells[9].Text;
                //obj.CpId = cp.getPresentCompanySessionValue();
                //obj.InwardType = "MRN";
                //obj.DateAdded = DateTime.Now.ToString();
                //obj.ItemLoc = gvrow.Cells[13].Text;
                //if (((CheckBox)gvrow.FindControl("Chk")).Checked)
                //{
                //    obj.Cust_id = gvrow.Cells[17].Text;
                //}
                //else
                //{
                //    obj.Cust_id = "0";
                //}
                //obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[19].Text);
                //obj.InwardTemp_Save();
           // }
          //  MessageBox.Show(this, "Data Saved Successfully");
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //gvCheckForm.DataBind();
            btnSave.Enabled = true;

            tblCheckDetails.Visible = false;
            SCM.ClearControls(this);
            SCM.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
  "alert(' MRN Saved sucessfully');window.location ='CheckingFormat.aspx';", true);
        }

    }
    #endregion

    #region CheckingFormat Update
    private void CheckingFormaUpdate()
    {

        try
        {
            btnSave.Enabled = false;

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
                objchkf.ItemTypeId = "0";

            }
            if (rdbWithPo.Checked == true)
            {

                objchkf.CHKPONo = txtSlpl.Text;
                objchkf.CHKInvoiceNo = ddlSupInvNo.SelectedItem.Text;
                //objchkf.CHKInvoiceNo = txtSupplier.Text;
                objchkf.ChkInvId = ddlSupInvNo.SelectedItem.Value;
                //objchkf.ChkPOId = ddlFPO.SelectedItem.Value;
                objchkf.ItemTypeId = lblSupInvId.Text;
                //objchkf.CHKPacking = ddlFPO.SelectedItem.Text;
            }
            objchkf.CHKReceivedOn = Yantra.Classes.General.toMMDDYYYY(txtReceivedOn.Text);
            objchkf.CHKModel = ddlItemName.SelectedItem.Value;
            //objchkf.CHKSerialNo = txtSerialNo.Text;
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
            if (rdbChk.Checked == true)
            {
                objchkf.CHKSerialNo = "1";
            }
            else
            {
                objchkf.CHKSerialNo = "0";
            }
            //objchkf.ItemTypeId = lblSupInvId.Text;
            
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
                string id = objchkf.CHKId;
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
                   // objchkf.CheckingFormatIssueStock_Update(gvrow.Cells[2].Text, gvrow.Cells[10].Text, objchkf.CHKCPID, gvrow.Cells[13].Text, gvrow.Cells[16].Text);
                    objchkf.ChekingFormatDetails_Save();

                    string ItemCat = "";
                    string ItemSubCat = "";
                    Masters.ItemMaster objMaster = new Masters.ItemMaster();
                    if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                    {
                        ItemCat = objMaster.ItemCategoryName;
                        ItemSubCat = objMaster.ItemType;
                    }

                    Masters.ItemPurchase obj = new Masters.ItemPurchase();
                    obj.RefNo = objchkf.ChkDetId;
                    obj.ItemCode = gvrow.Cells[2].Text;
                    obj.ItemCategory = ItemCat;
                    obj.ItemSubCategory = ItemSubCat;
                    obj.ColorId = gvrow.Cells[16].Text;
                    obj.qty = gvrow.Cells[8].Text;
                    obj.BalanceQty = gvrow.Cells[8].Text;
                    obj.DamageQty = gvrow.Cells[9].Text;
                    obj.CpId = cp.getPresentCompanySessionValue();
                    obj.InwardType = "MRN";
                    obj.DateAdded = DateTime.Now.ToString();
                    obj.ItemLoc = gvrow.Cells[13].Text;
                    if (((CheckBox)gvrow.FindControl("Chk")).Checked)
                    {
                        obj.Cust_id = gvrow.Cells[17].Text;
                    }
                    else
                    {
                        obj.Cust_id = "0";
                    }
                    obj.DeliveryDate = DateTime.Now.ToString();
                    obj.InwardTemp_Save();

                    if (gvrow.Cells[21].Text != "0")
                    {
                        SCM.PurchaseInvoice objpi = new SCM.PurchaseInvoice();
                        objpi.PIStatus = "Arrived";
                        objpi.PIDetDeliveryDt = Yantra.Classes.General.toMMDDYYYY(txtCheckingDate.Text);
                        objpi.PIDetId = gvrow.Cells[21].Text;
                        objpi.PIStatusUpdate();
                    }

                    //Masters.ItemPurchase obj = new Masters.ItemPurchase();
                    //int hai = int.Parse(gvrow.Cells[7].Text);
                    //for (int i = 0; i < hai; i++)
                    //{
                    //    obj.itemcode = gvrow.Cells[2].Text;
                    //    obj.ItemID = "I" + i + gvrow.Cells[2].Text;
                    //    obj.companyid = ddlCompanyName.SelectedItem.Value;
                    //    obj.Barcode = "I" + i + gvrow.Cells[2].Text;
                    //    //obj.MRNID = objchkf.CHKID;
                    //    obj.MRNID = id;
                    //    obj.COLORID = gvrow.Cells[16].Text;
                    //    obj.ItemInward_Save();
                    //}
                    

                }
            }




            SCM.CommitTransaction();
            //foreach (GridViewRow gvrow in gvItemDetails.Rows)
            //{
                //string ItemCat = "";
                //string ItemSubCat = "";
                //Masters.ItemMaster objMaster = new Masters.ItemMaster();
                //if (objMaster.ItemMaster_Select(gvrow.Cells[2].Text) > 0)
                //{
                //    ItemCat = objMaster.ItemCategoryName;
                //    ItemSubCat = objMaster.ItemType;
                //}

                //Masters.ItemPurchase obj = new Masters.ItemPurchase();
                //obj.RefNo = objchkf.ChkDetId;
                //obj.ItemCode = gvrow.Cells[2].Text;
                //obj.ItemCategory = ItemCat;
                //obj.ItemSubCategory = ItemSubCat;
                //obj.ColorId = gvrow.Cells[16].Text;
                //obj.qty = gvrow.Cells[8].Text;
                //obj.BalanceQty = gvrow.Cells[8].Text;
                //obj.DamageQty = gvrow.Cells[9].Text;
                //obj.CpId = cp.getPresentCompanySessionValue();
                //obj.InwardType = "MRN";
                //obj.DateAdded = DateTime.Now.ToString();
                //obj.ItemLoc = gvrow.Cells[13].Text;
                //if (((CheckBox)gvrow.FindControl("Chk")).Checked)
                //{
                //    obj.Cust_id = gvrow.Cells[17].Text;
                //}
                //else
                //{
                //    obj.Cust_id = "0";
                //}
                //obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[19].Text);
                //obj.InwardTemp_Save();
          //  }
            MessageBox.Show(this, "Data Updated Successfully");

        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnSave.Enabled = true;

            //gvCheckForm.DataBind();
            tblCheckDetails.Visible = false;
            SCM.ClearControls(this);
            SCM.Dispose();
            SM.ClearControls(this);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
    "alert(' MRN Updated sucessfully');window.location ='CheckingFormat.aspx';", true);
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
                    if (objchkf.CHKSerialNo == "1")
                    {
                        rdbChk.Checked = true;
                    }
                    else { rdbChk.Checked = false; }
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
                if (GridView1.Rows.Count > 0)
                {
                    foreach (GridViewRow gvr in GridView1.Rows)
                    {
                        SCM.CheckingFormat hai = new SCM.CheckingFormat();
                        hai.TempCheckingFormatDetails_Delete(gvr.Cells[16].Text, gvr.Cells[1].Text, gvr.Cells[15].Text);
                    }
                }
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
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
 "alert(' MRN Deleted sucessfully');window.location ='CheckingFormat.aspx';", true);
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
            SCM.CheckingFormat objCHK = new SCM.CheckingFormat();
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
            lblSupInvId.Text = ddlSupInvNo.SelectedItem.Value;
            SCM.SupplierFixedPO objscm = new SCM.SupplierFixedPO();
            if (objscm.SuppliersPI_Select(ddlSupInvNo.SelectedItem.Value) > 0)
            {
                txtSupplierDate.Text = objscm.PIDate;
                txtPODate.Text = objscm.FPODate;
                SCM.PurchaseInvoice.PurchaseInvoice_Select(ddlPONO, ddlSupInvNo.SelectedItem.Value);
                SCM.PurchaseInvoice.PIforsupname_Select(ddlSupplierName, ddlSupInvNo.SelectedItem.Value);
                SCM.PurchaseInvoice.FPO_Select(ddlFPO, ddlSupInvNo.SelectedItem.Value);
                objscm.SuppliersFixedPIDetails_Select(ddlSupInvNo.SelectedItem.Value, gvItDetails);
            }
            if (rdbWithPo.Checked == true)
            {
                ItemType_Fill();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }

    private void ItemType_Fill()
    {
        try
        {
            SM.SalesOrder.POItems_Select(ddlSupInvNo.SelectedItem.Value, ddlItemName);

            //SM.SalesOrder.POItems_Select1(ddlSupInvNo.SelectedItem.Value, ddlItemName);
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
   
    protected void ddlPONO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtSlpl.Text = ddlPONO.SelectedItem.Text;
            SCM.SupplierFixedPO objscm = new SCM.SupplierFixedPO();
            if (objscm.SuppliersFixedPO_Select(ddlPONO.SelectedItem.Value) > 0)
            {
                //txtPODate.Text = objscm.FPODate;
                //SCM.PurchaseInvoice.PurchaseInvoice_Select(ddlSupInvNo, ddlPONO.SelectedItem.Value);
                SCM.PurchaseInvoice.PIforsupname_Select(ddlSupplierName, ddlSupInvNo.SelectedItem.Value);

                //SCM.PurchaseInvoice.pochangeforsupname_Select(ddlSupplierName, ddlPONO.SelectedItem.Value);
                //objscm.SuppliersFixedPODetails_Select(ddlPONO.SelectedItem.Value, gvItDetails);
            }
            if (rdbWithPo.Checked == true)
            {
                ItemType_Fill();
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
                //string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=MRN&chkid=" + Request.QueryString["ChkId"].ToString() + "";
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
            //if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                //txtColor.Text = objMaster.Color;
                txtManufacturer.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                txtUom.Text = objMaster.ItemUOMShort;
                //txtRate.Text = objMaster.ItemRate;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlcolor, ddlItemName.SelectedItem.Value);
            Masters.ItemPurchase obj = new Masters.ItemPurchase();
            if (obj.ItemPrice_PIDdl(ddlSupInvNo.SelectedItem.Value, ddlItemName.SelectedItem.Value) > 0)
            {
                txtRate.Text = obj.rsp;
                txtQty.Text = obj.qty;
                txtRecqty.Text = obj.qty;
                txtAcceptedqty.Text = obj.qty;
                txtRejectedqty.Text = "0";
            }
            SCM.SupplierFixedPO objscm = new SCM.SupplierFixedPO();
            if (objscm.POCustomer_Select(ddlSupInvNo.SelectedItem.Value, ddlItemName.SelectedItem.Value) > 0)
            {
                lblCustId.Text = objscm.CustId;
                txtCustomer.Text = objscm.CustName;
                txtRemarks.Text = objscm.CustName;
            }
            else
            {
                lblCustId.Text = "0";
                txtCustomer.Text = "Self";
            }
            if (objscm.PODeliveryDate_Select(ddlPONO.SelectedItem.Value, ddlItemName.SelectedItem.Value) > 0)
            {
                lblDeliveryDate.Text = objscm.DeliveryDate;
            }
            else
            {
                lblDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
           // Godown_Fill();



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
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[5].Text) * Convert.ToDecimal(e.Row.Cells[6].Text));
            //   e.Row.Cells[8].Text = Convert.ToDateTime(e.Row.Cells[8].Text).ToShortDateString();
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvItDetails.Rows)
        {
             CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {
                DataTable PurchaseInvoiceProducts = new DataTable();
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
                col = new DataColumn("CustId");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Customer");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("FPO_DET_ID");
                PurchaseInvoiceProducts.Columns.Add(col);
                if (ddlgodown.SelectedIndex !=0)
                {
                    if (gvItemDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvrow1 in gvItemDetails.Rows)
                        {
                            if (gvItemDetails.SelectedIndex > -1)
                            {
                                if (gvrow.RowIndex == gvItemDetails.SelectedRow.RowIndex)
                                {
                                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                                    dr["ItemCode"] = gvrow.Cells[1].Text;
                                    dr["ItemType"] = gvrow.Cells[2].Text;
                                    dr["ItemName"] = gvrow.Cells[3].Text;
                                    dr["OQuantity"] = gvrow.Cells[6].Text;
                                    dr["RQuantity"] = gvrow.Cells[6].Text;
                                    dr["AQuantity"] = gvrow.Cells[6].Text;
                                    dr["ReQuantity"] = "0";
                                    dr["NetQty"] = gvrow.Cells[6].Text;
                                    dr["Rate"] = gvrow.Cells[5].Text;
                                    dr["Amount"] = gvrow.Cells[7].Text;
                                    dr["Godown"] = ddlgodown.SelectedItem.Text;
                                    dr["Godownid"] = ddlgodown.SelectedItem.Value;
                                    dr["Remarks"] = gvrow.Cells[10].Text;
                                    dr["Color"] = gvrow.Cells[12].Text;
                                    dr["Colorid"] = gvrow.Cells[13].Text;
                                    dr["CustId"] = gvrow.Cells[14].Text;
                                    dr["Customer"] = gvrow.Cells[15].Text;
                                    dr["DeliveryDate"] = gvrow.Cells[16].Text;
                                    dr["FPO_DET_ID"] = gvrow.Cells[18].Text;
                                    PurchaseInvoiceProducts.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                                    dr["ItemCode"] = gvrow1.Cells[2].Text;
                                    dr["ItemType"] = gvrow1.Cells[3].Text;
                                    dr["ItemName"] = gvrow1.Cells[4].Text;
                                    dr["OQuantity"] = gvrow1.Cells[6].Text;
                                    dr["RQuantity"] = gvrow1.Cells[7].Text;
                                    dr["AQuantity"] = gvrow1.Cells[8].Text;
                                    dr["ReQuantity"] = gvrow1.Cells[9].Text;
                                    dr["NetQty"] = gvrow1.Cells[10].Text;
                                    dr["Rate"] = gvrow1.Cells[11].Text;
                                    dr["Amount"] = gvrow1.Cells[12].Text;
                                    dr["Godown"] = gvrow1.Cells[5].Text;
                                    dr["Godownid"] = gvrow1.Cells[13].Text;
                                    dr["Remarks"] = gvrow1.Cells[14].Text;
                                    dr["Color"] = gvrow1.Cells[15].Text;
                                    dr["Colorid"] = gvrow1.Cells[16].Text;
                                    dr["CustId"] = gvrow1.Cells[17].Text;
                                    dr["Customer"] = gvrow1.Cells[18].Text;
                                    dr["DeliveryDate"] = gvrow1.Cells[19].Text;
                                    dr["FPO_DET_ID"] = gvrow1.Cells[21].Text;

                                    PurchaseInvoiceProducts.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = PurchaseInvoiceProducts.NewRow();
                                dr["ItemCode"] = gvrow1.Cells[2].Text;
                                dr["ItemType"] = gvrow1.Cells[3].Text;
                                dr["ItemName"] = gvrow1.Cells[4].Text;
                                dr["OQuantity"] = gvrow1.Cells[6].Text;
                                dr["RQuantity"] = gvrow1.Cells[7].Text;
                                dr["AQuantity"] = gvrow1.Cells[8].Text;
                                dr["ReQuantity"] = gvrow1.Cells[9].Text;
                                dr["NetQty"] = gvrow1.Cells[10].Text;
                                dr["Rate"] = gvrow1.Cells[11].Text;
                                dr["Amount"] = gvrow1.Cells[12].Text;
                                dr["Godown"] = gvrow1.Cells[5].Text;
                                dr["Godownid"] = gvrow1.Cells[13].Text;
                                dr["Remarks"] = gvrow1.Cells[14].Text;
                                dr["Color"] = gvrow1.Cells[15].Text;
                                dr["Colorid"] = gvrow1.Cells[16].Text;
                                dr["CustId"] = gvrow1.Cells[17].Text;
                                dr["Customer"] = gvrow1.Cells[18].Text;
                                dr["DeliveryDate"] = gvrow1.Cells[19].Text;
                                dr["FPO_DET_ID"] = gvrow1.Cells[21].Text;

                                PurchaseInvoiceProducts.Rows.Add(dr);
                            }
                        }
                    }
                    if (gvItemDetails.SelectedIndex == -1)
                    {
                        DataRow drnew = PurchaseInvoiceProducts.NewRow();
                        drnew["ItemCode"] = gvrow.Cells[1].Text;
                        drnew["ItemType"] = gvrow.Cells[2].Text;
                        drnew["ItemName"] = gvrow.Cells[3].Text;
                        drnew["OQuantity"] = gvrow.Cells[6].Text;
                        drnew["RQuantity"] = gvrow.Cells[6].Text;
                        drnew["AQuantity"] = gvrow.Cells[6].Text;
                        drnew["ReQuantity"] = "0";
                        drnew["NetQty"] = gvrow.Cells[6].Text;
                        drnew["Rate"] = gvrow.Cells[5].Text;
                        drnew["Amount"] = gvrow.Cells[7].Text;
                        drnew["Godown"] = ddlgodown.SelectedItem.Text;
                        drnew["Godownid"] = ddlgodown.SelectedItem.Value;
                        drnew["Remarks"] = gvrow.Cells[10].Text;
                        drnew["Color"] = gvrow.Cells[12].Text;
                        drnew["Colorid"] = gvrow.Cells[13].Text;
                        drnew["CustId"] = gvrow.Cells[14].Text;
                        drnew["Customer"] = gvrow.Cells[15].Text;
                        drnew["DeliveryDate"] = gvrow.Cells[16].Text;
                        drnew["FPO_DET_ID"] = gvrow.Cells[18].Text;

                        PurchaseInvoiceProducts.Rows.Add(drnew);
                    }
                    gvItemDetails.DataSource = PurchaseInvoiceProducts;
                    gvItemDetails.DataBind();
                    gvItemDetails.SelectedIndex = -1;
                    btnItemRefresh_Click(sender, e);
                    ch.Checked = false;
                }
                else
                {
                    MessageBox.Show(this, "Please Select Warehouse Location");
                }

                
                
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text == "")
        {
            txtRemarks.Text = "0";
        }

        DataTable PurchaseInvoiceProducts = new DataTable();
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
        col = new DataColumn("CustId");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Customer");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("FPO_DET_ID");
        PurchaseInvoiceProducts.Columns.Add(col);

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
                dr["CustId"] = gvrow.Cells[17].Text;
                dr["Customer"] = gvrow.Cells[18].Text;
                dr["DeliveryDate"] = gvrow.Cells[19].Text;
                dr["FPO_DET_ID"] = gvrow.Cells[21].Text;
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
        drnew["CustId"] = lblCustId.Text;
        drnew["Customer"] = txtCustomer.Text;
        drnew["DeliveryDate"] = lblDeliveryDate.Text;
        drnew["FPO_DET_ID"] = 0;
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
        txtCustomer.Text = string.Empty;
        lblCustId.Text = string.Empty;
        lblDeliveryDate.Text = string.Empty;
        //  ddlcolor.SelectedItem.Value = "";

    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[16].Visible = false;
           e.Row.Cells[17].Visible = false;
            e.Row.Cells[0].Visible = false;
             e.Row.Cells[19].Visible = false;
            //e.Row.Cells[16].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[12].Text = ((Convert.ToDouble(e.Row.Cells[10].Text)) * (Convert.ToDouble(e.Row.Cells[11].Text))).ToString();
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItemDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable PurchaseInvoiceProducts = new DataTable();
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
        col = new DataColumn("CustId");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("Customer");
        PurchaseInvoiceProducts.Columns.Add(col);
        col = new DataColumn("DeliveryDate");
        PurchaseInvoiceProducts.Columns.Add(col);
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
                    dr["CustId"] = gvrow.Cells[17].Text;
                    dr["Customer"] = gvrow.Cells[18].Text;
                    dr["DeliveryDate"] = gvrow.Cells[19].Text;
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
        DataTable PurchaseInvoiceProducts = new DataTable();
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
        if (rdbWithoutPo.Checked == true)
        {
            Masters.ItemMaster.ItemMaster5_Select(ddlItemName, ddlBrand.SelectedItem.Value);
        }
        else if(rdbWithPo.Checked==true)
        {
            Masters.ItemMaster.ItemMaster8_Select(ddlItemName, ddlBrand.SelectedItem.Value,ddlSupInvNo.SelectedItem.Value);
        }
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
      //  Masters.ItemMaster.Stockentry12(ddlgodown, ddlCompanyName.SelectedItem.Value);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Visible = true;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[11].Text = ((Convert.ToDouble(e.Row.Cells[9].Text)) * (Convert.ToDouble(e.Row.Cells[10].Text))).ToString();
        }
        //if (btnSave.Enabled == false)
        //{
        //    if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Cells[0].Visible = true;
        //    }
        //}


    }
    protected void lbtnDelete2_Click(object sender, EventArgs e)
    {

        LinkButton lbtnDelete2;
        lbtnDelete2 = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDelete2.Parent.Parent;
        GridView1.SelectedIndex = gvRow.RowIndex;
        lbtnDelete2.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        SCM.CheckingFormat hai = new SCM.CheckingFormat();

        hai.TempCheckingFormatDetails_Delete(GridView1.SelectedRow.Cells[16].Text, GridView1.SelectedRow.Cells[1].Text, GridView1.SelectedRow.Cells[15].Text);

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
    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lbtnDelete2_Click1(object sender, EventArgs e)
    {
        LinkButton lbtnDelete2;
        lbtnDelete2 = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDelete2.Parent.Parent;
        GridView1.SelectedIndex = gvRow.RowIndex;
        lbtnDelete2.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        SCM.CheckingFormat hai = new SCM.CheckingFormat();
        hai.TempCheckingFormatDetails_Delete(GridView1.SelectedRow.Cells[16].Text, GridView1.SelectedRow.Cells[1].Text, GridView1.SelectedRow.Cells[15].Text);
        hai.CheckingFormatDetails_Delete(GridView1.SelectedRow.Cells[16].Text);
        hai.ChekingFormatDetailsscm_Select(GridView1.SelectedRow.Cells[17].Text, GridView1);
    }
    #region Button DELETE Click
 //   protected void btnDelete_Click(object sender, EventArgs e)
 //   {
       
 //           try
 //           {
 //               foreach (GridViewRow gvr in GridView1.Rows)
 //               {
 //                   SCM.CheckingFormat hai = new SCM.CheckingFormat();
 //                   hai.TempCheckingFormatDetails_Delete(gvr.Cells[16].Text, gvr.Cells[1].Text, gvr.Cells[15].Text);
 //               }
 //               SCM.CheckingFormat objchkf = new SCM.CheckingFormat();
 //               MessageBox.Show(this, objchkf.CheckingFormat_Delete(Request.QueryString["ChkId"]));
 //           }
 //           catch (Exception ex)
 //           {
 //               MessageBox.Show(this, ex.Message);
 //           }
 //           finally
 //           {

 //              // gvCheckForm.DataBind();
 //               SCM.ClearControls(this);
 //               SCM.Dispose();
 //               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
 //"alert(' MRN Deleted sucessfully');window.location ='CheckingFormat.aspx';", true);
 //           }
       
 //   }
    #endregion
  
}

 
