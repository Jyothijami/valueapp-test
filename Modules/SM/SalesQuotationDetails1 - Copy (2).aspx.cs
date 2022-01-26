using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using System.Data;
using vllib;
using System.IO;
using Yantra.MessageBox;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using Yantra.Classes;
using System.Drawing;
using System.Collections.Generic;
using iTextSharp.text.html;
using System.Text;

public partial class Modules_SM_SalesQuotationDetails1 : basePage
{
    ScriptManager ScriptManagerLocal;
    decimal TotalAmount = 0;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            setControlsVisibility();
            lblQuotIdHiddenForFollowUp.Text = Request.QueryString["QuoId"];
            txtspldiscount.Text = "0";
            txtspldiscount.Attributes.Add("onkeyup", "javascript:summarycalc();");
            lblFOB.Visible = lblCIF.Visible = txtFOB.Visible = txtCIF.Visible = false;
            Yantra.Authentication.Privilege_Check(this);
            txtQunatity.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtRate.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtDiscount.Attributes.Add("onkeyup", "javascript:amtcalc();");

            txtGST_Perc.Attributes.Add("onkeyup", "javascript:Gst_amtcalc();");
            txtGST_Amt.Attributes.Add("onkeyup", "javascript:Gst_Disc_calc();");

            txtSpPrice.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");

            txtOptQty.Attributes.Add("onkeyup", "javascript:Optamtcalc();");
            txtOptRate.Attributes.Add("onkeyup", "javascript:Optamtcalc();");
            txtOptDisc.Attributes.Add("onkeyup", "javascript:Optamtcalc();");
            txtOptSpPrice.Attributes.Add("onkeyup", "javascript:OptamtcalcDisc();");

            rbVAT.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            rbCST.Attributes.Add("onclick", "javascript:rbVATCSTEnableDisable();");
            btnRegret.Attributes.Add("onclick", "return confirm('Are you sure you want to Absolute this Quotation !');");
            //  btnForApproveHidden.Style.Add("display", "none");
            EnquiryMaster_Fill();
            DeliveryType_Fill();
            CurrencyType_Fill();
            EmployeeMaster_Fill();
            CustomerMaster_Fill();
            CompanyName_Fill();
            RateType_Fill();
            ddlRate.SelectedValue = "1";

            if (Request.QueryString["enqid"] != null)
            {
                if (SM.SalesQuotation.IsSalesQuotationRaised(Request.QueryString["enqid"].ToString()) > 0)
                {
                    MessageBox.Show(this, "Quotation has already prepared for this Sales Lead");
                }
                btnNew_Click(sender, e);
                ddlEnquiryNo.SelectedValue = Request.QueryString["enqid"].ToString();
                ddlEnquiryNo_SelectedIndexChanged(sender, e);
                tblQuotationDetails.Visible = true;

            }

            if (Request.QueryString["lbtn"] == "lbtn")
            {

                btnEdit_Click(sender, e);
            }

            if (Request.QueryString["New"] == "New")
            {
                btnNew_Click(sender, e);
            }

            gridbind();

        }
    }



    private void gridbind()
    {

        lblSearchItemHidden.Text = "0";
        lblSearchTypeHidden.Text = "0";
        lblSearchValueFromHidden.Text = "0";
        lblSearchValueHidden.Text = "0";

        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        lblUserId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
        lblCPID.Text = cp.getPresentCompanySessionValue();
        //gvQuotationDetails.DataBind();


    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "8");
        btnSave.Enabled = up.add;
        btnApprove.Enabled = up.Approve;
        btnPrint.Enabled = up.Print;
        btnPrintTB.Enabled = up.Print;
        //btnSend.Enabled = up.Email;
        //btnPrintCB.Enabled = up.Print;
        //btnPrintTB.Enabled = up.Print;
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "9");
        btnSalesOrder.Enabled = up1.add;


    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString["QuoNo"] != null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["AppBy"].ToString()) && Request.QueryString["AppBy"].ToString() != "&nbsp;")
            {
                btnApprove.Visible = false;
                btnRegret.Visible = false;
                btnSave.Visible = false;
                btnEdit.Visible = false;
                btnRefresh.Visible = false;
                btnDelete.Visible = false;

                if (Request.QueryString["Status"].ToString() == "Revised" || Request.QueryString["Status"].ToString() == "Closed")
                {
                    btnRevise.Visible = false;
                    btnSalesOrder.Visible = false;
                    btnSend.Visible = false;
                    btnFollowUp.Visible = false;
                }
                if (Request.QueryString["Status"].ToString() == "Absolute")
                {
                    btnApprove.Visible = false;
                    btnRegret.Visible = false;
                    btnSave.Visible = false;
                    btnEdit.Visible = false;
                    btnRefresh.Visible = false;
                    btnSalesOrder.Visible = false;
                }
                else
                {
                    btnRevise.Visible = true;
                    btnSalesOrder.Visible = true;
                    btnSend.Visible = true;
                    btnFollowUp.Visible = true;
                }

            }
            else
            {
                btnApprove.Visible = true;
                btnRegret.Visible = true;
                btnSave.Visible = true;
                btnEdit.Visible = false;
                btnRefresh.Visible = false;
                btnDelete.Visible = false;
                btnSend.Visible = false;
                btnRevise.Visible = true;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnEdit.Visible = false;
            btnRefresh.Visible = true;
            btnApprove.Visible = false;
            btnRegret.Visible = false;
            btnSend.Visible = false;
            btnRevise.Visible = true;
            btnDelete.Visible = false;
        }

        if (tdEMDHeader.Visible == true)
        {
            btnPrint.Visible = true;
            btnPrintTB.Visible = false;
            btnPrintCB.Visible = true;
        }
        else
        {
            btnPrint.Visible = true;
            btnPrintTB.Visible = false;
            btnPrintCB.Visible = true;
        }
    }
    #endregion

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);
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

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName2(ddlCustomer);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //SM.Dispose();
        }
    }
    #endregion

    #region Enquiry Master Fill
    private void EnquiryMaster_Fill()
    {
        try
        {
            SM.SalesEnquiry.SalesEnquiry_Select(ddlEnquiryNo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //SM.Dispose();
        }
    }
    #endregion

    #region Rate Type Fill
    private void RateType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlRate);
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

    #region Delivery Type Fill
    private void DeliveryType_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDespatchMode);
            ddlDespatchMode.SelectedIndex = 1;
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

    #region Currency Type Fill
    private void CurrencyType_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlCurrencyType);
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

    //#region Item Types Fill
    //private void ItemTypes_Fill()
    //{
    //    try
    //    {
    //        //Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
    //        SM.SalesEnquiry.SalesEnquiryItemTypes1_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        //Masters.Dispose();            
    //        //SM.Dispose();
    //    }
    //}
    //#endregion

    // #region Item TypesAll Fill
    //private void ItemTypesAll_Fill()
    //{
    //    try
    //    {
    //        Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
    //       // SM.SalesEnquiry.SalesEnquiryItemTypes1_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        //Masters.Dispose();            
    //        SM.Dispose();
    //    }
    //}
    //#endregion

    #region  Types Fill
    private void Types_Fill()
    {
        try
        {
            //Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
            SM.SalesEnquiry.SalesEnquiryItemTypes2_Select(ddlModelNo.SelectedItem.Value, ddlEssentials);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();            
            //  SM.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            //HR.EmployeeMaster.EmployeeMasterStatus_Select(ddlResponsiblePerson);
            //HR.EmployeeMaster.EmployeeMasterStatus_Select(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlResponsiblePerson);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlCheckedBy);
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

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {

        btnFollowUp.Visible = btnEdit.Visible = btnSalesOrder.Visible = false;
        //gvQuotationDetails.SelectedIndex = -1;
        // SM.ClearControls(this);
        gvQuotationItems.DataBind();
        // gvEnquiryProducts.DataBind();
        txtQuotationNo.Text = SM.SalesQuotation.SalesQuotation_AutoGenCode();
        txtQuotationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        tblQuotationDetails.Visible = true;
        txtorgRemarks.Text = "-";
        txtFloor.Text = "-";
        //rdbOnlyfromLead.Checked = true;
        //rdbOnlyfromLead_CheckedChanged(sender,e);
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;

        if (txtFOB.Text == "") { txtFOB.Text = "0"; }
        if (txtCIF.Text == "") { txtCIF.Text = "0"; }

        if (btnSave.Text == "Save")
        {
            SalesQuotationSave();
            //Response.Redirect("SalesQuotation.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            SalesQuotationUpdate();
            // Response.Redirect("SalesQuotation.aspx");
        }
    }
    #endregion

    #region SalesQuotationSave
    private void SalesQuotationSave()
    {
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                btnSave.Enabled = false;
                SM.SalesQuotation objSM = new SM.SalesQuotation();
                SM.BeginTransaction();
                objSM.QuotNo = txtQuotationNo.Text;
                objSM.QuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objSM.EnqId = ddlEnquiryNo.SelectedItem.Value;
                objSM.QuotDelivery = txtDelivery.Text;
                objSM.QuotPayTerms = txtPaymentTerms.Text;
                objSM.QuotPackCharges = txtPackingCharges.Text;
                objSM.QuotExcise = txtExciseDuty.Text;

                if (rbVAT.Checked == true)
                { objSM.QuotVAT = txtVAT.Text; }
                else if (rbCST.Checked == true)
                { objSM.QuotCST = txtVAT.Text; }
                else if (rbincluding.Checked == true)
                { objSM.QuotIncluding = txtVAT.Text; }

                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.QuotGuarantee = txtGuarantee.Text;
                objSM.QuotTransCharges = txtTransCharges.Text;
                objSM.QuotInsurance = txtInsurance.Text;
                objSM.QuotErrec = txtErrection.Text;
                objSM.QuotJurisdiction = txtJurisdiction.Text;
                objSM.QuotValidity = txtValidity.Text;
                objSM.QuotInspection = txtInspection.Text;
                objSM.QuotOtherSpecs = txtOtherSpecs.Text;
                //   objSM.QuotPOLog = txtpo.Text;
                objSM.QuotRespId = ddlResponsiblePerson.SelectedItem.Value;
                objSM.QuotSalespId = ddlSalesPerson.SelectedItem.Value;
                objSM.QuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.QuotCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.QuotApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.CurrencyId = ddlCurrencyType.SelectedItem.Value;
                //objSM.QuotDDNo = txtDDNo.Text;
                objSM.QuotDDNo = "4";
                objSM.QuotDDDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
                //objSM.QuotBankName = txtBankName.Text;
                objSM.QuotBankName = "5";
                objSM.IsExpectedOrder = chkIsExpectedOrder.Checked;
                objSM.QuotTotalEMDCharges = txtEMDCharges.Text;
                objSM.QuotFOB = txtFOB.Text;
                objSM.QuotCIF = txtCIF.Text;
                objSM.QuotCompany = ddlCompany.SelectedItem.Value;
                objSM.ttlDisc = txtspldiscount.Text;
                if (rbIndividual.Checked == true)
                {
                    objSM.QuotType = "Discount";
                }
                else
                {
                    objSM.QuotType = "Special";
                }
                objSM.Cp_Id = lblCPID.Text;
                if (objSM.SalesQuotation_Save() == "Data Saved Successfully")
                {
                    objSM.SalesQuotationDetails_Delete(objSM.QuotId);
                    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                    {
                        objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                        //objSM.QuotDetQty = gvrow.Cells[6].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                        objSM.QuotDetQty = Quantity.Text;
                        TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                        objSM.QuotRate = Rate.Text;
                        //objSM.QuotRate = gvrow.Cells[8].Text;
                        objSM.QuotDetDisc = gvrow.Cells[10].Text;
                        objSM.QuotDetSpPrice = gvrow.Cells[11].Text;
                        objSM.QuotGSTperc = gvrow.Cells[12].Text;
                        objSM.QuotGSTRate = gvrow.Cells[13].Text;
                        //objSM.QuotRoom = gvrow.Cells[14].Text;
                        TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                        objSM.QuotRoom = Room.Text;
                        objSM.QuotCurrency = gvrow.Cells[15].Text;
                        objSM.ColorId = gvrow.Cells[17].Text;
                        objSM.OptionalId = gvrow.Cells[18].Text;
                        objSM.Remarks = gvrow.Cells[20].Text;
                        objSM.SrlOrder = gvrow.Cells[22].Text;
                        // objSM.SrlOrder = gvrow.Cells[21].Text;
                        TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                        objSM.SrlOrder = srl.Text;

                        objSM.SalesQuotationDetails_Save();
                    }
                    objSM.SalesQuotationDetails_DeleteSubItems(objSM.QuotId);
                    foreach (GridViewRow gvr in gvSubItems.Rows)
                    {
                        objSM.QuotItemCodeMain = gvr.Cells[1].Text;
                        objSM.QuotSubItemCode = gvr.Cells[2].Text;
                        objSM.SalesQuotationDetailsSubItems_Save();
                    }

                    #region Saving Optional Items
                    foreach (GridViewRow gvrow in gvOptQuatationItems.Rows)
                    {
                        objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                        objSM.QuotDetQty = gvrow.Cells[6].Text;
                        objSM.QuotRate = gvrow.Cells[8].Text;
                        objSM.QuotDetDisc = gvrow.Cells[10].Text;
                        objSM.QuotDetSpPrice = gvrow.Cells[11].Text;
                        objSM.QuotRoom = gvrow.Cells[12].Text;
                        objSM.QuotCurrency = gvrow.Cells[13].Text;
                        objSM.ColorId = gvrow.Cells[15].Text;
                        objSM.OptionalId = gvrow.Cells[16].Text;
                        objSM.Remarks = gvrow.Cells[18].Text;
                        objSM.Itemtype = gvrow.Cells[17].Text;
                        objSM.Floor = gvrow.Cells[19].Text;

                        objSM.Itemtype = "Optional";

                        objSM.SalesQuotationDetails_Save();
                        objSM.SalesOptQuotationDetails_Save();
                    }
                    foreach (GridViewRow gvr in gvOptSubItems.Rows)
                    {
                        objSM.QuotItemCodeMain = gvr.Cells[1].Text;
                        objSM.QuotSubItemCode = gvr.Cells[2].Text;
                        objSM.SalesQuotationDetailsSubItems_Save();
                    }

                    #endregion

                    SM.SalesAssignments.SalesAssignmentsStatusQua_Update("Open", ddlEnquiryNo.SelectedItem.Value);

                    SM.CommitTransaction();
                    // MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    SM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                //gvQuotationDetails.DataBind();
                // gvEnquiryProducts.DataBind();
                btnSave.Enabled = true;
                gvQuotationItems.DataBind();
                tblQuotationDetails.Visible = false;
                SM.ClearControls(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
        "alert(' Quotation Saved sucessfully');window.location ='SalesQuotation.aspx';", true);
                /// Response.Redirect("~/Modules/SM/SalesQuotation.aspx");
                //  SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region SalesQuotationUpdate
    private void SalesQuotationUpdate()
    {
        if (gvQuotationItems.Rows.Count > 0)
        {
            try
            {
                SM.SalesQuotation objSM = new SM.SalesQuotation();

                SM.BeginTransaction();
                objSM.QuotId = Request.QueryString["QuoId"].ToString();
                objSM.QuotNo = txtQuotationNo.Text;
                objSM.QuotDate = Yantra.Classes.General.toMMDDYYYY(txtQuotationDate.Text);
                objSM.QuotDelivery = txtDelivery.Text;
                objSM.QuotPayTerms = txtPaymentTerms.Text;
                objSM.QuotPackCharges = txtPackingCharges.Text;
                objSM.QuotExcise = txtExciseDuty.Text;
                //objSM.QuotCST = txtCST.Text;
                //objSM.QuotVAT = txtVAT.Text;
                if (rbVAT.Checked == true)
                { objSM.QuotVAT = txtVAT.Text; }
                else if (rbCST.Checked == true)
                { objSM.QuotCST = txtVAT.Text; }
                else if (rbincluding.Checked == true)
                { objSM.QuotIncluding = txtVAT.Text; }

                objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                objSM.QuotGuarantee = txtGuarantee.Text;
                objSM.QuotTransCharges = txtTransCharges.Text;
                objSM.QuotInsurance = txtInsurance.Text;
                objSM.QuotErrec = txtErrection.Text;
                objSM.QuotJurisdiction = txtJurisdiction.Text;
                objSM.QuotValidity = txtValidity.Text;
                objSM.QuotInspection = txtInspection.Text;
                objSM.QuotOtherSpecs = txtOtherSpecs.Text;
                //   objSM.QuotPOLog = txtpo.Text;
                objSM.QuotRespId = ddlResponsiblePerson.SelectedItem.Value;
                objSM.QuotSalespId = ddlSalesPerson.SelectedItem.Value;
                objSM.QuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.QuotCheckedBy = ddlCheckedBy.SelectedItem.Value;
                objSM.QuotApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objSM.CurrencyId = ddlCurrencyType.SelectedItem.Value;

                objSM.QuotDDNo = txtDDNo.Text;
                objSM.QuotDDDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
                objSM.QuotBankName = txtBankName.Text;
                objSM.IsExpectedOrder = chkIsExpectedOrder.Checked;
                objSM.QuotTotalEMDCharges = txtEMDCharges.Text;
                objSM.QuotFOB = txtFOB.Text;
                objSM.QuotCIF = txtCIF.Text;
                objSM.QuotCompany = ddlCompany.SelectedItem.Value;
                objSM.ttlDisc = txtspldiscount.Text;
                if (rbIndividual.Checked == true)
                {
                    objSM.QuotType = "Discount";
                }
                else
                {
                    objSM.QuotType = "Special";
                }
                objSM.Cp_Id = lblCPID.Text;
                if (objSM.SalesQuotation_Update() == "Data Updated Successfully")
                {
                    objSM.SalesQuotationDetails_Delete(objSM.QuotId);
                    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                    {
                        objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                        //objSM.QuotDetQty = gvrow.Cells[6].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                        objSM.QuotDetQty = Quantity.Text;
                        TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                        objSM.QuotRate = Rate.Text;
                        //objSM.QuotRate = gvrow.Cells[8].Text;
                        objSM.QuotDetDisc = gvrow.Cells[10].Text;
                        objSM.QuotDetSpPrice = gvrow.Cells[11].Text;
                        objSM.QuotGSTperc = gvrow.Cells[12].Text;
                        objSM.QuotGSTRate = gvrow.Cells[13].Text;
                        //objSM.QuotRoom = gvrow.Cells[14].Text;
                        TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                        objSM.QuotRoom = Room.Text;
                        objSM.QuotCurrency = gvrow.Cells[15].Text;
                        objSM.ColorId = gvrow.Cells[17].Text;
                        objSM.OptionalId = gvrow.Cells[18].Text;
                        objSM.Remarks = gvrow.Cells[20].Text;
                        objSM.SrlOrder = gvrow.Cells[22].Text;
                        // objSM.SrlOrder = gvrow.Cells[21].Text;
                        TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                        objSM.SrlOrder = srl.Text;
                        objSM.SalesQuotationDetails_Save();
                    }
                    objSM.SalesQuotationDetails_DeleteSubItems(objSM.QuotId);
                    foreach (GridViewRow gvr in gvSubItems.Rows)
                    {
                        objSM.QuotItemCodeMain = gvr.Cells[1].Text;
                        objSM.QuotSubItemCode = gvr.Cells[2].Text;

                        objSM.SalesQuotationDetailsSubItems_Save();
                    }

                    #region Saving Optional Items
                    foreach (GridViewRow gvrow in gvOptQuatationItems.Rows)
                    {
                        objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                        objSM.QuotDetQty = gvrow.Cells[6].Text;
                        objSM.QuotRate = gvrow.Cells[8].Text;
                        objSM.QuotDetDisc = gvrow.Cells[10].Text;
                        objSM.QuotDetSpPrice = gvrow.Cells[11].Text;
                        objSM.QuotRoom = gvrow.Cells[12].Text;
                        objSM.QuotCurrency = gvrow.Cells[13].Text;
                        objSM.ColorId = gvrow.Cells[15].Text;
                        objSM.OptionalId = gvrow.Cells[16].Text;
                        objSM.Remarks = gvrow.Cells[17].Text;
                        objSM.SrlOrder = gvrow.Cells[18].Text;
                        objSM.Itemtype = "Optional";

                        objSM.SalesQuotationDetails_Save();
                        objSM.SalesOptQuotationDetails_Save();
                    }
                    foreach (GridViewRow gvr in gvOptSubItems.Rows)
                    {
                        objSM.QuotItemCodeMain = gvr.Cells[1].Text;
                        objSM.QuotSubItemCode = gvr.Cells[2].Text;
                        objSM.SalesQuotationDetailsSubItems_Save();
                    }

                    #endregion
                    SM.CommitTransaction();
                    // MessageBox.Show(this, "Data Updated Successfully");
                }
                else
                {
                    SM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnSave.Text = "Save";
                btnDelete.Attributes.Clear();
                //gvQuotationDetails.DataBind();
                // gvEnquiryProducts.DataBind();
                gvQuotationItems.DataBind();
                tblQuotationDetails.Visible = false;
                SM.ClearControls(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
        "alert(' Quotation Updated sucessfully');window.location ='SalesQuotation.aspx';", true);
                // SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["QuoId"] != null)
        {
            try
            {
                SM.SalesQuotation objSM = new SM.SalesQuotation();
                if (objSM.SalesQuotation_Select(Request.QueryString["QuoId"].ToString()) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblQuotationDetails.Visible = true;

                    txtQuotationNo.Text = objSM.QuotNo;
                    txtQuotationDate.Text = objSM.QuotDate;
                    ddlEnquiryNo.SelectedValue = objSM.EnqId;
                    txtDelivery.Text = objSM.QuotDelivery;
                    txtPaymentTerms.Text = objSM.QuotPayTerms;
                    txtPackingCharges.Text = objSM.QuotPackCharges;
                    txtExciseDuty.Text = objSM.QuotExcise;
                    //txtCST.Text = objSM.QuotCST;
                    //txtVAT.Text = objSM.QuotVAT;
                    if (objSM.QuotVAT != "")
                    {
                        txtVAT.Text = objSM.QuotVAT;
                        lblVATCST.Text = "TAX";
                        rbVAT.Checked = true;
                        rbCST.Checked = false;
                        rbincluding.Checked = false;

                    }
                    else if (objSM.QuotCST != "")
                    {
                        txtVAT.Text = objSM.QuotCST;
                        lblVATCST.Text = "TAX";
                        rbCST.Checked = true;
                        rbVAT.Checked = false;
                        rbincluding.Checked = false;

                    }
                    else if (objSM.QuotIncluding != "")
                    {
                        txtVAT.Text = objSM.QuotIncluding;
                        rbincluding.Checked = true;
                        rbCST.Checked = false;
                        rbVAT.Checked = false;

                        lblVATCST.Text = "TAX";

                    }
                    if (objSM.QuotType == "Special")
                    {
                        rbProject.Checked = true;
                    }
                    else
                    {
                        rbIndividual.Checked = true;
                    }
                    if (objSM.QuotType == "Discount")
                    {
                        rbIndividual.Checked = true;
                    }
                    else
                    {
                        rbProject.Checked = true;
                    }

                    txtspldiscount.Text = objSM.ttlDisc;
                    ddlCompany.SelectedValue = objSM.Cp_Id;
                    ddlDespatchMode.SelectedValue = objSM.DespmId;
                    txtGuarantee.Text = objSM.QuotGuarantee;
                    txtTransCharges.Text = objSM.QuotTransCharges;
                    txtInsurance.Text = objSM.QuotInsurance;
                    txtErrection.Text = objSM.QuotErrec;
                    txtJurisdiction.Text = objSM.QuotJurisdiction;
                    txtValidity.Text = objSM.QuotValidity;
                    txtInspection.Text = objSM.QuotInspection;
                    txtOtherSpecs.Text = objSM.QuotOtherSpecs;
                    //   objSM.QuotPOLog = txtpo.Text;
                    ddlResponsiblePerson.SelectedValue = objSM.QuotRespId;
                    ddlSalesPerson.SelectedValue = objSM.QuotSalespId;
                    ddlPreparedBy.SelectedValue = objSM.QuotPreparedBy;
                    ddlCheckedBy.SelectedValue = objSM.QuotCheckedBy;
                    ddlApprovedBy.SelectedValue = objSM.QuotApprovedBy;
                    if (objSM.QuotApprovedBy != "0")
                    {
                        btnApprove.Visible = false;
                        btnSalesOrder.Visible = false;
                        btnRefresh.Visible = false;

                    }

                    ddlCurrencyType.SelectedValue = objSM.CurrencyId;
                    // ddlCurrencyType_SelectedIndexChanged(sender, e);
                    chkIsExpectedOrder.Checked = objSM.IsExpectedOrder;
                    if (objSM.QuotPOLog == SM.SMStatus.Obsolete.ToString())
                    {
                        lblApprovedBy.Text = "Absoluted By";
                    }
                    else
                    {
                        lblApprovedBy.Text = "Approved By";
                    }
                    txtDDNo.Text = objSM.QuotDDNo;
                    txtDDDate.Text = objSM.QuotDDDate;
                    txtBankName.Text = objSM.QuotBankName;
                    txtFOB.Text = objSM.QuotFOB;
                    txtCIF.Text = objSM.QuotCIF;

                    objSM.SalesQuotationDetails_Select(Request.QueryString["QuoId"].ToString(), gvQuot);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                // SM.Dispose();
                ddlEnquiryNo_SelectedIndexChanged(sender, e);
                ddlResponsiblePerson_SelectedIndexChanged(sender, e);
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
        if (Request.QueryString["QuoId"] != null)
        {
            try
            {
                SM.SalesQuotation objSM = new SM.SalesQuotation();
                MessageBox.Show(this, objSM.SalesQuotation_Delete(Request.QueryString["QuoId"].ToString()));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                //gvQuotationDetails.DataBind();
                SM.ClearControls(this);
                //  SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Enquiry No. Select Index Changed
    protected void ddlEnquiryNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.SalesEnquiry objSM = new SM.SalesEnquiry();
            if (objSM.SalesEnquiry_Select(ddlEnquiryNo.SelectedItem.Value) > 0)
            {
                txtTransCharges.Text = "Transportation levies are extra as applicable";
                txtDelivery.Text = "Within 10-14 weeks";
                txtPaymentTerms.Text = "100% in advance";
                ddlEssentials.Enabled = false;
                ddlRate.SelectedValue = "1";
                ddlRate.Enabled = true;
                txtValidity.Text = DateTime.Now.AddDays(15).ToString("d");
                txtEnquiryDate.Text = objSM.EnqDate;
                if (objSM.EnqModeName.ToLower() == "tender")
                {
                    tdEMDHeader.Visible = true;
                    tdDDNolbl.Visible = tdDDNoField.Visible = tdDDDatelbl.Visible = tdDDDateField.Visible = true;
                    tdBankNamelbl.Visible = tdBankNameField.Visible = true;
                    tdEMDChargeslbl.Visible = tdEMDChargesField.Visible = tdInfavourOflbl.Visible = tdInfavourofField.Visible = true;
                    // objSM.SalesEnquiryDetails_Select(ddlEnquiryNo.SelectedItem.Value, gvEnquiryProducts);
                    //foreach (GridViewRow gvRow in gvEnquiryProducts.Rows)
                    //{
                    //    if (txtEMDCharges.Text == "") { txtEMDCharges.Text = "0"; }
                    //    if (gvRow.Cells[10].Text == "-") { gvRow.Cells[10].Text = "0"; }

                    //    txtEMDCharges.Text = Convert.ToString(int.Parse(gvRow.Cells[10].Text) + int.Parse(txtEMDCharges.Text));
                    //    if (gvRow.RowIndex == 0)
                    //        txtInFavourofEMD.Text = gvRow.Cells[11].Text;
                    //}
                }
                else
                {
                    tdEMDHeader.Visible = false;
                    tdDDNolbl.Visible = tdDDNoField.Visible = tdDDDatelbl.Visible = tdDDDateField.Visible = false;
                    tdBankNamelbl.Visible = tdBankNameField.Visible = false;
                    tdEMDChargeslbl.Visible = tdEMDChargesField.Visible = tdInfavourOflbl.Visible = tdInfavourofField.Visible = false;
                    //  objSM.SalesEnquiryDetails_Select(ddlEnquiryNo.SelectedItem.Value, gvEnquiryProducts);
                }
                //ItemTypes_Fill();

                ddlCustomer.SelectedValue = objSM.CustId;
                ddlCustomer_SelectedIndexChanged(sender, e);
                ddlUnitName.SelectedValue = objSM.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objSM.CustDetId;
                ddlContactPerson_SelectedIndexChanged(sender, e);

                //SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                //if (objSMCustomer.CustomerMasterUnitsDetailsEnquiry_Select(objSM.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = objSMCustomer.CustUnitName;
                //}
                //else if (objSMCustomer.CustomerMaster_Select(objSM.CustId) > 0)
                //{
                //    txtCustName.Text = objSMCustomer.CustName;
                //    txtAddress.Text = objSMCustomer.Address;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtRegion.Text = objSMCustomer.RegName;
                //    txtPhone.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
                //    txtUnitName.Text = "--";
                //}
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            //SM.Dispose();
        }
    }
    #endregion

    #region GridView Enquiry Products Row Databound
    protected void gvEnquiryProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

            if (tdEMDHeader.Visible == false)
            {
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
            }
        }
    }
    #endregion
    decimal TotalAmount1 = 0;
    decimal TotalAmount2 = 0;
    decimal TotalGst = 0;
    protected void gvitemsgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = (Convert.ToDecimal(e.Row.Cells[10].Text) / Convert.ToInt32(e.Row.Cells[6].Text)).ToString("F");
            e.Row.Cells[11].Text = (Convert.ToInt32(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text)).ToString();
            TotalAmount2 = TotalAmount2 + Convert.ToDecimal(e.Row.Cells[10].Text);
            lblsp.Text = TotalAmount2.ToString();
            TotalAmount1 = TotalAmount1 + Convert.ToDecimal(e.Row.Cells[11].Text);
            lblFlr.Text = TotalAmount1.ToString();
            TotalGst = TotalGst + Convert.ToDecimal(e.Row.Cells[12].Text);
            lblGstrate.Text = TotalGst.ToString();
            string imageName = "~/Content/Images/" + (e.Row.FindControl("lblPath") as Label).Text;
            string[] filename = imageName.Split('/');

            // 70 is define image size.
            GenerateThumbNail("~/Content/ItemImage/" + filename[3], imageName, 100);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            string msg = ddlUnitName.SelectedItem.Text + "<br>" + txtUnitAddress.Text + "<br>" + "<br>" + "Email : " + " " + txtEmail.Text + "<br>" + "Contact :" + " " + txtPhoneNo.Text + ","
                + "<br>" + "<br>" + "Dear Sir," + "<br>" +
                "We thank you for your enquiry and are pleased to submit out bet offer quoe for supply of the following" +
                "<br>" + "sanitaryware and bath fittings. We hope you find rates competitive and will oblige us with your valuable order.";

            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = msg;
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);
            string logo = "<img style='text-align:left; width:100PX; height :100px'  src='http://183.82.108.55/Content/CompanyProfileImgs/Valueline-Logo-Uploaded-on-Website-_-388-X-126px.png'>" + "<br>" + "Date : " + txtQuotationDate.Text + "";

            //GridView HeaderGrid1 = (GridView)sender;
            //GridViewRow HeaderGridRow1 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell1 = new TableCell();
            //HeaderCell1.Text = logo;
            //HeaderCell1.ColumnSpan = 7;
            //HeaderGridRow1.Cells.Add(HeaderCell1);

            gvitemsgrid.Controls[0].Controls.AddAt(0, HeaderGridRow);
            //gvitemsgrid.Controls[0].Controls.AddAt(0, HeaderGridRow1);
            if (chkwp.Checked == true)
            {
                e.Row.Cells[7].Visible = true;
                HeaderCell.ColumnSpan = 8;

            }
            if (chkwpsp.Checked == true)
            {
                e.Row.Cells[9].Visible = true;
                if (chkwp.Checked == true)
                {
                    HeaderCell.ColumnSpan = 9;
                }
                else { HeaderCell.ColumnSpan = 8; }
            }
            if (chkwpspta.Checked == true)
            {
                e.Row.Cells[10].Visible = true;
                if (chkwp.Checked == true)
                {
                    if (chkwpsp.Checked == true)
                    {
                        HeaderCell.ColumnSpan = 10;
                    }
                    else
                    {
                        HeaderCell.ColumnSpan = 9;
                    }
                }
                else { HeaderCell.ColumnSpan = 8; }
            }
            if (chk3gst.Checked == true)
            {
                if (chkwpspta.Checked == true)
                {
                    e.Row.Cells[10].Visible = true;
                    HeaderCell.ColumnSpan = 11;
                }
                
                else if (chkmrptotal.Checked == true)
                {
                    e.Row.Cells[11].Visible = true;
                }
            }
            if (chkmrptotal.Checked == true)
            {
                e.Row.Cells[11].Visible = true;
                if (chkwp.Checked == true)
                {
                    HeaderCell.ColumnSpan = 9;
                }
                else { HeaderCell.ColumnSpan = 8; }

            }
            if (chk3wc.Checked == true)
            {
                e.Row.Cells[12].Visible = true;
            }
            if (chkwt.Checked == true)
            {

            }
            if (chkWoPrices.Checked == true)
            {
                e.Row.Cells[3].Visible = true;
            }
            if (chkwsp.Checked == true)
            {

            }
            if (chkGST.Checked == true)
            {
                e.Row.Cells[12].Visible = true;
                if (chkwpspta.Checked == true)
                {
                    if (chkwpsp.Checked == true)
                    {
                        HeaderCell.ColumnSpan = 10;
                    }
                    else
                    {
                        HeaderCell.ColumnSpan = 9;
                    }
                }
                else { HeaderCell.ColumnSpan = 8; }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            if (chkwp.Checked == true)
            {
                e.Row.Cells[7].Visible = true;
            }
            if (chkwpsp.Checked == true)
            {
                e.Row.Cells[9].Visible = true;
            }
            if (chkwpspta.Checked == true)
            {
                e.Row.Cells[10].Visible = true;
            }
            if (chk3gst.Checked == true)
            {
                if (chkwpspta.Checked == true)
                {
                    e.Row.Cells[10].Visible = true;
                }
                else if (chkmrptotal.Checked == true)
                {
                    e.Row.Cells[11].Visible = true;
                }
            }
            if (chkmrptotal.Checked == true)
            {
                e.Row.Cells[11].Visible = true;
            }
            if (chk3wc.Checked == true)
            {
                e.Row.Cells[12].Visible = true;
            }
            if (chkwt.Checked == true)
            {

            }
            if (chkWoPrices.Checked == true)
            {
                e.Row.Cells[3].Visible = true;
            }
            if (chkwsp.Checked == true)
            {

            }
            if (chkGST.Checked == true)
            {
                e.Row.Cells[12].Visible = true;
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            if (chkwp.Checked == true)
            {
                e.Row.Cells[7].Visible = true;
            }
            if (chkwpsp.Checked == true)
            {
                e.Row.Cells[9].Visible = true;
            }
            if (chkwpspta.Checked == true)
            {
                e.Row.Cells[10].Visible = true;

            }
            if (chk3gst.Checked == true)
            {
                if (chkwpspta.Checked == true)
                {
                    e.Row.Cells[10].Visible = true;
                    //e.Row.Cells[9].Text = "Total Amount";
                    e.Row.Cells[10].Text = "Total Amount : " + " " + TotalAmount2.ToString();
                    chkmrptotal.Checked = false;
                }
                else if (chkmrptotal.Checked == true)
                {
                    e.Row.Cells[11].Visible = true;
                    //e.Row.Cells[9].Text = "Total Amount";
                    e.Row.Cells[11].Text = "Total Amount : " + " " + TotalAmount1.ToString();
                    chkwpspta.Checked = false;
                }
            }
            if (chkmrptotal.Checked == true)
            {
                e.Row.Cells[11].Visible = true;
            }
            if (chk3wc.Checked == true)
            {
                e.Row.Cells[12].Text = "Add GST - 18% : " + " " + TotalGst.ToString();
                e.Row.Cells[12].Visible = true;
            }
            if (chkwt.Checked == true)
            {

            }
            if (chkWoPrices.Checked == true)
            {
                e.Row.Cells[3].Visible = true;
            }
            if (chkwsp.Checked == true)
            {

            }
            if (chkGST.Checked == true)
            {
                //e.Row.Cells[12].Text = "Add GST - 18% : " + " " + TotalGst.ToString();
                e.Row.Cells[12].Visible = true;
            }
            TableRow tableRow = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = "<br>"+"Terms and Conditions"+"<br>"+"<br>"+"";
            cell1.ColumnSpan = 7; // You can change this
            //cell1.Visible = false;
            tableRow.Controls.AddAt(tableRow.Controls.Count, cell1);
            e.Row.NamingContainer.Controls.Add(tableRow);

            TableRow tableRow1 = new TableRow();
            TableCell cell2 = new TableCell();
            
            cell2.Text = "1. Rates quoted are Exclusive of GST as applicable, Ex- Hyderabad | Bangalore | Vizag | Chennai." 
                + "<br>" + "2. In-case there is price revision from the manufacturing company those prices will be applicable without any prior intimation." 
                + "<br>" + "3. Transportation levies are extra as applicable."
                + "<br>" + "4. Unloading of material at site to be arranged by the client."
                + "<br>" + "5. The prices mentioned in the quotation will be valid for 15 days only." 
                + "<br>" + "6. Payment 100% advance."
                + "<br>" + "7. This order is confirmed only after a Purchase Order is received from you, a written / email signed confirmation by you."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;This has to be accompanied by an advance payment, Billing Detail, PAN/TIN  Number & Delivery Schedule."
                + "<br>" + "8. Delivery within 10-14 Weeks from the date of receipt of your confirmation."
                + "<br>" + "9. No return or exchange policy exists in this case as goods are procured on clients specifications."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;As special case If any Cancellation is Done then 35% ofthe  Value of the product will be charged. "
                + "<br>" + "10. Delivery Charges will be as actuals. Please mention delivery address in the purchase order along with contact name and telephone number."
                + "<br>" + "11. Inspection of the material is done at the site of delivery. Client or client’s"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;representative is expected to check the material carefully and then sign the bill / DC."
                + "<br>" + "12. Value Line will not be responsible for any of the breakages / shortages after "
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;the products have been checked / accepted and the Bill/DC has been signed by the client or the client’s representative."
                + "<br>" + "13. Ordered goods as per the delivery status given at the time of confirmation, once received from the port,  cannot be warehoused with us for more than two weeks,"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;thereafter there will be 5% of the total order value warehouse charges for the period for which goods are kept in our warehouse."
                + "<br>" + "14. For all chrome plated fittings, damage caused due to acid based cleaning liquids, the warranty stands void."
                + "<br>" + "15. The warranty on all the products will be from the manufacturer's end."
                + "<br>" + "16. For selected materials, essentials need to take separately and the quote for the same will be given as and when required."
                + "<br>" + "17. Plumbing pipes should be checked thoroughly before the installation of concealed and exposed part to avoid choking and improper functioning of the products. "
                + "<br>" + "18. Fittings"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a) Use Only Water And Neutral Soap Detergents To Clean The Faucets, Wiping It With Sponge Or A Soft Cloth."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b) Never Use Alcohol, Solvents, Solids Or Liquid Detergents Containing Corroding Substances Or Acids, "
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cloths With Synthetic Fibres, Abrasive Sponges Or Wire Buffers That May Cause Irreversible Damages On The Treated Surfaces. If used, seller is not responsible for the damages."
                + "<br>" + "19. Tiles:"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a) Unloading of material at site to be arranged by client."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b) Please check all the tiles for shade & size variation and warpage at time of delivery. No complaints will be entertained thereafter."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)  Execution of material at site has to be taken care by the client as the layers / workers are outsourced."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d) Transit damage is not our responsibility."
                + "<br>" + "20. Wooden Flooring"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Laying charges Including Film & Foam  are extra as applicable."
                + "<br>" + "21. Technical Guidance:"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a) Kindly note that we provide only technical assistance and do not undertake any contract for installation of the products"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b) The technical assistance for pre & post installation will be free of cost for maximum 5 visits in Local and 3 Visits if site is Outstation."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;c)  All subsequent visits of the technicians will be on chargeable basis."
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;d) For any complaints you may contact on the given nos. M: 8008103089, Land: 040 23554474/ 75, or E-mail us : customercare@valueline.in"
                + "<br>" + "22. Dispatches:"
                + "<br>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a) For any dispatch queries you may contact on the given nos.  M:8008103089  Land: 040-23554474 / 75 Timings are from 11.30 am to 6 pm."
                +"<br>"+"<br>"+"<br>"+"Thanking You,";
            cell2.ColumnSpan = 7; // You can change this5. 
            cell2.Wrap = true;
            //cell1.Visible = false;
            tableRow1.Controls.AddAt(tableRow1.Controls.Count, cell2);
            e.Row.NamingContainer.Controls.Add(tableRow1);
            ////e.Row.Cells[3].Text = "Total :";
            //e.Row.Cells[0].Text = cell1.Text;
        }
    }
    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
        gvQuotationItems.DataBind();
        // gvEnquiryProducts.DataBind();

        //chkIsExpectedOrder.Checked=
        SM.SalesQuotation OBJSM = new SM.SalesQuotation();
        if (chkIsExpectedOrder.Checked == true)
        {
            chkIsExpectedOrder.Checked = false;
            //OBJSM.IsExpectedOrder = false;
        }
        else
        {
            OBJSM.IsExpectedOrder = chkIsExpectedOrder.Checked;
        }
        if (Request.QueryString["enqid"] != null)
        {
            btnNew_Click(sender, e);
            ddlEnquiryNo.SelectedValue = Request.QueryString["enqid"].ToString();
            ddlEnquiryNo_SelectedIndexChanged(sender, e);
            tblQuotationDetails.Visible = true;
        }
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalesQuotation.aspx");

        tblQuotationDetails.Visible = false;
    }
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblOptId.Text = RandomGenerator().ToString();
        if (txtRoom.Text == "") { txtRoom.Text = "--"; }
        if (ddlColor.SelectedItem.Value == "")
        {
            ddlColor.SelectedItem.Value = "0";
            ddlColor.SelectedItem.Text = "-";
        }
        if (txtRemarks.Text == "") { txtRemarks.Text = "--"; }
        if (txtItemUOM.Text == "") { txtItemUOM.Text = "--"; }


        if (ddlEssentials.SelectedValue == "0")
        {
            ddlEssentials.SelectedItem.Value = lblItemCode.Text;
            ddlEssentials.SelectedItem.Text = ddlModelNo.SelectedItem.Text;
        }
        DataTable SubItems = new DataTable();
        DataColumn dc;
        dc = new DataColumn("ITEM_CODE");
        SubItems.Columns.Add(dc);
        dc = new DataColumn("SUBITEM_CODE");
        SubItems.Columns.Add(dc);

        if (gvSubItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvSubItems.Rows)
            {
                DataRow dr = SubItems.NewRow();
                dr["ITEM_CODE"] = gvrow.Cells[1].Text;
                dr["SUBITEM_CODE"] = gvrow.Cells[2].Text;
                SubItems.Rows.Add(dr);
            }
        }
        for (int i = 0; i < chklitemcolor.Items.Count; i++)
        {
            if (chklitemcolor.Items[i].Selected == true)
            {
                DataRow dr = SubItems.NewRow();
                dr["ITEM_CODE"] = lblItemCode.Text;
                dr["SUBITEM_CODE"] = chklitemcolor.Items[i].Value;
                SubItems.Rows.Add(dr);
            }
            //else if (chklitemcolor.Items[i].Selected != true)
            //{

            //    return;
            //}

        }
        gvSubItems.DataSource = SubItems;
        gvSubItems.DataBind();
        chklitemcolor.Items.Clear();

        if (rbProject.Checked == true)
        {
            DataTable QuotationItems = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("ItemCode");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ModelNo");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ItemName");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("UOM");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Quantity");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Currency");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Rate");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Discount");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("SpPrice");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("GSTperc");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("GSTRate");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Room");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("CurrencyId");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Color");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ColorId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("OptId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ItemType");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Remarks");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Floor");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("SrlOrder");
            QuotationItems.Columns.Add(col);

            if (gvQuotationItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    if (gvQuotationItems.SelectedIndex > -1)
                    {
                        if (gvrow.RowIndex == gvQuotationItems.SelectedRow.RowIndex)
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = lblItemCode.Text;
                            dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                            dr["ItemName"] = txtItemName.Text;
                            dr["UOM"] = txtItemUOM.Text;
                            dr["Quantity"] = txtQunatity.Text;
                            dr["Currency"] = ddlRate.SelectedItem.Text;
                            dr["Rate"] = txtRate.Text;

                            dr["Discount"] = txtDiscount.Text;
                            dr["SpPrice"] = txtSpPrice.Text;
                            dr["GSTperc"] = txtGST_Perc.Text;
                            dr["GSTRate"] = txtGST_Amt.Text;
                            dr["Room"] = txtRoom.Text;
                            dr["CurrencyId"] = ddlRate.SelectedItem.Value;

                            dr["Color"] = ddlColor.SelectedItem.Text;
                            dr["ColorId"] = ddlColor.SelectedItem.Value;
                            dr["OptId"] = lblOptId.Text;
                            dr["ItemType"] = "Original";
                            if (txtorgRemarks.Text == "")
                            {
                                dr["Remarks"] = "-";
                            }
                            else
                            {
                                dr["Remarks"] = txtorgRemarks.Text;
                            }
                            dr["Floor"] = txtFloor.Text;
                            dr["SrlOrder"] = txtSrlOrderNo.Text;

                            QuotationItems.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = gvrow.Cells[2].Text;
                            dr["ModelNo"] = gvrow.Cells[3].Text;
                            dr["ItemName"] = gvrow.Cells[4].Text;
                            dr["UOM"] = gvrow.Cells[5].Text;
                            //dr["Quantity"] = gvrow.Cells[6].Text;
                            TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                            dr["Quantity"] = Quantity.Text;
                            dr["Currency"] = gvrow.Cells[7].Text;
                            //dr["Rate"] = gvrow.Cells[8].Text;
                            TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                            dr["Rate"] = Rate.Text;
                            dr["Discount"] = gvrow.Cells[10].Text;
                            dr["SpPrice"] = gvrow.Cells[11].Text;
                            dr["GSTperc"] = gvrow.Cells[12].Text;
                            dr["GSTRate"] = gvrow.Cells[13].Text;
                            //dr["Room"] = gvrow.Cells[14].Text;
                            TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                            dr["Room"] = Room.Text;
                            dr["CurrencyId"] = gvrow.Cells[15].Text;

                            dr["Color"] = gvrow.Cells[16].Text;
                            dr["ColorId"] = gvrow.Cells[17].Text;
                            dr["OptId"] = gvrow.Cells[18].Text;
                            dr["ItemType"] = gvrow.Cells[19].Text;
                            dr["Remarks"] = gvrow.Cells[20].Text;
                            //dr["Floor"] = gvrow.Cells[21].Text;
                            TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                            dr["Floor"] = Floor.Text;
                            TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                            dr["SrlOrder"] = srl.Text;

                            QuotationItems.Rows.Add(dr);
                        }
                    }
                    else
                    {

                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        //dr["Quantity"] = gvrow.Cells[6].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                        dr["Quantity"] = Quantity.Text;
                        dr["Currency"] = gvrow.Cells[7].Text;
                        //dr["Rate"] = gvrow.Cells[8].Text;
                        TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                        dr["Rate"]=Rate.Text ;
                        dr["Discount"] = gvrow.Cells[10].Text;
                        dr["SpPrice"] = gvrow.Cells[11].Text;
                        dr["GSTperc"] = gvrow.Cells[12].Text;
                        dr["GSTRate"] = gvrow.Cells[13].Text;
                        //dr["Room"] = gvrow.Cells[14].Text;
                        TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                        dr["Room"] = Room.Text;
                        dr["CurrencyId"] = gvrow.Cells[15].Text;

                        dr["Color"] = gvrow.Cells[16].Text;
                        dr["ColorId"] = gvrow.Cells[17].Text;
                        dr["OptId"] = gvrow.Cells[18].Text;
                        dr["ItemType"] = gvrow.Cells[19].Text;
                        dr["Remarks"] = gvrow.Cells[20].Text;
                        //dr["Floor"] = gvrow.Cells[21].Text;
                        TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                        dr["Floor"] = Floor.Text;
                        // dr["SrlOrder"] = gvrow.Cells[21].Text;
                        TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                        dr["SrlOrder"] = srl.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
            }

            //if (gvQuotationItems.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            //    {
            //        if (gvQuotationItems.SelectedIndex == -1)
            //        {
            //            if (gvrow.Cells[2].Text == ddlModelNo.SelectedItem.Value)
            //            {
            //                gvQuotationItems.DataSource = QuotationItems;
            //                gvQuotationItems.DataBind();
            //                MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
            //                return;
            //            }
            //        }
            //    }
            //}

            if (gvQuotationItems.SelectedIndex == -1)
            {
                DataRow drnew = QuotationItems.NewRow();
                drnew["ItemCode"] = lblItemCode.Text;
                drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
                drnew["ItemName"] = txtItemName.Text;
                drnew["UOM"] = txtItemUOM.Text;
                drnew["Quantity"] = txtQunatity.Text;
                drnew["Currency"] = ddlRate.SelectedItem.Text;
                drnew["Rate"] = txtRate.Text;

                drnew["Discount"] = txtDiscount.Text;
                drnew["SpPrice"] = txtSpPrice.Text;
                drnew["GSTperc"] = txtGST_Perc.Text;
                drnew["GSTRate"] = txtGST_Amt.Text;
                drnew["Room"] = txtRoom.Text;
                drnew["CurrencyId"] = ddlRate.SelectedItem.Value;

                drnew["Color"] = ddlColor.SelectedItem.Text;
                drnew["ColorId"] = ddlColor.SelectedItem.Value;
                drnew["OptId"] = lblOptId.Text;
                drnew["ItemType"] = "Original";
                drnew["Remarks"] = txtorgRemarks.Text;
                drnew["Floor"] = txtFloor.Text;
                drnew["SrlOrder"] = txtSrlOrderNo.Text;

                QuotationItems.Rows.Add(drnew);
            }
            gvQuotationItems.DataSource = QuotationItems;
            gvQuotationItems.DataBind();
            gvQuotationItems.SelectedIndex = -1;
            btnItemRefresh_Click(sender, e);
        }
        else
        {
            DataTable QuotationItems = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("ItemCode");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ModelNo");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ItemName");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("UOM");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Quantity");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Currency");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Rate");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Discount");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("SpPrice");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("GSTperc");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("GSTRate");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Room");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("CurrencyId");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Color");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ColorId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("OptId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ItemType");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Remarks");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Floor");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("SrlOrder");
            QuotationItems.Columns.Add(col);



            if (gvQuotationItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    if (gvQuotationItems.SelectedIndex > -1)
                    {
                        if (gvrow.RowIndex == gvQuotationItems.SelectedRow.RowIndex)
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = ddlEssentials.SelectedItem.Value;
                            dr["ModelNo"] = ddlEssentials.SelectedItem.Text;
                            dr["ItemName"] = txtItemName.Text;
                            dr["UOM"] = txtItemUOM.Text;
                            dr["Quantity"] = txtQunatity.Text;
                            dr["Currency"] = ddlRate.SelectedItem.Text;
                            dr["Rate"] = txtRate.Text;

                            dr["Discount"] = txtDiscount.Text;
                            dr["SpPrice"] = txtSpPrice.Text;
                            dr["GSTperc"] = txtGST_Perc.Text;
                            dr["GSTRate"] = txtGST_Amt.Text;
                            dr["Room"] = txtRoom.Text;
                            dr["CurrencyId"] = ddlRate.SelectedItem.Value;

                            dr["Color"] = ddlColor.SelectedItem.Text;
                            dr["ColorId"] = ddlColor.SelectedItem.Value;
                            dr["OptId"] = lblOptId.Text;
                            dr["ItemType"] = "Original";
                            dr["Remarks"] = txtorgRemarks.Text;
                            dr["Floor"] = txtFloor.Text;
                            dr["SrlOrder"] = txtSrlOrderNo.Text;

                            QuotationItems.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = gvrow.Cells[2].Text;
                            dr["ModelNo"] = gvrow.Cells[3].Text;
                            dr["ItemName"] = gvrow.Cells[4].Text;
                            dr["UOM"] = gvrow.Cells[5].Text;
                            TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                            dr["Quantity"] = Quantity.Text;
                            dr["Currency"] = gvrow.Cells[7].Text;
                            //dr["Rate"] = gvrow.Cells[8].Text;
                            TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                            dr["Rate"] = Rate.Text;
                            dr["Discount"] = gvrow.Cells[10].Text;
                            dr["SpPrice"] = gvrow.Cells[11].Text;
                            dr["GSTperc"] = gvrow.Cells[12].Text;
                            dr["GSTRate"] = gvrow.Cells[13].Text;
                            TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                            dr["Room"] = Room.Text;
                            dr["CurrencyId"] = gvrow.Cells[15].Text;
                            dr["Color"] = gvrow.Cells[16].Text;
                            dr["ColorId"] = gvrow.Cells[17].Text;
                            dr["OptId"] = gvrow.Cells[18].Text;
                            dr["ItemType"] = gvrow.Cells[19].Text;
                            dr["Remarks"] = gvrow.Cells[20].Text;
                            TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                            dr["Floor"] = Floor.Text;
                            // dr["SrlOrder"] = gvrow.Cells[21].Text;
                            TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                            dr["SrlOrder"] = srl.Text;
                            QuotationItems.Rows.Add(dr);
                        }
                    }
                    else
                    {

                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        //dr["Quantity"] = gvrow.Cells[6].Text;
                        TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                        dr["Quantity"] = Quantity.Text;
                        dr["Currency"] = gvrow.Cells[7].Text;
                        //dr["Rate"] = gvrow.Cells[8].Text;
                        TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                        dr["Rate"] = Rate.Text;
                        dr["Discount"] = gvrow.Cells[10].Text;
                        dr["SpPrice"] = gvrow.Cells[11].Text;
                        dr["GSTperc"] = gvrow.Cells[12].Text;
                        dr["GSTRate"] = gvrow.Cells[13].Text;
                        TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                        dr["Room"] = Room.Text;
                        dr["CurrencyId"] = gvrow.Cells[15].Text;
                        dr["Color"] = gvrow.Cells[16].Text;
                        dr["ColorId"] = gvrow.Cells[17].Text;
                        dr["OptId"] = gvrow.Cells[18].Text;
                        dr["ItemType"] = gvrow.Cells[19].Text;
                        dr["Remarks"] = gvrow.Cells[20].Text;
                        TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                        dr["Floor"] = Floor.Text;
                        //  dr["SrlOrder"] = gvrow.Cells[21].Text;
                        TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                        dr["SrlOrder"] = srl.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
            }

            //if (gvQuotationItems.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            //    {
            //        if (gvQuotationItems.SelectedIndex == -1)
            //        {
            //            if (gvrow.Cells[2].Text == ddlEssentials.SelectedItem.Value)
            //            {
            //                gvQuotationItems.DataSource = QuotationItems;
            //                gvQuotationItems.DataBind();
            //                MessageBox.Show(this, "The Item  you have selected is already exists in list");
            //                return;
            //            }
            //        }
            //    }
            //}

            if (gvQuotationItems.SelectedIndex == -1)
            {
                DataRow drnew = QuotationItems.NewRow();
                drnew["ItemCode"] = ddlEssentials.SelectedValue;
                drnew["ModelNo"] = ddlEssentials.SelectedItem.Text;
                drnew["ItemName"] = txtItemName.Text;
                drnew["UOM"] = txtItemUOM.Text;
                drnew["Quantity"] = txtQunatity.Text;
                drnew["Currency"] = ddlRate.SelectedItem.Text;
                drnew["Rate"] = txtRate.Text;

                drnew["Discount"] = txtDiscount.Text;
                drnew["SpPrice"] = txtSpPrice.Text;
                drnew["GSTperc"] = txtGST_Perc.Text;
                drnew["GSTRate"] = txtGST_Amt.Text;
                drnew["Room"] = txtRoom.Text;
                drnew["CurrencyId"] = ddlRate.SelectedItem.Value;

                drnew["Color"] = ddlColor.SelectedItem.Text;
                drnew["ColorId"] = ddlColor.SelectedItem.Value;
                drnew["OptId"] = lblOptId.Text;
                drnew["ItemType"] = "Original";
                drnew["Remarks"] = txtorgRemarks.Text;
                drnew["Floor"] = txtFloor.Text;
                drnew["SrlOrder"] = txtSrlOrderNo.Text;

                QuotationItems.Rows.Add(drnew);
            }
            gvQuotationItems.DataSource = QuotationItems;
            gvQuotationItems.DataBind();
            gvQuotationItems.SelectedIndex = -1;
            btnItemRefresh_Click(sender, e);

        }



    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {

        if (rbIndividual.Checked == true)
        {
            ddlRate.SelectedValue = "1";
            //ddlModelNo.SelectedValue = "0";


        }
        else
        {
            ddlRate.SelectedValue = "0";
            //ddlModelNo.SelectedValue = "0";
            //ddlModelNo.Items.Clear();
        }

        //ddlItemName.SelectedValue = "0";
        ddlColor.SelectedValue = "0";
        txtItemUOM.Text = string.Empty;
        txtQunatity.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtItemSpec.Text = string.Empty;
        txtDiscount.Text = "0";
        txtSpPrice.Text = string.Empty;
        txtRoom.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtColor.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemName.Text = string.Empty;
        gvQuotationItems.SelectedIndex = -1;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        Image2.ImageUrl = "~/Images/noimage400x300.gif";
    }
    #endregion

    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
            //if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible  =e.Row .Cells [18].Visible =e.Row .Cells [19].Visible  = false;

            }
        }
        GridViewRow gvr = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Item Details list?');");
            TextBox rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            e.Row.Cells[9].Text = (Convert.ToDecimal(rate.Text) * Convert.ToDecimal(Qty.Text)).ToString();
            //e.Row.Cells[9].Text = (Convert.ToDecimal(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[6].Text)).ToString();
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            e.Row.Cells[13].Text = ((Convert.ToDecimal(e.Row .Cells [11].Text) * Convert.ToDecimal(e.Row .Cells [12].Text)) / 100).ToString();
            disc.Text = e.Row.Cells[10].Text;
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total Amount:";
            e.Row.Cells[11].Text = TotalAmount.ToString();
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            txttotal.Text = txtsubtotal.Text = GrossAmountCalc().ToString();
            if (txtspldiscount.Text == "") { txtspldiscount.Text = "0"; }
            if (txtspldiscount.Text != "0")
            { txtsubtotal.Text = (Convert.ToDecimal(txttotal.Text) - (Convert.ToDecimal(txttotal.Text) * Convert.ToDecimal(txtspldiscount.Text)) / 100).ToString(); }
            txtsummaryvat.Text = ((Convert.ToDecimal(txtsubtotal.Text) * Convert.ToDecimal(18)) / 100).ToString();
            txtsummaryCst.Text = ((Convert.ToDecimal(txtsubtotal.Text) * Convert.ToDecimal(2)) / 100).ToString();
            txtsummaryGtoalvat.Text = (Convert.ToDecimal(txtsubtotal.Text) + Convert.ToDecimal(txtsummaryvat.Text)).ToString();
            txtsummaryGtotalcst.Text = (Convert.ToDecimal(txtsubtotal.Text) + Convert.ToDecimal(txtsummaryCst.Text)).ToString();
        }
    }
    #endregion

    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }





    #region gvQuotationItems_RowEditing
    protected void gvQuotationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //  ItemTypesAll_Fill();
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Currency");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Discount");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Room");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CurrencyId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["UOM"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Currency"] = gvrow.Cells[7].Text;
                dr["Rate"] = gvrow.Cells[8].Text;
                dr["Discount"] = gvrow.Cells[10].Text;
                dr["SpPrice"] = gvrow.Cells[11].Text;
                dr["Room"] = gvrow.Cells[12].Text;
                dr["CurrencyId"] = gvrow.Cells[13].Text;
                dr["Color"] = gvrow.Cells[14].Text;
                dr["ColorId"] = gvrow.Cells[15].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlModelNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlModelNo_SelectedIndexChanged(sender, e);
                    txtItemUOM.Text = gvrow.Cells[5].Text;
                    txtQunatity.Text = gvrow.Cells[6].Text;
                    ddlRate.SelectedItem.Text = gvrow.Cells[7].Text;
                    txtRate.Text = gvrow.Cells[8].Text;
                    txtDiscount.Text = gvrow.Cells[10].Text;
                    txtSpPrice.Text = gvrow.Cells[11].Text;
                    ddlColor.SelectedValue = gvrow.Cells[15].Text;
                    foreach (GridViewRow gvr in gvSubItems.Rows)
                    {

                        if (gvr.Cells[1].Text == gvrow.Cells[2].Text)
                        {

                            //chklitemcolor.SelectedValue = gvr.Cells[1].Text.ToString();
                            //ListItem currentCheckBox = chklitemcolor.Items.FindByValue(gvr.Cells[2].Text);
                            //if (currentCheckBox != null)
                            //{
                            //    currentCheckBox.Selected = true;
                            //}
                        }
                    }
                    chklitemcolor_SelectedIndexChanged(sender, e);
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region GridView Quotation Items Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        string x2 = gvQuotationItems.Rows[e.RowIndex].Cells[2].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Currency");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Discount");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GSTperc");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("GSTRate");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Room");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CurrencyId");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("OptId");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Floor");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SrlOrder");
        QuotationItems.Columns.Add(col);

        if (gvSubItems.Rows.Count > 0)
        {
            DataTable SubItems = new DataTable();
            DataColumn dc;
            dc = new DataColumn("ITEM_CODE");
            SubItems.Columns.Add(dc);
            dc = new DataColumn("SUBITEM_CODE");
            SubItems.Columns.Add(dc);
            foreach (GridViewRow gvrow in SubItems.Rows)
            {
                if (x2 != gvrow.Cells[0].Text)
                {

                    DataRow dr = SubItems.NewRow();
                    dr["ITEM_CODE"] = gvrow.Cells[1].Text;
                    dr["SUBITEM_CODE"] = gvrow.Cells[2].Text;
                    SubItems.Rows.Add(dr);

                }
            }
            gvSubItems.DataSource = SubItems;
            gvSubItems.DataBind();

        }

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    //dr["Quantity"] = gvrow.Cells[6].Text;
                    TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                    dr["Quantity"] = Quantity.Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    //dr["Rate"] = gvrow.Cells[8].Text;
                    TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                    dr["Rate"] = Rate.Text;
                    dr["Discount"] = gvrow.Cells[10].Text;
                    dr["SpPrice"] = gvrow.Cells[11].Text;
                    dr["GSTperc"] = gvrow.Cells[12].Text;
                    dr["GSTRate"] = gvrow.Cells[13].Text;
                    //dr["Room"] = gvrow.Cells[14].Text;
                    TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                    dr["Room"] = Room.Text;
                    dr["CurrencyId"] = gvrow.Cells[15].Text;
                    dr["Color"] = gvrow.Cells[16].Text;
                    dr["ColorId"] = gvrow.Cells[17].Text;
                    dr["OptId"] = gvrow.Cells[18].Text;
                    dr["ItemType"] = gvrow.Cells[19].Text;
                    dr["Remarks"] = gvrow.Cells[20].Text;
                    //dr["Floor"] = gvrow.Cells[21].Text;
                    TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                    dr["Floor"] = Floor.Text;
                    //  dr["SrlOrder"] = gvrow.Cells[21].Text;
                    TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                    dr["SrlOrder"] = srl.Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region Responsible Person Select Index Changed
    protected void ddlResponsiblePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlResponsiblePerson.SelectedItem.Value) > 0)
            {
                //txtFollowupEmail.Text = objHR.EmpEMail;
                //txtFollowupPhoneNo.Text = objHR.EmpMobile;
                txtFollowupEmail.Text = objHR.AssignedEmailId;
                txtFollowupPhoneNo.Text = objHR.AssignedMobileNo;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // HR.Dispose();
        }

    }
    #endregion

    #region GridView Quotation Details Row DataBound
    protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[9].Visible = false;
            //if (!string.IsNullOrEmpty(e.Row.Cells[9].Text) && e.Row.Cells[9].Text != "&nbsp;")
            //{             }
        }
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //if (rbIndividual.Checked == true)
        //{
        //    //tblDiscountSelect.Visible = true;
        //    tblPrint.Visible = true;
        //}
        //else
        //{
        //    tblDiscountSelect.Visible = false;
        //}
        //if (rbProject.Checked == true)
        //{
        //    tblPrint.Visible = true;
        //}
        //else
        //{
        //   // tblPrint.Visible = false;
        //}
        tblDiscountSelect.Visible = true;

        //if (gvQuotationDetails.SelectedIndex > -1)
        //{
        //    string pagenavigationstr = "";
        //if (((Button)sender).Text == "Print")
        //{
        //    pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "";
        //}
        //    else if (((Button)sender).Text == "Print T.B.")
        //    {
        //        pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quottb&qno=" + Request.QueryString["QuoId"].ToString() + "";
        //    }
        //    else if (((Button)sender).Text == "Print C.B.")
        //    {
        //        pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotcb&qno=" + Request.QueryString["QuoId"].ToString() + "";
        //    }
        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please select atleast a Record");
        //}
    }
    #endregion











    #region Send Button Click
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["QuoId"] != null)
        {
            string pagenavigationstr = "../Reports/Email.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "" +
                "&qnono=" + ddlCustomer.SelectedItem.Text + "" +
                "&empid=" + Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId) + "" +
                "&custemail=" + txtEmail.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Follow Up Button Click
    protected void btnFollowUp_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["QuoId"] != null)
        {

            gvFollowUp.DataBind();
            tblFollowUpHistory.Visible = false;
            txtFollowUpName.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpName];
            if (tblFollowUp.Visible == false)
            {
                tblFollowUp.Visible = true;
            }
            else if (tblFollowUp.Visible == true)
            {
                tblFollowUp.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    #region Button FOLLOW UP CLOSE Click
    protected void btnFollowUpClose_Click(object sender, EventArgs e)
    {
        tblFollowUp.Visible = false;
        SM.ClearControls(this);

    }
    #endregion

    #region Button FOLLOW UP SAVE Click
    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        try
        {
            SM.SalesQuotation objSMQuotAssign = new SM.SalesQuotation();
            SM.BeginTransaction();
            objSMQuotAssign.QuotId = Request.QueryString["QuoId"].ToString();
            objSMQuotAssign.FollowUpEmpId = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMQuotAssign.FollowUpDesc = txtFollowUpDesc.Text;
            objSMQuotAssign.FollowUpDate = DateTime.Now.ToString();

            objSMQuotAssign.FollowUpTechDiss = ddlTechnicalDiscussions.SelectedValue;
            objSMQuotAssign.FollowUpCommNegos = ddlCommercialNegociations.SelectedValue;
            objSMQuotAssign.FollowUpCompExistance = ddlCompetatorsExistance.SelectedValue;
            objSMQuotAssign.FollowUpRemarks = txtRemarks.Text;
            objSMQuotAssign.FollowUpExpDate = Yantra.Classes.General.toMMDDYYYY(txtExpectedFwpDate.Text);

            MessageBox.Show(this, objSMQuotAssign.SalesQuotationFollowUp_Save());
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFollowUp.DataBind();
            txtFollowUpDesc.Text = string.Empty;
            ddlTechnicalDiscussions.SelectedValue = "--";
            ddlCommercialNegociations.SelectedValue = "--";
            ddlCompetatorsExistance.SelectedValue = "--";
            txtRemarks.Text = string.Empty;
            txtExpectedFwpDate.Text = string.Empty;
            // SM.Dispose();
        }
    }
    #endregion

    #region Button FOLLOW UP REFRESH Click
    protected void btnFollowUpRefresh_Click(object sender, EventArgs e)
    {
        txtFollowUpDesc.Text = string.Empty;
        txtFollowUpName.Text = string.Empty;
        //if (ddlTechnicalDiscussions.SelectedValue == "Open")
        //{
        //    ddlTechnicalDiscussions.SelectedValue = "0";

        //}
        //else
        //{
        //    ddlTechnicalDiscussions.SelectedValue = "Open";
        //}
        //if (ddlTechnicalDiscussions.SelectedValue == "Closed")
        //{
        //    ddlTechnicalDiscussions.SelectedValue = "0";
        //}
        //else
        {
            ddlTechnicalDiscussions.SelectedValue = "Closed";
        }
        ddlCommercialNegociations.SelectedValue = "0";
        //ddlCompetatorsExistance.SelectedValue = "0";
        txtRemarks.Text = string.Empty;
        txtExpectedFwpDate.Text = string.Empty;
        gvFollowUp.SelectedIndex = -1;
    }
    #endregion

    #region Button FOLLOW UP HISTORY Click
    protected void btnFollowUpHistory_Click(object sender, EventArgs e)
    {
        if (tblFollowUpHistory.Visible == false)
        {
            tblFollowUpHistory.Visible = true;
        }
        else if (tblFollowUpHistory.Visible == true)
        {
            tblFollowUpHistory.Visible = false;
        }
    }
    #endregion

    #region Button CONFIRM YES Click
    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {


        //if (((Button)sender).Text == "Yes")
        //{
        //    btnSend_Click(sender, e);
        //}
    }
    #endregion

    #region Button REVISE Click
    protected void btnRevise_Click(object sender, EventArgs e)
    {
        if (btnRevise.Text == "Modify")
        {
            btnRevise.Text = "Revise";
            btnSave.Enabled = true;
            btnEdit_Click(sender, e);
        }
        else if (btnRevise.Text == "Revise")
        {
            if (gvQuotationItems.Rows.Count > 0)
            {
                if (txtFOB.Text == "") { txtFOB.Text = "0"; }
                if (txtCIF.Text == "") { txtCIF.Text = "0"; }
                try
                {
                    SM.SalesQuotation objSM = new SM.SalesQuotation();
                    SM.BeginTransaction();
                    objSM.EnqId = ddlEnquiryNo.SelectedItem.Value;
                    objSM.QuotId = Request.QueryString["QuoId"].ToString();
                    objSM.QuotNo = txtQuotationNo.Text;
                    objSM.QuotDate = Yantra.Classes.General.toMMDDYYYY(DateTime.Now.ToString("dd/MM/yyyy"));
                    objSM.QuotDelivery = txtDelivery.Text;
                    objSM.QuotPayTerms = txtPaymentTerms.Text;
                    objSM.QuotPackCharges = txtPackingCharges.Text;
                    objSM.QuotExcise = txtExciseDuty.Text;
                    //objSM.QuotCST = txtCST.Text;
                    //objSM.QuotVAT = txtVAT.Text;
                    if (rbVAT.Checked == true)
                    { objSM.QuotVAT = txtVAT.Text; }
                    else if (rbCST.Checked == true)
                    { objSM.QuotCST = txtVAT.Text; }
                    else if (rbincluding.Checked == true)
                    { objSM.QuotIncluding = txtVAT.Text; }

                    objSM.DespmId = ddlDespatchMode.SelectedItem.Value;
                    objSM.QuotGuarantee = txtGuarantee.Text;
                    objSM.QuotTransCharges = txtTransCharges.Text;
                    objSM.QuotInsurance = txtInsurance.Text;
                    objSM.QuotErrec = txtErrection.Text;
                    objSM.QuotJurisdiction = txtJurisdiction.Text;
                    objSM.QuotValidity = txtValidity.Text;
                    objSM.QuotInspection = txtInspection.Text;
                    objSM.QuotOtherSpecs = txtOtherSpecs.Text;
                    //   objSM.QuotPOLog = txtpo.Text;
                    objSM.QuotRespId = ddlResponsiblePerson.SelectedItem.Value;
                    objSM.QuotSalespId = ddlSalesPerson.SelectedItem.Value;
                    objSM.QuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    objSM.QuotCheckedBy = ddlCheckedBy.SelectedItem.Value;
                    objSM.QuotApprovedBy = "0";
                    objSM.CurrencyId = ddlCurrencyType.SelectedItem.Value;

                    objSM.QuotDDNo = txtDDNo.Text;
                    objSM.QuotDDDate = Yantra.Classes.General.toMMDDYYYY(txtDDDate.Text);
                    objSM.QuotBankName = txtBankName.Text;
                    objSM.IsExpectedOrder = chkIsExpectedOrder.Checked;
                    objSM.QuotTotalEMDCharges = txtEMDCharges.Text;
                    objSM.QuotFOB = txtFOB.Text;
                    objSM.QuotCIF = txtCIF.Text;
                    objSM.QuotCompany = ddlCompany.SelectedItem.Value;
                    objSM.ttlDisc = txtspldiscount.Text;
                    if (rbIndividual.Checked == true)
                    {
                        objSM.QuotType = "Discount";
                    }
                    else
                    {
                        objSM.QuotType = "Special";
                    }

                    objSM.Cp_Id = lblCPID.Text;
                    if (objSM.SalesQuotationRevise_Save() == "Data Saved Successfully")
                    {
                        objSM.SalesQuotationDetails_Delete(objSM.QuotId);
                        foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                        {

                            objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                            TextBox Quantity = (TextBox)gvrow.FindControl("txtDetQty");
                            objSM.QuotDetQty = Quantity.Text;
                            //objSM.QuotDetQty = gvrow.Cells[6].Text;
                            TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                            objSM.QuotRate = Rate.Text;
                            //objSM.QuotRate = gvrow.Cells[8].Text;
                            objSM.QuotDetDisc = gvrow.Cells[10].Text;
                            objSM.QuotDetSpPrice = gvrow.Cells[11].Text;
                            objSM.QuotGSTperc = gvrow.Cells[12].Text;
                            objSM.QuotGSTRate = gvrow.Cells[13].Text;
                            TextBox Room = (TextBox)gvrow.FindControl("txtDetRoom");
                            objSM.QuotRoom = Room.Text;
                            //objSM.QuotRoom = gvrow.Cells[14].Text;
                            objSM.QuotCurrency = gvrow.Cells[15].Text;
                            objSM.ColorId = gvrow.Cells[17].Text;
                            objSM.OptionalId = gvrow.Cells[18].Text;
                            objSM.Remarks = gvrow.Cells[20].Text;
                            objSM.SrlOrder = gvrow.Cells[22].Text;

                            objSM.Itemtype = "Original";
                            TextBox Floor = (TextBox)gvrow.FindControl("txtDetFloor");
                            objSM.Floor = Floor.Text;
                            //objSM.Floor = gvrow.Cells[19].Text;
                            // objSM.SrlOrder = gvrow.Cells[21].Text;
                            TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");
                            objSM.SrlOrder = srl.Text;

                            objSM.SalesQuotationDetails_Save();

                        }
                        objSM.SalesQuotationDetails_DeleteSubItems(objSM.QuotId);
                        foreach (GridViewRow gvr in gvSubItems.Rows)
                        {
                            objSM.QuotItemCodeMain = gvr.Cells[1].Text;
                            objSM.QuotSubItemCode = gvr.Cells[2].Text;

                            objSM.SalesQuotationDetailsSubItems_Save();
                        }

                        SM.CommitTransaction();
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
                    else
                    {
                        SM.RollBackTransaction();
                    }
                }
                catch (Exception ex)
                {
                    SM.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                    //gvQuotationDetails.DataBind();
                }
                finally
                {
                    btnDelete.Attributes.Clear();
                    //gvQuotationDetails.DataBind();
                    // gvEnquiryProducts.DataBind();
                    gvQuotationItems.DataBind();
                    tblQuotationDetails.Visible = false;
                    SM.ClearControls(this);
                    Response.Redirect("SalesQuotation.aspx");
                    // SM.Dispose();
                }
                //gvQuotationDetails.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(this, "Please add atleast one Item for Quotation");
            }
        }
    }
    #endregion

    #region ddlCompetatorsExistance_SelectedIndexChanged
    protected void ddlCompetatorsExistance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCompetatorsExistance.SelectedItem.Text == "Exist")
            {
                tdRemarksField.Visible = true;
                tdRemarkslbl.Visible = true;

                tdOrderExpDateField.Visible = false;
                tdOrderExpDatelbl.Visible = false;
            }
            else if (ddlCompetatorsExistance.SelectedItem.Text == "Does Not Exist")
            {
                tdRemarksField.Visible = false;
                tdRemarkslbl.Visible = false;

                tdOrderExpDateField.Visible = true;
                tdOrderExpDatelbl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    #endregion

    #region gvFollowUp_RowDataBound
    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "01/01/1900")
            {
                e.Row.Cells[7].Text = "";
            }


        }
    }
    #endregion

    #region Send To Sales Order
    protected void btnSalesOrder_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["QuoId"] != null)
        {
            Response.Redirect("SalesOrder.aspx?qId=" + Request.QueryString["QuoId"].ToString() + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }
    }
    #endregion

    #region Button REGRET
    protected void btnRegret_Click(object sender, EventArgs e)
    {
        try
        {
            SM.SalesQuotation objSMQuotApprove = new SM.SalesQuotation();
            SM.BeginTransaction();
            objSMQuotApprove.QuotId = Request.QueryString["QuoId"].ToString();
            objSMQuotApprove.QuotApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objSMQuotApprove.SalesQuotationRegret_Update();
            if (objSMQuotApprove.Get_Ids_Select(Request.QueryString["QuoId"].ToString()) > 0)
            {
                SM.SalesEnquiry.SalesEnquiryStatus_Update(SM.SMStatus.Obsolete, objSMQuotApprove.EnqId);
                SM.SalesAssignments.SalesAssignmentsStatus_Update(SM.SMStatus.Obsolete, objSMQuotApprove.AssignTaskId);
            }
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvQuotationDetails.DataBind();
            // SM.Dispose();
            btnEdit_Click(sender, e);
        }
    }
    #endregion

    #region Button APPROVE
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {

            SM.SalesQuotation objSMQuotApprove = new SM.SalesQuotation();
            //SM.BeginTransaction();
            objSMQuotApprove.QuotId = Request.QueryString["QuoId"].ToString();
            objSMQuotApprove.QuotApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            Request.QueryString["Status"] = "Open";
            objSMQuotApprove.QuotPOLog = Request.QueryString["Status"].ToString();

            if (objSMQuotApprove.SalesQuotationApprove_Update() == "Status Updated Successfully")
                MessageBox.Show(this, "Quotation Approved Successfully");
            //  objSMQuotApprove.SalesQuotationApprove_Update();

            if (objSMQuotApprove.Get_Ids_Select(Request.QueryString["QuoId"].ToString()) > 0)
            {
                SM.SalesEnquiry.SalesEnquiryStatus_Update(SM.SMStatus.Open, objSMQuotApprove.EnqId);
                SM.SalesAssignments.SalesAssignmentsStatus_Update(SM.SMStatus.Open, objSMQuotApprove.AssignTaskId);
            }
            //gvQuotationDetails.SelectedRow.Cells[8].Text = "Open";
            //objSMQuotApprove.QuotPOLog = gvQuotationDetails.SelectedRow.Cells[8].Text;
            SM.CommitTransaction();
        }
        catch (Exception ex)
        {
            //  SM.Dispose();
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            //  SM.Dispose();
            //gvQuotationDetails.DataBind();
            btnEdit_Click(sender, e);
        }
        //ModalPopupExtender.Show();
    }
    #endregion

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                rfvContactPerson.Enabled = true;
                rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                rfvContactPerson.Enabled = false;
                rfvUnitName.Enabled = false;
                lblUnitAddress.Text = "Customer Address";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                    txtUnitAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtPhoneNo.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
            }
            // SM.Dispose();

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            // SM.Dispose();
        }
        finally
        {
        }

    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtPhoneNo.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
            }
            //SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            // SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlContactPerson_SelectedIndexChanged
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtEmail.Text = objSMCustomer.CustCorpEmail;
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
                txtMobile.Text = objSMCustomer.CustCorpMobile;
            }
            // SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            //SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion


    #region DDL model No change

    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            string itemcode = "";
            if (rdbOnlyfromLead.Checked == true)
            {
                itemcode = getItemCode(ddlModelNo.SelectedItem.Value);
                lblItemCode.Text = itemcode;
            }
            else if (rdbAll.Checked == true)
            {
                itemcode = ddlModelNo.SelectedValue;
                lblItemCode.Text = itemcode;
            }

            if (objMaster.ItemMaster_Select(itemcode) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
                Image2.ImageUrl = "~/Content/ItemDrawings/" + objMaster.itemSpecifcation;
                txtRoom.Text = string.Empty;
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(itemcode) > 0)
                {
                    txtRate.Text = objrate.rsp;
                }
                SM.SalesEnquiry obj = new SM.SalesEnquiry();
                if (obj.EnqDetails_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo.SelectedItem.Value) > 0)
                {

                    txtRoom.Text = obj.EnqDetRoom; txtQunatity.Text = obj.EnqDetQty;
                    if (objrate.rsp == null)
                    {
                        txtRate.Text = Convert.ToString(0);
                        txtSpPrice.Text = (Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQunatity.Text)).ToString();
                    }
                    else
                    {
                        txtSpPrice.Text = (Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtQunatity.Text)).ToString();
                    }

                }

                txtGST_Perc.Text = objMaster.GST_Tax;
                if (txtGST_Perc.Text == "" || txtGST_Perc.Text == null)
                {
                    txtGST_Perc.Text = "0";
                }

                if (txtSpPrice.Text == "" || txtSpPrice.Text == null)
                {
                    txtSpPrice.Text = "0";
                }

                txtGST_Amt.Text = ((Convert.ToDecimal(txtGST_Perc.Text) * Convert.ToDecimal(txtSpPrice.Text)) / 100).ToString();
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, itemcode);

            Types_Fill();
            ddlEssentials.Enabled = true;
            Masters.ItemMaster.CheckboxListWithStatement(chklitemcolor, "SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),[YANTRA_ITEM_MAST].ITEM_MODEL_NO FROM [YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST]  WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE  = [YANTRA_ITEM_MAST].ITEM_CODE  and [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=" + itemcode + "");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //  Masters.Dispose();
        }
    }
    private string getItemCode(string ENQ_DET_ID)
    {
        string itemcode;

        itemcode = dbc.get_column_data("ITEM_CODE", "ENQ_DET_ID", Convert.ToInt32(ENQ_DET_ID), "YANTRA_ENQ_DET");

        return itemcode;
    }
    #endregion

    #region Radiobutton Project Checked Change
    protected void rbProject_CheckedChanged(object sender, EventArgs e)
    {
        if (rbProject.Checked == true)
        {
            ddlEssentials.Enabled = false;
            ddlRate.SelectedValue = "1";
            ddlRate.Enabled = true;
            //ddlModelNo.SelectedValue = "0";
            txtItemUOM.Text = string.Empty;
            txtQunatity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtItemSpec.Text = string.Empty;
            //txtDiscount.Text = string.Empty;
            txtSpPrice.Text = string.Empty;
            txtRoom.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtItemSubCategory.Text = string.Empty;
            txtItemCategory.Text = string.Empty;
            txtItemName.Text = string.Empty;
            gvQuotationItems.SelectedIndex = -1;
            Image1.ImageUrl = "~/Images/noimage400x300.gif";
            Image2.ImageUrl = "~/Images/noimage400x300.gif";
            tblDiscountSelect.Visible = false;

        }
        else
        {
            ddlEssentials.Enabled = true;
            lblEssentials.Visible = true;
            ddlRate.SelectedValue = "1";
            ddlRate.Enabled = false;
            //ddlModelNo.SelectedValue = "0";
            txtItemUOM.Text = string.Empty;
            txtQunatity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtItemSpec.Text = string.Empty;
            //txtDiscount.Text = string.Empty;
            txtSpPrice.Text = string.Empty;
            txtRoom.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtItemSubCategory.Text = string.Empty;
            txtItemCategory.Text = string.Empty;
            txtItemName.Text = string.Empty;
            gvQuotationItems.SelectedIndex = -1;
            Image1.ImageUrl = "~/Images/noimage400x300.gif";
            Image2.ImageUrl = "~/Images/noimage400x300.gif";
            tblPrint.Visible = false;

        }

    }
    #endregion

    #region ddl Essentials Change
    protected void ddlEssentials_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlEssentials.SelectedItem.Value) > 0)
            {
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtItemSpec.Text = objMaster.ItemSpec;
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                // txtItemSpec.Text = objMaster.ItemSpec;
                txtColor.Text = objMaster.Color;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
                Image1.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
                Image2.ImageUrl = "~/Content/ItemDrawings/" + objMaster.itemSpecifcation;
                //txtRoom.Text = objMaster.Room;
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(ddlModelNo.SelectedItem.Value) > 0)
                {
                    txtRate.Text = objrate.rsp;
                }



            }

            SM.SalesEnquiry obj = new SM.SalesEnquiry();
            if (obj.EnqDetails_Select(ddlModelNo.SelectedItem.Value, ddlModelNo.SelectedItem.Value) > 0)
            {
                txtRoom.Text = obj.EnqDetRoom;

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

    #region Rdb All change
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        //for clearing the fields when click on this radio button
        ddlModelNo.ClearSelection();
        btnItemRefresh_Click(sender, e);
        //####
        lblSearch.Visible = true;
        txtSearchModel.Visible = true;
        btnSearchModelNo.Visible = true;
        lblSearchBrand.Visible = true;
        ddlBrand.Visible = true;
        ddlModelNo.DataSource = null;
        ddlModelNo.DataBind();
        Masters.ProductCompany.ProductCompany_Select(ddlBrand);
    }
    #endregion

    #region RDB OnlyFrom Lead
    protected void rdbOnlyfromLead_CheckedChanged(object sender, EventArgs e)
    {
        //for clearing the fields when click on this radio button
        // btnItemRefresh_Click(sender, e);
        //#####
        if (rdbOnlyfromLead.Checked == true)
        {
            ItemTypes_Fill();
            lblSearch.Visible = false;
            txtSearchModel.Visible = false;
            btnSearchModelNo.Visible = false;
            //lblSearchBrand.Visible = false;
            //ddlBrand.Visible = false;
            lblSearchBrand.Visible = true;
            ddlBrand.Visible = true;
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);



        }
    }

    private void ItemTypes_Fill()
    {
        try
        {
            SM.SalesOrder.SalesQuatation_Select(ddlEnquiryNo.SelectedItem.Value, ddlModelNo);
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

    protected void rdbPert_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbPert.Checked == true)
        {

            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "";
            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwinp&qno=" + Request.QueryString["QuoId"].ToString() + "";
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PertQuot&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }

        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }

    protected void rdbDiscountPrice_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbDiscountPrice.Checked == true)
        {

            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "";
            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwinp&qno=" + Request.QueryString["QuoId"].ToString() + "";
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwinp&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }

        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }


    protected void rdbProject_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbProject.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quottb&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }

    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbSpecialPrice.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotcb&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }



    //private void Print()
    //{
    //    if (gvQuotationDetails.SelectedIndex > -1)
    //    {
    //        string pagenavigationstr = "";
    //        if (((RadioButton)sender).Text == "Discount Price")
    //        {
    //            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "";
    //        }
    //        else if (((RadioButton)sender).Text == "Project Price")
    //        {
    //            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quottb&qno=" + Request.QueryString["QuoId"].ToString() + "";
    //        }
    //        else if (((RadioButton)sender).Text == "Print C.B.")
    //        {
    //            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotcb&qno=" + Request.QueryString["QuoId"].ToString() + "";
    //        }
    //        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}

    protected void gvEnquiryProducts_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;

            if (tdEMDHeader.Visible == false)
            {
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
            }


        }
    }
    protected void rdbDiscountWithoutDrawings_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbDiscountWithoutDrawings.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=T3&qno=" + Request.QueryString["QuoId"].ToString() + "";

            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwd&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbWithoutRatesDiscount_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbWithoutRatesDiscount.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=T2&qno=" + Request.QueryString["QuoId"].ToString() + "";

            // pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwr&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbSpWithDrawings_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbSpWithDrawings.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotspwd&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbPrDrawings_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbPrDrawings.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotppwd&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }

    protected void rdbWithoutModelnoandDrwings_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbWithoutModelnoandDrwings.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwimrp&qno=" + Request.QueryString["QuoId"].ToString() + "";



            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwdmn&qno=" + Request.QueryString["QuoId"].ToString() + "";
            // pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Copyquotwiwithoutnorpricie&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbWithoutratesandMn_CheckedChanged(object sender, EventArgs e)
    {

        string pagenavigationstr = "";

        if (rdbWithoutratesandMn.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Q3&qno=" + Request.QueryString["QuoId"].ToString() + "";

            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwrMn&qno=" + Request.QueryString["QuoId"].ToString() + "";
            // pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Copyquotwiwithsprpricie&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbProjectpriceMn_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbProjectpriceMn.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quottbmn&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbProjectdrawingsandmn_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbProjectdrawingsandmn.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotppwdmn&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbspecialpricemn_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbspecialpricemn.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotcbmn&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbspwdmn_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbspwdmn.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotspwdmn&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbDiscountPriceNOModelno_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbDiscountPriceNOModelno.Checked == true)
        {
            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwithoutMN&qno=" + Request.QueryString["QuoId"].ToString() + "";
            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Copyquotwinp&qno=" + Request.QueryString["QuoId"].ToString() + "";
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwinpnor&qno=" + Request.QueryString["QuoId"].ToString() + "";

            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
    }
    protected void chklitemcolor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string totSpec = "";
        decimal sumRate = 0;
        for (int i = 0; i < chklitemcolor.Items.Count; i++)
        {
            if (chklitemcolor.Items[i].Selected == true)
            {

                string str = chklitemcolor.Items[i].Value;
                SM.SalesQuotation objsq = new SM.SalesQuotation();
                decimal rate;
                rate = objsq.SalesQuotationDetails_SubItemsRate(str);
                string Spec = objsq.ItemSpec;
                if (totSpec.Length > 1)
                {
                    totSpec = totSpec + " , " + Spec;
                }
                else
                {
                    totSpec = totSpec + Spec;
                }
                sumRate = sumRate + rate;
            }
            //else if (chklitemcolor.Items[i].Selected != true)
            //{

            //    return;
            //}

        }
        txtRate.Text = sumRate.ToString();
        txtItemSpec.Text = totSpec.ToString();
    }

    protected void gvSubItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvSubItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable SubItems = new DataTable();
        DataColumn dc;
        dc = new DataColumn("ITEM_CODE");
        SubItems.Columns.Add(dc);
        dc = new DataColumn("SUBITEM_CODE");
        SubItems.Columns.Add(dc);
        foreach (GridViewRow gvrow in gvSubItems.Rows)
        {
            if (gvrow.RowIndex != e.RowIndex)
            {

                DataRow dr = SubItems.NewRow();
                dr["ITEM_CODE"] = gvrow.Cells[1].Text;
                dr["SUBITEM_CODE"] = gvrow.Cells[2].Text;
                SubItems.Rows.Add(dr);

            }
        }
        gvSubItems.DataSource = SubItems;
        gvSubItems.DataBind();
    }

    protected void gvSubItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Item Details list?');");
        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }
    protected void rdbWithoutImagesDr_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbWithoutImagesDr.Checked == true)
        {

            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwdimg&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void rdbWithoutDrawingsmn_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbWithoutDrawingsmn.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Q4&qno=" + Request.QueryString["QuoId"].ToString() + "";

            // pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwdimgmn&qno=" + Request.QueryString["QuoId"].ToString() + "";
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbAll.Checked == true)
        {
            Masters.ItemMaster.ItemMaster5_Select(ddlModelNo, ddlBrand.SelectedItem.Value);
        }
        else if (rdbOnlyfromLead.Checked == true)
        {
            Masters.ItemMaster.ItemMaster7_Select(ddlModelNo, ddlBrand.SelectedItem.Value, ddlEnquiryNo.SelectedItem.Value);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SM.QuotApprove obj = new SM.QuotApprove();
        obj.quotid = Request.QueryString["QuoId"].ToString();
        obj.flag = "Open";
        obj.approved = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        MessageBox.Show(this, obj.quotapprove());
        btnEdit_Click(sender, e);
        // gvQuotationDetails.DataBind();

    }


    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvQuotationDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);

        //gvQuotationDetails.DataBind();
    }


    protected void btnOptSearch_Click(object sender, EventArgs e)
    {
        ddlOptModelNo.DataSourceID = "SqlDataSourceopt";
        ddlOptModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlOptModelNo.DataValueField = "ITEM_CODE";
        ddlOptModelNo.DataBind();
        ddlOptModelNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlOptBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbnOptAll.Checked == true)
        {
            Masters.ItemMaster.ItemMaster5_Select(ddlOptModelNo, ddlOptBrand.SelectedItem.Value);
        }
        else if (rbnOptFromLead.Checked == true)
        {
            Masters.ItemMaster.ItemMaster7_Select(ddlOptModelNo, ddlOptBrand.SelectedItem.Value, ddlEnquiryNo.SelectedItem.Value);

        }
    }
    protected void rbnOptAll_CheckedChanged(object sender, EventArgs e)
    {
        //for clearing the fields when click on this radio button
        ddlOptModelNo.ClearSelection();
        btnOptRefresh_Click(sender, e);
        //####
        lblOptSearch.Visible = true;
        txtOptSearch.Visible = true;
        btnOptSearch.Visible = true;
        lblOptBrand.Visible = true;
        ddlOptBrand.Visible = true;
        ddlOptModelNo.DataSource = null;
        ddlOptModelNo.DataBind();
        Masters.ProductCompany.ProductCompany_Select(ddlOptBrand);

    }
    protected void rbnOptFromLead_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ddlOptModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlOptModelNo.SelectedItem.Value) > 0)
            {
                txtOptItemUOM.Text = objMaster.ItemUOMShort;
                txtOptItemSpec.Text = objMaster.ItemSpec;
                txtOptItemCat.Text = objMaster.ItemCategoryName;
                txtOptItemName.Text = objMaster.ItemName;
                txtOptColor.Text = objMaster.Color;
                txtOptBrand.Text = objMaster.BrandProductName;
                txtOptItemSubCat.Text = objMaster.ItemType;
                OptImage3.ImageUrl = "~/Content/ItemImage/" + objMaster.ItemImage;
                OptImage4.ImageUrl = "~/Content/ItemDrawings/" + objMaster.itemSpecifcation;
                txtRoom.Text = "-";
                Masters.ItemPurchase objrate = new Masters.ItemPurchase();
                if (objrate.ItemPrice_Ddl(ddlOptModelNo.SelectedItem.Value) > 0)
                {
                    txtOptRate.Text = objrate.rsp;
                }
                SM.SalesEnquiry obj = new SM.SalesEnquiry();
                if (obj.EnqDetails_Select(ddlEnquiryNo.SelectedItem.Value, ddlOptModelNo.SelectedItem.Value) > 0)
                {

                    txtOptRoom.Text = obj.EnqDetRoom; txtOptQty.Text = obj.EnqDetQty;
                    if (objrate.rsp == null)
                    {
                        txtOptRate.Text = Convert.ToString(0);
                        txtOptSpPrice.Text = (Convert.ToDecimal(txtOptRate.Text) * Convert.ToDecimal(txtOptQty.Text)).ToString();
                    }
                    else
                    {
                        txtOptSpPrice.Text = (Convert.ToDecimal(txtOptRate.Text) * Convert.ToDecimal(txtOptQty.Text)).ToString();
                    }

                }
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlOptColor, ddlOptModelNo.SelectedValue);

            Types1_Fill();
            ddlEssentials.Enabled = true;
            Masters.ItemMaster.CheckboxListWithStatement(chkOptItemColor, "SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),[YANTRA_ITEM_MAST].ITEM_MODEL_NO FROM [YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST]  WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE  = [YANTRA_ITEM_MAST].ITEM_CODE  and [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=" + ddlOptModelNo.SelectedValue + "");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //  Masters.Dispose();
        }
    }

    private void Types1_Fill()
    {
        try
        {
            //Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
            SM.SalesEnquiry.SalesEnquiryItemTypes2_Select(ddlOptModelNo.SelectedItem.Value, ddlOptEssentials);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();            
            //  SM.Dispose();
        }
    }
    protected void chkOptItemColor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlOptEssentials_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #region Optional Items Add
    protected void btnOptAdd_Click(object sender, EventArgs e)
    {
        if (txtOptRoom.Text == "") { txtOptRoom.Text = "--"; }
        if (ddlOptColor.SelectedItem.Value == "")
        {
            ddlOptColor.SelectedItem.Value = "0";
            ddlOptColor.SelectedItem.Text = "-";
        }


        if (ddlOptEssentials.SelectedValue == "0")
        {
            ddlOptEssentials.SelectedItem.Value = ddlOptModelNo.SelectedItem.Value;
            ddlOptEssentials.SelectedItem.Text = ddlOptModelNo.SelectedItem.Text;
        }
        DataTable SubItems = new DataTable();
        DataColumn dc;
        dc = new DataColumn("ITEM_CODE");
        SubItems.Columns.Add(dc);
        dc = new DataColumn("SUBITEM_CODE");
        SubItems.Columns.Add(dc);

        if (gvOptSubItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOptSubItems.Rows)
            {
                DataRow dr = SubItems.NewRow();
                dr["ITEM_CODE"] = gvrow.Cells[1].Text;
                dr["SUBITEM_CODE"] = gvrow.Cells[2].Text;
                SubItems.Rows.Add(dr);
            }
        }
        for (int i = 0; i < chkOptItemColor.Items.Count; i++)
        {
            if (chkOptItemColor.Items[i].Selected == true)
            {
                DataRow dr = SubItems.NewRow();
                dr["ITEM_CODE"] = ddlModelNo.SelectedValue;
                dr["SUBITEM_CODE"] = chkOptItemColor.Items[i].Value;
                SubItems.Rows.Add(dr);
            }
            //else if (chklitemcolor.Items[i].Selected != true)
            //{

            //    return;
            //}

        }
        gvOptSubItems.DataSource = SubItems;
        gvOptSubItems.DataBind();
        chkOptItemColor.Items.Clear();

        if (rbProject.Checked == true)
        {
            DataTable QuotationItems = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("ItemCode");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ModelNo");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ItemName");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("UOM");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Quantity");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Currency");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Rate");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Discount");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("SpPrice");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Room");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("CurrencyId");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Color");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ColorId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("OptId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Remarks");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("OptSrlOrder");
            QuotationItems.Columns.Add(col);

            if (gvOptQuatationItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvOptQuatationItems.Rows)
                {
                    if (gvOptQuatationItems.SelectedIndex > -1)
                    {
                        if (gvrow.RowIndex == gvOptQuatationItems.SelectedRow.RowIndex)
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = ddlOptModelNo.SelectedItem.Value;
                            dr["ModelNo"] = ddlOptModelNo.SelectedItem.Text;
                            dr["ItemName"] = txtOptItemName.Text;
                            dr["UOM"] = txtOptItemUOM.Text;
                            dr["Quantity"] = txtOptQty.Text;
                            dr["Currency"] = ddlOptRate.SelectedItem.Text;
                            dr["Rate"] = txtOptRate.Text;

                            dr["Discount"] = txtOptDisc.Text;
                            dr["SpPrice"] = txtOptSpPrice.Text;
                            dr["Room"] = txtOptRoom.Text;
                            dr["CurrencyId"] = ddlOptRate.SelectedItem.Value;

                            dr["Color"] = ddlOptColor.SelectedItem.Text;
                            dr["ColorId"] = ddlOptColor.SelectedItem.Value;
                            dr["OptId"] = lblOptId.Text;
                            dr["Remarks"] = txtOptRemarks.Text;
                            dr["OptSrlOrder"] = txtOptSrlOrder.Text;

                            QuotationItems.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = gvrow.Cells[2].Text;
                            dr["ModelNo"] = gvrow.Cells[3].Text;
                            dr["ItemName"] = gvrow.Cells[4].Text;
                            dr["UOM"] = gvrow.Cells[5].Text;
                            dr["Quantity"] = gvrow.Cells[6].Text;
                            dr["Currency"] = gvrow.Cells[7].Text;
                            dr["Rate"] = gvrow.Cells[8].Text;

                            dr["Discount"] = gvrow.Cells[10].Text;
                            dr["SpPrice"] = gvrow.Cells[11].Text;
                            dr["Room"] = gvrow.Cells[12].Text;
                            dr["CurrencyId"] = gvrow.Cells[13].Text;

                            dr["Color"] = gvrow.Cells[14].Text;
                            dr["ColorId"] = gvrow.Cells[15].Text;
                            dr["OptId"] = gvrow.Cells[16].Text;
                            dr["Remarks"] = gvrow.Cells[17].Text;
                            dr["OptSrlOrder"] = gvrow.Cells[18].Text;

                            QuotationItems.Rows.Add(dr);
                        }
                    }
                    else
                    {

                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Currency"] = gvrow.Cells[7].Text;
                        dr["Rate"] = gvrow.Cells[8].Text;

                        dr["Discount"] = gvrow.Cells[10].Text;
                        dr["SpPrice"] = gvrow.Cells[11].Text;
                        dr["Room"] = gvrow.Cells[12].Text;
                        dr["CurrencyId"] = gvrow.Cells[13].Text;

                        dr["Color"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;
                        dr["OptId"] = gvrow.Cells[16].Text;
                        dr["Remarks"] = gvrow.Cells[17].Text;
                        dr["OptSrlOrder"] = gvrow.Cells[18].Text;

                        QuotationItems.Rows.Add(dr);
                    }
                }
            }



            if (gvOptQuatationItems.SelectedIndex == -1)
            {
                DataRow drnew = QuotationItems.NewRow();
                drnew["ItemCode"] = ddlOptModelNo.SelectedItem.Value;
                drnew["ModelNo"] = ddlOptModelNo.SelectedItem.Text;
                drnew["ItemName"] = txtOptItemName.Text;
                drnew["UOM"] = txtOptItemUOM.Text;
                drnew["Quantity"] = txtOptQty.Text;
                drnew["Currency"] = ddlOptRate.SelectedItem.Text;
                drnew["Rate"] = txtOptRate.Text;

                drnew["Discount"] = txtOptDisc.Text;
                drnew["SpPrice"] = txtOptSpPrice.Text;
                drnew["Room"] = txtOptRoom.Text;
                drnew["CurrencyId"] = ddlOptRate.SelectedItem.Value;

                drnew["Color"] = ddlOptColor.SelectedItem.Text;
                drnew["ColorId"] = ddlOptColor.SelectedItem.Value;
                drnew["OptId"] = lblOptId.Text;
                drnew["Remarks"] = txtOptRemarks.Text;
                drnew["OptSrlOrder"] = txtOptSrlOrder.Text;

                QuotationItems.Rows.Add(drnew);
            }
            gvOptQuatationItems.DataSource = QuotationItems;
            gvOptQuatationItems.DataBind();
            gvOptQuatationItems.SelectedIndex = -1;
            btnOptRefresh_Click(sender, e);
        }
        else
        {
            DataTable QuotationItems = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("ItemCode");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ModelNo");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ItemName");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("UOM");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Quantity");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Currency");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Rate");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Discount");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("SpPrice");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Room");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("CurrencyId");
            QuotationItems.Columns.Add(col);

            col = new DataColumn("Color");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("ColorId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("OptId");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("Remarks");
            QuotationItems.Columns.Add(col);
            col = new DataColumn("OptSrlOrder");
            QuotationItems.Columns.Add(col);


            if (gvOptQuatationItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvOptQuatationItems.Rows)
                {
                    if (gvOptQuatationItems.SelectedIndex > -1)
                    {
                        if (gvrow.RowIndex == gvOptQuatationItems.SelectedRow.RowIndex)
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = ddlOptEssentials.SelectedItem.Value;
                            dr["ModelNo"] = ddlOptEssentials.SelectedItem.Text;
                            dr["ItemName"] = txtOptItemName.Text;
                            dr["UOM"] = txtOptItemUOM.Text;
                            dr["Quantity"] = txtOptQty.Text;
                            dr["Currency"] = ddlOptRate.SelectedItem.Text;
                            dr["Rate"] = txtOptRate.Text;

                            dr["Discount"] = txtOptDisc.Text;
                            dr["SpPrice"] = txtOptSpPrice.Text;
                            dr["Room"] = txtOptRoom.Text;
                            dr["CurrencyId"] = ddlOptRate.SelectedItem.Value;

                            dr["Color"] = ddlOptColor.SelectedItem.Text;
                            dr["ColorId"] = ddlOptColor.SelectedItem.Value;
                            dr["OptId"] = lblOptId.Text;
                            dr["Remarks"] = txtOptRemarks.Text;
                            dr["OptSrlOrder"] = txtOptSrlOrder.Text;

                            QuotationItems.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = QuotationItems.NewRow();
                            dr["ItemCode"] = gvrow.Cells[2].Text;
                            dr["ModelNo"] = gvrow.Cells[3].Text;
                            dr["ItemName"] = gvrow.Cells[4].Text;
                            dr["UOM"] = gvrow.Cells[5].Text;
                            dr["Quantity"] = gvrow.Cells[6].Text;
                            dr["Currency"] = gvrow.Cells[7].Text;
                            dr["Rate"] = gvrow.Cells[8].Text;

                            dr["Discount"] = gvrow.Cells[10].Text;
                            dr["SpPrice"] = gvrow.Cells[11].Text;
                            dr["Room"] = gvrow.Cells[12].Text;
                            dr["CurrencyId"] = gvrow.Cells[13].Text;
                            dr["Color"] = gvrow.Cells[14].Text;
                            dr["ColorId"] = gvrow.Cells[15].Text;
                            dr["OptId"] = gvrow.Cells[16].Text;
                            dr["Remarks"] = gvrow.Cells[17].Text;
                            dr["OptSrlOrder"] = gvrow.Cells[18].Text;

                            QuotationItems.Rows.Add(dr);
                        }
                    }
                    else
                    {

                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Currency"] = gvrow.Cells[7].Text;
                        dr["Rate"] = gvrow.Cells[8].Text;

                        dr["Discount"] = gvrow.Cells[10].Text;
                        dr["SpPrice"] = gvrow.Cells[11].Text;
                        dr["Room"] = gvrow.Cells[12].Text;
                        dr["CurrencyId"] = gvrow.Cells[13].Text;
                        dr["Color"] = gvrow.Cells[14].Text;
                        dr["ColorId"] = gvrow.Cells[15].Text;
                        dr["OptId"] = gvrow.Cells[16].Text;
                        dr["remarks"] = gvrow.Cells[17].Text;
                        dr["OptSrlOrder"] = gvrow.Cells[18].Text;

                        QuotationItems.Rows.Add(dr);
                    }
                }
            }


            if (gvOptQuatationItems.SelectedIndex == -1)
            {
                DataRow drnew = QuotationItems.NewRow();
                drnew["ItemCode"] = ddlOptEssentials.SelectedValue;
                drnew["ModelNo"] = ddlOptEssentials.SelectedItem.Text;
                drnew["ItemName"] = txtOptItemName.Text;
                drnew["UOM"] = txtOptItemUOM.Text;
                drnew["Quantity"] = txtOptQty.Text;
                drnew["Currency"] = ddlOptRate.SelectedItem.Text;
                drnew["Rate"] = txtOptRate.Text;

                drnew["Discount"] = txtOptDisc.Text;
                drnew["SpPrice"] = txtOptSpPrice.Text;
                drnew["Room"] = txtOptRoom.Text;
                drnew["CurrencyId"] = ddlOptRate.SelectedItem.Value;

                drnew["Color"] = ddlOptColor.SelectedItem.Text;
                drnew["ColorId"] = ddlOptColor.SelectedItem.Value;
                drnew["OptId"] = lblOptId.Text;
                drnew["Remarks"] = txtOptRemarks.Text;
                drnew["OptSrlOrder"] = txtOptSrlOrder.Text;

                QuotationItems.Rows.Add(drnew);
            }
            gvOptQuatationItems.DataSource = QuotationItems;
            gvOptQuatationItems.DataBind();
            gvOptQuatationItems.SelectedIndex = -1;
            btnOptRefresh_Click(sender, e);

        }

    }
    #endregion
    protected void btnOptRefresh_Click(object sender, EventArgs e)
    {
        if (rbIndividual.Checked == true)
        {
            ddlOptRate.SelectedValue = "1";
            //ddlModelNo.SelectedValue = "0";


        }
        else
        {
            ddlOptRate.SelectedValue = "0";
            //ddlModelNo.SelectedValue = "0";
            //ddlModelNo.Items.Clear();
        }

        //ddlItemName.SelectedValue = "0";
        ddlOptColor.SelectedValue = "0";
        txtOptItemUOM.Text = string.Empty;
        txtOptQty.Text = string.Empty;
        txtOptRate.Text = string.Empty;
        txtOptItemSpec.Text = string.Empty;
        txtOptDisc.Text = "0";
        txtOptSpPrice.Text = string.Empty;
        txtOptRoom.Text = string.Empty;
        txtOptBrand.Text = string.Empty;
        txtOptColor.Text = string.Empty;
        txtOptItemSubCat.Text = string.Empty;
        txtOptItemCat.Text = string.Empty;
        txtOptItemName.Text = string.Empty;
        gvOptQuatationItems.SelectedIndex = -1;
        OptImage3.ImageUrl = "~/Images/noimage400x300.gif";
        OptImage4.ImageUrl = "~/Images/noimage400x300.gif";

    }
    protected void gvOptQuatationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
            //if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Item Details list?');");
            e.Row.Cells[9].Text = (Convert.ToDecimal(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[6].Text)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[11].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total Amount:";
            e.Row.Cells[11].Text = TotalAmount.ToString();
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[0].Visible = false;
        }

    }
    protected void gvOptQuatationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvOptQuatationItems.Rows[e.RowIndex].Cells[1].Text;
        string x2 = gvOptQuatationItems.Rows[e.RowIndex].Cells[2].Text;

        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("UOM");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Currency");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Rate");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Discount");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SpPrice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Room");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CurrencyId");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("Color");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("OptId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("OptSrlOrder");
        QuotationItems.Columns.Add(col);

        if (gvOptSubItems.Rows.Count > 0)
        {
            DataTable SubItems = new DataTable();
            DataColumn dc;
            dc = new DataColumn("ITEM_CODE");
            SubItems.Columns.Add(dc);
            dc = new DataColumn("SUBITEM_CODE");
            SubItems.Columns.Add(dc);
            foreach (GridViewRow gvrow in gvOptSubItems.Rows)
            {
                if (x2 != gvrow.Cells[0].Text)
                {

                    DataRow dr = SubItems.NewRow();
                    dr["ITEM_CODE"] = gvrow.Cells[1].Text;
                    dr["SUBITEM_CODE"] = gvrow.Cells[2].Text;
                    SubItems.Rows.Add(dr);

                }
            }
            gvOptSubItems.DataSource = SubItems;
            gvOptSubItems.DataBind();

        }
        if (gvOptQuatationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOptQuatationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Currency"] = gvrow.Cells[7].Text;
                    dr["Rate"] = gvrow.Cells[8].Text;

                    dr["Discount"] = gvrow.Cells[10].Text;
                    dr["SpPrice"] = gvrow.Cells[11].Text;
                    dr["Room"] = gvrow.Cells[12].Text;
                    dr["CurrencyId"] = gvrow.Cells[13].Text;

                    dr["Color"] = gvrow.Cells[14].Text;
                    dr["ColorId"] = gvrow.Cells[15].Text;
                    dr["OptId"] = gvrow.Cells[16].Text;
                    dr["Remarks"] = gvrow.Cells[17].Text;
                    dr["OptSrlOrder"] = gvrow.Cells[18].Text;
                    QuotationItems.Rows.Add(dr);

                }
            }
        }
        gvOptQuatationItems.DataSource = QuotationItems;
        gvOptQuatationItems.DataBind();


    }
    protected void gvOptQuatationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvOptSubItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvOptSubItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnOptAddItems_Click(object sender, EventArgs e)
    {
        tblOptional.Visible = true;
        RateType1_Fill();
        ddlOptRate.SelectedValue = "1";
    }

    private void RateType1_Fill()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlOptRate);
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
    protected int RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        //lblLeaveId.Text = intRandomNumber.ToString();
        return intRandomNumber;

    }
    protected void rdbQC2_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbQC2.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QCt2&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbqt5_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbqt5.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Q5&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void txtDisc_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvQuotationItems.Rows)
        {

            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[11].Text = ((Convert.ToDecimal(Rate.Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(Rate.Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[13].Text = ((Convert.ToDecimal(gvr.Cells[11].Text) * Convert.ToDecimal(gvr.Cells[12].Text)) / 100).ToString();

        }

    }
    protected void txtDetQty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvQuotationItems.Rows)
        {
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[9].Text = (Convert.ToDecimal(Rate.Text) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[11].Text = ((Convert.ToDecimal(Rate.Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(Rate.Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[13].Text = ((Convert.ToDecimal(gvr.Cells[11].Text) * Convert.ToDecimal(gvr.Cells[12].Text)) / 100).ToString();

        }
    }
    protected void txtDetRate_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvQuotationItems.Rows)
        {
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[9].Text = (Convert.ToDecimal(Rate.Text) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[11].Text = ((Convert.ToDecimal(Rate.Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(Rate.Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[13].Text = ((Convert.ToDecimal(gvr.Cells[11].Text) * Convert.ToDecimal(gvr.Cells[12].Text)) / 100).ToString();

        }
    }
    protected void rdbTechDrawings_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbTechDrawings.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Tech&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbCOmpare_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";

        if (rdbCOmpare.Checked == true)
        {
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=C1&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rbEquotation_CheckedChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/SM/List_eQuotations2.aspx?QuoId=" + Request.QueryString["QuoId"].ToString());
    }
    protected void btnPrintTB_Click(object sender, EventArgs e)
    {
        tblWoroomprint.Visible = true;
    }
    protected void rdbwithPrice_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbwithPrice.Checked == true)
        {

            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "";
            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwinp&qno=" + Request.QueryString["QuoId"].ToString() + "";
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwithprice&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }

        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void rdbWPSP_CheckedChanged(object sender, EventArgs e)
    {
        string pagenavigationstr = "";
        if (rdbWPSP.Checked == true)
        {

            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quot&qno=" + Request.QueryString["QuoId"].ToString() + "";
            //pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotwinp&qno=" + Request.QueryString["QuoId"].ToString() + "";
            pagenavigationstr = "../Reports/SMReportViewer.aspx?type=quotWPSP&qno=" + Request.QueryString["QuoId"].ToString() + "";

        }

        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void btnPrintCB_Click(object sender, EventArgs e)
    {
        tblprintColumns.Visible = true;
    }



    protected void chkwp_CheckedChanged(object sender, EventArgs e)
    {

        gvitemsgrid.DataBind();

    }
    protected void chkwpsp_CheckedChanged(object sender, EventArgs e)
    {
        chkmrptotal.Checked = false;
        gvitemsgrid.DataBind();
    }
    protected void chkwpspta_CheckedChanged(object sender, EventArgs e)
    {
        chkmrptotal.Checked = false;
        gvitemsgrid.DataBind();
    }
    protected void chk3gst_CheckedChanged(object sender, EventArgs e)
    {

        gvitemsgrid.DataBind();
    }
    protected void chk3wc_CheckedChanged(object sender, EventArgs e)
    {
        chkGST.Checked = true;
        chkmrptotal.Checked = false;
        gvitemsgrid.DataBind();
    }
    protected void chkwsp_CheckedChanged(object sender, EventArgs e)
    {

        gvitemsgrid.DataBind();
    }
    protected void chkWoPrices_CheckedChanged(object sender, EventArgs e)
    {

        gvitemsgrid.DataBind();
    }
    protected void btnExport2_Click(object sender, EventArgs e)
    {
        if (gvitemsgrid.Rows.Count > 0)
        {

            //Image1.ImageUrl = this.GetAbsoluteUrl(Image1.ImageUrl);
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages

                    gvitemsgrid.AllowPaging = false;
                    gvitemsgrid.DataBind();
                    //gvterms.AllowPaging = false;
                    //gvterms.DataBind();
                    //gvitemsgrid.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvitemsgrid.HeaderRow.Cells)
                    {
                        cell.BackColor = gvitemsgrid.HeaderStyle.BackColor;
                        cell.BackColor = gvterms.HeaderStyle.BackColor;
                        
                    }
                    foreach (TableCell cell2 in gvitemsgrid.FooterRow.Cells)
                    {
                        cell2.Style["font-family"] = "Book Antiqua";
                        //cell2.Wrap = true;
                    }
                    foreach (GridViewRow row in gvitemsgrid.Rows)
                    {
                        //row.BackColor = Color.White;
                        row.HorizontalAlign = HorizontalAlign.Center;
                        gvterms.HorizontalAlign = HorizontalAlign.Center;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvitemsgrid.AlternatingRowStyle.BackColor;
                                cell.BackColor = gvterms.AlternatingRowStyle.BackColor;

                                cell.Wrap = true;
                            }
                            else
                            {
                                cell.BackColor = gvitemsgrid.RowStyle.BackColor;
                                cell.BackColor = gvterms.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }

                        gvitemsgrid.Style["font-family"] = "Book Antiqua";
                        row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                        row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                        string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                        System.Web.UI.WebControls.Image img1 = row.Cells[16].Controls[1] as System.Web.UI.WebControls.Image;
                        row.Cells[16].Controls.Add(img1);
                        img1.Height = Unit.Pixel(150);
                        img1.Width = Unit.Pixel(150);
                    }

                    gvitemsgrid.RenderControl(hw);
                    //gvterms.RenderControl(hw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Quotation.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    
                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    public void GenerateThumbNail(string sourcefile, string destinationfile, int width)
    {
        if (File.Exists(Server.MapPath(sourcefile)))
        {

            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(sourcefile));
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            int thumbWidth = width;
            int thumbHeight;
            Bitmap bmp;
            if (srcHeight > srcWidth)
            {
                thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }
            else
            {
                thumbHeight = thumbWidth;
                thumbWidth = (srcWidth / srcHeight) * thumbHeight;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

            bmp.Save(Server.MapPath(destinationfile));
            bmp.Dispose();
            image.Dispose();
            //DeleteTempImage(sourcefile, destinationfile);

        }
    }

    private void DeleteTempImage(string sourcefile, string destinationfile)
    {
        string directoryPath = Server.MapPath(sourcefile);
        File.Delete(directoryPath);
    }

    protected void chkmrptotal_CheckedChanged(object sender, EventArgs e)
    {
        chkGST.Checked = false;
        chkwpspta.Checked = false;
        chkwpsp.Checked = false;
        chk3wc.Checked = false;
        gvitemsgrid.DataBind();
    }
    protected void chkGST_CheckedChanged(object sender, EventArgs e)
    {
        chkmrptotal.Checked = false;
        gvitemsgrid.DataBind();
    }
    protected void gvterms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
        e.Row.Cells[0].Text = decodedText;
    }
    protected void chkterms_CheckedChanged(object sender, EventArgs e)
    {
        gvitemsgrid.DataBind();
        gvterms.Visible = true;
        gvterms.DataBind();
    }

    protected void btnExportPdf1_Click(object sender, EventArgs e)
    {
        gvitemsgrid.AllowPaging = false;
        gvitemsgrid.DataBind();

        BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIALUNI.TTF", BaseFont.IDENTITY_H, true);

        iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(gvitemsgrid.Columns.Count);
        int[] widths = new int[gvitemsgrid.Columns.Count];
        for (int x = 0; x < gvitemsgrid.Columns.Count; x++)
        {
            widths[x] = (int)gvitemsgrid.Columns[x].ItemStyle.Width.Value;
            string cellText = Server.HtmlDecode(gvitemsgrid.HeaderRow.Cells[x].Text);

            //Set Font and Font Color
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //font.Color = new Color(gvitemsgrid.HeaderStyle.ForeColor);
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));

            //Set Header Row BackGround Color
            //cell.BackgroundColor = new Color(gvitemsgrid.HeaderStyle.BackColor);


            table.AddCell(cell);
        }
        table.SetWidths(widths);

        for (int i = 0; i < gvitemsgrid.Rows.Count; i++)
        {
            if (gvitemsgrid.Rows[i].RowType == DataControlRowType.DataRow)
            {
                for (int j = 0; j < gvitemsgrid.Columns.Count; j++)
                {
                    string cellText = Server.HtmlDecode(gvitemsgrid.Rows[i].Cells[j].Text);

                    //Set Font and Font Color
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                    //font.Color = new Color(gvitemsgrid.RowStyle.ForeColor);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                    
                    //Set Color of row
                    if (i % 2 == 0)
                    {
                        //Set Row BackGround Color
                        //cell.BackgroundColor = new Color(gvitemsgrid.RowStyle.BackColor);
                    }
                    
                }
            }
        }
        foreach (GridViewRow row in gvitemsgrid.Rows)
        {
            row.Style.Add(HtmlTextWriterStyle.Height, "100px");
            row.Style.Add(HtmlTextWriterStyle.Width, "100px");
            string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
            System.Web.UI.WebControls.Image img1 = row.Cells[16].Controls[1] as System.Web.UI.WebControls.Image;
            row.Cells[16].Controls.Add(img1);
            img1.Height = Unit.Pixel(150);
            img1.Width = Unit.Pixel(150);
        }
        //Create the PDF Document
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        pdfDoc.Add(table);
        pdfDoc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();

    }
    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        
        if (gvitemsgrid.Rows.Count > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvitemsgrid.AllowPaging = false;
                    gvitemsgrid.DataBind();
                    int[] widths = new int[gvitemsgrid.Columns.Count];
                    for (int x = 0; x < gvitemsgrid.Columns.Count; x++)
                    {
                        widths[x] = (int)gvitemsgrid.Columns[x].ItemStyle.Width.Value;
                        string cellText = Server.HtmlDecode(gvitemsgrid.HeaderRow.Cells[x].Text);
                        
                    }
                    foreach (TableCell cell in gvitemsgrid.HeaderRow.Cells)
                    {
                        cell.BackColor = gvitemsgrid.HeaderStyle.BackColor;
                        cell.BackColor = gvitemsgrid.HeaderStyle.BackColor;
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.Wrap = true;
                    }
                    foreach (TableCell cell2 in gvitemsgrid.FooterRow.Cells)
                    {
                        cell2.Style["font-family"] = "Book Antiqua";
                        //cell2.Wrap = true;
                    }
                    foreach (GridViewRow row in gvitemsgrid.Rows)
                    {
                        //row.BackColor = Color.White;
                        
                        row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        row.Cells[1].HorizontalAlign = HorizontalAlign.Left ;
                        row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                        row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                        row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                        row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                        //gvitemsgrid.Columns[1].ItemStyle.Width = Unit.Pixel(1);
                        row.Cells[1].Width = Unit.Pixel(1);
                        foreach (TableCell cell in row.Cells)
                        {
                            
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvitemsgrid.AlternatingRowStyle.BackColor;
                                cell.BackColor = gvterms.AlternatingRowStyle.BackColor;

                                cell.Wrap = true;
                            }
                            else
                            {
                                cell.BackColor = gvitemsgrid.RowStyle.BackColor;
                                cell.BackColor = gvterms.RowStyle.BackColor;
                                cell.Wrap = true;
                            }
                            cell.CssClass = "textmode";
                        }

                        gvterms.Style["font-family"] = "Book Antiqua, Helvetica, sans-serif;";
                        row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                        row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                        string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                        System.Web.UI.WebControls.Image img1 = row.Cells[16].Controls[1] as System.Web.UI.WebControls.Image;
                        row.Cells[16].Controls.Add(img1);
                        img1.Height = Unit.Pixel(150);
                        img1.Width = Unit.Pixel(150);
                       
                    }

                    gvitemsgrid.RenderControl(hw);
                    gvitemsgrid.HeaderRow.Style.Add("width", "15%");
                   
                    //gvterms.RenderControl(hw);
                    Response.Clear();
                    Response.Buffer = true;
                    //Response.AddHeader("content-disposition", "attachment;filename=Quotation.xls");
                    Response.Charset = "";
                    //Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Quotation.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0.0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.Write(pdfDoc);

                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }


    
}

