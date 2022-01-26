using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using YantraBLL.Modules;
using Yantra.MessageBox;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

public partial class Modules_SCM_IndentRecords : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "10");
        //btnSupplierQuation.Enabled = up.SupplierQuation;
        //btnPurchaseOrder.Enabled = up.PurchaseOrder;
        btnExprot.Enabled = up.Email;
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "Required By Date" || ddlSearchBy.SelectedItem.Text == "Indent Date")
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
        gvIndentDetails.DataBind();
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Required By Date" || ddlSearchBy.SelectedItem.Text == "Indent Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
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



    protected void btnPurchaseOrder_Click(object sender, EventArgs e)
    {
        DeleteRecords();
        int i = 0;
        foreach (GridViewRow gvrow in gvIndentDetails.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                string dId = gvrow.Cells[3].Text;
                string indId = gvrow.Cells[2].Text;
                SqlCommand cmd = new SqlCommand("Insert into IND_DET_ITEMS values (@detId,@IndId)", con);
                cmd.Parameters.AddWithValue("@detId", dId);
                cmd.Parameters.AddWithValue("@IndId", indId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                //string dId = gvrow.Cells[3].Text;
                //SCM.IndentApproval obj = new SCM.IndentApproval();
                //obj.IndentRecordsStatus_Update("po", dId);

            }
        }

        Response.Redirect("PurchaseOrder1.aspx?type=" + "Indent");

    }
    protected void btnSupplierQuation_Click(object sender, EventArgs e)
    {
        DeleteRecords();
        int i = 0;
        foreach (GridViewRow gvrow in gvIndentDetails.Rows)
        {

            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                string dId = gvrow.Cells[3].Text;
                string indId = gvrow.Cells[2].Text;
                SqlCommand cmd = new SqlCommand("Insert into IND_DET_ITEMS values (@detId,@IndId)", con);
                cmd.Parameters.AddWithValue("@detId", dId);
                cmd.Parameters.AddWithValue("@IndId", indId);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        if (i > 0)
        {
            Response.Redirect("SupplierQuation.aspx?IndentId=" + "New");
        }
    }

    private void DeleteRecords()
    {
        SqlCommand cmd = new SqlCommand("Delete From IND_DET_ITEMS", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected void gvIndentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            //e.Row.Cells[13].Visible = false;
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvIndentDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvIndentDetails.DataBind();

    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvIndentDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }

    protected void chkH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvIndentDetails.HeaderRow.FindControl("chkH");
        foreach (GridViewRow row in gvIndentDetails.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Chk");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;

            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void btnExprot_Click(object sender, EventArgs e)
    {
        if (gvIndentDetails.Rows.Count > 0)
        {
            #region Export selected Data to Excel
            bool isselected = false;

            foreach (GridViewRow gvrow in gvIndentDetails.Rows)
            {
                CheckBox chck = gvrow.FindControl("Chk") as CheckBox;
                if (chck != null && chck.Checked)
                {
                    isselected = true;
                    break;
                }
            }
            if (isselected)
            {
                GridView grdxport = gvIndentDetails;
                grdxport.AllowPaging = false;

                grdxport.Columns[0].Visible = false;
                foreach (GridViewRow gvrow in gvIndentDetails.Rows)
                {
                    grdxport.Rows[gvrow.RowIndex].Visible = false;
                    CheckBox chck = gvrow.FindControl("Chk") as CheckBox;
                    if (chck != null && chck.Checked)
                    {
                        grdxport.Rows[gvrow.RowIndex].Visible = true;
                    }
                }

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";

                Response.ContentType = "application/vnd.ms-excel";


                StringWriter swr = new StringWriter();

                HtmlTextWriter htmlwtr = new HtmlTextWriter(swr);
                grdxport.RenderControl(htmlwtr);
                Response.Output.Write(swr.ToString());
                //    Response.Flush();
                Response.End();

            }
            #endregion

            #region Old Exporting Code
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=IndentRecordsReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvIndentDetails.AllowPaging = false;
                //BindGrid_All();
                // BindGridview();
                gvIndentDetails.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvIndentDetails.HeaderRow.Cells)
                {
                    cell.BackColor = gvIndentDetails.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvIndentDetails.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvIndentDetails.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvIndentDetails.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvIndentDetails.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            #endregion

        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //Required to verify that the control is rendered properly on page
    }
}

