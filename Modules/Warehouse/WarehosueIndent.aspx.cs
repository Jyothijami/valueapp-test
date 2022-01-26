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
public partial class Modules_Warehouse_WarehosueIndent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Inventory.Internalindent.Godown_Select(ddlfrom);
            Inventory.Internalindent.Godown_Select(ddlto);
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            HR.EmployeeMaster.EmployeeMaster_Select(ddlPreparedBy);
        }
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        tblmain.Visible = true;
        txtIndentno.Text = Inventory.Internalindent.InternalIndent_AutoGenCode();
        btnSave.Text = "Save";
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
        ddlModelno.SelectedValue = "0";
        ddlColor.SelectedValue = "0";
        ddlBrand.SelectedValue = "0";
        txtQty.Text = "";
        txtClientName.Text = "";
        txtRemarks.Text = "";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvInternalIndent.SelectedIndex > -1)
        {
            try
            {
                Inventory.Internalindent objSM = new Inventory.Internalindent();
                MessageBox.Show(this, objSM.InternalIndent_Delete(gvInternalIndent.SelectedRow.Cells[0].Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {
                btnDelete.Attributes.Clear();
                gvInternalIndent.DataBind();
                SM.ClearControls(this);
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
                        dr["Brand"] = ddlBrand.SelectedItem.Text;
                        dr["Modelno"] = ddlModelno.SelectedItem.Value;
                        dr["Color"] = ddlColor.SelectedItem.Text;
                        dr["Qty"] = txtQty.Text;
                        dr["Remarks"] = txtRemarks.Text;
                        dr["ClientName"] = txtClientName.Text;
                        dr["BrandId"] = ddlBrand.SelectedItem.Value;
                        dr["ColorId"] = ddlColor.SelectedItem.Value;

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
                    CustomerUnits.Rows.Add(dr);
                }
            }
        }

        if (gvIndentdetails.SelectedIndex == -1)
        {
            DataRow drnew = CustomerUnits.NewRow();
            drnew["Itemcode"] = ddlModelno.SelectedItem.Value;
            drnew["Brand"] = ddlBrand.SelectedItem.Text;
            drnew["Modelno"] = ddlModelno.SelectedItem.Value;
            drnew["Color"] = ddlColor.SelectedItem.Text;
            drnew["Qty"] = txtQty.Text;
            drnew["Remarks"] = txtRemarks.Text;
            drnew["ClientName"] = txtClientName.Text;
            drnew["BrandId"] = ddlBrand.SelectedItem.Value;
            drnew["ColorId"] = ddlColor.SelectedItem.Value;
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
                    CustomerUnits.Rows.Add(dr);
                }

            }
        }
        gvIndentdetails.DataSource = CustomerUnits;
        gvIndentdetails.DataBind();
        btnrefresh_Click(sender, e);
    }





    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (btnSave.Text == "Save")
        {
            Indentsave();
            tblmain.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            IndentUpdate();
            tblmain.Visible = true;
        }
    }

    private void IndentUpdate()
    {
         try
        {
            Inventory.Internalindent obj = new Inventory.Internalindent();
            obj.IndId = gvInternalIndent.SelectedRow.Cells[0].Text;
            obj.Inddate = Yantra.Classes.General.toMMDDYYYY(txtindentdate.Text);
            obj.Indno = txtIndentno.Text;
            obj.from = ddlfrom.Text;
            obj.to = ddlto.Text;
            obj.preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.cpid = cp.getPresentCompanySessionValue();

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
                    obj.InternalIndentDetails_Save();
                }
                MessageBox.Show(this, "Data Updated Successfully");
            }
        }
         catch (Exception ex)
         {
             MessageBox.Show(this, ex.Message);
         }
         finally
         {
             Masters.ClearControls(this);
         }


    }

    private void Indentsave()
    {
        try
        {
            Inventory.Internalindent obj = new Inventory.Internalindent();
            obj.Inddate = Yantra.Classes.General.toMMDDYYYY(txtindentdate.Text);
            obj.Indno = txtIndentno.Text;
            obj.from = ddlfrom.Text;
            obj.to = ddlto.Text;
            obj.preparedby = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.cpid = cp.getPresentCompanySessionValue();

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
                    obj.InternalIndentDetails_Save();
                }
                MessageBox.Show(this, "Data Saved Succesfully");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.ClearControls(this);
        }
    }





    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        tblmain.Visible = false;
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvInternalIndent.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
         if (gvInternalIndent.SelectedIndex > -1)
        {
            try
            {
                Inventory.Internalindent obj = new Inventory.Internalindent();

                if (obj.InternalIndent_Select(gvInternalIndent.SelectedRow.Cells[0].Text) > 0)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    tblmain.Visible = true;
                    txtindentdate.Text = obj.Inddate;
                    txtIndentno.Text = obj.Indno;
                    ddlfrom.SelectedItem.Value = obj.from;
                    ddlto.SelectedItem.Value = obj.to;
                    ddlPreparedBy.SelectedItem.Value = obj.preparedby;
                    obj.InternalindentDetails_Select(gvInternalIndent.SelectedRow.Cells[0].Text, gvIndentdetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                btnDelete.Attributes.Clear();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }


}
 
