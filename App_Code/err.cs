using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for err
/// </summary>
public class err
{
	public err()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void log(string error)
    {
        string[] lines = { error };
        // WriteAllLines creates a file, writes a collection of strings to the file, 
        // and then closes the file.
        string rnum;
        rnum = DateTime.Now.ToString("ddMMyyyyhhmmssfff");
        System.IO.File.WriteAllLines(HttpContext.Current.Server.MapPath("~/error_log_files/") + rnum + ".txt", lines);
    }
}