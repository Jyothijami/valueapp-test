using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class abouthelp : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DataList1.Items.Count == 0)
            {
                hlp.add_helptbl1(Request.QueryString["purl"], "", "", "");

                DataList1.DataBind();
            }
        }
    }
    
    
    protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        string pageurl, helpcnt;

        pageurl = Request.QueryString["purl"];
        //helpcnt = ((TextBox)e.Item.FindControl("tbxhlpcnt1")).Text;
        helpcnt = ((AjaxControlToolkit.HTMLEditor.Editor)e.Item.FindControl("helpcntEditor1")).Content;

        if (hlp.update_helpcnt(pageurl, helpcnt))
        {
            DataList1.EditItemIndex = -1;
            DataList1.DataBind();
        }
    }
    protected void DataList1_CancelCommand(object source, DataListCommandEventArgs e)
    {
        DataList1.EditItemIndex = -1;
        DataList1.DataBind();
    }
    protected void DataList2_EditCommand(object source, DataListCommandEventArgs e)
    {
        DataList2.EditItemIndex = 0;
        DataList2.DataBind();
    }
    protected void DataList2_CancelCommand(object source, DataListCommandEventArgs e)
    {
        DataList2.EditItemIndex = -1;
        DataList2.DataBind();
    }
    protected void DataList2_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        string pageurl, helpcnt;

        pageurl = Request.QueryString["purl"];
        helpcnt = ((TextBox)e.Item.FindControl("tbxpageflow1")).Text;

        if (hlp.update_pageflow(pageurl, helpcnt))
        {
            DataList2.EditItemIndex = -1;
            DataList2.DataBind();
        }
    }
    protected void DataList3_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        string pageurl, helpcnt;

        pageurl = Request.QueryString["purl"];
        helpcnt = ((TextBox)e.Item.FindControl("tbxhlptblssps1")).Text;

        if (hlp.update_tbls_sps(pageurl, helpcnt))
        {
            DataList3.EditItemIndex = -1;
            DataList3.DataBind();
        }
    }
    protected void DataList3_CancelCommand(object source, DataListCommandEventArgs e)
    {
        DataList3.EditItemIndex = -1;
        DataList3.DataBind();
    }
    protected void DataList3_EditCommand(object source, DataListCommandEventArgs e)
    {
        DataList3.EditItemIndex = 0;
        DataList3.DataBind();
    }
    protected void DataList1_EditCommand(object source, DataListCommandEventArgs e)
    {
        DataList1.EditItemIndex = 0;
        DataList1.DataBind();
    }
}