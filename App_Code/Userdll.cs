using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Userdll
/// </summary>
using System.Data;
using vllib;
using System.Data.SqlClient;
using System.Configuration;

namespace Jamilib
{
    public class User_Permissions
    {
        public bool add;
        public bool Update;
        public bool Delete;
        public bool Approve;
        public bool Print;
        public bool Email;
        public bool Full_ReadOnly;
        public bool AdminGridRights;

        public User_Permissions(string UserId, string Privilege_ID)
        {
            DataTable permission = uPriv.getPermission(Privilege_ID, UserId);
            if (permission.Rows.Count == 0)
            {
                this.add = false;
                this.Update = false;
                this.Delete = false;
                this.Approve = false;
                this.Print = false;
                this.Email = false;
                this.Full_ReadOnly = false;
                this.AdminGridRights = false;
            }
            else
            {
                foreach (DataRow row in (InternalDataCollectionBase)permission.Rows)
                {
                    if (row["Permission_Id"].ToString().Equals("1"))
                        this.add = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("2"))
                        this.Update = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("3"))
                        this.Delete = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("4"))
                        this.Approve = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("5"))
                        this.Print = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("6"))
                        this.Email = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("7"))
                        this.Full_ReadOnly = !row["permission"].ToString().Equals("0");
                    if (row["Permission_Id"].ToString().Equals("8"))
                        this.AdminGridRights = !row["permission"].ToString().Equals("0");
                }
            }
        }


        public static bool getPermission(string Permission_ID, string Privilege_ID, string User_ID)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                SqlCommand selectCommand = new SqlCommand("select permission from YANTRA_USER_PERMISSIONS where UserId = @UserId and Permission_Id = @Permission_Id and PRIVILEGE_ID = @PRIVILEGE_ID", connection);
                selectCommand.CommandType = CommandType.Text;
                selectCommand.Parameters.Add("@UserId", SqlDbType.BigInt).Value = (object)Convert.ToInt32(User_ID);
                selectCommand.Parameters.Add("@Permission_Id", SqlDbType.BigInt).Value = (object)Convert.ToInt32(Permission_ID);
                selectCommand.Parameters.Add("@PRIVILEGE_ID", SqlDbType.BigInt).Value = (object)Convert.ToInt32(Privilege_ID);
                new SqlDataAdapter(selectCommand).Fill(dataTable);
            }
            return dataTable.Rows.Count != 0 && dataTable.Rows[0][0].ToString().Equals("1");
        }

    }
}
