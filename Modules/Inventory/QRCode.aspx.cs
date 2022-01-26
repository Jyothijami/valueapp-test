using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Text.RegularExpressions;
using iTextSharp.text.html.simpleparser;
using Yantra.MessageBox;

public partial class Modules_Inventory_QRCode : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid_AllCond();
        }
    }

    private void BindGrid_AllCond()
    {
        SqlCommand cmd = new SqlCommand("USP_Inventory_QRSerach_Cond", con);
        cmd.CommandType = CommandType.StoredProcedure;

        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInventoryInward.DataSource = dt;
        gvInventoryInward.DataBind();
    }
    protected void btnQR_Click(object sender, EventArgs e)
    {
        Button lbtnCompanyName;
        lbtnCompanyName = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvInventoryInward.SelectedIndex = gvRow.RowIndex;
        if (gvInventoryInward.SelectedIndex > -1)
        {
            string code = "MRN No :" + gvInventoryInward.SelectedRow.Cells[1].Text + "\n" + "MRN Date :" + gvInventoryInward.SelectedRow.Cells[2].Text + "\n" + "Model No :" + gvInventoryInward.SelectedRow.Cells[5].Text + "\n" + "Color :" + gvInventoryInward.SelectedRow.Cells[8].Text+"\n" +" I="+gvInventoryInward.SelectedRow .Cells [3].Text+"\n"+" C="+gvInventoryInward .SelectedRow .Cells [14].Text +"\n"+" ID="+gvInventoryInward .SelectedRow .Cells [12].Text+"";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 150;
            imgBarCode.Width = 150;
            //using (Bitmap bitMap = qrCode.GetGraphic(20))
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();
            //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //    }
            //    plBarCode.Controls.Add(imgBarCode);
            //}


            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    string itemimage = "";
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    File.WriteAllBytes(Server.MapPath("~/Content/QRCodes/" + gvInventoryInward.SelectedRow.Cells[12].Text + ".png"), byteImage);
                    //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    
                    Inventory.QRCode obj = new Inventory.QRCode();
                    itemimage = gvInventoryInward.SelectedRow.Cells[12].Text + ".png";
                    obj.MRN_Det_Id = gvInventoryInward.SelectedRow.Cells[12].Text;
                    obj.Item_Code = gvInventoryInward.SelectedRow.Cells[3].Text;
                    obj.Image = itemimage;
                    obj.Image_Path = "http://183.82.108.55/Content/QRCodes/" + gvInventoryInward.SelectedRow.Cells[12].Text+".png" ;
                    obj.QRImage_Save();
                    
                }
                //plBarCode.Controls.Add(imgBarCode);
                //BindGrid();
                BindGrid_AllCond();
            }
        }

    }

    //private void ExportToPDF()
    //{
    //    if (gvInventoryInward.Rows.Count > 0)
    //    {
    //        BindGrid_AllCond();
    //        //for (int i = 0; i < gvInventoryInward.Rows.Count; i++)
    //        //{
    //        GridViewRow row = gvInventoryInward.Rows;
    //        row.Visible = false;

    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            gvInventoryInward.Rows.Style.Add(HtmlTextWriterStyle.Height, "100px");
    //            gvInventoryInward.Rows.Style.Add(HtmlTextWriterStyle.Width, "100px");
    //            //string imageName = "~/Content/QRCodes/" + (row.FindControl("lblImage") as Label).Text;
    //            //System.Web.UI.WebControls.Image img1 = row.Cells[13].Controls[1] as System.Web.UI.WebControls.Image;
    //            //row.Cells[13].Controls.Add(img1);
    //            //img1.Height = Unit.Pixel(100);
    //            //img1.Width = Unit.Pixel(100);
    //            string img = gvInventoryInward.Row.Cells[13].Text;

    //            byte[] file;
    //            file = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/QRCodes/" + img));//ImagePath    
    //            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(file);
    //            jpg.ScaleToFit(100F, 100F);//Set width and height in float    

    //            lblModelNo.Text = row.Cells[5].Text;
    //            lblQty.Text = row.Cells[9].Text;
    //            lblmrndt.Text = row.Cells[2].Text;
    //            lblColor.Text = row.Cells[8].Text;

    //        }
    //        //}
    //    }
    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("content-disposition", "attachment;filename=FileName.pdf");
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter w = new HtmlTextWriter(sw);
    //    print.RenderControl(w);
    //    string htmWrite = sw.GetStringBuilder().ToString();
    //    htmWrite = Regex.Replace(htmWrite, "</?(a|A).*?>", "");
    //    htmWrite = htmWrite.Replace("\r\n", "");
    //    StringReader reader = new StringReader(htmWrite);
    //    Document doc = new Document(PageSize.A6, 5f, 5f, 20f, 0f);
    //    string pdfFilePath = Server.MapPath(".") + "/PDFFiles";
    //    HTMLWorker htmlparser = new HTMLWorker(doc);
    //    PdfWriter.GetInstance(doc, Response.OutputStream);
    //    doc.Open();
    //    try
    //    {
    //        htmlparser.Parse(reader);
    //        doc.Close();
    //        Response.Write(doc);
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    { }
    //    finally
    //    {
    //        doc.Close();
    //    }  
    //}
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvInventoryInward.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=QRCode&QRid=" + gvInventoryInward.SelectedRow.Cells[12].Text + " ";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select a Record");
        }
        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}