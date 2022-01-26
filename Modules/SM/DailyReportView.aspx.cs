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
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
using System.Data.SqlClient;
using YantraDAL;
using System.IO;

public partial class Modules_SM_DailyReportView : basePage 
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    public bool AdminGridRights;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        if (!IsPostBack)
        {
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.UserId);
            lblDeptId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesPerson);
            setControlsVisibility();

            BindGrid_All();

            BindRdblist();
        }
    }
    private void setControlsVisibility()
    {
        Jamilib.User_Permissions up = new Jamilib.User_Permissions(Session["vl_userid"].ToString(), "13");
        //btnExprot.Enabled = up.Email;
        lblUserType.Enabled  = up.Email ;
        if (up.AdminGridRights  == true)
        {
            lblUserType.Text = "0";
        }
      
    }


    private void BindRdblist()
    {

        DataSet ds = new DataSet();

        string cmdstr = "select COUNT(*) as count,DR_Followup  from YANTRA_DAILY_REPORT where DR_Followup ='DailyReport' group by DR_Followup  union all select COUNT(*),DR_Followup from YANTRA_DAILY_REPORT where DR_Followup ='Follow Up' group by DR_Followup  union all select COUNT(*),DR_Followup  from YANTRA_DAILY_REPORT where DR_Followup ='Comment' group by DR_Followup ";

        SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

        adp.Fill(ds);

        rdblist.DataSource = ds;

        rdblist.DataTextField = "count";

        rdblist.DataValueField = "count";

        rdblist.DataBind();

    } 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid_All();
    }

    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_DailyReportSearch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblUserType.Text != "")
        {
            cmd.Parameters.AddWithValue("@userType", lblUserType.Text);

        }
        if (lblEmpIdHidden.Text != "")
        {
            cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);

        }
        if (txtClientName.Text != "")
        {
            cmd.Parameters.AddWithValue("@CLIENTSNAME", txtClientName.Text);
        }
        //if (txtEmp_Name.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmp_Name.Text);
        //}
        if (ddlSalesPerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", ddlSalesPerson.SelectedItem.Value);
        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (lblDeptId.Text != "")
        {
            cmd.Parameters.AddWithValue("@DeptId", lblDeptId.Text);
        }

        if (ddlAction.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Action", ddlAction.SelectedItem.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDrs.DataSource = dt;
        gvDrs.DataBind();
    }
    protected void gvDrs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDrs.PageIndex = e.NewPageIndex;
        BindGrid_All();

    }

    protected void gvDrs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[0].Visible = false;

            e.Row.Cells[3].Visible = false;
            //e.Row.Cells[6].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;


            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;

            e.Row.Cells[10].Visible = false;
            if (lblUserType.Text == "0")
            {
                e.Row.Cells[11].Visible = true ;

            }
            else
            {
                e.Row.Cells[11].Visible = false;

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[8].Text == "Comment")
            {
                e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
            }
            DateTime myDate = (DateTime)DataBinder.Eval(e.Row.DataItem, "DATETIME");

            DateTime Createdon = (DateTime)DataBinder.Eval(e.Row.DataItem, "createdOn");
            //
            //{
            //    if (DateTime.Now.Subtract(myDate).TotalHours > 24)
            //    {


            //        //e.Row.ForeColor = System.Drawing.Color.Red;

            //        HyperLink btnEdit = (HyperLink)e.Row.FindControl("btnedit1");
            //        btnEdit.Visible = false;
            //    }
            //}
            //else
            //{
            //    if (DateTime.Now.Subtract(Createdon).TotalHours > 24)
            //    {


            //        //e.Row.ForeColor = System.Drawing.Color.Red;

            //        HyperLink btnEdit = (HyperLink)e.Row.FindControl("btnedit1");
            //        btnEdit.Visible = false;
            //    }
            //}

            if (DateTime.Now.Subtract(Createdon).TotalHours > 24)
            {


                //e.Row.ForeColor = System.Drawing.Color.Red;

                HyperLink btnEdit = (HyperLink)e.Row.FindControl("btnedit1");
                btnEdit.Visible = false;
            }
        }
    }
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{

    //}
   
}