using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for svc_quotations
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_quotations : System.Web.Services.WebService {

    public svc_quotations () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public int getLastModified_Count(string last_Updated_Number)
    //{
    //    return item.getLastModified_Count(last_Updated_Number);
    //}

    [WebMethod]
    public string getLastModified_Quotations(int last_Updated_Number)
    {
        return SQ.sv_getLastModified_Quotations(last_Updated_Number).GetXml();
    }

    [WebMethod]
    public string getQuotationDetail(int QuotId)
    {
        return SQ.sv_getQuotationDetails(QuotId).GetXml();
    }
}
