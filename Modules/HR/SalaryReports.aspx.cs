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

public partial class Modules_HR_SalaryReports : System.Web.UI.Page
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
    double Bouns2 = 0;
    double EcTotal1 = 0;
    double EcTotal2 = 0;
    double Ptax = 0;
    double EdTotal = 0;

    double CtcPm = 0;
    double CtcPa = 0;

    double NetPay = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpName);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtGrossAmount.Text != "")
        {
            Basic1 = (((double.Parse(txtGrossAmount.Text) * 60) / 100));
            txtBasic1.Text = Basic1.ToString();

            Basic2 = Basic1 * 12;
            txtBasic2.Text = Basic2.ToString();

            Hra1 = (((Basic1 * 40) / 100));
            txtHRA1.Text = Hra1.ToString();

            Hra2 = Hra1 * 12;
            txtHRA2.Text = Hra2.ToString();

            Other1 = (double.Parse(txtGrossAmount.Text) - Basic1 - Hra1);
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
            if (txtAge.Text != "")
            {
                if ((int.Parse(txtAge.Text) <= 25))
                {
                    Medi1 = 45;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(txtAge.Text) >= 26 && int.Parse(txtAge.Text) <= 35)
                {
                    Medi1 = 59;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }

                if (int.Parse(txtAge.Text) >= 36 && int.Parse(txtAge.Text) <= 45)
                {
                    Medi1 = 80;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(txtAge.Text) >= 46 && int.Parse(txtAge.Text) <= 55)
                {
                    Medi1 = 136;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(txtAge.Text) >= 56 && int.Parse(txtAge.Text) <= 65)
                {
                    Medi1 = 180;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(txtAge.Text) >= 66 && int.Parse(txtAge.Text) <= 70)
                {
                    Medi1 = 224;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }

                if (int.Parse(txtAge.Text) >= 71 && int.Parse(txtAge.Text) <= 75)
                {
                    Medi1 = 240;
                    txtMedi1.Text = Medi1.ToString();

                    Medi2 = (Medi1 * 12);
                    txtMedi2.Text = Medi2.ToString();
                }
                if (int.Parse(txtAge.Text) >= 76 && int.Parse(txtAge.Text) <= 80)
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

            Bouns1 = (((double.Parse(txtGrossAmount.Text) * 8.33) / 100));
            txtBonus1.Text = Bouns1.ToString();

            Bouns2 = Bouns1 * 12;
            txtBonus2.Text = Bouns2.ToString();

            EcTotal1 = (Pf1 + Medi1 + Bouns1);
            txtECTotal1.Text = EcTotal1.ToString();

            EcTotal2 = (Pf2 + Medi2 + Bouns2);
            txtECTotal2.Text = EcTotal2.ToString();


            if (int.Parse(txtGrossAmount.Text) <= 5000)
            {
                Ptax = 0;
                txtEDptax.Text = Ptax.ToString();
            }

            if (int.Parse(txtGrossAmount.Text) >= 5001 && int.Parse(txtGrossAmount.Text) <= 6000)
            {
                Ptax = 60;
                txtEDptax.Text = Ptax.ToString();
            }

            if (int.Parse(txtGrossAmount.Text) >= 6001 && int.Parse(txtGrossAmount.Text) <= 10000)
            {
                Ptax = 80;
                txtEDptax.Text = Ptax.ToString();
            }

            if (int.Parse(txtGrossAmount.Text) >= 10001 && int.Parse(txtGrossAmount.Text) <= 15000)
            {
                Ptax = 100;
                txtEDptax.Text = Ptax.ToString();
            }
            if (int.Parse(txtGrossAmount.Text) >= 15001 && int.Parse(txtGrossAmount.Text) <= 20000)
            {
                Ptax = 150;
                txtEDptax.Text = Ptax.ToString();
            }
            if (int.Parse(txtGrossAmount.Text) >= 20000)
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../HR/Print.aspx','PrintMe','height=600,width=900,scrollbars=yes');</script>");
    }
    protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster objHR = new HR.EmployeeMaster();
            if (objHR.EmployeeMaster_Select(ddlEmpName.SelectedItem.Value) > 0)
            {

                txtGrossAmount.Text = objHR.GrossSal;
                txtAge.Text = "";
                string hi = objHR.grossdob; //Yantra.Classes.General.toMMDDYYYY(objHR.grossdob);
                txtAge.Text = CalculateAge(Convert.ToDateTime(hi)).ToString();

                lblAge.Text = txtAge.Text;
                lblEmpname.Text = objHR.EmpFirstName;

                Button1_Click(sender, e);
            }
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
    public static int CalculateAge(DateTime birthDate)
    {
        DateTime now = DateTime.Today;
        int years = now.Year - birthDate.Year;

        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            --years;

        return years;
    }


    public long DateDiff(System.DateTime StartDate, System.DateTime EndDate)
    {
        long lngDateDiffValue = 0;
        System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
        lngDateDiffValue = (long)(TS.Days / 365);
        return (lngDateDiffValue);
    }
}