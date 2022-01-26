using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;
using YantraDAL;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Modules_HR_TourExpansesClaim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EmployeeMaster_Fill();
        }
    }
    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {

            HR.EmployeeMaster.EmployeeMaster_SelectSales12(ddlEmp);
            HR.RegionalMaster_Select(ddlLocation);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    #endregion
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        tbldet.Visible = true;
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable VoucherDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("DetTXId");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("From");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("To");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("TravelMode");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("Class");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("Total");
        VoucherDetails.Columns.Add(col);
        col = new DataColumn("Remarks");
        
        VoucherDetails.Columns.Add(col);

        if (gvTravelExp.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvTravelExp.Rows)
            {
                if (gvTravelExp.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvTravelExp.SelectedRow.RowIndex)
                    {
                        DataRow dr = VoucherDetails.NewRow();
                        dr["DetTXId"] = lblDetTxId.Text;
                        dr["From"] = txtFrom.Text;
                        dr["To"] = txtTo.Text;
                        dr["TravelMode"] = ddlTravelMode.SelectedItem .Text;
                        dr["Class"] = ddlClass.SelectedItem.Text;
                        dr["Total"] = txtTotal.Text;
                        dr["Remarks"] = txtRemarks.Text;

                        VoucherDetails.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = VoucherDetails.NewRow();
                        dr["DetTXId"] = gvrow.Cells[1].Text;
                        dr["From"] = gvrow.Cells[2].Text;
                        dr["To"] = gvrow.Cells[3].Text;
                        dr["TravelMode"] = gvrow.Cells[4].Text;
                        dr["Class"] = gvrow.Cells[5].Text;
                        dr["Total"] = gvrow.Cells[6].Text;
                        dr["Remarks"] = gvrow.Cells[7].Text;

                        VoucherDetails.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = VoucherDetails.NewRow();
                    dr["DetTXId"] = gvrow.Cells[1].Text;
                    dr["From"] = gvrow.Cells[2].Text;
                    dr["To"] = gvrow.Cells[3].Text;
                    dr["TravelMode"] = gvrow.Cells[4].Text;
                    dr["Class"] = gvrow.Cells[5].Text;
                    dr["Total"] = gvrow.Cells[6].Text;
                    dr["Remarks"] = gvrow.Cells[7].Text;

                    VoucherDetails.Rows.Add(dr);
                }
            }
        }

        if (gvTravelExp.SelectedIndex == -1)
        {
            DataRow drnew = VoucherDetails.NewRow();
            drnew["DetTXId"] = lblDetTxId.Text;
            drnew["From"] = txtFrom.Text;
            drnew["To"] = txtTo.Text;
            drnew["TravelMode"] = ddlTravelMode.SelectedItem.Text;
            drnew["Class"] = ddlClass.SelectedItem.Text;
            drnew["Total"] = txtTotal.Text;
            drnew["Remarks"] = txtRemarks.Text;

            VoucherDetails.Rows.Add(drnew);
        }
        gvTravelExp.DataSource = VoucherDetails;
        gvTravelExp.DataBind();
        gvTravelExp.SelectedIndex = -1;
        //btnRefresh_Click(sender, e);
    }
}