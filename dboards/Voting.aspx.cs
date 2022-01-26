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

public partial class dboards_Voting : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {
        DataSet ds = new DataSet();

        con.Open();

        string cmdstr = "select EMP_FIRST_NAME +' '+EMP_LAST_NAME as EmpName,EMP_PHOTO ,DEPT_NAME ,DESG_NAME,YANTRA_EMPLOYEE_MAST.EMP_ID,convert(nvarchar(50),YANTRA_EMPLOYEE_DET.EMP_DET_DOJ,103) as DOJ from YANTRA_EMPLOYEE_MAST  inner join YANTRA_EMPLOYEE_DET on YANTRA_EMPLOYEE_MAST .EMP_ID =YANTRA_EMPLOYEE_DET .EMP_ID inner join YANTRA_DEPT_MAST on YANTRA_EMPLOYEE_DET .DEPT_ID =YANTRA_DEPT_MAST .DEPT_ID inner join YANTRA_DESG_MAST on YANTRA_EMPLOYEE_DET .DESG_ID =YANTRA_DESG_MAST .DESG_ID where EMP_DET_DOT ='2020-12-31 00:00:00.000' and STATUS !=0 order by YANTRA_EMPLOYEE_DET.DEPT_ID asc";

        SqlCommand cmd = new SqlCommand(cmdstr, con);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        adp.Fill(ds);

        DataList1.DataSource = ds.Tables[0];

        DataList1.DataBind();       
    }
    
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Vote")
            {

                string tck_ID = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string Dt_added = DateTime.Now.ToString();
                string Month = DateTime.Now.Month.ToString();
                string Year = DateTime.Now.Year.ToString();
                string Voted_by = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                string EmpId = e.CommandArgument.ToString();
                int i = SaveServiceRequest1(tck_ID, ddlVoteType.SelectedItem.Text, EmpId, Month, Year, Dt_added, Voted_by);
                if (i > 0)
                {
                    //btnVote.Enabled = false;
                    MessageBox.Show(this, "Successfully voted");
                }
            }
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Unable to Vote the request, please try again or contact Admin.");
        }
    }
    private int SaveServiceRequest1(string id, string VoteType, string EmpId, string Month, string Year, string Dt_added, string Voted_by)
    {
        SqlCommand cmd = new SqlCommand();
        int i = 0;
        try
        {
            con.Close();
            string instr = "insert into emp_voting_tbl values( " + "'" + id + "'," + "'" + VoteType + "'," + "'" + EmpId + "'," + "'" + Month + "'," + "'" + Year + "'," + "'" + Dt_added + "'," + "'" + Voted_by + ")";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }
        catch (Exception ex)
        {
            i = 0;
        }
        finally
        {
            con.Close();
            //gvServiceRequests.DataBind();
            //lblTotalTicketsRaised.Text = gvServiceRequests.Rows.Count.ToString();

        }
        return i;
    }
}