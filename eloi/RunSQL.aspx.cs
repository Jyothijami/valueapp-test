using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class eloi_RunSQL : System.Web.UI.Page
{
	SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
	

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnupload1_Click(object sender, EventArgs e)
    {
        if (System.IO.File.Exists(Server.MapPath("~/eloi/upload_scripts/") + "script.sql"))
        {
            System.IO.File.Delete(Server.MapPath("~/eloi/upload_scripts/") + "script.sql");
        }

        FileUpload1.SaveAs(Server.MapPath("~/eloi/upload_scripts/") + "script.sql");
    }
    protected void btnExecute1_Click(object sender, EventArgs e)
    {
        ExecuteSQL();
    }

    protected void ExecuteSQL()
    {

        // Url of the T-SQL file you want to run
        string fileUrl = Server.MapPath("~/eloi/upload_scripts/") + "script.sql";

        // Timeout of batches (in seconds)
        int timeout = 600;

        SqlConnection conn = null;
        try
        {
            this.Response.Write(String.Format("Opening url {0}<BR>", fileUrl));

            // read file
            WebRequest request = WebRequest.Create(fileUrl);
            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                this.Response.Write("Connecting to SQL Server database...<BR>");

                // Create new connection to database
                conn = dbc.con;

                conn.Open();

                while (!sr.EndOfStream)
                {
                    StringBuilder sb = new StringBuilder();
                    SqlCommand cmd = conn.CreateCommand();

                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        if (s != null && s.ToUpper().Trim().Equals("GO"))
                        {
                            break;
                        }

                        sb.AppendLine(s);
                    }

                    // Execute T-SQL against the target database
                    cmd.CommandText = sb.ToString();
                    cmd.CommandTimeout = timeout;

                    cmd.ExecuteNonQuery();
                }

            }
            this.Response.Write("T-SQL file executed successfully");
        }
        catch (Exception ex)
        {
            this.Response.Write(String.Format("An error occured: {0}", ex.ToString()));
        }
        finally
        {
            // Close out the connection
            //
            if (conn != null)
            {
                try
                {
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception e)
                {
                    this.Response.Write(String.Format(@"Could not close the connection.  Error was {0}", e.ToString()));
                }
            }
        }       
    }
	SqlConnection test = new SqlConnection("Data Source=VALUELINE;Initial Catalog =ASPState; Persist Security Info=True;User ID=sa;Password=Password@123;max pool size=31000");
	

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = dbc.con;
            string squery = TextBox1.Text;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(squery, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            ds.Tables.Add(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch(Exception ex)
        {
            
        }
        
    }
	
	protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            test.Close();
			test.Open();
            string squery = TextBox1.Text;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(squery, test);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            ds.Tables.Add(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch(Exception ex)
        {
            
        }
        
    }
	
}
 
