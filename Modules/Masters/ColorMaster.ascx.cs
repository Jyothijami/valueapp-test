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

public partial class Modules_Masters_ItemCategoryMaster : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            setControlsVisibility();
            FillBrand();
            CategoryType();
            FillCategory();
        }
        
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCategoryDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvCategoryDetails.DataBind();

    }
    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion


    private void FillCategory()
    {
        try
        {
            Masters.ItemCategory.ItemCategory_Select(ddlCat);

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
    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrandStock);
           
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
    #endregion

    #region Fill CategoryType
    private void CategoryType()
    {
        try
        {
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
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
    #endregion

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            CategorySave();
            tblCompanyDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            CategoryUpdate();
            tblCompanyDetails.Visible = false;
        }
        gvCategoryDetails.SelectedIndex = -1;
    }
    #endregion

    #region Color Save
    private void CategorySave()
    {
        try
        {
            Masters.ColorMaster objMaster = new Masters.ColorMaster ();
            objMaster.ColorName = txtCategoryName.Text;
            objMaster.Brand_Id = ddlBrandStock.SelectedValue;
            objMaster.Ic_Id = ddlCategory.SelectedValue;
            //objMaster.ItCategoryDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Color_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvCategoryDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion


    #region Color Update
    private void CategoryUpdate()
    {
        try
        {
            Masters.ColorMaster objMaster = new Masters.ColorMaster ();
            objMaster.ColorId = gvCategoryDetails.SelectedRow.Cells[1].Text;
            objMaster.ColorName = txtCategoryName.Text;
            objMaster.Brand_Id = ddlBrandStock.SelectedValue;
            objMaster.Ic_Id = ddlCategory.SelectedValue;
            //objMaster.ItCategoryDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.Color_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvCategoryDetails.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvCompanyDetails_RowDataBound
    protected void gvCompanyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
    }
    #endregion

    #region Link Button lbtnCompanyName_Click
    protected void lbtnCompanyName_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
        LinkButton lbtnCompanyName;
        lbtnCompanyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvCategoryDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCategoryDetails.SelectedIndex > -1)
        {
            tblCompanyDetails.Visible = true;
            txtCategoryName.Text = gvCategoryDetails.SelectedRow.Cells[0].Text;
            ddlBrandStock.SelectedValue = gvCategoryDetails.SelectedRow.Cells[5].Text;
            ddlCategory.SelectedValue = gvCategoryDetails.SelectedRow.Cells[6].Text;
            //txtDescription.Text = gvCategoryDetails.SelectedRow.Cells[3].Text;

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
        if (gvCategoryDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.ColorMaster objMaster = new Masters.ColorMaster ();
                objMaster.ColorId = gvCategoryDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Color_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvCategoryDetails.DataBind();
                gvCategoryDetails.SelectedIndex = -1;

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
        tblCompanyDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtCategoryName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        lblSearchcatHidden.Text = ddlCat.SelectedValue;
        gvCategoryDetails.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Dropdown list select index change
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
        if (ddlSearchBy.SelectedValue == "PRODUCT_COMPANY_NAME")
        {
            ddlCat.Visible = true;
        }
        else
        {
            ddlCat.Visible = false;
        }
    }
    #endregion
    
}
