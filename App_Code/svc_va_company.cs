using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using vllib;
using System.Data;

/// <summary>
/// Summary description for svc_va_company
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_va_company : System.Web.Services.WebService
{

    public svc_va_company()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public string getCompanies()
    {
        DataSet ds = new DataSet();
        DataTable dt = cp.getCompanies();

        ds.Tables.Add(dt);

        return ds.GetXml();
    }

}
