using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for svc_va_items
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_va_items : System.Web.Services.WebService {

    public svc_va_items () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int getLastModified_Count(string last_Value)
    {
        return item.getLastModified_Count(last_Value);
    }

    [WebMethod]
    public string getLastModified_Items(string last_Value)
    {
        return item.getLastModified_Items(last_Value).GetXml();
    }

    [WebMethod]
    public string getItemDetails(int Item_Code)
    {
        return item.getItemDetails(Item_Code).GetXml();
    }

    [WebMethod]
    public string getItemUOMs()
    {
        return item.getItemUOMs().GetXml();
    }
}
