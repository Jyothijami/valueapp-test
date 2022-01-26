using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;
using YantraBLL.Modules;
using System.Data;
using System.Data.SqlClient;
using vllib;


public partial class Modules_Services_SparePartsIndent : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            gvSparePartsIndent.DataBind();
            txtIndentDate.Text = DateTime.Now.ToString();
            genearateRandomIndentNo();
            setControlsVisibility();
        }
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "35");
        btnAddSpareIndent.Enabled = up.add;
        btnSave.Enabled = up.add;
        //btnRefresh.Enabled = up.Refresh;
        //btnDelete.Enabled = up.Delete;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void genearateRandomIndentNo()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        txtIndentNo.Text = intRandomNumber.ToString();
    }
    protected void btnAddSpareIndent_Click(object sender, EventArgs e)
    {

        DataTable VisitReport = new DataTable();
        DataColumn col = new DataColumn();

        col = new DataColumn("Description");
        VisitReport.Columns.Add(col);

        col = new DataColumn("Brand");
        VisitReport.Columns.Add(col);

        col = new DataColumn("Model");
        VisitReport.Columns.Add(col);

        col = new DataColumn("Code");
        VisitReport.Columns.Add(col);

        col = new DataColumn("Quantity");
        VisitReport.Columns.Add(col);

        //col = new DataColumn("IndentDate");
        //VisitReport.Columns.Add(col);

        col = new DataColumn("ClientAddress");
        VisitReport.Columns.Add(col);

        if (gvIndentDetails.Rows.Count > 0)
        {

            foreach (GridViewRow gvrow in gvIndentDetails.Rows)
            {
                if (gvIndentDetails.SelectedIndex > -1)
                {

                    if (gvrow.RowIndex == gvIndentDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = VisitReport.NewRow();
                        dr["Description"] = txtDescription.Text;
                        dr["Brand"] = txtBrand.Text;
                        dr["Model"] = txtModel.Text;
                        dr["Code"] = txtCode.Text;
                        dr["Quantity"] = txtQuantity.Text;
                        //dr["IndentDate"] = txtIndentDate.Text;
                        dr["ClientAddress"] = txtClientAddress.Text;


                        VisitReport.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = VisitReport.NewRow();
                        dr["Description"] = gvrow.Cells[0].Text;
                        dr["Brand"] = gvrow.Cells[1].Text;
                        dr["Model"] = gvrow.Cells[2].Text;
                        dr["Code"] = gvrow.Cells[3].Text;
                        dr["Quantity"] = gvrow.Cells[4].Text;
                        //dr["IndentDate"] = gvrow.Cells[5].Text;
                        dr["ClientAddress"] = gvrow.Cells[5].Text;

                        VisitReport.Rows.Add(dr);
                    }
                }
                else
                {
                    
                    DataRow dr = VisitReport.NewRow();
                    dr["Description"] = gvrow.Cells[0].Text;
                    dr["Brand"] = gvrow.Cells[1].Text;
                    dr["Model"] = gvrow.Cells[2].Text;
                    dr["Code"] = gvrow.Cells[3].Text;
                    dr["Quantity"] = gvrow.Cells[4].Text;
                    //dr["IndentDate"] = gvrow.Cells[5].Text;
                    dr["ClientAddress"] = gvrow.Cells[5].Text;
                    VisitReport.Rows.Add(dr);
                }
            }
        }


        if (gvIndentDetails.SelectedIndex == -1)
        {
            DataRow drnew = VisitReport.NewRow();
            drnew["Description"] = txtDescription.Text;
            drnew["Brand"] = txtBrand.Text;
            drnew["Model"] = txtModel.Text;
            drnew["Code"] = txtCode.Text;
            drnew["Quantity"] = txtQuantity.Text;
            //drnew["IndentDate"] = txtIndentDate.Text;
            drnew["ClientAddress"] = txtClientAddress.Text;
            VisitReport.Rows.Add(drnew);
        }

        gvIndentDetails.DataSource = VisitReport;
        gvIndentDetails.DataBind();
        ClearFields();
        
    }

    private void ClearFields()
    {
        txtBrand.Text = txtModel.Text = txtCode.Text = txtQuantity.Text = txtDescription.Text = txtClientAddress.Text = "";
        
    }
    protected void btnRefreshAll_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveSpareIndentDetails();
        ClearAllFields();
        gvSparePartsIndent.DataBind();
        txtIndentDate.Text = DateTime.Now.ToString();
    }

    private void SaveSpareIndentDetails()
    {
        if (gvIndentDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvIndentDetails.Rows)
            {

                string Description = gvrow.Cells[0].Text.ToString();
                string Brand = gvrow.Cells[1].Text.ToString();
                string Model = gvrow.Cells[2].Text.ToString();
                string Code = gvrow.Cells[3].Text.ToString();
                string Quantity = gvrow.Cells[4].Text.ToString();
                //string IndentDate = gvrow.Cells[5].Text.ToString();
                string ClientAddress = gvrow.Cells[5].Text.ToString();

                Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
                obj.Indent_No = txtIndentNo.Text;
                obj.IndentDate = txtIndentDate.Text;
                obj.Brand = Brand;
                obj.Model = Model;
                obj.Code = Code;
                obj.Quantity = Quantity;
                obj.Description = Description;
                obj.ClientAddress = ClientAddress;
                obj.Available = "";
                obj.Indent = "";
                obj.Technician_Id=obj.TechnicianName=obj.StorePerson_Id=obj.StorePerson_Name=obj.HeadTech_Id=obj.HeadTech_Name=obj.PurchasePerson_Id=obj.PurchasePerson_Name ="";
                MessageBox.Show(this, obj.InsertSpareIndentDetails());

            }
        }
        else
        {
            MessageBox.Show(this, "Please Raise Indent For Atleast One Item To Save Details");
        }
    }

    private void ClearAllFields()
    {
        txtIndentNo.Text = "";
        gvIndentDetails.DataSource = null;
        gvIndentDetails.DataBind();
        ClearFields();
        genearateRandomIndentNo();
    }
    protected void lbtnIndentNo_Click(object sender, EventArgs e)
    {
        LinkButton lbtnIndentNo = (LinkButton)sender;
        GridViewRow Row = (GridViewRow)lbtnIndentNo.Parent.Parent;
        gvSparePartsIndent.SelectedIndex = Row.RowIndex;
        string str = lbtnIndentNo.Text;
        Response.Redirect("SparesIndentPrint.aspx?IndentNo=" + str);        

    }
}
 
