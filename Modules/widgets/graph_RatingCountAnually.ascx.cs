using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using vllib;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
[PartialCaching(300)]
public partial class Modules_widgets_graph_RatingCountAnually : System.Web.UI.UserControl
{
    DataTable dt;

    protected void Page_Init(object sender, EventArgs e)
    {
        dt = getQuotationRpt("1");
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string getMonths()
    {
        string st = "";
        int year, month, day;
        year = 2014;
        month = 1;
        day = 1;

        DataTable dt2 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "year", "month", "yr_mnth");

        foreach (DataRow dr in dt2.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            year = Convert.ToInt32(dr["year"].ToString());
            month = Convert.ToInt32(dr["month"].ToString());

            string monthName = new DateTime(year, month, day).ToString("MMM", CultureInfo.InvariantCulture);
            st = st + "'" + monthName + "-" + dr["year"].ToString() + "'";
        }

        return st;
    }

    protected string getData()
    {
        string st = "";

        //DataTable dt2 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "year", "month", "yr_mnth");

        //DataTable dt2 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "CP_ID");
        DataTable dt2 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "year", "month", "yr_mnth");


        DataTable cpid_dt = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "Recommend", "Recommend1");



        foreach (DataRow cdr in cpid_dt.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            string str1 = "";

            foreach (DataRow ddr in dt2.Rows)
            {
                if (!str1.Equals(""))
                {
                    str1 = str1 + ",";
                }
                try
                {

                    DataTable dt5 = dt.Select("Recommend = " + cdr["Recommend"].ToString() + " and yr_mnth = '" + ddr["yr_mnth"].ToString() + "'").CopyToDataTable().DefaultView.ToTable(true, "sqCnt");

                    if (dt5.Rows.Count > 0)
                    {
                        str1 = str1 + dt5.Rows[0][0].ToString();
                    }
                }
                catch (Exception)
                {
                    str1 = str1 + "0";
                }
            }

            st = st + "{name: '" + cdr["Recommend1"].ToString() + "', data: [" + str1 + "]}";
        }




        return st;
    }

    protected string getData2()
    {
        string st = "";

        //DataTable dt2 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "year", "month", "yr_mnth");

        DataTable dt2 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "Recommend");

        foreach (DataRow drr in dt2.Rows)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            string cpstr = "";

            //var results = (from myRow in dt.AsEnumerable()
            //              select myRow)
            //              .Distinct();

            DataTable dt3 = dt.Select("").CopyToDataTable().DefaultView.ToTable(true, "Recommend", "year", "month", "sqCnt", "yr_mnth");

            foreach (DataRow dr in dt3.Rows)
            {
                if (!cpstr.Equals(""))
                {
                    cpstr = cpstr + ",";
                }
                if (dr["Recommend"].ToString() == drr["Recommend"].ToString())
                {
                    cpstr = cpstr + dr["sqCnt"].ToString();
                }
                else
                {
                    cpstr = cpstr + "0";
                }
            }

            st = st + "{name: 'CP-1', data: [" + cpstr + "]}";
        }

        //var results = from myRow in dt.AsEnumerable()
        //              where myRow.Field<Int64>("CP_ID") == 1
        //              select myRow;

        //foreach (DataRow dr in results)
        //{
        //    if (!st.Equals(""))
        //    {
        //        st = st + ",";
        //    }

        //    st = st + dr["sqCnt"].ToString();
        //}

        return st;
    }
    public static DataTable getQuotationRpt(string Recommend)
    {
        DateTime now = DateTime.Now;
        DateTime dateTime = now.AddMonths(-12);
        DateTime now2 = DateTime.Now;
        DataTable dataTable = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("dsp_FeedbackRating_RPT", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@dt1", SqlDbType.DateTime).Value = dateTime;
            sqlCommand.Parameters.Add("@dt2", SqlDbType.DateTime).Value = now2;
            sqlCommand.Parameters.Add("@cp_id", SqlDbType.BigInt).Value = Recommend;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        return dataTable;
    }

}