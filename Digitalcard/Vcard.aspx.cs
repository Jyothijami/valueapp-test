using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vcard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Cid"] != null)
            {
                string Cid = Request.QueryString["Cid"].ToString();
                HR.EmployeeMaster obj = new HR.EmployeeMaster();
                if (obj.EmployeeMaster_Select(Cid) > 0)
                {
                    lblName.Text = obj.EmpFirstName + ' ' + obj.EmpLastName;
                    lblDept.Text = obj.DesgName12;
                    if (obj.AssignedEmailId == "" && obj.EmpEMail == "")
                    {
                        mail.Attributes["href"] = "mailto:info@valueline.in" ;
                    }
                    else if (obj.AssignedEmailId != "")
                    {
                        mail.Attributes["href"] = "mailto:" + obj.AssignedEmailId;
                    }
                    else
                    {
                        mail.Attributes["href"] = "mailto:" + obj.EmpEMail ;

                    }
                    Phn.Attributes["href"] = "tel:" + obj.AssignedMobileNo;
                    wa.Attributes["href"] = "https://wa.me/" + obj.AssignedMobileNo;

                }

            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        Panel1.RenderControl(hw);
        //        //gvitemsgrid.HeaderRow.Style.Add("width", "15%");

        //        //gvterms.RenderControl(hw);
        //        Response.Clear();
        //        Response.Buffer = true;
        //        //Response.AddHeader("content-disposition", "attachment;filename=Quotation.xls");
        //        Response.Charset = "";
        //        //Response.ContentType = "application/vnd.ms-excel";
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=Quotation.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        //style to format numbers to string
        //        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0.0f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();
        //        Response.Write(pdfDoc);

        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

        StringReader sr = new StringReader(Request.Form[hfGridHtml.UniqueID]);
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        pdfDoc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=HTML.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
    }
}