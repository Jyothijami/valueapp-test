using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.Data;
using vllib;


public partial class Modules_SM_LeadNew : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "fnPageLoad()", true);
       

      
        if (!IsPostBack)
        {

            Masters.ProductCompany.ProductCompany_Select(ddlBrand2);
            Masters.ItemCategory.ItemCategory_Select(ddlCategory);
            SM.SalesEnquiry objSM = new SM.SalesEnquiry();

            objSM.SalesEnquiryDetails_Select_SelfQuot(Request.QueryString["Cid"].ToString(), gvInterestedProducts);
            gvItemMaster.DataBind();
        }
        
    }

    protected void btnModelSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //BindGrid();
            BindSearchGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
    }
    protected void ddlBrand2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlBrand.SelectedIndex = ddlBrand2.SelectedIndex;
            Masters.ItemCategory.ItemCategory_Select_WithBrand(ddlCategory, ddlBrand2.SelectedItem.Value);
            //BindSearchGrid();
            gvItemMaster.DataBind();


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }


    }
    
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Masters.ItemType.ItemTypeCategory_Select(ddlSubCat, ddlCategory.SelectedValue);
           // gvItemMaster.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            Masters.Dispose();
        }
    }
    protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSearchGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message.ToString());
        }
        finally
        {
            //Masters.Dispose();
        }
    }


    protected void gvItemMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[13].Text == "Discontinued")
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text + ' ' + " (Discontinued)";
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;

            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;

            e.Row.Cells[1].Visible = false;

            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;

        }
        
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lbtnDetId;
        lbtnDetId = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnDetId.Parent.Parent;
        gvItemMaster.SelectedIndex = gvRow.RowIndex;
        //lblDCID.Text = gvItemMaster.SelectedRow.Cells[8].Text;
        //Outward_Delete();

        SM.SalesEnquiry objSM = new SM.SalesEnquiry();

        objSM.SalesEnquiryDet_Delete(gvRow.Cells[19].Text);
        objSM.SalesEnquiryDetails_Select_SelfQuot(Request.QueryString["Cid"].ToString(), gvInterestedProducts);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "Lead()", true);


    }
    private void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_GetItemsReport_2", con);
        //SqlCommand cmd = new SqlCommand("[USP_StockReportNew_Serach]", con);
        cmd.CommandType = CommandType.StoredProcedure;



        if (txtModelNo.Text != "" && txtModelNo.Text != null)
        {
            cmd.Parameters.AddWithValue("@Model", txtModelNo.Text);
        }
        //if (txtseries.Text != "" && txtseries.Text != null)
        //{
        //    cmd.Parameters.AddWithValue("@Series", txtseries.Text);
        //}
        if (ddlBrand2.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand2.SelectedItem.Value);
        }

        if (ddlCategory.SelectedIndex != 0 && ddlCategory.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@Category", ddlCategory.SelectedItem.Value);
        }

        if (ddlSubCat.SelectedIndex != 0 && ddlSubCat.SelectedIndex != -1)
        {
            cmd.Parameters.AddWithValue("@Sub_Cat", ddlSubCat.SelectedItem.Value);
        }
        //if (ddlModelNo.SelectedIndex > 0)
        //{
        //    cmd.Parameters.AddWithValue("@Item_Code", ddlModelNo.SelectedItem.Value);
        //}

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvItemMaster.DataSource = dt;
        gvItemMaster.DataBind();
        // gvItemMaster.PageIndex = 0;

    }
  
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        LinkButton lbtnRegionalMaster;
        lbtnRegionalMaster = (LinkButton)sender;
        GridViewRow gvrow = (GridViewRow)lbtnRegionalMaster.Parent.Parent;
        gvItemMaster.SelectedIndex = gvrow.RowIndex;
        TextBox txtDetQty = (TextBox)gvrow.FindControl("txtDetQty");

        if (gvItemMaster.SelectedIndex > -1)
        {
            if (txtDetQty.Text != "")
            {
                try
                {
                    SM.SalesEnquiry objSM = new SM.SalesEnquiry();
                    objSM.EnqId = Request.QueryString["Cid"].ToString();

                    objSM.EnqDetItemCode = gvrow.Cells[2].Text;
                    objSM.modelno = gvrow.Cells[3].Text;
                    objSM.brand = gvrow.Cells[8].Text;
                    objSM.itemname = gvrow.Cells[5].Text;
                    objSM.EnqDetQty = txtDetQty.Text;
                    objSM.EnqDetSpec = gvrow.Cells[7].Text;
                    objSM.EnqDetRemarks = gvrow.Cells[8].Text;
                    objSM.EnqDetPriority = "-";
                    TextBox txtDetDisc = (TextBox)gvrow.FindControl("txtDetDisc");
                    objSM.EnqDocCharges = txtDetDisc.Text;
                    objSM.EnqDocFavourof = "-";
                    objSM.EnqEMDCharges = "-";
                    TextBox txtDetRoom = (TextBox)gvrow.FindControl("txtDetRoom");
                    objSM.EnqEMDFavourof = txtDetRoom.Text;
                    TextBox txtDetFloor = (TextBox)gvrow.FindControl("txtDetFloor");
                    objSM.EnqDetRoom = txtDetRoom.Text;
                    objSM.EnqColor = gvrow.Cells[14].Text;
                    objSM.uom = "-";
                    objSM.colorname = gvrow.Cells[9].Text;

                    MessageBox.Show(this, objSM.SalesEnquiryDetails_Save());
                  

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);

                }
                finally
                {
                   
                    SM.SalesEnquiry objSM = new SM.SalesEnquiry();

                    objSM.SalesEnquiryDetails_Select_SelfQuot(Request.QueryString["Cid"].ToString(), gvInterestedProducts);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "Lead()", true);
                    BindSearchGrid();
                }
            }
            else
            {
                MessageBox.Show(this, "Please Enter Qty of the product");

            }
        }
    }
    protected void btnQuot_Click(object sender, EventArgs e)
    {
        if (SM.SalesQuotation.IsSalesQuotationRaised(Request.QueryString["Cid"].ToString()) > 0)
        {
            MessageBox.Show(this, "Quotation has already prepared for this Sales Lead");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ISQuot()", true);

        }
        else 
        if (gvInterestedProducts.Rows.Count > 0)
        {
            try
            {
                btnQuot.Enabled = false;
                SM.SalesQuotation objSM = new SM.SalesQuotation();
                SM.BeginTransaction();
                //objSM.QuotNo = SM.SalesQuotation.SalesQuotation_AutoGenCode();
                objSM.QuotDate = DateTime .Now .ToString ();
                objSM.EnqId = Request.QueryString["Cid"].ToString();
                objSM.QuotPayTerms ="100% in advance";
                objSM.QuotDelivery = "Within 10-14 weeks";
                objSM.QuotPackCharges = "0";
                objSM.QuotExcise = "-";
                objSM.QuotVAT = "18";
                objSM.QuotCST = "";
                objSM.QuotIncluding = "";
                objSM.DespmId = "4";
                objSM.QuotGuarantee = "0";
                objSM.QuotTransCharges = "Transportation levies are extra as applicable";
                objSM.QuotInsurance = "0";
                objSM.QuotErrec = "";
                objSM.QuotJurisdiction = "";
                objSM.QuotValidity = DateTime.Now.ToString();
                objSM.QuotInspection ="";
                objSM.QuotOtherSpecs ="-";
                //   objSM.QuotPOLog = txtpo.Text;
                objSM.QuotRespId = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.QuotSalespId = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.QuotPreparedBy = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId);
                objSM.QuotCheckedBy = "0";
                objSM.QuotApprovedBy = "0";
                objSM.CurrencyId = "0";
                objSM.QuotDDNo = "4";
                objSM.QuotDDDate = DateTime.Now.ToString();
                //objSM.QuotBankName = txtBankName.Text;
                objSM.QuotBankName = "5";
                objSM.IsExpectedOrder = true;
                objSM.QuotTotalEMDCharges = "";
                objSM.QuotFOB = "0";
                objSM.QuotCIF = "0";
                objSM.QuotCompany = objSM.Cp_Id = cp.getPresentCompanySessionValue();
                objSM.ttlDisc = "0";
                 objSM.QuotType = "Discount";
                 if (objSM.SalesQuotation_Save() == "Data Saved Successfully")
                 {
                     foreach (GridViewRow gvrow in gvInterestedProducts .Rows)
                     {
                         objSM.QuotDetItemCode = gvrow.Cells[2].Text;
                         //objSM.QuotDetQty = gvrow.Cells[6].Text;
                         TextBox Quantity = (TextBox)gvrow.FindControl("txtLeadDetQty");
                         objSM.QuotDetQty = Quantity.Text;
                         //TextBox Rate = (TextBox)gvrow.FindControl("txtDetRate");
                         //objSM.QuotRate = Rate.Text;
                         objSM.QuotRate = gvrow.Cells[8].Text;
                         TextBox txtDisc = (TextBox)gvrow.FindControl("txtDisc");
                         objSM.QuotDetDisc = txtDisc.Text;
                         //objSM.QuotDetDisc = gvrow.Cells[10].Text;
                         objSM.QuotDetSpPrice = gvrow.Cells[21].Text;
                         objSM.QuotGSTperc = gvrow.Cells[22].Text;
                         objSM.QuotGSTRate = gvrow.Cells[23].Text;
                         //objSM.QuotRoom = gvrow.Cells[14].Text;
                         TextBox Room = (TextBox)gvrow.FindControl("txtRomm");
                         TextBox srl = (TextBox)gvrow.FindControl("txtSrlOrder");

                         objSM.QuotRoom = Room.Text;
                         objSM.QuotCurrency = "1";
                         objSM.ColorId = gvrow.Cells[17].Text;
                         objSM.OptionalId = srl.Text;
                         objSM.Remarks = "";
                         objSM.SrlOrder = gvrow.Cells[22].Text;
                         // objSM.SrlOrder = gvrow.Cells[21].Text;
                         objSM.SrlOrder = srl.Text;

                        MessageBox.Show(this, objSM.SalesQuotationDetails_Save());

                     }
                     SM.CommitTransaction();
                 }
                 else
                 {
                     SM.RollBackTransaction();
                 }
                
            }
            catch (Exception ex)
            {
                SM.RollBackTransaction();
                MessageBox.Show(this, ex.Message.ToString());
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "hi()", true);

            }
        }
        else
        {
            MessageBox.Show(this, "Please add atleast one Item for Quotation");
        }
    }
    #region gvInterestedProducts_RowDataBound
    protected void gvInterestedProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (ddlEnquirySource.SelectedItem.Text != "Tender")
            //{
            //e.Row.Cells[18].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;
            e.Row.Cells[23].Visible = false;
            e.Row.Cells[24].Visible = false;


        }
        GridViewRow gvr = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TextBox rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtLeadDetQty");
            e.Row.Cells[20].Text = (Convert.ToDecimal(e.Row.Cells[8].Text) * Convert.ToDecimal(Qty.Text)).ToString();
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            if (disc.Text == "-") { disc.Text = "0"; }
            e.Row.Cells[21].Text = ((Convert.ToDecimal(e.Row.Cells[8].Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(e.Row.Cells[8].Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            
            //e.Row.Cells[21].Text = (Convert.ToDecimal(e.Row.Cells[20].Text) - (Convert.ToDecimal(e.Row.Cells[20].Text) * Convert.ToDecimal(e.Row.Cells[24].Text)) / 100).ToString();
            e.Row.Cells[23].Text = decimal.Round((Convert.ToDecimal(e.Row.Cells[21].Text) * Convert.ToDecimal(e.Row.Cells[22].Text)) / 100).ToString();

        }

    }
    #endregion
    protected void txtLeadDetQty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvInterestedProducts.Rows)
        {
            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            
            TextBox Qty = (TextBox)gvr.FindControl("txtLeadDetQty");
            gvr.Cells[20].Text = (Convert.ToDecimal(gvr.Cells[8].Text) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[21].Text = ((Convert.ToDecimal(gvr.Cells[8].Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(gvr.Cells[8].Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[23].Text = decimal.Round((Convert.ToDecimal(gvr.Cells[21].Text) * Convert.ToDecimal(gvr.Cells[22].Text)) / 100).ToString();

        }
    }
    protected void txtDisc_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvInterestedProducts.Rows)
        {

            TextBox disc = (TextBox)gvr.FindControl("txtDisc");
            //TextBox Rate = (TextBox)gvr.FindControl("txtDetRate");
            TextBox Qty = (TextBox)gvr.FindControl("txtLeadDetQty");
            gvr.Cells[10].Text = disc.Text;
            gvr.Cells[21].Text = ((Convert.ToDecimal(gvr.Cells[8].Text) - (Convert.ToDecimal(disc.Text) * Convert.ToDecimal(gvr.Cells[8].Text)) / 100) * Convert.ToDecimal(Qty.Text)).ToString();
            gvr.Cells[23].Text = decimal .Round ((Convert.ToDecimal(gvr.Cells[21].Text) * Convert.ToDecimal(gvr.Cells[22].Text)) / 100).ToString();

        }
    }
}