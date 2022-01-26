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

public partial class Modules_HR_Employee_Images : System.Web.UI.Page
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
        } 
    }

    protected void gvProductMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow||e.Row.RowType==DataControlRowType.Header)
        {
            //e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text == "0")
            {
                e.Row.BackColor = System.Drawing.Color.Pink;
                //e.Row.ForeColor = System.Drawing.Color.White;
            }
            // int count = General.CountofRecordsWithQuery("select count(*) from YANTRA_EMPLOYEE_MAST where EMP_ID = " + Convert.ToInt16(e.Row.Cells[0].Text) + "");
            // if (count > 0)
                // (e.Row.FindControl("Image") as Image).ImageUrl = "~/Modules/Masters/EmpImage.ashx?id=" + Convert.ToInt16(e.Row.Cells[0].Text) + "";

        }
       
    }
  
}