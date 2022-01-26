using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;
using YantraBLL.Modules;
using System.Data;
using System.Data.SqlClient;
using vllib;

public partial class Modules_Services_SiteInspection_Report : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            setControlsVisibility();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteSiteInspReport();
        BindGrid();

    }
    private void DeleteSiteInspReport()
    {
        #region Delete Application
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        foreach (GridViewRow gvr in gvSiteReport.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label onDutyId = (Label)gvr.FindControl("lblClientId");
                    int ID = Convert.ToInt32(onDutyId.Text);

                    SqlCommand cmd = new SqlCommand("USP_Delete_SiteInsp_Report", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        MessageBox.Show(this, "Record Deleted Successfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "33");
        btnNew.Enabled = up.add;
        btnDelete.Enabled = up.Delete;

        //btnEdit.Enabled = up.Update;
        //btnDelete.Enabled = up.Delete;
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSiteReport.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSiteReport.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        string str = "New";
        Response.Redirect("Site_Inspection_Report_Details.aspx?ClientID=" + str);
    }
    protected void lbtnClientName_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtn.Parent.Parent;
        gvSiteReport.SelectedIndex = Row.RowIndex;
        Label lblClientId = (Label)Row.FindControl("lblClientId");
        //string Cust_Code = gvCustMasterDetails.SelectedRow.Cells[0].Text;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        Response.Redirect("Site_Inspection_Report_Details.aspx?ClientID=" + lblClientId.Text);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSeearchText.Text == "" && txtClientId.Text == "")
        {
            BindGrid();
        }
        else if (txtSeearchText.Text != "" && txtClientId.Text == "")
        {
            SqlCommand cmd1 = new SqlCommand("select * from Site_Inspection_Report_tbl   where Client_Name ='" + txtSeearchText.Text + "' order by Quotation_Date desc", con);
            cmd1.Parameters.AddWithValue("@Client_Name", txtSeearchText.Text);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            gvSiteReport.DataSource = dt1;
            gvSiteReport.DataBind();
        }
        else if (txtSeearchText.Text == "" && txtClientId.Text != "")
        {
            SqlCommand cmd1 = new SqlCommand("select * from Site_Inspection_Report_tbl   where Client_Name ='" + txtSeearchText.Text + "' and Client_Id ='" + txtClientId.Text + "' order by Quotation_Date desc", con);
            cmd1.Parameters.AddWithValue("@Client_Id", txtClientId.Text);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            gvSiteReport.DataSource = dt1;
            gvSiteReport.DataBind();
        }
        else
        {
            SqlCommand cmd1 = new SqlCommand("select * from Site_Inspection_Report_tbl   where Client_Id ='" + txtClientId.Text + "' order by Quotation_Date desc", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            gvSiteReport.DataSource = dt1;
            gvSiteReport.DataBind();
        }
    }

    private void BindGrid()
    {
        SqlCommand cmd1 = new SqlCommand("select * from Site_Inspection_Report_tbl order by Quotation_Date desc", con);
        //cmd1.Parameters.AddWithValue("Client_Name", txtSeearchText.Text);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        gvSiteReport.DataSource = dt1;
        gvSiteReport.DataBind();
    }

    protected void gvSiteReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSiteReport.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

    }
}

