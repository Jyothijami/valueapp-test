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

public partial class Modules_Masters_Review_Questions : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            setControlsVisibility();

            CategoryType();
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
        //btnPrint.Enabled = up.Print;

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvReviewQuestionDet.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvReviewQuestionDet.DataBind();

    }

    #region Fill CategoryType
    private void CategoryType()
    {
        try
        {
            Masters.ReviewQuestion.ReviewQuestion_Select(ddlCategoryName);
            //Masters.ItemCategory.AssetCategory_Select(ddlCategoryName);
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
            ItemSave();
        }
        else if (btnSave.Text == "Update")
        {
            ItemUpdate();
        }
    }
    #endregion

    #region ItemSave
    private void ItemSave()
    {
        try
        {
            Masters.ReviewQuestion objMaster = new Masters.ReviewQuestion();
            objMaster.ReviewQuestionName = txtReviewQuestionName.Text;
            objMaster.ItemDesc = txtDescription.Text;
            objMaster.ReviewCategoryId = ddlCategoryName.SelectedItem.Value;
            objMaster.Status = "0";
            MessageBox.Show(this, objMaster.ReviewQuestion_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblReviewQuestionDet.Visible = false;

            gvReviewQuestionDet.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region ItemUpdate
    private void ItemUpdate()
    {
        try
        {
            Masters.ReviewQuestion objMaster = new Masters.ReviewQuestion();
            objMaster.ReviewQuestionId = gvReviewQuestionDet.SelectedRow.Cells[1].Text;
            objMaster.ReviewQuestionName = txtReviewQuestionName.Text;
            objMaster.ReviewCategoryId = ddlCategoryName.SelectedItem.Value;
            objMaster.ItemDesc = txtDescription.Text;
            MessageBox.Show(this, objMaster.ReviewQuestion_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            tblReviewQuestionDet.Visible = false;

            gvReviewQuestionDet.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvReviewQuestionDet_RowDataBound
    protected void gvReviewQuestionDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
    }
    #endregion

    #region Link Button ReviewQuestionName_Click
    protected void lbtnReviewQuestionName_Click(object sender, EventArgs e)
    {
        LinkButton lbtnReviewQuestionName;
        lbtnReviewQuestionName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnReviewQuestionName.Parent.Parent;
        gvReviewQuestionDet.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        tblReviewQuestionDet.Visible = false;
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvReviewQuestionDet.SelectedIndex > -1)
        {
            Masters.ReviewQuestion objMaster = new Masters.ReviewQuestion();
            tblReviewQuestionDet.Visible = true;
            txtReviewQuestionName.Text = gvReviewQuestionDet.SelectedRow.Cells[0].Text;

            txtDescription.Text = gvReviewQuestionDet.SelectedRow.Cells[4].Text;
            btnSave.Text = "Update";

            ddlCategoryName.SelectedIndex = ddlCategoryName.Items.IndexOf(ddlCategoryName.Items.FindByValue(gvReviewQuestionDet.SelectedRow.Cells[5].Text));

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
        if (gvReviewQuestionDet.SelectedIndex > -1)
        {
            try
            {
                Masters.ReviewQuestion objMaster = new Masters.ReviewQuestion();
                objMaster.ReviewQuestionId = gvReviewQuestionDet.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.ReviewQuestion_Delete());

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                tblReviewQuestionDet.Visible = false;

                gvReviewQuestionDet.DataBind();
                gvReviewQuestionDet.SelectedIndex = -1;
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
        tblReviewQuestionDet.Visible = true;
        //ScriptManagerLocal.SetFocus(txtReviewQuestionName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvReviewQuestionDet.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblReviewQuestionDet.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion 
}