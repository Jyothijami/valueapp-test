using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraDAL;
using vllib;
/// <summary>
/// Summary description for SCM

namespace YantraBLL.Modules
{
    public class SCM
    {
        private static int _returnIntValue;
        private static string _returnStringMessage, _commandText;
        public enum SCMStatus { New = 0, Open = 1, Closed = 2, Cancelled = 3, Regret = 4, Revised = 5, }
        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());

        public SCM()
        { }

        //Method for dispose
        public static void Dispose()
        {
            dbManager.Dispose();
        }

        //Method for BeginTransaction
        public static void BeginTransaction()
        {
            dbManager.Open();
            dbManager.BeginTransaction();
        }

        //Method for CommitTransaction
        public static void CommitTransaction()
        {
            dbManager.CommitTransaction();
        }

        //Method for RollBackTransaction
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

        private static bool IsRecordExists(string paraTableName, string paraFirstFieldName, string paraFirstFieldValue, string paraSecondFieldName, string paraSecondFieldValue, string paraThirdFieldName, string paraThridFieldValue, string paraFourthFieldName, string paraFourthFieldValue)
        {
            bool check = false;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFirstFieldName + "='" + paraFirstFieldValue + "' and " + paraSecondFieldName + "='" + paraSecondFieldValue + "' and " + paraThirdFieldName + "='" + paraThridFieldValue + "'  and " + paraFourthFieldName + "='" + paraFourthFieldValue + "'").ToString());
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
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteNonQuery(CommandType.Text, "DELETE FROM " + paraTableName + " WHERE " + paraFieldName + "='" + paraFieldValue + "'").ToString());
            if (_returnIntValue > 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }
           // dbManager.Close();
            return check;
        }

        //Method for Auto Generate Max Serial ID
        public static string AutoGenMaxId(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
           // dbManager.Close();
            return _returnIntValue.ToString();
        }

        //Method for to Get Current Financial Year
        public static string CurrentFinancialYear()
        {
            string year;
            if (dbManager.Transaction == null)
                dbManager.Open();
            year = dbManager.ExecuteScalar(CommandType.Text, "SELECT CP_CF_YEAR FROM [YANTRA_COMP_PROFILE]").ToString();
            if (string.IsNullOrEmpty(year))
            {
                year = "0000";
            }
           // dbManager.Close();
            return year;
        }

        //Method for clearing Textbox and Dropdown list and Listbox
        public static void ClearControls(Control Parent)
        {
            if (Parent is TextBox)
                (Parent as TextBox).Text = string.Empty;
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
           // dbManager.Close();
        }

        //Method for GridBind Fill
        private static void GridViewBind(GridView gv)
        {
            gv.DataSource = dbManager.DataReader;
            gv.DataBind();
            dbManager.DataReader.Close();
        }

        //Method for Supplier Master Form
        public class SuppliersMaster
        {
            public string SupId, SupName, SupContactPerson, SupAddress, SupContactPersonDetails, SupPhone, SupMobile, SupEmail, SupFaxNo, SupPanNo, SupCstNo, SupVatNo, SupEccNo, SupRanking, SupBrand, SupIndigenousForeign, CountryId, CpId, supplier_Template_ID; // Suppliers Master
            public string SupDetId, ItemCode, ItemType, UOM, SupBasisOfApproval, Stno;
            public string unitname, unitaddress, unitid;

			public string Id, Name, Date, PONo, Ref, Quot_Revised_Date, Shipper_Name, Terms, Freight, Exworks, Low_Sul_Charge, Destin_Charges, Shipping_Charges, DO_Charges;
            public string Fuel, ssc, All_in_Charges, HAWB_Fee, Airline_DO, CC_Fee, IGM_Fee, Service_Tax;
            public string ex1, ex2, ex3, ex4, ex5;


            public SuppliersMaster()
            { }
			
            public string PObySea_Save()
            {             
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [PObySea] SELECT ISNULL(MAX(Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18} FROM [PObySea]", this.Name, this.Date, this.PONo, this.Ref, this.Quot_Revised_Date, this.Shipper_Name, this.Terms, this.Freight, this.Low_Sul_Charge, this.Exworks, this.Destin_Charges, this.Shipping_Charges, this.DO_Charges, this.ex1, this.ex2, this.ex3, this.ex4, this.ex5, this.CpId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        //log.add_Insert("Supplier Master Details", "75");

                    }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string PObyAir_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [PObyAir] SELECT ISNULL(MAX(Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',{20} FROM [PObyAir]", this.Name, this.Date, this.PONo, this.Ref, this.Quot_Revised_Date, this.Shipper_Name, this.Terms, this.Freight, this.Fuel, this.ssc, this.Exworks, this.All_in_Charges, this.HAWB_Fee, this.Airline_DO, this.CC_Fee, this.IGM_Fee, this.Service_Tax, this.ex1, this.ex2, this.ex3,this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Supplier Master Details", "75");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersMaster_Save()
            {
                //if (dbManager.Transaction == null)
                dbManager.Open();
                if (IsRecordExists("[YANTRA_SUPPLIER_MAST]", "SUP_NAME", this.SupName) == false)
                {
                    this.SupId = AutoGenMaxId("[YANTRA_SUPPLIER_MAST]", "SUP_ID");
                    _commandText = string.Format("INSERT INTO [YANTRA_SUPPLIER_MAST] SELECT ISNULL(MAX(SUP_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13},{14},'{15}',{16},'{17}','{18}','{19}','{20}' FROM [YANTRA_SUPPLIER_MAST]", this.SupName, this.SupContactPerson, this.SupAddress, this.SupContactPersonDetails, this.SupPhone, this.SupMobile, this.SupEmail, this.SupFaxNo, this.SupPanNo, this.SupCstNo, this.SupVatNo, this.SupEccNo, this.SupRanking, this.SupBrand, this.ItemCode, this.SupIndigenousForeign, this.CountryId, this.Stno, this.supplier_Template_ID,DateTime.Now,DateTime.Now);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Supplier Master Details", "75");

                    }
                }
                else
                {
                    _returnStringMessage = "Supplier  Already Exists.";
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersMaster_Update()
            {
                //if (dbManager.Transaction == null)
                dbManager.Open();
                if (IsRecordExists("[YANTRA_SUPPLIER_MAST]", "SUP_NAME", this.SupName, "SUP_ID", this.SupId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_SUPPLIER_MAST] SET SUP_NAME='{0}',SUP_CONTACT_PERSON='{1}',SUP_ADDRESS='{2}',SUP_CONTACT_PER_DET='{3}',SUP_PHONE='{4}',SUP_MOBILE='{5}',SUP_EMAIL='{6}',SUP_FAXNO='{7}',SUP_PANNO='{8}',SUP_CSTNO='{9}',SUP_VATNO='{10}',SUP_ECCNO='{11}',SUP_RANKING='{12}',SUP_BRAND={13},ITEM_CODE={14},INDIGENOUS_FOREIGN='{15}',COUNTRY_ID={17},STNO='{18}', sptid='{19}',DateInserted ='{20}' WHERE SUP_ID={16}", this.SupName, this.SupContactPerson, this.SupAddress, this.SupContactPersonDetails, this.SupPhone, this.SupMobile, this.SupEmail, this.SupFaxNo, this.SupPanNo, this.SupCstNo, this.SupVatNo, this.SupEccNo, this.SupRanking, this.SupBrand, this.ItemCode, this.SupIndigenousForeign, this.SupId, this.CountryId, this.Stno, this.supplier_Template_ID,DateTime.Now);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Supplier Master Details", "75");

                    }
                }
                else
                {
                    _returnStringMessage = "Supplier Already Exists.";
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersMaster_Delete()
            {
                SCM.BeginTransaction();
                if (DeleteRecord("[YANTRA_SUPPLIER_APPROVALS]", "SUP_ID", this.SupId) == true)
                {
                    if (DeleteRecord("[YANTRA_SUPPLIER_MAST]", "SUP_ID", this.SupId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Supplier Master Details", "75");

                        SCM.CommitTransaction();
                    }
                    else
                    {
                        SCM.RollBackTransaction();
                    }
                }
                else
                {
                    if (DeleteRecord("[YANTRA_SUPPLIER_MAST]", "SUP_ID", this.SupId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Supplier Master Details", "75");

                        SCM.CommitTransaction();
                    }
                    else
                    {
                        SCM.RollBackTransaction();
                    }
                }
                return _returnStringMessage;
            }
            public static void SuppliersMast_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT SUP_NAME,SUP_ID FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_ID");
                _commandText = string.Format("SELECT SUP_NAME,SUP_ID FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_NAME desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    //DropDownListBind(ControlForBind as DropDownList, "SUP_ID", "SUP_ID");
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
                // dbManager.Close();
            }
            public static void SuppliersMaster_SelectName(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SUP_NAME,SUP_ID FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_NAME");
                //_commandText = string.Format("SELECT SUP_NAME,SUP_ID FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    //DropDownListBind(ControlForBind as DropDownList, "SUP_ID", "SUP_ID");
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
                // dbManager.Close();
            }
            public static void SuppliersMaster_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SUP_NAME,SUP_ID FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_ID");
                //_commandText = string.Format("SELECT SUP_NAME,SUP_ID FROM [YANTRA_SUPPLIER_MAST] ORDER BY SUP_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_ID", "SUP_ID");
                    //DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
               // dbManager.Close();
            }

            public static void SuppliersMaster_SelectForBrand(Control ControlForBind, string brand)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST] where SUP_BRAND ={0} ORDER BY SUP_NAME", brand);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
               // dbManager.Close();
            }
            public static void SuppliersMaster_Select1(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST] ORDER BY [YANTRA_SUPPLIER_MAST].SUP_NAME order by Sup_name desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
               // dbManager.Close();
            }

            public static void SuppliersMaster_ModelNoFill(Control ControlForBind, int SupNo, int Brand)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct dbo.YANTRA_ITEM_MAST.ITEM_CODE, dbo.YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM dbo.YANTRA_ITEM_MAST INNER JOIN dbo.YANTRA_SUP_QUOT_DET ON dbo.YANTRA_ITEM_MAST.ITEM_CODE = dbo.YANTRA_SUP_QUOT_DET.ITEM_CODE INNER JOIN dbo.YANTRA_SUP_QUOT_MAST ON dbo.YANTRA_SUP_QUOT_DET.SUP_QUOT_ID = dbo.YANTRA_SUP_QUOT_MAST.SUP_QUOT_ID INNER JOIN dbo.YANTRA_SUPPLIER_MAST ON dbo.YANTRA_SUP_QUOT_MAST.SUP_ID = dbo.YANTRA_SUPPLIER_MAST.SUP_ID INNER JOIN dbo.YANTRA_LKUP_PRODUCT_COMPANY ON dbo.YANTRA_ITEM_MAST.BRAND_ID = dbo.YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID WHERE  (dbo.YANTRA_SUP_QUOT_MAST.SUP_ID =" + SupNo + " ) AND (dbo.YANTRA_ITEM_MAST.BRAND_ID =" + Brand + " )");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
               // dbManager.Close();
            }

            public int SuppliersMaster_Select(string SupplierId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST] WHERE SUP_ID = " + SupplierId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.SupName = dbManager.DataReader["SUP_NAME"].ToString();
                    this.SupContactPerson = dbManager.DataReader["SUP_CONTACT_PERSON"].ToString();
                    this.SupAddress = dbManager.DataReader["SUP_ADDRESS"].ToString();
                    this.SupContactPersonDetails = dbManager.DataReader["SUP_CONTACT_PER_DET"].ToString();
                    this.SupPhone = dbManager.DataReader["SUP_PHONE"].ToString();
                    this.SupMobile = dbManager.DataReader["SUP_MOBILE"].ToString();
                    this.SupEmail = dbManager.DataReader["SUP_EMAIL"].ToString();
                    this.SupFaxNo = dbManager.DataReader["SUP_FAXNO"].ToString();
                    this.SupPanNo = dbManager.DataReader["SUP_PANNO"].ToString();
                    this.SupCstNo = dbManager.DataReader["SUP_CSTNO"].ToString();
                    this.SupVatNo = dbManager.DataReader["SUP_VATNO"].ToString();
                    this.SupEccNo = dbManager.DataReader["SUP_ECCNO"].ToString();
                    this.SupRanking = dbManager.DataReader["SUP_RANKING"].ToString();
                    this.SupBrand = dbManager.DataReader["SUP_BRAND"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.SupIndigenousForeign = dbManager.DataReader["INDIGENOUS_FOREIGN"].ToString();
                    this.CountryId = dbManager.DataReader["COUNTRY_ID"].ToString();
                    this.supplier_Template_ID = dbManager.DataReader["sptid"].ToString();
                    this.Stno = dbManager.DataReader["STNO"].ToString();

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

            public int SuppliersMaster_SelectByItemCode(string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST] WHERE ITEM_CODE = " + ItemCode);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.SupName = dbManager.DataReader["SUP_NAME"].ToString();
                    this.SupContactPerson = dbManager.DataReader["SUP_CONTACT_PERSON"].ToString();
                    this.SupAddress = dbManager.DataReader["SUP_ADDRESS"].ToString();
                    this.SupContactPersonDetails = dbManager.DataReader["SUP_CONTACT_PER_DET"].ToString();
                    this.SupPhone = dbManager.DataReader["SUP_PHONE"].ToString();
                    this.SupMobile = dbManager.DataReader["SUP_MOBILE"].ToString();
                    this.SupEmail = dbManager.DataReader["SUP_EMAIL"].ToString();
                    this.SupFaxNo = dbManager.DataReader["SUP_FAXNO"].ToString();
                    this.SupPanNo = dbManager.DataReader["SUP_PANNO"].ToString();
                    this.SupCstNo = dbManager.DataReader["SUP_CSTNO"].ToString();
                    this.SupVatNo = dbManager.DataReader["SUP_VATNO"].ToString();
                    this.SupEccNo = dbManager.DataReader["SUP_ECCNO"].ToString();
                    this.SupRanking = dbManager.DataReader["SUP_RANKING"].ToString();
                    this.SupBrand = dbManager.DataReader["SUP_BRAND"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();

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

            public static void SuppliersMaster_Select(Control ControlForBind, string SupEnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST],[YANTRA_ENQ_SUPPLIERS] WHERE [YANTRA_SUPPLIER_MAST].SUP_ID = [YANTRA_ENQ_SUPPLIERS].SUP_ID AND [YANTRA_ENQ_SUPPLIERS].SUP_ENQ_ID=" + SupEnqId + " ORDER BY [YANTRA_SUPPLIER_MAST].SUP_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
               // dbManager.Close();
            }

            public string SuppliersDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //  if (IsRecordExists("[YANTRA_SUPPLIER_MAST]", "SUP_NAME", this.SupName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_SUPPLIER_DET] SELECT ISNULL(MAX(SUP_DET_ID),0)+1,{0},{1} FROM [YANTRA_SUPPLIER_DET]", this.SupId, this.ItemCode);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Suppliers Details", "76");

                    }
                }
                //else
                //{
                //    _returnStringMessage = "Supplier  Already Exists.";
                //}
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersDetails_Delete(string SupId)
            {
                if (DeleteRecord("[YANTRA_SUPPLIER_DET]", "SUP_ID", SupId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Suppliers Details", "76");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public void SuppliersDetails_Select(string SupId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_DET],[YANTRA_SUPPLIER_MAST],[YANTRA_ITEM_MAST] WHERE [YANTRA_SUPPLIER_DET].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_SUPPLIER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_SUPPLIER_DET].SUP_DET_ID=" + SupId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    SuppliersItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersItems;
                gv.DataBind();
               // dbManager.Close();
            }

            public string SuppliersUnit_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("INSERT INTO [YANTRA_SUPPLIER_UNITS] SELECT ISNULL(MAX(SUP_UNIT_ID),0)+1,'{0}','{1}',{2} FROM [YANTRA_SUPPLIER_UNITS]", this.unitname, this.unitaddress, this.SupId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Unit Details", "77");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersUnits_Delete(string SupId)
            {
                if (DeleteRecord("[YANTRA_SUPPLIER_UNITS]", "SUP_ID", SupId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Supplier Unit Details", "77");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public void SuppliersUnit_Select(string SupId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_UNITS],[YANTRA_SUPPLIER_MAST] WHERE [YANTRA_SUPPLIER_UNITS].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_SUPPLIER_UNITS].SUP_ID = " + SupId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable CustomerUnits = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("unitname");
                CustomerUnits.Columns.Add(col);
                col = new DataColumn("unitaddress");
                CustomerUnits.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["unitname"] = dbManager.DataReader["SUP_UNIT_NAME"].ToString();
                    dr["unitaddress"] = dbManager.DataReader["SUP_UNIT_ADDRESS"].ToString();
                    CustomerUnits.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = CustomerUnits;
                gv.DataBind();
               // dbManager.Close();
            }

            public static void SuppliersUnits_Select(Control ControlForBind, string SupUnitId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST],[YANTRA_SUPPLIER_UNITS] WHERE [YANTRA_SUPPLIER_MAST].SUP_ID = [YANTRA_SUPPLIER_UNITS].SUP_ID AND [YANTRA_SUPPLIER_UNITS].SUP_UNIT_ID=" + SupUnitId + " ORDER BY [YANTRA_SUPPLIER_UNITS].SUP_UNIT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_UNIT_NAME", "SUP_UNIT_ID");
                }
               // dbManager.Close();
            }


            public string SuppliersApprovalDetails_Save()
            {
                dbManager.Open();
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_SUPPLIER_APPROVALS] SELECT ISNULL(MAX(SUP_APPROVAL_ID),0)+1,{0},'{1}' FROM [YANTRA_SUPPLIER_APPROVALS]", this.SupId, this.SupBasisOfApproval);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Supplier Approval Details", "78");

                    }
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public void SuppliersApprovalDetails_Select(string SupId, CheckBoxList chklBasisOfApproval)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_APPROVALS] WHERE SUP_ID=" + SupId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    for (int i = 0; i < chklBasisOfApproval.Items.Count; i++)
                    {
                        if (chklBasisOfApproval.Items[i].Value == dbManager.DataReader["BASIS_OF_APPROVAL"].ToString())
                        {
                            chklBasisOfApproval.Items[i].Selected = true;
                        }
                    }
                }
                dbManager.DataReader.Close();
               // dbManager.Close();
            }

            public string SuppliersApprovalsDetails_Delete(string SupId)
            {
                if (DeleteRecord("[YANTRA_SUPPLIER_APPROVALS]", "SUP_ID", SupId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Supplier Approval Details", "78");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }
        }

        //Methods for Suppliers Quotation Form
        public class SuppliersQuotation
        {
            public string SupQuotId, CpId, SupQuotNo, SupQuotDate, SupId, SupName, SupQuotPOType, TransId = "0", SupQuotSTC, SupEnqId, SupSpPrice, SupQty, SupOtherSpec, SupExworks, SupDiscount;
            public string SupQuotDetId, ItemCode, UOM, SupQuotDetRate, SupQuotDetCurrency, SupQuotDetTax, SupQuotDetTaxType, SupQuotDetPFRate, SupQuotDetPFType, SupQuotDetExciseRate, SupQuotDetExciseType, SupQuotDetDisRate, SupQuotDetDisType, SupQuotDetMinDelPer, SupQuotDetMaxDelPer, SupQuotDetDelPer, SupQuotDetSpecs, SupQuotDetPayTerms, SupQuotDetLandedCost, SupQuotDetOldRate, SupQuotDetEnqid;
            public string SupQuotdetDiscount, SupQuotDetSpPrice, SupQuotDetArriDate, SupQuotDetInvoiceNo, SupQuotDetColorId, SupQuotDetReqFor, SupQuotDetQty, SupQuotDetIndId, SupQuotDetIndDetId;
            public string SupDelivery, SupEnqDate, SupPaymentTerms, SupPackingCharges, SupGuarantee, SupVat, SupValidity, SupInsurance, SupDepatchMode, SupTransportationCharges, SupFOB, SupCIF, SupIncluding, SupCST;
            public string QuotAttchment;
            public SuppliersQuotation()
            {
            }

            public static string SuppliersQuotation_AutoGenCode()
            {
                string _codePrefix = "SUPQUOT/";
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = "SELECT isnull(max(convert(bigint, (REPLACE(SUP_QUOT_NO,'SUPQUOT/','')))),0)+1  FROM [YANTRA_SUP_QUOT_MAST]";

                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
               // dbManager.Close();
                return _codePrefix + _returnIntValue;
            }
            public string SupNetAmount, SupTtlAmount;

            public string SuppliersQuotation_Save()
            {
                this.SupQuotNo = SuppliersQuotation_AutoGenCode();
                this.SupQuotId = AutoGenMaxId("[YANTRA_SUP_QUOT_MAST]", "SUP_QUOT_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_QUOT_MAST] SELECT ISNULL(MAX(SUP_QUOT_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},{23},{24} FROM [YANTRA_SUP_QUOT_MAST]", this.SupQuotNo, this.SupQuotDate, this.SupId, this.SupQuotPOType, this.TransId, this.SupQuotSTC, this.SupEnqId, this.SupDelivery, this.SupPaymentTerms, this.SupPackingCharges, this.SupCST, this.SupDepatchMode, this.SupGuarantee, this.SupValidity, this.SupInsurance, this.SupTransportationCharges, this.SupVat, this.SupCIF, this.SupFOB, this.SupOtherSpec, this.SupExworks, this.SupDiscount, this.CpId,this.SupNetAmount,this.SupTtlAmount);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Quotation Details", "79");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersQuotation_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SUP_QUOT_MAST] SET SUP_QUOT_DATE='{0}',SUP_ID={1},SUP_QUOT_PO_TYPE='{2}',TRANS_ID={3},SUP_QUOT_STC='{4}',SUP_ENQ_ID={5},SUP_QUOT_DELIVERY='{7}',SUP_QUOT_PAY_TERM='{8}',SUP_QUOT_PACK_CHARGES='{9}' ,SUP_QUOT_CST='{10}', DESPM_ID={11},SUP_QUOT_GUARANTEE='{12}',SUP_QUOT_VALIDITY='{13}',SUP_QUOT_INSURANCE='{14}',SUP_QUOT_TRANS_CHARGES='{15}',SUP_QUOT_VAT='{16}',SUP_QUOT_CIF='{17}',SUP_QUOT_FOB='{18}', SUP_QUOT_OTHER_SPEC='{19}',SUP_EXWORKS = '{20}',SUP_DISCOUNT='{21}',CP_ID={22},SUP_NETAMOUNT={23},SUP_TOTALAMOUNT={24} WHERE SUP_QUOT_ID={6}", this.SupQuotDate, this.SupId, this.SupQuotPOType, this.TransId, this.SupQuotSTC, this.SupEnqId, this.SupQuotId, this.SupDelivery, this.SupPaymentTerms, this.SupPackingCharges, this.SupCST, this.SupDepatchMode, this.SupGuarantee, this.SupValidity, this.SupInsurance, this.SupTransportationCharges, this.SupVat, this.SupCST, this.SupFOB, this.SupOtherSpec, this.SupExworks, this.SupDiscount, this.CpId,this.SupNetAmount,this.SupTtlAmount);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Supplier Quotation Details", "79");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersQuotation_Delete(string SupQuotationId)
            {
                if (DeleteRecord("[YANTRA_SUP_QUOT_DET]", "SUP_QUOT_ID", SupQuotationId) == true)
                {
                    if (DeleteRecord("[YANTRA_SUP_QUOT_MAST]", "SUP_QUOT_ID", SupQuotationId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Supplier Quotation Details", "79");

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

            public static void SuppliersQuotation_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SUP_QUOT_NO,SUP_QUOT_ID FROM [YANTRA_SUP_QUOT_MAST] ORDER BY SUP_QUOT_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_QUOT_NO", "SUP_QUOT_ID");
                }
               // dbManager.Close();
            }

            public static void SuppliersQuotationPI_Select(Control ControlForBind,string supid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SUP_QUOT_NO,SUP_QUOT_ID FROM [YANTRA_SUP_QUOT_MAST],YANTRA_SUPPLIER_MAST where YANTRA_SUPPLIER_MAST.SUP_ID = [YANTRA_SUP_QUOT_MAST].SUP_ID and [YANTRA_SUP_QUOT_MAST].SUP_ID = '"+supid+"' ORDER BY SUP_QUOT_ID DESC");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_QUOT_NO", "SUP_QUOT_ID");
                }
               // dbManager.Close();
            }

            public int SuppliersQuotation_Select(string SupQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_SUP_QUOT_MAST],[YANTRA_SUPPLIER_MAST],[YANTRA_LKUP_TRANS_MAST],YANTRA_SUP_ENQ_MAST WHERE [YANTRA_SUP_QUOT_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_SUP_QUOT_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID  and YANTRA_SUP_ENQ_MAST.SUP_ENQ_ID=[YANTRA_SUP_QUOT_MAST].SUP_ENQ_ID " +
                //                            " AND [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID='" + SupQuotationId + "' ORDER BY [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID DESC ");
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_QUOT_MAST],[YANTRA_SUPPLIER_MAST] WHERE [YANTRA_SUP_QUOT_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID " +
                                            " AND [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID='" + SupQuotationId + "' ORDER BY [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SupQuotId = dbManager.DataReader["SUP_QUOT_ID"].ToString();
                    this.SupQuotNo = dbManager.DataReader["SUP_QUOT_NO"].ToString();
                    this.SupQuotDate = Convert.ToDateTime(dbManager.DataReader["SUP_QUOT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.SupName = dbManager.DataReader["SUP_NAME"].ToString();
                    this.SupQuotPOType = dbManager.DataReader["SUP_QUOT_PO_TYPE"].ToString();
                    this.TransId = dbManager.DataReader["TRANS_ID"].ToString();
                    this.SupQuotSTC = dbManager.DataReader["SUP_QUOT_STC"].ToString();
                    this.SupEnqId = dbManager.DataReader["SUP_ENQ_ID"].ToString();
                    this.SupDelivery = dbManager.DataReader["SUP_QUOT_DELIVERY"].ToString();
                    this.SupPaymentTerms = dbManager.DataReader["SUP_QUOT_PAY_TERM"].ToString();
                    this.SupPackingCharges = dbManager.DataReader["SUP_QUOT_PACK_CHARGES"].ToString();
                    this.SupVat = dbManager.DataReader["SUP_QUOT_VAT"].ToString();
                    this.SupGuarantee = dbManager.DataReader["SUP_QUOT_GUARANTEE"].ToString();
                    this.SupValidity = dbManager.DataReader["SUP_QUOT_VALIDITY"].ToString();
                    this.SupInsurance = dbManager.DataReader["SUP_QUOT_INSURANCE"].ToString();
                    this.SupTransportationCharges = dbManager.DataReader["SUP_QUOT_TRANS_CHARGES"].ToString();
                    this.SupCIF = dbManager.DataReader["SUP_QUOT_CIF"].ToString();
                    this.SupFOB = dbManager.DataReader["SUP_QUOT_FOB"].ToString();
                    this.SupCST = dbManager.DataReader["SUP_QUOT_CST"].ToString();
                    this.SupDepatchMode = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SupOtherSpec = dbManager.DataReader["SUP_QUOT_OTHER_SPEC"].ToString();
                    this.SupExworks = dbManager.DataReader["SUP_EXWORKS"].ToString();
                    this.SupDiscount = dbManager.DataReader["SUP_DISCOUNT"].ToString();
                    this.SupNetAmount = dbManager.DataReader["SUP_NETAMOUNT"].ToString();
                    this.SupTtlAmount = dbManager.DataReader["SUP_TOTALAMOUNT"].ToString();
                  
                    //this.SupEnqDate = dbManager.DataReader["SUP_ENQ_DATE"].ToString();
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

            
            public string SupplierQuotAttachment_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_QUOT_ATTACHMENTS] SELECT ISNULL(MAX(SUP_QUOT_ATTACHMENT_ID),0)+1,{0},'{1}' FROM [YANTRA_SUP_QUOT_ATTACHMENTS]", this.SupQuotId, this.QuotAttchment);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
               // dbManager.Close();
                return _returnStringMessage;

            }

            public int SupplierQuotAttachment_Delete(string SupQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SUP_QUOT_ATTACHMENTS] WHERE SUP_QUOT_ID={0}", SupQuotationId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

           
            public string SuppliersQuotationDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_QUOT_DET] SELECT ISNULL(MAX(SUP_QUOT_DET_ID),0)+1,{0},{1},'{2}',{3},'{4}','{5}','{6}','{7}',{8},'{9}','{10}',{11},{12} FROM [YANTRA_SUP_QUOT_DET]", this.SupQuotId, this.ItemCode, this.SupQuotDetRate, this.SupQuotDetCurrency, this.SupQuotdetDiscount,this.SupQuotDetSpPrice,this.SupQuotDetArriDate,this.SupQuotDetInvoiceNo,this.SupQuotDetColorId,this.SupQuotDetReqFor,this.SupQuotDetQty,this.SupQuotDetIndId,this.SupQuotDetIndDetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Quotation Details", "79");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int SuppliersQuotationDetails_Delete(string SupQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SUP_QUOT_DET] WHERE SUP_QUOT_ID={0}", SupQuotationId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public void SuppliersQuotationDetails_Select(string SupQuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_QUOT_DET],[YANTRA_SUP_QUOT_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_PRODUCT_COMPANY],[YANTRA_LKUP_CURRENCY_TYPE] WHERE [YANTRA_SUP_QUOT_DET].SUP_QUOT_DET_CURRENCY = [YANTRA_LKUP_CURRENCY_TYPE].CURRENCY_ID and [YANTRA_ITEM_MAST].BRAND_ID= [YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID and [YANTRA_SUP_QUOT_DET].SUP_QUOT_ID=[YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID AND [YANTRA_SUP_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_SUP_QUOT_DET].SUP_QUOT_ID=" + SupQuotationId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("ModelName");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Brand");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Specification");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("SpRate");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Curency");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("CurencyId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("Delivarydate");
                SuppliersQuotationItems.Columns.Add(col);
                //col = new DataColumn("EnqNo");
                //SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("IndentId");
                SuppliersQuotationItems.Columns.Add(col);
                col = new DataColumn("IndentDetId");
                SuppliersQuotationItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["ModelName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SUP_QUOT_DET_QTY"].ToString();
                    dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    dr["Curency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["Rate"] = dbManager.DataReader["SUP_QUOT_DET_RATE"].ToString();
                    dr["Disc"] = dbManager.DataReader["SUP_QUOT_DISCOUNT"].ToString();
                    dr["SpRate"] = dbManager.DataReader["SUP_QUOT_DET_SP_PRICE"].ToString();
                    dr["Specification"] = dbManager.DataReader["ITEM_SPEC"].ToString();
                    dr["CurencyId"] = dbManager.DataReader["SUP_QUOT_DET_CURRENCY"].ToString();
                   // dr["oldrate"] = dbManager.DataReader["SUP_QUOT_OLDRATE"].ToString();
                    dr["Delivarydate"] = dbManager.DataReader["SUP_QUOT_DET_ARRIVAL_DATE"].ToString();
                   // dr["EnqNo"] = dbManager.DataReader["SUP_QUOT_ENQ_NO"].ToString();
                    dr["IndentId"] = dbManager.DataReader["SUP_QUOT_IND_ID"].ToString();
                    dr["IndentDetId"] = dbManager.DataReader["SUP_QUOT_IND_DET_ID"].ToString();


                    SuppliersQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersQuotationItems;
                gv.DataBind();
               // dbManager.Close();
            }

            public static string SupEnqStatus_Update(SCMStatus Status, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SUP_ENQ_DET] SET  SUP_ENQ_STATUS='{0}' WHERE SUP_ENQ_ID='{1}'", Status, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Supplier Enquiry Status Details", "80");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
        }
        //Methods for PO Scheduling Form
        public class POScheduling
        {
            public string POSId, POSNo, POSDate, PODate, FPOPOType, FPOId, PONo, SupId, POSPreparedBy, POSApprovedBy, POSStatus;
            public string POSDetId, ItemCode, ItemName, UOM, POSDetRate, POSDetQty, POSDetSchQty;
            public string POSDetSchDate;

            public POScheduling()
            {
            }

            public static string POScheduling_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(POS_NO,LEFT(POS_NO,5),''))),0)+1 FROM [YANTRA_PO_SCHEDULING_MAST]").ToString());
               // dbManager.Close();

                return _codePrefix + _returnIntValue;
            }

            public string POScheduling_Save()
            {
                this.POSNo = POScheduling_AutoGenCode();
                this.POSId = AutoGenMaxId("[YANTRA_PO_SCHEDULING_MAST]", "POS_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_PO_SCHEDULING_MAST] VALUES({0},'{1}','{2}',{3},'{4}','{5}','{6}')", this.POSId, this.POSNo, this.POSDate, this.FPOId, this.POSPreparedBy, this.POSApprovedBy, this.POSStatus);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("PO Schedule Details", "81");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string POScheduling_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PO_SCHEDULING_MAST] SET POS_DATE='{0}',FPO_ID={1}, POS_PREPARED_BY='{2}', POS_APPROVED_BY='{3}', POS_STATUS='{4}' WHERE POS_ID='{5}'", this.POSDate, this.FPOId, this.POSPreparedBy, this.POSApprovedBy, this.POSStatus, this.POSId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("PO Schedule Details", "81");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string POScheduling_Delete(string POSchedulingId)
            {
                if (DeleteRecord("[YANTRA_PO_SCHEDULING_DET]", "POS_ID", POSchedulingId) == true)
                {
                    if (DeleteRecord("[YANTRA_PO_SCHEDULING_MAST]", "POS_ID", POSchedulingId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("PO Schedule Details", "81");

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

            public static void POScheduling_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PO_SCHEDULING_MAST] ORDER BY POS_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "POS_NO", "POS_ID");
                }
               // dbManager.Close();
            }

            public int POScheduling_Select(string POSchedulingId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PO_SCHEDULING_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_SUP_QUOT_MAST] WHERE [YANTRA_PO_SCHEDULING_MAST].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID=[YANTRA_FIXED_PO_MAST].SUP_QUOT_ID " +
                                            " AND [YANTRA_PO_SCHEDULING_MAST].POS_ID='" + POSchedulingId + "' ORDER BY [YANTRA_PO_SCHEDULING_MAST].POS_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.POSId = dbManager.DataReader["POS_ID"].ToString();
                    this.POSNo = dbManager.DataReader["POS_NO"].ToString();
                    this.POSDate = Convert.ToDateTime(dbManager.DataReader["POS_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    this.PODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FPOPOType = dbManager.DataReader["FPO_PO_TYPE"].ToString();
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.POSPreparedBy = dbManager.DataReader["POS_PREPARED_BY"].ToString();
                    this.POSApprovedBy = dbManager.DataReader["POS_APPROVED_BY"].ToString();
                    this.POSStatus = dbManager.DataReader["POS_STATUS"].ToString();

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

            public string POSchedulingDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_PO_SCHEDULING_DET] SELECT ISNULL(MAX(POS_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}' FROM [YANTRA_PO_SCHEDULING_DET]",
                                                                                                                        this.POSId, this.ItemCode, this.POSDetRate, this.POSDetQty, this.POSDetSchQty, this.POSDetSchDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("PO Schedule Details", "81");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int POSchedulingDetails_Delete(string POSchedulingId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_PO_SCHEDULING_DET] WHERE POS_ID={0}", POSchedulingId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public void POSchedulingDetails_Select(string POSchedulingId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PO_SCHEDULING_DET],[YANTRA_PO_SCHEDULING_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_PO_SCHEDULING_DET].POS_ID=[YANTRA_PO_SCHEDULING_MAST].POS_ID AND [YANTRA_PO_SCHEDULING_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID   AND [YANTRA_PO_SCHEDULING_DET].POS_ID=" + POSchedulingId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable POSchedulingItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                POSchedulingItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                POSchedulingItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                POSchedulingItems.Columns.Add(col);
                col = new DataColumn("UOM");
                POSchedulingItems.Columns.Add(col);
                col = new DataColumn("Rate");
                POSchedulingItems.Columns.Add(col);
                col = new DataColumn("POQuantity");
                POSchedulingItems.Columns.Add(col);
                col = new DataColumn("SchQuantity");
                POSchedulingItems.Columns.Add(col);
                //col = new DataColumn("GrandAmt");
                //POSchedulingItems.Columns.Add(col);
                col = new DataColumn("SchDate");
                POSchedulingItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = POSchedulingItems.NewRow();

                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();

                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Rate"] = dbManager.DataReader["POS_DET_RATE"].ToString();
                    dr["POQuantity"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                    dr["SchQuantity"] = dbManager.DataReader["POS_DET_SCH_QTY"].ToString();
                    //dr["GrandAmt"] = dbManager.DataReader["SUP_QUOT_DET_PF_RATE"].ToString();
                    dr["SchDate"] = dbManager.DataReader["POS_DET_SCH_DATE"].ToString();

                    POSchedulingItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = POSchedulingItems;
                gv.DataBind();
               // dbManager.Close();
            }
        }

        //Methods for Fixed Purchase Order
        public class SupplierFixedPO
        {
            public string FPOId, FPONo, FPODate, FPOSuppRef, FPOPOStatus, FPONetAmount, FPOAmtWords, FPOTermsConds, DespmId, SupQuotId, SupQuotNo, SupQuotDate, IndApprlId, IndApprlNo, IndApprlDate, SupId, FPOPaymentTerms, CurrencyId, FPONetAmtInOtherCurrency, FPODestination, FPOInsurance, FPOFreight, FPOTermsOfDelivery, FPODiscount, FPOTaxCST, FPOContactPerson, PreparedBy, ApprovedBy, FPOCIFCharges, FPOFOBCharges, FPOSuppContactPerson, FPOCurrencyType, FPOSuppDisc, FPOSuppSpPrice, CpId, FPOTotalAmt;
            public string FPODetId, ItemCode, FPODetRate, FPODetQty, FPODetDeliveryDate, FPODetSpec, FPODetRemarks, FPODetTax,FPODetIndId,FPODetIndDetId;
            public string Remitance,Insurance,PackingCharges,CIFCharges,FOBCharges;
            public SupplierFixedPO()
            {
            }

            public static string SuppliersFixedPO_AutoGenCode()
            {
                ////string _codePrefix = CurrentFinancialYear() + " ";
                //string _codePrefix = "FPO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT   ISNULL(MAX(CONVERT(BIGINT,REPLACE(FPO_NO,LEFT(FPO_NO,5),''))),0)+1 FROM [YANTRA_FIXED_PO_MAST]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(FPO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_FIXED_PO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_FIXED_PO_MAST", "FPO_NO");
            }

            public string SuppliersFixedPO_Save()
            {
                this.FPONo = SuppliersFixedPO_AutoGenCode();
                this.FPOId = AutoGenMaxId("[YANTRA_FIXED_PO_MAST]", "FPO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_FIXED_PO_MAST] VALUES({0},'{1}','{2}','-',{3},{4},{5},'{6}','{7}',{8},'{9}','{10}',{11},'{12}',{13},'{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}',{28},'{29}',{30},{31},{32},{33},{34},'{35}','{36}') ", this.FPOId, this.FPONo, this.FPODate, this.SupQuotId, this.IndApprlId, this.SupId, this.FPOSuppRef, this.FPOPOStatus, this.FPONetAmount, this.FPOAmtWords, this.FPOTermsConds, this.DespmId, this.FPOPaymentTerms, this.CurrencyId, this.FPONetAmtInOtherCurrency, this.FPODestination, this.FPOInsurance, this.FPOFreight, this.FPODiscount, this.FPOTaxCST, this.FPOTermsOfDelivery, this.FPOContactPerson, this.PreparedBy, this.ApprovedBy, this.FPOCIFCharges, this.FPOFOBCharges, this.FPOSuppContactPerson, this.FPOCurrencyType, this.CpId, this.Remitance,this.Insurance,this.CIFCharges,this.FOBCharges,this.PackingCharges,this.FPOTotalAmt,DateTime.Now,DateTime.Now);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Fixed PO Details", "82");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersFixedPO_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FIXED_PO_MAST] SET FPO_DATE='{0}',FPO_SUPP_REF='{1}', FPO_PO_STATUS='{2}', FPO_NET_AMOUNT={3}, FPO_AMT_WORDS='{4}', FPO_TERMS_COND='{5}',IND_APPRL_ID={6},SUP_ID={7},FPO_PAYMENT_TERMS='{8}',FPO_CURRENCY_ID={9},FPO_NET_AMOUNT_IN_OTHER_CURRENCY='{10}',FPO_DESTINATION='{11}',FPO_INSURANCE='{12}',FPO_FREIGHT='{13}',FPO_DISCOUNT='{14}',FPO_TAXCST='{15}',FPO_TERMS_OF_DELIVERY='{16}',FPO_CONTACTPERSON='{17}',PREPAREDBY='{18}',FPO_CIF='{19}',FPO_FOB='{20}',FPO_SUPP_CONTACT_PERSON='{21}',FPO_CURRENCY_TYPE='{22}',CP_ID={24},REMITANCE = '{25}',INSURANCE={26},CIF_CHARGES={27},FOB_CHARGES={28},PACKING_CHARGES={29},TotalAmt={30},DateModified ='{31}' WHERE FPO_ID='{23}' and CP_ID={24}", this.FPODate, this.FPOSuppRef, this.FPOPOStatus, this.FPONetAmount, this.FPOAmtWords, this.FPOTermsConds, this.IndApprlId, this.SupId, this.FPOPaymentTerms, this.CurrencyId, this.FPONetAmtInOtherCurrency, this.FPODestination, this.FPOInsurance, this.FPOFreight, this.FPODiscount, this.FPOTaxCST, this.FPOTermsOfDelivery, this.FPOContactPerson, this.PreparedBy, this.FPOCIFCharges, this.FPOFOBCharges, this.FPOSuppContactPerson, this.FPOCurrencyType, this.FPOId, this.CpId, this.Remitance, this.Insurance, this.CIFCharges, this.FOBCharges, this.PackingCharges, this.FPOTotalAmt,DateTime.Now);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Supplier Fixed PO Details", "82");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersFixedPOApprove_Update(string ApprovedBy, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FIXED_PO_MAST] SET APPROVEDBY='{0}'   WHERE FPO_ID='{1}'", ApprovedBy, FPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Approved Successfully";
                    log.add_Update("Supplier Fixed PO Approve Details", "82");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }



        





            public string SuppliersFixedPOStatus_Update(string FPOStatus, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FIXED_PO_MAST] SET FPO_PO_STATUS='{0}'   WHERE FPO_ID='{1}'", FPOStatus, FPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Supplier Fixed PO Status Details", "82");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersFixedPO_Delete(string SupFixedPOId)
            {
                if (DeleteRecord("[YANTRA_FIXED_PO_DET]", "FPO_ID", SupFixedPOId) == true)
                {
                    if (DeleteRecord("[YANTRA_FIXED_PO_MAST]", "FPO_ID", SupFixedPOId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Supplier Fixed PO Details", "82");

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
               // dbManager.Close();
                return _returnStringMessage;
            }

            public static void SuppliersFixedPO_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT FPO_NO,FPO_ID FROM [YANTRA_FIXED_PO_MAST] ORDER BY FPO_ID desc");
                
                //_commandText = string.Format("SELECT FPO_NO,FPO_ID FROM [YANTRA_FIXED_PO_MAST] where [FPO_ID] not in (select [FPO_ID] from [YANTRA_PURCHASE_INVOICE_MAST]) ORDER BY FPO_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "FPO_NO", "FPO_ID");
                }
               // dbManager.Close();
            }

            public static void SuppliersPI_Select_MRN(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT PI_NO,PI_ID FROM [YANTRA_PURCHASE_INVOICE_MAST] ORDER BY PI_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PI_NO", "PI_ID");
                }
                // dbManager.Close();
            }


            public static void SuppliersFixedPO_Select_MRN(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT FPO_NO,FPO_ID FROM [YANTRA_FIXED_PO_MAST] ORDER BY FPO_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "FPO_NO", "FPO_ID");
                }
                // dbManager.Close();
            }

            public static void SuppliersPO_Select(Control ControlForBind,string POId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT FPO_NO,FPO_ID FROM [YANTRA_FIXED_PO_MAST],YANTRA_SUPPLIER_MAST where YANTRA_SUPPLIER_MAST.SUP_ID = [YANTRA_FIXED_PO_MAST].SUP_ID and [YANTRA_FIXED_PO_MAST].SUP_ID = '" + POId + "' ORDER BY FPO_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "FPO_NO", "FPO_ID");
                }
               // dbManager.Close();
            }
            public string PIDate;
            public int SuppliersPI_Select(string PIId)
            {
                dbManager.Open();
                _commandText = string.Format("select * from  YANTRA_PURCHASE_INVOICE_MAST left outer join YANTRA_SUPPLIER_MAST on YANTRA_PURCHASE_INVOICE_MAST .SUP_ID =YANTRA_SUPPLIER_MAST .SUP_ID WHERE [YANTRA_PURCHASE_INVOICE_MAST].PI_ID='" + PIId + "' ORDER BY YANTRA_PURCHASE_INVOICE_MAST.PI_ID  DESC ");
               dbManager.ExecuteReader(CommandType.Text, _commandText);
               if (dbManager.DataReader.Read())
               {
                   this.FPOId = dbManager.DataReader["PI_ID"].ToString();
                   this.FPONo = dbManager.DataReader["PI_NO"].ToString();
                   this.FPODate = Convert.ToDateTime(dbManager.DataReader["PI_CUST_INV_DATE"].ToString()).ToString("dd/MM/yyyy");
                   this.PIDate = Convert.ToDateTime(dbManager.DataReader["PI_CUST_INV_DATE"].ToString()).ToString("dd/MM/yyyy");
                   
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
            public int SuppliersFixedPO_Select(string SupFixedPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FIXED_PO_MAST] inner join [YANTRA_SUPPLIER_MAST] on [YANTRA_SUPPLIER_MAST].SUP_ID=[YANTRA_FIXED_PO_MAST].SUP_ID left outer join [YANTRA_SUP_QUOT_MAST] on [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID=[YANTRA_FIXED_PO_MAST].SUP_QUOT_ID left outer join [YANTRA_INDENT_MAST] ON [YANTRA_FIXED_PO_MAST].IND_APPRL_ID=[YANTRA_INDENT_MAST].IND_ID WHERE [YANTRA_FIXED_PO_MAST].FPO_ID='" + SupFixedPOId + "' ORDER BY [YANTRA_FIXED_PO_MAST].FPO_ID DESC ");
                //_commandText = string.Format("SELECT * FROM [YANTRA_SUP_QUOT_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_LKUP_DESP_MODE] WHERE [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID=[YANTRA_FIXED_PO_MAST].SUP_QUOT_ID AND " +
                //"[YANTRA_LKUP_DESP_MODE].DESPM_ID=[YANTRA_FIXED_PO_MAST].DESPM_ID AND [YANTRA_FIXED_PO_MAST].FPO_ID='" + SupFixedPOId + "' ORDER BY [YANTRA_FIXED_PO_MAST].FPO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    this.FPONo = dbManager.DataReader["FPO_NO"].ToString();
                    this.FPODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SupQuotId = dbManager.DataReader["SUP_QUOT_ID"].ToString();
                    this.SupQuotNo = dbManager.DataReader["SUP_QUOT_NO"].ToString();
                    this.FPOSuppRef = dbManager.DataReader["FPO_SUPP_REF"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.FPOPOStatus = dbManager.DataReader["FPO_PO_STATUS"].ToString();
                    this.FPONetAmount = dbManager.DataReader["FPO_NET_AMOUNT"].ToString();
                    this.FPOAmtWords = dbManager.DataReader["FPO_AMT_WORDS"].ToString();
                    this.FPOTermsConds = dbManager.DataReader["FPO_TERMS_COND"].ToString();
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.IndApprlId = dbManager.DataReader["IND_ID"].ToString();
                    this.IndApprlNo = dbManager.DataReader["IND_NO"].ToString();
                    this.IndApprlDate = dbManager.DataReader["IND_DATE"].ToString();
                    this.FPOPaymentTerms = dbManager.DataReader["FPO_PAYMENT_TERMS"].ToString();
                    this.CurrencyId = dbManager.DataReader["FPO_CURRENCY_ID"].ToString();
                    this.FPONetAmtInOtherCurrency = dbManager.DataReader["FPO_NET_AMOUNT_IN_OTHER_CURRENCY"].ToString();
                    this.FPODestination = dbManager.DataReader["FPO_DESTINATION"].ToString();
                    this.FPOInsurance = dbManager.DataReader["FPO_INSURANCE"].ToString();
                    this.FPOFreight = Convert.ToDateTime(dbManager.DataReader["FPO_FREIGHT"].ToString()).ToString("dd/MM/yyyy"); 
                    this.FPODiscount = dbManager.DataReader["FPO_DISCOUNT"].ToString();
                    this.FPOTaxCST = dbManager.DataReader["FPO_TAXCST"].ToString();
                    this.FPOTermsOfDelivery = dbManager.DataReader["FPO_TERMS_OF_DELIVERY"].ToString();
                    this.FPOContactPerson = dbManager.DataReader["FPO_CONTACTPERSON"].ToString();
                    this.PreparedBy = dbManager.DataReader["PREPAREDBY"].ToString();
                    this.ApprovedBy = dbManager.DataReader["APPROVEDBY"].ToString();
                    this.FPOCIFCharges = dbManager.DataReader["FPO_CIF"].ToString();
                    this.FPOFOBCharges = dbManager.DataReader["FPO_FOB"].ToString();
                    this.FPOSuppContactPerson = dbManager.DataReader["FPO_SUPP_CONTACT_PERSON"].ToString();
                    this.FPOCurrencyType = dbManager.DataReader["FPO_CURRENCY_TYPE"].ToString();
                    this.Insurance = dbManager.DataReader["INSURANCE"].ToString();
                    this.CIFCharges = dbManager.DataReader["CIF_CHARGES"].ToString();
                    this.FOBCharges = dbManager.DataReader["FOB_CHARGES"].ToString();
                    this.PackingCharges = dbManager.DataReader["PACKING_CHARGES"].ToString();
                    this.FPOTotalAmt = dbManager.DataReader["TotalAmt"].ToString();
                    this.FPOTermsConds = dbManager.DataReader["FPO_TERMS_COND"].ToString();
                  
                    if (this.IndApprlDate != "") { this.IndApprlDate = Convert.ToDateTime(this.IndApprlDate).ToString("dd/MM/yyyy"); }
                    this.Remitance = dbManager.DataReader["REMITANCE"].ToString();
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
            public string CustId, CustName, DeliveryDate,Remarks;
            public int POCustomer_Select(string POId, string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select ind.CusId,cust.CUST_NAME from dbo.YANTRA_CUSTOMER_MAST cust inner join dbo.YANTRA_INDENT_MAST ind on cust.CUST_ID=ind.CusId inner join YANTRA_FIXED_PO_DET po on po.FPO_DET_IND_ID=ind.IND_ID left outer join YANTRA_PURCHASE_INVOICE_DET on YANTRA_PURCHASE_INVOICE_DET.PI_PONo  =po.FPO_DET_ID " +
                                             //" on cust.CUST_ID=ind.CusId inner join YANTRA_FIXED_PO_DET po on po.FPO_DET_IND_ID=ind.IND_ID inner join YANTRA_FIXED_PO_MAST on po .FPO_ID =YANTRA_FIXED_PO_MAST .FPO_ID left outer join  YANTRA_PURCHASE_INVOICE_MAST on YANTRA_PURCHASE_INVOICE_MAST .FPO_ID =YANTRA_FIXED_PO_MAST .FPO_ID left outer join YANTRA_PURCHASE_INVOICE_DET on YANTRA_PURCHASE_INVOICE_DET .PI_ID =YANTRA_PURCHASE_INVOICE_MAST .PI_ID" +
                                             " where  YANTRA_PURCHASE_INVOICE_DET .PI_DET_Customer =cust.cust_name and YANTRA_PURCHASE_INVOICE_DET.Item_Code='" + ItemCode + "' and YANTRA_PURCHASE_INVOICE_DET.PI_ID='" + POId + "'");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.POSId = dbManager.DataReader["POS_ID"].ToString();
                    this.CustId = dbManager.DataReader["CusId"].ToString();
                    this.CustName = dbManager.DataReader["CUST_NAME"].ToString();
                    this.Remarks = dbManager.DataReader["CUST_NAME"].ToString();

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

            public int PODeliveryDate_Select(string POId, string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select so.SO_DET_DELIVERY_DATE from dbo.YANTRA_INDENT_MAST ind inner join YANTRA_FIXED_PO_DET po on po.FPO_DET_IND_ID=ind.IND_ID " +
                                             "inner join dbo.YANTRA_SO_DET so on ind.IND_SO_ID=so.SO_ID and so.Item_Code=po.ITEM_CODE  where po.ITEM_CODE='" + ItemCode + "' and po.FPO_ID='" + POId + "'");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.DeliveryDate = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");

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
            public string colorid, CurrencyType, BalanceQty, FPODetGSTAmt,FPODetFright,FPODetInsurance;
            public string SuppliersFixedPODetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_FIXED_PO_DET] SELECT ISNULL(MAX(FPO_DET_ID),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}',{13},'{14}',{15},{16},{17},{18} FROM [YANTRA_FIXED_PO_DET]", this.FPOId, this.ItemCode, this.FPODetRate, this.FPODetQty, this.FPODetDeliveryDate, this.FPODetSpec, this.FPODetRemarks, this.FPODetTax, this.FPOSuppDisc, this.FPOSuppSpPrice, this.FPODetIndId, this.FPODetIndDetId, "New", this.colorid, this.CurrencyType, this.BalanceQty, this.FPODetGSTAmt,this.FPODetFright,FPODetInsurance );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Fixed PO Details", "82");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int SuppliersFixedPODetails_Delete(string SupFixedPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_FIXED_PO_DET] WHERE FPO_ID={0}", SupFixedPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }
            public int SupItemsFixedPO_Select(string SupItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_FIXED_PO_DET ,YANTRA_FIXED_PO_MAST ,YANTRA_ITEM_MAST where YANTRA_FIXED_PO_DET .FPO_ID =YANTRA_FIXED_PO_MAST .FPO_ID and YANTRA_FIXED_PO_DET .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE and YANTRA_FIXED_PO_DET .ITEM_CODE ='" + SupItemCode + "' Order By YANTRA_FIXED_PO_DET.FPO_DET_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    this.FPONo = dbManager.DataReader["FPO_NO"].ToString();
                    this.FPODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SupQuotId = dbManager.DataReader["SUP_QUOT_ID"].ToString();
                    //this.SupQuotNo = dbManager.DataReader["SUP_QUOT_NO"].ToString();
                    this.FPOSuppRef = dbManager.DataReader["FPO_SUPP_REF"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.FPOPOStatus = dbManager.DataReader["FPO_PO_STATUS"].ToString();
                    this.FPONetAmount = dbManager.DataReader["FPO_NET_AMOUNT"].ToString();
                    this.FPOAmtWords = dbManager.DataReader["FPO_AMT_WORDS"].ToString();
                    this.FPOTermsConds = dbManager.DataReader["FPO_TERMS_COND"].ToString();
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    //this.IndApprlId = dbManager.DataReader["IND_ID"].ToString();
                    //this.IndApprlNo = dbManager.DataReader["IND_NO"].ToString();
                    //this.IndApprlDate = dbManager.DataReader["IND_DATE"].ToString();
                    this.FPOPaymentTerms = dbManager.DataReader["FPO_PAYMENT_TERMS"].ToString();
                    this.CurrencyId = dbManager.DataReader["FPO_CURRENCY_ID"].ToString();
                    this.FPONetAmtInOtherCurrency = dbManager.DataReader["FPO_NET_AMOUNT_IN_OTHER_CURRENCY"].ToString();
                    this.FPODestination = dbManager.DataReader["FPO_DESTINATION"].ToString();
                    this.FPOInsurance = dbManager.DataReader["FPO_INSURANCE"].ToString();
                    this.FPOFreight = Convert.ToDateTime(dbManager.DataReader["FPO_FREIGHT"].ToString()).ToString("dd/MM/yyyy");
                    this.FPODiscount = dbManager.DataReader["FPO_DISCOUNT"].ToString();
                    this.FPOTaxCST = dbManager.DataReader["FPO_TAXCST"].ToString();
                    this.FPOTermsOfDelivery = dbManager.DataReader["FPO_TERMS_OF_DELIVERY"].ToString();
                    this.FPOContactPerson = dbManager.DataReader["FPO_CONTACTPERSON"].ToString();
                    this.PreparedBy = dbManager.DataReader["PREPAREDBY"].ToString();
                    this.ApprovedBy = dbManager.DataReader["APPROVEDBY"].ToString();
                    this.FPOCIFCharges = dbManager.DataReader["FPO_CIF"].ToString();
                    this.FPOFOBCharges = dbManager.DataReader["FPO_FOB"].ToString();
                    this.FPOSuppContactPerson = dbManager.DataReader["FPO_SUPP_CONTACT_PERSON"].ToString();
                    this.FPOCurrencyType = dbManager.DataReader["FPO_CURRENCY_TYPE"].ToString();
                    this.Insurance = dbManager.DataReader["INSURANCE"].ToString();
                    this.CIFCharges = dbManager.DataReader["CIF_CHARGES"].ToString();
                    this.FOBCharges = dbManager.DataReader["FOB_CHARGES"].ToString();
                    this.PackingCharges = dbManager.DataReader["PACKING_CHARGES"].ToString();
                    this.FPOTotalAmt = dbManager.DataReader["TotalAmt"].ToString();
                    this.FPOTermsConds = dbManager.DataReader["FPO_TERMS_COND"].ToString();

                    //if (this.IndApprlDate != "") { this.IndApprlDate = Convert.ToDateTime(this.IndApprlDate).ToString("dd/MM/yyyy"); }
                    //this.Remitance = dbManager.DataReader["REMITANCE"].ToString();
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
            public void SuppliersFixedPODetails_Select1(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FIXED_PO_DET],[YANTRA_FIXED_PO_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_COLOR_MAST,[YANTRA_INDENT_DET] WHERE [YANTRA_FIXED_PO_DET].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_FIXED_PO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                              "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_INDENT_DET].[IND_DET_ID]=YANTRA_FIXED_PO_DET.[FPO_DET_IND_DET_ID] AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID=YANTRA_FIXED_PO_DET.Color_Id AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID   AND [YANTRA_FIXED_PO_DET].FPO_ID=" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("Color");
                //SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GST");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("HSNCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("FPO_DET_ID");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("FPO_ID");
                SuppliersFixedPOItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Specifications"] = dbManager.DataReader["FPO_DET_SPECIFICATION"].ToString();
                    dr["Remarks"] = dbManager.DataReader["FPO_DET_REMARKS"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["FPO_DET_SPPRICE"].ToString();
                    dr["Disc"] = dbManager.DataReader["FPO_DET_DISC"].ToString();
                    //dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Customer"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
                    dr["ColorId"] = dbManager.DataReader["Color_Id"].ToString();
                    dr["BalanceQty"] = dbManager.DataReader["Balance_QTY"].ToString();
                    dr["GST"] = dbManager.DataReader["GST TAX"].ToString();
                    dr["HSNCode"] = dbManager.DataReader["HSN_Code"].ToString();
                    dr["FPO_DET_ID"] = dbManager.DataReader["FPO_DET_ID"].ToString();
                    dr["FPO_ID"] = dbManager.DataReader["FPO_ID"].ToString();
                    SuppliersFixedPOItems.Rows.Add(dr);

                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
                // dbManager.Close();
            }
            public void SuppliersFixedPODetails_Select(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_FIXED_PO_DET],[YANTRA_FIXED_PO_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_COLOR_MAST,[YANTRA_INDENT_DET] WHERE [YANTRA_FIXED_PO_DET].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_FIXED_PO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                              "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_INDENT_DET].[IND_DET_ID]=YANTRA_FIXED_PO_DET.[FPO_DET_IND_DET_ID] AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID=YANTRA_FIXED_PO_DET.Color_Id AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID   AND [YANTRA_FIXED_PO_DET].ITEM_CODE=" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("Color");
                //SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GST");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("HSNCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("FPO_DET_ID");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("FPO_ID");
                SuppliersFixedPOItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Specifications"] = dbManager.DataReader["FPO_DET_SPECIFICATION"].ToString();
                    dr["Remarks"] = dbManager.DataReader["FPO_DET_REMARKS"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["FPO_DET_SPPRICE"].ToString();
                    dr["Disc"] = dbManager.DataReader["FPO_DET_DISC"].ToString();
                    //dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Customer"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
                    dr["ColorId"] = dbManager.DataReader["Color_Id"].ToString();
                    dr["BalanceQty"] = dbManager.DataReader["Balance_QTY"].ToString();
                    dr["GST"] = dbManager.DataReader["GST TAX"].ToString();
                    dr["HSNCode"] = dbManager.DataReader["HSN_Code"].ToString();
                    dr["FPO_DET_ID"] = dbManager.DataReader["FPO_DET_ID"].ToString();
                    dr["FPO_ID"] = dbManager.DataReader["FPO_ID"].ToString();
                    SuppliersFixedPOItems.Rows.Add(dr);
             
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
               // dbManager.Close();
            }

            public void SuppliersFixedPIDetails_Select(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_PURCHASE_INVOICE_DET inner join YANTRA_PURCHASE_INVOICE_MAST on YANTRA_PURCHASE_INVOICE_DET .PI_ID =YANTRA_PURCHASE_INVOICE_MAST .PI_ID inner join YANTRA_ITEM_MAST on YANTRA_PURCHASE_INVOICE_DET .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE inner join YANTRA_LKUP_ITEM_TYPE on YANTRA_ITEM_MAST .IT_TYPE_ID =YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID inner join YANTRA_LKUP_COLOR_MAST on YANTRA_PURCHASE_INVOICE_DET .PI_ColorId =YANTRA_LKUP_COLOR_MAST.COLOUR_ID inner join YANTRA_LKUP_UOM on YANTRA_ITEM_MAST .UOM_ID =YANTRA_LKUP_UOM .UOM_ID inner join YANTRA_FIXED_PO_MAST on YANTRA_PURCHASE_INVOICE_MAST.FPO_ID=YANTRA_FIXED_PO_MAST.FPO_ID inner join YANTRA_FIXED_PO_DET on YANTRA_PURCHASE_INVOICE_DET .PI_PONo =YANTRA_FIXED_PO_DET.FPO_DET_ID inner join YANTRA_INDENT_DET on YANTRA_FIXED_PO_DET .FPO_DET_IND_DET_ID =YANTRA_INDENT_DET .IND_DET_ID inner join YANTRA_SO_MAST   on YANTRA_INDENT_DET.IND_DET_SOID =YANTRA_SO_MAST .SO_ID left outer join YANTRA_CUSTOMER_MAST on YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID where YANTRA_PURCHASE_INVOICE_DET .PI_ID =" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Color");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GST");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("HSNCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("FPO_DET_ID");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("FPO_ID");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("CustId");
                SuppliersFixedPOItems.Columns.Add(col);
                
                
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Rate"] = dbManager.DataReader["PI_DET_RATE"].ToString();
                    //dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    //dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Specifications"] = dbManager.DataReader["FPO_DET_SPECIFICATION"].ToString();
                    dr["Remarks"] = dbManager.DataReader["PI_DET_Customer"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["PI_DET_AMOUNT"].ToString();
                    dr["Disc"] = dbManager.DataReader["PI_DET_DISC"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Customer"] = dbManager.DataReader["PI_DET_Customer"].ToString();
                    dr["ColorId"] = dbManager.DataReader["PI_ColorId"].ToString();
                    //dr["BalanceQty"] = dbManager.DataReader["Balance_QTY"].ToString();
                    dr["GST"] = dbManager.DataReader["GST TAX"].ToString();
                    dr["HSNCode"] = dbManager.DataReader["HSN_Code"].ToString();
                    dr["FPO_DET_ID"] = dbManager.DataReader["PI_DET_ID"].ToString();
                    dr["FPO_ID"] = dbManager.DataReader["PI_ID"].ToString();
                    dr["CustId"] = dbManager.DataReader["CUST_ID"].ToString();

                    SuppliersFixedPOItems.Rows.Add(dr);

                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
                // dbManager.Close();
            }
            public void SuppliersPODetails_Select(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_SUP_QUOT_DET,[YANTRA_ITEM_MAST],YANTRA_LKUP_COLOR_MAST,YANTRA_LKUP_ITEM_TYPE,YANTRA_CUSTOMER_MAST,YANTRA_INDENT_MAST,YANTRA_LKUP_UOM,YANTRA_INDENT_DET,YANTRA_LKUP_CURRENCY_TYPE where " +
             "   YANTRA_SUP_QUOT_DET.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE and YANTRA_INDENT_MAST.IND_ID=YANTRA_SUP_QUOT_DET.SUP_QUOT_IND_ID and " +
  "[YANTRA_ITEM_MAST].UOM_ID= YANTRA_LKUP_UOM.UOM_ID and YANTRA_LKUP_CURRENCY_TYPE.CURRENCY_ID=YANTRA_SUP_QUOT_DET.SUP_QUOT_DET_CURRENCY and " +
  "YANTRA_INDENT_DET.IND_DET_ID=YANTRA_SUP_QUOT_DET.SUP_QUOT_IND_DET_ID and YANTRA_CUSTOMER_MAST.CUST_ID=YANTRA_INDENT_MAST.CusId and " +
 " YANTRA_LKUP_COLOR_MAST.COLOUR_ID=YANTRA_SUP_QUOT_DET.SUP_QUOT_DET_COLOR_ID and " +
 " YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID=YANTRA_ITEM_MAST.IT_TYPE_ID and YANTRA_SUP_QUOT_DET.SUP_QUOT_ID=" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Desc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Color");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("MRP");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Discount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Amount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("IndId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("IndDetId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GST");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Fright");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Insurance");
                SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("ItemTypeId");
                //SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("SpPrice");
                //SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("Disc");
                //SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UnitPrice");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Total");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GSTAmount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("HSN_Code");
                SuppliersFixedPOItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    //dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    // dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SUP_QUOT_DET_QTY"].ToString();
                    // dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    //dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Desc"] = dbManager.DataReader["ITEM_SPEC"].ToString();
                    dr["IndId"] = dbManager.DataReader["SUP_QUOT_IND_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOUR_ID"].ToString();
                    dr["IndDetId"] = dbManager.DataReader["SUP_QUOT_IND_DET_ID"].ToString();
                    dr["MRP"] = dbManager.DataReader["SUP_QUOT_DET_RATE"].ToString();
                    //dr["CurrencyType"] = dbManager.DataReader["SUP_QUOT_DET_CURRENCY"].ToString();
                    dr["Customer"] = dbManager.DataReader["CUST_NAME"].ToString();
                    dr["Currency"] = dbManager.DataReader["CURRENCY_NAME"].ToString();
                    dr["GST"] = dbManager.DataReader["GST Tax"].ToString();
                    dr["HSN_Code"] = dbManager.DataReader["HSN_Code"].ToString();
                    dr["Fright"] = dbManager.DataReader["FPO_Fright"].ToString();
                    dr["Insurance"] = dbManager.DataReader["FPO_insurance"].ToString();
                    //dr["GSTAmount"] = dbManager.DataReader["FPO_DET_GST"].ToString();
                    // dr["Disc"] = dbManager.DataReader["FPO_DET_DISC"].ToString();
                    SuppliersFixedPOItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
                // dbManager.Close();
            }

            public void SuppliersPODetails_Select2(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select a.ITEM_CODE,a.FPO_DET_RATE,a.FPO_DET_QTY,FPO_DET_TAX,a.FPO_DET_GST,a.FPO_DET_SPECIFICATION,a.FPO_DET_REMARKS,a.FPO_DET_TAX,a.FPO_DET_DISC,a.FPO_FRIGHT,a.FPO_Insurance," +
                                                "a.FPO_DET_Currency,a.FPO_DET_SPPRICE,a.FPO_DET_IND_ID,a.FPO_DET_IND_DET_ID,b.ITEM_MODEL_NO,c.COLOUR_NAME,e.UOM_SHORT_DESC,f.CUST_NAME,b.HSN_Code from dbo.YANTRA_FIXED_PO_DET a " +
                                                "inner join dbo.YANTRA_ITEM_MAST b on a.ITEM_CODE=b.ITEM_CODE inner join dbo.YANTRA_LKUP_COLOR_MAST c " +
                                                "on a.FPO_DET_REMARKS=c.COLOUR_ID inner join dbo.YANTRA_FIXED_PO_MAST d on a.FPO_ID=d.FPO_ID inner join YANTRA_INDENT_MAST g on g.IND_ID=a.FPO_DET_IND_ID inner join YANTRA_CUSTOMER_MAST f on f.CUST_ID=g.CusId" +
                                                " inner join dbo.YANTRA_LKUP_UOM e on b.UOM_ID=e.UOM_ID where d.FPO_ID=" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Desc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Color");
                SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("UOM");
                //SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GST");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("MRP");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Discount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Amount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GSTAmount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("IndId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("IndDetId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Fright");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Insurance");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Total");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UnitPrice");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("HSN_Code");
                SuppliersFixedPOItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    //dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    //dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    // dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                    // dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    //dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Desc"] = dbManager.DataReader["FPO_DET_SPECIFICATION"].ToString();
                    dr["IndId"] = dbManager.DataReader["FPO_DET_IND_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["FPO_DET_REMARKS"].ToString();
                    dr["IndDetId"] = dbManager.DataReader["FPO_DET_IND_DET_ID"].ToString();
                    dr["Customer"] = dbManager.DataReader["CUST_NAME"].ToString();
                    dr["MRP"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    dr["Discount"] = dbManager.DataReader["FPO_DET_DISC"].ToString();
                    dr["Amount"] = dbManager.DataReader["FPO_DET_SPPRICE"].ToString();
                    dr["GSTAmount"] = dbManager.DataReader["FPO_DET_GST"].ToString();
                    dr["Currency"] = dbManager.DataReader["FPO_DET_Currency"].ToString();
                    dr["GST"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["HSN_code"] = dbManager.DataReader["HSN_Code"].ToString();
                    dr["Fright"] = dbManager.DataReader["FPO_Fright"].ToString();
                    dr["Insurance"] = dbManager.DataReader["FPO_Insurance"].ToString();
                    SuppliersFixedPOItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
                // dbManager.Close();
            }

            public void SupplierIndentPO_Select(GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from IND_DET_ITEMS, [YANTRA_ITEM_MAST],YANTRA_LKUP_COLOR_MAST,YANTRA_LKUP_ITEM_TYPE,YANTRA_INDENT_DET,YANTRA_INDENT_MAST,YANTRA_CUSTOMER_MAST, " +
  "YANTRA_LKUP_UOM where IND_DET_ITEMS.Ind_Det_Id=YANTRA_INDENT_DET.IND_DET_ID and YANTRA_INDENT_DET.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE and" +
  "[YANTRA_ITEM_MAST].UOM_ID= YANTRA_LKUP_UOM.UOM_ID and YANTRA_INDENT_MAST.IND_ID=IND_DET_ITEMS.Ind_Id and YANTRA_INDENT_MAST.CusId=YANTRA_CUSTOMER_MAST.CUST_ID and " +
 " YANTRA_LKUP_COLOR_MAST.COLOUR_ID=YANTRA_INDENT_DET.COLOR_ID and" +
 " YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID=YANTRA_ITEM_MAST.IT_TYPE_ID order by IND_DET_ITEMS.IND_DET_ID asc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Desc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Color");
                SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("UOM");
                //SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("IndId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("IndDetId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Currency");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("MRP");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Discount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Amount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("ItemTypeId");
                //SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("SpPrice");
                //SuppliersFixedPOItems.Columns.Add(col);
                //col = new DataColumn("Disc");
                //SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GST");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("GSTAmount");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Fright");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Insurance");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("HSN_Code");
                SuppliersFixedPOItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    //dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    //dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    // dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
                    // dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    //dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Desc"] = dbManager.DataReader["IND_DET_SPECS"].ToString();
                    dr["IndId"] = dbManager.DataReader["IND_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOUR_ID"].ToString();
                    dr["IndDetId"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["Customer"] = dbManager.DataReader["CUST_NAME"].ToString();
                    dr["GST"] = dbManager.DataReader["GST Tax"].ToString();
                    dr["HSN_Code"] = dbManager.DataReader["HSN_Code"].ToString();
                    // dr["Disc"] = dbManager.DataReader["FPO_DET_DISC"].ToString();
                    SuppliersFixedPOItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
                // dbManager.Close();
            }
        }

        ////Methods for Indent
        //public class Indent
        //{
        //    public string INDId, INDNo, INDDate, DeptId, FollowUp, INDPreparedBy, INDApprovedBy, CP_ID, INDSoId, Status,INDENTFOR;
        //    public string INDDetId, INDDetNo, INDItemCode, INDDetQty, INDDetBrand, INDDetSuggParty, INDDetReqFor, INDDetReqByDate, INDDetSpecs, INDDetPriority, IndSoid, IndColor,INDDetStatus;
        //    public string Godown;
        //    public Indent()
        //    {
        //    }

        //    public static string Indent_AutoGenCode()
        //    {
        //        string _codePrefix = CurrentFinancialYear() + " ";
        //        //string _codePrefix = "IND/";
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,LEFT(IND_NO,5),''))),0)+1  FROM [YANTRA_INDENT_MAST]").ToString());
        //        //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,'" + _codePrefix + "',''))),0)+1 FROM [YANTRA_INDENT_MAST]").ToString());
        //        return _codePrefix + _returnIntValue;

        //    }

        //    public string Indent_Save()
        //    {
        //        this.INDNo = Indent_AutoGenCode();
        //        this.INDId = AutoGenMaxId("[YANTRA_INDENT_MAST]", "IND_ID");
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_INDENT_MAST] VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},{8},'{9}','{10}')", this.INDId, this.INDNo, this.INDDate, this.DeptId, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.CP_ID, this.INDSoId, this.Status,this.INDENTFOR);
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

        //    public string Indent_Update()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET  IND_DATE='{0}',FOLLOW_UP={1},IND_PREPARED_BY={2},IND_APPROVED_BY={3},DEPT_ID={4},CP_ID = {5},IND_SO_ID ={6},INDENT_FOR = '{7}' WHERE IND_ID='{8}'", this.INDDate, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.DeptId, this.CP_ID, this.INDSoId,this.INDENTFOR, this.INDId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        if (_returnIntValue < 0 || _returnIntValue == 0)
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        else if (_returnIntValue > 0)
        //        {
        //            _returnStringMessage = "Data Updated Successfully";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string Indent_Delete(string IndentId)
        //    {
        //        if (DeleteRecord("[YANTRA_INDENT_DET]", "IND_ID", IndentId) == true)
        //        {
        //            if (DeleteRecord("[YANTRA_INDENT_MAST]", "IND_ID", IndentId) == true)
        //            {
        //                _returnStringMessage = "Data Deleted Successfully";
        //            }
        //            else
        //            {
        //                _returnStringMessage = "Some Data Missing.";
        //            }
        //        }
        //        else
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string IndentDetails_Save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_INDENT_DET] SELECT ISNULL(MAX(IND_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},'{11}',{12} FROM [YANTRA_INDENT_DET]", this.INDId, this.INDItemCode, this.INDDetQty, this.INDDetBrand, this.INDDetSuggParty, this.INDDetReqFor, this.INDDetReqByDate, this.INDDetSpecs, this.INDDetPriority, this.INDSoId, this.IndColor,this.INDDetStatus,this.Godown);
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

        //    public int IndentDetails_Delete(string IndentId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("DELETE FROM [YANTRA_INDENT_DET] WHERE IND_ID={0}", IndentId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        return _returnIntValue;
        //    }

        //    public string IndentApprove_Update()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();

        //        _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET IND_APPROVED_BY={0} WHERE IND_ID='{1}'", this.INDApprovedBy, this.INDId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //        _returnStringMessage = string.Empty;
        //        if (_returnIntValue < 0 || _returnIntValue == 0)
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        else if (_returnIntValue > 0)
        //        {
        //            _returnStringMessage = "Status Updated Successfully";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public int Indent_Select(string IndentId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_MAST],[YANTRA_DEPT_MAST],YANTRA_INDENT_DET WHERE YANTRA_INDENT_DET.IND_ID  = [YANTRA_INDENT_MAST].IND_ID AND  [YANTRA_INDENT_MAST].DEPT_ID=[YANTRA_DEPT_MAST].DEPT_ID AND [YANTRA_INDENT_MAST].IND_ID='" + IndentId + "' ORDER BY [YANTRA_INDENT_MAST].IND_ID DESC ");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (dbManager.DataReader.Read())
        //        {
        //            this.INDId = dbManager.DataReader["IND_ID"].ToString();
        //            this.INDNo = dbManager.DataReader["IND_NO"].ToString();
        //            this.INDDate = Convert.ToDateTime(dbManager.DataReader["IND_DATE"].ToString()).ToString("dd/MM/yyyy");
        //            this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();
        //            this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
        //            this.INDPreparedBy = dbManager.DataReader["IND_PREPARED_BY"].ToString();
        //            this.INDApprovedBy = dbManager.DataReader["IND_APPROVED_BY"].ToString();
        //            this.INDENTFOR = dbManager.DataReader["INDENT_FOR"].ToString();
        //            this.INDSoId = dbManager.DataReader["IND_SO_ID"].ToString();
        //            this.INDDetReqFor = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();

        //            _returnIntValue = 1;
        //        }
        //        else
        //        {
        //            _returnIntValue = 0;
        //        }
        //        return _returnIntValue;
        //    }

        //    public void IndentDetails_Select(string IndentId, GridView gv)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId);

        //        dbManager.ExecuteReader(CommandType.Text, _commandText);

        //        DataTable IndentProducts = new DataTable();
        //        DataColumn col = new DataColumn();

        //        col = new DataColumn("ItemCode");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ModelNo");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemName");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemGroup");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("UOM");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Quantity");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Priority");
        //        IndentProducts.Columns.Add(col);
        //        //col = new DataColumn("BalQty");
        //        //IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Brand");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("SuggestedParty");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ReqFor");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ReqDate");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Specification");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Color");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ColorId");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Godownid");
        //        IndentProducts.Columns.Add(col);

        //        while (dbManager.DataReader.Read())
        //        {
        //            DataRow dr = IndentProducts.NewRow();
        //            dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
        //            dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
        //            dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
        //            //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
        //            dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
        //            dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
        //            dr["Priority"] = dbManager.DataReader["IND_DET_PRIORITY"].ToString();
        //            //    dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["SuggestedParty"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
        //            dr["ReqFor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
        //            dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");

        //            // dr["ReqDate"] = dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString();
        //            dr["Specification"] = dbManager.DataReader["IND_DET_SPECS"].ToString();

        //            dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
        //            dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
        //            dr["Godownid"] = dbManager.DataReader["IND_GODOWN_ID"].ToString();

        //            IndentProducts.Rows.Add(dr);
        //        }

        //        gv.DataSource = IndentProducts;
        //        gv.DataBind();
        //    }

        //    public void IndentDetailsBrand_Select(string IndentId, GridView gv)
        //    {
        //        string strdet = "New";
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM YANTRA_INDENT_DET,[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_INDENT_MAST,YANTRA_LKUP_COLOR_MAST  WHERE   YANTRA_INDENT_DET.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND  [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and     YANTRA_INDENT_DET.IND_ID = YANTRA_INDENT_MAST.IND_ID AND YANTRA_INDENT_DET.IND_DET_STATUS = '" + strdet + "'AND  YANTRA_INDENT_DET.IND_DET_BRAND ='" + IndentId + "'");
        //       // _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId);

        //        dbManager.ExecuteReader(CommandType.Text, _commandText);

        //        DataTable IndentProducts = new DataTable();
        //        DataColumn col = new DataColumn();

        //        col = new DataColumn("ItemCode");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemName");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemType");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemGroup");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("UOM");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Quantity");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Priority");
        //        IndentProducts.Columns.Add(col);
        //        //col = new DataColumn("BalQty");
        //        //IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Brand");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("SuggestedParty");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ReqFor");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ReqDate");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Specification");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Color");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ColorId");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Indetid");
        //        IndentProducts.Columns.Add(col);

        //        while (dbManager.DataReader.Read())
        //        {
        //            DataRow dr = IndentProducts.NewRow();
        //            dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
        //            dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
        //            dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
        //            //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
        //            dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
        //            dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
        //            dr["Priority"] = dbManager.DataReader["IND_DET_PRIORITY"].ToString();
        //            //    dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["SuggestedParty"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
        //            dr["ReqFor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
        //            dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");

        //            // dr["ReqDate"] = dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString();
        //            dr["Specification"] = dbManager.DataReader["IND_DET_SPECS"].ToString();

        //            dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
        //            dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
        //            dr["Indetid"] = dbManager.DataReader["IND_DET_ID"].ToString();

        //            IndentProducts.Rows.Add(dr);
        //        }

        //        gv.DataSource = IndentProducts;
        //        gv.DataBind();
        //    }

        //    public static void Indent_Select(Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT IND_NO,IND_ID FROM [YANTRA_INDENT_MAST] ORDER BY IND_ID");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "IND_NO", "IND_ID");
        //        }
        //    }

        //    public static void IndentItemTypes_Select(string IndentId, Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_INDENT_DET].IND_ID=" + IndentId + "");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
        //        }
        //    }

        //    public static void IndentItemNames_Select(string IndentId, string ItemTypeId, Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
        //        }
        //    }

        //}

        ////Methods for Indent
        //public class Indent
        //{
        //    public string INDId, INDNo, INDDate, DeptId, FollowUp, INDPreparedBy, INDApprovedBy,CP_ID,INDSoId,Status;
        //    public string INDDetId, INDDetNo, INDItemCode, INDDetQty, INDDetBrand, INDDetSuggParty, INDDetReqFor, INDDetReqByDate, INDDetSpecs, INDDetPriority,IndSoid;

        //    public Indent()
        //    {
        //    }

        //    public static string Indent_AutoGenCode()
        //    {
        //        string _codePrefix = CurrentFinancialYear() + " ";
        //        //string _codePrefix = "IND/";
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,LEFT(IND_NO,5),''))),0)+1  FROM [YANTRA_INDENT_MAST]").ToString());
        //        //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,'" + _codePrefix + "',''))),0)+1 FROM [YANTRA_INDENT_MAST]").ToString());
        //        return _codePrefix + _returnIntValue;

        //    }

        //    public string Indent_Save()
        //    {
        //        this.INDNo = Indent_AutoGenCode();
        //        this.INDId = AutoGenMaxId("[YANTRA_INDENT_MAST]", "IND_ID");
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_INDENT_MAST] VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},{8},'{9}')", this.INDId, this.INDNo, this.INDDate, this.DeptId, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy,this.CP_ID,this.INDSoId,this.Status);
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

        //    public string Indent_Update()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET  IND_DATE='{0}',FOLLOW_UP={1},IND_PREPARED_BY={2},IND_APPROVED_BY={3},DEPT_ID={4} WHERE IND_ID='{5}'", this.INDDate, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.DeptId, this.INDId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        if (_returnIntValue < 0 || _returnIntValue == 0)
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        else if (_returnIntValue > 0)
        //        {
        //            _returnStringMessage = "Data Updated Successfully";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string Indent_Delete(string IndentId)
        //    {
        //        if (DeleteRecord("[YANTRA_INDENT_DET]", "IND_ID", IndentId) == true)
        //        {
        //            if (DeleteRecord("[YANTRA_INDENT_MAST]", "IND_ID", IndentId) == true)
        //            {
        //                _returnStringMessage = "Data Deleted Successfully";
        //            }
        //            else
        //            {
        //                _returnStringMessage = "Some Data Missing.";
        //            }
        //        }
        //        else
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string IndentDetails_Save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_INDENT_DET] SELECT ISNULL(MAX(IND_DET_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9} FROM [YANTRA_INDENT_DET]", this.INDId, this.INDItemCode, this.INDDetQty, this.INDDetBrand, this.INDDetSuggParty, this.INDDetReqFor, this.INDDetReqByDate, this.INDDetSpecs, this.INDDetPriority,this.INDSoId);
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

        //    public int IndentDetails_Delete(string IndentId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("DELETE FROM [YANTRA_INDENT_DET] WHERE IND_ID={0}", IndentId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        return _returnIntValue;
        //    }

        //    public string IndentApprove_Update()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();

        //        _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET IND_APPROVED_BY={0} WHERE IND_ID='{1}'", this.INDApprovedBy, this.INDId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //        _returnStringMessage = string.Empty;
        //        if (_returnIntValue < 0 || _returnIntValue == 0)
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        else if (_returnIntValue > 0)
        //        {
        //            _returnStringMessage = "Status Updated Successfully";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public int Indent_Select(string IndentId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_MAST],[YANTRA_DEPT_MAST] WHERE [YANTRA_INDENT_MAST].DEPT_ID=[YANTRA_DEPT_MAST].DEPT_ID AND [YANTRA_INDENT_MAST].IND_ID='" + IndentId + "' ORDER BY [YANTRA_INDENT_MAST].IND_ID DESC ");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (dbManager.DataReader.Read())
        //        {
        //            this.INDId = dbManager.DataReader["IND_ID"].ToString();
        //            this.INDNo = dbManager.DataReader["IND_NO"].ToString();
        //            this.INDDate = Convert.ToDateTime(dbManager.DataReader["IND_DATE"].ToString()).ToString("dd/MM/yyyy");
        //            this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();
        //            this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
        //            this.INDPreparedBy = dbManager.DataReader["IND_PREPARED_BY"].ToString();
        //            this.INDApprovedBy = dbManager.DataReader["IND_APPROVED_BY"].ToString();

        //            _returnIntValue = 1;
        //        }
        //        else
        //        {
        //            _returnIntValue = 0;
        //        }
        //        return _returnIntValue;
        //    }

        //    public void IndentDetails_Select(string IndentId, GridView gv)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM] WHERE [YANTRA_INDENT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId);

        //        dbManager.ExecuteReader(CommandType.Text, _commandText);

        //        DataTable IndentProducts = new DataTable();
        //        DataColumn col = new DataColumn();

        //        col = new DataColumn("ItemCode");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemName");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemType");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ItemGroup");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("UOM");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Quantity");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Priority");
        //        IndentProducts.Columns.Add(col);
        //        //col = new DataColumn("BalQty");
        //        //IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Brand");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("SuggestedParty");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ReqFor");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("ReqDate");
        //        IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Specification");
        //        IndentProducts.Columns.Add(col);

        //        while (dbManager.DataReader.Read())
        //        {
        //            DataRow dr = IndentProducts.NewRow();
        //            dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
        //            dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
        //            dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
        //            //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
        //            dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
        //            dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
        //            dr["Priority"] = dbManager.DataReader["IND_DET_PRIORITY"].ToString();
        //            //    dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["SuggestedParty"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
        //            dr["ReqFor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
        //            dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");

        //            // dr["ReqDate"] = dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString();
        //            dr["Specification"] = dbManager.DataReader["IND_DET_SPECS"].ToString();

        //            IndentProducts.Rows.Add(dr);
        //        }

        //        gv.DataSource = IndentProducts;
        //        gv.DataBind();
        //    }

        //    public static void Indent_Select(Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_MAST] ORDER BY IND_ID");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "IND_NO", "IND_ID");
        //        }
        //    }

        //    public static void IndentItemTypes_Select(string IndentId, Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_INDENT_DET].IND_ID=" + IndentId + "");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
        //        }
        //    }

        //    public static void IndentItemNames_Select(string IndentId, string ItemTypeId, Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
        //        }
        //    }

        //}

        ////Methods For IndentApproval Form
        //public class IndentApproval
        //{
        //    public String INDAPPRLId, INDAPPRLNo, INDAPPRLDate, IndId, IndNo, DeptId, FollowUp, INDAPPRLPreparedBy, INDAPPRLApprovedBy, INDAPPRLFlag,CpId;
        //    public String INDAPPRLDetId, INDAPPRLItemCode, INDAPPRLDetQty, INDAPPRLDetBrand, INDAPPRLDetSuggParty, INDAPPRLDetReqFor, INDAPPRLDetReqByDate, INDAPPRLDetSpecs, INDAPPRLDetPriority,INDAPPRLDetStatus;

        //    public IndentApproval()
        //    {
        //    }

        //    public static string IndentApproval_AutoGenCode()
        //    {
        //        string _codePrefix = CurrentFinancialYear() + " ";
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_APPRL_NO,LEFT(IND_APPRL_NO,5),''))),0)+1 FROM [YANTRA_INDENT_APPROVAL_MAST]").ToString());

        //        return _codePrefix + _returnIntValue;
        //    }

        //    public string IndentApproval_Save()
        //    {
        //        this.INDAPPRLNo = IndentApproval_AutoGenCode();
        //        this.INDAPPRLId = AutoGenMaxId("[YANTRA_INDENT_APPROVAL_MAST]", "IND_APPRL_ID");
        //        //this.INDAPPRLId = this.INDAPPRLNo.Replace("INDAPPRL/", "");
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_INDENT_APPROVAL_MAST] VALUES({0},'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}',{9})", this.INDAPPRLId, this.INDAPPRLNo, this.INDAPPRLDate, this.IndId, this.DeptId, this.FollowUp, this.INDAPPRLPreparedBy, this.INDAPPRLApprovedBy, this.INDAPPRLFlag,this.CpId);
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

        //    public string IndentApproval_Update()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("UPDATE [YANTRA_INDENT_APPROVAL_MAST] SET IND_APPRL_DATE='{0}',DEPT_ID={1},FOLLOW_UP={2},IND_APPRL_PREPARED_BY={3},IND_APPRL_APPROVED_BY={4},IND_APPRL_FLAG='{5}',CP_ID={7} WHERE IND_APPRL_ID={6}", this.INDAPPRLDate, this.DeptId, this.FollowUp, this.INDAPPRLPreparedBy, this.INDAPPRLApprovedBy, this.INDAPPRLFlag, this.INDAPPRLId,this.CpId );
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        if (_returnIntValue < 0 || _returnIntValue == 0)
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        else if (_returnIntValue > 0)
        //        {
        //            _returnStringMessage = "Data Updated Successfully";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string IndentApproval_Delete(string IndentApprovalId)
        //    {
        //        if (DeleteRecord("[YANTRA_INDENT_APPROVAL_DET]", "IND_APPRL_ID", IndentApprovalId) == true)
        //        {
        //            if (DeleteRecord("[YANTRA_INDENT_APPROVAL_MAST]", "IND_APPRL_ID", IndentApprovalId) == true)
        //            {
        //                _returnStringMessage = "Data Deleted Successfully";
        //            }
        //            else
        //            {
        //                _returnStringMessage = "Some Data Missing.";
        //            }
        //        }
        //        else
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string IndentApprovalDetails_Save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_INDENT_APPROVAL_DET] SELECT ISNULL(MAX(IND_APPRL_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}' FROM [YANTRA_INDENT_APPROVAL_DET]",
        //                                                                                      this.INDAPPRLId, this.INDAPPRLItemCode, this.INDAPPRLDetQty, this.INDAPPRLDetBrand, this.INDAPPRLDetSuggParty, this.INDAPPRLDetReqFor, this.INDAPPRLDetReqByDate, this.INDAPPRLDetSpecs, this.INDAPPRLDetPriority,this.INDAPPRLDetStatus );
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

        //    public int IndentApprovalDetails_Delete(string IndentApprovalId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("DELETE FROM [YANTRA_INDENT_APPROVAL_DET] WHERE IND_APPRL_ID={0}", IndentApprovalId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        return _returnIntValue;
        //    }

        //    public int IndentApproval_Select(string IndentApprovalId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_MAST],[YANTRA_INDENT_MAST],[YANTRA_DEPT_MAST] WHERE [YANTRA_INDENT_APPROVAL_MAST].DEPT_ID=[YANTRA_DEPT_MAST].DEPT_ID AND [YANTRA_INDENT_APPROVAL_MAST].IND_ID=[YANTRA_INDENT_MAST].IND_ID AND [YANTRA_INDENT_APPROVAL_MAST].IND_APPRL_ID='" + IndentApprovalId + "' ORDER BY [YANTRA_INDENT_APPROVAL_MAST].IND_APPRL_ID DESC ");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (dbManager.DataReader.Read())
        //        {
        //            this.INDAPPRLNo = dbManager.DataReader["IND_APPRL_NO"].ToString();
        //            this.INDAPPRLDate = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DATE"].ToString()).ToString("dd/MM/yyyy");
        //            this.IndId = dbManager.DataReader["IND_ID"].ToString();
        //            this.IndNo = dbManager.DataReader["IND_NO"].ToString();
        //            this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();

        //            this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
        //            this.INDAPPRLPreparedBy = dbManager.DataReader["IND_APPRL_PREPARED_BY"].ToString();
        //            this.INDAPPRLApprovedBy = dbManager.DataReader["IND_APPRL_APPROVED_BY"].ToString();
        //            this.INDAPPRLFlag = dbManager.DataReader["IND_APPRL_FLAG"].ToString();

        //            _returnIntValue = 1;
        //        }
        //        else
        //        {
        //            _returnIntValue = 0;
        //        }
        //        return _returnIntValue;
        //    }
        //    public void IndentApprovalDetails_Select(string IndentApprovalId, GridView gv)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM] WHERE [YANTRA_INDENT_APPROVAL_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
        //                                       "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID AND [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_ID=" + IndentApprovalId + "");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);

        //        DataTable IndentApprovalProducts = new DataTable();
        //        DataColumn col = new DataColumn();
        //        col = new DataColumn("ItemCode");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ItemName");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ItemType");
        //        IndentApprovalProducts.Columns.Add(col);
        //        //col = new DataColumn("ItemGroup");
        //        //IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("UOM");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Quantity");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Priority");
        //        IndentApprovalProducts.Columns.Add(col);
        //        //col = new DataColumn("BalQty");
        //        //IndentProducts.Columns.Add(col);
        //        col = new DataColumn("Brand");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("SuggestedParty");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ReqFor");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ReqDate");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Specification");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Detid");
        //        IndentApprovalProducts.Columns.Add(col);

        //        while (dbManager.DataReader.Read())
        //        {
        //            DataRow dr = IndentApprovalProducts.NewRow();
        //            dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
        //            dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
        //            dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
        //            //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
        //            dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
        //            dr["Quantity"] = dbManager.DataReader["IND_APPRL_DET_QTY"].ToString();
        //            dr["Priority"] = dbManager.DataReader["IND_APPRL_DET_PRIORITY"].ToString();
        //            //dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["Brand"] = dbManager.DataReader["IND_APPRL_DET_BRAND"].ToString();
        //            dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
        //            dr["ReqFor"] = dbManager.DataReader["IND_APPRL_DET_REQ_FOR"].ToString();
        //            dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
        //            dr["Specification"] = dbManager.DataReader["IND_APPRL_DET_SPECS"].ToString();
        //            dr["Detid"] = dbManager.DataReader["IND_APPRL_DET_ID"].ToString();
        //            IndentApprovalProducts.Rows.Add(dr);
        //        }

        //        gv.DataSource = IndentApprovalProducts;
        //        gv.DataBind();
        //    }
        //    public void IndentApprovalDetails_Select1(string IndentApprovalId, GridView gv)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //         string str="New";
        //         string strdet = "New";
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_INDENT_APPROVAL_MAST  WHERE [YANTRA_INDENT_APPROVAL_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
        //                                       "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and   [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_ID = YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID and YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_FLAG =  '" + str + "' and YANTRA_INDENT_APPROVAL_DET.IND_APPRL_DET_STATUS = '"+strdet+"' AND [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_DET_BRAND = '" + IndentApprovalId + "'");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);

        //        DataTable IndentApprovalProducts = new DataTable();
        //        DataColumn col = new DataColumn();
        //        col = new DataColumn("ItemCode");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ItemName");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ItemType");
        //        IndentApprovalProducts.Columns.Add(col);

        //        col = new DataColumn("UOM");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Quantity");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Priority");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Brand");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("SuggestedParty");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ReqFor");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("ReqDate");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Specification");
        //        IndentApprovalProducts.Columns.Add(col);
        //        col = new DataColumn("Detid");
        //        IndentApprovalProducts.Columns.Add(col);

        //        while (dbManager.DataReader.Read())
        //        {
        //            DataRow dr = IndentApprovalProducts.NewRow();
        //            dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
        //            dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
        //            dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
        //            //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
        //            dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
        //            dr["Quantity"] = dbManager.DataReader["IND_APPRL_DET_QTY"].ToString();
        //            dr["Priority"] = dbManager.DataReader["IND_APPRL_DET_PRIORITY"].ToString();
        //            // dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
        //            dr["Brand"] = dbManager.DataReader["IND_APPRL_DET_BRAND"].ToString();
        //            dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
        //            dr["ReqFor"] = dbManager.DataReader["IND_APPRL_DET_REQ_FOR"].ToString();
        //            dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
        //            dr["Specification"] = dbManager.DataReader["IND_APPRL_DET_SPECS"].ToString();
        //            dr["Detid"] = dbManager.DataReader["IND_APPRL_DET_ID"].ToString();

        //            IndentApprovalProducts.Rows.Add(dr);
        //        }

        //        gv.DataSource = IndentApprovalProducts;
        //        gv.DataBind();
        //    }

        //    public static void IndentApproval_Select(Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_MAST] ORDER BY IND_APPRL_ID");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "IND_APPRL_NO", "IND_APPRL_ID");
        //        }
        //    }

        //    public static void IndentApproval_Select1(Control ControlForBind)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        string str="New";
        //        _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_MAST] where YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_FLAG = '" + str +"' ORDER BY YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID" );
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "IND_APPRL_NO", "IND_APPRL_ID");
        //        }
        //    }
        //    public static string IndentStatus_Update(SCMStatus Status, string IndId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET  STATUS='{0}' WHERE IND_ID='{1}'", Status, IndId);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        if (_returnIntValue < 0 || _returnIntValue == 0)
        //        {
        //            _returnStringMessage = "Some Data Missing.";
        //        }
        //        else if (_returnIntValue > 0)
        //        {
        //            _returnStringMessage = "Status Updated Successfully";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public int Get_Ids_Select(string INDAPPRLId)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM YANTRA_INDENT_MAST,YANTRA_INDENT_APPROVAL_MAST where YANTRA_INDENT_APPROVAL_MAST.IND_ID = YANTRA_INDENT_MAST.IND_ID and YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID ='" + INDAPPRLId + "'");

        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (dbManager.DataReader.Read())
        //        {
        //            this.IndId = dbManager.DataReader["IND_ID"].ToString();

        //            _returnIntValue = 1;
        //        }
        //        else
        //        {
        //            _returnIntValue = 0;
        //        }
        //        dbManager.DataReader.Close();
        //        return _returnIntValue;

        //    }

        //}

        //Methods For IndentApproval Form

        //Methods for Indent
        public class Indent
        {
            public string INDId, INDNo, INDDate, DeptId, FollowUp, INDPreparedBy, INDApprovedBy, CP_ID, INDSoId, Status, INDENTFOR, Custid;
            public string INDDetId, INDDetNo, INDItemCode, INDDetQty, INDDetBrand, INDDetSuggParty, INDDetReqFor, INDDetReqByDate, INDDetSpecs, INDDetPriority, IndSoid, IndSoDetId,IndColor, INDDetStatus, INDDetRemQty,INDDETORDQTY;

            public Indent()
            {
            }

            public static string Indent_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                //string _codePrefix = "IND/";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,LEFT(IND_NO,5),''))),0)+1  FROM [YANTRA_INDENT_MAST]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,'" + _codePrefix + "',''))),0)+1 FROM [YANTRA_INDENT_MAST]").ToString());
               // dbManager.Close();
                return _codePrefix + _returnIntValue;
            }
            public static string Indent_AutoGenCode1()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                //string _codePrefix = "IND/";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,LEFT(IND_NO,5),''))),0)+1  FROM [YANTRA_INDENT_REQUEST_MAST]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,'" + _codePrefix + "',''))),0)+1 FROM [YANTRA_INDENT_MAST]").ToString());
                // dbManager.Close();
                return _codePrefix + _returnIntValue;
            }
            public string Indent_Save()
            {
                this.INDNo = Indent_AutoGenCode();
                this.INDId = AutoGenMaxId("[YANTRA_INDENT_MAST]", "IND_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_MAST] VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},{8},'{9}','{10}',{11})", this.INDId, this.INDNo, this.INDDate, this.DeptId, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.CP_ID, this.INDSoId, this.Status, this.INDENTFOR, this.Custid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
            public string Indent_Save1()
            {
                this.INDNo = Indent_AutoGenCode1();
                this.INDId = AutoGenMaxId("[YANTRA_INDENT_REQUEST_MAST]", "IND_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_REQUEST_MAST] VALUES({0},'{1}','{2}','{3}',{4},{5},{6},{7},{8},'{9}','{10}',{11})", this.INDId, this.INDNo, this.INDDate, this.DeptId, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.CP_ID, this.INDSoId, this.Status, this.INDENTFOR, this.Custid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Details", "83");

                }
                // dbManager.Close();
                return _returnStringMessage;
            }
            public string Indent_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET  IND_DATE='{0}',FOLLOW_UP={1},IND_PREPARED_BY={2},IND_APPROVED_BY={3},DEPT_ID={4},CP_ID = {5},IND_SO_ID ={6},INDENT_FOR = '{7}',CusId = {8} WHERE IND_ID={9}", this.INDDate, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.DeptId, this.CP_ID, this.INDSoId, this.INDENTFOR, this.Custid, this.INDId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Indent Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
            public string Indent_Update1()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_REQUEST_MAST] SET  IND_DATE='{0}',FOLLOW_UP={1},IND_PREPARED_BY={2},IND_APPROVED_BY={3},DEPT_ID={4},CP_ID = {5},IND_SO_ID ={6},INDENT_FOR = '{7}',CusId = {8} WHERE IND_ID={9}", this.INDDate, this.FollowUp, this.INDPreparedBy, this.INDApprovedBy, this.DeptId, this.CP_ID, this.INDSoId, this.INDENTFOR, this.Custid, this.INDId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Indent Details", "83");

                }
                // dbManager.Close();
                return _returnStringMessage;
            }
            public string Indent_Delete(string IndentId)
            {
                if (DeleteRecord("[YANTRA_INDENT_DET]", "IND_ID", IndentId) == true)
                {
                    if (DeleteRecord("[YANTRA_INDENT_MAST]", "IND_ID", IndentId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Indent Details", "83");

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

            public string Remarks;
            public string IndentDetails_Save1()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_REQUEST_DET] SELECT ISNULL(MAX(IND_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},'{11}',{12},{13},'{14}','{15}' from YANTRA_INDENT_REQUEST_DET", this.INDId, this.INDItemCode, this.INDDetQty, this.INDDetBrand, this.INDDetSuggParty, this.INDDetReqFor, this.INDDetReqByDate, this.INDDetSpecs, this.INDDetPriority, this.INDSoId, this.IndColor, this.INDDetStatus, this.INDDetRemQty, this.INDDETORDQTY, this.Remarks, this.IndSoDetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Details", "83");

                }
                // dbManager.Close();
                return _returnStringMessage;
            }

            public string IndentDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_DET] SELECT ISNULL(MAX(IND_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},'{11}',{12},{13},'{14}','{15}' from YANTRA_INDENT_DET", this.INDId, this.INDItemCode, this.INDDetQty, this.INDDetBrand, this.INDDetSuggParty, this.INDDetReqFor, this.INDDetReqByDate, this.INDDetSpecs, this.INDDetPriority, this.INDSoId, this.IndColor, this.INDDetStatus, this.INDDetRemQty, this.INDDETORDQTY,this.Remarks,this.IndSoDetId );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int IndentDetails_Delete1(string IndentId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_INDENT_REQUEST_DET] WHERE IND_ID={0}", IndentId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                // dbManager.Close();
                return _returnIntValue;
            }
            public int IndentDetails_Delete(string IndentId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_INDENT_DET] WHERE IND_ID={0}", IndentId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public string IndentApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET IND_APPROVED_BY={0},STATUS = '{1}' WHERE IND_ID={2}", this.INDApprovedBy, this.Status, this.INDId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Indent Approve Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int Indent_Select1(string IndentId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_REQUEST_MAST],[YANTRA_DEPT_MAST],YANTRA_INDENT_DET WHERE YANTRA_INDENT_DET.IND_ID  = [YANTRA_INDENT_REQUEST_MAST].IND_ID AND  [YANTRA_INDENT_REQUEST_MAST].DEPT_ID=[YANTRA_DEPT_MAST].DEPT_ID AND [YANTRA_INDENT_REQUEST_MAST].IND_ID='" + IndentId + "' ORDER BY [YANTRA_INDENT_REQUEST_MAST].IND_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.INDId = dbManager.DataReader["IND_ID"].ToString();
                    this.INDNo = dbManager.DataReader["IND_NO"].ToString();
                    this.INDDate = Convert.ToDateTime(dbManager.DataReader["IND_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();
                    this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
                    this.INDPreparedBy = dbManager.DataReader["IND_PREPARED_BY"].ToString();
                    this.INDApprovedBy = dbManager.DataReader["IND_APPROVED_BY"].ToString();
                    this.INDENTFOR = dbManager.DataReader["INDENT_FOR"].ToString();
                    this.INDSoId = dbManager.DataReader["IND_SO_ID"].ToString();
                    this.INDDetReqFor = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    this.Custid = dbManager.DataReader["CusId"].ToString();

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
            public int Indent_Select(string IndentId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_MAST],[YANTRA_DEPT_MAST],YANTRA_INDENT_DET WHERE YANTRA_INDENT_DET.IND_ID  = [YANTRA_INDENT_MAST].IND_ID AND  [YANTRA_INDENT_MAST].DEPT_ID=[YANTRA_DEPT_MAST].DEPT_ID AND [YANTRA_INDENT_MAST].IND_ID='" + IndentId + "' ORDER BY [YANTRA_INDENT_MAST].IND_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.INDId = dbManager.DataReader["IND_ID"].ToString();
                    this.INDNo = dbManager.DataReader["IND_NO"].ToString();
                    this.INDDate = Convert.ToDateTime(dbManager.DataReader["IND_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();
                    this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
                    this.INDPreparedBy = dbManager.DataReader["IND_PREPARED_BY"].ToString();
                    this.INDApprovedBy = dbManager.DataReader["IND_APPROVED_BY"].ToString();
                    this.INDENTFOR = dbManager.DataReader["INDENT_FOR"].ToString();
                    this.INDSoId = dbManager.DataReader["IND_SO_ID"].ToString();
                    this.INDDetReqFor = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    this.Custid = dbManager.DataReader["CusId"].ToString();

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
            public void IndentDetails_Select1(string IndentId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_REQUEST_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_REQUEST_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_REQUEST_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_INDENT_REQUEST_DET].IND_ID = '" + IndentId + "' order by [YANTRA_INDENT_REQUEST_DET].IND_DET_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable IndentProducts = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemGroup");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                IndentProducts.Columns.Add(col);
                //col = new DataColumn("BalQty");
                //IndentProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("SuggestedParty");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ReqFor");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ReqDate");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("IndentId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("IndentDetId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("So_Det_Id");
                IndentProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
                    dr["Priority"] = dbManager.DataReader["IND_DET_PRIORITY"].ToString();
                    //    dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["SuggestedParty"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
                    dr["ReqFor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");

                    // dr["ReqDate"] = dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString();
                    dr["Specification"] = dbManager.DataReader["IND_DET_SPECS"].ToString();

                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["IndentId"] = dbManager.DataReader["IND_ID"].ToString();
                    dr["IndentDetId"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["IND_REMARKS"].ToString();
                    dr["So_Det_Id"] = dbManager.DataReader["IND_DET_SO_ID"].ToString();
                    IndentProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentProducts;
                gv.DataBind();
                // dbManager.Close();
            }
            public void IndentDetails_Select(string IndentId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_INDENT_DET].IND_ID = '" + IndentId + "' order by [YANTRA_INDENT_DET].IND_DET_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable IndentProducts = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemGroup");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                IndentProducts.Columns.Add(col);
                //col = new DataColumn("BalQty");
                //IndentProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("SuggestedParty");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ReqFor");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ReqDate");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("IndentId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("IndentDetId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("So_Det_Id");
                IndentProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
                    dr["Priority"] = dbManager.DataReader["IND_DET_PRIORITY"].ToString();
                    //    dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["SuggestedParty"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
                    dr["ReqFor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");

                    // dr["ReqDate"] = dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString();
                    dr["Specification"] = dbManager.DataReader["IND_DET_SPECS"].ToString();

                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["IndentId"] = dbManager.DataReader["IND_ID"].ToString();
                    dr["IndentDetId"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["IND_REMARKS"].ToString();
                    dr["So_Det_Id"] = dbManager.DataReader["IND_DET_SO_ID"].ToString();
                    IndentProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public void IndentDetailsBrand_Select(string IndentId, GridView gv)
            {
                string strdet = "New";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_INDENT_DET,[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_INDENT_MAST,YANTRA_LKUP_COLOR_MAST  WHERE   YANTRA_INDENT_DET.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND  [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and     YANTRA_INDENT_DET.IND_ID = YANTRA_INDENT_MAST.IND_ID AND YANTRA_INDENT_DET.IND_DET_STATUS = '" + strdet + "'AND  YANTRA_INDENT_DET.IND_DET_BRAND ='" + IndentId + "'");
                // _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_INDENT_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable IndentProducts = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ItemGroup");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                IndentProducts.Columns.Add(col);
                //col = new DataColumn("BalQty");
                //IndentProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("SuggestedParty");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ReqFor");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ReqDate");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentProducts.Columns.Add(col);
                col = new DataColumn("Indetid");
                IndentProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_DET_QTY"].ToString();
                    dr["Priority"] = dbManager.DataReader["IND_DET_PRIORITY"].ToString();
                    //    dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["SuggestedParty"] = dbManager.DataReader["IND_DET_SUGG_PARTY"].ToString();
                    dr["ReqFor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");

                    // dr["ReqDate"] = dbManager.DataReader["IND_DET_REQ_BY_DATE"].ToString();
                    dr["Specification"] = dbManager.DataReader["IND_DET_SPECS"].ToString();

                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Indetid"] = dbManager.DataReader["IND_DET_ID"].ToString();

                    IndentProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public static void Indent_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT IND_NO,IND_ID FROM [YANTRA_INDENT_MAST] ORDER BY IND_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IND_NO", "IND_ID");
                }
               // dbManager.Close();
            }

            public static void IndentItemTypes_Select(string IndentId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_INDENT_DET].IND_ID=" + IndentId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
               // dbManager.Close();
            }

            public static void IndentItemNames_Select(string IndentId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
               // dbManager.Close();
            }
        }

        public class IndentApproval
        {
            public String INDAPPRLId, INDAPPRLNo, INDAPPRLDate, IndId, IndNo, DeptId, FollowUp, INDAPPRLPreparedBy, INDAPPRLApprovedBy, INDAPPRLFlag, CpId;
            public String INDAPPRLDetId, INDAPPRLItemCode, INDAPPRLDetQty, INDAPPRLDetBrand, INDAPPRLDetSuggParty, INDAPPRLDetReqFor, INDAPPRLDetReqByDate, INDAPPRLDetSpecs, INDAPPRLDetPriority, INDAPPRLDetStatus, IndColor, IND_DET_ID;
            public string Supplier_Det_Id, Supplier_Id;
            public IndentApproval()
            {
            }

            public static string IndentApproval_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_APPRL_NO,LEFT(IND_APPRL_NO,5),''))),0)+1 FROM [YANTRA_INDENT_APPROVAL_MAST]").ToString());
               // dbManager.Close();

                return _codePrefix + _returnIntValue;
            }

            public string IndentApproval_Save()
            {
                this.INDAPPRLNo = IndentApproval_AutoGenCode();
                this.INDAPPRLId = AutoGenMaxId("[YANTRA_INDENT_APPROVAL_MAST]", "IND_APPRL_ID");
                //this.INDAPPRLId = this.INDAPPRLNo.Replace("INDAPPRL/", "");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_APPROVAL_MAST] VALUES({0},'{1}','{2}','{3}','{4}',{5},{6},{7},'{8}',{9},{10})", this.INDAPPRLId, this.INDAPPRLNo, this.INDAPPRLDate, this.IndId, this.DeptId, this.FollowUp, this.INDAPPRLPreparedBy, this.INDAPPRLApprovedBy, this.INDAPPRLFlag, this.CpId,this.Supplier_Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Approval Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string IndentApproval_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_APPROVAL_MAST] SET IND_APPRL_DATE='{0}',DEPT_ID={1},FOLLOW_UP={2},IND_APPRL_PREPARED_BY={3},IND_APPRL_APPROVED_BY={4},IND_APPRL_FLAG='{5}',CP_ID={7},SUP_ID={8} WHERE IND_APPRL_ID={6}", this.INDAPPRLDate, this.DeptId, this.FollowUp, this.INDAPPRLPreparedBy, this.INDAPPRLApprovedBy, this.INDAPPRLFlag, this.INDAPPRLId, this.CpId,this.Supplier_Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Indent Approval Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }


            public int SuppliersEnquiryMaster_Select2(string SuppEnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format(" select a.IND_APPRL_ID,a.IND_APPRL_NO,a.IND_APPRL_DATE,a.DEPT_ID,a.IND_APPRL_PREPARED_BY ,a.FOLLOW_UP,a.SUP_ID" +
  " from YANTRA_INDENT_APPROVAL_MAST a where a.[IND_APPRL_ID]=" + SuppEnqId + " ");
                //_commandText = string.Format("SELECT * FROM [YANTRA_SUP_ENQ_MAST] ORDER BY SUP_ENQ_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.IndId = dbManager.DataReader["IND_APPRL_ID"].ToString();
                    this.INDAPPRLNo = dbManager.DataReader["IND_APPRL_NO"].ToString();
                    this.INDAPPRLDate = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DATE"].ToString()).ToString("dd/MM/yyyy");                       
                    this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();
                    this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
                    this.Supplier_Id = dbManager.DataReader["SUP_ID"].ToString();
                    //this.SuppEnqId = dbManager.DataReader["SUP_ENQ_ID"].ToString();
                    //this.SuppEnqNo = dbManager.DataReader["SUP_ENQ_NO"].ToString();
                    //this.SuppEnqDate = Convert.ToDateTime(dbManager.DataReader["SUP_ENQ_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.SuppEnqOrgBy = dbManager.DataReader["SUP_ENQ_ORIGINATED_BY"].ToString();
                    //this.SuppEnqStatus = dbManager.DataReader["SUP_ENQ_STATUS"].ToString();
                    //this.SuppEnqFollwUp = dbManager.DataReader["SUP_ENQ_FOLLOWUP_CRI"].ToString();
                    //this.SuppEnqDueDate = Convert.ToDateTime(dbManager.DataReader["SUP_ENQ_DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.SuppEnqDespId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.INDAPPRLPreparedBy = dbManager.DataReader["IND_APPRL_PREPARED_BY"].ToString();
                    //this.SuppEnqApprovedBy = dbManager.DataReader["SUP_ENQ_APPROVED_BY"].ToString();
                    ////this.SuppEnqbr= dbManager.DataReader["SUP_ENQ_APPROVED_BY"].ToString();
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

            public string IndentApproval_Delete(string IndentApprovalId)
            {
                if (DeleteRecord("[YANTRA_INDENT_APPROVAL_DET]", "IND_APPRL_ID", IndentApprovalId) == true)
                {
                    if (DeleteRecord("[YANTRA_INDENT_APPROVAL_MAST]", "IND_APPRL_ID", IndentApprovalId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Indent Approval Details", "83");

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
               // dbManager.Close();
                return _returnStringMessage;
            }

            public void IndentDetails_Select3(GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select a.ITEM_CODE,c.ITEM_NAME,g.IND_ID,a.IND_DET_QTY,a.IND_DET_BRAND,a.IND_DET_SUGG_PARTY,a.IND_DET_REQ_FOR,a.IND_DET_SPECS,"+
" b.COLOUR_NAME,c.ITEM_MODEL_NO,a.IND_DET_STATUS,a.COLOR_ID,d.IT_TYPE,e.UOM_SHORT_DESC,g.IND_DET_ID,f.IND_DATE,a.IND_DET_REQ_BY_DATE,a.IND_DET_REM_QTY,a.IND_DET_ORD_QTY from "+
" IND_DET_ITEMS as g inner join dbo.YANTRA_INDENT_DET as a on a.IND_DET_ID=g.Ind_Det_Id inner join dbo.YANTRA_LKUP_COLOR_MAST as b on a.COLOR_ID=b.COLOUR_ID inner join "+
" dbo.YANTRA_ITEM_MAST as c on a.ITEM_CODE=c.ITEM_CODE inner join dbo.YANTRA_LKUP_ITEM_TYPE as d on c.IT_TYPE_ID=d.IT_TYPE_ID inner join dbo.YANTRA_LKUP_UOM e on c.UOM_ID=e.UOM_ID inner join"+
" YANTRA_INDENT_MAST f on  a.IND_ID = f.IND_ID where a.IND_DET_STATUS='New'");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ITEM_CODE");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ITEM_MODEL_NO");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IT_TYPE");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_ID");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM_SHORT_DESC");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_QTY");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_BRAND");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_REQ_FOR");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("COLOUR_NAME");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("COLOR_ID");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_ID");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_REM_QTY");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_ORD_QTY");
                IndentApprovalProducts.Columns.Add(col);
             

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ITEM_CODE"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ITEM_MODEL_NO"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["IT_TYPE"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["IND_ID"] = dbManager.DataReader["IND_ID"].ToString();
                    dr["UOM_SHORT_DESC"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["IND_DET_QTY"] = dbManager.DataReader["IND_DET_QTY"].ToString();
                    dr["IND_DET_BRAND"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["IND_DET_REQ_FOR"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    dr["COLOUR_NAME"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["COLOR_ID"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["IND_DET_ID"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["IND_DET_REM_QTY"] = dbManager.DataReader["IND_DET_REM_QTY"].ToString();
                    dr["IND_DET_ORD_QTY"] = dbManager.DataReader["IND_DET_ORD_QTY"].ToString();

                    IndentApprovalProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public void IndentDetails_Select2(string IndentApprovalId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_APPROVAL_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                                 "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_INDENT_APPROVAL_DET.COLOR_ID  AND [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_ID=" + IndentApprovalId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ITEM_CODE");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ITEM_MODEL_NO");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IT_TYPE");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_ID");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM_SHORT_DESC");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_QTY");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_BRAND");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_REQ_FOR");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("COLOUR_NAME");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("COLOR_ID");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_ID");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_REM_QTY");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IND_DET_ORD_QTY");
                IndentApprovalProducts.Columns.Add(col);
             
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ITEM_CODE"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ITEM_MODEL_NO"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["IT_TYPE"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["IND_ID"] = dbManager.DataReader["IND_ID"].ToString();
                    dr["UOM_SHORT_DESC"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["IND_DET_QTY"] = dbManager.DataReader["IND_APPRL_DET_QTY"].ToString();
                    dr["IND_DET_BRAND"] = dbManager.DataReader["IND_APPRL_DET_BRAND"].ToString();
                    dr["IND_DET_REQ_FOR"] = dbManager.DataReader["IND_APPRL_DET_REQ_FOR"].ToString();
                    dr["COLOUR_NAME"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["COLOR_ID"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["IND_DET_ID"] = dbManager.DataReader["COLOR_ID"].ToString();
                    //dr["IND_DET_REM_QTY"] = dbManager.DataReader["IND_DET_REM_QTY"].ToString();
                    //dr["IND_DET_ORD_QTY"] = dbManager.DataReader["IND_DET_ORD_QTY"].ToString();


                    IndentApprovalProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }


            public string IndentApprovalDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_APPROVAL_DET] SELECT ISNULL(MAX(IND_APPRL_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},{12} FROM [YANTRA_INDENT_APPROVAL_DET]",
                                                                                              this.INDAPPRLId, this.INDAPPRLItemCode, this.INDAPPRLDetQty, this.INDAPPRLDetBrand, this.INDAPPRLDetSuggParty, this.INDAPPRLDetReqFor, this.INDAPPRLDetReqByDate, this.INDAPPRLDetSpecs, this.INDAPPRLDetPriority, this.INDAPPRLDetStatus, this.IndColor, this.IndId, this.IND_DET_ID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Approval Details", "83");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int IndentApprovalDetails_Delete(string IndentApprovalId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_INDENT_APPROVAL_DET] WHERE IND_APPRL_ID={0}", IndentApprovalId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public void IndentSuppliersPODetails_Select(string IndId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from dbo.YANTRA_INDENT_APPROVAL_SUPPLIER a inner join YANTRA_INDENT_APPROVAL_MAST b on a.Ind_Apprl_Id=b.IND_APPRL_ID inner join "+
                                                "dbo.YANTRA_SUPPLIER_MAST c on a.Supplier_Id=c.SUP_ID where b.IND_APPRL_ID=" + IndId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SupplierDetails = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("SuppId");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("Name");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("ContactPerson");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("PhoneNo");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("Email");
                SupplierDetails.Columns.Add(col);
               
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SupplierDetails.NewRow();
                    dr["SuppId"] = dbManager.DataReader["SUP_ID"].ToString();
                    dr["Name"] = dbManager.DataReader["SUP_NAME"].ToString();
                    dr["ContactPerson"] = dbManager.DataReader["SUP_CONTACT_PERSON"].ToString();
                    dr["PhoneNo"] = dbManager.DataReader["SUP_MOBILE"].ToString();
                    dr["Email"] = dbManager.DataReader["SUP_EMAIL"].ToString();
                    SupplierDetails.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SupplierDetails;
                gv.DataBind();
               // dbManager.Close();
            }

            public string IndentSupplierDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INDENT_APPROVAL_SUPPLIER] SELECT ISNULL(MAX(Supplier_Det_Id),0)+1,{0},{1} FROM [YANTRA_INDENT_APPROVAL_SUPPLIER]",
                                                                                              this.INDAPPRLId, this.Supplier_Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Indent Supplier Details", "84");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
            /// <summary>
            /// Update enquired Quantity
            /// </summary>
            /// <param name="qty"></param>
            /// <param name="IndId"></param>
            /// <returns></returns>
            public string IndentDetEnqQuantity_Update(string qty, string IndId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_DET] SET  IND_DET_REM_QTY='{0}' WHERE IND_DET_ID='{1}'", qty, IndId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Delete("Indent Enq Quantity Details", "84");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int IndentSupplierDetails_Delete(string IndentApprovalId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_INDENT_APPROVAL_SUPPLIER] WHERE IND_APPRL_ID={0}", IndentApprovalId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public string Indentqty,IndOrdqty;
            public int IndentDetQty_Select(string Detid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET] where [YANTRA_INDENT_DET].IND_DET_ID ='" + Detid + "'  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Indentqty = dbManager.DataReader["IND_DET_QTY"].ToString();
                    this.IndOrdqty = dbManager.DataReader["IND_DET_ORD_QTY"].ToString();
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


            public string IndentRecordsStatus_Update( string status, string IndDetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_DET] SET IND_DET_STATUS='{0}' WHERE IND_DET_ID='{1}'", status, IndDetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Indent Status Details", "84");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }


            public string IndentStatus_Update(double qty,string status, string IndId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_DET] SET  IND_DET_ORD_QTY='{0}',IND_DET_STATUS='{1}' WHERE IND_DET_ID='{2}'", qty,status, IndId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Indent Status Details", "84");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
            public int IndentApproval_Select(string IndentApprovalId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_MAST] where [YANTRA_INDENT_APPROVAL_MAST].IND_APPRL_ID='" + IndentApprovalId + "' ORDER BY [YANTRA_INDENT_APPROVAL_MAST].IND_APPRL_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.INDAPPRLNo = dbManager.DataReader["IND_APPRL_NO"].ToString();
                    this.INDAPPRLDate = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.IndId = dbManager.DataReader["IND_ID"].ToString();
                    //this.IndNo = dbManager.DataReader["IND_NO"].ToString();
                    //this.DeptId = dbManager.DataReader["DEPT_ID"].ToString();

                    this.FollowUp = dbManager.DataReader["FOLLOW_UP"].ToString();
                    this.INDAPPRLPreparedBy = dbManager.DataReader["IND_APPRL_PREPARED_BY"].ToString();
                    this.INDAPPRLApprovedBy = dbManager.DataReader["IND_APPRL_APPROVED_BY"].ToString();
                    this.INDAPPRLFlag = dbManager.DataReader["IND_APPRL_FLAG"].ToString();

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
            public void IndentApprovalDetails_Select(string IndentApprovalId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_APPROVAL_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_INDENT_APPROVAL_DET.COLOR_ID  AND [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_ID=" + IndentApprovalId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentApprovalProducts.Columns.Add(col);
                //col = new DataColumn("ItemGroup");
                //IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                IndentApprovalProducts.Columns.Add(col);
                //col = new DataColumn("BalQty");
                //IndentProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("SuggestedParty");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ReqFor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ReqDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Detid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_APPRL_DET_QTY"].ToString();
                    dr["Priority"] = dbManager.DataReader["IND_APPRL_DET_PRIORITY"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_APPRL_DET_BRAND"].ToString();
                    dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
                    dr["ReqFor"] = dbManager.DataReader["IND_APPRL_DET_REQ_FOR"].ToString();
                    dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["Specification"] = dbManager.DataReader["IND_APPRL_DET_SPECS"].ToString();
                    dr["Detid"] = dbManager.DataReader["IND_APPRL_DET_ID"].ToString();

                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();

                    IndentApprovalProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }
            public void IndentApprovalDetails_Select1(string IndentApprovalId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                string str = "New";
                string strdet = "New";
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_INDENT_APPROVAL_MAST  WHERE [YANTRA_INDENT_APPROVAL_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and   [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_ID = YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID and YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_FLAG =  '" + str + "' and YANTRA_INDENT_APPROVAL_DET.IND_APPRL_DET_STATUS = '" + strdet + "' AND [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_DET_BRAND = '" + IndentApprovalId + "'");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentApprovalProducts.Columns.Add(col);

                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Priority");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("SuggestedParty");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ReqFor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ReqDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Detid");
                IndentApprovalProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_APPRL_DET_QTY"].ToString();
                    dr["Priority"] = dbManager.DataReader["IND_APPRL_DET_PRIORITY"].ToString();
                    // dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_APPRL_DET_BRAND"].ToString();
                    dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
                    dr["ReqFor"] = dbManager.DataReader["IND_APPRL_DET_REQ_FOR"].ToString();
                    dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["Specification"] = dbManager.DataReader["IND_APPRL_DET_SPECS"].ToString();
                    dr["Detid"] = dbManager.DataReader["IND_APPRL_DET_ID"].ToString();

                    IndentApprovalProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public void IndentApprovalDetails_Select2(string IndentApprovalId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_INDENT_APPROVAL_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                                 "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID and YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_INDENT_APPROVAL_DET.COLOR_ID  AND [YANTRA_INDENT_APPROVAL_DET].IND_APPRL_ID=" + IndentApprovalId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Indentid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Requiredfor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IndentdetId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Price");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("SplAmt");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ArrivalDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("InvoiceNo");
                IndentApprovalProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["IND_APPRL_DET_QTY"].ToString();
                    dr["Indentid"] = dbManager.DataReader["IND_ID"].ToString();
                    // dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_APPRL_DET_BRAND"].ToString();
                    // dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
                    dr["Requiredfor"] = dbManager.DataReader["IND_APPRL_DET_REQ_FOR"].ToString();
                    //dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["IndentdetId"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();

                    IndentApprovalProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public void ProformaInvoiceDetails_Select(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_FIXED_PO_DET],[YANTRA_FIXED_PO_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_COLOR_MAST,[YANTRA_INDENT_DET] WHERE [YANTRA_FIXED_PO_DET].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_FIXED_PO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_INDENT_DET].[IND_DET_ID]=YANTRA_FIXED_PO_DET.[FPO_DET_IND_DET_ID] AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID=YANTRA_FIXED_PO_DET.Color_Id AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_FIXED_PO_MAST].FPO_ID=" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Indentid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Requiredfor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IndentdetId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Price");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("SplAmt");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ArrivalDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("InvoiceNo");
                IndentApprovalProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                    dr["Indentid"] = dbManager.DataReader["IND_ID"].ToString();
                    // dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    // dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
                    dr["Requiredfor"] = dbManager.DataReader["IND_DET_REQ_FOR"].ToString();
                    //dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["IndentdetId"] = dbManager.DataReader["IND_DET_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();

                    IndentApprovalProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }

           public string disc;
            public void SupQuationDtls_Select3(string QuotId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select a.ITEM_CODE,a.SUP_QUOT_DET_RATE,a.SUP_QUOT_DET_CURRENCY,a.SUP_QUOT_DISCOUNT,a.SUP_QUOT_DET_SP_PRICE,"+
                                                "a.SUP_QUOT_DET_ARRIVAL_DATE,a.SUP_QUOT_DET_INVOCIE_NO,a.SUP_QUOT_DET_QTY,a.SUP_QUOT_DET_REQUIREDFOR,"+
                                                "a.SUP_QUOT_IND_ID,a.SUP_QUOT_IND_DET_ID,a.SUP_QUOT_DET_COLOR_ID,e.PRODUCT_COMPANY_NAME,"+
                                                "b.ITEM_MODEL_NO,b.ITEM_NAME,c.COLOUR_NAME,d.UOM_SHORT_DESC,f.CURRENCY_NAME from dbo.YANTRA_SUP_QUOT_DET a inner join " +
                                                "dbo.YANTRA_ITEM_MAST b on a.ITEM_CODE =b.ITEM_CODE inner join  dbo.YANTRA_LKUP_COLOR_MAST c "+
                                                "on a.SUP_QUOT_DET_COLOR_ID=c.COLOUR_ID inner join dbo.YANTRA_LKUP_UOM d "+
                                                "on b.UOM_ID=d.UOM_ID inner join YANTRA_LKUP_PRODUCT_COMPANY e "+
                                                "on b.BRAND_ID=e.PRODUCT_COMPANY_ID inner join dbo.YANTRA_LKUP_CURRENCY_TYPE f on a.SUP_QUOT_DET_CURRENCY=f.CURRENCY_ID  where a.SUP_QUOT_ID=" + QuotId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable IndentApprovalProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Indentid");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Requiredfor");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Color");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("IndentdetId");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Currency");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Price");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("SplAmt");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("ArrivalDate");
                IndentApprovalProducts.Columns.Add(col);
                col = new DataColumn("InvoiceNo");
                IndentApprovalProducts.Columns.Add(col);
              

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = IndentApprovalProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SUP_QUOT_DET_QTY"].ToString();
                    dr["Indentid"] = dbManager.DataReader["SUP_QUOT_IND_ID"].ToString();
                    // dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    // dr["SuggestedParty"] = dbManager.DataReader["IND_APPRL_DET_SUGG_PARTY"].ToString();
                    dr["Requiredfor"] = dbManager.DataReader["SUP_QUOT_DET_REQUIREDFOR"].ToString();
                    //dr["ReqDate"] = Convert.ToDateTime(dbManager.DataReader["IND_APPRL_DET_REQ_BY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["ColorId"] = dbManager.DataReader["SUP_QUOT_DET_COLOR_ID"].ToString();
                    dr["IndentdetId"] = dbManager.DataReader["SUP_QUOT_IND_DET_ID"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Currency"] = dbManager.DataReader["SUP_QUOT_DET_CURRENCY"].ToString();
                    dr["Price"] = dbManager.DataReader["SUP_QUOT_DET_RATE"].ToString();
                    dr["Discount"] = dbManager.DataReader["SUP_QUOT_DISCOUNT"].ToString();
                    dr["SplAmt"] = dbManager.DataReader["SUP_QUOT_DET_SP_PRICE"].ToString();
                    dr["ArrivalDate"] = Convert.ToDateTime(dbManager.DataReader["SUP_QUOT_DET_ARRIVAL_DATE"].ToString()).ToString("dd/MM/yyyy");                 
                    dr["InvoiceNo"] = dbManager.DataReader["SUP_QUOT_DET_INVOCIE_NO"].ToString();
                    
                    IndentApprovalProducts.Rows.Add(dr);
                    this.disc = dbManager.DataReader["SUP_QUOT_DET_CURRENCY"].ToString();
                }
                dbManager.DataReader.Close();
                gv.DataSource = IndentApprovalProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public int SupQuationDtls_Select4(string QuotId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select a.ITEM_CODE,a.SUP_QUOT_DET_RATE,a.SUP_QUOT_DET_CURRENCY,a.SUP_QUOT_DISCOUNT,a.SUP_QUOT_DET_SP_PRICE," +
                                                "a.SUP_QUOT_DET_ARRIVAL_DATE,a.SUP_QUOT_DET_INVOCIE_NO,a.SUP_QUOT_DET_QTY,a.SUP_QUOT_DET_REQUIREDFOR," +
                                                "a.SUP_QUOT_IND_ID,a.SUP_QUOT_IND_DET_ID,a.SUP_QUOT_DET_COLOR_ID,e.PRODUCT_COMPANY_NAME," +
                                                "b.ITEM_MODEL_NO,b.ITEM_NAME,c.COLOUR_NAME,d.UOM_SHORT_DESC,f.CURRENCY_NAME from dbo.YANTRA_SUP_QUOT_DET a inner join " +
                                                "dbo.YANTRA_ITEM_MAST b on a.ITEM_CODE =b.ITEM_CODE inner join  dbo.YANTRA_LKUP_COLOR_MAST c " +
                                                "on a.SUP_QUOT_DET_COLOR_ID=c.COLOUR_ID inner join dbo.YANTRA_LKUP_UOM d " +
                                                "on b.UOM_ID=d.UOM_ID inner join YANTRA_LKUP_PRODUCT_COMPANY e " +
                                                "on b.BRAND_ID=e.PRODUCT_COMPANY_ID inner join dbo.YANTRA_LKUP_CURRENCY_TYPE f on a.SUP_QUOT_DET_CURRENCY=f.CURRENCY_ID  where a.SUP_QUOT_ID=" + QuotId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.disc = dbManager.DataReader["SUP_QUOT_DET_CURRENCY"].ToString();
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
            

            public static void IndentApproval_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT IND_APPRL_NO,IND_APPRL_ID FROM [YANTRA_INDENT_APPROVAL_MAST] ORDER BY IND_APPRL_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IND_APPRL_NO", "IND_APPRL_ID");
                }
               // dbManager.Close();
            }

            public static void IndentApproval_Select1(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                string str = "New";
                _commandText = string.Format("SELECT * FROM [YANTRA_INDENT_APPROVAL_MAST] where YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_FLAG = '" + str + "' ORDER BY YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IND_APPRL_NO", "IND_APPRL_ID");
                }
               // dbManager.Close();
            }
            public static string IndentStatus_Update(SCMStatus Status, string IndId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_MAST] SET  STATUS='{0}' WHERE IND_ID='{1}'", Status, IndId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Indent Status Details", "84");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int Get_Ids_Select(string INDAPPRLId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_INDENT_MAST,YANTRA_INDENT_APPROVAL_MAST where YANTRA_INDENT_APPROVAL_MAST.IND_ID = YANTRA_INDENT_MAST.IND_ID and YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID ='" + INDAPPRLId + "'");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.IndId = dbManager.DataReader["IND_ID"].ToString();

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
        }

        //Methods For Purchase Invoice Form
        public class PurchaseInvoice
        {
            public String PIId, PINo, PIDate, FPOId, FPONo, CUSTId, SUPId, PIInvType, DESPMId, PITermsofDelivery, PIPackChrgs, PIInsuranceChrgs, PITransChrgs, PIMiscChrgs, PIDiscount, PIGrossAmt, PIPreparedBy, PIApprovedBy, PIRemarks, PIStatus, PIAmount, PIChequeNo, PIChequeDate, PIBank, PICustInvNo, PICustInvDate, CpId;
            public string PIDetId, PIItemCode, PIGST, PIDetQty, PIDetRate, PIDetVat, PIDetCst, PIDetAmount, PIDetExcise, PIDetDeliveryDt, PIDetStatus;
            public string PODate, POAmount, PAYBYLC, LCDATE, LCEXPDATE, PAYBYTT, TTDATE, PIPOID, PIVehicleNo, PIVehicleArrDt;
            public string iqitemcode, iqcolorid, iqqty, iqdetid, iqid;

            public PurchaseInvoice()
            {
            }

            public static string PurchaseInvoice_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(PI_NO,LEFT(PI_NO,5),''))),0)+1 FROM [YANTRA_PURCHASE_INVOICE_MAST]").ToString());

                //// _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(PI_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_PURCHASE_INVOICE_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_PURCHASE_INVOICE_MAST", "PI_NO");
            }

            public string PurchaseInvoice_Save()
            {
                this.PINo = PurchaseInvoice_AutoGenCode();
                this.PIId = AutoGenMaxId("[YANTRA_PURCHASE_INVOICE_MAST]", "PI_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_INVOICE_MAST] SELECT ISNULL(MAX(PI_ID),0)+1,'{0}','{1}','{2}',{3},'{4}',{5},'{6}',{7},{8},{9},{10},{11},{12},'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},'{23}','{24}','{25}','{26}','{27}','{28}','{29}' FROM [YANTRA_PURCHASE_INVOICE_MAST]",
                    this.PINo, this.PIDate, this.FPOId, this.SUPId, this.PIInvType, this.DESPMId, this.PITermsofDelivery, this.PIPackChrgs, this.PIInsuranceChrgs, this.PITransChrgs, this.PIMiscChrgs, this.PIDiscount, this.PIGrossAmt, this.PIPreparedBy, this.PIApprovedBy, this.PIRemarks, this.PIStatus, this.PIChequeNo, this.PIChequeDate, this.PIBank, this.PICustInvNo, this.PICustInvDate, this.CpId, this.PAYBYLC, this.LCDATE, this.LCEXPDATE, this.PAYBYTT, this.TTDATE, this.PIVehicleNo,this.PIVehicleArrDt);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Purchase Invoice Details", "85");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
            public void POItemsDetailsBySupName_Select(Control ddlItemCode, string SupplierId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) from YANTRA_FIXED_PO_DET ,YANTRA_FIXED_PO_MAST ,YANTRA_ITEM_MAST where YANTRA_FIXED_PO_DET .FPO_ID =YANTRA_FIXED_PO_MAST .FPO_ID and YANTRA_FIXED_PO_DET .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE and Balance_QTY !=0 and SUP_ID =" + SupplierId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (ddlItemCode is DropDownList)
                {
                    DropDownListBind(ddlItemCode as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }
            public void PurchaseInvoiceDetails1_Select(string PurchaseInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_PURCHASE_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_PURCHASE_INVOICE_DET].PI_ID=" + PurchaseInvoiceId + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SugParty");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PONo");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PIID");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("GST");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Status");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("AppDeliveryDt");
                PurchaseInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                    // dr["BalanceQty"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Rate"] = dbManager.DataReader["PI_DET_RATE"].ToString();
                    dr["Discount"] = dbManager.DataReader["PI_DET_DISC"].ToString();
                    dr["Amount"] = dbManager.DataReader["PI_DET_AMOUNT"].ToString();
                    dr["SugParty"] = dbManager.DataReader["PI_DET_Customer"].ToString();
                    dr["PONo"] = dbManager.DataReader["PI_PONo"].ToString();
                    dr["PIID"] = dbManager.DataReader["PI_DET_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["PI_ColorId"].ToString();
                    dr["GST"] = dbManager.DataReader["PI_GST"].ToString();
                    dr["Status"] = dbManager.DataReader["Status"].ToString();
                    dr["AppDeliveryDt"] = dbManager.DataReader["AppDeliveryDt"].ToString();

                    PurchaseInvoiceProducts.Rows.Add(dr);

                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
            }
            public int PurchaseInvoiceDetails_Delete1(string PurchaseInvoiceId)
            {
                if (dbManager.DataReader == null)
                    dbManager.Open();
                _commandText = string.Format("Delete from [YANTRA_PURCHASE_INVOICE_DET] where PI_DET_ID={0}", PurchaseInvoiceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }
            public string PurchaseInvoiceStatus_Update(string PurchaseInvoiceId)
            {
                 if (dbManager.Transaction == null)
                    dbManager.Open();
                 _commandText = string.Format("update YANTRA_PURCHASE_INVOICE_DET set Status ='{0}',AppDeliveryDt ='{1}' where PI_DET_ID ={2}", this.PIDetStatus, this.PIDetDeliveryDt, PurchaseInvoiceId);
                 _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    //log.add_Update("Purchase Invoice Details", "85");
                }
                return _returnStringMessage;
            }
            public string PurchaseInvoice_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
               // _commandText = string.Format("UPDATE [YANTRA_PURCHASE_INVOICE_MAST] SET PI_DATE='{0}',SUP_ID={1},PI_INV_TYPE='{2}',DESPM_ID={3},PI_TERMSOF_DELIVERY='{4}',PI_PACK_CHRGS={5},PI_INSURANCE_CHRGS={6},PI_TRANS_CHRGS={7},PI_MISC_CHRGS={8},PI_DISCOUNT={9},PI_GROSS_AMT={10},PI_PREPARED_BY={11},PI_APPROVED_BY={12},PI_REMARKS='{13}',PI_STATUS='{14}',PI_CHEQUENO='{15}',PI_CHEQUE_DATE='{16}',PI_BANK_DETAILS='{17}',PI_CUST_INV_NO='{18}',PI_CUST_INV_DATE='{19}',CP_ID={21},PI_PAYBYLC = '{22}',PI_LCDATE ='{23}',PI_LCEXPDATE = '{24}',PI_PAYBYTT ='{25}',PI_TTDATE='{26}',FPO_ID='{27}' WHERE PI_ID='{20}'", this.PIDate, this.SUPId, this.PIInvType, this.DESPMId, this.PITermsofDelivery, this.PIPackChrgs, this.PIInsuranceChrgs, this.PITransChrgs, this.PIMiscChrgs, this.PIDiscount, this.PIGrossAmt, this.PIPreparedBy, this.PIApprovedBy, this.PIRemarks, this.PIStatus, this.PIChequeNo, this.PIChequeDate, this.PIBank, this.PICustInvNo, this.PICustInvDate, this.PIId, this.CpId, this.PAYBYLC, this.LCDATE, this.LCEXPDATE, this.PAYBYTT, this.TTDATE, this.FPOId);

                _commandText = string.Format("UPDATE [YANTRA_PURCHASE_INVOICE_MAST] SET PI_DATE='{0}',SUP_ID={1},PI_INV_TYPE='{2}',DESPM_ID={3},PI_TERMSOF_DELIVERY='{4}',PI_PACK_CHRGS={5},PI_INSURANCE_CHRGS={6},PI_TRANS_CHRGS={7},PI_MISC_CHRGS={8},PI_DISCOUNT={9},PI_GROSS_AMT={10},PI_PREPARED_BY={11},PI_APPROVED_BY={12},PI_REMARKS='{13}',PI_STATUS='{14}',PI_CHEQUENO='{15}',PI_CHEQUE_DATE='{16}',PI_BANK_DETAILS='{17}',PI_CUST_INV_NO='{18}',PI_CUST_INV_DATE='{19}',CP_ID={21},PI_PAYBYLC = '{22}',PI_LCDATE ='{23}',PI_LCEXPDATE = '{24}',PI_PAYBYTT ='{25}',PI_TTDATE='{26}',PI_VehicleNo='{27}',PI_VehicleArrDt='{28}' WHERE PI_ID='{20}'", this.PIDate, this.SUPId, this.PIInvType, this.DESPMId, this.PITermsofDelivery, this.PIPackChrgs, this.PIInsuranceChrgs, this.PITransChrgs, this.PIMiscChrgs, this.PIDiscount, this.PIGrossAmt, this.PIPreparedBy, this.PIApprovedBy, this.PIRemarks, this.PIStatus, this.PIChequeNo, this.PIChequeDate, this.PIBank, this.PICustInvNo, this.PICustInvDate, this.PIId, this.CpId, this.PAYBYLC, this.LCDATE, this.LCEXPDATE, this.PAYBYTT, this.TTDATE,this.PIVehicleNo,this.PIVehicleArrDt);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Purchase Invoice Details", "85");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }
            public void PurchaseOrderDetailsByCustName_Select(Control ddlPONo, String SupplierId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT FPO_ID,FPO_NO from [YANTRA_FIXED_PO_MAST] where SUP_ID=" + SupplierId+" Order by FPO_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (ddlPONo is DropDownList)
                {
                    DropDownListBind(ddlPONo as DropDownList, "FPO_NO", "FPO_ID");
                }
            }
            public string FPOBalanceQty_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int BalanceQty = 0;
                int TotalQty = 0;
                _commandText = string.Format("select YANTRA_FIXED_PO_DET .Balance_QTY from YANTRA_FIXED_PO_DET where ITEM_CODE ={0} and Color_Id ={1} and FPO_ID ={2}", this.PIItemCode, this.PIColorId, this.PIPOID);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["Balance_QTY"].ToString() != string.Empty)
                    {
                        BalanceQty = int.Parse(dbManager.DataReader["Balance_QTY"].ToString());
                    }

                }
                dbManager.DataReader.Close();

                TotalQty = BalanceQty + int.Parse(this.PIDetQty);

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FIXED_PO_DET] set Balance_QTY={0} where ITEM_CODE ={1} and FPO_ID={2} and Color_Id ={3}", TotalQty, this.PIItemCode, this.PIPOID, this.PIColorId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //  _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Balence Quantity Details", "65");

                }
                return _returnStringMessage;
            }

            public string PIStatusUpdate()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PURCHASE_INVOICE_DET] set AppDeliveryDt='{0}',Status='{1}' where PI_DET_ID={2}", PIDetDeliveryDt ,PIStatus ,PIDetId );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //  _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    //log.add_Update("Balence Quantity Details", "65");

                }
                return _returnStringMessage;
            }
            public string BalanceQty_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int BalanceQty = 0;
                int TotalQty = 0;
                _commandText = string.Format("select YANTRA_FIXED_PO_DET .Balance_QTY from YANTRA_FIXED_PO_DET where ITEM_CODE ={0} and Color_Id ={1} and FPO_ID ={2} and FPO_DET_ID={3}", this.PIItemCode, this.PIColorId, this.PIPOID, this.PIDetId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["Balance_QTY"].ToString() != string.Empty)
                    {
                        BalanceQty = int.Parse(dbManager.DataReader["Balance_QTY"].ToString());
                    }

                }
                dbManager.DataReader.Close();

                TotalQty = BalanceQty - int.Parse(this.PIDetQty);

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FIXED_PO_DET] set Balance_QTY={0} where ITEM_CODE ={1} and FPO_ID={2} and Color_Id ={3} and FPO_DET_ID={4}", TotalQty, this.PIItemCode, this.PIPOID, this.PIColorId, this.PIDetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //  _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Balence Quantity Details", "65");

                }
                return _returnStringMessage;
            }
            public string PurchaseInvoice_Delete(string PurchaseInvoiceId)
            {
                if (DeleteRecord("[YANTRA_PURCHASE_INVOICE_DET]", "PI_ID", PurchaseInvoiceId) == true)
                {
                    if (DeleteRecord("[YANTRA_PURCHASE_INVOICE_MAST]", "PI_ID", PurchaseInvoiceId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Purchase Invoice Details", "85");

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

            public string PIDetDisc, PIDetCustomer,PIPONo,PIColorId;
            public string PurchaseInvoiceDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
               // _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_INVOICE_DET] SELECT ISNULL(MAX(PI_DET_ID),0)+1,'{0}','{1}','{2}',{3},{4},'{5}',{6},{7},{8},{9},'{10}','{11}' FROM [YANTRA_PURCHASE_INVOICE_DET]", this.PIId, this.PIItemCode, this.PIDetQty, this.PIDetRate, this.PIDetDisc, this.PIDetCustomer, this.PIDetAmount, this.PIPONo, this.PIColorId, this.PIGST, this.PIDetStatus, this.PIDetDeliveryDt);
                 _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_INVOICE_DET] SELECT ISNULL(MAX(PI_DET_ID),0)+1,{0},{1},{2},{3},{4},'{5}',{6},{7},{8},{9},'{10}','{11}' FROM [YANTRA_PURCHASE_INVOICE_DET]", this.PIId, this.PIItemCode, this.PIDetQty, this.PIDetRate, this.PIDetDisc, this.PIDetCustomer, this.PIDetAmount, this.PIPONo, this.PIColorId, this.PIGST, this.PIDetStatus, this.PIDetDeliveryDt);


                 _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Purchase Invoice Details", "85");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int PurchaseInvoiceDetails_Delete(string PurchaseInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_PURCHASE_INVOICE_DET] WHERE PI_ID={0}", PurchaseInvoiceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public int PurchaseInvoice_Select(string PurchaseInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_MAST],[YANTRA_SUPPLIER_MAST],[YANTRA_LKUP_DESP_MODE],YANTRA_FIXED_PO_MAST WHERE [YANTRA_PURCHASE_INVOICE_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_PURCHASE_INVOICE_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND YANTRA_FIXED_PO_MAST.[FPO_ID]=YANTRA_PURCHASE_INVOICE_MAST.FPO_ID AND [YANTRA_PURCHASE_INVOICE_MAST].PI_ID='" + PurchaseInvoiceId + "' ORDER BY [YANTRA_PURCHASE_INVOICE_MAST].PI_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PIId = dbManager.DataReader["PI_ID"].ToString();
                    this.PINo = dbManager.DataReader["PI_NO"].ToString();
                    this.PIDate = Convert.ToDateTime(dbManager.DataReader["PI_CUST_INV_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    this.FPONo = dbManager.DataReader["FPO_NO"].ToString();
                    this.POAmount = dbManager.DataReader["TotalAmt"].ToString();
                    this.PODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SUPId = dbManager.DataReader["SUP_ID"].ToString();
                    this.PIInvType = dbManager.DataReader["PI_INV_TYPE"].ToString();
                    this.DESPMId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.PITermsofDelivery = dbManager.DataReader["PI_TERMSOF_DELIVERY"].ToString();
                    this.PIPackChrgs = dbManager.DataReader["PI_PACK_CHRGS"].ToString();
                    this.PIInsuranceChrgs = dbManager.DataReader["PI_INSURANCE_CHRGS"].ToString();
                    this.PITransChrgs = dbManager.DataReader["PI_TRANS_CHRGS"].ToString();
                    this.PIMiscChrgs = dbManager.DataReader["PI_MISC_CHRGS"].ToString();
                    this.PIDiscount = dbManager.DataReader["PI_DISCOUNT"].ToString();
                    this.PIGrossAmt = dbManager.DataReader["PI_GROSS_AMT"].ToString();
                    this.PIPreparedBy = dbManager.DataReader["PI_PREPARED_BY"].ToString();
                    this.PIApprovedBy = dbManager.DataReader["PI_APPROVED_BY"].ToString();
                    this.PIRemarks = dbManager.DataReader["PI_REMARKS"].ToString();
                    this.PIStatus = dbManager.DataReader["PI_STATUS"].ToString();
                    this.PIChequeNo = dbManager.DataReader["PI_CHEQUENO"].ToString();
                    this.PIChequeDate = Convert.ToDateTime(dbManager.DataReader["PI_CHEQUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PIBank = dbManager.DataReader["PI_BANK_DETAILS"].ToString();
                    this.PICustInvNo = dbManager.DataReader["PI_CUST_INV_NO"].ToString();
                    this.PICustInvDate = Convert.ToDateTime(dbManager.DataReader["PI_CUST_INV_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.PODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                   // this.POAmount = dbManager.DataReader["FPO_NET_AMOUNT"].ToString();

                    this.TTDATE = Convert.ToDateTime(dbManager.DataReader["PI_TTDATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PAYBYTT = dbManager.DataReader["PI_PAYBYTT"].ToString();
                    this.LCDATE = Convert.ToDateTime(dbManager.DataReader["PI_LCDATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PAYBYLC = dbManager.DataReader["PI_PAYBYLC"].ToString();
                    this.LCEXPDATE = Convert.ToDateTime(dbManager.DataReader["PI_LCEXPDATE"].ToString()).ToString("dd/MM/yyyy");


                    this.PIVehicleArrDt = Convert.ToDateTime(dbManager.DataReader["PI_VehicleArrDt"].ToString()).ToString("dd/MM/yyyy");
                    this.PIVehicleNo = dbManager.DataReader["PI_VehicleNo"].ToString();

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

            public void PurchaseInvoiceDetails_Select(string PurchaseInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_PURCHASE_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_PURCHASE_INVOICE_DET].PI_ID=" + PurchaseInvoiceId + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                PurchaseInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("Cst");
                //PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SugParty");
                PurchaseInvoiceProducts.Columns.Add(col);   
                col = new DataColumn("ItemTypeId");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PONo");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                PurchaseInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                    // dr["BalanceQty"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Rate"] = dbManager.DataReader["PI_DET_RATE"].ToString();

                    dr["PONo"] = dbManager.DataReader["PI_PONo"].ToString();
                    dr["ColorId"] = dbManager.DataReader["PI_ColorId"].ToString();
                    //["Excise"] = dbManager.DataReader["PI_DET_EXCISE"].ToString();
                    dr["Amount"] = dbManager.DataReader["PI_DET_AMOUNT"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["Discount"] = dbManager.DataReader["PI_DET_DISC"].ToString();
                    dr["SugParty"] = dbManager.DataReader["PI_DET_Customer"].ToString();
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public void PurchaseshipmentDetails_Select(string PurchaseInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                //_commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_PURCHASE_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_PURCHASE_INVOICE_DET].PI_ID=" + PurchaseInvoiceId + "");

                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_PURCHASE_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                             "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_PURCHASE_INVOICE_DET].PI_ID=" + PurchaseInvoiceId + "");


                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                PurchaseInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("Cst");
                //PurchaseInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("Excise");
                //PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Customer");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PI_DET_ID");
                PurchaseInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                    // dr["BalanceQty"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Rate"] = dbManager.DataReader["PI_DET_RATE"].ToString();
                    dr["Discount"] = dbManager.DataReader["PI_DET_DISC"].ToString();
                    //dr["VAT"] = dbManager.DataReader["PI_DET_VAT"].ToString();
                    //dr["Cst"] = dbManager.DataReader["PI_DET_CST"].ToString();
                    //dr["Excise"] = dbManager.DataReader["PI_DET_EXCISE"].ToString();
                    dr["Amount"] = dbManager.DataReader["PI_DET_AMOUNT"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["Customer"] = dbManager.DataReader["PI_DET_Customer"].ToString();
                    dr["PI_DET_ID"] = dbManager.DataReader["PI_DET_ID"].ToString();
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
            }

            public string PurchaseInvoiceApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PURCHASE_INVOICE_MAST] SET PI_APPROVED_BY={0},PI_STATUS='{1}' WHERE PI_ID='{2}'", this.PIApprovedBy, "Closed", this.PIId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Purchase Invoice Approve Details", "85");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public static void PurchaseInvoice_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_MAST] ORDER BY PI_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PI_NO", "PI_ID");
                }
               // dbManager.Close();
            }

            public static void PurchaseInvoice_Select(Control ControlForBind, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_MAST] WHERE PI_ID=" + FPOId + " ORDER BY PI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PI_CUST_INV_NO", "PI_ID");
                }
               // dbManager.Close();
            }
            public static void FPO_Select(Control ControlForBind, string PIId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                    _commandText = string.Format("Select * from [YANTRA_PURCHASE_INVOICE_MAST],[YANTRA_FIXED_PO_MAST] where YANTRA_PURCHASE_INVOICE_MAST .FPO_ID  =YANTRA_FIXED_PO_MAST .FPO_ID and [YANTRA_PURCHASE_INVOICE_MAST].PI_ID=" + PIId + " ");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "FPO_NO", "FPO_ID");
                }
            }

            public static void CHKItemsBind(Control ControlForBind, string PIID)
            {

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_ITEM_MAST.ITEM_CODE ,ITEM_MODEL_NO  from YANTRA_ITEM_MAST ,YANTRA_PURCHASE_INVOICE_DET where YANTRA_ITEM_MAST .ITEM_CODE =YANTRA_PURCHASE_INVOICE_DET .ITEM_CODE and PI_ID =" + PIID  + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
            }
             public static void PIforsupname_Select(Control ControlForBind, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_SUPPLIER_MAST,[YANTRA_PURCHASE_INVOICE_MAST] WHERE  YANTRA_PURCHASE_INVOICE_MAST.SUP_ID =  YANTRA_SUPPLIER_MAST.SUP_ID     and YANTRA_PURCHASE_INVOICE_MAST.PI_ID=" + FPOId + " ORDER BY  YANTRA_SUPPLIER_MAST.SUP_ID  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
               // dbManager.Close();
            }
        
            public static void pochangeforsupname_Select(Control ControlForBind, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_SUPPLIER_MAST,[YANTRA_FIXED_PO_MAST] WHERE  YANTRA_FIXED_PO_MAST.SUP_ID =  YANTRA_SUPPLIER_MAST.SUP_ID     and YANTRA_FIXED_PO_MAST.FPO_ID=" + FPOId + " ORDER BY  YANTRA_SUPPLIER_MAST.SUP_ID  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_NAME", "SUP_ID");
                }
               // dbManager.Close();
            }
        }

        //Method for Supplier Enquiry Form
        public class SuppliersEnquiry
        {
            public string SuppEnqId, SuppEnqNo, SuppEnqDate, SuppEnqOrgBy, SuppEnqStatus, SuppEnqFollwUp, SuppEnqDueDate, SuppEnqDespId, SuppEnqPreparedBy, SuppEnqApprovedBy, CpId;
            public string SuppEnqItemCode, SuppEnqDetQty, SuppEnqDetSpec, SuppEnqDetPriority, SuppEnqDetBrand, SuppEnqDetReqFor, SuppEnqDetStatus;
            public string SupId, EnqSuppId;
            public string SuppEnqDetId, CustId;

            public SuppliersEnquiry()
            { }

            public static string SuppliersEnquiry_AutoGenCode()
            {
                return SM.AutoGenMaxNo("YANTRA_SUP_ENQ_MAST", "SUP_ENQ_NO");
                //string _codePrefix = CurrentFinancialYear() + "";
                ////string _codePrefix = "SUPPENQ/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SUP_ENQ_NO,LEFT(SUP_ENQ_NO,5),''))),0)+1 FROM [YANTRA_SUP_ENQ_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SUP_ENQ_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_SUP_ENQ_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
            }

            public string SuppliersEnquiryMater_Save()
            {
                this.SuppEnqNo = SuppliersEnquiry_AutoGenCode();
                this.SuppEnqId = AutoGenMaxId("YANTRA_SUP_ENQ_MAST", "SUP_ENQ_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_ENQ_MAST] VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10})", this.SuppEnqId, this.SuppEnqNo, this.SuppEnqDate, this.SuppEnqOrgBy, this.SuppEnqStatus, this.SuppEnqFollwUp, this.SuppEnqDueDate, this.SuppEnqDespId, this.SuppEnqPreparedBy, this.SuppEnqApprovedBy, this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Enquiry Details", "86");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersEnquiryMaster_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SUP_ENQ_MAST] SET SUP_ENQ_DATE='{0}',SUP_ENQ_ORIGINATED_BY='{1}',SUP_ENQ_STATUS='{2}',SUP_ENQ_FOLLOWUP_CRI='{3}',SUP_ENQ_DUE_DATE='{4}',DESPM_ID={5},SUP_ENQ_PREPARED_BY='{6}',SUP_ENQ_APPROVED_BY='{7}',CP_ID={9} WHERE SUP_ENQ_ID={8}", this.SuppEnqDate, this.SuppEnqOrgBy, this.SuppEnqStatus, this.SuppEnqFollwUp, this.SuppEnqDueDate, this.SuppEnqDespId, this.SuppEnqPreparedBy, this.SuppEnqApprovedBy, this.SuppEnqId, this.CpId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Supplier Enquiry Details", "86");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersEnquiryMaster_Delete(string SuppEnqId)
            {
                if (DeleteRecord("[YANTRA_ENQ_SUPPLIERS]", "SUP_ENQ_ID", SuppEnqId) == true)
                {
                    if (DeleteRecord("[YANTRA_SUP_ENQ_DET]", "SUP_ENQ_ID", SuppEnqId) == true)
                    {
                        if (DeleteRecord("[YANTRA_SUP_ENQ_MAST]", "SUP_ENQ_ID", SuppEnqId) == true)
                        {
                            _returnStringMessage = "Data Deleted Successfully";
                            log.add_Delete("Supplier Enquiry Details", "86");

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
                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public int SuppliersEnquiryMaster_Select(string SuppEnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_ENQ_MAST],[YANTRA_ENQ_SUPPLIERS],[YANTRA_SUPPLIER_MAST] WHERE [YANTRA_SUP_ENQ_MAST].SUP_ENQ_ID=[YANTRA_ENQ_SUPPLIERS].SUP_ENQ_ID " +
                "AND [YANTRA_ENQ_SUPPLIERS].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_SUP_ENQ_MAST].SUP_ENQ_ID=" + SuppEnqId + " ORDER BY [YANTRA_SUP_ENQ_MAST].SUP_ENQ_ID DESC");
                //_commandText = string.Format("SELECT * FROM [YANTRA_SUP_ENQ_MAST] ORDER BY SUP_ENQ_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SuppEnqId = dbManager.DataReader["SUP_ENQ_ID"].ToString();
                    this.SuppEnqNo = dbManager.DataReader["SUP_ENQ_NO"].ToString();
                    this.SuppEnqDate = Convert.ToDateTime(dbManager.DataReader["SUP_ENQ_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SuppEnqOrgBy = dbManager.DataReader["SUP_ENQ_ORIGINATED_BY"].ToString();
                    this.SuppEnqStatus = dbManager.DataReader["SUP_ENQ_STATUS"].ToString();
                    this.SuppEnqFollwUp = dbManager.DataReader["SUP_ENQ_FOLLOWUP_CRI"].ToString();
                    this.SuppEnqDueDate = Convert.ToDateTime(dbManager.DataReader["SUP_ENQ_DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SuppEnqDespId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SuppEnqPreparedBy = dbManager.DataReader["SUP_ENQ_PREPARED_BY"].ToString();
                    this.SuppEnqApprovedBy = dbManager.DataReader["SUP_ENQ_APPROVED_BY"].ToString();
                    //this.SuppEnqbr= dbManager.DataReader["SUP_ENQ_APPROVED_BY"].ToString();
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


            

            public string SuppliersEnquiryDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_ENQ_DET] SELECT ISNULL(MAX(SUP_ENQ_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}' FROM [YANTRA_SUP_ENQ_DET]", this.SuppEnqId, this.SuppEnqItemCode, this.SuppEnqDetQty, this.SuppEnqDetBrand, this.SuppEnqDetReqFor, this.SuppEnqDetSpec, this.SuppEnqDetPriority, this.SuppEnqDetStatus);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Enquiry Details", "86");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string UpdateIndApprovalDetails(string IndeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INDENT_DET] SET IND_DET_STATUS ='Close' where IND_DET_ID =" + IndeId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Indent Approval Details", "87");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int SuppliersEnquiryDetails_Delete(string SuppEnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SUP_ENQ_DET] WHERE SUP_ENQ_ID={0}", SuppEnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public void SuppliersEnquiryDetails_Select(string SuppEnqId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM] WHERE [YANTRA_INDENT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID AND [YANTRA_INDENT_DET].IND_ID=" + IndentId);
                string str = "New";
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_ENQ_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM] WHERE [YANTRA_SUP_ENQ_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID  AND [YANTRA_SUP_ENQ_DET].SUP_ENQ_ID=" + SuppEnqId + " and YANTRA_SUP_ENQ_DET.SUP_ENQ_STATUS = '" + str + "' ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable ItemDetails = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("ItemName");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("ItemType");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("UOM");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("Quantity");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("Specifications");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("Priority");
                ItemDetails.Columns.Add(col);
                col = new DataColumn("Brand");
                ItemDetails.Columns.Add(col);

                col = new DataColumn("ReqFor");
                ItemDetails.Columns.Add(col);

                col = new DataColumn("Specification");
                ItemDetails.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = ItemDetails.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SUP_ENQ_QTY"].ToString();
                    dr["Specification"] = dbManager.DataReader["SUP_ENQ_SPECS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SUP_ENQ_PRORITY"].ToString();
                    dr["Brand"] = dbManager.DataReader["SUP_ENQ_BRAND"].ToString();
                    dr["ReqFor"] = dbManager.DataReader["SUP_ENQ_REQ_FOR"].ToString();
                    ItemDetails.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = ItemDetails;
                gv.DataBind();
               // dbManager.Close();
            }

            public void EnquirySuppliersDetails_Select(string SuppEnqId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from [YANTRA_ENQ_SUPPLIERS],[YANTRA_SUPPLIER_MAST] WHERE [YANTRA_ENQ_SUPPLIERS].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_ENQ_SUPPLIERS].SUP_ENQ_ID=" + SuppEnqId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SupplierDetails = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("SuppId");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("Name");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("Designation");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("ContactPerson");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("PhoneNo");
                SupplierDetails.Columns.Add(col);
                col = new DataColumn("Email");
                SupplierDetails.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SupplierDetails.NewRow();
                    dr["SuppId"] = dbManager.DataReader["SUP_ID"].ToString();
                    dr["Name"] = dbManager.DataReader["SUP_NAME"].ToString();
                    dr["ContactPerson"] = dbManager.DataReader["SUP_CONTACT_PERSON"].ToString();
                    dr["PhoneNo"] = dbManager.DataReader["SUP_PHONE"].ToString();
                    dr["Email"] = dbManager.DataReader["SUP_EMAIL"].ToString();

                    SupplierDetails.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SupplierDetails;
                gv.DataBind();
               // dbManager.Close();
            }

            public string EnqSuppliersDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ENQ_SUPPLIERS] SELECT ISNULL(MAX(ENQ_SUP_ID),0)+1,{0},{1} FROM [YANTRA_ENQ_SUPPLIERS]", this.SuppEnqId, this.SupId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Enquiry Details", "86");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int EnqSuppliersDetails_Delete(string SuppEnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_ENQ_SUPPLIERS] WHERE SUP_ENQ_ID={0}", SuppEnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public static void SuppliersEnquiryMaster_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_ENQ_MAST] ORDER BY SUP_ENQ_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_ENQ_NO", "SUP_ENQ_ID");
                }
               // dbManager.Close();
            }

            public static void SuppliersEnquiryMaster_Select(Control ControlForBind, string SupId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT YANTRA_SUP_ENQ_MAST.SUP_ENQ_ID, YANTRA_SUP_ENQ_MAST.SUP_ENQ_NO FROM  YANTRA_SUP_ENQ_MAST INNER JOIN YANTRA_ENQ_SUPPLIERS ON YANTRA_SUP_ENQ_MAST.SUP_ENQ_ID = YANTRA_ENQ_SUPPLIERS.SUP_ENQ_ID INNER JOIN YANTRA_SUPPLIER_MAST ON YANTRA_ENQ_SUPPLIERS.SUP_ID = YANTRA_SUPPLIER_MAST.SUP_ID where  YANTRA_SUPPLIER_MAST.SUP_ID =" + SupId);
                // _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST],[YANTRA_ENQ_SUPPLIERS],YANTRA_SUP_ENQ_MAST WHERE [YANTRA_SUPPLIER_MAST].SUP_ID = [YANTRA_ENQ_SUPPLIERS].SUP_ID AND [YANTRA_ENQ_SUPPLIERS].SUP_ENQ_ID = YANTRA_SUP_ENQ_MAST.SUP_ENQ_ID and [YANTRA_SUPPLIER_MAST].SUP_ID=" + SupId + " ORDER BY [YANTRA_SUPPLIER_MAST].SUP_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SUP_ENQ_NO", "SUP_ENQ_ID");
                }
               // dbManager.Close();
            }

            public static void SuppliersEnquiryMaster_Select2(Control ControlForBind, string SupId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID, YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_NO FROM  YANTRA_INDENT_APPROVAL_MAST INNER JOIN YANTRA_INDENT_APPROVAL_SUPPLIER ON YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID = YANTRA_INDENT_APPROVAL_SUPPLIER.Ind_Apprl_Id where  YANTRA_INDENT_APPROVAL_SUPPLIER.Supplier_Id =" + SupId);
                _commandText = string.Format("SELECT YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID, YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_NO FROM  YANTRA_INDENT_APPROVAL_MAST where YANTRA_INDENT_APPROVAL_MAST.SUP_ID ='" + SupId +"'   order by YANTRA_INDENT_APPROVAL_MAST.IND_APPRL_ID desc");
                // _commandText = string.Format("SELECT * FROM [YANTRA_SUPPLIER_MAST],[YANTRA_ENQ_SUPPLIERS],YANTRA_SUP_ENQ_MAST WHERE [YANTRA_SUPPLIER_MAST].SUP_ID = [YANTRA_ENQ_SUPPLIERS].SUP_ID AND [YANTRA_ENQ_SUPPLIERS].SUP_ENQ_ID = YANTRA_SUP_ENQ_MAST.SUP_ENQ_ID and [YANTRA_SUPPLIER_MAST].SUP_ID=" + SupId + " ORDER BY [YANTRA_SUPPLIER_MAST].SUP_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IND_APPRL_NO", "IND_APPRL_ID");
                }
               // dbManager.Close();
            }
        }

        //Methods for Checking Format
        public class CheckingFormat
        {
            public string CHKId, CHKNo, CHKDate, CHKEquipmentName,
                CHKManufacturerName, CHKReceivedOn, CHKModel, CHKSerialNo, CHKPONo,
                CHKPODate, CHKInvoiceNo, CHKInvoiceDate, CHKPacking, CHKQuantity, CHKPhyDamage, CHKAccessories,
                CHKFittings, CHKPowerCable, CHKOperatingManual, CHKDisplayPanel, CHKRemarks,ChkPOId,ChkInvId,
                CHKPreparedBy, CHKApprovedBy, ItemTypeId, ItemCode, CHKQty, CHKGatepass, CHKLrno, CHKCPID, CHKSTOREINCHARGE, CHKACCOUNTS, CHKAUTHORISEDPAYMENTS;
            public string CHKID, CHKDETITEMCODE, CHKDETORDEREDQTY, CHKDETRECEIVEDQTY, CHKDETACCEPTEDQTY, CHKDETREJECTEDQTY, CHKDETRATE, CHKDETGODOWNID, CHKDETNETQTY, CHKDETREMARKS, CHKDETCOLOR, qtyid;

            public CheckingFormat()
            {
            }

            public static string CheckingFormat_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "IND/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(CHK_NO,LEFT(CHK_NO,5),''))),0)+1  FROM [YANTRA_CHECKING_FORMAT]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,'" + _codePrefix + "',''))),0)+1 FROM [YANTRA_INDENT_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_CHECKING_FORMAT", "CHK_NO");
            }

            public string CheckingFormat_Save()
            {
                this.CHKNo = CheckingFormat_AutoGenCode();

                this.CHKId = AutoGenMaxId("[YANTRA_CHECKING_FORMAT]", "CHK_ID");

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CHECKING_FORMAT] SELECT ISNULL(MAX(CHK_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},{23},'{24}','{25}','{26}',{27},'{28}','{29}','{30}','{31}','{32}','{33}','{34}' FROM [YANTRA_CHECKING_FORMAT]",
                 this.CHKNo, this.CHKDate, this.CHKEquipmentName, this.CHKManufacturerName, this.CHKReceivedOn, this.CHKModel, this.CHKSerialNo, this.CHKPONo, this.CHKPODate, this.CHKInvoiceNo, this.CHKInvoiceDate, this.CHKPacking, this.CHKQuantity, this.CHKPhyDamage, this.CHKAccessories, this.CHKFittings, this.CHKPowerCable, this.CHKOperatingManual, this.CHKDisplayPanel, this.CHKRemarks, this.CHKPreparedBy, this.CHKApprovedBy, this.ItemCode, this.ItemTypeId, this.CHKQty, this.CHKGatepass, this.CHKLrno, this.CHKCPID, this.CHKSTOREINCHARGE, this.CHKACCOUNTS, this.CHKAUTHORISEDPAYMENTS,DateTime.Now,DateTime.Now,this.ChkInvId,this.ChkPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Checking Format Details", "88");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string CheckingFormat_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_CHECKING_FORMAT] SET CHK_NO='{0}',CHK_DATE='{1}',CHK_EQUIPMENT_NAME='{2}',CHK_MANUFACTURER_NAME='{3}',CHK_RECEIVED_ON='{4}',CHK_MODEL='{5}',CHK_SERIAL_NO='{6}',CHK_PO_NO='{7}',CHK_PO_DATE='{8}',CHK_INVOICE_NO='{9}',CHK_INVOICE_DATE='{10}',CHK_PACKING='{11}',CHK_QUANTITY='{12}',CHK_PHYSICAL_DAMAGE='{13}',CHK_ACCESSORIES='{14}',CHK_FITTINGS='{15}',CHK_POWER_CABLE='{16}',CHK_OPERATING_MANUAL='{17}',CHK_DISPLAY_PANEL='{18}',CHK_REMARKS='{19}',CHK_PREPARED_BY='{20}',CHK_APPROVED_BY='{21}',IT_TYPE_ID={22},ITEM_CODE='{23}',CHK_QTY='{24}',GATEPASS_NO = '{25}',LR_NO = '{26}' ,CP_ID = {27},STORE_INCHARGE = '{28}',ACCOUNTS = '{29}',AUTHORISED_PAYMENTS = '{30}',DateInserted='{32}',CHK_InvId='{33}',CHK_POId='{34}' WHERE CHK_ID={31}",
                this.CHKNo, this.CHKDate, this.CHKEquipmentName, this.CHKManufacturerName, this.CHKReceivedOn, this.CHKModel, this.CHKSerialNo, this.CHKPONo, this.CHKPODate, this.CHKInvoiceNo, this.CHKInvoiceDate, this.CHKPacking, this.CHKQuantity, this.CHKPhyDamage, this.CHKAccessories, this.CHKFittings, this.CHKPowerCable, this.CHKOperatingManual, this.CHKDisplayPanel, this.CHKRemarks, this.CHKPreparedBy, this.CHKApprovedBy, this.ItemCode, this.ItemTypeId, this.CHKQty, this.CHKGatepass, this.CHKLrno, this.CHKCPID, this.CHKSTOREINCHARGE, this.CHKACCOUNTS, this.CHKAUTHORISEDPAYMENTS, this.CHKId,DateTime.Now,this.ChkInvId ,this .ChkPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Checking Format Details", "88");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string POStatus_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_FIXED_PO_MAST] SET FPO_PO_STATUS='Received' WHERE FPO_ID={0}", this.CHKPONo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    // log.add_Update("Checking Format Details", "88");

                }
                // dbManager.Close();
                return _returnStringMessage;
            }

            public string CheckingFormat_Delete(string CheckingFormatId)
            {
                DeleteRecord("[YANTRA_CHECKING_FORMAT_DETAILS]", "CHK_ID", CheckingFormatId);
                {
                    if (DeleteRecord("[YANTRA_CHECKING_FORMAT]", "CHK_ID", CheckingFormatId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Checking Format Details", "88");

                    }
                    else
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                }
                //{
                //    _returnStringMessage = "Some Data Missing.";
                //}

                return _returnStringMessage;
            }

            public string CheckingFormatDetails_Delete(string CheckingFormatId)
            {
                if (DeleteRecord("[YANTRA_CHECKING_FORMAT_DETAILS]", "CHK_DET_ID", CheckingFormatId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Checking Format Details", "88");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public int TempCheckingFormatDetails_Delete(string RefNo, string ItemCode,string ColorID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [Temp_Inward] WHERE Reference_No='{0}' and ItemCode='{1}' and Color_Id={2}", RefNo, ItemCode, ColorID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }

            public static void CheckinfDormat_SelctByDate(Control ControlForBind, string Fromdate, string Todate)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                 _commandText = string.Format("select * from YANTRA_CHECKING_FORMAT where CHK_DATE between '{0}' and '{1}' ", Fromdate ,Todate );
                 dbManager.ExecuteReader(CommandType.Text, _commandText);
                 if (ControlForBind is DropDownList)
                 {
                     DropDownListBind(ControlForBind as DropDownList, "CHK_NO", "CHK_ID");
                 }
            }
            public static void CheckingFormat_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CHECKING_FORMAT] ORDER BY CHK_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CHK_NO", "CHK_ID");
                }
               // dbManager.Close();
            }
            string CHKFPONo;
            public int CheckingFormat_Select(string CheckingFormatId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_CHECKING_FORMAT]" +
                                                " WHERE  [YANTRA_CHECKING_FORMAT].CHK_ID='" + CheckingFormatId + "' ORDER BY [YANTRA_CHECKING_FORMAT].CHK_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CHKId = dbManager.DataReader["CHK_ID"].ToString();
                    this.CHKNo = dbManager.DataReader["CHK_NO"].ToString();
                    this.CHKDate = Convert.ToDateTime(dbManager.DataReader["CHK_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CHKEquipmentName = dbManager.DataReader["CHK_EQUIPMENT_NAME"].ToString();
                    this.CHKManufacturerName = dbManager.DataReader["CHK_MANUFACTURER_NAME"].ToString();
                    this.CHKReceivedOn = Convert.ToDateTime(dbManager.DataReader["CHK_RECEIVED_ON"].ToString()).ToString("dd/MM/yyyy");
                    this.CHKModel = dbManager.DataReader["CHK_MODEL"].ToString();
                    this.CHKSerialNo = dbManager.DataReader["CHK_SERIAL_NO"].ToString();
                    this.CHKPONo = dbManager.DataReader["CHK_PO_NO"].ToString();
                    this.CHKPODate = Convert.ToDateTime(dbManager.DataReader["CHK_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CHKInvoiceNo = dbManager.DataReader["CHK_INVOICE_NO"].ToString();
                    this.CHKInvoiceDate = Convert.ToDateTime(dbManager.DataReader["CHK_INVOICE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CHKPacking = dbManager.DataReader["CHK_PACKING"].ToString();
                    this.CHKQuantity = dbManager.DataReader["CHK_QUANTITY"].ToString();
                    this.CHKPhyDamage = dbManager.DataReader["CHK_PHYSICAL_DAMAGE"].ToString();
                    this.CHKAccessories = dbManager.DataReader["CHK_ACCESSORIES"].ToString();
                    this.CHKFittings = dbManager.DataReader["CHK_FITTINGS"].ToString();
                    this.CHKPowerCable = dbManager.DataReader["CHK_POWER_CABLE"].ToString();
                    this.CHKOperatingManual = dbManager.DataReader["CHK_OPERATING_MANUAL"].ToString();
                    this.CHKDisplayPanel = dbManager.DataReader["CHK_DISPLAY_PANEL"].ToString();
                    this.CHKRemarks = dbManager.DataReader["CHK_REMARKS"].ToString();
                    this.CHKPreparedBy = dbManager.DataReader["CHK_PREPARED_BY"].ToString();
                    this.CHKApprovedBy = dbManager.DataReader["CHK_APPROVED_BY"].ToString();
                    this.ItemTypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.CHKQty = dbManager.DataReader["CHK_QTY"].ToString();
                    this.CHKAUTHORISEDPAYMENTS = dbManager.DataReader["AUTHORISED_PAYMENTS"].ToString();
                    this.CHKACCOUNTS = dbManager.DataReader["ACCOUNTS"].ToString();
                    this.CHKSTOREINCHARGE = dbManager.DataReader["STORE_INCHARGE"].ToString();
                    this.CHKLrno = dbManager.DataReader["LR_NO"].ToString();
                    this.CHKGatepass = dbManager.DataReader["GATEPASS_NO"].ToString();
                    this.CHKCPID = dbManager.DataReader["CP_ID"].ToString();
                    this.ChkInvId = dbManager.DataReader["CHK_InvId"].ToString();
                    this.ChkPOId = dbManager.DataReader["CHK_POId"].ToString();
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

            public string ItemMaster_AutoGen_ItemQty()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(ITEM_QTY_ID),0)+1 FROM YANTRA_ITEM_QTY").ToString());
               // dbManager.Close();
                return _returnIntValue.ToString();
            }

            public string CheckingFormatIssueStock_Update(string ItemCode, string Qty, string Cpid, string GdId, string Colorid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_QTY]", "ITEM_CODE", ItemCode, "CP_ID", Cpid, "GODOWN_ID", GdId, "COLOUR_ID", Colorid) == true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET  ITEM_QTY_IN_HAND=CONVERT(BIGINT,ITEM_QTY_IN_HAND)+'{0}' WHERE ITEM_CODE = '{1}' and CP_ID = '{2}'and GODOWN_ID = '{3}' and COLOUR_ID = '{4}'", Qty, ItemCode, Cpid, GdId, Colorid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Checking Format Issue Stock Details", "88");

                    }
                }
                else
                {
                    this.qtyid = ItemMaster_AutoGen_ItemQty();
                    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_QTY] VALUES ({0},{1},{2},{3},{4},{5},{6})", this.qtyid, ItemCode, Qty, Cpid, "0", GdId, Colorid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Checking Format Issue Stock Details", "88");

                    }
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string CheckingFormatUpdate_Update(string ItemCode, string Qty, string Cpid, string GdId, string Colorid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_QTY]", "ITEM_CODE", ItemCode, "CP_ID", Cpid, "GODOWN_ID", GdId, "COLOUR_ID", Colorid) == true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET  ITEM_QTY_IN_HAND=CONVERT(BIGINT,ITEM_QTY_IN_HAND)+'{0}' WHERE ITEM_CODE = '{1}' and CP_ID = '{2}'and GODOWN_ID = '{3}' and COLOUR_ID = '{4}'", Qty, ItemCode, Cpid, GdId, Colorid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Checking Format Details", "88");

                    }
                }
                else
                {
                    this.qtyid = ItemMaster_AutoGen_ItemQty();
                    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_QTY] VALUES ({0},{1},{2},{3},{4},{5},{6})", this.qtyid, ItemCode, Qty, Cpid, "0", GdId, Colorid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Checking Format Details", "88");

                    }
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            /// <summary>
            /// iNTERAL oRDER APPROVAL RESERVE STOCK
            /// </summary>
            public string ReserveStock_Update(string ItemCode, string Qty, string Cpid, string GdId, string Colorid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_QTY]", "ITEM_CODE", ItemCode, "CP_ID", Cpid, "GODOWN_ID", GdId, "COLOUR_ID", Colorid) == true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET  ITEM_RES_QTY = CONVERT(BIGINT,ITEM_RES_QTY)+'{0}' WHERE ITEM_CODE = '{1}' and CP_ID = '{2}'and GODOWN_ID = '{3}' and COLOUR_ID = '{4}'", Qty, ItemCode, Cpid, GdId, Colorid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Reserve Stock Details", "89");

                    }
                }
                else
                {
                    _returnStringMessage = "Data Could Not be Reserved";
                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string CheckingFormatResqty_Update(string ItemCode, string Qty, string godownid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET  ITEM_RES_QTY=CONVERT(BIGINT,ITEM_RES_QTY)+'{0}' WHERE ITEM_CODE = '{1}' and GODOWN_ID = '{2}' ", Qty, ItemCode, godownid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Checking Format Qty Details", "88");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            //public string CheckingFormatIssueStock_Update(string ItemCode, string Qty,string Cpid)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET  ITEM_QTY_IN_HAND=CONVERT(BIGINT,ITEM_QTY_IN_HAND)+'{0}' WHERE ITEM_CODE = '{1}' and CP_ID = '{2}'", Qty, ItemCode,Cpid);
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    _returnStringMessage = string.Empty;
            //    if (_returnIntValue < 0 || _returnIntValue == 0)
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }
            //    else if (_returnIntValue > 0)
            //    {
            //        _returnStringMessage = "Data Updated Successfully";
            //    }
            //    return _returnStringMessage;
            //}

            //public string CheckingFormatIssueStock_Update(string ItemCode, string Qty)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET  ITEM_QTY_IN_HAND=CONVERT(BIGINT,ITEM_QTY_IN_HAND)+'{0}' WHERE ITEM_CODE='{1}'", Qty, ItemCode);
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    _returnStringMessage = string.Empty;
            //    if (_returnIntValue < 0 || _returnIntValue == 0)
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }
            //    else if (_returnIntValue > 0)
            //    {
            //        _returnStringMessage = "Data Updated Successfully";
            //    }
            //    return _returnStringMessage;
            //}
            public string CheckingFormatApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_CHECKING_FORMAT] SET CHK_APPROVED_BY={0} WHERE CHK_ID='{1}'", this.CHKApprovedBy, this.CHKId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Checking Format Approve Details", "88");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int ChekingFormatDetails_Delete(string CHKID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_CHECKING_FORMAT_DETAILS] WHERE CHK_ID={0}", CHKID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }
            public string ChkDetId;
            public string ChekingFormatDetails_Save()
            {
                this.ChkDetId = AutoGenMaxId("[YANTRA_CHECKING_FORMAT_DETAILS]", "CHK_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CHECKING_FORMAT_DETAILS] SELECT ISNULL(MAX(CHK_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}',{7},'{8}','{9}',{10} FROM [YANTRA_CHECKING_FORMAT_DETAILS]",
                                                                                              this.CHKId, this.CHKDETITEMCODE, this.CHKDETORDEREDQTY, this.CHKDETRECEIVEDQTY, this.CHKDETACCEPTEDQTY, this.CHKDETREJECTEDQTY, this.CHKDETRATE, this.CHKDETGODOWNID, this.CHKDETNETQTY, this.CHKDETREMARKS, this.CHKDETCOLOR);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Checking Format Details", "88");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public void ChekingFormatDetails_Select(string CHKID, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CHECKING_FORMAT_DETAILS],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],YANTRA_LKUP_GODOWN,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_CHECKING_FORMAT_DETAILS].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND YANTRA_LKUP_GODOWN.GODOWN_ID = YANTRA_CHECKING_FORMAT_DETAILS.CHK_DET_GODOWNID AND  [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_CHECKING_FORMAT_DETAILS.CHK_DET_COLOR   AND [YANTRA_CHECKING_FORMAT_DETAILS].CHK_ID=" + CHKID + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("OQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("RQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("AQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ReQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("NetQty");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Godown");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Godownid");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Color");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Colorid");
                PurchaseInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["OQuantity"] = dbManager.DataReader["CHK_DET_ORDEREDQTY"].ToString();
                    dr["RQuantity"] = dbManager.DataReader["CHK_DET_RECEIVEDQTY"].ToString();
                    dr["AQuantity"] = dbManager.DataReader["CHK_DET_ACCEPTEDQTY"].ToString();
                    //dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["ReQuantity"] = dbManager.DataReader["CHK_DET_REJECTEDQTY"].ToString();
                    dr["NetQty"] = dbManager.DataReader["CHK_DET_NETQTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["CHK_DET_RATE"].ToString();

                    dr["Godown"] = dbManager.DataReader["GODOWN_NAME"].ToString();
                    dr["Godownid"] = dbManager.DataReader["CHK_DET_GODOWNID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["CHK_DET_REMARKS"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Colorid"] = dbManager.DataReader["CHK_DET_COLOR"].ToString();
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public void ChekingFormatDetailsscm_Select(string CHKID, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CHECKING_FORMAT_DETAILS],[YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],warehouse_tbl,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_CHECKING_FORMAT_DETAILS].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND warehouse_tbl.wh_id = YANTRA_CHECKING_FORMAT_DETAILS.CHK_DET_GODOWNID AND  [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_CHECKING_FORMAT_DETAILS.CHK_DET_COLOR   AND [YANTRA_CHECKING_FORMAT_DETAILS].CHK_ID=" + CHKID + " order by YANTRA_CHECKING_FORMAT_DETAILS.CHK_DET_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("OQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("RQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("AQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ReQuantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("NetQty");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Godown");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Godownid");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Color");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Colorid");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Chkid");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("CHKDET_ID");
                PurchaseInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["OQuantity"] = dbManager.DataReader["CHK_DET_ORDEREDQTY"].ToString();
                    dr["RQuantity"] = dbManager.DataReader["CHK_DET_RECEIVEDQTY"].ToString();
                    dr["AQuantity"] = dbManager.DataReader["CHK_DET_ACCEPTEDQTY"].ToString();
                    //dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    dr["ReQuantity"] = dbManager.DataReader["CHK_DET_REJECTEDQTY"].ToString();
                    dr["NetQty"] = dbManager.DataReader["CHK_DET_NETQTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["CHK_DET_RATE"].ToString();

                    dr["Godown"] = dbManager.DataReader["whname"].ToString();
                    dr["Godownid"] = dbManager.DataReader["CHK_DET_GODOWNID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["CHK_DET_REMARKS"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Colorid"] = dbManager.DataReader["CHK_DET_COLOR"].ToString();
                    dr["Chkid"] = dbManager.DataReader["CHK_DET_ID"].ToString();
                    dr["CHKDET_ID"] = dbManager.DataReader["CHK_ID"].ToString();

                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
               // dbManager.Close();
            }
        }

        //Methods for Supplier Work Order Details
        public class SupplierWorkOrder
        {
            public string SupWoId, SupWoNo, SupWoNoPrefix, SupWoNoMiddle, SupWoNoSufix, SupWoDate, SupWoIndigenousForeign, SupWoSupplierName, SupWoSupplierContactPerson, SupWoSupplierAddress, SupWoSupplierPhone, SupWoSupplierMobile, SupWoSupplierEmail, SupWoSupplierFaxNo, SupWoStatus, SupWoNetAmount, SupWoAmtInWords, SupWoTermsConditions, DespmId, SupWoPaymentTerms, CurrencyId, SupWoNetAmountInOtherCurrency, SupWoDestination, SupWoInsurance, SupWoFreight, SupWoTermsOfDelivery, SupWoDiscount, SupWoTaxCST, SupWoContactPerson, SupWoReference, PreparedBy, ApprovedBy;
            public string SupWoDetId, ItemCode, SupWoDetRate, SupWoDetQty, SupWoDetDeliveryDate, SupWoDetSpec, SupWoDetRemarks, SupWoDetTax;

            public SupplierWorkOrder()
            {
            }

            public static string Work_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                //string _codePrefix = "IND/";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SUP_WO_NO,LEFT(SUP_WO_NO,5),''))),0)+1  FROM [YANTRA_SUP_WO_MAST]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IND_NO,'" + _codePrefix + "',''))),0)+1 FROM [YANTRA_INDENT_MAST]").ToString());
               // dbManager.Close();
                return _codePrefix + _returnIntValue;
            }

            public static string SupplierWorkOrder_AutoGenCode()
            {
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(SUP_WO_NO_MIDDLE),0)+1 FROM [YANTRA_SUP_WO_MAST]").ToString());
                //return _returnIntValue;
                //return CurrentFinancialYear() + " " + AutoGenMaxId("[YANTRA_SUP_WO_MAST]", "SUP_WO_NO_MIDDLE");
                return SM.AutoGenMaxNo("YANTRA_SUP_WO_MAST", "SUP_WO_NO_PREFIX");
            }

            public string SupplierWorkOrder_Save()
            {
                this.SupWoNo = Work_AutoGenCode();
                this.SupWoNoPrefix = CurrentFinancialYear();
                this.SupWoNoMiddle = AutoGenMaxId("[YANTRA_SUP_WO_MAST]", "SUP_WO_NO_MIDDLE");
                this.SupWoId = AutoGenMaxId("[YANTRA_SUP_WO_MAST]", "SUP_WO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_WO_MAST] VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17},'{18}',{19},'{20}','{21}','{22}','{23}','{24}','{25}','{26}',{27},'{28}',{29},{30},'{31}') ",
                    this.SupWoId, this.SupWoNoPrefix, this.SupWoNoMiddle, null, this.SupWoDate, this.SupWoIndigenousForeign, this.SupWoSupplierName, this.SupWoSupplierContactPerson, this.SupWoSupplierAddress, this.SupWoSupplierPhone, this.SupWoSupplierMobile, this.SupWoSupplierEmail, this.SupWoSupplierFaxNo, this.SupWoStatus, this.SupWoNetAmount, this.SupWoAmtInWords, this.SupWoTermsConditions, this.DespmId, this.SupWoPaymentTerms, this.CurrencyId, this.SupWoNetAmountInOtherCurrency, this.SupWoDestination, this.SupWoInsurance, this.SupWoFreight, this.SupWoDiscount, this.SupWoTaxCST, this.SupWoTermsOfDelivery, this.SupWoContactPerson, this.SupWoReference, this.PreparedBy, this.ApprovedBy, this.SupWoNo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Work Order Details", "90");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SupplierWorkOrder_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SUP_WO_MAST] SET SUP_WO_DATE='{0}',INDIGENOUS_FOREIGN='{1}', SUP_NAME='{2}', SUP_CONTACT_PERSON='{3}', SUP_ADDRESS='{4}', SUP_PHONE='{5}',SUP_MOBILE='{6}',SUP_EMAIL='{7}',SUP_FAXNO='{8}',SUP_WO_STATUS='{9}',SUP_WO_NET_AMOUNT='{10}',SUP_WO_AMT_WORDS='{11}',SUP_WO_TERMS_COND='{12}',DESPM_ID={13},SUP_WO_PAYMENT_TERMS='{14}',CURRENCY_ID={15},SUP_WO_NET_AMOUNT_IN_OTHER_CURRENCY='{16}',SUP_WO_DESTINATION='{17}', SUP_WO_INSURANCE='{18}', SUP_WO_FREIGHT='{19}', SUP_WO_DISCOUNT='{20}', SUP_WO_TAXCST='{21}',SUP_WO_TERMS_OF_DELIVERY='{22}',  SUP_WO_CONTACTPERSON='{23}',SUP_WO_REFERENCE='{24}',PREPAREDBY='{25}'  WHERE SUP_WO_ID={26}", this.SupWoDate, this.SupWoIndigenousForeign, this.SupWoSupplierName, this.SupWoSupplierContactPerson, this.SupWoSupplierAddress, this.SupWoSupplierPhone, this.SupWoSupplierMobile, this.SupWoSupplierEmail, this.SupWoSupplierFaxNo, this.SupWoStatus, this.SupWoNetAmount, this.SupWoAmtInWords, this.SupWoTermsConditions, this.DespmId, this.SupWoPaymentTerms, this.CurrencyId, this.SupWoNetAmountInOtherCurrency, this.SupWoDestination, this.SupWoInsurance, this.SupWoFreight, this.SupWoDiscount, this.SupWoTaxCST, this.SupWoTermsOfDelivery, this.SupWoContactPerson, this.SupWoReference, this.PreparedBy, this.SupWoId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Supplier Work Order Details", "90");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SupplierWorkOrderApprove_Update(string ApprovedBy, string SupWoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SUP_WO_MAST] SET APPROVEDBY='{0}'   WHERE SUP_WO_ID='{1}'", ApprovedBy, SupWoId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Approved Successfully";
                    log.add_Update("Supplier Work Order Approve Details", "90");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SupplierWorkOrderStatus_Update(string SupWoStatus, string SupWoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SUP_WO_MAST] SET SUP_WO_STATUS='{0}'   WHERE SUP_WO_ID='{1}'", SupWoStatus, SupWoId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Supplier Work Order Status Details", "90");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SupplierWorkOrder_Delete(string SupWoId)
            {
                if (DeleteRecord("[YANTRA_SUP_WO_MAST]", "SUP_WO_ID", SupWoId) == true)
                {
                    if (DeleteRecord("[YANTRA_SUP_WO_MAST]", "SUP_WO_ID", SupWoId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Supplier Work Details", "90");

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

            public static void SupplierWorkOrder_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_WO_MAST] ORDER BY SUP_WO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownList ddl = ControlForBind as DropDownList;
                    ddl.Items.Clear();
                    ddl.Items.Add(new ListItem("--", "0"));
                    while (dbManager.DataReader.Read())
                    {
                        ddl.Items.Add(new ListItem(dbManager.DataReader["SUP_WO_NO_PREFIX"].ToString() + " " + dbManager.DataReader["SUP_WO_NO_MIDDLE"].ToString(), dbManager.DataReader["SUP_WO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
               // dbManager.Close();
            }

            public int SupplierWorkOrder_Select(string SupWoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_WO_MAST]  WHERE SUP_WO_ID='" + SupWoId + "' ORDER BY SUP_WO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SupWoId = dbManager.DataReader["SUP_WO_ID"].ToString();
                    this.SupWoNoPrefix = dbManager.DataReader["SUP_WO_NO_PREFIX"].ToString();
                    this.SupWoNoMiddle = dbManager.DataReader["SUP_WO_NO_MIDDLE"].ToString();
                    this.SupWoNoSufix = dbManager.DataReader["SUP_WO_NO_SUFIX"].ToString();
                    this.SupWoNo = this.SupWoNoPrefix + " " + this.SupWoNoMiddle;
                    this.SupWoDate = Convert.ToDateTime(dbManager.DataReader["SUP_WO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SupWoIndigenousForeign = dbManager.DataReader["INDIGENOUS_FOREIGN"].ToString();
                    this.SupWoSupplierName = dbManager.DataReader["SUP_NAME"].ToString();
                    this.SupWoSupplierContactPerson = dbManager.DataReader["SUP_CONTACT_PERSON"].ToString();
                    this.SupWoSupplierAddress = dbManager.DataReader["SUP_ADDRESS"].ToString();
                    this.SupWoSupplierPhone = dbManager.DataReader["SUP_PHONE"].ToString();
                    this.SupWoSupplierMobile = dbManager.DataReader["SUP_MOBILE"].ToString();
                    this.SupWoSupplierEmail = dbManager.DataReader["SUP_EMAIL"].ToString();
                    this.SupWoSupplierFaxNo = dbManager.DataReader["SUP_FAXNO"].ToString();
                    this.SupWoStatus = dbManager.DataReader["SUP_WO_STATUS"].ToString();
                    this.SupWoNetAmount = dbManager.DataReader["SUP_WO_NET_AMOUNT"].ToString();
                    this.SupWoAmtInWords = dbManager.DataReader["SUP_WO_AMT_WORDS"].ToString();
                    this.SupWoTermsConditions = dbManager.DataReader["SUP_WO_TERMS_COND"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SupWoPaymentTerms = dbManager.DataReader["SUP_WO_PAYMENT_TERMS"].ToString();
                    this.CurrencyId = dbManager.DataReader["CURRENCY_ID"].ToString();
                    this.SupWoNetAmountInOtherCurrency = dbManager.DataReader["SUP_WO_NET_AMOUNT_IN_OTHER_CURRENCY"].ToString();
                    this.SupWoDestination = dbManager.DataReader["SUP_WO_DESTINATION"].ToString();
                    this.SupWoInsurance = dbManager.DataReader["SUP_WO_INSURANCE"].ToString();
                    this.SupWoFreight = dbManager.DataReader["SUP_WO_FREIGHT"].ToString();
                    this.SupWoTermsOfDelivery = dbManager.DataReader["SUP_WO_TERMS_OF_DELIVERY"].ToString();
                    this.SupWoDiscount = dbManager.DataReader["SUP_WO_DISCOUNT"].ToString();
                    this.SupWoTaxCST = dbManager.DataReader["SUP_WO_TAXCST"].ToString();
                    this.SupWoContactPerson = dbManager.DataReader["SUP_WO_CONTACTPERSON"].ToString();
                    this.SupWoReference = dbManager.DataReader["SUP_WO_REFERENCE"].ToString();
                    this.PreparedBy = dbManager.DataReader["PREPAREDBY"].ToString();
                    this.ApprovedBy = dbManager.DataReader["APPROVEDBY"].ToString();
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

            public string SupplierWorkOrderDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SUP_WO_DET] SELECT ISNULL(MAX(SUP_WO_DET_ID),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}' FROM [YANTRA_SUP_WO_DET]", this.SupWoId, this.ItemCode, this.SupWoDetRate, this.SupWoDetQty, this.SupWoDetDeliveryDate, this.SupWoDetSpec, this.SupWoDetRemarks, this.SupWoDetTax);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Supplier Work Order Details", "90");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int SupplierWorkOrderDetails_Delete(string SupWoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SUP_WO_DET] WHERE SUP_WO_ID={0}", SupWoId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public void SupplierWorkOrderDetails_Select(string SupWoId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SUP_WO_DET],[YANTRA_SUP_WO_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SUP_WO_DET].SUP_WO_ID=[YANTRA_SUP_WO_MAST].SUP_WO_ID AND [YANTRA_SUP_WO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SUP_WO_DET].SUP_WO_ID=" + SupWoId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersFixedPOItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Rate"] = dbManager.DataReader["SUP_WO_DET_RATE"].ToString();
                    dr["Tax"] = dbManager.DataReader["SUP_WO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SUP_WO_DET_QTY"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SUP_WO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["Specifications"] = dbManager.DataReader["SUP_WO_DET_SPECIFICATION"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SUP_WO_DET_REMARKS"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();

                    SuppliersFixedPOItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
               // dbManager.Close();
            }
        }

        //Methods For Purchase Return Form
        public class PurchaseReturn
        {
            public String PRId, PRNo, PRDate, FPOId, FPONo, CUSTId, SUPId, PRPackChrgs, PRInsuranceChrgs, PRTransChrgs, PRMiscChrgs, PRDiscount, PRGrossAmt, PRPreparedBy, PRApprovedBy, PRRemarks, PRAmount, CpId;
            public string PRDetId, PRItemCode, PRDetQty, PRDetRate, PRDetVat, PRDetCst, PRDetAmount, PRDetExcise;

            public PurchaseReturn()
            {
            }

            public static string PurchaseReturn_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(PI_NO,LEFT(PI_NO,5),''))),0)+1 FROM [YANTRA_PURCHASE_INVOICE_MAST]").ToString());

                //// _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(PI_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_PURCHASE_INVOICE_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_PURCHASE_RETURN_MAST", "PR_NO");
            }

            public string PurchaseReturn_Save()
            {
                this.PRNo = PurchaseReturn_AutoGenCode();
                this.PRId = AutoGenMaxId("[YANTRA_PURCHASE_RETURN_MAST]", "PR_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_RETURN_MAST] SELECT ISNULL(MAX(PR_ID),0)+1,'{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13} FROM [YANTRA_PURCHASE_RETURN_MAST]", this.PRNo, this.PRDate, this.FPOId, this.SUPId, this.PRPackChrgs, this.PRInsuranceChrgs, this.PRTransChrgs, this.PRMiscChrgs, this.PRDiscount, this.PRGrossAmt, this.PRPreparedBy, this.PRApprovedBy, this.PRRemarks, this.CpId);
                //    _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_RETURN_MAST] SELECT ISNULL(MAX(PR_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12} FROM [YANTRA_PURCHASE_RETURN_MAST]", this.PRNo, Convert.ToDateTime(this.PRDate), this.DCId, this.SOId, this.PRMissChrgs, this.PRDiscount, this.PRGrossAmt, this.PRRemarks, this.PRPreparedBy, this.PRApprovedBy, this.PRVAT, this.PRCSTax, this.CPid );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Purchase Return Details", "91");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string PurchaseReturn_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PURCHASE_RETURN_MAST] SET PR_DATE='{0}',SUP_ID={1},PI_PACK_CHRGS='{2}',PI_INSURANCE_CHRGS='{3}',PI_TRANS_CHRGS='{4}',PI_MISC_CHRGS='{5}',PI_DISCOUNT='{6}',PI_GROSS_AMT='{7}',PI_PREPARED_BY='{8}',PI_APPROVED_BY='{9}',PI_REMARKS='{10}',CP_ID = '{11}' WHERE PR_ID='{12}'", this.PRDate, this.SUPId, this.PRPackChrgs, this.PRInsuranceChrgs, this.PRTransChrgs, this.PRMiscChrgs, this.PRDiscount, this.PRGrossAmt, this.PRPreparedBy, this.PRApprovedBy, this.PRRemarks, this.CpId, this.PRId);
                // _commandText = string.Format("UPDATE [YANTRA_PURCHASE_RETURN_MAST] SET PR_DATE='{0}',SI_MISS_CHRGS='{1}',SI_DISCOUNT='{2}',SI_GROSS_AMT='{3}',SI_REMARKS='{4}',SI_PREPARED_BY='{5}',SI_APPROVED_BY='{6}',SI_VAT='{7}',SI_CSTAX='{8}',CP_ID = {9} WHERE PR_ID={10}", this.PRDate, this.PRMissChrgs, this.PRDiscount, this.PRGrossAmt, this.PRRemarks, this.PRPreparedBy, this.PRApprovedBy, this.PRVAT, this.PRCSTax, this.CPid, this.PRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Purchase Return Details", "91");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string PurchaseReturn_Delete(string PurchaseReturnId)
            {
                if (DeleteRecord("[YANTRA_PURCHASE_RETURN_DET]", "PR_ID", PurchaseReturnId) == true)
                {
                    if (DeleteRecord("[YANTRA_PURCHASE_RETURN_MAST]", "PR_ID", PurchaseReturnId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Purchase Return Details", "91");

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

            public string PurchaseReturnDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_RETURN_DET] SELECT ISNULL(MAX(PR_DET_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' FROM [YANTRA_PURCHASE_RETURN_DET]", this.PRId, this.PRItemCode, this.PRDetQty, this.PRDetRate, this.PRDetVat, this.PRDetCst, this.PRDetAmount, this.PRDetExcise);
                //   _commandText = string.Format("INSERT INTO [YANTRA_PURCHASE_RETURN_DET] SELECT ISNULL(MAX(PR_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_PURCHASE_RETURN_DET]", this.PRId, this.ItemCode, this.PRDetQty, this.PRDetRate, this.PRDetVat, this.PRDetCst, this.PRDetExcise);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Purchase Return Details", "91");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int PurchaseReturnDetails_Delete(string PurchaseReturnId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_PURCHASE_RETURN_DET] WHERE PR_ID={0}", PurchaseReturnId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public int PurchaseReturn_Select(string PurchaseReturnId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_RETURN_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_SUPPLIER_MAST]  WHERE  [YANTRA_PURCHASE_RETURN_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_PURCHASE_RETURN_MAST].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_PURCHASE_RETURN_MAST].PR_ID='" + PurchaseReturnId + "' ORDER BY [YANTRA_PURCHASE_RETURN_MAST].PR_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PRNo = dbManager.DataReader["PR_NO"].ToString();
                    this.PRDate = Convert.ToDateTime(dbManager.DataReader["PR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    this.FPONo = dbManager.DataReader["FPO_NO"].ToString();
                    this.SUPId = dbManager.DataReader["SUP_ID"].ToString();
                    this.PRPackChrgs = dbManager.DataReader["PI_PACK_CHRGS"].ToString();
                    this.PRInsuranceChrgs = dbManager.DataReader["PI_INSURANCE_CHRGS"].ToString();
                    this.PRTransChrgs = dbManager.DataReader["PI_TRANS_CHRGS"].ToString();
                    this.PRMiscChrgs = dbManager.DataReader["PI_MISC_CHRGS"].ToString();
                    this.PRDiscount = dbManager.DataReader["PI_DISCOUNT"].ToString();
                    this.PRGrossAmt = dbManager.DataReader["PI_GROSS_AMT"].ToString();
                    this.PRPreparedBy = dbManager.DataReader["PI_PREPARED_BY"].ToString();
                    this.PRApprovedBy = dbManager.DataReader["PI_APPROVED_BY"].ToString();
                    this.PRRemarks = dbManager.DataReader["PI_REMARKS"].ToString();
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

            public void PurchaseReturnDetails_Select(string PurchaseReturnId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_RETURN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_PURCHASE_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_PURCHASE_RETURN_DET].PR_ID=" + PurchaseReturnId + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("VAT");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Cst");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Excise");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                PurchaseInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PR_DET_QTY"].ToString();
                    // dr["BalanceQty"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Rate"] = dbManager.DataReader["PR_DET_RATE"].ToString();

                    dr["VAT"] = dbManager.DataReader["PR_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["PR_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["PR_DET_EXCISE"].ToString();
                    dr["Amount"] = dbManager.DataReader["PR_DET_AMOUNT"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    PurchaseInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
               // dbManager.Close();
            }

            public string PurchaseReturnApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PURCHASE_RETURN_MAST] SET PI_APPROVED_BY={0} WHERE PR_ID='{1}'", this.PRApprovedBy, this.PRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Purchase Return Approve Details", "91");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public static void PurchaseReturn_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_RETURN_MAST] ORDER BY PR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PR_NO", "PR_ID");
                }
               // dbManager.Close();
            }

            public static void PurchaseInvoice_Select(Control ControlForBind, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_PURCHASE_INVOICE_MAST] WHERE FPO_ID=" + FPOId + " ORDER BY PI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PI_CUST_INV_NO", "PI_ID");
                }
               // dbManager.Close();
            }
        }

        //Methods for Shipment Details

        public class ShipmentDetails
        {
            public string sdid, sdno, sddate, fpoid, supid, forwardingthroghu, shipmentdetails, insurance, packingcharges, dutyexcise, customclearance, datearrival, materialreceiptdate, dateofshipping, cpid;
            public string sddetid, fudesc, fudate, funame;
            public string SDADDRESS, SDCONTACTPERSON, SDPHONENO, SDEMAIL, SDREMAMOUNT, SDREMDATE, SDVOLUME, SDWEIGHT, SDCONTAINER;
            public string Invoiceno, InvoiceDate, InvoiceValue, Forwoarderid, inscompnayname, insdate, insamount;
            public string ItemCode, Quantity, Rate, Discount, Amount, ItemTypeId, Customer, BrandId, PIId, PIDetId, Brand, PINo;
            public ShipmentDetails()
            {
            }

            public static string Shipmentdetails_AutoGenCode()
            {
                return SM.AutoGenMaxNo("YANTRA_SHIPPING_DETAILS_MASTER", "SD_NO");
            }
            public string SiId;
            public string ShipmentDetails_Save()
            {
                this.sdno = Shipmentdetails_AutoGenCode();
                this.sdid = AutoGenMaxId("YANTRA_SHIPPING_DETAILS_MASTER", "SD_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Insert into YANTRA_SHIPPING_DETAILS_MASTER values({0},'{1}','{2}',{3},{4},'{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}',{27},{28},'{29}','{30}',{31})", this.sdid, this.sdno, this.sddate, this.fpoid, this.supid, this.forwardingthroghu, this.shipmentdetails, this.insurance, this.packingcharges, this.dutyexcise, this.customclearance, this.datearrival, this.materialreceiptdate, this.dateofshipping, this.cpid, this.SDREMAMOUNT, this.SDREMDATE, this.SDCONTAINER, this.SDVOLUME, this.SDWEIGHT, this.SDADDRESS, this.SDCONTACTPERSON, this.SDPHONENO, this.SDEMAIL, this.Invoiceno, this.InvoiceDate, this.InvoiceValue, this.Forwoarderid, this.inscompnayname, this.insdate, this.insamount,this.SiId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Shipment Details", "92");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SHIPPING_DETAILS] SELECT ISNULL(MAX(SD_DET_ID),0)+1,'{0}','{1}','{2}',{3},{4},'{5}',{6},'{7}',{8},'{9}','{10}','{11}' FROM [YANTRA_SHIPPING_DETAILS]", this.sdid, this.ItemCode , this.Quantity , this.Rate , this.Discount, this.Customer , this.Amount, this.Brand  , this.BrandId , this.PINo ,this.PIId ,this .PIDetId );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Shipment Details", "85");

                }
                // dbManager.Close();
                return _returnStringMessage;
            }
            public String ShipmentDetails_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SHIPPING_DETAILS_MASTER] SET SD_DATE='{0}',FPO_ID={1},SUP_ID={2},FORWARDING_THROGHU='{3}',SHIPMENT_DETAILS='{4}',INSURANCE='{5}',PACKING_CHARGES='{6}',DUTY_EXCISE='{7}',CUSTOM_CLEARANCE='{8}',DATE_ARRIVAL='{9}',MATERIAL_RECEIPT_DATE='{10}',DATE_OF_SHIPPING = '{11}',CP_ID = {12},REMITTENCE_AMOUNT = '{13}',REMITTENCE_DATE = '{14}',CONTAINER = '{15}',VOLUME = '{16}',WEIGHT = '{17}',SUP_ADDRESS = '{18}',SUP_CONTACTPERSON = '{19}',SUP_PHONE = '{20}',SUP_EMAIL = '{21}',INVOICE_NO='{22}',INVOICE_DATE = '{23}',INVOICE_VALUE = '{24}',FORWARDER_ID = {25},INSURANCE_COMPANY = {26},INSURANCE_DATE = '{27}',INSURANCE_AMOUNT = '{28}',SI_ID={30}   WHERE SD_ID= {29}", this.sddate, this.fpoid, this.supid, this.forwardingthroghu, this.shipmentdetails, this.insurance, this.packingcharges, this.dutyexcise, this.customclearance, this.datearrival, this.materialreceiptdate, this.dateofshipping, this.cpid, this.SDREMAMOUNT, this.SDREMDATE, this.SDCONTAINER, this.SDVOLUME, this.SDWEIGHT, this.SDADDRESS, this.SDCONTACTPERSON, this.SDPHONENO, this.SDEMAIL, this.Invoiceno, this.InvoiceDate, this.InvoiceValue, this.Forwoarderid, this.inscompnayname, this.insdate, this.insamount, this.sdid,this.SiId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Shipment Details", "92");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int SDetails_Delete(string ShipingdetailsId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SHIPPING_DETAILS] WHERE SD_ID={0}", ShipingdetailsId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                // dbManager.Close();
                return _returnIntValue;
            }
            public string ShipmentDetails_Delete(string ShipingdetailsId)
            {
                if (DeleteRecord("[YANTRA_SHIPPING_DETAILS_MASTER]", "SD_ID", ShipingdetailsId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Shipment Details", "92");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public int ShipmentDetails_Select(string ShipingdetailsId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SHIPPING_DETAILS_MASTER],[YANTRA_FIXED_PO_MAST],[YANTRA_SUPPLIER_MAST]  WHERE  [YANTRA_SHIPPING_DETAILS_MASTER].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_SHIPPING_DETAILS_MASTER].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_SHIPPING_DETAILS_MASTER].SD_ID='" + ShipingdetailsId + "' ORDER BY [YANTRA_SHIPPING_DETAILS_MASTER].SD_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.sdno = dbManager.DataReader["SD_NO"].ToString();
                    this.sddate = Convert.ToDateTime(dbManager.DataReader["SD_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.fpoid = dbManager.DataReader["FPO_ID"].ToString();
                    this.SiId = dbManager.DataReader["SI_ID"].ToString();

                    this.supid = dbManager.DataReader["SUP_ID"].ToString();
                    this.forwardingthroghu = dbManager.DataReader["FORWARDING_THROGHU"].ToString();
                    this.shipmentdetails = dbManager.DataReader["SHIPMENT_DETAILS"].ToString();
                    this.insurance = dbManager.DataReader["INSURANCE"].ToString();
                    this.packingcharges = dbManager.DataReader["PACKING_CHARGES"].ToString();
                    this.dutyexcise = dbManager.DataReader["DUTY_EXCISE"].ToString();
                    this.customclearance = dbManager.DataReader["CUSTOM_CLEARANCE"].ToString();
                    this.datearrival = Convert.ToDateTime(dbManager.DataReader["DATE_ARRIVAL"].ToString()).ToString("dd/MM/yyyy");
                    this.materialreceiptdate = Convert.ToDateTime(dbManager.DataReader["MATERIAL_RECEIPT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.dateofshipping = Convert.ToDateTime(dbManager.DataReader["DATE_OF_SHIPPING"].ToString()).ToString("dd/MM/yyyy");
                    this.SDADDRESS = dbManager.DataReader["SUP_ADDRESS"].ToString();
                    this.SDCONTACTPERSON = dbManager.DataReader["SUP_CONTACTPERSON"].ToString();
                    this.SDPHONENO = dbManager.DataReader["SUP_PHONE"].ToString();
                    this.SDEMAIL = dbManager.DataReader["SUP_EMAIL"].ToString();
                    this.SDREMAMOUNT = dbManager.DataReader["REMITTENCE_AMOUNT"].ToString();
                    this.SDREMDATE = Convert.ToDateTime(dbManager.DataReader["REMITTENCE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SDCONTAINER = dbManager.DataReader["CONTAINER"].ToString();
                    this.SDWEIGHT = dbManager.DataReader["WEIGHT"].ToString();
                    this.SDVOLUME = dbManager.DataReader["VOLUME"].ToString();
                    this.Invoiceno = dbManager.DataReader["INVOICE_NO"].ToString();
                    this.InvoiceDate = Convert.ToDateTime(dbManager.DataReader["INVOICE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.InvoiceValue = dbManager.DataReader["INVOICE_VALUE"].ToString();
                    this.Forwoarderid = dbManager.DataReader["FORWARDER_ID"].ToString();
                    this.insdate = Convert.ToDateTime(dbManager.DataReader["INSURANCE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.inscompnayname = dbManager.DataReader["INSURANCE_COMPANY"].ToString();
                    this.insamount = dbManager.DataReader["INSURANCE_AMOUNT"].ToString();
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

            public int PurchaseInvoiceDetails_Delete1(string SDDetID)
            {
                if (dbManager.DataReader == null)
                    dbManager.Open();
                _commandText = string.Format("Delete from [YANTRA_SHIPPING_DETAILs] where SD_DET_ID={0}", SDDetID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void PurchaseInvoiceDetails1_Select(string SDID, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_SHIPPING_DETAILs,[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE YANTRA_SHIPPING_DETAILs.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_SHIPPING_DETAILs.sd_ID=" + SDID + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable PurchaseInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Discount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Amount");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SugParty");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Brand");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Brand_ID");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PINo");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PIID");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PI_DET_ID");
                PurchaseInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SD_DET_ID");
                PurchaseInvoiceProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = PurchaseInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SD_DET_QTY"].ToString();
                    // dr["BalanceQty"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Rate"] = dbManager.DataReader["SD_DET_RATE"].ToString();
                    dr["Discount"] = dbManager.DataReader["SD_DET_DISC"].ToString();
                    dr["Amount"] = dbManager.DataReader["SD_DET_AMOUNT"].ToString();
                    dr["SugParty"] = dbManager.DataReader["SD_DET_Customer"].ToString();
                    dr["PINO"] = dbManager.DataReader["PI_NO"].ToString();
                    dr["PI_DET_ID"] = dbManager.DataReader["PI_DET_ID"].ToString();
                    dr["PIID"] = dbManager.DataReader["PI_ID"].ToString();
                    dr["BRAND"] = dbManager.DataReader["BRAND"].ToString();
                    dr["BRAND_Id"] = dbManager.DataReader["BRAND_ID"].ToString();
                    dr["SD_DET_ID"] = dbManager.DataReader["SD_DET_ID"].ToString();

                    PurchaseInvoiceProducts.Rows.Add(dr);

                }
                dbManager.DataReader.Close();
                gv.DataSource = PurchaseInvoiceProducts;
                gv.DataBind();
            }

            public string ShipmentFollowups_Save()
            {
                this.sddetid = AutoGenMaxId("[YANTRA_SHIPPING_DETAILS_FOLLOWUP]", "SHIPPING_DETAILS_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_SHIPPING_DETAILS_FOLLOWUP] VALUES({0},{1},'{2}','{3}',{4})", this.sddetid, this.sdid, this.fudesc, this.fudate, this.funame);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Shipment Followup Details", "92");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public static void PISelectByBrand(string BrandId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select distinct PI_CUST_INV_NO ,piDet.PI_ID from YANTRA_PURCHASE_INVOICE_DET  PIDet inner join YANTRA_PURCHASE_INVOICE_MAST PIMast on Pidet.PI_ID=PIMast .PI_ID inner join YANTRA_ITEM_MAST It on it.ITEM_CODE =pidet.ITEM_CODE where BRAND_ID =" + BrandId + "   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PI_CUST_INV_NO", "PI_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods for Self Purchase Order
        public class SupplierSelfPO
        {
            public string FPOSId, FPONo, FPODate, FPOSuppRef, FPOPOStatus, FPONetAmount, FPOAmtWords, FPOTermsConds, DespmId, SupId, FPOPaymentTerms, CurrencyId, FPONetAmtInOtherCurrency, FPODestination, FPOInsurance, FPOFreight, FPOTermsOfDelivery, FPODiscount, FPOTaxCST, FPOContactPerson, PreparedBy, ApprovedBy, FPOCIFCharges, FPOFOBCharges, FPOSuppContactPerson, FPOCurrencyType, FPOSuppDisc, FPOSuppSpPrice, CpId;
            public string FPODetId, ItemCode, FPODetRate, FPODetQty, FPODetDeliveryDate, FPODetSpec, FPODetRemarks, FPODetTax, Customer, Color;
            public string Remitance, DeliveryAddrees, InvoiceAddress, CustomerCode, ExpDateArrival, detStatus;
            public SupplierSelfPO()
            {
            }

            public static string SuppliersSelfPO_AutoGenCode()
            {
                return SM.AutoGenMaxNo("SELF_PO_MAST", "FPO_NO");
            }

            public string SuppliersSelfPO_Save()
            {
                this.FPONo = SuppliersSelfPO_AutoGenCode();
                this.FPOSId = AutoGenMaxId("[SELF_PO_MAST]", "FPOS_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [SELF_PO_MAST] VALUES({0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19},{20},'{21}','{22}','{23}','{24}',{25},{26},'{27}',{28},{29},'{30}') ", this.FPOSId, this.FPONo, this.FPODate, this.SupId, this.FPOSuppRef, this.FPOPOStatus, this.FPONetAmount, this.FPOAmtWords, this.FPOTermsConds, this.DespmId, this.FPOPaymentTerms, this.CurrencyId, this.FPONetAmtInOtherCurrency, this.FPODestination, this.FPOInsurance, this.FPOFreight, this.FPODiscount, this.FPOTaxCST, this.FPOTermsOfDelivery, this.FPOContactPerson, this.PreparedBy, this.ApprovedBy, this.FPOCIFCharges, this.FPOFOBCharges, this.FPOSuppContactPerson, this.FPOCurrencyType, this.CpId, this.Remitance, this.DeliveryAddrees, this.InvoiceAddress, this.CustomerCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Shipment SelfPO Details", "93");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersSelfPO_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [SELF_PO_MAST] SET FPO_DATE='{0}',FPO_SUPP_REF='{1}', FPO_PO_STATUS='{2}', FPO_NET_AMOUNT='{3}', FPO_AMT_WORDS='{4}', FPO_TERMS_COND='{5}',SUP_ID={6},FPO_PAYMENT_TERMS='{7}',FPO_CURRENCY_ID={8},FPO_NET_AMOUNT_IN_OTHER_CURRENCY='{9}',FPO_DESTINATION='{10}',FPO_INSURANCE='{11}',FPO_FREIGHT='{12}',FPO_DISCOUNT='{13}',FPO_TAXCST='{14}',FPO_TERMS_OF_DELIVERY='{15}',FPO_CONTACTPERSON='{16}',PREPAREDBY='{17}',FPO_CIF='{18}',FPO_FOB='{19}',FPO_SUPP_CONTACT_PERSON='{20}',FPO_CURRENCY_TYPE='{21}',CP_ID={23},REMITANCE = '{24}',DELIVERYADRESS = {25},InvoiceAddress = {26},CustoemerCod = '{27}'  WHERE FPOS_ID='{22}'", this.FPODate, this.FPOSuppRef, this.FPOPOStatus, this.FPONetAmount, this.FPOAmtWords, this.FPOTermsConds, this.SupId, this.FPOPaymentTerms, this.CurrencyId, this.FPONetAmtInOtherCurrency, this.FPODestination, this.FPOInsurance, this.FPOFreight, this.FPODiscount, this.FPOTaxCST, this.FPOTermsOfDelivery, this.FPOContactPerson, this.PreparedBy, this.FPOCIFCharges, this.FPOFOBCharges, this.FPOSuppContactPerson, this.FPOCurrencyType, this.FPOSId, this.CpId, this.Remitance, this.DeliveryAddrees, this.InvoiceAddress, this.CustomerCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Shipment SelfPO Details", "93");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersSelfPOApprove_Update(string ApprovedBy, string FPOSId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [SELF_PO_MAST] SET APPROVEDBY='{0}'   WHERE FPOS_ID='{1}'", ApprovedBy, FPOSId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Approved Successfully";
                    log.add_Update("Shipment SelfPO Approve Details", "93");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersSelfPOStatus_Update(string FPOStatus, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [SELF_PO_MAST] SET FPO_PO_STATUS='{0}'   WHERE FPOS_ID='{1}'", FPOStatus, FPOSId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Shipment SelfPO Status Details", "93");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersSelfPO_Delete(string SupFixedPOId)
            {
                if (DeleteRecord("[SELF_PO_MASTER_DET]", "FPOS_ID", SupFixedPOId) == true)
                {
                    if (DeleteRecord("[SELF_PO_MAST]", "FPOS_ID", SupFixedPOId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Shipment SelfPO Details", "93");

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

            public static void SuppliersSelfPO_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT FPO_NO,FPOS_ID FROM [SELF_PO_MAST] ORDER BY FPOS_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "FPO_NO", "FPOS_ID");
                }
               // dbManager.Close();
            }

            public int SuppliersSelfPO_Select(string SupFixedPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [SELF_PO_MAST] inner join [YANTRA_SUPPLIER_MAST] on [YANTRA_SUPPLIER_MAST].SUP_ID=[SELF_PO_MAST].SUP_ID  WHERE [SELF_PO_MAST].FPOS_ID='" + SupFixedPOId + "' ORDER BY [SELF_PO_MAST].FPOS_ID DESC ");
                //_commandText = string.Format("SELECT * FROM [YANTRA_SUP_QUOT_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_LKUP_DESP_MODE] WHERE [YANTRA_SUP_QUOT_MAST].SUP_QUOT_ID=[YANTRA_FIXED_PO_MAST].SUP_QUOT_ID AND " +
                //"[YANTRA_LKUP_DESP_MODE].DESPM_ID=[YANTRA_FIXED_PO_MAST].DESPM_ID AND [YANTRA_FIXED_PO_MAST].FPO_ID='" + SupFixedPOId + "' ORDER BY [YANTRA_FIXED_PO_MAST].FPO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.FPOSId = dbManager.DataReader["FPOS_ID"].ToString();
                    this.FPONo = dbManager.DataReader["FPO_NO"].ToString();
                    this.FPODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");

                    this.FPOSuppRef = dbManager.DataReader["FPO_SUPP_REF"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.FPOPOStatus = dbManager.DataReader["FPO_PO_STATUS"].ToString();
                    this.FPONetAmount = dbManager.DataReader["FPO_NET_AMOUNT"].ToString();
                    this.FPOAmtWords = dbManager.DataReader["FPO_AMT_WORDS"].ToString();
                    this.FPOTermsConds = dbManager.DataReader["FPO_TERMS_COND"].ToString();
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();

                    this.FPOPaymentTerms = dbManager.DataReader["FPO_PAYMENT_TERMS"].ToString();
                    this.CurrencyId = dbManager.DataReader["FPO_CURRENCY_ID"].ToString();
                    this.FPONetAmtInOtherCurrency = dbManager.DataReader["FPO_NET_AMOUNT_IN_OTHER_CURRENCY"].ToString();
                    this.FPODestination = dbManager.DataReader["FPO_DESTINATION"].ToString();
                    this.FPOInsurance = dbManager.DataReader["FPO_INSURANCE"].ToString();
                    this.FPOFreight = dbManager.DataReader["FPO_FREIGHT"].ToString();
                    this.FPODiscount = dbManager.DataReader["FPO_DISCOUNT"].ToString();
                    this.FPOTaxCST = dbManager.DataReader["FPO_TAXCST"].ToString();
                    this.FPOTermsOfDelivery = dbManager.DataReader["FPO_TERMS_OF_DELIVERY"].ToString();
                    this.FPOContactPerson = dbManager.DataReader["FPO_CONTACTPERSON"].ToString();
                    this.PreparedBy = dbManager.DataReader["PREPAREDBY"].ToString();
                    this.ApprovedBy = dbManager.DataReader["APPROVEDBY"].ToString();
                    this.FPOCIFCharges = dbManager.DataReader["FPO_CIF"].ToString();
                    this.FPOFOBCharges = dbManager.DataReader["FPO_FOB"].ToString();
                    this.FPOSuppContactPerson = dbManager.DataReader["FPO_SUPP_CONTACT_PERSON"].ToString();
                    this.FPOCurrencyType = dbManager.DataReader["FPO_CURRENCY_TYPE"].ToString();
                    this.Remitance = dbManager.DataReader["REMITANCE"].ToString();
                    this.DeliveryAddrees = dbManager.DataReader["DELIVERYADRESS"].ToString();
                    this.CpId = dbManager.DataReader["CP_ID"].ToString();
                    this.InvoiceAddress = dbManager.DataReader["InvoiceAddress"].ToString();
                    this.CustomerCode = dbManager.DataReader["CustoemerCod"].ToString();

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

            public string SuppliersSelfPODetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [SELF_PO_MASTER_DET] SELECT ISNULL(MAX(FPOS_DET_ID),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}' FROM [SELF_PO_MASTER_DET]", this.FPOSId, this.ItemCode, this.FPODetRate, this.FPODetQty, this.FPODetDeliveryDate, this.FPODetSpec, this.FPODetRemarks, this.FPODetTax, this.FPOSuppDisc, this.FPOSuppSpPrice, this.Customer, this.Color, this.detStatus, this.ExpDateArrival);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Shipment SelfPO Details", "93");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public string SuppliersSelfPODetStatus_Update(string FPOStatus, string ExpDate, string ArrivalDate, string FPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [SELF_PO_MASTER_DET] SET FPO_DET_STATUS='{0}',FPO_DET_DELIVERY_DATE = '{1}',FPO_DET_EXPDATE ='{2}'  WHERE FPOS_DET_ID ='{3}'", FPOStatus, ExpDate, ArrivalDate, FPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Shipment SelfPO Status Details", "93");

                }
               // dbManager.Close();
                return _returnStringMessage;
            }

            public int SuppliersSelfPODetails_Delete(string SupFixedPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [SELF_PO_MASTER_DET] WHERE FPOS_ID={0}", SupFixedPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
               // dbManager.Close();
                return _returnIntValue;
            }

            public void SuppliersSelfPODetails_Select(string SupFixedPOId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [SELF_PO_MASTER_DET],[SELF_PO_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [SELF_PO_MASTER_DET].FPOS_ID=[SELF_PO_MAST].FPOS_ID AND [SELF_PO_MASTER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [SELF_PO_MAST].FPOS_ID=" + SupFixedPOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SuppliersFixedPOItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Tax");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Specifications");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("SpPrice");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Disc");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Customer");
                SuppliersFixedPOItems.Columns.Add(col);
                col = new DataColumn("Color");
                SuppliersFixedPOItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SuppliersFixedPOItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Rate"] = dbManager.DataReader["FPO_DET_RATE"].ToString();
                    dr["Tax"] = dbManager.DataReader["FPO_DET_TAX"].ToString();
                    dr["Quantity"] = dbManager.DataReader["FPO_DET_QTY"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString()).ToString("dd//MM/yyyy");
                    dr["DeliveryDate"] = (Convert.ToDateTime(dbManager.DataReader["FPO_DET_DELIVERY_DATE"].ToString())).ToString("dd/MM/yyyy");
                    dr["Specifications"] = dbManager.DataReader["FPO_DET_SPECIFICATION"].ToString();
                    dr["Remarks"] = dbManager.DataReader["FPO_DET_REMARKS"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SpPrice"] = dbManager.DataReader["FPO_DET_SPPRICE"].ToString();
                    dr["Disc"] = dbManager.DataReader["FPO_DET_DISC"].ToString();
                    dr["Customer"] = dbManager.DataReader["FPO_DET_CUSTOMER"].ToString();
                    dr["Color"] = dbManager.DataReader["FPO_DET_COLOR"].ToString();
                    SuppliersFixedPOItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SuppliersFixedPOItems;
                gv.DataBind();
               // dbManager.Close();
            }
        }
    }
}