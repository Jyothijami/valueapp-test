using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Warehouse_Manage_Locations3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setLocations();
    }

    protected void setLocations()
    {
        TreeView1.Nodes.Clear();

        DataTable dt = WH.getWarehouseLocations();

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = "L_" + dr["locid"].ToString();
            tn.Text = dr["locname"].ToString();
            //tn.SelectAction = TreeNodeSelectAction.Expand;

            setWarehouses(dr["locid"].ToString(), tn);

            TreeView1.Nodes.Add(tn);
        }

        TreeView1.ExpandAll();
    }

    protected void setWarehouses(string locid, TreeNode tnode)
    {
        DataTable dt = WH.getWarehouses(locid);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = "W_" + dr["wh_id"].ToString();
            tn.Text = dr["whname"].ToString();
            //tn.SelectAction = TreeNodeSelectAction.Expand;

            SetWarehouseLocations(dr["wh_id"].ToString(), tn);

            tnode.ChildNodes.Add(tn);
        }
    }

    protected void SetWarehouseLocations(string wh_id, TreeNode tnode, string parentId = "0")
    {
        

        DataTable dt = WH_Locations.getLocations(wh_id, parentId);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = "S_" + dr["whLocId"].ToString();
            tn.Text = dr["whLocName"].ToString();
            //tn.SelectAction = TreeNodeSelectAction.Expand;

            SetWarehouseLocations_child(wh_id, tn, dr["whLocId"].ToString());

            tnode.ChildNodes.Add(tn);
        }
    }

    protected void SetWarehouseLocations_child(string wh_id, TreeNode tn1, string parentId = "0")
    {
        DataTable dt = WH_Locations.getLocations(wh_id, parentId);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = "S_" + dr["whLocId"].ToString();
            tn.Text = dr["whLocName"].ToString();
            //tn.SelectAction = TreeNodeSelectAction.Expand;

            SetWarehouseLocations_child(wh_id, tn, dr["whLocId"].ToString());

            tn1.ChildNodes.Add(tn);
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }

    protected void btnAddSubSection1_Click(object sender, EventArgs e)
    {
        string secid_str = hfsectionid1.Value;
        secid_str = secid_str.Split(new char[] { ',' })[1].Split(new char[] { '\'' })[1];
        secid_str = secid_str.Split(new char[] { '\\' })[secid_str.Split(new char[] { '\\' }).Length - 1];

        secid_str = WH_Locations.getLocationID(secid_str);

        if (secid_str.Equals(""))
        {
            sticky.Error_Display("Cannot Insert At this Location", Page);
        }
        else
        {
            if (WH_Locations.add_WH_Locations(Convert.ToInt32(secid_str), tbxSubSection1.Text, tbxSubSection1.Text))
            {
                sticky.Success_Display("Inserted Successfully", Page);
            }
            else
            {
                sticky.Error_Display("Insertion Failed", Page);
            }
        }
        setLocations();
    }
    protected void btnEditSection1_Click(object sender, EventArgs e)
    {
        string secid_str = hfsectionid1.Value;
        secid_str = secid_str.Split(new char[] { ',' })[1].Split(new char[] { '\'' })[1];
        secid_str = secid_str.Split(new char[] { '\\' })[secid_str.Split(new char[] { '\\' }).Length - 1];

        if (secid_str.Split(new char[] { '_' })[0] == "S")
        {
            string whlocid = secid_str.Split(new char[] { '_' })[1];

            if (WH_Locations.add_WH_Locations_Rename(whlocid, tbxNewSectionName1.Text, tbxNewSectionName1.Text))
            {
                sticky.Success_Display("Updated Successfully", Page);
            }
            else
            {
                sticky.Error_Display("Updation Failed", Page);
            }
        }
        else
        {
            sticky.Error_Display("Please Select Correct Location", Page);
        }

        setLocations();
    }
}
 
