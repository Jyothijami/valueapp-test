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
using System.IO;
using vllib;

public partial class Modules_Masters_CompanyLogos : System.Web.UI.Page
{
    FileUpload fu1 = new FileUpload();



    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }



     #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region Link Button lbtnCompanyName_Click
    protected void lbtnCompanyName_Click(object sender, EventArgs e)
    {
        tblConpanyLogos.Visible = false;
        LinkButton lbtnCompanyName;
        lbtnCompanyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvConpanyLogos.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");

    }
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            ConpanyLogosSave();
            tblConpanyLogos.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            ConpanyLogosUpdate();
            tblConpanyLogos.Visible = false;
        }
        gvConpanyLogos.SelectedIndex = -1;
    }
    #endregion

    #region ConpanyLogosSave
    private void ConpanyLogosSave()
    {
        try
        {
            Masters.CompanyLogos objMaster = new Masters.CompanyLogos();

            objMaster.CLCompanyName = txtCompanyName.Text;
            objMaster.CLDescription = txtDescription.Text;

            if (FileUpload1.HasFile)
            {
                objMaster.CLLogos = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objMaster.LogoAttachments = "";
            }
            MessageBox.Show(this, objMaster.CompanyLogos_Save());
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Masters/Items/" + objMaster.CLId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvConpanyLogos.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ConpanyLogosUpdate
    private void ConpanyLogosUpdate()
    {
        try
        {
            Masters.CompanyLogos objMaster = new Masters.CompanyLogos();

            objMaster.CLId = gvConpanyLogos.SelectedRow.Cells[1].Text;

            objMaster.CLCompanyName = txtCompanyName.Text;
            objMaster.CLDescription = txtDescription.Text;

            if (FileUpload1.HasFile)
            {
                objMaster.LogoAttachments = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            else
            {
                objMaster.LogoAttachments = lbtnAttachedFile.Text;
            }
            MessageBox.Show(this, objMaster.CompanyLogos_Update());
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "Modules/Masters/Items/" + objMaster.CLId + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            btnDelete.Attributes.Clear();
            gvConpanyLogos.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvConpanyLogos.SelectedIndex > -1)
        {
            tblConpanyLogos.Visible = true;

            Masters.CompanyLogos objMaster = new Masters.CompanyLogos();

            if (objMaster.CompanyLogos_Select(gvConpanyLogos.SelectedRow.Cells[1].Text) > 0)
            {
                txtCompanyName.Text = objMaster.CLCompanyName;
                txtDescription.Text = objMaster.CLDescription;

                lbtnAttachedFile.Text = objMaster.LogoAttachments;
                if (lbtnAttachedFile.Text != "")
                {
                    string[] ext = lbtnAttachedFile.Text.Split('.');
                    lbtnAttachedFile.Attributes.Add("onclick", "window.open('Items/" + objMaster.CLId + "." + ext[1] + "','ItemFiles','resizable=yes,width=900,height=600,status=yes,toolbar=no,menubar=no');");
                }
                else
                {
                    lbtnAttachedFile.Attributes.Clear();
                }
            }
            btnSave.Text = "Update";
            btnDelete.Attributes.Clear();
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button NEW Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblConpanyLogos.Visible = true;
    }
    #endregion


    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvConpanyLogos.SelectedIndex > -1)
        {
            try
            {
                Masters.CompanyLogos objMaster = new Masters.CompanyLogos();

                objMaster.CLId = gvConpanyLogos.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.CompanyLogos_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblConpanyLogos.Visible = false;
                btnDelete.Attributes.Clear();
                gvConpanyLogos.DataBind();
                gvConpanyLogos.SelectedIndex = -1;

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


    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion


    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblConpanyLogos.Visible = false;
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvConpanyLogos.DataBind();
    }
    #endregion

    #region gvConpanyLogos_RowDataBound
    protected void gvConpanyLogos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion


    
}

 
