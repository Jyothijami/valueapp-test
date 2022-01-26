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
using vllib;
using System.IO;

public partial class Modules_Masters_Architect : System.Web.UI.UserControl
{
    ScriptManager ScriptManagerLocal;
    protected void Page_Load(object sender, EventArgs e)
    {
        setControlsVisibility();
        Masters.EnquiryMode.EnquiryMode_Select(ddlCategory );
        ScriptManagerLocal = Page.Master.FindControl("ScriptManager1") as ScriptManager;
    }

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

    #region Button SAVE/UPDATE  Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "Save")
        {
            CategorySave();
            tblCompanyDetails.Visible = false;
        }
        else if (btnSave.Text == "Update")
        {
            CategoryUpdate();
            tblCompanyDetails.Visible = false;
        }
        gvArch.SelectedIndex = -1;
    }
    #endregion

    #region CategorySave
    private void CategorySave()
    {
        try
        {
            Masters.Architect objMaster = new Masters.Architect();
            objMaster.Architect_Name = txtArchitectName.Text;
            objMaster.Architect_Address = txtAddress .Text;
            objMaster.Architect_Mobile = txtMobile.Text;
            objMaster.Architect_Email = txtEmail.Text;
            objMaster.Category  = ddlCategory .SelectedItem.Text;
            if (Uploadattach.HasFiles)
            {
                #region Item Attachment
                string Attachment = "";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles"))
                {

                    foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                    {
                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/YANTRA_DOCUMENTS/SOFiles/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        Attachment = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        lblAtt.Text = Attachment;
                        //objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text;
                        //objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                        //objSM.SalesOrderUploads_Save();
                    }

                }
                else
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles");
                    foreach (HttpPostedFile uploadedFile in Uploadattach.PostedFiles)
                    {
                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/YANTRA_DOCUMENTS/SOFiles/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        Attachment = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        lblAtt.Text = Attachment;
                        //objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text;
                        //objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                        //objSM.SalesOrderUploads_Save();
                    }

                }

                #endregion
            }
            if (FileUpload1.HasFiles)
            {
                #region Item Attachment
                string Attachment = "";
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles"))
                {

                    foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                    {
                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/YANTRA_DOCUMENTS/SOFiles/");
                        string fileName = System.IO.Path.GetFileName(FileUpload1.FileName);

                        Attachment = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        lblAtt1.Text = Attachment;
                        //objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text;
                        //objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                        //objSM.SalesOrderUploads_Save();
                    }

                }
                else
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/YANTRA_DOCUMENTS/SOFiles");
                    foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                    {
                        Random rand = new Random();
                        string randNumber = Convert.ToString(rand.Next(0, 10000));
                        string path = Server.MapPath("~/YANTRA_DOCUMENTS/SOFiles/");
                        string fileName = System.IO.Path.GetFileName(uploadedFile.FileName);

                        Attachment = randNumber + "_" + fileName;
                        uploadedFile.SaveAs(path + randNumber + "_" + fileName);
                        lblAtt1.Text = Attachment;
                        //objSM.SOFileContentType = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Second.ToString() + lblSOIdHidden.Text;
                        //objSM.SOUploadDate = DateTime.Now.ToShortDateString();
                        //objSM.SalesOrderUploads_Save();
                    }

                }

                #endregion
            }
            objMaster.UPLOAD1 = lblAtt.Text;
            objMaster.UPLOAD2 = lblAtt1.Text;
            MessageBox.Show(this, objMaster.Architect_Save());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {

            gvArch.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region CategoryUpdate
    private void CategoryUpdate()
    {
        try
        {
            Masters.Architect objMaster = new Masters.Architect();
            objMaster.Architect_Id = gvArch.SelectedRow.Cells[1].Text;
            objMaster.Architect_Name = txtArchitectName.Text;
            objMaster.Architect_Address = txtAddress.Text;
            objMaster.Architect_Mobile = txtMobile.Text;
            objMaster.Architect_Email = txtEmail.Text;
            objMaster.Category = ddlCategory.SelectedItem.Text;

            MessageBox.Show(this, objMaster.Architect_Update());
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message);
        }
        finally
        {
            gvArch.DataBind();
            Masters.ClearControls(this);
            Masters.Dispose();
        }
    }
    #endregion

    #region gvCompanyDetails_RowDataBound
    protected void gvCompanyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager && e.Row.RowType != DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    #endregion

    #region Link Button lbtnCompanyName_Click
    protected void lbtnCompanyName_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
        LinkButton lbtnCompanyName;
        lbtnCompanyName = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnCompanyName.Parent.Parent;
        gvArch.SelectedIndex = gvRow.RowIndex;
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this Record !');");
    }
    #endregion

    #region Button EDIT Click
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (gvArch.SelectedIndex > -1)
        {
            tblCompanyDetails.Visible = true;
            txtArchitectName.Text = gvArch.SelectedRow.Cells[0].Text;
            txtAddress.Text = gvArch.SelectedRow.Cells[3].Text;
            txtMobile.Text  = gvArch.SelectedRow.Cells[4].Text;
            txtEmail.Text = gvArch.SelectedRow.Cells[5].Text;
            btnSave.Text = "Update";
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
    #endregion

    #region Button DELETE Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (gvArch.SelectedIndex > -1)
        {
            try
            {
                Masters.Architect objMaster = new Masters.Architect();
                objMaster.Architect_Id = gvArch.SelectedRow.Cells[1].Text;
                MessageBox.Show(this, objMaster.Architect_Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {

                gvArch.DataBind();
                gvArch.SelectedIndex = -1;

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

    #region Button NEW Ckick
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
        btnSave.Text = "Save";
        tblCompanyDetails.Visible = true;
        //ScriptManagerLocal.SetFocus(txtArchitectName);
    }
    #endregion

    #region Button SEARCH GO Click
    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        gvArch.DataBind();
    }
    #endregion

    #region Button CLOSE Click
    protected void btnClose_Click(object sender, EventArgs e)
    {
        tblCompanyDetails.Visible = false;
    }
    #endregion

    #region Button REFRESH Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Masters.ClearControls(this);
    }
    #endregion

    #region Dropdown list select index change
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        //ScriptManagerLocal.SetFocus(txtSearchText);
    }
    #endregion
}