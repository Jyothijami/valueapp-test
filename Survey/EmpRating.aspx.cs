using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_EmpRating : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

        }
    }
    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        btnsubmit1.Enabled = false;
        try
        {
            string starQ1 = Request.Form["star"];
            string starQ2 = Request.Form["star1"];
            string starQ3 = Request.Form["star2"];
            string SurveyId = lblSurveyId.Text = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

        }
        catch (Exception ex)
        {

        }
    }
}
