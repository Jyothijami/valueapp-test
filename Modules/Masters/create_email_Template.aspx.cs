using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Masters_create_email_Template : System.Web.UI.Page
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        tbxemt1.CssClass = "yui_editor";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void addtemplatebt1_Click(object sender, EventArgs e)
    {
        //if (emtemplates.add_email_templates(shortcodetb1.Text, emailtptb1.Text, subjtb1.Text, tbxemt1.Text))
        //{
        //    //noty.Success_Display("Template Added Successfully", Page);
        //    sticky.Success_Display("Template Added Successfully", Page);
        //}
        //else
        //{
        //    noty.Error_Display("Template Insertion Failed", Page);
        //}
    }
}
 
