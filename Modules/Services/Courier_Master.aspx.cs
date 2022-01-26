using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using vllib;
public partial class Modules_Services_Courier_Master : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindGridSearch();
            setControlsVisibility();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteDailyReport();
        BindGridSearch();

    }

    private void DeleteDailyReport()
    {
        #region Delete Application
        foreach (GridViewRow gvr in gvCourierDetails.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label onDutyId = (Label)gvr.FindControl("lblId");
                    int ID = Convert.ToInt32(onDutyId.Text);
                    SqlCommand cmd = new SqlCommand("[USP_Delete_Courier_Master]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "43");
        btnSave.Enabled = up.add;
        btnDelete.Enabled = up.Delete;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveCourierDetails();
        ClearAllFields();
        BindGridSearch();

    }

    private void SaveCourierDetails()
    {
        Services.ServiceCourierInfo obj = new Services.ServiceCourierInfo();
        obj.Courier_CompanyName = txtCourierCompName.Text;
        obj.Courier_DocketNo = txtDocNo.Text;
        obj.Courier_From = txtFrom.Text;
        obj.Courier_ReceivedBy = txtReceivedBy.Text;
        obj.Courier_To = txtTo.Text;
        obj.Courier_Type = ddlCourierType.SelectedItem.Text;
        obj.Date = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
        obj.Remarks = txtRemarks.Text;

        MessageBox.Show(this, obj.Courier_Save());
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }

    private void ClearAllFields()
    {
        txtCourierCompName.Text = txtDate.Text = txtDocNo.Text = txtFrom.Text = txtReceivedBy.Text = txtRemarks.Text = txtTo.Text = "";
        ddlCourierType.SelectedIndex = 0;
        
    }
    protected void ddlCourierType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCourierType.SelectedIndex == 1)
        {
            lblDate.Text = "Received Date :";
            lblFrom.Text = "From :";
            lblTo.Text = "To :";
            lblReceivedBy.Text = "Received By :";

        }
        else if (ddlCourierType.SelectedIndex == 2)
        {
            lblDate.Text = "Sent Date :";
            lblFrom.Text = "From :";
            lblTo.Text = "To :";
            lblReceivedBy.Text = "Handeled By :";

        }

    }

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Recieved Date")
        {
            ddlSymbols.Visible = true;
            imgToDate.Visible = true;
            ceSearchValueToDate.Enabled = true;
            MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            imgToDate.Visible = false;
            ceSearchValueToDate.Enabled = false;
            MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
    }
    #endregion
    #region ddlSymbols_SelectedIndexChanged
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            imgFromDate.Visible = true;
            ceSearchFrom.Enabled = true;
            MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            imgFromDate.Visible = false;
            ceSearchFrom.Enabled = false;
            MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion   
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvCourierDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvCourierDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        BindGridSearch();
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridSearch();
    }

    private void BindGridSearch()
    {
        SqlCommand cmd = new SqlCommand("USP_Courier_Search_Dyn", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlType.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Text);
        }
        if (txtCurFrom.Text != "")
        {
            cmd.Parameters.AddWithValue("@CourierFrom", txtCurFrom.Text);
        }
        if (txtCurTo.Text != "")
        {
            cmd.Parameters.AddWithValue("@CourierTo", txtCurTo.Text);
        }

        if (txtCompanyName.Text != "")
        {
            cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
        }

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvCourierDetails.DataSource = dt;
        gvCourierDetails.DataBind();
    }

    
    protected void gvCourierDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCourierDetails.PageIndex = e.NewPageIndex;
        BindGridSearch();
        
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

    }
}
 
