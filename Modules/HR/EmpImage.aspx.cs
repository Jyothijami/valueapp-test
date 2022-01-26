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

using Yantra.MessageBox;
using System.IO;
using System.Data.SqlClient;
using Yantra.Classes;


public partial class Modules_Masters_EmpImage : System.Web.UI.Page
{


    public string ITEM_CODE;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (gvProductMasterDetails.SelectedIndex > -1)
        {
            //SaveImage();
            //Masters.ItemMaster objMaster = new Masters.ItemMaster();
           HR.EmployeeMaster objMaster = new HR.EmployeeMaster();
            ITEM_CODE = objMaster.EmpID;
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

            String strQuery = "update YANTRA_EMPLOYEE_MAST set EMP_PHOTO=@image WHERE EMP_ID=" + Convert.ToInt16(gvProductMasterDetails.SelectedRow.Cells[0].Text) + "";
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



    protected void gvProductMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int count = General.CountofRecordsWithQuery("select count(*) from YANTRA_EMPLOYEE_MAST where EMP_ID = " + Convert.ToInt16(e.Row.Cells[0].Text) + "");
            if (count > 0)
                (e.Row.FindControl("Image") as Image).ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + Convert.ToInt16(e.Row.Cells[0].Text) + "";

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lbtnProductName;
        lbtnProductName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnProductName.Parent.Parent;
        gvProductMasterDetails.SelectedIndex = gvRow.RowIndex;
    }
}
