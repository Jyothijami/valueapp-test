using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Yantra.MessageBox;
using YantraDAL;
using System.Configuration;
using YantraBLL.Modules;
using System.Data;
using System.Data.SqlClient;
using vllib;

public partial class Modules_Services_ServiceCustomerInformation : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string Cust_Code;

    protected void Page_Load(object sender, EventArgs e)
    {
        Cust_Code = Request.QueryString["CustCode"].ToString();
        if (!IsPostBack)
        {
            //txtCustCode.Text = Services.ServiceCustInfo.AutoGenerateCustCode();

            if (Cust_Code != "New")
            {
                try
                {
                    Services.ServiceCustInfo obj = new Services.ServiceCustInfo();
                    if (obj.ServiceCustMaster_Select(Cust_Code) > 0)
                    {
                        txtCustCode.Text = obj.Cust_Code;
                        txtCustName.Text = obj.Cust_Name;
                        txtCompanyName.Text = obj.Cust_Company_Name;
                        txtContactNo.Text = obj.Cust_Mobile;
                        txtCustAddress.Text = obj.Cust_Address;
                        txtEmail.Text = obj.Cust_Email;
                        txtContactPerson.Text = obj.Cust_Contact_Person;
                        SalesOrder_Fill();

                        if(obj.PONo != null && obj.PONo !="")
                        {
                            ddlPONo.SelectedValue = obj.PONo;
                        }

                    }
                    SqlCommand cmd1 = new SqlCommand("select * from Service_Customer_Unit_Details where Cust_Id ='" + Cust_Code + "' ", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
                finally
                {
                    Services.Dispose();
                }
                //Fill_CustomerDetails();
                //ddlPONo_SelectedIndexChanged1(sender, e);
                //gvCustUnitDetails.DataBind();
            }

            else if (Cust_Code == "New")
            {
                genearateRandomCustNo();
                SalesOrder_Fill();
            }
            else
            {
                MessageBox.Show(this, "Please Follow The Flow");
            }
        }
    }

    protected void btnSearchModelNo_Click(object sender, EventArgs e)
    {
        ddlPONo.DataSourceID = "SqlDataSource1";
        ddlPONo.DataTextField = "SO_NO";
        ddlPONo.DataValueField = "SO_ID";
        ddlPONo.DataBind();
        ddlPONo_SelectedIndexChanged1(sender, e);
    }
    private void SalesOrder_Fill()
    {
        try
        {
            SM.SalesOrder.SalesOrder_Select(ddlPONo);
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
    private void Fill_CustomerDetails()
    {
        SqlCommand cmd = new SqlCommand("select * from Service_Customer_Information where Cust_Id ='" + Cust_Code + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtCustCode.Text = Cust_Code;
            txtCustName.Text = dt.Rows[0][2].ToString();
            txtCompanyName.Text = dt.Rows[0][3].ToString();
            txtContactPerson.Text = dt.Rows[0][4].ToString();
            txtContactNo.Text = dt.Rows[0][5].ToString();
            txtEmail.Text = dt.Rows[0][6].ToString();
            txtCustAddress.Text = dt.Rows[0][7].ToString();
            txtReference.Text = dt.Rows[0][8].ToString();
            ddlPONo.SelectedValue = dt.Rows[0][9].ToString();
            //ddlPONo_SelectedIndexChanged1(sender, e);

        }

        SqlCommand cmd1 = new SqlCommand("select * from Service_Customer_Unit_Details where Cust_Id ='" + Cust_Code + "' ", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }

    private void genearateRandomCustNo()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        txtCustCode.Text = intRandomNumber.ToString();
    }
    protected void btnAddUnit_Click(object sender, EventArgs e)
    {
        DataTable CustomerUnits = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Cust_Unit_Name");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Cust_Unit_Address");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Unit_Contact_Person");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Contact_Mobile");
        CustomerUnits.Columns.Add(col);
        col = new DataColumn("Email");
        CustomerUnits.Columns.Add(col);
        if (gvCustUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustUnitDetails.Rows)
            {
                if (gvCustUnitDetails.SelectedIndex > -1)
                {
                    if (gvrow.RowIndex == gvCustUnitDetails.SelectedRow.RowIndex)
                    {
                        DataRow dr = CustomerUnits.NewRow();
                        if (txtUnitName.Text == "")
                        {
                            txtUnitName.Text = "-";
                        }
                        dr["Cust_Unit_Name"] = txtUnitName.Text;
                        if (txtUnitAddress.Text == "")
                        {
                            txtUnitAddress.Text = "-";
                        }
                        dr["Cust_Unit_Address"] = txtUnitAddress.Text;
                        if (txtUnitContactPerson.Text == "")
                        {
                            txtUnitContactPerson.Text = "-";
                        }
                        dr["Unit_Contact_Person"] = txtUnitContactPerson.Text;
                        if (txtUnitContactNo.Text == "")
                        {
                            txtUnitContactNo.Text = "-";
                        }

                        dr["Contact_Mobile"] = txtUnitContactNo.Text;
                        if (txtUnitEmail.Text == "")
                        {
                            txtUnitEmail.Text = "-";
                        }
                        dr["Email"] = txtUnitEmail.Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                    else
                    {
                        //LinkButton lnkUnit = (LinkButton)gvrow.FindControl("lbtnUnitName");

                        DataRow dr = CustomerUnits.NewRow();
                        dr["Cust_Unit_Name"] = gvrow.Cells[1].Text;
                        dr["Cust_Unit_Address"] = gvrow.Cells[2].Text;
                        dr["Unit_Contact_Person"] = gvrow.Cells[3].Text;
                        dr["Contact_Mobile"] = gvrow.Cells[4].Text;
                        dr["Email"] = gvrow.Cells[5].Text;
                        CustomerUnits.Rows.Add(dr);
                    }
                }
                else
                {
                    //LinkButton lnkUnit = (LinkButton)gvrow.FindControl("lbtnUnitName");

                    DataRow dr = CustomerUnits.NewRow();
                    dr["Cust_Unit_Name"] = gvrow.Cells[1].Text;
                    dr["Cust_Unit_Address"] = gvrow.Cells[2].Text;
                    dr["Unit_Contact_Person"] = gvrow.Cells[3].Text;
                    dr["Contact_Mobile"] = gvrow.Cells[4].Text;
                    dr["Email"] = gvrow.Cells[5].Text;
                    CustomerUnits.Rows.Add(dr);
                }
            }
        }

        if (gvCustUnitDetails.Rows.Count > 0)
        {
            if (gvCustUnitDetails.SelectedIndex == -1)
            {
                foreach (GridViewRow gvrow in gvCustUnitDetails.Rows)
                {
                    //LinkButton lnkUnit = (LinkButton)gvrow.FindControl("lbtnUnitName");
                    if (gvrow.Cells[1].Text == txtUnitName.Text)
                    {
                        gvCustUnitDetails.DataSource = CustomerUnits;
                        gvCustUnitDetails.DataBind();
                        MessageBox.Show(this, "The Unit Name you have selected is already exists in list");
                        return;
                    }
                }
            }
        }

        if (gvCustUnitDetails.SelectedIndex == -1)
        {
            DataRow drnew = CustomerUnits.NewRow();
            if (txtUnitName.Text == "")
            {
                txtUnitName.Text = "-";
            }
            drnew["Cust_Unit_Name"] = txtUnitName.Text;
            if (txtUnitAddress.Text == "")
            {
                txtUnitAddress.Text = "-";
            }
            drnew["Cust_Unit_Address"] = txtUnitAddress.Text;
            if (txtUnitContactPerson.Text == "")
            {
                txtUnitContactPerson.Text = "-";
            }
            drnew["Unit_Contact_Person"] = txtUnitContactPerson.Text;
            if (txtUnitContactNo.Text == "")
            {
                txtUnitContactNo.Text = "-";
            }

            drnew["Contact_Mobile"] = txtUnitContactNo.Text;
            if (txtUnitEmail.Text == "")
            {
                txtUnitEmail.Text = "-";
            }
            drnew["Email"] = txtUnitEmail.Text;
            CustomerUnits.Rows.Add(drnew);
        }
        gvCustUnitDetails.DataSource = CustomerUnits;
        gvCustUnitDetails.DataBind();
        ClearUnitFields();

    }

    private void ClearUnitFields()
    {
        txtUnitAddress.Text = txtUnitName.Text = txtUnitContactPerson.Text = txtUnitContactNo.Text = "";
        txtUnitEmail.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUnitFields();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveCustDetails();
        ClearAllFields();
    }

    private void ClearAllFields()
    {
        txtCustCode.Text = txtCustName.Text = txtCustAddress.Text = txtCompanyName.Text = txtContactPerson.Text = txtContactNo.Text = txtEmail.Text = "";
        gvCustUnitDetails.DataSource = null;
        gvCustUnitDetails.DataBind();
        ClearUnitFields();
    }
    private void SaveCustDetails()
    {
        Services.ServiceCustInfo obj = new Services.ServiceCustInfo();
        obj.Cust_Code = txtCustCode.Text;
        obj.Cust_Name = txtCustName.Text;
        obj.Cust_Company_Name = txtCompanyName.Text;
        obj.Cust_Contact_Person = txtContactPerson.Text;
        obj.Cust_Mobile = txtContactNo.Text;
        obj.Cust_Email = txtEmail.Text;
        obj.Cust_Address = txtCustAddress.Text;
        obj.Add_Reference = txtReference.Text;

        if (Convert.ToInt32(ddlPONo.SelectedItem.Value ) > 0)
        {
            obj.PONo = ddlPONo.SelectedItem.Value;
        }
        else
        {
            obj.PONo = "0";
        }
        if (Cust_Code != "New")
        {
            obj.UpdateCustInfo();
            ///obj.DeleteUnitCustInfo(txtCustCode.Text);
            SaveCustUnitDetails();
            //Response.Redirect("Service_Customers.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
        "alert(' Data Updated sucessfully');window.location ='Service_Customers.aspx';", true);
        }
        else
        {

            obj.InsertCustInfo();
            SaveCustUnitDetails();
            MessageBox.Show(this, "Data Saved");
            //Response.Redirect("Service_Customers.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
        "alert(' Data Saved sucessfully');window.location ='Service_Customers.aspx';", true);
        }

    }

    private void SaveCustUnitDetails()
    {

        if (gvCustUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustUnitDetails.Rows)
            {

                //LinkButton lbtnUnitName = (LinkButton)gvrow.FindControl("lbtnUnitName");

                string UnitName = gvrow.Cells[1].Text.ToString();
                string UnitAddress = gvrow.Cells[2].Text.ToString();
                string ContactPerson = gvrow.Cells[3].Text.ToString();
                string ContactNo = gvrow.Cells[4].Text.ToString();

                Services.ServiceCustInfo obj = new Services.ServiceCustInfo();
                //Services.BeginTransaction();
                obj.Cust_ID = Cust_Code;
                obj.Cust_Unit_Name = UnitName;
                obj.Cust_Unit_Address = UnitAddress;
                obj.Unit_Contact_Person = ContactPerson;
                obj.Contact_Mobile = ContactNo;
                obj.UnitEmail = gvrow.Cells[5].Text;
                if (Cust_Code != "New")
                {

                    MessageBox.Show(this, obj.UpdateCustUnitInfo());
                }
                else
                {
                    MessageBox.Show(this, obj.InsertCustUnitInfo());
                }

            }
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }

    protected void gvCustUnitDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Unit from Customer Unit Details list?');");
        }
    }
    protected void gvCustUnitDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string x1 = gvCustUnitDetails.Rows[e.RowIndex].Cells[1].Text;
        DataTable custDetails = new DataTable();
        DataColumn col = new DataColumn();
        col = new DataColumn("Cust_Unit_Name");
        custDetails.Columns.Add(col);
        col = new DataColumn("Cust_Unit_Address");
        custDetails.Columns.Add(col);
        col = new DataColumn("Unit_Contact_Person");
        custDetails.Columns.Add(col);
        col = new DataColumn("Contact_Mobile");
        custDetails.Columns.Add(col);
        col = new DataColumn("Email");
        custDetails.Columns.Add(col);


        if (gvCustUnitDetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gvCustUnitDetails.Rows)
            {
                if (gvrow.RowIndex != e.RowIndex)
                {
                    DataRow dr = custDetails.NewRow();
                    dr["Cust_Unit_Name"] = gvrow.Cells[1].Text;
                    dr["Cust_Unit_Address"] = gvrow.Cells[2].Text;
                    dr["Unit_Contact_Person"] = gvrow.Cells[3].Text;
                    dr["Contact_Mobile"] = gvrow.Cells[4].Text;
                    dr["Email"] = gvrow.Cells[5].Text;

                    custDetails.Rows.Add(dr);
                }
            }
        }
        gvCustUnitDetails.DataSource = custDetails;
        gvCustUnitDetails.DataBind();
    }

    protected void ddlPONo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            Services.ServiceCustInfo objPO = new Services.ServiceCustInfo();
            if (objPO.CustomerMaster_SelelctPONo(ddlPONo.SelectedItem.Value) > 0)
            {
                txtCustName.Text = objPO.SOCustName;
                txtCompanyName.Text = objPO.SOCompanyName;
                txtContactPerson.Text = objPO.SOContactPerson;
                txtContactNo.Text = objPO.SOMobileNO;
                txtEmail.Text = objPO.SOEmail;
                txtCustAddress.Text = objPO.SOAddress;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            // btnDelete.Attributes.Clear();
            SM.Dispose();
        }
    }
}
 
