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
using System.IO;
using System.Data.SqlClient;
using System.Text;
using vllib;
using YantraDAL;
using YantraBLL.Modules;
public partial class Modules_HR_EmpData : System.Web.UI.Page
{
    //SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter sda;
    DataTable dt;
    string EmpId1,ConString, CmdString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        ConString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
       
        gvEmployeeMaster.DataBind();
    }
    protected int slnoforMaingrid = 1;
    protected void gvEmployeeMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            //e.Row.Cells[10].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString(gvEmployeeMaster.PageIndex * gvEmployeeMaster.PageSize + slnoforMaingrid);
            slnoforMaingrid++;
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
        }

    }
    private DataTable SelectData(string sqlQuery)
    {
        string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, connectionString))
        {
            DataTable dt = new DataTable("Emp_Documents_Submitted");
            sqlDataAdapter.Fill(dt);
            return dt;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=1.xls");
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        System.Web.UI.Page sPage = new System.Web.UI.Page();
        sPage.RenderControl(htmlWrite);
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.Flush();

    }
    

    protected void gvempdoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton bts = e.CommandSource as ImageButton;

        if (e.CommandName.Equals("Save"))
        {
            Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
            int rowindex = int.Parse(e.CommandArgument.ToString().Trim());
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            FileUpload FileUpload1 = bts.Parent.Parent.FindControl("fileupload1") as FileUpload;
            Label lbldocid1=bts.Parent.Parent.FindControl("lbldocid1") as Label;
            if (FileUpload1.HasFile)
            {
                string Attachment = "";

                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments");
                foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                {
                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 10000));
                    string path = Server.MapPath("~/Content/EmployeeDocuments/");
                    string fileName = System.IO.Path.GetFileName(FileUpload1.FileName);

                    Attachment = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    objComplaintRegister.docid = lbldocid1.Text;
                    objComplaintRegister.docsub = Attachment;
                    objComplaintRegister.date = DateTime.Now.ToString();
                    objComplaintRegister.empid = lblempid.Text;
                    if (rdbDocType.SelectedValue  =="")
                    {
                        objComplaintRegister.empdocid = "0";
                    }
                    else
                    {
                        objComplaintRegister.empdocid = rdbDocType.SelectedItem.Value;
                    }
                    objComplaintRegister.EmpDoc_Update();
                    gvEmployeeMaster_RowCommand(sender,  e);
                }
            }
            
        }
    }

    protected void Display(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        string EmpID = row.Cells[2].Text;
        EmpId1 = EmpID;
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from Emp_Documents_Submitted a inner join YANTRA_EMP_DOCUMENTS_SUBMITTED b on a.EMP_Id =b.EMP_ID inner join YANTRA_EMPLOYEE_MAST c on a.EMP_Id =c.EMP_ID where a.EMP_Id =" + EmpID + "", con);
        //cmd.CommandType = CommandType.Text;
        SqlDataReader dr = cmd.ExecuteReader();
        gvempdoc.DataSource = dr;
        gvempdoc.DataBind();
        con.Close();
        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "myModal();", true);
    }
    protected void gvEmployeeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowPopup")
        {
            LinkButton btndetails = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btndetails.NamingContainer;
            //LinkButton lbtnSalesOrderNo;
            //lbtnSalesOrderNo = (LinkButton)sender;
            //GridViewRow row = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
            string EmpID = row.Cells[2].Text;
            lblempid.Text = EmpID;
            con.Open();
            SqlCommand cmd = new SqlCommand("select  a.doc_id,c.EMP_FIRST_NAME ,a.Document_Submitted ,a.emp_doc_id ,a.Date_Submitted,a.emp_ID from Emp_Documents_Submitted a inner join YANTRA_EMPLOYEE_MAST c on a.EMP_Id =c.EMP_ID where a.EMP_Id =" + EmpID + "", con);
            //cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            gvempdoc.DataSource = dr;
           
            gvempdoc.DataBind();
            con.Close();
            Popup(true);
        }
    }
    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }
    protected void ibtmImage_Click(object sender, ImageClickEventArgs e)
    {
        Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
        if (Uploadattach.HasFiles)
        {
            string Attachment = "";
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/EmployeeDocuments");
            foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
            {
                Random rand = new Random();
                string randNumber = Convert.ToString(rand.Next(0, 10000));
                string path = Server.MapPath("~/Content/EmployeeDocuments/");
                string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                Attachment = randNumber + "_" + fileName;
                uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                objComplaintRegister.docsub = Attachment;
                objComplaintRegister.date = DateTime.Now.ToString();
                objComplaintRegister.empid = lblempid.Text ;
                objComplaintRegister.empdocid = rdbDocType.SelectedItem.Value;
                objComplaintRegister.EmpDoc_Save();
            }
        }
    }
    protected void gvempdoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
}
