using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;

public partial class Modules_HR_Tour_ExpansesMast : basePage 
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            //HR.EmployeeMaster.EmployeeMaster_Select_Compalint(ddlPreparedBy);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
            objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
            lblUserType.Text = objmas.EmpTypeID;
            setControlsVisibility();
            BindGrid_All();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "87");
        btnAdd.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvConvenienceVoucher.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvConvenienceVoucher.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid_All();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvConvenienceVoucher.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
    }
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_Tour_Expanses]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblUserType.Text != "")
        {
            cmd.Parameters.AddWithValue("@userType", lblUserType.Text);

        }
        if (lblEmpIdHidden.Text != "")
        {
            cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);

        }

        if (txtEmployeeName.Text != "")
        {
            cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmployeeName.Text);
        }
        if (txtVoucher.Text != "")
        {
            cmd.Parameters.AddWithValue("@Voucher_No", txtVoucher.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvConvenienceVoucher.DataSource = dt;
        gvConvenienceVoucher.DataBind();
    }
    protected void lbtnVoucherNo_Click(object sender, EventArgs e)
    {
        //btnSave.Visible = false;
        btnEdit.Visible = true;
        LinkButton lbtnVoucherNo;
        lbtnVoucherNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnVoucherNo.Parent.Parent;
        gvConvenienceVoucher.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        Response.Redirect("Tour_ExpansesDet.aspx?TourId=" + gvConvenienceVoucher.SelectedRow.Cells[0].Text);


    }
    protected void gvConvenienceVoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConvenienceVoucher.PageIndex = e.NewPageIndex;
        BindGrid_All();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvConvenienceVoucher.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Tour&sdid=" + gvConvenienceVoucher.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvConvenienceVoucher.SelectedIndex > -1)
        {
            //HR.Tour_Expanses objTour = new HR.Tour_Expanses();
            //if (objTour.TourExpanses_Select(gvConvenienceVoucher.SelectedRow.Cells[0].Text) > 0)
            //{

            //}
            LinkButton lbtnVoucherNo = (LinkButton)sender;
            GridViewRow Row = (GridViewRow)lbtnVoucherNo.Parent.Parent;
        gvConvenienceVoucher.SelectedIndex = Row.RowIndex;
        Response.Redirect("Tour_ExpansesDet.aspx?TourId=" + gvConvenienceVoucher.SelectedRow.Cells[0].Text);

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tour_ExpansesDet.aspx");
    }
}