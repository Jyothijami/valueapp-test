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
using System.IO;
using System.Data.SqlClient;
using vllib;
using YantraDAL;
public partial class Modules_Services_ComplaintRegister : basePage
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerMaster_Fill();
            EmployeeMaster_Fill();
            //Region_Fill();
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblEmpIdHidden.Text = lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            tblComplaintRegister.Visible = false;
           // ddlstatus.SelectedIndex = 1;
            gvComplaintRegister.DataBind();
            setControlsVisibility();
            rdblIndentfor_SelectedIndexChanged(sender, e);
            lblStartOTP.Text = RandomGenerator().ToString ();
            lblClosedOTP.Text = RandomGenerator1().ToString();
            lblInTime.Text = DateTime.Now.ToString();
            lblOutTime.Text = DateTime.Now.ToString();
            txtTechinTime.Text = DateTime.Now.ToString();
            txtTechOutTime.Text = DateTime.Now.ToString();
        }
    }
    #endregion
    protected int RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 9999);
        return intRandomNumber;
    }
    protected int RandomGenerator1()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0102, 9999);
        return intRandomNumber;
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "32");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnAssign.Enabled = up.Assign;

    }

    #region PAGE PRERENDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            if (!string.IsNullOrEmpty(gvComplaintRegister.SelectedRow.Cells[6].Text) && gvComplaintRegister.SelectedRow.Cells[6].Text != "&nbsp;")
            {
                // btnSave.Visible = false;
                btnRefresh.Visible = false;

            }
            else
            {
                btnSave.Visible = true;
                btnRefresh.Visible = false;

            }
        }
        else
        {
            btnSave.Visible = true;
            btnRefresh.Visible = true;

        }
    }
    #endregion

    //#region Region_Fill
    //private void Region_Fill()
    //{
    //    try
    //    {
    //        Masters.RegionalMaster.RegionalMaster_Select(ddlRegion);
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


    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.Services_CustomerDdlBind(ddlCustomerName);
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



    #region Unit Name Fill
    private void UnitName_Fill()
    {
        try
        {
            SM.CustomerMaster.ServiceUnitName_Bind(ddlUnitName, ddlCustomerName.SelectedItem.Value);
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
            // HR.EmployeeMaster.EmployeeMaster_Select(ddlResponsiblePerson);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlSalesPerson);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            //  HR.EmployeeMaster.EmployeeMaster_Select(ddlCheckedBy);\
            // HR.EmployeeMaster.EmployeeMaster_ServiceSelect(ddlAttendBy);
            //HR.EmployeeMaster.EmployeeMaster_SelectCustomerCare(ddlAttendBy);
            HR.EmployeeMaster.EmployeeMaster_SelectTechnical(ddlAttendBy);
            HR.EmployeeMaster.EmployeeMaster_SelectTechnical(ddlCrAttendBy);
            HR.EmployeeMaster.EmployeeMaster_Select_Compalint(ddlPreparedBy);
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectTechnical(ddlEmpNameForAssign);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesExecutive);
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

    #region Link Button lbtnCRNo_Click
    protected void lbtnCRNo_Click(object sender, EventArgs e)
    {
        CustomerMaster_Fill();
        tblComplaintRegister.Visible = false;
        LinkButton lbtnCRNo;
        lbtnCRNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCRNo.Parent.Parent;
        gvComplaintRegister.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");


        Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

        if (objComplaintRegister.ComplaintRegister_Select1(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
        {
            btnSave.Enabled = false;
            btnSave.Text = "Update";
            tblComplaintRegister.Visible = true;
            txtCRNo.Text = objComplaintRegister.CRNo;
            txtCRDate.Text = objComplaintRegister.CRDate;
            ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
            //ddlRegion.SelectedValue = objComplaintRegister.RegId;
            ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
            ddlCustomerName_SelectedIndexChanged(sender, e);
            ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
            ddlUnitName_SelectedIndexChanged(sender, e);
            if (objComplaintRegister.CustDetId != "0")
            {
                rdblIndentfor.SelectedValue = "Executive";
                rdblIndentfor_SelectedIndexChanged(sender, e);
                ddlSalesExecutive.SelectedValue = objComplaintRegister.CustDetId;
                lblEditExe.Text = objComplaintRegister.CustDetId;
                ddlSalesExecutive_SelectedIndexChanged(sender, e);
            }
            else { rdblIndentfor.SelectedValue = "Client"; }
            //ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
            // ddlContactPerson_SelectedIndexChanged(sender, e);
            //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
            //ddlItemType_SelectedIndexChanged(sender, e);
            //     ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
            txtCompletedDate.Text = objComplaintRegister.CompletedDate;
            ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;
            ddlAttendBy.SelectedValue = objComplaintRegister.attendby;
            lblEditTech.Text = objComplaintRegister.attendby;
            ddlAttendBy_SelectedIndexChanged(sender, e);
            if (objComplaintRegister.CRSatus == "ReOpened")
            {
                ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
            }
            else if (objComplaintRegister.CRSatus == "Open")
            {
                ddlstatus.Items[4].Attributes.Add("disabled", "disabled");
            }
            else if (objComplaintRegister.CRSatus == "Closed")
            {
                ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
                ddlstatus.Items[2].Attributes.Add("disabled", "disabled");
                ddlstatus.Items[3].Attributes.Add("disabled", "disabled");
            }
            else if (objComplaintRegister.CRSatus == "Pending")
            {
                ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
                ddlstatus.Items[4].Attributes.Add("disabled", "disabled");
            }
            ddlstatus.SelectedValue = objComplaintRegister.CRSatus;
            lblEditStatus.Text = objComplaintRegister.CRSatus;
            lblCRIDHidden.Text = objComplaintRegister.CRId;
            objComplaintRegister.ComplaintRegisterDetails_SelectDet(gvComplaintRegister.SelectedRow.Cells[0].Text, gvQuotationItems);
        }

    }
    #endregion



    #region Button NEW  Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            gvComplaintRegister.SelectedIndex = -1;
            //Services.ClearControls(this);
            btnSendSMS.Enabled = false;
            txtCRNo.Text = Services.ComplaintRegister.ComplaintRegister_AutoGenCode();
            lblCRIDHidden.Text = Services.ComplaintRegister.ComplaintRegister_AutoGenCode1();
            txtCRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtCompletedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Text = "Save";
            btnSave.Enabled = true;
            tblComplaintRegister.Visible = true;
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
            ddlstatus.SelectedValue  = "Open";
            ddlstatus.Enabled = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ComplaintRegisterSave();
            Response.Redirect("/Modules/Services/ComplaintRegister.aspx");
        }
        else if (btnSave.Text == "Update")
        {
            ComplaintRegisterUpdate();
            Response.Redirect("/Modules/Services/ComplaintRegister.aspx");

        }
    }
    #endregion

    #region Complaint Register Save
    private void ComplaintRegisterSave()
    {
        try
        {
            Services.BeginTransaction();
            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
            objComplaintRegister.CRNo = txtCRNo.Text;
            objComplaintRegister.CRDate = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
            objComplaintRegister.CRCallType = ddlCallType.SelectedItem.Text;
            objComplaintRegister.CustId = ddlCustomerName.SelectedItem.Value;
            objComplaintRegister.CustUnitId = ddlUnitName.SelectedItem.Value;
            if (rdblIndentfor.SelectedItem.Text == "Executive")
            {
                objComplaintRegister.CustDetId = ddlSalesExecutive.SelectedItem.Value;
            }
            else { objComplaintRegister.CustDetId = "0"; }
           
            objComplaintRegister.CRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objComplaintRegister.Cp_Id = lblCPID.Text;
            objComplaintRegister.attendby = ddlAttendBy.SelectedItem.Value;
            objComplaintRegister.CRStartOTP = lblStartOTP.Text;
            objComplaintRegister.CRClosedOTP = lblClosedOTP.Text;
            objComplaintRegister.CRINTIME = lblInTime.Text;
            objComplaintRegister.CROutTime = lblOutTime.Text;
            objComplaintRegister.TECHStatus = ddlstatus.SelectedItem.Value;
            objComplaintRegister.TechRemarks = "";
            objComplaintRegister.CRPendingFollowUpDt = "1990/01/01";
            objComplaintRegister.CRPendingReason = "";
            if (ddlstatus.SelectedIndex == 0)
            {
                ddlstatus.SelectedIndex = 1;
            }
            objComplaintRegister.CRSatus = ddlstatus.SelectedItem.Value;

            objComplaintRegister.CompletedDate = Yantra.Classes.General.toMMDDYYYY(txtCompletedDate.Text);

            if (objComplaintRegister.ComplaintRegister_Save1() == "Data Saved Successfully")
            {
                objComplaintRegister.ComplaintRegisterDetails_Delete(objComplaintRegister.CRId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objComplaintRegister.ItemCode = gvrow.Cells[2].Text;
                    objComplaintRegister.CRDetQty = gvrow.Cells[3].Text;
                    objComplaintRegister.CRComplaintNature = gvrow.Cells[4].Text;
                    TextBox Feedback = (TextBox)gvrow.FindControl("txtFeedback");
                    objComplaintRegister.CRRootCause = Feedback.Text;
                    TextBox CRCorrectiveAction = (TextBox)gvrow.FindControl("txtactionTaken");
                    objComplaintRegister.CRCorrectiveAction = CRCorrectiveAction.Text;
                    //objComplaintRegister.CRRootCause = gvrow.Cells[5].Text;
                    //objComplaintRegister.CRCorrectiveAction = gvrow.Cells[6].Text;
                    objComplaintRegister.attendby = gvrow.Cells[7].Text;
                    objComplaintRegister.CRStartOTP = lblStartOTP.Text;
                    objComplaintRegister.CRClosedOTP = lblClosedOTP.Text;
                    objComplaintRegister.CRINTIME = lblInTime.Text;
                    objComplaintRegister.CROutTime = lblOutTime.Text;
                    objComplaintRegister.TechInTime = DateTime.Now.ToString();
                    objComplaintRegister.TechOutTime = DateTime.Now.ToString();
                    objComplaintRegister.ComplaintRegisterDetails_Save1();
                }
                //SendSMSToCustomer();
                //SendSMSToTechnician();
                //SendSMSToExecutive();
                SendSMS();
                
                #region  Attachment

                if (Uploadattach.HasFiles)
                {

                    string Attachment = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServicesComplaintRegisterAttachments"))
                    {

                        foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ServicesComplaintRegisterAttachments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            objComplaintRegister.RegAttachments = Attachment;
                            objComplaintRegister.ComplaintRegisterAttachment_Save();

                        }

                    }
                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServicesComplaintRegisterAttachments");
                        foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ServicesComplaintRegisterAttachments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            objComplaintRegister.RegAttachments = Attachment;
                            objComplaintRegister.ComplaintRegisterAttachment_Save();

                        }

                    }

                    #endregion
                   
                }
            }
            Services.CommitTransaction();
            MessageBox.Show(this, "Data Saved Successfully");
            //Send SMS To Customer
            
            tblComplaintRegister.Visible = false;
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
            gvComplaintRegister.DataBind();
        }

    }
    #endregion

    #region Complaint Register Update
    private void ComplaintRegisterUpdate()
    {
        try
        {

            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

            objComplaintRegister.CRId = gvComplaintRegister.SelectedRow.Cells[0].Text;

            objComplaintRegister.CRNo = txtCRNo.Text;
            objComplaintRegister.CRDate = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
            objComplaintRegister.CRCallType = ddlCallType.SelectedItem.Text;
            objComplaintRegister.CustId = ddlCustomerName.SelectedItem.Value;
            objComplaintRegister.CustUnitId = ddlUnitName.SelectedItem.Value;
            //objComplaintRegister.attendby = ddlAttendBy.SelectedItem.Value;
            objComplaintRegister.CRStartOTP = lblStartOTP.Text;
            objComplaintRegister.CRClosedOTP = lblClosedOTP.Text;
            objComplaintRegister.CRINTIME = lblInTime.Text;
            objComplaintRegister.CROutTime = lblOutTime.Text;
           
            objComplaintRegister.TechRemarks = "";
            objComplaintRegister.CRPendingFollowUpDt = "1990/01/01";
            objComplaintRegister.CRPendingReason = "";
            if (rdblIndentfor.SelectedItem.Text == "Executive")
            {
                objComplaintRegister.CustDetId = ddlSalesExecutive.SelectedItem.Value;
            }
            else { objComplaintRegister.CustDetId = "0"; }
            objComplaintRegister.CRSatus = ddlstatus.SelectedItem.Value;
            objComplaintRegister.attendby = ddlAttendBy.SelectedItem.Value;
            objComplaintRegister.CRPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            objComplaintRegister.Cp_Id = lblCPID.Text;
            objComplaintRegister.CompletedDate = Yantra.Classes.General.toMMDDYYYY(txtCompletedDate.Text);

            if (objComplaintRegister.ComplaintRegister_Update_mast() == "Data Updated Successfully")
            {
                objComplaintRegister.ComplaintRegisterDetails_DeleteDet(objComplaintRegister.CRId);
                foreach (GridViewRow gvrow in gvQuotationItems.Rows)
                {
                    objComplaintRegister.ItemCode = gvrow.Cells[2].Text;
                    objComplaintRegister.CRDetQty = gvrow.Cells[3].Text;
                    objComplaintRegister.CRComplaintNature = gvrow.Cells[4].Text;
                    TextBox Feedback = (TextBox)gvrow.FindControl("txtFeedback");
                    objComplaintRegister.CRRootCause = Feedback.Text;
                    TextBox CRCorrectiveAction = (TextBox)gvrow.FindControl("txtactionTaken");
                    objComplaintRegister.CRCorrectiveAction = CRCorrectiveAction.Text;
                    //objComplaintRegister.CRRootCause = gvrow.Cells[5].Text;
                    //objComplaintRegister.CRCorrectiveAction = gvrow.Cells[6].Text;
                    objComplaintRegister.attendby = gvrow.Cells[7].Text;
                    objComplaintRegister.TechInTime = DateTime.Now.ToString();
                    objComplaintRegister.TechOutTime = DateTime.Now.ToString();

                    objComplaintRegister.ComplaintRegisterDetails_Save1();
                }

                #region Attachment

                if (Uploadattach.HasFiles)
                {

                    string Attachment = "";
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServicesComplaintRegisterAttachments"))
                    {

                        foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ServicesComplaintRegisterAttachments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            objComplaintRegister.RegAttachments = Attachment;
                            objComplaintRegister.ComplaintRegisterAttachment_Save();

                        }

                    }
                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServicesComplaintRegisterAttachments");
                        foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ServicesComplaintRegisterAttachments/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            objComplaintRegister.RegAttachments = Attachment;
                            objComplaintRegister.ComplaintRegisterAttachment_Save();

                        }

                    }

                    #endregion
                    
                }
            }
            if (lblEditStatus.Text != ddlstatus.SelectedValue)
            {
                SendSMSUpdateToExecutive();
                SendSMSUpdateToTechnician();
                SendSMSUpdateToCustomer();
            }
            else if (lblEditStatus.Text == ddlstatus.SelectedValue)
            {
                if (lblEditTech.Text != ddlAttendBy.SelectedValue)
                {
                    SendSMSUpdateToCustomer();
                   
                    SendSMSUpdateToTechnician();
                }
                if (lblEditExe.Text != ddlSalesExecutive.SelectedValue)
                {
                    SendSMSUpdateToCustomer();
                    SendSMSUpdateToExecutive();
                }
            }
            
            Services.CommitTransaction();
            MessageBox.Show(this, "Data Updated Successfully");
            tblComplaintRegister.Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
            gvQuotationItems.DataSource = null;
            gvQuotationItems.DataBind();
            Response.Redirect("../Services/ComplaintRegister.aspx");
        }
    }
    #endregion

    #region Button EDIT  Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            try
            {
                Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();

                if (objComplaintRegister.ComplaintRegister_Select1(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
                {
                    tblComplaintRegister.Visible = true;
                    btnRefresh.Visible = true;
                    btnSave.Visible = true;
                    btnClose.Visible = true;
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;
                    btnSendSMS.Enabled = true;
                    txtCRNo.Text = objComplaintRegister.CRNo;
                    txtCRDate.Text = objComplaintRegister.CRDate;
                    ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
                    ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
                    ddlCustomerName_SelectedIndexChanged(sender, e);
                    ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
                    ddlUnitName_SelectedIndexChanged(sender, e);
                    if (objComplaintRegister.CustDetId != "0")
                    {
                        rdblIndentfor.SelectedValue = "Executive";
                        rdblIndentfor_SelectedIndexChanged(sender, e);
                        ddlSalesExecutive.SelectedValue = objComplaintRegister.CustDetId;
                        lblEditExe.Text = objComplaintRegister.CustDetId;
                        ddlSalesExecutive_SelectedIndexChanged(sender, e);
                    }
                    else { rdblIndentfor.SelectedValue = "Client"; }
                    // ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
                    //ddlContactPerson_SelectedIndexChanged(sender, e);
                    //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
                    //ddlItemType_SelectedIndexChanged(sender, e);
                    //      ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
                    txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
                    txtRootCause.Text = objComplaintRegister.CRRootCause;
                    txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
                    ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;
                    ddlAttendBy.SelectedValue = objComplaintRegister.attendby;
                    lblEditTech.Text = objComplaintRegister.attendby;
                    ddlAttendBy_SelectedIndexChanged(sender, e);
                    if (objComplaintRegister.CRSatus == "ReOpened")
                    {
                        ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
                    }
                    else if (objComplaintRegister.CRSatus == "Open")
                    {
                        ddlstatus.Items[4].Attributes.Add("disabled", "disabled");
                    }
                    else if (objComplaintRegister.CRSatus == "Closed")
                    {
                        ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
                        ddlstatus.Items[2].Attributes.Add("disabled", "disabled");
                        ddlstatus.Items[3].Attributes.Add("disabled", "disabled");
                    }
                    else if (objComplaintRegister.CRSatus == "Pending")
                    {
                        ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
                        ddlstatus.Items[4].Attributes.Add("disabled", "disabled");
                    }
                    else if (objComplaintRegister.CRSatus == "Manual Closing")
                    {
                        ddlstatus.Items[1].Attributes.Add("disabled", "disabled");
                        ddlstatus.Items[2].Attributes.Add("disabled", "disabled");
                        ddlstatus.Items[3].Attributes.Add("disabled", "disabled");
                        ddlstatus.Items[4].Attributes.Add("disabled", "disabled");
                    }
                    ddlstatus.SelectedValue = objComplaintRegister.CRSatus;
                    lblEditStatus.Text = objComplaintRegister.CRSatus;
                    
                    txtCompletedDate.Text = objComplaintRegister.CompletedDate;
                    lblCRIDHidden.Text = objComplaintRegister.CRId;
                    objComplaintRegister.ComplaintRegisterDetails_SelectDet(gvComplaintRegister.SelectedRow.Cells[0].Text, gvQuotationItems);

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
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE  Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            try
            {
                Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
                objComplaintRegister.CRId = gvComplaintRegister.SelectedRow.Cells[0].Text;
                MessageBox.Show(this, objComplaintRegister.ComplaintRegister_Delete());
            }
            catch (Exception ex)
            {
                Services.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvComplaintRegister.DataBind();
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

    #region GridView Quotation Products Row Databound
    protected void gvQuotationProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
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
        tblComplaintRegister.Visible = false;
    }
    #endregion

    #region GridView gvComplaintRegister_RowDataBound
    protected void gvComplaintRegister_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "CR Date")
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
    #endregion

    #region DropDownList Symbols Select Index Changed
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
    #endregion

    #region Search Go Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvComplaintRegister.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "CR Date")
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
        gvComplaintRegister.DataBind();
    }
    #endregion

    #region ddlCustomerName_SelectedIndexChanged
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.ServiceUnitName_Bind(ddlUnitName, ddlCustomerName.SelectedItem.Value);
            Services.ServiceCustInfo objServiceCust = new Services.ServiceCustInfo();

            if (ddlUnitName.Items.Count > 0)
            {
                lblUnitAddress.Text = "Unit Address";
            }
            else
            {
                lblUnitAddress.Text = "Customer Address";
                if (objServiceCust.ServiceCustMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
                {
                    //Bind Customer Details
                    txtUnitAddress.Text = objServiceCust.Cust_Address;
                    txtUnitContactPerson.Text = objServiceCust.Cust_Contact_Person;
                    txtEmail.Text = objServiceCust.Cust_Email;
                    txtMobile.Text = objServiceCust.Cust_Mobile;
                }
            }
            if (objServiceCust.ServiceCustMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
            {
                txtCompanyName.Text = objServiceCust.Cust_Company_Name;
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

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.ServiceCustInfo objSMCustomer = new Services.ServiceCustInfo();
            if ((objSMCustomer.ServiceUnitMaster_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                txtUnitAddress.Text = objSMCustomer.Cust_Unit_Address;
                txtUnitContactPerson.Text = objSMCustomer.Unit_Contact_Person;
                txtMobile.Text = objSMCustomer.Contact_Mobile;
                txtEmail.Text = objSMCustomer.UnitEmail;
            }
            Services.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            Services.Dispose();
        }
        finally
        {

        }

    }
    #endregion



    #region Assign Button Click
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        tblComplaintRegister.Visible = false;
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            try
            {
                txtAssignTaskNo.Text = Services.ServicesAssignments.ServicesAssignments_AutoGenCode();
                Services.ServicesAssignments objServicesAssign = new Services.ServicesAssignments();
                if (objServicesAssign.ServicesAssignments_Select(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
                {
                    tblAssignTasks.Visible = true;
                    btnAssignTask.Text = "Re-Assign";

                    txtEnquiryNoForAssign.Text = string.Empty;
                    txtEnquiryDateForAssign.Text = string.Empty;
                    txtCustomerNameForAssingn.Text = string.Empty;
                    txtCustomerEmailForAssingn.Text = string.Empty;
                    ddlEmpNameForAssign.SelectedValue = "0";
                    txtEmpEmailId.Text = string.Empty;
                    txtRemarksForAssingn.Text = string.Empty;
                    txtAssignDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());
                    txtDueDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());

                    txtEnquiryNoForAssign.Text = objServicesAssign.CrNo;
                    txtEnquiryDateForAssign.Text = objServicesAssign.CrDate;
                    ddlEmpNameForAssign.SelectedValue = objServicesAssign.EmpId;
                    txtAssignDate.Text = objServicesAssign.AssingDate;
                    txtDueDate.Text = objServicesAssign.DueDate;
                    txtRemarksForAssingn.Text = objServicesAssign.AssignRemarks;
                    ddlEmpNameForAssign_SelectedIndexChanged(sender, e);

                    Services.CustomerMaster objServicesCustomer = new Services.CustomerMaster();
                    if ((objServicesCustomer.CustomerMaster_Select(objServicesAssign.CustId)) > 0)
                    {
                        txtCustomerNameForAssingn.Text = objServicesCustomer.CompName;
                        txtCustomerEmailForAssingn.Text = objServicesCustomer.Email;
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Re-Assign this Enquiry?');");
                }
                else
                {
                    Services.ComplaintRegister objServices = new Services.ComplaintRegister();
                    if (objServices.ComplaintRegister_Select(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
                    {
                        tblAssignTasks.Visible = true;
                        btnAssignTask.Text = "Assign";

                        txtEnquiryNoForAssign.Text = string.Empty;
                        txtEnquiryDateForAssign.Text = string.Empty;
                        txtCustomerNameForAssingn.Text = string.Empty;
                        txtCustomerEmailForAssingn.Text = string.Empty;
                        ddlEmpNameForAssign.SelectedValue = "0";
                        txtEmpEmailId.Text = string.Empty;
                        txtRemarksForAssingn.Text = string.Empty;
                        txtAssignDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());
                        txtDueDate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());

                        txtEnquiryNoForAssign.Text = objServices.CRNo;
                        txtEnquiryDateForAssign.Text = objServices.CRDate;
                        Services.ServiceCustInfo objServicesCustomer = new Services.ServiceCustInfo();
                        if ((objServicesCustomer.ServiceCustMaster_Select(objServices.CustId)) > 0)
                        {
                            txtCustomerNameForAssingn.Text = objServicesCustomer.Cust_Name;
                            txtCustomerEmailForAssingn.Text = objServicesCustomer.Cust_Email;
                        }
                    }
                    btnAssignTask.Attributes.Add("onclick", "return confirm('Are you sure you want to Assign this Enquiry?');");
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
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region ddlEmpNameForAssign_SelectedIndexChanged
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

    #region Button Assign Task
    protected void btnAssignTask_Click(object sender, EventArgs e)
    {
        try
        {

            Services.ServicesAssignments objServicesAssign = new Services.ServicesAssignments();
            Services.BeginTransaction();
            objServicesAssign.AssignTaskNo = txtAssignTaskNo.Text;
            objServicesAssign.CrId = gvComplaintRegister.SelectedRow.Cells[0].Text;
            objServicesAssign.CrNo = txtEnquiryNoForAssign.Text;
            objServicesAssign.EmpId = ddlEmpNameForAssign.SelectedItem.Value;
            objServicesAssign.AssingDate = Yantra.Classes.General.toMMDDYYYY(txtAssignDate.Text);
            objServicesAssign.DueDate = Yantra.Classes.General.toMMDDYYYY(txtDueDate.Text);
            objServicesAssign.AssignRemarks = txtRemarksForAssingn.Text;
            objServicesAssign.AssignStatus = "New";
            Services.ComplaintRegister.CompalintRegisterStatus_Update(Services.ServicesStatus.Open, gvComplaintRegister.SelectedRow.Cells[0].Text);
            objServicesAssign.Cp_Id = lblCPID.Text;

            if (btnAssignTask.Text == "Assign")
            {
                MessageBox.Show(this, objServicesAssign.ServicesAssignments_Save());
                Services.CommitTransaction();
            }
            else if (btnAssignTask.Text == "Re-Assign")
            {
                MessageBox.Show(this, objServicesAssign.ServicesAssignments_Update());
                Services.CommitTransaction();
            }
        }
        catch (Exception ex)
        {
            Services.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            tblComplaintRegister.Visible = false;
            tblAssignTasks.Visible = false;
            //gvInterestedProducts.DataBind();
            btnDelete.Attributes.Clear();
            btnAssignTask.Attributes.Clear();
            gvComplaintRegister.DataBind();
            Services.ClearControls(this);
            Services.Dispose();
        }
        tblAssignTasks.Visible = false;
    }
    #endregion

    #region Button Cancel Task
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {

        tblAssignTasks.Visible = false;
    }
    #endregion



    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtNatureofComplaint.Text == string.Empty)
        {
            txtNatureofComplaint.Text = "-";
        }
        if (txtRootCause.Text == string.Empty)
        { txtRootCause.Text = "-"; }
        if (txtCorrectiveAction.Text == string.Empty)
        { txtCorrectiveAction.Text = "-"; }
        if (txtQuantity.Text == string.Empty)
        {
            txtQuantity.Text = "-";
        }

        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("AttendBy");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Tech_StartDt");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Tech_EndDt");
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
                        dr["ItemName"] = txtItemName.Text;
                        dr["Quantity"] = txtQuantity.Text;
                        dr["NatureofComplaint"] = txtNatureofComplaint.Text;
                        dr["RootCausedNotice"] = txtRootCause.Text;
                        dr["CorrectiveActionTaken"] = txtCorrectiveAction.Text;
                        dr["AttendBy"] = ddlCrAttendBy.SelectedItem.Text;
                        dr["Tech_StartDt"] = txtTechinTime.Text;
                        dr["Tech_EndDt"] = txtTechOutTime.Text;
                        QuotationItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QuotationItems.NewRow();
                        dr["ItemName"] = gvrow.Cells[2].Text;
                        dr["Quantity"] = gvrow.Cells[3].Text;
                        dr["NatureofComplaint"] = gvrow.Cells[4].Text;
                        dr["RootCausedNotice"] = gvrow.Cells[5].Text;
                        dr["CorrectiveActionTaken"] = gvrow.Cells[6].Text;
                        dr["AttendBy"] = gvrow.Cells[7].Text;
                        dr["Tech_StartDt"] = gvrow.Cells[8].Text;
                        dr["Tech_EndDt"] = gvrow.Cells[9].Text;
                        QuotationItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QuotationItems.NewRow();

                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Quantity"] = gvrow.Cells[3].Text;
                    dr["NatureofComplaint"] = gvrow.Cells[4].Text;
                    dr["RootCausedNotice"] = gvrow.Cells[5].Text;
                    dr["CorrectiveActionTaken"] = gvrow.Cells[6].Text;
                    dr["AttendBy"] = gvrow.Cells[7].Text;
                    dr["Tech_StartDt"] = gvrow.Cells[8].Text;
                    dr["Tech_EndDt"] = gvrow.Cells[9].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }



        if (gvQuotationItems.SelectedIndex == -1)
        {
            DataRow drnew = QuotationItems.NewRow();

            drnew["ItemName"] = txtItemName.Text;
            drnew["Quantity"] = txtQuantity.Text;
            drnew["NatureofComplaint"] = txtNatureofComplaint.Text;
            drnew["RootCausedNotice"] = txtRootCause.Text;
            drnew["CorrectiveActionTaken"] = txtCorrectiveAction.Text;
            drnew["AttendBy"] = ddlCrAttendBy.SelectedItem.Text;
            drnew["Tech_StartDt"] = txtTechinTime.Text;
            drnew["Tech_EndDt"] = txtTechOutTime.Text;
            QuotationItems.Rows.Add(drnew);
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
        gvQuotationItems.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {


        txtItemName.Text = string.Empty;//
        //txtItemCategory.Text = string.Empty;//
        //txtItemSubCategory.Text = string.Empty;//
        //txtBrand.Text = string.Empty;//
        txtQuantity.Text = string.Empty;
        txtCorrectiveAction.Text = string.Empty;
        txtNatureofComplaint.Text = string.Empty;
        txtRootCause.Text = string.Empty;
    }
    #endregion

    #region GridView Quotation Items Row DataBound
    protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        //}
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox Feedback = (TextBox)row.FindControl("txtFeedback");
            TextBox actiontaken = (TextBox)row.FindControl("txtactionTaken");
        }


    }
    #endregion

    #region GridView Quotation Items Row Deleting
    protected void gvQuotationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvQuotationItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("AttendBy");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Tech_StartDt");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Tech_EndDt");
        QuotationItems.Columns.Add(col);

        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = QuotationItems.NewRow();

                    dr["ItemName"] = gvrow.Cells[2].Text;
                    dr["Quantity"] = gvrow.Cells[3].Text;
                    TextBox Feedback = (TextBox)gvrow.FindControl("txtFeedback");
                    dr["RootCausedNotice"] = Feedback.Text;
                    TextBox Actiontaken = (TextBox)gvrow.FindControl("txtactionTaken");
                    dr["CorrectiveActionTaken"] = Actiontaken.Text;
                    dr["NatureofComplaint"] = gvrow.Cells[4].Text;
                    //dr["RootCausedNotice"] = gvrow.Cells[5].Text;
                    //dr["CorrectiveActionTaken"] = gvrow.Cells[6].Text;
                    dr["AttendBy"] = gvrow.Cells[7].Text;
                    dr["Tech_StartDt"] = gvrow.Cells[8].Text;
                    dr["Tech_EndDt"] = gvrow.Cells[9].Text;
                    QuotationItems.Rows.Add(dr);
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }
    #endregion

    #region GridView Quotation Items Row Editing
    protected void gvQuotationItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable QuotationItems = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ItemName");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        QuotationItems.Columns.Add(col);

        col = new DataColumn("NatureofComplaint");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("RootCausedNotice");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("CorrectiveActionTaken");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("AttendBy");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Tech_StartDt");
        QuotationItems.Columns.Add(col);
        col = new DataColumn("Tech_EndDt");
        QuotationItems.Columns.Add(col);
        if (gvQuotationItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvQuotationItems.Rows)
            {
                DataRow dr = QuotationItems.NewRow();

                dr["ItemName"] = gvrow.Cells[2].Text;
                dr["Quantity"] = gvrow.Cells[3].Text;
                dr["NatureofComplaint"] = gvrow.Cells[4].Text;
                dr["RootCausedNotice"] = gvrow.Cells[5].Text;
                dr["CorrectiveActionTaken"] = gvrow.Cells[6].Text;
                dr["AttendBy"] = gvrow.Cells[7].Text;
                dr["Tech_StartDt"] = gvrow.Cells[8].Text;
                dr["Tech_EndDt"] = gvrow.Cells[9].Text;
                QuotationItems.Rows.Add(dr);
                gvQuotationItems.EditIndex = e.NewEditIndex;  
                if (gvrow.RowIndex == gvQuotationItems.Rows[e.NewEditIndex].RowIndex)
                {
                    txtItemName.Text = gvrow.Cells[2].Text;
                    txtQuantity.Text = gvrow.Cells[3].Text;
                    txtNatureofComplaint.Text = gvrow.Cells[4].Text;
                    txtRootCause.Text = gvrow.Cells[5].Text;
                    txtCorrectiveAction.Text = gvrow.Cells[6].Text;
                    ddlAttendBy.SelectedItem.Text = gvrow.Cells[7].Text;
                    txtTechinTime.Text = gvrow.Cells[8].Text;
                    txtTechOutTime .Text =gvrow .Cells [9].Text;
                    gvQuotationItems.EditIndex = -1; 
                    gvQuotationItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvQuotationItems.DataSource = QuotationItems;
        gvQuotationItems.DataBind();
    }

    #endregion

    #region GvOrder Acceptance RowDataBound
    protected void gvOrderAcceptanceItems_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Text = ((Convert.ToDouble(e.Row.Cells[7].Text)) * (Convert.ToDouble(e.Row.Cells[6].Text))).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = e.Row.Cells[9].Visible = e.Row.Cells[10].Visible = e.Row.Cells[11].Visible = false;
            //  e.Row.Cells[12].Visible = false;
        }
    }
    #endregion

    #region GVItmDetails_DataBound
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
        //}
    }
    #endregion

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {

        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource1";
            ddlCustomerName.DataTextField = "Cust_Name";
            ddlCustomerName.DataValueField = "Cust_Id";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }




    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvComplaintRegister.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
    //protected void btnCompanySearch_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmd = new SqlCommand("USP_ComplaintReg_Company_Search", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@SearchItemName", txtSearchCompany.Text);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    txtCompanyName.Text = dt.Rows[0][3].ToString();
    //}
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        LinkButton lbtnFileOpener;
        lbtnFileOpener = (LinkButton)sender;
        Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
        DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
        string command = "SELECT REG_ATTACHMENT FROM [YANTRA_COMPLAINT_REGISTER_ATTACHMENTS] WHERE CR_ID=" + gvComplaintRegister.SelectedRow.Cells[0].Text + " AND REG_ATTACHMENT='" + lbtnFileOpener.Text + "'";
        dbcon.Open();
        string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
        string path = "../../Content/ServicesComplaintRegisterAttachments/" + filename;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
    }
    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        
        
            HR.SendSMS objsms = new HR.SendSMS();
            ////Send MSG to Employee
            string msfEmp = "Dear Sir, Your " + ddlCallType.SelectedItem.Text + " No. " + txtCRNo.Text + " date " + txtCRDate.Text + " will be resolved shortly. Any enquiry contact no 040-23554474/75. VALUELINE";
            //string emp_MNo = "9059638293";
            string emp_MNo = txtMobile.Text;
            objsms.Send_App_SMS(msfEmp, emp_MNo);
    }
    string pagenavigationstr, htmlstr;

    public void SendSMSUpdateToCustomer()
    {
        HR.SendSMS objsms = new HR.SendSMS();
        if (ddlstatus.SelectedItem.Text == "Open")
        {
            if (ddlAttendBy.SelectedItem.Text != "--")
            {
                string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been assigned to our Technician " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " Please share OTP - " + lblStartOTP.Text + " with him when he visits your location. VALUELINE";
                //string emp_MNo = "9059638293";
                string emp_MNo = txtMobile.Text;
                objsms.Send_App_SMS(msfEmp, emp_MNo);
            }
        }
        else if (ddlstatus.SelectedItem.Text == "Manual Closing")
        {
            string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been manually closed. if you need any further assistance please contact our customer care. VALUELINE";
            //string emp_MNo = "9059638293";
            string emp_MNo = txtMobile.Text;
            objsms.Send_App_SMS(msfEmp, emp_MNo);
        }
    }
    public void SendSMSUpdateToTechnician()
    {
        HR.SendSMS objsms = new HR.SendSMS();
        if (ddlstatus.SelectedItem.Text == "Open")
        {
            if (ddlAttendBy.SelectedItem.Text != "--")
            {
                pagenavigationstr = "http://183.82.108.55/CRTech.aspx?CRID=" + lblCRIDHidden.Text + "";

                string msgtoTech = "Dear " + ddlAttendBy.SelectedItem.Text + ", You have a new " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Please visit " + ddlCustomerName.SelectedItem.Text + " at " + txtUnitAddress.Text +
                    ". Click on the link when you start the work " + pagenavigationstr + " and enter OTP. VALUELINE";
                string TechNo = txtTechMobile.Text;
                objsms.Send_App_SMS2(msgtoTech, TechNo);
            }
        }
        else if (ddlstatus.SelectedItem.Text == "Manual Closing")
        {
            if (ddlAttendBy.SelectedItem.Text != "--")
            {
                string msgtoTech = "Dear " + ddlAttendBy.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Customer " + ddlCustomerName.SelectedItem.Text + " Address " + txtUnitAddress.Text +
                    "has been manually closed for more information contact our customer care executive. VALUELINE";
                string TechNo = txtTechMobile.Text;
                objsms.Send_App_SMS2(msgtoTech, TechNo);
            }
        }
        
    }
    public void SendSMSUpdateToExecutive()
    {
        try 
        {
            
            if (ddlstatus.SelectedItem.Text == "Open")
            {
                HR.SendSMS objsms = new HR.SendSMS();
                if (ddlAttendBy.SelectedItem.Text != "--")
                {
                    
                    if (rdblIndentfor.SelectedItem.Text == "Executive")
                    {
                        string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", " + ddlAttendBy.SelectedItem.Text + " has been assigned to your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " For " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                        string ExeNo = txtExeMobile.Text;
                        objsms.Send_App_SMS1(msgtoExe, ExeNo);
                    }
                    
                }
                else
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered, for " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                }
            }
            else if (ddlstatus.SelectedItem.Text == "Closed")
            {
                HR.SendSMS objsms = new HR.SendSMS();
                ////Send MSG to Employee

                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been closed. By " + ddlAttendBy.SelectedItem.Text + " 0"+txtTechMobile .Text+". Customer Name is " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                   
                }
                
            }
            else if (ddlstatus.SelectedItem.Text == "Pending")
            {
                HR.SendSMS objsms = new HR.SendSMS();
                ////Send MSG to Employee

                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " is pending for now. For more details contact " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + ". Customer Name is "+ ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                }
                
            }
            else if (ddlstatus.SelectedItem.Text == "Manual Closing")
            {
                HR.SendSMS objsms = new HR.SendSMS();
                ////Send MSG to Employee
                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been manually closed as per customer request. For more information contact our customer care executive. Customer Name is " + ddlCustomerName.SelectedItem.Text + " VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                }
            }
        }
        catch (Exception ex) { }
    }
    public void SendSMSToTechnician()
    {
        HR.SendSMS objsms = new HR.SendSMS();
        try
        {
            if (ddlstatus.SelectedItem.Text == "Open")
            {
                if (ddlAttendBy.SelectedItem.Text == "--")
                {

                }
                else
                {
                    pagenavigationstr = "http://183.82.108.55/CRTech.aspx?CRID=" + lblCRIDHidden.Text + "";
                    string msgtoTech = "Dear " + ddlAttendBy.SelectedItem.Text + " , You have a new " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Please visit " + ddlCustomerName.SelectedItem.Text + " at " + txtUnitAddress.Text +
                        ". Click on the link when you start the work " + pagenavigationstr + " and enter OTP. VALUELINE";
                    string TechNo = txtTechMobile.Text;
                    objsms.Send_App_SMS2(msgtoTech, TechNo);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void SendSMSToCustomer()
    {
        HR.SendSMS objsms = new HR.SendSMS();
        try
        {
            if (ddlstatus.SelectedItem.Text == "Open")
            {
                if (ddlAttendBy.SelectedItem.Text == "--")
                {
                    string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered. Our Technician will assist you shortly. VALUELINE";
                    //string emp_MNo = "9059638293";
                    string emp_MNo = txtMobile.Text;
                    objsms.Send_App_SMS(msfEmp, emp_MNo);
                }
                else
                {
                    string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered. Our Technician " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " will assist you shortly. Please share OTP - " + lblStartOTP.Text + " with him when he visits your location. VALUELINE";
                    //string emp_MNo = "9059638293";
                    string emp_MNo = txtMobile.Text;
                    objsms.Send_App_SMS(msfEmp, emp_MNo);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void SendSMS()
    {
        try
        {
            HR.SendSMS objsms = new HR.SendSMS();
            if (ddlstatus.SelectedItem.Text == "Open")
            {
                if (ddlAttendBy.SelectedItem.Text == "--")
                {
                    if (rdblIndentfor.SelectedItem.Text == "Executive")
                    {
                        string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered. Our Technician will assist you shortly. VALUELINE";
                        //string emp_MNo = "9059638293";
                        string emp_MNo = txtMobile.Text;
                        objsms.Send_App_SMS(msfEmp, emp_MNo);

                        string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered, for " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                        string ExeNo = txtExeMobile.Text;
                        objsms.Send_App_SMS1(msgtoExe, ExeNo);
                    }
                    else
                    {
                        string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered. Our Technician will assist you shortly. VALUELINE";
                        //string emp_MNo = "9059638293";
                        string emp_MNo = txtMobile.Text;
                        objsms.Send_App_SMS(msfEmp, emp_MNo);
                    }
                }
                else
                {
                    if (rdblIndentfor.SelectedItem.Text == "Executive")
                    {
                        string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered. Our Technician " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " will assist you shortly. Please share OTP - " + lblStartOTP.Text + " with him when he visits your location. VALUELINE";
                        //string emp_MNo = "9059638293";
                        string emp_MNo = txtMobile.Text;
                        objsms.Send_App_SMS(msfEmp, emp_MNo);

                        pagenavigationstr = "http://183.82.108.55/CRTech.aspx?CRID=" + lblCRIDHidden.Text + "";
                        string msgtoTech = "Dear " + ddlAttendBy.SelectedItem.Text + " , You have a new " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Please visit " + ddlCustomerName.SelectedItem.Text + " at " + txtUnitAddress.Text +
                            ". Click on the link when you start the work " + pagenavigationstr + " and enter OTP. VALUELINE";
                        string TechNo = txtTechMobile.Text;
                        objsms.Send_App_SMS2(msgtoTech, TechNo);

                        string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered For " + ddlCustomerName.SelectedItem.Text + " and Assigned to " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + ". VALUELINE";
                        string ExeNo = txtExeMobile.Text;
                        objsms.Send_App_SMS1(msgtoExe, ExeNo);
                    }
                }
            }
            else if (ddlstatus.SelectedItem.Text == "Closed")
            {
                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been closed. By " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " Customer Name is " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                }
            }
            else if (ddlstatus.SelectedItem.Text == "Pending")
            {
                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " , Customer Name " + ddlCustomerName.SelectedItem.Text + " is Pending for now. For more details contact " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void SendSMSToExecutive()
    {
        try
        {
            if (ddlstatus.SelectedItem.Text == "Open")
            {
                if (ddlAttendBy.SelectedItem.Text == "--")
                {
                    HR.SendSMS objsms = new HR.SendSMS();
                    if (rdblIndentfor.SelectedItem.Text == "Executive")
                    {
                        string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered, for " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                        string ExeNo = txtExeMobile.Text;
                        objsms.Send_App_SMS1(msgtoExe, ExeNo);

                    }
                    
                }
                else
                {
                    HR.SendSMS objsms = new HR.SendSMS();
                    if (rdblIndentfor.SelectedItem.Text == "Executive")
                    {
                        string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been registered For " + ddlCustomerName.SelectedItem.Text + " and Assigned to " + ddlAttendBy.SelectedItem.Text + " 0"+txtTechMobile .Text+". VALUELINE";
                        string ExeNo = txtExeMobile.Text;
                        objsms.Send_App_SMS1(msgtoExe, ExeNo);
                    }
                    
                }
            }
            else if (ddlstatus.SelectedItem.Text == "Closed")
            {
                HR.SendSMS objsms = new HR.SendSMS();
                ////Send MSG to Employee

                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " has been closed. By " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " Customer Name is " + ddlCustomerName.SelectedItem.Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);
                }
            }
            else if (ddlstatus.SelectedItem.Text == "Pending")
            {
                HR.SendSMS objsms = new HR.SendSMS();
                ////Send MSG to Employee

                if (rdblIndentfor.SelectedItem.Text == "Executive")
                {
                    string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " , Customer Name " + ddlCustomerName.SelectedItem.Text + " is Pending for now. For more details contact " + ddlAttendBy.SelectedItem.Text + " 0"+txtTechMobile .Text + ". VALUELINE";
                    string ExeNo = txtExeMobile.Text;
                    objsms.Send_App_SMS1(msgtoExe, ExeNo);

                    //string msfEmp = "Dear Sir, We have an Update Your " + ddlCallType.SelectedItem.Text + " No. " + txtCRNo.Text + " date " + txtCRDate.Text + " is in Pending will resolve Shortly. Any enquiry contact no 040-23554474/75. VALUELINE";
                    ////string emp_MNo = "9059638293";
                    //string emp_MNo = txtMobile.Text;
                    //objsms.Send_App_SMS(msfEmp, emp_MNo);
                }
                else
                {
                    //string msfEmp = "Dear Sir, We have an update Your " + ddlCallType.SelectedItem.Text + " No. " + txtCRNo.Text + " date " + txtCRDate.Text + " is in Pending, will resolve Shortly. Any enquiry contact no 040-23554474/75. VALUELINE";
                    ////string emp_MNo = "9059638293";
                    //string emp_MNo = txtMobile.Text;
                    //objsms.Send_App_SMS(msfEmp, emp_MNo);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void rdblIndentfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblIndentfor.SelectedItem.Text == "Client")
        {
            ddlSalesExecutive.Visible = false;
            lblCompanyName.Visible = false;
            lblExecutiveMobile.Visible = false;
            txtExeMobile.Visible = false;
        }
        else
        {
            ddlSalesExecutive.Visible = true;
            lblCompanyName.Visible = true;
            lblExecutiveMobile.Visible = true;
            txtExeMobile.Visible = true;
        }
    }
    protected void ddlSalesExecutive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlSalesExecutive.SelectedItem.Value) > 0)
            {
                txtExeMobile.Text = objHR.AssignedMobileNo;
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
    protected void ddlAttendBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlAttendBy.SelectedItem.Value) > 0)
            {
                txtTechMobile.Text = objHR.AssignedMobileNo;
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
    protected void gvComplaintRegister_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hf = (HiddenField)e.Row.FindControl("cthf1");
            DropDownList ddlcurency = (DropDownList)e.Row.FindControl("ddlAttendedBy");
            SM.DDLBindWithSelect(ddlcurency, "SELECT a.EMP_ID,EMP_FIRST_NAME FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID=6 or b.DEPT_ID=21 or b.DEPT_ID=28 or b.DEPT_ID=32  ORDER BY EMP_FIRST_NAME", hf.Value);
        }
    }
    protected void btnReopen_Click(object sender, EventArgs e)
    {
        if (gvComplaintRegister.SelectedIndex > -1)
        {
            if (gvComplaintRegister.SelectedRow.Cells[9].Text == "Pending" && gvComplaintRegister.SelectedRow.Cells[10].Text == "Pending")
            {
                //foreach (GridViewRow gvrow in gvComplaintRegister.Rows)
                //{
                GridViewRow gvrow = gvComplaintRegister.Rows[gvComplaintRegister.SelectedIndex];
                
                    int i = 0;
                    lblReopenDt.Text = DateTime.Now.ToString();
                    lblReopenStartOTP.Text = RandomGenerator().ToString();
                    lblReopenClosedOTP.Text = RandomGenerator1().ToString();
                    DropDownList attendedBy = (DropDownList)gvrow.FindControl("ddlAttendedBy");
                    lblAttendedBy.Text = attendedBy.SelectedItem.Value;
                    lblTechName.Text = attendedBy.SelectedItem.Text;
                    string _command = "update YANTRA_COMPLAINT_REGISTER_MAST set CR_PENDING_FOLLOWUP_DT='" + lblReopenDt.Text +
                        "',CR_STATUS='ReOpened',TECH_STATUS='ReOpened', CR_START_OTP='" + lblReopenStartOTP.Text +
                        "', CR_CLOSED_OTP='" + lblReopenClosedOTP.Text + "', CR_Attendby='" + lblAttendedBy.Text + "'  where CR_ID='" + gvComplaintRegister.SelectedRow.Cells[0].Text + "'";
                    SqlCommand cmd = new SqlCommand(_command, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    i = i + cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
                        if (objComplaintRegister.ComplaintRegister_Select1(gvComplaintRegister.SelectedRow.Cells[0].Text) > 0)
                        {
                            txtCRNo.Text = objComplaintRegister.CRNo;
                            txtCRDate.Text = Yantra.Classes.General.toMMDDYYYY(txtCRDate.Text);
                            ddlCallType.SelectedItem.Text = objComplaintRegister.CRCallType;
                            ddlCustomerName.SelectedValue = objComplaintRegister.CustId;
                            ddlCustomerName_SelectedIndexChanged(sender, e);
                            ddlUnitName.SelectedValue = objComplaintRegister.CustUnitId;
                            ddlUnitName_SelectedIndexChanged(sender, e);
                            // ddlContactPerson.SelectedValue = objComplaintRegister.CustDetId;
                            //ddlContactPerson_SelectedIndexChanged(sender, e);
                            //ddlItemType.SelectedValue = Masters.ItemMaster.GetItemTypeId(objComplaintRegister.ItemCode);
                            //ddlItemType_SelectedIndexChanged(sender, e);
                            //      ddlItemName.SelectedValue = objComplaintRegister.ItemCode;
                            txtNatureofComplaint.Text = objComplaintRegister.CRComplaintNature;
                            txtRootCause.Text = objComplaintRegister.CRRootCause;
                            txtCorrectiveAction.Text = objComplaintRegister.CRCorrectiveAction;
                            ddlPreparedBy.SelectedValue = objComplaintRegister.CRPreparedBy;
                            ddlAttendBy.SelectedValue = objComplaintRegister.attendby;
                            ddlAttendBy_SelectedIndexChanged(sender, e);
                            ddlstatus.SelectedValue = objComplaintRegister.CRSatus;
                            txtCompletedDate.Text = objComplaintRegister.CompletedDate;
                            lblCRIDHidden.Text = objComplaintRegister.CRId;
                            HR.SendSMS objsms = new HR.SendSMS();
                            if (rdblIndentfor.SelectedItem.Text == "Executive")
                            {
                                
                                string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Dated " + txtCRDate.Text + " has been reopened and assigned to Technician " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " Please share OTP - " + lblReopenStartOTP.Text + " with him when he visits your location. VALUELINE";
                                //string emp_MNo = "9059638293";
                                string emp_MNo = txtMobile.Text;
                                objsms.Send_App_SMS(msfEmp, emp_MNo);
                                //myHyperLink.NavigateUrl = string.Format("~/CRTech.aspx?CRid={0}", lblCRIDHidden.Text);
                                pagenavigationstr = "http://183.82.108.55/CRTech.aspx?CRID=" + lblCRIDHidden.Text + "";
                                string msgtoTech = "Dear " + ddlAttendBy.SelectedItem.Text + " , " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Dated " + txtCRDate.Text + " has been reopened. Please visit " + ddlCustomerName.SelectedItem.Text + " at " + txtUnitAddress.Text +
                                    ". Click here " + pagenavigationstr + " to enter OTP. VALUELINE";
                                string TechNo = txtTechMobile.Text;
                                objsms.Send_App_SMS2(msgtoTech, TechNo);
                                string msgtoExe = "Dear " + ddlSalesExecutive.SelectedItem.Text + ", Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Dated " + txtCRDate.Text + " has been reopened For " + ddlCustomerName.SelectedItem.Text + "and Assigned to " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + ". VALUELINE";
                                string ExeNo = txtExeMobile.Text;
                                objsms.Send_App_SMS1(msgtoExe, ExeNo);
                            }
                            else
                            {
                                //Send MSG to Employee
                                string msfEmp = "Dear Customer, Your " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Dated " + txtCRDate.Text + " has been reopened and assigned to Technician " + ddlAttendBy.SelectedItem.Text + " 0" + txtTechMobile.Text + " Please share OTP - " + lblReopenStartOTP.Text + " with him when he visits your location. VALUELINE";
                                //string emp_MNo = "9059638293";
                                string emp_MNo = txtMobile.Text;
                                objsms.Send_App_SMS(msfEmp, emp_MNo);
                                pagenavigationstr = "http://183.82.108.55/CRTech.aspx?CRID=" + lblCRIDHidden.Text + "";
                                string msgtoTech = "Dear " + ddlAttendBy.SelectedItem.Text + " , " + ddlCallType.SelectedItem.Text + " Ticket " + txtCRNo.Text + " Dated " + txtCRDate .Text+ " has been reopened. Please visit " + ddlCustomerName.SelectedItem.Text + " at " + txtUnitAddress.Text +
                                      ". Click here " + pagenavigationstr + " to enter OTP. VALUELINE";
                                string TechNo = txtTechMobile.Text;
                                objsms.Send_App_SMS2(msgtoTech, TechNo);

                            }
                        //}
                        gvComplaintRegister.DataBind();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Reopen will not accept for this complaint, Contact Admin!!"); ;
            }
        }
        
    }

    
}

 
