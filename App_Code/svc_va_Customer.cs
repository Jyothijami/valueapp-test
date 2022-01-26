﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using vllib;

/// <summary>
/// Summary description for svc_va_Customer
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class svc_va_Customer : System.Web.Services.WebService {

    public svc_va_Customer () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string getAllCustomers(string CP_ID)
    {
        //return customer.getAllCustomers(CP_ID).GetXml();
        return customer.getAllCustomers().GetXml();
    }
    
}
