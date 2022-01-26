using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using YantraBLL.Modules;

public partial class Modules_SM_WalkIn : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.InsertParameters["WalkIn_No"].DefaultValue = SM.CustomerMaster.WalkIn_AutoGenCode();
        SqlDataSource1.InsertParameters["CP_ID"].DefaultValue = cp.getPresentCompanySessionValue();

        if (!IsPostBack)
        {
            RadGrid1.DataBind();
        }
    }

   
}