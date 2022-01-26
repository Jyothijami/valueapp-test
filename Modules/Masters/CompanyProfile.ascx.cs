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
using YantraBLL.Modules;
using Yantra.MessageBox;
using System.IO;
using System.Data.SqlClient;
using Yantra.Classes;
using vllib;

public partial class Modules_Masters_CompanyProfile : System.Web.UI.UserControl
{
    public string CompanyId;
    ScriptManager ScriptManagerLocal;
    static DateTime dt;

    #region PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
        if (!IsPostBack)
        {
            setControlsVisibility();
            //CompanyProfileSelect();
        }
    }
    #endregion

    private void setControlsVisibility()
    {
        User_Permissions up = new User_Permissions(Session["vl_userid"].ToString(), "2");
        btnNew.Enabled = up.add;
        btnEdit.Enabled = up.Update;
        btnDelete.Enabled = up.Delete;
        btnSave.Enabled = up.add;

    }


    #region Page PreRender
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save") { btnRefresh.Visible = true; } else if (btnSave.Text == "Update") { btnRefresh.Visible = false; }
    }
    #endregion

    #region CompanyProfileSave
    private void CompanyProfileSave()
    {
        try
        {
            Masters.CompanyProfile objMaster = new Masters.CompanyProfile();
            objMaster.CPFullName = txtFullName.Text;
            objMaster.CPShortName = txtShortName.Text;
            objMaster.CPAddress = txtAddress.Text;
            objMaster.CPContactNo1 = txtContactNo1.Text;
            objMaster.CPFaxNo = txtFaxNo.Text;
            objMaster.CPContactNo2 = txtContactNo2.Text;
            objMaster.CPEmail = txtEmail.Text;
            objMaster.CPTelexNo = txtTelexNo.Text;
            objMaster.CPAPGSTNo = txtAPGSTNo.Text;
            objMaster.CPCSTNo = txtCSTNo.Text;
            objMaster.CPECCNo = txtECCNo.Text;
            objMaster.CPVATNo = txtVATNo.Text;
            objMaster.CPPANNo = txtPANNo.Text;
            objMaster.CPEstYear = txtEstablishmentYear.Text;
            objMaster.CPCFYear = txtCurrentFinancialYear.Text;
            objMaster.CPCPONo = txtCurrentPurchaseOrderNo.Text;
            objMaster.CPCINo = txtCurrentInvoiceNo.Text;
            objMaster.CPCDCNo = txtCurrentDCNo.Text;
            objMaster.CPYearStartDate =Yantra.Classes.General.toMMDDYYYY( txtYearStartDate.Text);
            objMaster.CPYearEndDate = Yantra.Classes.General.toMMDDYYYY(txtYearEndDate.Text);
            objMaster.CPInvoicePrefix = txtInvoicePrefix.Text;
            objMaster.CPInvoiceSuffix = txtInvoiceSuffix.Text;
            objMaster.CPPOPrefix = txtPOPrefix.Text;
            objMaster.CPPOSuffix = txtPOSuffix.Text;
            objMaster.CPDCPrefix = txtDCPrefix.Text;
            objMaster.CPDCSuffix = txtDCSuffix.Text;
            
            objMaster.CPLogo = "Vline_Written.png";
            //objMaster.CPLogo = hfpicname1.Value;
            objMaster.locid = ddlLocation1.SelectedValue;

            objMaster.TechNo = txtTechNo.Text;
            objMaster.DespNo = txtDespNo.Text;
            if (rdbActive.Checked == true)
            {
                objMaster.Status = "1";
            }
            else
            {
                objMaster.Status = "0";
            }
            //string ReturnMessage = objMaster.CompanyProfile_Save();
            MessageBox.Show(this, objMaster.CompanyProfile_Save());
            Masters.CommitTransaction();
                       
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {           
            gvCompanyDetails.DataBind();
            Masters.ClearControls(this);
            tblCompanyDetails.Visible = false;
            Masters.Dispose();
        }
    }
    #endregion

    #region CompanyProfileUpdate
    private void CompanyProfileUpdate()
    {
        try
        {
            string companyId = gvCompanyDetails.SelectedRow.Cells[1].Text;
            string filename = "";

            if (FileUp1.HasFile)
            {
                filename = companyId + ".jpg";

                FileUp1.SaveAs(Server.MapPath("~/Content/CompanyProfileImgs/") + filename);

            }
            else
            {
                filename = hfpicname1.Value;
            }

            Masters.CompanyProfile objMaster = new Masters.CompanyProfile();
            objMaster.CPCompanyId = companyId;
            objMaster.CPFullName = txtFullName.Text;
            objMaster.CPShortName = txtShortName.Text;
            objMaster.CPAddress = txtAddress.Text;
            objMaster.CPContactNo1 = txtContactNo1.Text;
            objMaster.CPFaxNo = txtFaxNo.Text;
            objMaster.CPContactNo2 = txtContactNo2.Text;
            objMaster.CPEmail = txtEmail.Text;
            objMaster.CPTelexNo = txtTelexNo.Text;
            objMaster.CPAPGSTNo = txtAPGSTNo.Text;
            objMaster.CPCSTNo = txtCSTNo.Text;
            objMaster.CPECCNo = txtECCNo.Text;
            objMaster.CPVATNo = txtVATNo.Text;
            objMaster.CPPANNo = txtPANNo.Text;
            objMaster.CPEstYear = txtEstablishmentYear.Text;
            objMaster.CPCFYear = txtCurrentFinancialYear.Text;
            objMaster.CPCPONo = txtCurrentPurchaseOrderNo.Text;
            objMaster.CPCINo = txtCurrentInvoiceNo.Text;
            objMaster.CPCDCNo = txtCurrentDCNo.Text;
            objMaster.CPYearStartDate = Yantra.Classes.General.toMMDDYYYY(txtYearStartDate.Text);
            objMaster.CPYearEndDate =Yantra.Classes.General.toMMDDYYYY( txtYearEndDate.Text);
            //objMaster.CPYearStartDate = Yantra.Classes.General.toMMDDYYYY(txtYearStartDate.Text);
            //objMaster.CPYearEndDate = Yantra.Classes.General.toMMDDYYYY(txtYearEndDate.Text);
            objMaster.CPInvoicePrefix = txtInvoicePrefix.Text.Trim();
            objMaster.CPInvoiceSuffix = txtInvoiceSuffix.Text.Trim();
            objMaster.CPPOPrefix = txtPOPrefix.Text.Trim();
            objMaster.CPPOSuffix = txtPOSuffix.Text.Trim();
            objMaster.CPDCPrefix = txtDCPrefix.Text.Trim();
            objMaster.CPDCSuffix = txtDCSuffix.Text.Trim();
            objMaster.CPLogo = "Vline_Written.png";
            //objMaster.CPLogo = filename;
            
            objMaster.TechNo = txtTechNo.Text;
            objMaster.DespNo = txtDespNo.Text;

            objMaster.locid = ddlLocation1.SelectedValue;
            if (rdbActive.Checked == true)
            {
                objMaster.Status = "1";
            }
            else
            {
                objMaster.Status = "0";
            }
            MessageBox.Show(this, objMaster.CompanyProfile_Update());

        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvCompanyDetails.DataBind();
            Masters.ClearControls(this);
            tblCompanyDetails.Visible = false;
            Masters.Dispose();
        }
    }
    #endregion

    #region CompanyProfileSelect
    private void CompanyProfileSelect()
    {
        try
        {
            Masters.CompanyProfile objMaster = new Masters.CompanyProfile();
            if (objMaster.CompanyProfile_Select() > 0)
            {
                btnSave.Text = "Update";
                txtFullName.Text = objMaster.CPFullName;
                txtShortName.Text = objMaster.CPShortName;
                txtAddress.Text = objMaster.CPAddress;
                txtContactNo1.Text = objMaster.CPContactNo1;
                txtFaxNo.Text = objMaster.CPFaxNo;
                txtContactNo2.Text = objMaster.CPContactNo2;
                txtEmail.Text = objMaster.CPEmail;
                txtTelexNo.Text = objMaster.CPTelexNo;
                txtAPGSTNo.Text = objMaster.CPAPGSTNo;
                txtCSTNo.Text = objMaster.CPCSTNo;
                txtECCNo.Text = objMaster.CPECCNo;
                txtVATNo.Text = objMaster.CPVATNo;
                txtPANNo.Text = objMaster.CPPANNo;
                txtEstablishmentYear.Text = objMaster.CPEstYear;
                txtCurrentFinancialYear.Text = objMaster.CPCFYear;
                txtCurrentPurchaseOrderNo.Text = objMaster.CPCPONo;
                txtCurrentInvoiceNo.Text = objMaster.CPCINo;
                txtCurrentDCNo.Text = objMaster.CPCDCNo;
                txtYearStartDate.Text = objMaster.CPYearStartDate;
                txtYearEndDate.Text = objMaster.CPYearEndDate;
                txtInvoicePrefix.Text = objMaster.CPInvoicePrefix;
                txtInvoiceSuffix.Text = objMaster.CPInvoiceSuffix;
                txtPOPrefix.Text = objMaster.CPPOPrefix;
                txtPOSuffix.Text = objMaster.CPPOSuffix;
                txtDCPrefix.Text = objMaster.CPDCPrefix;
                txtDCSuffix.Text = objMaster.CPDCSuffix;
                hfpicname1.Value = objMaster.CPLogo;
                ddlLocation1.SelectedValue = objMaster.locid;

                if (!hfpicname1.Value.Equals(""))
                {
                    Image1.ImageUrl = "~/Content/CompanyProfileImgs/" + hfpicname1.Value;
                }
            }
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

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
    }
    #endregion

    #region Link Button Click
    protected void lbtnCompanyName_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
        LinkButton lbtnCompanyName;
        lbtnCompanyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvCompanyDetails.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button SAVE Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            CompanyProfileSave();
        }
        else if (btnSave.Text == "Update")
        {
            CompanyProfileUpdate();
        }
        gvCompanyDetails.SelectedIndex = -1;
    }
    #endregion

    #region New Button Click
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblCompanyDetails.Visible = true;
        Image1.ImageUrl = "~/Images/noimage400x300.gif";
        //ScriptManagerLocal.SetFocus(txtFullName);
    }
    #endregion

    #region Edit Button Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvCompanyDetails.SelectedIndex > -1)
        {
            tblCompanyDetails.Visible = true;
            txtFullName.Text=gvCompanyDetails.SelectedRow.Cells[0].Text;
            txtShortName.Text = gvCompanyDetails.SelectedRow.Cells[3].Text;
            txtAddress.Text = gvCompanyDetails.SelectedRow.Cells[4].Text;
            txtContactNo1.Text = gvCompanyDetails.SelectedRow.Cells[5].Text;
            txtFaxNo.Text = gvCompanyDetails.SelectedRow.Cells[6].Text;
            txtContactNo2.Text = gvCompanyDetails.SelectedRow.Cells[7].Text;
            txtEmail.Text = gvCompanyDetails.SelectedRow.Cells[8].Text;
            txtTelexNo.Text = gvCompanyDetails.SelectedRow.Cells[9].Text;
            txtAPGSTNo.Text = gvCompanyDetails.SelectedRow.Cells[10].Text;
            txtCSTNo.Text = gvCompanyDetails.SelectedRow.Cells[11].Text;
            txtECCNo.Text = gvCompanyDetails.SelectedRow.Cells[12].Text;
            if (txtECCNo.Text == "&nbsp;")
            {
                txtECCNo.Text = "";
            }
            else 
            {
                txtECCNo.Text =gvCompanyDetails.SelectedRow.Cells[12].Text;
            }
            txtVATNo.Text = gvCompanyDetails.SelectedRow.Cells[13].Text;
            txtPANNo.Text = gvCompanyDetails.SelectedRow.Cells[14].Text;
            //txtEstablishmentYear.Text = gvCompanyDetails.SelectedRow.Cells[15].Text;
            txtCurrentFinancialYear.Text = gvCompanyDetails.SelectedRow.Cells[16].Text;
            txtCurrentPurchaseOrderNo.Text = gvCompanyDetails.SelectedRow.Cells[17].Text;
            txtCurrentInvoiceNo.Text = gvCompanyDetails.SelectedRow.Cells[18].Text;
            txtCurrentDCNo.Text = gvCompanyDetails.SelectedRow.Cells[19].Text;
            txtYearStartDate.Text =Convert.ToDateTime(gvCompanyDetails.SelectedRow.Cells[20].Text).ToString("dd/MM/yyyy");
            dt = Convert.ToDateTime(gvCompanyDetails.SelectedRow.Cells[15].Text);
            txtEstablishmentYear.Text = Convert.ToString(dt.Year);
            txtYearEndDate.Text = Convert.ToDateTime(gvCompanyDetails.SelectedRow.Cells[21].Text).ToString("dd/MM/yyyy"); 
            txtInvoicePrefix.Text = gvCompanyDetails.SelectedRow.Cells[22].Text;
            txtInvoiceSuffix.Text = gvCompanyDetails.SelectedRow.Cells[23].Text;
            txtPOPrefix.Text = gvCompanyDetails.SelectedRow.Cells[24].Text;
            txtPOSuffix.Text = gvCompanyDetails.SelectedRow.Cells[25].Text;
            txtDCPrefix.Text = gvCompanyDetails.SelectedRow.Cells[26].Text;
            txtDCSuffix.Text = gvCompanyDetails.SelectedRow.Cells[27].Text;
            ddlLocation1.SelectedValue = gvCompanyDetails.SelectedRow.Cells[29].Text;

            txtTechNo.Text = gvCompanyDetails.SelectedRow.Cells[30].Text;
            txtDespNo.Text = gvCompanyDetails.SelectedRow.Cells[31].Text;
            
            string logostr = ((HiddenField)gvCompanyDetails.SelectedRow.Cells[28].FindControl("hfcplogo1")).Value;

            Image1.ImageUrl = "~/Content/CompanyProfileImgs/" + logostr;
            hfpicname1.Value = logostr;
            if (gvCompanyDetails.SelectedRow.Cells[32].Text == "Active")
            {
                rdbActive.Checked = true;
                rdbInactive.Checked = false;
            }
            else if (gvCompanyDetails.SelectedRow.Cells[32].Text == "Inactive")
            {
                rdbInactive.Checked = true;
                rdbActive.Checked = false;
            }
            btnSave.Text = "Update";

        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Delete Button Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvCompanyDetails.SelectedIndex > -1)
        {
            try
            {
                Masters.CompanyProfile objMaster = new Masters.CompanyProfile();
                objMaster.CPCompanyId = gvCompanyDetails.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.CompanyProfile_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvCompanyDetails.DataBind();
                gvCompanyDetails.SelectedIndex = -1;

                Masters.ClearControls(this);
                Masters.Dispose();
            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Grid View Row Databound
    protected void gvCompanyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;
            e.Row.Cells[23].Visible = false;
            e.Row.Cells[24].Visible = false;
            e.Row.Cells[25].Visible = false;
            e.Row.Cells[26].Visible = false;
            e.Row.Cells[27].Visible = false;
            e.Row.Cells[28].Visible = false;
            e.Row.Cells[29].Visible = false;

            e.Row.Cells[30].Visible = false;
            e.Row.Cells[31].Visible = false;
            if (e.Row.Cells[32].Text == "1") { e.Row.Cells[32].Text = "Active"; }
            else if (e.Row.Cells[32].Text == "0") { e.Row.Cells[32].Text = "Inactive"; }
            
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    int count = General.CountofRecordsWithQuery("select count(*) from YANTRA_COMP_PROFILE where CP_ID = " + Convert.ToInt16(e.Row.Cells[1].Text) + "");
        //    if (count > 0)
        //        (e.Row.FindControl("Image") as Image).ImageUrl = "~/Modules/Masters/ComapanyImage.ashx?id=" + e.Row.Cells[2].Text + "";

        //}
    }
    #endregion

    #region Go Button Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvCompanyDetails.DataBind();
    }
    #endregion

    #region Grid View Selected Index
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion

    #region Image Save
    private void ImageSave()
    {
        ////We are using SQL express.
        //    //My database name is "PictureDb".
        //    string connect=ConfigurationManager.ConnectionStrings["DBCon"].ToString();
        //    SqlConnection conn = new SqlConnection(connect);


        //    conn.Open();
        //    String strQuery = "update YANTRA_COMP_PROFILE set CP_LOGO=@pic WHERE CP_ID=" + CompanyId + "";
        //    SqlCommand cmd = new SqlCommand(strQuery);

        //    MemoryStream stream = new MemoryStream();
        //    pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

        //    byte[] pic = stream.ToArray();

        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = conn;

        //    cmd.Parameters.Add("@pic", pic);

        //    int a = cmd.ExecuteNonQuery();
        //    if (a > 0)
        //    {
        //        MessageBox.Show(this, "Data Uploaded Successfully");
        //    }
        //    else
        //    {
        //        MessageBox.Show(this, "Data  Uploading Failed..!");
        //    }


        //try
        //{
        //    //Read Image Bytes into a byte array
        //    byte[] imageData = ReadFile(txtImagePath.Text);

        //    string connect = ConfigurationManager.ConnectionStrings["DBCon"].ToString();
        //    SqlConnection conn = new SqlConnection(connect);

        //    conn.Open();
        //    //Set insert query
        //    string qry = "insert into ImagesStore (OriginalPath,ImageData) values(@OriginalPath, @ImageData)";

        //    //update YANTRA_COMP_PROFILE set CP_LOGO=@pic WHERE CP_ID=" + CompanyId + "

        //    //Initialize SqlCommand object for insert.
        //    SqlCommand SqlCom = new SqlCommand(qry);

        //    //We are passing Original Image Path and Image byte data as sql parameters.
        //    SqlCom.Parameters.Add(new SqlParameter("@OriginalPath", (object)txtImagePath.Text));
        //    SqlCom.Parameters.Add(new SqlParameter("@ImageData", (object)imageData));

        //    //Open connection and execute insert query.
        //    CN.Open();
        //    SqlCom.ExecuteNonQuery();
        //    CN.Close();

        //    //Close form and return to list or images.
        //    this.Close();
    }
    #endregion

    #region Upload Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {

    }
    #endregion

}

