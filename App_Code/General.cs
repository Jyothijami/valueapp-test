//Changed Code server
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using YantraDAL;
using System.Data.SqlClient;

using vllib;
using System.Data.SqlClient;

#region Message Box
namespace Yantra.MessageBox
{
    public class MessageBox
    {
        public static void Show(Page page, string message)        
        {
            string msg = "alert('" + message + "');";
            System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", msg, true);

            sticky.Success_Display(message, page);
        }
        public static void Show(UserControl page, string message)
        {
            string msg = "alert('" + message + "');";
            System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "MsgBox", msg, true);
            sticky.Success_Display(message, page.Page);
        }
    }
}
#endregion

namespace Yantra.Classes
{
    public class General
    {
        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
        SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand SqlCmd;
        DataSet ds;
        SqlDataAdapter SqlDap;
        DataTable dt;
        int status;
        string strStatus;
        SqlTransaction trns;
        SqlDataReader sdr;

        public General()
        { }

        private static int _returnIntValue;

        #region Auto Random Number Generator
        public string generatePass()
        {
            Random rdm1 = new Random(unchecked((int)DateTime.Now.Ticks));
            return (rdm1.Next()).ToString();
        }
        #endregion

        #region ClearALL
        public static void ClearControls(Control Parent)
        {
            if (Parent is TextBox)
                (Parent as TextBox).Text = "";
            else if (Parent is DropDownList)
                (Parent as DropDownList).ClearSelection();
            else if (Parent is ListBox)
                (Parent as ListBox).ClearSelection();
            else
                foreach (Control c in Parent.Controls)
                    ClearControls(c);
        }
        #endregion

        #region Number To Words Converter Class
        public class NumberToEnglish
        {

            public String changeNumericToWords(String numb)
            {
                return changeToWords(numb, false);
            }

            public String changeNumericToWords(double numb)
            {
                String num = numb.ToString();
                return changeToWords(num, false);
            }

            public String changeCurrencyToWords(String numb)
            {
                return changeToWords(numb, true);
            }

            public String changeCurrencyToWords(double numb)
            {
                return changeToWords(numb.ToString(), true);
            }

            private String changeToWords(String numb, bool isCurrency)
            {
                String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
                String endStr = (isCurrency) ? ("Only") : ("");
                try
                {
                    int decimalPlace = numb.IndexOf(".");
                    if (decimalPlace > 0)
                    {
                        wholeNo = numb.Substring(0, decimalPlace);
                        points = numb.Substring(decimalPlace + 1);
                        if (Convert.ToInt32(points) > 0)
                        {
                            andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents
                            endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                            pointStr = translateCents(points);
                        }
                    }
                    val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
                }
                catch { ;}
                return val;
            }

            private String translateWholeNumber(String number)
            {
                string word = "";
                try
                {
                    bool beginsZero = false;//tests for 0XX
                    bool isDone = false;//test if already translated
                    double dblAmt = (Convert.ToDouble(number));
                    //if ((dblAmt > 0) && number.StartsWith("0"))
                    if (dblAmt > 0)
                    {//test for zero or digit zero in a nuemric
                        beginsZero = number.StartsWith("0");
                        int numDigits = number.Length;
                        int pos = 0;//store digit grouping
                        String place = "";//digit grouping name:hundres,thousand,etc...
                        switch (numDigits)
                        {
                            case 1://ones' range
                                word = ones(number);
                                isDone = true;
                                break;
                            case 2://tens' range
                                word = tens(number);
                                isDone = true;
                                break;
                            case 3://hundreds' range
                                pos = (numDigits % 3) + 1;
                                place = " Hundred ";
                                break;
                            case 4://thousands' range
                            case 5:
                            case 6:
                                pos = (numDigits % 4) + 1;
                                place = " Thousand ";
                                break;
                            case 7://millions' range
                            case 8:
                            case 9:
                                pos = (numDigits % 7) + 1;
                                place = " Million ";
                                break;
                            case 10://Billions's range
                                pos = (numDigits % 10) + 1;
                                place = " Billion ";
                                break;
                            //add extra case options for anything above Billion...
                            default:
                                isDone = true;
                                break;
                        }
                        if (!isDone)
                        {//if transalation is not done, continue...(Recursion comes in now!!)
                            word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                            //check for trailing zeros
                            if (beginsZero) word = " and " + word.Trim();
                        }
                        //ignore digit grouping names
                        if (word.Trim().Equals(place.Trim())) word = "";
                    }
                }
                catch { ;}
                return word.Trim();
            }

            private String tens(String digit)
            {
                int digt = Convert.ToInt32(digit);
                String name = null;
                switch (digt)
                {
                    case 10:
                        name = "Ten";
                        break;
                    case 11:
                        name = "Eleven";
                        break;
                    case 12:
                        name = "Twelve";
                        break;
                    case 13:
                        name = "Thirteen";
                        break;
                    case 14:
                        name = "Fourteen";
                        break;
                    case 15:
                        name = "Fifteen";
                        break;
                    case 16:
                        name = "Sixteen";
                        break;
                    case 17:
                        name = "Seventeen";
                        break;
                    case 18:
                        name = "Eighteen";
                        break;
                    case 19:
                        name = "Nineteen";
                        break;
                    case 20:
                        name = "Twenty";
                        break;
                    case 30:
                        name = "Thirty";
                        break;
                    case 40:
                        name = "Fourty";
                        break;
                    case 50:
                        name = "Fifty";
                        break;
                    case 60:
                        name = "Sixty";
                        break;
                    case 70:
                        name = "Seventy";
                        break;
                    case 80:
                        name = "Eighty";
                        break;
                    case 90:
                        name = "Ninety";
                        break;
                    default:
                        if (digt > 0)
                        {
                            name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                        }
                        break;
                }
                return name;
            }

            private String ones(String digit)
            {
                int digt = Convert.ToInt32(digit);
                String name = "";
                switch (digt)
                {
                    case 1:
                        name = "One";
                        break;
                    case 2:
                        name = "Two";
                        break;
                    case 3:
                        name = "Three";
                        break;
                    case 4:
                        name = "Four";
                        break;
                    case 5:
                        name = "Five";
                        break;
                    case 6:
                        name = "Six";
                        break;
                    case 7:
                        name = "Seven";
                        break;
                    case 8:
                        name = "Eight";
                        break;
                    case 9:
                        name = "Nine";
                        break;
                }
                return name;
            }

            private String translateCents(String cents)
            {
                String cts = "", digit = "", engOne = "";
                for (int i = 0; i < cents.Length; i++)
                {
                    digit = cents[i].ToString();
                    if (digit.Equals("0"))
                    {
                        engOne = "Zero";
                    }
                    else
                    {
                        engOne = ones(digit);
                    }
                    cts += " " + engOne;
                }
                return cts;
            }

        }
        #endregion

        public static string toDDMMYYYY(string MMDDYYYY)
        {
            string[] date = MMDDYYYY.Split('/');
            return date[1] + "/" + date[0] + "/" + date[2];
        }
        public static string toMMDDYYYY(string DDMMYYYY)
        {
            if (!string.IsNullOrEmpty(DDMMYYYY))
            {
                string[] date = DDMMYYYY.Split('/');
                return date[1] + "/" + date[0] + "/" + date[2];
            }
            else
            {
                return "";
            }
        }
        public static string toyymmdd(string DDMMYYYY)
        {
            if (!string.IsNullOrEmpty(DDMMYYYY))
            {
                string[] date = DDMMYYYY.Split('/');
                return date[2] + "-" + date[1] + "-" + date[0];
            }
            else
            {
                return "";
            }
        }
        static string contenttype,prefixfield;
        public static string GetRequiredPrefix(string TableName)
        {
            switch (TableName)
            {
                case "YANTRA_CUSTOMER_MAST": prefixfield = "PF_CUSTOMERINFO"; break;
                case "YANTRA_ENQ_MAST": prefixfield = "PF_SALESLEAD"; break;
                case "YANTRA_ENQ_ASSIGN_TASKS": prefixfield = "PF_SALESASSIGNMENTS"; break;
                case "YANTRA_QUOT_MAST": prefixfield = "PF_SALESQUOTATION"; break;
                case "YANTRA_SO_MAST": prefixfield = "PF_SALESORDER"; break;
                case "YANTRA_WO_MAST": prefixfield = "PF_ORDERPROFILE"; break;
                case "YANTRA_OA_MAST": prefixfield = "PF_SALESORDERACCEPTANCE"; break;
                case "YANTRA_DELIVERY_CHALLAN_MAST": prefixfield = "PF_DELIVERYCHALLAN"; break;
                case "YANTRA_SALES_INVOICE_MAST": prefixfield = "PF_SALESINVOICE"; break;
                case "YANTRA_SALES_RETURN_MAST": prefixfield = "PF_SALESRETURN"; break;
                case "returnnote_tbl": prefixfield = "PF_SALESRETURN"; break;
                case "YANTRA_CHECKING_FORMAT": prefixfield = "PF_CHECKINGFORMAT"; break;
                case "YANTRA_FIXED_PO_MAST": prefixfield = "PF_PURCHASEORDERDETAILS"; break;
                case "YANTRA_SUP_WO_MAST": prefixfield = "PF_WORKORDERDETAILS"; break;
                case "YANTRA_PURCHASE_INVOICE_MAST": prefixfield = "PF_PURCHASEINVOICE"; break;
                case "YANTRA_SUPPLIER_MAST": prefixfield = "PF_SUPPLIERMASTER"; break;
                case "YANTRA_AGENT_MASTER": prefixfield = "PF_AGENTMASTER"; break;
                case "YANTRA_SDBG_MAST": prefixfield = "PF_SD_BG"; break;
                case "YANTRA_FE_ORDER_PROFILE": prefixfield = "PF_FE_ORDERPROFILE"; break;
                case "YANTRA_CLAIM_FORM": prefixfield = "PF_CLAIMFORM"; break;
                case "YANTRA_PAYMENTS_RECEIVED": prefixfield = "PF_SALESPAYMENTSRECEIVED"; break;
                case "YANTRA_SDBG_RECEIPTS": prefixfield = "PF_SD_BG_RECEIPTS"; break;
                case "YANTRA_EMDS_RECEIVED": prefixfield = "PF_EMDSRECEIVED"; break;
                case "YANTRA_COMPLAINT_REGISTER": prefixfield = "PF_COMPLAINTREGISTER"; break;
                case "YANTRA_COMPLAINT_REGISTER_MAST": prefixfield = "PF_COMPLAINTREGISTER"; break;
                case "YANTRA_SERVICES_ASSIGN_TASKS": prefixfield = "PF_SERVICEASSIGNMENTS"; break;
                case "YANTRA_SERVICE_REPORT_MAST": prefixfield = "PF_SERVICEREPORT"; break;
                case "YANTRA_AMC_QUOTATION_MAST": prefixfield = "PF_AMC_QUOTATION"; break;
                case "YANTRA_AMC_ORDER_MAST": prefixfield = "PF_AMC_ORDER"; break;
                case "YANTRA_AMC_OA_MAST": prefixfield = "PF_AMC_ORDERACCEPTANCE"; break;
                case "YANTRA_AMC_WO_MAST": prefixfield = "PF_AMC_ORDERPROFILE"; break;
                case "YANTRA_WARRANTY_CLAIM": prefixfield = "PF_WARRANTYCLAIM"; break;
                case "YANTRA_SPARES_QUOT_MAST": prefixfield = "PF_SPARESQUOTATION"; break;
                case "YANTRA_SPARES_ORDER_MAST": prefixfield = "PF_SPARESORDER"; break;
                case "YANTRA_SPARES_OP_MAST": prefixfield = "PF_SPARESORDERPROFILE"; break;
                case "YANTRA_SPARES_OA_MAST": prefixfield = "PF_SPARESORDERACCEPTANCE"; break;
                case "YANTRA_AMC_INVOICE_MAST": prefixfield = "PF_AMC_INVOICE"; break;
                case "YANTRA_AMC_PAYMENT_RECEIVED": prefixfield = "PF_AMC_PAYMETSRECEIVED"; break;
                case "YANTRA_EMPLOYEE_MAST": prefixfield = "PF_EMPLOYEEMASTER"; break;
                case "YANTRA_EMP_MEMO": prefixfield = "PF_MEMO"; break;
                case "YANTRA_HR_CIRCULAR": prefixfield = "PF_CIRCULAR"; break;
                case "YANTRA_PURCHASE_RETURN_MAST": prefixfield = "PF_PURCHASERETURN"; break;
                case "YANTRA_SHIPPING_DETAILS_MASTER": prefixfield = "PF_SHIPMENTDETAILS"; break;
                case "YANTRA_OFFER_LETTER": prefixfield = "PF_OFFER_LETTER"; break;
                case "YANTRA_SUP_ENQ_MAST": prefixfield = "PF_SUP_ENQ_MAST"; break;
                case "SELF_PO_MAST": prefixfield = "PF_SELF_PO"; break;
                case "INTERNAL_INDENT": prefixfield = "PF_INT_IND"; break;
                case "StockMovement_Master": prefixfield = "PF_INT_DC"; break;
                case "Convenience_Voucher_tbl": prefixfield = "PF_Voucher_No"; break;
				case "Spare_Outward": prefixfield = "PF_SPR_DC"; break;
                case "Insurance_Form_tbl": prefixfield = "INS_Id"; break;
                case "YANTRA_PI_MAST": prefixfield = "PF_PURCHASEINVOICE"; break;
                case "Amendment_tbl": prefixfield = "PF_AMC_ORDER"; break;
                case "Tour_Expanses": prefixfield = "PF_TourNo"; break;
                case "Yantra_Emp_Reward": prefixfield = "PF_RewardNo"; break;
                case "Asset_Tbl": prefixfield = "PF_AssetNo"; break;
                case "Credit_Approval_tbl": prefixfield = "PF_CRA_NO"; break;
                case "WalkIn_tbl": prefixfield = "PF_WalkIn_NO"; break; 

            }
            return prefixfield;
        }
        public static string GetContentType(string Extension)
        {

            switch (Extension)
            {

                case ".doc":

                    contenttype = "application/vnd.ms-word";

                    break;

                case ".docx":

                    contenttype = "application/vnd.ms-word";

                    break;

                case ".xls":

                    contenttype = "application/vnd.ms-excel";

                    break;

                case ".xlsx":

                    contenttype = "application/vnd.ms-excel";

                    break;

                case ".jpg":

                    contenttype = "image/jpg";

                    break;

                case ".png":

                    contenttype = "image/png";

                    break;

                case ".gif":

                    contenttype = "image/gif";

                    break;

                case ".pdf":

                    contenttype = "application/pdf";

                    break;

            }
            return contenttype;
        }

        #region Count of Records
        public static int CountofRecordsWithQuery(string command)
        {
            dbManager.Open();
            int _returnIntValue;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, command).ToString());

            dbManager.Close();
            return _returnIntValue;
        }
        #endregion

        #region GridBind with Statement
        public static void GridBindwithCommand(GridView gridview, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            gridview.DataSource = dbManager.DataReader;
            gridview.DataBind();
            dbManager.Close();

        }
        #endregion

        public static bool IsRecordExists1(string paraTableName, string paraFieldName, string paraFieldValue)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            bool check = false;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFieldName + "='" + paraFieldValue + "'").ToString());
            if (_returnIntValue > 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }


            dbManager.Close();
            return check;
        }

        public static bool IsRecordExists(string paraTableName, string paraFieldName, string paraFieldValue)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            bool check = false;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFieldName + "='" + paraFieldValue + "'").ToString());
            if (_returnIntValue > 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            dbManager.Close();
            return check;
        }




        public void getCon()
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
        }
        public DataSet ReturnDataSet(string Query)
        {
            try
            {
                //getCon();
                SqlDap = new SqlDataAdapter(Query, SqlCon);
                ds = new DataSet("DSResult");
                SqlDap.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return ErrorDataSet(ex.Message);
            }
            finally
            {
                SqlCon.Close();
                //  SqlCmd.Dispose();
                SqlDap.Dispose();
                ds.Dispose();
            }

        }
        public DataTable ReturnDataTable(string Query)
        {
            try
            {
                // getCon();

                SqlDap = new SqlDataAdapter(Query, SqlCon);
                ds = new DataSet("DSResult");
                SqlDap.Fill(ds);
                dt = new DataTable("DTResult");
                dt = ds.Tables[0];
                return dt;


            }
            catch (Exception ex)
            {
                return ErrorDataTable(ex.Message);
            }
            finally
            {
                SqlCon.Close();
                // SqlCmd.Dispose();
                SqlDap.Dispose();
                ds.Dispose();
            }
        }

        public string ReturnExecuteNoneQuery(string Query)
        {
            try
            {
                getCon();
                SqlCmd = new SqlCommand(Query, SqlCon);
                strStatus = SqlCmd.ExecuteNonQuery().ToString();
                return strStatus;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                SqlCon.Close();
                SqlCmd.Dispose();
            }
        }
        public string ReturnTransExecuteNoneQuery(string[] Query)
        {
            try
            {
                getCon();
                trns = SqlCon.BeginTransaction(IsolationLevel.ReadCommitted);


                // SqlCmd.Connection = SqlCon;
                for (int i = 0; i < Query.Length; i++)
                {
                    if (Query[i] != null)
                    {

                        SqlCmd = new SqlCommand(Query[i], SqlCon, trns);

                        //  SqlCmd.CommandText = Query[i];
                        strStatus = SqlCmd.ExecuteNonQuery().ToString();
                    }
                    else
                    {
                        break;
                    }
                }
                trns.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                trns.Rollback();
                return ex.Message;
            }
            finally
            {
                if (SqlCon != null)
                    SqlCon.Close();
                if (SqlCmd != null)
                    SqlCmd.Dispose();
            }
        }

        public string ReturnExecuteScalar(string Query)
        {
            try
            {
                getCon();
                SqlCmd = new SqlCommand(Query, SqlCon);
                strStatus = SqlCmd.ExecuteScalar().ToString();
                return strStatus;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                SqlCon.Close();
                SqlCmd.Dispose();
            }
        }

        public DataSet ErrorDataSet(string ErrMsg)
        {
            try
            {
                DataTable dt = CreateDataTable();
                DataRow row = dt.NewRow();
                row["ErrMsg"] = ErrMsg;
                dt.Rows.Add(row);
                ds = new DataSet("ERRDS");
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ds.Dispose();
            }
        }
        public DataTable CreateDataTable()
        {
            try
            {
                dt = new DataTable("ERRDT");
                DataColumn myDataColumn = new DataColumn();
                myDataColumn.DataType = Type.GetType("System.String");
                myDataColumn.ColumnName = "ErrMsg";
                dt.Columns.Add(myDataColumn);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
        public DataTable ErrorDataTable(string ErrMsg)
        {
            dt = CreateDataTable();
            DataRow row = dt.NewRow();
            row["ErrMsg"] = ErrMsg;
            dt.Rows.Add(row);
            return dt;

        }

        public string GetColumnVal(string Query, string ColumnName)
        {
            string RetVal = "";
            using (SqlCmd = new SqlCommand(Query, SqlCon))
            {
                SqlCon.Open();
                sdr = SqlCmd.ExecuteReader();
                while (sdr.Read())
                {
                    RetVal = sdr[ColumnName].ToString();
                    break;
                }
                sdr.Close();
                SqlCon.Close();
            }

            return RetVal;
        }



    }
}





