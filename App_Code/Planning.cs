
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
/// Summary description for Planning
/// </summary>

public class Planning
{
    private static int _returnIntValue;
    private static string _returnStringMessage, _commandText;

    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());


	public Planning()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //Method for dispose 
    public static void Dispose()
    {
        dbManager.Dispose();
    }

    //Method for Begin Transaction 
    public static void BeginTransaction()
    {
        dbManager.Open();
        dbManager.BeginTransaction();
    }

    //Method for Commit Transaction 
    public static void CommitTransaction()
    {
        dbManager.CommitTransaction();
    }

    //Method for Rollback Transaction 
    public static void RollBackTransaction()
    {
        dbManager.RollBackTransaction();
    }

    //Methods for Checking a record exists or not with reference id
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


    //Methods For DeptWiseMachineSetup Form
    public class DeptWiseMachineSetup
    {
        public string DWMSId, WCId, DeptId, CMId,CMNo, DWMSProdCapacity, DWMSStdRate, DWMSAvaiTime, ShiftId,MachineType,MachineDesc;


        public DeptWiseMachineSetup()
        {
        }
        public string DeptWiseMachineSetup_Save()
        {
           
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_DEPTWISE_MACHINESETUP] SELECT ISNULL(MAX(DWMS_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}' FROM [YANTRA_DEPTWISE_MACHINESETUP]", this.WCId, this.DeptId, this.CMId, this.DWMSProdCapacity, this.DWMSStdRate, this.DWMSAvaiTime, this.ShiftId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Department Wise Machine Setup Details", "72");

            }
            return _returnStringMessage;
        }

        public string DeptWiseMachineSetup_Update()
        {
            //this.SOId = this.SONo.Replace("SO/", "");
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_DEPTWISE_MACHINESETUP] SET WC_ID='{0}',DEPT_ID='{1}',CM_ID='{2}',DWMS_PRODUCT_CAPACITY='{3}',DWMS_STD_RATE='{4}',DWMS_AVAILABLE_TIME='{5}',SHIFT_ID='{6}' WHERE DWMS_ID='{7}'", this.WCId, this.DeptId, this.CMId, this.DWMSProdCapacity, this.DWMSStdRate, this.DWMSAvaiTime, this.ShiftId, this.DWMSId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Department Wise Machine Setup Details", "72");

            }
            return _returnStringMessage;
        }

        public string DeptWiseMachineSetup_Delete()
        {
            if (DeleteRecord("[YANTRA_DEPTWISE_MACHINESETUP]", "DWMS_ID", this.DWMSId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Department Wise Machine Setup Details", "72");

            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public static void DeptWiseMachineSetup_Select(Control ControlForBind)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_DEPTWISE_MACHINESETUP] ORDER BY DWMS_ID");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "DWMS_ID", "DWMS_ID");
            }
        }

        public static void WorkCenterName_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_WORKCENTER_MAST] ORDER BY WC_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "WC_NAME", "WC_ID");
            }
        }
    }

    //Methods For MachineAllocationDet Form
    public class MachineAllocationDet
    {
        public string MAId,MANo,MADate,ShiftId,CMId,WCId,EmpId,EmpType,EmpName,MAPurpose,MADuration,MAPreparedBy,MAApprovedBy;


        public MachineAllocationDet()
        {
        }

        public static string MachineAllocationDet_AutoGenCode()
        {
            string _codePrefix = "MA/";
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(REPLACE(MA_NO,'" + _codePrefix + "','')),0)+1 FROM [YANTRA_MACHINEALLOCATION_DET]").ToString());
            return _codePrefix + _returnIntValue;
        }

        public string MachineAllocationDet_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_MACHINEALLOCATION_DET] SELECT ISNULL(MAX(MA_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}' FROM [YANTRA_MACHINEALLOCATION_DET]", this.MANo, this.MADate, this.ShiftId, this.CMId, this.WCId, this.EmpId, this.MAPurpose, this.MADuration, this.MAPreparedBy, this.MAApprovedBy);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Machine Allocation Details", "73");

            }
            return _returnStringMessage;
        }

        public string MachineAllocationDet_Update()
        {
            //this.SOId = this.SONo.Replace("SO/", "");
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_MACHINEALLOCATION_DET] SET MA_DATE='{0}',SHIFT_ID='{1}',CM_ID='{2}',WC_ID='{3}',EMP_ID='{4}',MA_PURPOSE='{5}',MA_DURATION='{6}',MA_PREPARED_BY='{7}',MA_APPROVED_BY='{8}' WHERE MA_ID='{9}'", this.MADate, this.ShiftId, this.CMId, this.WCId, this.EmpId, this.MAPurpose, this.MADuration, this.MAPreparedBy, this.MAApprovedBy);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Machine Allocation Details", "73");

            }
            return _returnStringMessage;
        }

        public string MachineAllocationDet_Delete()
        {
            if (DeleteRecord("[YANTRA_MACHINEALLOCATION_DET]", "MA_ID", this.MAId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Machine Allocation Details", "73");

            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public static void MachineAllocationDet_Select(Control ControlForBind)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_MACHINEALLOCATION_DET] ORDER BY MA_ID");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "DWMS_ID", "MA_ID");
            }
        }

        public static void WorkCenterName_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_WORKCENTER_MAST] ORDER BY WC_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "WC_NAME", "WC_ID");
            }
        }
    }

    //Methods For MachineProductRelation Form
    public class MachineProductRelation
    {
        public string  MPRId,CMId,ProductId,ProductName,UOM,ProcessName,BaseName,MPRBaseQty,MPRBatchQty,MPRPreparedBy;
        public string MPRdDetId, OPRId, MPRDetProcessTime, MPRDetLoadingTime, MPRDetUnLoadingTime, MPRDetCleaningTime;


        public MachineProductRelation()
        {
        }

        
        public string MachineProductRelation_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_MACHINE_PROD_REL_MAST] SELECT ISNULL(MAX(MPR_ID),0)+1,'{0}','{1}',{2},'{3}','{4}','{5}' FROM [YANTRA_MACHINE_PROD_REL_MAST]", this.CMId, this.ProductId, this.BaseName, this.MPRBaseQty, this.MPRBatchQty, this.MPRPreparedBy);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Machine Product Relation Details", "74");

            }
            return _returnStringMessage;
        }

        public string MachineProductRelation_Update()
        {
            //this.SOId = this.SONo.Replace("SO/", "");
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_MACHINE_PROD_REL_MAST] SET CM_ID='{0}',PRODUCT_ID='{1}',PRODUCT_BASE_NAME='{2}',MPR_BASE_QTY='{3}',MPR_BATCH_QTY='{4}',MPR_PREPARED_BY='{5}' WHERE MPR_ID='{6}'", this.CMId, this.ProductId, this.BaseName, this.MPRBaseQty, this.MPRBatchQty, this.MPRPreparedBy,this.MPRId );
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Machine Product Relation Details", "74");

            }
            return _returnStringMessage;
        }

        public string MachineProductRelation_Delete()
        {
            if (DeleteRecord("[YANTRA_MACHINE_PROD_REL_MAST]", "MPR_ID", this.MPRId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Machine Product Relation Details", "74");

            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public static void MachineProductRelation_Select(Control ControlForBind)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_MACHINE_PROD_REL_MAST] ORDER BY MPR_ID");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "MPR_ID", "MPR_ID");
            }
        }

        public static void MachineName_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_COMP_MACHINARY_MAST] ORDER BY CM_MACHINE_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "CM_MACHINE_NAME", "CM_ID");
            }
        }
    }
}
