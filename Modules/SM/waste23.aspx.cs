using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using System.Data.SqlClient;
using vllib;
using System.Data;

public partial class Modules_SM_waste23 : System.Web.UI.Page
{
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    private static string _commandText;
    string custId = "", custStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        tblPopup2.Visible = true;
        dbManager.Open();
        _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_NAME ='" + txtCustomerName.Text + "'");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (dbManager.DataReader.Read())
        {
            custId = dbManager.DataReader["CUST_ID"].ToString();
            custStatus = dbManager.DataReader["CUST_STATUS"].ToString();
        }
        dbManager.DataReader.Close();
        if (custId.Trim()!="")
        {
            LoadCustomerHistory();
        }
    }

    #region btnConfirmNo
    protected void LoadCustomerHistory()
    {
        //tblPopUp1.Visible = false;
        //ModalPopupExtender.Hide();
        //ModalPopupExtender.Controls.Clear();
        //lblData.Text = "";
        //lblData1.Text = "";
        //lblData2.Text = "";
        //lblData3.Text = "";
        //lblData4.Text = "";
        //lblData5.Text = "";
        //ModalPopupExtender1.Controls.Clear();
        GridView1.Visible = false;
        gvQuotationDetails.Visible = false;
        gvSalesEnquiry.Visible = false;
        #region custId
        if (custId != "")
        {

            if (custStatus == "Open" || custStatus == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + custId);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + custId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;
                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + custId);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            //tblpopup3.Visible = true;
            //ModalPopupExtender1.Show();
        }
        #endregion
        
        custId = "";
    }
    #endregion
    
}
 
