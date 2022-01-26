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
            
        }
    }

    
   
    protected void btnsubmit1_Click(object sender, EventArgs e)
    {
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
            string SurveyId = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            int i = SaveServiceRequest(SurveyId, DateTime.Now, LoogingFor, txtNo.Value, chktest, Behave, txtBad.Value, rdbKnowledge.SelectedItem.Value, rdbShowroom, rdbvisit, star1, txtComments.Value, txtCustName.Text, txtCustEmail.Text, txtCustName.Text, txtCustMobile.Text);
            if (i > 0)
            {
                SendMsgToAdmin();
                btnCancel_Click(sender, e);

            }
                
        }
        catch (Exception ex)
        {

        }

    }
    private void SendMsgToAdmin()
    {
        MessageBox.Show(this, "Thank You");
        HR.SendSMS objsms = new HR.SendSMS();
        string msfEmp = "Dear Sir, New feedback form has been Submitted by the Client Mr/Ms." + txtCustName.Text + ". To view details, Please click here";
        string MD_MNo = "9177700473";
        objsms.Send_App_SMS(msfEmp, MD_MNo);
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
        txtCustName.Text = txtComments.Value  = txtBad.Value = txtYbprh.Text = txtCustMobile.Text =txtCustEmail.Text =txtNo.Value  ="";
        //rdbshowamb.ClearSelection();
        //rdbVisit.ClearSelection();
        rdbKnowledge.ClearSelection();
        rdbBehave.Checked = false;
        rdbBehaveBad.Checked = false;
        rdbLooking.Checked = false;
        rdbNo.Checked = false;
        
        
    }
    
}