using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for svc_po
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_po : System.Web.Services.WebService {

    public svc_po () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string getAllPOs(string CP_ID)
    {
        return po.getALLPOs(CP_ID).GetXml();
    }

    [WebMethod]
    public string getPO_Detail(string pono)
    {
        return po.getPO_Detail(pono).GetXml();
    }

    [WebMethod]
    public string getPO_ItemDetails(string pono)
    {
        return po.getPO_ItemDetails(pono).GetXml();
    }

    [WebMethod]
    public string getPO_SupplierDetails(string pono)
    {
        return po.getPO_SupplierDetails(pono).GetXml();
    }
}
