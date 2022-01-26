using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for svc_va_DC
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_va_DC : System.Web.Services.WebService {

    public svc_va_DC () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string getAllDCs(string CP_ID)
    {
        return dc.getALLDCs(CP_ID).GetXml();
    }

    [WebMethod]
    public string getDC_Detail(string DCno)
    {
        return dc.getDC_Detail(DCno).GetXml();
    }

    [WebMethod]
    public string getDC_ItemDetails(string DCno)
    {
        return dc.getDC_ItemDetails(DCno).GetXml();
    }

    [WebMethod]
    public string getDC_CustomerDetails(string DCno)
    {
        return dc.getDC_CustomerDetails(DCno).GetXml();
    }
    
}
