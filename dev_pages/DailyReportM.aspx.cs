using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class dev_pages_DailyReportM : System.Web.UI.Page
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
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);

        if (!IsPostBack)
        {
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlAttendedBy);
            HR.EmployeeMaster.EmployeeMaster_SelectSales(ddlBackup);
            Masters.EnquiryMode.EnquiryMode_Select(ddlreference);
            ddlreference.Items.FindByText("--").Text = "Not Selected";
            txtDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            ddlAttendedBy.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            //ddlBackup.SelectedValue = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
            BindTasks();
            Masters.CheckboxListWithStatement(CheckBoxList1, "Select * from YANTRA_LKUP_ITEM_CATEGORY");
            ddlHour.SelectedValue = "10";
            ddlAMPM.SelectedItem.Value = "AM";
            Masters.ProductCompany.ProductCompany_Select(ListBox1);
        }
    }
    private void BindTasks()
    {
        try
        {
            SM.DailyReport obj = new SM.DailyReport();
            if (obj.DailyReport_Tasks(lblEmpIdHidden.Text) > 0)
            {
                txtAchiveYesterday.Text = obj.AchiveYesterday;
                txtachiveToday.Text = obj.AchiveToday;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string st = "";
        foreach (ListItem li in CheckBoxList1.Items)
        {
            if (li.Selected)
            {
                if (st != "")
                {
                    st += ",";
                }

                st = st + li.Value;
            }
        }
        Set_Brand(st);
        foreach (ListItem li in ListBox1.Items)
        {

            //li.Selected = true;
        }
    }

    protected void Set_Brand(string CateId)
    {
        ListBox1.Items.Clear();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = default(SqlDataReader);


        con.Close();
        cmd = new SqlCommand(@"SELECT  Distinct   PRODUCT_COMPANY_NAME, PRODUCT_COMPANY_ID
FROM         YANTRA_LKUP_PRODUCT_COMPANY inner join YANTRA_ITEM_MAST on YANTRA_LKUP_PRODUCT_COMPANY .PRODUCT_COMPANY_ID =YANTRA_ITEM_MAST .BRAND_ID 
where  IC_ID in (" + CateId + ")", con);

        con.Open();

        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        while (dr.Read())
        {
            ListItem li1 = new ListItem();
            li1.Text = dr["PRODUCT_COMPANY_NAME"].ToString();
            li1.Value = dr["PRODUCT_COMPANY_ID"].ToString();

            ListBox1.Items.Add(li1);

        }

        con.Close();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SM.DailyReport objdr = new SM.DailyReport();
        try
        {
            if ( txtDateTime.Text != "" &&
                txtPurpose.Text != "" && txtRemarks.Text != ""  && txtAchiveYesterday.Text != "" && txtachiveToday.Text != ""
                && txtAddress .Text!="")
            {
                objdr.DRDate = Yantra.Classes.General.toMMDDYYYY(txtDateTime.Text);
                objdr.CustName = txtClientsName.Text;
                objdr.Purpose = txtPurpose.Text;
                objdr.Remarks = txtRemarks.Text;
                objdr.DRAttendedBy = ddlAttendedBy.SelectedItem.Value;
                objdr.DRPreparedBy = lblEmpIdHidden.Text;
                objdr.DRAssistedBy = ddlBackup.SelectedItem.Value;
                objdr.Time = "01/01/1900 " + ddlHour.SelectedItem.Value + ":" + ddlMin.SelectedItem.Value + " " + ddlAMPM.SelectedItem.Value;
                objdr.Address = txtAddress.Text;
                objdr.Phone = txtPhoneNo.Text;
                objdr.Reference = ddlreference.SelectedItem.Text;
                if (txtArchitect.Text != "")
                {
                    objdr.Architect = txtArchitect.Text;

                }
                else
                {
                    objdr.Architect = "NA";
                }
                objdr.outTime = "01/01/1900 " + ddlOutHour.SelectedItem.Value + ":" + ddlOutMin.SelectedItem.Value + " " + ddlOutAMPM.SelectedItem.Value;
                objdr.Comment = "";
                //ddlHour.SelectedValue + ":" + ddlMin.SelectedValue + " " + ddlAMPM.SelectedValue;
                objdr.FileName = ddlAction.SelectedItem.Text;
                objdr.DRType = ddlDRType.SelectedItem.Value;
                objdr.DRFollowup = "DailyReport";
                objdr.DRStatus = "Open";
                if (txtEmail.Text != "")
                {
                    objdr.email = txtEmail.Text;

                }
                else
                {
                    objdr.email = "-";
                }
                if (objdr.DailyReports_Save() == "Data Saved Successfully")
                {

                    if (txtachiveToday.Text != null && txtAchiveYesterday.Text != null)
                    {
                        objdr.AchiveYesterday = txtAchiveYesterday.Text;
                        objdr.AchiveToday = txtachiveToday.Text;
                        objdr.EmpID = lblEmpIdHidden.Text;
                        objdr.Date = Yantra.Classes.General.toyymmdd(txtDateTime.Text);
                        objdr.DRTasks_Save();
                    }
                    SM.CustomerMaster objMaster = new SM.CustomerMaster();

                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        if (CheckBoxList1.Items[i].Selected == true)
                        {
                            objMaster.CatId = CheckBoxList1.Items[i].Value;
                            objMaster.CatText = CheckBoxList1.Items[i].Text;
                            objMaster.date = DateTime.Now.ToString();
                            objMaster.CustId = objdr.DRId;
                            objMaster.DR_Analysis_Cat_Save();

                        }
                    }

                    for (int li = 0; li < ListBox1.Items.Count; li++)
                    {
                        if (ListBox1.Items[li].Selected == true)
                        {
                            objMaster.BrandId = ListBox1.Items[li].Value;
                            objMaster.Brand_text = ListBox1.Items[li].Text;
                            objMaster.date = DateTime.Now.ToString();
                            objMaster.CustId = objdr.DRId;

                            objMaster.DR_Analysis_Brand_Save();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "you have missed the data, please do it again");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Unable to raise the request, please try again or contact Admin.");
        }
        finally
        {
            MessageBox.Show(this, "Data Saved Successfully");
            btnRefresh_Click(sender, e);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtClientsName.Text = string.Empty;
        txtPurpose.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        //txtDateTime.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtPhoneNo.Text = string.Empty;
        //ddlAttendedBy.SelectedValue = "0";
        txtArchitect.Text = string.Empty;
    }
}