using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yantra.Classes;
using Yantra.MessageBox;
using YantraBLL.Modules;

public partial class Modules_Inventory_DCDoc1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Qid = Request.QueryString["Cid"].ToString();
        if (!IsPostBack)
        {
            Inventory.Delivery obj = new Inventory.Delivery();

            if (obj.Delivery_Select(Request.QueryString["Cid"].ToString()) > 0)
            {

                txtClientsName.Text = obj.DCNo;
                txtDCNo.Text = obj.DCDate;
            }
            txtfloorplanreceiveddate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            General.GridBindwithCommand(gvFloorPlan, "select * from DC_Documents where DCId= '" + Request.QueryString["Cid"].ToString() + "' order by ISSUEDDATE desc");

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Inventory.Delivery objSM = new Inventory.Delivery();
            foreach (HttpPostedFile uploadfile in FileUpload2.PostedFiles)
            {
                string Attachment = "";
                objSM.IssuedDate = General.toMMDDYYYY(txtfloorplanreceiveddate.Text);
                objSM.Remarks = txtfloorplanremarks.Text;
                objSM.DCId  = Request.QueryString["Cid"].ToString();

                Random rand = new Random();
                string randNumber = Convert.ToString(rand.Next(0, 10000));
                string path = Server.MapPath("~/Content/FloorPlanDrawings/");
                string fileName = System.IO.Path.GetFileName(uploadfile.FileName);

                Attachment = randNumber + "_" + fileName;
                uploadfile.SaveAs(path + randNumber + "_" + fileName);
                objSM.FileName = Attachment;
                objSM.DCDOC_Save();
                MessageBox.Show(this, "Data Saved Successfully");

            }
        }
        catch (Exception ex) { }
        finally
        {
            txtfloorplanremarks.Text = "";
            gvFloorPlan.DataBind();
            General.GridBindwithCommand(gvFloorPlan, "select * from DC_Documents where DCId= '" + Request.QueryString["Cid"].ToString() + "'");

            //  SM.Dispose();
            //Response.Redirect("~/Modules/Sales/SalesEnquiry.aspx");
        }
    }
    protected void lbtnFloorDetails_Click(object sender, EventArgs e)
    {
        LinkButton lbtnRegionalMaster;
        lbtnRegionalMaster = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbtnRegionalMaster.Parent.Parent;
        gvFloorPlan.SelectedIndex = gvRow.RowIndex;

        if (gvFloorPlan.SelectedIndex > -1)
        {
            try
            {
                Inventory.Delivery objSM = new Inventory.Delivery();
                MessageBox.Show(this, objSM.DCDocDetails_Delete(gvFloorPlan.SelectedRow.Cells[0].Text));
                // MessageBox.Show(this, "Data Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            finally
            {


                gvFloorPlan.DataBind();
                General.GridBindwithCommand(gvFloorPlan, "select * from DC_Documents where DCId= '" + Request.QueryString["Cid"].ToString() + "'");

            }
        }
        else
        {
            MessageBox.Show(this, "Please select atleast a Record");
        }
    }
}