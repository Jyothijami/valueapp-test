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

public partial class Modules_Masters_SubContractorMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }
    #endregion

     #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            SubContractorSave();
            tblSubContractorDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            SubContractorUpdate();
            tblSubContractorDetails.Visible = false;
        }
        gvSubContractorDetails.SelectedIndex = -1;
    }
    #endregion

    #region SubContractorSave
    private void SubContractorSave()
    {
        try
        {
            Masters.SubContractorMaster objMaster = new Masters.SubContractorMaster();
            objMaster.SCName = txtSubContractorName.Text;
            objMaster.SCContactPerson = txtContactPerson.Text;
            objMaster.SCAddress = txtAddress.Text;
            objMaster.SCContPersonDet = txtContactPersonDetails.Text;
            objMaster.SCContactNo1 = txtContactNo1.Text;
            objMaster.SCContactNo2 = txtContactNo2.Text;
            objMaster.SCEmail = txtEmail.Text;
            objMaster.SCFAXNo = txtFaxNo.Text;
            objMaster.SCPANNo = txtPANNo.Text;
            objMaster.SCCSTNo = txtCSTNo.Text;
            objMaster.SCVATNo = txtVATNo.Text;
            objMaster.SCECCNo = txtECCNo.Text;
            objMaster.SCRanking = txtRanking.Text;

            MessageBox.Show(this, objMaster.SubContractorMaster_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvSubContractorDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region SubContractorUpdate
    private void SubContractorUpdate()
    {
        try
        {
            Masters.SubContractorMaster objMaster = new Masters.SubContractorMaster();
            objMaster.SCId = gvSubContractorDetails.SelectedRow.Cells[1].Text;
            objMaster.SCName = txtSubContractorName.Text;
            objMaster.SCContactPerson = txtContactPerson.Text;
            objMaster.SCAddress = txtAddress.Text;
            objMaster.SCContPersonDet = txtContactPersonDetails.Text;
            objMaster.SCContactNo1 = txtContactNo1.Text;
            objMaster.SCContactNo2 = txtContactNo2.Text;
            objMaster.SCEmail = txtEmail.Text;
            objMaster.SCFAXNo = txtFaxNo.Text;
            objMaster.SCPANNo = txtPANNo.Text;
            objMaster.SCCSTNo = txtCSTNo.Text;
            objMaster.SCVATNo = txtVATNo.Text;
            objMaster.SCECCNo = txtECCNo.Text;
            objMaster.SCRanking = txtRanking.Text;
            MessageBox.Show(this, objMaster.SubContractorMaster_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           
            gvSubContractorDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region  gvSubContractorDetails_RowDataBound

    protected void gvSubContractorDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
        }
    }
    #endregion

    #region Link Button SubContractor_Click
    protected void lbtnSubContractor_Click(object sender, EventArgs e)
    {
        tblSubContractorDetails.Visible = false;
        LinkButton lbtnSubContractor;
        lbtnSubContractor = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSubContractor.Parent.Parent;
        gvSubContractorDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvSubContractorDetails.SelectedIndex > -1)
        {
            tblSubContractorDetails.Visible = true;
            txtSubContractorName.Text = gvSubContractorDetails.SelectedRow.Cells[0].Text;
            txtContactPerson.Text = gvSubContractorDetails.SelectedRow.Cells[3].Text;
            txtAddress.Text = gvSubContractorDetails.SelectedRow.Cells[4].Text;
            txtContactPersonDetails.Text = gvSubContractorDetails.SelectedRow.Cells[5].Text;
            txtContactNo1.Text = gvSubContractorDetails.SelectedRow.Cells[6].Text;
            txtContactNo2.Text = gvSubContractorDetails.SelectedRow.Cells[7].Text;
            txtEmail.Text = gvSubContractorDetails.SelectedRow.Cells[8].Text;
            txtFaxNo.Text = gvSubContractorDetails.SelectedRow.Cells[9].Text;
            txtPANNo.Text = gvSubContractorDetails.SelectedRow.Cells[10].Text;
            txtCSTNo.Text = gvSubContractorDetails.SelectedRow.Cells[11].Text;
            txtVATNo.Text = gvSubContractorDetails.SelectedRow.Cells[12].Text;
            txtECCNo.Text = gvSubContractorDetails.SelectedRow.Cells[13].Text;
            txtRanking.Text = gvSubContractorDetails.SelectedRow.Cells[14].Text;


            btnSave.Text = "Update";
           
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion


    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvSubContractorDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.SubContractorMaster objMaster = new Masters.SubContractorMaster();
                objMaster.SCId = gvSubContractorDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.SubContractorMaster_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblSubContractorDetails.Visible = false;
               
                gvSubContractorDetails.DataBind();
                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblSubContractorDetails.Visible = true;
        ScriptManagerLocal.SetFocus(txtSubContractorName);
    }
    #endregion

    #region  Button Refresh
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblSubContractorDetails.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvSubContractorDetails.DataBind();
    }
    #endregion

    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
}
