using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_SCM_Manage_SupplierPO_Templates : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnAddTemplate1.Enabled = up.add;

    }


    protected void btnAddTemplate1_Click(object sender, EventArgs e)
    {
        string sptid = "";

        sptid = supplier_po_template.get_sptid(tbxTname1.Text);

        if (sptid.Equals(""))
        {
            sptid = dbc.get_rnum("sptid", "sup_po_templates_tbl");
        }

        string fname = "";

        if (FileUp1.HasFile)
        {
            string ext = System.IO.Path.GetExtension(FileUp1.FileName).TrimStart(".".ToCharArray()).ToLower();

            fname = sptid + "." + ext;

            FileUp1.SaveAs(Server.MapPath("~/Content/SupplierPOs/") + fname);
        }

        if (supplier_po_template.add_sup_po_templates_tbl(tbxTname1.Text, fname, tbxTMcell1.Text, tbxTDcell1.Text, tbxTCcell1.Text, tbxTQcell1.Text))
        {
            sticky.Success_Display("Successfully Inserted", Page);

            GridView1.DataBind();
        }
        else
        {
            sticky.Error_Display("Insertion Failed", Page);
        }
    }
    protected void btnReset1_Click(object sender, EventArgs e)
    {
        cbxIsGeneral1.Checked = false;

        tbxTname1.Enabled = true;
        
        tbxTname1.Text = "";
        tbxTCcell1.Text = "";
        tbxTDcell1.Text = "";
        tbxTMcell1.Text = "";
        tbxTQcell1.Text = "";

    }
    protected void cbxIsGeneral1_CheckedChanged(object sender, EventArgs e)
    {
        if (cbxIsGeneral1.Checked)
        {
            tbxTname1.Text = "General";
            tbxTname1.Enabled = false;
        }
        else
        {
            tbxTname1.Text = "";
            tbxTname1.Enabled = true;
        }
    }
}
 
