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
        //System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        if(!IsPostBack)
        {
            setControlsVisibility();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "OpenNews();", true);
            ddlCust.Visible = true;
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            HR.EmployeeMaster.EmployeeMaster_SelectSales12(ddlResponsiblePerson);
            HR.EmployeeMaster.EmployeeMaster_SelectInactiveSales12(ddlInactiveResponsible);

            CustomerName_Fill();
            btnReserve2.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnReserve2, null) + ";");

           
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "127");
        User_Permissions up1 = new User_Permissions(Session["vl_userid"].ToString(), "148");

        btnExprot.Enabled = up.Email;
        btnReserve2.Enabled = up1.add;
    }

    private void CustomerName_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCust);
            SM.CustomerMaster.BlockedCustBind(ddlBlockedCust);

        }
        catch (Exception ex) { }
    }

    
    private void BindIndent()
    {
        foreach (GridViewRow gvrow in gvSalesOrder.Rows)
        {
            GridView gvIndent = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[5].FindControl("gvIndent"));
            //if (gvrow.Cells[27].Text == "&nbsp;") { gvrow.Cells[27].Text = "0"; }

            SqlCommand cmd = new SqlCommand("select IND_DET_QTY ,IND_DET_ID  from YANTRA_INDENT_DET left outer join YANTRA_SO_DET on YANTRA_SO_DET .SO_DET_ID =YANTRA_INDENT_DET .IND_DET_SO_ID where SO_DET_ID ='" + gvrow.Cells[15].Text + "' ", con);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvIndent.DataSource = dt;
            gvIndent.DataBind();
            BindFPO();
        }
    }

    private void BindBlockStock()
    {
        foreach (GridViewRow gvrow in gvSalesOrder.Rows)
        {
            GridView gvBlock = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[5].FindControl("gvBlock"));
            //if (gvrow.Cells[27].Text == "&nbsp;") { gvrow.Cells[27].Text = "0"; }

            SqlCommand cmd = new SqlCommand("select distinct isnull((select SUM(Quantity) from BLOCK_New where item_code=YANTRA_SO_DET.item_code and colour_id=YANTRA_SO_DET.COLOR_ID and SO_Id=p.SO_Id and Customer_Id=YANTRA_SO_MAST.SO_CUST_ID),0) as Blocked_Qty,so_det_Price,SO_DET_QTY FROM  YANTRA_SO_DET INNER JOIN YANTRA_SO_MAST ON YANTRA_SO_DET.SO_ID = YANTRA_SO_MAST.SO_ID left outer join YANTRA_WO_MAST on YANTRA_SO_MAST .SO_ID =YANTRA_WO_MAST  .SO_ID left outer join BlOCK_New p on YANTRA_WO_MAST .WO_ID =p .SO_Id where YANTRA_SO_DET.ITEM_CODE ='" + gvrow.Cells[29].Text + "' and YANTRA_SO_MAST.SO_ID = '" + gvrow.Cells[31].Text + "' group by p.SO_Id ,YANTRA_SO_DET.item_code,YANTRA_SO_DET.COLOR_ID ,YANTRA_SO_MAST.SO_Id,SO_CUST_ID,so_det_Price,SO_DET_QTY ", con);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvBlock.DataSource = dt;
            gvBlock.DataBind();
            
        }
    }

    private void BindFPO()
    {
        foreach (GridViewRow gvrow in gvSalesOrder.Rows)
        {
            GridView gvIndent = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[20].FindControl("gvIndent"));
            foreach (GridViewRow gvIndentRow in gvIndent.Rows)
            {
                GridView gvFPO = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[21].FindControl("gvFPO"));
                //if (gvrow.Cells[27].Text == "&nbsp;") { gvrow.Cells[27].Text = "0"; }

                SqlCommand cmd = new SqlCommand("select FPO_DET_QTY ,FPO_DET_ID  from YANTRA_FIXED_PO_DET left outer join YANTRA_INDENT_DET on  YANTRA_INDENT_DET .IND_DET_ID =YANTRA_FIXED_PO_DET .FPO_DET_IND_DET_ID where IND_DET_ID ='" + gvIndentRow.Cells[1].Text + "' ", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvFPO.DataSource = dt;
                gvFPO.DataBind();
                
            }
           

        }
    }
    private void BindInvoiceQty()
    {
        foreach (GridViewRow gvrow in gvSalesOrder.Rows)
        {
            GridView gvFPO = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[21].FindControl("gvFPO"));
            foreach (GridViewRow gvFPORow in gvFPO.Rows)
            {
                GridView gvDC = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[22].FindControl("gvDC"));

                SqlCommand cmd = new SqlCommand("select PI_DET_QTY ,Status ,CONVERT(VARCHAR(10),YANTRA_PURCHASE_INVOICE_DET .AppDeliveryDt ,103) as  AppDeliveryDt,PI_DET_Customer  from YANTRA_PURCHASE_INVOICE_DET left outer join YANTRA_FIXED_PO_DET on YANTRA_PURCHASE_INVOICE_DET .PI_PONo =YANTRA_FIXED_PO_DET .FPO_DET_ID where FPO_DET_ID ='" + gvFPORow.Cells[1].Text + "' ", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvDC.DataSource = dt;
                gvDC.DataBind();
            }
        }
    }
    private void BindStock()
    {
        foreach (GridViewRow gvrow in gvSalesOrder.Rows)
        {
            GridView gvStock = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[5].FindControl("gvStock"));
            
            SqlCommand cmd = new SqlCommand("SELECT(isnull((select SUM(Quantity) from V_INWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)-isnull((select SUM(Quantity) from V_OUTWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)) as TOTAL_STOCK,isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.COLOUR_ID and locid=p.locid),0) " +
                                              "as TOTAL_BLOCK_Stock,(isnull((select SUM(Quantity) from V_INWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)- isnull((select SUM(Quantity) from V_OUTWARDNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0) -isnull((select SUM(Quantity) from V_BLOCKNew where item_code=p.item_code and colour_id=p.colour_id and locid=p.locid),0)) " +
                                               " as TOTAL_AVALIABLE_STOCK,p.locname,p.locid,'" + gvrow.Cells[6].Text + "' as POQty from V_INWARDNew  p left join V_OUTWARDNew out on p.item_code=out.item_code left join V_BLOCKNew blo on p.item_code=blo.item_code  where p.ITEM_CODE = '" + gvrow.Cells[29].Text + "' and p.COLOUR_ID = '" + gvrow.Cells[30].Text + "' and p.locid = '" + gvrow.Cells[28].Text + "' group by p.item_code,p.colour_id,p.locid,p.locname", con);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvStock.DataSource = dt;
            gvStock.DataBind();

        }
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
            //txtCustName.Visible = false;
            ddlCust.Visible = false ;
            BindBlockedGrid_All();
        }


        //else if (rblBalQty.SelectedItem.Text == "Running")
        //{
        //    BindGridRunning_All();
        //}
        //else if (rblBalQty.SelectedItem.Text == "Closed")
        //{
        //    BindGrid_All();
        //}




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

    private void BindGridRunning_All()
    {
        SqlCommand cmd = new SqlCommand("USP_SALESORDERpHA_Details_Search", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@statusCond1", "New");
        cmd.Parameters.AddWithValue("@statusCond2", "Open");
        cmd.Parameters.AddWithValue("@statusCond3", "Closed");

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
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlCust.SelectedItem.Value);

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
       
        BindIndent();
        BindInvoiceQty();  BindBlockStock();
        BindStock(); BindAnnexure(); 
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
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        if (ddlCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlCust.SelectedItem.Value);

        }
        if (ddlstatus.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@status", ddlstatus.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
        BindIndent();
        BindInvoiceQty();  BindBlockStock();
        BindStock(); BindAnnexure(); 
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
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        if (ddlBlockedCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlBlockedCust .SelectedItem.Value);

        }
        if (ddlstatus.SelectedItem.Value == "ManuallyClosed")
        {
            cmd.Parameters.AddWithValue("@status", ddlstatus.SelectedItem.Value);
        }
        if (ddlstatus.SelectedItem.Value == "Closed") 
        {
            cmd.Parameters.AddWithValue("@StatusClosed", ddlstatus.SelectedItem.Value);
            
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
       
        BindIndent();
        BindInvoiceQty();  BindBlockStock();
        BindStock(); BindAnnexure(); 
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
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        if (ddlCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlCust.SelectedItem.Value);

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
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        if (ddlCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlCust.SelectedItem.Value);

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
       
        BindIndent();
        BindInvoiceQty();  BindBlockStock();
        BindStock(); BindAnnexure(); 
    }
    private void BindAnnexure()
    {
        foreach (GridViewRow gvrow in gvSalesOrder.Rows)
        {
            GridView gvAnnexure = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[5].FindControl("gvAnnexure"));
            //if (gvrow.Cells[27].Text == "&nbsp;") { gvrow.Cells[27].Text = "0"; }

            SqlCommand cmd = new SqlCommand("select Annexure_Qty,CONVERT (nvarchar(50),wo_date,103) as WO_Date from YANTRA_WO_MAST  inner join YANTRA_WO_DET on YANTRA_WO_MAST .WO_ID =YANTRA_WO_DET .WO_ID left outer join YANTRA_SO_MAST on YANTRA_WO_MAST .SO_ID =YANTRA_SO_MAST .SO_ID where YANTRA_WO_DET .ITEM_CODE='" + gvrow.Cells[29].Text + "' and YANTRA_WO_MAST.SO_ID ='" + gvrow.Cells[31].Text + "' ", con);
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvAnnexure.DataSource = dt;
            gvAnnexure.DataBind();

        }
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
        if (ddlResponsiblePerson.SelectedIndex != 0 )
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlCust.SelectedItem.Value);

        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        if (ddlstatus.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@status", ddlstatus.SelectedItem.Value);
        }



        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
      
        BindIndent();
        BindInvoiceQty();  BindBlockStock();
        BindStock(); BindAnnexure(); 
    }


    protected void btnReserve2_Click(object sender, EventArgs e)
    {
        try
        {
            int qty_Reserved = 0;
            int qty_Actual = 0;
            

            foreach (GridViewRow gvrow in gvSalesOrder.Rows)
            {
                
                    CheckBox ch = new CheckBox();
                    ch = (CheckBox)gvrow.FindControl("chk");
                    if (ch.Checked == true)
                    {
                        GridView gvStock = (GridView)(gvSalesOrder.Rows[gvrow.RowIndex].Cells[34].FindControl("gvStock"));
                        foreach (GridViewRow gvStockRow in gvStock.Rows)
                        {
                            TextBox reqQty = (TextBox)gvStockRow.FindControl("txtQuantity");
                        int qty = int.Parse(reqQty.Text);

                        qty_Actual = qty_Actual + qty;

                        int avlQty = Convert.ToInt32(gvStockRow.Cells[1].Text);
                        if (avlQty < qty)
                        {
                            MessageBox.Show(this, "Required Quantity is not Avaliable in Stock for Item  " + gvrow.Cells[1].Text + "");
                            return;
                        }
                        else
                        {
                            string Itemcode = gvrow.Cells[29].Text;
                            string ColorId = gvrow.Cells[30].Text;
                            string locId = gvrow.Cells[28].Text;

                            SqlCommand cmd = new SqlCommand("select Item_ID,whLocId,cp_id,Quantity from [V_LocAvaliableItems] where Quantity>0 and ITEM_CODE=" + Itemcode + " and [COLOUR_ID]=" + ColorId + " and [locid]=" + locId + "  order by whname  ", con);
                            cmd.CommandType = CommandType.Text;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);


                            qty = int.Parse(reqQty.Text);
                            int qty2 = int.Parse(reqQty.Text);

                            string confirm = "";

                            if (dt.Rows.Count > 0)
                            {
                                Masters.ItemPurchase obj = new Masters.ItemPurchase();

                                for (int i = 0; i < qty2; i++)
                                {

                                    if (qty >= Convert.ToInt32(dt.Rows[i][3]))
                                    {
                                        obj.Quantity = dt.Rows[i][3].ToString();
                                    }

                                    else if (qty < Convert.ToInt32(dt.Rows[i][3]))
                                    {
                                        obj.Quantity = qty.ToString();
                                    }

                                    obj.itemcode = gvrow.Cells[29].Text;

                                    obj.ItemID = dt.Rows[i][0].ToString();
                                    obj.whLocId = dt.Rows[i][1].ToString();
                                    obj.Barcode = dt.Rows[i][0].ToString();
                                    obj.companyid = dt.Rows[i][2].ToString();

                                    // obj.companyid = lblCPID.Text;
                                    obj.POID = gvrow.Cells[34].Text;
                                    obj.COLORID = gvrow.Cells[30].Text;
                                    obj.status = "Blocked";
                                    obj.DeliveryDate = gvrow.Cells[33].Text;
                                    obj.CustomerId = gvrow.Cells[32].Text;

                                    string msg = obj.BlockNew_Save2();

                                    if (msg == "Data Saved Successfully")
                                    {
                                        qty = qty - Convert.ToInt32(dt.Rows[i][3]);

                                        qty_Reserved = qty_Reserved + Convert.ToInt32(obj.Quantity);
                                    }

                                    if (qty <= 0)
                                    {
                                        confirm = "Successful";
                                        break;
                                    }
                                }
                            }

                            if (confirm == "Successful")
                            {
                                SM.SalesOrder objSM = new SM.SalesOrder();
                                objSM.SODetId = gvrow.Cells[15].Text;
                                // objSM.BalanceQty = qty1.Text;                   
                                objSM.BalanceQty = (qty2 + Convert.ToInt32(gvrow.Cells[8].Text)).ToString();
                                objSM.SalesOrderStatus_Update();
                            }

                        }
                    }
                }
            }
            if (qty_Reserved == qty_Actual)
            {
                MessageBox.Show(this, "Stock Reserved Successfully");
                //tblWorkOrderDetails.Visible = false;
                BindGrid_All();
            }
            else
            {
                MessageBox.Show(this, qty_Reserved.ToString() + ", Stock got reserved instead of " + qty_Actual.ToString() + ", Please Re-check and confirm.");

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }

    }



    private void OpenBindGrid_All()
    {
        SqlCommand cmd = new SqlCommand("USP_SALESORDERpHA_Details_Search", con);
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
            cmd.Parameters.AddWithValue("@EmpName", ddlResponsiblePerson.SelectedItem.Value);
        }
        if (ddlInactiveResponsible.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@EmpName", ddlInactiveResponsible.SelectedItem.Value);

        }
        if (ddlCust.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CustId", ddlCust.SelectedItem.Value);

        }
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Company_Id", ddlCompany.SelectedItem.Value);
        }
        if (ddlstatus.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@status", ddlstatus.SelectedItem.Value);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvSalesOrder.DataSource = dt;
        gvSalesOrder.DataBind();
       
        BindIndent();
        BindInvoiceQty();  BindBlockStock();
        BindStock(); BindAnnexure(); 
    }

















    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSalesOrder.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvSalesOrder.DataBind();
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
        gvSalesOrder.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

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
    protected void gvSalesOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalesOrder.PageIndex = e.NewPageIndex;
        //if (rblBalQty.SelectedItem.Text == "All")
        //{
        //    BindGrid_All();
        //}
        //else if (rblBalQty.SelectedItem.Text == "Balance Qty != 0")
        //{
        //    BindGrid_Cond();
        //}
        //else if (rblBalQty.SelectedItem.Text == "Blocked Qty != 0")
        //{
        //    BindBlockedGrid_All();
        //}
        //else if (rblBalQty.SelectedItem.Text == "Balance Qty = 0")
        //{
        //    BindDelivered_All();
        //}

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
            //txtCustName.Visible = false;
            ddlCust.Visible = false;
            BindBlockedGrid_All();
        }


        //else if (rblBalQty.SelectedItem.Text == "Running")
        //{
        //    BindGridRunning_All();
        //}
        //else if (rblBalQty.SelectedItem.Text == "Closed")
        //{
        //    BindGrid_All();
        //}




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
    int TotalQty = 0, TotalBalQty = 0, TotalBlocedQty = 0, TotalBlockedAmt=0;

    protected void gvBlock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = Math.Round((Convert.ToDecimal(e.Row.Cells[1].Text) / Convert.ToDecimal(e.Row.Cells[2].Text)), 0).ToString();
            e.Row.Cells[4].Text = (Convert.ToInt32(e.Row.Cells[0].Text) * Convert.ToDecimal(e.Row.Cells[3].Text)).ToString();

        }
    }
    protected void gvSalesOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            //e.Row.Cells[21].Visible = false;
            e.Row.Cells[26].Visible = false;
            e.Row.Cells[25].Visible = false;
            e.Row.Cells[27].Visible = false;
            e.Row.Cells[28].Visible = false;
            e.Row.Cells[29].Visible = false;
            e.Row.Cells[30].Visible = false;
            e.Row.Cells[31].Visible = false;
            e.Row.Cells[32].Visible = false;
            e.Row.Cells[33].Visible = false;
            e.Row.Cells[34].Visible = false;


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text == "0")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (e.Row.Cells[26].Text == "ManuallyClosed")
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;

            }
            TotalQty = TotalQty + int.Parse(e.Row.Cells[6].Text);
            TotalBalQty = TotalBalQty + int.Parse(e.Row.Cells[7].Text);
            //TotalBlocedQty = TotalBlocedQty + int.Parse(e.Row.Cells[8].Text);
            e.Row.Cells[9].Text = Math.Round((Convert.ToDecimal(e.Row.Cells[19].Text) / Convert.ToDecimal(e.Row.Cells[6].Text)), 0).ToString();
            //e.Row.Cells[10].Text = (Convert.ToInt32(e.Row.Cells[8].Text) * Convert.ToDecimal(e.Row.Cells[9].Text)).ToString();
            //TotalBlockedAmt = TotalBlockedAmt + int.Parse(e.Row.Cells[10].Text);

            if (e.Row.Cells[24].Text != "0" && e.Row.Cells[25].Text != "") { e.Row.Cells[24].Text = "Running"; }
            if (e.Row.Cells[24].Text == "0" && e.Row.Cells[25].Text != "") { e.Row.Cells[24].Text = "Closed"; }
            if (e.Row.Cells[26].Text == "ManuallyClosed") { e.Row.Cells[24].Text = "ManuallyClosed"; }
            if (e.Row.Cells[26].Text == "Obsolete") { e.Row.Cells[24].Text = "Obsolete"; }

           
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = lblTtlOrderdQty.Text = TotalQty.ToString();
            e.Row.Cells[7].Text = lblTtlBalnceQty.Text = TotalBalQty.ToString();
            //e.Row.Cells[8].Text = lblBlockedQty.Text = TotalBlocedQty.ToString();
            //e.Row.Cells[10].Text = lblBlockedAmtTotal.Text = TotalBlockedAmt.ToString();

            

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
                    obj.userid = gvrow.Cells[15].Text;
                    obj.dtadd = DateTime.Now.ToString();
                    //con.Open();
                    //log.add_Insert(wishs + " Remarks " + " - Posted By:  " + htc.Session["vl_username"].ToString(), SO_ID);
                    if (obj.log_commentsInsert() == "Data Saved Successfully")
                    {
                        txtSaledId.Text = "";
                        ch.Checked = false;
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
    protected void ddlResponsiblePerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.Exec_CustBind(ddlCust, ddlResponsiblePerson.SelectedValue);
            SM.CustomerMaster.Exec_BlockedCustBind(ddlBlockedCust , ddlResponsiblePerson.SelectedValue);

        }
        catch (Exception ex)
        {

        }
    }
    protected void rblBalQty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblBalQty.SelectedItem.Text == "Blocked Qty != 0")
        {
            lblstatus.Visible = true;
            ddlstatus.Visible = true;
            ddlBlockedCust.Visible = true;
            ddlCust.Visible = false;
        }
        else
        {
            lblstatus.Visible = false;
            ddlstatus.Visible = false;
            ddlBlockedCust.Visible = false;
            ddlCust.Visible = true;
        }
    }
    protected void ddlInactiveResponsible_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster.Exec_CustBind(ddlCust, ddlInactiveResponsible.SelectedValue);
        SM.CustomerMaster.Exec_BlockedCustBind(ddlBlockedCust, ddlInactiveResponsible.SelectedValue);
    }
    protected void gvIndent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
    protected void gvFPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
    
}
 
