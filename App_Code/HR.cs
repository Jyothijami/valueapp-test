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
using System.Net;
using System.IO;
using System.Data.SqlClient;

public class HR
{
    private static int _returnIntValue;
    private static string _returnStringMessage, _commandText;
    
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());

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
    #endregion


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

    //Method for Dropdownlist Binding
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
        dbManager.Close();
    }

    //Method for Auto Generate Max Serial ID
    public static string AutoGenMaxId(string TableName, string FieldName)
    {
        if (dbManager.Transaction == null)
            dbManager.Open();
        _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
        return _returnIntValue.ToString();
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

    //Binding COmpany
    public static void Company_Select(Control ControlForBind)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid where a.Status=1");
        //_commandText = string.Format("SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "COMP_NAME", "CP_ID");
        }
    }
    //Binding Department
    public static void Department_Select(Control ControlForBind)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT DEPT_NAME,DEPT_ID FROM [YANTRA_DEPT_MAST] where DEPT_NAME is not null");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "DEPT_NAME", "DEPT_ID");
        }

    }
    //Binding Designation
    public static void Designation_Select(Control ControlForBind)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT  * FROM [YANTRA_DESG_MAST] ORDER BY DESG_NAME");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "DESG_NAME", "DESG_ID");
        }

    }
    //Binding Emp Type
    public static void EmployeeType_Select(Control ControlForBind)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_EMP_TYPE] ORDER BY EMP_TYPE_NAME");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "EMP_TYPE_NAME", "EMP_TYPE_ID");
        }

    }
    //Binding Region or Location 
    public static void RegionalMaster_Select(Control ControlForBind)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT * FROM [YANTRA_LKUP_REGION] ORDER BY REG_NAME");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "REG_NAME", "REG_ID");
        }
    }


    //Binding Employee Bind With Dept Id
    public static void EmployeeDept(Control ControlForBind, string Dept_id)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT  YANTRA_EMPLOYEE_MAST.EMP_ID, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME FROM YANTRA_EMPLOYEE_MAST INNER JOIN YANTRA_EMPLOYEE_DET ON dbo.YANTRA_EMPLOYEE_MAST.EMP_ID = dbo.YANTRA_EMPLOYEE_DET.EMP_ID INNER JOIN YANTRA_DEPT_MAST ON dbo.YANTRA_EMPLOYEE_DET.DEPT_ID = dbo.YANTRA_DEPT_MAST.DEPT_ID and YANTRA_DEPT_MAST.DEPT_ID =" + int.Parse(Dept_id) + "");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "EMP_FIRST_NAME", "EMP_ID");

        }
    }

    public static void EmployeeDeptNameSelect(Control ControlForBind)
    {
        dbManager.Open();
        _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],YANTRA_EMPLOYEE_DET,YANTRA_DEPT_MAST  WHERE YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID and YANTRA_DEPT_MAST.DEPT_ID = YANTRA_EMPLOYEE_DET.DEPT_ID and   YANTRA_EMPLOYEE_MAST.EMP_ID<>0 and YANTRA_DEPT_MAST.DEPT_NAME = 'HR And Admin' ORDER BY YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME asc");
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
    }

    //Binding Employee Bind With Company ID
    public static void EmployeeCompany(Control ControlForBind, string company_id)
    {
        dbManager.Open();
        _commandText = string.Format("select EMP_ID,EMP_FIRST_NAME,COMPANY_ID from YANTRA_EMPLOYEE_MAST where    YANTRA_EMPLOYEE_MAST.EMP_ID<>0 AND    YANTRA_EMPLOYEE_MAST.COMPANY_ID=" + int.Parse(company_id) + " ");
        dbManager.ExecuteReader(CommandType.Text, _commandText);
        if (ControlForBind is DropDownList)
        {
            DropDownListBind(ControlForBind as DropDownList, "EMP_FIRST_NAME", "EMP_ID");

        }
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
    }



    #region Circular
    public class Circular
    {
        public string CirId, CirName, CirNo, CirDate, Desc;
        public string EmpId, Cpid;
        public string CmpId, DeptId, empid, circular, issuedate, readmsg,descrption;

        DateTime date;

        public Circular()
        {
        }

        public int Circular_Select(string CirID)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select * from YANTRA_HR_CIRCULAR where COMPANY_ID in (55,56,57) and CIR_ID ='" + CirID + "' ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.CirId = dbManager.DataReader["CIR_ID"].ToString();
                this.CirNo = dbManager.DataReader["CIR_NO"].ToString();
                this.Desc = dbManager.DataReader["DESCRIPTION"].ToString();
                this.CirDate = Convert.ToDateTime(dbManager.DataReader["CIR_DATE"].ToString()).ToString("dd/MM/yyyy");
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.Cpid = dbManager.DataReader["COMPANY_ID"].ToString();

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

        #region Circular SAVE
        public string Circular_Save()
        {
            dbManager.Open();
            readmsg="0";
            _commandText = string.Format("INSERT INTO [YANTRA_HR_CIRCULAR] SELECT ISNULL(MAX(CIR_ID),0)+1,'{0}',{1},'{2}',{3},'{4}','{5}',{6} FROM [YANTRA_HR_CIRCULAR]", this.circular, this.CmpId, this.DeptId, this.empid, this.issuedate, this.descrption,this.readmsg );
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Circular Details", "49");
            }

            return _returnStringMessage;
        }
        #endregion

        #region Circular Update
        public string Circular_Update()
        {
            dbManager.Open();

            if (true)
            {
                _commandText = string.Format("UPDATE [YANTRA_HR_CIRCULAR] SET  CIR_NO='{0}',COMPANY_ID ={1},DEPT_ID = {2},Emp_Id={3},CIR_DATE='{4}',DESCRIPTION='{5}',read_msg={6} WHERE CIR_ID={7}", this.circular, this.CmpId, this.DeptId, this.empid, this.issuedate, this.descrption,this.readmsg , this.CirId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Circular Details", "49");

                }
            }
            else
            {
                //   _returnStringMessage = "Circular Name Already Exists.";
            }
            return _returnStringMessage;

        }
        #endregion

        #region Circular DELETE
        public string Circular_Delete()
        {

            if (DeleteRecord("[YANTRA_HR_CIRCULAR]", "CIR_ID", this.CirId) == true)
            {

                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Circular Details", "49");

            }
            else
            {

                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }
        #endregion
        public static string Cir_AutoGenCode()
        {

            return AutoGenMaxNo("YANTRA_HR_CIRCULAR", "CIR_NO");
        }
    }

    #endregion

    public class getDashBoards
    {
        static SqlConnection con = dbc.con;
        public DataTable getUserControls(string UserId)
        {

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            con.Close();
            cmd = new SqlCommand(@"SELECT    distinct(YANTRA_LKUP_PRIVILEGES.DBoardName),YANTRA_LKUP_PRIVILEGES.seqno
FROM         YANTRA_USER_PERMISSIONS INNER JOIN
                      YANTRA_USER_LKUP_PERMISSIONS ON YANTRA_USER_PERMISSIONS.Permission_Id = YANTRA_USER_LKUP_PERMISSIONS.Permission_Id INNER JOIN
                      YANTRA_LKUP_PRIVILEGES ON YANTRA_USER_PERMISSIONS.PRIVILEGE_ID = YANTRA_LKUP_PRIVILEGES.PRIVILEGE_ID
WHERE     (YANTRA_USER_PERMISSIONS.permission=1) AND (YANTRA_USER_PERMISSIONS.UserId = @UserId) and YANTRA_LKUP_PRIVILEGES.DBoardName is not null order by YANTRA_LKUP_PRIVILEGES.seqno asc", con);
            cmd.Parameters.Add("@UserId", SqlDbType.Decimal).Value = Convert.ToDecimal(UserId);
            con.Open();

            da.SelectCommand = cmd;
            da.Fill(dt);

            con.Close();

            return dt;
        }
    }
    public class SendSMS
    {
        public string Message, Mobile_No,ExecutiveName,AttendedBy;
        public SendSMS()
        {

        }

        public void Send_OTPSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161760256110229 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "https://api.msg91.com/api/sendhttp.php?authkey=344934AYG4cJ7EnxX5f8d3fc0P1&mobiles=" + MobileNo + "&route=4&sender=VLULIN&DLT_TE_ID=1707161760256110229&message=" + Message + "";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_LeaveApprovalSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161976694884092 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_ApprovedLeaveSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162012407853073 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void CreditAppSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162012364564392 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void ComptoCC(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162012369276230 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_RejectedLeaveSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161976687620839 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_TechSMS(string Message, string MobileNo)
        {
            string url = "";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162373523879919 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_ExecReopenedTechCompSMS(string Message, string MobileNo)
        {
            string url = "";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162012866819509 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_ExecCompClosedSMS(string Message, string MobileNo)
        {
            string url = "";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161976697314103 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }

        public void Send_ExecCompPendingSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161976697314103 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void CustReopenCompSMS(string Message, string MobileNo)
        {
            string url = "";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162012922772090 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_CustInOTPSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161579052399143 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void CustManualClosedSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161846964910613 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_ExecCompSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707161579049475825 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void SendExecWoTechSMS(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162013048397356 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_CustWoTechSMS(string Message, string MobileNo)
        {
            string url = "";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707162012953796331 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_App_SMS_msg91Comp(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=312340An6O47xVF5e16eb03P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            //url = "http://bhashsms.com/api/sendmsg.php?user=valueline&pass=12345&sender=VLULIN&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=flash";
            url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";

            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }

        public void Send_App_SMS_CloseOTP(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=312340An6O47xVF5e16eb03P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            ////url = "http://bhashsms.com/api/sendmsg.php?user=valueline&pass=12345&sender=VLULIN&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=flash";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707163722169389931 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }

        public void Send_App_SMS_PendingOTP(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=312340An6O47xVF5e16eb03P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            ////url = "http://bhashsms.com/api/sendmsg.php?user=valueline&pass=12345&sender=VLULIN&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=flash";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&DLT_TE_ID=1707163722227695649 &country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_App_SMS_msg91(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=312340An6O47xVF5e16eb03P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            ////url = "http://bhashsms.com/api/sendmsg.php?user=valueline&pass=12345&sender=VLULIN&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=flash";
            url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_App_SMS(string Message, string MobileNo)
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
        public void Send_App_SMS1(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=245967A5luEbTL4i7w5dde128d";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            ////url = "http://bhashsms.com/api/sendmsg.php?user=valueline&pass=12345&sender=VLULIN&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=flash";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public void Send_App_SMS2(string Message, string MobileNo)
        {
            string url = "";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=245967A5luEbTL4i7w5dde128d";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=312340An6O47xVF5e16eb03P1";
            //url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLINEE&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";
            url = "https://api.msg91.com/api/sendhttp.php?route=4&sender=VLULIN&message=" + Message + "&country=91&mobiles=" + MobileNo + "&authkey=344934AYG4cJ7EnxX5f8d3fc0P1";

            //url = "http://bhashsms.com/api/sendmsg.php?user=valueline&pass=12345&sender=VLULIN&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=flash";
            //url = "http://bhashsms.com/api/sendmsg.php?user=success&pass=123456&sender=BSHSMS&phone=" + MobileNo + "&text=" + Message + "&priority=ndnd&stype=normal";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            string baseurl = url;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }

    }
    public class EmployeeMaster
    {
        public string EmpID, UserId, ExpiryDate, EmpFirstName, locid, EmpMiddleName, EmpLastName, EmpGender, EmpMobile, EmpPhone, EmpDOB, EmpEMail, EmpPhoto, EmpAddress, EmpCity, DeptID, DesgID, BranchId, EmpDetDOJ, EmpDetDOT, ReporingName, ReportingId, EmpTypeID, EmpBranchID, EMPUserName, EmpCompany, servicebond, PerPhoneno, permobileno, emrname, emrrelation, emgphone, emgaddress, Others, FATHERNAME, AssignedEmpId, AssignedAccNo, Status, AssignedBankName;
        public string tEmpPhoto, Company_ID, Dept_Hod_Id;
        public string AssignedMobileNo, AssignedEmailId, InsuraceInfo,InsuranceCompanyName;
        public string DeptName12, ComapanyName, DesgName12, GrossSal, grossdob,CurrentCTC;
        public string DocSubmit,DocSubmit1, Cp_Id, DateSubmitted;
        public string SickLeaves, CasualLeaves, LeavesEarned;
        public string Asset1, Asset2, Asset3, Asset4, Asset5, Asset6,DOcRewardId;
        public EmployeeMaster()
        {
        }

        public EmployeeMaster(string EmployeeID)
        {
            dbManager.Open();
            _commandText = string.Format(@"SELECT  YANTRA_EMPLOYEE_MAST.EMP_ID, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME + ' ' + YANTRA_EMPLOYEE_MAST.EMP_MIDDLE_NAME AS Fullname, YANTRA_EMPLOYEE_DET.DEPT_ID, 
                      YANTRA_EMPLOYEE_MAST.COMPANY_ID
FROM         YANTRA_EMPLOYEE_MAST INNER JOIN
                      YANTRA_EMPLOYEE_DET ON YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID where YANTRA_EMPLOYEE_MAST.EMP_ID = " + EmployeeID);
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.EmpID = dbManager.DataReader["EMP_ID"].ToString();
                this.DeptID = dbManager.DataReader["DEPT_ID"].ToString();
                this.Company_ID = dbManager.DataReader["COMPANY_ID"].ToString();
            }
            dbManager.DataReader.Close();
        }
        public static void EmployeeMaster_SelectDept(Control ControlForBind,string deptId)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID='"+deptId+"' ORDER BY EMP_FIRST_NAME");
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
        }
        public static void LeaveEmpMaster_Select(Control ControlForBind)
        {
            //if(dbManager .DataReader ==null)
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE()  ORDER BY EMP_FIRST_NAME");
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
        }
        public static void EmployeeMaster_SelectDept_DamageReport(Control ControlForBind, string EmpId)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID in (select [YANTRA_DEPT_MAST].DEPT_ID from [YANTRA_DEPT_MAST] where [YANTRA_DEPT_MAST].DEPT_HEAD='" + EmpId + "') ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectSalesIn12(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (c.DEPT_NAME like '%Sales%' or a.EMP_ID=89)  ORDER BY EMP_FIRST_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                (ControlForBind as DropDownList).Items.Clear();
                (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                while (dbManager.DataReader.Read())
                {
                    (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
                }
                dbManager.DataReader.Close();
            }
        }

        public static void EmployeeMaster_SelectSales12(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (c.DEPT_NAME like '%Sales%' or a.EMP_ID=89) and STATUS !=0  ORDER BY EMP_FIRST_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                (ControlForBind as DropDownList).Items.Clear();
                (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                while (dbManager.DataReader.Read())
                {
                    (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
                }
                dbManager.DataReader.Close();
            }
        }
        public static void EmployeeMaster_SelectAllSales12(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (c.DEPT_NAME like '%Sales%' or a.EMP_ID=89)   ORDER BY EMP_FIRST_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                (ControlForBind as DropDownList).Items.Clear();
                (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                while (dbManager.DataReader.Read())
                {
                    (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
                }
                dbManager.DataReader.Close();
            }
        }
        public static void EmployeeMaster_SelectInactiveSales12(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (c.DEPT_NAME like '%Sales%' ) and STATUS =0  ORDER BY EMP_FIRST_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                (ControlForBind as DropDownList).Items.Clear();
                (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                while (dbManager.DataReader.Read())
                {
                    (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
                }
                dbManager.DataReader.Close();
            }
        }

        public static void EmployeeMaster_SelectSales(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID inner join YANTRA_DEPT_MAST c on c.DEPT_ID=b.DEPT_ID WHERE a.EMP_ID<>0   and (b.DEPT_ID=1 or b.Dept_Id=9 or  b.DEPT_ID=11  or  b.DEPT_ID=16 or  b.DEPT_ID=17 or  b.DEPT_ID=19 or  b.DEPT_ID=24 or  b.DEPT_ID=30 or b.DEPT_ID =32 or b.Dept_id=16  or c.DEPT_NAME like '%Sales%' or b.DEPT_ID=11) and STATUS !=0  ORDER BY EMP_FIRST_NAME");
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
        }
        public void EmpPayroll_Select1(string Cp_Id, string Year, string Month, string locid, GridView gv)
        {
            dbManager.Open();
            _commandText = string.Format("select V_ToGenerate_PaySlip.Emp_ID,EMP_FIRST_NAME,Paid,TotalNOD,Basic,HRA,CA,MedicalAllowance,Other_Allowance,PF,PT,TDS,Other_Deductions,Sal_Advance from V_ToGenerate_PaySlip  inner join YANTRA_EMPLOYEE_MAST on V_ToGenerate_PaySlip .Emp_ID =YANTRA_EMPLOYEE_MAST .emp_id inner join YANTRA_EMPLOYEE_DET on V_ToGenerate_PaySlip .Emp_ID =YANTRA_EMPLOYEE_DET .EMP_ID inner join YANTRA_COMP_PROFILE on YANTRA_EMPLOYEE_MAST .COMPANY_ID =YANTRA_COMP_PROFILE .CP_ID  Where [YANTRA_COMP_PROFILE].cp_id=" + Cp_Id + "  and YEAR=" + Year + " and MONTH= " + Month + " and YANTRA_COMP_PROFILE.locid=" + locid + " ORDER BY EMP_FIRST_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            DataTable Payroll = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("Emp_ID");
            Payroll.Columns.Add(col);
            col = new DataColumn("Emp_Name");
            Payroll.Columns.Add(col);
            col = new DataColumn("Paid");
            Payroll.Columns.Add(col);
            col = new DataColumn("TotalNOD");
            Payroll.Columns.Add(col);
            //col = new DataColumn("LOP");
            //Payroll.Columns.Add(col);
            col = new DataColumn("Basic");
            Payroll.Columns.Add(col);
            col = new DataColumn("HRA");
            Payroll.Columns.Add(col);
            col = new DataColumn("Conveyance");
            Payroll.Columns.Add(col);
            col = new DataColumn("Medical");
            Payroll.Columns.Add(col);
            col = new DataColumn("Other_Allow");
            Payroll.Columns.Add(col);
            //col = new DataColumn("Gross");
            //Payroll.Columns.Add(col);
            col = new DataColumn("PF");
            Payroll.Columns.Add(col);
            col = new DataColumn("PTax");
            Payroll.Columns.Add(col);
            //col = new DataColumn("ESI");
            //Payroll.Columns.Add(col);
            col = new DataColumn("TDS");
            Payroll.Columns.Add(col);
            col = new DataColumn("OtherDedc");
            Payroll.Columns.Add(col);
            col = new DataColumn("Sal_Advance");
            Payroll.Columns.Add(col);
            //col = new DataColumn("TotalDeductions");
            //Payroll.Columns.Add(col);
            //col = new DataColumn("NetAmount");
            //Payroll.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = Payroll.NewRow();
                dr["Emp_ID"] = dbManager.DataReader["Emp_ID"].ToString();
                dr["Emp_Name"] = dbManager.DataReader["EMP_FIRST_NAME"].ToString();
                dr["Paid"] = dbManager.DataReader["Paid"].ToString();
                dr["TotalNOD"] = dbManager.DataReader["TotalNOD"].ToString();
                //dr["LOP"] = dbManager.DataReader["LOP"].ToString();
                dr["Basic"] = dbManager.DataReader["Basic"].ToString();
                dr["HRA"] = dbManager.DataReader["HRA"].ToString();
                dr["Conveyance"] = dbManager.DataReader["CA"].ToString();
                dr["Medical"] = dbManager.DataReader["MedicalAllowance"].ToString();
                dr["Other_Allow"] = dbManager.DataReader["Other_Allowance"].ToString();
                dr["PF"] = dbManager.DataReader["PF"].ToString();
                dr["PTax"] = dbManager.DataReader["PT"].ToString();
                //dr["ESI "] = dbManager.DataReader["EMP_ID"].ToString();
                dr["TDS"] = dbManager.DataReader["TDS"].ToString();
                dr["OtherDedc"] = dbManager.DataReader["Other_Deductions"].ToString();
                dr["Sal_Advance"] = dbManager.DataReader["Sal_Advance"].ToString();

                Payroll.Rows.Add(dr);
            }
            dbManager.DataReader.Close();
            gv.DataSource = Payroll;
            gv.DataBind();
        }
        public static void EmployeeMaster_SelectPurchase(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and (b.DEPT_ID=2 or  b.DEPT_ID=18) ORDER BY EMP_FIRST_NAME");
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
        }


        public static void EmployeeMaster_SelectStores(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID=10 ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectAccounts(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID in(3,22,26,29) ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectCMD(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID=11 ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectCustomerCare(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID=13 ORDER BY EMP_FIRST_NAME");
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
        }
        public static void EmployeeMaster_SelectCustomerCareTechnical(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and (b.DEPT_ID=13 or b.dept_id=6 or b.dept_id=32)  ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectFromCompany(Control ControlForBind, string Cp_Id)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET],YANTRA_COMP_PROFILE WHERE YANTRA_EMPLOYEE_MAST.EMP_ID<>0 and YANTRA_EMPLOYEE_DET.EMP_DET_DOT >=GETDATE() and [YANTRA_EMPLOYEE_MAST].COMPANY_ID=[YANTRA_COMP_PROFILE].cp_id AND YANTRA_EMPLOYEE_MAST.EMP_ID=YANTRA_EMPLOYEE_DET.EMP_ID and [YANTRA_COMP_PROFILE].cp_id=" + Cp_Id + " ORDER BY EMP_FIRST_NAME");
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
        }
        public static void EmployeeMaster_SelectTechnical(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID=6 or b.DEPT_ID=21 or b.DEPT_ID=28 or b.DEPT_ID=32  ORDER BY EMP_FIRST_NAME");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                //or b.DEPT_ID=21 or b.DEPT_ID=28 
                (ControlForBind as DropDownList).Items.Clear();
                (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                while (dbManager.DataReader.Read())
                {
                    (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString() + " " + dbManager.DataReader["EMP_LAST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
                }
                dbManager.DataReader.Close();
            }
        }

        public static void EmployeeMaster_SelectDept_Comp(Control ControlForBind, string deptId, string Comp)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0  and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and b.DEPT_ID='" + deptId + "' and a.Company_id ='" + Comp + "' ORDER BY EMP_FIRST_NAME");
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
        }
        public static void EmployeeMaster_Select12(Control ControlForBind, string locname)
        {
            dbManager.Open();
            _commandText = string.Format("select * from YANTRA_EMPLOYEE_MAST inner join YANTRA_DEPT_MAST on YANTRA_EMPLOYEE_MAST .EMP_ID =YANTRA_DEPT_MAST.DEPT_HEAD inner join YANTRA_COMP_PROFILE on YANTRA_EMPLOYEE_MAST .COMPANY_ID =YANTRA_COMP_PROFILE .CP_ID inner join location_tbl  on YANTRA_COMP_PROFILE .locid =location_tbl .locid where YANTRA_EMPLOYEE_MAST.EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS != 0  and location_tbl .locid ='" + locname + "' and YANTRA_DEPT_MAST.DEPT_ID in(34,35,36,37,38) ORDER BY YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME");
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
        }
        public static void EmployeeSelect_location(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("Select * from Location_tbl");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                (ControlForBind as DropDownList).Items.Clear();
                (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
                while (dbManager.DataReader.Read())
                {
                    (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["Description"].ToString() , dbManager.DataReader["locid"].ToString()));
                }
                dbManager.DataReader.Close();
            }
        }
        public static void EmployeeMaster_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0 and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE()  ORDER BY EMP_FIRST_NAME");
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
        }
        public void EmpDOBReminder_Select(GridView gv)
        {
            dbManager.Open();
            _commandText = string.Format("Select YANTRA_EMPLOYEE_MAST.EMP_ID ,EMP_FIRST_NAME +''+ EMP_LAST_NAME as Emp_Name, EMP_GENDER,EMP_DOB,DESG_NAME,ReadRecords_tbl.isread from YANTRA_EMPLOYEE_MAST inner join YANTRA_EMPLOYEE_DET on YANTRA_EMPLOYEE_MAST .EMP_ID =YANTRA_EMPLOYEE_DET .EMP_ID inner join YANTRA_DESG_MAST on YANTRA_EMPLOYEE_DET .DESG_ID =YANTRA_DESG_MAST .DESG_ID left outer join ReadRecords_tbl on YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME  =ReadRecords_tbl.unqid  where DATEPART(m, EMP_DOB)=DATEPART(m,GETDATE()) AND DATEPART(d, EMP_DOB) = DATEPART(d, GETDATE())");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            DataTable DailyAllowance = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("Emp_Id");
            DailyAllowance.Columns.Add(col);
            col = new DataColumn("EMP_Name");
            DailyAllowance.Columns.Add(col);
            col = new DataColumn("EMP_DOB");
            DailyAllowance.Columns.Add(col);
            col = new DataColumn("DESG_NAME");
            DailyAllowance.Columns.Add(col);
            col = new DataColumn("isread");
            DailyAllowance.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = DailyAllowance.NewRow();
                dr["Emp_Id"] = dbManager.DataReader["Emp_Id"].ToString();
                dr["EMP_Name"] = dbManager.DataReader["EMP_Name"].ToString();
                dr["EMP_DOB"] = dbManager.DataReader["EMP_DOB"].ToString();
                dr["DESG_NAME"] = dbManager.DataReader["DESG_NAME"].ToString();
                dr["isread"] = dbManager.DataReader["isread"].ToString();
                DailyAllowance.Rows.Add(dr);
            }
            dbManager.DataReader.Close();
            gv.DataSource = DailyAllowance;
            gv.DataBind();
        }

        public static void EmployeeMaster_Select_Compalint(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0 ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_Select_BioMap(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0 and a.STATUS != 0 and b.EMP_DET_DOT >=GETDATE() and a.EMP_ID not in(select app_emp_id from EMP_BIO_MAP) ORDER BY EMP_FIRST_NAME");
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
        }
        public static void EmployeeMaster_Select1(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] a inner join dbo.YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE a.EMP_ID<>0    ORDER BY EMP_FIRST_NAME");
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
        }

        //Binding Employee With Company Name

        public static void Employee_Company(Control ControlForBind, string Cp_Id)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET],YANTRA_COMP_PROFILE WHERE YANTRA_EMPLOYEE_MAST.EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1  and [YANTRA_EMPLOYEE_MAST].COMPANY_ID=[YANTRA_COMP_PROFILE].cp_id AND YANTRA_EMPLOYEE_MAST.EMP_ID=YANTRA_EMPLOYEE_DET.EMP_ID and [YANTRA_COMP_PROFILE].cp_id=" + Cp_Id + " ORDER BY EMP_FIRST_NAME");
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
        }
        public static void EmployeeMaster_ServiceSelect(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],YANTRA_EMPLOYEE_DET,YANTRA_DEPT_MAST  WHERE YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID and YANTRA_DEPT_MAST.DEPT_ID = YANTRA_EMPLOYEE_DET.DEPT_ID and   YANTRA_EMPLOYEE_MAST.EMP_ID<>0 and YANTRA_DEPT_MAST.DEPT_ID = 6 or  YANTRA_DEPT_MAST.DEPT_ID = 21 or  YANTRA_DEPT_MAST.DEPT_ID = 28  ORDER BY YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME asc");
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
        }

        public static void EmployeeMasterStatus_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST] WHERE EMP_ID<>0 and STATUS = 1 ORDER BY EMP_FIRST_NAME");
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
        }

        //public static void EmployeeMaster_DailyReport(Control ControlForBind)
        //{
        //    dbManager.Open();
        //    _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID=1 and [YANTRA_EMPLOYEE_DET].DEPT_ID=6 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
        //    dbManager.ExecuteReader(CommandType.Text, _commandText);
        //    if (ControlForBind is DropDownList)
        //    {
        //        (ControlForBind as DropDownList).Items.Clear();
        //        (ControlForBind as DropDownList).Items.Add(new ListItem("--", "0"));
        //        while (dbManager.DataReader.Read())
        //        {
        //            (ControlForBind as DropDownList).Items.Add(new ListItem(dbManager.DataReader["EMP_FIRST_NAME"].ToString() + " " + dbManager.DataReader["EMP_LAST_NAME"].ToString(), dbManager.DataReader["EMP_ID"].ToString()));
        //        }
        //        dbManager.DataReader.Close();
        //    }
        //}


        public static void EmployeeMaster_SelectForEDP(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 9 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectForSales(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID = 9 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectForPurchases(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_DET].DEPT_ID=2 AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 and YANTRA_EMPLOYEE_MAST.STATUS = 1 ORDER BY EMP_FIRST_NAME");
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
        }

        public static void EmployeeMaster_SelectFromDepartment(Control ControlForBind, string Depid)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET] WHERE YANTRA_EMPLOYEE_MAST.EMP_ID<>0 AND YANTRA_EMPLOYEE_MAST.EMP_ID=YANTRA_EMPLOYEE_DET.EMP_ID and YANTRA_EMPLOYEE_MAST.STATUS = 1 and YANTRA_EMPLOYEE_DET.DEPT_ID=" + Depid + " ORDER BY EMP_FIRST_NAME");
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
        }

        public void EmployeeMaster_SelectDepartment(string str)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT EMP_ID FROM [YANTRA_EMPLOYEE_MAST] WHERE EMP_FIRST_NAME='" + str + "' Order by EMP_ID desc");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.EmpID = dbManager.DataReader["EMP_ID"].ToString();
            }
            dbManager.DataReader.Close();
        }

        //This method is for temporary use only For Sales & Marketing Module...

        public int UserName_Select(string UserName)
        {
            dbManager.Open();
            _commandText = string.Format(" select * from YANTRA_USER_DETAILS where USER_NAME ='" + UserName + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.UserId  = dbManager.DataReader["USER_ID"].ToString();
                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }
        public int EmployeeMaster_Select(string EmployeeId)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET],[YANTRA_LKUP_EMP_TYPE],YANTRA_COMP_PROFILE,YANTRA_DEPT_MAST,YANTRA_DESG_MAST WHERE " +
                            " [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 AND " +
                              " YANTRA_EMPLOYEE_DET.DEPT_ID=YANTRA_DEPT_MAST.DEPT_ID AND YANTRA_EMPLOYEE_DET.DESG_ID=YANTRA_DESG_MAST.DESG_ID AND " +
                            " [YANTRA_EMPLOYEE_DET].EMP_TYPE_ID=[YANTRA_LKUP_EMP_TYPE].EMP_TYPE_ID and YANTRA_COMP_PROFILE.CP_ID =  YANTRA_EMPLOYEE_MAST.COMPANY_ID AND [YANTRA_EMPLOYEE_MAST].EMP_ID=" + EmployeeId + "");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.Dept_Hod_Id = dbManager.DataReader["DEPT_HEAD"].ToString();

                this.EmpID = dbManager.DataReader["EMP_ID"].ToString();
                this.EmpFirstName = dbManager.DataReader["EMP_FIRST_NAME"].ToString();
                this.EmpMiddleName = dbManager.DataReader["EMP_MIDDLE_NAME"].ToString();
                this.EmpLastName = dbManager.DataReader["EMP_LAST_NAME"].ToString();
                this.EmpGender = dbManager.DataReader["EMP_GENDER"].ToString();
                this.EmpMobile = dbManager.DataReader["EMP_MOBILE"].ToString();
                this.EmpPhone = dbManager.DataReader["EMP_PHONE"].ToString();
                this.EmpDOB = dbManager.DataReader["EMP_DOB"].ToString();
                this.EmpEMail = dbManager.DataReader["EMP_EMAIL"].ToString();
                this.EmpPhoto = dbManager.DataReader["EMP_PHOTO"].ToString();
                this.EmpAddress = dbManager.DataReader["EMP_ADDRESS"].ToString();
                this.EmpCity = dbManager.DataReader["EMP_CITY"].ToString();
                this.DeptID = dbManager.DataReader["DEPT_ID"].ToString();
                this.DesgID = dbManager.DataReader["DESG_ID"].ToString();
                this.EmpDetDOJ = dbManager.DataReader["EMP_DET_DOJ"].ToString();
                this.EmpDetDOT = dbManager.DataReader["EMP_DET_DOT"].ToString();
                this.EmpBranchID = dbManager.DataReader["BRAN_ID"].ToString();
                this.locid = dbManager.DataReader["locid"].ToString();
                
                this.EmpTypeID = dbManager.DataReader["EMP_TYPE_ID"].ToString();
                this.EMPUserName = dbManager.DataReader["EMP_USERNAME"].ToString();
                this.EmpCompany = dbManager.DataReader["COMPANY_ID"].ToString();
                this.DeptName12 = dbManager.DataReader["DEPT_NAME"].ToString();
                this.DesgName12 = dbManager.DataReader["DESG_NAME"].ToString();
                this.ComapanyName = dbManager.DataReader["CP_FULL_NAME"].ToString();
                this.servicebond = dbManager.DataReader["SERVICE_BOND"].ToString();
                this.PerPhoneno = dbManager.DataReader["PERMANET_PHONENO"].ToString();
                this.permobileno = dbManager.DataReader["PERMANET_MOBILENO"].ToString();
                this.emrname = dbManager.DataReader["EMRNAME"].ToString();
                this.emrrelation = dbManager.DataReader["EMGRELATION"].ToString();
                this.emgphone = dbManager.DataReader["EMGPHONE"].ToString();
                this.emgaddress = dbManager.DataReader["EMGADDRESS"].ToString();
                this.Others = dbManager.DataReader["OTHERS"].ToString();
                this.FATHERNAME = dbManager.DataReader["FATHER_NAME"].ToString();
                this.AssignedMobileNo = dbManager.DataReader["ASSIGNED_MOBILENO"].ToString();
                this.AssignedEmailId = dbManager.DataReader["ASSINED_EMAILID"].ToString();
                this.AssignedEmpId = dbManager.DataReader["ASSIGNED_EMPID"].ToString();
                this.AssignedAccNo = dbManager.DataReader["ASSIGNED_ACC_NO"].ToString();
                this.InsuraceInfo = dbManager.DataReader["INSURANCE_INFO"].ToString();
                this.Status = dbManager.DataReader["STATUS"].ToString();
                this.AssignedBankName = dbManager.DataReader["ASSIGNED_BANKNAME"].ToString();
                this.InsuranceCompanyName = dbManager.DataReader["ASSIGNED_INSURANCECOMPANY"].ToString();
                this.GrossSal = dbManager.DataReader["GROSS_SAL"].ToString();
                this.grossdob = dbManager.DataReader["EMP_DOB"].ToString();
                this.Asset1 = dbManager.DataReader["Asset1"].ToString();
                this.Asset2 = dbManager.DataReader["Asset2"].ToString();
                this.Asset3 = dbManager.DataReader["Asset3"].ToString();
                this.Asset4 = dbManager.DataReader["Asset4"].ToString();
                this.Asset5 = dbManager.DataReader["Asset5"].ToString();
                this.Asset6 = dbManager.DataReader["Asset6"].ToString();
                this.ReportingId = dbManager.DataReader["Reporting_ID"].ToString();
                
                //this.SickLeaves = dbManager.DataReader["SickLeaves"].ToString();
                //this.CasualLeaves = dbManager.DataReader["Casual_Leaves"].ToString();
                //this.LeavesEarned = dbManager.DataReader["Earned_Leaves"].ToString();
                if (this.EmpDOB == "1/1/1900 12:00:00 AM") { this.EmpDOB = ""; } else { this.EmpDOB = Convert.ToDateTime(this.EmpDOB).ToString("dd/MM/yyyy"); }
                if (this.EmpDetDOJ == "1/1/1900 12:00:00 AM") { this.EmpDetDOJ = ""; } else { this.EmpDetDOJ = Convert.ToDateTime(this.EmpDetDOJ).ToString("dd/MM/yyyy"); }
                if (this.EmpDetDOT == "1/1/1900 12:00:00 AM") { this.EmpDetDOT = ""; } else { this.EmpDetDOT = Convert.ToDateTime(this.EmpDetDOT).ToString("dd/MM/yyyy"); }

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }
        public int count;





        //public int EmployeeBirthdays_Select()
        //{
        //    dbManager.Open();
        //    _commandText = string.Format("select * from YANTRA_EMPLOYEE_MAST where STATUS !=0 and DATEPART(M,EMP_DOB)=DATEPART(m,getdate()) and DATEPART(d,EMP_DOB)=DATEPART(d,getdate())");
        //    dbManager.ExecuteReader(CommandType.Text, _commandText);
        //    if (dbManager.DataReader.Read())
        //    {
        //        // this.EMPUserName = dbManager.DataReader["EMP_USERNAME"].ToString();
        //        this.count = Convert.ToInt32(dbManager.DataReader["cnt"]);
        //        if (count > 0)
        //        {
        //            _returnIntValue = 1;

        //        }
        //        else
        //        {
        //            _returnIntValue = 0;
        //        }
        //    }
        //    //else
        //    //{
        //    //    _returnIntValue = 0;
        //    //}
        //    dbManager.DataReader.Close();
        //    return _returnIntValue;
        //}












        public int EmployeeUserName_Select(string UserName)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT count(*) as cnt FROM [YANTRA_EMPLOYEE_MAST] where [EMP_USERNAME]='" + UserName + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
               // this.EMPUserName = dbManager.DataReader["EMP_USERNAME"].ToString();
                this.count = Convert.ToInt32(dbManager.DataReader["cnt"]);
                if(count > 0)
                {
                    _returnIntValue = 1;

                }
                else
                {
                    _returnIntValue = 0;
                }
            }
            //else
            //{
            //    _returnIntValue = 0;
            //}
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string EmployeeCTC_Update(string EmployeeId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //  _commandText = string.Format("UPDATE YANTRA_EMPLOYEE_MAST SET EMP_FIRST_NAME='{0}',EMP_MIDDLE_NAME='{1}',EMP_LAST_NAME='{2}',EMP_GENDER='{3}',EMP_MOBILE='{4}',EMP_PHONE='{5}',EMP_DOB='{6}',EMP_EMAIL='{7}',EMP_PHOTO='{8}',EMP_ADDRESS='{9}',EMP_CITY='{10}',EMP_USERNAME='{11}',COMPANY_ID ={12},SERVICE_BOND = '{13}',PERMANET_PHONENO = '{14}',PERMANET_MOBILENO = '{15}',EMRNAME = '{16}',EMGRELATION ='{17}',EMGPHONE = '{18}',EMGADDRESS ='{19}',CP_ID = '{20}',OTHERS = '{21}',FATHER_NAME = '{22}',ASSIGNED_MOBILENO='{24}',ASSINED_EMAILID='{25}',ASSIGNED_EMPID={26},ASSIGNED_ACC_NO='{27}',INSURANCE_INFO='{28}',STATUS = {29} where EMP_ID={23}", EmpFirstName, EmpMiddleName, EmpLastName, EmpGender, EmpMobile, EmpPhone, EmpDOB, EmpEMail, tEmpPhoto, EmpAddress, EmpCity, EMPUserName, EmpCompany, servicebond, PerPhoneno, permobileno, emrname, emrrelation, emgphone, emgaddress, Cp_Id, Others, FATHERNAME, EmployeeId, AssignedMobileNo, AssignedEmailId, AssignedEmpId, AssignedAccNo, InsuraceInfo,Status);

            _commandText = string.Format("UPDATE YANTRA_EMPLOYEE_MAST SET GROSS_SAL = '{1}' where EMP_ID={0}", EmployeeId, GrossSal);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                // log.add_Update("Employee Details", "50");

            }
            return _returnStringMessage;
        }

        public string Employee_Sal_His_Update(string EmployeeId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //EmpFirstName

            _commandText = string.Format("INSERT INTO Salary_hist values ('{0}',{1},'{2}',{3}) ", EmpFirstName, CurrentCTC, DateTime.Now.ToString(), EmployeeId);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                // log.add_Update("Employee Details", "50");

            }
            return _returnStringMessage;
        }
        public int GetAge(int EmpId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select DATEDIFF(yyyy,emp_dob,getdate()) from [YANTRA_EMPLOYEE_MAST] where EMP_ID=" + EmpId + "");
            _returnIntValue = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, _commandText));
            return _returnIntValue;
        }
        public int UsersEmployeeMaster_Select(string EmployeeId)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET],[YANTRA_LKUP_EMP_TYPE],YANTRA_COMP_PROFILE,YANTRA_DEPT_MAST,YANTRA_DESG_MAST WHERE " +
                            " [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 AND " +
                              " YANTRA_EMPLOYEE_DET.DEPT_ID=YANTRA_DEPT_MAST.DEPT_ID AND YANTRA_EMPLOYEE_DET.DESG_ID=YANTRA_DESG_MAST.DESG_ID AND " +
                            " [YANTRA_EMPLOYEE_DET].EMP_TYPE_ID=[YANTRA_LKUP_EMP_TYPE].EMP_TYPE_ID and YANTRA_COMP_PROFILE.CP_ID =  YANTRA_EMPLOYEE_MAST.COMPANY_ID AND [YANTRA_EMPLOYEE_MAST].EMP_ID=" + EmployeeId + "");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.EmpID = dbManager.DataReader["EMP_ID"].ToString();
                this.EmpFirstName = dbManager.DataReader["EMP_FIRST_NAME"].ToString();
                this.EmpMiddleName = dbManager.DataReader["EMP_MIDDLE_NAME"].ToString();
                this.EmpLastName = dbManager.DataReader["EMP_LAST_NAME"].ToString();
                this.EmpGender = dbManager.DataReader["EMP_GENDER"].ToString();
                this.EmpMobile = dbManager.DataReader["EMP_MOBILE"].ToString();
                this.EmpPhone = dbManager.DataReader["EMP_PHONE"].ToString();
                this.EmpDOB = dbManager.DataReader["EMP_DOB"].ToString();
                this.EmpEMail = dbManager.DataReader["EMP_EMAIL"].ToString();
                this.EmpPhoto = dbManager.DataReader["EMP_PHOTO"].ToString();
                this.EmpAddress = dbManager.DataReader["EMP_ADDRESS"].ToString();
                this.EmpCity = dbManager.DataReader["EMP_CITY"].ToString();
                this.DeptID = dbManager.DataReader["DEPT_ID"].ToString();
                this.DesgID = dbManager.DataReader["DESG_ID"].ToString();
                this.EmpDetDOJ = dbManager.DataReader["EMP_DET_DOJ"].ToString();
                this.EmpDetDOT = dbManager.DataReader["EMP_DET_DOT"].ToString();
                this.EmpBranchID = dbManager.DataReader["BRAN_ID"].ToString();
                this.locid = dbManager.DataReader["locid"].ToString();

                this.EmpTypeID = dbManager.DataReader["EMP_TYPE_ID"].ToString();
                this.EMPUserName = dbManager.DataReader["EMP_USERNAME"].ToString();
                this.EmpCompany = dbManager.DataReader["COMPANY_ID"].ToString();
                this.DeptName12 = dbManager.DataReader["DEPT_NAME"].ToString();
                this.DesgName12 = dbManager.DataReader["DESG_NAME"].ToString();
                this.ComapanyName = dbManager.DataReader["CP_FULL_NAME"].ToString();
                this.servicebond = dbManager.DataReader["SERVICE_BOND"].ToString();
                this.PerPhoneno = dbManager.DataReader["PERMANET_PHONENO"].ToString();
                this.permobileno = dbManager.DataReader["PERMANET_MOBILENO"].ToString();
                this.emrname = dbManager.DataReader["EMRNAME"].ToString();
                this.emrrelation = dbManager.DataReader["EMGRELATION"].ToString();
                this.emgphone = dbManager.DataReader["EMGPHONE"].ToString();
                this.emgaddress = dbManager.DataReader["EMGADDRESS"].ToString();
                this.Others = dbManager.DataReader["OTHERS"].ToString();
                this.FATHERNAME = dbManager.DataReader["FATHER_NAME"].ToString();
                this.AssignedMobileNo = dbManager.DataReader["ASSIGNED_MOBILENO"].ToString();
                this.AssignedEmailId = dbManager.DataReader["ASSINED_EMAILID"].ToString();
                this.AssignedEmpId = dbManager.DataReader["ASSIGNED_EMPID"].ToString();
                this.AssignedAccNo = dbManager.DataReader["ASSIGNED_ACC_NO"].ToString();
                this.InsuraceInfo = dbManager.DataReader["INSURANCE_INFO"].ToString();
                this.Status = dbManager.DataReader["STATUS"].ToString();
         
                if (this.EmpDOB == "1/1/1900 12:00:00 AM") { this.EmpDOB = ""; } else { this.EmpDOB = Convert.ToDateTime(this.EmpDOB).ToString("dd/MM/yyyy"); }
                if (this.EmpDetDOJ == "1/1/1900 12:00:00 AM") { this.EmpDetDOJ = ""; } else { this.EmpDetDOJ = Convert.ToDateTime(this.EmpDetDOJ).ToString("dd/MM/yyyy"); }
                if (this.EmpDetDOT == "1/1/1900 12:00:00 AM") { this.EmpDetDOT = ""; } else { this.EmpDetDOT = Convert.ToDateTime(this.EmpDetDOT).ToString("dd/MM/yyyy"); }

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }


        //This method is for temporary use only For Sales & Marketing Module...
        public int UserEmployeeMaster_Select(string EmployeeId)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_EMPLOYEE_DET],[YANTRA_LKUP_EMP_TYPE],YANTRA_COMP_PROFILE,YANTRA_DEPT_MAST,YANTRA_DESG_MAST WHERE " +
                            " [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_EMPLOYEE_DET].EMP_ID AND [YANTRA_EMPLOYEE_MAST].EMP_ID<>0 AND " +
                              " YANTRA_EMPLOYEE_DET.DEPT_ID=YANTRA_DEPT_MAST.DEPT_ID AND YANTRA_EMPLOYEE_DET.DESG_ID=YANTRA_DESG_MAST.DESG_ID AND " +
                            " [YANTRA_EMPLOYEE_DET].EMP_TYPE_ID=[YANTRA_LKUP_EMP_TYPE].EMP_TYPE_ID and YANTRA_COMP_PROFILE.CP_ID =  YANTRA_EMPLOYEE_MAST.COMPANY_ID and [YANTRA_EMPLOYEE_MAST].EMP_ID=" + EmployeeId + "");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.EmpID = dbManager.DataReader["EMP_ID"].ToString();
                this.EmpFirstName = dbManager.DataReader["EMP_FIRST_NAME"].ToString();
                this.EmpMiddleName = dbManager.DataReader["EMP_MIDDLE_NAME"].ToString();
                this.EmpLastName = dbManager.DataReader["EMP_LAST_NAME"].ToString();
                this.EmpGender = dbManager.DataReader["EMP_GENDER"].ToString();
                this.EmpMobile = dbManager.DataReader["EMP_MOBILE"].ToString();
                this.EmpPhone = dbManager.DataReader["EMP_PHONE"].ToString();
                this.EmpDOB = dbManager.DataReader["EMP_DOB"].ToString();
                this.EmpEMail = dbManager.DataReader["EMP_EMAIL"].ToString();
                this.EmpPhoto = dbManager.DataReader["EMP_PHOTO"].ToString();
                this.EmpAddress = dbManager.DataReader["EMP_ADDRESS"].ToString();
                this.EmpCity = dbManager.DataReader["EMP_CITY"].ToString();
                this.DeptID = dbManager.DataReader["DEPT_ID"].ToString();
                this.DesgID = dbManager.DataReader["DESG_ID"].ToString();
                this.EmpDetDOJ = dbManager.DataReader["EMP_DET_DOJ"].ToString();
                this.EmpDetDOT = dbManager.DataReader["EMP_DET_DOT"].ToString();
                this.EmpBranchID = dbManager.DataReader["BRAN_ID"].ToString();
                this.locid = dbManager.DataReader["locid"].ToString();

                this.EmpTypeID = dbManager.DataReader["EMP_TYPE_ID"].ToString();
                this.EMPUserName = dbManager.DataReader["EMP_USERNAME"].ToString();
                this.EmpCompany = dbManager.DataReader["COMPANY_ID"].ToString();
                this.DeptName12 = dbManager.DataReader["DEPT_NAME"].ToString();
                this.DesgName12 = dbManager.DataReader["DESG_NAME"].ToString();
                this.ComapanyName = dbManager.DataReader["CP_FULL_NAME"].ToString();
                this.servicebond = dbManager.DataReader["SERVICE_BOND"].ToString();
                this.PerPhoneno = dbManager.DataReader["PERMANET_PHONENO"].ToString();
                this.permobileno = dbManager.DataReader["PERMANET_MOBILENO"].ToString();
                this.emrname = dbManager.DataReader["EMRNAME"].ToString();
                this.emrrelation = dbManager.DataReader["EMGRELATION"].ToString();
                this.emgphone = dbManager.DataReader["EMGPHONE"].ToString();
                this.emgaddress = dbManager.DataReader["EMGADDRESS"].ToString();
                this.Others = dbManager.DataReader["OTHERS"].ToString();
                this.FATHERNAME = dbManager.DataReader["FATHER_NAME"].ToString();
                this.AssignedMobileNo = dbManager.DataReader["ASSIGNED_MOBILENO"].ToString();
                this.AssignedEmailId = dbManager.DataReader["ASSINED_EMAILID"].ToString();
                this.AssignedEmpId = dbManager.DataReader["ASSIGNED_EMPID"].ToString();
                this.AssignedAccNo = dbManager.DataReader["ASSIGNED_ACC_NO"].ToString();
                this.InsuraceInfo = dbManager.DataReader["INSURANCE_INFO"].ToString();
                this.Status = dbManager.DataReader["STATUS"].ToString();
               
                //this.SickLeaves = dbManager.DataReader["SickLeaves"].ToString();
               
                if (this.EmpDOB == "1/1/1900 12:00:00 AM") { this.EmpDOB = ""; } else { this.EmpDOB = Convert.ToDateTime(this.EmpDOB).ToString("dd/MM/yyyy"); }
                if (this.EmpDetDOJ == "1/1/1900 12:00:00 AM") { this.EmpDetDOJ = ""; } else { this.EmpDetDOJ = Convert.ToDateTime(this.EmpDetDOJ).ToString("dd/MM/yyyy"); }
                if (this.EmpDetDOT == "1/1/1900 12:00:00 AM") { this.EmpDetDOT = ""; } else { this.EmpDetDOT = Convert.ToDateTime(this.EmpDetDOT).ToString("dd/MM/yyyy"); }

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }


        public static string GetEmployeeEmail(string EmployeeId)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT EMP_EMAIL FROM [YANTRA_EMPLOYEE_MAST] WHERE EMP_ID=" + EmployeeId + "");
            _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
            return _returnStringMessage;
        }
        public static string GetEmailPass(string Email)
        {
            dbManager.Open();
            _commandText = string.Format("SELECT smartpassword FROM [smart_emails] WHERE smartemail='" + Email + "'");
            _returnStringMessage = dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString();
            return _returnStringMessage;
        }

        public int tMaxEmpId;
        public string newEmpId;

        public string NewEmp_Id()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();

            _commandText = string.Format("SELECT ISNULL(MAX(EMP_ID),0)+1 FROM YANTRA_EMPLOYEE_MAST");

            dbManager.ExecuteReader(CommandType.Text, _commandText);
            while (dbManager.DataReader.Read())
            {
                newEmpId = dbManager.DataReader[0].ToString();
            }
            dbManager.DataReader.Close();
            return newEmpId;
        }
        public string Empoyee_Save()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO YANTRA_EMPLOYEE_MAST SELECT ISNULL(MAX(EMP_ID),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}','{19}',{20},'{21}','{22}','{23}','{24}',{25},'{26}','{27}',{28},'{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}' FROM YANTRA_EMPLOYEE_MAST", EmpFirstName, EmpMiddleName, EmpLastName, EmpGender, EmpMobile, EmpPhone, EmpDOB, EmpEMail, tEmpPhoto, EmpAddress, EmpCity, EMPUserName, EmpCompany, servicebond, PerPhoneno, permobileno, emrname, emrrelation, emgphone, emgaddress, Cp_Id, Others, FATHERNAME, AssignedMobileNo, AssignedEmailId, AssignedEmpId, AssignedAccNo, InsuraceInfo, Status, GrossSal, AssignedBankName, InsuranceCompanyName, Asset1, Asset2, Asset3, Asset4, Asset5, Asset6);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _commandText = string.Format("SELECT MAX(EMP_ID) FROM YANTRA_EMPLOYEE_MAST");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            while (dbManager.DataReader.Read())
            {
                tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
            }
            dbManager.DataReader.Close();
            _commandText = string.Format("INSERT INTO YANTRA_EMPLOYEE_DET SELECT ISNULL(MAX(EMP_DET_ID),0)+1,{0},{1},{2},'{3}','{4}',{5},{6},{7} FROM YANTRA_EMPLOYEE_DET", tMaxEmpId, DeptID, DesgID, EmpDetDOJ, EmpDetDOT, BranchId, EmpTypeID,ReportingId );
            dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            //_commandText = string.Format("INSERT INTO YANTRA_LEAVE_TYPE SELECT ISNULL(MAX(LeaveType_ID),0)+1,{0},'{1}','{2}','{3}' FROM YANTRA_LEAVE_TYPE", tMaxEmpId, CasualLeaves, SickLeaves, LeavesEarned);
            //dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                //EmpFirstName, EmpMiddleName, EmpLastName,
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert(EmpFirstName +" "+ EmpLastName + " "+ "Employee Details Inserted", "50");

            }
            return _returnStringMessage;
        }
        public string Update_UserExpDate(string UserId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText =string .Format ("Update YANTRA_USER_DETAILS SET EXPIRY_DATE='{0}' Where USER_ID='{1}'",this.ExpiryDate ,this.UserId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                //log.add_Insert(EmpFirstName + " " + EmpLastName + " " + "Employee Details Updated", "50");
                //log.add_Update("Employee Details", "50");

            }
            
            return _returnStringMessage;

        }

        public string Update_EmpExpDate(string UserId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("Update YANTRA_EMPLOYEE_DET SET EMP_DET_DOT='{0}' Where EMP_ID='{1}'", this.ExpiryDate, this.UserId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                //log.add_Insert(EmpFirstName + " " + EmpLastName + " " + "Employee Details Updated", "50");
                //log.add_Update("Employee Details", "50");

            }

            return _returnStringMessage;

        }
        public string Employee_Update(string EmployeeId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //  _commandText = string.Format("UPDATE YANTRA_EMPLOYEE_MAST SET EMP_FIRST_NAME='{0}',EMP_MIDDLE_NAME='{1}',EMP_LAST_NAME='{2}',EMP_GENDER='{3}',EMP_MOBILE='{4}',EMP_PHONE='{5}',EMP_DOB='{6}',EMP_EMAIL='{7}',EMP_PHOTO='{8}',EMP_ADDRESS='{9}',EMP_CITY='{10}',EMP_USERNAME='{11}',COMPANY_ID ={12},SERVICE_BOND = '{13}',PERMANET_PHONENO = '{14}',PERMANET_MOBILENO = '{15}',EMRNAME = '{16}',EMGRELATION ='{17}',EMGPHONE = '{18}',EMGADDRESS ='{19}',CP_ID = '{20}',OTHERS = '{21}',FATHER_NAME = '{22}',ASSIGNED_MOBILENO='{24}',ASSINED_EMAILID='{25}',ASSIGNED_EMPID={26},ASSIGNED_ACC_NO='{27}',INSURANCE_INFO='{28}',STATUS = {29} where EMP_ID={23}", EmpFirstName, EmpMiddleName, EmpLastName, EmpGender, EmpMobile, EmpPhone, EmpDOB, EmpEMail, tEmpPhoto, EmpAddress, EmpCity, EMPUserName, EmpCompany, servicebond, PerPhoneno, permobileno, emrname, emrrelation, emgphone, emgaddress, Cp_Id, Others, FATHERNAME, EmployeeId, AssignedMobileNo, AssignedEmailId, AssignedEmpId, AssignedAccNo, InsuraceInfo,Status);

            _commandText = string.Format("UPDATE YANTRA_EMPLOYEE_MAST SET EMP_FIRST_NAME='{0}',EMP_MIDDLE_NAME='{1}',EMP_LAST_NAME='{2}',EMP_GENDER='{3}',EMP_MOBILE='{4}',EMP_PHONE='{5}',EMP_DOB='{6}',EMP_EMAIL='{7}',EMP_ADDRESS='{8}',EMP_CITY='{9}',EMP_USERNAME='{10}',COMPANY_ID ={11},SERVICE_BOND = '{12}',PERMANET_PHONENO = '{13}',PERMANET_MOBILENO = '{14}',EMRNAME = '{15}',EMGRELATION ='{16}',EMGPHONE = '{17}',EMGADDRESS ='{18}',CP_ID = '{19}',OTHERS = '{20}',FATHER_NAME = '{21}',ASSIGNED_MOBILENO='{23}',ASSINED_EMAILID='{24}',ASSIGNED_EMPID={25},ASSIGNED_ACC_NO='{26}',INSURANCE_INFO='{27}',STATUS = {28},GROSS_SAL = '{29}',ASSIGNED_BANKNAME='{30}',ASSIGNED_INSURANCECOMPANY='{31}',Asset1='{32}',Asset2='{33}',Asset3='{34}',Asset4='{35}',Asset5='{36}',Asset6='{37}' where EMP_ID={22}", EmpFirstName, EmpMiddleName, EmpLastName, EmpGender, EmpMobile, EmpPhone, EmpDOB, EmpEMail, EmpAddress, EmpCity, EMPUserName, EmpCompany, servicebond, PerPhoneno, permobileno, emrname, emrrelation, emgphone, emgaddress, Cp_Id, Others, FATHERNAME, EmployeeId, AssignedMobileNo, AssignedEmailId, AssignedEmpId, AssignedAccNo, InsuraceInfo, Status, GrossSal, AssignedBankName, InsuranceCompanyName, Asset1, Asset2, Asset3, Asset4, Asset5, Asset6);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _commandText = string.Format("UPDATE YANTRA_EMPLOYEE_DET SET DEPT_ID={0},DESG_ID={1},EMP_DET_DOJ='{2}',EMP_DET_DOT='{3}',BRAN_ID={4},EMP_TYPE_ID={5},Reporting_ID={6} where EMP_ID={7}", DeptID, DesgID, EmpDetDOJ, EmpDetDOT, BranchId, EmpTypeID, ReportingId, EmployeeId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Insert(EmpFirstName + " " + EmpLastName+" " + "Employee Details Updated", "50");
                //log.add_Update("Employee Details", "50");

            }
            return _returnStringMessage;
        }

        public string empcode, empname, appempid, appempname, dateadded;

        public string biomapping_update()
        {
            if(dbManager.Transaction == null)
                dbManager.Close();

                dbManager.Open();
            _commandText = string.Format("UPDATE EMP_BIO_MAP SET app_emp_id={0},app_emp_name='{1}',dateadded='{4}' where emp_code={2} and emp_name='{3}'", appempid, appempname,empcode, empname, DateTime.Now.ToString("MM/dd/yyyy"));
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
            }
            dbManager.Close();

            return _returnStringMessage;
        }


        public string IsRecordExists(string paraFieldValue)
        {

            dbManager.Close();
            dbManager.Open();

            string check = "0";
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM EMP_BIO_MAP WHERE emp_code ='" + paraFieldValue + "'").ToString());
            if (_returnIntValue > 0)
            {
                check = "1";
            }
            else
            {
                check = "0";
            }
            return check;
            dbManager.Close();

        }

        public string RecordExists(string empcode,string paraFieldValue1)
        {
            dbManager.Close();
            dbManager.Open();

            string check = "0";
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*) FROM Emp_Attendance WHERE Att_Date ='" + paraFieldValue1 + "' and Emp_Code ='"+empcode+"' ").ToString());
            if (_returnIntValue > 0)
            {
                check = "1";
            }
            else
            {
                check = "0";
            }
            return check;
            dbManager.Close();

        }






        public string Employee_Delete(string EmployeeId)
        {
            if (DeleteRecord("[YANTRA_EMPLOYEE_DET]", "EMP_ID", EmployeeId) == true)
            {
                if (DeleteRecord("[YANTRA_EMPLOYEE_MAST]", "EMP_ID", EmployeeId) == true)
                {
                    if (DeleteRecord("YANTRA_LEAVE_TYPE", "Emp_Id", EmployeeId) == true)
                    {
                        _returnStringMessage = "Data Deleted Successfully";
                        log.add_Insert(EmpFirstName + " " + EmpLastName+" " + "Employee Details Deleted", "50");
                        //log.add_Delete("Employee Details", "50");

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

        public string EmpDocDetails_Save()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //_commandText = string.Format("SELECT MAX(EMP_ID) FROM YANTRA_EMPLOYEE_MAST");
            //dbManager.ExecuteReader(CommandType.Text, _commandText);
            //while (dbManager.DataReader.Read())
            //{
            //    tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
            //}
            //dbManager.Close();
            _commandText = string.Format("INSERT INTO Emp_Documents_Submitted SELECT ISNULL(MAX(Doc_Id),0)+1,{0},'{1}','{2}','{3}','{4}' FROM Emp_Documents_Submitted", EmpID , DocSubmit, DateSubmitted, DocSubmit1,DOcRewardId );
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            return _returnStringMessage;
        }
        public string EmpDocDetails_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();

            _commandText = string.Format("INSERT INTO Emp_Documents_Submitted SELECT ISNULL(MAX(Doc_Id),0)+1,{0},'{1}','{2}','{3}' FROM Emp_Documents_Submitted", tMaxEmpId, DocSubmit, DateSubmitted, DocSubmit1);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            return _returnStringMessage;
        }
        public string Empdoc_save()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT MAX(EMP_ID) FROM YANTRA_EMPLOYEE_MAST");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            while (dbManager.DataReader.Read())
            {
                tMaxEmpId = int.Parse(dbManager.DataReader[0].ToString());
            }
            dbManager.DataReader.Close();
            _commandText = string.Format("INSERT INTO YANTRA_EMP_DOCUMENTS_SUBMITTED SELECT ISNULL(MAX(EMP_DOC_SUBMITTED_ID),0)+1,{0},'{1}' FROM YANTRA_EMP_DOCUMENTS_SUBMITTED", tMaxEmpId, DocSubmit);
            dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Employee Document Details", "51");

            }
            return _returnStringMessage;
        }

        public string Empdoc_Update(string Empid)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO YANTRA_EMP_DOCUMENTS_SUBMITTED SELECT ISNULL(MAX(EMP_DOC_SUBMITTED_ID),0)+1,{0},'{1}' FROM YANTRA_EMP_DOCUMENTS_SUBMITTED", Empid, DocSubmit);
            dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Update("Employee Document Details", "51");

            }
            return _returnStringMessage;
        }

        public int Empdoc_Delete(string Empid)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("DELETE FROM [YANTRA_EMP_DOCUMENTS_SUBMITTED] WHERE EMP_ID={0}", Empid);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            return _returnIntValue;
        }
        public string Emp_photo,Item_Path;

        public int Item_techimg_update(string itemcode)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("Update YANTRA_ITEM_SPECIFICATION_IMAGE set Item_Specification_Image='{0}',Item_Path='{1}' WHERE ITEM_CODE={2}", Emp_photo, Item_Path, itemcode);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            dbManager.Close();
            return _returnIntValue;
        }
        public int SpareItem_img_update(string itemcode)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("Update YANTRA_ITEM_ATTACHMENTS set Item_attachments='{0}',Item_Path='{1}' WHERE ITEM_CODE={2}", Emp_photo, Item_Path, itemcode);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            dbManager.Close();
            return _returnIntValue;
        }
        public int Emp_Doc_Update(string itemcode)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("update Emp_Documents_Submitted set Document_Submitted ='{0}',Date_Submitted ='{1}' where Doc_Id ='{2}'", Emp_photo, DateTime .Now .ToString (), itemcode);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            dbManager.Close();
            return _returnIntValue;
        }
        public int Item_img_update(string itemcode)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("Update YANTRA_ITEM_IMAGE set Item_Image='{0}',Item_Path='{1}' WHERE ITEM_CODE={2}", Emp_photo,Item_Path , itemcode);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            dbManager.Close();
            return _returnIntValue;
        }
        public int Emp_Img_Update(string Empid)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("Update YANTRA_EMPLOYEE_MAST set EMP_PHOTO='{0}' WHERE EMP_ID={1}", Emp_photo, Empid);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            dbManager.Close();
            return _returnIntValue;
        }
        #region EmpDoc Select
        public DataTable EmpDoc_Select(int EmpId)
        {
            DataTable dtable = new DataTable();
            DataColumn dcol = new DataColumn();
            dcol = new DataColumn("DOCUMENTS_SUBMITTED");
            dtable.Columns.Add(dcol);

            dbManager.Open();
            _commandText = string.Format("SELECT * FROM YANTRA_EMP_DOCUMENTS_SUBMITTED WHERE EMP_ID={0}", EmpId);
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            while (dbManager.DataReader.Read())
            {
                DataRow drow = dtable.NewRow();
                drow["DOCUMENTS_SUBMITTED"] = dbManager.DataReader[2].ToString();
                dtable.Rows.Add(drow);

            }
            dbManager.DataReader.Close();
            return dtable;

        }
        #endregion


        public string EmployeeLeave_Update(string EmployeeId)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();

            _commandText = string.Format("UPDATE YANTRA_LEAVE_TYPE SET CasualLeaves='{0}',SickLeaves='{1}',LeavesEarned='{2}' where EMP_ID={3}", CasualLeaves, SickLeaves, LeavesEarned, EmployeeId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Employee Document Details", "52");


            }
            return _returnStringMessage;
        }
    }

    public class Asset
    {
        public string Asset_Id, Asset_No, Product_Name, Description, Category_Id, IT_Type_Id, Manufacturer, Vendor, PONo, PO_Dt, Warrenty, Expiry_Dt, Cost, Discount, TotalCost, Barcode, Asset_ManagedBy, Remarks, Updated_Dt, location,Status;

        public Asset() { }
        public static string Asset_AutoGenCode()
        {
            return HR.AutoGenMaxNo("Asset_Tbl", "Asset_No");

        }

        public string Asset_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            this.Asset_Id = AutoGenMaxId("[Asset_Tbl]", "Asset_Id");

            _commandText = string.Format("INSERT INTO [Asset_Tbl] SELECT ISNULL(MAX(Asset_Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12},{13},'{14}','{15}','{16}','{17}','{18}','{19}' from [Asset_Tbl] ", this.Asset_No, this.Product_Name, this.Description, this.Category_Id, this.IT_Type_Id, this.Manufacturer, this.Vendor, this.PONo, this.PO_Dt, this.Warrenty, this.Expiry_Dt, this.Cost, this.Discount, this.TotalCost, this.Barcode, this.Asset_ManagedBy, this.Remarks, this.Updated_Dt, this.Status,this.location );

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");
            }
            return _returnStringMessage;
        }

        public int Asset_Select(string AssetID)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select * from Asset_Tbl where Asset_Id ='" + AssetID + "' ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.Asset_Id = dbManager.DataReader["Asset_Id"].ToString();
                this.Asset_No = dbManager.DataReader["Asset_No"].ToString();
                this.Expiry_Dt = Convert.ToDateTime(dbManager.DataReader["Expiry_Dt"].ToString()).ToString("dd/MM/yyyy");
                this.Product_Name = dbManager.DataReader["Product_Name"].ToString();
                this.Description = dbManager.DataReader["Description"].ToString();
                this.Category_Id = dbManager.DataReader["Category_Id"].ToString();
                this.IT_Type_Id = dbManager.DataReader["IT_Type_Id"].ToString();
                this.PO_Dt = Convert.ToDateTime(dbManager.DataReader["PO_Dt"].ToString()).ToString("dd/MM/yyyy");
                //this.ArrivalDate = Convert.ToDateTime(dbManager.DataReader["ArrivalDate"].ToString()).ToString("dd/MM/yyyy");
                this.Manufacturer = dbManager.DataReader["Manufacturer"].ToString();
                this.Vendor = dbManager.DataReader["Vendor"].ToString();
                this.PONo = dbManager.DataReader["PONo"].ToString();
                this.Warrenty = dbManager.DataReader["Warrenty"].ToString();
                this.Cost = dbManager.DataReader["Cost"].ToString();
                this.Discount = dbManager.DataReader["Discount"].ToString();
                this.TotalCost = dbManager.DataReader["TotalCost"].ToString();
                this.Barcode = dbManager.DataReader["Barcode"].ToString();
                this.Asset_ManagedBy = dbManager.DataReader["Asset_ManagedBy"].ToString();
                this.Remarks = dbManager.DataReader["Remarks"].ToString();
                this.Status = dbManager.DataReader["Status"].ToString();
                this.location = dbManager.DataReader["location"].ToString();
                //this.Description = dbManager.DataReader["Description"].ToString();

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

        #region Asset Update
        public string Asset_Update()
        {
            dbManager.Open();

            if (true)
            {
                _commandText = string.Format("UPDATE [Asset_tbl] SET  Asset_No='{0}',Product_Name='{1}',Description='{2}',Category_Id='{3}',IT_Type_Id='{4}',Manufacturer='{5}',Vendor='{6}',PONo='{7}',PO_Dt='{8}',Warrenty='{9}',Expiry_Dt='{10}',Cost={11},Discount={12},TotalCost={13},Barcode='{14}',Asset_ManagedBy='{15}',Remarks='{16}',Status='{17}',Location='{18}' WHERE Asset_Id={19}", this.Asset_No , this.Product_Name , this.Description ,this.Category_Id , this.IT_Type_Id , this.Manufacturer ,this.Vendor,this.PONo ,this.PO_Dt ,this.Warrenty,this.Expiry_Dt ,this.Cost ,this.Discount ,this.TotalCost ,this.Barcode ,this.Asset_ManagedBy ,this.Remarks ,this.Status ,this.location ,this.Asset_Id  );
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";

                    log.add_Update("aSSET Details", "39");

                }
            }
            else
            {
            }
            return _returnStringMessage;
        }
        #endregion

        public string Asset_Delete(string AssetId)
        {
            if (DeleteRecord("[Asset_tbl]", "Asset_Id", AssetId) == true)
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

    #region Tour_Expanses

    public class Tour_Expanses
    {
        public string TourId, TourNo, EmpId, Tour_Date, FromLocation, PlaceOfVisit, NoOfDays, DeptDate, ArrivalDate, TotalTravel, TotalLocalConveyance, TotalLodging, TotalFooding, TotalTourExpanses, TicketsByComp, HotelBillsByComp, Advance, BalanceAmt, PreparedBy;
        public string FromLoc, ToLoc, ModeOfTravel, Kms, Class, Amount, Remarks;
        public string FromDate, ToDate, TotalHrs, DA, Incidental,HotelName,HotelAddress,DayTrafiee,EligibleTrafiee;
        public Tour_Expanses() { }

        public static string Expanses_AutoGenCode()
        {
            return HR.AutoGenMaxNo("Tour_Expanses", "TourNo");

        }

        public int TourExpanses_Select(string TourId)
        {
            if (dbManager.Transaction == null)
                    dbManager.Open();
            _commandText = string.Format("select * from Tour_Expanses where TourId ='" + TourId  + "' ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.TourId = dbManager.DataReader["TourId"].ToString();
                this.TourNo = dbManager.DataReader["TourNo"].ToString();
                this.Tour_Date = Convert.ToDateTime(dbManager.DataReader["Tour_Date"].ToString()).ToString("dd/MM/yyyy");
                this.EmpId = dbManager.DataReader["EmpId"].ToString();
                this.FromLocation = dbManager.DataReader["FromLocation"].ToString();
                this.PlaceOfVisit = dbManager.DataReader["PlaceOfVisit"].ToString();
                this.NoOfDays  = dbManager.DataReader["Noofdays"].ToString();
                this.DeptDate = Convert.ToDateTime(dbManager.DataReader["DeptDate"].ToString()).ToString("dd/MM/yyyy");
                this.ArrivalDate = Convert.ToDateTime(dbManager.DataReader["ArrivalDate"].ToString()).ToString("dd/MM/yyyy");
                this.TotalTravel = dbManager.DataReader["TotalTravel"].ToString();
                this.TotalLocalConveyance  = dbManager.DataReader["TotalLocalConvyance"].ToString();
                this.TotalLodging = dbManager.DataReader["TotalLodging"].ToString();
                this.TotalFooding = dbManager.DataReader["TotalFooding"].ToString();
                this.TotalTourExpanses  = dbManager.DataReader["TotalTourExapnses"].ToString();
                this.TicketsByComp = dbManager.DataReader["TicketsByComp"].ToString();
                this.HotelBillsByComp = dbManager.DataReader["HotelBillsByComp"].ToString();
                this.Advance  = dbManager.DataReader["Adavance"].ToString();
                this.BalanceAmt = dbManager.DataReader["BalanceAmt"].ToString();
                this.PreparedBy = dbManager.DataReader["PreparedBy"].ToString();
                this.TicketsByComp = dbManager.DataReader["TicketsByComp"].ToString();
                this.HotelBillsByComp = dbManager.DataReader["HotelBillsByComp"].ToString();

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

        public void Tour_TravelExpanses_Select(string TourId, GridView gv)
        {
            if (dbManager.Transaction == null)
                    dbManager.Open();
            _commandText = string.Format("select * from Tour_TravelExpanses  where TourId ='" + TourId + "' Order by TravelId ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);

            DataTable Travel = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("TravelId");
            Travel.Columns.Add(col);
            col = new DataColumn("TourId");
            Travel.Columns.Add(col);
            col = new DataColumn("FromLoc");
            Travel.Columns.Add(col);
            col = new DataColumn("ToLoc");
            Travel.Columns.Add(col);
            col = new DataColumn("ModeOfTravel");
            Travel.Columns.Add(col);
            col = new DataColumn("Class");
            Travel.Columns.Add(col);
            col = new DataColumn("Amount");
            Travel.Columns.Add(col);
            col = new DataColumn("Remarks");
            Travel.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = Travel.NewRow();
                dr["TravelId"] = dbManager.DataReader["TravelId"].ToString();
                dr["TourId"] = dbManager.DataReader["TourId"].ToString();

                dr["FromLoc"] = dbManager.DataReader["FromLoc"].ToString();
                dr["ToLoc"] = dbManager.DataReader["ToLoc"].ToString();
                dr["ModeOfTravel"] = dbManager.DataReader["ModeOfTravel"].ToString();
                dr["Class"] = dbManager.DataReader["Class"].ToString();
                dr["Amount"] = dbManager.DataReader["Amount"].ToString();
                dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                Travel.Rows.Add(dr);

            }
            dbManager.DataReader.Close();
            gv.DataSource = Travel;
            gv.DataBind();
        }

        public void Tour_LocalConveyance_Select(string TourId, GridView gv)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select * from Tour_LocalConveyance  where TourId ='" + TourId + "' Order by LocalConvId ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);

            DataTable Travel = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("LocalConvId");
            Travel.Columns.Add(col);
            col = new DataColumn("TourId");
            Travel.Columns.Add(col);
            col = new DataColumn("FromLoc");
            Travel.Columns.Add(col);
            col = new DataColumn("ToLoc");
            Travel.Columns.Add(col);
            col = new DataColumn("ModeOfTravel");
            Travel.Columns.Add(col);
            col = new DataColumn("Kms");
            Travel.Columns.Add(col);
            col = new DataColumn("Amount");
            Travel.Columns.Add(col);
            col = new DataColumn("Remarks");
            Travel.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = Travel.NewRow();
                dr["LocalConvId"] = dbManager.DataReader["LocalConvId"].ToString();
                dr["TourId"] = dbManager.DataReader["TourId"].ToString();

                dr["FromLoc"] = dbManager.DataReader["FromLoc"].ToString();
                dr["ToLoc"] = dbManager.DataReader["ToLoc"].ToString();
                dr["ModeOfTravel"] = dbManager.DataReader["ModeOfTravel"].ToString();
                dr["Kms"] = dbManager.DataReader["Kms"].ToString();
                dr["Amount"] = dbManager.DataReader["Amount"].ToString();
                dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                Travel.Rows.Add(dr);

            }
            dbManager.DataReader.Close();
            gv.DataSource = Travel;
            gv.DataBind();
        }

        public void Tour_LodgingExpanses_Select(string TourId, GridView gv)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select * from Tour_LodgingExpanses  where TourId ='" + TourId + "' Order by LodgingId ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);

            DataTable Travel = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("LodgingId");
            Travel.Columns.Add(col);
            col = new DataColumn("TourId");
            Travel.Columns.Add(col);
            col = new DataColumn("HotelName");
            Travel.Columns.Add(col);
            col = new DataColumn("HotelAddress");
            Travel.Columns.Add(col);
            col = new DataColumn("DayTrafiee");
            Travel.Columns.Add(col);
            col = new DataColumn("EligibleTrafiee");
            Travel.Columns.Add(col);
            col = new DataColumn("NoOfDays");
            Travel.Columns.Add(col);
            col = new DataColumn("Amount");
            Travel.Columns.Add(col);
            col = new DataColumn("Remarks");
            Travel.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = Travel.NewRow();
                dr["LodgingId"] = dbManager.DataReader["LodgingId"].ToString();
                dr["TourId"] = dbManager.DataReader["TourId"].ToString();

                dr["HotelName"] = dbManager.DataReader["HotelName"].ToString();
                dr["HotelAddress"] = dbManager.DataReader["HotelAddress"].ToString();
                dr["DayTrafiee"] = dbManager.DataReader["DayTrafiee"].ToString();
                dr["EligibleTrafiee"] = dbManager.DataReader["EligibleTrafiee"].ToString();
                dr["NoOfDays"] = dbManager.DataReader["NoOfDays"].ToString();
                dr["Amount"] = dbManager.DataReader["Amount"].ToString();
               
                dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                Travel.Rows.Add(dr);

            }
            dbManager.DataReader.Close();
            gv.DataSource = Travel;
            gv.DataBind();
        }

        public void Tour_DailyAllowances_Select(string TourId, GridView gv)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select * from Tour_DailAllowances  where TourId ='" + TourId + "' Order by DailyAllowanceId ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);

            DataTable Travel = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("DailyAllowanceId");
            Travel.Columns.Add(col);
            col = new DataColumn("TourId");
            Travel.Columns.Add(col);
            col = new DataColumn("FromDate");
            Travel.Columns.Add(col);
            col = new DataColumn("ToDate");
            Travel.Columns.Add(col);
            col = new DataColumn("TotalHrs");
            Travel.Columns.Add(col);
            col = new DataColumn("DA");
            Travel.Columns.Add(col);
            col = new DataColumn("Incidental");
            Travel.Columns.Add(col);
            col = new DataColumn("Amount");
            Travel.Columns.Add(col);
            col = new DataColumn("Remarks");
            Travel.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = Travel.NewRow();
                dr["DailyAllowanceId"] = dbManager.DataReader["DailyAllowanceId"].ToString();
                dr["TourId"] = dbManager.DataReader["TourId"].ToString();

                dr["FromDate"] = dbManager.DataReader["FromDate"].ToString();
                dr["ToDate"] = dbManager.DataReader["ToDate"].ToString();
                dr["TotalHrs"] = dbManager.DataReader["TotalHrs"].ToString();
                dr["DA"] = dbManager.DataReader["DA"].ToString();
                dr["Incidental"] = dbManager.DataReader["Incidental"].ToString();
                dr["Amount"] = dbManager.DataReader["Amount"].ToString();

                dr["Remarks"] = dbManager.DataReader["Remarks"].ToString();
                Travel.Rows.Add(dr);

            }
            dbManager.DataReader.Close();
            gv.DataSource = Travel;
            gv.DataBind();
        }
        
        public string TourExapnses_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            this.TourId = AutoGenMaxId("[Tour_Expanses]", "TourId");

            _commandText = string.Format("INSERT INTO [Tour_Expanses] SELECT ISNULL(MAX(TourId),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},{13},{14},{15},{16},'{17}' from [Tour_Expanses] ", this.TourNo, this.EmpId, this.Tour_Date, this.FromLocation, this.PlaceOfVisit, this.NoOfDays, this.DeptDate, this.ArrivalDate, this.TotalTravel, this.TotalLocalConveyance, this.TotalLodging, this.TotalFooding, this.TotalTourExpanses, this.TicketsByComp, this.HotelBillsByComp, this.Advance, this.BalanceAmt, this.PreparedBy);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");
            }
            return _returnStringMessage;
        }
        public string Tour_LocalConveyance_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [Tour_LocalConveyance] SELECT ISNULL(MAX(LocalConvId),0)+1,'{0}','{1}','{2}','{3}','{4}',{5},'{6}' FROM [Tour_LocalConveyance]", this.TourId ,this.FromLoc ,this.ToLoc ,this.ModeOfTravel,this.Kms,this.Amount ,this.Remarks );

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");

            }
            return _returnStringMessage;
        }
        public string Tour_LocalConveyance_Delete(string TourId)
        {
            if (DeleteRecord("[Tour_LocalConveyance]", "TourId", TourId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public string Tour_Lodging_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [Tour_LodgingExpanses] SELECT ISNULL(MAX(LodgingId),0)+1,'{0}','{1}','{2}',{3},{4},{5},{6},'{7}' FROM [Tour_LodgingExpanses]", this.TourId, this.HotelName, this.HotelAddress, this.DayTrafiee, this.EligibleTrafiee, this.NoOfDays,this.Amount, this.Remarks);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");

            }
            return _returnStringMessage;
        }
        public string Tour_Lodging_Delete(string TourId)
        {
            if (DeleteRecord("[Tour_LodgingExpanses]", "TourId", TourId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public string Tour_TravelExpanses_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [Tour_TravelExpanses] SELECT ISNULL(MAX(TravelId),0)+1,'{0}','{1}','{2}','{3}','{4}',{5},'{6}' FROM [Tour_TravelExpanses]", this.TourId, this.FromLoc, this.ToLoc, this.ModeOfTravel, this.Class, this.Amount, this.Remarks);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");

            }
            return _returnStringMessage;
        }
        public string Tour_TravelExapnses_Delete(string TourId)
        {
            if (DeleteRecord("[Tour_TravelExpanses]", "TourId", TourId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public string Tour_DA_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [Tour_DailAllowances] SELECT ISNULL(MAX(DailyAllowanceId),0)+1,'{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}' FROM [Tour_DailAllowances]", this.TourId, this.FromDate , this.ToDate, this.TotalHrs, this.DA, this.Incidental, this.Amount, this.Remarks);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");

            }
            return _returnStringMessage;
        }
        public string Tour_DA_Delete(string TourId)
        {
            if (DeleteRecord("[Tour_DailAllowances]", "TourId", TourId) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public string Tour_Expanses_Delete(string TourId)
        {
            if (DeleteRecord("[Tour_Expanses]", "TourId", TourId) == true)
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
    #endregion

    #region Convenience_Voucher

    public class Convenience_Voucher
    {
        public string id, VoucherNo, Date, ExeName, Cpid, preparedby,On_Date;
        public string DetId, SiteName, Purpose, From, To, FromTime, ToTime, KMs, TotalKms;
        public string Ins_Id,Ins_Num, Ins_Comp_Id, Ins_Comp_Name, Ins_Comp_Add, Open_Cover_No, Importing_Item_Details, From_Supplier, To_CompanyLocation, Mode_Of_Dispatch, via_Location, Currency_Id, Currency_Name, Amount, Multiply_Factor, Value_Of_Consignment, Customs_Duty, Customs_Duty_Amt, Total_Amount, Name_Of_Consignor, Name_Of_Consignee, Invoice_No, Invoice_Dated_On, Bill_Of_Loading_No, Bill_Dated_On, Vessel_Voyage_No, Insurance_Date, Ex1, Ex2, Ex3;
        public Convenience_Voucher()
        {

        }

        public string Insurance_Save()
        {

            if (dbManager.Transaction == null)
                dbManager.Open();
            this.Ins_Id = AutoGenMaxId("[Insurance_Form_tbl]", "Ins_Id");

            _commandText = string.Format("INSERT INTO [Insurance_Form_tbl] values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}') ", this.Ins_Id, this.Ins_Comp_Id, this.Ins_Comp_Name, this.Ins_Comp_Add, this.Open_Cover_No, this.Importing_Item_Details, this.From_Supplier, this.To_CompanyLocation, this.Mode_Of_Dispatch, this.via_Location, this.Currency_Id, this.Currency_Name, this.Amount, this.Multiply_Factor, this.Value_Of_Consignment, this.Customs_Duty, this.Customs_Duty_Amt, this.Total_Amount, this.Name_Of_Consignor, this.Name_Of_Consignee, this.Invoice_No, this.Invoice_Dated_On, this.Bill_Of_Loading_No, this.Bill_Dated_On, this.Vessel_Voyage_No, this.Insurance_Date, this.Ex1, this.Ex2, this.Ex3,this.Ins_Num);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");
            }
            return _returnStringMessage;
        }

        public static string Insurance_AutoGenCode()
        {

            return AutoGenMaxNo("Insurance_Form_tbl", "Ins_Id");
        }

        public string Conv_Voucher_Save()
        {
            this.id = AutoGenMaxId("[Convenience_Voucher_tbl]", "Id");

            dbManager.Open();

            _commandText = string.Format("INSERT INTO [Convenience_Voucher_tbl] SELECT ISNULL(MAX(Id),0)+1,'{0}','{1}','{2}','{3}','{4}' FROM [Convenience_Voucher_tbl]", this.VoucherNo, this.Date, this.ExeName, this.Cpid, this.preparedby);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Employee Document Details", "39");

            }
            
            return _returnStringMessage;
        }
        public string Conv_VoucherDetails_Save()
        {
            
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [Convenience_Voucher_Det_tbl] SELECT ISNULL(MAX(Det_Id),0)+1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}' FROM [Convenience_Voucher_Det_tbl]", this.id, this.SiteName, this.Purpose, this.From, this.To, this.FromTime, this.ToTime, this.KMs, this.TotalKms, this.On_Date);

            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Internal INdent Details", "61");

            }            
            return _returnStringMessage;
        }
        public string Conv_VoucheDetails_Delete(string ID)
        {
            if (DeleteRecord("[Convenience_Voucher_Det_tbl]", "Id", ID) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }
        public string Conv_Vouche_Delete(string ID)
        {
            if (DeleteRecord("[Convenience_Voucher_tbl]", "Id", ID) == true)
            {
                _returnStringMessage = "Data Deleted Successfully";
            }
            else
            {
                _returnStringMessage = "Some Data Missing.";
            }
            return _returnStringMessage;
        }

        public int Conv_Voucher_Select(string id)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("select * From Convenience_Voucher_tbl  where Id='" + id +"'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.id = dbManager.DataReader["Id"].ToString();
                this.VoucherNo = dbManager.DataReader["Voucher_No"].ToString();
                this.Date = Convert.ToDateTime(dbManager.DataReader["Date"].ToString()).ToString("dd/MM/yyyy");
                this.ExeName = dbManager.DataReader["Exe_Name"].ToString();
                this.preparedby = dbManager.DataReader["EmpId"].ToString();

                
                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public void Conv_VoucherDetails_Select(string Id, GridView gv)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();

            _commandText = string.Format("SELECT * FROM Convenience_Voucher_Det_tbl where Id='" + Id + "' order by [Det_Id] desc");
            dbManager.ExecuteReader(CommandType.Text, _commandText);


            DataTable ConVouDet = new DataTable();
            DataColumn col = new DataColumn();
            col = new DataColumn("Site_Name");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("Purpose");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("From_Loc_Id");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("To_Loc_Id");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("From_Time");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("To_Time");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("KMs");
            ConVouDet.Columns.Add(col);
            col = new DataColumn("On_Date");
            ConVouDet.Columns.Add(col);
            while (dbManager.DataReader.Read())
            {
                DataRow dr = ConVouDet.NewRow();

                dr["Site_Name"] = dbManager.DataReader["Site_Name"].ToString();
                dr["Purpose"] = dbManager.DataReader["Purpose"].ToString();
                dr["From_Loc_Id"] = dbManager.DataReader["From_Loc_Id"].ToString();
                dr["To_Loc_Id"] = dbManager.DataReader["To_Loc_Id"].ToString();
                dr["From_Time"] = dbManager.DataReader["From_Time"].ToString();
                dr["To_Time"] = dbManager.DataReader["To_Time"].ToString();
                dr["KMs"] = dbManager.DataReader["KMs"].ToString();
                dr["On_Date"] = Yantra.Classes.General.toDDMMYYYY(dbManager.DataReader["On_Date"].ToString());

                //dr["On_Date"] = dbManager.DataReader["On_Date"].ToString();

                ConVouDet.Rows.Add(dr);
            }
            dbManager.DataReader.Close();
            gv.DataSource = ConVouDet;
            gv.DataBind();
        }

       
        public static string Convenience_Voucher_AutoGenCode()
        {

            return AutoGenMaxNo("Convenience_Voucher_tbl", "Voucher_No");
        }
    }
    #endregion

    public class Vote
    {
        public string VoteId, VoterEmpId, Month, PreparedBy, Reason;
        public Vote() { }

        public string Vote_Save()
        {
            dbManager.Open();

            _commandText = string.Format("INSERT INTO [Vote] SELECT ISNULL(MAX(VoteId),0)+1,'{0}','{1}','{2}','{3}', FROM [Vote]", this.Month , this.VoterEmpId , this.Reason , this.PreparedBy );
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Shift Change Details", "25");

            }

            return _returnStringMessage;
        }
        
    }
    #region Reward
    public class Reward
    {
        public string RewardId, RewardName, RewardDate, RewardReason;
        public string EmpId, Cpid;
        DateTime date;

        public Reward()
        {
        }

        public static int getCircularMemoCount(string userid)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                connection.Close();
                command = new SqlCommand("sp_getCircular_Memo_UnreadCount", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@user_id", SqlDbType.Int).Value = Convert.ToInt32(userid);
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                connection.Close();
            }
            int num = 0;
            if (dataTable.Rows.Count > 0)
            {
                num = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return num;
        }




        public static int getCircularRewardCount(string userid)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                connection.Close();
                command = new SqlCommand("sp_getCircular_Reward_UnreadCount", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@user_id", SqlDbType.Int).Value = Convert.ToInt32(userid);
                connection.Open();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
                connection.Close();
            }
            int num = 0;
            if (dataTable.Rows.Count > 0)
            {
                num = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return num;
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
        }


        public string Reward_Save()
        {
            dbManager.Open();


            this.RewardId = AutoGenMaxId("YANTRA_EMP_Reward", "Reward_Id");

            _commandText = string.Format("INSERT INTO [YANTRA_EMP_Reward] SELECT ISNULL(MAX(Reward_Id),0)+1,'{0}',{1},'{2}','{3}' FROM [YANTRA_EMP_Reward]", RewardName, EmpId, RewardDate, RewardReason);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                //log.add_Insert("Employee Document Details", "39");

            }


            return _returnStringMessage;
        }

        #region Reward Update
        public string Reward_Update()
        {
            dbManager.Open();

            if (true)
            {
                _commandText = string.Format("UPDATE [YANTRA_EMP_Reward] SET  Reward_No='{0}',Emp_Id='{1}',Reward_Date='{2}',Reason='{3}' WHERE Reward_Id={4}", RewardName, EmpId, RewardDate, RewardReason, this.RewardId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";

                    log.add_Update("Employee Document Details", "39");

                }
            }
            else
            {
            }
            return _returnStringMessage;
        }
        #endregion

        #region RewardDELETE
        public string Reward_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_EMP_Reward]", "Reward_Id", this.RewardId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Employee Document Details", "39");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }
        #endregion

        public static string Reward_AutoGenCode()
        {

            return AutoGenMaxNo("YANTRA_EMP_Reward", "Reward_No");
        }
    }
    #endregion

    #region Memo
    public class Memo
    {
        public string MemoId, MemoName, MemoDate, MemoReason;
        public string EmpId, Cpid;
        DateTime date;

        public Memo()
        {
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
        }


        public string Memo_Save()
        {
            dbManager.Open();

            _commandText = string.Format("INSERT INTO [YANTRA_EMP_MEMO] SELECT ISNULL(MAX(Memo_Id),0)+1,'{0}',{1},'{2}','{3}' FROM [YANTRA_EMP_MEMO]", MemoName, EmpId, MemoDate, MemoReason);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Employee Document Details", "39");

            }


            return _returnStringMessage;
        }

        #region Memo Update
        public string Memo_Update()
        {
            dbManager.Open();

            if (true)
            {
                _commandText = string.Format("UPDATE [YANTRA_EMP_MEMO] SET  Memo_No='{0}',Emp_Id='{1}',Memo_Date='{2}',Reason='{3}' WHERE Memo_Id={4}", MemoName, EmpId, MemoDate, MemoReason, this.MemoId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";

                    log.add_Update("Employee Document Details", "39");

                }
            }
            else
            {
            }
            return _returnStringMessage;
        }
        #endregion

        #region MEMODELETE
        public string Memo_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_EMP_MEMO]", "Memo_Id", this.MemoId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Employee Document Details", "39");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }
        #endregion

        public static string Memo_AutoGenCode()
        {

            return AutoGenMaxNo("YANTRA_EMP_MEMO", "Memo_No");
        }
    }
    #endregion

    //Methods For Shift Change Form
    public class ShiftChange
    {
        public string ShiftId, EmpId, DeptId, DesgId, LocationId, PresentShift, RequiredShift, ShiftChangeBetween, ApprovedBy, ShiftDate, Reason;
        public string Status1, Status2, Status3, Comment1, Comment2, Comment3, Rejected_By;

        public ShiftChange()
        { }

        public string ShiftChange_Save()
        {
            dbManager.Open();

            _commandText = string.Format("INSERT INTO [YANTRA_EMP_SHIFT_CHANGE_FORM] SELECT ISNULL(MAX(Shift_Change_ID),0)+1,{0},{1},{2},{3},'{4}','{5}','{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}' FROM [YANTRA_EMP_SHIFT_CHANGE_FORM]", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.PresentShift, this.RequiredShift, this.ShiftChangeBetween, this.ApprovedBy, this.ShiftDate, this.Reason, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Shift Change Details", "25");

            }

            return _returnStringMessage;
        }

        public string ShiftChange_Update()
        {
            dbManager.Open();

            _commandText = string.Format("UPDATE [YANTRA_EMP_SHIFT_CHANGE_FORM] SET Emp_ID={0},Dept_ID={1},Desg_ID = {2},Location_ID = {3},Present_Shift = '{4}',Required_Shift = '{5}',Shift_Change_Between = '{6}',Approved_ID ={7},Shift_Change_Date = '{8}',Reason ='{10}',Status1='{11}',Status2='{12}',Status3='{13}',Comment1='{14}',Comment2='{15}',Comment3='{16}',Rejected_By='{17}' WHERE Shift_Change_ID={9}", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.PresentShift, this.RequiredShift, this.ShiftChangeBetween, this.ApprovedBy, this.ShiftDate, this.ShiftId, this.Reason, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Shift Change Details", "25");

            }

            return _returnStringMessage;
        }

        public string ShiftChange_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_EMP_SHIFT_CHANGE_FORM]", "Shift_Change_ID", this.ShiftId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Shift Change Details", "25");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }

        public int ShiftChange_Select(string ShiftId)
        {

            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_EMP_SHIFT_CHANGE_FORM],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST,YANTRA_LKUP_REGION  WHERE [YANTRA_EMP_SHIFT_CHANGE_FORM].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_EMP_SHIFT_CHANGE_FORM].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_EMP_SHIFT_CHANGE_FORM.Desg_ID = YANTRA_DESG_MAST.DESG_ID and YANTRA_EMP_SHIFT_CHANGE_FORM.Location_ID = YANTRA_LKUP_REGION.REG_ID and YANTRA_EMP_SHIFT_CHANGE_FORM.Shift_Change_ID = " + ShiftId + " ORDER BY Shift_Change_ID");


            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.ShiftId = dbManager.DataReader["Shift_Change_ID"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.DesgId = dbManager.DataReader["Desg_ID"].ToString();
                this.LocationId = dbManager.DataReader["Location_ID"].ToString();
                this.PresentShift = dbManager.DataReader["Present_Shift"].ToString();
                this.RequiredShift = dbManager.DataReader["Required_Shift"].ToString();
                this.ShiftChangeBetween = dbManager.DataReader["Shift_Change_Between"].ToString();
                this.ApprovedBy = dbManager.DataReader["Approved_ID"].ToString();
                this.ShiftDate = Convert.ToDateTime(dbManager.DataReader["Shift_Change_Date"].ToString()).ToString("dd/MM/yyyy");

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string ShiftChangeApprove_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_EMP_SHIFT_CHANGE_FORM] SET Approved_ID = {0}  WHERE Shift_Change_ID ={1}", this.ApprovedBy, this.ShiftId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Approved Successfully";
                log.add_Update("Shift Change Details", "25");

            }

            return _returnStringMessage;
        }


    }

    //Methods For OnDuty Form
    public class OnDuty
    {
        public string Status1, Status2, Status3, Comment1, Comment2, Comment3, Rejected_By;
        public string OndutyId, EmpId, DeptId, DesgId, LocationId, MovingDate, OnDutyDateFrom, OnDutyDateTo, ReturnDate, RefExecutive, NatureofWork, CoffDays, PlaceVisited, ApprovedBy, OndutyDate, FromTime, ToTime;
        public OnDuty()
        { }

        public string OnDuty_Save()
        {
            dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_ONDUTY_FORM] SELECT ISNULL(MAX(OnDuty_ID),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}'  FROM [YANTRA_ONDUTY_FORM]", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.MovingDate, this.OnDutyDateFrom, this.OnDutyDateTo, this.ReturnDate, this.RefExecutive, this.NatureofWork, this.CoffDays, this.PlaceVisited, this.ApprovedBy, this.OndutyDate, this.FromTime, this.ToTime, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";

                log.add_Insert("OnDuty Details", "53");


            }

            return _returnStringMessage;
        }

        public string OnDuty_Update()
        {
            dbManager.Open();

            _commandText = string.Format("UPDATE [YANTRA_ONDUTY_FORM] SET Emp_ID={0},Dept_ID={1},Desg_ID = {2},Location_ID = '{3}',Moving_Date = '{4}',OnDutyDate_From = '{5}',OnDutyDate_To = '{6}',Return_Date ='{7}',Ref_Executive = '{8}',Nature_Of_Work='{9}',C_Off_Days='{10}',Place_Visited ='{11}',Approved_By={12},OnDuty_Date = '{13}',FromTime='{15}',ToTime='{16}',Status1='{17}',Status2='{18}',Status3='{19}',Comment1='{20}',Comment2='{21}',Comment3='{22}',Rejected_By='{23}' WHERE OnDuty_ID={14}", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.MovingDate, this.OnDutyDateFrom, this.OnDutyDateTo, this.ReturnDate, this.RefExecutive, this.NatureofWork, this.CoffDays, this.PlaceVisited, this.ApprovedBy, this.OndutyDate, this.OndutyId, this.FromTime, this.ToTime, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("OnDuty Details", "53");

            }

            return _returnStringMessage;
        }

        public string OnDuty_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_ONDUTY_FORM]", "OnDuty_ID", this.OndutyId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("OnDuty Details", "53");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }

        public int OnDuty_Select(string OnDutyID)
        {

            dbManager.Open();
            //_commandText = string.Format("SELECT * FROM [YANTRA_ONDUTY_FORM],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST,YANTRA_LKUP_REGION  WHERE [YANTRA_ONDUTY_FORM].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_ONDUTY_FORM].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_ONDUTY_FORM.Desg_ID = YANTRA_DESG_MAST.DESG_ID and YANTRA_ONDUTY_FORM.Location_ID = YANTRA_LKUP_REGION.REG_ID and YANTRA_ONDUTY_FORM.OnDuty_ID = " + OnDutyID + " ORDER BY OnDuty_ID");
            _commandText = string.Format("SELECT * FROM [YANTRA_ONDUTY_FORM],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST,YANTRA_LKUP_REGION  WHERE [YANTRA_ONDUTY_FORM].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_ONDUTY_FORM].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_ONDUTY_FORM.Desg_ID = YANTRA_DESG_MAST.DESG_ID  and YANTRA_ONDUTY_FORM.OnDuty_ID = " + OnDutyID + " ORDER BY OnDuty_ID");



            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.OndutyId = dbManager.DataReader["OnDuty_ID"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.DesgId = dbManager.DataReader["Desg_ID"].ToString();
                this.LocationId = dbManager.DataReader["Location_ID"].ToString();
                this.RefExecutive = dbManager.DataReader["Ref_Executive"].ToString();
                this.NatureofWork = dbManager.DataReader["Nature_Of_Work"].ToString();
                this.CoffDays = dbManager.DataReader["C_Off_Days"].ToString();
                this.PlaceVisited = dbManager.DataReader["Place_Visited"].ToString();
                this.ApprovedBy = dbManager.DataReader["Approved_By"].ToString();
                this.FromTime = dbManager.DataReader["FromTime"].ToString();
                this.ToTime = dbManager.DataReader["ToTime"].ToString();
                this.OnDutyDateFrom = Convert.ToDateTime(dbManager.DataReader["OnDutyDate_From"].ToString()).ToString("dd/MM/yyyy");
                this.OnDutyDateTo = Convert.ToDateTime(dbManager.DataReader["OnDutyDate_To"].ToString()).ToString("dd/MM/yyyy");
                this.ReturnDate = Convert.ToDateTime(dbManager.DataReader["Return_Date"].ToString()).ToString("dd/MM/yyyy");
                this.OndutyDate = Convert.ToDateTime(dbManager.DataReader["OnDuty_Date"].ToString()).ToString("dd/MM/yyyy");
                this.MovingDate = Convert.ToDateTime(dbManager.DataReader["Moving_Date"].ToString()).ToString("dd/MM/yyyy");

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string OnDutyApprove_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_ONDUTY_FORM] SET Approved_By = {0}  WHERE OnDuty_ID ={1}", this.ApprovedBy, this.OndutyId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Approved Successfully";
                log.add_Update("OnDuty Details", "53");

            }

            return _returnStringMessage;
        }


    }


    //Methods For OneHour Permission Form
    public class OneHour
    {
        public string Status1, Status2, Status3, Comment1, Comment2, Comment3, Rejected_By;
        public string HourId, EmpId, DeptId, DesgId, LocationId, FromTime, ToTime, ReasonforPermission, ApprovedBy, HOurDate, ReqDate;
        public OneHour()
        { }

        public string OneHour_Save()
        {
            dbManager.Open();

            _commandText = string.Format("INSERT INTO [YANTRA_ONE_HOUR_PERMISSION] SELECT ISNULL(MAX(One_Hour_ID),0)+1,{0},{1},{2},{3},'{4}','{5}','{6}',{7},'{8}' ,'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}' FROM [YANTRA_ONE_HOUR_PERMISSION]", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.FromTime, this.ToTime, this.ReasonforPermission, this.ApprovedBy, this.HOurDate, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By, this.ReqDate);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("One Hour Details", "54");

            }

            return _returnStringMessage;
        }

        public string OneHour_Update()
        {
            dbManager.Open();

            _commandText = string.Format("UPDATE [YANTRA_ONE_HOUR_PERMISSION] SET Emp_ID={0},Dept_ID={1},Desg_ID = {2},Location_ID = {3},Req_From_Time = '{4}',Req_To_Time = '{5}',Reason_For_Permission = '{6}',Approved_By ={7},OneHour_Date = '{8}' ,Status1='{10}',Status2='{11}',Status3='{12}',Comment1='{13}',Comment2='{14}',Comment3='{15}',Rejected_By='{16}',Req_Date='{17}' WHERE One_Hour_ID ={9}", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.FromTime, this.ToTime, this.ReasonforPermission, this.ApprovedBy, this.HOurDate, this.HourId, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By, this.ReqDate);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("One Hour Details", "54");

            }

            return _returnStringMessage;
        }

        public string OneHour_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_ONE_HOUR_PERMISSION]", "One_Hour_ID", this.HourId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("One Hour Details", "54");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }

        public int OneHour_Select(string HourId)
        {

            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_ONE_HOUR_PERMISSION],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST,YANTRA_LKUP_REGION  WHERE [YANTRA_ONE_HOUR_PERMISSION].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_ONE_HOUR_PERMISSION].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_ONE_HOUR_PERMISSION.Desg_ID = YANTRA_DESG_MAST.DESG_ID and YANTRA_ONE_HOUR_PERMISSION.Location_ID = YANTRA_LKUP_REGION.REG_ID and YANTRA_ONE_HOUR_PERMISSION.One_Hour_ID = " + HourId + " ORDER BY One_Hour_ID");


            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.HourId = dbManager.DataReader["One_Hour_ID"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.DesgId = dbManager.DataReader["Desg_ID"].ToString();
                this.LocationId = dbManager.DataReader["Location_ID"].ToString();
                this.FromTime = dbManager.DataReader["Req_From_Time"].ToString();
                this.ToTime = dbManager.DataReader["Req_To_Time"].ToString();
                this.ReasonforPermission = dbManager.DataReader["Reason_For_Permission"].ToString();
                this.ApprovedBy = dbManager.DataReader["Approved_By"].ToString();
                this.HOurDate = Convert.ToDateTime(dbManager.DataReader["OneHour_Date"].ToString()).ToString("MM/dd/yyyy");

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string OneHOurApprove_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_ONE_HOUR_PERMISSION] SET Approved_By = {0}  WHERE One_Hour_ID ={1}", this.ApprovedBy, this.HourId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Approved Successfully";
                log.add_Update("One Hour Details", "54");

            }

            return _returnStringMessage;
        }


    }

    //Methods For OverTime Form
    public class OverTime
    {
        public string Status1, Status2, Status3, Comment1, Comment2, Comment3, Rejected_By;
        public string OvertimeId, EmpId, DeptId, DesgId, LocationId, FromTime, ToTime, CoffDays, Natureofwork, AddPlaceWorked, Remarks, ApprovedBy, OvertimeDate;
        public OverTime()
        { }

        public string OverTime_Save()
        {
            dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_OVER_TIME_FORM] SELECT ISNULL(MAX(Overtime_ID),0)+1,{0},{1},{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}' ,'{12}','{13}','{14}','{15}','{16}','{17}','{18}' FROM [YANTRA_OVER_TIME_FORM]", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.FromTime, this.ToTime, this.CoffDays, this.Natureofwork, this.AddPlaceWorked, this.Remarks, this.ApprovedBy, this.OvertimeDate, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Over Time Details", "55");

            }

            return _returnStringMessage;
        }

        public string OverTime_Update()
        {
            dbManager.Open();

            _commandText = string.Format("UPDATE [YANTRA_OVER_TIME_FORM] SET Emp_ID={0},Dept_ID={1},Desg_ID = {2},Location_ID = {3},Worked_From_Time = '{4}',Worked_Upto_Time = '{5}',C_OFF_Days = '{6}',Nature_Of_Work ='{7}',Addr_Of_Place_Worked = '{8}',Remarks='{9}',Approved_By={10},Overtime_Date ='{11}' ,Status1='{13}',Status2='{14}',Status3='{15}',Comment1='{16}',Comment2='{17}',Comment3='{18}',Rejected_By='{19}' WHERE Overtime_ID={12}", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.FromTime, this.ToTime, this.CoffDays, this.Natureofwork, this.AddPlaceWorked, this.Remarks, this.ApprovedBy, this.OvertimeDate, this.OvertimeId, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Over Time Details", "55");

            }

            return _returnStringMessage;
        }

        public string OverTime_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_OVER_TIME_FORM]", "Overtime_ID", this.OvertimeId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Over Time Details", "55");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }

        public int OverTime_Select(string OvertimeId)
        {

            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_OVER_TIME_FORM],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST,YANTRA_LKUP_REGION  WHERE [YANTRA_OVER_TIME_FORM].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_OVER_TIME_FORM].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_OVER_TIME_FORM.Desg_ID = YANTRA_DESG_MAST.DESG_ID and YANTRA_OVER_TIME_FORM.Location_ID = YANTRA_LKUP_REGION.REG_ID and YANTRA_OVER_TIME_FORM.Overtime_ID = " + OvertimeId + " ORDER BY Overtime_ID");


            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.OvertimeId = dbManager.DataReader["Overtime_ID"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.DesgId = dbManager.DataReader["Desg_ID"].ToString();
                this.LocationId = dbManager.DataReader["Location_ID"].ToString();
                this.CoffDays = dbManager.DataReader["C_OFF_Days"].ToString();
                this.Natureofwork = dbManager.DataReader["Nature_Of_Work"].ToString();
                this.AddPlaceWorked = dbManager.DataReader["Addr_Of_Place_Worked"].ToString();
                this.Remarks = dbManager.DataReader["Remarks"].ToString();
                this.ApprovedBy = dbManager.DataReader["Approved_By"].ToString();

                this.FromTime = dbManager.DataReader["Worked_From_Time"].ToString();
                this.ToTime = dbManager.DataReader["Worked_Upto_Time"].ToString();
                this.OvertimeDate = Convert.ToDateTime(dbManager.DataReader["Overtime_Date"].ToString()).ToString("MM/dd/yyyy");

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string OverTimeApprove_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_OVER_TIME_FORM] SET Approved_By = {0}  WHERE Overtime_ID ={1}", this.ApprovedBy, this.OvertimeId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Approved Successfully";
                log.add_Update("Over Time Details", "55");

            }

            return _returnStringMessage;
        }

    }

    //Methods For Ticket Details Form

    public class TicketDetails
    {
        public string Status1, Status2, Status3, Comment1, Comment2, Comment3, Rejected_By;
        public string TicketId, EmpId, DeptId, DesgId, LocationId, MovingDate, ModeofTravel, Destination, Eligibility, Idproof, IdProofNo, ApprovedBy, TicketDate, Age, Mobile;
        public TicketDetails()
        { }

        public string TicketDetails_Save()
        {
            dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_TICKET_DETAILS] SELECT ISNULL(MAX(TicketDetails_Id),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},{12},'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}' FROM [YANTRA_TICKET_DETAILS]", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.MovingDate, this.ModeofTravel, this.Destination, this.Eligibility, this.IdProofNo, this.ApprovedBy, this.TicketDate, this.Idproof, this.Age, this.Mobile, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Ticket Details", "56");

            }

            return _returnStringMessage;
        }

        public string TicketDetails_Update()
        {
            dbManager.Open();

            _commandText = string.Format("UPDATE [YANTRA_TICKET_DETAILS] SET Emp_ID={0},Dept_ID={1},Desg_ID = {2},Location_ID = '{3}',Moving_Date = '{4}',Mode_Travel = '{5}',Destination = '{6}',Eligibility ='{7}',IdProof_Number = '{8}',Approved_By={9},TicketDetails_Date ='{10}', IdProof={11}, Age={12}, Mobile_Number='{13}' ,Status1='{15}',Status2='{16}',Status3='{17}',Comment1='{18}',Comment2='{19}',Comment3='{20}',Rejected_By='{21}' WHERE TicketDetails_Id={14}", this.EmpId, this.DeptId, this.DesgId, this.LocationId, this.MovingDate, this.ModeofTravel, this.Destination, this.Eligibility, this.IdProofNo, this.ApprovedBy, this.TicketDate, this.Idproof, this.Age, this.Mobile, this.TicketId, this.Status1, this.Status2, this.Status3, this.Comment1, this.Comment2, this.Comment3, this.Rejected_By);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Ticket Details", "56");

            }

            return _returnStringMessage;
        }

        public string TicketDetails_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_TICKET_DETAILS]", "TicketDetails_Id", this.TicketId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Ticket Details", "56");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }

        public int TicketDetails_Select(string TicketId)
        {

            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_TICKET_DETAILS],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST WHERE [YANTRA_TICKET_DETAILS].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_TICKET_DETAILS].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_TICKET_DETAILS.Desg_ID = YANTRA_DESG_MAST.DESG_ID and YANTRA_TICKET_DETAILS.TicketDetails_Id = " + TicketId + " ORDER BY TicketDetails_Id");


            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.TicketId = dbManager.DataReader["TicketDetails_Id"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.DesgId = dbManager.DataReader["Desg_ID"].ToString();
                this.LocationId = dbManager.DataReader["Location_ID"].ToString();
                this.ModeofTravel = dbManager.DataReader["Mode_Travel"].ToString();
                this.Destination = dbManager.DataReader["Destination"].ToString();
                this.Eligibility = dbManager.DataReader["Eligibility"].ToString();
                this.IdProofNo = dbManager.DataReader["IdProof_Number"].ToString();
                this.ApprovedBy = dbManager.DataReader["Approved_By"].ToString();
                this.Comment1 = dbManager.DataReader["Comment1"].ToString();
                this.MovingDate = Convert.ToDateTime(dbManager.DataReader["Moving_Date"].ToString()).ToString("dd/MM/yyyy");
                this.TicketDate = Convert.ToDateTime(dbManager.DataReader["TicketDetails_Date"].ToString()).ToString("dd/MM/yyyy");
                this.Idproof = dbManager.DataReader["IdProof"].ToString();
                this.Age = dbManager.DataReader["Age"].ToString();
                this.Mobile = dbManager.DataReader["Mobile_Number"].ToString();


                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string TicketDetailsApprove_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_TICKET_DETAILS] SET Approved_By = {0}  WHERE TicketDetails_Id ={1}", this.ApprovedBy, this.TicketId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Approved Successfully";
                log.add_Update("Ticket Details", "56");

            }

            return _returnStringMessage;
        }
    }



    //Methods For Emp Leave Form
    public class EmpLeave
    {
        public string LeaveId, EmpId, DeptId, DesgId, DayofLeave, FromDate, ToDate, ReasonforLeave, HandedName, HandedDept, AddressofLeavePeriod, LeaveDate, Approvedby, EmpName,LeaveType;
        public string Status1,Status2,Status3,MobileNo;
        public EmpLeave()
        { }
        public static void LeaveType_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format(" select distinct TypeOfLeave from EMP_Leave_tbl  ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "TypeOfLeave", "TypeOfLeave");
            }
        }
        public static void LeaveStatus_Select(Control ControlForBind)
        {
            dbManager.Open();
            _commandText = string.Format(" select distinct Status3 from EMP_Leave_tbl   ");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (ControlForBind is DropDownList)
            {
                DropDownListBind(ControlForBind as DropDownList, "Status3", "Status3");
            }
        }
        public string EmpLeave_Save()
        {
            dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_LEAVE_FORM] SELECT ISNULL(MAX(Leave_Id),0)+1,{0},{1},{2},'{3}','{4}','{5}','{6}',{7},{8},'{9}','{10}','{11}','{12}' FROM [YANTRA_LEAVE_FORM]", this.EmpId, this.DeptId, this.DesgId, this.DayofLeave, this.FromDate, this.ToDate, this.ReasonforLeave, this.HandedName, this.HandedDept, this.AddressofLeavePeriod, this.LeaveDate, this.Approvedby, this.LeaveType);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Saved Successfully";
                log.add_Insert("Employee Leave Details", "52");

            }

            return _returnStringMessage;
        }

        public string EmpLeave_Update()
        {
            dbManager.Open();

            _commandText = string.Format("UPDATE [YANTRA_LEAVE_FORM] SET Emp_ID={0},Dept_ID={1},Desg_ID = {2},DayofLeave = '{3}',FromDate = '{4}',ToDate = '{5}',ReasonforLeave = '{6}',HandedName ={7},HandedDept = {8},AddressLeavePeriod='{9}',Leave_Date ='{10}',Approved_Id='{11}',LeaveType = '{12}' WHERE Leave_Id={13}", this.EmpId, this.DeptId, this.DesgId, this.DayofLeave, this.FromDate, this.ToDate, this.ReasonforLeave, this.HandedName, this.HandedDept, this.AddressofLeavePeriod, this.LeaveDate, this.Approvedby, this.LeaveType, this.LeaveId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;
            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Updated Successfully";
                log.add_Update("Employee Leave Details", "52");

            }

            return _returnStringMessage;
        }

        public string Leave_Delete()
        {
            HR.BeginTransaction();
            if (DeleteRecord("[YANTRA_LEAVE_FORM]", "Leave_Id", this.LeaveId) == true)
            {
                HR.CommitTransaction();
                _returnStringMessage = "Data Deleted Successfully";
                log.add_Delete("Employee Leave Details", "52");

            }
            else
            {
                HR.RollBackTransaction();
                _returnStringMessage = "This Record cannot be deleted. It has been used as reference in other forms.........";

            }
            return _returnStringMessage;
        }

        public int EmpLeave_Select(string LeaveId)
        {

            dbManager.Open();
            _commandText = string.Format("select * from EMP_Leave_tbl where Leave_Id = '" + LeaveId + "' ORDER BY Leave_Id");


            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.LeaveId = dbManager.DataReader["Leave_Id"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.LeaveType = dbManager.DataReader["TypeOfLeave"].ToString();
                this.EmpName = dbManager.DataReader["Emp_Name"].ToString();
                this.FromDate = Convert.ToDateTime(dbManager.DataReader["FromDate"].ToString()).ToString("dd/MM/yyyy");
                this.ToDate = Convert.ToDateTime(dbManager.DataReader["ToDate"].ToString()).ToString("dd/MM/yyyy");
                this.LeaveDate = Convert.ToDateTime(dbManager.DataReader["DateOfApplying"].ToString()).ToString("dd/MM/yyyy");
                this.DesgId = dbManager.DataReader["Emp_Designation"].ToString();
                this.DayofLeave  = dbManager.DataReader["AppliedNoOfLeaves"].ToString();
                this.ReasonforLeave = dbManager.DataReader["Reason"].ToString();
                this.AddressofLeavePeriod = dbManager.DataReader["AddressInLeavePeriod"].ToString();
                this.Status1 = dbManager.DataReader["Status1"].ToString();
                this.Status2 = dbManager.DataReader["Status2"].ToString();
                this.Status3 = dbManager.DataReader["Status3"].ToString();
                this.MobileNo = dbManager.DataReader["Comment2"].ToString();
                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }
        public int LeaveDetails_Select(string LeaveId)
        {

            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_LEAVE_FORM],[YANTRA_EMPLOYEE_MAST],YANTRA_DEPT_MAST,YANTRA_DESG_MAST  WHERE [YANTRA_LEAVE_FORM].Emp_ID =[YANTRA_EMPLOYEE_MAST].EMP_ID AND [YANTRA_LEAVE_FORM].Dept_ID = YANTRA_DEPT_MAST.DEPT_ID and YANTRA_LEAVE_FORM.Desg_ID = YANTRA_DESG_MAST.DESG_ID and YANTRA_LEAVE_FORM.Leave_Id = " + LeaveId + " ORDER BY Leave_Id");


            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                this.LeaveId = dbManager.DataReader["Leave_Id"].ToString();
                this.EmpId = dbManager.DataReader["Emp_ID"].ToString();
                this.DeptId = dbManager.DataReader["Dept_ID"].ToString();
                this.DesgId = dbManager.DataReader["Desg_ID"].ToString();
                this.DayofLeave = dbManager.DataReader["DayofLeave"].ToString();
                this.ReasonforLeave = dbManager.DataReader["ReasonforLeave"].ToString();
                this.HandedName = dbManager.DataReader["HandedName"].ToString();
                this.HandedDept = dbManager.DataReader["HandedDept"].ToString();
                this.AddressofLeavePeriod = dbManager.DataReader["AddressLeavePeriod"].ToString();
                this.Approvedby = dbManager.DataReader["Approved_Id"].ToString();
                this.LeaveType = dbManager.DataReader["LeaveType"].ToString();

                this.FromDate = Convert.ToDateTime(dbManager.DataReader["FromDate"].ToString()).ToString("dd/MM/yyyy");
                this.ToDate = Convert.ToDateTime(dbManager.DataReader["ToDate"].ToString()).ToString("dd/MM/yyyy");
                this.LeaveDate = Convert.ToDateTime(dbManager.DataReader["Leave_Date"].ToString()).ToString("dd/MM/yyyy");

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            return _returnIntValue;
        }

        public string LeaveDetailsApprove_Update()
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("UPDATE [YANTRA_LEAVE_FORM] SET Approved_Id = {0}  WHERE Leave_Id ={1}", this.Approvedby, this.LeaveId);
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            _returnStringMessage = string.Empty;

            if (_returnIntValue < 0 || _returnIntValue == 0)
            {
                _returnStringMessage = "Some Data Missing.";
            }
            else if (_returnIntValue > 0)
            {
                _returnStringMessage = "Data Approved Successfully";
                log.add_Update("Employee Leave Details", "52");

            }

            return _returnStringMessage;
        }

        //class for Enrollment Form

        public class Enrollment
        {
            public string EnrollmentId, EnrollmentDate, FName, MName, LName, MobileNo, EmailId, Education, Address, Resume, DateOfBirth, Age, Status;
            public string GrossSalary,CompanyName,Department,Designation,Doj,CompanyId,DeptId,DesignId;
            public int EnrollmentMaster_Select(string EnrollmentId)
            {
                dbManager.Open();
                _commandText = string.Format("select a.FirstName,a.MiddleName,a.LastName,a.MobileNo,a.EmailId,a.Address,convert(varchar(10),a.DateOfBirth,103) as DateOfBirth ,b.GrossSalary,c.CompanyName," +
      "c.Department,c.Designation,c.CompanyId,c.DeptId,c.DesignId,convert(varchar(10),c.DOJ,103) as DOJ from dbo.Emp_Enrollment a inner join dbo.Emp_InterviewDetails b " +
     " on a.EnrollmentId=b.EnrollmentId inner join dbo.Emp_OfferLetterDetails c " +
      " on a.EnrollmentId=c.EnrollmentId where c.EnrollmentId=" + EnrollmentId + "");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.FName = dbManager.DataReader["FirstName"].ToString();
                    this.MName = dbManager.DataReader["MiddleName"].ToString();
                    this.LName = dbManager.DataReader["LastName"].ToString();
                    this.MobileNo = dbManager.DataReader["MobileNo"].ToString();
                    this.EmailId = dbManager.DataReader["EmailId"].ToString();
                    this.Address = dbManager.DataReader["Address"].ToString();
                    this.DateOfBirth = dbManager.DataReader["DateOfBirth"].ToString();
                    this.GrossSalary = dbManager.DataReader["GrossSalary"].ToString();
                    this.CompanyName = dbManager.DataReader["CompanyName"].ToString();
                    this.Department = dbManager.DataReader["Department"].ToString();
                    this.Designation = dbManager.DataReader["Designation"].ToString();
                    this.Doj = dbManager.DataReader["DOJ"].ToString();
                    this.CompanyId = dbManager.DataReader["CompanyId"].ToString();
                    this.DeptId = dbManager.DataReader["DeptId"].ToString();
                    this.DesignId = dbManager.DataReader["DesignId"].ToString();

                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }

            public Enrollment()
            { }

            public string Enrollment_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [Emp_Enrollment] SELECT ISNULL(MAX(EnrollmentId),0)+1, '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' FROM [Emp_Enrollment]", this.EnrollmentDate, this.FName, this.MName, this.LName, this.MobileNo, this.EmailId, this.Education, this.Address, this.Resume, this.DateOfBirth, this.Age, this.Status);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("Enrollment Details", "57");

                }

                return _returnStringMessage;
            }

            public string EnrollmentApprove_Update()
            {

                dbManager.Open();
                _commandText = string.Format("UPDATE [Emp_Enrollment] SET Status = '{0}'  WHERE EnrollmentId ={1}", this.Status, this.EnrollmentId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {

    
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Enrollment Details", "57");

                }

                return _returnStringMessage;
            }
        }
        #region DDL Bind with Select
        public static void DDLBindWithSelect1(DropDownList ddlname, string command)
        {
            dbManager.Open();
            dbManager.ExecuteReader(CommandType.Text, command);
            ddlname.Items.Clear();
            ddlname.Items.Add(new ListItem("---", "0"));
            while (dbManager.DataReader.Read())
            {
                ddlname.Items.Add(new ListItem(dbManager.DataReader[1].ToString(), dbManager.DataReader[0].ToString()));
            }
            dbManager.DataReader.Close();
        }
        #endregion

        //class For Interview Schedule
        public class InterviewSchedule
        {
            //public string InterviewId, EnrollmentId, InterviewerName, DateOfInterview, TimeOfInterview, Location, InterviewStatus, GrossSalary, OfferStatus;
            public string InterviewId, EnrollmentId, InterviewerName, DateOfInterview, TimeOfInterview, Location, GrossSalary, InterviewStatus1, OfferStatus, Remarks, InterviewType, InterviewStatus;
            public InterviewSchedule()
            { }

            public string InterviewSchedule_Save()
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO [Emp_InterviewDetails] SELECT  ISNULL(MAX(InterviewId),0)+1,  {0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}' FROM [Emp_InterviewDetails]", this.EnrollmentId, this.InterviewerName, this.DateOfInterview, this.TimeOfInterview, this.Location, this.InterviewStatus, this.GrossSalary, this.OfferStatus, this.InterviewType, this.Remarks, this.InterviewStatus1);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Mail  Successfully Sent";
                    log.add_Insert("Interview Schedule Details", "58");

                }

                return _returnStringMessage;
            }
            public string InterviewSchedule_Update()
            {
                dbManager.Open();
                _commandText = string.Format("UPDATE [Emp_InterviewDetails] SET InterviewStatus = '{0}',InterviewType={1},Remarks='{2}',InterviewStatus1 = '{3}' WHERE EnrollmentId ={4}", this.InterviewStatus, this.InterviewType, this.Remarks, this.InterviewStatus1, this.EnrollmentId);

                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Mail  Successfully Sent";
                    log.add_Insert("Interview Schedule Details", "58");

                }

                return _returnStringMessage;
            }

            public string InterviewApprove_Update()
            {

                dbManager.Open();
                _commandText = string.Format("UPDATE [Emp_InterviewDetails] SET InterviewStatus = '{0}',GrossSalary='{1}',OfferStatus='{2}' WHERE EnrollmentId ={3}", this.InterviewStatus, this.GrossSalary, this.OfferStatus, this.EnrollmentId);
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Updated Successfully";
                    log.add_Update("Interview Details", "58");

                }

                return _returnStringMessage;
            }
        }
    }



   
}















    
