using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using iTextSharp.text;


public partial class Modules_HR_Emp_Attendance : basePage
{
    DataTable dt = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //GridView1.DataBind();
            //BindAttendence();
            BindEmpAttendanceGrid();

        }
    }
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReport.SelectedItem.Text == "Total Time")
        {
            
            SqlCommand cmd = new SqlCommand("[Usp_GetDailyAttendence_TotalTime_Bio]", con);
            cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gdvAttendence.DataSource = dt;
            gdvAttendence.DataBind();
            gdvAttendence.Visible = true;
        }
        else if (ddlReport.SelectedItem.Text == "In Time")
        {
            SqlCommand cmd = new SqlCommand("[Usp_GetDailyAttendence_InTime_Bio]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gdvAttendence.DataSource = dt;
            gdvAttendence.DataBind();
            gdvAttendence.Visible = true;

        }
        else if (ddlReport.SelectedItem.Text == "Out Time")
        {
            SqlCommand cmd = new SqlCommand("[Usp_GetDailyAttendence_OutTime_Bio]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gdvAttendence.DataSource = dt;
            gdvAttendence.DataBind();
            gdvAttendence.Visible = true;

        }
        else
        {
            gdvAttendence.Visible = false;
        }
    }
    private void BindAttendence()
    {
        if (txtEmpName.Text == "")
        {
            if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("[Usp_GetDailyAttendence_TotalTime_Bio]", con);
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex != 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence_Location", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("[Usp_GetDailyAttendence_TotalTime_Bio]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtInTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("[Usp_GetDailyAttendence_InTime_Bio]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtOutTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetTtlDailyAttendence_OutTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text != "" && txtInTime.Text != "" || txtTotalTime.Text != "" && txtOutTime.Text != "" || txtOutTime.Text != "" && txtInTime.Text != "")
            {
                MessageBox.Show(this, "Please Provide Only One Search Criteria at a time");
            }

        }
        else
        {
            if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex == 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            if (txtTotalTime.Text == "" && txtInTime.Text == "" && txtOutTime.Text == "" && ddlLoc.SelectedIndex != 0)
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_Location", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }

            else if (txtTotalTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_TotalTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtInTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_InTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtOutTime.Text != "")
            {
                SqlCommand cmd = new SqlCommand("Usp_GetDailyAttendence_OutTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Location", ddlLoc.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gdvAttendence.DataSource = dt;
                gdvAttendence.DataBind();
            }
            else if (txtTotalTime.Text != "" && txtInTime.Text != "" || txtTotalTime.Text != "" && txtOutTime.Text != "" || txtOutTime.Text != "" && txtInTime.Text != "")
            {
                MessageBox.Show(this, "Please Provide Only One Search Criteria at a time");
            }

        }
    }
    private void BindLateEmpAttendanceGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_AttendanceManagement_Late]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (txtEmpName.Text != "")
        {
            cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    private void BindEmpAttendanceGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_AttendanceManagement_Alumil]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if ( txtEmpName.Text != "")
        {
            cmd.Parameters.AddWithValue("@EMP_First_Name", txtEmpName.Text);
        }
        if ( txtFromDate.Text!= "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if ( txtToDate.Text!= "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        if (ddlReportType.SelectedItem.Text == "Late Arrival")
        {
            cmd.Parameters.AddWithValue("@Late", ddlReportType.SelectedItem.Text);


    //        General.GridBindwithCommand(GridView1, " select Emp_code ,EMP_FIRST_NAME +' '+EMP_LAST_NAME as Emp_Name ,DEPT_NAME as Department ,CONVERT(nvarchar(50),ldt,103)as Att_Date,Min(CONVERT(nvarchar(50),ldt,8)) as Intime " +
    //" ,case  "+
    //" when Min(CONVERT(nvarchar(50),ldt,8)) = '10:00:00'   then 'Grace' "+
    // " when Min(CONVERT(nvarchar(50),ldt,8))<='10:00:00' then 'Early '+ "+
    //" convert(varchar(5),abs(DateDiff(s, MIN(parallel_Add.Ldt) , min (CONVERT(DATETIME, CONVERT(date, ldt)) + '10:00:00'))/3600))+':'+   "+
    //" convert(varchar(5),abs(DateDiff(s, MIN(parallel_Add.Ldt),min (CONVERT(DATETIME, CONVERT(date, ldt)) + '10:00:00') )%3600/60))  "+
	 
    //" when   "+
    //" Min(CONVERT(nvarchar(50),ldt,8))>='10:00:00' then 'Late '+  "+
    //" convert(varchar(5),abs(DateDiff(s, MIN(parallel_Add.Ldt) , min (CONVERT(DATETIME, CONVERT(date, ldt)) + '10:00:00'))/3600))+':'+  "+
    //" convert(varchar(5),abs(DateDiff(s, MIN(parallel_Add.Ldt),min (CONVERT(DATETIME, CONVERT(date, ldt)) + '10:00:00') )%3600/60))   "+
    //" end as InRemark  "+
    //" ,MAX(CONVERT(nvarchar(50),ldt,8)) as OutTime,  "+
    //" convert(varchar(5),DateDiff(s, MIN(parallel_Add.Ldt) , MAX(parallel_Add.Ldt))/3600)+':'+convert(varchar(5),DateDiff(s, MIN(parallel_Add.Ldt),MAX(parallel_Add.Ldt) )%3600/60) as TotalTime   "+
    //" ,CP_SHORT_NAME  "+
	
    //" from parallel_Add  "+
    //" left outer join YANTRA_EMPLOYEE_MAST on parallel_Add .Emp_code =YANTRA_EMPLOYEE_MAST .ASSIGNED_EMPID "+
    //" left outer join YANTRA_EMPLOYEE_DET on YANTRA_EMPLOYEE_MAST .EMP_ID =YANTRA_EMPLOYEE_DET .EMP_ID  "+
    //" left outer join YANTRA_DEPT_MAST on YANTRA_EMPLOYEE_DET .DEPT_ID =YANTRA_DEPT_MAST .DEPT_ID  "+
    //" left outer join YANTRA_COMP_PROFILE on YANTRA_EMPLOYEE_MAST .COMPANY_ID =YANTRA_COMP_PROFILE .CP_ID  "+
	
    // " group by Emp_code,CONVERT(nvarchar(50),ldt,103),EMP_FIRST_NAME ,EMP_LAST_NAME,DEPT_NAME,CP_SHORT_NAME  "+
    //"  having Min(CONVERT(nvarchar(50),ldt,8))>='10:05:60'  "+
	 
    //" order by EMP_FIRST_NAME");



        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filename = FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath(filename));
            ExportToGrid(Server.MapPath(filename));
            //GridView1.DataBind();
            BindEmpAttendanceGrid();
        }
        else
        {
            MessageBox.Show(this, "Please Select A Location & File To Upload");
        }
    }

    void ExportToGrid(String path)
    {
        OleDbConnection MyConnection = null;
        DataSet DtSet = null;
        OleDbDataAdapter MyCommand = null;
        MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0;");


        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
        DtSet = new DataSet();
        MyCommand.Fill(DtSet, "[Sheet1$]");
        dt = DtSet.Tables[0];
        MyConnection.Close();
        General obj = new General();
        if (dt.Rows.Count > 0)
        {
            
            foreach (DataRow dr in dt.Rows)
            {
                string sql,Outtime,Totaltime,Intime;
                if (dr.ItemArray[0].ToString() != "")
                {
                    if (dr.ItemArray[5].ToString() != "")
                    {
                        Intime = Convert.ToDateTime(dr.ItemArray[5].ToString()).ToString("HH:mm:ss");
                    }
                    else
                    {
                        Intime = "";
                    }
                    if (dr.ItemArray[7].ToString() != "")
                    {
                        Outtime = Convert.ToDateTime(dr.ItemArray[7].ToString()).ToString("HH:mm:ss");
                    }
                    else
                    {
                        Outtime = "";
                    }
                    if (dr.ItemArray[9].ToString() != "")
                    {
                        Totaltime = Convert.ToDateTime(dr.ItemArray[9].ToString()).ToString("HH:mm:ss");
                    }
                    else
                    {
                        Totaltime = "";
                    }
                    HR.EmployeeMaster hai = new HR.EmployeeMaster();
                    if (hai.RecordExists(dr.ItemArray[0].ToString(), Convert.ToDateTime(dr.ItemArray[3].ToString()).ToString("MM/dd/yyyy")) == "0")
                    {

                        sql = @"insert into Emp_Attendance (Emp_Code,Emp_Name,Department,Att_Date,Shift,Intime,InRemark,Outtime,OutRemark,Totaltime,Last_Updated,Location)  values 
                        ('" + dr.ItemArray[0].ToString() + "','" + dr.ItemArray[1].ToString() + "','" + dr.ItemArray[2].ToString() + "','" + Convert.ToDateTime(dr.ItemArray[3].ToString()).ToString("MM/dd/yyyy") + "','" + dr.ItemArray[4].ToString() + "','" + Intime + "','" + dr.ItemArray[6].ToString() + "','" + Outtime + "','" + dr.ItemArray[8].ToString() + "','" + Totaltime + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + ddlLocation.SelectedItem.Text + "' ) ";
                        obj.ReturnExecuteNoneQuery(sql);
                    }
                    else
                    {
                        sql = @"update Emp_Attendance set Intime = '" + Intime + "',InRemark = '" + dr.ItemArray[6].ToString() + "', Outtime ='" + Outtime + "',OutRemark ='" + dr.ItemArray[8].ToString() + "',Totaltime ='" + Totaltime + "',Last_Updated ='" + DateTime.Now.ToString("MM/dd/yyyy") + "' ,Location='"+ddlLocation.SelectedItem.Text+"'   where Emp_Code  ='" + dr.ItemArray[0].ToString() + "' ";
                        obj.ReturnExecuteNoneQuery(sql);
                    }

                    MessageBox.Show(this, "Data Inserted Successfully");

                }

            }

        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "10:00:00")
            {
                e.Row.Cells[5].Text = "Grace";
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BindAttendence();
        BindEmpAttendanceGrid();
    }
    //protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindAttendence();
    //}

    protected void btnTotalGo_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtTotalTime.Text = "";
        txtInTime.Text = "";
        txtOutTime.Text = "";
        
    }
    protected void btnInGo_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtInTime.Text = "";
        txtOutTime.Text = "";
        txtTotalTime.Text = "";

    }
    protected void btnOutGo_Click(object sender, EventArgs e)
    {
        BindAttendence();
        txtOutTime.Text = "";
        txtInTime.Text = "";
        txtTotalTime.Text = "";

    }
    protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAttendence();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindEmpAttendanceGrid();
    }

    protected void btnExprot_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventorMRNyReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                BindEmpAttendanceGrid();
                //GridView1.HeaderRow.BackColor = Color.WHITE;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (gdvAttendence.Rows.Count > 0)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventorMRNyReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gdvAttendence.AllowPaging = false;
                BindEmpAttendanceGrid();
                //gdvAttendence.HeaderRow.BackColor = Color.WHITE;
                foreach (TableCell cell in gdvAttendence.HeaderRow.Cells)
                {
                    cell.BackColor = gdvAttendence.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gdvAttendence.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gdvAttendence.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gdvAttendence.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gdvAttendence.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }
   
}