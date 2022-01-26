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
public partial class Modules_SCM_Item_History : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Supplier_Fill();
            FillBrand();
        }
    }

    #region Supplier_Fill
    private void Supplier_Fill()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_Select1(ddlSupplierName);
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

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrandName);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            MessageBox.Show(this, ex.StackTrace.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion

    #region Fill Model No
    private void FillModelNo()
    {
        try
        {
            SCM.SuppliersMaster.SuppliersMaster_ModelNoFill(ddlItemCode,int.Parse(ddlSupplierName.SelectedValue),int.Parse(ddlBrandName.SelectedValue));
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            SCM.Dispose();
        }
    }
    #endregion

    #region Brand_selectedChanged
    protected void ddlBrandName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillModelNo();
       
    }
    #endregion 

    #region Item_Code_SelectedChange
    protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSupplierName.SelectedValue; 
        lblSearchValueHidden.Text = ddlItemCode.SelectedValue;
        gvItemsMasterDetails.DataBind();
        gvItemsMasterDetails.Visible = true;
    }
    #endregion
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { e.Row.Cells[5].Text = ((Convert.ToInt32(e.Row.Cells[4].Text)) - (Convert.ToInt32(e.Row.Cells[3].Text))).ToString(); }

    }
    protected void btnSearchModelNo_Click1(object sender, EventArgs e)
    {
        ddlItemCode.DataSourceID = "SqlDataSource2";
        ddlItemCode.DataTextField = "ITEM_MODEL_NO";
        ddlItemCode.DataValueField = "ITEM_CODE";
        ddlItemCode.DataBind();
        ddlItemCode_SelectedIndexChanged(sender, e);
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemsMasterDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvItemsMasterDetails.DataBind();
    }
}

 
