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
using vllib;

/// <summary>
/// Summary description for Services
/// </summary>
/// 

namespace YantraBLL.Modules
{
    public class Services
    {

        private static int _returnIntValue;
        private static string _returnStringMessage, _commandText;
        public enum ServicesStatus { New = 0, Open = 1, Closed = 2, Cancelled = 3, Regret = 4, }

        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());


        public Services()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //Method for Dispose
        public static void Dispose()
        {
            if (dbManager.Connection != null)
                dbManager.Dispose();
        }

        //Method for Begin Transaction
        public static void BeginTransaction()
        {
            dbManager.Open();
            dbManager.BeginTransaction();
        }

        //Method for Auto Generate Max Serial NO
        public static string AutoGenMaxNo(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = "SELECT ISNULL(MAX(CONVERT(BIGINT,SUBSTRING(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5),CHARINDEX('-'," + FieldName + ")+1,LEN(SUBSTRING(" + FieldName + ",0,LEN(" + FieldName + ")-5))))),0)+1 FROM " + TableName + " WHERE SUBSTRING(" + FieldName + ",LEN(" + FieldName + ")-4,5)='" + CurrentFinancialYear() + "'";
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

        //Method For Binding Service.Unit Names

        // public static void CustomerName_Select(Control ControlForBind, string CustomerId)
        //{
        //    dbManager.Open();
        //}


        //Method For Saving Courier Details
        public class ServiceCourierInfo
        {
            public string Sno, Courier_Type, Courier_From, Courier_To, Date, Courier_CompanyName, Courier_DocketNo, Courier_ReceivedBy, Remarks;
            public string Emp_Name, Tour_Tenure, Date_Of_Travel, Destination, ExtraField;
            public int Emp_Id, ID;
            public string Desg, ext, purpose, Comment;
            public string SalAdv, OldDue, Amount, EMI;

            #region Staff Tour Advance
            public string Staff_Tour_Save()
            {
                dbManager.Open();

                //_commandText = string.Format("INSERT INTO [Staff_Tour_Advance_tbl] SELECT ISNULL(MAX(ID),0)+1,{0},'{1}','{2}',{3},'{4}','{5}',{6},'{7}' FROM [Staff_Tour_Advance_tbl]", this.Emp_Id, this.Emp_Name, this.Date, this.Amount, this.Date_Of_Travel, this.Destination, this.Tour_Tenure, this.Comment);
                _commandText = string.Format("INSERT INTO [Staff_Tour_Advance_tbl] SELECT ISNULL(MAX(ID),0)+1,{0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}' FROM [Staff_Tour_Advance_tbl]", this.Emp_Id, this.Emp_Name, this.Date, this.Amount, this.Date_Of_Travel, this.Destination, this.Tour_Tenure, this.Comment);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Courier Master Details", "94");

                }
                dbManager.Close();

                return _returnStringMessage;
            }
            #endregion

            #region Salary Advance Request
            public string Salary_Advance_Save()
            {
                dbManager.Open();

                //_commandText = string.Format("INSERT INTO [Staff_Salary_Advance_Request_tbl] SELECT ISNULL(MAX(Sal_Adv_Id),0)+1,{0},'{1}','{2}','{3}',{4},'{5}',{6},{7},'{8}' FROM [Staff_Salary_Advance_Request_tbl]", this.Emp_Id, this.Emp_Name, this.Date, this.Desg, this.Amount, this.purpose, this.SalAdv, this.OldDue, this.ext);
                _commandText = string.Format("INSERT INTO [Staff_Salary_Advance_Request_tbl] SELECT ISNULL(MAX(Sal_Adv_Id),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}' FROM [Staff_Salary_Advance_Request_tbl]", this.Emp_Id, this.Emp_Name, this.Date, this.Desg, this.Amount, this.purpose, this.SalAdv, this.OldDue, this.ext);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Courier Master Details", "94");

                }
                dbManager.Close();

                return _returnStringMessage;
            }
            #endregion

            #region Mobile Advance Request
            public string Mobile_Advance_Save()
            {
                dbManager.Open();

                //_commandText = string.Format("INSERT INTO [Staff_Mobile_Advance_tbl] SELECT ISNULL(MAX(Id),0)+1,{0},'{1}','{2}','{3}',{4},{5},'{6}'  FROM [Staff_Mobile_Advance_tbl]", this.Emp_Id, this.Emp_Name, this.Desg, this.Date, this.Amount, this.EMI, this.ExtraField);
                _commandText = string.Format("INSERT INTO [Staff_Mobile_Advance_tbl] SELECT ISNULL(MAX(Id),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','{6}'  FROM [Staff_Mobile_Advance_tbl]", this.Emp_Id, this.Emp_Name, this.Desg, this.Date, this.Amount, this.EMI, this.ExtraField);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Courier Master Details", "94");

                }
                dbManager.Close();
                return _returnStringMessage;
            }
            #endregion

            #region Courier Details SAVE
            public string Courier_Save()
            {
                dbManager.Open();

                _commandText = string.Format("INSERT INTO [CourierMaster] SELECT ISNULL(MAX(Sno),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' FROM [CourierMaster]", this.Courier_Type, this.Courier_From, this.Courier_To, this.Date, this.Courier_CompanyName, this.Courier_DocketNo, this.Courier_ReceivedBy, this.Remarks);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Courier Master Details", "94");

                }
                dbManager.Close();

                return _returnStringMessage;
            }
            #endregion
        }

        //Methid For Saving Site Report Details
        public class ServiceSiteReport
        {
            public string Client_Id,Client_Name,Site_Address,Site_Plumber_Name ,Plumber_Mobile_No,Customer_Name,Executive_Name,Technician_Name,Quotation_Date,PO_Number,Site_Incharge_Mobile_No,Architecture_Name,Project_Manager_Name,Mobile_No;
            public string Inspection_Id,Client_Id_Details, Date_Of_Inspection,Attended_By,Position,Visit_Report;
            public string Indent_No, IndentDate, Brand, Model, Code, Quantity, Description, ClientAddress, Available, Indent, Technician_Id, TechnicianName, StorePerson_Id, StorePerson_Name, HeadTech_Id, HeadTech_Name, PurchasePerson_Id, PurchasePerson_Name;
            public int ServiceCustMaster_Select(string Client_Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM Site_Inspection_Report_tbl where Cust_Code= " + Client_Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Client_Id = dbManager.DataReader["Client_Id"].ToString();
                    this.Client_Name = dbManager.DataReader["Client_Name"].ToString();
                    this.Site_Address = dbManager.DataReader["Site_Address"].ToString();
                    this.Site_Plumber_Name = dbManager.DataReader["Site_Plumber_Name"].ToString();
                    this.Plumber_Mobile_No = dbManager.DataReader["Plumber_Mobile_No"].ToString();
                    this.Customer_Name = dbManager.DataReader["Customer_Name"].ToString();
                    this.Executive_Name = dbManager.DataReader["Executive_Name"].ToString();

                    this.Technician_Name = dbManager.DataReader["Technician_Name"].ToString();
                    this.Quotation_Date = dbManager.DataReader["Quotation_Date"].ToString();
                    this.PO_Number = dbManager.DataReader["PO_Number"].ToString();
                    this.Site_Incharge_Mobile_No = dbManager.DataReader["Site_Incharge_Mobile_No"].ToString();
                    this.Architecture_Name = dbManager.DataReader["Architecture_Name"].ToString();
                    this.Project_Manager_Name = dbManager.DataReader["Project_Manager_Name"].ToString();
                    this.Mobile_No = dbManager.DataReader["Mobile_No"].ToString();
                   
                    
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //  dbManager.Close();
                return _returnIntValue;
            }
            public int ServiceVisitedReports_Select(string Client_Id)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM Site_Inspection_Details_tbl where Client_Id= " + Client_Id);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Inspection_Id = dbManager.DataReader["Inspection_Id"].ToString();
                    this.Attended_By = dbManager.DataReader["Attended_By"].ToString();
                    this.Client_Id_Details = dbManager.DataReader["Client_Id"].ToString();
                    this.Date_Of_Inspection = dbManager.DataReader["Date_Of_Inspection"].ToString();
                    this.Position = dbManager.DataReader["Position"].ToString();
                    this.Visit_Report = dbManager.DataReader["Visit_Report"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //  dbManager.Close();
                return _returnIntValue;
            }
            public string InsertSiteReportInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [Site_Inspection_Report_tbl] VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", this.Client_Id, this.Client_Name, this.Site_Address, this.Site_Plumber_Name, this.Plumber_Mobile_No, this.Customer_Name, this.Executive_Name, this.Technician_Name, this.Quotation_Date, this.PO_Number, this.Site_Incharge_Mobile_No, this.Architecture_Name, this.Project_Manager_Name, this.Mobile_No);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Site Report Details", "139");

                }
                return _returnStringMessage;
            }
            public string UpdateSiteReportInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [Site_Inspection_Report_tbl] SET Client_Name='{1}',Site_Address='{2}',Site_Plumber_Name='{3}',Plumber_Mobile_No='{4}',Customer_Name='{5}',Executive_Name='{6}',Technician_Name='{7}',Quotation_Date='{8}',PO_Number='{9}',Site_Incharge_Mobile_No='{10}',Architecture_Name='{11}',Project_Manager_Name='{12}',Mobile_No='{13}'  WHERE Client_Id={0}", this.Client_Id, this.Client_Name, this.Site_Address, this.Site_Plumber_Name, this.Plumber_Mobile_No, this.Customer_Name, this.Executive_Name, this.Technician_Name, this.Quotation_Date, this.PO_Number, this.Site_Incharge_Mobile_No, this.Architecture_Name, this.Project_Manager_Name, this.Mobile_No);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Site Report Details", "139");
                }
                return _returnStringMessage;
            }
            public string InsertSiteReportDetailsInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [Site_Inspection_Details_tbl] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", this.Inspection_Id, this.Client_Id_Details, this.Date_Of_Inspection, this.Attended_By, this.Position, this.Visit_Report);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Site Report Details", "139");
                }
                return _returnStringMessage;
            }
            public string UpdateSiteReportDetailsInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [Site_Inspection_Details_tbl] SET Client_Id='{1}', Date_Of_Inspection='{2}',Attended_By='{3}',Position='{4}',Visit_Report='{5}'  WHERE Inspection_Id={0}", this.Inspection_Id, this.Client_Id_Details, this.Date_Of_Inspection, this.Attended_By, this.Position, this.Visit_Report);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Site Report Details", "139");



                }
                return _returnStringMessage;
            }
            public string DeleteSiteReportDetailsInfo(string Client_Id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("DELETE FROM [Site_Inspection_Details_tbl] WHERE Client_Id={0}", Client_Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Delete("Site Report Details", "139");

                }
                return _returnStringMessage;
            }
            public string InsertSpareIndentDetails()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [SparesIndent_tbl] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')", this.Indent_No, this.IndentDate, this.Brand, this.Model, this.Code, this.Quantity, this.Description, this.ClientAddress, this.Available, this.Indent, this.Technician_Id, this.TechnicianName, this.StorePerson_Id, this.StorePerson_Name, this.HeadTech_Id, this.HeadTech_Name, this.PurchasePerson_Id, this.PurchasePerson_Name);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spare Indent Details", "140");
                }
                return _returnStringMessage;
            }

            public string UpdateSpareIndentTechInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [SparesIndent_tbl] SET Technician_Id={1},TechnicianName='{2}' WHERE Indent_No={0}", this.Indent_No,this.Technician_Id,this.Technician_Name );

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Spare Indent Details", "140");

                }
                return _returnStringMessage;
            }

            public string UpdateSpareIndentStoreInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [SparesIndent_tbl] SET StorePerson_Id={1},StorePerson_Name='{2}' WHERE Indent_No={0}", this.Indent_No, this.StorePerson_Id, this.StorePerson_Name);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Spare Indent Details", "140");

                }
                return _returnStringMessage;
            }

            public string UpdateSpareIndentHeadInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [SparesIndent_tbl] SET HeadTech_Id={1},HeadTech_Name='{2}' WHERE Indent_No={0}", this.Indent_No, this.HeadTech_Id, this.HeadTech_Name);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Spare Indent Details", "140");

                }
                return _returnStringMessage;
            }

            public string UpdateSpareIndentPurchaseInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [SparesIndent_tbl] SET PurchasePerson_Id={1},PurchasePerson_Name='{2}' WHERE Indent_No={0}", this.Indent_No, this.PurchasePerson_Id, this.PurchasePerson_Name);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Spare Indent Details", "140");

                }
                return _returnStringMessage;
            }


        }

        //Method for Saving Customer Complaint Register
        public class ServiceCustInfo
        {
            public string Cust_Code, Cust_Name, Cust_Company_Name, Cust_Contact_Person, Cust_Mobile, Cust_Email, Cust_Address,Add_Reference;
            public string Cust_ID, Cust_Unit_Name, Cust_Unit_Address, Unit_Contact_Person, Contact_Mobile,UnitEmail;
            public string SOId, PONo, SODate, SOCUSTID, SOCustName, SOCompanyName, SOAddress, SOMobileNO, SOPhoneNo, SOContactPerson, SOEmail;
            public static string AutoGenerateCustCode()
            {
                return AutoGenMaxNo("Service_Customer_Information", "Cust_Code");
            }

            public string InsertCustInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [Service_Customer_Information] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", this.Cust_Code, this.Cust_Name, this.Cust_Company_Name, this.Cust_Contact_Person, this.Cust_Mobile, this.Cust_Email, this.Cust_Address,this.Add_Reference);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Customer Information Details", "95");

                }
                return _returnStringMessage;
            }
            public static void ServiceCust_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("select Cust_Id ,Cust_Name  from Service_Customer_Information ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Cust_Name", "Cust_Id");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
            }
            public string UpdateCustInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("UPDATE [Service_Customer_Information] SET Cust_Name='{1}',Cust_Company_Name='{2}',Cust_Contact_Person = '{3}',Cust_Mobile = '{4}',Cust_Email = '{5}',Cust_Address = '{6}',Add_Reference = '{7}' WHERE Cust_Code={0}", this.Cust_Code, this.Cust_Name, this.Cust_Company_Name, this.Cust_Contact_Person, this.Cust_Mobile, this.Cust_Email, this.Cust_Address,this.Add_Reference);

                //_commandText = string.Format("update [Service_Customer_Information] VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", this.Cust_Code, this.Cust_Name, this.Cust_Company_Name, this.Cust_Contact_Person, this.Cust_Mobile, this.Cust_Email, this.Cust_Address);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Customer Information Details", "95");


                }
                return _returnStringMessage;
            }

            public string UpdateCustUnitInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("INSERT  INTO [Service_Customer_Unit_Details] VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", this.Cust_ID, this.Cust_Unit_Name, this.Cust_Unit_Address, this.Unit_Contact_Person, this.Contact_Mobile, this.UnitEmail);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Customer Unit Information Details", "96");

                }
                return _returnStringMessage;
            }
            public string InsertCustUnitInfo()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                this.Cust_ID = AutoGenMaxId2("Service_Customer_Information", "Cust_Id");

                _commandText = string.Format("INSERT  INTO [Service_Customer_Unit_Details] VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", this.Cust_ID, this.Cust_Unit_Name, this.Cust_Unit_Address, this.Unit_Contact_Person, this.Contact_Mobile,this.UnitEmail);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Customer Unit Information Details", "96");

                }
                return _returnStringMessage;
            }

            public string DeleteUnitCustInfo(string Cust_ID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("DELETE FROM [Service_Customer_Unit_Details] WHERE Cust_Id={0}", Cust_ID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Delete("Customer Unit Information Details", "96");

                }
                return _returnStringMessage;
            }
            public int CustomerMaster_SelelctPONo(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_SO_MAST  inner join YANTRA_CUSTOMER_MAST  on YANTRA_CUSTOMER_MAST .CUST_ID =YANTRA_SO_MAST .SO_CUST_ID where YANTRA_SO_MAST .SO_ID ='" + SalesOrderId + "' ORDER BY [YANTRA_SO_MAST].SO_ID DESC");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.PONo = dbManager.DataReader["SO_NO"].ToString();
                    this.SODate = dbManager.DataReader["SO_DATE"].ToString();
                    this.SOCUSTID = dbManager.DataReader["CUST_ID"].ToString();
                    this.SOCustName = dbManager.DataReader["CUST_NAME"].ToString();
                    this.SOCompanyName = dbManager.DataReader["CUST_COMPANY_NAME"].ToString();
                    this.SOAddress = dbManager.DataReader["CUST_ADDRESS"].ToString();
                    this.SOMobileNO = dbManager.DataReader["CUST_MOBILE"].ToString();
                    this.SOPhoneNo = dbManager.DataReader["CUST_PHONE"].ToString();
                    this.SOContactPerson = dbManager.DataReader["CUST_CONTACT_PERSON"].ToString();
                    this.SOEmail = dbManager.DataReader["CUST_EMAIL"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public int ServiceCustMaster_Select(string CustomerId)
            {
                dbManager.Open();
                //_commandText = string.Format("SELECT * FROM Service_Customer_Information where Cust_Id= " + CustomerId);
                _commandText = string.Format("SELECT * FROM Service_Customer_Information where Cust_Id= " + CustomerId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Cust_Code = dbManager.DataReader["Cust_Code"].ToString();
                    this.Cust_Name = dbManager.DataReader["Cust_Name"].ToString();
                    this.Cust_Company_Name = dbManager.DataReader["Cust_Company_Name"].ToString();
                    this.Cust_Contact_Person = dbManager.DataReader["Cust_Contact_Person"].ToString();
                    this.Cust_Mobile = dbManager.DataReader["Cust_Mobile"].ToString();
                    this.Cust_Email = dbManager.DataReader["Cust_Email"].ToString();
                    this.Cust_Address = dbManager.DataReader["Cust_Address"].ToString();



                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                //  dbManager.Close();
                return _returnIntValue;
            }
            public int ServiceUnitMaster_Select(string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM Service_Customer_Unit_Details where Cust_Unit_Id = " + CustomerId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.Cust_Unit_Name = dbManager.DataReader["Cust_Unit_Name"].ToString();
                    this.Cust_Unit_Address = dbManager.DataReader["Cust_Unit_Address"].ToString();
                    this.Unit_Contact_Person = dbManager.DataReader["Unit_Contact_Person"].ToString();
                    this.Contact_Mobile = dbManager.DataReader["Contact_Mobile"].ToString();
                    this.UnitEmail = dbManager.DataReader["Email"].ToString();
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


        //Method for Commit Transaction
        public static void CommitTransaction()
        {
            if (dbManager.Connection != null)
                dbManager.CommitTransaction();
        }

        //Method for RollBack Transaction
        public static void RollBackTransaction()
        {
            if (dbManager.Connection != null)
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
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--", "0"));
            while (dbManager.DataReader.Read())
            {
                ddl.Items.Add(new ListItem(dbManager.DataReader[DataTextField].ToString(), dbManager.DataReader[DataValueField].ToString()));
            }
            dbManager.DataReader.Close();
        }

        //Method for GridBind Fill
        private static void GridViewBind(GridView gv)
        {
            gv.DataSource = dbManager.DataReader;
            gv.DataBind();
            dbManager.DataReader.Close();
        }

        //Method for Auto Generate Max Serial ID
        public static string AutoGenMaxId(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
            return _returnIntValue.ToString();
        }
        public static string AutoGenMaxId2(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0) FROM " + TableName + "").ToString());
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
            return year;
        }


        // Method for Complaint Register Form
        public class ComplaintRegister
        {
            public string CRId, startOTP, ClosedOtp, TechInTime,TechOutTime,CRTechComments,CRTechStatus,CRPendingRemarks, CRNo, CRDate, CustId, CustUnitId, CustUnitName, CustUnitAddress, CustContactPerson, CustDetId, ItemCode, CRCallType, CRComplaintNature, CRRootCause, CRCorrectiveAction, CRPreparedBy, CRSatus, CRStartOTP,CRClosedOTP,CRINTIME,CROutTime,TECHStatus,TechRemarks,CRPendingReason,CRPendingFollowUpDt;
            public string CRDetQty, CRDetSerialNo,Cp_Id,CRDetID;
            public string cust_unit_add, Dear;
            public string SR_DET_SR_DETID, attendby, CompletedDate, Courtesy_Date, Courtesy_Text, Courtesy_By;
            //This declaration is for Customer Information Saving pupose
            public string  CustCode, RegId, RegName, CustName, CompName, ContactPerson, Phone, Mobile, IndTypeId, IndType, Fax, Email, PANNo, ECCNo, CSTNo, LocalSTNo, SplInsrs, Address, Website, CorpContactPerson, CorpPhone, CorpMobile, CorpEmail, CorpAddress, DesgId, CorpDesgId, CorpFax, IsNewOrExisting,  CustCorpContactPerson, CustCorpPhone, CustCorpMobile, CustCorpEmail, CustCorpAddress, CustCorpDesgId, CustCorpFax,UnitNo, CustStatus;
            public string RegAttachments;
            public ComplaintRegister()
            { }

            public static string ComplaintRegisterDet_AutoGenCode1()
            {
                return SM.AutoGenMaxId("[YANTRA_COMPLAINT_REGISTER_Det]", "CR_DET_ID");
            }
            public static string ComplaintRegister_AutoGenCode1()
            {
               return SM. AutoGenMaxId("[YANTRA_COMPLAINT_REGISTER]", "CR_ID");
            }
            public static string ComplaintRegister_AutoGenCodeMast()
            {

                return SM.AutoGenMaxNo("YANTRA_COMPLAINT_REGISTER", "CR_NO");

            }
            public static string ComplaintRegister_AutoGenCode()
            {
               
                return SM.AutoGenMaxNo("YANTRA_COMPLAINT_REGISTER", "CR_NO");
                
            }
            public string ComplaintRegister_Save1()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                this.CRId = AutoGenMaxId("[YANTRA_COMPLAINT_REGISTER]", "CR_ID");
                this.CRNo = ComplaintRegister_AutoGenCodeMast();
                _commandText = string.Format("INSERT INTO [YANTRA_COMPLAINT_REGISTER] VALUES ({0},'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},{10},'{11}','{12}')", this.CRId, this.CRNo, this.CRDate, this.CustId, this.CustUnitId, this.CustDetId, this.CRCallType, this.CRPreparedBy, this.CRSatus, this.Cp_Id, this.attendby, this.CompletedDate,this.TECHStatus  );

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Compalint Register Details", "97");

                }

                return _returnStringMessage;
            }
            public string ComplaintRegister_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                this.CRId = AutoGenMaxId("[YANTRA_COMPLAINT_REGISTER]", "CR_ID");
                this.CRNo = ComplaintRegister_AutoGenCode();
                _commandText = string.Format("INSERT INTO [YANTRA_COMPLAINT_REGISTER] VALUES ({0},'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},{10},'{11}')", this.CRId, this.CRNo, this.CRDate, this.CustId, this.CustUnitId, this.CustDetId,this.CRCallType, this.CRPreparedBy, this.CRSatus,this.Cp_Id,this.attendby,this.CompletedDate);
             
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Compalint Register Details", "97");

                }

                return _returnStringMessage;
            }

            public string ComplaintRegister_Update_mast()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_COMPLAINT_REGISTER]", "CR_NO", this.CRNo, "CR_ID", this.CRId) == true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER] SET CR_DATE='{0}',CUST_ID={1},CUST_UNIT_ID={2},CUST_DET_ID={3},CR_CALL_TYPE='{5}',CR_PREPARED_BY={6},CP_ID={8},CR_Attendby = {9},CR_STATUS = '{10}',CR_COMPLETED = '{11}', Tech_Status='{12}' WHERE CR_ID={7}", this.CRDate, this.CustId, this.CustUnitId, this.CustDetId, this.ItemCode, this.CRCallType, this.CRPreparedBy, this.CRId, this.Cp_Id, this.attendby, this.CRSatus, this.CompletedDate, this.TECHStatus);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Compalint Register Details", "97");

                    }
                }
                else
                {
                    _returnStringMessage = "Complaint Already Exists.";
                }
                return _returnStringMessage;
            }

            public string ComplaintRegister_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_COMPLAINT_REGISTER]", "CR_NO", this.CRNo, "CR_ID", this.CRId) == true)
                {
                    _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER] SET CR_DATE='{0}',CUST_ID={1},CUST_UNIT_ID={2},CUST_DET_ID={3},CR_CALL_TYPE='{5}',CR_PREPARED_BY={6},CP_ID={8},CR_Attendby = {9},CR_STATUS = '{10}',CR_COMPLETED = '{11}' WHERE CR_ID={7}", this.CRDate, this.CustId, this.CustUnitId, this.CustDetId, this.ItemCode, this.CRCallType, this.CRPreparedBy, this.CRId, this.Cp_Id, this.attendby, this.CRSatus,this.CompletedDate);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Compalint Register Details", "97");

                    }
                }
                else
                {
                    _returnStringMessage = "Complaint Already Exists.";
                }
                return _returnStringMessage;
            }

            public string ComplaintRegister_Delete()
            {
                if (DeleteRecord("[YANTRA_COMPLAINT_REGISTER]", "CR_ID", this.CRId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Compalint Register Details", "97");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }
            public int ComplaintRegisterDetails_DeleteDet(string CRId)
            {
                if (DeleteRecord("[YANTRA_COMPLAINT_REGISTER_Det]", "CR_ID", CRId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }
            public int ComplaintRegisterDetails_Delete(string CRId)
            {
                if (DeleteRecord("[YANTRA_COMPLAINT_REGISTER_DET]", "CR_ID", CRId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }
            public string ComplaintRegisterDetails_Save1()
            {
                this.RevisedKey = "";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_COMPLAINT_REGISTER_Det] SELECT ISNULL(MAX(CR_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}' FROM [YANTRA_COMPLAINT_REGISTER_Det]", this.CRId, this.ItemCode, this.CRDetQty, this.CRComplaintNature, this.CRRootCause, this.CRCorrectiveAction, this.attendby,this.Courtesy_Date ,this.Courtesy_Text ,this.Courtesy_By ,this.TechInTime ,this.TechOutTime,this.CRStartOTP ,this.CRClosedOTP ,this.CRNo ,this.CRINTIME ,this.CROutTime ,this.TECHStatus ,this.TechRemarks,this.RevisedKey);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Compalint Register Details", "97");

                }
                return _returnStringMessage;
            }
            public string ComplaintRegisterDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_COMPLAINT_REGISTER_DET] SELECT ISNULL(MAX(CR_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}' FROM [YANTRA_COMPLAINT_REGISTER_DET]", this.CRId, this.ItemCode, this.CRDetQty,this.CRComplaintNature,this.CRRootCause,this.CRCorrectiveAction,this.attendby,this.Courtesy_Date ,Courtesy_Text ,Courtesy_By  );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Compalint Register Details", "97");

                }
                return _returnStringMessage;
            }

            public static void ComplaintRegister_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }
            public string docid, empid, docsub, date, empdocid;

            public string EmpDoc_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update Emp_Documents_Submitted set Document_Submitted ='{0}',emp_doc_id='{1}',Date_Submitted ='{2}' where Doc_Id ='{3}'  ",this.docsub ,this.empdocid ,this .date ,this.docid );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                // dbManager.Close();
                return _returnStringMessage;
            }
            public string EmpDoc_Save()
            {
                 if (dbManager.Transaction == null)
                    dbManager.Open();
                 _commandText = string.Format("insert into Emp_Documents_Submitted SELECT ISNULL(MAX(doc_id),0)+1,{0},'{1}','{2}','{3}' FROM Emp_Documents_Submitted  ",this.empid,this.docsub,this.date,this.empdocid);
                 _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                // dbManager.Close();
                return _returnStringMessage;
            }
            //Complaint Register Attachmnets saving
            public string ComplaintRegisterAttachment_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_COMPLAINT_REGISTER_ATTACHMENTS] SELECT ISNULL(MAX(COMPL_REG_ATTACHMENT_ID),0)+1,{0},'{1}' FROM [YANTRA_COMPLAINT_REGISTER_ATTACHMENTS]", this.CRId, this.RegAttachments);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                // dbManager.Close();
                return _returnStringMessage;

            }
            //Deleting Attachments
            public int ComplaintRegisterAttachment_Delete(string crid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_COMPLAINT_REGISTER_ATTACHMENTS] WHERE [CR_ID]={0}", crid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                // dbManager.Close();
                return _returnIntValue;
            }
            public string CustomerMaster_Save()
            {
                this.CustStatus = "New";
                this.CustCode = CustomerMaster_AutoGenCode();
                this.CustId = AutoGenMaxId("[YANTRA_CUSTOMER_MAST]", "CUST_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_CUSTOMER_MAST] VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}',{23},{24},'{25}','{26}','{27}',{28},'{29}')",
               this.CustId, this.CustCode, this.CustName, this.CompName, this.ContactPerson, this.Phone, this.Mobile, this.Fax, this.Email, this.Website, this.PANNo, this.ECCNo, this.CSTNo, this.LocalSTNo, this.RegId, this.IndTypeId, this.Address, this.SplInsrs, this.CorpContactPerson, this.CorpPhone, this.CorpMobile, this.CorpEmail, this.CorpAddress, this.DesgId, this.CorpDesgId, this.CorpFax, this.IsNewOrExisting, this.CustStatus, cp.getPresentCompanySessionValue(),this.Dear);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Customer Information Details", "95");

                }
                return _returnStringMessage;
            }


           
            public static string CustomerMaster_AutoGenCode()
            {
                return SM.AutoGenMaxNo("YANTRA_CUSTOMER_MAST", "CUST_CODE");
            }

            public void ComplaintRegisterDetails_SelectDet(string CRId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT *,[YANTRA_COMPLAINT_REGISTER_Det].CR_NO+' '+[YANTRA_COMPLAINT_REGISTER_Det].Revised_Key AS CRNO, Convert(varchar(5),DateDiff(s, Tech_StartDt , Tech_EndDt )/3600)+':'+convert(varchar(5),DateDiff(s, Tech_StartDt , Tech_EndDt )%3600/60)+':'+convert(varchar(5),(DateDiff(s, Tech_StartDt , Tech_EndDt )%60)) as TechTimeDuration,convert(varchar(50),DateDiff(s, Start_Dt, End_Dt)/3600)+':'+convert(varchar(50),DateDiff(s, Start_Dt , End_Dt )%3600/60)+':'+convert(varchar(50),(DateDiff(s, Start_Dt , End_Dt )%60)) as CompTimeDuration FROM [YANTRA_COMPLAINT_REGISTER_Det] WHERE  " +
                                               "[YANTRA_COMPLAINT_REGISTER_Det].CR_ID=" + CRId + " Order By CR_DET_ID Desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable AMCQuotationItems = new DataTable();
                DataColumn col = new DataColumn();

                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("NatureofComplaint");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("RootCausedNotice");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CorrectiveActionTaken");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("AttendBy");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Tech_StartDt");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Tech_EndDt");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Tech_StartOTP");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Tech_EndOTP");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CR_NO");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Start_Dt");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("End_Dt");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CR_DET_ID");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CR_ID");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Tech_Remarks");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Tech_Status");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Revised_Key");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("TechTimeDuration");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CompTimeDuration");
                AMCQuotationItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = AMCQuotationItems.NewRow();
                    dr["ItemName"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["Quantity"] = dbManager.DataReader["CR_DET_QTY"].ToString();
                    dr["NatureofComplaint"] = dbManager.DataReader["CR_NATURE_OF_COMPLAINT"].ToString();
                    dr["RootCausedNotice"] = dbManager.DataReader["CR_ROOT_CAUSE_NOTICED"].ToString();
                    dr["CorrectiveActionTaken"] = dbManager.DataReader["CR_CORRECTIVE_ACTION_TAKEN"].ToString();
                    dr["AttendBy"] = dbManager.DataReader["Attended_By"].ToString();
                    dr["Tech_StartDt"] = dbManager.DataReader["Tech_StartDt"].ToString();
                    dr["Tech_EndDt"] = dbManager.DataReader["Tech_EndDt"].ToString();
                    dr["Tech_StartOTP"] = dbManager.DataReader["Tech_StartOTP"].ToString();
                    dr["Tech_EndOTP"] = dbManager.DataReader["Tech_EndOTP"].ToString();
                    dr["CR_NO"] = dbManager.DataReader["CRNO"].ToString();
                    dr["Start_Dt"] = dbManager.DataReader["Start_Dt"].ToString();
                    dr["End_Dt"] = dbManager.DataReader["End_Dt"].ToString();
                    dr["CR_DET_ID"] = dbManager.DataReader["CR_DET_ID"].ToString();
                    dr["CR_ID"] = dbManager.DataReader["CR_ID"].ToString();
                    dr["Tech_Remarks"] = dbManager.DataReader["Tech_Remarks"].ToString();
                    dr["Tech_Status"] = dbManager.DataReader["Tech_Status"].ToString();
                    dr["Revised_Key"] = dbManager.DataReader["Revised_Key"].ToString();
                    dr["TechTimeDuration"] = dbManager.DataReader["TechTimeDuration"].ToString();
                    dr["CompTimeDuration"] = dbManager.DataReader["CompTimeDuration"].ToString();
                    AMCQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = AMCQuotationItems;
                gv.DataBind();
            }

            public int ComplaintRegisterDetID_Select(string CRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Select * from YANTRA_COMPLAINT_REGISTER_DET where CR_ID=" + CRId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CRDetID  = dbManager.DataReader["CR_DET_ID"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public void ComplaintRegisterDetails_Select(string CRId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER_DET] WHERE  " +
                                               "[YANTRA_COMPLAINT_REGISTER_DET].CR_ID=" + CRId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable AMCQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
              
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("NatureofComplaint");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("RootCausedNotice");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CorrectiveActionTaken");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("AttendBy");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CR_DET_ID");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Courtesy_Text");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Courtesy_By");
                AMCQuotationItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = AMCQuotationItems.NewRow();
                    dr["ItemName"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["Quantity"] = dbManager.DataReader["CR_DET_QTY"].ToString();
                    dr["NatureofComplaint"] = dbManager.DataReader["CR_NATURE_OF_COMPLAINT"].ToString();
                    dr["RootCausedNotice"] = dbManager.DataReader["CR_ROOT_CAUSE_NOTICED"].ToString();
                    dr["CorrectiveActionTaken"] = dbManager.DataReader["CR_CORRECTIVE_ACTION_TAKEN"].ToString();
                    dr["AttendBy"] = dbManager.DataReader["Attended_By"].ToString();
                    dr["CR_DET_ID"] = dbManager.DataReader["CR_DET_ID"].ToString();
                    dr["Courtesy_Text"] = dbManager.DataReader["Courtesy_Text"].ToString();
                    dr["Courtesy_By"] = dbManager.DataReader["Courtesy_By"].ToString();

                    AMCQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = AMCQuotationItems;
                gv.DataBind();
            }

            public void ComplaintRegisterDetails_Select1(string CRId, GridView gv)
            {
               if (dbManager.Transaction == null)
                 dbManager.Open();
             //_commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER_DET],[YANTRA_ITEM_MAST],YANTRA_LKUP_ITEM_TYPE,yantra_service_report_mast,YANTRA_SERVICE_REPORT_DET  WHERE YANTRA_SERVICE_REPORT_DET.SRDET_ITEM_NAME=YANTRA_ITEM_MAST.ITEM_NAME AND [YANTRA_COMPLAINT_REGISTER_DET].CR_ID=yantra_service_report_mast.cr_id and YANTRA_SERVICE_REPORT_DET.SRDET_SR_NO=yantra_service_report_mast.SR_NO AND YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
             //                                  "[YANTRA_COMPLAINT_REGISTER_DET].CR_ID=" + CRId + "");    //  [YANTRA_COMPLAINT_REGISTER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE and
               _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],yantra_service_report_mast,YANTRA_SERVICE_REPORT_DET WHERE  [YANTRA_COMPLAINT_REGISTER].CR_ID=yantra_service_report_mast.cr_id and YANTRA_SERVICE_REPORT_DET.SRDET_SR_NO=yantra_service_report_mast.SR_NO ANd " +
                                           "[YANTRA_COMPLAINT_REGISTER].CR_ID=" + CRId + "");    //  [YANTRA_COMPLAINT_REGISTER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE and

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable QuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Description");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("ActionTaken");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("FurtherAction");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Status");
                QuotationItems.Columns.Add(col); 
                col = new DataColumn("ItemTypeId");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("SER_DET_ID");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("Date Of Comp");
                QuotationItems.Columns.Add(col);
                col = new DataColumn("CustomerFeedBack");
                QuotationItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = QuotationItems.NewRow();
                    dr["ItemType"] = dbManager.DataReader["SRDET_ITEM_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["SRDET_ITEM_NAME"].ToString();
                    dr["Description"] = dbManager.DataReader["SRDET_DESCRIPTION"].ToString();
                    dr["ActionTaken"] = dbManager.DataReader["SRDET_ACTION_TAKEN"].ToString();
                    dr["FurtherAction"] = dbManager.DataReader["SRDET_FOLLOWUP"].ToString();
                    dr["Status"] = dbManager.DataReader["SRDET_STATUS"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["SRDET_ITEM_TYPE"].ToString();
                      dr["SER_DET_ID"] = dbManager.DataReader["SRDET_DET_ID"].ToString();
                      dr["Date Of Comp"] = dbManager.DataReader["Date_Of_Comp"].ToString();
                    dr["CustomerFeedBack"]=dbManager.DataReader["CustomerFeedBack"].ToString();
                    QuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = QuotationItems;
                gv.DataBind();
            }
            public void ComplaintRegisterDetails_Select3(string CRId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER_DET],[YANTRA_ITEM_MAST],YANTRA_LKUP_ITEM_TYPE,yantra_service_report_mast,YANTRA_SERVICE_REPORT_DET  WHERE [YANTRA_COMPLAINT_REGISTER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND YANTRA_SERVICE_REPORT_DET.SRDET_STATUS='OPEN' and YANTRA_SERVICE_REPORT_DET.SRDET_ITEM_NAME=YANTRA_ITEM_MAST.ITEM_NAME AND [YANTRA_COMPLAINT_REGISTER_DET].CR_ID=yantra_service_report_mast.cr_id and YANTRA_SERVICE_REPORT_DET.SRDET_SR_NO=yantra_service_report_mast.SR_NO AND YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_COMPLAINT_REGISTER_DET].CR_ID=" + CRId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable AMCQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("NatureofComplaint");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("RootCausedNotice");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("CorrectiveActionTaken");
                AMCQuotationItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = AMCQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["Quantity"] = dbManager.DataReader["CR_DET_QTY"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SerialNo"] = dbManager.DataReader["CR_DET_SERIALNO"].ToString();
                    dr["NatureofComplaint"] = dbManager.DataReader["CR_NATURE_OF_COMPLAINT"].ToString();
                    dr["RootCausedNotice"] = dbManager.DataReader["SRDET_ACTION_TAKEN"].ToString();
                    dr["CorrectiveActionTaken"] = dbManager.DataReader["SRDET_FOLLOWUP"].ToString();
                   // dr["Description"] = dbManager.DataReader["SRDET_DESCRIPTION"].ToString();
                   // dr["ActionTaken"] = dbManager.DataReader["SRDET_ACTION_TAKEN"].ToString();
                   // dr["FurtherAction"] = dbManager.DataReader["SRDET_FOLLOWUP"].ToString();
                   // dr["Status"] = dbManager.DataReader["SRDET_STATUS"].ToString();
                   // dr["ItemTypeId"] = dbManager.DataReader["SRDET_ITEM_TYPE"].ToString();



                    AMCQuotationItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = AMCQuotationItems;
                gv.DataBind();
            }
            public static void ComplaintRegisterForComplaint_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Complaint' ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }
            public static void ComplaintRegisterForTechnical_Guidance_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Technical Guidance' ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }
            public static void ComplaintRegisterForInstallation_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Installation' ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }
            public static void ComplaintRegisterForInstallation_Select1(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER]  ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }
            public static void ComplaintRegisterForAMC_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT CR_NO,CR_ID FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='AMC' ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }

            public static void ComplaintRegisterForSparesInstall_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Non Warranty' ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }

            public static void ComplaintRegisterForWarranty_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Warranty' ORDER BY CR_Id desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }
            //public static void ComplaintRegisterForInstallation_Select1(Control ControlForBind)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Installation' and CR_STATUS<>'CLOSED' ORDER BY CR_Id desc");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
            //    }
            //}

            //public static void ComplaintRegisterForAMC_Select1(Control ControlForBind)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='AMC' and CR_STATUS<>'CLOSED' ORDER BY CR_Id desc");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
            //    }
            //}

            //public static void ComplaintRegisterForSparesInstall_Select1(Control ControlForBind)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Non Warranty' and CR_STATUS<>'CLOSED' ORDER BY CR_Id desc");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
            //    }
            //}

            //public static void ComplaintRegisterForWarranty_Select1(Control ControlForBind)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_CALL_TYPE='Warranty' and CR_STATUS<>'CLOSED' ORDER BY CR_Id desc");
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (ControlForBind is DropDownList)
            //    {
            //        DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
            //    }
            //}

            public static void complanitmodelnofill(string cid, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_COMPLAINT_REGISTER_DET,YANTRA_ITEM_MAST where YANTRA_COMPLAINT_REGISTER_DET.ITEM_CODE = YANTRA_ITEM_MAST.item_code and YANTRA_COMPLAINT_REGISTER_DET.CR_DET_ID =" + cid +"");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }


            }
            public int CompRegister_Select(string CRID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select *,[YANTRA_COMPLAINT_REGISTER_Det].CR_NO+' '+[YANTRA_COMPLAINT_REGISTER_Det].Revised_Key AS CRNO from YANTRA_COMPLAINT_REGISTER inner join Service_Customer_Information on YANTRA_COMPLAINT_REGISTER .CUST_ID =Service_Customer_Information .Cust_Id inner join Service_Customer_Unit_Details on YANTRA_COMPLAINT_REGISTER .CUST_UNIT_ID =Service_Customer_Unit_Details .Cust_Unit_Id inner join YANTRA_COMPLAINT_REGISTER_Det on YANTRA_COMPLAINT_REGISTER .CR_ID =YANTRA_COMPLAINT_REGISTER_Det.CR_ID  where YANTRA_COMPLAINT_REGISTER_Det.CR_DET_ID  =" + CRID + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    this.CRNo = dbManager.DataReader["CRNO"].ToString();
                    this.CRDate = dbManager.DataReader["CR_DATE"].ToString();

                //this.CRDate = Convert.ToDateTime(dbManager.DataReader["CR_DATE"].ToString()).ToString("dd/MM/yyyy");
                this.CustName = dbManager.DataReader["Cust_Unit_Name"].ToString();
                this.CustCorpMobile  = dbManager.DataReader["Contact_Mobile"].ToString();
                this.CustUnitAddress = dbManager.DataReader["CUST_UNIT_Address"].ToString();
                this.CRStartOTP = dbManager.DataReader["Tech_StartOTP"].ToString();
                this.CRClosedOTP = dbManager.DataReader["Tech_EndOTP"].ToString();
                this.CRINTIME = dbManager.DataReader["Tech_StartDt"].ToString();
                this.CROutTime = dbManager.DataReader["Tech_EndDt"].ToString();
                this.CRTechComments = dbManager.DataReader["TECH_REMARKS"].ToString();
                this.CRTechStatus  = dbManager.DataReader["TECH_STATUS"].ToString();
                //this.CRPendingRemarks = dbManager.DataReader["CR_PENDING_REMARKS"].ToString();
                this.CRCallType  = dbManager.DataReader["CR_CALL_TYPE"].ToString();
                this.CRDetID = dbManager.DataReader["CR_DET_ID"].ToString();
                this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                this.CRComplaintNature = dbManager.DataReader["CR_NATURE_OF_COMPLAINT"].ToString();
                  _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public int ComplaintRegisterDetails_SelectForRevised(string CRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER_Det] WHERE CR_ID = " + CRId +" Order by cr_det_id Desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                 if (dbManager.DataReader.Read())
                {
               
                this.CRId = dbManager.DataReader["CR_ID"].ToString();
                this.CRDetID  = dbManager.DataReader["CR_DET_ID"].ToString();
                this.CRNo = dbManager.DataReader["CR_No"].ToString();
                this.RevisedKey = dbManager.DataReader["Revised_Key"].ToString();
                this.startOTP = dbManager.DataReader["Tech_StartOtp"].ToString();
                this.ClosedOtp = dbManager.DataReader["Tech_EndOTP"].ToString();

                _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public int ComplaintRegister_Select1(string CRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_ID = " + CRId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    this.CRNo = dbManager.DataReader["CR_NO"].ToString();
                    this.CRDate = dbManager.DataReader["CR_DATE"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.CRCallType = dbManager.DataReader["CR_CALL_TYPE"].ToString();
                    this.CRPreparedBy = dbManager.DataReader["CR_PREPARED_BY"].ToString();
                    this.CRSatus = dbManager.DataReader["CR_STATUS"].ToString();
                    this.attendby = dbManager.DataReader["CR_Attendby"].ToString();
                    this.CompletedDate = dbManager.DataReader["CR_COMPLETED"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }


            public int ComplaintRegister_Select(string CRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] WHERE CR_ID = " + CRId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    this.CRNo = dbManager.DataReader["CR_NO"].ToString();
                    this.CRDate = Convert.ToDateTime(dbManager.DataReader["CR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.CRCallType = dbManager.DataReader["CR_CALL_TYPE"].ToString();
                    this.CRPreparedBy = dbManager.DataReader["CR_PREPARED_BY"].ToString();
                    this.CRSatus = dbManager.DataReader["CR_STATUS"].ToString();
                    this.attendby = dbManager.DataReader["CR_Attendby"].ToString();
                    this.CompletedDate = Convert.ToDateTime(dbManager.DataReader["CR_COMPLETED"].ToString()).ToString("dd/MM/yyyy");
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public enum CRStatus { New = 0, Open = 1, Closed = 2, Cancelled = 3, Regret = 4, ReOpened = 5, Obsolete = 6, ManuallyClosed = 7 }
            public string RevisedKey;
            public string Task_Revise()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(REVISED_KEY,'R','')),0)+1 FROM YANTRA_COMPLAINT_REGISTER_Det WHERE CR_NO LIKE '" + this.CRNo + "%'").ToString());
                this.RevisedKey = "R" + _returnIntValue.ToString();

                TaskStatusUpdate(CRStatus.ReOpened, this.CRDetID);
                _commandText = string.Format("INSERT INTO [YANTRA_COMPLAINT_REGISTER_Det] SELECT ISNULL(MAX(CR_DET_ID),0)+1,{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}' FROM [YANTRA_COMPLAINT_REGISTER_Det]", this.CRId,this.ItemCode, this.CRDetQty, this.CRComplaintNature, this.CRRootCause, this.CRCorrectiveAction, this.attendby,this.Courtesy_Date ,this.Courtesy_Text ,Courtesy_By , this.TechInTime, this.TechOutTime, this.CRStartOTP, this.CRClosedOTP, this.CRNo, this.CRINTIME, this.CROutTime, this.TECHStatus, this.TechRemarks, this.RevisedKey);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;

                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    //log.add_Update("Compalint Register Details", "97");

                }
                return _returnStringMessage;
            }

            public static string TaskStatusUpdate(CRStatus Status, string CRDetID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  TECH_STATUS FROM [YANTRA_COMPLAINT_REGISTER_Det] WHERE CR_DET_ID='{0}'", CRDetID);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == CRStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == CRStatus.ManuallyClosed .ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER_Det] SET TECH_STATUS='{0}' WHERE CR_DET_ID='{1}'", Status, CRDetID);
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
                    //log.add_Update("Sales Quotation Status Details", "119");

                }
                return _returnStringMessage;
            }
            public int Task_Delete(string CRDetID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_COMPLAINT_REGISTER_Det] WHERE CR_DET_ID={0}", CRDetID );

                //log.add_Delete("Delivery Challan Item Details", "62");

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            public string Courtsey_Update(string CRDetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update YANTRA_COMPLAINT_REGISTER_DET set Courtesy_Text ='{0}',Courtesy_By ='{1}',Courtesy_Date ='{2}' where CR_DET_ID ='{3}' ", this.Courtesy_Text , this.Courtesy_By , this.Courtesy_Date , this.CRDetID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    //log.add_Update("Compalint Register Details", "97");

                }
                return _returnStringMessage;
            }
            public string Task_Update(string CRDetID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("update YANTRA_COMPLAINT_REGISTER_Det set CR_ROOT_CAUSE_NOTICED ='{0}',CR_CORRECTIVE_ACTION_TAKEN ='{1}',End_Dt='{2}',Tech_Remarks='{3}' where CR_DET_ID='{4}' ", this.CRRootCause, CRCorrectiveAction, this.CROutTime,this.TechRemarks,this.CRDetID  );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    //log.add_Update("Compalint Register Details", "97");

                }
                return _returnStringMessage;
            }
            public static string CompalintRegisterStatus_Update(ServicesStatus Status, string CrId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER] SET  CR_STATUS='{0}' WHERE CR_ID='{1}'", Status, CrId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Compalint Register Details", "97");

                }
                return _returnStringMessage;
            }


        }

        
        //Methods For Customer Master Form
        public class CustomerMaster
        {
            public string CustId, CustCode, RegId, RegName, CustName, CompName, ContactPerson, Phone, Mobile, IndTypeId, IndType, Fax, Email, PANNo, ECCNo, CSTNo, LocalSTNo, SplInsrs, Address, Website;   //Customer Master
            public CustomerMaster()
            { }

            public string CustomerMaster_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_CUSTOMER_MAST]", "CUST_NAME", this.CustName) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_CUSTOMER_MAST] SELECT ISNULL(MAX(CUST_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}' FROM [YANTRA_CUSTOMER_MAST]", this.CustCode, this.CustName, this.CompName, this.ContactPerson, this.Phone, this.Mobile, this.Fax, this.Email, this.Website, this.PANNo, this.ECCNo, this.CSTNo, this.LocalSTNo, this.RegId, this.IndTypeId, this.Address, this.SplInsrs, this.CustId, this.RegId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Customer Information Details", "95");

                    }
                }
                else
                {
                    _returnStringMessage = "Customer Name Already Exists.";
                }
                return _returnStringMessage;
            }

            public string CustomerMaster_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_CUSTOMER_MAST]", "CUST_NAME", this.CustName, "CUST_ID", this.CustId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_CUSTOMER_MAST] SET CUST_NAME='{0}',CUST_COMPANY_NAME='{1}',CUST_CONTACT_PERSON='{2}',CUST_PHONE='{3}',CUST_MOBILE='{4}',CUST_FAX='{5}',CUST_EMAIL='{6}',CUST_WEBSITE='{7}',CUST_PAN='{8}',CUST_ECC='{9}',CUST_CST='{10}',CUST_LOCAL_ST_NO='{11}',REG_ID='{12}',IND_TYPE_ID='{13}',CUST_ADDRESS='{14}',CUST_SPL_INSTRS='{15}' WHERE CUST_ID={16}", this.CustName, this.CompName, this.ContactPerson, this.Phone, this.Mobile, this.Fax, this.Email, this.Website, this.PANNo, this.ECCNo, this.CSTNo, this.LocalSTNo, this.RegId, this.IndTypeId, this.Address, this.SplInsrs, this.CustId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Customer Information Details", "95");

                    }
                }
                else
                {
                    _returnStringMessage = "Customer Name Already Exists.";
                }
                return _returnStringMessage;
            }

            public string CustomerMaster_Delete()
            {
                if (DeleteRecord("[YANTRA_CUSTOMER_MAST]", "CUST_ID", this.CustId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Customer Information Details", "95");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public static string CustomerMaster_AutoGenCode()
            {
                string _codePrefix = "CUST/";
                dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(CUST_ID,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_CUSTOMER_MAST]").ToString());
                return _codePrefix + _returnIntValue;
            }

            public int CustomerMaster_Select(string CustomerId)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST],[YANTRA_LKUP_INDUSTRY_TYPE],[YANTRA_LKUP_REGION] WHERE " +
                                " [YANTRA_CUSTOMER_MAST].IND_TYPE_ID=[YANTRA_LKUP_INDUSTRY_TYPE].IND_TYPE_ID AND " +
                                " [YANTRA_CUSTOMER_MAST].REG_ID=[YANTRA_LKUP_REGION].REG_ID AND [YANTRA_CUSTOMER_MAST].CUST_ID= " + CustomerId);
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
                    this.RegId = dbManager.DataReader["REG_ID"].ToString();
                    this.RegName = dbManager.DataReader["REG_NAME"].ToString();
                    this.SplInsrs = dbManager.DataReader["CUST_SPL_INSTRS"].ToString();
                    this.Website = dbManager.DataReader["CUST_WEBSITE"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static void CustomerMaster_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] ORDER BY CUST_COMPANY_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CUST_COMPANY_NAME", "CUST_ID");
                }
            }
        }

        //Methods For AMC Enquiry Form
        public class AMCEnquiry
        {
            public string EnqId, EnqNo, EnqDate, CustId, CustName, EnqModeId, EnqOrigBy, EnqOrigName, EnqRef, EnqFollowUp, EnqDeliveryDate, DespModeId, PromotionType, PromotionActivity, EnqPriority, EnqStatus, EnqDueDate, EnqDesc;
            public string EnqDetItemCode, EnqDetQty, EnqDetSpec, EnqDetRemarks, EnqDetPriority;

            public AMCEnquiry()
            {
            }

            public static string AMCEnquiry_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                //string _codePrefix = "ENQ/";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(ENQ_NO,LEFT(ENQ_NO,5),''))),0)+1 FROM [YANTRA_AMC_ENQUIRY_MAST]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ENQ_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_ENQUIRY_MAST]").ToString());
                return _codePrefix + _returnIntValue;
            }

            public string AMCEnquiry_Save()
            {
                this.EnqNo = AMCEnquiry_AutoGenCode();
                this.EnqId = AutoGenMaxId("[YANTRA_AMC_ENQUIRY_MAST]", "ENQ_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_ENQUIRY_MAST] VALUES({0},'{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}','{15}')", this.EnqId, this.EnqNo, this.EnqDate, this.CustId, this.EnqModeId, this.EnqOrigBy, this.EnqOrigName, this.EnqRef, this.EnqFollowUp, this.EnqDeliveryDate, this.DespModeId, this.PromotionType, this.PromotionActivity, this.EnqStatus, this.EnqDueDate, this.EnqDesc);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Enquiry Details", "98");

                }
                return _returnStringMessage;
            }

            public string AMCEnquiry_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_ENQUIRY_MAST] SET ENQ_DATE='{0}',CUST_ID={1},ENQM_ID={2},ENQ_ORIG_BY='{3}',ENQ_ORIG_NAME='{4}',ENQ_REFERENCE='{5}',ENQ_FOLLOWUP_CITERIA='{6}',ENQ_DELIVERY_DATE='{7}',DESM_ID={8},PROMOTION_TYPE='{9}',PROMOTION_ACTIVITY='{10}',ENQ_STATUS='{11}',ENQ_DUE_DATE='{12}',ENQ_DESC='{13}' WHERE ENQ_ID='{14}'", this.EnqDate, this.CustId, this.EnqModeId, this.EnqOrigBy, this.EnqOrigName, this.EnqRef, this.EnqFollowUp, this.EnqDeliveryDate, this.DespModeId, this.PromotionType, this.PromotionActivity, this.EnqStatus, this.EnqDueDate, this.EnqDesc, this.EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Enquiry Details", "98");

                }
                return _returnStringMessage;
            }

            public string AMCEnquiry_Delete(string EnquiryId)
            {
                if (DeleteRecord("[YANTRA_AMC_ENQUIRY_DET]", "ENQ_ID", EnquiryId) == true)
                {
                    if (DeleteRecord("[YANTRA_AMC_ENQUIRY_MAST]", "ENQ_ID", EnquiryId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("AMC Enquiry Details", "98");

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

            public string AMCEnquiryDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_ENQUIRY_DET] SELECT ISNULL(MAX(ENQ_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}' FROM [YANTRA_AMC_ENQUIRY_DET]", this.EnqId, this.EnqDetItemCode, this.EnqDetQty, this.EnqDetSpec, this.EnqDetRemarks, this.EnqDetPriority);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Enquiry Details", "98");

                }
                return _returnStringMessage;
            }

            public int AMCEnquiryDetails_Delete(string EnquiryId)
            {
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_commandText = string.Format("DELETE FROM [YANTRA_AMC_ENQUIRY_DET] WHERE ENQ_ID={0}", EnquiryId);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                if (DeleteRecord("[YANTRA_AMC_ENQUIRY_DET]", "ENQ_ID", EnquiryId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }

            public int AMCEnquiry_Select(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST] WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND " +
                                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_ENQUIRY_MAST].ENQ_ID='" + EnquiryId + "' ORDER BY [YANTRA_AMC_ENQUIRY_MAST].ENQ_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.EnqId = dbManager.DataReader["ENQ_ID"].ToString();
                    this.EnqNo = dbManager.DataReader["ENQ_NO"].ToString();
                    this.EnqDate = dbManager.DataReader["ENQ_DATE"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.EnqModeId = dbManager.DataReader["ENQM_ID"].ToString();
                    this.EnqOrigBy = dbManager.DataReader["ENQ_ORIG_BY"].ToString();
                    this.EnqOrigName = dbManager.DataReader["ENQ_ORIG_NAME"].ToString();
                    this.EnqRef = dbManager.DataReader["ENQ_REFERENCE"].ToString();
                    this.EnqFollowUp = dbManager.DataReader["ENQ_FOLLOWUP_CITERIA"].ToString();
                    this.EnqDeliveryDate = dbManager.DataReader["ENQ_DELIVERY_DATE"].ToString();
                    this.DespModeId = dbManager.DataReader["DESM_ID"].ToString();
                    this.PromotionType = dbManager.DataReader["PROMOTION_TYPE"].ToString();
                    this.PromotionActivity = dbManager.DataReader["PROMOTION_ACTIVITY"].ToString();
                    this.EnqStatus = dbManager.DataReader["ENQ_STATUS"].ToString();
                    this.EnqDueDate = dbManager.DataReader["ENQ_DUE_DATE"].ToString();
                    this.EnqDesc = dbManager.DataReader["ENQ_DESC"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string AMCEnquiryStatus_Update(ServicesStatus Status, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_ENQUIRY_MAST] SET  ENQ_STATUS='{0}' WHERE ENQ_ID='{1}'", Status, EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("AMC Enquiry Status Details", "98");

                }
                return _returnStringMessage;
            }

            public void AMCEnquiryDetails_Select(string EnquiryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM] WHERE [YANTRA_AMC_ENQUIRY_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_AMC_ENQUIRY_DET].ENQ_ID=" + EnquiryId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable EnquiryInterestedProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
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

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = EnquiryInterestedProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["ENQ_DET_QTY"].ToString();
                    dr["Specifications"] = dbManager.DataReader["ENQ_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["ENQ_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["ENQ_DET_PRIORITY"].ToString();
                    EnquiryInterestedProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = EnquiryInterestedProducts;
                gv.DataBind();
            }

            public static void AMCEnquiry_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST] ORDER BY ENQ_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ENQ_NO", "ENQ_ID");
                }
            }
        }

        //Methods for AMC Assignments
        public class AMCAssignments
        {
            public string EnqId, EnqNo, EnqDate, EmpId, AssignTaskId, AssignTaskNo, AssingDate, DueDate, AssignRemarks, AssignStatus, CustId, EnqAssignFollowUpDet_Id, FollowUpEmpId, FollowUpDate, FollowUpDesc;

            public AMCAssignments()
            {
            }

            public static string AMCAssignments_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(AMC_ASSIGN_TASK_NO,LEFT(AMC_ASSIGN_TASK_NO,5),''))),0)+1 FROM [YANTRA_AMC_ENQ_ASSIGN_TASKS]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ASSIGN_TASK_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_ENQ_ASSIGN_TASKS]").ToString());
                return _codePrefix + _returnIntValue;
            }

            public string AMCAssignments_Save()
            {
                this.AssignTaskNo = AMCAssignments_AutoGenCode();
                this.AssignTaskId = AutoGenMaxId("[YANTRA_AMC_ENQ_ASSIGN_TASKS]", "AMC_ASSIGN_TASK_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_AMC_ENQ_ASSIGN_TASKS] VALUES({0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}')", this.AssignTaskId, this.AssignTaskNo, this.EnqId, this.EmpId, this.AssingDate, this.DueDate, this.AssignRemarks, this.AssignStatus);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Assignments Details", "99");

                }
                return _returnStringMessage;
            }

            public string AMCAssignments_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_ENQ_ASSIGN_TASKS] SET  EMP_ID={0},AMC_ASSIGN_DATE='{1}',AMC_DUE_DATE='{2}',AMC_REMARKS='{3}' WHERE CR_ID={4}", this.EmpId, this.AssingDate, this.DueDate, this.AssignRemarks, this.EnqId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Assignments Details", "99");

                }
                return _returnStringMessage;
            }

            public string AMCAssignments_Delete(string AssignTaskId)
            {
                if (DeleteRecord("[YANTRA_AMC_ENQ_ASSIGN_TASKS]", "AMC_ASSIGN_TASK_ID", AssignTaskId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("AMC Assignments Details", "99");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static string AMCAssignmentsStatus_Update(ServicesStatus Status, string AssignTaskId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_ENQ_ASSIGN_TASKS] SET  AMC_ASSIGN_STATUS='{0}' WHERE AMC_ASSIGN_TASK_ID='{1}'", Status, AssignTaskId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("AMC Assignments Status Details", "99");

                }
                return _returnStringMessage;
            }

            public string AMCAssignmentsFollowUp_Save()
            {
                this.EnqAssignFollowUpDet_Id = AutoGenMaxId("[YANTRA_AMC_ENQ_ASSIGN_FOLLOWUP_DET]", "ENQ_ASSIGN_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_AMC_ENQ_ASSIGN_FOLLOWUP_DET] VALUES({0},{1},{2},'{3}','{4}')", this.EnqAssignFollowUpDet_Id, this.AssignTaskId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Assignments FollowUp Details", "99");

                }
                return _returnStringMessage;
            }

            public int AMCAssignments_Select(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],[YANTRA_AMC_ENQ_ASSIGN_TASKS],[YANTRA_CUSTOMER_MAST] WHERE [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_AMC_ENQ_ASSIGN_TASKS].CR_ID AND [YANTRA_CUSTOMER_MAST].CUST_ID= [YANTRA_COMPLAINT_REGISTER].CUST_ID AND " +
                                            " [YANTRA_COMPLAINT_REGISTER].CR_ID ='" + EnquiryId + "' ORDER BY [YANTRA_AMC_ENQ_ASSIGN_TASKS].AMC_ASSIGN_TASK_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AssignTaskId = dbManager.DataReader["AMC_ASSIGN_TASK_ID"].ToString();
                    this.AssignTaskNo = dbManager.DataReader["AMC_ASSIGN_TASK_NO"].ToString();
                    this.EnqId = dbManager.DataReader["CR_ID"].ToString();
                    this.EnqNo = dbManager.DataReader["CR_NO"].ToString();
                    this.EnqDate = dbManager.DataReader["CR_DATE"].ToString();
                    this.EmpId = dbManager.DataReader["EMP_ID"].ToString();
                    this.AssingDate = dbManager.DataReader["AMC_ASSIGN_DATE"].ToString();
                    this.DueDate = dbManager.DataReader["AMC_DUE_DATE"].ToString();
                    this.AssignRemarks = dbManager.DataReader["AMC_REMARKS"].ToString();
                    this.AssignStatus = dbManager.DataReader["AMC_ASSIGN_STATUS"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            //public int SalesAssignmentsFollowUp_Select(string AssignTaskId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQ_ASSIGN_FOLLOWUP_DET],[YANTRA_AMC_ENQ_ASSIGN_TASKS] WHERE [YANTRA_AMC_ENQ_ASSIGN_FOLLOWUP_DET].ASSIGN_TASK_ID=[YANTRA_AMC_ENQ_ASSIGN_TASKS].ASSIGN_TASK_ID AND " +
            //                                " [YANTRA_AMC_ENQ_ASSIGN_TASKS].ASSIGN_TASK_ID ='" + AssignTaskId + "' ORDER BY [YANTRA_AMC_ENQ_ASSIGN_FOLLOWUP_DET].ENQ_ASSIGN_FOLLOWUP_DET_ID DESC ");
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

            public static void AMCAssignments_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQ_ASSIGN_TASKS] ORDER BY ASSIGN_TASK_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ASSIGN_TASK_NO", "ASSIGN_TASK_ID");
                }
            }
        }

        //Methods for AMC Quotation Form
        public class AMCQuotation
        {
            public string AMCQTId, AMCQTNo, AMCQTCurrFinYear, AMCQTDate, AMCQTPeriod, CRId, AMCQTPMCalls, AMCQTBreakDownCalls, AMCQTPaymentTerms, CustId, CustUnitId, CustDetId, AMCQTCustPONo, AMCQTCustPODate, AMCQTServiceTax, AMCQTPreparedBy, AMCQTApprovedBy, AMCQTStatus, AMCQTValidity;
            public string AMCQTDetId, ItemCode, AMCQTDetQty, AMCQTDetUnitPrice, AMCQTDetSerialNo, AssignTaskId;
            public string AMCQTFUDetId, EmpId, AMCQTFUDetDesc, AMCQTFUDetDate;

            public AMCQuotation()
            {
            }

            public static string AMCQuotation_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "QUOT/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(AMCQT_NO,LEFT(AMCQT_NO,5),''))),0)+1 FROM [YANTRA_AMC_QUOTATION_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(QUOT_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_QUOTATION_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_AMC_QUOTATION_MAST", "AMCQT_NO");
            }

            public string AMCQuotation_Save()
            {
                this.AMCQTNo = AMCQuotation_AutoGenCode();
                this.AMCQTId = AutoGenMaxId("[YANTRA_AMC_QUOTATION_MAST]", "AMCQT_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_QUOTATION_MAST] SELECT ISNULL(MAX(AMCQT_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}' FROM [YANTRA_AMC_QUOTATION_MAST]",
                    this.AMCQTNo, this.AMCQTDate, this.AMCQTPeriod, this.CRId, this.AMCQTPMCalls, this.AMCQTBreakDownCalls, this.AMCQTPaymentTerms, this.CustId, this.CustUnitId, this.CustDetId, this.AMCQTCustPONo, this.AMCQTCustPODate, this.AMCQTServiceTax, this.AMCQTPreparedBy, this.AMCQTApprovedBy, this.AMCQTStatus, this.AMCQTValidity);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Quotation Details", "100");

                }
                return _returnStringMessage;
            }

            public string AMCQuotation_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_QUOTATION_MAST] SET AMCQT_DATE='{0}',AMCQT_PERIOD='{1}',AMCQT_PM_CALLS_VISITS='{2}',AMCQT_BREAKDOWN_CALLS_VISITS='{3}',AMCQT_PAYMENT_TERMS='{4}',CUST_ID={5},CUST_UNIT_ID={6},CUST_DET_ID={7},AMCQT_CUST_PREV_PO_NO='{8}',AMCQT_CUST_PREV_PO_DATE='{9}',AMCQT_SERVICE_TAX='{10}',AMCQT_PREPARED_BY='{11}',AMCQT_APPROVED_BY='{12}',AMCQT_VALIDITY='{13}' WHERE AMCQT_ID='{14}'", this.AMCQTDate, this.AMCQTPeriod, this.AMCQTPMCalls, this.AMCQTBreakDownCalls, this.AMCQTPaymentTerms, this.CustId, this.CustUnitId, this.CustDetId, this.AMCQTCustPONo, this.AMCQTCustPODate, this.AMCQTServiceTax, this.AMCQTPreparedBy, this.AMCQTApprovedBy, this.AMCQTValidity, this.AMCQTId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Quotation Details", "100");

                }
                return _returnStringMessage;
            }

            public string AMCQuotation_Delete(string AMCQuotationId)
            {
                if (DeleteRecord("[YANTRA_AMC_QUOTATION_DET]", "AMCQT_ID", AMCQuotationId) == true)
                {
                    if (DeleteRecord("[YANTRA_AMC_QUOTATION_MAST]", "AMCQT_ID", AMCQuotationId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("AMC Quotation Details", "100");

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

            public static void AMCQuotation_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_QUOTATION_MAST] ORDER BY AMCQT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AMCQT_NO", "AMCQT_ID");
                }
            }
          
            public int AMCQuotation_Select(string AMCQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("SELECT * FROM YANTRA_SERVICES_ASSIGN_TASKS,YANTRA_COMPLAINT_REGISTER,[YANTRA_AMC_QUOTATION_MAST],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS] ,[YANTRA_CUSTOMER_DET] WHERE YANTRA_COMPLAINT_REGISTER.CR_ID=YANTRA_SERVICES_ASSIGN_TASKS.CR_ID AND " +
" YANTRA_COMPLAINT_REGISTER.CR_ID=[YANTRA_AMC_QUOTATION_MAST].CR_ID AND [YANTRA_AMC_QUOTATION_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND " +
" [YANTRA_AMC_QUOTATION_MAST].CUST_DET_ID=[YANTRA_CUSTOMER_DET].CUST_DET_ID  AND [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID='" + AMCQuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
            public int AMCQuotation_isrecordexists(string AMCQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from yantra_amc_order_mast where AMCQT_ID="+AMCQuotationId+" ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                   // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                   // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
            public string AMCQuotationDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_QUOTATION_DET] SELECT ISNULL(MAX(AMCQT_DET_ID),0)+1,{0},{1},{2},'{3}','{4}' FROM [YANTRA_AMC_QUOTATION_DET]", this.AMCQTId, this.ItemCode, this.AMCQTDetQty, this.AMCQTDetUnitPrice, this.AMCQTDetSerialNo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Quotation Details", "100");

                }
                return _returnStringMessage;
            }

            public int AMCQuotationDetails_Delete(string AMCQuotationId)
            {
                if (DeleteRecord("[YANTRA_AMC_QUOTATION_DET]", "AMCQT_ID", AMCQuotationId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;

            }

            public void AMCQuotationDetails_Select(string AMCQuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_QUOTATION_DET],[YANTRA_ITEM_MAST],YANTRA_LKUP_ITEM_TYPE WHERE [YANTRA_AMC_QUOTATION_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND YANTRA_LKUP_ITEM_TYPE.IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_AMC_QUOTATION_DET].AMCQT_ID=" + AMCQuotationId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable AMCQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                AMCQuotationItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                AMCQuotationItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = AMCQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["Quantity"] = dbManager.DataReader["AMCQT_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["AMCQT_DET_UNIT_PRICE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SerialNo"] = dbManager.DataReader["AMCQT_DET_SERIAL_NO"].ToString();
                    AMCQuotationItems.Rows.Add(dr);
                }

                gv.DataSource = AMCQuotationItems;
                gv.DataBind();
            }

            public string AMCQuotationFollowUp_Save()
            {
                this.AMCQTFUDetId = AutoGenMaxId("[YANTRA_AMC_QUOT_FOLLOWUP_DET]", "AMCQT_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_AMC_QUOT_FOLLOWUP_DET] VALUES({0},{1},{2},'{3}','{4}')", this.AMCQTFUDetId,this.AMCQTId, this.EmpId, this.AMCQTFUDetDesc, this.AMCQTFUDetDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Quotation Follow Up Details", "100");

                }
                return _returnStringMessage;
            }


            public string AMCQuotationApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  AMCQT_STATUS FROM [YANTRA_AMC_QUOTATION_MAST] WHERE AMCQT_ID='{0}'", this.AMCQTId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_AMC_QUOTATION_MAST] SET AMCQT_APPROVED_BY={0},AMCQT_STATUS='{1}' WHERE AMCQT_ID='{2}'", this.AMCQTApprovedBy, ServicesStatus.Open, AMCQTId);
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
                    log.add_Update("AMC Quotation Approve Details", "100");

                }
                return _returnStringMessage;
            }

            public static string AMCQuotationStatus_Update(ServicesStatus Status, string AMCQTId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  AMCQT_STATUS FROM [YANTRA_AMC_QUOTATION_MAST] WHERE AMCQT_ID='{0}'", AMCQTId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_AMC_QUOTATION_MAST] SET AMCQT_STATUS='{0}' WHERE AMCQT_ID='{1}'", Status, AMCQTId);
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
                    log.add_Update("AMC Quotation Status Details", "100");

                }
                return _returnStringMessage;
            }

            public int Get_Ids_Select(string AMCQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_AMC_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND [YANTRA_AMC_ENQ_ASSIGN_TASKS].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND " +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + AMCQuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");

                _commandText = string.Format("SELECT * FROM YANTRA_AMC_ENQ_ASSIGN_TASKS,YANTRA_COMPLAINT_REGISTER,[YANTRA_AMC_QUOTATION_MAST],[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS] ,[YANTRA_CUSTOMER_DET] WHERE [YANTRA_AMC_QUOTATION_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND YANTRA_COMPLAINT_REGISTER.CR_ID=YANTRA_AMC_ENQ_ASSIGN_TASKS.CR_ID AND" +
                                         " [YANTRA_AMC_QUOTATION_MAST].CUST_DET_ID=[YANTRA_CUSTOMER_DET].CUST_DET_ID  AND [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID='" + AMCQuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID DESC ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["AMC_ASSIGN_TASK_ID"].ToString();
                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public int Get_Ids_Select1(string QuotationId)
            {
                // if (dbManager.Transaction == null)
                // dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_QUOTATION_MAST],[YANTRA_SERVICES_ASSIGN_TASKS],[YANTRA_COMPLAINT_REGISTER],[YANTRA_CUSTOMER_MAST]  WHERE [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND  [YANTRA_SERVICES_ASSIGN_TASKS].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND " +
                                            " [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["SERVICE_ASSIGN_TASK_ID"].ToString();
                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
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

            public static void AMCQuotationItemTypes_Select(string QuotationId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_AMC_QUOTATION_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_AMC_QUOTATION_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_AMC_QUOTATION_DET].AMCQT_ID=" + QuotationId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void AMCQuotationItemNames_Select(string QuotationId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_QUOTATION_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_AMC_QUOTATION_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_AMC_QUOTATION_DET].AMCQT_ID=" + QuotationId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }
            public string AMCQuotationRegret_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  AMCQT_STATUS FROM [YANTRA_AMC_QUOTATION_MAST] WHERE AMCQT_ID='{0}'", this.AMCQTId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_AMC_QUOTATION_MAST] SET AMCQT_APPROVED_BY={0},AMCQT_STATUS='{1}' WHERE AMCQT_ID='{2}'", this.AMCQTApprovedBy, ServicesStatus.Open, AMCQTId);
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
                    log.add_Update("AMC Quotation Regret Details", "100");

                }
                return _returnStringMessage;
            }


            public int complaintrecord_isrecordexists(string p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from yantra_amc_quotation_mast where cr_id="+p+" ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;  
            }


            public String WOId, WOCallName, WOCallDate;

            public string AMCWorkOrderPMCallDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "INSERT INTO [YANTRA_AMC_PMCALLS] SELECT ISNULL(MAX(AMC_OP_PM_ID),0)+1," + this.AMCQTId + ",'" + this.WOCallName + "','" + this.WOCallDate + "' FROM [YANTRA_AMC_PMCALLS]"; ;
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Work Order Details", "101");

                }
                return _returnStringMessage;
            }

            public int AMCWorkOrderPMCallDetails_Delete(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_PMCALLS] WHERE AMC_OP_ID={0}", WorkOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void AMCWorkOrderPMCallDetails_Select(string WorkOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT * FROM [YANTRA_AMC_PMCALLS] WHERE AMC_OP_ID=" + WorkOrderId + "";
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable OrderAcceptanceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("callname");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("calldate");
                OrderAcceptanceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = OrderAcceptanceProducts.NewRow();
                    dr["callname"] = dbManager.DataReader["AMC_OP_PM_NAME"].ToString();
                    dr["calldate"] = Convert.ToDateTime(dbManager.DataReader["AMC_OP_PM_DATE"].ToString()).ToString("dd/MM/yyyy");

                    OrderAcceptanceProducts.Rows.Add(dr);
                }

                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }





        }

        //Methods For AMC Order Form
        public class AMCOrder
        {
            public string AMCOId, AMCONo, AMCODate, AMCQTId, AMCQTNo, CustId, CustUnitId, CustDetId, AMCORespId, AMCOSalespId, AMCOPreparedBy, AMCOCheckedBy, AMCOApprovedBy, AMCOAcceptanceFlag, AMCODelivery, AMCOCurrencyTypeId, AMCOPackageCharges, AMCOPaymentTerms, AMCOCSTax, AMCOExciseDuty, AMCOGuarantee, DespmId, AMCOInsurance, AMCOTransportCharges, AMCOJurisdiction, AMCOErection, AMCOInspection, AMCOValidity, AMCOOtherSpec, CrId, AssignTaskId, AMCOTillDate, EnqId, AMCOCustPONo, AMCOCustPODate, AMCOConsignee, AMCOResponsiblePerson, AMCOResponsiblePersonEmail, AMCOPMCalls, AMCOBDCalls;
            public string AMCODetId, AMCOItemCode, AMCODetQty, AMCORate, AMCODetSpec, AMCODetRemarks, AMCODetPriority;
            public string AMCOUploadId, AMCOUploadFileName, AMCOUploadDate, AMCOFileContentType;
            public AMCOrder()
            {
            }

            public static string AMCOrder_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(AMCO_NO,LEFT(AMCO_NO,5),''))),0)+1 FROM [YANTRA_AMC_ORDER_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_SO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_AMC_ORDER_MAST", "AMCO_NO");
            }
            public string AMCOrderUploads_Save()
            {
                //  this.SONo = SalesOrder_AutoGenCode();
                this.AMCOUploadId = AutoGenMaxId("[YANTRA_AMCO_UPLOADS]", "AMCO_UPLOAD_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMCO_UPLOADS] values (" + AMCOUploadId + ",{0},'{1}','{2}','{3}',convert(varbinary(max),'{4}'))", this.AMCOId, this.AMCOUploadFileName, this.AMCOUploadDate, this.AMCOFileContentType, null);
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
                    log.add_Insert("AMC Order Upload Details", "102");

                }
                return _returnStringMessage;
            }

            public string AMCOrder_Save()
            {
                this.AMCONo = AMCOrder_AutoGenCode();
                this.AMCOId = AutoGenMaxId("[YANTRA_AMC_ORDER_MAST]", "AMCO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_ORDER_MAST] SELECT ISNULL(MAX(AMCO_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}' FROM [YANTRA_AMC_ORDER_MAST]", this.AMCONo, this.AMCODate, this.AMCQTId, this.AMCORespId, this.AMCOSalespId, this.AMCOPreparedBy, this.AMCOCheckedBy, this.AMCOApprovedBy, this.AMCOAcceptanceFlag, this.AMCODelivery, this.AMCOCurrencyTypeId, this.AMCOPaymentTerms, this.AMCOPackageCharges, this.AMCOExciseDuty, this.AMCOCSTax, this.DespmId, this.AMCOGuarantee, this.AMCOTransportCharges, this.AMCOInsurance, this.AMCOErection, this.AMCOJurisdiction, this.AMCOValidity, this.AMCOInspection, this.AMCOOtherSpec, this.AMCOTillDate, this.AMCOCustPONo, this.AMCOCustPODate, this.AMCOConsignee, this.AMCOResponsiblePerson, this.AMCOResponsiblePersonEmail, this.AMCOPMCalls, this.AMCOBDCalls);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Order Details", "102");

                }
                return _returnStringMessage;
            }

            public string AMCOrder_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_ORDER_MAST] SET AMCO_DATE='{0}',AMCO_RESP_ID='{1}',AMCO_SALESP_ID='{2}',AMCO_PREPARED_BY='{3}',AMCO_CHECKED_BY='{4}',AMCO_APPROVED_BY='{5}',AMCO_DELIVERY='{6}',CURRENCY_ID={7},AMCO_PAY_TERM='{8}',AMCO_PACK_CHARGES='{9}',AMCO_EXCISE='{10}',AMCO_CST='{11}',DESPM_ID={12},AMCO_GUARANTEE='{13}',AMCO_TRANS_CHARGES='{14}',AMCO_INSURANCE='{15}',AMCO_EREC_COMM='{16}',AMCO_JURISDICTION='{17}',AMCO_VALIDITY='{18}',AMCO_INSPECTION='{19}',AMCO_OTHER_SPEC='{20}',AMCO_TILLDATE='{21}',AMCO_CUST_PO='{22}',AMCO_CUST_PO_DATED='{23}',AMCO_CONSIGNEE='{24}',AMCO_RESPONSIBLE_PERSON='{25}',AMCO_RESPONSIBLE_PERSON_EMAIL='{26}',AMCO_PM_CALLS='{27}',AMCO_BD_CALLS='{28}' WHERE AMCO_ID='{29}'", this.AMCODate, this.AMCORespId, this.AMCOSalespId, this.AMCOPreparedBy, this.AMCOCheckedBy, this.AMCOApprovedBy, this.AMCODelivery, this.AMCOCurrencyTypeId, this.AMCOPaymentTerms, this.AMCOPackageCharges, this.AMCOExciseDuty, this.AMCOCSTax, this.DespmId, this.AMCOGuarantee, this.AMCOTransportCharges, this.AMCOInsurance, this.AMCOErection, this.AMCOJurisdiction, this.AMCOValidity, this.AMCOInspection, this.AMCOOtherSpec, this.AMCOTillDate, this.AMCOCustPONo, this.AMCOCustPODate, this.AMCOConsignee, this.AMCOResponsiblePerson, this.AMCOResponsiblePersonEmail, this.AMCOPMCalls, this.AMCOBDCalls, this.AMCOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Order Details", "102");

                }
                return _returnStringMessage;
            }

            public string AMCOrder_Delete(string SalesOrderId)
            {
                if (DeleteRecord("[YANTRA_AMC_ORDER_DET]", "AMCO_ID", SalesOrderId) == true)
                {
                    if (DeleteRecord("[YANTRA_AMC_ORDER_MAST]", "AMCO_ID", SalesOrderId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("AMC Order Details", "102");
                        
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

            public string AMCOrderDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_ORDER_DET] SELECT ISNULL(MAX(AMCO_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_AMC_ORDER_DET]", this.AMCOId, this.AMCOItemCode, this.AMCODetQty, this.AMCORate, this.AMCODetSpec, this.AMCODetRemarks, this.AMCODetPriority);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Order Details", "102");

                }
                return _returnStringMessage;
            }

            public int AMCOrderDetails_Delete(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_ORDER_DET] WHERE AMCO_ID={0}", SalesOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;

            }

            public int AMCOrder_Select(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_AMC_ORDER_MAST] WHERE   [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND" +
                                            " [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_ORDER_MAST].AMCO_ID='" + SalesOrderId + "' ORDER BY [YANTRA_AMC_ORDER_MAST].AMCO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCONo = dbManager.DataReader["AMCO_NO"].ToString();
                    this.AMCODate = Convert.ToDateTime(dbManager.DataReader["AMCO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    this.AMCORespId = dbManager.DataReader["AMCO_RESP_ID"].ToString();
                    this.AMCOSalespId = dbManager.DataReader["AMCO_SALESP_ID"].ToString();
                    this.AMCOPreparedBy = dbManager.DataReader["AMCO_PREPARED_BY"].ToString();
                    this.AMCOCheckedBy = dbManager.DataReader["AMCO_CHECKED_BY"].ToString();
                    this.AMCOApprovedBy = dbManager.DataReader["AMCO_APPROVED_BY"].ToString();
                    this.AMCOAcceptanceFlag = dbManager.DataReader["AMCO_ACCEPTANCE_FLAG"].ToString();
                    this.AMCODelivery = dbManager.DataReader["AMCO_DELIVERY"].ToString();
                    this.AMCOCurrencyTypeId = dbManager.DataReader["CURRENCY_ID"].ToString();
                    this.AMCOPaymentTerms = dbManager.DataReader["AMCO_PAY_TERM"].ToString();
                    this.AMCOPackageCharges = dbManager.DataReader["AMCO_PACK_CHARGES"].ToString();
                    this.AMCOExciseDuty = dbManager.DataReader["AMCO_EXCISE"].ToString();
                    this.AMCOCSTax = dbManager.DataReader["AMCO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.AMCOGuarantee = dbManager.DataReader["AMCO_GUARANTEE"].ToString();
                    this.AMCOTransportCharges = dbManager.DataReader["AMCO_TRANS_CHARGES"].ToString();
                    this.AMCOInsurance = dbManager.DataReader["AMCO_INSURANCE"].ToString();
                    this.AMCOErection = dbManager.DataReader["AMCO_EREC_COMM"].ToString();
                    this.AMCOJurisdiction = dbManager.DataReader["AMCO_JURISDICTION"].ToString();
                    this.AMCOValidity = dbManager.DataReader["AMCO_VALIDITY"].ToString();
                    this.AMCOInspection = dbManager.DataReader["AMCO_INSPECTION"].ToString();
                    this.AMCOOtherSpec = dbManager.DataReader["AMCO_OTHER_SPEC"].ToString();
                    this.AMCOTillDate = Convert.ToDateTime(dbManager.DataReader["AMCO_TILLDATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCOCustPONo = dbManager.DataReader["AMCO_CUST_PO"].ToString();
                    this.AMCOCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCO_CUST_PO_DATED"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCOConsignee = dbManager.DataReader["AMCO_CONSIGNEE"].ToString();
                    this.AMCOResponsiblePerson = dbManager.DataReader["AMCO_RESPONSIBLE_PERSON"].ToString();
                    this.AMCOResponsiblePersonEmail = dbManager.DataReader["AMCO_RESPONSIBLE_PERSON_EMAIL"].ToString();
                    this.AMCOPMCalls = dbManager.DataReader["AMCO_PM_CALLS"].ToString();
                    this.AMCOBDCalls = dbManager.DataReader["AMCO_BD_CALLS"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string AMCOrderStatus_Update(ServicesStatus Status, string SOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_ORDER_MAST] SET  AMCO_ACCEPTANCE_FLAG='{0}' WHERE AMCO_ID='{1}'", Status, SOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("AMC Order Status Details", "102");

                }
                return _returnStringMessage;
            }

            public void AMCOrderDetails_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_AMC_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_AMC_ORDER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_AMC_ORDER_DET].AMCO_ID=" + SalesOrderId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
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
                col = new DataColumn("ItemTypeId");
                SalesOrderProducts.Columns.Add(col);



                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["AMCO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["AMCO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["AMCO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["AMCO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["AMCO_DET_PRIORITY"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();



                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }

            public string AMCOrderApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  AMCO_ACCEPTANCE_FLAG FROM [YANTRA_AMC_ORDER_MAST] WHERE AMCO_ID='{0}'", this.AMCOId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_AMC_ORDER_MAST] SET AMCO_APPROVED_BY={0},AMCO_ACCEPTANCE_FLAG='{1}' WHERE AMCO_ID='{2}'", this.AMCOApprovedBy, ServicesStatus.Open, this.AMCOId);
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
                    log.add_Update("AMC Order Approve Details", "102");

                }
                return _returnStringMessage;
            }

            public int Get_Ids_Select(string SalesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],[YANTRA_AMC_ENQ_ASSIGN_TASKS],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_AMC_ORDER_MAST]  WHERE  [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_AMC_ENQ_ASSIGN_TASKS].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND " +
                                            " [YANTRA_AMC_ORDER_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND [YANTRA_AMC_ORDER_MAST].AMCO_ID='" + SalesOrderId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["AMC_ASSIGN_TASK_ID"].ToString();
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
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

            public static void AMCOrder_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ORDER_MAST] ORDER BY AMCO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AMCO_NO", "AMCO_ID");
                }
            }

            public static void AMCOrderbyCRId_Select(Control ControlForBind, string CRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_QUOTATION_MAST] WHERE [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND [YANTRA_AMC_QUOTATION_MAST].CR_ID=" + CRId + " ORDER BY AMCO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AMCO_NO", "AMCO_ID");
                }
            }


            public static void AMCOrderForPayments_Select(Control ControlForBind, string CustomerId, string UnitId, string SaveButtonText)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_COMPLAINT_REGISTER] WHERE [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=" + CustomerId + " AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=" + UnitId + " AND [YANTRA_AMC_ORDER_MAST].AMCO_ID IN (SELECT AMCO_ID FROM YANTRA_AMC_PAYMENT_RECEIVED WHERE AMCPR_PAYMENT_STATUS <> 'Cleared') AND [YANTRA_AMC_ORDER_MAST].AMCO_ID IN (SELECT [YANTRA_AMC_INVOICE_MAST].AMCO_ID FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_AMC_INVOICE_MAST] WHERE [YANTRA_AMC_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID) ORDER BY [YANTRA_AMC_ORDER_MAST].AMCO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["AMCO_NO"].ToString(), dbManager.DataReader["AMCO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_COMPLAINT_REGISTER] WHERE [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=" + CustomerId + " AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=" + UnitId + " AND [YANTRA_AMC_ORDER_MAST].AMCO_ID NOT IN (SELECT AMCO_ID FROM YANTRA_AMC_PAYMENT_RECEIVED) AND [YANTRA_AMC_ORDER_MAST].AMCO_ID IN (SELECT [YANTRA_AMC_INVOICE_MAST].AMCO_ID FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_AMC_INVOICE_MAST] WHERE [YANTRA_AMC_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID) ORDER BY [YANTRA_AMC_ORDER_MAST].AMCO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["AMCO_NO"].ToString(), dbManager.DataReader["AMCO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_COMPLAINT_REGISTER] WHERE [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=" + CustomerId + " AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=" + UnitId + "  ORDER BY [YANTRA_AMC_ORDER_MAST].AMCO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "AMCO_NO", "AMCO_ID");
                    }
                }
            }

            public static void AMCOrderItemTypes_Select(string SalesOrderId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_AMC_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_AMC_ORDER_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_AMC_ORDER_DET].AMCO_ID=" + SalesOrderId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void AMCOrderItemNames_Select(string SalesOrderId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_AMC_ORDER_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_AMC_ORDER_DET].AMCO_ID=" + SalesOrderId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }

        }

        //Methods For AMC Order Acceptance Form
        public class AMCOrderAcceptance
        {
            public string OAId, OANo, OADate, SOId, SONo, SOCSTax, DespmId, QuotNo, QuotId, CustId, CustUnitId, CustDetId, OARespId, OASalespId, OAPreparedBy, OACheckedBy, OAApprovedBy, OAFlag, OAAcceptanceFlag, OAConsignee, OAPMVisits, OAPMSchedule, OAPayment, OAPaymentStatus, OAInvoiceStatus, OAInvoiceDetails, OAAMCAmt, OAServiceTax, OARespPerson, OARespPersonEmail;
            public string OADetId, OAItemCode, OADetQty, OARate, OADetSpec, OADetRemarks, OADetPriority, OACallName, OACallDate;

            public AMCOrderAcceptance()
            {
            }
            public int AMCOrderAcceptance_isrecordexists(string AMCQuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_AMC_OA_MAST where wo_id=" + AMCQuotationId + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public static string AMCOrderAcceptance_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(OA_NO,LEFT(OA_NO,5),''))),0)+1 FROM [YANTRA_AMC_OA_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_SO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_AMC_OA_MAST", "OA_NO");
            }

            public string AMCOrderAcceptance_Save()
            {
                this.OANo = AMCOrderAcceptance_AutoGenCode();
                this.OAId = AutoGenMaxId("[YANTRA_AMC_OA_MAST]", "OA_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_OA_MAST] SELECT ISNULL(MAX(OA_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}' FROM [YANTRA_AMC_OA_MAST]", this.OANo, this.OADate, this.SOId, this.OARespId, this.OASalespId, this.OAPreparedBy, this.OACheckedBy, this.OAApprovedBy, this.OAFlag, this.OAConsignee, this.OAPMVisits, this.OAPMSchedule, this.OAPayment, this.OAPaymentStatus, this.OAInvoiceStatus, this.OAInvoiceDetails, this.OAServiceTax, this.OAAMCAmt, this.OARespPerson, this.OARespPersonEmail);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Order Acceptance Details", "102");

                }
                return _returnStringMessage;
            }

            public string AMCOrderAcceptance_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_OA_MAST] SET OA_DATE='{0}',WO_ID='{1}',OA_RESP_ID='{2}',OA_SALESP_ID='{3}',OA_PREPARED_BY='{4}',OA_CHECKED_BY='{5}',OA_APPROVED_BY='{6}',OA_CONSIGNEE='{7}',OA_PM_VISITS='{8}',OA_PM_SCHEDULE='{9}',OA_PAYMENT='{10}',OA_PAYMENT_STATUS='{11}',OA_INVOICE_STATUS='{12}',OA_INVOICE_DETAILS='{13}',OA_SERVICE_TAX='{14}',OA_AMC_AMT='{15}',OA_RESP_PERSON='{16}',OA_RESP_PERSON_EMAIL='{17}' WHERE OA_ID='{18}'", this.OADate, this.SOId, this.OARespId, this.OASalespId, this.OAPreparedBy, this.OACheckedBy, this.OAApprovedBy, this.OAConsignee, this.OAPMVisits, this.OAPMSchedule, this.OAPayment, this.OAPaymentStatus, this.OAInvoiceStatus, this.OAInvoiceDetails, this.OAServiceTax, this.OAAMCAmt, this.OARespPerson, this.OARespPersonEmail, this.OAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Order Acceptance Details", "102");

                }
                return _returnStringMessage;
            }

            public string AMCOrderAcceptance_Delete(string OrderAcceptanceId)
            {
                if (DeleteRecord("[YANTRA_AMC_OA_PMCALLS]", "AMC_OA_PM_ID", OrderAcceptanceId) == true)
                {
                    if (DeleteRecord("[YANTRA_AMC_OA_DET]", "OA_ID", OrderAcceptanceId) == true)
                    {
                        if (DeleteRecord("[YANTRA_AMC_OA_MAST]", "OA_ID", OrderAcceptanceId) == true)
                        {
                            _returnStringMessage = "Data Deleted Successfully";
                            log.add_Delete("AMC Order Acceptance Details", "102");

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

            public string AMCOrderAcceptanceDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_OA_DET] SELECT ISNULL(MAX(OA_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_AMC_OA_DET]", this.OAId, this.OAItemCode, this.OADetQty, this.OARate, this.OADetSpec, this.OADetPriority, this.OADetRemarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Order Acceptance Details", "102");

                }
                return _returnStringMessage;
            }

            public int AMCOrderAcceptanceDetails_Delete(string OrderAcceptanceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_OA_DET] WHERE OA_ID={0}", OrderAcceptanceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }
            public string AMCOrderAcceptancePMCallDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "INSERT INTO [YANTRA_AMC_OA_PMCALLS] SELECT ISNULL(MAX(AMC_OA_PM_ID),0)+1," + this.OAId + ",'" + this.OACallName + "','" + this.OACallDate + "' FROM [YANTRA_AMC_OA_PMCALLS]"; ;
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Order Acceptance PM Call Details", "102");

                }
                return _returnStringMessage;
            }

            public int AMCOrderAcceptancePMCallDetails_Delete(string OAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_OA_PMCALLS] WHERE AMC_OA_ID={0}", OAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void AMCOrderAcceptancePMCallDetails_Select(string OAId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT * FROM [YANTRA_AMC_OA_PMCALLS] WHERE AMC_OA_ID=" + OAId + "";
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable OrderAcceptanceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("callname");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("calldate");
                OrderAcceptanceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = OrderAcceptanceProducts.NewRow();
                    dr["callname"] = dbManager.DataReader["AMC_OA_PM_NAME"].ToString();
                    dr["calldate"] = Convert.ToDateTime(dbManager.DataReader["AMC_OA_PM_DATE"].ToString()).ToString("dd/MM/yyyy");

                    OrderAcceptanceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }

            public int AMCOrderAcceptance_Select(string OrderAcceptanceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_COMPLAINT_REGISTER],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_WO_MAST],[YANTRA_AMC_OA_MAST] WHERE [YANTRA_COMPLAINT_REGISTER].CR_ID = [YANTRA_AMC_QUOTATION_MAST].CR_ID " +
                                            " AND [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID=[YANTRA_AMC_ORDER_MAST].AMCQT_ID AND [YANTRA_AMC_ORDER_MAST].AMCO_ID=[YANTRA_AMC_WO_MAST].AMCO_ID  AND [YANTRA_AMC_WO_MAST].WO_ID=[YANTRA_AMC_OA_MAST].WO_ID  AND  " +
                                            " [YANTRA_AMC_OA_MAST].OA_ID='" + OrderAcceptanceId + "' ORDER BY [YANTRA_AMC_OA_MAST].OA_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.OANo = dbManager.DataReader["OA_NO"].ToString();
                    this.OADate = Convert.ToDateTime(dbManager.DataReader["OA_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.SONo = dbManager.DataReader["AMCO_NO"].ToString();
                    this.SOCSTax = dbManager.DataReader["AMCO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.QuotId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.QuotNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    this.OARespId = dbManager.DataReader["OA_RESP_ID"].ToString();
                    this.OASalespId = dbManager.DataReader["OA_SALESP_ID"].ToString();
                    this.OAPreparedBy = dbManager.DataReader["OA_PREPARED_BY"].ToString();
                    this.OACheckedBy = dbManager.DataReader["OA_CHECKED_BY"].ToString();
                    this.OAApprovedBy = dbManager.DataReader["OA_APPROVED_BY"].ToString();
                    this.OAAcceptanceFlag = dbManager.DataReader["OA_FLAG"].ToString();
                    this.OAConsignee = dbManager.DataReader["OA_CONSIGNEE"].ToString();
                    this.OAPMVisits = dbManager.DataReader["OA_PM_VISITS"].ToString();
                    this.OAPMSchedule = dbManager.DataReader["OA_PM_SCHEDULE"].ToString();
                    this.OAPayment = dbManager.DataReader["OA_PAYMENT"].ToString();
                    this.OAPaymentStatus = dbManager.DataReader["OA_PAYMENT_STATUS"].ToString();
                    this.OAInvoiceStatus = dbManager.DataReader["OA_INVOICE_STATUS"].ToString();
                    this.OAInvoiceDetails = dbManager.DataReader["OA_INVOICE_DETAILS"].ToString();
                    this.OAServiceTax = dbManager.DataReader["OA_SERVICE_TAX"].ToString();
                    this.OAAMCAmt = dbManager.DataReader["OA_AMC_AMT"].ToString();
                    this.OARespPerson = dbManager.DataReader["OA_RESP_PERSON"].ToString();
                    this.OARespPersonEmail = dbManager.DataReader["OA_RESP_PERSON_EMAIL"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string AMCOrderAcceptanceStatus_Update(ServicesStatus Status, string OAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_OA_MAST] SET  OA_FLAG='{0}' WHERE OA_ID='{1}'", Status, OAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("AMC Order Acceptance Status Details", "102");

                }
                return _returnStringMessage;
            }

            public void AMCOrderAcceptanceDetails_Select(string OrderAcceptanceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_AMC_OA_DET] WHERE [YANTRA_AMC_OA_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_AMC_OA_DET].OA_ID=" + OrderAcceptanceId + "");
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

            public string AMCOrderAcceptanceApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  OA_FLAG FROM [YANTRA_AMC_OA_MAST] WHERE OA_ID='{0}'", this.OAId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_AMC_OA_MAST] SET OA_APPROVED_BY={0},OA_FLAG='{1}' WHERE OA_ID='{2}'", this.OAApprovedBy, ServicesStatus.Open, this.OAId);
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
                    log.add_Update("AMC Order Acceptance Approve Details", "102");

                }
                return _returnStringMessage;
            }

            public static void AMCOrderAcceptance_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_OA_MAST] ORDER BY OA_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "OA_NO", "OA_ID");
                }
            }
        }

        //Methods For AMC Work Order Form
        public class AMCWorkOrder
        {
            public string WOId, WONo, WODate, AMCOId, AMCONo, DespId, CustId, WOInspDate, WOPackForwInst, WODeliveryDate, WOAccessories, WOExtraSpares, WOPreparedBy, WOCheckedBy, WOApprovedBy, WOFLag, WOPMCalls, WOBDCalls, WOServiceTax;
            public string WODetId, WOItemCode, WODetQty, WODetRate, WODetSpec, WODetRemarks, WOCallName, WOCallDate;

            public AMCWorkOrder()
            {
            }

            public static string AMCWorkOrder_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "WO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(WO_NO,LEFT(WO_NO,5),''))),0)+1 FROM [YANTRA_AMC_WO_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(WO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_WO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_AMC_WO_MAST", "WO_NO");
            }

            public string AMCWorkOrder_Save()
            {
                this.WONo = AMCWorkOrder_AutoGenCode();
                this.WOId = AutoGenMaxId("[YANTRA_AMC_WO_MAST]", "WO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_WO_MAST] SELECT ISNULL(MAX(WO_ID),0)+1,'{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}' FROM [YANTRA_AMC_WO_MAST]", this.WONo, this.WODate, this.AMCOId, this.DespId, this.WOInspDate, this.WOPackForwInst, this.WODeliveryDate, this.WOAccessories, this.WOExtraSpares, this.WOPreparedBy, this.WOCheckedBy, this.WOApprovedBy, "New", this.WOPMCalls, this.WOBDCalls, this.WOServiceTax);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Work Order Details", "103");

                }
                return _returnStringMessage;
            }
            
            public int AMCOrder_isrecordexists(string accepno)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_AMC_WO_MAST where WO_ID= " + accepno + "  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
            public string AMCWorkOrder_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_WO_MAST] SET WO_DATE='{0}',DESPM_ID='{1}',WO_INSP_DATE='{2}',WO_PACK_FORW_INS='{3}',WO_DELIVERY_DATE='{4}',WO_ACCESSORIES='{5}',WO_EXTRA_SPARES='{6}',WO_PREPARED_BY='{7}',WO_APPROVED_BY='{8}',WO_PM_CALLS='{9}',WO_BD_CALLS='{10}',WO_SERVICE_TAX='{11}' WHERE WO_ID='{12}'", this.WODate, this.DespId, this.WOInspDate, this.WOPackForwInst, this.WODeliveryDate, this.WOAccessories, this.WOExtraSpares, this.WOPreparedBy, this.WOApprovedBy, this.WOPMCalls, this.WOBDCalls, this.WOServiceTax, this.WOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Work Order Details", "103");

                }
                return _returnStringMessage;
            }

            public string AMCWorkOrder_Delete(string WorkOrderId)
            {
                if (DeleteRecord("[YANTRA_AMC_OP_PMCALLS]", "AMC_OP_PM_ID", WorkOrderId) == true)
                {
                    if (DeleteRecord("[YANTRA_AMC_WO_DET]", "WO_ID", WorkOrderId) == true)
                    {
                        if (DeleteRecord("[YANTRA_AMC_WO_MAST]", "WO_ID", WorkOrderId) == true)
                        {
                            _returnStringMessage = "Data Deleted Successfully";
                            log.add_Delete("AMC Work Order Details", "103");

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

            public string AMCWorkOrderDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_WO_DET] SELECT ISNULL(MAX(WO_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}' FROM [YANTRA_AMC_WO_DET]", this.WOId, this.WOItemCode, this.WODetQty, this.WODetSpec, this.WODetRemarks, this.WODetRate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Work Order Details", "103");

                }
                return _returnStringMessage;
            }

            public int AMCWorkOrderDetails_Delete(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_WO_DET] WHERE WO_ID={0}", WorkOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public string AMCWorkOrderPMCallDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "INSERT INTO [YANTRA_AMC_OP_PMCALLS] SELECT ISNULL(MAX(AMC_OP_PM_ID),0)+1," + this.WOId + ",'" + this.WOCallName + "','" + this.WOCallDate + "' FROM [YANTRA_AMC_OP_PMCALLS]"; ;
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Work Order PM Call Details", "103");

                }
                return _returnStringMessage;
            }

            public int AMCWorkOrderPMCallDetails_Delete(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_OP_PMCALLS] WHERE AMC_OP_ID={0}", WorkOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void AMCWorkOrderPMCallDetails_Select(string WorkOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT * FROM [YANTRA_AMC_OP_PMCALLS] WHERE AMC_OP_ID=" + WorkOrderId + "";
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable OrderAcceptanceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("callname");
                OrderAcceptanceProducts.Columns.Add(col);
                col = new DataColumn("calldate");
                OrderAcceptanceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = OrderAcceptanceProducts.NewRow();
                    dr["callname"] = dbManager.DataReader["AMC_OP_PM_NAME"].ToString();
                    dr["calldate"] = Convert.ToDateTime(dbManager.DataReader["AMC_OP_PM_DATE"].ToString()).ToString("dd/MM/yyyy");

                    OrderAcceptanceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }


            public int AMCWorkOrder_Select(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_WO_MAST] WHERE [YANTRA_COMPLAINT_REGISTER].CR_ID = [YANTRA_AMC_QUOTATION_MAST].CR_ID " +
                                            " AND [YANTRA_AMC_QUOTATION_MAST].AMCQT_ID=[YANTRA_AMC_ORDER_MAST].AMCQT_ID AND [YANTRA_AMC_ORDER_MAST].AMCO_ID=[YANTRA_AMC_WO_MAST].AMCO_ID" +
                                            " AND [YANTRA_AMC_WO_MAST].WO_ID='" + WorkOrderId + "' ORDER BY [YANTRA_AMC_WO_MAST].WO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.WONo = dbManager.DataReader["WO_NO"].ToString();
                    this.WODate = Convert.ToDateTime(dbManager.DataReader["WO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCONo = dbManager.DataReader["AMCO_NO"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DespId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.WOInspDate = dbManager.DataReader["WO_INSP_DATE"].ToString();
                    this.WOPackForwInst = dbManager.DataReader["WO_PACK_FORW_INS"].ToString();
                    this.WODeliveryDate = dbManager.DataReader["WO_DELIVERY_DATE"].ToString();
                    this.WOAccessories = dbManager.DataReader["WO_ACCESSORIES"].ToString();
                    this.WOExtraSpares = dbManager.DataReader["WO_EXTRA_SPARES"].ToString();
                    this.WOPreparedBy = dbManager.DataReader["WO_PREPARED_BY"].ToString();
                    this.WOCheckedBy = dbManager.DataReader["WO_CHECKED_BY"].ToString();
                    this.WOApprovedBy = dbManager.DataReader["WO_APPROVED_BY"].ToString();
                    this.WOPMCalls = dbManager.DataReader["WO_PM_CALLS"].ToString();
                    this.WOBDCalls = dbManager.DataReader["WO_BD_CALLS"].ToString();
                    this.WOServiceTax = dbManager.DataReader["WO_SERVICE_TAX"].ToString();
                    if (this.WOInspDate == "1/1/1900 12:00:00 AM") { this.WOInspDate = ""; } else { this.WOInspDate = Convert.ToDateTime(this.WOInspDate).ToString("dd/MM/yyyy"); }
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public void AMCWorkOrderDetails_Select(string WorkOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_AMC_WO_DET] WHERE [YANTRA_AMC_WO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_AMC_WO_DET].WO_ID=" + WorkOrderId + "");
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
                col = new DataColumn("Rate");
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
                    dr["Rate"] = dbManager.DataReader["WO_DET_RATE"].ToString();

                    OrderAcceptanceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }

            public string AMCWorkOrderApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  WO_FLAG FROM [YANTRA_AMC_WO_MAST] WHERE WO_ID='{0}'", this.WOId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_AMC_WO_MAST] SET  WO_APPROVED_BY={0},WO_FLAG='{1}' WHERE WO_ID='{2}'", this.WOApprovedBy, ServicesStatus.Open, this.WOId);
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
                    log.add_Update("AMC Work Order Acceptance Details", "103");

                }
                return _returnStringMessage;
            }

            public static string AMCWorkOrderStatus_Update(ServicesStatus Status, string WOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_WO_MAST] SET  WO_FLAG='{0}' WHERE WO_ID='{1}'", Status, WOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Work Order Status Details", "103");

                }
                return _returnStringMessage;
            }

            public static void AMCWorkOrder_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_WO_MAST] ORDER BY WO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "WO_NO", "WO_ID");
                }
            }
        }

        //Methods For Installation Report Form
        public class InstallationReport
        {
            public string IRId, IRNo, IRDate, WOId, IRInsatalledPerson, IRPreparedBy, IRApprovedBy;

            public string IRDetId, ItemTypeId, IRDetItemSerialNo, IRDetDateOfInstallation, IRDetWarrantyDet, IRDetPeriodFrom, IRDetPeriodTo, Itemcode;

            public InstallationReport()
            {
            }

            public static string InstallationReport_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";

                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(IR_NO,LEFT(IR_NO,5),''))),0)+1 FROM [YANTRA_INSTALLATION_REPORT_MAST]").ToString());

                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_INSTALLATION_REPORT_MAST", "IR_NO");
            }

            public string InstallationReport_Save()
            {
                this.IRNo = InstallationReport_AutoGenCode();
                this.IRId = AutoGenMaxId("[YANTRA_INSTALLATION_REPORT_MAST]", "IR_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INSTALLATION_REPORT_MAST] SELECT ISNULL(MAX(IR_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}' FROM [YANTRA_INSTALLATION_REPORT_MAST]", this.IRNo, this.IRDate, this.WOId, this.IRInsatalledPerson, this.IRPreparedBy, this.IRApprovedBy, "New");
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Installation Report Details", "104");

                }
                return _returnStringMessage;
            }

            public string InstallationReport_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_INSTALLATION_REPORT_MAST] SET IR_DATE='{0}',WO_ID='{1}',IR_INSTALLED_PERSON='{2}',IR_PREPARED_BY='{3}',IR_APPROVED_BY='{4}' WHERE IR_ID='{5}'", this.IRDate, this.WOId, this.IRInsatalledPerson, this.IRPreparedBy, this.IRApprovedBy, this.IRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Installation Report Details", "104");

                }
                return _returnStringMessage;
            }

            public string InstallationReport_Delete(string InstallationReportId)
            {

                if (DeleteRecord("[YANTRA_INSTALLATION_REPORT_MAST]", "IR_ID", InstallationReportId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Installation Report Details", "104");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public string InstallationReportDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_INSTALLATION_REPORT_DET] SELECT ISNULL(MAX(IR_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}' FROM [YANTRA_INSTALLATION_REPORT_DET]", this.IRId, this.ItemTypeId, this.IRDetItemSerialNo, this.Itemcode, this.IRDetDateOfInstallation, this.IRDetWarrantyDet, this.IRDetPeriodFrom, this.IRDetPeriodTo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Installation Report Details", "104");

                }
                return _returnStringMessage;
            }

            public int InstallationReportDetails_Delete(string InstallationReportId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_INSTALLATION_REPORT_DET] WHERE IR_ID={0}", InstallationReportId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int InstallationReport_Select(string InstallationReportId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INSTALLATION_REPORT_MAST],[YANTRA_WO_MAST] WHERE [YANTRA_INSTALLATION_REPORT_MAST].WO_ID = [YANTRA_WO_MAST].WO_ID AND [YANTRA_INSTALLATION_REPORT_MAST].IR_ID='" + InstallationReportId + "' ORDER BY [YANTRA_INSTALLATION_REPORT_MAST].IR_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.IRNo = dbManager.DataReader["IR_NO"].ToString();
                    this.IRDate = dbManager.DataReader["IR_DATE"].ToString();
                    this.WOId = dbManager.DataReader["WO_ID"].ToString();
                    this.IRInsatalledPerson = dbManager.DataReader["IR_INSTALLED_PERSON"].ToString();
                    this.IRPreparedBy = dbManager.DataReader["IR_PREPARED_BY"].ToString();
                    this.IRApprovedBy = dbManager.DataReader["IR_APPROVED_BY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public void InstallationReportDetails_Select(string InstallationReportId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_INSTALLATION_REPORT_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_INSTALLATION_REPORT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_INSTALLATION_REPORT_DET].IR_ID=" + InstallationReportId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable InstallationReportProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemType");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("SerialNo");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("DateOfInstallation");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("Warranty");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("WarrantyPeriodFrom");
                InstallationReportProducts.Columns.Add(col);
                col = new DataColumn("WarrantyPeriodTo");
                InstallationReportProducts.Columns.Add(col);



                while (dbManager.DataReader.Read())
                {
                    DataRow dr = InstallationReportProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    //dr["Quantity"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["SerialNo"] = dbManager.DataReader["IR_DET_ITEM_SERIAL_NO"].ToString();
                    dr["DateOfInstallation"] = dbManager.DataReader["IR_DET_DATE_OF_INSTALLATION"].ToString();
                    dr["Warranty"] = dbManager.DataReader["IR_DET_WARRANTY_DET"].ToString();
                    dr["WarrantyPeriodFrom"] = dbManager.DataReader["IR_DET_PERIOD_FROM"].ToString();
                    dr["WarrantyPeriodTo"] = dbManager.DataReader["IR_DET_PERIOD_TO"].ToString();


                    InstallationReportProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = InstallationReportProducts;
                gv.DataBind();
            }

            public static void InstallationReport_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_INSTALLATION_REPORT_MAST] ORDER BY IR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IR_NO", "IR_ID");
                }
            }
        }

        //Methods for Spares  Quotation Form     
        public class SparesQuotation
        {
            public string SparesQuotId, SparesQuotNo, SparesQuotDate, CrId, CrNo, CustId, SparesQuotDelivery, SparesQuotPayTerms, SparesQuotPackCharges, SparesQuotExcise, SparesQuotCST, SparesQuotVAT, DespmId, SparesQuotGuarantee, SparesQuotTransCharges, SparesQuotInsurance, SparesQuotErrec, SparesQuotJurisdiction, SparesQuotValidity, SparesQuotInspection, SparesQuotOtherSpecs, SparesQuotStatus, SparesQuotRespId, SparesQuotSalespId, SparesQuotPreparedBy, SparesQuotCheckedBy, SparesQuotApprovedBy, SparesCurrencyId, SparesRevisedKey, AssignTaskId;
            public string SparesQuotDetItemCode, SparesQuotDetQty, SparesQuotRate, SparesQuotDetSpec, SparesQuotDetRemarks, SparesQuotDetPriority;
            public string CrFollowUpDetId, FollowUpEmpId, FollowUpDesc, FollowUpDate, FollowUpTechDiss, FollowUpCommNegos, FollowUpCompExistance, FollowUpRemarks, FollowUpExpDate,Price;
            public bool IsExpectedOrder;
            public string cust_unit;
            

            public string CrDate, CustUnitId, CustDetId, ItemCode, Call, Complaint, CauseNo, Corrective, PreparedBy, Staus;
            public string cust_unit_add;

            public SparesQuotation()
            {
            }

            public static string SparesQuotation_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "QUOT/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SPARES_QUOT_NO,LEFT(SPARES_QUOT_NO,5),''))),0)+1 FROM [YANTRA_SPARES_QUOT_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(QUOT_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_QUOT_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SPARES_QUOT_MAST", "SPARES_QUOT_NO");
            }

            public string SparesQuotation_Save()
            {
                this.SparesRevisedKey = "";
                this.SparesQuotNo = SparesQuotation_AutoGenCode();
                this.SparesQuotId = AutoGenMaxId("[YANTRA_SPARES_QUOT_MAST]", "SPARES_QUOT_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_QUOT_MAST] SELECT ISNULL(MAX(SPARES_QUOT_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19},'{20}','{21}','{22}',{23},'{24}','{25}','{26}' FROM [YANTRA_SPARES_QUOT_MAST]",
                    this.SparesQuotNo, this.SparesQuotDate, this.CrId, this.SparesQuotDelivery, this.SparesQuotPayTerms, this.SparesQuotPackCharges, this.SparesQuotExcise, this.SparesQuotCST, this.DespmId, this.SparesQuotGuarantee, this.SparesQuotTransCharges, this.SparesQuotInsurance, this.SparesQuotErrec, this.SparesQuotJurisdiction, this.SparesQuotValidity, this.SparesQuotInspection, this.SparesQuotOtherSpecs, "New", this.SparesQuotRespId, this.SparesQuotSalespId, this.SparesQuotPreparedBy, this.SparesQuotCheckedBy, this.SparesQuotApprovedBy, this.SparesCurrencyId, this.SparesRevisedKey, this.SparesQuotVAT,this.Price);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Quotation Details", "105");

                }
                return _returnStringMessage;
            }

            public string SparesQuotationRevise_Save()
            {
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(QUOT_REVISED_KEY,'R','')),0)+1 FROM YANTRA_QUOT_MAST WHERE QUOT_ID=" + this.QuotId + "").ToString());
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SPARES_QUOT_REVISED_KEY,'R','')),0)+1 FROM YANTRA_SPARES_QUOT_MAST WHERE SPARES_QUOT_NO LIKE '" + this.SparesQuotNo + "%'").ToString());
                this.SparesRevisedKey = "R" + _returnIntValue.ToString();

                this.SparesQuotId = AutoGenMaxId("[YANTRA_SPARES_QUOT_MAST]", "SPARES_QUOT_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_QUOT_MAST] SELECT ISNULL(MAX(SPARES_QUOT_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19},'{20}','{21}','{22}',{23},'{24}','{25}','{26}' FROM [YANTRA_SPARES_QUOT_MAST]",
                     this.SparesQuotNo, this.SparesQuotDate, this.CrId, this.SparesQuotDelivery, this.SparesQuotPayTerms, this.SparesQuotPackCharges, this.SparesQuotExcise, this.SparesQuotCST, this.DespmId, this.SparesQuotGuarantee, this.SparesQuotTransCharges, this.SparesQuotInsurance, this.SparesQuotErrec, this.SparesQuotJurisdiction, this.SparesQuotValidity, this.SparesQuotInspection, this.SparesQuotOtherSpecs, "New", this.SparesQuotRespId, this.SparesQuotSalespId, this.SparesQuotPreparedBy, this.SparesQuotCheckedBy, this.SparesQuotApprovedBy, this.SparesCurrencyId, this.SparesRevisedKey, this.SparesQuotVAT,this.Price);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Quotation Revise Details", "105");

                }
                return _returnStringMessage;
            }

            public string SparesQuotation_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_QUOT_MAST] SET SPARES_QUOT_DATE='{0}',SPARES_QUOT_DELIVERY='{1}',SPARES_QUOT_PAY_TERM='{2}',SPARES_QUOT_PACK_CHARGES='{3}',SPARES_QUOT_EXCISE='{4}',SPARES_QUOT_CST='{5}',DESPM_ID={6},SPARES_QUOT_GUARANTEE='{7}',SPARES_QUOT_TRANS_CHARGES='{8}',SPARES_QUOT_INSURANCE='{9}',SPARES_QUOT_EREC_COMM='{10}',SPARES_QUOT_JURISDICTION='{11}',SPARES_QUOT_VALIDITY='{12}',SPARES_QUOT_INSPECTION='{13}',SPARES_QUOT_OTHER_SPEC='{14}',SPARES_QUOT_RESP_ID='{15}',SPARES_QUOT_SALESP_ID='{16}',SPARES_QUOT_PREPARED_BY='{17}',SPARES_QUOT_CHECKED_BY='{18}',SPARES_QUOT_APPROVED_BY='{19}',SPARES_CURRENCY_ID={20},SPARES_QUOT_VAT='{21}',SPARES_QUOT_PRICE='{23}' WHERE SPARES_QUOT_ID={22}",
                    this.SparesQuotDate, this.SparesQuotDelivery, this.SparesQuotPayTerms, this.SparesQuotPackCharges, this.SparesQuotExcise, this.SparesQuotCST, this.DespmId, this.SparesQuotGuarantee, this.SparesQuotTransCharges, this.SparesQuotInsurance, this.SparesQuotErrec, this.SparesQuotJurisdiction, this.SparesQuotValidity, this.SparesQuotInspection, this.SparesQuotOtherSpecs, this.SparesQuotRespId, this.SparesQuotSalespId, this.SparesQuotPreparedBy, this.SparesQuotCheckedBy, this.SparesQuotApprovedBy, this.SparesCurrencyId, this.SparesQuotVAT,this.SparesQuotId,this.Price);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Spares Quotation Details", "105");

                }
                return _returnStringMessage;
            }

            public string SparesQuotation_Delete(string QuotationId)
            {
                Services.BeginTransaction();

                if (DeleteRecord("[YANTRA_SPARES_QUOT_MAST]", "SPARES_QUOT_ID", QuotationId) == true)
                {
                    Services.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Spares Quotation Details", "105");

                }
                else
                {
                    Services.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }

            public string SparesQuotationDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_QUOT_DET] SELECT ISNULL(MAX(SPARES_QUOT_DET_ID),0)+1,{0},{1},'{2}','{3}' FROM [YANTRA_SPARES_QUOT_DET]", this.SparesQuotId, this.SparesQuotDetItemCode, this.SparesQuotDetQty, this.SparesQuotRate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Quotation Details", "105");

                }
                return _returnStringMessage;
            }

            public int SparesQuotationDetails_Delete(string QuotationId)
            {
                if (DeleteRecord("[YANTRA_SPARES_QUOT_DET]", "SPARES_QUOT_ID", QuotationId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }

            public int SparesQuotation_Select(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_QUOT_MAST],[YANTRA_COMPLAINT_REGISTER],[YANTRA_CUSTOMER_MAST],[YANTRA_LKUP_DESP_MODE]  WHERE [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_SPARES_QUOT_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND" +
                                            " [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.SparesQuotNo = dbManager.DataReader["SPARES_QUOT_NO"].ToString();
                    this.SparesQuotDate = Convert.ToDateTime(dbManager.DataReader["SPARES_QUOT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
                    this.CrNo = dbManager.DataReader["CR_NO"].ToString();
                    this.cust_unit = dbManager.DataReader["cust_unit_id"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.SparesQuotDelivery = dbManager.DataReader["SPARES_QUOT_DELIVERY"].ToString();
                    this.SparesQuotPayTerms = dbManager.DataReader["SPARES_QUOT_PAY_TERM"].ToString();
                    this.SparesQuotPackCharges = dbManager.DataReader["SPARES_QUOT_PACK_CHARGES"].ToString();
                    this.SparesQuotExcise = dbManager.DataReader["SPARES_QUOT_EXCISE"].ToString();
                    this.SparesQuotCST = dbManager.DataReader["SPARES_QUOT_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SparesQuotGuarantee = dbManager.DataReader["SPARES_QUOT_GUARANTEE"].ToString();
                    this.SparesQuotTransCharges = dbManager.DataReader["SPARES_QUOT_TRANS_CHARGES"].ToString();
                    this.SparesQuotInsurance = dbManager.DataReader["SPARES_QUOT_INSURANCE"].ToString();
                    this.SparesQuotErrec = dbManager.DataReader["SPARES_QUOT_EREC_COMM"].ToString();
                    this.SparesQuotJurisdiction = dbManager.DataReader["SPARES_QUOT_JURISDICTION"].ToString();
                    this.SparesQuotValidity = dbManager.DataReader["SPARES_QUOT_VALIDITY"].ToString();
                    this.SparesQuotInspection = dbManager.DataReader["SPARES_QUOT_INSPECTION"].ToString();
                    this.SparesQuotOtherSpecs = dbManager.DataReader["SPARES_QUOT_OTHER_SPEC"].ToString();
                    this.SparesQuotStatus = dbManager.DataReader["SPARES_QUOT_STATUS"].ToString();
                    this.SparesQuotRespId = dbManager.DataReader["SPARES_QUOT_RESP_ID"].ToString();
                    this.SparesQuotSalespId = dbManager.DataReader["SPARES_QUOT_SALESP_ID"].ToString();
                    this.SparesQuotPreparedBy = dbManager.DataReader["SPARES_QUOT_PREPARED_BY"].ToString();
                    this.SparesQuotCheckedBy = dbManager.DataReader["SPARES_QUOT_CHECKED_BY"].ToString();
                    this.SparesQuotApprovedBy = dbManager.DataReader["SPARES_QUOT_APPROVED_BY"].ToString();
                    this.SparesCurrencyId = dbManager.DataReader["SPARES_CURRENCY_ID"].ToString();
                    this.SparesQuotVAT = dbManager.DataReader["SPARES_QUOT_VAT"].ToString();
                    this.Price = dbManager.DataReader["SPARES_QUOT_PRICE"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public string SparesQuotationFollowUp_Save()
            {
                this.CrFollowUpDetId = AutoGenMaxId("[YANTRA_SPARES_QUOT_FOLLOW_DET]", "CR_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_SPARES_QUOT_FOLLOW_DET] VALUES({0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}')", this.CrFollowUpDetId, this.SparesQuotId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate, this.FollowUpTechDiss, this.FollowUpCommNegos, this.FollowUpCompExistance, this.FollowUpRemarks, this.FollowUpExpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Quotation Follow Up Details", "105");

                }
                return _returnStringMessage;
            }

            public string SparesQuotationApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SPARES_QUOT_STATUS FROM [YANTRA_SPARES_QUOT_MAST] WHERE SPARES_QUOT_ID='{0}'", this.SparesQuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_QUOT_MAST] SET SPARES_QUOT_APPROVED_BY={0},SPARES_QUOT_STATUS='{1}' WHERE SPARES_QUOT_ID='{2}'", this.SparesQuotApprovedBy, ServicesStatus.Open, SparesQuotId);
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
                    log.add_Insert("Spares Quotation Approve Details", "105");

                }
                return _returnStringMessage;
            }
            public string SparesQuotationApprove_Update1()
            {
                if (dbManager.Transaction == null)
                   // dbManager.Open();
                _commandText = string.Format("SELECT  SPARES_QUOT_STATUS FROM [YANTRA_SPARES_QUOT_MAST] WHERE SPARES_QUOT_ID='{0}'", this.SparesQuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_QUOT_MAST] SET SPARES_QUOT_APPROVED_BY={0},SPARES_QUOT_STATUS='{1}' WHERE SPARES_QUOT_ID='{2}'", this.SparesQuotApprovedBy, ServicesStatus.Open, SparesQuotId);
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
                    log.add_Update("Spares Quotation Approve Details", "105");

                }
                return _returnStringMessage;
            }

            public string SparesQuotationRegret_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SPARES_QUOT_STATUS FROM [YANTRA_SPARES_QUOT_MAST] WHERE SPARES_QUOT_ID='{0}'", this.SparesQuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_QUOT_MAST] SET SPARES_QUOT_APPROVED_BY={0},SPARES_QUOT_STATUS='{1}' WHERE SPARES_QUOT_ID='{2}'", this.SparesQuotApprovedBy, ServicesStatus.Open, SparesQuotId);
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
                    log.add_Update("Spares Quotation Regreet Details", "105");

                }
                return _returnStringMessage;
            }

            public static string SparesQuotationStatus_Update(ServicesStatus Status, string SparesQuotId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SPARES_QUOT_STATUS FROM [YANTRA_SPARES_QUOT_MAST] WHERE SPARES_QUOT_ID='{0}'", SparesQuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_QUOT_MAST] SET SPARES_QUOT_STATUS='{0}' WHERE SPARES_QUOT_ID='{1}'", Status, SparesQuotId);
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
                    log.add_Update("Spares Quotation Status Details", "105");

                }
                return _returnStringMessage;
            }
            public static string SparesQuotationStatus_Update1(ServicesStatus Status, string SparesQuotId)
            {
                if (dbManager.Transaction == null)
                   // dbManager.Open();
                _commandText = string.Format("SELECT  SPARES_QUOT_STATUS FROM [YANTRA_SPARES_QUOT_MAST] WHERE SPARES_QUOT_ID='{0}'", SparesQuotId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_QUOT_MAST] SET SPARES_QUOT_STATUS='{0}' WHERE SPARES_QUOT_ID='{1}'", Status, SparesQuotId);
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
                    log.add_Update("Spares Quotation Status Details", "105");

                }
                return _returnStringMessage;
            }

            public void SparesQuotationDetails_Select(string QuotationId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_QUOT_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SPARES_QUOT_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SPARES_QUOT_DET].SPARES_QUOT_DET_ID=" + QuotationId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesQuotationItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesQuotationItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SalesQuotationItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesQuotationItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SPARES_QUOT_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SPARES_QUOT_RATE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    SalesQuotationItems.Rows.Add(dr);
                }

                gv.DataSource = SalesQuotationItems;
                gv.DataBind();
            }

            public static void SparesQuotation_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT *,SPARES_QUOT_NO+' '+SPARES_QUOT_REVISED_KEY AS SPARESQUOTNO FROM [YANTRA_SPARES_QUOT_MAST] ORDER BY SPARES_QUOT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPARESQUOTNO", "SPARES_QUOT_ID");
                }
            }
            public static void SparesQuotation_Select1(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    //dbManager.Open();
                _commandText = string.Format("SELECT *,SPARES_QUOT_NO+' '+SPARES_QUOT_REVISED_KEY AS SPARESQUOTNO FROM [YANTRA_SPARES_QUOT_MAST] ORDER BY SPARES_QUOT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPARESQUOTNO", "SPARES_QUOT_ID");
                }
            }
            public static void SparesQuotation_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT *,SPARES_QUOT_NO+' '+SPARES_QUOT_REVISED_KEY AS SPARESQUOTNO FROM [YANTRA_COMPLAINT_REGISTER]  inner join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SPARES_QUOT_MAST].CR_ID  left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_AMC_ENQ_ASSIGN_TASKS].EMP_ID  where [YANTRA_AMC_ENQ_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY SPARES_QUOT_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPARESQUOTNO", "SPARES_QUOT_ID");
                }
            }

            public int Get_Ids_Select(string QuotationId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_QUOT_MAST],[YANTRA_SERVICES_ASSIGN_TASKS],[YANTRA_COMPLAINT_REGISTER],[YANTRA_CUSTOMER_MAST],[YANTRA_LKUP_DESP_MODE]  WHERE [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_SPARES_QUOT_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_SERVICES_ASSIGN_TASKS].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND " +
                                            " [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["AMC_ASSIGN_TASK_ID"].ToString();
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
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
            public int Get_Ids_Select1(string QuotationId)
            {
                if (dbManager.Transaction == null)
                   // dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_QUOT_MAST],[YANTRA_SERVICES_ASSIGN_TASKS],[YANTRA_COMPLAINT_REGISTER],[YANTRA_CUSTOMER_MAST],[YANTRA_LKUP_DESP_MODE]  WHERE [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_SPARES_QUOT_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_SERVICES_ASSIGN_TASKS].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND " +
                                            " [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["AMC_ASSIGN_TASK_ID"].ToString();
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
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

            public static void SparesQuotationItemTypes_Select(string QuotationId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_SPARES_QUOT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_SPARES_QUOT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_SPARES_QUOT_DET].SPARES_QUOT_ID=" + QuotationId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void SparesQuotationItemNames_Select(string QuotationId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_QUOT_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_SPARES_QUOT_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SPARES_QUOT_DET].SPARES_QUOT_IDD=" + QuotationId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }
            public static void CompalintRegister_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER] ORDER BY CR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "CR_NO", "CR_ID");
                }
            }


            public static string CompalintRegisterStatus_Update(ServicesStatus Status, string CrId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER] SET  CR_STATUS='{0}' WHERE CR_ID='{1}'", Status, CrId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Compalint Register Status Details", "97");

                }
                return _returnStringMessage;
            }
            public static string CompalintRegisterStatus_Update1(ServicesStatus Status, string CrId)
            {
                if (dbManager.Transaction == null)
                    //dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER] SET  CR_STATUS='{0}' WHERE CR_ID='{1}'", Status, CrId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Compalint Register Status Details", "97");

                }
                return _returnStringMessage;
            }

            public int CompalintRegister_Select(string CrId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],YANTRA_COMPLAINT_REGISTER_DET,[YANTRA_CUSTOMER_MAST],[YANTRA_CUSTOMER_UNITS],YANTRA_CUSTOMER_DET  WHERE  [YANTRA_COMPLAINT_REGISTER].CUST_DET_ID=YANTRA_CUSTOMER_DET.CUST_DET_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID AND " +
                                            " [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_COMPLAINT_REGISTER].CR_ID='" + CrId + "' ORDER BY [YANTRA_COMPLAINT_REGISTER].CR_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
                    this.CrNo = dbManager.DataReader["CR_NO"].ToString();
                    this.CrDate = Convert.ToDateTime(dbManager.DataReader["CR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.cust_unit_add = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.Call = dbManager.DataReader["CR_CALL_TYPE"].ToString();
                    this.cust_unit = dbManager.DataReader["cust_unit_name"].ToString();
                 //  this.Complaint = dbManager.DataReader["CR_NATURE_OF_COMPLAINT"].ToString();
                 //  this.CauseNo = dbManager.DataReader["CR_ROOT_CAUSE_NOTICED"].ToString();
                 //   this.Corrective = dbManager.DataReader["CR_CORRECTIVE_ACTION_TAKEN"].ToString();
                    this.PreparedBy = dbManager.DataReader["CR_PREPARED_BY"].ToString();
                    this.Staus = dbManager.DataReader["CR_STATUS"].ToString();




                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }
            public int complaintrecord_isrecordexists(object p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_SPARES_QUOT_MAST where cr_id=" + p + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;  
            }
        
            
        }

        //Methods For Spares Order Form
        public class SparesOrder
        {
            public string SPOId, SPONo, CrId, SPODate, SparesQuotId, SparesQuotNo, CustId, SPORespId, SPOSalespId, SPOPreparedBy, SPOCheckedBy, SPOApprovedBy, SPOAcceptanceFlag, SPODelivery, SPOCurrencyTypeId, SPOPackageCharges, SPOPaymentTerms, SPOCSTax, SPOExciseDuty, SPOGuarantee, DespmId, SPOInsurance, SPOTransportCharges, SPOJurisdiction, SPOErection, SPOInspection, SPOValidity, SPOOtherSpec, EnqId, AssignTaskId, ContactName1, ContactPhone1, ContactEmail1, ContactName2, ContactPhone2, ContactEmail2, ConsignmentTo, InvoiceTo, ContactDesig1, ContactDesig2, SPOAdvanceAmt, SPOFLag, SPOFiles, SPOVAT, SPOAccessories, SPOExtraSpares, SOFiles;
            public string SPODetId, SPOItemCode, SPODetQty, SPORate, SPODetSpec, SPODetRemarks, SPODetPriority, SPODetDeliveryStatus;
            public string SOUploadId, SOUploadFileName, SOUploadDate;
            public enum SOItemStatus { PartiallyDelivered = 0, Delivered = 1, }

            public SparesOrder()
            {
            }

            public static string SparesOrder_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SPO_NO,LEFT(SPO_NO,5),''))),0)+1 FROM [YANTRA_SPARES_ORDER_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(SO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_SO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SPARES_ORDER_MAST", "SPO_NO");
            }

            public string SparesOrder_Save()
            {
                this.SPONo = SparesOrder_AutoGenCode();
                this.SPOId = AutoGenMaxId("[YANTRA_SPARES_ORDER_MAST]", "SPO_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_ORDER_MAST] SELECT ISNULL(MAX(SPO_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}',{32},{33},'{34}','{35}','{36}','{37}','{38}' FROM [YANTRA_SPARES_ORDER_MAST]", this.SPONo, this.SPODate, this.SparesQuotId, this.SPORespId, this.SPOSalespId, this.SPOPreparedBy, this.SPOCheckedBy, this.SPOApprovedBy, this.SPOAcceptanceFlag, this.SPODelivery, this.SPOCurrencyTypeId, this.SPOPaymentTerms, this.SPOPackageCharges, this.SPOExciseDuty, this.SPOCSTax, this.DespmId, this.SPOGuarantee, this.SPOTransportCharges, this.SPOInsurance, this.SPOErection, this.SPOJurisdiction, this.SPOValidity, this.SPOInspection, this.SPOOtherSpec, this.ContactName1, this.ContactPhone1, this.ContactEmail1, this.ContactName2, this.ContactPhone2, this.ContactEmail2, this.ConsignmentTo, this.InvoiceTo, this.ContactDesig1, this.ContactDesig2, this.SPOAdvanceAmt, this.SOFiles, this.SPOVAT, this.SPOAccessories, this.SPOExtraSpares);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Order Details", "107");

                }
                return _returnStringMessage;
            }

            public string SparesOrder_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_ORDER_MAST] SET SPO_DATE='{0}',SPO_RESP_ID='{1}',SPO_SALESP_ID='{2}',SPO_PREPARED_BY='{3}',SPO_CHECKED_BY='{4}',SPO_APPROVED_BY='{5}',SPO_DELIVERY='{6}',CURRENCY_ID={7},SPO_PAY_TERM='{8}',SPO_PACK_CHARGES='{9}',SPO_EXCISE='{10}',SPO_CST='{11}',DESPM_ID={12},SPO_GUARANTEE='{13}',SPO_TRANS_CHARGES='{14}',SPO_INSURANCE='{15}',SPO_EREC_COMM='{16}',SPO_JURISDICTION='{17}',SPO_VALIDITY='{18}',SPO_INSPECTION='{19}',SPO_OTHER_SPEC='{20}',SPO_CONTACT_NAME1='{21}',SPO_CONTACT_PHONE1='{22}',SPO_CONTACT_EMAIL1='{23}',SPO_CONTACT_NAME2='{24}',SPO_CONTACT_PHONE2='{25}',SPO_CONTACT_EMAIL2='{26}',SPO_CONSIGNMENT_TO='{27}',SPO_INVOICE_TO='{28}',SPO_DESIGNATION1={29},SPO_DESIGNATION2={30},SPO_ADVANCE_AMT='{31}',SPO_FILES='{32}',SPO_VAT='{33}',SPO_ACCESSORIES='{34}',SPO_EXTRA_SPARES='{35}' WHERE SPO_ID='{36}'", this.SPODate, this.SPORespId, this.SPOSalespId, this.SPOPreparedBy, this.SPOCheckedBy, this.SPOApprovedBy, this.SPODelivery, this.SPOCurrencyTypeId, this.SPOPaymentTerms, this.SPOPackageCharges, this.SPOExciseDuty, this.SPOCSTax, this.DespmId, this.SPOGuarantee, this.SPOTransportCharges, this.SPOInsurance, this.SPOErection, this.SPOJurisdiction, this.SPOValidity, this.SPOInspection, this.SPOOtherSpec, this.ContactName1, this.ContactPhone1, this.ContactEmail1, this.ContactName2, this.ContactPhone2, this.ContactEmail2, this.ConsignmentTo, this.InvoiceTo, this.ContactDesig1, this.ContactDesig2, this.SPOAdvanceAmt, this.SPOFiles, this.SPOVAT, this.SPOAccessories, this.SPOExtraSpares, this.SPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Spares Order Details", "107");

                }
                return _returnStringMessage;
            }

            public string SparesOrder_Delete(string SparesOrderId)
            {
                Services.BeginTransaction();

                if (DeleteRecord("[YANTRA_SPARES_ORDER_MAST]", "SPO_ID", SparesOrderId) == true)
                {
                    Services.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Spares Order Details", "107");

                }
                else
                {
                    Services.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }

            public string SparesOrderDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_ORDER_DET] SELECT ISNULL(MAX(SPO_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','' FROM [YANTRA_SPARES_ORDER_DET]", this.SPOId, this.SPOItemCode, this.SPODetQty, this.SPORate, this.SPODetSpec, this.SPODetPriority, this.SPODetRemarks, this.SPODetDeliveryStatus);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Order Details", "107");

                }
                return _returnStringMessage;
            }

            public int SparesOrderDetails_Delete(string SparesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SPARES_ORDER_DET] WHERE SPO_ID={0}", SparesOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int SparesOrder_Select(string SparesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_QUOT_MAST],[YANTRA_LKUP_DESP_MODE],[YANTRA_LKUP_CURRENCY_TYPE],YANTRA_COMPLAINT_REGISTER,YANTRA_CUSTOMER_MAST WHERE  [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID  AND [YANTRA_SPARES_ORDER_MAST].CURRENCY_ID=[YANTRA_LKUP_CURRENCY_TYPE].CURRENCY_ID  AND YANTRA_COMPLAINT_REGISTER.CUST_ID=YANTRA_CUSTOMER_MAST.CUST_ID AND [YANTRA_SPARES_QUOT_MAST].CR_ID=YANTRA_COMPLAINT_REGISTER.CR_ID AND  " +
                                            "[YANTRA_SPARES_ORDER_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND YANTRA_COMPLAINT_REGISTER.CUST_ID=YANTRA_CUSTOMER_MAST.CUST_ID  AND [YANTRA_SPARES_ORDER_MAST].SPO_ID='" + SparesOrderId + "' ORDER BY [YANTRA_SPARES_ORDER_MAST].SPO_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SPONo = dbManager.DataReader["SPO_NO"].ToString();
                    this.SPODate = Convert.ToDateTime(dbManager.DataReader["SPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.SparesQuotNo = dbManager.DataReader["SPARES_QUOT_NO"].ToString();
                    this.SPORespId = dbManager.DataReader["SPO_RESP_ID"].ToString();
                    this.SPOSalespId = dbManager.DataReader["SPO_SALESP_ID"].ToString();
                    this.SPOPreparedBy = dbManager.DataReader["SPO_PREPARED_BY"].ToString();
                    this.SPOCheckedBy = dbManager.DataReader["SPO_CHECKED_BY"].ToString();
                    this.SPOApprovedBy = dbManager.DataReader["SPO_APPROVED_BY"].ToString();
                    this.SPOAcceptanceFlag = dbManager.DataReader["SPO_ACCEPTANCE_FLAG"].ToString();
                    this.SPODelivery = dbManager.DataReader["SPO_DELIVERY"].ToString();
                    this.SPOCurrencyTypeId = dbManager.DataReader["CURRENCY_ID"].ToString();
                    this.SPOPaymentTerms = dbManager.DataReader["SPO_PAY_TERM"].ToString();
                    this.SPOPackageCharges = dbManager.DataReader["SPO_PACK_CHARGES"].ToString();
                    this.SPOExciseDuty = dbManager.DataReader["SPO_EXCISE"].ToString();
                    this.SPOCSTax = dbManager.DataReader["SPO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SPOGuarantee = dbManager.DataReader["SPO_GUARANTEE"].ToString();
                    this.SPOTransportCharges = dbManager.DataReader["SPO_TRANS_CHARGES"].ToString();
                    this.SPOInsurance = dbManager.DataReader["SPO_INSURANCE"].ToString();
                    this.SPOErection = dbManager.DataReader["SPO_EREC_COMM"].ToString();
                    this.SPOJurisdiction = dbManager.DataReader["SPO_JURISDICTION"].ToString();
                    this.SPOValidity = dbManager.DataReader["SPO_VALIDITY"].ToString();
                    this.SPOInspection = dbManager.DataReader["SPO_INSPECTION"].ToString();
                    this.SPOOtherSpec = dbManager.DataReader["SPO_OTHER_SPEC"].ToString();
                    this.ContactName1 = dbManager.DataReader["SPO_CONTACT_NAME1"].ToString();
                    this.ContactPhone1 = dbManager.DataReader["SPO_CONTACT_PHONE1"].ToString();
                    this.ContactEmail1 = dbManager.DataReader["SPO_CONTACT_EMAIL1"].ToString();
                    this.ContactName2 = dbManager.DataReader["SPO_CONTACT_NAME2"].ToString();
                    this.ContactPhone2 = dbManager.DataReader["SPO_CONTACT_PHONE2"].ToString();
                    this.ContactEmail2 = dbManager.DataReader["SPO_CONTACT_EMAIL2"].ToString();
                    this.ConsignmentTo = dbManager.DataReader["SPO_CONSIGNMENT_TO"].ToString();
                    this.InvoiceTo = dbManager.DataReader["SPO_INVOICE_TO"].ToString();
                    this.ContactDesig1 = dbManager.DataReader["SPO_DESIGNATION1"].ToString();
                    this.ContactDesig2 = dbManager.DataReader["SPO_DESIGNATION2"].ToString();
                    this.SPOAdvanceAmt = dbManager.DataReader["SPO_ADVANCE_AMT"].ToString();
                    this.SPOFiles = dbManager.DataReader["SPO_FILES"].ToString();
                    this.SPOVAT = dbManager.DataReader["SPO_VAT"].ToString();
                    this.SPOAccessories = dbManager.DataReader["SPO_ACCESSORIES"].ToString();
                    this.SPOExtraSpares = dbManager.DataReader["SPO_EXTRA_SPARES"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public static string SparesOrderStatus_Update(ServicesStatus Status, string SPOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_ORDER_MAST] SET  SPO_ACCEPTANCE_FLAG='{0}' WHERE SPO_ID='{1}'", Status, SPOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Spares Order Status Details", "107");

                }
                return _returnStringMessage;
            }

            public void SparesOrderDetails_Select(string SparesOrderId, GridView gv)
            {

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SPARES_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SPARES_ORDER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SPARES_ORDER_DET].SPO_ID=" + SparesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
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
                col = new DataColumn("ItemTypeId");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("DeliveryStatus");
                SalesOrderProducts.Columns.Add(col);
                col = new DataColumn("SODetId");
                SalesOrderProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SPO_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SPO_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SPO_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SPO_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SPO_DET_PRIORITY"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["DeliveryStatus"] = dbManager.DataReader["SPO_DET_DELIVERY_STATUS"].ToString();
                    dr["SODetId"] = dbManager.DataReader["SPO_DET_ID"].ToString();

                    SalesOrderProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderProducts;
                gv.DataBind();
            }

            public static string SparesOrderDetailsItemStatus_Update(SOItemStatus Status, string SPODetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_ORDER_DET] SET  SPO_DET_DELIVERY_STATUS='{0}' WHERE SPO_DET_ID='{1}'", Status, SPODetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Spares Order Details", "107");

                }
                return _returnStringMessage;
            }

            public static string SparesOrderDetailsItemStatusReset_Update(string SPODetId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_ORDER_DET] SET  SPO_DET_DELIVERY_STATUS='-' WHERE SPO_DET_ID='{0}'", SPODetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Spares Order Status Details", "107");

                }
                return _returnStringMessage;
            }

            public string SparesOrderApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SPO_ACCEPTANCE_FLAG FROM [YANTRA_SPARES_ORDER_MAST] WHERE SPO_ID='{0}'", this.SPOId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_ORDER_MAST] SET SPO_APPROVED_BY={0},SPO_ACCEPTANCE_FLAG='{1}' WHERE SPO_ID='{2}'", this.SPOApprovedBy, ServicesStatus.Open, this.SPOId);
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

                    log.add_Update("Spares Order Approve Details", "107");

                }
                return _returnStringMessage;
            }

            public int Get_Ids_Select(string SparesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_COMPLAINT_REGISTER,[YANTRA_SERVICES_ASSIGN_TASKS],[YANTRA_CUSTOMER_MAST],[YANTRA_SPARES_QUOT_MAST],[YANTRA_SPARES_ORDER_MAST],YANTRA_LKUP_DESP_MODE  WHERE [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND  [YANTRA_SPARES_QUOT_MAST].DESPM_ID=YANTRA_LKUP_DESP_MODE.DESPM_ID AND  [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND " +
                                            " [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID AND [YANTRA_SERVICES_ASSIGN_TASKS].CR_ID=YANTRA_COMPLAINT_REGISTER.CR_ID  AND [YANTRA_SPARES_ORDER_MAST].SPO_ID='" + SparesOrderId + "' ORDER BY YANTRA_COMPLAINT_REGISTER.CR_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.AssignTaskId = dbManager.DataReader["SERVICE_ASSIGN_TASK_ID"].ToString();
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
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

            public static void SparesOrder_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_MAST] ORDER BY SPO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPO_NO", "SPO_ID");
                }
            }
            public static void SparesOrderByCRId_Select(Control ControlForBind, string CRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format(" SELECT * FROM [YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_QUOT_MAST] WHERE [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID AND [YANTRA_SPARES_QUOT_MAST].CR_ID=" + CRId + " ORDER BY [YANTRA_SPARES_ORDER_MAST].SPO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPO_NO", "SPO_ID");
                }
            }

            public static void SparesOrderForDelivery_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_MAST] WHERE SPO_ID IN (SELECT SPO_ID FROM [YANTRA_SPARES_ORDER_DET] WHERE SPO_DET_DELIVERY_STATUS <> 'DELIVERED') ORDER BY SPO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPO_NO", "SPO_ID");
                }
            }

            public static void SparesOrder_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_SPARES_ORDER_MAST].* FROM [YANTRA_COMPLAINT_REGISTER] inner join [YANTRA_SERVICES_ASSIGN_TASKS] on YANTRA_SERVICES_ASSIGN_TASKS.CR_ID = YANTRA_COMPLAINT_REGISTER.CR_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_SERVICES_ASSIGN_TASKS].EMP_ID inner join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SPARES_QUOT_MAST].CR_ID inner join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID where [YANTRA_SERVICES_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_SPARES_ORDER_MAST] .SPO_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPO_NO", "SPO_ID");
                }
            }

            public string SparesOrderUploads_Save()
            {
                //  this.SONo = SalesOrder_AutoGenCode();
                this.SOUploadId = AutoGenMaxId("[YANTRA_SPARES_UPLOAD]", "SP_UPLOAD_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_UPLOAD] SELECT ISNULL(MAX(SP_UPLOAD_ID),0)+1,{0},'{1}','{2}' FROM [YANTRA_SPARES_UPLOAD]", this.SPOId, this.SOUploadFileName, this.SOUploadDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Order Upload Details", "107");

                }
                return _returnStringMessage;
            }

            public static void SparesOrderItemTypes_Select(string SalesOrderId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_SPARES_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_SPARES_ORDER_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID and YANTRA_ITEM_MAST.Status !=0   AND  [YANTRA_SPARES_ORDER_DET].SPO_ID=" + SalesOrderId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void SparesOrderItemNames_Select(string SparesOrderId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_SPARES_ORDER_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SPARES_ORDER_DET].SPO_ID=" + SparesOrderId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
            }

            public static void SparesOrderForPayments_Select(Control ControlForBind, string CustomerId, string UnitId, string SaveButtonText)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_QUOT_MAST],[YANTRA_COMPLAINT_REGISTER] WHERE [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID =[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID  AND [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=1 AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=1 AND [YANTRA_SPARES_ORDER_MAST].SPO_ID IN (SELECT SPO_ID FROM YANTRA_AMC_PAYMENT_RECEIVED WHERE AMCPR_PAYMENT_STATUS <> 'Cleared') AND [YANTRA_SPARES_ORDER_MAST].SPO_ID IN (SELECT [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SALES_INVOICE_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID) ORDER BY [YANTRA_SPARES_ORDER_MAST].SPO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SPO_NO"].ToString(), dbManager.DataReader["SPO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_QUOT_MAST],[YANTRA_COMPLAINT_REGISTER] WHERE [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID AND [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=" + CustomerId + " AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=" + UnitId + " AND [YANTRA_SPARES_ORDER_MAST].SPO_ID NOT IN (SELECT SPO_ID   FROM YANTRA_AMC_PAYMENT_RECEIVED) AND  [YANTRA_SPARES_ORDER_MAST].SPO_ID IN (SELECT [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SALES_INVOICE_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID) ORDER BY [YANTRA_SPARES_ORDER_MAST].SPO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SPO_NO"].ToString(), dbManager.DataReader["SPO_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_QUOT_MAST],[YANTRA_COMPLAINT_REGISTER] WHERE [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID =[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID  AND [YANTRA_SPARES_QUOT_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=" + CustomerId + " AND [YANTRA_COMPLAINT_REGISTER].CUST_UNIT_ID=" + UnitId + "  ORDER BY [YANTRA_SPARES_ORDER_MAST].SPO_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "SPO_NO", "SPO_ID");
                    }
                }
            }
            public int sparesQuotation_isrecordexists(string p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_SPARES_ORDER_MAST where SPARES_QUOT_ID=" + p + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public int SparesOrder_isrecordexists(string p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_SPARES_OP_MAST where SPO_ID=" + p + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;  
            }
        }

        //Methods For Work Order Form
        public class OrderProfile
        {
            public string SWOId, SWONo, SWODate, SPOId, cridnew,SPONo, DespId, CustId, SWOInspDate, SWOPackForwInst, SWODeliveryDate, SWOAccessories, SWOExtraSpares, SWOPreparedBy, SWOCheckedBy, SWOApprovedBy, SWOFLag, SWOFrieght, SWORoadPermit, SWOFiles, SPOAdvanceAmt, SWOCSTax;
            public string SWODetId, SWOItemCode, SWODetQty, SWODetSpec, SWODetRemarks;

            public OrderProfile()
            {
            }

            public static string OrderProfile_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "WO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SPOP_NO,LEFT(SPOP_NO,5),''))),0)+1 FROM [YANTRA_SPARES_OP_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(WO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_WO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SPARES_OP_MAST", "SPOP_NO");
            }

            public string OrderProfile_Save()
            {
                this.SWONo = OrderProfile_AutoGenCode();
                this.SWOId = AutoGenMaxId("[YANTRA_SPARES_OP_MAST]", "SPOP_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_OP_MAST] SELECT ISNULL(MAX(SPOP_ID),0)+1,'{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}' FROM [YANTRA_SPARES_OP_MAST]", this.SWONo, this.SWODate, this.SPOId, this.DespId, this.SWOInspDate, this.SWOPackForwInst, this.SWODeliveryDate, this.SWOAccessories, this.SWOExtraSpares, this.SWOPreparedBy, this.SWOCheckedBy, this.SWOApprovedBy, "New", this.SWOFrieght, this.SWORoadPermit, this.SWOFiles, this.SWOCSTax);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Order Profile Details", "108");

                }
                return _returnStringMessage;
            }

            public string OrderProfile_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_OP_MAST] SET SPOP_DATE='{0}',DESPM_ID={1},SPOP_INSP_DATE='{2}',SPOP_PACK_FORW_INS='{3}',SPOP_DELIVERY_DATE='{4}',SPOP_ACCESSORIES='{5}',SPOP_EXTRA_SPARES='{6}',SPOP_PREPARED_BY='{7}',SPOP_CHECKED_BY='{8}',SPOP_APPROVED_BY='{9}',SPOP_FRIEGHT='{10}',SPOP_ROAD_PERMIT='{11}',SPOP_FILES='{12}',SPOP_CSTAX='{13}' WHERE SPOP_ID={14}", this.SWODate, this.DespId, this.SWOInspDate, this.SWOPackForwInst, this.SWODeliveryDate, this.SWOAccessories, this.SWOExtraSpares, this.SWOPreparedBy, this.SWOCheckedBy, this.SWOApprovedBy, this.SWOFrieght, this.SWORoadPermit, this.SWOFiles, this.SWOCSTax, this.SWOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Order Profile Details", "108");

                }
                return _returnStringMessage;
            }

            public string OrderProfile_Delete(string WorkOrderId)
            {
                Services.BeginTransaction();
                if (DeleteRecord("[YANTRA_SPARES_OP_MAST]", "SPOP_ID", WorkOrderId) == true)
                {
                    Services.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Order Profile Details", "108");

                }
                else
                {
                    Services.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }

            public string OrderProfileDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_OP_DET] SELECT ISNULL(MAX(SPOP_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}' FROM [YANTRA_SPARES_OP_DET]", this.SWOId, this.SWOItemCode, this.SWODetQty, this.SWODetSpec, this.SWODetRemarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Order Profile Details", "108");

                }
                return _returnStringMessage;
            }

            public int OrderProfileDetails_Delete(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SPARES_OP_DET] WHERE SPOP_ID={0}", WorkOrderId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int OrderProfile_Select(string WorkOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_QUOT_MAST],[YANTRA_COMPLAINT_REGISTER],[YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_OP_MAST] WHERE [YANTRA_COMPLAINT_REGISTER].CR_ID = [YANTRA_SPARES_QUOT_MAST].CR_ID " +
                                            " AND [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_SPARES_OP_MAST].SPO_ID " +
                                            " AND [YANTRA_SPARES_OP_MAST].SPOP_ID='" + WorkOrderId + "' ORDER BY [YANTRA_SPARES_OP_MAST].SPOP_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SWOId = dbManager.DataReader["SPOP_ID"].ToString();
                    this.SWONo = dbManager.DataReader["SPOP_NO"].ToString();
                    this.SWODate = Convert.ToDateTime(dbManager.DataReader["SPOP_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SPONo = dbManager.DataReader["SPO_No"].ToString();
                    this.cridnew = dbManager.DataReader["CR_ID"].ToString();

                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DespId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SWOInspDate = dbManager.DataReader["SPOP_INSP_DATE"].ToString();
                    this.SWOPackForwInst = dbManager.DataReader["SPOP_PACK_FORW_INS"].ToString();
                    this.SWODeliveryDate = Convert.ToDateTime(dbManager.DataReader["SPOP_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SWOAccessories = dbManager.DataReader["SPOP_ACCESSORIES"].ToString();
                    this.SWOExtraSpares = dbManager.DataReader["SPOP_EXTRA_SPARES"].ToString();
                    this.SWOPreparedBy = dbManager.DataReader["SPOP_PREPARED_BY"].ToString();
                    this.SWOCheckedBy = dbManager.DataReader["SPOP_CHECKED_BY"].ToString();
                    this.SWOApprovedBy = dbManager.DataReader["SPOP_APPROVED_BY"].ToString();
                    this.SWOFrieght = dbManager.DataReader["SPOP_FRIEGHT"].ToString();
                    this.SWORoadPermit = dbManager.DataReader["SPOP_ROAD_PERMIT"].ToString();
                    this.SWOFiles = dbManager.DataReader["SPOP_FILES"].ToString();
                    this.SWOCSTax = dbManager.DataReader["SPOP_CSTAX"].ToString();
                    this.SPOAdvanceAmt = dbManager.DataReader["SPO_ADVANCE_AMT"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public static string GetOrderProfileIdOfSparesOrder(string SparesOrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SPOP_ID FROM [YANTRA_SPARES_OP_MAST] WHERE SPO_ID='" + SparesOrderId + "' ORDER BY [YANTRA_SPARES_OP_MAST].SPOP_ID DESC ");
                _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();

                return _returnStringMessage;
            }

            public void OrderProfileDetails_Select(string WorkOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SPARES_OP_DET] WHERE [YANTRA_SPARES_OP_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_SPARES_OP_DET].SPOP_ID=" + WorkOrderId + "");
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
                    dr["Quantity"] = dbManager.DataReader["SPOP_DET_QTY"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SPOP_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SPOP_DET_REMARKS"].ToString();


                    OrderAcceptanceProducts.Rows.Add(dr);
                }

                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }

            public string OrderProfileApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SPOP_STATUS FROM [YANTRA_SPARES_OP_MAST] WHERE SPOP_ID='{0}'", this.SWOId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_OP_MAST] SET  SPOP_APPROVED_BY={0},SPOP_STATUS='{1}' WHERE SPOP_ID='{2}'", this.SWOApprovedBy, ServicesStatus.Open, this.SWOId);
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
                    log.add_Insert("Order Profile Approve Details", "108");

                }
                return _returnStringMessage;
            }

            public static string OrderProfileStatus_Update(ServicesStatus Status, string SWOId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_OP_MAST] SET  SPOP_STATUS='{0}' WHERE SPOP_ID='{1}'", Status, SWOId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Order Profile Status Details", "108");

                }
                return _returnStringMessage;
            }

            public static void OrderProfile_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_OP_MAST] WHERE SPOP_STATUS='Open' ORDER BY SPOP_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPOP_NO", "SPOP_ID");
                }
            }

            public static void OrderProfile_SelectAll(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_OP_MAST] ORDER BY SPOP_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPOP_NO", "SPOP_ID");
                }
            }

            public static void OrderProfile_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_SPARES_OP_MAST].* FROM [YANTRA_COMPLAINT_REGISTER] inner join [YANTRA_SERVICES_ASSIGN_TASKS] on YANTRA_SERVICES_ASSIGN_TASKS.CR_ID = YANTRA_COMPLAINT_REGISTER.CR_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_SERVICES_ASSIGN_TASKS].EMP_ID inner join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SPARES_QUOT_MAST].CR_ID inner join [YANTRA_SPARES_ORDER_MAST] on [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID inner join [YANTRA_SPARES_OP_MAST] on [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_SPARES_OP_MAST].SPO_ID where [YANTRA_SERVICES_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_SPARES_OP_MAST].SPOP_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPOP_NO", "SPOP_ID");
                }
            }


        }

        //Methods For Order Acceptance Form
        public class SparesOrderAcceptance
        {
            public string SOAId, SOANo, SOADate, SPOId, SPONo, SWOId, SWONo, SSOACSTax, DespmId, SparesQuotNo, SparesQuotId, CustId, SOARespId, SOASalespId, SOAPreparedBy, SOACheckedBy, SOAApprovedBy, SOAFlag, SOAAcceptanceFlag, SOAConsignee, TransId, SOADeliveryDate, SOACSTax, SOAInspection, SOAInvoiceTo, Responsible;
            public string SOADetId, SOAItemCode, SOADetQty, SOARate, SOADetSpec, SOADetRemarks, SOADetPriority;

            public SparesOrderAcceptance()
            {
            }

            public static string SparesOrderAcceptance_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "OA/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SPOA_NO,LEFT(SPOA_NO,5),''))),0)+1 FROM [YANTRA_SPARES_OA_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(OA_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_OA_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SPARES_OA_MAST", "SPOA_NO");
            }

            public string SparesOrderAcceptance_Save()
            {
                this.SOANo = SparesOrderAcceptance_AutoGenCode();
                this.SOAId = AutoGenMaxId("[YANTRA_SPARES_OA_MAST]", "SPOA_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_OA_MAST] SELECT ISNULL(MAX(SPOA_ID),0)+1,'{0}','{1}',{2},'{3}','{4}',{5},{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}' FROM [YANTRA_SPARES_OA_MAST]", this.SOANo, this.SOADate, this.SWOId, this.SOARespId, this.SOASalespId, this.DespmId, this.TransId, this.SOADeliveryDate, this.SOAPreparedBy, this.SOACheckedBy, this.SOAApprovedBy, this.SOAFlag, this.SOAConsignee, this.SOACSTax, this.SOAInspection, this.SOAInvoiceTo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Order Acceptance Details", "107");

                }
                return _returnStringMessage;
            }

            public string SparesOrderAcceptance_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_OA_MAST] SET SPOA_DATE='{0}',SPOA_RESP_ID='{1}',SPOA_SALESP_ID='{2}',SPOA_PREPARED_BY='{3}',SPOA_CHECKED_BY='{4}',SPOA_APPROVED_BY='{5}',SPOA_CONSIGNEE='{6}',DESPM_ID={7},TRANS_ID={8},SPOA_DELIVERY_DATE='{9}',SPOA_CSTAX='{10}',SPOA_INSPECTION='{11}',SPOA_INVOICE_TO='{12}' WHERE SPOA_ID={13}", this.SOADate, this.SOARespId, this.SOASalespId, this.SOAPreparedBy, this.SOACheckedBy, this.SOAApprovedBy, this.SOAConsignee, this.DespmId, this.TransId, this.SOADeliveryDate, this.SOACSTax, this.SOAInspection, this.SOAInvoiceTo, this.SOAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Insert("Spares Order Acceptance Details", "107");

                }
                return _returnStringMessage;
            }

            public string SparesOrderAcceptance_Delete(string OrderAcceptanceId)
            {
                Services.BeginTransaction();

                if (DeleteRecord("[YANTRA_SPARES_OA_MAST]", "SPOA_ID", OrderAcceptanceId) == true)
                {
                    Services.CommitTransaction();
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Spares Order Profile Details", "107");

                }
                else
                {
                    Services.RollBackTransaction();
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }

            public string SparesOrderAcceptanceDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SPARES_OA_DET] SELECT ISNULL(MAX(SPOA_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_SPARES_OA_DET]", this.SOAId, this.SOAItemCode, this.SOADetQty, this.SOARate, this.SOADetSpec, this.SOADetPriority, this.SOADetRemarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Spares Order Acceptance Details", "107");

                }
                return _returnStringMessage;
            }

            public int SparesOrderAcceptanceDetails_Delete(string OrderAcceptanceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SPARES_OA_DET] WHERE SPOA_ID={0}", OrderAcceptanceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public int SparesOrderAcceptance_Select(string OrderAcceptanceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  * FROM [YANTRA_LKUP_DESP_MODE],[YANTRA_COMPLAINT_REGISTER],[YANTRA_SPARES_QUOT_MAST],[YANTRA_SPARES_ORDER_MAST],[YANTRA_SPARES_OP_MAST],[YANTRA_SPARES_OA_MAST] WHERE [YANTRA_COMPLAINT_REGISTER].CR_ID = [YANTRA_SPARES_QUOT_MAST].CR_ID " +
                    " AND [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_SPARES_OP_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID" +
                                            " AND [YANTRA_SPARES_OP_MAST].SPOP_ID=[YANTRA_SPARES_OA_MAST].SPOP_ID AND [YANTRA_SPARES_OA_MAST].SPOA_ID='" + OrderAcceptanceId + "' ORDER BY [YANTRA_SPARES_OA_MAST].SPOA_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SOANo = dbManager.DataReader["SPOA_NO"].ToString();
                    this.SOADate = Convert.ToDateTime(dbManager.DataReader["SPOA_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SPONo = dbManager.DataReader["SPO_NO"].ToString();
                    this.SWOId = dbManager.DataReader["SPOP_ID"].ToString();
                    this.SWONo = dbManager.DataReader["SPOP_ID"].ToString();
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.SparesQuotNo = dbManager.DataReader["SPARES_QUOT_NO"].ToString();
                    this.SSOACSTax = dbManager.DataReader["SPO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.SparesQuotId = dbManager.DataReader["SPARES_QUOT_ID"].ToString();
                    this.SparesQuotNo = dbManager.DataReader["SPARES_QUOT_NO"].ToString();
                    this.SOARespId = dbManager.DataReader["SPOA_RESP_ID"].ToString();
                    this.SOASalespId = dbManager.DataReader["SPOA_SALESP_ID"].ToString();
                    this.SOAPreparedBy = dbManager.DataReader["SPOA_PREPARED_BY"].ToString();
                    this.SOACheckedBy = dbManager.DataReader["SPOA_CHECKED_BY"].ToString();
                    this.SOAApprovedBy = dbManager.DataReader["SPOA_APPROVED_BY"].ToString();
                    this.SOAAcceptanceFlag = dbManager.DataReader["SPOA_STATUS"].ToString();
                    this.SOAConsignee = dbManager.DataReader["SPOA_CONSIGNEE"].ToString();
                    this.SOADeliveryDate = Convert.ToDateTime(dbManager.DataReader["SPOA_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SOACSTax = dbManager.DataReader["SPOA_CSTAX"].ToString();
                    this.SOAInspection = dbManager.DataReader["SPOA_INSPECTION"].ToString();
                    this.SOAInvoiceTo = dbManager.DataReader["SPOA_INVOICE_TO"].ToString();
                    this.TransId = dbManager.DataReader["TRANS_ID"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public static string SparesOrderAcceptanceStatus_Update(ServicesStatus Status, string OAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SPARES_OA_MAST] SET  SPOA_STATUS='{0}' WHERE SPOA_ID='{1}'", Status, OAId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Spares Order Acceptance Details", "107");
                }
                return _returnStringMessage;
            }

            public void SparesOrderAcceptanceDetails_Select(string OrderAcceptanceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SPARES_OA_DET] WHERE [YANTRA_SPARES_OA_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_SPARES_OA_DET].SPOA_ID=" + OrderAcceptanceId + "");
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
                    dr["Quantity"] = dbManager.DataReader["SPOA_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SPOA_RATE"].ToString();
                    dr["Specifications"] = dbManager.DataReader["SPOA_DET_SPEC"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SPOA_DET_REMARKS"].ToString();
                    dr["Priority"] = dbManager.DataReader["SPOA_DET_PRIORITY"].ToString();

                    OrderAcceptanceProducts.Rows.Add(dr);
                }

                gv.DataSource = OrderAcceptanceProducts;
                gv.DataBind();
            }

            public string SparesOrderAcceptanceApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  SPOA_STATUS FROM [YANTRA_SPARES_OA_MAST] WHERE SPOA_ID='{0}'", this.SOAId);
                if (dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Closed.ToString() || dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString() == ServicesStatus.Cancelled.ToString())
                {
                    _returnIntValue = 1;
                }
                else
                {
                    _commandText = string.Format("UPDATE [YANTRA_SPARES_OA_MAST] SET SPOA_APPROVED_BY={0},SPOA_STATUS='{1}' WHERE SPOA_ID='{2}'", this.SOAApprovedBy, ServicesStatus.Closed, this.SOAId);
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
                    log.add_Update("Spares Order Acceptance Approve Details", "107");

                }
                return _returnStringMessage;
            }

            public static void SparesOrderAcceptance_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SPARES_OA_MAST] ORDER BY SPOA_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPOA_NO", "SPOA_ID");
                }
            }
            public static void OrderAcceptance_Select(Control ControlForBind, string EmployeeId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_SPARES_OA_MAST].* 	FROM [YANTRA_COMPLAINT_REGISTER] inner join [YANTRA_SERVICES_ASSIGN_TASKS] on YANTRA_SERVICES_ASSIGN_TASKS.CR_ID = YANTRA_COMPLAINT_REGISTER.CR_ID inner join [YANTRA_EMPLOYEE_MAST] on [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_SERVICES_ASSIGN_TASKS].EMP_ID inner join [YANTRA_SPARES_QUOT_MAST] on YANTRA_COMPLAINT_REGISTER].CR_ID=YANTRA_SPARES_QUOT_MAST].CR_ID	 inner join [YANTRA_SPARES_ORDER_MAST] on [YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID inner join [YANTRA_SPARES_OP_MAST] on [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_SPARES_OP_MAST].SPO_ID inner join [YANTRA_SPARES_OA_MAST] on [YANTRA_SPARES_OA_MAST].SPOP_ID=[YANTRA_SPARES_ORDER_MAST].SPOP_ID 	where [YANTRA_SERVICES_ASSIGN_TASKS].EMP_ID=" + EmployeeId + " ORDER BY [YANTRA_SPARES_OA_MAST].SPOA_ID");




                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SPOA_NO", "SPOA_ID");
                }
            }

            public int SparesOrderAcceptance_isrecordexists(string p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_SPARES_OA_MAST where SPOP_ID= " + p + "  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue; 
            }
        }

        //Methods for Services Assignments
        public class ServicesAssignments
        {
            public string CrId, CrNo, CrDate, EmpId, AssignTaskId, AssignTaskNo, AssingDate, DueDate, AssignRemarks, AssignStatus, CustId, ServiceAssignFollowUpDet_Id, FollowUpEmpId, FollowUpDate, FollowUpDesc,Cp_Id;

            public ServicesAssignments()
            {
            }

            public static string ServicesAssignments_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(SERVICE_ASSIGN_TASK_NO,LEFT(SERVICE_ASSIGN_TASK_NO,5),''))),0)+1 FROM [YANTRA_SERVICES_ASSIGN_TASKS]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ASSIGN_TASK_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_ENQ_ASSIGN_TASKS]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SERVICES_ASSIGN_TASKS", "SERVICE_ASSIGN_TASK_NO");
            }

            public string ServicesAssignments_Save()
            {
                this.AssignTaskNo = ServicesAssignments_AutoGenCode();
                this.AssignTaskId = AutoGenMaxId("[YANTRA_SERVICES_ASSIGN_TASKS]", "SERVICE_ASSIGN_TASK_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_SERVICES_ASSIGN_TASKS] VALUES({0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8})", this.AssignTaskId, this.AssignTaskNo, this.CrId, this.EmpId, this.AssingDate, this.DueDate, this.AssignRemarks, this.AssignStatus,this.Cp_Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Service Assignments Details", "109");

                }
                return _returnStringMessage;
            }

            public string ServicesAssignments_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SERVICES_ASSIGN_TASKS] SET  EMP_ID={0},SERVICE_ASSIGN_DATE='{1}',DUE_DATE='{2}',REMARKS='{3}',CP_ID={5} WHERE CR_ID={4}", this.EmpId, this.AssingDate, this.DueDate, this.AssignRemarks, this.CrId,this.Cp_Id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Service Assignments Details", "109");

                }
                return _returnStringMessage;
            }

            public string ServicesAssignments_Delete(string AssignTaskId)
            {
                if (DeleteRecord("[YANTRA_SERVICES_ASSIGN_TASKS]", "SERVICE_ASSIGN_TASK_ID", AssignTaskId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Service Assignments Details", "109");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static string ServicesAssignmentsStatus_Update(ServicesStatus Status, string AssignTaskId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SERVICES_ASSIGN_TASKS] SET  SERVICE_ASSIGN_STATUS='{0}' WHERE SERVICE_ASSIGN_TASK_ID='{1}'", Status, AssignTaskId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Service Assignments Status Details", "109");

                }
                return _returnStringMessage;
            }
            public static string ServicesAssignmentsStatus_Update1(ServicesStatus Status, string AssignTaskId)
            {
                if (dbManager.Transaction == null)
                   // dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SERVICES_ASSIGN_TASKS] SET  SERVICE_ASSIGN_STATUS='{0}' WHERE SERVICE_ASSIGN_TASK_ID='{1}'", Status, AssignTaskId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Service Assignments Status Details", "109");

                }
                return _returnStringMessage;
            }

            public string ServicesAssignmentsFollowUp_Save()
            {
                this.ServiceAssignFollowUpDet_Id = AutoGenMaxId("[YANTRA_SERVICE_ASSIGN_FOLLOWUP_DET]", "SERVICE_ASSIGN_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_SERVICE_ASSIGN_FOLLOWUP_DET] VALUES({0},{1},{2},'{3}','{4}')", this.ServiceAssignFollowUpDet_Id, this.AssignTaskId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Service Assignments Follow Up Details", "109");
                }
                return _returnStringMessage;
            }

            public int ServicesAssignments_Select(string CrId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_COMPLAINT_REGISTER],[YANTRA_SERVICES_ASSIGN_TASKS],[Service_Customer_Information] WHERE [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SERVICES_ASSIGN_TASKS].CR_ID AND [Service_Customer_Information].Cust_Code= [YANTRA_COMPLAINT_REGISTER].CUST_ID AND " +
                                            " [YANTRA_COMPLAINT_REGISTER].CR_ID ='" + CrId + "' ORDER BY [YANTRA_SERVICES_ASSIGN_TASKS].SERVICE_ASSIGN_TASK_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AssignTaskId = dbManager.DataReader["SERVICE_ASSIGN_TASK_ID"].ToString();
                    this.AssignTaskNo = dbManager.DataReader["SERVICE_ASSIGN_TASK_NO"].ToString();
                    this.CrId = dbManager.DataReader["CR_ID"].ToString();
                    this.CrNo = dbManager.DataReader["CR_NO"].ToString();
                    this.CrDate = Convert.ToDateTime(dbManager.DataReader["CR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.EmpId = dbManager.DataReader["EMP_ID"].ToString();
                    this.AssingDate = Convert.ToDateTime(dbManager.DataReader["SERVICE_ASSIGN_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.DueDate = Convert.ToDateTime(dbManager.DataReader["DUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AssignRemarks = dbManager.DataReader["REMARKS"].ToString();
                    this.AssignStatus = dbManager.DataReader["SERVICE_ASSIGN_STATUS"].ToString();
                    this.CustId = dbManager.DataReader["Cust_Name"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }



            public static void ServicesAssignments_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SERVICES_ASSIGN_TASKS] ORDER BY SERVICE_ASSIGN_TASK_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SERVICE_ASSIGN_TASK_NO", "SERVICE_ASSIGN_TASK_ID");
                }
            }
        }

        //Methods For AMC Invoice Form
        public class AmcInvoice
        {
            public string AMCIId, AMCINo, AMCIDate, AMCIType, DCId, DespmId, AMCOId, AMCIMissChrgs, AMCIDiscount, AMCIGrossAmt, AMCIRemarks, AMCIPreparedBy, AMCIApprovedBy;
            public string AMCIDetId, AMCIDetQty, AMCIDetRate, AMCIDetVat, AMCIDetCst, ItemCode, ItemName, UOM, AMCIDetExcise;
            public string CustId, CustCode, RegId, RegName, CustName, CompName, ContactPerson, Phone, Mobile, IndTypeId, IndType, Fax, Email, PANNo, ECCNo, CSTNo, LocalSTNo, SplInsrs, Address, Website, CorpContactPerson, CorpPhone, CorpMobile, CorpEmail, CorpAddress, DesgId, CorpDesgId, CorpFax, IsNewOrExisting;   //Customer Master
            public string CustDetId, CustCorpContactPerson, CustCorpPhone, CustCorpMobile, CustCorpEmail, CustCorpAddress, CustCorpDesgId, CustCorpFax;
            public string CustUnitId, CustUnitName, CustUnitAddress, UnitNo;
            public string  AMCONo, AMCODate, AMCQTId, AMCQTNo, AMCORespId, AMCOSalespId, AMCOPreparedBy, AMCOCheckedBy, AMCOApprovedBy, AMCOAcceptanceFlag, AMCODelivery, AMCOCurrencyTypeId, AMCOPackageCharges, AMCOPaymentTerms, AMCOCSTax, AMCOExciseDuty, AMCOGuarantee,  AMCOInsurance, AMCOTransportCharges, AMCOJurisdiction, AMCOErection, AMCOInspection, AMCOValidity, AMCOOtherSpec, CrId, AssignTaskId, AMCOTillDate, EnqId, AMCOCustPONo, AMCOCustPODate, AMCOConsignee, AMCOResponsiblePerson, AMCOResponsiblePersonEmail, AMCOPMCalls, AMCOBDCalls;
            public string AMCODetId, AMCOItemCode, AMCODetQty, AMCORate, AMCODetSpec, AMCODetRemarks, AMCODetPriority;
            public string AMCOUploadId, AMCOUploadFileName, AMCOUploadDate, AMCOFileContentType;
            public AmcInvoice()
            {
            }

            public static string AmcInvoice_AutoGenCode()
            {

                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SI/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(AMCI_NO,LEFT(AMCI_NO,5),''))),0)+1 FROM [YANTRA_AMC_INVOICE_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_AMC_INVOICE_MAST", "AMCI_NO");


            }

            public string AmcInvoice_Save()
            {
                this.AMCINo = AmcInvoice_AutoGenCode();
                this.AMCIId = AutoGenMaxId("[YANTRA_AMC_INVOICE_MAST]", "AMCI_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_INVOICE_MAST] SELECT ISNULL(MAX(AMCI_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' FROM [YANTRA_AMC_INVOICE_MAST]", this.AMCINo, Convert.ToDateTime(this.AMCIDate), this.AMCIType, this.DCId, this.DespmId, this.AMCOId, this.AMCIMissChrgs, this.AMCIDiscount, this.AMCIGrossAmt, this.AMCIRemarks, this.AMCIPreparedBy, this.AMCIApprovedBy);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Invoice Details", "110");

                }
                return _returnStringMessage;
            }

            public string AmcInvoice_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_INVOICE_MAST] SET AMCI_DATE='{0}',AMCI_TYPE='{1}',AMCI_MISS_CHRGS='{2}',AMCI_DISCOUNT='{3}',AMCI_GROSS_AMT='{4}',AMCI_REMARKS='{5}',AMCI_PREPARED_BY='{6}',AMCI_APPROVED_BY='{7}' WHERE AMCI_ID={8}", this.AMCIDate, this.AMCIType, this.AMCIMissChrgs, this.AMCIDiscount, this.AMCIGrossAmt, this.AMCIRemarks, this.AMCIPreparedBy, this.AMCIApprovedBy, this.AMCIId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Invoice Details", "110");

                }
                return _returnStringMessage;
            }

            public string AmcInvoice_Delete(string AmcInvoiceId)
            {

                if (DeleteRecord("[YANTRA_AMC_INVOICE_DET]", "AMCI_ID", AmcInvoiceId) == true)
                {
                    if (DeleteRecord("[YANTRA_AMC_INVOICE_MAST]", "AMCI_ID", AmcInvoiceId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("AMC Invoice Details", "110");

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

            public static void AmcInvoice_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_INVOICE_MAST] ORDER BY AMCI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AMCI_NO", "AMCI_ID");
                }
            }
            public string amcpayterms;
            public int AmcInvoice_Select(string AmcInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_INVOICE_MAST],[YANTRA_AMC_ORDER_MAST] WHERE [YANTRA_AMC_ORDER_MAST].AMCO_ID= [YANTRA_AMC_INVOICE_MAST] .AMCO_ID " +
                                            " AND [YANTRA_AMC_INVOICE_MAST].AMCI_ID='" + AmcInvoiceId + "' ORDER BY [YANTRA_AMC_INVOICE_MAST].AMCI_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCIId = dbManager.DataReader["AMCI_ID"].ToString();
                    this.AMCINo = dbManager.DataReader["AMCI_NO"].ToString();
                    this.AMCIDate = Convert.ToDateTime(dbManager.DataReader["AMCI_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCIType = dbManager.DataReader["AMCI_TYPE"].ToString();
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCIMissChrgs = dbManager.DataReader["AMCI_MISS_CHRGS"].ToString();
                    this.AMCIDiscount = dbManager.DataReader["AMCI_DISCOUNT"].ToString();
                    this.AMCIGrossAmt = dbManager.DataReader["AMCI_GROSS_AMT"].ToString();
                    this.AMCIRemarks = dbManager.DataReader["AMCI_REMARKS"].ToString();
                    this.AMCIPreparedBy = dbManager.DataReader["AMCI_PREPARED_BY"].ToString();
                    this.AMCIApprovedBy = dbManager.DataReader["AMCI_APPROVED_BY"].ToString();
                    this.amcpayterms = dbManager.DataReader["AMCO_PAY_TERM"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
            public int AmcInvoice_Select1(string AmcInvoiceId,String AmcOrderID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_AMC_ORDER_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_AMC_INVOICE_MAST],[YANTRA_AMC_ORDER_MAST],[YANTRA_LKUP_UOM],[YANTRA_COMPLAINT_REGISTER],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]WHERE  [YANTRA_AMC_QUOTATION_MAST].CR_ID=[YANTRA_COMPLAINT_REGISTER].CR_ID AND [YANTRA_AMC_ORDER_MAST].AMCQT_ID=[YANTRA_AMC_QUOTATION_MAST].AMCQT_ID AND [YANTRA_COMPLAINT_REGISTER].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID and [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID and YANTRA_AMC_ORDER_DET.ITEM_CODE=YANTRA_ITEM_MAST.ITEM_CODE AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and [YANTRA_AMC_ORDER_MAST].AMCO_ID= [YANTRA_AMC_INVOICE_MAST].AMCO_ID and [YANTRA_AMC_ORDER_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND [YANTRA_AMC_ORDER_DET].AMCO_ID="+AmcOrderID+" AND [YANTRA_AMC_INVOICE_MAST].AMCI_ID="+AmcInvoiceId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCONo = dbManager.DataReader["AMCO_NO"].ToString();
                    this.AMCODate = Convert.ToDateTime(dbManager.DataReader["AMCO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    this.AMCORespId = dbManager.DataReader["AMCO_RESP_ID"].ToString();
                    this.AMCOSalespId = dbManager.DataReader["AMCO_SALESP_ID"].ToString();
                    this.AMCOPreparedBy = dbManager.DataReader["AMCO_PREPARED_BY"].ToString();
                    this.AMCOCheckedBy = dbManager.DataReader["AMCO_CHECKED_BY"].ToString();
                    this.AMCOApprovedBy = dbManager.DataReader["AMCO_APPROVED_BY"].ToString();
                    this.AMCOAcceptanceFlag = dbManager.DataReader["AMCO_ACCEPTANCE_FLAG"].ToString();
                    this.AMCODelivery = dbManager.DataReader["AMCO_DELIVERY"].ToString();
                    this.AMCOCurrencyTypeId = dbManager.DataReader["CURRENCY_ID"].ToString();
                    this.AMCOPaymentTerms = dbManager.DataReader["AMCO_PAY_TERM"].ToString();
                    this.AMCOPackageCharges = dbManager.DataReader["AMCO_PACK_CHARGES"].ToString();
                    this.AMCOExciseDuty = dbManager.DataReader["AMCO_EXCISE"].ToString();
                    this.AMCOCSTax = dbManager.DataReader["AMCO_CST"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.AMCOGuarantee = dbManager.DataReader["AMCO_GUARANTEE"].ToString();
                    this.AMCOTransportCharges = dbManager.DataReader["AMCO_TRANS_CHARGES"].ToString();
                    this.AMCOInsurance = dbManager.DataReader["AMCO_INSURANCE"].ToString();
                    this.AMCOErection = dbManager.DataReader["AMCO_EREC_COMM"].ToString();
                    this.AMCOJurisdiction = dbManager.DataReader["AMCO_JURISDICTION"].ToString();
                    this.AMCOValidity = dbManager.DataReader["AMCO_VALIDITY"].ToString();
                    this.AMCOInspection = dbManager.DataReader["AMCO_INSPECTION"].ToString();
                    this.AMCOOtherSpec = dbManager.DataReader["AMCO_OTHER_SPEC"].ToString();
                    this.AMCOTillDate = Convert.ToDateTime(dbManager.DataReader["AMCO_TILLDATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCOCustPONo = dbManager.DataReader["AMCO_CUST_PO"].ToString();
                    this.AMCOCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCO_CUST_PO_DATED"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCOConsignee = dbManager.DataReader["AMCO_CONSIGNEE"].ToString();
                    this.AMCOResponsiblePerson = dbManager.DataReader["AMCO_RESPONSIBLE_PERSON"].ToString();
                    this.AMCOResponsiblePersonEmail = dbManager.DataReader["AMCO_RESPONSIBLE_PERSON_EMAIL"].ToString();
                    this.AMCOPMCalls = dbManager.DataReader["AMCO_PM_CALLS"].ToString();
                    this.AMCOBDCalls = dbManager.DataReader["AMCO_BD_CALLS"].ToString();
                    this.Address = dbManager.DataReader["CUST_ADDRESS"].ToString();
                    this.CompName = dbManager.DataReader["CUST_COMPANY_NAME"].ToString();
                    this.ContactPerson = dbManager.DataReader["CUST_CONTACT_PERSON"].ToString();
                    this.CSTNo = dbManager.DataReader["CUST_CST"].ToString();
                    this.CustCode = dbManager.DataReader["CUST_CODE"].ToString();
                    this.CustName = dbManager.DataReader["CUST_NAME"].ToString();
                    this.ECCNo = dbManager.DataReader["CUST_ECC"].ToString();
                    this.Email = dbManager.DataReader["CUST_EMAIL"].ToString();
                    this.Fax = dbManager.DataReader["CUST_FAX"].ToString();
                    //this.IndTypeId = dbManager.DataReader["IND_TYPE_ID"].ToString();
                    //this.IndType = dbManager.DataReader["IND_TYPE"].ToString();
                    this.LocalSTNo = dbManager.DataReader["CUST_LOCAL_ST_NO"].ToString();
                    this.Mobile = dbManager.DataReader["CUST_MOBILE"].ToString();
                    this.PANNo = dbManager.DataReader["CUST_PAN"].ToString();
                    this.Phone = dbManager.DataReader["CUST_PHONE"].ToString();
                    this.RegId = dbManager.DataReader["REG_ID"].ToString();
                    //this.RegName = dbManager.DataReader["REG_NAME"].ToString();
                    this.SplInsrs = dbManager.DataReader["CUST_SPL_INSTRS"].ToString();
                    //this.Website = dbManager.DataReader["CUST_WEBSITE"].ToString();
                    this.CorpContactPerson = dbManager.DataReader["CUST_CORP_CONTACT_PERSON"].ToString();
                    this.CorpPhone = dbManager.DataReader["CUST_CORP_PHONE"].ToString();
                    this.CorpMobile = dbManager.DataReader["CUST_CORP_MOBILE"].ToString();
                    this.CorpEmail = dbManager.DataReader["CUST_CORP_EMAIL"].ToString();
                    this.CorpAddress = dbManager.DataReader["CUST_CORP_ADDRESS"].ToString();
                    this.DesgId = dbManager.DataReader["CUST_DESG_ID"].ToString();
                    this.CorpDesgId = dbManager.DataReader["CUST_CORP_DESG_ID"].ToString();
                    this.CorpFax = dbManager.DataReader["CUST_CORP_FAX"].ToString();
                    this.IsNewOrExisting = dbManager.DataReader["ISNEWOREXISTING"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
            public string AmcInvoiceDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_INVOICE_DET] SELECT ISNULL(MAX(AMCI_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_AMC_INVOICE_DET]", this.AMCIId, this.ItemCode, this.AMCIDetQty, this.AMCIDetRate, this.AMCIDetVat, this.AMCIDetCst, this.AMCIDetExcise);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Invoice Details", "110");

                }
                return _returnStringMessage;
            }

            public int AmcInvoiceDetails_Delete(string AmcInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_AMC_INVOICE_DET] WHERE AMCI_ID={0}", AmcInvoiceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }

            public void AmcInvoiceDetails_Select(string AmcInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_AMC_INVOICE_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_AMC_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_AMC_INVOICE_DET].AMCI_ID=" + AmcInvoiceId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("VAT");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Cst");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Excise");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemType");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["AMCI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["AMCI_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["AMCI_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["AMCI_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["AMCI_DET_EXCISE"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }

                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
            }

            public string AMCInvoiceApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_AMC_INVOICE_MAST] SET AMCI_APPROVED_BY={0}  WHERE AMCI_ID='{1}'", this.AMCIApprovedBy, this.AMCIId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("AMC Invoice Approve Details", "110");


                }
                return _returnStringMessage;
            }

            public static void AmcInvoiceForPayments_Select(Control ControlForBind, string AmcOrderId, string SaveButtonText)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_AMC_ORDER_MAST] WHERE [YANTRA_AMC_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_AMC_ORDER_MAST].AMCO_ID=" + AmcOrderId + " AND [YANTRA_AMC_INVOICE_MAST].AMCI_ID IN (SELECT AMCI_ID FROM YANTRA_AMC_PAYMENT_RECEIVED WHEREAMCPR_PAYMENT_STATUS <> 'Cleared') ORDER BY [YANTRA_AMC_INVOICE_MAST].AMCI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["AMCI_NO"].ToString(), dbManager.DataReader["AMCI_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_AMC_ORDER_MAST] WHERE [YANTRA_AMC_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID  AND [YANTRA_AMC_ORDER_MAST].AMCO_ID=" + AmcOrderId + " AND [YANTRA_AMC_INVOICE_MAST].AMCI_ID NOT IN (SELECT AMCI_ID FROM YANTRA_AMC_PAYMENT_RECEIVEDD) ORDER BY [YANTRA_AMC_INVOICE_MAST].AMCI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["AMCI_NO"].ToString(), dbManager.DataReader["AMCI_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_AMC_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_AMC_ORDER_MAST] WHERE [YANTRA_AMC_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID  AND [YANTRA_AMC_ORDER_MAST].AMCO_ID=" + AmcOrderId + " ORDER BY [YANTRA_AMC_INVOICE_MAST].AMCI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "AMCI_NO", "AMCI_ID");
                    }
                }
            }



        }

        //Methods For AMC Payments Received Form
        public class AMCPaymentsReceived
        {
            public string AMCPRId, AMCPRNo, AMCPRDate, CustId, UnitId, AMCOId, AMCIId, AMCIAmt, AMCPRReceivedAmt, AMCServiceType, AMCPREquipmentModel, AMCPRSLNo, AMCPRPaymentTerms, AMCPRPaymodeType, AMCPRChequeNo, AMCPRChequeDate, AMCPRCahReceivedDate, AMCPRBankDetails, AMCPRPreparedBy, AMCPRApprovedBy, AMCPRPaymentStatus;


            public AMCPaymentsReceived()
            {
            }

            public static string AMCPaymentsReceived_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(AMCPR_NO,LEFT(AMCPR_NO,5),''))),0)+1 FROM [YANTRA_AMC_PAYMENT_RECEIVED]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_AMC_PAYMENT_RECEIVED", "AMCPR_NO");
            }

            public string AMCPaymentsReceived_Save()
            {
                this.AMCPRNo = AMCPaymentsReceived_AutoGenCode();
                this.AMCPRId = AutoGenMaxId("[YANTRA_AMC_PAYMENT_RECEIVED]", "AMCPR_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_AMC_PAYMENT_RECEIVED] SELECT ISNULL(MAX(AMCPR_ID),0)+1,'{0}','{1}',{2},{3},{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}' FROM [YANTRA_AMC_PAYMENT_RECEIVED]", this.AMCPRNo, this.AMCPRDate, this.CustId, this.UnitId, this.AMCOId, this.AMCIId, this.AMCIAmt, this.AMCPRReceivedAmt, this.AMCServiceType, this.AMCPREquipmentModel, this.AMCPRSLNo, this.AMCPRPaymentTerms, this.AMCPRPaymodeType, this.AMCPRChequeNo, this.AMCPRChequeDate, this.AMCPRCahReceivedDate, this.AMCPRBankDetails, this.AMCPRPreparedBy, this.AMCPRApprovedBy, this.AMCPRPaymentStatus);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Payyments Details", "111");

                }
                return _returnStringMessage;
            }

            public string AMCPaymentsReceived_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_PAYMENT_RECEIVED] SET AMCPR_NO='{0}',AMCPR_DATE='{1}',CUST_ID={2},CUST_UNIT_ID={3},AMCO_ID={4},AMCI_ID={5},AMCI_AMOUNT='{6}',AMCPR_AMT_RECEIVED='{7}',AMCPR_SERVICE_TYPE='{8}',AMCPR_EQUIPMENT_MODEL='{9}',AMCPR_SERIAL_NO='{10}',AMCPR_PAYMENT_TERMS='{11}',AMCPR_PAYMODE_TYPE='{12}',AMCPR_CHEQUE_NO='{13}',AMCPR_CHEQUE_DATE='{14}',AMCPR_CASH_RECEIVED_DATE='{15}',AMCPR_BANK_DETAILS='{16}',AMCPR_PREPARED_BY='{17}',AMCPR_APPROVED_BY='{18}',AMCPR_PAYMENT_STATUS='{19}' WHERE AMCPR_ID={20}", this.AMCPRNo, Convert.ToDateTime(this.AMCPRDate), this.CustId, this.UnitId, this.AMCOId, this.AMCIId, this.AMCIAmt, this.AMCPRReceivedAmt, this.AMCServiceType, this.AMCPREquipmentModel, this.AMCPRSLNo, this.AMCPRPaymentTerms, this.AMCPRPaymodeType, this.AMCPRChequeNo, Convert.ToDateTime(this.AMCPRChequeDate), Convert.ToDateTime(this.AMCPRCahReceivedDate), this.AMCPRBankDetails, this.AMCPRPreparedBy, this.AMCPRApprovedBy, this.AMCPRPaymentStatus, this.AMCPRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";

                    log.add_Update("AMC Payyments Details", "111");

                }
                return _returnStringMessage;
            }

            public string AMCPaymentsReceived_Delete(string AMCPaymentsReceivedId)
            {
                if (DeleteRecord("[YANTRA_AMC_PAYMENT_RECEIVED]", "AMCPR_ID", AMCPaymentsReceivedId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("AMC Payyments Details", "111");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static void AMCPaymentsReceived_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_PAYMENT_RECEIVED] ORDER BY AMCPR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "AMCPR_NO", "AMCPR_ID");
                }
            }

            public int AMCPaymentsReceived_Select(string AMCPaymentsReceivedId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_PAYMENT_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_INVOICE_MAST],[YANTRA_CUSTOMER_UNITS],[YANTRA_AMC_ORDER_MAST]" +
                                                                        " WHERE [YANTRA_AMC_PAYMENT_RECEIVED].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                                                        " AND [YANTRA_AMC_PAYMENT_RECEIVED].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
                                                                        " AND [YANTRA_AMC_PAYMENT_RECEIVED].AMCI_ID=[YANTRA_AMC_INVOICE_MAST].AMCI_ID  " +
                                                                          " AND [YANTRA_AMC_PAYMENT_RECEIVED].AMCO_ID=[YANTRA_AMC_ORDER_MAST].AMCO_ID  " +
                                                                        " AND [YANTRA_AMC_PAYMENT_RECEIVED].AMCPR_ID='" + AMCPaymentsReceivedId + "' ORDER BY [YANTRA_AMC_PAYMENT_RECEIVED].AMCPR_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCPRId = dbManager.DataReader["AMCPR_ID"].ToString();
                    this.AMCPRNo = dbManager.DataReader["AMCPR_NO"].ToString();
                    this.AMCPRDate = Convert.ToDateTime(dbManager.DataReader["AMCPR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.UnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCIId = dbManager.DataReader["AMCI_ID"].ToString();
                    this.AMCIAmt = dbManager.DataReader["AMCI_AMOUNT"].ToString();
                    this.AMCPRReceivedAmt = dbManager.DataReader["AMCPR_AMT_RECEIVED"].ToString();
                    this.AMCServiceType = dbManager.DataReader["AMCPR_SERVICE_TYPE"].ToString();
                    this.AMCPREquipmentModel = dbManager.DataReader["AMCPR_EQUIPMENT_MODEL"].ToString();
                    this.AMCPRSLNo = dbManager.DataReader["AMCPR_SERIAL_NO"].ToString();
                    this.AMCPRPaymentTerms = dbManager.DataReader["AMCPR_PAYMENT_TERMS"].ToString();
                    this.AMCPRPaymodeType = dbManager.DataReader["AMCPR_PAYMODE_TYPE"].ToString();
                    this.AMCPRChequeNo = dbManager.DataReader["AMCPR_CHEQUE_NO"].ToString();
                    this.AMCPRChequeDate = Convert.ToDateTime(dbManager.DataReader["AMCPR_CHEQUE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCPRCahReceivedDate = Convert.ToDateTime(dbManager.DataReader["AMCPR_CASH_RECEIVED_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.AMCPRBankDetails = dbManager.DataReader["AMCPR_BANK_DETAILS"].ToString();
                    this.AMCPRPreparedBy = dbManager.DataReader["AMCPR_PREPARED_BY"].ToString();
                    this.AMCPRApprovedBy = dbManager.DataReader["AMCPR_APPROVED_BY"].ToString();
                    this.AMCPRPaymentStatus = dbManager.DataReader["AMCPR_PAYMENT_STATUS"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }

            public void ExistingAMCPaymentsReceived_Select(GridView gv, string AMCInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_AMC_PAYMENT_RECEIVED],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_INVOICE_MAST],[YANTRA_CUSTOMER_UNITS]" +
                                                                       " WHERE [YANTRA_AMC_PAYMENT_RECEIVED].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                                                       " AND [YANTRA_AMC_PAYMENT_RECEIVED].CUST_UNIT_ID=[YANTRA_CUSTOMER_UNITS].CUST_UNIT_ID " +
                                                                       " AND [YANTRA_AMC_PAYMENT_RECEIVED].AMCI_ID=[YANTRA_AMC_INVOICE_MAST].AMCI_ID  " +
                                                                       " AND [YANTRA_AMC_PAYMENT_RECEIVED].AMCI_ID='" + AMCInvoiceId + "' ORDER BY [YANTRA_AMC_PAYMENT_RECEIVED].AMCPR_ID DESC ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                gv.DataSource = dbManager.DataReader;
                gv.DataBind();
                dbManager.DataReader.Close();

            }

            public string AMCPaymentsStatus_Update(string AMCPaymentStatus, string AMCIId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_AMC_PAYMENT_RECEIVED] SET AMCPR_PAYMENT_STATUS='{0}' WHERE AMCI_ID={1}", AMCPaymentStatus, AMCIId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Payyments Status Details", "111");

                }
                return _returnStringMessage;
            }

            public int AMCinvoice_isrecordexists(string p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select * from YANTRA_AMC_INVOICE_MAST where AMCI_ID= " + p + "  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue; 
            }
        }

        // Method for Warranty Claim  Form
        public class WarrantyClaim
        {
            public string WCId, WCNo, WCCurrFinYear, WCDate, WCInvoiceNo, WCInvoiceDate, WCInstDate, CustId, CustUnitId, CustDetId, ItemCode, CRCallType, WCItemsSlNo, WCQty, WCWarrantyExpOn, WCWarrantyClaimsOn, WCProbNature, WCSerEngRemarks, WCSMPLRemarks, WCRemarksNeeds, WCPreparedBy, WCApprovedBy, WCSupplierName, WCItemDesc, WCSupplierAddressForCommunication, WCAttentionTo, WCSupplierCompanyName;


            public WarrantyClaim()
            { }

            public static string WarrantyClaim_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "WO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(WC_NO,LEFT(WC_NO,5),''))),0)+1 FROM [YANTRA_WARRANTY_CLAIM]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(WO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_WO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_WARRANTY_CLAIM", "WC_NO");
            }

            public string WarrantyClaim_Save()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_WARRANTY_CLAIM]", "WC_NO", this.WCNo) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_WARRANTY_CLAIM] SELECT ISNULL(MAX(WC_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},{8},{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}' FROM [YANTRA_WARRANTY_CLAIM]", this.WCNo, this.WCCurrFinYear, this.WCDate, this.WCInvoiceNo, this.WCInvoiceDate, this.WCInstDate, this.CustId, this.CustUnitId, this.CustDetId, this.ItemCode, this.WCItemsSlNo, this.WCQty, this.WCWarrantyExpOn, this.WCWarrantyClaimsOn, this.WCProbNature, this.WCSerEngRemarks, this.WCSMPLRemarks, this.WCRemarksNeeds, this.WCPreparedBy, this.WCApprovedBy, this.WCSupplierName, this.WCItemDesc, this.WCSupplierCompanyName, this.WCSupplierAddressForCommunication, this.WCAttentionTo);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Warranty Claim Details", "112");

                    }
                }
                else
                {
                    _returnStringMessage = "Warranty Claim  Already Exists.";
                }
                return _returnStringMessage;
            }

            public string WarrantyClaim_Update()
            {
                dbManager.Open();
                if (IsRecordExists("[YANTRA_WARRANTY_CLAIM]", "WC_NO", this.WCNo, "WC_ID", this.WCId) == false)
                {
                    _commandText = string.Format("UPDATE [YANTRA_WARRANTY_CLAIM] SET WC_CURR_FIN_YEAR='{0}',WC_DATE='{1}',WC_INVOICE_NO='{2}',WC_INVOICE_DATE='{3}',WC_DATE_OF_INSTAL='{4}',CUST_ID={5},CUST_UNIT_ID={6},CUST_DET_ID={7},ITEM_CODE={8},WC_ITEMS_SL_NO='{9}',WC_QTY='{10}',WC_WARRANTY_EXPIRES_ON='{11}',WC_WARRANTY_CLAIMS_ON='{12}',WC_PROB_NATURE='{13}',WC_SER_ENG_REMARKS='{14}',WC_SMPL_REMARKS='{15}',WC_REMARKS_NEEDS='{16}',WC_PREPARED_BY={17},WC_APPROVED_BY={18},WC_SUPPLIER_NAME='{19}',WC_ITEM_DESCRIPTION='{20}',WC_SUPPLIER_ADDRESS_COMMUNICATION='{21}',WC_ATTENTION_TO='{22}',WC_SUPPLIER_COMPANY_NAME='{23}' WHERE WC_ID={24}", this.WCCurrFinYear, this.WCDate, this.WCInvoiceNo, this.WCInvoiceDate, this.WCInstDate, this.CustId, this.CustUnitId, this.CustDetId, this.ItemCode, this.WCItemsSlNo, this.WCQty, this.WCWarrantyExpOn, this.WCWarrantyClaimsOn, this.WCProbNature, this.WCSerEngRemarks, this.WCSMPLRemarks, this.WCRemarksNeeds, this.WCPreparedBy, this.WCApprovedBy, this.WCSupplierName, this.WCItemDesc, this.WCSupplierAddressForCommunication, this.WCAttentionTo, this.WCSupplierCompanyName, this.WCId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Warranty Claim Details", "112");


                    }
                }
                else
                {
                    _returnStringMessage = "Warranty Claim Already Exists.";
                }
                return _returnStringMessage;
            }

            public string WarrantyClaim_Delete()
            {
                if (DeleteRecord("[YANTRA_WARRANTY_CLAIM]", "WC_ID", this.WCId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Warranty Claim Details", "112");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public static void WarrantyClaim_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_WARRANTY_CLAIM] ORDER BY WC_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "WC_NO", "WC_ID");
                }
            }

            public int WarrantyClaim_Select(string WCId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_WARRANTY_CLAIM] WHERE WC_ID = " + WCId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.WCId = dbManager.DataReader["WC_ID"].ToString();
                    this.WCNo = dbManager.DataReader["WC_NO"].ToString();
                    this.WCCurrFinYear = dbManager.DataReader["WC_CURR_FIN_YEAR"].ToString();
                    this.WCDate = Convert.ToDateTime(dbManager.DataReader["WC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.WCInvoiceNo = dbManager.DataReader["WC_INVOICE_NO"].ToString();
                    this.WCInvoiceDate = Convert.ToDateTime(dbManager.DataReader["WC_INVOICE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.WCInstDate = Convert.ToDateTime(dbManager.DataReader["WC_DATE_OF_INSTAL"].ToString()).ToString("dd/MM/yyyy");
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.WCItemsSlNo = dbManager.DataReader["WC_ITEMS_SL_NO"].ToString();
                    this.WCQty = dbManager.DataReader["WC_QTY"].ToString();
                    this.WCWarrantyExpOn = Convert.ToDateTime(dbManager.DataReader["WC_WARRANTY_EXPIRES_ON"].ToString()).ToString("dd/MM/yyyy");
                    this.WCWarrantyClaimsOn = Convert.ToDateTime(dbManager.DataReader["WC_WARRANTY_CLAIMS_ON"].ToString()).ToString("dd/MM/yyyy");
                    this.WCProbNature = dbManager.DataReader["WC_PROB_NATURE"].ToString();
                    this.WCSerEngRemarks = dbManager.DataReader["WC_SER_ENG_REMARKS"].ToString();
                    this.WCSMPLRemarks = dbManager.DataReader["WC_SMPL_REMARKS"].ToString();
                    this.WCRemarksNeeds = dbManager.DataReader["WC_REMARKS_NEEDS"].ToString();
                    this.WCPreparedBy = dbManager.DataReader["WC_PREPARED_BY"].ToString();
                    this.WCApprovedBy = dbManager.DataReader["WC_APPROVED_BY"].ToString();
                    this.WCSupplierName = dbManager.DataReader["WC_SUPPLIER_NAME"].ToString();
                    this.WCItemDesc = dbManager.DataReader["WC_ITEM_DESCRIPTION"].ToString();
                    this.WCSupplierAddressForCommunication = dbManager.DataReader["WC_SUPPLIER_ADDRESS_COMMUNICATION"].ToString();
                    this.WCAttentionTo = dbManager.DataReader["WC_ATTENTION_TO"].ToString();
                    this.WCSupplierCompanyName = dbManager.DataReader["WC_SUPPLIER_COMPANY_NAME"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }


        }

        // Method for Service Report  Form
        public class ServiceReport
        {
            public string SRId, SRNo, SRCurrFinYear, SRDate, CRId, CRCallType, CustId, CustUnitId, CustDetId, SRServiceCenter, SOId, AMCOId, SPOId, SRAMCRefNo, SRAMCVisitDate, SRCompletionDate, SRDescription, SRActionTaken, SRFurtherActionReq, SRCustFeedback, SRIsDocSubmited, SRServiceCompleted, ItemCode, ItemName,SRItemSLNo, SRStatus, EmpId, SRServiceType, SRFiles, SRPreparedBy, SRApprovedBy, SRAMCAId, SRInsAId,Cp_Id;
            public string SRDET_DET_ID, Status,DOC,SRId2;

            public ServiceReport()
            { }
            public string ServiceReportItemDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_SERVICE_REPORT_DET]", "SRDET_DET_ID", this.SRDET_DET_ID) == false)
                {
                    _commandText = string.Format("INSERT INTO [YANTRA_SERVICE_REPORT_DET] SELECT ISNULL(MAX(SRDET_DET_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}' FROM [YANTRA_SERVICE_REPORT_DET]", this.SRNo, this.ItemCode,this.ItemName ,this.SRDescription, this.SRActionTaken, this.SRFurtherActionReq, this.Status,this.DOC,this.SRCustFeedback);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Service Reports Details", "114");

                    }
                }
                else
                {
                }
                return _returnStringMessage;
            }
            public string ServiceReportItemDetails_Update()
            {
               // if (dbManager.Transaction == null)
                    //dbManager.Open();
                                //{
                _commandText = string.Format("update [YANTRA_SERVICE_REPORT_DET]  SET SRDET_SR_NO='{1}',SRDET_ITEM_TYPE='{2}',SRDET_ITEM_NAME='{3}',SRDET_DESCRIPTION='{4}',SRDET_ACTION_TAKEN='{5}',SRDET_FOLLOWUP='{6}',SRDET_STATUS='{7}',Date_Of_Comp='{8}',CustomerFeedBack='{9}' WHERE SRDET_DET_ID='{0}'",this.SRDET_DET_ID ,this.SRNo, this.ItemCode, this.ItemName, this.SRDescription, this.SRActionTaken, this.SRFurtherActionReq, this.Status,this.DOC,this.SRCustFeedback);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Update("Service Reports Details", "114");

                    }
               // }
                else
                {
                }
                return _returnStringMessage;
            }
            public string ServiceReportDetails_Delete(string ServiceReportId)
            {
                if (DeleteRecord("[YANTRA_SERVICE_REPORT_DET]", "SRDET_SR_NO", ServiceReportId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";

                    log.add_Delete("Service Reports Details", "114");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public static string ServiceReport_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "WO/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SR_NO,LEFT(SR_NO,5),''))),0)+1 FROM [YANTRA_SERVICE_REPORT_MAST]").ToString());
                ////_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(WO_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_AMC_WO_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SERVICE_REPORT_MAST", "SR_NO");
            }

            public string ServiceReport_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_SERVICE_REPORT_MAST]", "SR_NO", this.SRNo, "CP_ID", this.Cp_Id) == false)
                {
                    CustDetId = "0";
                    _commandText = string.Format("INSERT INTO [YANTRA_SERVICE_REPORT_MAST] SELECT ISNULL(MAX(SR_ID),0)+1,'{0}','{1}','{2}',{3},'{4}',{5},{6},{7},'{8}',{9},{10},{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',{21},'{22}','{23}',{24},'{25}','{26}',{27},{28},{29},{30},{31} FROM [YANTRA_SERVICE_REPORT_MAST]", this.SRNo, this.SRCurrFinYear, this.SRDate, this.CRId, this.CRCallType, this.CustId, this.CustUnitId, this.CustDetId, this.SRServiceCenter, this.SOId, this.AMCOId, this.SPOId, this.SRAMCRefNo, this.SRAMCVisitDate, this.SRCompletionDate, this.SRDescription, this.SRActionTaken, this.SRFurtherActionReq, this.SRCustFeedback, this.SRIsDocSubmited, this.SRServiceCompleted, this.ItemCode, this.SRItemSLNo, this.SRStatus, this.EmpId, this.SRServiceType, this.SRFiles, this.SRPreparedBy, this.SRApprovedBy, this.SRAMCAId, this.SRInsAId,this.Cp_Id );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Service Reports Details", "114");

                    }
                }
                else
                {
                }
                return _returnStringMessage;
            }


            public string ServiceReport_Update1()
            {
                _commandText = string.Format("UPDATE [YANTRA_SERVICES_ASSIGN_TASKS] SET SERVICE_ASSIGN_STATUS='{0}' WHERE CR_ID='{1}'",this.SRStatus ,this.CRId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Service Reports Details", "114");

                    }
                return _returnStringMessage;
            }
            public string ServiceReport_Update2()
            {
                _commandText = string.Format("UPDATE [YANTRA_COMPLAINT_REGISTER] SET CR_STATUS='{0}' WHERE CR_ID='{1}'", this.SRStatus, this.CRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";

                    log.add_Update("Service Reports Details", "114");

                }


                return _returnStringMessage;
            }
            public string ServiceReport_Update()
            {
               // dbManager.Open();
                if (IsRecordExists("[YANTRA_SERVICE_REPORT_MAST]", "SR_NO", this.SRNo, "SR_ID", this.SRId) == false)
                {
                    CustDetId = "0";
                    _commandText = string.Format("UPDATE [YANTRA_SERVICE_REPORT_MAST] SET SR_CURR_FIN_YEAR='{0}',SR_DATE='{1}',CR_ID={2},CR_CALL_TYPE='{3}',CUST_ID={4},CUST_UNIT_ID={5},CUST_DET_ID={6},SR_SERVICE_CENTER='{7}',SO_ID={8},AMCO_ID={9},SPO_ID={10},SR_AMC_REF_NO='{11}',SR_AMC_VISIT_DATE='{12}',SR_COMPLETION_DATE='{13}',SR_DESCRIPTION='{14}',SR_ACTION_TAKEN='{15}',SR_FURTHER_ACTION_REQ='{16}',SR_CUST_FEEDBACK='{17}',SR_IS_DOC_SUBMITED='{18}',SR_SERVICE_COMPLETED='{19}',ITEM_CODE={20},SR_ITEM_SL_NO='{21}',SR_STATUS='{22}',EMP_ID={23},SR_SERVICE_TYPE='{24}',SR_FILES='{25}',SR_PREPARED_BY={26},SR_APPROVED_BY={27},SR_AMCA_ID={28},SR_INSA_ID={29},CP_ID={31} WHERE SR_ID={30}", this.SRCurrFinYear, this.SRDate, this.CRId, this.CRCallType, this.CustId, this.CustUnitId, this.CustDetId, this.SRServiceCenter, this.SOId, this.AMCOId, this.SPOId, this.SRAMCRefNo, this.SRAMCVisitDate, this.SRCompletionDate, this.SRDescription, this.SRActionTaken, this.SRFurtherActionReq, this.SRCustFeedback, this.SRIsDocSubmited, this.SRServiceCompleted, this.ItemCode, this.SRItemSLNo, this.SRStatus, this.EmpId, this.SRServiceType, this.SRFiles, this.SRPreparedBy, this.SRApprovedBy, this.SRAMCAId, this.SRInsAId, this.SRId,this.Cp_Id );
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("Service Reports Details", "114");

                    }
                }
                else
                {
                }
                return _returnStringMessage;
            }

            public string ServiceReport_Delete(string ServiceReportId)
            {
                if (DeleteRecord("[YANTRA_SERVICE_REPORT_MAST]", "SR_ID", ServiceReportId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Service Reports Details", "114");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }

            public static void ServiceReport_Select(Control ControlForBind)
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SERVICE_REPORT_MAST] ORDER BY SR_NO");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SR_NO", "SR_ID");
                }
            }

            public static void ServiceReportForPreviousRecords_Select(Control ControlForBind, string ServiceType, string OrderId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (ServiceType == "Installation" || ServiceType == "Warranty")
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_SERVICE_REPORT_MAST] WHERE SO_ID=" + OrderId + "ORDER BY SR_AMC_VISIT_DATE");
                }
                else if (ServiceType == "AMC")
                {
                    _commandText = string.Format("SELECT * FROM [YANTRA_SERVICE_REPORT_MAST] WHERE AMCO_ID=" + OrderId + "ORDER BY SR_AMC_VISIT_DATE");
                }
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
            }

            public int ServiceReport_Select(string SRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_SERVICE_REPORT_MAST] WHERE SR_ID = " + SRId);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.SRId = dbManager.DataReader["SR_ID"].ToString();
                    this.SRNo = dbManager.DataReader["SR_NO"].ToString();
                    this.SRCurrFinYear = dbManager.DataReader["SR_CURR_FIN_YEAR"].ToString();
                    this.SRDate = Convert.ToDateTime(dbManager.DataReader["SR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    this.CRCallType = dbManager.DataReader["CR_CALL_TYPE"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    this.SRServiceCenter = dbManager.DataReader["SR_SERVICE_CENTER"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SRAMCRefNo = dbManager.DataReader["SR_AMC_REF_NO"].ToString();
                    this.SRAMCVisitDate = Convert.ToDateTime(dbManager.DataReader["SR_AMC_VISIT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SRCompletionDate = Convert.ToDateTime(dbManager.DataReader["SR_COMPLETION_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SRDescription = dbManager.DataReader["SR_DESCRIPTION"].ToString();
                    this.SRActionTaken = dbManager.DataReader["SR_ACTION_TAKEN"].ToString();
                    this.SRFurtherActionReq = dbManager.DataReader["SR_FURTHER_ACTION_REQ"].ToString();
                    this.SRCustFeedback = dbManager.DataReader["SR_CUST_FEEDBACK"].ToString();
                    this.SRIsDocSubmited = dbManager.DataReader["SR_IS_DOC_SUBMITED"].ToString();
                    this.SRServiceCompleted = dbManager.DataReader["SR_SERVICE_COMPLETED"].ToString();
                    this.ItemCode = dbManager.DataReader["ITEM_CODE"].ToString();
                    this.SRItemSLNo = dbManager.DataReader["SR_ITEM_SL_NO"].ToString();
                    this.SRStatus = dbManager.DataReader["SR_STATUS"].ToString();
                    this.EmpId = dbManager.DataReader["EMP_ID"].ToString();
                    this.SRServiceType = dbManager.DataReader["SR_SERVICE_TYPE"].ToString();
                    this.SRFiles = dbManager.DataReader["SR_FILES"].ToString();
                    this.SRPreparedBy = dbManager.DataReader["SR_PREPARED_BY"].ToString();
                    this.SRApprovedBy = dbManager.DataReader["SR_APPROVED_BY"].ToString();
                    this.SRAMCAId = dbManager.DataReader["SR_AMCA_ID"].ToString();
                    this.SRInsAId = dbManager.DataReader["SR_INSA_ID"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
            public int ServiceReport_Select2(string SRNO)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_SERVICE_REPORT_MAST] WHERE SR_NO = " + SRNO);

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {

                    this.SRId2 = dbManager.DataReader["SR_ID"].ToString();
                    
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }




            public int complaintrecord_isrecordexists(string p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select *  from YANTRA_SERVICE_REPORT_MAST where cr_id=" + p + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;  
            }

            public string InstallAssignmentStatussalesorder_Update1(object p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "UPDATE [YANTRA_INSTALL_ASSIGNMENTS] SET  IA_STATUS='CLOSED'  WHERE SO_ID='" + p + "'";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Install Assignment Status Details", "115");

                }
                return _returnStringMessage;
            }

            public int servicereportsalesorder_isrecordexists(object p)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_AMC_ENQUIRY_MAST],[YANTRA_LKUP_ENQ_MODE],[YANTRA_LKUP_DESP_MODE],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST]  WHERE [YANTRA_AMC_ENQUIRY_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID AND [YANTRA_AMC_QUOTATION_MAST].ENQ_ID=[YANTRA_AMC_ENQUIRY_MAST].ENQ_ID AND" +
                //                            " [YANTRA_AMC_ENQUIRY_MAST].DESM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_AMC_ENQUIRY_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID AND [YANTRA_AMC_QUOTATION_MAST].QUOT_ID='" + QuotationId + "' ORDER BY [YANTRA_AMC_QUOTATION_MAST].QUOT_ID DESC ");


                _commandText = string.Format("select *  from YANTRA_SERVICE_REPORT_MAST where so_id=" + p + " ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (dbManager.DataReader.Read())
                {

                    //this.AMCQTId = dbManager.DataReader["AMCQT_ID"].ToString();
                    // this.AMCQTNo = dbManager.DataReader["AMCQT_NO"].ToString();
                    //this.AMCQTCurrFinYear = dbManager.DataReader["AMCQT_CURR_FIN_YEAR"].ToString();
                    //this.AMCQTDate = Convert.ToDateTime(dbManager.DataReader["AMCQT_DATE"].ToString()).ToString("dd/MM/yyyy");
                    // this.AMCQTPeriod = dbManager.DataReader["AMCQT_PERIOD"].ToString();
                    //this.CRId = dbManager.DataReader["CR_ID"].ToString();
                    //this.AMCQTPMCalls = dbManager.DataReader["AMCQT_PM_CALLS_VISITS"].ToString();
                    //this.AMCQTBreakDownCalls = dbManager.DataReader["AMCQT_BREAKDOWN_CALLS_VISITS"].ToString();
                    //this.AMCQTPaymentTerms = dbManager.DataReader["AMCQT_PAYMENT_TERMS"].ToString();
                    //this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();
                    //this.CustDetId = dbManager.DataReader["CUST_DET_ID"].ToString();
                    //this.AMCQTCustPONo = dbManager.DataReader["AMCQT_CUST_PREV_PO_NO"].ToString();
                    //this.AMCQTCustPODate = Convert.ToDateTime(dbManager.DataReader["AMCQT_CUST_PREV_PO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //this.AMCQTServiceTax = dbManager.DataReader["AMCQT_SERVICE_TAX"].ToString();
                    //this.AMCQTPreparedBy = dbManager.DataReader["AMCQT_PREPARED_BY"].ToString();
                    //this.AMCQTApprovedBy = dbManager.DataReader["AMCQT_APPROVED_BY"].ToString();
                    //this.AMCQTStatus = dbManager.DataReader["AMCQT_STATUS"].ToString();
                    //this.AMCQTValidity = dbManager.DataReader["AMCQT_VALIDITY"].ToString();


                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;  
            }
        }

        //Methods for Services Assignments
        public class AMCAssignment
        {
            public string AMCAId, AMCANo, AMCADate, AMCAScheduleDate, AMCAAssignDate, AMCADueDate, EmpId, AMCARemarks, AMCAStatus, AMCOId, AMCOAId;

            public AMCAssignment()
            { }

            public static string AMCAssignment_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(AMCA_NO,LEFT(AMCA_NO,5),''))),0)+1 FROM [YANTRA_AMC_ASSIGNMENTS]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ASSIGN_TASK_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_ENQ_ASSIGN_TASKS]").ToString());
                return _codePrefix + _returnIntValue;
            }

            public string AMCAssignment_Save()
            {
                this.AMCANo = AMCAssignment_AutoGenCode();
                this.AMCAId = AutoGenMaxId("[YANTRA_AMC_ASSIGNMENTS]", "AMCA_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "INSERT  INTO [YANTRA_AMC_ASSIGNMENTS] (AMCA_ID,AMCA_DATE,AMCA_SCHEDULE_DATE,AMCO_ID,AMCOA_ID,EMP_ID) VALUES(" + this.AMCAId + ",'" + this.AMCADate + "','" + this.AMCAScheduleDate + "'," + this.AMCOId + "," + this.AMCOAId + ",0)";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("AMC Assignments Details", "99");

                }
                return _returnStringMessage;
            }

            public string AMCAssignment_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "UPDATE [YANTRA_AMC_ASSIGNMENTS] SET  EMP_ID=" + this.EmpId + ",AMCA_NO='" + this.AMCANo + "',AMCA_DATE='" + this.AMCADate + "',AMCA_ASSIGN_DATE='" + this.AMCAAssignDate + "',AMCA_DUE_DATE='" + this.AMCADueDate + "',AMCA_REMARKS='" + this.AMCARemarks + "',AMCA_STATUS='New' WHERE AMCA_ID=" + this.AMCAId + "";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("AMC Assignments Details", "99");

                }
                return _returnStringMessage;
            }

            public string AMCAssignment_Delete(string AMCAId)
            {
                if (DeleteRecord("[YANTRA_AMC_ASSIGNMENTS]", "AMCA_ID", AMCAId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("AMC Assignments Details", "99");

                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static string AMCAssignmentStatus_Update(string Status, string AMCAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "UPDATE [YANTRA_AMC_ASSIGNMENTS] SET  AMCA_STATUS='" + Status + "' WHERE AMCA_ID='" + AMCAId + "'";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("AMC Assignments Status Details", "99");

                }
                return _returnStringMessage;
            }
            public string AMCAssignFollowUpDet_Id, AMCAssignTaskId, FollowUpEmpId, FollowUpDesc, FollowUpDate;
            public string AMCAssignmentsFollowUp_Save()
            {
                this.AMCAssignFollowUpDet_Id = AutoGenMaxId("[YANTRA_AMC_ASSIGNMENTS_FOLLOWUP]", "AMC_ASSIGN_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_AMC_ASSIGNMENTS_FOLLOWUP] VALUES({0},{1},{2},'{3}','{4}')", this.AMCAssignFollowUpDet_Id, this.AMCAssignTaskId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";

                    log.add_Insert("AMC Assignments Follow Up Details", "99");

                }
                return _returnStringMessage;
            }

            public int AMCAssignment_Select(string AMCAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT * FROM [YANTRA_AMC_ASSIGNMENTS],[YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_OA_MAST],[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_EMPLOYEE_MAST] " +
                                " WHERE [YANTRA_AMC_ASSIGNMENTS].AMCO_ID=[YANTRA_AMC_ORDER_MAST].AMCO_ID " +
                                " AND [YANTRA_AMC_ORDER_MAST].AMCQT_ID =YANTRA_AMC_QUOTATION_MAST.AMCQT_ID " +
                                " AND [YANTRA_AMC_QUOTATION_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                " AND [YANTRA_AMC_ASSIGNMENTS].AMCOA_ID=[YANTRA_AMC_OA_MAST].OA_ID " +
                                " AND [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_AMC_ASSIGNMENTS].EMP_ID " +
                                " AND [YANTRA_AMC_ASSIGNMENTS].AMCA_ID=" + AMCAId + "";
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.AMCAId = dbManager.DataReader["AMCA_ID"].ToString();
                    this.AMCANo = dbManager.DataReader["AMCA_NO"].ToString();
                    this.AMCADate = dbManager.DataReader["AMCA_DATE"].ToString();
                    this.AMCAScheduleDate = dbManager.DataReader["AMCA_SCHEDULE_DATE"].ToString();
                    this.AMCAAssignDate = dbManager.DataReader["AMCA_ASSIGN_DATE"].ToString();
                    this.AMCADueDate = dbManager.DataReader["AMCA_DUE_DATE"].ToString();
                    this.EmpId = dbManager.DataReader["EMP_ID"].ToString();
                    this.AMCARemarks = dbManager.DataReader["AMCA_REMARKS"].ToString();
                    this.AMCAStatus = dbManager.DataReader["AMCA_STATUS"].ToString();
                    this.AMCOId = dbManager.DataReader["AMCO_ID"].ToString();
                    this.AMCOAId = dbManager.DataReader["AMCOA_ID"].ToString();
                    if (this.AMCADate == "1/1/1900 12:00:00 AM" || this.AMCADate == "") { this.AMCADate = ""; } else { this.AMCADate = Convert.ToDateTime(this.AMCADate).ToString("dd/MM/yyyy"); }
                    if (this.AMCAScheduleDate == "1/1/1900 12:00:00 AM" || this.AMCAScheduleDate == "") { this.AMCAScheduleDate = ""; } else { this.AMCAScheduleDate = Convert.ToDateTime(this.AMCAScheduleDate).ToString("dd/MM/yyyy"); }
                    if (this.AMCAAssignDate == "1/1/1900 12:00:00 AM" || this.AMCAAssignDate == "") { this.AMCAAssignDate = ""; } else { this.AMCAAssignDate = Convert.ToDateTime(this.AMCAAssignDate).ToString("dd/MM/yyyy"); }
                    if (this.AMCADueDate == "1/1/1900 12:00:00 AM" || this.AMCADueDate == "") { this.AMCADueDate = ""; } else { this.AMCADueDate = Convert.ToDateTime(this.AMCADueDate).ToString("dd/MM/yyyy"); }

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
        }

        //Methods for Services Assignments
        public class InstallAssignment
        {
            public string IAId, IANo, IADate, IAScheduleDate, IAAssignDate, IADueDate, EmpId, IARemarks, IAStatus, SOId, DCId,Cp_Id;

            public InstallAssignment()
            { }

            public static string InstallAssignment_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(CONVERT(BIGINT,REPLACE(IA_NO,LEFT(IA_NO,5),''))),0)+1 FROM [YANTRA_INSTALL_ASSIGNMENTS]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ASSIGN_TASK_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_ENQ_ASSIGN_TASKS]").ToString());
                return _codePrefix + _returnIntValue;
            }

            public string InstallAssignment_Save()
            {
                this.IAId = AutoGenMaxId("[YANTRA_INSTALL_ASSIGNMENTS]", "IA_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "INSERT  INTO [YANTRA_INSTALL_ASSIGNMENTS] (IA_ID,IA_DATE,IA_SCHEDULE_DATE,SO_ID,DC_ID,EMP_ID) VALUES(" + this.IAId + ",'" + this.IADate + "','" + this.IAScheduleDate + "'," + this.SOId + "," + this.DCId + ",0)";
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

            public string InstallAssignment_Update()
            {
                this.IANo = InstallAssignment_AutoGenCode();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "UPDATE [YANTRA_INSTALL_ASSIGNMENTS] SET  EMP_ID=" + this.EmpId + ",IA_NO='" + this.IANo + "',IA_DATE='" + this.IADate + "',IA_ASSIGN_DATE='" + this.IAAssignDate + "',IA_DUE_DATE='" + this.IADueDate + "',IA_REMARKS='" + this.IARemarks + "',IA_STATUS='New',CP_ID="+this.Cp_Id+" WHERE IA_ID=" + this.IAId + "";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Install Assignments Details", "115");

                }
                return _returnStringMessage;
            }

            public string InstallAssignment_Delete(string IAId)
            {
                if (DeleteRecord("[YANTRA_INSTALL_ASSIGNMENTS]", "IA_ID", IAId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Install Assignments Details", "115");
                 
                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static string InstallAssignmentStatus_Update(string Status, string IAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "UPDATE [YANTRA_INSTALL_ASSIGNMENTS] SET  IA_STATUS='" + Status + "' WHERE IA_ID='" + IAId + "'";
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Install Assignments Status Details", "115");

                }
                return _returnStringMessage;
            }
            public string InstallAssignFollowUpDet_Id, InstallAssignTaskId, FollowUpEmpId, FollowUpDesc, FollowUpDate;
            public string InstallAssignmentsFollowUp_Save()
            {
                this.InstallAssignFollowUpDet_Id = AutoGenMaxId("[YANTRA_INSTALL_ASSIGNMENTS_FOLLOWUP]", "INSTALL_ASSIGN_FOLLOWUP_DET_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT  INTO [YANTRA_INSTALL_ASSIGNMENTS_FOLLOWUP] VALUES({0},{1},{2},'{3}','{4}')", this.InstallAssignFollowUpDet_Id, this.InstallAssignTaskId, this.FollowUpEmpId, this.FollowUpDesc, this.FollowUpDate);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Install Assignments Follow up Details", "115");

                }
                return _returnStringMessage;
            }

            public int InstallAssignment_Select(string IAId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT * FROM [YANTRA_INSTALL_ASSIGNMENTS],[YANTRA_SO_MAST],[YANTRA_CUSTOMER_MAST],[YANTRA_QUOT_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_EMPLOYEE_MAST],[YANTRA_ENQ_MAST] " +
                                " WHERE [YANTRA_INSTALL_ASSIGNMENTS].SO_ID=[YANTRA_SO_MAST].SO_ID " +
                                " AND [YANTRA_SO_MAST].QUOT_ID =YANTRA_QUOT_MAST.QUOT_ID " +
                                " AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID " +
                                " AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                " AND [YANTRA_INSTALL_ASSIGNMENTS].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID " +
                                " AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID " +
                                " AND [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_INSTALL_ASSIGNMENTS].EMP_ID " +
                                " AND [YANTRA_INSTALL_ASSIGNMENTS].IA_ID=" + IAId + "";
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.IAId = dbManager.DataReader["IA_ID"].ToString();
                    this.IANo = dbManager.DataReader["IA_NO"].ToString();
                    this.IADate = dbManager.DataReader["IA_DATE"].ToString();
                    this.IAScheduleDate = dbManager.DataReader["IA_SCHEDULE_DATE"].ToString();
                    this.IAAssignDate = dbManager.DataReader["IA_ASSIGN_DATE"].ToString();
                    this.IADueDate = dbManager.DataReader["IA_DUE_DATE"].ToString();
                    this.EmpId = dbManager.DataReader["EMP_ID"].ToString();
                    this.IARemarks = dbManager.DataReader["IA_REMARKS"].ToString();
                    this.IAStatus = dbManager.DataReader["IA_STATUS"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    if (this.IADate == "1/1/1900 12:00:00 AM" || this.IADate == "") { this.IADate = ""; } else { this.IADate = Convert.ToDateTime(this.IADate).ToString("dd/MM/yyyy"); }
                    if (this.IAScheduleDate == "1/1/1900 12:00:00 AM" || this.IAScheduleDate == "") { this.IAScheduleDate = ""; } else { this.IAScheduleDate = Convert.ToDateTime(this.IAScheduleDate).ToString("dd/MM/yyyy"); }
                    if (this.IAAssignDate == "1/1/1900 12:00:00 AM" || this.IAAssignDate == "") { this.IAAssignDate = ""; } else { this.IAAssignDate = Convert.ToDateTime(this.IAAssignDate).ToString("dd/MM/yyyy"); }
                    if (this.IADueDate == "1/1/1900 12:00:00 AM" || this.IADueDate == "") { this.IADueDate = ""; } else { this.IADueDate = Convert.ToDateTime(this.IADueDate).ToString("dd/MM/yyyy"); }

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                return _returnIntValue;
            }
        }
    }
}
