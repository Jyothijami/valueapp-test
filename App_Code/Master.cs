
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using YantraDAL;

/// <summary>
/// Summary description for Master
/// </summary>
/// 
namespace YantraBLL.Modules
{
    public class Masters
    {
        private static int _returnIntValue;
        private static string _returnStringMessage, _commandText;

        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());

        public Masters()
        { }

        //Method for Checkbox List Filling with statement
        #region chkbox list fill
        public static void CheckboxListWithStatement(CheckBoxList cblName, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            cblName.Items.Clear();
            while (dbManager.DataReader.Read())
            {
                cblName.Items.Add(new ListItem(dbManager.DataReader[1].ToString(), dbManager.DataReader[0].ToString()));

            }
            dbManager.DataReader.Close();
            //dbManager.Close();
        }
        #endregion

        //Method for dispose 
        public static void Dispose()
        {
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

        private static bool IsRecordExistsForRate(string paraTableName, string paraFirstFieldName, string paraFirstFieldValue, string paraSecondFieldName, string paraSecondFieldValue)
        {
            bool check = false;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFirstFieldName + "='" + paraFirstFieldValue + "' and " + paraSecondFieldName + "='" + paraSecondFieldValue + "'").ToString());
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
            //dbManager.Close();
            return check;
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
            //dbManager.Close();
        }

        //Method for GridBind Fill
        private static void GridViewBind(GridView gv)
        {
            gv.DataSource = dbManager.DataReader;
            gv.DataBind();
            dbManager.DataReader.Close();
        }

        //Method For Product Master
        public class ProductMasterDetails
        {
            public string Product_Id, Product_Code, Product_Name, ReorderLevel, Rate, Image,Product_Detail_Id,ItemCode,Product_Specification,Product_Company;  //Product Master
            public ProductMasterDetails()
            { }

            public string ProductMasterDetails_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PRODUCT_MASTER]", "Product_Name", this.Product_Name) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PRODUCT_MASTER] SELECT ISNULL(MAX(Product_Id),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}',{6} FROM [YANTRA_LKUP_PRODUCT_MASTER]", this.Product_Code, this.Product_Name, this.ReorderLevel, this.Rate, this.Image,this.Product_Specification,this.Product_Company);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                   
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";

                        log.add_Insert("Product Master Details", "7");
                        
                    }
                }
                else
                {
                    _returnStringMessage = "Product Name Already Exists.";
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string ProductMasterDetails_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PRODUCT_MASTER]", "Product_Name", this.Product_Name, "Product_Id", this.Product_Id) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_PRODUCT_MASTER] SET Product_Code='{0}',Product_Name='{1}',ReorderLevel={2},Rate='{3}',Image='{4}',ProductSpecification='{5}',Product_Company ={6} WHERE Product_Id={7}", this.Product_Code, this.Product_Name, this.ReorderLevel, this.Rate, this.Image, this.Product_Specification,this.Product_Company, this.Product_Id);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";

                        log.add_Update("Product Master Details", "7");
                    }
                }
                else
                {
                    _returnStringMessage = "Product Name Already Exists.";
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string ProductMasterDetails_Delete()
            {
                Masters.BeginTransaction();
                _commandText = string.Format("DELETE FROM YANTRA_LKUP_PRODUCT_DETAILS WHERE Product_Id={0}", Product_Id);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                if (DeleteRecord("[YANTRA_LKUP_PRODUCT_MASTER]", "Product_Id", this.Product_Id) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Product Master Details", "7");
                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public static void ProductMasterDetails_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_LKUP_PRODUCT_MASTER] ORDER BY Product_Name");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Product_Name", "Product_Id");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }
        public class SendSMS
        {
            public string Message,Mobile_No;
            public SendSMS()
            {

            }
            public void Send_App_SMS(string Message,string MobileNo)
            {
                string url = "";
                url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

                WebClient client = new WebClient();
                string baseurl = url;
                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                data.Close();
                reader.Close();
            }

        }

        public class ProductDetails
        {
            public string Product_Detail_Id,Product_Id,ItemCode;
            public ProductDetails()
            { }

            public string ProductDetails_Save()
            {
                dbManager.Open();
             

                    string Product_Id = (dbManager.ExecuteScalar(CommandType.Text, "select max(Product_Id) from YANTRA_LKUP_PRODUCT_MASTER ")).ToString();
                    
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PRODUCT_DETAILS] SELECT ISNULL(MAX(Product_Det_Id),0)+1,{0},{1} FROM [YANTRA_LKUP_PRODUCT_DETAILS]", Product_Id, this.ItemCode);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Product Details", "7");

                    }
                    //dbManager.Close();

                    return _returnStringMessage;
             }


            public void SalesEnquiryDetails_Select(string EnquiryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_LKUP_PRODUCT_DETAILS,YANTRA_ITEM_MAST,YANTRA_LKUP_ITEM_TYPE,YANTRA_LKUP_PRODUCT_MASTER where YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID " +
                                               "and YANTRA_LKUP_PRODUCT_DETAILS.ItemCode = YANTRA_ITEM_MAST.ITEM_CODE and YANTRA_LKUP_PRODUCT_DETAILS.Product_Id =YANTRA_LKUP_PRODUCT_MASTER.Product_Id  and YANTRA_LKUP_PRODUCT_DETAILS.Product_Id = " + EnquiryId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable EnquiryInterestedProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Specifications");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                EnquiryInterestedProducts.Columns.Add(col);
             

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = EnquiryInterestedProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();

                    dr["Specifications"] = dbManager.DataReader["ITEM_SPEC"].ToString();
                  
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
             
                    EnquiryInterestedProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = EnquiryInterestedProducts;
                gv.DataBind();
                //dbManager.Close();

            }
            public string ProductDetails_Update()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PRODUCT_DETAILS] SELECT ISNULL(MAX(Product_Det_Id),0)+1,{0},{1} FROM [YANTRA_LKUP_PRODUCT_DETAILS]", Product_Id, this.ItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Update("Product Details", "7");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            //public string ProductDetails_Delete()
            //{
            //    //Masters.BeginTransaction();
            //    //if (DeleteRecord("[YANTRA_LKUP_PRODUCT_MASTER]", "Product_Id", this.Product_Id) == true)
            //    //{
            //    //    Masters.CommitTransaction();
            //    //    _returnStringMessage = "Data Deleted Successfully";
            //    //}
            //    //else
            //    //{
            //    //    Masters.RollBackTransaction();
            //    //    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            //    //}
            //    //return _returnStringMessage;
            //}


            //public static void ProductDetails_Select(Control ControlForBind)
            //{
            //    //dbManager.Open();
            //    //_commandText = string.Format("SELECT  * FROM [YANTRA_LKUP_PRODUCT_MASTER] ORDER BY Product_Name");
            //    //dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    //if (ControlForBind is DropDownList)
            //    //{
            //    //    DropDownListBind(ControlForBind as DropDownList, "Product_Name", "Product_Id");
            //    //}
            //    //else if (ControlForBind is GridView)
            //    //{
            //    //    GridViewBind(ControlForBind as GridView);
            //    //}
            //    ////dbManager.Close();     
            //}
        }

        //Methods For Department Master Form
        public class Department
        {
            public string DeptId, DeptName, DeptHead, DeptDesc;   //Department Master
            public Department()
            { }

            public string Department_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_DEPT_MAST]", "DEPT_NAME", this.DeptName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_DEPT_MAST] SELECT ISNULL(MAX(DEPT_ID),0)+1,'{0}','{1}','{2}' FROM [YANTRA_DEPT_MAST]", this.DeptName, this.DeptHead, this.DeptDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Department Details", "8");

                    }
                }
                else
                {
                    _returnStringMessage = "Department Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Department_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_DEPT_MAST]", "DEPT_NAME", this.DeptName, "DEPT_ID", this.DeptId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_DEPT_MAST] SET DEPT_NAME='{0}',DEPT_DESC='{1}',DEPT_HEAD={2} WHERE DEPT_ID={3}", this.DeptName, this.DeptDesc, this.DeptHead, this.DeptId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Department Details", "8");
                        
                    }
                }
                else
                {
                    _returnStringMessage = "Department Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Department_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_DEPT_MAST]", "DEPT_ID", this.DeptId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Department Details", "8");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void Department_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT DEPT_NAME,DEPT_ID FROM [YANTRA_DEPT_MAST] where DEPT_NAME is not null");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DEPT_NAME", "DEPT_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }


            

            public static void DepartmentHead_Select(Control ControlForBind,string Depthead)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT DEPT_NAME,DEPT_ID FROM [YANTRA_DEPT_MAST]  where   DEPT_HEAD = '"+Depthead+"' and  DEPT_NAME is not null");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DEPT_NAME", "DEPT_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }





            public static void Department_Select_Damage_Report(Control ControlForBind, string EmpId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT DEPT_NAME,DEPT_ID FROM [YANTRA_DEPT_MAST] where [YANTRA_DEPT_MAST].DEPT_ID in (select [YANTRA_DEPT_MAST].DEPT_ID from [YANTRA_DEPT_MAST] where [YANTRA_DEPT_MAST].DEPT_HEAD='" + EmpId + "') and DEPT_NAME is not null");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DEPT_NAME", "DEPT_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        //Methods For Designation Master Form
        public class Designation
        {
            public string DesgId, DesgName, DesgDesc;  //Desination Master
            public Designation()
            { }

            public string Designation_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_DESG_MAST]", "DESG_NAME", this.DesgName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_DESG_MAST] SELECT ISNULL(MAX(DESG_ID),0)+1,'{0}','{1}' FROM [YANTRA_DESG_MAST]", this.DesgName, this.DesgDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Designation Details", "9");

                    }
                }
                else
                {
                    _returnStringMessage = "Designation Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Designation_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_DESG_MAST]", "DESG_NAME", this.DesgName, "DESG_ID", this.DesgId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_DESG_MAST] SET DESG_NAME='{0}',DESG_DESC='{1}' WHERE DESG_ID={2}", this.DesgName, this.DesgDesc, this.DesgId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Designation Details", "9");

                    }
                }
                else
                {
                    _returnStringMessage = "Designation Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Designation_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_DESG_MAST]", "DESG_ID", this.DesgId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Designation Details", "9");
                   
                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public static void Designation_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_DESG_MAST] ORDER BY DESG_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DESG_NAME", "DESG_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        //Methods for Operation Master Form
        public class Operation
        {
            public string OprId, OprName, OprDesc;   //Operations Master
            public Operation()
            { }

            public string Operation_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_OPERATIONS]", "OPR_NAME", this.OprName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_OPERATIONS] SELECT ISNULL(MAX(OPR_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_OPERATIONS]", this.OprName, this.OprDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Operation Details", "10");

                    }
                }
                else
                {
                    _returnStringMessage = "Operation Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Operation_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_OPERATIONS]", "OPR_NAME", this.OprName, "OPR_ID", this.OprId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_OPERATIONS] SET OPR_NAME='{0}',OPR_DESC='{1}' WHERE OPR_ID={2}", this.OprName, this.OprDesc, this.OprId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Operation Details", "10");
                    }
                }
                else
                {
                    _returnStringMessage = "Operation Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Operation_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_OPERATIONS]", "OPR_ID", this.OprId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Operation Details", "10");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void Operation_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_OPERATIONS] ORDER BY OPR_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "OPR_NAME", "OPR_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods For Country Master Form
        public class Country
        {
            public string CountryId, CountryName;  //Country Master
            public Country()
            { }

            public static string GetCountryName(string Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT COUNTRY_NAME FROM [YANTRA_LKUP_COUNTRY_MAST] where COUNTRY_ID=" + Id );
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    //this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                    _returnStringMessage = dbManager.DataReader["COUNTRY_NAME"].ToString();

                }
                dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnStringMessage; ;
            }

            public string Country_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_COUNTRY_MAST]", "COUNTRY_NAME", this.CountryName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_COUNTRY_MAST] SELECT ISNULL(MAX(COUNTRY_ID),0)+1,'{0}' FROM [YANTRA_LKUP_COUNTRY_MAST]", this.CountryName );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Country Details", "11");
                    }
                }
                else
                {
                    _returnStringMessage = "Country Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Country_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_COUNTRY_MAST]", "COUNTRY_NAME", this.CountryName, "COUNTRY_ID", this.CountryId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_COUNTRY_MAST] SET COUNTRY_NAME='{0}' WHERE COUNTRY_ID={1}", this.CountryName,this.CountryId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Country Details", "11");

                    }
                }
                else
                {
                    _returnStringMessage = "Country Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Country_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_COUNTRY_MAST]", "COUNTRY_ID", this.CountryId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Country Details", "11");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                //dbManager.Close();

                return _returnStringMessage;
            }


            public static void Country_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_LKUP_COUNTRY_MAST] ORDER BY COUNTRY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COUNTRY_NAME", "COUNTRY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        //Methods For Enquiry Mode Form
        public class EnquiryMode
        {
            public string EnqmId, EnqmName, EnqmDesc;  //Equiry Mode
            public EnquiryMode()
            { }

            public string EnquiryMode_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ENQ_MODE]", "ENQM_NAME", this.EnqmName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ENQ_MODE] SELECT ISNULL(MAX(ENQM_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_ENQ_MODE]", this.EnqmName, this.EnqmDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Enquiry Details", "12");

                    }
                }
                else
                {
                    _returnStringMessage = "Enquiry  Mode Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string EnquiryMode_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ENQ_MODE]", "ENQM_NAME", this.EnqmName, "ENQM_ID", this.EnqmId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_ENQ_MODE] SET ENQM_NAME='{0}',ENQM_DESC='{1}' WHERE ENQM_ID={2}", this.EnqmName, this.EnqmDesc, this.EnqmId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Enquiry Details", "12");

                    }
                }
                else
                {
                    _returnStringMessage = "Enquiry Mode Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string EnquiryMode_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_ENQ_MODE]", "ENQM_ID", this.EnqmId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Enquiry Details", "12");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void EnquiryMode_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ENQ_MODE] ORDER BY ENQM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ENQM_NAME", "ENQM_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        //Methods For Payment Mode Form
        public class ReviewCate
        {
            public string RV_CAT_ID, RV_Cat_Name, RV_Cat_Desc;        // ReviewCate
            public ReviewCate()
            { }

            public string ReviewCate_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_REVIEW_CATEGORY]", "RV_CAT_NAME", this.RV_Cat_Name) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_REVIEW_CATEGORY] SELECT ISNULL(MAX(RV_Cat_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_REVIEW_CATEGORY]", this.RV_Cat_Name, this.RV_Cat_Desc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("ReviewCate Details", "13");

                    }
                }
                else
                {
                    _returnStringMessage = "Pay  Mode Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ReviewCate_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_REVIEW_CATEGORY]", "RV_CAT_NAME", this.RV_Cat_Name, "RV_Cat_ID", this.RV_CAT_ID) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_REVIEW_CATEGORY] SET RV_CAT_NAME='{0}',RV_CAT_Desc='{1}' WHERE RV_Cat_ID={2}", this.RV_Cat_Name, this.RV_Cat_Desc, this.RV_CAT_ID);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("ReviewCate Details", "13");

                    }
                }
                else
                {
                    _returnStringMessage = "Pay Mode Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ReviewCate_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_REVIEW_CATEGORY]", "RV_Cat_ID", this.RV_CAT_ID) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("ReviewCate Details", "13");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
        }


        //Methods For Payment Mode Form
        public class PayMode
        {
            public string PMId, PMName, PMDesc;        // PayMode
            public PayMode()
            { }

            public string PayMode_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PAY_MODE]", "PM_NAME", this.PMName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PAY_MODE] SELECT ISNULL(MAX(PM_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_PAY_MODE]", this.PMName, this.PMDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("PayMode Details", "13");
                        
                    }
                }
                else
                {
                    _returnStringMessage = "Pay  Mode Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string PayMode_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PAY_MODE]", "PM_NAME", this.PMName, "PM_ID", this.PMId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_PAY_MODE] SET PM_NAME='{0}',PM_DESC='{1}' WHERE PM_ID={2}", this.PMName, this.PMDesc, this.PMId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("PayMode Details", "13");

                    }
                }
                else
                {
                    _returnStringMessage = "Pay Mode Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string PayMode_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_PAY_MODE]", "PM_ID", this.PMId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("PayMode Details", "13");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
        }

        //Methods For Regional Master Form
        public class RegionalMaster
        {
            public string RegId, RegName, RegDesc;        // Regional Master
            public RegionalMaster()
            { }

            public string RegionalMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[location_tbl]", "locname", this.RegName) == false)
                {
                    _commandText = string.Format("INSERT INTO [location_tbl] values('{0}','{1}','{2}') ", this.RegId, this.RegName, this.RegDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Regional Master Details", "14");

                    }
                }
                else
                {
                    _returnStringMessage = "Regional Code Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string RegionalMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[location_tbl]", "locname", this.RegName, "locid", this.RegId) == false)
                {
                    _commandText = string.Format("UPDATE [location_tbl] SET locname='{0}',Description='{1}' WHERE locid={2}", this.RegName, this.RegDesc, this.RegId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Regional Master Details", "14");

                    }
                }
                else
                {
                    _returnStringMessage = "Regional Code Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string RegionalMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[location_tbl]", "locid", this.RegId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Regional Master Details", "14");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void RegionalMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [location_tbl] ORDER BY locname");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "locname", "locid");
                }
                //dbManager.Close();

            }
        }

        //Methods For Unit Master Form
        public class UnitMaster
        {
            public string UOMId, UOMName, UOMDesc;        // Unit Master
            public UnitMaster()
            { }

            public string UnitMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_UOM]", "UOM_SHORT_DESC", this.UOMName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_UOM] SELECT ISNULL(MAX(UOM_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_UOM]", this.UOMName, this.UOMDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Unit Master Details", "15");

                    }
                }
                else
                {
                    _returnStringMessage = "Unit Code Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string UnitMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_UOM]", "UOM_SHORT_DESC", this.UOMName, "UOM_ID", this.UOMId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_UOM] SET UOM_SHORT_DESC='{0}',UOM_LONG_DESC='{1}' WHERE UOM_ID={2}", this.UOMName, this.UOMDesc, this.UOMId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Unit Master Details", "15");

                    }
                }
                else
                {
                    _returnStringMessage = "Unit Code Already Exists.";
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string UnitMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_UOM]", "UOM_ID", this.UOMId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Unit Master Details", "15");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void UnitMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_UOM] ORDER BY UOM_SHORT_DESC");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "UOM_SHORT_DESC", "UOM_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods For Transporter Master Form
        public class TrasnporterMaster
        {
            public string TransId, TransContactPerson, TransAddr, TransLongName, TransContactNo, TransMobileNo; // Transporter Master
            public TrasnporterMaster()
            { }

            public string TransporterMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExistsForRate("[YANTRA_LKUP_TRANS_MAST]", "TRANS_CONTACT_PERSON", this.TransContactPerson, "TRANS_LONG_NAME", this.TransLongName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_TRANS_MAST] SELECT ISNULL(MAX(TRANS_ID),0)+1,'{0}','{1}','{2}','{3}','{4}' FROM [YANTRA_LKUP_TRANS_MAST]", this.TransContactPerson, this.TransAddr, this.TransLongName, this.TransContactNo, this.TransMobileNo);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Transporter Master Details", "16");

                    }
                }
                else
                {
                    _returnStringMessage = "Transporter Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string TransporterMaster_Update()
            {
                dbManager.Open();
               
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_TRANS_MAST] SET TRANS_CONTACT_PERSON='{0}',TRANS_ADDRESS='{1}',TRANS_LONG_NAME='{2}',TRANS_CONTACT_NO='{3}',TRANS_MOBILE_NO='{4}' WHERE TRANS_ID={5}", this.TransContactPerson, this.TransAddr, this.TransLongName, this.TransContactNo, this.TransMobileNo, this.TransId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";

                        log.add_Update("Transporter Master Details", "16");

                    }
                    //dbManager.Close();
               
                return _returnStringMessage;
            }

            public string TransporterMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_TRANS_MAST]", "TRANS_ID", this.TransId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Transporter Master Details", "16");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void TransporterMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT TRANS_LONG_NAME,TRANS_ID FROM [YANTRA_LKUP_TRANS_MAST] ORDER BY TRANS_LONG_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "TRANS_LONG_NAME", "TRANS_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods For Idle Codes Master Form
        public class IdleCode
        {
            public string IdleCodeId, IdleCodeName, IdleCodeDesc; //Idle Codes
            public IdleCode()
            { }

            public string IdleCode_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_IDLE_CODE]", "IDC_NAME", this.IdleCodeName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_IDLE_CODE] SELECT ISNULL(MAX(IDC_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_IDLE_CODE]", this.IdleCodeName, this.IdleCodeDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Idle Details", "17");

                    }
                }
                else
                {
                    _returnStringMessage = "Idle Code Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string IdleCode_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_IDLE_CODE]", "IDC_NAME", this.IdleCodeName, "IDC_ID", this.IdleCodeId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_IDLE_CODE] SET IDC_NAME='{0}',IDC_DESC='{1}' WHERE IDC_ID={2}", this.IdleCodeName, this.IdleCodeDesc, this.IdleCodeId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Idle Details", "17");

                    }
                }
                else
                {
                    _returnStringMessage = "Idle Code Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string IdleCode_Delete()
            {

                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_IDLE_CODE]", "IDC_ID", this.IdleCodeId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Idle Details", "17");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
        }

        //Methods For Company Profile Form
        public class CompanyProfile
        {
            public string CPCompanyId,CPFullName, CPShortName, CPAddress, CPContactNo1, CPFaxNo, CPContactNo2, CPEmail, CPTelexNo, CPAPGSTNo, CPCSTNo, CPECCNo, CPVATNo, CPPANNo, CPEstYear;
            public string CPCFYear, CPCPONo, CPCINo, CPCDCNo, CPYearStartDate, CPYearEndDate, CPInvoicePrefix, CPInvoiceSuffix, CPPOPrefix, CPPOSuffix, CPDCPrefix, CPDCSuffix, CPLogo, Status, locid;
            public string TechNo, DespNo;
            public CompanyProfile()
            { }

            public string CompanyProfile_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_COMP_PROFILE] SELECT ISNULL(MAX(CP_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}' FROM [YANTRA_COMP_PROFILE]", this.CPFullName, this.CPShortName, this.CPAddress, this.CPContactNo1, this.CPFaxNo, this.CPContactNo2, this.CPEmail, this.CPTelexNo, this.CPAPGSTNo, this.CPCSTNo, this.CPECCNo, this.CPVATNo, this.CPPANNo, this.CPEstYear, this.CPCFYear, this.CPCPONo, this.CPCINo, this.CPCDCNo, Convert.ToDateTime(this.CPYearStartDate), Convert.ToDateTime(this.CPYearEndDate), this.CPInvoicePrefix, this.CPInvoiceSuffix, this.CPPOPrefix, this.CPPOSuffix, this.CPDCPrefix, this.CPDCSuffix, this.CPLogo, this.locid, this.TechNo, this.DespNo, this.Status );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Company Profile Details", "19");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string CompanyProfile_Update()
            {
                dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_COMP_PROFILE] SET CP_FULL_NAME='{0}',CP_SHORT_NAME='{1}',CP_ADDRESS='{2}',CP_CONTACT_NO1='{3}',CP_FAXNO='{4}',CP_CONTACT_NO2='{5}',CP_EMAIL='{6}',CP_TELEX_NO='{7}',CP_APGST_NO='{8}',CP_CST_NO='{9}',CP_ECC_NO='{10}',CP_VAT_NO='{11}',CP_PAN_NO='{12}',CP_EST_YEAR='{13}' " +
                      ",CP_CF_YEAR='{14}',CP_CPO_NO='{15}',CP_CI_NO='{16}',CP_CDC_NO='{17}',CP_YEAR_STARTDATE='{18}',CP_YEAR_ENDDATE='{19}',CP_INVOICE_PREFIX='{20}',CP_INVOICE_SUFFIX='{21}',CP_PO_PREFIX='{22}',CP_PO_SUFFIX='{23}',CP_DC_PREFIX='{24}',CP_DC_SUFFIX='{25}', CP_LOGO='{26}', locid='{27}',Technical_No='{29}',Despatch_No='{30}',Status='{31}' WHERE CP_ID={28}",
                      this.CPFullName, this.CPShortName, this.CPAddress, this.CPContactNo1, this.CPFaxNo, this.CPContactNo2, this.CPEmail, this.CPTelexNo, this.CPAPGSTNo, this.CPCSTNo, this.CPECCNo, this.CPVATNo, this.CPPANNo, this.CPEstYear,
                      this.CPCFYear, this.CPCPONo, this.CPCINo, this.CPCDCNo, this.CPYearStartDate, this.CPYearEndDate, this.CPInvoicePrefix, this.CPInvoiceSuffix, this.CPPOPrefix, this.CPPOSuffix, this.CPDCPrefix, this.CPDCSuffix, this.CPLogo, this.locid, this.CPCompanyId,this.TechNo,this.DespNo,this.Status);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Company Profile Details", "19");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string CompanyProfile_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_COMP_PROFILE]", "CP_ID", this.CPCompanyId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Company Profile Details", "19");


                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public int CompanyProfile_Select()
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMP_PROFILE]");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CPFullName = dbManager.DataReader["CP_FULL_NAME"].ToString();
                    this.CPShortName = dbManager.DataReader["CP_SHORT_NAME"].ToString();
                    this.CPAddress = dbManager.DataReader["CP_ADDRESS"].ToString();
                    this.CPContactNo1 = dbManager.DataReader["CP_CONTACT_NO1"].ToString();
                    this.CPFaxNo = dbManager.DataReader["CP_FAXNO"].ToString();
                    this.CPContactNo2 = dbManager.DataReader["CP_CONTACT_NO2"].ToString();
                    this.CPEmail = dbManager.DataReader["CP_EMAIL"].ToString();
                    this.CPTelexNo = dbManager.DataReader["CP_TELEX_NO"].ToString();
                    this.CPAPGSTNo = dbManager.DataReader["CP_APGST_NO"].ToString();
                    this.CPCSTNo = dbManager.DataReader["CP_CST_NO"].ToString();
                    this.CPECCNo = dbManager.DataReader["CP_ECC_NO"].ToString();
                    this.CPVATNo = dbManager.DataReader["CP_VAT_NO"].ToString();
                    this.CPPANNo = dbManager.DataReader["CP_PAN_NO"].ToString();
                    this.CPEstYear = Convert.ToDateTime(dbManager.DataReader["CP_EST_YEAR"].ToString()).ToString("yyyy");
                    this.CPCFYear = dbManager.DataReader["CP_CF_YEAR"].ToString();
                    this.CPCPONo = dbManager.DataReader["CP_CPO_NO"].ToString();
                    this.CPCINo = dbManager.DataReader["CP_CI_NO"].ToString();
                    this.CPCDCNo = dbManager.DataReader["CP_CDC_NO"].ToString();
                    this.CPYearStartDate = Convert.ToDateTime(dbManager.DataReader["CP_YEAR_STARTDATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CPYearEndDate = Convert.ToDateTime(dbManager.DataReader["CP_YEAR_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    //////this.CPYearStartDate = dbManager.DataReader["CP_YEAR_STARTDATE"].ToString();
                    //////this.CPYearEndDate = dbManager.DataReader["CP_YEAR_ENDDATE"].ToString();
                    this.CPInvoicePrefix = dbManager.DataReader["CP_INVOICE_PREFIX"].ToString();
                    this.CPInvoiceSuffix = dbManager.DataReader["CP_INVOICE_SUFFIX"].ToString();
                    this.CPPOPrefix = dbManager.DataReader["CP_PO_PREFIX"].ToString();
                    this.CPPOSuffix = dbManager.DataReader["CP_PO_SUFFIX"].ToString();
                    this.CPDCPrefix = dbManager.DataReader["CP_DC_PREFIX"].ToString();
                    this.CPDCSuffix = dbManager.DataReader["CP_DC_SUFFIX"].ToString();
                    this.CPLogo = dbManager.DataReader["CP_LOGO"].ToString();
                    this.locid = dbManager.DataReader["locid"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnIntValue;
            }

            public static void  Company_Select(Control ControlForBind)
            {
                dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_COMP_PROFILE] ORDER BY CP_FULL_NAME");
                _commandText = string.Format("SELECT distinct(a.CP_ID), a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    //DropDownListBind(ControlForBind as DropDownList, "CP_FULL_NAME", "CP_ID");
                    DropDownListBind(ControlForBind as DropDownList, "COMP_NAME", "CP_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }



        }

        //Methods For Sub Contractor Master Form
        public class SubContractorMaster
        {
            public string SCId, SCName, SCContactPerson, SCAddress, SCContPersonDet, SCContactNo1, SCContactNo2, SCEmail, SCFAXNo, SCPANNo, SCCSTNo, SCVATNo, SCECCNo, SCRanking; // Sub Contractor Master
            public SubContractorMaster()
            { }

            public string SubContractorMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_SUB_CONTRACTOR_MAST]", "SC_NAME", this.SCName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_SUB_CONTRACTOR_MAST] SELECT ISNULL(MAX(SC_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}' FROM [YANTRA_SUB_CONTRACTOR_MAST]", this.SCName, this.SCContactPerson, this.SCAddress, this.SCContPersonDet, this.SCContactNo1, this.SCContactNo2, this.SCEmail, this.SCFAXNo, this.SCPANNo, this.SCCSTNo, this.SCVATNo, this.SCECCNo, this.SCRanking);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("SubContractor Details", "19");

                    }
                }
                else
                {
                    _returnStringMessage = "Sub Contractor Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string SubContractorMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_SUB_CONTRACTOR_MAST]", "SC_NAME", this.SCName, "SC_ID", this.SCId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_SUB_CONTRACTOR_MAST] SET SC_NAME='{0}',SC_CONTACT_PERSON='{1}',SC_ADDRESS='{2}',SC_CONTACT_PRESON_DET='{3}',SC_CONTACT_NO1='{4}',SC_CONTACT_NO2='{5}',SC_EMAIL='{6}',SC_FAX_NO='{7}',SC_PAN_NO='{8}',SC_CST_NO='{9}',SC_VAT_NO='{10}',SC_ECC_NO='{11}',SC_RANKING='{12}' WHERE SC_ID={13}", this.SCName, this.SCContactPerson, this.SCAddress, this.SCContPersonDet, this.SCContactNo1, this.SCContactNo2, this.SCEmail, this.SCFAXNo, this.SCPANNo, this.SCCSTNo, this.SCVATNo, this.SCECCNo, this.SCRanking, this.SCId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("SubContractor Details", "19");

                    }
                }
                else
                {
                    _returnStringMessage = "Sub Contractor Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string SubContractorMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_SUB_CONTRACTOR_MAST]", "SC_ID", this.SCId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("SubContractor Details", "19");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
        }

        //Methods For Company Machinary Master Form
        public class CompMachinaryMaster
        {
            public string CMId, CMMachineName, CMManufactName, CMInvoiceNo, CMWarrenty, CMInstDate, CMDesc, CMManufactDate, CMMachineType, CMMachineDescription; // Company Machinary Master
            public CompMachinaryMaster()
            { }

            public string CompMachinaryMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_COMP_MACHINARY_MAST]", "CM_MACHINE_NAME", this.CMMachineName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_COMP_MACHINARY_MAST] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", this.CMId, this.CMMachineName, this.CMManufactName, this.CMInvoiceNo, this.CMWarrenty, this.CMInstDate, this.CMDesc, this.CMManufactDate);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("CompMachinarry Details", "20");

                    }
                }
                else
                {
                    _returnStringMessage = "Machinary Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string CompMachinaryMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_COMP_MACHINARY_MAST]", "CM_MACHINE_NAME", this.CMMachineName, "CM_ID", this.CMId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_COMP_MACHINARY_MAST] SET CM_MACHINE_NAME='{0}',CM_MANUFACT_NAME='{1}',CM_INVOICE_NO='{2}',CM_WARANTY_DET='{3}',CM_INSTALLATION_DATE='{4}',CM_DESC='{5}',CM_MANUFACT_DATE='{6}' WHERE CM_ID='{7}'", this.CMMachineName, this.CMManufactName, this.CMInvoiceNo, this.CMWarrenty, this.CMInstDate, this.CMDesc, this.CMManufactDate, this.CMId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("CompMachinarry Details", "20");

                    }
                }
                else
                {
                    _returnStringMessage = "Machinary Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string CompMachinaryMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_COMP_MACHINARY_MAST]", "CM_ID", this.CMId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("CompMachinarry Details", "20");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public int CompMachinaryMaster_Change(string MachineId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMP_MACHINARY_MAST] WHERE CM_ID = " + MachineId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CMMachineType = dbManager.DataReader["CM_MACHINE_TYPE"].ToString();
                    this.CMMachineDescription = dbManager.DataReader["CM_DESC"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                //dbManager.Close();

                return _returnIntValue;
            }

            public static void MachineName_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMP_MACHINARY_MAST] ORDER BY CM_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CM_MACHINE_NAME", "CM_ID");
                }
                //dbManager.Close();

            }

            public static string CompMachinaryMaster_AutoGenCode()
            {
                string _codePrefix = "CM/";
                dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(CM_ID,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_COMP_MACHINARY_MAST]").ToString());
                //dbManager.Close();

                return _codePrefix + _returnIntValue;
            }
        }

        //Methods For Despatch Mode Form
        public class DespatchMode
        {
            public string DespmId, DespmName, DespmDesc;
            public DespatchMode()
            { }

            public string DespatchMode_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_DESP_MODE]", "DESPM_NAME", this.DespmName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_DESP_MODE] SELECT ISNULL(MAX(DESPM_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_DESP_MODE]", this.DespmName, this.DespmDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";

                        log.add_Insert("Despatch Details", "21");

                    }
                }
                else
                {
                    _returnStringMessage = "Despatch Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string DespatchMode_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_DESP_MODE]", "DESPM_NAME", this.DespmName) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_DESP_MODE] SET DESPM_NAME='{0}',DESPM_DESC='{1}' WHERE DESPM_ID={2}", this.DespmName, this.DespmDesc, this.DespmId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Despatch Details", "21");

                    }
                }
                else
                {
                    _returnStringMessage = "Despatch Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string DespatchMode_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_DESP_MODE]", "DESPM_ID", this.DespmId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Despatch Details", "21");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void DespatchMode_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT DESPM_NAME,DESPM_ID FROM [YANTRA_LKUP_DESP_MODE] where DESPM_ID <> '0' ORDER BY DESPM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DESPM_NAME", "DESPM_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods For Item Master Form
        public class ItemMaster
        {
            public string ItemCode,spItemCode, ItemName, ItemSpec, Materialtype, ItemtypeId, Uomid, Principalname, Itemseries, Purchasespec, ModelNo, IcId, Brandid, financialyear, rsp, mrp, roundprice, Barcode;
            public string HSN_Code, Remarks, GST_Tax, F2,Prepared_By;
            public string ItemMinStockQty, ItemMaterialType, ItemType, ItemTypeId, ItemQtyInHand, ItemUOMId, ItemDetManf, ItemDetMfgDate, ItemDetExpDate, ItemDetBatchNo, ItemUOMShort, ItemUOMLong, ItemRate, ItemPrincipalName, ItemSeries, ItemPurchaseSpec, ItemPurchaseTypeId, ItemPurchaseTypeName, ItemCategoryId, BrandName, Color, IFY, IRM_ID, ItemCategoryName, Room, BrandProductName, ItemQtyId, CpId, PartOf, Rate;
           // public string ItemCode, ItemName, ItemSpec, ItemMinStockQty, ItemMaterialType, ItemType, ItemTypeId, ItemQtyInHand, ItemUOMId, ItemDetManf, ItemDetMfgDate, ItemDetExpDate, ItemDetBatchNo, ItemUOMShort, ItemUOMLong, ItemRate, ItemPrincipalName, ItemSeries, ItemPurchaseSpec, ItemPurchaseTypeId, ItemPurchaseTypeName, ModelNo, ItemCategoryId, BrandName, Color, IFY, IRM_ID, ItemCategoryName, Room, BrandProductName, ItemQtyId, CpId, PartOf, Rate;
            public string quotQunatity, quotRate, quotSpprice, quotDiscount;
            public string detailscolorid, detailsitemcode, itemquantity, location;
            public string qtycolorid, qtyitemcode, qtygdid, qtyquantity, qtyrq, qtycompanyid;
            public string supporate, quantity;
            public string Itemattachment, attachmentdate;
            public string ItemImage, ItemDate;
            public string itemSpecifcation, Specdate,Status,SPCode,SpModelNo,SpDisc,spImage,spImageId;
            public ItemMaster()
            { }

            public static void ItemMaster_BrandSelect(Control ControlForBind, string brandId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_MODEL_NO,ITEM_CODE FROM YANTRA_ITEM_MAST I,YANTRA_LKUP_PRODUCT_COMPANY B where B.PRODUCT_COMPANY_ID = I.BRAND_ID AND I.BRAND_ID=" + brandId + " ORDER BY I.ITEM_MODEL_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void ModelNoSelect_Brand_Cat_SubCat(Control ControlForBind, string brandId, string catId, string subCatId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_MODEL_NO,ITEM_CODE FROM YANTRA_ITEM_MAST I,YANTRA_LKUP_PRODUCT_COMPANY B where B.PRODUCT_COMPANY_ID = I.BRAND_ID AND I.BRAND_ID=" + brandId + " and I.IC_ID="+ catId +" and I.IT_TYPE_ID="+ subCatId +" ORDER BY I.ITEM_MODEL_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMaster_ModelNoGenSelect(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_MODEL_NO,ITEM_CODE FROM YANTRA_ITEM_MAST ORDER BY ITEM_MODEL_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public string ItemMaster_ModelNoSelect1(string ModelNo)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_MODEL_NO,ITEM_CODE FROM YANTRA_ITEM_MAST where ITEM_CODE ='" + ModelNo + "' ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    //this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();

                }
                dbManager.DataReader.Close();
                return _returnStringMessage; ;
            }
            public static void ItemMaster_GoDownSelect(Control ControlForBind, string CmpId)
            {
                dbManager.Open();
                //_commandText = string.Format("SELECT * FROM YANTRA_COMP_PROFILE C,YANTRA_LKUP_GODOWN G where C.CP_ID = G.CP_ID AND G.CP_ID=" + CmpId + " ORDER BY G.GODOWN_NAME");
                _commandText = string.Format("select GODOWN_NAME,GODOWN_ID from YANTRA_LKUP_GODOWN,YANTRA_COMP_PROFILE where YANTRA_LKUP_GODOWN.cp_id = YANTRA_COMP_PROFILE.CP_ID and YANTRA_LKUP_GODOWN.cp_id =" + CmpId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                //dbManager.Close();

            }


            public static void ItemMaster_GoDown(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_LKUP_GODOWN ORDER BY GODOWN_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                //dbManager.Close();

            }

            public static void ItemMaster_ModelNoSelect1(Control ControlForBind, string MdNo)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT DISTINCT YANTRA_LKUP_COLOR_MAST.COLOUR_ID, YANTRA_LKUP_COLOR_MAST.COLOUR_NAME,PI_DET_ID  FROM YANTRA_LKUP_COLOR_MAST INNER JOIN YANTRA_ITEM_COLOR_DETAILS ON YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_ITEM_COLOR_DETAILS.COLOR_ID inner join YANTRA_PURCHASE_INVOICE_DET on YANTRA_ITEM_COLOR_DETAILS .ITEM_CODE =YANTRA_PURCHASE_INVOICE_DET .ITEM_CODE where YANTRA_ITEM_COLOR_DETAILS.COLOR_ID!=0 and YANTRA_PURCHASE_INVOICE_DET.PI_DET_ID = " + MdNo);
                //_commandText = string.Format("SELECT dbo.YANTRA_LKUP_COLOR_MAST.COLOUR_ID, dbo.YANTRA_LKUP_COLOR_MAST.COLOUR_NAME FROM dbo.YANTRA_ITEM_COLOR_DETAILS INNER JOIN dbo.YANTRA_LKUP_COLOR_MAST ON dbo.YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = dbo.YANTRA_LKUP_COLOR_MAST.COLOUR_ID WHERE  dbo.YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE="+MdNo);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "PI_DET_ID");
                }
                //dbManager.Close();

            }
            public static void ItemMaster_ModelNoSelect2(Control ControlForBind, string MdNo)
            {
                dbManager.Open();
                _commandText = string.Format("select   DISTINCT YANTRA_LKUP_COLOR_MAST.COLOUR_ID, YANTRA_LKUP_COLOR_MAST.COLOUR_NAME from YANTRA_LKUP_COLOR_MAST inner join YANTRA_ITEM_COLOR_DETAILS on YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_ITEM_COLOR_DETAILS.COLOR_ID inner join YANTRA_ITEM_MAST on YANTRA_ITEM_COLOR_DETAILS .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE  where YANTRA_ITEM_COLOR_DETAILS.COLOR_ID!=0 and YANTRA_ITEM_MAST.ITEM_MODEL_NO = '" + MdNo + "' ");
                //_commandText = string.Format("SELECT dbo.YANTRA_LKUP_COLOR_MAST.COLOUR_ID, dbo.YANTRA_LKUP_COLOR_MAST.COLOUR_NAME FROM dbo.YANTRA_ITEM_COLOR_DETAILS INNER JOIN dbo.YANTRA_LKUP_COLOR_MAST ON dbo.YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = dbo.YANTRA_LKUP_COLOR_MAST.COLOUR_ID WHERE  dbo.YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE="+MdNo);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "COLOUR_ID");
                }
                //dbManager.Close();

            }
            public static void ItemMaster_ModelNoSelect(Control ControlForBind, string MdNo)
            {
                dbManager.Open();
                //_commandText = string.Format("select   DISTINCT YANTRA_LKUP_COLOR_MAST.COLOUR_ID, YANTRA_LKUP_COLOR_MAST.COLOUR_NAME from YANTRA_LKUP_COLOR_MAST inner join YANTRA_ITEM_COLOR_DETAILS on YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_ITEM_COLOR_DETAILS.COLOR_ID inner join YANTRA_ITEM_MAST on YANTRA_ITEM_COLOR_DETAILS .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE  where YANTRA_ITEM_COLOR_DETAILS.COLOR_ID!=0 and YANTRA_ITEM_MAST.ITEM_MODEL_NO = '" + MdNo+"' ");
                _commandText = string.Format("select   DISTINCT YANTRA_LKUP_COLOR_MAST.COLOUR_ID, YANTRA_LKUP_COLOR_MAST.COLOUR_NAME from YANTRA_LKUP_COLOR_MAST inner join YANTRA_ITEM_COLOR_DETAILS on YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_ITEM_COLOR_DETAILS.COLOR_ID WHERE YANTRA_ITEM_COLOR_DETAILS.COLOR_ID!=0 and  dbo.YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE=" + MdNo);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "COLOUR_ID");
                }
                //dbManager.Close();

            }


            public static void ItemMaster_SelectForComplaint(Control ControlForBind, string brandId, string subCatId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_ITEM_MAST I,YANTRA_LKUP_ITEM_TYPE T,YANTRA_LKUP_PRODUCT_COMPANY B where I.IT_TYPE_ID = T.IT_TYPE_ID and B.PRODUCT_COMPANY_ID = I.BRAND_ID and I.IT_TYPE_ID=" + subCatId + " AND I.BRAND_ID=" + brandId + " ORDER BY I.ITEM_MODEL_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public string ItemMaster_AutoGen1()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(Item_SpareCode),0)+1 FROM YANTRA_ITEM_SPARE_MAST").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }
            public string ItemMaster_AutoGen()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(ITEM_CODE),0)+1 FROM YANTRA_ITEM_MAST").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }
            public static string GetItemTypeId(string ItemCode)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE]" +
                            " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND " +
                            "  [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ORDER BY ITEM_CODE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    //this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                    _returnStringMessage = dbManager.DataReader["ITEM_CODE"].ToString();

                }
                dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnStringMessage; ;
            }




            //public int ItemMaster_Select(string ItemCode)
            //{
            //    dbManager.Open();
            //    try
            //    {
            //        _commandText = string.Format("SELECT *,YANTRA_LKUP_ITEM_RATE_MASTER.ITEM_RATE as rate FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY],[YANTRA_LKUP_ITEM_RATE_MASTER] " +
            //                    " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID and " +
            //                    " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID and YANTRA_LKUP_ITEM_RATE_MASTER.ITEM_CODE =YANTRA_ITEM_MAST.ITEM_CODE  and [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ");

            //        //_commandText = string.Format("SELECT *,YANTRA_LKUP_ITEM_RATE_MASTER.ITEM_RATE as rate,YANTRA_ITEM_QTY.ITEM_QTY_IN_HAND as phani FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY],[YANTRA_LKUP_ITEM_RATE_MASTER],YANTRA_ITEM_QTY " +
            //        //            " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND YANTRA_ITEM_MAST.ITEM_CODE = YANTRA_ITEM_QTY.ITEM_CODE and " +
            //        //            " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID and YANTRA_LKUP_ITEM_RATE_MASTER.ITEM_CODE =YANTRA_ITEM_MAST.ITEM_CODE  and [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ");
            //        dbManager.ExecuteReader(CommandType.Text, _commandText);
            //        if (dbManager.DataReader.Read())
            //        {
            //            this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
            //            this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
            //            this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
            //            this.ItemMinStockQty = dbManager.DataReader["ITEM_MIN_STOCK_QTY"].ToString();
            //            this.ItemMaterialType = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
            //            this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
            //            this.ItemQtyInHand = dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString();
            //            this.ItemUOMLong = dbManager.DataReader["UOM_LONG_DESC"].ToString();
            //            this.ItemUOMShort = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
            //            this.ItemUOMId = dbManager.DataReader["UOM_ID"].ToString();
            //            this.ItemTypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
            //            this.ItemAttachments = dbManager.DataReader["ITEM_ATTACHMENTS"].ToString();
            //            this.ItemRate = dbManager.DataReader["ITEM_RATE"].ToString();
            //            this.ItemPrincipalName = dbManager.DataReader["ITEM_PRINCIPAL_NAME"].ToString();
            //            this.ItemSeries = dbManager.DataReader["ITEM_SERIES"].ToString();
            //            this.ItemPurchaseSpec = dbManager.DataReader["ITEM_PURCHASE_SPEC"].ToString();
            //            this.ItemPurchaseTypeId = dbManager.DataReader["ITEM_PURCHASE_ITEM_TYPE_ID"].ToString();
            //            this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //            this.ItemCategoryId = dbManager.DataReader["IC_ID"].ToString();
            //            this.BrandName = dbManager.DataReader["BRAND_ID"].ToString();
            //            this.BrandProductName = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
            //            this.Color = dbManager.DataReader["COLOR"].ToString();
            //            this.IFY = dbManager.DataReader["FINANCIAL_YEAR"].ToString();
            //            this.ItemCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
            //            this.PartOf = dbManager.DataReader["PARTOF"].ToString();
            //            this.Rate = dbManager.DataReader["rate"].ToString();

            //            _returnIntValue = 1;
            //        }
            //        else
            //        {
            //            _returnIntValue = 0;
            //        }
            //        dbManager.DataReader.Close();
            //          //dbManager.Close();
                            
            //    }
            //    catch
            //    {

            //    }
            //    finally
            //    {
            //        //  Masters.Dispose();

            //    }
            //    return _returnIntValue;

            //}
            public int ItemMaster_Select345(string ItemCode)
            {
                dbManager.Open();
                try
                {
                   
                    _commandText = string.Format("SELECT top 1 YANTRA_ITEM_MAST.ITEM_NAME, YANTRA_ITEM_MAST.ITEM_SPEC, YANTRA_LKUP_ITEM_TYPE.IT_TYPE,YANTRA_ITEM_IMAGE.Item_Image, " +
                     " YANTRA_LKUP_UOM.UOM_SHORT_DESC, YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_NAME, " +
                     " YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_NAME " +
                     "FROM  YANTRA_ITEM_MAST INNER JOIN " +
                      "YANTRA_LKUP_ITEM_TYPE ON YANTRA_ITEM_MAST.IT_TYPE_ID = YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID INNER JOIN " +
                      "YANTRA_LKUP_UOM ON YANTRA_ITEM_MAST.UOM_ID = YANTRA_LKUP_UOM.UOM_ID INNER JOIN " +
                      "YANTRA_LKUP_ITEM_CATEGORY ON YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID INNER JOIN " +
                      "YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  " +
                      "left outer join [YANTRA_ITEM_IMAGE] on YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_IMAGE].[Item_Code]" +
                      "where [YANTRA_ITEM_MAST].ITEM_CODE= " + ItemCode + " ");


                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
                        this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
                        this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                        this.ItemUOMShort = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                        this.BrandProductName = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                        this.ItemCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
                        this.ItemImage = dbManager.DataReader["Item_Image"].ToString();
                        _returnIntValue = 1;
                        dbManager.DataReader.Close();

                    }
                    else
                    {
                        _returnIntValue = 0;
                    }

                }
                catch
                {

                }
                finally
                {
                    // Masters.Dispose();
                    dbManager.DataReader.Close();



                }
                //dbManager.Close();

                return _returnIntValue;

            }

            public int supporate_select(string Itemcode)
            {
                dbManager.Open();
                _commandText = string.Format("select SUP_QUOT_DET_RATE from  YANTRA_SUP_QUOT_DET where ITEM_CODE = " + Itemcode + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.supporate = dbManager.DataReader["SUP_QUOT_DET_RATE"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnIntValue;
            }

            public int StockEntry_select(string Itemcode)
            {
                dbManager.Open();
                _commandText = string.Format("select * from YANTRA_ITEM_QTY,YANTRA_LKUP_GODOWN  where YANTRA_ITEM_QTY.GODOWN_ID = YANTRA_LKUP_GODOWN.GODOWN_ID and YANTRA_ITEM_QTY.ITEM_CODE = " + Itemcode + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString();
                    this.location = dbManager.DataReader["GODOWN_NAME"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnIntValue;
            }





            ///DC Form Item Avail stock

            public int ItemStockAvail_select(string Itemcode)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid)-(select COUNT(*) from outward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid  )) as TOTAL_AVAILABLE_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code) as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " group by p.item_code,p.cp_id,p.colour_id,p.whlocid ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["TOTAL_AVAILABLE_STOCK"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            public string TtlQuantity, AvaliableQuantity, BlockQuantity, CustQty, locName, locId;
            public int TtlStockAvailNew_select(string Itemcode, string Color)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT(isnull((select SUM(Quantity) from INWARD_NEW where item_code=p.item_code and colour_id=p.colour_id),0)-isnull((select SUM(Quantity) from outward_New where item_code=p.item_code and colour_id=p.colour_id),0)) as TOTAL_STOCK,isnull((select SUM(Quantity) from BLOCK_New where item_code=p.item_code and colour_id=p.COLOUR_ID),0) as TOTAL_BLOCK_Stock, " +
                                            "(isnull((select SUM(Quantity) from INWARD_NEW where item_code=p.item_code and colour_id=p.colour_id),0)-isnull((select SUM(Quantity) from outward_New where item_code=p.item_code and colour_id=p.colour_id),0)-isnull((select SUM(Quantity) from BLOCK_New where item_code=p.item_code and colour_id=p.colour_id),0)) as TOTAL_AVALIABLE_STOCK " +
                                            " from INWARD_NEW  p left join outward_New out on p.item_code=out.item_code left join BLOCK_New blo on p.item_code=blo.item_code  where p.ITEM_CODE = '" + Itemcode + "' and p.COLOUR_ID = '" + Color + "' group by p.item_code,p.colour_id");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.TtlQuantity = dbManager.DataReader["TOTAL_STOCK"].ToString();
                    this.BlockQuantity = dbManager.DataReader["TOTAL_BLOCK_Stock"].ToString();
                    this.AvaliableQuantity = dbManager.DataReader["TOTAL_AVALIABLE_STOCK"].ToString();
                    // this.CustQty = dbManager.DataReader["Cust_Block_Items"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }
            public int TtlStockAvailNew_select(string Itemcode, string Color, string locId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT(isnull((select SUM(Quantity) from V_INWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)-isnull((select SUM(Quantity) from V_OUTWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)) as TOTAL_STOCK,isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.COLOUR_ID and locid=p.locid),0) " +
                                              "as TOTAL_BLOCK_Stock,(isnull((select SUM(Quantity) from V_INWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)- isnull((select SUM(Quantity) from V_OUTWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0) -isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)) " +
                                               " as TOTAL_AVALIABLE_STOCK,p.locname,p.locid from V_INWARDNew  p left join V_OUTWARDNew out on p.item_code=out.item_code left join V_BLOCKNew blo on p.item_code=blo.item_code  where p.ITEM_CODE = '" + Itemcode + "' and p.COLOUR_ID = '" + Color + "' and p.locid = '" + locId + "' group by p.item_code,p.colour_id,p.locid,p.locname");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.TtlQuantity = dbManager.DataReader["TOTAL_STOCK"].ToString();
                    this.BlockQuantity = dbManager.DataReader["TOTAL_BLOCK_Stock"].ToString();
                    this.AvaliableQuantity = dbManager.DataReader["TOTAL_AVALIABLE_STOCK"].ToString();
                    this.locName = dbManager.DataReader["locname"].ToString();
                    this.locId = dbManager.DataReader["locid"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }
            public int CustStockAvailNew_select(string Itemcode, string Color, string So_Id, string Customer_Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT isnull((select SUM(Quantity) from BLOCK_New where item_code=p.item_code and colour_id=p.colour_id and SO_Id=p.SO_Id and Customer_Id=p.Customer_Id),0) as Cust_Block_Items from  BLOCK_New p where p.ITEM_CODE = '" + Itemcode + "' and p.COLOUR_ID = '" + Color + "' and p.SO_Id = '" + So_Id + "' and p.Customer_Id=" + Customer_Id + " group by p.item_code,p.colour_id,p.SO_Id,p.Customer_Id");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.TtlQuantity = dbManager.DataReader["TOTAL_STOCK"].ToString();
                    //this.BlockQuantity = dbManager.DataReader["TOTAL_BLOCK_Stock"].ToString();
                    //this.AvaliableQuantity = dbManager.DataReader["TOTAL_AVALIABLE_STOCK"].ToString();
                    this.CustQty = dbManager.DataReader["Cust_Block_Items"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }
            public int TtlStockAvail_select(string Itemcode, string Color)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code and colour_id=p.COLOUR_ID) as TOTAL_BLOCK_Stock,((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from block where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_AVALIABLE_STOCK from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = '" + Itemcode + "' and p.COLOUR_ID = '" + Color + "' group by p.item_code,p.colour_id");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.TtlQuantity = dbManager.DataReader["TOTAL_STOCK"].ToString();
                    this.BlockQuantity = dbManager.DataReader["TOTAL_BLOCK_Stock"].ToString();
                    this.AvaliableQuantity = dbManager.DataReader["TOTAL_AVALIABLE_STOCK"].ToString();
                   // this.CustQty = dbManager.DataReader["Cust_Block_Items"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            public int CustStockAvail_select(string Itemcode, string Color, string So_Id, string Customer_Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT(select COUNT(*) from BlOCK where item_code=p.item_code and colour_id=p.colour_id and SO_Id=p.SO_Id and Customer_Id=p.Customer_Id) as Cust_Block_Items from  block p where p.ITEM_CODE = '" + Itemcode + "' and p.COLOUR_ID = '" + Color + "' and p.SO_Id = '" + So_Id + "' and p.Customer_Id=" + Customer_Id + " group by p.item_code,p.colour_id,p.SO_Id,p.Customer_Id");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.TtlQuantity = dbManager.DataReader["TOTAL_STOCK"].ToString();
                    //this.BlockQuantity = dbManager.DataReader["TOTAL_BLOCK_Stock"].ToString();
                    //this.AvaliableQuantity = dbManager.DataReader["TOTAL_AVALIABLE_STOCK"].ToString();
                   this.CustQty = dbManager.DataReader["Cust_Block_Items"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            //(select COUNT(*) from BlOCK where item_code=p.item_code and colour_id=p.colour_id and SO_Id=blo.SO_Id and Customer_Id=blo.Customer_Id) as Cust_Block_Items
           // and blo.Customer_Id=" + Customer_Id + "
            public int StockAvail_select(string Itemcode, string Color)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT(select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from block where item_code=p.item_code and colour_id=p.colour_id) as TOTAL_AVALIABLE_STOCK from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.COLOUR_ID = " + Color + " group by p.item_code,p.colour_id");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AvaliableQuantity = dbManager.DataReader["TOTAL_AVALIABLE_STOCK"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }


            //Dc Item With Color

            public int ItemColorStockAvail_select(string Itemcode,string colorid)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid)-(select COUNT(*) from outward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid  )) as TOTAL_AVAILABLE_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code) as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.colour_id = "+colorid+" group by p.item_code,p.cp_id,p.colour_id,p.whlocid ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["TOTAL_AVAILABLE_STOCK"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }


            //Dc Item With Color and company

            public int ItemColorCompanyStockAvail_select(string Itemcode, string colorid, string companyid)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid)-(select COUNT(*) from outward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid  )) as TOTAL_AVAILABLE_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code) as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.colour_id = " + colorid + " and p.cp_id = "+companyid+" group by p.item_code,p.cp_id,p.colour_id,p.whlocid ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["TOTAL_AVAILABLE_STOCK"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }


            //Dc Item With Color and company and Location

            public int ItemColorcompanylocation_select(string Itemcode, string colorid, string companyid, string location)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid)-(select COUNT(*) from outward where item_code=p.item_code and cp_id=p.cp_id and colour_id=p.colour_id and whlocid=p.whlocid  )) as TOTAL_AVAILABLE_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code) as TOTAL_BLOCK_Stock from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.colour_id = " + colorid + " and p.cp_id = " + companyid + " and p.whlocid ="+location+" group by p.item_code,p.cp_id,p.colour_id,p.whlocid ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["TOTAL_AVAILABLE_STOCK"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }


            //Dc Location Bind By Item Model No

            public static void ModelLocation(Control controlForBind, string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [WH_Locations].[whLocId] as locid,[WH_Locations].[whLocName] as locname  FROM [vltn].[dbo].[WH_Locations],INWARD where INWARD.whLocId = [WH_Locations].whLocId and INWARD.item_code = " + ItemCode + " group by [WH_Locations].[whLocId],[WH_Locations].[whLocName]");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (controlForBind is DropDownList)
                {
                    DropDownListBind(controlForBind as DropDownList, "locname", "locid");
                }
				//dbManager.Close();
            }
            public int QTYInHand_select(string Itemcode, string Color)
            {
                dbManager.Open();
                _commandText = string.Format("select sum(ITEM_QTY_IN_HAND) as phani from yantra_item_qty where item_code =" + Itemcode + " and COLOUR_ID = " + Color + "   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["phani"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            //Qty with COlor and model
            public int QTYInHandCOlor_select(string Itemcode, string Colorid)
            {
                dbManager.Open();
                //select distinct((select COUNT(*) from [INWARD] where ITEM_CODE=11896 and COLOUR_ID=460)-(select COUNT(*) from [OUTWARD] where ITEM_CODE=11896  and COLOUR_ID=460)-(select COUNT(*) from BlOCK where ITEM_CODE=11896  and COLOUR_ID=460)) as Qty 
                _commandText = string.Format("select distinct((select COUNT(*) from [INWARD] where item_code =" + Itemcode + " and COLOUR_ID=" + Colorid + ")-(select COUNT(*) from [OUTWARD] where item_code =" + Itemcode + " and COLOUR_ID=" + Colorid + ")-(select COUNT(*) from BlOCK where item_code =" + Itemcode + "  and COLOUR_ID=" + Colorid + ")) as Qty  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["Qty"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            public int QTYInHand_select(string Itemcode)
            {
                dbManager.Open();
                
                _commandText = string.Format("select distinct((select COUNT(*) from [INWARD] where item_code =" + Itemcode + ")-(select COUNT(*) from [OUTWARD] where item_code =" + Itemcode + ")-(select COUNT(*) from BlOCK where item_code =" + Itemcode + ")) as Qty  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["Qty"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            public int QTY_select(string Itemcode, string Pono)
            {

                dbManager.Open();
                _commandText = string.Format("SELECT SO_DET_QTY FROM [YANTRA_SO_DET] WHERE  [YANTRA_SO_DET].ITEM_CODE =" + ItemCode + "  and YANTRA_SO_DET.SO_ID = " + Pono + " ");

                //_commandText = string.Format("select sum(ITEM_QTY_IN_HAND) as phani from yantra_item_qty where item_code =" + Itemcode + "  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.quantity = dbManager.DataReader["SO_DET_QTY"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            public int IndentQTYInHand_select(string Itemcode, string godownid)
            {
                dbManager.Open();
                _commandText = string.Format("select ITEM_QTY_IN_HAND as phani from yantra_item_qty where item_code =" + Itemcode + " and  GODOWN_ID =" + godownid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.itemquantity = dbManager.DataReader["phani"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }

            public int StockEntrychange_select(string itemcode, string godown, string colorid)
            {

                if (godown == "0")
                {
                    dbManager.Open();
                    _commandText = string.Format("select * from YANTRA_ITEM_QTY Q,YANTRA_LKUP_GODOWN G  where Q.GODOWN_ID = G.GODOWN_ID and Q.ITEM_CODE = " + itemcode + " and Q.COLOUR_ID = '" + colorid + "' ");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.itemquantity = dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString();
                        _returnIntValue = 1;
                    }
                    else
                    {
                        this.itemquantity = "0";
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                    return _returnIntValue;
                }

                else
                {
                    dbManager.Open();
                    _commandText = string.Format("select * from YANTRA_ITEM_QTY,YANTRA_LKUP_GODOWN  where YANTRA_ITEM_QTY.GODOWN_ID = YANTRA_LKUP_GODOWN.GODOWN_ID and YANTRA_ITEM_QTY.ITEM_CODE = " + itemcode + " and YANTRA_LKUP_GODOWN.GODOWN_ID = " + godown + " and YANTRA_ITEM_QTY.COLOUR_ID = " + colorid + " ");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.itemquantity = dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString();
                        _returnIntValue = 1;
                    }
                    else
                    {
                        this.itemquantity = "0";
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                    return _returnIntValue;
                }

            }
           
            public static void PurchaseQuotationItemTypes1_Select(string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_FIXED_PO_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_FIXED_PO_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE)  AND  [YANTRA_FIXED_PO_DET].FPO_ID=" + ItemTypeId + " order by ITEM_MODEL_NO asc   ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void PurchaseQuotationItemTypesIndent_Select(string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_INDENT_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_INDENT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE)  AND  [YANTRA_INDENT_DET].IND_ID=" + ItemTypeId + "  order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void PerformaInvoiceItemTypesIndent_Select(string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_SUP_QUOT_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_SUP_QUOT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE)  AND  [YANTRA_SUP_QUOT_DET].SUP_QUOT_ID=" + ItemTypeId + " order by ITEM_MODEL_NO asc  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void PurchaseQuotationItemTypesPerformaInvoice_Select(string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_MODEL_NO),([YANTRA_ITEM_MAST].ITEM_CODE) FROM [YANTRA_SUP_ENQ_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_SUP_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE)  AND  [YANTRA_SUP_ENQ_DET].SUP_ENQ_ID=" + ItemTypeId + "  order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void Stockentry(Control controlForBind, string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_ITEM_QTY,YANTRA_LKUP_GODOWN  where YANTRA_ITEM_QTY.GODOWN_ID = YANTRA_LKUP_GODOWN.GODOWN_ID and YANTRA_ITEM_QTY.ITEM_CODE = " + ItemCode + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (controlForBind is DropDownList)
                {
                    DropDownListBind(controlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                //dbManager.Close();

            }

            public static void Stockentry1(Control controlForBind, string ItemCode, string cpid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_LKUP_GODOWN.GODOWN_NAME,YANTRA_LKUP_GODOWN.GODOWN_ID  from YANTRA_ITEM_QTY,YANTRA_LKUP_GODOWN,dbo.YANTRA_COMP_PROFILE  where YANTRA_ITEM_QTY.GODOWN_ID = YANTRA_LKUP_GODOWN.GODOWN_ID and dbo.YANTRA_ITEM_QTY.CP_ID = dbo.YANTRA_COMP_PROFILE.CP_ID  and YANTRA_ITEM_QTY.ITEM_CODE = " + ItemCode + " and dbo.YANTRA_ITEM_QTY.CP_ID = " + cpid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (controlForBind is DropDownList)
                {
                    DropDownListBind(controlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                //dbManager.Close();

            }
            public static void Stockentry143(Control controlForBind, string cpid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_LKUP_GODOWN.GODOWN_NAME,YANTRA_LKUP_GODOWN.GODOWN_ID  from YANTRA_LKUP_GODOWN,dbo.YANTRA_COMP_PROFILE  where YANTRA_LKUP_GODOWN.CP_ID = YANTRA_COMP_PROFILE.CP_ID   and YANTRA_LKUP_GODOWN.CP_ID = " + cpid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (controlForBind is DropDownList)
                {
                    DropDownListBind(controlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                //dbManager.Close();

            }
            public static void Stockentry12(Control controlForBind, string cpid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_LKUP_GODOWN.GODOWN_NAME,YANTRA_LKUP_GODOWN.GODOWN_ID from YANTRA_LKUP_GODOWN,dbo.YANTRA_COMP_PROFILE  where  YANTRA_LKUP_GODOWN.CP_ID = YANTRA_COMP_PROFILE.CP_ID and YANTRA_COMP_PROFILE.cp_id =  " + cpid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (controlForBind is DropDownList)
                {
                    DropDownListBind(controlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                //dbManager.Close();

            }
            public int SalesOrderMaster_Select(string ItemCode)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_QUOT_DET] where YANTRA_QUOT_DET.QUOT_ID = " + ItemCode + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.quotQunatity = dbManager.DataReader["QUOT_DET_QTY"].ToString();
                    this.quotRate = dbManager.DataReader["QUOT_RATE"].ToString();
                    this.quotSpprice = dbManager.DataReader["QUOT_SPPRICE"].ToString();
                    this.quotDiscount = dbManager.DataReader["QUOT_DISC"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return _returnIntValue;
            }
            public static void ItemLocation_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_QTY] ORDER BY ITEM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
                //dbManager.Close();

            }


            public static void ItemMaster90_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT top 1 ITEM_NAME,ITEM_CODE FROM [YANTRA_ITEM_MAST] ORDER BY ITEM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMasterModelNo_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT top 20  ITEM_MODEL_NO,ITEM_CODE FROM [YANTRA_ITEM_MAST] ORDER BY ITEM_MODEL_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void ItemMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT top 20 ITEM_NAME,ITEM_CODE FROM [YANTRA_ITEM_MAST] ORDER BY ITEM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMaster_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST] WHERE IT_TYPE_ID=" + ItemTypeId + " ORDER BY ITEM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMaster1_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE] WHERE ITEM_CATEGORY_ID =" + ItemTypeId + " ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();

            }

            public static void ItemMaster12_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE]  ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();

            }
            public static void sample()
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ToString());
                SqlCommand cmd = new SqlCommand("select distinct(ITEM_CODE) from YANTRA_ITEM_MAST", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int i = 1;
                while (dr.Read())
                {

                    SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ToString());
                    SqlCommand cmd1 = new SqlCommand("insert into YANTRA_ITEM_QTY values(" + i + "," + Convert.ToInt32(dr["ITEM_CODE"].ToString()) + ",0,0,0)", con1);
                    con1.Open();
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                    i = i + 1;

                }
                dr.Close();
                con.Close();
            }

            public static void ItemMaster5_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_ITEM_MAST.STATUS ='1'  and  YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ItemTypeId + " order by YANTRA_ITEM_MAST.ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void ItemMaster99_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  top 400  YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ItemTypeId + " order by YANTRA_ITEM_MAST.ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void ItemMaster7_Select(Control ControlForBind, string ItemTypeId, string SalesleadId)
            {
                dbManager.Open();
              // SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO) FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND YANTRA_ENQ_DET.ENQ_ID=" + SalesleadId + " order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc  
                _commandText = string.Format("SELECT top 300 YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID inner join YANTRA_ENQ_DET on YANTRA_ENQ_DET.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ItemTypeId + " and YANTRA_ENQ_DET.ENQ_ID=" + SalesleadId + " order by YANTRA_ITEM_MAST.ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }


            public static void ItemMaster8_Select(Control ControlForBind, string ItemTypeId, string POId)
            {
                dbManager.Open();
                // SELECT ([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO) FROM [YANTRA_ENQ_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_ENQ_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND YANTRA_ENQ_DET.ENQ_ID=" + SalesleadId + " order by [YANTRA_ITEM_MAST].ITEM_MODEL_NO asc  
                _commandText = string.Format("SELECT top 300 YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID inner join YANTRA_PURCHASE_INVOICE_DET on YANTRA_PURCHASE_INVOICE_DET.ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ItemTypeId + " and YANTRA_PURCHASE_INVOICE_DET.PI_ID=" + POId + " order by YANTRA_ITEM_MAST.ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void ItemMaster6_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("select ITEM_MODEL_NO,ITEM_CODE from YANTRA_ITEM_MAST,YANTRA_LKUP_PRODUCT_COMPANY where YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ItemTypeId + " order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

            public static void ItemMasterself_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_CODE,ITEM_MODEL_NO FROM [YANTRA_ITEM_MAST] where BRAND_ID = '1' order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMaster2_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_CODE,ITEM_MODEL_NO FROM [YANTRA_ITEM_MAST] order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMaster3_Select(Control ControlForBind)
            {

                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_MODEL_NO,ITEM_CODE FROM YANTRA_ITEM_MAST order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void ItemMaster4_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT ITEM_CODE,ITEM_MODEL_NO,cast(ITEM_CODE as nvarchar(50))+'/'+ITEM_MODEL_NO as itmodelno from YANTRA_ITEM_MAST order by ITEM_MODEL_NO asc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "itmodelno", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            public static void Godown_select(Control ControlForBind)
            {
                //dbManager.Open();
                //_commandText = string.Format("SELECT *  from YANTRA_LKUP_GODOWN order by GODOWN_ID  ");

                //dbManager.ExecuteReader(CommandType.Text, _commandText);
                //if (ControlForBind is DropDownList)
                //{
                //    DropDownListBind(ControlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                //}
                 //   //dbManager.Close();

                dbManager.Open();
                _commandText = string.Format("SELECT *  from warehouse_tbl order by whname ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "whname", "wh_id");
                }
                //dbManager.Close();

            }
            public static void PartOf_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST] ORDER BY ITEM_MODEL_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }
            //------------------------------------
            public string UOMId, ItemAttachments;
            //public string ItemMaster_Save()
            //{
            //    this.ItemCode = ItemMaster_AutoGen();
            //    dbManager.Open();
            //    if (IsRecordExists("[YANTRA_ITEM_MAST]", "ITEM_MODEL_NO", this.ModelNo) == false)
            //    {
            //        if (DeleteRecord("[YANTRA_LKUP_ITEM_RATE_MASTER]", "ITEM_CODE", this.ItemCode) == true)
            //        {

            //            _commandText = string.Format("INSERT INTO [YANTRA_ITEM_MAST] VALUES ({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}','{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}','{18}','{19}',{20} )", this.ItemCode, this.ItemName, this.ItemSpec, this.ItemMinStockQty, this.ItemMaterialType, this.ItemType, this.ItemQtyInHand, this.UOMId, this.ItemAttachments, this.ItemRate, this.ItemPrincipalName, this.ItemSeries, this.ItemPurchaseSpec, this.ItemPurchaseTypeId, this.ModelNo, this.ItemCategoryId, this.BrandName, this.Color, "0", "0", "0");
            //            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            //            _commandText = string.Format("insert into [YANTRA_LKUP_ITEM_RATE_MASTER] select isnull(max(IRM_ID),0)+1,{0},'{1}','{2}' from [YANTRA_LKUP_ITEM_RATE_MASTER]", this.ItemCode, this.ItemRate, this.IFY);
            //            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);


            //            _returnStringMessage = string.Empty;
            //            if (_returnIntValue < 0 || _returnIntValue == 0)
            //            {
            //                _returnStringMessage = "Some Data Missing.";
            //            }
            //            else if (_returnIntValue > 0)
            //            {
            //                _returnStringMessage = "Data Saved Successfully";
            //            }
            //        }
            //    }
            //    else
            //    {
            //        _returnStringMessage = "Item Master Already Exists.";
            //    }
            //    //dbManager.Close();

            //    return _returnStringMessage;
            //}
            //public string ItemMaster_Update()
            //{
            //    dbManager.Open();

            //    _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_NAME='{0}',ITEM_SPEC='{1}',ITEM_MIN_STOCK_QTY='{2}',ITEM_MATERIAL_TYPE='{3}',IT_TYPE_ID='{4}',ITEM_QTY_IN_HAND='{5}',UOM_ID='{6}',ITEM_ATTACHMENTS='{7}',ITEM_RATE='{8}',ITEM_PRINCIPAL_NAME='{9}',ITEM_SERIES='{10}',ITEM_PURCHASE_SPEC='{11}',ITEM_PURCHASE_ITEM_TYPE_ID={12},ITEM_MODEL_NO ='{13}',IC_ID ='{14}',BRAND_ID ='{15}',COLOR ='{16}',PARTOF = {17} WHERE ITEM_CODE={18}", this.ItemName, this.ItemSpec, this.ItemMinStockQty, this.ItemMaterialType, this.ItemType, this.ItemQtyInHand, this.UOMId, this.ItemAttachments, this.ItemRate, this.ItemPrincipalName, this.ItemSeries, this.ItemPurchaseSpec, this.ItemPurchaseTypeId, this.ModelNo, this.ItemCategoryId, this.BrandName, this.Color, "0", this.ItemCode);
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            //    if (IsRecordExistsForRate("[YANTRA_LKUP_ITEM_RATE_MASTER]", "ITEM_CODE", this.ItemCode, "FINANCIAL_YEAR", this.IFY) == true)
            //    {
            //        _commandText = string.Format("update [YANTRA_LKUP_ITEM_RATE_MASTER] set ITEM_RATE = '{0}',FINANCIAL_YEAR = '{1}' where ITEM_CODE ={2}", this.ItemRate, this.IFY, this.ItemCode);
            //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    }
            //    else
            //    {
            //        _commandText = string.Format("insert into [YANTRA_LKUP_ITEM_RATE_MASTER] select isnull(max(IRM_ID),0)+1,{0},'{1}','{2}' from [YANTRA_LKUP_ITEM_RATE_MASTER]", this.ItemCode, this.ItemRate, this.IFY);
            //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    }
            //    _returnStringMessage = string.Empty;
            //    if (_returnIntValue < 0 || _returnIntValue == 0)
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }
            //    else if (_returnIntValue > 0)
            //    {
            //        _returnStringMessage = "Data Updated Successfully";
            //    }
            //    //dbManager.Close();


            //    return _returnStringMessage;
            //}
         

            
            public string ItemMaster_AutoGen_ItemQty()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(ITEM_QTY_ID),0)+1 FROM YANTRA_ITEM_QTY").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }
            //public void ItemMaster_Update(long item_code, long quantity, string rq)
            //{
            //    this.CpId = cp.getPresentCompanySessionValue();
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    dbManager.ExecuteNonQuery(CommandType.Text, "DELETE FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + this.CpId).ToString();
            //    this.ItemQtyId = ItemMaster_AutoGen_ItemQty();
            //    // this.CpId = cp.getPresentCompanySessionValue();
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_QTY] VALUES ({0},{1},{2},{3},{4},{5})", this.ItemQtyId, item_code, quantity, this.CpId, rq);
            //    //_commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_QTY_IN_HAND='{1}' WHERE ITEM_CODE={0}", item_code,quantity );
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            //}
            public void ItemMaster_UpdateStock(long item_code, long quantity, int rq, int gd, int CmpId, int color)
            {
                int Reserve = 0;
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int i = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(ITEM_CODE) FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + CmpId + " AND GODOWN_ID=" + gd + " AND COLOUR_ID=" + color + "").ToString());
                this.ItemQtyId = ItemMaster_AutoGen_ItemQty();
                if (i == 0)
                {
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_QTY] VALUES ({0},{1},{2},{3},{4},{5},{6})", this.ItemQtyId, item_code, quantity, CmpId, rq, gd, color);
                    //_commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_QTY_IN_HAND='{1}' WHERE ITEM_CODE={0}", item_code,quantity );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    //dbManager.Close();

                }
                else
                {
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    dbManager.ExecuteReader(CommandType.Text, "select ITEM_QTY_IN_HAND FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + CmpId + " AND GODOWN_ID=" + gd + " AND COLOUR_ID=" + color + "");
                    if (dbManager.DataReader.Read())
                    {
                        Reserve = Convert.ToInt32(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());

                    }
                    else
                    {
                        //_returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                    quantity = quantity + Reserve;
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET ITEM_QTY_IN_HAND='{0}' WHERE ITEM_CODE={1} AND CP_ID={2} AND GODOWN_ID={3} AND COLOUR_ID={4} ", quantity, item_code, CmpId, gd, color);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    //dbManager.Close();

                }
                //dbManager.Close();

            }
            public void ItemMaster_UpdateStoc(long item_code, long quantity, int gd, int CmpId, int color)
            {
                int Reserve = 0;
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int i = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(ITEM_CODE) FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + CmpId + " AND GODOWN_ID=" + gd + " AND COLOUR_ID=" + color + "").ToString());
                this.ItemQtyId = ItemMaster_AutoGen_ItemQty();
                if (i == 0)
                {
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_QTY] VALUES ({0},{1},{2},{3},{4},{5},{6})", this.ItemQtyId, item_code, quantity, CmpId, Reserve, gd, color);
                    //_commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_QTY_IN_HAND='{1}' WHERE ITEM_CODE={0}", item_code,quantity );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    //dbManager.Close();

                }
                else
                {
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    dbManager.ExecuteReader(CommandType.Text, "select ITEM_QTY_IN_HAND FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + CmpId + " AND GODOWN_ID=" + gd + " AND COLOUR_ID=" + color + "");
                    if (dbManager.DataReader.Read())
                    {
                        Reserve = Convert.ToInt32(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());

                    }
                    else
                    {
                        //_returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
					
                    quantity = quantity + Reserve;
                    if (dbManager.Transaction == null)
                        dbManager.Open();
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET ITEM_QTY_IN_HAND='{0}' WHERE ITEM_CODE={1} AND CP_ID={2} AND GODOWN_ID={3} AND COLOUR_ID={4} ", quantity, item_code, CmpId, gd, color);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    //dbManager.Close();

                }
                //dbManager.Close();

            }
            public void ItemMaster_EditUpdateStock(string QtyId, long item_code, long quantity, string rq, int gd, int CmpId, int color)
            {
                int Reserve = 0;
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (Masters.DeleteRecord("YANTRA_ITEM_QTY", "ITEM_QTY_ID", QtyId) == true)
                {
                    int i = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(ITEM_CODE) FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + CmpId + " AND GODOWN_ID=" + gd + " AND COLOUR_ID=" + color + "").ToString());
                    this.ItemQtyId = ItemMaster_AutoGen_ItemQty();
                    if (i == 0)
                    {
                        if (dbManager.Transaction == null)
                            dbManager.Open();
                        _commandText = string.Format("INSERT INTO [YANTRA_ITEM_QTY] VALUES ({0},{1},{2},{3},{4},{5},{6})", this.ItemQtyId, item_code, quantity, CmpId, rq, gd, color);
                        //_commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_QTY_IN_HAND='{1}' WHERE ITEM_CODE={0}", item_code,quantity );
                        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                        //dbManager.Close();

                    }
                    else
                    {
                        if (dbManager.Transaction == null)
                            dbManager.Open();
                        dbManager.ExecuteReader(CommandType.Text, "select ITEM_QTY_IN_HAND FROM [YANTRA_ITEM_QTY]  WHERE ITEM_CODE=" + item_code + " AND CP_ID=" + CmpId + " AND GODOWN_ID=" + gd + " AND COLOUR_ID=" + color + "");
                        if (dbManager.DataReader.Read())
                        {
                            Reserve = Convert.ToInt32(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());

                        }
                        else
                        {
                            //_returnIntValue = 0;
                        }
                        dbManager.DataReader.Close();
						//dbManager.Close();
                        quantity = quantity + Reserve;
                        if (dbManager.Transaction == null)
                            dbManager.Open();
                        _commandText = string.Format("UPDATE [YANTRA_ITEM_QTY] SET ITEM_QTY_IN_HAND='{0}' WHERE ITEM_CODE={1} AND CP_ID={2} AND GODOWN_ID={3} AND COLOUR_ID={4} ", quantity, item_code, CmpId, gd, color);
                        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                        //dbManager.Close();


                    }
                }
                //dbManager.Close();

            }
            public string stockItemMaster_Delete()
            {
                Masters.BeginTransaction();

                if (DeleteRecord("[YANTRA_ITEM_QTY]", "ITEM_QTY_ID", this.ItemCode) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Insert("Stock Item Details", "22");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            //public string ItemMaster_Delete()
            //{
            //    Masters.BeginTransaction();

            //    if (DeleteRecord("[YANTRA_ITEM_MAST]", "ITEM_CODE", this.ItemCode) == true)
            //    {
            //        if (DeleteRecord("[YANTRA_LKUP_ITEM_RATE_MASTER]", "ITEM_CODE", this.ItemCode) == true)
            //        {
            //            Masters.CommitTransaction();
            //            _returnStringMessage = "Data Deleted Successfully";
            //        }
            //    }
            //    else
            //    {
            //        Masters.RollBackTransaction();
            //        _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            //    }
            //    return _returnStringMessage;
            //}
            //public int ItemMaster_Select(string ItemCode)
            //{

            //    dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM]  WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_LKUP_UOM].UOM_ID=[YANTRA_ITEM_MAST].UOM_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ORDER BY ITEM_CODE");


            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
            //        this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
            //        this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
            //        this.ItemMinStockQty = dbManager.DataReader["ITEM_MIN_STOCK_QTY"].ToString();
            //        this.ItemMaterialType = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
            //        this.ItemType = dbManager.DataReader["IT_TYPE_ID"].ToString();
            //        this.ItemQtyInHand = dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString();
            //        this.UOMId = dbManager.DataReader["UOM_ID"].ToString();
            //        this.ItemAttachments = dbManager.DataReader["ITEM_ATTACHMENTS"].ToString();

            //        _returnIntValue = 1;
            //    }
            //    else
            //    {
            //        _returnIntValue = 0;
            //    }
            //    return _returnIntValue;
            //}

            //public static void ItemMaster_Select(Control ControlForBind)
            //{
            //    dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST] ORDER BY ITEM_CODE");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "ITEM_CODE", "ITEM_CODE");
            //    }
            //}

            public static void ItemMaster_RateCalc(string Brand, string percentage, string finan)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(ITEM_CODE) FROM [YANTRA_ITEM_MAST]  WHERE BRAND_ID=" + Brand + "").ToString());
                int[] a = new int[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select ITEM_CODE FROM [YANTRA_ITEM_MAST]  WHERE BRAND_ID=" + Brand + "");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["ITEM_CODE"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
				
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select ITEM_RATE FROM [YANTRA_LKUP_ITEM_RATE_MASTER]  WHERE ITEM_CODE=" + a[x] + " AND FINANCIAL_YEAR='" + finan + "'");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["ITEM_RATE"].ToString();
                        //int temp = Convert.ToInt32(dbManager.DataReader["ITEM_RATE"].ToString());
                        decimal flt = Convert.ToDecimal(temp1);
                        //int temp = (int)flt;
                        flt = flt + (flt * int.Parse(percentage) / 100);
                        b[x] = flt;

                    }
                    dbManager.DataReader.Close();
                }

                decimal[] c = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select ITEM_SERIES FROM [YANTRA_ITEM_MAST]  WHERE ITEM_CODE=" + a[x] + "");
                    if (dbManager.DataReader.Read())
                    {
                        string tempMrp = dbManager.DataReader["ITEM_SERIES"].ToString();
                        //int temp = Convert.ToInt32(dbManager.DataReader["ITEM_RATE"].ToString());
                        decimal flt = Convert.ToDecimal(tempMrp);
                        //int temp = (int)flt;
                        flt = flt + (flt * int.Parse(percentage) / 100);
                        c[x] = flt;

                    }
                    dbManager.DataReader.Close();
                }


                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_ITEM_RATE_MASTER] SET ITEM_RATE='{0}' WHERE ITEM_CODE={1} AND FINANCIAL_YEAR='{2}'", b[x], a[x], finan);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_SERIES='{0}' WHERE ITEM_CODE={1} ", c[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);


                }
                //dbManager.Close();
            }

            public int titemcode;
            public string ItemColorDetails_save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT MAX(ITEM_CODE) FROM YANTRA_ITEM_MAST");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    titemcode = int.Parse(dbManager.DataReader[0].ToString());
                }
                dbManager.DataReader.Close();
                _commandText = string.Format("INSERT INTO YANTRA_ITEM_COLOR_DETAILS SELECT ISNULL(MAX(ITEM_MASTER_COLOR_DETAIL_ID),0)+1,{0},{1} FROM YANTRA_ITEM_COLOR_DETAILS", titemcode, detailscolorid);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Item Color Details", "23");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemColorDetails_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                               
                _commandText = string.Format("INSERT INTO YANTRA_ITEM_COLOR_DETAILS SELECT ISNULL(MAX(ITEM_MASTER_COLOR_DETAIL_ID),0)+1,{0},{1} FROM YANTRA_ITEM_COLOR_DETAILS", titemcode, detailscolorid);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Item Color Details", "23");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public int ItemColorDetails_Delete(string Itemcode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_ITEM_COLOR_DETAILS] WHERE ITEM_CODE={0}", Itemcode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();

                return _returnIntValue;
            }

            public string ItemMastercolordetails_Update()
            {
                dbManager.Open();

                //_commandText = string.Format("UPDATE [YANTRA_ITEM_COLOR_DETAILS] SET ITEM_NAME='{0}',ITEM_SPEC='{1}',ITEM_MIN_STOCK_QTY='{2}',ITEM_MATERIAL_TYPE='{3}',IT_TYPE_ID='{4}',ITEM_QTY_IN_HAND='{5}',UOM_ID='{6}',ITEM_ATTACHMENTS='{7}',ITEM_RATE='{8}',ITEM_PRINCIPAL_NAME='{9}',ITEM_SERIES='{10}',ITEM_PURCHASE_SPEC='{11}',ITEM_PURCHASE_ITEM_TYPE_ID={12},ITEM_MODEL_NO ='{13}',IC_ID ='{14}',BRAND_ID ='{15}',COLOR ='{16}',PARTOF = {17} WHERE ITEM_CODE={18}", this.ItemName, this.ItemSpec, this.ItemMinStockQty, this.ItemMaterialType, this.ItemType, this.ItemQtyInHand, this.UOMId, this.ItemAttachments, this.ItemRate, this.ItemPrincipalName, this.ItemSeries, this.ItemPurchaseSpec, this.ItemPurchaseTypeId, this.ModelNo, this.ItemCategoryId, this.BrandName, this.Color, "0", this.ItemCode);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _commandText = string.Format("INSERT INTO YANTRA_ITEM_COLOR_DETAILS SELECT ISNULL(MAX(ITEM_MASTER_COLOR_DETAIL_ID),0)+1,{0},{1} FROM YANTRA_ITEM_COLOR_DETAILS", detailsitemcode, detailscolorid);
                // _commandText = string.Format("update [YANTRA_ITEM_COLOR_DETAILS] set COLOR_ID ={0} where ITEM_CODE ={1}", this.detailscolorid,this.ItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Item Color Details", "23");
                    
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public DataTable ItemSpare_Select(string ModelNo)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("item_code");
                dtable.Columns.Add(dcol);
                dcol = new DataColumn("item_model_no");
                dtable.Columns.Add(dcol);
                dcol = new DataColumn("item_spareDisc");
                dtable.Columns.Add(dcol);
                dbManager.Open();
                _commandText = string.Format("SELECT item_model_no,item_code,item_spareDisc  FROM YANTRA_item_spare_mast WHERE  item_SpareModelNo='{0}'", ModelNo);
                //_commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS,YANTRA_LKUP_COLOR_MAST WHERE  YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE={0}", ItemCode);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    drow["item_model_no"] = dbManager.DataReader[0].ToString();
                    drow["item_code"] = dbManager.DataReader[1].ToString();
                    drow["item_spareDisc"] = dbManager.DataReader[2].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return dtable;
            }
            #region Itemcolor Select
            public DataTable ItemColor_Select(int ItemCode)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("COLOR_ID");
                dtable.Columns.Add(dcol);

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS WHERE  ITEM_CODE={0}", ItemCode);
                //_commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS,YANTRA_LKUP_COLOR_MAST WHERE  YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE={0}", ItemCode);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    drow["COLOR_ID"] = dbManager.DataReader[2].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return dtable;

            }
            #endregion

            #region Itemcolor Select for Item History
            public DataTable HistItemColor_Select(int ItemCode)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("COLOR_ID");
                dtable.Columns.Add(dcol);

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_LKUP_COLOR_MAST as a inner join YANTRA_ITEM_COLOR_DETAILS as b on a.COLOUR_ID=b.COLOR_ID WHERE  ITEM_CODE={0}", ItemCode);
                //_commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS,YANTRA_LKUP_COLOR_MAST WHERE  YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE={0}", ItemCode);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    drow["COLOR_ID"] = dbManager.DataReader[1].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                return dtable;

            }
            #endregion

            #region chkbox list fill
            public static void CheckboxListWithStatement(CheckBoxList cblName, string command)
            {
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, command);
                cblName.Items.Clear();
                while (dbManager.DataReader.Read())
                {
                    cblName.Items.Add(new ListItem(dbManager.DataReader[1].ToString(), dbManager.DataReader[0].ToString()));

                }
                dbManager.DataReader.Close();
				//dbManager.Close();
				
            }
            #endregion

            public string Item_Spare_Save()
            {
                this.spItemCode = ItemMaster_AutoGen1();
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_ITEM_SPARE_MAST]", "Item_SpareModelNo", this.SpModelNo) == false)
                //{
                    _commandText = string.Format("Insert into YANTRA_ITEM_SPARE_MAST values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')", this.spItemCode, this.SpModelNo, this.SpDisc, this.spImage, this.spImageId, this.ItemCode, this.Item_Path, this.ModelNo);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Master Spare Details", "27");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Item Already Exists";
                //}
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string ItemMaster_Save()
            {
                this.ItemCode = ItemMaster_AutoGen();
                this.Barcode = this.ItemCode;
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_MAST]", "ITEM_MODEL_NO", this.ModelNo,"Status","1") == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_MAST] VALUES ({0},'{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}',{13},'{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}' )", this.ItemCode, this.ItemName, this.ItemSpec, this.Materialtype, this.ItemtypeId, this.Uomid, this.Principalname, this.Itemseries, this.Purchasespec, this.ModelNo, this.IcId, this.Brandid, this.Barcode, '1', DateTime.Now.ToString(), DateTime.Now.ToString(), this.HSN_Code, this.Remarks, this.GST_Tax, this.Prepared_By, this.Prepared_By, "");
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    //_commandText = string.Format("insert into [YANTRA_ITEM_RSP] select isnull(max(Item_Rsp_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_RSP]", this.rsp,this.ItemCode, this.financialyear, DateTime.Now.ToString());
                    //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    //_commandText = string.Format("insert into [YANTRA_ITEM_MRP] select isnull(max(Item_Mrp_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_MRP]", this.mrp, this.ItemCode, this.financialyear, DateTime.Now.ToString());
                    //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    //_commandText = string.Format("insert into [YANTRA_ITEM_ROUNDPRICE] select isnull(max(Item_RoundPrice_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_ROUNDPRICE]", this.roundprice, this.ItemCode, this.financialyear, DateTime.Now.ToString());
                    //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Master Details", "27");

                    }

                }
                else
                {
                    _returnStringMessage = "Item Already Exists";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemAttachment_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ITEM_ATTACHMENTS] SELECT ISNULL(MAX(Item_attachmentId),0)+1,'{0}',{1},'{2}','{3}' FROM [YANTRA_ITEM_ATTACHMENTS]", this.Itemattachment, this.ItemCode, this.attachmentdate,this.Item_Path );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                //dbManager.Close();

                return _returnStringMessage;

            }
            public string Item_Path;
            public string ItemImage_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ITEM_IMAGE] SELECT ISNULL(MAX(Item_Image_Id),0)+1,{0},'{1}','{2}','{3}' FROM [YANTRA_ITEM_IMAGE]", this.ItemCode, this.ItemImage, this.ItemDate,this.Item_Path);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string SpareImage_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ITEM_ATTACHMENTS] SELECT ISNULL(MAX(Item_attachmentId),0)+1,'{0}','{1}','{2}','{3}' FROM [YANTRA_ITEM_ATTACHMENTS]", this.spImage , this.ItemCode , this.Specdate, this.Item_Path);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string SpecImage_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ITEM_SPECIFICATION_IMAGE] SELECT ISNULL(MAX(Item_Specification_Id),0)+1,{0},'{1}','{2}','{3}' FROM [YANTRA_ITEM_SPECIFICATION_IMAGE]", this.ItemCode, this.ItemSpec, this.Specdate, this.Item_Path);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemMaster_Update()
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_NAME='{0}',ITEM_SPEC='{1}',ITEM_MATERIAL_TYPE='{2}',IT_TYPE_ID={3},UOM_ID={4},ITEM_PRINCIPAL_NAME='{5}',ITEM_SERIES='{6}',ITEM_PURCHASE_SPEC='{7}',ITEM_MODEL_NO ='{8}',IC_ID ={9},BRAND_ID ={10},ITEM_BARCODE = '{11}',dt_updated = '{13}',HSN_Code='{14}',[GST Tax]='{15}',	[F1]='{16}',Remarks='{17}' ,F2='{18}' WHERE ITEM_CODE={12}", this.ItemName, this.ItemSpec, this.Materialtype, this.ItemtypeId, this.Uomid, this.Principalname, this.Itemseries, this.Purchasespec, this.ModelNo, this.IcId, this.Brandid, this.ItemCode, this.ItemCode, DateTime.Now.ToString(), this.HSN_Code,  this.GST_Tax, this.Prepared_By,this.Remarks,this.F2);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Item Master Details", "27");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }


            public string ItemMasterStatus_Update()
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET STATUS = '0' WHERE ITEM_CODE={1}", this.Status,this.ItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Item Master Status Details", "27");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }




            public string ItemMaster_Delete()
            {
              //  Masters.BeginTransaction();

                if (DeleteRecord("[YANTRA_ITEM_MAST]", "ITEM_CODE", this.ItemCode) == true)
                {

                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Master Details", "27");


                }
                else
                {
                  //  Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public int ItemMaster_Select1(string ItemCode)
            {
                dbManager.Open();
                try
                {
                    _commandText = string.Format("SELECT top 1 * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY],YANTRA_ITEM_IMAGE,YANTRA_ITEM_SPECIFICATION_IMAGE " +
                                " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID and " +
                                " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID and YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_IMAGE].[Item_Code] and YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_SPECIFICATION_IMAGE].[Item_Code] and [YANTRA_ITEM_MAST].ITEM_MODEL_NO='" + ItemCode + "' ");

                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                        this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
                        this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
                        this.Materialtype = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
                        this.Uomid = dbManager.DataReader["UOM_ID"].ToString();
                        this.ItemtypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
                        this.ItemUOMShort = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                        // this.mrp = dbManager.DataReader["MRP"].ToString();
                        this.Principalname = dbManager.DataReader["ITEM_PRINCIPAL_NAME"].ToString();
                        this.Itemseries = dbManager.DataReader["ITEM_SERIES"].ToString();
                        this.Purchasespec = dbManager.DataReader["ITEM_PURCHASE_SPEC"].ToString();
                        this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                        this.IcId = dbManager.DataReader["IC_ID"].ToString();
                        this.Brandid = dbManager.DataReader["BRAND_ID"].ToString();
                        this.Barcode = dbManager.DataReader["ITEM_BARCODE"].ToString();
                        this.ItemCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
                        this.BrandProductName = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                        this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                        this.ItemImage = dbManager.DataReader["Item_Image"].ToString();
                        this.itemSpecifcation = dbManager.DataReader["Item_Specification_Image"].ToString();

                        this.HSN_Code = dbManager.DataReader["HSN_Code"].ToString();
                        this.GST_Tax = dbManager.DataReader["GST Tax"].ToString();
                        this.Remarks = dbManager.DataReader["Remarks"].ToString();
                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();

                }
                catch
                {

                }
                finally
                {


                }
                //dbManager.Close();

                return _returnIntValue;

            }
            public void SpareDetails_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from yantra_item_spare_mast where ITEM_CODE=" + ItemCode + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("Item_Model_No");
                SalesOrderProducts.Columns.Add(col);

                col = new DataColumn("Item_SpareModelNo");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("Item_SpareDisc");
                SalesOrderProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["Item_Model_No"] = dbManager.DataReader["Item_Model_No"].ToString();

                    dr["Item_SpareModelNo"] = dbManager.DataReader["Item_SpareModelNo"].ToString();
                    dr["Item_SpareDisc"] = dbManager.DataReader["Item_SpareDisc"].ToString();
                    SalesOrderProducts.Rows.Add(dr);
                }
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }
            public int ItemMaster_Select(string ItemCode)
            {
                dbManager.Open();
                try
                {
                    _commandText = string.Format("SELECT top 1 * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY],YANTRA_ITEM_IMAGE,YANTRA_ITEM_SPECIFICATION_IMAGE " +
                                " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID and " +
                                " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID and YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_IMAGE].[Item_Code] and YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_SPECIFICATION_IMAGE].[Item_Code] and [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ");

                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                        this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
                        this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
                        this.Materialtype = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
                        this.Uomid = dbManager.DataReader["UOM_ID"].ToString();
                        this.ItemtypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
                        this.ItemUOMShort = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                       // this.mrp = dbManager.DataReader["MRP"].ToString();
                        this.Principalname = dbManager.DataReader["ITEM_PRINCIPAL_NAME"].ToString();
                        this.Itemseries = dbManager.DataReader["ITEM_SERIES"].ToString();
                        this.Purchasespec = dbManager.DataReader["ITEM_PURCHASE_SPEC"].ToString();
                        this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                        this.IcId = dbManager.DataReader["IC_ID"].ToString();
                        this.Brandid = dbManager.DataReader["BRAND_ID"].ToString();
                        this.Barcode = dbManager.DataReader["ITEM_BARCODE"].ToString();
                        this.ItemCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
                        this.BrandProductName = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                        this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                        this.ItemImage = dbManager.DataReader["Item_Image"].ToString();
                        this.itemSpecifcation = dbManager.DataReader["Item_Specification_Image"].ToString();

                        this.HSN_Code = dbManager.DataReader["HSN_Code"].ToString();
                        this.GST_Tax = dbManager.DataReader["GST Tax"].ToString(); 
                        this.Remarks = dbManager.DataReader["Remarks"].ToString();
                        this.F2 = dbManager.DataReader["F2"].ToString();

                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();

                }
                catch
                {

                }
                finally
                {


                }
                //dbManager.Close();

                return _returnIntValue;

            }

            public int ItemMasterEdit_Select(string ItemCode)
            {
                dbManager.Open();
                try
                {
                    _commandText = string.Format("SELECT  * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY],YANTRA_ITEM_IMAGE,YANTRA_ITEM_SPECIFICATION_IMAGE " +
                                " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID and " +
                                " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID and YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_IMAGE].[Item_Code] and YANTRA_ITEM_MAST.ITEM_CODE = [YANTRA_ITEM_SPECIFICATION_IMAGE].[Item_Code] and [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ");

                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                        this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
                        this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
                        this.Materialtype = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
                        this.Uomid = dbManager.DataReader["UOM_ID"].ToString();
                        this.ItemtypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
                        // this.mrp = dbManager.DataReader["MRP"].ToString();
                        this.Principalname = dbManager.DataReader["ITEM_PRINCIPAL_NAME"].ToString();
                        this.Itemseries = dbManager.DataReader["ITEM_SERIES"].ToString();
                        this.Purchasespec = dbManager.DataReader["ITEM_PURCHASE_SPEC"].ToString();
                        this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                        this.IcId = dbManager.DataReader["IC_ID"].ToString();
                        this.Brandid = dbManager.DataReader["BRAND_ID"].ToString();
                        this.Barcode = dbManager.DataReader["ITEM_BARCODE"].ToString();
                        this.ItemCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
                        this.BrandProductName = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                        this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                        this.ItemImage = dbManager.DataReader["Item_Image"].ToString();
                        this.itemSpecifcation = dbManager.DataReader["Item_Specification_Image"].ToString();

                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();

                }
                catch
                {

                }
                finally
                {


                }
                //dbManager.Close();

                return _returnIntValue;

            }


            public int ItemMasterStock_Select(string ItemCode)
            {
                dbManager.Open();
                try
                {
                    _commandText = string.Format("SELECT top 1 * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY] " +
                                " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID and " +
                                " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID  and [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ");

                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (dbManager.DataReader.Read())
                    {
                        this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                        this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
                        this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
                        this.Materialtype = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
                        this.Uomid = dbManager.DataReader["UOM_ID"].ToString();
                        this.ItemtypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
                        // this.mrp = dbManager.DataReader["MRP"].ToString();
                        this.Principalname = dbManager.DataReader["ITEM_PRINCIPAL_NAME"].ToString();
                        this.Itemseries = dbManager.DataReader["ITEM_SERIES"].ToString();
                        this.Purchasespec = dbManager.DataReader["ITEM_PURCHASE_SPEC"].ToString();
                        this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                        this.IcId = dbManager.DataReader["IC_ID"].ToString();
                        this.Brandid = dbManager.DataReader["BRAND_ID"].ToString();
                        this.Barcode = dbManager.DataReader["ITEM_BARCODE"].ToString();
                        this.ItemCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
                        this.BrandProductName = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                        this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();

                        _returnIntValue = 1;
                    }
                    else
                    {
                        _returnIntValue = 0;
                    }
                    dbManager.DataReader.Close();

                }
                catch
                {

                }
                finally
                {


                }
                //dbManager.Close();

                return _returnIntValue;

            }



        }


        ///////////Item Master New/////////////

        //public class ItemMaster
        //{
        //    #region New
        //    public string ItemCode, ItemName, ItemSpec, Materialtype, ItemtypeId, Uomid, Principalname, Itemseries, Purchasespec, ModelNo, IcId, Brandid, financialyear, rsp, mrp, roundprice, Barcode;
        //    public string detailscolorid, detailsitemcode;
        //    public string Itemattachment, attachmentdate;
        //    public string ItemImage, ItemDate;
        //    public string itemSpecifcation, Specdate;
        //    public string ItemMaster_AutoGen()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(ITEM_CODE),0)+1 FROM YANTRA_ITEM_MAST").ToString());
        //        return _returnIntValue.ToString();
        //    }
        //    public string ItemMaster_Save()
        //    {
        //        this.ItemCode = ItemMaster_AutoGen();
        //        this.Barcode = this.ItemCode;
        //        dbManager.Open();
        //        if (IsRecordExists("[YANTRA_ITEM_MAST]", "ITEM_MODEL_NO", this.ModelNo) == false)
        //        {


        //            _commandText = string.Format("INSERT INTO [YANTRA_ITEM_MAST] VALUES ({0},'{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}' )", this.ItemCode, this.ItemName, this.ItemSpec, this.Materialtype, this.ItemtypeId, this.Uomid, this.Principalname, this.Itemseries, this.Purchasespec, this.ModelNo, this.IcId, this.Brandid, this.Barcode);
        //            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //            //_commandText = string.Format("insert into [YANTRA_ITEM_RSP] select isnull(max(Item_Rsp_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_RSP]", this.rsp,this.ItemCode, this.financialyear, DateTime.Now.ToString());
        //            //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //            //_commandText = string.Format("insert into [YANTRA_ITEM_MRP] select isnull(max(Item_Mrp_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_MRP]", this.mrp, this.ItemCode, this.financialyear, DateTime.Now.ToString());
        //            //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //            //_commandText = string.Format("insert into [YANTRA_ITEM_ROUNDPRICE] select isnull(max(Item_RoundPrice_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_ROUNDPRICE]", this.roundprice, this.ItemCode, this.financialyear, DateTime.Now.ToString());
        //            //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //            _returnStringMessage = string.Empty;
        //            if (_returnIntValue < 0 || _returnIntValue == 0)
        //            {
        //                _returnStringMessage = "Some Data Missing.";
        //            }
        //            else if (_returnIntValue > 0)
        //            {
        //                _returnStringMessage = "Data Saved Successfully";
        //            }

        //        }
        //        else
        //        {
        //            _returnStringMessage = "Item Already Exists.";
        //        }
        //        return _returnStringMessage;
        //    }

        //    public string ItemAttachment_Save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_ITEM_ATTACHMENTS] SELECT ISNULL(MAX(Item_attachmentId),0)+1,'{0}',{1},'{2}' FROM [YANTRA_ITEM_ATTACHMENTS]", this.Itemattachment, this.ItemCode, this.attachmentdate);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        return _returnStringMessage;

        //    }
        //    public string ItemImage_Save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_ITEM_IMAGE] SELECT ISNULL(MAX(Item_Image_Id),0)+1,{0},'{1}','{2}' FROM [YANTRA_ITEM_IMAGE]", this.ItemCode, this.ItemImage, this.ItemDate);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        return _returnStringMessage;
        //    }
        //    public string SpecImage_Save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO [YANTRA_ITEM_SPECIFICATION_IMAGE] SELECT ISNULL(MAX(Item_Specification_Id),0)+1,{0},'{1}','{2}' FROM [YANTRA_ITEM_SPECIFICATION_IMAGE]", this.ItemCode, this.ItemSpec, this.Specdate);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        _returnStringMessage = string.Empty;
        //        return _returnStringMessage;
        //    }


        //    public int titemcode;
        //    public string ItemColorDetails_save()
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("INSERT INTO YANTRA_ITEM_COLOR_DETAILS SELECT ISNULL(MAX(ITEM_MASTER_COLOR_DETAIL_ID),0)+1,{0},{1} FROM YANTRA_ITEM_COLOR_DETAILS", this.ItemCode, this.detailscolorid);
        //        dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

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

        //    public int ItemColorDetails_Delete(string Itemcode)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        _commandText = string.Format("DELETE FROM [YANTRA_ITEM_COLOR_DETAILS] WHERE ITEM_CODE={0}", Itemcode);
        //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        return _returnIntValue;
        //    }

        //    #region Itemcolor Select
        //    public DataTable ItemColor_Select(int ItemCode)
        //    {
        //        DataTable dtable = new DataTable();
        //        DataColumn dcol = new DataColumn();
        //        dcol = new DataColumn("COLOR_ID");
        //        dtable.Columns.Add(dcol);

        //        dbManager.Open();
        //        _commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS WHERE  ITEM_CODE={0}", ItemCode);
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        while (dbManager.DataReader.Read())
        //        {
        //            DataRow drow = dtable.NewRow();
        //            drow["COLOR_ID"] = dbManager.DataReader[2].ToString();
        //            dtable.Rows.Add(drow);

        //        }
        //        dbManager.DataReader.Close();
        //        return dtable;

        //    }
        //    #endregion

        //    public string ItemMaster_Update()
        //    {
        //        dbManager.Open();

        //        _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET ITEM_NAME='{0}',ITEM_SPEC='{1}',ITEM_MATERIAL_TYPE='{2}',IT_TYPE_ID={3},UOM_ID={4},ITEM_PRINCIPAL_NAME='{5}',ITEM_SERIES='{6}',ITEM_PURCHASE_SPEC='{7}',ITEM_MODEL_NO ='{8}',IC_ID ={9},BRAND_ID ={10},ITEM_BARCODE = '{11}' WHERE ITEM_CODE={12}", this.ItemName, this.ItemSpec, this.Materialtype, this.ItemtypeId, this.Uomid, this.Principalname, this.Itemseries, this.Purchasespec, this.ModelNo, this.IcId, this.Brandid, this.ItemCode, this.ItemCode);
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

        //    public string ItemMaster_Delete()
        //    {
        //        Masters.BeginTransaction();

        //        if (DeleteRecord("[YANTRA_ITEM_MAST]", "ITEM_CODE", this.ItemCode) == true)
        //        {

        //            _returnStringMessage = "Data Deleted Successfully";

        //        }
        //        else
        //        {
        //            Masters.RollBackTransaction();
        //            _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

        //        }
        //        return _returnStringMessage;
        //    }

        //    public int ItemMaster_Select(string ItemCode)
        //    {
        //        dbManager.Open();
        //        try
        //        {
        //            _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_CATEGORY],[YANTRA_LKUP_PRODUCT_COMPANY] " +
        //                        " WHERE [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID and " +
        //                        " [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_ITEM_MAST.IC_ID = YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID AND YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST.BRAND_ID and [YANTRA_ITEM_MAST].ITEM_CODE=" + ItemCode + " ");

        //            dbManager.ExecuteReader(CommandType.Text, _commandText);
        //            if (dbManager.DataReader.Read())
        //            {
        //                this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
        //                this.ItemName = dbManager.DataReader["ITEM_NAME"].ToString();
        //                this.ItemSpec = dbManager.DataReader["ITEM_SPEC"].ToString();
        //                this.Materialtype = dbManager.DataReader["ITEM_MATERIAL_TYPE"].ToString();
        //                this.Uomid = dbManager.DataReader["UOM_ID"].ToString();
        //                this.ItemtypeId = dbManager.DataReader["IT_TYPE_ID"].ToString();
        //                this.mrp = dbManager.DataReader["MRP"].ToString();
        //                this.Principalname = dbManager.DataReader["ITEM_PRINCIPAL_NAME"].ToString();
        //                this.Itemseries = dbManager.DataReader["ITEM_SERIES"].ToString();
        //                this.Purchasespec = dbManager.DataReader["ITEM_PURCHASE_SPEC"].ToString();
        //                this.ModelNo = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
        //                this.IcId = dbManager.DataReader["IC_ID"].ToString();
        //                this.Brandid = dbManager.DataReader["BRAND_ID"].ToString();
        //                this.financialyear = dbManager.DataReader["FINANCIAL_YEAR"].ToString();
        //                this.roundprice = dbManager.DataReader["ROUND_PRICE"].ToString();
        //                this.rsp = dbManager.DataReader["RSP"].ToString();

        //                _returnIntValue = 1;
        //            }
        //            else
        //            {
        //                _returnIntValue = 0;
        //            }
        //            dbManager.DataReader.Close();

        //        }
        //        catch
        //        {

        //        }
        //        finally
        //        {


        //        }
        //        return _returnIntValue;

        //    }

        //    public static void ItemMaster_RateCalc(string Brand, string percentage, string finan)
        //    {
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(ITEM_CODE) FROM [YANTRA_ITEM_MAST]  WHERE BRAND_ID=" + Brand + "").ToString());
        //        int[] a = new int[sizeOfArray];
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        dbManager.ExecuteReader(CommandType.Text, "select ITEM_CODE FROM [YANTRA_ITEM_MAST]  WHERE BRAND_ID=" + Brand + "");
        //        int i = 0;
        //        while (dbManager.DataReader.Read())
        //        {
        //            a[i] = Convert.ToInt32(dbManager.DataReader["ITEM_CODE"].ToString());
        //            i++;
        //        }
        //        dbManager.DataReader.Close();

        //        decimal[] c = new decimal[sizeOfArray];
        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        for (int x = 0; x < a.Length; x++)
        //        {
        //            dbManager.ExecuteReader(CommandType.Text, "select MRP FROM [YANTRA_ITEM_MAST]  WHERE ITEM_CODE=" + a[x] + "");
        //            if (dbManager.DataReader.Read())
        //            {
        //                string tempMrp = dbManager.DataReader["MRP"].ToString();
        //                decimal flt = Convert.ToDecimal(tempMrp);
        //                flt = flt + (flt * int.Parse(percentage) / 100);
        //                c[x] = flt;

        //            }
        //            dbManager.DataReader.Close();
        //        }


        //        if (dbManager.Transaction == null)
        //            dbManager.Open();
        //        for (int x = 0; x < c.Length; x++)
        //        {
        //            _commandText = string.Format("insert into [YANTRA_ITEM_MRP] select isnull(max(Item_Mrp_Id),0)+1,'{0}',{1},'{2}','{3}' from [YANTRA_ITEM_MRP]", c[x], a[x], finan, DateTime.Now.ToString());
        //            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

        //            _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET MRP='{0}' and FINANCIAL_YEAR = '{1}' WHERE ITEM_CODE={2} ", c[x], finan, a[x]);
        //            dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        //        }

        //    }

        //    #endregion

        //     public static void ItemMaster_Select(Control ControlForBind)
        //    {
        //        dbManager.Open();
        //        _commandText = string.Format("SELECT ITEM_NAME,ITEM_CODE FROM [YANTRA_ITEM_MAST] ORDER BY ITEM_NAME");
        //        dbManager.ExecuteReader(CommandType.Text, _commandText);
        //        if (ControlForBind is DropDownList)
        //        {
        //            DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
        //        }
        //    }

        //     public static void ItemMaster12_Select(Control ControlForBind)
        //     {
        //         dbManager.Open();
        //         _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE]  ORDER BY IT_TYPE");
        //         dbManager.ExecuteReader(CommandType.Text, _commandText);
        //         if (ControlForBind is DropDownList)
        //         {
        //             DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
        //         }
        //     }

        //     public static void ItemMaster1_Select(Control ControlForBind, string ItemTypeId)
        //     {
        //         dbManager.Open();
        //         _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE] WHERE ITEM_CATEGORY_ID =" + ItemTypeId + " ORDER BY IT_TYPE");
        //         dbManager.ExecuteReader(CommandType.Text, _commandText);
        //         if (ControlForBind is DropDownList)
        //         {
        //             DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
        //         }
        //     }

        //}




        //Methods For Industry Type Master Form
        public class IndustryType
        {
            public string IndTypeId, IndType, IndDesc;

            public IndustryType()
            { }

            public string IndustryType_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_INDUSTRY_TYPE]", "IND_TYPE", this.IndType) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_INDUSTRY_TYPE] SELECT ISNULL(MAX(IND_TYPE_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_INDUSTRY_TYPE]", this.IndType, this.IndDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Industry Details", "24");

                    }
                }
                else
                {
                    _returnStringMessage = "Industry Type Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string IndustryType_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_INDUSTRY_TYPE]", "IND_TYPE", this.IndType) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_INDUSTRY_TYPE] SET IND_TYPE='{0}',IND_DESC='{1}' WHERE IND_TYPE_ID={2}", this.IndType, this.IndDesc, this.IndTypeId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Industry Details", "24");

                    }
                }
                else
                {
                    _returnStringMessage = "Industry Type Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string IndustryType_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_INDUSTRY_TYPE]", "IND_TYPE_ID", this.IndTypeId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Industry Details", "24");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }
                return _returnStringMessage;
            }

            public static void IndustryType_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_INDUSTRY_TYPE] ORDER BY IND_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IND_TYPE", "IND_TYPE_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods for Review Questions Form

        public class ReviewQuestion
        {
            public string ReviewQuestionId, ReviewQuestionName, ItemDesc, ReviewCategoryId,Status;
            public ReviewQuestion()
            { }

            public string ReviewQuestion_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_REVIEW_QUESTIONS]", "RV_Question", this.ReviewQuestionName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_REVIEW_QUESTIONS] SELECT ISNULL(MAX(RV_Question_ID),0)+1,'{0}','{1}','{2}','{3}' FROM [YANTRA_LKUP_REVIEW_QUESTIONS]", this.ReviewCategoryId, this.ReviewQuestionName , this.ItemDesc, this.Status);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Type Details", "28");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Type Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

           
            public string ReviewQuestion_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_REVIEW_QUESTIONS]", "RV_Question", this.ReviewQuestionName) == false)
                //{
                _commandText = string.Format("UPDATE [YANTRA_LKUP_REVIEW_QUESTIONS] SET RV_Question='{0}',IT_DESC='{1}',RV_CAT_ID={2} WHERE RV_Question_ID={3}", this.ReviewQuestionName, this.ItemDesc, this.ReviewCategoryId, this.ReviewQuestionId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Item Type Details", "28");

                }
                //}
                //else
                //{
                //    _returnStringMessage = "Item Type Name Already Exists.";
                //}
                //dbManager.Close();

                return _returnStringMessage;
            }

         
            public string ReviewQuestion_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_REVIEW_QUESTIONS]", "RV_Question_ID", this.ReviewQuestionId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Type Details", "28");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            
            public static void ReviewQuestion_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_REVIEW_CATEGORY] ORDER BY RV_CAT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "RV_CAT_NAME", "RV_CAT_ID");
                }
                //dbManager.Close();

            }
          
            public static void ReviewQuestionCategory_Select(Control ControlForBind, string ReviewQuestionId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_REVIEW_QUESTIONS] WHERE RV_CAT_ID =" + ReviewQuestionId + " ORDER BY RV_Question");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "RV_Question", "RV_Question_ID");
                }
                //dbManager.Close();

            }
           
           

        }

        //Methods For Item Type Form
        public class ItemType
        {
            public string ItemTypeId, ItemTypeName, ItemDesc, ItemCategoryId;
            public ItemType()
            { }

            public string ItemType_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ITEM_TYPE]", "IT_TYPE", this.ItemTypeName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ITEM_TYPE] SELECT ISNULL(MAX(IT_TYPE_ID),0)+1,'{0}',{1},'{2}' FROM [YANTRA_LKUP_ITEM_TYPE]", this.ItemTypeName, this.ItemCategoryId, this.ItemDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Type Details", "28");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Type Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string AssetType_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ASSET_TYPE]", "IT_TYPE", this.ItemTypeName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ASSET_TYPE] SELECT ISNULL(MAX(IT_TYPE_ID),0)+1,'{0}',{1},'{2}' FROM [YANTRA_LKUP_ASSET_TYPE]", this.ItemTypeName, this.ItemCategoryId, this.ItemDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Asset Type Details", "28");

                    }
                }
                else
                {
                    _returnStringMessage = " Asset Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemType_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_ITEM_TYPE]", "IT_TYPE", this.ItemTypeName) == false)
                //{
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_ITEM_TYPE] SET IT_TYPE='{0}',IT_DESC='{1}',ITEM_CATEGORY_ID={2} WHERE IT_TYPE_ID={3}", this.ItemTypeName, this.ItemDesc, this.ItemCategoryId, this.ItemTypeId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Item Type Details", "28");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Item Type Name Already Exists.";
                //}
                    //dbManager.Close();

                return _returnStringMessage;
            }

            public string AssetType_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_ITEM_TYPE]", "IT_TYPE", this.ItemTypeName) == false)
                //{
                _commandText = string.Format("UPDATE [YANTRA_LKUP_ASSET_TYPE] SET IT_TYPE='{0}',IT_DESC='{1}',ITEM_CATEGORY_ID={2} WHERE IT_TYPE_ID={3}", this.ItemTypeName, this.ItemDesc, this.ItemCategoryId, this.ItemTypeId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Asset Type Details", "28");

                }
                //}
                //else
                //{
                //    _returnStringMessage = "Item Type Name Already Exists.";
                //}
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string ItemType_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_ITEM_TYPE]", "IT_TYPE_ID", this.ItemTypeId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Type Details", "28");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public string AssetType_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_Asset_TYPE]", "IT_TYPE_ID", this.ItemTypeId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Asset Type Details", "28");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void ItemType_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE] ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();

            }
            public static void AssetType_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_Asset_TYPE] ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();

            }
            public static void ItemTypeCategory_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE] WHERE ITEM_CATEGORY_ID =" + ItemTypeId + " ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();

            }
            public static void AssetTypeCategory_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_Asset_TYPE] WHERE ITEM_CATEGORY_ID =" + ItemTypeId + " ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();

            }
            public static void Item_ModelNo_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST] WHERE IT_TYPE_ID =" + ItemTypeId + " ORDER BY ITEM_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();

            }

        }


        //Methods For Item Type Form
        public class PurchaseItemType
        {
            public string PurchaseItemTypeId, PurchaseItemTypeName, PurchaseItemDesc;
            public PurchaseItemType()
            { }

            public string ItemType_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PURCHASE_ITEM_TYPE]", "PIT_TYPE", this.PurchaseItemTypeName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PURCHASE_ITEM_TYPE] SELECT ISNULL(MAX(PIT_TYPE_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_PURCHASE_ITEM_TYPE]", this.PurchaseItemTypeName, this.PurchaseItemDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Type Details", "28");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Type Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemType_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_PURCHASE_ITEM_TYPE]", "PIT_TYPE", this.PurchaseItemTypeName) == false)
                //{
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_PURCHASE_ITEM_TYPE] SET PIT_TYPE='{0}',PIT_DESC='{1}' WHERE PIT_TYPE_ID={2}", this.PurchaseItemTypeName, this.PurchaseItemDesc, this.PurchaseItemTypeId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Item Type Details", "28");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Item Type Name Already Exists.";
                //}
                    //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemType_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_PURCHASE_ITEM_TYPE]", "PIT_TYPE_ID", this.PurchaseItemTypeId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Type Details", "28");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void ItemType_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_PURCHASE_ITEM_TYPE] ORDER BY PIT_TYPE_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PIT_TYPE", "PIT_TYPE_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods For Item Group form
        public class ItemGroupMaster
        {

            public string IgId, IgName, IgDesc, ItTypeId; //Item  Group 

            public ItemGroupMaster()
            { }

            public string ItemGroupMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ITEM_GROUP]", "IG_NAME", this.IgName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ITEM_GROUP] SELECT ISNULL(MAX(IG_ID),0)+1,'{0}','{1}','{2}' FROM [YANTRA_LKUP_ITEM_GROUP]", this.IgName, this.IgDesc, this.ItTypeId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Group Details", "29");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Group Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemGroupMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ITEM_GROUP]", "IG_NAME", this.IgName, "IG_ID", this.IgId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_ITEM_GROUP] SET IG_NAME='{0}',IG_DESC='{1}',IT_TYPE_ID='{2}'' WHERE IG_ID={3}", this.IgName, this.IgDesc, this.ItTypeId, this.IgId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Item Group Details", "29");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Group Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemGroupMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_ITEM_GROUP]", "IG_ID", this.IgId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Group Details", "29");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void ItemGroupMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_GROUP] ORDER BY IG_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IG_NAME", "IG_ID");
                }
                //dbManager.Close();

            }


        }

        //Methods for Shift Master Form
        public class ShiftMaster
        {
            public string ShiftId, ShiftCode, ShiftName, ShiftStartTime, ShiftEndTime, ShiftBreakDur, ShiftAvailableDur;
            public ShiftMaster()
            { }

            public static string ShiftMaster_AutoGenCode()
            {
                dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(SHIFT_ID),0)+1 FROM [YANTRA_LKUP_SHIFT]").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }

            public string ShiftMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_SHIFT]", "SHIFT_NAME", this.ShiftName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_SHIFT] SELECT ISNULL(MAX(SHIFT_ID),0)+1,'{0}','{1}','{2}','{3}','{4}' FROM [YANTRA_LKUP_SHIFT]", this.ShiftName, this.ShiftStartTime, this.ShiftEndTime, this.ShiftBreakDur, this.ShiftAvailableDur);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Shift Master Details", "25");

                    }
                }
                else
                {
                    _returnStringMessage = "Shift Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ShiftMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_SHIFT]", "SHIFT_NAME", this.ShiftName, "SHIFT_ID", this.ShiftId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_SHIFT] SET SHIFT_NAME='{0}',SHIFT_START_TIME='{1}',SHIFT_END_TIME='{2}',SHIFT_BREAK_DUR='{3}',SHIFT_AVAILABLE_DUR='{4}' WHERE SHIFT_ID={5}", this.ShiftName, this.ShiftStartTime, this.ShiftEndTime, this.ShiftBreakDur, this.ShiftAvailableDur, this.ShiftId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Shift Master Details", "25");

                    }
                }
                else
                {
                    _returnStringMessage = "Shift Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ShiftMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_SHIFT]", "SHIFT_ID", this.ShiftId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Shift Master Details", "25");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void ShiftMaster_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_SHIFT] ORDER BY SHIFT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SHIFT_NAME", "SHIFT_ID");
                }
                //dbManager.Close();

            }
        }

        //Methods for Register Master Form
        public class RegisterMaster
        {
            public string RegisterId, RegisterName, RegisterDesc;
            public RegisterMaster()
            { }

            public string RegisterMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_REGISTER_MAST]", "REGISTER_NAME", this.RegisterName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_REGISTER_MAST] SELECT ISNULL(MAX(REGISTER_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_REGISTER_MAST]", this.RegisterName, this.RegisterDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Register Master Details", "26");

                    }
                }
                else
                {
                    _returnStringMessage = "Shift Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string RegisterMaster_Update()
            {
                dbManager.Open();

                if (IsRecordExists("[YANTRA_LKUP_REGISTER_MAST]", "REGISTER_NAME", this.RegisterName) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_REGISTER_MAST] SET REGISTER_NAME='{0}',REGISTER_DESC='{1}' WHERE REGISTER_ID={2}", this.RegisterName, this.RegisterDesc, this.RegisterId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Register Master Details", "26");

                    }
                }
                else
                {
                    _returnStringMessage = "Shift Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string RegisterMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_REGISTER_MAST]", "REGISTER_ID", this.RegisterId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Register Master Details", "26");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
        }

        //Methods For EmployeeType Master Form
        public class EmployeeType
        {
            public string EmpTypeId, EmpTypeName, EmpTypeDesc;   //EmployeeType Look Up
            public EmployeeType()
            { }

            public static void EmployeeType_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_EMP_TYPE] ORDER BY EMP_TYPE_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "EMP_TYPE_NAME", "EMP_TYPE_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        //Methods For Product Master Form
        public class ProductMaster
        {
            public string ProductId, ProductCode, ProductName, ProductDesc, ProductBaseName; // Product Master
            public string EssentialId, ItemCode, EssentialCode, BrandId, ModelNo, ModelName, ITEM_PURCHASE_SPEC, ITEMESSENTIAL, Quantity,date,empname;
            public static int sp=0;
            public ProductMaster()
            { }

            public string ProductMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PRODUCT_MAST]", "PRODUCT_NAME", this.ProductName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PRODUCT_MAST] SELECT ISNULL(MAX(PRODUCT_ID),0)+1,'{0}','{1}','{2}','{3}' FROM [YANTRA_LKUP_PRODUCT_MAST]", this.ProductCode, this.ProductName, this.ProductDesc, this.ProductBaseName);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Product Details", "7");

                    }
                }
                else
                {
                    _returnStringMessage = "Product Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }


            public string ProductMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PRODUCT_MAST]", "PRODUCT_NAME", this.ProductName, "PRODUCT_ID", this.ProductId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_PRODUCT_MAST] SET PRODUCT_CODE='{0}',PRODUCT_NAME='{1}',PRODUCT_DESC='{2}',PRODUCT_BASE_NAME='{3}' WHERE PRODUCT_ID={4}", this.ProductCode, this.ProductName, this.ProductDesc, this.ProductBaseName, this.ProductId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Product Details", "7");

                    }
                }
                else
                {
                    _returnStringMessage = "Product Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }


            public string ProductMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_PRODUCT_MAST]", "PRODUCT_ID", this.ProductId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Product Details", "7");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void ProductMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_PRODUCT_MAST] ORDER BY PRODUCT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PRODUCT_NAME", "PRODUCT_ID");
                }
                //dbManager.Close();

            }

            public int ProductMaster_Change(string ProductId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_PRODUCT_MAST] WHERE PRODUCT_ID = " + ProductId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.ProductName = dbManager.DataReader["PRODUCT_NAME"].ToString();
                    this.ProductBaseName = dbManager.DataReader["PRODUCT_BASE_NAME"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                //dbManager.Close();

                return _returnIntValue;
            }

            public string ProductEsseintial_Save()
            {

                if (dbManager.Transaction == null)
                    dbManager.Open();

                if (sp == 0)
                {
                    _commandText = string.Format("DELETE FROM [YANTRA_ITEM_ESSENTIALS] WHERE BRAND_ID={0} AND ITEM_CODE={1} ", this.BrandId, this.ItemCode);
                    _returnIntValue = int.Parse(dbManager.ExecuteNonQuery(CommandType.Text, _commandText).ToString());
                    sp++;
                }
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_ITEM_ESSENTIALS] SELECT ISNULL(MAX(ITEM_ESSENTIAL_ID),0)+1,{0},{1},{2},'{3}',{4},'{5}','{6}' FROM [YANTRA_ITEM_ESSENTIALS]", this.ItemCode, this.EssentialCode, this.BrandId, this.ITEMESSENTIAL, this.Quantity,this.date,this.empname);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Product Essentials Details", "30");

                }
                // //dbManager.Close();

                return _returnStringMessage;

            }

            //public void ProductEssential1_select(string ModelNo, GridView gv)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT [YANTRA_ITEM_ESSENTIALS].ITEM_CODE,YANTRA_ITEM_ESSENTIALS.BRAND_ID,ITEM_ESSENTIAL_ID, ITEM_ESSENTIAL_CODE,ITEM_ESSENTIAL,ITEM_MODEL_NO,YANTRA_ITEM_PRICE.Item_Price,[YANTRA_ITEM_ESSENTIALS].Qty FROM YANTRA_ITEM_PRICE,[YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST],[YANTRA_LKUP_PRODUCT_COMPANY] WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID=[YANTRA_ITEM_MAST].BRAND_ID AND  YANTRA_ITEM_PRICE.Item_Code = [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE AND  [YANTRA_ITEM_ESSENTIALS].ITEM_CODE='" + ModelNo + "'  ORDER BY [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_ID DESC ");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);

            //    DataTable EnquiryInterestedProducts = new DataTable();
            //    DataColumn col = new DataColumn();
            //    col = new DataColumn("EssentialCode");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("EssentialName");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("ModelNo");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("ItemCode");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("BrandId");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("EssentialId");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("ITEM_RATE");
            //    EnquiryInterestedProducts.Columns.Add(col);

            //    col = new DataColumn("Qty");
            //    EnquiryInterestedProducts.Columns.Add(col);
            //    col = new DataColumn("Total");
            //    EnquiryInterestedProducts.Columns.Add(col);



            //    while (dbManager.DataReader.Read())
            //    {
            //        DataRow dr = EnquiryInterestedProducts.NewRow();
            //        dr["EssentialCode"] = dbManager.DataReader["ITEM_ESSENTIAL_CODE"].ToString();
            //        dr["EssentialName"] = dbManager.DataReader["ITEM_ESSENTIAL"].ToString();
            //        dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //        dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
            //        dr["BrandId"] = dbManager.DataReader["BRAND_ID"].ToString();
            //        dr["EssentialId"] = dbManager.DataReader["ITEM_ESSENTIAL_ID"].ToString();
            //        dr["ITEM_RATE"] = dbManager.DataReader["Item_Price"].ToString();

            //        dr["Qty"] = dbManager.DataReader["Qty"].ToString();
            //        //dr["Total"] = dbManager.DataReader["Total"].ToString();

            //        EnquiryInterestedProducts.Rows.Add(dr);
            //    }
            //    gv.DataSource = EnquiryInterestedProducts;
            //    gv.DataBind();
            //    dbManager.DataReader.Close();
            //    //  //dbManager.Close();
            //}

            //public int ProductEsseintial_Delete(string EssentialCode)
            //{
            //    if (DeleteRecord("[YANTRA_ITEM_ESSENTIALS]", "ITEM_ESSENTIAL_CODE", EssentialCode) == true)
            //    { _returnIntValue = 1; }
            //    else
            //    { _returnIntValue = 0; }
            //    return _returnIntValue;
            //}

            public void ProductEssential1_select(string ModelNo, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_ITEM_ESSENTIALS].ITEM_CODE,YANTRA_ITEM_ESSENTIALS.BRAND_ID,ITEM_ESSENTIAL_ID, ITEM_ESSENTIAL_CODE,ITEM_ESSENTIAL,ITEM_MODEL_NO,YANTRA_ITEM_PRICE.Item_Price,[YANTRA_ITEM_ESSENTIALS].Qty,YANTRA_ITEM_MAST.ITEM_SPEC,YANTRA_ITEM_ESSENTIALS.Date,YANTRA_ITEM_ESSENTIALS.PreparedBy FROM YANTRA_ITEM_PRICE,[YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST],[YANTRA_LKUP_PRODUCT_COMPANY] WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID=[YANTRA_ITEM_MAST].BRAND_ID AND  YANTRA_ITEM_PRICE.Item_Code = [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE AND  [YANTRA_ITEM_ESSENTIALS].ITEM_CODE='" + ModelNo + "'  ORDER BY [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable EnquiryInterestedProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("EssentialCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EssentialName");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("BrandId");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EssentialId");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ITEM_RATE");
                EnquiryInterestedProducts.Columns.Add(col);

                col = new DataColumn("Qty");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Total");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Date");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("PreparedBy");
                EnquiryInterestedProducts.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = EnquiryInterestedProducts.NewRow();
                    dr["EssentialCode"] = dbManager.DataReader["ITEM_ESSENTIAL_CODE"].ToString();
                    dr["EssentialName"] = dbManager.DataReader["ITEM_ESSENTIAL"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["BrandId"] = dbManager.DataReader["BRAND_ID"].ToString();
                    dr["EssentialId"] = dbManager.DataReader["ITEM_ESSENTIAL_ID"].ToString();
                    dr["ITEM_RATE"] = dbManager.DataReader["Item_Price"].ToString();
                    dr["Specification"] = dbManager.DataReader["ITEM_SPEC"].ToString();

                    dr["Qty"] = dbManager.DataReader["Qty"].ToString();
                    //dr["Total"] = dbManager.DataReader["Total"].ToString();
                    dr["Date"] = dbManager.DataReader["Date"].ToString();
                    dr["PreparedBy"] = dbManager.DataReader["PreparedBy"].ToString();

                    EnquiryInterestedProducts.Rows.Add(dr);
                }
                gv.DataSource = EnquiryInterestedProducts;
                gv.DataBind();
                dbManager.DataReader.Close();
                //  //dbManager.Close();
            }
            public DataTable ProductEssential1_select2(string EssModelNo, string ModelNo)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_ITEM_ESSENTIALS].ITEM_CODE,YANTRA_ITEM_ESSENTIALS.BRAND_ID,ITEM_ESSENTIAL_ID, ITEM_ESSENTIAL_CODE,ITEM_ESSENTIAL,ITEM_MODEL_NO,YANTRA_ITEM_PRICE.Item_Price,[YANTRA_ITEM_ESSENTIALS].Qty,YANTRA_ITEM_MAST.ITEM_SPEC,YANTRA_ITEM_ESSENTIALS.Date,YANTRA_ITEM_ESSENTIALS.PreparedBy FROM YANTRA_ITEM_PRICE,[YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST],[YANTRA_LKUP_PRODUCT_COMPANY] WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID=[YANTRA_ITEM_MAST].BRAND_ID AND  YANTRA_ITEM_PRICE.Item_Code = [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE AND  [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_CODE='" + EssModelNo + "' AND  [YANTRA_ITEM_ESSENTIALS].ITEM_CODE='" + ModelNo + "'  ORDER BY [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable EnquiryInterestedProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("EssentialCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EssentialName");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Specification");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ItemCode");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("BrandId");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("EssentialId");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("ITEM_RATE");
                EnquiryInterestedProducts.Columns.Add(col);

                col = new DataColumn("Qty");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Total");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("Date");
                EnquiryInterestedProducts.Columns.Add(col);
                col = new DataColumn("PreparedBy");
                EnquiryInterestedProducts.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = EnquiryInterestedProducts.NewRow();
                    dr["EssentialCode"] = dbManager.DataReader["ITEM_ESSENTIAL_CODE"].ToString();
                    dr["EssentialName"] = dbManager.DataReader["ITEM_ESSENTIAL"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["BrandId"] = dbManager.DataReader["BRAND_ID"].ToString();
                    dr["EssentialId"] = dbManager.DataReader["ITEM_ESSENTIAL_ID"].ToString();
                    dr["ITEM_RATE"] = dbManager.DataReader["Item_Price"].ToString();
                    dr["Specification"] = dbManager.DataReader["ITEM_SPEC"].ToString();

                    dr["Qty"] = dbManager.DataReader["Qty"].ToString();
                    // dr["Total"] = dbManager.DataReader["Total"].ToString();
                    dr["Date"] = dbManager.DataReader["Date"].ToString();
                    dr["PreparedBy"] = dbManager.DataReader["PreparedBy"].ToString();
                    EnquiryInterestedProducts.Rows.Add(dr);
                }
             
                return EnquiryInterestedProducts;
                dbManager.DataReader.Close();              
            }



            public string ProductEsseintial1_Delete(string EssentialCode)
            {
                //Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_ITEM_ESSENTIALS]", "ITEM_CODE", EssentialCode) == true)
                {
                  //  Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Product Essentials Details", "30");

                }
                else
                {
                    //Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }



            public int ProductEssential_Select(string EssentialCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_ESSENTIALS],[YANTRA_ITEM_MAST],[YANTRA_LKUP_PRODUCT_COMPANY] WHERE [YANTRA_ITEM_ESSENTIALS].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                            "[YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID=[YANTRA_ITEM_MAST].BRAND_ID AND [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_ID='" + EssentialCode + "' ORDER BY [YANTRA_ITEM_ESSENTIALS].ITEM_ESSENTIAL_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.BrandId = dbManager.DataReader["BRAND_ID"].ToString();
                    this.ModelNo = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.EssentialCode = dbManager.DataReader["ITEM_ESSENTIAL_CODE"].ToString();
                    this.ModelName = dbManager.DataReader["ITEM_NAME"].ToString();
                    this.ITEM_PURCHASE_SPEC = dbManager.DataReader["ITEM_SPEC"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }


        }

        //Methods For Item Details 
        public class ItemDetails
        {
            public string ItemDetId, ItemCode, ItemDetManufacturer, ItemDetMfgDate, ItemDetExpDate, ItemDetBatchNo;

            public ItemDetails()
            { }

            public string ItemDetails_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_DET]", "ITEM_CODE", this.ItemCode) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_ITEM_DET] SELECT ISNULL(MAX(ITEM_DET_ID),0)+1,'{0}','{1}','{2}','{3}','{4}' FROM [YANTRA_ITEM_DET]", this.ItemCode, this.ItemDetManufacturer, this.ItemDetMfgDate, this.ItemDetExpDate, this.ItemDetBatchNo);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Details", "31");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Details Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemDetails_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_DET]", "ITEM_CODE", this.ItemCode, "ITEM_DET_ID", this.ItemDetId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_DET] SET ITEM_CODE='{0}',ITEM_DET_MANUFACTURER='{1}',ITEM_DET_MFG_DATE='{2}',ITEM_DET_EXP_DATE='{3}',ITEM_DET_BATCH_NO='{4}' WHERE ITEM_DET_ID={5}", this.ItemCode, this.ItemDetManufacturer, this.ItemDetMfgDate, this.ItemDetExpDate, this.ItemDetBatchNo, this.ItemDetId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Item Details", "31");

                    }
                }
                else
                {
                    _returnStringMessage = "Item  Details Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemDetails_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_ITEM_DET]", "ITEM_DET_ID", this.ItemDetId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Details", "31");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public int ItemDetails_Select(string ItemDetId)
            {

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_ITEM_DET]  WHERE [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_ITEM_DET].ITEM_CODE AND [YANTRA_ITEM_DET].ITEM_DET_ID=" + ItemDetId + " ORDER BY ITEM_DET_ID");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.ItemDetId = dbManager.DataReader["ITEM_DET_ID"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.ItemDetManufacturer = dbManager.DataReader["ITEM_DET_MANUFACTURER"].ToString();

                    this.ItemDetBatchNo = dbManager.DataReader["ITEM_DET_BATCH_NO"].ToString();
                    this.ItemDetMfgDate = Convert.ToDateTime(dbManager.DataReader["ITEM_DET_MFG_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.ItemDetExpDate = Convert.ToDateTime(dbManager.DataReader["ITEM_DET_EXP_DATE"].ToString()).ToString("MM/dd/yyyy");

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }

            public static void ItemDetails_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_DET] ORDER BY ITEM_DET_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_DET_ID", "ITEM_DET_ID");
                }
                //dbManager.Close();

            }
        }

        //Mehods for Branch Name Form
        public class Branch
        {
            public static void Branch_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_BRANCH_DETAILS] ORDER BY BRANCH_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "BRANCH_NAME", "BRANCH_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        //Methods For Currency Type Master Form 
        public class CurrencyType
        {
            public string CurrencyId, CurrencyName, CurrencyFullName, CurrencyDesc;

            public CurrencyType()
            {
            }

            public string CurrencyType_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_CURRENCY_TYPE]", "CURRENCY_NAME", this.CurrencyName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_CURRENCY_TYPE] SELECT ISNULL(MAX(CURRENCY_ID),0)+1,'{0}','{1}','{2}' FROM [YANTRA_LKUP_CURRENCY_TYPE]", this.CurrencyName, this.CurrencyFullName, this.CurrencyDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Currency Type Details", "32");
                    }
                }
                else
                {
                    _returnStringMessage = "Currency Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string CurrencyType_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_CURRENCY_TYPE]", "CURRENCY_NAME", this.CurrencyName) == false)
                //{
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_CURRENCY_TYPE] SET CURRENCY_NAME='{0}',CURRENCY_FULL_NAME='{1}',CURRENCY_DESC='{2}' WHERE CURRENCY_ID={3}", this.CurrencyName, this.CurrencyFullName, this.CurrencyDesc, this.CurrencyId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Currency Type Details", "32");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Currency Name Already Exists.";
                //}
                    //dbManager.Close();

                return _returnStringMessage;
            }
            public string CurrencyType_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_CURRENCY_TYPE]", "CURRENCY_ID", this.CurrencyId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Currency Type Details", "32");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
            public static void CurrencyType_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CURRENCY_NAME,CURRENCY_ID FROM [YANTRA_LKUP_CURRENCY_TYPE] where CURRENCY_NAME is not null");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CURRENCY_NAME", "CURRENCY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }


        }

        //Methods For Company Logos
        public class CompanyLogos
        {
            public string CLId, CLCompanyName, CLDescription, CLLogos;

            public CompanyLogos()
            { }

            public string CompanyLogos_AutoGen()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CL_ID),0)+1 FROM YANTRA_LKUP_COMPANY_LOGOS").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }


            public string LogoAttachments;

            public string CompanyLogos_Save()
            {
                this.CLId = CompanyLogos_AutoGen();

                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_COMPANY_LOGOS]", "CL_COMPANY_NAME", this.CLCompanyName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_COMPANY_LOGOS] VALUES ({0},'{1}','{2}','{3}' )", this.CLId, this.CLCompanyName, this.CLDescription, this.CLLogos);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Company Logo Details", "33");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Master Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string CompanyLogos_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_COMPANY_LOGOS]", "CL_COMPANY_NAME", this.CLCompanyName, "CL_ID", this.CLId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_COMPANY_LOGOS] SET CL_COMPANY_NAME='{0}',CL_DESCRIPTION='{1}',CL_LOGOS='{2}' WHERE CL_ID={3}", this.CLCompanyName, this.CLDescription, this.LogoAttachments, this.CLId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Company Logo Details", "33");

                    }
                }
                else
                {
                    _returnStringMessage = "Item Master Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string CompanyLogos_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_COMPANY_LOGOS]", "CL_ID", this.CLId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Company Logo Details", "33");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void CompanyLogos_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_COMPANY_LOGOS] ORDER BY CL_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CL_COMPANY_NAME", "CL_ID");
                }
                //dbManager.Close();

            }

            public int CompanyLogos_Select(string CLId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_COMPANY_LOGOS] WHERE CL_ID=" + CLId + " ORDER BY CL_ID");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CLId = dbManager.DataReader["CL_ID"].ToString();
                    this.CLCompanyName = dbManager.DataReader["CL_COMPANY_NAME"].ToString();
                    this.CLDescription = dbManager.DataReader["CL_DESCRIPTION"].ToString();
                    this.LogoAttachments = dbManager.DataReader["CL_LOGOS"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }


        }

        //Methods For City Master
        public class City
        {
            public City()
            {
            }

            public static void City_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_CITY_MAST] ORDER BY CITY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CITY_NAME", "CITY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
        }

        public class Prefix
        {
            public string PF_CUSTOMERINFO, PF_SALESLEAD, PF_SALESASSIGNMENTS, PF_SALESQUOTATION, PF_SALESORDER, PF_ORDERPROFILE, PF_SALESORDERACCEPTANCE, PF_DELIVERYCHALLAN, PF_SALESINVOICE, PF_CHECKINGFORMAT, PF_PURCHASEORDERDETAILS, PF_WORKORDERDETAILS, PF_PURCHASEINVOICE, PF_SUPPLIERMASTER;
            public string PF_CLAIMFORM, PF_AGENTMASTER, PF_SD_BG, PF_FE_ORDERPROFILE, PF_SALESPAYMENTSRECEIVED, PF_SD_BG_RECEIPTS, PF_EMDSRECEIVED, PF_COMPLAINTREGISTER, PF_SERVICEASSIGNMENTS, PF_SERVICEREPORT, PF_AMC_QUOTATION, PF_AMC_ORDER, PF_AMC_ORDERACCEPTANCE, PF_AMC_ORDERPROFILE, PF_WARRANTYCLAIM, PF_SPARESQUOTATION, PF_SPARESORDER, PF_SPARESORDERPROFILE, PF_SPARESORDERACCEPTANCE, PF_AMC_INVOICE, PF_AMC_PAYMETSRECEIVED, PF_EMPLOYEEMASTER;
            public Prefix()
            { }

            public string Prefix_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_Prefix]  values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}')",
                    this.PF_CUSTOMERINFO, this.PF_SALESLEAD, this.PF_SALESASSIGNMENTS, this.PF_SALESQUOTATION, this.PF_SALESORDER, this.PF_ORDERPROFILE, this.PF_SALESORDERACCEPTANCE, this.PF_DELIVERYCHALLAN, this.PF_SALESINVOICE, this.PF_CHECKINGFORMAT, this.PF_PURCHASEORDERDETAILS, this.PF_WORKORDERDETAILS, this.PF_PURCHASEINVOICE, this.PF_SUPPLIERMASTER, this.PF_CLAIMFORM, this.PF_AGENTMASTER, this.PF_SD_BG, this.PF_FE_ORDERPROFILE, this.PF_SALESPAYMENTSRECEIVED, this.PF_SD_BG_RECEIPTS, this.PF_EMDSRECEIVED, this.PF_COMPLAINTREGISTER, this.PF_SERVICEASSIGNMENTS, this.PF_SERVICEREPORT, this.PF_AMC_QUOTATION, this.PF_AMC_ORDER, this.PF_AMC_ORDERACCEPTANCE, this.PF_AMC_ORDERPROFILE, this.PF_WARRANTYCLAIM, this.PF_SPARESQUOTATION, this.PF_SPARESORDER, this.PF_SPARESORDERPROFILE, this.PF_SPARESORDERACCEPTANCE, this.PF_AMC_INVOICE, this.PF_AMC_PAYMETSRECEIVED, this.PF_EMPLOYEEMASTER);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Prefix Details", "34");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Prefix_Update()
            {
                dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_prefix] SET PF_CUSTOMERINFO='{0}',PF_SALESLEAD='{1}',PF_SALESASSIGNMENTS='{2}',PF_SALESQUOTATION='{3}',PF_SALESORDER='{4}',PF_ORDERPROFILE='{5}',PF_SALESORDERACCEPTANCE='{6}',PF_DELIVERYCHALLAN='{7}',PF_SALESINVOICE='{8}',PF_CHECKINGFORMAT='{9}',PF_PURCHASEORDERDETAILS='{10}',PF_WORKORDERDETAILS='{11}',PF_PURCHASEINVOICE='{12}',PF_SUPPLIERMASTER='{13}' " +
                      ",PF_CLAIMFORM='{14}',PF_AGENTMASTER='{15}',PF_SD_BG='{16}',PF_FE_ORDERPROFILE='{17}',PF_SALESPAYMENTSRECEIVED='{18}',PF_SD_BG_RECEIPTS='{19}',PF_EMDSRECEIVED='{20}',PF_COMPLAINTREGISTER='{21}',PF_SERVICEASSIGNMENTS='{22}',PF_SERVICEREPORT='{23}',PF_AMC_QUOTATION='{24}',PF_AMC_ORDER='{25}',PF_AMC_ORDERACCEPTANCE='{26}',PF_AMC_ORDERPROFILE='{27}',PF_WARRANTYCLAIM='{28}',PF_SPARESQUOTATION='{29}',PF_SPARESORDER='{30}', PF_SPARESORDERPROFILE='{31}',PF_SPARESORDERACCEPTANCE='{32}',PF_AMC_INVOICE='{33}',PF_AMC_PAYMETSRECEIVED='{34}',PF_EMPLOYEEMASTER='{35}'",
                      this.PF_CUSTOMERINFO, this.PF_SALESLEAD, this.PF_SALESASSIGNMENTS, this.PF_SALESQUOTATION, this.PF_SALESORDER, this.PF_ORDERPROFILE, this.PF_SALESORDERACCEPTANCE, this.PF_DELIVERYCHALLAN, this.PF_SALESINVOICE, this.PF_CHECKINGFORMAT, this.PF_PURCHASEORDERDETAILS, this.PF_WORKORDERDETAILS, this.PF_PURCHASEINVOICE, this.PF_SUPPLIERMASTER, this.PF_CLAIMFORM, this.PF_AGENTMASTER, this.PF_SD_BG, this.PF_FE_ORDERPROFILE, this.PF_SALESPAYMENTSRECEIVED, this.PF_SD_BG_RECEIPTS, this.PF_EMDSRECEIVED, this.PF_COMPLAINTREGISTER, this.PF_SERVICEASSIGNMENTS, this.PF_SERVICEREPORT, this.PF_AMC_QUOTATION, this.PF_AMC_ORDER, this.PF_AMC_ORDERACCEPTANCE, this.PF_AMC_ORDERPROFILE, this.PF_WARRANTYCLAIM, this.PF_SPARESQUOTATION, this.PF_SPARESORDER, this.PF_SPARESORDERPROFILE, this.PF_SPARESORDERACCEPTANCE, this.PF_AMC_INVOICE, this.PF_AMC_PAYMETSRECEIVED, this.PF_EMPLOYEEMASTER);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Prefix Details", "34");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public int Prefix_Select()
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_Prefix]");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PF_CUSTOMERINFO = dbManager.DataReader["PF_CUSTOMERINFO"].ToString();
                    this.PF_SALESLEAD = dbManager.DataReader["PF_SALESLEAD"].ToString();
                    this.PF_SALESASSIGNMENTS = dbManager.DataReader["PF_SALESASSIGNMENTS"].ToString();
                    this.PF_SALESQUOTATION = dbManager.DataReader["PF_SALESQUOTATION"].ToString();
                    this.PF_SALESORDER = dbManager.DataReader["PF_SALESORDER"].ToString();
                    this.PF_ORDERPROFILE = dbManager.DataReader["PF_ORDERPROFILE"].ToString();
                    this.PF_SALESORDERACCEPTANCE = dbManager.DataReader["PF_SALESORDERACCEPTANCE"].ToString();
                    this.PF_DELIVERYCHALLAN = dbManager.DataReader["PF_DELIVERYCHALLAN"].ToString();
                    this.PF_SALESINVOICE = dbManager.DataReader["PF_SALESINVOICE"].ToString();
                    this.PF_CHECKINGFORMAT = dbManager.DataReader["PF_CHECKINGFORMAT"].ToString();
                    this.PF_PURCHASEORDERDETAILS = dbManager.DataReader["PF_PURCHASEORDERDETAILS"].ToString();
                    this.PF_WORKORDERDETAILS = dbManager.DataReader["PF_WORKORDERDETAILS"].ToString();
                    this.PF_PURCHASEINVOICE = dbManager.DataReader["PF_PURCHASEINVOICE"].ToString();
                    this.PF_SUPPLIERMASTER = dbManager.DataReader["PF_SUPPLIERMASTER"].ToString();
                    this.PF_CLAIMFORM = dbManager.DataReader["PF_CLAIMFORM"].ToString();
                    this.PF_AGENTMASTER = dbManager.DataReader["PF_AGENTMASTER"].ToString();
                    this.PF_SD_BG = dbManager.DataReader["PF_SD_BG"].ToString();
                    this.PF_FE_ORDERPROFILE = dbManager.DataReader["PF_FE_ORDERPROFILE"].ToString();
                    this.PF_SALESPAYMENTSRECEIVED = dbManager.DataReader["PF_SALESPAYMENTSRECEIVED"].ToString();
                    this.PF_SD_BG_RECEIPTS = dbManager.DataReader["PF_SD_BG_RECEIPTS"].ToString();
                    this.PF_EMDSRECEIVED = dbManager.DataReader["PF_EMDSRECEIVED"].ToString();
                    this.PF_COMPLAINTREGISTER = dbManager.DataReader["PF_COMPLAINTREGISTER"].ToString();
                    this.PF_SERVICEASSIGNMENTS = dbManager.DataReader["PF_SERVICEASSIGNMENTS"].ToString(); ;
                    this.PF_SERVICEREPORT = dbManager.DataReader["PF_SERVICEREPORT"].ToString(); ;
                    this.PF_AMC_QUOTATION = dbManager.DataReader["PF_AMC_QUOTATION"].ToString();
                    this.PF_AMC_ORDER = dbManager.DataReader["PF_AMC_ORDER"].ToString();
                    this.PF_AMC_ORDERACCEPTANCE = dbManager.DataReader["PF_AMC_ORDERACCEPTANCE"].ToString();
                    this.PF_AMC_ORDERPROFILE = dbManager.DataReader["PF_AMC_ORDERPROFILE"].ToString();
                    this.PF_WARRANTYCLAIM = dbManager.DataReader["PF_WARRANTYCLAIM"].ToString();
                    this.PF_SPARESQUOTATION = dbManager.DataReader["PF_SPARESQUOTATION"].ToString();
                    this.PF_SPARESORDER = dbManager.DataReader["PF_SPARESORDER"].ToString();
                    this.PF_SPARESORDERPROFILE = dbManager.DataReader["PF_SPARESORDERPROFILE"].ToString();
                    this.PF_SPARESORDERACCEPTANCE = dbManager.DataReader["PF_SPARESORDERACCEPTANCE"].ToString();
                    this.PF_AMC_INVOICE = dbManager.DataReader["PF_AMC_INVOICE"].ToString();
                    this.PF_AMC_PAYMETSRECEIVED = dbManager.DataReader["PF_AMC_PAYMETSRECEIVED"].ToString();
                    this.PF_EMPLOYEEMASTER = dbManager.DataReader["PF_EMPLOYEEMASTER"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }
        }


        //Methods For ProductCompany Master
        public class ProductCompany
        {
            public string PdCompanyId, PdCompanyName, PdCompanyDesc;   //ProductCompany Master
            public ProductCompany()
            {
            }

            public string ProductCompany_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PRODUCT_COMPANY]", "PRODUCT_COMPANY_NAME", this.PdCompanyName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PRODUCT_COMPANY] SELECT ISNULL(MAX(PRODUCT_COMPANY_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_PRODUCT_COMPANY]", this.PdCompanyName, this.PdCompanyDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Product Company Details", "35");

                    }
                }
                else
                {
                    _returnStringMessage = "Company Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            public int ExcelBrand_Select(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("  select * from excelprice_aud where Filename= '"+SalesOrderId+"' " );
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PdCompanyName   = dbManager.DataReader["BRAND"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public string ProductCompany_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_PRODUCT_COMPANY]", "PRODUCT_COMPANY_NAME", this.PdCompanyName, "PRODUCT_COMPANY_ID", this.PdCompanyId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_PRODUCT_COMPANY] SET PRODUCT_COMPANY_NAME='{0}',PRODUCT_COMPANY_DESC='{1}' WHERE PRODUCT_COMPANY_ID={2}", this.PdCompanyName, this.PdCompanyDesc, this.PdCompanyId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Product Company Details", "35");

                    }
                }
                else
                {
                    _returnStringMessage = "Company Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ProductCompany_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_PRODUCT_COMPANY]", "PRODUCT_COMPANY_ID", this.PdCompanyId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Product Company Details", "35");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }

                return _returnStringMessage;
            }
            public static void ProductCompany_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT PRODUCT_COMPANY_NAME,PRODUCT_COMPANY_ID FROM [YANTRA_LKUP_PRODUCT_COMPANY] ORDER BY PRODUCT_COMPANY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PRODUCT_COMPANY_NAME", "PRODUCT_COMPANY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }

            public static void ExcelFiles_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("select Distinct Filename,Brand from excelprice_aud");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Filename", "Brand");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }

            public static void InsuranceCompany_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT [Insurance_Name],[Insurance_Master_id] FROM [Insurance_Master] ORDER BY [Insurance_Name]");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Insurance_Name", "Insurance_Master_id");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }



        }

        public class Architect
        {
            public string Architect_Id, Architect_Name, Architect_Address, Architect_Mobile, Architect_Email, UPLOAD1, UPLOAD2, Category,OrgName,city,pincode,website,ArchDesg;

            public string FPS_Id, FPS_Dt,SO_ID, PO_Amt, PO_Amt1, Percntage, TotalAmt, Status, Remarks,Executive_ID,Prepared_By,Dispatch_Id,Cust_ID;
            public bool UserNameInUse { get; set; }
            public Architect() { }
            public string Architect_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ARCHITECT]", "ARCHITECT_NAME", this.Architect_Name) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ARCHITECT] SELECT ISNULL(MAX(ARCHITECT_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}' FROM [YANTRA_LKUP_ARCHITECT]", this.Architect_Name, this.Architect_Address, this.Architect_Mobile, this.Architect_Email, this.UPLOAD1, this.UPLOAD2,this.Category,this.OrgName ,this.ArchDesg,this.city ,this.pincode   );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Architect Details", "36");

                    }
                }
                else
                {
                    _returnStringMessage = "Company Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public static void ArCity_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("select distinct City  from YANTRA_LKUP_ARCHITECT ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "City", "City");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
            public static void ArPincode_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("select distinct Pincode  from YANTRA_LKUP_ARCHITECT ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Pincode", "Pincode");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
            public string FPS_Save()
            {
                dbManager.Open();
                
                    _commandText = string.Format("INSERT INTO [FPS_tbl] SELECT ISNULL(MAX(FPS_ID),0)+1,'{0}','{1}',{2},{3},{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}' FROM [FPS_tbl]", this.SO_ID , this.Architect_Id , this.PO_Amt , this.PO_Amt1 , this.Percntage , this.TotalAmt , this.Status , this.Remarks , this.FPS_Dt,this.Architect_Name,this.Executive_ID ,this.Prepared_By ,this.Dispatch_Id ,this.Cust_ID   );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("FPS Architect Details", "36");

                    }
                
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string Architect_Update()
            {
                //if (dbManager.Transaction == null)

                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_ARCHITECT]", "ARCHITECT_NAME", this.Architect_Name, "ARCHITECT_ID", this.Architect_Id) == false)
                //{
                    _commandText = string.Format("UPDATE YANTRA_LKUP_ARCHITECT SET ARCHITECT_NAME='{0}', ARCHITECT_ADDRESS='{1}', ARCHITECT_MOBILE='{2}', ARCHITECT_EMAIL='{3}', UPLOAD1='{4}', UPLOAD2='{5}', Category='{6}', Organization_Name='{7}', Designation='{8}', City='{9}', Pincode='{10}' WHERE ARCHITECT_ID={11}", this.Architect_Name, this.Architect_Address, this.Architect_Mobile, this.Architect_Email, this.UPLOAD1, this.UPLOAD2, this.Category, this.OrgName, this.ArchDesg, this.city, this.pincode, this.Architect_Id);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Item Architect Details", "36");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Architect Already Exists.";
                //}
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Architect_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_ARCHITECT]", "ARCHITECT_ID", this.Architect_Id) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Category Details", "36");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public int SelectArchitectMast(string Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ARCHITECT] where ARCHITECT_ID=" + Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.Architect_Mobile = dbManager.DataReader["Architect_Mobile"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                //dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public string FPS_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update FPS_tbl set PO_Amt='{0}',PO_Amt1='{1}',Percntage='{2}',TotalAmt='{3}',Status='{4}',Remarks='{5}',FPS_Dt='{6}',Name='{7}', Architect_Id='{8}' where FPS_ID ={9}", this.PO_Amt , this.PO_Amt1 , this.Percntage , this.TotalAmt ,this.Status , this.Remarks , this.FPS_Dt , this.Architect_Name,this.Architect_Id , this.FPS_Id );
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
            public int FPS_Select1(string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from fps_tbl inner join YANTRA_SO_MAST on fps_tbl.so_id =YANTRA_SO_MAST .SO_ID where fps_id=" + Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SO_ID = dbManager.DataReader["SO_ID"].ToString();
                    this.Architect_Id = dbManager.DataReader["Architect_Id"].ToString();
                    this.PO_Amt = dbManager.DataReader["PO_Amt"].ToString();
                    this.PO_Amt1 = dbManager.DataReader["PO_Amt"].ToString();

                    this.Percntage = dbManager.DataReader["Percntage"].ToString();
                    this.TotalAmt = dbManager.DataReader["TotalAmt"].ToString();
                    this.Status = dbManager.DataReader["Status"].ToString();
                    this.Remarks = dbManager.DataReader["Remarks"].ToString();
                    this.Architect_Name  = dbManager.DataReader["Name"].ToString();
                    
                    this.FPS_Dt = Convert.ToDateTime(dbManager.DataReader["FPS_Dt"].ToString()).ToString("dd/MM/yyyy");

                   

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public void FPS_Select(string ArchitectId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select ARCHITECT_NAME ,SO_NO,CUST_NAME ,po_Amt,PO_Amt1,Percntage,TotalAmt,Status,Remarks,fps_dt from fps_tbl inner join YANTRA_LKUP_ARCHITECT on fps_tbl.Architect_id=YANTRA_LKUP_ARCHITECT .ARCHITECT_ID inner join YANTRA_SO_MAST on fps_tbl.so_Id =YANTRA_SO_MAST .SO_ID inner join YANTRA_CUSTOMER_MAST on YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID where FPS_tbl.Architect_ID=" + ArchitectId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ARCHITECT_NAME");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SO_NO");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("CUST_NAME");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("po_Amt");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PO_Amt1");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Percntage");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("TotalAmt");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Status");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("fps_dt");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ARCHITECT_NAME"] = dbManager.DataReader["ARCHITECT_NAME"].ToString();
                    dr["SO_NO"] = dbManager.DataReader["SO_NO"].ToString();
                    dr["CUST_NAME"] = dbManager.DataReader["CUST_NAME"].ToString();
                    dr["po_Amt"] = dbManager.DataReader["po_Amt"].ToString();
                    dr["PO_Amt1"] = dbManager.DataReader["PO_Amt1"].ToString();
                    dr["Percntage"] = dbManager.DataReader["Percntage"].ToString();
                    dr["TotalAmt"] = dbManager.DataReader["TotalAmt"].ToString();

                    dr["Status"] = dbManager.DataReader["Status"].ToString();
                    dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                    //dr["fps_dt"] = dbManager.DataReader["fps_dt"].ToString();
                    dr["fps_dt"] = Convert.ToDateTime(dbManager.DataReader["fps_dt"].ToString()).ToString("dd/MM/yyyy");

                    //dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                    //dbManager.DataReader.Close();
                    gv.DataSource = SalesInvoiceProducts;
                    gv.DataBind();
                }
            }

            public void FPS_SelectCust(string ArchitectId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select ARCHITECT_NAME,FPS_ID ,SO_NO,CUST_NAME,Name ,po_Amt,PO_Amt1,Percntage,TotalAmt,Status,Remarks,fps_dt from fps_tbl left outer join YANTRA_LKUP_ARCHITECT on fps_tbl.Architect_id=YANTRA_LKUP_ARCHITECT .ARCHITECT_ID inner join YANTRA_SO_MAST on fps_tbl.so_Id =YANTRA_SO_MAST .SO_ID inner join YANTRA_CUSTOMER_MAST on YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID where YANTRA_SO_MAST.SO_CUST_ID=" + ArchitectId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ARCHITECT_NAME");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("SO_NO");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("CUST_NAME");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("po_Amt");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("PO_Amt1");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Percntage");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("TotalAmt");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Status");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("fps_dt");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Name");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("FPS_ID");
                SalesInvoiceProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ARCHITECT_NAME"] = dbManager.DataReader["ARCHITECT_NAME"].ToString();
                    dr["SO_NO"] = dbManager.DataReader["SO_NO"].ToString();
                    dr["CUST_NAME"] = dbManager.DataReader["CUST_NAME"].ToString();
                    dr["po_Amt"] = dbManager.DataReader["po_Amt"].ToString();
                    dr["PO_Amt1"] = dbManager.DataReader["PO_Amt1"].ToString();
                    dr["Percntage"] = dbManager.DataReader["Percntage"].ToString();
                    dr["TotalAmt"] = dbManager.DataReader["TotalAmt"].ToString();

                    dr["Status"] = dbManager.DataReader["Status"].ToString();
                    dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                    dr["Name"] = dbManager.DataReader["Name"].ToString();
                    dr["fps_dt"] = Convert.ToDateTime(dbManager.DataReader["fps_dt"].ToString()).ToString("dd/MM/yyyy");

                    dr["FPS_ID"] = dbManager.DataReader["FPS_ID"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                    //dbManager.DataReader.Close();
                    gv.DataSource = SalesInvoiceProducts;
                    gv.DataBind();
                }
            }
            public static void Architect_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ARCHITECT] ORDER BY ARCHITECT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ARCHITECT_NAME", "ARCHITECT_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }

        }

        //Methods For ItemCategory Master
        public class ItemCategory
        {
            public string ItCategoryId, ItCategoryName, ItCategoryDesc;   //Item Category Master 
            public ItemCategory()
            {
            }
            public ItemCategory(string BrandId)
        {
            dbManager.Open();
            _commandText = string.Format(@"select distinct ITEM_CATEGORY_ID ,ITEM_CATEGORY_NAME  from YANTRA_ITEM_MAST 
	inner join YANTRA_LKUP_ITEM_CATEGORY on YANTRA_ITEM_MAST .IC_ID =YANTRA_LKUP_ITEM_CATEGORY .ITEM_CATEGORY_ID where BRAND_ID = "+BrandId);
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.ItCategoryId = dbManager.DataReader["ITEM_CATEGORY_ID"].ToString();
                this.ItCategoryName = dbManager.DataReader["ITEM_CATEGORY_NAME"].ToString();
                //this.Company_ID = dbManager.DataReader["COMPANY_ID"].ToString();
            }
            dbManager.DataReader.Close();
        }
            public string ItemCategory_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ITEM_CATEGORY]", "ITEM_CATEGORY_NAME", this.ItCategoryName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ITEM_CATEGORY] SELECT ISNULL(MAX(ITEM_CATEGORY_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_ITEM_CATEGORY]", this.ItCategoryName, this.ItCategoryDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Category Details", "36");

                    }
                }
                else
                {
                    _returnStringMessage = "Company Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string AssetCategory_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ASSET_CATEGORY]", "ITEM_CATEGORY_NAME", this.ItCategoryName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_LKUP_ASSET_CATEGORY] SELECT ISNULL(MAX(ITEM_CATEGORY_ID),0)+1,'{0}','{1}' FROM [YANTRA_LKUP_ASSET_CATEGORY]", this.ItCategoryName, this.ItCategoryDesc);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Category Details", "36");

                    }
                }
                else
                {
                    _returnStringMessage = "Company Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string ItemCategory_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ITEM_CATEGORY]", "ITEM_CATEGORY_NAME", this.ItCategoryName, "ITEM_CATEGORY_ID", this.ItCategoryId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_ITEM_CATEGORY] SET ITEM_CATEGORY_NAME='{0}',ITEM_CATEGORY_DESC='{1}' WHERE ITEM_CATEGORY_ID={2}", this.ItCategoryName, this.ItCategoryDesc, this.ItCategoryId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Item Category Details", "36");

                    }
                }
                else
                {
                    _returnStringMessage = "Company Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string AssetCategory_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_LKUP_ASSET_CATEGORY]", "ITEM_CATEGORY_NAME", this.ItCategoryName, "ITEM_CATEGORY_ID", this.ItCategoryId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_ASSET_CATEGORY] SET ITEM_CATEGORY_NAME='{0}',ITEM_CATEGORY_DESC='{1}' WHERE ITEM_CATEGORY_ID={2}", this.ItCategoryName, this.ItCategoryDesc, this.ItCategoryId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Asset Category Details", "36");

                    }
                }
                else
                {
                    _returnStringMessage = "Asset Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string ItemCategory_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_ITEM_CATEGORY]", "ITEM_CATEGORY_ID", this.ItCategoryId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Item Category Details", "36");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public string AssetCategory_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_ASSET_CATEGORY]", "ITEM_CATEGORY_ID", this.ItCategoryId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Asset Category Details", "36");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public static void ItemCategory_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_CATEGORY] ORDER BY ITEM_CATEGORY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_CATEGORY_NAME", "ITEM_CATEGORY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }

            public static void AssetCategory_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_asset_CATEGORY] ORDER BY ITEM_CATEGORY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_CATEGORY_NAME", "ITEM_CATEGORY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
            public static void ItemCategory_Select_WithBrand(Control ControlForBind, string BrandId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT distinct a.ITEM_CATEGORY_ID,a.ITEM_CATEGORY_NAME FROM [YANTRA_LKUP_ITEM_CATEGORY] a inner join dbo.YANTRA_ITEM_MAST b on a.ITEM_CATEGORY_ID=b.IC_ID where b.BRAND_ID='" + BrandId + "'");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_CATEGORY_NAME", "ITEM_CATEGORY_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
               // //dbManager.Close();

            }

            public static void ItemCategory_SelectForPrint(Control ControlForBind,string brand,string category)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  DISTINCT(dbo.YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID),dbo.YANTRA_LKUP_ITEM_TYPE.IT_TYPE FROM  dbo.YANTRA_LKUP_ITEM_CATEGORY INNER JOIN   dbo.YANTRA_ITEM_MAST INNER JOIN dbo.YANTRA_LKUP_PRODUCT_COMPANY ON dbo.YANTRA_ITEM_MAST.BRAND_ID = dbo.YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID ON dbo.YANTRA_LKUP_ITEM_CATEGORY.ITEM_CATEGORY_ID = dbo.YANTRA_ITEM_MAST.IC_ID INNER JOIN dbo.YANTRA_LKUP_ITEM_TYPE ON dbo.YANTRA_ITEM_MAST.IT_TYPE_ID = dbo.YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID WHERE     (dbo.YANTRA_ITEM_MAST.IC_ID =" + category + ") AND (dbo.YANTRA_ITEM_MAST.BRAND_ID =" + brand + ")");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }


        }


        //Methods For Color Master
        public class ColorMaster
        {
            public string ColorId, ColorName, Ic_Id,Brand_Id;    
            public ColorMaster()
            {
            }

            public string Color_Save()
            {
                dbManager.Open();
                _commandText = string.Format("select count(*) from [YANTRA_LKUP_COLOR_MAST] where BRAND_ID={0} and IC_ID={1} and COLOUR_NAME='{2}'", this.Brand_Id, this.Ic_Id, this.ColorName);
                int check=Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                //int check=Convert.ToInt32(
                //if (IsRecordExists("[YANTRA_LKUP_COLOR_MAST]", "ITEM_CATEGORY_NAME", this.ItCategoryName) == false)
                //{
                if(check==0)
                {
                _commandText = string.Format("INSERT INTO [YANTRA_LKUP_COLOR_MAST] SELECT ISNULL(MAX(COLOUR_ID),0)+1,'{0}',{1},{2} FROM [YANTRA_LKUP_COLOR_MAST]", this.ColorName, this.Brand_Id, this.Ic_Id);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Color Details", "37");


                    }
                }
                else
                {
                    _returnStringMessage = "Colour Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Color_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_ITEM_CATEGORY]", "ITEM_CATEGORY_NAME", this.ItCategoryName, "ITEM_CATEGORY_ID", this.ItCategoryId) == false)
                //{
                _commandText = string.Format("UPDATE [YANTRA_LKUP_COLOR_MAST] SET COLOUR_NAME='{0}',BRAND_ID={1},IC_ID={2} WHERE COLOUR_ID={3}", this.ColorName , this.Brand_Id, this.Ic_Id,this.ColorId );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Color Details", "37");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Company Name Already Exists.";
                //}
                    //dbManager.Close();

                return _returnStringMessage;
            }

            public string Color_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_COLOR_MAST]", "COLOUR_ID", this.ColorId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Color Details", "37");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public static void Color_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT top 10 * FROM [YANTRA_LKUP_COLOR_MAST] ORDER BY COLOUR_NAME ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "COLOUR_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
            public static void Color_Select(Control ControlForBind, int ic_id, int brandId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_COLOR_MAST] where IC_ID=" + ic_id + " and BRAND_ID=" + brandId + " ORDER BY COLOUR_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "COLOUR_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }
            public static void Color_Select(Control ControlForBind,string brand,string category)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_COLOR_MAST]  WHERE BRAND_ID=" + brand + " AND IC_ID=" + category + "  ORDER BY COLOUR_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "COLOUR_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }


            public static void ColorBrand_Select(Control ControlForBind, string brand)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_COLOR_MAST]  WHERE BRAND_ID=" + brand + "  ORDER BY COLOUR_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "COLOUR_NAME", "COLOUR_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }






        }
        public class Godown
        {
            public string GodownId, GodownName;  //Godown Master
            public string Cpid;

            public Godown()
            { }

            public static string GetGodownName(string Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT GODOWN_NAME FROM [YANTRA_LKUP_GODOWN] where GODOWN_ID=" + Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    //this.ItemType = dbManager.DataReader["IT_TYPE"].ToString();
                    _returnStringMessage = dbManager.DataReader["GODOWN_NAME"].ToString();

                }
                //dbManager.Close();

                return _returnStringMessage; ;
            }

            public string Godown_Save()
            {
                dbManager.Open();
                //if (IsRecordExists("[YANTRA_LKUP_GODOWN]", "GODOWN_NAME", this.GodownName) == false)
                //{

                _commandText = string.Format("INSERT INTO [YANTRA_LKUP_GODOWN] SELECT ISNULL(MAX(GODOWN_ID),0)+1,'{0}',{1} FROM [YANTRA_LKUP_GODOWN]", this.GodownName, this.Cpid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Godown Details", "38");

                }
                //}
                //else
                //{
                //    _returnStringMessage = "Godown Name Already Exists.";
                //}

                //dbManager.Close();
                return _returnStringMessage;
            }

            public string Godown_Update()
            {
                dbManager.Open();
                // if (IsRecordExists("[YANTRA_LKUP_GODOWN]", "GODOWN_NAME", this.GodownName, "GODOWN_ID", this.GodownId) == false)
                if (true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_LKUP_GODOWN] SET GODOWN_NAME='{0}',CP_ID = {1} WHERE GODOWN_ID={2}", this.GodownName,this.Cpid, this.GodownId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Godown Details", "38");

                    }
                }
                else
                {
                    _returnStringMessage = "Godown Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Godown_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_LKUP_GODOWN]", "GODOWN_ID", this.GodownId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Godown Details", "38");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                //dbManager.Close();

                return _returnStringMessage;
            }


            public static void Godown_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_LKUP_GODOWN] ORDER BY GODOWN_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "GODOWN_NAME", "GODOWN_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
                //dbManager.Close();

            }

            public static void Company_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CP_FULL_NAME,CP_ID FROM [YANTRA_COMP_PROFILE] ORDER BY CP_FULL_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CP_FULL_NAME", "CP_ID");
                }
                //dbManager.Close();

            }

            public static void EmployeeMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] WHERE EMP_ID<>0 ORDER BY EMP_FIRST_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString() + " " + dbManager.DataReader["EMP_LAST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                //dbManager.Close();

            }
        }
        public class Memo
        {
            public string MemoId, MemoName,MemoDate,MemoReason;
            public string EmpId, Cpid;
            DateTime date;

            public Memo()
            {
            }

            public static void Company_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CP_FULL_NAME,CP_ID FROM [YANTRA_COMP_PROFILE] ORDER BY CP_FULL_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CP_FULL_NAME", "CP_ID");
                }
                //dbManager.Close();

            }

            public static void Employee(Control ControlForBind, string company_id)
            {
                dbManager.Open();
                _commandText = string.Format("select EMP_ID,EMP_FIRST_NAME,COMPANY_ID from YANTRA_EMPLOYEE_MAST where    YANTRA_EMPLOYEE_MAST.EMP_ID<>0 AND    YANTRA_EMPLOYEE_MAST.COMPANY_ID=" + int.Parse(company_id) + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "EMP_FIRST_NAME", "EMP_ID");

                }
                //dbManager.Close();

            }


            public string Memo_Save()
            {
                dbManager.Open();

                _commandText = string.Format("INSERT INTO [Yantra_EMP_MEMO] SELECT ISNULL(MAX(Memo_Id),0)+1,'{0}',{1},'{2}','{3}' FROM [Yantra_EMP_MEMO]", MemoName,EmpId,MemoDate,MemoReason);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Memo Details", "39");

                }

                //dbManager.Close();

                return _returnStringMessage;
            }



            //#region MEMO SAVE
            //public string Memo_Save(int empid, string memo, DateTime issuedate, string reason)
            //{
            //    dbManager.Open();
            //    //if (IsRecordExists("[YANTRA_LKUP_GODOWN]", "GODOWN_NAME", this.GodownName) == false)
            //    //{

            //    _commandText = string.Format("INSERT INTO [Yantra_EMP_MEMO] SELECT ISNULL(MAX(Memo_Id),0)+1,'{0}',{1},'{2}','{3}' FROM [Yantra_EMP_MEMO]", memo, empid, issuedate, reason);
            //    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //    _returnStringMessage = string.Empty;
            //    if (_returnIntValue < 0 || _returnIntValue == 0)
            //    {
            //        _returnStringMessage = "Some Data Missing.";
            //    }
            //    else
            //    {
            //        _returnStringMessage = "Data Saved Successfully";
            //    }
            //    //}
            //    //else
            //    //{
            //    //    _returnStringMessage = "Godown Name Already Exists.";
            //    //}
            //    return _returnStringMessage;
            //}
            //#endregion

            #region Memo Update
            public string Memo_Update()
            {
                dbManager.Open();

                if (true)
                {
                    _commandText = string.Format("UPDATE [Yantra_EMP_MEMO] SET  Memo_No='{0}',Emp_Id='{1}',Memo_Date='{2}',Reason='{3}' WHERE Memo_Id={4}", MemoName,EmpId,MemoDate,MemoReason, this.MemoId);
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
                }
                else
                {
                   // _returnStringMessage = "Memo  Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            #endregion

            #region MEMODELETE
            public string Memo_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[Yantra_EMP_MEMO]", "Memo_Id", this.MemoId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
            #endregion



            public static string Memo_AutoGenCode()
            {

                return AutoGenMaxNo("YANTRA_EMP_MEMO", "Memo_No");
            }

            //Method for Auto Generate Max Serial ID
            public static string AutoGenMaxId(string TableName, string FieldName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }

            //Method for Auto Generate Max Serial NO
            public static string AutoGenMaxNo(string TableName, string FieldName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                // _commandText = "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-10),CHARINDEX('-'," + FieldName + ")+1,LEN(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-10))))),0)+1 FROM " + TableName + " WHERE SUBSTRING(" + FieldName + ",LEN(" + FieldName + ")-9,5)='" + CurrentFinancialYear() + "'";
                _commandText = "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5),CHARINDEX('-'," + FieldName + ")+1,LEN(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5))))),0)+1 FROM " + TableName + " WHERE SUBSTRING(" + FieldName + ",LEN(" + FieldName + ")-4,5)='" + CurrentFinancialYear() + "'";
                string numb = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
                //return Prefix(TableName) + "-" + numb + "/" + CurrentFinancialYear() + "/" + DepartmentName();
                //dbManager.Close();

                return Prefix(TableName) + "-" + numb + "/" + CurrentFinancialYear();
            }

            public static string Prefix(string TableName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, "SELECT " + Yantra.Classes.General.GetRequiredPrefix(TableName) + " FROM YANTRA_PREFIX").ToString();
                //dbManager.Close();

                return _returnStringMessage.ToString();
            }

            //public static string DepartmentName()
            //{
            //    string Dept;
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    Dept = dbManager.ExecuteScalar(CommandType.Text, "select DEPT_NAME FROM [YANTRA_DEPT_MAST]").ToString();
            //    //if (string.IsNullOrEmpty(Dept))
            //    //{
            //    //    Dept = "";
            //    //}
            //    return Dept;
            //}
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
                //dbManager.Close();

                return year;
            }
        }
        public class Circular
        {
            public string CirId, CirName;
            public string EmpId, Cpid;
            public string CmpId, DeptId, empid, circular, issuedate, descrption;

            DateTime date;

            public Circular()
            {
            }

            public static void Company_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT CP_FULL_NAME,CP_ID FROM [YANTRA_COMP_PROFILE] ORDER BY CP_FULL_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CP_FULL_NAME", "CP_ID");
                }
                //dbManager.Close();

            }

            public static void EmployeeMas(Control ControlForBind, string Dept_id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT  YANTRA_EMPLOYEE_MAST.EMP_ID, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME FROM YANTRA_EMPLOYEE_MAST INNER JOIN YANTRA_EMPLOYEE_DET ON dbo.YANTRA_EMPLOYEE_MAST.EMP_ID = dbo.YANTRA_EMPLOYEE_DET.EMP_ID INNER JOIN YANTRA_DEPT_MAST ON dbo.YANTRA_EMPLOYEE_DET.DEPT_ID = dbo.YANTRA_DEPT_MAST.DEPT_ID and YANTRA_DEPT_MAST.DEPT_ID =" + int.Parse(Dept_id) + "");
                //  _commandText = string.Format("select YANTRA_EMPLOYEE_MAST.EMP_ID AS EMPID ,EMP_FIRST_NAME,DEPT_ID from YANTRA_EMPLOYEE_MAST,YANTRA_EMPLOYEE_DET where YANTRA_EMPLOYEE_MAST.EMP_ID=YANTRA_EMPLOYEE_DET.EMP_ID and YANTRA_EMPLOYEE_DET.DEPT_ID=" + int.Parse(Dept_id) + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "EMP_FIRST_NAME", "EMP_ID");

                }
                //dbManager.Close();

            }

            public static void Dept_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT DEPT_NAME,DEPT_ID FROM [YANTRA_DEPT_MAST] ORDER BY DEPT_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DEPT_NAME", "DEPT_ID");
                }
                //dbManager.Close();

            }


            public static void Employee(Control ControlForBind, string company_id)
            {
                dbManager.Open();
                _commandText = string.Format("select EMP_ID,EMP_FIRST_NAME,COMPANY_ID from YANTRA_EMPLOYEE_MAST where YANTRA_EMPLOYEE_MAST.COMPANY_ID=" + int.Parse(company_id) + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "EMP_FIRST_NAME", "EMP_ID");

                }
                //dbManager.Close();

            }

            #region Circular SAVE
            public string Circular_Save()
            {
                dbManager.Open();

                _commandText = string.Format("INSERT INTO [YANTRA_HR_CIRCULAR] SELECT ISNULL(MAX(CIR_ID),0)+1,'{0}',{1},{2},{3},'{4}','{5}' FROM [YANTRA_HR_CIRCULAR]", this.circular, this.CmpId, this.DeptId,this.empid, this.issuedate, this.descrption);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else
                {
                    _returnStringMessage = "Data Saved Successfully";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            #endregion

            #region Circular Update
            public string Circular_Update()
            {
                dbManager.Open();

                if (true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_HR_CIRCULAR] SET  CIR_NO='{0}',COMPANY_ID ={1},DEPT_ID = {2},Emp_Id={3},CIR_DATE='{4}',DESCRIPTION='{5}' WHERE CIR_ID={6}", this.circular, this.CmpId, this.DeptId, this.empid, this.issuedate, this.descrption, this.CirId);
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
                }
                else
                {
                 //   _returnStringMessage = "Circular Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;

            }
            #endregion

            #region Circular DELETE
            public string Circular_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[YANTRA_HR_CIRCULAR]", "CIR_ID", this.CirId) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
            #endregion
            public static string Cir_AutoGenCode()
            {

                return AutoGenMaxNo("YANTRA_HR_CIRCULAR", "CIR_NO");
            }



            public static string Offer_AutoGenCode()
            {

                return AutoGenMaxNo("YANTRA_OFFER_LETTER", "APP_NO");
            }



            //Method for Auto Generate Max Serial ID
            public static string AutoGenMaxId(string TableName, string FieldName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
                //dbManager.Close();

                return _returnIntValue.ToString();
            }

            //Method for Auto Generate Max Serial NO
            public static string AutoGenMaxNo(string TableName, string FieldName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-10),CHARINDEX('-'," + FieldName + ")+1,LEN(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-10))))),0)+1 FROM " + TableName + " WHERE SUBSTRING(" + FieldName + ",LEN(" + FieldName + ")-9,5)='" + CurrentFinancialYear() + "'";
                _commandText = "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5),CHARINDEX('-'," + FieldName + ")+1,LEN(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5))))),0)+1 FROM " + TableName + " WHERE SUBSTRING(" + FieldName + ",LEN(" + FieldName + ")-4,5)='" + CurrentFinancialYear() + "'";
                string numb = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
                //return Prefix(TableName) + "-" + numb + "/" + CurrentFinancialYear() + "/" + DepartmentName();
                //dbManager.Close();

                return Prefix(TableName) + "-" + numb + "/" + CurrentFinancialYear();
            }

            public static string Prefix(string TableName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, "SELECT " + Yantra.Classes.General.GetRequiredPrefix(TableName) + " FROM YANTRA_PREFIX").ToString();
                //dbManager.Close();

                return _returnStringMessage.ToString();
            }

            //public static string DepartmentName()
            //{
            //    string Dept;
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    Dept = dbManager.ExecuteScalar(CommandType.Text, "select DEPT_NAME FROM [YANTRA_DEPT_MAST]").ToString();

            //    return Dept;
            //}
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
                //dbManager.Close();

                return year;
            }


        }


        //Methods for InsuranceMaste Form
        public class InsuranceMaster
        {
            public string Insid, InsName, Insaddress;
            public InsuranceMaster()
            { }

            public string InsuranceMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[Insurance_Master]", "Insurance_Name", this.InsName) == false)
                {
                    _commandText = string.Format("INSERT INTO [Insurance_Master] SELECT ISNULL(MAX(Insurance_Master_id),0)+1,'{0}','{1}' FROM [Insurance_Master]", this.InsName, this.Insaddress);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Insurance Master Details", "40");

                    }
                }
                else
                {
                    _returnStringMessage = "Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string InsuranceMaster_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[Insurance_Master]", "Insurance_Name", this.InsName) == false)
                //{
                    _commandText = string.Format("UPDATE [Insurance_Master] SET Insurance_Name='{0}',Address='{1}' WHERE Insurance_Master_id={2}", this.InsName, this.Insaddress, this.Insid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Insurance Master Details", "40");

                    }
                //}
                //else
                //{
                //    _returnStringMessage = "Name Already Exists.";
                //}
                    //dbManager.Close();

                return _returnStringMessage;
            }

            public string RegisterMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[Insurance_Master]", "Insurance_Master_id", this.Insid) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Insurance Master Details", "40");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public static void Insurance_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT Insurance_Name ,Insurance_Master_id FROM [Insurance_Master] ORDER BY Insurance_Name");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Insurance_Name", "Insurance_Master_id");
                }
                //dbManager.Close();

            }
        }

        //Methods for DeliveryAddress Form
        public class DeliveryAddress
        {
            public string Insid, InsName, Insaddress;
            public DeliveryAddress()
            { }

            public string InsuranceMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[DeliveryAddress_Master]", "Delivery_Name", this.InsName) == false)
                {
                    _commandText = string.Format("INSERT INTO [DeliveryAddress_Master] SELECT ISNULL(MAX(DeliveryAddress_Id),0)+1,'{0}','{1}' FROM [DeliveryAddress_Master]", this.InsName, this.Insaddress);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Insurance Master Details", "40");

                    }
                }
                else
                {
                    _returnStringMessage = "Name Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string InsuranceMaster_Update()
            {
                dbManager.Open();
               
                    _commandText = string.Format("UPDATE [DeliveryAddress_Master] SET Delivery_Name='{0}',Delivery_Address='{1}' WHERE DeliveryAddress_Id={2}", this.InsName, this.Insaddress, this.Insid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Insurance Master Details", "40");

                    }
                    //dbManager.Close();
                
                return _returnStringMessage;
            }

            public string RegisterMaster_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[DeliveryAddress_Master]", "DeliveryAddress_Id", this.Insid) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Insurance Master Details", "40");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public static void DeliveryAddress_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT Delivery_Name ,DeliveryAddress_Id FROM [DeliveryAddress_Master] ORDER BY Delivery_Name");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Delivery_Name", "DeliveryAddress_Id");
                }
                //dbManager.Close();

            }

            public int Deliveryadd_sele(string codeid)
            {

                dbManager.Open();
                _commandText = string.Format("select * from DeliveryAddress_Master where DeliveryAddress_Id = " + codeid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Insaddress = dbManager.DataReader["Delivery_Address"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }


        }

        //Methods for orwardedDetailsr Form
        public class ForwardedDetails
        {
            public string Forid, Forname, Forphone, ForEmail, Foraddress;
            public ForwardedDetails()
            { }

            public string Terms_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[terms_Conditions]", "Title", this.Forname) == false)
                {
                    _commandText = string.Format("INSERT INTO [terms_Conditions] SELECT ISNULL(MAX(ID),0)+1,'{0}','{1}' FROM [terms_Conditions]", this.Forname, this.Forphone);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Forward Details", "41");

                    }
                }
                else
                {
                    _returnStringMessage = "Details Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string ForwardDetails_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[Forwarder_Details]", "Forwarder_Name", this.Forname) == false)
                {
                    _commandText = string.Format("INSERT INTO [Forwarder_Details] SELECT ISNULL(MAX(Forwarder_id),0)+1,'{0}','{1}','{2}','{3}' FROM [Forwarder_Details]", this.Forname, this.Forphone,this.ForEmail,this.Foraddress);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Forward Details", "41");

                    }
                }
                else
                {
                    _returnStringMessage = "Details Already Exists.";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Terms_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[Forwarder_Details]", "Forwarder_Name", this.Forname, "Phone",this.Forphone) == false)
                //{
                _commandText = string.Format("UPDATE [terms_Conditions] SET Title='{0}', Message='{1}' WHERE ID={2}", this.Forname, this.Forphone,this.Forid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Forward Details", "41");

                }
                //}
                //else
                //{
                //    _returnStringMessage = "Details Already Exists.";
                //}
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Forwarddetails_Update()
            {
                dbManager.Open();
                //if (IsRecordExists("[Forwarder_Details]", "Forwarder_Name", this.Forname, "Phone",this.Forphone) == false)
                //{
                _commandText = string.Format("UPDATE [Forwarder_Details] SET Forwarder_Name='{0}',Phone='{1}',Email = '{2}',Address = '{3}' WHERE Forwarder_id={4}", this.Forname, this.Forphone, this.ForEmail, this.Foraddress, this.Forid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Forward Details", "41");

                }
                //}
                //else
                //{
                //    _returnStringMessage = "Details Already Exists.";
                //}
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string Terms_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[terms_Conditions]", "ID", this.Forid) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Forward Details", "41");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }

            public string Forwarder_Delete()
            {
                Masters.BeginTransaction();
                if (DeleteRecord("[Forwarder_Details]", "Forwarder_id", this.Forid) == true)
                {
                    Masters.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Forward Details", "41");

                }
                else
                {
                    Masters.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }


            public static void Forwarder_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT Forwarder_Name ,Forwarder_id FROM [Forwarder_Details] ORDER BY Forwarder_Name");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Forwarder_Name", "Forwarder_id");
                }
                //dbManager.Close();

            }


            public int Forwarderdetails_Ddl(string forid)
            {

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [Forwarder_Details] where Forwarder_id=" + forid + " ORDER BY Forwarder_id");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Forid = dbManager.DataReader["Forwarder_id"].ToString();
                    this.Forname = dbManager.DataReader["Forwarder_Name"].ToString();
                    this.Forphone = dbManager.DataReader["Phone"].ToString();
                    this.ForEmail = dbManager.DataReader["Email"].ToString();
                    this.Foraddress = dbManager.DataReader["Address"].ToString();
                    
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }


        }

        public class CompanyLocations
        {
            public string locName, locDesc,locId;
            
            public string Location_Save()
            {
                dbManager.Open();

                _commandText = string.Format("INSERT INTO [location_tbl] values('{0}','{1}','{2}') ", this.locId,this.locName,this.locDesc);
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
                //dbManager.Close();


                return _returnStringMessage;
            }

        }

        //Methods for orwardedDetailsr Form
        public class OfferLetter
        {
            public string appid,Appno, Companyid, empname, designation, dept_id,mobileno,issueddate,description,Email;
            public OfferLetter()
            { }

            public string OfferLetter_Save()
            {
                dbManager.Open();

                _commandText = string.Format("INSERT INTO [YANTRA_OFFER_LETTER] SELECT ISNULL(MAX(APP_ID),0)+1,'{0}',{1},'{2}','{3}',{4},'{5}','{6}','{7}','{8}' FROM [YANTRA_OFFER_LETTER]", this.Appno, this.Companyid, this.empname, this.designation,this.dept_id,this.mobileno,this.issueddate,this.description,this.Email);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Offer Letter Details", "42");

                    }
                    //dbManager.Close();
                
               
                return _returnStringMessage;
            }

            public string OfferLetter_Update()
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [YANTRA_OFFER_LETTER] SET COMPANY_ID={0},EMP_NAME='{1}',DESIGNATION = '{2}',DEPT_ID = {3},MOBILE_NO = '{4}',ISSUED_DATE = '{5}',DESCRIPTION = '{6}',EMAIL='{7}' WHERE APP_ID={8}", this.Companyid, this.empname, this.designation, this.dept_id, this.mobileno, this.issueddate, this.description,this.Email, this.appid);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Offer Letter Details", "42");

                    }
                    //dbManager.Close();
              
                return _returnStringMessage;
            }

            public string OfferLetter_Delete()
            {
                             

              
                if (DeleteRecord("[YANTRA_OFFER_LETTER]", "APP_ID", this.appid) == true)
                {
                    
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Offer Letter Details", "42");

                }
                else
                {
                    
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

                }
                return _returnStringMessage;
            }
            public int OfferLetter_Ddl(string forid)
            {

                dbManager.Open();
                _commandText = string.Format("select * from YANTRA_OFFER_LETTER,YANTRA_DEPT_MAST,YANTRA_COMP_PROFILE where YANTRA_OFFER_LETTER.COMPANY_ID=YANTRA_COMP_PROFILE.CP_ID  and YANTRA_OFFER_LETTER.DEPT_ID=YANTRA_DEPT_MAST.DEPT_ID and APP_ID=" + forid + " ORDER BY APP_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.appid  = dbManager.DataReader["APP_ID"].ToString();
                    this.Appno = dbManager.DataReader["APP_NO"].ToString();
                    this.empname = dbManager.DataReader["EMP_NAME"].ToString();
                    this.designation = dbManager.DataReader["DESIGNATION"].ToString();
                    this.description = dbManager.DataReader["DESCRIPTION"].ToString();
                    this.mobileno = dbManager.DataReader["MOBILE_NO"].ToString();
                    this.issueddate = dbManager.DataReader["ISSUED_DATE"].ToString();
                    this.Companyid = dbManager.DataReader["CP_ID"].ToString();
                    this.dept_id = dbManager.DataReader["DEPT_ID"].ToString();
                    this.Email = dbManager.DataReader["EMAIL"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return _returnIntValue;
            }


        }


        public class ItemPurchase
        {
            public string itemcode, ItemID,status,whLocId, Barcode, Currency, rpp, rsp, mrp, internationalprice, companyid, Date, brandid, catid, subcatid,COLORID,COMPANYID,POID,DCID;
            public string MRNID, Cust_id, Cust_Name;
            public string InvoiceNo, ModelNo, SpareModelNo, color, Subcategory, Brand, Color, Quantity, Remarks, MRN_No, currencyType, gross, coefficient, mulFactor;

            public string ItemPurchase_Save()
            {
                dbManager.Open();
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PURCHASE VALUES ({0},'{1}',{2},'{3}','{4}','{5}','{6}')", this.itemcode, this.Barcode,this.Currency,this.rpp,this.rsp,this.mrp,this.internationalprice);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);


                    _commandText = string.Format("insert into YANTRA_ITEM_PRICE values({0},'{1}','{2}',{3},{4},'{5}',{6},'{7}',{8},'{9}','{10}','{11}')", itemcode, rsp, internationalprice, brandid, catid, DateTime.Now.ToString(), subcatid, DateTime.Now.ToString(), currencyType, gross, coefficient, mulFactor);
                //_commandText = string.Format("insert into YANTRA_ITEM_PRICE values({0},'{1}','{2}',{3},{4},'{5}',{6},'{7}')", this.itemcode, this.rsp, this.internationalprice, this.brandid, this.catid, DateTime.Now.ToString(), this.subcatid, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _commandText = string.Format("insert into YANTRA_ITEM_PURCHASE_PRICE_HISTORY values({0},'{1}','{2}','{3}','{4}','{5}')", this.itemcode, 0, this.internationalprice, 0, 0, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                
                _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Purchase Details", "43");

                    }
                    //dbManager.Close();
               
                return _returnStringMessage;
            }

            public string DisconItem_Update(int itemcode, string EmpName)
            {
                dbManager.Open();

                if (IsRecordExists("[YANTRA_ITEM_MAST]", "Item_Code", this.itemcode) == true)
                 {
                     _commandText = string.Format("UPDATE YANTRA_ITEM_MAST SET F2='Discontinued', dt_Updated='{0}',F1='{1}' where Item_Code={2}", DateTime.Now.ToString(), EmpName, itemcode);
                     _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                 }
                 return _returnStringMessage;
            }
            public string ExcelPriceUpdate(int itemcode, decimal GrossAmt, decimal ItemPrice,decimal OldPrice, decimal Factor)
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_ITEM_PRICE]", "Item_Code", this.itemcode) == false)
                {
                    _commandText = string.Format("insert into YANTRA_ITEM_PRICE values({0},'{1}','{2}',{3},{4},'{5}',{6},'{7}',{8},'{9}','{10}','{11}')", itemcode, rsp, internationalprice, brandid, catid, DateTime.Now.ToString(), subcatid, DateTime.Now.ToString(), currencyType, gross, coefficient, mulFactor);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                else if (IsRecordExists("[YANTRA_ITEM_PRICE]", "Item_Code", this.itemcode) == true)
                {
                    _commandText = string.Format("UPDATE YANTRA_ITEM_PRICE SET Item_Price='{0}', Date='{1}',GrossAmount='{2}', MulFactor='{3}' where Item_Code={4}", ItemPrice ,DateTime .Now .ToString (),GrossAmt,Factor ,itemcode  );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    _commandText = string.Format("insert into YANTRA_ITEM_PRICE_HISTORY values({0},'{1}','{2}','{3}','{4}','{5}')", itemcode, OldPrice, ItemPrice, 0, 0, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    //log.add_Update("Item Price Details Updated", "44");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }




          





            public string ItemPriceUpdate(int itemcode, decimal rsp, decimal mrp, decimal internationalprice, int brandid, int catid, int subcatid, int currencyType, decimal gross, decimal coefficient, decimal mulFactor)
            {
                dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ITEM_PURCHASE] SET RSP='{0}',MRP='{1}' WHERE Item_Code={2}", rsp, mrp, itemcode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                if (IsRecordExists("[YANTRA_ITEM_PRICE]", "Item_Code", this.itemcode) == false)
                {
                    _commandText = string.Format("insert into YANTRA_ITEM_PRICE values({0},'{1}','{2}',{3},{4},'{5}',{6},'{7}',{8},'{9}','{10}','{11}')", itemcode, rsp, internationalprice, brandid, catid, DateTime.Now.ToString(), subcatid, DateTime.Now.ToString(), currencyType, gross, coefficient, mulFactor);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                else if (IsRecordExists("[YANTRA_ITEM_PRICE]", "Item_Code", this.itemcode) == true)
                {
                    _commandText = string.Format("UPDATE YANTRA_ITEM_PRICE SET Item_Price='{0}', Date='{1}',InternationalPrice='{3}',CurrencyType={4},GrossAmount='{5}',Coefficient='{6}',MulFactor='{7}' where Item_Code={2}", rsp, DateTime.Now.ToString(), itemcode, internationalprice, currencyType, gross, coefficient, mulFactor);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                }
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    _commandText = string.Format("insert into YANTRA_ITEM_PRICE_HISTORY values({0},'{1}','{2}','{3}','{4}','{5}')", itemcode, 0, rsp, 0, 0, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    log.add_Update("Item Price Details Updated", "44");

                }
                //dbManager.Close();

                return _returnStringMessage;

            }

            public static void ItemBrand_RateCalc(string Brand, string percentage, decimal Amount_Increased)
            {
                
                    dbManager.Open();
                    int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + "").ToString());
                int[] a = new int[sizeOfArray];
               
                    dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + "");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];
               
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["Item_Price"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                }


                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["Item_Price"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if ( Amount_Increased == 0  && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if(Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }                        

                    }
                    dbManager.DataReader.Close();
                }
               
                //if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage,Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Price Details Updated", "44");

                    }

                }
                //dbManager.Close();

            }

            public static void ItemModel_RateCalc(string ModelNo, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + ModelNo + "").ToString());
                int[] a = new int[sizeOfArray];

                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + ModelNo + "");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["Item_Price"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                    //dbManager.Close();
                }


                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["Item_Price"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                }

                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Price Details Updated", "44");

                    }

                }
                //dbManager.Close();

            }

            public static void ItemPurchaseModel_RateCalc(string ModelNo, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + ModelNo + "").ToString());
                int[] a = new int[sizeOfArray];

                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + ModelNo + "");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }


                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                    //dbManager.Close();
                }

                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET InternationalPrice='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PURCHASE_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                //dbManager.Close();

            }
            public static void ItemPurchase_RateCalc(string Brand, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + "").ToString());
                int[] a = new int[sizeOfArray];

                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + "");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }


                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                }

                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET InternationalPrice='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PURCHASE_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                //dbManager.Close();

            }

            public static void ItemCategory_RateCalc(string Category, string percentage, decimal Amount_Increased)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Cat_Id =" + Category + "").ToString());
                int[] a = new int[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Cat_Id=" + Category + "");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["Item_Price"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                }


                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["Item_Price"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
					//dbManager.Close();
                }

                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased,DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                }
                //dbManager.Close();

            }


            /// <summary>
            /// Price Increase by Brand and Category
            /// </summary>
            /// <param name="codeid"></param>
            /// <returns></returns>

            public static void ItemBrand_RateCalc3(string Brand, string Category, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = "+Category+"  ").ToString());
                //dbManager.Close();
				int[] a = new int[sizeOfArray];

                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = "+Category+" ");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["Item_Price"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();


                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["Item_Price"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();
                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5})", a[x], c[x], b[x], percentage,Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Price Details Updated", "44");

                    }

                }
                //dbManager.Close();

            }

            public static void ItemPurchase_RateCalc3(string Brand, string Category, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + "  ").ToString());
                int[] a = new int[sizeOfArray];
				//dbManager.Close();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " ");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();

                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();
                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET InternationalPrice='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PURCHASE_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5})", a[x], c[x], b[x], percentage, Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                //dbManager.Close();

            }

            /// <summary>
            /// Price Increase by Brand and Category and subcategory
            /// </summary>
            /// <param name="codeid"></param>
            /// <returns></returns>
            public static void ItemBrand_RateCalc4(string Brand, string Category, string subcate, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = "+subcate+"  ").ToString());
                int[] a = new int[sizeOfArray];
				//dbManager.Close();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = "+subcate+" ");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["Item_Price"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();

                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["Item_Price"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();
                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased ,DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Price Details Updated", "44");

                    }

                }
                //dbManager.Close();

            }

            public static void ItemPurchase_RateCalc4(string Brand, string Category, string subcate, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = " + subcate + "  ").ToString());
                int[] a = new int[sizeOfArray];
				//dbManager.Close();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = " + subcate + " ");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();

                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();
                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [InternationalPrice] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PURCHASE_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                //dbManager.Close();

            }

            /// <summary>
            /// Price Increase by Brand and Category and subcategory and ModelNo
            /// </summary>
            /// <param name="codeid"></param>
            /// <returns></returns>
            public static void ItemBrand_RateCalc5(string Brand, string Category, string subcate, string modelno, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = " + subcate + " and Item_Code = "+modelno+" ").ToString());
                int[] a = new int[sizeOfArray];
				//dbManager.Close();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = " + subcate + " and Item_Code = " + modelno + " ");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["Item_Price"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();

                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select Item_Price FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["Item_Price"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * int.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                }
				//dbManager.Close();
                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET Item_Price='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage,Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Item Price Details Updated", "44");

                    }

                }
                //dbManager.Close();

            }

            public static void ItemPurchase_RateCalc5(string Brand, string Category, string subcate, string modelno, string percentage, decimal Amount_Increased)
            {

                dbManager.Open();
                int sizeOfArray = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, "select count(Item_Code) FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = " + subcate + " and Item_Code = " + modelno + " ").ToString());
                int[] a = new int[sizeOfArray];
				//dbManager.Close();
                dbManager.Open();
                dbManager.ExecuteReader(CommandType.Text, "select Item_Code FROM [YANTRA_ITEM_PRICE]  WHERE Brand_id=" + Brand + " and Cat_Id = " + Category + " and SubCat_Id = " + subcate + " and Item_Code = " + modelno + " ");
                int i = 0;
                while (dbManager.DataReader.Read())
                {
                    a[i] = Convert.ToInt32(dbManager.DataReader["Item_Code"].ToString());
                    i++;
                }
                dbManager.DataReader.Close();
				//dbManager.Close();
                decimal[] c = new decimal[sizeOfArray];

                dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE  Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string oldPrice = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal op = Convert.ToDecimal(oldPrice);
                        c[x] = op;

                    }
                    dbManager.DataReader.Close();
                }

				//dbManager.Close();
                decimal[] b = new decimal[sizeOfArray];
                if (dbManager.Transaction == null)
                    dbManager.Open();
                for (int x = 0; x < a.Length; x++)
                {
                    dbManager.ExecuteReader(CommandType.Text, "select InternationalPrice FROM [YANTRA_ITEM_PRICE]  WHERE Item_Code=" + a[x] + " ");
                    if (dbManager.DataReader.Read())
                    {
                        string temp1 = dbManager.DataReader["InternationalPrice"].ToString();
                        decimal flt = Convert.ToDecimal(temp1);
                        if (Amount_Increased == 0 && percentage != "")
                        {
                            flt = flt + (flt * Decimal.Parse(percentage) / 100);
                            b[x] = flt;
                        }
                        else if (Amount_Increased != 0 && percentage == "")
                        {
                            flt = flt + Amount_Increased;
                            b[x] = flt;
                        }

                    }
                    dbManager.DataReader.Close();
                    //dbManager.Close();
                }

                //if (dbManager.Transaction == null)
                dbManager.Open();
                for (int x = 0; x < b.Length; x++)
                {
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_PRICE] SET InternationalPrice='{0}' WHERE Item_Code={1} ", b[x], a[x]);
                    dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _commandText = string.Format("INSERT INTO YANTRA_ITEM_PURCHASE_PRICE_HISTORY VALUES ({0},'{1}',{2},'{3}','{4}','{5}')", a[x], c[x], b[x], percentage, Amount_Increased, DateTime.Now.ToString());
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                //dbManager.Close();

            }
            public static void ItemTypeCategory_Select(Control ControlForBind, string ItemTypeId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE] WHERE ITEM_CATEGORY_ID =" + ItemTypeId + " ORDER BY IT_TYPE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
                //dbManager.Close();
            }
            public int ItemPrice_Ddl(string codeid)
            {

                dbManager.Open();
                _commandText = string.Format("select * from YANTRA_ITEM_PRICE where Item_Code =" + codeid + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.rsp = dbManager.DataReader["Item_Price"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                //dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnIntValue;
            }
            public int ItemPrice_PIDdl(string POId, string codeid)
            {

                dbManager.Open();
                _commandText = string.Format("select * from YANTRA_PURCHASE_INVOICE_DET where Item_Code =" + codeid + " and PI_ID='" + POId + "' ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.rsp = dbManager.DataReader["PI_DET_RATE"].ToString();
                    this.qty = dbManager.DataReader["PI_DET_QTY"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                //dbManager.DataReader.Close();
                //dbManager.Close();

                return _returnIntValue;
            }
            public string ItemInward_Save_New()
            {
                // string locationid = WH_Locations.getBaseLocation(this.locationid);
                string locationid = "0";
                dbManager.Open();
                _commandText = string.Format("INSERT INTO INWARD_New VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},'{7}',{8})", this.ItemID, this.Barcode, this.itemcode, this.companyid, this.locationid, DateTime.Now.ToString(), this.COLORID, this.MRNID, this.Quantity);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Item Inward Details", "45");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            
            public string ItemInward_Save()
            {
               // string locationid = WH_Locations.getBaseLocation(this.locationid);
                string locationid ="0";
                dbManager.Open();
                _commandText = string.Format("INSERT INTO INWARD VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},'{7}')", this.ItemID, this.Barcode, this.itemcode, this.companyid,this.locationid, DateTime.Now.ToString(), this.COLORID, this.MRNID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Item Inward Details", "45");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string ItemId, RefNo, ItemCode, ItemCategory, ItemSubCategory, ColorId, qty, BalanceQty, DamageQty, CpId, InwardType,DateAdded,ItemLoc;
            public string InwardTemp_Save()
            {

                dbManager.Open();
                _commandText = string.Format("INSERT INTO [Temp_Inward] SELECT ISNULL(MAX(Id),0)+1,'{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},'{9}','{10}',{11},{12},'{13}','{14}' FROM [Temp_Inward]", this.RefNo, this.ItemCode, this.ItemCategory, this.ItemSubCategory, this.ColorId, this.qty, this.BalanceQty, this.DamageQty, this.CpId, this.InwardType, this.DateAdded, this.ItemLoc, this.Cust_id, this.DeliveryDate,this.Remarks );
               // _commandText = string.Format("INSERT INTO [Temp_Inward] SELECT ISNULL(MAX(Id),0)+1,'{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},'{9}','{10}',{11} FROM [Temp_Inward]", this.RefNo, this.ItemCode, this.ItemCategory, this.ItemSubCategory, this.ColorId, this.qty, this.BalanceQty, this.DamageQty, this.CpId, this.InwardType, this.DateAdded, this.ItemLoc);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Item Temporary Inward Details", "45");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }

          

            public string RecItemQty_Update(string qty, string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [Temp_Inward] SET Balance_Qty={0} WHERE Id={1}", qty, Id);
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
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string DamageItemQty_Update(string qty, string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [Temp_Inward] SET Damage_Qty={0} WHERE Id={1}", qty, Id);
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
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string MovingItemQty_Update(string qty, string Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [STOCKMOVEMENT_DETAILS] SET QUANTITY={0} WHERE SM_DCDET_ID={1}", qty, Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                   // log.add_Update("Indent Status Details", "84");

                }
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string Block_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO BlOCK VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},{7},'{8}')", this.ItemID, this.Barcode, this.itemcode, this.companyid, "0", DateTime.Now.ToString(), this.COLORID, this.POID,this.status);
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
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string CustomerId, DeliveryDate;

            public string Block_Save_New()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO BlOCK_New VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},'{7}','{8}',{9},'{10}',{11})", this.ItemID, this.Barcode, this.itemcode, this.companyid, this.whLocId, DateTime.Now.ToString(), this.COLORID, this.POID, this.status, this.CustomerId, DateTime.Now.ToString(), this.Quantity);
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
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string BlockNew_Save2()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO BlOCK_New VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},'{7}','{8}',{9},'{10}',{11})", this.ItemID, this.Barcode, this.itemcode, this.companyid, this.whLocId, DateTime.Now.ToString(), this.COLORID, this.POID, this.status, this.CustomerId, this.DeliveryDate, this.Quantity);
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
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string Block_Save2()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO BlOCK VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},'{7}','{8}',{9},'{10}')", this.ItemID, this.Barcode, this.itemcode, this.companyid, this.whLocId, DateTime.Now.ToString(), this.COLORID, this.POID,this.status,this.CustomerId,this.DeliveryDate);
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
                //dbManager.Close();

                return _returnStringMessage;
            }

            public static string SpareDC_AutoGenCode()
            {
                return SM.AutoGenMaxNo("Spare_Outward", "SP_DC_NO");
            }
            public string Spare_Inward_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO Spare_Inward VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}',{10},'{11}','{12}',{13})", this.ItemID, this.InvoiceNo, this.Barcode, this.ModelNo, this.SpareModelNo, this.subcatid, this.brandid, this.color, this.Quantity, this.Remarks, this.whLocId, DateTime.Now.ToString(), this.MRN_No, this.Quantity);
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
                //dbManager.Close();

                return _returnStringMessage;
            }
            public string Category,SubCat,Qty,Damage,Excess,Shortage,ItemType;
            public string Damage_Report_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO Damage_Report_tbl VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", this.ItemID, this.InvoiceNo, this.Barcode, this.ModelNo, this.Brand, this.Category, this.SubCat, this.Qty, this.color, this.Damage, this.Excess, this.Shortage,this.ItemType, this.Remarks, DateTime.Now.ToString());
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                }
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string DcNo;
            public string Spare_Outward_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO Spare_Outward VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},'{12}','{13}',{14},'{15}',{16})", this.DcNo, this.ItemID, this.InvoiceNo, this.Barcode, this.ModelNo, this.SpareModelNo, this.subcatid, this.brandid, this.color, this.Quantity, this.Remarks, this.whLocId, DateTime.Now.ToString(), this.MRN_No, this.Cust_id, this.Cust_Name, this.CpId);
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
                //dbManager.Close();

                return _returnStringMessage;
            }

            public string SpareQty_Update(string ModelNo, string itemId, int qty)
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [Spare_Inward] SET BalanceQty=" + qty + " WHERE Item_ID='" + itemId + "' and Model_No='" + ModelNo + "'");
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
                //dbManager.Close();

                return _returnStringMessage;
            }


            public string locationid,CustId;
            public string OutwardNew_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO OUTWARD_New VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},{7},{8},{9})", this.ItemID, this.Barcode, this.itemcode, this.companyid, this.locationid, DateTime.Now.ToString(), this.COLORID, this.DCID, this.CustId, this.qty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Item Temporary Outward Details", "45");

                }
                ////dbManager.Close();

                return _returnStringMessage;
            }
            public string Outward_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO OUTWARD VALUES ('{0}','{1}',{2},{3},{4},'{5}',{6},{7},{8})", this.ItemID, this.Barcode, this.itemcode, this.companyid, this.locationid, DateTime.Now.ToString(), this.COLORID, this.DCID,this.CustId);
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
                ////dbManager.Close();

                return _returnStringMessage;
            }

            public string InwardLoc_Update(int locId, string itemId, int whLocId)
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [INWARD] SET whLocId=" + whLocId + " WHERE Item_ID='" + itemId + "' and whLocId=" + locId + "");
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
                ////dbManager.Close();

                return _returnStringMessage;
            }

            public string InwardLocNew_Update(int locId, string itemId, int whLocId)
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [INWARD_New] SET whLocId=" + whLocId + " WHERE Item_ID='" + itemId + "' and whLocId=" + locId + "");
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
                ////dbManager.Close();

                return _returnStringMessage;
            }

            public string InwardLocNew_Update(string itemId, int locId, int qty)
            {
                dbManager.Open();

                _commandText = string.Format("UPDATE [INWARD_New] SET Quantity=" + qty + " WHERE Item_ID='" + itemId + "' and whLocId=" + locId + "");
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
                ////dbManager.Close();

                return _returnStringMessage;
            }
        }


    }
}