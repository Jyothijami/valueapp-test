using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for notif_tasks
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class notif_tasks : System.Web.Services.WebService
{

    public notif_tasks()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public string markRead(string unqid)
    {
        string userid = "0";

        try
        {
            userid = HttpContext.Current.Session["vl_userid"].ToString();
        }
        catch (Exception) { }

        readRecords.markRead(unqid, Convert.ToInt32(userid), true);
        
        return "True";
    }
}
