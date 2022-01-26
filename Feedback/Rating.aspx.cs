using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;

public partial class Rating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //DataTable dt = this.GetData("SELECT ISNULL(AVG(Rating), 0) AverageRating, COUNT(Rating) RatingCount FROM UserRatings");
            //Rating1.CurrentRating = Convert.ToInt32(dt.Rows[0]["AverageRating"]);
            //lblRatingStatus.Text = string.Format("{0} Users have rated. Average Rating {1}", dt.Rows[0]["RatingCount"], dt.Rows[0]["AverageRating"]);
        }
    }

    //private DataTable GetData(string query)
    //{
    //    DataTable dt = new DataTable();
    //    string constr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(query))
    //        {
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.CommandType = CommandType.Text;
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                sda.Fill(dt);
    //            }
    //        }
    //        return dt;
    //    }
    //}

    //protected void OnRatingChanged(object sender, RatingEventArgs e)
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("INSERT INTO UserRatings VALUES(@Rating)"))
    //        {
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.CommandType = CommandType.Text;
    //                cmd.Parameters.AddWithValue("@Rating", e.Value);
    //                cmd.Connection = con;
    //                con.Open();
    //                cmd.ExecuteNonQuery();
    //                con.Close();
    //            }
    //        }
    //    }
    //    Response.Redirect(Request.Url.AbsoluteUri);
    //}

}