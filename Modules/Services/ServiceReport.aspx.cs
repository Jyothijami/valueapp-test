
//Date Written: 07/May/2009      Written By: L.Hima Kishore
//Modified and Issues Solved By A.VISHNU PRASAD  ON 14 MAY 2009

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
public partial class Modules_Services_ServiceReport : basePage
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            
            //Yantra.Authentication.Privilege_Check(this);
            //lblEmpIdHidden.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            lblEmpIdHidden.Text  = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblCPID.Text = cp.getPresentCompanySessionValue();

           // Masters.ItemMaster.ItemMaster_Select(ddlItemType);

            if (Request.QueryString["enqid"] != null)
            {
                btnNew_Click(sender, e);
                tblServiceReport.Visible = true;
            }
            //lblUserName1.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
            //if (lblUserName1.Text == "rajesh_service" || lblUserName1.Text == "naidu_service")
            //{
                ddlServiceStatus.Enabled = true;
            //}
            //else
            //{
            //    ddlServiceStatus.Enabled = false;
            //}
            if (Request.QueryString["insaid"] != null)
            {
                btnNew_Click(sender, e);
                rbServiceType.Enabled = false;
                lblInsAId.Text = Request.QueryString["insaid"].ToString();
                lblAMCAId.Text = "0";
                lblCRNo.Visible = lblCRDate.Visible = ddlCRNo.Visible = txtCRDate.Visible = false;
                rbServiceType.SelectedValue = "Installation";
                SalesOrder_Fill();
                tblServiceReport.Visible = true;
            }
            if (Request.QueryString["amcaid"] != null)
            {
                btnNew_Click(sender, e);
                rbServiceType.Enabled = false;
                lblAMCAId.Text = Request.QueryString["amcaid"].ToString();
                lblInsAId.Text = "0";
                lblCRNo.Visible = lblCRDate.Visible = ddlCRNo.Visible = txtCRDate.Visible = false;
                rbServiceType.SelectedValue = "AMC";
                AMCOrder_Fill();
                ddlCRNo_SelectedIndexChanged(sender, e);
                tblServiceReport.Visible = true;
            }
            if (Request.QueryString["SOId"] != null)
            {
                btnNew_Click(sender, e);

             ddlCRNo.SelectedValue = Request.QueryString["SOId"].ToString();
             ddlCRNo_SelectedIndexChanged(sender, e);
            }
            setControlsVisibility();
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "34");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvServiceReport.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvServiceReport.SelectedRow.Cells[7].Text) && gvServiceReport.SelectedRow.Cells[7].Text != "&nbsp;")
            {
                // btnApprove.Visible = false;
                btnSave.Visible = false;
                if (btnSave.Text == "Update")
                {
                    btnSave.Visible = true;
                }
                btnRefresh.Visible = false;
              //  btnPrint.Visible = true;
            }
            else
            {
                //  btnApprove.Visible = true;
                btnSave.Visible = true;
                btnRefresh.Visible = true;
              //  btnPrint.Visible = false;
            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;
            // btnApprove.Visible = false;
         //   btnPrint.Visible = false;
        }

        //if (btnApprove.Visible == false)
        //{
        //    Panel1.Visible = false;
        //}
        //else
        //{
        //    Panel1.Visible = true;
        //}     
    }
    #endregion

    #region Service Report Fill
    private void ServiceReport_Fill()
    {
        try
        {
            Services.ServiceReport.ServiceReport_Select(ddlSRNo);
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

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlItemType);
            // Services.SalesQuotation.SalesQuotationItemTypes_Select(ddlQuotationNo.SelectedItem.Value, ddlItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
            //  Services.Dispose();
        }
    }
    #endregion

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlCustomerName);
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

            HR.EmployeeMaster.EmployeeMaster_Select(ddlVisitedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);

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

    #region  CR No Fill
    private void CRNo_Fill()
    {
        if (rbServiceType.SelectedItem.Text == "Installation")
        {
            try
            {
                Services.ComplaintRegister.ComplaintRegisterForInstallation_Select(ddlCRNo);
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
        else if (rbServiceType.SelectedItem.Text == "Warranty")
        {
            try
            {
                Services.ComplaintRegister.ComplaintRegisterForWarranty_Select(ddlCRNo);
                //ddlOrderNo.Enabled = ddlCRNo.Enabled = false;
                //rbWarrantySelect.Visible = true;
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
        else if (rbServiceType.SelectedItem.Text == "AMC")
        {
            try
            {
                Services.ComplaintRegister.ComplaintRegisterForAMC_Select(ddlCRNo);
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
        else if (rbServiceType.SelectedItem.Text == "Non Warranty")
        {
            try
            {
                Services.ComplaintRegister.ComplaintRegisterForSparesInstall_Select(ddlCRNo);
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

        //try
        //{
        //    Services.ComplaintRegister.ComplaintRegister_Select(ddlCRNo);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        //finally
        //{
        //    Services.Dispose();
        //}
    }
    #endregion

    #region Link Button lbtnSRNo_Click
    protected void lbtnSRNo_Click(object sender, EventArgs e)
    {
       
        ItemTypes_Fill();
        EmployeeMaster_Fill();
        CRNo1_Fill();
        CustomerMaster_Fill();
        tblServiceReport.Visible = false;
        //  tblFollowUp.Visible = false;
        LinkButton lbtnSRNo;
        lbtnSRNo = (LinkButton)sender;
        btnSave.Enabled = false;
        GridViewRow gvRow = (GridViewRow)lbtnSRNo.Parent.Parent;
        gvServiceReport.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        // lblQuotIdHiddenForFollowUp.Text = gvRow.Cells[0].Text;


        //try
        //{
        //    Services.ServiceReport objServiceReport = new Services.ServiceReport();
        //    if (objServiceReport.ServiceReport_Select(gvServiceReport.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Save";
        //        btnSave.Enabled = false;
        //        ddlCRNo.Enabled = false;
        //        tblServiceReport.Visible = true;
        //        rbServiceType.SelectedValue = objServiceReport.CRCallType;
        //        rbServiceType_SelectedIndexChanged(sender, e);

        //        txtSRNo.Text = objServiceReport.SRNo;
        //        txtSRDate.Text = objServiceReport.SRDate;
        //        ddlCRNo.SelectedValue = objServiceReport.CRId;
        //        ddlCRNo_SelectedIndexChanged(sender, e);
        //        txtCallType.Text = objServiceReport.CRCallType;
        //        ddlCustomerName.SelectedValue = objServiceReport.CustId;
        //        ddlCustomerName_SelectedIndexChanged(sender, e);
        //        ddlUnitName.SelectedValue = objServiceReport.CustUnitId;
        //        ddlUnitName_SelectedIndexChanged(sender, e);
        //        ddlContactPerson.SelectedValue = objServiceReport.CustDetId;
        //        txtServiceCenter.Text = objServiceReport.SRServiceCenter;
        //        if (rbServiceType.SelectedValue == "Installation" || rbServiceType.SelectedValue == "Warranty")
        //        {
        //            ddlOrderNo.SelectedValue = objServiceReport.SOId;
        //            btnCr.Enabled = true;
        //        }
        //        else if (rbServiceType.SelectedValue == "AMC")
        //        {
        //            ddlOrderNo.SelectedValue = objServiceReport.AMCOId;
        //            btnCr.Enabled = true;
        //        }
                
        //        else if (rbServiceType.SelectedValue == "Non Warranty")
        //        {
        //            ddlOrderNo.SelectedValue = objServiceReport.SPOId;
        //            btnCr.Enabled = true;
        //        }
        //        ddlOrderNo_SelectedIndexChanged(sender, e);
        //        txtAMCRefNo.Text = objServiceReport.SRAMCRefNo;
        //        txtAMCVisitDate.Text = objServiceReport.SRAMCVisitDate;
        //        txtCompletionDate.Text = objServiceReport.SRCompletionDate;
        //        txtDescription.Text = objServiceReport.SRDescription;
        //        txtActionTaken.Text = objServiceReport.SRActionTaken;
        //        txtActionRequired.Text = objServiceReport.SRFurtherActionReq;
        //        txtCustFeedback.Text = objServiceReport.SRCustFeedback;
        //        ddlIsDocSubmitted.SelectedValue = objServiceReport.SRIsDocSubmited;
        //        txtServiceCompleted.Text = objServiceReport.SRServiceCompleted;
        //        ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objServiceReport.ItemCode);
        //        ddlItemType_SelectedIndexChanged(sender, e);
        //        //ddlItemName.SelectedValue = objServiceReport.ItemCode;
        //        txtItemSLNo.Text = objServiceReport.SRItemSLNo;
        //        ddlVisitedBy.SelectedValue = objServiceReport.EmpId;

        //        //fuAttachments.Text = objServiceReport.SRFiles;

        //        ddlPreparedBy.SelectedValue = objServiceReport.SRPreparedBy;
        //        ddlApprovedBy.SelectedValue = objServiceReport.SRApprovedBy;


        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    btnDelete.Attributes.Clear();
        //    Services.Dispose();

        //}

    }
    #endregion

    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ServiceReportNew.aspx?srNo=0");
        //Services.ClearControls(this);
        //rbServiceType.Enabled = true;
        //rbServiceType.SelectedIndex = -1;
        //gvServiceReport.SelectedIndex = -1;
        //ddlCRNo.Enabled = true;
        //txtSRNo.Text = Services.ServiceReport.ServiceReport_AutoGenCode();
        //txtSRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //gvServiceItems.DataSource = null;
        //gvServiceItems.DataBind();
        //gvQuotationItems.DataSource = null;
        //gvQuotationItems.DataBind();
        //btnSave.Text = "Save";
        //tblServiceReport.Visible = true;
        //btnSave.Enabled = true;
        //btnCr.Enabled = false;
        //lblMessage.Text = "";
        //ServiceReport_Fill();
        //lblSRNo1.Visible = lblSRDate1.Visible = ddlSRNo.Visible = txtSRDate1.Visible = false;
        //ddlOrderNo.Enabled = ddlCRNo.Enabled = true;
        //rbWarrantySelect.Visible = false;
        //rbWarrantySelect.SelectedIndex = -1;
        //CustomerMaster_Fill();
        
        //EmployeeMaster_Fill();
        //CRNo1_Fill();
        //SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY where PRODUCT_COMPANY_NAME is not null");

    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
      
        if (gvPreviousServiceRecords.Rows.Count > 0)
        {
            string InstallDate = gvPreviousServiceRecords.Rows[0].Cells[4].Text;
            try
            {
                TimeSpan ts = DateTime.Now.Subtract(DateTime.Parse(Yantra.Classes.General.toMMDDYYYY(InstallDate)));
                if (ts.Days > 365)
                {
                    MessageBox.Show(this, "Warranty Expired on " + DateTime.Parse(Yantra.Classes.General.toMMDDYYYY(InstallDate)).AddYears(1).ToString("dd/MM/yyyy") + "");
                    lblMessage.Text = "Warranty Expired on " + DateTime.Parse(Yantra.Classes.General.toMMDDYYYY(InstallDate)).AddYears(1).ToString("dd/MM/yyyy") + "";
                    btnSave.Enabled = false;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    lblMessage.Text = "";
                }
            }
            catch
            {
            }
        }
        if (btnSave.Text == "Save")
        {
            ServiceReportSave();
        }
        else if (btnSave.Text == "Update")
        {
            ServiceReportUpdate();
        }
    }
    #endregion

    #region ServiceReportSave
    private void ServiceReportSave()
    {

        try
        {
            Services.ServiceReport objsr = new Services.ServiceReport();
            if (objsr.complaintrecord_isrecordexists(ddlCRNo.SelectedItem.Value) > 0)
            {
               
            }
            Services.ServiceReport objServiceReport = new Services.ServiceReport();
            Services.BeginTransaction();

            objServiceReport.SRNo = txtSRNo.Text;
            objServiceReport.SRDate = Yantra.Classes.General.toMMDDYYYY(txtSRDate.Text);
            objServiceReport.CRId = ddlCRNo.SelectedItem.Value; ;
            objServiceReport.CRCallType = txtCallType.Text;
            objServiceReport.CustId = ddlCustomerName.SelectedItem.Value;
            objServiceReport.CustUnitId = ddlUnitName.SelectedItem.Value;
            objServiceReport.CustDetId = ddlContactPerson.SelectedItem.Value;
            objServiceReport.SRServiceCenter = txtServiceCenter.Text;

            if (rbServiceType.SelectedValue == "Installation" || rbServiceType.SelectedValue == "Warranty")
            {
                if (gvPreviousServiceRecords.Rows.Count >= 1)
                {
                    objServiceReport.SRServiceType = "Warranty";
                }
                else if (gvPreviousServiceRecords.Rows.Count == 0)
                {
                    objServiceReport.SRServiceType = "Installation";
                }
                objServiceReport.SOId = ddlOrderNo.SelectedItem.Value;
                objServiceReport.AMCOId = "0";
                objServiceReport.SPOId = "0";
            }
            else if (rbServiceType.SelectedValue == "AMC")
            {
                objServiceReport.SRServiceType = rbServiceType.SelectedItem.Text;
                objServiceReport.AMCOId = ddlOrderNo.SelectedItem.Value;
                objServiceReport.SOId = "0";
                objServiceReport.SPOId = "0";
            }
            else if (rbServiceType.SelectedValue == "Non Warranty")
            {
                objServiceReport.SRServiceType = rbServiceType.SelectedItem.Text;
                objServiceReport.SPOId = ddlOrderNo.SelectedItem.Value;
                objServiceReport.SOId = "0";
                objServiceReport.AMCOId = "0";
            }
            objServiceReport.SRAMCRefNo = txtAMCRefNo.Text;
            objServiceReport.SRAMCVisitDate = Yantra.Classes.General.toMMDDYYYY(txtAMCVisitDate.Text);
            objServiceReport.SRCompletionDate = Yantra.Classes.General.toMMDDYYYY(txtCompletionDate.Text);
            objServiceReport.SRDescription = "-";
            objServiceReport.SRActionTaken = "-";
            objServiceReport.SRFurtherActionReq = "-";
            objServiceReport.SRCustFeedback = txtCustFeedback.Text;
            objServiceReport.SRIsDocSubmited = ddlIsDocSubmitted.SelectedItem.Value;
            objServiceReport.SRServiceCompleted = txtServiceCompleted.Text;
            objServiceReport.ItemCode = "0";
            objServiceReport.SRItemSLNo = txtItemSLNo.Text;
            objServiceReport.EmpId = ddlVisitedBy.SelectedItem.Value;
            objServiceReport.SRAMCAId = "0";
            objServiceReport.SRInsAId = "0";
            objServiceReport.Cp_Id = lblCPID.Text;


            // objServiceReport.SRFiles = fuAttachments.Text;                       
            if (FileUpload1.HasFile)
            {
                objServiceReport.SRFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objServiceReport.SRFiles = "";
            }
            //if (objServiceReport.SRIsDocSubmited == "Yes")
            //{
            //    objServiceReport.SRStatus = "Closed";
            //}
            //else if (objServiceReport.SRIsDocSubmited == "NA")
            //{
            //    objServiceReport.SRStatus = "Closed";
            //}
            //else
            //{
            //    objServiceReport.SRStatus = "Open";
            //}
             if (gvServiceItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvServiceItems.Rows)
                {
                    if (gvrow.Cells[7].Text.Trim() == "OPEN")
                    {
                        objServiceReport.SRStatus = "Open";
                    }
                    else
                    {
                        objServiceReport.SRStatus = "Closed";
                    }
                }
            }
            if (gvServiceItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvServiceItems.Rows)
                {
                    string status = gvrow.Cells[7].Text;
                    string[] status1 = status.Split(' ');
                    if ((status1[0]) == "Open")
                    {
                        objServiceReport.SRStatus = "Open";
                    }
                    else
                    {
                        objServiceReport.SRStatus = "Closed";
                    }
                }
            }
            objServiceReport.SRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objServiceReport.SRApprovedBy = ddlApprovedBy.SelectedItem.Value;


            if (FileUpload1.HasFile)
            {
                //if (lbtnAttachedFiles.Text != "")
                //{
                //    string[] ext = lbtnAttachedFiles.Text.Split('.');
                //    if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]))
                //    { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]); }
                //}
                FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SRFiles/" + objServiceReport.SRId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
            }

            //objServiceReport.ServiceReport_Save();


            if (objServiceReport.ServiceReport_Save() == "Data Saved Successfully")
            {
                //objServiceReport.ServiceReportDetails_Delete(objServiceReport.SRId);
                foreach (GridViewRow gvrow in gvServiceItems.Rows)
                {
                    objServiceReport.SRNo = txtSRNo.Text;
                    //Services.ServiceReport sr2 = new Services.ServiceReport();
                //   sr2.ServiceReport_Select2(txtSRNo.Text);
                //   objServiceReport.SRId2 = sr2.SRId2;
                    objServiceReport.ItemCode = gvrow.Cells[2].Text;

                    objServiceReport.ItemName = gvrow.Cells[3].Text;
                    if (gvrow.Cells[4].Text == "&nbsp;")
                    {
                        objServiceReport.SRDescription = "-";
                    }

                    else { objServiceReport.SRDescription = gvrow.Cells[4].Text; }
                    if(gvrow.Cells[5].Text=="&nbsp;")
                    {
                       objServiceReport.SRActionTaken="-";
                    }
                    else{ objServiceReport.SRActionTaken = gvrow.Cells[5].Text;}
                    if(gvrow.Cells[6].Text=="&nbsp;")
                    {
                        objServiceReport.SRFurtherActionReq="-";
                    }
                    else{  objServiceReport.SRFurtherActionReq = gvrow.Cells[6].Text;}
                    if (gvrow.Cells[11].Text == "&nbsp;")
                    {
                        objServiceReport.SRCustFeedback = "-";
                    }
                    else { objServiceReport.SRCustFeedback = gvrow.Cells[11].Text; }
                    objServiceReport.Status = gvrow.Cells[7].Text;
                    objServiceReport.DOC = gvrow.Cells[10].Text;
                    
                    objServiceReport.ServiceReportItemDetails_Save();
                }
            }

            objServiceReport.ServiceReport_Update1();
            MessageBox.Show(this, objServiceReport.ServiceReport_Update2());

            Services.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");

        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvServiceReport.DataBind();
            gvServiceItems.DataSource = null;
            gvServiceItems.DataBind();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
            tblServiceReport.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
        }

    }
    #endregion

    #region ServiceReportUpdate
    private void ServiceReportUpdate()
    {

        try
        {
            Services.ServiceReport objServiceReport = new Services.ServiceReport();
            Services.BeginTransaction();

            objServiceReport.SRId = gvServiceReport.SelectedRow.Cells[0].Text;

            objServiceReport.SRNo = txtSRNo.Text;
            objServiceReport.SRDate = Yantra.Classes.General.toMMDDYYYY(txtSRDate.Text);
            objServiceReport.CRId = ddlCRNo.SelectedItem.Value; ;
            objServiceReport.CRCallType = txtCallType.Text;
            objServiceReport.CustId = ddlCustomerName.SelectedItem.Value;
            objServiceReport.CustUnitId = ddlUnitName.SelectedItem.Value;
            objServiceReport.CustDetId = ddlContactPerson.SelectedItem.Value;
            objServiceReport.SRServiceCenter = txtServiceCenter.Text;
            if (rbServiceType.SelectedValue == "Installation" || rbServiceType.SelectedValue == "Warranty")
            {
                if (gvPreviousServiceRecords.Rows.Count > 1)
                {
                    objServiceReport.SRServiceType = "Warranty";
                }
                else if (gvPreviousServiceRecords.Rows.Count == 0)
                {
                    objServiceReport.SRServiceType = "Installation";
                }
                objServiceReport.SOId = ddlOrderNo.SelectedItem.Value;
             
                objServiceReport.AMCOId = "0";
                objServiceReport.SPOId = "0";
            }
            else if (rbServiceType.SelectedValue == "AMC")
            {
                objServiceReport.SRServiceType = rbServiceType.SelectedItem.Text;
                objServiceReport.AMCOId = ddlOrderNo.SelectedItem.Value;
                objServiceReport.SOId = "0";
                objServiceReport.SPOId = "0";
            }
            else if (rbServiceType.SelectedValue == "Non Warranty")
            {
                objServiceReport.SRServiceType = rbServiceType.SelectedItem.Text;
                objServiceReport.SPOId = ddlOrderNo.SelectedItem.Value;
                objServiceReport.SOId = "0";
                objServiceReport.AMCOId = "0";
            }
            objServiceReport.SRAMCRefNo = txtAMCRefNo.Text;
            objServiceReport.SRAMCVisitDate = Yantra.Classes.General.toMMDDYYYY(txtAMCVisitDate.Text);
            objServiceReport.SRCompletionDate = Yantra.Classes.General.toMMDDYYYY(txtCompletionDate.Text);
            objServiceReport.SRDescription = "-";
            objServiceReport.SRActionTaken = "-";
            objServiceReport.SRFurtherActionReq = "-";
            objServiceReport.SRCustFeedback = txtCustFeedback.Text;
            objServiceReport.SRIsDocSubmited = ddlIsDocSubmitted.SelectedItem.Value;
            objServiceReport.SRServiceCompleted = txtServiceCompleted.Text;
            objServiceReport.ItemCode = "0";
            objServiceReport.SRItemSLNo = txtItemSLNo.Text;
            objServiceReport.EmpId = ddlVisitedBy.SelectedItem.Value;
            objServiceReport.SRAMCAId = "0";
            objServiceReport.SRInsAId = "0";
            objServiceReport.Cp_Id = lblCPID.Text;
            //  objServiceReport.SRFiles = fuAttachments.Text;
            // string fileName = FileUpload1.PostedFile.FileName;
            //
            //    string fileFullPath = System.IO.Path.GetFullPath(fileName);
            if (FileUpload1.HasFile)
            {
                objServiceReport.SRFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            // if (fileFullPath !=String.Empty)
            //{
            //   objServiceReport.SRFiles = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            //  }
            else
            {
                objServiceReport.SRFiles = "";
            }
            //if (objServiceReport.SRIsDocSubmited == "Yes")
            //{
            //    objServiceReport.SRStatus = "Closed";
            //}
            //else if (objServiceReport.SRIsDocSubmited == "NA")
            //{
            //    objServiceReport.SRStatus = "Closed";
            //}
            //else
            //{
            //    objServiceReport.SRStatus = "Open";
            //}
            if (gvServiceItems.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvServiceItems.Rows)
                {
                    string status = gvrow.Cells[7].Text;
                  string [] status1=status.Split(' ');
                  if ((status1[0]) == "Open")
                    {
                        objServiceReport.SRStatus = "Open";
                    }
                    else
                    {
                        objServiceReport.SRStatus = "Closed";
                    }
                }
            }
            objServiceReport.SRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objServiceReport.SRApprovedBy = ddlApprovedBy.SelectedItem.Value;

            if (FileUpload1.HasFile)
            {
                //if (lbtnAttachedFiles.Text != "")
                //{
                //    string[] ext = lbtnAttachedFiles.Text.Split('.');
                //    if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]))
                //    { System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Modules/SM/WOFiles/" + objWorkOrder.WOId + "." + ext[1]); }
                //}
                FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Services/SRFiles/" + objServiceReport.SRId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
            }

            if (objServiceReport.ServiceReport_Update() == "Data Updated Successfully") 
            {
                foreach (GridViewRow gvrow in gvServiceItems.Rows)
                {
                
                    objServiceReport.SRNo = txtSRNo.Text;
                    objServiceReport.ItemCode = gvrow.Cells[2].Text;

                    objServiceReport.SRDET_DET_ID = gvrow.Cells[9].Text;


                    objServiceReport.ItemName = gvrow.Cells[3].Text;
                    if (gvrow.Cells[4].Text == "&nbsp;")
                    {
                        objServiceReport.SRDescription = "-";
                    }

                    else { objServiceReport.SRDescription = gvrow.Cells[4].Text; }
                    if (gvrow.Cells[5].Text == "&nbsp;")
                    {
                        objServiceReport.SRActionTaken = "-";
                    }
                    else { objServiceReport.SRActionTaken = gvrow.Cells[5].Text; }
                    if (gvrow.Cells[6].Text == "&nbsp;")
                    {
                        objServiceReport.SRFurtherActionReq = "-";
                    }
                    else { objServiceReport.SRFurtherActionReq = gvrow.Cells[6].Text; }
                    if (gvrow.Cells[11].Text == "&nbsp;")
                    {
                        objServiceReport.SRCustFeedback = "-";
                    }
                    else { objServiceReport.SRCustFeedback = gvrow.Cells[11].Text; }
                    objServiceReport.Status = gvrow.Cells[7].Text;
                    objServiceReport.DOC = gvrow.Cells[10].Text;

                    objServiceReport.ServiceReportItemDetails_Update();
                }
            }

            objServiceReport.ServiceReport_Update1();
            objServiceReport.ServiceReport_Update2();

            Services.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnSave.Text = "Save";
            btnDelete.Attributes.Clear();
            gvServiceReport.DataBind();
            //gvEnquiryProducts.DataBind();
            //  gvQuotationItems.DataBind();
            tblServiceReport.Visible = false;
            Services.ClearControls(this);
            Services.Dispose();
            gvServiceItems.DataSource = null;
            gvServiceItems.DataBind();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
        }

    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvServiceReport.SelectedIndex > -1)
        {
            Response.Redirect("ServiceReportNew.aspx?srNo=" + gvServiceReport.SelectedRow.Cells[0].Text);

            //Response.Redirect("ServiceReportNew.aspx?srNo=" + gvServiceReport.SelectedRow.Cells[0].Text + "&status" + gvServiceReport.SelectedRow.Cells[7].Text);
        //    try
        //    {
        //        Services.ServiceReport objServiceReport = new Services.ServiceReport();
        //        if (objServiceReport.ServiceReport_Select(gvServiceReport.SelectedRow.Cells[0].Text) > 0)
        //        {
        //            btnSave.Text = "Update";
        //            btnSave.Enabled = true;
        //            tblServiceReport.Visible = true;
        //            ddlCRNo.Enabled = false;

        //            rbServiceType.SelectedValue = objServiceReport.CRCallType;
        //            rbServiceType_SelectedIndexChanged(sender, e);
        //            txtSRNo.Text = objServiceReport.SRNo;
        //            txtSRDate.Text = objServiceReport.SRDate;
        //            ddlCRNo.SelectedValue = objServiceReport.CRId;
        //            ddlCRNo_SelectedIndexChanged(sender, e);
        //            txtCallType.Text = objServiceReport.CRCallType;
        //            ddlCustomerName.SelectedValue = objServiceReport.CustId;
        //            ddlCustomerName_SelectedIndexChanged(sender, e);
        //            ddlUnitName.SelectedValue = objServiceReport.CustUnitId;
        //            ddlUnitName_SelectedIndexChanged(sender, e);
        //            ddlContactPerson.SelectedValue = objServiceReport.CustDetId;
        //            txtServiceCenter.Text = objServiceReport.SRServiceCenter;
        //            if (rbServiceType.SelectedValue == "Installation" || rbServiceType.SelectedValue == "Warranty")
        //            {
        //                ddlOrderNo.SelectedValue = objServiceReport.SOId;
        //                btnCr.Enabled = true;
        //            }
        //            else if (rbServiceType.SelectedValue == "AMC")
        //            {
        //                ddlOrderNo.SelectedValue = objServiceReport.AMCOId;
        //                btnCr.Enabled = false;
        //            }
        //            else if (rbServiceType.SelectedValue == "Non Warranty")
        //            {
        //                ddlOrderNo.SelectedValue = objServiceReport.SPOId;
        //                btnCr.Enabled = false;
        //            }
        //            // ddlOrderNo_SelectedIndexChanged(sender, e);
        //            txtAMCRefNo.Text = objServiceReport.SRAMCRefNo;
        //            txtAMCVisitDate.Text = objServiceReport.SRAMCVisitDate;
        //            txtCompletionDate.Text = objServiceReport.SRCompletionDate;
        //            txtDescription.Text = objServiceReport.SRDescription;
        //            txtActionTaken.Text = objServiceReport.SRActionTaken;
        //            txtActionRequired.Text = objServiceReport.SRFurtherActionReq;
        //            txtCustFeedback.Text = objServiceReport.SRCustFeedback;
        //            ddlIsDocSubmitted.SelectedValue = objServiceReport.SRIsDocSubmited;
        //            txtServiceCompleted.Text = objServiceReport.SRServiceCompleted;
        //            ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objServiceReport.ItemCode);
        //            ddlItemType_SelectedIndexChanged(sender, e);
        //            //   ddlItemName.SelectedValue = objServiceReport.ItemCode;
        //            txtItemSLNo.Text = objServiceReport.SRItemSLNo;
        //            ddlVisitedBy.SelectedValue = objServiceReport.EmpId;

        //            //fuAttachments.Text = objServiceReport.SRFiles;

        //            ddlPreparedBy.SelectedValue = objServiceReport.SRPreparedBy;
        //            ddlApprovedBy.SelectedValue = objServiceReport.SRApprovedBy;


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        btnDelete.Attributes.Clear();
        //        Services.Dispose();

        //    }
        //}
        //else
        //{
        //    MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvServiceReport.SelectedIndex > -1)
        {
            try
            {
                Services.ServiceReport objServiceReport = new Services.ServiceReport();
                Services.BeginTransaction();
                objServiceReport.ServiceReportDetails_Delete(gvServiceReport.SelectedRow.Cells[10].Text);
               objServiceReport.ServiceReport_Delete(gvServiceReport.SelectedRow.Cells[0].Text);
            
                Services.CommitTransaction();
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvServiceReport.DataBind();
                Services.ClearControls(this);
                Services.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            SM.Dispose();
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
                //txtContactPerson.Text  = objSMCustomer.ContactPerson;
                txtCustUnitAddress.Text = objSMCustomer.CustUnitAddress;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region Button REFRESH  Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Services.ClearControls(this);
    }
    #endregion

    #region Button CLOSE  Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblServiceReport.Visible = false;
    }
    #endregion

    #region GridView Serivice Report Row DataBound
    protected void gvServiceReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[10].Visible = false;

        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "SR Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            meeSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            meeSearchFromDate.Enabled = false;
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
            meeSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            meeSearchFromDate.Enabled = false;
        }
    }
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvServiceReport.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvServiceReport.DataBind();
    }
    #endregion

    #region Item Type Select Index Changed
    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlItemType.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtModelName.Text = objMaster.ItemName;
                 txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;
            }
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlItemType.SelectedValue);
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

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvServiceReport.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=servicerpt&srid=" + gvServiceReport.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Send Button Click
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (gvServiceReport.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/ServicesReportViewer.aspx?type=quot&qno=" + gvServiceReport.SelectedRow.Cells[0].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region ddlCRNo_SelectedIndexChanged
    protected void ddlCRNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        Services.ComplaintRegister.complanitmodelnofill(ddlCRNo.SelectedItem.Value, ddlItemType);
        try
        {
            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
            if ((objComplaintRegister.ComplaintRegister_Select(ddlCRNo.SelectedItem.Value)) > 0)
            {
                txtCRDate.Text = objComplaintRegister.CRDate;
                txtCallType.Text = objComplaintRegister.CRCallType;
                ddlCustomerName.SelectedItem.Value = objComplaintRegister.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                txtCustName.Text = ddlCustomerName.SelectedItem.Text;
                ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
                txtUnitName.Text = ddlUnitName.SelectedItem.Text;
                if (Request.QueryString["SOId"] != null)
                {
                    if (objComplaintRegister.CRCallType == "Installation" )
                    {
                        rbServiceType.SelectedValue = "Installation";
                        SalesOrder_Fill();
                    }
                    else if (objComplaintRegister.CRCallType == "AMC")
                    {
                        rbServiceType.SelectedValue = "AMC";
                        AMCOrder_Fill();
                    }

                    else if (objComplaintRegister.CRCallType == "Non Warranty")
                    {
                        rbServiceType.SelectedValue = "Non Warranty";
                    }

                    else if (objComplaintRegister.CRCallType == "Warranty")
                    {
                        rbServiceType.SelectedValue = "Warranty";
                    }
                }
                
                txtCustUnitAddress.Text = objComplaintRegister.CustUnitAddress;
                txtDescription.Text = objComplaintRegister.CRComplaintNature;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.Visible = true;
                txtContactPerson.Visible = false;
                ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;

             
                
                if (txtUnitName.Text=="--")
                {
                   // txtContactPerson.Visible = true;
                   // ddlContactPerson.Visible = false;
                   // rfvContactPerson.Enabled = false;
                   // rfvUnitName.Enabled = false;
                    Label6.Text = "Customer Address";
                    SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                    if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
                    {
                        ddlContactPerson.Visible = false;
                        txtContactPerson.Text = objSMCustomer.ContactPerson;
                        txtContactPerson.Visible = true;
                        //txtRegion.Text = objSMCustomer.RegName;
                        //txtIndustryType.Text = objSMCustomer.IndType;
                        txtCustUnitAddress.Text = objSMCustomer.Address;
                       // txtEmail.Text = objSMCustomer.Email;
                       // txtPhoneNo.Text = objSMCustomer.Phone;
                       // txtMobile.Text = objSMCustomer.Mobile;
                    }
                }

                objComplaintRegister.ComplaintRegisterDetails_Select(ddlCRNo.SelectedItem.Value, gvQuotationItems);
                objComplaintRegister.ComplaintRegisterDetails_Select1(ddlCRNo.SelectedItem.Value, gvServiceItems);

                if (txtCallType.Text == "AMC")
                {
                    AMCOrder_Fill();
                    lblOrderNo.Text = "AMC Order No.";
                    lblOrderDate.Text = "AMC Order Date";
                    lblAMCRefNo.Text = "AMC Ref No.";
                    lblVisitDate.Text = "AMC Visit Date";
                    tblPreviousServiceRecords.Visible = true;
                }
                else if (txtCallType.Text == "Installation")
                {
                    SalesOrder_Fill();
                    lblOrderNo.Text = "Sales Order No.";
                    lblOrderDate.Text = "Sales Order Date";
                    lblAMCRefNo.Text = "Sales Ref No.";
                    lblVisitDate.Text = "Installation Date";
                    tblPreviousServiceRecords.Visible = true;
                }
                else if (txtCallType.Text == "Non Warranty")
                {
                    SparesOrder_Fill();
                    lblOrderNo.Text = "Spares Order No.";
                    lblOrderDate.Text = "Spares Order Date";
                    lblAMCRefNo.Text = "Spares Ref No.";
                    lblVisitDate.Text = "Visit Date";
                    tblPreviousServiceRecords.Visible = false;
                }
                else if (txtCallType.Text == "Warranty")
                {
                    SalesOrder_Fill();
                    lblOrderNo.Text = "Sales Order No.";
                    lblOrderDate.Text = "Sales Order Date";
                    lblAMCRefNo.Text = "Sales Ref No.";
                    lblVisitDate.Text = "Warranty Visit Date";
                    tblPreviousServiceRecords.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            Services.Dispose();
        }
        finally
        {
            if (txtSRNo.Text == String.Empty)
            {
                txtSRNo.Text = Services.ServiceReport.ServiceReport_AutoGenCode();
            }
        }
    }

    private void fillorderno()
    {
        if (lblAMCAId.Text != "0")
        {
            AMCOrder_Fill();
            lblOrderNo.Text = "AMC Order No.";
            lblOrderDate.Text = "AMC Order Date";
            lblAMCRefNo.Text = "AMC Ref No.";
            lblVisitDate.Text = "AMC Visit Date";
            tblPreviousServiceRecords.Visible = true;
        }
        else if (lblInsAId.Text != "0")
        {
            SalesOrder_Fill();
            lblOrderNo.Text = "Sales Order No.";
            lblOrderDate.Text = "Sales Order Date";
            lblAMCRefNo.Text = "Sales Ref No.";
            lblVisitDate.Text = "Installation Date";
            tblPreviousServiceRecords.Visible = true;
        }

    }

    private void SparesOrder_Fill()
    {
        try
        {
            Services.SparesOrder.SparesOrderByCRId_Select(ddlOrderNo, ddlCRNo.SelectedItem.Value);
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

    private void SalesOrder_Fill()
    {
        try
        {
            if (ddlCRNo.SelectedValue != "0")
            {
                SM.SalesOrder.SalesOrderByCustomerId_Select(ddlOrderNo, ddlCustomerName.SelectedValue, ddlUnitName.SelectedValue);
            }
            else
            {
                SM.SalesOrder.SalesOrder_Select(ddlOrderNo);
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

    private void AMCOrder_Fill()
    {
        try
        {
            if (ddlCRNo.SelectedValue != "0")
            {
                Services.AMCOrder.AMCOrderbyCRId_Select(ddlOrderNo, ddlCRNo.SelectedItem.Value);
            }
            else
            {
                Services.AMCOrder.AMCOrder_Select(ddlOrderNo);
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

    #region ddlOrderNo_SelectedIndexChanged
    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        btnSave.Enabled = true;
        if (txtCallType.Text == "AMC")
        {
            Services.AMCOrder objamco = new Services.AMCOrder();
            if (objamco.AMCOrder_Select(ddlOrderNo.SelectedItem.Value) > 0)
            {
                if (objamco.CustId != ddlCustomerName.SelectedValue)
                {
                    MessageBox.Show(this, "The AMC Order No. does not belongs to Customer : " + ddlCustomerName.SelectedItem.Text + "");
                    ddlOrderNo.SelectedValue = "0";
                }
                else
                {
                    txtOrderDate.Text = objamco.AMCODate;
                    txtAMCRefNo.Text = ddlOrderNo.SelectedItem.Text;
                    Services.ServiceReport.ServiceReportForPreviousRecords_Select(gvPreviousServiceRecords, rbServiceType.SelectedValue, ddlOrderNo.SelectedValue);
                }
            }
        }
        else if (txtCallType.Text == "Installation")
        {
            SM.SalesOrder objso = new SM.SalesOrder();
            if (objso.SalesOrder_Select(ddlOrderNo.SelectedValue) > 0)
            {
                if (objso.CustId != ddlCustomerName.SelectedValue)
                {
                    MessageBox.Show(this, "The Sales Order No. does not belongs to Customer : " + ddlCustomerName.SelectedItem.Text + "");
                    ddlOrderNo.SelectedValue = "0";
                }
                else
                {
                    //ddlOrderNo.SelectedValue = objso.SOId;
                    txtOrderDate.Text = objso.SODate;
                    txtAMCRefNo.Text = ddlOrderNo.SelectedItem.Text;
                    Services.ServiceReport.ServiceReportForPreviousRecords_Select(gvPreviousServiceRecords, rbServiceType.SelectedValue, ddlOrderNo.SelectedValue);

                }
            }
        }
        else if (txtCallType.Text == "Non Warranty")
        {
            Services.SparesOrder objspo = new Services.SparesOrder();
            if (objspo.SparesOrder_Select(ddlOrderNo.SelectedValue) > 0)
            {
                if (objspo.CustId != ddlCustomerName.SelectedValue)
                {
                    MessageBox.Show(this, "The Spares Order No. does not belongs to Customer : " + ddlCustomerName.SelectedItem.Text + "");
                    ddlOrderNo.SelectedValue = "0";
                }
                else
                {
                    txtOrderDate.Text = objspo.SPODate;
                    txtAMCRefNo.Text = ddlOrderNo.SelectedItem.Text;
                    gvPreviousServiceRecords.DataBind();
                }
            }
        }
        else if (txtCallType.Text == "Warranty")
        {
            SM.SalesOrder objso = new SM.SalesOrder();
            if (objso.SalesOrder_Select(ddlOrderNo.SelectedValue) > 0)
            {
                if (objso.CustId != ddlCustomerName.SelectedValue)
                {
                    MessageBox.Show(this, "The Sales Order No. does not belongs to Customer : " + ddlCustomerName.SelectedItem.Text + "");
                }
                else
                {
                    txtOrderDate.Text = objso.SODate;
                    txtAMCRefNo.Text = ddlOrderNo.SelectedItem.Text;
                    Services.ServiceReport.ServiceReportForPreviousRecords_Select(gvPreviousServiceRecords, rbServiceType.SelectedValue, ddlOrderNo.SelectedValue);
                }
            }
        }
    }
    #endregion

    #region Radio Button Service Type Selected Index
    protected void rbServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        lblCRNo.Visible = lblCRDate.Visible = ddlCRNo.Visible = txtCRDate.Visible = true;
        CRNo_Fill();
    }
    #endregion

    #region Radio Button Warranty Selected Index
    protected void rbWarrantySelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbWarrantySelect.SelectedIndex == 0)
        {
            lblSRNo1.Visible = lblSRDate1.Visible = ddlSRNo.Visible = txtSRDate1.Visible = false;
            ddlOrderNo.Enabled = ddlCRNo.Enabled = true;
        }
        else if (rbWarrantySelect.SelectedIndex == 1)
        {
            lblSRNo1.Visible = lblSRDate1.Visible = ddlSRNo.Visible = txtSRDate1.Visible = true;
            ddlOrderNo.Enabled = ddlCRNo.Enabled = false;
        }
    }
    #endregion

    #region Ddl SrNo Selected Index
    protected void ddlSRNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.ServiceReport objServiceReport = new Services.ServiceReport();
            if (objServiceReport.ServiceReport_Select(ddlSRNo.SelectedValue) > 0)
            {
                btnSave.Enabled = true;
                tblServiceReport.Visible = true;
                rbServiceType.SelectedValue = objServiceReport.CRCallType;
                rbServiceType_SelectedIndexChanged(sender, e);
                txtSRDate1.Text = objServiceReport.SRDate;
                ddlCRNo.SelectedValue = objServiceReport.CRId;
                ddlCRNo_SelectedIndexChanged(sender, e);
                txtCallType.Text = objServiceReport.CRCallType;
                ddlCustomerName.SelectedValue = objServiceReport.CustId;
                ddlCustomerName_SelectedIndexChanged(sender, e);
                txtCustName.Text = ddlCustomerName.SelectedItem.Text;
                ddlUnitName.SelectedValue = objServiceReport.CustUnitId;
                ddlUnitName_SelectedIndexChanged(sender, e);
                ddlContactPerson.SelectedValue = objServiceReport.CustDetId;
                if (rbServiceType.SelectedValue == "Installation" || rbServiceType.SelectedValue == "Warranty")
                {
                    ddlOrderNo.SelectedValue = objServiceReport.SOId;
                }
                else if (rbServiceType.SelectedValue == "AMC")
                {
                    ddlOrderNo.SelectedValue = objServiceReport.AMCOId;
                }
                else if (rbServiceType.SelectedValue == "Non Warranty")
                {
                    ddlOrderNo.SelectedValue = objServiceReport.SPOId;
                }
                ddlOrderNo_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            Services.Dispose();

        }
    }
    #endregion

    #region gvQuotaiotnsItems Row Databound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
    #endregion

    #region gvQuotations Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemType"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["SerialNo"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["ItemTypeId"] = gvrow.Cells[7].Text;
                    dr["NatureofComplaint"] = gvrow.Cells[8].Text;
                    dr["RootCausedNotice"] = gvrow.Cells[9].Text;
                    dr["CorrectiveActionTaken"] = gvrow.Cells[10].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region gvQuotationItems Row Editing
    protected void gvQuotationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
       QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ItemType"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["SerialNo"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["ItemTypeId"] = gvrow.Cells[7].Text;
                dr["NatureofComplaint"] =gvrow.Cells[8].Text;
                dr["RootCausedNotice"]=gvrow.Cells[9].Text;
                dr["CorrectiveActionTaken"] = gvrow.Cells[10].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedValue = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    //ddlItemName.SelectedValue = gvrow.Cells[2].Text;
                    txtQuantity.Text = gvrow.Cells[6].Text;
                    txtDescription.Text = gvrow.Cells[8].Text;
                    txtActionRequired.Text = gvrow.Cells[9].Text;
                    txtActionTaken.Text = gvrow.Cells[10].Text;
                    if (gvrow.Cells[5].Text != "-")
                    { txtItemSLNo.Text = gvrow.Cells[5].Text; }
                    else
                    { txtItemSLNo.Text = ""; }
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region gvServiceItems Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtQuantity.Text == string.Empty)
        {
            MessageBox.Show(this, "Please Enter Quantity");
            return;
        }
        if (txtDescription.Text == string.Empty)
        {
            txtDescription.Text = "-";
            }
        if (txtActionRequired.Text == string.Empty)
        { txtActionRequired.Text = "-"; }
        if (txtActionTaken.Text ==string.Empty)
        { txtActionTaken.Text = "-"; }
        if (txtCustFeedback.Text == string.Empty)
        { txtCustFeedback.Text = "-"; }
        
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Description");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ActionTaken");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("FurtherAction");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Status");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SER_DET_ID");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Date Of Comp");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CustomerFeedBack");
        QuotationItems.Columns.Add(col);
        if (gvServiceItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvServiceItems.Rows)
            {
                if (gvServiceItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvServiceItems.SelectedRow.RowIndex)
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        //dr["ItemCode"] = ddlItemName.SelectedItem.Value;
                        dr["ItemName"] = txtModelName.Text;
                        dr["Description"] = txtDescription.Text;
                        dr["ActionTaken"] = txtActionTaken.Text;
                        dr["FurtherAction"] = txtActionRequired.Text;
                        dr["Status"] = ddlServiceStatus.Text;
                        dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        dr["SER_DET_ID"] = txtSER_DET_DETID.Text;
                        dr["Date Of Comp"] = txtCompletionDate.Text;
                        dr["CustomerFeedBack"] = txtCustFeedback.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemType"] = gvrow.Cells[2].Text;
                        dr["ItemName"] = gvrow.Cells[3].Text;
                        dr["Description"] = gvrow.Cells[4].Text;
                        dr["ActionTaken"] = gvrow.Cells[5].Text;
                        dr["FurtherAction"] = gvrow.Cells[6].Text;
                        dr["Status"] = gvrow.Cells[7].Text;
                        dr["ItemTypeId"] = gvrow.Cells[8].Text;
                        dr["SER_DET_ID"] = gvrow.Cells[9].Text;
                        dr["Date Of Comp"] = gvrow.Cells[10].Text;
                        dr["CustomerFeedBack"] = gvrow.Cells[11].Text;
                 
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemType"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["Description"] = gvrow.Cells[4].Text;
                    dr["ActionTaken"] = gvrow.Cells[5].Text;
                    dr["FurtherAction"] = gvrow.Cells[6].Text;
                    dr["Status"] = gvrow.Cells[7].Text;
                    dr["ItemTypeId"] = gvrow.Cells[8].Text;
                    dr["SER_DET_ID"] = gvrow.Cells[9].Text;
                    dr["Date Of Comp"] = gvrow.Cells[10].Text;
                    dr["CustomerFeedBack"] = gvrow.Cells[11].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }

        if (gvServiceItems.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();
            drnew["ItemType"] = ddlItemType.SelectedItem.Text;
            drnew["ItemName"] = txtModelName.Text;
            drnew["Description"] = txtDescription.Text;
            drnew["ActionTaken"] = txtActionTaken.Text;
            drnew["FurtherAction"] = txtActionRequired.Text;
            drnew["Status"] = ddlServiceStatus.Text;
            drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            drnew["SER_DET_ID"] = txtSER_DET_DETID.Text;
            drnew["Date Of Comp"] = txtCompletionDate.Text;
            drnew["CustomerFeedBack"] = txtCustFeedback.Text;
            if (txtDescription.Text == "&nbsp;")
            {
               // txtDescription.Text = "-";
               // drnew["Description"] = txtDescription.Text;
                drnew["Description"] = "-";
            }
            else
            {
                drnew["Description"] = txtDescription.Text;
            }
            if (txtActionTaken.Text == "&nbsp;")
            {
               // txtDescription.Text = "-";
                //drnew["ActionTaken"] = txtActionTaken.Text;
                drnew["ActionTaken"] = "-";
                 
            }
            else
            {
                drnew["ActionTaken"] = txtActionTaken.Text;
            }
            if (txtActionRequired.Text == "&nbsp;")
            {
               // txtActionTaken.Text = "-";
              //  drnew["FurtherAction"] = txtActionTaken.Text;
                drnew["FurtherAction"] = "-";
            }
            else
            {
                drnew["FurtherAction"] = txtActionRequired.Text;
            }
            if (txtCompletionDate.Text == "")
            {
                // txtActionTaken.Text = "-";
                //  drnew["FurtherAction"] = txtActionTaken.Text;
                drnew["Date Of Comp"] = "-";
            }
            else
            {
                drnew["Date Of Comp"] = txtCompletionDate.Text;
            }
            if (txtCustFeedback.Text == "")
            {
                // txtActionTaken.Text = "-";
                //  drnew["FurtherAction"] = txtActionTaken.Text;
                drnew["CustomerFeedBack"] = "-";
            }
            else
            {
                drnew["CustomerFeedBack"] = txtCustFeedback.Text;
            }
            QuotationItems.Rows.Add(drnew);
        }
        gvServiceItems.DataSource = QuotationItems;
        gvServiceItems.DataBind();
        gvServiceItems.SelectedIndex = -1;
        //btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region gvServiceItems Row Databound
    protected void gvServiceItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Service list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
    }
    #endregion

    #region gcService Items Row Deleting
    protected void gvServiceItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvServiceItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Description");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ActionTaken");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("FurtherAction");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Status");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SER_DET_ID");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Date Of Comp");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CustomerFeedBack");
        QuotationItems.Columns.Add(col);
        

        if (gvServiceItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvServiceItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemType"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["Description"] = gvrow.Cells[4].Text;
                    dr["ActionTaken"] = gvrow.Cells[5].Text;
                    dr["FurtherAction"] = gvrow.Cells[6].Text;
                    dr["Status"] = gvrow.Cells[7].Text;
                    dr["ItemTypeId"] = gvrow.Cells[8].Text;
                    dr["SER_DET_ID"] = gvrow.Cells[9].Text;
                    dr["Date Of Comp"] = gvrow.Cells[10].Text;
                    dr["CustomerFeedBack"] = gvrow.Cells[11].Text;

                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvServiceItems.DataSource = QuotationItems;
        gvServiceItems.DataBind();
    }
    #endregion

    #region gcServiceItems Row Editing
    protected void gvServiceItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ItemTypes_Fill();
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemType");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Description");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ActionTaken");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("FurtherAction");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Status");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("ItemTypeId");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("SER_DET_ID");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Date Of Comp");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CustomerFeedBack");
        QuotationItems.Columns.Add(col);
        if (gvServiceItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvServiceItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();
                dr["ItemType"] = gvrow.Cells[2].Text;
                dr["ItemName"] = gvrow.Cells[3].Text;

                if (gvrow.Cells[4].Text == "&nbsp;")
                {
                    gvrow.Cells[4].Text = "-";
                    dr["Description"] =  gvrow.Cells[4].Text;
                }
                else
                {
                    dr["Description"] = gvrow.Cells[4].Text;
                }
                if (gvrow.Cells[5].Text == "&nbsp;")
                {
                    gvrow.Cells[5].Text = "-";
                    dr["ActionTaken"] = gvrow.Cells[5].Text;
                }
                else
                {
                    dr["ActionTaken"] = gvrow.Cells[5].Text;
                }
                if (gvrow.Cells[6].Text == "&nbsp;")
                {
                    gvrow.Cells[6].Text = "-";
                    dr["FurtherAction"] = gvrow.Cells[6].Text;
                }
                else
                {
                    dr["FurtherAction"] = gvrow.Cells[6].Text;
                }
                if (gvrow.Cells[10].Text == "&nbsp;")
                {
                    gvrow.Cells[10].Text = "-";
                    dr["Date Of Comp"] = gvrow.Cells[10].Text;
                }
                else
                {
                    dr["Date Of Comp"] = gvrow.Cells[10].Text;
                }
                if (gvrow.Cells[11].Text == "&nbsp;")
                {
                    gvrow.Cells[11].Text = "-";
                    dr["CustomerFeedBack"] = gvrow.Cells[11].Text;
                }
                else
                {
                    dr["CustomerFeedBack"] = gvrow.Cells[11].Text;
                }
               // dr["ActionTaken"] = gvrow.Cells[5].Text;
               // dr["FurtherAction"] = gvrow.Cells[6].Text;
                dr["Status"] = gvrow.Cells[7].Text;
                dr["ItemTypeId"] = gvrow.Cells[8].Text;
              dr["SER_DET_ID"] = gvrow.Cells[9].Text;
              dr["Date Of Comp"] = gvrow.Cells[10].Text;
              dr["CustomerFeedBack"] = gvrow.Cells[11].Text;
                QuotationItems.Rows.Add(dr);

                if (gvrow.RowIndex == gvServiceItems.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlItemType.SelectedItem.Text = gvrow.Cells[2].Text;
                    ddlItemType_SelectedIndexChanged(sender, e);
                    //ItemName_Fill();
                    //ddlItemName.SelectedItem.Text = gvrow.Cells[3].Text;
                    txtDescription.Text = gvrow.Cells[4].Text;
                    if (gvrow.Cells[5].Text != "-")
                    { txtActionTaken.Text = gvrow.Cells[5].Text; }
                    else
                    { txtActionTaken.Text = ""; }
                    
                
                    if (gvrow.Cells[6].Text != "-")
                    { txtActionRequired.Text = gvrow.Cells[6].Text; }
                    else
                    { txtActionRequired.Text = ""; }
                    if (gvrow.Cells[11].Text != "-")
                    { txtCustFeedback.Text = gvrow.Cells[11].Text; }
                    else
                    { txtCustFeedback.Text= ""; }
                    ddlServiceStatus.SelectedValue = gvrow.Cells[7].Text.Trim();
                    txtSER_DET_DETID.Text = gvrow.Cells[9].Text;
                    txtCompletionDate.Text = gvrow.Cells[10].Text;
                    txtCustFeedback.Text = gvrow.Cells[11].Text;
                    gvServiceItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvServiceItems.DataSource = QuotationItems;
        gvServiceItems.DataBind();
    }
    #endregion

    #region Complaint Reg
    protected void btnCr_Click(object sender, EventArgs e)
    {
        Response.Redirect("ComplaintRegister.aspx?crid=" + ddlCRNo.SelectedValue + "");
    }
    #endregion

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSRNo1;
        lbtnSRNo1 = (LinkButton)sender;
        //   btnSave.Enabled = false;
        GridViewRow gvRow = (GridViewRow)lbtnSRNo1.Parent.Parent;
        gvServiceItems.SelectedIndex = gvRow.RowIndex;
        if (gvServiceReport.SelectedIndex > -1)
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=servicerpt&srid=" + gvServiceReport.SelectedRow.Cells[0].Text + "&srid2=" + gvServiceItems.SelectedRow.Cells[9].Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }





    #region  CR No Fill
    private void CRNo1_Fill()
    {
      
            try
            {
                Services.ComplaintRegister.ComplaintRegisterForInstallation_Select1(ddlCRNo);
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



    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlItemType, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value);
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlItemType.DataSourceID = "SqlDataSource2";
        ddlItemType.DataTextField = "ITEM_MODEL_NO";
        ddlItemType.DataValueField = "ITEM_CODE";
        ddlItemType.DataBind();
        ddlItemType_SelectedIndexChanged(sender, e);
    }
}

    

 
