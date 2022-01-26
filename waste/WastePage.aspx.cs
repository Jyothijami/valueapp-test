using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using vllib;
using System.Data.SqlClient;
using Yantra.Classes;
using YantraBLL.Modules;
using System.Collections.Generic;
using Yantra.MessageBox;

public partial class waste_WastePage : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindMainLoc();
        }
    }

    private void BindMainLoc()
    {
        SqlCommand cmd = new SqlCommand("select * from location_tbl", con);
        cmd.CommandType = CommandType.Text;
        //cmd.Parameters.AddWithValue("@itemcode", ddlMain.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlMain.DataTextField = ds.Tables[0].Columns["locname"].ToString();
        ddlMain.DataValueField = ds.Tables[0].Columns["locid"].ToString();
        ddlMain.DataSource = ds.Tables[0];
        ddlMain.DataBind();
        ddlMain.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mainloc = ddlMain.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("select wh_id,whname from warehouse_tbl1 where  locid= " + mainloc + " order by whname", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlSubLoc1.DataTextField = ds.Tables[0].Columns["whname"].ToString();
        ddlSubLoc1.DataValueField = ds.Tables[0].Columns["wh_id"].ToString();
        ddlSubLoc1.DataSource = ds.Tables[0];
        ddlSubLoc1.DataBind();
        ddlSubLoc1.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlSubLoc1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mainloc = ddlMain.SelectedItem.Value;
        string SubLoc1 = ddlSubLoc1.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("select whLocId,whLocName from WH_Locations1 where  parentId= " + SubLoc1 + " order by whLocName", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlSubLoc2.DataTextField = ds.Tables[0].Columns["whLocName"].ToString();
        ddlSubLoc2.DataValueField = ds.Tables[0].Columns["whLocId"].ToString();
        ddlSubLoc2.DataSource = ds.Tables[0];
        ddlSubLoc2.DataBind();
        ddlSubLoc2.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlSubLoc2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mainloc = ddlMain.SelectedItem.Value;
        string SubLoc1 = ddlSubLoc2.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("select whLocId,whLocName from WH_Locations1 where  parentId= " + SubLoc1 + " order by whLocName", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlSubLoc3.DataTextField = ds.Tables[0].Columns["whLocName"].ToString();
        ddlSubLoc3.DataValueField = ds.Tables[0].Columns["whLocId"].ToString();
        ddlSubLoc3.DataSource = ds.Tables[0];
        ddlSubLoc3.DataBind();
        ddlSubLoc3.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlSubLoc3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mainloc = ddlMain.SelectedItem.Value;
        string SubLoc1 = ddlSubLoc3.SelectedItem.Value;
        SqlCommand cmd = new SqlCommand("select whLocId,whLocName from WH_Locations1 where  parentId= " + SubLoc1 + " order by whLocName", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlSubLoc4.DataTextField = ds.Tables[0].Columns["whLocName"].ToString();
        ddlSubLoc4.DataValueField = ds.Tables[0].Columns["whLocId"].ToString();
        ddlSubLoc4.DataSource = ds.Tables[0];
        ddlSubLoc4.DataBind();
        ddlSubLoc4.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void ddlSubLoc4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

