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
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using System.Data.SqlClient;
using vllib;

public partial class Modules_SM_Customer_Info_Log_Activity : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
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
        GridView1.Visible = true;
        loadGrid();
    }

    protected void loadGrid()
    {
        if (ddlUser1.SelectedValue == "0")
        {
            GridView1.DataSource = log.getLogDetails_All_Cust();
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = log.getLogDetails_byUser_Cust(ddlUser1.SelectedValue);
            GridView1.DataBind();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        loadGrid();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        GridView1.DataBind();
    }
    

}
 
