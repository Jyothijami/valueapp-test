<%@ WebHandler Language="C#" Class="DownloadFile" %>

using System;
using System.Web;

public class DownloadFile : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {

        if (System.Web.HttpContext.Current.Request.QueryString["dt"] == "Tally")
        {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/x-msdownload";
            response.AddHeader("Content-Disposition", "attachment; filename=VlineTallyConnect.exe;");
            response.TransmitFile(HttpContext.Current.Server.MapPath("~/Modules/vTallyConnect/VlineTallyConnect.exe"));
            response.Flush();
            response.End();
        }
        else
        {

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/x-msdownload";
            response.AddHeader("Content-Disposition", "attachment; filename=eQuotationSFX.exe;");
            response.TransmitFile(HttpContext.Current.Server.MapPath("~/Modules/eQuotation/eQuotationSFX.exe"));
            response.Flush();
            response.End();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}