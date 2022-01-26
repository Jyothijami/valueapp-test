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

public partial class Modules_Masters_ItemImage : System.Web.UI.Page
{
    public string ITEM_CODE;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        //    lblCPID.Text = cp.getPresentCompanySessionValue();
            gvProductMasterDetails.DataBind();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (gvProductMasterDetails.SelectedIndex > -1)
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            ITEM_CODE = objMaster.ItemCode;
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

    #region Save Image Function
    private void SaveImage()
    {
        string filePath = FileUpload1.PostedFile.FileName;
        string filename = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename);
        string contenttype = String.Empty;

        switch (ext)
        {


            case ".wmf":
                contenttype = "image/wmf";
                break;
            case ".tif":
                contenttype = "image/tif";
                break;

            case ".jpeg":
                contenttype = "image/jpeg";
                break;
           
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

            case ".JPEG":
                contenttype = "image/jpeg";
                break;

            case ".TIF":
                contenttype = "image/tif";
                break;

            case ".WMF":
                contenttype = "image/wmf";
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

            String strQuery = "update YANTRA_ITEM_MAST set IMAGE=@image WHERE ITEM_CODE=" + Convert.ToInt16(gvProductMasterDetails.SelectedRow.Cells[0].Text) + "";
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





    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lbtnProductName;
        lbtnProductName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnProductName.Parent.Parent;
        gvProductMasterDetails.SelectedIndex = gvRow.RowIndex;
    }



    protected void gvProductMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int count = General.CountofRecordsWithQuery("select count(*) from YANTRA_ITEM_MAST where ITEM_CODE = " + Convert.ToInt16(e.Row.Cells[0].Text) + "");
            if (count > 0)
                (e.Row.FindControl("Image") as Image).ImageUrl = "~/Modules/Masters/ItemMasterimageupload.ashx?id=" + Convert.ToInt16(e.Row.Cells[0].Text) + "";

        }
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
       
        gvProductMasterDetails.DataBind();
    }
}

 
