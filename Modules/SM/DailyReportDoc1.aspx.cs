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
using System.Globalization;
using Yantra.Classes;

public partial class Modules_SM_DailyReportDoc1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Qid = Request.QueryString["Cid"].ToString();
        if (!IsPostBack)
        {
            SM.DailyReport obj = new SM.DailyReport();

            if (obj.DailyReport_Select(Request.QueryString["Cid"].ToString()) > 0)
            {

                txtClientsName.Text = obj.CustName;
                txtPhoneNo.Text = obj.Phone;
            }
            txtfloorplanreceiveddate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            General.GridBindwithCommand(gvFloorPlan, "select * from YANTRA_DAILY_REPORT_DOCUMENTS where DAILYREPORTID= '" + Request.QueryString["Cid"].ToString() + "' order by ISSUEDDATE desc");

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SM.DailyReport objSM = new SM.DailyReport();
            foreach (HttpPostedFile uploadfile in FileUpload2.PostedFiles)
            {
                string Attachment = "";
                objSM.IssuedDate = General.toMMDDYYYY(txtfloorplanreceiveddate.Text);
                objSM.Remarks = txtfloorplanremarks.Text;
                objSM.DRId = Request.QueryString["Cid"].ToString();

                Random rand = new Random();
                string randNumber = Convert.ToString(rand.Next(0, 10000));
                string path = Server.MapPath("~/Content/FloorPlanDrawings/");
                string fileName = System.IO.Path.GetFileName(uploadfile.FileName);

                Attachment = randNumber + "_" + fileName;
                uploadfile.SaveAs(path + randNumber + "_" + fileName);
                objSM.FileName = Attachment;
                objSM.DRDOC_Save();
                MessageBox.Show(this, "Data Saved Successfully");
               
            }
        }
        catch (Exception ex) { }
        finally
        {
            txtfloorplanremarks.Text = "";
            gvFloorPlan.DataBind();
            General.GridBindwithCommand(gvFloorPlan, "select * from YANTRA_DAILY_REPORT_DOCUMENTS where DAILYREPORTID= '" + Request.QueryString["Cid"].ToString() + "'");

            //  SM.Dispose();
            //Response.Redirect("~/Modules/Sales/SalesEnquiry.aspx");
        }
    }
    protected void lbtnFloorDetails_Click(object sender, EventArgs e)
    {
        LinkButton lbtnRegionalMaster;
        lbtnRegionalMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnRegionalMaster.Parent.Parent;
        gvFloorPlan.SelectedIndex = gvRow.RowIndex;

        if (gvFloorPlan.SelectedIndex > -1)
        {
            try
            {
                SM.DailyReport objSM = new SM.DailyReport();
                MessageBox.Show(this, objSM.DRDocDetails_Delete(gvFloorPlan.SelectedRow.Cells[0].Text));
                // MessageBox.Show(this, "Data Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {


                gvFloorPlan.DataBind();
                General.GridBindwithCommand(gvFloorPlan, "select * from YANTRA_DAILY_REPORT_DOCUMENTS where DAILYREPORTID= '" + Request.QueryString["Cid"].ToString() + "'");

            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
}