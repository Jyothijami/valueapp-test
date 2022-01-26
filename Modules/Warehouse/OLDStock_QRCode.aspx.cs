using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using YantraBLL.Modules;
using System.IO;
using System.Drawing;
using Yantra.MessageBox;
using Yantra.Classes;
using QRCoder;
using System.Text;

public partial class Modules_Warehouse_OLDStock_QRCode : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            SCM.CheckingFormat.CheckingFormat_Select(ddlMrn);
            Masters.ItemMaster.ItemMaster_Select(ddlModelNo);
            Masters.ColorMaster.Color_Select(ddlColor);
            //BindStockReportGrid();
            //setControlsVisibility();
            ItemCategoryFill();
        }
    }
    private void ItemCategoryFill()
    {
        Masters.ItemCategory.ItemCategory_Select(ddlCategory);
    }
    private void BindStockReportGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_StockReport_Serach", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();

    }


    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelNo, ddlBrand.SelectedItem.Value);
        //ItemCategoryFill();
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlColor.Items.Clear();
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //btnExport.Visible = true;
        //btnExport2.Visible = false;
        btnQR.Visible = true;
        btnPrint.Visible = true;
        gvStockReport.Visible = true;
        gvModelNoSearch.Visible = false;
        BindSearchGrid();
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("[USP_StockReportNew_Serach_QRCODE1]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedItem.Value);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value);
        }
        if (ddlModelNo.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@ModelNo", ddlModelNo.SelectedItem.Value);
        }

        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        }

        if (ddlCategory.SelectedIndex != 0 && ddlCategory.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@CatId", ddlCategory.SelectedItem.Value);
        }

        if (ddlSubCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@SubCatId", ddlSubCat.SelectedItem.Value);
        }
        if (ddlMrn.SelectedIndex != 0 && ddlMrn.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@CHK_ID", ddlMrn.SelectedItem.Value);
        }
        if (ddlColor.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Color", ddlColor.SelectedItem.Value);
        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ItemCode", txtModelNo.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];

            if (dt.Rows[i]["ClosingStock"].ToString() == "0")
                dr.Delete();

            //if (dr["Total Available Stock"] == "0")
            //    dr.Delete();
        }

        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();
    }
    decimal ClosingStock = 0, printQty = 0;
    protected void gvStockReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {

            //e.Row.Cells[1].Visible = false;

            // Total Inward -Green
            e.Row.Cells[5].ForeColor = Color.DarkRed;
            
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            //e.Row.Cells[19].Visible = false;


          




            if (e.Row.Cells[18].Text == "1")
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[19].Text == string.Empty || e.Row .Cells [19].Text == "&nbsp;")
                {
                    e.Row.Cells[19].Text = "0";
                }
                ClosingStock += Convert.ToDecimal(e.Row.Cells[5].Text);
                printQty += Convert.ToDecimal(e.Row.Cells[19].Text);
                //InwardQty = InwardQty + int.Parse(e.Row.Cells[8].Text);
                //e.Row.Cells[5].Text = lblCS.Text = ClosingStock.ToString();
                lblCS.Text = ClosingStock.ToString ();
                lblPrintQty.Text  = printQty.ToString();

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = lblCS.Text = ClosingStock.ToString();
            }
        }
    }

    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStockReport.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvStockReport.DataBind();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        //btnExport.Visible = false;
        //btnExport2.Visible = true;
        btnQR.Visible = true;
        btnPrint.Visible = true;
        gvStockReport.Visible = true ;

        gvModelNoSearch.Visible = false ;
        BindSearchGrid();
    }

    private void BindGridOnModelSearch()
    {
        if (txtModelNo.Text != "")
        {
            SqlCommand cmd = new SqlCommand("[USP_StockReportNew_ModelNoQRCode]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (dt.Rows[i]["ClosingStock"].ToString() == "0")
                    dr.Delete();

                //if (dr["Total Available Stock"] == "0")
                //    dr.Delete();
            }

            gvModelNoSearch.DataSource = dt;
            gvModelNoSearch.DataBind();
        }
        else
        {
            MessageBox.Show(this, "Please Enter ModelNo");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Export To Excel
        if (gvStockReport.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=StockReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvStockReport.AllowPaging = false;
                this.BindSearchGrid();

                gvStockReport.HeaderRow.BackColor = Color.Yellow;
                foreach (TableCell cell in gvStockReport.HeaderRow.Cells)
                {
                    cell.BackColor = gvStockReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvStockReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvStockReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvStockReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvStockReport.RenderControl(hw);

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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);
            // ddlColor.Items.Clear();
            Masters.ColorMaster.Color_Select(ddlColor, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlBrand.SelectedValue));

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }

    protected void gvStockReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStockReport.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }

    protected void gvModelNoSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[1].Visible = false;

            // Total Inward -Green
            e.Row.Cells[5].ForeColor = Color.DarkRed;
            // Total Outward -Red
            //e.Row.Cells[6].ForeColor = Color.Red;
            //// Total Block -Dark Red
            //e.Row.Cells[7].ForeColor = Color.DarkRed;

            //if (e.Row.Cells[5].Text == e.Row.Cells[6].Text)
            //{
            //    e.Row.Visible = false;
            //}
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[17].Visible = false;
            //e.Row.Cells[18].Visible = false;

        }
    }
    protected void gvModelNoSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvModelNoSearch.PageIndex = e.NewPageIndex;
        //BindGridOnModelSearch();

    }
    protected void btnQR_Click(object sender, EventArgs e)
    {
        //btnQR.Enabled = false;
        foreach (GridViewRow gvrow in gvStockReport.Rows)
        {
            CheckBox ch = new CheckBox();
                    ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                    if (ch.Checked == true)
                    {
                        if (gvrow.Cells[11].Text != "")
                        {
                            string code = "MRN No :" + gvrow.Cells[6].Text + "\n" + "Dt:" + gvrow.Cells[7].Text + "\n" + "Model No :" + gvrow.Cells[1].Text + "\n" + "Color :" + gvrow.Cells[4].Text + "\n" + " Brand=" + gvrow.Cells[2].Text + "\n" + " Description=" + gvrow.Cells[3].Text + "\n" + " ID=" + gvrow.Cells[13].Text + "";
                            lblCode.Text = code;
                        }
                        else
                        {
                            string code = "MRN No :" + gvrow.Cells[11].Text + "\n" + "Dt:" + gvrow.Cells[17].Text + "\n" + "Model No :" + gvrow.Cells[1].Text + "\n" + "Color :" + gvrow.Cells[4].Text + "\n" + " Brand=" + gvrow.Cells[2].Text + "\n" + " Description=" + gvrow.Cells[3].Text + "\n" + " ID=" + gvrow.Cells[13].Text + "";
                            lblCode.Text = code;

                        }
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(lblCode.Text, QRCodeGenerator.ECCLevel.Q);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                string itemimage = "";
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                File.WriteAllBytes(Server.MapPath("~/Content/QRCodes/" + gvrow.Cells[13].Text + ".png"), byteImage);
                                //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

                                Inventory.QRCode obj = new Inventory.QRCode();
                                itemimage = gvrow.Cells[13].Text + ".png";
                                obj.MRN_Det_Id = gvrow.Cells[11].Text;
                                obj.Item_Code = gvrow.Cells[0].Text;
                                obj.Image = itemimage;
                                obj.Image_Path = "http://183.82.108.55/Content/QRCodes/" + gvrow.Cells[13].Text + ".png";
                                obj.Item_Id = gvrow.Cells[13].Text;
                                obj.CHK_DET_Color = gvrow.Cells[12].Text;
                                obj.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                                TextBox txtqty = (TextBox)gvrow.FindControl("txtqtyw");
                                obj.PrintQty  = txtqty.Text;

                                TextBox txtPrintqty = (TextBox)gvrow.FindControl("txtqty");
                                obj.Qty = txtPrintqty.Text;
                                obj.ISPrint = "0";
                                obj.LocName = gvrow.Cells[10].Text;
                                obj.Updateddt = DateTime.Now.ToString();
                                obj.QRImage_Save();

                            }
                            //plBarCode.Controls.Add(imgBarCode);
                            //BindGrid();
                            
                        }
                    }
        }
        BindSearchGrid();

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string Ids = string.Empty;
        string tx1 = string.Empty;
        string tx2 = string.Empty;
        string QrID = string.Empty;

        
        foreach (GridViewRow gvrow in gvStockReport.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("CheckBox_row");
            TextBox t1 = gvrow.FindControl("txtqtyw") as TextBox;
            TextBox t2 = gvrow.FindControl("txtqty") as TextBox;
            //Label lblQrID = gvrow.FindControl("txtqrid") as Label;
            tx1 = t1.Text;
            tx2 = t2.Text;

            //QrID = lblQrID.Text;

                    if (ch.Checked == true)
                    {
                       // QrID = lblQrID.Text;
                        Ids += string.Format("pha{0}jam,", gvrow.Cells[13].Text);
                        //tx2 += string.Format(",", t2.Text);
                        QrID += string.Format("{0},", gvrow.Cells[20].Text);
                    }
                    
        }
        if (!string.IsNullOrEmpty(Ids))
        {
            Ids = string.Format(Ids.Substring(0, Ids.Length - 1));
            tx2 = string.Format(tx2.Substring(0, tx2.Length - 1));
           

        }
        if (!string.IsNullOrEmpty(QrID))
        {
            QrID = string.Format(QrID.Substring(0, QrID.Length - 1));
        }
        string Isprint = "1";
        StringBuilder sb = new StringBuilder(Ids);
        sb.Replace("pha", "'");
        sb.Replace("jam", "'");

        string hai = sb.ToString();
        Inventory.QRCode.QRCodeStatus_Update(Isprint, hai);
        //if (!string.IsNullOrEmpty(Ids))
        //{
        //    Ids = string.Format("WHERE Quotation_Id IN ({0})", condition.Substring(0, condition.Length - 1));
        //}
        //try
        //{
        //    string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCodeProc&ItemCode=" + gvrow.Cells[0].Text + "&t1=" + t1.Text + "&t2=" + t2.Text + "&ID=" + gvrow.Cells[13].Text + " ";

        //    //string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCodeLabel&Qty=" + gvrow.Cells[5].Text +" ";
        //    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message);
        //}
        try
        {
            


            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCodeProc1&t1=" + tx1 + "&t2=" + tx2 + "&ID="+Server.UrlEncode(hai)+ "&QRId="+QrID +" ";

            //string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCodeLabel&Qty=" + gvrow.Cells[5].Text +" ";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            BindSearchGrid();
       
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void btnPrintRoller_Click(object sender, EventArgs e)
    {
        string Ids = string.Empty;
        string tx1 = string.Empty;
        string tx2 = string.Empty;

        foreach (GridViewRow gvrow in gvStockReport.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("CheckBox_row");
            TextBox t1 = gvrow.FindControl("txtqtyw") as TextBox;
            TextBox t2 = gvrow.FindControl("txtqty") as TextBox;
            tx1 = t1.Text;
            tx2 = t2.Text;


            if (ch.Checked == true)
            {
                Ids += string.Format("pha{0}jam,", gvrow.Cells[13].Text);
                tx2 += string.Format(",", t2.Text);

            }

        }
        if (!string.IsNullOrEmpty(Ids))
        {
            Ids = string.Format(Ids.Substring(0, Ids.Length - 1));
            tx2 = string.Format(tx2.Substring(0, tx2.Length - 1));

        }
        string Isprint = "1";
        StringBuilder sb = new StringBuilder(Ids);
        sb.Replace("pha", "'");
        sb.Replace("jam", "'");

        string hai = sb.ToString();
        Inventory.QRCode.QRCodeStatus_Update(Isprint, hai);
        BindSearchGrid();
        try
        {



            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCodeProc2&t1=" + tx1 + "&t2=" + tx2 + "&ID=" + Server.UrlEncode(hai) + " ";

            //string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCodeLabel&Qty=" + gvrow.Cells[5].Text +" ";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
}