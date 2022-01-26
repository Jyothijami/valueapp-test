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
using vllib;

public partial class Modules_SM_ProcessStatus : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EmployeeNames_Fill();
        }
    }

    private void EmployeeNames_Fill()
    {
        try
        {
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeName);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlEmployeeName);
           // HR.EmployeeMaster.EmployeeMaster_SelectForSales(ddlEmployeeName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            HR.Dispose();
        }
    }


    protected void ddlNoHeads_SelectedIndexChanged(object sender, EventArgs e)
    {
        SerialNoFill();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        lblEmpIdHidden.Text = ddlEmployeeName.SelectedItem.Value;
        lblNameHidden.Text = ddlNoHeads.SelectedItem.Value;
        lblNoHidden.Text = ddlNos.SelectedItem.Value;
    }


    private void SerialNoFill()
    {
        if (ddlNoHeads.SelectedItem.Value == "0")
        {
            
        }
        else if (ddlNoHeads.SelectedItem.Value == "sl")
        {
            try
            {
                if (ddlEmployeeName.SelectedItem.Value == "0") { SM.SalesEnquiry.SalesEnquiry_Select(ddlNos); } else { SM.SalesEnquiry.SalesEnquiry_Select(ddlNos, ddlEmployeeName.SelectedItem.Value); }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
            finally
            {
                SM.Dispose();
            }
        }
        else if (ddlNoHeads.SelectedItem.Value == "qtn")
        {
            try
            {
                if (ddlEmployeeName.SelectedItem.Value == "0") { SM.SalesQuotation.SalesQuotation_Select(ddlNos); } else { SM.SalesQuotation.SalesQuotation_Select(ddlNos, ddlEmployeeName.SelectedItem.Value); }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
            finally
            {
                SM.Dispose();
            }
        }
        else if (ddlNoHeads.SelectedItem.Value == "so")
        {
            try
            {
                if (ddlEmployeeName.SelectedItem.Value == "0") { SM.SalesOrder.SalesOrder_Select(ddlNos); } else { SM.SalesOrder.SalesOrder_Select(ddlNos, ddlEmployeeName.SelectedItem.Value); }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
            finally
            {
                SM.Dispose();
            }
        }
        else if (ddlNoHeads.SelectedItem.Value == "op")
        {
            try
            {
                if (ddlEmployeeName.SelectedItem.Value == "0") { SM.WorkOrder.WorkOrder_SelectAll(ddlNos); } else { SM.WorkOrder.WorkOrder_Select(ddlNos, ddlEmployeeName.SelectedItem.Value); }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
            finally
            {
                SM.Dispose();
            }
        }
        else if (ddlNoHeads.SelectedItem.Value == "oa")
        {
            try
            {
                if (ddlEmployeeName.SelectedItem.Value == "0") { SM.OrderAcceptance.OrderAcceptance_Select(ddlNos); } else { SM.OrderAcceptance.OrderAcceptance_Select(ddlNos, ddlEmployeeName.SelectedItem.Value); }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
            finally
            {
                SM.Dispose();
            }
        }
        else if (ddlNoHeads.SelectedItem.Value == "dc")
        {
            try
            {
                if (ddlEmployeeName.SelectedItem.Value == "0") { Inventory.Delivery.Delivery_Select(ddlNos); } else { }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message.ToString()); }
            finally
            {
                Inventory.Dispose();
            }
        }
    }
    protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SerialNoFill();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        GridView1.DataBind();
    }
}

 
