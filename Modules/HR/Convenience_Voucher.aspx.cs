using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_HR_Convenience_Voucher : basePage
{
    decimal Total = 0;
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
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_Conv_Voucher_2", con);
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
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvConvenienceVoucher.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;
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
    protected void lbtnVoucherNo_Click(object sender, EventArgs e)
    {
        //btnSave.Visible = false;
        tblDetails.Visible = false;
        btnEdit.Visible = true;
        LinkButton lbtnVoucherNo;
        lbtnVoucherNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnVoucherNo.Parent.Parent;
        gvConvenienceVoucher.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        btnSave.Text = "Update";
        btnMainRefresh.Visible = false;

        
    }
    protected void gvConvenienceVoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvConvenienceVoucher.PageIndex = e.NewPageIndex;
        BindGrid_All();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = true;
        //btnMainRefresh_Click(sender, e);
        HR.ClearControls(this);
        txtVoucherNo.Text = HR.Convenience_Voucher.Convenience_Voucher_AutoGenCode();
        btnSave.Visible = true;
        btnMainRefresh.Visible = true;
        btnSave.Text = "Save";
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        HR.Convenience_Voucher objCV = new HR.Convenience_Voucher();
        tblDetails.Visible = true;

        if (objCV.Conv_Voucher_Select(gvConvenienceVoucher.SelectedRow.Cells[0].Text) > 0)
        {
            tblDetails.Visible = true;
            txtVoucherNo.Text = objCV.VoucherNo;
            txtDate.Text = objCV.Date;
            txtExeName.Text = objCV.ExeName;
            ddlPreparedBy.SelectedValue = objCV.preparedby;
            lblVoucherID.Text = objCV.id;

            objCV.Conv_VoucherDetails_Select(gvConvenienceVoucher.SelectedRow.Cells[0].Text, gvVoucherDetails);

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        if (gvConvenienceVoucher.SelectedIndex > -1)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                MessageBox.Show(this, objSM.Convinence_Form_Delete(gvConvenienceVoucher.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                btnDelete.Attributes.Clear();
                //gvConvenienceVoucher.DataBind();
                BindGrid_All();

                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvConvenienceVoucher.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=CV1&sdid=" + gvConvenienceVoucher.SelectedRow.Cells[0].Text + "";
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
    protected void btnAddDet_Click(object sender, EventArgs e)
    {
        DataTable VoucherDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Site_Name");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("Purpose");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("From_Loc_Id");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("To_Loc_Id");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("From_Time");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("To_Time");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("KMs");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("On_Date");
        VoucherDetails.Columns.Add(col);

        if (gvVoucherDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvVoucherDetails.Rows)
            {
                if (gvVoucherDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvVoucherDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = VoucherDetails.NewRow();
                        dr["Site_Name"] = txtSiteName.Text;
                        dr["Purpose"] = txtPurpose.Text;
                        dr["From_Loc_Id"] = txtFromLoc.Text;
                        dr["To_Loc_Id"] = txtToLoc.Text;
                        dr["From_Time"] = txtFromTime.Text;
                        dr["To_Time"] = txtToTime.Text;
                        dr["KMs"] = txtKMs.Text;
                        dr["On_Date"] = txtOnDate.Text;

                        VoucherDetails.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = VoucherDetails.NewRow();
                        dr["Site_Name"] = gvrow.Cells[1].Text;
                        dr["Purpose"] = gvrow.Cells[2].Text;
                        dr["From_Loc_Id"] = gvrow.Cells[3].Text;
                        dr["To_Loc_Id"] = gvrow.Cells[4].Text;
                        dr["From_Time"] = gvrow.Cells[5].Text;
                        dr["To_Time"] = gvrow.Cells[6].Text;
                        dr["KMs"] = gvrow.Cells[7].Text;
                        dr["On_Date"] = gvrow.Cells[8].Text;

                        VoucherDetails.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = VoucherDetails.NewRow();
                    dr["Site_Name"] = gvrow.Cells[1].Text;
                    dr["Purpose"] = gvrow.Cells[2].Text;
                    dr["From_Loc_Id"] = gvrow.Cells[3].Text;
                    dr["To_Loc_Id"] = gvrow.Cells[4].Text;
                    dr["From_Time"] = gvrow.Cells[5].Text;
                    dr["To_Time"] = gvrow.Cells[6].Text;
                    dr["KMs"] = gvrow.Cells[7].Text;
                    dr["On_Date"] = gvrow.Cells[8].Text;

                    VoucherDetails.Rows.Add(dr);
                }
            }
        }

        if (gvVoucherDetails.SelectedIndex == -1)
        {
            DataRow drnew = VoucherDetails.NewRow();
            drnew["Site_Name"] = txtSiteName.Text;
            drnew["Purpose"] = txtPurpose.Text;
            drnew["From_Loc_Id"] = txtFromLoc.Text;
            drnew["To_Loc_Id"] = txtToLoc.Text;
            drnew["From_Time"] = txtFromTime.Text;
            drnew["To_Time"] = txtToTime.Text;
            drnew["KMs"] = txtKMs.Text;
            drnew["On_Date"] = txtOnDate.Text;

            VoucherDetails.Rows.Add(drnew);
        }
        gvVoucherDetails.DataSource = VoucherDetails;
        gvVoucherDetails.DataBind();
        gvVoucherDetails.SelectedIndex = -1;
        btnRefresh_Click(sender, e);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSiteName.Text = "";
        txtPurpose.Text = "";
        txtFromLoc.Text = "";
        txtToLoc.Text = "";
        txtFromTime.Text = "";
        txtToTime.Text = "";
        txtKMs.Text = "";

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            HR.Convenience_Voucher obj = new HR.Convenience_Voucher();
            obj.Date = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
            obj.VoucherNo = txtVoucherNo.Text;
            obj.ExeName = txtExeName.Text;
            obj.Cpid = lblCPID.Text;
            obj.preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

            if (btnSave.Text == "Save")
            {
                if (obj.Conv_Voucher_Save() == "Data Saved Successfully")
                {
                    obj.Conv_VoucheDetails_Delete(obj.id);
                    #region Details Save
                    foreach (GridViewRow gvrow in gvVoucherDetails.Rows)
                    {

                        obj.SiteName = gvrow.Cells[1].Text;
                        obj.Purpose = gvrow.Cells[2].Text;
                        obj.From = gvrow.Cells[3].Text;
                        obj.To = gvrow.Cells[4].Text;

                        obj.FromTime = gvrow.Cells[5].Text;
                        obj.ToTime = gvrow.Cells[6].Text;
                        obj.KMs = gvrow.Cells[7].Text;
                        obj.On_Date = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[8].Text);
                        obj.TotalKms = lblTotalKMs.Text;
                        obj.Conv_VoucherDetails_Save();
                    }
                    #endregion

                    MessageBox.Show(this, "Data Saved Succesfully");
                }
            }
            else
            {
                //if (obj.Conv_VoucheDetails_Delete(lblVoucherID.Text) == "Data Deleted Successfully")
                //{
                obj.Conv_VoucheDetails_Delete(lblVoucherID.Text);
                if (obj.Conv_Vouche_Delete(lblVoucherID.Text) == "Data Deleted Successfully")
                {
                    if (obj.Conv_Voucher_Save() == "Data Saved Successfully")
                    {

                        #region Details Save
                        foreach (GridViewRow gvrow in gvVoucherDetails.Rows)
                        {

                            obj.SiteName = gvrow.Cells[1].Text;
                            obj.Purpose = gvrow.Cells[2].Text;
                            obj.From = gvrow.Cells[3].Text;
                            obj.To = gvrow.Cells[4].Text;

                            obj.FromTime = gvrow.Cells[5].Text;
                            obj.ToTime = gvrow.Cells[6].Text;
                            obj.KMs = gvrow.Cells[7].Text;
                            //String dt = Convert.ToDateTime(gvrow.Cells[8].Text).ToString();
                            obj.On_Date = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[8].Text);
                            obj.TotalKms = lblTotalKMs.Text;
                            obj.Conv_VoucherDetails_Save();
                        }
                        #endregion

                        MessageBox.Show(this, "Data Updated Succesfully");
                    }
                    //}
                }

            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //gvConvenienceVoucher.DataBind();
            HR.ClearControls(this);
            btnMainRefresh_Click(sender, e);
            tblDetails.Visible = false;
            btnSave.Text = "Save";
            lblVoucherID.Text = "";
            //BindGrid_All();
            Response.Redirect("~/Modules/HR/Convenience_Voucher.aspx");
        }
    }
    protected void btnMainRefresh_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
        gvVoucherDetails.DataSource = null;
        gvVoucherDetails.DataBind();
        txtVoucherNo.Text = HR.Convenience_Voucher.Convenience_Voucher_AutoGenCode();

    }

    #region gvVoucherDetails_RowDeleting
    protected void gvVoucherDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvVoucherDetails.Rows[e.RowIndex].Cells[2].Text;
        DataTable VoucherDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Site_Name");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("Purpose");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("From_Loc_Id");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("To_Loc_Id");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("From_Time");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("To_Time");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("KMs");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("On_Date");
        VoucherDetails.Columns.Add(col);
        if (gvVoucherDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvVoucherDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = VoucherDetails.NewRow();
                    dr["Site_Name"] = gvrow.Cells[1].Text;
                    dr["Purpose"] = gvrow.Cells[2].Text;
                    dr["From_Loc_Id"] = gvrow.Cells[3].Text;
                    dr["To_Loc_Id"] = gvrow.Cells[4].Text;
                    dr["From_Time"] = gvrow.Cells[5].Text;
                    dr["To_Time"] = gvrow.Cells[6].Text;
                    dr["KMs"] = gvrow.Cells[7].Text;
                    dr["On_Date"] = gvrow.Cells[8].Text;
                    VoucherDetails.Rows.Add(dr);
                }
            }
        }
        gvVoucherDetails.DataSource = VoucherDetails;
        gvVoucherDetails.DataBind();
    }
    #endregion
    protected void gvVoucherDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Total = Total + Convert.ToDecimal(e.Row.Cells[7].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total KMs :";
            e.Row.Cells[7].Text = lblTotalKMs.Text = Total.ToString();
        }
    }
}