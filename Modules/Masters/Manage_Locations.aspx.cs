using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using YantraBLL.Modules;
using Yantra.MessageBox;

public partial class Modules_Masters_Manage_Locations : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();
    }

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnAddLocation1.Enabled = up.add;
        

    }
    protected string RandomGenerator()
    {
        Random RandomGenerator = null;
        int intRandomNumber = 0;
        RandomGenerator = new Random();
        intRandomNumber = RandomGenerator.Next(0001, 99999999);
        return intRandomNumber.ToString();

    }
    protected void btnAddLocation1_Click(object sender, EventArgs e)
    {
        Masters.CompanyLocations obj = new Masters.CompanyLocations();
        obj.locName = tbxLocationName1.Text;
        obj.locDesc = txtDesc.Text;
        obj.locId = RandomGenerator();

        if(obj.Location_Save() == "Data Saved Successfully")
        {
            MessageBox.Show(this, "Data Saved Successfully");

        }
        else
        {
            MessageBox.Show(this, "Unable to save, please try again");

        }
        GridView1.DataBind();
        //if (locations.add_location_tbl(tbxLocationName1.Text))
        //{
        //    tbxLocationName1.Text = "";
        //    sticky.Success_Display("Location Inserted Successfully", Page);

        //    GridView1.DataBind();
        //}
        //else
        //{
        //    sticky.Error_Display("Insertion Failed, Please Try Again", Page);
        //}
    }
    
}
 
