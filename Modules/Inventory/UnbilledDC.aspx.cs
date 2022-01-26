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
using System.IO;
using vllib;
using YantraBLL.Modules;
using System.Data.SqlClient;
using Yantra.MessageBox;
using System.Drawing;
using System.Text;
public partial class Modules_Inventory_UnbilledDC : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblUserName.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpUserName);
            lblUserId.Text = usre.getUserID(lblUserName.Text);
            DataTable dt = Yantra.Authentication.Execute_Command("SELECT [CP_ID]  FROM [user_Company_Access_tbl] where permission=1 and USER_ID='" + lblUserId.Text + "' order by [CP_ID]  ", "Select");
            lblCp_Ids.Text = Yantra.Authentication.GetCompIds(dt);
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            Masters.CompanyProfile.Company_Select(ddlCpId);
            Masters.CompanyProfile.Company_Select(ddlCpName );

            //BindGrid();
            
        }
    }
    private void BindGrid()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("USP_UnbilledDCSales", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchItemName", lblSearchItemHidden.Text);
            cmd.Parameters.AddWithValue("@SearchValue", lblSearchValueHidden.Text);

            cmd.Parameters.AddWithValue("@SearchType", lblSearchTypeHidden.Text);
            cmd.Parameters.AddWithValue("@SearchValueFrom", lblSearchValueFromHidden.Text);

            //cmd.Parameters.AddWithValue("@EMPID", lblEmpIdHidden.Text);
            //cmd.Parameters.AddWithValue("@UserType", lblUserType.Text);
            cmd.Parameters.AddWithValue("@CPID", lblCp_Ids.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDeliveryChallanDetails.DataSource = dt;
            gvDeliveryChallanDetails.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void gvDeliveryChallanDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[13].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
           // e.Row.Cells[8].Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[13].Text == "Spares")
        //    {
        //        e.Row.Cells[7].Visible = false;
        //    }
        //    else if (e.Row.Cells[13].Text == "Sales")
        //    {
        //        e.Row.Cells[8].Visible = false;
        //    }
        //}
        BindChileDCSalesGV();
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvDeliveryChallanDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy.SelectedItem.Text == "Company Search") 
        {
            lblSearchValueHidden.Text = ddlCpId.SelectedValue;
        }

         else if (ddlSearchBy.SelectedItem.Text == "DC Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
         else
         {
             txtSearchValueFromDate.Visible = false;
             txtSearchValueToDate.Visible = false;
             lblSearchValueHidden.Text = txtSearchText.Text;
         }
        gvDeliveryChallanDetails.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "DC Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
            ddlCpId.Visible = false;
            ddlCpId .SelectedIndex = 0;

        }
        else if (ddlSearchBy.SelectedItem.Text == "Company Search")
        {
            ddlSymbols.Visible = false;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            ddlCpId.Visible = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchText.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
            ddlCpId.Visible = false;
            ddlCpId .SelectedIndex = 0;

        }
        
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDeliveryChallanDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvDeliveryChallanDetails.DataBind();
    }
    protected void lnkSales_Click(object sender, EventArgs e)
    {
        pnlSample.Visible = false;
        pnlSales.Visible = true;
    }
    protected void lnkCash_Click(object sender, EventArgs e)
    {
        pnlSales.Visible = false;
        pnlSample.Visible = true;

    }
    protected void ddlSearchBy1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy1.SelectedItem.Text == "DC Date")
        {
            ddlSymbol1.Visible = true;
            txtSearchText1.Visible = false;
            txtSearchValueToDate1.Visible = false;
            txtSearchValueFromDate1.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
            ddlCpName.Visible = false;
            ddlCpName.SelectedIndex = 0;

        }
        else if (ddlSearchBy1.SelectedItem.Text == "Company Search")
        {
            ddlSymbol1.Visible = false;
            txtSearchText1.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = false;
            ddlCpName .Visible = true;
        }
        else
        {
            ddlSymbol1.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueToDate1.Visible = false;
            txtSearchValueFromDate1.Visible = false;
            txtSearchText1.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbol1.SelectedIndex = 0;
            ddlCpName.Visible = false;
            ddlCpName.SelectedIndex = 0;
        }
        txtSearchText1.Text = string.Empty;
        txtSearchValueFromDate1.Text = string.Empty;
        txtSearchValueToDate1.Text = string.Empty;
    }
    protected void ddlSymbol1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbol1.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate1.Visible = true;
            txtSearchValueToDate1.Visible = true;
            txtSearchText1.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate1.Visible = true;
            txtSearchValueToDate1.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    protected void btnSearchGo1_Click(object sender, EventArgs e)
    {
        gvSampleDC.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy1.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbol1.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }
        if (ddlSearchBy1.SelectedItem.Text == "Company Search")
        {
            lblSearchValueHidden.Text = ddlCpName .SelectedValue;
        }
        else if (ddlSearchBy1.SelectedItem.Text == "DC Date")
        {
            if (ddlSymbol1.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate1.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate1.Text);
                txtSearchValueToDate1.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate1.Text);

            }
            else
            {
                txtSearchText1.Visible = false;
                txtSearchValueFromDate1.Visible = true;
                txtSearchValueToDate1.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate1.Text);
            }
        }
        else
        {
            txtSearchValueFromDate1.Visible = false;
            txtSearchValueToDate1.Visible = false;
            lblSearchValueHidden.Text = txtSearchText1.Text;
        }
        gvSampleDC.DataBind();
    }
    protected void gvSampleDC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gridView1 = e.Row.FindControl("gvDC") as GridView;
            if (e.Row.RowIndex == 0)
            {
                gridView1.ShowHeader = true;
            }
            else
            {
                gridView1.ShowHeader = false;
            }
        }
        BindChileDCGV();

    }
    private void BindChileDCGV()
    {
        foreach (GridViewRow gvrow in gvSampleDC.Rows)
        {
            GridView gvDC = (GridView)(gvSampleDC.Rows[gvrow.RowIndex].Cells[0].FindControl("gvDC"));
            SqlCommand cmd = new SqlCommand("[USP_DC_SRStatement]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[0].Text != "")
            {
                cmd.Parameters.AddWithValue("@DCID", gvrow.Cells[0].Text);
            }
            //if (gvrow.Cells[6].Text != "")
            //{
            //    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();
           

        }
    }
    private void BindChileDCSalesGV()
    {
        foreach (GridViewRow gvrow in gvDeliveryChallanDetails .Rows)
        {
            GridView gvDC = (GridView)(gvDeliveryChallanDetails.Rows[gvrow.RowIndex].Cells[0].FindControl("gvDCSales"));
            SqlCommand cmd = new SqlCommand("[USP_DC_SRStatementForSales]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (gvrow.Cells[0].Text != "")
            {
                cmd.Parameters.AddWithValue("@DCID", gvrow.Cells[0].Text);
            }
            //if (gvrow.Cells[6].Text != "")
            //{
            //    cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[6].Text);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvDC.DataSource = dt;
            gvDC.DataBind();


        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Export To Excel
        if (gvDeliveryChallanDetails.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=UnbilledDCS.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvDeliveryChallanDetails.AllowPaging = false;
                gvDeliveryChallanDetails.DataBind();

                gvDeliveryChallanDetails.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvDeliveryChallanDetails.HeaderRow.Cells)
                {
                    cell.BackColor = gvDeliveryChallanDetails.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvDeliveryChallanDetails.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvDeliveryChallanDetails.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvDeliveryChallanDetails.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvDeliveryChallanDetails.RenderControl(hw);

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
        #endregion

    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        #region Export To Excel
        if (gvSampleDC.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=UnbilledDCS.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvSampleDC.AllowPaging = false;
                gvSampleDC.DataBind();

                gvSampleDC.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvSampleDC.HeaderRow.Cells)
                {
                    cell.BackColor = gvSampleDC.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvSampleDC.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvSampleDC.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvSampleDC.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvSampleDC.RenderControl(hw);

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
        #endregion

    }
    protected void gvDC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "All Returned";
                e.Row.ForeColor  = System.Drawing.Color.Red;
                
                
            }
            else if ( e.Row.Cells[2].Text != e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "Partially Returned";
                e.Row.ForeColor = System.Drawing.Color.ForestGreen ;

            }
            else 
            {
                e.Row.Cells[4].Text = "No Returns";
            }
        }
    }
    protected void gvDCSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "All Returned";
                e.Row.ForeColor = System.Drawing.Color.Red;


            }
            else if (e.Row.Cells[2].Text != e.Row.Cells[3].Text)
            {
                e.Row.Cells[4].Text = "Partially Returned";
                e.Row.ForeColor = System.Drawing.Color.ForestGreen;

            }
            else
            {
                e.Row.Cells[4].Text = "No Returns";
            }
        }
    }
    protected void btnCloseDC_Click(object sender, EventArgs e)
    {

    }
    protected void gvSampleDC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Button bts = e.CommandSource as Button;
        if (e.CommandName.Equals("Save"))
        {
            int rowindex = int.Parse(e.CommandArgument.ToString().Trim());
            
            Inventory.SalesInvoice obj = new Inventory.SalesInvoice();
            obj.DCId = rowindex.ToString();
            if (obj.DCStatus_Update() == "Data Updated Successfully")
            {
                MessageBox.Show(this, "DC Closed SuccessFully");
                gvSampleDC.DataBind();
            }
            
        }
    }
    protected void gvSalesDC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Button bts = e.CommandSource as Button;
        if (e.CommandName.Equals("SalesSave"))
        {
            int rowindex = int.Parse(e.CommandArgument.ToString().Trim());

            Inventory.SalesInvoice obj = new Inventory.SalesInvoice();
            obj.DCId = rowindex.ToString();
            if (obj.DCStatus_Update() == "Data Updated Successfully")
            {
                MessageBox.Show(this, "DC Closed SuccessFully");
                gvDeliveryChallanDetails.DataBind();
            }

        }
    }
    protected void btnSISave_Click(object sender, EventArgs e)
    {
        string Ids = string.Empty;
        string DCS = string.Empty;
        string DCID = string.Empty;
        string SOID = string.Empty;
        string UnitId = string.Empty;
        string CPID = string.Empty;
        string SalesEmpID = string.Empty;

        foreach (GridViewRow gvrow in gvDeliveryChallanDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chkItemSelect");
            if (ch.Checked == true)
            {
                Ids += string.Format("pha{0}jam,", gvrow.Cells[0].Text);
                DCS += string.Format("s{0}j,", gvrow.Cells[2].Text);
                DCID = gvrow.Cells[0].Text;
                SOID = gvrow.Cells[1].Text;
                UnitId = gvrow.Cells[9].Text;
                CPID = gvrow.Cells[10].Text;
                SalesEmpID = gvrow.Cells[11].Text;
            }
        }
        if (!string.IsNullOrEmpty(Ids))
        {
            Ids = string.Format(Ids.Substring(0, Ids.Length - 1));
            DCS = string.Format(DCS.Substring(0, DCS.Length - 1));

        }
        StringBuilder sb = new StringBuilder(Ids);
        StringBuilder sb1 = new StringBuilder(DCS);

        sb.Replace("pha", "'");
        sb.Replace("jam", "'");
        sb1.Replace("s", "");
        sb1.Replace("j", "");
        string hai = sb.ToString();
        string haidc = sb1.ToString();
        txtRemarks1.Text = haidc;
        Inventory.Delivery objDelivery = new Inventory.Delivery();

        objDelivery.DeliveryDetails_SelectInvoiceforUB(hai, gvDeliveryChallanItems);

        if (gvDeliveryChallanItems.Rows.Count > 0)
        {

            try
            {


                btnSISave.Enabled = false;
                Inventory.SalesInvoice objInventory = new Inventory.SalesInvoice();
                objInventory.SIDate = Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text);
                objInventory.SOId = SOID;
                objInventory.SPOId = "0";
                objInventory.SIPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.SIApprovedBy = SalesEmpID;
                objInventory.SIType = "Tax Invoice";
                objInventory.DCId = DCID;
                objInventory.DespmId = "1";
                objInventory.SIMissChrgs = "0";
                objInventory.SIDiscount = "0";
                objInventory.SIGrossAmt = txtGrossAmount.Text;
                objInventory.SIRemarks = txtRemarks1.Text;
                objInventory.SIVAT = txtVAT.Text;
                objInventory.SICSTax = "0";
                objInventory.CpId = CPID;
                objInventory.SIStatus = "Open";
                objInventory.InvoiceNo = txtInno.Text;
                objInventory.Unitname = UnitId;
                objInventory.SIBranchTransfer = "0";
                int one = 500;
                int two = 500;
                decimal amt1 = (Convert.ToDecimal(txtGrossAmount.Text) + Convert.ToDecimal(one));
                decimal amt2 = (Convert.ToDecimal(txtGrossAmount.Text) - Convert.ToDecimal(two));
                //string Totalrec = "0";
                objInventory.SIStatus = "Open";
                if (objInventory.SalesInvoice_Save() == "Data Saved Successfully")
                {
                    objInventory.SalesInvoiceDC_Update(objInventory.DCId);
                    foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[0].Text;
                        objInventory.SIDetQty = gvrow.Cells[1].Text;
                        objInventory.SIDetRate = gvrow.Cells[12].Text;
                        objInventory.SIDetVat = gvrow.Cells[3].Text;
                        objInventory.SIDetCst = "0";
                        objInventory.SIDetExcise = "0";
                        objInventory.SIDcid = gvrow.Cells[9].Text;
                        objInventory.sicoLORID = gvrow.Cells[7].Text;
                        objInventory.Remarks = "-";
                        objInventory.SalesInvoiceDetails_Save();

                    }
                    foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
                    {

                        objInventory.SIDcid = gvrow.Cells[9].Text;

                        objInventory.SalesInvoiceDC_Update(objInventory.SIDcid);
                    }
                    MessageBox.Show(this, "Data Saved Successfully");

                }
            }
            catch (Exception ex)
            {
                //  Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvSalesInvoiceDetails.DataBind();
                btnSISave.Enabled = true;
                gvDeliveryChallanDetails.DataBind();
                gvDeliveryChallanDetails.DataBind();
                //tblSIDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this,"Please select dc ");

        }

    }

    
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "" || e.Row.Cells[4].Text == null) { e.Row.Cells[4].Text = "0"; }
            e.Row.Cells[12].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[2].Text) / Convert.ToDecimal(e.Row.Cells[6].Text));

            e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[1].Text) * Convert.ToDecimal(e.Row.Cells[12].Text));
            e.Row.Cells[5].Text = (Convert.ToDecimal(e.Row.Cells[4].Text) * Convert.ToDecimal(e.Row.Cells[3].Text) / 100).ToString();

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (lblTtlAmt.Text == "" || lblTtlAmt.Text == null) { lblTtlAmt.Text = "0"; }
            if (lblTtlGSTAmt.Text == "" || lblTtlGSTAmt.Text == null) { lblTtlGSTAmt.Text = "0"; }
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = GrossAmountCalc().ToString();
            txtGrossTotalAmtHidden.Value = txtTotalAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) + Convert.ToDecimal(lblTtlAmt.Text)).ToString();
            txtVAT.Text = GSTAmountCalc().ToString();
            txtVAT.Text = (Convert.ToDecimal(txtVAT.Text) + Convert.ToDecimal(lblTtlGSTAmt.Text)).ToString();
            txtGrossAmount.Text = (Convert.ToDecimal(txtGrossTotalAmtHidden.Value) + Convert.ToDecimal(txtVAT.Text)).ToString();
        }
    }

    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[4].Text);
        }
        return _totalAmt;
    }
    private double GSTAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvDeliveryChallanItems.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[5].Text);
        }
        return _totalAmt;
    }
}

 
