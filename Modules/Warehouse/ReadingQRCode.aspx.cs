using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;

public partial class Modules_Warehouse_ReadingQRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void txtQR_TextChanged(object sender, EventArgs e)
    {


        // Match match = Regex.Match(txtQR.Text, @"ID", RegexOptions.IgnoreCase);
        //  Regex regex = new Regex(@"(?<=ID=)\d+");

        Regex regex = new Regex(@"ID=([A-Za-z0-9\-]+)");
        Match match = regex.Match(txtQR.Text);


        //Match match = Regex.Match(txtQR.Text, @"ID=/([A-Za-z0-9\-]+)\$",
        //   RegexOptions.IgnoreCase);


        // Regex.Matches(txtQR.Text, @"=(.*?)");

        //  Label3.Text = match.Groups[1].Value;

        var s = match.Value;
        var match1 = Regex.Match(s, "=([^-]+)$");
        if (match1.Success)
        {
            Label3.Text = match1.Groups[1].Value;

        }

        //SelectQrItems();
        btnAdd_Click(sender, e);
        txtQR.Text = "";
        txtQR.Focus();

    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Inventory.QRCode obj = new Inventory.QRCode();
        if (obj.QRItems_Select(Label3.Text) > 0)
        {
            txtItemCode.Text = obj.Item_Code;
            txtModelNo.Text = obj.ITEM_Model_No;
            txtBrand.Text = obj.Brand;
            txtColorName.Text = obj.COlour_NAme;
            txtQty.Text = obj.Qty;
            txtRemarks.Text = "QR MDC";
            txtClientName.Text = "-";
            txtBrandId.Text = obj.BrandId;
            txtColorId.Text = obj.CHK_DET_Color;

        }

        DataTable QRItems = new DataTable();

        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        QRItems.Columns.Add(col);
        col = new DataColumn("Brand");
        QRItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        QRItems.Columns.Add(col);
        col = new DataColumn("Color");
        QRItems.Columns.Add(col);
        col = new DataColumn("Qty");
        QRItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        QRItems.Columns.Add(col);
        col = new DataColumn("ClientName");
        QRItems.Columns.Add(col);
        col = new DataColumn("BrandId");
        QRItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        QRItems.Columns.Add(col);
        col = new DataColumn("Remark");
        QRItems.Columns.Add(col);

        //col = new DataColumn("Specifications");
        //QRItems.Columns.Add(col);

        if (gvMovingItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvMovingItems.Rows)
            {
                if (gvMovingItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvMovingItems.SelectedRow.RowIndex)
                    {


                        DataRow dr = QRItems.NewRow();
                        dr["ItemCode"] = txtItemCode.Text;

                        dr["Brand"] = txtBrand.Text;
                        dr["ModelNo"] = txtModelNo.Text;
                        dr["Color"] = txtColorName.Text;

                        dr["Qty"] = txtQty.Text;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["ClientName"] = txtClientName.Text;

                        dr["BrandId"] = txtQty.Text;
                        dr["ColorId"] = txtColorId.Text;
                        dr["Remark"] = txtRemarks1.Text;

                        QRItems.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = QRItems.NewRow();
                        dr["ItemCode"] = gvrow.Cells[0].Text;
                        dr["Brand"] = gvrow.Cells[1].Text;
                        dr["ModelNo"] = gvrow.Cells[2].Text;
                        dr["Color"] = gvrow.Cells[3].Text;
                        //dr["Qty"] = gvrow.Cells[4].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        dr["Qty"] = qty.Text;
                        dr["Remarks"] = gvrow.Cells[5].Text;
                        dr["ClientName"] = gvrow.Cells[6].Text;
                        dr["BrandId"] = gvrow.Cells[7].Text;
                        dr["ColorId"] = gvrow.Cells[8].Text;
                        //dr["Remark"] = gvrow.Cells[9].Text;
                        TextBox txtRemarks = (TextBox)gvrow.FindControl("txtRemarks");
                        dr["Remark"] = txtRemarks.Text;
                        QRItems.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = QRItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[0].Text;
                    dr["Brand"] = gvrow.Cells[1].Text;
                    dr["ModelNo"] = gvrow.Cells[2].Text;
                    dr["Color"] = gvrow.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    dr["Qty"] = qty.Text;
                    dr["Remarks"] = gvrow.Cells[5].Text;
                    dr["ClientName"] = gvrow.Cells[6].Text;
                    dr["BrandId"] = gvrow.Cells[7].Text;
                    dr["ColorId"] = gvrow.Cells[8].Text;
                    TextBox txtRemarks = (TextBox)gvrow.FindControl("txtRemarks");
                    dr["Remark"] = txtRemarks.Text;
                    QRItems.Rows.Add(dr);
                }
            }
        }
        if (gvMovingItems.SelectedIndex == -1)
        {

            DataRow drnew = QRItems.NewRow();
            drnew["ItemCode"] = txtItemCode.Text;

            drnew["Brand"] = txtBrand.Text;
            drnew["ModelNo"] = txtModelNo.Text;
            drnew["Color"] = txtColorName.Text;

            drnew["Qty"] = txtQty.Text;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["ClientName"] = txtClientName.Text;

            drnew["BrandId"] = txtQty.Text;
            drnew["ColorId"] = txtColorId.Text;
            drnew["Remark"] = txtRemarks1.Text;

            QRItems.Rows.Add(drnew);
        }
        gvMovingItems.DataSource = QRItems;
        gvMovingItems.DataBind();
        gvMovingItems.SelectedIndex = -1;
        Label3.Text = string.Empty;
    }

}