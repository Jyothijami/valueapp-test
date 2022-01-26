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

public partial class waste_waste9 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string parentId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLocation();

        }
    }

    private void BindLocation()
    {
        SqlCommand cmd = new SqlCommand("select * from location_tbl", con);
        cmd.CommandType = CommandType.Text;
        //cmd.Parameters.AddWithValue("@itemcode", ddlMain.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlLocation1.DataTextField = ds.Tables[0].Columns["locname"].ToString();
        ddlLocation1.DataValueField = ds.Tables[0].Columns["locid"].ToString();
        ddlLocation1.DataSource = ds.Tables[0];
        ddlLocation1.DataBind();
        ddlLocation1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
   

    protected void ddlLocation1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string LocId = ddlLocations.DataValueField;
        SqlCommand cmd = new SqlCommand("select wh_id, whname from warehouse_tbl where locid=@locid", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@locid", ddlLocation1.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            d1.Visible = false;

        }
        else
        {
            d1.Visible = true;
            ddlLocation2.DataTextField = ds.Tables[0].Columns["whname"].ToString();
            ddlLocation2.DataValueField = ds.Tables[0].Columns["wh_id"].ToString();
            ddlLocation2.DataSource = ds.Tables[0];
            ddlLocation2.DataBind();
            ddlLocation2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void ddlLocation2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("select [whLocId], [whLocName]  FROM [vltn_13_11].[dbo].[WH_Locations] where wh_id=@wh_id", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@wh_id", ddlLocation2.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            d2.Visible = false;
        }
        else
        {

            d2.Visible = true;
            ddlLocation3.DataTextField = ds.Tables[0].Columns["whLocName"].ToString();
            ddlLocation3.DataValueField = ds.Tables[0].Columns["whLocId"].ToString();

            ddlLocation3.DataSource = ds.Tables[0];
            ddlLocation3.DataBind();
            ddlLocation3.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
   
    protected void ddlLocation3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("select whLocId, [whLocName] from dbo.WH_Locations where parentId like (@whLocId) and wh_id=@wh_id", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@wh_id", ddlLocation2.SelectedValue);
        cmd.Parameters.AddWithValue("@whLocId", ddlLocation3.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            d3.Visible = false;
        }
        else
        {
            d3.Visible = true;
            ddlLocation4.DataTextField = ds.Tables[0].Columns["whLocName"].ToString();
            ddlLocation4.DataValueField = ds.Tables[0].Columns["whLocId"].ToString();
            ddlLocation4.DataSource = ds.Tables[0];
            ddlLocation4.DataBind();
            ddlLocation4.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void ddlLocation4_SelectedIndexChanged(object sender, EventArgs e)
    {
         SqlCommand cmd = new SqlCommand("select whLocId, [whLocName] from dbo.WH_Locations where parentId like (@whLocId) and wh_id=@wh_id", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@wh_id", ddlLocation2.SelectedValue);
        cmd.Parameters.AddWithValue("@whLocId", ddlLocation4.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            d4.Visible = false;
        }
        else
        {
            d4.Visible = true;
            ddlLocation5.DataTextField = ds.Tables[0].Columns["whLocName"].ToString();
            ddlLocation5.DataValueField = ds.Tables[0].Columns["whLocId"].ToString();
            ddlLocation5.DataSource = ds.Tables[0];
            ddlLocation5.DataBind();
            ddlLocation5.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
   
}

