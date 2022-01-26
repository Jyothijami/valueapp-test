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
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.IO;
using vllib;
public partial class Modules_Inventory_Statementprint : System.Web.UI.Page
{
    decimal TotalAmount = 0;
    decimal totalamount6 = 0;
    decimal totalamount7 = 0;
    decimal totalamount8 = 0;
    decimal totalamount9 = 0;
    decimal totalamount10 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            CustomerName_Fill();
            SalesOrder_Fill1();
            gvSupliedMaterialExtra.DataBind();
            GvSuppliedMaterial.DataBind();
            gvInvoiceMaterial.DataBind();
            gvreturnmaterial.DataBind();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "74");

        //btnSearchModelNo.Enabled = up.SearchModelNo;
        btnPrint1.Enabled = up.Print;
        //Button1.Enabled = up.ExportExcel;
    }

    #region CustomerName Fill
    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectForCustomer(ddlCustomerName);
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
    #region SalesOrder1 Fill
    private void SalesOrder_Fill1()
    {
        try
        {
            SM.SalesOrder.SalesOrder_SelectByCustomerId(ddlPONO, ddlCustomerName.SelectedValue);
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
    protected void gvSupliedMaterialExtra_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) / (Convert.ToDouble(e.Row.Cells[4].Text))).ToString("F");
            e.Row.Cells[8].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[3].Text)) * (Convert.ToDecimal(e.Row.Cells[7].Text))).ToString("F");
            totalamount7 = totalamount7 + Convert.ToDecimal(e.Row.Cells[8].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Text = "TotalAmount:";
            e.Row.Cells[8].Text = totalamount7.ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
    }
    protected void gvInvoiceMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text));

        }
    }
    protected void gvreturnmaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[9].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[5].Text));
            totalamount8 = totalamount8 + Convert.ToDecimal(e.Row.Cells[9].Text);
            //  lblSalesReturnAmount.Text = Convert.ToDecimal(TotalAmount).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Total Amount:";
            e.Row.Cells[9].Text = totalamount8.ToString();
        }
    }
    protected void btnPrint1_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../HR/Print.aspx','PrintMe','height=600,width=900,scrollbars=yes');</script>");
    }

    protected void gvInvoiceMaterial_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
           
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[5].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[4].Text)) * (Convert.ToDecimal(e.Row.Cells[8].Text))).ToString("F");
            // totalamount10 = totalamount10 + Convert.ToDecimal(e.Row.Cells[7].Text);
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalamount9 = totalamount9 + Convert.ToDecimal(e.Row.Cells[5].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[4].Text = "Total Amount:";
            e.Row.Cells[5].Text = totalamount9.ToString();
        }
    }


    protected void ddlPONO_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.SalesOrder objSM = new SM.SalesOrder();
        if (objSM.SalesOrder_Select(ddlPONO.SelectedItem.Value) > 0)
        {

            lblTerms.Text = objSM.SOOtherSpec;
        }
        objSM.SalesOrderDetailsStatement_Select(ddlPONO.SelectedItem.Value, gvItemDetails);
        objSM.SalesOrderDetailsBalanceQty_Select(ddlPONO.SelectedItem.Value, gvPendingMaterial);

        Inventory.Delivery objDelivery = new Inventory.Delivery();
        objDelivery.DeliveryDetailsPOPrint_SelectInvoice(ddlPONO.SelectedItem.Value, GvSuppliedMaterial);
        objDelivery.DeliveryDetailsPOExtraPrint_SelectInvoice(ddlPONO.SelectedItem.Value, gvSupliedMaterialExtra);
        objDelivery.DeliveryDetails_SelectInvoiceSI(ddlPONO.SelectedItem.Value, gvInvoiceMaterial);

        Inventory.SalesReturn objInventory = new Inventory.SalesReturn();
        objInventory.SalesReturnDetailsSO1_Select(ddlPONO.SelectedItem.Value, gvreturnmaterial);
    }
    protected void GvSuppliedMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Text = ((Convert.ToDouble(e.Row.Cells[6].Text)) / (Convert.ToDouble(e.Row.Cells[4].Text))).ToString("F");
            e.Row.Cells[8].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[3].Text)) * (Convert.ToDecimal(e.Row.Cells[7].Text))).ToString("F");
            totalamount6 = totalamount6 + Convert.ToDecimal(e.Row.Cells[8].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Text = "TotalAmount:";
            e.Row.Cells[8].Text = totalamount6.ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

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
        SalesOrder_Fill1();
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedValue) > 0)
        {
            //ddlCustomerName.SelectedItem.Text = objSMCustomer.CustName;
            //txtCustomerName.Text = objSMCustomer.CustName;
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
        }
    }
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[5].Visible = false;
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[4].Text))).ToString("F");

            e.Row.Cells[7].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[6].Text)) * (Convert.ToDecimal(e.Row.Cells[4].Text))).ToString("F");
            //e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[4].Text)) * (Convert.ToDouble(e.Row.Cells[5].Text))).ToString();
            TotalAmount = TotalAmount + Convert.ToDecimal(e.Row.Cells[7].Text);
            //  lblPoAmount.Text = Convert.ToDecimal(TotalAmount).ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Text = "Total Amount:";
            e.Row.Cells[7].Text = TotalAmount.ToString();
          
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.ContentType = "application/vnd.xls";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=1.xls");
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        System.Web.UI.Page sPage = new System.Web.UI.Page();
        sPage.RenderControl(htmlWrite);
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.Flush();



        //Response.ClearContent();
        //string filename = "Output" + "Stm" + ".xls";
        //Response.AddHeader("content-disposition", "attachment; filename=" + filename + ";");
        //Response.ContentType = "application/excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);

        //divExport.RenderControl(htw);


        //Response.Write(sw.ToString());
        //Response.End();




    }
    protected void gvPendingMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Text = ((Convert.ToDouble(e.Row.Cells[8].Text)) / (Convert.ToDouble(e.Row.Cells[9].Text))).ToString("F");
            e.Row.Cells[7].Text = Convert.ToDecimal((Convert.ToDecimal(e.Row.Cells[6].Text)) * (Convert.ToDecimal(e.Row.Cells[4].Text))).ToString("F");
            totalamount10 = totalamount10 + Convert.ToDecimal(e.Row.Cells[7].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Text = "Total Amount:";
            e.Row.Cells[7].Text = totalamount10.ToString();
        }
    }
}

 
