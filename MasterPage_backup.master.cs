using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Yantra.MessageBox;
using System.IO;
using System;
using vllib;

public partial class MasterPage_backup : System.Web.UI.MasterPage
{
    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        Yantra.Authentication.Session_Check(this);
        lblUserName.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpName);
        lblEmpIdHidden.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
        //string EmpIdTemp;
        //imgComapany.ImageUrl = "~/Modules/Masters/ComapanyImage.ashx?id=" + Yantra.Authentication.Logged_EMP_Details.EmpId + "";

        imgComapany.ImageUrl = "~/Modules/Masters/ComapanyImage.ashx?id=" + cp.getPresentCompanySessionValue();
        lblCName.Text = "";
        lblCName.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.CmpName);

        if (!IsPostBack)
        {
            //imgComapany.ImageUrl = "~/Modules/Masters/ComapanyImage.ashx?id="+ Yantra.Authentication.CmpId +"";
            //lblCName.Text = "";
            //lblCName.Text = Yantra.Authentication.CmpName;
            Yantra.Authentication.UserPrivilegesFill(HiddenFieldDoNotRemove);
            lblCurrentDateTime.Text = DateTime.Now.ToLongDateString();
            HiddenField1.Value = "false";
            //if (GetPermission("Permission_FullControl") > 0) { LinkButton1.Visible = true; }
            //else
            //{
            //    LinkButton1.Visible = false;
            //    try
            //    {
            //        if (GetPermission("Permission_ShowIndividual") > 0)
            //        {
            //            ((Label)ContentPlaceHolderBody.FindControl("lblEmpIdHidden")).Text = lblEmpIdHidden.Text;
            //        }
            //        else
            //        {
            //            ((Label)ContentPlaceHolderBody.FindControl("lblEmpIdHidden")).Text = "0";
            //        }
            //    }
            //    catch { }
            //    try
            //    {
            //        if (GetPermission("Permission_ShowAll") > 0)
            //        {
            //            ((Label)ContentPlaceHolderBody.FindControl("lblEmpIdHidden")).Text = "0";
            //        }
            //    }
            //    catch { }
            //}
        }
    }
    #endregion

    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    //    //if (LinkButton1.Visible == true)
    //    //{
    //    //    if (HiddenField1.Value == "true")
    //    //    {
    //    //        try
    //    //        {
    //    //            ContentPlaceHolderBody.FindControl("btnSave").Visible = true;
    //    //            ContentPlaceHolderBody.FindControl("btnEdit").Visible = true;
    //    //            //ContentPlaceHolderBody.FindControl("btnDelete").Visible = true;
    //    //        }
    //    //        catch
    //    //        {
    //    //            MessageBox.Show(this, "No Control Found");
    //    //        }
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Add") > 0)
    //    //        {                    //ContentPlaceHolderBody.FindControl("btnNew").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnNew").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Edit") > 0)
    //    //        {                    //ContentPlaceHolderBody.FindControl("btnEdit").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnEdit").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Approve") > 0)
    //    //        {                    //ContentPlaceHolderBody.FindControl("btnApprove").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnApprove").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Regret") > 0)
    //    //        {                    //ContentPlaceHolderBody.FindControl("btnRegret").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnRegret").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Delete") > 0)
    //    //        {                    //ContentPlaceHolderBody.FindControl("btnDelete").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnDelete").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Print") > 0)
    //    //        {//ContentPlaceHolderBody.FindControl("btnPrint").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnPrint").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Print") > 0)
    //    //        {//ContentPlaceHolderBody.FindControl("btnPrint").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnPrintTB").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_Print") > 0)
    //    //        {//ContentPlaceHolderBody.FindControl("btnPrint").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnPrintCB").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    try
    //    //    {
    //    //        if (GetPermission("Permission_E-Mail") > 0)
    //    //        {                    //ContentPlaceHolderBody.FindControl("btnSend").Visible = true;
    //    //        }
    //    //        else { ContentPlaceHolderBody.FindControl("btnSend").Visible = false; }
    //    //    }
    //    //    catch { }
    //    //    //try
    //    //    //{
    //    //    //    if (GetPermission("Permission_ShowIndividual") > 0)
    //    //    //    {
    //    //    //        ((Label)ContentPlaceHolderBody.FindControl("lblEmpIdHidden")).Text = lblEmpIdHidden.Text;
    //    //    //    }
    //    //    //    else
    //    //    //    {
    //    //    //        ((Label)ContentPlaceHolderBody.FindControl("lblEmpIdHidden")).Text = "0";
    //    //    //    }
    //    //    //}
    //    //    //catch { }
    //    //    //try
    //    //    //{
    //    //    //    if (GetPermission("Permission_ShowAll") > 0)
    //    //    //    {
    //    //    //        ((Label)ContentPlaceHolderBody.FindControl("lblEmpIdHidden")).Text = "0";
    //    //    //    }
    //    //    //}
    //    //    //catch { }
    //    //}
    //}

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        try
        {
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "mailattach/" + lblEmpIdHidden.Text + ""))
            { Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "mailattach/" + lblEmpIdHidden.Text + "", true); }
        }
        catch
        { }
        Yantra.Authentication.ClearSession(this);

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (HiddenField1.Value == "true")
        {
            HiddenField1.Value = "false";
            LinkButton1.Text = "Full Access";
        }
        else if (HiddenField1.Value == "false")
        {
            HiddenField1.Value = "true";
            LinkButton1.Text = "No Access";
        }
    }

    private int GetPermission(string MenuName)
    {
        return Yantra.Authentication.UserPrivilegesFill(MenuName);
    }
    protected void lbtnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Home/Default.aspx");
    }
}
