using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data;
using Yantra.Classes;
using YantraBLL.Modules;
using Yantra.MessageBox;
public partial class Modules_Reports_SalesOrder_Search : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        if(!IsPostBack)
        {
            setControlsVisibility();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "OpenNews();", true);

            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            HR.EmployeeMaster.EmployeeMaster_SelectSales12(ddlResponsiblePerson);
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "127");
        btnExprot.Enabled = up.Email;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (rblBalQty.SelectedItem.Text == "All")
        {
            BindGrid_All();
            //gvShipment.Visible = false;
            //gvSalesOrder.Visible = true;
        }
        else if (rblBalQty.SelectedItem.Text == "Balance Qty != 0")
        {
            BindGrid_Cond();
            //gvShipment.Visible = false;
            //gvSalesOrder.Visible = true;
        }
        else if (rblBalQty.SelectedItem.Text == "Balance Qty = 0")
        {
            BindDelivered_All();
            //gvShipment.Visible = false;
            //gvSalesOrder.Visible = true;
        }
        else if (rblBalQty.SelectedItem.Text == "Blocked Qty != 0")
        {
            BindBlockedGrid_All();
        }
        //if (rblBalQty.SelectedItem.Value == "2")
        //{
        //    BindGrid_Shipment();
        //    //gvShipment.Visible = true;
        //    //gvSalesOrder.Visible = false;
        //}
        lblTtlOrderdQty.Text = TotalQty.ToString();
        lblTtlBalnceQty.Text = TotalBalQty.ToString();
        lblBlockedQty.Text = TotalBlocedQty.ToString();
        lblBlockedAmtTotal.Text = TotalBlockedAmt.ToString();
    }
    private void BindDelivered_All()
    {
        SqlCommand cmd = new SqlCommand("USP_SO_Details_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlBrand.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@DeliveredQty", 99);

        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ITEM_MODEL_NO", txtModelNo.Text);
        }
        if (txtPurchaseOrderNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_NO", txtPurchaseOrderNo.Text);
        }
        if (txtCustomerPO.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_CUST_PO_NO", txtCustomerPO.Text);
        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cust_Name", txtCustName.Text);
        }

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (ddlResponsiblePerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
    }
    private void BindBlockedGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_SO_Details_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlBrand.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@BlockedQty", 99);

        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ITEM_MODEL_NO", txtModelNo.Text);
        }
        if (txtPurchaseOrderNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_NO", txtPurchaseOrderNo.Text);
        }
        if (txtCustomerPO.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_CUST_PO_NO", txtCustomerPO.Text);
        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cust_Name", txtCustName.Text);
        }

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (ddlResponsiblePerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
    }
    private void BindGrid_Shipment()
    {
        gvShipment.Visible = true;
        SqlCommand cmd = new SqlCommand("USP_SO_ShipmentDetails_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlBrand.SelectedItem.Text);
        }
        //cmd.Parameters.AddWithValue("@BalanceQty", 99);

        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ITEM_MODEL_NO", txtModelNo.Text);
        }
        if (txtPurchaseOrderNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_NO", txtPurchaseOrderNo.Text);
        }
        if (txtCustomerPO.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_CUST_PO_NO", txtCustomerPO.Text);
        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cust_Name", txtCustName.Text);
        }

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (ddlResponsiblePerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvShipment.DataSource = dt;
        gvShipment.DataBind();
    }
    private void BindGrid_Cond()
    {
        SqlCommand cmd = new SqlCommand("USP_SO_Details_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlBrand.SelectedItem.Text);
        }
        cmd.Parameters.AddWithValue("@BalanceQty", 99);

        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ITEM_MODEL_NO", txtModelNo.Text);
        }
        if (txtPurchaseOrderNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_NO", txtPurchaseOrderNo.Text);
        }
        if (txtCustomerPO.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_CUST_PO_NO", txtCustomerPO.Text);
        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cust_Name", txtCustName.Text);
        }

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (ddlResponsiblePerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
    }
    
    private void BindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_SO_Details_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlBrand.SelectedItem.Text);
        }

        
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ITEM_MODEL_NO", txtModelNo.Text);
        }
        if (txtPurchaseOrderNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_NO", txtPurchaseOrderNo.Text);
        }
        if (txtCustomerPO.Text != "")
        {
            cmd.Parameters.AddWithValue("@SO_CUST_PO_NO", txtCustomerPO.Text);
        }
        if (txtCustName.Text != "")
        {
            cmd.Parameters.AddWithValue("@Cust_Name", txtCustName.Text);
        }

        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));
        }
        if (ddlResponsiblePerson.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Text);
        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvSalesOrder.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvSalesOrder.DataBind();



        //if (rblBalQty.SelectedItem.Value == "5")
        //{
        //    gvSalesOrder.Visible = false;
        //    gvShipment.Visible = true;
        //    gvShipment.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //    gvShipment.DataBind();
        //}
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
       // gvSalesOrder.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
    protected void btnExprot_Click(object sender, EventArgs e)
    {
        if(gvSalesOrder.Rows.Count > 0 )
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvSalesOrder.AllowPaging = false;
                if (rblBalQty.SelectedItem.Text == "All")
                {
                    this.BindGrid_All();

                }
                else
                {
                    this.BindGrid_Cond();
                }

                gvSalesOrder.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvSalesOrder.HeaderRow.Cells)
                {
                    cell.BackColor = gvSalesOrder.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvSalesOrder.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvSalesOrder.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvSalesOrder.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvSalesOrder.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
  
    
    //protected void gvSalesOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvSalesOrder.PageIndex = e.NewPageIndex;
    //    if (rblBalQty.SelectedItem.Text == "All")
    //    {
    //        BindGrid_All();
    //    }
    //    else if (rblBalQty.SelectedItem.Text == "Balance Qty != 0")
    //    {
    //        BindGrid_Cond();
    //    }
    //    else if (rblBalQty.SelectedItem.Text == "Blocked Qty != 0")
    //    {
    //        BindBlockedGrid_All();
    //    }
    //    else if (rblBalQty.SelectedItem.Text == "Balance Qty = 0")
    //    {
    //        BindDelivered_All();
    //    }
    //}
  
    
    
    
    
    
    
    int TotalQty = 0, TotalBalQty=0, TotalBlocedQty=0, TotalBlockedAmt=0;
    protected void gvSalesOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[19].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "0")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            TotalQty = TotalQty + int.Parse(e.Row.Cells[6].Text);
            TotalBalQty = TotalBalQty + int.Parse(e.Row.Cells[7].Text);
            TotalBlocedQty = TotalBlocedQty + int.Parse(e.Row.Cells[8].Text);
            e.Row.Cells[9].Text = Math.Round((Convert.ToDecimal(e.Row.Cells[19].Text) / Convert.ToDecimal(e.Row.Cells[6].Text)), 0).ToString();
            e.Row.Cells[10].Text = (Convert.ToInt32(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[9].Text)).ToString();
            TotalBlockedAmt = TotalBlockedAmt + int.Parse(e.Row.Cells[10].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = lblTtlOrderdQty.Text = TotalQty.ToString();
            e.Row.Cells[7].Text = lblTtlBalnceQty.Text = TotalBalQty.ToString();
            e.Row.Cells[8].Text = lblBlockedQty.Text = TotalBlocedQty.ToString();
            e.Row.Cells[10].Text = lblBlockedAmtTotal.Text = TotalBlockedAmt.ToString();

            e.Row.Cells[14].Visible = false;
            e.Row.Cells[19].Visible = false;

        }
    }
    protected void btnpost_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvSalesOrder.Rows)
            {
                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {

                    SM.SalesOrder obj = new SM.SalesOrder();
                    obj.logid  = "S" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                    obj.logtypeid ="Open";
                    string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
                    string wishs = txtSaledId.Text ;
                    HttpContext htc = System.Web.HttpContext.Current;
                    obj.logdesc = (wishs + " Remarks " + " - Posted By:  " + htc.Session["vl_username"].ToString());
                    obj.logcatid = "141";
                    obj.userid = gvrow.Cells[13].Text;
                    obj.dtadd = DateTime.Now.ToString();
                    //con.Open();
                    //log.add_Insert(wishs + " Remarks " + " - Posted By:  " + htc.Session["vl_username"].ToString(), SO_ID);
                    if (obj.log_commentsInsert() == "Data Saved Successfully")
                    {
                        txtSaledId.Text = "";
                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Inventory.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
    }
    protected void gvShipment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShipment.PageIndex = e.NewPageIndex;
        BindGrid_Shipment();
    }
}
 
