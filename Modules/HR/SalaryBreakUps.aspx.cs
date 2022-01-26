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
using vllib;

using System.Data.SqlClient;

public partial class Modules_HR_SalaryBreakUps : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    
    double amt;
    double Basic1 = 0;
    double Basic2 = 0;
    double Hra1 = 0;
    double Hra2 = 0;
    double ca1 = 0;
    double ca2 = 0;
    double mer1 = 0;
    double mer2 = 0;
    double Other1 = 0;
    double Other2 = 0;
    double GrossTotal1 = 0;
    double GrossTotal2 = 0;
    double Pf1 = 0;
    double Pf2 = 0;
    double esicd1 = 0;
    double esicd2 = 0;
    double Pt1 = 0;
    double Pt2 = 0;
    double ectotal1 = 0;
    double ectotal2 = 0;
    double gtd1 = 0;
    double gtd2 = 0;
    double pfb1 = 0;
    double pfb2 = 0;
    double esicb1 = 0;
    double esicb2 = 0;
    double accb1 = 0;
    double accb2 = 0;
    double bonusb1 = 0;
    double bonusb2 = 0;
    double totalb1 = 0;
    double totalb2 = 0;

    double CtcPm = 0;
    double CtcPa = 0;

    double NetPay = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpName);
            txtGrossAmountYear.Visible = false;
            btnPrint.Visible = false;
        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "81");

        //Button1.Enabled = up.Calculate;
        btnPrint.Enabled = up.Print;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
        if (txtGrossAmount.Text != "")
        {
            int sal = Convert.ToInt32(txtGrossAmount.Text);
            int age = Convert.ToInt32(txtAge.Text);

            SqlCommand cmd = new SqlCommand("Usp_GetCTCAge", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@salary_fix", sal);
            cmd.Parameters.AddWithValue("@Age", age);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Basic1 = Convert.ToDouble(dt.Rows[0][0].ToString());
            txtBasic1.Text = Math.Round(Basic1).ToString();
            Basic2 = Convert.ToDouble(txtBasic1.Text);
            txtBasic2.Text = Math.Round(Basic2 * 12).ToString();

            Hra1 = Convert.ToDouble(dt.Rows[0][1].ToString());
            txtHRA1.Text = Math.Round(Hra1).ToString();
            Hra2 = Convert.ToDouble(txtHRA1.Text);
            txtHRA2.Text = Math.Round(Hra2 * 12).ToString();

            ca1 = Convert.ToDouble(dt.Rows[0][2].ToString());
            txtCA1.Text = Math.Round(ca1).ToString();
            ca2 = Convert.ToDouble(txtCA1.Text);
            txtCA2.Text = Math.Round(ca2 * 12).ToString();

            mer1 = Convert.ToDouble(dt.Rows[0][3].ToString());
            txtMER1.Text = Math.Round(mer1).ToString();
            mer2 = Convert.ToDouble(txtMER1.Text);
            txtMER2.Text = Math.Round(mer2 * 12).ToString();

            Other1 = Convert.ToDouble(dt.Rows[0][4].ToString());
            txtOther1.Text = Math.Round(Other1).ToString();
            Other2 = Convert.ToDouble(txtOther1.Text);
            txtOther2.Text = Math.Round(Other2 * 12).ToString();

            GrossTotal1 = Convert.ToDouble(dt.Rows[0][5].ToString());
            txtGrossTotal1.Text = Math.Round(GrossTotal1).ToString();
            GrossTotal2 = Convert.ToDouble(txtGrossTotal1.Text);
            txtGrossTotal2.Text = Math.Round(GrossTotal2 * 12).ToString();

            Pf1 = Convert.ToDouble(dt.Rows[0][6].ToString());
            txtPFD1.Text = Math.Round(Pf1).ToString();
            Pf2 = Convert.ToDouble(txtPFD1.Text);
            txtPFD2.Text = Math.Round(Pf2 * 12).ToString();

            esicd1 = Convert.ToDouble(dt.Rows[0][7].ToString());
            txtESICD1.Text = Math.Round(esicd1).ToString();
             esicd2= Convert.ToDouble(txtESICD1.Text);
             txtESICD2.Text = Math.Round(esicd2 * 12).ToString();

            Pt1 = Convert.ToDouble(dt.Rows[0][8].ToString());
            txtPT1.Text = Math.Round(Pt1).ToString();
            Pt2 = Convert.ToDouble(txtPT1.Text);
            txtPT2.Text = Math.Round(Pt2 * 12).ToString();

            ectotal1 = Convert.ToDouble(dt.Rows[0][9].ToString());
            txtECTotal1.Text = Math.Round(ectotal1).ToString();
            ectotal2 = Convert.ToDouble(txtECTotal1.Text);
            txtECTotal2.Text = Math.Round(ectotal2 * 12).ToString();

            gtd1 = Convert.ToDouble(dt.Rows[0][10].ToString());
            txtGrossTotalD1.Text = Math.Round(gtd1).ToString();
            gtd2 = Convert.ToDouble(txtGrossTotalD1.Text);
            txtGrossTotalD2.Text = Math.Round(gtd2 * 12).ToString();

            pfb1 = Convert.ToDouble(dt.Rows[0][11].ToString());
            txtPFB1.Text = Math.Round(pfb1).ToString();
            pfb2 = Convert.ToDouble(txtPFB1.Text);
            txtPFB2.Text = Math.Round(pfb2 * 12).ToString();

            esicb1 = Convert.ToDouble(dt.Rows[0][12].ToString());
            txtESICB1.Text = Math.Round(esicb1).ToString();
            esicb2 = Convert.ToDouble(txtESICB1.Text);
            txtESICB2.Text = Math.Round(esicb2 * 12).ToString();

            accb1 = Convert.ToDouble(dt.Rows[0][13].ToString());
            txtACCB1.Text = Math.Round(accb1).ToString();
            accb2 = Convert.ToDouble(txtACCB1.Text);
            txtACCB2.Text = Math.Round(accb2 * 12).ToString();

            bonusb1 = Convert.ToDouble(dt.Rows[0][14].ToString());
            txtBONUSB1.Text = Math.Round(bonusb1).ToString();
            bonusb2 = Convert.ToDouble(txtBONUSB1.Text);
            txtBONUSB2.Text = Math.Round(bonusb2 * 12).ToString();

            totalb1 = Convert.ToDouble(dt.Rows[0][15].ToString());
            txtTotalB1.Text = Math.Round(totalb1).ToString();
            totalb2 = Convert.ToDouble(txtTotalB1.Text);
            txtTotalB2.Text = Math.Round(totalb2 * 12).ToString();

            txtCTCPM.Text = (GrossTotal2 + totalb2).ToString();
            CtcPa = Convert.ToDouble(txtCTCPM.Text);
            txtCTCPA.Text = Math.Round(CtcPa * 12).ToString();

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

                txtGrossAmountYear.Text =Math.Round(Convert.ToDecimal(objHR.GrossSal)).ToString();
                
                //long yrs;
                //System.DateTime date2 = System.DateTime.Now;
                //System.DateTime date1 = new DateTime(Int64.Parse(objHR.grossdob));
                //yrs = DateDiff(date1, date2);
                //txtAge.Text = yrs.ToString();
                //lblAge.Text = yrs.ToString();
                lblEmpname.Text = objHR.EmpFirstName;
                lblNetSal.Text = objHR.GrossSal;
            }
            txtAge.Text = objHR.GetAge(Convert.ToInt32(ddlEmpName.SelectedItem.Value)).ToString();
            lblAge.Text = txtAge.Text;
            amt = Convert.ToDouble(txtGrossAmountYear.Text);
            txtGrossAmount.Text =Math.Round((amt / 12)).ToString();
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
}
