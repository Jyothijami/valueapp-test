using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data;

public partial class Modules_Warehouse_Manage_Locations2 : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SetWarehouse();
            setControlsVisibility();
        }
        //LoadLocations();
        
    }

    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "92");
        btnaddLocation1.Enabled = up.add;

    }

    //protected void LoadLocations()
    //{
    //    DataTable dt = locations.getLocations();

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        string locid = dr["locid"].ToString();

    //        TreeNode tn = new TreeNode();
    //        tn.Value = locid;
    //        tn.Text = dr["locname"].ToString();

    //        SetWarehouse(tn, locid);

    //        TreeView1.Nodes.Add(tn);
    //    }
    //}

    //protected void SetWarehouse()
    //{
    //    TreeView1.Nodes.Clear();

    //    string locid = ddlLocations1.SelectedValue;

    //    DataTable dt = WH.getWarehouses(locid);

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        string wh_id = dr["wh_id"].ToString();
    //        TreeNode tn = new TreeNode();
    //        tn.Value = wh_id;
    //        tn.Text = dr["whname"].ToString();

    //        SetWarehouseLocations(tn, wh_id);

    //        TreeView1.Nodes.Add(tn);
    //    }
    //}

    protected void SetWarehouseLocations(string parentId = "0")
    {
        TreeView1.Nodes.Clear();

        string wh_id = ddlBranch1.SelectedValue;

        DataTable dt = WH_Locations.getLocations(wh_id, parentId);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["whLocId"].ToString();
            tn.Text = dr["whLocName"].ToString();

            SetWarehouseLocations_child(tn, dr["whLocId"].ToString());

            TreeView1.Nodes.Add(tn);
        }
    }

    protected void SetWarehouseLocations_child(TreeNode tn1, string parentId = "0")
    {
        //TreeView1.Nodes.Clear();

        string wh_id = ddlBranch1.SelectedValue;

        DataTable dt = WH_Locations.getLocations(wh_id, parentId);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["whLocId"].ToString();
            tn.Text = dr["whLocName"].ToString();

            SetWarehouseLocations_child(tn, dr["whLocId"].ToString());
            
            tn1.ChildNodes.Add(tn);
        }
    }

    protected void ddlLocations1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch1.DataBind();

        //SetWarehouse();
        SetWarehouseLocations();
    }
    protected void btnaddLocation1_Click(object sender, EventArgs e)
    {
        if (WH_Locations.add_WH_Locations(Convert.ToInt32(hfwhlocid1.Value), tbxLocationName1.Text, "", ddlBranch1.SelectedValue))
        {
            SetWarehouseLocations();
            tbxLocationName1.Text = "";
            GridView1.DataBind();
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        PnlAddNewLocation1.Visible = true;

        hfwhlocid1.Value = TreeView1.SelectedValue;
        lblLocationName1.Text = TreeView1.SelectedNode.Text;
    }
    protected void ddlBranch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetWarehouseLocations();
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        WH_Locations.updateTreeJson();

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        WH_Locations.updateTreeJson();
    }
}
 
