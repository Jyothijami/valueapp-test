using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Home_notifications_Def : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EmpDOB_Fill();
        DateTime Cdt = DateTime.Now;
        //Run after 11AM.
        if (Cdt.Hour == 11 && Cdt.Minute <= 05)
        {
            EmployeeDOBReminder();
        }
    }
    protected void EmpDOB_Fill()
    {
        HR.EmployeeMaster obj = new HR.EmployeeMaster();
        obj.EmpDOBReminder_Select(gvEmpBdyReminder);
    }
    protected void EmployeeDOBReminder()
    {
        HR.SendSMS objsms = new HR.SendSMS();

        foreach (GridViewRow row in gvEmpBdyReminder.Rows)
        {
            Label lblEmpId = (Label)row.FindControl("Emp_Id");
            Label EmpName = (Label)row.FindControl("EMP_Name");
            Label DOB = (Label)row.FindControl("EMP_DOB");

            lblHRMobileNo.Text = "8008103080";
            string msgHR = "Reminder - " + EmpName.Text + " Birthday is Today. Please Convey the Best wishes from VALUELINE.";
            objsms.Send_App_SMS(msgHR, lblHRMobileNo.Text);
        }
        //string DOB = General.toDDMMYYYY (lblIsReadEmpDOB.Text).ToString();
        //int FMonth = DOB.Month;
        //int CMonth = DateTime.Now.Month;
        //if (FMonth == CMonth)
        //{

        //}

        gvEmpBdyReminder.DataBind();

    }
    protected string short_Description(string desc)
    {
        string sdesc = "";

        sdesc = dbc.getLimitedWords(desc, 20);

        return sdesc;
    }
    
    protected string getColor(string isread)
    {
        if (isread.Equals("True"))
        {
            return "readcls";
        }
        else
        {
            return "unreadcls";
        }
    }
}
 
