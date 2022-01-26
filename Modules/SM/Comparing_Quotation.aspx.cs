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
using Yantra.MessageBox;
using YantraBLL.Modules;
using System.Collections.Generic;
using System.Data.SqlClient;
using vllib;

public partial class Modules_SM_Comparing_Quotation : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            CustomerMaster_Fill();
            FillBrand();

        }
    }

    #region  Customer Fill
    private void CustomerMaster_Fill()
    {
        try
        {
            SM.CustomerMaster.CustomerMaster_SelectDdlCustomerName(ddlCustomer);
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
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        //gvQuotationDetails.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        //gvQuotationDetails.DataBind();
    }

    #region ddlCustomer_SelectedIndexChanged
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerUnits_Select(ddlUnitName, ddlCustomer.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();

            if (ddlUnitName.Items.Count > 1)
            {
                txtContactPerson.Visible = false;
                ddlContactPerson.Visible = true;
                rfvContactPerson.Enabled = true;
                rfvUnitName.Enabled = true;
                lblUnitAddress.Text = "Unit Address :";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                }
            }
            else
            {
                txtContactPerson.Visible = true;
                ddlContactPerson.Visible = false;
                rfvContactPerson.Enabled = false;
                rfvUnitName.Enabled = false;
                lblUnitAddress.Text = "Customer Address :";
                if (objSMCustomer.CustomerMaster_Select(ddlCustomer.SelectedItem.Value) > 0)
                {
                    txtContactPerson.Text = objSMCustomer.ContactPerson;
                    txtRegion.Text = objSMCustomer.RegName;
                    txtIndustryType.Text = objSMCustomer.IndType;
                    txtUnitAddress.Text = objSMCustomer.Address;
                    txtEmail.Text = objSMCustomer.Email;
                    txtPhoneNo.Text = objSMCustomer.Phone;
                    txtMobile.Text = objSMCustomer.Mobile;
                }
            }
            // SM.Dispose();

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            // SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlUnitName_SelectedIndexChanged
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SM.CustomerMaster.CustomerMasterDetails_Select(ddlContactPerson, ddlUnitName.SelectedItem.Value);

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerUnits_Select(ddlUnitName.SelectedItem.Value)) > 0)
            {
                //ddlContactPerson.SelectedValue = objSMCustomer.ContactPerson;
                //txtRegion.Text = objSMCustomer.RegName;
                //txtIndustryType.Text = objSMCustomer.IndType;
                txtUnitAddress.Text = objSMCustomer.CustUnitAddress;
                //    txtEmail.Text = objSMCustomer.Email;
                //    txtPhoneNo.Text = objSMCustomer.Phone;
                //    txtMobile.Text = objSMCustomer.Mobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region ddlContactPerson_SelectedIndexChanged
    protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            SM.CustomerMaster objSMCustomer = new SM.CustomerMaster();
            if ((objSMCustomer.CustomerMasterDetails_Select(ddlContactPerson.SelectedItem.Value)) > 0)
            {
                txtEmail.Text = objSMCustomer.CustCorpEmail;
                txtPhoneNo.Text = objSMCustomer.CustCorpPhone;
                txtMobile.Text = objSMCustomer.CustCorpMobile;
            }
            SM.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
            SM.Dispose();
        }
        finally
        {
        }
    }
    #endregion

    #region Fill Brand master
    private void FillBrand()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand1);
            Masters.ProductCompany.ProductCompany_Select(ddlBrand2);
            Masters.ProductCompany.ProductCompany_Select(ddlBrand3);

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

    protected void ddlBrand1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster99_Select(ddlModel1, ddlBrand1.SelectedItem.Value);
            
        }
        catch (Exception ex)
        {

            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
        }

    }
    protected void ddlBrand2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster99_Select(ddlModel2, ddlBrand2.SelectedItem.Value);

        }
        catch (Exception ex)
        {

            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
        }

    }
    protected void ddlBrand3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster99_Select(ddlModel3, ddlBrand3.SelectedItem.Value);

        }
        catch (Exception ex)
        {

            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
        }

    }
    protected void ddlModel1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select345(ddlModel1.SelectedItem.Value) > 0)
            {
                txtDesc1.Text = objMaster.ItemSpec;
                lblBrand1.Text = objMaster.BrandProductName;
               lblImg1.Text = "~/Content/ItemImage/" + objMaster.ItemImage;
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor1, ddlModel1.SelectedValue);
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
    protected void ddlModel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select345(ddlModel2.SelectedItem.Value) > 0)
            {
                txtDesc2.Text = objMaster.ItemSpec;
                lblBrand2.Text = objMaster.BrandProductName;
                lblImg2.Text = "~/Content/ItemImage/" + objMaster.ItemImage;
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor2, ddlModel2.SelectedValue);
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
    protected void ddlModel3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Masters.ItemMaster objMaster = new Masters.ItemMaster();
            if (objMaster.ItemMaster_Select345(ddlModel3.SelectedItem.Value) > 0)
            {
                txtDesc3.Text = objMaster.ItemSpec;
                lblBrand3.Text = objMaster.BrandProductName;
                lblImg3.Text = "~/Content/ItemImage/" + objMaster.ItemImage;
            }

            Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor3, ddlModel3.SelectedValue);
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
    protected void btnCompare1_Click(object sender, EventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Model");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Description");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Qty");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Price");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Spl_Price");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("image1_path");
        InterestedProducts.Columns.Add(col);

        if (gv1.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gv1.Rows)
            {
                if (gv1.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gv1.SelectedRow.RowIndex)
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["Model"] = ddlModel1.SelectedItem.Value;
                        dr["Description"] = txtDesc1.Text;                       

                        if (ddlColor1.SelectedIndex >= 1)
                        {
                            dr["color"] = ddlColor1.SelectedItem.Text;

                        }
                        else 
                        {
                            dr["Color"] = "N-A";
                        }
                        dr["Qty"] = "1";
                        dr["Price"] = txtPrice1.Text;
                        dr["Spl_Price"] = txtSpl1.Text;
                        dr["image1_path"] = lblImg1.Text;                       


                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["Model"] = gvrow.Cells[1].Text;
                        dr["Description"] = gvrow.Cells[2].Text;
                        dr["Color"] = gvrow.Cells[3].Text;
                        dr["Qty"] = gvrow.Cells[4].Text;
                        dr["Price"] = gvrow.Cells[5].Text;
                        dr["Spl_Price"] = gvrow.Cells[6].Text;
                        dr["image1_path"] = gvrow.Cells[7].Text;
                     

                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["Model"] = gvrow.Cells[1].Text;
                    dr["Description"] = gvrow.Cells[2].Text;
                    dr["Color"] = gvrow.Cells[3].Text;
                    dr["Qty"] = gvrow.Cells[4].Text;
                    dr["Price"] = gvrow.Cells[5].Text;
                    dr["Spl_Price"] = gvrow.Cells[6].Text;
                    dr["image1_path"] = gvrow.Cells[7].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gv1.SelectedIndex == -1)
        {
            DataRow dr = InterestedProducts.NewRow();
            dr["Model"] = ddlModel1.SelectedItem.Value;
            dr["Description"] = txtDesc1.Text;

            if (ddlColor1.SelectedIndex >= 1)
            {
                dr["color"] = ddlColor1.SelectedItem.Text;

            }
            else
            {
                dr["Color"] = "N-A";
            }
            dr["Qty"] = "1";
            dr["Price"] = txtPrice1.Text;
            dr["Spl_Price"] = txtSpl1.Text;
            dr["image1_path"] = lblImg1.Text;    
            InterestedProducts.Rows.Add(dr);
        }
        gv1.DataSource = InterestedProducts;
        gv1.DataBind();
        gv1.SelectedIndex = -1;
        
    }
    protected void btnCompare2_Click(object sender, EventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Model");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Description");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Qty");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Price");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Spl_Price");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("image2_path");
        InterestedProducts.Columns.Add(col);

        if (gv2.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gv2.Rows)
            {
                if (gv2.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gv2.SelectedRow.RowIndex)
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["Model"] = ddlModel2.SelectedItem.Value;
                        dr["Description"] = txtDesc2.Text;

                        if (ddlColor2.SelectedIndex >= 1)
                        {
                            dr["color"] = ddlColor2.SelectedItem.Text;

                        }
                        else
                        {
                            dr["Color"] = "N-A";
                        }
                        dr["Qty"] = "1";
                        dr["Price"] = txtPrice2.Text;
                        dr["Spl_Price"] = txtSpl2.Text;
                        dr["image2_path"] = lblImg2.Text;


                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["Model"] = gvrow.Cells[1].Text;
                        dr["Description"] = gvrow.Cells[2].Text;
                        dr["Color"] = gvrow.Cells[3].Text;
                        dr["Qty"] = gvrow.Cells[4].Text;
                        dr["Price"] = gvrow.Cells[5].Text;
                        dr["Spl_Price"] = gvrow.Cells[6].Text;
                        dr["image2_path"] = gvrow.Cells[7].Text;


                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["Model"] = gvrow.Cells[1].Text;
                    dr["Description"] = gvrow.Cells[2].Text;
                    dr["Color"] = gvrow.Cells[3].Text;
                    dr["Qty"] = gvrow.Cells[4].Text;
                    dr["Price"] = gvrow.Cells[5].Text;
                    dr["Spl_Price"] = gvrow.Cells[6].Text;
                    dr["image2_path"] = gvrow.Cells[7].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gv2.SelectedIndex == -1)
        {
            DataRow dr = InterestedProducts.NewRow();
            dr["Model"] = ddlModel2.SelectedItem.Value;
            dr["Description"] = txtDesc2.Text;

            if (ddlColor2.SelectedIndex >= 1)
            {
                dr["color"] = ddlColor2.SelectedItem.Text;

            }
            else
            {
                dr["Color"] = "N-A";
            }
            dr["Qty"] = "1";
            dr["Price"] = txtPrice2.Text;
            dr["Spl_Price"] = txtSpl2.Text;
            dr["image2_path"] = lblImg2.Text;
            InterestedProducts.Rows.Add(dr);
        }
        gv2.DataSource = InterestedProducts;
        gv2.DataBind();
        gv2.SelectedIndex = -1;
    }
    protected void btnCompare3_Click(object sender, EventArgs e)
    {
        DataTable InterestedProducts = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Model");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Description");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Color");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Qty");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Price");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Spl_Price");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("image3_path");
        InterestedProducts.Columns.Add(col);

        if (gv3.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gv3.Rows)
            {
                if (gv3.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gv3.SelectedRow.RowIndex)
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["Model"] = ddlModel3.SelectedItem.Value;
                        dr["Description"] = txtDesc3.Text;

                        if (ddlColor3.SelectedIndex >= 1)
                        {
                            dr["color"] = ddlColor3.SelectedItem.Text;

                        }
                        else
                        {
                            dr["Color"] = "N-A";
                        }
                        dr["Qty"] = "1";
                        dr["Price"] = txtPrice3.Text;
                        dr["Spl_Price"] = txtSpl3.Text;
                        dr["image3_path"] = lblImg3.Text;


                        InterestedProducts.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["Model"] = gvrow.Cells[1].Text;
                        dr["Description"] = gvrow.Cells[2].Text;
                        dr["Color"] = gvrow.Cells[3].Text;
                        dr["Qty"] = gvrow.Cells[4].Text;
                        dr["Price"] = gvrow.Cells[5].Text;
                        dr["Spl_Price"] = gvrow.Cells[6].Text;
                        dr["image3_path"] = gvrow.Cells[7].Text;


                        InterestedProducts.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = InterestedProducts.NewRow();
                    dr["Model"] = gvrow.Cells[1].Text;
                    dr["Description"] = gvrow.Cells[2].Text;
                    dr["Color"] = gvrow.Cells[3].Text;
                    dr["Qty"] = gvrow.Cells[4].Text;
                    dr["Price"] = gvrow.Cells[5].Text;
                    dr["Spl_Price"] = gvrow.Cells[6].Text;
                    dr["image3_path"] = gvrow.Cells[7].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }

        if (gv3.SelectedIndex == -1)
        {
            DataRow dr = InterestedProducts.NewRow();
            dr["Model"] = ddlModel3.SelectedItem.Value;
            dr["Description"] = txtDesc3.Text;

            if (ddlColor3.SelectedIndex >= 1)
            {
                dr["color"] = ddlColor3.SelectedItem.Text;

            }
            else
            {
                dr["Color"] = "N-A";
            }
            dr["Qty"] = "1";
            dr["Price"] = txtPrice3.Text;
            dr["Spl_Price"] = txtSpl3.Text;
            dr["image3_path"] = lblImg3.Text;
            InterestedProducts.Rows.Add(dr);
        }
        gv3.DataSource = InterestedProducts;
        gv3.DataBind();
        gv3.SelectedIndex = -1;
    }
}
 
