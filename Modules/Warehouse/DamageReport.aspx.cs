using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using YantraBLL.Modules;
using Yantra.MessageBox;
using Yantra.Classes;
using System.Drawing;
using System.Collections;
public partial class Modules_Warehouse_DamageReport : basePage
{
    static string msgid;
    Warehouse.Items whItem; 
    DataTable dt;
    static DataTable dtDamage = new DataTable();
    static DataTable dtDamageDaily = new DataTable();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        PopulateCheckBoxArray();
        if (!IsPostBack)
        {
            setControlsVisibility();

            ClearRow();
            PrepareGrid();
            AddNewRow();
            BindDamageReportGrid();
            pnlDamageReport.Visible = true;

            pnlDailyReport.Visible = false;
            ClearRowDaily();
            PrepareGridDaily();
            AddNewRowDaily();

            BindDamageReportGridDaily();
        }
        SaveGridValues();
        SaveGridValuesDaily();
        BindDamageReportGrid();
    }

    private void PopulateCheckBoxArray()
    {
        if (gvDamageReport.Rows.Count > 0)
        {
            ArrayList CheckBoxArray;
            if (ViewState["CheckBoxArray"] != null)
            {
                CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
            }
            else
            {
                CheckBoxArray = new ArrayList();
            }

            int CheckBoxIndex;
            bool CheckAllWasChecked = false;
            CheckBox chkAll = (CheckBox)gvDamageReport.HeaderRow.Cells[0].FindControl("chkAll");
            string checkAllIndex = "chkAll-" + gvDamageReport.PageIndex;
            if (chkAll.Checked)
            {
                if (CheckBoxArray.IndexOf(checkAllIndex) == -1)
                {
                    CheckBoxArray.Add(checkAllIndex);
                }
            }
            else
            {
                if (CheckBoxArray.IndexOf(checkAllIndex) != -1)
                {
                    CheckBoxArray.Remove(checkAllIndex);
                    CheckAllWasChecked = true;
                }
            }
            for (int i = 0; i < gvDamageReport.Rows.Count; i++)
            {
                if (gvDamageReport.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)gvDamageReport.Rows[i].Cells[0].FindControl("CheckBox1");
                    CheckBoxIndex = gvDamageReport.PageSize * gvDamageReport.PageIndex + (i + 1);
                    if (chk.Checked)
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) == -1 && !CheckAllWasChecked)
                        {
                            CheckBoxArray.Add(CheckBoxIndex);
                        }
                    }
                    else
                    {
                        if (CheckBoxArray.IndexOf(CheckBoxIndex) != -1 || CheckAllWasChecked)
                        {
                            CheckBoxArray.Remove(CheckBoxIndex);
                        }
                    }
                }
            }

            ViewState["CheckBoxArray"] = CheckBoxArray;
        }
    }

    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "96");
        //btnSubmitReport.Enabled = up.Submit;
        btnAddNewRow.Enabled = up.add;
        btnDeleteRow.Enabled = up.Delete;

    }

    private void SaveGridValues()
    {
        for (int i=0;gvInvoiceItems.Rows.Count>i;i++)
        {
            for (int j = 1; j < gvInvoiceItems.Rows[i].Cells.Count; j++)
			{
                dtDamage.Rows[i][j] = ((TextBox)gvInvoiceItems.Rows[i].Cells[j].Controls[1]).Text;
                //dtDamage.Rows[i][j] = ((FileUpload)gvInvoiceItems.Rows[i].Cells[j].Controls[1]).FileName;
			}
        }
    }

    private void SaveGridValuesDaily()
    {
        for (int i = 0; gvDailyReport.Rows.Count > i; i++)
        {
            for (int j = 1; j < gvDailyReport.Rows[i].Cells.Count; j++)
            {
                dtDamageDaily.Rows[i][j] = ((TextBox)gvDailyReport.Rows[i].Cells[j].Controls[1]).Text;
            }
        }
    }
    public void ClearRow()
    {
        dtDamage.Columns.Clear();
        dtDamage.Rows.Clear();
        gvInvoiceItems.DataSource = null;
        gvInvoiceItems.DataBind();
    }
    public void ClearRowDaily()
    {
        dtDamageDaily.Columns.Clear();
        dtDamageDaily.Rows.Clear();
        gvDailyReport.DataSource = null;
        gvDailyReport.DataBind();
    }
    public void AddNewRow()
    {
        DataRow row = dtDamage.NewRow();
        dtDamage.Rows.Add(row);
        gvInvoiceItems.DataSource = dtDamage;
        gvInvoiceItems.DataBind();
    }

    public void AddNewRowDaily()
    {
        DataRow row = dtDamageDaily.NewRow();
        dtDamageDaily.Rows.Add(row);
        gvDailyReport.DataSource = dtDamageDaily;
        gvDailyReport.DataBind();
    }
    public void JavaScriptAlert(string msg)
    {
        ClientScript.RegisterStartupScript(Page.GetType(),"JCall","alert('"+msg+"')",true);
    }
    protected void btnSubmitReport_Click(object sender, EventArgs e)
    {
        #region Save Report

        foreach (GridViewRow gvrow in gvInvoiceItems.Rows)
        {
            CheckBox ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                TextBox InvoiceNo = (TextBox)gvrow.FindControl("txtInvoiceNo");
                TextBox ModelNo = (TextBox)gvrow.FindControl("txtModelNo");
                TextBox Brand = (TextBox)gvrow.FindControl("txtBrand");
                TextBox Category = (TextBox)gvrow.FindControl("txtCategory");
                TextBox SubCat = (TextBox)gvrow.FindControl("txtSubCategory");
                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                TextBox color = (TextBox)gvrow.FindControl("txtColor");
                TextBox damage = (TextBox)gvrow.FindControl("txtDamage");
                TextBox excess = (TextBox)gvrow.FindControl("txtExcess");
                TextBox shortage = (TextBox)gvrow.FindControl("txtShortage");
                TextBox Remarks = (TextBox)gvrow.FindControl("txtRemarks");

                Masters.ItemPurchase obj = new Masters.ItemPurchase();

                    obj.ItemID = "I" + ModelNo.Text + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                    obj.InvoiceNo = InvoiceNo.Text;
                    obj.Barcode = ModelNo.Text;
                    obj.ModelNo = ModelNo.Text;
                    obj.Brand = Brand.Text;
                    obj.Category = Category.Text;
                    obj.SubCat = SubCat.Text;
                    obj.Qty = qty.Text;
                    obj.color = color.Text;
                    obj.Damage = damage.Text;
                    obj.Excess = excess.Text;
                    obj.Shortage = shortage.Text;
                    obj.Remarks = Remarks.Text;
                    if(rbnDamage.Checked==true)
                    {
                        obj.ItemType = "Damaged";
                    }
                    else
                    {
                        obj.ItemType = "Defective";
                    }
                    
                    if(obj.Damage_Report_Save() == "Data Saved Successfully")
                    {
                        MessageBox.Show(this, "Data Saved Successfully");
                    }
                    else
                    {
                        MessageBox.Show(this, "Some Data Missing");

                    }

            }
        }

        ClearRow();
        //PrepareGrid();
       // AddNewRow();
        //gvInvoiceItems.DataSource = null;
        //gvInvoiceItems.DataBind();       
        BindDamageReportGrid();
        PrepareGrid();
#endregion
    }
    protected void btnInvoiceItems_Click(object sender, EventArgs e)
    {
        int invoiceNo=0;
        if (int.TryParse(txtInvoice.Text.Trim(),out invoiceNo))
        {
            dt = new DataTable();
            whItem = new Warehouse.Items();
            dt=whItem.GetInvoiceItems(invoiceNo);
            gvInvoiceItems.DataSource = dt;
            gvInvoiceItems.DataBind();
        }
        
    }
    
    public void PrepareGrid()
    {

        dtDamage.Columns.Add("", typeof(string));
        dtDamage.Columns.Add("Invoice No", typeof(string));
        dtDamage.Columns.Add("Model No", typeof(string));
        dtDamage.Columns.Add("Brand", typeof(string));
        dtDamage.Columns.Add("Category", typeof(string));
        dtDamage.Columns.Add("Sub Category", typeof(string));
        dtDamage.Columns.Add("Quantity", typeof(string));
        dtDamage.Columns.Add("Color", typeof(string));
        dtDamage.Columns.Add("Damage", typeof(string));
        dtDamage.Columns.Add("Excess", typeof(string));
        dtDamage.Columns.Add("Shortage", typeof(string));
        dtDamage.Columns.Add("Remarks", typeof(string));
        dtDamage.Columns.Add("DamageReportId", typeof(string));
    }

    public void PrepareGridDaily()
    {

        dtDamageDaily.Columns.Add("", typeof(string));
        dtDamageDaily.Columns.Add("Invoice No", typeof(string));
        dtDamageDaily.Columns.Add("Model No", typeof(string));
        dtDamageDaily.Columns.Add("Brand", typeof(string));
        dtDamageDaily.Columns.Add("Description", typeof(string));
        dtDamageDaily.Columns.Add("Supplier", typeof(string));
        dtDamageDaily.Columns.Add("Quantity", typeof(string));
        dtDamageDaily.Columns.Add("Color", typeof(string));
        //dtDamageDaily.Columns.Add("Damage", typeof(string));
        dtDamageDaily.Columns.Add("Excess", typeof(string));
        dtDamageDaily.Columns.Add("Shortage", typeof(string));
        dtDamageDaily.Columns.Add("Remarks", typeof(string));
        dtDamageDaily.Columns.Add("DamageReportId", typeof(string));
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDamageReport.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        BindDamageReportGrid();
    }

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gvInvoiceItems.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvInvoiceItems.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                dtDamage.Rows[i].Delete();
            }
        }
        gvInvoiceItems.DataSource = dtDamage; gvInvoiceItems.DataBind();
    }
    protected void Chk_CheckedChanged(object sender, EventArgs e)
    {
        //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");

    }
    private void BindDamageReportGrid()
    {
        SqlCommand cmd1 = new SqlCommand("USP_DamageReportSearch", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        if(txtInvNo.Text != "")
        {
        cmd1.Parameters.AddWithValue("@InvoiceNo", txtInvNo.Text);

        }
        if (txtModelNo.Text != "")
        {
            cmd1.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);

        }
        if(txtFromDate.Text !="")
        {
            cmd1.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));

        }
        if(txtToDate.Text !="")
        {
            cmd1.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));

        }
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        gvDamageReport.DataSource = dt1;
        gvDamageReport.DataBind();
    }

    private void BindDamageReportGridDaily()
    {
        SqlCommand cmd1 = new SqlCommand("USP_DamageReportSearch_Daily", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        if (txtInvNoDaily.Text != "")
        {
            cmd1.Parameters.AddWithValue("@InvoiceNo", txtInvNoDaily.Text);

        }
        if (txtModelNoDaily.Text != "")
        {
            cmd1.Parameters.AddWithValue("@ModelNo", txtModelNoDaily.Text);

        }
        if (txtFromDateDaily.Text != "")
        {
            cmd1.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDateDaily.Text));

        }
        if (txtToDateDaily.Text != "")
        {
            cmd1.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDateDaily.Text));

        }
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        gvDailyReportSearch.DataSource = dt1;
        gvDailyReportSearch.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDamageReportGrid();
    }

    protected void lnkDamageReport_Click(object sender, EventArgs e)
    {
        pnlDailyReport.Visible = false;
        pnlDamageReport.Visible = true;
    }
    protected void lnkDailyReport_Click(object sender, EventArgs e)
    {
        pnlDailyReport.Visible = true;
        pnlDamageReport.Visible = false;

    }
    protected void btnDeleteNewRowDaily_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gvDailyReport.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gvDailyReport.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                dtDamageDaily.Rows[i].Delete();
            }
        }
        gvDailyReport.DataSource = dtDamageDaily; 
        gvDailyReport.DataBind();
    }
    protected void btnAddNewRowDaily_Click(object sender, EventArgs e)
    {
        AddNewRowDaily();
    }
    protected void btnSubmitReportDaily_Click(object sender, EventArgs e)
    {
        #region Save Report

        foreach (GridViewRow gvrow in gvDailyReport.Rows)
        {
            CheckBox ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                TextBox InvoiceNo = (TextBox)gvrow.FindControl("txtInvoiceNo");
                TextBox ModelNo = (TextBox)gvrow.FindControl("txtModelNo");
                TextBox Brand = (TextBox)gvrow.FindControl("txtBrand");
                TextBox Category = (TextBox)gvrow.FindControl("txtItemDescription");
                TextBox SubCat = (TextBox)gvrow.FindControl("txtSupplier");
                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                TextBox color = (TextBox)gvrow.FindControl("txtColor");
                
                //TextBox damage = (TextBox)gvrow.FindControl("txtDamage");
                
                TextBox excess = (TextBox)gvrow.FindControl("txtExcess");
                TextBox shortage = (TextBox)gvrow.FindControl("txtShortage");
                TextBox Remarks = (TextBox)gvrow.FindControl("txtRemarks");

                Masters.ItemPurchase obj = new Masters.ItemPurchase();

                obj.ItemID = "I" + ModelNo.Text + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                obj.InvoiceNo = InvoiceNo.Text;
                obj.Barcode = ModelNo.Text;
                obj.ModelNo = ModelNo.Text;
                obj.Brand = Brand.Text;
                obj.Category = Category.Text;
                obj.SubCat = SubCat.Text;
                obj.Qty = qty.Text;
                obj.color = color.Text;

                obj.Damage = "Daily";
                
                obj.Excess = excess.Text;
                obj.Shortage = shortage.Text;
                obj.Remarks = Remarks.Text;
            
                obj.ItemType = "Daily";

                if (obj.Damage_Report_Save() == "Data Saved Successfully")
                {
                    MessageBox.Show(this, "Data Saved Successfully");
                }
                else
                {
                    MessageBox.Show(this, "Some Data Missing");

                }

            }
        }

        ClearRowDaily();
        BindDamageReportGridDaily();
        PrepareGridDaily();
        #endregion
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the    control is rendered */
    }
    protected void btnSearchDaily_Click(object sender, EventArgs e)
    {
        BindDamageReportGridDaily();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Export to Excel
        if (gvDailyReportSearch.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=WarehouseDailyInwardReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvDailyReportSearch.AllowPaging = false;
                BindDamageReportGridDaily();
                gvDailyReportSearch.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvDailyReportSearch.HeaderRow.Cells)
                {
                    cell.BackColor = gvDailyReportSearch.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvDailyReportSearch.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvDailyReportSearch.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvDailyReportSearch.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvDailyReportSearch.RenderControl(hw);

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
    protected void gvDamageReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[15].Visible = false;
            TextBox comments = (TextBox)e.Row.FindControl("txtComment");
            if (comments.Text != e.Row.Cells[5].Text)
            {
                e.Row.BackColor = System.Drawing.Color.Coral;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            
        }
    }

    protected void btnPostComment_Click(object sender, EventArgs e)
    {
        PostAdminComment();
        BindDamageReportGrid();
    }
    private void PostAdminComment()
    {
        foreach (GridViewRow gvr in gvDamageReport.Rows)
        {
            if (((CheckBox)gvr.FindControl("CheckBox1")).Checked)
            {
                try
                {
                    Label DailyReportId = (Label)gvr.FindControl("lblId");
                    TextBox comment = (TextBox)gvr.FindControl("txtComment");
                    SqlCommand cmd = new SqlCommand("USP_UpdateDamageDailyReportComment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DailyReportId", DailyReportId.Text);
                    cmd.Parameters.AddWithValue("@Comment", comment.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { }
            }
        }
    }
    protected void gvDamageReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDamageReport.PageIndex = e.NewPageIndex;
        BindDamageReportGrid();
    }
    protected void gvDailyReportSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            TextBox comments = (TextBox)e.Row.FindControl("txtComment");
            if (comments.Text != null && comments.Text != "")
            {
                //e.Row.BackColor = System.Drawing.Color.Coral;
                //e.Row.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
    protected void gvDailyReportSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDailyReportSearch.PageIndex = e.NewPageIndex;
        BindDamageReportGrid();
    }
    protected void btnFollowUp_Click(object sender, EventArgs e)
    {

    }

    //protected void gvInvoiceItems_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    Button bts = e.CommandSource as Button;
    //    if (e.CommandName.Equals("attach"))
    //    {
    //        int rowindex = int.Parse(e.CommandArgument.ToString().Trim());
    //        FileUpload FileUpload1 = bts.Parent.Parent.FindControl("fileupload1") as FileUpload;
    //        DataList DLAttachments1 = bts.Parent.Parent.FindControl("DLAttachments1") as DataList;
    //        if (FileUpload1.HasFile)
    //        {
    //            string msgattid = dbc.get_rnum("msgattid", "msgs_Attachments_tbl");

    //            string filename = FileUpload1.FileName;
    //            string savefilename = msgattid + "_" + filename;

    //            FileUpload1.SaveAs(Server.MapPath("~/Content/messagesAttachments/") + savefilename);

    //            if (msgs.Attachments.add(filename, savefilename, msgid, msgattid))
    //            {
    //                DataTable dt = msgs.Attachments.getAttachments(msgid);

    //                DLAttachments1.DataSource = dt;
    //                DLAttachments1.DataBind();
    //            }
    //        }
    //    }
    //}
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (gvDamageReport.Rows.Count > 0)
        {
            if (ViewState["CheckBoxArray"] != null)
            {
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition",
                 "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                gvDamageReport.AllowPaging = false;
                BindDamageReportGrid();
                gvDamageReport.HeaderRow.Cells[0].Visible = false;
                if (ViewState["CheckBoxArray"] != null)
                {
                    ArrayList CheckBoxArray = (ArrayList)ViewState["CheckBoxArray"];
                    string checkAllIndex = "chkAll-" + gvDamageReport.PageIndex;
                    int rowIdx = 0;
                    for (int i = 0; i < gvDamageReport.Rows.Count; i++)
                    {
                        GridViewRow row = gvDamageReport.Rows[i];
                        row.Visible = false;

                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            if (CheckBoxArray.IndexOf(i + 1) != -1)
                            {
                                row.Visible = true;
                                row.BackColor = System.Drawing.Color.White;
                                row.Cells[0].Visible = false;
                                row.Attributes.Add("class", "textmode");
                                if (rowIdx % 2 != 0)
                                {

                                }
                                rowIdx++;
                                row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                                row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                                //string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                                //System.Web.UI.WebControls.Image img1 = row.Cells[11].Controls[1] as System.Web.UI.WebControls.Image;
                                //row.Cells[11].Controls.Add(img1);
                                //img1.Height = Unit.Pixel(150);
                                //img1.Width = Unit.Pixel(150);
                            }
                        }
                    }
                }
                gvDamageReport.RenderControl(hw);
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.End();

            }

        }
        else
        {
            MessageBox.Show(this, "There is No Data To Exprot To Excel");
        }
    }
}











 
