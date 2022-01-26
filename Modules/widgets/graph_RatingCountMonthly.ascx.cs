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

public partial class Modules_widgets_graph_RatingCountMonthly : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string getQuotData()
    {
        string st = "";

        DataTable dt = getRate5Count_Monthly();

        for (int i = 0; i < 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception)
            {
                st = st + "0";
            }
        }

        return st;
    }

    protected string getPOData()
    {
        string st = "";

        DataTable dt = getRate4Count_Monthly();

        for (int i = 0; i < 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception)
            {
                st = st + "0";
            }
        }

        return st;
    }

    protected string getRate3Data()
    {
        string st = "";

        DataTable dt = getRate3Count_Monthly();

        for (int i = 0; i < 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception)
            {
                st = st + "0";
            }
        }

        return st;
    }
    protected string getRate2Data()
    {
        string st = "";

        DataTable dt = getRate2Count_Monthly();

        for (int i = 0; i < 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception)
            {
                st = st + "0";
            }
        }

        return st;
    }
    protected string getRate1Data()
    {
        string st = "";

        DataTable dt = getRate1Count_Monthly();

        for (int i = 0; i < 12; i++)
        {
            if (!st.Equals(""))
            {
                st = st + ",";
            }

            try
            {
                st = st + dt.Rows[i][0].ToString();
            }
            catch (Exception)
            {
                st = st + "0";
            }
        }

        return st;
    }
    public static DataTable getRate5Count_Monthly()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("sp_getRate5Count_Monthly", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        return dataTable;
    }
    public static DataTable getRate4Count_Monthly()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("sp_getRate4Count_Monthly", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        return dataTable;
    }
    public static DataTable getRate3Count_Monthly()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("sp_getRate3Count_Monthly", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        return dataTable;
    }
    public static DataTable getRate2Count_Monthly()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("sp_getRate2Count_Monthly", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        return dataTable;
    }
    public static DataTable getRate1Count_Monthly()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("sp_getRate1Count_Monthly", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
        }
        return dataTable;
    }
}