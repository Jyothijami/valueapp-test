using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using YantraDAL;
using System.Data.SqlClient;
using vllib;

/// <summary>
/// Summary description for Search
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Search : System.Web.Services.WebService 
{
    [WebMethod]
    public string[] AutoCompleteAjaxRequest(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetDataFromDataBase(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["ITEM_MODEL_NO"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    public DataTable GetDataFromDataBase(string prefixText)
    {

        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        DataTable _objdt = new DataTable();
        string querystring = "select * from YANTRA_ITEM_MAST where ITEM_MODEL_NO like '%" + prefixText + "%';";
        SqlConnection _objcon = new SqlConnection(connectionstring);
        SqlDataAdapter _objda = new SqlDataAdapter(querystring, _objcon);
        _objcon.Open();
        _objda.Fill(_objdt);
        return _objdt;
    }
    
}
