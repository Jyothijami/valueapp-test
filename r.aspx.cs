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

public partial class Rating : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["CRID"] != null)
        {
            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
            if (objComplaintRegister.CompRegister_Select(Request.QueryString["CRID"]) > 0)
            {

            }
        }
        rdbbehaviour.CssClass = "container";
    }
    protected void rdbbehaviour_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbbehaviour.SelectedItem.Text == "Good")
        {
            lblBad.Visible = false;
            txtbad.Visible = false;
        }
        else if (rdbbehaviour.SelectedItem.Text == "Poor")
        {
            lblBad.Visible = true;
            txtbad.Visible = true;
        }
        else if (rdbbehaviour.SelectedItem.Text == "Average")
        {
            lblBad.Visible = false;
            txtbad.Visible = false;
        }
       
    }
   
    //protected void rdbsp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdbsp.SelectedItem.Text == "Yes")
    //    {
    //        TextBox1.Visible = true;

    //    }
    //    else
    //    {
    //        TextBox1.Visible = false;
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblCRID.Text = Request.QueryString["CRID"];
            string tck_ID = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            int i = SaveServiceRequest1(tck_ID, DateTime.Now, lblCRID.Text, rdbRating.SelectedItem.Value, txt1.Text, rdbbehaviour.SelectedItem.Text, txtbad.Text, rdbknwldg.SelectedItem.Text, rdbrecommend.SelectedItem.Text, txtbad.Text, rdbrecommend.SelectedItem.Text, TextBox2.Text);
            if (i > 0)
            {
                btnCancel_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Thank You For Your Valueable Feedback ");
        }
    }
    private int SaveServiceRequest1(string Id, DateTime Date, string CR_ID, string satisfiedbtn, string Sugg, string behaviour, string badreason, string knwldge, string sp, string spdetails, string recommend, string comments)
    {
        SqlCommand cmd = new SqlCommand();
        int i = 0;
        try
        {
            con.Close();
            string instr = "Insert into YANTRA_COMPLAINT_RATING VALUES (" + "'" + Id + "',"+"'"+DateTime .Now.ToString ()+ "',"+ "'"+CR_ID+"',"+"'"+satisfiedbtn +"',"+"'"+Sugg+"',"+"'"+behaviour+"',"+"'"+badreason +"',"+"'"+knwldge +"',"+"'"+sp+"',"+"'"+spdetails +"',"+"'"+recommend+ "',"+"'"+comments+ "'" +")";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            i = 0;
        }
         return i;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rdbrecommend.ClearSelection();
        rdbknwldg.ClearSelection();
        rdbrecommend.ClearSelection();
        rdbRating.ClearSelection();
        rdbbehaviour.ClearSelection();
        txt1.Visible = false;
        lbl1.Visible = false;
        txtbad.Visible = false;
        lblBad.Visible = false;
        txtbad.Text = "";
        txt1.Text = "";
    }
    protected void rdbRating_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbRating.SelectedItem.Value == "Very Dissatisfied")
        {
            lbl1.Visible = true;
            txt1.Visible = true;
        }
        else if (rdbRating.SelectedItem.Value == "Dissatisfied")
        {
            lbl1.Visible = true;
            txt1.Visible = true;
        }
        else if (rdbRating.SelectedItem.Value == "Neither Satisified nor Dissatisfied")
        {
            lbl1.Visible = true;
            txt1.Visible = true;
        }
        else if (rdbRating.SelectedItem.Value == "Satisfied")
        {
            lbl1.Visible = false;
            txt1.Visible = false;
        }
        else if (rdbRating.SelectedItem.Value == "Very Satisfied")
        {
            lbl1.Visible = false;
            txt1.Visible = false;
        }
    }
}