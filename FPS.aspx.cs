using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class FPS : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            //Response.Redirect("~/MobileLogin.aspx");
            Response.Redirect("~/FPSLogin.aspx?url=" + Server.UrlEncode(Request.Url.AbsoluteUri));

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.Architect.Architect_Select(ddlArchitect);
            CustomerName_Fill();
            txtArAmount.Text = "0";
            txtPOAmt1.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtPerc.Attributes.Add("onkeyup", "javascript:amtcalc();");
            txtArAmount.Attributes.Add("onkeyup", "javascript:amtcalcDisc();");

            BindGrid();
        }
    }

    #region SalesOrder Fill
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlSalesOrderNo, ddlCustomerName.SelectedValue);
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

    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.InvoiceCustomerMaster_SelectForCustomer(ddlCustomerName);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlResponsiblePerson);

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

    private void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("select FPS_ID,ARCHITECT_NAME,Name ,SO_NO,CUST_NAME ,po_Amt,PO_Amt1,Percntage,TotalAmt,Status,Remarks,fps_dt from fps_tbl left outer join YANTRA_LKUP_ARCHITECT on fps_tbl.Architect_id=YANTRA_LKUP_ARCHITECT .ARCHITECT_ID inner join YANTRA_SO_MAST on fps_tbl.so_Id =YANTRA_SO_MAST .SO_ID inner join YANTRA_CUSTOMER_MAST on YANTRA_SO_MAST .SO_CUST_ID =YANTRA_CUSTOMER_MAST .CUST_ID order by fps_id desc ", con);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvFPS.DataSource = dt;
        gvFPS.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Masters.Architect obj = new Masters.Architect();
        obj.SO_ID = ddlSalesOrderNo.SelectedItem.Value;
        obj.Architect_Id = ddlArchitect.SelectedItem.Value;
        obj.PO_Amt = txtPOAmt.Text;
        obj.PO_Amt1 = txtPOAmt1.Text;
        obj.Percntage = txtPerc.Text;
        obj.TotalAmt = txtArAmount.Text;
        obj.Status = "Open";
        obj.Remarks = txtArRemarks.Text;
        obj.FPS_Dt = DateTime.Now.ToString();
        obj.Architect_Name = txtArchitect.Text;
        obj.Executive_ID = ddlResponsiblePerson.SelectedItem.Value;
        obj.Prepared_By = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        obj.Dispatch_Id = "";
        obj.Cust_ID = ddlCustomerName .SelectedItem.Value;
        MessageBox.Show(this, obj.FPS_Save());
        gvFPS.DataBind();
    }

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (txtSearchModel.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource1";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
            //  ddlModelNo_SelectedIndexChanged(sender, e);

            
            
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }

    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSalesOrderNo.SelectedValue = "0";
        SalesOrder_Fill();
        Masters.Architect objAr = new Masters.Architect();

        objAr.FPS_SelectCust(ddlCustomerName.SelectedItem.Value, gvFPS);
        gvFPS.DataBind();

        
    }
    protected void ddlArchitect_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtArchitect.Text = ddlArchitect.SelectedItem.Text;
    }
}