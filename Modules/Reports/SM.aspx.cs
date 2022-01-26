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

using vllib;
public partial class Modules_Reports_SM : basePage
{
    string pagenavigationstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblDeptId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
            EmployeeNames_Fill();
            CustomerNames_Fill();
            AdsMode_Fill();
            AdsAgency_Fill();
            CompanyName_Fill();
            Department_Fill();
            Masters.CompanyProfile.Company_Select(ddlComp);
            ddlComp.Items.FindByText("--").Text = "All";
            Masters.RegionalMaster.RegionalMaster_Select(ddlRegion);
            ddlRegion.Items.FindByText("--").Text = "All";
            Masters.EnquiryMode.EnquiryMode_Select(ddlCategory);
            ddlCategory.Items.FindByText("--").Text = "All";
            Masters.Architect.ArCity_Select(txtCity );
            txtCity.Items.FindByText("--").Text = "All";
            Masters.Architect.ArPincode_Select(txtPincode );
            txtPincode.Items.FindByText("--").Text = "All";
            SM.CustomerMaster.CustomerMaster_Select(ddlCustSales);
            ddlCustSales.Items.FindByText("--").Text = "All";

        }
    }

    #region Department Fill
    public void Department_Fill()
    {
        try
        {
            if (lblUserType.Text == "0")
            {
                Masters.Department.Department_Select(ddlDepartment);
                Masters.Department.Department_Select(ddlDeptSaleLead);
                Masters.Department.Department_Select(ddlDepDaily);
                ddlDepDaily.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlDepSalesAssign);
                ddlDepartment.Items.FindByText("--").Text = "All";
                ddlDeptSaleLead.Items.FindByText("--").Text = "All";
                ddlDepSalesAssign.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlPurchaseDepartment);
                ddlPurchaseDepartment.Items.FindByText("--").Text = "All";
                Masters.ItemCategory.ItemCategory_Select(ddlDepartmentProforma);
                ddlDepartmentProforma.Items.FindByText("--").Text = "All";
                //SM.DailyReport.ReferenceSelect(ddlReference);
                Masters.EnquiryMode.EnquiryMode_Select(ddlReference);
                ddlReference.Items.FindByText("--").Text = "All";
                Masters.ProductCompany.ProductCompany_Select(ddlEmployeeNameProforma);
                ddlEmployeeNameProforma.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlPurchaseDepartment2);
                ddlPurchaseDepartment2.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlDept);
                ddlDept.Items.FindByText("--").Text = "All";

                Masters.Department.Department_Select(ddlDepartmentInternal);
                ddlDepartmentInternal.Items.FindByText("--").Text = "All";
        
            }
            else 
            {
                Masters.Department.Department_Select(ddlDepartment);
                Masters.Department.Department_Select(ddlDeptSaleLead);
                Masters.Department.Department_Select(ddlDepDaily);
                ddlDepDaily.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlDepSalesAssign);
                ddlDepartment.Items.FindByText("--").Text = "All";
                ddlDeptSaleLead.Items.FindByText("--").Text = "All";
                ddlDepSalesAssign.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlPurchaseDepartment);
                ddlPurchaseDepartment.Items.FindByText("--").Text = "All";
                Masters.ItemCategory.ItemCategory_Select(ddlDepartmentProforma);
                ddlDepartmentProforma.Items.FindByText("--").Text = "All";
                Masters.EnquiryMode.EnquiryMode_Select(ddlReference );
                ddlReference.Items.FindByText("--").Text = "All";
                Masters.ProductCompany.ProductCompany_Select(ddlEmployeeNameProforma);
                ddlEmployeeNameProforma.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlPurchaseDepartment2);
                ddlPurchaseDepartment2.Items.FindByText("--").Text = "All";
                Masters.Department.Department_Select(ddlDept);
                ddlDept.Items.FindByText("--").Text = "All";

                Masters.Department.Department_Select(ddlDepartmentInternal);
                ddlDepartmentInternal.Items.FindByText("--").Text = "All";
                ddlDeptSaleLead.Enabled = ddlDepDaily.Enabled = ddlDepSalesAssign.Enabled = ddlPurchaseDepartment.Enabled = ddlPurchaseDepartment2.Enabled = ddlDept.Enabled = ddlDepartmentInternal.Enabled = ddlDepartment.Enabled = false;
                ddlDeptSaleLead.SelectedValue = ddlDepDaily.SelectedValue = ddlDepSalesAssign.SelectedValue
                    = ddlPurchaseDepartment.SelectedValue  = ddlPurchaseDepartment2.SelectedValue = ddlDept.SelectedValue = ddlDepartmentInternal.SelectedValue = ddlDepartment.SelectedValue = lblDeptId.Text;
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

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompanyName);
            ddlCompanyName.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyNameSalesAssign);
            ddlCompanyNameSalesAssign.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyMIDS);
            ddlCompanyMIDS.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlPurchaseCmpName);
            ddlPurchaseCmpName.Items.FindByText("--").Text = "All";

            Masters.CompanyProfile.Company_Select(ddlPurchaseCmpName2);
            ddlPurchaseCmpName2.Items.FindByText("--").Text = "All";
            
            Masters.CompanyProfile.Company_Select(ddlCompanyNameInternal);
            ddlCompanyNameInternal.Items.FindByText("--").Text = "All";
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

    #region Employee Names Fill
    private void EmployeeNames_Fill()
    {
        try
        {
            if (lblUserType.Text == "0")
            {
                HR.EmployeeMaster.EmployeeMaster_Select(ddlMIDSEmployeeName);
                ddlMIDSEmployeeName.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlMIDSEmployeeName2);
                ddlMIDSEmployeeName2.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlMPDSEmployeeName);
                ddlMPDSEmployeeName.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlPendingPaymentsEmployeeName);
                ddlPendingPaymentsEmployeeName.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmpDaily);
                ddlEmpDaily.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeNameSalesLead);
                ddlEmployeeNameSalesLead.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpSalesAssign);
                ddlEmpSalesAssign.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlPurchaseEmpName);
                ddlPurchaseEmpName.Items.FindByText("--").Text = "All";

                HR.EmployeeMaster.EmployeeMaster_Select(ddlPurchaseEmpName2);
                ddlPurchaseEmpName2.Items.FindByText("--").Text = "All";

                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmployeeNameInternal);
                ddlEmployeeNameInternal.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlEmp);
                ddlEmp.Items.FindByText("--").Text = "All";
            }
            else
            {
                HR.EmployeeMaster.EmployeeMaster_Select(ddlMIDSEmployeeName);
                ddlMIDSEmployeeName.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlMIDSEmployeeName2);
                ddlMIDSEmployeeName2.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlMPDSEmployeeName);
                ddlMPDSEmployeeName.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlPendingPaymentsEmployeeName);
                ddlPendingPaymentsEmployeeName.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmpDaily);
                ddlEmpDaily.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeNameSalesLead);
                ddlEmployeeNameSalesLead.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpSalesAssign);
                ddlEmpSalesAssign.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlPurchaseEmpName);
                ddlPurchaseEmpName.Items.FindByText("--").Text = "All";

                HR.EmployeeMaster.EmployeeMaster_Select(ddlPurchaseEmpName2);
                ddlPurchaseEmpName2.Items.FindByText("--").Text = "All";

                HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmployeeNameInternal);
                ddlEmployeeNameInternal.Items.FindByText("--").Text = "All";
                HR.EmployeeMaster.EmployeeMaster_Select(ddlEmp);
                ddlEmp.Items.FindByText("--").Text = "All";
                ddlMIDSEmployeeName.SelectedValue =ddlMIDSEmployeeName2.SelectedValue =ddlMPDSEmployeeName.SelectedValue =
                    ddlPendingPaymentsEmployeeName.SelectedValue =ddlEmpDaily.SelectedValue =ddlEmployeeNameSalesLead.SelectedValue =
                    ddlEmpSalesAssign.SelectedValue =ddlPurchaseEmpName.SelectedValue =ddlPurchaseEmpName2.SelectedValue =ddlEmployeeNameInternal.SelectedValue =
                    ddlEmp.SelectedValue =lblEmpId .Text ;
                ddlMIDSEmployeeName.Enabled =ddlMIDSEmployeeName2.Enabled =ddlMPDSEmployeeName.Enabled =
                    ddlPendingPaymentsEmployeeName.Enabled =ddlEmpDaily.Enabled =ddlEmployeeNameSalesLead.Enabled =
                    ddlEmpSalesAssign.Enabled =ddlPurchaseEmpName.Enabled =ddlPurchaseEmpName2.Enabled =ddlEmployeeNameInternal.Enabled =
                    ddlEmp.Enabled =false ;
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

    #region Customer Names Fill
    private void CustomerNames_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_Select(ddlSOSCustomerName);
            SM.CustomerMaster.CustomerMaster_Select(ddlSDBGCustomerName);
            ddlSOSCustomerName.Items.FindByText("--").Text = "All";
            ddlSDBGCustomerName.Items.FindByText("--").Text = "All";
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

    #region Ads Mode Fill
    private void AdsMode_Fill()
    {
        try
        {
            SM.AdvertisingMode.AdvertisingMode_Select(ddlAdsOSAdsMode);
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

    #region Ads Agency Fill
    private void AdsAgency_Fill()
    {
        try
        {
            SM.AdvertisingAgency.AdvertisingAgency_Select(ddlAdsOSAdsAgency);
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

    protected void lbtnMenuLinks_Click(object sender, EventArgs e)
    {
        LinkButton lbtnMenuLinks;
        lbtnMenuLinks = (LinkButton)sender;

        lbtnMIDS.CssClass = "leftmenu";
        lbtnMPDS.CssClass = "leftmenu";
        lbtnEMDReceived.CssClass = "leftmenu";
        lbtnEMDList.CssClass = "leftmenu";
        lbtnPendingPayments.CssClass = "leftmenu";
        lbtnSalesOutStanding.CssClass = "leftmenu";
        lbtnAdsOutStanding.CssClass = "leftmenu";
        lbtnSDBGStatement.CssClass = "leftmenu";
        lbtnSalesLeadStatement.CssClass = "leftmenu";
        lbtnSalesAssignment.CssClass = "leftmenu";
        lbtnPurchaseOrder.CssClass = "leftmenu";
        lbtnInternalOrder.CssClass = "leftmenu";
        lbtnDailyReport.CssClass = "leftmenu";
        lbtnResverveStock.CssClass = "leftmenu";
        

        tblMIDS.Visible = false;
        tblMPDS.Visible = false;
        tblEMDReceived.Visible = false;
        tblEMDList.Visible = false;
        tblPendingPayments.Visible = false;
        tblSalesOutStanding.Visible = false;
        tblAdsOutStanding.Visible = false;
        tblSDBGStatement.Visible = false;
        tblSalesLeadsStatement.Visible = false;
        tblSalesAssignment .Visible = false;
        tblPurchase.Visible = false;
        tblInternalOrderstmt.Visible=false;
        tblDailyReport.Visible = false;
        tblProforma.Visible = false;
        tblReserveStockHistory.Visible = false;
        tblArchitect.Visible = false;

        switch (lbtnMenuLinks.ID)
        {
            case "lbtnMIDS":
                {
                    tblMIDS.Visible = true;
                    tblMIDS2.Visible = true;
                    lbtnMIDS.CssClass = "leftmenuhighlight";
                    txtMIDSYear.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDateMids.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnMPDS":
                {
                    tblMPDS.Visible = true;
                    lbtnMPDS.CssClass = "leftmenuhighlight";
                    txtMPDSYear.Text = DateTime.Now.Year.ToString();
                    break;
                }
            case "lbtnEMDReceived":
                {
                    tblEMDReceived.Visible = true;
                    lbtnEMDReceived.CssClass = "leftmenuhighlight";
                    txtEMDReceivedFromDate.Text = txtEMDReceivedToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnEMDList":
                {
                    tblEMDList.Visible = true;
                    lbtnEMDList.CssClass = "leftmenuhighlight";
                    txtEMDListFromDate.Text = txtEMDListToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnPendingPayments":
                {
                    tblPendingPayments.Visible = true;
                    lbtnPendingPayments.CssClass = "leftmenuhighlight";
                    txtPendingPaymentsYear.Text = DateTime.Now.Year.ToString();
                    break;
                }
            case "lbtnSalesOutStanding":
                {
                    tblSalesOutStanding.Visible = true;
                    lbtnSalesOutStanding.CssClass = "leftmenuhighlight";
                    break;
                }
            case "lbtnAdsOutStanding":
                {
                    tblAdsOutStanding.Visible = true;
                    lbtnAdsOutStanding.CssClass = "leftmenuhighlight";
                    break;
                }
            case "lbtnSDBGStatement":
                {
                    tblSDBGStatement.Visible = true;
                    lbtnSDBGStatement.CssClass = "leftmenuhighlight";
                    break;
                }
            case "lbtnSalesLeadStatement":
                {
                    tblSalesLeadsStatement.Visible = true;
                    lbtnSalesLeadStatement.CssClass = "leftmenuhighlight";
                    txtSalesLeadFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtSalesLeadTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnSalesAssignment": 
                {
                    tblSalesAssignment .Visible = true;
                    lbtnSalesAssignment.CssClass = "leftmenuhighlight";
                    txtSalesAssignFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtSalesAssignTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnPurchaseOrder":
                {
                    tblPurchase.Visible = true;
                    lbtnPurchaseOrder.CssClass = "leftmenuhighlight";
                    txtPurchaseFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtPurchaseTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    
                    txtPurchaseFrom2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtPurchaseTo2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    
                    break;

                }
                case "lbtnInternalOrder":
                {
                    tblInternalOrderstmt.Visible = true;
                    lbtnInternalOrder.CssClass = "leftmenuhighlight";
                    txtinternalsalsend.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtinternalsalesto.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
                case "lbtnProformaInvoice":
                {
                    tblProforma.Visible = true;
                    lbtnProformaInvoice.CssClass = "leftmenuhighlight";
                    txtProformaFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtproformaTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnDailyReport":
                {
                    tblDailyReport.Visible = true;
                    lbtnDailyReport.CssClass = "leftmenuhighlight";
                    txtFromDailyRep.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDailyRep.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnResverveStock":
                {
                    tblReserveStockHistory.Visible = true;
                    lbtnResverveStock.CssClass = "leftmenuhighlight";
                    //txtfrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbtnArchitect":
                {
                    tblArchitect .Visible = true;
                    lbtnArchitect .CssClass = "leftmenuhighlight";
                    //txtfrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmp, ddlDept.SelectedValue);
            ddlEmp.Items.FindByText("--").Text = "All";
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
    protected void btnMIDSRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=mids&y=" + Yantra.Classes.General.toMMDDYYYY(txtMIDSYear.Text) + "&m=" + Yantra.Classes.General.toMMDDYYYY(txtToDateMids.Text) + "&e=" + ddlMIDSEmployeeName.SelectedValue + "&cmp=" + ddlCompanyMIDS.SelectedValue + "&d=" + ddlDepartment.SelectedValue + "&expec=" + chkIsExpectedOrder.Checked + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnMPDSRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=mpds&y=" + txtMPDSYear.Text + "&m=" + ddlMPDSMonth.SelectedItem.Value + "&e=" + ddlMPDSEmployeeName.SelectedItem.Value + "&cs=" + ddlMPDSCustomerStatus.SelectedItem.Text + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnEMDReceivedRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=emdreceived&f=" + Yantra.Classes.General.toMMDDYYYY(txtEMDReceivedFromDate.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtEMDReceivedToDate.Text) + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnEMDListRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=emdlist&f=" + Yantra.Classes.General.toMMDDYYYY(txtEMDListFromDate.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtEMDListToDate.Text) + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnPendingPaymentsRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=pendingpayments&y=" + txtPendingPaymentsYear.Text + "&m=" + ddlPendingPaymentsMonth.SelectedItem.Value + "&e=" + ddlPendingPaymentsEmployeeName.SelectedItem.Value + "&cs=" + ddlPendingPaymentsCustomerStatus.SelectedItem.Text + "&d=" + ddlPendingPaymentsDepartment.SelectedItem.Value + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    string nodParameter;
    protected void btnSalesOutStandingRpt_Click(object sender, EventArgs e)
    {
        if (ddlSOSNoOfDays.SelectedItem.Value == "All") { nodParameter = "&nod1=All&nod2=All"; }
        else if (ddlSOSNoOfDays.SelectedItem.Value == "<=30") { nodParameter = "&nod1=<=30&nod2=<=30"; }
        else if (ddlSOSNoOfDays.SelectedItem.Value == "30-60") { nodParameter = "&nod1=>30&nod2=<=60"; }
        else if (ddlSOSNoOfDays.SelectedItem.Value == "60-90") { nodParameter = "&nod1=>60&nod2=<=90"; }
        else if (ddlSOSNoOfDays.SelectedItem.Value == "90-120") { nodParameter = "&nod1=>90&nod2=<=120"; }
        else if (ddlSOSNoOfDays.SelectedItem.Value == ">120") { nodParameter = "&nod1=>120&nod2=>120"; }

        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=sos&cid=" + ddlSOSCustomerName.SelectedItem.Value + nodParameter + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes','ddddddddddddd')", true);
    }

    protected void btnAdsOutStandingRpt_Click(object sender, EventArgs e)
    {
        if (ddlAdsOSAdsNoOfDays.SelectedItem.Value == "All") { nodParameter = "&nod1=All&nod2=All"; }
        else if (ddlAdsOSAdsNoOfDays.SelectedItem.Value == "<=30") { nodParameter = "&nod1=<=30&nod2=<=30"; }
        else if (ddlAdsOSAdsNoOfDays.SelectedItem.Value == "30-60") { nodParameter = "&nod1=>30&nod2=<=60"; }
        else if (ddlAdsOSAdsNoOfDays.SelectedItem.Value == "60-90") { nodParameter = "&nod1=>60&nod2=<=90"; }
        else if (ddlAdsOSAdsNoOfDays.SelectedItem.Value == "90-120") { nodParameter = "&nod1=>90&nod2=<=120"; }
        else if (ddlAdsOSAdsNoOfDays.SelectedItem.Value == ">120") { nodParameter = "&nod1=>120&nod2=>120"; }

        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=adsoutstanding&mode=" + ddlAdsOSAdsMode.SelectedItem.Text + "&agcy=" + ddlAdsOSAdsAgency.SelectedItem.Value + "" + nodParameter + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes','ddddddddddddd')", true);
    }

    protected void btnSDBGStatementRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=sdbg&c=" + ddlSDBGCustomerName.SelectedItem.Value + "&s=" + ddlSDBGStatementOf.SelectedItem.Value + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes','ddddddddddddd')", true);
    }

    protected void btnSalesLeadStatement_Click(object sender, EventArgs e)
    {
        //if (ddlCompanyName.SelectedItem.Value == "All") { string lbl }
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=SalesLeadStatement&FromSales=" + Yantra.Classes.General.toMMDDYYYY(txtSalesLeadFrom.Text) + "&ToSales=" + Yantra.Classes.General.toMMDDYYYY(txtSalesLeadTo.Text) + "&Comapanyid="+ddlCompanyName.SelectedValue +"&ut="+lblUserType.Text +"&empid="+ddlEmployeeNameSalesLead.SelectedValue+"&dep="+ddlDeptSaleLead.SelectedValue+"&emp="+ddlEmployeeNameSalesLead.SelectedValue +"";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnSalesAssignment_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=SalesAssignmentStatement&FromSalesAssign=" + Yantra.Classes.General.toMMDDYYYY(txtSalesAssignFrom.Text) + "&ToSalesAssign=" + Yantra.Classes.General.toMMDDYYYY(txtSalesAssignTo.Text) + "&Comapanyid=" + ddlCompanyNameSalesAssign.SelectedValue + "&Status=" + ddlStatus.SelectedValue + "&dep="+ddlDepSalesAssign.SelectedValue+"&emp="+ddlEmpSalesAssign.SelectedValue;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlMIDSEmployeeName, ddlDepartment.SelectedValue);
            ddlMIDSEmployeeName.Items.FindByText("--").Text = "All";
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

    protected void ddlDeptSaleLead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmployeeNameSalesLead, ddlDeptSaleLead.SelectedValue);
            ddlEmployeeNameSalesLead.Items.FindByText("--").Text = "All";
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
    protected void ddlDepSalesAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmpSalesAssign, ddlDepSalesAssign.SelectedValue);
            ddlEmpSalesAssign.Items.FindByText("--").Text = "All";
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
    protected void ddlPurchaseDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlPurchaseEmpName, ddlPurchaseDepartment.SelectedValue);
            ddlPurchaseEmpName.Items.FindByText("--").Text = "All";
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
    protected void btnPurchaseOrder_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=POStatement&FromPO=" + Yantra.Classes.General.toMMDDYYYY(txtPurchaseFrom.Text) + "&ToPO=" + Yantra.Classes.General.toMMDDYYYY(txtPurchaseTo.Text) + "&Comapanyid=" + ddlPurchaseCmpName.SelectedValue + "&dep=" + ddlPurchaseDepartment.SelectedValue + "&emp=" + ddlPurchaseEmpName.SelectedValue;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void btnInternal_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=InternalOrder&FromInternal=" + Yantra.Classes.General.toMMDDYYYY(txtinternalsalsend.Text) + "&ToInternal=" + Yantra.Classes.General.toMMDDYYYY(txtinternalsalesto.Text) + "&Comapanyid=" + ddlCompanyNameInternal.SelectedValue + "&empid=" + ddlEmployeeNameInternal.SelectedValue + "&dep=" + ddlDepartmentInternal.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void ddlDepartmentInternal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectFromDepartment(ddlEmployeeNameInternal, ddlDepartmentInternal.SelectedValue);
            ddlEmployeeNameInternal.Items.FindByText("--").Text = "All";
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
    
    protected void ddlDepDaily_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmpDaily, ddlDepDaily.SelectedValue);
            ddlEmpDaily.Items.FindByText("--").Text = "All";
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
    protected void runDailyReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=DailyReport&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDailyRep.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDailyRep.Text) + "&empid=" + ddlEmpDaily.SelectedValue + "&dep=" + ddlDepDaily.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void btnMIDSRpt2_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=mids2&y=" + Yantra.Classes.General.toMMDDYYYY(txtMIDSYear2.Text) + "&m=" + Yantra.Classes.General.toMMDDYYYY(txtToDateMids2.Text) + "&e=" + ddlMIDSEmployeeName2.SelectedValue + " ";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }

    protected void ddlPurchaseDepartment2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlPurchaseEmpName2, ddlPurchaseDepartment2.SelectedValue);
            ddlPurchaseEmpName2.Items.FindByText("--").Text = "All";
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
    protected void btnPurchaseOrder2_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=PurchaseStmt_Executive&FromPO=" + Yantra.Classes.General.toMMDDYYYY(txtPurchaseFrom2.Text) + "&ToPO=" + Yantra.Classes.General.toMMDDYYYY(txtPurchaseTo2.Text) + "&Comapanyid=" + ddlPurchaseCmpName2.SelectedValue + "&dep=" + ddlPurchaseDepartment2.SelectedValue + "&emp=" + ddlPurchaseEmpName2.SelectedValue;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        tblpRint.Visible = true;
    }
    protected void chkOriginal_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ProformaInvoice&f=" + Yantra.Classes.General.toMMDDYYYY(txtProformaFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtproformaTo.Text) + "&c=" + ddlCompanyNameProforma.SelectedValue + "&e=" + ddlDepartmentProforma.SelectedValue + "&d=" + ddlEmployeeNameProforma.SelectedValue + "&Reg= "+ddlRegion .SelectedValue +"";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void chkDuplicate_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ProformaInvoiceBar&f=" + Yantra.Classes.General.toMMDDYYYY(txtProformaFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtproformaTo.Text) + "&c=" + ddlCompanyNameProforma.SelectedValue + "&e=" + ddlDepartmentProforma.SelectedValue + "&d=" + ddlEmployeeNameProforma.SelectedValue + "&Reg= " + ddlRegion.SelectedValue + " ";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void chktriplicate_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=CustBrand&f=" + Yantra.Classes.General.toMMDDYYYY(txtProformaFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtproformaTo.Text) + "&c=" + ddlCompanyNameProforma.SelectedValue + "&e=" + ddlDepartmentProforma.SelectedValue + "&d=" + ddlEmployeeNameProforma.SelectedValue + "&Reg= " + ddlRegion.SelectedValue + " ";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=Salesrpt&y=" + Yantra.Classes.General.toMMDDYYYY(txtfrom.Text) + "&m=" + Yantra.Classes.General.toMMDDYYYY(txtTo.Text) + "&cmp=" + DropDownList3.SelectedValue + "&Dept=" + ddlDept.SelectedValue + "&Emp=" + ddlEmp.SelectedValue + "&ST=" + ddlSaleType.SelectedValue + "&Cust=" + ddlCustSales.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
  
    }
    protected void btnRunReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=Architect&cat=" +ddlCategory .SelectedItem .Text+"&City=" +txtCity .SelectedItem .Text +"&Pincode=" +txtPincode .SelectedItem .Text +"" ;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
   
    }
}

 
