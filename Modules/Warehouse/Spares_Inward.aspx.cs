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

public partial class Modules_Warehouse_Spares_Inward : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FirstGridViewRow();
        }
    }

    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add("Invoice No", typeof(string));
        dt.Columns.Add("Item Model No", typeof(string));
        dt.Columns.Add("Spare Model No", typeof(string));
        dt.Columns.Add("Sub Category", typeof(string));
        dt.Columns.Add("Brand", typeof(string));
        dt.Columns.Add("Color", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Invoice No"] = string.Empty;
        dr["Item Model No"] = string.Empty;
        dr["Spare Model No"] = string.Empty;
        dr["Sub Category"] = string.Empty;
        dr["Brand"] = string.Empty;
        dr["Color"] = string.Empty;
        dr["Quantity"] = string.Empty;
        dr["Remarks"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        gvSparesInward.DataSource = dt;
        gvSparesInward.DataBind();
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSparesInward.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSparesInward.DataBind();
    }
    protected void gvSparesInward_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    public void AddNewRow()
    {
        //DataRow row = dtSpares.NewRow();
       
        //dtSpares.Rows.Add(row);
        //gvSpares.DataSource = dtSpares; gvSpares.DataBind();
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {

    }
}
 
