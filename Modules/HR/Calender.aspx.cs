using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;

using DatumDAL;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Yantra.Classes;
using vllib;
public partial class Modules_HR_Calender : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    int holidayId, mn, yy, FDate, TDate, FYear, Tyear;    
    string Text;

    protected void Page_Load(object sender, EventArgs e)
    {
         if(!IsPostBack)
            {
                gvHolidayList.DataBind();
                BranchName_Fill();
                setControlsVisibility();
            }           
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "120");
        btnAddHoliday.Enabled = up.add;
        btnDelete.Enabled = up.Delete;
        //btnDelete.Enabled = up.Delete;
        //btnSave.Enabled = up.Save;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;

    }
    #region BranchName Fill
    public void BranchName_Fill()
    {
        try
        {
            //Masters.Branch.Branch_Select(ddlBranch);
             Masters.RegionalMaster.RegionalMaster_Select(ddlLocation);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }

    }
    #endregion
    private int GetMonthValue(string date)
    {

        DateTime datevalue = (Convert.ToDateTime(date.ToString()));

        //dy = datevalue.Day;
        mn = datevalue.Month;
        //yy = datevalue.Year;
        return mn;
    }

    private int GetYearValue(string date)
    {

        DateTime datevalue = (Convert.ToDateTime(date.ToString()));

        //dy = datevalue.Day;
        //mn = datevalue.Month;
        yy = datevalue.Year;
        return yy;
    }
    protected void btnAddHoliday_Click(object sender, EventArgs e)
    {
        if (btnAddHoliday.Text == "Add Holiday")
        {
            Text = "Add Holiday";
            RandomGenerator();
            AddHoliday();
            MessageBox.Show(this, "Data Added Successfully");
            ClearFields();
            gvHolidayList.DataBind();
            BranchName_Fill();

        }
        else if (btnAddHoliday.Text == "Update Holiday")
        {
            Text = "Update Holiday";            
            AddHoliday();
            MessageBox.Show(this, "Data Updated Successfully");
            ClearFields();
            btnAddHoliday.Text = "Add Holiday";
            btnDelete.Visible = false;
            gvHolidayList.DataBind();
            BranchName_Fill();

        }
        
    }
    private void ClearFields()
    {
        txtHoliday.Text = txtDay.Text = txtDateselTbox.Text =lblHolidayId.Text= "";
        ddlLocation.SelectedIndex = 0;
    }

    private void AddHoliday()
    {
        SqlCommand cmd = new SqlCommand("USP_SaveHoliday", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblHolidayId.Text == "")
        {
            cmd.Parameters.AddWithValue("@Holiday_Id", holidayId);
        }
        else
        {
            cmd.Parameters.AddWithValue("@Holiday_Id", lblHolidayId.Text);
        }
        cmd.Parameters.AddWithValue("@Location_Id", ddlLocation.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@Holiday_Desc",txtHoliday.Text);
        cmd.Parameters.AddWithValue("@Date_Of_Holiday", General.toMMDDYYYY(txtDateselTbox.Text));
        cmd.Parameters.AddWithValue("@Day",  General.toMMDDYYYY(txtDay.Text));
        cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Text);
            
        FDate = GetMonthValue(General.toMMDDYYYY(txtDateselTbox.Text));
        TDate = GetMonthValue(General.toMMDDYYYY(txtDay.Text));

        FYear = GetYearValue(General.toMMDDYYYY(txtDateselTbox.Text));
        Tyear = GetYearValue(General.toMMDDYYYY(txtDay.Text));
        TimeSpan diff = Convert.ToDateTime(General.toMMDDYYYY(txtDay.Text)) - Convert.ToDateTime(General.toMMDDYYYY(txtDateselTbox.Text));


        double NrOfDays = diff.TotalDays;
        double ttlDays = NrOfDays + 1;

        cmd.Parameters.AddWithValue("@From_No", FDate);
        cmd.Parameters.AddWithValue("@To_No", TDate);
        cmd.Parameters.AddWithValue("@Total_Days", ttlDays);

        cmd.Parameters.AddWithValue("@From_Year", FYear);
        cmd.Parameters.AddWithValue("@To_Year", Tyear);


        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

      
    }
    protected void RandomGenerator()
    {
        Random RandomGenerator = null;
        RandomGenerator = new Random();
        holidayId = RandomGenerator.Next(0001, 99999999);       

    }
    protected void lbtnHolidayId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbtnHolidayId = (LinkButton)sender;
            lblHolidayId.Text = lbtnHolidayId.Text;
            GridViewRow gvr = (GridViewRow)lbtnHolidayId.Parent.Parent;
            gvHolidayList.SelectedIndex = gvr.RowIndex;

            Label lblLocationId = (Label)gvr.FindControl("lblLocationId");
            Label lblHolidayDesc = (Label)gvr.FindControl("lblHolidayDesc");
            Label lblDateOfHoliday = (Label)gvr.FindControl("lblDateOfHoliday");
            Label lblDay = (Label)gvr.FindControl("lblDayg");
            Label lblLocation = (Label)gvr.FindControl("lblLocation");

            txtHoliday.Text = lblHolidayDesc.Text;
            txtDateselTbox.Text = lblDateOfHoliday.Text;
            txtDay.Text = lblDay.Text;

            int id = Convert.ToInt32(lblLocationId.Text);

            ddlLocation.SelectedItem.Text = lblLocation.Text;
            ddlLocation.SelectedItem.Value = id.ToString();

            //ddlLocation.SelectedIndex =Convert.ToInt32(lblLocationId.Text.ToString());
            btnAddHoliday.Text = "Update Holiday";
            btnDelete.Visible = true;
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string deleteId = lblHolidayId.Text;
            SqlCommand cmd = new SqlCommand("Delete from HolidayCalender_tbl where Holiday_Id=" + deleteId + " ", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
        catch(Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            ClearFields();
            gvHolidayList.DataBind();
            btnDelete.Visible = false;
            btnAddHoliday.Text = "Add Holiday";
            BranchName_Fill();

        }
    }

}
 
