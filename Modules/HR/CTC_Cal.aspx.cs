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

public partial class Modules_HR_CTC_Cal : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpName);
        }
    }
    //protected void btnCal_Click(object sender, EventArgs e)
    //{
    //    decimal basic2 = (Convert.ToDecimal(txtGrossAmount.Text) * 45) / 100;
    //    decimal basic1 = basic2 / 12;
    //    txtBasic2.Text = basic2.ToString();
    //    txtBasic1.Text = basic1.ToString();


    //    decimal hra2 = (basic2 * 40) / 100;
    //    decimal hra1 = hra2 / 12;
    //    txtHRA1.Text = hra1.ToString();
    //    txtHRA2.Text = hra2.ToString();

    //    decimal trans2 = 9600;
    //    decimal trans1 = 800;
    //    txtOther2.Text = trans2.ToString();
    //    txtOther1.Text = trans1.ToString();


    //    //esi fixed factor = 0.00326 per 1/- Rs.  Gross-Rs.2,08,664
    //    decimal esi1 = 0;
    //    decimal esi2 = 0;
    //    if(Convert.ToDecimal(txtGrossAmount.Text) > 208659)
    //    {

    //        txtMedi1.Text = esi1.ToString();
    //        txtMedi2.Text = esi2.ToString();

    //    }
    //    else
    //    {

    //         esi1 = (Convert.ToDecimal(txtGrossAmount.Text) * Convert.ToDecimal(0.00326));
    //         esi2 = esi1 * 12;
    //        txtMedi1.Text = esi1.ToString();
    //        txtMedi2.Text = esi2.ToString();

    //    }
    //    decimal other2 = 0;
    //    decimal other1 = 0;
    //    txtoth1.Text = other1.ToString();
    //    txtoth2.Text = other2.ToString();

    //    decimal perf2 = Convert.ToDecimal(txtGrossAmount.Text) / 12;
    //    txtBonus2.Text = perf2.ToString();
    //    decimal perf1 = perf2 / 12;
    //    txtBonus1.Text = perf1.ToString();

    //    decimal pf2 = (basic2 * 12) / 100;
    //    decimal pf1 = pf2 / 12;
    //    txtPF2.Text = pf2.ToString();
    //    txtPF1.Text = pf1.ToString();

    //    decimal spl2 = Convert.ToDecimal(txtGrossAmount.Text) - other2 - perf2 - esi2 - pf2 - basic2 - hra2 - trans2;
    //    decimal spl1 = spl2 / 12;
    //    txtSpl2.Text = spl2.ToString();
    //    txtSpl1.Text = spl1.ToString();

    //    decimal gross2 = basic2 + hra2 + trans2 + spl2;
    //    decimal gross1 = basic1 + hra1 + trans1 + spl1;
    //    txtGrossTotal2.Text = gross2.ToString();
    //    txtGrossTotal1.Text = gross1.ToString();

    //    decimal stat2 = pf2 + esi2 + perf2;
    //    decimal stat1 = pf1 + esi1 + perf1;
    //    txtECTotal2.Text = stat2.ToString();
    //    txtECTotal1.Text = stat1.ToString();



    //    txtCTCPA.Text = (gross2 + stat2 + other2).ToString();
    //    txtCTCPM.Text = (gross1 + stat1 + other1).ToString();



    //}
    protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string EmpId = ddlEmpName.SelectedItem.Value;
        //SqlCommand cmd = new SqlCommand("select GROSS_SAL from YANTRA_EMPLOYEE_MAST where EMP_ID='" + EmpId + "'", con);
        //cmd.CommandType = CommandType.Text;
        ////cmd.Parameters.AddWithValue("@EmpId", EmpId);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        ////txtAge.Text = dt.Rows[0][0].ToString();
        //String SalPerA = dt.Rows[0][0].ToString();
        //txtGrossAmount.Text = (Convert.ToDecimal(SalPerA) * 12).ToString();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnCal_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("[Salary_Breakup]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Fsal", txtGrossAmount.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblStatus.Text = dt.Rows[0][15].ToString();
            txtBasic2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][0])).ToString();
            txtHRA2.Text = dt.Rows[0][1].ToString();
            txtOther2.Text = dt.Rows[0][2].ToString();
            txtBonus2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][4])).ToString();
            txtPF2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][3])).ToString();
            txtCTCPA.Text = dt.Rows[0][14].ToString();
            txtoth2.Text = "0";
            

            txtBasic1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][0])/12).ToString();
            txtHRA1.Text =  Math.Round(Convert.ToDecimal(dt.Rows[0][1])/12).ToString();
            txtOther1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][2])/12).ToString();
            txtBonus1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][4])/12).ToString();
            txtPF1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][3])/12).ToString();
            txtCTCPM.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][14])/12).ToString();
            txtoth1.Text = "0";

            if (lblStatus.Text == "ESI_is_Applicable")
            {
                txtSpl2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][7])).ToString();
                txtGrossTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][9])).ToString();
                txtECTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][11])).ToString();
                txtMedi2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][10])).ToString();

                txtSpl1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][7])/12).ToString();
                txtGrossTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][9])/12).ToString();
                txtECTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][11])/12).ToString();
                txtMedi1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][10])/12).ToString();
               
            }
            else
            {
                txtSpl2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][6])).ToString();
                txtGrossTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][8])).ToString();
                txtECTotal2.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][12])).ToString();
                txtMedi2.Text = "0";

                txtSpl1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][6])/12).ToString();
                txtGrossTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][8])/12).ToString();
                txtECTotal1.Text = Math.Round(Convert.ToDecimal(dt.Rows[0][12])/12).ToString();
                txtMedi1.Text = "0";
            }
        }
    }
}