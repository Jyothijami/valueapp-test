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
            BindData();
            setControlsVisibility();
            EmployeeMaster_Fill1();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            lblUserId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.UserId);
            lblDeptId.Text =Yantra .Authentication .GetEmployeeInSession (Yantra.Authentication.Logged_EMP_Details.DeptId );
            DeptHead_Check();
            //lblDeptHeadId.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.DeptId);
            EmployeeMaster_Fill();
            ddlPreparedBy.SelectedValue  = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            ddlCommentedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            //ddlBackup.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);


          //  ddlAttendedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
          
            BindDailyReportDtls();
           
            BindToDoList_All();
            //CheckBoxList1.DataBind();
            txtDate.Text = DateTime.Now.ToString();
            BindGrid_All();

            BindTasks();
        }
    }

    private void BindTasks()
    {
        try
        {
            SM.DailyReport obj = new SM.DailyReport();
            if (obj.DailyReport_Tasks(lblEmpIdHidden.Text) > 0)
            {
                txtAchiveYesterday.Text = obj.AchiveYesterday;
                txtachiveToday.Text = obj.AchiveToday;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void BindChildGV()
    {
        foreach (GridViewRow gvrow in gvList.Rows)
        {
            GridView gvDC = (GridView)(gvList.Rows[gvrow.RowIndex].Cells[8].FindControl("gvChild"));
            SqlCommand cmd = new SqlCommand("USP_ToDOList_ReportingTo_Search", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[8].Text != "")
            {
                cmd.Parameters.AddWithValue("@ID", gvrow.Cells[8].Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();
        }
        
        
    }
    private void BindToDoList_All()
    {
        SqlCommand cmd = new SqlCommand("[USP_ToDOListSearch]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (lblUserType.Text != "")
        {
            cmd.Parameters.AddWithValue("@userType", lblUserType.Text);

        }
        if (lblEmpIdHidden.Text != "")
        {
            cmd.Parameters.AddWithValue("@EmpId", lblEmpIdHidden.Text);

        }
        if (txtListSubject.Text != "")
        {
            cmd.Parameters.AddWithValue("@CLIENTSNAME", txtListSubject.Text);
        }
        //if (txtEmp_Name.Text != "")
        //{
        //    cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", txtEmp_Name.Text);
        //}
        if (ddlEmp.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EMP_FIRST_NAME", ddlEmp.SelectedItem.Value);
        }
        if (txtListFrom.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtListFrom.Text));
        }
        if (txtListTo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtListTo.Text));
        }
        if (lblDeptId.Text != "")
        {
            cmd.Parameters.AddWithValue("@DeptId", lblDeptId.Text);
        }
        if (lblDeptHead.Text != "")
        {
            //DeptHead_Check();
            cmd.Parameters.AddWithValue("@DeptHead", lblDeptHeadId.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvList.DataSource = dt;
        gvList.DataBind();
        BindChildGV();

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
            Masters.EnquiryMode.EnquiryMode_Select(ddlreference);
            ddlreference.Items.FindByText("--").Text = "Not Selected";

            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlSalesPerson);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmp);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlBackup );
            Masters.CheckboxListWithStatement(CheckBoxList1, "Select * from YANTRA_LKUP_ITEM_CATEGORY");
            //CheckBoxList1.Items.Add("Spares");
            Masters.ProductCompany.ProductCompany_Select(ListBox1);
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
   
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {


       


        if (CheckBoxList1.Items.Count > 0)
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
            Set_Brand(st);
        }
        else
        {
            ListBox1.Items.Clear();
        }



        //foreach (ListItem li in ListBox1.Items)
        //{

        //    //li.Selected = true;
        //}
    }

    protected void Set_Brand(string CateId)
    {
        ListBox1.Items.Clear();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = default(SqlDataReader);


        //con.Open();
        cmd = new SqlCommand(@"SELECT  Distinct   PRODUCT_COMPANY_NAME, PRODUCT_COMPANY_ID
FROM         YANTRA_LKUP_PRODUCT_COMPANY ", con);

        con.Open();

        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (dr.Read())
        {
            ListItem li1 = new ListItem();
            li1.Text = dr["PRODUCT_COMPANY_NAME"].ToString();
            li1.Value = dr["PRODUCT_COMPANY_ID"].ToString();

            ListBox1.Items.Add(li1);

        }

        con.Close();
    }
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
                    MessageBox.Show(this, "Data Deleted Successfully");
                    BindGrid_All();
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
            HR.EmployeeMaster.EmployeeMaster_Select(ddlBackup);
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
        col = new DataColumn("BackupName");
        DailyReport.Columns.Add(col);
        col = new DataColumn("BackupID");
        DailyReport.Columns.Add(col);
        col = new DataColumn("DRType");
        DailyReport.Columns.Add(col);
        col = new DataColumn("Email");
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
                        dr["ref"] = ddlreference.SelectedItem.Text;

                        if (txtArchitect.Text != "")
                        {
                            dr["arch"] = txtArchitect.Text;
                        }
                        else
                        {
                            dr["arch"] = "NA";

                        }
                        //dr["arch"] = txtArchitect.Text;
                        dr["OutHOUR"] = ddlOutHour.SelectedValue;
                        dr["OutMIN"] = ddlOutMin.SelectedItem.Text;
                        dr["OutAMPM"] = ddlOutAMPM.SelectedItem.Text;
                        dr["UploadDoc"] = ddlAction.SelectedItem.Text;
                        dr["BackupName"] = ddlBackup.SelectedItem.Text;
                        dr["BackupID"] = ddlBackup.SelectedValue;
                        dr["DRType"] = ddlDRType.SelectedItem .Text ;
                        if (txtEmail.Text != "")
                        {
                            dr["Email"] = txtEmail.Text;
                        }
                        else
                        {
                            dr["Email"] = "-";

                        }
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
                        dr["BackupName"] = gvrow.Cells[21].Text;
                        dr["BackupID"] = gvrow.Cells[22].Text;
                        dr["DRType"] = gvrow.Cells[23].Text;
                        dr["Email"] = gvrow.Cells[24].Text;

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
                    dr["BackupName"] = gvrow.Cells[21].Text;
                    dr["BackupID"] = gvrow.Cells[22].Text;
                    dr["DRType"] = gvrow.Cells[23].Text;
                    dr["Email"] = gvrow.Cells[24].Text;

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
            drnew["ref"] = ddlreference.SelectedItem.Text;
            if (txtArchitect.Text != "")
            {
                drnew["arch"] = txtArchitect.Text;
            }
            else
            {
                drnew["arch"] = "NA";

            }
            //drnew["arch"] = txtArchitect.Text;
            drnew["OutHOUR"] = ddlOutHour.SelectedValue;
            drnew["OutMIN"] = ddlOutMin.SelectedItem.Text;
            drnew["OutAMPM"] = ddlOutAMPM.SelectedItem.Text;
            drnew["UploadDoc"] = ddlAction.SelectedItem.Text;
            drnew["BackupName"] = ddlBackup.SelectedItem.Text;
            drnew["BackupID"] = ddlBackup.SelectedValue;
            drnew["DRType"] = ddlDRType.SelectedItem.Text;
            if (txtEmail.Text != "")
            {
                drnew["Email"] = txtEmail.Text;
            }
            else
            {
                drnew["Email"] = "-";

            }
            DailyReport.Rows.Add(drnew);

        }
        gvDailyReport.DataSource = DailyReport;
        gvDailyReport.DataBind();
        gvDailyReport.SelectedIndex = -1;
        txtClientsName.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        //txtDateTime.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        txtAddress.Text = string.Empty;
        //ddlAttendedBy.SelectedValue = "0";
        ddlreference.SelectedValue  = "0";
        txtArchitect.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlBackup.SelectedValue = "0";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        //SM.BeginTransaction();
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
                    objdr.DRAssistedBy = gvr.Cells[22].Text;
                    objdr.Time = "01/01/1900 " + gvr.Cells[10].Text + ":" + gvr.Cells[11].Text + " " + gvr.Cells[12].Text;
                    objdr.Address = gvr.Cells[13].Text;
                    objdr.Phone = gvr.Cells[14].Text;
                    objdr.Reference = gvr.Cells[15].Text;
                    if (gvr.Cells[16].Text != "" || gvr.Cells[16].Text == string.Empty)
                    {
                        objdr.Architect = gvr.Cells[16].Text;

                    }
                    else { objdr.Architect = "NA"; }
                    objdr.outTime = "01/01/1900 " + gvr.Cells[17].Text + ":" + gvr.Cells[18].Text + " " + gvr.Cells[19].Text;
                    objdr.Comment = "";
                    //ddlHour.SelectedValue + ":" + ddlMin.SelectedValue + " " + ddlAMPM.SelectedValue;
                    objdr.FileName = gvr.Cells[20].Text;
                    objdr.DRType = gvr.Cells[23].Text;
                    objdr.DRFollowup = "DailyReport";
                    objdr.DRStatus = "Open";
                    if (gvr.Cells[24].Text != "" || gvr.Cells[24].Text ==string .Empty )
                    {
                        objdr.email = gvr.Cells[24].Text;

                    }
                    else { objdr.email = "-"; }
                    objdr.DailyReports_Save();
                    lblDrId.Text = objdr.DRId;
                    

                }
                if (txtachiveToday.Text != null && txtAchiveYesterday.Text != null)
                {
                    objdr.AchiveYesterday = txtAchiveYesterday.Text;
                    objdr.AchiveToday = txtachiveToday.Text;
                    objdr.EmpID = lblEmpIdHidden.Text;
                    objdr.Date = Yantra.Classes.General.toyymmdd(txtDateTime.Text);
                    objdr.DRTasks_Save();
                }
                SM.CustomerMaster objMaster = new SM.CustomerMaster();

                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    if (CheckBoxList1.Items[i].Selected == true)
                    {
                        objMaster.CatId = CheckBoxList1.Items[i].Value;
                        objMaster.CatText = CheckBoxList1.Items[i].Text;
                        objMaster.date = DateTime.Now.ToString();
                        objMaster.CustId = lblDrId.Text;
                        objMaster.DR_Analysis_Cat_Save();

                    }
                }

                for (int li = 0; li < ListBox1.Items.Count; li++)
                {
                    if (ListBox1.Items[li].Selected == true)
                    {
                        objMaster.BrandId = ListBox1.Items[li].Value;
                        objMaster.Brand_text = ListBox1.Items[li].Text;
                        objMaster.date = DateTime.Now.ToString();
                        objMaster.CustId = lblDrId.Text;

                        objMaster.DR_Analysis_Brand_Save();
                    }
                }
                
                //SM.CommitTransaction();
            }

            else
            {
                MessageBox.Show(this, "Please Add Atleast One Record to Daily Report Grid");
            }
        }
        catch(Exception ex)
        {
            //SM.RollBackTransaction();
            MessageBox.Show(this,ex.Message);
        }
        finally
        {
            
            SM.Dispose();
            btnRefresh_Click(sender, e);
            BindDailyReportDtls();
            BindGrid_All();
            MessageBox.Show(this, "Data Saved Successfully");
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
        btnListDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
           btnListDelete.Visible = true;
           btnListUpdate.Visible = true;
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
                    obj.DetReference = ddlreference.SelectedItem.Text;
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
                    //ddlreference.SelectedItem.Text = string.Empty;
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
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            HiddenField hf = (HiddenField)e.Row.FindControl("cthf1");
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
           
            ddlStatus.SelectedValue  = hf.Value;
        }
        
    }
    string pagenavigationstr;
    protected void runDailyReport_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=DailyReport&From=" + Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text) + "&To=" + Yantra.Classes.General.toMMDDYYYY(txtToDate.Text) + "&empid=" + ddlSalesPerson.SelectedValue + "&dep=" + ddlSalesPerson.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindToDoList_All();
    }
    protected void btnListSearch_Click(object sender, EventArgs e)
    {
        BindToDoList_All();
    }
    protected void btnListDelete_Click(object sender, EventArgs e)
    {
        DeleteListReport();
        BindToDoList_All();
    }
    private void DeleteListReport()
    {
        #region Delete Application
        foreach (GridViewRow gvr in gvList.Rows)
        {
            if (((CheckBox)gvr.FindControl("Chk")).Checked)
            {
                try
                {
                    Label onDutyId = (Label)gvr.FindControl("lblId");
                    int ID = Convert.ToInt32(onDutyId.Text);
                    SqlCommand cmd = new SqlCommand("[USP_Delete_TODOLIst]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DailyReport_ID", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show(this, "Data Deleted Suucessfully");
                    BindToDoList_All();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message.ToString());
                }
            }
        }
        #endregion
    }
    protected void btnListUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SM.DailyReport objdr = new SM.DailyReport();
            foreach (GridViewRow gvrow in gvList.Rows)
            {
                DropDownList d1 = gvrow.FindControl("ddlStatus") as DropDownList;
                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    objdr.ID = gvrow.Cells[8].Text;
                    objdr.Status = d1.SelectedItem.Text;
                    objdr.ToDO_List_Status_Update();
                }
            }
            MessageBox.Show(this, "Data Saved Suucessfully");
            BindToDoList_All();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnListSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        objdr.Date = DateTime.Now.ToString();
        objdr.Subject = txtSubject.Text;
        objdr.IssuedDate = Yantra.Classes.General.toMMDDYYYY(txtIssueddt.Text);
        objdr.Description = txtDescr.Text;
        objdr.Status = ddlActivity.SelectedItem.Text;
        objdr.PreparedBy = lblEmpIdHidden.Text;

        if (objdr.ToDo_List_Save() == "Data Saved Successfully")
        {
            //foreach (var hai in Books.DataTextField )
            //{
            //    objdr.EmpID = hai.ToString();
            //    objdr.ToDO_List_Det_Save();
            //}
            //for (int i = 0; i <= Books.Items.Count - 1; i++)
            //{
            //    var selectedText = Books.Items[Books.SelectedIndex].Value.Trim();
            //    objdr.ToDO_List_Det_Save();
            //    //MessageBox.Show(this, selectedText);
            //}
            lblItem.Text = "";
            foreach (ListItem item in Books.Items)
            {
                if (item.Selected)
                {
                   
                   objdr .EmpID =   item.Value.ToString();
                    objdr.ToDO_List_Det_Save();

                }
            }
        }
        MessageBox.Show(this, "Data Saved Suucessfully");
        btnRefresh1_Click(sender, e);
        BindToDoList_All();
    }
    private void BindData()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString()))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT Emp_first_name +' '+Emp_Last_Name as USER_NAME  ,Emp_ID FROM YANTRA_EMPLOYEE_MAST  where status =1 ORDER BY Emp_first_name";
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                // BIND THE SELECT DROP-DOWN LIST WITH A DATABASE TABLE.
                Books.DataSource = dt;
                Books.DataTextField = "USER_NAME";
                Books.DataValueField = "EMP_ID";

                Books.DataBind();

                // FOR MULTIPLE SELECTION. SET THE VALUE AS FALSE, AND SEE WHAT HAPPENS.
                Books.Multiple = true;
            }
        }


        // SM.CustomerMaster.CustomerMaster_Select(Books);


    }
   
}

 
