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
using YantraDAL;
using System.Data.SqlClient;
using vllib;

public partial class Modules_Services_CustomerInformationNew : System.Web.UI.Page
{
    string customerCode = "";
    ScriptManager ScriptManagerLocal;
    string table = "YANTRA_CUSTOMER_MAST", field = "CUST_NAME", field2 = "CUST_COMPANY_NAME", field3 = "CUST_CONTACT_PERSON", field4 = "CUST_PHONE", field5 = "CUST_MOBILE", field6 = "CUST_ADDRESS";
    string msg, msg1, msg2, msg3, msg4, msg5;
    public static string str1, str2, str3, str4, str5, str6, StatusStr1, StatusStr2, StatusStr3, StatusStr4, StatusStr5, StatusStr6;
    static DBManager dbManager = new DBManager(DataProvider.SqlServer, DBConString.ConnectionString());
    private static string _commandText;
    protected void Page_Load(object sender, EventArgs e)
    {
        customerCode = Request.QueryString["customerCode"];
        if(!IsPostBack)
        {
            if (customerCode == "")
            {
                //gvCustMasterDetails.SelectedIndex = -1;
                SM.ClearControls(this);
                btnSave.Text = "Save";
                btnSave.Enabled = true;
                txtCustomerCode.Text = SM.CustomerMaster.CustomerMaster_AutoGenCode();
                tblCustomerDetails.Visible = true;
                //  tblUnitDetails.Visible = true;
                tblContactDetails.Visible = false;
                gvOtherCorpDetails.DataBind();
                gvUnitDetails.DataBind();
                gvCustomerItems.DataBind();
                tdContactDetailsHead.Visible = tdUnitDetailsHead.Visible = tdOtherCorpDetailsHead.Visible = false;
                btnOtherCorpDetails.Visible = btnAddUnits.Visible = true;
            }
            else
            {

                FillCustomerDetails();
            }

            //lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
            //lblSearchValueHidden.Text = txtSearchText.Text;
            lblCPID.Text = cp.getPresentCompanySessionValue();
            lblUserType.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpType);





           // gvCustMasterDetails.DataBind();



            btnForApproveHidden.Style.Add("display", "none");

            Button3.Style.Add("display", "none");

            Region_Fill();
            IndustryType_Fill();
            Designation_Fill();
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

            #region RequestCustomerRedirect
            if (Request.QueryString["CustId"] != null)
            {
                string CustId = Request.QueryString["CustId"].ToString();

                UnitName_Fill(CustId);

                SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
                if ((objSMCustomer.CustomerMaster_Select(CustId)) > 0)
                {
                    btnSave.Text = "Update";
                    tblCustomerDetails.Visible = true;
                    btnSave.Enabled = false;
                    tblUnitDetails.Visible = false;
                    tblContactDetails.Visible = false;
                    txtCustomerCode.Text = objSMCustomer.CustCode;
                    txtCustomerName.Text = objSMCustomer.CustName;
                    ddlRegion.SelectedValue = objSMCustomer.RegId;
                    txtCompanyName.Text = objSMCustomer.CompName;
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtAddress.Text = objSMCustomer.Address;
                    txtContactNo1.Text = objSMCustomer.Phone;
                    txtContactNo2.Text = objSMCustomer.Mobile;
                    txtFAXNo.Text = objSMCustomer.Fax;
                    txtEmail.Text = objSMCustomer.Email;
                    ddlIndustryType.SelectedValue = objSMCustomer.IndTypeId;
                    txtWebsite.Text = objSMCustomer.Website;
                    txtPANNo.Text = objSMCustomer.PANNo;
                    txtECCNo.Text = objSMCustomer.ECCNo;
                    txtCSTNo.Text = objSMCustomer.CSTNo;
                    txtLocalSalesTaxNo.Text = objSMCustomer.LocalSTNo;
                    txtSpecialInstructions.Text = objSMCustomer.SplInsrs;
                    ddlDesignationNo.SelectedValue = objSMCustomer.DesgId;
                    rbNewExisting.SelectedValue = objSMCustomer.IsNewOrExisting;
                    //txtCorpContactPerson.Text = objSMCustomer.CorpContactPerson;
                    //ddlCorpDesignationNo.SelectedValue = objSMCustomer.CorpDesgId;
                    //txtCorpAddress.Text = objSMCustomer.CorpAddress;
                    //txtCorpContactNo1.Text = objSMCustomer.CorpPhone;
                    //txtCorpContactNo2.Text = objSMCustomer.CorpMobile;
                    //txtCorpFaxNo.Text = objSMCustomer.CorpFax;
                    //txtCorpEmail.Text = objSMCustomer.CorpEmail;

                    objSMCustomer.CustomerMasterDetails_Select(CustId, "0", gvOtherCorpDetails);
                    //objSMCustomer.CustomerMasterDetails_Select(customerCode, gvCustomerItems);
                    tdContactDetailsHead.Visible = tdUnitDetailsHead.Visible = true;
                    gvCustomerItems.DataBind(); gvUnitDetails.SelectedIndex = -1;
                    objSMCustomer.CustomerUnitDetails_Select(CustId, gvUnitDetails);
                    btnOtherCorpDetails.Visible = btnAddUnits.Visible = false;
                }

                //lblSearchItemHidden.Text = "CUST_NAME";
                //lblSearchValueHidden.Text = Request.QueryString["CustName"].ToString();
                // gvCustMasterDetails.DataBind();
                //dbManager.Open();
                //_commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE YANTRA_CUSTOMER_MAST.CUST_ID =" + CustId);
                //SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                //DataSet ds = new DataSet();
                //da.Fill(ds, "enq");
                //gvCustMasterDetails.DataSource = ds;
                //gvCustMasterDetails.DataBind();
                //dbManager.DataReader.Close();

            }
            #endregion
        }
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }

    private void FillCustomerDetails()
    {
        SM.ClearControls(this);
        try
        {
            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMaster_Select(customerCode)) > 0)
            {
                tblCustomerDetails.Visible = true;
                tblUnitDetails.Visible = true;
                txtCustomerCode.Text = objSMCustomer.CustCode;
                txtCustomerName.Text = objSMCustomer.CustName;
                ddlRegion.SelectedValue = objSMCustomer.RegId;
                txtCompanyName.Text = objSMCustomer.CompName;
                txtContactPerson.Text = objSMCustomer.ContactPerson;
                txtAddress.Text = objSMCustomer.Address;
                txtContactNo1.Text = objSMCustomer.Phone;
                txtContactNo2.Text = objSMCustomer.Mobile;
                txtFAXNo.Text = objSMCustomer.Fax;
                txtEmail.Text = objSMCustomer.Email;
                ddlIndustryType.SelectedValue = objSMCustomer.IndTypeId;
                txtWebsite.Text = objSMCustomer.Website;
                txtPANNo.Text = objSMCustomer.PANNo;
                txtECCNo.Text = objSMCustomer.ECCNo;
                txtCSTNo.Text = objSMCustomer.CSTNo;
                txtLocalSalesTaxNo.Text = objSMCustomer.LocalSTNo;
                txtSpecialInstructions.Text = objSMCustomer.SplInsrs;
                ddlDesignationNo.SelectedValue = objSMCustomer.DesgId;
                rbNewExisting.SelectedValue = objSMCustomer.IsNewOrExisting;
                //txtCorpContactPerson.Text = objSMCustomer.CorpContactPerson;
                //ddlCorpDesignationNo.SelectedValue = objSMCustomer.CorpDesgId;
                //txtCorpAddress.Text = objSMCustomer.CorpAddress;
                //txtCorpContactNo1.Text = objSMCustomer.CorpPhone;
                //txtCorpContactNo2.Text = objSMCustomer.CorpMobile;
                //txtCorpFaxNo.Text = objSMCustomer.CorpFax;
                //txtCorpEmail.Text = objSMCustomer.CorpEmail;


                btnSave.Visible = true;
                btnSave.Enabled = true;
                btnSave.Text = "Update";
                gvUnitDetails.SelectedIndex = -1;
                objSMCustomer.CustomerMasterDetails_Select(customerCode, "0", gvOtherCorpDetails);
                objSMCustomer.CustomerMasterDetails_Select(customerCode, gvCustomerItems);
                objSMCustomer.CustomerUnitDetails_Select(customerCode, gvUnitDetails);
                tblContactDetails.Visible = true;
                gvCustomerItems.Visible = true;
                tblUnitDetails.Visible = true;
                gvUnitDetails.Visible = true;
                tdContactDetailsHead.Visible = tdUnitDetailsHead.Visible = tdOtherCorpDetailsHead.Visible = tblOtherCorpDetails.Visible = true;
                btnOtherCorpDetails.Visible = btnAddUnits.Visible = true;



                //gvCustomerItems.Visible = true;

                // objSMCustomer.CustomerMasterDetails_Select(customerCode, Repeater1);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            SM.Dispose();
        }
    }

    #region PAGE PREREDER
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save" && btnSave.Enabled == true)
        {
            btnRefresh.Visible = true;
        }
        else
        {
            btnRefresh.Visible = false;
        }

        if (gvUnitDetails.SelectedIndex > -1)
        {
            if (gvCustomerItems.Rows.Count == 0)
            {
                lblContectDetailsGridLabel.Text = "No Contacts Found";
            }
            else
            {
                lblContectDetailsGridLabel.Text = "";
            }
        }
        else
        {
            if (gvCustomerItems.Rows.Count == 0)
            {
                lblContectDetailsGridLabel.Text = "Please Select An Unit Name";
            }
            else
            {
                lblContectDetailsGridLabel.Text = "";
            }
        }
        if (btnSave.Text == "Save")
        {
            lblContectDetailsGridLabel.Text = "";
        }
    }
    #endregion

    #region Region_Fill
    private void Region_Fill()
    {
        try
        {
            Masters.RegionalMaster.RegionalMaster_Select(ddlRegion);
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

    #region IndustryType_Fill
    private void IndustryType_Fill()
    {
        try
        {
            Masters.IndustryType.IndustryType_Select(ddlIndustryType);
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

    #region Designation Master Fill
    private void Designation_Fill()
    {
        try
        {
            Masters.Designation.Designation_Select(ddlDesignationNo);
            Masters.Designation.Designation_Select(ddlOtherCorpDesignation);
            Masters.Designation.Designation_Select(ddlCorpoDesignationNo);
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

    #region Unit Name Fill
    private void UnitName_Fill(string CustId)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, CustId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Unit Name Fill As Per Unit Names Grid
    private void UnitName_Fill()
    {
        try
        {
            ddlUnitName.Items.Clear();
            ddlUnitName.Items.Add(new ListItem("--", "0"));
            foreach (GridViewRow gvRow in gvUnitDetails.Rows)
            {
                if (gvRow.Cells[4].Text != "delete")
                {
                    ddlUnitName.Items.Add(new ListItem(gvRow.Cells[4].Text, gvRow.Cells[5].Text));
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            SM.Dispose();
        }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            CustomerMasterSave();
            tblCustomerDetails.Visible = true;
        }
        else if (btnSave.Text == "Update")
        {
            CustomerMasterUpdate();
            tblCustomerDetails.Visible = false;
        }
        //gvCustMasterDetails.SelectedIndex = -1;
    }
    #endregion

    #region CustomerMasterSave
    private void CustomerMasterSave()
    {

        str1 = "";
        str2 = "";
        str3 = "";
        str4 = "";
        str5 = "";
        str6 = "";
        if (Yantra.Classes.General.IsRecordExists1(table, field, txtCustomerName.Text) == true)
        {
            msg = " CustomerName: " + txtCustomerName.Text;
            dbManager.Open();
            _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_NAME ='" + txtCustomerName.Text + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                str1 = dbManager.DataReader["CUST_ID"].ToString();
                StatusStr1 = dbManager.DataReader["CUST_STATUS"].ToString();

            }
            dbManager.DataReader.Close();

        }

        if (Yantra.Classes.General.IsRecordExists1(table, field2, txtCompanyName.Text) == true)
        {
            msg1 = " CompanyName: " + txtCompanyName.Text;
            dbManager.Open();
            _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_COMPANY_NAME ='" + txtCompanyName.Text + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                str2 = dbManager.DataReader["CUST_ID"].ToString();
                StatusStr2 = dbManager.DataReader["CUST_STATUS"].ToString();
            }
            dbManager.DataReader.Close();
        }
        if (Yantra.Classes.General.IsRecordExists1(table, field3, txtContactPerson.Text) == true)
        {
            msg2 = " ContactPerson: " + txtContactPerson.Text;
            dbManager.Open();
            _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_CONTACT_PERSON ='" + txtContactPerson.Text + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                str3 = dbManager.DataReader["CUST_ID"].ToString();
                StatusStr3 = dbManager.DataReader["CUST_STATUS"].ToString();
            }
            dbManager.DataReader.Close();
        }
        if (Yantra.Classes.General.IsRecordExists1(table, field4, txtContactNo1.Text) == true)
        {
            msg3 = " PhoneNumber: " + txtContactNo1.Text;
            dbManager.Open();
            _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_PHONE ='" + txtContactNo1.Text + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                str4 = dbManager.DataReader["CUST_ID"].ToString();
                StatusStr4 = dbManager.DataReader["CUST_STATUS"].ToString();
            }
            dbManager.DataReader.Close();
        }
        if (Yantra.Classes.General.IsRecordExists1(table, field5, txtContactNo2.Text) == true)
        {
            msg4 = " MobileNumber: " + txtContactNo2.Text;
            dbManager.Open();
            _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_MOBILE ='" + txtContactNo2.Text + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                str5 = dbManager.DataReader["CUST_ID"].ToString();
                StatusStr5 = dbManager.DataReader["CUST_STATUS"].ToString();
            }
            dbManager.DataReader.Close();
        }
        if (Yantra.Classes.General.IsRecordExists1(table, field6, txtAddress.Text) == true)
        {
            msg5 = " Address: " + txtAddress.Text;
            dbManager.Open();
            _commandText = string.Format("SELECT CUST_ID,CUST_STATUS FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ADDRESS ='" + txtAddress.Text + "'");
            dbManager.ExecuteReader(CommandType.Text, _commandText);
            if (dbManager.DataReader.Read())
            {
                str6 = dbManager.DataReader["CUST_ID"].ToString();
                StatusStr6 = dbManager.DataReader["CUST_STATUS"].ToString();
            }
            dbManager.DataReader.Close();
        }

        if (msg != null)
        {
            lblData.Text = msg;
        }
        if (msg1 != null)
        {
            lblData1.Text = msg1;
        }
        if (msg2 != null)
        {
            lblData2.Text = msg2;
        }
        if (msg3 != null)
        {
            lblData3.Text = msg3;
        }
        if (msg4 != null)
        {
            lblData4.Text = msg4;
        }
        if (msg5 != null)
        {
            lblData5.Text = msg5;
        }
        if (msg5 != null || msg4 != null || msg3 != null || msg2 != null || msg1 != null || msg != null)
        {
            tblPopUp1.Visible = true;

            ModalPopupExtender.Show();


        }
        else
        {
            try
            {
                SM.CustomerMaster objMaster = new SM.CustomerMaster();
                SM.BeginTransaction();
                objMaster.CustCode = txtCustomerCode.Text;
                objMaster.RegId = ddlRegion.SelectedItem.Value;
                objMaster.CustName = txtCustomerName.Text;

                objMaster.CompName = txtCompanyName.Text;

                objMaster.ContactPerson = txtContactPerson.Text;
                objMaster.Address = txtAddress.Text;
                objMaster.Phone = txtContactNo1.Text;
                objMaster.Mobile = txtContactNo2.Text;
                objMaster.Fax = txtFAXNo.Text;
                objMaster.Email = txtEmail.Text;
                objMaster.IndTypeId = ddlIndustryType.SelectedItem.Value;
                objMaster.Website = txtWebsite.Text;
                objMaster.PANNo = txtPANNo.Text;
                objMaster.ECCNo = txtECCNo.Text;
                objMaster.CSTNo = txtCSTNo.Text;
                objMaster.LocalSTNo = txtLocalSalesTaxNo.Text;
                objMaster.SplInsrs = txtSpecialInstructions.Text;
                objMaster.DesgId = ddlDesignationNo.SelectedItem.Value;
                objMaster.IsNewOrExisting = rbNewExisting.SelectedItem.Text;
                //objMaster.CorpContactPerson = txtCorpContactPerson.Text;
                //objMaster.CorpDesgId= ddlCorpDesignationNo.SelectedItem.Value;
                objMaster.CorpDesgId = "0";
                //objMaster.CorpAddress = txtCorpAddress.Text;
                //objMaster.CorpPhone = txtCorpContactNo1.Text;
                //objMaster.CorpMobile = txtCorpContactNo2.Text;
                //objMaster.CorpFax = txtCorpFaxNo.Text;
                //objMaster.CorpEmail = txtCorpEmail.Text;
                // objMaster.CPID = cp.getPresentCompanySessionValue();

                if (objMaster.CustomerMaster_Save() == "Data Saved Successfully")
                {
                    foreach (GridViewRow gvRowOtherCorp in gvOtherCorpDetails.Rows)
                    {
                        objMaster.CustCorpContactPerson = gvRowOtherCorp.Cells[2].Text;
                        objMaster.CustCorpPhone = gvRowOtherCorp.Cells[4].Text;
                        objMaster.CustCorpMobile = gvRowOtherCorp.Cells[5].Text;
                        objMaster.CustCorpFax = gvRowOtherCorp.Cells[6].Text;
                        objMaster.CustCorpEmail = gvRowOtherCorp.Cells[7].Text;
                        objMaster.CustCorpDesgId = gvRowOtherCorp.Cells[8].Text;
                        objMaster.CustUnitId = gvRowOtherCorp.Cells[10].Text;
                        objMaster.CustDetId = gvRowOtherCorp.Cells[9].Text;
                        objMaster.CustomerMasterDetails_Save();
                    }

                    //objMaster.CustomerMasterDetails_Delete(objMaster.CustId);

                    foreach (GridViewRow gvrow in gvUnitDetails.Rows)
                    {
                        string CustUnitIdForRef = gvrow.Cells[5].Text;

                        objMaster.CustUnitId = gvrow.Cells[5].Text;
                        objMaster.CustUnitName = gvrow.Cells[4].Text;
                        objMaster.CustUnitAddress = gvrow.Cells[3].Text;
                        objMaster.CustomerUnits_Save();
                        if (CustUnitIdForRef.Substring(0, 1) == "t")
                        {
                            foreach (GridViewRow gvCustRow in gvCustomerItems.Rows)
                            {
                                if (gvCustRow.Cells[10].Text == CustUnitIdForRef)
                                {
                                    gvCustRow.Cells[10].Text = objMaster.CustUnitId;
                                }
                            }
                        }
                    }

                    foreach (GridViewRow gvrow in gvCustomerItems.Rows)
                    {
                        objMaster.CustCorpContactPerson = gvrow.Cells[3].Text;
                        objMaster.CustCorpPhone = gvrow.Cells[5].Text;
                        objMaster.CustCorpMobile = gvrow.Cells[6].Text;
                        objMaster.CustCorpFax = gvrow.Cells[7].Text;
                        objMaster.CustCorpEmail = gvrow.Cells[8].Text;
                        objMaster.CustCorpDesgId = gvrow.Cells[9].Text;
                        objMaster.CustUnitId = gvrow.Cells[10].Text;
                        objMaster.CustDetId = gvrow.Cells[11].Text;
                        objMaster.CustomerMasterDetails_Save();
                    }
                    SM.CommitTransaction();
                    MessageBox.Show(this, "Data Saved Successfully");
                   // gvCustMasterDetails.DataBind();
                    //foreach (GridViewRow gvrow in gvCustMasterDetails.Rows)
                    //{
                    //    if (gvrow.Cells[8].Text == objMaster.CustId)
                    //    {
                    //        gvCustMasterDetails.SelectedIndex = gvrow.RowIndex;
                    //    }
                    //}
                    //SM.CustomerMaster objMaster = new SM.CustomerMaster();
                    //objMaster.SalesEnquiry_Save1();
                }
                else
                {
                    SM.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //gvCustMasterDetails.DataBind();
                gvCustomerItems.DataBind();
                tblCustomerDetails.Visible = false;
                //SM.ClearControls(this);
                SM.Dispose();
            }

        }
    }

    #endregion

    #region CustomerMasterUpdate
    private void CustomerMasterUpdate()
    {
        try
        {
            SM.CustomerMaster objMaster = new SM.CustomerMaster();
            SM.BeginTransaction();
            objMaster.CustId = customerCode;
            objMaster.CustCode = txtCustomerCode.Text;
            objMaster.RegId = ddlRegion.SelectedItem.Value;
            objMaster.CustName = txtCustomerName.Text;

            objMaster.CompName = txtCompanyName.Text;

            objMaster.ContactPerson = txtContactPerson.Text;
            objMaster.Address = txtAddress.Text;
            objMaster.Phone = txtContactNo1.Text;
            objMaster.Mobile = txtContactNo2.Text;
            objMaster.Fax = txtFAXNo.Text;
            objMaster.Email = txtEmail.Text;
            objMaster.IndTypeId = ddlIndustryType.SelectedItem.Value;
            objMaster.Website = txtWebsite.Text;
            objMaster.PANNo = txtPANNo.Text;
            objMaster.ECCNo = txtECCNo.Text;
            objMaster.CSTNo = txtCSTNo.Text;
            objMaster.LocalSTNo = txtLocalSalesTaxNo.Text;
            objMaster.SplInsrs = txtSpecialInstructions.Text;
            objMaster.DesgId = ddlDesignationNo.SelectedItem.Value;
            objMaster.IsNewOrExisting = rbNewExisting.SelectedItem.Text;
            //objMaster.CorpContactPerson = txtCorpContactPerson.Text;
            //objMaster.CorpDesgId = ddlCorpDesignationNo.SelectedItem.Value;
            //objMaster.CorpAddress = txtCorpAddress.Text;
            //objMaster.CorpPhone = txtCorpContactNo1.Text;
            //objMaster.CorpMobile = txtCorpContactNo2.Text;
            //objMaster.CorpFax = txtCorpFaxNo.Text;
            //objMaster.CorpEmail = txtCorpEmail.Text;
            if (objMaster.CustomerMaster_Update() == "Data Updated Successfully")
            {
                foreach (GridViewRow gvRowOtherCorp in gvOtherCorpDetails.Rows)
                {
                    objMaster.CustCorpContactPerson = gvRowOtherCorp.Cells[2].Text;
                    objMaster.CustCorpPhone = gvRowOtherCorp.Cells[4].Text;
                    objMaster.CustCorpMobile = gvRowOtherCorp.Cells[5].Text;
                    objMaster.CustCorpFax = gvRowOtherCorp.Cells[6].Text;
                    objMaster.CustCorpEmail = gvRowOtherCorp.Cells[7].Text;
                    objMaster.CustCorpDesgId = gvRowOtherCorp.Cells[8].Text;
                    objMaster.CustUnitId = gvRowOtherCorp.Cells[10].Text;
                    objMaster.CustDetId = gvRowOtherCorp.Cells[9].Text;
                    objMaster.CustomerMasterDetails_Save();
                }
                ////////objMaster.CustomerUnits_Delete(objMaster.CustId);
                foreach (GridViewRow gvrow in gvUnitDetails.Rows)
                {
                    string CustUnitIdForRef = gvrow.Cells[5].Text;

                    objMaster.CustUnitId = gvrow.Cells[5].Text;
                    objMaster.CustUnitName = gvrow.Cells[4].Text;
                    objMaster.CustUnitAddress = gvrow.Cells[3].Text;
                    objMaster.CustomerUnits_Save();
                    if (CustUnitIdForRef.Substring(0, 1) == "t")
                    {
                        foreach (GridViewRow gvCustRow in gvCustomerItems.Rows)
                        {
                            if (gvCustRow.Cells[10].Text == CustUnitIdForRef)
                            {
                                gvCustRow.Cells[10].Text = objMaster.CustUnitId;
                            }
                        }
                    }
                }
                //objMaster.CustomerMasterDetails_Delete(objMaster.CustId);
                foreach (GridViewRow gvrow in gvCustomerItems.Rows)
                {
                    objMaster.CustCorpContactPerson = gvrow.Cells[3].Text;
                    objMaster.CustCorpPhone = gvrow.Cells[5].Text;
                    objMaster.CustCorpMobile = gvrow.Cells[6].Text;
                    objMaster.CustCorpFax = gvrow.Cells[7].Text;
                    objMaster.CustCorpEmail = gvrow.Cells[8].Text;
                    objMaster.CustCorpDesgId = gvrow.Cells[9].Text;
                    objMaster.CustUnitId = gvrow.Cells[10].Text;
                    objMaster.CustDetId = gvrow.Cells[11].Text;

                    objMaster.CustomerMasterDetails_Save();
                }
                SM.CommitTransaction();
                MessageBox.Show(this, "Data Updated Successfully");
            }
            else
            {
                SM.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            btnSave.Text = "Save";
           // gvCustMasterDetails.DataBind();
            gvCustomerItems.DataBind();
            tblCustomerDetails.Visible = false;
            tblUnitDetails.Visible = false;
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region gvCustMasterDetails_RowDataBound
    protected void gvCustMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
       // btnNew_Click(sender, e);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerInformation.aspx");
    }
    #endregion

    

    #region Button Items Refresh
    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        ddlUnitName.SelectedValue = "0";
        ddlCorpoDesignationNo.SelectedValue = "0";
        txtCorpoContactPerson.Text = string.Empty;
        txtCorpoContactNo1.Text = string.Empty;
        txtCorpoContactNo2.Text = string.Empty;
        txtCorpoFaxNo.Text = string.Empty;
        txtCorpoEmail.Text = string.Empty;
        gvCustomerItems.SelectedIndex = -1;
    }
    #endregion

    #region GridView Customer Items Row DataBound
    protected void gvCustomerItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Contact from list?');");
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            if (e.Row.Cells[10].Text == "delete")
            {
                e.Row.Visible = false;
            }
        }

    }
    #endregion

    #region GridView Customer Items Items Row Deleting
    protected void gvCustomerItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvCustomerItems.Rows[e.RowIndex].Cells[1].Text;
        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("UnitName");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);

        if (gvCustomerItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustomerItems.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = CustomerProducts.NewRow();
                    dr["UnitName"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["Designation"] = gvrow.Cells[4].Text;
                    dr["ContactNo1"] = gvrow.Cells[5].Text;
                    dr["ContactNo2"] = gvrow.Cells[6].Text;
                    dr["FaxNo"] = gvrow.Cells[7].Text;
                    dr["Email"] = gvrow.Cells[8].Text;
                    dr["DesignationId"] = gvrow.Cells[9].Text;
                    dr["CustUnitId"] = gvrow.Cells[10].Text;
                    dr["CustDetId"] = gvrow.Cells[11].Text;
                    CustomerProducts.Rows.Add(dr);
                }
                else
                {
                    if (gvrow.Cells[11].Text != "-")
                    {
                        DataRow dr = CustomerProducts.NewRow();
                        dr["UnitName"] = gvrow.Cells[2].Text;
                        dr["ContactPerson"] = gvrow.Cells[3].Text;
                        dr["Designation"] = gvrow.Cells[4].Text;
                        dr["ContactNo1"] = gvrow.Cells[5].Text;
                        dr["ContactNo2"] = gvrow.Cells[6].Text;
                        dr["FaxNo"] = gvrow.Cells[7].Text;
                        dr["Email"] = gvrow.Cells[8].Text;
                        dr["DesignationId"] = gvrow.Cells[9].Text;
                        dr["CustUnitId"] = "delete";
                        dr["CustDetId"] = gvrow.Cells[11].Text;
                        CustomerProducts.Rows.Add(dr);
                    }
                }
            }
        }
        gvCustomerItems.DataSource = CustomerProducts;
        gvCustomerItems.DataBind();
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Print Button Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (customerCode!="")
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=CustomerMaster&custcode=" + customerCode + "";
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
    #endregion

    #region Button Add
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);
        if (gvCustomerItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustomerItems.Rows)
            {
                if (gvCustomerItems.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvCustomerItems.SelectedRow.RowIndex)
                    {
                        DataRow dr = CustomerProducts.NewRow();
                        dr["UnitName"] = ddlUnitName.SelectedItem.Text;
                        dr["ContactPerson"] = txtCorpoContactPerson.Text;
                        dr["Designation"] = ddlCorpoDesignationNo.SelectedItem.Text;
                        dr["ContactNo1"] = txtCorpoContactNo1.Text;
                        dr["ContactNo2"] = txtCorpoContactNo2.Text;
                        dr["FaxNo"] = txtCorpoFaxNo.Text;
                        dr["Email"] = txtCorpoEmail.Text;
                        dr["DesignationId"] = ddlCorpoDesignationNo.SelectedItem.Value;
                        dr["CustUnitId"] = ddlUnitName.SelectedItem.Value;
                        dr["CustDetId"] = gvrow.Cells[11].Text;
                        CustomerProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = CustomerProducts.NewRow();
                        dr["UnitName"] = gvrow.Cells[2].Text;
                        dr["ContactPerson"] = gvrow.Cells[3].Text;
                        dr["Designation"] = gvrow.Cells[4].Text;
                        dr["ContactNo1"] = gvrow.Cells[5].Text;
                        dr["ContactNo2"] = gvrow.Cells[6].Text;
                        dr["FaxNo"] = gvrow.Cells[7].Text;
                        dr["Email"] = gvrow.Cells[8].Text;
                        dr["DesignationId"] = gvrow.Cells[9].Text;
                        dr["CustUnitId"] = gvrow.Cells[10].Text;
                        dr["CustDetId"] = gvrow.Cells[11].Text;
                        CustomerProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = CustomerProducts.NewRow();
                    dr["UnitName"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["Designation"] = gvrow.Cells[4].Text;
                    dr["ContactNo1"] = gvrow.Cells[5].Text;
                    dr["ContactNo2"] = gvrow.Cells[6].Text;
                    dr["FaxNo"] = gvrow.Cells[7].Text;
                    dr["Email"] = gvrow.Cells[8].Text;
                    dr["DesignationId"] = gvrow.Cells[9].Text;
                    dr["CustUnitId"] = gvrow.Cells[10].Text;
                    dr["CustDetId"] = gvrow.Cells[11].Text;
                    CustomerProducts.Rows.Add(dr);
                }
            }
        }

        ////if (gvCustomerItems.Rows.Count > 0)
        ////{
        ////    foreach (GridViewRow gvrow in gvCustomerItems.Rows)
        ////    {
        ////        if (gvrow.Cells[2].Text == txtCorpoContactPerson.Text)
        ////        {
        ////            gvCustomerItems.DataSource = CustomerProducts;
        ////            gvCustomerItems.DataBind();
        ////            MessageBox.Show(this, "The Contact Person you have selected is already exists in list");
        ////            return;
        ////        }

        ////    }
        ////}
        if (gvCustomerItems.SelectedIndex == -1)
        {
            DataRow drnew = CustomerProducts.NewRow();
            drnew["UnitName"] = ddlUnitName.SelectedItem.Text;
            drnew["ContactPerson"] = txtCorpoContactPerson.Text;
            drnew["Designation"] = ddlCorpoDesignationNo.SelectedItem.Text;
            drnew["ContactNo1"] = txtCorpoContactNo1.Text;
            drnew["ContactNo2"] = txtCorpoContactNo2.Text;
            drnew["FaxNo"] = txtCorpoFaxNo.Text;
            drnew["Email"] = txtCorpoEmail.Text;
            drnew["DesignationId"] = ddlCorpoDesignationNo.SelectedItem.Value;
            drnew["CustUnitId"] = ddlUnitName.SelectedItem.Value;
            drnew["CustDetId"] = "-";
            CustomerProducts.Rows.Add(drnew);
        }
        gvCustomerItems.DataSource = CustomerProducts;
        gvCustomerItems.DataBind();
        gvCustomerItems.SelectedIndex = -1;
        btnItemRefresh_Click(sender, e);
    }
    #endregion

    #region Button ADD UNITS Click
    protected void btnAddUnits_Click(object sender, EventArgs e)
    {
        tdUnitDetailsHead.Visible = true;
        tblUnitDetails.Visible = true;
    }
    #endregion

    #region gvUnitDetails_RowDataBound
    protected void gvUnitDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Unit from list?');");
            if (e.Row.Cells[4].Text == "delete")
            {
                e.Row.Visible = false;
            }
            if (e.Row.Cells[5].Text == "-")
            {
                e.Row.Cells[5].Text = "temp" + e.Row.RowIndex;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            UnitName_Fill();
        }
    }
    #endregion

    #region gvUnitDetails_RowDeleting
    protected void gvUnitDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvUnitDetails.Rows[e.RowIndex].Cells[4].Text;
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        //col = new DataColumn("Unitno");
        //CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitAddress");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("custunitid");
        CustomerUnits.Columns.Add(col);

        if (gvUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvUnitDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["UnitName"] = gvrow.Cells[4].Text;
                    dr["UnitAddress"] = gvrow.Cells[3].Text;
                    dr["custunitid"] = gvrow.Cells[5].Text;
                    CustomerUnits.Rows.Add(dr);
                }
                else
                {
                    if (gvrow.Cells[5].Text.Substring(0, 1) != "t")
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["UnitName"] = "delete";
                        dr["UnitAddress"] = gvrow.Cells[3].Text;
                        dr["custunitid"] = gvrow.Cells[5].Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                }
            }
        }
        gvUnitDetails.DataSource = CustomerUnits;
        gvUnitDetails.DataBind();
        if (gvUnitDetails.Rows.Count > 0) { tblContactDetails.Visible = true; tdContactDetailsHead.Visible = true; } else { tblContactDetails.Visible = false; tdContactDetailsHead.Visible = false; }
        foreach (GridViewRow gvRowUnits in gvUnitDetails.Rows)
        {
            if (gvRowUnits.Cells[4].Text == "delete")
            {
                foreach (GridViewRow gvRowCustDet in gvCustomerItems.Rows)
                {
                    if (gvRowUnits.Cells[5].Text == gvRowCustDet.Cells[10].Text)
                    {
                        gvRowCustDet.Cells[10].Text = "delete";
                        gvRowCustDet.Visible = false;
                    }
                }
            }
        }
        btnUnitsRefresh_Click(sender, e);
    }
    #endregion

    #region Button UNITS ADD Click
    protected void btnUnitsAdd_Click(object sender, EventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        //col = new DataColumn("Unitno");
        //CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitAddress");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("custunitid");
        CustomerUnits.Columns.Add(col);

        if (gvUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvUnitDetails.Rows)
            {
                if (gvUnitDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvUnitDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["UnitName"] = txtUnitName.Text;
                        dr["UnitAddress"] = txtUnitAddress.Text;
                        dr["custunitid"] = gvrow.Cells[5].Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["UnitName"] = gvrow.Cells[4].Text;
                        dr["UnitAddress"] = gvrow.Cells[3].Text;
                        dr["custunitid"] = gvrow.Cells[5].Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["UnitName"] = gvrow.Cells[4].Text;
                    dr["UnitAddress"] = gvrow.Cells[3].Text;
                    dr["custunitid"] = gvrow.Cells[5].Text;
                    CustomerUnits.Rows.Add(dr);
                }
            }
        }

        if (gvUnitDetails.Rows.Count > 0)
        {
            if (gvUnitDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvUnitDetails.Rows)
                {
                    if (gvrow.Cells[4].Text == txtUnitName.Text)
                    {
                        gvUnitDetails.DataSource = CustomerUnits;
                        gvUnitDetails.DataBind();
                        MessageBox.Show(this, "The Unit Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvUnitDetails.SelectedIndex == -1)
        {
            DataRow drnew = CustomerUnits.NewRow();
            drnew["UnitName"] = txtUnitName.Text;
            drnew["UnitAddress"] = txtUnitAddress.Text;
            drnew["custunitid"] = "-";
            CustomerUnits.Rows.Add(drnew);
        }
        gvUnitDetails.DataSource = CustomerUnits;
        gvUnitDetails.DataBind();
        if (gvUnitDetails.Rows.Count > 0)
        { 
            tblContactDetails.Visible = true; 
            tdContactDetailsHead.Visible = true; 
        } 
        else 
        { 
            tblContactDetails.Visible = false; 
            tdContactDetailsHead.Visible = false; 
        }
        gvUnitDetails.SelectedIndex = -1;
        btnUnitsRefresh_Click(sender, e);
    }
    #endregion

    #region Button UNITS REFRESH Click
    protected void btnUnitsRefresh_Click(object sender, EventArgs e)
    {
        txtUnitName.Text = string.Empty;
        txtUnitAddress.Text = string.Empty;
        gvUnitDetails.SelectedIndex = -1;
    }
    #endregion

    #region gvUnitDetails_RowEditing
    protected void gvUnitDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("UnitAddress");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("custunitid");
        CustomerUnits.Columns.Add(col);

        if (gvUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvUnitDetails.Rows)
            {
                DataRow dr = CustomerUnits.NewRow();
                dr["UnitName"] = gvrow.Cells[4].Text;
                dr["UnitAddress"] = gvrow.Cells[3].Text;
                dr["custunitid"] = gvrow.Cells[5].Text;
                CustomerUnits.Rows.Add(dr);
                if (gvrow.RowIndex == gvUnitDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    txtUnitName.Text = gvrow.Cells[4].Text;
                    txtUnitAddress.Text = gvrow.Cells[3].Text;
                    gvUnitDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvUnitDetails.DataSource = CustomerUnits;
        gvUnitDetails.DataBind();
    }
    #endregion

    #region gvCustomerItems_RowEditing
    protected void gvCustomerItems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);
        if (gvCustomerItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustomerItems.Rows)
            {
                DataRow dr = CustomerProducts.NewRow();
                dr["UnitName"] = gvrow.Cells[2].Text;
                dr["ContactPerson"] = gvrow.Cells[3].Text;
                dr["Designation"] = gvrow.Cells[4].Text;
                dr["ContactNo1"] = gvrow.Cells[5].Text;
                dr["ContactNo2"] = gvrow.Cells[6].Text;
                dr["FaxNo"] = gvrow.Cells[7].Text;
                dr["Email"] = gvrow.Cells[8].Text;
                dr["DesignationId"] = gvrow.Cells[9].Text;
                dr["CustUnitId"] = gvrow.Cells[10].Text;
                dr["CustDetId"] = gvrow.Cells[11].Text;
                CustomerProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvCustomerItems.Rows[e.NewEditIndex].RowIndex)
                {
                    txtCorpoContactPerson.Text = gvrow.Cells[3].Text;
                    txtCorpoContactNo1.Text = gvrow.Cells[5].Text;
                    txtCorpoContactNo2.Text = gvrow.Cells[6].Text;
                    txtCorpoFaxNo.Text = gvrow.Cells[7].Text;
                    txtCorpoEmail.Text = gvrow.Cells[8].Text;
                    ddlCorpoDesignationNo.SelectedValue = gvrow.Cells[9].Text;
                    ddlUnitName.SelectedValue = gvrow.Cells[10].Text;
                    gvCustomerItems.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvCustomerItems.DataSource = CustomerProducts;
        gvCustomerItems.DataBind();
    }
    #endregion

    #region Button Other Corporate Details ADD Click
    protected void btnOtherCorpDetailsAdd_Click(object sender, EventArgs e)
    {
        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);

        if (gvOtherCorpDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOtherCorpDetails.Rows)
            {
                if (gvOtherCorpDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvOtherCorpDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = CustomerProducts.NewRow();
                        dr["ContactPerson"] = txtOtherCorpContactName.Text;
                        dr["Designation"] = ddlOtherCorpDesignation.SelectedItem.Text;
                        dr["ContactNo1"] = txtOtherCorpPhoneNo.Text;
                        dr["ContactNo2"] = txtOtherCorpMobileNo.Text;
                        dr["FaxNo"] = txtOtherCorpFaxNo.Text;
                        dr["Email"] = txtOtherCorpEmail.Text;
                        dr["DesignationId"] = ddlOtherCorpDesignation.SelectedItem.Value;
                        dr["CustDetId"] = gvrow.Cells[9].Text;
                        dr["CustUnitId"] = gvrow.Cells[10].Text;
                        CustomerProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = CustomerProducts.NewRow();
                        dr["ContactPerson"] = gvrow.Cells[2].Text;
                        dr["Designation"] = gvrow.Cells[3].Text;
                        dr["ContactNo1"] = gvrow.Cells[4].Text;
                        dr["ContactNo2"] = gvrow.Cells[5].Text;
                        dr["FaxNo"] = gvrow.Cells[6].Text;
                        dr["Email"] = gvrow.Cells[7].Text;
                        dr["DesignationId"] = gvrow.Cells[8].Text;
                        dr["CustDetId"] = gvrow.Cells[9].Text;
                        dr["CustUnitId"] = gvrow.Cells[10].Text;
                        CustomerProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = CustomerProducts.NewRow();
                    dr["ContactPerson"] = gvrow.Cells[2].Text;
                    dr["Designation"] = gvrow.Cells[3].Text;
                    dr["ContactNo1"] = gvrow.Cells[4].Text;
                    dr["ContactNo2"] = gvrow.Cells[5].Text;
                    dr["FaxNo"] = gvrow.Cells[6].Text;
                    dr["Email"] = gvrow.Cells[7].Text;
                    dr["DesignationId"] = gvrow.Cells[8].Text;
                    dr["CustDetId"] = gvrow.Cells[9].Text;
                    dr["CustUnitId"] = gvrow.Cells[10].Text;
                    CustomerProducts.Rows.Add(dr);
                }
            }
        }

        if (gvOtherCorpDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOtherCorpDetails.Rows)
            {
                if (gvrow.Cells[2].Text == txtCorpoContactPerson.Text)
                {
                    gvOtherCorpDetails.DataSource = CustomerProducts;
                    gvOtherCorpDetails.DataBind();
                    MessageBox.Show(this, "The Contact Person you have selected is already exists in list");
                    return;
                }

            }
        }
        if (gvOtherCorpDetails.SelectedIndex == -1)
        {
            DataRow drnew = CustomerProducts.NewRow();
            drnew["ContactPerson"] = txtOtherCorpContactName.Text;
            drnew["Designation"] = ddlOtherCorpDesignation.SelectedItem.Text;
            drnew["ContactNo1"] = txtOtherCorpPhoneNo.Text;
            drnew["ContactNo2"] = txtOtherCorpMobileNo.Text;
            drnew["FaxNo"] = txtOtherCorpFaxNo.Text;
            drnew["Email"] = txtOtherCorpEmail.Text;
            drnew["DesignationId"] = ddlOtherCorpDesignation.SelectedItem.Value;
            drnew["CustDetId"] = "-";
            drnew["CustUnitId"] = "0";
            CustomerProducts.Rows.Add(drnew);
        }

        gvOtherCorpDetails.DataSource = CustomerProducts;
        gvOtherCorpDetails.DataBind();
        gvOtherCorpDetails.SelectedIndex = -1;
        btnOtherCorpDetailsRefresh_Click(sender, e);
    }

    #endregion

    #region Button Other Corporate Details Refresh Click
    protected void btnOtherCorpDetailsRefresh_Click(object sender, EventArgs e)
    {
        txtOtherCorpContactName.Text = txtOtherCorpPhoneNo.Text = txtOtherCorpMobileNo.Text = txtOtherCorpFaxNo.Text = txtOtherCorpEmail.Text = "";
        ddlOtherCorpDesignation.SelectedValue = "0";
        gvOtherCorpDetails.SelectedIndex = -1;
    }
    #endregion

    #region gvOtherCorpDetails_RowDataBound
    protected void gvOtherCorpDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (btnSave.Enabled == false)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Contact from list?');");
            if (e.Row.Cells[10].Text == "delete")
            {
                e.Row.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
    }
    #endregion

    #region gvOtherCorpDetails_RowDeleting
    protected void gvOtherCorpDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvOtherCorpDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);

        if (gvOtherCorpDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOtherCorpDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = CustomerProducts.NewRow();
                    dr["ContactPerson"] = gvrow.Cells[2].Text;
                    dr["Designation"] = gvrow.Cells[3].Text;
                    dr["ContactNo1"] = gvrow.Cells[4].Text;
                    dr["ContactNo2"] = gvrow.Cells[5].Text;
                    dr["FaxNo"] = gvrow.Cells[6].Text;
                    dr["Email"] = gvrow.Cells[7].Text;
                    dr["DesignationId"] = gvrow.Cells[8].Text;
                    dr["CustDetId"] = gvrow.Cells[9].Text;
                    dr["CustUnitId"] = gvrow.Cells[10].Text;
                    CustomerProducts.Rows.Add(dr);
                }
                else
                {
                    if (gvrow.Cells[9].Text != "-")
                    {
                        DataRow dr = CustomerProducts.NewRow();
                        dr["ContactPerson"] = gvrow.Cells[2].Text;
                        dr["Designation"] = gvrow.Cells[3].Text;
                        dr["ContactNo1"] = gvrow.Cells[4].Text;
                        dr["ContactNo2"] = gvrow.Cells[5].Text;
                        dr["FaxNo"] = gvrow.Cells[6].Text;
                        dr["Email"] = gvrow.Cells[7].Text;
                        dr["DesignationId"] = gvrow.Cells[8].Text;
                        dr["CustDetId"] = gvrow.Cells[9].Text;
                        dr["CustUnitId"] = "delete";
                        CustomerProducts.Rows.Add(dr);
                    }
                }
            }
        }
        gvOtherCorpDetails.DataSource = CustomerProducts;
        gvOtherCorpDetails.DataBind();
        btnOtherCorpDetailsRefresh_Click(sender, e);
    }
    #endregion

    #region gvOtherCorpDetails_RowEditing
    protected void gvOtherCorpDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);

        if (gvOtherCorpDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvOtherCorpDetails.Rows)
            {
                DataRow dr = CustomerProducts.NewRow();
                dr["ContactPerson"] = gvrow.Cells[2].Text;
                dr["Designation"] = gvrow.Cells[3].Text;
                dr["ContactNo1"] = gvrow.Cells[4].Text;
                dr["ContactNo2"] = gvrow.Cells[5].Text;
                dr["FaxNo"] = gvrow.Cells[6].Text;
                dr["Email"] = gvrow.Cells[7].Text;
                dr["DesignationId"] = gvrow.Cells[8].Text;
                dr["CustDetId"] = gvrow.Cells[9].Text;
                dr["CustUnitId"] = gvrow.Cells[10].Text;
                CustomerProducts.Rows.Add(dr);

                if (gvrow.RowIndex == gvOtherCorpDetails.Rows[e.NewEditIndex].RowIndex)
                {
                    txtOtherCorpContactName.Text = gvrow.Cells[2].Text;
                    txtOtherCorpPhoneNo.Text = gvrow.Cells[4].Text;
                    txtOtherCorpMobileNo.Text = gvrow.Cells[5].Text;
                    txtOtherCorpFaxNo.Text = gvrow.Cells[6].Text;
                    txtOtherCorpEmail.Text = gvrow.Cells[7].Text;
                    ddlOtherCorpDesignation.SelectedValue = gvrow.Cells[8].Text;
                    gvOtherCorpDetails.SelectedIndex = gvrow.RowIndex;
                }
            }
        }

        gvOtherCorpDetails.DataSource = CustomerProducts;
        gvOtherCorpDetails.DataBind();

    }
    #endregion
    #region Link Button Unit Name Click
    protected void lbtnUnitName_Click(object sender, EventArgs e)
    {
        if (btnSave.Enabled == true)
        { return; }
        LinkButton lbtnUnitName;
        lbtnUnitName = (LinkButton)sender;
        GridViewRow gvRowUnit = (GridViewRow)lbtnUnitName.Parent.Parent;
        gvUnitDetails.SelectedIndex = gvRowUnit.RowIndex;


        DataTable CustomerProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("UnitName");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactPerson");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Designation");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Address");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo1");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("ContactNo2");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("FaxNo");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("DesignationId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustUnitId");
        CustomerProducts.Columns.Add(col);
        col = new DataColumn("CustDetId");
        CustomerProducts.Columns.Add(col);

        if (gvCustomerItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustomerItems.Rows)
            {
                if (gvrow.Cells[9].Text == "-")
                {
                    DataRow dr = CustomerProducts.NewRow();
                    dr["UnitName"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["Designation"] = gvrow.Cells[4].Text;
                    dr["ContactNo1"] = gvrow.Cells[5].Text;
                    dr["ContactNo2"] = gvrow.Cells[6].Text;
                    dr["FaxNo"] = gvrow.Cells[7].Text;
                    dr["Email"] = gvrow.Cells[8].Text;
                    dr["DesignationId"] = gvrow.Cells[9].Text;
                    dr["CustUnitId"] = gvrow.Cells[10].Text;
                    dr["CustDetId"] = gvrow.Cells[11].Text;

                    CustomerProducts.Rows.Add(dr);
                }
            }
        }
        if (gvRowUnit.Cells[5].Text != "-")
        {
            SM.CustomerMaster objCust = new SM.CustomerMaster();
            objCust.CustomerMasterDetails_Select(customerCode, gvRowUnit.Cells[5].Text, gvCustomerItems);
        }
        else
        {
            gvCustomerItems.DataBind();
        }
        if (gvCustomerItems.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustomerItems.Rows)
            {
                if (gvrow.Cells[9].Text != "-")
                {
                    DataRow dr = CustomerProducts.NewRow();
                    dr["UnitName"] = gvrow.Cells[2].Text;
                    dr["ContactPerson"] = gvrow.Cells[3].Text;
                    dr["Designation"] = gvrow.Cells[4].Text;
                    dr["ContactNo1"] = gvrow.Cells[5].Text;
                    dr["ContactNo2"] = gvrow.Cells[6].Text;
                    dr["FaxNo"] = gvrow.Cells[7].Text;
                    dr["Email"] = gvrow.Cells[8].Text;
                    dr["DesignationId"] = gvrow.Cells[9].Text;
                    dr["CustUnitId"] = gvrow.Cells[10].Text;
                    dr["CustDetId"] = gvrow.Cells[11].Text;
                    CustomerProducts.Rows.Add(dr);
                }
            }
        }
        gvCustomerItems.DataSource = CustomerProducts;
        gvCustomerItems.DataBind();


    }
    #endregion

    #region Button Other Corporate Details Click
    protected void btnOtherCorpDetails_Click(object sender, EventArgs e)
    {
        tdOtherCorpDetailsHead.Visible = tblOtherCorpDetails.Visible = true;
    }
    #endregion

    #region SalesLead
    protected void btnSalesLead_Click(object sender, EventArgs e)
    {

        if (customerCode!="")
        {
            Response.Redirect("SalesEnquiry.aspx?Cid=" + customerCode + "");
        }
        else
        {
            MessageBox.Show(this, "Please Select atleast a Record ");
        }

    }
    #endregion

    #region btnConfirmNo
    protected void btnConfirmNo_Click(object sender, EventArgs e)
    {
        tblPopUp1.Visible = false;
        ModalPopupExtender.Hide();
        ModalPopupExtender.Controls.Clear();
        lblData.Text = "";
        lblData1.Text = "";
        lblData2.Text = "";
        lblData3.Text = "";
        lblData4.Text = "";
        lblData5.Text = "";
        ModalPopupExtender1.Controls.Clear();
        GridView1.Visible = false;
        gvQuotationDetails.Visible = false;
        gvSalesEnquiry.Visible = false;
        #region Str1
        if (str1 != "")
        {

            if (StatusStr1 == "Open" || StatusStr1 == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + str1);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + str1);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;
                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + str1);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            tblpopup3.Visible = true;
            ModalPopupExtender1.Show();
        }
        #endregion
        #region Str2
        else if (str2 != "")
        {

            if (StatusStr2 == "Open" || StatusStr2 == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + str2);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + str2);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();

                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;
                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + str2);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            tblpopup3.Visible = true;
            ModalPopupExtender1.Show();
        }
        #endregion
        #region Str3
        else if (str3 != "")
        {

            if (StatusStr3 == "Open" || StatusStr3 == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + str3);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + str3);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;
                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + str3);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            tblpopup3.Visible = true;
            ModalPopupExtender1.Show();
        }
        #endregion
        #region Str4
        else if (str4 != "")
        {

            if (StatusStr4 == "Open" || StatusStr4 == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + str4);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + str4);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;

                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + str4);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            tblpopup3.Visible = true;
            ModalPopupExtender1.Show();
        }
        #endregion
        #region Str5
        else if (str5 != "")
        {

            if (StatusStr1 == "Open" || StatusStr1 == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + str5);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + str5);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;
                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + str5);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            tblpopup3.Visible = true;
            ModalPopupExtender1.Show();
        }
        #endregion
        #region Str6
        else if (str6 != "")
        {

            if (StatusStr6 == "Open" || StatusStr6 == "Close")
            {
                dbManager.Open();
                _commandText = string.Format("SELECT * FROM [YANTRA_ENQ_MAST],[YANTRA_CUSTOMER_MAST] WHERE YANTRA_ENQ_MAST.CUST_ID =YANTRA_CUSTOMER_MAST.CUST_ID and YANTRA_ENQ_MAST.CUST_ID=" + str6);
                SqlDataAdapter da = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds = new DataSet();
                da.Fill(ds, "enq");
                gvSalesEnquiry.DataSource = ds;
                gvSalesEnquiry.DataBind();
                gvSalesEnquiry.Visible = true;
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT ENQ_ID FROM [YANTRA_ENQ_MAST] WHERE CUST_ID =" + str6);
                dbManager.ExecuteReader(CommandType.Text, _commandText);
                string s = "";
                if (dbManager.DataReader.Read())
                {

                    s = dbManager.DataReader["ENQ_ID"].ToString();


                }
                dbManager.DataReader.Close();
                dbManager.Open();
                _commandText = string.Format("SELECT * ,[YANTRA_QUOT_MAST].QUOT_NO+' '+[YANTRA_QUOT_MAST].QUOT_REVISED_KEY AS QUOTNO,FORPREPARED.EMP_FIRST_NAME AS PREPAREDBY,FORAPPROVED.EMP_FIRST_NAME AS APPROVEDBY FROM [YANTRA_QUOT_MAST]	 inner join [YANTRA_ENQ_MAST] on [YANTRA_ENQ_MAST].ENQ_ID=[YANTRA_QUOT_MAST].ENQ_ID inner join [YANTRA_CUSTOMER_MAST] on [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID inner join [YANTRA_LKUP_ENQ_MODE] on [YANTRA_ENQ_MAST].ENQM_ID=[YANTRA_LKUP_ENQ_MODE].ENQM_ID left outer join [YANTRA_ENQ_ASSIGN_TASKS] on YANTRA_ENQ_ASSIGN_TASKS.ENQ_ID = YANTRA_ENQ_MAST.ENQ_ID left outer join [YANTRA_EMPLOYEE_MAST] FORPREPARED on FORPREPARED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_PREPARED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORAPPROVED on FORAPPROVED.EMP_ID=[YANTRA_QUOT_MAST].QUOT_APPROVED_BY left outer join [YANTRA_EMPLOYEE_MAST] FORASSIGN on FORASSIGN.EMP_ID=[YANTRA_ENQ_ASSIGN_TASKS].EMP_ID WHERE [YANTRA_QUOT_MAST].ENQ_ID =" + s);
                SqlDataAdapter da1 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "enq1");
                gvQuotationDetails.DataSource = ds1;
                gvQuotationDetails.DataBind();
                gvQuotationDetails.Visible = true;
                dbManager.DataReader.Close();
            }
            dbManager.Open();
            _commandText = string.Format("SELECT * FROM [YANTRA_CUSTOMER_MAST] WHERE CUST_ID =" + str6);
            SqlDataAdapter da2 = new SqlDataAdapter(_commandText, DBConString.ConnectionString());
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "enq2");
            GridView1.DataSource = ds2;
            GridView1.DataBind();
            GridView1.Visible = true;
            tblpopup3.Visible = true;
            ModalPopupExtender1.Show();
        }
        #endregion
        str1 = "";
        str2 = "";
        str3 = "";
        str4 = "";
        str5 = "";
        str6 = "";
    }
    #endregion

    #region btnYesClick
    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster objMaster = new SM.CustomerMaster();
            SM.BeginTransaction();
            objMaster.CustCode = txtCustomerCode.Text;
            objMaster.RegId = ddlRegion.SelectedItem.Value;
            objMaster.CustName = txtCustomerName.Text;

            objMaster.CompName = txtCompanyName.Text;

            objMaster.ContactPerson = txtContactPerson.Text;
            objMaster.Address = txtAddress.Text;
            objMaster.Phone = txtContactNo1.Text;
            objMaster.Mobile = txtContactNo2.Text;
            objMaster.Fax = txtFAXNo.Text;
            objMaster.Email = txtEmail.Text;
            objMaster.IndTypeId = ddlIndustryType.SelectedItem.Value;
            objMaster.Website = txtWebsite.Text;
            objMaster.PANNo = txtPANNo.Text;
            objMaster.ECCNo = txtECCNo.Text;
            objMaster.CSTNo = txtCSTNo.Text;
            objMaster.LocalSTNo = txtLocalSalesTaxNo.Text;
            objMaster.SplInsrs = txtSpecialInstructions.Text;
            objMaster.DesgId = ddlDesignationNo.SelectedItem.Value;
            objMaster.IsNewOrExisting = rbNewExisting.SelectedItem.Text;
            //objMaster.CorpContactPerson = txtCorpContactPerson.Text;
            //objMaster.CorpDesgId= ddlCorpDesignationNo.SelectedItem.Value;
            objMaster.CorpDesgId = "0";
            //objMaster.CorpAddress = txtCorpAddress.Text;
            //objMaster.CorpPhone = txtCorpContactNo1.Text;
            //objMaster.CorpMobile = txtCorpContactNo2.Text;
            //objMaster.CorpFax = txtCorpFaxNo.Text;
            //objMaster.CorpEmail = txtCorpEmail.Text;
            //   objMaster.CPID = cp.getPresentCompanySessionValue();
            if (objMaster.CustomerMaster_Save() == "Data Saved Successfully")
            {
                foreach (GridViewRow gvRowOtherCorp in gvOtherCorpDetails.Rows)
                {
                    objMaster.CustCorpContactPerson = gvRowOtherCorp.Cells[2].Text;
                    objMaster.CustCorpPhone = gvRowOtherCorp.Cells[4].Text;
                    objMaster.CustCorpMobile = gvRowOtherCorp.Cells[5].Text;
                    objMaster.CustCorpFax = gvRowOtherCorp.Cells[6].Text;
                    objMaster.CustCorpEmail = gvRowOtherCorp.Cells[7].Text;
                    objMaster.CustCorpDesgId = gvRowOtherCorp.Cells[8].Text;
                    objMaster.CustUnitId = gvRowOtherCorp.Cells[10].Text;
                    objMaster.CustDetId = gvRowOtherCorp.Cells[9].Text;
                    objMaster.CustomerMasterDetails_Save();
                }

                //objMaster.CustomerMasterDetails_Delete(objMaster.CustId);

                foreach (GridViewRow gvrow in gvUnitDetails.Rows)
                {
                    string CustUnitIdForRef = gvrow.Cells[5].Text;

                    objMaster.CustUnitId = gvrow.Cells[5].Text;
                    objMaster.CustUnitName = gvrow.Cells[4].Text;
                    objMaster.CustUnitAddress = gvrow.Cells[3].Text;
                    objMaster.CustomerUnits_Save();
                    if (CustUnitIdForRef.Substring(0, 1) == "t")
                    {
                        foreach (GridViewRow gvCustRow in gvCustomerItems.Rows)
                        {
                            if (gvCustRow.Cells[10].Text == CustUnitIdForRef)
                            {
                                gvCustRow.Cells[10].Text = objMaster.CustUnitId;
                            }
                        }
                    }
                }

                foreach (GridViewRow gvrow in gvCustomerItems.Rows)
                {
                    objMaster.CustCorpContactPerson = gvrow.Cells[3].Text;
                    objMaster.CustCorpPhone = gvrow.Cells[5].Text;
                    objMaster.CustCorpMobile = gvrow.Cells[6].Text;
                    objMaster.CustCorpFax = gvrow.Cells[7].Text;
                    objMaster.CustCorpEmail = gvrow.Cells[8].Text;
                    objMaster.CustCorpDesgId = gvrow.Cells[9].Text;
                    objMaster.CustUnitId = gvrow.Cells[10].Text;
                    objMaster.CustDetId = gvrow.Cells[11].Text;
                    objMaster.CustomerMasterDetails_Save();
                }
                SM.CommitTransaction();
                MessageBox.Show(this, "Data Saved Successfully");
                ModalPopupExtender.Hide();
                tblPopUp1.Visible = false;
               // gvCustMasterDetails.DataBind();

                //foreach (GridViewRow gvrow in gvCustMasterDetails.Rows)
                //{
                //    if (gvrow.Cells[8].Text == objMaster.CustId)
                //    {
                //        gvCustMasterDetails.SelectedIndex = gvrow.RowIndex;
                //    }
                //}
                //SM.CustomerMaster objMaster = new SM.CustomerMaster();
                //objMaster.SalesEnquiry_Save1();
            }
            else
            {
                SM.RollBackTransaction();
            }
        }
        catch (Exception ex)
        {
            SM.RollBackTransaction();
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
           // gvCustMasterDetails.DataBind();
            gvCustomerItems.DataBind();
            //tblCustomerDetails.Visible = false;
            //SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region PopUplbtnCustomer
    protected void lbtnCustomer_Click(object sender, EventArgs e)
    {

        LinkButton lbtnCustMaster;
        lbtnCustMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCustMaster.Parent.Parent;
        GridView1.SelectedIndex = gvRow.RowIndex;
        tblpopup3.Visible = true;
        tblPopUp1.Visible = false;
        ModalPopupExtender1.Show();
        //     if (GridView1.SelectedIndex > -1)
        //    {
        //    Response.Redirect("CustomerInformation.aspx?CustId=" +GridView1.SelectedRow.Cells[7].Text);
        //}
        ////else
        //{

        //    MessageBox.Show(this, "Please Select atleast a Record ");

        //}
    }
    #endregion

    #region RoWdataGridview1
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }
    #endregion

    #region lbtnSalesEnq_Click
    protected void lbtnSalesEnq_Click(object sender, EventArgs e)
    {
        LinkButton lbtnSales;
        lbtnSales = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSales.Parent.Parent;
        gvSalesEnquiry.SelectedIndex = gvRow.RowIndex;
        if (gvSalesEnquiry.SelectedIndex > -1)
        {
            Response.Redirect("SalesEnquiry.aspx?EnqNo=" + gvSalesEnquiry.SelectedRow.Cells[1].Text);
        }

    }
    #endregion

    #region lbtnQuotation_Click
    protected void lbtnQuotation_Click(object sender, EventArgs e)
    {
        LinkButton lbtnCustMaster;
        lbtnCustMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCustMaster.Parent.Parent;
        gvQuotationDetails.SelectedIndex = gvRow.RowIndex;
        if (gvQuotationDetails.SelectedIndex > -1)
        {
            Response.Redirect("SalesQuotation.aspx?QtoNo=" + gvQuotationDetails.SelectedRow.Cells[1].Text);
        }
    }
    #endregion

    #region gvSalesEnquiry_RowDataBound
    protected void gvSalesEnquiry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
        }

    }
    #endregion

    #region gvQuotationDetails_RowDataBound
    protected void gvQuotationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
        }
    }
    #endregion

    #region btnSalesLeadPopUp_Click
    protected void btnSalesLeadPopUp_Click(object sender, EventArgs e)
    {
        if (GridView1.SelectedIndex > -1)
        {
            Response.Redirect("SalesEnquiry.aspx?Cid=" + GridView1.SelectedRow.Cells[7].Text + "");
        }
        else
        {

            MessageBox.Show(this, "Please Select atleast a Record ");

        }
    }
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        tblpopup3.Visible = false;

    }


}
 
