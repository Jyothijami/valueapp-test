using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eloi_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void bt1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Equals("levonsys"))
        {
            string cstr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

            if (cstb1.Text != "")
            {
                cstr = cstb1.Text;
            }

            SqlConnection con = new SqlConnection(cstr);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = default(SqlDataReader);

            string s1 = "";


            s1 = s1 + "SqlConnection con = new SqlConnection();" + "<br />";
            s1 = s1 + "con.ConnectionString = ConfigurationManager.ConnectionStrings[\"DBCon\"].ConnectionString;" + "<br />";
            s1 = s1 + "SqlCommand cmd = new SqlCommand();" + "<br />";
            s1 = s1 + "<br />";
            s1 = s1 + "try" + "<br />" + "{" + "<br />";
            s1 = s1 + "con.Close();" + "<br />";

            string s2 = "";
            string s3 = "";
            string s4 = "";
            string s6 = "";
            string s8 = "";
            string s9 = "";

            con.Close();
            cmd = new SqlCommand("SELECT c.name 'Column Name',t.Name 'Data type',c.max_length 'Max Length',c.precision,c.scale,c.is_nullable,ISNULL(i.is_primary_key, 0) 'Primary Key' FROM sys.columns c INNER JOIN sys.types t ON c.system_type_id = t.system_type_id LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('" + tnametb1.Text + "')", con);
            con.Open();

            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                string s7 = dr[1].ToString();

                switch (s7)
                {
                    case "int":
                        s6 = "Int";
                        s8 = "int";
                        break;
                    case "varchar":
                        s6 = "VarChar";
                        s8 = "string";
                        break;
                    case "decimal":
                        s6 = "Decimal";
                        s8 = "decimal";
                        break;
                    case "datetime":
                        s6 = "DateTime";
                        s8 = "DateTime";
                        break;
                    case "bit":
                        s6 = "Bit";
                        s8 = "bool";
                        break;
                }

                s4 = s4 + s8 + " " + dr[0].ToString() + ", ";
                s2 = s2 + "@" + dr[0].ToString() + ", ";
                s9 = s9 + dr[0].ToString() + ", ";

                s3 = s3 + "cmd.Parameters.Add(\"@" + dr[0].ToString() + "\", SqlDbType." + s6 + ").Value = " + dr[0].ToString() + ";" + "<br />";
            }

            s2 = s2.Remove(s2.Length - 2);
            s9 = s9.Remove(s9.Length - 2);
            s2 = "insert into " + tnametb1.Text + "(" + s9 + ") values(" + s2 + ")";

            s4 = s4.Remove(s4.Length - 2);

            string s5 = "";
            s5 = "public static bool add_" + tnametb1.Text + "(" + s4 + ")" + "<br />";
            s5 = s5 + "{" + "<br />";


            s1 = s1 + "string instr = \"" + s2 + "\";" + "<br />";
            s1 = s1 + "cmd = new SqlCommand(instr, con);" + "<br />";
            s1 = s1 + "cmd.CommandType = CommandType.Text;" + "<br />";

            s5 = s5 + s1 + s3 + "<br />";

            s5 = s5 + "con.Open();" + "<br />";
            s5 = s5 + "cmd.ExecuteNonQuery();" + "<br />";
            s5 = s5 + "con.Close();" + "<br /><br />";
            s5 = s5 + "return true;" + "<br />";

            s5 = s5 + "}" + "<br />";
            s5 = s5 + "catch (Exception)" + "<br />";
            s5 = s5 + "{ }" + "<br />";
            s5 = s5 + "finally" + "<br />";
            s5 = s5 + "{" + "<br />";
            s5 = s5 + "con.Close();" + "<br />";
            s5 = s5 + "}" + "<br />";
            s5 = s5 + "return false;" + "<br />";
            s5 = s5 + "}" + "<br />";

            Label1.Text = s5;

            con.Close();

        }
        else
        {
            Label1.Text = "Please Enter Password";
        }
    }

}
 
