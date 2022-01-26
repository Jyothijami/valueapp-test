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
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.IO;
using System.Data.SqlClient;
using Yantra.Classes;

public partial class Modules_Masters_ImageUpload : System.Web.UI.Page
{
    public string CompanyId;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Upload Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (gvCompanyDetails.SelectedIndex > -1)
        {
            Masters.ProductMasterDetails objMaster = new Masters.ProductMasterDetails();
            CompanyId = objMaster.Product_Id;
            if (FileUpload1.HasFile)
            {
                SaveImage();
            }
                  
                   
        }
        else
        {
            MessageBox.Show(this, "Please Select Atleast One Record");
        }
    }
    #endregion

    #region Save Image Function
    private void SaveImage()
    {
        string filePath = FileUpload1.PostedFile.FileName;
        string filename = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename);
        string contenttype = String.Empty;

        switch (ext)
        {
            case ".jpg":
                contenttype = "image/jpg";
                break;

            case ".png":
                contenttype = "image/png";
                break;

            case ".gif":
                contenttype = "image/gif";
                break;

            case ".JPG":
                contenttype = "image/jpg";
                break;

            case ".PNG":
                contenttype = "image/png";
                break;

            case ".GIF":
                contenttype = "image/gif";
                break;

            case ".WMF":
                contenttype = "image/wmf";
                break;

            case ".wmf":
                contenttype = "image/wmf";
                break;

            case ".JPEG":
                contenttype = "image/jpeg";
                break;

            case ".jpeg":
                contenttype = "image/jpeg";
                break;

            case ".BMP":
                contenttype = "image/bmp";
                break;

            case ".bmp":
                contenttype = "image/bmp";
                break;
            
            case ".jpe":
                contenttype = "image/jpe";
                break;

            case ".JPE":
                contenttype = "image/jpe";
                break;

            case ".JFIF":
                contenttype = "image/jfif";
                break;

            case ".jfif":
                contenttype = "image/jfif";
                break;

            case ".TIFF":
                contenttype = "image/jfif";
                break;

            case ".tiff":
                contenttype = "image/jfif";
                break;
        }

        if (contenttype != String.Empty)
        {
            Stream fs = FileUpload1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);

            string connect = ConfigurationManager.ConnectionStrings["DBCon"].ToString();
            SqlConnection conn = new SqlConnection(connect);

            conn.Open();
            //String strQuery = "update cfamenities_master set aminityimage=@image WHERE amenitiesunkid=" + Amentyid + "";

            String strQuery = "update YANTRA_COMP_PROFILE set CP_LOGO=@image WHERE CP_ID=" + Convert.ToInt16(gvCompanyDetails.SelectedRow.Cells[1].Text) + "";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            cmd.Parameters.Add("@image", SqlDbType.Binary).Value = bytes;

            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show(this, "Image Uploaded Successfully");
                
            }
            else
            {
                MessageBox.Show(this, "Image  Uploading Failed..!");
            }
        }
    }
    #endregion

    #region Row Data Bound
    protected void gvCompanyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
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
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;
            e.Row.Cells[23].Visible = false;
            e.Row.Cells[24].Visible = false;
            e.Row.Cells[25].Visible = false;
            e.Row.Cells[26].Visible = false;
            e.Row.Cells[27].Visible = false;
            //e.Row.Cells[28].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int count = General.CountofRecordsWithQuery("select count(*) from YANTRA_COMP_PROFILE where CP_ID = " + Convert.ToInt16(e.Row.Cells[1].Text) + "");
            if (count > 0)
                (e.Row.FindControl("Image") as Image).ImageUrl = "~/Modules/Masters/ComapanyImage.ashx?id=" + Convert.ToInt16(e.Row.Cells[1].Text) + "";

        }
    }
    #endregion

    #region Link Button Click
    protected void lbtnCompanyName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnCompanyName;
        lbtnCompanyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvCompanyDetails.SelectedIndex = gvRow.RowIndex;
    }
    #endregion


}

 
