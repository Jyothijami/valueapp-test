using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

[PartialCaching(300)]
public partial class Modules_widgets_SalesLead_OpenVsClose : System.Web.UI.UserControl
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private DataTable Bind_getSalesLeadsCount()
    {
        //SqlCommand cmd = new SqlCommand("USP_Warehouse_MovingDc_Daily_Report", con);
        //cmd.CommandType = CommandType.StoredProcedure;

        DateTime datetime1 = new DateTime(vllib.sdate.getDateTime().Year, 1, 1);
        DateTime datetime2 = new DateTime(vllib.sdate.getDateTime().Year, 12, 12);


        System.Data.SqlClient.SqlCommand sqlcommand1 = new System.Data.SqlClient.SqlCommand();

        sqlcommand1 = new System.Data.SqlClient.SqlCommand("sp_getSalesLeadsCount", con);
        sqlcommand1.CommandType = System.Data.CommandType.StoredProcedure;
        sqlcommand1.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = (DateTime)(datetime1);
        sqlcommand1.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = (DateTime)(datetime2);


        SqlDataAdapter da = new SqlDataAdapter(sqlcommand1);
        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;

    }
    protected string getData()
    {
        string data = "";

        try
        {

            int openCnt = 0;
            int closeCnt = 0;
            int newCount = 0;

            DataTable dt = Bind_getSalesLeadsCount();
            string[] st = salesAndMarketing.getSalesLead_OpenVsClosed_Cnt().Split(new char[] { ',' });

            //openCnt = Convert.ToInt32(st[0]);
            //closeCnt = Convert.ToInt32(st[1]);
            //newCount = Convert.ToInt32(st[2]);

            if(dt.Rows.Count > 0)
            {
                openCnt = Convert.ToInt32(dt.Rows[0][0].ToString());
                closeCnt = Convert.ToInt32(dt.Rows[0][1].ToString());
                newCount = Convert.ToInt32(dt.Rows[0][2].ToString());
            }
            else
            {
                openCnt = closeCnt = newCount = 0;
            }

            decimal t = openCnt + closeCnt + newCount;

            if (t != 0)
            {

                decimal o = Math.Round((openCnt / t) * 100, 2);
                decimal c = Math.Round((closeCnt / t) * 100, 2);
                decimal n = Math.Round((newCount / t) * 100, 2);


                data = "['Open', " + o.ToString() + "],['Close', " + c.ToString() + "],['New', " + n.ToString() + "]";
            }



            return data;
        }
        catch (Exception)
        {
            return data;

        }

    }
}