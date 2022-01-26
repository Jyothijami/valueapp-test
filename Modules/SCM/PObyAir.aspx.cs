using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Modules_SCM_PObyAir : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
           
        }
        //if (!IsPostBack)
        //{
        //    txtAirFreight.Visible = txtAirlinerDO.Visible = txtAllInCharges.Visible = txtCCfee.Visible = txtEx1.Visible = txtEx2.Visible = txtEx3.Visible = false;
        //    txtExworks.Visible = txtFuel.Visible = txtHAWBfee.Visible = txtIGMfee.Visible = txtServiceTax.Visible = txtSsc.Visible = false;
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDetails();
        clearFields();
        gvPObyAir.DataBind();
    }
    private void clearFields()
    {
        txtAirFreight.Text = txtAirlinerDO.Text = txtAllInCharges.Text = txtCCfee.Text = txtDate.Text = txtExworks.Text = txtFuel.Text =
            txtHAWBfee.Text = txtIGMfee.Text = txtName.Text = txtPONo.Text = txtRef.Text = txtRevisedDate.Text = txtServiceTax.Text =
            txtShipperName.Text = txtSsc.Text = txtTerms.Text = "";
        txtEx1.Text = txtEx2.Text = txtEx3.Text = "";
    }
    private void SaveDetails()
    {
        SCM.SuppliersMaster obj = new SCM.SuppliersMaster();
        obj.Name = txtName.Text;
        obj.Date = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
        obj.PONo = txtPONo.Text;
        obj.Ref = txtRef.Text;
        obj.Quot_Revised_Date = Yantra.Classes.General.toMMDDYYYY(txtRevisedDate.Text);
        obj.Shipper_Name = txtShipperName.Text;
        obj.Terms = txtTerms.Text;
        if (txtAirFreight.Text == "")
        {
            txtAirFreight.Text = "-";
        }
        obj.Freight = txtAirFreight.Text;
        if (txtFuel.Text == "")
        {
            txtFuel.Text = "-";
        }
        obj.Fuel = txtFuel.Text;
        if (txtSsc.Text == "")
        {
            txtSsc.Text = "-";
        }
        obj.ssc = txtSsc.Text;
        if (txtExworks.Text == "")
        {
            txtExworks.Text = "-";
        }
        obj.Exworks = txtExworks.Text;
        if (txtAllInCharges.Text == "")
        {
            txtAllInCharges.Text = "-";
        }
        obj.All_in_Charges = txtAllInCharges.Text;
        if (txtHAWBfee.Text == "")
        {
            txtHAWBfee.Text = "-";
        }
        obj.HAWB_Fee = txtHAWBfee.Text;
        if (txtAirlinerDO.Text == "")
        {
            txtAirlinerDO.Text = "-";
        }
        obj.Airline_DO = txtAirlinerDO.Text;
        if (txtCCfee.Text == "")
        {
            txtCCfee.Text = "-";
        }
        obj.CC_Fee = txtCCfee.Text;
        if (txtIGMfee.Text == "")
        {
            txtIGMfee.Text = "-";
        }
        obj.IGM_Fee = txtIGMfee.Text;
        if (txtServiceTax.Text == "")
        {
            txtServiceTax.Text = "-";
        }
        obj.Service_Tax = txtServiceTax.Text;
        if (txtEx1.Text == "")
        {
            txtEx1.Text = "-";
        }
        obj.ex1 = txtEx1.Text;
        if (txtEx2.Text == "")
        {
            txtEx2.Text = "-";
        }
        obj.ex2 = txtEx2.Text;
        if (txtEx3.Text == "")
        {
            txtEx3.Text = "-";
        }
        obj.ex3 = txtEx3.Text;
        obj.CpId = cp.getPresentCompanySessionValue();

        MessageBox.Show(this, obj.PObyAir_Save());
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvPObyAir.SelectedIndex > -1)
        {
            try
            {
               // string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=SalaryAdvance&siid=" + gvSalAdv.SelectedRow.Cells[9].Text + "";

                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=POAIR&siid="+gvPObyAir.SelectedRow.Cells[22].Text+"";
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
   
    protected void lbtnId_Click(object sender, EventArgs e)
    {

        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvPObyAir.SelectedIndex = gvRow.RowIndex;

    }
}
 
