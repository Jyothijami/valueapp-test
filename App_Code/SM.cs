using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraDAL;
using System.Data.SqlClient;
using vllib;

/// <summary>
/// Summary description for SM
/// </summary>

namespace YantraBLL.Modules
{
    public class SM
    {
        private static int _returnIntValue;
        private static string _returnStringMessage, _commandText, _commandText2;
        public enum SMStatus { New = 0, Open = 1, Closed = 2, Cancelled = 3, Regret = 4, Revised = 5, Obsolete = 6, ManuallyClosed=7, CreditApproval_Raised=8}

        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());

        public SM()
        { }

        //Method For Dispose
        public static void Dispose()
        {
            if (dbManager.Connection != null)
                dbManager.Dispose();
        }

        //Method For BeginTransaction
        public static void BeginTransaction()
        {
            dbManager.Open();
            dbManager.BeginTransaction();
        }

        //Method For CommitTransaction
        public static void CommitTransaction()
        {
            dbManager.CommitTransaction();
        }

        //Method For RollBackTransaction
        public static void RollBackTransaction()
        {
            dbManager.RollBackTransaction();
        }

        //Method for Checking a record exists or not with reference id
        private static bool IsRecordExists(string paraTableName, string paraFieldName, string paraFieldValue)
        {
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
            return check;
        }
        private static bool IsRecordExists(string paraTableName, string paraFirstFieldName, string paraFirstFieldValue, string paraSecondFieldName, string paraSecondFieldValue)
        {
            bool check = false;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFirstFieldName + "='" + paraFirstFieldValue + "' and " + paraSecondFieldName + "<>'" + paraSecondFieldValue + "'").ToString());
            if (_returnIntValue > 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }

        //Method for deleting a record with a reference table name and id
        private static bool DeleteRecord(string paraTableName, string paraFieldName, string paraFieldValue)
        {
            bool check = false;
            try
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteNonQuery(CommandType.Text, "DELETE FROM " + paraTableName + " WHERE " + paraFieldName + "='" + paraFieldValue + "'").ToString());
                if (_returnIntValue > 0)
                {
                    check = true;
                }
                else
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException)
                {
                    if ((ex as System.Data.SqlClient.SqlException).Number == 547)
                    {
                        //MessageBox.Show(this, "This Record cannot be deleted. It has been used as reference in other forms.........");
                        check = false;
                    }
                }
            }
            return check;
        }

        //Method for clearing Textbox and Dropdown list and Listbox
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

        //Method for DropDownList Fill
        private static void DropDownListBind(DropDownList ddl, string DataTextField, string DataValueField)
        {
            dbManager.Open();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--", "0"));
            while (dbManager.DataReader.Read())
            {
                ddl.Items.Add(new ListItem(dbManager.DataReader[DataTextField].ToString(), dbManager.DataReader[DataValueField].ToString()));
            }
            dbManager.DataReader.Close();
            //dbManager.Close();
        }

        //Method for Auto Generate Max Serial ID
        public static string AutoGenMaxId(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
            return _returnIntValue.ToString();
        }
        public static string AutoGenMaxId1(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + 1000 + "),0)+1 FROM " + TableName + "").ToString());
            return _returnIntValue.ToString();
        }
        //Method for Auto Generate Max Serial NO
        public static string AutoGenMaxNo(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
        //string hai = Session["vl_cmpid"].ToString();

            _commandText = "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5),CHARINDEX('-'," + FieldName + ")+1,LEN(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5))))),0)+1 FROM " + TableName + " WHERE SUBSTRING(" + FieldName + ",LEN(" + FieldName + ")-4,5)='" + CurrentFinancialYear() + "' and CP_ID = '" + cp.getPresentCompanySessionValue() + "' ";
            string numb = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
            return Prefix(TableName) + "-" + numb + "/" + CurrentFinancialYear();
        }

        public static string Prefix(string TableName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, "SELECT " + Yantra.Classes.General.GetRequiredPrefix(TableName) + " FROM YANTRA_PREFIX").ToString();
            return _returnStringMessage.ToString();
        }

        //Method for to Get Current Financial Year
        public static string CurrentFinancialYear()
        {
            string year;
            if (dbManager.Transaction == null)
                dbManager.Open();
            year = dbManager.ExecuteScalar(CommandType.Text, "SELECT CP_CF_YEAR FROM [YANTRA_COMP_PROFILE] where CP_ID = '" + cp.getPresentCompanySessionValue() + "' ").ToString();
            if (string.IsNullOrEmpty(year))
            {
                year = "0000";
            }
            return year;
        }

        //Method for to Get dropdown Bind
        #region DDL Bind with Select
        public static void DDLBindWithSelect(DropDownList ddlname, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            ddlname.Items.Clear();
            ddlname.Items.Add(new ListItem("--SELECT--", "0"));
            while (dbManager.DataReader.Read())
            {
                ddlname.Items.Add(new ListItem(dbManager.DataReader[1].ToString(), dbManager.DataReader[0].ToString()));
            }
            dbManager.DataReader.Close();          


        }

        public static void DDLBindWithSelect(DropDownList ddlname, string command, string defval)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            ddlname.Items.Clear();
            ddlname.Items.Add(new ListItem("--SELECT--", "0"));
            while (dbManager.DataReader.Read())
            {
                ListItem li = new ListItem();
                li.Value = dbManager.DataReader[0].ToString();
                li.Text = dbManager.DataReader[1].ToString();

                if (li.Value.Equals(defval))
                {
                    li.Selected = true;
                }
                else
                {
                    li.Selected = false;
                }

                ddlname.Items.Add(li);
            }
            dbManager.DataReader.Close();



        }
        #endregion

        #region GridBind with Statement
        public static void GridBindwithCommand(GridView gridview, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            gridview.DataSource = dbManager.DataReader;
            gridview.DataBind();

        }
        #endregion

        #region checkbox list bind
        public static void chklistbind(CheckBoxList ddlname, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            ddlname.Items.Clear();
            while (dbManager.DataReader.Read())
            {
                ddlname.Items.Add(new ListItem(dbManager.DataReader[1].ToString(), dbManager.DataReader[0].ToString()));
            }


        }
        #endregion
      
        #region repeater bind
        public static void RepeaterBindwithCommand(Repeater rptr, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            rptr.DataSource = dbManager.DataReader;
            rptr.DataBind();

        }
        #endregion

        #region DDL Bind with All
        public static void DDLBindAllWithAll(DropDownList ddlname, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            ddlname.Items.Clear();
            ddlname.Items.Add(new ListItem("--All--", "ALL"));
            while (dbManager.DataReader.Read())
            {
                ddlname.Items.Add(new ListItem(dbManager.DataReader[1].ToString(), dbManager.DataReader[0].ToString()));
            }



        }
        #endregion


        

        //Methods For Customer Master Form
        public class CustomerMaster
        {
            public string CustId, CustCode, RegId, RegName, CustName, CompName, ContactPerson, Phone, Mobile, IndTypeId, IndType, Fax, Email, PANNo, ECCNo, CSTNo, LocalSTNo, SplInsrs, Address, Website, CorpContactPerson, CorpPhone, CorpMobile, CorpEmail, CorpAddress, DesgId, CorpDesgId, CorpFax, IsNewOrExisting, CPID;   //Customer Master
            public string CustDetId, CustCorpContactPerson, CustCorpPhone, CustCorpMobile, CustCorpEmail, CustCorpAddress, CustCorpDesgId, CustCorpFax;
            public string CustAnalysisId, CustUnitId, CustUnitName, CustUnitAddress, UnitNo, CustStatus, cUSTdEAR, date, reqid, Others, reqtxt, CatId, Custcatid, CatText, BrandId, Brand_text, LookinfForId, LookinfForText,NickName;
            public string SurveyId, SurveyDt, FindLookingFor, txtLookingFor, txtybph, ExeBehav, ExeBad, ExeKnow, ShowroomLook, VisitAgain, Rating, AnyComments, CustFullName, CustMobile, ExeName, Emailid;
            public CustomerMaster()
            { }

            public static string CustomerMaster_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(CUST_CODE,LEFT(CUST_CODE,5),''))),0)+1 FROM [YANTRA_CUSTOMER_MAST]").ToString());
                //return _codePrefix + _returnIntValue;

                return AutoGenMaxNo("YANTRA_CUSTOMER_MAST", "CUST_CODE");
            }
            public static string WalkIn_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(CUST_CODE,LEFT(CUST_CODE,5),''))),0)+1 FROM [YANTRA_CUSTOMER_MAST]").ToString());
                //return _codePrefix + _returnIntValue;

                return AutoGenMaxNo("WalkIn_tbl", "WALKIN_NO");
            }
            public static void BlockedCustBind(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("select distinct CUST_NAME ,CUST_ID  from BlOCK_New ,YANTRA_WO_MAST ,YANTRA_SO_MAST ,YANTRA_CUSTOMER_MAST where BlOCK_New .SO_Id =YANTRA_WO_MAST .WO_ID and YANTRA_WO_MAST .SO_ID =YANTRA_SO_MAST .SO_ID and YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID ORDER BY YANTRA_CUSTOMER_MAST.CUST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
               
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }
            public static void Exec_CustBind(Control ControlForBind, string empId)
            {
                dbManager.Open();
                _commandText = string.Format("select distinct CUST_NAME,CUST_ID   from YANTRA_SO_MAST ,YANTRA_CUSTOMER_MAST where YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID and SO_RESP_ID=" + empId + " ORDER BY YANTRA_CUSTOMER_MAST.CUST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }
            public static void Exec_BlockedCustBind(Control ControlForBind, string empId)
            {
                dbManager.Open();
                _commandText = string.Format("select distinct CUST_NAME ,CUST_ID  from BlOCK_New ,YANTRA_WO_MAST ,YANTRA_SO_MAST ,YANTRA_CUSTOMER_MAST where BlOCK_New .SO_Id =YANTRA_WO_MAST .WO_ID and YANTRA_WO_MAST .SO_ID =YANTRA_SO_MAST .SO_ID and YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID  and SO_RESP_ID=" + empId + " ORDER BY YANTRA_CUSTOMER_MAST.CUST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }
            public static void CustomerMaster_SelectForCustomer(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CUST_NAME,CUST_ID FROM [YANTRA_CUSTOMER_MAST] where CUST_ID <> '0' ORDER BY YANTRA_CUSTOMER_MAST.CUST_NAME ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }


            public static void InvoiceCustomerMaster_SelectForCustomer(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CUST_NAME,CUST_ID FROM [YANTRA_CUSTOMER_MAST] ORDER BY CUST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }

            public static void DeliveryCustomerMaster_SelectForCustomer(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CUST_NAME,CUST_ID FROM [YANTRA_CUSTOMER_MAST],YANTRA_DELIVERY_CHALLAN_MAST where YANTRA_CUSTOMER_MAST.CUST_ID = YANTRA_DELIVERY_CHALLAN_MAST.DC_CUSTOMER_ID and DC_CUSTOMER_ID <> '0' ORDER BY CUST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }




            public static void CustomerMaster_SelectDdlCustomerNameIndent(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  CUST_ID,CUST_NAME FROM [YANTRA_CUSTOMER_MAST] ORDER BY CUST_NAME desc");
               // _commandText = string.Format("SELECT distinct [YANTRA_CUSTOMER_MAST].CUST_ID,[YANTRA_CUSTOMER_MAST].CUST_NAME FROM [YANTRA_CUSTOMER_MAST],[YANTRA_SO_MAST],[YANTRA_ENQ_MAST],[YANTRA_QUOT_MAST] WHERE [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_SO_MAST].SO_ACCEPTANCE_FLAG='Open' order by [YANTRA_CUSTOMER_MAST].CUST_NAME desc ");//Modification done by Krishna as per who's PO are Open their Names will be displayed in Indent CustomersDropDown 
               // _commandText = string.Format("select YANTRA_CUSTOMER_MAST.CUST_ID, YANTRA_CUSTOMER_MAST.CUST_NAME from [YANTRA_CUSTOMER_MAST],[YANTRA_SO_MAST],[YANTRA_ENQ_MAST],[YANTRA_QUOT_MAST] WHERE [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID and CUST_ID <> '0'  ORDER BY CUST_COMPANY_NAME");
               
                
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }

            //Method For Binding Customers In Services
            public static void Services_CustomerDdlBind(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT Cust_Id,Cust_Name FROM [Service_Customer_Information] where Cust_Id <> '0' ORDER BY Cust_Name asc");

               // _commandText = string.Format("SELECT CUST_ID,CUST_NAME,CUST_NAME+' / '+cast(CUST_ID as nvarchar(50)) AS CUST_IDNAME FROM [YANTRA_CUSTOMER_MAST] where CUST_ID <> '0' ORDER BY CUST_NAME asc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Cust_Name", "Cust_Id");
                }
            }


            public static void CustomerMaster_SelectDdlCustomerName(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CUST_ID,CUST_NAME FROM [YANTRA_CUSTOMER_MAST] where CUST_ID <> '0' ORDER BY CUST_NAME asc");

                // _commandText = string.Format("SELECT CUST_ID,CUST_NAME,CUST_NAME+' / '+cast(CUST_ID as nvarchar(50)) AS CUST_IDNAME FROM [YANTRA_CUSTOMER_MAST] where CUST_ID <> '0' ORDER BY CUST_NAME asc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }
            public static void CustomerMaster_SelectDdlCustomerName2(Control ControlForBind)
            {
                dbManager.Open();

                _commandText = string.Format("SELECT CUST_ID,CUST_NAME   FROM [YANTRA_CUSTOMER_MAST] where CUST_ID <> '0' ORDER BY CUST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }



            public string CustomerMaster_Save()
            {
                this.CustStatus = "New";
                this.CustCode = CustomerMaster_AutoGenCode();
                this.CustId = AutoGenMaxId("[YANTRA_CUSTOMER_MAST]", "CUST_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CUSTOMER_MAST] VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}',{23},{24},'{25}','{26}','{27}',{28},'{29}','{30}')",
               this.CustId, this.CustCode, this.CustName, this.CompName, this.ContactPerson, this.Phone, this.Mobile, this.Fax, this.Email, this.Website, this.PANNo, this.ECCNo, this.CSTNo, this.LocalSTNo, this.RegId, this.IndTypeId, this.Address, this.SplInsrs, this.CorpContactPerson, this.CorpPhone, this.CorpMobile, this.CorpEmail, this.CorpAddress, this.DesgId, this.CorpDesgId, this.CorpFax, this.IsNewOrExisting, this.CustStatus, this.CPID,this.cUSTdEAR,DateTime .Now );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    HttpContext htc = System.Web.HttpContext.Current;
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Customer Master Details", "116");
                    log.add_Insert(this.CustName + " Details are " + " - Inserted by:  " + htc.Session["vl_username"].ToString(), "116");

                }
                return _returnStringMessage;
            }

            public string CustomerMaster_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_CUSTOMER_MAST] SET CUST_NAME='{0}',CUST_COMPANY_NAME='{1}',CUST_CONTACT_PERSON='{2}',CUST_PHONE='{3}',CUST_MOBILE='{4}',CUST_FAX='{5}',CUST_EMAIL='{6}',CUST_WEBSITE='{7}',CUST_PAN='{8}',CUST_ECC='{9}',CUST_CST='{10}',CUST_LOCAL_ST_NO='{11}',REG_ID='{12}',IND_TYPE_ID='{13}',CUST_ADDRESS='{14}',CUST_SPL_INSTRS='{15}',CUST_CORP_CONTACT_PERSON='{16}',CUST_CORP_PHONE='{17}',CUST_CORP_MOBILE='{18}',CUST_CORP_EMAIL='{19}',CUST_CORP_ADDRESS='{20}',CUST_DESG_ID='{21}',CUST_CORP_DESG_ID='{22}',ISNEWOREXISTING='{23}',CUST_DEAR = '{24}',Dt_created='{25}' WHERE CUST_ID={26}", this.CustName, this.CompName, this.ContactPerson, this.Phone, this.Mobile, this.Fax, this.Email, this.Website, this.PANNo, this.ECCNo, this.CSTNo, this.LocalSTNo, this.RegId, this.IndTypeId, this.Address, this.SplInsrs, this.CorpContactPerson, this.CorpPhone, this.CorpMobile, this.CorpEmail, this.CorpAddress, this.DesgId, this.CorpDesgId, this.IsNewOrExisting, this.cUSTdEAR,DateTime .Now, this.CustId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    HttpContext htc = System.Web.HttpContext.Current;

                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update(this.CustName + " Details are " + " - Updated by:  " + htc.Session["vl_username"].ToString(), "116");

                    

                }
                return _returnStringMessage;
            }


            public string UpdateCustomerStatus(string CustId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_CUSTOMER_MAST] SET CUST_STATUS ='Open' where CUST_ID =" + CustId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    HttpContext htc = System.Web.HttpContext.Current;
                    _returnStringMessage = "Data Updated Successfully";

                    log.add_Update(this.CustName + " Details are " + " - Updated by:  " + htc.Session["vl_username"].ToString(), "116");

                    //log.add_Update("Customer Status Details", "116");

                }
                return _returnStringMessage;
            }

            public string CustAnalysys_Delete()
            {
                if (DeleteRecord("Cust_Brand_Analysis_tbl", "Cust_Id", this.CustId) == true)
                {
                    if (DeleteRecord("Cust_Cate_Analysis_tbl", "Cust_Id", this.CustId) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";
                    }
                }
                return _returnStringMessage;
            }
            public string CustomerMaster_Delete()
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_CUSTOMER_DET]", "CUST_ID", this.CustId) == true)
                {
                    if (DeleteRecord("[YANTRA_CUSTOMER_UNITS]", "CUST_ID", this.CustId) == true)
                    {
                        if (DeleteRecord("[YANTRA_CUSTOMER_MAST]", "CUST_ID", this.CustId) == true)
                        {
                            SM.CommitTransaction();
                            _returnStringMessage = "Data Deleted Successfully";
                            //log.add_Delete("Customer Master Details", "116");
                            HttpContext htc = System.Web.HttpContext.Current;
                            log.add_Delete(this.CustName + " Details are " + " - Deleted by:  " + htc.Session["vl_username"].ToString(), "116");


                        }
                        else
                        {
                            SM.RollBackTransaction();
                            _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                        }
                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public static void CustomerMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CUST_NAME,CUST_ID FROM [YANTRA_CUSTOMER_MAST] ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_NAME", "CUST_ID");
                }
            }
            public int Feedback_Select(string KYCID)
            {
                dbManager.Open();
                _commandText = string.Format("select * from Showroom_Survey_tbl where SurveyId='" + KYCID + "' ");
                  dbManager.ExecuteReader(CommandType.Text, _commandText);
                  if (dbManager.DataReader.Read())
                  {
                      this.SurveyId = dbManager.DataReader["SurveyId"].ToString();
                      this.SurveyDt = dbManager.DataReader["Date"].ToString();
                      this.FindLookingFor = dbManager.DataReader["LookingFor"].ToString();
                      this.txtLookingFor = dbManager.DataReader["txtLookingFor"].ToString();
                      this.txtybph = dbManager.DataReader["txtYbtpricehigh"].ToString();
                      this.ExeBehav = dbManager.DataReader["Representive_Behav"].ToString();
                      this.ExeBad = dbManager.DataReader["Bad_Reamrks"].ToString();
                      this.ExeKnow = dbManager.DataReader["Executive_Knowl"].ToString();
                      this.ShowroomLook = dbManager.DataReader["Showroomlook"].ToString();
                      this.VisitAgain = dbManager.DataReader["Visit_Again"].ToString();
                      this.Rating = dbManager.DataReader["Recommend"].ToString();
                      this.CustFullName = dbManager.DataReader["Cust_Name"].ToString();
                      this.AnyComments = dbManager.DataReader["Comments"].ToString();
                      this.Emailid = dbManager.DataReader["Mail"].ToString();
                      this.ExeName = dbManager.DataReader["PreparedBy"].ToString();
                      this.CustMobile = dbManager.DataReader["MobileNo"].ToString();

                      _returnIntValue = 1;
                  }
                  else
                  {
                      _returnIntValue = 0;
                  }
                  dbManager.DataReader.Close();
                  //  //dbManager.Close();
                  return _returnIntValue;
            }
            public int CustomerMaster_Select(string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST],[YANTRA_LKUP_INDUSTRY_TYPE],[location_tbl] WHERE " +
                    //" [YANTRA_CUSTOMER_MAST].CUST_ID=[YANTRA_CUSTOMER_UNITS].CUST_ID AND " +
                                " [YANTRA_CUSTOMER_MAST].IND_TYPE_ID=[YANTRA_LKUP_INDUSTRY_TYPE].IND_TYPE_ID AND " +
                                " [YANTRA_CUSTOMER_MAST].REG_ID=[location_tbl].locid  AND [YANTRA_CUSTOMER_MAST].CUST_ID= " + CustomerId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Address = dbManager.DataReader["CUST_ADDRESS"].ToString();
                    this.CompName = dbManager.DataReader["CUST_COMPANY_NAME"].ToString();
                    this.ContactPerson = dbManager.DataReader["CUST_CONTACT_PERSON"].ToString();
                    this.CSTNo = dbManager.DataReader["CUST_CST"].ToString();
                    this.CustCode = dbManager.DataReader["CUST_CODE"].ToString();
                    this.CustName = dbManager.DataReader["CUST_NAME"].ToString();
                    this.ECCNo = dbManager.DataReader["CUST_ECC"].ToString();
                    this.Email = dbManager.DataReader["CUST_EMAIL"].ToString();
                    this.Fax = dbManager.DataReader["CUST_FAX"].ToString();
                    this.IndTypeId = dbManager.DataReader["IND_TYPE_ID"].ToString();
                    this.IndType = dbManager.DataReader["IND_TYPE"].ToString();
                    this.LocalSTNo = dbManager.DataReader["CUST_LOCAL_ST_NO"].ToString();
                    this.Mobile = dbManager.DataReader["CUST_MOBILE"].ToString();
                    this.PANNo = dbManager.DataReader["CUST_PAN"].ToString();
                    this.Phone = dbManager.DataReader["CUST_PHONE"].ToString();
                    this.RegId = dbManager.DataReader["locid"].ToString();
                    this.RegName = dbManager.DataReader["locname"].ToString();
                    this.SplInsrs = dbManager.DataReader["CUST_SPL_INSTRS"].ToString();
                    this.Website = dbManager.DataReader["CUST_WEBSITE"].ToString();
                    this.CorpContactPerson = dbManager.DataReader["CUST_CORP_CONTACT_PERSON"].ToString();
                    this.CorpPhone = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                    this.CorpMobile = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();
                    this.CorpEmail = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                    this.CorpAddress = dbManager.DataReader["CUST_CORP_ADDRESS"].ToString();
                    this.DesgId = dbManager.DataReader["CUST_DESG_ID"].ToString();
                    this.CorpDesgId = dbManager.DataReader["CUST_CORP_DESG_ID"].ToString();
                    this.CorpFax = dbManager.DataReader["CUST_CORP_FAX"].ToString();
                    this.IsNewOrExisting = dbManager.DataReader["ISNEWOREXISTING"].ToString();
                    this.cUSTdEAR = dbManager.DataReader["CUST_DEAR"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
              //  //dbManager.Close();
                return _returnIntValue;
            }

            public int CustomerMasterUnitsDetailsEnquiry_Select(string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_CUSTOMER_MAST].*,[YANTRA_CUSTOMER_UNITS].*,[YANTRA_CUSTOMER_DET].*,[YANTRA_LKUP_REGION].* " +
                                                                " FROM [YANTRA_CUSTOMER_MAST],[YANTRA_LKUP_INDUSTRY_TYPE],[YANTRA_LKUP_REGION],[YANTRA_CUSTOMER_UNITS],[YANTRA_CUSTOMER_DET],[YANTRA_ENQ_MAST] WHERE " +
                                                                " [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND  [YANTRA_ENQ_MAST].CUST_DET_ID=[YANTRA_CUSTOMER_DET].CUST_DET_ID AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND " +
                                                                " [YANTRA_CUSTOMER_MAST].IND_TYPE_ID=[YANTRA_LKUP_INDUSTRY_TYPE].IND_TYPE_ID AND  [YANTRA_CUSTOMER_MAST].REG_ID=[YANTRA_LKUP_REGION].REG_ID AND " +
                                                                " [YANTRA_CUSTOMER_MAST].CUST_ID= " + CustomerId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CustName = dbManager.DataReader["CUST_NAME"].ToString();
                    this.CompName = dbManager.DataReader["CUST_COMPANY_NAME"].ToString();
                    this.Address = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();
                    this.Email = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                    this.RegName = dbManager.DataReader["REG_NAME"].ToString();
                    this.Phone = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                    this.Mobile = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();
                    this.CustUnitName = dbManager.DataReader["CUST_UNIT_NAME"].ToString();

                    ////this.ContactPerson = dbManager.DataReader["CUST_CONTACT_PERSON"].ToString();
                    ////this.Fax = dbManager.DataReader["CUST_FAX"].ToString();
                    ////this.IndTypeId = dbManager.DataReader["IND_TYPE_ID"].ToString();
                    ////this.IndType = dbManager.DataReader["IND_TYPE"].ToString();
                    ////this.CorpContactPerson = dbManager.DataReader["CUST_CORP_CONTACT_PERSON"].ToString();
                    ////this.CorpPhone = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                    ////this.CorpMobile = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();
                    ////this.CorpEmail = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                    ////this.CorpAddress = dbManager.DataReader["CUST_CORP_ADDRESS"].ToString();
                    ////this.CorpDesgId = dbManager.DataReader["CUST_CORP_DESG_ID"].ToString();
                    ////this.CorpFax = dbManager.DataReader["CUST_CORP_FAX"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            int tMaxEmpId;
            public static void CustomerMasterAllDetails_Select(Control ControlForBind, string CustId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] where CUST_ID=" + CustId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["CUST_CONTACT_PERSON"].ToString(), dbManager.DataReader["CUST_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET] where CUST_ID=" + CustId + " ORDER BY CUST_CORP_CONTACT_PERSON");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["CUST_CORP_CONTACT_PERSON"].ToString(), dbManager.DataReader["CUST_DET_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
            }

            public string Analysis_Cat_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT MAX(CUST_ID) from YANTRA_CUSTOMER_MAST");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
                    //TermTitle = dbManager.DataReader[1].ToString();
                    //Others =0;
                }
                dbManager.DataReader.Close();
                _commandText = string.Format("update Cust_Cate_Analysis_tbl Set Category_Id={0},Category_Text='{1}' where Cust_Catd_Id={2} and Cust_Id={3} ",this.CatId ,this.CatText ,this.Custcatid,this.CustId );
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Employee Document Details", "51");

                }
                return _returnStringMessage;
            }
            public string DR_Analysis_Brand_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT MAX(DAILYREPORTID) from YANTRA_DAILY_REPORT");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
                    //TermTitle = dbManager.DataReader[1].ToString();
                    //Others =0;
                }
                dbManager.DataReader.Close();
                _commandText = string.Format("INSERT INTO DR_Brand_Analysis_tbl SELECT ISNULL(MAX(DR_analysis_id),0)+1,{0},'{1}','{2}','{3}','{4}','{5}' FROM DR_Brand_Analysis_tbl", CustId, BrandId, Brand_text, date, CatId, CatText);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Employee Document Details", "51");

                }
                return _returnStringMessage;
            }
            public string DR_Analysis_Cat_Save()
            {
                //if (dbManager.Transaction == null)
                dbManager.Open();
                //_commandText = string.Format("SELECT MAX(DAILYREPORTID) from YANTRA_DAILY_REPORT");
                //dbManager.ExecuteReader(CommandType.Text, _commandText);
                //while (dbManager.DataReader.Read())
                //{
                //    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
                //    //TermTitle = dbManager.DataReader[1].ToString();
                //    //Others =0;
                //}
                //dbManager.DataReader.Close();
                _commandText = string.Format("INSERT INTO DR_Cate_Analysis_tbl SELECT ISNULL(MAX(DR_Catd_Id),0)+1,{0},'{1}','{2}','{3}' FROM DR_Cate_Analysis_tbl", CustId, CatId, CatText, date);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Employee Document Details", "51");

                }
                return _returnStringMessage;
            }
            public string Analysis_Brand_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT MAX(CUST_ID) from YANTRA_CUSTOMER_MAST");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
                    //TermTitle = dbManager.DataReader[1].ToString();
                    //Others =0;
                }
                dbManager.DataReader.Close();
                _commandText = string.Format("INSERT INTO Cust_Brand_Analysis_tbl SELECT ISNULL(MAX(Brand_analysis_id),0)+1,{0},'{1}','{2}','{3}','{4}','{5}' FROM Cust_Brand_Analysis_tbl", CustId, BrandId, Brand_text,date,CatId ,CatText  );
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Employee Document Details", "51");

                }
                return _returnStringMessage;
            }
            public string Analysis_Cat_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT MAX(CUST_ID) from YANTRA_CUSTOMER_MAST");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
                    //TermTitle = dbManager.DataReader[1].ToString();
                    //Others =0;
                }
                dbManager.DataReader.Close();
                _commandText = string.Format("INSERT INTO Cust_Cate_Analysis_tbl SELECT ISNULL(MAX(Cust_Catd_Id),0)+1,{0},'{1}','{2}','{3}' FROM Cust_Cate_Analysis_tbl", CustId , CatId, CatText,date );
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Employee Document Details", "51");

                }
                return _returnStringMessage;
            }
            //public string Analysis_Save()
            //{
            //    if (this.CustAnalysisId == "delete")
            //    {
            //        _commandText = string.Format("DELETE FROM [YANTRA_CUST_ANALYSIS] WHERE Cust_Analysis_id='{0}'", this.CustAnalysisId);
            //    }
            //    else if (this.CustDetId == "-")//Do Not Remove This Condition ... This Is Reference For Checking New Record Or Not
            //    {
            //        _commandText = string.Format("INSERT INTO [YANTRA_CUST_ANALYSIS] SELECT ISNULL(MAX(Cust_Analysis_id),0)+1,{0},'{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}' FROM [YANTRA_CUST_ANALYSIS]",
            //        this.CustId, this.date, this.reqid, this.reqtxt, this.CatId, this.CatText, this.BrandId, this.Brand_text, this.Others);
            //    }
            //    else
            //    {
            //        _commandText = string.Format("UPDATE [YANTRA_CUST_ANALYSIS] SET Cust_ID='{0}',Date='{1}',Requirement='{2}',Req_text='{3}',Category_Id='{4}',Category_text={5},Brand_Id='{6}',Brand_text='{7}',Others='{8}'  WHERE Cust_Analysis_Id={9}",
            //      this.CustId, this.date, this.reqid, this.reqtxt, this.CatId, this.CatText, this.BrandId, this.Brand_text, this.Others, this.CustAnalysisId);
            //    }

            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    _returnStringMessage = string.Empty;
            //    if (_returnIntValue < 0 || _returnIntValue == 0)
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }
            //    else if (_returnIntValue > 0)
            //    {
            //        HttpContext htc = System.Web.HttpContext.Current;

            //        _returnStringMessage = "Data Saved Successfully";

            //        //log.add_Insert("Customer Master Details - [" + this.CustId + "] " + this.CustName + " - Edited by: [" + htc.Session["vl_userid"].ToString() + "] " + htc.Session["vl_username"].ToString(), "116");


            //    }

            //    return _returnStringMessage;
            //}
            public string CustomerMasterDetails_Save()
            {
                if (this.CustUnitId == "delete")
                {
                    _commandText = string.Format("DELETE FROM [YANTRA_CUSTOMER_DET] WHERE CUST_DET_ID='{0}'", this.CustDetId);
                }
                else if (this.CustDetId == "-")//Do Not Remove This Condition ... This Is Reference For Checking New Record Or Not
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_CUSTOMER_DET] SELECT ISNULL(MAX(CUST_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},'{9}' FROM [YANTRA_CUSTOMER_DET]",
                                                                                                     this.CustId, this.CustCorpContactPerson, this.CustCorpPhone, this.CustCorpMobile, this.CustCorpEmail, this.CustCorpAddress, this.CustCorpDesgId, this.CustCorpFax, this.CustUnitId,this.ExeName );
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_CUSTOMER_DET] SET CUST_CORP_CONTACT_PERSON='{0}',CUST_CORP_PHONE='{1}',CUST_CORP_MOBILE='{2}',CUST_CORP_EMAIL='{3}',CUST_CORP_ADDRESS='{4}',CUST_CORP_DESG_ID={5},CUST_CORP_FAX='{6}',CUST_UNIT_ID={7},Prepared_By='{8}'  WHERE CUST_DET_ID={9}",
                                                                                                      this.CustCorpContactPerson, this.CustCorpPhone, this.CustCorpMobile, this.CustCorpEmail, this.CustCorpAddress, this.CustCorpDesgId, this.CustCorpFax, this.CustUnitId,this.ExeName , this.CustDetId);
                }

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    HttpContext htc = System.Web.HttpContext.Current;
                    
                    _returnStringMessage = "Data Saved Successfully";

                    log.add_Insert("Customer Master Details - [" + this.CustId + "] " + this.CustName + " - Edited by: [" + htc.Session["vl_userid"].ToString() + "] " + htc.Session["vl_username"].ToString(), "116");
                    

                }
                return _returnStringMessage;
            }

            public int CustomerMasterDetails_Delete(string CustomerId)
            {
                if (DeleteRecord("[YANTRA_CUSTOMER_DET]", "CUST_ID", CustomerId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }

            public void CustomerMasterDetails_Select(string CustomerId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET],[YANTRA_DESG_MAST],[YANTRA_CUSTOMER_UNITS] WHERE [YANTRA_CUSTOMER_DET].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND [YANTRA_CUSTOMER_DET].CUST_CORP_DESG_ID=[YANTRA_DESG_MAST].DESG_ID AND [YANTRA_CUSTOMER_DET].CUST_ID=" + CustomerId + " AND [YANTRA_CUSTOMER_DET].CUST_UNIT_ID<>0");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable CustomerProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("UnitName");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("ContactPerson");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Designation");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Address");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("ContactNo1");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("ContactNo2");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("FaxNo");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Email");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("DesignationId");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("CustUnitId");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("CustDetId");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Prepared_By");
                CustomerProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = CustomerProducts.NewRow();

                    dr["CustDetId"] = dbManager.DataReader["CUST_DET_ID"].ToString();
                    dr["CustUnitId"] = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    dr["UnitName"] = dbManager.DataReader["CUST_UNIT_NAME"].ToString();
                    dr["ContactPerson"] = dbManager.DataReader["CUST_CORP_CONTACT_PERSON"].ToString();
                    dr["DesignationId"] = dbManager.DataReader["CUST_CORP_DESG_ID"].ToString();
                    dr["Designation"] = dbManager.DataReader["DESG_NAME"].ToString();
                    dr["Address"] = dbManager.DataReader["CUST_CORP_ADDRESS"].ToString();
                    dr["ContactNo1"] = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                    dr["ContactNo2"] = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();
                    dr["FaxNo"] = dbManager.DataReader["CUST_CORP_FAX"].ToString();
                    dr["Email"] = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                    dr["Prepared_By"] = dbManager.DataReader["Prepared_By"].ToString();
                    CustomerProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = CustomerProducts;
                gv.DataBind();
            }

            public void CustomerMasterDetails_Select(string CustomerId, string CustUnitId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (CustUnitId == "0")
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET],[YANTRA_DESG_MAST] WHERE [YANTRA_CUSTOMER_DET].CUST_CORP_DESG_ID=[YANTRA_DESG_MAST].DESG_ID AND [YANTRA_CUSTOMER_DET].CUST_ID=" + CustomerId + " AND [YANTRA_CUSTOMER_DET].CUST_UNIT_ID=" + CustUnitId + "");
                }
                else
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET],[YANTRA_DESG_MAST],[YANTRA_CUSTOMER_UNITS] WHERE [YANTRA_CUSTOMER_DET].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND [YANTRA_CUSTOMER_DET].CUST_CORP_DESG_ID=[YANTRA_DESG_MAST].DESG_ID AND [YANTRA_CUSTOMER_DET].CUST_ID=" + CustomerId + " AND [YANTRA_CUSTOMER_DET].CUST_UNIT_ID=" + CustUnitId + "");
                }
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable CustomerProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("UnitName");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("ContactPerson");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Designation");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Address");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("ContactNo1");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("ContactNo2");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("FaxNo");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("Email");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("DesignationId");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("CustUnitId");
                CustomerProducts.Columns.Add(col);
                col = new DataColumn("CustDetId");
                CustomerProducts.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = CustomerProducts.NewRow();

                    dr["CustDetId"] = dbManager.DataReader["CUST_DET_ID"].ToString();
                    dr["CustUnitId"] = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    if (CustUnitId != "0") { dr["UnitName"] = dbManager.DataReader["CUST_UNIT_NAME"].ToString(); }
                    dr["ContactPerson"] = dbManager.DataReader["CUST_CORP_CONTACT_PERSON"].ToString();
                    dr["DesignationId"] = dbManager.DataReader["CUST_CORP_DESG_ID"].ToString();
                    dr["Designation"] = dbManager.DataReader["DESG_NAME"].ToString();
                    dr["Address"] = dbManager.DataReader["CUST_CORP_ADDRESS"].ToString();
                    dr["ContactNo1"] = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                    dr["ContactNo2"] = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();
                    dr["FaxNo"] = dbManager.DataReader["CUST_CORP_FAX"].ToString();
                    dr["Email"] = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                    CustomerProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = CustomerProducts;
                gv.DataBind();
            }

            public static void CustomerMasterDetails_Select(Control ControlForBind, string CustUnitId)
            {
                dbManager.Open();
                if (CustUnitId == "0")
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET] where CUST_DET_ID<>0 ORDER BY CUST_CORP_CONTACT_PERSON");
                }
                else
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET] where CUST_UNIT_ID=" + CustUnitId + " ORDER BY CUST_CORP_CONTACT_PERSON");
                }
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_CORP_CONTACT_PERSON", "CUST_DET_ID");
                }
            }

            public static void Service_Cust(Control ControlForBind, string CustUnitId)
            {
                dbManager.Open();
                if (CustUnitId == "0")
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET] where CUST_DET_ID<>0 ORDER BY CUST_CORP_CONTACT_PERSON");
                }
                else
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET] where CUST_UNIT_ID=" + CustUnitId + " ORDER BY CUST_CORP_CONTACT_PERSON");
                }
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_CORP_CONTACT_PERSON", "CUST_DET_ID");
                }
            }

            public int CustomerMasterDetails_Select(string CustDetId)
            {
                if (CustDetId == "0") { _returnIntValue = 0; }
                else
                {
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_DET]  WHERE CUST_DET_ID='" + CustDetId + "' ORDER BY CUST_DET_ID DESC ");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                        this.CustCorpEmail = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                        this.CustCorpPhone = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                        this.CustCorpMobile = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();

                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
                }
                return _returnIntValue;
            }

            public string CustomerUnits_Save()
            {
                if (this.CustUnitName == "delete")
                {
                    _commandText = string.Format("DELETE FROM [YANTRA_CUSTOMER_UNITS] WHERE CUST_UNIT_ID='{0}'", this.CustUnitId);
                }
                else if (this.CustUnitId.Substring(0, 1) != "t")//Do Not Remove This Condition ... This Is Reference For Checking Neew Recoed Or Not
                {
                    _commandText = string.Format("UPDATE [YANTRA_CUSTOMER_UNITS] SET CUST_UNIT_NAME='{0}',CUST_UNIT_ADDRESS='{1}',Prepared_BY='{2}',NickName='{3}' WHERE CUST_UNIT_ID='{4}'", this.CustUnitName, this.CustUnitAddress,this.ExeName ,this.NickName , this.CustUnitId);
                }
                else
                {
                    this.CustUnitId = AutoGenMaxId("[YANTRA_CUSTOMER_UNITS]", "CUST_UNIT_ID");
                    _commandText = string.Format("INSERT INTO [YANTRA_CUSTOMER_UNITS] VALUES({0},'{1}','{2}','{3}','{4}','{5}')", this.CustUnitId, this.CustId, this.CustUnitName, this.CustUnitAddress,this.ExeName,this.NickName  );
                }
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Customer Unit Details", "96");
                    HttpContext htc = System.Web.HttpContext.Current;
                    log.add_Insert(this.CustName + " Unit Details are " + " - Inserted by:  " + htc.Session["vl_username"].ToString(), "96");


                }
                return _returnStringMessage;
            }

            public string CustomerUnits_Delete(string CustomerId)
            {
                if (DeleteRecord("[YANTRA_CUSTOMER_UNITS]", "CUST_ID", CustomerId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    //log.add_Delete("Customer Unit Details", "96");
                    HttpContext htc = System.Web.HttpContext.Current;
                    log.add_Insert(this.CustName + " Unit Details are " + " - Inserted by:  " + htc.Session["vl_username"].ToString(), "96");


                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public static void CustomerNickName_Select(Control ControlForBind, string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_UNITS] where CUST_ID=" + CustomerId + " ORDER BY NickName");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "NickName", "CUST_UNIT_ID");
                }
            }
            public static void CustomerNickName_SelectCredit(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_UNITS] ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_UNIT_NAME", "CUST_UNIT_ID");
                }
            }
            public static void CustomerUnits_Select(Control ControlForBind, string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_UNITS] where CUST_ID=" + CustomerId + " ORDER BY CUST_UNIT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_UNIT_NAME", "CUST_UNIT_ID");
                }
            }


            public static void ServiceUnitName_Bind(Control ControlForBind, string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [Service_Customer_Unit_Details] where Cust_Id=" + CustomerId + " ORDER BY Cust_Unit_Name");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Cust_Unit_Name", "Cust_Unit_Id");
                }
            }
            public DataTable Analysis_Select1(int CustId)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("Brand_Id");
                //dcol = new DataColumn("Brand_Id");
                dtable.Columns.Add(dcol);

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM Cust_Brand_Analysis_tbl WHERE  Cust_ID={0}", CustId);
                //_commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS,YANTRA_LKUP_COLOR_MAST WHERE  YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE={0}", ItemCode);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    //drow["cust_analysis_Id"] = dbManager.DataReader[0].ToString();
                    //drow["Cust_ID"] = dbManager.DataReader[1].ToString();
                    //drow["Date"] = dbManager.DataReader[2].ToString();
                    //drow["Requirement"] = dbManager.DataReader[3].ToString();
                    //drow["Req_text"] = dbManager.DataReader[4].ToString();
                    drow["Brand_Id"] = dbManager.DataReader[2].ToString();
                    //drow["Category_text"] = dbManager.DataReader[6].ToString();
                    //drow["Brand_Id"] = dbManager.DataReader[7].ToString();
                    //drow["Brand_text"] = dbManager.DataReader[8].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return dtable;

            }
            public DataTable Analysis_Select(int CustId)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("ITEM_CATEGORY_ID");
                //dcol = new DataColumn("Brand_Id");
                dtable.Columns.Add(dcol);

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM cust_cate_analysis_tbl WHERE  Cust_ID={0}", CustId);
                //_commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS,YANTRA_LKUP_COLOR_MAST WHERE  YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE={0}", ItemCode);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    //drow["cust_analysis_Id"] = dbManager.DataReader[0].ToString();
                    //drow["Cust_ID"] = dbManager.DataReader[1].ToString();
                    //drow["Date"] = dbManager.DataReader[2].ToString();
                    //drow["Requirement"] = dbManager.DataReader[3].ToString();
                    //drow["Req_text"] = dbManager.DataReader[4].ToString();
                    drow["ITEM_CATEGORY_ID"] = dbManager.DataReader[2].ToString();
                    //drow["Category_text"] = dbManager.DataReader[6].ToString();
                    //drow["Brand_Id"] = dbManager.DataReader[7].ToString();
                    //drow["Brand_text"] = dbManager.DataReader[8].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return dtable;

            }

            public void CustomerUnitDetails_Select(string CustomerId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_UNITS] WHERE CUST_ID = " + CustomerId + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable CustomerUnits = new DataTable();
                DataColumn col = new DataColumn();
                //col = new DataColumn("unitno");
                //CustomerUnits.Columns.Add(col);
                col = new DataColumn("unitname");
                CustomerUnits.Columns.Add(col);
                col = new DataColumn("unitaddress");
                CustomerUnits.Columns.Add(col);
                col = new DataColumn("custunitid");
                CustomerUnits.Columns.Add(col);
                col = new DataColumn("Prepared_By");
                CustomerUnits.Columns.Add(col);
                col = new DataColumn("NickName");
                CustomerUnits.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = CustomerUnits.NewRow();

                    //dr["unitno"] = dbManager.DataReader["UNIT_NO"].ToString();
                    dr["unitname"] = dbManager.DataReader["CUST_UNIT_NAME"].ToString();
                    dr["unitaddress"] = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();
                    dr["custunitid"] = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    dr["Prepared_By"] = dbManager.DataReader["Prepared_By"].ToString();
                    dr["NickName"] = dbManager.DataReader["NickName"].ToString();

                    CustomerUnits.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = CustomerUnits;
                gv.DataBind();
            }

            

            public int CustomerUnits_Select(string CustUnitId)
            {
                if (CustUnitId == "0") { _returnIntValue = 0; }
                else
                {

                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_UNITS]  WHERE CUST_UNIT_ID='" + CustUnitId + "' ORDER BY CUST_UNIT_ID DESC ");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                        this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                        this.CustUnitName = dbManager.DataReader["CUST_UNIT_NAME"].ToString();
                        this.CustUnitAddress = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();

                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
                }
                return _returnIntValue;
            }

            public static void TenderCustomerMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE [YANTRA_CUSTOMER_MAST].CUST_ID IN( SELECT [YANTRA_ENQ_MAST].CUST_ID FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' AND [YANTRA_ENQ_MAST].ENQ_ID IN (SELECT ENQ_ID FROM [YANTRA_QUOT_MAST])) ORDER BY [YANTRA_CUSTOMER_MAST].CUST_COMPANY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_COMPANY_NAME", "CUST_ID");
                }
            }

            public static void TenderCustomerUnits_Select(Control ControlForBind, string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_UNITS] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_UNITS].CUST_ID AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND [YANTRA_CUSTOMER_UNITS].CUST_ID=" + CustomerId + " AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' AND [YANTRA_ENQ_MAST].ENQ_ID IN (SELECT ENQ_ID FROM [YANTRA_QUOT_MAST])ORDER BY [YANTRA_CUSTOMER_UNITS].CUST_UNIT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_UNIT_NAME", "CUST_UNIT_ID");
                }
            }

            public int unit_Select(string unit)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS] where [YANTRA_CUSTOMER_UNITS].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID='" + unit + "' ORDER BY [YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitName = dbManager.DataReader["CUST_UNIT_NAME"].ToString();
                    this.CustUnitAddress = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();



                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

        }

        //Methods For Sales Enquiry Form
        public class SalesEnquiry
        {
            public string EnqId, EnqNo, EnqDate, CustId, CustName, EnqModeId, EnqModeName, EnqOrigBy, EnqOrigName, EnqRef, EnqFollowUp, EnqDeliveryDate, DespModeId, PromotionType, PromotionActivity, EnqPriority, EnqStatus, EnqDueDate, EnqDesc, CustUnitId, CustDetId, EnqSubTime, EnqOpeningDate, EnqOpeningTime, EnqDocCharges, EnqDocFavourof, EnqEMDCharges, EnqEMDFavourof, EnqTenderDate, EnqContact, EnqPreparedBy, EnqApprovedBy,CpId,ENQPRIORITY;
            public string EnqDetItemCode, EnqDetQty, EnqDetSpec, EnqDetRemarks, EnqDetPriority, EnqDetRoom,EnqColor,colorname,modelno,brand,itemname,uom;

            public SalesEnquiry()
            {
            }

            public static string SalesEnquiry_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "ENQ/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(ENQ_NO,0,LEN(ENQ_NO)-5),CHARINDEX('-',ENQ_NO)+1,LEN(SUBSTRING(ENQ_NO,0,LEN(ENQ_NO)-5))))),0)+1 FROM YANTRA_ENQ_MAST WHERE SUBSTRING(ENQ_NO,LEN(ENQ_NO)-4,5)='09-10'").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(ENQ_NO,LEFT(ENQ_NO,5),''))),0)+1 FROM [YANTRA_ENQ_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ENQ_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_ENQ_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_ENQ_MAST", "ENQ_NO");

            }

            public string SalesEnquiry_Save()
            {
                this.EnqNo = SalesEnquiry_AutoGenCode();
                this.EnqId = AutoGenMaxId("[YANTRA_ENQ_MAST]", "ENQ_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ENQ_MAST] VALUES({0},'{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','','','','','{24}','{25}',{26},{27},{28},'{29}')", this.EnqId, this.EnqNo, this.EnqDate, this.CustId, this.EnqModeId, this.EnqOrigBy, this.EnqOrigName, this.EnqRef, this.EnqFollowUp, this.EnqDeliveryDate, this.PromotionType, this.PromotionActivity, this.EnqStatus, this.EnqDueDate, this.EnqDesc, this.CustUnitId, this.CustDetId, this.EnqSubTime, this.EnqOpeningDate, this.EnqOpeningTime, this.EnqDocCharges, this.EnqDocFavourof, this.EnqEMDCharges, this.EnqEMDFavourof, this.EnqTenderDate, this.EnqContact, this.EnqPreparedBy, this.EnqApprovedBy,this.CpId,this.ENQPRIORITY);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Enquiry Details", "117");

                }
                return _returnStringMessage;
            }

            public string SalesEnquiry_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ENQ_MAST] SET ENQ_DATE='{0}',CUST_ID={1},ENQM_ID={2},ENQ_ORIG_BY='{3}',ENQ_ORIG_NAME='{4}',ENQ_REFERENCE='{5}',ENQ_FOLLOWUP_CITERIA='{6}',ENQ_DELIVERY_DATE='{7}',PROMOTION_TYPE='{8}',PROMOTION_ACTIVITY='{9}',ENQ_DUE_DATE='{11}',ENQ_DESC='{12}',CUST_UNIT_ID={13},CUST_DET_ID={14},ENQ_SUB_TIME='{15}',ENQ_OPENING_DATE='{16}',ENQ_OPENING_TIME='{17}',ENQ_DOC_CHARGES='',ENQ_DOC_INFAVOUROF='',ENQ_EMD_CHARGES='',ENQ_EMD_INFAVOUROF='',ENQ_TENDER_DATE='{22}',ENQ_REF_CONTACT='{23}',CP_ID={25},ENQ_PRIORITY = '{26}' WHERE ENQ_ID='{24}'", this.EnqDate, this.CustId, this.EnqModeId, this.EnqOrigBy, this.EnqOrigName, this.EnqRef, this.EnqFollowUp, this.EnqDeliveryDate, this.PromotionType, this.PromotionActivity, this.EnqStatus, this.EnqDueDate, this.EnqDesc, this.CustUnitId, this.CustDetId, this.EnqSubTime, this.EnqOpeningDate, this.EnqOpeningTime, this.EnqDocCharges, this.EnqDocFavourof, this.EnqEMDCharges, this.EnqEMDFavourof, this.EnqTenderDate, this.EnqContact, this.EnqId, this.CpId,this.ENQPRIORITY);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Sales Enquiry Details", "117");

                }
                return _returnStringMessage;
            }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="Del Dc"></param>
          /// <returns></returns>

            public string SampleDc_Delete(string EnquiryId)
            {
                //SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_DELIVERY_CHALLAN_DET]", "DC_ID", EnquiryId) == true)
                {
                    if (DeleteRecord("[YANTRA_DELIVERY_CHALLAN_MAST]", "DC_ID", EnquiryId) == true)
                    {
                        if (true)
                        {
                            // SM.CommitTransaction();
                            _returnStringMessage = "Data Deleted Successfully";
                            log.add_Delete("Sample DC Details", "118");

                        }
                        //if (DeleteRecord("[YANTRA_DELIVERY_CHALLAN_MAST]", "DC_ID", EnquiryId) == true)
                        //{
                        //    SM.CommitTransaction();
                        //    _returnStringMessage = "Data Deleted Successfully";
                        //}
                        else
                        {
                            //   SM.RollBackTransaction();
                            _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                        }
                    }
                    else
                    {
                        //SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                return _returnStringMessage;
            }



            /// <summary>
            /// ///
            /// </summary>
            /// <param name="EnquiryId"></param>
            /// <returns></returns>

            public string SalesEnquiryDet_Delete(string EnquiryDETId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_ENQ_DET]", "ENQ_DET_ID", EnquiryDETId) == true)
                {
                    //if (DeleteRecord("[YANTRA_ENQ_MAST]", "ENQ_ID", EnquiryId) == true)
                    //{
                    SM.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Sales Enquiry Details", "117");

                    //}
                    //else
                    //{
                    //    SM.RollBackTransaction();
                    //    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    //}
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public string SalesEnquiry_Delete(string EnquiryId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_ENQ_DET]", "ENQ_ID", EnquiryId) == true)
                {
                    if (DeleteRecord("[YANTRA_ENQ_MAST]", "ENQ_ID", EnquiryId) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Sales Enquiry Details", "117");

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public string SalesEnquiryDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ENQ_DET] SELECT ISNULL(MAX(ENQ_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}' FROM [YANTRA_ENQ_DET]", this.EnqId, this.EnqDetItemCode, this.EnqDetQty, this.EnqDetSpec, this.EnqDetRemarks, this.EnqDetPriority, this.EnqDocCharges, this.EnqDocFavourof, this.EnqEMDCharges, this.EnqEMDFavourof, this.EnqDetRoom, this.EnqColor,this.modelno,this.colorname,this.brand,this.itemname,this.uom);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Enquiry Details", "117");

                }
                return _returnStringMessage;
            }
            public string Convinence_Form_Delete(string ID)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[Convenience_Voucher_Det_tbl]", "Id", ID) == true)
                {
                    if (DeleteRecord("[Convenience_Voucher_tbl]", "Id", ID) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }
            public int SalesEnquiryDetails_Delete(string EnquiryId)
            {
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_commandText = string.Format("DELETE FROM [YANTRA_ENQ_DET] WHERE ENQ_ID={0}", EnquiryId);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                if (DeleteRecord("[YANTRA_ENQ_DET]", "ENQ_ID", EnquiryId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }

            public int SalesEnquiry_Select(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND " +
                                            "[YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_ENQ_MAST].ENQ_ID='" + EnquiryId + "' ORDER BY [YANTRA_ENQ_MAST].ENQ_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.EnqNo = dbManager.DataReader["ENQ_NO"].ToString();
                    this.EnqDate = Convert.ToDateTime(dbManager.DataReader["ENQ_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.EnqModeId = dbManager.DataReader["ENQM_ID"].ToString();
                    this.EnqModeName = dbManager.DataReader["ENQM_NAME"].ToString();
                    this.EnqOrigBy = dbManager.DataReader["ENQ_ORIG_BY"].ToString();
                    this.EnqOrigName = dbManager.DataReader["ENQ_ORIG_NAME"].ToString();
                    this.EnqRef = dbManager.DataReader["ENQ_REFERENCE"].ToString();
                    this.EnqFollowUp = dbManager.DataReader["ENQ_FOLLOWUP_CITERIA"].ToString();
                    this.EnqDeliveryDate = Convert.ToDateTime(dbManager.DataReader["ENQ_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PromotionType = dbManager.DataReader["PROMOTION_TYPE"].ToString();
                    this.PromotionActivity = dbManager.DataReader["PROMOTION_ACTIVITY"].ToString();
                    this.EnqStatus = dbManager.DataReader["ENQ_STATUS"].ToString();
                    this.EnqDueDate = Convert.ToDateTime(dbManager.DataReader["ENQ_DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.EnqDesc = dbManager.DataReader["ENQ_DESC"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.EnqSubTime = dbManager.DataReader["ENQ_SUB_TIME"].ToString();
                    this.EnqOpeningDate = Convert.ToDateTime(dbManager.DataReader["ENQ_OPENING_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.EnqOpeningTime = dbManager.DataReader["ENQ_OPENING_TIME"].ToString();
                    //this.EnqDocCharges = dbManager.DataReader["ENQ_DOC_CHARGES"].ToString();
                    //this.EnqDocFavourof = dbManager.DataReader["ENQ_DOC_INFAVOUROF"].ToString();
                    this.EnqEMDCharges = dbManager.DataReader["ENQ_EMD_CHARGES"].ToString();
                    //this.EnqEMDFavourof = dbManager.DataReader["ENQ_EMD_INFAVOUROF"].ToString();
                    this.EnqTenderDate = dbManager.DataReader["ENQ_TENDER_DATE"].ToString();
                    this.EnqContact = dbManager.DataReader["ENQ_REF_CONTACT"].ToString();
                    this.EnqPreparedBy = dbManager.DataReader["ENQ_PREPARED_BY"].ToString();
                    this.EnqApprovedBy = dbManager.DataReader["ENQ_APPROVED_BY"].ToString();
                    this.ENQPRIORITY = dbManager.DataReader["ENQ_PRIORITY"].ToString();
                    if (this.EnqSubTime == "1/1/1900 12:00:00 AM") { this.EnqSubTime = ""; } else { this.EnqSubTime = Convert.ToDateTime(this.EnqSubTime).ToShortTimeString(); }
                    if (this.EnqOpeningTime == "1/1/1900 12:00:00 AM") { this.EnqOpeningTime = ""; } else { this.EnqOpeningTime = Convert.ToDateTime(this.EnqOpeningTime).ToShortTimeString(); }
                    if (this.EnqTenderDate == "1/1/1900 12:00:00 AM") { this.EnqTenderDate = ""; } else { this.EnqTenderDate = Convert.ToDateTime(this.EnqTenderDate).ToString("dd/MM/yyyy"); }
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string AutoCountUnleads(string cpid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "select count(distinct(CUST_ID)) from YANTRA_CUSTOMER_MAST where YANTRA_CUSTOMER_MAST.CP_ID=" + cpid;
                string numb1 = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
                _commandText = "select count(distinct(CUST_ID)) from YANTRA_ENQ_MAST where YANTRA_ENQ_MAST.CP_ID=" + cpid;
                string numb2 = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
                
                string numb = Convert.ToString(int.Parse(numb1) - int.Parse(numb2));
                
                return numb;
            }

            public static string SalesEnquiryStatus_Update(SMStatus Status, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ENQ_MAST] SET  ENQ_STATUS='{0}' WHERE ENQ_ID={1}", Status, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Enquiry Status Details", "117");

                }
                return _returnStringMessage;
            }

            public static string SalesEnquiryStatusQua_Update(string Status, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //Status = "Open";
                _commandText = string.Format("UPDATE [YANTRA_ENQ_MAST] SET  ENQ_STATUS='{0}' WHERE ENQ_ID={1}", Status, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Enquiry Status Quotation Details", "117");
                }
                return _returnStringMessage;
            }


            public void SalesEnquiryDetails_Select_SelfQuot(string EnquiryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_PRODUCT_COMPANY],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST,YANTRA_ITEM_PRICE WHERE [YANTRA_ENQ_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID =YANTRA_ENQ_DET.COLOR_ID and  [YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID=[YANTRA_ITEM_MAST].BRAND_ID and YANTRA_ENQ_DET .ITEM_CODE =YANTRA_ITEM_PRICE .Item_Code and  [YANTRA_ENQ_DET].ENQ_ID='" + EnquiryId + "' order by YANTRA_ENQ_DET.ENQ_DET_ID");

                //_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_DET] WHERE  [YANTRA_ENQ_DET].ENQ_ID=" + EnquiryId + "");


                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable EnquiryInterestedProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                EnquiryInterestedProducts.Columns.Add(col);

                col = new DataColumn("DocCharges");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("DocInFavourOf");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EMDCharges");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EMDInFavourOf");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Room");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Color");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ENQ_DET_ID");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("GrossAmount");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("GST");
                EnquiryInterestedProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = EnquiryInterestedProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["Model_No"].ToString();
                    dr["Brand"] = dbManager.DataReader["Brand"].ToString();

                    dr["ItemName"] = dbManager.DataReader["ItemName"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM"].ToString();
                    dr["Quantity"] = dbManager.DataReader["ENQ_DET_QTY"].ToString();
                    dr["Specifications"] = dbManager.DataReader["ENQ_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["ENQ_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["ENQ_DET_PRIORITY"].ToString();
                    //dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    //dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["DocCharges"] = dbManager.DataReader["ENQ_DOC_CHARGES"].ToString();
                    dr["DocInFavourOf"] = dbManager.DataReader["ENQ_DOC_INFAVOUROF"].ToString();
                    dr["EMDCharges"] = dbManager.DataReader["ENQ_EMD_CHARGES"].ToString();
                    dr["EMDInFavourOf"] = dbManager.DataReader["ENQ_EMD_INFAVOUROF"].ToString();
                    dr["Room"] = dbManager.DataReader["ENQ_DET_ROOM"].ToString();

                    dr["Color"] = dbManager.DataReader["colorname"].ToString();

                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();

                    dr["ENQ_DET_ID"] = dbManager.DataReader["ENQ_DET_ID"].ToString();
                    dr["GrossAmount"] = dbManager.DataReader["GrossAmount"].ToString();
                    dr["GST"] = dbManager.DataReader["GST Tax"].ToString();


                    EnquiryInterestedProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = EnquiryInterestedProducts;
                gv.DataBind();
            }

            public void SalesEnquiryDetails_Select(string EnquiryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_PRODUCT_COMPANY],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_ENQ_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID =YANTRA_ENQ_DET.COLOR_ID and  [YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID=[YANTRA_ITEM_MAST].BRAND_ID and  [YANTRA_ENQ_DET].ENQ_ID='" + EnquiryId + "' order by YANTRA_ENQ_DET.ENQ_DET_ID");

                //_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_DET] WHERE  [YANTRA_ENQ_DET].ENQ_ID=" + EnquiryId + "");

                
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                    
                DataTable EnquiryInterestedProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                EnquiryInterestedProducts.Columns.Add(col);

                col = new DataColumn("DocCharges");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("DocInFavourOf");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EMDCharges");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EMDInFavourOf");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Room");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Color");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ENQ_DET_ID");
                EnquiryInterestedProducts.Columns.Add(col);
               

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = EnquiryInterestedProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["Model_No"].ToString();
                    dr["Brand"] = dbManager.DataReader["Brand"].ToString();

                    dr["ItemName"] = dbManager.DataReader["ItemName"].ToString();
                   dr["UOM"] = dbManager.DataReader["UOM"].ToString();
                    dr["Quantity"] = dbManager.DataReader["ENQ_DET_QTY"].ToString();
                    dr["Specifications"] = dbManager.DataReader["ENQ_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["ENQ_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["ENQ_DET_PRIORITY"].ToString();
                    //dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    //dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["DocCharges"] = dbManager.DataReader["ENQ_DOC_CHARGES"].ToString();
                    dr["DocInFavourOf"] = dbManager.DataReader["ENQ_DOC_INFAVOUROF"].ToString();
                    dr["EMDCharges"] = dbManager.DataReader["ENQ_EMD_CHARGES"].ToString();
                    dr["EMDInFavourOf"] = dbManager.DataReader["ENQ_EMD_INFAVOUROF"].ToString();
                    dr["Room"] = dbManager.DataReader["ENQ_DET_ROOM"].ToString();

                    dr["Color"] = dbManager.DataReader["colorname"].ToString();

                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["ENQ_DET_ID"] = dbManager.DataReader["ENQ_DET_ID"].ToString();
                   


                    EnquiryInterestedProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = EnquiryInterestedProducts;
                gv.DataBind();
            }

            public static void SalesEnquiry_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST] ORDER BY ENQ_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ENQ_NO", "ENQ_ID");
                }
            }

            public static void SalesEnquiry_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_ENQ_MAST].* FROM [YANTRA_ENQ_MAST] inner join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID	where [YANTRA_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ENQ_NO", "ENQ_ID");
                }
            }

            public static void SalesEnquiryItemTypes_Select(string EnquiryId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_ENQ_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_ENQ_DET].ENQ_ID=" + EnquiryId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void SalesEnquiryItemTypes1_Select(string EnquiryId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST]  WHERE [YANTRA_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) ANd    [YANTRA_ENQ_DET].ENQ_ID=" + EnquiryId + " order by YANTRA_ITEM_MAST.ITEM_MODEL_NO asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }
            public static void SalesEnquiryItemTypes123_Select(string EnquiryId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_SO_DET],[YANTRA_ITEM_MAST]  WHERE [YANTRA_SO_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) ANd    [YANTRA_SO_DET].SO_ID=" + EnquiryId + " order by YANTRA_ITEM_MAST.ITEM_MODEL_NO asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }
            public static void SalesEnquiryItemTypes2_Select(string ModelNo, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_ITEM_MAST].ITEM_MODEL_NO,[YANTRA_ITEM_MAST].ITEM_CODE FROM [YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST]  WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE  = [YANTRA_ITEM_MAST].ITEM_CODE  and [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=" + ModelNo + " order by YANTRA_ITEM_ESSENTIALS.Item_Code asc  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }

            //public static void SalesEnquiryItemType3_select(Control ControlForBind)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    //_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST] ORDER BY ENQ_ID");
            //    _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "ENQ_REFERENCE", "ENQ_ID");
            //    }
            //}
            public static void SalesEnquiryItemNames_Select(string EnquiryId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ENQ_DET].ENQ_ID=" + EnquiryId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }


            public static void SalesEnquiryTenderNo_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST] ORDER BY ENQ_ID");
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ENQ_REFERENCE", "ENQ_ID");
                }
            }


            public static void SalesEnquiryTenderNo_Select(Control ControlForBind, string CustId, string CustUnitId, string SaveButtonText)
            {
                //////////if (dbManager.Transaction == null)
                //////////    dbManager.Open();
                ////////////_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST] ORDER BY ENQ_ID");
                //////////_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                //////////dbManager.ExecuteReader(CommandType.Text, _commandText);
                //////////if (ControlForBind is DropDownList)
                //////////{
                //////////    DropDownListBind(ControlForBind as DropDownList, "ENQ_REFERENCE", "ENQ_ID");
                //////////}
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST] WHERE [YANTRA_ENQ_MAST].CUST_ID=" + CustId + " AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + CustUnitId + " AND [YANTRA_ENQ_MAST].ENQ_ID IN 	(SELECT ENQ_ID FROM YANTRA_EMDS_RECEIVED WHERE EMDR_STATUS <> 'Cleared') AND [YANTRA_ENQ_MAST].ENQ_ID IN 	(SELECT ENQ_ID FROM [YANTRA_QUOT_MAST]) ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["ENQ_REFERENCE"].ToString(), dbManager.DataReader["ENQ_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST] WHERE [YANTRA_ENQ_MAST].CUST_ID=" + CustId + " AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + CustUnitId + " AND [YANTRA_ENQ_MAST].ENQ_ID NOT IN 	(SELECT ENQ_ID FROM YANTRA_EMDS_RECEIVED) AND [YANTRA_ENQ_MAST].ENQ_ID IN 	(SELECT ENQ_ID FROM [YANTRA_QUOT_MAST]) ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["ENQ_REFERENCE"].ToString(), dbManager.DataReader["ENQ_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE] WHERE [YANTRA_ENQ_MAST].ENQ_ID IN (SELECT ENQ_ID FROM [YANTRA_QUOT_MAST]) AND [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + CustUnitId + " ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "ENQ_REFERENCE", "ENQ_ID");
                    }
                }
            }

            public static void SalesOrderByTender_Select(string EnquiryId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();


                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST] WHERE  [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_ENQ_MAST].ENQ_ID=" + EnquiryId + " ORDER BY [YANTRA_ENQ_MAST].ENQ_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }


            public int EnqDetails_Select(string EnqId,string ICode)
            {
                dbManager.Open();
                //  _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM] WHERE [YANTRA_ENQ_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //"[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ENQ_DET].ITEM_CODE=" + EnqDetItemCode + "");

                _commandText = string.Format("SELECT [YANTRA_ENQ_DET].ENQ_ID,[YANTRA_ENQ_DET].ENQ_DET_ROOM ,[YANTRA_ENQ_DET].ENQ_DET_QTY FROM [YANTRA_ENQ_DET]  where  [YANTRA_ENQ_DET].ENQ_DET_ID=" + ICode + "and [YANTRA_ENQ_DET].ENQ_ID=" + EnqId + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.EnqDetRoom = dbManager.DataReader["ENQ_DET_ROOM"].ToString();
                    this.EnqDetQty = dbManager.DataReader["ENQ_DET_QTY"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }


            public string EnqApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [YANTRA_ENQ_MAST] SET ENQ_APPROVED_BY={0} WHERE ENQ_ID='{1}'", this.EnqApprovedBy, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Enquiry Approve Details", "117");

                }
                return _returnStringMessage;
            }



        }

        //Methods for Sales Quotation Form
        public class SalesQuotation
        {
            public string QuotId, QuotNo, QuotDate, EnqId, EnqNo, EnqDate, CustId, QuotDelivery, QuotPayTerms, QuotPackCharges, QuotExcise, QuotCST, QuotVAT, DespmId, QuotGuarantee, QuotTransCharges, QuotInsurance, QuotErrec, QuotJurisdiction, QuotValidity, QuotInspection, QuotOtherSpecs, QuotPOLog, QuotRespId, QuotSalespId, QuotPreparedBy, QuotCheckedBy, QuotApprovedBy, AssignTaskId, CurrencyId, RevisedKey, QuotDDNo, QuotDDDate, QuotBankName, QuotTotalEMDCharges, CustUnitId, CustDetId, QuotFOB, QuotCIF, QuotCompany, QuotIncluding, QuotType;
            public string QuotDetItemCode, QuotDetItemName, QuotDetQty, QuotRate, QuotDetSpec, QuotDetRemarks, QuotDetPriority, QuotDetDisc, QuotDetSpPrice, QuotRoom, QuotCurrency, quotUom;
            public string QuotFollowUpDetId, ModelNo, CurrencyName, FollowUpEmpId, FollowUpDesc, FollowUpDate, FollowUpTechDiss, FollowUpCommNegos, FollowUpCompExistance, FollowUpRemarks, FollowUpExpDate,Cp_Id,ColorId;
            public bool IsExpectedOrder;
            public string QuotSubItemCode, QuotItemCodeMain, ItemSpec;
            public SalesQuotation()
            {
            }

            public static string SalesQuotation_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "QUOT/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(QUOT_NO,LEFT(QUOT_NO,5),''))),0)+1 FROM [YANTRA_QUOT_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(QUOT_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_QUOT_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_QUOT_MAST", "QUOT_NO");
            }
            public string ttlDisc;
            public string SalesQuotation_Save()
            {
                this.RevisedKey = "";
                this.QuotNo = SalesQuotation_AutoGenCode();
                this.QuotId = AutoGenMaxId("[YANTRA_QUOT_MAST]", "QUOT_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_QUOT_MAST] SELECT ISNULL(MAX(QUOT_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19},'{20}','{21}','{22}',{23},'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}',{33},'{34}','{35}',{36},'{37}' FROM [YANTRA_QUOT_MAST]", this.QuotNo, this.QuotDate, this.EnqId, this.QuotDelivery, this.QuotPayTerms, this.QuotPackCharges, this.QuotExcise, this.QuotCST, this.DespmId, this.QuotGuarantee, this.QuotTransCharges, this.QuotInsurance, this.QuotErrec, this.QuotJurisdiction, this.QuotValidity, this.QuotInspection, this.QuotOtherSpecs, "New", this.QuotRespId, this.QuotSalespId, this.QuotPreparedBy, this.QuotCheckedBy, this.QuotApprovedBy, this.CurrencyId, this.RevisedKey, this.QuotVAT, this.QuotDDNo, this.QuotDDDate, this.QuotBankName, this.IsExpectedOrder, this.QuotTotalEMDCharges, this.QuotFOB, this.QuotCIF, this.QuotCompany, this.QuotIncluding,this.QuotType,this.Cp_Id,this.ttlDisc );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _commandText = string.Format("insert into [YANTRA_QUOT_APPROVERS] select isnull(max(Quotation_approve_id),0)+1,'{0}',{1},{2} from YANTRA_QUOT_APPROVERS", "New", this.QuotApprovedBy, this.QuotId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);



                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Quotation Details", "119");

                }
                return _returnStringMessage;
            }

            public string SalesQuotationRevise_Save()
            {
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(QUOT_REVISED_KEY,'R','')),0)+1 FROM YANTRA_QUOT_MAST WHERE QUOT_ID=" + this.QuotId + "").ToString());
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(QUOT_REVISED_KEY,'R','')),0)+1 FROM YANTRA_QUOT_MAST WHERE QUOT_NO LIKE '" + this.QuotNo + "%'").ToString());
                this.RevisedKey = "R" + _returnIntValue.ToString();

                    SalesQuotationStatus_Update(SMStatus.Revised, this.QuotId);

                this.QuotId = AutoGenMaxId("[YANTRA_QUOT_MAST]", "QUOT_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_QUOT_MAST] SELECT ISNULL(MAX(QUOT_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19},'{20}','{21}','{22}',{23},'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}',{33},'{34}','{35}',{36},'{37}' FROM [YANTRA_QUOT_MAST]", this.QuotNo, this.QuotDate, this.EnqId, this.QuotDelivery, this.QuotPayTerms, this.QuotPackCharges, this.QuotExcise, this.QuotCST, this.DespmId, this.QuotGuarantee, this.QuotTransCharges, this.QuotInsurance, this.QuotErrec, this.QuotJurisdiction, this.QuotValidity, this.QuotInspection, this.QuotOtherSpecs, "New", this.QuotRespId, this.QuotSalespId, this.QuotPreparedBy, this.QuotCheckedBy, this.QuotApprovedBy, this.CurrencyId, this.RevisedKey, this.QuotVAT, this.QuotDDNo, this.QuotDDDate, this.QuotBankName, this.IsExpectedOrder, this.QuotTotalEMDCharges, this.QuotFOB, this.QuotCIF, this.QuotCompany, this.QuotIncluding, this.QuotType, this.Cp_Id,this.ttlDisc);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _commandText = string.Format("insert into [YANTRA_QUOT_APPROVERS] select isnull(max(Quotation_approve_id),0)+1,'{0}',{1},{2} from YANTRA_QUOT_APPROVERS", "New", this.QuotApprovedBy, this.QuotId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);


                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Quotation Revised Details", "119");

                }
                return _returnStringMessage;
            }

            public string SalesQuotation_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_QUOT_MAST] SET QUOT_DATE='{0}',QUOT_DELIVERY='{1}',QUOT_PAY_TERM='{2}',QUOT_PACK_CHARGES='{3}',QUOT_EXCISE='{4}',QUOT_CST='{5}',DESPM_ID='{6}',QUOT_GUARANTEE='{7}',QUOT_TRANS_CHARGES='{8}',QUOT_INSURANCE='{9}',QUOT_EREC_COMM='{10}',QUOT_JURISDICTION='{11}',QUOT_VALIDITY='{12}',QUOT_INSPECTION='{13}',QUOT_OTHER_SPEC='{14}',QUOT_RESP_ID='{15}',QUOT_SALESP_ID='{16}',QUOT_PREPARED_BY='{17}',QUOT_CHECKED_BY='{18}',QUOT_APPROVED_BY='{19}',CURRENCY_ID={20},QUOT_VAT='{21}',QUOT_DD_NO='{22}',QUOT_DD_DATE='{23}',QUOT_BANK_NAME='{24}',IS_EXPECTED_ORDER='{25}',QUOT_EMD_CHARGES='{26}',QUOT_FOB='{27}',QUOT_CIF='{28}',QUOT_COMPANY = {29},QUOT_INCLUDING ='{30}',QUOT_TYPE = '{31}',CP_ID={33},SPL_DISCOUNT='{34}'  WHERE QUOT_ID={32}", this.QuotDate, this.QuotDelivery, this.QuotPayTerms, this.QuotPackCharges, this.QuotExcise, this.QuotCST, this.DespmId, this.QuotGuarantee, this.QuotTransCharges, this.QuotInsurance, this.QuotErrec, this.QuotJurisdiction, this.QuotValidity, this.QuotInspection, this.QuotOtherSpecs, this.QuotRespId, this.QuotSalespId, this.QuotPreparedBy, this.QuotCheckedBy, this.QuotApprovedBy, this.CurrencyId, this.QuotVAT, this.QuotDDNo, this.QuotDDDate, this.QuotBankName, this.IsExpectedOrder, this.QuotTotalEMDCharges, this.QuotFOB, this.QuotCIF, this.QuotCompany, this.QuotIncluding, this.QuotType, this.QuotId, this.Cp_Id,this.ttlDisc);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Sales Quotation Details", "119");

                }
                return _returnStringMessage;
            }

            public string SalesQuotation_Delete(string QuotationId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_QUOT_FOLLOWUP_DET]", "QUOT_ID", QuotationId) == true)
                {
                    if (DeleteRecord("[YANTRA_QUOT_DET]", "QUOT_ID", QuotationId) == true)
                    {
                        if (DeleteRecord("[YANTRA_QUOT_MAST]", "QUOT_ID", QuotationId) == true)
                        {
                            SM.CommitTransaction();
                            _returnStringMessage = "Data Deleted Successfully";
                            log.add_Delete("Sales Quotation Details", "119");

                        }
                        else
                        {
                            SM.RollBackTransaction();
                            _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                        }
                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }
            public string OptionalId, Remarks, Itemtype, Floor, SrlOrder, QuotGSTperc, QuotGSTRate;
            public string SalesQuotationDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_QUOT_DET] SELECT ISNULL(MAX(QUOT_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},{9},'{10}','{11}','{12}',{13},{14},{15} FROM [YANTRA_QUOT_DET]", this.QuotId, this.QuotDetItemCode, this.QuotDetQty, this.QuotRate, this.QuotDetDisc, this.QuotDetSpPrice, this.QuotRoom, this.QuotCurrency, this.ColorId, this.OptionalId, this.Remarks, this.Itemtype, this.Floor, this.SrlOrder,this.QuotGSTperc, this.QuotGSTRate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Quotation Details", "119");

                }
                return _returnStringMessage;
            }
            public string SalesOptQuotationDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_QUOT_OPT_DET] SELECT ISNULL(MAX(QUOT_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},{9},'{10}','{11}','{12}',{13} FROM [YANTRA_QUOT_OPT_DET]", this.QuotId, this.QuotDetItemCode, this.QuotDetQty, this.QuotRate, this.QuotDetDisc, this.QuotDetSpPrice, this.QuotRoom, this.QuotCurrency, this.ColorId, this.OptionalId, this.Remarks, this.Itemtype, this.Floor, this.SrlOrder);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Quotation Details", "119");

                }
                return _returnStringMessage;
            }
            public string SalesQuotationDetailsSubItems_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_QUOT_ITEM_DETAILS] SELECT ISNULL(MAX(QUOTITEMDETID),0)+1,{0},{1},{2} FROM [YANTRA_QUOT_ITEM_DETAILS]", this.QuotId, this.QuotItemCodeMain, this.QuotSubItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Quotation Details SubItems", "119");

                }
                return _returnStringMessage;
            }

            public int SalesQuotationDetails_Delete(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_QUOT_DET] WHERE QUOT_ID={0}", QuotationId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int SalesQuotationDetails_DeleteSubItems(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_QUOT_ITEM_DETAILS] WHERE QUOTID={0}", QuotationId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }


            public decimal SalesQuotationDetails_SubItemsRate(string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT YANTRA_ITEM_PRICE.Item_Price,ITEM_SPEC FROM [YANTRA_ITEM_PRICE],[YANTRA_ITEM_MAST] WHERE YANTRA_ITEM_PRICE.Item_Code=YANTRA_ITEM_MAST.ITEM_CODE AND YANTRA_ITEM_PRICE.Item_Code={0}", ItemCode);
                  dbManager.ExecuteReader (CommandType.Text, _commandText);
                  if (dbManager.DataReader.Read())
                  {
                      this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
                      return Convert.ToDecimal(dbManager.DataReader["Item_Price"].ToString());
                  }
                      
                  else
                {
                return 0;
                }
                dbManager.DataReader.Close();
            }

            public static int IsSalesQuotationRaised(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT COUNT(*) FROM [YANTRA_QUOT_MAST] WHERE ENQ_ID=" + EnquiryId + "";
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                return _returnIntValue;
            }

            public static int GetSalesQuotationId(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT QUOT_ID FROM [YANTRA_QUOT_MAST] WHERE ENQ_ID=" + EnquiryId + " ORDER BY QUOT_ID DESC";
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                return _returnIntValue;
            }
            public int SalesQuotation_Select1(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("sELECT * FROM [YANTRA_QUOT_MAST] where [YANTRA_QUOT_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_ID DESC");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.QuotCST = dbManager.DataReader["QUOT_CST"].ToString();
                    this.QuotVAT = dbManager.DataReader["QUOT_VAT"].ToString();
                    this.QuotIncluding = dbManager.DataReader["QUOT_INCLUDING"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public string QuotRespoId;
            public int SalesQuotation_Select(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_COMP_PROFILE]  WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND" +
                //                            " [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID and YANTRA_QUOT_MAST.QUOT_COMPANY = YANTRA_COMP_PROFILE.CP_ID       AND [YANTRA_QUOT_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_ID DESC ");


                _commandText = string.Format("sELECT * FROM [YANTRA_QUOT_MAST]	"+
									"inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID "+			
									"inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID "+
									"inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID "+
									"LEFT OUTER JOIN  dbo.YANTRA_QUOT_APPROVERS ON YANTRA_QUOT_APPROVERS.QUOT_ID = YANTRA_QUOT_MAST.QUOT_ID "+
                                    "where [YANTRA_QUOT_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_ID DESC ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.QuotRespoId = dbManager.DataReader["QUOT_RESP_ID"].ToString();

                    this.QuotNo = dbManager.DataReader["QUOT_NO"].ToString();
                    this.QuotDate = Convert.ToDateTime(dbManager.DataReader["QUOT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.EnqNo = dbManager.DataReader["ENQ_NO"].ToString();
                    this.Cp_Id = dbManager.DataReader["CP_ID"].ToString();                    
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.Cp_Id = dbManager.DataReader["CP_ID"].ToString();
                    this.QuotDelivery = dbManager.DataReader["QUOT_DELIVERY"].ToString();
                    this.QuotPayTerms = dbManager.DataReader["QUOT_PAY_TERM"].ToString();
                    this.QuotPackCharges = dbManager.DataReader["QUOT_PACK_CHARGES"].ToString();
                    this.QuotExcise = dbManager.DataReader["QUOT_EXCISE"].ToString();
                    this.QuotCST = dbManager.DataReader["QUOT_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.QuotGuarantee = dbManager.DataReader["QUOT_GUARANTEE"].ToString();
                    this.QuotTransCharges = dbManager.DataReader["QUOT_TRANS_CHARGES"].ToString();
                    this.QuotInsurance = dbManager.DataReader["QUOT_INSURANCE"].ToString();
                    this.QuotErrec = dbManager.DataReader["QUOT_EREC_COMM"].ToString();
                    this.QuotJurisdiction = dbManager.DataReader["QUOT_JURISDICTION"].ToString();
                    this.QuotValidity = dbManager.DataReader["QUOT_VALIDITY"].ToString();
                    this.QuotInspection = dbManager.DataReader["QUOT_INSPECTION"].ToString();
                    this.QuotOtherSpecs = dbManager.DataReader["QUOT_OTHER_SPEC"].ToString();
                    this.QuotPOLog = dbManager.DataReader["QUOT_PO_FLAG"].ToString();
                    this.QuotRespId = dbManager.DataReader["QUOT_RESP_ID"].ToString();
                    this.QuotSalespId = dbManager.DataReader["QUOT_SALESP_ID"].ToString();
                    this.QuotPreparedBy = dbManager.DataReader["QUOT_PREPARED_BY"].ToString();
                    this.QuotCheckedBy = dbManager.DataReader["QUOT_CHECKED_BY"].ToString();
                    this.QuotApprovedBy = dbManager.DataReader["Quatation_Approved"].ToString();
                    this.CurrencyId = dbManager.DataReader["CURRENCY_ID"].ToString();
                    this.QuotVAT = dbManager.DataReader["QUOT_VAT"].ToString();
                    this.QuotDDNo = dbManager.DataReader["QUOT_DD_NO"].ToString();
                    this.QuotDDDate = Convert.ToDateTime(dbManager.DataReader["QUOT_DD_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.QuotBankName = dbManager.DataReader["QUOT_BANK_NAME"].ToString();
                    this.QuotTotalEMDCharges = dbManager.DataReader["QUOT_EMD_CHARGES"].ToString();
                    this.QuotFOB = dbManager.DataReader["QUOT_FOB"].ToString();
                    this.QuotCIF = dbManager.DataReader["QUOT_CIF"].ToString();
                    this.IsExpectedOrder = bool.Parse(dbManager.DataReader["IS_EXPECTED_ORDER"].ToString());
                    this.QuotCompany = dbManager.DataReader["QUOT_COMPANY"].ToString();
                    this.QuotIncluding = dbManager.DataReader["QUOT_INCLUDING"].ToString();
                    this.QuotType = dbManager.DataReader["QUOT_TYPE"].ToString();
                    this.ttlDisc = dbManager.DataReader["SPL_DISCOUNT"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public string SalesQuotationFollowUp_Save()
            {
                this.QuotFollowUpDetId = AutoGenMaxId("[YANTRA_QUOT_FOLLOWUP_DET]", "QUOT_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_QUOT_FOLLOWUP_DET] VALUES({0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}')", this.QuotFollowUpDetId, this.QuotId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate, this.FollowUpTechDiss, this.FollowUpCommNegos, this.FollowUpCompExistance, this.FollowUpRemarks, this.FollowUpExpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Quotation Followup Details", "119");

                }
                return _returnStringMessage;
            }

            public string SalesQuotationApprove_Update()
            {
                //if (dbManager.Transaction == null)
                  dbManager.Open();
                //_commandText = string.Format("SELECT  QUOT_PO_FLAG FROM [YANTRA_QUOT_MAST] WHERE QUOT_ID={0}", this.QuotId);
                //if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                //{
                //    _returnIntValue = 1;
                //}
                //else
                //{
                // dbManager.Dispose();
                //    string Open = "Open";
                //    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ToString());
                //    SqlCommand cmd = new SqlCommand();
                //    cmd.Connection = con;
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Clear();
                //    cmd.Parameters.AddWithValue("@ApprovedBy", this.QuotApprovedBy);
                //    cmd.Parameters.AddWithValue("@Flag", Open);
                //    cmd.Parameters.AddWithValue("@QuotId",  QuotId);
                //    cmd.CommandText = "SP_SM_QUOTATION_UPDATE_APPROVE";
                //    cmd.CommandTimeout = 50;
                //    con.Open();
                   
                //    _returnIntValue = cmd.ExecuteNonQuery();
                //    con.Close();

                 _commandText = string.Format("UPDATE [YANTRA_QUOT_MAST] SET QUOT_APPROVERS={0} WHERE QUOT_ID={1}", this.QuotApprovedBy, this.QuotId);
                 _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                 //}


               //_commandText = string.Format("UPDATE [YANTRA_QUOT_MAST] SET QUOT_APPROVED_BY={0},QUOT_PO_FLAG='{1}' WHERE QUOT_ID={2}", this.QuotApprovedBy, "Open", QuotId);
               //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
  

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                }
                return _returnStringMessage;
                dbManager.Dispose();
            }

            public string SalesQuotationRegret_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  QUOT_PO_FLAG FROM [YANTRA_QUOT_MAST] WHERE QUOT_ID='{0}'", this.QuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                    //if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    string Absolute = "Obsolete";
                     SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SP_SM_QUOTATION_UPDATE_APPROVE";
                    cmd.Parameters.AddWithValue("@ApprovedBy", this.QuotApprovedBy);
                    cmd.Parameters.AddWithValue("@Flag", Absolute);
                    cmd.Parameters.AddWithValue("@QuotId", QuotId);
                    con.Open();
                    _returnIntValue = cmd.ExecuteNonQuery();
                    con.Close();
                    //_commandText = string.Format("UPDATE [YANTRA_QUOT_MAST] SET QUOT_APPROVED_BY={0},QUOT_PO_FLAG='{1}' WHERE QUOT_ID='{2}'", this.QuotApprovedBy, SMStatus.Absolute, QuotId);
                    //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Quotation Regret Details", "119");

                }
                return _returnStringMessage;
            }

            public static string SalesQuotationStatus_Update(SMStatus Status, string QuotId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  QUOT_PO_FLAG FROM [YANTRA_QUOT_MAST] WHERE QUOT_ID='{0}'", QuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_QUOT_MAST] SET QUOT_PO_FLAG='{0}' WHERE QUOT_ID='{1}'", Status, QuotId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Quotation Status Details", "119");

                }
                return _returnStringMessage;
            }

            public void SalesQuotationDetails_SelectPreview(string QuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_LKUP_COLOR_MAST,YANTRA_ITEM_iMAGE WHERE [YANTRA_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_QUOT_DET.QUOT_CURRENCY = YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID AND YANTRA_QUOT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND YANTRA_ITEM_IMAGE.ITEM_CODE=YANTRA_QUOT_DET.ITEM_CODE  and  [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + " order by YANTRA_QUOT_DET.Quot_OrderNo");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Discount");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Room");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("GSTperc");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("GSTRate");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("CurrencyId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Color");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("ColorId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("OptId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Floor");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SrlOrder");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Item_Image");
                SalesQuotationItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["QUOT_DET_QTY"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Rate"] = dbManager.DataReader["QUOT_RATE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();

                    dr["Discount"] = dbManager.DataReader["QUOT_DISC"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["QUOT_SPPRICE"].ToString();
                    dr["Room"] = dbManager.DataReader["QUOT_ROOM"].ToString();
                    dr["GSTperc"] = dbManager.DataReader["QUOT_DET_GST"].ToString();
                    dr["GSTRate"] = dbManager.DataReader["QUOT_DET_GSTRATE"].ToString();
                    dr["CurrencyId"] = dbManager.DataReader["CURRENCY_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["OptId"] = dbManager.DataReader["OPTIONALID"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_TYPE"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS"].ToString();
                    dr["Floor"] = dbManager.DataReader["QUOT_FLOOR"].ToString();
                    dr["SrlOrder"] = dbManager.DataReader["Quot_OrderNo"].ToString();
                    dr["Item_Image"] = dbManager.DataReader["Item_Image"].ToString();

                    SalesQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesQuotationItems;
                gv.DataBind();
            }
            public void SalesQuotationDetails_Select1(string QuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_QUOT_DET.QUOT_CURRENCY = YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID AND YANTRA_QUOT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and  [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + " order by YANTRA_QUOT_DET.Quot_OrderNo");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Discount");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Room");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("GSTperc");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("GSTRate");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("CurrencyId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Color");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("ColorId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("OptId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Floor");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SrlOrder");
                SalesQuotationItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["QUOT_DET_QTY"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Rate"] = dbManager.DataReader["QUOT_RATE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();

                    dr["Discount"] = dbManager.DataReader["QUOT_DISC"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["QUOT_SPPRICE"].ToString();
                    dr["Room"] = dbManager.DataReader["QUOT_ROOM"].ToString();
                    dr["GSTperc"] = dbManager.DataReader["QUOT_DET_GST"].ToString();
                    dr["GSTRate"] = dbManager.DataReader["QUOT_DET_GSTRATE"].ToString();
                    dr["CurrencyId"] = dbManager.DataReader["CURRENCY_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["OptId"] = dbManager.DataReader["OPTIONALID"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_TYPE"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS"].ToString();
                    dr["Floor"] = dbManager.DataReader["QUOT_FLOOR"].ToString();
                    dr["SrlOrder"] = dbManager.DataReader["Quot_OrderNo"].ToString();

                    SalesQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesQuotationItems;
                gv.DataBind();
            }
            public void SalesQuotationDetails_Select(string QuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_QUOT_DET.QUOT_CURRENCY = YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID AND YANTRA_QUOT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and  [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + " order by YANTRA_QUOT_DET.Quot_OrderNo");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Discount");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Room");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("GSTperc");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("GSTRate");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("CurrencyId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Color");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("ColorId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("OptId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Floor");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SrlOrder");
                SalesQuotationItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["QUOT_DET_QTY"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Rate"] = dbManager.DataReader["QUOT_RATE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();

                    dr["Discount"] = dbManager.DataReader["QUOT_DISC"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["QUOT_SPPRICE"].ToString();
                    dr["Room"] = dbManager.DataReader["QUOT_ROOM"].ToString();
                    dr["GSTperc"] = dbManager.DataReader["QUOT_DET_GST"].ToString();
                    dr["GSTRate"] = dbManager.DataReader["QUOT_DET_GSTRATE"].ToString();
                    dr["CurrencyId"] = dbManager.DataReader["CURRENCY_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["OptId"] = dbManager.DataReader["OPTIONALID"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_TYPE"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS"].ToString();
                    dr["Floor"] = dbManager.DataReader["QUOT_FLOOR"].ToString();
                    dr["SrlOrder"] = dbManager.DataReader["Quot_OrderNo"].ToString();

                    SalesQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesQuotationItems;
                gv.DataBind();
            }

            public void SalesQuotationDetails_SelectForPO(string QuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_QUOT_DET.QUOT_CURRENCY = YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID AND YANTRA_QUOT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and  [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + " order by YANTRA_QUOT_DET.OPTIONALID");

                _commandText = string.Format("SELECT a.ITEM_CODE,a.COLOR_ID,a.QUOT_RATE," +
                                           " a.QUOT_DISC,a.REMARKS,a.ITEM_TYPE," +
                                           " YANTRA_ITEM_MAST.ITEM_NAME,YANTRA_ITEM_MAST.ITEM_MODEL_NO,YANTRA_LKUP_COLOR_MAST.COLOUR_NAME," +
                                           " YANTRA_LKUP_UOM.UOM_SHORT_DESC,YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID,YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_NAME," +
                                           " (select sum(cast (YANTRA_QUOT_DET.QUOT_DET_QTY as decimal(18,2))) FROM [YANTRA_QUOT_DET] " +
                                             " where [ITEM_CODE]=a.[ITEM_CODE] and [COLOR_ID]=a.[COLOR_ID] and QUOT_ID=" + QuotationId + ") as qty, (select sum(cast (YANTRA_QUOT_DET.QUOT_SPPRICE as decimal(18,2))) FROM [YANTRA_QUOT_DET] " +
                                             " where [ITEM_CODE]=a.[ITEM_CODE] and [COLOR_ID]=a.[COLOR_ID] and QUOT_ID=" + QuotationId + ") as SPPRICE " +
                                            "  FROM [YANTRA_QUOT_DET] a,[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE]," +
                                          "  YANTRA_LKUP_CURRENCY_TYPE,YANTRA_LKUP_COLOR_MAST WHERE a.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                           " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                           " a.QUOT_CURRENCY = YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID AND a.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and " +
                                           "  a.QUOT_ID=" + QuotationId + " group by a.[ITEM_CODE],a.[COLOR_ID],a.QUOT_RATE,a.QUOT_DISC,a.REMARKS,a.ITEM_TYPE," +
                                           " YANTRA_ITEM_MAST.ITEM_NAME,YANTRA_ITEM_MAST.ITEM_MODEL_NO,YANTRA_LKUP_COLOR_MAST.COLOUR_NAME," +
                                           " YANTRA_LKUP_UOM.UOM_SHORT_DESC,YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID,YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("Discount");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Room");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("CurrencyId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Color");
                SalesQuotationItems.Columns.Add(col);

                col = new DataColumn("ColorId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("OptId");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesQuotationItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["qty"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Rate"] = dbManager.DataReader["QUOT_RATE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();

                    dr["Discount"] = dbManager.DataReader["QUOT_DISC"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["SPPRICE"].ToString();
                    dr["Room"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["CurrencyId"] = dbManager.DataReader["CURRENCY_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["OptId"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_TYPE"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS"].ToString();

                    SalesQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesQuotationItems;
                gv.DataBind();
            }


            public void SalesQuotationDetailsSubItems_Select(string QuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_ITEM_DETAILS] WHERE [YANTRA_QUOT_ITEM_DETAILS].QUOTID=" + QuotationId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ITEM_CODE");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("SUBITEM_CODE");
                SalesQuotationItems.Columns.Add(col);
                
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesQuotationItems.NewRow();
                    dr["ITEM_CODE"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["SUBITEM_CODE"] = dbManager.DataReader["SUBITEM_CODE"].ToString();
                    
                    SalesQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesQuotationItems;
                gv.DataBind();
            }


            public static void SalesQuotation_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT *,QUOT_NO+' '+QUOT_REVISED_KEY AS QUOTNO FROM [YANTRA_QUOT_MAST] ORDER BY QUOT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "QUOTNO", "QUOT_ID");
                }
            }

            public static void SalesQuotation_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                 _commandText = string.Format("SELECT *,QUOT_NO+' '+QUOT_REVISED_KEY AS QUOTNO FROM [YANTRA_ENQ_MAST]  inner join [YANTRA_QUOT_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID  where [YANTRA_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY QUOT_ID");

                // _commandText = string.Format("SELECT *,QUOT_NO+' '+QUOT_REVISED_KEY AS QUOTNO FROM [YANTRA_ENQ_MAST]  inner join [YANTRA_QUOT_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID  where [YANTRA_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY QUOT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "QUOTNO", "QUOT_ID");
                }
            }

            public int Get_Ids_Select(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST]  WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_ASSIGN_TASKS].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND " +
                                            " [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_QUOT_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["ASSIGN_TASK_ID"].ToString();
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;

            }

            public static void SalesQuotationItemTypes_Select(string QuotationId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_QUOT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_QUOT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void SalesQuotationItemTypes1_Select(string QuotationId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_QUOT_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_QUOT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE)  AND  [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + " And YANTRA_ITEM_MAST.Status !=0 order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }





            public static void SalesQuotationItemNames_Select(string QuotationId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_QUOT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_QUOT_DET].QUOT_ID=" + QuotationId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }

            public int TenderWithQuotationRaised_Select(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT TOP 1 * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_LKUP_ENQ_MODE].ENQM_NAME='TENDER' AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND  [YANTRA_ENQ_MAST].ENQ_ID='" + EnquiryId + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_REVISED_KEY DESC");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.EnqNo = dbManager.DataReader["ENQ_NO"].ToString();
                    this.EnqDate = Convert.ToDateTime(dbManager.DataReader["ENQ_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.QuotDDNo = dbManager.DataReader["QUOT_DD_NO"].ToString();
                    this.QuotDDDate = Convert.ToDateTime(dbManager.DataReader["QUOT_DD_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.QuotBankName = dbManager.DataReader["QUOT_BANK_NAME"].ToString();
                    this.QuotTotalEMDCharges = dbManager.DataReader["QUOT_EMD_CHARGES"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public int SalesQuotationDetails1_Select(string ModelName, string qutotid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE WHERE [YANTRA_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_QUOT_DET.QUOT_CURRENCY = YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID AND  [YANTRA_ITEM_MAST].ITEM_CODE=" + ModelName + " and YANTRA_QUOT_DET.QUOT_ID=" + qutotid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);



                if (dbManager.DataReader.Read())
                {

                    this.QuotDetItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.QuotDetItemName = dbManager.DataReader["ITEM_NAME"].ToString();
                    this.quotUom = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    this.QuotDetQty = dbManager.DataReader["QUOT_DET_QTY"].ToString();
                    this.CurrencyName = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    this.QuotRate = dbManager.DataReader["QUOT_RATE"].ToString();
                    this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();

                    this.QuotDetDisc = dbManager.DataReader["QUOT_DISC"].ToString();
                    this.QuotDetSpPrice = dbManager.DataReader["QUOT_SPPRICE"].ToString();
                    this.QuotRoom = dbManager.DataReader["QUOT_ROOM"].ToString();
                    this.CurrencyId = dbManager.DataReader["CURRENCY_ID"].ToString();
                 //   this.ColorId = dbManager.DataReader["COLOR_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

        }

        //Methods for Sales Assignments
        public class SalesAssignments
        {
            public string EnqId, EnqNo, EnqDate, EmpId, AssignTaskId, AssignTaskNo, AssingDate, DueDate, AssignRemarks, AssignStatus, CustId, EnqAssignFollowUpDet_Id, FollowUpEmpId, FollowUpDate, FollowUpDesc,Cpid;

            public SalesAssignments()
            {
            }

            public static string SalesAssignments_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(ASSIGN_TASK_NO,LEFT(ASSIGN_TASK_NO,5),''))),0)+1 FROM [YANTRA_ENQ_ASSIGN_TASKS]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ASSIGN_TASK_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_ENQ_ASSIGN_TASKS]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_ENQ_ASSIGN_TASKS", "ASSIGN_TASK_NO");
            }

            public string SalesAssignments_Save()
            {
                this.AssignTaskNo = SalesAssignments_AutoGenCode();
                this.AssignTaskId = AutoGenMaxId("[YANTRA_ENQ_ASSIGN_TASKS]", "ASSIGN_TASK_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_ENQ_ASSIGN_TASKS] VALUES({0},'{1}',{2},{3},'{4}','{5}','{6}','{7}',{8})", this.AssignTaskId, this.AssignTaskNo, this.EnqId, this.EmpId, this.AssingDate, this.DueDate, this.AssignRemarks, this.AssignStatus,this.Cpid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Assignments Details", "120");

                }
                return _returnStringMessage;
            }

            public string SalesAssignments_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ENQ_ASSIGN_TASKS] SET  EMP_ID={0},ASSIGN_DATE='{1}',DUE_DATE='{2}',REMARKS='{3}',CP_ID = {5} WHERE ENQ_ID={4}", this.EmpId, this.AssingDate, this.DueDate, this.AssignRemarks, this.EnqId,this.Cpid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Sales Assignments Details", "120");

                }
                return _returnStringMessage;
            }

            public string SalesAssignments_Delete(string AssignTaskId)
            {
                if (DeleteRecord("[YANTRA_ENQ_ASSIGN_TASKS]", "ASSIGN_TASK_ID", AssignTaskId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Sales Assignments Details", "120");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static string SalesAssignmentsStatus_Update(SMStatus Status, string AssignTaskId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ENQ_ASSIGN_TASKS] SET  ASSIGN_STATUS='{0}' WHERE ASSIGN_TASK_ID='{1}'", Status, AssignTaskId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Assignments Status Details", "120");

                }
                return _returnStringMessage;
            }


            public static string SalesAssignmentsStatusQua_Update(string Status, string AssignTaskId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ENQ_ASSIGN_TASKS] SET  ASSIGN_STATUS='{0}' WHERE ENQ_ID='{1}'", Status, AssignTaskId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Assignments Status Quotation Details", "120");

                }
                return _returnStringMessage;
            }




            public string SalesAssignmentsFollowUp_Save()
            {
                this.EnqAssignFollowUpDet_Id = AutoGenMaxId("[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET]", "ENQ_ASSIGN_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET] VALUES({0},{1},{2},'{3}','{4}')", this.EnqAssignFollowUpDet_Id, this.AssignTaskId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Assignments Follow Up Details", "120");

                }
                return _returnStringMessage;
            }

            public int SalesAssignments_Select(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_ENQ_ASSIGN_TASKS],[YANTRA_CUSTOMER_MAST] WHERE [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_ENQ_ASSIGN_TASKS].ENQ_ID AND [YANTRA_CUSTOMER_MAST].CUST_ID= [YANTRA_ENQ_MAST].CUST_ID AND " +
                                            " [YANTRA_ENQ_MAST].ENQ_ID ='" + EnquiryId + "' ORDER BY [YANTRA_ENQ_ASSIGN_TASKS].ASSIGN_TASK_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AssignTaskId = dbManager.DataReader["ASSIGN_TASK_ID"].ToString();
                    this.AssignTaskNo = dbManager.DataReader["ASSIGN_TASK_NO"].ToString();
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.EnqNo = dbManager.DataReader["ENQ_NO"].ToString();
                    this.EnqDate = Convert.ToDateTime(dbManager.DataReader["ENQ_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.EmpId = dbManager.DataReader["EMP_ID"].ToString();
                    this.AssingDate = Convert.ToDateTime(dbManager.DataReader["ASSIGN_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.DueDate = Convert.ToDateTime(dbManager.DataReader["DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AssignRemarks = dbManager.DataReader["REMARKS"].ToString();
                    this.AssignStatus = dbManager.DataReader["ASSIGN_STATUS"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            //public int SalesAssignmentsFollowUp_Select(string AssignTaskId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET],[YANTRA_ENQ_ASSIGN_TASKS] WHERE [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].ASSIGN_TASK_ID=[YANTRA_ENQ_ASSIGN_TASKS].ASSIGN_TASK_ID AND " +
            //                                " [YANTRA_ENQ_ASSIGN_TASKS].ASSIGN_TASK_ID ='" + AssignTaskId + "' ORDER BY [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].ENQ_ASSIGN_FOLLOWUP_DET_ID DESC ");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.AssignTaskId = dbManager.DataReader["ASSIGN_TASK_ID"].ToString();
            //        this.AssignTaskNo = dbManager.DataReader["ASSIGN_TASK_NO"].ToString();
            //        this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
            //        this.FollowUpEmpId = dbManager.DataReader["EMP_ID"].ToString();
            //        this.FollowUpDate = dbManager.DataReader["FU_DATE"].ToString();
            //        this.FollowUpDesc = dbManager.DataReader["FU_DESC"].ToString();
            //        _returnIntValue = 1;
            //    }
            //    else
            //    {
            //        _returnIntValue = 0;
            //    }
            //    return _returnIntValue;
            //}

            public static void SalesAssignments_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_ASSIGN_TASKS] ORDER BY ASSIGN_TASK_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ASSIGN_TASK_NO", "ASSIGN_TASK_ID");
                }
            }
        }

        //Methods For Sales Order Form
        public class SalesOrder
        {
            public string AmeId,AmeNo,AmeDate,SOId, SONo, SODate, QuotId, QuotNo, CustId, SORespId, SOSalespId, SOPreparedBy, SOCheckedBy, SOApprovedBy,POInchaarge,StoreIncharge, SOAcceptanceFlag, SODelivery, SOCurrencyTypeId, SOPackageCharges, SOPaymentTerms, SOCSTax, SOExciseDuty, SOGuarantee, DespmId, SOInsurance, SOTransportCharges, SOJurisdiction, SOErection, SOInspection, SOValidity, SOOtherSpec, EnqId, AssignTaskId, ContactName1, ContactPhone1, ContactEmail1, ContactName2, ContactPhone2, ContactEmail2, ConsignmentTo, InvoiceTo, ContactDesig1, ContactDesig2, SOAdvanceAmt, SOFLag, SOFiles, SOVAT, SOAccessories, SOExtraSpares, SOCustPONo, SOCustPODated, SOCSTNo, SOTINNo, CustUnitId, CustDetId,Sototalamt,SOCUSTID,sosalestatus;
            public string SODetId, SOItemCode, ItemModelNo, SOModelNo, SODetQty, SORate, SODetSpec, SODetRemarks, SODetPriority, SODetDeliveryStatus, SODETDeliveryDate, SODetRoom, SODetPrice, CpId, Sales, Cp_ID_Confirm, emodelno, eqty, erate, eamt, AnnexureQty;
            public string SOUploadId, SOUploadFileName, SOUploadDate,ColorId,Paymentrec,BalanceQty,SoDetAmt,SoDetGST,IndentQty,Orderdqty,OrderPayed,OrderShipped,OrderArrived,Blocked;
            public enum SOItemStatus { PartiallyDelivered = 0, Delivered = 1,Closed = 2 }
            public string SODocdate, SODocRemarks, SODocuments, SOtId;

            public SalesOrder()
            {
            }
            #region GridBind with Statement
            public static void GridBindwithCommand(GridView gridview, string command)
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, command);
                gridview.DataSource = dbManager.DataReader;
                gridview.DataBind();
                // gridview.HeaderRow.TableSection = TableRowSection.TableHeader;  

            }
            #endregion
            public string SODocuments_Details_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [SalesOrder_Documents] SELECT ISNULL(MAX(SO_Doc_Id),0)+1,'{0}','{1}','{2}',{3} FROM [SalesOrder_Documents]", this.SODocdate, this.SODocRemarks, this.SODocuments, this.SOId );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                }
                return _returnStringMessage;
            }
            public string SODocumentsDetails_Delete(string Eleid)
            {
                if (DeleteRecord("[SalesOrder_Documents]", "SO_Doc_Id", Eleid) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }
            public int SalesOrder_SelectForComplaint(string DelId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  dbo.YANTRA_ENQ_MAST.CUST_ID FROM dbo.YANTRA_ENQ_MAST INNER JOIN  dbo.YANTRA_QUOT_MAST INNER JOIN dbo.YANTRA_DELIVERY_CHALLAN_MAST INNER JOIN  dbo.YANTRA_SO_MAST ON dbo.YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = dbo.YANTRA_SO_MAST.SO_ID ON dbo.YANTRA_QUOT_MAST.QUOT_ID = dbo.YANTRA_SO_MAST.QUOT_ID ON dbo.YANTRA_ENQ_MAST.ENQ_ID = dbo.YANTRA_QUOT_MAST.ENQ_ID where dbo.YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DelId + " ORDER BY YANTRA_DELIVERY_CHALLAN_MAST.DC_ID ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public int SalesOrderInvoice_SelectForComplaint(string SId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT     dbo.YANTRA_ENQ_MAST.CUST_ID FROM  dbo.YANTRA_SO_MAST INNER JOIN dbo.YANTRA_SALES_INVOICE_MAST INNER JOIN   dbo.YANTRA_DELIVERY_CHALLAN_MAST ON dbo.YANTRA_SALES_INVOICE_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_MAST.DC_ID ON  dbo.YANTRA_SO_MAST.SO_ID = dbo.YANTRA_DELIVERY_CHALLAN_MAST.SO_ID INNER JOIN dbo.YANTRA_QUOT_MAST INNER JOIN dbo.YANTRA_ENQ_MAST ON dbo.YANTRA_QUOT_MAST.ENQ_ID = dbo.YANTRA_ENQ_MAST.ENQ_ID ON dbo.YANTRA_SO_MAST.QUOT_ID = dbo.YANTRA_QUOT_MAST.QUOT_ID WHERE dbo.YANTRA_SALES_INVOICE_MAST.SI_ID=" + SId + " ORDER BY YANTRA_SALES_INVOICE_MAST.SI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string OrderedQuantity(string itemcode, string so_id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "select sum(cast(ind_det_qty as bigint)) from yantra_indent_det where item_code = " + itemcode + " and ind_det_soid = " + so_id + "";

                return Convert.ToString(dbManager.ExecuteScalar(CommandType.Text, _commandText));

            }
            public static string Amendment_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SO_NO,LEFT(SO_NO,5),''))),0)+1 FROM [YANTRA_SO_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_SO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("Amendment_tbl", "Amendment_NO");
            }
            public static string SalesOrder_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SO_NO,LEFT(SO_NO,5),''))),0)+1 FROM [YANTRA_SO_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_SO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_SO_MAST", "SO_NO");
            }
            public string logid, logdesc, logtypeid, logcatid, userid, dtadd;

            public string log_statusUpdate()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Update [log_details_tbl1] SET logTypeid='{0}' where logid='{1}'", this.logtypeid, this.logid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    //log.add_Insert("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }
            public string log_commentsInsert()
            {
                //this.logid = AutoGenMaxId1("[log_details_tbl1]", "logid");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [log_details_tbl1] values( '{0}','{1}','{2}','{3}','{4}','{5}') ",this.logid , this.logdesc, this.logtypeid, this.logcatid, this.userid, this.dtadd);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }
            public string BillingAdd, DeliveryAdd,EmpNamme, ContactNo;

            public string Amendment_Save()
            {
                this.SONo = Amendment_AutoGenCode();
                this.AmeId = AutoGenMaxId("[Amendment_tbl]", "Amendment_Id");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Insert into [Amendment_tbl] SELECT ISNULL(MAX(Amendment_Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}' From Amendment_tbl", this.SONo, this.SODate, this.SOCUSTID, this.SODetId, this.SOPreparedBy, this.SOApprovedBy, this.POInchaarge, this.StoreIncharge, this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }

            public string SalesOrder_Save()
            {
                this.SONo = SalesOrder_AutoGenCode();
                this.SOId = AutoGenMaxId("[YANTRA_SO_MAST]", "SO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SO_MAST] SELECT ISNULL(MAX(SO_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}',{32},{33},'{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}',{43},'{44}',{45},{46},'{47}','{48}' FROM [YANTRA_SO_MAST]", this.SONo, this.SODate, this.QuotId, this.SORespId, this.SOSalespId, this.SOPreparedBy, this.SOCheckedBy, this.SOApprovedBy, this.SOAcceptanceFlag, this.SODelivery, this.SOCurrencyTypeId, this.SOPaymentTerms, this.SOPackageCharges, this.SOExciseDuty, this.SOCSTax, this.DespmId, this.SOGuarantee, this.SOTransportCharges, this.SOInsurance, this.SOErection, this.SOJurisdiction, this.SOValidity, this.SOInspection, this.SOOtherSpec, this.ContactName1, this.ContactPhone1, this.ContactEmail1, this.ContactName2, this.ContactPhone2, this.ContactEmail2, this.ConsignmentTo, this.InvoiceTo, this.ContactDesig1, this.ContactDesig2, this.SOAdvanceAmt, this.SOFiles, this.SOVAT, this.SOAccessories, this.SOExtraSpares, this.SOCustPONo, this.SOCustPODated, this.SOCSTNo, this.SOTINNo,this.CpId,this.Sototalamt,this.SOCUSTID,this.sosalestatus,this.BillingAdd,this.DeliveryAdd);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }

            public string SalesOrder_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SO_MAST] SET SO_DATE='{0}',SO_SALESP_ID='{2}',SO_PREPARED_BY='{3}',SO_CHECKED_BY='{4}',SO_APPROVED_BY='{5}',SO_DELIVERY='{6}',CURRENCY_ID={7},SO_PAY_TERM='{8}',SO_PACK_CHARGES='{9}',SO_EXCISE='{10}',SO_CST='{11}',DESPM_ID={12},SO_GUARANTEE='{13}',SO_TRANS_CHARGES='{14}',SO_INSURANCE='{15}',SO_EREC_COMM='{16}',SO_JURISDICTION='{17}',SO_VALIDITY='{18}',SO_INSPECTION='{19}',SO_OTHER_SPEC='{20}',SO_CONTACT_NAME1='{21}',SO_CONTACT_PHONE1='{22}',SO_CONTACT_EMAIL1='{23}',SO_CONTACT_NAME2='{24}',SO_CONTACT_PHONE2='{25}',SO_CONTACT_EMAIL2='{26}',SO_CONSIGNMENT_TO='{27}',SO_INVOICE_TO='{28}',SO_DESIGNATION1={29},SO_DESIGNATION2={30},SO_ADVANCE_AMT='{31}',SO_FILES='{32}',SO_VAT='{33}',SO_ACCESSORIES='{34}',SO_EXTRA_SPARES='{35}',SO_CUST_PO_NO='{36}',SO_CUST_PO_DATED='{37}',SO_CST_NO='{38}',SO_TIN_NO='{39}',CP_ID={41},SO_TOTAL_AMT = '{42}',SO_CUST_ID = {43},SO_SALE_STATUS = {44},BillingAddress='{45}',DeliveryAddress='{46}',SO_Resp_Id='{47}' WHERE SO_ID='{40}'", this.SODate,this.SORespId, this.SOSalespId, this.SOPreparedBy, this.SOCheckedBy, this.SOApprovedBy, this.SODelivery, this.SOCurrencyTypeId, this.SOPaymentTerms, this.SOPackageCharges, this.SOExciseDuty, this.SOCSTax, this.DespmId, this.SOGuarantee, this.SOTransportCharges, this.SOInsurance, this.SOErection, this.SOJurisdiction, this.SOValidity, this.SOInspection, this.SOOtherSpec, this.ContactName1, this.ContactPhone1, this.ContactEmail1, this.ContactName2, this.ContactPhone2, this.ContactEmail2, this.ConsignmentTo, this.InvoiceTo, this.ContactDesig1, this.ContactDesig2, this.SOAdvanceAmt, this.SOFiles, this.SOVAT, this.SOAccessories, this.SOExtraSpares, this.SOCustPONo, this.SOCustPODated, this.SOCSTNo, this.SOTINNo, this.SOId, this.CpId, this.Sototalamt, this.SOCUSTID, this.sosalestatus,this.BillingAdd,this.DeliveryAdd,this.SORespId );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                   // log.add_Update("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }

            public string SalesOrder_Delete(string SalesOrderId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_SO_DET]", "SO_ID", SalesOrderId) == true)
                {
                    if (DeleteRecord("[YANTRA_SO_MAST]", "SO_ID", SalesOrderId) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Sales Order Details", "121");

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }
            public string Amendment_Det_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("insert into Amendment_Det_tbl select isnull(max(Ame_Det_Id),0)+1,{0},'{1}','{2}','{3}','{4}','{5}',{6},{7},{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19} from Amendment_Det_tbl", this.AmeId, this.SODetId, this.SOItemCode, this.SOModelNo, this.SODetSpec,this.SODetQty,this.SODetPrice,this.SoDetAmt,this.SoDetGST,this.BalanceQty,this.IndentQty,this.Orderdqty,this.OrderPayed,this.OrderShipped,this.OrderArrived,this.Blocked,this.emodelno ,this.eqty ,this.erate ,this.eamt  );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }
            public string SalesOrderDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SO_DET] SELECT ISNULL(MAX(SO_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12},0 ,'{13}',{14} FROM [YANTRA_SO_DET]",
                                      this.SOId, this.SOItemCode, this.SODetQty, this.SORate, this.SODetSpec, this.SODetPriority, this.SODetRemarks, this.SODetDeliveryStatus, this.SODETDeliveryDate, this.SODetRoom, this.SODetPrice, this.ColorId, this.BalanceQty, this.Sales, this.AnnexureQty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }
            public string SalesOrderStatus_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SO_DET] SET SO_RES_STATUS='{0}' WHERE SO_DET_ID={1}",this.BalanceQty, this.SODetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    // log.add_Update("Sales Order Details", "121");

                }
                return _returnStringMessage;
            }
            public int SalesOrderDetails_Delete(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SO_DET] WHERE SO_ID={0}", SalesOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int SalesOrderDetailsDone_Delete(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SO_DET] WHERE SO_DET_ID = {0}", SalesOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public static void CustomerMaster_SelectDdlCustomerName(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CUST_ID,CUST_NAME,cast(CUST_ID as nvarchar(50))+' / '+CUST_NAME AS CUST_IDNAME FROM [YANTRA_CUSTOMER_MAST] ORDER BY CUST_COMPANY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_IDNAME", "CUST_ID");
                }
            }

            public int GetOtherPONo(string SONo)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SO_EREC_COMM,'O','')),0)+1 FROM YANTRA_SO_MAST  WHERE SO_NO  LIKE '" + this.SONo + "%'").ToString());
                this.SOErection  = "O" + _returnIntValue.ToString();
                return _returnIntValue;
            }
            public int SalesOrder_Select(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT *,[YANTRA_SO_MAST].CURRENCY_ID as hai FROM [YANTRA_SO_MAST],[YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],YANTRA_EMPLOYEE_MAST,YANTRA_QUOT_DET,YANTRA_SO_DET WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND" +
                                            "  YANTRA_SO_MAST.SO_ID = YANTRA_SO_DET.SO_ID and YANTRA_EMPLOYEE_MAST.EMP_ID=[YANTRA_ENQ_MAST].[ENQ_ORIG_NAME] and YANTRA_QUOT_MAST.QUOT_ID = YANTRA_QUOT_DET.QUOT_ID and  [YANTRA_SO_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].SO_ID='" + SalesOrderId + "' ORDER BY [YANTRA_SO_MAST].SO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.SONo = dbManager.DataReader["SO_NO"].ToString();
                    this.SODate = Convert.ToDateTime(dbManager.DataReader["SO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();

                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.QuotNo = dbManager.DataReader["QUOT_NO"].ToString();
                    this.SORespId = dbManager.DataReader["SO_RESP_ID"].ToString();
                    this.SOSalespId = dbManager.DataReader["SO_SALESP_ID"].ToString();
                    this.SOPreparedBy = dbManager.DataReader["SO_PREPARED_BY"].ToString();
                    this.SOCheckedBy = dbManager.DataReader["SO_CHECKED_BY"].ToString();
                    this.SOApprovedBy = dbManager.DataReader["SO_APPROVED_BY"].ToString();
                    this.SOAcceptanceFlag = dbManager.DataReader["SO_ACCEPTANCE_FLAG"].ToString();
                    this.SODelivery = dbManager.DataReader["SO_DELIVERY"].ToString();
                    this.SOCurrencyTypeId = dbManager.DataReader["hai"].ToString();
                    this.SOPaymentTerms = dbManager.DataReader["SO_PAY_TERM"].ToString();
                    this.SOPackageCharges = dbManager.DataReader["SO_PACK_CHARGES"].ToString();
                    this.SOExciseDuty = dbManager.DataReader["SO_EXCISE"].ToString();
                    this.SOCSTax = dbManager.DataReader["SO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SOGuarantee = dbManager.DataReader["SO_GUARANTEE"].ToString();
                    this.SOTransportCharges = dbManager.DataReader["SO_TRANS_CHARGES"].ToString();
                    this.SOInsurance = dbManager.DataReader["SO_INSURANCE"].ToString();
                    this.SOErection = dbManager.DataReader["SO_EREC_COMM"].ToString();
                    this.SOJurisdiction = dbManager.DataReader["SO_JURISDICTION"].ToString();
                    this.SOValidity = dbManager.DataReader["SO_VALIDITY"].ToString();
                    this.SOInspection = dbManager.DataReader["SO_INSPECTION"].ToString();
                    this.SOOtherSpec = dbManager.DataReader["SO_OTHER_SPEC"].ToString();
                    this.ContactName1 = dbManager.DataReader["SO_CONTACT_NAME1"].ToString();
                    this.ContactPhone1 = dbManager.DataReader["SO_CONTACT_PHONE1"].ToString();
                    this.ContactEmail1 = dbManager.DataReader["SO_CONTACT_EMAIL1"].ToString();
                    this.ContactName2 = dbManager.DataReader["SO_CONTACT_NAME2"].ToString();
                    this.ContactPhone2 = dbManager.DataReader["SO_CONTACT_PHONE2"].ToString();
                    this.ContactEmail2 = dbManager.DataReader["SO_CONTACT_EMAIL2"].ToString();
                    this.ConsignmentTo = dbManager.DataReader["SO_CONSIGNMENT_TO"].ToString();
                    this.InvoiceTo = dbManager.DataReader["SO_INVOICE_TO"].ToString();
                    this.ContactDesig1 = dbManager.DataReader["SO_DESIGNATION1"].ToString();
                    this.ContactDesig2 = dbManager.DataReader["SO_DESIGNATION2"].ToString();
                    this.SOAdvanceAmt = dbManager.DataReader["SO_ADVANCE_AMT"].ToString();
                    this.SOFiles = dbManager.DataReader["SO_FILES"].ToString();
                    this.SOVAT = dbManager.DataReader["SO_VAT"].ToString();
                    this.SOAccessories = dbManager.DataReader["SO_ACCESSORIES"].ToString();
                    this.SOExtraSpares = dbManager.DataReader["SO_EXTRA_SPARES"].ToString();
                    this.SOCustPONo = dbManager.DataReader["SO_CUST_PO_NO"].ToString();
                    this.SOCustPODated = Convert.ToDateTime(dbManager.DataReader["SO_CUST_PO_DATED"].ToString()).ToString("dd/MM/yyyy");
                    if (this.SOCustPODated == "01/01/1900") { this.SOCustPODated = ""; }
                    this.SOCSTNo = dbManager.DataReader["SO_CST_NO"].ToString();
                    this.SOTINNo = dbManager.DataReader["SO_TIN_NO"].ToString();
                    this.SODetPrice = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    this.Sototalamt = dbManager.DataReader["SO_TOTAL_AMT"].ToString();
                    this.SODetId = dbManager.DataReader["SO_DET_ID"].ToString();
                    this.sosalestatus = dbManager.DataReader["SO_SALE_STATUS"].ToString();
                    this.SOOtherSpec = dbManager.DataReader["SO_OTHER_SPEC"].ToString();
                    this.BillingAdd = dbManager.DataReader["BillingAddress"].ToString();
                    this.DeliveryAdd = dbManager.DataReader["DeliveryAddress"].ToString();
                    this.EmpNamme = dbManager.DataReader["EMP_FIRST_NAME"].ToString();
                    this.ContactNo = dbManager.DataReader["EMP_MOBILE"].ToString();

                    this.Cp_ID_Confirm = dbManager.DataReader["CP_ID"].ToString();
                    
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public int PaymentRecivedSum(string Poid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SUM(PR_AMT_RECEIVED) AS amount FROM dbo.YANTRA_PAYMENTS_RECEIVED WHERE SO_ID ='" + Poid + "' ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Paymentrec = dbManager.DataReader["amount"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            
            }


            public int Get_Ids_Select1(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST],YANTRA_SO_DET   WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_ASSIGN_TASKS].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND " +
                                            "[YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_SO_MAST].SO_ID=YANTRA_SO_DET.SO_ID and YANTRA_CUSTOMER_MAST.CUST_ID =  '" + SalesOrderId + "' ORDER BY YANTRA_SO_DET.SO_ID  DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["ASSIGN_TASK_ID"].ToString();
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;

            }

            //public static void CusID_Select(Control ControlForBind, string SalesOrderId)
            //{
            //    dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST],YANTRA_SO_DET   WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_ASSIGN_TASKS].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND  [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_SO_MAST].SO_ID=YANTRA_SO_DET.SO_ID and YANTRA_CUSTOMER_MAST.CUST_ID =  '" + SalesOrderId + "' ORDER BY YANTRA_SO_DET.SO_ID  DESC ");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
            //    }
            //}
            public static void CusID_Select(Control ControlForBind, string SalesOrderId)
            {
                dbManager.Open();
                _commandText = string.Format("select YANTRA_SO_MAST.SO_NO, YANTRA_SO_MAST.SO_ID from  YANTRA_SO_MAST INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_SO_MAST.SO_CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID where YANTRA_CUSTOMER_MAST.CUST_ID = '" + SalesOrderId + "'  ");
               // _commandText = string.Format("SELECT SO_NO,SO_ID FROM [YANTRA_ENQ_MAST],[YANTRA_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST]   WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_ASSIGN_TASKS].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND  [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID  and YANTRA_CUSTOMER_MAST.CUST_ID =  '" + SalesOrderId + "'  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }
            public static string SalesOrderStatus_Update(SMStatus Status, string SOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SO_MAST] SET  SO_ACCEPTANCE_FLAG='{0}' WHERE SO_ID='{1}'", Status, SOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Order Status Details", "121");

                }
                return _returnStringMessage;
            }
           
            public  string SOAmmendment_Update(string SoDetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SO_DET] SET SO_DET_QTY='{0}',SO_RATE={1},SO_DET_PRICE={2},BalanceQty='{3}',Annexure_Qty={7} where SO_DET_ID='{4}' and ITEM_CODE='{5}' and COLOR_ID='{6}'", SODetQty, SORate, SODetPrice, BalanceQty, SoDetId, SOItemCode, ColorId,AnnexureQty );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Ammendments Done Successfully";
                }
                return _returnStringMessage;
            }
            public int AmendmentDetails_Select(string AmeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from Amendment_tbl inner join YANTRA_CUSTOMER_MAST on Amendment_tbl.Cust_ID =YANTRA_CUSTOMER_MAST .CUST_ID inner join YANTRA_SO_MAST on Amendment_tbl .So_Id =YANTRA_SO_MAST .SO_ID inner join YANTRA_COMP_PROFILE on Amendment_tbl .cp_id =YANTRA_COMP_PROFILE .CP_ID  and Amendment_tbl.Amendment_ID='" + AmeId + "' ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AmeId = dbManager.DataReader["Amendment_Id"].ToString();
                    this.AmeNo = dbManager.DataReader["Amendment_No"].ToString();
                    this.AmeDate = Convert.ToDateTime(dbManager.DataReader["Date"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.SOId = dbManager.DataReader["So_Id"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                // dbManager.Close();
                return _returnIntValue;
            }

            public int Amendment_DetDelete(string AmeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [Amendment_Det_tbl] WHERE Amendment_ID={0}", AmeId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                // dbManager.Close();
                return _returnIntValue;
            }
            public string Amendment_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Update Amendment_tbl set Amendment_No='{0}',Date='{1}',Cust_Id='{2}',SO_ID='{3}',Prepared_By='{4}',cp_id='{5}' where Amendment_ID='{6}' ",this .AmeNo ,this.AmeDate ,this.CustId,this.SOId ,this.SOPreparedBy ,this.CpId,this.AmeId  );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                }
                return _returnStringMessage;
            }
            public string Amendment_Delete(string AmeId)
            {
                if (DeleteRecord("[Amendment_Det_tbl]", "Amendment_ID", AmeId) == true)
                {
                    if (DeleteRecord("[Amendment_tbl]", "Amendment_ID", AmeId) == true)
                    {
                        
                    }
                    else
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }
            public void SalesOrderDetails_Amendment_select(string SalesOrderId, GridView gv)
            {
                 if (dbManager.Transaction == null)
                    dbManager.Open();
                 _commandText = string.Format("select * from [YANTRA_ITEM_MAST],[YANTRA_SO_DET] left outer join Amendment_Det_tbl on YANTRA_SO_DET .SO_DET_ID =Amendment_Det_tbl .So_Det_Id inner join YANTRA_SO_MAST on YANTRA_SO_DET .SO_ID =YANTRA_SO_MAST .SO_ID  WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE and [YANTRA_SO_DET].SO_ID='" + SalesOrderId + "' order by [YANTRA_SO_DET].SO_DET_ID");

                 //_commandText = string.Format("select *,isnull((select SUM(Quantity) from BLOCK_New where item_code=YANTRA_SO_DET.item_code and colour_id=YANTRA_SO_DET.COLOR_ID and SO_Id=p.SO_Id and Customer_Id=YANTRA_SO_MAST.SO_CUST_ID),0) as BlockedQty from [YANTRA_ITEM_MAST],[YANTRA_SO_DET] left outer join Amendment_Det_tbl on YANTRA_SO_DET .SO_DET_ID =Amendment_Det_tbl .So_Det_Id inner join YANTRA_SO_MAST on YANTRA_SO_DET .SO_ID =YANTRA_SO_MAST .SO_ID left outer join YANTRA_WO_MAST on YANTRA_SO_MAST .SO_ID =YANTRA_WO_MAST  .SO_ID left outer join BlOCK_New p on YANTRA_WO_MAST .WO_ID =p .SO_Id left outer join YANTRA_INDENT_DET on YANTRA_SO_DET .SO_DET_ID =YANTRA_INDENT_DET .IND_DET_SO_ID left outer join YANTRA_FIXED_PO_DET on YANTRA_INDENT_DET .IND_DET_ID =YANTRA_FIXED_PO_DET .FPO_DET_IND_DET_ID left outer join YANTRA_PURCHASE_INVOICE_DET on YANTRA_FIXED_PO_DET .FPO_DET_ID =YANTRA_PURCHASE_INVOICE_DET.PI_PONo left outer join YANTRA_SHIPPING_DETAILS_MASTER on YANTRA_FIXED_PO_DET .FPO_ID =YANTRA_SHIPPING_DETAILS_MASTER .FPO_ID  WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE and [YANTRA_SO_DET].SO_ID='" + SalesOrderId + "' order by [YANTRA_SO_DET].SO_DET_ID");
                 dbManager.ExecuteReader(CommandType.Text, _commandText);
                
                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("Slno");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ITEM_CODE");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ITEM_MODEL_NO");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ITEM_SPEC");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_DET_QTY");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_RATE");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_DET_PRICE");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SOGST");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("NewModel_No");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Reason");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Qty");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("GST");
                SalesOrderProducts.Columns.Add(col);
                //col = new DataColumn("BlockedQty");
                //SalesOrderProducts.Columns.Add(col);
                //col = new DataColumn("Ind_det_Qty");
                //SalesOrderProducts.Columns.Add(col);
                //col = new DataColumn("FPO_DET_QTY");
                //SalesOrderProducts.Columns.Add(col);
                //col = new DataColumn("PI_DET_QTY");
                //SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SalesOrderProducts .Columns.Add (col);


                 while (dbManager.DataReader.Read())
                 {
                     DataRow dr = SalesOrderProducts.NewRow();
                     dr["Slno"] = dbManager.DataReader["SO_DET_ID"].ToString();

                     dr["ITEM_CODE"] = dbManager.DataReader["ITEM_CODE"].ToString();
                     dr["ITEM_MODEL_NO"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                     dr["ITEM_SPEC"] = dbManager.DataReader["ITEM_SPEC"].ToString();

                     dr["SO_DET_QTY"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                     dr["SO_RATE"] = dbManager.DataReader["SO_RATE"].ToString();
                     dr["SO_DET_PRICE"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                     //dr["SOGST"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
                     dr["NewModel_No"] = dbManager.DataReader["NewModel_No"].ToString();
                     dr["Reason"] = dbManager.DataReader["Reason"].ToString();
                     dr["Qty"] = dbManager.DataReader["Qty"].ToString();
                     dr["Rate"] = dbManager.DataReader["Rate"].ToString();
                     dr["Amount"] = dbManager.DataReader["Amount"].ToString();
                     dr["GST"] = dbManager.DataReader["GST"].ToString();
                     //dr["BlockedQty"] = dbManager.DataReader["BlockedQty"].ToString();
                     //dr["Ind_det_Qty"] = dbManager.DataReader["Ind_det_Qty"].ToString();
                     //dr["FPO_DET_QTY"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                     //dr["PI_DET_QTY"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                     dr["BalanceQty"]=dbManager .DataReader ["BalanceQty"].ToString ();
                     SalesOrderProducts.Rows.Add(dr);
                 }
                 gv.DataSource = SalesOrderProducts;
                 gv.DataBind();
            }

            public void SalesOrderDetails_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],YANTRA_LKUP_PRODUCT_COMPANY,[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_COLOR_MAST,YANTRA_COMP_PROFILE WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                             "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND YANTRA_ITEM_MAST.BRAND_ID=YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID and [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID AND  YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_SO_DET.COLOR_ID and YANTRA_SO_MAST .CP_ID =YANTRA_COMP_PROFILE .CP_ID   and [YANTRA_SO_DET].SO_ID='" + SalesOrderId + "' order by [YANTRA_SO_DET].SO_DET_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                //_commandText2 = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code and colour_id=p.COLOUR_ID) as TOTAL_BLOCK_Stock,((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from block where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_AVALIABLE_STOCK from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.COLOUR_ID = " + Color + " group by p.item_code,p.colour_id");
                //dbManager.ExecuteReader(CommandType.Text, _commandText2);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("Slno");
                SalesOrderProducts.Columns.Add(col);

                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemSpec");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryStatus");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SODetId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Color");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_RES_STATUS");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Sales");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("HSN_CODE");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("GST_TAX");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("locid");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Annexure_Qty");
                SalesOrderProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["Slno"] = dbManager.DataReader["SO_DET_ID"].ToString();

                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["ItemSpec"] = dbManager.DataReader["ITEM_SPEC"].ToString();

                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
                    dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
                    dr["Room"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["SO_RES_STATUS"] = dbManager.DataReader["SO_RES_STATUS"].ToString();
                    dr["BalanceQty"] = dbManager.DataReader["BalanceQty"].ToString();
                    dr["Sales"] = dbManager.DataReader["Sales"].ToString();
                    dr["HSN_CODE"] = dbManager.DataReader["HSN_CODE"].ToString();
                    dr["GST_TAX"] = dbManager.DataReader["GST TAX"].ToString();
                    dr["locid"] = dbManager.DataReader["locid"].ToString();
                    dr["Annexure_Qty"] = dbManager.DataReader["Annexure_Qty"].ToString();

                    SalesOrderProducts.Rows.Add(dr);
                }

                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }

            //public void SalesOrderDetails_Select(string SalesOrderId, GridView gv)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
            //    //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


            //    _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],YANTRA_LKUP_PRODUCT_COMPANY,[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
            //                                 "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND YANTRA_ITEM_MAST.BRAND_ID=YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID and [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID AND  YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_SO_DET.COLOR_ID   and [YANTRA_SO_DET].SO_ID='" + SalesOrderId + "' order by [YANTRA_SO_DET].SO_DET_ID");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);

            //    //_commandText2 = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code and colour_id=p.COLOUR_ID) as TOTAL_BLOCK_Stock,((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from block where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_AVALIABLE_STOCK from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.COLOUR_ID = " + Color + " group by p.item_code,p.colour_id");
            //    //dbManager.ExecuteReader(CommandType.Text, _commandText2);

            //    DataTable SalesOrderProducts = new DataTable();
            //    DataColumn col = new DataColumn();
            //    col = new DataColumn("Slno");
            //    SalesOrderProducts.Columns.Add(col);

            //    col = new DataColumn("ItemCode");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ModelNo");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ItemName");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ItemSpec");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("UOM");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Quantity");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Currency");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Rate");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Specifications");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Remarks");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Priority");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("DeliveryDate");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("DeliveryStatus");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("SODetId");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Room");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Price");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Color");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ColorId");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("SO_RES_STATUS");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("BalanceQty");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Sales");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("HSN_CODE");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("GST_TAX");
            //    SalesOrderProducts.Columns.Add(col);

            //    while (dbManager.DataReader.Read())
            //    {
            //        DataRow dr = SalesOrderProducts.NewRow();
            //        dr["Slno"] = dbManager.DataReader["SO_DET_ID"].ToString();

            //        dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
            //        dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
            //        dr["ItemSpec"] = dbManager.DataReader["ITEM_SPEC"].ToString();

            //        dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
            //        dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
            //        dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
            //        dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
            //        dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
            //        dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
            //        dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //        dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
            //        dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
            //        dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
            //        dr["Room"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
            //        dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
            //        dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
            //        dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
            //        dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
            //        dr["SO_RES_STATUS"] = dbManager.DataReader["SO_RES_STATUS"].ToString();
            //        dr["BalanceQty"] = dbManager.DataReader["BalanceQty"].ToString();
            //        dr["Sales"] = dbManager.DataReader["Sales"].ToString();
            //        dr["HSN_CODE"] = dbManager.DataReader["HSN_CODE"].ToString();
            //        dr["GST_TAX"] = dbManager.DataReader["GST TAX"].ToString();

            //        SalesOrderProducts.Rows.Add(dr);
            //    }

            //    gv.DataSource = SalesOrderProducts;
            //    gv.DataBind();
            //}

            public int MumbaiStockDetails_Delete()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [Excel_Price]");
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int DiscExcel_Delete()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [Discontinued_Items]");
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }
            public void SalesOrderDetailsStatement_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                             "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID AND  YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_SO_DET.COLOR_ID  and YANTRA_SO_DET.Sales = 'Sales' and [YANTRA_SO_DET].SO_ID=" + SalesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryStatus");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SODetId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Color");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_RES_STATUS");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Sales");
                SalesOrderProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
                    dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
                    dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
                    dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["SO_RES_STATUS"] = dbManager.DataReader["SO_RES_STATUS"].ToString();
                    dr["BalanceQty"] = dbManager.DataReader["BalanceQty"].ToString();
                    dr["Sales"] = dbManager.DataReader["Sales"].ToString();


                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }






            public void SalesOrderDetailsBalanceQty_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                             "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID AND  YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_SO_DET.COLOR_ID and YANTRA_SO_DET.BalanceQty >= 1 and [YANTRA_SO_DET].SO_ID=" + SalesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryStatus");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SODetId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Color");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_RES_STATUS");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SalesOrderProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
                    dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
                    dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
                    dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["SO_RES_STATUS"] = dbManager.DataReader["SO_RES_STATUS"].ToString();
                    dr["BalanceQty"] = dbManager.DataReader["BalanceQty"].ToString();


                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }

            public void SalesOrderSelect_Check(string CustId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_SO_MAST where SO_CUST_ID ='" + CustId + "' order by so_id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("SO_ID");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SO_NO");
                SalesOrderProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["SO_ID"] = dbManager.DataReader["SO_ID"].ToString();
                    dr["SO_NO"] = dbManager.DataReader["SO_NO"].ToString();
                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }
            public void SalesOrderDetails_Select1(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


                _commandText = string.Format(" SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_COLOR_MAST,YANTRA_LKUP_PRODUCT_COMPANY WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  " +
                                             "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID AND  YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_SO_DET.COLOR_ID and YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and [YANTRA_SO_DET].SO_ID= '" + SalesOrderId+"' order by  [YANTRA_SO_DET].SO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);

                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderProducts.Columns.Add(col);
               
               
                col = new DataColumn("DeliveryDate");
                SalesOrderProducts.Columns.Add(col);
              
               
                col = new DataColumn("Room");
                SalesOrderProducts.Columns.Add(col);
              
                col = new DataColumn("Color");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesOrderProducts.Columns.Add(col);
              
                col = new DataColumn("Brand");
                SalesOrderProducts.Columns.Add(col);

                col = new DataColumn("ResStatus");
                SalesOrderProducts.Columns.Add(col);

                col = new DataColumn("So_Det_ID");
                SalesOrderProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["ITEM_SPEC"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
                    dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    dr["ResStatus"] = dbManager.DataReader["SO_RES_STATUS"].ToString();
                    dr["So_Det_Id"] = dbManager.DataReader["SO_DET_ID"].ToString();
                
                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }













            //public void SalesOrderDetails_Select1(string SalesOrderId, GridView gv)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
            //    //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


            //    _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_PRODUCT_COMPANY,,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
            //                                 "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID  and YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID AND YANTRA_QUOT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);

            //    DataTable SalesOrderProducts = new DataTable();
            //    DataColumn col = new DataColumn();
            //    col = new DataColumn("ItemCode");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ModelNo");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ItemName");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("UOM");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Quantity");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Currency");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Rate");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Specifications");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Remarks");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Priority");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("DeliveryDate");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("DeliveryStatus");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("SODetId");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Room");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Price");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Brand");
            //    SalesOrderProducts.Columns.Add(col);
            //    while (dbManager.DataReader.Read())
            //    {
            //        DataRow dr = SalesOrderProducts.NewRow();
            //        dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
            //        dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
            //        dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
            //        dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
            //        dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
            //        dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
            //        dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
            //        dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
            //        dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //        dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
            //        dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
            //        dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
            //        dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
            //        dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
            //        dr["Currency"] = dbManager.DataReader["CURRENCY_ID"].ToString();
            //        dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();

            //        SalesOrderProducts.Rows.Add(dr);
            //    }

            //    gv.DataSource = SalesOrderProducts;
            //    gv.DataBind();
            //}




            public void SalesOrderDetails_Select2(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_PRODUCT_COMPANY,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                             "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID  and YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID AND YANTRA_SO_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryStatus");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SODetId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                SalesOrderProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
                    dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
                    dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
                    dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();

                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }







            //public void SalesOrderDetails_Select1(string SalesOrderId, GridView gv)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
              
            //    _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_CURRENCY_TYPE,YANTRA_SO_MAST,YANTRA_LKUP_PRODUCT_COMPANY,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
            //                                 "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SO_MAST.CURRENCY_ID AND YANTRA_SO_MAST.SO_ID=YANTRA_SO_DET.SO_ID  and YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID and YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_SO_DET.COLOR_ID   AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);

            //    DataTable SalesOrderProducts = new DataTable();
            //    DataColumn col = new DataColumn();
            //    col = new DataColumn("ItemCode");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ModelNo");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ItemName");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("UOM");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Quantity");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Currency");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Rate");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Specifications");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Remarks");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Priority");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("DeliveryDate");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("DeliveryStatus");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("SODetId");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Room");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Price");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("Brand");
            //    SalesOrderProducts.Columns.Add(col);

            //    col = new DataColumn("Color");
            //    SalesOrderProducts.Columns.Add(col);
            //    col = new DataColumn("ColorId");
            //    SalesOrderProducts.Columns.Add(col);


            //    while (dbManager.DataReader.Read())
            //    {
            //        DataRow dr = SalesOrderProducts.NewRow();
            //        dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
            //        dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
            //        dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
            //        dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
            //        dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
            //        dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
            //        dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
            //        dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
            //        dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //        dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
            //        dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
            //        dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
            //        dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
            //        dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
            //        dr["Currency"] = dbManager.DataReader["CURRENCY_ID"].ToString();
            //        dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
            //        dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
            //        dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();

            //        SalesOrderProducts.Rows.Add(dr);
            //    }

            //    gv.DataSource = SalesOrderProducts;
            //    gv.DataBind();
            //    //dbManager.Close();
            //}


            public void SalesOrderDetailsSO_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryStatus");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SODetId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Room");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SO_DET_PRIORITY"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DeliveryDate"] = dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString();
                    dr["DeliveryStatus"] = dbManager.DataReader["SO_DET_DELIVERY_STATUS"].ToString();
                    dr["SODetId"] = dbManager.DataReader["SO_DET_ID"].ToString();
                    dr["Room"] = dbManager.DataReader["SO_DET_ROOM"].ToString();
                    dr["Price"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }




            public static string InventoryDetailsItemStatus_Update(SOItemStatus Status, string SODetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_DELIVERY_CHALLAN_MAST] SET  STATUS='{0}' WHERE DC_ID='{1}'", Status, SODetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Inventory Item Status Details", "122");

                }
                return _returnStringMessage;
            }
            public static string SalesOrderDetailsItemStatus_Update(SOItemStatus Status, string SODetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SO_DET] SET  SO_DET_DELIVERY_STATUS='{0}' WHERE SO_DET_ID='{1}'", Status, SODetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Order Item Status Details", "121");

                }
                return _returnStringMessage;
            }

            public static string SalesOrderDetailsItemStatusReset_Update(string SODetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SO_DET] SET  SO_DET_DELIVERY_STATUS='-' WHERE SO_DET_ID='{0}'", SODetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Order Item Status Reset Details", "121");

                }
                return _returnStringMessage;
            }

            public string SalesOrderApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SO_ACCEPTANCE_FLAG FROM [YANTRA_SO_MAST] WHERE SO_ID='{0}'", this.SOId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SO_MAST] SET SO_APPROVED_BY={0},SO_ACCEPTANCE_FLAG='{1}' WHERE SO_ID='{2}'", this.SOApprovedBy, SMStatus.Open, this.SOId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Order Approve Details", "121");

                }
                return _returnStringMessage;
            }

            public int Get_Ids_Select(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_ENQ_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST]  WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_ASSIGN_TASKS].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND " +
                                            "[YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_SO_MAST].SO_ID='" + SalesOrderId + "' ORDER BY [YANTRA_QUOT_MAST].QUOT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["ASSIGN_TASK_ID"].ToString();
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;

            }

            public static void SalesOrder_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SO_NO,SO_ID FROM [YANTRA_SO_MAST] ORDER BY SO_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }

            public static void SalesOrderByCustomerId_Select(Control ControlForBind, string CustomerId, string CustUnitId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID and [YANTRA_ENQ_MAST].CUST_ID=" + CustomerId + " AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + CustUnitId + " ORDER BY [YANTRA_SO_MAST].SO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }


            public static void SalesOrderForDelivery_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_SO_MAST WHERE SO_ID IN (SELECT SO_ID FROM YANTRA_SO_DET WHERE SO_DET_DELIVERY_STATUS <> 'DELIVERED') ORDER BY SO_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }


            public static void SalesOrderForDeliveryHighsale_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_SO_MAST WHERE SO_ID IN (SELECT SO_ID FROM YANTRA_SO_DET WHERE SO_DET_DELIVERY_STATUS <> 'DELIVERED') and SO_SALE_STATUS = '2' ORDER BY SO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }
            public static void SalesOrder_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_SO_MAST].* FROM [YANTRA_ENQ_MAST] inner join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID inner join [YANTRA_QUOT_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_SO_MAST] on [YANTRA_QUOT_MAST].QUOT_ID=[YANTRA_SO_MAST].QUOT_ID where [YANTRA_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_SO_MAST] .SO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }

            public Byte[] SOfileBytes;
            public string SOFileContentType;
            public string SalesOrderUploads_Save()
            {
                //  this.SONo = SalesOrder_AutoGenCode();
                this.SOUploadId = AutoGenMaxId("[YANTRA_SO_UPLOADS]", "SO_UPLOAD_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SO_UPLOADS] values (" + SOUploadId + ",{0},'{1}','{2}','{3}',convert(varbinary(max),'{4}'))", this.SOId, this.SOUploadFileName, this.SOUploadDate, this.SOFileContentType, this.SOfileBytes);
                //SqlCommand cmd = new SqlCommand(_commandText);
                //cmd.Parameters.AddWithValue("@Data", this.SOfileBytes);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                   // log.add_Insert("Sales Order Upload Details", "121");

                }
                return _returnStringMessage;
            }

            public static void SalesOrderItemTypes_Select(string SalesOrderId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_SO_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_SO_DET].SO_ID=" + SalesOrderId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void SalesOrderItemTypes1_Select(string SalesOrderId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO) FROM [YANTRA_SO_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_SO_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND  [YANTRA_SO_DET].SO_ID=" + SalesOrderId + " order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }

            public static void SalesOrderItemTypes2_Select(string SalesOrderId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO) FROM [YANTRA_SO_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_SO_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) and YANTRA_ITEM_MAST.Status !=0  AND  [YANTRA_SO_DET].SO_ID=" + SalesOrderId + " order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }


            public static void SalesQuatation_Select(string SalesleadId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT ([YANTRA_ENQ_DET].ENQ_DET_ID),([YANTRA_ITEM_MAST].ITEM_MODEL_NO) FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND YANTRA_ENQ_DET.ENQ_ID=" + SalesleadId + " order by [YANTRA_ENQ_DET].Enq_Det_Id asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ENQ_DET_ID");
                }
            }

            public static void POItems_Select1(string POId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO),PI_DET_ID FROM YANTRA_PURCHASE_INVOICE_DET,[YANTRA_ITEM_MAST] WHERE [YANTRA_PURCHASE_INVOICE_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND YANTRA_PURCHASE_INVOICE_DET.PI_ID=" + POId + " order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    //DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "PI_DET_ID");
                }
            }
            public static void POItems_Select(string POId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO),PI_DET_ID FROM YANTRA_PURCHASE_INVOICE_DET,[YANTRA_ITEM_MAST] WHERE [YANTRA_PURCHASE_INVOICE_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND YANTRA_PURCHASE_INVOICE_DET.PI_ID=" + POId + " order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                    //DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "PI_DET_ID");
                }
            }



            public static void SalesOrderItemNames_Select(string SalesOrderId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_SO_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }

            public static void SalesOrderForPayments_Select(Control ControlForBind, string CustomerId, string UnitId, string SaveButtonText)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_MAST].CUST_ID=" + CustomerId + " AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + UnitId + " AND [YANTRA_SO_MAST].SO_ID IN (SELECT SO_ID FROM YANTRA_PAYMENTS_RECEIVED WHERE PR_PAYMENT_STATUS <> 'Cleared') AND [YANTRA_SO_MAST].SO_ID IN (SELECT [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SALES_INVOICE_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID) ORDER BY [YANTRA_SO_MAST].SO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SO_NO"].ToString(), dbManager.DataReader["SO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_MAST].CUST_ID=" + CustomerId + " AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + UnitId + " AND [YANTRA_SO_MAST].SO_ID NOT IN (SELECT SO_ID FROM YANTRA_PAYMENTS_RECEIVED) AND [YANTRA_SO_MAST].SO_ID IN (SELECT [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SALES_INVOICE_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID) ORDER BY [YANTRA_SO_MAST].SO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SO_NO"].ToString(), dbManager.DataReader["SO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_ENQ_MAST].CUST_ID=" + CustomerId + " AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=" + UnitId + "  ORDER BY [YANTRA_SO_MAST].SO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                    }
                }
            }
            public int SalesOrderItem_Select(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND" +
                                            " [YANTRA_SO_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_ENQ_MAST].ENQ_REFERENCE='" + SalesOrderId + "' ORDER BY [YANTRA_ENQ_MAST].ENQ_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.SONo = dbManager.DataReader["SO_NO"].ToString();
                    this.SODate = Convert.ToDateTime(dbManager.DataReader["SO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.QuotNo = dbManager.DataReader["QUOT_NO"].ToString();



                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }


            public static void SalesOrder_SelectByCustomerId(Control ControlForBind, string CustomerId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID and [YANTRA_ENQ_MAST].CUST_ID=" + CustomerId + " and YANTRA_SO_MAST.SO_ACCEPTANCE_FLAG !='Obsolete'  ORDER BY [YANTRA_SO_MAST].SO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SO_NO", "SO_ID");
                }
            }



        }

        //Methods For Order Acceptance Form
        public class OrderAcceptance
        {
            public string OAId, OANo, OADate, SOId, SONo, WOId, WONo, SOCSTax, DespmId, QuotNo, QuotId, CustId, OARespId, OASalespId, OAPreparedBy, OACheckedBy, OAApprovedBy, OAFlag, OAAcceptanceFlag, OAConsignee, TransId, OADeliveryDate, OACSTax, OAInspection, OAInvoiceTo;
            public string OADetId, OAItemCode, OADetQty, OARate, OADetSpec, OADetRemarks, OADetPriority;

            public OrderAcceptance()
            {
            }

            public static string OrderAcceptance_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "OA/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(OA_NO,LEFT(OA_NO,5),''))),0)+1 FROM [YANTRA_OA_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(OA_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_OA_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_OA_MAST", "OA_NO");
            }

            public string OrderAcceptance_Save()
            {
                this.OANo = OrderAcceptance_AutoGenCode();
                this.OAId = AutoGenMaxId("[YANTRA_OA_MAST]", "OA_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_OA_MAST] SELECT ISNULL(MAX(OA_ID),0)+1,'{0}','{1}',{2},'{3}','{4}',{5},{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}' FROM [YANTRA_OA_MAST]", this.OANo, this.OADate, this.WOId, this.OARespId, this.OASalespId, this.DespmId, this.TransId, this.OADeliveryDate, this.OAPreparedBy, this.OACheckedBy, this.OAApprovedBy, this.OAFlag, this.OAConsignee, this.OACSTax, this.OAInspection, this.OAInvoiceTo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Order Acceptance Details", "123");

                }
                return _returnStringMessage;
            }

            public string OrderAcceptance_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_OA_MAST] SET OA_DATE='{0}',OA_RESP_ID='{1}',OA_SALESP_ID='{2}',OA_PREPARED_BY='{3}',OA_CHECKED_BY='{4}',OA_APPROVED_BY='{5}',OA_CONSIGNEE='{6}',DESPM_ID={7},TRANS_ID={8},OA_DELIVERY_DATE='{9}',OA_CSTAX='{10}',OA_INSPECTION='{11}',OA_INVOICE_TO='{12}' WHERE OA_ID={13}", this.OADate, this.OARespId, this.OASalespId, this.OAPreparedBy, this.OACheckedBy, this.OAApprovedBy, this.OAConsignee, this.DespmId, this.TransId, this.OADeliveryDate, this.OACSTax, this.OAInspection, this.OAInvoiceTo, this.OAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Order Acceptance Details", "123");

                }
                return _returnStringMessage;
            }

            public string OrderAcceptance_Delete(string OrderAcceptanceId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_OA_DET]", "OA_ID", OrderAcceptanceId) == true)
                {
                    if (DeleteRecord("[YANTRA_OA_MAST]", "OA_ID", OrderAcceptanceId) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Order Acceptance Details", "123");

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public string OrderAcceptanceDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_OA_DET] SELECT ISNULL(MAX(OA_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_OA_DET]", this.OAId, this.OAItemCode, this.OADetQty, this.OARate, this.OADetSpec, this.OADetPriority, this.OADetRemarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Order Acceptance Details", "123");

                }
                return _returnStringMessage;
            }

            public int OrderAcceptanceDetails_Delete(string OrderAcceptanceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_OA_DET] WHERE OA_ID={0}", OrderAcceptanceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int OrderAcceptance_Select(string OrderAcceptanceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_LKUP_DESP_MODE],[YANTRA_ENQ_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST],[YANTRA_WO_MAST],[YANTRA_OA_MAST] WHERE [YANTRA_ENQ_MAST].ENQ_ID = [YANTRA_QUOT_MAST].ENQ_ID " +
                                            " AND [YANTRA_QUOT_MAST].QUOT_ID=[YANTRA_SO_MAST].QUOT_ID AND [YANTRA_SO_MAST].SO_ID=[YANTRA_WO_MAST].SO_ID AND [YANTRA_SO_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID" +
                                            " AND [YANTRA_WO_MAST].WO_ID=[YANTRA_OA_MAST].WO_ID AND [YANTRA_OA_MAST].OA_ID='" + OrderAcceptanceId + "' ORDER BY [YANTRA_OA_MAST].OA_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.OANo = dbManager.DataReader["OA_NO"].ToString();
                    this.OADate = Convert.ToDateTime(dbManager.DataReader["OA_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.SONo = dbManager.DataReader["SO_NO"].ToString();
                    this.WOId = dbManager.DataReader["WO_ID"].ToString();
                    this.WONo = dbManager.DataReader["WO_NO"].ToString();
                    this.SOCSTax = dbManager.DataReader["SO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.QuotNo = dbManager.DataReader["QUOT_NO"].ToString();
                    this.OARespId = dbManager.DataReader["OA_RESP_ID"].ToString();
                    this.OASalespId = dbManager.DataReader["OA_SALESP_ID"].ToString();
                    this.OAPreparedBy = dbManager.DataReader["OA_PREPARED_BY"].ToString();
                    this.OACheckedBy = dbManager.DataReader["OA_CHECKED_BY"].ToString();
                    this.OAApprovedBy = dbManager.DataReader["OA_APPROVED_BY"].ToString();
                    this.OAAcceptanceFlag = dbManager.DataReader["OA_FLAG"].ToString();
                    this.OAConsignee = dbManager.DataReader["OA_CONSIGNEE"].ToString();
                    this.OADeliveryDate = Convert.ToDateTime(dbManager.DataReader["OA_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.OACSTax = dbManager.DataReader["OA_CSTAX"].ToString();
                    this.OAInspection = dbManager.DataReader["OA_INSPECTION"].ToString();
                    this.OAInvoiceTo = dbManager.DataReader["OA_INVOICE_TO"].ToString();
                    this.TransId = dbManager.DataReader["TRANS_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string OrderAcceptanceStatus_Update(SMStatus Status, string OAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_OA_MAST] SET  OA_FLAG='{0}' WHERE OA_ID='{1}'", Status, OAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Order Acceptance Status Details", "123");

                }
                return _returnStringMessage;
            }

            public void OrderAcceptanceDetails_Select(string OrderAcceptanceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_OA_DET] WHERE [YANTRA_OA_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_OA_DET].OA_ID=" + OrderAcceptanceId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable OrderAcceptanceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                OrderAcceptanceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = OrderAcceptanceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["OA_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["OA_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["OA_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["OA_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["OA_DET_PRIORITY"].ToString();

                    OrderAcceptanceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }

            public string OrderAcceptanceApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  OA_FLAG FROM [YANTRA_OA_MAST] WHERE OA_ID='{0}'", this.OAId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_OA_MAST] SET OA_APPROVED_BY={0},OA_FLAG='{1}' WHERE OA_ID='{2}'", this.OAApprovedBy, SMStatus.Closed, this.OAId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Order Acceptance Approve Details", "123");

                }
                return _returnStringMessage;
            }

            public static void OrderAcceptance_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_OA_MAST] ORDER BY OA_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "OA_NO", "OA_ID");
                }
            }
            public static void OrderAcceptance_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_OA_MAST].* 	FROM [YANTRA_ENQ_MAST] inner join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID inner join [YANTRA_QUOT_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID	 inner join [YANTRA_SO_MAST] on [YANTRA_QUOT_MAST].QUOT_ID=[YANTRA_SO_MAST].QUOT_ID inner join [YANTRA_FE_ORDER_PROFILE] on [YANTRA_SO_MAST].SO_ID=[YANTRA_FE_ORDER_PROFILE].SO_ID inner join [YANTRA_OA_MAST] on [YANTRA_OA_MAST].WO_ID=[YANTRA_FE_ORDER_PROFILE].WO_ID 	where [YANTRA_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_OA_MAST].OA_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "OA_NO", "OA_ID");
                }
            }
        }

        //Methods For Work Order Form
        public class WorkOrder
        {
            public string WOId, WONo, WODate, SOId, SoDetId, SONo, DespId, CustId, WOInspDate, WOPackForwInst, WODeliveryDate, WOAccessories, WOExtraSpares, WOPreparedBy, WOCheckedBy, WOApprovedBy, WOFLag, WOFrieght, WORoadPermit, WOFiles, SOAdvanceAmt, WOCSTax, WOTaxLabel, CpId;
            public string WODetId, WOItemCode, WODetQty, WODetSpec, WODetRemarks, ColorId, RespId, AnnexureQty;
            public int ResQty,ReserveGodownId,TotalQty;
            public static string StringForMessage;
           
            public WorkOrder()
            {
            }
           
            public static string WorkOrder_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "WO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(WO_NO,LEFT(WO_NO,5),''))),0)+1 FROM [YANTRA_WO_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(WO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_WO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_WO_MAST", "WO_NO");
            }


            public string WorkOrder_Save()
            {
                this.WONo = WorkOrder_AutoGenCode();
                this.WOId = AutoGenMaxId("[YANTRA_WO_MAST]", "WO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_WO_MAST] SELECT ISNULL(MAX(WO_ID),0)+1,'{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19} FROM [YANTRA_WO_MAST]", this.WONo, this.WODate, this.SOId, this.DespId, this.WOInspDate, this.WOPackForwInst, this.WODeliveryDate, this.WOAccessories, this.WOExtraSpares, this.WOPreparedBy, this.WOCheckedBy, this.WOApprovedBy, "New", this.WOFrieght, this.WORoadPermit, this.WOFiles, this.WOCSTax, this.WOTaxLabel, this.CpId, this.RespId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Work Order Details", "124");

                }
                return _returnStringMessage;
            }

            public string WorkOrder_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_WO_MAST] SET WO_DATE='{0}',DESPM_ID={1},WO_INSP_DATE='{2}',WO_PACK_FORW_INS='{3}',WO_DELIVERY_DATE='{4}',WO_ACCESSORIES='{5}',WO_EXTRA_SPARES='{6}',WO_PREPARED_BY='{7}',WO_CHECKED_BY='{8}',WO_APPROVED_BY='{9}',WO_FRIEGHT='{10}',WO_ROAD_PERMIT='{11}',WO_FILES='{12}',WO_CSTAX='{13}',CP_ID={15} WHERE WO_ID={14}", this.WODate, this.DespId, this.WOInspDate, this.WOPackForwInst, this.WODeliveryDate, this.WOAccessories, this.WOExtraSpares, this.WOPreparedBy, this.WOCheckedBy, this.WOApprovedBy, this.WOFrieght, this.WORoadPermit, this.WOFiles, this.WOCSTax, this.WOId,this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Work Order Details", "124");

                }
                return _returnStringMessage;
            }

            public string WorkOrder_Delete(string WorkOrderId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_WO_DET]", "WO_ID", WorkOrderId) == true)
                {
                    if (DeleteRecord("[YANTRA_WO_MAST]", "WO_ID", WorkOrderId) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Work Order Details", "124");

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public string WorkOrderDetails_Save()
            {


                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("INSERT INTO [YANTRA_WO_DET] SELECT ISNULL(MAX(WO_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','False',{6} FROM [YANTRA_WO_DET]", this.WOId, this.WOItemCode, this.WODetQty, this.WODetSpec, this.WODetRemarks, this.ColorId, this.AnnexureQty);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _commandText = string.Format("update YANTRA_SO_DET set Annexure_Qty= Annexure_Qty+{2} where SO_DET_ID ={0} and ITEM_CODE={1} ", this.SoDetId, this.WOItemCode, this.AnnexureQty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);


                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Work Order Details", "124");

                }
                return _returnStringMessage;
            }

            public string WorkOrderDetailsEdit_Save()
            {


                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("INSERT INTO [YANTRA_WO_DET] SELECT ISNULL(MAX(WO_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','False',{6} FROM [YANTRA_WO_DET]", this.WOId, this.WOItemCode, this.WODetQty, this.WODetSpec, this.WODetRemarks, this.ColorId, this.AnnexureQty);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                //_commandText = string.Format("update YANTRA_SO_DET set Annexure_Qty= Annexure_Qty-{2} where SO_DET_ID ={0} and ITEM_CODE={1} ", this.SoDetId, this.WOItemCode, this.AnnexureQty);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);


                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Work Order Details", "124");

                }
                return _returnStringMessage;
            }

            public string ItemMasterReserveQty_Update(string itcd,int colorId,int qtyFromIO,string modelNo,int PoDetId)
            {

                string status="";
                int AvailableStock=0;
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SO_RES_STATUS from YANTRA_SO_DET where ITEM_CODE={0} and SO_DET_ID={1} and COLOR_ID={2}", itcd, PoDetId,colorId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    status = dbManager.DataReader["SO_RES_STATUS"].ToString();
                }
                dbManager.DataReader.Close();
                this.CpId = cp.getPresentCompanySessionValue();
                if (status == "False")
                {
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("SELECT sum(RES_QTY) as RES_QTY from YANTRA_RESERVE_QTY where ITEM_CODE={0} and COLOUR_ID={1} ", itcd, colorId);
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        if (dbManager.DataReader["RES_QTY"].ToString() != string.Empty )
                        {
                            this.ResQty = int.Parse(dbManager.DataReader["RES_QTY"].ToString());
                        }
                    }
                    //this.ResQty = this.ResQty + qtyFromIO;
                    dbManager.DataReader.Close();
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("SELECT sum(ITEM_QTY_IN_HAND) as ITEM_QTY_IN_HAND from YANTRA_ITEM_QTY where ITEM_CODE={0} and COLOUR_ID={1} ", itcd, colorId);
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        if (dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString() != string.Empty)
                        {
                            this.TotalQty = int.Parse(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());
                        }
                       
                    }
                    AvailableStock = this.TotalQty - this.ResQty;
                    dbManager.DataReader.Close();
                    if (qtyFromIO > AvailableStock)
                    {
                        StringForMessage = "ModelNo:" + modelNo + "    ||   TotalStock:" + this.TotalQty + "    ||    ReservedStock:" + this.ResQty + " <br> ";
                    }
                    return StringForMessage;
                }
                else return "";
            }


          public string ItemMasterReserveQty3_Update(int itcd, int qtyFromIO,int colorId,int soid,int cpid,int godownid)
            {
              
                if (dbManager.Transaction == null)
                    dbManager.Open();
              
                this.CpId = cp.getPresentCompanySessionValue();
               

                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("INSERT INTO [YANTRA_RESERVE_QTY] SELECT ISNULL(MAX(RESERVE_ID),0)+1,{0},{1},{2},{3},{4},{5} FROM [YANTRA_RESERVE_QTY]", itcd, colorId, CpId, qtyFromIO, soid, godownid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                   
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Update("Item Master Reserve Qty Details", "27");

                    }
                    return _returnStringMessage;
                
                
            }

            //public string ItemMasterReserveQty_Update(string itcd, int qtyFromIO,int PoDetId,int colorId)
            //{
            //    string status="";
            //    int AvailableStock=0;
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT SO_RES_STATUS from YANTRA_SO_DET where ITEM_CODE={0} and SO_DET_ID={1} and COLOR_ID={2}", itcd, PoDetId,colorId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        status = dbManager.DataReader["SO_RES_STATUS"].ToString();
            //    }
            //    dbManager.DataReader.Close();
            //    this.CpId = cp.getPresentCompanySessionValue();
            //    if (status == "False")
            //    {
            //        //if (dbManager.Transaction == null)
            //        // dbManager.Open();
            //        // _commandText = string.Format("SELECT sum(RES_QTY) as RES_QTY from YANTRA_RESERVE_QTY where ITEM_CODE={0} and COLOUR_ID={1} ", itcd, colorId );
            //        //dbManager.ExecuteReader(CommandType.Text, _commandText);
            //        //if (dbManager.DataReader.Read())
            //        //{
            //        //    this.ResQty = int.Parse(dbManager.DataReader["RES_QTY"].ToString());
                       
            //        //}
            //        ////this.ResQty = this.ResQty + qtyFromIO;
            //        //dbManager.DataReader.Close();
            //        //if (dbManager.Transaction == null)
            //        //    dbManager.Open();
            //        //_commandText = string.Format("SELECT sum(ITEM_QTY_IN_HAND) as ITEM_QTY_IN_HAND from YANTRA_ITEM_QTY where ITEM_CODE={0} and COLOUR_ID={1} ", itcd, colorId);
            //        //dbManager.ExecuteReader(CommandType.Text, _commandText);
            //        //if (dbManager.DataReader.Read())
            //        //{
            //        //    this.TotalQty = int.Parse(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());

            //        //}
            //        //AvailableStock = this.TotalQty - this.ResQty;
            //        //dbManager.DataReader.Close();
                    
                    
            //        if (dbManager.Transaction == null)
            //            dbManager.Open();
            //        _commandText = string.Format("INSERT INTO [YANTRA_RESERVE_QTY] SELECT ISNULL(MAX(RESERVE_ID),0)+1,{0},{1},{2},{3},{4} FROM [YANTRA_RESERVE_QTY]", itcd, colorId, CpId, qtyFromIO, PoDetId);
            //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //        if (dbManager.Transaction == null)
            //            dbManager.Open();
            //        _commandText = string.Format("UPDATE [YANTRA_SO_DET] SET SO_RES_STATUS='True' where ITEM_CODE={0} and SO_DET_ID={1} and COLOR_ID={2}", itcd, PoDetId,colorId);
            //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //        _returnStringMessage = string.Empty;
            //        if (_returnIntValue < 0 || _returnIntValue == 0)
            //        {
            //            _returnStringMessage = "Some Data Missing.";
            //        }
            //        else if (_returnIntValue > 0)
            //        {
            //            _returnStringMessage = "Data Saved Successfully";
            //        }
            //        return _returnStringMessage;
            //    }
            //    else
            //    {
            //        _returnStringMessage = "Stock Already Reserved";
            //        return _returnStringMessage;
            //    }
            //}

            public int WorkOrderDetails_Delete(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_WO_DET] WHERE WO_ID={0}", WorkOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int WorkOrder_Select(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_QUOT_MAST],[YANTRA_SO_MAST],[YANTRA_WO_MAST] WHERE [YANTRA_ENQ_MAST].ENQ_ID = [YANTRA_QUOT_MAST].ENQ_ID " +
                                            " AND [YANTRA_QUOT_MAST].QUOT_ID=[YANTRA_SO_MAST].QUOT_ID AND [YANTRA_SO_MAST].SO_ID=[YANTRA_WO_MAST].SO_ID " +
                                            " AND [YANTRA_WO_MAST].WO_ID='" + WorkOrderId + "' ORDER BY [YANTRA_WO_MAST].WO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.WOId = dbManager.DataReader["WO_ID"].ToString();
                    this.WONo = dbManager.DataReader["WO_NO"].ToString();
                    this.WODate = Convert.ToDateTime(dbManager.DataReader["WO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.SONo = dbManager.DataReader["SO_No"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DespId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.WOInspDate = Convert.ToDateTime(dbManager.DataReader["WO_INSP_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.WOPackForwInst = dbManager.DataReader["WO_PACK_FORW_INS"].ToString();
                    this.WODeliveryDate = Convert.ToDateTime(dbManager.DataReader["WO_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.WOAccessories = dbManager.DataReader["WO_ACCESSORIES"].ToString();
                    this.WOExtraSpares = dbManager.DataReader["WO_EXTRA_SPARES"].ToString();
                    this.WOPreparedBy = dbManager.DataReader["WO_PREPARED_BY"].ToString();
                    this.WOCheckedBy = dbManager.DataReader["WO_CHECKED_BY"].ToString();
                    this.WOApprovedBy = dbManager.DataReader["WO_APPROVED_BY"].ToString();
                    this.WOFrieght = dbManager.DataReader["WO_FRIEGHT"].ToString();
                    this.WORoadPermit = dbManager.DataReader["WO_ROAD_PERMIT"].ToString();
                    this.WOFiles = dbManager.DataReader["WO_FILES"].ToString();
                    this.WOCSTax = dbManager.DataReader["WO_CSTAX"].ToString();
                    this.WOTaxLabel = dbManager.DataReader["WO_TAXLBL"].ToString();
                    this.SOAdvanceAmt = dbManager.DataReader["SO_ADVANCE_AMT"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public void WorkOrderDetails_Select(string WorkOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_WO_DET] WHERE [YANTRA_WO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_WO_DET].WO_ID=" + WorkOrderId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable OrderAcceptanceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                OrderAcceptanceProducts.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = OrderAcceptanceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["WO_DET_QTY"].ToString();
                    dr["Specifications"] = dbManager.DataReader["WO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["WO_DET_REMARKS"].ToString();


                    OrderAcceptanceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }

            public string WorkOrderApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  WO_FLAG FROM [YANTRA_WO_MAST] WHERE WO_ID='{0}'", this.WOId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_WO_MAST] SET  WO_APPROVED_BY={0},WO_FLAG='{1}' WHERE WO_ID='{2}'", this.WOApprovedBy, SMStatus.Open, this.WOId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Work Order Approve Details", "124");

                }
                return _returnStringMessage;
            }

            public static string WorkOrderStatus_Update(SMStatus Status, string WOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_WO_MAST] SET  WO_FLAG='{0}' WHERE WO_ID='{1}'", Status, WOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Work Order Status Details", "124");

                }
                return _returnStringMessage;
            }

            public static void WorkOrder_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_WO_MAST] WHERE WO_FLAG='Open' ORDER BY WO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "WO_NO", "WO_ID");
                }
            }

            public static void WorkOrder_SelectAll(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_WO_MAST] ORDER BY WO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "WO_NO", "WO_ID");
                }
            }

            public static void WorkOrder_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_WO_MAST].* FROM [YANTRA_ENQ_MAST] inner join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID inner join [YANTRA_QUOT_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_SO_MAST] on [YANTRA_QUOT_MAST].QUOT_ID=[YANTRA_SO_MAST].QUOT_ID inner join [YANTRA_WO_MAST] on [YANTRA_SO_MAST].SO_ID=[YANTRA_WO_MAST].SO_ID where [YANTRA_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_WO_MAST].WO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "WO_NO", "WO_ID");
                }
            }
            public static string GetWorkOrderIdOfSalesOrder(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT WO_ID FROM [YANTRA_WO_MAST] WHERE SO_ID=" + SalesOrderId + " ORDER BY [YANTRA_WO_MAST].WO_ID DESC ");
                _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();

                return _returnStringMessage;
            }

        }

        //Method for Agent Master Form
        public class AgentMaster
        {
            public string AgentId, AgentName, AgentContactPerson, AgentAddress, AgentContactPersonDetails, AgentPhone, AgentMobile, AgentEmail, AgentFaxNo; // Agent Master
            // public string SupDetId, ItemCode, ItemType, UOM;

            public AgentMaster()
            { }

            public string AgentMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_AGENT_MASTER]", "AGENT_NAME", this.AgentName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_AGENT_MASTER] SELECT ISNULL(MAX(AGENT_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' FROM [YANTRA_AGENT_MASTER]", this.AgentName, this.AgentContactPerson, this.AgentAddress, this.AgentContactPersonDetails, this.AgentPhone, this.AgentMobile, this.AgentEmail, this.AgentFaxNo);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Agent Master Details", "125");

                    }
                }
                else
                {
                    _returnStringMessage = "Supplier  Already Exists.";
                }
                return _returnStringMessage;
            }

            public string AgentMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_AGENT_MASTER]", "AGENT_NAME", this.AgentName, "AGENT_ID", this.AgentId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_AGENT_MASTER] SET AGENT_NAME='{0}',AGENT_CONTACT_PERSON='{1}',AGENT_ADDRESS='{2}',AGENT_CONTACT_PER_DET='{3}',AGENT_PHONE='{4}',AGENT_MOBILE='{5}',AGENT_EMAIL='{6}',AGENT_FAXNO='{7}' WHERE AGENT_ID={8}", this.AgentName, this.AgentContactPerson, this.AgentAddress, this.AgentContactPersonDetails, this.AgentPhone, this.AgentMobile, this.AgentEmail, this.AgentFaxNo, this.AgentId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Agent Master Details", "125");

                    }
                }
                else
                {
                    _returnStringMessage = "Supplier Already Exists.";
                }
                return _returnStringMessage;
            }

            public string AgentMaster_Delete()
            {
                if (DeleteRecord("[YANTRA_AGENT_MASTER]", "AGENT_ID", this.AgentId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Agent Master Details", "125");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public static void AgentMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AGENT_MASTER] ORDER BY AGENT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AGENT_NAME", "AGENT_ID");
                }
            }

            public int AgentMaster_Select(string AgentId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_AGENT_MASTER] WHERE AGENT_ID = " + AgentId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AgentId = dbManager.DataReader["AGENT_ID"].ToString();
                    this.AgentName = dbManager.DataReader["AGENT_NAME"].ToString();
                    this.AgentContactPerson = dbManager.DataReader["AGENT_CONTACT_PERSON"].ToString();
                    this.AgentAddress = dbManager.DataReader["AGENT_ADDRESS"].ToString();
                    this.AgentContactPersonDetails = dbManager.DataReader["AGENT_CONTACT_PER_DET"].ToString();
                    this.AgentPhone = dbManager.DataReader["AGENT_PHONE"].ToString();
                    this.AgentMobile = dbManager.DataReader["AGENT_MOBILE"].ToString();
                    this.AgentEmail = dbManager.DataReader["AGENT_EMAIL"].ToString();
                    this.AgentFaxNo = dbManager.DataReader["AGENT_FAXNO"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
        }

        //Methods For Payments Received Form
        public class PaymentsReceived
        {
            public string PRId, PRNo, PRDate, CustId, UnitId, SO_Id, SIId, SIAmt, PRReceivedAmt, PRPaymodeType, PRChequeNo, PRChequeDate, PRCahReceivedDate, PRBankDetails, PRPreparedBy, PRApprovedBy, PRPaymentStatus, SPOId,CpId;

            public string Totalamt;
            public PaymentsReceived()
            {
            }

            public static string PaymentsReceived_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(PR_NO,LEFT(PR_NO,5),''))),0)+1 FROM [YANTRA_PAYMENTS_RECEIVED]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_PAYMENTS_RECEIVED", "PR_NO");
            }
            
            public string PaymentsReceived_Save()
            {
                this.PRNo = PaymentsReceived_AutoGenCode();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_PAYMENTS_RECEIVED] SELECT ISNULL(MAX(PR_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17} FROM [YANTRA_PAYMENTS_RECEIVED]", this.PRNo, this.PRDate, this.CustId, this.UnitId, this.SO_Id, this.SIId, this.SIAmt, this.PRReceivedAmt, this.PRPaymodeType, this.PRChequeNo, this.PRChequeDate, this.PRCahReceivedDate, this.PRBankDetails, this.PRPreparedBy, this.PRApprovedBy, this.PRPaymentStatus, this.SPOId, this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Payments Received Details", "126");

                }
                return _returnStringMessage;
            }

            public string PaymentsReceived_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PAYMENTS_RECEIVED] SET PR_NO='{0}',PR_DATE='{1}',CUST_ID={2},UNIT_ID={3},SI_ID={4},SI_AMOUNT='{5}',PR_AMT_RECEIVED='{6}',PR_PAYMODE_TYPE='{7}',PR_CHEQUE_NO='{8}',PR_CHEQUE_DATE='{9}',PR_CASH_RECEIVED_DATE='{10}',PR_BANK_DETAILS='{11}',PR_PREPARED_BY='{12}',PR_APPROVED_BY='{13}',PR_PAYMENT_STATUS='{14}',SO_ID={15},SPO_ID={16},CP_ID={17}  WHERE PR_ID={18}", this.PRNo, Convert.ToDateTime(this.PRDate), this.CustId, this.UnitId, this.SIId, this.SIAmt, this.PRReceivedAmt, this.PRPaymodeType, this.PRChequeNo, Convert.ToDateTime(this.PRChequeDate), Convert.ToDateTime(this.PRCahReceivedDate), this.PRBankDetails, this.PRPreparedBy, this.PRApprovedBy, this.PRPaymentStatus, this.SO_Id, this.SPOId,this.CpId, this.PRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Payments Received Details", "126");

                }
                return _returnStringMessage;
            }

            public string PaymentsReceived_Delete(string PaymentsReceivedId)
            {
                if (DeleteRecord("[YANTRA_PAYMENTS_RECEIVED]", "PR_ID", PaymentsReceivedId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Payments Received Details", "126");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static void PaymentsReceived_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PAYMENTS_RECEIVED] ORDER BY PR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PR_NO", "PR_ID");
                }
            }

            public int PaymentsReceived_Select(string PaymentsReceivedId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_PAYMENTS_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS],YANTRA_SO_MAST" +
                                                                        " WHERE [YANTRA_PAYMENTS_RECEIVED].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                                                        " AND [YANTRA_PAYMENTS_RECEIVED].UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
                    //" AND [YANTRA_PAYMENTS_RECEIVED].SI_ID=[YANTRA_SALES_INVOICE_MAST].SI_ID  " +
                                                                         " AND [YANTRA_PAYMENTS_RECEIVED].SO_ID=[YANTRA_SO_MAST].SO_ID  " +
                                                                        " AND [YANTRA_PAYMENTS_RECEIVED].PR_ID='" + PaymentsReceivedId + "' ORDER BY [YANTRA_PAYMENTS_RECEIVED].PR_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PRId = dbManager.DataReader["PR_ID"].ToString();
                    this.PRNo = dbManager.DataReader["PR_NO"].ToString();
                    this.PRDate = Convert.ToDateTime(dbManager.DataReader["PR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.UnitId = dbManager.DataReader["UNIT_ID"].ToString();
                    this.SO_Id = dbManager.DataReader["SO_ID"].ToString();
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SIId = dbManager.DataReader["SI_ID"].ToString();
                    this.SIAmt = dbManager.DataReader["SI_AMOUNT"].ToString();
                    this.PRReceivedAmt = dbManager.DataReader["PR_AMT_RECEIVED"].ToString();
                    this.PRPaymodeType = dbManager.DataReader["PR_PAYMODE_TYPE"].ToString();
                    this.PRChequeNo = dbManager.DataReader["PR_CHEQUE_NO"].ToString();
                    this.PRChequeDate = Convert.ToDateTime(dbManager.DataReader["PR_CHEQUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PRCahReceivedDate = Convert.ToDateTime(dbManager.DataReader["PR_CASH_RECEIVED_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PRBankDetails = dbManager.DataReader["PR_BANK_DETAILS"].ToString();
                    this.PRPreparedBy = dbManager.DataReader["PR_PREPARED_BY"].ToString();
                    this.PRApprovedBy = dbManager.DataReader["PR_APPROVED_BY"].ToString();
                    this.PRPaymentStatus = dbManager.DataReader["PR_PAYMENT_STATUS"].ToString();
                    this.Totalamt = dbManager.DataReader["SO_TOTAL_AMT"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }


            public void ExistingPaymentsReceived_Select(GridView gv, string SalesInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_PAYMENTS_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS],YANTRA_SO_MAST" +
                                                                        " WHERE [YANTRA_PAYMENTS_RECEIVED].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                                                        " AND [YANTRA_PAYMENTS_RECEIVED].UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
                    //" AND [YANTRA_PAYMENTS_RECEIVED].SI_ID=[YANTRA_SALES_INVOICE_MAST].SI_ID  " +
                                                                        " AND [YANTRA_PAYMENTS_RECEIVED].SO_ID=[YANTRA_SO_MAST].SO_ID  " +
                                                                        " AND [YANTRA_CUSTOMER_MAST].cust_id='" + SalesInvoiceId + "' ORDER BY [YANTRA_PAYMENTS_RECEIVED].PR_ID DESC");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                gv.DataSource = dbManager.DataReader;
                gv.DataBind();
                dbManager.DataReader.Close();

            }
            public void ExistingPaymentsReceivedPO_Select(GridView gv, string SalesInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_PAYMENTS_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS],YANTRA_SO_MAST" +
                                                                        " WHERE [YANTRA_PAYMENTS_RECEIVED].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                                                        " AND [YANTRA_PAYMENTS_RECEIVED].UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
                    //" AND [YANTRA_PAYMENTS_RECEIVED].SI_ID=[YANTRA_SALES_INVOICE_MAST].SI_ID  " +
                                                                        " AND [YANTRA_PAYMENTS_RECEIVED].SO_ID=[YANTRA_SO_MAST].SO_ID  " +
                                                                        " AND [YANTRA_SO_MAST].SO_ID='" + SalesInvoiceId + "' ORDER BY [YANTRA_PAYMENTS_RECEIVED].PR_ID DESC");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                gv.DataSource = dbManager.DataReader;
                gv.DataBind();
                dbManager.DataReader.Close();

            }

            public string PaymentsStatus_Update(string PaymentStatus, string SOID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PAYMENTS_RECEIVED] SET PR_PAYMENT_STATUS='{0}' WHERE SO_ID={1}", PaymentStatus, SOID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Payments Received Status Details", "126");

                }
                return _returnStringMessage;
            }

            public string PaymentsStatus_Update(string PaymentStatus, string SOID, string CustId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PAYMENTS_RECEIVED] SET PR_PAYMENT_STATUS='{0}' WHERE SO_ID={1} and CUST_ID={2}", PaymentStatus, SOID, CustId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Payments Received Status Details", "126");

                }
                return _returnStringMessage;
            }
        }

        //Methods For EMDS Received Form
        public class EMDSReceived
        {
            public string EMDRId, EMDRNo, EMDRDate, EnqRef, EnqId, EnqEMDCharges, EMDRAmtReceived, EMDRPaymodeType, EMDRChequeNo, EMDRChequeDate, EMDRCahReceivedDate, EMDRBankDetails, EMDRPreparedBy, EMDRApprovedBy, EMDRRemarks, EmdrStatus;
            public string CustId, CustUnitId, CustDetId;

            public EMDSReceived()
            {
            }

            public static string EMDSReceived_AutoGenCode()
            {

                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SI/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(EMDR_NO,LEFT(EMDR_NO,5),''))),0)+1 FROM [YANTRA_EMDS_RECEIVED]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_EMDS_RECEIVED", "EMDR_NO");

            }

            public string EMDSReceived_Save()
            {
                this.EMDRNo = EMDSReceived_AutoGenCode();
                this.EMDRId = AutoGenMaxId("[YANTRA_EMDS_RECEIVED]", "EMDR_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_EMDS_RECEIVED] SELECT ISNULL(MAX(EMDR_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}' FROM [YANTRA_EMDS_RECEIVED]", this.EMDRNo, this.EMDRDate, this.EnqRef, this.EnqId, this.EnqEMDCharges, this.EMDRAmtReceived, this.EMDRPaymodeType, this.EMDRChequeNo, this.EMDRChequeDate, this.EMDRCahReceivedDate, this.EMDRBankDetails, this.EMDRPreparedBy, this.EMDRApprovedBy, this.EMDRRemarks, this.EmdrStatus);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("EMDS Received Details", "127");

                }
                return _returnStringMessage;
            }

            public string EMDSReceived_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_EMDS_RECEIVED] SET EMDR_NO='{0}',EMDR_DATE='{1}',ENQ_REFERENCE='{2}',ENQ_EMD_CHARGES='{3}',EMDR_AMT_RECEIVED='{4}',EMDR_PAYMODE_TYPE='{5}',EMDR_CHEQUE_NO='{6}',EMDR_CHEQUE_DATE='{7}',EMDR_CASH_RECEIVED_DATE='{8}',EMDR_BANK_DETAILS='{9}',EMDR_PREPARED_BY='{10}',EMDR_APPROVED_BY='{11}',EMDR_REMARKS='{12}',EMDR_STATUS='{13}' WHERE EMDR_ID={14}", this.EMDRNo, this.EMDRDate, this.EnqRef, this.EnqEMDCharges, this.EMDRAmtReceived, this.EMDRPaymodeType, this.EMDRChequeNo, this.EMDRChequeDate, this.EMDRCahReceivedDate, this.EMDRBankDetails, this.EMDRPreparedBy, this.EMDRApprovedBy, this.EMDRRemarks, this.EmdrStatus, this.EMDRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("EMDS Received Details", "127");

                }
                return _returnStringMessage;
            }

            public string EMDSReceived_Delete(string EMDSReceivedId)
            {
                if (DeleteRecord("[YANTRA_EMDS_RECEIVED]", "EMDR_ID", EMDSReceivedId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("EMDS Received Details", "127");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static void EMDSReceived_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_EMDS_RECEIVED] ORDER BY EMDR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "EMDR_NO", "EMDR_ID");
                }
            }

            public int EMDSReceived_Select(string EMDSReceivedId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_EMDS_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_UNITS],[YANTRA_CUSTOMER_DET] WHERE [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_EMDS_RECEIVED].ENQ_ID  AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID  AND [YANTRA_ENQ_MAST].CUST_DET_ID=[YANTRA_CUSTOMER_DET].CUST_DET_ID  AND [YANTRA_ENQ_MAST].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID   AND [YANTRA_EMDS_RECEIVED].EMDR_ID='" + EMDSReceivedId + "' ORDER BY [YANTRA_EMDS_RECEIVED].EMDR_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EMDRId = dbManager.DataReader["EMDR_ID"].ToString();
                    this.EMDRNo = dbManager.DataReader["EMDR_NO"].ToString();
                    this.EMDRDate = Convert.ToDateTime(dbManager.DataReader["EMDR_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.EnqRef = dbManager.DataReader["ENQ_REFERENCE"].ToString();
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.EnqEMDCharges = dbManager.DataReader["ENQ_EMD_CHARGES"].ToString();
                    this.EMDRAmtReceived = dbManager.DataReader["EMDR_AMT_RECEIVED"].ToString();
                    this.EMDRPaymodeType = dbManager.DataReader["EMDR_PAYMODE_TYPE"].ToString();
                    this.EMDRChequeNo = dbManager.DataReader["EMDR_CHEQUE_NO"].ToString();
                    this.EMDRChequeDate = Convert.ToDateTime(dbManager.DataReader["EMDR_CHEQUE_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.EMDRCahReceivedDate = Convert.ToDateTime(dbManager.DataReader["EMDR_CASH_RECEIVED_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.EMDRBankDetails = dbManager.DataReader["EMDR_BANK_DETAILS"].ToString();
                    this.EMDRPreparedBy = dbManager.DataReader["EMDR_PREPARED_BY"].ToString();
                    this.EMDRApprovedBy = dbManager.DataReader["EMDR_APPROVED_BY"].ToString();
                    this.EMDRRemarks = dbManager.DataReader["EMDR_REMARKS"].ToString();
                    this.EmdrStatus = dbManager.DataReader["EMDR_STATUS"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public void ExistingEMDSReceived_Select(GridView gv, string SalesEnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_EMDS_RECEIVED],[YANTRA_ENQ_MAST] WHERE [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_EMDS_RECEIVED].ENQ_ID AND [YANTRA_EMDS_RECEIVED].ENQ_ID='" + SalesEnquiryId + "' ORDER BY [YANTRA_EMDS_RECEIVED].EMDR_ID DESC");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                gv.DataSource = dbManager.DataReader;
                gv.DataBind();
                dbManager.DataReader.Close();

            }

            public string EMDSStatus_Update(string EMDSStatus, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_EMDS_RECEIVED] SET EMDR_STATUS='{0}' WHERE ENQ_ID={1}", EMDSStatus, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("EMDS Received Status Details", "127");

                }
                return _returnStringMessage;
            }
        }

        //Methods For Claim Form
        public class ClaimForm
        {



            public string CfId, CfNo, CfDate, SupId, SupNo, CustId, CustUnitId, CustDetId,
                CfPoRefNo, CfFOBDocCharges, CfTotalExworksFOB, CfCifCharges, CfTotalCifCharges, CfTranferCharges,
                CfTotalValue, CfClaimValueUsd, CURRENCYID, CfCurValueAsPerDay, CfIrs, CfConsigneeBillingAddress,
                CfPayment, CfAccountNo, CfDelivery, CfSwift, CfDeliveryInstructions, CITYID, Preparedby, Approvedby, CfPoRefDate,CpId;

            public string CFProdDetId, ItemCode, CFProdDetQty, CFProdDetCurrency, CFProdDetUnitPrice;
            public string CFTpDetId, CFItemCode, CFTpDetValue, CFTpDetClaimValue;
            public string InsCompany, InsContactperson, InsAddress, InsTelephone;

            public ClaimForm()
            {
            }
            public static string ClaimForm_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(CF_NO,LEFT(CF_NO,5),''))),0)+1 FROM [YANTRA_CLAIM_FORM]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_CLAIM_FORM", "CF_NO");
            }
            public string ClaimForm_Save()
            {
                this.CfNo = ClaimForm_AutoGenCode();
                //NEED TO DEVELOP THIS 'PO REF NO' WITH 'FE ORDER PROFILE' ( AUTO GENERATE NO TO BE FILLED IN BOTH FORMS WITH OUT REPETATIONS)
                this.CfId = AutoGenMaxId("[YANTRA_CLAIM_FORM]", "CF_ID");

                //////////////////////////////////////////
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CLAIM_FORM] SELECT ISNULL(MAX(CF_ID),0)+1,'{0}','{1}',{2},{3},{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}',{23},'{24}','{25}','{26}',{27},'{28}','{29}','{30}',{31} FROM [YANTRA_CLAIM_FORM]",
                    this.CfNo, this.CfDate, this.SupId, this.CustId, this.CustUnitId, this.CustDetId, this.CfPoRefNo, this.CfFOBDocCharges, this.CfTotalExworksFOB, this.CfCifCharges, this.CfTotalCifCharges, this.CfTranferCharges, this.CfTotalValue, this.CfClaimValueUsd, this.CURRENCYID, this.CfCurValueAsPerDay, this.CfIrs, this.CfConsigneeBillingAddress, this.CfPayment, this.CfAccountNo, this.CfDelivery, this.CfSwift, this.CfDeliveryInstructions, this.CITYID, this.Preparedby, this.Approvedby, this.CfPoRefDate,this.InsCompany,this.InsContactperson,this.InsTelephone,this.InsAddress,this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Claim Form Details", "128");

                }
                return _returnStringMessage;
            }

            public string ClaimForm_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_CLAIM_FORM] SET CF_DATE='{0}',CUST_ID={1},CF_PO_REF_NO='{2}',CF_FOB_DOC_CHARGES='{3}',CF_TOTAL_EXWORKS_FOB='{4}',CF_CIF_CHARGES='{5}',CF_TOTAL_CIF_CHARGES='{6}',CF_TRANSFER_CHARGES='{7}',CF_TOTAL_VALUE='{8}',CF_CLAIM_VALUE_USD='{9}',CURRENCY_ID={10},CF_CUR_VALUE_AS_PER_DAY='{11}',CF_IRS='{12}',CF_CONSIGNEE_BILLING_ADDRESS='{13}',CF_PAYMENT='{14}',CF_ACCOUNT_NO='{15}',CF_DELIVERY='{16}',CF_SWIFT='{17}',CF_DELIVERY_INSTRUCTIONS='{18}',  CITY_ID={19},CF_PREPARED_BY='{20}',CF_APPROVED_BY='{21}',CF_PO_REF_DATE='{22}',SUP_ID={23},CF_INSURANCE_ID = {24},CF_INSCONTACT_PERSON='{25}',CF_INSPHONE ='{26}',CF_INSADDRESS = '{27}' WHERE CF_ID={28}",
                    this.CfDate, this.CustId, this.CfPoRefNo, this.CfFOBDocCharges, this.CfTotalExworksFOB, this.CfCifCharges, this.CfTotalCifCharges, this.CfTranferCharges, this.CfTotalValue, this.CfClaimValueUsd, this.CURRENCYID, this.CfCurValueAsPerDay, this.CfIrs, this.CfConsigneeBillingAddress, this.CfPayment, this.CfAccountNo, this.CfDelivery, this.CfSwift, this.CfDeliveryInstructions, this.CITYID, this.Preparedby, this.Approvedby, this.CfPoRefDate, this.SupId,this.InsCompany,this.InsContactperson,this.InsTelephone,this.InsAddress, this.CfId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Claim Form Details", "128");

                }
                return _returnStringMessage;
            }

            public string ClaimForm_Delete(string ClaimFormId)
            {
                if (DeleteRecord("[YANTRA_CLAIM_FORM]", "CF_ID", ClaimFormId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Claim Form Details", "128");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }
            public static void ClaimForm_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CLAIM_FORM] ORDER BY CF_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CF_NO", "CF_ID");
                }
            }
            public int ClaimForm_Select(string ClaimFormId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_CLAIM_FORM] inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_CLAIM_FORM].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
" inner join [YANTRA_CUSTOMER_UNITS] on [YANTRA_CLAIM_FORM].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
" inner join [YANTRA_CUSTOMER_DET] on [YANTRA_CLAIM_FORM].CUST_DET_ID=[YANTRA_CUSTOMER_DET].CUST_DET_ID " +
" left outer join [YANTRA_LKUP_CURRENCY_TYPE] on [YANTRA_CLAIM_FORM].CURRENCY_ID=[YANTRA_LKUP_CURRENCY_TYPE].CURRENCY_ID " +
" left outer join [YANTRA_LKUP_CITY_MAST] on [YANTRA_CLAIM_FORM].CITY_ID=[YANTRA_LKUP_CITY_MAST].CITY_ID " +
" left outer join [YANTRA_SUPPLIER_MAST] on [YANTRA_CLAIM_FORM].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID " +
" left outer join [Insurance_Master] on [YANTRA_CLAIM_FORM].CF_INSURANCE_ID=[Insurance_Master].Insurance_Master_id " +
" where [YANTRA_CLAIM_FORM].CF_ID='" + ClaimFormId + "' ORDER BY [YANTRA_CLAIM_FORM].CF_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CfId = dbManager.DataReader["CF_ID"].ToString();
                    this.CfNo = dbManager.DataReader["CF_NO"].ToString();
                    this.CfDate = Convert.ToDateTime(dbManager.DataReader["CF_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.CfPoRefNo = dbManager.DataReader["CF_PO_REF_NO"].ToString();
                    this.CfFOBDocCharges = dbManager.DataReader["CF_FOB_DOC_CHARGES"].ToString();
                    this.CfTotalExworksFOB = dbManager.DataReader["CF_TOTAL_EXWORKS_FOB"].ToString();
                    this.CfCifCharges = dbManager.DataReader["CF_CIF_CHARGES"].ToString();
                    this.CfTotalCifCharges = dbManager.DataReader["CF_TOTAL_CIF_CHARGES"].ToString();
                    this.CfTranferCharges = dbManager.DataReader["CF_TRANSFER_CHARGES"].ToString();
                    this.CfTotalValue = dbManager.DataReader["CF_TOTAL_VALUE"].ToString();
                    this.CfClaimValueUsd = dbManager.DataReader["CF_CLAIM_VALUE_USD"].ToString();
                    this.CURRENCYID = dbManager.DataReader["CURRENCY_ID"].ToString();
                    this.CfCurValueAsPerDay = dbManager.DataReader["CF_CUR_VALUE_AS_PER_DAY"].ToString();
                    this.CfIrs = dbManager.DataReader["CF_IRS"].ToString();
                    this.CfConsigneeBillingAddress = dbManager.DataReader["CF_CONSIGNEE_BILLING_ADDRESS"].ToString();
                    this.CfPayment = dbManager.DataReader["CF_PAYMENT"].ToString();
                    this.CfAccountNo = dbManager.DataReader["CF_ACCOUNT_NO"].ToString();
                    this.CfDelivery = dbManager.DataReader["CF_DELIVERY"].ToString();
                    this.CfSwift = dbManager.DataReader["CF_SWIFT"].ToString();
                    this.CfDeliveryInstructions = dbManager.DataReader["CF_DELIVERY_INSTRUCTIONS"].ToString();
                    this.Preparedby = dbManager.DataReader["CF_PREPARED_BY"].ToString();
                    this.Approvedby = dbManager.DataReader["CF_APPROVED_BY"].ToString();
                    this.CITYID = dbManager.DataReader["CITY_ID"].ToString();
                    this.CfPoRefDate = Convert.ToDateTime(dbManager.DataReader["CF_PO_REF_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.InsAddress = dbManager.DataReader["CF_INSADDRESS"].ToString();
                    this.InsCompany = dbManager.DataReader["CF_INSURANCE_ID"].ToString();
                    this.InsContactperson = dbManager.DataReader["CF_INSCONTACT_PERSON"].ToString();
                    this.InsTelephone = dbManager.DataReader["CF_INSPHONE"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string GetClaimFormId(string ClaimFormNo)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT CF_ID FROM [YANTRA_CLAIM_FORM] WHERE CF_NO='" + ClaimFormNo + "'");
                _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
                return _returnStringMessage;
            }

            public string ClaimFormApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_CLAIM_FORM] SET CF_APPROVED_BY={0} WHERE CF_ID={1}", this.Approvedby, this.CfId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Claim Form Status Details", "128");

                }
                return _returnStringMessage;
            }
            public string Detremarks;
            public string ClaimFormDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CLAIM_FORM_PROD_DET] SELECT ISNULL(MAX(CF_PROD_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}' FROM [YANTRA_CLAIM_FORM_PROD_DET]",
                                                                                                                              this.CfId, this.ItemCode, this.CFProdDetQty, this.CFProdDetCurrency, this.CFProdDetUnitPrice,this.Detremarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Claim Form Details", "128");

                }
                return _returnStringMessage;
            }
            public int ClaimFormDetails_Delete(string ClaimFormId)
            {
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_commandText = string.Format("DELETE FROM [YANTRA_CLAIM_FORM_PROD_DET] WHERE CF_ID={0}", ClaimFormId);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //return _returnIntValue;
                if (DeleteRecord("[YANTRA_CLAIM_FORM_PROD_DET]", "CF_ID", ClaimFormId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }
            public void ClaimFormDetails_Select(string ClaimFormId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_CURRENCY_TYPE],[YANTRA_CLAIM_FORM_PROD_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_CLAIM_FORM_PROD_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_LKUP_CURRENCY_TYPE].CURRENCY_ID=[YANTRA_CLAIM_FORM_PROD_DET].CF_PROD_DET_CURRENCY AND [YANTRA_CLAIM_FORM_PROD_DET].CF_ID=" + ClaimFormId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable ClaimFormProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("CurrencyName");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("UnitPrice");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("CFProdDetId");
                ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                ClaimFormProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = ClaimFormProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["Quantity"] = dbManager.DataReader["CF_PROD_DET_QTY"].ToString();
                    dr["Currency"] = dbManager.DataReader["CF_PROD_DET_CURRENCY"].ToString();
                    dr["CurrencyName"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["UnitPrice"] = dbManager.DataReader["CF_PROD_DET_UNIT_PRICE"].ToString();
                    dr["CFProdDetId"] = dbManager.DataReader["CF_PROD_DET_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["CF_PROD_DET_REMARKS"].ToString();
                    ClaimFormProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = ClaimFormProducts;
                gv.DataBind();
            }

            public string ClaimTransferFormDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET] SELECT ISNULL(MAX(CF_TP_DET_ID),0)+1,{0},{1},'{2}','{3}' FROM [YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET]",
                                                                                                                              this.CfId, this.CFItemCode, this.CFTpDetValue, this.CFTpDetClaimValue);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Claim Transfer Details", "129");

                }
                return _returnStringMessage;
            }
            public int ClaimTransferFormDetails_Delete(string ClaimFormId)
            {
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_commandText = string.Format("DELETE FROM [YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET] WHERE CF_ID={0}", ClaimFormId);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //return _returnIntValue;
                if (DeleteRecord("[YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET]", "CF_ID", ClaimFormId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }
            public void ClaimTransferFormDetails_Select(string ClaimFormId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_CLAIM_FORM_TRANSFER_PRICE_DET].CF_ID=" + ClaimFormId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable ClaimFormTransferProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                ClaimFormTransferProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                ClaimFormTransferProducts.Columns.Add(col);
                col = new DataColumn("Value");
                ClaimFormTransferProducts.Columns.Add(col);
                col = new DataColumn("CFTpDetId");
                ClaimFormTransferProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = ClaimFormTransferProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["Value"] = dbManager.DataReader["CF_TP_DET_VALUE"].ToString();
                    dr["CFTpDetId"] = dbManager.DataReader["CF_TP_DET_ID"].ToString();

                    ClaimFormTransferProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = ClaimFormTransferProducts;
                gv.DataBind();
            }


        }

        //Methods For FE Order Profile Form
        public class FEOrderProfile
        {
            public string FEOPId, FEOPNo, FEOPDate, FEOPDocRefNo, CustId, CustUnitId, CustDetId, FEOPPONo, FEOPPODate, FEOPMarketing, FEOPRegion, FEOPTerritory, FEOPDivision, FEOPMarketSegment, FEOPOrders, FEOPForwarder, FEOPPortofLanding, FEOPFOBCharges, FEOPCIFCharges, FEOPDespatchMode, FEOPECCNo, FEOPCSTNo, FEOPLSTNo, FEOPTINNo, FEOPFrieghtCharges, FEOPOctroi, FEOPInsurance, FEOPPatrshipment, FEOPRoadPermitReq, FEOPARE1Trans, FEOPEPCGTrans, FEOPDocEnclosed, FEOPDocNo, FEOPTotItemsValue, FEOPTotDisValue, FEOPTotExWorksValue, FEOPPacking, FEOPExciseDuty, FEOPEduCess, FEOPSecEduCess, FEOPCST, FEOPStampingCharges, FEOPTotalValue, FEOPDeliveryDate, FEOPWarrantyPeriod, FEOPPaymentTerms, FEOPAdvRecdDetails, FEOPChequeDD, FEOPPostalAddress, FEOPContactPerson, FEOPTelNo, FEOPMobileNo, FEOPSplInstr, FEOPOrderBookedBy, FEOPApprovedBy, FEOPName, FEOPSignature, FEOPPerDisc, FEPortOfDestination, EnqId, FEOPDespatchConsignee;
            public string ProdDetId, ItemCode, ProdDetQty, ProdDetCurrency, ProdDetUnitPrice, ProdDetTotalPrice;

            public string BuyerDetId, BuyerDetBuyerType, BuyerDetContactPerson, BuyerDetDesig, BuyerDetTelNo, BuyerDetMobileNo;


            public FEOrderProfile()
            {
            }

            public static string FEOrderProfile_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "WO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(FEOP_NO,LEFT(FEOP_NO,5),''))),0)+1 FROM [YANTRA_FE_ORDER_PROFILE]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(WO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_WO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                //return AutoGenMaxNo("YANTRA_FE_ORDER_PROFILE", "FEOP_NO");
                return AutoGenMaxNo("YANTRA_CLAIM_FORM", "CF_NO");
            }

            public string FEOrderProfile_Save()
            {
                this.FEOPNo = FEOrderProfile_AutoGenCode();
                this.FEOPId = AutoGenMaxId("[YANTRA_FE_ORDER_PROFILE]", "FEOP_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_FE_ORDER_PROFILE] SELECT ISNULL(MAX(FEOP_ID),0)+1,'{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}' FROM [YANTRA_FE_ORDER_PROFILE]", this.FEOPNo, this.FEOPDate, this.CustId, this.CustUnitId, this.CustDetId, this.FEOPPONo, this.FEOPPODate, this.FEOPMarketing, this.FEOPRegion, this.FEOPTerritory, this.FEOPDivision, this.FEOPMarketSegment, this.FEOPOrders, this.FEOPForwarder, this.FEOPPortofLanding, this.FEOPFOBCharges, this.FEOPCIFCharges, this.FEOPDespatchMode, this.FEOPECCNo, this.FEOPCSTNo, this.FEOPLSTNo, this.FEOPTINNo, this.FEOPFrieghtCharges, this.FEOPOctroi, this.FEOPInsurance, this.FEOPPatrshipment, this.FEOPRoadPermitReq, this.FEOPARE1Trans, this.FEOPEPCGTrans, this.FEOPDocEnclosed, this.FEOPDocNo, this.FEOPTotItemsValue, this.FEOPTotDisValue, this.FEOPTotExWorksValue, this.FEOPPacking, this.FEOPExciseDuty, this.FEOPEduCess, this.FEOPSecEduCess, this.FEOPCST, this.FEOPStampingCharges, this.FEOPTotalValue, this.FEOPDeliveryDate, this.FEOPWarrantyPeriod, this.FEOPPaymentTerms, this.FEOPAdvRecdDetails, this.FEOPChequeDD, this.FEOPPostalAddress, this.FEOPContactPerson, this.FEOPTelNo, this.FEOPMobileNo, this.FEOPSplInstr, this.FEOPOrderBookedBy, this.FEOPApprovedBy, this.FEOPName, this.FEOPSignature, this.FEOPPerDisc, this.FEOPDocRefNo, this.FEPortOfDestination, this.FEOPDespatchConsignee);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _commandText = "INSERT INTO [YANTRA_CLAIM_FORM] (CF_ID,CF_NO,CF_DATE,CUST_ID,CUST_UNIT_ID,CUST_DET_ID,CF_PO_REF_NO,CF_PO_REF_DATE,SUP_ID,CITY_ID,CURRENCY_ID,CF_PREPARED_BY,CF_APPROVED_BY) VALUES(" + AutoGenMaxId("[YANTRA_CLAIM_FORM]", "CF_ID") + ",'" + this.FEOPNo + "','" + this.FEOPDate + "'," + this.CustId + "," + this.CustUnitId + "," + this.CustDetId + ",'" + this.FEOPPONo + "','" + this.FEOPPODate + "',0,0,0,0,0)";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("FE Order Profile Details", "130");

                }
                return _returnStringMessage;
            }

            public string FEOrderProfile_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FE_ORDER_PROFILE] SET FEOP_DATE='{0}',CUST_ID={1},CUST_UNIT_ID={2},CUST_DET_ID={3},FEOP_PO_NO='{4}',FEOP__PO_DATE='{5}',FEOP_MARKETING='{6}',FEOP_REGION='{7}',FEOP_TERRITORY='{8}',FEOP_DIVISION='{9}',FEOP_MARKET_SEGMENT='{10}',FEOP_ORDERS='{11}',FEOP_FORWARDER='{12}',FEOP_PORT_OF_LANDING='{13}',FEOP_FOB_CHARGES='{14}',FEOP_CIF_CHARGES='{15}',FEOP_DESPATCH_MODE='{16}',FEOP_ECC_NO='{17}',FEOP_CST_NO='{18}',FEOP_LST_NO='{19}',FEOP_TIN_NO='{20}',FEOP_FRIEGHT_CHARGES='{21}',FEOP_OCTROI='{22}',FEOP_INSURANCE='{23}',FEOP_PARTSHIPMENT='{24}',FEOP_ROAD_PERMIT_REQUIRED='{25}',FEOP_ARE1_TRANSACTION='{26}',FEOP_EPCG_TRANSACTION='{27}',FEOP_DOCUMENT_ENCLOSED='{28}',FEOP_DOCUMENT_NO='{29}',FEOP_TOTAL_ITEMS_VALUE='{30}',FEOP_DISCOUNTED_VALUE='{31}',FEOP_EXWORKS_VALUE='{32}',FEOP_PACKING_FORWARDING='{33}',FEOP_EXCISE_DUTY='{34}',FEOP_EDU_CESS='{35}',FEOP_SEC_EDU_CESS='{36}',FEOP_CST='{37}',FEOP_STAMPING_CHARGES='{38}',FEOP_TOTAL_VALUE='{39}',FEOP_DELIVERY_DATE='{40}',FEOP_WARRANTY_PERIOD='{41}',FEOP_PAYMENT_TERMS='{42}',FEOP_ADV_RECD_DETAILS='{43}',FEOP_CHEQUE_DD_EPAYMENT='{44}',FEOP_FULL_POSTAL_ADDRESS='{45}',FEOP_CONTACT_PERSON='{46}',FEOP_TELNO_DIRECT='{47}',FEOP_MOBILE_NO='{48}',FEOP_SPL_INSTRUCTIONS='{49}',FEOP_ORDER_BOOKED_BY='{50}',FEOP_APPROVED_BY='{51}',FEOP_NAME='{52}',FEOP_SIGNATURE='{53}',FEOP_PERCENTAGE_DISCOUNT='{54}',FEOP_DOC_REF_NO='{55}',FEOP_PORT_OF_DESTINATION='{56}',FEOP_DESPATCH_CONSIGNEE='{57}' WHERE FEOP_ID={58}", this.FEOPDate, this.CustId, this.CustUnitId, this.CustDetId, this.FEOPPONo, this.FEOPPODate, this.FEOPMarketing, this.FEOPRegion, this.FEOPTerritory, this.FEOPDivision, this.FEOPMarketSegment, this.FEOPOrders, this.FEOPForwarder, this.FEOPPortofLanding, this.FEOPFOBCharges, this.FEOPCIFCharges, this.FEOPDespatchMode, this.FEOPECCNo, this.FEOPCSTNo, this.FEOPLSTNo, this.FEOPTINNo, this.FEOPFrieghtCharges, this.FEOPOctroi, this.FEOPInsurance, this.FEOPPatrshipment, this.FEOPRoadPermitReq, this.FEOPARE1Trans, this.FEOPEPCGTrans, this.FEOPDocEnclosed, this.FEOPDocNo, this.FEOPTotItemsValue, this.FEOPTotDisValue, this.FEOPTotExWorksValue, this.FEOPPacking, this.FEOPExciseDuty, this.FEOPEduCess, this.FEOPSecEduCess, this.FEOPCST, this.FEOPStampingCharges, this.FEOPTotalValue, this.FEOPDeliveryDate, this.FEOPWarrantyPeriod, this.FEOPPaymentTerms, this.FEOPAdvRecdDetails, this.FEOPChequeDD, this.FEOPPostalAddress, this.FEOPContactPerson, this.FEOPTelNo, this.FEOPMobileNo, this.FEOPSplInstr, this.FEOPOrderBookedBy, this.FEOPApprovedBy, this.FEOPName, this.FEOPSignature, this.FEOPPerDisc, this.FEOPDocRefNo, this.FEPortOfDestination, this.FEOPDespatchConsignee, this.FEOPId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("FE Order Profile Details", "130");

                }
                return _returnStringMessage;
            }

            public string FEOrderProfileApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [YANTRA_FE_ORDER_PROFILE] SET FEOP_APPROVED_BY={0} WHERE FEOP_ID={1}", this.FEOPApprovedBy, this.FEOPId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                }
                return _returnStringMessage;
            }


            public string FEOrderProfile_Delete(string FEOrderProfileId)
            {
                SM.BeginTransaction();

                if (DeleteRecord("[YANTRA_FE_ORDER_PROFILE]", "FEOP_ID", FEOrderProfileId) == true)
                {
                    SM.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("FE Order Profile Details", "130");

                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;



            }

            public int FEOrderProfile_Select(string FEOrderProfileId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FE_ORDER_PROFILE],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS],[YANTRA_CUSTOMER_DET] WHERE [YANTRA_FE_ORDER_PROFILE].CUST_ID = [YANTRA_CUSTOMER_MAST].CUST_ID " +
                                            " AND [YANTRA_FE_ORDER_PROFILE].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND [YANTRA_FE_ORDER_PROFILE].CUST_DET_ID=[YANTRA_CUSTOMER_DET].CUST_DET_ID " +
                                            " AND [YANTRA_FE_ORDER_PROFILE].FEOP_ID='" + FEOrderProfileId + "' ORDER BY [YANTRA_FE_ORDER_PROFILE].FEOP_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.FEOPId = dbManager.DataReader["FEOP_ID"].ToString();
                    this.FEOPNo = dbManager.DataReader["FEOP_NO"].ToString();
                    this.FEOPDate = Convert.ToDateTime(dbManager.DataReader["FEOP_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.FEOPPONo = dbManager.DataReader["FEOP_PO_NO"].ToString();
                    this.FEOPPODate = Convert.ToDateTime(dbManager.DataReader["FEOP__PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FEOPMarketing = dbManager.DataReader["FEOP_MARKETING"].ToString();
                    this.FEOPRegion = dbManager.DataReader["FEOP_REGION"].ToString();
                    this.FEOPTerritory = dbManager.DataReader["FEOP_TERRITORY"].ToString();
                    this.FEOPDivision = dbManager.DataReader["FEOP_DIVISION"].ToString();
                    this.FEOPMarketSegment = dbManager.DataReader["FEOP_MARKET_SEGMENT"].ToString();
                    this.FEOPOrders = dbManager.DataReader["FEOP_ORDERS"].ToString();
                    this.FEOPForwarder = dbManager.DataReader["FEOP_FORWARDER"].ToString();
                    this.FEOPPortofLanding = dbManager.DataReader["FEOP_PORT_OF_LANDING"].ToString();
                    this.FEOPFOBCharges = dbManager.DataReader["FEOP_FOB_CHARGES"].ToString();
                    this.FEOPCIFCharges = dbManager.DataReader["FEOP_CIF_CHARGES"].ToString();
                    this.FEOPDespatchMode = dbManager.DataReader["FEOP_DESPATCH_MODE"].ToString();
                    this.FEOPECCNo = dbManager.DataReader["FEOP_ECC_NO"].ToString();
                    this.FEOPCSTNo = dbManager.DataReader["FEOP_CST_NO"].ToString();
                    this.FEOPLSTNo = dbManager.DataReader["FEOP_LST_NO"].ToString();
                    this.FEOPTINNo = dbManager.DataReader["FEOP_TIN_NO"].ToString();
                    this.FEOPFrieghtCharges = dbManager.DataReader["FEOP_FRIEGHT_CHARGES"].ToString();
                    this.FEOPOctroi = dbManager.DataReader["FEOP_OCTROI"].ToString();
                    this.FEOPInsurance = dbManager.DataReader["FEOP_INSURANCE"].ToString();
                    this.FEOPPatrshipment = dbManager.DataReader["FEOP_PARTSHIPMENT"].ToString();
                    this.FEOPRoadPermitReq = dbManager.DataReader["FEOP_ROAD_PERMIT_REQUIRED"].ToString();
                    this.FEOPARE1Trans = dbManager.DataReader["FEOP_ARE1_TRANSACTION"].ToString();
                    this.FEOPEPCGTrans = dbManager.DataReader["FEOP_EPCG_TRANSACTION"].ToString();
                    this.FEOPDocEnclosed = dbManager.DataReader["FEOP_DOCUMENT_ENCLOSED"].ToString();
                    this.FEOPDocNo = dbManager.DataReader["FEOP_DOCUMENT_NO"].ToString();
                    this.FEOPTotItemsValue = dbManager.DataReader["FEOP_TOTAL_ITEMS_VALUE"].ToString();
                    this.FEOPTotDisValue = dbManager.DataReader["FEOP_DISCOUNTED_VALUE"].ToString();
                    this.FEOPTotExWorksValue = dbManager.DataReader["FEOP_EXWORKS_VALUE"].ToString();
                    this.FEOPPacking = dbManager.DataReader["FEOP_PACKING_FORWARDING"].ToString();
                    this.FEOPExciseDuty = dbManager.DataReader["FEOP_EXCISE_DUTY"].ToString();
                    this.FEOPEduCess = dbManager.DataReader["FEOP_EDU_CESS"].ToString();
                    this.FEOPSecEduCess = dbManager.DataReader["FEOP_SEC_EDU_CESS"].ToString();
                    this.FEOPCST = dbManager.DataReader["FEOP_CST"].ToString();
                    this.FEOPStampingCharges = dbManager.DataReader["FEOP_STAMPING_CHARGES"].ToString();
                    this.FEOPTotalValue = dbManager.DataReader["FEOP_TOTAL_VALUE"].ToString();
                    this.FEOPDeliveryDate = Convert.ToDateTime(dbManager.DataReader["FEOP_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FEOPWarrantyPeriod = dbManager.DataReader["FEOP_WARRANTY_PERIOD"].ToString();
                    this.FEOPPaymentTerms = dbManager.DataReader["FEOP_PAYMENT_TERMS"].ToString();
                    this.FEOPAdvRecdDetails = dbManager.DataReader["FEOP_ADV_RECD_DETAILS"].ToString();
                    this.FEOPChequeDD = dbManager.DataReader["FEOP_CHEQUE_DD_EPAYMENT"].ToString();
                    this.FEOPPostalAddress = dbManager.DataReader["FEOP_FULL_POSTAL_ADDRESS"].ToString();
                    this.FEOPContactPerson = dbManager.DataReader["FEOP_CONTACT_PERSON"].ToString();
                    this.FEOPTelNo = dbManager.DataReader["FEOP_TELNO_DIRECT"].ToString();
                    this.FEOPMobileNo = dbManager.DataReader["FEOP_MOBILE_NO"].ToString();
                    this.FEOPSplInstr = dbManager.DataReader["FEOP_SPL_INSTRUCTIONS"].ToString();
                    this.FEOPOrderBookedBy = dbManager.DataReader["FEOP_ORDER_BOOKED_BY"].ToString();
                    this.FEOPApprovedBy = dbManager.DataReader["FEOP_APPROVED_BY"].ToString();
                    this.FEOPName = dbManager.DataReader["FEOP_NAME"].ToString();
                    this.FEOPSignature = dbManager.DataReader["FEOP_SIGNATURE"].ToString();
                    this.FEOPPerDisc = dbManager.DataReader["FEOP_PERCENTAGE_DISCOUNT"].ToString();
                    this.FEOPPerDisc = dbManager.DataReader["FEOP_PERCENTAGE_DISCOUNT"].ToString();
                    this.FEOPDocRefNo = dbManager.DataReader["FEOP_DOC_REF_NO"].ToString();
                    this.FEPortOfDestination = dbManager.DataReader["FEOP_PORT_OF_DESTINATION"].ToString();
                    this.FEOPDespatchConsignee = dbManager.DataReader["FEOP_DESPATCH_CONSIGNEE"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static void FEOrderProfile_SelectAll(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FE_ORDER_PROFILE] ORDER BY FEOP_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "FEOP_NO", "FEOP_ID");
                }
            }


            public string FEOrderProfileBuyerDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_FE_ORDER_PROFILE_BUYER_DET] SELECT ISNULL(MAX(FEOP_BUYER_DET_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}' FROM [YANTRA_FE_ORDER_PROFILE_BUYER_DET]", this.FEOPId, this.BuyerDetBuyerType, this.BuyerDetContactPerson, this.BuyerDetDesig, this.BuyerDetTelNo, this.BuyerDetMobileNo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("FE Order Profile Buyers Details", "130");

                }
                return _returnStringMessage;
            }

            public int FEOrderProfileBuyerDetails_Delete(string FEOrderProfileId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_FE_ORDER_PROFILE_BUYER_DET] WHERE FEOP_ID={0}", FEOrderProfileId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void FEOrderProfileBuyerDetails_Select(string FEOrderProfileId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FE_ORDER_PROFILE_BUYER_DET],[YANTRA_FE_ORDER_PROFILE] WHERE [YANTRA_FE_ORDER_PROFILE_BUYER_DET].FEOP_ID=[YANTRA_FE_ORDER_PROFILE].FEOP_ID  AND [YANTRA_FE_ORDER_PROFILE_BUYER_DET].FEOP_ID=" + FEOrderProfileId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable BuyerDetails = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("BuyerType");
                BuyerDetails.Columns.Add(col);
                col = new DataColumn("ContactPerson");
                BuyerDetails.Columns.Add(col);
                col = new DataColumn("Designation");
                BuyerDetails.Columns.Add(col);
                col = new DataColumn("TelNo");
                BuyerDetails.Columns.Add(col);
                col = new DataColumn("MobileNo");
                BuyerDetails.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = BuyerDetails.NewRow();

                    dr["BuyerType"] = dbManager.DataReader["FEOP_BUYER_DET_BUYER_TYPE"].ToString();
                    dr["ContactPerson"] = dbManager.DataReader["FEOP_BUYER_DET_CONTACT_PERSON"].ToString();
                    dr["Designation"] = dbManager.DataReader["FEOP_BUYER_DET_DESIGNATION"].ToString();
                    dr["TelNo"] = dbManager.DataReader["FEOP_BUYER_DET_TEL_NO"].ToString();
                    dr["MobileNo"] = dbManager.DataReader["FEOP_BUYER_DET_MOBILE_NO"].ToString();

                    BuyerDetails.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = BuyerDetails;
                gv.DataBind();
            }


            public string FEOrderProfileProductDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET] SELECT ISNULL(MAX(FEOP_PROD_DET_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}' FROM [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET]", this.FEOPId, this.ItemCode, this.ProdDetQty, this.ProdDetCurrency, this.ProdDetUnitPrice, this.ProdDetTotalPrice);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("FE Order Profile Product Details", "130");

                }
                return _returnStringMessage;
            }

            public int FEOrderProfileProductDetails_Delete(string FEOrderProfileId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET] WHERE FEOP_ID={0}", FEOrderProfileId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void FEOrderProfileProductDetails_Select(string FEOrderProfileId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET],[YANTRA_FE_ORDER_PROFILE],[YANTRA_ITEM_MAST] WHERE [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET].FEOP_ID=[YANTRA_FE_ORDER_PROFILE].FEOP_ID AND [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_FE_ORDER_PROFILE_PRODUCT_DET].FEOP_ID=" + FEOrderProfileId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable ProductDetails = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemName");
                ProductDetails.Columns.Add(col);
                col = new DataColumn("Qty");
                ProductDetails.Columns.Add(col);
                col = new DataColumn("Currency");
                ProductDetails.Columns.Add(col);
                col = new DataColumn("UnitPrice");
                ProductDetails.Columns.Add(col);
                col = new DataColumn("TotalPrice");
                ProductDetails.Columns.Add(col);
                col = new DataColumn("ItemCode");
                ProductDetails.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = ProductDetails.NewRow();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["Qty"] = dbManager.DataReader["FEOP_PROD_DET_QTY"].ToString();
                    dr["Currency"] = dbManager.DataReader["FEOP_PROD_DET_CURRENCY"].ToString();
                    dr["UnitPrice"] = dbManager.DataReader["FEOP_PROD_DET_UNIT_PRICE"].ToString();
                    dr["TotalPrice"] = dbManager.DataReader["FEOP_PROD_DET_TOTAL_PRICE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    ProductDetails.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = ProductDetails;
                gv.DataBind();
            }
        }

        //Methods For Advertisement Agency Information
        public class AdvertisingAgencyInformation
        {
            public string AAIId, AAINo, AAIDate, AdvmId, AaId, AmId, AAIOrgName, AAISubscriptionDate, AAIEventName,
             AAIEventOnDate, AAIEventTillDate, AAISponsorshipMode, AAISponsorshipDate, AAIAdvertisement, AAIAdvtApprovedDate, SaId, AAIAdvtPublishingDate,
             AAIInvoiceNo, AAIInvoiceDate, AAIInvoiceAmount, AAIPayMode, AAIAdvanceGiven, AAIPaymentDate, AAIChequeNo,
             AAIChequeDate, AAIBankDetails, AAIBalnceAmount, AAIFullPayMode, AAIFullAmountPaid, AAIFullPaymentDate,
                AAIFullChequeNo, AAIFullChequeDate, AAIFullBankDetails;

            public AdvertisingAgencyInformation()
            {
            }


            public static string AdvertisingAgencyInformation_AutoGenCode()
            {

                string _codePrefix = CurrentFinancialYear() + " ";

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(AAI_NO,LEFT(AAI_NO,5),''))),0)+1 FROM [YANTRA_ADVERTISING_AGENSIES_INFO]").ToString());
                return _codePrefix + _returnIntValue;


            }
            public string AdvertisingAgencyInformation_Save()
            {
                this.AAINo = AdvertisingAgencyInformation_AutoGenCode();
                this.AAIId = AutoGenMaxId("[YANTRA_ADVERTISING_AGENSIES_INFO]", "AAI_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ADVERTISING_AGENSIES_INFO] SELECT ISNULL(MAX(AAI_ID),0)+1,'{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}' FROM [YANTRA_ADVERTISING_AGENSIES_INFO]",
                    this.AAINo, this.AAIDate, this.AdvmId, this.AaId, this.AmId, this.AAIOrgName, this.AAISubscriptionDate, this.AAIEventName, this.AAIEventOnDate, this.AAIEventTillDate, this.AAISponsorshipMode, this.AAISponsorshipDate, this.AAIAdvertisement, this.AAIAdvtApprovedDate, this.SaId, this.AAIAdvtPublishingDate, this.AAIInvoiceNo, this.AAIInvoiceDate, this.AAIInvoiceAmount, this.AAIPayMode, this.AAIAdvanceGiven, this.AAIPaymentDate, this.AAIChequeNo, this.AAIChequeDate, this.AAIBankDetails, this.AAIBalnceAmount, this.AAIFullPayMode, this.AAIFullAmountPaid, this.AAIFullPaymentDate, this.AAIFullChequeNo, this.AAIFullChequeDate, this.AAIFullBankDetails);



                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Advertising Agency Information Details", "131");

                }
                return _returnStringMessage;
            }

            public string AdvertisingAgencyInformation_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ADVERTISING_AGENSIES_INFO] SET  AAI_NO='{0}',AAI_DATE='{1}',ADVM_ID={2},AA_ID={3},AM_ID={4},AAI_ORG_NAME='{5}',AAI_SUBSCRIPTION_DATE='{6}',AAI_EVENT_NAME='{7}',AAI_EVENT_ON_DATE='{8}',AAI_EVENT_TILL_DATE='{9}',AAI_SPONSORSHIP_MODE='{10}',AAI_SPONSORSHIP_DATE='{11}',AAI_ADVERTISEMENT='{12}',AAI_ADVT_APPROVED_DATE='{13}',SA_ID={14},AAI_ADVT_PUBLISHING_DATE='{15}',AAI_INVOICE_NO='{16}',AAI_INVOICE_DATE='{17}',AAI_INVOICE_AMOUNT='{18}',AAI_PAY_MODE='{19}',AAI_ADVANCE_GIVEN='{20}',AAI_PAYMENT_DATE='{21}',AAI_CHEQUE_NO='{22}',AAI_CHEQUE_DATE='{23}',AAI_BANK_DETAILS='{24}',AAI_BALANCE_AMOUNT='{25}',AAI_FULL_PAY_MODE='{26}',AAI_FULL_AMOUNT_PAID='{27}',AAI_FULL_PAYMENT_DATE='{28}',AAI_FULL_CHEQUE_NO='{29}',AAI_FULL_CHEQUE_DATE='{30}',AAI_FULL_BANK_DETAILS='{31}' WHERE AAI_ID={32}",
                 this.AAINo, this.AAIDate, this.AdvmId, this.AaId, this.AmId, this.AAIOrgName, this.AAISubscriptionDate, this.AAIEventName,
                    this.AAIEventOnDate, this.AAIEventTillDate, this.AAISponsorshipMode, this.AAISponsorshipDate, this.AAIAdvertisement, this.AAIAdvtApprovedDate, this.SaId, this.AAIAdvtPublishingDate,
                    this.AAIInvoiceNo, this.AAIInvoiceDate, this.AAIInvoiceAmount, this.AAIPayMode, this.AAIAdvanceGiven, this.AAIPaymentDate, this.AAIChequeNo,
                    this.AAIChequeDate, this.AAIBankDetails, this.AAIBalnceAmount, this.AAIFullPayMode, this.AAIFullAmountPaid, this.AAIFullPaymentDate, this.AAIFullChequeNo, this.AAIFullChequeDate, this.AAIFullBankDetails, this.AAIId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Advertising Agency Information Details", "131");

                }
                return _returnStringMessage;
            }
            public string AdvertisingAgencyInformation_Delete(string AdvertisingInfoId)
            {


                if (DeleteRecord("[YANTRA_ADVERTISING_AGENSIES_INFO]", "AAI_ID", AdvertisingInfoId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Advertising Agency Information Details", "131");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static void AdvertisingAgencyInformation_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_AGENSIES_INFO] ORDER BY AAI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AAI_NO", "AAI_ID");
                }
            }
            public int AdvertisingAgencyInformation_Select(string AdvertisingInfoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_AGENSIES_INFO],[YANTRA_ADVERTISING_MAGZINES],[YANTRA_ADVERTISING_AGENCY],[YANTRA_ADVERTISING_MODE],[YANTRA_SIZE_OF_ADVERTISING]" +
                                                " WHERE [YANTRA_ADVERTISING_AGENSIES_INFO].ADVM_ID=[YANTRA_ADVERTISING_MODE].ADVM_ID  " +

                                                " AND [YANTRA_ADVERTISING_AGENSIES_INFO].AA_ID=[YANTRA_ADVERTISING_AGENCY].AA_ID " +
                     " AND [YANTRA_ADVERTISING_AGENSIES_INFO].AM_ID=[YANTRA_ADVERTISING_MAGZINES].AM_ID  " +
                      " AND [YANTRA_ADVERTISING_AGENSIES_INFO].SA_ID=[YANTRA_SIZE_OF_ADVERTISING].SA_ID   " +
                                                " AND [YANTRA_ADVERTISING_AGENSIES_INFO].AAI_ID='" + AdvertisingInfoId + "' ORDER BY [YANTRA_ADVERTISING_AGENSIES_INFO].AAI_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);




                if (dbManager.DataReader.Read())
                {
                    this.AAIId = dbManager.DataReader["AAI_ID"].ToString();
                    this.AAINo = dbManager.DataReader["AAI_NO"].ToString();
                    this.AAIDate = Convert.ToDateTime(dbManager.DataReader["AAI_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AdvmId = dbManager.DataReader["ADVM_ID"].ToString();
                    this.AaId = dbManager.DataReader["AA_ID"].ToString();
                    this.AmId = dbManager.DataReader["AM_ID"].ToString();
                    this.AAIOrgName = dbManager.DataReader["AAI_ORG_NAME"].ToString();
                    this.AAISubscriptionDate = Convert.ToDateTime(dbManager.DataReader["AAI_SUBSCRIPTION_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIEventName = dbManager.DataReader["AAI_EVENT_NAME"].ToString();
                    this.AAIEventOnDate = Convert.ToDateTime(dbManager.DataReader["AAI_EVENT_ON_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIEventTillDate = Convert.ToDateTime(dbManager.DataReader["AAI_EVENT_TILL_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAISponsorshipMode = dbManager.DataReader["AAI_SPONSORSHIP_MODE"].ToString();
                    this.AAISponsorshipDate = Convert.ToDateTime(dbManager.DataReader["AAI_SPONSORSHIP_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIAdvertisement = dbManager.DataReader["AAI_ADVERTISEMENT"].ToString();
                    this.AAIAdvtApprovedDate = Convert.ToDateTime(dbManager.DataReader["AAI_ADVT_APPROVED_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.SaId = dbManager.DataReader["SA_ID"].ToString();
                    this.AAIAdvtPublishingDate = Convert.ToDateTime(dbManager.DataReader["AAI_ADVT_PUBLISHING_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIInvoiceNo = dbManager.DataReader["AAI_INVOICE_NO"].ToString();
                    this.AAIInvoiceDate = Convert.ToDateTime(dbManager.DataReader["AAI_INVOICE_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIInvoiceAmount = dbManager.DataReader["AAI_INVOICE_AMOUNT"].ToString();
                    this.AAIPayMode = dbManager.DataReader["AAI_PAY_MODE"].ToString();
                    this.AAIAdvanceGiven = dbManager.DataReader["AAI_ADVANCE_GIVEN"].ToString();
                    this.AAIPaymentDate = Convert.ToDateTime(dbManager.DataReader["AAI_PAYMENT_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIChequeNo = dbManager.DataReader["AAI_CHEQUE_NO"].ToString();
                    this.AAIChequeDate = Convert.ToDateTime(dbManager.DataReader["AAI_CHEQUE_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIBankDetails = dbManager.DataReader["AAI_BANK_DETAILS"].ToString();
                    this.AAIBalnceAmount = dbManager.DataReader["AAI_BALANCE_AMOUNT"].ToString();
                    this.AAIFullPayMode = dbManager.DataReader["AAI_FULL_PAY_MODE"].ToString();
                    this.AAIFullAmountPaid = dbManager.DataReader["AAI_FULL_AMOUNT_PAID"].ToString();
                    this.AAIFullPaymentDate = Convert.ToDateTime(dbManager.DataReader["AAI_FULL_PAYMENT_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIFullChequeNo = dbManager.DataReader["AAI_FULL_CHEQUE_NO"].ToString();
                    this.AAIFullChequeDate = Convert.ToDateTime(dbManager.DataReader["AAI_FULL_CHEQUE_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.AAIFullBankDetails = dbManager.DataReader["AAI_FULL_BANK_DETAILS"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
        }

        //Methods For AdvertisingAgency
        public class AdvertisingAgency
        {

            public string AaId, AaName, AaDesc;
            public AdvertisingAgency()
            {
            }

            public string AdvertisingAgency_AutoGen()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(AA_ID),0)+1 FROM YANTRA_ADVERTISING_AGENCY").ToString());
                return _returnIntValue.ToString();
            }

            public string AdvertisingAgency_Save()
            {
                this.AaId = AdvertisingAgency_AutoGen();

                dbManager.Open();
                if (IsRecordExists("[YANTRA_ADVERTISING_AGENCY]", "AA_NAME", this.AaName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_ADVERTISING_AGENCY] SELECT ISNULL(MAX(AA_ID),0)+1,'{0}','{1}' FROM [YANTRA_ADVERTISING_AGENCY]", this.AaName, this.AaDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Advertising Agency Information Details", "131");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising Agency Name Already Exists.";
                }
                return _returnStringMessage;
            }

            public string AdvertisingAgency_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ADVERTISING_AGENCY]", "AA_NAME", this.AaName, "AA_ID", this.AaId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ADVERTISING_AGENCY] SET AA_NAME='{0}',AA_DESC='{1}' WHERE AA_ID={2}", this.AaName, this.AaDesc, this.AaId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Advertising Agency Information Details", "131");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising Agency Already Exists.";
                }
                return _returnStringMessage;
            }

            public string AdvertisingAgency_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_ADVERTISING_AGENCY]", "AA_ID", this.AaId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Advertising Agency Information Details", "131");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void AdvertisingAgency_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_AGENCY] ORDER BY AA_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AA_NAME", "AA_ID");
                }

            }

            public int AdvertisingAgency_Select(string AdvertisingId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_AGENCY] WHERE AA_ID=" + AdvertisingId + " ORDER BY AA_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AaId = dbManager.DataReader["AA_ID"].ToString();
                    this.AaName = dbManager.DataReader["AA_NAME"].ToString();
                    this.AaDesc = dbManager.DataReader["AA_DESC"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
        }

        //Methods For Advertising Mode
        public class AdvertisingMode
        {

            public string AdvmId, AdvmName, AdvmDesc;
            public AdvertisingMode()
            {
            }

            public string AdvertisingMode_AutoGen()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(ADVM_ID),0)+1 FROM YANTRA_ADVERTISING_MODE").ToString());
                return _returnIntValue.ToString();
            }

            public string AdvertisingMode_Save()
            {
                this.AdvmId = AdvertisingMode_AutoGen();

                dbManager.Open();
                if (IsRecordExists("[YANTRA_ADVERTISING_MODE]", "ADVM_NAME", this.AdvmName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_ADVERTISING_MODE] SELECT ISNULL(MAX(ADVM_ID),0)+1,'{0}','{1}' FROM [YANTRA_ADVERTISING_MODE]", this.AdvmName, this.AdvmDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Advertising Mode Details", "132");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising Name Already Exists.";
                }
                return _returnStringMessage;
            }

            public string AdvertisingMode_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ADVERTISING_MODE]", "ADVM_NAME", this.AdvmName, "ADVM_ID", this.AdvmId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ADVERTISING_MODE] SET ADVM_NAME='{0}',ADVM_DESC='{1}' WHERE ADVM_ID={2}", this.AdvmName, this.AdvmDesc, this.AdvmId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Advertising Mode Details", "132");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising Already Exists.";
                }
                return _returnStringMessage;
            }
            public string AdvertisingMode_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_ADVERTISING_MODE]", "ADVM_ID", this.AdvmId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Advertising Mode Details", "132");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void AdvertisingMode_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_MODE] ORDER BY ADVM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ADVM_NAME", "ADVM_ID");
                }

            }

            public int AdvertisingMode_Select(string AdvertisingId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_MODE] WHERE ADVM_ID=" + AdvertisingId + " ORDER BY ADVM_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AdvmId = dbManager.DataReader["ADVM_ID"].ToString();
                    this.AdvmName = dbManager.DataReader["ADVM_NAME"].ToString();
                    this.AdvmDesc = dbManager.DataReader["ADVM_DESC"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
        }

        //Methods For Magzines
        public class AdvertisingMagzines
        {
            public string AmId, AmName, AmDesc;
            public AdvertisingMagzines()
            {
            }

            public string AdvertisingMagzines_AutoGen()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(AM_ID),0)+1 FROM YANTRA_ADVERTISING_MAGZINES").ToString());
                return _returnIntValue.ToString();
            }

            public string AdvertisingMagzines_Save()
            {
                this.AmId = AdvertisingMagzines_AutoGen();

                dbManager.Open();
                if (IsRecordExists("[YANTRA_ADVERTISING_MAGZINES]", "AM_NAME", this.AmName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_ADVERTISING_MAGZINES] SELECT ISNULL(MAX(AM_ID),0)+1,'{0}','{1}' FROM [YANTRA_ADVERTISING_MAGZINES]", this.AmName, this.AmDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Advertising Magzines Details", "133");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising Magzine Name Already Exists.";
                }
                return _returnStringMessage;
            }

            public string AdvertisingMagzines_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ADVERTISING_MAGZINES]", "AM_NAME", this.AmName, "AM_ID", this.AmId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ADVERTISING_MAGZINES] SET AM_NAME='{0}',AM_DESC='{1}' WHERE AM_ID={2}", this.AmName, this.AmDesc, this.AmId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Advertising Magzines Details", "133");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising Magzine Already Exists.";
                }
                return _returnStringMessage;
            }
            public string AdvertisingMagzines_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_ADVERTISING_MAGZINES]", "AM_ID", this.AmId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Advertising Magzines Details", "133");
                    
                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void AdvertisingMagzines_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_MAGZINES] ORDER BY AM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AM_NAME", "AM_ID");
                }

            }

            public int AdvertisingMagzines_Select(string AdvertisingId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ADVERTISING_MAGZINES] WHERE AM_ID=" + AdvertisingId + " ORDER BY AM_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AmId = dbManager.DataReader["AM_ID"].ToString();
                    this.AmName = dbManager.DataReader["AM_NAME"].ToString();
                    this.AmDesc = dbManager.DataReader["AM_DESC"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
        }

        //Methods For SizeOfAdvertising 
        public class SizeOfAdvertising
        {
            public string SaId, SaDesc, SaSize;
            public SizeOfAdvertising()
            {
            }

            public string SizeOfAdvertising_AutoGen()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(SA_ID),0)+1 FROM YANTRA_SIZE_OF_ADVERTISING").ToString());
                return _returnIntValue.ToString();
            }

            public string SizeOfAdvertising_Save()
            {
                this.SaId = SizeOfAdvertising_AutoGen();

                dbManager.Open();
                if (IsRecordExists("[YANTRA_SIZE_OF_ADVERTISING]", "SA_ID", this.SaId) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_SIZE_OF_ADVERTISING] SELECT ISNULL(MAX(SA_ID),0)+1,'{0}','{1}' FROM [YANTRA_SIZE_OF_ADVERTISING]", this.SaSize, this.SaDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Advertising Agency Size Details", "131");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising  Already Exists.";
                }
                return _returnStringMessage;
            }

            public string SizeOfAdvertising_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_SIZE_OF_ADVERTISING]", "SA_SIZE", this.SaSize, "SA_ID", this.SaId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_SIZE_OF_ADVERTISING] SET SA_SIZE='{0}',SA_DESC='{1}' WHERE SA_ID={2}", this.SaSize, this.SaDesc, this.SaId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Advertising Agency Size Details", "132");

                    }
                }
                else
                {
                    _returnStringMessage = "Advertising  Already Exists.";
                }
                return _returnStringMessage;
            }

            public string SizeOfAdvertising_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_SIZE_OF_ADVERTISING]", "SA_ID", this.SaId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Advertising Agency Size Details", "131");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void SizeOfAdvertising_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SIZE_OF_ADVERTISING] ORDER BY SA_SIZE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SA_SIZE", "SA_ID");
                }

            }

            public int SizeOfAdvertising_Select(string AdvertisingId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SIZE_OF_ADVERTISING] WHERE SA_ID=" + AdvertisingId + " ORDER BY SA_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SaId = dbManager.DataReader["SA_ID"].ToString();
                    this.SaSize = dbManager.DataReader["SA_SIZE"].ToString();
                    this.SaDesc = dbManager.DataReader["SA_DESC"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
        }

        //Methods For SDBG Details
        public class SDBG
        {
            public string SDBGId, SDBGNo, SDBGDate, CustId, SDBGStatementOf, EnqId, SoId;
            public string SDBGDetId, SDBGDetStatementOf, SDBGDetNo, SDBGDetDDNo, SDBGDetDated, SDBGDetAmount, SDBGDetBank, SDBGDetDueDate, SDBGDetRemarks;

            public SDBG()
            {
            }

            public static string SDBG_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SDBG_NO,LEFT(SDBG_NO,5),''))),0)+1 FROM [YANTRA_SDBG_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_SDBG_MAST", "SDBG_NO");
            }
            public static string SDBGDetails_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SDBG_DET_NO,LEFT(SDBG_DET_NO,5),''))),0)+1 FROM [YANTRA_SDBG_DET]").ToString());
                return _codePrefix + _returnIntValue;
            }

            public string SDBG_Save()
            {
                this.SDBGNo = SDBG_AutoGenCode();
                this.SDBGId = AutoGenMaxId("[YANTRA_SDBG_MAST]", "SDBG_ID");
                this.SDBGDetNo = SDBGDetails_AutoGenCode();
                this.SDBGDetId = AutoGenMaxId("[YANTRA_SDBG_DET]", "SDBG_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SDBG_MAST] SELECT ISNULL(MAX(SDBG_ID),0)+1,'{0}','{1}',{2},'{3}',{4},{5} FROM [YANTRA_SDBG_MAST]",
                                                                                                             this.SDBGNo, this.SDBGDate, this.CustId, this.SDBGStatementOf, this.EnqId, this.SoId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("SDBG Details", "134");

                }
                return _returnStringMessage;
            }

            public string SDBG_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SDBG_MAST] SET SDBG_NO='{0}',SDBG_DATE='{1}',CUST_ID={2},SDBG_STATEMENT_OF='{3}',ENQ_ID={4},SO_ID={5} WHERE SDBG_ID={6}",
                                                                          this.SDBGNo, Convert.ToDateTime(this.SDBGDate), this.CustId, this.SDBGStatementOf, this.EnqId, this.SoId, this.SDBGId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("SDBG Details", "134");

                }
                return _returnStringMessage;
            }

            public string SDBG_Delete(string SDBGId)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[YANTRA_SDBG_DET]", "SDBG_ID", SDBGId) == true)
                {
                    if (DeleteRecord("[YANTRA_SDBG_MAST]", "SDBG_ID", SDBGId) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";

                        log.add_Delete("SDBG Details", "134");

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public static void SDBG_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SDBG_MAST] ORDER BY SDBG_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SDBG_NO", "SDBG_ID");
                }
            }
            public int SDBG_Select(string SDBGId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_SDBG_MAST],[YANTRA_SO_MAST],[YANTRA_CUSTOMER_MAST],[YANTRA_ENQ_MAST]" +
                                                                        " WHERE [YANTRA_SDBG_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID  " +
                                                                        " AND [YANTRA_SDBG_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID  " +
                                                                        " AND [YANTRA_SDBG_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID  " +
                    //" AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                                                        " AND [YANTRA_SDBG_MAST].SDBG_ID='" + SDBGId + "' ORDER BY [YANTRA_SDBG_MAST].SDBG_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SDBGId = dbManager.DataReader["SDBG_ID"].ToString();
                    this.SDBGNo = dbManager.DataReader["SDBG_NO"].ToString();
                    this.SDBGDate = Convert.ToDateTime(dbManager.DataReader["SDBG_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.SDBGStatementOf = dbManager.DataReader["SDBG_STATEMENT_OF"].ToString();
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.SoId = dbManager.DataReader["SO_ID"].ToString();
                    //this.SDBGDdNO = dbManager.DataReader["SDBG_DDNO"].ToString();
                    //this.SDBGDdDate = Convert.ToDateTime(dbManager.DataReader["SDBG_DDDATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.SDBGAmount = dbManager.DataReader["SDBG_AMOUNT"].ToString();
                    //this.SDBGBank = dbManager.DataReader["SDBG_BANK"].ToString();
                    //this.SDBGDueDate = Convert.ToDateTime(dbManager.DataReader["SDBG_DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.SDBGRemarks = dbManager.DataReader["SDBG_REMARKS"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public string SDBGDetails_Save()
            {
                //this.SDBGDetNo = SDBGDetails_AutoGenCode();
                //this.SDBGDetId = AutoGenMaxId("[YANTRA_SDBG_DET]", "SDBG_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SDBG_DET] SELECT ISNULL(MAX(SDBG_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}'FROM [YANTRA_SDBG_DET]",
                                                                                                                              this.SDBGId, this.SDBGDetNo, this.SDBGDetDDNo, this.SDBGDetDated, this.SDBGDetAmount, this.SDBGDetBank, this.SDBGDetDueDate, this.SDBGDetRemarks);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("SDBG Details", "134");

                }
                return _returnStringMessage;
            }
            public int SDBGDetails_Delete(string SDBGId)
            {

                if (DeleteRecord("[YANTRA_SDBG_DET]", "SDBG_ID", SDBGId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }
            public void SDBGDetails_Select(string SDBGId, GridView gv)
            {
                // dbManager.Open();
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_SDBG_DET] WHERE SDBG_ID = " + SDBGId + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SdbgProducts = new DataTable();
                DataColumn col = new DataColumn();
                //col = new DataColumn("StatementOf");
                //SdbgProducts.Columns.Add(col);
                col = new DataColumn("SDNumber");
                SdbgProducts.Columns.Add(col);
                col = new DataColumn("DDNo");
                SdbgProducts.Columns.Add(col);
                //col = new DataColumn("UOM");
                //ClaimFormProducts.Columns.Add(col);
                col = new DataColumn("DDDate");
                SdbgProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                SdbgProducts.Columns.Add(col);
                col = new DataColumn("Bank");
                SdbgProducts.Columns.Add(col);
                col = new DataColumn("DueDate");
                SdbgProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SdbgProducts.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SdbgProducts.NewRow();
                    //dr["StatementOf"] = dbManager.DataReader["SDBG_DET_STATEMENT_OF"].ToString();
                    dr["SDNumber"] = dbManager.DataReader["SDBG_DET_NO"].ToString();
                    dr["DDNo"] = dbManager.DataReader["SDBG_DET_DD_NO"].ToString();
                    dr["DDDate"] = Convert.ToDateTime(dbManager.DataReader["SDBG_DET_DATED"].ToString()).ToString("dd/MM/yyyy");
                    dr["Amount"] = dbManager.DataReader["SDBG_DET_AMOUNT"].ToString();
                    dr["Bank"] = dbManager.DataReader["SDBG_DET_BANK"].ToString();
                    dr["DueDate"] = Convert.ToDateTime(dbManager.DataReader["SDBG_DET_DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["Remarks"] = dbManager.DataReader["SDBG_DET_REMARKS"].ToString();

                    SdbgProducts.Rows.Add(dr);
                }

                gv.DataSource = SdbgProducts;
                gv.DataBind();
                //}


            }
        }

        //Methods For SDBG  Receipts Details
        public class SDBGReceipts
        {
            public string SDBGReceiptsId, SDBGReceiptsNo, SDBGReceiptsDate, CustId, SDBGReceiptsStatementOf, SDBGDetId, SDBGDETDATE, SDBGReceiptsPayMode, SDBGReceiptsDdChequeNo, SDBGReceiptsDDChequeDate, SDBGReceiptsBankDetails, SDBGReceiptsDueDate, SDBGReceiptsRemarks, SOID, ENQID;

            public SDBGReceipts()
            {
            }

            public static string SDBGReceipts_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SDBG_RECEIPTS_NO,LEFT(SDBG_RECEIPTS_NO,5),''))),0)+1 FROM [YANTRA_SDBG_RECEIPTS]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("YANTRA_SDBG_RECEIPTS", "SDBG_RECEIPTS_NO");
            }


            public string SDBGReceipts_Save()
            {
                this.SDBGReceiptsNo = SDBGReceipts_AutoGenCode();
                this.SDBGReceiptsId = AutoGenMaxId("[YANTRA_SDBG_RECEIPTS]", "SDBG_RECEIPTS_ID");

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SDBG_RECEIPTS] SELECT ISNULL(MAX(SDBG_RECEIPTS_ID),0)+1,'{0}','{1}',{2},'{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}' FROM [YANTRA_SDBG_RECEIPTS]",
                                                                                                             this.SDBGReceiptsNo, this.SDBGReceiptsDate, this.CustId, this.SDBGReceiptsStatementOf, this.SDBGDetId, this.SDBGReceiptsPayMode, this.SDBGReceiptsDdChequeNo, this.SDBGReceiptsDDChequeDate, this.SDBGReceiptsBankDetails, this.SDBGReceiptsDueDate, this.SDBGReceiptsRemarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("SDBG Reports Details", "134");

                }
                return _returnStringMessage;
            }

            public string SDBGReceipts_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SDBG_RECEIPTS] SET SDBG_RECEIPTS_NO='{0}',SDBG_RECEIPTS_DATE='{1}',CUST_ID={2},SDBG_RECEIPTS_STATEMENT_OF='{3}',SDBG_DET_ID={4},SDBG_RECEIPTS_PAY_MODE='{5}',SDBG_RECEIPTS_DD_CHEQUE_NO='{6}',SDBG_RECEIPTS_DD_CHEQUE_DATE='{7}',SDBG_RECEIPTS_BANK_DETAILS='{8}',SDBG_RECEIPTS_DUE_DATE='{9}',SDBG_RECEIPTS_REMARKS='{10}' WHERE SDBG_RECEIPTS_ID={11}",

                                                                           this.SDBGReceiptsNo, Convert.ToDateTime(this.SDBGReceiptsDate), this.CustId, this.SDBGReceiptsStatementOf, this.SDBGDetId, this.SDBGReceiptsPayMode, this.SDBGReceiptsDdChequeNo, Convert.ToDateTime(this.SDBGReceiptsDDChequeDate), this.SDBGReceiptsBankDetails, Convert.ToDateTime(this.SDBGReceiptsDueDate), this.SDBGReceiptsRemarks, this.SDBGReceiptsId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("SDBG Reports Details", "134");

                }
                return _returnStringMessage;
            }

            public string SDBGReceipts_Delete(string SDBGReceiptsId)
            {
                if (DeleteRecord("[YANTRA_SDBG_RECEIPTS]", "SDBG_RECEIPTS_ID", SDBGReceiptsId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("SDBG Receipts Details", "134");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static void SDBGReceipts_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SDBG_RECEIPTS] ORDER BY SDBG_RECEIPTS_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SDBG_RECEIPTS_NO", "SDBG_RECEIPTS_ID");
                }
            }

            public int SDBGReceipts_Select(string SDBGReceiptsId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM YANTRA_SDBG_RECEIPTS, [YANTRA_SDBG_MAST],[YANTRA_SO_MAST],[YANTRA_CUSTOMER_MAST],[YANTRA_ENQ_MAST],YANTRA_SDBG_DET " +
                                                                        " WHERE [YANTRA_SDBG_RECEIPTS].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID   " +
                                                                        " AND [YANTRA_SDBG_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID   " +
                                                                         " AND [YANTRA_SDBG_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID     " +
                                                                          " AND YANTRA_SDBG_RECEIPTS.SDBG_DET_ID=[YANTRA_SDBG_DET].SDBG_DET_ID     " +
                                                                            " AND YANTRA_SDBG_DET.SDBG_ID=YANTRA_SDBG_MAST.SDBG_ID       " +

                                                                        " AND [YANTRA_SDBG_RECEIPTS].SDBG_RECEIPTS_ID='" + SDBGReceiptsId + "' ORDER BY [YANTRA_SDBG_RECEIPTS].SDBG_RECEIPTS_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SDBGReceiptsId = dbManager.DataReader["SDBG_RECEIPTS_ID"].ToString();
                    this.SDBGReceiptsNo = dbManager.DataReader["SDBG_RECEIPTS_NO"].ToString();
                    this.SDBGReceiptsDate = Convert.ToDateTime(dbManager.DataReader["SDBG_RECEIPTS_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.SOID = dbManager.DataReader["SO_ID"].ToString();
                    this.ENQID = dbManager.DataReader["ENQ_ID"].ToString();
                    this.SDBGDETDATE = dbManager.DataReader["SDBG_DATE"].ToString();
                    this.SDBGReceiptsStatementOf = dbManager.DataReader["SDBG_RECEIPTS_STATEMENT_OF"].ToString();
                    this.SDBGDetId = dbManager.DataReader["SDBG_DET_ID"].ToString();
                    this.SDBGReceiptsPayMode = dbManager.DataReader["SDBG_RECEIPTS_PAY_MODE"].ToString();
                    this.SDBGReceiptsDdChequeNo = dbManager.DataReader["SDBG_RECEIPTS_DD_CHEQUE_NO"].ToString();
                    this.SDBGReceiptsDDChequeDate = Convert.ToDateTime(dbManager.DataReader["SDBG_RECEIPTS_DD_CHEQUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SDBGReceiptsBankDetails = dbManager.DataReader["SDBG_RECEIPTS_BANK_DETAILS"].ToString();
                    this.SDBGReceiptsDueDate = Convert.ToDateTime(dbManager.DataReader["SDBG_RECEIPTS_DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SDBGReceiptsRemarks = dbManager.DataReader["SDBG_RECEIPTS_REMARKS"].ToString();



                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
        }

        //Methods For Daily Report Form
        public class DailyReport
        {
            public string DRId, DRNo, DRDate, CustName, Remarks, Purpose, DRPreparedBy, DRAssistedBy,DRAttendedBy,DRType,DRStatus,DRFollowup, Time, Phone, Address;
            public string DRDetId, DRDetDate, DetCustName, DetReference, DetPurpose, DetRemarks, DetComments, EmpID, CommentedBy;
            public string ID, DetId, Date, IssuedDate, Subject, Description, PreparedBy, Status, AchiveYesterday,AchiveToday,email;
            public DailyReport()
            {
            }

            public static string DailyReports_AutoGenCode()
            {

                return AutoGenMaxNo("YANTRA_DAILY_REPORT", "DR_NO");
            }
            public string Reference, Architect, outTime, Comment, FileName;

            public static void ReferenceSelect(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("select Distinct CUST_ECC ,CUST_ECC  from YANTRA_CUSTOMER_MAST ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_ECC", "CUST_ECC");
                }
                else if (ControlForBind is GridView)
                {
                    //GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
            public string ToDO_List_Status_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update to_do_list set status='{0}' where id='{1}' ", this.Status ,this.ID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Daily Reports Details", "135");

                }
                return _returnStringMessage;
            }
            public string ToDo_List_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                this.ID = AutoGenMaxId("To_Do_List", "Id");

                _commandText = string.Format("INSERT INTO [To_Do_List] SELECT ISNULL(MAX(ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}' FROM [To_Do_List]", this.Date, this.IssuedDate , this.Subject , this.Description , this.Status, this.PreparedBy);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Daily Reports Details", "135");

                }
                return _returnStringMessage;
            }
            public string ToDO_List_Det_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [To_Do_List_Reporting] SELECT ISNULL(MAX(DetId),0)+1,'{0}','{1}' FROM [To_Do_List_Reporting]", this.ID, this.EmpID );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Daily Reports Details", "135");

                }
                return _returnStringMessage;
            }

            public string DRTasks_Save()
            {
                //this.DRNo = DailyReports_AutoGenCode();
                dbManager.Open();
                _commandText = string.Format("delete from [DailyReport_Tasks] where EmpId='{0}' and Date='{1}' ", this.EmpID,this.Date );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO [DailyReport_Tasks] SELECT ISNULL(MAX(TaskId),0)+1,'{0}','{1}','{2}','{3}' FROM [DailyReport_Tasks]", this.Date , this.AchiveYesterday, this.AchiveToday, this.EmpID);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                   
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        //log.add_Insert("Daily Reports Details", "135");

                    }
                 
                
                return _returnStringMessage;
            }

            
            public string DailyReports_Save()
            {
                //this.DRNo = DailyReports_AutoGenCode();


                this.DRId = AutoGenMaxId("YANTRA_DAILY_REPORT", "DAILYREPORTID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_DAILY_REPORT] SELECT ISNULL(MAX(DAILYREPORTID),0)+1,'{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}', GetDate() FROM [YANTRA_DAILY_REPORT]", this.DRDate, this.CustName, this.Purpose, this.Remarks, this.DRAttendedBy, this.DRPreparedBy, this.Time, this.Phone, this.Address, this.Reference, this.Architect, this.outTime, this.Comment, this.FileName, this.DRAssistedBy, this.DRType, this.DRFollowup, this.DRStatus, this.email);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Daily Reports Details", "135");

                }
                return _returnStringMessage;
            }

            public string DailyReport_Update1()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update YANTRA_DAILY_REPORT set DATETIME='{0}',CLIENTSNAME='{1}',PURPOSE='{2}',REMARKS='{3}',ATTENDEDBY='{4}',TIME='{5}',PHONE='{6}',ADDRESS='{7}',Reference='{8}',Architect='{9}',OUTTIME='{10}',AssistedBy='{11}',DR_Type='{12}',Email='{13}' where DAILYREPORTID ={14}", this.DRDate, this.CustName, this.Purpose, this.Remarks, this.DRAttendedBy, this.Time, this.Phone, this.Address, this.Reference, this.Architect, this.outTime, this.DRAssistedBy, this.DRType,this.email , this.DRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    //log.add_Update("Delivery Details", "62");

                }
                return _returnStringMessage;
            }

            public string DailyReportComm_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update YANTRA_DAILY_REPORT set Comments ='{0}' where DAILYREPORTID ={1}", this.DetComments, this.DRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    //log.add_Update("Delivery Details", "62");

                }
                return _returnStringMessage;
            }
            public string DailyReportDet_Save()
            {
                this.DRDetId = AutoGenMaxId("[YANTRA_DAILY_REPORT_DET]", "DAILYREPORTDET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_DAILY_REPORT_DET] VALUES({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}')", this.DRId, this.DRDetId, this.DetCustName, this.DetPurpose, this.DetRemarks, this.DetComments, this.DRDetDate, this.DetReference, this.CommentedBy,this.Time ,this.outTime );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Assignments Follow Up Details", "120");

                }
                return _returnStringMessage;
            }
            public int DailyReport_Tasks(string EmpId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Select * from DailyReport_Tasks where EmpId=" + EmpId + " and convert (nvarchar(50),Date,103) = convert (nvarchar(50),GETDATE(),103)");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AchiveYesterday = dbManager.DataReader["Achiveyesterday"].ToString();
                    this.AchiveToday = dbManager.DataReader["AchiveToday"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public int DailyReport_Taskswithdt(string EmpId, string Dt)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Select * from DailyReport_Tasks where EmpId=" + EmpId + " and convert (nvarchar(50),Date,103) = convert (nvarchar(50)," + Dt + ",103)");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AchiveYesterday = dbManager.DataReader["Achiveyesterday"].ToString();
                    this.AchiveToday = dbManager.DataReader["AchiveToday"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public string InHours, InMin, InAM, OutHours, OutMin, OutAM;
            public int DailyReport_Select(string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Select *,DATEPART(HOUR,TIME )as InHours,DATEPART(minute,time)as inmin ,SUBSTRING(CONVERT(varchar(50),time,22),18,3) inAM ,DATEPART(Hour,OUTTime)as OutHours, datepart(MINUTE,OUTTime) as outmin ,SUBSTRING(CONVERT(varchar(50),OUTTime,22),18,3) outAm from YANTRA_DAILY_REPORT where DAILYREPORTID=" + Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Purpose   = dbManager.DataReader["PURPOSE"].ToString();
                    this.Remarks  = dbManager.DataReader["REMARKS"].ToString();
                    this.CustName  = dbManager.DataReader["CLIENTSNAME"].ToString();
                    this.Phone  = dbManager.DataReader["PHONE"].ToString();
                    //this.OtherVaue = dbManager.DataReader["BranchesValue"].ToString();
                    //this.AccountsId = dbManager.DataReader["AccId"].ToString();
                    //this.CMDID = dbManager.DataReader["MDID"].ToString();
                    //this.Credit_Id = dbManager.DataReader["Credit_Id"].ToString();
                    //this.PaymentsCollected = Convert.ToDateTime(dbManager.DataReader["PayCollectedDate"].ToString()).ToString("dd/MM/yyyy");
                    //this.NoOfDays = dbManager.DataReader["NoOfDays"].ToString();
                    //this.Time = dbManager.DataReader["Time"].ToString();
                    //this.Status = dbManager.DataReader["Status"].ToString();
                    this.Reference = dbManager.DataReader["Reference"].ToString();
                    this.Architect = dbManager.DataReader["Architect"].ToString();
                    this.DRAttendedBy = dbManager.DataReader["ATTENDEDBY"].ToString();
                    this.DRAssistedBy = dbManager.DataReader["AssistedBy"].ToString();
                    this.Address = dbManager.DataReader["ADDRESS"].ToString();
                    this.DRType = dbManager.DataReader["DR_Type"].ToString();
                    this.Time = dbManager.DataReader["Time"].ToString();
                    this.outTime = dbManager.DataReader["OutTime"].ToString();
                    this.DRDate = Convert.ToDateTime(dbManager.DataReader["DATETIME"].ToString()).ToString("dd/MM/yyyy");
                    
                    this.InHours = dbManager.DataReader["InHours"].ToString();
                    this.InMin = dbManager.DataReader["InMin"].ToString();

                    this.InAM = dbManager.DataReader["InAM"].ToString();
                    this.OutHours = dbManager.DataReader["OutHours"].ToString();
                    this.OutMin = dbManager.DataReader["OutMin"].ToString();
                    this.OutAM = dbManager.DataReader["OutAM"].ToString();
                    this.email = dbManager.DataReader["Email"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public string DRDOC_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_DAILY_REPORT_DOCUMENTS] SELECT ISNULL(MAX(DAILYREPORTDOC_ID),0)+1,'{0}','{1}','{2}',{3} FROM [YANTRA_DAILY_REPORT_DOCUMENTS]", this.IssuedDate , this.Remarks , this.FileName , this.DRId );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                }
                return _returnStringMessage;
            }

            public string DRDocDetails_Delete(string Eleid)
            {
                if (DeleteRecord("[YANTRA_DAILY_REPORT_DOCUMENTS]", "DAILYREPORTDOC_ID", Eleid) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }
            //public string PaymentsReceived_Update()
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("UPDATE [YANTRA_PAYMENTS_RECEIVED] SET PR_NO='{0}',PR_DATE='{1}',CUST_ID={2},UNIT_ID={3},SI_ID={4},SI_AMOUNT='{5}',PR_AMT_RECEIVED='{6}',PR_PAYMODE_TYPE='{7}',PR_CHEQUE_NO='{8}',PR_CHEQUE_DATE='{9}',PR_CASH_RECEIVED_DATE='{10}',PR_BANK_DETAILS='{11}',PR_PREPARED_BY='{12}',PR_APPROVED_BY='{13}',PR_PAYMENT_STATUS='{14}',SO_ID={15},SPO_ID={16}  WHERE PR_ID={17}", this.PRNo, Convert.ToDateTime(this.PRDate), this.CustId, this.UnitId, this.SIId, this.SIAmt, this.PRReceivedAmt, this.PRPaymodeType, this.PRChequeNo, Convert.ToDateTime(this.PRChequeDate), Convert.ToDateTime(this.PRCahReceivedDate), this.PRBankDetails, this.PRPreparedBy, this.PRApprovedBy, this.PRPaymentStatus, this.SO_Id, this.SPOId, this.PRId);
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    _returnStringMessage = string.Empty;
            //    if (_returnIntValue < 0 || _returnIntValue == 0)
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }
            //   else if (_returnIntValue > 0)
            //    {
            //        _returnStringMessage = "Data Updated Successfully";
            //    }
            //    return _returnStringMessage;
            //}

            //public string PaymentsReceived_Delete(string PaymentsReceivedId)
            //{
            //    if (DeleteRecord("[YANTRA_PAYMENTS_RECEIVED]", "PR_ID", PaymentsReceivedId) == true)
            //    {
            //        _returnStringMessage = "Data Deleted Successfully";
            //    }
            //    else
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }

            //    return _returnStringMessage;
            //}



            //public int PaymentsReceived_Select(string PaymentsReceivedId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();

            //    _commandText = string.Format("SELECT * FROM [YANTRA_PAYMENTS_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS],YANTRA_SO_MAST" +
            //                                                            " WHERE [YANTRA_PAYMENTS_RECEIVED].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
            //                                                            " AND [YANTRA_PAYMENTS_RECEIVED].UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
            //        //" AND [YANTRA_PAYMENTS_RECEIVED].SI_ID=[YANTRA_SALES_INVOICE_MAST].SI_ID  " +
            //                                                             " AND [YANTRA_PAYMENTS_RECEIVED].SO_ID=[YANTRA_SO_MAST].SO_ID  " +
            //                                                            " AND [YANTRA_PAYMENTS_RECEIVED].PR_ID='" + PaymentsReceivedId + "' ORDER BY [YANTRA_PAYMENTS_RECEIVED].PR_ID DESC ");

            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.PRId = dbManager.DataReader["PR_ID"].ToString();
            //        this.PRNo = dbManager.DataReader["PR_NO"].ToString();
            //        this.PRDate = Convert.ToDateTime(dbManager.DataReader["PR_DATE"].ToString()).ToString("dd/MM/yyyy");
            //        this.CustId = dbManager.DataReader["CUST_ID"].ToString();
            //        this.UnitId = dbManager.DataReader["UNIT_ID"].ToString();
            //        this.SO_Id = dbManager.DataReader["SO_ID"].ToString();
            //        this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
            //        this.SIId = dbManager.DataReader["SI_ID"].ToString();
            //        this.SIAmt = dbManager.DataReader["SI_AMOUNT"].ToString();
            //        this.PRReceivedAmt = dbManager.DataReader["PR_AMT_RECEIVED"].ToString();
            //        this.PRPaymodeType = dbManager.DataReader["PR_PAYMODE_TYPE"].ToString();
            //        this.PRChequeNo = dbManager.DataReader["PR_CHEQUE_NO"].ToString();
            //        this.PRChequeDate = Convert.ToDateTime(dbManager.DataReader["PR_CHEQUE_DATE"].ToString()).ToString("dd/MM/yyyy");
            //        this.PRCahReceivedDate = Convert.ToDateTime(dbManager.DataReader["PR_CASH_RECEIVED_DATE"].ToString()).ToString("dd/MM/yyyy");
            //        this.PRBankDetails = dbManager.DataReader["PR_BANK_DETAILS"].ToString();
            //        this.PRPreparedBy = dbManager.DataReader["PR_PREPARED_BY"].ToString();
            //        this.PRApprovedBy = dbManager.DataReader["PR_APPROVED_BY"].ToString();
            //        this.PRPaymentStatus = dbManager.DataReader["PR_PAYMENT_STATUS"].ToString();

            //        _returnIntValue = 1;
            //    }
            //    else
            //    {
            //        _returnIntValue = 0;
            //    }
            //    return _returnIntValue;
            //}



            


        }


        public class QuotApprove
        {
            public string id, flag, approved, quotid;

            public string quotapprove()
            {

                dbManager.Open();
                _commandText = string.Format("SELECT  QUOT_PO_FLAG FROM [YANTRA_QUOT_MAST] WHERE QUOT_ID={0}", this.quotid);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == SMStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    
                    if (dbManager.Transaction == null)
                        //dbManager.Open();
                    //    _commandText = string.Format("insert into [YANTRA_QUOT_APPROVERS] select isnull(max(Quotation_approve_id),0)+1,'{0}',{1},{2} from YANTRA_QUOT_APPROVERS", this.flag, this.approved, this.quotid);
                    //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                        _commandText = string.Format("UPDATE [YANTRA_QUOT_APPROVERS] SET Quatation_Approved = {0},Quatation_flag = 'Open' WHERE QUOT_ID={1}", this.approved,this.quotid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);



                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Quatation Approved Successfully";
                    log.add_Insert("Quotation Approve Details", "136");

                    }
                    return _returnStringMessage;
                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Quotation Approve Details", "136");

                }
                return _returnStringMessage;
                dbManager.Dispose();
            }

        }

        //Method for Dispatch Form
        public class Dispatch
        {
            public string DispatchId, SoId, CustId, UnitId, CompanyId, Remarks, PaymentsCollected, OldDues, TransportCharges,PackingCharges,Exective,Preparedby,ApprovedBy,CreatedOn,DeliveryDate,CRANo,Time,Status;
            public string Credit_Id, DetId, ItemCode, Qty, Color, ModelNo, Rate, Price, CustUnitname, UnitAddress, POValue, CP_ID,DispatchValue, CrAppValue, crValue, UDCValue, OtherVaue, AccountsId, CMDID, NoOfDays,OtherProjectsValue,CRDRUDC,CRDRBranches,CRDRProjects;
            public string CustomerPhoneno, CustomerEmailid;
            public Dispatch()
            { }

            public static string CRA_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(PR_NO,LEFT(PR_NO,5),''))),0)+1 FROM [YANTRA_PAYMENTS_RECEIVED]").ToString());
                //return _codePrefix + _returnIntValue;
                return AutoGenMaxNo("Credit_Approval_tbl", "CRA_NO");
            }
            public int CreditApp_Select(string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Select * from credit_approval_tbl where Dispatch_Id=" + Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CrAppValue = dbManager.DataReader["CreditAppValue"].ToString();
                    this.DispatchValue = dbManager.DataReader["DispatchValue"].ToString();
                    this.crValue = dbManager.DataReader["CR_DR_Value"].ToString();
                    this.UDCValue = dbManager.DataReader["UnbilledDCValue"].ToString();
                    this.OtherVaue = dbManager.DataReader["BranchesValue"].ToString();
                    this.AccountsId = dbManager.DataReader["AccId"].ToString();
                    this.CMDID = dbManager.DataReader["MDID"].ToString();
                    this.Credit_Id = dbManager.DataReader["Credit_Id"].ToString();
                    this.PaymentsCollected = Convert.ToDateTime(dbManager.DataReader["PayCollectedDate"].ToString()).ToString("dd/MM/yyyy");
                    //this.PaymentsCollected = dbManager.DataReader["PayCollectedDate"].ToString();
                    this.NoOfDays = dbManager.DataReader["NoOfDays"].ToString();
                    this.Time = dbManager.DataReader["Time"].ToString();
                    this.Status = dbManager.DataReader["Status"].ToString();
                    this.CRANo =dbManager .DataReader ["CRA_NO"].ToString ();
                    this.OtherProjectsValue = dbManager.DataReader["OtherProjects"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public int Select_CrApp(string CustUnitId)
            {
                if (CustUnitId == "0") { _returnIntValue = 0; }
                else
                {

                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("select * from Credit_Approval_tbl where Cust_Id='" + CustUnitId + "' ORDER BY Cust_Id DESC ");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.CRANo  = dbManager.DataReader["CRA_NO"].ToString();
                        this.CreatedOn  = dbManager.DataReader["CreatedOn"].ToString();
                        this.PaymentsCollected  = dbManager.DataReader["PayCollectedDate"].ToString();
                        //this.CustUnitAddress = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();

                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
                }
                return _returnIntValue;
            }
            public int Dispatch_SelectForCredit(string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("select * from Dispatch inner join YANTRA_CUSTOMER_UNITS on Dispatch.Cust_Unitid=YANTRA_CUSTOMER_UNITS .CUST_UNIT_ID left outer join YANTRA_SO_MAST on Dispatch .So_Id =YANTRA_SO_MAST .SO_ID where Dispatch_Id=" + Id);

                _commandText = string.Format("select * from Dispatch inner join YANTRA_CUSTOMER_UNITS on Dispatch.Cust_Unitid=YANTRA_CUSTOMER_UNITS .CUST_UNIT_ID INNER JOIN YANTRA_CUSTOMER_MAST ON YANTRA_CUSTOMER_UNITS.CUST_ID = YANTRA_CUSTOMER_MAST.CUST_ID left outer join YANTRA_SO_MAST on Dispatch .So_Id =YANTRA_SO_MAST .SO_ID where Dispatch_Id=" + Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CustId = dbManager.DataReader["Cust_Unitid"].ToString();
                    this.CustUnitname = dbManager.DataReader["CUST_UNIT_NAME"].ToString();
                    this.UnitAddress = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();
                    this.POValue = dbManager.DataReader["SO_TOTAL_AMT"].ToString();
                    this.Status = dbManager.DataReader["Status"].ToString();
                    this.DispatchValue = dbManager.DataReader["PackingCharges"].ToString();
                    this.PackingCharges = dbManager.DataReader["SO_OTHER_SPEC"].ToString();

                    this.CustomerEmailid = dbManager.DataReader["CUST_EMAIL"].ToString();
                    this.CustomerPhoneno = dbManager.DataReader["CUST_MOBILE"].ToString();
                    this.SoId = dbManager.DataReader["SO_ID"].ToString();
                    this.Exective = dbManager.DataReader["Executive"].ToString();
                    this.CP_ID = dbManager.DataReader["Company_id"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;

            }
            public static string DispatchStatus_Update(SMStatus Status, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [Dispatch] SET  Status='{0}' WHERE Dispatch_Id={1}", Status, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Enquiry Status Details", "117");

                }
                return _returnStringMessage;
            }
            public string CreditApproval_Update()
            {
                dbManager.Open();
                _commandText = string.Format("Update credit_approval_tbl Set Dispatch_Id='{0}',Cust_Id='{1}',So_Id='{2}',Executive_Id='{3}',PreparedBy='{4}',ApprovedBy='{5}',CreatedOn='{6}',PayCollectedDate='{7}',NoOfDays='{8}',Time='{9}',Status='{10}',POValue={11},DispatchValue={12},CreditAppValue={13},CR_DR_Value={14},UnbilledDCValue={15},BranchesValue={16},AccId='{17}',MDID='{18}',CRA_NO='{19}',CP_ID='{20}',OtherProjects={21},CR_DR_UDC='{22}',CR_DR_Branches='{23}',CR_DR_Projects='{24}' where Dispatch_Id='{0}'",
                    this.DispatchId, this.CustId, this.SoId, this.Exective, this.Preparedby, this.ApprovedBy, this.CreatedOn, this.PaymentsCollected, this.NoOfDays, this.Time, this.Status, this.POValue, this.DispatchValue, this.CrAppValue, this.crValue, this.UDCValue, this.OtherVaue, this.AccountsId, this.CMDID,this.CRANo,this.CP_ID,this.OtherProjectsValue ,this.CRDRUDC ,this.CRDRBranches ,this.CRDRProjects    );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;

                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    //log.add_Insert("Dispatch Details", "137");

                }

                return _returnStringMessage;
            }
            public string CreditApproval_Save()
            {
                dbManager.Open();
                this.Credit_Id = AutoGenMaxId("[Credit_Approval_tbl]", "Credit_id");
                _commandText = string.Format("INSERT INTO [Credit_Approval_tbl] SELECT ISNULL(MAX(Credit_id),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12},{13},{14},{15},{16},{17},{18},'{19}',{20},{21},'{22}','{23}','{24}' FROM [Credit_Approval_tbl]",
                    this.DispatchId, this.CustId, this.SoId, this.Exective, this.Preparedby, this.ApprovedBy, this.CreatedOn, this.PaymentsCollected, this.NoOfDays, this.Time, this.Status, this.POValue, this.DispatchValue, this.CrAppValue, this.crValue, this.UDCValue, this.OtherVaue, this.AccountsId, this.CMDID,this.CRANo,this.CP_ID,this.OtherProjectsValue ,this.CRDRUDC ,this.CRDRBranches ,this.CRDRProjects   );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Dispatch Details", "137");

                }

                return _returnStringMessage;
            }
            public string Dispatch_Save()
            {
                dbManager.Open();
                this.DispatchId = AutoGenMaxId("[Dispatch]", "Dispatch_id");
                _commandText = string.Format("INSERT INTO [Dispatch] SELECT ISNULL(MAX(Dispatch_id),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}','{13}',{14},'{15}' FROM [Dispatch]", this.CustId, this.UnitId, this.CompanyId, this.Remarks, this.PaymentsCollected, this.OldDues, this.TransportCharges, this.PackingCharges, this.Exective, this.Preparedby, this.ApprovedBy, this.CreatedOn, this.DeliveryDate, this.Time, this.SoId,this.Status);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Dispatch Details", "137");

                    }
               
                return _returnStringMessage;
            }

            public string Dispatch_Update()
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [Dispatch] SET So_Id={0},Cust_id={1},Cust_Unitid={2},Company_id={3},Remarks='{4}',PaymentsCollected='{5}',Olddues='{6}',Transportcharges = '{7}',Packingcharges ='{8}',Executive ={9},PreparedBy ={10},ApprovedBy = {11},CreatedOn = '{12}',DeliveryDate ='{13}',Time = '{14}',Status = '{15}' WHERE Dispatch_id = {16}", this.SoId, this.CustId, this.UnitId, this.CompanyId, this.Remarks, this.PaymentsCollected, this.OldDues, this.TransportCharges, this.PackingCharges, this.Exective, this.Preparedby, this.ApprovedBy, this.CreatedOn, this.DeliveryDate, this.Time,this.Status, this.DispatchId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Dispatch Details", "137");

                    }
                
                return _returnStringMessage;
            }

            public string Dispatch_Delete(string Id)
            {
                SM.BeginTransaction();
                if (DeleteRecord("[Dispatch_Details]", "DispatchId", Id) == true)
                {
                    if (DeleteRecord("[Dispatch]", "Dispatch_id", Id) == true)
                    {
                        SM.CommitTransaction();
                        _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Dispatch Details", "137");

                    }
                    else
                    {
                        SM.RollBackTransaction();
                        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                    }
                }
                else
                {
                    SM.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }


            public string DispatchDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [Dispatch_Details] SELECT ISNULL(MAX(Dispatch_DetId),0)+1,{0},'{1}',{2},'{3}','{4}',{5},{6} FROM [Dispatch_Details]",
                                      this.DispatchId, this.ItemCode, this.Qty, this.Color,this.ModelNo,this.Rate,this.Price);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Dispatch Details", "137");

                }
                return _returnStringMessage;
            }

            public int DispatchDetails_Delete(string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [Dispatch_Details] WHERE DispatchId ={0}", Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int Dispatch_Select(string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [Dispatch] WHERE Dispatch_id = " + Id);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SoId = dbManager.DataReader["So_Id"].ToString();
                    this.CustId = dbManager.DataReader["Cust_id"].ToString();
                    this.UnitId = dbManager.DataReader["Cust_Unitid"].ToString();
                    this.CompanyId = dbManager.DataReader["Company_id"].ToString();
                    this.Remarks = dbManager.DataReader["Remarks"].ToString();
                    this.PaymentsCollected = dbManager.DataReader["PaymentsCollected"].ToString();
                    this.OldDues = dbManager.DataReader["Olddues"].ToString();
                    this.TransportCharges = dbManager.DataReader["Transportcharges"].ToString();
                    this.PackingCharges = dbManager.DataReader["Packingcharges"].ToString();
                    this.Exective = dbManager.DataReader["Executive"].ToString();
                    this.Preparedby = dbManager.DataReader["PreparedBy"].ToString();
                    this.ApprovedBy = dbManager.DataReader["ApprovedBy"].ToString();
                    this.CreatedOn = Convert.ToDateTime(dbManager.DataReader["CreatedOn"].ToString()).ToString("dd/MM/yyyy");
                    this.DeliveryDate = Convert.ToDateTime(dbManager.DataReader["DeliveryDate"].ToString()).ToString("dd/MM/yyyy");
                    this.Time = dbManager.DataReader["Time"].ToString();
                    this.Status = dbManager.DataReader["Status"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
               
            public void DispatchDetails_Select(string disID, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);


                _commandText = string.Format("SELECT * FROM [Dispatch_Details] INNER JOIN YANTRA_ITEM_MAST ON [Dispatch_Details].ModelNo=YANTRA_ITEM_MAST.ITEM_MODEL_NO INNER JOIN YANTRA_LKUP_UOM ON YANTRA_ITEM_MAST.UOM_ID=YANTRA_LKUP_UOM.UOM_ID WHERE YANTRA_ITEM_MAST .STATUS =1 and [Dispatch_Details].DispatchId=" + disID + " Order by Dispatch_DetId asc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("HSN_CODE");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("GST_TAX");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Color");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Price");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ModelNo"].ToString();
                    dr["HSN_CODE"] = dbManager.DataReader["HSN_Code"].ToString();
                    dr["GST_TAX"] = dbManager.DataReader["GST Tax"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Color"] = dbManager.DataReader["Color"].ToString();
                    dr["Rate"] = dbManager.DataReader["Rate"].ToString();
                    dr["Price"] = dbManager.DataReader["Price"].ToString();
                    dr["Quantity"] = dbManager.DataReader["Qty"].ToString();


                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }

            public static string DispatchApprove_Update(string Approved,string DisId,string Status)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [Dispatch] SET  ApprovedBy ='{0}',Status='{1}' WHERE Dispatch_id ='{2}'", Approved, Status, DisId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Dispatch Approve Details", "137");

                }
                return _returnStringMessage;
            }


        }


    }
}
    