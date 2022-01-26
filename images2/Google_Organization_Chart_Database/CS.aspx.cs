using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    [WebMethod]
    public static List<object> GetChartData()
    {
        string query = "select YANTRA_EMPLOYEE_MAST .EMP_ID as EmployeeId,EMP_FIRST_NAME +' '+EMP_LAST_NAME as EmployeeName,DESG_NAME,dept_name,EMP_PHOTO,YANTRA_DEPT_MAST.dept_head ";
        query += " from YANTRA_EMPLOYEE_DET inner join YANTRA_EMPLOYEE_MAST on YANTRA_EMPLOYEE_DET .EMP_ID =YANTRA_EMPLOYEE_MAST .EMP_ID inner join YANTRA_DEPT_MAST on YANTRA_EMPLOYEE_DET .DEPT_ID =YANTRA_DEPT_MAST .DEPT_ID inner join  YANTRA_DESG_MAST on YANTRA_EMPLOYEE_DET .DESG_ID =YANTRA_DESG_MAST .DESG_ID where STATUS !=0 order by YANTRA_EMPLOYEE_MAST.EMP_ID asc";
        
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<object> chartData = new List<object>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new object[]
                {
                    sdr["EmployeeId"], sdr["EmployeeName"], sdr["DESG_NAME"] , sdr["dept_name"],sdr["EMP_PHOTO"],sdr["dept_head"]
                });
                    }
                }
                con.Close();
                return chartData;
            }
        }
    }
}