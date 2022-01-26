using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using vllib;

public partial class Modules_Warehouse_OutwardStock : basePage
{
    Warehouse.Quotations quot; DataTable dt; DataSet ds; Warehouse.Items whItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "95");
        //btnGetDetails.Enabled = up.GetDetails;

    }
    protected void btnGetDetails_Click(object sender, EventArgs e)
    {
        quot = new Warehouse.Quotations();
        int quotId;
        if (int.TryParse(txtQuotation.Text,out quotId))
        {
            ds = new DataSet(); ds = quot.GetQuotations(quotId);
            gvQuotDeatils.DataSource = ds.Tables[0];
            gvQuotDeatils.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                // bind address of the customer
                txtStoreAddress.Text = ds.Tables[1].Rows[0][0].ToString();
            }
            //btnGetStock.Visible = true;
            divQuotDetails.Visible = true;
            divWh_Items.Visible = false;
        }
    }
    public DataTable LoadQuotItems()
    {
        DataTable QuotTable = new DataTable();

        QuotTable.Columns.Add("ItemName");
        QuotTable.Columns.Add("Color");
        QuotTable.Columns.Add("Brand");
        QuotTable.Columns.Add("DispatchQuantity");
        foreach (GridViewRow row in gvQuotDeatils.Rows)
        {
            CheckBox chk = (CheckBox)row.Cells[0].FindControl("chk");
            if (chk.Checked == true)
            {
                DataRow r = QuotTable.NewRow();
                r["ItemName"] = row.Cells[1].Text;
                r["Color"] = row.Cells[2].Text;
                r["Brand"] = row.Cells[3].Text;
                r["DispatchQuantity"] = "0";
                QuotTable.Rows.Add(r);
            }
        }
        return QuotTable;
    }
    public DataTable LoadDispatchItems()
    {
        DataTable DispatchTable = new DataTable();

        DispatchTable.Columns.Add("ItemID");
        DispatchTable.Columns.Add("Quantity");
        foreach (GridViewRow row in gvWH_Items.Rows)
        {
            CheckBox chk = (CheckBox)row.Cells[0].FindControl("chk");
            if (chk.Checked == true)
            {
                DataRow r = DispatchTable.NewRow();
                r["ItemID"] = row.Cells[2].Text;
                r["Quantity"] = ((TextBox)row.Cells[1].FindControl("txtDispatchQuantity")).Text;
                DispatchTable.Rows.Add(r);
            }
        }
        return DispatchTable;
    }
    protected void btnGetStock_Click(object sender, EventArgs e)
    {
        DataTable dtSelectedItems = new DataTable();
        dtSelectedItems=LoadQuotItems();
        if (dtSelectedItems.Rows.Count > 0)
        {
            quot = new Warehouse.Quotations();

            dt = new DataTable(); dt = quot.GetWarehouseItemsByQuotations(dtSelectedItems);
            gvWH_Items.DataSource = dt;
            gvWH_Items.DataBind();
            btnDispatchItems.Visible = true;
            divWh_Items.Visible = true;
            
        }
        else
        {
            JavaScriptAlert("please select items of Quotaion to view the stock");
        }
    }
    public StringBuilder PrepareMovingForm()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<table><tr><td></td><td></td><td>Moving Form</td><td></td><td></td></tr></table><br/>");
        sb.Append("<table>");
        sb.Append("<tr><td>Address</td><td></td><td></td><td></td></tr>");
        sb.Append("<tr><td rowspan=\"3\">" + txtStoreAddress.Text + "</td><td></td><td></td><td></td></tr>");
        sb.Append("<tr><td></td><td></td><td>Indent No : " + txtQuotation.Text + "</td></tr>");
        sb.Append("<tr><td></td><td></td><td>Date : " + DateTime.Now + "</td></tr>");
        
        sb.Append("</table>");
        sb.AppendLine("<br/>");
        

        //Indent Items
        #region "Indent Items"
        sb.AppendLine("<p>Indent Item Details</p>");
        sb.AppendLine("<br/>");
        sb.Append("<table border=\"1\">");
        //Header row
        sb.Append("<tr>");
        sb.Append("<th>S.No</th>");
        sb.Append("<th>" + gvQuotDeatils.HeaderRow.Cells[1].Text + "</th>");
        sb.Append("<th>" + gvQuotDeatils.HeaderRow.Cells[2].Text + "</th>");
        sb.Append("<th>" + gvQuotDeatils.HeaderRow.Cells[3].Text + "</th>");
        sb.Append("<th>" + gvQuotDeatils.HeaderRow.Cells[4].Text + "</th>");
        sb.Append("</tr>");
        //Content rows
        int Sno = 1;
        foreach (GridViewRow row in gvQuotDeatils.Rows)
        {
            sb.Append("<tr>");
            sb.Append("<td>" + Sno.ToString() + "</td>");
            sb.Append("<td>" + row.Cells[1].Text + "</td>");
            sb.Append("<td>" + row.Cells[2].Text + "</td>");
            sb.Append("<td>" + row.Cells[3].Text + "</td>");
            sb.Append("<td>" + row.Cells[4].Text + "</td>");
            sb.Append("</tr>");
            Sno++;
        }
        sb.Append("</table>");
        sb.AppendLine("<br/>");
        #endregion
        //Dispatching Items
        #region "Dispatching Items"
        sb.AppendLine("<p>Moved Details</p>");
        sb.AppendLine("<br/>");
        sb.Append("<table border=\"1\">");
        //Header row
        sb.Append("<tr>");
        sb.Append("<th>S.No</th>");
        sb.Append("<th>" + gvWH_Items.HeaderRow.Cells[3].Text + "</th>");
        sb.Append("<th>" + gvWH_Items.HeaderRow.Cells[4].Text + "</th>");
        sb.Append("<th>" + gvWH_Items.HeaderRow.Cells[5].Text + "</th>");
        sb.Append("<th>" + gvWH_Items.HeaderRow.Cells[6].Text + "</th>");
        sb.Append("<th>" + gvWH_Items.HeaderRow.Cells[1].Text + "</th>");
        sb.Append("</tr>");
        //Content rows
        Sno = 1;
        foreach (GridViewRow row in gvWH_Items.Rows)
        {
            sb.Append("<tr>");
            sb.Append("<td>" + Sno.ToString() + "</td>");
            sb.Append("<td>" + row.Cells[3].Text + "</td>");
            sb.Append("<td>" + row.Cells[4].Text + "</td>");
            sb.Append("<td>" + row.Cells[5].Text + "</td>");
            sb.Append("<td>" + row.Cells[6].Text + "</td>");
            sb.Append("<td>" + row.Cells[1].Text + "</td>");
            sb.Append("</tr>");
            Sno++;
        }
        sb.Append("</table>");
        #endregion

        return sb;
    }
    public void JavaScriptAlert(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('"+msg+"')", true);
    }
    protected void btnDispatchItems_Click(object sender, EventArgs e)
    {
        if (txtQuotation.Text.Trim() == "" && txtComments.Text.Trim() == "")
        {
            JavaScriptAlert("please enter values for Quotation NO and Comments ");
        }
        else
        {
            //STEP-1: update stock in temp_items table
            //STEP-2: add dispatch details in WH_Dispatched_Items columns Quotation id, comments, IsDispatched
            DataTable dtDispatchItems = new DataTable();
            dtDispatchItems = LoadDispatchItems();
            if (dtDispatchItems.Rows.Count > 0)
            {
                whItem = new Warehouse.Items();
                int n = whItem.DispatchItems(dtDispatchItems, Convert.ToInt32(txtQuotation.Text), txtComments.Text);
                if (n > 0)
                {
                    JavaScriptAlert("Dispatching items are updated successfully");
                }
                btnGetDetails_Click(null,null);
                Export export = new Export();
                //pnlContet.Controls.Add(tblAddress);
                //pnlContet.Controls.Add(gvWH_Items);
                //pnlContet.Controls.Add(gvWH_Items);
                //ExportGrid(pnlContet, "Panel", Export.Extension.PDF);
                //pnlContet.Controls.Clear();
                export.ExportGrid(PrepareMovingForm(), "Moving From", Export.Extension.PDF);
                
            }
            else
            {
                JavaScriptAlert("please select items in stock table to dispatch");
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    //=================================================================
    public void ExportGrid(System.Text.StringBuilder sb, string filename, Export.Extension ext)
    {
        Response.ContentType = "application/pdf";
    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    StringWriter sw = new StringWriter(sb);
    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //gvWH_Items.RenderControl(hw);
    //gvQuotDeatils.RenderControl(hw);
    StringReader sr = new StringReader(sw.ToString());
    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    PdfWriter writer= PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    
    pdfDoc.Open();
    htmlparser.Parse(sr);
    //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
    pdfDoc.Close();
    Response.Write(pdfDoc);
    Response.End();
        
    }
}
 
