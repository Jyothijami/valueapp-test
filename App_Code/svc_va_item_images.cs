using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for svc_va_item_images
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_va_item_images : System.Web.Services.WebService {

    public svc_va_item_images () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int getLastModified_Count(int last_Updated_Number)
    {
        return itemImages.getLastModified_Count(last_Updated_Number);
    }

    [WebMethod]
    public string getLastModified_Items(int last_Updated_Number)
    {
        return itemImages.getLastModified_Items(last_Updated_Number).GetXml();
    }

    [WebMethod]
    public string getItemDetails(int Item_Image_Id)
    {
        return itemImages.getItemDetails(Item_Image_Id).GetXml();
    }
    
}
