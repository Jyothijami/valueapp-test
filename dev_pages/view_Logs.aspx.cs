using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class dev_pages_view_Logs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnrefresh1_Click(object sender, EventArgs e)
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/error_log_files/"));
        FileInfo[] files = dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

        //string[] filesPath = Directory.GetFiles(Server.MapPath("~/error_log_files/"));

        DataTable dt = new DataTable();
        dt.Columns.Add("fname");
        dt.Columns.Add("createdOn");

        foreach (FileInfo path in files)
        {
            dt.Rows.Add(Path.GetFileName(path.FullName), path.CreationTime.ToString());
        }

        
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open")
        {
            try
            {
                using (StreamReader sr = new StreamReader(Request.MapPath(e.CommandArgument.ToString())))
                {
                    String line = sr.ReadToEnd();
                    txtLit1.Text = line;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exc.Message);
            }
        }
    }
}
 
