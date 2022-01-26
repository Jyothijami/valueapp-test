using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Warehouse_StockMovement : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblCompany.Text = cp.getPresentCompanySessionValue();
            ddlMovingFrom.DataBind();
            ddlMovingTo.DataBind();
            gvInternalDCDtls.DataBind();

            Inventory.Internalindent.Internalindent_select(ddlIndentno);
            ddlIndentno.DataBind();
            txtQR.Focus();

            //Inventory.Internalindent.Godown_Select(ddlMovingto);
        }
    }
    protected void ddlIndentno_SelectedIndexChanged(object sender, EventArgs e)
    {
         Inventory.Internalindent obj = new Inventory.Internalindent();

         if (obj.InternalIndent_Select(ddlIndentno.SelectedItem.Value) > 0)
         {
             //ddlMovingFrom.SelectedItem.Value = obj.to;
             txtIndentdate.Text = obj.Inddate;
             obj.InternalindentDetails_Select(ddlIndentno.SelectedItem.Value, gvIndentdetails);
         }
         
    }
    protected void rdblIndentfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblIndentfor.SelectedItem.Text == "General")
        {

            ddlIndentno.Enabled = true;
            txtIndentdate.Enabled = true;
            //txtQR.Enabled = false;
        }
        else
        {
            //ddlIndentno.Enabled = false;
            //txtIndentdate.Enabled = false;
            txtQR.Enabled = true;
            btnGo.Enabled = false;
        }
    }
    protected void btnRead_Click(object sender, EventArgs e) 
    {

        if (fileupload1.HasFile)
        {
            string Savepath = Server.MapPath("~/Uploaded/" + fileupload1.FileName);
            fileupload1.SaveAs(Savepath);

            string path = Savepath;
            if (!string.IsNullOrEmpty(path))
            {

                textBoxContents.Text = File.ReadAllText(path, Encoding.UTF8);
                string pat01 = @"ID=([A-Za-z0-9\-]+)";
                Regex rgx = new Regex(pat01);
                foreach (Match match in rgx.Matches(textBoxContents.Text))
                {
                    Label3.Text = match.Groups[1].Value;
                    SelectQrItems();
                }

            }
        }
        else
        {
            MessageBox.Show(this, "File Name Already Exists");
        }
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

        SelectQrItems();

        txtQR.Text = "";
        txtQR.Focus();

    }
    private void SelectQrItems()
    {
        Inventory.QRCode obj = new Inventory.QRCode();
        if (obj.QRItems_Select(Label3.Text) > 0)
        {
            txtItemCode.Text = obj.Item_Code;
            txtModelNo.Text = obj.ITEM_Model_No;
            txtBrand.Text = obj.Brand;
            txtColorName.Text = obj.COlour_NAme;
            txtQty.Text = obj.Qty;
            txtRemarks.Text = obj.Remarks;
            txtClientName.Text = "-";
            txtBrandId.Text = obj.BrandId;
            txtColorId.Text = obj.CHK_DET_Color;
            btnAdd_Click();
        }
    }
    protected void btnAdd_Click()
    {
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
        txtQR.Focus();
    }
 
    protected void btnGo_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvIndentdetails.Rows)
        {

            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {

                DataTable MovingItems = new DataTable();

                DataColumn col = new DataColumn();

                col = new DataColumn("ItemCode");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Brand");
                MovingItems.Columns.Add(col);
                col = new DataColumn("ModelNo");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Color");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Qty");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Remarks");
                MovingItems.Columns.Add(col);
                col = new DataColumn("ClientName");
                MovingItems.Columns.Add(col);
                col = new DataColumn("BrandId");
                MovingItems.Columns.Add(col);
                col = new DataColumn("ColorId");
                MovingItems.Columns.Add(col);
                col = new DataColumn("Remark");
                MovingItems.Columns.Add(col);
              
                if (gvMovingItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvMovingItems.Rows)
                    {
                        DataRow dr = MovingItems.NewRow();
                        dr["ItemCode"] = gvrow1.Cells[0].Text;
                        dr["Brand"] = gvrow1.Cells[1].Text;
                        dr["ModelNo"] = gvrow1.Cells[2].Text;
                       // dr["Indentid"] = gvrow1.Cells[5].Text;
                        dr["Color"] = gvrow1.Cells[3].Text;
                        TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                        dr["Qty"] = qty.Text;
                       // DropDownList ddlLoc = (DropDownList)gvrow1.FindControl("ddllocation");
                        //dr["Location"] = ddlMovingTo.SelectedItem.Value;
                        dr["Remarks"] = gvrow1.Cells[5].Text;
                        dr["ClientName"] = gvrow1.Cells[6].Text;
                        dr["BrandId"] = gvrow1.Cells[7].Text;
                        dr["ColorId"] = gvrow1.Cells[8].Text;
                        TextBox remark = (TextBox)gvrow1.FindControl("txtRemarks");
                        dr["Remark"] = remark.Text;


                        MovingItems.Rows.Add(dr);
                    }
                }

                if (gvMovingItems.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow1 in gvMovingItems.Rows)
                    {
                        if (gvrow1.Cells[0].Text == gvrow.Cells[0].Text)
                        {
                            gvMovingItems.DataSource = MovingItems;
                            gvMovingItems.DataBind();
                            MessageBox.Show(this, "The  Item Name you have selected is already exists in list");
                            ch.Checked = false;
                            return;
                        }

                    }
                }

                DataRow drnew = MovingItems.NewRow();
                drnew["ItemCode"] = gvrow.Cells[0].Text;
                drnew["Brand"] = gvrow.Cells[1].Text;
                drnew["ModelNo"] = gvrow.Cells[2].Text;
                drnew["Color"] = gvrow.Cells[3].Text;
                drnew["Qty"] = gvrow.Cells[4].Text;
                //drnew["Location"] = gvrow.Cells[7].Text;
                drnew["Remarks"] = gvrow.Cells[5].Text;
                drnew["ClientName"] = gvrow.Cells[6].Text;
                drnew["BrandId"] = gvrow.Cells[7].Text;
                drnew["ColorId"] = gvrow.Cells[8].Text;
                drnew["Remark"] = gvrow.Cells[9].Text;

                MovingItems.Rows.Add(drnew);
                gvMovingItems.DataSource = MovingItems;
                gvMovingItems.DataBind();
                ch.Checked = false;
            }
        }

    }
    protected void gvMovingItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

           // DropDownList ddlLoc = (DropDownList)e.Row.FindControl("ddllocation");
            lblCompany.Text = cp.getPresentCompanySessionValue();
            //SM.DDLBindWithSelect(ddlLoc, "  select [wh_id],[whname] from [warehouse_tbl] as a inner join [YANTRA_COMP_PROFILE] as b on a.locid=b.locid where b.CP_ID=" + lblCompany.Text + " ");
            //Inventory.Internalindent.Godown_Select(ddlLoc);         

           // ddlLoc.DataBind();
          
                e.Row.Cells[10].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
         
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            //  e.Row.Cells[0].Visible = false;
        }
    
    }
    private void StockMovementSave()
    {
        if (gvMovingItems.Rows.Count > 0)
        {
            try
            {
                Inventory.StockMovement obj = new Inventory.StockMovement();
                SCM.BeginTransaction();

                obj.IntIndId = ddlIndentno.SelectedValue;
                obj.MovingFrom = ddlMovingFrom.SelectedValue;
                obj.MovingDcDate =Yantra.Classes.General.toMMDDYYYY(txtmovingdate.Text);
                obj.CpId = cp.getPresentCompanySessionValue();
                obj.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                obj.Status = ddlStatus.SelectedItem.Text;
                obj.VechileNo = txtVechileNo.Text;
                obj.Movingto = ddlMovingTo.SelectedItem.Value;
                obj.DCNo = txtDCNo.Text;
                if (obj.StockMovementMaster_Save() == "Data Saved Successfully")
                {
                    // obj.SuppliersQuotationDetails_Delete(obj.SmdcId)
                    foreach (GridViewRow gvrow in gvMovingItems.Rows)
                    {
                        obj.Itemcode = gvrow.Cells[0].Text;
                        obj.Brandid = gvrow.Cells[7].Text;
                        obj.colorid = gvrow.Cells[8].Text;
                        TextBox qty=(TextBox)gvrow.FindControl("txtQuantity");
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
                internalDCtbl.Visible = false;
                
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }

    private void StockMovementUpdate()
    {
        if (gvMovingItems.Rows.Count > 0)
        {
            try
            {
                Inventory.StockMovement obj = new Inventory.StockMovement();
                SCM.BeginTransaction();
                obj.SmdcId = lblDCId.Text;
                obj.IntIndId = ddlIndentno.SelectedValue;
                obj.MovingFrom = ddlMovingFrom.SelectedValue;
                obj.MovingDcDate = Yantra.Classes.General.toMMDDYYYY(txtmovingdate.Text);
                obj.CpId = cp.getPresentCompanySessionValue();
                obj.PreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                obj.Status = ddlStatus.SelectedItem.Text;
                obj.VechileNo = txtVechileNo.Text;
                obj.Movingto = ddlMovingTo.SelectedItem.Value;
               // obj.DCNo = txtDCNo.Text;
                if (obj.InternalDC_Update() == "Data Updated Successfully")
                {
                    //obj.InternalDCDetails_Delete(obj.SmdcId);
                    foreach (GridViewRow gvrow in gvMovingItems.Rows)
                    {
                        obj.Itemcode = gvrow.Cells[0].Text;
                        obj.Brandid = gvrow.Cells[7].Text;
                        obj.colorid = gvrow.Cells[8].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        obj.Quantity = qty.Text;
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
                internalDCtbl.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            StockMovementSave();
           // gvInternalDCDtls.DataBind();
            gvIndentdetails.DataSource = null;
            gvIndentdetails.DataBind();
            gvMovingItems.DataSource = null;
            gvMovingItems.DataBind();
            gvInternalDCDtls.DataBind();
          //  Response.Redirect("StockMovement.aspx");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
 "alert(' Moving DC Saved sucessfully');window.location ='StockMovement.aspx';", true);
        }
        else if (btnSave.Text == "Update")
        {
            StockMovementUpdate();
           // gvInternalDCDtls.DataBind();
            gvIndentdetails.DataSource = null;
            gvIndentdetails.DataBind();
            gvMovingItems.DataSource = null;
            gvMovingItems.DataBind();
            gvInternalDCDtls.DataBind();
           // Response.Redirect("StockMovement.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
"alert(' Moving DC Updated sucessfully');window.location ='StockMovement.aspx';", true);
        }
    }
    protected void gvIndentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[7].Visible = false;
            //  e.Row.Cells[0].Visible = false;
        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        txtDCNo.Text = Inventory.StockMovement.InternalDC_AutoGenCode();
        txtmovingdate.Text = Yantra.Classes.General.toDDMMYYYY(DateTime.Now.ToString());
        internalDCtbl.Visible = true;
        btnSave.Text = "Save";
        ClearControls();
    }
    private void ClearControls()
    {
        txtIndentdate.Text = string.Empty;
        txtVechileNo.Text = string.Empty;
        ddlIndentno.SelectedIndex = 0;
        ddlMovingFrom.SelectedIndex = 0;
        ddlMovingTo.SelectedIndex = 0;
        gvIndentdetails.DataSource = null;
        gvIndentdetails.DataBind();
        gvDCItems.DataSource = null;
        gvDCItems.DataBind();
        gvMovingItems.DataSource = null;
        gvMovingItems.DataBind();
        ddlStatus.SelectedIndex = 0;
    }
    protected void lbtnDCNo_Click(object sender, EventArgs e)
    {
        internalDCtbl.Visible = false;
        LinkButton lbtnIntDcNo;
        lbtnIntDcNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIntDcNo.Parent.Parent;
        gvInternalDCDtls.SelectedIndex = gvRow.RowIndex;
        lblDCId.Text = gvInternalDCDtls.SelectedRow.Cells[0].Text;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    protected void gvInternalDCDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;           
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        internalDCtbl.Visible = true;
        btnSave.Text = "Update";
        if (gvInternalDCDtls.SelectedIndex > -1)
        {
            try
            {
                Inventory.StockMovement obj = new Inventory.StockMovement();
                if (obj.InternalDC_Select(lblDCId.Text) > 0)
                    {
                        txtDCNo.Text = obj.DCNo;
                        txtmovingdate.Text = obj.MovingDcDate;
                        txtVechileNo.Text = obj.VechileNo;
                        ddlIndentno.SelectedValue = obj.IntIndId;
                        //ddlIndentno.DataBind();
                        ddlIndentno_SelectedIndexChanged(sender,e);
                        ddlMovingTo.SelectedValue = obj.Movingto;
                        ddlMovingFrom.SelectedValue = obj.MovingFrom;
                       // ddlMovingTo.DataBind();
                        //ddlMovingFrom.SelectedValue = obj.MovingFrom;
                       // ddlStatus.SelectedItem.Text = obj.Status;
                        if (obj.Status == "Open")
                        {
                            ddlStatus.SelectedIndex =1;
                        }
                        else if (obj.Status == "Close")
                        {
                            ddlStatus.SelectedIndex = 2;
                        }
                        obj.InternalDcDetails_Select(lblDCId.Text, gvDCItems);
                    }

            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvInternalDCDtls.SelectedIndex > -1)
        {
            try
            {
                Inventory.StockMovement obj = new Inventory.StockMovement();
                obj.InternalDC_Delete(lblDCId.Text);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvInternalDCDtls.DataBind();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        internalDCtbl.Visible = false;
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Moving Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchText.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
           
    }
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;

            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvInternalDCDtls.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ddlSearchBy.SelectedItem.Text == "Moving Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvInternalDCDtls.DataBind();
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvInternalDCDtls.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvInternalDCDtls.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvInternalDCDtls.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (gvInternalDCDtls.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=MovingDc&dcid=" + gvInternalDCDtls.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void gvDCItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            e.Row.Cells[11].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDetId;
        lbtnDetId = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDetId.Parent.Parent;
        gvDCItems.SelectedIndex = gvRow.RowIndex;
        Inventory.Delivery objInventory = new Inventory.Delivery();
        objInventory.MovingDC_Delete(gvDCItems.SelectedRow.Cells[10].Text);
        Inventory.StockMovement obj = new Inventory.StockMovement();
        obj.InternalDcDetails_Select(lblDCId.Text, gvDCItems);
       
    }
    protected void gvMovingItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvMovingItems.Rows[e.RowIndex].Cells[0].Text;
        DataTable MovingItems = new DataTable();

        DataColumn col = new DataColumn();

        col = new DataColumn("ItemCode");
        MovingItems.Columns.Add(col);
        col = new DataColumn("Brand");
        MovingItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        MovingItems.Columns.Add(col);
        col = new DataColumn("Color");
        MovingItems.Columns.Add(col);
        col = new DataColumn("Qty");
        MovingItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        MovingItems.Columns.Add(col);
        col = new DataColumn("ClientName");
        MovingItems.Columns.Add(col);
        col = new DataColumn("BrandId");
        MovingItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        MovingItems.Columns.Add(col);
        col = new DataColumn("Remark");
        MovingItems.Columns.Add(col);

        if (gvMovingItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow1 in gvMovingItems.Rows)
            {
                if (gvrow1.RowIndex != e.RowIndex)
                {
                    DataRow dr = MovingItems.NewRow();
                    dr["ItemCode"] = gvrow1.Cells[0].Text;
                    dr["Brand"] = gvrow1.Cells[1].Text;
                    dr["ModelNo"] = gvrow1.Cells[2].Text;
                    // dr["Indentid"] = gvrow1.Cells[5].Text;
                    dr["Color"] = gvrow1.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow1.FindControl("txtQuantity");
                    dr["Qty"] = qty.Text;
                    // DropDownList ddlLoc = (DropDownList)gvrow1.FindControl("ddllocation");
                    //dr["Location"] = ddlMovingTo.SelectedItem.Value;
                    dr["Remarks"] = gvrow1.Cells[5].Text;
                    dr["ClientName"] = gvrow1.Cells[6].Text;
                    dr["BrandId"] = gvrow1.Cells[7].Text;
                    dr["ColorId"] = gvrow1.Cells[8].Text;
                    TextBox remark = (TextBox)gvrow1.FindControl("txtRemarks");
                    dr["Remark"] = remark.Text;
                    MovingItems.Rows.Add(dr);
                }
            }
        }
        gvMovingItems.DataSource = MovingItems;
        gvMovingItems.DataBind();
    }
}
 
