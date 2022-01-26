using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_SM_List_eQuotations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGenerateQuot1_Click(object sender, EventArgs e)
    {
        asposeSlide ase = new asposeSlide();
        ase.generatePPT(TextBox1.Text);

    }
}
 
