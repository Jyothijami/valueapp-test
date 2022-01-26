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

namespace YantraBLL.Modules
{

    public class Inventory
    {
        private static int _returnIntValue;
        private static string _returnStringMessage, _commandText;

        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());

        public Inventory()
        {
        }

        //Method for dispose 
        public static void Dispose()
        {
            if (dbManager.Connection != null)
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

        private static bool IsRecordExistsQRCode(string paraTableName, string paraFirstFieldName, string paraFirstFieldValue, string paraSecondFieldName, string paraSecondFieldValue)
        {
            bool check = false;
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM " + paraTableName + " WHERE " + paraFirstFieldName + "='" + paraFirstFieldValue + "' and convert(nvarchar(50)," + paraSecondFieldName + ")='" + paraSecondFieldValue + "'").ToString());
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
            //dbManager.Close();
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
            //dbManager.Close();
            return year;
        }

        //Methods for MRIR Form
        public class MRIR
        {
            public string MRIRId, MRIRNo, MRIRDate, FPOId, FPONo, FPODate, MRIRPDCNo, MRIRPDCDate, SupId, SupName, SupAddress, MRIRPreparedBy, MRIRApprovedBy, MRIRInvoiceNo, MRIRInvoiceDate, MRIRLRNo, MRIRVehicleNo, MRIRFromStation, MRIRTransportName, MRIRChallanNo, MRIRChallanDate, MRIRGatePassNo, MRIRGatePassDate, MRIRNotInStock, MRIRIsExcisble;
            public string MRIRDetId, ItemType, ItemCode, ItemName, UOM, MRIRDetReceivedQty, MRIRDetAccpQty, MRIRDetRejtQty, MRIRDetOrderedQty;
            public string MRIRINSPDetId, MRIRINSPDetInspDate, MRIRINSPDetVisual, MRIRINSPDetHardness, MRIRINSPDetSurfFinish, MRIRINSPDetOthers, MRIRINSPDetSTC, MRIRINSPDetInspStatus, MRIRINSPDetInspBy, MRIRINSPDetRemarks;


            public MRIR()
            {
            }

            public static string MRIR_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(MRIR_NO,LEFT(MRIR_NO,5),''))),0)+1 FROM [YANTRA_MRIR_MAST]").ToString());
                //dbManager.Close();

                return _codePrefix + _returnIntValue;
            }

            public string MRIR_Save()
            {
                this.MRIRNo = MRIR_AutoGenCode();
                this.MRIRId = AutoGenMaxId("[YANTRA_MRIR_MAST]", "MRIR_ID");

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_MRIR_MAST] VALUES({0},'{1}','{2}',{3},'{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                                                                                   this.MRIRId, this.MRIRNo, this.MRIRDate, this.FPOId, this.MRIRPDCNo, this.MRIRPDCDate, this.SupId, this.MRIRPreparedBy, this.MRIRApprovedBy, this.MRIRInvoiceNo, this.MRIRInvoiceDate, this.MRIRLRNo, this.MRIRVehicleNo, this.MRIRFromStation, this.MRIRTransportName, this.MRIRChallanNo, this.MRIRChallanDate, this.MRIRGatePassNo, this.MRIRGatePassDate, this.MRIRNotInStock, this.MRIRIsExcisble);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("MRIR Details", "59");
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string MRIR_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_MRIR_MAST] SET MRIR_DATE='{0}',MRIR_PDC_NO='{1}',MRIR_PDC_DATE='{2}',SUP_ID={3},MRIR_PREPARED_BY='{4}',MRIR_APPROVED_BY='{5}',MRIR_INVOICE_NO='{6}',MRIR_INVOICE_DATE='{7}',MRIR_LR_NO='{8}',MRIR_VEHICLE_NO='{9}',MRIR_FROM_STATION='{10}',MRIR_TRANSPORT_NAME='{11}',MRIR_CHALLAN_NO='{12}',MRIR_CHALLAN_DATE='{13}',MRIR_GATE_PASS_NO='{14}',MRIR_GATE_PASS_DATE='{15}',MRIR_NOT_IN_STOCK='{16}',MRIR_IN_EXCISBLE='{17}' WHERE MRIR_ID='{18}'", this.MRIRDate, this.MRIRPDCNo, this.MRIRPDCDate, this.SupId, this.MRIRPreparedBy, this.MRIRApprovedBy, this.MRIRInvoiceNo, this.MRIRInvoiceDate, this.MRIRLRNo, this.MRIRVehicleNo, this.MRIRFromStation, this.MRIRTransportName, this.MRIRChallanNo, this.MRIRChallanDate, this.MRIRGatePassNo, this.MRIRGatePassDate, this.MRIRNotInStock, this.MRIRIsExcisble, this.MRIRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("MRIR Details", "59");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string MRIR_Delete(string MRIRFormId)
            {
                if (DeleteRecord("[YANTRA_MRIR_DET]", "MRIR_ID", MRIRFormId) == true)
                {
                    if (DeleteRecord("[YANTRA_MRIR_MAST]", "MRIR_ID", MRIRFormId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("MRIR Details", "59");

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


            public static void MRIR_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MRIR_MAST] ORDER BY MRIR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "MRIR_NO", "MRIR_ID");
                }
                //dbManager.Close();
            }

            public int MRIR_Select(string MRIRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MRIR_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_SUPPLIER_MAST] WHERE [YANTRA_MRIR_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_MRIR_MAST].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID " +
                                            " AND [YANTRA_MRIR_MAST].MRIR_ID='" + MRIRId + "' ORDER BY [YANTRA_MRIR_MAST].MRIR_ID DESC ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.MRIRId = dbManager.DataReader["MRIR_ID"].ToString();
                    this.MRIRNo = dbManager.DataReader["MRIR_NO"].ToString();
                    this.MRIRDate = Convert.ToDateTime(dbManager.DataReader["MRIR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    this.FPODate = Convert.ToDateTime(dbManager.DataReader["FPO_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.MRIRPDCNo = dbManager.DataReader["MRIR_PDC_NO"].ToString();
                    this.MRIRPDCDate = Convert.ToDateTime(dbManager.DataReader["MRIR_PDC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.SupName = dbManager.DataReader["SUP_NAME"].ToString();
                    this.SupAddress = dbManager.DataReader["SUP_ADDRESS"].ToString();
                    this.MRIRPreparedBy = dbManager.DataReader["MRIR_PREPARED_BY"].ToString();
                    this.MRIRApprovedBy = dbManager.DataReader["MRIR_APPROVED_BY"].ToString();
                    this.MRIRInvoiceNo = dbManager.DataReader["MRIR_INVOICE_NO"].ToString();
                    this.MRIRInvoiceDate = Convert.ToDateTime(dbManager.DataReader["MRIR_INVOICE_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.MRIRLRNo = dbManager.DataReader["MRIR_LR_NO"].ToString();
                    this.MRIRVehicleNo = dbManager.DataReader["MRIR_VEHICLE_NO"].ToString();
                    this.MRIRFromStation = dbManager.DataReader["MRIR_FROM_STATION"].ToString();
                    this.MRIRTransportName = dbManager.DataReader["MRIR_TRANSPORT_NAME"].ToString();
                    this.MRIRChallanNo = dbManager.DataReader["MRIR_CHALLAN_NO"].ToString();
                    this.MRIRChallanDate = Convert.ToDateTime(dbManager.DataReader["MRIR_CHALLAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.MRIRGatePassNo = dbManager.DataReader["MRIR_GATE_PASS_NO"].ToString();
                    this.MRIRGatePassDate = Convert.ToDateTime(dbManager.DataReader["MRIR_GATE_PASS_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.MRIRNotInStock = dbManager.DataReader["MRIR_NOT_IN_STOCK"].ToString();
                    this.MRIRIsExcisble = dbManager.DataReader["MRIR_IN_EXCISBLE"].ToString();


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


            public string MRIRDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_MRIR_DET] SELECT ISNULL(MAX(MRIR_DET_ID),0)+1,{0},{1},'{2}','{3}' FROM [YANTRA_MRIR_DET]", this.MRIRId, this.ItemCode, this.MRIRDetReceivedQty, this.MRIRDetOrderedQty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("MRIR Details", "59");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public int MRIRDetails_Delete(string MRIRFormId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_MRIR_DET] WHERE MRIR_ID={0}", MRIRFormId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }

            public void MRIRDetails_Select(string MRIRFormId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MRIR_DET],[YANTRA_MRIR_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_MRIR_DET].MRIR_ID=[YANTRA_MRIR_MAST].MRIR_ID AND [YANTRA_MRIR_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_MRIR_DET].MRIR_ID=" + MRIRFormId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);


                DataTable MRIRItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                MRIRItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                MRIRItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                MRIRItems.Columns.Add(col);
                col = new DataColumn("UOM");
                MRIRItems.Columns.Add(col);
                col = new DataColumn("ReceivedQty");
                MRIRItems.Columns.Add(col);
                col = new DataColumn("OrderedQty");
                MRIRItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = MRIRItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["ReceivedQty"] = dbManager.DataReader["MRIR_DET_RECEIVED_QTY"].ToString();
                    dr["OrderedQty"] = dbManager.DataReader["MRIR_DET_ORDERED_QTY"].ToString();

                    MRIRItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();

                gv.DataSource = MRIRItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public string MRIRInspectionDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_MRIR_INSPECTION_DET] SELECT ISNULL(MAX(MRIR_INSP_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' FROM [YANTRA_MRIR_INSPECTION_DET]",
                                                                                                          this.MRIRId, this.ItemCode, this.MRIRINSPDetVisual, this.MRIRINSPDetHardness, this.MRIRINSPDetSurfFinish, this.MRIRINSPDetOthers, this.MRIRINSPDetSTC, this.MRIRINSPDetInspStatus, this.MRIRINSPDetInspBy, this.MRIRINSPDetRemarks, this.MRIRDetAccpQty, this.MRIRDetRejtQty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("MRIR Inspection Details", "60");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public int MRIRInspectionDetails_Delete(string MRIRFormId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_MRIR_INSPECTION_DET] WHERE MRIR_ID={0}", MRIRFormId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }

            public void MRIRInspectionDetails_Select(string MRIRNo, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MRIR_INSPECTION_DET],[YANTRA_MRIR_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_MRIR_INSPECTION_DET].MRIR_ID=[YANTRA_MRIR_MAST].MRIR_ID AND [YANTRA_MRIR_INSPECTION_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_ITEM_MAST].IT_TYPE_ID=[YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID AND [YANTRA_MRIR_INSPECTION_DET].FPO_ID=" + MRIRNo.Replace("MRIR/", ""));
                dbManager.ExecuteReader(CommandType.Text, _commandText);


                DataTable MRIRInspItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("UOM");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("ReceivedQty");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("OrderedQty");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("AcceptedQty");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("RejectedQty");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("Visual");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("Hardness");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("SurfaceFinish");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("Others");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("STC");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("InspectedStatus");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("InspectedBy");
                MRIRInspItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                MRIRInspItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = MRIRInspItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["ReceivedQty"] = dbManager.DataReader["MRIR_DET_RECEIVED_QTY"].ToString();
                    dr["OrderedQty"] = dbManager.DataReader["MRIR_DET_ORDERED_QTY"].ToString();
                    dr["AcceptedQty"] = dbManager.DataReader["MRIR_INSP_DET_ACCP_QTY"].ToString();
                    dr["RejectedQty"] = dbManager.DataReader["MRIR_INSP_DET_REJT_QTY"].ToString();
                    dr["Visual"] = dbManager.DataReader["MRIR_INSP_DET_VISUAL"].ToString();
                    dr["Hardness"] = dbManager.DataReader["MRIR_INSP_DET_HARDNESS"].ToString();
                    dr["SurfaceFinish"] = dbManager.DataReader["MRIR_INSP_DET_SUR_FINISH"].ToString();
                    dr["Others"] = dbManager.DataReader["MRIR_INSP_DET_OTHERS"].ToString();
                    dr["STC"] = dbManager.DataReader["MRIR_INSP_DET_STC"].ToString();
                    dr["InspectedStatus"] = dbManager.DataReader["MRIR_INSP_DET_INSP_STATUS"].ToString();
                    dr["InspectedBy"] = dbManager.DataReader["MRIR_INSP_DET_INSPECTED_BY"].ToString();
                    dr["Remarks"] = dbManager.DataReader["MRIR_INSP_DET_REMARKS"].ToString();


                    MRIRInspItems.Rows.Add(dr);

                }
                dbManager.DataReader.Close();
                gv.DataSource = MRIRInspItems;
                gv.DataBind();
                //dbManager.Close();

            }


        }

        //Methods for GRN Details Form
        public class GRNDetails
        {
            public string GRNId, GRNNo, GRNDate, MRIRId, SupId, FPOId, POSId, GRNType, GRNRemarks, GRNPreparedBy, GRNApprovedBy;
            public string GRNDetId, ItemCode, ItemName, UOM, MRIRDetOrderedQty, GRNDetReceivedQty;



            public GRNDetails()
            {
            }

            public static string GRN_AutoGenCode()
            {
                string _codePrefix = CurrentFinancialYear() + " ";
                //string _codePrefix = "ENQ/";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(GRN_NO,LEFT(GRN_NO,5),''))),0)+1 FROM [YANTRA_GRN_DETAILS_MAST]").ToString());
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(ENQ_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_ENQ_MAST]").ToString());
                //dbManager.Close();
                return _codePrefix + _returnIntValue;
            }

            public string GRN_Save()
            {
                this.GRNNo = GRN_AutoGenCode();
                this.GRNId = AutoGenMaxId("[YANTRA_GRN_DETAILS_MAST]", "GRN_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_GRN_DETAILS_MAST] VALUES({0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}')",
                                                                                           this.GRNId, this.GRNNo, this.GRNDate, this.MRIRId, this.GRNType, this.GRNRemarks, this.GRNPreparedBy, this.GRNApprovedBy);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("GRN Details", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string GRN_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_GRN_DETAILS_MAST] SET GRN_DATE='{0}',GRN_TYPE='{1}',GRN_REMARKS='{2}',GRN_PREPARED_BY='{3}',GRN_APPROVED_BY='{4}' WHERE GRN_ID='{5}'", this.GRNDate, this.GRNType, this.GRNRemarks, this.GRNPreparedBy, this.GRNApprovedBy, this.GRNId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("GRN Details", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string GRN_Delete(string GRNFormId)
            {
                if (DeleteRecord("[YANTRA_GRN_DETAILS_DET]", "GRN_ID", GRNFormId) == true)
                {
                    if (DeleteRecord("[YANTRA_GRN_DETAILS_MAST]", "GRN_ID", GRNFormId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("GRN Details", "61");

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

            public static void GRN_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_GRN_DETAILS_MAST] ORDER BY GRN_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "GRN_NO", "GRN_ID");
                }
                //dbManager.Close();
            }

            public int GRN_Select(string GRNFormId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_GRN_DETAILS_MAST],[YANTRA_MRIR_MAST],[YANTRA_SUPPLIER_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_PO_SCHEDULING_MAST] WHERE [YANTRA_GRN_DETAILS_MAST].MRIR_ID=[YANTRA_MRIR_MAST].MRIR_ID AND [YANTRA_MRIR_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_MRIR_MAST].FPO_ID=[YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_FIXED_PO_MAST].POS_ID=[YANTRA_PO_SCHEDULING_MAST].POS_ID " +
                //                            " AND [YANTRA_GRN_DETAILS_MAST].GRN_NO='" + GRNNo + "' ORDER BY [YANTRA_GRN_DETAILS_MAST].GRN_ID DESC ");

                _commandText = string.Format("SELECT * FROM [YANTRA_GRN_DETAILS_MAST],[YANTRA_GRN_DETAILS_DET],[YANTRA_MRIR_MAST],[YANTRA_FIXED_PO_MAST],[YANTRA_SUPPLIER_MAST],[YANTRA_ITEM_MAST] ,[YANTRA_LKUP_UOM] WHERE [YANTRA_GRN_DETAILS_MAST].GRN_ID=[YANTRA_GRN_DETAILS_DET].GRN_ID AND [YANTRA_GRN_DETAILS_MAST].MRIR_ID=[YANTRA_MRIR_MAST].MRIR_ID AND [YANTRA_MRIR_MAST].FPO_ID = [YANTRA_FIXED_PO_MAST].FPO_ID AND [YANTRA_MRIR_MAST].SUP_ID=[YANTRA_SUPPLIER_MAST].SUP_ID AND [YANTRA_GRN_DETAILS_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  " +
                                            " AND [YANTRA_GRN_DETAILS_MAST].GRN_ID='" + GRNFormId + "' ORDER BY [YANTRA_GRN_DETAILS_MAST].GRN_ID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.GRNId = dbManager.DataReader["GRN_ID"].ToString();
                    this.GRNNo = dbManager.DataReader["GRN_NO"].ToString();
                    this.GRNDate = Convert.ToDateTime(dbManager.DataReader["GRN_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.MRIRId = dbManager.DataReader["MRIR_ID"].ToString();
                    this.SupId = dbManager.DataReader["SUP_ID"].ToString();
                    this.FPOId = dbManager.DataReader["FPO_ID"].ToString();
                    //  this.POSId = dbManager.DataReader["POS_ID"].ToString();
                    this.GRNType = dbManager.DataReader["GRN_TYPE"].ToString();
                    this.GRNRemarks = dbManager.DataReader["GRN_REMARKS"].ToString();
                    this.GRNPreparedBy = dbManager.DataReader["GRN_PREPARED_BY"].ToString();
                    this.GRNApprovedBy = dbManager.DataReader["GRN_APPROVED_BY"].ToString();


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

            public int GRNItemCode_Select(string ItemCode, string MRIRId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_MRIR_DET] WHERE MRIR_ID=" + MRIRId + " AND ITEM_CODE=" + ItemCode + "");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.MRIRDetOrderedQty = dbManager.DataReader["MRIR_DET_ORDERED_QTY"].ToString();
                    //this.GRNDetReceivedQty = dbManager.DataReader["MRIR_DET_RECEIVED_QTY"].ToString();


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

            public string GRNDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_GRN_DETAILS_DET] SELECT ISNULL(MAX(GRN_DET_ID),0)+1,{0},'{1}','{2}','{3}' FROM [YANTRA_GRN_DETAILS_DET]", this.GRNId, this.ItemName, this.MRIRDetOrderedQty, this.GRNDetReceivedQty);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("GRN Details", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public int GRNDetails_Delete(string GRNFormId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_GRN_DETAILS_DET] WHERE GRN_ID={0}", GRNFormId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }

            public void GRNDetails_Select(string GRNFormId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_GRN_DETAILS_DET],[YANTRA_GRN_DETAILS_MAST],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_MRIR_DET] WHERE [YANTRA_GRN_DETAILS_DET].GRN_ID=[YANTRA_GRN_DETAILS_MAST].GRN_ID AND [YANTRA_GRN_DETAILS_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_GRN_DETAILS_MAST].MRIR_ID=[YANTRA_MRIR_DET].MRIR_ID  AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND [YANTRA_GRN_DETAILS_DET].GRN_ID=" + GRNFormId + "");


                dbManager.ExecuteReader(CommandType.Text, _commandText);


                DataTable GRNItems = new DataTable();
                DataColumn col = new DataColumn();


                col = new DataColumn("ItemName");
                GRNItems.Columns.Add(col);
                col = new DataColumn("UOM");
                GRNItems.Columns.Add(col);
                col = new DataColumn("OrderedQty");
                GRNItems.Columns.Add(col);
                col = new DataColumn("ReceivedQty");
                GRNItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = GRNItems.NewRow();


                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["OrderedQty"] = dbManager.DataReader["MRIR_DET_ORDERED_QTY"].ToString();
                    dr["ReceivedQty"] = dbManager.DataReader["GRN_DET_RECEIVED_QTY"].ToString();


                    GRNItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = GRNItems;
                gv.DataBind();
                //dbManager.Close();
            }
        }

        //  Methods for Delivery Challan Form
        public class Delivery
        {
            public string DCId, DCNo, DCDate,DelAdd,BillAdd, TransId, DCLrNo, DCLrDate, SOId, SoDetId, DCPreparedBy, DCApprovedBy, DCType, DCCSTNo, DCTINNo, RevisedKey, DCInwardDate, DCFor, SPOId, DespmId, Cp_Id, STATUS, Company, DcCustomerid, UnitId;
            public string DCDetId, ItemCode, DCDetQty, DCDetRemarks, CustId, DCDetSerialNo, COLORID, GODOWNID, DETSTATUS, DetCompany, DCfor, Remarks2;
            private string[] DeliveryId = { };
            public string GoDownName, Color;
            public string iqitemcode, iqresqty, iqcpid, iqgodownid, iqcolorid, iqitemqtyinhand, ITemremarks;
            public int TotalQty, ResQty, Resqtyitem;
            public string qty, actualQty, updateQty, ItemId, date, Remarks, TotalQty1, IssuedDate,FileName;
            public Delivery()
            {
            }
            public static string Delivery_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "DC/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(DC_NO,LEFT(DC_NO,5),''))),0)+1 FROM [YANTRA_DELIVERY_CHALLAN_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_DELIVERY_CHALLAN_MAST", "DC_NO");
            }

            public void DeliveryDetailsUnitName_SelectInvoiceExtra1SO(string DeliveryId, string UnitName, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR1 = 'Extra' and YANTRA_DELIVERY_CHALLAN_MAST.DC_UNIT_ID = '" + UnitName + "' AND YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();


                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }
            public void DeliveryDetails_SelectInvoiceExtra1SO(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_COLOR_MAST] WHERE YANTRA_LKUP_COLOR_MAST.Colour_id = YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID and [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and [YANTRA_SO_DET].COLOR_ID = [YANTRA_DELIVERY_CHALLAN_DET].COLOR_ID and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR1 = 'Extra' AND YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Colour");
                DeliveryItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Colour"] = dbManager.DataReader["COLOUR_NAME"].ToString();


                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

           

            public void DeliveryDetails_SelectInvoiceExtra1(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND YANTRA_DELIVERY_CHALLAN_DET.DC_FOR = 'Extra' and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID=" + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DeliveryDetails_SelectInvoiceHighSeaSale(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND YANTRA_DELIVERY_CHALLAN_MAST.DC_FOR = 'HighSeaSale' and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID=" + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void SoDetqty(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_SO_DET.ITEM_CODE,ITEM_MODEL_NO,SO_DET_QTY from YANTRA_SO_DET,YANTRA_SO_MAST,YANTRA_ITEM_MAST where YANTRA_SO_MAST.so_id = YANTRA_SO_DET.so_id  and YANTRA_SO_MAST.so_id = '" + DeliveryId + "' and YANTRA_ITEM_MAST.item_code = YANTRA_SO_DET.item_code and YANTRA_SO_DET.item_code  in (select YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE from  YANTRA_DELIVERY_CHALLAN_DET,[YANTRA_DELIVERY_CHALLAN_MAST] where [YANTRA_DELIVERY_CHALLAN_MAST].dc_id = YANTRA_DELIVERY_CHALLAN_DET.dc_id and  [YANTRA_DELIVERY_CHALLAN_MAST].so_id = '" + DeliveryId + "' group by YANTRA_DELIVERY_CHALLAN_DET.item_code  ) order by YANTRA_SO_DET.ITEM_CODE");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();

                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DCDetqtyUnitName(string DeliveryId, string UnitName, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE, sum(cast(DC_DET_QTY as int)) as DCQty  from YANTRA_DELIVERY_CHALLAN_DET,[YANTRA_DELIVERY_CHALLAN_MAST],YANTRA_LKUP_COLOR_MAST where [YANTRA_DELIVERY_CHALLAN_MAST].dc_id = YANTRA_DELIVERY_CHALLAN_DET.dc_id and  YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_UNIT_ID = '" + UnitName + "'  and [YANTRA_DELIVERY_CHALLAN_MAST].so_id = '" + DeliveryId + "' group by YANTRA_DELIVERY_CHALLAN_DET.item_code,YANTRA_DELIVERY_CHALLAN_DET.Color_id order by YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCQty");
                DeliveryItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();

                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["DCQty"] = dbManager.DataReader["DCQty"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DCDetqty(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE, sum(cast(DC_DET_QTY as int)) as DCQty  from YANTRA_DELIVERY_CHALLAN_DET,[YANTRA_DELIVERY_CHALLAN_MAST],YANTRA_LKUP_COLOR_MAST where [YANTRA_DELIVERY_CHALLAN_MAST].dc_id = YANTRA_DELIVERY_CHALLAN_DET.dc_id and  YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID  and [YANTRA_DELIVERY_CHALLAN_MAST].so_id = '" + DeliveryId + "' group by YANTRA_DELIVERY_CHALLAN_DET.item_code,YANTRA_DELIVERY_CHALLAN_DET.Color_id order by YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE  ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCQty");
                DeliveryItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();

                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["DCQty"] = dbManager.DataReader["DCQty"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }




            public void DeliveryDetailsPOUnitName_SelectInvoice(string DeliveryId, string Unitid, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_COLOR_MAST] WHERE YANTRA_LKUP_COLOR_MAST.Colour_id = YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID and [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  and [YANTRA_SO_DET].COLOR_ID = [YANTRA_DELIVERY_CHALLAN_DET].COLOR_ID  and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR1 != 'Extra'  and YANTRA_DELIVERY_CHALLAN_MAST.DC_UNIT_ID = '" + Unitid + "'   AND YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Colour");
                DeliveryItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();

                    dr["Colour"] = dbManager.DataReader["COLOUR_NAME"].ToString();


                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetailsPOPrint_SelectInvoice(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and [YANTRA_SO_DET].COLOR_ID = [YANTRA_DELIVERY_CHALLAN_DET].COLOR_ID and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR1 != 'Extra' AND YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Qty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Description");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("ODcNo");
                DeliveryItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Qty"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DCDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Description"] = dbManager.DataReader["Remarks"].ToString();
                    dr["BalanceQty"] = dbManager.DataReader["BalanceQty"].ToString();
                    dr["ODcNo"] = dbManager.DataReader["DC_LR_NO"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public string DCDOC_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [DC_DOCUMENTS] SELECT ISNULL(MAX(DCDOC_ID),0)+1,'{0}','{1}','{2}',{3} FROM [DC_DOCUMENTS]", this.IssuedDate, this.Remarks, this.FileName, this.DCId);
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

            public string DCDocDetails_Delete(string Eleid)
            {
                if (DeleteRecord("[DC_DOCUMENTS]", "DCDOC_ID", Eleid) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";
                }

                return _returnStringMessage;
            }

            public void DeliveryDetailsPOExtraPrint_SelectInvoice(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR1 = 'Extra' AND YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Qty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Description");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ODcNo");
                DeliveryItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["Qty"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DCDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Description"] = dbManager.DataReader["Remarks"].ToString();
                    dr["ODcNo"] = dbManager.DataReader["DC_LR_NO"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DeliveryDetailsPO_SelectInvoice(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_LKUP_COLOR_MAST] WHERE YANTRA_LKUP_COLOR_MAST.Colour_id = YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID and [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and [YANTRA_SO_DET].COLOR_ID = [YANTRA_DELIVERY_CHALLAN_DET].COLOR_ID and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR1 != 'Extra' AND YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("Colour");
                DeliveryItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Colour"] = dbManager.DataReader["COLOUR_NAME"].ToString();



                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DeliveryDetails_SelectInvoice(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID = YANTRA_SO_DET.COLOR_ID and  [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID   and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID=" + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DetId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("GST_TAX");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                    dr["DetId"] = dbManager.DataReader["DC_DET_ID"].ToString();
                    dr["GST_TAX"] = dbManager.DataReader["GST Tax"].ToString();


                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

		public void DeliveryDetails_SelectInvoiceforUB(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID = YANTRA_SO_DET.COLOR_ID and  [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID   and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID in (" + DeliveryId+")");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DetId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("GST_TAX");
                DeliveryItems.Columns.Add(col);
                

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                    dr["DetId"] = dbManager.DataReader["DC_DET_ID"].ToString();
                    dr["GST_TAX"] = dbManager.DataReader["GST Tax"].ToString();
                    //dr["DC_UNIT_ID"] = dbManager.DataReader["DC_UNIT_ID"].ToString();


                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DeliveryDetails_SelectInvoiceExtra2(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                // string hai = "Extra";
                _commandText = string.Format("SELECT * from [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_ITEM_MAST,YANTRA_LKUP_UOM WHERE YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_ITEM_MAST.ITEM_CODE =  YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE and  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                //col = new DataColumn("Rate");
                //DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    //dr["Rate"] = dbManager.DataReader["ITEM_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetails_SelectInvoiceExtraItems(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //string hai = "Cash";
                _commandText = string.Format("SELECT * from [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_ITEM_MAST,YANTRA_LKUP_UOM WHERE YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_ITEM_MAST.ITEM_CODE =  YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE and  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("GST_Tax");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["Remarks"] = dbManager.DataReader["ITEM_SPEC"].ToString();
                    dr["GST_Tax"] = dbManager.DataReader["GST Tax"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            //public void DeliveryDetails_SelectInvoiceExtraItems(string DeliveryId, GridView gv)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    //string hai = "Cash";
            //    _commandText = string.Format("SELECT * from [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_ITEM_MAST,YANTRA_LKUP_UOM WHERE YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_ITEM_MAST.ITEM_CODE =  YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE and  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DeliveryId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);

            //    DataTable DeliveryItems = new DataTable();
            //    DataColumn col = new DataColumn();
            //    col = new DataColumn("DC No");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("ItemCode");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("ModelNo");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("ItemName");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("UOM");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("Quantity");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("Remarks");
            //    DeliveryItems.Columns.Add(col);

            //    while (dbManager.DataReader.Read())
            //    {
            //        DataRow dr = DeliveryItems.NewRow();
            //        dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
            //        dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
            //        dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //        dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
            //        dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
            //        dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
            //        dr["Remarks"] = dbManager.DataReader["ITEM_SPEC"].ToString();

            //        DeliveryItems.Rows.Add(dr);
            //    }
            //    dbManager.DataReader.Close();
            //    gv.DataSource = DeliveryItems;
            //    gv.DataBind();
            //    //dbManager.Close();
            //}


            public void DeliveryDetails_SelectInvoiceExtra(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                string hai = "Extra";
                _commandText = string.Format("SELECT * from [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_ITEM_MAST,YANTRA_LKUP_UOM WHERE YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_ITEM_MAST.ITEM_CODE =  YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE and  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_DET.DC_FOR = '" + hai + "' and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["ITEM_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetails_SelectInvoiceExtraonDc(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * from [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_ITEM_MAST,YANTRA_LKUP_UOM WHERE YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_ITEM_MAST.ITEM_CODE =  YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE and  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  and  YANTRA_DELIVERY_CHALLAN_MAST.DC_FOR = 'Sample'  and  YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["ITEM_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DeliveryDetails_SelectInvoiceExtraonCash(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * from [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_ITEM_MAST,YANTRA_LKUP_UOM WHERE YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_ITEM_MAST.ITEM_CODE =  YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE and  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  and  YANTRA_DELIVERY_CHALLAN_MAST.DC_FOR = 'Cash'  and  YANTRA_DELIVERY_CHALLAN_MAST.DC_ID= " + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["ITEM_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetails_SelectInvoice1(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],warehouse_tbl,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND  YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID and YANTRA_DELIVERY_CHALLAN_DET.GODOWN_ID = warehouse_tbl.wh_id and  YANTRA_DELIVERY_CHALLAN_MAST.DC_ID=" + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Vat");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Cst");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Godown");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Color");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();

                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();

                    if (dbManager.DataReader["SO_VAT"].ToString() == "")
                    {
                        dr["Vat"] = "0";
                    }
                    else
                    {
                        dr["Vat"] = dbManager.DataReader["SO_VAT"].ToString();
                    }
                    if (dbManager.DataReader["SO_CST"].ToString() == "")
                    {
                        dr["Cst"] = "0";
                    }
                    else
                    {
                        dr["Cst"] = dbManager.DataReader["SO_CST"].ToString();
                    }
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Godown"] = dbManager.DataReader["whname"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetails_SelectInvoiceSO(string SoId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_APPROVED_BY<>0 and YANTRA_DELIVERY_CHALLAN_MAST.SO_ID=" + SoId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("POQty");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("GST_TAX");
                DeliveryItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Rate"] = dbManager.DataReader["SO_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString();
                    dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["GST_TAX"] = dbManager.DataReader["GST Tax"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetails_SelectInvoiceSI(string SoId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT *,YANTRA_DELIVERY_CHALLAN_MAST.DC_LR_NO as hai,YANTRA_DELIVERY_CHALLAN_MAST.DC_NO as ph FROM [YANTRA_SALES_INVOICE_DET],[YANTRA_SO_DET],[YANTRA_SO_MAST],[YANTRA_ITEM_MAST],[YANTRA_SALES_INVOICE_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_DELIVERY_CHALLAN_MAST WHERE YANTRA_SALES_INVOICE_DET.SI_COLOR_ID = YANTRA_SO_DET.COLOR_ID and [YANTRA_SO_MAST].SO_ID=[YANTRA_SO_DET].SO_ID AND [YANTRA_ITEM_MAST].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_SO_MAST].SO_ID=[YANTRA_SALES_INVOICE_MAST].SO_ID AND [YANTRA_SALES_INVOICE_MAST].SI_ID=[YANTRA_SALES_INVOICE_DET].SI_ID AND [YANTRA_SALES_INVOICE_DET].ITEM_CODE=[YANTRA_SO_DET].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = YANTRA_SALES_INVOICE_DET.SI_DC_ID  and YANTRA_SALES_INVOICE_MAST.SO_ID=" + SoId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Rate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SPPrice");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemStatus");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DcNO");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("LrNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("InvoiceNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ODcNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SIDate");
                DeliveryItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DC No"] = dbManager.DataReader["SI_NO"].ToString();
                    dr["Rate"] = dbManager.DataReader["SI_DET_RATE"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    //dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["DcNO"] = dbManager.DataReader["hai"].ToString();
                    dr["LrNo"] = dbManager.DataReader["Ph"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SI_Description"].ToString();
                    dr["InvoiceNo"] = dbManager.DataReader["SI_Invoice"].ToString();
                    dr["ODcNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["SIDate"] = Convert.ToDateTime(dbManager.DataReader["SI_DATE"].ToString()).ToString("dd/MM/yyyy");

                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public string Delivery_Save()
            {
                this.RevisedKey = "";
                this.DCNo = Delivery_AutoGenCode();
                this.DCId = AutoGenMaxId("[YANTRA_DELIVERY_CHALLAN_MAST]", "DC_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_DELIVERY_CHALLAN_MAST] SELECT ISNULL(MAX(DC_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},{16},'{17}',{18},{19},{20} FROM [YANTRA_DELIVERY_CHALLAN_MAST]", this.DCNo, this.DCDate, this.TransId, this.DCLrNo, this.DCLrDate, this.SOId, this.DCPreparedBy, this.DCApprovedBy, this.DCType, this.DCCSTNo, this.DCTINNo, this.RevisedKey, this.DCInwardDate, this.DCFor, this.SPOId, this.DespmId, this.Cp_Id, this.STATUS, this.Company, this.DcCustomerid, this.UnitId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Delivery Details", "62");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string DeliveryRevise_Save()
            {
                this.DCId = AutoGenMaxId("[YANTRA_DELIVERY_CHALLAN_MAST]", "DC_ID");
                this.DCNo = Delivery_AutoGenCode();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_DELIVERY_CHALLAN_MAST] SELECT ISNULL(MAX(DC_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},{16},'{17}',{18},{19},{20} FROM [YANTRA_DELIVERY_CHALLAN_MAST]", this.DCNo, this.DCDate, this.TransId, this.DCLrNo, this.DCLrDate, this.SOId.Replace("SO/", ""), this.DCPreparedBy, this.DCApprovedBy, this.DCType, this.DCCSTNo, this.DCTINNo, this.RevisedKey, this.DCInwardDate, this.DCFor, this.SPOId, this.DespmId, this.Cp_Id, this.STATUS, this.Company, this.DcCustomerid, this.UnitId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Delivery Revise Details", "63");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string Sample_Delivery_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_DELIVERY_CHALLAN_MAST] SET DC_DATE='{0}',DC_LR_NO='{1}',DC_LR_DATE='{2}',TRANS_ID={3},DC_TYPE='{4}',DC_CST_NO='{5}',DC_TIN_NO='{6}',DC_INWARD_DATE='{7}',DESPM_ID={8},CP_ID={9},DC_COMPANY = {10},DC_UNIT_ID = {11},DC_PREPARED_BY='{13}',DC_CUSTOMER_ID={14},DC_For='{15}' WHERE DC_ID= {12}", this.DCDate, this.DCLrNo, this.DCLrDate, this.TransId, this.DCType, this.DCCSTNo, this.DCTINNo, this.DCInwardDate, this.DespmId, this.Cp_Id, this.Company, this.UnitId, this.DCId, this.DCPreparedBy, this.DcCustomerid, this.DCFor);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Delivery Details", "62");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string Delivery_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_DELIVERY_CHALLAN_MAST] SET DC_DATE='{0}',DC_LR_NO='{1}',DC_LR_DATE='{2}',TRANS_ID={3},DC_TYPE='{4}',DC_CST_NO='{5}',DC_TIN_NO='{6}',DC_INWARD_DATE='{7}',DESPM_ID={8},CP_ID={9},DC_COMPANY = {10},DC_UNIT_ID = {11},DC_PREPARED_BY='{13}' WHERE DC_ID= {12}", this.DCDate, this.DCLrNo, this.DCLrDate, this.TransId, this.DCType, this.DCCSTNo, this.DCTINNo, this.DCInwardDate, this.DespmId, this.Cp_Id, this.Company, this.UnitId, this.DCId, this.DCPreparedBy);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Delivery Details", "62");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string Delivery_Delete(string DeliveryId, string SoIdForDelete)
            {

                //if (DeleteRecord("[YANTRA_DELIVERY_CHALLAN_DET]", "DC_ID", DeliveryId) == true)
                if (DeliveryDetails_Delete(DeliveryId) > 0)
                {

                    if (DeleteRecord("[YANTRA_DELIVERY_CHALLAN_MAST]", "DC_ID", DeliveryId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Delivery Details", "62");

                        //if (dbManager.Transaction == null)
                        //    dbManager.Open();
                        //_commandText = string.Format("UPDATE  [YANTRA_SO_DET] SET SO_RES_STATUS='True'  WHERE SO_ID={0} ",SoIdForDelete );
                        //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
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

            public static void DeliveryChallanApproved_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE DC_APPROVED_BY<>0 ORDER BY DC_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DC_NO", "DC_ID");
                }
                //dbManager.Close();
            }

            public static void Delivery_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] ORDER BY DC_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DC_NO", "DC_ID");
                }
                //dbManager.Close();
            }


            public static void DeliveryChallanSample_SelectSO(Control ControlForBind, string CpId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT DC_ID,DC_NO FROM [YANTRA_DELIVERY_CHALLAN_MAST] where  [YANTRA_DELIVERY_CHALLAN_MAST].DC_For = 'Sample' or [YANTRA_DELIVERY_CHALLAN_MAST].DC_For = 'Cash' and YANTRA_DELIVERY_CHALLAN_MAST.CP_ID=" + CpId + " ORDER BY DC_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DC_NO", "DC_ID");
                }
                //dbManager.Close();
            }



            public static void DeliveryChallanApprovedOnDC_SelectSO(Control ControlForBind, string Custid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT DC_ID,DC_NO FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_CUSTOMER_MAST.CUST_ID=YANTRA_DELIVERY_CHALLAN_MAST.DC_CUSTOMER_ID   and [YANTRA_DELIVERY_CHALLAN_MAST].DC_CUSTOMER_ID=" + Custid + "ORDER BY DC_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DC_NO", "DC_ID");
                }
                //dbManager.Close();
            }

            public static void DeliveryChallanApprovedOnDCUnit_SelectSO(Control ControlForBind, string Custid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT DC_ID,DC_NO FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_CUSTOMER_MAST.CUST_ID=YANTRA_DELIVERY_CHALLAN_MAST.DC_CUSTOMER_ID   and [YANTRA_DELIVERY_CHALLAN_MAST].DC_UNIT_ID =" + Custid + "ORDER BY DC_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DC_NO", "DC_ID");
                }
                //dbManager.Close();
            }





            public static void DeliveryChallanApproved_SelectSO(Control ControlForBind, string SoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT DC_ID,DC_NO FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE YANTRA_SO_MAST.SO_ID=YANTRA_DELIVERY_CHALLAN_MAST.SO_ID and [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=" + SoId + "ORDER BY DC_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "DC_NO", "DC_ID");
                }
                //dbManager.Close();
            }

            public static void DeliveryChallanApproved_SelectSI(Control ControlForBind, string SoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SI_ID,SI_NO FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_SO_MAST] WHERE YANTRA_SO_MAST.SO_ID=YANTRA_SALES_INVOICE_MAST.SO_ID and SI_APPROVED_BY<>0 and [YANTRA_SALES_INVOICE_MAST].SO_ID=" + SoId + " ORDER BY SI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SI_NO", "SI_ID");
                }
                //dbManager.Close();
            }

            public int DeliveryCust_Select(string DCId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_DELIVERY_CHALLAN_MAST inner join YANTRA_CUSTOMER_MAST on YANTRA_DELIVERY_CHALLAN_MAST .DC_CUSTOMER_ID  =YANTRA_CUSTOMER_MAST .CUST_ID inner join YANTRA_CUSTOMER_UNITS on YANTRA_DELIVERY_CHALLAN_MAST .DC_UNIT_ID =YANTRA_CUSTOMER_UNITS .CUST_UNIT_ID  where DC_ID='" + DCId + "' ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.DCNo = dbManager.DataReader["DC_NO"].ToString();
                    this.DCDate = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.DelAdd = dbManager.DataReader["CUST_UNIT_ADDRESS"].ToString();
                    this.BillAdd = dbManager.DataReader["CUST_ADDRESS"].ToString();
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

            public int Delivery_Select(string DCId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE  [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID='" + DCId + "' ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC ");

                //_commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_TRANS_MAST],[YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID  AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID " +
                //     " AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID  " +
                //     " AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID='" + DCId + "' ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC ");

                //                _commandText = "SELECT * ,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY,FORSALES.CUST_NAME AS FORSALESCUST,FORSPARES.CUST_NAME AS FORSPARESCUST " +
                //" FROM [YANTRA_DELIVERY_CHALLAN_MAST] inner join [YANTRA_LKUP_TRANS_MAST] on [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID AND  [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID='" + DCId + "'" +
                //" LEFT OUTER join [YANTRA_SO_MAST] on [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID " +
                //" LEFT OUTER join [YANTRA_QUOT_MAST] on [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID " +
                //" LEFT OUTER join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID 	" +
                //" LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSALES on [YANTRA_ENQ_MAST].CUST_ID=FORSALES.CUST_ID " +
                //" LEFT OUTER join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID " +
                //" left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID " +
                //" left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_PREPARED_BY " +
                //" left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_APPROVED_BY " +
                //" left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID " +
                //" LEFT OUTER join [YANTRA_SPARES_ORDER_MAST] on [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID " +
                //" LEFT OUTER join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID " +
                //" LEFT OUTER join [YANTRA_COMPLAINT_REGISTER] on [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SPARES_QUOT_MAST].CR_ID 	" +
                //" LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSPARES on [YANTRA_COMPLAINT_REGISTER].CUST_ID=FORSPARES.CUST_ID " +
                //" left outer join YANTRA_COMP_PROFILE on YANTRA_COMP_PROFILE.CP_ID = [YANTRA_DELIVERY_CHALLAN_MAST].DC_COMPANY " +
                //" ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC";

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    this.DCNo = dbManager.DataReader["DC_NO"].ToString();
                    this.DCDate = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.TransId = dbManager.DataReader["TRANS_ID"].ToString();
                    this.CustId = dbManager.DataReader["DC_CUSTOMER_ID"].ToString();
                    this.DCLrNo = dbManager.DataReader["DC_LR_NO"].ToString();
                    this.DCLrDate = Convert.ToDateTime(dbManager.DataReader["DC_LR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.DCPreparedBy = dbManager.DataReader["DC_PREPARED_BY"].ToString();
                    this.DCApprovedBy = dbManager.DataReader["DC_APPROVED_BY"].ToString();
                    this.DCType = dbManager.DataReader["DC_TYPE"].ToString();
                    this.DCCSTNo = dbManager.DataReader["DC_CST_NO"].ToString();
                    this.DCTINNo = dbManager.DataReader["DC_TIN_NO"].ToString();
                    this.DCInwardDate = Convert.ToDateTime(dbManager.DataReader["DC_INWARD_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.RevisedKey = dbManager.DataReader["DC_REVISED_KEY"].ToString();
                    this.DCFor = dbManager.DataReader["DC_FOR"].ToString();
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.Company = dbManager.DataReader["DC_COMPANY"].ToString();
                    this.DcCustomerid = dbManager.DataReader["DC_CUSTOMER_ID"].ToString();
                    this.DetCompany = dbManager.DataReader["DC_COMPANY"].ToString();
                    this.UnitId = dbManager.DataReader["DC_UNIT_ID"].ToString();
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

            public int Delivery_SelectSO(string SoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_TRANS_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID  " +
                //                            " AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID='" + DCId + "' ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC ");

                //_commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_TRANS_MAST],[YANTRA_SO_MAST],[YANTRA_QUOT_MAST],[YANTRA_ENQ_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID  AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID " +
                //     " AND [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID AND [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID  " +
                //     " AND [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID='" + DCId + "' ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC ");

                _commandText = "SELECT * ,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY,FORSALES.CUST_NAME AS FORSALESCUST,FORSPARES.CUST_NAME AS FORSPARESCUST " +
                " FROM [YANTRA_DELIVERY_CHALLAN_MAST] inner join [YANTRA_LKUP_TRANS_MAST] on [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID" +
                " LEFT OUTER join [YANTRA_SO_MAST] on [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID AND  [YANTRA_SO_MAST].SO_ID=" + SoId +
                " LEFT OUTER join [YANTRA_QUOT_MAST] on [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID " +
                " LEFT OUTER join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID 	" +
                " LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSALES on [YANTRA_ENQ_MAST].CUST_ID=FORSALES.CUST_ID " +
                " LEFT OUTER join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID " +
                " left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID " +
                 " left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_PREPARED_BY " +
" left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_APPROVED_BY " +
" left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID " +
" LEFT OUTER join [YANTRA_SPARES_ORDER_MAST] on [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID " +
" LEFT OUTER join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID " +
" LEFT OUTER join [YANTRA_COMPLAINT_REGISTER] on [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SPARES_QUOT_MAST].CR_ID 	" +
" LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSPARES on [YANTRA_COMPLAINT_REGISTER].CUST_ID=FORSPARES.CUST_ID " +
" ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC";

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    this.DCNo = dbManager.DataReader["DC_NO"].ToString();
                    this.DCDate = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.TransId = dbManager.DataReader["TRANS_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.DCLrNo = dbManager.DataReader["DC_LR_NO"].ToString();
                    this.DCLrDate = Convert.ToDateTime(dbManager.DataReader["DC_LR_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.DCPreparedBy = dbManager.DataReader["DC_PREPARED_BY"].ToString();
                    this.DCApprovedBy = dbManager.DataReader["DC_APPROVED_BY"].ToString();
                    this.DCType = dbManager.DataReader["DC_TYPE"].ToString();
                    this.DCCSTNo = dbManager.DataReader["DC_CST_NO"].ToString();
                    this.DCTINNo = dbManager.DataReader["DC_TIN_NO"].ToString();
                    this.DCInwardDate = Convert.ToDateTime(dbManager.DataReader["DC_INWARD_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.RevisedKey = dbManager.DataReader["DC_REVISED_KEY"].ToString();
                    this.DCFor = dbManager.DataReader["DC_FOR"].ToString();
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.UnitId = dbManager.DataReader["DC_UNIT_ID"].ToString();

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
            public string OutwardDeleteHistory()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [Outward_History] SELECT ISNULL (MAX(Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}' from [Outward_History]", this.ItemId, this.ItemCode, this.Cp_Id, this.GoDownName, this.date, this.Color, this.TotalQty1 , this.qty, this.Remarks, this.DCPreparedBy);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Delivery Details", "62");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string invoiceNo;
            public string DeliveryDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_DELIVERY_CHALLAN_DET] SELECT ISNULL(MAX(DC_DET_ID),0)+1,{0},{1},'{2}','{3}',{4},{5},'{6}','{7}',{8},'{9}','{10}','{11}' FROM [YANTRA_DELIVERY_CHALLAN_DET]", this.DCId, this.ItemCode, this.DCDetQty, this.DCDetSerialNo, this.COLORID, this.GODOWNID, this.DETSTATUS, this.ITemremarks, this.DetCompany, this.DCfor, this.Remarks2, this.invoiceNo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Delivery Details", "62");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }




            public string Itemqty_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int Rqty = 0;

                _commandText = string.Format("UPDATE  [YANTRA_SO_DET] SET SO_RES_STATUS='False'  WHERE ITEM_CODE={0} and COLOR_ID = {1} and SO_ID={2} ", this.iqitemcode, this.iqcolorid, this.SoDetId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT ITEM_QTY_IN_HAND,ITEM_RES_QTY from YANTRA_ITEM_QTY where ITEM_CODE={0} and COLOUR_ID={1}  and CP_ID = {2} and GODOWN_ID = {3}", this.iqitemcode, this.iqcolorid, this.iqcpid, this.iqgodownid);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString() != string.Empty)
                    {
                        this.TotalQty = int.Parse(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());
                    }
                    if (dbManager.DataReader["ITEM_RES_QTY"].ToString() != "0")
                    {
                        Rqty = int.Parse(dbManager.DataReader["ITEM_RES_QTY"].ToString());
                    }

                }
                dbManager.DataReader.Close();

                this.TotalQty = this.TotalQty - int.Parse(this.iqitemqtyinhand);
                Rqty = Rqty - int.Parse(this.iqitemqtyinhand);

                if (Rqty < 0)
                {
                    Rqty = 0;
                }


                _commandText = string.Format("UPDATE  [YANTRA_RESERVE_QTY] SET RES_QTY = {0}  WHERE ITEM_CODE={1} and SO_ID = {2} and COLOUR_ID = {3} ", Rqty, this.iqitemcode, this.SOId, this.iqcolorid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                //dbManager.Close();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_ITEM_QTY] SET ITEM_QTY_IN_HAND = {0},ITEM_RES_QTY = {5}  WHERE ITEM_CODE={1} and CP_ID = {2} and GODOWN_ID = {3} and COLOUR_ID = {4} ", this.TotalQty, this.iqitemcode, this.iqcpid, this.iqgodownid, this.iqcolorid, Rqty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Item Quantity Details", "64");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string BalanceQty_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int BalanceQty = 0;
                int totalqty = 0;
                _commandText = string.Format("SELECT YANTRA_SO_DET.BalanceQty from YANTRA_SO_DET where ITEM_CODE={0} and COLOR_ID={1} and SO_ID = {2}", this.iqitemcode, this.iqcolorid, this.SOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["BalanceQty"].ToString() != string.Empty)
                    {
                        BalanceQty = int.Parse(dbManager.DataReader["BalanceQty"].ToString());
                    }

                }
                dbManager.DataReader.Close();

                totalqty = BalanceQty - int.Parse(this.DCDetQty);

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_SO_DET] SET BalanceQty = {0}  WHERE ITEM_CODE={1} and SO_ID = {2} and COLOR_ID = {3} ", totalqty, this.iqitemcode, this.SOId, this.iqcolorid);
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
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string POBalanceQty_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int BalanceQty = 0;
                int totalqty = 0;
                _commandText = string.Format("SELECT YANTRA_SO_DET.BalanceQty from YANTRA_SO_DET where ITEM_CODE={0} and COLOR_ID={1} and SO_ID = {2}", this.iqitemcode, this.iqcolorid, this.SOId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["BalanceQty"].ToString() != string.Empty)
                    {
                        BalanceQty = int.Parse(dbManager.DataReader["BalanceQty"].ToString());
                    }

                }
                dbManager.DataReader.Close();

                totalqty = BalanceQty + int.Parse(this.DCDetQty);

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_SO_DET] SET BalanceQty = {0}  WHERE ITEM_CODE={1} and SO_ID = {2} and COLOR_ID = {3} ", totalqty, this.iqitemcode, this.SOId, this.iqcolorid);
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
                //dbManager.Close();
                return _returnStringMessage;
            }
            //////////////////////////////////////////Sample Dc///////////////
            public string SampleItemqty_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                int Rqty = 0;

                _commandText = string.Format("SELECT ITEM_QTY_IN_HAND,ITEM_RES_QTY from YANTRA_ITEM_QTY where ITEM_CODE={0} and COLOUR_ID={1}  and CP_ID = {2} and GODOWN_ID = {3}", this.iqitemcode, this.iqcolorid, this.iqcpid, this.iqgodownid);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString() != string.Empty)
                    {
                        this.TotalQty = int.Parse(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());
                    }

                    if (dbManager.DataReader["ITEM_RES_QTY"].ToString() != "0")
                    {
                        Rqty = int.Parse(dbManager.DataReader["ITEM_RES_QTY"].ToString());
                    }



                }
                dbManager.DataReader.Close();
                this.TotalQty = this.TotalQty - int.Parse(this.iqitemqtyinhand);
                Rqty = Rqty - int.Parse(this.iqitemqtyinhand);

                if (Rqty < 0)
                {
                    Rqty = 0;
                }


                //_commandText = string.Format("UPDATE  [YANTRA_RESERVE_QTY] SET RES_QTY = {0}  WHERE ITEM_CODE={1} and SO_ID = {2} and COLOUR_ID = {3} ", Rqty, this.iqitemcode, this.SoDetId, this.iqcolorid);
                //_returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //_returnStringMessage = string.Empty;                   
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_ITEM_QTY] SET ITEM_QTY_IN_HAND = {0},ITEM_RES_QTY = {5}  WHERE ITEM_CODE={1} and CP_ID = {2} and GODOWN_ID = {3} and COLOUR_ID = {4} ", this.TotalQty, this.iqitemcode, this.iqcpid, this.iqgodownid, this.iqcolorid, ResQty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sample Item Quantity Details", "66");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            //////////////////////////SalesReturn Update in Stock///////////////////
            public string Return_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();


                _commandText = string.Format("SELECT ITEM_QTY_IN_HAND from YANTRA_ITEM_QTY where ITEM_CODE={0} and COLOUR_ID={1}  and CP_ID = {2} and GODOWN_ID = {3}", this.iqitemcode, this.iqcolorid, this.iqcpid, this.iqgodownid);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    if (dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString() != string.Empty)
                    {
                        this.TotalQty = int.Parse(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString());
                    }

                }
                dbManager.DataReader.Close();
                this.TotalQty = this.TotalQty + int.Parse(this.iqitemqtyinhand);

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_ITEM_QTY] SET ITEM_QTY_IN_HAND = {0}  WHERE ITEM_CODE={1} and CP_ID = {2} and GODOWN_ID = {3} and COLOUR_ID = {4} ", this.TotalQty, this.iqitemcode, this.iqcpid, this.iqgodownid, this.iqcolorid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Return Status Details", "67");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            ////////////

            //string[] forItemsStock;
            //string ItemsStockString;
            public int DeliveryDetails_Delete(string DeliveryId)
            {
                ////////  DO NOT REMOVE THIS CODE.... KEEP IT IN COMMENTS
                ////////if (dbManager.Transaction == null)
                ////////    dbManager.Open();
                ////////_commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM] WHERE [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                ////////                          "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND [YANTRA_DELIVERY_CHALLAN_DET].DC_ID=" + DeliveryId);
                ////////dbManager.ExecuteReader(CommandType.Text, _commandText);
                ////////while (dbManager.DataReader.Read())
                ////////{
                ////////    if (ItemsStockString == null || ItemsStockString == "")
                ////////    {
                ////////        ItemsStockString = dbManager.DataReader["ITEM_CODE"].ToString() + "#" + Convert.ToString(Convert.ToInt64(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString()) + Convert.ToInt64(dbManager.DataReader["DC_DET_QTY"].ToString()));
                ////////    }
                ////////    else
                ////////    {
                ////////        ItemsStockString = ItemsStockString + "@" + dbManager.DataReader["ITEM_CODE"].ToString() + "#" + Convert.ToString(Convert.ToInt64(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString()) + Convert.ToInt64(dbManager.DataReader["DC_DET_QTY"].ToString()));
                ////////    }
                ////////}
                ////////dbManager.DataReader.Close();

                ////////if (ItemsStockString != null)
                ////////{
                ////////    forItemsStock = ItemsStockString.Split('@');
                ////////    ItemsStockString = "";
                ////////    for (int count = 0; count < forItemsStock.Length; count++)
                ////////    {
                ////////        string[] forItemStockUpdate;
                ////////        forItemStockUpdate = forItemsStock[count].Split('#');
                ////////        _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST]  SET  ITEM_QTY_IN_HAND={0}  WHERE ITEM_CODE={1}", forItemStockUpdate[1], forItemStockUpdate[0]);
                ////////        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                ////////    }
                ////////}

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_DELIVERY_CHALLAN_DET] WHERE DC_ID={0}", DeliveryId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            public int SpareItem_Delete(string spId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_item_spare_mast] WHERE item_sparecode={0}", spId);

                //log.add_Delete("Delivery Challan Item Details", "62");

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            public int DeliveryItem_Delete(string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_DELIVERY_CHALLAN_DET] WHERE DC_DET_ID={0}", ItemCode);

                log.add_Delete("Delivery Challan Item Details", "62");

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            #region delete dc_det with serial no
            public int DeliveryItem_Delete_SrNO(string SrNo)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_DELIVERY_CHALLAN_DET] WHERE DC_SERIAL_NO='{0}'", SrNo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            #endregion

            public int MovingDC_Delete(string ItemCode)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [STOCKMOVEMENT_DETAILS] WHERE SM_DCDET_ID={0}", ItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            public int DeliveryChallen_Delete(string DCID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE DC_ID={0}", DCID);
                log.add_Delete("Delivery Challan Master Details", "62");

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            public string DeliveryDetailsIssueStock_Update(string ItemCode, string DCQty)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET  ITEM_QTY_IN_HAND=CONVERT(BIGINT,ITEM_QTY_IN_HAND)-'{0}' WHERE ITEM_CODE='{1}'", DCQty, ItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Delivery Details", "62");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public void DeliveryDetails_Select(string DeliveryId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],YANTRA_DELIVERY_CHALLAN_MAST,[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_COLOR_MAST,warehouse_tbl WHERE  [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND YANTRA_DELIVERY_CHALLAN_MAST.DC_ID=dbo.YANTRA_DELIVERY_CHALLAN_DET.DC_ID AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID AND YANTRA_DELIVERY_CHALLAN_DET.GODOWN_ID = warehouse_tbl.wh_id AND  [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID='" + DeliveryId + "' order by [YANTRA_DELIVERY_CHALLAN_DET].DC_DET_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DcId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SubCategory");
                DeliveryItems.Columns.Add(col);
                //col = new DataColumn("SerialNo");
                //DeliveryItems.Columns.Add(col);
                col = new DataColumn("Color");
                DeliveryItems.Columns.Add(col);
                //col = new DataColumn("ColorId");
                //DeliveryItems.Columns.Add(col);
                col = new DataColumn("Godown");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Extra");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DetId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DC_ID");
                DeliveryItems.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["DCDate"] = dbManager.DataReader["DC_DATE"].ToString();

                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();

                    //dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Godown"] = dbManager.DataReader["GODOWN_ID"].ToString();

                    dr["SubCategory"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["DcId"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS2"].ToString();
                    dr["Extra"] = dbManager.DataReader["DC_FOR1"].ToString();
                    dr["DetId"] = dbManager.DataReader["DC_DET_ID"].ToString();
                    dr["DC_ID"] = dbManager.DataReader["DC_ID"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }



            //public void DeliveryDetails1_Select(string DeliveryId1, GridView gv)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_DELIVERY_CHALLAN_MAST,YANTRA_SO_DET,YANTRA_SO_MAST WHERE  [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
            //                                   "       YANTRA_DELIVERY_CHALLAN_DET.DC_ID = YANTRA_DELIVERY_CHALLAN_MAST.DC_ID and [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID and YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = YANTRA_SO_DET.SO_ID and YANTRA_SO_DET.SO_ID = YANTRA_SO_MAST.SO_ID  AND YANTRA_SO_DET.SO_ID =" + DeliveryId1);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);

            //    DataTable DeliveryItems = new DataTable();
            //    DataColumn col = new DataColumn();

            //    col = new DataColumn("ItemCode");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("ModelNo");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("ItemName");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("UOM");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("Quantity");
            //    DeliveryItems.Columns.Add(col);

            //    col = new DataColumn("ItemStatus");
            //    DeliveryItems.Columns.Add(col);
            //    col = new DataColumn("SerialNo");
            //    DeliveryItems.Columns.Add(col);

            //    while (dbManager.DataReader.Read())
            //    {
            //        DataRow dr = DeliveryItems.NewRow();
            //        dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
            //        dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
            //        dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
            //        dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
            //        dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();

            //        dr["SerialNo"] = dbManager.DataReader["DC_SERIAL_NO"].ToString();

            //        DeliveryItems.Rows.Add(dr);
            //    }
            //    gv.DataSource = DeliveryItems;
            //    gv.DataBind();
            //}




            public void DeliveryDetailsByCustId_Select(string Custid, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE DC_CUSTOMER_ID=" + Custid);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string DeliveryIds = "";
                while (dbManager.DataReader.Read())
                {
                    if (DeliveryIds == "") DeliveryIds = dbManager.DataReader["DC_ID"].ToString();
                    else DeliveryIds = DeliveryIds + "," + dbManager.DataReader["DC_ID"].ToString();
                }
                dbManager.DataReader.Close();
                if (DeliveryIds == "") { DeliveryIds = "0"; }
                //DeliveryId = DeliveryIds.Split(',');

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DcId");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);

                //foreach (string DcId in DeliveryId)
                //{
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID IN (" + DeliveryIds + ")");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["DCDate"] = dbManager.DataReader["DC_DATE"].ToString();
                    dr["DcId"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                //}
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetailsByCustIdSample_Select(string Custid, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE DC_ID=" + Custid);
                //_commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE DC_CUSTOMER_ID=" + Custid);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string DeliveryIds = "";
                while (dbManager.DataReader.Read())
                {
                    if (DeliveryIds == "") DeliveryIds = dbManager.DataReader["DC_ID"].ToString();
                    else DeliveryIds = DeliveryIds + "," + dbManager.DataReader["DC_ID"].ToString();
                }
                dbManager.DataReader.Close();
                if (DeliveryIds == "") { DeliveryIds = "0"; }
                //DeliveryId = DeliveryIds.Split(',');

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DcId");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("Color");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DetId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                //foreach (string DcId in DeliveryId)
                //{
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID IN (" + DeliveryIds + ") order by [YANTRA_DELIVERY_CHALLAN_DET].DC_DET_ID ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["DCDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["DcId"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS2"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["DetId"] = dbManager.DataReader["DC_DET_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                //}
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void PurchaseOrderDetailsByCustomerName_Select(Control ddlCustomerPo, String CustomerId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT SO_ID,SO_NO FROM [YANTRA_SO_MAST] WHERE SO_CUST_ID=" + CustomerId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                if (ddlCustomerPo is DropDownList)
                {
                    DropDownListBind(ddlCustomerPo as DropDownList, "SO_NO", "SO_ID");
                }
                //dbManager.Close();

            }
            public void DeliveryDetailsBySalesOrderId_Select(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE SO_ID=" + SalesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string DeliveryIds = "";
                while (dbManager.DataReader.Read())
                {
                    if (DeliveryIds == "") DeliveryIds = dbManager.DataReader["DC_ID"].ToString();
                    else DeliveryIds = DeliveryIds + "," + dbManager.DataReader["DC_ID"].ToString();
                }
                dbManager.DataReader.Close();
                if (DeliveryIds == "") { DeliveryIds = "0"; }
                //DeliveryId = DeliveryIds.Split(',');

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DcId");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Color");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Godown");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Extra");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("SubCategory");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                DeliveryItems.Columns.Add(col);
                //foreach (string DcId in DeliveryId)
                //{
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE],WH_Locations,YANTRA_LKUP_COLOR_MAST WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND YANTRA_LKUP_COLOR_MAST.COLOUR_ID = YANTRA_DELIVERY_CHALLAN_DET.COLOR_ID and YANTRA_DELIVERY_CHALLAN_DET.GODOWN_ID =WH_Locations.whLocId and  " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID IN (" + DeliveryIds + ")");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["DCDate"] = dbManager.DataReader["DC_DATE"].ToString();
                    dr["DcId"] = dbManager.DataReader["DC_ID"].ToString();
                    dr["Godown"] = dbManager.DataReader["whLocName"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS2"].ToString();
                    dr["Extra"] = dbManager.DataReader["DC_FOR1"].ToString();
                    dr["SubCategory"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();

                    DeliveryItems.Rows.Add(dr);
                }
                //}
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DeliveryDetailsBySalesOrderId_Select2(string SalesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE SO_ID=" + SalesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string DeliveryIds = "";
                while (dbManager.DataReader.Read())
                {
                    if (DeliveryIds == "") DeliveryIds = dbManager.DataReader["DC_ID"].ToString();
                    else DeliveryIds = DeliveryIds + "," + dbManager.DataReader["DC_ID"].ToString();
                }
                dbManager.DataReader.Close();
                if (DeliveryIds == "") { DeliveryIds = "0"; }
                //DeliveryId = DeliveryIds.Split(',');

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemType");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemTypeId");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("SerialNo");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("NatureofComplaint");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("RootCausedNotice");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("CorrectiveActionTaken");
                DeliveryItems.Columns.Add(col);


                //foreach (string DcId in DeliveryId)
                //{
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID IN (" + DeliveryIds + ")");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemType"] = dbManager.DataReader["IT_TYPE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();
                    dr["ItemTypeId"] = dbManager.DataReader["IT_TYPE_ID"].ToString();
                    dr["SerialNo"] = "-";
                    dr["NatureofComplaint"] = "-";
                    dr["RootCausedNotice"] = "-";
                    dr["CorrectiveActionTaken"] = "-";
                    DeliveryItems.Rows.Add(dr);
                }
                //}
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public void DeliveryDetailsBySparesOrderId_Select(string SparesOrderId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST] WHERE SPO_ID=" + SparesOrderId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string DeliveryIds = "";
                while (dbManager.DataReader.Read())
                {
                    if (DeliveryIds == "") DeliveryIds = dbManager.DataReader["DC_ID"].ToString();
                    else DeliveryIds = DeliveryIds + "," + dbManager.DataReader["DC_ID"].ToString();
                }
                dbManager.DataReader.Close();
                if (DeliveryIds == "") { DeliveryIds = "0"; }
                //DeliveryId = DeliveryIds.Split(',');

                DataTable DeliveryItems = new DataTable();
                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("UOM");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                DeliveryItems.Columns.Add(col);

                col = new DataColumn("DCDate");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DCNo");
                DeliveryItems.Columns.Add(col);
                col = new DataColumn("DcId");
                DeliveryItems.Columns.Add(col);

                //foreach (string DcId in DeliveryId)
                //{
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID AND [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID IN (" + DeliveryIds + ")");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["DC_DET_QTY"].ToString();

                    dr["DCNo"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["DCDate"] = dbManager.DataReader["DC_DATE"].ToString();
                    dr["DcId"] = dbManager.DataReader["DC_ID"].ToString();
                    DeliveryItems.Rows.Add(dr);
                }
                //}
                dbManager.DataReader.Close();
                gv.DataSource = DeliveryItems;
                gv.DataBind();
                //dbManager.Close();
            }

            public string DeliveryDetailsApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_DELIVERY_CHALLAN_MAST] SET DC_APPROVED_BY= '{0}' WHERE DC_ID='{1}'", this.DCApprovedBy, this.DCId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Delivery  Approval Details", "68");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public static void DeliveryDetailsItemTypes_Select(string DeliveryId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID),([YANTRA_LKUP_ITEM_TYPE].IT_TYPE) FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID=" + DeliveryId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IT_TYPE", "IT_TYPE_ID");
                }
            }

            public static void DeliveryDetailsItemTypes1_Select(string DeliveryId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT distinct([YANTRA_ITEM_MAST].ITEM_CODE),([YANTRA_ITEM_MAST].ITEM_MODEL_NO) FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_ITEM_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND  [YANTRA_DELIVERY_CHALLAN_DET].DC_ID=" + DeliveryId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_MODEL_NO", "ITEM_CODE");
                }
                //dbManager.Close();
            }


            public static void DeliveryDetailsItemNames_Select(string DeliveryId, string ItemTypeId, Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_DELIVERY_CHALLAN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_ITEM_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_DET].ITEM_CODE in ([YANTRA_ITEM_MAST].ITEM_CODE) AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_DELIVERY_CHALLAN_DET].DC_ID=" + DeliveryId + " AND [YANTRA_ITEM_MAST].IT_TYPE_ID=" + ItemTypeId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "ITEM_NAME", "ITEM_CODE");
                }
                //dbManager.Close();
            }
        }

        public class QRCode
        {
            public string QRCode_Id, Image, Image_Path, Item_Code, MRN_Det_Id,Item_Id,Brand,Remarks,ClientName,BrandId;
            public string  ITEM_Model_No, CHK_NO, CHK_DATE, CHK_DET_Color, COlour_NAme, CHK_DET_NetQty,PreparedBy,Updateddt,LocName,ISPrint,Qty,PrintQty;

            public QRCode() { }

            public string QRImage_Save1()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //if (IsRecordExistsQRCode("[qrcode_Image]", "Item_Id", this.Item_Id, "MRN_DET_ID", this.MRN_Det_Id) == false)
                //{
                _commandText = string.Format("INSERT INTO [QRCode_Image] SELECT ISNULL(MAX(Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' FROM [QRCode_IMAGE]", this.MRN_Det_Id, this.Item_Code, this.Image_Path, this.Image, this.Item_Id, this.CHK_DET_Color, this.PreparedBy, this.Qty, this.PrintQty, this.ISPrint, this.Updateddt, this.LocName);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                dbManager.Close();
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("QR Code Generated", "132");

                }

                //}
                //else
                //{
                //    _returnStringMessage = "QRCode  Already Exists.";
                //}
                return _returnStringMessage;
            }
            public string QRImage_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //if (IsRecordExistsQRCode("[qrcode_Image]", "Item_Id", this.Item_Id, "MRN_DET_ID", this.MRN_Det_Id) == false)
                //{
                    _commandText = string.Format("INSERT INTO [QRCode_Image] SELECT ISNULL(MAX(Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' FROM [QRCode_IMAGE]", this.MRN_Det_Id, this.Item_Code, this.Image_Path, this.Image, this.Item_Id, this.CHK_DET_Color, this.PreparedBy, this.PrintQty, this.Qty, this.ISPrint, this.Updateddt, this.LocName);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                    _returnStringMessage = string.Empty;
                    dbManager.Close();
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("QR Code Generated", "132");

                    }

                //}
                //else
                //{
                //    _returnStringMessage = "QRCode  Already Exists.";
                //}
                return _returnStringMessage;
            }

            public static string QRCodeStatus_Update(String Status, string EnqId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [QRCode_Image] SET  ISPrint='{0}' WHERE Item_Id in ({1})", Status, EnqId);
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
            public int QRItems_Select(string DETID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select Inward_New.Item_Code,ITEM_Model_No,CHK_NO,Convert(nvarchar(50),CHK_DATE)as CHK_DATE,INWARD_New.COLOUR_ID,COlour_NAme,QRCode_Image.Qty,QRCode_Image.PrintQty,QRCode_Image.ISPrint,PRODUCT_COMPANY_NAME,YANTRA_ITEM_MAST .BRAND_ID,YANTRA_ITEM_MAST.ITEM_SPEC from INWARD_New" +
                " left outer join YANTRA_CHECKING_FORMAT_DETAILS  on INWARD_New .MRN_NO =CONVERT(nvarchar(50),chk_det_ID) " +
                " left outer join YANTRA_CHECKING_FORMAT on YANTRA_CHECKING_FORMAT_DETAILS .CHK_ID =YANTRA_CHECKING_FORMAT.CHK_ID " +
                " left outer join YANTRA_LKUP_COLOR_MAST on INWARD_New .COLOUR_ID =YANTRA_LKUP_COLOR_MAST .COLOUR_ID" +
                " left outer join YANTRA_COMP_PROFILE on INWARD_New .Cp_Id =YANTRA_COMP_PROFILE .CP_ID " +
                " left outer join YANTRA_ITEM_MAST on INWARD_New .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE" +
                " left outer join YANTRA_LKUP_ITEM_TYPE on YANTRA_ITEM_MAST .IT_TYPE_ID =YANTRA_LKUP_ITEM_TYPE .IT_TYPE_ID" +
                " left outer join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID" +
                " left outer join YANTRA_LKUP_ITEM_CATEGORY on YANTRA_ITEM_MAST .IC_ID =YANTRA_LKUP_ITEM_CATEGORY .ITEM_CATEGORY_ID " +
                " left outer join YANTRA_SUPPLIER_MAST on YANTRA_CHECKING_FORMAT.CHK_MANUFACTURER_NAME =YANTRA_SUPPLIER_MAST .SUP_ID " +
                " left outer join QRCode_Image on INWARD_New .Item_ID =QRCode_Image.Item_Id  where Inward_New.item_Id ='" + DETID + "' ");
               

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    //this.MIVId = dbManager.DataReader["MIV_ID"].ToString();
                    this.Item_Code  = dbManager.DataReader["Item_Code"].ToString();

                    this.ITEM_Model_No = dbManager.DataReader["ITEM_Model_No"].ToString();
                    this.CHK_NO = dbManager.DataReader["CHK_NO"].ToString();
                    this.CHK_DATE = dbManager.DataReader["CHK_DATE"].ToString();

                    this.CHK_DET_Color = dbManager.DataReader["COLOUR_ID"].ToString();
                    this.COlour_NAme = dbManager.DataReader["COlour_NAme"].ToString();
                    this.Qty  = dbManager.DataReader["Qty"].ToString();
                    this.PrintQty = dbManager.DataReader["PrintQty"].ToString();
                    this.ISPrint = dbManager.DataReader["ISPrint"].ToString();
                    this.Brand = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    this.BrandId = dbManager.DataReader["BRAND_ID"].ToString();
                    this.Remarks = dbManager.DataReader["ITEM_SPEC"].ToString();
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

            public void QRItems_Selectgv(string DETID, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SO_DET],[YANTRA_LKUP_ITEM_TYPE] WHERE [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                //                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [YANTRA_SO_DET].SO_ID=" + SalesOrderId);

                _commandText = string.Format("select Inward_New.Item_Code,ITEM_Model_No,CHK_NO,Convert(nvarchar(50),CHK_DATE)as CHK_DATE,CHK_DET_Color,COlour_NAme,QRCode_Image.Qty,QRCode_Image.PrintQty,QRCode_Image.ISRead from INWARD_New" +
                " left outer join YANTRA_CHECKING_FORMAT_DETAILS  on INWARD_New .MRN_NO =CONVERT(nvarchar(50),chk_det_ID)" +
                " inner join YANTRA_CHECKING_FORMAT on YANTRA_CHECKING_FORMAT_DETAILS .CHK_ID =YANTRA_CHECKING_FORMAT.CHK_ID " +
                " inner join YANTRA_LKUP_COLOR_MAST on YANTRA_CHECKING_FORMAT_DETAILS .CHK_DET_COLOR =YANTRA_LKUP_COLOR_MAST .COLOUR_ID" +
                " inner join YANTRA_COMP_PROFILE on INWARD_New .Cp_Id =YANTRA_COMP_PROFILE .CP_ID " +
                " inner join YANTRA_ITEM_MAST on INWARD_New .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE" +
                " inner join YANTRA_LKUP_ITEM_TYPE on YANTRA_ITEM_MAST .IT_TYPE_ID =YANTRA_LKUP_ITEM_TYPE .IT_TYPE_ID" +
                " inner join YANTRA_LKUP_PRODUCT_COMPANY on YANTRA_ITEM_MAST .BRAND_ID =YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID" +
                " inner join YANTRA_LKUP_ITEM_CATEGORY on YANTRA_ITEM_MAST .IC_ID =YANTRA_LKUP_ITEM_CATEGORY .ITEM_CATEGORY_ID " +
                " inner join YANTRA_SUPPLIER_MAST on YANTRA_CHECKING_FORMAT.CHK_MANUFACTURER_NAME =YANTRA_SUPPLIER_MAST .SUP_ID " +
                " left outer join QRCode_Image on INWARD_New .Item_ID =QRCode_Image.Item_Id  where Inward_New.item_Id ='" + DETID + "' ");
               
                
                
                dbManager.ExecuteReader(CommandType.Text, _commandText); 

                //_commandText2 = string.Format("SELECT((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_STOCK,(select COUNT(*) from BLOCK where item_code=p.item_code and colour_id=p.COLOUR_ID) as TOTAL_BLOCK_Stock,((select COUNT(*) from inward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from outward where item_code=p.item_code and colour_id=p.colour_id)-(select COUNT(*) from block where item_code=p.item_code and colour_id=p.colour_id)) as TOTAL_AVALIABLE_STOCK from inward  p left join outward out on p.item_code=out.item_code left join block blo on p.item_code=blo.item_code where p.ITEM_CODE = " + Itemcode + " and p.COLOUR_ID = " + Color + " group by p.item_code,p.colour_id");
                //dbManager.ExecuteReader(CommandType.Text, _commandText2);
                DataTable QRItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("Item_Code");
                QRItems.Columns.Add(col);
                col = new DataColumn("ITEM_Model_No");
                QRItems.Columns.Add(col);
                col = new DataColumn("CHK_NO");
                QRItems.Columns.Add(col);
                col = new DataColumn("CHK_DATE");
                QRItems.Columns.Add(col);
                col = new DataColumn("CHK_DET_Color");
                QRItems.Columns.Add(col);
                col = new DataColumn("COlour_NAme");
                QRItems.Columns.Add(col);
                col = new DataColumn("Qty");
                QRItems.Columns.Add(col);
                col = new DataColumn("PrintQty");
                QRItems.Columns.Add(col);
                col = new DataColumn("ISRead");
                QRItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = QRItems.NewRow();
                    dr["Item_Code"] = dbManager.DataReader["Item_Code"].ToString();

                    dr["ITEM_Model_No"] = dbManager.DataReader["ITEM_Model_No"].ToString();
                    dr["CHK_NO"] = dbManager.DataReader["CHK_NO"].ToString();
                    dr["CHK_DATE"] = dbManager.DataReader["CHK_DATE"].ToString();

                    dr["CHK_DET_Color"] = dbManager.DataReader["CHK_DET_Color"].ToString();
                    dr["COlour_NAme"] = dbManager.DataReader["COlour_NAme"].ToString();
                    dr["Qty"] = dbManager.DataReader["Qty"].ToString();
                    dr["PrintQty"] = dbManager.DataReader["PrintQty"].ToString();
                    dr["ISRead"] = dbManager.DataReader["ISRead"].ToString();
                    

                    QRItems.Rows.Add(dr);
                }

                gv.DataSource = QRItems;
                gv.DataBind();
            }

        }


        // Methods For Material issues
        public class MaterialIssue
        {
            public string MIVId, MIVNo, MIVDate, WOId, MIVRemarks, MIVPreparedBy, MIVApprovedBy;
            public string MIVDetId, ItemCode, ItemName, UOM, MIVDetIssueQty, ItemBalanceQuantity;

            public MaterialIssue()
            {
            }

            public static string MaterialIssue_AutoGenCode()
            {
                string _codePrefix = "MIV/";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(MIV_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_MATERIAL_ISSUE_MAST]").ToString());
                //dbManager.Close();
                return _codePrefix + _returnIntValue;
            }

            public string MaterialIssue_Save()
            {
                this.MIVNo = MaterialIssue_AutoGenCode();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_MATERIAL_ISSUE_MAST] SELECT ISNULL(MAX(MIV_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}' FROM [YANTRA_MATERIAL_ISSUE_MAST]", this.MIVNo, this.MIVDate, this.WOId, this.MIVRemarks, this.MIVPreparedBy, this.MIVApprovedBy);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Material Issue Details", "69");
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string MaterialIssue_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_MATERIAL_ISSUE_MAST] SET MIV_DATE='{0}',WO_ID='{1}',MIV_REMARKS='{2}',MIV_PREPARED_BY='{3}',MIV_APPROVED_BY='{4}' WHERE MIV_ID='{5}'", this.MIVDate, this.WOId, this.MIVRemarks, this.MIVPreparedBy, this.MIVApprovedBy, this.MIVId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Material Issue Details", "69");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string MaterialIssue_Delete(string MIVId)
            {

                if (DeleteRecord("[YANTRA_MATERIAL_ISSUE_MAST]", "MIV_ID", MIVId) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("Material Issue Details", "69");

                }

                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }

                return _returnStringMessage;
            }

            public static void MaterialIssue_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MATERIAL_ISSUE_MAST] ORDER BY MIV_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "MIV_NO", "MIV_ID");
                }
                //dbManager.Close();
            }

            public int MaterialIssue_Select(string MIVId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [YANTRA_MATERIAL_ISSUE_MAST],[YANTRA_WO_MAST] WHERE  [YANTRA_MATERIAL_ISSUE_MAST].WO_ID=[YANTRA_WO_MAST].WO_ID AND [YANTRA_MATERIAL_ISSUE_MAST].MIV_ID='" + MIVId + "' ORDER BY [YANTRA_MATERIAL_ISSUE_MAST].MIV_ID DESC ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.MIVId = dbManager.DataReader["MIV_ID"].ToString();
                    this.MIVNo = dbManager.DataReader["MIV_NO"].ToString();
                    this.MIVDate = Convert.ToDateTime(dbManager.DataReader["MIV_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.WOId = dbManager.DataReader["WO_ID"].ToString();
                    this.MIVRemarks = dbManager.DataReader["MIV_REMARKS"].ToString();
                    this.MIVPreparedBy = dbManager.DataReader["MIV_PREPARED_BY"].ToString();
                    this.MIVApprovedBy = dbManager.DataReader["MIV_APPROVED_BY"].ToString();


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
            public string MaterialIssueDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_MATERIAL_ISSUE_DET] SELECT ISNULL(MAX(MIV_DET_ID),0)+1,{0},{1},{2} FROM [YANTRA_MATERIAL_ISSUE_DET]", this.MIVId, this.ItemCode, this.MIVDetIssueQty);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Material Issue Details", "69");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string MaterialIssueStock_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST] SET  ITEM_QTY_IN_HAND=CONVERT(BIGINT,ITEM_QTY_IN_HAND)-'{0}' WHERE ITEM_CODE='{1}'", this.MIVDetIssueQty, this.ItemCode);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Material Issue Stock Details", "69");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }


            string[] forItemsStock; string ItemsStockString;
            public int MaterialIssueDetails_Delete(string MIVId)
            {

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MATERIAL_ISSUE_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM] WHERE [YANTRA_MATERIAL_ISSUE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                          "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND [YANTRA_MATERIAL_ISSUE_DET].MIV_ID=" + MIVId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    if (ItemsStockString == null || ItemsStockString == "")
                    {
                        ItemsStockString = dbManager.DataReader["ITEM_CODE"].ToString() + "#" + Convert.ToString(Convert.ToInt64(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString()) + Convert.ToInt64(dbManager.DataReader["MIV_DET_ISSUE_QTY"].ToString()));
                    }
                    else
                    {
                        ItemsStockString = ItemsStockString + "@" + dbManager.DataReader["ITEM_CODE"].ToString() + "#" + Convert.ToString(Convert.ToInt64(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString()) + Convert.ToInt64(dbManager.DataReader["MIV_DET_ISSUE_QTY"].ToString()));
                    }
                }
                dbManager.DataReader.Close();
                forItemsStock = ItemsStockString.Split('@');
                ItemsStockString = "";
                for (int count = 0; count < forItemsStock.Length; count++)
                {
                    string[] forItemStockUpdate;
                    forItemStockUpdate = forItemsStock[count].Split('#');
                    _commandText = string.Format("UPDATE [YANTRA_ITEM_MAST]  SET  ITEM_QTY_IN_HAND={0}  WHERE ITEM_CODE={1}", forItemStockUpdate[1], forItemStockUpdate[0]);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                }
                _commandText = string.Format("DELETE FROM [YANTRA_MATERIAL_ISSUE_DET] WHERE MIV_ID={0}", MIVId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }

            public void MaterialIssueDetails_Select(string MIVId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_MATERIAL_ISSUE_DET],[YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM] WHERE [YANTRA_MATERIAL_ISSUE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND " +
                                               "[YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID  AND [YANTRA_MATERIAL_ISSUE_DET].MIV_ID=" + MIVId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable MIVItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                MIVItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                MIVItems.Columns.Add(col);
                col = new DataColumn("UOM");
                MIVItems.Columns.Add(col);
                col = new DataColumn("BalanceQty");
                MIVItems.Columns.Add(col);
                col = new DataColumn("IssueQty");
                MIVItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = MIVItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["BalanceQty"] = Convert.ToString(Convert.ToInt64(dbManager.DataReader["ITEM_QTY_IN_HAND"].ToString()) - Convert.ToInt64(dbManager.DataReader["MIV_DET_ISSUE_QTY"].ToString())).ToString();
                    dr["IssueQty"] = dbManager.DataReader["MIV_DET_ISSUE_QTY"].ToString();

                    MIVItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = MIVItems;
                gv.DataBind();
                //dbManager.Close();
            }
        }


        //Methods For Sales Invoice Form
        public class SalesInvoice
        {
            public string SIId, SINo, SIDate,RecivedBy,AuthorisedBy, SIType, DCId, DespmId, SOId, SIMissChrgs, SIDiscount, SIGrossAmt, CustId, SIRemarks, SIPreparedBy, SIApprovedBy, SPOId, DCFor, SIVAT, SICSTax, CpId, SIStatus, InvoiceNo, Unitname;
            public string SIDetId, SIDetQty, SIDetRate, SIDetVat, SIDetCst, ItemCode, ItemName, UOM, SIDetExcise, SIDcid, sicoLORID, Remarks;

            public string Get_Yet_To_Deliver(int SO_ID)
            {

                if (dbManager.Transaction == null)
                    dbManager.Open();

                string item_code = string.Empty;

                _commandText = "SELECT YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE FROM YANTRA_DELIVERY_CHALLAN_DET, YANTRA_DELIVERY_CHALLAN_MAST, YANTRA_SO_DET  " +
                    " WHERE YANTRA_SO_DET.SO_ID = " + SO_ID + "AND  " +
                    "YANTRA_DELIVERY_CHALLAN_DET.DC_ID = YANTRA_DELIVERY_CHALLAN_MAST.DC_ID AND " +
                    " YANTRA_DELIVERY_CHALLAN_MAST.SO_ID = YANTRA_SO_DET.SO_ID AND YANTRA_SO_DET.ITEM_CODE = YANTRA_DELIVERY_CHALLAN_DET.ITEM_CODE";

                dbManager.ExecuteReader(CommandType.Text, _commandText);

                while (dbManager.DataReader.Read())
                {
                    if (item_code == string.Empty)
                        item_code = dbManager.DataReader[0].ToString();
                    else
                        item_code = item_code + "," + dbManager.DataReader[0].ToString();
                }
                //dbManager.Close();

                return item_code;



            }

            public SalesInvoice()
            {
            }

            public static string SalesInvoice_AutoGenCode()
            {
                //string _codePrefix = CurrentFinancialYear() + " ";
                ////string _codePrefix = "SI/";
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT  ISNULL(MAX(CONVERT(BIGINT,REPLACE(SI_NO,LEFT(SI_NO,5),''))),0)+1 FROM [YANTRA_SALES_INVOICE_MAST]").ToString());
                //return _codePrefix + _returnIntValue;
                return SM.AutoGenMaxNo("YANTRA_SALES_INVOICE_MAST", "SI_NO");
            }
            public string SIBranchTransfer;
            public string SalesInvoice_Save()
            {
                this.SINo = SalesInvoice_AutoGenCode();
                this.SIId = AutoGenMaxId("[YANTRA_SALES_INVOICE_MAST]", "SI_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SALES_INVOICE_MAST] SELECT ISNULL(MAX(SI_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}',{15},'{16}','{17}',{18},'{19}' FROM [YANTRA_SALES_INVOICE_MAST]", this.SINo, Convert.ToDateTime(this.SIDate), this.SIType, this.DCId, this.DespmId, this.SOId, this.SIMissChrgs, this.SIDiscount, this.SIGrossAmt, this.SIRemarks, this.SIPreparedBy, this.SIApprovedBy, this.SPOId, this.SIVAT, this.SICSTax, this.CpId, this.SIStatus, this.InvoiceNo, this.Unitname, this.SIBranchTransfer);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Invoice Details", "70");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
		
		public static int ISInvoiceRaised(string EnquiryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "SELECT COUNT(*) FROM YANTRA_SALES_INVOICE_MAST  WHERE DC_ID=" + EnquiryId + "";
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                return _returnIntValue;
            }

            public string SalesInvoice_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SALES_INVOICE_MAST] SET SI_DATE='{0}',SI_TYPE='{1}',SI_MISS_CHRGS={2},SI_DISCOUNT={3},SI_GROSS_AMT={4},SI_REMARKS='{5}',SI_PREPARED_BY='{6}',SI_APPROVED_BY='{7}',SI_VAT='{8}',SI_CSTAX='{9}',CP_ID={11},SI_STATUS = '{12}',SI_Invoice = '{13}',UnitId = {14},SI_BRANCHTRANSFER='{15}',SI_NO='{16)' WHERE SI_ID={10}", this.SIDate, this.SIType, this.SIMissChrgs, this.SIDiscount, this.SIGrossAmt, this.SIRemarks, this.SIPreparedBy, this.SIApprovedBy, this.SIVAT, this.SICSTax, this.SIId, this.CpId, this.SIStatus, this.InvoiceNo, this.Unitname, this.SIBranchTransfer,this.SINo );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Sales Invoice Details", "70");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string SalesInvoice_Delete(string SalesInvoiceId)
            {

                if (DeleteRecord("[YANTRA_SALES_INVOICE_DET]", "SI_ID", SalesInvoiceId) == true)
                {
                    if (DeleteRecord("[YANTRA_SALES_INVOICE_MAST]", "SI_ID", SalesInvoiceId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Sales Invoice Details", "70");


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

            public string DCStatus_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_DELIVERY_CHALLAN_MAST] SET STATUS='Closed' WHERE DC_ID={0}", this.DCId);
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

            public int SalesInvoiceItems_Delete(string InvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SALES_INVOICE_DET] WHERE SI_DET_ID = {0}", InvoiceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                return _returnIntValue;
            }
            public static void SalesInvoice_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST] ORDER BY SI_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SI_NO", "SI_ID");
                }
                //dbManager.Close();
            }

            public int SalesInvoice_Select(string SalesInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_DESP_MODE],[YANTRA_SO_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_SALES_INVOICE_MAST].DC_ID AND [YANTRA_SALES_INVOICE_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SALES_INVOICE_MAST] .SO_ID " +
                //                            " AND [YANTRA_SALES_INVOICE_MAST].SI_ID='" + SalesInvoiceId + "' ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID DESC ");

                _commandText = "SELECT * ,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY,FORSALES.CUST_NAME AS FORSALESCUST,FORSPARES.CUST_NAME AS FORSPARESCUST  FROM [YANTRA_SALES_INVOICE_MAST] " +
                                               " inner join [YANTRA_DELIVERY_CHALLAN_MAST] on[YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_SALES_INVOICE_MAST].SI_ID='" + SalesInvoiceId + "' " +
                                               " inner join [YANTRA_LKUP_TRANS_MAST] on [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID " +
                                               " LEFT OUTER join [YANTRA_SO_MAST] on [YANTRA_SO_MAST].SO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SO_ID " +
                                               " LEFT OUTER join [YANTRA_QUOT_MAST] on [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID " +
                                               " LEFT OUTER join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID 	 " +
                                               " LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSALES on [YANTRA_ENQ_MAST].CUST_ID=FORSALES.CUST_ID  " +
                                               " LEFT OUTER join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID  " +
                                               " left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID  " +
                                               " left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_PREPARED_BY  " +
                                               " left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_APPROVED_BY " +
                                               " left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID " +
                                               " LEFT OUTER join [YANTRA_SPARES_ORDER_MAST] on [YANTRA_SPARES_ORDER_MAST].SPO_ID=[YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID " +
                                               " LEFT OUTER join [YANTRA_SPARES_QUOT_MAST] on [YANTRA_SPARES_ORDER_MAST].SPARES_QUOT_ID=[YANTRA_SPARES_QUOT_MAST].SPARES_QUOT_ID " +
                                               " LEFT OUTER join [YANTRA_COMPLAINT_REGISTER] on [YANTRA_COMPLAINT_REGISTER].CR_ID=[YANTRA_SPARES_QUOT_MAST].CR_ID 	 " +
                                               " LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSPARES on [YANTRA_COMPLAINT_REGISTER].CUST_ID=FORSPARES.CUST_ID  " +
                                               " ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC";
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SIId = dbManager.DataReader["SI_ID"].ToString();
                    this.SINo = dbManager.DataReader["SI_NO"].ToString();
                    this.SIDate = Convert.ToDateTime(dbManager.DataReader["SI_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.SIType = dbManager.DataReader["SI_TYPE"].ToString();
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    this.DCFor = dbManager.DataReader["DC_FOR"].ToString();
                    this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                    this.SPOId = dbManager.DataReader["SPO_ID"].ToString();
                    this.SIMissChrgs = dbManager.DataReader["SI_MISS_CHRGS"].ToString();
                    this.SIDiscount = dbManager.DataReader["SI_DISCOUNT"].ToString();
                    this.SIGrossAmt = dbManager.DataReader["SI_GROSS_AMT"].ToString();
                    this.SIRemarks = dbManager.DataReader["SI_REMARKS"].ToString();
                    this.SIPreparedBy = dbManager.DataReader["SI_PREPARED_BY"].ToString();
                    this.SIApprovedBy = dbManager.DataReader["SI_APPROVED_BY"].ToString();
                    this.SIVAT = dbManager.DataReader["SI_VAT"].ToString();
                    this.SICSTax = dbManager.DataReader["SI_CSTAX"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.InvoiceNo = dbManager.DataReader["SI_Invoice"].ToString();
                    this.Unitname = dbManager.DataReader["UnitId"].ToString();
                    this.SIBranchTransfer = dbManager.DataReader["SI_BRANCHTRANSFER"].ToString();

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


            public void SalesInvoice_SelectSO(string SoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT [YANTRA_SALES_INVOICE_DET].SI_ID FROM [YANTRA_SALES_INVOICE_DET],YANTRA_SALES_INVOICE_MAST,[YANTRA_SO_DET] WHERE [YANTRA_SO_DET].SO_ID=YANTRA_SALES_INVOICE_MAST.SO_Id and YANTRA_SALES_INVOICE_MAST.SI_ID=YANTRA_SALES_INVOICE_DET.SI_ID and YANTRA_SALES_INVOICE_MAST.SO_ID=" + SoId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SIId = dbManager.DataReader["SI_ID"].ToString();
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
            }

            public void SalesInvoice_SelectDelivery(string DeliveryId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT Top 1 [YANTRA_SALES_INVOICE_DET].SI_ID FROM [YANTRA_SALES_INVOICE_DET],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],YANTRA_SALES_INVOICE_MAST,[YANTRA_SO_DET] WHERE  [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID and [YANTRA_SO_DET].SO_ID=YANTRA_SALES_INVOICE_MAST.SO_Id and YANTRA_SALES_INVOICE_MAST.SI_ID=YANTRA_SALES_INVOICE_DET.SI_ID and  YANTRA_SALES_INVOICE_MAST.DC_ID=" + DeliveryId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SIId = dbManager.DataReader["SI_ID"].ToString();
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
            }




            //public void SalesInvoice_SelectDelivery(string DeliveryId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT [YANTRA_SALES_INVOICE_DET].SI_ID FROM [YANTRA_SALES_INVOICE_DET],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],YANTRA_SALES_INVOICE_MAST,[YANTRA_SO_DET] WHERE  [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID and [YANTRA_SO_DET].SO_ID=YANTRA_SALES_INVOICE_MAST.SO_Id and YANTRA_SALES_INVOICE_MAST.SI_ID=YANTRA_SALES_INVOICE_DET.SI_ID and  YANTRA_SALES_INVOICE_MAST.DC_ID=" + DeliveryId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.SIId = dbManager.DataReader["SI_ID"].ToString();
            //    }
            //}

            
            public string SalesInvoiceDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SALES_INVOICE_DET] SELECT ISNULL(MAX(SI_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},'{9}' FROM [YANTRA_SALES_INVOICE_DET]", this.SIId, this.ItemCode, this.SIDetQty, this.SIDetRate, this.SIDetVat, this.SIDetCst, this.SIDetExcise, this.SIDcid, this.sicoLORID, this.Remarks);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Invoice Details", "70");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }


            public string SalesInvoiceDC_Update(string id)
            {
                if (dbManager.Transaction == null)
                dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_DELIVERY_CHALLAN_MAST] SET STATUS = 'Closed'  WHERE DC_ID= '" + id + "'");
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Invoice DC Details", "70");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string dcdetId;
            public string DCInvoiceDtls_Update(string id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  YANTRA_DELIVERY_CHALLAN_DET SET INVOICENO = '{0}'  WHERE DC_DET_ID= '" + id + "'", this.SINo);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Invoice DC Details", "70");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }


            public int SalesInvoiceDetails_Delete(string SalesInvoiceId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SALES_INVOICE_DET] WHERE SI_ID={0}", SalesInvoiceId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }
            public void SalesInvoiceDetailsSample_Select(string SalesInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_INVOICE_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_INVOICE_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].SI_ID=[YANTRA_SALES_INVOICE_DET].SI_ID AND  [YANTRA_SALES_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_INVOICE_DET].SI_ID=" + SalesInvoiceId);
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
                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ColorId");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DCId");
                SalesInvoiceProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SI_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SI_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SI_DET_CST"].ToString();

                    dr["ColorId"] = dbManager.DataReader["SI_COLOR_ID"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SI_Description"].ToString();
                    dr["DCId"] = dbManager.DataReader["SI_DC_ID"].ToString();
                    // dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");

                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }

            public void SalesInvoiceDetails_Select(string SalesInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_INVOICE_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_INVOICE_MAST],[YANTRA_SO_MAST],[YANTRA_SO_DET],YANTRA_DELIVERY_CHALLAN_MAST WHERE YANTRA_SALES_INVOICE_DET.SI_COLOR_ID = YANTRA_SO_DET.COLOR_ID and [YANTRA_SALES_INVOICE_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SO_DET].SO_ID AND [YANTRA_SALES_INVOICE_MAST].SI_ID=[YANTRA_SALES_INVOICE_DET].SI_ID AND [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = YANTRA_SALES_INVOICE_DET.SI_DC_ID AND YANTRA_SALES_INVOICE_MAST.So_id  =" + SalesInvoiceId);
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
                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DC_NO");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("POQty");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DetId");
                SalesInvoiceProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SI_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SI_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SI_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SI_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DC_NO"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SO_DET_QTY"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SI_Description"].ToString();
                    dr["DetId"] = dbManager.DataReader["SI_DET_ID"].ToString();



                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }

            public void DCInvoiceDetails_Select(string SalesInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_INVOICE_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_INVOICE_MAST],YANTRA_DELIVERY_CHALLAN_MAST WHERE [YANTRA_SALES_INVOICE_MAST].SI_ID=[YANTRA_SALES_INVOICE_DET].SI_ID AND [YANTRA_SALES_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID and YANTRA_DELIVERY_CHALLAN_MAST.DC_ID = YANTRA_SALES_INVOICE_DET.SI_DC_ID AND YANTRA_SALES_INVOICE_MAST.[DC_ID]  =" + SalesInvoiceId);
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
                //col = new DataColumn("SPPrice");
                //SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DC_NO");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("POQty");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DetId");
                SalesInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("DetGST");
                //SalesInvoiceProducts.Columns.Add(col);
                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SI_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SI_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SI_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SI_DET_EXCISE"].ToString();
                    //dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DC_NO"] = dbManager.DataReader["DC_NO"].ToString();
                    dr["POQty"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["Remarks"] = dbManager.DataReader["SI_Description"].ToString();
                    dr["DetId"] = dbManager.DataReader["SI_DET_ID"].ToString();
                    //dr["DetGST"] = dbManager.DataReader["GST Tax"].ToString();


                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }

            public void SalesInvoiceDetailsSampleReturn_Select(string SalesInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_INVOICE_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_INVOICE_MAST] WHERE  [YANTRA_SALES_INVOICE_MAST].SI_ID=[YANTRA_SALES_INVOICE_DET].SI_ID AND [YANTRA_SALES_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_INVOICE_DET].SI_ID=" + SalesInvoiceId);
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
                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SI_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SI_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SI_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SI_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SI_DET_RATE"].ToString();

                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }


            public void SalesInvoiceDetailsReturn_Select(string SalesInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_INVOICE_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_INVOICE_MAST],[YANTRA_SO_MAST],[YANTRA_SO_DET] WHERE [YANTRA_SALES_INVOICE_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SO_DET].SO_ID AND [YANTRA_SALES_INVOICE_MAST].SI_ID=[YANTRA_SALES_INVOICE_DET].SI_ID AND [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_INVOICE_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_INVOICE_DET].SI_ID=" + SalesInvoiceId);
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
                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SI_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SO_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SO_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SI_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("dd/MM/yyyy");

                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }

            public static void SalesInvoiceForPayments_Select(Control ControlForBind, string SalesOrderId, string SaveButtonText)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED WHERE PR_PAYMENT_STATUS <> 'Cleared') ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID NOT IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED) ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID=" + SalesOrderId + " ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "SI_NO", "SI_ID");
                    }
                }
                //dbManager.Close();
            }

            public static void SalesInvoiceForSparePayments_Select(Control ControlForBind, string SalesOrderId, string SaveButtonText)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                if (SaveButtonText == "Save")
                {
                    (ControlForBind as DropDownList).Enabled = true;
                    (ControlForBind as DropDownList).Items.Clear();
                    (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                    _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SPARES_ORDER_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID=[YANTRA_SPARES_ORDER_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED WHERE PR_PAYMENT_STATUS <> 'Cleared') ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();

                    _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SPARES_ORDER_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID=[YANTRA_SPARES_ORDER_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID NOT IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED) ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    while (dbManager.DataReader.Read())
                    {
                        (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
                    }
                    dbManager.DataReader.Close();
                }
                else if (SaveButtonText == "Update")
                {
                    (ControlForBind as DropDownList).Enabled = false;
                    _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SPARES_ORDER_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID=[YANTRA_SPARES_ORDER_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=" + SalesOrderId + " ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
                    dbManager.ExecuteReader(CommandType.Text, _commandText);
                    if (ControlForBind is DropDownList)
                    {
                        DropDownListBind(ControlForBind as DropDownList, "SI_NO", "SI_ID");
                    }
                }
                //dbManager.Close();
            }

            public string SalesInvoiceApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_SALES_INVOICE_MAST] SET SI_APPROVED_BY={0}  WHERE SI_ID={1}", this.SIApprovedBy, this.SIId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Invoice Approve Details", "70");
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

        }

        //Methods For Sales Return Form
        public class SalesReturn
        {
            public string SRId, SRNo, SRDate, DCId, SOId, SRMissChrgs, SRDiscount, SRGrossAmt, CustId, SRRemarks, SRPreparedBy, SRApprovedBy, RecivedBy, AuthorisedBy, DCFor, SRVAT, SRCSTax, CPid, SRaftermonth, SIINVOICEID;
            public string SRDetId, SRDetQty, SRDetRate, SRDetVat, SRDetCst, ItemCode, ItemName, UOM, SRDetExcise;

            public SalesReturn()
            {
            }
            public static string SalesReturnNote_AutoGenCode()
            {
                return SM.AutoGenMaxNo("returnnote_tbl", "SR_NO");
            }
            public static string SalesReturn_AutoGenCode()
            {
                return SM.AutoGenMaxNo("YANTRA_SALES_RETURN_MAST", "SR_NO");
            }
           

            public string Return_Save()
            {
                this.SRNo = SalesReturnNote_AutoGenCode();
                this.SRId = AutoGenMaxId("[ReturnNote_tbl]", "SR_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Insert into returnnote_tbl select ISNULL(MAX(SR_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12} FROM returnnote_tbl ", this.SRNo, this.SRDate, this.CustId, this.DCId, this.SOId, this.SRPreparedBy, this.SRApprovedBy, this.RecivedBy, this.AuthorisedBy, this.CPid, this.SRRemarks, this.SRGrossAmt, this.SRaftermonth);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Sales Invoice Details", "70");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string SalesReturn_Save()
            {
                this.SRNo = SalesReturn_AutoGenCode();
                this.SRId = AutoGenMaxId("[YANTRA_SALES_RETURN_MAST]", "SR_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SALES_RETURN_MAST] SELECT ISNULL(MAX(SR_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}',{14} FROM [YANTRA_SALES_RETURN_MAST]", this.SRNo, Convert.ToDateTime(this.SRDate), this.DCId, this.SOId, this.SRMissChrgs, this.SRDiscount, this.SRGrossAmt, this.SRRemarks, this.SRPreparedBy, this.SRApprovedBy, this.SRVAT, this.SRCSTax, this.CPid, this.SRaftermonth, this.SIINVOICEID);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Return Details", "71");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string SalesReturn_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_SALES_RETURN_MAST] SET SR_DATE='{0}',SI_MISS_CHRGS='{1}',SI_DISCOUNT='{2}',SI_GROSS_AMT='{3}',SI_REMARKS='{4}',SI_PREPARED_BY='{5}',SI_APPROVED_BY='{6}',SI_VAT='{7}',SI_CSTAX='{8}',CP_ID = {9},SR_AFTERONE_MONTH = '{10}',SI_INVOICEID={11} WHERE SR_ID={12}", this.SRDate, this.SRMissChrgs, this.SRDiscount, this.SRGrossAmt, this.SRRemarks, this.SRPreparedBy, this.SRApprovedBy, this.SRVAT, this.SRCSTax, this.CPid, this.SRaftermonth, this.SIINVOICEID, this.SRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Sales Return Details", "71");
                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string SalesReturn_Delete(string SalesReturnId)
            {

                if (DeleteRecord("[YANTRA_SALES_RETURN_DET]", "SR_ID", SalesReturnId) == true)
                {
                    if (DeleteRecord("[YANTRA_SALES_RETURN_MAST]", "SR_ID", SalesReturnId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Sales Return Details", "71");

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

            public static void SalesReturn_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_SALES_RETURN_MAST] ORDER BY SR_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "SR_NO", "SR_ID");
                }
                //dbManager.Close();
            }

            public int SalesReturn_Select(string SalesReturnId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_DESP_MODE],[YANTRA_SO_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_SALES_INVOICE_MAST].DC_ID AND [YANTRA_SALES_INVOICE_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SALES_INVOICE_MAST] .SO_ID " +
                //                            " AND [YANTRA_SALES_INVOICE_MAST].SI_ID='" + SalesInvoiceId + "' ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID DESC ");

                _commandText = "SELECT * ,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY  FROM [YANTRA_SALES_RETURN_MAST] " +
                                 " inner join [YANTRA_DELIVERY_CHALLAN_MAST] on[YANTRA_SALES_RETURN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_SALES_RETURN_MAST].SR_ID='" + SalesReturnId + "' " +
                                               " inner join [YANTRA_LKUP_TRANS_MAST] on [YANTRA_DELIVERY_CHALLAN_MAST].TRANS_ID=[YANTRA_LKUP_TRANS_MAST].TRANS_ID " +
                                             " inner join [YANTRA_CUSTOMER_MAST] on YANTRA_DELIVERY_CHALLAN_MAST.DC_CUSTOMER_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                                               " left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_PREPARED_BY  " +
                                               " left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_APPROVED_BY " +
                    //" LEFT OUTER join [YANTRA_CUSTOMER_MAST] FORSPARES on [YANTRA_COMPLAINT_REGISTER].CUST_ID=FORSPARES.CUST_ID  " +
                                               " ORDER BY [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID DESC";
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SRId = dbManager.DataReader["SR_ID"].ToString();
                    this.SRNo = dbManager.DataReader["SR_NO"].ToString();
                    this.SRDate = Convert.ToDateTime(dbManager.DataReader["SR_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.SIINVOICEID = dbManager.DataReader["SI_INVOICEID"].ToString();
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    //this.DCFor = dbManager.DataReader["DC_FOR"].ToString();
                    //this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();

                    this.SRMissChrgs = dbManager.DataReader["SI_MISS_CHRGS"].ToString();
                    this.SRDiscount = dbManager.DataReader["SI_DISCOUNT"].ToString();
                    this.SRGrossAmt = dbManager.DataReader["SI_GROSS_AMT"].ToString();
                    this.SRRemarks = dbManager.DataReader["SI_REMARKS"].ToString();
                    this.SRPreparedBy = dbManager.DataReader["SI_PREPARED_BY"].ToString();
                    this.SRApprovedBy = dbManager.DataReader["SI_APPROVED_BY"].ToString();
                    this.SRVAT = dbManager.DataReader["SI_VAT"].ToString();
                    this.SRCSTax = dbManager.DataReader["SI_CSTAX"].ToString();
                    this.SRaftermonth = dbManager.DataReader["SR_AFTERONE_MONTH"].ToString();

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

            public int ReturnNote_Select(string SRID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = "Select * from [returnnote_tbl] inner join YANTRA_DELIVERY_CHALLAN_MAST on ReturnNote_tbl .DC_Id =YANTRA_DELIVERY_CHALLAN_MAST .DC_ID inner join YANTRA_CUSTOMER_MAST on ReturnNote_tbl .Cust_ID =YANTRA_CUSTOMER_MAST .CUST_ID where ReturnNote_tbl.SR_ID= '" + SRID + "'";
               
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                
                if (dbManager.DataReader.Read())
                 {
                     this.SRId = dbManager.DataReader["SR_ID"].ToString();
                     this.SRNo = dbManager.DataReader["SR_NO"].ToString();
                     this.SRDate = Convert.ToDateTime(dbManager.DataReader["SR_DATE"].ToString()).ToString("MM/dd/yyyy");
                     this.DCId = dbManager.DataReader["DC_ID"].ToString();
                     //this.DCFor = dbManager.DataReader["DC_FOR"].ToString();
                     //this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                     this.SOId = dbManager.DataReader["SO_ID"].ToString();
                     this.CustId = dbManager.DataReader["CUST_ID"].ToString();
                     this.SRGrossAmt = dbManager.DataReader["SRGrossAmt"].ToString();
                     this.SRRemarks = dbManager.DataReader["Reason"].ToString();
                     this.SRPreparedBy = dbManager.DataReader["Prepared_By"].ToString();
                     this.SRApprovedBy = dbManager.DataReader["Approved_By"].ToString();
                     this.RecivedBy = dbManager.DataReader["Received_by"].ToString();
                     this.AuthorisedBy = dbManager.DataReader["Authorised_By"].ToString();
                     this.SRaftermonth = dbManager.DataReader["SRAfterMonth"].ToString();

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
            public int SalesReturn_SelectOri(string SalesReturnId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_LKUP_DESP_MODE],[YANTRA_SO_MAST] WHERE [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_SALES_INVOICE_MAST].DC_ID AND [YANTRA_SALES_INVOICE_MAST].DESPM_ID=[YANTRA_LKUP_DESP_MODE].DESPM_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SALES_INVOICE_MAST] .SO_ID " +
                //                            " AND [YANTRA_SALES_INVOICE_MAST].SI_ID='" + SalesInvoiceId + "' ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID DESC ");

                _commandText = " SELECT * ,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY from" +
                   " YANTRA_SALES_RETURN_MAST " +
                    " inner join [YANTRA_DELIVERY_CHALLAN_MAST] on YANTRA_SALES_RETURN_MAST.DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID " +
                    "inner join [YANTRA_SO_MAST] on [YANTRA_SO_MAST].SO_ID=YANTRA_SALES_RETURN_MAST.SO_ID " +
                "	inner join [YANTRA_QUOT_MAST] on [YANTRA_SO_MAST].QUOT_ID=[YANTRA_QUOT_MAST].QUOT_ID " +
                "	inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID " +

                "	inner join [YANTRA_CUSTOMER_MAST] on YANTRA_ENQ_MAST.CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID " +
                "	inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID " +
                "	left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID " +
                "	left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=YANTRA_SALES_RETURN_MAST.SI_PREPARED_BY " +
                "	left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=YANTRA_SALES_RETURN_MAST.SI_APPROVED_BY " +
                "	left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID " +
                 "        where  YANTRA_SALES_RETURN_MAST.SO_ID != '0' aND [YANTRA_SALES_RETURN_MAST].SR_ID='" + SalesReturnId + "' ";


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.SRId = dbManager.DataReader["SR_ID"].ToString();
                    this.SRNo = dbManager.DataReader["SR_NO"].ToString();
                    this.SRDate = Convert.ToDateTime(dbManager.DataReader["SR_DATE"].ToString()).ToString("MM/dd/yyyy");
                    this.SIINVOICEID = dbManager.DataReader["SI_INVOICEID"].ToString();
                    this.DCId = dbManager.DataReader["DC_ID"].ToString();
                    //this.DCFor = dbManager.DataReader["DC_FOR"].ToString();
                    //this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();
                    this.SOId = dbManager.DataReader["SO_ID"].ToString();
                    this.CustId = dbManager.DataReader["CUST_ID"].ToString();

                    this.SRMissChrgs = dbManager.DataReader["SI_MISS_CHRGS"].ToString();
                    this.SRDiscount = dbManager.DataReader["SI_DISCOUNT"].ToString();
                    this.SRGrossAmt = dbManager.DataReader["SI_GROSS_AMT"].ToString();
                    this.SRRemarks = dbManager.DataReader["SI_REMARKS"].ToString();
                    this.SRPreparedBy = dbManager.DataReader["SI_PREPARED_BY"].ToString();
                    this.SRApprovedBy = dbManager.DataReader["SI_APPROVED_BY"].ToString();
                    this.SRVAT = dbManager.DataReader["SI_VAT"].ToString();
                    this.SRCSTax = dbManager.DataReader["SI_CSTAX"].ToString();
                    this.SRaftermonth = dbManager.DataReader["SR_AFTERONE_MONTH"].ToString();

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

            //public void SalesInvoice_SelectSO(string SoId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT [YANTRA_SALES_INVOICE_DET].SI_ID FROM [YANTRA_SALES_INVOICE_DET],YANTRA_SALES_INVOICE_MAST,[YANTRA_SO_DET] WHERE [YANTRA_SO_DET].SO_ID=YANTRA_SALES_INVOICE_MAST.SO_Id and YANTRA_SALES_INVOICE_MAST.SI_ID=YANTRA_SALES_INVOICE_DET.SI_ID and YANTRA_SALES_INVOICE_MAST.SO_ID=" + SoId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.SIId = dbManager.DataReader["SI_ID"].ToString();
            //    }
            //}

            //public void SalesInvoice_SelectDelivery(string DeliveryId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT [YANTRA_SALES_INVOICE_DET].SI_ID FROM [YANTRA_SALES_INVOICE_DET],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],YANTRA_SALES_INVOICE_MAST,[YANTRA_SO_DET] WHERE  [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID and [YANTRA_SO_DET].SO_ID=YANTRA_SALES_INVOICE_MAST.SO_Id and YANTRA_SALES_INVOICE_MAST.SI_ID=YANTRA_SALES_INVOICE_DET.SI_ID and  YANTRA_SALES_INVOICE_MAST.DC_ID=" + DeliveryId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.SIId = dbManager.DataReader["SI_ID"].ToString();
            //    }
            //}

            //public void SalesInvoice_SelectDelivery(string DeliveryId)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    _commandText = string.Format("SELECT [YANTRA_SALES_INVOICE_DET].SI_ID FROM [YANTRA_SALES_INVOICE_DET],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_DELIVERY_CHALLAN_DET],YANTRA_SALES_INVOICE_MAST,[YANTRA_SO_DET] WHERE  [YANTRA_DELIVERY_CHALLAN_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_DET].DC_ID and [YANTRA_SO_DET].SO_ID=YANTRA_SALES_INVOICE_MAST.SO_Id and YANTRA_SALES_INVOICE_MAST.SI_ID=YANTRA_SALES_INVOICE_DET.SI_ID and  YANTRA_SALES_INVOICE_MAST.DC_ID=" + DeliveryId);
            //    dbManager.ExecuteReader(CommandType.Text, _commandText);
            //    if (dbManager.DataReader.Read())
            //    {
            //        this.SIId = dbManager.DataReader["SI_ID"].ToString();
            //    }
            //}

            public string ReturnDet_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [ReturnNote_Det_Tbl] SELECT ISNULL(MAX(SR_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}' FROM [ReturnNote_Det_Tbl]", this.SRId, this.ItemCode, this.SRDetQty, this.SRDetRate, this.SRDetVat, this.SRDetCst, this.SRDetExcise);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    //log.add_Insert("Sales Return Details", "71");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string SalesReturnDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_SALES_RETURN_DET] SELECT ISNULL(MAX(SR_DET_ID),0)+1,{0},{1},'{2}','{3}','{4}','{5}','{6}' FROM [YANTRA_SALES_RETURN_DET]", this.SRId, this.ItemCode, this.SRDetQty, this.SRDetRate, this.SRDetVat, this.SRDetCst, this.SRDetExcise);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Sales Return Details", "71");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public int SalesReturnDetails_Delete(string SalesReturnId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_SALES_RETURN_DET] WHERE SR_ID={0}", SalesReturnId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();
                return _returnIntValue;
            }

            public void ReturnNoteDet_Select(string SalesReturnId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST],[YANTRA_SO_MAST],[YANTRA_SO_DET] WHERE [YANTRA_SALES_RETURN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SO_DET].SO_ID AND [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID=" + SalesReturnId);
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST] WHERE [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID=" + SalesReturnId);
                _commandText = string.Format("SELECT *,ReturnNote_tbl.DC_Id as [DC No],ReturnNote_Det_Tbl .Item_Code as ItemCode,ITEM_MODEL_NO as ModelNo,YANTRA_LKUP_UOM .UOM_SHORT_DESC as UOM,ITEM_NAME as ItemName,SR_DET_Qty as Quantity,ITEM_SPEC as ItemRemarks FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[ReturnNote_Det_Tbl],[YANTRA_LKUP_ITEM_TYPE],[ReturnNote_tbl] WHERE [ReturnNote_tbl].SR_ID=[ReturnNote_Det_Tbl].SR_ID AND [ReturnNote_Det_Tbl].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [ReturnNote_Det_Tbl].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID  AND [ReturnNote_Det_Tbl].SR_ID= {0} order by [ReturnNote_Det_Tbl].SR_DET_ID ", SalesReturnId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("DC No");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemCode");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Remarks");
                SalesInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("Excise");
                //SalesInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("SPPrice");
                //SalesInvoiceProducts.Columns.Add(col);
                //col = new DataColumn("DeliveryDate");
                //SalesInvoiceProducts.Columns.Add(col);

                //col = new DataColumn("ModelNo");
                //SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["DC No"] = dbManager.DataReader["DC No"].ToString();
                    dr["ItemCode"] = dbManager.DataReader["ItemCode"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ModelNo"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ItemName"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM"].ToString();
                    dr["Quantity"] = dbManager.DataReader["Quantity"].ToString();
                    dr["Remarks"] = dbManager.DataReader["ItemRemarks"].ToString();
                    //dr["Excise"] = dbManager.DataReader["SR_DET_EXCISE"].ToString();
                    //dr["SPPrice"] = dbManager.DataReader["SR_DET_RATE"].ToString();
                    //dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["dt_added"].ToString()).ToString("MM/dd/yyyy");

                    //dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
            }
            public void SalesReturnDetails_Select(string SalesReturnId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST],[YANTRA_SO_MAST],[YANTRA_SO_DET] WHERE [YANTRA_SALES_RETURN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SO_DET].SO_ID AND [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID=" + SalesReturnId);
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST] WHERE [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID=" + SalesReturnId);
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST] WHERE [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID= {0} order by [YANTRA_SALES_RETURN_DET].SR_DET_ID ", SalesReturnId);
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
                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SR_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SR_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SR_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SR_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SR_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SR_DET_RATE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["dt_added"].ToString()).ToString("MM/dd/yyyy");

                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }

            public void SalesReturnDetailsSO_Select(string SalesReturnId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST],[YANTRA_SO_MAST],[YANTRA_SO_DET] WHERE [YANTRA_SALES_RETURN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SO_DET].SO_ID AND [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID=" + SalesReturnId);
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
                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SR_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SR_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SR_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SR_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SR_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("MM/dd/yyyy");

                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }


            public void SalesReturnDetailsSO1_Select(string SalesReturnId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST],[YANTRA_SO_MAST],[YANTRA_SO_DET] WHERE [YANTRA_SALES_RETURN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID= [YANTRA_SO_DET].SO_ID AND [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SO_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_MAST].SO_ID=" + SalesReturnId);
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
                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DeliveryDate");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SR_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SR_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SR_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SR_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SR_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["SO_DET_PRICE"].ToString();
                    dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["SO_DET_DELIVERY_DATE"].ToString()).ToString("MM/dd/yyyy");

                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }





            public void SalesReturnDetailsSample_Select(string SalesReturnId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST] WHERE [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID= {0} order by [YANTRA_SALES_RETURN_DET].SR_DET_ID ", SalesReturnId);
                //_commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_SALES_RETURN_DET],[YANTRA_LKUP_ITEM_TYPE],[YANTRA_SALES_RETURN_MAST] WHERE [YANTRA_SALES_RETURN_MAST].SR_ID=[YANTRA_SALES_RETURN_DET].SR_ID AND [YANTRA_SALES_RETURN_DET].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND  [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID AND [YANTRA_SALES_RETURN_DET].SR_ID=" + SalesReturnId);
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


                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["SR_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["SR_DET_RATE"].ToString();
                    dr["VAT"] = dbManager.DataReader["SR_DET_VAT"].ToString();
                    dr["Cst"] = dbManager.DataReader["SR_DET_CST"].ToString();
                    dr["Excise"] = dbManager.DataReader["SR_DET_EXCISE"].ToString();


                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }





            //public static void SalesInvoiceForPayments_Select(Control ControlForBind, string SalesOrderId, string SaveButtonText)
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    if (SaveButtonText == "Save")
            //    {
            //        (ControlForBind as DropDownList).Enabled = true;
            //        (ControlForBind as DropDownList).Items.Clear();
            //        (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
            //        _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED WHERE PR_PAYMENT_STATUS <> 'Cleared') ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
            //        dbManager.ExecuteReader(CommandType.Text, _commandText);
            //        while (dbManager.DataReader.Read())
            //        {
            //            (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
            //        }
            //        dbManager.DataReader.Close();

            //        _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID NOT IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED) ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
            //        dbManager.ExecuteReader(CommandType.Text, _commandText);
            //        while (dbManager.DataReader.Read())
            //        {
            //            (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
            //        }
            //        dbManager.DataReader.Close();
            //    }
            //    else if (SaveButtonText == "Update")
            //    {
            //        (ControlForBind as DropDownList).Enabled = false;
            //        _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SO_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID AND [YANTRA_SO_MAST].SO_ID=" + SalesOrderId + " ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
            //        dbManager.ExecuteReader(CommandType.Text, _commandText);
            //        if (ControlForBind is DropDownList)
            //        {
            //            DropDownListBind(ControlForBind as DropDownList, "SI_NO", "SI_ID");
            //        }
            //    }
            //}

            //public static void SalesReturnForSparePayments_Select(Control ControlForBind, string SalesOrderId, string SaveButtonText)
            //     {
            //         if (dbManager.Transaction == null)
            //             dbManager.Open();
            //         if (SaveButtonText == "Save")
            //         {
            //             (ControlForBind as DropDownList).Enabled = true;
            //             (ControlForBind as DropDownList).Items.Clear();
            //             (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
            //             _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SPARES_ORDER_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID=[YANTRA_SPARES_ORDER_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED WHERE PR_PAYMENT_STATUS <> 'Cleared') ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
            //             dbManager.ExecuteReader(CommandType.Text, _commandText);
            //             while (dbManager.DataReader.Read())
            //             {
            //                 (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
            //             }
            //             dbManager.DataReader.Close();

            //             _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SPARES_ORDER_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID=[YANTRA_SPARES_ORDER_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=" + SalesOrderId + " AND [YANTRA_SALES_INVOICE_MAST].SI_ID NOT IN (SELECT SI_ID FROM YANTRA_PAYMENTS_RECEIVED) ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
            //             dbManager.ExecuteReader(CommandType.Text, _commandText);
            //             while (dbManager.DataReader.Read())
            //             {
            //                 (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["SI_NO"].ToString(), dbManager.DataReader["SI_ID"].ToString()));
            //             }
            //             dbManager.DataReader.Close();
            //         }
            //         else if (SaveButtonText == "Update")
            //         {
            //             (ControlForBind as DropDownList).Enabled = false;
            //             _commandText = string.Format("SELECT * FROM [YANTRA_SALES_INVOICE_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_SPARES_ORDER_MAST] WHERE [YANTRA_SALES_INVOICE_MAST].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID AND [YANTRA_DELIVERY_CHALLAN_MAST].SPO_ID=[YANTRA_SPARES_ORDER_MAST].SPO_ID AND [YANTRA_SPARES_ORDER_MAST].SPO_ID=" + SalesOrderId + " ORDER BY [YANTRA_SALES_INVOICE_MAST].SI_ID");
            //             dbManager.ExecuteReader(CommandType.Text, _commandText);
            //             if (ControlForBind is DropDownList)
            //             {
            //                 DropDownListBind(ControlForBind as DropDownList, "SI_NO", "SI_ID");
            //             }
            //         }
            //     }

            public string SalesReturnApprove_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE  [YANTRA_SALES_RETURN_MAST] SET SI_APPROVED_BY={0}  WHERE SR_ID='{1}'", this.SRApprovedBy, this.SRId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Status Updated Successfully";
                    log.add_Update("Sales Return Approve Details", "71");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

        }



        //Methods for Internal Indent

        public class Internalindent
        {
            public string IndId, Indno, Inddate, from, to, preparedby, cpid;
            public string Inddetid, itemcode, colorid, clientname, qty, description, brandid;
            public Internalindent()
            {
            }
            public static string InternalIndent_AutoGenCode()
            {
                return SM.AutoGenMaxNo("INTERNAL_INDENT", "IND_NO");
            }

            public string InternalIndent_Save()
            {
                this.Indno = InternalIndent_AutoGenCode();
                this.IndId = AutoGenMaxId("[INTERNAL_INDENT]", "INT_INDID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [INTERNAL_INDENT] VALUES({0},'{1}','{2}',{3},{4},{5},'{6}')",
                                                                                           this.IndId, this.Indno, this.Inddate, this.from, this.to, this.preparedby, this.cpid);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("InternalIndent", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string Remarks;
            public string InternalIndentDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [INTERNAL_INDENT_DETAILS] SELECT ISNULL(MAX(INT_IND_DETID),0)+1,{0},{1},{2},'{3}',{4},'{5}','{6}','{7}' FROM [INTERNAL_INDENT_DETAILS]", this.IndId, this.itemcode, this.colorid, this.clientname, this.qty, this.description, this.brandid, this.Remarks);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Internal INdent Details", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string InternalIndent_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [INTERNAL_INDENT] SET IND_DATE='{0}',IND_FROM={1},IND_TO={2},IND_PREPAREDBY={3} WHERE INT_INDID={4}", this.Inddate, this.from, this.to, this.preparedby, this.IndId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("GRN Details", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }

            public string InternalIndent_Delete(string Indid)
            {
                if (DeleteRecord("[INTERNAL_INDENT_DETAILS]", "INT_INDID", Indid) == true)
                {
                    if (DeleteRecord("[INTERNAL_INDENT]", "INT_INDID", Indid) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Internalindent Details", "61");

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

            public int InternalIndent_Select(string Indid)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("SELECT * FROM [INTERNAL_INDENT] where [INTERNAL_INDENT].INT_INDID='" + Indid + "' ORDER BY [INTERNAL_INDENT].INT_INDID DESC ");

                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.IndId = dbManager.DataReader["INT_INDID"].ToString();
                    this.Indno = dbManager.DataReader["IND_NO"].ToString();
                    this.Inddate = Convert.ToDateTime(dbManager.DataReader["IND_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.from = dbManager.DataReader["IND_FROM"].ToString();
                    this.to = dbManager.DataReader["IND_TO"].ToString();
                    this.preparedby = dbManager.DataReader["IND_PREPAREDBY"].ToString();

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

            public void InternalindentDetails_Select(string indid, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [INTERNAL_INDENT_DETAILS],[INTERNAL_INDENT],[YANTRA_ITEM_MAST],[YANTRA_LKUP_PRODUCT_COMPANY],YANTRA_LKUP_COLOR_MAST WHERE [INTERNAL_INDENT_DETAILS].INT_INDID=[INTERNAL_INDENT].INT_INDID AND [INTERNAL_INDENT_DETAILS].ITEM_CODE=[YANTRA_ITEM_MAST].ITEM_CODE AND [INTERNAL_INDENT_DETAILS].COLOR_ID=[YANTRA_LKUP_COLOR_MAST].COLOUR_ID  AND " +
                                               "[INTERNAL_INDENT_DETAILS].BRAND_ID=[YANTRA_LKUP_PRODUCT_COMPANY].PRODUCT_COMPANY_ID AND [INTERNAL_INDENT_DETAILS].INT_INDID='" + indid + "' order by INTERNAL_INDENT_DETAILS.INT_IND_DETID desc ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);


                DataTable GRNItems = new DataTable();
                DataColumn col = new DataColumn();


                col = new DataColumn("Itemcode");
                GRNItems.Columns.Add(col);
                col = new DataColumn("Brand");
                GRNItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                GRNItems.Columns.Add(col);
                col = new DataColumn("Color");
                GRNItems.Columns.Add(col);
                col = new DataColumn("Qty");
                GRNItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                GRNItems.Columns.Add(col);
                col = new DataColumn("ClientName");
                GRNItems.Columns.Add(col);
                col = new DataColumn("BrandId");
                GRNItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                GRNItems.Columns.Add(col);
                col = new DataColumn("Remark");
                GRNItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = GRNItems.NewRow();


                    dr["Itemcode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Qty"] = dbManager.DataReader["QTY"].ToString();
                    dr["Remarks"] = dbManager.DataReader["DESCRIPTION"].ToString();
                    dr["ClientName"] = dbManager.DataReader["CLINET_NAME"].ToString();
                    dr["BrandId"] = dbManager.DataReader["BRAND_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Remark"] = dbManager.DataReader["REMARKS"].ToString();


                    GRNItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = GRNItems;
                gv.DataBind();
                //dbManager.Close();
            }


            public static void Internalindent_select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select INT_INDID,IND_NO from INTERNAL_INDENT order by INT_INDID desc ");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "IND_NO", "INT_INDID");
                }
                //dbManager.Close();
            }
            public static void Location_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select b.wh_id as locid ,a.locname+','+b.whname as Locationname from dbo.location_tbl as a inner join dbo.warehouse_tbl as b on a.locid=b.locid");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Locationname", "locid");
                }
                //dbManager.Close();
            }

            public static void Godown_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [warehouse_tbl] ORDER BY wh_id");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "whname", "wh_id");
                }
                //dbManager.Close();
            }


            public string InternalIndentDetails_Delete(string Indid)
            {
                if (DeleteRecord("[INTERNAL_INDENT_DETAILS]", "INT_INDID", Indid) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                return _returnStringMessage;
            }



        }

        public class ProformaInvoice
        {
            public string PIID, PINO, PIDate, PIType, CUSTID, DespmId, SOID, PIMissCharges, PIDisc, PIGross, PIRemarks, PIPreparedBy, PIApprovedBy, PackingCharges, TransportCharges, Fright, PaymentTerms, CST, TIN, AdvanceAmount, SPOID, PIVAT, PICST, PIInclude, CPID, PISTATUS, PIInvoice, UnitId, OtherSpec, SplDisc;
            public string PI_Total;
            public string PIDetId, GSTDetRate, ItemCode, PIDetQty, PIDetRate, PIDetDisc, PIDetSplAmt, PIDetUnitPrice, PIDetVat, PIDetCst, PIDetExcise, PISOID, PIColiId, PIDescrption, InvoiceNo, PIGST;
            public string SoNo, Others, SoDate, QuotId, SoId, CustName, CustAddress, CustContactPerson, CustPhone, CustMobile, CustUnitId, CustUnitAdd, GSTType,Term_id,TermSelectId,TermTitle;
            public ProformaInvoice()
            {

            }
            public static string PI_AutoGenCode()
            {
                return SM.AutoGenMaxNo("YANTRA_PI_MAST", "PI_NO");
            }
            public void ProformaInvoice_SelectSO(string SoId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("Select YANTRA_PI_DET.PI_ID FROM YANTRA_PI_DET,YANTRA_PI_MAST,YANTRA_SO_DET WHERE YANTRA_SO_DET.SO_ID=YANTRA_PI_MAST.SO_ID AND YANTRA_PI_MAST.PI_ID=YANTRA_PI_DET.PI_ID AND YANTRA_PI_MAST .SO_ID = " + SoId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PIID = dbManager.DataReader["PI_ID"].ToString();
                }
                dbManager.DataReader.Close();
                //dbManager.Close();
            }

            public DataTable PITerms_Select(int PiId)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("Term_Id");
                dtable.Columns.Add(dcol);
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM Terms_Conditions_Selected WHERE  Others={0}", PiId);
                //_commandText = string.Format("SELECT * FROM YANTRA_ITEM_COLOR_DETAILS,YANTRA_LKUP_COLOR_MAST WHERE  YANTRA_ITEM_COLOR_DETAILS.COLOR_ID = YANTRA_LKUP_COLOR_MAST.COLOUR_ID and  YANTRA_ITEM_COLOR_DETAILS.ITEM_CODE={0}", ItemCode);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    drow["Term_Id"] = dbManager.DataReader[1].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
                //dbManager.Close();
                return dtable;
            }
            public int ProformaInvoice_Select(string PiID)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_PI_MAST,YANTRA_SO_MAST,YANTRA_CUSTOMER_MAST where YANTRA_PI_MAST .SO_ID =YANTRA_SO_MAST .SO_ID and YANTRA_PI_MAST.PI_CUST_ID=YANTRA_CUSTOMER_MAST .CUST_ID  and YANTRA_PI_MAST.PI_ID='" + PiID + "'  order BY YANTRA_PI_MAST .PI_ID desc");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.PIID = dbManager.DataReader["PI_ID"].ToString();
                    this.PINO = dbManager.DataReader["PI_NO"].ToString();
                    this.PIDate = Convert.ToDateTime(dbManager.DataReader["PI_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.PIType = dbManager.DataReader["PI_TYPE"].ToString();
                    this.SOID = dbManager.DataReader["SO_ID"].ToString();
                    this.PIMissCharges = dbManager.DataReader["PI_MISS_CHARGES"].ToString();
                    this.PIDisc = dbManager.DataReader["PI_DISCOUNT"].ToString();
                    this.PIGross = dbManager.DataReader["PI_GROSS_AMT"].ToString();
                    this.PIRemarks = dbManager.DataReader["PI_REMARKS"].ToString();
                    this.PIPreparedBy = dbManager.DataReader["PI_PREPARED_BY"].ToString();

                    this.PIGST = dbManager.DataReader["GST_RATE"].ToString();
                    //this.PICST = dbManager.DataReader["PI_CSTAX"].ToString();
                    //this.PIInclude = dbManager.DataReader["PI_INCLUDE"].ToString();
                    this.PackingCharges = dbManager.DataReader["Packing_Charges"].ToString();
                    this.TransportCharges = dbManager.DataReader["Transport_Charges"].ToString();
                    //this.CPID = dbManager.DataReader["CP_ID "].ToString();
                    //this.Fright = dbManager.DataReader["PI_FRIGHT"].ToString();
                    this.PaymentTerms = dbManager.DataReader["PAYMENT_TERMS"].ToString();
                    this.AdvanceAmount = dbManager.DataReader["Advance_Amt"].ToString();
                    this.CUSTID = dbManager.DataReader["CUST_ID"].ToString();
                    this.SoNo = dbManager.DataReader["SO_NO"].ToString();
                    this.SoDate = dbManager.DataReader["SO_DATE"].ToString();
                    this.QuotId = dbManager.DataReader["QUOT_ID"].ToString();
                    this.CustName = dbManager.DataReader["CUST_NAME"].ToString();
                    this.CustAddress = dbManager.DataReader["CUST_ADDRESS"].ToString();
                    this.CustContactPerson = dbManager.DataReader["CUST_CONTACT_PERSON"].ToString();
                    this.CustMobile = dbManager.DataReader["CUST_MOBILE"].ToString();
                    this.CustPhone = dbManager.DataReader["CUST_PHONE"].ToString();
                    this.UnitId = dbManager.DataReader["PI_UNIT_ID"].ToString();
                    this.OtherSpec = dbManager.DataReader["Other_Details"].ToString();
                    //this.SoNo = dbManager.DataReader["SO_NO"].ToString();
                    //this.CustUnitId = dbManager.DataReader["CUST_UNIT_ID"].ToString();

                    this.GSTType = dbManager.DataReader["GST_TYPE"].ToString();
                    this.PI_Total = dbManager.DataReader["PI_Total"].ToString();
                    this.Fright= dbManager.DataReader["Fright_Charges"].ToString();
                    //this.DespmId = dbManager.DataReader["DESPM_ID"].ToString();

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
            public string PI_Save()
            {
                this.PINO = PI_AutoGenCode();
                this.PIID = AutoGenMaxId("YANTRA_PI_MAST", "PI_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO YANTRA_PI_MAST SELECT ISNULL(MAX(PI_ID),0)+1,'{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}',{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17},'{18}',{19},'{20}' FROM YANTRA_PI_MAST ", PINO, PIDate, PIType, SOID, PIMissCharges, SplDisc, PIGross, PIRemarks, PIPreparedBy, PIGST, CPID, PackingCharges, TransportCharges, CUSTID, CustUnitId, OtherSpec, PaymentTerms, AdvanceAmount, GSTType, PI_Total, Fright);
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
            public int PIDetails_Delete(string PiId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [YANTRA_PI_DET] WHERE PI_DET_ID={0}", PiId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                // dbManager.Close();
                return _returnIntValue;
            }
            public int tMaxEmpId;
            public string Terms_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT MAX(ID) from Terms_Conditions");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
                    //TermTitle = dbManager.DataReader[1].ToString();
                    //Others =0;
                }
                dbManager.DataReader.Close();
                _commandText = string.Format("INSERT INTO Terms_Conditions_Selected SELECT ISNULL(MAX(ID),0)+1,{0},'{1}','{2}' FROM Terms_Conditions_Selected", Term_id, TermTitle, PIID);
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

            public int Terms_Delete(string id)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("DELETE FROM [Terms_Conditions_Selected] WHERE Term_Id={0}", id);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                //dbManager.Close();

                return _returnIntValue;
            }

            public string PIDetails_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO YANTRA_PI_DET SELECT ISNULL(MAX(PI_DET_ID),0)+1, '{0}','{1}','{2}', {3} ,{4},'{5}','{6}',{7},{8} FROM YANTRA_PI_DET ", this.PIID, this.ItemCode, this.PIDetQty, this.PIDetRate, GSTDetRate, this.PIColiId, this.PIDescrption, PIDetSplAmt, PIDetUnitPrice);
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
            public void ProformaInvoiceDetails_Select(string PiId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from YANTRA_PI_DET inner join YANTRA_PI_MAST on YANTRA_PI_DET .PI_ID =YANTRA_PI_MAST .PI_ID inner join YANTRA_ITEM_MAST on YANTRA_PI_DET .ITEM_CODE =YANTRA_ITEM_MAST .ITEM_CODE inner join YANTRA_LKUP_UOM on YANTRA_ITEM_MAST .UOM_ID =YANTRA_LKUP_UOM.UOM_ID inner join YANTRA_LKUP_COLOR_MAST on YANTRA_PI_DET .PI_COLOR_ID =YANTRA_LKUP_COLOR_MAST .COLOUR_ID  where YANTRA_PI_MAST .PI_ID= " + PiId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesOrderItems = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("HSN_CODE");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("GST_RATE");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("Amount");
                SalesOrderItems.Columns.Add(col);

                col = new DataColumn("UnitPrice");
                SalesOrderItems.Columns.Add(col);

                col = new DataColumn("SPPrice");
                SalesOrderItems.Columns.Add(col);
                col = new DataColumn("DetId");

                SalesOrderItems.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["HSN_CODE"] = dbManager.DataReader["HSN_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    //dr["ItemGroup"] = dbManager.DataReader["IG_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["PI_DET_RATE"].ToString();
                    dr["GST_RATE"] = dbManager.DataReader["GST_DET_RATE"].ToString();
                    // dr["BalQty"] = dbManager.DataReader["IND_DET_BRAND"].ToString();
                    //dr["Amount"] = dbManager.DataReader["PI_DET_SPLAMT"].ToString();
                    dr["UnitPrice"] = dbManager.DataReader["PI_DET_UNITPRICE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["PI_DET_SPLAMt"].ToString();
                    //dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    //dr["ColorId"] = dbManager.DataReader["PI_COLOR_ID"].ToString();
                    ////dr["Specifications"] = dbManager.DataReader["PI_DESCRIPTION"].ToString();
                    dr["DetId"] = dbManager.DataReader["PI_DET_ID"].ToString();
                    SalesOrderItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesOrderItems;
                gv.DataBind();
            }
            public string PI_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE [YANTRA_PI_MAST] SET PI_DATE='{0}', PI_TYPE='{1}' ,PI_MISS_CHARGES={2},PI_DISCOUNT={3},PI_GROSS_AMT={4},PI_REMARKS='{5}',PI_PREPARED_BY='{6}', GST_RATE={7},CP_ID = '{8}',Packing_Charges='{9}', Transport_Charges='{10}',PI_CUST_ID='{11}',PI_UNIT_ID='{12}',OTHER_DETAILS = '{13}',Payment_Terms =' {14}',Advance_Amt={15},GST_TYPE='{16}',Fright_Charges='{18}',PI_Total='{19}' where PI_ID='{17}' ",
                    this.PIDate, PIType, PIMissCharges, SplDisc, PIGross, PIRemarks, PIPreparedBy, PIGST, CPID, PackingCharges, TransportCharges, CUSTID, CustUnitId, OtherSpec, PaymentTerms, AdvanceAmount, GSTType, PIID, Fright, PI_Total);
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
            public string PI_Delete(string PiId)
            {
                if (DeleteRecord("[YANTRA_PI_DET]", "PI_ID", PiId) == true)
                {
                    if (DeleteRecord("[YANTRA_PI_MAST]", "PI_ID", PiId) == true)
                    {
                        if (DeleteRecord("[Terms_Conditions_Selected]","Others",PiId )==true )
                        {
                            _returnStringMessage = "Data Deleted Successfully";
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

            public void SalesPIDetails_Select(string SalesInvoiceId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ITEM_MAST],[YANTRA_LKUP_UOM],[YANTRA_PI_DET] ,[YANTRA_LKUP_ITEM_TYPE],[YANTRA_PI_MAST],YANTRA_CUSTOMER_MAST  WHERE [YANTRA_PI_MAST] .[PI_ID]=[YANTRA_PI_DET] .[PI_ID] AND [YANTRA_PI_DET] .[ITEM_CODE]=[YANTRA_ITEM_MAST].[ITEM_CODE] AND [YANTRA_ITEM_MAST].UOM_ID=[YANTRA_LKUP_UOM].UOM_ID AND [YANTRA_LKUP_ITEM_TYPE].IT_TYPE_ID=[YANTRA_ITEM_MAST].IT_TYPE_ID and [YANTRA_CUSTOMER_MAST] .CUST_ID  = [YANTRA_PI_MAST] .Cust_Id  AND [YANTRA_CUSTOMER_MAST] .CUST_ID =" + SalesInvoiceId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable SalesInvoiceProducts = new DataTable();
                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("HSN_CODE");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("ItemName");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("UOM");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Quantity");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("Rate");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("GST_TAX");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("SPPrice");
                SalesInvoiceProducts.Columns.Add(col);

                col = new DataColumn("ModelNo");
                SalesInvoiceProducts.Columns.Add(col);
                col = new DataColumn("DetId");
                SalesInvoiceProducts.Columns.Add(col);

                while (dbManager.DataReader.Read())
                {
                    DataRow dr = SalesInvoiceProducts.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["HSN_CODE"] = dbManager.DataReader["HSN_CODE"].ToString();
                    dr["ItemName"] = dbManager.DataReader["ITEM_NAME"].ToString();
                    dr["UOM"] = dbManager.DataReader["UOM_SHORT_DESC"].ToString();
                    dr["Quantity"] = dbManager.DataReader["PI_DET_QTY"].ToString();
                    dr["Rate"] = dbManager.DataReader["PI_DET_RATE"].ToString();
                    dr["GST_TAX"] = dbManager.DataReader["GST TAX"].ToString();
                    //dr["Excise"] = dbManager.DataReader["SI_DET_EXCISE"].ToString();
                    dr["SPPrice"] = dbManager.DataReader["PI_DET_SPPRICE"].ToString();
                    //dr["DeliveryDate"] = Convert.ToDateTime(dbManager.DataReader["DC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["DetId"] = dbManager.DataReader["PI_DET_ID"].ToString();



                    SalesInvoiceProducts.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = SalesInvoiceProducts;
                gv.DataBind();
                //dbManager.Close();
            }

        }
        public class StockMovement
        {
            public string SmdcId, IntIndId, MovingFrom, MovingDcDate, CpId, PreparedBy, Status;
            public string SmdcdetId, Itemcode, Brandid, colorid, Quantity, Movingto, Remarks, Clientname, RecQty;

            public StockMovement()
            {

            }

            public static string InternalDC_AutoGenCode()
            {
                return SM.AutoGenMaxNo("StockMovement_Master", "INT_DC_NO");
            }
            public string DCNo, VechileNo;
            public string StockMovementMaster_Save()
            {
                this.SmdcId = AutoGenMaxId("[StockMovement_Master]", "SM_DC_ID");

                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [StockMovement_Master] SELECT ISNULL(MAX(SM_DC_ID),0)+1,'{0}',{1},{2},'{3}',{4},{5},'{6}',{7},'{8}' FROM [StockMovement_Master]", this.DCNo, this.IntIndId, this.MovingFrom, this.MovingDcDate, this.CpId, this.Movingto, this.VechileNo, this.PreparedBy, this.Status);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Internal INdent Details", "61");

                }
                //dbManager.Close();
                return _returnStringMessage;
            }
            public string Remark;
            public string StockMovementDetails_Save()
            {
                // this.SmdcId = AutoGenMaxId("[StockMovement_Master]", "SM_DC_ID");
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [STOCKMOVEMENT_DETAILS] SELECT ISNULL(MAX(SM_DCDET_ID),0)+1,{0},{1},{2},{3},{4},{5},'{6}','{7}',{8},'{9}' FROM [STOCKMOVEMENT_DETAILS]", this.SmdcId, this.Itemcode, this.Brandid, this.colorid, this.Quantity, this.Movingto, this.Remarks, this.Clientname, this.RecQty, this.Remark);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Internal INdent Details", "61");

                }
                return _returnStringMessage;
            }

            public int InternalDC_Select(string DCId)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("select * from [StockMovement_Master] where [SM_DC_ID]='" + DCId + "' ORDER BY SM_DC_ID DESC ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.DCNo = dbManager.DataReader["INT_DC_NO"].ToString();
                    this.MovingDcDate = Convert.ToDateTime(dbManager.DataReader["MOVINGDC_DATE"].ToString()).ToString("dd/MM/yyyy");
                    this.IntIndId = dbManager.DataReader["INT_IND_ID"].ToString();
                    this.MovingFrom = dbManager.DataReader["MOVINGFROM"].ToString();
                    this.Movingto = dbManager.DataReader["MOVINGTO"].ToString();
                    this.VechileNo = dbManager.DataReader["VEHICLENO"].ToString();
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

            public void InternalDcDetails_Select(string DcId, GridView gv)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format(" select a.ITEM_CODE,a.QUANTITY,a.ReceivedQty,a.BRAND_ID,a.COLOR_ID,a.REMARKS,a.CLIENT_NAME,a.Remark,a.SM_DCDET_ID,c.ITEM_MODEL_NO,d.COLOUR_NAME,e.PRODUCT_COMPANY_NAME from " +
                   "dbo.STOCKMOVEMENT_DETAILS a inner join dbo.StockMovement_Master b on a.SM_DC_ID=b.SM_DC_ID " +
                   "inner join YANTRA_ITEM_MAST c on a.ITEM_CODE=c.ITEM_CODE " +
                   "inner join YANTRA_LKUP_COLOR_MAST d on a.COLOR_ID=d.COLOUR_ID " +
                   "inner join dbo.YANTRA_LKUP_PRODUCT_COMPANY e on e.PRODUCT_COMPANY_ID=a.BRAND_ID  where a.[SM_DC_ID]='" + DcId + "' order by a.SM_DCDET_ID");
                dbManager.ExecuteReader(CommandType.Text, _commandText);

                DataTable MovingItems = new DataTable();

                DataColumn col = new DataColumn();
                col = new DataColumn("ItemCode");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Brand");
                MovingItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Color");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Qty");
                MovingItems.Columns.Add(col);
                //col = new DataColumn("Location");
                //MovingItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                MovingItems.Columns.Add(col);
                col = new DataColumn("ClientName");
                MovingItems.Columns.Add(col);
                col = new DataColumn("BrandId");
                MovingItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Remark");
                MovingItems.Columns.Add(col);
                col = new DataColumn("DetId");
                MovingItems.Columns.Add(col);


                while (dbManager.DataReader.Read())
                {
                    DataRow dr = MovingItems.NewRow();
                    dr["ItemCode"] = dbManager.DataReader["ITEM_CODE"].ToString();
                    dr["Brand"] = dbManager.DataReader["PRODUCT_COMPANY_NAME"].ToString();
                    dr["ModelNo"] = dbManager.DataReader["ITEM_MODEL_NO"].ToString();
                    dr["Color"] = dbManager.DataReader["COLOUR_NAME"].ToString();
                    dr["Qty"] = dbManager.DataReader["ReceivedQty"].ToString();
                    dr["Remarks"] = dbManager.DataReader["REMARKS"].ToString();
                    dr["ClientName"] = dbManager.DataReader["CLIENT_NAME"].ToString();
                    dr["BrandId"] = dbManager.DataReader["BRAND_ID"].ToString();
                    dr["ColorId"] = dbManager.DataReader["COLOR_ID"].ToString();
                    dr["Remark"] = dbManager.DataReader["Remark"].ToString();
                    dr["DetId"] = dbManager.DataReader["SM_DCDET_ID"].ToString();

                    MovingItems.Rows.Add(dr);
                }
                dbManager.DataReader.Close();
                gv.DataSource = MovingItems;
                gv.DataBind();
            }

            public string InternalDC_Update()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("UPDATE StockMovement_Master SET [INT_IND_ID]={0},[MOVINGFROM]={1},[MOVINGDC_DATE]='{2}',[MOVINGTO]={3},[VEHICLENO]='{4}',[PREPAREDBY]={5},[Status]='{6}' WHERE [SM_DC_ID]={7}", this.IntIndId, this.MovingFrom, this.MovingDcDate, this.Movingto, this.VechileNo, this.PreparedBy, this.Status, this.SmdcId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    // log.add_Update("GRN Details", "61");

                }
                return _returnStringMessage;
            }
            public string InternalDC_Delete(string DCId)
            {
                if (DeleteRecord("[STOCKMOVEMENT_DETAILS]", "SM_DC_ID", DCId) == true)
                {
                    if (DeleteRecord("[StockMovement_Master]", "SM_DC_ID", DCId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Delete("Internalindent Details", "61");

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
            public string InternalDCDetails_Delete(string DCid)
            {
                if (DeleteRecord("STOCKMOVEMENT_DETAILS", "SM_DC_ID", DCid) == true)
                {
                    _returnStringMessage = "Data Deleted Successfully";
                }
                else
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                //dbManager.Close();
                return _returnStringMessage;
            }
        }

    }
}
