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

public partial class Feedback_SrvRating : System.Web.UI.Page
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
                        //txtYPricehigh.Text = obj.txtybph;
                        rdb2.SelectedValue = obj.ExeBehav;
                        rdb2_SelectedIndexChanged(sender, e);
                        txtbad.Text = obj.ExeBad;
                        rdb3.SelectedValue = obj.ExeKnow;
                        rdb4.SelectedValue = obj.ShowroomLook;
                        rdb5.SelectedValue = obj.VisitAgain;
                        rdb6.SelectedValue = obj.Rating;
                        lblCustName.Text = obj.CustFullName;
                        //txtCustMobile.Text = obj.CustMobile;
                        //txtCustEmail.Text = obj.Emailid;
                        txtcomments.Text = obj.AnyComments;
                        lblDt.Text = obj.SurveyDt;
                        lblExeName.Text = obj.ExeName;
                    }

                }
                catch (Exception ex) { }
            }
        }
    }
    protected void rdblIndentfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblIndentfor.SelectedItem.Text == "Yes")
        {
            lblNoText.Visible = false;
            txtNo.Visible = false;
            //lblYPricehigh.Visible = false;
            //txtYPricehigh.Visible = false;
        }
        else if (rdblIndentfor.SelectedItem.Text == "No")
        {
            lblNoText.Visible = true;
            txtNo.Visible = true;
            //lblYPricehigh.Visible = false;
            //txtYPricehigh.Visible = false;
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
}