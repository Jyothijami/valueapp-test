using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
public partial class Modules_SM_rough : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("DocCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Room");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
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
                        dr["ItemCode"] = "0";
                        dr["ModelNo"] = txtModelNo.Text;
                        //dr["ItemType"] = ddlItemType.SelectedItem.Text;
                        dr["ItemName"] = txtItemName.Text;

                        dr["Quantity"] = txtItemQuantity.Text;
                        dr["Specifications"] = txtItemSpecifications.Text;
                        dr["Remarks"] = txtRemarks.Text; ;
                        dr["Priority"] = ddlPriority.SelectedItem.Text;
                        //dr["ItemTypeId"] = ddlItemType.SelectedItem.Value;
                        dr["DocCharges"] = txtDocCharges.Text;
                        dr["DocInFavourOf"] = txtInFavourofDoc.Text;
                        dr["EMDCharges"] = txtEMDCharges.Text;
                        dr["EMDInFavourOf"] = txtInFavourofEMD.Text;
                        dr["Room"] = txtRoom.Text;

                        dr["Color"] = txtColor.Text;
                        dr["ColorId"] = "0";
                        dr["Brand"] = txtBrand.Text;

                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["ItemCode"] = gvrow.Cells[2].Text;
                        dr["ModelNo"] = gvrow.Cells[3].Text;
                        dr["ItemName"] = gvrow.Cells[4].Text;
                        dr["Brand"] = gvrow.Cells[5].Text;
                        dr["Quantity"] = gvrow.Cells[6].Text;
                        dr["Specifications"] = gvrow.Cells[7].Text;
                        dr["Remarks"] = gvrow.Cells[8].Text;
                        dr["Priority"] = gvrow.Cells[9].Text;
                        // dr["ItemTypeId"] = gvrow.Cells[10].Text;
                        dr["DocCharges"] = gvrow.Cells[10].Text;
                        dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                        dr["EMDCharges"] = gvrow.Cells[12].Text;
                        dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                        dr["Room"] = gvrow.Cells[14].Text;

                        dr["Color"] = gvrow.Cells[15].Text;
                        dr["ColorId"] = gvrow.Cells[16].Text;

                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["Brand"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Specifications"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;
                    dr["Priority"] = gvrow.Cells[9].Text;
                    // dr["ItemTypeId"] = gvrow.Cells[10].Text;
                    dr["DocCharges"] = gvrow.Cells[10].Text;
                    dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                    dr["EMDCharges"] = gvrow.Cells[12].Text;
                    dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                    dr["Room"] = gvrow.Cells[14].Text;
                    dr["Color"] = gvrow.Cells[15].Text;
                    dr["ColorId"] = gvrow.Cells[16].Text;

                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gvInterestedProducts.SelectedIndex == -1)
        {
            DataRow drnew = InterestedProducts.NewRow();
            drnew["ItemCode"] = "0";
            drnew["ModelNo"] = txtModelNo.Text;
            drnew["ItemName"] = txtItemName.Text;
            drnew["Brand"] = txtBrand.Text;
            drnew["Quantity"] = txtItemQuantity.Text;
            drnew["Specifications"] = txtItemSpecifications.Text;
            drnew["Remarks"] = txtRemarks.Text; ;
            drnew["Priority"] = ddlPriority.SelectedItem.Text;
            //drnew["ItemTypeId"] = ddlItemType.SelectedItem.Value;
            drnew["DocCharges"] = txtDocCharges.Text;
            drnew["DocInFavourOf"] = txtInFavourofDoc.Text;
            drnew["EMDCharges"] = txtEMDCharges.Text;
            drnew["EMDInFavourOf"] = txtInFavourofEMD.Text;
            drnew["Room"] = txtRoom.Text;
            drnew["Color"] = txtColor.Text;
            drnew["ColorId"] = "0";
            //  drnew["Brand"] = txtBrand.Text;
            InterestedProducts.Rows.Add(drnew);
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
        gvInterestedProducts.SelectedIndex = -1;
        btnRefreshItems_Click(sender, e);
    
    }
    protected void btnRefreshItems_Click(object sender, EventArgs e)
    {

    }
    protected void gvInterestedProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string x1 = gvInterestedProducts.Rows[e.RowIndex].Cells[2].Text;
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("DocCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Room");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        InterestedProducts.Columns.Add(col);

        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["ItemCode"] = gvrow.Cells[2].Text;
                    dr["ModelNo"] = gvrow.Cells[3].Text;
                    dr["ItemName"] = gvrow.Cells[4].Text;
                    dr["Brand"] = gvrow.Cells[5].Text;
                    dr["Quantity"] = gvrow.Cells[6].Text;
                    dr["Specifications"] = gvrow.Cells[7].Text;
                    dr["Remarks"] = gvrow.Cells[8].Text;
                    dr["Priority"] = gvrow.Cells[9].Text;

                    dr["DocCharges"] = gvrow.Cells[10].Text;
                    dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                    dr["EMDCharges"] = gvrow.Cells[12].Text;
                    dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                    dr["Room"] = gvrow.Cells[14].Text;
                    dr["Color"] = gvrow.Cells[15].Text;
                    dr["ColorId"] = gvrow.Cells[16].Text;

                    InterestedProducts.Rows.Add(dr);
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    
    }
    protected void gvInterestedProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ModelNo");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemName");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Quantity");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Specifications");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Remarks");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Priority");
        InterestedProducts.Columns.Add(col);
        //col = new DataColumn("ItemTypeId");
        //InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("DocInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDCharges");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EMDInFavourOf");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Room");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ColorId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Brand");
        InterestedProducts.Columns.Add(col);
        if (gvInterestedProducts.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
            {
                DataRow dr = InterestedProducts.NewRow();
                dr["ItemCode"] = gvrow.Cells[2].Text;
                dr["ModelNo"] = gvrow.Cells[3].Text;
                dr["ItemName"] = gvrow.Cells[4].Text;
                dr["Brand"] = gvrow.Cells[5].Text;
                dr["Quantity"] = gvrow.Cells[6].Text;
                dr["Specifications"] = gvrow.Cells[7].Text;
                dr["Remarks"] = gvrow.Cells[8].Text;
                dr["Priority"] = gvrow.Cells[9].Text;

                dr["DocCharges"] = gvrow.Cells[10].Text;
                dr["DocInFavourOf"] = gvrow.Cells[11].Text;
                dr["EMDCharges"] = gvrow.Cells[12].Text;
                dr["EMDInFavourOf"] = gvrow.Cells[13].Text;
                dr["Room"] = gvrow.Cells[14].Text;
                dr["Color"] = gvrow.Cells[15].Text;
                dr["ColorId"] = gvrow.Cells[16].Text;
                // dr["Brand"] = gvrow.Cells[17].Text;
                InterestedProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvInterestedProducts.Rows[e.NewEditIndex].RowIndex)
                {
                    //ddlItemType.SelectedValue = gvrow.Cells[10].Text;
                    //ItemName_Fill();
                    //txtModelNo.Text = gvrow.Cells[2].Text;
                    //txtItemName.Text = gvrow.Cells[3].Text;
                    ////ddlItemName_SelectedIndexChanged(sender, e);
                    ////txtItemUOM.Text = gvrow.Cells[5].Text;
                    //// ItemTypes_Fill();
                    txtItemQuantity.Text = gvrow.Cells[6].Text;
                    txtItemSpecifications.Text = gvrow.Cells[7].Text;
                    txtRemarks.Text = gvrow.Cells[8].Text;
                    //ddlPriority.SelectedValue = gvrow.Cells[9].Text;
                    //txtDocCharges.Text = gvrow.Cells[10].Text;
                    //txtInFavourofDoc.Text = gvrow.Cells[11].Text;
                    //txtEMDCharges.Text = gvrow.Cells[12].Text;
                    //txtInFavourofEMD.Text = gvrow.Cells[13].Text;
                    txtRoom.Text = gvrow.Cells[14].Text;
                    txtColor.Text = gvrow.Cells[15].Text;
                    //txtBrand.Text = gvrow.Cells[5].Text;
                    gvInterestedProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        ////Set the edit index.
        //gvInterestedProducts.EditIndex = e.NewEditIndex;
        ////Bind data to the GridView control.
        //gvInterestedProducts.DataBind();
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.DataBind();
    }
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[16].Visible = false;
            
        }
       
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[16].Visible  =  e.Row.Cells[9].Visible = false;
                
            }
        }


    protected void gvInterestedProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Reset the edit index.
        gvInterestedProducts.EditIndex = -1;
        gvInterestedProducts.DataBind();
    }
    protected void gvInterestedProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        
    }
}
 
