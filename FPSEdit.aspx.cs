using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class FPSEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Qid = Request.QueryString["Cid"].ToString();
        if (!IsPostBack)
        {
            Masters.Architect.Architect_Select(ddlArchitect);
            CustomerName_Fill();
            SalesOrder_Fill();
            Masters.Architect obj = new Masters.Architect();
            if (obj.FPS_Select1(Request.QueryString["Cid"].ToString()) > 0)
            {
                ddlSalesOrderNo.SelectedValue  = obj.SO_ID;
                ddlArchitect.SelectedValue  = obj.Architect_Id;
                txtArchitect.Text = obj.Architect_Name;
                txtPOAmt.Text = obj.PO_Amt;
                txtPOAmt1.Text = obj.PO_Amt1;
                txtPerc.Text = obj.Percntage;
                txtArAmount.Text = obj.TotalAmt;
                txtArRemarks.Text = obj.Remarks;
                ddlStatus.SelectedValue  = obj.Status;
            }
        }
    }

    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.InvoiceCustomerMaster_SelectForCustomer(ddlCustomerName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_Select(ddlSalesOrderNo);

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Masters.Architect obj = new Masters.Architect();

        obj.FPS_Id = Request.QueryString["Cid"].ToString();
        obj.PO_Amt = txtPOAmt.Text;
        obj.PO_Amt1 = txtPOAmt1.Text;
        obj.Percntage = txtPerc.Text;
        obj.TotalAmt = txtArAmount.Text;
        obj.Status = ddlStatus.SelectedItem.Value;
        obj.Remarks = txtArRemarks.Text;
        obj.FPS_Dt = DateTime.Now.ToString();
        obj.Architect_Name = txtArchitect.Text;
        obj.Architect_Id = ddlArchitect.SelectedItem.Value;
        obj.Executive_ID = "";
        obj.Prepared_By = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.Dispatch_Id = "";
        obj.Cust_ID = "";
        MessageBox.Show(this, obj.FPS_Update());
    }
}