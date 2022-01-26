using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;

using System.Data;
using System.Data.SqlClient;
using vllib;
public partial class Modules_HR_MobileAdvaceReqForm : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();
        }
    }
    protected void setControlsVisibility()
    {
        //User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "43");
        //btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDetails();
        clearFields();
        gvMobileAdv.DataBind();
    }
    private void clearFields()
    {
        ddlEmployee.SelectedIndex = 0;
        txtAmount.Text = txtDate.Text = txtDesg.Text = txtEMI.Text = "";
    }
    private void SaveDetails()
    {
        Services.ServiceCourierInfo obj = new Services.ServiceCourierInfo();

        obj.Emp_Id = Convert.ToInt32(ddlEmployee.SelectedItem.Value);
        obj.Emp_Name = ddlEmployee.SelectedItem.Text;
        obj.Date = Yantra.Classes.General.toMMDDYYYY(txtDate.Text);
        if (txtAmount.Text == "")
        {
            txtAmount.Text = "0";
        }
        obj.Amount = Convert.ToDecimal(txtAmount.Text);
        obj.Desg = txtDesg.Text;
        if (txtEMI.Text == "")
        {
            txtEMI.Text = "0";
        }
        obj.SalAdv = Convert.ToDecimal(txtEMI.Text);
        obj.ExtraField = txtExtraField.Text;
        MessageBox.Show(this, obj.Mobile_Advance_Save());
    }
    protected void lbtnId_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvMobileAdv.SelectedIndex = gvRow.RowIndex;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvMobileAdv.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=MobileAdvance&siid=" + gvMobileAdv.SelectedRow.Cells[7].Text + "";
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
}