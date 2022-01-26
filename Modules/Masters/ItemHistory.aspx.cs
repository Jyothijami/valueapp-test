using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Yantra.MessageBox;
using YantraBLL.Modules;
using vllib;
using System.Data;

public partial class Modules_Masters_ItemHistory : basePage
{
    string Qid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string Qid = Request.QueryString["Cid"].ToString();
             Qid = "17";
            Itemfill();
        }
    }

    private void Itemfill()
    {
        Masters.ItemMaster objmaster = new Masters.ItemMaster();
        if (objmaster.ItemMaster_Select(Qid) > 0)
        {
            lblItemName.Text = objmaster.ItemName;
            lblUOM.Text = objmaster.Uomid;
            lblItemSpecification.Text = objmaster.ItemSpec;
            lblMaterialType.Text = objmaster.Materialtype;
            lblPrincipalName.Text = objmaster.Principalname;
            lblItemSeries.Text = objmaster.Itemseries;
            lblPurchaseSpec.Text = objmaster.Purchasespec;
            lblItemModelNo.Text = objmaster.ModelNo;

            lblItemCategory.Text = objmaster.ItemCategoryName;
            
            lblBrand.Text = objmaster.Brandid;


            lblSubCategory.Text = objmaster.ItemType;


            DataTable dt = objmaster.HistItemColor_Select(int.Parse(Qid));
            lblColor.Text = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string color = dt.Rows[i][0].ToString();
                lblColor.Text = lblColor.Text + color + " " ;
                
            }
        }

   }

}
 
