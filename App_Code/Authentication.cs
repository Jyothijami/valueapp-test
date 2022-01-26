using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using YantraDAL;
using vllib;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net;

namespace Yantra
{

    public class Authentication
    {
        static SqlConnection con = dbc.con;
        static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());

        private static int _returnIntValue;
        private static string _returnStringMessage, _commandText, EmpId, EmpName, EmpEmail, EmpUserName, DeptId, Desgid, UserId, userTypeId;
        public static string[] EmpDetails;
        public static string CmpId, CmpName, EmpType;
        public static string MACAdd,IPAddress,IMEINo;
        public enum Logged_EMP_Details
        { EmpId = 0, EmpName = 1, EmpEmail = 2, EmpUserName = 3, CmpId = 4, CmpName = 5, EmpType = 6, DeptId = 7, Desgid = 8, UserId = 9, userTypeId = 10 }

        private static object YantraSession
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
                {
                    HttpContext htc = System.Web.HttpContext.Current;

                    HttpCookie cookie = default(HttpCookie);
                    if (htc.Request.Cookies["udetails"] == null)
                    {
                        cookie = new HttpCookie("udetails");

                        return null;
                    }
                    else
                    {
                        cookie = htc.Request.Cookies["udetails"];

                        string[] u = new string[10];

                        u[0] = cookie.Values["vl_empid"];
                        u[1] = cookie.Values["vl_userfullname"];
                        u[2] = cookie.Values["vl_useremail"];
                        u[3] = cookie.Values["vl_username"];
                        u[4] = cookie.Values["vl_cmpid"];
                        u[5] = cookie.Values["vl_cmpname"];
                        u[6] = cookie.Values["vl_usertype"];
                        u[7] = cookie.Values["vl_deptid"];
                        u[8] = cookie.Values["vl_desgid"];
                        u[9] = cookie.Values["vl_userid"];
                        u[10] = cookie.Values["vl_userstype"];

                        htc.Session["vl_empid"] = u[0];
                        htc.Session["vl_userfullname"] = u[1];
                        htc.Session["vl_useremail"] = u[2];
                        htc.Session["vl_username"] = u[3];
                        htc.Session["vl_cmpid"] = u[4];
                        htc.Session["vl_cmpname"] = u[5];
                        htc.Session["vl_usertype"] = u[6];
                        htc.Session["vl_deptid"] = u[7];
                        htc.Session["vl_desgid"] = u[8];
                        htc.Session["vl_userid"] = u[9];
                        htc.Session["vl_userstype"] = u[10];

                        htc.Response.AppendCookie(cookie); 

                        System.Web.HttpContext.Current.Session["YantraSession"] = (object)u;
                        
                        return System.Web.HttpContext.Current.Session["YantraSession"];
                    }

                    

                    //return null;
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["YantraSession"];
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["YantraSession"] = value;

                string[] u = (string[])value;
                HttpContext htc = System.Web.HttpContext.Current;

                HttpCookie cookie = default(HttpCookie);

                cookie = new HttpCookie("udetails");

                if (htc.Request.Cookies["udetails"] == null)
                {
                    cookie = new HttpCookie("udetails");                    
                }
                else
                {
                    cookie = htc.Request.Cookies["udetails"];

                    //htc.Session["vl_userid"] = cookie.Values["vl_userid"];
                    //htc.Session["vl_userfullname"] = cookie.Values["vl_userfullname"];
                    //htc.Session["vl_useremail"] = cookie.Values["vl_useremail"];
                    //htc.Session["vl_username"] = cookie.Values["vl_username"];
                    //htc.Session["vl_cmpid"] = cookie.Values["vl_cmpid"];
                    //htc.Session["vl_cmpname"] = cookie.Values["vl_cmpname"];
                    //htc.Session["vl_usertype"] = cookie.Values["vl_usertype"];
                    //htc.Session["vl_deptid"] = cookie.Values["vl_deptid"];
                    //htc.Session["vl_desgid"] = cookie.Values["vl_desgid"];

                    //htc.Response.AppendCookie(cookie); 
                }

                htc.Session["vl_empid"] = u[0];
                htc.Session["vl_userfullname"] = u[1];
                htc.Session["vl_useremail"] = u[2];
                htc.Session["vl_username"] = u[3];
                htc.Session["vl_cmpid"] = u[4];
                htc.Session["vl_cmpname"] = u[5];
                htc.Session["vl_usertype"] = u[6];
                htc.Session["vl_deptid"] = u[7];
                htc.Session["vl_desgid"] = u[8];
                htc.Session["vl_userid"] = u[9];
                htc.Session["vl_userstype"] = u[10];

                try
                {
                    cookie.Values.Remove("vl_empid");
                    cookie.Values.Remove("vl_userfullname");
                    cookie.Values.Remove("vl_useremail");
                    cookie.Values.Remove("vl_username");
                    cookie.Values.Remove("vl_cmpid");
                    cookie.Values.Remove("vl_cmpname");
                    cookie.Values.Remove("vl_usertype");
                    cookie.Values.Remove("vl_deptid");
                    cookie.Values.Remove("vl_desgid");
                    cookie.Values.Remove("vl_userid");
                    cookie.Values.Remove("vl_links");
                    cookie.Values.Remove("vl_userstype");
                }
                catch (Exception) { }

                cookie.Values.Add("vl_empid", u[0]);
                cookie.Values.Add("vl_userfullname", u[1]);
                cookie.Values.Add("vl_useremail", u[2]);
                cookie.Values.Add("vl_username", u[3]);
                cookie.Values.Add("vl_cmpid", u[4]);
                cookie.Values.Add("vl_cmpname", u[5]);
                cookie.Values.Add("vl_usertype", u[6]);
                cookie.Values.Add("vl_deptid", u[7]);
                cookie.Values.Add("vl_desgid", u[8]);
                cookie.Values.Add("vl_userid", u[9]);
                cookie.Values.Add("vl_links", "");
                cookie.Values.Add("vl_userstype", u[10]);

                htc.Response.AppendCookie(cookie);

                //htc.Response.AppendCookie(cookie);                           
                


                //{ EmpId, EmpName, EmpEmail, EmpUserName, CmpId, CmpName, EmpType, DeptId, Desgid };
                //HttpCookie aCookie = new HttpCookie("yantraVookie");
                //aCookie.Value = (string)value;
                //aCookie.Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies.Add(aCookie);
            }
        }

        public static void UpdateYantraSessionToCompany(string cp_id, string cp_name)
        {
            string[] e = (string[])YantraSession;
            e[4] = cp_id;
            e[5] = cp_name;

            YantraSession = e;
        }

        public Authentication()
        { }

        public static void Dispose()
        {
            dbManager.Dispose();
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
                (Parent as TextBox).Text = string.Empty;
            else if (Parent is DropDownList)
                (Parent as DropDownList).ClearSelection();
            else if (Parent is ListBox)
                (Parent as ListBox).ClearSelection();
            else
                foreach (Control c in Parent.Controls)
                    ClearControls(c);
        }

        public static void BeginTransaction()
        {
            dbManager.Open();
            dbManager.BeginTransaction();
        }

        public static void CommitTransaction()
        {
            dbManager.CommitTransaction();
        }

        public static void RollBackTransaction()
        {
            dbManager.RollBackTransaction();
        }

        //Method for Auto Generate Max Serial ID
        public static string AutoGenMaxId(string TableName, string FieldName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, "SELECT ISNULL(MAX(" + FieldName + "),0)+1 FROM " + TableName + "").ToString());
            return _returnIntValue.ToString();
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
        private string GetMAC()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }











        public static string GetVisitorIPAddress()
        {
            bool GetLan = false;
            string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);

                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    //Get Ip Address of InterNetwork From The Ip Host Entry Address List
                    foreach (IPAddress ip in ipHostEntries.AddressList)
                    {
                        if (ip.AddressFamily.ToString() == "InterNetwork")
                        {
                            visitorIPAddress = ip.ToString();
                        }
                    }
                    //visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }


            return visitorIPAddress;
        }






        public static string HostName;
        public string LoginCheck(Page page, string UserName, string Password)
        {
            try
            {
                if (this.UserName_Check(UserName) > 0 && this.UserNameFromEmployeeMaster_Check(UserName) > 0)
                {
                    if (this.UserNamePwd_Check(UserName, Password) == 0)
                    {
                        if (this.LoginExpiry_Check(UserName, Password) == 0)
                        {
                            MACAdd = GetMAC();
                              
                            
                                IPAddress = GetVisitorIPAddress();
                                //IMEINo = "355049101792131";
                                HostName = Dns.GetHostName();
                                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                //using (var httpClient = new HttpClient())
                                //{
                                //    var ip_task = httpClient.GetStringAsync("https://api.ipify.org");
                                //    ip_task.Wait();
                                //    IPAddress = ip_task.Result;
                                //}
                                

                                if (this.IpAddress_Check(IPAddress) > 0)
                                {
                                   //if (this.LoginExpiry_Check(UserName, Password) == 1)
                                    if (true)
                                    {
                                        string[] e = Get_EmployeeDetails(UserName);
                                        YantraSession = e;
                                        //LogDetailsInsert(UserName);
                                        
                                        log.add_Insert("Loged With - Mac : " + MACAdd +" , IP Address : "+ IPAddress , "1");

                                        string dboard = uPriv.getUserDashboard(e[10]);
                                        page.Response.Redirect("~/dboards/" + dboard, false);


                                        //page.Response.Redirect(HttpContext.Current.Request.QueryString["rurl"]);

                                        //page.Response.Redirect("~/Modules/Home/Default.aspx",false);

                                    }
                                }
                                //else if (this.IpAddress_Check(MACAdd) > 0)
                                //{
                                //    if (true)
                                //    {
                                //        string[] e = Get_EmployeeDetails(UserName);
                                //        YantraSession = e;
                                //        //LogDetailsInsert(UserName);

                                //        log.add_Insert("Loged With - Mac : " + MACAdd + " , Tab IMEI : " + IMEINo, "1");

                                //        string dboard = uPriv.getUserDashboard(e[10]);
                                //        page.Response.Redirect("~/dboards/" + dboard, false);


                                //        //page.Response.Redirect(HttpContext.Current.Request.QueryString["rurl"]);

                                //        //page.Response.Redirect("~/Modules/Home/Default.aspx",false);

                                //    }
                                //}
                                else
                                {
                                   // _returnStringMessage = "Ip Address Was Not Registered";
                                    page.Response.Redirect("IPCheck.aspx?UserName=" +  UserName.ToLower());
                                }
                        }
                        else
                        {
                            _returnStringMessage = "Login Details Are Expiried";
                        }
                    }
                    else
                    {
                        _returnStringMessage = "Invalid Password";
                    }
                }
                else
                {
                    _returnStringMessage = "User Name Is Not Registered";
                }
            }
            catch (Exception ex)
            {
                _returnStringMessage = ex.Message.ToString();
            }
            finally
            {
            }
            return _returnStringMessage;
        }

        public string MobileLoginCheck(Page page, string UserName, string Password)
        {
            try
            {
                if (this.UserName_Check(UserName) > 0 && this.UserNameFromEmployeeMaster_Check(UserName) > 0)
                {
                    if (this.UserNamePwd_Check(UserName, Password) == 0)
                    {
                        if (this.LoginExpiry_Check(UserName, Password) == 0)
                        {
                            if (true)
                            {
                                string[] e = Get_EmployeeDetails(UserName);
                                YantraSession = e;
                                //LogDetailsInsert(UserName);

                                log.add_Insert("Loged With - UserName : " + UserName , "1");

                                //string dboard = "DailyReport.aspx";
                                //page.Response.Redirect("~/dev_pages/" + dboard, false);


                                //page.Response.Redirect(HttpContext.Current.Request.QueryString["rurl"]);

                               // string ReturnUrl = Convert.ToString(Request.QueryString["url"]);

                                //if (!string.IsNullOrEmpty(ReturnUrl))
                                //{
                                //    page.Response.Redirect(ReturnUrl); 
                                   
                                //}
                                //else
                                //{
                                    page.Response.Redirect("~/dev_pages/Home.aspx", false);
                               // }

                            }
                        }
                        else
                        {
                            _returnStringMessage = "Login Details Are Expiried";
                        }
                    }
                    else
                    {
                        _returnStringMessage = "Invalid Password";
                    }
                }
                else
                {
                    _returnStringMessage = "User Name Is Not Registered";
                }
            }
            catch (Exception ex) { _returnStringMessage = ex.Message.ToString(); }
            return _returnStringMessage;
        }


      
        public string MobileLoginCheck2(Page page, string UserName, string Password,string url)
        {
            try
            {
                if (this.UserName_Check(UserName) > 0 && this.UserNameFromEmployeeMaster_Check(UserName) > 0)
                {
                    if (this.UserNamePwd_Check(UserName, Password) == 0)
                    {
                        if (this.LoginExpiry_Check(UserName, Password) == 0)
                        {
                            if (true)
                            {
                                string[] e = Get_EmployeeDetails(UserName);
                                YantraSession = e;
                                //LogDetailsInsert(UserName);

                                log.add_Insert("Loged With - UserName : " + UserName, "1");
                                page.Response.Redirect(url); 
                                //string dboard = "DailyReport.aspx";
                                //page.Response.Redirect("~/dev_pages/" + dboard, false);


                                //page.Response.Redirect(HttpContext.Current.Request.QueryString["rurl"]);

                                // string ReturnUrl = Convert.ToString(Request.QueryString["url"]);

                                //if (!string.IsNullOrEmpty(ReturnUrl))
                                //{
                                //    page.Response.Redirect(ReturnUrl); 

                                //}
                                //else
                                //{
                               // page.Response.Redirect("~/dev_pages/MobileHome.aspx", false);
                                // }

                            }
                        }
                        else
                        {
                            _returnStringMessage = "Login Details Are Expiried";
                        }
                    }
                    else
                    {
                        _returnStringMessage = "Invalid Password";
                    }
                }
                else
                {
                    _returnStringMessage = "User Name Is Not Registered";
                }
            }
            catch (Exception ex) { _returnStringMessage = ex.Message.ToString(); }
            return _returnStringMessage;
        }


        public void FPSCheck(Page page, string UserName)
        {
            try
            {
                string[] e = Get_EmployeeDetails(UserName);
                YantraSession = e;
                //LogDetailsInsert(UserName);

                log.add_Insert("Loged With - Mac : " + MACAdd + " , IP Address : " + IPAddress, "1");

                //string dboard = uPriv.getUserDashboard(e[10]);
                page.Response.Redirect("~/FPS.aspx");
                //return _returnStringMessage;
            }
            catch (Exception ex)
            {
                _returnStringMessage = ex.Message.ToString();
            }
        }

        public void IPCheck(Page page, string UserName)
        {
            try
            {
                string[] e = Get_EmployeeDetails(UserName);
                YantraSession = e;
                //LogDetailsInsert(UserName);

                log.add_Insert("Loged With - Mac : " + MACAdd + " , IP Address : " + IPAddress, "1");

                string dboard = uPriv.getUserDashboard(e[10]);
                page.Response.Redirect("~/dboards/" + dboard, false);
                //return _returnStringMessage;
            }
            catch (Exception ex) 
            {
                _returnStringMessage = ex.Message.ToString();
            }
        }
        public string OLoginCheck(string UserName, string Password)
        {
            try
            {
                if (this.UserName_Check(UserName) > 0 && this.UserNameFromEmployeeMaster_Check(UserName) > 0)
                {
                    if (this.UserNamePwd_Check(UserName, Password) == 0)
                    {
                        //if (this.LoginExpiry_Check(UserName, Password) > 0)
                        //{
                            _returnStringMessage = "0";
                       // }
                    }
                    else
                    {
                        _returnStringMessage = "2"; // "Invalid Password";
                    }
                }
                else
                {
                    _returnStringMessage = "3"; // "User Name Is Not Registered";
                }
            }
            catch (Exception ex)
            {
                _returnStringMessage = ex.Message.ToString();
            }
            finally
            {
            }
            return _returnStringMessage;
        }

        public string LoginCheck1(Page page, string UserName, string Password)
        {
            try
            {
                if (this.UserName_Check(UserName) > 0 && this.UserNameFromEmployeeMaster_Check(UserName) > 0)
                {
                    if (this.UserNamePwd_Check(UserName, Password) == 0)
                    {
                        if (this.LoginExpiry_Check(UserName, Password) == 0)
                        {
                            MACAdd = GetMAC();
                            IPAddress = GetVisitorIPAddress();
                            HostName = Dns.GetHostName();
                            //if (this.IpAddress_Check(IPAddress) > 0)
                            //{
                                //if (this.LoginExpiry_Check(UserName, Password) == 1)
                                if (true)
                                {
                                    string[] e = Get_EmployeeDetails(UserName);
                                    YantraSession = e;
                                    //LogDetailsInsert(UserName);

                                   // log.add_Insert("Loged With - Mac : " + MACAdd + " , IP Address : " + IPAddress, "1");

                                    string dboard = uPriv.getUserDashboard(e[10]);
                                    page.Response.Redirect("~/dboards/" + dboard, false);


                                    //page.Response.Redirect(HttpContext.Current.Request.QueryString["rurl"]);

                                    //page.Response.Redirect("~/Modules/Home/Default.aspx",false);

                                }
                            //}
                            //else
                            //{
                            //    // _returnStringMessage = "Ip Address Was Not Registered";
                            //    page.Response.Redirect("IPCheck.aspx?UserName=" + UserName);
                            //}
                        }
                        else
                        {
                            _returnStringMessage = "Login Details Are Expiried";
                        }
                    }
                    else
                    {
                        _returnStringMessage = "Invalid Password";
                    }
                }
                else
                {
                    _returnStringMessage = "User Name Is Not Registered";
                }
            }
            catch (Exception ex)
            {
                _returnStringMessage = ex.Message.ToString();
            }
            finally
            {
            }
            return _returnStringMessage;
        }
        private int UserName_Check(string UserName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_USER_DETAILS] WHERE USER_NAME='{0}'", UserName);
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            return _returnIntValue;
        }

        //public string OLoginCheck(string UserName, string Password)
        //{
        //    try
        //    {
        //        if (this.UserName_Check(UserName) > 0 && this.UserNameFromEmployeeMaster_Check(UserName) > 0)
        //        {
        //            if (this.UserNamePwd_Check(UserName, Password) == 0)
        //            {
        //                _returnStringMessage = "0";
        //            }
        //            else
        //            {
        //                _returnStringMessage = "2"; // "Invalid Password";
        //            }
        //        }
        //        else
        //        {
        //            _returnStringMessage = "3"; // "User Name Is Not Registered";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _returnStringMessage = ex.Message.ToString();
        //    }
        //    finally
        //    {
        //    }
        //    return _returnStringMessage;
        //}
        private int UserNameFromEmployeeMaster_Check(string UserName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_EMPLOYEE_MAST] WHERE EMP_USERNAME='{0}'", UserName);
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            return _returnIntValue;
        }

        private int UserNamePwd_Check(string UserName, string Password)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //_commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_USER_DETAILS] WHERE USER_NAME='{0}' AND PASSWORD='{1}'", UserName, Password);
            _commandText = string.Format("EXEC  dbo.Verifyuser @newusername = '{0}', @newpassword = {1}", UserName, Password);
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            return _returnIntValue;
        }

       
        private int IpAddress_Check(string IpAddress)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT  COUNT(*) FROM [Ip_Address_tbl] WHERE Ip_Address='{0}'", IpAddress);
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            return _returnIntValue;
        }
        private int LoginExpiry_Check(string UserName, string Password)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //_commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_USER_DETAILS] WHERE USER_NAME='{0}' AND PASSWORD='{1}' AND ASSIGN_DATE <= '{2}' AND EXPIRY_DATE >= '{2}'", UserName, Password, DateTime.Now.ToString("MM/dd/yyyy"));
            _commandText = string.Format("EXEC dbo.ExipiryCheck @newusername = '{0}', @newpassword = '{1}',@assigndate='{2}',@expirydate='{3}'", UserName, Password, DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("MM/dd/yyyy"));

            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            return _returnIntValue;
        }

        private string[] Get_EmployeeDetails(string UserName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            //_commandText = string.Format("SELECT  YANTRA_EMPLOYEE_MAST.EMP_ID,EMP_FIRST_NAME+' ' + EMP_MIDDLE_NAME + ' ' + EMP_LAST_NAME AS EMP_FULLNAME,EMP_EMAIL,EMP_USERNAME,[YANTRA_EMPLOYEE_MAST].COMPANY_ID,[YANTRA_COMP_PROFILE].CP_FULL_NAME, YANTRA_USER_DETAILS.USER_TYPE,YANTRA_EMPLOYEE_DET.DEPT_ID,YANTRA_EMPLOYEE_DET.DESG_ID FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_COMP_PROFILE], [YANTRA_USER_DETAILS], YANTRA_EMPLOYEE_DET WHERE YANTRA_EMPLOYEE_MAST.COMPANY_ID = YANTRA_COMP_PROFILE.CP_ID and YANTRA_EMPLOYEE_MAST.EMP_USERNAME = YANTRA_USER_DETAILS.USER_NAME and YANTRA_EMPLOYEE_DET.EMP_ID = YANTRA_EMPLOYEE_MAST.EMP_ID and [YANTRA_EMPLOYEE_MAST].EMP_USERNAME='{0}'", UserName);
            _commandText = string.Format("SELECT  YANTRA_EMPLOYEE_MAST.EMP_ID,EMP_FIRST_NAME+' ' + EMP_MIDDLE_NAME + ' ' + EMP_LAST_NAME AS EMP_FULLNAME,EMP_EMAIL,EMP_USERNAME,[YANTRA_EMPLOYEE_MAST].COMPANY_ID,[YANTRA_COMP_PROFILE].CP_FULL_NAME, YANTRA_USER_DETAILS.USER_TYPE,YANTRA_EMPLOYEE_DET.DEPT_ID,YANTRA_EMPLOYEE_DET.DESG_ID, YANTRA_USER_DETAILS.USER_ID FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_COMP_PROFILE], [YANTRA_USER_DETAILS], YANTRA_EMPLOYEE_DET WHERE YANTRA_EMPLOYEE_MAST.COMPANY_ID = YANTRA_COMP_PROFILE.CP_ID and YANTRA_EMPLOYEE_MAST.EMP_USERNAME = YANTRA_USER_DETAILS.USER_NAME and YANTRA_EMPLOYEE_DET.EMP_ID = YANTRA_EMPLOYEE_MAST.EMP_ID and [YANTRA_EMPLOYEE_MAST].EMP_USERNAME='{0}'", UserName);

            string querystr = @"SELECT        YANTRA_EMPLOYEE_MAST.EMP_ID, 
                         YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME + ' ' + YANTRA_EMPLOYEE_MAST.EMP_MIDDLE_NAME + ' ' + YANTRA_EMPLOYEE_MAST.EMP_LAST_NAME AS EMP_FULLNAME, 
                         YANTRA_EMPLOYEE_MAST.EMP_EMAIL, YANTRA_EMPLOYEE_MAST.EMP_USERNAME, YANTRA_EMPLOYEE_MAST.COMPANY_ID, YANTRA_COMP_PROFILE.CP_FULL_NAME, 
                         YANTRA_USER_DETAILS.USER_TYPE, YANTRA_EMPLOYEE_DET.DEPT_ID, YANTRA_EMPLOYEE_DET.DESG_ID, YANTRA_USER_DETAILS.USER_ID, usertype_tbl2.userTypeId
FROM            YANTRA_EMPLOYEE_MAST INNER JOIN
                         YANTRA_COMP_PROFILE ON YANTRA_EMPLOYEE_MAST.COMPANY_ID = YANTRA_COMP_PROFILE.CP_ID INNER JOIN
                         YANTRA_USER_DETAILS ON YANTRA_EMPLOYEE_MAST.EMP_USERNAME = YANTRA_USER_DETAILS.USER_NAME INNER JOIN
                         YANTRA_EMPLOYEE_DET ON YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID LEFT OUTER JOIN
                         usertype_tbl2 ON YANTRA_USER_DETAILS.USER_ID = usertype_tbl2.USER_ID
where [YANTRA_EMPLOYEE_MAST].EMP_USERNAME='{0}'";

            _commandText = string.Format(querystr, UserName);

            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                EmpId = dbManager.DataReader["EMP_ID"].ToString();
                EmpName = dbManager.DataReader["EMP_FULLNAME"].ToString();
                EmpEmail = dbManager.DataReader["EMP_EMAIL"].ToString();
                EmpUserName = dbManager.DataReader["EMP_USERNAME"].ToString();

                CmpId = dbManager.DataReader["COMPANY_ID"].ToString();
                CmpName = dbManager.DataReader["CP_FULL_NAME"].ToString();
                EmpType = dbManager.DataReader["USER_TYPE"].ToString();
                DeptId = dbManager.DataReader["DEPT_ID"].ToString();
                Desgid = dbManager.DataReader["DESG_ID"].ToString();

                UserId = dbManager.DataReader["USER_ID"].ToString();

                userTypeId = dbManager.DataReader["userTypeId"].ToString();

                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            string[] emp = { EmpId, EmpName, EmpEmail, EmpUserName, CmpId, CmpName, EmpType, DeptId, Desgid, UserId, userTypeId };
            return emp;
        }
        
        // CompanyLabelName for MasterPage  
        private string[] Get_EmployeeDetailsForCmpLbl(string UserName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT  EMP_ID,EMP_FIRST_NAME+' ' + EMP_MIDDLE_NAME + ' ' + EMP_LAST_NAME AS EMP_FULLNAME,EMP_EMAIL,EMP_USERNAME,[YANTRA_EMPLOYEE_MAST].COMPANY_ID,[YANTRA_COMP_PROFILE] .CP_FULL_NAME FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_COMP_PROFILE] WHERE [YANTRA_EMPLOYEE_MAST].COMPANY_ID=[YANTRA_COMP_PROFILE].CP_ID and [YANTRA_EMPLOYEE_MAST].EMP_USERNAME='{0}'", UserName);
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                
                CmpName = dbManager.DataReader["CP_FULL_NAME"].ToString();
                _returnIntValue = 1;
            }
            else
            {
                _returnIntValue = 0;
            }
            dbManager.DataReader.Close();
            string[] emp = { EmpId, EmpName, EmpEmail, EmpUserName, CmpId, CmpName, EmpType };
            return emp;
        }

        private static void LogDetailsInsert(string UserName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("INSERT INTO [YANTRA_LOG_DETAILS] SELECT ISNULL(MAX(LOGID),0)+1,'{0}','{1}','{1}' FROM [YANTRA_LOG_DETAILS]", UserName, DateTime.Now.ToString("MM/dd/yyyy"));
            _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
        }

        public static void Privilege_Check(Page page)
        {
            if (YantraSession != null)
            {
                EmpDetails = (string[])YantraSession;
                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                //_commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_USER_PRIVILEGES],[YANTRA_LKUP_PRIVILEGES] WHERE [YANTRA_USER_PRIVILEGES.PRIVILEGE_ID=[YANTRA_LKUP_PRIVILEGES].PRIVILEGE_ID AND [YANTRA_USER_PRIVILEGES].USER_NAME='{0}'", UserName);
                //_returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            }
            else
            {
                page.Response.Redirect("Default.aspx?p=noaccess");
            }
        }

        public static void Session_Check(MasterPage page)
        {
            if (YantraSession != null)
            {
                EmpDetails = (string[])YantraSession;
            }
            else
            {
                page.Response.Redirect("~/Default.aspx");
            }
        }

        public static void ClearSession(MasterPage page)
        {
            YantraSession = null;
            page.Response.Redirect("~/Default.aspx");
        }

        public static string GetEmployeeInSession(Logged_EMP_Details Type)
        {
            if (YantraSession != null)
            {
                EmpDetails = (string[])YantraSession;
                
            }
               return EmpDetails[(int)Type];
        }

        public static string GetCompIds(DataTable dt)
        {
            string cp_Id="";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i + 1 == dt.Rows.Count)
                    {
                        cp_Id = cp_Id + "" + dt.Rows[i][0].ToString() + " ";
                    }
                    else
                    {
                        if (cp_Id == "")
                        {
                            cp_Id = "" + dt.Rows[i][0].ToString() + " ,";

                        }
                        else
                        {
                            cp_Id = cp_Id + "" + dt.Rows[i][0].ToString() + " ,";

                        }
                    }
                }
                //lblCp_Ids.Text = "'" + lblCp_Ids.Text + "'";
            }
            return cp_Id;
        }
        public static DataTable Execute_Command(string _Command, string isSelectCommand)
        {
            int i = 0;
            DataTable dt = new DataTable();
            //SqlCommand cmd = new SqlCommand("USP_Bind_Dropdown_2Columns", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                con.Close();
                con.Open();

                SqlCommand cmd = new SqlCommand(_Command, con);
                cmd.CommandType = CommandType.Text;

                if (isSelectCommand == "Select")
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                else
                {

                    i = cmd.ExecuteNonQuery();
                    con.Close();

                    dt.Columns.Add("Count");
                    DataRow row = dt.NewRow();
                    row["Count"] = i.ToString();
                    dt.Rows.Add(row);
                }

            }
            catch (Exception)
            {
                dt = null;
            }

            return dt;
        }
        public static void UserPrivilegesFill(HiddenField Field)
        {
            Field.Value = "";
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_USER_DETAILS],[YANTRA_USER_PRIVILEGES],[YANTRA_LKUP_PRIVILEGES] WHERE [YANTRA_USER_DETAILS].[USER_ID]=[YANTRA_USER_PRIVILEGES].[USER_ID] AND [YANTRA_USER_PRIVILEGES].PRIVILEGE_ID=[YANTRA_LKUP_PRIVILEGES].PRIVILEGE_ID AND [YANTRA_USER_DETAILS].[USER_NAME]='{0}'", EmpUserName);
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            while (dbManager.DataReader.Read())
            {
                if (Field.Value == "")
                {
                    Field.Value = dbManager.DataReader["PRIVILEGE_NAME"].ToString();
                }
                else
                {
                    Field.Value = Field.Value + "#|#" + dbManager.DataReader["PRIVILEGE_NAME"].ToString();
                }
                _returnIntValue = 1;
            }

            dbManager.DataReader.Close();
        }

        public static int UserPrivilegesFill(string PermissionName)
        {
            if (dbManager.Transaction == null)
                dbManager.Open();
            _commandText = string.Format("SELECT count(*) FROM [YANTRA_USER_DETAILS],[YANTRA_USER_PRIVILEGES],[YANTRA_LKUP_PRIVILEGES] WHERE [YANTRA_USER_DETAILS].[USER_ID]=[YANTRA_USER_PRIVILEGES].[USER_ID] AND [YANTRA_USER_PRIVILEGES].PRIVILEGE_ID=[YANTRA_LKUP_PRIVILEGES].PRIVILEGE_ID AND [YANTRA_USER_DETAILS].[USER_NAME]='{0}' AND [YANTRA_LKUP_PRIVILEGES].PRIVILEGE_NAME='{1}'", EmpUserName, PermissionName);
            _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
            return _returnIntValue;
        }



        public class UserDetails
        {
            public string UserId, UserName, Password, AssignDate, ExpiryDate, PrivelegeId, UserPrivelegeId, UserType, EmpId;   //User Details
            public UserDetails()
            { }

            public static void UserDetails_Select(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                //_commandText = string.Format("select EMP_ID,EMP_FIRST_NAME  +' '+ EMP_LAST_NAME as Full_Name from YANTRA_EMPLOYEE_MAST order by EMP_FIRST_NAME");
                _commandText = "select a.EMP_ID,a.EMP_FIRST_NAME  +' '+ a.EMP_LAST_NAME as Full_Name from YANTRA_EMPLOYEE_MAST a inner join YANTRA_EMPLOYEE_DET b on a.EMP_ID=b.EMP_ID WHERE EMP_USERNAME NOT IN (SELECT [USER_NAME] FROM YANTRA_USER_DETAILS) AND a.EMP_ID<>0 AND b.EMP_DET_DOT>=GETDATE() order by EMP_FIRST_NAME";
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "Full_Name", "EMP_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
            }

            public static void UserDetails_Fill(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select EMP_EMAIL,EMP_FIRST_NAME  +' '+ EMP_LAST_NAME as Full_Name from YANTRA_EMPLOYEE_MAST order by EMP_USERNAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "EMP_USERNAME", "EMP_USERNAME");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
            }

            public static void Privileges_Fill(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select * from YANTRA_LKUP_PRIVILEGES order by PRIVILEGE_NAME");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "PRIVILEGE_NAME", "PRIVILEGE_ID");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
            }

            public static void UserTypesFill(Control ControlForBind)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();

                _commandText = string.Format("select userTypeId,userTypeName from usertype_tbl");
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (ControlForBind is DropDownList)
                {
                    DropDownListBind(ControlForBind as DropDownList, "userTypeName", "userTypeId");
                }
                else if (ControlForBind is GridView)
                {
                    GridViewBind(ControlForBind as GridView);
                }
            }


            public string UserTypes, usertypeid;




            public int UserDetails_Select(string Userid)
            {

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [usertype_tbl],[UserType_Details] WHERE [usertype_tbl].userTypeId =[UserType_Details].User_Type_Id AND  UserType_Details.User_Id = " + Userid + " ");


                dbManager.ExecuteReader(CommandType.Text, _commandText);
                if (dbManager.DataReader.Read())
                {
                    this.UserTypes = dbManager.DataReader["userTypeName"].ToString();
                    this.usertypeid = dbManager.DataReader["User_Type_Id"].ToString();
                    _returnIntValue = 1;
                }
                else
                {
                    _returnIntValue = 0;
                }
                dbManager.DataReader.Close();
                return _returnIntValue;
            }


            public string UserDetails_Save()
            {
              //  if (dbManager.Transaction == null)
                    dbManager.Open();
                if (IsRecordExists("[YANTRA_USER_DETAILS]", "USER_NAME", this.UserName) == false)
                {
                    this.EmpId = AutoGenMaxId("[YANTRA_USER_DETAILS]", "USER_ID");
                    //_commandText = string.Format("INSERT INTO [YANTRA_USER_DETAILS] VALUES({0},'{1}','{2}','{3}','{4}',{5},{6})", this.UserId, this.UserName,this.Password, this.AssignDate, this.ExpiryDate,this.UserType,this.EmpId);
                    _commandText = string.Format("EXEC dbo.Createuser @newuserid = {0}, @Newusername = '{1}',@newpassword = {2}, @Newassigndate = '{3}',@newexpirydate = '{4}',@Newusertype = {5},@newempid= {6};", this.EmpId, this.UserName, this.Password, this.AssignDate, this.ExpiryDate, this.UserType, this.UserId);
                    //_commandText = string.Format("EXEC dbo.Createuser @newuserid = {0}, @Newusername = {1},@newpassword = {2}, @Newassigndate = '{3}',@newexpirydate = '{4}',@Newusertype = {5},@newempid= {6};", this.UserId, this.UserName, this.Password, this.AssignDate, this.ExpiryDate, this.UserType, this.EmpId);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _commandText = string.Format("INSERT INTO [UserType_Details] VALUES({0},{1})", this.UserId, this.UserType);
                    _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Saved Successfully";
                        log.add_Insert("Add User Page Details", "46");

                    }
                }
                else
                {


                    _returnStringMessage = "User Name Already Exists.";
                }
                return _returnStringMessage;
            }

            #region UserPermissions Save
            public void UserPermissions_Save(int UserId, string UserPermissions)
            {
                dbManager.Open();
                _commandText = string.Format("INSERT INTO YANTRA_USER_PERMISSIONS VALUES ({0},'{1}')", UserId, UserPermissions);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            }
            #endregion

            #region UserPermisssions_Delete
            public void UserPermissions_Delete(int UserId)
            {
                dbManager.Open();
                _commandText = string.Format("DELETE FROM YANTRA_USER_PERMISSIONS WHERE UserId=" + UserId);
                dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            }

            #endregion

            #region UserPermissions Select
            public DataTable UserPermissions_Select(int UserId)
            {
                DataTable dtable = new DataTable();
                DataColumn dcol = new DataColumn();
                dcol = new DataColumn("Permissions");
                dtable.Columns.Add(dcol);

                dbManager.Open();
                _commandText = string.Format("SELECT * FROM YANTRA_USER_PERMISSIONS WHERE UserId={0}", UserId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    DataRow drow = dtable.NewRow();
                    drow["Permissions"] = dbManager.DataReader[1].ToString();
                    dtable.Rows.Add(drow);

                }
                dbManager.DataReader.Close();
                return dtable;
            }
            #endregion

            #region UserName & UserPermissions Delete
            public string UserName_UserPermissions_Delete(int UserId)
            {
                if (Authentication.DeleteRecord("YANTRA_USER_DETAILS", "USER_ID", UserId.ToString()) == true)
                {
                    Authentication.DeleteRecord("YANTRA_USER_PERMISSIONS", "UserId", UserId.ToString());
                    _returnStringMessage = "Data Deleted Successfully";
                    log.add_Delete("User Permission Details", "47");


                }
                else
                {
                    _returnStringMessage = "Some Data Missing";
                }
                return _returnStringMessage;

            }
            #endregion
            public int UserDetails_Delete(string UserId)
            {
                if (DeleteRecord("[YANTRA_USER_DETAILS]", "USER_ID", UserId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }

            public string Privelges_Save()
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT PRIVILEGE_ID FROM [YANTRA_LKUP_PRIVILEGES] WHERE PRIVILEGE_NAME='" + this.PrivelegeId + "'");
                _commandText = string.Format("INSERT INTO [YANTRA_USER_PRIVILEGES] SELECT ISNULL(MAX(USER_PRIVILEGES_ID),0)+1,'{0}','{1}' FROM [YANTRA_USER_PRIVILEGES]", this.UserId, dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
                _returnStringMessage = string.Empty;
                if (_returnIntValue < 0 || _returnIntValue == 0)
                {
                    _returnStringMessage = "Some Data Missing.";
                }
                else if (_returnIntValue > 0)
                {
                    _returnStringMessage = "Data Saved Successfully";
                    log.add_Insert("User Privileges Details", "48");

                }
                return _returnStringMessage;
            }

            public int Privelges_Delete(string UserId)
            {
                if (DeleteRecord("[YANTRA_USER_PRIVILEGES]", "USER_ID", UserId) == true)
                { _returnIntValue = 1; }
                else
                { _returnIntValue = 0; }
                return _returnIntValue;
            }

            public string UserDetails_Update()
            {

                SqlCommand cmd = new SqlCommand();

                //if (dbManager.Transaction == null)
                //    dbManager.Open();
                
                con.Close();
                
                if (IsRecordExists("[YANTRA_USER_DETAILS]", "USER_NAME", this.UserName, "USER_ID", this.UserId) == false)
                {
                    if (this.Password.Length > 0)
                    {
                        //_commandText = string.Format("UPDATE [YANTRA_USER_DETAILS] SET USER_NAME='{0}',PASSWORD='{1}',ASSIGN_DATE='{2}',EXPIRY_DATE='{3}',USER_TYPE={5} WHERE USER_ID={4}", this.UserName, this.Password, this.AssignDate, this.ExpiryDate, this.UserId, this.UserType);

                        
                        string instr = "Updateuser";
                        cmd = new SqlCommand(instr, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@newuserid", SqlDbType.BigInt).Value = Convert.ToInt32(this.UserId);
                        cmd.Parameters.Add("@newusername", SqlDbType.VarChar).Value = this.UserName;
                        cmd.Parameters.Add("@newpassword", SqlDbType.VarChar).Value = this.Password;
                        cmd.Parameters.Add("@newassigndate", SqlDbType.DateTime).Value = Convert.ToDateTime(this.AssignDate);
                        cmd.Parameters.Add("@newexpirydate", SqlDbType.DateTime).Value = Convert.ToDateTime(this.ExpiryDate);
                        cmd.Parameters.Add("@newusertype", SqlDbType.BigInt).Value = Convert.ToInt32(this.UserType);
                    }
                    else
                    {
                        //_commandText = string.Format("UPDATE [YANTRA_USER_DETAILS] SET USER_NAME=@USER_NAME,ASSIGN_DATE=@ASSIGN_DATE,EXPIRY_DATE=@EXPIRY_DATE,USER_TYPE=@USER_TYPE WHERE USER_ID=@USER_ID", this.UserName, this.Password, this.AssignDate, this.ExpiryDate, this.UserId, this.UserType);

                        string instr = "UPDATE [YANTRA_USER_DETAILS] SET USER_NAME=@USER_NAME,ASSIGN_DATE=@ASSIGN_DATE,EXPIRY_DATE=@EXPIRY_DATE,USER_TYPE=@USER_TYPE WHERE USER_ID=@USER_ID";
                        cmd = new SqlCommand(instr, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@USER_NAME", SqlDbType.VarChar).Value = this.UserName;
                        cmd.Parameters.Add("@ASSIGN_DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(this.AssignDate);
                        cmd.Parameters.Add("@EXPIRY_DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(this.ExpiryDate);
                        cmd.Parameters.Add("@USER_TYPE", SqlDbType.BigInt).Value = Convert.ToInt32(this.UserType);
                        cmd.Parameters.Add("@USER_ID", SqlDbType.BigInt).Value = Convert.ToInt32(this.UserId);
                    }

                    con.Open();
                    _returnIntValue = cmd.ExecuteNonQuery();
                    con.Close();

                    _returnStringMessage = string.Empty;
                    if (_returnIntValue < 0 || _returnIntValue == 0)
                    {
                        _returnStringMessage = "Some Data Missing.";
                    }
                    else if (_returnIntValue > 0)
                    {
                        _returnStringMessage = "Data Updated Successfully";
                        log.add_Update("User Details", "46");

                    }

                }
                else
                {
                    _returnStringMessage = "User Name Already Exists.";
                }
                return _returnStringMessage;
            }

            //public string UserDetails_Update()
            //{
            //    if (dbManager.Transaction == null)
            //        dbManager.Open();
            //    if (IsRecordExists("[YANTRA_USER_DETAILS]", "USER_NAME", this.UserName, "USER_ID", this.UserId) == false)
            //    {
            //        if (this.Password.Length > 0)
            //        {
            //            _commandText = string.Format("UPDATE [YANTRA_USER_DETAILS] SET USER_NAME='{0}',PASSWORD='{1}',ASSIGN_DATE='{2}',EXPIRY_DATE='{3}',USER_TYPE={5} WHERE USER_ID={4}", this.UserName, this.Password, this.AssignDate, this.ExpiryDate, this.UserId, this.UserType);
            //        }
            //        else
            //        {
            //            _commandText = string.Format("UPDATE [YANTRA_USER_DETAILS] SET USER_NAME='{0}',ASSIGN_DATE='{2}',EXPIRY_DATE='{3}',USER_TYPE={5} WHERE USER_ID={4}", this.UserName, this.Password, this.AssignDate, this.ExpiryDate, this.UserId, this.UserType);
            //        }

            //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);
            //        _commandText = string.Format("UPDATE [UserType_Details] SET User_Type_Id ={0} WHERE USER_ID={1}", this.UserTypes, this.UserId);
            //        _returnIntValue = dbManager.ExecuteNonQuery(CommandType.Text, _commandText);

            //        _returnStringMessage = string.Empty;
            //        if (_returnIntValue < 0 || _returnIntValue == 0)
            //        {
            //            _returnStringMessage = "Some Data Missing.";
            //        }
            //        else if (_returnIntValue > 0)
            //        {
            //            _returnStringMessage = "Data Updated Successfully";
            //            log.add_Update("User Details", "46");

            //        }

            //    }
            //    else
            //    {
            //        _returnStringMessage = "User Name Already Exists.";
            //    }
            //    return _returnStringMessage;
            //}

            public static void UserPrivilegesFill(string UserId, HiddenField Field)
            {
                Field.Value = "";
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_USER_PRIVILEGES],[YANTRA_LKUP_PRIVILEGES] WHERE [YANTRA_USER_PRIVILEGES].PRIVILEGE_ID=[YANTRA_LKUP_PRIVILEGES].PRIVILEGE_ID AND [YANTRA_USER_PRIVILEGES].[USER_ID]='{0}'", UserId);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                while (dbManager.DataReader.Read())
                {
                    if (Field.Value == "")
                    {
                        Field.Value = dbManager.DataReader["PRIVILEGE_NAME"].ToString();
                    }
                    else
                    {
                        Field.Value = Field.Value + "#|#" + dbManager.DataReader["PRIVILEGE_NAME"].ToString();
                    }
                    _returnIntValue = 1;
                }

                dbManager.DataReader.Close();
            }
            public static void AddPrivilegesInList(string PrivilegeName,string PrivilegeDesc,string PageUrl,string CatName,string PageName )
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("INSERT INTO [YANTRA_LKUP_PRIVILEGES] SELECT ISNULL(MAX(PRIVILEGE_ID),0)+1,'{0}','{1}','{2}','{3}','{4}' FROM [YANTRA_LKUP_PRIVILEGES]", PrivilegeName,PrivilegeDesc,PageUrl,CatName,PageName);
                _returnIntValue = int.Parse(dbManager.ExecuteNonQuery(CommandType.Text, _commandText).ToString());
            }

            public static void RemovePrivilegesFromList(string PrivilegeName)
            {
                Authentication.BeginTransaction();
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT PRIVILEGE_ID FROM [YANTRA_LKUP_PRIVILEGES] WHERE PRIVILEGE_NAME='{0}'", PrivilegeName);
                if (DeleteRecord("[YANTRA_USER_PRIVILEGES]", "PRIVILEGE_ID", dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString()) == true)
                {
                    if (DeleteRecord("[YANTRA_LKUP_PRIVILEGES]", "PRIVILEGE_NAME", PrivilegeName) == true)
                    {
                        Authentication.CommitTransaction();
                    }
                    else
                    {
                        Authentication.RollBackTransaction();
                    }
                }
                else
                {
                    Authentication.RollBackTransaction();
                }
            }

            public static int CheckPrivilegesInList(string PrivilegeName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_LKUP_PRIVILEGES] WHERE [YANTRA_LKUP_PRIVILEGES].PRIVILEGE_NAME='{0}'", PrivilegeName);
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                return _returnIntValue;
            }

            public static int CheckPrivilegesCountInList(string PrivilegeName)
            {
                if (dbManager.Transaction == null)
                    dbManager.Open();
                _commandText = string.Format("SELECT  COUNT(*) FROM [YANTRA_LKUP_PRIVILEGES] WHERE [YANTRA_LKUP_PRIVILEGES].PRIVILEGE_NAME LIKE '{0}%'", PrivilegeName);
                _returnIntValue = int.Parse(dbManager.ExecuteScalar(CommandType.Text, _commandText).ToString());
                return _returnIntValue;
            }
        }
    }
}