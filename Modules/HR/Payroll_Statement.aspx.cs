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
using Yantra.Classes;
using vllib;
using System.IO;
using System.Drawing;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
public partial class Modules_HR_Payroll : basePage
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    double amt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindYears();
            CompFill();
        }
    }
    private void BindYears()
    {
        int year = DateTime.Now.Year;
        for (int i = year; i >= year - 4; i--)
        {
            ddlYear.Items.Add(i.ToString());
        }
    }
    private void CompFill()
    {
        try
        {
            HR.Company_Select(ddlComp);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void btnCalcl_Click(object sender, EventArgs e)
    {
        BindPayrollGrid();
        btnExport.Visible = true;
        //btnExportPDF.Visible = true;
        lblYear.Text = ddlYear.SelectedItem.Value;
        lblMonth.Text = ddlMonth.SelectedItem.Text;
    }
    private void BindPayrollGrid()
    {
        if (ddlMonth.SelectedIndex > 0)
        {
            HR.EmployeeMaster obj = new HR.EmployeeMaster();
            obj.EmpPayroll_Select1(ddlComp.SelectedItem.Value, ddlYear.SelectedItem.Value, ddlMonth.SelectedItem.Value, ddlLocation.SelectedItem.Value, gvPayroll);

            //obj.EmpPayroll_Select1(ddlYear.SelectedItem.Value, ddlMonth.SelectedItem.Value, ddlComp.SelectedItem.Value, ddlLocation.SelectedItem.Value, gvPayroll);
            //if (obj.EmpPayroll_Select1(ddlYear.SelectedItem.Value, ddlMonth.SelectedItem.Value, ddlComp.SelectedItem.Value,ddlLocation .SelectedItem .Value,  gvPayroll) )
            //{
            //    foreach (GridViewRow row in gvPayroll.Rows)
            //    {
            //        Label Paid = (Label)row.FindControl("lblPaid");
            //        Paid.Text = obj.Paid;
            //        Label NOD = (Label)row.FindControl("lblNOD");
            //        NOD.Text = obj.TotalNOD;
            //        Label Basic = (Label)row.FindControl("lblBasic");
            //        Basic.Text = Math.Round(((Convert.ToDecimal(obj.Basic) * Convert.ToDecimal(obj.Paid)) / Convert.ToDecimal(obj.TotalNOD)), 0).ToString();
            //        Label HRA = (Label)row.FindControl("lblHRA");
            //        HRA.Text = Math.Round((Convert.ToDecimal(obj.HRA) * Convert.ToDecimal(obj.Paid)) / Convert.ToDecimal(obj.TotalNOD), 0).ToString();
            //        Label ConvAllow = (Label)row.FindControl("lblCV");
            //        ConvAllow.Text = Math.Round((Convert.ToDecimal(obj.CA) * Convert.ToDecimal(obj.Paid)) / Convert.ToDecimal(obj.TotalNOD), 0).ToString();
            //        Label MedicalAllow = (Label)row.FindControl("lblMedical");
            //        MedicalAllow.Text = Math.Round((Convert.ToDecimal(obj.MedicalAllow) * Convert.ToDecimal(obj.Paid)) / Convert.ToDecimal(obj.TotalNOD), 0).ToString();
            //        Label OtherAllow=(Label)row .FindControl ("lblOther");
            //        OtherAllow.Text = Math.Round((Convert.ToDecimal(obj.OtherAllow) * Convert.ToDecimal(obj.Paid)) / Convert.ToDecimal(obj.TotalNOD), 0).ToString();
            //        Label PF = (Label)row.FindControl("lblPF");
            //        PF.Text = Math.Round((Convert.ToDecimal(obj.PF) * Convert.ToDecimal(obj.Paid)) / Convert.ToDecimal(obj.TotalNOD), 0).ToString();
            //        Label PTax = (Label)row.FindControl("lblPTax");
            //        PTax.Text = obj.PT;
            //        Label TDS = (Label)row.FindControl("lblTDS");
            //        TDS.Text = obj.TDS;
            //        Label OtherDeduc = (Label)row.FindControl("lblOD");
            //        OtherDeduc.Text = obj.OtherDedc;
            //        Label S_Adv = (Label)row.FindControl("lblSalAdv");
            //        S_Adv.Text = obj.Sal_Advance;
            //        Label TotalEarn = (Label)row.FindControl("lblGross");
            //        TotalEarn.Text = ((Convert.ToDecimal(Basic.Text)) + (Convert.ToDecimal(HRA.Text)) + (Convert.ToDecimal(ConvAllow.Text)) + (Convert.ToDecimal(MedicalAllow.Text)) + (Convert.ToDecimal(OtherAllow.Text))).ToString();
            //        Label TotalDedc = (Label)row.FindControl("lblTotlDedu");
            //        TotalDedc.Text = ((Convert.ToDecimal(PF.Text)) + (Convert.ToDecimal(PTax.Text)) + (Convert.ToDecimal(TDS.Text)) + (Convert.ToDecimal(OtherDeduc.Text)) + (Convert.ToDecimal(S_Adv.Text))).ToString();
            //        Label NetSal = (Label)row.FindControl("lnlNet");
            //        NetSal.Text = ((Convert.ToDecimal(TotalEarn.Text)) - (Convert.ToDecimal(TotalDedc.Text))).ToString();
            //    }
            //}
        }
    }

    protected void gvPayroll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label paid = (Label)row.FindControl("lblPaid");
            Label NOD = (Label)row.FindControl("lblNOD");
            e.Row.Cells[4].Text = (Convert.ToDecimal(NOD.Text) - Convert.ToDecimal(paid.Text)).ToString();
            Label Basic = (Label)row.FindControl("lblBasic");
            Basic.Text = Math.Round(((Convert.ToDecimal(Basic.Text) * Convert.ToDecimal(paid.Text)) / Convert.ToDecimal(NOD.Text)), 0).ToString();
            Label HRA = (Label)row.FindControl("lblHRA");
            HRA.Text = Math.Round((Convert.ToDecimal(HRA.Text) * Convert.ToDecimal(paid.Text)) / Convert.ToDecimal(NOD.Text), 0).ToString();
            Label ConvAllow = (Label)row.FindControl("lblCV");
                    ConvAllow.Text = Math.Round((Convert.ToDecimal(ConvAllow.Text) * Convert.ToDecimal(paid.Text)) / Convert.ToDecimal(NOD.Text), 0).ToString();
                    Label MedicalAllow = (Label)row.FindControl("lblMedical");
                    MedicalAllow.Text = Math.Round((Convert.ToDecimal(MedicalAllow.Text) * Convert.ToDecimal(paid.Text)) / Convert.ToDecimal(NOD.Text), 0).ToString();
                    Label OtherAllow=(Label)row .FindControl ("lblOther");
                    OtherAllow.Text = Math.Round((Convert.ToDecimal(OtherAllow.Text) * Convert.ToDecimal(paid.Text)) / Convert.ToDecimal(NOD.Text), 0).ToString();
                    Label PF = (Label)row.FindControl("lblPF");
                    PF.Text = Math.Round((Convert.ToDecimal(PF.Text ) * Convert.ToDecimal(paid.Text)) / Convert.ToDecimal(NOD.Text), 0).ToString();
                    Label PTax = (Label)row.FindControl("lblPTax");
                    
                    Label TDS = (Label)row.FindControl("lblTDS");
                    
                    Label OtherDeduc = (Label)row.FindControl("lblOD");
                    
                    Label S_Adv = (Label)row.FindControl("lblSalAdv");
                   
                    Label TotalEarn = (Label)row.FindControl("lblGross");
                    e.Row .Cells [10].Text  = ((Convert.ToDecimal(Basic.Text)) + (Convert.ToDecimal(HRA.Text)) + (Convert.ToDecimal(ConvAllow.Text)) + (Convert.ToDecimal(MedicalAllow.Text)) + (Convert.ToDecimal(OtherAllow.Text))).ToString();
                    e.Row.Cells[17].Text = ((Convert.ToDecimal(PF.Text)) + (Convert.ToDecimal(PTax.Text)) + (Convert.ToDecimal(TDS.Text)) + (Convert.ToDecimal(OtherDeduc.Text)) + (Convert.ToDecimal(S_Adv.Text))).ToString();
                    Label NetSal = (Label)row.FindControl("lnlNet");
                    NetSal.Text = ((Convert.ToDecimal(e.Row.Cells[10].Text)) - (Convert.ToDecimal(e.Row.Cells[17].Text))).ToString();
            

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Export To Excel
        if (gvPayroll.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StockReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvPayroll.AllowPaging = false;
                this.BindPayrollGrid();

                //gvPayroll.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvPayroll.HeaderRow.Cells)
                {
                    cell.BackColor = gvPayroll.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvPayroll.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvPayroll.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvPayroll.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvPayroll.RenderControl(hw);

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
        #endregion

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    private void ExportGridToPDF()
    {
        if (gvPayroll.Rows.Count > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    gvPayroll.AllowPaging = false;
                    this.BindPayrollGrid();

                    gvPayroll.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To PDF");
        }
    }
    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        ExportGridToPDF();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        tblpRint.Visible = true;
    }
    protected void chkSalaryPaySheet_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkESISheet.Checked = false;
            chkPFSheet.Checked = false;
            chkBankStatement.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Payshet&CPid=" + ddlComp.SelectedItem.Value + " &e=" + ddlMonth.SelectedValue + " &year=" + ddlYear.SelectedItem.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chkPFSheet_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkESISheet.Checked = false;
            chkSalaryPaySheet.Checked = false;
            chkBankStatement.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=PFSheet&CPid=" + ddlComp.SelectedItem.Value + " &e=" + ddlMonth.SelectedValue + " &year=" + ddlYear.SelectedItem.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chkESISheet_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkSalaryPaySheet.Checked = false;
            chkPFSheet.Checked = false;
            chkBankStatement.Checked = false;
            //chkSalaryPaySheet.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ESISheet&CPid=" + ddlComp.SelectedItem.Value + " &e=" + ddlMonth.SelectedValue + " &year=" + ddlYear.SelectedItem.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chkBankStatement_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkSalaryPaySheet.Checked = false;
            chkPFSheet.Checked = false;
            chkESISheet.Checked = false;
            //chkSalaryPaySheet.Checked = false;
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=BankStatement&CPid=" + ddlComp.SelectedItem.Value + " &e=" + ddlMonth.SelectedValue + " &year=" + ddlYear.SelectedItem.Text + "";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
}