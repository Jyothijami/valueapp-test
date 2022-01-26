using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using YantraBLL.Modules;

public partial class dev_pages_SalesReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlCustomer);
            SM.SalesOrder.SalesOrder_Select(ddlproject);
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.SalesOrder obj = new SM.SalesOrder();



        lblEnquiryCount.Text = General.CountofRecordsWithQuery("select count(CUST_ID) from YANTRA_ENQ_MAST where CUST_ID ='" + ddlCustomer.SelectedItem .Value  + "' ").ToString();
        General.GridBindwithCommand(gvEnquires, "	select *,Cou = (select count(*) from YANTRA_ENQ_DET  where YANTRA_ENQ_DET .ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID ) from YANTRA_ENQ_MAST,YANTRA_CUSTOMER_MAST,YANTRA_COMP_PROFILE  where YANTRA_ENQ_MAST.CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID and yantra_enq_mast.CP_ID =YANTRA_COMP_PROFILE .CP_ID  and YANTRA_ENQ_MAST.CUST_ID ='" + ddlCustomer.SelectedItem.Value + "'");

        lblquationscount.Text = General.CountofRecordsWithQuery("select count(CUST_ID) from YANTRA_QUOT_MAST,YANTRA_ENQ_MAST   where YANTRA_QUOT_MAST .ENQ_ID =YANTRA_ENQ_MAST .ENQ_ID and Cust_ID ='" + ddlCustomer.SelectedItem.Value + "' ").ToString();
        General.GridBindwithCommand(gvTotalQuatations, "sELECT *,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY ,forCheck .EMP_FIRST_NAME as CheckedBy  "+
								" FROM [YANTRA_QUOT_MAST]	inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID "+
									" inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID LEFT OUTER JOIN  dbo.YANTRA_QUOT_APPROVERS ON YANTRA_QUOT_APPROVERS.QUOT_ID = YANTRA_QUOT_MAST.QUOT_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID "+
                                " left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_APPROVERS].Quatation_Approved	left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID  left outer join YANTRA_EMPLOYEE_MAST forcheck on forcheck .EMP_ID =YANTRA_QUOT_MAST.QUOT_SALESP_ID left outer join [V_lOCATION] comp  on comp.CP_ID=[YANTRA_QUOT_MAST].CP_ID" +
                                " Where YANTRA_ENQ_MAST.Cust_ID ='" + ddlCustomer.SelectedItem.Value + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_ID DESC");

        lblsalesorderstatus.Text = General.CountofRecordsWithQuery("select count(SO_CUST_ID) from YANTRA_SO_MAST where SO_CUST_ID  ='" + ddlCustomer.SelectedItem.Value + "' ").ToString();
        
        General.GridBindwithCommand(gvSalesorder, "SELECT  distinct [YANTRA_SO_MAST].SO_ID ,SO_NO ,SO_DATE ,CUST_NAME ,CUST_CONTACT_PERSON,SO_ACCEPTANCE_FLAG , emp.EMP_FIRST_NAME as Executive ,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY,[V_lOCATION].Full_CompName ,balanceqty=(Select SUM([YANTRA_SO_DET].BalanceQty) ) FROM [YANTRA_SO_MAST]"+
		" inner join YANTRA_SO_DET on YANTRA_SO_MAST .SO_ID =YANTRA_SO_DET .SO_ID inner join [YANTRA_QUOT_MAST] on [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID "+
		" inner join [V_lOCATION] on [V_lOCATION].CP_ID=[YANTRA_SO_MAST].CP_ID inner join YANTRA_EMPLOYEE_MAST emp on emp.EMP_ID=[YANTRA_ENQ_MAST].[ENQ_ORIG_NAME] left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_SO_MAST].SO_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_SO_MAST].SO_APPROVED_BY"+
		" left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID Where SO_CUST_ID ='" + ddlCustomer.SelectedItem.Value + "' group by [YANTRA_SO_MAST].SO_ID ,SO_NO ,SO_DATE ,CUST_NAME ,CUST_CONTACT_PERSON,SO_ACCEPTANCE_FLAG , emp.EMP_FIRST_NAME  ,FORPREPARED.EMP_FIRST_NAME ,FORAPPROVED.EMP_FIRST_NAME ,[V_lOCATION].Full_CompName order by [YANTRA_SO_MAST].SO_ID DESC");

    }
    protected void gvSalesorder_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (e.Row.Cells[8].Text == "ManuallyClosed") { e.Row.Cells[10].Text = "ManuallyClosed"; }
            if (e.Row.Cells[8].Text == "Obsolete") { e.Row.Cells[10].Text = "Obsolete"; }

        }
    }
}