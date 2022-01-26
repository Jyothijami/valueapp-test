using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.Classes;
using Yantra.MessageBox;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using vllib;


public partial class Modules_HR_PaySlip : basePage
{
    double Basic1 = 0;
    double Basic2 = 0;
    double Hra1 = 0;
    double Hra2 = 0;
    double Other1 = 0;
    double Other2 = 0;
    double GrossTotal1 = 0;
    double GrossTotal2 = 0;
    double Pf1 = 0;
    double Pf2 = 0;
    double Medi1 = 0;
    double Medi2 = 0;
    double Bouns1 = 0;
    double NetPay = 0;
    double Bouns2 = 0;
    double EcTotal1 = 0;
    double EcTotal2 = 0;
    double Ptax = 0;
    double EdTotal = 0;

    double CtcPm = 0;
    double CtcPa = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            HR.EmployeeMaster objmas = new HR.EmployeeMaster();
            objmas.EmployeeMaster_Select(Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId));
            lblEmpId.Text = objmas.EmpID;
            txtName.Text = objmas.EmpFirstName;
            txtDesignation.Text = objmas.DesgName12;
            txtDepartment.Text = objmas.DeptName12;
            lblSal.Text = objmas.GrossSal;
            lblDOB.Text = objmas.EmpDOB;
            lblAccNo.Text = objmas.AssignedAccNo;
            lblDOJ.Text = objmas.EmpDetDOJ;
            BindYears();

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

    private void CalcGrossSalary()
    {
        lblSal1.Text = (Convert.ToInt32(lblSal.Text) / Convert.ToInt32(txtNoOfDays.Text)).ToString();
        lblGrossSalary.Text = (Convert.ToInt32(lblSal1.Text) * Convert.ToInt32(txtPaidDays.Text)).ToString();
    }

    private void GetAge()
    {
        DateTime now = Convert.ToDateTime(DateTime.Now);
        string date = Yantra.Classes.General.toMMDDYYYY(lblDOB.Text);

        DateTime dob = Convert.ToDateTime(date);
        TimeSpan time = now.Subtract(dob);
        int total = (time.Days) / 365;
        lblAge.Text = total.ToString();
    }

    private void getCal()
    {

        if (lblGrossSalary.Text != "")
        {
            Basic1 = (((double.Parse(lblGrossSalary.Text) * 60) / 100));
            txtBasic1.Text = Basic1.ToString();

            Basic2 = Basic1 * 12;
            txtBasic2.Text = Basic2.ToString();

            Hra1 = (((Basic1 * 40) / 100));
            txtHRA1.Text = Hra1.ToString();

            Hra2 = Hra1 * 12;
            txtHRA2.Text = Hra2.ToString();

            Other1 = (double.Parse(lblGrossSalary.Text) - Basic1 - Hra1);
            txtOther1.Text = Other1.ToString();

            Other2 = Other1 * 12;
            txtOther2.Text = Other2.ToString();

            GrossTotal1 = (Basic1 + Hra1 + Other1);
            txtGrossTotal1.Text = GrossTotal1.ToString();

            GrossTotal2 = (Basic2 + Hra2 + Other2);
            txtGrossTotal2.Text = GrossTotal2.ToString();

            Pf1 = ((Basic1 * 12) / 100);
            if (Pf1 >= 780)
            {
                Pf1 = 780;
                txtPF1.Text = Pf1.ToString();
                txtEDpf.Text = Pf1.ToString();
            }
            else
            {
                txtPF1.Text = Pf1.ToString();
                txtEDpf.Text = Pf1.ToString();
            }


            Pf2 = double.Parse(txtPF1.Text) * 12;
            txtPF2.Text = Pf2.ToString();

            #region Age
            if (lblAge.Text != "")
            {
                if ((int.Parse(lblAge.Text) <= 25))
                {
                    Medi1 = 45;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(lblAge.Text) >= 26 && int.Parse(lblAge.Text) <= 35)
                {
                    Medi1 = 59;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }

                if (int.Parse(lblAge.Text) >= 36 && int.Parse(lblAge.Text) <= 45)
                {
                    Medi1 = 80;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(lblAge.Text) >= 46 && int.Parse(lblAge.Text) <= 55)
                {
                    Medi1 = 136;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(lblAge.Text) >= 56 && int.Parse(lblAge.Text) <= 65)
                {
                    Medi1 = 180;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(lblAge.Text) >= 66 && int.Parse(lblAge.Text) <= 70)
                {
                    Medi1 = 224;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }

                if (int.Parse(lblAge.Text) >= 71 && int.Parse(lblAge.Text) <= 75)
                {
                    Medi1 = 240;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(lblAge.Text) >= 76 && int.Parse(lblAge.Text) <= 80)
                {
                    Medi1 = 296;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
            }
            else
            {
                Medi1 = 0;
                txtMedi1.Text = Medi1.ToString();

                Medi2 = (Medi1 * 12);
                txtMedi2.Text = Medi2.ToString();
            }

            #endregion

            Bouns1 = (((double.Parse(lblGrossSalary.Text) * 8.33) / 100));
            txtBonus1.Text = Bouns1.ToString();

            Bouns2 = Bouns1 * 12;
            txtBonus2.Text = Bouns2.ToString();

            EcTotal1 = (Pf1 + Medi1 + Bouns1);
            txtECTotal1.Text = EcTotal1.ToString();

            EcTotal2 = (Pf2 + Medi2 + Bouns2);
            txtECTotal2.Text = EcTotal2.ToString();


            if (int.Parse(lblGrossSalary.Text) <= 5000)
            {
                Ptax = 0;
                txtEDptax.Text = Ptax.ToString();
            }

            if (int.Parse(lblGrossSalary.Text) >= 5001 && int.Parse(lblGrossSalary.Text) <= 6000)
            {
                Ptax = 60;
                txtEDptax.Text = Ptax.ToString();
            }

            if (int.Parse(lblGrossSalary.Text) >= 6001 && int.Parse(lblGrossSalary.Text) <= 10000)
            {
                Ptax = 80;
                txtEDptax.Text = Ptax.ToString();
            }

            if (int.Parse(lblGrossSalary.Text) >= 10001 && int.Parse(lblGrossSalary.Text) <= 15000)
            {
                Ptax = 100;
                txtEDptax.Text = Ptax.ToString();
            }
            if (int.Parse(lblGrossSalary.Text) >= 15001 && int.Parse(lblGrossSalary.Text) <= 20000)
            {
                Ptax = 150;
                txtEDptax.Text = Ptax.ToString();
            }
            if (int.Parse(lblGrossSalary.Text) >= 20000)
            {
                Ptax = 200;
                txtEDptax.Text = Ptax.ToString();
            }

            EdTotal = Pf1 + Ptax;
            txtEDTotal.Text = EdTotal.ToString();

            CtcPm = GrossTotal1 + EcTotal1;
            txtCTCPM.Text = CtcPm.ToString();

            CtcPa = GrossTotal2 + EcTotal2;
            txtCTCPA.Text = CtcPa.ToString();

            NetPay = GrossTotal1 - EdTotal;

            txtNetSal.Text = NetPay.ToString();



        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        CalcGrossSalary();
        GetAge();
        getCal();
        GeneratePaySlip();
    }

    private void GeneratePaySlip()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string pfNumber = "";
                string MOP = "Online Banking";
                string dateOfJoin = "";
                string month = "";
                StringBuilder sb = new StringBuilder();

                sb.Append("<table width='100%' border='1' border-style='solid' border-color='black' cellspacing='0' cellpadding='2'>");
                //sb.Append("<tr><td align='center' colspan = '3'><img src='" + Server.MapPath("~/ValueLine.gif") + "'/></td></tr>");
                sb.Append("<tr><td colspan = '4' align='center'><H3><b>VALUE LINE HOME STYLE PVT.LTD </b></H3></td></tr>");
                sb.Append("<tr><td colspan = '4' align='center'><b>Pay Slip For The Month Of</b>  ");
                sb.Append(ddlMonth.SelectedItem.Text);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>Emp Id </td>");
                sb.Append("<td align='left' width='32%'> ");
                sb.Append(lblEmpId.Text);
                sb.Append(" </td>");

                sb.Append("<td align='left' width='18%'>PF Number </td>");
                sb.Append("<td align='left'> ");
                sb.Append(pfNumber);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>Name Of Employee </td>");
                sb.Append("<td align='left' width='32%'> ");
                sb.Append(txtName.Text);
                sb.Append(" </td>");

                sb.Append("<td align='left' width='18%'>Bank Account Number </td>");
                sb.Append("<td align='left'> ");
                sb.Append(lblAccNo.Text);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>D.O.J </td>");
                sb.Append("<td align='left' width='32%'> ");
                sb.Append(lblDOJ.Text);
                sb.Append(" </td>");

                sb.Append("<td align='left' width='18%'>Mode Of Payment </td>");
                sb.Append("<td align='left'> ");
                sb.Append(MOP);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>Designation </td>");
                sb.Append("<td align='left' width='32%'> ");
                sb.Append(txtDesignation.Text);
                sb.Append(" </td>");

                sb.Append("<td align='left' width='18%'>Department </td>");
                sb.Append("<td align='left'> ");
                sb.Append(txtDepartment.Text);
                sb.Append(" </td></tr>");
                sb.Append("</table>");
                //==================================================================
                sb.Append("<table width='100%' border='1' border-style='solid' border-color='black' cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td colspan = '13' align='center'><b>LEAVES STATUS</b></td></tr>");
                sb.Append("<tr><td colspan = '4' align='center'><b>CL</b></td>  ");
                sb.Append("<td colspan = '4' align='center'><b>PL</b></td>  ");
                sb.Append("<td colspan = '4' align='center'><b>C-OFF</b></td>  ");
                sb.Append("<td rowspan='2' align='center'><b>Total Balance</b></td></tr>  ");

                sb.Append("<tr><td align='left' width='7.5%'><b>OB</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>LC</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>LA</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>CB</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>OB</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>LC</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>LA</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>CB</b> </td>");
                sb.Append("<td align='left' width='10%'><b>OB</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>LC</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>LA</b> </td>");
                sb.Append("<td align='left' width='7.5%'><b>CB</b> </td></tr>");

                sb.Append("<tr><td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='10%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='7.5%'>");
                sb.Append(dateOfJoin);
                sb.Append("</td>");
                sb.Append("<td align='center'>");
                sb.Append(dateOfJoin);
                sb.Append("</td></tr>");

                sb.Append("</table>");
                //========================================================================
                sb.Append("<table width='100%' border='1' border-style='solid' border-color='black'  cellspacing='0' cellpadding='2'>");
                sb.Append("<tr><td colspan = '8' align='center'><b>SALARY DETAILS</b></td></tr>");
                sb.Append("<tr><td align='left' width='18%'><b>EARNINGS: </b></td>");
                sb.Append("<td align='left' width='9%'></td> ");
                sb.Append("<td align='left'width='9%'></td> ");
                sb.Append("<td align='left' width='14%'> <b>RS. </b>");
                sb.Append(" </td>");
                //width='32%'
                sb.Append("<td align='left' width='18%'><b>DEDUCTIONS: </b></td>");
                sb.Append("<td align='left' width='9%'></td> ");
                sb.Append("<td align='left' width='9%'></td> ");
                sb.Append("<td align='left' width='14%'><b>RS. </b>");
                sb.Append(" </td></tr>");


                sb.Append("<tr><td align='left' width='18%'>BASIC</td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(txtBasic1.Text);
                sb.Append(" </td>");


                sb.Append("<td align='left' width='18%'>Provident Fund </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(txtPF1.Text);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>HRA </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(txtHRA1.Text);
                sb.Append(" </td>");


                sb.Append("<td align='left' width='18%'>Proffessional Tax </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(txtEDptax.Text);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>Other Allow.  </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);

                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(txtOther1.Text);

                sb.Append(" </td>");


                sb.Append("<td align='left' width='18%'>Salary Advance </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'></td>");
                sb.Append("<td colspan='3' align='left'> ");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");

                sb.Append("<td align='left' width='18%'>TDS</td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>Gross Salary</td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(txtGrossTotal1.Text);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td>");


                sb.Append("<td align='left' width='18%'>Other Deductions </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'>Less Loss of Pay</td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(dateOfJoin);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td>");


                sb.Append("<td align='left' width='18%'></td>");
                sb.Append("<td align='left'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td></tr>");

                sb.Append("<tr><td align='left' width='18%'><b>TOTAL EARNINGS</b></td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(txtCTCPM.Text);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td>");


                sb.Append("<td align='left' width='18%'><b>TOTAL DEDUCTIONS </b></td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(txtEDTotal.Text);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='9%'> ");
                sb.Append(month);
                sb.Append(" </td>");
                sb.Append("<td align='left' width='14%'> ");
                sb.Append(month);
                sb.Append(" </td></tr>");


                sb.Append("<tr><td align='left' width='10%'> Manager HR Sign</td>");
                sb.Append("<td align='left' colspan='3' width='35%'> ");
                sb.Append(month);
                sb.Append(" </td>");

                sb.Append("<td align='left' width='18%'>NET SALARY</td>");
                sb.Append("<td align='left' colspan='3'> ");
                sb.Append(txtNetSal.Text);
                sb.Append(" </td></tr>");
                sb.Append("</table>");

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                StringReader sr = new StringReader(sb.ToString());
                //  Editor1.Content = sb.ToString();
                Document pdfDoc = new Document(PageSize.A3);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();


            }
        }

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        //Here we need to generate our new form bhaya..
        try
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Payslip&siid=" + lblEmpId.Text + " &e="+ddlMonth.SelectedValue+" &year="+ddlYear.SelectedItem.Text+"";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
}