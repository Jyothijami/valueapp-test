using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_SM_CreditApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomerName_Fill();
            EmployeeMaster_Fill();
            try
            {
                if (Request.QueryString["Cid"].ToString() != null)
                {

                    try
                    {
                        SM.Dispatch obj = new SM.Dispatch();
                        if (obj.Dispatch_SelectForCredit( Request.QueryString["Cid"].ToString()) > 0)
                        {
                            txtCustName.Text = obj.CustUnitname;
                            txtcustAddress.Text = obj.UnitAddress;
                            txtcustomerEmail.Text = obj.CustomerEmailid;
                            txtCustomerMobile.Text = obj.CustomerPhoneno;
                            txtPOValue.Text = obj.POValue;
                            txtDispatchValue.Text = obj.DispatchValue;
                            lblPaymentTerms.Text = obj.PackingCharges;
                            ddlComp.SelectedValue = obj.CP_ID;
                            ddlExec.SelectedValue = obj.Exective;
                            ddlCust.SelectedValue = obj.CustId;
                            ddlSO.SelectedValue = obj.SoId;
                            

                            //tblpopup3.Visible = true;

                            if (obj.Status == "CreditApproval_Raised")
                            {
                                if (obj.CreditApp_Select( Request.QueryString["Cid"].ToString()) > 0)
                                {
                                    string id = obj.Credit_Id;
                                    btnSavepopup.Text = "Update";
                                    txtDispatchValue.Text = obj.DispatchValue;
                                    txtDays.Text = obj.PaymentsCollected;
                                    txtCreditAmt.Text = obj.CrAppValue;
                                    txtCR.Text = obj.crValue;
                                    txtUDC.Text = obj.UDCValue;
                                    txtOther.Text = obj.OtherVaue;
                                    ddlAccId.SelectedValue = obj.AccountsId;
                                    ddlCMD.SelectedValue = obj.CMDID;
                                    txtCRANo.Text = obj.CRANo;
                                    txtRmks.Text = obj.Time;
                                    txtOtherProjects.Text = obj.OtherProjectsValue;
                                    if (obj.NoOfDays == "DR")
                                    {
                                        rdbWithPo.Checked = true;
                                        rdbWithoutPo.Checked = false;
                                    }
                                    else
                                    {
                                        rdbWithoutPo.Checked = true;
                                        rdbWithPo.Checked = false;
                                    }

                                }
                            }
                            else
                            {
                                txtCRANo.Text = SM.Dispatch.CRA_AutoGenCode();
                                txtDays.Text = DateTime.Now.ToString("MM/dd/yyyy");
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }


    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            //SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlCust );
            SM.CustomerMaster.CustomerNickName_SelectCredit(ddlCust);
            SM.SalesOrder.SalesOrder_Select(ddlSO);
            Masters.CompanyProfile.Company_Select(ddlComp);
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
            HR.EmployeeMaster.EmployeeMaster_Select(ddlExec );
            HR.EmployeeMaster.EmployeeMaster_SelectAccounts(ddlAccId);
            HR.EmployeeMaster.EmployeeMaster_SelectCMD(ddlCMD);
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
    protected void btnSavepopup_Click(object sender, EventArgs e)
    {
        btnSavepopup.Enabled = false;

        if (btnSavepopup.Text == "Save")
        {
            CreditApproveSave();
        }
        else
        {
            CreditApproveUpdate();
            btnPrint.Enabled = true;
        }
    }

    private void CreditApproveSave()
    {
        SM.Dispatch obj = new SM.Dispatch();
        obj.DispatchId = Request.QueryString["Cid"].ToString();
        obj.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.CreatedOn = DateTime.Now.ToString();
        obj.Exective = ddlExec.SelectedValue;
        //obj.PaymentsCollected = txtDays.Text;
        obj.PaymentsCollected = txtDays.Text;
        obj.POValue = txtPOValue.Text;
        obj.DispatchValue = txtDispatchValue.Text;
        obj.CP_ID = ddlComp.SelectedValue;
        if (rdbWithPo.Checked == true)
        {
            obj.NoOfDays = "DR";
        }
        else
        {
            obj.NoOfDays = "CR";
        }
        if (rdbDRUnbilledDcs.Checked == true)
        {
            obj.CRDRUDC  = "DR";
        }
        else if (rdbCRUnbilledDcs.Checked == true)
        {
            obj.CRDRUDC = "CR";
        }
        if (rdbDROtherBranches.Checked == true)
        {
            obj.CRDRBranches  = "DR";
        }
        else if (rdbCROtherBranches.Checked == true)
        {
            obj.CRDRBranches  = "CR";
        }
        if (rdbDROtherPro.Checked == true)
        {
            obj.CRDRProjects  = "DR";
        }
        else if (rdbCROtherPro.Checked == true)
        {
            obj.CRDRProjects = "CR";
        }
        if (txtCreditAmt.Text == "" || txtCreditAmt.Text == null)
        {
            obj.CrAppValue = "0";
        }
        else { obj.CrAppValue = txtCreditAmt.Text; }
        if (txtCR.Text == "" || txtCR.Text == null)
        {
            obj.crValue = "0";
        }
        else { obj.crValue = txtCR.Text; }
        if (txtUDC.Text == "" || txtUDC.Text == null)
        {
            obj.UDCValue = "0";
        }
        else { obj.UDCValue = txtUDC.Text; }
        if (txtOther.Text == "" || txtOther.Text == null)
        {
            obj.OtherVaue = "0";
        }
        else { obj.OtherVaue = txtOther.Text; }
        if (txtOtherProjects.Text == "" || txtOtherProjects.Text == null)
        {
            obj.OtherProjectsValue  = "0";
        }
        else { obj.OtherProjectsValue = txtOtherProjects.Text; }

        obj.AccountsId = ddlAccId.SelectedItem.Value;
        obj.CMDID = ddlCMD.SelectedItem.Value;
        obj.CustId = ddlCust.SelectedValue;
        obj.SoId = ddlSO.SelectedValue;
        obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.Status = "CreditApproval_Raised";
        obj.Time = txtRmks.Text;
        obj.CRANo = txtCRANo.Text;
        if (obj.CreditApproval_Save() == "Data Saved Successfully")
        {
            SM.Dispatch.DispatchStatus_Update(SM.SMStatus.CreditApproval_Raised, obj.DispatchId);
            SendSmsToAccounts();

        }
        //MessageBox.Show(this, "Data Saved Successfully");
        //tblpopup3.Visible = false;
    }
    private void SendSmsToAccounts()
    {
        MessageBox.Show(this, "Your Credit Approval raised and forwarded to Accounts Team.");
        HR.SendSMS objsms = new HR.SendSMS();
        string msfEmp = "Dear Team, New Credit Approval is raised for " + ddlCust.SelectedItem.Text + " By " + ddlExec.SelectedItem.Text + ". VALUELINE";
        string MD_MNo = "8008103074";
        objsms.CreditAppSMS(msfEmp, MD_MNo);
    }
    private void CreditApproveUpdate()
    {
        SM.Dispatch obj = new SM.Dispatch();
        obj.DispatchId = Request.QueryString["Cid"].ToString();
        obj.Preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.CreatedOn = DateTime.Now.ToString();
        obj.Exective = ddlExec.SelectedValue;
        obj.PaymentsCollected =Yantra.Classes.General.toMMDDYYYY (txtDays.Text);
        obj.DeliveryDate = Yantra.Classes.General.toMMDDYYYY(txtDays.Text);
        obj.POValue = txtPOValue.Text;
        obj.DispatchValue = txtDispatchValue.Text;
        if (rdbWithPo.Checked == true)
        {
            obj.NoOfDays = "DR";
        }
        else
        {
            obj.NoOfDays = "CR";
        }
        if (rdbDRUnbilledDcs.Checked == true)
        {
            obj.CRDRUDC = "DR";
        }
        else if (rdbCRUnbilledDcs.Checked == true)
        {
            obj.CRDRUDC = "CR";
        }
        if (rdbDROtherBranches.Checked == true)
        {
            obj.CRDRBranches = "DR";
        }
        else if (rdbCROtherBranches.Checked == true)
        {
            obj.CRDRBranches = "CR";
        }
        if (rdbDROtherPro.Checked == true)
        {
            obj.CRDRProjects = "DR";
        }
        else if (rdbCROtherPro.Checked == true)
        {
            obj.CRDRProjects = "CR";
        }
        if (txtCreditAmt.Text == "" || txtCreditAmt.Text == null)
        {
            obj.CrAppValue = "0";
        }
        else { obj.CrAppValue = txtCreditAmt.Text; }
        if (txtCR.Text == "" || txtCR.Text == null)
        {
            obj.crValue = "0";
        }
        else { obj.crValue = txtCR.Text; }
        if (txtUDC.Text == "" || txtUDC.Text == null)
        {
            obj.UDCValue = "0";
        }
        else { obj.UDCValue = txtUDC.Text; }
        if (txtOther.Text == "" || txtOther.Text == null)
        {
            obj.OtherVaue = "0";
        }
        else { obj.OtherVaue = txtOther.Text; }
        if (txtOtherProjects.Text == "" || txtOtherProjects.Text == null)
        {
            obj.OtherProjectsValue = "0";
        }
        else { obj.OtherProjectsValue = txtOtherProjects.Text; }
        //obj.CrAppValue = txtCreditAmt.Text;
        //obj.UDCValue = txtUDC.Text;
        //obj.OtherVaue = txtOther.Text;
        obj.AccountsId = ddlAccId.SelectedItem.Value;
        obj.CMDID = ddlCMD.SelectedItem.Value;
        obj.CustId = ddlCust.SelectedValue;
        obj.SoId = ddlSO.SelectedValue;
        obj.ApprovedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.Status = "CreditApproval_Raised";
        obj.CP_ID = ddlComp.SelectedValue;

        obj.Time = txtRmks.Text;
        obj.CRANo = txtCRANo.Text;
        //obj.NoOfDays = "";

        if (obj.CreditApproval_Update() == "Data Updated Successfully")
        {
            //SM.Dispatch.DispatchStatus_Update(SM.SMStatus.CreditApproval_Raised, obj.DispatchId);
            MessageBox.Show(this, "Data Updated Successfully");

        }
        else
        {
            MessageBox.Show(this, "Data was not updated, Please contact the admin");

        }
        //tblpopup3.Visible = false;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=CreditApproval&DCId=" + Request.QueryString["Cid"].ToString()+ "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        
       
    }
    
}