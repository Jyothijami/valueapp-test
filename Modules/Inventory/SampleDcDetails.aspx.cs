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
using vllib;
using System.Data.SqlClient;
public partial class Modules_Inventory_SampleDcDetails : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // Customer_fil();
        //btnEdit.Enabled = false;
        btnRevise.Enabled = false;
        if (!IsPostBack)
        {
            setControlsVisibility();

            lblCPID.Text = cp.getPresentCompanySessionValue();
            DespatchMode_Fill();
            Trans_Fill();
            EmployeeMaster_Fill();
            tblDCDetails.Visible = false;
            Customer_fil();

            Masters.CompanyProfile.Company_Select(ddlCompany);
            btnForApproveHidden.Style.Add("display", "none");
            Masters.CompanyProfile.Company_Select(ddlCompany1);
            SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY order by PRODUCT_COMPANY_NAME asc ");

            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (Request.QueryString["DCId"] != null)
           {

               try
               {
                   Inventory.Delivery objDelivery = new Inventory.Delivery();

                   if (objDelivery.Delivery_Select(Request.QueryString["DCId"].ToString()) > 0)
                   {

                       btnSave.Enabled = true;
                       btnSave.Text = "Update";
                       if (objDelivery.DCFor == "Sample")
                       {
                           rdbSample.Checked = true;
                           ddlCustomerName.SelectedValue = objDelivery.DcCustomerid;
                           ddlCustomerName_SelectedIndexChanged(sender, e);
                           ddlUnitName.SelectedValue = objDelivery.UnitId;
                           ddlUnitName_SelectedIndexChanged(sender, e);
                       }
                       else if (objDelivery.DCFor == "Cash")
                       {
                           rdbCash.Checked = true;
                           ddlCustomerName.SelectedValue = objDelivery.DcCustomerid;
                           ddlCustomerName_SelectedIndexChanged(sender, e);
                           ddlUnitName.SelectedValue = objDelivery.UnitId;
                           ddlUnitName_SelectedIndexChanged(sender, e);
                       }
                       tblDCDetails.Visible = true;
                       txtDeliveryChallanNo.Text = objDelivery.DCNo;
                       txtDeliveryChallanDate.Text = objDelivery.DCDate;
                       ddlTransPorterName.SelectedValue = objDelivery.TransId;
                       txtLRNo.Text = objDelivery.DCLrNo;
                       txtLRDate.Text = objDelivery.DCLrDate;
                       ddlPreparedBy.SelectedValue = objDelivery.DCPreparedBy;
                       ddlApprovedBy.SelectedValue = objDelivery.DCApprovedBy;
                       ddlDCType.SelectedValue = objDelivery.DCType;
                       ddlDCType_SelectedIndexChanged(sender, e);
                       txtInwardDate.Text = objDelivery.DCInwardDate;
                       ddlDespatchMode.SelectedValue = objDelivery.DespmId;
                       ddlCompany1.SelectedValue = objDelivery.Company;
                       txtRevisedFrom.Text = objDelivery.RevisedKey;
                       if (txtRevisedFrom.Text != "")
                       {
                           lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
                       }
                       else
                       {
                           lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                       }
                   }
               }
               catch (Exception ex)
               {
                   MessageBox.Show(this, ex.Message.ToString());
               }
               finally
               {
                   Inventory.Dispose();
                   // ddlSalesOrderNo_SelectedIndexChanged(sender, e);
               }
                
                try
                {
                    Inventory.Delivery objDelivery = new Inventory.Delivery();
                    if (objDelivery.Delivery_Select(Request.QueryString["DCId"].ToString()) > 0)
                    {
                        //btnSave.Enabled = false;
                        //btnSave.Text = "Update";
                        if (objDelivery.DCFor == "Sample")
                        {
                            rdbSample.Checked = true;
                            rdbCash.Checked = false;
                            Customer_fil();
                            ddlCustomerName.SelectedValue = objDelivery.DcCustomerid;
                            ddlCustomerName_SelectedIndexChanged(sender, e);

                            if (objDelivery.UnitId != "")
                            {

                                ddlUnitName.SelectedValue = objDelivery.UnitId;
                                ddlUnitName_SelectedIndexChanged(sender, e);
                            }

                        }
                        else if (objDelivery.DCFor == "Cash")
                        {
                            rdbCash.Checked = true;
                            rdbSample.Checked = false;
                            Customer_fil();
                            ddlCustomerName.SelectedValue = objDelivery.DcCustomerid;
                            ddlCustomerName_SelectedIndexChanged(sender, e);
                            if (objDelivery.UnitId != "")
                            {

                                ddlUnitName.SelectedValue = objDelivery.UnitId;
                                ddlUnitName_SelectedIndexChanged(sender, e);
                            }
                        }
               
                        tblDCDetails.Visible = true;
                        txtDeliveryChallanNo.Text = objDelivery.DCNo;
                        txtDeliveryChallanDate.Text = objDelivery.DCDate;
                        ddlTransPorterName.SelectedValue = objDelivery.TransId;
                        txtLRNo.Text = objDelivery.DCLrNo;
                        txtLRDate.Text = objDelivery.DCLrDate;
                        ddlDCType.SelectedValue = objDelivery.DCType;
                        ddlDCType_SelectedIndexChanged(sender, e);
                        ddlCompany1.SelectedValue = objDelivery.Company;
                        ddlPreparedBy.SelectedValue = objDelivery.DCPreparedBy;
                        ddlApprovedBy.SelectedValue = objDelivery.DCApprovedBy;
                        txtInwardDate.Text = objDelivery.DCInwardDate;
                        ddlDespatchMode.SelectedValue = objDelivery.DespmId;
                        txtRevisedFrom.Text = objDelivery.RevisedKey;
                        if (txtRevisedFrom.Text != "")
                        {
                            lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
                        }
                        else
                        {
                            lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Inventory.RollBackTransaction();
                    MessageBox.Show(this, ex.Message.ToString());
                }
                finally
                {
                    Inventory.Dispose();
                }
           }
            else
            {

            }
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "78");
        
        //btnAssign.Enabled = up.Assign;
        //btnAssignTask.Enabled = up.AssignTask;
        //btnCloseAssignTask.Enabled = up.CloseAssignTask;
        //Button1.Enabled = up.Go;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        btnSave.Enabled = up.add;
        //btnRevise.Enabled = up.Revise;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        btnPrint.Enabled = up.Print;
    }

    #region Despatch Mode Fill
    private void DespatchMode_Fill()
    {
        try
        {
            Masters.DespatchMode.DespatchMode_Select(ddlDespatchMode);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Trans Fill
    private void Trans_Fill()
    {
        try
        {
            Masters.TrasnporterMaster.TransporterMaster_Select(ddlTransPorterName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Employee Master Fill
    private void EmployeeMaster_Fill()
    {
        try
        {

            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlApprovedBy);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpNameForAssign);
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

   
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToShortDateString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnRefresh.Enabled = false;
        btnRevise.Enabled = false;
        DeliverySampleSave();
        Response.Redirect("SampleDc.aspx");
    }

    #region DeliverySampleSave
    private void DeliverySampleSave()
    {
        if (gvItmDetails.Rows.Count > 0)
        {
            try
            {
                Inventory.Delivery objInventory = new Inventory.Delivery();
                Inventory.BeginTransaction();
                objInventory.DCNo = txtDeliveryChallanNo.Text;
                objInventory.DCDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryChallanDate.Text);
                if (rdbSample.Checked)
                {
                    objInventory.DCFor = rdbSample.Text;
                    objInventory.SOId = "0";
                    objInventory.SPOId = "0";
                }
                if (rdbCash.Checked)
                {
                    objInventory.DCFor = rdbCash.Text;
                    objInventory.SOId = "0";
                    objInventory.SPOId = "0";
                }

                objInventory.DCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objInventory.DCApprovedBy = ddlApprovedBy.SelectedItem.Value;
                objInventory.TransId = ddlTransPorterName.SelectedItem.Value;
                objInventory.DCLrNo = txtLRNo.Text;
                objInventory.DCLrDate = Yantra.Classes.General.toMMDDYYYY(txtLRDate.Text);
                objInventory.DCType = ddlDCType.SelectedItem.Value;
                objInventory.DCCSTNo = "0";
                objInventory.DCTINNo = "0";
                objInventory.DCInwardDate = Yantra.Classes.General.toMMDDYYYY(txtInwardDate.Text);
                objInventory.DespmId = ddlDespatchMode.SelectedItem.Value;
                objInventory.Cp_Id = lblCPID.Text;
                objInventory.STATUS = "OPEN";
                objInventory.Company = ddlCompany1.SelectedItem.Value;
                objInventory.DcCustomerid = ddlCustomerName.SelectedItem.Value;
                objInventory.UnitId = ddlUnitName.SelectedItem.Value;


                if (objInventory.Delivery_Save() == "Data Saved Successfully")
                {
                    objInventory.DeliveryDetails_Delete(objInventory.DCId);
                    foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    {
                        objInventory.ItemCode = gvrow.Cells[2].Text;
                        objInventory.DCDetQty = gvrow.Cells[6].Text;
                        objInventory.DCDetSerialNo = gvrow.Cells[8].Text;
                        objInventory.COLORID = gvrow.Cells[11].Text;
                        objInventory.GODOWNID = gvrow.Cells[12].Text;
                        objInventory.DETSTATUS = gvrow.Cells[15].Text;

                        objInventory.DetCompany = gvrow.Cells[17].Text;
                        objInventory.ITemremarks = gvrow.Cells[18].Text;
                        objInventory.DCfor = "0";
                        objInventory.Remarks2 = gvrow.Cells[19].Text;
                        objInventory.DeliveryDetails_Save();


                        string Itemcode = gvrow.Cells[2].Text;
                        SqlCommand cmd = new SqlCommand("select  Item_ID,whLocId from dbo.INWARD where ITEM_CODE=" + Itemcode + " and Item_ID not in(select Item_ID from dbo.OUTWARD where ITEM_CODE=" + Itemcode + ")", con);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Masters.ItemPurchase objout = new Masters.ItemPurchase();
                        int qty = int.Parse(gvrow.Cells[6].Text);

                        for (int i = 0; i < qty; i++)
                        {
                            objout.itemcode = gvrow.Cells[2].Text;
                            objout.ItemID = dt.Rows[i][0].ToString();
                            objout.locationid = dt.Rows[i][1].ToString();
                            objout.Barcode = dt.Rows[i][0].ToString();
                            objout.companyid = lblCPID.Text;
                            objout.DCID = txtDeliveryChallanNo.Text;
                            objout.COLORID = gvrow.Cells[11].Text;
                            objout.Outward_Save();
                        }   

                    }
                    //foreach (GridViewRow gvrow in gvItmDetails.Rows)
                    //{
                    //    objInventory.iqitemcode = gvrow.Cells[2].Text;
                    //    objInventory.iqcpid = gvrow.Cells[17].Text;
                    //    objInventory.iqgodownid = gvrow.Cells[12].Text;
                    //    objInventory.iqcolorid = gvrow.Cells[11].Text;
                    //    objInventory.iqitemqtyinhand = gvrow.Cells[6].Text;
                    //    objInventory.iqresqty = gvrow.Cells[14].Text;
                    //    objInventory.SampleItemqty_Update();

                    //}
                    Inventory.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");


                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }

            catch (Exception ex)
            {
                Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

                //gvDeliveryChallanDetails.DataBind();
                gvItmDetails.DataBind();
                //gvDeliveryChallanDetails.SelectedIndex = -1;
                tblDCDetails.Visible = false;
                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }

    }
    #endregion

    protected void btnRevise_Click(object sender, EventArgs e)
    {

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Inventory.ClearControls(this);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleDc.aspx");
        //gvDeliveryChallanDetails.SelectedIndex = -1;
        tblDCDetails.Visible = false;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRefresh.Enabled = false;
        if (Request.QueryString["DCId"] != null)
        {
            tblpRint.Visible = true;
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void chkOriginal_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanSample&dcid=" + Request.QueryString["DCId"].ToString() + " ";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chkDuplicate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanSample&dcid=" + Request.QueryString["DCId"].ToString() + " ";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void chktriplicate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanSample&dcid=" + Request.QueryString["DCId"].ToString() + " ";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnSave.Visible == false && btnRevise.Text == "Revise")
        {
            MessageBox.Show(this, "No More Items Can Be Added In this Revise Delivery Challan. Issue Another Delivery Challan For Other Items");
            return;
        }
        if (txtQtyInHand.Text == "")
        {
            MessageBox.Show(this, "We are not Having the Item in Stock");
            return;
        }

        if (txtItemQuantity.Text == "0")
        {
            MessageBox.Show(this, "Item Quantity Should Be More Than Zero");
            return;
        }

        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Location");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Color");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("GodownId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Inhand");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("resqty");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Status");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("StatusId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Companyid");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks2");
        DeliveryItems.Columns.Add(col);

        string str = "";
        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvItmDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvItmDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = DeliveryItems.NewRow();
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemName"] = txtItemName.Text;
                        dr["UOM"] = txtItemUOM.Text;
                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["SerialNo"] = txtSerialNo.Text;
                        dr["Location"] = ddllocation.SelectedItem.Text;
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["GodownId"] = ddllocation.SelectedItem.Value;
                        if (txtInhand.Text != str)
                            dr["Inhand"] = txtInhand.Text;
                        else
                            dr["Inhand"] = "0";
                        if (txtresqty.Text != str)
                            dr["resqty"] = txtresqty.Text;
                        else
                            dr["resqty"] = "0";
                        dr["Status"] = ddlStatus.SelectedItem.Text;
                        dr["StatusID"] = ddlStatus.SelectedItem.Value;
                        dr["Companyid"] = ddlCompany.SelectedItem.Value;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["Remarks2"] = txtremarks2.Text;
                        DeliveryItems.Rows.Add(dr);
                    }
                    else
                    {

                        DataRow dr = DeliveryItems.NewRow();
                        //dr["ItemCode"] = gvrow.Cells[2].Text;
                        //dr["ItemName"] = gvrow.Cells[3].Text;
                        //dr["UOM"] = gvrow.Cells[4].Text;
                        //dr["Quantity"] = gvrow.Cells[5].Text;

                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["UOM"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;

                        dr["ItemStatus"] = gvrow.Cells[7].Text;
                        dr["SerialNo"] = gvrow.Cells[8].Text;
                        dr["Location"] = gvrow.Cells[9].Text;

                        dr["Color"] = gvrow.Cells[10].Text;
                        dr["ColorId"] = gvrow.Cells[11].Text;

                        dr["GodownId"] = gvrow.Cells[12].Text;
                        dr["Inhand"] = gvrow.Cells[13].Text;
                        dr["resqty"] = gvrow.Cells[14].Text;
                        dr["Status"] = gvrow.Cells[15].Text;
                        dr["StatusId"] = gvrow.Cells[16].Text;
                        dr["Companyid"] = gvrow.Cells[17].Text;
                        dr["Remarks"] = gvrow.Cells[18].Text;
                        dr["Remarks2"] = gvrow.Cells[19].Text;
                        DeliveryItems.Rows.Add(dr);

                    }
                }
                else
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;

                    dr["ItemStatus"] = gvrow.Cells[7].Text;
                    dr["SerialNo"] = gvrow.Cells[8].Text;
                    dr["Location"] = gvrow.Cells[9].Text;
                    dr["Color"] = gvrow.Cells[10].Text;
                    dr["ColorId"] = gvrow.Cells[11].Text;
                    dr["GodownId"] = gvrow.Cells[12].Text;
                    dr["Inhand"] = gvrow.Cells[13].Text;
                    dr["resqty"] = gvrow.Cells[14].Text;
                    dr["Status"] = gvrow.Cells[15].Text;
                    dr["StatusId"] = gvrow.Cells[16].Text;
                    dr["Companyid"] = gvrow.Cells[17].Text;
                    dr["Remarks"] = gvrow.Cells[18].Text;
                    dr["Remarks2"] = gvrow.Cells[19].Text;
                    DeliveryItems.Rows.Add(dr);

                }

            }
        }

        if (gvItmDetails.Rows.Count > 0)
        {
            if (gvItmDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvItmDetails.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlModelNo.SelectedItem.Value && gvrow.Cells[11].Text == ddlColor.SelectedItem.Value)
                    {
                        gvItmDetails.DataSource = DeliveryItems;
                        gvItmDetails.DataBind();
                        MessageBox.Show(this, "The Item Code and Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvItmDetails.SelectedIndex == -1)
        {
            DataRow drnew = DeliveryItems.NewRow();
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemName"] = txtItemName.Text;
            drnew["UOM"] = txtItemUOM.Text;
            drnew["Quantity"] = txtItemQuantity.Text;

            drnew["SerialNo"] = txtSerialNo.Text;
            drnew["Location"] = ddllocation.SelectedItem.Text;
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["GodownId"] = ddllocation.SelectedItem.Value;
            if (txtInhand.Text != str)
                drnew["Inhand"] = txtInhand.Text;
            else
                drnew["Inhand"] = "0";
            if (txtresqty.Text != str)
                drnew["resqty"] = txtresqty.Text;
            else
                drnew["resqty"] = "0";
            drnew["Status"] = ddlStatus.SelectedItem.Text;
            drnew["StatusId"] = ddlStatus.SelectedItem.Value;
            drnew["Companyid"] = ddlCompany.SelectedItem.Value;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["Remarks2"] = txtremarks2.Text;
            DeliveryItems.Rows.Add(drnew);
        }
        gvItmDetails.DataSource = DeliveryItems;
        gvItmDetails.DataBind();

        gvItmDetails.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);




    }
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        //ddlModelNo.SelectedValue = "0";
        txtItemName.Text = string.Empty;
        txtItemCategory.Text = string.Empty;
        txtItemSubCategory.Text = string.Empty;
        txtItemUOM.Text = string.Empty;
        //txtColor.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtItemQuantity.Text = string.Empty;
        txtOrderedQty.Text = string.Empty;
        txtSerialNo.Text = string.Empty;
        txtBalanceQty.Text = txtBalanceQtyHidden.Text = string.Empty;
        ddllocation.SelectedValue = "0";
        gvItmDetails.SelectedIndex = -1;
        ddlColor.SelectedValue = "0";
        txtInhand.Text = string.Empty;
        txtresqty.Text = string.Empty;
        // ddlessential.SelectedValue = "0";
        txtQtyInHand.Text = string.Empty;
        ddlCompany.SelectedValue = "0";
        txtRemarks.Text = "";
        txtDescription.Text = "";
        txtremarks2.Text = "";
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value + " order by  YANTRA_ITEM_MAST.ITEM_MODEL_NO ");
    }

    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Godown_Fill();
            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtItemCategory.Text = objMaster.ItemCategoryName;
                txtItemName.Text = objMaster.ItemName;
                txtItemUOM.Text = objMaster.ItemUOMShort;
                txtDescription.Text = objMaster.ItemSpec;
                txtBrand.Text = objMaster.BrandProductName;
                txtItemSubCategory.Text = objMaster.ItemType;

            }
            if (objMaster.StockEntry_select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtQtyInHand.Text = objMaster.itemquantity;
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }

    #region Godown Name Fill
    private void Godown_Fill()
    {
        try
        {
            Masters.ItemMaster.Stockentry(ddllocation, ddlModelNo.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster objMaster = new Masters.ItemMaster();
        if (objMaster.StockEntrychange_select(ddlModelNo.SelectedItem.Value, ddllocation.SelectedItem.Value, ddlColor.SelectedValue) > 0)
        {

            txtQtyInHand.Text = objMaster.itemquantity;

        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQtyInHand.Text = "";
        Masters.ItemMaster.Stockentry1(ddllocation, ddlModelNo.SelectedItem.Value, ddlCompany.SelectedItem.Value);
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
        btnRevise.Attributes.Clear();
        Inventory.ClearControls(this);
        txtDeliveryChallanNo.Text = Inventory.Delivery.Delivery_AutoGenCode();
        txtCustOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnRevise.Text = "Modify";
        btnSave.Text = "Save";
        Customer_fil();
        btnSave.Enabled = true;
        tblDCDetails.Visible = true;
        //gvDeliveryChallanDetails.SelectedIndex = -1;
        gvItmDetails.DataBind();
        gvDeliveryChallanItems.DataBind();
    }
    protected void gvItmDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvItmDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable DeliveryItems = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ModelNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemName");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("UOM");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Quantity");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ItemStatus");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("SerialNo");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Location");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Color");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("ColorId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("GodownId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Inhand");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("resqty");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Status");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("StatusId");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Companyid");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks");
        DeliveryItems.Columns.Add(col);
        col = new DataColumn("Remarks2");
        DeliveryItems.Columns.Add(col);
        if (gvItmDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvItmDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = DeliveryItems.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ItemName"] = gvrow.Cells[3].Text;
                    dr["UOM"] = gvrow.Cells[4].Text;
                    dr["Quantity"] = gvrow.Cells[5].Text;

                    //   dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["UOM"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;

                    dr["ItemStatus"] = gvrow.Cells[7].Text;
                    dr["SerialNo"] = gvrow.Cells[8].Text;
                    dr["Location"] = gvrow.Cells[9].Text;

                    dr["Color"] = gvrow.Cells[10].Text;
                    dr["ColorId"] = gvrow.Cells[11].Text;

                    dr["GodownId"] = gvrow.Cells[12].Text;
                    dr["Inhand"] = gvrow.Cells[13].Text;
                    dr["resqty"] = gvrow.Cells[14].Text;
                    dr["Status"] = gvrow.Cells[15].Text;
                    dr["StatusId"] = gvrow.Cells[16].Text;
                    dr["Companyid"] = gvrow.Cells[17].Text;
                    dr["Remarks"] = gvrow.Cells[18].Text;
                    dr["Remarks2"] = gvrow.Cells[19].Text;
                    DeliveryItems.Rows.Add(dr);

                }
            }
        }
        gvItmDetails.DataSource = DeliveryItems;
        gvItmDetails.DataBind();
    }
    protected void ddlDCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDCType.SelectedValue == "Returnable")
        {
            lblInwardDate.Visible = lblInwardDateValInd.Visible  = txtInwardDate.Visible = true;
            rfvInwardDate.Enabled = custValInwardDate.Enabled = true;
        }
        else if (ddlDCType.SelectedValue == "Non Returnable")
        {
            lblInwardDate.Visible = lblInwardDateValInd.Visible = txtInwardDate.Visible = false;
            rfvInwardDate.Enabled = custValInwardDate.EnableClientScript = false;
        }
    }
    protected void gvItmDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Interested Products list?');");
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[17].Visible = false;
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DCId"] != null)
        {
            try
            {
                Inventory.Delivery objDelivery = new Inventory.Delivery();

                if (objDelivery.Delivery_Select(Request.QueryString["DCId"].ToString()) > 0)
                {

                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    if (objDelivery.DCFor == "Sample")
                    {
                        rdbSample.Checked = true;
                        ddlCustomerName.SelectedValue = objDelivery.DcCustomerid;
                        ddlCustomerName_SelectedIndexChanged(sender, e);
                        ddlUnitName.SelectedValue = objDelivery.UnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                    }
                    else if (objDelivery.DCFor == "Cash")
                    {
                        rdbCash.Checked = true;
                        ddlCustomerName.SelectedValue = objDelivery.DcCustomerid;
                        ddlCustomerName_SelectedIndexChanged(sender, e);
                        ddlUnitName.SelectedValue = objDelivery.UnitId;
                        ddlUnitName_SelectedIndexChanged(sender, e);
                    }
                    tblDCDetails.Visible = true;
                    txtDeliveryChallanNo.Text = objDelivery.DCNo;
                    txtDeliveryChallanDate.Text = objDelivery.DCDate;
                    ddlTransPorterName.SelectedValue = objDelivery.TransId;
                    txtLRNo.Text = objDelivery.DCLrNo;
                    txtLRDate.Text = objDelivery.DCLrDate;
                    ddlPreparedBy.SelectedValue = objDelivery.DCPreparedBy;
                    ddlApprovedBy.SelectedValue = objDelivery.DCApprovedBy;
                    ddlDCType.SelectedValue = objDelivery.DCType;
                    ddlDCType_SelectedIndexChanged(sender, e);
                    txtInwardDate.Text = objDelivery.DCInwardDate;
                    ddlDespatchMode.SelectedValue = objDelivery.DespmId;
                    ddlCompany1.SelectedValue = objDelivery.Company;
                    txtRevisedFrom.Text = objDelivery.RevisedKey;
                    if (txtRevisedFrom.Text != "")
                    {
                        lblRevisedFrom.Visible = txtRevisedFrom.Visible = true;
                    }
                    else
                    {
                        lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Inventory.Dispose();
                // ddlSalesOrderNo_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DCId"] != null)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                MessageBox.Show(this, objSM.SampleDc_Delete(Request.QueryString["DCId"].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                //btnDelete.Attributes.Clear();
                //gvDeliveryChallanDetails.DataBind();
                //SM.ClearControls(this);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void rdbSample_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if (objSMCustomer.CustomerMaster_Select(ddlCustomerName.SelectedItem.Value) > 0)
        {
            txtCustomerName.Text = objSMCustomer.CustName;
            txtAddress.Text = objSMCustomer.Address;
            txtEmail.Text = objSMCustomer.Email;
            txtRegion.Text = objSMCustomer.RegName;
            txtPhone.Text = objSMCustomer.Phone;
            txtMobile.Text = objSMCustomer.Mobile;
        }
        SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomerName.SelectedItem.Value);
        Inventory.Delivery objDC = new Inventory.Delivery();
        objDC.DeliveryDetailsByCustIdSample_Select(ddlCustomerName.SelectedItem.Value, gvDeliveryChallanItems);
    }

    #region Customer FIl

    private void Customer_fil()
    {
        SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomerName);
    }
    #endregion

   
    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
        ddlModelNo_SelectedIndexChanged(sender, e);
    }

    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlColor.SelectedValue = "0";
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource3";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            ddlCustomerName_SelectedIndexChanged(sender, e);
        }
        else
        {
            MessageBox.Show(this, "Please Enter Text to Search");
        }
    }


    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
        if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
        {

            txtunitaddress.Text = objSMCustomer.CustUnitAddress;

        }
    }
    
}
 
