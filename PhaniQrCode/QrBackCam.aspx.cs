using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;

public partial class QrBackCam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {


        Inventory.QRCode obj = new Inventory.QRCode();
        if (obj.QRItems_Select(txtQrcode .Text) > 0)
        {
            //txtitemcode.Text = obj.MatCode;
            //txtUom.Text = obj.UomName;
            //txtcolor.Text = obj.Description;
        }



        #region code

        DataTable SalesOrderItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Color");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Uom");
        SalesOrderItems.Columns.Add(col);
        col = new DataColumn("Qty");
        SalesOrderItems.Columns.Add(col);


        if (gvItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItems.Rows)
            {
                if (gvItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItems.SelectedRow.RowIndex)
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["ItemCode"] = txtitemcode.Text;
                        dr["Color"] = txtcolor.Text;
                        dr["Uom"] = txtUom.Text;
                        dr["Qty"] = "0";
                       
                        SalesOrderItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = SalesOrderItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[1].Text;
                        dr["Color"] = gvrow.Cells[2].Text;
                        dr["Uom"] = gvrow.Cells[3].Text;
                        dr["Qty"] = gvrow.Cells[4].Text;
                        SalesOrderItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = SalesOrderItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[1].Text;
                    dr["Color"] = gvrow.Cells[2].Text;
                    dr["Uom"] = gvrow.Cells[3].Text;
                    dr["Qty"] = gvrow.Cells[4].Text;
                    SalesOrderItems.Rows.Add(dr);
                }
            }
        }

        if (gvItems.SelectedIndex == -1)
        {
            DataRow drnew = SalesOrderItems.NewRow();
            drnew["ItemCode"] = txtitemcode.Text;
            drnew["Color"] = txtcolor.Text;
            drnew["Uom"] = txtUom.Text;
            drnew["Qty"] = "0";
            SalesOrderItems.Rows.Add(drnew);
        }
        gvItems.DataSource = SalesOrderItems;
        gvItems.DataBind();
        gvItems.SelectedIndex = -1;
        Clear_Items();
        //}


        #endregion
    }

    private void Clear_Items()
    {
        txtitemcode.Text = "";
        txtcolor.Text = "";
        txtUom.Text = "";
        txtQrcode.Text = "";
    }




    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void txtQrcode_TextChanged(object sender, EventArgs e)
    {
        //txtQrcode.Text = "";
        txtQrcode.Focus();
    }
}