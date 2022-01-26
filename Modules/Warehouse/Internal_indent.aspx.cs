using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
public partial class Modules_Warehouse_Internal_indent : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblCompany.Text = cp.getPresentCompanySessionValue();
            //Inventory.Internalindent.Godown_Select(ddlfrom);
            //Inventory.Internalindent.Godown_Select(ddlto);
            ddlfrom.DataBind();
            ddlto.DataBind();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        tblmain.Visible = true;
        txtIndentno.Text = Inventory.Internalindent.InternalIndent_AutoGenCode();
        txtindentdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        btnSave.Text = "Save";
        ClearControls();
    }
    private void ClearControls()
    {
        txtClientName.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlfrom.SelectedIndex = 0;
        ddlto.SelectedIndex = 0;
        ddlBrand.SelectedIndex = 0;
        ddlPreparedBy.SelectedIndex = 0;
        //ddlColor.SelectedIndex = 0;      
        gvIndentdetails.DataSource = null;
        gvIndentdetails.DataBind();

    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster5_Select(ddlModelno, ddlBrand.SelectedItem.Value);
    }
    protected void ddlModelno_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelno.SelectedValue);
         Masters.ItemMaster objMaster = new Masters.ItemMaster();
         if (objMaster.ItemMaster_Select(ddlModelno.SelectedItem.Value) > 0)
         {
             txtRemarks.Text = objMaster.ItemSpec;
             txtBrand.Text = objMaster.BrandProductName;
             lblBrandId.Text = objMaster.Brandid;
         }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        tblmain.Visible = false;
    }

    protected void btnrefresh2_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }



    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        ddlModelno.SelectedIndex = 0;
        ddlColor.SelectedIndex = 0;
        ddlBrand.SelectedIndex = 0;
        txtQty.Text = "";
        txtClientName.Text = "";
        txtRemark.Text = "";
        txtRemarks.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvInternalIndent.SelectedIndex > -1)
        {
            try
            {
                Inventory.Internalindent obj = new Inventory.Internalindent();
                MessageBox.Show(this, obj.InternalIndent_Delete(gvInternalIndent.SelectedRow.Cells[0].Text.ToString()));

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvInternalIndent.DataBind();
                Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Itemcode");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Brand");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("ModelNo");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Color");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Qty");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Remarks");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("ClientName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("BrandId");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("ColorId");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Remark");
        CustomerUnits.Columns.Add(col);

        if (gvIndentdetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvIndentdetails.Rows)
            {
                if (gvIndentdetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvIndentdetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["Itemcode"] = ddlModelno.SelectedItem.Value;
                        dr["Brand"] = txtBrand.Text;
                        dr["Modelno"] = ddlModelno.SelectedItem.Text;
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["Qty"] = txtQty.Text;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["ClientName"] = txtClientName.Text;
                        dr["BrandId"] = lblBrandId.Text;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;
                        dr["Remark"] = txtRemark.Text;
                        
                        CustomerUnits.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        dr["Itemcode"] = gvrow.Cells[1].Text;
                        dr["Brand"] = gvrow.Cells[2].Text;
                        dr["Modelno"] = gvrow.Cells[3].Text;
                        dr["Color"] = gvrow.Cells[4].Text;
                        dr["Qty"] = gvrow.Cells[5].Text;
                        dr["Remarks"] = gvrow.Cells[6].Text;
                        dr["ClientName"] = gvrow.Cells[7].Text;
                        dr["BrandId"] = gvrow.Cells[8].Text;
                        dr["ColorId"] = gvrow.Cells[9].Text;
                        dr["Remark"] = gvrow.Cells[10].Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                }
                else
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["Itemcode"] = gvrow.Cells[1].Text;
                    dr["Brand"] = gvrow.Cells[2].Text;
                    dr["Modelno"] = gvrow.Cells[3].Text;
                    dr["Color"] = gvrow.Cells[4].Text;
                    dr["Qty"] = gvrow.Cells[5].Text;
                    dr["Remarks"] = gvrow.Cells[6].Text;
                    dr["ClientName"] = gvrow.Cells[7].Text;
                    dr["BrandId"] = gvrow.Cells[8].Text;
                    dr["ColorId"] = gvrow.Cells[9].Text;
                    dr["Remark"] = gvrow.Cells[10].Text;

                    CustomerUnits.Rows.Add(dr);
                }
            }
        }

        if (gvIndentdetails.SelectedIndex == -1)
        {
            DataRow drnew = CustomerUnits.NewRow();
            drnew["Itemcode"] = ddlModelno.SelectedItem.Value;
            drnew["Brand"] = txtBrand.Text;
            drnew["Modelno"] = ddlModelno.SelectedItem.Text;
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["Qty"] = txtQty.Text;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["ClientName"] = txtClientName.Text;
            drnew["BrandId"] = lblBrandId.Text;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
            drnew["Remark"] = txtRemark.Text;
            CustomerUnits.Rows.Add(drnew);
        }
        gvIndentdetails.DataSource = CustomerUnits;
        gvIndentdetails.DataBind();
        gvIndentdetails.SelectedIndex = -1;
        btnrefresh_Click(sender, e);
    }


    protected void gvIndentdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Itemcode");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Brand");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("ModelNo");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Color");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Qty");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Remarks");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("ClientName");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("BrandId");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("ColorId");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Remark");
        CustomerUnits.Columns.Add(col);

        if (gvIndentdetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvIndentdetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = CustomerUnits.NewRow();
                    dr["Itemcode"] = gvrow.Cells[1].Text;
                    dr["Brand"] = gvrow.Cells[2].Text;
                    dr["Modelno"] = gvrow.Cells[3].Text;
                    dr["Color"] = gvrow.Cells[4].Text;
                    dr["Qty"] = gvrow.Cells[5].Text;
                    dr["Remarks"] = gvrow.Cells[6].Text;
                    dr["ClientName"] = gvrow.Cells[7].Text;
                    dr["BrandId"] = gvrow.Cells[8].Text;
                    dr["ColorId"] = gvrow.Cells[9].Text;
                    dr["Remark"] = gvrow.Cells[10].Text;
                    CustomerUnits.Rows.Add(dr);
                }

            }
        }
        gvIndentdetails.DataSource = CustomerUnits;
        gvIndentdetails.DataBind();
        //btnrefresh_Click(sender, e);
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (btnSave.Text == "Save")
        {
            Indentsave();
           // Response.Redirect("Internal_indent.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
"alert(' Internal Indent Saved sucessfully');window.location ='Internal_indent.aspx';", true);
            gvInternalIndent.DataBind();
            //tblmain.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            IndentUpdate();
            //Response.Redirect("Internal_indent.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
"alert(' Internal Indent Updated sucessfully');window.location ='Internal_indent.aspx';", true);
            gvInternalIndent.DataBind();
            //tblmain.Visible = false;
        }
    }

    private void IndentUpdate()
    {
        if (gvIndentdetails.Rows.Count > 0)
        {
            try
            {
                Inventory.Internalindent obj = new Inventory.Internalindent();
                obj.IndId = gvInternalIndent.SelectedRow.Cells[0].Text;
                obj.Inddate = Yantra.Classes.General.toMMDDYYYY(txtindentdate.Text);
                obj.Indno = txtIndentno.Text;
                obj.from = ddlfrom.SelectedItem.Value;
                obj.to = ddlto.SelectedItem.Value;
                obj.preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                if (obj.InternalIndent_Update() == "Data Updated Successfully")
                {
                    obj.InternalIndentDetails_Delete(obj.IndId);
                    foreach (GridViewRow gvrow in gvIndentdetails.Rows)
                    {
                        obj.itemcode = gvrow.Cells[1].Text;
                        obj.brandid = gvrow.Cells[8].Text;
                        obj.colorid = gvrow.Cells[9].Text;
                        obj.qty = gvrow.Cells[5].Text;
                        obj.description = gvrow.Cells[6].Text;
                        obj.clientname = gvrow.Cells[7].Text;
                        obj.Remarks = gvrow.Cells[10].Text;
                        obj.InternalIndentDetails_Save();
                    }
                    //MessageBox.Show(this, "Data Updated Succesfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                Masters.ClearControls(this);
                tblmain.Visible = false;
                
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");

        }
    }

    private void Indentsave()
    {
        if (gvIndentdetails.Rows.Count > 0)
        {
            try
            {
                Inventory.Internalindent obj = new Inventory.Internalindent();
                obj.Inddate = Yantra.Classes.General.toMMDDYYYY(txtindentdate.Text);
                obj.Indno = txtIndentno.Text;
                obj.from = ddlfrom.SelectedItem.Value;
                obj.to = ddlto.SelectedItem.Value;
                obj.cpid = cp.getPresentCompanySessionValue();
                obj.preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                if (obj.InternalIndent_Save() == "Data Saved Successfully")
                {
                    obj.InternalIndentDetails_Delete(obj.IndId);
                    foreach (GridViewRow gvrow in gvIndentdetails.Rows)
                    {
                        obj.itemcode = gvrow.Cells[1].Text;
                        obj.brandid = gvrow.Cells[8].Text;
                        obj.colorid = gvrow.Cells[9].Text;
                        obj.qty = gvrow.Cells[5].Text;
                        obj.description = gvrow.Cells[6].Text;
                        obj.clientname = gvrow.Cells[7].Text;
                        obj.Remarks = gvrow.Cells[10].Text;
                        obj.InternalIndentDetails_Save();
                    }
                    //MessageBox.Show(this, "Data Saved Succesfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                gvInternalIndent.DataBind();
                Masters.ClearControls(this);
                tblmain.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "Please Add atleast one Item");
        }
    }



    protected void gvIndentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if(e.Row.RowType==DataControlRowType.Header||e.Row.RowType==DataControlRowType.DataRow)
       {
           e.Row.Cells[8].Visible = false;
           e.Row.Cells[9].Visible = false;
         //  e.Row.Cells[0].Visible = false;
       }
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
           e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Item from Items list?');");
       }
    }
    #region ddlSearchBy_SelectedIndexChanged
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Indent Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //MaskedEditSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //MaskedEditSearchToDate.Enabled = false;
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            txtSearchText.Visible = true;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
           
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        gvInternalIndent.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        //if (ceSearchFrom.Enabled == false) { lblSearchValueFromHidden.Text = txtSearchValueFromDate.Text; }
        //else if (ceSearchFrom.Enabled == true) { lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text); }
        //if (ceSearchValueToDate.Enabled == false) { lblSearchValueHidden.Text = txtSearchText.Text; }
        //else if (ceSearchValueToDate.Enabled == true) { lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchText.Text); }

        if (ddlSearchBy.SelectedItem.Text == "Indent Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        gvInternalIndent.DataBind();
    }
    #endregion
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;

            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #region lbtnEnqNo_Click
    protected void lbtnIndNo_Click(object sender, EventArgs e)
    {

        LinkButton lbtnIndNo;
        lbtnIndNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnIndNo.Parent.Parent;
        gvInternalIndent.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvInternalIndent.SelectedIndex > -1)
        {
            Inventory.ClearControls(this);
            try
            {
                Inventory.Internalindent obj = new Inventory.Internalindent();

                if (obj.InternalIndent_Select(gvInternalIndent.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblmain.Visible = true;
                    txtIndentno.Text = obj.Indno;
                    txtindentdate.Text = obj.Inddate;
                    ddlfrom.SelectedValue= obj.from;
                    ddlto.SelectedValue = obj.to;
                    ddlPreparedBy.SelectedValue = obj.preparedby;
                    obj.InternalindentDetails_Select(obj.IndId, gvIndentdetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                //btnDelete.Attributes.Clear();
               // Inventory.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }

    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvInternalIndent.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        gvInternalIndent.DataBind();
    }
    protected void btnPageNoSearch_Click(object sender, EventArgs e)
    {
        gvInternalIndent.PageIndex = Convert.ToInt32(txtPageNo.Text) - 1;

    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (gvInternalIndent.SelectedIndex > -1)
        {
            try
            {
                string pagenavigationstr = "../Reports/SMReportViewer.aspx?type=InternalIndent&indno=" + gvInternalIndent.SelectedRow.Cells[0].Text + "";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlModelno.DataSourceID = "SqlDataSource3";
        ddlModelno.DataTextField = "ITEM_MODEL_NO";
        ddlModelno.DataValueField = "ITEM_CODE";
        ddlModelno.DataBind();
        ddlModelno_SelectedIndexChanged(sender, e);
    }
}
 
