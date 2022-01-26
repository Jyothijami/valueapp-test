using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;
using YantraBLL.Modules;
using System.Data;
using System.Data.SqlClient;
using vllib;

public partial class Modules_Services_Site_Inspection_Report_Details : basePage
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string Client_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        Client_Id = Request.QueryString["ClientID"].ToString();

        if (!IsPostBack)
        {
            if (Client_Id != "New")
            {
                Fill_SiteInspectionDetails();
                lblClientId.Text = Client_Id;
                genearateRandomInspectionId();
                SalesOrder_Fill();

            }
            else if (Client_Id == "New")
            {
                genearateRandomCustNo();
                genearateRandomInspectionId();
                SalesOrder_Fill();
            }
            else
            {
                MessageBox.Show(this, "Please Follow The Flow");
            }
            setControlsVisibility();
        }
    }
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_Select(ddlPONo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "33");
        btnAddVisits.Enabled = up.add;
        //btnRefresh.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;
    }
    private void Fill_SiteInspectionDetails()
    {
        SqlCommand cmd = new SqlCommand("select * from Site_Inspection_Report_tbl where Client_Id ='" + Client_Id + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblClientId.Text = Client_Id;
            txtClientName.Text = dt.Rows[0][1].ToString();
            txtSiteAddress.Text = dt.Rows[0][2].ToString();
            txtSitePlumber.Text = dt.Rows[0][3].ToString();
            txtPlumberMobileNo.Text = dt.Rows[0][4].ToString();
            txtCustomerName.Text = dt.Rows[0][5].ToString();
            txtExecutiveName.Text = dt.Rows[0][6].ToString();
            txtTechnicianName.Text = dt.Rows[0][7].ToString();
            //txtQuoDate.Text = dt.Rows[0][8].ToString();
            txtQuoDate.Text = Convert.ToDateTime(dt.Rows[0][8].ToString()).ToString("dd/MM/yyyy");

            txtPONumber.Text = dt.Rows[0][9].ToString();

            //ddlPONo.SelectedValue = dt.Rows[0][9].ToString();

            txtSiteInchargeName.Text = dt.Rows[0][10].ToString();
            txtArchitectName.Text = dt.Rows[0][11].ToString();
            txtProjectManager.Text = dt.Rows[0][12].ToString();
            txtProManagerNo.Text = dt.Rows[0][13].ToString();

        }

        FillVisitingReportDetails();

    }

    private void FillVisitingReportDetails()
    {
        SqlCommand cmd1 = new SqlCommand("select [Inspection_Id],[Client_Id],CONVERT(VARCHAR(10),[Date_Of_Inspection],103) as Date_Of_Inspection,[Attended_By],[Position],[Visit_Report] from Site_Inspection_Details_tbl where Client_Id ='" + Client_Id + "' ", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        gvVisitDetails.DataSource = dt1;
        gvVisitDetails.DataBind();
    }


    private void genearateRandomCustNo()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        lblClientId.Text = intRandomNumber.ToString();
    }

    private void genearateRandomInspectionId()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        txtVisitingId.Text = intRandomNumber.ToString();
    }

    protected void btnAddVisits_Click(object sender, EventArgs e)
    {

        //if(btnAddVisits.Text == "Update")
        //{
        //    Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
        //    //Services.BeginTransaction();
        //    obj.Client_Id_Details = lblClientId.Text;
        //    obj.Inspection_Id = txtVisitingId.Text;
        //    obj.Date_Of_Inspection = txtDate.Text;
        //    obj.Attended_By = txtAttendedBy.Text;
        //    obj.Position = txtPosition.Text;
        //    obj.Visit_Report = txtVisitReport.Text;
        //    MessageBox.Show(this, obj.UpdateSiteReportDetailsInfo());
        //    //FillVisitingReportDetails();
        //    ClearVisitFields();
        //    genearateRandomInspectionId();
        //    btnAddVisits.Text = "Add";
        //}
        //else if (btnAddVisits.Text == "Add")
        //{


        DataTable VisitReport = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("Inspection_Id");
        VisitReport.Columns.Add(col);

        col = new DataColumn("Attended_By");
        VisitReport.Columns.Add(col);

        col = new DataColumn("Date_Of_Inspection");
        VisitReport.Columns.Add(col);
        col = new DataColumn("Position");
        VisitReport.Columns.Add(col);
        col = new DataColumn("Visit_Report");

        VisitReport.Columns.Add(col);
        if (gvVisitDetails.Rows.Count > 0)
        {

            foreach (GridViewRow gvrow in gvVisitDetails.Rows)
            {
                if (gvVisitDetails.SelectedIndex > -1)
                {

                    if (gvrow.RowIndex == gvVisitDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = VisitReport.NewRow();
                        dr["Inspection_Id"] = txtVisitingId.Text;
                        dr["Attended_By"] = txtAttendedBy.Text;
                        dr["Date_Of_Inspection"] = txtDate.Text;
                        dr["Position"] = txtPosition.Text;
                        dr["Visit_Report"] = txtVisitReport.Text;

                        VisitReport.Rows.Add(dr);
                    }
                    else
                    {
                        LinkButton lnkAttend = (LinkButton)gvrow.FindControl("lbtnAttendedBy");
                        LinkButton lnkVisitId = (LinkButton)gvrow.FindControl("lbtnVisitId");
                        DataRow dr = VisitReport.NewRow();
                        dr["Inspection_Id"] = lnkVisitId.Text;
                        dr["Attended_By"] = lnkAttend.Text;
                        dr["Date_Of_Inspection"] = gvrow.Cells[2].Text;
                        dr["Position"] = gvrow.Cells[3].Text;
                        dr["Visit_Report"] = gvrow.Cells[4].Text;

                        VisitReport.Rows.Add(dr);
                    }
                }
                else
                {
                    LinkButton lnkAttend = (LinkButton)gvrow.FindControl("lbtnAttendedBy");
                    LinkButton lnkVisitId = (LinkButton)gvrow.FindControl("lbtnVisitId");

                    DataRow dr = VisitReport.NewRow();
                    dr["Inspection_Id"] = lnkVisitId.Text;
                    dr["Attended_By"] = lnkAttend.Text;
                    dr["Date_Of_Inspection"] = gvrow.Cells[2].Text;
                    dr["Position"] = gvrow.Cells[3].Text;
                    dr["Visit_Report"] = gvrow.Cells[4].Text;

                    VisitReport.Rows.Add(dr);
                }
            }
        }

        //if (gvVisitDetails.Rows.Count > 0)
        //{
        //    if (gvVisitDetails.SelectedIndex == -1)
        //    {
        //        foreach (GridViewRow gvrow in gvVisitDetails.Rows)
        //        {
        //            LinkButton lnkAttnd = (LinkButton)gvrow.FindControl("lbtnAttendedBy");
        //            if (lnkAttnd.Text == txtAttendedBy.Text)
        //            {
        //                gvVisitDetails.DataSource = VisitReport;
        //                gvVisitDetails.DataBind();
        //                MessageBox.Show(this, "The Attended By Name you have selected is already exists in List");
        //                return;
        //            }
        //        }
        //    }
        //}

        if (gvVisitDetails.SelectedIndex == -1)
        {
            DataRow drnew = VisitReport.NewRow();
            drnew["Inspection_Id"] = txtVisitingId.Text;
            drnew["Attended_By"] = txtAttendedBy.Text;
            drnew["Date_Of_Inspection"] = txtDate.Text;
            drnew["Position"] = txtPosition.Text;
            drnew["Visit_Report"] = txtVisitReport.Text;

            VisitReport.Rows.Add(drnew);
        }
        //else if(gvVisitDetails.SelectedIndex != -1)
        //{
        //    DataRow drnew = VisitReport.NewRow();
        //    drnew["Inspection_Id"] = txtVisitingId.Text;
        //    drnew["Attended_By"] = txtAttendedBy.Text;
        //    drnew["Date_Of_Inspection"] = txtDate.Text;
        //    drnew["Position"] = txtPosition.Text;
        //    drnew["Visit_Report"] = txtVisitReport.Text;

        //    VisitReport.Rows.Add(drnew);
        //}
        gvVisitDetails.DataSource = VisitReport;
        gvVisitDetails.DataBind();
        ClearVisitFields();
        genearateRandomInspectionId();
        // }

    }

    private void ClearVisitFields()
    {
        txtDate.Text = txtAttendedBy.Text = txtPosition.Text = txtVisitReport.Text = txtVisitingId.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveSiteReportDetails();

        ClearAllFields();
    }
    private void ClearAllFields()
    {
        txtClientName.Text = txtQuoDate.Text = txtCustomerName.Text = txtPONumber.Text = txtExecutiveName.Text = txtTechnicianName.Text = txtSitePlumber.Text = txtPlumberMobileNo.Text = txtSiteInchargeName.Text = txtArchitectName.Text = txtProjectManager.Text = txtProManagerNo.Text = txtSiteAddress.Text = "";
        ddlPONo.SelectedIndex = 0;

        gvVisitDetails.DataSource = null;
        gvVisitDetails.DataBind();
        ClearVisitFields();
    }
    private void SaveSiteReportDetails()
    {
        Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
        obj.Client_Id = lblClientId.Text;
        obj.Client_Name = txtClientName.Text;
        obj.Quotation_Date = Yantra.Classes.General.toMMDDYYYY(txtQuoDate.Text);
        obj.Customer_Name = txtCustomerName.Text;
        obj.PO_Number = txtPONumber.Text;
        //obj.PO_Number = ddlPONo.SelectedItem.Text;

        obj.Executive_Name = txtExecutiveName.Text;
        obj.Technician_Name = txtTechnicianName.Text;
        obj.Site_Plumber_Name = txtSitePlumber.Text;
        obj.Plumber_Mobile_No = txtPlumberMobileNo.Text;

        obj.Site_Incharge_Mobile_No = txtSiteInchargeName.Text;
        obj.Architecture_Name = txtArchitectName.Text;
        obj.Project_Manager_Name = txtProjectManager.Text;
        obj.Mobile_No = txtProManagerNo.Text;
        obj.Site_Address = txtSiteAddress.Text;


        if (Client_Id != "New")
        {
            obj.UpdateSiteReportInfo();
            obj.DeleteSiteReportDetailsInfo(lblClientId.Text);
            SaveVisitDetails();
            Response.Redirect("SiteInspection_Report.aspx");
        }
        else
        {
            obj.InsertSiteReportInfo();
            SaveVisitDetails();
            MessageBox.Show(this, "Data Saved Successfully");
            Response.Redirect("SiteInspection_Report.aspx");
        }
    }

    private void SaveVisitDetails()
    {
        if (gvVisitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvVisitDetails.Rows)
            {

                LinkButton lnkVisitId = (LinkButton)gvrow.FindControl("lbtnVisitId");
                LinkButton lbtnAttendedBy = (LinkButton)gvrow.FindControl("lbtnAttendedBy");

                string Date_Of_Inspection = Yantra.Classes.General.toMMDDYYYY(gvrow.Cells[2].Text.ToString());
                string Position = gvrow.Cells[3].Text.ToString();
                string Visit_Report = gvrow.Cells[4].Text.ToString();

                Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
                //Services.BeginTransaction();
                obj.Client_Id_Details = lblClientId.Text;
                obj.Inspection_Id = lnkVisitId.Text;
                obj.Date_Of_Inspection = Date_Of_Inspection;
                obj.Attended_By = lbtnAttendedBy.Text;
                obj.Position = Position;
                obj.Visit_Report = Visit_Report;
                MessageBox.Show(this, obj.InsertSiteReportDetailsInfo());

            }
        }

        else
        {
            MessageBox.Show(this, "Please Provide Site Visiting Details");
            ClearAllFields();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearVisitFields();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnVisitId_Click(object sender, EventArgs e)
    {
        txtVisitingId.Text = "";
        LinkButton lbtnvisit = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnvisit.Parent.Parent;
        gvVisitDetails.SelectedIndex = gvRow.RowIndex;

        LinkButton lnkVisitId = (LinkButton)gvRow.FindControl("lbtnVisitId");
        LinkButton lbtnAttendedBy = (LinkButton)gvRow.FindControl("lbtnAttendedBy");
        string Date_Of_Inspection = gvVisitDetails.SelectedRow.Cells[2].Text;
        string Position = gvVisitDetails.SelectedRow.Cells[3].Text;
        string Visit_Report = gvVisitDetails.SelectedRow.Cells[4].Text;
        txtVisitingId.Text = lnkVisitId.Text;
        txtDate.Text = Date_Of_Inspection;
        txtAttendedBy.Text = lbtnAttendedBy.Text;
        txtPosition.Text = Position;
        txtVisitReport.Text = Visit_Report;
        btnAddVisits.Text = "Update";
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SiteInspection&ClientID=" + Client_Id + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Services.ServiceCustInfo objPO = new Services.ServiceCustInfo();
            if (objPO.CustomerMaster_SelelctPONo(ddlPONo.SelectedItem.Value) > 0)
            {
                txtClientName.Text = objPO.SOCustName;
                txtCustomerName.Text = objPO.SOCompanyName;
                txtProjectManager.Text = objPO.SOContactPerson;
                txtSiteInchargeName.Text = objPO.SOMobileNO;
                txtSiteAddress.Text = objPO.SOAddress;
                txtPONumber.Text = ddlPONo.SelectedItem.Text;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            // btnDelete.Attributes.Clear();
            SM.Dispose();
        }
    }
}

