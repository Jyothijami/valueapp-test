using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using vllib;

public partial class Modules_Warehouse_InwardStock2 : basePage
{
    static DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvSetBind();
            setControlsVisibility();
        }
        else
        {
            //gvBind();
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "93");
        btnUpdate.Enabled = up.Update;
        //btnCreateMRN1.Enabled=up.CreateMRN

    }

    protected void gvSetBind()
    {
        SqlConnection con = dbc.con;

        dt = new DataTable();

        SqlCommand cmd = new SqlCommand("SP_Inwardstock2_SEARCH_SELECT", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SearchItemName", SqlDbType.NVarChar).Value = ddlSearchBy.SelectedValue;
        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = (txtSearchText.Text == "") ? "0" : txtSearchText.Text;

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dt);

        gvBind();
        
    }

    protected void gvBind()
    {
        gvIS2.DataSource = dt;
        gvIS2.DataBind();
        
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvSetBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        updateDT();

    }

    private void updateDT()
    {
        foreach (GridViewRow gvr in gvIS2.Rows)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["rid"].ToString() == ((HiddenField)gvr.FindControl("HFrid1")).Value)
                {
                    row.SetField("NewQty", ((TextBox)gvr.FindControl("txtnewqty")).Text);
                }
            }

        }
    }
    protected void btnCreateMRN1_Click(object sender, EventArgs e)
    {
        updateDT();

        DataTable dt2 = new DataTable();
        dt2 = dt.Clone();

        foreach (GridViewRow gvr in gvIS2.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chkselect")).Checked)
            {
                string rid = ((HiddenField)gvr.FindControl("HFrid1")).Value;

                foreach (DataRow MyDataRow in dt.Select("rid = " + rid))
                {
                    //dt2.Rows.Add(MyDataRow);
                    dt2.ImportRow(MyDataRow);
                }
            }
        }

        Session["vl_TempInward"] = dt2;
        //dt2 = (DataTable)Session["ss"];
        Response.Redirect("~/Modules/SCM/CheckingFormatDetails2.aspx");
    }
}
 
