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


public partial class Modules_Services_SparesIndentPrint : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            lblDate.Text = DateTime.Now.ToString();
            lblIndentNo.Text = Request.QueryString["IndentNo"].ToString();
            FillGridView();
        }
    }

    private void FillGridView()
    {
        SqlCommand cmd1 = new SqlCommand("select * from SparesIndent_tbl where Indent_No ='" + lblIndentNo.Text + "' ", con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        gvSpareIndentPrint.DataSource = dt1;
        gvSpareIndentPrint.DataBind();
        txtClientAddress.Text = dt1.Rows[0][8].ToString();
        lblTechnician.Text = dt1.Rows[0][12].ToString();
        lblStores.Text = dt1.Rows[0][14].ToString();
        lblHead.Text = dt1.Rows[0][16].ToString();
        lblPurchase.Text = dt1.Rows[0][18].ToString();
            
        if(lblTechnician.Text==""){ btnTechApprove.Visible = true;} else if(lblTechnician.Text!=""){btnTechApprove.Visible = false;}
        if(lblStores.Text==""){ btnStoresApprove.Visible = true;} else if(lblStores.Text!=""){btnStoresApprove.Visible = false;}
        if(lblHead.Text==""){ btnHeadApprove.Visible = true;} else if(lblHead.Text!=""){btnHeadApprove.Visible = false;}
        if(lblPurchase.Text==""){ btnPurchaseApprove.Visible = true;} else if(lblPurchase.Text!=""){btnPurchaseApprove.Visible = false;}

        
    }
    protected void btnTechApprove_Click(object sender, EventArgs e)
    {              
            Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
            obj.Indent_No = lblIndentNo.Text;
            obj.Technician_Id = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.Technician_Name = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            MessageBox.Show(this, obj.UpdateSpareIndentTechInfo());
            lblTechnician.Text =   Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            btnTechApprove.Visible = false;
            //gvSpareIndentPrint.DataBind();
    }
    protected void btnStoresApprove_Click(object sender, EventArgs e)
    {
            btnStoresApprove.Visible = true;
            Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
            obj.Indent_No = lblIndentNo.Text;
            obj.StorePerson_Id = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.StorePerson_Name = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            MessageBox.Show(this, obj.UpdateSpareIndentStoreInfo());
            lblStores.Text =   Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            btnStoresApprove.Visible = false;

            //gvSpareIndentPrint.DataBind();        
    }
    protected void btnHeadApprove_Click(object sender, EventArgs e)
    {      
            btnHeadApprove.Visible = true;
            Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
            obj.Indent_No = lblIndentNo.Text;
            obj.HeadTech_Id = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.HeadTech_Name = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            MessageBox.Show(this, obj.UpdateSpareIndentHeadInfo());
            lblHead.Text =   Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            btnHeadApprove.Visible = false;

            //gvSpareIndentPrint.DataBind();
    }
    protected void btnPurchaseApprove_Click(object sender, EventArgs e)
    {
            btnPurchaseApprove.Visible = true;
            Services.ServiceSiteReport obj = new Services.ServiceSiteReport();
            obj.Indent_No = lblIndentNo.Text;
            obj.PurchasePerson_Id = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            obj.PurchasePerson_Name = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            MessageBox.Show(this, obj.UpdateSpareIndentPurchaseInfo());
            lblPurchase.Text =   Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
            btnPurchaseApprove.Visible = false;

            //gvSpareIndentPrint.DataBind();       

    }
}
 
