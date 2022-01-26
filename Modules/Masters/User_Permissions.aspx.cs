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
using Yantra;
using vllib;

public partial class Modules_Masters_User_Permissions : basePage
{
    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.QueryString["UserId"] != null)
            {
                lblUserId.Text = Request.QueryString["UserId"];
                UserPrivilegesFill();
            }
        }
    }
    protected bool getPermission(string Permission_Id, string Privilege_Id)
    {
        try
        {
            string userid = usre.getUserID(lblUserId.Text);

            return uPriv.getPermission(Permission_Id, Privilege_Id, userid);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void UserPrivilegesFill()
    {
        Panel1.Visible = true;
        gvMastersPriv1.DataBind();
        
    }
    protected void savePermissions1(string userid)
    {
        if(i ==1)
        {
            setPermissions(gvMastersPriv1, userid);
            MessageBox.Show(this, "Updated Master Privileges");
        }
        if (i == 2)
        {
            setPermissions(gvSMPriv1, userid);
            MessageBox.Show(this, "Updated SM Privileges");

        }
        if (i == 3)
        {
            setPermissions(gvSCMPriv1, userid);
            MessageBox.Show(this, "Updated SCM Privileges");

        }
        if (i == 4)
        {
            setPermissions(gvInventoryPriv1, userid);
            MessageBox.Show(this, "Updated Inventory Privileges");

        }
        if (i == 5)
        {
            setPermissions(gvServicesPriv1, userid);
            MessageBox.Show(this, "Updated Services Privileges");

        }
        if (i == 6)
        {
            setPermissions(gvFinancePriv1, userid);
            MessageBox.Show(this, "Updated Finance Privileges");

        }
        if (i == 7)
        {
            setPermissions(gvHRPriv1, userid);
            MessageBox.Show(this, "Updated HR Privileges");

        }
        if (i == 8)
        {
            setPermissions(gvReportsPriv1, userid);
            MessageBox.Show(this, "Updated Reports Privileges");

        }
        if (i == 9)
        {
            setPermissions(gvWarehousePriv1, userid);
            MessageBox.Show(this, "Updated Warehouse Privileges");

        }
        i = 0;
    }
    protected void setPermissions(GridView gv, string userid)
    {
        string id = gv.ID;
        //string userid = usre.getUserID(gvAddUserDetails.SelectedRow.Cells[1].Text);

        foreach (GridViewRow gvr in gv.Rows)
        {
            HiddenField hfpv = (HiddenField)gvr.FindControl("hfPRIVILEGE_ID1");

            CheckBox cAdd = (CheckBox)gvr.FindControl("cbxAdd1");
            CheckBox cUpdate = (CheckBox)gvr.FindControl("cbxUpdate1");
            CheckBox cDelete = (CheckBox)gvr.FindControl("cbxDelete1");
            CheckBox cApprove = (CheckBox)gvr.FindControl("cbxApprove1");
            CheckBox cPrint = (CheckBox)gvr.FindControl("cbxPrint1");
            CheckBox cEmail = (CheckBox)gvr.FindControl("cbxEmail1");
            CheckBox cFull = (CheckBox)gvr.FindControl("cbxFull1");

            uPriv.SavePermission("1", hfpv.Value, userid, cAdd.Checked);
            uPriv.SavePermission("2", hfpv.Value, userid, cUpdate.Checked);
            uPriv.SavePermission("3", hfpv.Value, userid, cDelete.Checked);
            uPriv.SavePermission("4", hfpv.Value, userid, cApprove.Checked);
            uPriv.SavePermission("5", hfpv.Value, userid, cPrint.Checked);
            uPriv.SavePermission("6", hfpv.Value, userid, cEmail.Checked);
            uPriv.SavePermission("7", hfpv.Value, userid, cFull.Checked);
        }
    }
    protected void btnSaveMasters_Click(object sender, EventArgs e)
    {
        i = 1;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvMastersPriv1.DataBind();
    }
    protected void btnSaveSM_Click(object sender, EventArgs e)
    {
        i = 2;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvSMPriv1.DataBind();
    }
    protected void btnSaveSCM_Click(object sender, EventArgs e)
    {
        i = 3;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvSCMPriv1.DataBind();

    }
    protected void btnSaveInv_Click(object sender, EventArgs e)
    {
        i = 4;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvInventoryPriv1.DataBind();

    }
    protected void btnSaveServ_Click(object sender, EventArgs e)
    {
        i = 5;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvServicesPriv1.DataBind();

    }
    protected void btnFinance_Click(object sender, EventArgs e)
    {
        i = 6;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvFinancePriv1.DataBind();

    }
    protected void btnSaveHR_Click(object sender, EventArgs e)
    {
        i = 7;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvHRPriv1.DataBind();

    }
    protected void btnReports_Click(object sender, EventArgs e)
    {
        i = 8;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvReportsPriv1.DataBind();

    }
    protected void btnWarehouse_Click(object sender, EventArgs e)
    {
        i = 9;
        string userid = usre.getUserID(lblUserId.Text);
        savePermissions1(userid);
        gvWarehousePriv1.DataBind();

    }
        
    protected void lnk1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
    gvMastersPriv1.DataBind();

        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
        gvSMPriv1.DataBind();

        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = true;
        gvSCMPriv1.DataBind();
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk4_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = true;
        gvInventoryPriv1.DataBind();
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk5_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = true;
        gvServicesPriv1.DataBind();
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk6_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = true;
        gvFinancePriv1.DataBind();
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk7_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = true;
        gvHRPriv1.DataBind();
        Panel8.Visible = false;
        Panel9.Visible = false;
    }
    protected void lnk8_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = true;
        gvReportsPriv1.DataBind(); 
        Panel9.Visible = false;
    }
    protected void lnk9_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        Panel4.Visible = false;
        Panel5.Visible = false;
        Panel6.Visible = false;
        Panel7.Visible = false;
        Panel8.Visible = false;
        Panel9.Visible = true;
        
        gvWarehousePriv1.DataBind();

    }
}
 
