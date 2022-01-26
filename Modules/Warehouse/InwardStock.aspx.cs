using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;
using System.Text;
using vllib;
public partial class Modules_Warehouse_InwardStock : basePage
{
    SqlConnection con = new SqlConnection("data source=.;database=vlt2;uid=sa;pwd=Datumsql");
    DataTable dt=new DataTable();
    TreeNode targetNode = new TreeNode();
    List<string> childs = new List<string>();

    Warehouse.Category whCategory;
    Warehouse.Locations whLoc;
    LocationEntity loc;
    Warehouse.Items whItem;
    ItemEntity item;

    string _path = "VLine";
    string nodeValue; string category = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTreeViewControl();
            BindCategory();
        }
        nodeValue = "1";
        if (ddlLocations.Items.Count > 0)
        {
            nodeValue = ddlLocations.SelectedValue;
        }
       
        LoadBreadCrumb();
    }
    public void LoadMRNItems()
    {
        dt = new DataTable();
        item = new ItemEntity(); whItem = new Warehouse.Items();
        if (txtMRN.Text!="")
        {
            item.MRN = txtMRN.Text;
            //item.Category = "MRN";
        }
        //if (ddlCategory.SelectedIndex!=0)
        //{
        //    item.Category = ddlCategory.SelectedItem.Text;
        //}
        if (txtProductCode.Text!="")
        {
            item.ProductCode = txtProductCode.Text;
        }
        if (txtItemName.Text != "")
        {
            item.ItemName = txtItemName.Text;
        }
        dt=whItem.GetItems(item);
        gvMrnItems.DataSource = dt;
        gvMrnItems.DataBind();
    }
    private void BindCategory()
    {
       // whCategory = new Warehouse.Category();
       // dt = new DataTable();
       //dt= whCategory.GetCategories(0);
       //ddlCategory.DataSource = dt;
       //ddlCategory.DataValueField = "CategoryID";
       //ddlCategory.DataTextField = "CategoryName";
       //ddlCategory.DataBind();
       //ddlCategory.Items.Insert(0, new ListItem("--Select--", "0"));

    }
    protected void ddlLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBreadCrumb();

    }
    protected void BreadCrumb(object sender, EventArgs e)
    {
        pnlBreadCrumb.Controls.Clear();
        nodeValue = ((LinkButton)sender).ID;
        LoadBreadCrumb();

    }
    public void JavaScriptAlert(string val)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "Popup", "<script>alert('" + val + "');</script>");
    }

    #region "Location Tree"
    private void SearchChildNodes(TreeNode treeNode)
    {
        // Print the node.
        if (treeNode.Value == nodeValue)
        { //_path = treeNode.FullPath;
            targetNode = treeNode; return;
        }
        else
        {   // Print each node recursively.
            foreach (TreeNode tn in treeNode.ChildNodes)
            {
                SearchChildNodes(tn);
                if (targetNode.Value == nodeValue)
                {
                    return;
                }
            }
        }
    }

    public void LoadBreadCrumb()
    {
        pnlBreadCrumb.Controls.Clear();
        //Label lt = new Label();
        //lt.Text = "VLine";
        //pnlBreadCrumb.Controls.Add(lt);
        foreach (TreeNode n in tvLocations.Nodes)
        {
            if (n.Value == nodeValue)
            {
                targetNode = n;
                GetPath(n); return;
            }
            else
                SearchChildNodes(n);
        }

        string p = GetPath(targetNode);
        //ClientScript.RegisterStartupScript(Page.GetType(), "Popup", "<script>alert('" + p + "');</script>");
        //LoadWarehouse();

    }

    public string GetPath(TreeNode tn)
    {
        if (tn.Parent != null)
        {
            GetPath(tn.Parent);
        }
        //Label l = new Label();
        //l.Text = "=>";
        //pnlBreadCrumb.Controls.Add(l);
        if (tn.Value != targetNode.Value)
        {
            LinkButton lbtn = new LinkButton();
            lbtn.Text = tn.Text; lbtn.ID = tn.Value; lbtn.Click += new System.EventHandler(this.BreadCrumb);// lbtn.PostBackUrl = "#";

            pnlBreadCrumb.Controls.Add(lbtn);
        }
        else
        {
            LinkButton lbtn = new LinkButton();
            lbtn.Text = tn.Text; lbtn.ID = tn.Value;
            pnlBreadCrumb.Controls.Add(lbtn);
            //Label last = new Label();
            //last.Text = tn.Text;
            //pnlBreadCrumb.Controls.Add(last);
            GetDropLocation(tn.Value);
            //ddlLocations.Items.FindByValue(tn.Value).Selected=true;
        }
        _path += "->" + tn.Text;
        return _path;
    }

    private void GetDropLocation(string nodeId)
    {
        ddlLocations.Visible = true;
        DataSet ds = new DataSet();

        whLoc = new Warehouse.Locations();
        loc = new LocationEntity();
        loc.ParentId = Convert.ToInt32(nodeId);

        ds = whLoc.GetLocations(loc);
        //ds = GetDataSet("select whLocId,whLocName from WH_Locations where parentId=" + nodeId);

        ddlLocations.Items.Clear();
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlLocations.DataSource = ds.Tables[0];
            ddlLocations.DataTextField = "whLocName";
            ddlLocations.DataValueField = "whLocId";
            ddlLocations.DataBind();
            ddlLocations.Items.Insert(0, new ListItem("--Select--", nodeId));
        }
        else
        {
            ddlLocations.Items.Insert(0, new ListItem("--No Items--", nodeId));
            ddlLocations.Visible = false;
        }

    }

    private void BindTreeViewControl()
    {
        try
        {
            tvLocations.Nodes.Clear();
            TreeNode Vline = new TreeNode("V-Line", "1");
            whLoc = new Warehouse.Locations();
            loc = new LocationEntity();
            DataSet ds = whLoc.GetLocations(loc);
            //DataSet ds = GetDataSet("Select whLocId,whLocName,parentId from WH_Locations");
            DataRow[] Rows = ds.Tables[0].Select("parentId = 1");

            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode root = new TreeNode(Rows[i]["whLocName"].ToString(), Rows[i]["whLocId"].ToString());
                root.SelectAction = TreeNodeSelectAction.SelectExpand;

                CreateNode(root, ds.Tables[0]);
                Vline.ChildNodes.Add(root);
            }
            tvLocations.Nodes.Add(Vline);

            tvLocations.ExpandAll();
        }
        catch (Exception Ex) { throw Ex; }
    }

    public void CreateNode(TreeNode node, DataTable Dt)
    {
        DataRow[] Rows = Dt.Select("parentId =" + node.Value);
        if (Rows.Length == 0) { return; }
        for (int i = 0; i < Rows.Length; i++)
        {
            TreeNode Childnode = new TreeNode(Rows[i]["whLocName"].ToString(), Rows[i]["whLocId"].ToString());
            Childnode.SelectAction = TreeNodeSelectAction.SelectExpand;
            node.ChildNodes.Add(Childnode);
            CreateNode(Childnode, Dt);
        }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadMRNItems();
    }
    protected void btnMRNGo_Click(object sender, EventArgs e)
    {
        LoadMRNItems();
    }
    protected void btnSaveWH_Click(object sender, EventArgs e)
    {
        whItem = new Warehouse.Items();
        item = new ItemEntity();
        if (ddlLocations.Items.Count == 1)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" ?>");
            sb.AppendLine("     <items>");
            foreach (GridViewRow row in gvMrnItems.Rows)
            {
                CheckBox chk = (CheckBox)row.Cells[0].FindControl("chk");
                if (chk.Checked == true)
                {
                    sb.AppendLine("         <item>");
                    sb.AppendLine("             <ItemID>" + row.Cells[1].Text + "</ItemID>");
                    sb.AppendLine("             <LocationId>" + ddlLocations.SelectedValue + "</LocationId>");

                    sb.AppendLine("         </item>");

                }

            }

            sb.AppendLine("         </items>");

            SqlCommand cmd = new SqlCommand("Usp_SaveInWarehouse", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xml", sb.ToString());
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            int aff = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            JavaScriptAlert("Items are saved in warehouse");
            LoadMRNItems();
        }
        else
        {
            JavaScriptAlert("please select proper location");
        }
    }
}
 
