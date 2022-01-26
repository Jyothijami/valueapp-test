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

public partial class Modules_SCM_Shipment_Details : basePage
{

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();

            SupplierFixedPO_Fill();
            SupplierName_Fill();
            forwarderfill();
            txtShippingdate.Text = DateTime.Now.ToString("d");
            txtFollowDate.Text = DateTime.Now.ToString("d");
            txtdateofshipping.Text = DateTime.Now.ToString("d");
            insurancecompanyfill();
        }
    }


    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "25");
        btnAdd.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        //btnFollowup.Enabled = up.Followup;
        //btnSave.Enabled = up.Save;
        //btnRefresh.Enabled = up.Refresh;
        //btnclose.Enabled = up.Close;
        //btnFollowSave.Enabled = up.add;
        //btnFollowRefresh.Enabled = up.FollowRefresh;
        //btnHistory.Enabled = up.History;
        //btnFollowClose.Enabled = up.FollowClose;
        //BtnClose1.Enabled = up.Close1;
        //btnPrint.Enabled = up.Print;

    }
    private void insurancecompanyfill()
    {
        Masters.InsuranceMaster.Insurance_Select(ddlInsuranceCompany);
    }

    private void forwarderfill()
    {
        Masters.ForwardedDetails.Forwarder_Select(ddlForwarder);
    }
    #endregion

    #region Purchase Order Fill
    private void SupplierFixedPO_Fill()
    {
        try
        {
            SCM.PurchaseInvoice.PurchaseInvoice_Select(ddlPONo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SCM.Dispose();
        }
    }
    #endregion

    #region Supplier Name
    private void SupplierName_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SCM.Dispose();
        }
    }
    #endregion

    #region DDl PO nO Change
    protected void ddlPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SCM.PurchaseInvoice objSCMPO = new SCM.PurchaseInvoice();
            if(objSCMPO.PurchaseInvoice_Select(ddlPONo.SelectedItem.Value)>0)
          //  SCM.SupplierFixedPO objSCMPO = new SCM.SupplierFixedPO();
           // if (objSCMPO.SuppliersFixedPO_Select(ddlPONo.SelectedItem.Value) > 0)
            {
                txtPOamount.Text = objSCMPO.POAmount;
                txtPIDate.Text = objSCMPO.PIDate;
                txtPoDate.Text = objSCMPO.PODate;
                txtPiAount.Text = objSCMPO.PIGrossAmt;
                txtPoNo.Text = objSCMPO.FPONo;
                lblPONo.Text = objSCMPO.FPOId;
                objSCMPO.PurchaseInvoiceDetails_Select(objSCMPO.PIId, gvItemDetails);
             //  objSCMPO.SuppliersFixedPODetails_Select(objSCMPO.FPOId, gvItDetails);

                SCM.SuppliersMaster objSCMSM = new SCM.SuppliersMaster();
                if (objSCMSM.SuppliersMaster_Select(objSCMPO.SUPId) > 0)
                {
                    ddlSupplierName.SelectedValue = objSCMSM.SupId;
                    txtSupplierName.Text = objSCMSM.SupName;
                    txtAddress.Text = objSCMSM.SupAddress;
                    txtEmail.Text = objSCMSM.SupEmail;
                    txtContactPerson.Text = objSCMSM.SupContactPerson;
                    txtPhoneNo.Text = objSCMSM.SupPhone;
                    txtMobileNo.Text = objSCMSM.SupMobile;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnDelete.Attributes.Clear();
            SCM.Dispose();
        }
    }

    #endregion

    #region Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShipmentDetailsNew.aspx");

        //SCM.ClearControls(this);
        //txtShipmentNo.Text = SCM.ShipmentDetails.Shipmentdetails_AutoGenCode();
        //txtShippingdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtdateArrival.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtMaterialReceiptDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtdateofshipping.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtRemittenceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        
        //btnSave.Text = "Save";
        //tblDetails.Visible = true;
      
        //gvShippingDetails.SelectedIndex = -1;
        //gvItemDetails.DataBind();

       
    }
    #endregion

    #region FollowUp Details
    protected void btnFollowup_Click(object sender, EventArgs e)
    {


        if (gvShippingDetails.SelectedIndex > -1)
        {
            gvFollowuphistory.DataBind();
            txtFollowname.Text = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpName];
            txtFollowDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (tblFollowup.Visible == false)
            {

                tblFollowup.Visible = true;
            }
            else if (tblFollowup.Visible == true)
            {
                tblFollowup.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }


    }
    protected void btnHistory_Click(object sender, EventArgs e)
    {

        if (tblFollowup.Visible == false)
        {
            tblfollowupgrid.Visible = false;
        }
        else if (tblFollowup.Visible == true)
        {
            tblfollowupgrid.Visible = true;
        }

    }
    protected void btnFollowClose_Click(object sender, EventArgs e)
    {
        tblFollowup.Visible = false;
    }



    #endregion

    #region main Close
    protected void btnclose_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
    }
    #endregion

    #region Follow Refresh
    protected void btnFollowRefresh_Click(object sender, EventArgs e)
    {
        //SCM.ClearControls(this);
        txtFollowupDetails.Text = string.Empty;

    }
    #endregion

    #region Main Refresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SCM.ClearControls(this);
    }
    #endregion

    #region Save & Update
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ShippingDetailsSave();
        }
        else if (btnSave.Text == "Update")
        {
            ShipmentDetailsUpdate();
        }
    }
    #endregion

    #region Shipping Details Save

    private void  ShippingDetailsSave()
    {
      
            try
            {
                SCM.ShipmentDetails objSCM = new SCM.ShipmentDetails();
                SCM.BeginTransaction();
                
                objSCM.sdno = txtShipmentNo.Text;
                objSCM.sddate = Yantra.Classes.General.toMMDDYYYY(txtShippingdate.Text);
                //objSCM.fpoid = ddlPONo.SelectedItem.Value;
                objSCM.fpoid = lblPONo.Text;
                
                objSCM.supid = ddlSupplierName.SelectedItem.Value;

                objSCM.forwardingthroghu = txtFortrough.Text;
                objSCM.shipmentdetails = txtShipmentDetails.Text;
                objSCM.insurance = txtInsurance.Text;
                objSCM.packingcharges= txtPackingCharges.Text;
                objSCM.dutyexcise = txtDutyExcise.Text;
                objSCM.customclearance = txtCustomClearance.Text;
                objSCM.datearrival =Yantra.Classes.General.toMMDDYYYY( txtdateArrival.Text);
                objSCM.materialreceiptdate =Yantra.Classes.General.toMMDDYYYY( txtMaterialReceiptDate.Text);
                objSCM.dateofshipping = Yantra.Classes.General.toMMDDYYYY(txtdateofshipping.Text);
                objSCM.cpid = cp.getPresentCompanySessionValue();
                objSCM.SDADDRESS = txtShipmentDetailsAddress.Text;
               objSCM.SDCONTACTPERSON = txtContactPerson.Text;
                 objSCM.SDPHONENO = txtPhoneNo.Text;
                 objSCM.SDEMAIL = txtEmail.Text;
                 objSCM.SDREMAMOUNT = txtRemittenceAmount.Text;
                 objSCM.SDREMDATE = Yantra.Classes.General.toMMDDYYYY(txtRemittenceDate.Text);
                 objSCM.SDCONTAINER = txtContainer.Text;
                 objSCM.SDVOLUME = txtVolume.Text;
                 objSCM.SDWEIGHT = txtWeight.Text;
                 objSCM.Invoiceno = txtInvoiceno.Text;
                 objSCM.InvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtInvoicedate.Text);
                 objSCM.InvoiceValue = txtInvoiceValue.Text;
                 objSCM.Forwoarderid = ddlForwarder.SelectedItem.Value;
                 objSCM.insamount = txtInsuranceAmount.Text;
                 objSCM.inscompnayname = ddlInsuranceCompany.SelectedItem.Value;
                 objSCM.insdate = Yantra.Classes.General.toMMDDYYYY(txtInsuranceDate.Text);
                
             MessageBox.Show(this,objSCM.ShipmentDetails_Save() );
             SCM.CommitTransaction();
            }

            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvShippingDetails.DataBind();
               
                tblDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }

        }
    #endregion

    #region Follow Up Save
        protected void btnFollowSave_Click(object sender, EventArgs e)
    {
        try
        {
            SCM.ShipmentDetails objSMAssign = new SCM.ShipmentDetails();
            SCM.BeginTransaction();
            objSMAssign.sdid = gvShippingDetails.SelectedRow.Cells[0].Text;
          //  objSMAssign.funame = txtFollowname.Text;
            objSMAssign.fudesc = txtFollowupDetails.Text;
            objSMAssign.fudate = DateTime.Now.ToString();
            objSMAssign.funame = Yantra.Authentication.EmpDetails[(int)Yantra.Authentication.Logged_EMP_Details.EmpId];
            MessageBox.Show(this, objSMAssign.ShipmentFollowups_Save());
            SCM.CommitTransaction();
        }
        catch (Exception ex)
        {
            SCM.RollBackTransaction();
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvFollowuphistory.DataBind();
            txtFollowupDetails.Text = string.Empty;
            SCM.Dispose();
        }
    }
        #endregion

    #region ShipmentDetails Update
    private void ShipmentDetailsUpdate()
    {
       
            try
            {
                SCM.ShipmentDetails objSCM = new SCM.ShipmentDetails();
                SCM.BeginTransaction();
                objSCM.sdid = gvShippingDetails.SelectedRow.Cells[0].Text;
                objSCM.sdno = txtShipmentNo.Text;
                objSCM.sddate = Yantra.Classes.General.toMMDDYYYY(txtShippingdate.Text);
                //objSCM.fpoid = ddlPONo.SelectedItem.Value;
                objSCM.fpoid = lblPONo.Text;
                objSCM.supid = ddlSupplierName.SelectedItem.Value;

                objSCM.forwardingthroghu = txtFortrough.Text;
                objSCM.shipmentdetails = txtShipmentDetails.Text;
                objSCM.insurance = txtInsurance.Text;
                objSCM.packingcharges = txtPackingCharges.Text;
                objSCM.dutyexcise = txtDutyExcise.Text;
                objSCM.customclearance = txtCustomClearance.Text;
                objSCM.datearrival = Yantra.Classes.General.toMMDDYYYY(txtdateArrival.Text);
                objSCM.materialreceiptdate = Yantra.Classes.General.toMMDDYYYY(txtMaterialReceiptDate.Text);
                objSCM.dateofshipping = Yantra.Classes.General.toMMDDYYYY(txtdateofshipping.Text);
                objSCM.cpid = cp.getPresentCompanySessionValue();
                objSCM.SDADDRESS = txtShipmentDetailsAddress.Text;
                objSCM.SDCONTACTPERSON = txtContactPerson.Text;
                objSCM.SDPHONENO = txtPhoneNo.Text;
                objSCM.SDEMAIL = txtEmail.Text;
                objSCM.SDREMAMOUNT = txtRemittenceAmount.Text;
                objSCM.SDREMDATE = Yantra.Classes.General.toMMDDYYYY(txtRemittenceDate.Text);
                objSCM.SDCONTAINER = txtContainer.Text;
                objSCM.SDVOLUME = txtVolume.Text;
                objSCM.SDWEIGHT = txtWeight.Text;
                objSCM.Invoiceno = txtInvoiceno.Text;
                objSCM.InvoiceDate = Yantra.Classes.General.toMMDDYYYY(txtInvoicedate.Text);
                objSCM.InvoiceValue = txtInvoiceValue.Text;
                objSCM.Forwoarderid = ddlForwarder.SelectedItem.Value;
                objSCM.insamount = txtInsuranceAmount.Text;
                objSCM.inscompnayname = ddlInsuranceCompany.SelectedItem.Value;
                objSCM.insdate = Yantra.Classes.General.toMMDDYYYY(txtInsuranceDate.Text);



                 MessageBox.Show(this,objSCM.ShipmentDetails_Update());
                 SCM.CommitTransaction();
            }
            catch (Exception ex)
            {
                SCM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
               btnSave.Text = "Save";
                btnDelete.Attributes.Clear();
                gvShippingDetails.DataBind();
         
                tblDetails.Visible = false;
                SCM.ClearControls(this);
                SCM.Dispose();
            }
     
   
    }
    #endregion

    #region Delete
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvShippingDetails.SelectedIndex > -1)
        {
            try
            {
                SCM.ShipmentDetails objSCM = new SCM.ShipmentDetails();
                MessageBox.Show(this, objSCM.ShipmentDetails_Delete(gvShippingDetails.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvShippingDetails.DataBind();
                SCM.ClearControls(this);
                SCM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }

    #endregion

    #region Search
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Edit Clilck
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShipmentDetailsNew.aspx?shipmentNo=" + gvShippingDetails.SelectedRow.Cells[0].Text);
        //try
        //{
        //    SCM.ShipmentDetails objSCM = new SCM.ShipmentDetails();

        //    if (objSCM.ShipmentDetails_Select(gvShippingDetails.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = true;
        //        tblDetails.Visible = true;
        //        txtShipmentNo.Text = objSCM.sdno;
        //        txtShippingdate.Text = objSCM.sddate;
        //        ddlPONo.SelectedValue = objSCM.fpoid;
        //        ddlPONo_SelectedIndexChanged(sender, e);
        //        txtFortrough.Text = objSCM.forwardingthroghu;
        //        txtShipmentDetails.Text = objSCM.shipmentdetails;
        //        txtInsurance.Text = objSCM.insurance;
        //        txtPackingCharges.Text = objSCM.packingcharges;
        //        txtDutyExcise.Text = objSCM.dutyexcise;
        //        txtCustomClearance.Text = objSCM.customclearance;
        //        txtdateArrival.Text = objSCM.datearrival;
        //        //ddlDespatchMode.SelectedValue = objSCM.DESPMId;
        //        txtMaterialReceiptDate.Text = objSCM.materialreceiptdate;
        //        txtdateofshipping.Text = objSCM.dateofshipping;
        //        txtAddress.Text = objSCM.SDADDRESS;
        //        txtSupplierName.Text = objSCM.SDCONTACTPERSON;
        //        txtPhoneNo.Text = objSCM.SDPHONENO;
        //        txtEmail.Text = objSCM.SDEMAIL;
        //        txtRemittenceAmount.Text = objSCM.SDREMAMOUNT;
        //        txtRemittenceDate.Text = objSCM.SDREMDATE;
        //        txtContainer.Text = objSCM.SDCONTAINER;
        //        txtVolume.Text = objSCM.SDVOLUME;
        //        txtWeight.Text = objSCM.SDWEIGHT;
        //        txtInvoiceValue.Text = objSCM.InvoiceValue;
        //        txtInvoiceno.Text = objSCM.Invoiceno;
        //        txtInvoicedate.Text = objSCM.InvoiceDate;
        //        ddlForwarder.SelectedValue = objSCM.Forwoarderid;
        //        ddlForwarder_SelectedIndexChanged(sender, e);
        //        ddlInsuranceCompany.SelectedValue = objSCM.inscompnayname;
        //        txtInsuranceDate.Text = objSCM.insdate;
        //        txtInsuranceAmount.Text = objSCM.insamount;

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    btnDelete.Attributes.Clear();
        //}
    }
    #endregion

    #region Item Details DataBound
    protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text) + ((Convert.ToDecimal(e.Row.Cells[8].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100) + ((Convert.ToDecimal(e.Row.Cells[9].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100) + ((Convert.ToDecimal(e.Row.Cells[10].Text) * (Convert.ToDecimal(e.Row.Cells[6].Text) * Convert.ToDecimal(e.Row.Cells[7].Text))) / 100));
        }
    }
    protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }



    protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    #endregion

    #region Link Buttton Sd No Click
    protected void lbtnSdNo_Click(object sender, EventArgs e)
    {
        tblDetails.Visible = false;
        LinkButton lbtnsdno;
        lbtnsdno = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnsdno.Parent.Parent;
        gvShippingDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

        //try
        //{
        //    SCM.ShipmentDetails objSCM = new SCM.ShipmentDetails();

        //    if (objSCM.ShipmentDetails_Select(gvShippingDetails.SelectedRow.Cells[0].Text) > 0)
        //    {
        //        btnSave.Text = "Update";
        //        btnSave.Enabled = false;
        //        tblDetails.Visible = true;
        //        txtShipmentNo.Text = objSCM.sdno;
        //        txtShippingdate.Text = objSCM.sddate;
        //        ddlPONo.SelectedValue = objSCM.fpoid;
        //        ddlPONo_SelectedIndexChanged(sender, e);
        //        txtFortrough.Text = objSCM.forwardingthroghu;
        //        txtShipmentDetails.Text = objSCM.shipmentdetails;
        //        txtInsurance.Text = objSCM.insurance;
        //        txtPackingCharges.Text = objSCM.packingcharges;
        //        txtDutyExcise.Text = objSCM.dutyexcise;
        //        txtCustomClearance.Text = objSCM.customclearance;
        //        txtdateArrival.Text = objSCM.datearrival;
        //        txtMaterialReceiptDate.Text = objSCM.materialreceiptdate;
        //        txtdateofshipping.Text = objSCM.dateofshipping;
        //        txtAddress.Text = objSCM.SDADDRESS;
        //        txtSupplierName.Text = objSCM.SDCONTACTPERSON;
        //        txtPhoneNo.Text = objSCM.SDPHONENO;
        //        txtEmail.Text = objSCM.SDEMAIL;
        //        txtRemittenceAmount.Text = objSCM.SDREMAMOUNT;
        //        txtRemittenceDate.Text = objSCM.SDREMDATE;
        //        txtContainer.Text = objSCM.SDCONTAINER;
        //        txtVolume.Text = objSCM.SDVOLUME;
        //        txtWeight.Text = objSCM.SDWEIGHT;
        //        txtInvoiceValue.Text = objSCM.InvoiceValue;
        //        txtInvoiceno.Text = objSCM.Invoiceno;
        //        txtInvoicedate.Text = objSCM.InvoiceDate;
        //        ddlForwarder.SelectedValue = objSCM.Forwoarderid;
        //        ddlForwarder_SelectedIndexChanged(sender, e);
        //        ddlInsuranceCompany.SelectedValue = objSCM.inscompnayname;
        //        txtInsuranceDate.Text = objSCM.insdate;
        //        txtInsuranceAmount.Text = objSCM.insamount;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(this, ex.Message.ToString());
        //}
        //finally
        //{
        //    btnDelete.Attributes.Clear();
        //    //SCM.Dispose();
        //    //ddlPONo_SelectedIndexChanged(sender, e);

        //}
    }
    #endregion

    #region GV Shipping Details Databound
    protected void gvShippingDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;

        }
    }
    #endregion

    #region Print
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvShippingDetails.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ShipmentFallowUp&sdid=" + gvShippingDetails.SelectedRow.Cells[0].Text + "";
              //  string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=ShipmentFallowUp";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record");
        }
    }
    #endregion

    #region Close
    protected void BtnClose1_Click(object sender, EventArgs e)
    {
        tblfollowupgrid.Visible = false;
    }
    #endregion

    protected void ddlForwarder_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ForwardedDetails obj = new Masters.ForwardedDetails();
        if (obj.Forwarderdetails_Ddl(ddlForwarder.SelectedItem.Value) > -1)
        {
            txtshipingDetailsPhone.Text = obj.Forphone;
            txtShipmentDetailsAddress.Text = obj.Foraddress;
            txtShippingdetailsEmail.Text = obj.ForEmail;

            
        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvShippingDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvShippingDetails.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvShippingDetails.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
