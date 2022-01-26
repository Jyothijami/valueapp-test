using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Yantra.MessageBox;
using YantraBLL.Modules;
using Yantra.Classes;
using vllib;

public partial class waste_waste8 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            
        }
    }


    protected void ddlLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.DataBind();
        SetWarehouseLocations();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetWarehouseLocations();
    }
    
    protected void SetWarehouseLocations()
    {

        string wh_id = ddlBranch.SelectedValue;
        SqlCommand cmd = new SqlCommand("select * from WH_Locations where wh_id = @wh_id ", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add("@wh_id", SqlDbType.VarChar).Value = wh_id;
        //cmd.Parameters.Add("@parentId", SqlDbType.Int).Value = Convert.ToInt32(parentId);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //txtWh_Id.Text = wh_id;
    }

    protected void ddlSubBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            string whLocId = ddlSubBranch.SelectedValue;
            SqlCommand cmd = new SqlCommand("select *  FROM [vltn_13_11].[dbo].[v_WH_Location_Link] where [whLocId]=@whLocId", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@whLocId", SqlDbType.VarChar).Value = whLocId;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtWhLocId.Text = whLocId;
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}


