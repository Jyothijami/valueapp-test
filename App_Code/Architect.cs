using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using YantraBLL.Modules;


/// <summary>
/// Summary description for Architect
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Architect : System.Web.Services.WebService {

    public Architect () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(UseHttpGet = true, ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]

    public void ArchNameExists(string userName)
    {
        string mobileno = string.Empty, email = string.Empty;
        bool userNameInUse = false;
        string cs = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlCommand cmd = new SqlCommand("spArchNameExists", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@UserName",
                Value = userName
            });
            con.Open();
            userNameInUse = Convert.ToBoolean(cmd.ExecuteScalar());
        }
        Masters.Architect regsitration = new Masters.Architect();
        regsitration.Architect_Name  = userName;
        regsitration.UserNameInUse = userNameInUse;
        regsitration.Architect_Mobile  = mobileno;
        regsitration.Architect_Email  = email;
        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(regsitration));
    }

    
}
