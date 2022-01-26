using System.Collections.Generic;

using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using Yantra.MessageBox;
//using Subgurim.Controles;
using System.Data.SqlClient;
using vllib;
public partial class Modules_Warehouse_SpareInward : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    TreeNode targetNode = new TreeNode();
    List<string> childs = new List<string>();

    Warehouse.Category whCategory;
    Warehouse.Locations whLoc;
    LocationEntity loc;
    Warehouse.Items whItem;
    ItemEntity item;

    string _path = "VLine";
    string nodeValue; string category = null;
    DataTable dt;
    static DataTable dtSpares = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindTreeViewControl();
            ClearRows();
            PrepareGrid();
            AddNewRow();
            lblCPID.Text = cp.getPresentCompanySessionValue();

            BindSparesInwardGrid();
        }
        nodeValue = "1";
        
        LoadBreadCrumb();
        SaveGridValues();
        setControlsVisibility();
    }

    private void BindSparesInwardGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_SpareInwardSearch", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
        }
        if (txtBrand.Text != "")
        {
            cmd.Parameters.AddWithValue("@Brand", txtBrand.Text);

        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFromDate.Text));

        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));

        }
        if (txtInvNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@Invoice_No", txtInvNo.Text);

        }
        if (txtLocation.Text != "")
        {
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);

        }
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvSpareInward.DataSource = dt;
        gvSpareInward.DataBind();
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "94");
        btnAddNewRow.Enabled = up.add;
        btnDeleteRow.Enabled = up.Delete;
    }
    public void PrepareGrid()
    {
        dtSpares.PrimaryKey = null;
        dtSpares.Columns.Clear();
        DataColumn col = new DataColumn("SNo", typeof(int)); col.AutoIncrement = true; col.AutoIncrementSeed = 1;
        dtSpares.Columns.Add(col);
        dtSpares.PrimaryKey = new DataColumn[] { dtSpares.Columns["SNo"] };
        //dtDamage.PrimaryKey = col;
        dtSpares.Columns.Add("Invoice No", typeof(string));
        dtSpares.Columns.Add("Item Model No", typeof(string));
        dtSpares.Columns.Add("Spare Model No", typeof(string));
        dtSpares.Columns.Add("Sub Category", typeof(string));
        dtSpares.Columns.Add("Brand", typeof(string));
        dtSpares.Columns.Add("Color", typeof(string));
        dtSpares.Columns.Add("Quantity", typeof(string));
        dtSpares.Columns.Add("Remarks", typeof(string));
    }
    private void SaveGridValues()
    {
        
        for (int i = 0; gvSpares.Rows.Count > i; i++)
        {
            for (int j = 1; j < gvSpares.Rows[i].Cells.Count; j++)
            {
                if (dtSpares.Rows.Count-1 >=i)
                {
                    dtSpares.Rows[i][j] = ((TextBox)gvSpares.Rows[i].Cells[j].Controls[1]).Text;
                }
                else
                {
                    DataRow row = dtSpares.NewRow();
                    dtSpares.Rows.Add(row);
                    dtSpares.Rows[i][j] = ((TextBox)gvSpares.Rows[i].Cells[j].Controls[1]).Text;
                }
            }
        }
    }
    public void ClearRows()
    {
        dtSpares.Rows.Clear();
    }
    public void AddNewRow()
    {
        DataRow row = dtSpares.NewRow();
        //if (dtDamage.Rows.Count>0)
        //{
        //    row[0] = Convert.ToInt32(dtDamage.Rows[dtDamage.Rows.Count-1][0]) + 1;            
        //}
        //else
        //    row[0] = dtDamage.Rows.Count + 1;

        dtSpares.Rows.Add(row);
        gvSpares.DataSource = dtSpares; gvSpares.DataBind();
    }
    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        CheckBox chkAll=(CheckBox)gvSpares.HeaderRow.FindControl("chkAll");
        if (chkAll.Checked)
        {
            dtSpares.Rows.Clear();
        }
        else
        {
            for (int i = 0; i < gvSpares.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvSpares.Rows[i].FindControl("chk");
                if (chk.Checked)
                {
                    dtSpares.Rows.Find(chk.Text).Delete();
                }
            }
        }
        if (dtSpares.Rows.Count==0)
        {
            AddNewRow();
        }
        gvSpares.DataSource = dtSpares; gvSpares.DataBind();
    }

    protected void ddlLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBreadCrumb();

    }
    protected void BreadCrumb(object sender, EventArgs e)
    {
        //pnlBreadCrumb.Controls.Clear();
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
        //pnlBreadCrumb.Controls.Clear();
        ////Label lt = new Label();
        ////lt.Text = "VLine";
        ////pnlBreadCrumb.Controls.Add(lt);
        //foreach (TreeNode n in tvLocations.Nodes)
        //{
        //    if (n.Value == nodeValue)
        //    {
        //        targetNode = n;
        //        GetPath(n); return;
        //    }
        //    else
        //        SearchChildNodes(n);
        //}

        //string p = GetPath(targetNode);
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

            //pnlBreadCrumb.Controls.Add(lbtn);
        }
        else
        {
            LinkButton lbtn = new LinkButton();
            lbtn.Text = tn.Text; lbtn.ID = tn.Value;
            //pnlBreadCrumb.Controls.Add(lbtn);
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
        //ddlLocations.Visible = true;
        DataSet ds = new DataSet();

        whLoc = new Warehouse.Locations();
        loc = new LocationEntity();
        loc.ParentId = Convert.ToInt32(nodeId);

        ds = whLoc.GetLocations(loc);
        //ds = GetDataSet("select whLocId,whLocName from WH_Locations where parentId=" + nodeId);

        //ddlLocations.Items.Clear();
        //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlLocations.DataSource = ds.Tables[0];
        //    ddlLocations.DataTextField = "whLocName";
        //    ddlLocations.DataValueField = "whLocId";
        //    ddlLocations.DataBind();
        //    ddlLocations.Items.Insert(0, new ListItem("--Select--", nodeId));
        //}
        //else
        //{
        //    ddlLocations.Items.Insert(0, new ListItem("--No Items--", nodeId));
        //    ddlLocations.Visible = false;
        //}

    }

    private void BindTreeViewControl()
    {
        try
        {
            //tvLocations.Nodes.Clear();
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
            //tvLocations.Nodes.Add(Vline);

            //tvLocations.ExpandAll();
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
    private DataTable GetGridSelectedData()
    {
        DataTable dtSelectedSpares = dtSpares.Clone();
        for (int i = 0; gvSpares.Rows.Count > i; i++)
        {
            CheckBox chk = (CheckBox)gvSpares.Rows[i].FindControl("chk");
            if (chk.Checked)
            {
                DataRow row=dtSelectedSpares.NewRow();
                for (int j = 1; j < gvSpares.Rows[i].Cells.Count; j++)
                {
                    row[j] = ((TextBox)gvSpares.Rows[i].Cells[j].Controls[1]).Text;
                }
                dtSpares.Rows.Find(chk.Text).Delete();
                dtSelectedSpares.Rows.Add(row);
            }
        }
        if (dtSpares.Rows.Count == 0)
        {
            PrepareGrid();
            AddNewRow();
        }
        gvSpares.DataSource = dtSpares; gvSpares.DataBind();
        return dtSelectedSpares;
    }
    protected void btnSaveWH_Click(object sender, EventArgs e)
    {
        if (ddlMainLocation.SelectedIndex == 0)
        {
            MessageBox.Show(this, "Please Select a Location To Save Spares");
        }
        else
        {

        foreach(GridViewRow gvrow in gvSpares.Rows)
        {
            CheckBox ch = (CheckBox)gvrow.FindControl("chk");
            if(ch.Checked == true)
            {
                TextBox InvoiceNo = (TextBox)gvrow.FindControl("txtInvoiceNo");
                TextBox ModelNo = (TextBox)gvrow.FindControl("txtItemModelNo");
                TextBox SpareNo = (TextBox)gvrow.FindControl("txtSpareModelNo");
                TextBox SubCat = (TextBox)gvrow.FindControl("txtSubCategory");
                TextBox Brand = (TextBox)gvrow.FindControl("txtBrand");
                TextBox Color = (TextBox)gvrow.FindControl("txtColor");
                TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                TextBox Remarks = (TextBox)gvrow.FindControl("txtRemarks");

                Masters.ItemPurchase obj = new Masters.ItemPurchase();
                int quantity = Convert.ToInt32(qty.Text);
                //for(int i=0;  i<quantity; i++)
                //{

                    obj.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
                    
                    obj.InvoiceNo = InvoiceNo.Text;
                    obj.Barcode = ModelNo.Text;
                    obj.ModelNo = ModelNo.Text;
                    obj.SpareModelNo = SpareNo.Text;
                    obj.subcatid = SubCat.Text;
                    obj.brandid = Brand.Text;
                    obj.color = Color.Text;
                    obj.Quantity = quantity.ToString();
                    obj.Remarks = Remarks.Text;
                    obj.whLocId = ddlMainLocation.SelectedItem.Value;
                    obj.MRN_No = ModelNo.Text;
                    obj.Spare_Inward_Save();
                

                //}
                
            }
        }
    }

        ClearRows();
        PrepareGrid();
        AddNewRow();
        BindSparesInwardGrid();
        //dt = new DataTable();
        //dt = GetGridSelectedData();
        //whItem = new Warehouse.Items();
        ////if (ddlLocations.Items.Count == 1)
        ////{
        ////    //Usp_Spares_CRUD
        ////    if (dt.Rows.Count > 0)
        ////    {
        ////        dt.PrimaryKey = null;
        ////        dt.Columns.RemoveAt(0);
        ////        whItem.AddSpares(dt,Convert.ToInt32(ddlLocations.SelectedValue));
        ////        JavaScriptAlert("Items are saved in warehouse");
        ////    }
        ////    else
        ////        JavaScriptAlert("Select items in grid to save in warehouse");
        ////    //LoadMRNItems();
        ////}
        ////else
        ////{
        ////    JavaScriptAlert("please select proper location");
        ////}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        BindSparesInwardGrid();
    }
}
 
