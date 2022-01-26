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

public partial class Feedback_KYC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["KYCID"] != null)
            {
                try
                {
                    SM.CustomerMaster obj = new SM.CustomerMaster();
                    if (obj.Feedback_Select(Request.QueryString["KYCID"].ToString()) > 0)
                    {
                        rdblIndentfor.SelectedValue = obj.FindLookingFor;
                        rdblIndentfor_SelectedIndexChanged(sender, e);
                        txtNo.Text = obj.txtLookingFor;
                        txtYPricehigh.Text = obj.txtybph;
                        rdb2.SelectedValue = obj.ExeBehav;
                        rdb2_SelectedIndexChanged(sender, e);
                        txtbad.Text = obj.ExeBad;
                        rdb3.SelectedValue = obj.ExeKnow;
                        rdb4.SelectedValue = obj.ShowroomLook;
                        rdb5.SelectedValue = obj.VisitAgain;
                        rdb6.SelectedValue = obj.Rating;
                        txtCustName.Text = obj.CustFullName;
                        txtCustMobile.Text = obj.CustMobile;
                        txtCustEmail.Text = obj.Emailid;
                        txtcomments.Text = obj.AnyComments;
                        lblDt.Text = obj.SurveyDt;
                    }

                }
                catch (Exception ex) { }
            }
        }
    }
    protected void rdblIndentfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblIndentfor.SelectedItem .Text == "Yes")
        {
            lblNoText.Visible = false;
            txtNo.Visible = false;
            lblYPricehigh.Visible = false;
            txtYPricehigh.Visible = false;
        }
        else if (rdblIndentfor.SelectedItem.Text == "No")
        {
            lblNoText.Visible = true;
            txtNo.Visible = true;
            lblYPricehigh.Visible = false;
            txtYPricehigh.Visible = false;
        }
        else if (rdblIndentfor.SelectedItem.Text == "Yes, but price is high") 
        {
            lblNoText.Visible = false;
            txtNo.Visible = false;
            lblYPricehigh.Visible = true;
            txtYPricehigh.Visible = true;
        }
    }
    protected void rdb2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdb2.SelectedItem.Text == "Good")
        {
            lblBad.Visible = false;
            txtbad.Visible = false;
        }
        else if (rdb2.SelectedItem.Text == "Bad")
        {
            lblBad.Visible = true;
            txtbad.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SurveyId = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            int i = SaveServiceRequest(SurveyId, DateTime.Now, rdblIndentfor.SelectedItem.Text, txtNo.Text, txtYPricehigh.Text, rdb2.SelectedItem.Text, txtbad.Text, rdb3.SelectedItem.Value, rdb4.SelectedItem.Value, rdb5.SelectedItem.Value, rdb6.SelectedItem.Value, txtcomments.Text, txtCustName.Text, txtCustEmail.Text, txtCustEmail.Text, txtCustMobile.Text);
            if (i > 0)
            {
                //SendMsgToAdmin();
            }
                
        }
        catch (Exception ex)
        {

        }
    }
    private int SaveServiceRequest(string id, DateTime date_Created,string LookingFor, string No,string YesPriceHigh, string Behav, string Badtxt, string Knowl, string Showwroomlook, string VisitAgain, string Recommend, string Comments, string CustName, string Email, string PreparedBy, string MobileNo)
    {
        SqlCommand cmd = new SqlCommand();
        int i = 0;
        try
        {
            con.Close();
            string instr = "insert into Service_Requests_tbl values( " + "'" + id + "'," + "'" + date_Created + "'," + "'" + LookingFor + "'," + "'" + No + "'," + "'" + YesPriceHigh + "'," + "'" + Behav + "'," + "'" + Badtxt + "'," + "'" + Knowl + "'," + "'" + Showwroomlook + "'," + "'" + VisitAgain + "'," + "'" + Recommend + "'," + "'" + Comments + "'," + "'" + CustName + "'," + "'" + Email + "'," + "'" + PreparedBy + "'" +  "'," + "'" + MobileNo  + "'" +")";
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

}