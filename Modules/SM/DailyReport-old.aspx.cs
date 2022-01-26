using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
using System.Data.SqlClient;
using YantraDAL;
using System.IO;

public partial class Modules_SM_DailyReport : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.SetActiveView(View1);

            setControlsVisibility();
            EmployeeMaster_Fill1();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblDeptId.Text =Yantra .Authentication .GetEmployeeInSession (Yantra.Authentication.Logged_EMP_Details.DeptId );
            DeptHead_Check();
            //lblDeptHeadId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
            EmployeeMaster_Fill();
            ddlPreparedBy.SelectedValue  = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            ddlCommentedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            BindDailyReportDtls();
            BindGrid_All();

            CheckBoxList1.DataBind();
            txtDate.Text = DateTime.Now.ToString();
            FillCompany();
            FillDept();
        }
    }
    #region FillCompany
    private void FillCompany()
    {
        try
        {

            HR.Company_Select(ddlCompanyid);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }

    }
    #endregion
    private void FillDept()
    {
        try
        {
            HR.Dept_Select(ddlDept);

        }
        catch
        {
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteDailyReport();
        BindGrid_All();
    }
    #region Employee Master Fill
    private void EmployeeMaster_Fill1()
    {
        try
        {

            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesPerson);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //HR.Dispose();
        }
    }
    #endregion
    protected void lnkPOAmen_Click(object sender, EventArgs e)
    {
        POAmendment.Visible = true;
        ReturnNote.Visible = false;
    }
    private void DeleteDailyReport()
    {
        #region Delete Application
        foreach (GridViewRow gvr in gvDrs.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label onDutyId = (Label)gvr.FindControl("lblId");
                    int ID = Convert.ToInt32(onDutyId.Text);
                    SqlCommand cmd = new SqlCommand("USP_Delete_DailyReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DailyReport_ID", ID);
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
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "13");
        btnADD.Enabled = up.add;
        btnDelete.Enabled = up.Delete;
        //btnSave.Enabled = up.Save;
        //btnRefresh.Enabled = up.Refresh;

    }

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlAttendedBy);
            //HR.EmployeeMaster.EmployeeMaster_SelectForSales(ddlAttendedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlCommentedBy ); 
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion

    protected void btnADD_Click(object sender, EventArgs e)
    {
        if (Uploadattach.HasFiles)
        {
            #region Item Attachment
            string Attachment = "";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles"))
            {

                foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                {
                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 10000));
                    string path = Server.MapPath("~/YANTRA_DOCUMENTS/SOFiles/");
                    string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                    Attachment = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    lblAtt.Text  = Attachment;
                    //objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text;
                    //objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                    //objSM.SalesOrderUploads_Save();
                }

            }
            else
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles");
                foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                {
                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 10000));
                    string path = Server.MapPath("~/YANTRA_DOCUMENTS/SOFiles/");
                    string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                    Attachment = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    lblAtt.Text = Attachment;
                    //objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text;
                    //objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                    //objSM.SalesOrderUploads_Save();
                }

            }

            #endregion
        }
        DataTable DailyReport = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("DATETIME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("CLIENTSNAME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("PURPOSE");
        DailyReport.Columns.Add(col);
        col = new DataColumn("REMARKS");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ATTENDEDBY");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ATTENDEDBYNAME");//this is used to show the attended emp name in grid 
        DailyReport.Columns.Add(col);
        col = new DataColumn("EMPLOYEENAME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("EMPNAMEFORSHOW");//this is used to show the emp name in grid 
        DailyReport.Columns.Add(col);
        col = new DataColumn("HOUR");
        DailyReport.Columns.Add(col);
        col = new DataColumn("MIN");
        DailyReport.Columns.Add(col);
        col = new DataColumn("AMPM");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ADDRESS");
        DailyReport.Columns.Add(col);
        col = new DataColumn("PHONE");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ref");
        DailyReport.Columns.Add(col);
        col = new DataColumn("arch");
        DailyReport.Columns.Add(col);
        col = new DataColumn("OutHOUR");
        DailyReport.Columns.Add(col);
        col = new DataColumn("OutMIN");
        DailyReport.Columns.Add(col);
        col = new DataColumn("OutAMPM");
        DailyReport.Columns.Add(col);
        col = new DataColumn("UploadDoc");
        DailyReport.Columns.Add(col);
        if (gvDailyReport.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDailyReport.Rows)
            {
                if (gvDailyReport.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvDailyReport.SelectedRow.RowIndex)
                    {
                        DataRow dr = DailyReport.NewRow();
                        dr["DATETIME"] = txtDateTime.Text;
                        dr["CLIENTSNAME"] = txtClientsName.Text;
                        dr["PURPOSE"] = txtPurpose.Text;
                        dr["REMARKS"] = txtRemarks.Text;
                        dr["ATTENDEDBY"] = ddlAttendedBy.SelectedValue;
                        dr["EMPLOYEENAME"] = ddlPreparedBy.SelectedValue;
                        dr["ATTENDEDBYNAME"] = ddlAttendedBy.SelectedItem.Text;
                        dr["EMPNAMEFORSHOW"] = ddlPreparedBy.SelectedItem.Text;
                        dr["HOUR"] = ddlHour.SelectedValue;
                        dr["MIN"] = ddlMin.SelectedItem.Text;
                        dr["AMPM"] = ddlAMPM.SelectedItem.Text;
                        dr["ADDRESS"] = txtPhoneNo.Text;
                        dr["PHONE"] = txtAddress.Text;
                        dr["ref"] = txtReference.Text;
                        dr["arch"] = txtArchitect.Text;
                        dr["OutHOUR"] = ddlOutHour.SelectedValue;
                        dr["OutMIN"] = ddlOutMin.SelectedItem.Text;
                        dr["OutAMPM"] = ddlOutAMPM.SelectedItem.Text;
                        dr["UploadDoc"] = lblAtt.Text;
                        DailyReport.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = DailyReport.NewRow();
                        dr["DATETIME"] = gvrow.Cells[3].Text;
                        dr["CLIENTSNAME"] = gvrow.Cells[2].Text;
                        dr["PURPOSE"] = gvrow.Cells[4].Text;
                        dr["REMARKS"] = gvrow.Cells[5].Text;
                        dr["ATTENDEDBY"] = gvrow.Cells[6].Text;
                        dr["EMPLOYEENAME"] = gvrow.Cells[7].Text;
                        dr["ATTENDEDBYNAME"] = gvrow.Cells[8].Text;
                        dr["EMPNAMEFORSHOW"] = gvrow.Cells[9].Text;
                        dr["HOUR"] = gvrow.Cells[10].Text;
                        dr["MIN"] = gvrow.Cells[11].Text;
                        dr["AMPM"] = gvrow.Cells[12].Text;
                        dr["ADDRESS"] = gvrow.Cells[13].Text;
                        dr["PHONE"] = gvrow.Cells[14].Text;
                        dr["ref"] = gvrow.Cells[15].Text;
                        dr["arch"] = gvrow.Cells[16].Text;
                        dr["OutHOUR"] = gvrow.Cells[17].Text;
                        dr["OutMIN"] = gvrow.Cells[18].Text;
                        dr["OutAMPM"] = gvrow.Cells[19].Text;
                        dr["UploadDoc"] = gvrow.Cells[20].Text;

                        DailyReport.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = DailyReport.NewRow();
                    dr["DATETIME"] = gvrow.Cells[3].Text;
                    dr["CLIENTSNAME"] = gvrow.Cells[2].Text;
                    dr["PURPOSE"] = gvrow.Cells[4].Text;
                    dr["REMARKS"] = gvrow.Cells[5].Text;
                    dr["ATTENDEDBY"] = gvrow.Cells[6].Text;
                    dr["EMPLOYEENAME"] = gvrow.Cells[7].Text;
                    dr["ATTENDEDBYNAME"] = gvrow.Cells[8].Text;
                    dr["EMPNAMEFORSHOW"] = gvrow.Cells[9].Text;
                    dr["HOUR"] = gvrow.Cells[10].Text;
                    dr["MIN"] = gvrow.Cells[11].Text;
                    dr["AMPM"] = gvrow.Cells[12].Text;
                    dr["ADDRESS"] = gvrow.Cells[13].Text;
                    dr["PHONE"] = gvrow.Cells[14].Text;
                    dr["ref"] = gvrow.Cells[15].Text;
                    dr["arch"] = gvrow.Cells[16].Text;
                    dr["OutHOUR"] = gvrow.Cells[17].Text;
                    dr["OutMIN"] = gvrow.Cells[18].Text;
                    dr["OutAMPM"] = gvrow.Cells[19].Text;
                    dr["UploadDoc"] = gvrow.Cells[20].Text;
                    DailyReport.Rows.Add(dr);
                }
            }
        }
        if (gvDailyReport.SelectedIndex == -1)
        {
            DataRow drnew = DailyReport.NewRow();
            drnew["DATETIME"] = txtDateTime.Text;
            drnew["CLIENTSNAME"] = txtClientsName.Text;
            drnew["PURPOSE"] = txtPurpose.Text;
            drnew["REMARKS"] = txtRemarks.Text;
            drnew["ATTENDEDBY"] = ddlAttendedBy.SelectedValue;
            drnew["EMPLOYEENAME"] = ddlPreparedBy.SelectedValue;
            drnew["ATTENDEDBYNAME"] = ddlAttendedBy.SelectedItem.Text;
            drnew["EMPNAMEFORSHOW"] = ddlPreparedBy.SelectedItem.Text;
            drnew["HOUR"] = ddlHour.SelectedValue;
            drnew["MIN"] = ddlMin.SelectedItem.Text;
            drnew["AMPM"] = ddlAMPM.SelectedItem.Text;
            drnew["ADDRESS"] = txtAddress.Text;
            drnew["PHONE"] = txtPhoneNo.Text;
            drnew["ref"] = txtReference.Text;
            drnew["arch"] = txtArchitect.Text;
            drnew["OutHOUR"] = ddlOutHour.SelectedValue;
            drnew["OutMIN"] = ddlOutMin.SelectedItem.Text;
            drnew["OutAMPM"] = ddlOutAMPM.SelectedItem.Text;
            drnew["UploadDoc"] = lblAtt.Text;

            DailyReport.Rows.Add(drnew);

        }
        gvDailyReport.DataSource = DailyReport;
        gvDailyReport.DataBind();
        gvDailyReport.SelectedIndex = -1;
        txtClientsName.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtDateTime.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        ddlAttendedBy.SelectedValue = "0";
        txtReference.Text = string.Empty;
        txtArchitect.Text = string.Empty;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        SM.BeginTransaction();
        try
        {
            if (gvDailyReport.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvDailyReport.Rows)
                {
                    objdr.DRDate = Yantra.Classes.General.toMMDDYYYY(gvr.Cells[3].Text);
                    objdr.CustName = gvr.Cells[2].Text;
                    objdr.Purpose = gvr.Cells[4].Text;
                    objdr.Remarks = gvr.Cells[5].Text;
                    objdr.DRAttendedBy = gvr.Cells[6].Text;
                    objdr.DRPreparedBy = gvr.Cells[7].Text;
                    objdr.Time = "01/01/1900 " + gvr.Cells[10].Text + ":" + gvr.Cells[11].Text + " " + gvr.Cells[12].Text;
                    objdr.Address = gvr.Cells[13].Text;
                    objdr.Phone = gvr.Cells[14].Text;
                    objdr.Reference = gvr.Cells[15].Text;
                    objdr.Architect = gvr.Cells[16].Text;
                    objdr.outTime = "01/01/1900 " + gvr.Cells[17].Text + ":" + gvr.Cells[18].Text + " " + gvr.Cells[19].Text;
                    objdr.Comment = "";
                    //ddlHour.SelectedValue + ":" + ddlMin.SelectedValue + " " + ddlAMPM.SelectedValue;
                    objdr.FileName = gvr.Cells[20].Text;
                    objdr.DailyReports_Save();
                }

                SM.CommitTransaction();
                MessageBox.Show(this, "Data Saved Successfully");
            }
            else
            {
                MessageBox.Show(this, "Please Add Atleast One Record to Daily Report Grid");
            }
        }
        catch(Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this,ex.Message);
        }
        finally
        {
            
            SM.Dispose();
            btnRefresh_Click(sender, e);
            BindDailyReportDtls();
            BindGrid_All();
        }
    }

    private void BindDailyReportDtls()
    {
        SqlCommand cmd = new SqlCommand("USP_getDailyReport", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlSearchBy.SelectedIndex == 0)
        {
            lblSearchItemHidden.Text = lblSearchTypeHidden.Text = lblSearchValueHidden.Text = lblSearchValueFromHidden.Text = "0";
        }
        cmd.Parameters.AddWithValue("@SearchItemName", lblSearchItemHidden.Text);
        cmd.Parameters.AddWithValue("@SearchType", lblSearchTypeHidden.Text);
        cmd.Parameters.AddWithValue("@SearchValue", lblSearchValueHidden.Text);
        cmd.Parameters.AddWithValue("@SearchValueFrom", lblSearchValueFromHidden.Text);
        cmd.Parameters.AddWithValue("@userType", lblUserType.Text);
        cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);
        //cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
        //cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
        //cmd.Parameters.AddWithValue("@CLIENTSNAME", txtClientName.Text);
        //cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmpName.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDailyRptDtls.DataSource = dt;
        gvDailyRptDtls.DataBind();
        //gvDailyReportSearch.DataSource = dt;
        //gvDailyReportSearch.DataBind();
        
       
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtClientsName.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtDateTime.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        ddlAttendedBy.SelectedValue = "0";
        gvDailyReport.DataBind();
    }
    protected void gvDailyReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
           
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Delivery Report list?');");
        }
    }
    protected void gvDailyReport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable DailyReport = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("DATETIME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("CLIENTSNAME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("PURPOSE");
        DailyReport.Columns.Add(col);
        col = new DataColumn("REMARKS");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ATTENDEDBY");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ATTENDEDBYNAME");//this is used to show the attended emp name in grid 
        DailyReport.Columns.Add(col);
        col = new DataColumn("EMPLOYEENAME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("EMPNAMEFORSHOW");//this is used to show the emp name in grid 
        DailyReport.Columns.Add(col);
        col = new DataColumn("HOUR");
        DailyReport.Columns.Add(col);
        col = new DataColumn("MIN");
        DailyReport.Columns.Add(col);
        col = new DataColumn("AMPM");
        DailyReport.Columns.Add(col);

        if (gvDailyReport.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDailyReport.Rows)
            {
                DataRow dr = DailyReport.NewRow();
                dr["DATETIME"] = gvrow.Cells[3].Text;
                dr["CLIENTSNAME"] = gvrow.Cells[2].Text;
                dr["PURPOSE"] = gvrow.Cells[4].Text;
                dr["REMARKS"] = gvrow.Cells[5].Text;
                dr["ATTENDEDBY"] = gvrow.Cells[6].Text;
                dr["EMPLOYEENAME"] = gvrow.Cells[7].Text;
                dr["ATTENDEDBYNAME"] = gvrow.Cells[8].Text;
                dr["EMPNAMEFORSHOW"] = gvrow.Cells[9].Text;
                dr["HOUR"] = gvrow.Cells[10].Text;
                dr["MIN"] = gvrow.Cells[11].Text;
                dr["AMPM"] = gvrow.Cells[12].Text;
                DailyReport.Rows.Add(dr);

                if (gvrow.RowIndex == gvDailyReport.Rows[e.NewEditIndex].RowIndex)
                {
                    txtDateTime.Text = gvrow.Cells[3].Text; ;
                    txtClientsName.Text = gvrow.Cells[2].Text;
                    txtPurpose.Text = gvrow.Cells[4].Text;
                    txtRemarks.Text = gvrow.Cells[5].Text;
                    ddlAttendedBy.SelectedValue = gvrow.Cells[6].Text;
                    ddlHour.SelectedValue = gvrow.Cells[10].Text;
                    ddlMin.SelectedValue = gvrow.Cells[11].Text;
                    ddlAMPM.SelectedValue = gvrow.Cells[12].Text;
                    //txtAddress.Text = gvrow.Cells[13].Text;
                    gvDailyReport.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvDailyReport.DataSource = DailyReport;
        gvDailyReport.DataBind();
    }
    protected void gvDailyReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable DailyReport = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("DATETIME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("CLIENTSNAME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("PURPOSE");
        DailyReport.Columns.Add(col);
        col = new DataColumn("REMARKS");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ATTENDEDBY");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ATTENDEDBYNAME");//this is used to show the attended emp name in grid 
        DailyReport.Columns.Add(col);
        col = new DataColumn("EMPLOYEENAME");
        DailyReport.Columns.Add(col);
        col = new DataColumn("EMPNAMEFORSHOW");//this is used to show the emp name in grid 
        DailyReport.Columns.Add(col);
        col = new DataColumn("HOUR");
        DailyReport.Columns.Add(col);
        col = new DataColumn("MIN");
        DailyReport.Columns.Add(col);
        col = new DataColumn("AMPM");
        DailyReport.Columns.Add(col);
        col = new DataColumn("ADDRESS");
        DailyReport.Columns.Add(col);
        col = new DataColumn("PHONE");
        DailyReport.Columns.Add(col);

        if (gvDailyReport.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvDailyReport.Rows)
            { 
                if (gvrow.RowIndex != e.RowIndex)
                {
                DataRow dr = DailyReport.NewRow();
                dr["DATETIME"] = gvrow.Cells[3].Text;
                dr["CLIENTSNAME"] = gvrow.Cells[2].Text;
                dr["PURPOSE"] = gvrow.Cells[4].Text;
                dr["REMARKS"] = gvrow.Cells[5].Text;
                dr["ATTENDEDBY"] = gvrow.Cells[6].Text;
                dr["EMPLOYEENAME"] = gvrow.Cells[7].Text;
                dr["ATTENDEDBYNAME"] = gvrow.Cells[8].Text;
                dr["EMPNAMEFORSHOW"] = gvrow.Cells[9].Text;
                dr["HOUR"] = gvrow.Cells[10].Text;
                dr["MIN"] = gvrow.Cells[11].Text;
                dr["AMPM"] = gvrow.Cells[12].Text;
                dr["ADDRESS"] = gvrow.Cells[13].Text;
                dr["PHONE"] = gvrow.Cells[14].Text;

                DailyReport.Rows.Add(dr);

                }
            }
        }
        gvDailyReport.DataSource = DailyReport;
        gvDailyReport.DataBind();
    }

    protected void gvDailyRptDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       // e.NewPageIndex = -1;
        gvDailyRptDtls.PageIndex = e.NewPageIndex;
        BindDailyReportDtls();
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvDailyRptDtls.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == " Date")
        {

            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        BindDailyReportDtls();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == " Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;

    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvDailyRptDtls.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    protected void gvDailyRptDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //   e.Row.Cells[5].Width = Unit.Pixel(1000);
        
     
        //}
    }

    protected void gvDrs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDrs.PageIndex = e.NewPageIndex;
        BindGrid_All();
    }
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_DailyReportSearch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblUserType.Text != "")
        {
            cmd.Parameters.AddWithValue("@userType", lblUserType.Text);

        }
        if (lblEmpIdHidden.Text != "")
        {
            cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);

        }
        if (txtClientName.Text != "")
        {
            cmd.Parameters.AddWithValue("@CLIENTSNAME", txtClientName.Text);
        }
        //if (txtEmp_Name.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmp_Name.Text);
        //}
        if (ddlSalesPerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", ddlSalesPerson.SelectedItem.Value);
        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (lblDeptId.Text != "")
        {
            cmd.Parameters.AddWithValue("@DeptId", lblDeptId.Text);
        }
        if (lblDeptHead.Text != "")
        {
            DeptHead_Check();
            cmd.Parameters.AddWithValue("@DeptHead", lblDeptHeadId.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvDrs.DataSource = dt;
        gvDrs.DataBind();
    }

    private void DeptHead_Check()
    {
        //SqlCommand cmd = new SqlCommand("select DEPT_HEAD  from YANTRA_DEPT_MAST   where DEPT_ID =" + lblDeptId.Text + "", con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count != 0)
        //{
        //    lblDeptHeadId.Text = dt.Rows[0].ToString();
        //    if (lblEmpIdHidden.Text == lblDeptHeadId.Text)
        //    {
        //        lblDeptHead.Text = "1";
        //    }
        //    else
        //    {
        //        lblDeptHead.Text = "0";
        //    }
        //}
        //else
        //{
        //    return;
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid_All();
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
           btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

    }
    protected void btnPostComment_Click(object sender, EventArgs e)
    {
        PostAdminComment();
        BindGrid_All();
    }

    private void PostAdminComment()
    {
        #region Post Admin Commnet
        foreach (GridViewRow gvr in gvDrs.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label DailyReportId = (Label)gvr.FindControl("lblId");
                    TextBox comment = (TextBox)gvr.FindControl("txtComment");
                    SqlCommand cmd = new SqlCommand("USP_UpdateDailyReportComment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DailyReportId", DailyReportId.Text);
                    cmd.Parameters.AddWithValue("@Comment", comment.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Error Occured in Updating Comments. Please Try Again");
                }
            }
        }
        #endregion
    }
    protected void gvDrs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[15].Visible = false;
            TextBox comments = (TextBox)e.Row.FindControl("txtComment");
            if (comments.Text != null && comments.Text != "")
            {
                e.Row.BackColor = System.Drawing.Color.Coral;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
    protected void btnFollowUp_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvDrs.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    if (tblFollowUp.Visible == false)
                    {
                        tblFollowUp.Visible = true;
                    }
                    else if (tblFollowUp.Visible == true)
                    {
                        tblFollowUp.Visible = false;
                    }
                    lblId.Text = gvr.Cells[15].Text;
                    txtCustName.Text = gvr.Cells[5].Text;
                    //Yantra.Classes.General.toMMDDYYYY(gvr.Cells[2].Text);
                    txtDate.Text = Yantra.Classes.General.toMMDDYYYY(gvr.Cells[2].Text);
                    txtRef.Text = gvr.Cells[7].Text;
                    txtAdd.Text = gvr.Cells[9].Text;
                    txtPurps.Text = gvr.Cells[10].Text;
                    txtRemrks.Text = gvr.Cells[11].Text;
                    Label DailyReportId = (Label)gvr.FindControl("lblId");
                    //TextBox txtComments = (TextBox)gvr.FindControl("txtComment");
                    //txtRemarks.Text = txtComments.ToString();
                }
                catch (Exception ex) { 
                }
                gvFollowUp.DataBind();
                
            }
        }
    }
    #region Enquiry Assignments FollowUp

    protected void btnFollowUpSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvDrs.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    SM.DailyReport obj = new SM.DailyReport();
                    SM.BeginTransaction();
                    Label DailyReportId = (Label)gvr.FindControl("lblId");
                    obj.DRId = DailyReportId.Text;
                    //obj.DRId  = gvDrs.SelectedRow.Cells[14].Text;
                    obj.DetCustName = txtCustName.Text;
                    obj.DetReference = txtReference.Text;
                    obj.DetPurpose = txtPurps.Text;
                    obj.DetRemarks = txtRemrks.Text;
                    obj.DetComments = txtFollowUpDesc.Text;
                    obj.DRDetDate = txtDate.Text;
                    obj.CommentedBy = ddlCommentedBy.SelectedItem.Value;
                    //MessageBox.Show(this, obj.DailyReportDet_Save());
                    if (obj.DailyReportDet_Save() == "Data Saved Successfully")
                    {
                        TextBox txtComm = (TextBox)gvr.FindControl("txtComment");
                        txtComm.Text = obj.DetComments;
                        Label DailyRepId = (Label)gvr.FindControl("lblId");
                        obj.DRId = DailyRepId.Text;
                        obj.DailyReportComm_Update();

                    }
                    SM.CommitTransaction();
                }
                catch (Exception ex)
                {
                    SM.RollBackTransaction();
                    MessageBox.Show(this, ex.Message);
                }
                finally
                {
                    gvFollowUp.DataBind();
                    txtCustName.Text = string.Empty;
                    txtReference.Text = string.Empty;
                    txtPurps.Text = string.Empty;
                    txtRemrks.Text = string.Empty;
                    txtAdd.Text = string.Empty;
                    txtFollowUpDesc.Text = string.Empty;
                    txtDate.Text = string.Empty;
                    SM.Dispose();
                }
            }
        }
    }

    //protected void btnFollowUpRefresh_Click(object sender, EventArgs e)
    //{
    //    txtFollowUpDesc.Text = string.Empty;
    //}

    //protected void btnFollowUpHistory_Click(object sender, EventArgs e)
    //{
    //    if (tblFollowUpHistory.Visible == false)
    //    {
    //        tblFollowUpHistory.Visible = true;
    //    }
    //    else if (tblFollowUpHistory.Visible == true)
    //    {
    //        tblFollowUpHistory.Visible = false;
    //    }
    //}

    //protected void btnFollowUpClose_Click(object sender, EventArgs e)
    //{
    //    tblFollowUp.Visible = false;
    //}

   

    #endregion
    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Visible = false;
            if (e.Row.Cells[8].Text == "0")
            {
                e.Row.BackColor = System.Drawing.Color.Pink;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.SkyBlue;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        HR.EmployeeDept(ddlEmployee, ddlDept.SelectedItem.Value);
    }


    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string st = "";
        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected)
            {
                if (st != "")
                {
                    st += ",";
                }

                st = st + li.Value;
            }
        }

        set_EmployeesList(st);
        foreach (ListItem li in ListBox1.Items)
        {

            li.Selected = true;
        }
    }
    protected void set_EmployeesList(string deptids)
    {

        ListBox1.Items.Clear();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = default(SqlDataReader);


        con.Close();
        cmd = new SqlCommand(@"SELECT     YANTRA_EMPLOYEE_MAST.EMP_ID, YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME + ' ' + YANTRA_EMPLOYEE_MAST.EMP_MIDDLE_NAME as Fullname
FROM         YANTRA_EMPLOYEE_MAST INNER JOIN
                      YANTRA_EMPLOYEE_DET ON YANTRA_EMPLOYEE_MAST.EMP_ID = YANTRA_EMPLOYEE_DET.EMP_ID 
where YANTRA_EMPLOYEE_MAST.STATUS = 1 and DEPT_ID in (" + deptids + ")", con);

        con.Open();

        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (dr.Read())
        {
            ListItem li1 = new ListItem();
            li1.Text = dr["Fullname"].ToString();
            li1.Value = dr["EMP_ID"].ToString();

            ListBox1.Items.Add(li1);

        }

        con.Close();
    }
    #region DDLCompany
    protected void ddlCompanyid_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeCompany(ddlEmployee, ddlCompanyid.SelectedItem.Value);
    }
    #endregion
    #region DDL Employee
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        HR.EmployeeMaster obj = new HR.EmployeeMaster();
        obj.EmployeeMaster_Select(ddlEmployee.SelectedItem.Value);
        txtMobileno.Text = obj.EmpMobile;
        txtDesignation.Text = obj.DesgName12;
    }
    #endregion
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (btnSave1.Text == "Save")
        {
            CircularSave();
        }
    }
    #region CircularSave
    private void CircularSave()
    {
        try
        {

            foreach (ListItem li in ListBox1.Items)
            {
                string empid = li.Value;

                if (li.Selected)
                {
                    HR.EmployeeMaster em = new HR.EmployeeMaster(empid);


                    HR.Circular objMaster = new HR.Circular();
                    objMaster.circular = txtcirNo.Text;
                    objMaster.CmpId = ddlActivity.SelectedItem.Value;
                    //objMaster.DeptId = em.DeptID;
                    objMaster.DeptId = lblEmpIdHidden.Text;
                    objMaster.empid = empid;
                    objMaster.issuedate = txtDate.Text;
                    objMaster.descrption = txtdescription.Text;
                    objMaster.readmsg = "0";
                    MessageBox.Show(this, objMaster.Circular_Save());


                }
            }

            //HR.Circular objMaster = new HR.Circular();
            //if (ddlDept.SelectedValue == "0")
            //{
            //    objMaster.circular = txtcirNo.Text;
            //    objMaster.CmpId = ddlCompanyid.SelectedValue;
            //    objMaster.DeptId = ddlDept.SelectedValue;
            //    objMaster.empid = "0";
            //    objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
            //    objMaster.descrption = txtdescription.Text;

            //    MessageBox.Show(this, objMaster.Circular_Save());
            //}
            //else
            //{

            //    objMaster.circular = txtcirNo.Text;
            //    objMaster.CmpId = ddlCompanyid.SelectedValue;
            //    objMaster.DeptId = ddlDept.SelectedValue;
            //    objMaster.empid = ddlEmployee.SelectedValue;
            //    objMaster.issuedate = General.toMMDDYYYY(txtDate.Text);
            //    objMaster.descrption = txtdescription.Text;
            //    MessageBox.Show(this, objMaster.Circular_Save());

            //}
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            //gvCircular.DataBind();
            HR.ClearControls(this);
        }
    }
    #endregion
    protected void lbtnPINO_Click(object sender, EventArgs e)
    {
        LinkButton lbtnPINO;
        lbtnPINO = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnPINO.Parent.Parent;
        gvlist.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        tbldet.Visible = true;
        try
        {
            HR.Circular obj = new HR.Circular();
            if (obj.Circular_Select(gvlist.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave1.Text = "Update";
                txtcirNo.Text = obj.CirNo;
                txtcirdate.Text = obj.CirDate;
                txtdescription.Text = obj.Desc;
                ddlActivity.SelectedValue = obj.Cpid; 
            }
        }
        catch (Exception ex)
        {

        }
    }
    #region Refresh

    protected void btnRefresh1_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
    }
    #endregion
    protected void lnkSalesReturn_Click1(object sender, EventArgs e)
    {

        ReturnNote.Visible = true; POAmendment.Visible = false;
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "55") { e.Row.Cells[4].Text = "To Do"; }
            if (e.Row.Cells[4].Text == "56") { e.Row.Cells[4].Text = "In-Progress"; }
            if (e.Row.Cells[4].Text == "57") { e.Row.Cells[4].Text = "Completed"; }
        }
    }
    string pagenavigationstr;
    protected void runDailyReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=DailyReport&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDate.Text) + "&empid=" + ddlSalesPerson.SelectedValue + "&dep=" + ddlSalesPerson.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
}

 
