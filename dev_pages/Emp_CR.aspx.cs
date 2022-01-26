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
using vllib;


public partial class dev_pages_Emp_CR : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string CD = DateTime.Now.ToString("MM/dd/yyyy");
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            Response.Redirect("~/MobileLogin.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gvServiceRequests.DataBind();
            //lblTotalTicketsRaised.Text = gvServiceRequests.Rows.Count.ToString();
            EmployeeMaster_Fill();
            ddlRegion_SelectedIndexChanged(sender, e);
            ddlCreatedFor_SelectedIndexChanged(sender, e);
            ddlPreparedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        }
    }
    private void EmployeeMaster_Fill()
    {
       
        HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
        HR.EmployeeMaster.EmployeeSelect_location(ddlRegion);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlRegion.SelectedItem.Value != "0" && ddlCreatedFor.SelectedItem.Value != "0")
            {
                string tck_ID = "VL-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string status = "Open";

                string Attachment = "";

                Masters.ItemMaster objMaster = new Masters.ItemMaster();
                #region Item Attachment

                if (requestAttachements.HasFiles)
                {
                    if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServiceRequest"))
                    {

                        foreach (HttpPostedFile uploadedFile in requestAttachements.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ServiceRequest/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            //objMaster.Itemattachment = Attachment;
                        }

                    }
                    else
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServiceRequest");
                        foreach (HttpPostedFile uploadedFile in requestAttachements.PostedFiles)
                        {
                            Random rand = new Random();
                            string randNumber = Convert.ToString(rand.Next(0, 10000));
                            string path = Server.MapPath("~/Content/ServiceRequest/");
                            string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                            Attachment = randNumber + "_" + fileName;
                            uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                            //objMaster.Itemattachment = Attachment;
                        }

                    }

                }
                //ddlCreatedFor_SelectedIndexChanged (sender,e);
                #endregion
                DateTime d_Created = Convert.ToDateTime("1900/01/01");
                int i = SaveServiceRequest1(tck_ID, DateTime.Now, status, d_Created, txtDate.Text, ddlUserAffctd.SelectedItem.Text, txtCreatedFor.Text, ddlCreatedFor.SelectedItem.Value, ddlRegion.SelectedItem.Value, txtMobile.Text, txtEmail.Text, txtTime.Text, Attachment, txtSummary.Text, txtDescription.Text, ddlPreparedBy.SelectedItem.Text, txtClientName.Text, txtAddress.Text, txtEmail1.Text);
                //int i = SaveServiceRequest(tck_ID, DateTime.Now, status, d_Created, d_Created, ddlUserAffctd.SelectedItem.Text, txtCreatedFor.Text, ddlRegion.SelectedItem.Text, txtModule.Text, ddlIncidentType.SelectedItem.Text, txtSummary.Text, txtDescription.Text, Attachment, ddlUrgencyLevel.SelectedItem.Text, "");
                if (i > 0)
                {
                    SendMsgToAdmin();

                    btnCancel_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show(this, "Please Select Region and Created for");
            }
        }
        catch (Exception)
        {
            MessageBox.Show(this, "Unable to raise the request, please try again or contact Admin.");
        }
    }

    private void SendMsgToAdmin()
    {
        
        HR.SendSMS objsms = new HR.SendSMS();
        string msfEmp = "New " + ddlUserAffctd.SelectedItem.Text + " is Raised for " + txtSummary.Text + ". Client Details : " + txtClientName.Text + " 0" + txtCreatedFor .Text + " By Executive " + ddlPreparedBy.SelectedItem.Text + ". VALUELINE";
        string MD_MNo = txtMobile.Text;
        objsms.ComptoCC(msfEmp, MD_MNo);
        MessageBox.Show(this, "Your Complaint raised and forwarded to Responsible Person.");
    }
    private int SaveServiceRequest1(string id, DateTime date_Created, string status, DateTime date_Worked, string Date, string userAffc, string createdFor, string Createdid, string region, string mobile, string Email, string incident, string attachment, string summary, string description, string PreparedBy,string ClientName,string Address,string EmailId)
    {
        SqlCommand cmd = new SqlCommand();
        int i = 0;
        try
        {
            con.Close();
            string instr = "insert into emp_TicketRaised_tbl values( " + "'" + id + "'," + "'" + date_Created + "'," + "'" + status + "'," + "'" + date_Worked + "'," + "'" + Date + "'," + "'" + userAffc + "'," + "'" + createdFor + "'," + "'" + Createdid + "'," + "'" + region + "'," + "'" + mobile + "'," + "'" + Email + "'," + "'" + incident + "'," + "'" + attachment + "'," + "'" + summary + "'," + "'" + description + "'," + "'" + PreparedBy + "'," + "'" + ClientName + "'," + "'" + Address +"'," + "'" + EmailId + "'" + ")";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;

            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }
        catch (Exception ex)
        {
            i = 0;
        }
        finally
        {
            con.Close();
            //gvServiceRequests.DataBind();
            //lblTotalTicketsRaised.Text = gvServiceRequests.Rows.Count.ToString();

        }
        return i;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtCreatedFor.Text = txtModule.Text = txtSummary.Text=txtDate .Text=txtTime .Text = txtDescription.Text = txtClientName .Text=txtMobile .Text=txtEmail .Text=txtAddress .Text="";
         ddlRegion.SelectedIndex = ddlUserAffctd.SelectedIndex = 0;
    }

    protected void gvServiceRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "Open")
            {
                e.Row.BackColor = System.Drawing.Color.Bisque;
            }
            if (e.Row.Cells[4].Text == "Closed")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (e.Row.Cells[4].Text == "Working")
            {
                e.Row.BackColor = System.Drawing.Color.LightCyan;
            }

        }
    }
    //protected void ddlIncidentType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlIncidentType.SelectedItem.Text != "Application")
    //    {
    //        MessageBox.Show(this, "You are not authorized to raise this query, please contact admin.");
    //        ddlIncidentType.SelectedIndex = 0;
    //    }
    //}
    protected void ddlCreatedFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlCreatedFor.SelectedItem.Value) > 0)
            {
                //txtFollowupEmail.Text = objHR.EmpEMail;
                //txtFollowupPhoneNo.Text = objHR.EmpMobile;
                //txtCreatedFor.Text = objHR.EmpFirstName;
                //txtEmail.Text = objHR.AssignedEmailId;
                txtMobile.Text = objHR.AssignedMobileNo;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            // HR.Dispose();
        }

    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        HR.EmployeeMaster.EmployeeMaster_Select12(ddlCreatedFor, ddlRegion.SelectedValue );
    }
}