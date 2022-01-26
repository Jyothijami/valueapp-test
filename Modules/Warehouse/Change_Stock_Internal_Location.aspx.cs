using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Yantra.MessageBox;
using YantraBLL.Modules;
using Yantra.Classes;
using vllib;
using System.IO;
using System.Drawing;

using System.Data.SqlClient;

public partial class Modules_Warehouse_Change_Stock_Internal_Location : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindSearchGrid();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindSearchGrid();
        //RefreshFields();
    }

    private void RefreshFields()
    {
        txtColor.Text = txtFrom.Text = txtMainLoc.Text = txtModelNo.Text = txtSubLoc.Text = txtToDate.Text = "";
    }

    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("usp_StockMoving3", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (txtModelNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtModelNo.Text);
        }

        if (txtSubLoc.Text != "")
        {
            cmd.Parameters.AddWithValue("@LocName", txtSubLoc.Text);

        }
        if (txtMainLoc.Text != "")
        {
            cmd.Parameters.AddWithValue("@Location", txtMainLoc.Text);

        }
        if (txtColor.Text != "")
        {
            cmd.Parameters.AddWithValue("@Colour", txtColor.Text);

        }
       
        if (txtFrom.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", Yantra.Classes.General.toMMDDYYYY(txtFrom.Text));

        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", Yantra.Classes.General.toMMDDYYYY(txtToDate.Text));

        }
        
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvStock.DataSource = dt;
        gvStock.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveMovingStock();
        BindSearchGrid();

    }
    private void SaveMovingStock()
    {
        if (TextBox2_value.Value != "")
        {
            #region Stock Movement

            foreach (GridViewRow gvrow in gvStock.Rows)
            {
                CheckBox ch = (CheckBox)gvrow.FindControl("CheckBox_row");
                if (ch.Checked == true)
                {
                    string Itemcode = gvrow.Cells[0].Text;

                    SqlCommand cmd = new SqlCommand("SELECT * FROM [V_MovingStock] where avail_qty>0 and [ITEM_CODE]=" + Itemcode + " and [whLocId]=" + gvrow.Cells[6].Text + " ", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Masters.ItemPurchase objout = new Masters.ItemPurchase();
                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
                    int Quantity = int.Parse(qty.Text);
                    int Quantity1 = int.Parse(qty.Text);
                    int locId = int.Parse(gvrow.Cells[6].Text);
                    int whLocId = Convert.ToInt32(WH_Locations.getLocationID(TextBox2_value.Value));
                    int rowcount = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Quantity1; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i][7].ToString()) > Convert.ToInt32(dt.Rows[i][4].ToString()))
                            {
                                if (Quantity >= Convert.ToInt32(dt.Rows[i][4]))
                                {
                                    int x = Convert.ToInt32(dt.Rows[i][7].ToString()) - Convert.ToInt32(dt.Rows[i][4].ToString());
                                    int locId1 = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    objout.InwardLocNew_Update(itemId, locId1, x);

                                    objout.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.companyid = dt.Rows[i][3].ToString();
                                    objout.MRNID = gvrow.Cells[0].Text;
                                    objout.COLORID = dt.Rows[i][2].ToString();
                                    objout.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                                    objout.Quantity = dt.Rows[i][4].ToString();
                                    objout.ItemInward_Save_New();
                                }
                                else
                                {
                                    int x = Convert.ToInt32(dt.Rows[i][7].ToString()) - Quantity;
                                    int locId1 = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    objout.InwardLocNew_Update(itemId, locId1, x);

                                    objout.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.companyid = dt.Rows[i][3].ToString();
                                    objout.MRNID = gvrow.Cells[0].Text;
                                    objout.COLORID = dt.Rows[i][2].ToString();
                                    objout.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                                    objout.Quantity = Quantity.ToString();
                                    objout.ItemInward_Save_New();
                                }
                            }
                            else
                            {
                                if (Quantity >= Convert.ToInt32(dt.Rows[i][4]))
                                {
                                    int locId1 = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    objout.InwardLocNew_Update(locId1, itemId, whLocId);
                                    //objout.qty = dt.Rows[i][4].ToString();
                                }
                                else if (Quantity < Convert.ToInt32(dt.Rows[i][4]))
                                {
                                    int locId1 = Convert.ToInt32(dt.Rows[i][1].ToString());
                                    string itemId = dt.Rows[i][0].ToString();
                                    int qty1 = Convert.ToInt32(dt.Rows[i][4]) - Quantity;
                                    objout.InwardLocNew_Update(itemId, locId1, qty1);

                                    objout.ItemID = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.Barcode = "I" + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + rowcount;
                                    objout.companyid = dt.Rows[i][3].ToString();
                                    objout.MRNID = gvrow.Cells[0].Text;
                                    objout.COLORID = dt.Rows[i][2].ToString();
                                    objout.locationid = WH_Locations.getLocationID(TextBox2_value.Value);
                                    objout.Quantity = Quantity.ToString();
                                    objout.ItemInward_Save_New();
                                }

                            }
                            rowcount++;
                            Quantity = Quantity - Convert.ToInt32(dt.Rows[i][4]);
                            if (Quantity <= 0)
                            {
                                break;
                            }
                            //int locId = Convert.ToInt32(dt.Rows[i][1].ToString());
                            //string itemId = dt.Rows[i][0].ToString();
                            //objout.InwardLoc_Update(locId, itemId, whLocId);
                        }
                    }
                    //if (dt.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < Quantity1; i++)
                    //    {

                    //        //string itemId = dt.Rows[i][0].ToString();
                    //        //objout.InwardLocNew_Update(locId, itemId, whLocId);
                    //    }
                    //}
                }
            }
            #endregion
        }
        else
        {
            MessageBox.Show(this, "Please Select Proper Location To Move Stock");
        }

    }
    //private void SaveMovingStock()
    //{
    //    if (TextBox2_value.Value != "")
    //    {
    //        #region Stock Movement

    //            foreach (GridViewRow gvrow in gvStock.Rows)
    //            {
    //                CheckBox ch = (CheckBox)gvrow.FindControl("CheckBox_row");
    //                if (ch.Checked == true)
    //                {
    //                    string Itemcode = gvrow.Cells[0].Text;

    //                    SqlCommand cmd = new SqlCommand("select Item_ID,whLocId from dbo.INWARD where [ITEM_CODE]=" + Itemcode + " and [whLocId]=" + gvrow.Cells[6].Text + " ", con);
    //                    cmd.CommandType = CommandType.Text;
    //                    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //                    DataTable dt = new DataTable();
    //                    da.Fill(dt);
    //                    Masters.ItemPurchase objout = new Masters.ItemPurchase();
    //                    TextBox qty = (TextBox)gvrow.FindControl("txtQuantity");
    //                    int Quantity = int.Parse(qty.Text);
    //                    int locId = int.Parse(gvrow.Cells[6].Text);
    //                    int whLocId = Convert.ToInt32(WH_Locations.getLocationID(TextBox2_value.Value));

    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        for (int i = 0; i < Quantity; i++)
    //                        {

    //                            string itemId = dt.Rows[i][0].ToString();
    //                            objout.InwardLoc_Update(locId, itemId, whLocId);
    //                        }
    //                    }
    //                }
    //            }
    //    #endregion
    //    }
    //    else
    //    {
    //        MessageBox.Show(this, "Please Select Proper Location To Move Stock");
    //    }

    //}
    protected void gvStock_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvStock.PageIndex = e.NewPageIndex;
        BindSearchGrid();
    }
    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Visible = false;
            foreach (GridViewRow rw in gvStock.Rows)
            {
                TextBox tx = (TextBox)rw.FindControl("txtQuantity");

                if (tx.Text == "0")
                    rw.Visible = false;

            }
            
        }
    }
}
 
