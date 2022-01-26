<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        string ipaddress;
        ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddress == "" || ipaddress == null)
            ipaddress = Request.ServerVariables["REMOTE_ADDR"];

        string username = "";

        try
        {
            username = Session["vl_username"].ToString();
        }
        catch (Exception)
        {
            
        }
        
        string[] lines = { ipaddress + " - " + username + " - " + Server.GetLastError().ToString() };
        // WriteAllLines creates a file, writes a collection of strings to the file, 
        // and then closes the file.
        string rnum;
        rnum = DateTime.Now.ToString("ddMMyyyyhhmmssfff");
        System.IO.File.WriteAllLines( Server.MapPath("~/error_log_files/") + rnum + ".txt", lines);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
