using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data;
using Yantra.Classes;
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
public partial class CRTech : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
        lblCRIDHidden.Text = Request.QueryString["CID"];
        
        
        if (Request.QueryString["CID"] != null)
        {
            try 
            {
                if (objComplaintRegister.CompRegister_Select(Request.QueryString["CID"]) > 0)
                {
                    lblCRNo.Text = objComplaintRegister.CRNo;
                    lblCustName.Text = objComplaintRegister.CustName;
                    lblCustMobile.Text = objComplaintRegister.CustCorpMobile;
                    lblCustAdd.Text = objComplaintRegister.CustUnitAddress;
                    lblStartOtp.Text = objComplaintRegister.CRStartOTP;
                    lblEndOTP.Text = objComplaintRegister.CRClosedOTP;
                    lblcalltype.Text = objComplaintRegister.CRCallType;
                    lblDetId.Text = objComplaintRegister.CRDetID;
                    lblItemCode.Text = objComplaintRegister.ItemCode;
                    lblNOC.Text = objComplaintRegister.CRComplaintNature;
                    if (objComplaintRegister.CRINTIME != "1/1/1900 12:00:00 AM")
                    {
                        lblstatus.Visible = true;
                        ddlstatus.Visible = true;
                        lblOtp.Visible = false;
                        txtOTP.Visible = false;
                        Button1.Visible = false;
                        lblRemarks.Visible = true;
                        Uploadattach.Visible = true;
                        ibtmImage.Visible = true;
                        
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
    private void Refresh()
    {
        
        btnStatus.Text = "Enter OTP";
        //txtComments.Text = string.Empty;
        lblOtp.Text = "ENTER START OTP";
        txtOTP.Text = string.Empty;
        ddlstatus.Visible = false;
        lblstatus.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        if (lblOtp.Text == "Enter Start OTP")
        {
            if (txtOTP.Text != "")
            {
                if (txtOTP.Text == lblStartOtp.Text)
                {
                    lblstartDt.Text = DateTime.Now.ToString();
                    string _command = "update YANTRA_COMPLAINT_REGISTER_DET set Tech_StartDt='" + lblstartDt.Text + "' , Tech_EndDt='" + lblstartDt.Text + "' where CR_DET_ID='" + lblDetId.Text + "'";
                    SqlCommand cmd = new SqlCommand(_command, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    i = i + cmd.ExecuteNonQuery();
                    con.Close();
                    lblstatus.Visible = true;
                    ddlstatus.Visible = true;
                    lblOtp.Visible = false;
                    txtOTP.Visible = false;
                    lblRemarks.Visible = false;
                    //txtComments.Visible = false;
                    Button1.Visible = false;
                    txtOTP.Text = string.Empty;
                    if (gvcomDet.Rows.Count > 0)
                    {

                    }
                }
                else
                {
                    MessageBox.Show(this, "Incorrect OTP ");
                }
            }
            else
            {
                MessageBox.Show(this, "Please enter Otp ");
            }
        }
        else if (lblOtp.Text == "Enter Closed OTP")
        {
            if (txtOTP.Text != "")
            {
                if (txtOTP.Text == lblEndOTP.Text)
                {
                    lblEndDt.Text = DateTime.Now.ToString();

                    string _command = "update YANTRA_COMPLAINT_REGISTER_DET set Tech_EndDt='" + lblEndDt.Text + "' , TECH_STATUS='" + ddlstatus.SelectedValue + "' , TECH_REMARKS='" + 0 + "' where CR_DET_ID='" + lblDetId.Text + "'";
                    SqlCommand cmd = new SqlCommand(_command, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    i = i + cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        MessageBox.Show(this, "Task Status Updated");
                    }
                    lblstatus.Visible = false ;
                    ddlstatus.Visible = false ;
                    lblOtp.Visible = false;
                    txtOTP.Visible = false;
                    Button1.Visible = false;
                    txtOTP.Text = string.Empty;
                    lblRemarks.Visible = false;
                    btnStatus.Visible = false;

                    //txtComments.Visible = false;
                }
                else
                {
                    MessageBox.Show(this, "Incorrect OTP ");
                }
            }
            else
            {
                MessageBox.Show(this, "Please enter Otp ");
            }
        }
        else if (lblOtp.Text == "Enter Pending OTP")
        {
            if (txtOTP.Text != "")
            {
                if (txtOTP.Text == lblEndOTP.Text)
                {
                    lblstartDt.Text = DateTime.Now.ToString();
                    lblEndDt.Text = DateTime.Now.ToString();
                    string _command = "update YANTRA_COMPLAINT_REGISTER_DET set Tech_EndDt='" + lblEndDt.Text + "' , TECH_STATUS='" + ddlstatus.SelectedValue + "' , TECH_REMARKS='" + 0 + "' where  CR_DET_ID='" + lblDetId.Text + "'";
                    SqlCommand cmd = new SqlCommand(_command, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    i = i + cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        MessageBox.Show(this, "Task Status Updated");
                    }
                    lblstatus.Visible = true;
                    ddlstatus.Visible = true;
                    lblOtp.Visible = false;
                    txtOTP.Visible = false;
                    Button1.Visible = false;
                    txtOTP.Text = string.Empty;
                    lblRemarks.Visible = false;
                    //txtComments.Visible = false;

                    Refresh();
                }
                else
                {
                    MessageBox.Show(this, "Incorrect OTP ");
                }
            }
            else
            {
                MessageBox.Show(this, "Please enter Otp ");
            }
        }
        
    }
    string pagenavigationstr;
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedItem.Text == "Pending")
        {
            //lblRemarks.Visible = true;
            //txtComments.Visible = true;
            lblOtp.Visible = false ;
            txtOTP.Visible = false ;
            btnStatus .Text ="Request Pending OTP";
            Button1.Visible = false;
                btnStatus .Visible =true ;
                lblRemarks.Visible = true;
                Uploadattach.Visible = true;
                ibtmImage.Visible = true;
                UploadsRepeater.Visible = true;
        }
        else if (ddlstatus.SelectedItem.Text == "Closed")
        {
            //lblRemarks.Visible = true;
            //txtComments.Visible = true;
            Button1.Visible = false;
            btnStatus.Text = "Request Closed OTP";
                btnStatus .Visible =true ;
                lblOtp.Visible = false ;
                txtOTP.Visible = false ;
                lblRemarks.Visible = true;
                Uploadattach.Visible = true;
                ibtmImage.Visible = true;
                UploadsRepeater.Visible = true;
        }
    }
    protected void lbtnFileOpener_Click(object sender, EventArgs e)
    {
        LinkButton lbtnFileOpener;
        lbtnFileOpener = (LinkButton)sender;
        Repeater gvRow = (Repeater)lbtnFileOpener.Parent.Parent;
        DBManager dbcon = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
        string command = "SELECT REG_ATTACHMENT FROM [YANTRA_COMPLAINT_REGISTER_ATTACHMENTS] WHERE CR_ID=" + Request.QueryString["CID"] + " AND REG_ATTACHMENT='" + lbtnFileOpener.Text + "'";
        dbcon.Open();
        string filename = dbcon.ExecuteScalar(CommandType.Text, command).ToString();
        string path = "../../Content/ServicesComplaintRegisterAttachments/" + filename;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('" + path + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "DocWin", "window.open('SOFileOpen.aspx?soid=" + lblSOIdHidden.Text + "&fn=" + lbtnFileOpener.Text + "','DocWin1','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes');", true);
    }
    private void sendMsgToCust()
    {
        try
        {
            HR.SendSMS objsms = new HR.SendSMS();
        //    pagenavigationstr = "http://183.82.108.55/r.aspx?CRID=" + lblCRIDHidden.Text + "";
            string msg = "Dear Customer, your " + lblcalltype.Text + " " + lblCRNo.Text + " has been closed. Please share OTP - "
                + lblEndOTP.Text + " with out Tech. VALUELINE";
            string TechNo = lblCustMobile.Text;
            objsms.Send_App_SMS_CloseOTP(msg, TechNo);
        }
        catch (Exception ex) { }
    }
    private void SendPendingMsg()
    {
        try
        {
            HR.SendSMS objsms = new HR.SendSMS();

            string msg = "Dear Customer, " + lblcalltype.Text + " " + lblCRNo.Text + " is pending for now. We ll resolve the issue shortly. Please share OTP - "
                + lblEndOTP.Text + " with our Tech. VALUELINE";
            string TechNo = lblCustMobile.Text;
            objsms.Send_App_SMS_PendingOTP(msg, TechNo);
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        if (btnStatus.Text == "Request Closed OTP")
        {
            btnStatus.Text = "Request Closed OTP";
            //btnStatus.Visible = false;
            lblOtp.Visible = true;
            lblOtp.Text = "Enter Closed OTP";
            txtOTP.Visible = true;
            Button1.Visible = true ;
            sendMsgToCust();
        }
        else if (btnStatus.Text == "Request Pending OTP")
        {
            btnStatus.Text = "Request Pending OTP";
            btnStatus.Visible = false;
            lblOtp.Visible = true;
            lblOtp.Text = "Enter Pending OTP";
            Button1.Visible = true;
            txtOTP.Visible = true;
            SendPendingMsg();
        }
        
    }
    protected void ibtmImage_Click(object sender, ImageClickEventArgs e)
    {
        #region  Attachment
        Services.ComplaintRegister objComplaintRegister = new Services.ComplaintRegister();
        if (Uploadattach.HasFiles)
        {

            string Attachment = "";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServicesComplaintRegisterAttachments"))
            {

                foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                {
                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 10000));
                    string path = Server.MapPath("~/Content/ServicesComplaintRegisterAttachments/");
                    string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);
                    objComplaintRegister.CRId = Request.QueryString["CRID"];
                    Attachment = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    objComplaintRegister.RegAttachments = Attachment;
                    objComplaintRegister.ComplaintRegisterAttachment_Save();

                }

            }
            else
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Content/ServicesComplaintRegisterAttachments");
                foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                {
                    Random rand = new Random();
                    string randNumber = Convert.ToString(rand.Next(0, 10000));
                    string path = Server.MapPath("~/Content/ServicesComplaintRegisterAttachments/");
                    string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                    Attachment = randNumber + "_" + fileName;
                    uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                    objComplaintRegister.RegAttachments = Attachment;
                    objComplaintRegister.ComplaintRegisterAttachment_Save();

                }

            }

        #endregion
        }
        UploadsRepeater.DataBind();
    }
    protected void lbtnCRNo_Click(object sender, EventArgs e)
    {
        LinkButton lbtnCRNo;
        lbtnCRNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCRNo.Parent.Parent;
        gvcomDet.SelectedIndex = gvRow.RowIndex;
    }
    protected void gvcomDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
          {
             e.Row.Cells[0].Visible = false;
             e.Row.Cells[1].Visible = false;
          }
    }
}