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
using Yantra.Classes;

public partial class Modules_Masters_ProductMaster1 : System.Web.UI.UserControl
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            ItemTypes_Fill();
            Essential_Fill();

        }
    }
  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
          
            Masters.ProductMaster obj = new Masters.ProductMaster();
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                obj.EssentialCode = gvrow.Cells[2].Text;
                obj.ItemCode = gvrow.Cells[5].Text;
                obj.ProductEsseintial_Save();
               
            }

            MessageBox.Show(this, "Data Saved Successfully");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvInterestedProducts.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
            ddlModelNo.Enabled = true;
            ddlEssentialNo.Visible = true;
           

            
        }
        
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlEssentialNo.SelectedValue = "0";
        ddlModelNo.Enabled = true;
         ddlModelNo.SelectedValue = "0";
        txtModelName.Text = string.Empty;//
        txtModelSpecification.Text = string.Empty;
        gvInterestedProducts.DataBind();

    }

    #region Close
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Masters/Masters.aspx");
    }
    #endregion

    #region AddClick
    protected void btnAddProductDetails_Click(object sender, EventArgs e)
    {
        ddlModelNo.Visible = true;
        ddlModelNo.Enabled = false;
        if (txtModelSpecification.Text == "") { txtModelSpecification.Text = "-"; }
        if (txtModelName.Text == "") { txtModelName.Text = "-"; }

        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("EssentialCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EssentialName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
      
        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvInterestedProducts.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvInterestedProducts.SelectedRow.RowIndex)
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["EssentialCode"] = ddlEssentialNo.SelectedItem.Value;
                        dr["EssentialName"] = ddlEssentialNo.SelectedItem.Text;
                        dr["ModelNo"] = ddlModelNo.SelectedItem.Text;
                        dr["ItemCode"] = ddlModelNo.SelectedItem.Value;
                      
                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["EssentialCode"] = gvrow.Cells[2].Text;
                        dr["EssentialName"] = gvrow.Cells[3].Text;
                        dr["ModelNo"] = gvrow.Cells[4].Text;
                        dr["ItemCode"] = gvrow.Cells[5].Text;
                     
                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["EssentialCode"] = gvrow.Cells[2].Text;
                    dr["EssentialName"] = gvrow.Cells[3].Text;
                    dr["ModelNo"] = gvrow.Cells[4].Text;
                    dr["ItemCode"] = gvrow.Cells[5].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gvInterestedProducts.Rows.Count > 0)
        {
            if (gvInterestedProducts.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                {
                    if (gvrow.Cells[2].Text == ddlEssentialNo.SelectedItem.Value)
                    {
                        gvInterestedProducts.DataSource = InterestedProducts;
                        gvInterestedProducts.DataBind();
                        MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvInterestedProducts.SelectedIndex == -1)
        {
            DataRow drnew = InterestedProducts.NewRow();
            drnew["EssentialCode"] = ddlEssentialNo.SelectedItem.Value;
            drnew["EssentialName"] = ddlEssentialNo.SelectedItem.Text;
            drnew["ModelNo"] = ddlModelNo.SelectedItem.Text;
            drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
        
            InterestedProducts.Rows.Add(drnew);
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
        gvInterestedProducts.SelectedIndex = -1;
        btnAddProductDetailsRefresh_Click(sender, e);
    }
    #endregion

    #region AddRefresh
    protected void btnAddProductDetailsRefresh_Click(object sender, EventArgs e)
    {
        ddlEssentialNo.SelectedValue = "0";
        txtModelName.Text = string.Empty;//
        txtModelSpecification.Text = string.Empty;
        gvInterestedProducts.SelectedIndex = -1;
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
          // Masters.ItemMaster.ItemMaster2_Select(ddlNo);
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

    #region Essentials Fill
    private void Essential_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster3_Select(ddlEssentialNo);
           // Masters.ItemMaster.ItemMaster3_Select(ddlName);
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

    #region GvDelete
    protected void gvInterestedProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvInterestedProducts.Rows[e.RowIndex].Cells[2].Text;
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("EssentialCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EssentialName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
       

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["EssentialCode"] = gvrow.Cells[2].Text;
                    dr["EssentialName"] = gvrow.Cells[3].Text;
                    dr["ModelNo"] = gvrow.Cells[4].Text;
                    dr["ItemCode"] = gvrow.Cells[5].Text;
                 
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region GvEdit
    protected void gvInterestedProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("EssentialCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EssentialName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
       

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                DataRow dr = InterestedProducts.NewRow();
                dr["EssentialCode"] = gvrow.Cells[2].Text;
                dr["EssentialName"] = gvrow.Cells[3].Text;
                dr["ModelNo"] = gvrow.Cells[4].Text;
                dr["ItemCode"] = gvrow.Cells[5].Text;
              
            
                InterestedProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvInterestedProducts.Rows[e.NewEditIndex].RowIndex)
                {
                  
                    ddlEssentialNo.SelectedValue = gvrow.Cells[2].Text;
                    //ddlEssentialNo_SelectedIndexChanged(sender, e);
                  
                    gvInterestedProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region DdlEssentialNo Change
    protected void ddlEssentialNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select(ddlEssentialNo.SelectedItem.Value) > 0)
            {

                txtModelSpecification.Text = objMaster.ItemSpec;
                txtModelName.Text = objMaster.ItemName;

            }
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

}
