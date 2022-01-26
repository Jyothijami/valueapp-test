using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;

public partial class Modules_Inventory_Recovery_Status : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindStatusOpen();
            BindStatusClose();


        }


    }

    private void BindStatusClose()
    {
        SqlCommand cmd = new SqlCommand("USP_Invoice_Close_Status", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvInvoiceStatusClosedDetails.DataSource = dt;
        gvInvoiceStatusClosedDetails.DataBind();

    }

    private void BindStatusOpen()
    {
        SqlCommand cmd = new SqlCommand("USP_Invoice_Open_Status", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);


        gvInvoiceStatusOpenDetails.DataSource = dt;
        gvInvoiceStatusOpenDetails.DataBind();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvInvoiceStatusOpenDetails.Rows)
        {

            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("CheckBox_row");
            if (ch.Checked == true)
            {
                //Label InvoiceNo = (Label)gvrow.FindControl("lblSI_ID");
                SqlCommand cmd = new SqlCommand("update YANTRA_SALES_INVOICE_MAST SET SI_STATUS='Closed' where SI_ID='" + gvrow.Cells[0].Text + "' ", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if(i>0)
                {
                    MessageBox.Show(this, "Data Updated Sucucessfully");
                }
                BindStatusOpen();
                BindStatusClose();

            }
        }
    }
    protected void gvInvoiceStatusOpenDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            

        }
    }
}
 
