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
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
public partial class Modules_SCM_SelfPurchaseOrderq : basePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            // DateTime date = DateTime.Now.ToString();
            SupplierPO_Fill();
           
        }

    }


    #region Grid Fill
    private void SupplierPO_Fill()
    {
        try
        {
            string hai = "Close";
            General.GridBindwithCommand(gvPOOrdersearch, "SELECT SELF_PO_MAST.FPO_NO, YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_MODEL_NO, SELF_PO_MASTER_DET.FPO_DET_REMARKS, SELF_PO_MASTER_DET.FPO_DET_QTY, SELF_PO_MASTER_DET.FPO_DET_DELIVERY_DATE, SELF_PO_MASTER_DET.FPO_DET_CUSTOMER, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,SELF_PO_MASTER_DET.FPO_DET_COLOR,SELF_PO_MASTER_DET.FPO_DET_STATUS,SELF_PO_MASTER_DET.FPO_DET_EXPDATE,SELF_PO_MASTER_DET.FPOS_DET_ID,SELF_PO_MAST.FPO_DATE FROM YANTRA_ITEM_MAST INNER JOIN SELF_PO_MASTER_DET ON YANTRA_ITEM_MAST.ITEM_CODE = SELF_PO_MASTER_DET.ITEM_CODE INNER JOIN SELF_PO_MAST ON SELF_PO_MASTER_DET.FPOS_ID = SELF_PO_MAST.FPOS_ID INNER JOIN YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID where SELF_PO_MASTER_DET.FPO_DET_STATUS != '" + hai + "'");

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }

    }
    #endregion

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPOOrdersearch.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvPOOrdersearch.DataBind();
    }
    protected void gvPOOrdersearch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvPOOrdersearch.EditIndex = e.NewEditIndex;
        SupplierPO_Fill();
    }


    protected void gvPOOrdersearch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SCM.SupplierSelfPO obj = new SCM.SupplierSelfPO();
        GridViewRow row = gvPOOrdersearch.Rows[e.RowIndex];
        TextBox txtExpDate = (TextBox)row.FindControl("txtExpDate");
        TextBox txtArrivedDate = (TextBox)row.FindControl("txtArrivedDate");
        string expdate = txtExpDate.Text;
        string arriveddate = txtArrivedDate.Text;
        Label lblId = (Label)gvPOOrdersearch.Rows[e.RowIndex].FindControl("lblId");
        DropDownList ddlStatus = (DropDownList)gvPOOrdersearch.Rows[e.RowIndex].FindControl("ddlStatus");


     //   obj.SuppliersSelfPODetStatus_Update(ddlStatus.SelectedValue, Yantra.Classes.General.toMMDDYYYY(expdate),Yantra.Classes.General.toMMDDYYYY(arriveddate), lblId.Text);

        obj.SuppliersSelfPODetStatus_Update(ddlStatus.SelectedValue, expdate, arriveddate, lblId.Text);
      


        gvPOOrdersearch.EditIndex = -1;
        SupplierPO_Fill();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {


        if (ddlSearchBy.SelectedItem.Value != "0" && txtSearch.Text != "")
        {
            if (ddlSearchBy.SelectedItem.Value == "Model No")
            {
                General.GridBindwithCommand(gvPOOrdersearch, "SELECT SELF_PO_MAST.FPO_NO, YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_MODEL_NO, SELF_PO_MASTER_DET.FPO_DET_REMARKS, SELF_PO_MASTER_DET.FPO_DET_QTY, SELF_PO_MASTER_DET.FPO_DET_DELIVERY_DATE, SELF_PO_MASTER_DET.FPO_DET_CUSTOMER, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,SELF_PO_MASTER_DET.FPO_DET_COLOR,SELF_PO_MASTER_DET.FPO_DET_STATUS,SELF_PO_MASTER_DET.FPO_DET_EXPDATE,SELF_PO_MASTER_DET.FPOS_DET_ID,SELF_PO_MAST.FPO_DATE FROM YANTRA_ITEM_MAST INNER JOIN SELF_PO_MASTER_DET ON YANTRA_ITEM_MAST.ITEM_CODE = SELF_PO_MASTER_DET.ITEM_CODE INNER JOIN SELF_PO_MAST ON SELF_PO_MASTER_DET.FPOS_ID = SELF_PO_MAST.FPOS_ID INNER JOIN YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID where YANTRA_ITEM_MAST.ITEM_MODEL_NO like '" + txtSearch.Text + "%' ");

            }
            if (ddlSearchBy.SelectedItem.Value == "Customer")
            {
                General.GridBindwithCommand(gvPOOrdersearch, "SELECT SELF_PO_MAST.FPO_NO, YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_MODEL_NO, SELF_PO_MASTER_DET.FPO_DET_REMARKS, SELF_PO_MASTER_DET.FPO_DET_QTY, SELF_PO_MASTER_DET.FPO_DET_DELIVERY_DATE, SELF_PO_MASTER_DET.FPO_DET_CUSTOMER, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,SELF_PO_MASTER_DET.FPO_DET_COLOR,SELF_PO_MASTER_DET.FPO_DET_STATUS,SELF_PO_MASTER_DET.FPO_DET_EXPDATE,SELF_PO_MASTER_DET.FPOS_DET_ID,SELF_PO_MAST.FPO_DATE FROM YANTRA_ITEM_MAST INNER JOIN SELF_PO_MASTER_DET ON YANTRA_ITEM_MAST.ITEM_CODE = SELF_PO_MASTER_DET.ITEM_CODE INNER JOIN SELF_PO_MAST ON SELF_PO_MASTER_DET.FPOS_ID = SELF_PO_MAST.FPOS_ID INNER JOIN YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID where SELF_PO_MASTER_DET.FPO_DET_CUSTOMER like '" + txtSearch.Text + "%'");
            }
            if (ddlSearchBy.SelectedItem.Value == "PO No")
            {
                General.GridBindwithCommand(gvPOOrdersearch, "SELECT SELF_PO_MAST.FPO_NO, YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_MODEL_NO, SELF_PO_MASTER_DET.FPO_DET_REMARKS, SELF_PO_MASTER_DET.FPO_DET_QTY, SELF_PO_MASTER_DET.FPO_DET_DELIVERY_DATE, SELF_PO_MASTER_DET.FPO_DET_CUSTOMER, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,SELF_PO_MASTER_DET.FPO_DET_COLOR,SELF_PO_MASTER_DET.FPO_DET_STATUS,SELF_PO_MASTER_DET.FPO_DET_EXPDATE,SELF_PO_MASTER_DET.FPOS_DET_ID,SELF_PO_MAST.FPO_DATE FROM YANTRA_ITEM_MAST INNER JOIN SELF_PO_MASTER_DET ON YANTRA_ITEM_MAST.ITEM_CODE = SELF_PO_MASTER_DET.ITEM_CODE INNER JOIN SELF_PO_MAST ON SELF_PO_MASTER_DET.FPOS_ID = SELF_PO_MAST.FPOS_ID INNER JOIN YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID where SELF_PO_MAST.FPO_NO like '" + txtSearch.Text + "%' ");
            }
            if (ddlSearchBy.SelectedItem.Value == "Status")
            {
                General.GridBindwithCommand(gvPOOrdersearch, "SELECT SELF_PO_MAST.FPO_NO, YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_MODEL_NO, SELF_PO_MASTER_DET.FPO_DET_REMARKS, SELF_PO_MASTER_DET.FPO_DET_QTY, SELF_PO_MASTER_DET.FPO_DET_DELIVERY_DATE, SELF_PO_MASTER_DET.FPO_DET_CUSTOMER, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,SELF_PO_MASTER_DET.FPO_DET_COLOR,SELF_PO_MASTER_DET.FPO_DET_STATUS,SELF_PO_MASTER_DET.FPO_DET_EXPDATE,SELF_PO_MASTER_DET.FPOS_DET_ID,SELF_PO_MAST.FPO_DATE FROM YANTRA_ITEM_MAST INNER JOIN SELF_PO_MASTER_DET ON YANTRA_ITEM_MAST.ITEM_CODE = SELF_PO_MASTER_DET.ITEM_CODE INNER JOIN SELF_PO_MAST ON SELF_PO_MASTER_DET.FPOS_ID = SELF_PO_MAST.FPOS_ID INNER JOIN YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID where SELF_PO_MASTER_DET.FPO_DET_STATUS like '" + txtSearch.Text + "%' ");
            }
        }
        else
        {
            General.GridBindwithCommand(gvPOOrdersearch, "SELECT SELF_PO_MAST.FPO_NO, YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_MODEL_NO, SELF_PO_MASTER_DET.FPO_DET_REMARKS, SELF_PO_MASTER_DET.FPO_DET_QTY, SELF_PO_MASTER_DET.FPO_DET_DELIVERY_DATE, SELF_PO_MASTER_DET.FPO_DET_CUSTOMER, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,SELF_PO_MASTER_DET.FPO_DET_COLOR,SELF_PO_MASTER_DET.FPO_DET_STATUS,SELF_PO_MASTER_DET.FPO_DET_EXPDATE,SELF_PO_MASTER_DET.FPOS_DET_ID,SELF_PO_MAST.FPO_DATE FROM YANTRA_ITEM_MAST INNER JOIN SELF_PO_MASTER_DET ON YANTRA_ITEM_MAST.ITEM_CODE = SELF_PO_MASTER_DET.ITEM_CODE INNER JOIN SELF_PO_MAST ON SELF_PO_MASTER_DET.FPOS_ID = SELF_PO_MAST.FPOS_ID INNER JOIN YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID ");
        }
    }
    protected void gvPOOrdersearch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
       gvPOOrdersearch.EditIndex = -1;
        SupplierPO_Fill();
    }
    protected void gvPOOrdersearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[0].Visible = false;
        }

        //if (user == "0")
        //{
        //    if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Cells[0].Visible = true;
        //    }
        //}



    }
    //protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    gvPOOrdersearch.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
    //    gvPOOrdersearch.DataBind();
    //}

}
 
