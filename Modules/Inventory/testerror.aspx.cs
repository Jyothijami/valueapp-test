using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Inventory_testerror : System.Web.UI.Page
{
    string id = "12";
    SM.SalesOrder objSalesOrder = new SM.SalesOrder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objSalesOrder.SalesOrderDetails_Select(id, gvDonepo);

        }
    }

    protected void lbtnDelete_Click1(object sender, EventArgs e)
    {
        LinkButton lbtnSalesOrderNo;
        lbtnSalesOrderNo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnSalesOrderNo.Parent.Parent;
        gvDonepo.SelectedIndex = gvRow.RowIndex;
       
        //objSalesOrder.SalesOrderDetailsDone_Delete(gvDonepo.SelectedRow.Cells[1].Text);
        MessageBox.Show(this, "Data Deleted");
        objSalesOrder.SalesOrderDetails_Select(id, gvDonepo);
    }
}