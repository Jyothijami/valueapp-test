using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //RadGrid1.MasterTableView.EditMode = (GridEditMode)Enum.Parse(typeof(GridEditMode), RadioButtonList1.SelectedValue);

        }
    }

    protected void RadGrid1_PreRender(object sender, System.EventArgs e)
    {
        if (!this.IsPostBack && this.RadGrid1.MasterTableView.Items.Count > 1)
        {
            this.RadGrid1.MasterTableView.Items[1].Edit = true;
            this.RadGrid1.MasterTableView.Rebind();
        }
    }
    private static DataTable GetDataTable(string queryString)
    {
        string ConnString = ConfigurationManager.ConnectionStrings["DBCon1"].ConnectionString;
        SqlConnection MySqlConnection = new SqlConnection(ConnString);
        SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter();
        MySqlDataAdapter.SelectCommand = new SqlCommand(queryString, MySqlConnection);

        DataTable myDataTable = new DataTable();
        MySqlConnection.Open();
        try
        {
            MySqlDataAdapter.Fill(myDataTable);
        }
        finally
        {
            MySqlConnection.Close();
        }

        return myDataTable;
    }
    private DataTable Employees
    {
        get
        {
            object obj = this.Session["yantra_quot_DET"];
            if ((!(obj == null)))
            {
                return ((DataTable)(obj));
            }
            DataTable myDataTable = new DataTable();
            myDataTable = GetDataTable("SELECT * FROM yantra_quot_DET WHERE QUOT_ID =3897 ");
            this.Session["yantra_quot_DET"] = myDataTable;
            return myDataTable;
        }
    }
    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        this.RadGrid1.DataSource = this.Employees;
        this.Employees.PrimaryKey = new DataColumn[] { this.Employees.Columns["QUOT_DET_ID"] };
    }
    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

        //Create new row in the DataSource
        DataRow newRow = this.Employees.NewRow();

        //Insert new values
        Hashtable newValues = new Hashtable();
        newValues["QUOT_ID"] = "3897";
        newValues["ITEM_CODE"] = (userControl.FindControl("ddlModelNo") as DropDownList).SelectedItem.Value;
        newValues["QUOT_DET_QTY"] = (userControl.FindControl("txtQunatity") as TextBox).Text;
        newValues["QUOT_RATE"] = (userControl.FindControl("txtRate") as TextBox).Text;
        newValues["QUOT_DISC"] = (userControl.FindControl("txtDiscount") as TextBox).Text;
        newValues["QUOT_SPPRICE"] = (userControl.FindControl("txtSpPrice") as TextBox).Text;
        newValues["QUOT_ROOM"] = (userControl.FindControl("txtRoom") as TextBox).Text;

        newValues["QUOT_CURRENCY"] = "1";
        newValues["COLOR_ID"] = (userControl.FindControl("ddlColor") as DropDownList).SelectedItem.Value;
        newValues["OPTIONALID"] = "";
        newValues["REMARKS"] = "";
        newValues["QUOT_FLOOR"] = (userControl.FindControl("txtFloor") as TextBox).Text;
        newValues["Quot_OrderNo"] = (userControl.FindControl("txtSrlOrderNo") as TextBox).Text;

        newValues["QUOT_DET_GST"] = (userControl.FindControl("txtGST_Perc") as TextBox).Text;
        newValues["QUOT_DET_GSTRATE"] = (userControl.FindControl("txtGST_Amt") as TextBox).Text;
       

        //make sure that unique primary key value is generated for the inserted row 
        newValues["QUOT_DET_ID"] = (int)this.Employees.Rows[this.Employees.Rows.Count - 1]["QUOT_DET_ID"] + 1;
        try
        {
            foreach (DictionaryEntry entry in newValues)
            {
                newRow[(string)entry.Key] = entry.Value;
            }
            this.Employees.Rows.Add(newRow);
            this.Employees.AcceptChanges();
        }
        catch (Exception ex)
        {
            Label lblError = new Label();
            lblError.Text = "Unable to insert Employees. Reason: " + ex.Message;
            lblError.ForeColor = System.Drawing.Color.Red;
            RadGrid1.Controls.Add(lblError);

            e.Canceled = true;
        }
    }

 
}