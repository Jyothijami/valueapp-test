<%@ WebHandler Language="C#" Class="EmpImage" %>

using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

public class EmpImage : IHttpHandler
{
    long seq = 0;
    byte[] empPic = null;

    public void ProcessRequest(HttpContext context)
    {
        Int32 Productid = 1;
        if (context.Request.QueryString["id"] != null)
            Productid = Convert.ToInt32(context.Request.QueryString["id"]);
        else
            throw new ArgumentException("No parameter specified");

        context.Response.ContentType = "image/jpg";
        Stream strm = ShowEmpImage(Productid);
        if (strm != null)
        {
            byte[] buffer = new byte[4096];
            int byteSeq = strm.Read(buffer, 0, 4096);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = strm.Read(buffer, 0, 4096);
            }
        }
    }

    public Stream ShowEmpImage(int Productid)
    {
        string conn = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        SqlConnection connection = new SqlConnection(conn);
        string sql = "SELECT EMP_PHOTO FROM YANTRA_EMPLOYEE_MAST  WHERE EMP_ID =@ID";
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@ID", Productid);
        connection.Open();
        object img = cmd.ExecuteScalar();
        try
        {
            return new MemoryStream((byte[])img);
        }
        catch
        {
            return null;
        }
        finally
        {
            connection.Close();
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