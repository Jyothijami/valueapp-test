using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_EmpReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            gvEmployeeMaster.DataBind();
            
        }

       
    }
    protected void lbtnEmpFirstName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnEnqNo;
        lbtnEnqNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnEnqNo.Parent.Parent;
        gvEmployeeMaster.SelectedIndex = gvRow.RowIndex;
    }


   
    protected void gvEmployeeMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {








    }
}