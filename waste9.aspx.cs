using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class waste9 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int flag;
    //11047


    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[USP_GetItemsReportAll]", con);
        cmd.CommandType = CommandType.StoredProcedure;
       // cmd.Parameters.AddWithValue("@BrandId", "0");
        //if (flag == 1)
        //{
        //    cmd.Parameters.AddWithValue("@Flag", 1);
        //}
        //else
        //{
        //    cmd.Parameters.AddWithValue("@Flag", 0);
        //}
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        Label1.Text = dt.Rows.Count.ToString();
        int x = 0;

        foreach(DataRow dr in dt.Rows)
        {
            if (System.IO.File.Exists(Server.MapPath("~/Content/ItemImage/") + dr["Item_Image"].ToString()))
            {
                x++;
            }
        }

        Label1.Text = Label1.Text + " / " + x.ToString();



    }
}