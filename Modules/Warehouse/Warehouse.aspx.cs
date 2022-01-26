using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using vllib;
public partial class Modules_Warehouse_Warehouse : basePage
{
    SqlConnection con = dbc.con;

    TreeNode targetNode = new TreeNode();
    List<string> childs = new List<string>();

    Warehouse.Locations whLoc; Warehouse.Category whCat;
    LocationEntity loc;
    Warehouse.Items whItem;
    ItemEntity item;

    string _path = "VLine";
    string nodeValue; string category=null;
    protected void Page_Load(object sender, EventArgs e)
        {

        if (!IsPostBack)
        {
            BindTreeViewControl();
            LoadWarehouse();
            LoadCategory();
        }
        nodeValue = "1";
        if (ddlLocations.Items.Count > 0)
        {
            nodeValue = ddlLocations.SelectedValue;
        }
        //if (nodeValue == "1")
        //{
        //    GetDropLocation("1");
        //}
        //else
            LoadBreadCrumb();
    }
    private void BindTreeViewControl()
    {
        try
        {
            tvLocations.Nodes.Clear();
            TreeNode Vline = new TreeNode("V-Line", "1");
            whLoc = new Warehouse.Locations();
            loc=new LocationEntity();
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

    public void LoadCategory()
    {
        whCat = new Warehouse.Category();
        chkCategoryList.DataSource = whCat.GetCategories(0);
        chkCategoryList.DataTextField = "CategoryName";
        chkCategoryList.DataValueField = "CategoryID";
        chkCategoryList.DataBind();

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

    private DataSet GetDataSet(string Query)
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            da.Fill(Ds);
        }
        catch (Exception dex) { }
        return Ds;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "insert WH_Locations(whLocName,parentId)values(@lName,@pID) ";
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@lName", txtLocationName.Text);
        cmd.Parameters.AddWithValue("@pID", hdnParentID.Value);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        txtLocationName.Text = hdnParentID.Value = string.Empty;

        BindTreeViewControl();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "delete WH_Locations where whLocId=@ID";
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@ID", hdnParentID.Value);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblLoc.Text = hdnParentID.Value = string.Empty;

        BindTreeViewControl();
    }

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
            lbtn.Text = tn.Text; lbtn.ID = tn.Value; lbtn.Click += new System.EventHandler(this.BreadCrumb_Click);// lbtn.PostBackUrl = "#";

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
    protected void BreadCrumb_Click(object sender, EventArgs e)
    {
        pnlBreadCrumb.Controls.Clear();
        nodeValue = ((LinkButton)sender).ID;
        LoadBreadCrumb();
        LoadWarehouse();

    }

    //protected override void LoadViewState(object savedState)
    //{
    //    base.LoadViewState(savedState);
    //    if (ViewState["controsladded"] == null)
    //    { BindTreeViewControl(); TestTree(); }
    //}

    protected void ddlLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
        //nodeValue = ddlLocations.SelectedValue;
        LoadBreadCrumb();
        LoadWarehouse();
    }
    public void LoadWarehouse()
    {
        //code to load warehouse details. 
        string locID = ddlLocations.SelectedValue;
        ChildNodes(targetNode);

        DataSet ds = new DataSet();
        whItem = new Warehouse.Items();
        item = new ItemEntity();
        item.StockLocation = locID;
        //item.Category = category;

        if (chkCategoryList.SelectedItem != null)
            item.Category = chkCategoryList.SelectedItem.Value;

        if (txtProdCode.Text.Trim() != "")
            item.ProductCode = txtProdCode.Text;
        if (txtItemName.Text.Trim() != "")
            item.ItemName = txtItemName.Text;
        if (txtColor.Text.Trim() != "")
            item.Color = txtColor.Text;
        if (txtBrand.Text.Trim() != "")
            item.Brand = txtBrand.Text;
        if (txtFromDate.Text.Trim() != "")
            item.FromDate = txtFromDate.Text;
        if (txtToDate.Text.Trim() != "")
            item.ToDate = txtToDate.Text;
        if (childs.Count > 0)
            item.StockLocation=string.Join(",", childs);

        //JavaScriptAlert(item.StockLocation);
        DataTable dt = new DataTable();
            dt = whItem.GetItems(item);
        //gvWarehouse.DataSource = dt;
        //gvWarehouse.DataBind();
        //ds = GetDataSet("select * from Temp_Items where StockLocation=" + locID);
        if (dt.Rows.Count > 0)
        {
            gvWarehouse.DataSource = dt;
            gvWarehouse.DataBind();

            int i = 0;
            chkColumnList.Items.Clear();
            foreach (var col in dt.Columns)
            {
                ListItem li = new ListItem(col.ToString(),(i++).ToString()); li.Selected = true; 
                li.Attributes.Add("onchange", "ShowHideColumns(this)");
                chkColumnList.Items.Add(li);
            }
            
        }

    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        chkCategoryList.ClearSelection();
        txtToDate.Text = txtFromDate.Text = txtProdCode.Text = txtItemName.Text = txtBrand.Text = txtColor.Text = "";
        LoadWarehouse();
    }
    private void ChildNodes(TreeNode treeNode)
    {
        // Print the node.
        if (treeNode.ChildNodes.Count==0)
        {
            childs.Add(treeNode.Value);
            return;
        }
        else
        {   // Print each node recursively.
            foreach (TreeNode tn in treeNode.ChildNodes)
            {
                ChildNodes(tn);
                //childs.Add(treeNode.Value);
            }
        }
    }
    public void JavaScriptAlert(string val)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "Popup", "<script>alert('" + val + "');</script>"); 
    }

    protected void chkCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadWarehouse();
    }
}
 
