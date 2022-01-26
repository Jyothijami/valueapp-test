//Date Written      Written By

//28/03/2009        L.Hima Kishore             


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


public partial class Modules_SM_AgentMaster : System.Web.UI.Page
{

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //ItemName_Fill();
           // ItemTypes_Fill();
        }
    }
    #endregion

    
    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            AgentMasterSave();
            tblAgentDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            AgentMasterUpdate();
            tblAgentDetails.Visible = false;
        }
        gvAgentDetails.SelectedIndex = -1;
    }
    #endregion


    #region AgentMaster Save
    private void AgentMasterSave()
    {
        try
        {
            SM.AgentMaster objSMAgent = new SM.AgentMaster();
           
            objSMAgent.AgentName = txtAgentName.Text;
            objSMAgent.AgentContactPerson = txtContactPerson.Text;
            objSMAgent.AgentAddress = txtAddress.Text;
            objSMAgent.AgentContactPersonDetails = txtContactPersonDetails.Text;
            objSMAgent.AgentPhone = txtContactNo1.Text;
            objSMAgent.AgentMobile = txtContactNo2.Text;
            objSMAgent.AgentEmail = txtEmail.Text;
            objSMAgent.AgentFaxNo = txtFaxNo.Text;


            MessageBox.Show(this, objSMAgent.AgentMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvAgentDetails.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region AgentUpdate
    private void AgentMasterUpdate()
    {
        try
        {
           
            
            SM.AgentMaster objSMAgent = new SM.AgentMaster();

            objSMAgent.AgentId = gvAgentDetails.SelectedRow.Cells[1].Text;
            objSMAgent.AgentName = txtAgentName.Text;
            objSMAgent.AgentContactPerson = txtContactPerson.Text;
            objSMAgent.AgentAddress = txtAddress.Text;
            objSMAgent.AgentContactPersonDetails = txtContactPersonDetails.Text;
            objSMAgent.AgentPhone = txtContactNo1.Text;
            objSMAgent.AgentMobile = txtContactNo2.Text;
            objSMAgent.AgentEmail = txtEmail.Text;
            objSMAgent.AgentFaxNo = txtFaxNo.Text;


            MessageBox.Show(this, objSMAgent.AgentMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvAgentDetails.DataBind();
            SM.ClearControls(this);
            SM.Dispose();
        }
    }
    #endregion

    #region gvAgentMasterDetails_RowDataBound
    protected void gvAgentMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button AgentName_Click
    protected void lbtnAgentName_Click(object sender, EventArgs e)
    {
        tblAgentDetails.Visible = false;
        LinkButton lbtnAgentName;
        lbtnAgentName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnAgentName.Parent.Parent;
        gvAgentDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");



        try
        {
            SM.AgentMaster objSMAgent = new SM.AgentMaster();
            if (objSMAgent.AgentMaster_Select(gvAgentDetails.SelectedRow.Cells[1].Text) > 0)
            {
                tblAgentDetails.Visible = true;
                btnRefresh.Visible = true;
                btnSave.Visible = true;
                btnClose.Visible = true;
                btnSave.Text = "Update";
                btnSave.Enabled = false;

                txtAgentName.Text = objSMAgent.AgentName;
                txtContactPerson.Text = objSMAgent.AgentContactPerson; ;
                txtAddress.Text = objSMAgent.AgentAddress; ;
                txtContactPersonDetails.Text = objSMAgent.AgentContactPersonDetails;
                txtContactNo1.Text = objSMAgent.AgentPhone;
                txtContactNo2.Text = objSMAgent.AgentMobile;
                txtEmail.Text = objSMAgent.AgentEmail;
                txtFaxNo.Text = objSMAgent.AgentFaxNo;
             
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

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvAgentDetails.SelectedIndex > -1)
        {
            try
            {
                SM.AgentMaster objSMAgent = new SM.AgentMaster();

                if (objSMAgent.AgentMaster_Select(gvAgentDetails.SelectedRow.Cells[1].Text) > 0)
                {
                    tblAgentDetails.Visible = true;
                    btnRefresh.Visible = true;
                    btnSave.Visible = true;
                    btnClose.Visible = true;
                    btnSave.Text = "Update";
                    btnSave.Enabled = true;

                    txtAgentName.Text = objSMAgent.AgentName;
                    txtContactPerson.Text = objSMAgent.AgentContactPerson; ;
                    txtAddress.Text = objSMAgent.AgentAddress; ;
                    txtContactPersonDetails.Text = objSMAgent.AgentContactPersonDetails;
                    txtContactNo1.Text = objSMAgent.AgentPhone;
                    txtContactNo2.Text = objSMAgent.AgentMobile;
                    txtEmail.Text = objSMAgent.AgentEmail;
                    txtFaxNo.Text = objSMAgent.AgentFaxNo;
                                    

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
        else
        {
            MessageBox.Show(this, "Please select  a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (gvAgentDetails.SelectedIndex > -1)
        {
            try
            {
                SM.AgentMaster objSMAgent = new SM.AgentMaster();
                objSMAgent.AgentId = gvAgentDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objSMAgent.AgentMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblAgentDetails.Visible = false;
                btnDelete.Attributes.Clear();
                gvAgentDetails.DataBind();
                SM.ClearControls(this);
                SM.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select a Record");
        }

    }
    #endregion

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        SM.ClearControls(this);
        btnSave.Text = "Save";
        tblAgentDetails.Visible = true;
        btnRefresh.Visible = true;
        btnSave.Visible = true;
        btnClose.Visible = true;
    }
    #endregion


    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
         if (lblSearchItemHidden.Text == ddlSearchBy.SelectedValue || lblSearchValueHidden.Text == txtSearchText.Text)
        {
            lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
            lblSearchValueHidden.Text = txtSearchText.Text;
            gvAgentDetails.DataBind();
        }
        else if (lblSearchItemHidden.Text == "")
        {
           

           

            MessageBox.Show(this, "please select search by value");
        }
        else if (lblSearchValueHidden.Text == "")
        {
           
            MessageBox.Show(this, "please enter search text value");
        }


        else if (lblSearchValueHidden.Text == "" || lblSearchItemHidden.Text == "")
        {
            MessageBox.Show(this, " please enter  a record ");
        }
      
        
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblAgentDetails.Visible = false;
        btnClose.Visible = false;
        btnSave.Visible = false;
        btnRefresh.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        btnRefresh.Visible = true;
        btnSave.Visible = true;
        btnClose.Visible = true;
        Masters.ClearControls(this);
    }
    #endregion



    #region ********** Commented Code ************

    //#region Item Types Fill
    ////private void ItemTypes_Fill()
    ////{
    ////    try
    ////    {
    ////        Masters.ItemType.ItemType_Select(ddlItemType);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        MessageBox.Show(this, ex.Message);
    ////    }
    ////    finally
    ////    {
    ////        Masters.Dispose();
    ////    }
    ////}
    //#endregion

    //#region Item Name Fill
    //private void ItemName_Fill()
    //{
    //    try
    //    {
    //        Masters.ItemMaster.ItemMaster_Select(ddlItemName, ddlItemType.SelectedValue);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion

    //#region Item Type Select Index Changed
    //protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ItemName_Fill();
    //}
    //#endregion

    //#region ddlItemName_SelectedIndexChanged
    //protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Masters.ItemMaster objMaster = new Masters.ItemMaster();
    //        if (objMaster.ItemMaster_Select(ddlItemName.SelectedItem.Value) > 0)
    //        {
    //            txtItemUOM.Text = objMaster.ItemUOMShort;

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        Masters.Dispose();
    //    }
    //}
    //#endregion



    #endregion

}

 
