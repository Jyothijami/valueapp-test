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

public partial class Modules_Reports_Services : basePage
{
    string pagenavigationstr;
    protected void Page_Load(object sender, EventArgs e)
    
{
        if (!IsPostBack)
        {
            //EmployeeNames_Fill();
            //CompanyName_Fill();
            //Department_Fill();
            CustomerNames_Fill();
        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {

        if (txtSearchModel.Text != "")
        {
            ddlCust.DataSourceID = "SqlDataSource1";
            ddlCust.DataTextField = "Cust_Name";
            ddlCust.DataValueField = "Cust_Id";
            ddlCust.DataBind();
            //ddlCust_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }
    public void CustomerNames_Fill()
    {
        try
        {
            Services.ServiceCustInfo.ServiceCust_Select(ddlCust);
            ddlCust.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyMIDS);
            ddlCompanyMIDS.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyMIDS0);
            ddlCompanyMIDS0.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    //#region Department Fill
    //public void Department_Fill()
    //{
    //    try
    //    {
    //        Masters.Department.Department_Select(ddlDepartmentCrl);
    //        //Masters.Department.Department_Select(ddlDeptSrl);
    //        Masters.Department.Department_Select(ddlDeptAssign);
    //        ddlDepartmentCrl.Items.FindByText("--").Text = "All";
    //        //ddlDeptSrl.Items.FindByText("--").Text = "All";
    //        ddlDeptAssign.Items.FindByText("--").Text = "All";


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

    //#region Employee Names Fill
    //private void EmployeeNames_Fill()
    //{
    //    try
    //    {

    //        HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpCrl);
    //        ddlEmpCrl.Items.FindByText("--").Text = "All";
    //        //HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpSrl );
    //        //ddlEmpSrl.Items.FindByText("--").Text = "All";
    //        HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpAssign);
    //        ddlEmpAssign.Items.FindByText("--").Text = "All";

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        HR.Dispose();
    //    }

    //}
    //#endregion

    //#region Customer Names Fill
    //private void CustomerNames_Fill()
    //{
    //    try
    //    {
    //        //SM.CustomerMaster.CustomerMaster_Select(ddlAOSCustomerName);
    //        //ddlAOSCustomerName.Items.FindByText("--").Text = "All";
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        SM.Dispose();
    //    }
    //}
    //#endregion

    //#region CompanyName Fill
    //public void CompanyName_Fill()
    //{
    //    try
    //    {
    //        Masters.CompanyProfile.Company_Select(ddlCompanyNameCrl);
    //        ddlCompanyNameCrl.Items.FindByText("--").Text = "All";
    //        //Masters.CompanyProfile.Company_Select(ddlCmpSrl);
    //        //ddlCmpSrl.Items.FindByText("--").Text = "All";
    //        Masters.CompanyProfile.Company_Select(ddlCmpAsskgn);
    //        ddlCmpAsskgn.Items.FindByText("--").Text = "All";

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



    protected void lbtnMenuLinks_Click(object sender, EventArgs e)
    {
        LinkButton lbtnMenuLinks;
        lbtnMenuLinks = (LinkButton)sender;


        //lbtnServiceReportList.CssClass = "leftmenu";
        lbtnComplaintRegisterList.CssClass = "leftmenu";
        //lbtnServiceAssignList.CssClass = "leftmenu";
        LinkButton1.CssClass = "leftmenu";
        LinkButton2.CssClass = "leftmenu";


        //tblServiceReportList.Visible = false;
        tblComplaintRecordList.Visible = false;
        Table1.Visible = false;
        tblCompRegByClient.Visible = false;


        switch (lbtnMenuLinks.ID)
        {
            case "lbtnComplaintRegisterList":
                {
                    tblComplaintRecordList.Visible = true;
                    lbtnComplaintRegisterList.CssClass = "leftmenuhighlight";
                    txtCRLFromDate.Text = txtCRLToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "LinkButton1":
                {
                    Table1.Visible = true;
                    LinkButton1.CssClass = "leftmenuhighlight";
                    txtFrom2.Text = txtTo2.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "LinkButton2":
                {
                    tblCompRegByClient.Visible = true;
                    LinkButton2.CssClass = "leftmenuhighlight";
                    txtFromdt.Text = txtTodt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "LinkButton3":
                {
                    tblPO.Visible = true;
                    LinkButton3.CssClass = "leftmenuhighlight";

                    break;
                }
            default:
                {
                    break;
                }
        }
    }


    string nodParameter;
    //protected void btnAMCOutStandingRpt_Click(object sender, EventArgs e)
    //  {
    //      if (ddlPendingPaymentsNoOfDays.SelectedItem.Value == "All") { nodParameter = "&nod1=All&nod2=All"; }
    //      else if (ddlPendingPaymentsNoOfDays.SelectedItem.Value == "<=30") { nodParameter = "&nod1=<=30&nod2=<=30"; }
    //      else if (ddlPendingPaymentsNoOfDays.SelectedItem.Value == "30-60") { nodParameter = "&nod1=>30&nod2=<=60"; }
    //      else if (ddlPendingPaymentsNoOfDays.SelectedItem.Value == "60-90") { nodParameter = "&nod1=>60&nod2=<=90"; }
    //      else if (ddlPendingPaymentsNoOfDays.SelectedItem.Value == "90-120") { nodParameter = "&nod1=>90&nod2=<=120"; }
    //      else if (ddlPendingPaymentsNoOfDays.SelectedItem.Value == ">120") { nodParameter = "&nod1=>120&nod2=>120"; }

    //      pagenavigationstr = "../Reports/EODReportViewer.aspx?type=pendingpayments&y=" + txtPendingPaymentsYear.Text + "&m=" + ddlPendingPaymentsMonth.SelectedItem.Value + "&e=" + ddlPendingPaymentsEmployee.SelectedItem.Value + "&cs=" + ddlPendingPaymentsCustomerStatus.SelectedItem.Text + nodParameter + "&d=amc";
    //      System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    //  }

    //protected void btnSOPListRpt_Click(object sender, EventArgs e)
    //{
    //    pagenavigationstr = "../Reports/EODReportViewer.aspx?type=sop&f=" + Yantra.Classes.General.toMMDDYYYY(txtSOPFromDate.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtSOPToDate.Text) + "";
    //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    //}
    //protected void btnAOPListRpt_Click(object sender, EventArgs e)
    //{
    //    pagenavigationstr = "../Reports/EODReportViewer.aspx?type=aop&f=" + Yantra.Classes.General.toMMDDYYYY(txtAOPFromDate.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtAOPToDate.Text) + "";
    //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    //}
    //protected void btnServiceReportListRpt_Click(object sender, EventArgs e)
    //{
    //    pagenavigationstr = "../Reports/EODReportViewer.aspx?type=srl&f=" + Yantra.Classes.General.toMMDDYYYY(txtSRLFromDate.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtSRLToDate.Text) + "";
    //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    //}
    protected void btnComplaintRecordListRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=crl&f=" + Yantra.Classes.General.toyymmdd(txtCRLFromDate.Text) + "&t=" + Yantra.Classes.General.toyymmdd(txtCRLToDate.Text) + "&Comp="+ddlCompanyMIDS0 .SelectedValue +"";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    //protected void ddlDepartmentCrl_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        HR.EmployeeMaster.EmployeeMaster_SelectFromDepartment(ddlEmpCrl, ddlDepartmentCrl.SelectedValue);
    //        ddlEmpCrl.Items.FindByText("--").Text = "All";
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        HR.Dispose();
    //    }
    //}
    //protected void ddlDeptSrl_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //HR.EmployeeMaster.EmployeeMaster_SelectFromDepartment(ddlEmpSrl, ddlDeptSrl.SelectedValue);
    //        //ddlEmpSrl.Items.FindByText("--").Text = "All";
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        HR.Dispose();
    //    }

    //}
    //protected void btnAssign_Click(object sender, EventArgs e)
    //{
    //    pagenavigationstr = "../Reports/EODReportViewer.aspx?type=sal&f=" + Yantra.Classes.General.toMMDDYYYY(txtFromAssign.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtToAssign.Text) + "&st=" + ddlServiceAssign.SelectedItem.Value + "&c=" + ddlCmpAsskgn.SelectedValue + "&e=" + ddlEmpAssign.SelectedValue + "&d=" + ddlDeptAssign.SelectedValue + "";
    //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=Cou&f=" + Yantra.Classes.General.toyymmdd(txtFrom2.Text) + "&t=" + Yantra.Classes.General.toyymmdd(txtTo2.Text) + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void btnRunrpt_Click(object sender, EventArgs e)
    {
        //pagenavigationstr = "../Reports/EODReportViewer.aspx?type=crd&CrD=" + ddlCust.SelectedItem.Value + "&PO=" + ddlPONO.SelectedItem.Value + "&f=" + Yantra.Classes.General.toyymmdd(txtFromdt.Text) + "&t=" + Yantra.Classes.General.toyymmdd(txtTodt.Text) + "";

        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=crd&CrD=" + ddlCust.SelectedItem.Value + "&PONO=" + 0 + "&f=" + Yantra.Classes.General.toyymmdd(txtFromdt.Text) + "&t=" + Yantra.Classes.General.toyymmdd(txtTodt.Text) + "&Status=" + ddlStatus.SelectedValue + "&Comp="+ddlCompanyMIDS .SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);


    }
    protected void chktechrpt_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=crdet&CrD=" + ddlCust.SelectedItem.Value + "&PONO=" + 0 + "&f=" + Yantra.Classes.General.toyymmdd(txtFromdt.Text) + "&t=" + Yantra.Classes.General.toyymmdd(txtTodt.Text) + "&Status=" + ddlStatus.SelectedValue + "&Comp=" + ddlCompanyMIDS.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

    }
    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "PO Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
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
        gvSalesOrderDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "PO Date")
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
        //BindGrid();
        gvSalesOrderDetails.DataBind();
    }
    #endregion
    protected void gvSalesOrderDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalesOrderDetails.PageIndex = e.NewPageIndex;
        //BindGrid();
        gvSalesOrderDetails.DataBind();
    }
    protected void gvSalesOrderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[10].Text != "0" && e.Row.Cells[7].Text != "") { e.Row.Cells[10].Text = "Running"; }
            if (e.Row.Cells[10].Text == "0" && e.Row.Cells[7].Text != "") { e.Row.Cells[10].Text = "Closed"; }
            if (e.Row.Cells[8].Text == "ManuallyClosed") { e.Row.Cells[10].Text = "Manually Closed"; }
            if (e.Row.Cells[8].Text == "Obsolete") { e.Row.Cells[10].Text = "Obsolete"; }
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

       foreach (GridViewRow gvrow in gvSalesOrderDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("checkbox1");
            if (ch.Checked == true)
            {
                string SO_ID = gvrow.Cells[0].Text;
                pagenavigationstr = "../Reports/EODReportViewer.aspx?type=POTech&SoId=" + SO_ID + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
                ch.Checked = false;
            }

        }
    }
   
}



