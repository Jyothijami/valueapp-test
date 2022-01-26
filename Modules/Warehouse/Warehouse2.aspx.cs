using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Warehouse_Warehouse2 : basePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadTreeView();
        loadBC();
    }

    protected void loadBC()
    {
        string whlocid = "";

        whlocid = (Request.QueryString["whlocid"] == null) ? "" : Request.QueryString["whlocid"];

        string s = "";
        //if (whlocid == "")
        //{
            s = "<ul id='breadcrumbs'>";
            s += "<li><a href='#'>Warehouse</a></li>";
            s += "<li>" + getLocations() + "</li>";
            s += "<li>" + getWarehouses() + "</li>";
            //s += "<li>" + getSubSections() + "</li>";

            if (!whlocid.Equals(""))
            {
                string s2 = "<li>" + getSubSections_sub(whlocid) + "</li>";

                string parentid = whlocid;
                string s1 = "";

                do
                {
                    whlocid = parentid;

                    s1 = "<li>" + getSubSections(whlocid) + "</li>" + s1;

                    parentid = WH_Locations.getParentId(whlocid);

                } while (!(parentid.Equals("0") || parentid.Equals("-1")));

                s += s1;
                s += s2;
            }
            else
            {
                s += "<li>" + getSubSections("0") + "</li>";
            }

            s += "</ul>";
        //}

        litbc1.Text = s;
    }

    protected string getLocations()
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        if (lid.Equals(""))
        {
            m = "<a href='locid='>All</a>";
        }
        else
        {
            m = "<a href='?locid=" + lid + "'>" + locations.getLocationsName(lid) + "</a>";
        }

        DataTable dt = WH.getWarehouseLocations();

        string s = "";

        foreach (DataRow dr in dt.Rows)
        {
            s += "<li><a href='?locid=" + dr["locid"].ToString() + "'>" + dr["locname"].ToString() +"</a></li>";
        }

        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }

    protected string getWarehouses()
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        string s = "";

        if (lid.Equals(""))
        {
            m = "";
        }
        else
        {
            string whid = (Request.QueryString["wh_id"] == null) ? "" : Request.QueryString["wh_id"];

            if (whid.Equals(""))
            {
                m = "<a href='?locid=" + lid + "'>ALL</a>";
            }
            else
            {
                m = "<a href='?locid=" + lid + "&wh_id=" + whid + "'>" + WH.getWarehouseName(whid)  + "</a>";
            }

            

            DataTable dt = WH.getWarehouses(lid);

            s = "";

            foreach (DataRow dr in dt.Rows)
            {
                s += "<li><a href='?locid=" + lid + "&wh_id=" + dr["wh_id"].ToString() + "'>" + dr["whname"].ToString() + "</a></li>";
            }
        }


        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }

    protected string getSubSections(string whLocId)
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        string s = "";

        if (lid.Equals(""))
        {
            m = "";
        }
        else
        {
            string whid = (Request.QueryString["wh_id"] == null) ? "" : Request.QueryString["wh_id"];
            

            if (whid.Equals(""))
            {
                m = "";
            }
            else
            {
                DataTable dt = new DataTable();

                switch (whLocId)
                {
                    case "":
                    case "0":
                        m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";
                        
                        dt = WH_Locations.getLocations2(whid, whLocId);

                        s = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                        }

                        break;

                    default:
                        m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + whLocId + "'>" + WH_Locations.getLocationName(whLocId) + "</a>";

                        string parentid = WH_Locations.getParentId(whLocId);

                        dt = WH_Locations.getLocations2(whid, parentid);

                        s = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                        }
                        break;
                }

                //string whLocId = "";

                //whLocId = (Request.QueryString["whLocId"] == null) ? "" : Request.QueryString["whLocId"];

                //if (whLocId.Equals(""))
                //{
                //    m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";
                //}
                //else if (whLocId.Equals("0"))
                //{
                //    m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";


                //}
                //else
                //{
                //    //m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId='>ALL</a>";
                //    m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + whLocId + "'>" + WH_Locations.getLocationName(whLocId) + "</a>";

                //    string parentid = WH_Locations.getParentId(whLocId);

                //    DataTable dt = WH_Locations.getLocations2(whid, parentid);

                //    s = "";

                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                //    }
                //}

            }
        }


        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }

    protected string getSubSections_sub(string whLocId)
    {
        string m = "";

        string lid = (Request.QueryString["locid"] == null) ? "" : Request.QueryString["locid"];

        string s = "";

        if (lid.Equals(""))
        {
            m = "";
        }
        else
        {
            string whid = (Request.QueryString["wh_id"] == null) ? "" : Request.QueryString["wh_id"];


            if (whid.Equals(""))
            {
                m = "";
            }
            else
            {
                DataTable dt = new DataTable();

                m = "<a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + whLocId + "'>All</a>";

                dt = WH_Locations.getLocations2(whid, whLocId);

                s = "";

                foreach (DataRow dr in dt.Rows)
                {
                    s += "<li><a href='?locid=" + lid + "&wh_id=" + whid + "&whLocId=" + dr["whLocId"].ToString() + "'>" + dr["whLocName"].ToString() + "</a></li>";
                }


            }
        }


        if (!s.Equals(""))
        {
            s = "<ul><li><a href='?locid='>All</a></li>" + s + "</ul>";
        }

        return m + s;
    }



    //protected string getWHLocations(TreeNode tnode)
    //{
    //    string locid = tnode.Value;
    //    DataTable dt = WH.getWarehouses(locid);

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        TreeNode tn = new TreeNode();
    //        tn.Value = dr["wh_id"].ToString();
    //        tn.Text = dr["whname"].ToString();

    //        setWHLocations_Sections(tn, dr["wh_id"].ToString());

    //        tnode.ChildNodes.Add(tn);
    //    }
    //}


    #region Treeview
    
    protected void loadTreeView()
    {
        DataTable dt = WH.getWarehouseLocations();

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["locid"].ToString();
            tn.Text = dr["locname"].ToString();

            setWHLocations(tn);

            tview1.Nodes.Add(tn);
        }
    }

    protected void setWHLocations(TreeNode tnode)
    {
        string locid = tnode.Value;
        DataTable dt = WH.getWarehouses(locid);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["wh_id"].ToString();
            tn.Text = dr["whname"].ToString();

            setWHLocations_Sections(tn, dr["wh_id"].ToString());

            tnode.ChildNodes.Add(tn);
        }
    }

    protected void setWHLocations_Sections(TreeNode tnode, string wh_id, string parentid = "0")
    {
        //string wh_id = tnode.Value;
        DataTable dt = WH_Locations.getLocations2(wh_id, parentid);

        foreach (DataRow dr in dt.Rows)
        {
            TreeNode tn = new TreeNode();
            tn.Value = dr["whLocId"].ToString();
            tn.Text = dr["whLocName"].ToString();

            parentid = dr["whLocId"].ToString();

            setWHLocations_Sections(tn, wh_id, parentid);

            tnode.ChildNodes.Add(tn);   
        }
    }
    #endregion
}
 
