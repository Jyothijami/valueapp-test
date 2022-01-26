using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class dev_pages_QRCode : System.Web.UI.Page
{
    //private void Page_PreInit(object sender, System.EventArgs e)
    //{
    //    if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
    //    {
    //        Response.Redirect("~/MobileLogin.aspx");
    //    }

    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCompany.Text = cp.getPresentCompanySessionValue();
            ddlMovingFrom.DataBind();
            ddlMovingTo.DataBind();
            txtDCNo.Text = Inventory.StockMovement.InternalDC_AutoGenCode();

            txtmovingdate .Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {


        Regex regex = new Regex(@"ID=([A-Za-z0-9\-]+)");
        Match match = regex.Match(txtQrcode.Text);


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




        #region code

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

        if (gvItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItems.Rows)
            {
                if (gvItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItems.SelectedRow.RowIndex)
                    {


                        DataRow dr = QRItems.NewRow();
                        dr["ItemCode"] = txtItemCode.Text;

                        dr["Brand"] = txtBrand.Text;
                        dr["ModelNo"] = txtModelNo.Text;
                        dr["Color"] = txtColorName.Text;

                        dr["Qty"] = txtQty.Text;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["ClientName"] = txtClientName.Text;

                        dr["BrandId"] = txtBrandId.Text;
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
        if (gvItems.SelectedIndex == -1)
        {

            DataRow drnew = QRItems.NewRow();
            drnew["ItemCode"] = txtItemCode.Text;

            drnew["Brand"] = txtBrand.Text;
            drnew["ModelNo"] = txtModelNo.Text;
            drnew["Color"] = txtColorName.Text;

            drnew["Qty"] = txtQty.Text;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["ClientName"] = txtClientName.Text;

            drnew["BrandId"] = txtBrandId.Text;
            drnew["ColorId"] = txtColorId.Text;
            drnew["Remark"] = txtRemarks1.Text;

            QRItems.Rows.Add(drnew);
        }
        gvItems.DataSource = QRItems;
        gvItems.DataBind();
        gvItems.SelectedIndex = -1;

        Clear_Items();

        #endregion
    }

    private void Clear_Items()
    {
        txtItemCode.Text = "";
        txtBrand.Text = "";
        txtModelNo.Text = "";
        txtColorName.Text = "";
        txtQty.Text = "";
        txtRemarks.Text = "";
        txtClientName.Text = "";
        txtBrandId.Text = "";
        txtColorId.Text = "";
        txtRemarks1.Text = "";
        Label3.Text = string.Empty;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            StockMovementSave();
            // gvInternalDCDtls.DataBind();
            
            //  Response.Redirect("StockMovement.aspx");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
 "alert(' Moving DC Saved sucessfully');window.location ='QRBackCam.aspx';", true);
        }
    }

    private void StockMovementSave()
    {
        if (gvItems.Rows.Count > 0)
        {
            try
            {
                Inventory.StockMovement obj = new Inventory.StockMovement();
                SCM.BeginTransaction();

                obj.IntIndId = "0";
                obj.MovingFrom = ddlMovingFrom.SelectedValue;
                obj.MovingDcDate = Yantra.Classes.General.toMMDDYYYY(txtmovingdate.Text);
                obj.CpId = cp.getPresentCompanySessionValue();
                obj.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                obj.Status = "Open";
                obj.VechileNo = txtVechileNo.Text;
                obj.Movingto = ddlMovingTo.SelectedItem.Value;
                obj.DCNo = txtDCNo.Text;
                if (obj.StockMovementMaster_Save() == "Data Saved Successfully")
                {
                    // obj.SuppliersQuotationDetails_Delete(obj.SmdcId)
                    foreach (GridViewRow gvrow in gvItems.Rows)
                    {
                        obj.Itemcode = gvrow.Cells[0].Text;
                        obj.Brandid = gvrow.Cells[7].Text;
                        obj.colorid = gvrow.Cells[8].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        obj.Quantity = qty.Text;
                        // DropDownList location=(DropDownList)gvrow.FindControl("ddllocation");
                        obj.Movingto = ddlMovingTo.SelectedValue;
                        obj.Remarks = gvrow.Cells[5].Text;
                        obj.Clientname = gvrow.Cells[6].Text;
                        obj.RecQty = qty.Text;
                        TextBox remark = (TextBox)gvrow.FindControl("txtRemarks");
                        obj.Remark = remark.Text;
                        obj.StockMovementDetails_Save();



                        //string Itemcode = gvrow.Cells[0].Text;

                        //SqlCommand cmd = new SqlCommand("select  Item_ID,whLocId from dbo.INWARD where [ITEM_CODE]=" + Itemcode + " and [whLocId]=" + ddlMovingto.SelectedItem.Value + " ", con);
                        //cmd.CommandType = CommandType.Text;
                        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);
                        //Masters.ItemPurchase objout = new Masters.ItemPurchase();

                        //int Quantity = int.Parse(qty.Text);
                        //int locId = int.Parse(location.SelectedValue);
                        //int whLocId = int.Parse(ddlMovingto.SelectedItem.Value);

                        //if (dt.Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < Quantity; i++)
                        //    {

                        //        string itemId = dt.Rows[i][0].ToString();
                        //        objout.InwardLoc_Update(locId, itemId, whLocId);
                        //    }
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {


                SCM.ClearControls(this);
                SCM.Dispose();
                //internalDCtbl.Visible = false;

            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
}