using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;
using System.Data;


public partial class Modules_Reports_DailyReport : basePage
{
    string pagenavigationstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblUserId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.UserId );

            lblEmpId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblDeptId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
            General obj = new General();
            string sql = @"select DEPT_HEAD  from YANTRA_DEPT_MAST where DEPT_ID =" + lblDeptId.Text;
            DataTable dttemp4 = obj.ReturnDataTable(sql);
            lblDeptHead.Text  = dttemp4.Rows[0]["DEPT_HEAD"].ToString();

            //Department_Fill();
            //EmployeeNames_Fill();


            deptfill();
            empfile();

        }
       

    }

    private void empfile()
    {
        //if (lblUserType.Text == "0")
        //{
        //    HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpDaily);
        //}
        //else
        //{
        //    HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpDaily);
        //    ddlEmpDaily.SelectedValue = lblEmpId.Text;
        //    ddlEmpDaily.Enabled = false;
        //}

        HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpDaily);
        ddlEmpDaily.SelectedValue = lblEmpId.Text;
        ddlEmpDaily.Enabled = false;

         if(lblEmpId.Text == lblDeptHead.Text)
        {
            Masters.Department.DepartmentHead_Select(ddlDepDaily, lblDeptHead.Text);
            ddlDepDaily.SelectedValue = lblDeptId.Text;
            ddlDepDaily_SelectedIndexChanged(new object(), new System.EventArgs());
            ddlDepDaily.Enabled = true;
            ddlEmpDaily.Enabled = true;

        }


        if(lblEmpId.Text == "50" ||  lblEmpId.Text == "314")
        {
            Masters.Department.Department_Select(ddlDepDaily);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpDaily);
            ddlDepDaily.Enabled = true;
            ddlEmpDaily.Enabled = true;
        }

       
    }

    private void deptfill()
    {

       // if (lblUserType.Text == "0")
       // {
       //     Masters.Department.Department_Select(ddlDepDaily);
       // }
       //else
       // {
       //     Masters.Department.Department_Select(ddlDepDaily);
       //     ddlDepDaily.SelectedValue = lblDeptId.Text;
       //     ddlDepDaily.Enabled = false;
       // }

        Masters.Department.Department_Select(ddlDepDaily);
        ddlDepDaily.SelectedValue = lblDeptId.Text;
        ddlDepDaily.Enabled = false;
    }













    private void EmployeeNames_Fill()
    {
        if (lblUserType.Text == "0")
        {
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmpDaily);
            ddlEmpDaily.Items.FindByText("--").Text = "All";
        }
        //else if (lblUserType .Text =="1" || lblUserId .Text == lblDeptHead .Text )
        //{
        //    HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmpDaily);
        //    ddlEmpDaily.Items.FindByText("--").Text = "All";
        //    //ddlEmpDaily.SelectedValue = lblEmpId.Text;
        //    //ddlEmpDaily.Enabled = false;
        //}
        else
        {
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmpDaily);
            ddlEmpDaily.Items.FindByText("--").Text = "All";
            ddlEmpDaily.SelectedValue = lblEmpId.Text;
            ddlEmpDaily.Enabled = false;
        }
    }
    public void Department_Fill()
    {
        try
        {
            if (lblUserType.Text == "0")
            {
                Masters.Department.Department_Select(ddlDepDaily);
                ddlDepDaily.Items.FindByText("--").Text = "All";
            }
            //else if (lblUserType.Text == "1" || lblUserId.Text == lblDeptHead.Text)
            //{
            //    Masters.Department.Department_Select(ddlDepDaily);
            //    ddlDepDaily.Items.FindByText("--").Text = "All";
            //    ddlDepDaily.SelectedValue = lblDeptId.Text;
            //    ddlDepDaily_SelectedIndexChanged(new object(), new System.EventArgs());
            //    ddlDepDaily.Enabled = false;

            //}
            else
            {
                Masters.Department.Department_Select(ddlDepDaily);
                ddlDepDaily.Items.FindByText("--").Text = "All";
                ddlDepDaily.SelectedValue = lblDeptId.Text;
                //ddlDepDaily.Enabled = false;

                ddlDepDaily_SelectedIndexChanged(new object(), new System.EventArgs());


                ddlEmpDaily.SelectedValue = lblEmpId.Text;
                ddlEmpDaily.Enabled = false;
                



            }








        }
        catch (Exception ex)
        {

        }
    }
    protected void runDailyReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ToDoList&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDailyRep.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDailyRep.Text) + "&empid=" + ddlEmpDaily.SelectedValue + "&dep=" + ddlDepDaily.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void ddlDepDaily_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmpDaily, ddlDepDaily.SelectedValue);
           // ddlEmpDaily.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
}