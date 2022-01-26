using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Text;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
public partial class Modules_SCM_Insurance_Form : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillInsuranceCompany();
            FillCurrency();
        }
    }

    private void FillCurrency()
    {
        try
        {
            Masters.CurrencyType.CurrencyType_Select(ddlMoney);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }

    private void FillInsuranceCompany()
    {
        try
        {
            Masters.ProductCompany.InsuranceCompany_Select(ddlInsurance);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           Masters.Dispose();
        }
    }

    protected void ddlInsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("select Address from Insurance_Master where Insurance_Master_id = '"+ddlInsurance.SelectedItem.Value+"' ", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtAddress.Text = dt.Rows[0][0].ToString();
        }
        catch (Exception ex) 
        {
            MessageBox.Show(this, ex.Message.ToString()); 
        }
        finally 
        { 
            //Masters.Dispose();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (gvInsurance.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=Insurance&dcid=" + gvInsurance.SelectedRow.Cells[10].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
   {
        //USP_Save_InsuranceForm
        try
        {
            HR.Convenience_Voucher obj = new HR.Convenience_Voucher();
            string insnum = HR.Convenience_Voucher.Insurance_AutoGenCode();
            obj.Ins_Num = insnum;
            obj.Ins_Comp_Id = ddlInsurance.SelectedItem.Value;
            obj.Ins_Comp_Name = ddlInsurance.SelectedItem.Text;
            obj.Ins_Comp_Add = txtAddress.Text;
            obj.Open_Cover_No = txtOpenCoverNo.Text;
            obj.Importing_Item_Details = txtImporting.Text;
            obj.From_Supplier = txtSupplier.Text;
            obj.To_CompanyLocation = txtLocation.Text;
            obj.Mode_Of_Dispatch = txtMode.Text;
            obj.via_Location = txtVia.Text;
            obj.Currency_Id = ddlMoney.SelectedItem.Value;
            obj.Currency_Name = ddlMoney.SelectedItem.Text;
            obj.Amount = Convert.ToDecimal(txtMoney.Text).ToString();
            obj.Multiply_Factor =Convert.ToDecimal( txtMulFactor.Text).ToString();
            obj.Value_Of_Consignment = Convert.ToDecimal(txtValueOfCons.Text).ToString();
            obj.Customs_Duty = Convert.ToDecimal(txtCustDuty.Text).ToString();
            obj.Customs_Duty_Amt = ((Convert.ToDecimal(txtTotal.Text)) - (Convert.ToDecimal(txtValueOfCons.Text))).ToString();
            obj.Total_Amount = Convert.ToDecimal(txtTotal.Text).ToString();
            obj.Name_Of_Consignor = txtConsignor.Text;
            obj.Name_Of_Consignee = txtConsignee.Text;
            obj.Invoice_No = txtInvNo.Text;
            obj.Invoice_Dated_On = Yantra.Classes.General.toMMDDYYYY(txtInvDatedOn.Text);
            obj.Bill_Of_Loading_No = txtBillLoadNo.Text;
            obj.Bill_Dated_On = Yantra.Classes.General.toMMDDYYYY(txtBillDatedOn.Text);
            obj.Vessel_Voyage_No = txtVesselNo.Text;
            obj.Insurance_Date = (DateTime.UtcNow).ToString();
            obj.Ex1 = "-";
            obj.Ex2 = "-";
            obj.Ex3 = "-";

            MessageBox.Show(this, obj.Insurance_Save());

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);

        }
        finally
        {
            gvInsurance.DataSource = null;
            gvInsurance.DataBind();
        }

    }
    protected void lbtnInsId_Click(object sender, EventArgs e)
    {
        LinkButton lbtnInsId;
        lbtnInsId = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnInsId.Parent.Parent;
        gvInsurance.SelectedIndex = gvRow.RowIndex;
    }
}
 
