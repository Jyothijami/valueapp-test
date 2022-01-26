using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using YantraBLL.Modules;

public partial class Modules_SM_NewLead1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBrand();
            
        }
    }
    protected void LoadBrand()
    {
        SqlConnection connection = new SqlConnection(
        ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);

        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM YANTRA_LKUP_PRODUCT_COMPANY ORDER By PRODUCT_COMPANY_NAME", connection);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        RadComboBox1.DataTextField = "PRODUCT_COMPANY_NAME";
        RadComboBox1.DataValueField = "PRODUCT_COMPANY_ID";
        RadComboBox1.DataSource = dt;
        RadComboBox1.DataBind();
    }
    protected void LoadItemName()
    {
        SqlConnection connection = new SqlConnection(
        ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);

        SqlDataAdapter adapter = new SqlDataAdapter("SELECT ITEM_NAME,ITEM_MODEL_NO ,ITEM_SPEC ,I.ITEM_CODE,PRODUCT_COMPANY_NAME  FROM YANTRA_ITEM_MAST I,YANTRA_LKUP_PRODUCT_COMPANY B where B.PRODUCT_COMPANY_ID = I.BRAND_ID AND I.BRAND_ID=@BranbId and I.IC_ID=@CateID and I.IT_TYPE_ID=@SubCateId ORDER BY I.ITEM_MODEL_NO", connection);
        adapter.SelectCommand.Parameters.AddWithValue("@BranbId", RadComboBox1.SelectedItem .Value  );
        adapter.SelectCommand.Parameters.AddWithValue("@CateID", RadComboBox2.SelectedItem .Value);
        adapter.SelectCommand.Parameters.AddWithValue("@SubCateID", RadComboBox3.SelectedItem.Value);

        DataTable dt = new DataTable();
        adapter.Fill(dt);
        
        RadAutoCompleteBox1.DataTextField = "ITEM_NAME";
        RadAutoCompleteBox1.DataValueField = "ITEM_Code";
        RadAutoCompleteBox1.DataSource = dt;
        RadAutoCompleteBox1.DataBind();
        
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, RadAutoCompleteBox1.UniqueID );
    }

    protected void RadAutoCompleteBox1_EntryAdded(object sender, AutoCompleteEntryEventArgs e)
    {
        //EventLogConsole1.LoggedEvents.Add(String.Format("Entry added: '{0}'", e.Entry.Text));
        txtCode.Text = String.Format("Entry added: '{0}'", e.Entry.Text);
    }
    protected void RadAutoCompleteBox1_EntryRemoved(object sender, AutoCompleteEntryEventArgs e)
    {
        //EventLogConsole1.LoggedEvents.Add(String.Format("Entry removed: '{0}'", e.Entry.Text));
        txtCode.Text = String.Format("Entry removed: '{0}'", e.Entry.Text);
    }
    protected void RadAutoCompleteBox1_TextChanged(object sender, AutoCompleteTextEventArgs e)
    {
        //EventLogConsole1.LoggedEvents.Add(String.Format("Text changed: '{0}'", RadAutoCompleteBox1.Text));
        txtCode.Text = String.Format("Text changed: '{0}'", RadAutoCompleteBox1.Text);
    }
 

    protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox2.Text = "";
        RadComboBox3.Items.Clear();
        RadComboBox3.Text = "";
        LoadCate(e.Value);
    }
    protected void LoadCate(string BrandID)
    {
        SqlConnection connection = new SqlConnection(
        ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);

        // Select a country based on the continentID.
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT distinct a.ITEM_CATEGORY_ID,a.ITEM_CATEGORY_NAME FROM [YANTRA_LKUP_ITEM_CATEGORY] a inner join dbo.YANTRA_ITEM_MAST b on a.ITEM_CATEGORY_ID=b.IC_ID where b.BRAND_ID=@BrandID ORDER By ITEM_CATEGORY_NAME", connection);
        adapter.SelectCommand.Parameters.AddWithValue("@BrandID", BrandID);

        DataTable dt = new DataTable();
        adapter.Fill(dt);

        RadComboBox2.DataTextField = "ITEM_CATEGORY_NAME";
        RadComboBox2.DataValueField = "ITEM_CATEGORY_ID";
        RadComboBox2.DataSource = dt;
        RadComboBox2.DataBind();
    }
    protected void RadComboBox2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //RadComboBox3.Text = "";
        LoadSubCate(e.Value);
    }

    protected void LoadSubCate(string CateID)
    {
        SqlConnection connection = new SqlConnection(
        ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);

        // Select a city based on the countryID.
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [YANTRA_LKUP_ITEM_TYPE] WHERE ITEM_CATEGORY_ID=@CateID ORDER BY IT_TYPE", connection);
        adapter.SelectCommand.Parameters.AddWithValue("@CateID", CateID);

        DataTable dt = new DataTable();
        adapter.Fill(dt);

        RadComboBox3.DataTextField = "IT_TYPE";
        RadComboBox3.DataValueField = "IT_TYPE_ID";
        RadComboBox3.DataSource = dt;
        RadComboBox3.DataBind();
    }
   
    protected void RadComboBox3_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //RadComboBox3.Text = "";

        

    }
}