using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Yantra.MessageBox;
using Yantra.Classes;

public partial class Modules_Masters_user_Activity_Log : basePage
{
    public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            loadGrid();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");


    }

    protected void btnShow1_Click(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void loadGrid()
    {
        try
        {
            DataTable dt = getLogDetails();
            GridView2.DataSource = dt;
            GridView2.DataBind();

        }
        catch(Exception)
        {
            MessageBox.Show(this, "Please Reload the Page");
        }
        
        //if (ddlUser1.SelectedValue == "0")
        //{
        //    GridView2.DataSource = log.getLogDetails_All();
        //    GridView2.DataBind();
        //}
        //else
        //{
        //    GridView2.DataSource = log.getLogDetails_byUser(ddlUser1.SelectedValue);
        //    GridView2.DataBind();
        //}
    }

    private DataTable getLogDetails()
    {
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("sp_getActivityLog_All_2", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (ddlUser1.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@User_Id", ddlUser1.SelectedItem.Value);
        }
        if (txtUserName.Text != "")
        {
            cmd.Parameters.AddWithValue("@username", txtUserName.Text);
        }

        if (txtCatId.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cat_Id", txtCatId.Text);
        }
        if (txtCatName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cat_Name", txtCatName.Text);

        }
        if (txtFDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        da.Fill(dt);

        return dt;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        loadGrid();

    }
    protected void ddlNoOfRecords1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(ddlNoOfRecords1.SelectedValue);
        GridView1.DataBind();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        loadGrid();
    }
}

