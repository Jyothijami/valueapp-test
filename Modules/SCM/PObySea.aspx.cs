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


public partial class Modules_SCM_PObySea : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsPostBack)
        //{
        //    txtDestinationCharges.Visible = txtDOCharges.Visible = txtEx1.Visible = txtEx2.Visible = txtEx3.Visible = txtEx4.Visible = txtEx5.Visible = txtExworks.Visible = false;
        //    txtLowSulphurSurcharge.Visible = txtOceanFreight.Visible = txtShippingLineCharges.Visible = false;

        //}
    }

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDetails();
        clearFields();
        gvPObySea.DataBind();
    }
    private void clearFields()
    {
        txtDate.Text = txtDestinationCharges.Text = txtDOCharges.Text = txtExworks.Text = txtLowSulphurSurcharge.Text = txtName.Text =
            txtOceanFreight.Text = txtPONo.Text = txtRef.Text = txtRevisedDate.Text = txtShipperName.Text = txtTerms.Text = txtShippingLineCharges.Text = "";
        txtEx1.Text = txtEx2.Text = txtEx3.Text = txtEx4.Text = txtEx5.Text = "";
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
        if (txtOceanFreight.Text == "")
        {
            txtOceanFreight.Text = "-";
        }
        obj.Freight = txtOceanFreight.Text;
        if (txtLowSulphurSurcharge.Text == "")
        {
            txtLowSulphurSurcharge.Text = "-";
        }
        obj.Low_Sul_Charge = txtLowSulphurSurcharge.Text;
        if (txtExworks.Text == "")
        {
            txtExworks.Text = "-";
        }
        obj.Exworks = txtExworks.Text;
        if (txtDestinationCharges.Text == "")
        {
            txtDestinationCharges.Text = "-";
        }
        obj.Destin_Charges = txtDestinationCharges.Text;
        if (txtShippingLineCharges.Text == "")
        {
            txtShippingLineCharges.Text = "-";
        }
        obj.Shipping_Charges = txtShippingLineCharges.Text;
        if (txtDOCharges.Text == "")
        {
            txtDOCharges.Text = "-";
        }
        obj.DO_Charges = txtDOCharges.Text;
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
        if (txtEx4.Text == "")
        {
            txtEx4.Text = "-";
        }
        obj.ex4 = txtEx4.Text;
        if (txtEx5.Text == "")
        {
            txtEx5.Text = "-";
        }
        obj.ex5 = txtEx5.Text;
        obj.CpId = cp.getPresentCompanySessionValue();

        MessageBox.Show(this, obj.PObySea_Save());
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvPObySea.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=POSEA&siid=" + gvPObySea.SelectedRow.Cells[20].Text + "";
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
        gvPObySea.SelectedIndex = gvRow.RowIndex;
    }
}
 
