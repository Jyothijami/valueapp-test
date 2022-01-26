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
using vllib;
using System.Data.SqlClient;
using System.Configuration;

public partial class Modules_Masters_Default3 : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            setControlsVisibility();
            Brand_Fill();
            lblDate.Text = DateTime.Now.ToString();
            lblPreparedBy.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
        }
    }
    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "4");
        btnNew.Enabled = up.add;
        Button1.Enabled = up.Update;
        brndelete.Enabled = up.Delete;
        //btnSearchModelNo.Enabled = up.SearchModelNo;
        //btnSearchEssential.Enabled = up.SearchEssential;
        //btnAddProductDetails.Enabled = up.add;
        //btnAddProductDetailsRefresh.Enabled = up.Refresh;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnClose.Enabled = up.Close;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
            try
            {

                Masters.ProductMaster obj = new Masters.ProductMaster();
                Masters.ProductMaster.sp = 0;

                foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                {
                    obj.EssentialCode = gvrow.Cells[2].Text;
                    obj.ItemCode = gvrow.Cells[6].Text;
                    obj.BrandId = gvrow.Cells[7].Text;
                    obj.ITEMESSENTIAL = gvrow.Cells[3].Text;
                    obj.EssentialId = gvrow.Cells[8].Text;
                    obj.Quantity = gvrow.Cells[10].Text;
                    obj.date = gvrow.Cells[12].Text;
                    obj.empname = gvrow.Cells[13].Text;
                    obj.ProductEsseintial_Save();

                }

                MessageBox.Show(this, "Data Saved Successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvProductmaster.DataBind();
                gvInterestedProducts.DataBind();
                Masters.ClearControls(this);
                btnClose_Click(sender, e);
            }
        
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlModelNo.Enabled = true;
        ddlEssentialNo.SelectedIndex = -1;
        ddlModelNo.SelectedIndex = -1;
        txtModelName.Text = string.Empty;//
        txtModelSpecification.Text = string.Empty;
        gvInterestedProducts.DataBind();

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        
        General.ClearControls(this);
        tblMainDetails.Visible = false;

    }
    
    #region AddClick
    protected void btnAddProductDetails_Click(object sender, EventArgs e)
    {
        if(btnSave.Text == "Save")
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM YANTRA_ITEM_ESSENTIALS where ITEM_CODE ='" + ddlModelNo.SelectedItem.Value + "' ", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int count = Convert.ToInt32(dt.Rows[0][0].ToString());
            if (count > 0)
            {
                ddlModelNo.SelectedItem.Text = string.Empty;
                MessageBox.Show(this, "This Model No Already Exists");
                return;
            }
            else
            {
                AddNewItemToGrid();
            }
        
        }
        else
        {
            AddNewItemToGrid();
        }
    }
        #endregion
    private void AddNewItemToGrid()
    {
        
            //ddlModelNo.Visible = true;
            //ddlModelNo.Enabled = false;
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
            col = new DataColumn("Specification");
            InterestedProducts.Columns.Add(col);
            col = new DataColumn("ItemCode");
            InterestedProducts.Columns.Add(col);
            
            col = new DataColumn("BrandId");
            InterestedProducts.Columns.Add(col);
            col = new DataColumn("EssentialId");
            InterestedProducts.Columns.Add(col);
            col = new DataColumn("ITEM_RATE");
            InterestedProducts.Columns.Add(col);

            col = new DataColumn("Qty");
            InterestedProducts.Columns.Add(col);
            col = new DataColumn("Total");
            InterestedProducts.Columns.Add(col);
            col = new DataColumn("Date");
            InterestedProducts.Columns.Add(col);
            col = new DataColumn("PreparedBy");
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
                            dr["Specification"] = txtModelSpecification.Text;
                            dr["BrandId"] = ddlBrand.SelectedItem.Value;
                            dr["EssentialId"] = "0";
                            dr["ITEM_RATE"] = lblRate.Text.ToString();
                            dr["Qty"] = txtQuantity.Text;
                            dr["Date"] = lblDate.Text;
                            dr["PreparedBy"] = lblPreparedBy.Text;
                            InterestedProducts.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = InterestedProducts.NewRow();
                            dr["EssentialCode"] = gvrow.Cells[2].Text;
                            dr["EssentialName"] = gvrow.Cells[3].Text;
                            dr["ModelNo"] = gvrow.Cells[4].Text;
                            dr["Specification"] = gvrow.Cells[5].Text;
                            dr["ItemCode"] = gvrow.Cells[6].Text;
                            dr["BrandId"] = gvrow.Cells[7].Text;
                            dr["EssentialId"] = gvrow.Cells[8].Text;
                            dr["ITEM_RATE"] = gvrow.Cells[9].Text;
                            dr["Qty"] = gvrow.Cells[10].Text;
                            dr["Date"] = gvrow.Cells[12].Text;
                            dr["PreparedBy"] = gvrow.Cells[13].Text;
                            InterestedProducts.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = InterestedProducts.NewRow();
                        dr["EssentialCode"] = gvrow.Cells[2].Text;
                        dr["EssentialName"] = gvrow.Cells[3].Text;
                        dr["ModelNo"] = gvrow.Cells[4].Text;
                        dr["Specification"] = gvrow.Cells[5].Text;
                        dr["ItemCode"] = gvrow.Cells[6].Text;
                        dr["BrandId"] = gvrow.Cells[7].Text;
                        dr["EssentialId"] = gvrow.Cells[8].Text;
                        dr["ITEM_RATE"] = gvrow.Cells[9].Text;
                        dr["Qty"] = gvrow.Cells[10].Text;
                        dr["Date"] = gvrow.Cells[12].Text;
                        dr["PreparedBy"] = gvrow.Cells[13].Text;
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
                            //MessageBox.Show(this, "The Item Name you have selected is already exists in list");
                            //return;
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
                drnew["Specification"] = txtModelSpecification.Text;
                drnew["ItemCode"] = ddlModelNo.SelectedItem.Value;
                drnew["BrandId"] = ddlBrand.SelectedItem.Value;
                drnew["EssentialId"] = "0";
                drnew["ITEM_RATE"] = lblRate.Text.ToString();
                drnew["Qty"] = txtQuantity.Text;
                drnew["Date"] = lblDate.Text;
                drnew["PreparedBy"] = lblPreparedBy.Text;
                InterestedProducts.Rows.Add(drnew);
            }
            gvInterestedProducts.DataSource = InterestedProducts;
            gvInterestedProducts.DataBind();
            gvInterestedProducts.SelectedIndex = -1;
            gvInterestedProducts.EditIndex = -1;
            //btnAddProductDetailsRefresh_Click(sender, e);
            btnSave.Visible = true;
            btnSave.Enabled = true;
        }

    #region AddRefresh
    protected void btnAddProductDetailsRefresh_Click(object sender, EventArgs e)
    {
        ddlEssentialNo.SelectedIndex = -1;
        ddlModelNo.SelectedIndex = -1;
      ddlBrand.SelectedIndex = -1;
       // ddlEssentialNo.SelectedItem.Text="--";
        //ddlModelNo.SelectedItem.Text = "--";
        //ddlBrand.SelectedValue= "0";
        txtModelName.Text = string.Empty;
        txtModelSpecification.Text = string.Empty;
        gvInterestedProducts.EditIndex = -1;
        gvInterestedProducts.SelectedIndex = -1;
        gvInterestedProducts.DataBind();
        lblRate.Text = "";
    }
    #endregion

    #region Item Types Fill
    private void ItemTypes_Fill()
    {
        try
        {
            Masters.ItemMaster.ItemMaster2_Select(ddlModelNo);
            Masters.ItemMaster.ItemMaster2_Select(ddlEssentialNo);
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

    #region Brand Fill
    private void Brand_Fill()
    {
        try
        {
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
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
        col = new DataColumn("Specification");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ItemCode");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("BrandId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EssentialId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ITEM_RATE");
        InterestedProducts.Columns.Add(col);

        col = new DataColumn("Qty");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("Total");
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
                    dr["ItemCode"] = gvrow.Cells[6].Text;
                    dr["Specification"] = gvrow.Cells[5].Text;
                    dr["BrandId"] = gvrow.Cells[7].Text;
                    dr["EssentialId"] = gvrow.Cells[8].Text;
                    dr["ITEM_RATE"] = gvrow.Cells[9].Text;
                    dr["Qty"] = gvrow.Cells[10].Text;
                    dr["Total"] = gvrow.Cells[10].Text;
                    InterestedProducts.Rows.Add(dr);
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.EditIndex = -1;
        gvInterestedProducts.DataBind();
    }
    #endregion

    #region GvEdit
    protected void gvInterestedProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //ItemTypes_Fill();
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
        col = new DataColumn("BrandId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("EssentialId");
        InterestedProducts.Columns.Add(col);
        col = new DataColumn("ITEM_RATE");
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
                dr["BrandId"] = gvrow.Cells[6].Text;
                dr["EssentialId"] = gvrow.Cells[7].Text;
                dr["ITEM_RATE"] = gvrow.Cells[8].Text;

                InterestedProducts.Rows.Add(dr);
                if (gvrow.RowIndex == gvInterestedProducts.Rows[e.NewEditIndex].RowIndex)
                {
                    ddlModelNo.SelectedValue = gvrow.Cells[5].Text;
                    ddlEssentialNo.SelectedValue = gvrow.Cells[2].Text;
                    ddlEssentialNo_SelectedIndexChanged(sender, e);
                    ddlBrand.SelectedValue = gvrow.Cells[6].Text;

                    gvInterestedProducts.SelectedIndex = gvrow.RowIndex;
                }
            }
        }
        gvInterestedProducts.DataSource = InterestedProducts;
        gvInterestedProducts.EditIndex = -1;
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
                ddlBrand.SelectedValue = objMaster.Brandid;
               // lblRate.Text = objMaster.Rate;
                
            }
            Masters.ItemPurchase objrate = new Masters.ItemPurchase();
            if (objrate.ItemPrice_Ddl(ddlEssentialNo.SelectedItem.Value) > 0)
            {
                lblRate.Text = objrate.rsp;
                

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
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModelNoFill();
    }

    #region Model No Fill
    private void ModelNoFill()
    {
        try
        {
           
            Masters.ItemMaster.ItemMaster5_Select(ddlModelNo, ddlBrand.SelectedItem.Value);
            Masters.ItemMaster.ItemMaster5_Select(ddlEssentialNo, ddlBrand.SelectedItem.Value);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        tblMainDetails.Visible = true;
        Masters.ClearControls(this);
        txtQuantity.Text = "1";
        btnSave.Text = "Save";
        btnSave.Visible = false;
        Brand_Fill();
        ddlBrand_SelectedIndexChanged(sender, e);
        //ddlEssentialNo.SelectedIndex = -1;
        //ddlModelNo.SelectedIndex = -1;
        //ddlBrand.SelectedIndex = -1;
        gvInterestedProducts.DataBind();
        lblDate.Text = DateTime.Now.ToString();
        lblPreparedBy.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
    }
    protected void brndelete_Click(object sender, EventArgs e)
    {
        if (gvProductmaster.SelectedIndex > -1)
        {
            try
            {
                Masters.ProductMaster ObjOrderAcceptance = new Masters.ProductMaster();
                MessageBox.Show(this, ObjOrderAcceptance.ProductEsseintial1_Delete(gvProductmaster.SelectedRow.Cells[3].Text));
            }
            catch (Exception ex)
            {
                //Masters.RollBackTransaction();
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                brndelete.Attributes.Clear();
                gvProductmaster.DataBind();
                tblMainDetails.Visible = false;
                //Masters.ClearControls(this);
                //Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void lbtnModelNo_Click(object sender, EventArgs e)
    {

        LinkButton lbtnModelNo;
        lbtnModelNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnModelNo.Parent.Parent;
        gvProductmaster.SelectedIndex = gvRow.RowIndex;
        brndelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
        tblMainDetails.Visible = true;
        ddlBrand.SelectedValue = gvRow.Cells[4].Text.ToString();
        btnSave.Enabled = false;
        ddlBrand_SelectedIndexChanged(sender, e);
        ExistingRecordFill(sender, e);


    }
    private void ExistingRecordFill(object sender, EventArgs e)
    {
        if (gvProductmaster.SelectedIndex > -1)
        {
            try
            {
                
                Masters.ProductMaster objSM = new Masters.ProductMaster();
                
                    btnSave.Visible = true;
                    btnRefresh.Visible = false;
                    tblMainDetails.Visible = true;


                    ddlModelNo.SelectedValue = gvProductmaster.SelectedRow.Cells[3].Text;
                    objSM.ProductEssential1_select(gvProductmaster.SelectedRow.Cells[3].Text, gvInterestedProducts);
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();

                    foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
                    {
                        dt = objSM.ProductEssential1_select2(gvrow.Cells[2].Text, gvProductmaster.SelectedRow.Cells[3].Text);
                        dt1.Merge(dt);
                        dt1.AcceptChanges();
                    }

                    gvInterestedProducts.DataSource = dt1;
                    gvInterestedProducts.DataBind();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
               // Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        btnSave.Text = "Update";

        ExistingRecordFill(sender, e);
        txtQuantity.Text = "1";
    }
    protected void gvProductmaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
         
        }
    }
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
     
    }
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvProductmaster.DataBind();
    }
    private double GrossAmountCalc()
    {
        double _totalAmt = 0;
        foreach (GridViewRow gvrow in gvInterestedProducts.Rows)
        {
            _totalAmt = _totalAmt + Convert.ToDouble(gvrow.Cells[11].Text);
        }
        return _totalAmt;
    }
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
            e.Row.Cells[11].Text = (Convert.ToDecimal(e.Row.Cells[10].Text) * Convert.ToDecimal(e.Row.Cells[9].Text)).ToString();
          }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[10].Text = "Total Amount:";
            e.Row.Cells[11].Text = GrossAmountCalc().ToString();

            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;

        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelNo.DataSourceID = "SqlDataSource2";
        ddlModelNo.DataTextField = "ITEM_MODEL_NO";
        ddlModelNo.DataValueField = "ITEM_CODE";
        ddlModelNo.DataBind();
    }
    protected void btnSearchEssential_Click(object sender, EventArgs e)
    {
        ddlEssentialNo.DataSourceID = "SqlDataSource3";
        ddlEssentialNo.DataTextField = "ITEM_MODEL_NO";
        ddlEssentialNo.DataValueField = "ITEM_CODE";
        ddlEssentialNo.DataBind();
        ddlEssentialNo_SelectedIndexChanged(sender, e);

    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvProductmaster.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }
}

 
