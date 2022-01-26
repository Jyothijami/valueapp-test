using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL.Modules;
using System.Data;
using vllib;
using System.IO;
using Yantra.MessageBox;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using Yantra.Classes;
using System.Drawing;
using System.Collections.Generic;
public partial class Modules_Reports_SCM : basePage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

    string pagenavigationstr;
    string _cmdText;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  SuppName_Fill();
            ItemType_Fill();
            CompanyName_Fill();
            Department_Fill();
            EmployeeNames_Fill();
            Masters.ProductCompany.ProductCompany_Select(ddlBrand);
            Masters.ProductCompany.ProductCompany_Select(ddlBrand1);

            Masters.CompanyProfile.Company_Select(ddlComp);
            ddlComp.Items.FindByText("--").Text = "All";
            
            //Masters.ProductCompany.ProductCompany_Select(ddlCompanyMIDS);
            Masters.ProductCompany.ProductCompany_Select(DropDownList2);
            //DropDownList2.Items.FindByText("--").Text = "--";
            Masters.ItemMaster.ItemMaster_Select(ddlModelNo);
            Masters.ColorMaster.Color_Select(ddlColor);
        }
    }
    
    #region Department Fill
    public void Department_Fill()
    {
        try
        {
            Masters.Department.Department_Select(ddlDepartmentShipment);
            Masters.Department.Department_Select(ddlDept);
            Masters.Department.Department_Select(ddlDeptPI );
            //Masters.Department.Department_Select(ddlDepartmentProforma);
            Masters.ItemCategory.ItemCategory_Select(ddlDepartmentProforma);
            ddlDepartmentShipment.Items.FindByText("--").Text = "All";
            ddlDept.Items.FindByText("--").Text = "All";
            ddlDeptPI.Items.FindByText("--").Text = "All";
            ddlDepartmentProforma.Items.FindByText("--").Text = "All";
            //Masters.Department.Department_Select(ddlSuplierEnqDepartment);
            //ddlSuplierEnqDepartment.Items.FindByText("--").Text = "All";
            Masters.Department.Department_Select(ddlDepartmentPO);
            ddlDepartmentPO.Items.FindByText("--").Text = "All";
            Masters.ProductCompany.ProductCompany_Select(ddlEmployeeNameProforma);
            ddlEmployeeNameProforma.Items.FindByText("--").Text = "All";
            Masters.EnquiryMode.EnquiryMode_Select  (ddlReference );
            ddlReference.Items.FindByText("--").Text = "All";

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }
    }
    #endregion 

    #region CompanyName Fill
    public void CompanyName_Fill()
    {
        try
        {
            //Masters.CompanyProfile.Company_Select(ddlCompanyNameSuplierEnq);
            //ddlCompanyNameSuplierEnq.Items.FindByText("--").Text = "All";
            //Masters.CompanyProfile.Company_Select(ddlCompanyNameProforma);
            //ddlCompanyNameProforma.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyNameShipment);
            ddlCompanyNameShipment.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyPO);
            ddlCompanyPO.Items.FindByText("--").Text = "All";
            Masters.CompanyProfile.Company_Select(ddlCompanyNamePurInvoice);
            ddlCompanyNamePurInvoice.Items.FindByText("--").Text = "All";
            //Masters.CompanyProfile.Company_Select(ddlDcCompany);
            //ddlDcCompany.Items.FindByText("--").Text = "All";

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            Masters.Dispose();
        }

    }
    #endregion

    #region Employee Names Fill
    private void EmployeeNames_Fill()
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeNamePO);
            ddlEmployeeNamePO.Items.FindByText("--").Text = "All";
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmp );
            ddlEmp.Items.FindByText("--").Text = "All";
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlEmpSuplierEnq);
            //ddlEmpSuplierEnq.Items.FindByText("--").Text = "All";
            //HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeNameProforma);
            //ddlEmployeeNameProforma.Items.FindByText("--").Text = "All";
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeNameShipment);
            ddlEmployeeNameShipment.Items.FindByText("--").Text = "All";
            HR.EmployeeMaster.EmployeeMaster_Select(ddlEmployeeNamePI);
            ddlEmployeeNamePI.Items.FindByText("--").Text = "All";


        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }

    }
    #endregion

    //#region SuppName Fill
    //private void SuppName_Fill()
    //{
    //    try
    //    {
    //        SCM.SuppliersMaster.SuppliersMaster_Select(ddlSupplierName);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(this, ex.Message);
    //    }
    //    finally
    //    {
    //        SCM.Dispose();
    //    }

    //}
    //#endregion

    #region ItemType Filll
    private void ItemType_Fill()
    {
        try
        {
          //  Masters.ItemType.ItemType_Select(ddlItemType);
          //  ddlItemType.Items.FindByText("--").Text = "All";
          ////  Masters.ItemType.ItemType_Select(ddlSERItemType);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
           // Masters.Dispose();
        }

    }
    #endregion

    #region Item Name Fill
    private void ItemName_Fill()
    {
        try
        {
        //    Masters.ItemMaster.ItemMaster_Select(ddlSERItemName, ddlSERItemType.SelectedValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            //Masters.Dispose();
        }
    }
    #endregion

    protected void lbtnMenuLinks_Click(object sender, EventArgs e)
    {
        LinkButton lbtnMenuLinks;
        lbtnMenuLinks = (LinkButton)sender;

        lbtnGoodsReceiptOfTheDay.CssClass = "leftmenu";
        lbntDispatchDetailsForTheDay.CssClass = "leftmenu";
        //lbtnSuppliersEvaluationReport.CssClass = "leftmenu";
        //lbtnApprovedSuppliersReport.CssClass = "leftmenu";
        lbtnClosingStockReport.CssClass = "leftmenu";
        lbtnPurchaseOrderList.CssClass = "leftmenu";
       // lbtnSupliersEnquiry.CssClass = "leftmenu";
        lbtnProformaInvoice.CssClass = "leftmenu";
       // lbtnShipmentDetails.CssClass = "leftmenu";
        lbtnPurchaseInvoice.CssClass = "leftmenu";
        lbtnResverveStock.CssClass = "leftmenu";

        tblGoodsReceipt.Visible = false;
        tblDespatchDetails.Visible = false;
        //tblSuppliersEvaluation.Visible = false;
        //tblApprovedSuppliers.Visible = false;
        tblClosingStock.Visible = false;
        tblPOL.Visible = false;
       // tblSupEnq.Visible = false;
        tblProforma.Visible = false;
        tblPI.Visible = false;
        tblShipment.Visible = false;
        tblReserveStockHistory.Visible = false;

        switch (lbtnMenuLinks.ID)
        {
            case "lbtnGoodsReceiptOfTheDay":
                {
                    tblGoodsReceipt.Visible = true;
                    lbtnGoodsReceiptOfTheDay.CssClass = "leftmenuhighlight";
                    //txtInvoiceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
           
            case "lbtnProformaInvoice":
                {
                    tblProforma.Visible = true;
                    lbtnProformaInvoice.CssClass = "leftmenuhighlight";
                    txtProformaFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtproformaTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
          
            case "lbtnPurchaseInvoice":
                {
                    tblPI.Visible = true;
                    lbtnPurchaseInvoice.CssClass = "leftmenuhighlight";
                    txtPiFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtPiTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
            case "lbntDispatchDetailsForTheDay":
                {
                    tblDespatchDetails.Visible = true;
                    lbntDispatchDetailsForTheDay.CssClass = "leftmenuhighlight";
                    txtDispatchDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                }
          
            case "lbtnClosingStockReport":
                {
                    tblClosingStock.Visible = true;
                    lbtnClosingStockReport.CssClass = "leftmenuhighlight";
                    break;
                }
            case "lbtnPurchaseOrderList":
                {
                    tblPOL.Visible = true;
                    txtPOLFromDate.Text = txtPOLToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    lbtnPurchaseOrderList.CssClass = "leftmenuhighlight";
                    break;
                }
            case "lbtnResverveStock":
                {
                    tblReserveStockHistory.Visible = true;
                    
                    break;
                }
            case "lbtnShipping":
                {
                    tblShipment.Visible = true;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    protected void btnGoodsReceiptRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=GoodsReceiptOfTheDay&F=" + Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text) + "&T=" + Yantra.Classes.General.toMMDDYYYY(txtInDt.Text) + "&MRN=" + ddlMRNNo.SelectedValue + "&cmp=" + DropDownList1.SelectedValue + "&Brand=" + DropDownList2.SelectedValue + "&InvNo=" + txtinvNo.Text + "";

        //pagenavigationstr = "../Reports/EODReportViewer.aspx?type=goodsreceiptoftheday&date=" + Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text) + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnDespatchDetailsRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=despatchdetailsfortheday&date=" + Yantra.Classes.General.toMMDDYYYY(txtDispatchDate.Text) + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

  

    protected void btnPOLRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=PurOrderStmt&f=" + Yantra.Classes.General.toMMDDYYYY(txtPOLFromDate.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtPOLToDate.Text) + "&c="+ ddlCompanyPO.SelectedValue +"&d="+ ddlDepartmentPO.SelectedValue +"&e="+ddlEmployeeNamePO.SelectedValue;
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void ddlSERItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemName_Fill();
    }

   
   

   

    protected void gvSalesOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "return confirm('Are you sure you want to remove this Record?');");
        }

    }

    protected void ddlDepartmentPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmployeeNamePO, ddlDepartmentPO.SelectedValue);
            ddlEmployeeNamePO.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }

   
    protected void ddlDepartmentProforma_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmployeeNameProforma, ddlDepartmentProforma.SelectedValue);
            ddlEmployeeNameProforma.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmp , ddlDept.SelectedValue);
            ddlEmp.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }
    protected void ddlDepartmentShipment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmployeeNameShipment, ddlDepartmentShipment.SelectedValue);
            ddlEmployeeNameShipment.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }
    }

    
    protected void ddlDeptPI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HR.EmployeeMaster.EmployeeMaster_SelectDept(ddlEmployeeNamePI, ddlDeptPI.SelectedValue);
            ddlEmployeeNamePI.Items.FindByText("--").Text = "All";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            HR.Dispose();
        }

    }

    

    protected void btnInternal_Click(object sender, EventArgs e)
    {
        tblpRint.Visible = true;
           }  

    protected void btnShipment_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ShipmentDet&f=" + Yantra.Classes.General.toMMDDYYYY(txtShipmentFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtShipmentTo.Text) + "&c=" + ddlCompanyNameShipment.SelectedValue + "&b=" + ddlBrand1.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnPIStmt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=PurInvStmt&f=" + Yantra.Classes.General.toMMDDYYYY(txtPiFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtPiTo.Text) + "&c=" + ddlCompanyNamePurInvoice.SelectedValue + "&e=" + ddlEmployeeNamePI.SelectedValue + "&d=" + ddlDeptPI.SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ReserveStockHistory";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("USP_BlockedItems_AllNew", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CompanyName", ddlCompany.SelectedItem.Value);
        }
        if (ddlLocation.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@locid", ddlLocation.SelectedItem.Value);
        }
        if (ddlModelNo.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@ItemCode", ddlModelNo.SelectedItem.Value);
        }

        if (ddlBrand.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Brand", ddlBrand.SelectedItem.Value);
        }
        if (ddlColor.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@Color", ddlColor.SelectedItem.Value);
        }
        if (txtFromDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtFromDate.Text));
        }
        if (txtToDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtToDate.Text));
        }
        if (txtItemCode.Text != "")
        {
            cmd.Parameters.AddWithValue("@ModelNo", txtItemCode.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvStockReport.DataSource = dt;
        gvStockReport.DataBind();
        btnPrint.Visible = true;

    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_BrandSelect(ddlModelNo, ddlBrand.SelectedItem.Value);
    }
    protected void ddlModelNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Masters.ItemMaster.ItemMaster_ModelNoSelect(ddlColor, ddlModelNo.SelectedValue);
    }
    //protected void btnSearchGo_Click(object sender, EventArgs e)
    //{
    //    BindReservedStockGrid();
    //    btnPrint1.Visible = true;
    //}
    //private void BindReservedStockGrid()
    //{
    //    SqlCommand cmd = new SqlCommand("[USP_CustomerBlockedItems]", con);
    //    cmd.CommandType = CommandType.StoredProcedure;

    //    if (txtSearchText.Text != "")
    //    {
    //        cmd.Parameters.AddWithValue("@ModelNo", txtSearchText.Text);
    //    }
    //    if (txtCustomer.Text != "")
    //    {
    //        cmd.Parameters.AddWithValue("@Customer", txtCustomer.Text);
    //    }
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    gvReservedStock.DataSource = dt;
    //    gvReservedStock.DataBind();
    //}

    protected void chkOriginal_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ProformaInvoice&f=" + Yantra.Classes.General.toMMDDYYYY(txtProformaFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtproformaTo.Text) + "&c=" + ddlCompanyNameProforma.SelectedValue + "&e=" + ddlDepartmentProforma.SelectedValue + "&d=" + ddlEmployeeNameProforma.SelectedValue + "&Reg=" + ddlReference.SelectedValue  +" ";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void chkDuplicate_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=ProformaInvoiceBar&f=" + Yantra.Classes.General.toMMDDYYYY(txtProformaFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtproformaTo.Text) + "&c=" + ddlCompanyNameProforma.SelectedValue + "&e=" + ddlDepartmentProforma.SelectedValue + "&d=" + ddlEmployeeNameProforma.SelectedValue + "&Reg=" + ddlReference.SelectedValue + " ";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);

    }
    protected void chktriplicate_CheckedChanged(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=CustBrand&f=" + Yantra.Classes.General.toMMDDYYYY(txtProformaFrom.Text) + "&t=" + Yantra.Classes.General.toMMDDYYYY(txtproformaTo.Text) + "&c=" + ddlCompanyNameProforma.SelectedValue + "&e=" + ddlDepartmentProforma.SelectedValue + "&d=" + ddlEmployeeNameProforma.SelectedValue + "&Reg=" + ddlReference.SelectedValue + " ";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void btnMRNSearch_Click(object sender, EventArgs e)
    {
        BindSearchGrid();
    }
    protected void gvItemMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string imageName = "~/Content/Images/" + (e.Row.FindControl("lblPath") as Label).Text;
            string[] filename = imageName.Split('/');

            // 70 is define image size.
            GenerateThumbNail("~/Content/ItemImage/" + filename[3], imageName, 100);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row .RowType ==DataControlRowType .Footer )
        {
            e.Row.Cells[9].Visible = false;
        }
        
    }

    public void GenerateThumbNail(string sourcefile, string destinationfile, int width)
    {
        if (File.Exists(Server.MapPath(sourcefile)))
        {

            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(sourcefile));
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            int thumbWidth = width;
            int thumbHeight;
            Bitmap bmp;
            if (srcHeight > srcWidth)
            {
                thumbHeight = (srcHeight / srcWidth) * thumbWidth;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }
            else
            {
                thumbHeight = thumbWidth;
                thumbWidth = (srcWidth / srcHeight) * thumbHeight;
                bmp = new Bitmap(thumbWidth, thumbHeight);
            }

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);

            bmp.Save(Server.MapPath(destinationfile));
            bmp.Dispose();
            image.Dispose();
            //DeleteTempImage(sourcefile, destinationfile);

        }
    }
    protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemMaster.PageSize = Convert.ToInt32(ddlNoOfRecords.SelectedValue);
        BindSearchGrid();
        //gvSalesOrderDetails.DataBind();
    }
    protected void BindSearchGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_MRN_SEARCH", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if (ddlCompany.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CPID", DropDownList1.SelectedItem.Value);
        }
        if (ddlMRNNo.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@CHKID", ddlMRNNo.SelectedItem.Value);
        }
        if (DropDownList2.SelectedIndex != 0)
        {
            cmd.Parameters.AddWithValue("@BRand", DropDownList2.SelectedItem.Value);
        }

        if (txtInvoiceDate.Text != "")
        {
            cmd.Parameters.AddWithValue("@FromDate", General.toMMDDYYYY(txtInvoiceDate.Text));
        }
        if (txtInDt.Text != "")
        {
            cmd.Parameters.AddWithValue("@ToDate", General.toMMDDYYYY(txtInDt.Text));
        }
        if (txtinvNo.Text != "")
        {
            cmd.Parameters.AddWithValue("@InvNo", txtinvNo.Text);
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvItemMaster.DataSource = dt;
        gvItemMaster.DataBind();
        btnExport.Visible = true;
    }
    protected void gvItemMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemMaster.PageIndex = e.NewPageIndex;
        //BindGrid();
        BindSearchGrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvItemMaster.Rows.Count > 0)
        {

            //Image1.ImageUrl = this.GetAbsoluteUrl(Image1.ImageUrl);
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages

                    gvItemMaster.AllowPaging = false;
                    BindSearchGrid();
                    //gvterms.AllowPaging = false;
                    //gvterms.DataBind();
                    //gvitemsgrid.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvItemMaster.HeaderRow.Cells)
                    {
                        cell.BackColor = gvItemMaster.HeaderStyle.BackColor;
                        //cell.BackColor = gvterms.HeaderStyle.BackColor;

                    }
                    foreach (GridViewRow row in gvItemMaster.Rows)
                    {
                        //row.BackColor = Color.White;
                        row.HorizontalAlign = HorizontalAlign.Center;
                        gvItemMaster.HorizontalAlign = HorizontalAlign.Center;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvItemMaster.AlternatingRowStyle.BackColor;
                                //cell.BackColor = gvterms.AlternatingRowStyle.BackColor;

                                cell.Wrap = true;
                            }
                            else
                            {
                                cell.BackColor = gvItemMaster.RowStyle.BackColor;
                                //cell.BackColor = gvterms.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }

                        gvItemMaster.Style["font-family"] = "Book Antiqua, Helvetica, sans-serif;";
                        row.Style.Add(HtmlTextWriterStyle.Height, "100px");
                        row.Style.Add(HtmlTextWriterStyle.Width, "100px");
                        string imageName = "~/Content/Images/" + (row.FindControl("lblPath") as Label).Text;
                        System.Web.UI.WebControls.Image img1 = row.Cells[10].Controls[1] as System.Web.UI.WebControls.Image;
                        row.Cells[10].Controls.Add(img1);
                        img1.Height = Unit.Pixel(150);
                        img1.Width = Unit.Pixel(150);
                    }

                    gvItemMaster.RenderControl(hw);
                    //gvterms.RenderControl(hw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Quotation.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";

                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void btnMIDSRpt_Click(object sender, EventArgs e)
    {
        pagenavigationstr = "../Reports/EODReportViewer.aspx?type=Salesrpt&y=" + Yantra.Classes.General.toMMDDYYYY( txtfrom .Text)  + "&m=" + Yantra.Classes.General.toMMDDYYYY( txtTo.Text)  + "&cmp=" + DropDownList3.SelectedValue + "&Dept=" + ddlDept.SelectedValue + "&Emp=" + ddlEmp.SelectedValue  + "&ST="+ ddlSaleType .SelectedValue + "";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "rptwindow", "window.open('" + pagenavigationstr + "','rptview','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')", true);
    }
    protected void gvStockReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string imageName = "~/Content/Images/" + (e.Row.FindControl("lblPath") as Label).Text;
            string[] filename = imageName.Split('/');

            // 70 is define image size.
            GenerateThumbNail("~/Content/ItemImage/" + filename[3], imageName, 100);
        }
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Visible = false;
        }
    }
    protected void txtInvoiceDate_TextChanged(object sender, EventArgs e)
    {
        if (txtInvoiceDate.Text != "")
        {
            txtInDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void txtInDt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ibtmImage_Click(object sender, ImageClickEventArgs e)
    {
        if (txtInvoiceDate.Text != "" && txtInDt.Text !="")
        {
            SCM.CheckingFormat.CheckinfDormat_SelctByDate(ddlMRNNo, Yantra.Classes.General.toMMDDYYYY(txtInvoiceDate.Text),Yantra.Classes.General.toMMDDYYYY( txtInDt.Text));
        }
        else
        {
            MessageBox.Show(this, "Please Select Dates");
        }
    }
}

 
