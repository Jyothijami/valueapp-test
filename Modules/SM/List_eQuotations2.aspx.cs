using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_SM_List_eQuotations2 : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnGenerateNew1_Click(object sender, EventArgs e)
    {
        string quotid = Request.QueryString["QuoId"];

        string filename = QuotVer.getVersionName(quotid);

        asposeSlide ase = new asposeSlide();
        ase.generatePPT(quotid, filename, 10);

        QuotVer.addQuotVer(filename, "New quot Generation", quotid);

        DataList1.DataBind();
    }

    protected void btnupload1_Click(object sender, EventArgs e)
    {
        string quotid = Request.QueryString["QuoId"];

        string filename = QuotVer.getVersionName(quotid);

        fupload1.SaveAs(Server.MapPath("~/presentations/" + filename));
        QuotVer.addQuotVer(filename, "New Version", quotid);

        DataList1.DataBind();
    }
}
 
