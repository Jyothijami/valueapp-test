using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Data;
using System.Text;
using vllib;

/// <summary>
/// Summary description for Export
/// </summary>
public class Export
{
	public Export()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public enum Extension
    {
        Word,Excel,PDF
    }
    public void ExportGrid(DataTable dt, string filename, Extension ext)
    {
        if (dt == null || filename.Trim() == "")
        {
            return;
        }
        else
        {
            GridView gv = new GridView();
            gv.DataSource = dt; gv.DataBind();
            ExportGrid(gv, filename, ext);
        }
    }
    public void ExportGrid(GridView gv, string filename, Extension ext)
    {
        if (gv == null || filename.Trim() == "")
        {
            return;
        }
        else
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;

            switch (ext)
            {
                case Extension.Word: HttpContext.Current.Response.ContentType = "application/vnd.ms-word"; filename += ".doc";
                    break;
                case Extension.Excel: HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"; filename += ".xls";
                    break;
                case Extension.PDF: HttpContext.Current.Response.ContentType = "application/pdf";
                    break;

            }

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.AllowPaging = false; //gv.DataBind();
            gv.RenderControl(hw);


            if (ext == Extension.PDF)
            {
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                HttpContext.Current.Response.Write(pdfDoc);

            }
            else
            {
                HttpContext.Current.Response.Write(sw.ToString());
            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            gv.AllowPaging = true;
            gv.DataBind();
        }
    }
    public void ExportGrid(StringBuilder sbHtml, string filename, Extension ext)
    {
        //StringBuilder strHtml = new StringBuilder(str);
        //if (Control is GridView)
        //{
        //    string s = "";
        //}
        if (sbHtml.ToString().Trim() == "" || filename.Trim() == "")
        {
            return;
        }
        else
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;

            switch (ext)
            {
                case Extension.Word: HttpContext.Current.Response.ContentType = "application/vnd.ms-word"; filename += ".doc";
                    break;
                case Extension.Excel: HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"; filename += ".xls";
                    break;
                case Extension.PDF: HttpContext.Current.Response.ContentType = "application/pdf"; filename += ".pdf";
                    break;

            }

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter(sbHtml);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //gv.AllowPaging = false; //gv.DataBind();
            //control.RenderControl(hw);


            if (ext == Extension.PDF)
            {
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 80, 50, 30, 65);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                HttpContext.Current.Response.Write(pdfDoc);

            }
            else
            {
                HttpContext.Current.Response.Write(sw.ToString());
            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            //Control.AllowPaging = true;
            //Control.DataBind();
        }
    }
}