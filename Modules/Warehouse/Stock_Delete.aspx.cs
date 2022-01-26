using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Warehouse_Stock_Delete : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            setControlsVisibility();
        }
    }
    protected void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnDelete.Enabled = up.Delete;
    }
    private void BindAvailableStock()
    {
        SqlCommand cmd = new SqlCommand("USP_AvailableStock_Delete_2", con);
        cmd.CommandType = CommandType.StoredProcedure;

        if (txtItemCode.Text != "")
        {
            cmd.Parameters.AddWithValue("@ItemCode", txtItemCode.Text);
        }
        if (txtLocation.Text != "")
        {
            cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
        }
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);
        gvAvailableStock.DataSource = dt;
        gvAvailableStock.DataBind();
    }
    protected void gvAvailableStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAvailableStock.PageIndex = e.NewPageIndex;
        BindAvailableStock();
    }
    protected void gvAvailableStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach(GridViewRow gvrow in gvAvailableStock.Rows)
            {
                Inventory.Delivery obj = new Inventory.Delivery();
                //string itemId = gvrow.Cells[0].Text;
                //obj.ItemId = itemId.ToString();
                obj.TotalQty1 = gvrow.Cells[7].Text;
                TextBox qty1 = (TextBox)gvrow.FindControl("txtDeleteQty");
                obj.qty = qty1.Text;
                obj.ItemCode = gvrow.Cells[1].Text;
                obj.Cp_Id = cp.getPresentCompanySessionValue();
                obj.GoDownName = gvrow.Cells[3].Text;
                obj.date = DateTime.Now.ToString();
                obj.Color = gvrow.Cells[2].Text;
                obj.Remarks = txtRemarks.Text;
                obj.DCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                
                CheckBox ch = new CheckBox();
                    ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                    if (ch.Checked == true)
                    {
                        TextBox qty = (TextBox)gvrow.FindControl("txtDeleteQty");
                        string actualQty = gvrow.Cells[4].Text;

                        int updateQty = (Convert.ToInt32(actualQty.ToString()) - Convert.ToInt32(qty.Text));
                        if(updateQty >0)
                        {
                            string itemId = gvrow.Cells[0].Text;
                            obj.ItemId = itemId.ToString();
                            SqlCommand cmd = new SqlCommand("Update Inward_New set Quantity = " + updateQty + " where Item_Id = '" + itemId + "'  ", con);
                            cmd.CommandType = CommandType.Text;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //DeleteHistory_Save();
                            obj.OutwardDeleteHistory();

                        }
                        else if(updateQty == 0)
                        {
                            string itemId = gvrow.Cells[0].Text;
                            obj.ItemId = itemId.ToString();
                            SqlCommand cmd = new SqlCommand("Delete from Inward_New where Item_Id = '" + itemId + "'  ", con);
                            cmd.CommandType = CommandType.Text;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            obj.OutwardDeleteHistory();

                        }
                        else
                        {
                            MessageBox.Show(this, "You Cannot delete more Items than the existing Quantity");
                        }
                    }
                    gvHistory.DataBind();
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(this,ex.Message);
        }
        finally
        {
            btnSearch_Click(sender, e);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAvailableStock();
        tblHisty.Visible = true;
    }
    protected void DeleteHistory_Save()
    {
        try
        {
            foreach (GridViewRow gvrow in gvAvailableStock.Rows)
            {
                CheckBox ch = new CheckBox();
                ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                if (ch.Checked == true)
                {
                    Inventory.Delivery obj = new Inventory.Delivery();
                    string itemId = gvrow.Cells[0].Text;
                    obj.ItemId = itemId.ToString();
                    obj.TotalQty1 = gvrow.Cells[7].Text;
                    TextBox qty1 = (TextBox)gvrow.FindControl("txtDeleteQty");
                    obj.qty = qty1.Text;
                    string actualQty1 = gvrow.Cells[4].Text;
                    obj.actualQty = actualQty1.ToString();

                    int updateQty1 = (Convert.ToInt32(actualQty1.ToString()) - Convert.ToInt32(qty1.Text));
                    if (updateQty1 >0)
                    {
                        obj.updateQty = updateQty1.ToString();
                    }
                    else
                    {
                        obj.updateQty = actualQty1.ToString();
                    }
                    obj.ItemCode = gvrow.Cells[1].Text;
                    obj.Cp_Id = cp.getPresentCompanySessionValue();
                    obj.GoDownName = gvrow.Cells[3].Text;
                    obj.date = DateTime.Now.ToString("dd/MM/yyyy");
                    obj.Color = gvrow.Cells[2].Text;
                    obj.Remarks = txtRemarks.Text;
                    obj.DCPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                    obj.OutwardDeleteHistory();
                }
            }
            gvHistory.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnHistory_Click(object sender, EventArgs e)
    {
        gvHstrysearch.DataBind();
        tblHistorySearch.Visible = true;
       
    }
    protected void gvHstrysearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHstrysearch.PageIndex = e.NewPageIndex;
        gvHstrysearch.DataBind();
    }
}
 
