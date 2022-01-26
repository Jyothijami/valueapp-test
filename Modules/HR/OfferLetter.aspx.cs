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
using YantraDAL;
using Yantra.Classes;
using System.Data.SqlClient;
using vllib;
public partial class Modules_HR_OfferLetter : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
    string EnrollmentId = "";
    ScriptManager ScriptManagerLocal;
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();
        //ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        //txtDate.Text = System.DateTime.Now.Date.ToString();
        //if (!IsPostBack)
        //{


        //    FillCompany();
        //    FillDept();
        //}
    }

    //#region FillCompany
    //private void FillCompany()
    //{
    //    try
    //    {

    //        Masters.Circular.Company_Select(ddlCompanyid);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message.ToString());
    //    }

    //}
    //#endregion

    //private void FillDept()
    //{
    //    try
    //    {
    //        Masters.Circular.Dept_Select(ddlDept);

    //    }
    //    catch
    //    {
             
    //    }
    //}
    //protected void btnClose_Click(object sender, EventArgs e)
    //{
    //    tblemp.Visible = false;
    //    tblcircular.Visible = false;
    //}
    //protected void btnRefresh_Click(object sender, EventArgs e)
    //{
    //    Masters.ClearControls(this);
    //}
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if (btnSave.Text == "Save")
    //    {
    //        OfferLetterSave();
    //        tblemp.Visible = false;
    //        tblcircular.Visible = false;
    //    }
    //    if (btnSave.Text == "Update")
    //    {
    //        OfferLetterUpdate();
    //        tblemp.Visible = false;
    //        tblcircular.Visible = false;
    //    }
    //}

    //private void OfferLetterUpdate()
    //{
    //    try
    //    {
    //        Masters.OfferLetter objMaster = new Masters.OfferLetter();
    //        objMaster.appid = gvCircular.SelectedRow.Cells[0].Text;
    //        objMaster.Appno = txtcirNo.Text;
    //        objMaster.Companyid = ddlCompanyid.SelectedItem.Value;
    //        objMaster.empname = txtempname.Text;
    //        objMaster.designation = txtDesignation.Text;
    //        objMaster.dept_id = ddlDept.SelectedItem.Value;
    //        objMaster.mobileno = txtMobileno.Text;
    //        objMaster.issueddate = txtDate.Text;
    //        objMaster.description = txtdescription.Text;
    //        objMaster.Email = txtEmail.Text;
    //        MessageBox.Show(this, objMaster.OfferLetter_Update());

    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {

    //        gvCircular.DataBind();

    //        Masters.Dispose();
    //    }
    //}

    //#region OfferLetterSave
    //private void OfferLetterSave()
    //{
    //    try
    //    {
    //        Masters.OfferLetter objMaster = new Masters.OfferLetter();
    //        objMaster.Appno = txtcirNo.Text;
    //        objMaster.Companyid = ddlCompanyid.SelectedItem.Value;
    //        objMaster.empname = txtempname.Text;
    //        objMaster.designation = txtDesignation.Text;
    //        objMaster.dept_id = ddlDept.SelectedItem.Value;
    //        objMaster.mobileno = txtMobileno.Text;
    //        objMaster.issueddate = txtDate.Text;
    //        objMaster.description = txtdescription.Text;
    //        objMaster.Email = txtEmail.Text;
    //        MessageBox.Show(this, objMaster.OfferLetter_Save());

            
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {

    //        gvCircular.DataBind();
    //        Masters.ClearControls(this);
    //        Masters.Dispose();
    //    }
    //}
    //#endregion



   
    //protected void gvCircular_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    if (gvCircular.SelectedIndex > -1)
    //    {
    //        try
    //        {
    //            Masters.OfferLetter objMaster = new Masters.OfferLetter();
    //            objMaster.appid = gvCircular.SelectedRow.Cells[0].Text;
    //            MessageBox.Show(this, objMaster.OfferLetter_Delete());
    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(this, ex.Message);
    //        }
    //        finally
    //        {
    //            tblemp.Visible = false;
    //            tblcircular.Visible = false;
    //            gvCircular.DataBind();
    //            gvCircular.SelectedIndex = -1;

    //            Masters.ClearControls(this);
    //            Masters.Dispose();
    //        }
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast a Record");
    //    }
    //}
    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    if (gvCircular.SelectedIndex > -1)
    //    {
    //        tblemp.Visible = true;
    //        tblcircular.Visible = true;
    //        Masters.OfferLetter objmas = new Masters.OfferLetter();
    //        objmas.OfferLetter_Ddl(gvCircular.SelectedRow.Cells[0].Text);

    //        ddlCompanyid.SelectedValue = objmas.Companyid;

    //        ddlDept.SelectedValue = objmas.dept_id;
    //        //ddlEmployee.SelectedValue = gvCircular.SelectedRow.Cells[2].Text;
    //        txtempname.Text = objmas.empname;
    //        txtcirNo.Text = objmas.Appno;
    //        txtDate.Text = objmas.issueddate;
    //        txtdescription.Text = objmas.description;
    //        txtMobileno.Text = objmas.mobileno;
    //        txtDesignation.Text = objmas.designation;
    //        txtEmail.Text = objmas.Email;

    //        btnSave.Text = "Update";
    //    }

    //    else
    //    {
    //        MessageBox.Show(this, "Please select atleast one Record");
    //    }
    //}
    //protected void btnNew_Click(object sender, EventArgs e)
    //{
    //    Masters.ClearControls(this);
    //    txtcirNo.Text = Masters.Circular.Offer_AutoGenCode();
    //    btnSave.Text = "Save";
    //    tblemp.Visible = true;
    //    tblcircular.Visible = true;
    //}
    //protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtSearchText.Text = "";
    //  //  ScriptManagerLocal.SetFocus(txtSearchText);
    //}
    //protected void btnSearchGo_Click(object sender, EventArgs e)
    //{
    //    lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
    //    lblSearchValueHidden.Text = txtSearchText.Text;
    //    gvCircular.DataBind();
    //}
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Yantra.Classes.Email mail = new Yantra.Classes.Email(this.txtEmail.Text.Trim().ToLower());
    //    //for defining  the format
    //    mail.BodyFormat = System.Web.Mail.MailFormat.Html;
    //    // reading  the formated htmx  format file :
    //    System.IO.StreamReader sr = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Mail_Formats\\EmpOffer.htmx");
    //    mail.Body = sr.ReadToEnd();

    //    sr.Close();
    //    // to send to htmx file
    //    mail.Body = mail.Body.Replace("_UserTitle_", txtempname.Text);
    //    mail.Body = mail.Body.Replace("_EmpName_", txtempname.Text);
    //    mail.Body = mail.Body.Replace("_Comp_", ddlCompanyid.SelectedItem.Text);
    //    mail.Body = mail.Body.Replace("_Department_", ddlDept.SelectedItem.Text);
    //    mail.Body = mail.Body.Replace("_Designation_", txtDesignation.Text);
    //    mail.Body = mail.Body.Replace("_Details_", txtdescription.Text);

        
    //    mail.Subject = "Offer Letter";
    //    mail.From = "pramodbmk@gmail.com";


    //    try
    //    {
    //        mail.Send();
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, "Could not send your password. please try again.");
    //    }
    //}
    //protected void lbtnCirNo_Click(object sender, EventArgs e)
    //{
    //    LinkButton lbtnCirNo;
    //    lbtnCirNo = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lbtnCirNo.Parent.Parent;
    //    gvCircular.SelectedIndex = gvRow.RowIndex;
    //    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    //    if (gvCircular.SelectedIndex > -1)
    //    {
    //        Masters.OfferLetter objmas = new Masters.OfferLetter();
    //        objmas.OfferLetter_Ddl(gvCircular.SelectedRow.Cells[0].Text);

    //        ddlCompanyid.SelectedValue = objmas.Companyid;

    //        ddlDept.SelectedValue = objmas.dept_id;
    //        //ddlEmployee.SelectedValue = gvCircular.SelectedRow.Cells[2].Text;
    //        txtempname.Text = objmas.empname;
    //        txtcirNo.Text = objmas.Appno;
    //        txtDate.Text = objmas.issueddate;
    //        txtdescription.Text = objmas.description;
    //        txtMobileno.Text = objmas.mobileno;
    //        txtDesignation.Text = objmas.designation;
    //        txtEmail.Text = objmas.Email;
    //        btnSave.Text = "Update";
    //    }
    //}

    //protected void btnApprove_Click(object sender, EventArgs e)
    //{
    //    //Button bt = (Button)sender;
    //    //GridViewRow gv = (GridViewRow)bt.NamingContainer;
    //    //string id = gvOfferLetter.DataKeys[gv.DataItemIndex]["lblNo"].ToString();
    //    foreach(GridViewRow gvr in gvOfferLetter.Rows)
    //    {
    //        Label id = (Label)gvr.FindControl("lblNo");
    //        string EnrollId=id.Text;
    //    }
    //}
    protected void gvOfferLetter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // string country = (gvOfferLetter.SelectedRow.FindControl("lblCountry") as Label).Text;
         EnrollmentId = e.CommandArgument.ToString();
        if (e.CommandName == "Approve")
        {
            Response.Redirect("EmployeeMaster.aspx?EnrollId=" + EnrollmentId);
        }
        else if(e.CommandName=="Reject")
        {
            RejectOffer();
            gvOfferLetter.DataBind();
        }
    }

    private void RejectOffer()
    {
        //EnrollmentId = e.CommandArgument.ToString();
        SqlCommand cmd = new SqlCommand("USP_UpdateOfferStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@EnrollmentId", EnrollmentId);
        cmd.Parameters.AddWithValue("@OfferStatus", "Rejected");
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "77");
        gvOfferLetter.Columns[8].Visible = up.Approve;
        gvOfferLetter.Columns[9].Visible = up.Delete;
        

    }
}
