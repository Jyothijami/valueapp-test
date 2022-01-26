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

public partial class Modules_SM_ToDoList : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            EmployeeMaster_Fill1();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblDeptId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
            //BindDailyReportDtls();
            BindGrid_All();
            setControlsVisibility();
        }
    }
    #region Employee Master Fill
    private void EmployeeMaster_Fill1()
    {
        try
        {

            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesPerson);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //HR.Dispose();
        }
    }
    #endregion
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "13");
        btnDelete.Enabled = up.Delete;
        //btnSave.Enabled = up.Save;
        //btnRefresh.Enabled = up.Refresh;

    }
    private void BindData()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString()))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT USER_NAME,USER_ID FROM YANTRA_USER_DETAILS where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]";
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                // BIND THE SELECT DROP-DOWN LIST WITH A DATABASE TABLE.
                Books.DataSource = dt;
                Books.DataTextField = "USER_NAME";
                Books.DataValueField = "USER_ID";

                Books.DataBind();

                // FOR MULTIPLE SELECTION. SET THE VALUE AS FALSE, AND SEE WHAT HAPPENS.
                Books.Multiple = true;
            }
        }


        // SM.CustomerMaster.CustomerMaster_Select(Books);


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        objdr.Date = DateTime.Now.ToString();
        objdr.Subject = txtSubject.Text;
        objdr.IssuedDate = Yantra.Classes.General.toMMDDYYYY(txtDateTime.Text);
        objdr.Description = txtDescr.Text;
        objdr.Status = ddlActivity.SelectedItem.Text;
        objdr.PreparedBy = lblEmpIdHidden.Text;

        if (objdr.ToDo_List_Save() == "Data Saved Successfully")
        {
            //foreach (var hai in Books.DataTextField )
            //{
            //    objdr.EmpID = hai.ToString();
            //    objdr.ToDO_List_Det_Save();
            //}
            for (int i = 0; i <= Books.Items.Count - 1; i++)
            {
                var selectedText = Books.Items[Books.SelectedIndex].Value.Trim();
                objdr.ToDO_List_Det_Save();
                //MessageBox.Show(this, selectedText);
            }
        }
        MessageBox.Show(this, "Data Saved Suucessfully");
    }
    protected void gvDrs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDrs.PageIndex = e.NewPageIndex;
        BindGrid_All();
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Visible = true;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid_All();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteDailyReport();
        BindGrid_All();
    }
    private void DeleteDailyReport()
    {
        #region Delete Application
        foreach (GridViewRow gvr in gvDrs.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label onDutyId = (Label)gvr.FindControl("lblId");
                    int ID = Convert.ToInt32(onDutyId.Text);
                    SqlCommand cmd = new SqlCommand("USP_Delete_DailyReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DailyReport_ID", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_ToDOListSearch]", con);
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
        if (lblDeptHead.Text != "")
        {
            //DeptHead_Check();
            cmd.Parameters.AddWithValue("@DeptHead", lblDeptHeadId.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDrs.DataSource = dt;
        gvDrs.DataBind();
    }
}