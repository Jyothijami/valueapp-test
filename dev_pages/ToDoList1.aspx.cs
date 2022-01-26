using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using YantraBLL.Modules;
using Yantra.MessageBox;

public partial class ToDoList1 : System.Web.UI.Page
{
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (System.Web.HttpContext.Current.Session["YantraSession"] == null)
        {
            Response.Redirect("~/MobileLogin.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            //txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);

        }
    }
    private void BindData()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString()))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT Emp_first_name +' '+Emp_Last_Name as USER_NAME ,Emp_ID FROM YANTRA_EMPLOYEE_MAST  where status =1 ORDER BY Emp_first_name";
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                // BIND THE SELECT DROP-DOWN LIST WITH A DATABASE TABLE.
                Books.DataSource = dt;
                Books.DataTextField = "USER_NAME";
                Books.DataValueField = "Emp_ID";

                Books.DataBind();

                // FOR MULTIPLE SELECTION. SET THE VALUE AS FALSE, AND SEE WHAT HAPPENS.
                Books.Multiple = true;
            }
        }


        // SM.CustomerMaster.CustomerMaster_Select(Books);


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        objdr.Date = DateTime.Now.ToString();
        objdr.Subject = txtSubject.Text;
        objdr.IssuedDate = txtDateTime.Text;
        objdr.Description = txtDescr.Text;
        objdr.Status = ddlActivity.SelectedItem.Text;
        objdr.PreparedBy = lblEmpIdHidden.Text;

        if (objdr.ToDo_List_Save() == "Data Saved Successfully")
        {
            foreach (ListItem item in Books.Items)
            {
                if (item.Selected)
                {

                    objdr.EmpID = item.Value.ToString();
                    objdr.ToDO_List_Det_Save();

                }
            }
        }
        MessageBox.Show(this, "Data Saved Suucessfully");
        btnRefresh1_Click(sender, e);

    }
    protected void btnRefresh1_Click(object sender, EventArgs e)
    {
        HR.ClearControls(this);
    }
}