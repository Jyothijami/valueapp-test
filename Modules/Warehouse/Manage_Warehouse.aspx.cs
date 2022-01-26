using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Warehouse_Manage_Warehouse : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnAddWarehouse1.Enabled = up.add;


    }


    protected void btnAddWarehouse1_Click(object sender, EventArgs e)
    {
        if (WH.add_warehouse_tbl(tbxWarehousename1.Text, tbxWarehouseAddr1.Text, tbxWarehouseDesc1.Text, ddlLocations1.SelectedValue))
        {
            GridView1.DataBind();

            tbxWarehousename1.Text = "";
            tbxWarehouseAddr1.Text = "";
            tbxWarehouseDesc1.Text = "";

            sticky.Success_Display("Insertion Successfully", Page);
        }
        else
        {
            sticky.Error_Display("Insertion Failed", Page);
        }
    }
}
 
