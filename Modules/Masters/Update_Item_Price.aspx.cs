using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;

public partial class Modules_Masters_Update_Item_Price : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            try
            {
                Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally 
            {
                Masters.Dispose();
            }
        }
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }


    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        { 
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCategory, ddlCategory.SelectedValue); 
        }
        catch (Exception ex) 
        { 
            MessageBox.Show(this, ex.Message.ToString()); 
        }
        finally 
        {
            Masters.Dispose();
        }
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemMaster.ItemMaster5_Select(ddlModel, ddlBrand.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
}
 
