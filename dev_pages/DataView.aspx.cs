using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class dev_pages_DataView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            Response.Redirect("~/MobileLogin.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { 
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

        BindToDoList_All();
        }
    }
    private void BindToDoList_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_ToDOListSearch]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //if (lblUserType.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@userType", lblUserType.Text);

        //}
        if (lblEmpIdHidden.Text != "")
        {
            cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);

        }
        //if (txtListSubject.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@CLIENTSNAME", txtListSubject.Text);
        //}
        //if (txtEmp_Name.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmp_Name.Text);
        //}
        //if (ddlEmp.SelectedIndex != 0)
        //{
        //    cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", ddlEmp.SelectedItem.Value);
        //}
        //if (txtListFrom.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtListFrom.Text));
        //}
        //if (txtListTo.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtListTo.Text));
        //}
        //if (lblDeptId.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@DeptId", lblDeptId.Text);
        //}
        //if (lblDeptHead.Text != "")
        //{
        //    //DeptHead_Check();
        //    cmd.Parameters.AddWithValue("@DeptHead", lblDeptHeadId.Text);
        //}
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvList.DataSource = dt;
        gvList.DataBind();
        BindChildGV();

    }
    private void BindChildGV()
    {
        foreach (GridViewRow gvrow in gvList.Rows)
        {
            GridView gvDC = (GridView)(gvList.Rows[gvrow.RowIndex].Cells[8].FindControl("gvChild"));
            SqlCommand cmd = new SqlCommand("USP_ToDOList_ReportingTo_Search", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[8].Text != "")
            {
                cmd.Parameters.AddWithValue("@ID", gvrow.Cells[8].Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();
        }


    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hf = (HiddenField)e.Row.FindControl("cthf1");
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

            ddlStatus.SelectedValue = hf.Value;
        }

    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindToDoList_All();
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        //btnListDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
        //btnListDelete.Visible = true;
        btnListUpdate.Visible = true;
        lblItem.Visible = true;
    }
    protected void btnListUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SM.DailyReport objdr = new SM.DailyReport();
            foreach (GridViewRow gvrow in gvList.Rows)
            {
                DropDownList d1 = gvrow.FindControl("ddlStatus") as DropDownList;
                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    objdr.ID = gvrow.Cells[8].Text;
                    objdr.Status = d1.SelectedItem.Text;
                    objdr.ToDO_List_Status_Update();
                }
            }
            MessageBox.Show(this, "Data Saved Suucessfully");
            BindToDoList_All();
        }
        catch (Exception ex)
        {

        }
    }
}