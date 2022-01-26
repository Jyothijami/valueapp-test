using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using vllib; 

public partial class Modules_Warehouse_Manage_Locations_backup : System.Web.UI.Page
{
    TreeNode targetNode = new TreeNode();
    string _path = "VLine";
    string nodeValue;

    Warehouse.Locations whLoc;
    LocationEntity loc;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindTreeViewControl();
            LoadCategory();
        }
        nodeValue = "1";
        if (ddlLocations.Items.Count > 0)
        {
            nodeValue = ddlLocations.SelectedValue;
        }

        LoadBreadCrumb();
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

        }
        catch (Exception Ex) { JavaScriptAlert(Ex.Message); throw Ex; }
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
                GetPath(n); /*ChangeAddButtonText();*/ return;
            }
            else
                SearchChildNodes(n);
        }

        GetPath(targetNode); //ChangeAddButtonText();

    }

    public void GetPath(TreeNode tn)
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
            //Label lbtn = new Label();
            LinkButton lbtn = new LinkButton();
            //lbtn.Text = tn.Text; lbtn.ID = "lastNode"; lbtn.Attributes.Add("ondblclick", "return EditLoc(this);"); lbtn.Style.Add("padding-left", "50px");
            lbtn.Text = tn.Text; lbtn.ID = tn.Value; hdnLocText.Value = tn.Text; //hdnTitle.Value = tn.Value;
            pnlBreadCrumb.Controls.Add(lbtn);
            //Label last = new Label();
            //last.Text = tn.Text;
            //pnlBreadCrumb.Controls.Add(last);
            GetDropLocation(tn.Value);
            //ddlLocations.Items.FindByValue(tn.Value).Selected=true;
        }
        //_path += "->" + tn.Text;
        //return _path;
    }

    private void GetDropLocation(string nodeId)
    {
        ddlLocations.Visible = true;
        DataSet ds = new DataSet();
        int num;
        if (Int32.TryParse(nodeId, out num))
        {
            whLoc = new Warehouse.Locations();
            loc = new LocationEntity();
            loc.ParentId = Convert.ToInt32(nodeId);

            ds = whLoc.GetLocations(loc);
            //ds = GetDataSet("select whLocId,whLocName from WH_Locations where parentId=" + nodeId);
        }

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

    }

    protected void ddlLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
        //nodeValue = ddlLocations.SelectedValue;
        LoadBreadCrumb();
    }
    public void JavaScriptAlert(string msg)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "Popup", "<script>alert('" + msg + "');</script>");

    }

    #region Category
    Warehouse.Category whCat = new Warehouse.Category();
    public void LoadCategory()
    {
        chkCategoryList.DataSource = whCat.GetCategories(0);
        chkCategoryList.DataTextField = "CategoryName";
        chkCategoryList.DataValueField = "CategoryID";
        chkCategoryList.DataBind();

    }
    public enum OperationType
    {
        Insert, Select, Update, Delete
    }
    public void CategoryCrud(OperationType OperationType)
    {
        string msg = "";
        if (txtCategory.Text.Trim() != "")
        {
            switch (OperationType)
            {
                case OperationType.Insert: msg = whCat.AddCategory(txtCategory.Text) > 0 ? "Category Added Successfully" : "Category not added"; break;
                case OperationType.Update: msg = whCat.EditCategory(Convert.ToInt32(chkCategoryList.SelectedValue), txtCategory.Text) > 0 ? "Category Edit Successfully" : "Category not edited"; break;
            }

            JavaScriptAlert(msg);
        }
    }
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        string msg = "";
        if (txtCategory.Text.Trim() != "")
        {
            switch (btnAddCategory.Text)
            {
                case "Add": msg = whCat.AddCategory(txtCategory.Text) > 0 ? "Category Added Successfully" : "Category not added"; break;
                case "Update": msg = whCat.EditCategory(Convert.ToInt32(chkCategoryList.SelectedValue), txtCategory.Text) > 0 ? "Category Edit Successfully" : "Category not edited"; break;
            }

            JavaScriptAlert(msg);
            ClearCategoryFields();
            LoadCategory();
        }
    }
    protected void btnEditCategory_Click(object sender, EventArgs e)
    {
        if (chkCategoryList.SelectedIndex != -1)
        {
            lblCatTitle.Text = "Edit Category";
            txtCategory.Text = chkCategoryList.SelectedItem.Text;
            btnAddCategory.Text = "Update";
        }
    }
    public void ClearCategoryFields()
    {
        btnAddCategory.Text = "Add";
        lblCatTitle.Text = "Add New Category";
        txtCategory.Text = string.Empty;
        chkCategoryList.ClearSelection();
    }
    protected void btnDeleteCategory_Click(object sender, EventArgs e)
    {
        if (chkCategoryList.SelectedIndex != -1)
        {
            string msg = whCat.DeleteCategory(Convert.ToInt32(chkCategoryList.SelectedValue)) > 0 ? "Category deleted Successfully" : "Category not deleted";
            JavaScriptAlert(msg);
            ClearCategoryFields();
            LoadCategory();
        }
    }
    #endregion

    #region "Location"
    protected void btnAddLocation_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim() != "")
        {

            loc = new LocationEntity();
            whLoc = new Warehouse.Locations();
            //JavaScriptAlert(lblDialogTitle.InnerText);
            loc.LocationName = txtName.Text;
            string msg = "";
            if (hdnTitle.Value.Contains("Add"))
            {
                loc.ParentId = Convert.ToInt32(nodeValue);
                msg = whLoc.AddLocation(loc) > 0 ? hdnTitle.Value.Substring(4) + " Added Successfully" : hdnTitle.Value.Substring(4) + " not Added";
            }
            else if (hdnTitle.Value.Contains("Edit"))
            {
                loc.LocId = Convert.ToInt32(nodeValue);
                msg = whLoc.EditLocation(loc) > 0 ? hdnTitle.Value.Substring(4) + " Edited Successfully" : hdnTitle.Value.Substring(4) + " not Edited";
                targetNode.Text = txtName.Text;
            }
            BindTreeViewControl(); JavaScriptAlert(msg); LoadBreadCrumb();
            txtName.Text = "";

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        whLoc = new Warehouse.Locations();
        loc = new LocationEntity();
        loc.ParentId = Convert.ToInt32(hdnParentID.Value);
        loc.LocationName = txtLocationName.Text;
        int n = whLoc.AddLocation(loc);
        txtLocationName.Text = hdnParentID.Value = string.Empty;

        BindTreeViewControl(); LoadBreadCrumb();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //SqlCommand cmd = new SqlCommand();
        //cmd.CommandText = "delete WH_Locations where whLocId=@ID";
        //cmd.Connection = con;
        //cmd.Parameters.AddWithValue("@ID", hdnParentID.Value);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();

        ChildNodes(targetNode);
        if (targetNode.Parent == null)
        {
            targetNode = tvLocations.FindNode("1");
        }
        else
        {
            targetNode = targetNode.Parent;
        }
        whLoc = new Warehouse.Locations();
        loc = new LocationEntity();
        loc.LocId = Convert.ToInt32(nodeValue);
        string stockLocations = string.Join(",", childs);
        int n = whLoc.DeleteLocation(loc, stockLocations);
        lblLoc.Text = hdnParentID.Value = string.Empty;

        BindTreeViewControl(); LoadBreadCrumb();
    }
    List<string> childs = new List<string>();
    private void ChildNodes(TreeNode treeNode)
    {
        // Print the node.
        if (treeNode.ChildNodes.Count == 0)
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
    #endregion
}
 
