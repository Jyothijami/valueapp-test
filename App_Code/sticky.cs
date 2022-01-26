using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sticky
/// </summary>
public class sticky
{
	public sticky()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Success_Display(string msg, System.Web.UI.Page page)
    {
        string str = "<script>$.sticky('" + msg + "', { autoclose: 3000, position: 'top-right', type: 'st-success' });</script>";
        page.ClientScript.RegisterStartupScript(page.GetType(), "Script", str, false);
    }

    public static void Information_Display(string msg, System.Web.UI.Page page)
    {
        string str = "<script>$.sticky('" + msg + "', { autoclose: 3000, position: 'top-right', type: 'st-info' });</script>";
        page.ClientScript.RegisterStartupScript(page.GetType(), "Script", str, false);
    }

    public static void Error_Display(string msg, System.Web.UI.Page page1)
    {
        string str = "<script>$.sticky('" + msg + "', { autoclose: 3000, position: 'top-right', type: 'st-error' });</script>";
        page1.ClientScript.RegisterStartupScript(page1.GetType(), "Script", str, false);

    }
}