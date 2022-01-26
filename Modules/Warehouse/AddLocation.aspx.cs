using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Warehouse_AddLocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("data source=.;database=vlt2;uid=sa;pwd=Datumsql");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTreeViewControl();
            //tvLocations.FindNode("102").Selected = true;
        }
    }
    private void BindTreeViewControl()
    {
        try
        {
            tvLocations.Nodes.Clear();
            DataSet ds = GetDataSet("Select whLocId,whLocName,parentId from WH_Locations");
            DataRow[] Rows = ds.Tables[0].Select("parentId = 0");

            for (int i = 0; i < Rows.Length; i++)
            {
                TreeNode root = new TreeNode(Rows[i]["whLocName"].ToString(), Rows[i]["whLocId"].ToString());
                root.SelectAction = TreeNodeSelectAction.SelectExpand;
                
                CreateNode(root, ds.Tables[0]);
                tvLocations.Nodes.Add(root);
            }
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
}
 
