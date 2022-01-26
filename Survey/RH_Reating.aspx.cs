using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;

using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using YantraBLL.Modules;

public partial class Demo2 : System.Web.UI.Page
{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //HR.EmployeeMaster.EmployeeMaster_SelectSales12(ddlPreparedBy);
        }
    }

    
   
    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
        btnsubmit1.Enabled = false;
         try
         {
             
             string star1 = Request.Form["star"];
             string rdbvisit = Request.Form["rdbvisit"];
             string rdbShowroom = Request.Form["rdbShowroom"];
             string chktest = Request.Form["chktest"];
             if (star1 == "1" || star1 == "2" )
             {
                 chktest = "Not agrred";
             }
             else { chktest = "Yes agreed"; }

             string LoogingFor = "";
             string Behave = "";
             if (rdbLooking.Checked == true)
             {
                 LoogingFor = "Yes";
             }
             else if (rdbNo.Checked == true)
             {
                 LoogingFor = "No";
                 

             }
             else if (rdbybph.Checked == true)
             {
                 LoogingFor = "Yes, but price is high";
             }
             if (rdbBehaveBad.Checked == true) { Behave = "Bad"; }
             else { Behave = "Good"; }
             string SurveyId = lblSurveyId.Text = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            // int i = SaveServiceRequest(SurveyId, DateTime.Now, LoogingFor, txtNo.Value, chktest, Behave, txtBad.Value, rdbKnowledge.SelectedItem.Value, rdbShowroom, rdbvisit, star1, txtComments.Value, txtCustName.Text, txtCustEmail.Text, ddlPreparedBy.SelectedItem.Text, txtCustMobile.Text);
            //if (i > 0)
            //{
            //    SendMsgToAdmin();

            //    //MessageBox.Show(this, "Thank you");
            //}
                
        }
        catch (Exception ex)
        {

        }
         finally
         {
             btnCancel_Click(sender, e);
             btnsubmit1.Enabled = true;
         }
    }
    string pagenavigationstr, htmlstr;
    private void SendMsgToAdmin()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "hi()", true);
        HR.SendSMS objsms = new HR.SendSMS();
        pagenavigationstr = "http://183.82.108.55/Feedback/CustRating.aspx?KYCID=" + lblSurveyId.Text + "";
        //string msfEmp = "Dear Sir, New feedback form has been Submitted by " + txtCustName.Text + ", attended By - "+ddlPreparedBy.SelectedItem .Text +". To view Feedback form please click here " + pagenavigationstr;
        string MD_MNo = "8008103060";
        //objsms.Send_App_SMS(msfEmp, MD_MNo);
    }
    private int SaveServiceRequest(string id, DateTime date_Created,string LookingFor, string No,string YesPriceHigh, string Behav, string Badtxt, string Knowl, string Showwroomlook, string VisitAgain, string Recommend, string Comments, string CustName, string Email, string PreparedBy, string MobileNo)
    {
        SqlCommand cmd = new SqlCommand();
        int i = 0;
        try
        {
            con.Close();
            string instr = "insert into Showroom_Survey_tbl values( " + "'" + id + "'," + "'" + date_Created + "'," + "'" + LookingFor + "'," + "'" + No + "'," + "'" + YesPriceHigh + "'," + "'" + Behav + "'," + "'" + Badtxt + "'," + "'" + Knowl + "'," + "'" + Showwroomlook + "'," + "'" + VisitAgain + "'," + "'" + Recommend + "'," + "'" + Comments + "'," + "'" + CustName + "'," + "'" + Email + "'," + "'" + PreparedBy + "'," + "'" + MobileNo + "'" + ")";
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
            
        }
        return i;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtCustName.Text = txtComments.Value  = txtBad.Value = txtYbprh.Text = txtCustMobile.Text =txtCustEmail.Text =txtNo.Value  ="";
        //rdbshowamb.ClearSelection();
        //rdbVisit.ClearSelection();
        rdbKnowledge.ClearSelection();
        rdbBehave.Checked = false;
        rdbBehaveBad.Checked = false;
        rdbLooking.Checked = false;
        rdbNo.Checked = false;
        
        
    }
    
}