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
public partial class Modules_Inventory_SampleDc : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();

       // Customer_fil();
        //btnEdit.Enabled = false;
        btnRevise.Enabled = false;
        if (!IsPostBack)
        {
            lblCPID.Text = cp.getPresentCompanySessionValue();
            DespatchMode_Fill();
            Trans_Fill();
            EmployeeMaster_Fill();
            tblDCDetails.Visible = false;
            //tblButtons.Visible = false;

            Customer_fil();
            lblCompany.Text = cp.getPresentCompanySessionValue();

          
            Masters.CompanyProfile.Company_Select(ddlCompany);
            btnForApproveHidden.Style.Add("display", "none");
            Masters.CompanyProfile.Company_Select(ddlCompany1);
            SM.DDLBindWithSelect(ddlBrand, "select PRODUCT_COMPANY_ID,PRODUCT_COMPANY_NAME from YANTRA_LKUP_PRODUCT_COMPANY order by PRODUCT_COMPANY_NAME asc ");

            string user = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);

            if (user == "0")
            {
                btnDelete.Visible = true;
                btnEdit.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                btnEdit.Visible = false;
            }
        
        }
    }
    #endregion
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "78");
        btnNew.Enabled = up.add;
        btnReleaseBlock.Enabled = up.Email;
        //btnEdit.Enabled = up.Update;
        //btnDelete.Enabled = up.Delete;
        //btnAssign.Enabled = up.Assign;
        //btnAssignTask.Enabled = up.AssignTask;
        //btnCloseAssignTask.Enabled = up.CloseAssignTask;
        //Button1.Enabled = up.Go;
        //btnAdd.Enabled = up.add;
        //btnItemRefresh.Enabled = up.ItemRefresh;
        //btnSave.Enabled = up.Save;
        //btnRevise.Enabled = up.Revise;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
        //btnPrint.Enabled = up.Print;
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

    protected void gvDeliveryChallanDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvDeliveryChallanItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[1].Text = Convert.ToDateTime(e.Row.Cells[1].Text).ToShortDateString();
            e.Row.Cells[11].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
        }
        if(btnSave.Enabled==false)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[11].Visible = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if(btnSave.Text == "Save")
        { 
        btnRefresh.Enabled = false;
        btnRevise.Enabled = false;
        DeliverySampleSave();
        }
        if(btnSave.Text=="Update")
        {
            DeliverySampleUpdate();
        }
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
                    objInventory.SOId = ddlCustomerPo.SelectedItem.Value;
                    objInventory.SPOId = "0";
                }
                if (rdbCash.Checked)
                {
                    objInventory.DCFor = rdbCash.Text;
                    objInventory.SOId = ddlCustomerPo.SelectedItem.Value;
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

                        int qty =Convert.ToInt32(gvrow.Cells[6].Text);
                        #region Outward
                        string Itemcode = gvrow.Cells[2].Text;
                        SqlCommand cmd = new SqlCommand("Usp_Get_Top_Selected_Items", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
                        cmd.Parameters.AddWithValue("@ColourId", gvrow.Cells[11].Text);
                        cmd.Parameters.AddWithValue("@LocationId", gvrow.Cells[12].Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Masters.ItemPurchase objout = new Masters.ItemPurchase();

                        if (dt.Rows.Count > 0)
                        {
							if (dt.Rows.Count >= qty)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                objout.itemcode = gvrow.Cells[2].Text;
                                objout.ItemID = dt.Rows[i][0].ToString();
                                objout.locationid = dt.Rows[i][1].ToString();
                                objout.Barcode = dt.Rows[i][0].ToString();
                               // objout.companyid = lblCPID.Text;
                                objout.companyid = dt.Rows[i][3].ToString();
                                objout.DCID = ddlCustomerPo.SelectedItem.Value;
                                //objout.COLORID = gvrow.Cells[11].Text;
                                objout.COLORID = dt.Rows[i][2].ToString();
                                objout.CustId = ddlCustomerName.SelectedItem.Value;
                                objout.Outward_Save();
                            }
						}
                        }
                        #endregion

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

                gvDeliveryChallanDetails.DataBind();
                gvItmDetails.DataBind();
                gvDeliveryChallanDetails.SelectedIndex = -1;
                tblDCDetails.Visible = false;
                //tblButtons.Visible = false;

                Inventory.ClearControls(this);
                Inventory.Dispose();
            }
        }

    }
    #endregion


    #region DC Update
    private void DeliverySampleUpdate()
    {
            try
            {
                Inventory.Delivery objInventory = new Inventory.Delivery();
               // Inventory.BeginTransaction();
                objInventory.DCId = gvDeliveryChallanDetails.SelectedRow.Cells[0].Text;
                objInventory.DCNo = txtDeliveryChallanNo.Text;
                objInventory.DCDate = Yantra.Classes.General.toMMDDYYYY(txtDeliveryChallanDate.Text);
                if (rdbSample.Checked)
                {
                    objInventory.DCFor = rdbSample.Text;
                    objInventory.SOId = ddlCustomerPo.SelectedItem.Value;
                    objInventory.SPOId = "0";
                }
                if (rdbCash.Checked)
                {
                    objInventory.DCFor = rdbCash.Text;
                    objInventory.SOId = ddlCustomerPo.SelectedItem.Value;
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

               // objInventory.Delivery_Update();

                if (objInventory.Delivery_Update() == "Data Updated Successfully")
                {
                   // objInventory.DeliveryDetails_Delete(objInventory.DCId);
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

                        int qty = Convert.ToInt32(gvrow.Cells[6].Text);
                        #region Outward
                        string Itemcode = gvrow.Cells[2].Text;
                        SqlCommand cmd = new SqlCommand("Usp_Get_Top_Selected_Items", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[2].Text);
                        cmd.Parameters.AddWithValue("@ColourId", gvrow.Cells[11].Text);
                        cmd.Parameters.AddWithValue("@LocationId", gvrow.Cells[12].Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Masters.ItemPurchase objout = new Masters.ItemPurchase();

                        if (dt.Rows.Count > 0)
                        {
							if (dt.Rows.Count >= qty)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                objout.itemcode = gvrow.Cells[2].Text;
                                objout.ItemID = dt.Rows[i][0].ToString();
                                objout.locationid = dt.Rows[i][1].ToString();
                                objout.Barcode = dt.Rows[i][0].ToString();
                                //objout.companyid = lblCPID.Text;
                                objout.companyid = dt.Rows[i][3].ToString();
                                objout.DCID = ddlCustomerPo.SelectedItem.Value;
                                //objout.COLORID = gvrow.Cells[11].Text;
                                objout.COLORID = dt.Rows[i][2].ToString();
                                objout.CustId = ddlCustomerName.SelectedItem.Value;
                                objout.Outward_Save();
                            }
						}
                        }
                        #endregion

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
                    MessageBox.Show(this, "Data Updated Successfully");


                }
                else
                {
                    Inventory.RollBackTransaction();
                }
            }

            catch (Exception ex)
            {
               // Inventory.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {

                gvDeliveryChallanDetails.DataBind();
                gvItmDetails.DataBind();
                gvDeliveryChallanDetails.SelectedIndex = -1;
                tblDCDetails.Visible = false;
                //tblButtons.Visible = false;

                Inventory.ClearControls(this);
                Inventory.Dispose();
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
        gvDeliveryChallanDetails.SelectedIndex = -1;
        tblDCDetails.Visible = false;
        //tblButtons.Visible = false;

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRefresh.Enabled = false;
        if (gvDeliveryChallanDetails.SelectedIndex > -1)
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
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanSample&dcid=" + gvDeliveryChallanDetails.SelectedRow.Cells[0].Text + " ";
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
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanSample&dcid=" + gvDeliveryChallanDetails.SelectedRow.Cells[0].Text + " ";
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
            string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=deliverychallanSample&dcid=" + gvDeliveryChallanDetails.SelectedRow.Cells[0].Text + " ";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (btnSave.Visible == false && btnRevise.Text == "Revise")
        //{
        //    MessageBox.Show(this, "No More Items Can Be Added In this Revise Delivery Challan. Issue Another Delivery Challan For Other Items");
        //    return;
        //}
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

        if (Convert.ToInt32(txtItemQuantity.Text) > Convert.ToInt32(txtQtyInHand.Text))
        {
            MessageBox.Show(this, "Stock not Avaliable");
        }
        else
        {
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
        //txtremarks2.Text = "";
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        SM.DDLBindWithSelect(ddlModelNo, "SELECT YANTRA_ITEM_MAST.ITEM_CODE, YANTRA_ITEM_MAST.ITEM_MODEL_NO FROM YANTRA_ITEM_MAST INNER JOIN YANTRA_LKUP_PRODUCT_COMPANY ON YANTRA_ITEM_MAST.BRAND_ID = YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID  and YANTRA_LKUP_PRODUCT_COMPANY.PRODUCT_COMPANY_ID =" + ddlBrand.SelectedItem.Value + " order by  YANTRA_ITEM_MAST.ITEM_MODEL_NO "  );
    }

    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
          
            //Godown_Fill();
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
                txtRemarks.Text = ddlModelNo.SelectedItem.Text + ' ' + txtDescription.Text;

            }
            if (objMaster.StockEntry_select(ddlModelNo.SelectedItem.Value) > 0)
            {
                txtQtyInHand.Text = objMaster.itemquantity;
            }
            tblBlocked.Visible = true;
            BindBlockedItems();
            
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
        //Masters.ItemMaster objMaster = new Masters.ItemMaster();
        //if (objMaster.StockEntrychange_select(ddlModelNo.SelectedItem.Value, ddllocation.SelectedItem.Value, ddlColor.SelectedValue) > 0)
        //{

        //    txtQtyInHand.Text = objMaster.itemquantity;

        //}

       
        BindStock_Color();
       
    }
     private void BindStock_Color()
    {
        SqlCommand cmd = new SqlCommand("USP_Get_ModelNo_Color_Stock", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ColourId", ddlColor.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtQtyInHand.Text =dt.Rows[0][0].ToString();

        }
        else
        {
            txtQtyInHand.Text = "0";

        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtQtyInHand.Text = "";
        //Masters.ItemMaster.Stockentry1(ddllocation, ddlModelNo.SelectedItem.Value, ddlCompany.SelectedItem.Value);
    }
    

    protected void btnNew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("SampleDcDetails.aspx");

        //Old Code
        lblRevisedFrom.Visible = txtRevisedFrom.Visible = false;
        btnRevise.Attributes.Clear();
        //Inventory.ClearControls(this);
        txtDeliveryChallanNo.Text = Inventory.Delivery.Delivery_AutoGenCode();
        txtCustOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDeliveryChallanDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtLRDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlDCType.SelectedIndex = 2;
        ddlTransPorterName.SelectedValue = "1";
        ddlDespatchMode.SelectedValue = "1";
        btnRevise.Text = "Modify";
        btnSave.Text = "Save";
        Customer_fil();
        btnSave.Enabled = true;
        tblDCDetails.Visible = true;
        //tblButtons.Visible = true;
        ddlCompany1.SelectedValue = cp.getPresentCompanySessionValue();
        gvDeliveryChallanDetails.SelectedIndex = -1;
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
            lblInwardDate.Visible = lblInwardDateValInd.Visible = true;
              //  imgInwardDate.Visible = true;
            rfvInwardDate.Enabled = custValInwardDate.Enabled = true;
            txtInwardDate.Visible = true;
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
                e.Row.Cells[0].Visible =  false;
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
        //LinkButton lbtnDcNo;
        //lbtnDcNo = (LinkButton)sender;
        //GridViewRow Row = (GridViewRow)lbtnDcNo.Parent.Parent;
        //gvDeliveryChallanDetails.SelectedIndex = Row.RowIndex;
        //Response.Redirect("SampleDcDetails.aspx?DCId=" + gvDeliveryChallanDetails.SelectedRow.Cells[0].Text);

        # region Old Code
        if (gvDeliveryChallanDetails.SelectedIndex > -1)
        {
            try
            {
                Inventory.Delivery objDelivery = new Inventory.Delivery();

                if (objDelivery.Delivery_Select(gvDeliveryChallanDetails.SelectedRow.Cells[0].Text) > 0)
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
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                Inventory.Dispose();
                //ddlSalesOrderNo_SelectedIndexChanged(sender, e);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
        #endregion
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvDeliveryChallanDetails.SelectedIndex > -1)
        {
            try
            {
                SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                MessageBox.Show(this, objSM.SampleDc_Delete(gvDeliveryChallanDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
               
                btnDelete.Attributes.Clear();
                gvDeliveryChallanDetails.DataBind();
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
        objDC.PurchaseOrderDetailsByCustomerName_Select(ddlCustomerPo,ddlCustomerName.SelectedItem.Value);


    }

    #region Customer FIl

    private void Customer_fil()
    {
        SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomerName);
    }
    #endregion

    protected void lbtnDc_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = true;
        LinkButton lbtnDcNo;
        lbtnDcNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnDcNo.Parent.Parent;
        gvDeliveryChallanDetails.SelectedIndex = Row.RowIndex;
        lblDCid.Text = gvDeliveryChallanDetails.SelectedRow.Cells[0].Text;
        //Response.Redirect("SampleDcDetails.aspx?DCId=" + gvDeliveryChallanDetails.SelectedRow.Cells[0].Text);
        //+ "&SoId=" + gvWorkOrderDetails.SelectedRow.Cells[1].Text
        //+ "&DcType=" + gvWorkOrderDetails.SelectedRow.Cells[9].Text
        //+ "&DcFor=" + gvWorkOrderDetails.SelectedRow.Cells[13].Text
        //);

        //Old Code
        tblDCDetails.Visible = false;
        //tblButtons.Visible = false;

        btnRevise.Attributes.Clear();
        btnRevise.Text = "Modify";
        LinkButton lbtnDCNo;
        lbtnDCNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDCNo.Parent.Parent;
        gvDeliveryChallanDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        try
        {
            Inventory.Delivery objDelivery = new Inventory.Delivery();
            if (objDelivery.Delivery_Select(gvDeliveryChallanDetails.SelectedRow.Cells[0].Text) > 0)
            {
                btnSave.Enabled = false;
                btnSave.Text = "Update";
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
                //tblButtons.Visible = true;

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
        //ddlColor.SelectedValue = "0";
        BindStock_Location();
    }
    private void BindStock_Location()
    {
        SqlCommand cmd = new SqlCommand("USP_Get_ModelNo_Color_Loc_Stock", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@ColourId", ddlColor.SelectedItem.Value);
        cmd.Parameters.AddWithValue("@LocationId", ddllocation.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtQtyInHand.Text = dt.Rows[0][0].ToString();

        }
        else
        {
            txtQtyInHand.Text = "0";

        }
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvDeliveryChallanDetails.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvDeliveryChallanDetails.DataBind();
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != "")
        {
            ddlCustomerName.DataSourceID = "SqlDataSource3";
            ddlCustomerName.DataTextField = "CUST_NAME";
            ddlCustomerName.DataValueField = "CUST_ID";
            ddlCustomerName.DataBind();
            if (ddlCustomerName.SelectedIndex > -1)
            {
                ddlCustomerName_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show(this, "Customer Searched Does Not Exists");
            }
      
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
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDeliveryChallanDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvDeliveryChallanDetails.DataBind();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDetId;
        lbtnDetId = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDetId.Parent.Parent;
        gvDeliveryChallanItems.SelectedIndex = gvRow.RowIndex;
        Outward_Delete();

        Inventory.Delivery objInventory = new Inventory.Delivery();
        objInventory.DeliveryItem_Delete(gvDeliveryChallanItems.SelectedRow.Cells[10].Text);
        objInventory.DeliveryDetailsByCustIdSample_Select(ddlCustomerName.SelectedItem.Value, gvDeliveryChallanItems);

    }
    //private void Outward_Delete()
    //{
    //    for (int i = 0; i < Convert.ToInt32(gvDeliveryChallanItems.SelectedRow.Cells[7].Text); i++)
    //    {
    //        SqlCommand cmd = new SqlCommand("delete top (1) from [OUTWARD] where [ITEM_CODE]=" + gvDeliveryChallanItems.SelectedRow.Cells[2].Text + " and CUSTOMERID=" + ddlCustomerName.SelectedItem.Value + "", con);
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //    }
    //}

    private void Outward_Delete()
    {
       
        int qty = Convert.ToInt32(gvDeliveryChallanItems.SelectedRow.Cells[7].Text);
        int qty1 = Convert.ToInt32(gvDeliveryChallanItems.SelectedRow.Cells[7].Text);
       
        #region Outward Delete

        string Itemcode = gvDeliveryChallanItems.SelectedRow.Cells[2].Text;
        SqlCommand cmd2 = new SqlCommand("Usp_Outward_Items_New_SampleDC", con);
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.AddWithValue("@ItemCode", Itemcode);
       // cmd2.Parameters.AddWithValue("@ColorId", lblColorId.Text);
        cmd2.Parameters.AddWithValue("@CustId", ddlCustomerName.SelectedItem.Value);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        int remQty = 0;
        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < qty1; i++)
            {
                SqlCommand cmd1 = new SqlCommand("[Usp_Outward_Items_Delete]", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                if (qty >= Convert.ToInt32(dt2.Rows[i][1]))
                {
                    cmd1.Parameters.AddWithValue("@Flag", "1");
                }
                else if (qty < Convert.ToInt32(dt2.Rows[i][1]))
                {
                    cmd1.Parameters.AddWithValue("@Flag", "0");
                    remQty = Convert.ToInt32(dt2.Rows[i][1]) - qty;
                }
                cmd1.Parameters.AddWithValue("@ItemId", dt2.Rows[i][0]);
                cmd1.Parameters.AddWithValue("@Quantity", remQty);
                cmd1.Parameters.AddWithValue("@DateAdded", dt2.Rows[i][2]);
                cmd1.Parameters.AddWithValue("@PONo", lblDCid.Text);
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                qty = qty - Convert.ToInt32(dt2.Rows[i][1]);
                if (qty <= 0)
                {
                    break;
                }
            }
        }
        #endregion
    }
    protected void gvReservedStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
        }
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvReservedStock.Rows)
        {
            CheckBox ch = new CheckBox();
            ch = (CheckBox)gvrow.FindControl("chk");
            if (ch.Checked == true)
            {
                btnReleaseBlock.Visible = true;
            }
        }
    }
    protected void btnReleaseBlock_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {
                    string blockStock = gvrow.Cells[3].Text;
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int b = Convert.ToInt32(blockStock);
                    int q = Convert.ToInt32(qty.Text);


                    if (b >= q)
                    {                        
                        string Itemcode = gvrow.Cells[2].Text;
                        SqlCommand cmd = new SqlCommand("[Usp_Blocked_Items_New]", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", gvrow.Cells[0].Text);
                        cmd.Parameters.AddWithValue("@ColorId", gvrow.Cells[8].Text);
                        cmd.Parameters.AddWithValue("@DeliveryDate", gvrow.Cells[5].Text);
                        cmd.Parameters.AddWithValue("@CustId", gvrow.Cells[9].Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        int remQty = 0;
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < q; i++)
                            {
                                SqlCommand cmd1 = new SqlCommand("Usp_Blocked_Items_Release", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                if (q >= Convert.ToInt32(dt.Rows[i][1]))
                                {
                                    cmd1.Parameters.AddWithValue("@Flag", "1");
                                }
                                else if (q < Convert.ToInt32(dt.Rows[i][1]))
                                {
                                    cmd1.Parameters.AddWithValue("@Flag", "0");
                                    remQty = Convert.ToInt32(dt.Rows[i][1]) - q;
                                }
                                cmd1.Parameters.AddWithValue("@ItemId", dt.Rows[i][0]);
                                cmd1.Parameters.AddWithValue("@Quantity", remQty);
                                con.Open();
                                cmd1.ExecuteNonQuery();
                                con.Close();

                                q = q - Convert.ToInt32(dt.Rows[i][1]);
                                if (q <= 0)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "This Operation Cannot Be Performed.");
                    }

                }

            }


            foreach (GridViewRow gvrow in gvReservedStock.Rows)
            {

                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("chk");
                if (ch.Checked == true)
                {

                    DataTable ReleasedItems = new DataTable();

                    DataColumn col = new DataColumn();

                    col = new DataColumn("Item_Code");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("ITEM_MODEL_NO");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("COLOUR_NAME");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("Quantity");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("CUST_NAME");
                    ReleasedItems.Columns.Add(col);
                    col = new DataColumn("Delivery_Date");
                    ReleasedItems.Columns.Add(col);

                    if (gvReleasedItems.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvrow1 in gvReleasedItems.Rows)
                        {
                            if (gvReleasedItems.SelectedIndex > -1)
                            {
                                if (gvrow.RowIndex == gvReleasedItems.SelectedRow.RowIndex)
                                {


                                    DataRow dr = ReleasedItems.NewRow();
                                    dr["Item_Code"] = gvrow.Cells[0].Text;
                                    dr["ITEM_MODEL_NO"] = gvrow.Cells[1].Text;
                                    dr["COLOUR_NAME"] = gvrow.Cells[2].Text;
                                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                                    dr["Quantity"] = qty.Text;
                                    dr["CUST_NAME"] = gvrow.Cells[4].Text;
                                    dr["Delivery_Date"] = gvrow.Cells[5].Text;
                                    ReleasedItems.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = ReleasedItems.NewRow();
                                    dr["Item_Code"] = gvrow1.Cells[0].Text;
                                    dr["ITEM_MODEL_NO"] = gvrow1.Cells[1].Text;
                                    dr["COLOUR_NAME"] = gvrow1.Cells[2].Text;
                                    dr["Quantity"] = gvrow1.Cells[3].Text;
                                    dr["CUST_NAME"] = gvrow1.Cells[4].Text;
                                    dr["Delivery_Date"] = gvrow1.Cells[5].Text;
                                    ReleasedItems.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = ReleasedItems.NewRow();
                                dr["Item_Code"] = gvrow1.Cells[0].Text;
                                dr["ITEM_MODEL_NO"] = gvrow1.Cells[1].Text;
                                dr["COLOUR_NAME"] = gvrow1.Cells[2].Text;
                                dr["Quantity"] = gvrow1.Cells[3].Text;
                                dr["CUST_NAME"] = gvrow1.Cells[4].Text;
                                dr["Delivery_Date"] = gvrow1.Cells[5].Text;
                                ReleasedItems.Rows.Add(dr);
                            }
                        }
                    }

                    if (gvReleasedItems.SelectedIndex == -1)
                    {
                        DataRow drnew = ReleasedItems.NewRow();
                        drnew["Item_Code"] = gvrow.Cells[0].Text;
                        drnew["ITEM_MODEL_NO"] = gvrow.Cells[1].Text;
                        drnew["COLOUR_NAME"] = gvrow.Cells[2].Text;
                        TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                        drnew["Quantity"] = qty.Text;
                        drnew["CUST_NAME"] = gvrow.Cells[4].Text;
                        drnew["Delivery_Date"] = gvrow.Cells[5].Text;

                        ReleasedItems.Rows.Add(drnew);
                    }
                    gvReleasedItems.DataSource = ReleasedItems;
                    gvReleasedItems.DataBind();
                    gvReleasedItems.SelectedIndex = -1;
                    ch.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            BindBlockedItems();
            btnReleaseBlock.Visible = false;
        }
    }
    protected void gvReleasedItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void BindBlockedItems()
    {
        SqlCommand cmd = new SqlCommand("[USP_CustBlockedItemsNew]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvReservedStock.DataSource = dt;
        gvReservedStock.DataBind();
    }
}

 
